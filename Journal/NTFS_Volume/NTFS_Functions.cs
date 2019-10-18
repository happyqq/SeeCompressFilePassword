using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Journal.NTFS_Volume
{
    public static class NTFS_Functions
    {
        private static readonly int BUF_LEN = 8192 + 8;//8 bytes for the leading USN
        public static void QueryUsnJournal(SafeFileHandle roothandle, ref Win32Api.USN_JOURNAL_DATA usnJournalState)
        {
            int sizeUsnJournalState = Marshal.SizeOf(usnJournalState);
            UInt32 cb;
            if(!Win32Api.DeviceIoControl(
                   roothandle.DangerousGetHandle(),
                  Win32Api.FSCTL_QUERY_USN_JOURNAL,
                  IntPtr.Zero,
                  0,
                  out usnJournalState,
                  sizeUsnJournalState,
                  out cb,
                  IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());

            }
        }
        public static SafeFileHandle GetRootHandle(System.IO.DriveInfo driveInfo)
        {
            string vol = string.Concat("\\\\.\\", driveInfo.Name.TrimEnd('\\'));
            var handle = Win32Api.CreateFile(vol,
                 Win32Api.GENERIC_READ | Win32Api.GENERIC_WRITE,
                 Win32Api.FILE_SHARE_READ | Win32Api.FILE_SHARE_WRITE,
                 IntPtr.Zero,
                 Win32Api.OPEN_EXISTING,
                 0,
                 IntPtr.Zero);
            if(handle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return handle;
        }

        public static void GetUsnJournalEntries(SafeFileHandle roothandle, Win32Api.USN_JOURNAL_DATA previousUsnState,
            UInt32 reasonMask,
            out List<Win32Api.UsnEntry> usnEntries,
            out Win32Api.USN_JOURNAL_DATA newUsnState)
        {

            usnEntries = new List<Win32Api.UsnEntry>();
            newUsnState = new Win32Api.USN_JOURNAL_DATA();

            QueryUsnJournal(roothandle, ref newUsnState);

            Win32Api.READ_USN_JOURNAL_DATA rujd = new Win32Api.READ_USN_JOURNAL_DATA();
            rujd.StartUsn = previousUsnState.NextUsn;
            rujd.ReasonMask = reasonMask;
            rujd.ReturnOnlyOnClose = 0;
            rujd.Timeout = 0;
            rujd.bytesToWaitFor = 0;
            rujd.UsnJournalId = previousUsnState.UsnJournalID;

            using(var med_struct = new StructWrapper(rujd))
            using(var rawdata = new Raw_Array_Wrapper(BUF_LEN))
            {
                uint outBytesReturned = 0;
                var nextusn = previousUsnState.NextUsn;
                while(nextusn < newUsnState.NextUsn && Win32Api.DeviceIoControl(
                         roothandle.DangerousGetHandle(),
                        Win32Api.FSCTL_READ_USN_JOURNAL,
                        med_struct.Ptr,
                        med_struct.Size,
                        rawdata.Ptr,
                        rawdata.Size,
                        out outBytesReturned,
                        IntPtr.Zero))
                {
                    outBytesReturned = outBytesReturned - sizeof(Int64);
                    IntPtr pUsnRecord = System.IntPtr.Add(rawdata.Ptr, sizeof(Int64));//point safe arithmetic!~!!
                    while(outBytesReturned > 60)   // while there are at least one entry in the usn journal
                    {
                        var usnEntry = new Win32Api.UsnEntry(pUsnRecord);
                        if(usnEntry.USN > newUsnState.NextUsn)
                            break;
                        usnEntries.Add(usnEntry);
                        pUsnRecord = System.IntPtr.Add(pUsnRecord, (int)usnEntry.RecordLength);//point safe arithmetic!~!!   
                        outBytesReturned -= usnEntry.RecordLength;
                    }
                    nextusn = Marshal.ReadInt64(rawdata.Ptr, 0);
                    Marshal.WriteInt64(med_struct.Ptr, nextusn);//read the usn that we skipped and place it into the nextusn
                }
            }

        }

        public static bool Build_Volume_Mapping(SafeFileHandle roothandle, Win32Api.USN_JOURNAL_DATA currentUsnState, Action<Win32Api.UsnEntry> func)
        {
            Debug.WriteLine("Starting Build_Volume_Mapping");
       
            DateTime startTime = DateTime.Now;

            Win32Api.MFT_ENUM_DATA med;
            med.StartFileReferenceNumber = 0;
            med.LowUsn = 0;
            med.HighUsn = currentUsnState.NextUsn;

            using(var med_struct = new StructWrapper(med))
            using(var rawdata = new Raw_Array_Wrapper(BUF_LEN))
            {
                uint outBytesReturned = 0;

                while(Win32Api.DeviceIoControl(
                    roothandle.DangerousGetHandle(),
                    Win32Api.FSCTL_ENUM_USN_DATA,
                    med_struct.Ptr,
                    med_struct.Size,
                    rawdata.Ptr,
                    rawdata.Size,
                    out outBytesReturned,
                    IntPtr.Zero))
                {
                    outBytesReturned = outBytesReturned - sizeof(Int64);
                    IntPtr pUsnRecord = System.IntPtr.Add(rawdata.Ptr, sizeof(Int64));//need to skip 8 bytes because the first 8 bytes are to a usn number, which isnt in the structure
                    while(outBytesReturned > 60)
                    {
                        var usnEntry = new Win32Api.UsnEntry(pUsnRecord);
                        pUsnRecord = System.IntPtr.Add(pUsnRecord, (int)usnEntry.RecordLength);
                        func(usnEntry);
                     
                        if(usnEntry.RecordLength > outBytesReturned)
                            outBytesReturned = 0;// prevent overflow
                        else
                            outBytesReturned -= usnEntry.RecordLength;
                    }
                    Marshal.WriteInt64(med_struct.Ptr, Marshal.ReadInt64(rawdata.Ptr, 0));//read the usn that we skipped and place it into the nextusn
                }
                var possiblerror = Marshal.GetLastWin32Error();
                if(possiblerror < 0)
                    throw new Win32Exception(possiblerror);
            }
            Debug.WriteLine("Time took: " + (DateTime.Now - startTime).TotalMilliseconds + "ms");
            return true;
        }
        public static List<Win32Api.UsnEntry> Get_Changes(SafeFileHandle roothandle, Win32Api.USN_JOURNAL_DATA startUsnState){
   
            uint reasonMask = Win32Api.USN_REASON_DATA_OVERWRITE |
                   Win32Api.USN_REASON_DATA_EXTEND |
                   Win32Api.USN_REASON_NAMED_DATA_OVERWRITE |
                   Win32Api.USN_REASON_NAMED_DATA_TRUNCATION |
                    Win32Api.USN_REASON_FILE_CREATE | 
                    Win32Api.USN_REASON_FILE_DELETE |  
                   Win32Api.USN_REASON_EA_CHANGE |
                   Win32Api.USN_REASON_SECURITY_CHANGE |
                   Win32Api.USN_REASON_RENAME_OLD_NAME |
                   Win32Api.USN_REASON_RENAME_NEW_NAME |
                   Win32Api.USN_REASON_INDEXABLE_CHANGE |
                   Win32Api.USN_REASON_BASIC_INFO_CHANGE |
                   Win32Api.USN_REASON_HARD_LINK_CHANGE |
                   Win32Api.USN_REASON_COMPRESSION_CHANGE |
                   Win32Api.USN_REASON_ENCRYPTION_CHANGE |
                   Win32Api.USN_REASON_OBJECT_ID_CHANGE |
                   Win32Api.USN_REASON_REPARSE_POINT_CHANGE |
                   Win32Api.USN_REASON_STREAM_CHANGE |
            Win32Api.USN_REASON_CLOSE;

            var changes = new List<Win32Api.UsnEntry>();
            Win32Api.USN_JOURNAL_DATA newUsnState;
            GetUsnJournalEntries(roothandle, startUsnState, reasonMask, out changes, out newUsnState);
            return changes;
        }
        
    }
}
