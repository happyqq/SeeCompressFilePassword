using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Volume
{

    public class NTFS_File
    {
        public Win32Api.UsnEntry Entry { get; set; }
        private NTFS_File _Parent;
        public NTFS_File Parent { get { return _Parent; } set { _Parent = value; } }

        public List<NTFS_File> Children { get; set; }

        private int _FileCount = -1;
        private int _FolderCount = -1;
        public bool IsFile { get { return Entry.IsFile; } }
        public bool IsFolder { get { return Entry.IsFolder; } }

        public NTFS_File(Win32Api.UsnEntry u)
        {
            Entry = u;
            Children = new List<NTFS_File>();
            Parent = null;
        }

        public string Name { get { return Entry.Name; } }
        public string FullName
        {
            get
            {
                var par = Parent;
                var fullname = Entry.Name;
                while(par != null)
                {
                    fullname = par.Name + "\\" + fullname;
                    par = par.Parent;
                }
                return fullname;
            }
        }

        public Journal.Volume.NTFS_File GetChildByName(string name)
        {
            return Children.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
        }
        public void CopyTo(string dst_base_path, string source_path)
        {
            if(IsFolder){
                var dst = System.IO.Path.Combine(dst_base_path, Name);
                var src = System.IO.Path.Combine(source_path, Name);
                try{
                    System.IO.Directory.CreateDirectory(dst);//this will fail if the directory already exists, no point in checking to see if it exists first....
                }catch(Exception e){
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                dst_base_path = dst;
                source_path = src;
            }else if(IsFile){
                try{
                    if(Entry.Reason != 0)
                    {//DIRTY, update from REAL SOURCE
                        Debug.WriteLine("File Dirty, using NON-Local Copy of " + Name);
                        System.IO.File.Copy(FullName, System.IO.Path.Combine(dst_base_path, Name));
                    } else
                    {//try and use the local copy 
                        var trysrc = System.IO.Path.Combine(source_path, Name);
                        if(System.IO.File.Exists(trysrc))
                        {
                            Debug.WriteLine("Using Local Copy of " + Name);
                            System.IO.File.Copy(trysrc, System.IO.Path.Combine(dst_base_path, Name));
                        } else
                        {
                            Debug.WriteLine("Using NON-Local Copy of " + Name);
                            System.IO.File.Copy(FullName, System.IO.Path.Combine(dst_base_path, Name));
                        }
                    }
                }catch(Exception e){
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
            foreach(var item in Children){
                item.CopyTo(dst_base_path, source_path);
            }
        }
        public void Delete()
        {
            if(_Parent != null)
            {
                var par = (NTFS_File)_Parent;
                par.Children.Remove(this);
            }
        }
        public int FileCount()
        {
            if(_FileCount != -1)
                return _FileCount;
            _FileCount = 0;
            if(Entry.IsFile)
                _FileCount += 1;
            foreach(var item in Children)
                _FileCount += item.FileCount();
            return _FileCount;
        }

        public int FolderCount()
        {
            if(_FolderCount != -1)
                return _FolderCount;
            _FolderCount = 0;
            if(Entry.IsFolder)
                _FolderCount += 1;
            foreach(var item in Children)
                _FolderCount += item.FolderCount();
            return _FolderCount;
        }
        public override string ToString()
        {
            var t = "Entry: '" + Entry.Name + "'";
            if(Parent != null)
                t += " Parent: '" + Parent.Name + "'";
            t += "\n\tFileCount: " + FileCount() + " FolderCount: " + FolderCount();
            return t;
        }
    }
}
