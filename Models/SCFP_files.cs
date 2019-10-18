using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
  //  id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 压缩包ID
  //filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  //filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  //filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  //filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  //uid int(10) unsigned NOT NULL DEFAULT '0',		# 用户ID
  //score int(10) unsigned NOT NULL DEFAULT '1',		# 所需要消费的分数
  //author varchar(20) NOT NULL DEFAULT '',		# 分享作者
  //source varchar(150) NOT NULL DEFAULT '',		# 分享来源
  //dateline int(10) unsigned NOT NULL DEFAULT '0',	# 创建时间
  //lasttime int(10) unsigned NOT NULL DEFAULT '0',	# 更新时间 
  //rights int(10) unsigned NOT NULL DEFAULT '0',	# 正确数 为所有扫描用的数
  //wrongs int(10) unsigned NOT NULL DEFAULT '0',	# 错误数 为所有用户提出错误的数目
    public class SCFP_files
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string filemd5 { get; set; }


        public string filecontent_crc32 { get; set; }
        public string filepassword { get; set; }

        public string fileshareurl { get; set; }
       

        public int uid { get; set; }  // 等于SCFP_user_recv.uid
        public int score { get; set; }  //等于全局获取的分数  UserDefaultTaskScoreCost,同时 users 的表中对应的
        public string author { get; set; }  //等于SCFP_user_recv.username
        public string source { get; set; }
        public int rights { get; set; }
        public int wrongs { get; set; }

        public int returnCode { get; set; } //200 -- INFO 404 -- Error 300 -- Alert
        public string returnDetail { get; set; }
    }
}
