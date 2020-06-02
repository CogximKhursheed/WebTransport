using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using WebTransport.DAL;

namespace WebTransport.Controls
{
    public partial class HeaderControl : System.Web.UI.UserControl
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginDAL objLogin = new LoginDAL();
            EmployeeMasterDAL objEmployeeMasterDAL = new EmployeeMasterDAL();
            lblCompName.Text = objLogin.SelectCompanyName();


            lblDatabaseName.Text = "DB: " + Convert.ToString(Session["DBName"]);
            var objEmpMaster = objEmployeeMasterDAL.SelectById(Convert.ToInt64(Session["UserIdno"]));
            imgEmp.ImageUrl = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Photo"))) ? "~/img/placeholder.png" : "~" + Convert.ToString(DataBinder.Eval(objEmpMaster[0], "Photo")).Trim();

            objEmpMaster = null;

            if (Session["LastLoginDate"] != "" && Session["LastLoginDate"] != null)
            {
                lblLastLoginDate.Visible = true;
                lblLastLoginDate.Text = "Last Login : " + Convert.ToDateTime(Session["LastLoginDate"].ToString()).ToString();
            }
            else
            {
                lblLastLoginDate.Visible = true;
                lblLastLoginDate.Text = "First time Login";
            }

            if (string.IsNullOrEmpty(lblDatabaseName.Text))
            {
                Response.Redirect("Login.aspx", false);
            }
            lblusername.Text = Convert.ToString(Session["UserName"]);

            if (string.IsNullOrEmpty(lblusername.Text))
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        #endregion
    }
}