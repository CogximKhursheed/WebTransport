using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using System.Data;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class ClaimToCompList : System.Web.UI.Page
    {

        #region Private Variable....
        private int intFormId = 0;

        BindDropdownDAL obj;
        FinYearDAL objFinYearDAL;
        ClaimToDAL objClaimToDAL;
        DataTable dt = new DataTable();
        DataTable CSVTable = new DataTable();

        bool st;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindDateRange();
                this.BindPartyName();
                this.BindCompany();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange.SelectedIndex = 0; 
                ddlDateRange_SelectedIndexChanged(null, null);
                //this.Countall();
                
            }
        }

        #region Functions...

        private void BindGrid()
        {
            objClaimToDAL = new ClaimToDAL();
            string ClaimNo = string.IsNullOrEmpty(Convert.ToString(txtClaimNo.Text)) ? "" : Convert.ToString(txtClaimNo.Text);
            Int64 YearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlPartyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue);
            Int64 CompanyIdno = string.IsNullOrEmpty(Convert.ToString(ddlCompName.SelectedValue)) ? 0 : Convert.ToInt64(ddlCompName.SelectedValue);
            Int64 LocationFrom = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            if (string.IsNullOrEmpty(Convert.ToString(txtDateFrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }

            var lstGridData = objClaimToDAL.SearchList(dtfrom, dtto, ClaimNo, PartyIdno, LocationFrom, CompanyIdno, YearIdno);

            objClaimToDAL = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void BindPartyName()
        {
            obj = new BindDropdownDAL();
            DataTable dtParty = new DataTable();
            dtParty = obj.BindPartyForSale(ApplicationFunction.ConnectionString());
            obj = null;
            ddlPartyName.DataSource = dtParty;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

        }

        private void BindCompany()
        {
            obj = new BindDropdownDAL();
            var CompanyName = obj.SelectCompanyName();
            obj = null;
            if (CompanyName != null)
            {
                ddlCompName.DataSource = CompanyName;
                ddlCompName.DataTextField = "Acnt_Name";
                ddlCompName.DataValueField = "Acnt_Idno";
                ddlCompName.DataBind();
            }
            ddlCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));
        }

        private void BindCity()
        {
            obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = ToCity;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            obj = null;
            if (FrmCity.Count > 0)
            {
                ddlLocation.DataSource = FrmCity;
                ddlLocation.DataTextField = "City_Name";
                ddlLocation.DataValueField = "City_Idno";
                ddlLocation.DataBind();
            }
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }

        private void BindDateRange()
        {
            objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            objFinYearDAL = null;
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
            }

        }


        #endregion

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("ClaimToComp.aspx?ClaimHeadIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                objClaimToDAL = new ClaimToDAL();
                long intValue = objClaimToDAL.Delete(Convert.ToInt32(e.CommandArgument));
                objClaimToDAL = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use!";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                Int64 intClaimToComHeadIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ClaimToComHead_Idno"));

                if (intClaimToComHeadIdno > 0)
                {
                    ClaimToDAL obj = new ClaimToDAL();
                    var ClaimExist = obj.Exists(Convert.ToInt64(intClaimToComHeadIdno));
                    obj = null;
                    if (ClaimExist != null && ClaimExist.Count>0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }

            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}