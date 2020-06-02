using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class ManageVchrEntry : Pagebase
    {
        #region Private Variable...
        ////string conString = ConfigurationManager.ConnectionStrings["AutomobileConnectionString"].ToString();
        string conString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
        static FinYear UFinYear = new FinYear();
        private int intFormId = 49;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            UFinYear = base.FatchFinYear(1);
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                txtdatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtdateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                //  this.BindCompany();
                this.BindLedger();
                this.Countall();
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid Events...
        Double drLblNet = 0;
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("VchrEntry.aspx?VchrIdno=" + Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                VchrEntryDAL objVchrEntry = new VchrEntryDAL();
                int value = objVchrEntry.Deletefile(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                if (value > 0)
                {
                    this.BindGrid();
                    string strMsg = "Record deleted successfully.";
                    SuccessMessage(strMsg);
                }
                objVchrEntry = null;
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton imgBtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                drLblNet += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Amnt"));
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Delete == false)
                {
                    imgBtnDelete.Visible = false;
                }
                if (base.Edit == false)
                {
                    imgBtnDelete.Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalNet = (Label)e.Row.FindControl("lblTotalNet");
                lblTotalNet.Text = drLblNet.ToString("N2");
            }
        }
        #endregion

        #region Functions...
        public void Countall()
        {
            VchrEntryDAL obj = new VchrEntryDAL();
            Int64 count = obj.countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
        }
        private void BindGrid()
        {
            VchrEntryDAL objclsVchrEntry = new VchrEntryDAL();
            DataTable dsTable = objclsVchrEntry.spVoucherEntrySearch(ApplicationFunction.ConnectionString(), "All", txtdatefrom.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlVoucherType.SelectedValue),
                                                                    (txtVchrNo.Text.Trim() == "" ? 0 : Convert.ToInt32(txtVchrNo.Text.Trim())),// Convert.ToInt64(drpCompany.SelectedValue),
                                                              Convert.ToInt64(ddlLedgrName.SelectedValue), Convert.ToString(txtAmnt.Text.Trim()), Convert.ToInt32(ddlDateRange.SelectedValue), base.UserIdno);//, base.CompId);

            if (dsTable.Rows.Count > 0)
            {
                grdMain.DataSource = dsTable;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + dsTable.Rows.Count;

                Double TotalNetAmount = 0;

                for (int i = 0; i < dsTable.Rows.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(dsTable.Rows[i]["Tot_Amnt"]);
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dsTable.Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;

            }
        }

        //private void BindCompany()
        //{
        //    BindDropdownDAL CompanyDAL = new BindDropdownDAL();
        //    Int32 ALL = 1;
        //    if (base.UserIdno == 1)
        //    {
        //        ALL = 1;
        //    }
        //    else
        //    {
        //        ALL = Convert.ToInt32(base.CompId);
        //    }
        //    var lst = CompanyDAL.SelectCompany(ALL);
        //    CompanyDAL = null;
        //    drpCompany.DataSource = lst;
        //    drpCompany.DataTextField = "Name";
        //    drpCompany.DataValueField = "CompMast_Idno";
        //    drpCompany.DataBind();
        //    if (base.UserIdno == 1)
        //    {
        //        drpCompany.Items.Insert(0, new ListItem("< Choose Company >", "0"));
        //    }
        //}

        public void BindLedger()
        {
            VchrEntryDAL objVchrEntryDAL = new VchrEntryDAL();
            var lst = objVchrEntryDAL.SelectAcntName(); //Convert.ToInt64(drpCompany.SelectedValue));

            ddlLedgrName.DataSource = lst;
            ddlLedgrName.DataTextField = "ACNT_NAME";
            ddlLedgrName.DataValueField = "Acnt_Idno";
            ddlLedgrName.DataBind();
            objVchrEntryDAL = null;
            ddlLedgrName.Items.Insert(0, new ListItem("--Select Ledger--", "0"));
        }


        public void SuccessMessage(string strMsg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }
        #endregion

        #region Control Events...
        protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindLedger();
        }
        #endregion

        #region Date Range FinYear ...

        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));

            if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
            {
                txtdatefrom.Text = hidmindate.Value;
                txtdateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
            else
            {
                txtdatefrom.Text = hidmindate.Value;
                txtdateto.Text = hidmaxdate.Value;
            }
        }
        #endregion
    }
}