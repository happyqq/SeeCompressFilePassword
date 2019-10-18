using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
  //  id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
  //filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  //filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  //filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  //filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  //fileid int(10) unsigned NOT NULL DEFAULT '0',		# 文件ID
  //uid int(10) unsigned NOT NULL DEFAULT '0',		# 用户ID
  //author varchar(20) NOT NULL DEFAULT '',		# 分享作者
    public class SCFP_files_OtherPwd
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string filemd5 { get; set; }
        public string filecontent_crc32 { get; set; }
        public string filepassword { get; set; }
        public string fileshareurl { get; set; }
        public int fileid { get; set; }
        public int uid { get; set; }
        public string author { get; set; }
        public int returnCode { get; set; } //200 -- INFO 404 -- Error 300 -- Alert
        public string returnDetail { get; set; }
    }
}
