using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class ForgotPasswrd : System.Web.UI.Page
    {
        string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["QryRestPswd"] != null && Request["uid"] != null && Request["usid"] != null)
                {
                    string UID = Request["uid"].ToString();
                    string USID = Request["usid"].ToString();
                    string QryRestPswd = Request["QryRestPswd"].ToString();

                    if (UID != "" && USID != "" && QryRestPswd != "")
                    {
                        ViewState["UID"] = UID.ToString();
                        ViewState["USID"] = USID.ToString();

                        ForgotPasswordDAL obj = new ForgotPasswordDAL();
                        DataTable dt = obj.CheckUserForgotLink(strConnection, UID, USID, QryRestPswd);
                        if (dt.Rows.Count > 0)
                        {
                            string Message = dt.Rows[0]["MSG"].ToString();
                            if (dt.Rows[0]["BIT"].ToString() == "1")
                            {
                                multiResetPassword.ActiveViewIndex = 1;
                            }

                            if (Message != "")
                            {
                                lblMessage.Visible = true;
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = Message.ToString();
                            }
                        }
                    }
                }
                else
                {
                    multiResetPassword.ActiveViewIndex = 0;
                    txtEmail.Focus();
                }
            }
        }
        #endregion

        #region Button Events..
        protected void imgbtn_Click1(object sender, EventArgs e)
        {
            ForgotPasswordDAL obj = new ForgotPasswordDAL();

            DataTable dt = obj.SelectUserForgotPassword(strConnection, txtEmail.Text.Trim());
            if (dt.Rows.Count > 0 && txtEmail.Text.Trim() == dt.Rows[0]["Email"].ToString())
            {
                string body = "<a href='" + "http://transportnew.cogxim.com/ForgotPasswrd.aspx?QryRestPswd=" + dt.Rows[0]["RandomNum"].ToString() + "&uid=" + dt.Rows[0]["ID"].ToString() + "&usid=" + dt.Rows[0]["UID"].ToString() + "' target='_blank'>Click here to reset password</a>";

                string EmailBody = string.Empty;//
                tblEmailTemplate EmailTemplates = obj.SelectUserMailTemplates("ForgotPassword");
                EmailBody = EmailTemplates.Body.ToString();
                EmailBody = EmailBody.Replace("$$UrlLink$$", body);
                string FromMail = System.Configuration.ConfigurationManager.AppSettings["FROMEMAIL"].ToString();
                if (SendEMailMsg(txtEmail.Text.Trim(), "", "", "Forgot Password", EmailBody))
                {
                    txtEmail.Text = "";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "A confirm link send to your mail id, Please check your mailid.";
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Mail ID does not exist";
            }
        }

        protected void btnResetPassword_OnClick(object sender, EventArgs e)
        {
            ForgotPasswordDAL obj = new ForgotPasswordDAL();
            string password = WebTransport.Classes.EncryptDecryptPass.encryptPassword(txtPassword.Text.Trim());

            DataTable dt = obj.UpdateUserPassword(strConnection, ViewState["UID"].ToString(), ViewState["USID"].ToString(), password);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BIT"].ToString() == "1")
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    multiResetPassword.ActiveViewIndex = 0;

                    lblMessage.Text = dt.Rows[0]["MSG"].ToString();
                }
            }
        }
        #endregion

        #region Functions...

        public bool SendEMailMsg(string to, string bcc, string cc, string subject, string body)
        {
            MailMessage mMailMessage = new MailMessage();
            mMailMessage.From = new MailAddress("info@cogxim.com");

            if ((to != null) && (to != string.Empty))
            {
                string[] strMails = to.Split(';');
                if (strMails.Length > 0)
                {
                    for (int counter = 0; counter <= strMails.Length - 1; counter++)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.To.Add(strMails[counter]);
                    }
                }
            }
            if ((bcc != null) && (bcc != string.Empty))
            {
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }
            if ((cc != null) && (cc != string.Empty))
            {
                string[] strMails = cc.Split(';');
                if (strMails.Length > 0)
                {
                    // Looping
                    for (int counter = 0; counter <= strMails.Length - 1; counter++) // Setting CC
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(strMails[counter])) == false)
                            mMailMessage.CC.Add(strMails[counter]);
                    }
                }
            }
            mMailMessage.Subject = subject;
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            mMailMessage.Priority = MailPriority.Normal;
            SmtpClient SmtpServer = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["FROMEMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["FROMPWD"].ToString());
            try
            {
                SmtpServer.Send(mMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}