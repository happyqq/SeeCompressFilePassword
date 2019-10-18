using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    public static class Win32Api
    {


        public enum FILE_INFORMATION_CLASS
        {
            FileDirectoryInformation = 1,     // 1
            FileFullDirectoryInformation = 2,     // 2
            FileBothDirectoryInformation = 3,     // 3
            FileBasicInformation = 4,         // 4
            FileStandardInformation = 5,      // 5
            FileInternalInformation = 6,      // 6
            FileEaInformation = 7,        // 7
            FileAccessInformation = 8,        // 8
            FileNameInformation = 9,          // 9
            FileRenameInformation = 10,        // 10
            FileLinkInformation = 11,          // 11
            FileNamesInformation = 12,         // 12
            FileDispositionInformation = 13,       // 13
            FilePositionInformation = 14,      // 14
            FileFullEaInformation = 15,        // 15
            FileModeInformation = 16,     // 16
            FileAlignmentInformation = 17,     // 17
            FileAllInformation = 18,           // 18
            FileAllocationInformation = 19,    // 19
            FileEndOfFileInformation = 20,     // 20
            FileAlternateNameInformation = 21,     // 21
            FileStreamInformation = 22,        // 22
            FilePipeInformation = 23,          // 23
            FilePipeLocalInformation = 24,     // 24
            FilePipeRemoteInformation = 25,    // 25
            FileMailslotQueryInformation = 26,     // 26
            FileMailslotSetInformation = 27,       // 27
            FileCompressionInformation = 28,       // 28
            FileObjectIdInformation = 29,      // 29
            FileCompletionInformation = 30,    // 30
            FileMoveClusterInformation = 31,       // 31
            FileQuotaInformation = 32,         // 32
            FileReparsePointInformation = 33,      // 33
            FileNetworkOpenInformation = 34,       // 34
            FileAttributeTagInformation = 35,      // 35
            FileTrackingInformation = 36,      // 36
            FileIdBothDirectoryInformation = 37,   // 37
            FileIdFullDirectoryInformation = 38,   // 38
            FileValidDataLengthInformation = 39,   // 39
            FileShortNameInformation = 40,     // 40
            FileHardLinkInformation = 46    // 46    
        }


public const UInt32 SECURITY_ANONYMOUS     =     ( 0 << 16 );
public const UInt32 SECURITY_IDENTIFICATION  =   ( 1 << 16 );
public const UInt32 SECURITY_IMPERSONATION   =   ( 2 << 16 );
public const UInt32 SECURITY_DELEGATION      =   ( 3 << 16 );

public const UInt32 SECURITY_CONTEXT_TRACKING = 0x00040000;
public const UInt32 SECURITY_EFFECTIVE_ONLY  =  0x00080000;


        public const UInt32 GENERIC_READ = 0x80000000;
        public const UInt32 GENERIC_WRITE = 0x40000000;
        public const UInt32 FILE_SHARE_READ = 0x00000001;
        public const UInt32 FILE_SHARE_WRITE = 0x00000002;
        public const UInt32 FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        public const UInt32 CREATE_NEW = 1;
        public const UInt32 CREATE_ALWAYS = 2;
        public const UInt32 OPEN_EXISTING = 3;
        public const UInt32 OPEN_ALWAYS = 4;
        public const UInt32 TRUNCATE_EXISTING = 5;

        public const UInt32 FILE_ATTRIBUTE_NORMAL = 0x80;
        public const UInt32 FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
        public const UInt32 FileNameInformationClass = 9;
        public const UInt32 FILE_OPEN_FOR_BACKUP_INTENT = 0x4000;
        public const UInt32 FILE_OPEN_BY_FILE_ID = 0x2000;
        public const UInt32 FILE_OPEN = 0x1;
        public const UInt32 OBJ_CASE_INSENSITIVE = 0x40;

        private const UInt32 FILE_DEVICE_FILE_SYSTEM = 0x00000009;
        private const UInt32 METHOD_NEITHER = 3;
        private const UInt32 METHOD_BUFFERED = 0;
        private const UInt32 FILE_ANY_ACCESS = 0;
        private const UInt32 FILE_SPECIAL_ACCESS = 0;
        private const UInt32 FILE_READ_ACCESS = 1;
        private const UInt32 FILE_WRITE_ACCESS = 2;

        //goto http://msdn.microsoft.com/en-us/library/windows/desktop/aa365722%28v=vs.85%29.aspx  for the descriptions of below
        public const UInt32 USN_REASON_DATA_OVERWRITE = 0x00000001;
        public const UInt32 USN_REASON_DATA_EXTEND = 0x00000002;
        public const UInt32 USN_REASON_DATA_TRUNCATION = 0x00000004;
        public const UInt32 USN_REASON_NAMED_DATA_OVERWRITE = 0x00000010;
        public const UInt32 USN_REASON_NAMED_DATA_EXTEND = 0x00000020;
        public const UInt32 USN_REASON_NAMED_DATA_TRUNCATION = 0x00000040;
        public const UInt32 USN_REASON_FILE_CREATE = 0x00000100;
        public const UInt32 USN_REASON_FILE_DELETE = 0x00000200;
        public const UInt32 USN_REASON_EA_CHANGE = 0x00000400;
        public const UInt32 USN_REASON_SECURITY_CHANGE = 0x00000800;
        public const UInt32 USN_REASON_RENAME_OLD_NAME = 0x00001000;
        public const UInt32 USN_REASON_RENAME_NEW_NAME = 0x00002000;
        public const UInt32 USN_REASON_INDEXABLE_CHANGE = 0x00004000;
        public const UInt32 USN_REASON_BASIC_INFO_CHANGE = 0x00008000;
        public const UInt32 USN_REASON_HARD_LINK_CHANGE = 0x00010000;
        public const UInt32 USN_REASON_COMPRESSION_CHANGE = 0x00020000;
        public const UInt32 USN_REASON_ENCRYPTION_CHANGE = 0x00040000;
        public const UInt32 USN_REASON_OBJECT_ID_CHANGE = 0x00080000;
        public const UInt32 USN_REASON_REPARSE_POINT_CHANGE = 0x00100000;
        public const UInt32 USN_REASON_STREAM_CHANGE = 0x00200000;
        public const UInt32 USN_REASON_CLOSE = 0x80000000;

        public const UInt32 FSCTL_GET_OBJECT_ID = 0x9009c;
        public const UInt32 FSCTL_ENUM_USN_DATA = (FILE_DEVICE_FILE_SYSTEM << 16) | (FILE_ANY_ACCESS << 14) | (44 << 2) | METHOD_NEITHER;
        public const UInt32 FSCTL_READ_USN_JOURNAL = (FILE_DEVICE_FILE_SYSTEM << 16) | (FILE_ANY_ACCESS << 14) | (46 << 2) | METHOD_NEITHER;
        public const UInt32 FSCTL_CREATE_USN_JOURNAL = (FILE_DEVICE_FILE_SYSTEM << 16) | (FILE_ANY_ACCESS << 14) | (57 << 2) | METHOD_NEITHER;
        public const UInt32 FSCTL_QUERY_USN_JOURNAL = (FILE_DEVICE_FILE_SYSTEM << 16) | (FILE_ANY_ACCESS << 14) | (61 << 2) | METHOD_BUFFERED;
        public const UInt32 FSCTL_DELETE_USN_JOURNAL = (FILE_DEVICE_FILE_SYSTEM << 16) | (FILE_ANY_ACCESS << 14) | (62 << 2) | METHOD_BUFFERED;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle
            CreateFile(string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool
            GetFileInformationByHandle(
            IntPtr hFile,
            out BY_HANDLE_FILE_INFORMATION lpFileInformation);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeviceIoControl(
            IntPtr hDevice,
            UInt32 dwIoControlCode,
            IntPtr lpInBuffer,
            Int32 nInBufferSize,
            out USN_JOURNAL_DATA lpOutBuffer,
            Int32 nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern void ZeroMemory(IntPtr ptr, int size);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeviceIoControl(
            IntPtr hDevice,
            UInt32 dwIoControlCode,
            IntPtr lpInBuffer,
            Int32 nInBufferSize,
            IntPtr lpOutBuffer,
            Int32 nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped);


        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int NtCreateFile(
            out IntPtr handle,
            FileAccess access,
            ref OBJECT_ATTRIBUTES objectAttributes,
            ref IO_STATUS_BLOCK ioStatus,
            ref long allocSize,
            uint fileAttributes,
            FileShare share,
            uint createDisposition,
            uint createOptions,
            IntPtr eaBuffer,
            uint eaLength);


        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int NtQueryInformationFile(
            IntPtr fileHandle,
            ref IO_STATUS_BLOCK IoStatusBlock,
            IntPtr pInfoBlock,
            uint length,
            FILE_INFORMATION_CLASS fileInformation);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BY_HANDLE_FILE_INFORMATION
        {
            public uint FileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME CreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWriteTime;
            public uint VolumeSerialNumber;
            public uint FileSizeHigh;
            public uint FileSizeLow;
            public uint NumberOfLinks;
            public uint FileIndexHigh;
            public uint FileIndexLow;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct USN_JOURNAL_DATA
        {
            public UInt64 UsnJournalID;
            public Int64 FirstUsn;
            public Int64 NextUsn;
            public Int64 LowestValidUsn;
            public Int64 MaxUsn;
            public UInt64 MaximumSize;
            public UInt64 AllocationDelta;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct MFT_ENUM_DATA
        {
            public UInt64 StartFileReferenceNumber;
            public Int64 LowUsn;
            public Int64 HighUsn;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct CREATE_USN_JOURNAL_DATA
        {
            public UInt64 MaximumSize;
            public UInt64 AllocationDelta;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct DELETE_USN_JOURNAL_DATA
        {
            public UInt64 UsnJournalID;
            public UInt32 DeleteFlags;
            public UInt32 Reserved;
        }

        public class UsnEntry
        {
            private const int FR_OFFSET = 8;
            private const int PFR_OFFSET = 16;
            private const int USN_OFFSET = 24;
            private const int REASON_OFFSET = 40;
            public const int FA_OFFSET = 52;
            private const int FNL_OFFSET = 56;
            private const int FN_OFFSET = 58;


            public UInt32 RecordLength { get; private set; }

            public Int64 USN { get; private set; }

            public UInt64 FileReferenceNumber { get; private set; }

            public UInt64 ParentFileReferenceNumber { get; private set; }

            public UInt32 Reason { get; set; }
            public string Name { get; private set; }

            private UInt32 _fileAttributes;
            public bool IsFolder { get { return (_fileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0; } }
            public bool IsFile { get { return (_fileAttributes & FILE_ATTRIBUTE_DIRECTORY) == 0; } }


            /// <summary>
            /// USN Record Constructor
            /// </summary>
            /// <param name="p">Buffer pointer to first byte of the USN Record</param>
            public UsnEntry(IntPtr ptrToUsnRecord)
            {
                RecordLength = (UInt32)Marshal.ReadInt32(ptrToUsnRecord);
                FileReferenceNumber = (UInt64)Marshal.ReadInt64(ptrToUsnRecord, FR_OFFSET);
                ParentFileReferenceNumber = (UInt64)Marshal.ReadInt64(ptrToUsnRecord, PFR_OFFSET);
                USN = (Int64)Marshal.ReadInt64(ptrToUsnRecord, USN_OFFSET);
                Reason = (UInt32)Marshal.ReadInt32(ptrToUsnRecord, REASON_OFFSET);
                _fileAttributes = (UInt32)Marshal.ReadInt32(ptrToUsnRecord, FA_OFFSET);
                short fileNameLength = Marshal.ReadInt16(ptrToUsnRecord, FNL_OFFSET);
                short fileNameOffset = Marshal.ReadInt16(ptrToUsnRecord, FN_OFFSET);
                var ptr_to_name = System.IntPtr.Add(ptrToUsnRecord, fileNameOffset);

                Name = Marshal.PtrToStringUni(ptr_to_name, fileNameLength / sizeof(char));
            }
            public UsnEntry(string name)
            {
                RecordLength = 0;
                FileReferenceNumber = 0;
                ParentFileReferenceNumber = 0;
                USN = 0;
                Reason = 0;
                _fileAttributes = 0;
                Name = name;
            }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct READ_USN_JOURNAL_DATA
        {
            public Int64 StartUsn;
            public UInt32 ReasonMask;
            public UInt32 ReturnOnlyOnClose;
            public UInt64 Timeout;
            public UInt64 bytesToWaitFor;
            public UInt64 UsnJournalId;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct IO_STATUS_BLOCK
        {
            public uint status;
            public IntPtr information;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct OBJECT_ATTRIBUTES
        {
            public Int32 Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName;
            public uint Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;

        }
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;

        }

        const int MAX_PATH = 260;
        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        };



        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
         public struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternate;

        }


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern SafeFileHandle FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

        public static SafeFileHandle Find_First_File(string filepath, out WIN32_FIND_DATA filedata)
        {
            if(string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentNullException("FilePath cannot be null!");
            if(filepath[filepath.Length - 1] != '*')
                filepath += "*";
            if(filepath[filepath.Length - 2] != '\\')
                filepath.Insert(filepath.Length - 2, "\\");

            if(filepath.StartsWith(@"\\"))
                filepath = @"\\?\UNC\" + filepath.Substring(2, filepath.Length - 2);
            else
                filepath = @"\\?\" + filepath;
            return FindFirstFile(filepath, out filedata);
        }


        // Assume dirName passed in is already prefixed with \\?\

        //public static List<string> FindFilesAndDirs(string dirName)
        //{



        //    List<string> results = new List<string>();

        //    WIN32_FIND_DATA findData;

        //    IntPtr findHandle = FindFirstFile(dirName + @"\*", out findData);



        //    if(findHandle != INVALID_HANDLE_VALUE)
        //    {

        //        bool found;

        //        do
        //        {

        //            string currentFileName = findData.cFileName;



        //            // if this is a directory, find its contents

        //            if(((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
        //            {

        //                if(currentFileName != "." && currentFileName != "..")
        //                {

        //                    List<string> childResults = FindFilesAndDirs(Path.Combine(dirName, currentFileName));

        //                    // add children and self to results

        //                    results.AddRange(childResults);

        //                    results.Add(Path.Combine(dirName, currentFileName));

        //                }

        //            }



        //            // it's a file; add it to the results
        //            else
        //            {

        //                results.Add(Path.Combine(dirName, currentFileName));

        //            }



        //            // find next

        //            found = FindNextFile(findHandle, out findData);

        //        }

        //        while(found);

        //    }



        //    // close the find handle

        //    FindClose(findHandle);

        //    return results;

        //}

    }
}
