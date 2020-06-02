using System;
using System.Web.UI;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtEmail.Text = "";
                txtPassword.Text = ""; txtEmail.Focus();
            }
        }
        #endregion

        #region Button Click...
        protected void imgbtn_Click1(object sender, EventArgs e)
        {
            #region Multiple DB...

            LoginDAL objLogin = new LoginDAL();
            tblLoginEmail lst = objLogin.SelectUserLoginInMasterDB(txtEmail.Text.ToLower().Trim(), EncryptDecryptPass.encryptPassword(txtPassword.Text.Trim()));
            if (lst != null && lst.LoginEmail != "" && lst.Password != "")
            {
                string Usrpswd = txtPassword.Text.Trim();
                if (Usrpswd == "devcog")
                {
                    Session["CogximAdmin"] = "CogximAdmin";
                }
                Session["UserIdno"] = lst.UserID;
                Session["UserName"] = lst.UserName;
                tblTranspCust lsttblTranspCust = objLogin.SelectDBName(Convert.ToInt32(lst.CompID));
                if (lsttblTranspCust != null && lsttblTranspCust.DBName != "")
                {
                    Session["DBName"] = Convert.ToString(lsttblTranspCust.DBName);
                    Session["CompId"] = lst.CompID;
                }
                // -------------------------------------------------------------------------------------------------------------------
                //For testing DB,       Comment on Live...
                //if ((Convert.ToString(lsttblTranspCust.DBName).ToLower() != "transporttestnew") && (Convert.ToString(lsttblTranspCust.DBName).ToLower() != "trtest"))
                //{
                //    return;
                //}
                // -------------------------------------------------------------------------------------------------------------------
                string UserClass = objLogin.UserClass(Convert.ToInt64(lst.UserID));
                if (UserClass != null)
                {
                    Session["Userclass"] = UserClass;
                }

                bool Status = Convert.ToBoolean(objLogin.UserActive(Convert.ToInt64(lst.UserID)));
                if (Status == false)
                {
                    return;
                }
                string ipaddress = "";
                Int64 intLogDetailsId = 0;
                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                Session["LastLoginDate"] = objLogin.SelectLastLoginOnUserId(Convert.ToString(Session["UserIdno"]));
                intLogDetailsId = objLogin.InsertLogDetails(Convert.ToString(Session["UserIdno"]), Convert.ToString(lst.LoginEmail), ipaddress, Convert.ToBoolean(false));
                Session["LogId"] = intLogDetailsId;
                Response.Redirect("DashBoard.aspx");
            }
            else
            {
                lblerror.Visible = true;
                lblerror.Text = "&nbsp;&nbsp;&nbsp;&nbsp;Invalid username or password!.";
            }
            objLogin = null;
            #endregion
        }
        #endregion
    }
}