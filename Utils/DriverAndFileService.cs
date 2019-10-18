using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Management;

namespace Utils
{
    public class DriverAndFileService
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        List<String> fileList = new System.Collections.Generic.List<string>();
        /// <summary>
        /// 获得驱动器列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<String> getDriversList()
        {
            List<String> list = new System.Collections.Generic.List<string>();
            DriveInfo[] volumes = DriveInfo.GetDrives();

            foreach (DriveInfo di in volumes)
            {
                list.Add(di.Name);
            }
            return list;
        }

        /// <summary>
        /// 列出文件夹下面的所有文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="Filter">Filter为null列出所有文件，注意扩展名必须带.</param>
        /// <returns></returns>
        public List<String> getPathFiles(FileSystemInfo info, String[] Filter, System.ComponentModel.BackgroundWorker worker)
        {
            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录 

            try
            {
                FileSystemInfo[] files = dir.GetFileSystemInfos();
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = files[i] as FileInfo;

                    if (file != null)
                    {
                        //不获取回收站的内容
                        if (file.FullName.Contains("$RECYCLE.BIN") == false)
                        {
                            if (Filter != null)
                            {
                                foreach (String f in Filter)
                                {
                                    if (file.Extension.ToLower().Equals(f.ToLower()))
                                    {
                                        fileList.Add(file.FullName);
                                        if (worker != null)
                                        {
                                            worker.ReportProgress(fileList.Count);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                fileList.Add(file.FullName);
                            }

                        }
                    }
                    //对于子目录，进行递归调用 
                    else
                    {
                        getPathFiles(files[i], Filter, worker);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileList;
        }


        /// <summary>
        /// 列出文件夹下面的所有文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public List<String> getPathFiles(FileSystemInfo info, String[] Filter)
        {
            return getPathFiles(info, Filter, null);

        }


        /// <summary>
        /// 检查文件是否处于编辑状态
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static bool isUsing(String FileName)
        {
            string vFileName = FileName;
            IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                return true;
            }
            CloseHandle(vHandle);
            return false;
        }

        public static List<string> GetNtfsFiles(IEnumerable<string> enumList, String[] Filter)
        {
            List<String> ntfsList = new List<string>();
            foreach (String s in enumList)
            {
                foreach (String filter in Filter)
                {
                    if (s.ToLower().EndsWith(filter))
                    {
                        if (File.Exists(s) && (s.Contains("$RECYCLE.BIN") == false) && (File.GetAttributes(s) != FileAttributes.Hidden))
                        {
                            ntfsList.Add(s);
                        }
                    }
                }
            }
            return ntfsList;
        }
    }
}
