using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Models;
using RestSharp;

using System.Net;

namespace SCFP_Compress.SeeCompressFilePassword
{
    public partial class frmForgePWD : Form
    {
        private Form frmfrom = null;
        public frmForgePWD(Form frmfrom)
        {
            InitializeComponent();
            this.frmfrom = frmfrom;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
        
            this.Close();
            if (this.frmfrom != null)
            {
                //恢复A
                this.frmfrom.Visible = true;
            }
        }

        private bool isVaild()
        {
            lblError.Visible = true;
            lblError.Text = "";

            if (txtUserID.Text.Length < 6)
            {
                lblError.Text = "用户名不能够少于6个字符";
                txtUserID.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == string.Empty)
            {
                lblError.Text = "邮箱不能够为空，请重新输入";
                txtEmail.Focus();
                return false;
            }

            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (!r.IsMatch(txtEmail.Text.Trim()))
            {
                lblError.Text = "邮箱格式错误，请重新输入";
                txtEmail.Focus();
                return false;
            } 

            lblError.Visible = false;
            return true;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new SCFP_user_send();

                if (!isVaild())
                    return;

                user.id = -1;
                user.username = txtUserID.Text.Trim();
                user.email = txtEmail.Text.Trim();
                user.password = "";


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTSCFP_user_findpwdURL_Short, Method.POST);

                request.AddParameter("username", user.username); // adds to POST or URL querystring based on Method
                request.AddParameter("email", user.email); // adds to POST or URL querystring based on Method
                request.AddParameter("password", user.password); // adds to POST or URL querystring based on Method

                IRestResponse response = client.Execute(request);


                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
          (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {


                    //var returnuser = new SCFP_user_send();
                    var content = response.Content; // raw content as string
                    var returnuser = SimpleJson.DeserializeObject<SCFP_user_recv>(content);

                    //SimpleJson.DeserializeObject(content, returnuser);
                    if(returnuser.returnCode == 200)
                    { 
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Hide();
                        //frmMain myfrm = new frmMain();
                        //myfrm.Show();
                    }
                    else
                    {
                        MessageBox.Show(returnuser.returnDetail, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (response != null)
                {
                    MessageBox.Show(string.Format
                    ("Status code is {0} ({1}); response status is {2}",
                           response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
            }
        


        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void frmForgePWD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.frmfrom != null)
            {
                //恢复A
                this.frmfrom.Visible = true;
            }
        }
    }
}
