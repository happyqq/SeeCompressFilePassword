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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmRegister myfrm = new frmRegister(this);
            myfrm.Show();
        }
        private bool isVaild()
        {
            lblError.Visible = true;
            lblError.Text = "";

            if (txtUserID.Text.Trim() == string.Empty)
            {
                lblError.Text = "请输入用户名";
                txtUserID.Focus();
                return false;
            }


            if (txtPwd.Text.Trim() == string.Empty)
            {
                lblError.Text = "请输入密码";
                txtPwd.Focus();
                return false;
            }

            lblError.Visible = false;
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new SCFP_user_send();

                if (!isVaild())
                    return;

                user.id = -1;
                user.username = txtUserID.Text.Trim();
                user.email = "";
                user.password = txtPwd.Text.Trim();


                var client = new RestClient(BaseConstants.RESTBaseURL);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(BaseConstants.RESTSCFP_userURL_Short, Method.POST);

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
                        BaseVars.VAR_UID = returnuser.uid;
                        BaseVars.VAR_USERNAME = returnuser.username;
                        BaseVars.VAR_EMAIL = returnuser.email;
                        BaseVars.VAR_PASSWORD = user.password;


                        this.Hide();
                        frmMain myfrm = new frmMain();
                        myfrm.Show();
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

        private void lnkForgetPWD_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmForgePWD myfrm = new frmForgePWD(this);
            myfrm.Show();
        }
    }
}
