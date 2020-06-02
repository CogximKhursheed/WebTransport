using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Net;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class LowRateMaster : Pagebase
    {
        #region Private Variable...
        int intFormId = 57;
        int cOMPiD;
        #endregion

        #region Page Events...
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
                //if (base.ADD == false)
                //{
                //    lnkbtnSave.Visible = false;
                //}
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
               // cOMPiD = Convert.ToInt32(Session["CompId"].ToString());
                this.BindDateRange();
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindLedger();
                this.BindGrid();
                
            }
        }
        #endregion

        #region Bind DropDown...

        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }
        private void BindLedger()
        {
            LowRateMastDAL objMastBLL = new LowRateMastDAL();
            var lst = objMastBLL.SelectPartyName();
            objMastBLL = null;
            drpPrtyName.DataSource = lst;
            drpPrtyName.DataTextField = "Acnt_Name";
            drpPrtyName.DataValueField = "Acnt_Idno";
            drpPrtyName.DataBind();
            drpPrtyName.Items.Insert(0, new ListItem("--Select Party--", "0"));
        }
        private void BindPanNo()
        {
            LowRateMastDAL objMastBLL = new LowRateMastDAL();
            var lst = objMastBLL.SelectPANNoList(Convert.ToInt64(drpPrtyName.SelectedValue),ApplicationFunction.ConnectionString());
            objMastBLL = null;
            drpPanNo.DataSource = lst;
            drpPanNo.DataTextField = "Pan_No";
            //drpPanNo.DataValueField=""
            drpPanNo.DataBind();
            drpPanNo.Items.Insert(0, new ListItem("--Select PAN--", "0"));
        }

        #endregion

        #region Functions...
         
        private void Populate(int TaxMastID)
        {
            LowRateMastDAL objRateMast = new LowRateMastDAL();
            tblLowRateMaster RateMast = objRateMast.SelectTaxByID(TaxMastID);
            objRateMast = null;
            if (RateMast != null)
            {
                txtDate.Text = Convert.ToString(Convert.ToDateTime(RateMast.Date).ToString("dd-MM-yyyy"));
                drpPrtyName.SelectedValue = Convert.ToString(RateMast.Acnt_Idno);
                drpPrtyName_SelectedIndexChanged(null,null); 
                //this.BindPanNo();
                //drpPanNo. = RateMast.PAN.ToString();
                drpPanNo.Items.FindByText(RateMast.PAN.ToString()).Selected = true;
                taxRate.Text = Convert.ToString(RateMast.Tax_Rate);
                hidTaxid.Value = RateMast.LowRateMast_Idno.ToString();

            }
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
                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                    
                }
            }

        }
        private void ClearControls()
        {
            drpPanNo.SelectedIndex = 0; 
            taxRate.Text = "0.00";
            hidTaxid.Value = string.Empty;
            drpPrtyName.SelectedValue = "0";
        }

        private void BindGrid()
        {
            LowRateMastDAL objRateMast = new LowRateMastDAL();
            IList lstGridData = objRateMast.Select(Convert.ToInt32(Convert.ToString(drpPrtyName.SelectedValue) == "" ? 0 : Convert.ToInt32(drpPrtyName.SelectedValue)));
            objRateMast = null;
            grdMain.DataSource = lstGridData;
            grdMain.DataBind();
            if (lstGridData != null)
            {
                if (lstGridData.Count > 0)
                {
                    DivGridShow.Visible = true;
                    lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;
                }
                else
                {
                    DivGridShow.Visible = false;
                    lblTotalRecord.Text = "Total Record (s): 0";
                    lblcontant.Visible = false;
                    divpaging.Visible = false;
                }
            }
            else
            {
                DivGridShow.Visible = false;
                lblTotalRecord.Text = "Total Record (s): 0";
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string smsg = string.Empty;
            if ((Convert.ToDouble(Convert.ToDouble(taxRate.Text.Trim())) <= 0) || (Convert.ToDouble(Convert.ToDouble(taxRate.Text.Trim())) >= 100))
            {
                if (Convert.ToDouble(taxRate.Text.Trim()) <= 0)
                    ShowMessageErr("Please Enter Tax Rate!");
                else
                    ShowMessageErr("Tax rate cannot be greater than or equal to 100");

                taxRate.Focus(); 
                return;
            }

            LowRateMastDAL objTaxMastBLL = new LowRateMastDAL();
            Int64 RateId = 0;
            int compID = 0;
            string PanNo = Convert.ToString(drpPanNo.SelectedItem.Text.Trim());
            double rate = Convert.ToDouble(Convert.ToString(taxRate.Text));
            Int32 PrtyIdno = Convert.ToInt32(drpPrtyName.SelectedValue);
            DateTime dt = Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtDate.Text.Trim()));
            if (string.IsNullOrEmpty(hidTaxid.Value) == true)
            {
                RateId = objTaxMastBLL.Insert(PrtyIdno,PanNo, rate, compID, Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtDate.Text.Trim())),empIdno);
            }
            else
            {
                RateId = objTaxMastBLL.Update(Convert.ToInt32(hidTaxid.Value), PrtyIdno, PanNo, rate, compID, Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtDate.Text.Trim())), empIdno);
            }
            if (RateId == 0)
            {

                ShowMessageErr("Record  Not Saved ");
            }
            else if (RateId < 0)
            {
                ShowMessageErr("Record already exists."); 
            }
            else if (RateId > 0)
            {
                if (hidTaxid.Value == "")
                {
                   ShowMessage("Record Saved Successfully"); 
                }
                else
                { 
                    ShowMessage("Record Updated Successfully."); 
                }
                this.ClearControls();
                this.BindGrid();
            }  
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            this.ClearControls();
            this.BindGrid();
        }

        
        #endregion

        #region Grid Events...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            { 
                int TaxMastID = Convert.ToInt32(e.CommandArgument);
                this.Populate(TaxMastID);
            }
            else if (e.CommandName == "cmddelete")
            {
                LowRateMastDAL objTaxMastBLL = new LowRateMastDAL(); 
                int TaxMastID = Convert.ToInt32(e.CommandArgument);
                int value=objTaxMastBLL.Delete(TaxMastID);
                objTaxMastBLL = null;
                if(value > 0)
                  ShowMessage("Record Deleted Successfully"); 
                this.ClearControls();
                this.BindGrid();
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
        #endregion

        #region Control Events...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void drpPrtyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindPanNo();
            BindGrid();
        }
        protected void drpPanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
        #endregion

    }
}
