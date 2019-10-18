using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{

    public class NTFSVolume
    {
        private Dictionary<UInt64, Journal.Volume.NTFS_File> Lookup;
        private SafeFileHandle _Root_Handle;
        private Journal.Volume.NTFS_File _Root;
        public Journal.Volume.NTFS_File Root { get { return _Root; } }

        private Win32Api.USN_JOURNAL_DATA _Current_JournalState;
        public Win32Api.USN_JOURNAL_DATA Current_JournalState { get { return _Current_JournalState; } }
        public NTFSVolume(string drive)
        {
            var rootpath = new DriveInfo(drive);
            _Root_Handle = NTFS_Volume.NTFS_Functions.GetRootHandle(rootpath);
            _Root = new Volume.NTFS_File(new Win32Api.UsnEntry(rootpath.Name.Replace("\\", "")));
            _Current_JournalState = new Win32Api.USN_JOURNAL_DATA();
            Lookup = new Dictionary<ulong, Journal.Volume.NTFS_File>();
            NTFS_Volume.NTFS_Functions.QueryUsnJournal(_Root_Handle, ref _Current_JournalState);//need to query the jounral to get the first usn number
        }
        public NTFSVolume(string drive, Win32Api.USN_JOURNAL_DATA lastusn)
        {
            var rootpath = new DriveInfo(drive);
            _Root_Handle = NTFS_Volume.NTFS_Functions.GetRootHandle(rootpath);
            _Root = new Volume.NTFS_File(new Win32Api.UsnEntry(rootpath.Name.Replace("\\", "")));
            Lookup = new Dictionary<ulong, Journal.Volume.NTFS_File>();
            _Current_JournalState = lastusn;

        }
        public void Map_Volume()
        {
            Lookup = new Dictionary<ulong, Volume.NTFS_File>();
            if(NTFS_Volume.NTFS_Functions.Build_Volume_Mapping(_Root_Handle, _Current_JournalState, Add))
            {

                Debug.WriteLine("Starting Lookup");
                var start = DateTime.Now;
                int foldercounter = 0;
                int filecounter = 0;
                foreach(var item in Lookup)
                {
                    var file = item.Value;
                    if(file.Entry.IsFile)
                        filecounter += 1;
                    else
                        foldercounter += 1;
                    Journal.Volume.NTFS_File parent = null;
                    if(Lookup.TryGetValue(file.Entry.ParentFileReferenceNumber, out parent))
                    {
                        parent.Children.Add(item.Value);
                        item.Value.Parent = parent;
                    } else
                    {
                        _Root.Children.Add(item.Value);
                        item.Value.Parent = _Root;
                    }
                }
                Debug.WriteLine("Time took: " + (DateTime.Now - start).TotalMilliseconds + "ms");
                Debug.WriteLine("Total Files Found: " + filecounter);
                Debug.WriteLine("Total Folders Found: " + foldercounter);

            }//else something went wrong and the build mapping will throw!
        }
        private void Add(Win32Api.UsnEntry u)
        {
            Lookup.Add(u.FileReferenceNumber, new Journal.Volume.NTFS_File(u));
        }
        public Win32Api.USN_JOURNAL_DATA Refresh()
        {
            var old = _Current_JournalState;
            _Current_JournalState = new Win32Api.USN_JOURNAL_DATA();
            NTFS_Volume.NTFS_Functions.QueryUsnJournal(_Root_Handle, ref _Current_JournalState);//need to query the jounral to get the first usn number
            return old;
        }
        public Journal.Volume.NTFS_File GetDirectory(string path)
        {
            var splits = path.Split('\\');
            var r = _Root;
            if(splits.Length > 0)
            {
                if(!r.Name.ToLower().StartsWith(splits[0].ToLower()))
                    return null;
            }
            for(var i = 1; i < splits.Length; i++)
            {
                r = r.GetChildByName(splits[i]);
                if(r == null)
                    return null;
            }
            return r;
        }
        public List<Journal.Volume.NTFS_File> Update(Win32Api.USN_JOURNAL_DATA last)
        {
            var files = new List<Journal.Volume.NTFS_File>();
            var changes = NTFS_Volume.NTFS_Functions.Get_Changes(_Root_Handle, last);
            foreach(var item in changes)
            {
                Journal.Volume.NTFS_File found = null;
                Lookup.TryGetValue(item.FileReferenceNumber, out found);
                if(found != null)
                    Update((Journal.Volume.NTFS_File)found, item);

            }
            return files;
        }

        private void Update(Journal.Volume.NTFS_File original, Win32Api.UsnEntry usnEntry)
        {
            original.Entry.Reason = usnEntry.Reason;
            Debug.WriteLine("Marking '" + original.Name + "' as dirty ");
            //return;
            //// original.Entry.Reason = usnEntry;
            //uint value = usnEntry.Reason &
            //    (Win32Api.USN_REASON_DATA_OVERWRITE |
            //    Win32Api.USN_REASON_DATA_EXTEND |
            //    Win32Api.USN_REASON_DATA_TRUNCATION |
            //    Win32Api.USN_REASON_NAMED_DATA_OVERWRITE |
            //    Win32Api.USN_REASON_NAMED_DATA_EXTEND |
            //    Win32Api.USN_REASON_NAMED_DATA_TRUNCATION |
            //    Win32Api.USN_REASON_FILE_DELETE);
            //var sb = new StringBuilder();
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -FILE DELETE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_EA_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -EA CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_SECURITY_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -SECURITY CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_RENAME_OLD_NAME;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -RENAME OLD NAME");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_RENAME_NEW_NAME;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -RENAME NEW NAME");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_INDEXABLE_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -INDEXABLE CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_BASIC_INFO_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -BASIC INFO CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_HARD_LINK_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -HARD LINK CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_COMPRESSION_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -COMPRESSION CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_ENCRYPTION_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -ENCRYPTION CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_OBJECT_ID_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -OBJECT ID CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_REPARSE_POINT_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -REPARSE POINT CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_STREAM_CHANGE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -STREAM CHANGE");
            //}
            //value = usnEntry.Reason & Win32Api.USN_REASON_CLOSE;
            //if(0 != value)
            //{
            //    sb.AppendFormat("\n     -CLOSE");
            //}
            //Debug.WriteLine("Changes " + sb.ToString());
        }
        public void Dispose()
        {
            _Root_Handle.Dispose();
        }

    }
}
