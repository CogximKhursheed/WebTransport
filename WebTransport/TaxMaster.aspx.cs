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
    public partial class TaxMaster : Pagebase
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
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtLorryFrom.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtlorryto.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                cOMPiD = Convert.ToInt32(Session["CompId"].ToString());
                this.BindDateRange();
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindState();
                this.BindGrid();
                this.BindTaxType();
                SelectPANType();
                drpState.Focus();
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
        private void BindState()
        {
            drpState.DataSource = null;
            TaxMastDAL accountDAL = new TaxMastDAL();
            var lst = accountDAL.SelectState(0);
            accountDAL = null;
            drpState.DataSource = lst;
            drpState.DataTextField = "State_Name";
            drpState.DataValueField = "State_Idno";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }
        private void BindTaxType()
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            var lst = objTaxMastBLL.SelectTax();
            objTaxMastBLL = null;
            drpdownTaxType.DataSource = lst;
            drpdownTaxType.DataTextField = "TaxType_Name";
            drpdownTaxType.DataValueField = "TaxType_Idno";
            drpdownTaxType.DataBind();
            drpdownTaxType.Items.Insert(0, new ListItem("--Select Tax--", "0"));

        }
        private void SelectPANType()
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            var lst = objTaxMastBLL.SelectPANType();
            objTaxMastBLL = null;
            ddlPANType.DataSource = lst;
            ddlPANType.DataTextField = "PANType_Name";
            ddlPANType.DataValueField = "PANType_Idno";
            ddlPANType.DataBind();
            ddlPANType.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        #endregion

        #region Functions...
         
        private void Populate(int TaxMastID)
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            tblTaxMaster TaxMast = objTaxMastBLL.SelectTaxByID(TaxMastID);
            objTaxMastBLL = null;
            if (TaxMast != null)
            {
                txtDate.Text = string.IsNullOrEmpty(Convert.ToString(TaxMast.Tax_Date))?"":Convert.ToString(Convert.ToDateTime(TaxMast.Tax_Date).ToString("dd-MM-yyyy"));
                drpdownTaxType.SelectedValue =string.IsNullOrEmpty(Convert.ToString(TaxMast.TaxTyp_Idno))?"0": TaxMast.TaxTyp_Idno.ToString();
                drpState.SelectedValue = string.IsNullOrEmpty(Convert.ToString(TaxMast.State_Idno))?"0":TaxMast.State_Idno.ToString();
                drpdownTaxType.Enabled = false;
                drpState.Enabled = false;
                taxRate.Text = string.IsNullOrEmpty(Convert.ToString(TaxMast.Tax_Rate)) ? "0.00" : Convert.ToString(TaxMast.Tax_Rate);
                hidTaxid.Value = string.IsNullOrEmpty(Convert.ToString(TaxMast.TaxMast_Idno))?"0":TaxMast.TaxMast_Idno.ToString();
                ddlPANType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(TaxMast.PANTyp_Idno))?"0":Convert.ToString(TaxMast.PANTyp_Idno);
                txtlorryto.Text =string.IsNullOrEmpty(Convert.ToString(TaxMast.LorryCnt_To))?"0": Convert.ToString(TaxMast.LorryCnt_To);
                txtLorryFrom.Text = string.IsNullOrEmpty(Convert.ToString(TaxMast.LorryCnt_From)) ? "0" : Convert.ToString(TaxMast.LorryCnt_From);
                chkCalOnDF.Checked = string.IsNullOrEmpty((Convert.ToString(TaxMast.CalOn_DF))) ? false : Convert.ToBoolean(TaxMast.CalOn_DF);
                //chkLowRate.Checked = Convert.ToBoolean(TaxMast.LowRateWise) == true ? true : false;
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
            drpdownTaxType.SelectedValue = "0"; drpState.SelectedValue = "0";
            taxRate.Text = "0.00";
            drpdownTaxType.Enabled = true;
            drpState.Enabled = true;
            hidTaxid.Value = string.Empty;
            ddlPANType.SelectedIndex = 0;
            txtLorryFrom.Text = "0";
            txtlorryto.Text = "0";
            chkCalOnDF.Checked = false;

        }

        private void BindGrid()
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            IList lstGridData = objTaxMastBLL.Select(0, Convert.ToInt32(Convert.ToString(drpState.SelectedValue) == "" ? 0 : Convert.ToInt32(drpState.SelectedValue)), Convert.ToInt32(Convert.ToString(drpdownTaxType.SelectedValue) == "" ? 0 : Convert.ToInt32(drpdownTaxType.SelectedValue)));
            objTaxMastBLL = null;
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
            if (Convert.ToString(drpdownTaxType.SelectedValue) == "2")
            {
                if (txtLorryFrom.Text == "")
                {
                    txtLorryFrom.Text = "0";
                }
                if (txtlorryto.Text == "")
                {
                    txtlorryto.Text = "0";
                }
                if (Convert.ToInt32(txtLorryFrom.Text.Trim()) <= 0)
                {
                    ShowMessageErr("Please Enter Lorry From!");
                    txtLorryFrom.Focus(); 
                    return; 
                }
                if (Convert.ToInt32(txtlorryto.Text.Trim()) <= 0)
                {
                    ShowMessageErr("Please Enter Lorry To!");
                    txtlorryto.Focus(); 
                    return; 
                }
                if (Convert.ToInt32(txtlorryto.Text.Trim()) <= Convert.ToInt32(txtLorryFrom.Text.Trim()))
                {
                    ShowMessageErr("Lorry From Should be smaller than Lorry To!");
                    txtLorryFrom.Focus(); 
                    return; 
                }
                
            }

            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            Int64 TaxId = 0;
            int compID = 0;//Convert.ToInt32(UsessionValue.CompId);
            // DateTime dateModified = DateTime.Now;
            int taxtypeIdno = Convert.ToInt32(drpdownTaxType.SelectedValue);
            int stateIdno = Convert.ToInt32(drpState.SelectedValue);
            double rate = Convert.ToDouble(Convert.ToString(taxRate.Text));
            int PANtype = Convert.ToInt32(Convert.ToString(ddlPANType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPANType.SelectedValue));
            int lorryfrom = Convert.ToInt32(Convert.ToString(txtLorryFrom.Text));
            int lorryto = Convert.ToInt32(Convert.ToString(txtlorryto.Text));
            bool CalonDf = Convert.ToBoolean((chkCalOnDF.Checked) == true ? 1 : 0);
            //bool LowRate = Convert.ToBoolean((chkLowRate.Checked) == true ? 1 : 0);
            if (drpdownTaxType.SelectedItem.Text.Trim() == "UGST" || drpdownTaxType.SelectedItem.Text.Trim() == "IGST" || drpdownTaxType.SelectedItem.Text.Trim() == "CGST" || drpdownTaxType.SelectedItem.Text.Trim() == "SGST")
            {
                PANtype = 0; lorryfrom = 0; lorryto = 0; CalonDf = false; stateIdno = 0;
            }
            if (string.IsNullOrEmpty(hidTaxid.Value) == true)
            {
                TaxId = objTaxMastBLL.Insert(taxtypeIdno, stateIdno, rate, compID, Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtDate.Text.Trim())), PANtype, lorryfrom, lorryto, CalonDf, empIdno);
            }
            else
            {
                TaxId = objTaxMastBLL.Update(Convert.ToInt32(hidTaxid.Value), taxtypeIdno, stateIdno, rate, compID, Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtDate.Text.Trim())), PANtype, lorryfrom, lorryto, CalonDf, empIdno);
            }
            if (TaxId == 0)
            {

                ShowMessageErr("Record  Not Saved ");
            }
            else if (TaxId < 0)
            {
                ShowMessageErr("Record already exists."); 
            }
            else if (TaxId > 0)
            {
                if (hidTaxid.Value == "")
                {
                   ShowMessage("Record Saved Successfully"); 
                }
                else
                { 
                    ShowMessage("Record Updated Successfully."); 
                    drpState.Enabled = true;
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
                TaxMastDAL objTaxMastBLL = new TaxMastDAL(); 
                int TaxMastID = Convert.ToInt32(e.CommandArgument);
                objTaxMastBLL.Delete(TaxMastID);
                objTaxMastBLL = null;
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
        protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaxMastDAL objTaxMastBLL = new TaxMastDAL();
            var lst = (IList)null;
            if(Convert.ToInt32(drpdownTaxType.SelectedValue) <= 0 )
               lst = objTaxMastBLL.SelectAllTaxByID(Convert.ToInt32(drpState.SelectedValue), cOMPiD);
            else
                lst = objTaxMastBLL.SelectAllTaxTypeByID(Convert.ToInt32(Convert.ToString(drpState.SelectedValue) == "" ? 0 : Convert.ToInt32(drpState.SelectedValue)), Convert.ToInt32(Convert.ToString(drpdownTaxType.SelectedValue) == "" ? 0 : Convert.ToInt32(drpdownTaxType.SelectedValue)), 1);
            
            if (lst.Count > 0)
            {
                DivGridShow.Visible = true;
                objTaxMastBLL = null;
                grdMain.DataSource = lst;
                grdMain.DataBind();
            }
            else
            {
                this.BindGrid();
            }
            drpdownTaxType.Focus();
        }
        protected void drpdownTaxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpdownTaxType.SelectedItem.Text.Trim() == "TDS")
                {
                    ddlPANType.Enabled = txtLorryFrom.Enabled = txtlorryto.Enabled = chkCalOnDF.Enabled  = true;
                }
                else
                {
                    ddlPANType.Enabled = txtLorryFrom.Enabled = txtlorryto.Enabled = chkCalOnDF.Enabled = false;
                }
                if (drpdownTaxType.SelectedItem.Text.Trim() == "UGST" || drpdownTaxType.SelectedItem.Text.Trim() == "IGST" || drpdownTaxType.SelectedItem.Text.Trim() == "CGST" || drpdownTaxType.SelectedItem.Text.Trim() == "SGST")
                {
                    ddlPANType.Enabled = txtLorryFrom.Enabled = txtlorryto.Enabled = chkCalOnDF.Enabled = drpState.Enabled = RequiredFieldValidator1.Enabled = false;
                }
                else
                {
                    ddlPANType.Enabled = txtLorryFrom.Enabled = txtlorryto.Enabled = chkCalOnDF.Enabled = drpState.Enabled =RequiredFieldValidator1.Enabled= true;
                }
                TaxMastDAL objTaxMastBLL = new TaxMastDAL();
                var lst = objTaxMastBLL.SelectAllTaxTypeByID(Convert.ToInt32(Convert.ToString(drpState.SelectedValue) == "" ? 0 : Convert.ToInt32(drpState.SelectedValue)), Convert.ToInt32(Convert.ToString(drpdownTaxType.SelectedValue) == "" ? 0 : Convert.ToInt32(drpdownTaxType.SelectedValue)), 1);
                if (lst.Count > 0)
                { 
                    objTaxMastBLL = null;
                    DivGridShow.Visible = true;
                    grdMain.DataSource = lst;
                    grdMain.DataBind();
                }
                else
                {
                    this.BindGrid();
                }
            }
            catch (Exception Ex)
            {
            }
            txtDate.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
        #endregion

    }
}
