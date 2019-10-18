using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCFP_Compress.SeeCompressFilePassword
{
    public static class BaseConstants
    {

#if DEBUG
        /// <summary>
        /// RESTBaseURL
        /// </summary>
        public const string RESTBaseURL = "http://localhost/index.php/";
        /// <summary>
        /// RESTBaseURL Format
        /// </summary>
        public const string RESTBaseURL_Format = "http://localhost/index.php/{0}";
#else

        public const string RESTBaseURL = "http://webapi_compress.happyqq.cn/";
         /// <summary>
        /// RESTBaseURL Format
        /// </summary>
        public const string RESTBaseURL_Format = "http://webapi_compress.happyqq.cn/{0}";
#endif
        /// <summary>
        /// RESTRegister URL
        /// </summary>

        public const string RESTRegisterURL = RESTBaseURL + "register/users";
        public const string RESTRegisterURL_Short = "register/users";


        /// <summary>
        /// RESTSCFPFiles URL
        /// </summary>

        public const string RESTRESTSCFPFilesURL = RESTBaseURL + "scfp/files";
        public const string RESTRESTSCFPFilesURL_Short = "scfp/files";


         /// <summary>
        /// RESTSCFP_files_OtherPwds URL
        /// </summary>

        public const string RESTSCFP_files_OtherPwdsURL = RESTBaseURL + "scfp/files_OtherPwds";
        public const string RESTSCFP_files_OtherPwdsURL_Short = "scfp/files_OtherPwds";


        
        /// <summary>
        /// RESTSCFP_files_reward URL
        /// </summary>

        public const string RESTSCFP_files_rewardURL = RESTBaseURL + "scfp/files_reward";
        public const string RESTSCFP_files_rewardURL_Short = "scfp/files_reward";



        /// <summary>
        /// RESTSCFP_files_reward URL
        /// </summary>

        public const string RESTSCFP_globalURL = RESTBaseURL + "scfp/global";
        public const string RESTSCFP_globalURL_Short = "scfp/global";




        /// <summary>
        /// RESTuser URL
        /// </summary>

        public const string RESTSCFP_userURL = RESTBaseURL + "scfp/user";
        public const string RESTSCFP_userURL_Short = "scfp/user";



        /// <summary>
        /// RESTRESTSCFP_user_findpwd_ URL
        /// </summary>

        public const string RESTSCFP_user_findpwdURL = RESTBaseURL + "scfp/user_findpwd";
        public const string RESTSCFP_user_findpwdURL_Short = "scfp/user_findpwd";



        /// <summary>
        /// RESTSCFP_Global_userMinScoreURL
        /// </summary>

        public const string RESTSCFP_Global_userMinScoreURL = RESTBaseURL + "globalOption/userMinScore";
        public const string RESTSCFP_Global_userMinScoreURL_Short = "globalOption/userMinScore";



        /// <summary>
        /// RESTSCFP_Global_userDefaultTaskScoreURL
        /// </summary>

        public const string RESTSCFP_Global_userDefaultTaskScoreURL = RESTBaseURL + "globalOption/userDefaultTaskScore";
        public const string RESTSCFP_Global_userDefaultTaskScoreURL_Short = "globalOption/userDefaultTaskScore";




        /// <summary>
        /// RESTSCFP_Global_userDefaultTaskScoreCostURL
        /// </summary>

        public const string RESTSCFP_Global_userDefaultTaskScoreCostURL = RESTBaseURL + "globalOption/userDefaultTaskScoreCost";
        public const string RESTSCFP_Global_userDefaultTaskScoreCostURL_Short = "globalOption/userDefaultTaskScoreCost";


        /// <summary>
        /// RESTSCFP_Global_AppVersionURL
        /// </summary>

        public const string RESTSCFP_Global_AppVersionURL = RESTBaseURL + "globalOption/appVersion";
        public const string RESTSCFP_Global_AppVersionURL_Short = "globalOption/appVersion";

         /// <summary>
        /// RESTSCFP_Global_AppADURL
        /// </summary>

        public const string RESTSCFP_Global_AppADURL = RESTBaseURL + "globalOption/appADURL";
        public const string RESTSCFP_Global_AppADURL_Short = "globalOption/appADURL";


        /// <summary>
        /// RESTSCFP_Global_AppADURLURL
        /// </summary>

        public const string RESTSCFP_Global_AppVIPTopListURL = RESTBaseURL + "globalOption/appVIPTopListURL";
        public const string RESTSCFP_Global_AppVIPTopListURL_Short = "globalOption/appVIPTopListURL";



        

        /// <summary>
        /// User Agent HTTP Header
        /// </summary>
        public const string UserAgentHeader = "User-Agent";

        /// <summary>
        /// Content Type HTTP Header
        /// </summary>
        public const string ContentTypeHeader = "Content-Type";

        /// <summary>
        /// Application - Json Content Type
        /// </summary>
        public const string ContentTypeHeaderJson = "application/json";

        /// <summary>
        /// Application - Form URL Encoded Content Type
        /// </summary>
        public const string ContentTypeHeaderFormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Authorization HTTP Header
        /// </summary>
        public const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// SCFP Request Id HTTP Header
        /// </summary>
        public const string SCFPRequestHeader = "SeeCompressFilePassword";

        /// <summary>
        /// The version
        /// </summary>
        public const string Version = "1.0";

        /// <summary>
        /// The name
        /// </summary>
        public const string AppName = "SeeCompressFilePassword";

    }
        /// <summary>
        /// GlobalAction
        /// </summary>
        public static class GlobalAction
        {
            /// <summary>
            /// GlobalAction INFO
            /// </summary>
            public const string INFO = "INFO";

            /// <summary>
            /// GlobalAction ERROR
            /// </summary>
            public const string ERROR = "ERROR";

            /// <summary>
            /// GlobalAction ABORT
            /// </summary>
            public const string ABORT = "ABORT";

            /// <summary>
            /// GlobalAction EXIT
            /// </summary>
            public const string EXIT = "EXIT";

            

    }
}
