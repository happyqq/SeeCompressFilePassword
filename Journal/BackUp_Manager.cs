using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup_Service
{
    public static class BackUp_Manager
    {
        public static List<Backup_Model.Backup> Load_Backups(string path)
        {
            try
            {
                var filepath = Path.Combine(path, "savedbackups.bac");
                if(!File.Exists(filepath))
                    return new List<Backup_Model.Backup>();
                return Utilities.BinarySerialization.ReadFromBinaryFile<List<Backup_Model.Backup>>(filepath);
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return new List<Backup_Model.Backup>();
        }
        public static void Save_Backups(string path, List<Backup_Model.Backup> backups)
        {
            try
            {
                Utilities.BinarySerialization.WriteToBinaryFile<List<Backup_Model.Backup>>(Path.Combine(path, "savedbackups.bac"), backups);
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
