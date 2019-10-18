using Journal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup_Model
{
    [Serializable]
    public class Backup
    {
        private string _Name;
        public string Name { get { return _Name; } }

        public int Increment;

        public DateTime LastBackup;
        private string _SourcePath;
        public string SourcePath { get { return _SourcePath; } }

        private string _DestnationPath;
        public string DestnationPath { get { return _DestnationPath; } }

        private Win32Api.USN_JOURNAL_DATA _LastJournal;

        [NonSerialized]
        public NTFSVolume Vol;

        public Backup(string name, string sourcepath, string destinationpath)
        {
            _Name = name;
            _SourcePath = sourcepath;
            _DestnationPath = destinationpath;
            LastBackup = DateTime.MinValue;
            Increment = 0;
            Init_Volume();
        }
        public static bool ValidatePath(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                return false;
            if(path.Length < 3)
                return false;
            var drive = path.Substring(0, 1);
            try
            {
                return System.IO.DriveInfo.GetDrives().Any(a => a.Name.ToLower().Contains(drive.ToLower()) && a.DriveFormat.ToLower().Contains("ntfs"));
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }
        private void Init_Volume(){
            if(Vol == null)
            {
                if(_LastJournal.NextUsn != 0)
                {
                    Vol = new NTFSVolume(SourcePath.Substring(0, 1), _LastJournal);

                } else
                {
                    Vol = new NTFSVolume(SourcePath.Substring(0, 1));
                }
            }
        }
        public void Do_Backup()
        {
            Init_Volume();
            
            _LastJournal = Vol.Refresh();
            Vol.Map_Volume();
            Vol.Update(_LastJournal);
            var r = Vol.GetDirectory(SourcePath);

            if(r != null)
            {
                var dst = DestnationPath + "\\test_" + Increment.ToString();

                var src = "";
                if(Increment == 0)
                {
                    src = dst;
                } else
                {
                    src = DestnationPath + "\\test_" + (Increment - 1).ToString();
                }

                r.CopyTo(dst, src);
            }
            Increment += 1;

        }
    }
}
