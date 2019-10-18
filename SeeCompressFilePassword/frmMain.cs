//using SharpCompress.Common;
//using SharpCompress.Reader.Rar;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;


using System.Linq;





using SCFP_Compress.Reader.Rar;
using SCFP_Compress.Common;
using SCFP_Compress.Common.Rar.Headers;
using SCFP_Compress.IO;

using SCFP_Compress.Reader;

using SCFP_Compress.Archive;
using SCFP_Compress.Archive.Rar;

using SCFP_Compress.Reader.Zip;
using SCFP_Compress.Writer;

using Models;
using libcomm;

using RestSharp;
using System.Net;


namespace SCFP_Compress.SeeCompressFilePassword
{
    public partial class frmMain : Form
    {
        private CThreadSearch m_objThreadC;

        public static bool Initial_flag;
        private int filecount;//记录搜索到文件的个数； 
        private int filecount_pwd;//记录搜索到有压缩密码的文件的个数； 


        private int filecount_task = 0;//记录任务数个数
        private int filecount_task_pwd = 0;//记录已经分享密码个数
        private int filecount_task_payrewards = 0;//记录已经 扣悬赏 个数

        private bool isTaskMode = true; //是否任务查看模式


        private bool isTaskMode_QuckCommit_FileContains = false;
        private bool isTaskMode_QuckCommit_ColSelect = false;

        private int iTaskMode_CurrentRowIndex_FileContains = 0;

        private int iTaskMode_CurrentRowIndex_ColSelect = 0;


        private const string COL_FullFile = "colFullFile";
        private const string COL_ID_Result = "colID_Result";

        private const string COL_Filemd5_Result = "colFilemd5_Result";
        private const string COL_Filecontent_crc32_Result = "colFilecontent_crc32_Result";
        private const string COL_Uid_Result = "colUid_Result";
        private const string COL_Score = "colScore";

        private const string COL_FileName = "colFileName";
        private const string COL_FilePwd = "colFilePwd";
        private const string COL_FileShareURL_Result = "colFileShareURL_Result";
        private const string COL_Level = "colLevel";
        private const string COL_ShareFrom = "colShareFrom";
        private const string COL_Author = "colAuthor";
        private const string COL_FileisOK = "colFileisOK";
        private const string COL_TruePwd = "colTruePwd";

        private const string COL_Status = "colStatus";
               
        private const string COL_FullFilePath_Task = "colFullFilePath_Task";
        private const string COL_UID_Task = "colUID_Task";
        private const string COL_Author_Task = "colAuthor_Task";
        private const string COL_Source_Task = "colSource_Task";
        private const string COL_Filecontent_crc32 = "col_Filecontent_crc32";
        private const string COL_Filemd5_Task = "colFilemd5_Task";
        private const string COL_Select = "colSelect";
        private const string COL_FileName_Task = "colFileName_Task";
        private const string COL_FilePWD_Task = "colFilePWD_Task";
        private const string COL_FileShareURL_Task = "colFileShareURL_Task";
        private const string COL_Payrewards_score_Task = "colPayrewards_score_Task";
        private const string COL_SendStatus = "colSendStatus";

        

        private FileCheck fileMD5;
        private bool isSave = true;
        public frmMain()
        {
            InitializeComponent();


            m_objThreadC = new CThreadSearch(this, new SearchFileEventHandler(m_objThreadC_SearchEvent));
            Initial_flag = false;
            filecount = 0;
            fileMD5 = new FileCheck();
            //listViewFiles.DoubleClick += new EventHandler(listViewFiles_DoubleClick);
            //this.cmntrv_Path.TreeView.ImageList = this.imglist;


        }
        //事件处理函数
        void m_objThreadC_SearchEvent(CThreadSearch sender, CSearchFileEventArgs args)
        {
            ProcessEvent(lbl_info, args);
        }

        //处理CThreadSearch对象传回来的事件对象
        void ProcessEvent(Label lbl, CSearchFileEventArgs args)
        {
            switch (args.EventType)
            {
                case SearchFileEventTypes.Start:
                    lbl.Text = "开始扫描加密压缩包...";
                    break;
                case SearchFileEventTypes.Stop:
                    lbl.Text = "扫描结束.";
                    btnSeeAllDisk.Visible = true;
                    btnStopNow.Visible = false;
                    break;
                case SearchFileEventTypes.ProcessFile:
                    lbl.Text = "当前扫描位置：" + args.FilePath;
                    break;
                case SearchFileEventTypes.MatchedFile:


                    //if (CheckEncryptedFlag(args.FilePath))
                    //{
                    //    lbl.Text = "搜索到加密压缩包文件：" + args.FilePath;
                    //    //如果匹配则调用增加函 m_lstFiles.Items.Add(args.FilePath);
                    //    this.additemtodgvResult(args.FilePath);

                    //    filecount_pwd = filecount_pwd + 1;
                    //}
                    //else
                    //{
                    //    lbl.Text = "搜索到压缩包文件：" + args.FilePath;

                    //}
                    //string sExt = BaseFunc.GetExtFileNameByString(args.FilePath).ToString().ToLower();
                    //if (sExt == ".rar" || sExt == ".zip")
                    //{
                    //    if(sExt == ".rar") //&& CheckRAREncrypted(args.FilePath))
                    //    {
                    //      lbl.Text = "搜索到加密压缩包文件：" + args.FilePath;
                    //      //如果匹配则调用增加函 m_lstFiles.Items.Add(args.FilePath);
                    //      this.additemtodgvResult(args.FilePath);
                    //      filecount = filecount + 1;
                    //    }

                    //    else if (sExt == ".zip") // && CheckZipEncrypted(args.FilePath))
                    //   {
                    //      lbl.Text = "搜索到加密压缩包文件：" + args.FilePath;
                    //      //如果匹配则调用增加函 m_lstFiles.Items.Add(args.FilePath);
                    //      this.additemtodgvResult(args.FilePath);
                    //      filecount = filecount + 1;
                      
                    //    }
                       
                    //} 

                    lbl.Text = "扫描到加密压缩包文件：" + args.FilePath;
                          //如果匹配则调用增加函 m_lstFiles.Items.Add(args.FilePath);
                          this.additemtodgvResult(args.FilePath);
                          filecount = filecount + 1;
                          this.additemtodgvTasks(args.FilePath);
                          filecount_task = filecount_task + 1;
                    break;

                    
                case SearchFileEventTypes.Error:
                    lbl.Text = "错误：" + args.FilePath;
                    break;
            }
        }



        private void additemtodgvTasks(string filepath)
        {


            lblcount_02.Text = "共扫描到" + (this.filecount_task + 1).ToString() + "个加密压缩包文件";

            FileInfo fsi = new FileInfo(filepath);

            /*
        private const string  = "colFullFilePath_Task";
        private const string  = "colUID_Task";
        private const string  = "colAuthor_Task";
        private const string  = "colSource_Task";
        private const string COL_Filecontent_crc32 = "col_Filecontent_crc32";
        private const string  = "colFilemd5_Task";
        private const string COL_Select = "colSelect";
        private const string  = "colFileName_Task";
        private const string  = "colFilePWD_Task";
        private const string  = "colPayrewards_score_Task";
        private const string COL_SendStatus = "colSendStatus";
            */

            dgvTask.Rows.Add();

            dgvTask[COL_FullFilePath_Task, filecount_task].Value = fsi.FullName;
            dgvTask[COL_FileName_Task, filecount_task].Value = fsi.Name;
            dgvTask[COL_FileName_Task, filecount_task].ToolTipText = fsi.FullName + "\n" + "单击此处打开文件！";// +"MD5:" + fileMD5.MD5File(filepath); 
            dgvTask[COL_FileName_Task, filecount_task].Tag = "Click";
            dgvTask[COL_FilePWD_Task, filecount_task].Value = "";
            dgvTask[COL_FileShareURL_Task, filecount_task].Value = "不知道该文件下载地址";
            dgvTask[COL_FileShareURL_Task, filecount_task].ToolTipText = "你要是知道，可以告诉我啦，输入该压缩包的下载地址！";
            dgvTask[COL_UID_Task, filecount_task].Value = BaseVars.VAR_UID;
            dgvTask[COL_Author_Task, filecount_task].Value = BaseVars.VAR_USERNAME;
            dgvTask[COL_Filemd5_Task, filecount_task].Value = fileMD5.MD5File(filepath);
            dgvTask[COL_Source_Task, filecount_task].Value = "做任务";
            dgvTask[COL_Payrewards_score_Task, filecount_task].Value = 0;
            dgvTask[COL_SendStatus, filecount_task].Value = "";
            dgvTask.Rows[filecount_task].Tag = null;
            /*
            listViewFiles.Items.Add(fsi.Name);
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.Length.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.CreationTime.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.LastWriteTime.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add);*/



        }



        private void additemtodgvResult(string filepath)
        {


            lblcount.Text = "共扫描到" + (this.filecount + 1).ToString() + "个加密压缩包文件";

            FileInfo fsi = new FileInfo(filepath);


            



            dgvResult.Rows.Add();

            dgvResult[COL_FullFile, filecount].Value = fsi.FullName;

            dgvResult[COL_FileName, filecount].Value = fsi.Name;
            dgvResult[COL_FileName, filecount].ToolTipText = fsi.FullName + "\n" + "单击此处打开文件，请记得在密码输入处按 CTRL + V 进行粘贴密码";
            dgvResult[COL_FileName, filecount].Tag = "Click";
            dgvResult[COL_FilePwd, filecount].Value = "";
            dgvResult[COL_FileShareURL_Result, filecount].Value = "";
            dgvResult[COL_Level, filecount].Value = "";
            dgvResult[COL_ShareFrom, filecount].Value = "";

            dgvResult[COL_Author, filecount].Value = "";
            dgvResult[COL_FileisOK, filecount].Value = false;
            dgvResult[COL_TruePwd, filecount].Value = "";

            SCFP_files_send tmpfile = new SCFP_files_send();




            tmpfile.filename = "";
            tmpfile.filepassword = "";
            tmpfile.fileshareurl = "";
            tmpfile.uid = 0;
            tmpfile.username = BaseVars.VAR_USERNAME;
            tmpfile.password = BaseVars.VAR_PASSWORD;
            tmpfile.author = "";
            tmpfile.filemd5 = fileMD5.MD5File(fsi.FullName);
            tmpfile.source = "";
            tmpfile.score = 0;



           
            
            GetdgvResultValue(tmpfile, filecount);

            dgvResult.Rows[filecount].Tag = null;
            /*
            listViewFiles.Items.Add(fsi.Name);
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.Length.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.CreationTime.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add(fsi.LastWriteTime.ToString());
            listViewFiles.Items[listViewFiles.Items.Count - 1].SubItems.Add);*/



        }

        private void GetdgvResultValue(SCFP_files_send myfile,int rowindex)
        {
            try
            {
                var tmpData = myfile;

                tmpData.id =0;

                /*
                 			  'id' => $this->post('id'),
			  'filename' => $this->post('filename'),
			  'filemd5' => $this->post('filemd5'),
			  'filecontent_crc32' => $this->post('filecontent_crc32'),
			  'filepassword' => $this->post('filepassword'),
			  'uid' => $this->post('uid'),
			  'score' => $this->post('score'),
			  'author' => $this->post('author'),
			  'source' => $this->post('source'),
			  'dateline' => $this->post('dateline'),
			  'lasttime' => $this->post('lasttime'),
			  'rights' => $this->post('rights'),
			  'wrongs' => $this->post('wrongs'),			  		  
			  'username' => $this->post('author'),
			  'password' => $this->post('password')		*/


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTRESTSCFPFilesURL_Short, Method.POST);

                request.AddParameter("id", tmpData.id); // adds to POST or URL querystring based on Method
                request.AddParameter("filename", tmpData.filename); // adds to POST or URL querystring based on Method
                request.AddParameter("filemd5", tmpData.filemd5); // adds to POST or URL querystring based on Method
                request.AddParameter("filecontent_crc32", tmpData.filecontent_crc32); // adds to POST or URL querystring based on Method
                request.AddParameter("filepassword", tmpData.filepassword); // adds to POST or URL querystring based on Method
                request.AddParameter("fileshareurl", tmpData.fileshareurl); // adds to POST or URL querystring based on Method
                request.AddParameter("uid", tmpData.uid); // adds to POST or URL querystring based on Method
                request.AddParameter("username", tmpData.username); // adds to POST or URL querystring based on Method
                request.AddParameter("password", tmpData.password); // adds to POST or URL querystring based on Method
                request.AddParameter("score", tmpData.score); // adds to POST or URL querystring based on Method
                request.AddParameter("author", tmpData.author); // adds to POST or URL querystring based on Method
                request.AddParameter("source", tmpData.source); // adds to POST or URL querystring based on Method
                request.AddParameter("rights", tmpData.rights); // adds to POST or URL querystring based on Method
                request.AddParameter("wrongs", tmpData.wrongs); // adds to POST or URL querystring based on Method

               

                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnvalue = SimpleJson.DeserializeObject<SCFP_files>(content);

                    //SimpleJson.DeserializeObject(content, returnuser);
                    if (returnvalue.returnCode == 200)
                    {
                        /*
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BaseVars.VAR_UID = returnuser.uid;
                        BaseVars.VAR_USERNAME = returnuser.username;
                        BaseVars.VAR_EMAIL = returnuser.email;
                        BaseVars.VAR_PASSWORD = user.password;

                                private const string COL_FullFile = "colFullFile";
        private const string COL_ID_Result = "colID_Result";

        private const string COL_Filemd5_Result = "colFilemd5_Result";
        private const string COL_Filecontent_crc32_Result = "colFilecontent_crc32_Result";
        private const string COL_Uid_Result = "colUid_Result";
        private const string COL_Score = "colScore";

        private const string COL_FileName = "colFileName";
        private const string COL_FilePwd = "colFilePwd";
        private const string COL_Level = "colLevel";
        private const string COL_ShareFrom = "colShareFrom";
        private const string COL_Author = "colAuthor";
        private const string COL_FileisOK = "colFileisOK";
        private const string COL_TruePwd = "colTruePwd";*/


                        dgvResult[COL_ID_Result, rowindex].Value = returnvalue.id;
                        dgvResult[COL_Filemd5_Result, rowindex].Value = returnvalue.filemd5;
                        dgvResult[COL_Filecontent_crc32_Result, rowindex].Value = returnvalue.filecontent_crc32;
                        dgvResult[COL_Filemd5_Result, rowindex].Value = returnvalue.filemd5;
                        dgvResult[COL_FilePwd, rowindex].Value = returnvalue.filepassword;
                        dgvResult[COL_FilePwd, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Red.ToString());
                        dgvResult[COL_FileShareURL_Result, rowindex].Value = returnvalue.fileshareurl;
                        dgvResult[COL_Uid_Result, rowindex].Value = returnvalue.uid;
                        dgvResult[COL_Score, rowindex].Value = returnvalue.score;
                        dgvResult[COL_Level, rowindex].Value = "总使用数：" + returnvalue.rights + "报告错误数：" + returnvalue.wrongs; ;//maybe 0,0 returnvalue.wrongs / returnvalue.rights;
                        dgvResult[COL_ShareFrom, rowindex].Value = returnvalue.source;
                        dgvResult[COL_Author, rowindex].Value = returnvalue.author;
                        dgvResult[COL_TruePwd, rowindex].Style.BackColor = Color.FromName(System.Drawing.KnownColor.DarkGray.ToString());
                    }
                    else
                    {
                        //dgvResult[COL_FilePwd, rowindex].Value = returnvalue.returnDetail;
                        dgvResult[COL_Status, rowindex].Value = returnvalue.returnDetail;
                        dgvResult[COL_Status, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        //MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (response != null)
                {
                    dgvResult[COL_FilePwd, rowindex].Value = "查询异常" + string.Format
                   ("Status code is {0} ({1}); response status is {2}",
                     response.StatusCode, response.StatusDescription, response.ResponseStatus);
                   // MessageBox.Show(string.Format
                   // ("Status code is {0} ({1}); response status is {2}",
                   //        response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                dgvResult[COL_FilePwd, rowindex].Value = "提交失败";
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
            }

        }


        private void btnSeeAllDisk_Click(object sender, EventArgs e)
        {
            string sPattern;
            string path;
            sPattern = "";


            //if(rb_see_pwd.Checked)
            //{ 
            //    filecount = 0;
            //    filecount_pwd = 0;

            //    dgvResult.Rows.Clear();

            //}
            //else
            //{

            //    filecount_task = 0;
            //    filecount_task_payrewards = 0;
            //    filecount_task_pwd = 0;
            //    dgvTask.Rows.Clear();                
            //}


            filecount = 0;
            filecount_pwd = 0;

            dgvResult.Rows.Clear();

            filecount_task = 0;
            filecount_task_payrewards = 0;
            filecount_task_pwd = 0;
            dgvTask.Rows.Clear(); 


            sPattern = ".RAR"; //在纯种内部判断///

            path = txtFilePath.Text.Trim();


            if (path.Length <= 3)
            {
                MessageBox.Show("真不好意思，目前是精简版，仅支持对文件夹进行扫描\n因作者的服务器配置不够高，带宽不够大，所以暂时不支持整个盘符扫描！！因为对每一个加密压缩包的查询都需要从作者服务器查询结果！！\n如果您打算资助本软件的发展，请通过手机微信 或 支付宝扫描软件上面的二维码 进行资助！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //MessageBox.Show("提醒",);
                return;
            }

            btnStopNow.Visible = true;
            btnSeeAllDisk.Visible = false;

            if (sPattern.Length <= 0)
            {
                MessageBox.Show("请输入查询符");
                return;
            }

            else
            {
               // path = "C:\\Users\\Happy\\Downloads\\sharpcompress-0.11.1\\TestArchives";
              

                m_objThreadC.StartSearch(path, sPattern);

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvResult.RowCount; i++)
            {
                string sFileName = dgvResult[COL_FullFile, i].Value.ToString();

                string sExt = BaseFunc.GetExtFileNameByString(sFileName).ToString().ToLower();

                if (sExt == ".rar" && BaseFunc.CheckRAREncrypted(sFileName))
                {
                    dgvResult[COL_FilePwd, i].Value = "有密码";
                }
                else if (sExt == ".zip" && BaseFunc.CheckZipEncrypted(sFileName))
                {
                    dgvResult[COL_FilePwd, i].Value = "有密码";
                }
                else
                {
                    dgvResult[COL_FilePwd, i].Value = "无密码";
                }


            }
        }

        private void chkAllowYou_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            //ReadRar("d:\\test.rar", "1234");

            //var test = 

            //if (CheckEncryptedFlag(txtFilePath.Text.ToString().Trim()))
            //CheckZipEncrypted
            //if(BaseFunc.GetExtFileNameByString(txtFilePath.Text.ToString().Trim()) ==".rar")
            //{ 
            //if (BaseFunc.CheckRAREncrypted(txtFilePath.Text.ToString().Trim()))
            //{
            //    // Console.WriteLine("Encrypt");
            //    MessageBox.Show("Encrypt");
            //}
            //else
            //{
            //    //Console.WriteLine("No Encrypt");
            //    MessageBox.Show("No Encrypt");
            //}

            //}

            //if (BaseFunc.GetExtFileNameByString(txtFilePath.Text.ToString().Trim()) == ".zip")
            //{
            //    if (BaseFunc.CheckZipEncrypted(txtFilePath.Text.ToString().Trim()))
            //    {
            //        // Console.WriteLine("Encrypt");
            //        MessageBox.Show("Encrypt");
            //    }
            //    else
            //    {
            //        //Console.WriteLine("No Encrypt");
            //        MessageBox.Show("No Encrypt");
            //    }

            //}
        }

        //private void ReadRar(string FilePath, string password)
        //{

        //    using (Stream stream = File.OpenRead(FilePath))
        //    using (var reader = RarReader.Open(stream, password))
        //    {
        //        while (reader.MoveToNextEntry())
        //        {

        //            if (!reader.Entry.IsDirectory)
        //            {
        //                //Assert.AreEqual(reader.Entry.CompressionType, CompressionType.Rar);
        //                //reader.WriteEntryToDirectory(SCRATCH_FILES_PATH, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
        //            }
        //        }
        //    }

        //}
        //private bool CheckEncrypt(string FilePath)
        //{

        //    using (Stream stream = File.OpenRead(FilePath))
        //    using (var reader = RarReader.Open(stream, Options.LookForHeader))
        //    {
        //        while (reader.MoveToNextEntry())
        //        {
        //            if (reader.Entry.IsEncrypted)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        return false;
        //    }

        //}
        // private string SCRATCH_FILES_PATH = "c:\\tmp";


        //private FileStream GetReaderStream(string FilePath)
        //{
        //    return new FileStream(FilePath,
        //                          FileMode.Open);
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {

        }



        private void txtPwd02_Click(object sender, EventArgs e)
        {
            
        }

        private void txtPwd01_Click(object sender, EventArgs e)
        {
           
        }

        private void txtFileNameContains_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_objThreadC.Stop();
        }

        private void dgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
     
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string sFilePath;
            string sPwd;
            if (this.dgvResult.CurrentCell.Tag != null)
            {
                sFilePath = this.dgvResult[COL_FullFile, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                sPwd = this.dgvResult[COL_FilePwd, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                System.Diagnostics.Process.Start(sFilePath);
                Clipboard.SetDataObject(sPwd);

            }

           
             
        }

        private void tpHome_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            webAD.Navigate (BaseFunc.Get_Global_AppADURL());
            //webVIP.Navigate(BaseFunc.Get_Global_AppVIPTopListURL_Short());
            RefrechScore();
        }
        private void RefrechScore()
        {
            var currentinfo = BaseFunc.GetCurrentInfo(BaseVars.VAR_USERNAME, BaseVars.VAR_PASSWORD);
            lblUserName.Text = BaseVars.VAR_USERNAME;
            lbl_All_score_value.Text = currentinfo.all_score.ToString();
            lbl_getrewards_score.Text = currentinfo.getrewards_score.ToString();
            lbl_scans_score.Text = currentinfo.scans_score.ToString();
            lbl_tasks_score.Text = currentinfo.tasks_score.ToString();
            lbl_payrewards_score.Text = currentinfo.payrewards_score.ToString();
            lbl_pay_score.Text = currentinfo.pay_score.ToString();
            
        }
        private void tbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefrechScore();
        }

        private void rb_see_pwd_CheckedChanged(object sender, EventArgs e)
        {
            isTaskMode = false;
        }

        private void rb_do_task_CheckedChanged(object sender, EventArgs e)
        {
            isTaskMode = true;
        }

        private void dgvTask_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isSave = false;

        }
        private bool CheckCompressFilePWDVaild(string FilePath,string Password)
        {
            if(BaseFunc.GetExtFileNameByString(FilePath)==".rar")
            {
                return BaseFunc.CheckRARPasswordVaild(FilePath, Password);
            }
            else
            {
                return BaseFunc.CheckZipPasswordVaild(FilePath, Password);
            }

        }
        private void dgvTask_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            int rowindex = dgvTask.CurrentCell.RowIndex;

            if(isTaskMode_QuckCommit_FileContains)
            {
                rowindex = iTaskMode_CurrentRowIndex_FileContains;
            }

            if (isTaskMode_QuckCommit_ColSelect)
            {
                rowindex = iTaskMode_CurrentRowIndex_ColSelect;
            }
             
            //isTaskMode_QuckCommit_ColSelect = false;

           


            if(rb_task_mode.Checked)
            {
                SCFP_files_send myfileItem = new SCFP_files_send(); 

                if (this.dgvTask.CurrentCell.OwningColumn.Name.ToString() == COL_FilePWD_Task || this.dgvTask.CurrentCell.OwningColumn.Name.ToString() == COL_FileShareURL_Task)
                {
                    /*
                    sFilePath = this.dgvResult[COL_FullFile, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                    sPwd = this.dgvResult[COL_FilePwd, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                    System.Diagnostics.Process.Start(sFilePath);
                    Clipboard.SetDataObject(sPwd);*/

                    if (dgvTask[COL_FilePWD_Task, rowindex].Value == null || dgvTask[COL_FilePWD_Task, rowindex].Value.ToString() == string.Empty)
                    {
                        //MessageBox.Show("密码分享不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvTask[COL_SendStatus, rowindex].Value = "提交失败，密码分享不能够为空";
                        dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());                        
                        return;
                    }

                    if (dgvTask[COL_FileShareURL_Task, rowindex].Value == null || dgvTask[COL_FileShareURL_Task, rowindex].Value.ToString() == string.Empty)
                    {
                        //MessageBox.Show("文件分享下载地址不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvTask[COL_SendStatus, rowindex].Value = "提交失败，文件分享下载地址不能够为空";
                        dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        return;
                    }

                    if (CheckCompressFilePWDVaild(dgvTask[COL_FullFilePath_Task, rowindex].Value.ToString(), dgvTask[COL_FilePWD_Task, rowindex].Value.ToString()))
                    {
                        //MessageBox.Show("文件分享下载地址不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvTask[COL_SendStatus, rowindex].Value = "提交失败，请分享一个正确的解压密码";
                        dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        return;
                    }




                    myfileItem.filename = dgvTask[COL_FileName_Task, rowindex].Value.ToString();
                    myfileItem.filepassword = dgvTask[COL_FilePWD_Task, rowindex].Value.ToString();
                    myfileItem.fileshareurl = dgvTask[COL_FileShareURL_Task, rowindex].Value.ToString();

                    myfileItem.uid = Convert.ToInt32(dgvTask[COL_UID_Task, rowindex].Value.ToString());
                    myfileItem.author = dgvTask[COL_Author_Task, rowindex].Value.ToString();
                    myfileItem.filemd5 = dgvTask[COL_Filemd5_Task, rowindex].Value.ToString();
                    myfileItem.source = dgvTask[COL_Source_Task, rowindex].Value.ToString();
                    myfileItem.score = Convert.ToInt32(dgvTask[COL_Payrewards_score_Task, rowindex].Value.ToString());
                    myfileItem.username = BaseVars.VAR_USERNAME;
                    myfileItem.password = BaseVars.VAR_PASSWORD;


                    CommitTask(myfileItem, rowindex);

                }

            }
            else
            {
                SCFP_files_reward_send myfileItem = new SCFP_files_reward_send(); 
                 
                if (this.dgvTask.CurrentCell.OwningColumn.Name.ToString() == COL_Payrewards_score_Task || this.dgvTask.CurrentCell.OwningColumn.Name.ToString() == COL_FileShareURL_Task)
                {
                    /*
                    sFilePath = this.dgvResult[COL_FullFile, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                    sPwd = this.dgvResult[COL_FilePwd, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                    System.Diagnostics.Process.Start(sFilePath);
                    Clipboard.SetDataObject(sPwd);*/

                    if (dgvTask[COL_Payrewards_score_Task, rowindex].Value == null || dgvTask[COL_Payrewards_score_Task, rowindex].Value.ToString() == string.Empty)
                    {
                        //MessageBox.Show("文件分享下载地址不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvTask[COL_SendStatus, rowindex].Value = "提交失败，悬赏分数不能够为空";
                        return;
                    }

                    if (Convert.ToInt32(dgvTask[COL_Payrewards_score_Task, rowindex].Value.ToString()) > 0)
                    {
                        //MessageBox.Show("文件分享下载地址不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);




                        if (dgvTask[COL_FileShareURL_Task, rowindex].Value == null || dgvTask[COL_FileShareURL_Task, rowindex].Value.ToString() == string.Empty)
                        {
                            //MessageBox.Show("文件分享下载地址不能够为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvTask[COL_SendStatus, rowindex].Value = "提交失败，下载地址不能够为空";
                            return;

                        }
                        else
                        {
                            if (!BaseFunc.IS_Vaild_WebURL(dgvTask[COL_FileShareURL_Task, rowindex].Value.ToString()))
                            {
                                dgvTask[COL_SendStatus, rowindex].Value = "提交失败，提交一下正确的下载地址让网友帮助你？";
                                return;
                            }
                        }                      


                   

                        myfileItem.filename = dgvTask[COL_FileName_Task, rowindex].Value.ToString();
                        myfileItem.filepassword = dgvTask[COL_FilePWD_Task, rowindex].Value.ToString();
                        myfileItem.fileshareurl = dgvTask[COL_FileShareURL_Task, rowindex].Value.ToString();

                        myfileItem.uid_pay = Convert.ToInt32(dgvTask[COL_UID_Task, rowindex].Value.ToString());
                        myfileItem.author_pay = dgvTask[COL_Author_Task, rowindex].Value.ToString();
                        myfileItem.filemd5 = dgvTask[COL_Filemd5_Task, rowindex].Value.ToString();
                        myfileItem.source = "悬赏任务";
                        myfileItem.score = Convert.ToInt32(dgvTask[COL_Payrewards_score_Task, rowindex].Value.ToString());
                        myfileItem.username = BaseVars.VAR_USERNAME;
                        myfileItem.password = BaseVars.VAR_PASSWORD;


                        CommitReward(myfileItem, rowindex);
                    }

                     //执行提交任务
                }
            }

            

          

           
            isSave = true;
        }

        private void CommitTask(SCFP_files_send myfile,int rowindex)
        {
            try
            {
                var tmpData = myfile;

                tmpData.id = -1;

                /*
                 			  'id' => $this->post('id'),
			  'filename' => $this->post('filename'),
			  'filemd5' => $this->post('filemd5'),
			  'filecontent_crc32' => $this->post('filecontent_crc32'),
			  'filepassword' => $this->post('filepassword'),
			  'uid' => $this->post('uid'),
			  'score' => $this->post('score'),
			  'author' => $this->post('author'),
			  'source' => $this->post('source'),
			  'dateline' => $this->post('dateline'),
			  'lasttime' => $this->post('lasttime'),
			  'rights' => $this->post('rights'),
			  'wrongs' => $this->post('wrongs'),			  		  
			  'username' => $this->post('author'),
			  'password' => $this->post('password')		*/


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTRESTSCFPFilesURL_Short, Method.POST);

                request.AddParameter("id", tmpData.id); // adds to POST or URL querystring based on Method
                request.AddParameter("filename", tmpData.filename); // adds to POST or URL querystring based on Method
                request.AddParameter("filemd5", tmpData.filemd5); // adds to POST or URL querystring based on Method
                request.AddParameter("filecontent_crc32", tmpData.filecontent_crc32); // adds to POST or URL querystring based on Method
                request.AddParameter("filepassword", tmpData.filepassword); // adds to POST or URL querystring based on Method
                request.AddParameter("fileshareurl", tmpData.fileshareurl); // adds to POST or URL querystring based on Method
                request.AddParameter("uid", tmpData.uid); // adds to POST or URL querystring based on Method
                request.AddParameter("username", tmpData.username); // adds to POST or URL querystring based on Method
                request.AddParameter("password", tmpData.password); // adds to POST or URL querystring based on Method
                request.AddParameter("score", tmpData.score); // adds to POST or URL querystring based on Method
                request.AddParameter("author", tmpData.author); // adds to POST or URL querystring based on Method
                request.AddParameter("source", tmpData.source); // adds to POST or URL querystring based on Method
                request.AddParameter("rights", tmpData.rights); // adds to POST or URL querystring based on Method
                request.AddParameter("wrongs", tmpData.wrongs); // adds to POST or URL querystring based on Method

               

                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnvalue = SimpleJson.DeserializeObject<SCFP_files>(content);

                    //SimpleJson.DeserializeObject(content, returnuser);
                    if (returnvalue.returnCode == 200)
                    {
                        /*
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BaseVars.VAR_UID = returnuser.uid;
                        BaseVars.VAR_USERNAME = returnuser.username;
                        BaseVars.VAR_EMAIL = returnuser.email;
                        BaseVars.VAR_PASSWORD = user.password;*/


                        dgvTask[COL_SendStatus, rowindex].Value = returnvalue.returnDetail;
                        dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());  
                    }
                    else
                    {
                        dgvTask[COL_SendStatus, rowindex].Value = returnvalue.returnDetail;
                        dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());  
                        //MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (response != null)
                {
                    dgvTask[COL_SendStatus, rowindex].Value = "提交异常" + string.Format
                   ("Status code is {0} ({1}); response status is {2}",
                     response.StatusCode, response.StatusDescription, response.ResponseStatus);
                    dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());  
                   // MessageBox.Show(string.Format
                   // ("Status code is {0} ({1}); response status is {2}",
                   //        response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                dgvTask[COL_SendStatus, rowindex].Value = "提交失败";
                dgvTask[COL_SendStatus, rowindex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());  
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
            }

        }
       
        private void CommitReward(SCFP_files_reward_send myfile,int rowindex)
        {
            try
            {
                var tmpData = myfile;

                tmpData.id = -1;

                /*
                 			   id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
  fileid int(10) unsigned NOT NULL DEFAULT '0',		# 文件ID
  filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  fileshareurl varchar(400) NOT NULL DEFAULT '',	#文件分享地址
  uid_pay int(10) unsigned NOT NULL DEFAULT '0',		# 已出悬赏分数的用户ID
  uid_pay_me int(10) unsigned NOT NULL DEFAULT '0',		# 抢悬赏分数的用户ID
  score int(10) unsigned NOT NULL DEFAULT '1',		# 悬赏的分数
  author_pay varchar(20) NOT NULL DEFAULT '',		# 悬赏作者
  author_pay_me varchar(20) NOT NULL DEFAULT '',		# 抢悬赏作者
  source varchar(150) NOT NULL DEFAULT '',		# 分享来源
  dateline int(10) unsigned NOT NULL DEFAULT '0',	# 创建时间
  isOK tinyint(1) unsigned NOT NULL default '0', # 是否已经确认	*/


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTSCFP_files_rewardURL_Short, Method.POST);

                request.AddParameter("id", tmpData.id); // adds to POST or URL querystring based on Method
                request.AddParameter("fileid", tmpData.fileid); // adds to POST or URL querystring based on Method
                request.AddParameter("filename", tmpData.filename); // adds to POST or URL querystring based on Method
                request.AddParameter("filemd5", tmpData.filemd5); // adds to POST or URL querystring based on Method
                request.AddParameter("filecontent_crc32", tmpData.filecontent_crc32); // adds to POST or URL querystring based on Method
                request.AddParameter("filepassword", tmpData.filepassword); // adds to POST or URL querystring based on Method
                request.AddParameter("fileshareurl", tmpData.fileshareurl); // adds to POST or URL querystring based on Method
                request.AddParameter("uid_pay", tmpData.uid_pay); // adds to POST or URL querystring based on Method
                request.AddParameter("uid_pay_me", tmpData.uid_pay_me); // adds to POST or URL querystring based on Method
                request.AddParameter("score", tmpData.score); // adds to POST or URL querystring based on Method
                request.AddParameter("author_pay", tmpData.author_pay); // adds to POST or URL querystring based on Method
                request.AddParameter("author_pay_me", tmpData.author_pay_me); // adds to POST or URL querystring based on Method
                request.AddParameter("source", tmpData.source); // adds to POST or URL querystring based on Method
                request.AddParameter("username", tmpData.username); // adds to POST or URL querystring based on Method
                request.AddParameter("password", tmpData.password); // adds to POST or URL querystring based on Method




                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnvalue = SimpleJson.DeserializeObject<SCFP_files_reward>(content);

                    //SimpleJson.DeserializeObject(content, returnuser);
                    if (returnvalue.returnCode == 200)
                    {
                        /*
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BaseVars.VAR_UID = returnuser.uid;
                        BaseVars.VAR_USERNAME = returnuser.username;
                        BaseVars.VAR_EMAIL = returnuser.email;
                        BaseVars.VAR_PASSWORD = user.password;*/


                        dgvTask[COL_SendStatus, rowindex].Value = returnvalue.returnDetail;
                    }
                    else
                    {
                        dgvTask[COL_SendStatus, rowindex].Value = returnvalue.returnDetail;
                        //MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (response != null)
                {
                    dgvTask[COL_SendStatus, rowindex].Value = "提交异常" + string.Format
                   ("Status code is {0} ({1}); response status is {2}",
                     response.StatusCode, response.StatusDescription, response.ResponseStatus);
                    // MessageBox.Show(string.Format
                    // ("Status code is {0} ({1}); response status is {2}",
                    //        response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                dgvTask[COL_SendStatus, rowindex].Value = "提交失败";
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
            }

        }

        private void Commitfiles_OtherPwd(SCFP_files_OtherPwd_send myfile, int rowindex)
        {
            try
            {
                var tmpData = myfile;

                tmpData.id = -1;

                /*
                 			  id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
                 *  fileid int(10) unsigned NOT NULL DEFAULT '0',		# 文件ID
  filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  fileshareurl varchar(400) NOT NULL DEFAULT '',	#文件分享地址 
  uid int(10) unsigned NOT NULL DEFAULT '0',		# 用户ID
  author varchar(20) NOT NULL DEFAULT '',		# 分享作者
  dateline int(10) unsigned NOT NULL DEFAULT '0',	# 创建时间
                 * */


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTSCFP_files_OtherPwdsURL_Short, Method.POST);

                request.AddParameter("id", tmpData.id); // adds to POST or URL querystring based on Method
                request.AddParameter("fileid", tmpData.fileid); // adds to POST or URL querystring based on Method
                request.AddParameter("filename", tmpData.filename); // adds to POST or URL querystring based on Method
                request.AddParameter("filemd5", tmpData.filemd5); // adds to POST or URL querystring based on Method
                request.AddParameter("filecontent_crc32", tmpData.filecontent_crc32); // adds to POST or URL querystring based on Method
                request.AddParameter("filepassword", tmpData.filepassword); // adds to POST or URL querystring based on Method
                request.AddParameter("fileshareurl", tmpData.fileshareurl); // adds to POST or URL querystring based on Method
                request.AddParameter("uid", tmpData.uid); // adds to POST or URL querystring based on Method
                request.AddParameter("author", tmpData.author); // adds to POST or URL querystring based on Method
                request.AddParameter("username", tmpData.username); // adds to POST or URL querystring based on Method
                request.AddParameter("password", tmpData.password); // adds to POST or URL querystring based on Method




                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnvalue = SimpleJson.DeserializeObject<SCFP_files_OtherPwd>(content);

                    //SimpleJson.DeserializeObject(content, returnuser);
                    if (returnvalue.returnCode == 200)
                    {
                        /*
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BaseVars.VAR_UID = returnuser.uid;
                        BaseVars.VAR_USERNAME = returnuser.username;
                        BaseVars.VAR_EMAIL = returnuser.email;
                        BaseVars.VAR_PASSWORD = user.password;*/


                        dgvResult[COL_Status, rowindex].Value = returnvalue.returnDetail;
                        dgvResult[COL_Status, dgvResult.CurrentCell.RowIndex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                    }
                    else
                    {
                        dgvResult[COL_Status, rowindex].Value = returnvalue.returnDetail;
                        dgvResult[COL_Status, dgvResult.CurrentCell.RowIndex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                        //MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (response != null)
                {
                    dgvResult[COL_Status, rowindex].Value = "提交异常" + string.Format
                   ("Status code is {0} ({1}); response status is {2}",
                     response.StatusCode, response.StatusDescription, response.ResponseStatus);
                    dgvResult[COL_Status, dgvResult.CurrentCell.RowIndex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                    // MessageBox.Show(string.Format
                    // ("Status code is {0} ({1}); response status is {2}",
                    //        response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                dgvResult[COL_Status, rowindex].Value = "提交失败";
                dgvResult[COL_Status, dgvResult.CurrentCell.RowIndex].Style.ForeColor = Color.FromName(System.Drawing.KnownColor.Green.ToString());
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
            }

        }

        private void dgvResult_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            isSave = false;
        }

        private void dgvResult_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SCFP_files commitfile = new SCFP_files();
            int rowindex = dgvResult.CurrentCell.RowIndex;

            if (this.dgvResult.CurrentCell.OwningColumn.Name.ToString() == COL_FileisOK && dgvResult[COL_ID_Result, rowindex].Value != null)
            {
                bool isChecked = Convert.ToBoolean(this.dgvResult[COL_FileisOK, this.dgvResult.CurrentCell.RowIndex].Value);
                if (isChecked)
                {
                    this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].Style.BackColor = Color.FromName(System.Drawing.KnownColor.Window.ToString());
                    this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].Style.BackColor = Color.FromName(System.Drawing.KnownColor.DarkGray.ToString());
                    this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].ReadOnly = true;
                }
            }


            if (this.dgvResult.CurrentCell.OwningColumn.Name.ToString() == COL_TruePwd && dgvResult[COL_ID_Result, rowindex].Value !=null)
            {
                /*
                  id int(10) unsigned NOT NULL AUTO_INCREMENT,		# 序号ID
  fileid int(10) unsigned NOT NULL DEFAULT '0',		# 文件ID
  filename varchar(255) NOT NULL DEFAULT '',			# 文件名称
  filemd5 varchar(60) NOT NULL DEFAULT '',			# 文件MD5
  filecontent_crc32 varchar(4000) NOT NULL DEFAULT '',			# 压缩文件内部文件crc32值，通过逗号分隔
  filepassword varchar(255) NOT NULL DEFAULT '',			# 文件密码
  fileshareurl varchar(400) NOT NULL DEFAULT '',	#文件分享地址
  uid int(10) unsigned NOT NULL DEFAULT '0',		# 用户ID
  author varchar(20) NOT NULL DEFAULT '',		# 分享作者*/

                SCFP_files_OtherPwd_send myfileItem = new SCFP_files_OtherPwd_send();

                myfileItem.fileid = Convert.ToInt32(dgvResult[COL_ID_Result, rowindex].Value.ToString());
                myfileItem.filename = dgvResult[COL_FileName, rowindex].Value.ToString();
                myfileItem.filemd5 = dgvResult[COL_Filemd5_Result, rowindex].Value.ToString();
                myfileItem.filecontent_crc32 = dgvResult[COL_Filecontent_crc32_Result, rowindex].Value.ToString();
                myfileItem.filepassword = dgvResult[COL_TruePwd, rowindex].Value.ToString();
                myfileItem.fileshareurl = dgvResult[COL_FileShareURL_Result, rowindex].Value.ToString();

                myfileItem.uid = Convert.ToInt32(dgvResult[COL_Uid_Result, rowindex].Value.ToString());
                myfileItem.author = dgvResult[COL_Author, rowindex].Value.ToString();

                myfileItem.username = BaseVars.VAR_USERNAME;
                myfileItem.password = BaseVars.VAR_PASSWORD;


                Commitfiles_OtherPwd(myfileItem, rowindex);

               // CommitOtherPwds(, rowindex);

            }

 
            isSave = true;
        }

        private void rb_task_mode_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_task_mode.Checked)
            {
                gp_task.Visible = true;
                gp_reward.Visible = false;
                colPayrewards_score_Task.Visible = false;
                colFilePWD_Task.Visible = true;
            }
        }

        private void rb_reward_mode_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_reward_mode.Checked)
            {
                gp_reward.Visible = true;
                gp_task.Visible = false;
                colPayrewards_score_Task.Visible = true;
                colFilePWD_Task.Visible = false;
            }
        }

        private void rb_Condi_Select_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Condi_Select.Checked)
            {
                txtFileNameContains.BackColor = Color.FromName(System.Drawing.KnownColor.DarkGray.ToString());
                txtFileNameContains.ReadOnly = true;

            }
        }

        private void rb_Condi_FileNameContains_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_Condi_FileNameContains.Checked)
            {
                txtFileNameContains.BackColor = Color.FromName(System.Drawing.KnownColor.Window.ToString());

                txtFileNameContains.ReadOnly = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dgvTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string sFilePath;
            string sPwd;
            if (this.dgvTask.CurrentCell.Tag != null)
            {
                sFilePath = this.dgvTask[COL_FullFilePath_Task, this.dgvTask.CurrentCell.RowIndex].Value.ToString();
                //sPwd = this.dgvResult[COL_FilePwd, this.dgvResult.CurrentCell.RowIndex].Value.ToString();
                System.Diagnostics.Process.Start(sFilePath);
                //Clipboard.SetDataObject(sPwd);

            }
        }

        private void btnQuickCommit_Click(object sender, EventArgs e)
        {
            if(rb_Condi_Select.Checked)
            {
                dgvTask[COL_FilePWD_Task, 0].Selected = true;

                if(dgvTask.Rows.Count == 0)
                {
                    MessageBox.Show("老兄，列表中都没内容，你要提交什么呀？？你欺负我没学好数学呀！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                for (int iSelect = 0; iSelect < dgvTask.Rows.Count; iSelect++)
                {
                    bool isChecked = Convert.ToBoolean(dgvTask[COL_Select, iSelect].Value);
                    if (isChecked)
                    {
                        
                        dgvTask[COL_FilePWD_Task, iSelect].Value = txtPwd_Task.Text.ToString();
                        //dgvTask.CurrentCell.OwningColumn.Name = COL_FilePWD_Task;
                        //dgvTask[COL_SendStatus, iSelect].Selected = true;
                        //
                        //this.dgvTask[COL_Select, 0].Selected = true;

                        //isTaskMode_QuckCommit_FileContains = true;
                        isTaskMode_QuckCommit_ColSelect = true;

                        iTaskMode_CurrentRowIndex_ColSelect = iSelect;

                        dgvTask_CellEndEdit(null, null);

                        //System.Threading.Thread.Sleep(2000);

                        //--------------记得关闭状态
                        //isTaskMode_QuckCommit_FileContains = false;
                        isTaskMode_QuckCommit_ColSelect = false;

                        iTaskMode_CurrentRowIndex_ColSelect = 0;


                        //this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].Style.BackColor = Color.FromName(System.Drawing.KnownColor.Window.ToString());
                        //this.dgvResult[COL_TruePwd, this.dgvResult.CurrentCell.RowIndex].ReadOnly = false;
                    }
                  
                }
            }
            else
            {

            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://happyqq.cn/");
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://weibo.com/happyqq");
        }

        private void btnStopNow_Click(object sender, EventArgs e)
        {
            btnStopNow.Visible = false;
            btnSeeAllDisk.Visible = true;
            m_objThreadC.Stop();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
             FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txtFilePath.Text = foldPath;
            }
        
        }

        private void lnkPayMoney_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("请通过微信 或者 支付宝 扫软件上方的二维码，谢谢！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }






    }

    public class CThreadSearch
    {
        private bool m_bStart = false;
        private Queue<string> m_queFolders;
        private Regex m_objRegex;
        private Form m_objForm;
        private Delegate m_objInvoke;

        private object locker = new object();



        public CThreadSearch(Form frm, Delegate objInvoke)
        {
            m_objForm = frm;
            m_objInvoke = objInvoke;
            m_queFolders = new Queue<string>();


        }
        //启动查找
        public void StartSearch(string sRootFolder, string sFindPattern)
        {
            m_queFolders.Clear();
            m_queFolders.Enqueue(sRootFolder);
            m_objRegex = new Regex(sFindPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //创建用来查找的线程
            Thread objThread = new Thread(new ThreadStart(fnThread));
            objThread.IsBackground = true;
            //设置启动标志
            m_bStart = true;
            //启动线程
            objThread.Start();
        }
        //通知对象，停止查找
        public void Stop()
        {
            m_bStart = false;

        }
        private void doCheckEncrypt(string FilePath)
        {
            
            lock (locker)
            {
                bool isPassword = false;

                string sExt = BaseFunc.GetExtFileNameByString(FilePath);

                if (sExt == ".rar" && BaseFunc.CheckRAREncrypted(FilePath))
                {
                    isPassword = true;
                }

                else if (sExt == ".zip" && BaseFunc.CheckZipEncrypted(FilePath))
                {
                    isPassword = true;

                }
                


                if (isPassword)
                {
                    OnEvent(FilePath, SearchFileEventTypes.MatchedFile);
                }
                else
                {
                    OnEvent(FilePath, SearchFileEventTypes.ProcessFile);
                }

            }
        }


        //查找线程
        private void fnThread()
        {
            //检查参数
            if (m_queFolders == null || m_objRegex == null) return;
            //事件通知，查找开始
            OnEvent("", SearchFileEventTypes.Start);
            while (m_bStart && m_queFolders.Count > 0)
            {
                string sFolder = m_queFolders.Dequeue();
                try
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sFolder);
                    OnEvent(sFolder, SearchFileEventTypes.ProcessFile);
                    //子目录，加入队列
                    DirectoryInfo[] arrSubDirs = dir.GetDirectories();
                    for (int i = 0; i < arrSubDirs.Length; i++)
                    {
                        m_queFolders.Enqueue(arrSubDirs[i].FullName);
                        if (!m_bStart) break;
                    }
                    //检查是否需要终止
                    if (!m_bStart) break;
                    //检查文件是否匹配
                    FileInfo[] arrFiles = dir.GetFiles();
                    for (int i = 0; i < arrFiles.Length; i++)
                    {
                        //if (m_objRegex.IsMatch(arrFiles[i].Extension.ToUpper()) )
                        if (arrFiles[i].Extension.ToUpper() == ".ZIP" || arrFiles[i].Extension.ToUpper() == ".RAR" || arrFiles[i].Extension.ToUpper() == ".7Z")
                        {
                            //找到匹配的文件
                            //这个地方加入压缩包密码判断语句

                            doCheckEncrypt(arrFiles[i].FullName);

                            //if (CheckEncrypt(arrFiles[i].FullName))
                            //{
                            //    OnEvent(arrFiles[i].FullName, SearchFileEventTypes.MatchedFile);
                            //}
                            //else
                            //{
                            //    OnEvent(arrFiles[i].FullName, SearchFileEventTypes.ProcessFile);
                            //}

                            //OnEvent(arrFiles[i].FullName, SearchFileEventTypes.MatchedFile);

                            //if (CheckEncryptedFlag(arrFiles[i].FullName))
                            //{
                            //    OnEvent(arrFiles[i].FullName, SearchFileEventTypes.MatchedFile);
                            //}
                            //else
                            //{
                            //    //不匹配， 只是通知处理过该文件
                            //    OnEvent(arrFiles[i].FullName, SearchFileEventTypes.ProcessFile);
                            //}
                        }
                        else
                        {
                            //不匹配， 只是通知处理过该文件
                            OnEvent(arrFiles[i].FullName, SearchFileEventTypes.ProcessFile);
                        }
                        if (!m_bStart) break;
                    }
                    if (!m_bStart) break;
                }
                catch (Exception)
                {
                    //发生错误
                    OnEvent(sFolder, SearchFileEventTypes.Error);
                }
            }
            //事件通知，查找结束
            OnEvent("", SearchFileEventTypes.Stop);
        }
        //用来触发事件
        private void OnEvent(string sFilePath, SearchFileEventTypes eType)
        {
            if (m_objForm != null && m_objInvoke != null)
            {
                CSearchFileEventArgs args = new CSearchFileEventArgs(eType, sFilePath);
                m_objForm.Invoke(m_objInvoke, new object[] { this, args });
            }
        }
    }
    //用来定义事件函数的委托
    public delegate void SearchFileEventHandler(CThreadSearch sender, CSearchFileEventArgs args);
    //事件的类型
    public enum SearchFileEventTypes
    {
        Start,          //开始查找
        ProcessFile,    //检查指定的文件
        MatchedFile,    //找到匹配的文件
        Stop,          //查找结束
        Error           //发生错误
    }
    //事件参数
    public class CSearchFileEventArgs : EventArgs
    {
        string m_sFilePath;
        SearchFileEventTypes m_eType;
        public string FilePath
        {
            get { return m_sFilePath; }
        }
        public SearchFileEventTypes EventType
        {
            get { return m_eType; }
        }
        public CSearchFileEventArgs(SearchFileEventTypes eType, string sFile)
        {
            m_eType = eType;
            m_sFilePath = sFile;
        }
    }
}
