using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{

    //保存最近一次的扫描结果
    public class SCFP_files_scans_result
    {

        //1,2,3 仅保存通过 全盘扫描，指定盘符，指定目录 扫描的结果
        public int ScanMode { get; set; }

        public string ScanRange { get; set; } //全盘保存为 \\\\$$ALLDISK$$，其他分别保存为对应扫描路径
        public string fullfilename { get; set; }
        public string filemd5 { get; set; }
        public string filecontent_crc32 { get; set; }
        public DateTime LastDateTime { get; set; }

    }
}
