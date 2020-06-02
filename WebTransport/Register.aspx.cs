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
    public partial class Register : System.Web.UI.Page
    {
        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMainSuccessMsg.Visible = false;
            if (!Page.IsPostBack)
            {
                multivwRegstr.ActiveViewIndex = 0;
                txtMobileNumber.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtOwnerName.Focus();
            }
        }
        #endregion

        #region Button Events...

        protected void validateLoc(object source, ServerValidateEventArgs args)
        {
            if (!chkTermsCondi.Checked)
            {
                return;
            }
            else
            {
                //your code
            }
        }

        protected void lnkbtnContinoue_OnClick(object sender, EventArgs e)
        {
            if (ViewState["OTP"] != null)
            {
                string OTP = ViewState["OTP"].ToString();
                if (OTP == txtMobileOtp.Text.Trim())
                {
                    string strMsg = string.Empty;

                    RegisterDAL objtblRegister = new RegisterDAL();
                    Int64 intWorkCompIdno = 0;
                    string CompName = txtCompanyName.Text.Trim();
                    string OwnerName = txtOwnerName.Text.Trim();
                    string LastName = txtOwnerLast.Text.Trim();
                    //string Address1 = txtAdrs1.Text.Trim();
                    //string Address2 = txtAdrs2.Text.Trim();
                    //Int64 CityIdno = Convert.ToInt64(ddlCity.SelectedValue);
                    //Int64 StateIdno = Convert.ToInt64(ddlState.SelectedValue);
                    //string PinNo = Convert.ToString(txtpincode.Text.Trim());
                    string EmailId = txtemailid.Text.Trim();
                    string PhneNum = txtMobileNumber.Text.Trim();
                    string OwnMobileNum = txtMobileNumber.Text.Trim();

                    string Ipaddress = Request.UserHostAddress.ToString();

                    intWorkCompIdno = objtblRegister.Insert(CompName, OwnerName, LastName, EmailId, PhneNum, OwnMobileNum, Ipaddress, false);

                    if (intWorkCompIdno > 0)
                    {
                        #region ClientSMS & SMStoSir
                        string Clientsms = "Dear " + OwnerName + ", thank you for regsitering with us. We have received your request and we shall email you the login details very shortly.";

                        if (SendMsgForOTP(OwnMobileNum, Clientsms))
                        {
                            // sending sms to client
                        }

                        string Message = "Dear Sir, " + OwnerName + ", [" + OwnMobileNum + "], has submitted registration request for Web Transport.";
                        string MobNum = "9829068896"; ;  // here num to send sms 
                        if (SendMsgForOTP(MobNum, Message))
                        {
                            // sending sms to punitsir
                        }
                        #endregion

                        #region MailToClient & MailToAdmin
                        string Subject = "Registration";
                        string EmailBody = string.Empty;
                        tblEmailTemplate EmailTemplates = objtblRegister.SelectUserMailTemplates("Register");
                        EmailBody = EmailTemplates.Body.ToString();
                        EmailBody = EmailBody.Replace("$$UserName$$", OwnerName);

                        if (SendMailMessage(txtemailid.Text.Trim(), "", "", Subject, EmailBody))
                        {
                            // Sending mail to user
                        }

                        string AdminEmailBody = string.Empty;
                        tblEmailTemplate AdminstrSQLForTmplts = objtblRegister.SelectUserMailTemplates("Mail To Admin");
                        AdminEmailBody = AdminstrSQLForTmplts.Body.ToString();
                        AdminEmailBody = AdminEmailBody.Replace("$$UserName$$", OwnerName).Replace("$$MobileNum$$", OwnMobileNum).ToString();
                        string MailTo = "puneet.chopra@cogxim.com"; // to add more mailid use ; keyword
                        if (SendMailMessage(MailTo, "", "", Subject, AdminEmailBody))
                        {
                            // Sending mail to Punit sir
                        }
                        #endregion
                        multivwRegstr.ActiveViewIndex = 0;
                        ClearControls();
                        lblMainSuccessMsg.Visible = true;
                        lblMainSuccessMsg.Text = "Thank you for registration with us, we will revert back you soon.";
                    }
                    else
                    {
                        multivwRegstr.ActiveViewIndex = 0;
                        lblMainSuccessMsg.Visible = true;
                        lblMainSuccessMsg.Text = "Record already exists.";
                    }
                }
                else
                {
                    lblSuccessMsg.Visible = true;
                    lblSuccessMsg.ForeColor = System.Drawing.Color.Red;
                    lblSuccessMsg.Text = "OTP code does not match";

                    lnkbtnOTPSend.Visible = true;
                }
            }
            else
            {
                Response.Redirect("NewRegister.aspx");
            }
        }

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (chkTermsCondi.Checked)
            {
                lnkbtnOTPSend.Visible = false;
                txtMobileOtp.Text = "";
                RegisterDAL obj = new RegisterDAL();
                if (obj.IsExists(txtemailid.Text.Trim()))
                {
                    lblMainSuccessMsg.Visible = true;
                    lblMainSuccessMsg.Text = "User Email id already exists, Please select another Email id";
                }
                else
                {
                    Random generator = new Random();
                    Int64 OTP = generator.Next(100000, 999999);
                    ViewState["OTP"] = OTP;
                    lblSuccessMsg.ForeColor = System.Drawing.Color.Green;
                    string Message = "Dear Customer,Your One Time Password is " + OTP + ".Please enter OTP to proceed.Thank you,Team COGXIM";

                    string Subject = "OTP Varification.";
                    string EmailBody = string.Empty;
                    string OwnerName = txtOwnerName.Text.Trim();
                    RegisterDAL objtblRegister = new RegisterDAL();
                    tblEmailTemplate EmailTemplates = objtblRegister.SelectUserMailTemplates("OTP");
                    EmailBody = EmailTemplates.Body.ToString();
                    EmailBody = EmailBody.Replace("$$UserName$$", OwnerName);
                    EmailBody = EmailBody.Replace("$$OTP$$", ViewState["OTP"].ToString());

                    if (SendMailMessage(txtemailid.Text.Trim(), "", "", Subject, EmailBody))
                    {
                        // Sending mail to user
                    }

                    if (SendMsgForOTP(txtMobileNumber.Text.Trim(), Message))
                    {
                        multivwRegstr.ActiveViewIndex = 1;
                        lblSuccessMsg.Visible = true;
                        lblSuccessMsg.Text = "Your OTP is send to your mobile number and Email also, Please enter OTP to proceed complete registration";
                    }
                    else
                    {
                        lblSuccessMsg.Text = "Fetal error!, Please contact to support.";
                    }
                }
            }
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("NewRegister.aspx");
        }

        #endregion

        #region Miscellaneous Events...

        private void ShowMessageSuccess(string strMsg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }


        private void ClearControls()
        {
            txtCompanyName.Text = string.Empty;
            txtOwnerName.Text = string.Empty;
            txtOwnerLast.Text = string.Empty;
            txtemailid.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;
            txtLoginPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            chkTermsCondi.Checked = false;

            txtOwnerName.Focus();
        }

        public bool SendMailMessage(string to, string bcc, string cc, string subject, string body)
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

        public static bool SendMsgForOTP(string Mobile, string msg)
        {
            try
            {
                WebClient objWebClient;
                string sBaseURL;
                Stream objStreamData;
                StreamReader objReader;
                string sResult;
                objWebClient = new WebClient();
                sBaseURL = "http://globesms.in/sendhttp.php?user=cogximsms&password=teamcogximsms&authkey=32AQUm4rxkUM56540740&type=1&mobiles=91" + Mobile + "&message= " + HttpUtility.UrlEncode(msg) + "&sender=COGXIM&route=4";
                objStreamData = objWebClient.OpenRead(sBaseURL);
                objReader = new StreamReader(objStreamData);
                sResult = objReader.ReadToEnd();
                objStreamData.Close();
                objReader.Close();

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