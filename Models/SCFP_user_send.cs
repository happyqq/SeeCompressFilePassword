using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{

  //   uid int(10) unsigned NOT NULL AUTO_INCREMENT,		# 用户ID
  //username char(16) NOT NULL DEFAULT '',		# 用户名
  //password char(32) NOT NULL DEFAULT '',		# 密码	md5(md5() + salt)
  //salt char(16) NOT NULL DEFAULT '',			# 随机干扰字符，用来混淆密码
  //AppSecret char(32) NOT NULL DEFAULT '',			# 随机32位字符
  //AppKey char(32) NOT NULL DEFAULT '',			# 以上随机32位字符 	md5(md5() + salt)
  //groupid smallint(5) unsigned NOT NULL DEFAULT '0',	# 用户组
  //email char(40) NOT NULL DEFAULT '',			# EMAIL
  //regip int(10) NOT NULL DEFAULT '0',			# 注册IP
  //regdate int(10) unsigned NOT NULL DEFAULT '0',	# 注册日期
  //loginip int(10) NOT NULL DEFAULT '0',			# 登陆IP
  //logindate int(10) unsigned NOT NULL DEFAULT '0',	# 登陆日期
  //lastip int(10) NOT NULL DEFAULT '0',			# 上次登陆IP
  //lastdate int(10) unsigned NOT NULL DEFAULT '0',	# 上次登陆日期
  //all_score int(10) unsigned NOT NULL DEFAULT '0', # 总分数 = 充值分数 + 抢悬赏分数 + 任务分数 - 扫描所花分数 - 扣悬赏数分数
  //pay_score int(10) unsigned NOT NULL DEFAULT '0', # 充值分数
  //scans int(10) unsigned NOT NULL DEFAULT '0',	# 扫描个数
  //scans_score int(10) unsigned NOT NULL DEFAULT '0', # 扫描所花分数
  //tasks int(10) unsigned NOT NULL DEFAULT '0',	# 任务数
  //tasks_score int(10) unsigned NOT NULL DEFAULT '0', # 任务分数
  //getrewards int(10) unsigned NOT NULL DEFAULT '0',	# 抢悬赏数
  //getrewards_score int(10) unsigned NOT NULL DEFAULT '0',	# 抢悬赏分数
  //payrewards int(10) unsigned NOT NULL DEFAULT '0',		# 扣悬赏数
  //payrewards_score int(10) unsigned NOT NULL DEFAULT '0',		# 扣悬赏数分数
    public class SCFP_user_send
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int returnCode { get; set; } //200 -- INFO 404 -- Error 300 -- Alert
        public string returnDetail { get; set; }
      
    }
}
