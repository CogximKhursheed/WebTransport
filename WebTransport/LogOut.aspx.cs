using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class LogOut : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DBName"] != null && Session["UserName"] != null)
            {
                if (Session["LogId"].ToString() != "" && Session["LogId"] != null)
                {
                    LoginDAL objLogin = new LoginDAL();
                    objLogin.UpdateLogDetails(Convert.ToInt64(Session["LogId"].ToString()), Convert.ToBoolean(true));
                    Session["LogId"] = null;
                    Session["LastLoginDate"] = null;
                }
            }
            Session["UserIdno"] = null;
            Session["DBName"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Response.Redirect("Login.aspx");
        }
        #endregion
    }
}