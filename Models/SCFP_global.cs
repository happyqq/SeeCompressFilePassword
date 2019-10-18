using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
  //id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
  //Global_Key varchar(255) NOT NULL DEFAULT '',			# 全局Key
  //Global_Value varchar(255) NOT NULL DEFAULT '',			# 全局值
  //Global_Action varchar(255) NOT NULL DEFAULT '',			# 全局动作 'INFO','ERROR','ABORT','EXIT'
  //Global_Action_Msg varchar(255) NOT NULL DEFAULT '',    # 对应动作的提示语
  //AutoDirectURL varchar(255) NOT NULL DEFAULT '',  
    public class SCFP_global
    {
        public int id { get; set; }
        public string Global_Key { get; set; }
        public string Global_Value { get; set; }
        public string Global_Action { get; set; }
        public string Global_Action_Msg { get; set; }
        public string AutoDirectURL { get; set; }

        public int returnCode { get; set; } //200 -- INFO 404 -- Error 300 -- Alert
        public string returnDetail { get; set; }
    }
}
