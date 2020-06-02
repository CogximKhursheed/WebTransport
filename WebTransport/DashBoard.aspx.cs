using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != "" && Session["UserName"] != null)
            {
                string previousPageName = string.Empty;
                string previousPageUrl = string.Empty;
                if (Request.UrlReferrer != null)
                {
                    previousPageUrl = Request.UrlReferrer.AbsoluteUri;
                    previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);
                }

                string strSql = "";
                DataSet ds = new DataSet();
                strSql = "Exec [spLogin] @Action='SelectRecordForCaption',@Emp_Idno='" + Convert.ToString(Session["UserIdno"]) + "'";
                ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, strSql);
                if (ds.Tables[0].Rows.Count > 1)
                {
                    lblWelcomeCaption.Text = "WELCOME BACK ";
                }
                else
                {
                    lblWelcomeCaption.Text = "WELCOME ";

                }
                lblUserName.Text = Session["UserName"].ToString();
                ds = null;

                if (previousPageName == "Login.aspx")
                {
                    previousPageUrl = string.Empty;
                    previousPageName = string.Empty;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                }
                BindLorryStatusGrid();
                BindGridChallanNotConfirmed();
                GetOutstandingRepPartyWise();
            }
        }

        private void BindGridChallanNotConfirmed()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ds = obj.GetGrChlnNotConfirm(ApplicationFunction.ConnectionString());
            if (ds != null)
            {
                grdGrListChlnNotConfirm.DataSource = ds;
                grdGrListChlnNotConfirm.DataBind();
            }
        }

        private void GetOutstandingRepPartyWise()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ds = obj.GetOutstandingRepPartyWise(ApplicationFunction.ConnectionString());
            if (ds != null)
            {
                grdOutstandingRepPartyWise.DataSource = ds;
                grdOutstandingRepPartyWise.DataBind();
            }
        }

        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "closeModal();", true);
        }
        private void BindLorryStatusGrid()
        {
            LorryMasterDAL objDash = new LorryMasterDAL();
            DataSet ds = objDash.SelectLorryDetails(ApplicationFunction.ConnectionString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdMain.DataSource = ds;
                grdMain.DataBind();
                HideDiv.Visible = false;
                Upload_div.Visible = true;
            }
            else
            {
                HideDiv.Visible = true;
                Upload_div.Visible = false;
            }
            
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("DueDateLorryReport.aspx?LorryDt=" + e.CommandArgument, true);
            }
          
        }
    }
}