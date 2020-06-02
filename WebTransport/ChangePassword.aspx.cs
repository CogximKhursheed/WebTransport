using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace WebTransport
{
    public partial class ChangePassword : Pagebase
    {
        private int intFormId = 52;
        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            //if (base.CheckUserRights(intFormId) == false)
            //{
            //    Response.Redirect("PermissionDenied.aspx");
            //}
            //if (base.View == false)
            //{
            //    lnkbtnSave.Visible = true;
            //}

            if (!IsPostBack)
            {
                Session["RD"] = "1";
                if (Convert.ToInt64((Session["LastPwdChngDays"] == null || Session["LastPwdChngDays"].ToString() == "") ? "0" : Session["LastPwdChngDays"].ToString()) > 30)
                    trmsg.Visible = true;
                else
                    trmsg.Visible = false;
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Session["RD"] = "0";
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (txtOldpaswrd.Text != "" || txtNewPasswrd.Text != "" || txtCnfrmpaswrd.Text != "")
            {
                DataSet ds = new DataSet();
                string strSql = "";
                strSql = "Exec [spLogin] @Action='SelectUser',@Emp_Idno=" + Convert.ToString(Session["UserIdno"]) + "";
                ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    WebTransport.Classes.EncryptDecryptPass obj = new Classes.EncryptDecryptPass();
                    string OldPassTxt = WebTransport.Classes.EncryptDecryptPass.encryptPassword(txtOldpaswrd.Text).ToUpper().ToString();
                    if (Convert.ToString(ds.Tables[0].Rows[0]["User_Password"]).ToUpper() != OldPassTxt)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please Enter Correct Old Password!')", true);
                        txtOldpaswrd.Text = string.Empty;
                        txtNewPasswrd.Text = string.Empty;
                        txtOldpaswrd.Focus();
                        return;
                    }
                    else if (txtNewPasswrd.Text != txtCnfrmpaswrd.Text)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('New password and confirm Password not match.')", true);
                        txtNewPasswrd.Text = txtCnfrmpaswrd.Text = "";
                        txtNewPasswrd.Focus();
                        return;
                    }
                    else if (txtNewPasswrd.Text == "" || txtCnfrmpaswrd.Text == "")
                    {
                        if (txtNewPasswrd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please Enter New Password!')", true);
                            txtNewPasswrd.Focus();
                            return;
                        }
                        else if (txtCnfrmpaswrd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please Enter Confirm Password!')", true);
                            txtCnfrmpaswrd.Focus();
                            return;
                        }
                    }

                    string strSQL = "Exec [spLogin] @Action='UpdatePass', @User_Password='" + WebTransport.Classes.EncryptDecryptPass.encryptPassword(Convert.ToString(txtNewPasswrd.Text)) + "', @Emp_Idno='" + Convert.ToString(Session["UserIdno"]) + "'";
                    if (Convert.ToBoolean(SqlHelper.ExecuteNonQuery(ApplicationFunction.ConnectionString(), CommandType.Text, strSQL)) == true)
                    {
                        msg = "Your password has been successfully changed";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                        txtOldpaswrd.Text = txtNewPasswrd.Text = txtCnfrmpaswrd.Text = "";
                        return;

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Old Password dosn't match')", true);
                    txtOldpaswrd.Text = "";
                    return;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Please Fill Password Details!')", true);
                return;
            }

        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            txtOldpaswrd.Text = txtNewPasswrd.Text = txtCnfrmpaswrd.Text = "";
        }
        #endregion
    }
}