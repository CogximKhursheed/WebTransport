using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class FuelRateMaster : Pagebase
    {
        #region Private Variable...
        int intFormId = 0;
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
                //if (base.View == false)
                //{
                //    lnkbtnSave.Visible = false;
                //}
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                cOMPiD = Convert.ToInt32(Session["CompId"].ToString());
                this.BindDateRange();
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindState();
                //this.BindGrid();
                drpPump.Focus();
                this.BindFuel();
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
            drpPump.DataSource = null;
            FuelRateMasterDAL objDAL = new FuelRateMasterDAL();
            var lst = objDAL.BindPump();
            objDAL = null;
            drpPump.DataSource = lst;
            drpPump.DataTextField = "Acnt_Name";
            drpPump.DataValueField = "Acnt_Idno";
            drpPump.DataBind();
            drpPump.Items.Insert(0, new ListItem("--Select Pump--", "0"));
        }

        private void BindFuel()
        {
            FuelRateMasterDAL objclsFuelSlip = new FuelRateMasterDAL();
            var objFuelSlip = objclsFuelSlip.SelectItemName();
            objclsFuelSlip = null;
            drpFuel.DataSource = objFuelSlip;
            drpFuel.DataTextField = "Item_Name";
            drpFuel.DataValueField = "Item_Idno";
            drpFuel.DataBind();
            drpFuel.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
        #endregion

        #region Functions...

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            this.ClearControls();
        }
        private void Populate(int Idno)
        {
            lnkbtnNew.Visible = true;
            FuelRateMasterDAL objDAL = new FuelRateMasterDAL();
            tblFuelRateMaster tblMast = objDAL.SelectByID(Idno);
            objDAL = null;
            if (tblMast != null)
            {
                ddlDateRange.SelectedValue = Convert.ToString(tblMast.Year_Idno);
                txtDate.Text = Convert.ToString(Convert.ToDateTime(tblMast.FuelRate_Date).ToString("dd-MM-yyyy"));
                drpPump.SelectedValue = Convert.ToString(tblMast.Acnt_Idno);
                txtDate.Enabled = drpPump.Enabled = false;
                drpFuel.SelectedValue = Convert.ToString(tblMast.ItemIdno ?? 0);
                txtFuelRate.Text = Convert.ToString(tblMast.Fuel_Rate);
                hidIdno.Value = tblMast.FuelRate_Idno.ToString();
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
            lnkbtnNew.Visible = false; txtDate.Enabled = drpPump.Enabled = true;
            txtFuelRate.Text = "0.00"; hidIdno.Value = string.Empty;
            this.SetDate();
            ddlDateRange.Focus();
        }

        private void BindGrid()
        {
            FuelRateMasterDAL obj = new FuelRateMasterDAL();
            IList lstGridData = obj.BindRecords(Convert.ToInt32(drpPump.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue));
            obj = null;
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
            if (string.IsNullOrEmpty(txtFuelRate.Text.Trim()) == true || Convert.ToDouble(txtFuelRate.Text.Trim()) <= 0)
            {
                this.ShowMessageErr("Rate Should be greater than Zero.");  return;
            }
            if(drpFuel.SelectedValue == "0")
            {
                this.ShowMessageErr("Please select fuel."); drpFuel.Focus(); return;
            }
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string smsg = string.Empty;
            Int64 FuelIdno = 0;
            FuelRateMasterDAL objDAL = new FuelRateMasterDAL();
            tblFuelRateMaster tblobj = new tblFuelRateMaster();
            tblobj.FuelRate_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim()));
            tblobj.Acnt_Idno = Convert.ToInt32(drpPump.SelectedValue);
            tblobj.Fuel_Rate = Convert.ToDouble(txtFuelRate.Text.Trim());
            tblobj.Comp_Idno = Convert.ToInt32(cOMPiD);
            tblobj.ItemIdno = Convert.ToInt32(drpFuel.SelectedValue);
            tblobj.Status = true;
            tblobj.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
            tblobj.Date_Added = System.DateTime.Now;
            
            if (string.IsNullOrEmpty(hidIdno.Value) == true)
            {
                FuelIdno = objDAL.Insert(tblobj);
            }
            else
            {
                FuelIdno = objDAL.Update(tblobj, Convert.ToInt32(hidIdno.Value));
            }
            if (FuelIdno == 0)
            {
                ShowMessageErr("Record Not Saved ");
                this.ClearControls();
            }
            else if (FuelIdno < 0)
            {
                ShowMessageErr("Record already exists.");
                this.ClearControls();
            }
            else if (FuelIdno > 0)
            {
                if (string.IsNullOrEmpty(hidIdno.Value) == true)
                {
                   ShowMessage("Record Saved Successfully");
                   this.ClearControls();
                }
                else
                { 
                    ShowMessage("Record Updated Successfully.");
                    this.ClearControls();
                }
                this.BindGrid();
                this.ClearControls();
            }  
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidIdno.Value));
            }
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
                int Idno = Convert.ToInt32(e.CommandArgument);
                this.Populate(Idno);
            }
            else if (e.CommandName == "cmddelete")
            {
                FuelRateMasterDAL obj = new FuelRateMasterDAL(); 
                int Idno = Convert.ToInt32(e.CommandArgument);
                Int32 result = obj.Delete(Idno);
                obj = null;
                if (result == 1)
                {
                    this.ShowMessage("Records Delete Successfully.");
                }
                else
                {
                    this.ShowMessageErr("Records Not Delete.");
                }
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
        protected void drpPump_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDate.Text.Trim()) == true)
            {
                this.ShowMessageErr("Please Selct a Fuel Rate Date.");
            }
            this.BindGrid(); txtFuelRate.Focus();
        }
        #endregion

    }
}
