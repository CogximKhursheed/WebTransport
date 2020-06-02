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
    public partial class ManualTripSheet : Pagebase
    {
        #region Variable ...
        static FinYearA UFinYear = new FinYearA();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();
        DataTable dtTable = new DataTable(); bool IsWeight = false; Double iRate = 0.00;
        double dblTtAmnt = 0; static bool UserPrefGradeVal;
        int rb = 0; Int32 iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0; Int64 AdvOrderGR_Idno = 0;
        private int intFormId = 27;
        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DBName"] != null)
                if (Session["DBName"].ToString() == "TrHariCargoCarrierDdUK")
                    Response.Redirect("CustomTripSheet.aspx");
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            
            if (base.CheckUserRights(intFormId) == false)
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            if (!IsPostBack)
            {
                this.BindDateRange();
                BindDropdown();
                ManualTripSheetDAL obj = new ManualTripSheetDAL();
                txtTripNo.Text = obj.GetMaxTripNo(Convert.ToInt32(ddlDateRange.SelectedValue), ApplicationFunction.ConnectionString()).ToString();
                if (Request.QueryString["TripId"] != null)
                {
                    this.Populate(Request.QueryString["TripId"]);
                }
            }
        }

        #endregion

        #region Button Event...
        public void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                CalculateAll();
                Int64 return_status = 0;
                ManualTripSheetDAL obj = new ManualTripSheetDAL();
                if (Request.QueryString["TripId"] == null)
                {
                    return_status = obj.InsertTripSheet(Convert.ToInt64(txtTripNo.Text), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtTripDate.Text.Trim().ToString())), Convert.ToInt32(ddlCompFromCity.SelectedValue), Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToString(txtDriverName.Text), Convert.ToString(txkStartKms.Text), Convert.ToString(txkEndKms.Text), Convert.ToInt32(ddlDateRange.SelectedValue),
                        Convert.ToString(txtItemName.Text), Convert.ToString(txtItemSize.Text), Convert.ToInt32(ddlRateType.SelectedValue), Convert.ToDouble(txtQuantity.Text == "" ? "0" : txtQuantity.Text), Convert.ToDouble(txtGweight.Text == "" ? "0" : txtGweight.Text), Convert.ToDouble(txtAweight.Text == "" ? "0" : txtAweight.Text), Convert.ToDouble(txtrate.Text == "" ? "0" : txtrate.Text), Convert.ToDouble(txtAdvance.Text == "" ? "0" : txtAdvance.Text), Convert.ToDouble(txtCommission.Text == "" ? "0" : txtCommission.Text), Convert.ToDouble(txtTotalPartyAdv.Text == "" ? "0" : txtTotalPartyAdv.Text), Convert.ToDouble(txtRTOChallan.Text == "" ? "0" : txtRTOChallan.Text), Convert.ToDouble(txtDetention.Text == "" ? "0" : txtDetention.Text),
                        Convert.ToDouble(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text), Convert.ToDouble(txtTotalFreight.Text == "" ? "0" : txtTotalFreight.Text), Convert.ToDouble(txtReceived.Text == "" ? "0" : txtReceived.Text), Convert.ToInt32(ddlRecType.SelectedValue), Convert.ToDouble(txtTotalPartyBalance.Text == "" ? "0" : txtTotalPartyBalance.Text), Convert.ToDouble(txtDriver.Text == "" ? "0" : txtDriver.Text), Convert.ToDouble(txtDiesel.Text == "" ? "0" : txtDiesel.Text), Convert.ToDouble(txtDriverAc.Text == "" ? "0" : txtDriverAc.Text), Convert.ToDouble(txtTotalVehAdv.Text == "" ? "0" : txtTotalVehAdv.Text), Convert.ToDouble(txtNetTripProfit.Text == "" ? "0" : txtNetTripProfit.Text), chkFixRate.Checked);
                }
                else
                {
                    Int64 intTripIdno = Convert.ToInt64(Request.QueryString["TripId"]);
                    return_status = obj.UpdateTripSheet(intTripIdno, Convert.ToInt64(txtTripNo.Text == "" ? "0" : txtTripNo.Text), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtTripDate.Text.Trim().ToString())), Convert.ToInt32(ddlCompFromCity.SelectedValue), Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToString(txtDriverName.Text), Convert.ToString(txkStartKms.Text), Convert.ToString(txkEndKms.Text), Convert.ToInt32(ddlDateRange.SelectedValue),
                        Convert.ToString(txtItemName.Text), Convert.ToString(txtItemSize.Text), Convert.ToInt32(ddlRateType.SelectedValue), Convert.ToDouble(txtQuantity.Text == "" ? "0" : txtQuantity.Text), Convert.ToDouble(txtGweight.Text == "" ? "0" : txtGweight.Text), Convert.ToDouble(txtAweight.Text == "" ? "0" : txtAweight.Text), Convert.ToDouble(txtrate.Text), Convert.ToDouble(txtAdvance.Text == "" ? "0" : txtAdvance.Text), Convert.ToDouble(txtCommission.Text == "" ? "0" : txtCommission.Text), Convert.ToDouble(txtTotalPartyAdv.Text == "" ? "0" : txtTotalPartyAdv.Text), Convert.ToDouble(txtRTOChallan.Text == "" ? "0" : txtRTOChallan.Text), Convert.ToDouble(txtDetention.Text == "" ? "0" : txtDetention.Text),
                        Convert.ToDouble(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text), Convert.ToDouble(txtTotalFreight.Text == "" ? "0" : txtTotalFreight.Text), Convert.ToDouble(txtReceived.Text == "" ? "0" : txtReceived.Text), Convert.ToInt32(ddlRecType.SelectedValue), Convert.ToDouble(txtTotalPartyBalance.Text), Convert.ToDouble(txtDriver.Text == "" ? "0" : txtDriver.Text), Convert.ToDouble(txtDiesel.Text == "" ? "0" : txtDiesel.Text), Convert.ToDouble(txtDriverAc.Text == "" ? "0" : txtDriverAc.Text), Convert.ToDouble(txtTotalVehAdv.Text == "" ? "0" : txtTotalVehAdv.Text), Convert.ToDouble(txtNetTripProfit.Text == "" ? "0" : txtNetTripProfit.Text), chkFixRate.Checked);
                }
                if (return_status > 0)
                {
                    ShowMessage("Trip sheet is saved successfully.");
                    Response.Redirect("ManualTripSheet.aspx");
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

        public void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ManualTripSheet.aspx");
        }

        private bool CheckEmptyFields()
        {
            if (txtTripNo.Text == "") { txtTripNo.Focus(); return false; }
            if (txtTripDate.Text == "") { txtTripDate.Focus(); return false; }
            if (ddlCompFromCity.SelectedValue == "0") { ddlCompFromCity.Focus(); return false; }
            if (ddlTruckNo.SelectedValue == "0") { ddlTruckNo.Focus(); return false; }
            if (ddlSender.SelectedValue == "0") { ddlSender.Focus(); return false; }
            if (ddlFromCity.SelectedValue == "0") { ddlFromCity.Focus(); return false; }
            if (ddlToCity.SelectedValue == "0") { ddlToCity.Focus(); return false; }
            if (txtDriverName.Text == "") { txtDriverName.Focus(); return false; }
            if (txkStartKms.Text == "") { txkStartKms.Focus(); return false; }
            if (txkEndKms.Text == "") { txkEndKms.Focus(); return false; }
            if (ddlDateRange.SelectedValue == "0") { ddlDateRange.Focus(); return false; }
            if (chkFixRate.Checked == false)
            {

                if (txtItemName.Text == "") { txtItemName.Focus(); return false; }
                if (txtItemSize.Text == "") { txtItemSize.Focus(); return false; }
                if (ddlRateType.SelectedValue == "0") { ddlRateType.Focus(); return false; }
                if (txtQuantity.Text == "") { txtQuantity.Focus(); return false; }
                if (txtGweight.Text == "") { txtGweight.Focus(); return false; }
                if (txtAweight.Text == "") { txtAweight.Focus(); return false; }
                if (txtrate.Text == "") { txtrate.Focus(); return false; }
            }
            else
            {
                txtItemName.Text = "";
                txtItemSize.Text = "";
                txtQuantity.Text = "0";
                txtrate.Text = "0";
                txtGweight.Text = "0";
                txtAweight.Text = "0";
                ddlRateType.SelectedValue = "0";
            }
            if (txtAdvance.Text == "") { txtAdvance.Text = "0"; }
            if (txtCommission.Text == "") { txtCommission.Text = "0"; }
            if (txtTotalPartyAdv.Text == "") { txtTotalPartyAdv.Text = "0"; }
            if (txtRTOChallan.Text == "") { txtRTOChallan.Text = "0"; }
            if (txtDetention.Text == "") { txtDetention.Text = "0"; }
            if (txtTotalAmount.Text == "") { txtTotalAmount.Focus(); return false; }
            if (txtTotalFreight.Text == "") { txtTotalFreight.Text = "0"; }
            if (txtReceived.Text == "") { txtReceived.Text = "0"; }
            if (txtTotalPartyBalance.Text == "") { txtTotalPartyBalance.Text = "0"; }
            if (txtDriver.Text == "") { txtDriver.Text = "0"; }
            if (txtDiesel.Text == "") { txtDiesel.Text = "0"; }
            if (txtDriverAc.Text == "") { txtDriverAc.Text = "0"; }
            if (txtTotalVehAdv.Text == "") { txtTotalVehAdv.Text = "0"; }
            if (txtNetTripProfit.Text == "") { txtTotalAmount.Focus(); return false; }
            else return true;
        }
        public void Calculate_Click(object sender, EventArgs e)
        {
            Double dQty = Convert.ToDouble(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
            Double dGrossWeight = Convert.ToDouble(txtGweight.Text == "" ? "0" : txtGweight.Text);
            Double dRate = Convert.ToDouble(txtrate.Text == "" ? "0" : txtrate.Text);
            if (ddlRateType.SelectedValue == "0")
            {
                this.ShowMessageErr("Please Select rate type."); ddlRateType.SelectedIndex = 0; ddlRateType.Focus();
            }
            else if (ddlRateType.SelectedValue == "1")
            {
                txtTotalAmount.Text = (dQty * dRate).ToString();
            }
            else if (ddlRateType.SelectedValue == "2")
            {
                txtTotalAmount.Text = (dGrossWeight * dRate).ToString();
            }
            CalculateAll();
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        public void lnkWithVehicle_click(object sender, EventArgs e)
        {
            PrintTrip(1);
        }

        public void lnkWithoutVehicle_click(object sender, EventArgs e)
        {
            PrintTrip(2);
        }

        public void PrintTrip(int PrintType)
        {
            //PrintType = 1 Then With Vehicle detail Else 2 Without
            Int64 tripIdno = Convert.ToInt64(Request.QueryString["TripId"] == null ? "0" : Request.QueryString["TripId"]);
            ManualTripSheetDAL obj = new ManualTripSheetDAL();
            tblManualTripHead mt = obj.GetTripSheet(tripIdno);
            #region Variable declaration
            string TripNo = mt.Trip_No.ToString();
            string TripDate = mt.Trip_Date.ToString();
            string CompFromCity = mt.BaseCity_Idno.ToString();
            string TruckNo = mt.Truck_Idno.ToString();
            string Sender = mt.Party_Idno.ToString();
            string FromCity = mt.FromCity_idno.ToString();
            string ToCity = mt.ToCity_idno.ToString();
            string DriverName = mt.Driver_Name.ToString();
            string StartKms = mt.StartKms.ToString();
            string EndKms = mt.EndKms.ToString();
            string DateRange = mt.Year_Idno.ToString();
            string ItemName = mt.Item_Name.ToString();
            string ItemSize = mt.Item_Size.ToString();
            string RateType = mt.Rate_Type.ToString();
            string Quantity = mt.Quantity.ToString();
            string Gweight = mt.Gross_Weight.ToString();
            string Aweight = mt.Actual_Weight.ToString();
            string rate = mt.Item_Rate.ToString();
            string Advance = mt.Party_Adv.ToString();
            string Commission = mt.Party_Comm.ToString();
            string TotalPartyAdv = mt.TotalParty_Adv.ToString();
            string RTOChallan = mt.RTO_Chln.ToString();
            string Detention = mt.Detention.ToString();
            string TotalAmount = mt.Freight_Amnt.ToString();
            string TotalFreight = mt.GrossFreight_Amnt.ToString();
            string Received = mt.Received_Amnt.ToString();
            string RecType = mt.Rec_Type.ToString();
            string TotalPartyBalance = mt.TotalParty_Bal.ToString();
            string Driver = mt.DriverCash_Amnt.ToString();
            string Diesel = mt.Diesel_Amnt.ToString();
            string DriverAc = mt.DriverAC_Amnt.ToString();
            string TotalVehAdv = mt.TotalVeh_Amnt.ToString();
            string NetTripProfit = mt.NetTrip_Profit.ToString();
            #endregion
            string str = "PrintManualTrip.aspx?PrintType=" + PrintType +
                                "&TripNo=" + TripNo +
                                "&TripDate=" + TripDate +
                                "&CompFromCity=" + CompFromCity +
                                "&TruckNo=" + TruckNo +
                                "&Sender=" + Sender +
                                "&FromCity=" + FromCity +
                                "&ToCity=" + ToCity +
                                "&DriverName=" + DriverName +
                                "&StartKms=" + StartKms +
                                "&EndKms=" + EndKms +
                                "&DateRange=" + DateRange +
                                "&ItemName=" + ItemName +
                                "&ItemSize=" + ItemSize +
                                "&RateType=" + RateType +
                                "&Quantity=" + Quantity +
                                "&Gweight=" + Gweight +
                                "&Aweight=" + Aweight +
                                "&rate=" + rate +
                                "&Advance=" + Advance +
                                "&Commission=" + Commission +
                                "&TotalPartyAdv=" + TotalPartyAdv +
                                "&RTOChallan=" + RTOChallan +
                                "&Detention=" + Detention +
                                "&TotalAmount=" + TotalAmount +
                                "&TotalFreight=" + TotalFreight +
                                "&Received=" + Received +
                                "&RecType=" + RecType +
                                "&TotalPartyBalance=" + TotalPartyBalance +
                                "&Driver=" + Driver +
                                "&Diesel=" + Diesel +
                                "&DriverAc=" + DriverAc +
                                "&TotalVehAdv=" + TotalVehAdv +
                                "&NetTripProfit=" + NetTripProfit;
            Response.Redirect(str);
        }

        #endregion
        #region Events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
            txtTripDate.Focus();
        }
        public void txtTripNo_TextChanged(object sender, EventArgs e)
        {
            ManualTripSheetDAL obj = new ManualTripSheetDAL();
            txtTripNo.Text = obj.GetMaxTripNo(Convert.ToInt32(ddlDateRange.SelectedValue), ApplicationFunction.ConnectionString()).ToString();
            ddlCompFromCity.Focus();
        }
        public void txtTotalFreight_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalAmount.Text == "") txtTotalAmount.Text = "0.00";
            CalculateAll();
            txtAdvance.Focus();
        }

        public void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            if (txtAdvance.Text == "") txtAdvance.Text = "0.00";
            CalculateAll();
            txtCommission.Focus();
        }
        public void txtCommission_TextChanged(object sender, EventArgs e)
        {
            if (txtCommission.Text == "") txtCommission.Text = "0.00";
            CalculateAll();
            txtRTOChallan.Focus();
        }
        public void txtRTOChallan_TextChanged(object sender, EventArgs e)
        {
            if (txtRTOChallan.Text == "") txtRTOChallan.Text = "0.00";
            CalculateAll();
            txtDetention.Focus();
        }
        public void txtDetention_TextChanged(object sender, EventArgs e)
        {
            if (txtDetention.Text == "") txtDetention.Text = "0.00";
            CalculateAll();
            txtReceived.Focus();
        }
        public void txtReceived_TextChanged(object sender, EventArgs e)
        {
            if (txtReceived.Text == "") txtReceived.Text = "0.00";
            CalculateAll();
            ddlRecType.Focus();
        }
        public void DriverCash_TextChanged(object sender, EventArgs e)
        {
            if (txtDriver.Text == "") txtDriver.Text = "0.00";
            CalculateAll();
            txtDiesel.Focus();
        }
        public void Diesel_TextChanged(object sender, EventArgs e)
        {
            if (txtDiesel.Text == "") txtDiesel.Text = "0.00";
            CalculateAll();
            txtDriverAc.Focus();
        }
        public void DriverAc_TextChanged(object sender, EventArgs e)
        {
            if (txtDriverAc.Text == "") txtDriverAc.Text = "0.00";
            CalculateAll();
            lnkbtnSubmit.Focus();
        }
        public void FixRate_CheckedChanged(object Sender, EventArgs e)
        {
            if (chkFixRate.Checked == true)
            {
                txtItemName.Text = "";
                txtItemSize.Text = "";
                txtQuantity.Text = "";
                txtrate.Text = "";
                txtGweight.Text = "";
                txtAweight.Text = "";
                txtTotalAmount.Text = "";
                ddlRateType.SelectedValue = "0";
                txtItemName.Enabled = false;
                txtItemSize.Enabled = false;
                txtQuantity.Enabled = false;
                txtrate.Enabled = false;
                txtGweight.Enabled = false;
                txtAweight.Enabled = false;
                ddlRateType.Enabled = false;
            }
            else
            {
                txtItemName.Enabled = true;
                txtItemSize.Enabled = true;
                txtQuantity.Enabled = true;
                txtrate.Enabled = true;
                txtGweight.Enabled = true;
                txtAweight.Enabled = true;
                ddlRateType.Enabled = true;
            }
        }
        #endregion

        #region Functions
        private void Populate(string tripId)
        {
            lnkbtnPrint.Visible = true;
            lnkbtnSubmit.Text = "Update";
            Int64 tripIdno = Convert.ToInt64(tripId);
            ManualTripSheetDAL obj = new ManualTripSheetDAL();
            tblManualTripHead mt = obj.GetTripSheet(tripIdno);

            txtTripNo.Text = mt.Trip_No.ToString();
            txtTripDate.Text = ((mt.Trip_Date == null || mt.Trip_Date.ToString() == "") ? "" : Convert.ToDateTime(mt.Trip_Date.ToString()).ToString("dd-MM-yyyy"));
            ddlCompFromCity.SelectedValue = mt.BaseCity_Idno.ToString();
            ddlTruckNo.SelectedValue = mt.Truck_Idno.ToString();
            ddlSender.SelectedValue = mt.Party_Idno.ToString();
            ddlFromCity.SelectedValue = mt.FromCity_idno.ToString();
            ddlToCity.SelectedValue = mt.ToCity_idno.ToString();
            txtDriverName.Text = mt.Driver_Name.ToString();
            txkStartKms.Text = mt.StartKms.ToString();
            txkEndKms.Text = mt.EndKms.ToString();
            ddlDateRange.SelectedValue = mt.Year_Idno.ToString();
            txtItemName.Text = mt.Item_Name.ToString();
            txtItemSize.Text = mt.Item_Size.ToString();
            ddlRateType.SelectedValue = mt.Rate_Type.ToString();
            txtQuantity.Text = mt.Quantity.ToString();
            txtGweight.Text = mt.Gross_Weight.ToString();
            txtAweight.Text = mt.Actual_Weight.ToString();
            txtrate.Text = mt.Item_Rate.ToString();
            txtAdvance.Text = mt.Party_Adv.ToString();
            txtCommission.Text = mt.Party_Comm.ToString();
            txtTotalPartyAdv.Text = mt.TotalParty_Adv.ToString();
            txtRTOChallan.Text = mt.RTO_Chln.ToString();
            txtDetention.Text = mt.Detention.ToString();
            txtTotalAmount.Text = mt.Freight_Amnt.ToString();
            txtTotalFreight.Text = mt.GrossFreight_Amnt.ToString();
            txtReceived.Text = mt.Received_Amnt.ToString();
            ddlRecType.SelectedValue = mt.Rec_Type.ToString();
            txtTotalPartyBalance.Text = mt.TotalParty_Bal.ToString();
            txtDriver.Text = mt.DriverCash_Amnt.ToString();
            txtDiesel.Text = mt.Diesel_Amnt.ToString();
            txtDriverAc.Text = mt.DriverAC_Amnt.ToString();
            txtTotalVehAdv.Text = mt.TotalVeh_Amnt.ToString();
            txtNetTripProfit.Text = mt.NetTrip_Profit.ToString();
            if (mt.FixRate == true)
            {
                chkFixRate.Checked = mt.FixRate == null ? false : Convert.ToBoolean(mt.FixRate);
                txtItemName.Text = "";
                txtItemSize.Text = "";
                txtQuantity.Text = "";
                txtrate.Text = "";
                txtGweight.Text = "";
                txtAweight.Text = "";
                ddlRateType.SelectedValue = "0";
                txtItemName.Enabled = false;
                txtItemSize.Enabled = false;
                txtQuantity.Enabled = false;
                txtrate.Enabled = false;
                txtGweight.Enabled = false;
                txtAweight.Enabled = false;
                ddlRateType.Enabled = false;
            }
        }

        private void CalculateAll()
        {
            Double dPartyAdvance = 0;
            Double dPartyCommission = 0;
            Double dPartyTotalAdvance = 0;
            Double dRTOChallan = 0;
            Double dDetention = 0;
            Double dFreight = 0;
            Double dTotalFreight = 0;
            Double dReceived = 0;
            Double dTotalPartyBalance = 0;
            Double dDriverCash = 0;
            Double dDiesel = 0;
            Double dDriverAc = 0;
            Double dTotalVehAdv = 0;
            Double dNetTripProfit = 0;
            //Party Advance
            dPartyAdvance = Convert.ToDouble(txtAdvance.Text == "" ? "0" : txtAdvance.Text);
            dPartyCommission = Convert.ToDouble(txtCommission.Text == "" ? "0" : txtCommission.Text);
            dPartyTotalAdvance = dPartyAdvance + dPartyCommission;
            txtTotalPartyAdv.Text = dPartyTotalAdvance.ToString();

            //Total Freight
            dFreight = Convert.ToDouble(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
            dRTOChallan = Convert.ToDouble(txtRTOChallan.Text == "" ? "0" : txtRTOChallan.Text);
            dDetention = Convert.ToDouble(txtDetention.Text == "" ? "0" : txtDetention.Text);
            dTotalFreight = dRTOChallan + dDetention + dFreight;
            txtTotalFreight.Text = dTotalFreight.ToString();

            //Party Balance
            dReceived = Convert.ToDouble(txtReceived.Text == "" ? "0" : txtReceived.Text);
            dTotalPartyBalance = dTotalFreight - (dPartyAdvance + dReceived);
            txtTotalPartyBalance.Text = dTotalPartyBalance.ToString();

            //Net Trip Profit
            dDriverCash = Convert.ToDouble(txtDriver.Text == "" ? "0" : txtDriver.Text);
            dDiesel = Convert.ToDouble(txtDiesel.Text == "" ? "0" : txtDiesel.Text);
            dDriverAc= Convert.ToDouble(txtDriverAc.Text == "" ? "0" : txtDriverAc.Text);
            dTotalVehAdv = dDriverCash + dDiesel + dDriverAc;
            dNetTripProfit = dTotalFreight - dTotalVehAdv;
            txtTotalVehAdv.Text = dTotalVehAdv.ToString();
            txtNetTripProfit.Text = dNetTripProfit.ToString();
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

            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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

        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }

        public void ClearFields()
        {
            txtTripNo.Text = "";
            txtTripDate.Text = "";
            ddlCompFromCity.SelectedValue = "0";
            ddlTruckNo.SelectedValue = "0";
            ddlSender.SelectedValue = "0";
            ddlFromCity.SelectedValue = "0";
            ddlToCity.SelectedValue = "0";
            txtDriverName.Text = "";
            txkStartKms.Text = "";
            txkEndKms.Text = "";
            //ddlDateRange.SelectedValue = "0";
            txtItemName.Text = "";
            txtItemSize.Text = "";
            ddlRateType.SelectedValue = "0";
            txtQuantity.Text = "";
            txtGweight.Text = "";
            txtAweight.Text = "";
            txtrate.Text = "";
            txtAdvance.Text = "";
            txtCommission.Text = "";
            txtTotalPartyAdv.Text = "";
            txtRTOChallan.Text = "";
            txtDetention.Text = "";
            txtTotalAmount.Text = "";
            txtTotalFreight.Text = "";
            txtReceived.Text = "";
            ddlRecType.SelectedValue = "0";
            txtTotalPartyBalance.Text = "";
            txtDriver.Text = "";
            txtDiesel.Text = "";
            txtDriverAc.Text = "";
            txtTotalVehAdv.Text = "";
            txtNetTripProfit.Text = "";
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
        #endregion
    }
}