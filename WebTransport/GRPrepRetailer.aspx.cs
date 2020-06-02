using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace WebTransport
{
    public partial class GRPrepRetailer : Pagebase
    {
        #region Variable ...
        static FinYearA UFinYear = new FinYearA();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();
        DataTable dtTable = new DataTable(); bool IsWeight = false; Double iRate = 0.00;
        double dblTtAmnt = 0; static bool UserPrefGradeVal;
        int rb = 0; Int32 iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0; Int64 AdvOrderGR_Idno = 0;
        string CompGSTIN = String.Empty;
        private int intFormId = 27; Int32 comp_Id;
        string strSQL = ""; double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;// bool isTBBRate = false;dtotlAmnt="";
        double dSurchgPer = 0; double ItemWtAmnt = 0;
        double dCFT = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dSwacchBhrtTaxPer = 0, dtotalAmount = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, dSwacchBhrtTaxValid = 0, dKalyanTax = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;
        int chkbit = 0;
        double grAmnt = 0;
        //Upadhyay #GST
        double dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Amt, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per = 0;
        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            string strPostBackControlName = Request.Params.Get("__EVENTTARGET");
            if (!Page.IsPostBack)
            {
                GetPreferences();
                this.BindDateRange();
                this.ddlDateRange_SelectedIndexChanged(null, null);
                this.BindDropdown();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                this.BindReceiptType();
                this.BindItemInsert();
                this.JavascriptValidation();
                ddlType_SelectedIndexChanged(null, null);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                this.BindMaxNo(Convert.ToInt32((ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), intYearIdno);

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                GRPrepRetailerDAL objGrprepRetDAL = new GRPrepRetailerDAL();
                tblUserPref hiduserpref = objGrprepRetDAL.selectuserpref();
                HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
                hidRenamePF.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.PFLabel_GR)) ? "PF" : Convert.ToString(hiduserpref.PFLabel_GR);
                hidrefrename.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.Reflabel_Gr)) ? "Ref. No." : Convert.ToString(hiduserpref.Reflabel_Gr);
                hidRenameToll.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.TollTaxLabel_GR)) ? "Toll Tax" : Convert.ToString(hiduserpref.TollTaxLabel_GR);
                HidsRenCartage.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.CartageLabel_GR)) ? "Cartage" : Convert.ToString(hiduserpref.CartageLabel_GR);
                HidRenCommission.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.CommissionLabel_Gr)) ? "Commission" : Convert.ToString(hiduserpref.CommissionLabel_Gr);
                HidRenBilty.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.BiltyLabel_GR)) ? "Bilty" : Convert.ToString(hiduserpref.BiltyLabel_GR);
                HidStcharge.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.StChargLabel_GR)) ? "S. T. Charges" : Convert.ToString(hiduserpref.StChargLabel_GR);
                hidTBBType.Value = Convert.ToString(objGrprepRetDAL.SelectTBBRate());
                HiddSurchgPer.Value = Convert.ToString(hiduserpref.Surchg_Per);
                HiddWagsAmnt.Value = Convert.ToString(hiduserpref.Wages_Amnt);
                HiddBiltyAmnt.Value = Convert.ToString(hiduserpref.Bilty_Amnt);
                HiddTolltax.Value = Convert.ToString(hiduserpref.TollTax_Amnt);
                HiddServTaxValid.Value = Convert.ToString(hiduserpref.ServTax_Valid);
                HidiFromCity.Value = Convert.ToString(base.UserFromCity);
                ddlFromCity.SelectedValue = Convert.ToString(HidiFromCity.Value);
                ddlRcptType_SelectedIndexChanged(null, null);
                if ((string.IsNullOrEmpty(Convert.ToString(hidTBBType.Value)) ? "0" : Convert.ToString(hidTBBType.Value)) == "True")
                {
                    txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(hiduserpref.Bilty_Amnt)) ? "0" : Convert.ToDouble(Convert.ToString(hiduserpref.Bilty_Amnt)).ToString("N2");
                    txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(HiddTolltax.Value)) ? "0" : Convert.ToDouble(Convert.ToString(HiddTolltax.Value)).ToString("N2");
                    txtWages.Text = string.IsNullOrEmpty(Convert.ToString(HiddWagsAmnt.Value)) ? "0" : Convert.ToDouble(Convert.ToString(HiddWagsAmnt.Value)).ToString("N2");
                    RDbDirect.Checked = true;
                    rdbAdvanceOrder.Enabled = false;
                    ddlSender.Enabled = true;
                    ddlReceiver.Enabled = true;
                    ddlFromCity.Enabled = true;
                    ddlToCity.Enabled = true;
                    ddlLocation.Enabled = true;
                    ddlDateRange.Enabled = true;
                    RDbDirect.Enabled = true;
                }
                else
                {
                    txtBilty.Text = "0.00";
                    txtTollTax.Text = "0.00";
                    txtWages.Text = "0.00";
                    rdbAdvanceOrder.Enabled = true;
                    if (RDbDirect.Checked == false)
                    {
                        ddlSender.Enabled = false;
                        ddlReceiver.Enabled = false;
                        ddlFromCity.Enabled = false;
                        ddlToCity.Enabled = false;
                        ddlLocation.Enabled = false;
                        ddlDateRange.Enabled = false;
                        RDbDirect.Enabled = true;
                    }
                }
                if (Convert.ToString(HidsRenWages.Value) != "")
                {
                    lbltxtwages.Text = Convert.ToString(HidsRenWages.Value);
                    labHam.Text = Convert.ToString(HidsRenWages.Value);

                }
                else
                {
                    lbltxtwages.Text = "Wages";
                    labHam.Text = "Wages";
                }
                if (Convert.ToString(hidRenamePF.Value) != "")
                {
                    lbltxtPF.Text = Convert.ToString(hidRenamePF.Value);
                    labCollCha.Text = Convert.ToString(hidRenamePF.Value);
                }
                else
                {
                    lbltxtPF.Text = "PF";
                    labCollCha.Text = "PF";
                }
                if (Convert.ToString(hidrefrename.Value) != "")
                {
                    lblrefrename.Text = Convert.ToString(hidrefrename.Value);
                }
                else
                {
                    lblrefrename.Text = "Ref.No.";
                }
                if (Convert.ToString(hidRenameToll.Value) != "")
                {
                    lbltxtTolltax.Text = Convert.ToString(hidRenameToll.Value);
                    labDelCha.Text = Convert.ToString(hidRenameToll.Value);
                }
                else
                {
                    lbltxtTolltax.Text = "Toll Tax";
                    labDelCha.Text = "Toll Tax";
                }
                if (Convert.ToString(HidsRenCartage.Value) != "")
                {
                    lblCar.Text = Convert.ToString(HidsRenCartage.Value);
                    labFOV.Text = Convert.ToString(HidsRenCartage.Value);
                }
                else
                {
                    lblCar.Text = "Cartage";
                    labFOV.Text = "Cartage";
                }
                if (Convert.ToString(HidRenCommission.Value) != "")
                {
                    lblCom.Text = Convert.ToString(HidRenCommission.Value);
                    labOctroi.Text = Convert.ToString(HidRenCommission.Value);
                }
                else
                {
                    lblCom.Text = "Commission";
                    labOctroi.Text = "Commission";
                }
                if (Convert.ToString(HidRenBilty.Value) != "")
                {
                    lblBil.Text = Convert.ToString(HidRenBilty.Value);
                    labDemCha.Text = Convert.ToString(HidRenBilty.Value);
                }
                else
                {
                    lblBil.Text = "Bilty";
                    labDemCha.Text = "Bilty";
                }
                if (Convert.ToString(HidStcharge.Value) != "")
                {
                    lblStCharges.Text = Convert.ToString(HidStcharge.Value);
                    labST.Text = Convert.ToString(HidStcharge.Value);
                }
                else
                {
                    labST.Text = "S. T. Charges";
                    lblStCharges.Text = "S. T. Charges";
                }
                if (Request.QueryString["Gr"] != null)
                {
                    //divPosting.Visible = false;
                    try
                    {
                        this.Populate(Convert.ToInt32(Request.QueryString["Gr"]));
                    }
                    catch (Exception ex)
                    {

                    }
                    lnkbtnAdd.Visible = true;
                    lnkbtnPrint.Visible = true;
                    // tblUserPref objuserpref = objGrprepDAL.selectuserpref();
                    //if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                    //{
                    //    
                    //}
                    //else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                    //{
                    //    lnkJainPrint.Visible = true;
                    //}
                    lnkBtnLast.Visible = false;

                    //ddlDateRange.Enabled = false;
                    //ddlFromCity.Enabled = false;
                    //this.GrRateControl();
                    ddlDateRange.Enabled = true;
                    //ddlFromCity.Enabled = true;
                    lnkbtnNew.Visible = true;
                    //lnkChlnGen.Visible = true;
                    BindItemUpdate();
                }
                else
                {
                    lnkBtnLast.Visible = true;
                    lnkbtnNew.Visible = false;
                    //lnkJainPrint.Visible = false;
                    //lnkChlnGen.Visible = false;
                    lnkbtnPrint.Visible = false;
                    BindItemInsert();
                    BindLorry();
                    this.SetDefault();
                }
            }
            else if (strPostBackControlName == "TranTypeValue")
            {
                this.BindDropdownTran();
                this.ddlTranType.Focus();
            }
            HideShowTaxFields();
        }
        #endregion

        #region Functions...
        private void JavascriptValidation()
        {
            txtweight.Attributes.Add("onchange", "SetNumFormt('" + txtweight.ClientID + "');");
            txtActWeight.Attributes.Add("onchange", "SetNumFormt('" + txtActWeight.ClientID + "');");
            txtrate.Attributes.Add("onchange", "SetNumFormt('" + txtrate.ClientID + "');");
            txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            //txtCFT.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            //txtDimension.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ChangeOnAgainst()", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);

        }
        public string NumberToText(int number)
        {
            if (number == 0) return "Zero";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
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
                        txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    else
                        txtGRDate.Text = Session["GRDate"].ToString();
                    txtInstDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtInstDate.Text = hidmindate.Value;
                }
            }

        }
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            var receiverLst = obj.BindSender();
            var ToCity = obj.BindAllToCity();
            var Agent = obj.BindAgent();
            var bank = obj.BindBank();
            var UnitName = obj.BindUnitName();
            var TranType = obj.BindTranType();
            var TruckNolst = obj.BindTruckNo();
            //var MiscList = obj.BindTransportaion(Convert.ToInt64(ddlTranType.SelectedValue));            
            obj = null;

            ddlSender.DataSource = senderLst;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlReceiver.DataSource = receiverLst;
            ddlReceiver.DataTextField = "acnt_name";
            ddlReceiver.DataValueField = "acnt_idno";
            ddlReceiver.DataBind();
            ddlReceiver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlTranType.DataSource = TranType;
            ddlTranType.DataTextField = "Tran_Type";
            ddlTranType.DataValueField = "TranType_Idno";
            ddlTranType.DataBind();

            //if (ddlTranType.SelectedValue == "0")
            //{
            //    ddlTruckNo.DataSource = TruckNolst;
            //    ddlTruckNo.DataTextField = "Lorry_No";
            //    ddlTruckNo.DataValueField = "Lorry_Idno";
            //    ddlTruckNo.DataBind();
            //    ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            //}
            //else
            //{
            //    ddlTruckNo.DataSource = MiscList;
            //    ddlTruckNo.DataTextField = "Misc_Name";
            //    ddlTruckNo.DataValueField = "Misc_Idno";
            //    ddlTruckNo.DataBind();
            //    ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            //}
            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlCityVia.DataSource = ToCity;
            ddlCityVia.DataTextField = "city_name";
            ddlCityVia.DataValueField = "city_idno";
            ddlCityVia.DataBind();
            ddlCityVia.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlLocation.DataSource = ToCity;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlParty.DataSource = Agent;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlcustbank.DataSource = bank;
            ddlcustbank.DataTextField = "Acnt_Name";
            ddlcustbank.DataValueField = "Acnt_Idno";
            ddlcustbank.DataBind();
            ddlcustbank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlunitname.DataSource = UnitName;
            ddlunitname.DataTextField = "UOM_Name";
            ddlunitname.DataValueField = "UOM_idno";
            ddlunitname.DataBind();
            ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDropdownTran()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            var MiscList = obj.BindTransportaion(Convert.ToInt64(ddlTranType.SelectedValue));
            obj = null;

            if (ddlTranType.SelectedValue == "0")
            {
                ddlTruckNo.DataSource = TruckNolst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();
                ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                lblTruckNo.Text = "Truck No.";
            }
            else
            {
                if (ddlTranType.SelectedValue == "1")
                {
                    lblTruckNo.Text = "Flight";
                }
                else if (ddlTranType.SelectedValue == "2")
                {
                    lblTruckNo.Text = "Train";
                }
                else if (ddlTranType.SelectedValue == "3")
                {
                    lblTruckNo.Text = "Bus";
                }
                ddlTruckNo.DataSource = MiscList;
                ddlTruckNo.DataTextField = "Misc_Name";
                ddlTruckNo.DataValueField = "Misc_Idno";
                ddlTruckNo.DataBind();
                ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }

        }
        private void BindLorry()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            lblTruckNo.Text = "Truck No.";
            ddlTranType.SelectedValue = "0";
        }
        private void BindReceiptType()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var RcptType = obj.BindRcptType(ApplicationFunction.ConnectionString());
            ddlRcptType.DataSource = RcptType;
            ddlRcptType.DataTextField = "ACNT_NAME";
            ddlRcptType.DataValueField = "Acnt_Idno";
            ddlRcptType.DataBind();
            ddlRcptType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindItemUpdate()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var itemname = obj.BindItemNameUpdate();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindItemInsert()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var itemname = obj.BindItemName();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Item_Idno", "String",
                "Item_Name", "String",
                "Unit_Idno", "String",
                "Unit_Name", "String",
                "RateType_Idno", "String",
                "Rate_Type", "String",
                "Quantity", "String",
                "Dimension", "String",
                "CFT", "String",
                "Ch_Weight", "String",
                "Act_Weight", "String",
                "Item_Rate", "String",
                "Amount", "Double",
                "Detail", "String",
               "Packet_No", "String"
                );
            return dttemp;
        }
        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
                else
                {
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }
        private void ClearDetails(int type)
        {
            if (type > 0)
            {
                ddlItemName.SelectedValue = ddlunitname.SelectedValue = "0"; ddlRateType.SelectedValue = "1";
                txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtActWeight.Text = "0.00"; txtrate.Text = "0.00";
                txtPacketNo.Text = txtRowAdd.Text = string.Empty;
            }
            else
            {
                ddlRateType.SelectedIndex = ddlunitname.SelectedIndex = ddlItemName.SelectedIndex = 0;
                txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtActWeight.Text = "0.00"; txtrate.Text = "0.00";
                txtCFT.Text = txtDimension.Text = txtPacketNo.Text = txtRowAdd.Text = string.Empty;
                hidrowid.Value = string.Empty;
            }
            ViewState["dt"] = null;
        }
        private bool CheckDuplicatieItem()
        {
            bool value = true;
            DataTable dtTemp = (DataTable)ViewState["dt"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0)) { foreach (DataRow row in dtTemp.Rows) { if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim()))) { value = false; } } }
            if (value == false) { return false; } else { return true; }
        }
        private void SetDefault()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            tblUserDefault lst = obj.SelectDefault(string.IsNullOrEmpty(Session["UserIdno"].ToString()) ? 0 : Convert.ToInt64(Session["UserIdno"].ToString()));
            if (lst != null && lst.User_idno > 0)
            {
                ddlSender.SelectedValue = string.IsNullOrEmpty(lst.SenderIdno.ToString()) ? "0" : lst.SenderIdno.ToString();
                txtconsnr.Text = ddlSender.SelectedItem.Text;
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(lst.FromCity_idno.ToString()) ? "0" : lst.FromCity_idno.ToString();
                ddlFromCity_SelectedIndexChanged(null, null);
                ddlunitname.SelectedValue = string.IsNullOrEmpty(lst.UnitIdno.ToString()) ? "0" : lst.UnitIdno.ToString();
            }
        }

        private void CalculateEdit()
        {
            GRPrepRetailerDAL objGrprepDAL = new GRPrepRetailerDAL();
            double iRate = 0; double EditRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (ddlItemName.SelectedIndex > 0)
            {
                if (ddlGRType.SelectedIndex == 1) //In case of TBB
                {
                    if (hidTBBType.Value == "False")
                    {
                        txtrate.Text = "0.00";
                    }
                    else
                    {
                        if ((ddlRateType.SelectedIndex) > 0)
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                iRate = Convert.ToDouble(txtrate.Text);
                                if (txtQuantity.Text.Trim() != "")
                                    dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtQuantity.Text));
                            }
                            else
                            {
                                iRate = Convert.ToDouble(txtrate.Text);
                                if (txtweight.Text.Trim() != "")
                                    dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtweight.Text));
                            }
                        }
                        else
                        {
                            txtrate.Text = "0.00";
                        }
                    }
                }
                else
                {
                    if ((ddlRateType.SelectedIndex) > 0)
                    {
                        if (ddlRateType.SelectedIndex == 1)
                        {
                            iRate = Convert.ToDouble(txtrate.Text);
                            if (txtrate.Text.Trim() != "")
                                dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtQuantity.Text));
                        }
                        else
                        {
                            iRate = Convert.ToDouble(txtrate.Text);
                            if (txtweight.Text.Trim() != "")
                                dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtweight.Text));
                        }
                    }
                    else
                    {
                        txtrate.Text = "0.00";
                    }
                }
                AgentRate();
                RcptAmtTot(dtTemp);
                netamntcal();
            }
        }
        private void AgentRate()
        {
            try
            {

                GRPrepDAL objGrprepDAL = new GRPrepDAL();
                if (ddlParty.SelectedIndex >= 0)
                {
                    double dAgntRate = 0; double dQty = 0;
                    dAgntRate = objGrprepDAL.SelectAgntRate(Convert.ToInt32(ddlParty.SelectedValue));
                    if (txtQuantity.Text.Trim() != "")
                    {
                        dQty = Convert.ToDouble(txtQuantity.Text);
                    }
                    txtCommission.Text = Convert.ToDouble(dAgntRate * dQty).ToString("N2");
                }
                else
                {
                    txtCommission.Text = "0.00";
                }
            }
            catch (Exception Ex)
            { }
        }
        private void RcptAmtTot(DataTable dtTemp)
        {
            try
            {
                int c = 0, itotRow = 0, itotQty = 0; double itotWeght = 0;
                double dtotAmnt = 0;
                c = grdMain.Rows.Count;
                if (c > 0)
                {
                    for (int i = 0; i < (c - 1); i++)
                    {
                        if ((Convert.ToString(dtTemp.Rows[i]["Item_Idno"]) != "") && (Convert.ToString(dtTemp.Rows[i]["unit_Idno"]) != ""))
                        {
                            itotQty = Convert.ToInt32(itotQty + Convert.ToInt32(dtTemp.Rows[i]["Quantity"]));
                            //itotWeght = itotWeght + Convert.ToDouble(dtTemp.Rows[i]["Weight"]);
                            dtotAmnt = Convert.ToDouble(dtotAmnt + Convert.ToDouble(dtTemp.Rows[i]["Amount"]));
                        }
                    }
                    //if (Convert.ToString(dgvMain.Columns["Qty"].CellText(dgvMain.RowCount - 1)) != "")
                    //    itotQty = itotQty + Convert.ToInt32(dgvMain.Columns["Qty"].CellText(dgvMain.RowCount - 1));
                    //if (Convert.ToString(dgvMain.Columns["Weght_Kg"].CellText(dgvMain.RowCount - 1)) != "")
                    //    itotWeght = itotWeght + Convert.ToDouble(dgvMain.Columns["Weght_Kg"].CellText(dgvMain.RowCount - 1));
                    //if (Convert.ToString(dgvMain.Columns["Amount"].CellText(dgvMain.RowCount - 1)) != "")
                    //    dtotAmnt = Convert.ToDouble(dtotAmnt + Convert.ToDouble(dgvMain.Columns["Amount"].CellText(dgvMain.RowCount - 1)));
                }
                itotRow = grdMain.Rows.Count;
                if (ddlType.SelectedValue == "2")
                    txtGrossAmnt.Text = txtFixedAmount.Text.Trim();
                else
                    txtGrossAmnt.Text = dtotAmnt.ToString("N2");

                //netamntcal();
            }
            catch (Exception Ex)
            { }
        }
        private void netamntcal()
        {
            GRPrepRetailerDAL objGrprepDAL = new GRPrepRetailerDAL();
            try
            {
                int gstType = GetGSTType();
                //IF GST Calculation #GST
                #region GST Calculation
                if (gstType > 0)
                {
                    GetGSTValues();
                    if (ddlGRType.SelectedIndex == 1)
                    {
                        if (hidTBBType.Value == "False")
                        {
                            txtGrossAmnt.Text = txtCartage.Text = txtTotalAmnt.Text = txtSurchrge.Text = txtCommission.Text = txtBilty.Text = "0.00";
                            txtSGSTAmount.Text = txtCGSTAMount.Text = txtIGSTAmount.Text = txtWages.Text = txtPF.Text = txtSubTotal.Text = txtNetAmnt.Text = txtServTax.Text = txtkalyan.Text = txtSwchhBhartTx.Text = txtTollTax.Text = "0.00";
                        }
                        else
                        {
                            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");
                            GetGSTValues();
                            double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                            if (ddlType.SelectedValue == "2")
                            {
                                txtGrossAmnt.Text = txtFixedAmount.Text;
                                txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                                txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");
                            }
                            if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
                            {
                                double gst = 0;
                                if (gstType == 1)
                                {
                                    txtIGSTAmount.Text = "0";
                                    txtIGSTPercent.Text = hidIGSTPer.Value;
                                    dSGST_Per = Convert.ToDouble(Convert.ToString(hidSGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidSGSTPer.Value));
                                    txtSGSTAmount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)).ToString("N2");
                                    txtSGSTPercent.Text = hidSGSTPer.Value == "" ? "0.00" : hidSGSTPer.Value;
                                    dCGST_Per = Convert.ToDouble(Convert.ToString(hidCGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidCGSTPer.Value));
                                    txtCGSTAMount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100)).ToString("N2");
                                    txtCGSTPercent.Text = hidCGSTPer.Value == "" ? "0.00" : hidCGSTPer.Value;
                                    
                                    //Presently GST Cess is not being used
                                    dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                    //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                    gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)) + Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100));
                                }
                                else if (gstType == 2)
                                {
                                    txtSGSTAmount.Text = "0";
                                    txtSGSTPercent.Text = hidSGSTPer.Value;
                                    txtCGSTAMount.Text = "0";
                                    txtCGSTPercent.Text = hidCGSTPer.Value;
                                    dIGST_Per = Convert.ToDouble(Convert.ToString(hidIGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidIGSTPer.Value));
                                    txtIGSTAmount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100)).ToString("N2");
                                    txtIGSTPercent.Text = hidIGSTPer.Value == "" ? "0.00" : hidIGSTPer.Value;
                                    
                                    //Presently GST Cess is not being used
                                    dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                    //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                    gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100));
                                }
                            }
                            else
                            {
                                txtSGSTAmount.Text = txtCGSTAMount.Text = txtIGSTAmount.Text = "0.00";
                            }
                            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtSGSTAmount.Text) + Convert.ToDouble(txtCGSTAMount.Text) + Convert.ToDouble(txtIGSTAmount.Text)))).ToString("N2");
                            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtSGSTAmount.Text) + Convert.ToDouble(txtCGSTAMount.Text) + Convert.ToDouble(txtIGSTAmount.Text))).ToString("N2");
                        }
                    }
                    else
                    {
                        txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                        txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");

                        double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                        if (ddlType.SelectedValue == "2")
                        {
                            txtGrossAmnt.Text = txtFixedAmount.Text;
                            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");
                        }
                        if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
                        {
                            double gst = 0;
                            if (gstType == 1)
                            {
                                dSGST_Per = Convert.ToDouble(Convert.ToString(hidSGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidSGSTPer.Value));
                                txtSGSTAmount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)).ToString("N2");
                                txtSGSTPercent.Text = hidSGSTPer.Value == "" ? "0.00" : hidSGSTPer.Value;
                                dCGST_Per = Convert.ToDouble(Convert.ToString(hidCGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidCGSTPer.Value));
                                txtCGSTAMount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100)).ToString("N2");
                                txtCGSTPercent.Text = hidCGSTPer.Value == "" ? "0.00" : hidCGSTPer.Value;
                                //Presently GST Cess is not being used
                                dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)) + Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100));
                            }
                            else if (gstType == 2)
                            {
                                dIGST_Per = Convert.ToDouble(Convert.ToString(hidIGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidIGSTPer.Value));
                                txtIGSTAmount.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100)).ToString("N2");
                                txtIGSTPercent.Text = hidIGSTPer.Value == "" ? "0.00" : hidIGSTPer.Value;
                                //Presently GST Cess is not being used
                                dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100));
                            }
                        }
                        else
                        {
                            txtSGSTAmount.Text = txtCGSTAMount.Text = txtIGSTAmount.Text = "0.00";
                        }
                        txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtSGSTAmount.Text) + Convert.ToDouble(txtCGSTAMount.Text) + Convert.ToDouble(txtIGSTAmount.Text)))).ToString("N2");
                        txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                        TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtSGSTAmount.Text) + Convert.ToDouble(txtCGSTAMount.Text) + Convert.ToDouble(txtIGSTAmount.Text))).ToString("N2");
                    }
                    if (dSurchgPer > 0)
                    {
                        dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
                        txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
                    }
                    txtServPer.Text = "0";
                    txtServTax.Text = "0";
                    txtkalyan.Text = "0";
                    txtSwchhBhartTx.Text = "0";
                }
                #endregion
                //IF VAT
                #region VAT Calculation
                else
                {
                    if (ddlGRType.SelectedIndex == 1)
                    {
                        if (hidTBBType.Value == "False")
                        {
                            txtGrossAmnt.Text = txtCartage.Text = txtTotalAmnt.Text = txtSurchrge.Text = txtCommission.Text = txtBilty.Text = "0.00";
                            txtWages.Text = txtPF.Text = txtSubTotal.Text = txtNetAmnt.Text = txtServTax.Text = txtkalyan.Text = txtSwchhBhartTx.Text = txtTollTax.Text = "0.00";
                        }
                        else
                        {
                            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");

                            double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                            if (ddlType.SelectedValue == "2")
                            {
                                txtGrossAmnt.Text = txtFixedAmount.Text;
                                txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                                txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");
                            }
                            if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
                            {
                                dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));
                                txtServPer.Text = Convert.ToString(dServTaxPer);
                                txtServTax.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dServTaxPer) / 100)).ToString("N2");

                                dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
                                txtSwchhBhartTx.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSwacchBhrtTaxPer) / 100)).ToString("N2");

                                dKalyanTax = Convert.ToDouble(Convert.ToString(HiddKalyanTax.Value) == "" ? 0 : Convert.ToDouble(HiddKalyanTax.Value));
                                txtkalyan.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dKalyanTax) / 100)).ToString("N2");
                            }
                            else
                            {
                                txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
                            }
                            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text)))).ToString("N2");
                            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text))).ToString("N2");

                        }
                    }
                    else
                    {
                        double dCartage = Convert.ToDouble((txtCartage.Text) == "" ? 0 : Convert.ToDouble(txtCartage.Text));
                        txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + dCartage).ToString("N2");
                        txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble((txtBilty.Text) == "" ? 0 : Convert.ToDouble(txtBilty.Text)) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble((txtTollTax.Text) == "" ? 0 : Convert.ToDouble(txtTollTax.Text))).ToString("N2");

                        double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                        if (ddlType.SelectedValue == "2")
                        {
                            txtGrossAmnt.Text = txtFixedAmount.Text;
                            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text) + Convert.ToDouble(txtSTCharges.Text)).ToString("N2");
                        }
                        if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
                        {
                            dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));
                            txtServTax.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dServTaxPer) / 100)).ToString("N2");

                            dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
                            txtSwchhBhartTx.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSwacchBhrtTaxPer) / 100)).ToString("N2");

                            dKalyanTax = Convert.ToDouble(Convert.ToString(HiddKalyanTax.Value) == "" ? 0 : Convert.ToDouble(HiddKalyanTax.Value));
                            txtkalyan.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dKalyanTax) / 100)).ToString("N2");
                        }
                        else
                        {
                            txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
                        }

                        txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text)))).ToString("N2");
                        txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                        TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text))).ToString("N2");
                    }
                    if (dSurchgPer > 0)
                    {
                        dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
                        txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
                    }
                    txtSGSTAmount.Text = "0";
                    txtCGSTAMount.Text = "0";
                    txtIGSTAmount.Text = "0";
                }
                #endregion
            }
            catch (Exception Ex)
            { }
        }
        public void Populate(Int32 intGRIdno)
        {
            //txtGRDate.Enabled = false;
            GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
            tblGrRetailerHead objGRHead = obj.SelectTblGRHead(intGRIdno);
            DataTable objGRDetl = obj.SelectGRDetail(intGRIdno, ApplicationFunction.ConnectionString());
            hidGRHeadIdno.Value = Convert.ToString(intGRIdno);
            GetGSTValues();
            if (objGRHead != null)
            {
                ddlDateRange.SelectedValue = Convert.ToString(objGRHead.Year_Idno);
                GrRestrictDate.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRRet_Date)) ? "" : Convert.ToDateTime(objGRHead.GRRet_Date).ToString("dd-MM-yyyy");
                txtGRDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRRet_Date)) ? "" : Convert.ToDateTime(objGRHead.GRRet_Date).ToString("dd-MM-yyyy");
                iGrAgainst = Convert.ToInt32(objGRHead.GRRet_Agnst);

                if (iGrAgainst == 1) { RDbDirect.Checked = true; rdbAdvanceOrder.Checked = false; lnkbtnGrAgain.Visible = false; }
                else if (iGrAgainst == 2)
                {
                    RDbDirect.Checked = false; rdbAdvanceOrder.Checked = false; lnkbtnGrAgain.Visible = true; lnkbtnGrAgain.Visible = false;
                    ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlDateRange.Enabled = false; RDbDirect.Enabled = false;
                }
                else
                {
                    rdbAdvanceOrder.Checked = true; RDbDirect.Checked = false; lnkbtnGrAgain.Visible = true; lnkbtnGrAgain.Visible = false;
                    ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlDateRange.Enabled = false; RDbDirect.Enabled = false;
                }

                RDbDirect.Enabled = false; lnkbtnGrAgain.EnableViewState = false; rdbAdvanceOrder.Enabled = false;
                //if ((RDbRecpt.Checked == true) || (rdbAdvanceOrder.Checked == true)) { ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlParty.Enabled = false; }
                //else { ddlSender.Enabled = true; ddlReceiver.Enabled = true; ddlFromCity.Enabled = true; ddlToCity.Enabled = true; ddlLocation.Enabled = true; ddlParty.Enabled = true; }
                ddlGRType.SelectedValue = Convert.ToString(objGRHead.GRRet_Typ);// == "" ? "0" : Convert.ToString(objGRHead.GR_Typ);
                //lblPrintHeadng.Text = "Goods Receipt - " + Convert.ToString((objGRHead.GRRet_Typ) == 1 ? "Paid GR" : (objGRHead.GRRet_Typ == 2) ? "TBB GR" : "To Pay GR");
                //lblTypeOfGr.Text = "(" + Convert.ToString((objGRHead.GRRet_Typ) == 1 ? "Item Wise" : (objGRHead.GRRet_Typ == 2) ? "Fixed Amount Wise" : "Item Wise") + ")";
                ddlGRType.Enabled = false; txtGRNo.Visible = true;
                txtDelvNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.DI_NO)) ? "" : Convert.ToString(objGRHead.DI_NO);
                TxtEGPNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.EGP_No)) ? "" : Convert.ToString(objGRHead.EGP_No);
                txtGRNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRRet_No)) ? "" : Convert.ToString(objGRHead.GRRet_No);

                txtContainrNo.Text = Convert.ToString(objGRHead.Container_No1);
                txtContainerSealNo.Text = Convert.ToString(objGRHead.Seal_No1);
                txtContainrNo2.Text = Convert.ToString(objGRHead.Container_No2);
                txtContainerSealNo2.Text = Convert.ToString(objGRHead.Seal_No2);
                //txtNameI.Text = Convert.ToString(objGRHead.ChaFrwdr_Name);
                //ddlTypeI.SelectedValue = Convert.ToString(objGRHead.ImpExp_idno);


                //ddlContainerSize.SelectedValue = Convert.ToString(objGRHead.GRContanr_Size);
                //ddlContainerType.SelectedValue = Convert.ToString(objGRHead.GRContanr_Type);
                txtPrefixNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRRet_Pref)) ? "" : Convert.ToString(objGRHead.GRRet_Pref);

                txtTotItemPrice.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Total_Price)) ? "" : Convert.ToDouble(objGRHead.Total_Price).ToString("N2");

                ddlSender.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Sender_Idno)) ? "0" : Convert.ToString(objGRHead.Sender_Idno);
                //ddlTruckNo_SelectedIndexChanged(null, null);
                ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Lorry_Idno)) ? "0" : Convert.ToString(objGRHead.Lorry_Idno);
                HiddTruckIdno.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.Lorry_Idno)) ? "0" : Convert.ToString(objGRHead.Lorry_Idno);
                ddlReceiver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Recivr_Idno)) ? "0" : Convert.ToString(objGRHead.Recivr_Idno);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.From_City)) ? "0" : Convert.ToString(objGRHead.From_City);
                ddlToCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.To_City)) ? "0" : Convert.ToString(objGRHead.To_City);
                ddlCityVia.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Via_City)) ? "0" : Convert.ToString(objGRHead.Via_City);
                ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.DelvryPlce_Idno)) ? "0" : Convert.ToString(objGRHead.DelvryPlce_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Agnt_Idno)) ? "0" : Convert.ToString(objGRHead.Agnt_Idno);
                TxtRemark.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Remark)) ? "" : Convert.ToString(objGRHead.Remark);
                ddlTranType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Tran_Type)) ? "0" : Convert.ToString(objGRHead.Tran_Type);
                chkModvat.Checked = Convert.ToBoolean(objGRHead.MODVAT_CPY);
                if (ddlTranType.SelectedValue == "0")
                {
                    BindLorry();
                }
                else
                {
                    BindDropdownTran();
                }
                ddlType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.TypeId)) ? "0" : Convert.ToString(objGRHead.TypeId);

                if (objGRHead.TypeId == 2) { txtFixedAmount.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gross_Amnt)) ? "0.00" : String.Format("{0:0.00}", objGRHead.Gross_Amnt); DivAmount.Visible = true; }
                else { txtFixedAmount.Text = "0.00"; DivAmount.Visible = false; }
                txtRefNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Ref_No)) ? "" : Convert.ToString(objGRHead.Ref_No);
                txtManualNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Manual_No)) ? "" : Convert.ToString(objGRHead.Manual_No);
                txtshipment.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Shipmnt_No)) ? "" : Convert.ToString(objGRHead.Shipmnt_No);
                //txtPortNum.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRContanr_Port)) ? "" : Convert.ToString(objGRHead.GRContanr_Port);
                txtOrderNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Order_No)) ? "" : Convert.ToString(objGRHead.Order_No);
                txtFromNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Form_No)) ? "" : Convert.ToString(objGRHead.Form_No);
                txtRefDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Ref_Date)) ? "" : Convert.ToDateTime(objGRHead.Ref_Date).ToString("dd-MM-yyyy");

                txtconsnr.Text = string.IsNullOrEmpty(Convert.ToString(ddlSender.SelectedItem.Text)) ? "" : Convert.ToString(ddlSender.SelectedItem.Text);
                ddlSrvcetax.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.StaxPaid_ByIdno)) ? "0" : Convert.ToString(objGRHead.StaxPaid_ByIdno);
                ddlRcptType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Rcpt_Type)) ? "0" : Convert.ToString(objGRHead.Rcpt_Type);
                ddlRcptType_SelectedIndexChanged(null, null);
                txtInstNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Inst_No)) ? "" : Convert.ToString(objGRHead.Inst_No);
                txtInstDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Inst_Date)) ? "" : Convert.ToDateTime(objGRHead.Inst_Date).ToString("dd-MM-yyyy");
                ddlcustbank.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.CustBank_Idno)) ? "0" : Convert.ToString(objGRHead.CustBank_Idno);
                hidTBBType.Value = Convert.ToString(objGRHead.TBB_Rate);
                HiddWagsAmnt.Value = Convert.ToString(objGRHead.Wages_Amnt);
                HiddBiltyAmnt.Value = Convert.ToString(objGRHead.Bilty_Amnt);
                HiddTolltax.Value = Convert.ToString(objGRHead.TollTax_Amnt);
                HiddServTaxValid.Value = Convert.ToString(objGRHead.ServTax_Valid);
                //Hiditruckcitywise.Value = Convert.ToString(objGRHead.cmb_type);
                HidiFromCity.Value = Convert.ToString(objGRHead.From_City);
                //HidsRenWages.Value = Convert.ToString(objGRHead.WagesLabel_Print);
                HiddServTaxPer.Value = Convert.ToString(objGRHead.ServTax_Perc);
                HiddSwachhBrtTaxPer.Value = Convert.ToString(objGRHead.SwcgBrtTax_Perc);
                HiddKalyanTax.Value = Convert.ToString(objGRHead.KisanKalyan_Per);
                txtServPer.Text = HiddServTaxPer.Value;
                int id = 1;
                if (ddlGRType.SelectedValue == "1") { ddlRcptType.Enabled = true; } else { ddlRcptType.Enabled = false; ddlRcptType.SelectedIndex = 0; }
                if (Hiditruckcitywise.Value != "1")
                {
                    ddlTruckNo.Visible = true; lblTruckNo.Visible = true; lnkbtnTruuckRefresh.Visible = true;
                }
                else
                {
                    lblTruckNo.Visible = false; ddlTruckNo.Visible = false; lnkbtnTruuckRefresh.Visible = false;
                }
                dtTemp = CreateDt();
                for (int counter = 0; counter < objGRDetl.Rows.Count; counter++)
                {
                    string strItemName = Convert.ToString(objGRDetl.Rows[counter]["Item_Name"]);
                    string strItemNameId = Convert.ToString(objGRDetl.Rows[counter]["Item_Idno"]);
                    string strUnitName = Convert.ToString(objGRDetl.Rows[counter]["UOM_Name"]);
                    string strUnitNameId = Convert.ToString(objGRDetl.Rows[counter]["UOM_Idno"]);
                    string strRateType = Convert.ToString(objGRDetl.Rows[counter]["Rate_Type"]);
                    string strRateTypeIdno = Convert.ToString(objGRDetl.Rows[counter]["RateType_Idno"]);
                    string strQty = Convert.ToString(objGRDetl.Rows[counter]["Quantity"]);
                    string strDim = Convert.ToString(objGRDetl.Rows[counter]["Dimension"]);
                    string strCFT = Convert.ToString(objGRDetl.Rows[counter]["CFT"]);
                    string strCHWeight = Convert.ToString(objGRDetl.Rows[counter]["Ch_Weight"]);
                    string strACTWeight = Convert.ToString(objGRDetl.Rows[counter]["Act_Weight"]);
                    string strRate = Convert.ToString(objGRDetl.Rows[counter]["Item_Rate"]);
                    string strAmount = Convert.ToString(objGRDetl.Rows[counter]["Amount"]);
                    string strDetail = Convert.ToString(objGRDetl.Rows[counter]["Detail"]);
                    string strPacket = Convert.ToString(objGRDetl.Rows[counter]["Packet_No"]);

                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemNameId, strItemName, strUnitNameId, strUnitName, strRateTypeIdno, strRateType, strQty, strDim, strCFT, strCHWeight, strACTWeight, strRate, strAmount, strDetail, strPacket);
                }
                ViewState["dt"] = dtTemp;
                BindGridT();
                txtGrossAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gross_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Gross_Amnt);
                txtCommission.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Commission_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Commission_Amnt);
                txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.TollTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.TollTax_Amnt);
                txtCartage.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Cartage_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Cartage_Amnt);
                txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Bilty_Amnt)) ? "0" : String.Format("{0:0,0.00}", (objGRHead.Bilty_Amnt));
                txtSubTotal.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SubTotal_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.SubTotal_Amnt);
                txtTotalAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Total_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Total_Amnt);
                txtWages.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Wages_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Wages_Amnt);
                txtServTax.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.ServTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.ServTax_Amnt);
                txtSwchhBhartTx.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SwachhBhrtTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.SwachhBhrtTax_Amnt);
                txtkalyan.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.KrishiKalyan_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.KrishiKalyan_Amnt);
                txtSurchrge.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Surcharge_Amnt)) ? "0" : String.Format("{0:0,0.00}", (objGRHead.Surcharge_Amnt));
                txtPF.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.PF_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.PF_Amnt);
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Net_Amount)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Net_Amount);
                TxtRoundOff.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.RoundOff_Amnt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.RoundOff_Amnt);
                txtSTCharges.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Stcharg_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Stcharg_Amnt);

                //Populate #GST

                //Upadhyay #GST
                txtSGSTAmount.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.SGST_Amt);
                txtCGSTAMount.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.CGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.CGST_Amt);
                txtIGSTAmount.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.IGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.IGST_Amt);
                //hidGSTCessAmt.Value = "0";
                txtSGSTPercent.Text = hidSGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.SGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.SGST_Per);
                txtCGSTPercent.Text = hidCGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.CGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.CGST_Per);
                txtIGSTPercent.Text = hidIGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.IGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.IGST_Per);
                hidGSTCessPer.Value = "0";
                //Disabled textfields
                txtSGSTAmount.Enabled = false;
                txtCGSTAMount.Enabled = false;
                txtIGSTAmount.Enabled = false;
                txtSGSTPercent.Enabled = true;
                txtCGSTPercent.Enabled = true;
                txtIGSTPercent.Enabled = true;
                ddlType.SelectedValue = (objGRHead.TypeId == null ? "1" : objGRHead.TypeId.ToString());
                txtFixedAmount.Text = (objGRHead.Type_Amnt == null ? "0" : objGRHead.Type_Amnt.ToString());
                //Int64 ChallanNo = 0; ChallanNo = obj.CheckChallanDetails(intGRIdno);
                //Int64 InvoiceNo = 0; InvoiceNo = obj.CheckInvoiceDetails(intGRIdno);
                obj = null;
                //if (ChallanNo > 0)
                //{
                //    lnkbtnSave.Visible = false; ddlItemName.Enabled = false; lnkbtnSubmit.Enabled = false; lnkbtnAdd.Enabled = false;
                //    DivSave.Visible = false;
                //    Lblchllnno.Text = "Challan No :- " + Convert.ToString(ChallanNo);
                //    if (InvoiceNo > 0)
                //    {
                //        Lblchllnno.Text = "";
                //        Lblchllnno.Text = "Invoice No :- " + Convert.ToString(InvoiceNo);
                //    }
                //}
                //else
                //{
                //    DivSave.Visible = true;
                //    lnkbtnSave.Visible = true; ddlItemName.Enabled = true; lnkbtnSubmit.Enabled = true;
                //    Lblchllnno.Text = "";
                //}

                UpdateDiNo.Visible = UpdateEGPNo.Visible = UpdateInvNo.Visible = UpdateManNo.Visible = UpdateOrdrNo.Visible = UpdateFormNo.Visible = true;
            }
            obj = null;
        }
        private void FillRateWeightWiseRate()
        {
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            AdvBookGRRetDAL objGrprepDAL = new AdvBookGRRetDAL();
            if (txtweight.Text.Trim() != "" && Convert.ToDouble(txtweight.Text.Trim()) > 0.00)
            {
                iRate = objGrprepDAL.SelectItemWeightWiseRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), strGrDate, Convert.ToDecimal(txtweight.Text.Trim()), Convert.ToInt64(ddlSender.SelectedValue), Convert.ToInt64(ddlTranType.SelectedValue));
            }
            txtrate.Text = Convert.ToDouble(iRate > 0 ? iRate : 0.00).ToString("N2");
        }
        private void BindMaxNo(Int32 FromCityIdno, Int32 YearId)
        {
            GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
            Int64 MaxNo = obj.MaxNo(YearId, FromCityIdno, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxNo);
        }
        private void ClearAtFromCityChanged()
        {
            hidGRHeadIdno.Value = string.Empty;
            //hidDelvIdno.Value = string.Empty;
            ddlTruckNo.SelectedIndex = ddlParty.SelectedIndex = ddlRcptType.SelectedIndex = ddlLocation.SelectedIndex = ddlToCity.SelectedIndex = 0;
            txtDelvNo.Text = txtInstNo.Text = TxtEGPNo.Text = "";
            txtGrossAmnt.Text = txtCommission.Text = txtTollTax.Text = txtCartage.Text = txtBilty.Text = txtSubTotal.Text = txtTotalAmnt.Text = txtWages.Text = txtServTax.Text = txtSurchrge.Text = txtPF.Text = txtNetAmnt.Text = TxtRoundOff.Text = "0.00";
            hidrowid.Value = ""; lblmsg.Text = ""; txtkalyan.Text = string.Empty; txtSwchhBhartTx.Text = string.Empty;
            ddlItemName.SelectedIndex = 0; ddlunitname.SelectedIndex = 0; ddlRateType.SelectedIndex = 0; txtInstNo.Text = "";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtdetail.Text = ""; txtrate.Text = "0.00";
            ddlType.SelectedIndex = 0; txtFixedAmount.Text = string.Empty;
            txtshipment.Text = txtDelvNo.Text = txtTotItemPrice.Text = string.Empty;
            lnkbtnPrint.Visible = false;
            grdMain.DataSource = null;
            grdMain.DataBind();

        }
        public void Selectuserpref()
        {
            GRPrepRetailerDAL objGrprepDAL = new GRPrepRetailerDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            dSurchgPer = Convert.ToDouble(userpref.Surchg_Per);
            dServTaxValid = Convert.ToDouble(userpref.ServTax_Valid);

            var lststate = objGrprepDAL.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
            if (lststate != null)
            {
                HiddServTaxPer.Value = Convert.ToString(objGrprepDAL.SelectServiceTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));

                HiddSwachhBrtTaxPer.Value = Convert.ToString(objGrprepDAL.SelectSwacchBrtTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
                HiddKalyanTax.Value = Convert.ToString(objGrprepDAL.SelectKalyanTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            }
            txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
            txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(HiddBiltyAmnt.Value)) ? "0" : String.Format("{0:0,0.00}", (HiddBiltyAmnt.Value));
        }
        private void CalculateTax()
        {
            double Taxper; double Taxvalu; double netamnt; double tax; double subtotal;

            Taxper = Convert.ToDouble(txtServPer.Text);
            Taxvalu = Convert.ToDouble((txtServTax.Text).Replace(",", ""));
            subtotal = Convert.ToDouble((txtSubTotal.Text).Replace(",", ""));

            if (Taxper < 100)
            {
                tax = ((subtotal * Taxper) / 100);


                netamnt = subtotal + tax + Convert.ToDouble(txtkalyan.Text) + Convert.ToDouble(txtSwchhBhartTx.Text);

                txtServTax.Text = Convert.ToDouble(tax).ToString("N2");

                txtNetAmnt.Text = Math.Round(Convert.ToDouble(netamnt)).ToString("N2");

                TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text))).ToString("N2");


            }

        }
        protected void txtServPer_TextChanged(object sender, EventArgs e)
        {
            CalculateTax();
        }
        //Upadhyay #GST
        public void txtGRDate_TextChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["Gr"] != null)
            {
                if (GrRestrictDate.Value != null || GrRestrictDate.Value != "")
                {
                    string gstDate = GetGSTDate();
                    if ((Convert.ToString(gstDate) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrRestrictDate.Value.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(gstDate.Trim()))))
                    {
                        if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(gstDate.Trim())))
                            return;
                        else
                        {
                            ShowMessage("GR created post-GST, so date cannot be lesser than GST-Date.");
                            txtGRDate.Text = GrRestrictDate.Value;
                        }
                    }
                    else if ((Convert.ToString(gstDate) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrRestrictDate.Value.Trim().ToString())) <= Convert.ToDateTime(ApplicationFunction.mmddyyyy(gstDate.Trim()))))
                    {
                        if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString())) <= Convert.ToDateTime(ApplicationFunction.mmddyyyy(gstDate.Trim())))
                            return;
                        else
                        {
                            ShowMessage("GR created pre-GST, so date cannot be greater than GST-Date.");
                            txtGRDate.Text = GrRestrictDate.Value;
                        }
                    }
                }
                HideShowTaxFields();
                netamntcal();
            }
        }
        //Upadhyay #GST
        private void HideShowTaxFields()
        {
            if (txtGRDate.Text != "")
            {
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                {
                    pnlServiceTax.Visible = false;
                    pnlAdditionTax.Visible = false;

                    int gstType = GetGSTType();
                    if (gstType == 1)
                    {
                        pnlIsStateGST.Visible = true;
                        pnlOutStateGST.Visible = false;
                    }
                    else if (gstType == 2)
                    {
                        pnlIsStateGST.Visible = false;
                        pnlOutStateGST.Visible = true;
                    }
                    GetGSTValues();
                }
                else
                {
                    pnlServiceTax.Visible = true;
                    pnlAdditionTax.Visible = true;
                    pnlIsStateGST.Visible = false;
                    pnlOutStateGST.Visible = false;
                }
            }
        }

        //Upadhyay #GST
        private string GetGSTDate()
        {
            DateTime gstDate;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                gstDate = (from i in db.tblUserPrefs select i.GST_Date).FirstOrDefault();
                return gstDate.ToString("dd-MM-yyyy");
            }
        }

        //Upadhyay #GST
        private void GetGSTValues()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            if (hidSGSTPer.Value == "")
            hidSGSTPer.Value = Convert.ToString(objGrprepDAL.SelectSGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            if (hidCGSTPer.Value == "")
            hidCGSTPer.Value = Convert.ToString(objGrprepDAL.SelectCGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            if (hidIGSTPer.Value == "")
            hidIGSTPer.Value = Convert.ToString(objGrprepDAL.SelectIGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            //Presently GST Cess not being used
            hidGSTCessPer.Value = "0";
        }

        //Upadhyay #GST
        public int GetGSTType()
        {
            if (txtGRDate.Text != "")
            {
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                //if (Convert.ToDateTime(txtGRDate.Text) >= GetGSTDate())
                {
                    if (Convert.ToString(ddlToCity.SelectedValue) != "" && ddlToCity.SelectedValue != "0")
                    {
                        string compStateName = GetCompStateIdno();
                        string toStateName = GetStateByCityId(Convert.ToInt64(ddlToCity.SelectedValue));
                        hidToStateName.Value = toStateName;
                        txtToState.Text = toStateName;
                        if (toStateName.Trim().ToLower() == compStateName.Trim().ToLower())
                            hidGstType.Value = "1";
                        else
                            hidGstType.Value = "2";
                        return Convert.ToInt32(hidGstType.Value);
                    }
                }
            }
            return 0;
        }

        private string GetStateByCityId(long CityId)
        {
            hidToStateIdno.Value = "";
            hidToStateName.Value = "";
            string toState = String.Empty;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                toState = (from s in db.tblStateMasters
                               join c in db.tblCityMasters on s.State_Idno equals c.State_Idno
                               where c.City_Idno == CityId
                               select s.State_Name).SingleOrDefault();
            }
            return toState;
        }
        private Int64 GetPartyStateIdno(Int64 billPrtyIdno)
        {
            Int64? stateId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                stateId = (from i in db.AcntMasts where i.Acnt_Idno == billPrtyIdno select i.State_Idno).SingleOrDefault();
                return Convert.ToInt64(String.IsNullOrEmpty(stateId.ToString()) ? "0" : stateId.ToString());
            }
        }
        private string GetCompStateIdno()
        {
            string stateId = "0";
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                stateId = (from i in db.tblCompMasts select i.State_Idno).SingleOrDefault();
                return String.IsNullOrEmpty(stateId) ? "0" : stateId;
            }
        }

        public bool SendSMS(string Mobile, string msg)
        {
            if (Mobile != String.Empty && msg != String.Empty)
                try
                {
                    WebClient objWebClient;
                    string sBaseURL;
                    Stream objStreamData;
                    StreamReader objReader;
                    string sResult;
                    objWebClient = new WebClient();
                    DueAlignRepDAL obj = new DueAlignRepDAL();
                    var Comp = obj.SelectUserPref();
                    string UserName = Convert.ToString(DataBinder.Eval(Comp[0], "UserName"));
                    string Password = Convert.ToString(DataBinder.Eval(Comp[0], "Password"));
                    string SenderID = Convert.ToString(DataBinder.Eval(Comp[0], "SenderID"));
                    string ProfileID = Convert.ToString(DataBinder.Eval(Comp[0], "ProfileID"));
                    string AuthType = Convert.ToString(DataBinder.Eval(Comp[0], "AuthType"));
                    string AuthKey = Convert.ToString(DataBinder.Eval(Comp[0], "AuthKey"));
                    //string UserName = "cogximsms";
                    //string Password = "teamcogximsms";//This may vary api to api. like ite may be password, secrate key, hash etc
                    //string SenderID = "Cogxim";
                    sBaseURL = "http://globesms.in/sendhttp.php?user=" + UserName + "&password=" + Password + "&authkey=" + AuthKey + "&type=" + AuthType + "&mobiles=91" + Mobile + "&message=" + HttpUtility.UrlEncode(msg) + "&sender=" + SenderID + "&route=1";
                    objStreamData = objWebClient.OpenRead(sBaseURL);
                    objReader = new StreamReader(objStreamData);
                    sResult = objReader.ReadToEnd();
                    objStreamData.Close();
                    objReader.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            else
                return false;
        }

        private string GetMsg(int iGrNo, String GrDate, String ToCity, String FromCity)
        {
            return "Dear customer, your GR no. " + iGrNo + " dated " + GrDate + " has been generated.For futher queries plz contact " + base.CompPhone;
        }

        private string GetSenderMobileNumbers(Int64 SenderIdno)
        {
            GRPrepDAL obj = new GRPrepDAL();
            String strMobileNo = obj.GetPartyMobileNoBySenderIdno(SenderIdno);
            return strMobileNo;
        }

        public void GetPreferences()
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            var data = objDAL.GetUserPrefDetail();
            chkSendSMSOnGRSave.Checked = data.Send_GrSMS;
        }

        #endregion

        #region SelectedIndexChanged Event...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "2")
            {
                DivAmount.Visible = true;
                //txtFixedAmount.Text = "2000.00";
                txtrate.Enabled = false;
                //txtweight.Enabled = true;
                ddlType.Focus();
            }
            else
            {
                txtrate.Enabled = true;
                DivAmount.Visible = false;
                txtFixedAmount.Text = "0.00";
                ddlType.Focus();
            }

        }
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedValue != "2")
                {
                    DivAmount.Visible = false;
                }
                if (ddlToCity.SelectedIndex <= 0)
                {
                    this.ShowMessageErr("Please select To City."); ddlToCity.Focus();
                    return;
                }
                else if (ddlCityVia.SelectedIndex <= 0)
                {
                    this.ShowMessageErr("Please select To via City."); ddlCityVia.Focus();
                    return;
                }
                else
                {
                    FillRateWeightWiseRate();

                    if (Hiditruckcitywise.Value == "2")
                    {
                        if (Convert.ToInt32(ddlDateRange.SelectedValue) > 0)
                        {
                            if (txtGRDate.Text != "")
                            {
                                if (Convert.ToInt32(ddlFromCity.SelectedValue) > 0)
                                {
                                    if (Convert.ToInt32(ddlToCity.SelectedValue) > 0)
                                    {
                                        if (Convert.ToInt32(ddlTruckNo.SelectedValue) > 0)
                                        {
                                            if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
                                            {
                                                GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
                                                Int64 LorryType = 0;
                                                LorryType = obj.SelectLorryType(Convert.ToInt64(ddlTruckNo.SelectedValue));
                                                if (LorryType == 1)
                                                {
                                                    DivCommission.Visible = true;
                                                    DivCommissionUpdatebtn.Visible = true;
                                                    Double CommissionAmnt = 0;
                                                    DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
                                                    DateTime dtGRDate = strGrDate;
                                                    Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                                                    CommissionAmnt = obj.SelectCommission(string.IsNullOrEmpty(ddlItemName.SelectedValue) ? 0 : Convert.ToInt64(ddlItemName.SelectedValue), dtGRDate, string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(ddlToCity.SelectedValue) ? 0 : Convert.ToInt64(ddlToCity.SelectedValue), intYearIdno);
                                                    if (CommissionAmnt > 0)
                                                    {
                                                        txtItmCommission.Text = CommissionAmnt.ToString();
                                                        txtItmCommission.Enabled = false;
                                                        DivCommissionUpdatebtn.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        txtItmCommission.Text = "0.00";
                                                        DivCommission.Visible = true;
                                                        txtItmCommission.Enabled = true;
                                                        DivCommissionUpdatebtn.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    DivCommission.Visible = false;
                                                    DivCommissionUpdatebtn.Visible = false;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            this.ShowMessageErr("Please Select Truck No."); ddlTruckNo.Focus();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageErr("Please Select To City."); ddlToCity.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    this.ShowMessageErr("Please Select Loc[From]."); ddlFromCity.Focus();
                                    return;
                                }
                            }
                            else
                            {
                                this.ShowMessageErr("Please Select Date."); ddlToCity.Focus();
                                return;
                            }
                        }
                        else
                        {
                            this.ShowMessageErr("Please Select Year."); ddlToCity.Focus();
                            return;
                        }
                    }
                }
                DataTable dtTemp = (DataTable)ViewState["dt"];
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim())))
                        {
                            this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type.");
                            //this.ClearDetails();
                            ddlItemName.Focus();
                            return;
                        }
                        else
                        {
                            ddlunitname.SelectedIndex = 0; ddlRateType.SelectedIndex = 0; txtInstNo.Text = "";
                            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtdetail.Text = ""; txtrate.Text = "0.00";
                        }
                    }
                }
                //if (UserPrefGradeVal == false)
                ddlunitname.Focus();
                //else
                //  ddlItemGrade.Focus();
                if (IsWeight == true)
                {
                    ddlRateType.SelectedIndex = 1;
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAtFromCityChanged();
                GRPrepRetailerDAL objGR = new GRPrepRetailerDAL();
                var lststate = objGR.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
                if (lststate != null)
                {
                    HiddServTaxPer.Value = Convert.ToString(objGR.SelectServiceTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                    dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));

                    HiddSwachhBrtTaxPer.Value = Convert.ToString(objGR.SelectSwacchBrtTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                    dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
                    HiddKalyanTax.Value = Convert.ToString(objGR.SelectKalyanTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                }
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                ddlToCity.Focus();
                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate GR No.!");
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        txtGRNo.Focus(); txtGRNo.SelectText();
                        return;
                    }
                    else
                    {
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        ddlFromCity.Focus();
                        return;
                    }
                }
                else
                {
                    this.ShowMessageErr("GR No. can't be left blank.");
                    txtGRNo.Text = Convert.ToString(MaxGRNo);
                    txtGRNo.Focus(); txtGRNo.SelectText();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }
        protected void ddlRcptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddlGRType.SelectedValue == "1") && (ddlRcptType.SelectedIndex > 0))
                {
                    txtInstNo.Text = ""; txtInstNo.Enabled = false; //rfvinstno.Enabled = false; rfvddlcustbank.Enabled = false;
                    txtInstDate.Enabled = false; ddlcustbank.Enabled = false; txtInstDate.Text = "";
                    BindDropdownDAL obj = new BindDropdownDAL();
                    DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32(ddlRcptType.SelectedValue), ApplicationFunction.ConnectionString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                        if (intAcnttype == 4)
                        {

                            txtInstNo.Enabled = true; rfvinstno.Enabled = true;
                            txtInstDate.Enabled = true; rfvinstdate.Enabled = true;
                            ddlcustbank.Enabled = true; rfvddlcustbank.Enabled = true;

                        }
                        else
                        {
                            txtInstNo.Enabled = false; rfvinstno.Enabled = false;
                            txtInstDate.Enabled = false; txtInstDate.Text = ""; rfvinstdate.Enabled = false;
                            ddlcustbank.Enabled = false; rfvddlcustbank.Enabled = false;

                        }
                    }
                }
                else
                {
                    txtInstNo.Enabled = false; rfvinstno.Enabled = false;
                    txtInstDate.Enabled = false; rfvinstdate.Enabled = false;
                    ddlcustbank.Enabled = false; rfvddlcustbank.Enabled = false;
                }
                ddlRcptType.Focus();
            }
            catch (Exception Ex)
            { }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }



        #endregion

        #region GridCommandEvent ......
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    ddlunitname.SelectedValue = Convert.ToString(drs[0]["Unit_Idno"]);
                    ddlRateType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["RateType_Idno"]) == "" ? 0 : drs[0]["RateType_Idno"]);
                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtDimension.Text = string.IsNullOrEmpty(Convert.ToString(drs[0]["Dimension"])) ? "" : Convert.ToString(drs[0]["Dimension"]);
                    txtCFT.Text = string.IsNullOrEmpty(Convert.ToString(drs[0]["CFT"])) ? "" : Convert.ToString(drs[0]["CFT"]);
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Ch_Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Ch_Weight"])));
                    txtActWeight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Act_Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Act_Weight"])));
                    txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Item_Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Item_Rate"])));
                    txtdetail.Text = Convert.ToString(drs[0]["Detail"]);
                    txtPacketNo.Text = Convert.ToString(drs[0]["Packet_No"]);

                    hidratetype.Value = Convert.ToString(Convert.ToString(drs[0]["RateType_Idno"]) == "" ? 0 : drs[0]["RateType_Idno"]);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);

                    ddlItemName.Focus();
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Idno"], rw["Item_Name"], rw["Unit_Idno"], rw["Unit_Name"], rw["RateType_Idno"], rw["Rate_Type"],
                                                                 rw["Quantity"], rw["Dimension"], rw["CFT"], rw["Ch_Weight"], rw["Act_Weight"], rw["Item_Rate"], rw["Amount"], rw["Detail"], rw["Packet_No"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
                this.ShowMessage("Data deleted successfully");
            }
        }

        #endregion

        #region Button Click Event...

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ddlToCity.SelectedValue == "0")
            {
                this.ShowMessageErr("Please Select to city first.");
                return;
            }
            //if (ddlType.SelectedValue != "2")
            //{
            //    if (hidTBBType.Value == "True")
            //    {
            //        if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            //        {
            //            ShowMessageErr("Rate should be greater than zero!");
            //            txtrate.Focus();
            //            return;
            //        }
            //    }
            //    else if ((hidTBBType.Value == "False") && (Convert.ToInt32(ddlGRType.SelectedValue) != 2))
            //    {
            //        if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            //        {
            //            ShowMessageErr("Rate should be greater than zero!");
            //            txtrate.Focus();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        txtrate.Text = "0.00";
            //    }
            //}
            //else
            //{
            //    txtrate.Text = "0.00";
            //}
            if (hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                int RateType = 0; double Quantity = 0; double RateAmnt = 0; double WeightCh = 0; double WeightAct = 0; double Amount = 0; int RowNo = 0; string Dimension; int CFT = 0;
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Item_Idno"] = ddlItemName.SelectedValue;
                        dtrow["Item_Name"] = ddlItemName.SelectedItem.Text;
                        dtrow["Unit_Idno"] = ddlunitname.SelectedValue;
                        dtrow["Unit_Name"] = ddlunitname.SelectedItem.Text;
                        dtrow["RateType_Idno"] = ddlRateType.SelectedValue;
                        RateType = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? 0 : Convert.ToInt32(ddlRateType.SelectedValue);
                        dtrow["Rate_Type"] = ddlRateType.SelectedItem.Text;
                        dtrow["Quantity"] = txtQuantity.Text.Trim();
                        Quantity = string.IsNullOrEmpty(txtQuantity.Text.Trim().Replace(",", "")) ? 0 : Convert.ToDouble(txtQuantity.Text.Trim().Replace(",", ""));
                        dtrow["Dimension"] = txtDimension.Text.Trim();
                        Dimension = string.IsNullOrEmpty(txtDimension.Text.Trim().Replace(",", "")) ? "" : Convert.ToString(txtDimension.Text.Trim().Replace(",", ""));
                        dtrow["CFT"] = txtCFT.Text.Trim();
                        CFT = string.IsNullOrEmpty(txtCFT.Text.Trim().Replace(",", "")) ? 0 : Convert.ToInt16(txtCFT.Text.Trim().Replace(",", ""));
                        dtrow["Ch_Weight"] = txtweight.Text.Trim();
                        WeightCh = string.IsNullOrEmpty(txtweight.Text.Trim().Replace(",", "")) ? 0 : Convert.ToDouble(txtweight.Text.Trim().Replace(",", ""));
                        dtrow["Act_Weight"] = txtActWeight.Text.Trim();
                        WeightAct = string.IsNullOrEmpty(txtActWeight.Text.Trim().Replace(",", "")) ? 0 : Convert.ToDouble(txtActWeight.Text.Trim().Replace(",", ""));
                        dtrow["Item_Rate"] = txtrate.Text.Trim();
                        RateAmnt = string.IsNullOrEmpty(txtrate.Text.Trim().Replace(",", "")) ? 0 : Convert.ToDouble(txtrate.Text.Trim().Replace(",", ""));
                        if (RateType > 0)
                        {
                            if (RateType == 1)
                            {
                                Amount = Quantity * RateAmnt;
                            }
                            else if (RateType == 2)
                            {
                                Amount = WeightCh * RateAmnt;
                            }
                        }
                        dtrow["Amount"] = Amount;
                        dtrow["Detail"] = txtdetail.Text.Trim();
                        dtrow["Packet_No"] = string.IsNullOrEmpty(Convert.ToString(txtPacketNo.Text.Trim())) ? "0" : Convert.ToString(txtPacketNo.Text.Trim());
                    }
                }
            }
            else
            {
                if (ddlItemName.SelectedIndex == 0)
                {
                    this.ShowMessageErr("Please Select Item.");
                    return;
                }
                else if (ddlunitname.SelectedIndex == 0)
                {
                    this.ShowMessageErr("Please Select Unit.");
                    return;
                }
                else if (ddlRateType.SelectedIndex == 0)
                {
                    this.ShowMessageErr("Please Select Rate.");
                    return;
                }
                else
                {
                    dtTemp = (DataTable)ViewState["dt"];

                    int RateType = 0; double Quantity = 0; double RateAmnt = 0; double WeightCh = 0; double WeightAct = 0; double Amount = 0;
                    Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                    int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                    string ItemIdno = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                    string ItemName = string.IsNullOrEmpty(ddlItemName.SelectedItem.Text) ? "" : (ddlItemName.SelectedItem.Text);
                    string Unitidno = string.IsNullOrEmpty(ddlunitname.SelectedValue) ? "0" : (ddlunitname.SelectedValue);
                    string UnitName = string.IsNullOrEmpty(ddlunitname.SelectedItem.Text) ? "" : (ddlunitname.SelectedItem.Text);
                    string RatetypeIdno = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : (ddlRateType.SelectedValue);
                    RateType = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? 0 : Convert.ToInt32(ddlRateType.SelectedValue);
                    string RatetypeName = string.IsNullOrEmpty(ddlRateType.SelectedItem.Text) ? "" : (ddlRateType.SelectedItem.Text);
                    string Qty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                    Quantity = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? 0.00 : Convert.ToDouble(txtQuantity.Text.Trim());
                    string Dimension = string.IsNullOrEmpty(txtDimension.Text.Trim()) ? "" : (txtDimension.Text.Trim());
                    int CFT = string.IsNullOrEmpty(txtCFT.Text.Trim()) ? 0 : Convert.ToInt32(txtCFT.Text.Trim());
                    string ChWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                    WeightCh = string.IsNullOrEmpty(txtweight.Text.Trim()) ? 0.00 : Convert.ToDouble(txtweight.Text.Trim());
                    string ActWeight = string.IsNullOrEmpty(txtActWeight.Text.Trim()) ? "0" : (txtActWeight.Text.Trim());
                    WeightAct = string.IsNullOrEmpty(txtActWeight.Text.Trim()) ? 0.00 : Convert.ToDouble(txtActWeight.Text.Trim());
                    string Rate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0" : (txtrate.Text.Trim());
                    RateAmnt = string.IsNullOrEmpty(txtrate.Text.Trim()) ? 0.00 : Convert.ToDouble(txtrate.Text.Trim());
                    if (RateType > 0)
                    {
                        if (RateType == 1)
                        {
                            Amount = Quantity * RateAmnt;
                        }
                        else if (RateType == 2)
                        {
                            Amount = WeightCh * RateAmnt;
                        }
                    }
                    string Amnt = Amount.ToString("N2");
                    //CalculateEdit();
                    //txtGrossAmnt.Text = Amnt;
                    string Details = string.IsNullOrEmpty(txtdetail.Text.Trim()) ? "" : (txtdetail.Text.Trim());
                    int RowNo = string.IsNullOrEmpty(txtRowAdd.Text.Trim()) ? 1 : Convert.ToInt32(txtRowAdd.Text.Trim());
                    Int64 PacketNo = string.IsNullOrEmpty(txtPacketNo.Text.Trim()) ? 0 : Convert.ToInt64(txtPacketNo.Text.Trim().Replace(",", ""));
                    for (int i = 0; i < RowNo; i++)
                    {
                        ApplicationFunction.DatatableAddRow(dtTemp, id, ItemIdno, ItemName, Unitidno, UnitName, RatetypeIdno, RatetypeName, Qty, Dimension, CFT, ChWeight, ActWeight, Rate, Amnt, Details, Convert.ToString(PacketNo));
                        PacketNo = PacketNo + 1; id = id + 1;
                        //CalculateEdit();
                    }
                    ViewState["dt"] = dtTemp;
                }
            }
            txtGrossAmnt.Text = Convert.ToDouble(dtTemp.Compute("Sum(Amount)", "")).ToString("N2");
            this.netamntcal();
            this.BindGridT();
            this.ClearDetails(0);
        }
        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            this.ClearDetails(1);
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Int64 CreatedBy = Convert.ToInt64(Session["UserIdno"]);
            Int64 intGrPrepIdno = 0;
            int intagainst = (RDbDirect.Checked == true) ? 1 : 2;
            Int64 ViaCity = (string.IsNullOrEmpty(ddlCityVia.SelectedValue) ? 0 : Convert.ToInt64(ddlCityVia.SelectedValue));
            Int32 intTaxPaidBy = string.IsNullOrEmpty(ddlSrvcetax.SelectedValue) ? 0 : Convert.ToInt32(ddlSrvcetax.SelectedValue);
            Int64 Rcpttype = (string.IsNullOrEmpty(ddlRcptType.SelectedValue) ? 0 : Convert.ToInt64(ddlRcptType.SelectedValue));
            string InstNo = (string.IsNullOrEmpty(txtInstNo.Text.Trim()) ? "" : Convert.ToString(txtInstNo.Text.Trim()));
            DateTime? InstDate = null; if (string.IsNullOrEmpty(txtInstDate.Text.Trim()) == false) { InstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text.Trim())); }
            Int64 CustBankIdno = (string.IsNullOrEmpty(ddlcustbank.SelectedValue) ? 0 : Convert.ToInt64(ddlcustbank.SelectedValue));
            string Dino = (string.IsNullOrEmpty(txtDelvNo.Text.Trim()) ? "" : Convert.ToString(txtDelvNo.Text.Trim()));
            string EGPNo = (string.IsNullOrEmpty(TxtEGPNo.Text.Trim()) ? "" : Convert.ToString(TxtEGPNo.Text.Trim()));
            string RefNo = (string.IsNullOrEmpty(txtRefNo.Text.Trim()) ? "" : Convert.ToString(txtRefNo.Text.Trim()));
            DateTime? RefDate = null; if (string.IsNullOrEmpty(txtRefDate.Text.Trim()) == false) { RefDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtRefDate.Text.Trim())); }
            string OrdrNo = (string.IsNullOrEmpty(txtOrderNo.Text.Trim()) ? "" : Convert.ToString(txtOrderNo.Text.Trim()));
            string FormNo = (string.IsNullOrEmpty(txtFromNo.Text.Trim()) ? "" : Convert.ToString(txtFromNo.Text.Trim()));
            Int64 AgentIDno = (string.IsNullOrEmpty(ddlParty.SelectedValue) ? 0 : Convert.ToInt64(ddlParty.SelectedValue));
            string ManualNo = (string.IsNullOrEmpty(txtManualNo.Text.Trim()) ? "" : Convert.ToString(txtManualNo.Text.Trim()));
            string ShipmentNo = (string.IsNullOrEmpty(txtshipment.Text.Trim()) ? "" : Convert.ToString(txtshipment.Text.Trim()));
            string ContainerNo1 = (string.IsNullOrEmpty(txtContainrNo.Text.Trim()) ? "" : Convert.ToString(txtContainrNo.Text.Trim()));
            string ContainerNo2 = (string.IsNullOrEmpty(txtContainrNo2.Text.Trim()) ? "" : Convert.ToString(txtContainrNo2.Text.Trim()));
            string SealNo1 = (string.IsNullOrEmpty(txtContainerSealNo.Text.Trim()) ? "" : Convert.ToString(txtContainerSealNo.Text.Trim()));
            string SealNo2 = (string.IsNullOrEmpty(txtContainerSealNo2.Text.Trim()) ? "" : Convert.ToString(txtContainerSealNo2.Text.Trim()));
            Int64 ContainerType = (string.IsNullOrEmpty(ddlContainerType.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerType.SelectedValue));
            Int64 ContainerSize = (string.IsNullOrEmpty(ddlContainerSize.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerSize.SelectedValue));
            string Portno = (string.IsNullOrEmpty(txtPortNum.Text.Trim()) ? "" : Convert.ToString(txtPortNum.Text.Trim()));
            Int64 ContainerTypeWay = (string.IsNullOrEmpty(ddlTypeI.SelectedValue) ? 0 : Convert.ToInt64(ddlTypeI.SelectedValue));
            string Remark = (string.IsNullOrEmpty(TxtRemark.Text.Trim()) ? "" : Convert.ToString(TxtRemark.Text.Trim()));
            string strGross = (Convert.ToString(txtGrossAmnt.Text)).Replace(",", "");
            Double GrossAmnt = string.IsNullOrEmpty(strGross) ? 0 : Convert.ToDouble(strGross);
            string strCartage = (Convert.ToString(txtCartage.Text)).Replace(",", "");
            Double CartageAmnt = string.IsNullOrEmpty(strCartage) ? 0 : Convert.ToDouble(strCartage);
            string strTot = (Convert.ToString(txtTotalAmnt.Text)).Replace(",", "");
            Double TotalAmnt = string.IsNullOrEmpty(strTot) ? 0 : Convert.ToDouble(strTot);
            string strSur = (Convert.ToString(txtSurchrge.Text)).Replace(",", "");
            Double SurchargeAmnt = string.IsNullOrEmpty(strSur) ? 0 : Convert.ToDouble(strSur);
            string strCom = (Convert.ToString(txtCommission.Text)).Replace(",", "");
            Double CommissionAmnt = string.IsNullOrEmpty(strCom) ? 0 : Convert.ToDouble(strCom);
            string strBilty = (Convert.ToString(txtBilty.Text)).Replace(",", "");
            Double BiltyAmnt = string.IsNullOrEmpty(strBilty) ? 0 : Convert.ToDouble(strBilty);
            string strWages = (Convert.ToString(txtWages.Text)).Replace(",", "");
            Double WagesAmnt = string.IsNullOrEmpty(strWages) ? 0 : Convert.ToDouble(strWages);
            string strPF = (Convert.ToString(txtPF.Text)).Replace(",", "");
            Double PFAmnt = string.IsNullOrEmpty(strPF) ? 0 : Convert.ToDouble(strPF);
            string strToll = (Convert.ToString(txtTollTax.Text)).Replace(",", "");
            Double TollTaxAmnt = string.IsNullOrEmpty(strToll) ? 0 : Convert.ToDouble(strToll);
            string strSubTotal = (Convert.ToString(txtSubTotal.Text)).Replace(",", "");
            Double SubTotalAmnt = string.IsNullOrEmpty(strSubTotal) ? 0 : Convert.ToDouble(strSubTotal);
            string strSer = (Convert.ToString(txtServTax.Text)).Replace("'", "");
            Double ServTaxAmnt = string.IsNullOrEmpty(strSer) ? 0 : Convert.ToDouble(strSer);
            string strSwach = (Convert.ToString(txtSwchhBhartTx.Text)).Replace(",", "");
            Double SwachhBhrtCessAmnt = string.IsNullOrEmpty(strSwach) ? 0 : Convert.ToDouble(strSwach);
            string strKrishi = (Convert.ToString(txtkalyan.Text)).Replace(",", "");
            Double KrishiAmnt = string.IsNullOrEmpty(strKrishi) ? 0 : Convert.ToDouble(strKrishi);
            string strTotPri = (Convert.ToString(txtTotItemPrice.Text)).Replace(",", "");
            Double TotPrice = string.IsNullOrEmpty(strTotPri) ? 0 : Convert.ToDouble(strTotPri);
            string strRound = (Convert.ToString(TxtRoundOff.Text)).Replace(",", "");
            Double RoundoffAmnt = string.IsNullOrEmpty(strRound) ? 0 : Convert.ToDouble(strRound);
            string strNetamnt = (Convert.ToString(txtNetAmnt.Text)).Replace(",", "");
            Double NetAmnt = string.IsNullOrEmpty(strNetamnt) ? 0 : Convert.ToDouble(strNetamnt);
            bool Modvat = Convert.ToBoolean((chkModvat.Checked == true) ? 1 : 0);
            Double dblTaxValid = string.IsNullOrEmpty(HiddServTaxValid.Value) ? 0 : Convert.ToDouble(HiddServTaxValid.Value);
            Double dblServTaxPerc = string.IsNullOrEmpty(txtServPer.Text) ? 0 : Convert.ToDouble(txtServPer.Text);
            Double dblSwcgBrtTaxPerc = string.IsNullOrEmpty(HiddSwachhBrtTaxPer.Value) ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value);
            Double dblKalyanTaxPer = string.IsNullOrEmpty(HiddKalyanTax.Value) ? 0 : Convert.ToDouble(HiddKalyanTax.Value);
            Int64 intType = string.IsNullOrEmpty(ddlType.SelectedValue) ? 1 : Convert.ToInt64(ddlType.SelectedValue);
            string strStcharge = (Convert.ToString(txtSTCharges.Text)).Replace(",", "");
            Double Stcharges = string.IsNullOrEmpty(strStcharge) ? 0 : Convert.ToDouble(strStcharge);
            //Upadhyay #GST
            //Amounts
            string strSGST_Amt = (Convert.ToString(txtSGSTAmount.Text)).Replace(",", "");
            Double dSGST_Amt = string.IsNullOrEmpty(strSGST_Amt) ? 0 : Convert.ToDouble(strSGST_Amt);
            string strCGST_Amt = (Convert.ToString(txtCGSTAMount.Text)).Replace(",", "");
            Double dCGST_Amt = string.IsNullOrEmpty(strCGST_Amt) ? 0 : Convert.ToDouble(strCGST_Amt);
            string strIGST_Amt = (Convert.ToString(txtIGSTAmount.Text)).Replace(",", "");
            Double dIGST_Amt = string.IsNullOrEmpty(strIGST_Amt) ? 0 : Convert.ToDouble(strIGST_Amt);
            //Presently GST Cess is not being used
            string strGSTCess = "0";
            Double dGSTCess_Amt = string.IsNullOrEmpty(strGSTCess) ? 0 : Convert.ToDouble(strGSTCess);
            //Percentage
            string strSGST_Per = (Convert.ToString((hidSGSTPer.Value == null || hidSGSTPer.Value == "") ? "0" : hidSGSTPer.Value)).Replace(",", "");
            Double dSGST_Per = string.IsNullOrEmpty(strSGST_Per) ? 0 : Convert.ToDouble(strSGST_Per);
            string strCGST_Per = (Convert.ToString((hidCGSTPer.Value == null || hidCGSTPer.Value == "") ? "0" : hidCGSTPer.Value)).Replace(",", "");
            Double dCGST_Per = string.IsNullOrEmpty(strCGST_Per) ? 0 : Convert.ToDouble(strCGST_Per);
            string strIGST_Per = (Convert.ToString((hidIGSTPer.Value == null || hidIGSTPer.Value == "") ? "0" : hidIGSTPer.Value)).Replace(",", "");
            Double dIGST_Per = string.IsNullOrEmpty(strIGST_Per) ? 0 : Convert.ToDouble(strIGST_Per);
            //Presently GST Cess is not being used            
            string strGSTCess_Per = "0";
            Double dGSTCess_Per = string.IsNullOrEmpty(strGSTCess_Per) ? 0 : Convert.ToDouble(strGSTCess_Per);
            int GST_Idno = Convert.ToInt32(hidGstType.Value == "" ? "0" : hidGstType.Value);
            double dblFixedAmount = 0;
            Boolean IsSMSSent = false;
            Boolean SendGRSMS = false;
            if (intType == 2) { dblFixedAmount = string.IsNullOrEmpty(txtFixedAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtFixedAmount.Text.Trim()); }
            DataTable dtdetl = new DataTable();
            dtdetl = (DataTable)ViewState["dt"];
            //using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            //  {
            try
            {

                if (grdMain.Rows.Count > 0)
                {
                    GRPrepRetailerDAL objGR = new GRPrepRetailerDAL();
                    Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                    MaxGRNo = objGR.MaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                    if (Convert.ToString(hidGRHeadIdno.Value) == "")
                    {
                        if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                        {
                            var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                            if (lst.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirmAtSave()", true);
                                return;
                            }
                        }
                        else
                        {
                            this.ShowMessageErr("GR No. can't be left blank.");
                            txtGRNo.Text = Convert.ToString(MaxGRNo);
                            txtGRNo.Focus(); txtGRNo.SelectText();
                            return;
                        }
                    }
                    if ((Convert.ToString(hidGRHeadIdno.Value) == "") || (Convert.ToString(hidGRHeadIdno.Value) == "0"))
                    {
                        intGrPrepIdno = objGR.Insert(Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())), Convert.ToString(txtPrefixNo.Text.Trim()),
                            string.IsNullOrEmpty(txtGRNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGRNo.Text.Trim()), intagainst, Convert.ToInt32(ddlGRType.SelectedValue), Convert.ToInt64(ddlSender.SelectedValue),
                            Convert.ToInt64(ddlReceiver.SelectedValue), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt64(ddlLocation.SelectedValue), ViaCity,
                            intTaxPaidBy, Rcpttype, InstNo, InstDate, CustBankIdno, Dino, EGPNo, RefNo, RefDate, OrdrNo, FormNo, Convert.ToString(txtconsnr.Text.Trim()), AgentIDno, ManualNo, Convert.ToInt64(ddlTranType.SelectedValue),
                            Convert.ToInt64(ddlTruckNo.SelectedValue), ShipmentNo, ContainerNo1, ContainerNo2, SealNo1, SealNo2, ContainerType, ContainerSize, Portno, ContainerTypeWay, Remark, GrossAmnt, CartageAmnt,
                            TotalAmnt, SurchargeAmnt, CommissionAmnt, BiltyAmnt, WagesAmnt, PFAmnt, TollTaxAmnt, SubTotalAmnt, ServTaxAmnt, SwachhBhrtCessAmnt, KrishiAmnt, TotPrice, RoundoffAmnt, NetAmnt, Modvat, dtdetl,
                            dblKalyanTaxPer, dblServTaxPerc, dblSwcgBrtTaxPerc, dblTaxValid, intType, dblFixedAmount, Convert.ToBoolean(hidTBBType.Value), Stcharges, CreatedBy,
                            dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Per, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per, GST_Idno);
                        if (intGrPrepIdno > 0)
                        {
                            GRPrepDAL objGrPrep = new GRPrepDAL();
                            SendGRSMS = objGrPrep.GetUserPref().Send_GrSMS;
                            if (SendGRSMS)
                            {
                                string strPhoneNo = GetSenderMobileNumbers(Convert.ToInt32(ddlSender.SelectedValue == "" ? "0" : ddlSender.SelectedValue));
                                if (strPhoneNo != String.Empty)
                                {
                                    IsSMSSent = SendSMS(strPhoneNo, GetMsg(Convert.ToInt32(txtGRNo.Text), Convert.ToDateTime(txtGRDate.Text).ToString("dd-MM-yyyy"), ddlToCity.SelectedItem.Text, ddlFromCity.SelectedItem.Text));
                                }
                                else
                                {
                                    IsSMSSent = false;
                                }
                            }
                            if (SendGRSMS && IsSMSSent)
                            {
                                this.ShowMessage("Record saved with SMS sent to sender.");
                            }
                            else if (SendGRSMS && !IsSMSSent)
                            {
                                this.ShowMessage("Record saved but SMS could not be sent.");
                            }
                            else
                            {
                                this.ShowMessage("Record saved.");
                            }
                            //tScope.Complete();
                            objGR = null;
                        }
                        else
                        {
                            this.ShowMessageErr("Record not saved.");
                        }
                        //tScope.Dispose();
                    }
                    else
                    {
                        intGrPrepIdno = objGR.Update(Convert.ToInt64(hidGRHeadIdno.Value), Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())), Convert.ToString(txtPrefixNo.Text.Trim()),
                            string.IsNullOrEmpty(txtGRNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGRNo.Text.Trim()), intagainst, Convert.ToInt32(ddlGRType.SelectedValue), Convert.ToInt64(ddlSender.SelectedValue), Convert.ToInt64(ddlReceiver.SelectedValue), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue),
                            Convert.ToInt64(ddlLocation.SelectedValue), ViaCity, intTaxPaidBy, Rcpttype, InstNo, InstDate, CustBankIdno, Dino, EGPNo, RefNo, RefDate, OrdrNo, FormNo, Convert.ToString(txtconsnr.Text.Trim()), AgentIDno,
                            ManualNo, Convert.ToInt64(ddlTranType.SelectedValue), Convert.ToInt64(ddlTruckNo.SelectedValue), ShipmentNo, ContainerNo1, ContainerNo2, SealNo1, SealNo2, ContainerType,
                            ContainerSize, Portno, ContainerTypeWay, Remark, GrossAmnt, CartageAmnt, TotalAmnt, SurchargeAmnt, CommissionAmnt, BiltyAmnt, WagesAmnt, PFAmnt,
                            TollTaxAmnt, SubTotalAmnt, ServTaxAmnt, SwachhBhrtCessAmnt, KrishiAmnt, TotPrice, RoundoffAmnt, NetAmnt, Modvat, dtdetl, dblKalyanTaxPer, dblServTaxPerc,
                            dblSwcgBrtTaxPerc, dblTaxValid, intType, dblFixedAmount, Convert.ToBoolean(hidTBBType.Value), Stcharges, CreatedBy,
                            dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Per, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per, GST_Idno);
                        if (intGrPrepIdno > 0)
                        {
                            this.ShowMessage("Record updated.");
                            //tScope.Complete();
                            objGR = null;
                        }
                        else
                        {
                            this.ShowMessageErr("Record not updated.");
                        }
                        //tScope.Dispose();
                        Int64 iMaxGRNo = 0;
                        GRPrepRetailerDAL objGRDAL = new GRPrepRetailerDAL();
                        iMaxGRNo = objGRDAL.MaxNo(Convert.ToInt32(ddlDateRange.SelectedValue),
                                                        Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 :
                                                        Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                        txtGRNo.Text = Convert.ToString(iMaxGRNo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            this.ClearAtFromCityChanged();
            this.ClearDetails(1);
            //}
        }
        protected void lnkbtnNew_Click1(object sender, EventArgs e)
        {
            Response.Redirect("GRPrepRetailer.aspx");
        }
        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void imgComUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ddlDateRange.SelectedValue) > 0)
            {
                if (txtGRDate.Text != "")
                {
                    if (Convert.ToInt32(ddlFromCity.SelectedValue) > 0)
                    {
                        if (Convert.ToInt32(ddlToCity.SelectedValue) > 0)
                        {
                            if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
                            {
                                DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
                                DateTime dtGRDate = strGrDate;
                                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                                Int64 intComHeadIdno = 0;
                                GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
                                intComHeadIdno = obj.InsertInComMaster(string.IsNullOrEmpty(ddlItemName.SelectedValue) ? 0 : Convert.ToInt64(ddlItemName.SelectedValue), "1", dtGRDate, string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(ddlToCity.SelectedValue) ? 0 : Convert.ToInt64(ddlToCity.SelectedValue), intYearIdno, Convert.ToDouble(string.IsNullOrEmpty(txtItmCommission.Text) ? 0 : Convert.ToDouble(txtItmCommission.Text)));
                                if (intComHeadIdno > 0)
                                {
                                    txtItmCommission.Enabled = false;
                                    DivCommissionUpdatebtn.Visible = false;
                                    this.ShowMessage("Commission save successfully in Master.");

                                }
                                else
                                {
                                    txtItmCommission.Enabled = true;
                                    DivCommissionUpdatebtn.Visible = true;
                                    this.ShowMessage("Commission not Save in Master!");

                                }
                            }
                            else
                            {
                                this.ShowMessageErr("Please Select Item."); ddlItemName.Focus();
                                return;
                            }

                        }
                        else
                        {
                            this.ShowMessageErr("Please Select To City."); ddlToCity.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.ShowMessageErr("Please Select Loc[From]."); ddlFromCity.Focus();
                        return;
                    }
                }
                else
                {
                    this.ShowMessageErr("Please Select Gr Date."); txtGRDate.Focus();
                    return;
                }
            }
            else
            {
                this.ShowMessageErr("Please Select Date Range."); ddlDateRange.Focus();
                return;
            }
            //CvtxtRate.Enabled = false;
        }
        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Gr"] != null)
            {
                //txtGRDate.Enabled = false;
                this.Populate(Convert.ToInt32(Request.QueryString["Gr"]));
            }
            else
            {
                Response.Redirect("GRPrepRetailer.aspx");
            }
        }

        protected void lnkBtnSaveUserPref_OnClick(object sender, EventArgs e)
        {
            GRPrepDAL objDAL = new GRPrepDAL();
            tblUserPref UsrPref = new tblUserPref();
            UsrPref.Send_GrSMS = chkSendSMSOnGRSave.Checked;
            int ReturnValue = objDAL.SaveUserPrefDetail(UsrPref);
            if (ReturnValue == 1)
            {
                ShowMessage("User preference have been saved successfully.");
            }
            else
            {
                ShowMessageErr("Something went wrong, please contact your administer.");
            }
            GetPreferences();
        }

        #endregion

        #region "Text Changed Event"
        //#GST
        protected void GST_TextChanged(object sender, EventArgs e)
        {
            double igstPer = Convert.ToDouble(txtIGSTPercent.Text == "" ? "0" : txtIGSTPercent.Text);
            double sgstPer = Convert.ToDouble(txtSGSTPercent.Text == "" ? "0" : txtSGSTPercent.Text);
            double cgstPer = Convert.ToDouble(txtCGSTPercent.Text == "" ? "0" : txtCGSTPercent.Text);
            hidSGSTPer.Value = sgstPer.ToString();
            hidCGSTPer.Value = cgstPer.ToString();
            hidIGSTPer.Value = igstPer.ToString();
            netamntcal();
            //GetGSTType();
            //if (hidGstType.Value == "1")
            //{
            //    txtSGSTAmount.Text 
            //}
            //else if (hidGstType.Value == "2")
            //{

            //}
        }

        protected void txtGrossAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtCartage.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtCartage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                //Selectuserpref();
                txtCartage.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtSTCharges_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtSTCharges.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        protected void txtTotalAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtSurchrge_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtSurchrge.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtCommission_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtCommission.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtBilty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtBilty.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtWages_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtWages.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtPF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtPF.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtTollTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal();
                txtTollTax.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal(); txtSubTotal.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtServTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal(); txtServTax.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtSwchhBhartTx_TextChanged(object sender, EventArgs e)
        {
            try { netamntcal(); txtSwchhBhartTx.Focus(); }
            catch (Exception Ex) { }
        }

        protected void txtNetAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                netamntcal(); txtNetAmnt.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtQuantity_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                CalculateEdit();
                txtQuantity.Focus();
            }
            catch (Exception Ex) { }
        }

        protected void txtGRNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //  userpref();
                GRPrepRetailerDAL objGR = new GRPrepRetailerDAL();
                //string GRfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate GR No.!");
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        txtGRNo.Focus(); txtGRNo.SelectText();
                        return;
                    }
                    if ((txtGRNo.Text != Convert.ToString(MaxGRNo)) && (GrIdnos != Convert.ToInt32(txtGRNo.Text)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirm()", true);
                    }
                }
                else
                {
                    this.ShowMessageErr("GR No. can't be left blank.");
                    txtGRNo.Text = Convert.ToString(MaxGRNo);
                    txtGRNo.Focus(); txtGRNo.SelectText();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }

        protected void txtFixedAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtFixedAmount.Text == "0.00")
            {
                this.ShowMessageErr("Please enter amount");
            }
            else
            {
                txtGrossAmnt.Text = txtFixedAmount.Text;
                netamntcal();
            }
        }

        //Upadhyay #GST
        public void ToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGSTType();
            netamntcal();
        }
        #endregion

        #region "Print"

        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0")
            {
                ShowMessageErr("Please Select From City for Last Print.");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
            }
        }

        protected void lnkwithoutamount_Click(object sender, EventArgs e)
        {
            hidPages.Value = ddlPage.SelectedValue;
            GRPrepRetailerDAL objGRDAL = new GRPrepRetailerDAL();
            tblUserPref objuserpref = objGRDAL.selectuserpref();
            if (Request.QueryString["Gr"] == null)
            {
                Int64 iMaxGRIdno = objGRDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
                if (iMaxGRIdno > 0)
                {
                    if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                    {
                        chkbit = 1;
                        PrintGRPrep(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                    {
                        string url = "JainGrPrint.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = false; divAmntvalue.Visible = false;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);

                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
            else
            {
                if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                {
                    chkbit = 1; PrintGRPrep(Convert.ToInt32(Request.QueryString["Gr"]));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                {
                    string url = "JainGrPrint.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    //PrintGRPrepJainBulk(Convert.ToInt32(Request.QueryString["Gr"]));
                    //divAmntHead.Visible = false; divAmntvalue.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                {
                    string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }
        }

        protected void lnkwithamount_Click(object sender, EventArgs e)
        {
            GRPrepRetailerDAL objGRDAL = new GRPrepRetailerDAL();
            tblUserPref objuserpref = objGRDAL.selectuserpref(); hidPages.Value = ddlPage.SelectedValue;
            if (Request.QueryString["Gr"] == null)
            {
                Int64 iMaxGRIdno = objGRDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
                if (iMaxGRIdno > 0)
                {
                    if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                    {
                        chkbit = 2;
                        PrintGRPrep(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                    {
                        string url = "JainGrPrint.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
            else
            {
                if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                {
                    chkbit = 2; PrintGRPrep(Convert.ToInt32(Request.QueryString["Gr"]));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                {
                    string url = "JainGrPrint.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    //PrintGRPrepJainBulk(Convert.ToInt32(Request.QueryString["Gr"]));
                    //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                {
                    string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }

        }

        private void PrintGRPrep(Int64 GRHeadIdno)
        {
            visiFalse.Visible = false;
            Repeater obj = new Repeater();

            GRPrepRetailerDAL obj1 = new GRPrepRetailerDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
            //if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
            //{
            //    imgLogoShow.Visible = true;
            //    byte[] img = hiduserpref.Logo_Image;
            //    string base64String = Convert.ToBase64String(img, 0, img.Length);
            //    imgLogoShow.ImageUrl = "data:image/png;base64," + base64String;
            //}
            //else
            //{
            //    imgLogoShow.Visible = false;
            //    imgLogoShow.ImageUrl = "";
            //}
            //if (Convert.ToString(hiduserpref.Terms) == "" && Convert.ToString(hiduserpref.Terms1) == "")
            //{
            //    lblTerms.Visible = false;
            //    lblterms1.Visible = false;

            //}
            //else
            //{
            //    lblTerms.Visible = true;
            //    lblterms1.Visible = true;

            //    lblTerms.Text = "'" + hiduserpref.Terms + "'";
            //    lblterms1.Text = hiduserpref.Terms1;
            //}
            if (Convert.ToString(HidsRenWages.Value) != "")
            {
                lblUnloadingPrint.Text = Convert.ToString(HidsRenWages.Value);
            }
            else
            {
                lblUnloadingPrint.Text = "Wages";
            }
            if (Convert.ToString(hidRenamePF.Value) != "")
            {
                lblCollChargePrint.Text = Convert.ToString(hidRenamePF.Value);
            }
            else
            {
                lblCollChargePrint.Text = "PF";
            }
            if (Convert.ToString(hidRenameToll.Value) != "")
            {
                lblDelChargesPrint.Text = Convert.ToString(hidRenameToll.Value);
            }
            else
            {
                lblDelChargesPrint.Text = "Toll Tax";
            }
            if (Convert.ToString(HidsRenCartage.Value) != "")
            {
                lblFOVPrint.Text = Convert.ToString(HidsRenCartage.Value);
            }
            else
            {
                lblFOVPrint.Text = "Cartage";
            }
            if (Convert.ToString(HidRenCommission.Value) != "")
            {
                lblOctroiPrint.Text = Convert.ToString(HidRenCommission.Value);
            }
            else
            {
                lblOctroiPrint.Text = "Commission";
            }
            if (Convert.ToString(HidRenBilty.Value) != "")
            {
                lblDemuChargesPrint.Text = Convert.ToString(HidRenBilty.Value);
            }
            else
            {
                lblDemuChargesPrint.Text = "Bilty";
            }
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = "", CompDesc = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = ""; string RegNo = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            CompDesc = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            RegNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Reg_No"]);
            CompGSTIN = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            // lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            lblcompany.Text = CompName;
            lblOWNER.Text = "Subject to " + Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]) + " Jurisdiction only";
            if (CompDesc == "")
            {
                lblCompDesc.Visible = false;
            }
            else
            {
                lblCompDesc.Text = Convert.ToString(CompDesc.Trim().Replace("@", "<br/>") + "<br/>");
            }
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = "FAX No.:" + FaxNo;
                lblCompFaxNo.Visible = true;
            }
            if (RegNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = RegNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (CompGSTIN == "")
            {
                lblCompGSTINNo.Visible = false; lblCompGSTINNoValue.Visible = false;
            }
            else
            {
                lblCompGSTINNoValue.Text = CompGSTIN;
                lblCompGSTINNo.Visible = true; lblCompGSTINNoValue.Visible = true;
            }
            if (PanNo == "")
            {
                lblPanNo.Visible = false; lbltxtPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblPanNo.Visible = true; lbltxtPanNo.Visible = true;
            }
            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRRetailPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                hidGstType.Value = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GST_Idno"]);
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["GRRet_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                lblValueViaCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblSubtotalP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SubTotal_Amnt"]);
                lblUnitP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Unit"]);
                lblDetailP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Item_Desc"]);
                lblRefNoP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"]);
                lblRefDateP.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Ref_Date"]).ToString("dd-MM-yyyy");
                lblTotP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"]));
                lblQtP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Qty"]);
                lblNoPckg.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Qty"]);
                lblDimP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Dimension"]);
                lblChWght.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ch_Weight"]);
                lblPCFT.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CFT"]);
                lblTCFT.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CFT"]);
                lblActWght.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Act_Weight"]);
                lblRateRs.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Item_Rate"]));
                labFreightRs.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"]));
                labSurR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcharge_Amnt"]));
                labHamR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"]));
                labFOVR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartage_Amnt"]));
                labCollChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]));
                labDelChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]));
                labOctroiR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Commission_Amnt"]));
                labDemChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"]));
                labTotP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SubTotal_Amnt"]));
                labGTotR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Net_Amount"]));
                labSTR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Stcharg_Amnt"]));
                lblRemP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);
                lblLSTP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lst_No"]);
                lblCSTP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cst_No"]);
                lblLSTP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lst_No1"]);
                lblCSTP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cst_No1"]);
                lblMNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Manual_No"]);
                lblPhP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CNorMobile"]);
                lblPhP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CNeeMobile"]);

                //GET GST value for print
                int gstType = Convert.ToInt32(hidGstType.Value);
                if (gstType > 0)
                {
                    if (gstType == 1)
                    {
                        pnlCGST.Visible = true;
                        labSerTax.Text = "SGST @";
                        labSerTaxR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"]));
                        labCGSTValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["CGST_Amt"]));
                    }
                    else if (gstType == 2)
                    {
                        pnlCGST.Visible = false;
                        labSerTax.Text = "IGST @";
                        labSerTaxR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["IGST_Amt"]));
                    }
                }
                else
                {
                    pnlCGST.Visible = false;
                    labSerTaxR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]) == "")
                {
                    lbltxtagent.Visible = false; lblAgent.Visible = false;
                }
                else
                {
                    lblAgent.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]); lbltxtagent.Visible = true; lblAgent.Visible = true;
                }


                lblConsignorName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsignorAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"])) == true)
                {
                    Label5.Visible = false; lblConsigneeTin.Visible = false;
                }
                else
                {
                    lblConsigneeTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"]);
                }
                lblConsigeeName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsigneeAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"])) == true)
                { lblPrtyTinTxt.Visible = false; lblConsignorTin.Visible = false; }
                else
                { lblConsignorTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"]); }

                lblLorryNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true)
                {
                    lblOrderNo.Visible = false; lblOrderNoVal.Visible = false;
                }
                else { lblOrderNo.Visible = true; lblOrderNoVal.Visible = true; lblOrderNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true)
                {
                    lblFormNo.Visible = false; lblFormNoVal.Visible = false;
                }
                else { lblFormNo.Visible = true; lblFormNoVal.Visible = true; lblFormNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Form_No"].ToString(); }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                {
                    chkGoods.Checked = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "2")
                {
                    chkCNor.Checked = true;
                }
                else
                {
                    chkCNee.Checked = true;
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRRet_Typ"]) == "1")
                {
                    chkPaid.Checked = true;
                    lblCheP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Inst_No"]);
                    lblInsDate.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Inst_Date"]);
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRRet_Typ"]) == "2")
                {
                    chkToBeld.Checked = true;
                }
                else
                {
                    chkToPay.Checked = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Container_No1"])) == true)
                {
                    tr1.Visible = false;
                }
                else
                {
                    tr1.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["EGPNo"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true)
                {
                    tr2.Visible = false;
                }
                else
                {
                    tr2.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"])) == true)
                {
                    tr3.Visible = false;
                }
                else
                {
                    tr3.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"])) == true)
                {
                    tr4.Visible = false;
                }
                else
                {
                    tr4.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"])) == true)
                {
                    lblNameShipmentno.Visible = false; lblShipmentNo.Visible = false;
                }
                else { lblNameShipmentno.Visible = true; lblShipmentNo.Visible = true; lblShipmentNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Container_No1"])) == true)
                {
                    lblNameContnrNo.Visible = false; lblContainerNo.Visible = false;
                }
                else { lblNameContnrNo.Visible = true; lblContainerNo.Visible = true; lblContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Container_No1"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"])) == true)
                {
                    lblNameCntnrSize.Visible = false; lblContainerSize.Visible = false;
                }
                else
                { lblNameCntnrSize.Visible = true; lblContainerSize.Visible = true; lblContainerSize.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ConsigName"])) == true)
                {
                    lblConsName.Visible = false; lblvalConsName.Visible = false;
                }
                
                else
                { lblConsName.Visible = true; lblvalConsName.Visible = true; lblvalConsName.Text = dsReport.Tables["GRPrint"].Rows[0]["ConsigName"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["EGPNo"])) == true)
                {
                    lblEGPNo.Visible = false; lblEGPNoval.Visible = false;
                }
                else
                { lblEGPNo.Visible = true; lblEGPNoval.Visible = true; lblEGPNoval.Text = dsReport.Tables["GRPrint"].Rows[0]["EGPNo"].ToString(); }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTINNo"])) == true)
                {
                    lblConsignerGSTINValue.Visible = false;
                    lblConsignerGSTINLabel.Visible = false;
                }
                else
                {
                    lblConsignerGSTINValue.Visible = true;
                    lblConsignerGSTINLabel.Visible = true;
                    lblConsignerGSTINValue.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTINNo"]);
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTINNo"])) == true)
                {
                    lblConsigneeGSTINLabel.Visible = false;
                    lblConsigneeGSTINValue.Visible = false;
                }
                else
                {
                    lblConsigneeGSTINLabel.Visible = true;
                    lblConsigneeGSTINValue.Visible = true;
                    lblConsigneeGSTINValue.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTINNo"]);
                }
                if (Convert.ToBoolean(dsReport.Tables["GRPrint"].Rows[0]["MODVAT_CPY"]) == true)
                { lblModvatcpy.Text = "Yes"; }
                else
                { lblModvatcpy.Text = "No"; }

                //Ref No.
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true)
                {
                    lblRefNo.Visible = false; lblrefnoval.Visible = false;
                }
                else
                { lblRefNo.Visible = true; lblrefnoval.Visible = true; lblRefNo.Text = lblrefrename.Text; lblrefnoval.Text = dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString(); }
                //
                lblTranTypeP.Text = dsReport.Tables["GRPrint"].Rows[0]["Tran_Type"].ToString();
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == "0")
                {
                    lblTotItem.Visible = false; lblTotItemValue.Visible = false;
                }
                else
                {
                    lblTotItem.Visible = true; lblTotItemValue.Visible = true; lblTotItemValue.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"]).ToString("N2");
                }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true)
                {
                    lblNameContnrType.Visible = false; lblCntnrType.Visible = false;
                }
                else
                { lblNameContnrType.Visible = true; lblCntnrType.Visible = true; lblCntnrType.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"])) == true)
                {
                    lblNameSealNo.Visible = false; lblSealNo.Visible = false;
                }
                else { lblNameSealNo.Visible = true; lblSealNo.Visible = true; lblSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true)
                {
                    lblDinNoText.Visible = false; lblDinNo.Visible = false;
                }
                else { lblDinNoText.Visible = true; lblDinNo.Visible = true; lblDinNo.Text = dsReport.Tables["GRPrint"].Rows[0]["DI_NO"].ToString(); }

                //.........................
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"])) == true)
                { trRemarks.Visible = false; }
                else
                { lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]); }
                if (Convert.ToString(hiduserpref.Terms_Con_Retailer) != "")
                { lblTnCGR.Text = Convert.ToString(hiduserpref.Terms_Con_Retailer); }
                else { trTnC.Visible = false; }
                lblOctroi.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Commission_Amnt"]));
                if (lblOctroi.Text != "")
                {
                    dcmsn = Convert.ToDouble(lblOctroi.Text);
                }
                else
                {
                    dcmsn = 0;
                }
                lblDemurrage.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"]));
                if (lblDemurrage.Text != "")
                {
                    dblty = Convert.ToDouble(lblDemurrage.Text);
                }
                else
                {
                    dblty = 0;
                }
                lblFOV.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartage_Amnt"]));
                if (lblFOV.Text != "")
                {
                    dcrtge = Convert.ToDouble(lblFOV.Text);
                }
                else
                {
                    dcrtge = 0;
                }
                lblSurchargeP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcharge_Amnt"]));
                if (lblSurchargeP.Text != "")
                {
                    dsuchge = Convert.ToDouble(lblSurchargeP.Text);
                }
                else
                {
                    dsuchge = 0;
                }
                //if (Convert.ToString(HidsRenWages.Value) != "")
                //{
                //    lblwages.Text = Convert.ToString(HidsRenWages.Value);
                //}
                //else
                //{
                //    lblwages.Text = "Wages";
                //}
                //if (Convert.ToString(hidRenamePF.Value) != "")
                //{
                //    lblPFAmnt.Text = Convert.ToString(hidRenamePF.Value);
                //}
                //else
                //{
                //    lblPFAmnt.Text = "PF";
                //}
                //if (Convert.ToString(hidRenameToll.Value) != "")
                //{
                //    lblTollTax.Text = Convert.ToString(hidRenameToll.Value);
                //}
                //else
                //{
                //    lblTollTax.Text = "Toll Tax";
                //}
                lblUnloading.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"]));
                if (lblUnloading.Text != "")
                {
                    dwges = Convert.ToDouble(lblUnloading.Text);
                }
                else
                {
                    dwges = 0;
                }
                lblCollectionCharges.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]));
                if (lblCollectionCharges.Text != "")
                {
                    dPF = Convert.ToDouble(lblCollectionCharges.Text);
                }
                else
                {
                    dPF = 0;
                }
                //if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                //{

                //    valuelblservtaxConsigner.Text = "0.00";
                //}
                //else
                //{
                //    valuelblservtaxConsigner.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                //    valuelblservceTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //}
                lblDeliveryCharges.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]));
                lblSerTaxCharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                if (lblDeliveryCharges.Text != "")
                {
                    dtoll = Convert.ToDouble(lblDeliveryCharges.Text);
                }
                else
                {
                    dtoll = 0;
                }


                //if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])) == true)
                //{
                //    lblGrossAmnt.Visible = false; valuelblGrossAmnt.Visible = false;
                //}
                //else { lblGrossAmnt.Visible = true; valuelblGrossAmnt.Visible = true; valuelblGrossAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])); }

                //if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])) == true)
                //{
                //    valuelblTotal.Visible = false;
                //}
                //else { valuelblTotal.Visible = true; valuelblTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])); }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                {
                    lblSerTaxCharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                    if (lblSerTaxCharge.Text != "")
                    {
                        dtax = Convert.ToDouble(lblSerTaxCharge.Text);
                    }
                    else
                    {
                        dtax = 0;
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SwachhBhrtTax_Amnt"])) == true)
                    {
                        lblSwchBhrt.Visible = false; lblSwchBhrt.Visible = false;
                    }
                    else { lblSwchBhrt.Visible = true; lblSwchBhrt.Visible = true; lblSwchBhrt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SwachhBhrtTax_Amnt"])); }

                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["KrishiKalyan_Amnt"])) == true)
                    {
                        lblKrishi.Visible = false; lblKrishi.Visible = false;
                    }
                    else { lblKrishi.Visible = true; lblKrishi.Visible = true; lblKrishi.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["KrishiKalyan_Amnt"])); }
                }
                //else
                //{
                //    lblSwchBhrt.Visible = true; lblSwchBhrt.Visible = true; lblSwchBhrt.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //    lblKrishi.Visible = true; lblKrishi.Visible = true; lblKrishi.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //}

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
                lblNetAmntP.Text = string.Format("{0:0,0.00}", (dcmsn + dblty + dcrtge + dPF + dsuchge + dtax + dwges + dtoll + dtotlAmnt));
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlGenericControl HideGrhdr = (HtmlGenericControl)e.Item.FindControl("HideGrhdr");
                if (chkbit == 1) { HideGrhdr.Visible = false; Table2.Visible = false; } else { HideGrhdr.Visible = true; Table2.Visible = true; }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl HideGritem = (HtmlGenericControl)e.Item.FindControl("HideGritem");
                if (chkbit == 1) { HideGritem.Visible = false; } else { HideGritem.Visible = true; }

                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Ch_Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dCFT += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "CFT"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlGenericControl hidfooterdetl = (HtmlGenericControl)e.Item.FindControl("hidfooterdetl");
                Label lblFTtotalWeight = (Label)e.Item.FindControl("lblFTtotalWeight");
                Label lblFTTotalAmnt = (Label)e.Item.FindControl("lblFTTotalAmnt");
                Label lblFTQty = (Label)e.Item.FindControl("lblFTQty");
                Label lblCFT = (Label)e.Item.FindControl("lblTotalCFT");

                lblFTTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lblFTtotalWeight.Text = dtotwght.ToString("N2");
                lblCFT.Text = dCFT.ToString("N2");
                lblFTQty.Text = dqtnty.ToString();

                if (chkbit == 1)
                {
                    hidfooterdetl.Visible = false;
                    //lstInfoDiv.Visible = false; 
                }
                else if (chkbit == 2)
                {
                    hidfooterdetl.Visible = true;
                    //lstInfoDiv.Visible = false; 
                }
            }
        }
        #endregion

    }
}