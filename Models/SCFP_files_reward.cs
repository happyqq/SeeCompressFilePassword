using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
  //    id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
  //fileid int(10) unsigned NOT NULL DEFAULT '0',		# 文件ID
  //filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  //filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  //filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  //filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  //uid_pay int(10) unsigned NOT NULL DEFAULT '0',		# 已出悬赏分数的用户ID
  //uid_pay_me int(10) unsigned NOT NULL DEFAULT '0',		# 抢悬赏分数的用户ID
  //score int(10) unsigned NOT NULL DEFAULT '1',		# 悬赏的分数
  //author_pay varchar(20) NOT NULL DEFAULT '',		# 悬赏作者
  //author_pay_me varchar(20) NOT NULL DEFAULT '',		# 抢悬赏作者
  //source varchar(150) NOT NULL DEFAULT '',		# 分享来源
  //dateline int(10) unsigned NOT NULL DEFAULT '0',	# 创建时间
  //isOK tinyint(1) unsigned NOT NULL default '0', # 是否已经确认
    public class SCFP_files_reward
    {
        public int id { get; set; }
        public int fileid { get; set; }
        public string filename { get; set; }
        public string filemd5 { get; set; }

        public string filecontent_crc32 { get; set; }
        public string filepassword { get; set; }
        public string fileshareurl { get; set; }

        public string fileshareurl_pay_me { get; set; }
        public int uid_pay { get; set; }
        public int uid_pay_me { get; set; }
        public int score { get; set; }
        public string author_pay { get; set; }
        public string author_pay_me { get; set; }
        public string source { get; set; }
        public int isPasswordOK { get; set; }

        public int isShareFileOK { get; set; }

        public int returnCode { get; set; } //200 -- INFO 404 -- Error 300 -- Alert
        public string returnDetail { get; set; }
    }
}
