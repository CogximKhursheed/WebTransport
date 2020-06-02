using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DBName"] == null || Session["UserName"] == null)
            {
                Response.Redirect("LogOut.aspx");
            }

            BindDropdownDAL objPopmsg = new BindDropdownDAL();
            tblPopUpMessage msgobj = objPopmsg.CheckSiteoffMessage();
            if (Convert.ToBoolean(msgobj.PopUp_Bit))
            { divMessagePopMsg.Visible = true; divMessagePopMsg.InnerText = msgobj.PupUp_Message.ToString(); }
            else
            { divMessagePopMsg.Visible = false; }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}