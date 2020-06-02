using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Transactions;
namespace WebTransport
{
    public partial class ManageTyreIssue : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dGrossAmnt = 0, dNetAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.Print == false)
                //{
                //    imgBtnExcel.Visible = false;
                //}
                //this.BindState();
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                this.BindLocation();
                this.BindTruckNo();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            Int64 location = Convert.ToInt32(ddlLocation.SelectedValue);
            Int64 truckno = Convert.ToInt32(ddlTruckNo.SelectedValue);
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            var lstGridData = obj.SelectMatrial(dtfrom, dtto, location, truckno, yearidno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = false;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): 0 ";
                imgBtnExcel.Visible = false;
            }
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }
        private void BindLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var to = obj.BindLocFrom();
            ddlLocation.DataSource = to;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTruckNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = hidmaxdate.Value;
                }
            }
        }
        ///// <summary>
        ///// To Bind State DropDown
        ///// </summary>
        //private void BindState()
        //{
        //    CityMastDAL objclsCityMaster = new CityMastDAL();
        //    var objCityMast = objclsCityMaster.SelectState();
        //    objclsCityMaster = null;
        //    drpState.DataSource = objCityMast;
        //    drpState.DataTextField = "State_Name";
        //    drpState.DataValueField = "State_Idno";
        //    drpState.DataBind();
        //    drpState.Items.Insert(0, new ListItem("< Choose State >", "0"));
        //}
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("TyreIssue.aspx?TyreIssue=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    TyreIssueDAL obj = new TyreIssueDAL();
                    Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument));
                    obj = null;
                    if (intValue > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record deleted successfully.";
                        Tran.Complete();
                        //txtGRNo.Focus();
                    }
                    else
                    {
                        if (intValue == -1)
                            strMsg = "Record can not be deleted. It is in use.";
                        else
                            strMsg = "Record not deleted.";
                        Tran.Dispose();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
        }
        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {

                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");


            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblGridNo = (Label)e.Row.FindControl("lblGridNo");
                Int32 intGRIdno = Convert.ToInt32(lblGridNo.Text);
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                


            }
        }
        #endregion

        #region Button Events...
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.BindGrid();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ImgBtnLocation_Click(object sender, ImageClickEventArgs e)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var to = obj.BindLocFrom();
            ddlLocation.DataSource = to;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        protected void ImgBtnTruckRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        #endregion

        #region control events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        #endregion
    }
}