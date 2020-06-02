using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebTransport.Classes;


namespace WebTransport
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        #region Private Variables...
        public string strUserName = string.Empty;
        public string strSITE_URL = ConfigurationManager.AppSettings["strSITE_URL"];
        public int UserRgt_Idno;
        public int Form_Idno;
        public bool ADD;
        public bool Edit;
        public bool View;
        public bool Delete;
        public bool Print;
        public int LocId;
        #endregion

        #region Page Load Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                strUserName = Convert.ToString(Session["UserName"]);
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            if (!Page.IsPostBack)
            {
                //if (CheckUserRights(4) == false)
                //{
                //    masters.Visible = false;
                //}
                //if (CheckUserRights(5) == false)
                //{
                //    showrooomEntry.Visible = false;
                //}
                ////if (CheckUserRights(6) == false)
                ////{
                ////    workshopEntry.Visible = false;
                ////}
                //if (CheckUserRights(7) == false)
                //{
                //    goodlife.Visible = false;
                //}
                //if (CheckUserRights(77) == false)
                //{
                //    Accounts.Visible = false;
                //}
                //if (CheckUserRights(8) == false)
                //{
                //    showroomrep.Visible = false;
                //}
                ////if (CheckUserRights(80) == false)
                ////{
                ////    Othrs.Visible = false;
                ////}
                ////if (CheckUserRights(79) == false)
                ////{
                ////    Reminder.Visible = false;
                ////}
                //if (CheckUserRights(90) == false)
                //{
                //    utility.Visible = false;
                //}
                ////if (CheckUserRights(9) == false)
                ////{
                ////    workshoprep.Visible = false;
                ////}
                ////if (CheckUserRights(89) == false)
                ////{
                ////    finrep.Visible = false;
                ////}
                
                
                ////lblDatabaseName.Text = Convert.ToString(Session["DBName"]);
                ////if (string.IsNullOrEmpty(lblDatabaseName.Text))
                ////{
                ////    Response.Redirect("Login.aspx", false);
                ////}
                ////strUserName = Convert.ToString(Session["UserName"]);
                ////if (string.IsNullOrEmpty(strUserName))
                ////{
                ////    Response.Redirect("Login.aspx", false);
                ////}
            }
        }

        public bool CheckUserRights(int intFormIdno)
        {
            bool bvalue = false;
            try
            {
            //    //AutomobileOnline.Model.clsLoginDAL objLoginDAL = new AutomobileOnline.Model.clsLoginDAL();
            //    //AutomobileOnline.Model.UserRight objUserRghts = objLoginDAL.SelectUserRights(Convert.ToInt32(Session["UserIdno"]), intFormIdno);
            //    //UserRgt_Idno = Convert.ToInt32(objUserRghts.UserRgt_Idno);
            //    Form_Idno = Convert.ToInt32(objUserRghts.Form_Idno);
            //    ADD = Convert.ToBoolean(objUserRghts.ADD);
            //    Edit = Convert.ToBoolean(objUserRghts.Edit);
            //    View = Convert.ToBoolean(objUserRghts.View);
            //    Delete = Convert.ToBoolean(objUserRghts.Delete);
            //    Print = Convert.ToBoolean(objUserRghts.Print);
            //    if (ADD == false && Edit == false && View == false && Delete == false && Print == false)
            //    {
            //        bvalue = false;
            //    }
            //    else
            //    {
            //        bvalue = true;
            //    }
            }
            catch (Exception Ex)
            {
            }
            return bvalue;
        }
        #endregion

        #region Buttons Events...
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session["AutoIdno"] = string.Empty;
            Session["userclass"] = string.Empty;
            Session.Remove("UserIdno");
            Session.Remove("UserName");
            Session.Remove("DBName");   
            Session.Abandon();
            Response.Redirect("Login.aspx", false);
        }
        #endregion
    }
}