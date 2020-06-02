using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;
using System.Linq;


namespace WebTransport
{
    public partial class CustomTripSheet : Pagebase
    {
        private int intFormId = 27;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (base.CheckUserRights(intFormId) == false)
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            txkStartKms.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txkEndKms.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtdriverno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtdslcardltr.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
            txtdslcardrate.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtremark.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");


            if (!IsPostBack)
            {
                this.BindDateRange();
                BindDropdown();
                this.BindLane();
                CustomTripSheetDAL objDAL = new CustomTripSheetDAL();
                if (Request.QueryString["TripId"] != null)
                {
                    this.Populate(Request.QueryString["TripId"]);
                }
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
        private void BindLane()
        {
            CustomTripSheetDAL objDAL = new CustomTripSheetDAL();
            var objlist = objDAL.BindLane();
            ddlLane.DataSource = objlist;
            ddlLane.DataTextField = "Lane_Name";
            ddlLane.DataValueField = "Lane_Idno";
            ddlLane.DataBind();
            objDAL = null;
            ddlLane.Items.Insert(0, new ListItem("--- Select Lane ---", "0"));
        }
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            var receiverLst = obj.BindSender();
            var TruckNolst = obj.BindTruckNowithLastDigit();
            var ToCity = obj.BindAllToCity();
            var Agent = obj.BindAgent();
            var bank = obj.BindBank();
            var UnitName = obj.BindUnitName();
            var ToCityComp = obj.BindLocFrom();
            obj = null;

            ddlSender.DataSource = senderLst;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlCompFromCity.DataSource = ToCity;
            ddlCompFromCity.DataTextField = "city_name";
            ddlCompFromCity.DataValueField = "city_idno";
            ddlCompFromCity.DataBind();
            ddlCompFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlCompFromCity.DataSource = ToCityComp;
            ddlCompFromCity.DataTextField = "City_Name";
            ddlCompFromCity.DataValueField = "City_Idno";
            ddlCompFromCity.DataBind();
            ddlCompFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        public void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CustomTripSheet.aspx");
        }
        public void ClearFields()
        {
            txtTripNo.Text = "";
            txtTripDate.Text = "";
            ddlCompFromCity.SelectedValue = "0";
            ddlTruckNo.SelectedValue = "0";
            ddlSender.SelectedValue = "0";
            ddlLane.SelectedValue = "0";
            txtDriverName.Text = "";
            txtdriverno.Text = "";
            txtVehicleSize.Text = "";
            txkStartKms.Text = "";
            txkEndKms.Text = "";
            txtTotalKms.Text = "";
            txtdslqty.Text = "";
            txtdslrate.Text = "";
            txtdslamt.Text = "";
            txtcash.Text = "";
            txtdslcardamt.Text = "";
            txtdslcardrate.Text = "";
            txtdslcardltr.Text = "";
            txttoll.Text = "";
            txtwages.Text = "";
            txtfoodexp.Text = "";
            txtrepair.Text = "";
            txtexdslamt.Text = "";
            txtexdslltr.Text = "";
            txttotaldslqty.Text = "";
            txttotaldslamt.Text = "";
            txtmilage.Text = "";
            txtadvdriver.Text = "";
            txtother.Text = "";
            txtremark.Text = "";
            txttotalamt.Text = "";
        }
        private bool CheckEmptyFields()
        {
            if (txtTripDate.Text == "") { txtTripDate.Focus(); return false; }
            if (txtTripNo.Text == "") { txtTripNo.Focus(); return false; }
            if (ddlCompFromCity.SelectedValue == "0") { ddlCompFromCity.Focus(); return false; }
            if (ddlTruckNo.SelectedValue == "0") { ddlTruckNo.Focus(); return false; }
            if (ddlSender.SelectedValue == "0") { ddlSender.Focus(); return false; }
            if (ddlLane.SelectedValue == "0") { ddlLane.Focus(); return false; }
            if (txtDriverName.Text == "") { txtDriverName.Focus(); return false; }
            if (txtdriverno.Text == "") { txtdriverno.Focus(); return false; }
            if (txtVehicleSize.Text == "") { txtVehicleSize.Focus(); return false; }
            if (txkStartKms.Text == "") { txkStartKms.Focus(); return false; }
            if (txkEndKms.Text == "") { txkEndKms.Focus(); return false; }
            if (txtdslcardltr.Text == "") { txtdslcardltr.Focus(); return false; }
            if (ddlDateRange.SelectedValue == "0") { ddlDateRange.Focus(); return false; }
            if (txtdslqty.Text == "") { txtdslqty.Text = "0"; }
            if (txtdslrate.Text == "") { txtdslrate.Text = "0"; }
            if (txtcash.Text == "") { txtcash.Text = "0"; }
            if (txtdslcardamt.Text == "") { txtdslcardamt.Text = "0"; }
            if (txtdslcardrate.Text == "") { txtdslcardrate.Focus(); }
            if (txttoll.Text == "") { txttoll.Text = "0"; }
            if (txtwages.Text == "") { txtwages.Text = "0"; }
            if (txtfoodexp.Text == "") { txtfoodexp.Text = "0"; }
            if (txtrepair.Text == "") { txtrepair.Text = "0"; }
            if (txtadvdriver.Text == "") { txtadvdriver.Text = "0"; }
            if (txtother.Text == "") { txtother.Text = "0"; }
            if (txtremark.Text == "") { txtremark.Focus(); return false; }
            else return true;
        }
        private void CalculateAll()
        {
            //TOTAL KM.
            Double strtkm = Convert.ToDouble(String.IsNullOrEmpty(txkStartKms.Text) ? "0" : txkStartKms.Text);
            Double endkm = Convert.ToDouble(String.IsNullOrEmpty(txkEndKms.Text) ? "0" : txkEndKms.Text);
            Double totalkm = endkm - strtkm;
            txtTotalKms.Text = Convert.ToString(totalkm);

            //TotalAmt
            Double dQty = Convert.ToDouble(txtdslqty.Text == "" ? "0" : txtdslqty.Text);
            Double dRate = Convert.ToDouble(txtdslrate.Text == "" ? "0" : txtdslrate.Text);

            double damount = dQty * dRate;
            txtdslamt.Text = damount.ToString();

            //EX. DSL AMT.
            Double dtoll = Convert.ToDouble(txttoll.Text == "" ? "0" : txttoll.Text);
            Double dwages = Convert.ToDouble(txtwages.Text == "" ? "0" : txtwages.Text);
            Double dfoodex = Convert.ToDouble(txtfoodexp.Text == "" ? "0" : txtfoodexp.Text);
            Double drepair = Convert.ToDouble(txtrepair.Text == "" ? "0" : txtrepair.Text);

            Double dcash = Convert.ToDouble(txtcash.Text == "" ? "0" : txtcash.Text);
            Double dexdslamnt = dcash - (dtoll + dwages + dfoodex + drepair);
            txtexdslamt.Text = dexdslamnt.ToString("N2");

            //EX. DSL. LTR.
            Double dexdslamt = Convert.ToDouble(txtexdslamt.Text == "" ? "0" : txtexdslamt.Text);
            Double ddslrate = Convert.ToDouble(txtdslrate.Text == "" ? "0" : txtdslrate.Text);
            Double dexdslltr = dexdslamt / ddslrate;
            txtexdslltr.Text = dexdslltr.ToString("N2");

            //TOTAL DSL QTY

            Double dtotaldslltr = dQty + dexdslltr;
            txttotaldslqty.Text = dtotaldslltr.ToString("N2");

            //TOTAL DSL AMT.
            Double ddslcrdamnt = Convert.ToDouble(txtdslcardamt.Text == "" ? "0" : txtdslcardamt.Text);
            Double dtotaldslamt = damount + ddslcrdamnt + dexdslamnt;
            txttotaldslamt.Text = dtotaldslamt.ToString("N2");

            ////MILAGE
            double dmilage = 0.0;
            double TotalKms = Convert.ToDouble(string.IsNullOrEmpty(txtTotalKms.Text) ? "0.0" : txtTotalKms.Text);
            //double Qty = Convert.ToDouble(string.IsNullOrEmpty(txtdslqty.Text) ? "0.0" : txtdslqty.Text);
            double Qty = Convert.ToDouble(string.IsNullOrEmpty(txttotaldslqty.Text) ? "0.0" : txttotaldslqty.Text);
            if (Qty > 0)
                dmilage = (TotalKms / Qty);
            txtmilage.Text = dmilage.ToString("N2");

            //TOTAL AMT.
            double dtxtadvdriver = Convert.ToDouble(txtadvdriver.Text == "" ? "0" : txtadvdriver.Text);
            double dother = Convert.ToDouble(txtother.Text == "" ? "0" : txtother.Text);
            Double dtotal = damount + dcash + ddslcrdamnt + dother + dtxtadvdriver;
            txttotalamt.Text = dtotal.ToString("N2");

        }

        public void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {

            if (CheckEmptyFields())
            {
                CalculateAll();
                Int64 return_status = 0;
                CustomTripSheetDAL obj = new CustomTripSheetDAL();
                if (Request.QueryString["TripId"] == null)
                {
                    return_status = obj.InsertTripSheet(Convert.ToInt64(txtTripNo.Text),
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtTripDate.Text.Trim().ToString())),
                        Convert.ToInt32(ddlCompFromCity.SelectedValue),
                        Convert.ToInt32(ddlTruckNo.SelectedValue),
                        Convert.ToInt32(ddlSender.SelectedValue),
                        Convert.ToInt32(ddlLane.SelectedValue),
                        Convert.ToString(txtDriverName.Text),
                        Convert.ToInt64(txtdriverno.Text),
                        Convert.ToString(txtVehicleSize.Text),
                        Convert.ToString(txkStartKms.Text),
                        Convert.ToString(txkEndKms.Text),
                        Convert.ToString(txtTotalKms.Text),
                        Convert.ToInt32(ddlDateRange.SelectedValue),
                        Convert.ToDouble(txtdslqty.Text == "" ? "0" : txtdslqty.Text),
                        Convert.ToDouble(txtdslrate.Text == "" ? "0" : txtdslrate.Text),
                        Convert.ToDouble(txtdslamt.Text == "" ? "0" : txtdslamt.Text),
                        Convert.ToDouble(txtcash.Text == "" ? "0" : txtcash.Text),
                        Convert.ToDouble(txtdslcardamt.Text == "" ? "0" : txtdslcardamt.Text),
                        Convert.ToString(txtdslcardrate.Text == "" ? "0" : txtdslcardrate.Text),
                        Convert.ToString(txtdslcardltr.Text),
                        Convert.ToDouble(txttoll.Text == "" ? "0" : txttoll.Text),
                        Convert.ToDouble(txtwages.Text == "" ? "0" : txtwages.Text),
                        Convert.ToDouble(txtfoodexp.Text == "" ? "0" : txtfoodexp.Text),
                        Convert.ToDouble(txtrepair.Text == "" ? "0" : txtrepair.Text),
                        Convert.ToDouble(txtexdslamt.Text == "" ? "0" : txtexdslamt.Text),
                        Convert.ToDouble(txtexdslltr.Text == "" ? "0" : txtexdslltr.Text),
                        Convert.ToDouble(txttotaldslqty.Text == "" ? "0" : txttotaldslqty.Text),
                        Convert.ToDouble(txttotaldslamt.Text == "" ? "0" : txttotaldslamt.Text),
                        Convert.ToDouble(txtmilage.Text == "" ? "0" : txtmilage.Text),
                        Convert.ToDouble(txtadvdriver.Text == "" ? "0" : txtadvdriver.Text),
                        Convert.ToDouble(txtother.Text == "" ? "0" : txtother.Text),
                        Convert.ToString(txtremark.Text),
                        Convert.ToDouble(txttotalamt.Text == "" ? "0" : txttotalamt.Text));

                }
                else
                {
                    Int64 intTripIdno = Convert.ToInt64(Request.QueryString["TripId"]);
                    return_status = obj.UpdateTripSheet(intTripIdno, Convert.ToInt64(txtTripNo.Text),
                       Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtTripDate.Text.Trim().ToString())),
                       Convert.ToInt32(ddlCompFromCity.SelectedValue),
                       Convert.ToInt32(ddlTruckNo.SelectedValue),
                       Convert.ToInt32(ddlSender.SelectedValue),
                       Convert.ToInt32(ddlLane.SelectedValue),
                       Convert.ToString(txtDriverName.Text),
                       Convert.ToInt64(txtdriverno.Text),
                       Convert.ToString(txtVehicleSize.Text),
                       Convert.ToString(txkStartKms.Text),
                       Convert.ToString(txkEndKms.Text),
                       Convert.ToString(txtTotalKms.Text),
                       Convert.ToInt32(ddlDateRange.SelectedValue),
                       Convert.ToDouble(txtdslqty.Text == "" ? "0" : txtdslqty.Text),
                       Convert.ToDouble(txtdslrate.Text == "" ? "0" : txtdslrate.Text),
                       Convert.ToDouble(txtdslamt.Text == "" ? "0" : txtdslamt.Text),
                       Convert.ToDouble(txtcash.Text == "" ? "0" : txtcash.Text),
                       Convert.ToDouble(txtdslcardamt.Text == "" ? "0" : txtdslcardamt.Text),
                       Convert.ToString(txtdslcardrate.Text == "" ? "0" : txtdslcardrate.Text),
                       Convert.ToString(txtdslcardltr.Text),
                       Convert.ToDouble(txttoll.Text == "" ? "0" : txttoll.Text),
                       Convert.ToDouble(txtwages.Text == "" ? "0" : txtwages.Text),
                       Convert.ToDouble(txtfoodexp.Text == "" ? "0" : txtfoodexp.Text),
                       Convert.ToDouble(txtrepair.Text == "" ? "0" : txtrepair.Text),
                       Convert.ToDouble(txtexdslamt.Text == "" ? "0" : txtexdslamt.Text),
                       Convert.ToDouble(txtexdslltr.Text == "" ? "0" : txtexdslltr.Text),
                       Convert.ToDouble(txttotaldslqty.Text == "" ? "0" : txttotaldslqty.Text),
                       Convert.ToDouble(txttotaldslamt.Text == "" ? "0" : txttotaldslamt.Text),
                       Convert.ToDouble(txtmilage.Text == "" ? "0" : txtmilage.Text),
                       Convert.ToDouble(txtadvdriver.Text == "" ? "0" : txtadvdriver.Text),
                       Convert.ToDouble(txtother.Text == "" ? "0" : txtother.Text),
                       Convert.ToString(txtremark.Text),
                       Convert.ToDouble(txttotalamt.Text == "" ? "0" : txttotalamt.Text));

                }
                if (return_status > 0)
                {
                    if (string.IsNullOrEmpty(hidtripid.Value) == false)
                    {
                        ShowMessage("Trip sheet is updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Trip sheet is saved successfully.");
                    }
                    ClearFields();
                }
                else if (return_status == -1)
                {
                    ShowMessageErr("Trip sheet already exists.");
                }
                else
                {
                    ShowMessageErr("Trip sheet SAVED FAILURE.");
                }
            }
            else
            {
                ShowMessageErr("Please fill all the fields.");
            }
        }
        #region Events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
            txtTripDate.Focus();
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
                    if ((Convert.ToString(Session["GRDate"]) == ""))
                        txtTripDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    else
                        txtTripDate.Text = Session["GRDate"].ToString();
                }
                else
                {
                    txtTripDate.Text = hidmindate.Value;
                }
            }

        }

        protected void ddlCompFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomTripSheetDAL obj = new CustomTripSheetDAL();
            txtTripNo.Text = obj.GetTripNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(ddlCompFromCity.SelectedValue), ApplicationFunction.ConnectionString()).ToString();
            txtTripNo.Focus();
            tblCityMaster mt = obj.GEtLocationDetail(Convert.ToInt64(ddlCompFromCity.SelectedValue));
            txtPref.Text = Convert.ToString(mt.City_Abbr);
        }

        public void txtdslrate_TextChanged(object sender, EventArgs e)
        {
            if (txtdslamt.Text == "") txtdslamt.Text = "0.00";
            CalculateAll();
            txtcash.Focus();
        }
        public void txttoll_TextChanged(object sender, EventArgs e)
        {
            if (txttoll.Text == "") txttoll.Text = "0.00";
            CalculateAll();
            txtwages.Focus();
        }
        public void txtwages_TextChanged(object sender, EventArgs e)
        {
            if (txtwages.Text == "") txtwages.Text = "0.00";
            CalculateAll();
            txtfoodexp.Focus();
        }
        public void txtfoodexp_TextChanged(object sender, EventArgs e)
        {
            if (txtfoodexp.Text == "") txtfoodexp.Text = "0.00";
            CalculateAll();
            txtrepair.Focus();
        }
        public void txtrepair_TextChanged(object sender, EventArgs e)
        {
            if (txtrepair.Text == "") txtrepair.Text = "0.00";
            CalculateAll();
            txtadvdriver.Focus();
        }
        public void txkEndKms_TextChanged(object sender, EventArgs e)
        {

            CalculateAll();
            txtdslqty.Focus();
        }
        public void txtadvdriver_TextChanged(object sender, EventArgs e)
        {
            CalculateAll();
            txtother.Focus();
        }
        public void txtother_TextChanged(object sender, EventArgs e)
        {
            CalculateAll();
            txtremark.Focus();
        }
        #endregion


        private void Populate(string tripId)
        {
            lnkbtnPrint.Visible = true;
            lnkbtnSubmit.Text = "Update";            
            Int64 tripIdno = Convert.ToInt64(tripId);
            CustomTripSheetDAL obj = new CustomTripSheetDAL();
            Int64 MaxTripId = obj.GetMaxId(Convert.ToInt32(ddlDateRange.SelectedValue), ApplicationFunction.ConnectionString());
            if (Request.QueryString["TripId"] != null && Convert.ToInt64(Request.QueryString["TripId"]) != MaxTripId)
                lnkbtnNext.Visible = true;
            tblManualTripHead mt = obj.GetTripSheet(tripIdno);
            if (mt != null)
            {
                txtTripNo.Text = mt.Trip_No.ToString(); txtTripNo.Enabled = false;
                txtTripDate.Text = ((mt.Trip_Date == null || mt.Trip_Date.ToString() == "") ? "" : Convert.ToDateTime(mt.Trip_Date.ToString()).ToString("dd-MM-yyyy")); txtTripDate.Enabled = true;
                ddlCompFromCity.SelectedValue = mt.BaseCity_Idno.ToString(); ddlCompFromCity.Enabled = true;
                ddlTruckNo.SelectedValue = mt.Truck_Idno.ToString(); ddlTruckNo.Enabled = true;
                ddlSender.SelectedValue = mt.Party_Idno.ToString(); ddlSender.Enabled = true;
                ddlLane.SelectedValue = mt.Lane_Idno.ToString(); ddlLane.Enabled = true;
                txtDriverName.Text = mt.Driver_Name.ToString(); txtDriverName.Enabled = true;
                txtdriverno.Text = mt.Driver_No.ToString(); txtdriverno.Enabled = true;
                txtVehicleSize.Text = mt.Vehicle_Size.ToString(); txtVehicleSize.Enabled = true;
                txkStartKms.Text = mt.StartKms.ToString(); txkStartKms.Enabled = true;
                ddlDateRange.SelectedValue = mt.Year_Idno.ToString(); ddlDateRange.Enabled = true;
                txtdslqty.Text = mt.DSL_Qty.ToString(); txtdslqty.Enabled = true;
                txtdslrate.Text = mt.DSL_Rate.ToString(); txtdslrate.Enabled = true;
                txtdslamt.Text = mt.DSL_Amt.ToString(); txtdslamt.Enabled = true;
                txtcash.Text = mt.Cash.ToString(); txtcash.Enabled = true;
                txttoll.Text = mt.Toll.ToString(); txttoll.Enabled = true;
                txtwages.Text = mt.Dihadi.ToString(); txtwages.Enabled = true;
                txtfoodexp.Text = mt.Food_Exp.ToString(); txtfoodexp.Enabled = true;
                txtrepair.Text = mt.Repair.ToString(); txtrepair.Enabled = true;
                txttotaldslqty.Text = mt.Total_DSL_Qty.ToString(); txttotaldslqty.Enabled = true;
                txttotaldslamt.Text = mt.Total_DSL_Amt.ToString(); txttotaldslamt.Enabled = true;
                txtmilage.Text = mt.Milage.ToString(); txtmilage.Enabled = true;
                txtadvdriver.Text = mt.Adv_in_Driver.ToString(); txtadvdriver.Enabled = true;
                txtother.Text = mt.Other.ToString(); txtother.Enabled = true;
                txtremark.Text = mt.Remark.ToString(); txtremark.Enabled = true;
                txttotalamt.Text = mt.Total_Amt.ToString(); txttotalamt.Enabled = true;
                txtTotalKms.Text = mt.TotalKms.ToString();
                txkEndKms.Text = mt.EndKms.ToString();
                txtdslcardamt.Text = mt.DSL_Card_Amt.ToString();
                txtdslcardrate.Text = mt.DSL_Card_Rate.ToString();
                txtdslcardltr.Text = mt.DSL_Card_Name.ToString();
                txtexdslamt.Text = mt.Ex_DSL_Amt.ToString();
                txtexdslltr.Text = mt.Ex_DSL_Ltr.ToString();
                hidtripid.Value = mt.Trip_Idno.ToString();
            }
        }

        public void lnkbtnNext_OnClick(object sender, EventArgs e)
        {
            CustomTripSheetDAL obj = new CustomTripSheetDAL();
            Int64 MaxTripId = obj.GetMaxId(Convert.ToInt32(ddlDateRange.SelectedValue), ApplicationFunction.ConnectionString());
            if (Request.QueryString["TripId"] != null && Convert.ToInt64(Request.QueryString["TripId"]) != MaxTripId)
            {
                Int32 TripId = Convert.ToInt32(Request.QueryString["TripId"]) + 1;
                Response.Redirect("CustomTripSheet.aspx?TripId=" + TripId, true);
            }
        }
    }
}
