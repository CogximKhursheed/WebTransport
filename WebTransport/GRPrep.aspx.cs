
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
using System.Net;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace WebTransport
{
    public partial class GRPrep : Pagebase
    {
        #region Variable ...
        static FinYearA UFinYear = new FinYearA();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();
        DataTable dtTable = new DataTable(); bool IsWeight = false; Double iRate = 0.00; Double iunload = 0.00; Double dunloadamount = 0.00; string strunloadamt = "";
        double add = 0;
        double dblTtAmnt = 0; double dtotul = 0; double dtot = 0; double dul = 0; static bool UserPrefGradeVal;
        int rb = 0; Int32 iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0; Int64 AdvOrderGR_Idno = 0;
        private int intFormId = 27;
        Int32 comp_Id;
        string strSQL = ""; double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;// bool isTBBRate = false;dtotlAmnt="";
        double dSurchgPer = 0; double ItemWtAmnt = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dSwacchBhrtTaxPer = 0, dtotalAmount = 0;
        //Upadhyay #GST
        double dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Amt, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, dSwacchBhrtTaxValid = 0, dKalyanTax = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;
        int chkbit = 0;
        //Capital Logistic Variables
        string CapConsigneeName = String.Empty;
        string CapInvNo = String.Empty;
        string CapGrDate = String.Empty;
        string CapAdd1 = String.Empty;
        string CapAdd2 = String.Empty;
        string CapAdd3 = String.Empty;
        string CapVehNo = String.Empty;
        string CapFromCity = String.Empty;
        string CapToCity = String.Empty;
        string CapMtQty = String.Empty;
        string CapBags = String.Empty;
        string CapGSTINo = String.Empty;
        string CapTotAmnt = String.Empty;
        string CapOrderNo = String.Empty;
        string CapGrNo = String.Empty;
        string toState = String.Empty;
        Int64 toStateid = 0;
        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }

            if (RDbDirect.Checked) { lnkbtnGrAgain.Visible = false; }

            string strPostBackControlName = Request.Params.Get("__EVENTTARGET");
            if (!Page.IsPostBack)
            {
                GetPreferences();
                GetDefaultValues();
                if (Request.QueryString["Gr"] != null)
                {
                    hidFlagMoreDetail.Value = "1";
                }
                else
                {
                    hidFlagMoreDetail.Value = "0";
                }
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                txtFromKm.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtToKM.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTotKM.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtul.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                ValidateControls();
                this.BindDropdown();
                this.BindContainerDetails();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    divPosting.Visible = true;
                    this.BindCity();
                }
                else
                {
                    divPosting.Visible = false;
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                
                ddlGRType.SelectedIndex = 1;
                ddlGRType_SelectedIndexChanged(null, null);
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                string GRfrom = "BK";
                this.BindMaxNo(GRfrom, Convert.ToInt32((ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), intYearIdno);
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlType_SelectedIndexChanged(null, null);
                BindParty();
                BindReceiptType();
                userpref();
                GRPrepDAL objGrprepDAL = new GRPrepDAL();
                tblUserPref hiduserpref = objGrprepDAL.selectuserpref();
                if (Convert.ToBoolean(hiduserpref.GRPrint_WithoutHeader) == false)
                {
                    header.Visible = true; imgLogoShow.Visible = true; ImgLogoJain.Visible = true; header1.Visible = true;
                }
                else
                {
                    header.Visible = false; imgLogoShow.Visible = false; ImgLogoJain.Visible = false; header1.Visible = false;
                }
                if (Convert.ToBoolean(hiduserpref.ItemGrade_Req) == false)
                {
                    divGrade.Visible = false; rvfItemGrade.Enabled = false; grdMain.Columns[3].Visible = false; UserPrefGradeVal = false;
                    ddlItemGrade.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    divGrade.Visible = true; rvfItemGrade.Enabled = true; this.BindItemGrade(); grdMain.Columns[3].Visible = true; UserPrefGradeVal = true;
                }
                if (Convert.ToBoolean(hiduserpref.TypeDelPlace) == false)
                {
                    txtLoc.Visible = false;
                    rfvTxtLoc.Enabled = false;
                    ddlLocation.Visible = true;
                    rfvLocation.Enabled = true;
                    lnkBtnDelvryPlace.Visible = true;
                }
                else
                {
                    txtLoc.Visible = true;
                    rfvTxtLoc.Enabled = true;
                    ddlLocation.Visible = false;
                    rfvLocation.Enabled = false;
                    lnkBtnDelvryPlace.Visible = false;
                }
                hidTBBType.Value = Convert.ToString(objGrprepDAL.SelectTBBRate());
                HiddSurchgPer.Value = Convert.ToString(hiduserpref.Surchg_Per);
                HiddWagsAmnt.Value = Convert.ToString(hiduserpref.Wages_Amnt);
                HiddBiltyAmnt.Value = Convert.ToString(hiduserpref.Bilty_Amnt);
                HiddTolltax.Value = Convert.ToString(hiduserpref.TollTax_Amnt);
                HiddServTaxValid.Value = Convert.ToString(hiduserpref.ServTax_Valid);
                Hiditruckcitywise.Value = Convert.ToString(hiduserpref.Work_Type);
                HidiFromCity.Value = Convert.ToString(base.UserFromCity);
                hidGrtyp.Value = Convert.ToString(base.GRTyp);
                ddlGRType.SelectedValue = hidGrtyp.Value;
                HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
                hidDelPlace.Value = Convert.ToString(hiduserpref.TypeDelPlace);
                hidRenamePF.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.PFLabel_GR)) ? "PF" : Convert.ToString(hiduserpref.PFLabel_GR);
                hidrefrename.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.Reflabel_Gr)) ? "Ref. No." : Convert.ToString(hiduserpref.Reflabel_Gr);
                hidRenameToll.Value = string.IsNullOrEmpty(Convert.ToString(hiduserpref.TollTaxLabel_GR)) ? "Toll Tax" : Convert.ToString(hiduserpref.TollTaxLabel_GR);
                ddlFromCity.SelectedValue = Convert.ToString(HidiFromCity.Value);
                var lststate = objGrprepDAL.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
                this.GrRateControl();
                if (lststate != null)
                {
                    HiddServTaxPer.Value = Convert.ToString(objGrprepDAL.SelectServiceTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));

                    HiddSwachhBrtTaxPer.Value = Convert.ToString(objGrprepDAL.SelectSwacchBrtTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));

                    HiddKalyanTax.Value = Convert.ToString(objGrprepDAL.SelectKalyanTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                }
                
                ddlRateType_SelectedIndexChanged(null, null); ddlGRType.Focus();
                ddlRcptType_SelectedIndexChanged(null, null);
                if (Convert.ToString(HidsRenWages.Value) != "")
                {
                    lbltxtwages.Text = Convert.ToString(HidsRenWages.Value);
                }
                else
                {
                    lbltxtwages.Text = "Wages";
                }
                if (Convert.ToString(hidRenamePF.Value) != "")
                {
                    lbltxtPF.Text = Convert.ToString(hidRenamePF.Value);
                }
                else
                {
                    lbltxtPF.Text = "PF";
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
                }
                else
                {
                    lbltxtTolltax.Text = "Toll Tax";
                }
                EnableDisableAtLoad();
                if ((string.IsNullOrEmpty(Convert.ToString(hidTBBType.Value)) ? "0" : Convert.ToString(hidTBBType.Value)) == "True")
                {
                    txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(hiduserpref.Bilty_Amnt)) ? "0" : Convert.ToDouble(Convert.ToString(hiduserpref.Bilty_Amnt)).ToString("N2");
                    txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(HiddTolltax.Value)) ? "0" : Convert.ToDouble(Convert.ToString(HiddTolltax.Value)).ToString("N2");
                    txtWages.Text = string.IsNullOrEmpty(Convert.ToString(HiddWagsAmnt.Value)) ? "0" : Convert.ToDouble(Convert.ToString(HiddWagsAmnt.Value)).ToString("N2");
                    RDbDirect.Checked = true;
                    RDbRecpt.Enabled = false;
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
                    RDbRecpt.Checked = true;
                    RDbRecpt.Enabled = true;
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

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                if (Request.QueryString["Gr"] != null)
                {
                    divPosting.Visible = true;
                    try
                    {
                        //txtGRDate.Enabled = false;
                        this.Populate(Convert.ToInt32(Request.QueryString["Gr"]));
                    }
                    catch (Exception ex)
                    {

                    }
                    lnkbtnAdd.Visible = true;
                    tblUserPref objuserpref = objGrprepDAL.selectuserpref();
                    if (Convert.ToInt32(objuserpref.GRPrintPref) == 1 || Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                    {
                        lnkbtnPrint.Visible = true;
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                    {
                        lnkJainPrint.Visible = true;
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                    {
                        lnkOMCargo.Visible = true;
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 5)
                    {
                        lnkCapitalLogistic.Visible = true;
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        lnkKajariaLogistic.Visible = true;
                    }
                    lnkBtnLast.Visible = false;

                    //ddlDateRange.Enabled = false;
                    //ddlFromCity.Enabled = false;
                    this.GrRateControl();
                    ddlDateRange.Enabled = true;
                    //ddlFromCity.Enabled = true;
                    LnkBtnNew.Visible = true;
                    lnkChlnGen.Visible = true;
                    BindItemUpdate();
                }
                else
                {
                    lnkBtnLast.Visible = true;
                    LnkBtnNew.Visible = false;
                    lnkJainPrint.Visible = false;
                    lnkOMCargo.Visible = false;
                    lnkChlnGen.Visible = false;
                    lnkKajariaLogistic.Visible = false;
                    BindItemInsert();
                    this.SetDefault();
                }
                ddlGRType.Focus();

                if (Convert.ToString(Session["Userclass"]) == "SuperAdmin" || Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    //acc_posting.Visible = true;
                    this.PostingLeft();
                }

                DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
                userpref();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMsg", "OnchangeGrAgnst('1')", true);
                ddlDateRange.Focus();
                if (Request.QueryString["submit"] != null)
                {
                    if (Request.QueryString["submit"].ToString() == "Update_")
                    {
                        this.ShowMessage("Record updated successfully.");
                    }
                    else if (Request.QueryString["submit"].ToString().ToLower() == "uccess_")
                    {
                        this.ShowMessage("Record saved successfully.");
                    }
                }
                userprefForWeight();
                if (IsWeight == true)
                {
                    ddlRateType.Enabled = false;
                    ddlRateType.SelectedValue = "1";
                    rfvWeight.Enabled = true;
                    txtweight.AutoPostBack = true;
                }
                else
                {
                    txtweight.AutoPostBack = false;
                    rfvWeight.Enabled = false;
                    ddlRateType.Enabled = true;
                }
                //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("MM-dd-yyyy");
                // RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("MM-dd-yyyy");
            }
            else if (strPostBackControlName == "RateTypeValue")
            {
                this.FillRate();
                this.ddlRateType.Focus();
            }
            userprefForWeight();
            if (IsWeight == true)
            {
                ddlRateType.Enabled = false;
                ddlRateType.SelectedValue = "1";
                rfvWeight.Enabled = true;
            }
            else
            {
                rfvWeight.Enabled = false;
                ddlRateType.Enabled = true;
            }
            HideShowTaxFields();
            CheckLastPendingPrint();
        }

        private void CheckLastPendingPrint()
        {
            bool instantPrint = true;
            PrintLastSavedGR.Visible = false;
            Int64 lastGrIdno = Convert.ToInt64(Session["LastGrIdno"] == null ? "0" : Session["LastGrIdno"]);
            Session["LastGrIdno"] = null;
            if (instantPrint == true && lastGrIdno > 0)
            {
                hidLastgrId.Value = lastGrIdno.ToString();
                if (Session["DBName"] != null && Session["DBName"].ToString().ToLower() == "tromcargo")
                {
                    PrintLastSavedGR.Visible = true;
                }
                //PrintLastSavedGR.Visible = true;
            }
        }

        public void PrintLastSaved_Click(object sender, EventArgs e)
        {
            bool instantPrint = true;
            Int64 lastGrIdno = Convert.ToInt64(hidLastgrId.Value == null ? "0" : hidLastgrId.Value);
            if (instantPrint == true && lastGrIdno > 0)
            {
                Session["InstantPrint"] = true;
                hidPages.Value = ddlCopyPages.SelectedValue;
                if (Convert.ToBoolean(Session["InstantPrint"] == null ? false : Session["InstantPrint"]))
                {
                    GRPrepDAL objGRDAL = new GRPrepDAL();
                    tblUserPref objuserpref = objGRDAL.selectuserpref();
                    if (Convert.ToInt32(objuserpref.GRPrintPref) == 1)
                    {
                        chkbit = 2;
                        PrintGRPrep(lastGrIdno);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 2)
                    {
                        string url = "JainGrPrint.aspx" + "?q=" + Convert.ToInt64(lastGrIdno) + "&P=" + Convert.ToInt64(ddlCopyPages.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                    {
                        string url = "PrintOMCargo.aspx" + "?q=" + Convert.ToInt64(lastGrIdno) + "&P=" + Convert.ToInt64(ddlCopyPages.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                    {
                        string url = "TrLogistics.aspx" + "?q=" + Convert.ToInt64(lastGrIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(lastGrIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                }
                Session["InstantPrint"] = null;
                Session["LastGrIdno"] = null;
                PrintLastSavedGR.Visible = false;
                //hidLastgrId.Value = null;
            }
        }

        private void GetDefaultValues()
        {
            using (var db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int userid = Convert.ToInt32((Session["UserIdno"] == null || Session["UserIdno"] == "") ? "0" : Session["UserIdno"]);
                int? val = db.tblUserDefaults.Where(x => x.User_idno == userid).Select(x => x.STax_Typ).SingleOrDefault();
                //int? Gr_Typ = Convert.ToInt32(db.tblUserDefaults.Where(x => x.User_idno == userid).Select(x => x.Gr_Type).SingleOrDefault().Value);
                if ((val != null) && (val > 0))
                {
                    ddlSrvcetax.SelectedValue = val.ToString();
                }
                else
                {
                    ddlSrvcetax.SelectedValue = "2";
                }
                //if ((Gr_Typ != null) && (Gr_Typ > 0))
                //{
                //    ddlGRType.SelectedValue = Gr_Typ.ToString();
                //    ddlGRType_SelectedIndexChanged(null, null);
                //}
                //else
                //{
                //    ddlGRType.SelectedValue = "2";
                //    ddlGRType_SelectedIndexChanged(null, null);
                //}
            }
        }
        #endregion

        #region Button Event...

        //Caiptal Logistic Print Click
        protected void lnkbtnPrintCapitalLogistic_Click(object sender, EventArgs e)
        {
            GRPrepDAL objGRDAL = new GRPrepDAL();
            tblUserPref objuserpref = objGRDAL.selectuserpref();
            if (Request.QueryString["Gr"] != null)
            {
                Int64 GRIdno = Convert.ToInt64(Request.QueryString["Gr"]);
                PrintCapitalLogistic(GRIdno);
            }
        }
        protected void lnkbtnPrintKajariaLogistic_Click(object sender, EventArgs e)
        {
            GRPrepDAL objGRDAL = new GRPrepDAL();
            tblUserPref objuserpref = objGRDAL.selectuserpref();
            if (Request.QueryString["Gr"] != null)
            {
                Int64 GRIdno = Convert.ToInt64(Request.QueryString["Gr"]);
                Response.Redirect("GrPrintKajaria.aspx?q=" + GRIdno);
            }
        }
        public void PrintCapitalLogistic(Int64 GRHeadIdno)
        {
            DataSet dsReportCL = new DataSet();
            dsReportCL = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrintCapitalLogistic',@Id='" + GRHeadIdno + "'");
            CapFromCity = Convert.ToString(dsReportCL.Tables[0].Rows[0]["From_City"]);
            CapToCity = Convert.ToString(dsReportCL.Tables[0].Rows[0]["To_City"]);
            CapGrDate = Convert.ToDateTime(dsReportCL.Tables[0].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
            CapAdd1 = Convert.ToString(dsReportCL.Tables[0].Rows[0]["Add1"]);
            CapAdd2 = Convert.ToString(dsReportCL.Tables[0].Rows[0]["Add2"]);
            CapAdd3 = Convert.ToString(dsReportCL.Tables[0].Rows[0]["Add3"]);
            CapGSTINo = Convert.ToString(dsReportCL.Tables[0].Rows[0]["SenderGSTIN"]);
            CapVehNo = Convert.ToString(dsReportCL.Tables[0].Rows[0]["Lorry No"].ToString());
            CapGrNo = Convert.ToString(dsReportCL.Tables[0].Rows[0]["GR_No"].ToString());

            DataSet dsReportDetlCL = new DataSet();
            dsReportDetlCL = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectGRDetailCL',@Id='" + GRHeadIdno + "'");

            CapMtQty = Convert.ToString(dsReportDetlCL.Tables[0].Rows[0]["MtQty"]);
            CapBags = Convert.ToString(dsReportDetlCL.Tables[0].Rows[0]["Bags"]);

            Response.Redirect("PrintCapitalLogistic.aspx?CapOrderNo=" + txtOrderNo.Text + "&CapInvNo=" + txtGRNo.Text + "" + "&CapGrDate=" + CapGrDate + "&CapFromCity=" + CapFromCity + "&CapToCity=" + CapToCity
                + "&CapConsigneeName=" + txtconsnr.Text + "&CapAdd1=" + CapAdd1 + "&CapAdd2=" + CapAdd2 + "&CapAdd3=" + CapAdd3 + "&CapInvNo=" + txtRefNo.Text + "&CapVehNo="
                + CapVehNo + "&CapMTQty=" + CapMtQty + "&CapBags=" + CapBags + "&CapTotAmnt=" + txtTotItemPrice.Text + "&CapGSTINo=" + CapGSTINo + "&CapGrNo=" + CapGrNo);
        }
        protected void lnkbtnGrAgain_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
            txtDateFromDiv.Focus();
        }

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            #region Check Item Duplicate on ItemName and ItemUnit (By Peeyush Kaushik)

            if (hidrowid.Value == string.Empty)
            {
                if (CheckDuplicatieItem() == false) { this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type."); this.ClearItems(); ddlItemName.Focus(); } else { ddlunitname.Focus(); }
            }
            
            if (CheckforDuplicatesTax_InvNo(Convert.ToString(txtTaxInvoiceNo.Text.Trim()),(Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue))) && txtTaxInvoiceNo.Text !="" && txtTaxInvoiceNo.Text!="0")
            {
                txtTaxInvoiceNo.Focus(); txtTaxInvoiceNo.SelectText();
                this.ShowMessageErr("Duplicate INV.No! Please Check INV.No!");
                return;
            }
            if (CheckforDuplicatesDI_NO(Convert.ToString(txtDelvNo.Text.Trim()), (Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue))) && txtDelvNo.Text != "" && txtDelvNo.Text != "0")
            {
                txtDelvNo.Focus(); txtDelvNo.SelectText();
                this.ShowMessageErr("Duplicate DI No.! Please Check DI No.!");
                return;
            }
            #endregion
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            if (ddlunitname.SelectedIndex == 0) { ShowMessageErr("Please select Unit "); ddlunitname.Focus(); return; }
            if (ddlRateType.SelectedIndex == 0) { ShowMessageErr("Please select the Rate Type."); ddlRateType.Focus(); return; }
            if (IsWeight == true)
                if (Convert.ToDouble(txtweight.Text.Trim()) <= 0) { ShowMessageErr("Weight should be greater than zero!"); txtweight.Focus(); return; }
            if (ddlRateType.SelectedIndex != 1) { if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0) { ShowMessageErr("Weight should be greater than zero!"); txtweight.Focus(); return; } }
            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero!"); txtQuantity.Focus(); return; }
           // if (txtul.Text == "" || Convert.ToDouble(txtul.Text) <= 0) { ShowMessageErr("UnLoading should be greater than zero!"); txtul.Focus(); return; }
            if (DivCommission.Visible == true && (string.IsNullOrEmpty(Convert.ToString(txtItmCommission.Text)) ? 0 : Convert.ToDouble(txtItmCommission.Text)) > 0 && txtItmCommission.Enabled == true) { ShowMessageErr("Please Update Commission in Commission Master!"); imgComUpdate.Focus(); return; }

            if (rdbAdvanceOrder.Checked == true)
            {
                if (ddlRateType.SelectedValue == "1")
                {
                    if (Convert.ToDouble(txtQuantity.Text) > Convert.ToDouble(String.IsNullOrEmpty(txtPrevBal.Text) ? "0" : txtPrevBal.Text)) { ShowMessageErr("Quantity should not be greater than Advance Order Balance Quantity!"); txtQuantity.Focus(); return; }
                }
                if (ddlRateType.SelectedValue == "2")
                {
                    if (Convert.ToDouble(txtweight.Text) > Convert.ToDouble(String.IsNullOrEmpty(txtPrevBal.Text) ? "0" : txtPrevBal.Text)) { ShowMessageErr("Weight should not be greater than Advance Order Balance Weight."); txtweight.Focus(); return; }
                }
            }


            if (ddlType.SelectedValue != "2")
            {
                if (hidTBBType.Value == "True")
                {
                    if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
                    {
                        ShowMessageErr("Rate should be greater than zero!");
                        txtrate.Focus();
                        return;
                    }
                }
                else if ((hidTBBType.Value == "False") && (Convert.ToInt32(ddlGRType.SelectedValue) != 2))
                {
                    if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
                    {
                        ShowMessageErr("Rate should be greater than zero!");
                        txtrate.Focus();
                        return;
                    }
                }
                else
                {
                    txtrate.Text = "0.00";
                }
            }
            else
            {
                txtrate.Text = "0.00";
            }
            CalculateEdit();
            string strAmount = "";
            if (hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Item_Name"] = ddlItemName.SelectedItem.Text;
                        dtrow["Item_Idno"] = ddlItemName.SelectedValue;
                        dtrow["Unit_Name"] = ddlunitname.SelectedItem.Text;
                        dtrow["Unit_Idno"] = ddlunitname.SelectedValue;
                        dtrow["Rate_Type"] = ddlRateType.SelectedItem.Text;
                        dtrow["Rate_TypeIdno"] = ddlRateType.SelectedValue;
                        dtrow["Quantity"] = txtQuantity.Text.Trim(); iqty += Convert.ToDouble(txtQuantity.Text.Trim());
                        dtrow["Weight"] = txtweight.Text.Trim();
                        dtrow["Rate"] = txtrate.Text.Trim();
                        dtrow["Amount"] = dtotalAmount.ToString("N2");
                        dtrow["Detail"] = txtdetail.Text.Trim();
                        dtrow["Shrtg_Limit"] = hidShrtgLimit.Value;
                        dtrow["Shrtg_Rate"] = hidShrtgRate.Value;
                        dtrow["Grade_Name"] = ddlItemGrade.SelectedItem.Text;
                        dtrow["Grade_Idno"] = ddlItemGrade.SelectedValue;
                        dtrow["UnloadWeight"] = txtul.Text.Trim();  
                    }
                }
                if ((Convert.ToString(hidGRHeadIdno.Value) == "") || (Convert.ToString(hidGRHeadIdno.Value) == "0"))
                {
                    txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(iqty * Convert.ToDouble(Convert.ToString(HiddTolltax.Value) == "" ? 0 : Convert.ToDouble(HiddTolltax.Value)))) ? "0" : String.Format("{0:0,0.00}", (iqty * Convert.ToDouble(Convert.ToString(HiddTolltax.Value) == "" ? 0 : Convert.ToDouble(HiddTolltax.Value))));
                    txtWages.Text = string.IsNullOrEmpty(Convert.ToString(iqty * Convert.ToDouble(Convert.ToString(HiddWagsAmnt.Value) == "" ? 0 : Convert.ToDouble(HiddWagsAmnt.Value)))) ? "0" : String.Format("{0:0,0.00}", (iqty * Convert.ToDouble(Convert.ToString(HiddWagsAmnt.Value) == "" ? 0 : Convert.ToDouble(HiddWagsAmnt.Value))));

                }//txtWages.Text = Convert.ToDouble(iqty * dWagsAmnt).ToString("N2");
                //txtTollTax.Text = Convert.ToDouble(iqty * dTolltax).ToString("N2");
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp == null)
                {
                    dtTemp = CreateDt();
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemNameId = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                string strUnitName = ddlunitname.SelectedItem.Text.Trim();
                string strUnitNameId = string.IsNullOrEmpty(ddlunitname.SelectedValue) ? "0" : (ddlunitname.SelectedValue);
                string strRateType = ddlRateType.SelectedItem.Text.Trim();
                string strRateTypeIdno = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : (ddlRateType.SelectedValue);
                string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                strAmount = dtotalAmount.ToString("N2");
                string strDetail = string.IsNullOrEmpty(txtdetail.Text.Trim()) ? "" : (txtdetail.Text.Trim());
                string strhidShrtgRate = hidShrtgRate.Value;
                string strhidShrtgLimit = hidShrtgLimit.Value;
                string strhidShrtgRateOther = hidShrtgRateOther.Value;
                string strhidShrtgLimitOther = hidShrtgLimitOther.Value;
                string strGradeName = ddlItemGrade.SelectedItem.Text.Trim();
                string strItemGradeId = string.IsNullOrEmpty(ddlItemGrade.SelectedValue) ? "0" : (ddlItemGrade.SelectedValue);
                string strunloading = string.IsNullOrEmpty(txtul.Text.Trim()) ? "0" : (txtul.Text.Trim());
               // strunloadamt = dunloadamount.ToString("N2");
                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDetail, strhidShrtgLimit, strhidShrtgRate, strhidShrtgLimitOther, strhidShrtgRateOther, "", "", strGradeName, strItemGradeId, strunloading);
                ViewState["dt"] = dtTemp;
            }
            //  ddlItemName_SelectedIndexChanged(null,null);
            this.BindGridT();
            Selectuserpref();
            netamntcal();
            ddlItemName.Focus();
            ClearItems();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }
        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            this.ClearItems();
            ddlGRType.Focus();
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            #region fill values to Variables
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            //isTBBRate = objGrprepDAL.SelectTBBRate(); hidTBBType.Value = Convert.ToString(objGrprepDAL.SelectTBBRate());
            // tblUserPref userpref = objGrprepDAL.selectuserpref();
            //itruckcitywise = Convert.ToInt32(userpref.Work_Type);
            dtTemp = (DataTable)ViewState["dt"];
            #endregion

            #region Validation Messages

            if (ddlSender.SelectedIndex == 0) { this.ShowMessageErr("Please select Sender's Name."); ddlSender.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Sender's Name."; return; }
            if (ddlReceiver.SelectedIndex == 0) { this.ShowMessageErr("Please select Receiver's Name."); ddlReceiver.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Receiver's Name."; return; }
            if (ddlFromCity.SelectedIndex == 0) { this.ShowMessageErr("Please select From City."); ddlFromCity.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select From City."; return; }
            if (ddlToCity.SelectedIndex == 0) { this.ShowMessageErr("Please select To City."); ddlToCity.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select To City."; return; }
            if (ddlCityVia.SelectedIndex == 0) { this.ShowMessageErr("Please select via City."); ddlCityVia.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select via City."; return; }
            if (ddlLocation.SelectedIndex == 0 && ddlLocation.Visible == true) { this.ShowMessageErr("Please select Delivery Place."); ddlLocation.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Delivery Place."; return; }
            if (((Convert.ToString(Hiditruckcitywise.Value)) == "2") && (ddlTruckNo.SelectedIndex == 0)) { this.ShowMessageErr("Please select Truck No."); ddlTruckNo.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Truck No."; return; }
            if ((dtTemp != null) && (dtTemp.Rows.Count == 0)) { this.ShowMessageErr("Please enter Item Details ."); return; }

            if (ddlGRType.SelectedValue == "1")
            {
                if (ddlRcptType.SelectedIndex <= 0) { this.ShowMessageErr("Please select Receipt Type."); ddlRcptType.Focus(); return; }
                BindDropdownDAL obj = new BindDropdownDAL();
                DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32(ddlRcptType.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                    if (intAcnttype == 4)
                    {
                        if ((txtInstNo.Text.Trim() == "")) { this.ShowMessageErr("Please enter Inst. No."); txtInstNo.Focus(); return; }
                        if ((txtInstDate.Text.Trim() == "")) { this.ShowMessageErr("Please enter Inst. Date"); txtInstDate.Focus(); return; }
                        if ((ddlcustbank.SelectedIndex <= 0)) { this.ShowMessageErr("Please select Bank"); ddlcustbank.Focus(); return; }
                    }
                }
            }


            #endregion

            #region Declare Input Variables
            string strMsg = string.Empty;
            Int64 intGrPrepIdno = 0;
            Int32 intMaterialType = Convert.ToInt32(ddlGRType.SelectedIndex);
            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            Int32 IAgainst = Convert.ToInt32(RDbDirect.Checked == true ? 1 : RDbRecpt.Checked == true ? 2 : 3);
            string stragainst = Convert.ToString(RDbDirect.Checked == true ? "Direct" : "ByRecpt");
            Int32 intGRType = string.IsNullOrEmpty(ddlGRType.SelectedValue) ? 0 : Convert.ToInt32(ddlGRType.SelectedValue);
            String DIno = string.IsNullOrEmpty(txtDelvNo.Text.Trim()) ? "" : Convert.ToString(txtDelvNo.Text.Trim());
            String EGPNo = string.IsNullOrEmpty(TxtEGPNo.Text.Trim()) ? "" : Convert.ToString(TxtEGPNo.Text.Trim());
            Int64 intGRNo = string.IsNullOrEmpty(txtGRNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGRNo.Text.Trim());
            Int32 intSenderIdno = string.IsNullOrEmpty(ddlSender.SelectedValue) ? 0 : Convert.ToInt32(ddlSender.SelectedValue);
            Int32 TruckNoIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);
            Int32 intRecvrIDno = string.IsNullOrEmpty(ddlReceiver.SelectedValue) ? 0 : Convert.ToInt32(ddlReceiver.SelectedValue);
            Int32 intFromcityIDno = string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue);
            Int32 intTocityIDno = string.IsNullOrEmpty(ddlToCity.SelectedValue) ? 0 : Convert.ToInt32(ddlToCity.SelectedValue);
            Int32 intDelPlaceIdno = string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt32(ddlLocation.SelectedValue);
            Int32 intAgentIdno = string.IsNullOrEmpty(ddlParty.SelectedValue) ? 0 : Convert.ToInt32(ddlParty.SelectedValue);
            string strRemark = string.IsNullOrEmpty(TxtRemark.Text.Trim()) ? "" : Convert.ToString(TxtRemark.Text.Trim());
            string PreFixGRNum = string.IsNullOrEmpty(txtPrefixNo.Text.Trim()) ? "" : Convert.ToString(txtPrefixNo.Text.Trim());
            Int64 iCityviaIdno = string.IsNullOrEmpty(ddlCityVia.SelectedValue) ? 0 : Convert.ToInt64(ddlCityVia.SelectedValue);
            String RefNo = string.IsNullOrEmpty(txtRefNo.Text.Trim()) ? "" : Convert.ToString(txtRefNo.Text.Trim());
            String ManualNo = string.IsNullOrEmpty(txtManualNo.Text.Trim()) ? "" : Convert.ToString(txtManualNo.Text.Trim());
            String PortNum = string.IsNullOrEmpty(txtPortNum.Text.Trim()) ? "" : Convert.ToString(txtPortNum.Text.Trim());

            Int32 intServTaxPaidBy = string.IsNullOrEmpty(ddlSrvcetax.SelectedValue) ? 0 : Convert.ToInt32(ddlSrvcetax.SelectedValue);
            Int32 intRcptypeIdno = string.IsNullOrEmpty(ddlRcptType.SelectedValue) ? 0 : Convert.ToInt32(ddlRcptType.SelectedValue);
            String InstNo = string.IsNullOrEmpty(txtInstNo.Text.Trim()) ? "" : Convert.ToString(txtInstNo.Text.Trim());
            string strShipmentNo = string.IsNullOrEmpty(txtshipment.Text.Trim()) ? "" : Convert.ToString(txtshipment.Text.Trim());
            Int64 intType = string.IsNullOrEmpty(ddlType.SelectedValue) ? 1 : Convert.ToInt64(ddlType.SelectedValue);
            double dblFixedAmount = 0;
            if (intType == 2) { dblFixedAmount = string.IsNullOrEmpty(txtFixedAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtFixedAmount.Text.Trim()); }
            DateTime? dtInstDate;
            if (txtInstDate.Text == "") { dtInstDate = null; } else { dtInstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text)); }
            Int32 intcustBankIdno = string.IsNullOrEmpty(ddlcustbank.SelectedValue) ? 0 : Convert.ToInt32(ddlcustbank.SelectedValue);
            string strGrossAmnt = (Convert.ToString(txtGrossAmnt.Text)).Replace(",", "");
            Double DGrossAmnt = string.IsNullOrEmpty(strGrossAmnt) ? 0 : Convert.ToDouble(strGrossAmnt);
            string strCommission = (Convert.ToString(txtCommission.Text)).Replace(",", "");
            Double DCommission = string.IsNullOrEmpty(strCommission) ? 0 : Convert.ToDouble(strCommission);
            string strTollTax = (Convert.ToString(txtTollTax.Text)).Replace(",", "");
            Double DTollTax = string.IsNullOrEmpty(strTollTax) ? 0 : Convert.ToDouble(strTollTax);
            string strCartage = (Convert.ToString(txtCartage.Text)).Replace(",", "");
            Double DCartage = string.IsNullOrEmpty(strCartage) ? 0 : Convert.ToDouble(strCartage);
            string strBilty = (Convert.ToString(txtBilty.Text)).Replace(",", "");
            Double DBilty = string.IsNullOrEmpty(strBilty) ? 0 : Convert.ToDouble(strBilty);
            string strSubTotal = (Convert.ToString(txtSubTotal.Text)).Replace(",", "");
            double DSubTotal = string.IsNullOrEmpty(strSubTotal) ? 0 : Convert.ToDouble(strSubTotal);
            string strTotalAmnt = (Convert.ToString(txtTotalAmnt.Text)).Replace(",", "");
            Double DTotalAmnt = string.IsNullOrEmpty(strTotalAmnt) ? 0 : Convert.ToDouble(strTotalAmnt);
            string strWages = (Convert.ToString(txtWages.Text)).Replace(",", "");
            Double DWages = string.IsNullOrEmpty(strWages) ? 0 : Convert.ToDouble(strWages);
            string intConsName = string.IsNullOrEmpty(txtconsnr.Text.Trim()) ? "" : Convert.ToString(txtconsnr.Text.Trim());

            string strServTax = (Convert.ToString(txtServTax.Text)).Replace(",", "");
            Double DServTax = string.IsNullOrEmpty(strServTax) ? 0 : Convert.ToDouble(strServTax);

            string strSwchBrtTax = (Convert.ToString(txtSwchhBhartTx.Text)).Replace(",", "");
            Double DSwchBrtTax = string.IsNullOrEmpty(strSwchBrtTax) ? 0 : Convert.ToDouble(strSwchBrtTax);

            string strKisanTax = (Convert.ToString(txtkalyan.Text)).Replace(",", "");
            Double DKisanTax = string.IsNullOrEmpty(strKisanTax) ? 0 : Convert.ToDouble(strKisanTax);

            string strSurchrge = (Convert.ToString(txtSurchrge.Text)).Replace(",", "");
            Double DSurchrge = string.IsNullOrEmpty(strSurchrge) ? 0 : Convert.ToDouble(strSurchrge);
            string strPF = (Convert.ToString(txtPF.Text)).Replace(",", "");
            Double DPF = string.IsNullOrEmpty(strPF) ? 0 : Convert.ToDouble(strPF);
            string strNetAmnt = (Convert.ToString(txtNetAmnt.Text)).Replace(",", "");
            Double DNetAmnt = string.IsNullOrEmpty(strNetAmnt) ? 0 : Convert.ToDouble(strNetAmnt);
            string strRoundOffAmnt = (Convert.ToString(TxtRoundOff.Text)).Replace(",", "");
            Double DRoundOffAmnt = string.IsNullOrEmpty(strRoundOffAmnt) ? 0 : Convert.ToDouble(strRoundOffAmnt);
            DataTable dtDetail = (DataTable)ViewState["dt"];

            Double dblTaxValid = string.IsNullOrEmpty(HiddServTaxValid.Value) ? 0 : Convert.ToDouble(HiddServTaxValid.Value);
            Double dblServTaxPerc = string.IsNullOrEmpty(HiddServTaxPer.Value) ? 0 : Convert.ToDouble(HiddServTaxPer.Value);
            Double dblSwcgBrtTaxPerc = string.IsNullOrEmpty(HiddSwachhBrtTaxPer.Value) ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value);
            Double dblKalyanTaxPer = string.IsNullOrEmpty(HiddKalyanTax.Value) ? 0 : Convert.ToDouble(HiddKalyanTax.Value);
            Double TotItemValue = string.IsNullOrEmpty(txtTotItemPrice.Text.Trim()) ? 0 : Convert.ToDouble(txtTotItemPrice.Text.Trim());
            String strOrdrNo = string.IsNullOrEmpty(txtOrderNo.Text.Trim()) ? "" : Convert.ToString(txtOrderNo.Text.Trim());
            String strFormNo = string.IsNullOrEmpty(txtFromNo.Text.Trim()) ? "" : Convert.ToString(txtFromNo.Text.Trim());
            Double dblFromKM = string.IsNullOrEmpty(txtFromKm.Text.Trim()) ? 0 : Convert.ToDouble(txtFromKm.Text.Trim());
            Double dblToKM = string.IsNullOrEmpty(txtToKM.Text.Trim()) ? 0 : Convert.ToDouble(txtToKM.Text.Trim());
            Double dblTotKm = string.IsNullOrEmpty(txtTotKM.Text.Trim()) ? 0 : Convert.ToDouble(txtTotKM.Text.Trim());
            string strEwayBill = string.IsNullOrEmpty(txtEWayBillNo.Text.Trim()) ? "" : Convert.ToString(txtEWayBillNo.Text.Trim());
            string strTaxInvNo = string.IsNullOrEmpty(txtTaxInvoiceNo.Text.Trim()) ? "" : Convert.ToString(txtTaxInvoiceNo.Text.Trim());
            string strExcInvNo = string.IsNullOrEmpty(txtExcInvoceNO.Text.Trim()) ? "" : Convert.ToString(txtExcInvoceNO.Text.Trim());

            string strDelPlace = string.IsNullOrEmpty(txtLoc.Text.Trim()) ? "" : Convert.ToString(txtLoc.Text.Trim());
            bool TypeDelPlace = Convert.ToBoolean(hidDelPlace.Value);
            //Upadhyay #GST
            //Amounts
            string strSGST_Amt = (Convert.ToString(txtSGSTAmnt.Text)).Replace(",", "");
            Double dSGST_Amt = string.IsNullOrEmpty(strSGST_Amt) ? 0 : Convert.ToDouble(strSGST_Amt);
            string strCGST_Amt = (Convert.ToString(txtCGSTAmnt.Text)).Replace(",", "");
            Double dCGST_Amt = string.IsNullOrEmpty(strCGST_Amt) ? 0 : Convert.ToDouble(strCGST_Amt);
            string strIGST_Amt = (Convert.ToString(txtIGSTAmnt.Text)).Replace(",", "");
            Double dIGST_Amt = string.IsNullOrEmpty(strIGST_Amt) ? 0 : Convert.ToDouble(strIGST_Amt);
            //Presently GST Cess is not being used
            string strGSTCess = "0";
            Double dGSTCess_Amt = string.IsNullOrEmpty(strGSTCess) ? 0 : Convert.ToDouble(strGSTCess);
            //Percentage
            string strSGST_Per = "0";
            Double dSGST_Per = 0;
            string strCGST_Per = "0";
            Double dCGST_Per = 0;
            string strIGST_Per = "0";
            Double dIGST_Per = 0;
            
            if (hidGstType.Value == "1")
            {
                strSGST_Per = (Convert.ToString((hidSGSTPer.Value == null || hidSGSTPer.Value == "") ? "0" : hidSGSTPer.Value)).Replace(",", "");
                dSGST_Per = string.IsNullOrEmpty(strSGST_Per) ? 0 : Convert.ToDouble(strSGST_Per);
                strCGST_Per = (Convert.ToString((hidCGSTPer.Value == null || hidCGSTPer.Value == "") ? "0" : hidCGSTPer.Value)).Replace(",", "");
                dCGST_Per = string.IsNullOrEmpty(strCGST_Per) ? 0 : Convert.ToDouble(strCGST_Per);
            }
            else if (hidGstType.Value == "2")
            {
                strIGST_Per = (Convert.ToString((hidIGSTPer.Value == null || hidIGSTPer.Value == "") ? "0" : hidIGSTPer.Value)).Replace(",", "");
                dIGST_Per = string.IsNullOrEmpty(strIGST_Per) ? 0 : Convert.ToDouble(strIGST_Per);
            }
            //Presently GST Cess is not being used            
            string strGSTCess_Per = "0";
            Double dGSTCess_Per = string.IsNullOrEmpty(strGSTCess_Per) ? 0 : Convert.ToDouble(strGSTCess_Per);
            int iGST_Idno = Convert.ToInt32(hidGstType.Value == "" ? "0" : hidGstType.Value);
            DateTime? dtEGPDate = null;
            if (txtEGPDate.Text == "") { dtEGPDate = null; } else { dtEGPDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtEGPDate.Text)); }
            DateTime? dtDIDate = null;
            if (txtDIDate.Text == "") { dtDIDate = null; } else { dtDIDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDIDate.Text)); }

            DateTime? dtTaxInvDate = null;
            if (txtInvDate.Text == "") { dtTaxInvDate = null; } else { dtTaxInvDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInvDate.Text)); }

            if (RDbDirect.Checked == false)
            {
                if (ViewState["GrOrder"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "openModalGrdDtls();", true);
                }
                else if (ViewState["GrOrder"].ToString() == "Receipt")
                {
                    RcptGoodHeadIdno = Convert.ToInt64(Convert.ToString(HidGrAgnstRcptIdno.Value) == "" ? 0 : Convert.ToInt64(HidGrAgnstRcptIdno.Value));
                    AdvOrderGR_Idno = 0;
                }
                else
                {
                    if ((dtTemp != null) && (Convert.ToString(dtTemp.Rows[0]["Rate_Type"]) == "0")) { this.ShowMessageErr("Please enter Rate Type."); lblmessage.Visible = true; lblmessage.Text = "* Please enter Rate Type."; lblmessage.Focus(); return; }
                    AdvOrderGR_Idno = Convert.ToInt64(Convert.ToString(HidGrAgnstRcptIdno.Value) == "" ? 0 : Convert.ToInt64(HidGrAgnstRcptIdno.Value));
                    RcptGoodHeadIdno = 0;
                }
            }
            //Ajeet
            String ContainerNum = txtContainrNo.Text.Trim();
            String ContainerSealNum = txtContainerSealNo.Text.Trim();
            String ContainerNum2 = txtContainrNo2.Text.Trim();
            String ContainerSealNum2 = txtContainerSealNo2.Text.Trim();
            Int32 ImpExp_id = Convert.ToInt32(ddlTypeI.SelectedValue);
            string CharFrwder_Name = txtNameI.Text.Trim();

            Int64 ContainerSize = string.IsNullOrEmpty(ddlContainerSize.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerSize.SelectedValue);
            Int64 ContainerType = string.IsNullOrEmpty(ddlContainerType.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerType.SelectedValue);
            Boolean IsSMSSent = false;
            Boolean SendGRSMS = false;
            #endregion

            #region Insert/Update with Transaction
            //using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            //{
            Int64 CreatedBY = Convert.ToInt64(Session["UserIdno"]);
            if (grdMain.Rows.Count > 0 && dtTemp != null && dtTemp.Rows.Count > 0)
            {
                GRPrepDAL objGR = new GRPrepDAL();
                string GRfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(GRfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                if (Convert.ToString(hidGRHeadIdno.Value) == "")
                {
                    if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                    {
                        var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()),Convert.ToString(txtDelvNo.Text.Trim()) ,Convert.ToString(txtTaxInvoiceNo.Text.Trim()) ,Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
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
                Int64 UserIdno = (string.IsNullOrEmpty(Convert.ToString(Session["UserIdno"])) ? 0 : Convert.ToInt64(Session["UserIdno"]));
                GRPrepDAL obj = new GRPrepDAL();
                if (Convert.ToString(hidGRHeadIdno.Value) != "")
                {
                    intGrPrepIdno = obj.GRPrepUpdate(Convert.ToInt64(hidGRHeadIdno.Value), dtGRDate, IAgainst, intGRType, DIno, EGPNo, intGRNo, intSenderIdno,
                        TruckNoIdno, intRecvrIDno, intFromcityIDno, intTocityIDno, intDelPlaceIdno, intAgentIdno, strRemark, intServTaxPaidBy, intRcptypeIdno,
                        InstNo, dtInstDate, intcustBankIdno, DGrossAmnt, DCommission, DTollTax, DCartage, DBilty, DSubTotal, DTotalAmnt, DWages, DServTax,
                        DSwchBrtTax, DSurchrge, DPF, DNetAmnt, DRoundOffAmnt, YearIdno, RcptGoodHeadIdno, AdvOrderGR_Idno, Convert.ToBoolean(hidTBBType.Value),
                        Convert.ToInt32(Hiditruckcitywise.Value), Convert.ToString(HidsRenWages.Value), dtDetail, strShipmentNo, PreFixGRNum, ContainerNum,
                        ContainerSize, ContainerType, ContainerSealNum, iCityviaIdno, intType, dblFixedAmount, RefNo, PortNum, DKisanTax, ManualNo, UserIdno,
                        intConsName, dblTaxValid, dblServTaxPerc, dblSwcgBrtTaxPerc, dblKalyanTaxPer, TotItemValue, strOrdrNo, strFormNo, ContainerNum2,
                        ContainerSealNum2, ImpExp_id, CharFrwder_Name, strDelPlace, TypeDelPlace, dblFromKM, dblToKM, dblTotKm, CreatedBY,
                        dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Per, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per, iGST_Idno, dtEGPDate, strEwayBill, strTaxInvNo, strExcInvNo, dtDIDate, dtTaxInvDate);
                }
                else
                {
                    intGrPrepIdno = obj.InsertGR(dtGRDate, IAgainst, intGRType, DIno, EGPNo, intGRNo, intSenderIdno, TruckNoIdno, intRecvrIDno, intFromcityIDno,
                        intTocityIDno, intDelPlaceIdno, intAgentIdno, strRemark, intServTaxPaidBy, intRcptypeIdno, InstNo, dtInstDate, intcustBankIdno,
                        DGrossAmnt, DCommission, DTollTax, DCartage, DBilty, DSubTotal, DTotalAmnt, DWages, DServTax, DSwchBrtTax, DSurchrge, DPF, DNetAmnt,
                        DRoundOffAmnt, YearIdno, RcptGoodHeadIdno, AdvOrderGR_Idno, Convert.ToBoolean(hidTBBType.Value), Convert.ToInt32(Hiditruckcitywise.Value),
                        Convert.ToString(HidsRenWages.Value), dtDetail, strShipmentNo, PreFixGRNum, ContainerNum, ContainerSize, ContainerType, ContainerSealNum,
                        iCityviaIdno, intType, dblFixedAmount, false, RefNo, PortNum, DKisanTax, ManualNo, UserIdno, intConsName, dblTaxValid, dblServTaxPerc,
                        dblSwcgBrtTaxPerc, dblKalyanTaxPer, TotItemValue, strOrdrNo, strFormNo, ContainerNum2, ContainerSealNum2, ImpExp_id, CharFrwder_Name,
                        strDelPlace, TypeDelPlace, dblFromKM, dblToKM, dblTotKm, CreatedBY, dSGST_Amt, dCGST_Amt, dIGST_Amt, dGSTCess_Per, dSGST_Per, dCGST_Per, dIGST_Per, dGSTCess_Per, iGST_Idno, dtEGPDate, strEwayBill, strTaxInvNo, strExcInvNo, dtDIDate, dtTaxInvDate);

                    GRPrepDAL objGrPrep = new GRPrepDAL();
                    SendGRSMS = objGrPrep.GetUserPref().Send_GrSMS;
                    if (SendGRSMS)
                    {
                        string strPhoneNo = GetSenderMobileNumbers(Convert.ToInt32(ddlSender.SelectedValue == "" ? "0" : ddlSender.SelectedValue));
                        if (strPhoneNo != String.Empty)
                        {
                            IsSMSSent = SendSMS(strPhoneNo, GetMsg(Convert.ToInt32(intGRNo), dtGRDate.ToString("dd-MM-yyyy"), ddlToCity.SelectedItem.Text, ddlFromCity.SelectedItem.Text));
                        }
                        else
                        {
                            IsSMSSent = false;
                        }
                    }
                    
                }
                
                Session["LastGrIdno"] = intGrPrepIdno;
                obj = null;
            }
            if (intGrPrepIdno > 0)
            {
                string strRoundOff = TxtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                BindDropdownDAL obj = new BindDropdownDAL();
                Int64 intAcnttype = 0;
                Int32 RecptType = Convert.ToInt32(Convert.ToString(ddlRcptType.SelectedIndex) == "0" ? 0 : Convert.ToInt32(ddlRcptType.SelectedValue));
                if (RecptType > 0)
                {
                    DataTable dt = obj.BindRcptTypeDel(RecptType, ApplicationFunction.ConnectionString()); intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                }
                tblUserPref hiduserpref = objGrprepDAL.selectuserpref();

                double SGSTAmnt,CGSTAmnt,IGSTAmnt=0;

                if(ddlSrvcetax.SelectedValue=="1")
                {
                    SGSTAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtSGSTAmnt.Text) ? "0" : txtSGSTAmnt.Text);
                    CGSTAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtCGSTAmnt.Text) ? "0" : txtCGSTAmnt.Text);
                    IGSTAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtIGSTAmnt.Text) ? "0" : txtIGSTAmnt.Text);
                }
                else
                {
                    SGSTAmnt = 0;CGSTAmnt = 0;IGSTAmnt = 0;
                }
               

                if (this.PostIntoAccounts(dtTemp, intGrPrepIdno, "GR", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), 0, 0, 0, 0, (Convert.ToDouble(Convert.ToString(txtSubTotal.Text) == "" ? 0 : Convert.ToDouble(txtSubTotal.Text)) - Convert.ToDouble(Convert.ToString(txtGrossAmnt.Text) == "" ? 0 : Convert.ToDouble(txtGrossAmnt.Text))), Convert.ToDouble(Convert.ToString(txtNetAmnt.Text) == "" ? 0 : Convert.ToDouble(txtNetAmnt.Text)), Convert.ToDouble(Convert.ToString(txtServTax.Text) == "" ? 0 : Convert.ToDouble(txtServTax.Text)), Convert.ToDouble(Convert.ToString(txtGrossAmnt.Text) == "" ? 0 : Convert.ToDouble(txtGrossAmnt.Text)), Convert.ToDouble(Convert.ToString(TxtRoundOff.Text) == "" ? 0 : Convert.ToDouble(TxtRoundOff.Text)), Convert.ToDouble(Convert.ToString(txtSwchhBhartTx.Text) == "" ? 0 : Convert.ToDouble(txtSwchhBhartTx.Text)), Convert.ToDouble(Convert.ToString(txtkalyan.Text) == "" ? 0 : Convert.ToDouble(txtkalyan.Text)), RecptType, ((intAcnttype == 4) ? Convert.ToString(txtInstNo.Text.Trim()) : ""), ((intAcnttype == 4) ? Convert.ToString(txtInstDate.Text.Trim()) : ""), ((intAcnttype == 4) ? Convert.ToString(ddlcustbank.SelectedValue) : "0"), Convert.ToInt32(ddlGRType.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue), Convert.ToInt32(ddlSrvcetax.SelectedValue), Convert.ToString(txtGRDate.Text.Trim()), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(hiduserpref.InvGen_GrType), SGSTAmnt, CGSTAmnt, CGSTAmnt) == true)
                {
                    GRPrepDAL obj1 = new GRPrepDAL();
                    obj1.UpdateIsPosting(Convert.ToInt64(intGrPrepIdno));
                    obj1.UpdateFlag(ApplicationFunction.ConnectionString(), Convert.ToInt64(intGrPrepIdno));
                    ddlFromCity_SelectedIndexChanged(null, null);
                    this.ClearAll();
                    ViewState["dt"] = dtTemp = null;
                    this.BindGridT();
                    ddlGRType.Focus();
                    objGrprepDAL = new GRPrepDAL();
                    //tScope.Complete();

                    if (hiduserpref.DisableChlnEntry == true)
                    {
                        Response.Redirect("ChlnBooking.aspx?GrHeadIdno=" + intGrPrepIdno);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(hidGRHeadIdno.Value)) == false)
                        {
                            hidpostingmsg.Value = "Record(s) not updated.";
                        }
                        else
                        {
                            hidpostingmsg.Value = "Record(s) not saved.";
                        }
                        //tScope.Dispose();
                    }
                    //tScope.Dispose();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                    return;
                }
            }
            else if (intGrPrepIdno < 0)
            {
                if (txtGRNo.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirmAtSave()", true);
                }
            }
            else
            {
                if (Convert.ToString(hidGRHeadIdno.Value) != null && Convert.ToString(hidGRHeadIdno.Value) != "")
                {
                    this.ShowMessageErr("Record not updated.");
                }
                else
                {
                    this.ShowMessageErr("Record not saved.");
                }
            }
            //}

            Int64 iMaxGRNo = 0;
            GRPrepDAL objGRDAL = new GRPrepDAL();
            iMaxGRNo = objGRDAL.MaxNo("BK", Convert.ToInt32(ddlDateRange.SelectedValue),
                                            Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 :
                                            Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(iMaxGRNo);

            ddlGRType.Focus();

            if (intGrPrepIdno > 0)
            {
                if (Convert.ToString(hidGRHeadIdno.Value) != null && Convert.ToString(hidGRHeadIdno.Value) != "")
                {
                    hidGRHeadIdno.Value = string.Empty;
                    Response.Redirect("GRPrep.aspx?submit=Update_");
                }
                else
                {
                    Response.Redirect("GRPrep.aspx?submit=Success_");
                }
            }
            #endregion
        }

        private void ReCalculateGST()
        {
            double gst = Convert.ToDouble(txtSGSTAmnt.Text) + Convert.ToDouble(txtCGSTAmnt.Text) + Convert.ToDouble(txtIGSTAmnt.Text);
            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + gst))).ToString("N2");
            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + gst)).ToString("N2");
        }

        private void ReGSTCalGrN()
        {
            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text)))).ToString("N2");
            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text))).ToString("N2");
            pnlSGST.Visible = false;
            pnlCGST.Visible = false;
            pnlIGST.Visible = false;
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (hidGRHeadIdno.Value != null && hidGRHeadIdno.Value != "")
            {
                Populate(Convert.ToInt32(hidGRHeadIdno.Value));
                ClearItems(); ddlFromCity.SelectedValue = Convert.ToString(HidiFromCity.Value);
            }
            else
            {
                this.ClearAll(); ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                string GRfrom = "BK";
                this.BindMaxNo(GRfrom, Convert.ToInt32((ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), intYearIdno);
            }
            ddlGRType.Focus();
        }
        protected void btnPrtyDetSave_Click(object sender, ImageClickEventArgs e)
        {
            int rec = 0; Int64 RetnValue = 0;
            DateTime? DOA = null; DateTime? DOB = null;
            if (string.IsNullOrEmpty(txtDOB.Text) == false)
            {
                DOB = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDOB.Text));
            }
            if (string.IsNullOrEmpty(txtDOA.Text) == false)
            {
                DOA = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDOA.Text));
            }

        }
        protected void ImgCancelDetl_Click(object sender, ImageClickEventArgs e)
        {
            txtconperson.Text = txtadd1.Text = txtadd2.Text = txtTempAddr1.Text = txtTempAddr2.Text = txtTempAddr3.Text = txtpinno.Text = txtphoneno.Text = txtPhoneNoRes.Text = txtmobileno.Text = txtRegnPlace.Text = txtemail.Text = txtDOA.Text = txtDOB.Text = txtTinno.Text = txtfaxno.Text = string.Empty;
            ddlstate.SelectedIndex = 0;
            ddlcityarea.SelectedIndex = 0;
            ddlPost.SelectedIndex = 0;
            ddlTehsil.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlcity.SelectedIndex = 0;

        }
        //protected void imgPDF_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (hidGRHeadIdno.Value != "")
        //    {
        //        this.PrintGRPrep(Convert.ToInt64(hidGRHeadIdno.Value));
        //    }
        //    else
        //    {
        //        this.ShowMessageErr("Please Select Record");
        //    }

        //}
        //protected void imgbtnGRAginst_Click(object sender, ImageClickEventArgs e)
        //{
        //    chkSelectAllRows.Checked = false; chkSelectAllRows.Visible = false;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
        //}
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblmsg2.Visible = false;
            lblmsg.Visible = false;
            ddltocityDiv.SelectedIndex = ddlRecvrDiv.SelectedIndex = ddldelvplaceDIv.SelectedIndex = 0;
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            //chkSelectAllRows.Visible = false; chkSelectAllRows.Checked = false;

        }
        protected void lnkbtndivsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    string strchkDetlValue = string.Empty; int Icount = 0;
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {

                        CheckBox ChkGr = (CheckBox)grdGrdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {

                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                            RcptGoodHeadIdno = Convert.ToInt64(hidGrIdno.Value); HidGrAgnstRcptIdno.Value = (hidGrIdno.Value);
                            Icount++;

                        }

                    }
                    if (Icount > 1)
                    {
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalGrdDtls();", true);
                        return;
                    }
                    else
                    {
                        lblmsg.Visible = false;
                        lblmsg2.Visible = false;
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        lblmsg.Text = "Please select atleast one Gr.";
                        lblmsg2.Text = "Please select atleast one Gr.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalGrdDtls();", true);
                        return;

                    }
                    else
                    {
                        lblmsg.Visible = false;
                        lblmsg2.Visible = false;
                        GRPrepDAL obj = new GRPrepDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;

                        if (rdbAdvanceOrder.Checked)
                        {
                            dtRcptDetl = obj.SelectRECPTGrDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(strchkDetlValue), "SelectAdvanceOrderDetailInGR");
                            ViewState["GrOrder"] = "AdvanceOrder";
                            if (dtRcptDetl.Rows.Count > 0)
                            {
                                //if (Convert.ToInt32(dtRcptDetl.Rows[0]["Rate_TypeIdno"]) == 1)
                                //{
                                //    hidAdvOrdrQty.Value = Convert.ToString(dtRcptDetl.Rows[0]["PREV_BAL"]);
                                //    hidAdvOrdrWght.Value = Convert.ToString(dtRcptDetl.Rows[0]["Weight"]);
                                //}
                                //else if (Convert.ToInt32(dtRcptDetl.Rows[0]["Rate_TypeIdno"]) == 2)
                                //{
                                //    hidAdvOrdrWght.Value = Convert.ToString(dtRcptDetl.Rows[0]["PREV_BAL"]);
                                //    hidAdvOrdrQty.Value = Convert.ToString(dtRcptDetl.Rows[0]["Quantity"]);
                                //}
                                hidAdvOrdrWght.Value = Convert.ToString(dtRcptDetl.Rows[0]["Weight"]);
                                hidAdvOrdrQty.Value = Convert.ToString(dtRcptDetl.Rows[0]["Quantity"]);
                            }
                            TxtRemark.Text = dtRcptDetl.Rows[0]["Detail"].ToString();
                            ddlTruckNo.SelectedValue = dtRcptDetl.Rows[0]["TruckNo"].ToString();

                            ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(dtRcptDetl.Rows[0]["Agent_Idno"])) ? "" : Convert.ToString(dtRcptDetl.Rows[0]["Agent_Idno"]); // if agent name is select at adv order than here it will be selected. only in case of adv order.
                            ddlReceiver.Enabled = true;
                        }
                        else
                        {
                            ViewState["GrOrder"] = "Receipt";

                            dtRcptDetl = obj.SelectRECPTGrDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(strchkDetlValue), "SelectRcptDetailInGR");
                            ddlReceiver.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["RECEVR_IDNO"]); SpanReceiverRefresh.Visible = ddlReceiver.Enabled = lnkBtnReceiver.Enabled = false;
                        }

                        ViewState["dt"] = dtRcptDetl;
                        BindGridT();
                        grdMain.DataSource = null;
                        ddlFromCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["FROMCITY_IDNO"]);
                        if (Convert.ToString(dtRcptDetl.Rows[0]["FROMCITY_IDNO"]) != "0") { SpanFromCityRefresh.Visible = ddlFromCity.Enabled = lnkBtnFromCity.Enabled = false; } else { SpanFromCityRefresh.Visible = ddlFromCity.Enabled = lnkBtnFromCity.Enabled = true; }
                        ddlToCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["TOCITY_IDNO"]);
                        if (Convert.ToString(dtRcptDetl.Rows[0]["TOCITY_IDNO"]) != "0") { SpanToCityRefresh.Visible = ddlToCity.Enabled = lnkBtnTocity.Enabled = false; } else { SpanToCityRefresh.Visible = ddlToCity.Enabled = lnkBtnTocity.Enabled = true; }
                        ddlCityVia.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Cityvia_Idno"]);
                        if (Convert.ToString(dtRcptDetl.Rows[0]["Cityvia_Idno"]) != "0") { SpanCityViaRefresh.Visible = ddlCityVia.Enabled = lnkBtnCityvia.Enabled = false; } else { SpanCityViaRefresh.Visible = ddlCityVia.Enabled = lnkBtnCityvia.Enabled = true; }
                        ddlLocation.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["DELVRYPLC_IDNO"]);
                        if (Convert.ToString(dtRcptDetl.Rows[0]["DELVRYPLC_IDNO"]) != "0") { SpanDelvryPlaceRefresh.Visible = ddlLocation.Enabled = lnkBtnDelvryPlace.Enabled = false; } else { SpanDelvryPlaceRefresh.Visible = ddlLocation.Enabled = lnkBtnDelvryPlace.Enabled = true; }
                        ddlSender.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["SENDER_IDNO"]);
                        if (Convert.ToString(dtRcptDetl.Rows[0]["SENDER_IDNO"]) != "0") { SpanSenderRefresh.Visible = ddlSender.Enabled = lnkBtnSender.Enabled = false; } else { SpanSenderRefresh.Visible = ddlSender.Enabled = lnkBtnSender.Enabled = true; }
                        txtconsnr.Text = Convert.ToString(dtRcptDetl.Rows[0]["ConsigName"]);
                        // if (Convert.ToString(dtRcptDetl.Rows[0]["ConsigName"]) != "") { txtconsnr.Enabled = false; } else { txtconsnr.Enabled = true; }

                        if (rdbAdvanceOrder.Checked == true)
                        {
                            ddlReceiver.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["SENDER_IDNO"]);
                            txtshipment.Text = Convert.ToString(dtRcptDetl.Rows[0]["SHIPMENT_NO"]);
                            txtContainrNo.Text = Convert.ToString(dtRcptDetl.Rows[0]["Contanr_No"]);
                            ddlContainerSize.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Contanr_Size"]);
                            ddlContainerType.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Contanr_Type"]);
                            txtContainerSealNo.Text = Convert.ToString(dtRcptDetl.Rows[0]["Contanr_SealNo"]);
                            txtPortNum.Text = Convert.ToString(dtRcptDetl.Rows[0]["Port_no"]);
                            txtRefNo.Text = Convert.ToString(dtRcptDetl.Rows[0]["Ref_No"]);
                            txtContainerSealNo2.Text = Convert.ToString(dtRcptDetl.Rows[0]["GRContanr_SealNo2"]);
                            txtContainrNo2.Text = Convert.ToString(dtRcptDetl.Rows[0]["GRContanr_No2"]);
                            txtNameI.Text = Convert.ToString(dtRcptDetl.Rows[0]["ChaFrwdr_Name"]);
                            ddlTypeI.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["ImpExp_idno"]);
                        }

                        if ((Convert.ToString(dtRcptDetl.Rows[0]["AGENT_IDNO"]) != "0") && (Convert.ToString(dtRcptDetl.Rows[0]["AGENT_IDNO"]) != ""))
                        {
                            ddlParty.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["AGENT_IDNO"]); SpanAgentRefresh.Visible = ddlParty.Enabled = lnkBtnAgent.Enabled = false;
                        }
                        else
                        {
                            SpanAgentRefresh.Visible = ddlParty.Enabled = lnkBtnAgent.Enabled = true;
                        }
                        ddlDateRange.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["YEAR_IDNO"]); ddlDateRange.Enabled = false;
                        ddlItemName.Enabled = false; ddlunitname.Enabled = false;
                        //txtQuantity.Enabled = false; txtweight.Enabled = false;
                        foreach (GridViewRow row in grdMain.Rows)
                        {
                            LinkButton lnkbtnDelete = (LinkButton)row.FindControl("lnkbtnDelete");
                            lnkbtnDelete.Enabled = false;
                        }
                    }
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    ddlRecvrDiv.SelectedIndex = 0;
                    ddltocityDiv.SelectedIndex = 0;
                    ddldelvplaceDIv.SelectedIndex = 0;
                    //chkSelectAllRows.Checked = false;
                    lblErrorMsg.Visible = false;
                    LnkBtnNew.Visible = true;
                    Selectuserpref();
                }
                else
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "Gr Details not found";
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    //chkSelectAllRows.Checked = false;
                    // ddlDelvryPlace.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalGrdDtls();", true);

                }
                if (grdMain.Rows.Count > 0)
                {
                    if (RDbRecpt.Checked == true)
                    {
                        rdbAdvanceOrder.Enabled = false;
                        RDbDirect.Enabled = false;
                    }
                    else if (rdbAdvanceOrder.Checked == true)
                    {
                        RDbRecpt.Enabled = false;
                        RDbDirect.Enabled = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (RDbRecpt.Checked)
                {
                    lblGrDetails.Text = "Receipt Detail"; lblSender.InnerText = "Receiver";
                }
                else if (rdbAdvanceOrder.Checked)
                {
                    lblGrDetails.Text = "Advance Order"; lblSender.InnerText = "Party";
                }

                lblErrorMsg.Visible = false;
                GRPrepDAL obj = new GRPrepDAL();
                string strGrFrm = "";
                Int64 Recvr = Convert.ToInt64((ddlRecvrDiv.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlRecvrDiv.SelectedValue));
                Int64 Tocity = Convert.ToInt64((ddltocityDiv.SelectedValue) == "" ? 0 : Convert.ToInt64(ddltocityDiv.SelectedValue));
                Int64 delvplace = Convert.ToInt64((ddldelvplaceDIv.SelectedValue) == "" ? 0 : Convert.ToInt64(ddldelvplaceDIv.SelectedValue));
                Int64 DateRange = Convert.ToInt64((ddlDateRange.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "openModalGrdDtls();", true);

                DataTable DsGrdetail = new DataTable();
                if (rdbAdvanceOrder.Checked)
                {
                    DsGrdetail = obj.selectGrDetails("SelectAdvanceOrderDetail", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFromDiv.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateToDiv.Text)), Recvr, Tocity, delvplace, DateRange, ApplicationFunction.ConnectionString());
                }
                else
                {
                    DsGrdetail = obj.selectGrDetails("SelectRcptDetail", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFromDiv.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateToDiv.Text)), Recvr, Tocity, delvplace, DateRange, ApplicationFunction.ConnectionString());
                }

                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    if (rdbAdvanceOrder.Checked == true)
                    {
                        grdGrdetals.Columns[1].HeaderText = "Order No.";
                        grdGrdetals.Columns[6].HeaderText = "Party Name";
                        grdGrdetals.Columns[5].Visible = false;
                    }
                    if (RDbRecpt.Checked == true)
                        grdGrdetals.Columns[2].Visible = false;

                    grdGrdetals.DataBind(); BtnClerForPurOdr.Visible = true;
                    lnkbtnSubmit.Visible = true;
                    //chkSelectAllRows.Visible = true;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind(); BtnClerForPurOdr.Visible = false;
                    lnkbtnSubmit.Visible = false;
                    //chkSelectAllRows.Visible = false;
                }

            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void BtnClerForPurOdr_Click(object sender, EventArgs e)
        {
            ddlstate.SelectedValue = "0"; ddlcity.SelectedValue = "0"; ddlcityarea.SelectedIndex = -1; ddlDistrict.SelectedIndex = -1; ddlTehsil.SelectedIndex = -1; ddlPost.SelectedIndex = -1;
            txtemail.Text = string.Empty;
            txtadd1.Text = string.Empty;
            txtadd2.Text = string.Empty; txtpinno.Text = string.Empty; txtphoneno.Text = string.Empty; txtPhoneNoRes.Text = string.Empty; txtmobileno.Text = string.Empty; txtfaxno.Text = string.Empty; txtRegnPlace.Text = string.Empty; txtTinno.Text = string.Empty; txtDOB.Text = string.Empty; txtDOA.Text = string.Empty;
            txtTempAddr1.Text = string.Empty; txtTempAddr2.Text = string.Empty; txtTempAddr3.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "HideClient()", true);
        }

        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)
        {
            ClearAll(); ClearItems();
            Response.Redirect("GrPrep.aspx");
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            userpref();
            GRPrepDAL objGR = new GRPrepDAL(); Int64 MaxGRNo = 0; string GRfrom = "BK"; Int32 FromCityIdno = 0; FromCityIdno = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
            MaxGRNo = objGR.MaxNo(GRfrom, Convert.ToInt32(ddlDateRange.SelectedValue), FromCityIdno, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxGRNo);
        }
        protected void lnkbtnAtSave_Click(object sender, EventArgs e)
        {
            txtGRNo.Focus(); txtGRNo.SelectText();
        }
        protected void lnkbtnAtSave1_Click(object sender, EventArgs e)
        {
            userpref();
            GRPrepDAL objGR = new GRPrepDAL(); Int64 MaxGRNo = 0; string GRfrom = "BK"; Int32 FromCityIdno = 0; FromCityIdno = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
            MaxGRNo = objGR.MaxNo(GRfrom, Convert.ToInt32(ddlDateRange.SelectedValue), FromCityIdno, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxGRNo);
            lnkbtnSave_OnClick(null, null);
        }
        protected void lnkBtnSender_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var senderLst = obj.BindSender();
                obj = null;
                ddlSender.DataSource = senderLst;
                ddlSender.DataTextField = "Acnt_Name";
                ddlSender.DataValueField = "Acnt_Idno";
                ddlSender.DataBind();
                ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkBtnReceiver_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var receiverLst = obj.BindSender();
                obj = null;
                ddlReceiver.DataSource = receiverLst;
                ddlReceiver.DataTextField = "acnt_name";
                ddlReceiver.DataValueField = "acnt_idno";
                ddlReceiver.DataBind();
                ddlReceiver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkBtnFromCity_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var ToCity = obj.BindLocFrom();
                obj = null;
                ddlFromCity.DataSource = ToCity;
                ddlFromCity.DataTextField = "city_name";
                ddlFromCity.DataValueField = "city_idno";
                ddlFromCity.DataBind();
                ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkBtnDelvryPlace_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var ToCity = obj.BindAllToCity();
                obj = null;
                ddlLocation.DataSource = ToCity;
                ddlLocation.DataTextField = "city_name";
                ddlLocation.DataValueField = "city_idno";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkBtnAgent_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var Agent = obj.BindAgent();
                obj = null;
                ddlParty.DataSource = Agent;
                ddlParty.DataTextField = "Acnt_Name";
                ddlParty.DataValueField = "Acnt_Idno";
                ddlParty.DataBind();
                ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkBtnTocity_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var ToCity = obj.BindAllToCity();
                obj = null;

                ddlToCity.DataSource = ToCity;
                ddlToCity.DataTextField = "city_name";
                ddlToCity.DataValueField = "city_idno";
                ddlToCity.DataBind();
                ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "ShowClient('dvPrtyDet');", true);
        }
        protected void lnkbtnTruuckRefresh_Click(object sender, EventArgs e)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNowithLastDigit();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        protected void lnkbtnDiNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), txtDelvNo.Text.Trim(), 1);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("DiNo updated successfully.");
            }
            else
            {
                this.ShowMessageErr("DiNo not updated.");
            }
        }
        protected void lnkbtnEgpNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), TxtEGPNo.Text.Trim(), 2);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("EGP No. updated successfully.");
            }
            else
            {
                this.ShowMessageErr("EGP No. not updated.");
            }
        }
        protected void lnkbtnInvNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), txtRefNo.Text.Trim(), 3);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage(lblrefrename.Text + " updated successfully.");
            }
            else
            {
                this.ShowMessageErr(lblrefrename.Text + " not updated.");
            }
        }
        protected void lnkbtnManNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), txtManualNo.Text.Trim(), 4);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("Manual No. updated successfully.");
            }
            else
            {
                this.ShowMessageErr("Manual No. not updated.");
            }
        }

        protected void lnkbtnOrdrNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), txtOrderNo.Text.Trim(), 5);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("Order No. updated successfully.");
            }
            else
            {
                this.ShowMessageErr("Order No. not updated.");
            }
        }
        protected void lnkbtnFormNoUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateData(Convert.ToInt64(Request.QueryString["Gr"]), txtFromNo.Text.Trim(), 6);
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("Form No. updated successfully.");
            }
            else
            {
                this.ShowMessageErr("Form No. not updated.");
            }
        }
        protected void lnkbtnRemarkUpdate_Click(object sender, EventArgs e)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 intGrPrepIdno = obj.UpdateRemarkData(Convert.ToInt64(Request.QueryString["Gr"]), TxtRemark.Text.Trim());
            if (intGrPrepIdno > 0)
            {
                this.ShowMessage("Remark updated successfully.");
            }
            else
            {
                this.ShowMessageErr("Remark not updated!");
            }
        }


        protected void lnkbtnContnrDtl_OnClick(object sender, EventArgs e)
        {
            txtContainrNo.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        protected void lnkbtnContainerSubmit_OnClick(object sender, EventArgs e)
        {
            HiddConSize.Value = ddlContainerSize.SelectedValue;
            txtshipment.Focus();
        }
        protected void lnkbtnClose_OnClick(object sender, EventArgs e)
        {
            txtshipment.Focus();
            txtContainrNo.Text = txtContainerSealNo.Text = "";
            ddlContainerSize.SelectedValue = ddlContainerType.SelectedValue = "0";
        }
        protected void lnkBtnKM_OnClick(object sender, EventArgs e)
        {
            txtToKM.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModalKM();", true);
        }
        protected void lnkbtnAccPosting_Click(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["Userclass"]) == "SuperAdmin" || (Convert.ToString(Session["Userclass"]) == "Admin"))
            //{

            int Count = 0; 
            GRPrepDAL obj = new GRPrepDAL();
            BindDropdownDAL obj1 = new BindDropdownDAL();
            clsAccountPosting objVATInvoicePOSDAL = new clsAccountPosting();
            tblUserPref hiduserpref = obj.selectuserpref();
            DataSet objDataSet = objVATInvoicePOSDAL.AccPosting(ApplicationFunction.ConnectionString(), "GRPOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            {
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
                        {
                            DataTable dtDet = CreateDt();
                            TblGrHead objGRHead = obj.SelectTblGRHead(Convert.ToInt32(objDataSet.Tables[0].Rows[i]["Gr_Idno"]));
                            DataTable objGRDetl = obj.SelectGRDetail(Convert.ToInt32(objDataSet.Tables[0].Rows[i]["Gr_Idno"]), ApplicationFunction.ConnectionString());
                            Int64 intAcnttype = 0;
                            Int32 RecptType = Convert.ToInt32(Convert.ToString(objGRHead.RcptType_Idno) == "0" ? 0 : Convert.ToInt32(objGRHead.RcptType_Idno));
                            if (RecptType > 0)
                            {
                                DataTable dt = obj1.BindRcptTypeDel(RecptType, ApplicationFunction.ConnectionString()); intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                            }
                            for (int k = 0; k < objGRDetl.Rows.Count; k++)
                            {
                                //int id = dtDet.Rows.Count == 0 ? 1 : (Convert.ToInt32(objGRDetl.Rows[k]["id"])) + 1;
                                int id = dtDet.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtDet.Rows[k]["id"])) + 1;
                                double ShrtgRate = obj.SelectQtyShrtgRate(Convert.ToInt32(objGRDetl.Rows[k]["Item_Idno"]), Convert.ToInt32(objGRHead.To_City), Convert.ToInt32(objGRHead.From_City), Convert.ToDateTime(objGRHead.Gr_Date), Convert.ToInt64(objGRHead.Cityvia_Idno));
                                double ShrtgLimit = obj.SelectQtyShrtgLimit(Convert.ToInt32(objGRDetl.Rows[k]["Item_Idno"]), Convert.ToInt32(objGRHead.To_City), Convert.ToInt32(objGRHead.From_City), Convert.ToDateTime(objGRHead.Gr_Date), Convert.ToInt64(objGRHead.Cityvia_Idno));
                                double ShrtgRateOther = obj.SelectWghtShrtgRate(Convert.ToInt32(objGRDetl.Rows[k]["Item_Idno"]), Convert.ToInt32(objGRHead.To_City), Convert.ToInt32(objGRHead.From_City), Convert.ToDateTime(objGRHead.Gr_Date), Convert.ToInt64(objGRHead.Cityvia_Idno));
                                double ShrtgLimitOther = obj.SelectWghtShrtgLimit(Convert.ToInt32(objGRDetl.Rows[k]["Item_Idno"]), Convert.ToInt32(objGRHead.To_City), Convert.ToInt32(objGRHead.From_City), Convert.ToDateTime(objGRHead.Gr_Date), Convert.ToInt64(objGRHead.Cityvia_Idno));
                                ApplicationFunction.DatatableAddRow(dtDet, id, objGRDetl.Rows[k]["Item_Name"], objGRDetl.Rows[k]["Item_Idno"], objGRDetl.Rows[k]["UOM_Name"], objGRDetl.Rows[k]["Unit_Idno"], objGRDetl.Rows[k]["Rate_Type"], objGRDetl.Rows[k]["RateType_Idno"], objGRDetl.Rows[k]["Qty"], objGRDetl.Rows[k]["Tot_Weght"], objGRDetl.Rows[k]["Item_Rate"], objGRDetl.Rows[k]["Amount"], objGRDetl.Rows[k]["Detail"], ShrtgRate, ShrtgLimit, ShrtgLimitOther, ShrtgRateOther, "", "", objGRDetl.Rows[k]["Grade_Name"].ToString(), objGRDetl.Rows[k]["ItemGrade_Idno"].ToString());
                            }
                            if (this.PostIntoAccounts(dtDet, objGRHead.GR_Idno, "GR", (Convert.ToString(objGRHead.RndOff_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.RndOff_Amnt)), 0, 0, 0, 0, (Convert.ToDouble(Convert.ToString(objGRHead.SubTot_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.SubTot_Amnt)) - Convert.ToDouble(Convert.ToString(objGRHead.Gross_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.Gross_Amnt))), Convert.ToDouble(Convert.ToString(objGRHead.Net_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.Net_Amnt)), Convert.ToDouble(Convert.ToString(objGRHead.ServTax_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.ServTax_Amnt)), Convert.ToDouble(Convert.ToString(objGRHead.Gross_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.Gross_Amnt)), Convert.ToDouble(Convert.ToString(objGRHead.RndOff_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.RndOff_Amnt)), Convert.ToDouble(Convert.ToString(objGRHead.SwchBrtTax_Amt) == "" ? 0 : Convert.ToDouble(objGRHead.SwchBrtTax_Amt)), Convert.ToDouble(Convert.ToString(objGRHead.KisanKalyan_Amnt) == "" ? 0 : Convert.ToDouble(objGRHead.KisanKalyan_Amnt)), Convert.ToInt32(objGRHead.RcptType_Idno), ((intAcnttype == 4) ? Convert.ToString(objGRHead.Inst_No) : ""), ((intAcnttype == 4) ? Convert.ToDateTime(objGRHead.Inst_Dt).ToString("dd-MM-yyyy") : ""), ((intAcnttype == 4) ? Convert.ToString(objGRHead.Bank_Idno) : "0"), Convert.ToInt32(objGRHead.GR_Typ), Convert.ToInt32(objGRHead.Sender_Idno), Convert.ToInt32(objGRHead.STax_Typ), Convert.ToDateTime(objGRHead.Gr_Date).ToString("dd-MM-yyyy"), Convert.ToInt32(objGRHead.Year_Idno), Convert.ToInt32(hiduserpref.InvGen_GrType), Convert.ToDouble(txtSGSTAmnt.Text), Convert.ToDouble(txtCGSTAmnt.Text), Convert.ToDouble(txtIGSTAmnt.Text)) == true)
                            {
                                Count = Count + 1;
                                obj.UpdateIsPosting(Convert.ToInt64(objDataSet.Tables[0].Rows[i]["Gr_Idno"]));
                                obj.UpdateFlag(ApplicationFunction.ConnectionString(), Convert.ToInt64(objDataSet.Tables[0].Rows[i]["Gr_Idno"]));
                                tScope.Complete(); tScope.Dispose();
                            }
                            else
                            {
                                tScope.Dispose();
                                this.ShowMessageErr(hidpostingmsg.Value);
                            }
                        }
                        if (Count > 0)
                        {
                            this.PostingLeft();
                            this.ShowMessage("Record's " + Count + " Account Posting successfully done.");
                        }
                    }
                    catch (Exception ex)
                    {
                        tScope.Dispose();
                        this.PostingLeft();
                    }
                }
            }
            else
            {
                this.ShowMessageErr("No records found.");
            }

            //}
        }
        protected void LnkBtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("GRPrep.aspx");
            }
            catch (Exception)
            {
            }
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
                                GRPrepDAL obj = new GRPrepDAL();
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
            CvtxtRate.Enabled = false;
        }
        protected void lnkBtnCityvia_Click(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var ToCity = obj.BindAllToCity();
                obj = null;
                ddlCityVia.DataSource = ToCity;
                ddlCityVia.DataTextField = "city_name";
                ddlCityVia.DataValueField = "city_idno";
                ddlCityVia.DataBind();
                ddlCityVia.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkChlnGen_Click(object sender, EventArgs e)
        {
            Int64 ChlnIdno = Convert.ToInt64(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, "select isnull(Chln_Idno,0) as Chln_Idno from tblGrHead where GR_Idno=" + Convert.ToString(Request.QueryString["Gr"]) + ""));
            if (ChlnIdno > 0)
                Response.Redirect("ChlnAmntPayment.aspx?ChlnIdno=" + ChlnIdno + "");
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('Challan Not Genrated for GR.')", true);
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
            if (txtGRDate.Text.Trim() != "")
            {
                if (txtGRDate.Text != "")
                {
                    string dt = GetGSTDate();
                    if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                    {
                        pnlServiceTax.Visible = false;
                        pnlKrishiTax.Visible = false;
                        pnlSwatchBharatTax.Visible = false;
                        int gstType = GetGSTType();
                        if (gstType == 1)
                        {
                            pnlCGST.Visible = true;
                            pnlSGST.Visible = true;
                            pnlIGST.Visible = false;
                        }
                        else if (gstType == 2)
                        {
                            pnlCGST.Visible = false;
                            pnlSGST.Visible = false;
                            pnlIGST.Visible = true;
                        }
                        GetGSTValues();
                    }
                    else
                    {
                        pnlCGST.Visible = false;
                        pnlSGST.Visible = false;
                        pnlIGST.Visible = false;
                        pnlServiceTax.Visible = true;
                        pnlKrishiTax.Visible = true;
                        pnlSwatchBharatTax.Visible = true;
                    }
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
            hidSGSTPer.Value = Convert.ToString(objGrprepDAL.SelectSGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            hidCGSTPer.Value = Convert.ToString(objGrprepDAL.SelectCGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            hidIGSTPer.Value = Convert.ToString(objGrprepDAL.SelectIGSTMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
            //Presently GST Cess not being used
            hidGSTCessPer.Value = "0";
        }

        //Upadhyay #GST
        public void ToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GstCalGR();
            GetGSTType();
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

        #region Bind Event...
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

        private void BindItemGrade()
        {
            GRPrepDAL obj = new GRPrepDAL();
            var grade = obj.ItemGrade();
            ddlItemGrade.DataSource = grade;
            ddlItemGrade.DataTextField = "Grade_Name";
            ddlItemGrade.DataValueField = "Grade_Idno";
            ddlItemGrade.DataBind();
            ddlItemGrade.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            
            //Renaming Un-Assigned lorry text
            if (ddlTruckNo.Items.FindByText("gned[Un-Assigned]").Text != null)
            {
                ddlTruckNo.Items.FindByText("gned[Un-Assigned]").Text = "Un-Assigned";
            }

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



            ddltocityDiv.DataSource = ToCity;
            ddltocityDiv.DataTextField = "city_name";
            ddltocityDiv.DataValueField = "city_idno";
            ddltocityDiv.DataBind();
            ddltocityDiv.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddldelvplaceDIv.DataSource = ToCity;
            ddldelvplaceDIv.DataTextField = "city_name";
            ddldelvplaceDIv.DataValueField = "city_idno";
            ddldelvplaceDIv.DataBind();
            ddldelvplaceDIv.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlRecvrDiv.DataSource = senderLst;
            ddlRecvrDiv.DataTextField = "Acnt_Name";
            ddlRecvrDiv.DataValueField = "Acnt_Idno";
            ddlRecvrDiv.DataBind();
            ddlRecvrDiv.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindParty()
        {
            //var Prty = objclsReceiptEntry.BindParty();
            //ddlParty.DataSource = Prty;
            //ddlParty.DataTextField = "Acnt_Name";
            //ddlParty.DataValueField = "Acnt_Idno";
            //ddlParty.DataBind();
            //ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Item_Name", "String",
                "Item_Idno", "String",
                "Unit_Name", "String",
                "Unit_Idno", "String",
                "Rate_Type", "String",
                "Rate_TypeIdno", "String",
                "Quantity", "String",
                "Weight", "String",
                "Rate", "String",
                "Amount", "String",
                "Detail", "String",
                "Shrtg_Limit", "String",
                "Shrtg_Rate", "String",
                 "Shrtg_Limit_Other", "String",
                "Shrtg_Rate_Other", "String",
                "PREV_BAL", "String",
                "PREV_QTY", "String",
                "Grade_Name", "String",
                "Grade_Idno", "String", 
                "UnloadWeight", "String"
                );
            return dttemp;
        }
        private void BindMaxNo(string GRfrom, Int32 FromCityIdno, Int32 YearId)
        {
            GRPrepDAL obj = new GRPrepDAL();
            Int64 MaxNo = obj.MaxNo(GRfrom, YearId, FromCityIdno, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxNo);
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
                    if (rdbAdvanceOrder.Checked == true)
                    {
                        //grdMain.Columns[0].Visible = false;
                        grdMain.Columns[0].Visible = true;
                        grdMain.Columns[7].Visible = true;
                        grdMain.Columns[8].Visible = true;
                        foreach (GridViewRow row in grdMain.Rows)
                        {
                            LinkButton lnkbtnDelete = (LinkButton)row.FindControl("lnkbtnDelete");
                            lnkbtnDelete.Visible = false;
                        }
                    }
                    else
                    {
                        grdMain.Columns[7].Visible = true;
                        grdMain.Columns[8].Visible = false;
                    }

                }
                else
                {

                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                    txtGrossAmnt.Text = txtCartage.Text = txtTotalAmnt.Text = txtSurchrge.Text = txtCommission.Text = txtBilty.Text = "0.00";
                    txtWages.Text = txtPF.Text = txtSubTotal.Text = txtNetAmnt.Text = txtServTax.Text = txtSwchhBhartTx.Text = txtTollTax.Text = "0.00";
                }
            }
            else
            {
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
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
                    txtDateFromDiv.Text = hidmindate.Value;
                    txtDateToDiv.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtInstDate.Text = hidmindate.Value;
                    txtDateFromDiv.Text = hidmindate.Value;
                    txtDateToDiv.Text = hidmaxdate.Value;
                }
            }

        }
        //private void BindGridT()
        //{
        //    grdMain.DataSource = (DataTable)ViewState["dt"];
        //    grdMain.DataBind();
        //}

        #endregion

        #region Function ...
        public string GetTruckNo(string value)
        {
            string trukNo = value.Substring(5);
            trukNo = trukNo.Substring(0, trukNo.Length - 1);
            return trukNo;
        }
        public void BindContainerDetails()
        {
            GRPrepDAL obj = new GRPrepDAL();
            var varConainerType = obj.GetContainerType();
            ddlContainerType.DataSource = varConainerType;
            ddlContainerType.DataTextField = "Container_Type";
            ddlContainerType.DataValueField = "ContainerType_Idno";
            ddlContainerType.DataBind();
            ddlContainerType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var varConainerSize = obj.GetContainerSize();
            ddlContainerSize.DataSource = varConainerSize;
            ddlContainerSize.DataTextField = "Container_Size";
            ddlContainerSize.DataValueField = "ContainerSize_Idno";
            ddlContainerSize.DataBind();
            ddlContainerSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void AutofillDefault()
        {
            try
            {
                GRPrepDAL obj = new GRPrepDAL(); Int64 Yearidno = 0;
                Yearidno = obj.AutofillYear();
                ddlDateRange.SelectedValue = Convert.ToString(Yearidno == 1 ? "1" : "2");
                txtGRDate.Text = obj.AutofillDate();
            }
            catch (Exception Ex)
            {
            }
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void GstCalGR()
        {
            GRPrepDAL GstCalGR = new GRPrepDAL();
            tblUserPref hidGstCalgr = GstCalGR.selectuserpref();
            HidGstCalgr.Value = Convert.ToString(hidGstCalgr.GstCal_Gr);
            if (hidGstCalgr.GstCal_Gr == true && hidGstCalgr.GstCal_Gr != null)
            {
                ReCalculateGST();
                netamntcal();
            }
            else
            {
                ReGSTCalGrN();
            }
        }


        private void PrintGRPrep(Int64 GRHeadIdno)
        {
            Repeater obj = new Repeater();

            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
            if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
            {
                imgLogoShow.Visible = true;
                byte[] img = hiduserpref.Logo_Image;
                string base64String = Convert.ToBase64String(img, 0, img.Length);
                imgLogoShow.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                imgLogoShow.Visible = false;
                imgLogoShow.ImageUrl = "";
            }
            if (Convert.ToString(hiduserpref.Terms) == "" && Convert.ToString(hiduserpref.Terms1) == "")
            {
                lblTerms.Visible = false;
                lblterms1.Visible = false;

            }
            else
            {
                lblTerms.Visible = true;
                lblterms1.Visible = true;

                lblTerms.Text = "'" + hiduserpref.Terms + "'";
                lblterms1.Text = hiduserpref.Terms1;
            }
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            string CompGSTIN = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            CompGSTIN = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (ServTaxNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = ServTaxNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (CompGSTIN == "")
            {
                lblCompGST.Visible = false; lblCompGSTIN.Visible = false;

            }
            else
            {
                lblCompGSTIN.Text = CompGSTIN;
                lblCompGST.Visible = true; lblCompGSTIN.Visible = true;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PrefixGr_No"])) ? Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]) : Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PrefixGr_No"]) + "-" + Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                lblValueViaCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                try
                {
                    lblGeneratedByName.Text = Convert.ToString((dsReport.Tables["GRPrint"].Rows[0]["GeneratedBy"] == "" || dsReport.Tables["GRPrint"].Rows[0]["GeneratedBy"] == null) ? "" : "Generated by: " + dsReport.Tables["GRPrint"].Rows[0]["GeneratedBy"]);
                    lblLastUpdatedByName.Text = Convert.ToString((dsReport.Tables["GRPrint"].Rows[0]["ModifiedBy"] == "" || dsReport.Tables["GRPrint"].Rows[0]["ModifiedBy"] == null) ? "" : "Last Updated by: " + dsReport.Tables["GRPrint"].Rows[0]["ModifiedBy"]);
                }
                catch (Exception ex)
                {
                    lblGeneratedByName.Text = "";
                    lblLastUpdatedByName.Text = "";
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]) == "")
                {
                    lbltxtagent.Visible = false; lblAgent.Visible = false;
                }
                else
                {
                    lblAgent.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]); lbltxtagent.Visible = true; lblAgent.Visible = true;
                }

                // by lokesh For consignor and consignee portion add 
                lblConsigeeName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsigneeTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"]);

                lblConsignorName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblConsignorTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"]);
                lblPrtyGSTIN.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);
                lblConsignerGSTINValue.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);
                lblLorryNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                // end lokesh code

                // For shipment details & container details by lokesh

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


                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"])) == true)
                {
                    lblNameShipmentno.Visible = false; lblShipmentNo.Visible = false;
                }
                else { lblNameShipmentno.Visible = true; lblShipmentNo.Visible = true; lblShipmentNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"])) == true)
                {
                    lblNameContnrNo.Visible = false; lblContainerNo.Visible = false;
                }
                else { lblNameContnrNo.Visible = true; lblContainerNo.Visible = true; lblContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString(); }

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

                //Ref No.
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true)
                {
                    lblRefNo.Visible = false; lblrefnoval.Visible = false;
                }
                else
                { lblRefNo.Visible = true; lblrefnoval.Visible = true; lblRefNo.Text = lblrefrename.Text; lblrefnoval.Text = dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString(); }
                //

                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"])) == "0")
                {
                    lblTotItem.Visible = false; lblTotItemValue.Visible = false;
                }
                else
                {
                    lblTotItem.Visible = true; lblTotItemValue.Visible = true; lblTotItemValue.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"]).ToString("N2");
                }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true)
                {
                    lblNameContnrType.Visible = false; lblCntnrType.Visible = false;
                }
                else
                { lblNameContnrType.Visible = true; lblCntnrType.Visible = true; lblCntnrType.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"])) == true)
                {
                    lblNameSealNo.Visible = false; lblSealNo.Visible = false;
                }
                else { lblNameSealNo.Visible = true; lblSealNo.Visible = true; lblSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString(); }
                //------------------------- ADD BY PEEYUSH
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true)
                {
                    lblDinNoText.Visible = false; lblDinNo.Visible = false;
                }
                else { lblDinNoText.Visible = true; lblDinNo.Visible = true; lblDinNo.Text = dsReport.Tables["GRPrint"].Rows[0]["DI_NO"].ToString(); }

                //.........................

                lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);

                valuelblcommission.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"]));
                if (valuelblcommission.Text != "")
                {
                    dcmsn = Convert.ToDouble(valuelblcommission.Text);
                }
                else
                {
                    dcmsn = 0;
                }
                valuelblbilty.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"]));
                if (valuelblbilty.Text != "")
                {
                    dblty = Convert.ToDouble(valuelblbilty.Text);
                }
                else
                {
                    dblty = 0;
                }
                valuelblcartage.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartg_Amnt"]));
                if (valuelblcartage.Text != "")
                {
                    dcrtge = Convert.ToDouble(valuelblcartage.Text);
                }
                else
                {
                    dcrtge = 0;
                }
                valuelblsurcharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcrg_Amnt"]));
                if (valuelblsurcharge.Text != "")
                {
                    dsuchge = Convert.ToDouble(valuelblsurcharge.Text);
                }
                else
                {
                    dsuchge = 0;
                }
                if (Convert.ToString(HidsRenWages.Value) != "")
                {
                    lblwages.Text = Convert.ToString(HidsRenWages.Value);
                }
                else
                {
                    lblwages.Text = "Wages";
                }
                if (Convert.ToString(hidRenamePF.Value) != "")
                {
                    lblPFAmnt.Text = Convert.ToString(hidRenamePF.Value);
                }
                else
                {
                    lblPFAmnt.Text = "PF";
                }
                if (Convert.ToString(hidRenameToll.Value) != "")
                {
                    lblTollTax.Text = Convert.ToString(hidRenameToll.Value);
                }
                else
                {
                    lblTollTax.Text = "Toll Tax";
                }
                valuelblwages.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"]));
                if (valuelblwages.Text != "")
                {
                    dwges = Convert.ToDouble(valuelblwages.Text);
                }
                else
                {
                    dwges = 0;
                }
                valuelblPFAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]));
                if (valuelblPFAmnt.Text != "")
                {
                    dPF = Convert.ToDouble(valuelblPFAmnt.Text);
                }
                else
                {
                    dPF = 0;
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["STax_Typ"]) == "1")
                {
                    valuelblservceTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                    if (valuelblservceTax.Text != "")
                    {
                        dtax = Convert.ToDouble(valuelblservceTax.Text);
                    }
                    else
                    {
                        dtax = 0;
                    }
                    valuelblservtaxConsigner.Text = "0.00";
                }
                else
                {
                    valuelblservtaxConsigner.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                    valuelblservceTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                }
                valuelblTollTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]));
                if (valuelblTollTax.Text != "")
                {
                    dtoll = Convert.ToDouble(valuelblTollTax.Text);
                }
                else
                {
                    dtoll = 0;
                }

                //------------------------- ADD BY PEEYUSH
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])) == true)
                {
                    lblGrossAmnt.Visible = false; valuelblGrossAmnt.Visible = false;
                }
                else { lblGrossAmnt.Visible = true; valuelblGrossAmnt.Visible = true; valuelblGrossAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])) == true)
                {
                    valuelblTotal.Visible = false;
                }
                else { valuelblTotal.Visible = true; valuelblTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])); }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["STax_Typ"]) == "1")
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SwchBrtTax_Amt"])) == true)
                    {
                        lblSwachhBhrtTax.Visible = false; valuelblSwachhBhrtTax.Visible = false;
                    }
                    else { lblSwachhBhrtTax.Visible = true; valuelblSwachhBhrtTax.Visible = true; valuelblSwachhBhrtTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SwchBrtTax_Amt"])); }

                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["KisanKalyan_Amnt"])) == true)
                    {
                        lblKisanKalyanTax.Visible = false; ValueKisanKalyanTax.Visible = false;
                    }
                    else { lblKisanKalyanTax.Visible = true; ValueKisanKalyanTax.Visible = true; ValueKisanKalyanTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["KisanKalyan_Amnt"])); }
                }
                else
                {
                    lblSwachhBhrtTax.Visible = true; valuelblSwachhBhrtTax.Visible = true; valuelblSwachhBhrtTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                    lblKisanKalyanTax.Visible = true; ValueKisanKalyanTax.Visible = true; ValueKisanKalyanTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                }

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
                valuelblnetAmnt.Text = string.Format("{0:0,0.00}", (dcmsn + dblty + dcrtge + dPF + dsuchge + dtax + dwges + dtoll + dtotlAmnt));
            }
        }


        private void PrintGRPrepJainBulk(Int64 GRHeadIdno)
        {
            Repeater obj = new Repeater();
            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string email = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            email = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname1.Text = CompName; //lblCompname1.Text = "For - " + CompName;
            lblCompAdd3.Text = Add1;
            lblCompAdd4.Text = Add2;
            lblCompCity1.Text = City;
            lblCompState1.Text = State;
            //lblCompPhNo1.Text = PhNo;
            //if (FaxNo == "")
            //{
            //    lblCompFaxNo1.Visible = false; lblFaxNo1.Visible = false;
            //}
            //else
            //{
            //    lblCompFaxNo1.Text = FaxNo;
            //    lblCompFaxNo1.Visible = true; lblFaxNo1.Visible = true;
            //}
            //if (email == "")
            //{
            //    lblEmail.Visible = false; lblEmailValue.Visible = false;
            //}
            //else
            //{
            //    lblEmailValue.Text = email;
            //    lblEmail.Visible = true; lblEmailValue.Visible = true;
            //}
            if (ServTaxNo == "")
            {
                lblCompTIN1.Visible = false; lblTin1.Visible = false;
            }
            else
            {
                lblCompTIN1.Text = ServTaxNo;
                lblCompTIN1.Visible = true; lblTin1.Visible = true;
            }
            if (PanNo == "")
            {
                lblPanNo1.Visible = false; lbltxtPanNo1.Visible = false;
            }
            else
            {
                lblPanNo1.Text = PanNo;
                lblPanNo1.Visible = true; lbltxtPanNo1.Visible = true;
            }
            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
                {
                    ImgLogoJain.Visible = true;
                    byte[] img = hiduserpref.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    ImgLogoJain.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    ImgLogoJain.Visible = false;
                    ImgLogoJain.ImageUrl = "";
                }

                lblGRno1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate1.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblJainVia.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblConsigeeName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsignorName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);

                lblLorryNo1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"])) == true) { DivJainShipNo.Visible = false; }
                else { DivJainShipNo.Visible = true; lblJainShipNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"])) == true) { DivJainContainerNo.Visible = false; }
                else { DivJainContainerNo.Visible = true; lblJainContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"])) == true) { DivJainSealNo.Visible = false; }
                else { DivJainSealNo.Visible = true; lblJainSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true) { DivJainOrderNo.Visible = false; }
                else { DivJainOrderNo.Visible = true; lblJainShipNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true) { DivJainFormNo.Visible = false; }
                else { DivJainFormNo.Visible = true; lblJainFormNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Form_No"].ToString(); }


                lblItemName.Text = dsReport.Tables["GRPrint"].Rows[0]["Item_Modl"].ToString();
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Rate_Type"]) == "1")
                    lblItemQty.Text = dsReport.Tables["GRPrint"].Rows[0]["Qty"].ToString();
                else
                    lblItemQty.Text = dsReport.Tables["GRPrint"].Rows[0]["Tot_Weght"].ToString();

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "1")
                    lblGRType.Text = "PAID GR";
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "2")
                    lblGRType.Text = "TO BE BILLED";
                else
                    lblGRType.Text = "TO PAY GR";
                if (Convert.ToInt32(dsReport.Tables["GRPrint"].Rows[0]["Inv_No"]) > 0)
                    lblInvNoValue.Text = dsReport.Tables["GRPrint"].Rows[0]["Inv_No"].ToString();
                lblNetValue.Text = Convert.ToString(Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Net_Amnt"]).ToString("N2"));
            }
        }
        private void cbena()
        {
            ddlSender.SelectedIndex = -1; ddlReceiver.SelectedIndex = -1; ddlParty.SelectedIndex = -1; ddlTruckNo.SelectedIndex = -1; ddlLocation.SelectedIndex = -1;
            ddlSender.Enabled = ddlReceiver.Enabled = ddlParty.Enabled = ddlTruckNo.Enabled = ddlLocation.Enabled = true;
            lblDelvNo.Visible = false; txtDelvNo.Visible = false;
        }
        private void cbdis()
        {
            ddlSender.SelectedIndex = ddlSender.SelectedIndex = -1;
            ddlSender.Enabled = false;
            ddlReceiver.Enabled = ddlParty.Enabled = ddlTruckNo.Enabled = ddlLocation.Enabled = true;
            lblDelvNo.Visible = false; txtDelvNo.Visible = false;
        }
        private void cbdisDelNote()
        {
            ddlSender.SelectedIndex = -1; ddlReceiver.SelectedIndex = -1; ddlParty.SelectedIndex = -1; ddlTruckNo.SelectedIndex = -1; ddlLocation.SelectedIndex = -1;
            ddlSender.Enabled = ddlReceiver.Enabled = ddlParty.Enabled = ddlTruckNo.Enabled = ddlLocation.Enabled = false;
            lblDelvNo.Visible = true; txtDelvNo.Visible = true;
        }
        private void panVisi()
        {
            // panBillNoDate.Visible = true;
            // lblGoodLifeAmnt.Visible = txtVGoodLife.Visible = true;
            //txtCHighScrty.Visible = true;
            //DivHideSecurity.Visible = true;

        }
        private void panUnVisi()
        {
            //    panBillNoDate.Visible = false;
            //lblGoodLifeAmnt.Visible = txtVGoodLife.Visible = false;
            //txtVGoodLife.Text = "0";
            //txtCHighScrty.Visible = false;
            //DivHideSecurity.Visible = false;

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
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);

        }
        private void ClearAtFromCityChanged()
        {
            hidGRHeadIdno.Value = string.Empty;
            hidDelvIdno.Value = string.Empty;
            ddlTruckNo.SelectedIndex = ddlParty.SelectedIndex = ddlRcptType.SelectedIndex = ddlLocation.SelectedIndex = ddlToCity.SelectedIndex = 0;
            txtDelvNo.Text = txtInstNo.Text = TxtEGPNo.Text = "";
            txtGrossAmnt.Text = txtCommission.Text = txtTollTax.Text = txtCartage.Text = txtBilty.Text = txtSubTotal.Text = txtTotalAmnt.Text = txtWages.Text = txtServTax.Text = txtSurchrge.Text = txtPF.Text = txtNetAmnt.Text = TxtRoundOff.Text = "0.00";
            hidrowid.Value = ""; lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0; ddlunitname.SelectedIndex = 0; ddlRateType.SelectedIndex = 0; txtInstNo.Text = "";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtdetail.Text = ""; txtrate.Text = "0.00"; txtul.Text = "0";

            grdMain.DataSource = null;
            grdMain.DataBind();

        }

        private void PostingLeft()
        {
            clsAccountPosting clsobj = new clsAccountPosting();
            DataSet objDataSets = clsobj.AccPosting(ApplicationFunction.ConnectionString(), "GRPOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
            if (objDataSets != null && objDataSets.Tables.Count > 0 && objDataSets.Tables[1].Rows.Count > 0)
            {
                lblPostingLeft.Text = "Record(s): " + Convert.ToString(objDataSets.Tables[1].Rows[0][0]);
            }
            else
            {
                lblPostingLeft.Text = "Record(s): 0";
            }
        }
        private void ClearAll()
        {
            txtContainrNo.Text = txtContainerSealNo.Text = "";
            ddlContainerSize.SelectedValue = ddlContainerType.SelectedValue = "0";
            Session["GRDate"] = txtGRDate.Text.Trim();
            //hidGRHeadIdno.Value = string.Empty;
            ddlGRType.Enabled = true;
            hidrowid.Value = ""; hidDelvIdno.Value = string.Empty;
            ddlSender.SelectedIndex = ddlReceiver.SelectedIndex = ddlTruckNo.SelectedIndex = ddlParty.SelectedIndex = ddlRcptType.SelectedIndex = ddlLocation.SelectedIndex = ddlToCity.SelectedIndex = 0;
            txtDelvNo.Text = txtInstNo.Text = TxtEGPNo.Text = txtPrefixNo.Text = "";
            txtshipment.Text = "";
            ddlGRType.SelectedIndex = 1;
            //RDbDirect.Checked = true; --jeet
            ddlSrvcetax.SelectedIndex = 1;
            ddlItemName.SelectedIndex = ddlunitname.SelectedIndex = ddlRateType.SelectedIndex = 0;
            txtEWayBillNo.Text = txtTaxInvoiceNo.Text = txtExcInvoceNO.Text= txtGrossAmnt.Text = txtCommission.Text = txtTollTax.Text = txtCartage.Text = txtBilty.Text = txtSubTotal.Text = txtTotalAmnt.Text = txtWages.Text = txtServTax.Text = txtSurchrge.Text = txtPF.Text = txtNetAmnt.Text = TxtRoundOff.Text = "0.00";
            ViewState["dt"] = dtTemp = null;
            grdMain.DataSource = dtTemp;
            grdMain.DataBind();
        }
        private void ClearItems()
        {
            hidrowid.Value = ""; lblmessage.Text = "";
            if (DivCommission.Visible == true) { txtItmCommission.Text = "0.00"; }
            ddlItemName.Enabled = ddlunitname.Enabled = true;
            ddlItemName.SelectedIndex = 0;
            if (IsWeight == true)
                ddlRateType.SelectedIndex = 1;
            else
            {
                ddlRateType.SelectedIndex = 0;
                ddlRateType.Enabled = false;
            }

            txtInstNo.Text = "";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtdetail.Text = ""; txtrate.Text = "0.00"; txtPrevBal.Text = "0.00"; txtul.Text = "0";
        }

        public void Populate(Int32 intGRIdno)
        {
            //txtGRDate.Enabled = false;
            GRPrepDAL obj = new GRPrepDAL();
            TblGrHead objGRHead = obj.SelectTblGRHead(intGRIdno);
            DataTable objGRDetl = obj.SelectGRDetail(intGRIdno, ApplicationFunction.ConnectionString());
            hidGRHeadIdno.Value = Convert.ToString(intGRIdno);
            if (objGRHead != null)
            {
                GrRestrictDate.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gr_Date)) ? "" : Convert.ToDateTime(objGRHead.Gr_Date).ToString("dd-MM-yyyy");
                ddlDateRange.SelectedValue = Convert.ToString(objGRHead.Year_Idno);
                txtGRDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gr_Date)) ? "" : Convert.ToDateTime(objGRHead.Gr_Date).ToString("dd-MM-yyyy");
                iGrAgainst = Convert.ToInt32(objGRHead.GR_Agnst);

                if (iGrAgainst == 1) { RDbDirect.Checked = true; RDbRecpt.Checked = false; rdbAdvanceOrder.Checked = false; lnkbtnGrAgain.Visible = false; }
                else if (iGrAgainst == 2)
                {
                    RDbRecpt.Checked = true; RDbDirect.Checked = false; rdbAdvanceOrder.Checked = false; lnkbtnGrAgain.Visible = true; lnkbtnGrAgain.Visible = false;
                    ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlDateRange.Enabled = false; RDbDirect.Enabled = false;
                }
                else
                {
                    rdbAdvanceOrder.Checked = true; RDbRecpt.Checked = false; RDbDirect.Checked = false; lnkbtnGrAgain.Visible = true; lnkbtnGrAgain.Visible = false;
                    ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlDateRange.Enabled = false; RDbDirect.Enabled = false;
                }

                RDbDirect.Enabled = false; RDbRecpt.Enabled = false; lnkbtnGrAgain.EnableViewState = false; rdbAdvanceOrder.Enabled = false;
                if ((RDbRecpt.Checked == true) || (rdbAdvanceOrder.Checked == true)) { ddlSender.Enabled = false; ddlReceiver.Enabled = false; ddlFromCity.Enabled = false; ddlToCity.Enabled = false; ddlLocation.Enabled = false; ddlParty.Enabled = false; }
                else { ddlSender.Enabled = true; ddlReceiver.Enabled = true; ddlFromCity.Enabled = true; ddlToCity.Enabled = true; ddlLocation.Enabled = true; ddlParty.Enabled = true; }
                ddlGRType.SelectedValue = Convert.ToString(objGRHead.GR_Typ);// == "" ? "0" : Convert.ToString(objGRHead.GR_Typ);
                lblPrintHeadng.Text = "Goods Receipt - " + Convert.ToString((objGRHead.GR_Typ) == 1 ? "Paid GR" : (objGRHead.GR_Typ == 2) ? "TBB GR" : "To Pay GR");
                lblTypeOfGr.Text = "(" + Convert.ToString((objGRHead.GR_Typ) == 1 ? "Item Wise" : (objGRHead.GR_Typ == 2) ? "Fixed Amount Wise" : "Item Wise") + ")";
                ddlGRType.Enabled = false; txtGRNo.Visible = true;
                txtDelvNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.DI_NO)) ? "" : Convert.ToString(objGRHead.DI_NO);
                txtTaxInvoiceNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Tax_InvNo)) ? "" : Convert.ToString(objGRHead.Tax_InvNo);
                TxtEGPNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.EGP_NO)) ? "" : Convert.ToString(objGRHead.EGP_NO);
                txtGRNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gr_No)) ? "" : Convert.ToString(objGRHead.Gr_No);

                txtContainrNo.Text = Convert.ToString(objGRHead.GRContanr_No);
                txtContainerSealNo.Text = Convert.ToString(objGRHead.GRContanr_SealNo);
                txtContainrNo2.Text = Convert.ToString(objGRHead.GRContanr_No2);
                txtContainerSealNo2.Text = Convert.ToString(objGRHead.GRContanr_SealNo2);
                txtNameI.Text = Convert.ToString(objGRHead.ChaFrwdr_Name);
                ddlTypeI.SelectedValue = Convert.ToString(objGRHead.ImpExp_idno);


                ddlContainerSize.SelectedValue = Convert.ToString(objGRHead.GRContanr_Size);
                ddlContainerType.SelectedValue = Convert.ToString(objGRHead.GRContanr_Type);
                txtPrefixNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.PrefixGr_No)) ? "" : Convert.ToString(objGRHead.PrefixGr_No);

                txtTotItemPrice.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.TotItem_Value)) ? "" : Convert.ToDouble(objGRHead.TotItem_Value).ToString("N2");

                ddlSender.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Sender_Idno)) ? "0" : Convert.ToString(objGRHead.Sender_Idno);
                ddlTruckNo_SelectedIndexChanged(null, null);
                ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Lorry_Idno)) ? "0" : Convert.ToString(objGRHead.Lorry_Idno);
                HiddTruckIdno.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.Lorry_Idno)) ? "0" : Convert.ToString(objGRHead.Lorry_Idno);
                ddlReceiver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Recivr_Idno)) ? "0" : Convert.ToString(objGRHead.Recivr_Idno);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.From_City)) ? "0" : Convert.ToString(objGRHead.From_City);
                ddlToCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.To_City)) ? "0" : Convert.ToString(objGRHead.To_City);
                ddlCityVia.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Cityvia_Idno)) ? "0" : Convert.ToString(objGRHead.Cityvia_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Agnt_Idno)) ? "0" : Convert.ToString(objGRHead.Agnt_Idno);
                TxtRemark.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Remark)) ? "" : Convert.ToString(objGRHead.Remark);
                ddlType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.TypeId)) ? "0" : Convert.ToString(objGRHead.TypeId);
                hidDelPlace.Value = Convert.ToString(objGRHead.TypeDelPlace);

                if (objGRHead.TypeDelPlace == true)
                {
                    txtLoc.Visible = true;
                    rfvTxtLoc.Enabled = true;
                    ddlLocation.Visible = false;
                    rfvLocation.Enabled = false;
                    txtLoc.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.DelPlace_Val)) ? "" : Convert.ToString(objGRHead.DelPlace_Val);
                    lnkBtnDelvryPlace.Visible = false;
                }
                else
                {
                    lnkBtnDelvryPlace.Visible = true;
                    txtLoc.Visible = false;
                    rfvTxtLoc.Enabled = false;
                    ddlLocation.Visible = true;
                    rfvLocation.Enabled = true;
                    ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.DelvryPlce_Idno)) ? "0" : Convert.ToString(objGRHead.DelvryPlce_Idno);
                }
                if (objGRHead.TypeId == 2) { txtFixedAmount.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gross_Amnt)) ? "0.00" : String.Format("{0:0.00}", objGRHead.Gross_Amnt); DivAmount.Visible = true; }
                else { txtFixedAmount.Text = "0.00"; DivAmount.Visible = false; }
                txtRefNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Ref_No)) ? "" : Convert.ToString(objGRHead.Ref_No);
                txtManualNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.ManualNo)) ? "" : Convert.ToString(objGRHead.ManualNo);
                txtshipment.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Shipment_No)) ? "" : Convert.ToString(objGRHead.Shipment_No);
                txtPortNum.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.GRContanr_Port)) ? "" : Convert.ToString(objGRHead.GRContanr_Port);
                txtOrderNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Ordr_No)) ? "" : Convert.ToString(objGRHead.Ordr_No);
                txtFromNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Form_No)) ? "" : Convert.ToString(objGRHead.Form_No);
                txtEGPDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.EGP_Date)) ? "" : Convert.ToDateTime(objGRHead.EGP_Date).ToString("dd-MM-yyyy");
                txtconsnr.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Consignor_Name)) ? "" : Convert.ToString(objGRHead.Consignor_Name);
                txtEWayBillNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.EWay_BillNO)) ? "" : Convert.ToString(objGRHead.EWay_BillNO);
                txtDIDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.DI_Date)) ? "" : Convert.ToDateTime(objGRHead.DI_Date).ToString("dd-MM-yyyy");
                txtInvDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Tax_Date)) ? "" : Convert.ToDateTime(objGRHead.Tax_Date).ToString("dd-MM-yyyy");
                //txtTaxInvoiceNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Tax_InvNo)) ? "" : Convert.ToString(objGRHead.Tax_InvNo);
                //txtExcInvoceNO.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Exc_InvNo)) ? "" : Convert.ToString(objGRHead.Exc_BillNO);
                ddlSrvcetax.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.STax_Typ)) ? "0" : Convert.ToString(objGRHead.STax_Typ);
                ddlRcptType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.RcptType_Idno)) ? "0" : Convert.ToString(objGRHead.RcptType_Idno);
                ddlRcptType_SelectedIndexChanged(null, null);
                txtInstNo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Inst_No)) ? "" : Convert.ToString(objGRHead.Inst_No);
                txtInstDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Inst_Dt)) ? "" : Convert.ToDateTime(objGRHead.Inst_Dt).ToString("dd-MM-yyyy");
                ddlcustbank.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Bank_Idno)) ? "0" : Convert.ToString(objGRHead.Bank_Idno);
                hidTBBType.Value = Convert.ToString(objGRHead.TBB_Rate);
                HiddWagsAmnt.Value = Convert.ToString(objGRHead.Wages_Amnt);
                HiddBiltyAmnt.Value = Convert.ToString(objGRHead.Bilty_Amnt);
                HiddTolltax.Value = Convert.ToString(objGRHead.TollTax_Amnt);
                HiddServTaxValid.Value = Convert.ToString(objGRHead.ServTax_Valid);
                Hiditruckcitywise.Value = Convert.ToString(objGRHead.cmb_type);
                HidiFromCity.Value = Convert.ToString(objGRHead.From_City);
                HidsRenWages.Value = Convert.ToString(objGRHead.WagesLabel_Print);
                HiddServTaxPer.Value = Convert.ToString(objGRHead.ServTax_Perc);
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
                    string strUnitNameId = Convert.ToString(objGRDetl.Rows[counter]["Unit_Idno"]);
                    string strRateType = Convert.ToString(objGRDetl.Rows[counter]["Rate_Type"]);
                    string strRateTypeIdno = Convert.ToString(objGRDetl.Rows[counter]["RateType_Idno"]);
                    string strQty = Convert.ToString(objGRDetl.Rows[counter]["Qty"]);
                    string strWeight = Convert.ToString(objGRDetl.Rows[counter]["Tot_Weght"]);
                    string strRate = Convert.ToString(objGRDetl.Rows[counter]["Item_Rate"]);
                    string strAmount = Convert.ToString(objGRDetl.Rows[counter]["Amount"]);
                    string strDetail = Convert.ToString(objGRDetl.Rows[counter]["Detail"]);
                    string PrevBal = Convert.ToString(objGRDetl.Rows[counter]["PREV_BAL"]);
                    string PrevQty = Convert.ToString(objGRDetl.Rows[counter]["PREV_QTY"]);
                    string strhidShrtgLimit = hidShrtgLimit.Value;
                    string strhidShrtgRate = hidShrtgRate.Value;
                    string strhidShrtgLimitOther = hidShrtgLimitOther.Value;
                    string strhidShrtgRateOther = hidShrtgRateOther.Value;
                    string strGradeName = Convert.ToString(objGRDetl.Rows[counter]["Grade_Name"]);
                    string strItemGradeId = Convert.ToString(objGRDetl.Rows[counter]["ItemGrade_Idno"]);
                    string strul = Convert.ToString(objGRDetl.Rows[counter]["UnloadWeight"]);
                  //  string strulamnt = Convert.ToString(objGRDetl.Rows[counter]["UnloadWeight_Amount"]);

                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDetail, strhidShrtgLimit, strhidShrtgRate, strhidShrtgLimitOther, strhidShrtgRateOther, PrevBal, PrevQty, strGradeName, strItemGradeId, strul);
                }
                ViewState["dt"] = dtTemp;
                BindGridT();
                txtGrossAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gross_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Gross_Amnt);
                txtCommission.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.AgntComisn_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.AgntComisn_Amnt);
                txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.TollTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.TollTax_Amnt);
                txtCartage.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Cartg_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Cartg_Amnt);
                txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Bilty_Amnt)) ? "0" : String.Format("{0:0,0.00}", (objGRHead.Bilty_Amnt));
                txtSubTotal.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SubTot_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.SubTot_Amnt);
                txtTotalAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Total_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Total_Amnt);
                txtWages.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Wages_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Wages_Amnt);
                txtServTax.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.ServTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.ServTax_Amnt);
                txtSwchhBhartTx.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SwchBrtTax_Amt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.SwchBrtTax_Amt);
                txtkalyan.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SwchBrtTax_Amt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.KisanKalyan_Amnt);
                txtSurchrge.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Surcrg_Amnt)) ? "0" : String.Format("{0:0,0.00}", (objGRHead.Surcrg_Amnt));
                txtPF.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.PF_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.PF_Amnt);
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Net_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Net_Amnt);
                TxtRoundOff.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.RndOff_Amnt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.RndOff_Amnt);

                //Upadhyay #GST
                txtSGSTAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.SGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.SGST_Amt);
                txtCGSTAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.CGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.CGST_Amt);
                txtIGSTAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.IGST_Amt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.IGST_Amt);
                //hidGSTCessAmt.Value = "0";

                hidSGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.SGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.SGST_Per);
                hidCGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.CGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.CGST_Per);
                hidIGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(objGRHead.IGST_Per)) ? "0" : string.Format("{0:0,0.00}", objGRHead.IGST_Per);
                hidGSTCessPer.Value = "0";
                hidGstType.Value = (string.IsNullOrEmpty(Convert.ToString(objGRHead.GST_Idno)) ? "0" : Convert.ToString(objGRHead.GST_Idno));

                Int64 ChallanNo = 0; ChallanNo = obj.CheckChallanDetails(intGRIdno);
                Int64 InvoiceNo = 0; InvoiceNo = obj.CheckInvoiceDetails(intGRIdno);

                txtFromKm.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.From_KM)) ? "0" : Convert.ToString(objGRHead.From_KM);
                txtToKM.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.To_KM)) ? "" : Convert.ToString(objGRHead.To_KM);
                txtTotKM.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Tot_KM)) ? "" : Convert.ToString(objGRHead.Tot_KM);
                obj = null;
                if (ChallanNo > 0)
                {
                    lnkbtnSave.Visible = true; ddlItemName.Enabled = false; lnkbtnSubmit.Enabled = false; lnkbtnAdd.Enabled = false;
                    DivSave.Visible = true;
                    RemarkDiv.Visible = true;
                    Lblchllnno.Text = "Challan No :- " + Convert.ToString(ChallanNo);
                    if (InvoiceNo > 0)
                    {
                        Lblchllnno.Text = "";
                        Lblchllnno.Text = "Invoice No :- " + Convert.ToString(InvoiceNo);
                    }
                }
                else
                {
                    RemarkDiv.Visible = false;
                    DivSave.Visible = true;
                    lnkbtnSave.Visible = true; ddlItemName.Enabled = true; lnkbtnSubmit.Enabled = true;
                    Lblchllnno.Text = "";
                }

                UpdateDiNo.Visible = UpdateEGPNo.Visible = UpdateInvNo.Visible = UpdateManNo.Visible = UpdateOrdrNo.Visible = UpdateFormNo.Visible = true;
            }
            obj = null;
        }

        private void DisplayGridDetails()
        {
            try
            {
                DataTable dtSaved = new DataTable();
                DataRow Dr;
                dtTemp = CreateDt();
                GRPrepDAL obj = new GRPrepDAL();
                hidGRHeadIdno.Value = Convert.ToString(Request.QueryString["Gr"]);
                var objGRDetl = obj.SelectGRDetail(Convert.ToInt32(hidGRHeadIdno.Value), ApplicationFunction.ConnectionString());
                if (dtSaved != null && dtSaved.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSaved.Rows.Count; i++)
                    {
                        Dr = dtTemp.NewRow();
                        Dr.BeginEdit();
                        Dr[0] = dtTemp.Rows.Count == 0 ? 1 : dtTemp.Rows.Count + 1;
                        Dr[1] = Convert.IsDBNull(dtSaved.Rows[i]["Item_Name"]) ? "" : Convert.ToString(dtSaved.Rows[i]["Item_Name"]);
                        Dr[2] = Convert.IsDBNull(dtSaved.Rows[i]["Unit_Name"]) ? "" : Convert.ToString(dtSaved.Rows[i]["Unit_Name"]);
                        Dr[3] = Convert.IsDBNull(dtSaved.Rows[i]["Rate_Type"]) ? "" : Convert.ToString(dtSaved.Rows[i]["Rate_Type"]);
                        Dr[4] = Convert.IsDBNull(dtSaved.Rows[i]["Quantity"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Quantity"]);
                        Dr[5] = Convert.IsDBNull(dtSaved.Rows[i]["Weight"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Weight"]);
                        Dr[6] = Convert.IsDBNull(dtSaved.Rows[i]["Rate"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Rate"]);
                        Dr[7] = Convert.IsDBNull(dtSaved.Rows[i]["Amount"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Amount"]);
                        Dr[8] = Convert.IsDBNull(dtSaved.Rows[i]["Detail"]) ? "" : Convert.ToString(dtSaved.Rows[i]["Detail"]);

                        Dr[9] = Convert.IsDBNull(dtSaved.Rows[i]["Shrtg_Limit"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Shrtg_Limit"]);
                        Dr[10] = Convert.IsDBNull(dtSaved.Rows[i]["Shrtg_Rate"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Shrtg_Rate"]);
                        Dr[9] = Convert.IsDBNull(dtSaved.Rows[i]["Shrtg_Limit_Other"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Shrtg_Limit_Other"]);
                        Dr[10] = Convert.IsDBNull(dtSaved.Rows[i]["Shrtg_Rate_Other"]) ? "0" : Convert.ToString(dtSaved.Rows[i]["Shrtg_Rate_Other"]);
                        Dr.EndEdit();
                        dtTemp.Rows.Add(Dr);
                    }
                    ViewState["dt"] = dtTemp;
                    this.BindGridT();
                }
            }
            catch (Exception ex)
            { }
        }
        private void BindCitywithStateId(int stateIdno)
        {
            //Int64 istateid = Convert.ToInt32(ddlstate.SelectedValue);
            //clsLedgerAcnt objclsLedgerAcnt = new clsLedgerAcnt();
            //var lst = objclsLedgerAcnt.SelectCity(stateIdno);
            //objclsLedgerAcnt = null;
            //ddlcity.DataSource = lst;
            //ddlcity.DataTextField = "Name";
            //ddlcity.DataValueField = "City_Idno";
            //ddlcity.DataBind();
            //ddlcity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select City--", "0"));

            ////lblDvPrtyName.Text = "Communication Detail - " + lblDvPrtyName.Text;
            //ddlstate.SelectedValue = istateid.ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "ShowClient('dvPrtyDet');", true);
            //ddlcity.Focus();
        }
        private void CurBalLoad()
        {
            try
            {
                string StrSql = "";
                double db, cr, dbopbal, cropbal, totdb, totcr;
                db = cr = dbopbal = cropbal = totdb = totcr = 0;
                if (ddlParty.SelectedIndex > -1)
                {
                    StrSql = "Exec [spVoucherEntry] @Action='SELECTCRDR',@AmntType=2, @AcntIdno=" + Convert.ToString(ddlParty.SelectedValue) + ",@YearIdno=" + Convert.ToString(ddlDateRange.SelectedValue);
                    db = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, StrSql));
                    StrSql = "Exec [spVoucherEntry] @Action='SELECTCRDR',@AmntType=1, @AcntIdno=" + Convert.ToString(ddlParty.SelectedValue) + ",@YearIdno=" + Convert.ToString(ddlDateRange.SelectedValue);
                    cr = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, StrSql));
                    StrSql = "Exec [spVoucherEntry] @Action='SELECTOPBAL',@OpenType=0, @AcntIdno=" + Convert.ToString(ddlParty.SelectedValue) + ",@YearIdno=" + Convert.ToString(ddlDateRange.SelectedValue);
                    dbopbal = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, StrSql));
                    StrSql = "Exec [spVoucherEntry] @Action='SELECTOPBAL',@OpenType=1, @AcntIdno=" + Convert.ToString(ddlParty.SelectedValue) + ",@YearIdno=" + Convert.ToString(ddlDateRange.SelectedValue);
                    cropbal = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, StrSql));

                    totdb = db + dbopbal; totcr = cr + cropbal;
                }
            }
            catch (Exception Ex)
            { }
        }
        public void userpref()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            dSurchgPer = Convert.ToDouble(userpref.Surchg_Per);
            dServTaxValid = Convert.ToDouble(userpref.ServTax_Valid);
            if (Convert.ToBoolean(userpref.Cont_Rate) == true)
            {
                HiddUserPrefCont.Value = "1";
                txtweight.AutoPostBack = true;
            }
            else
            {
                HiddUserPrefCont.Value = "0";
                txtweight.AutoPostBack = false;
            }
        }
        public void Selectuserpref()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
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
        private void SetDefault()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            tblUserDefault lst = obj.SelectDefault(string.IsNullOrEmpty(Session["UserIdno"].ToString()) ? 0 : Convert.ToInt64(Session["UserIdno"].ToString()));
            if (lst != null && lst.User_idno > 0 && Convert.ToString(Session["Userclass"]) != "Admin")
            {
                ddlSender.SelectedValue = string.IsNullOrEmpty(lst.SenderIdno.ToString()) ? "0" : lst.SenderIdno.ToString();
                ddlSender.Enabled = false;
                txtconsnr.Text = ddlSender.SelectedItem.Text;
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(lst.FromCity_idno.ToString()) ? "0" : lst.FromCity_idno.ToString();
                ddlFromCity_SelectedIndexChanged(null, null);
                ddlunitname.SelectedValue = string.IsNullOrEmpty(lst.UnitIdno.ToString()) ? "0" : lst.UnitIdno.ToString();
                ddlSrvcetax.SelectedValue = string.IsNullOrEmpty(lst.STax_Typ.ToString()) ? "0" : lst.STax_Typ.ToString();
                ddlSrvcetax.Enabled = false;
            }
        }

        private void CalculateEdit()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
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
                            //iunload = Convert.ToDouble(txtul.Text);
                            //if (txtul.Text.Trim() != "")
                            //{
                            //    dunloadamount = dtotalAmount + Convert.ToDouble(dtotalAmount * (iunload / 100));
                            //}
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
        private void GrRateControl()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            var lststate = objGrprepDAL.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
            tblUserPref obj2 = objGrprepDAL.selectuserpref();
            if (Convert.ToInt32(obj2.GRRate) == 1)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                    txtrate.Enabled = true;
            }
            else
            {
                txtrate.Enabled = false;
            }
        }
        private void FillRate()
        {
            if (ddlType.SelectedValue == "2")
            {
                //txtFixedAmount.Text = "2000.00";
                txtrate.Enabled = false;
                txtrate.Text = "0.00";
                txtweight.Enabled = true;
                txtweight.Text = "0.00";
                return;
            }
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            double iRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (ddlItemName.SelectedIndex > 0)
            {
                if (ddlGRType.SelectedValue == "1") //In case of Paid
                {
                    if (hidTBBType.Value == "False")
                    {
                        txtrate.Text = "0.00";
                        if (Convert.ToInt32((ddlRateType.SelectedValue) == "" ? "0" : ddlRateType.SelectedValue) > 0)
                        {
                            if (Convert.ToInt32((ddlRateType.SelectedValue) == "" ? "0" : ddlRateType.SelectedValue) == 1)
                            {
                                txtweight.Text = "0.00"; //txtweight.Enabled = false; 
                                txtQuantity.Enabled = true;
                            }
                            else
                            {
                                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                                    txtweight.Enabled = true;
                                txtQuantity.Text = "1";
                                //txtQuantity.Enabled = false;
                            }
                        }
                        else
                        {
                            txtrate.Text = "0.00";
                        }
                    }
                    else
                    {
                        if ((ddlRateType.SelectedIndex) > 0)
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                iRate = objGrprepDAL.SelectRatePartyWise(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue));
                                if (iRate <= 0)
                                {
                                    iRate = objGrprepDAL.SelectItemRateForTBB(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                }
                                txtrate.Text = iRate.ToString("N2");
                                this.GrRateControl();
                                txtweight.Text = "0.00"; //txtweight.Enabled = false; 
                                txtQuantity.Enabled = true;

                                iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");

                                iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");

                                iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgLimitOther.Value = iWghtShrtgLimit.ToString("N2");

                                iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgRateOther.Value = iWghtShrtgRate.ToString("N2");
                            }
                            else
                            {
                                iRate = objGrprepDAL.SelectItemWghtRateForTBB(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                txtrate.Text = iRate.ToString("N2");
                                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                                    txtweight.Enabled = true;
                                txtQuantity.Text = "1";
                                //txtQuantity.Enabled = false; 
                                this.GrRateControl();
                                iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");

                                iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");

                                iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgRateOther.Value = iQtyShrtgRate.ToString("N2");

                                iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                                hidShrtgLimitOther.Value = iQtyShrtgLimit.ToString("N2");
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
                            iRate = objGrprepDAL.SelectRatePartyWise(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue), Convert.ToInt32(ddlSender.SelectedValue));
                            if (iRate <= 0)
                            {
                                iRate = objGrprepDAL.SelectItemRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            }
                            txtrate.Text = iRate.ToString("N2"); this.GrRateControl();
                            txtweight.Text = "0.00"; //txtweight.Enabled = false; 
                            txtQuantity.Enabled = true;

                            iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");

                            iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");

                            iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgLimitOther.Value = iWghtShrtgLimit.ToString("N2");

                            iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgRateOther.Value = iWghtShrtgRate.ToString("N2");
                        }
                        else
                        {

                            iRate = objGrprepDAL.SelectItemWghtRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            txtrate.Text = iRate.ToString("N2");
                            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                                txtweight.Enabled = true;
                            txtQuantity.Text = "1";
                            this.GrRateControl(); //txtQuantity.Enabled = false;

                            iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");

                            iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");

                            iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgRateOther.Value = iQtyShrtgRate.ToString("N2");

                            iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue));
                            hidShrtgLimitOther.Value = iQtyShrtgLimit.ToString("N2");
                        }
                    }
                    else
                    {
                        txtrate.Text = "0.00";
                    }
                }
            }
            if (HiddUserPrefCont.Value == "1")
            {
                txtrate.Text = "0.00";
            }
        }
        private void FillRateWeightWiseRate()
        {
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            AdvBookGRDAL objGrprepDAL = new AdvBookGRDAL();
            if (txtweight.Text.Trim() != "" && Convert.ToDouble(txtweight.Text.Trim()) > 0.00)
            {
                iRate = objGrprepDAL.SelectItemWeightWiseRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), strGrDate, Convert.ToDecimal(txtweight.Text.Trim()), Convert.ToInt64(ddlSender.SelectedValue));
            }
            txtrate.Text = Convert.ToDouble(iRate > 0 ? iRate : 0.00).ToString("N2");
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
                            itotWeght = itotWeght + Convert.ToDouble(dtTemp.Rows[i]["Weight"]);
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
        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }
        private void netamntcal()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
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
                            txtSGSTAmnt.Text = txtCGSTAmnt.Text = txtIGSTAmnt.Text = txtWages.Text = txtPF.Text = txtSubTotal.Text = txtNetAmnt.Text = txtServTax.Text = txtkalyan.Text = txtSwchhBhartTx.Text = txtTollTax.Text = "0.00";
                        }
                        else
                        {
                            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text)).ToString("N2");
                            double gst = 0;

                            double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                            if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0) && ddlSrvcetax.SelectedValue == "1")
                            {
                                if (gstType == 1)
                                {
                                    dSGST_Per = Convert.ToDouble(Convert.ToString(hidSGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidSGSTPer.Value));
                                    txtSGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)).ToString("N2");
                                    dCGST_Per = Convert.ToDouble(Convert.ToString(hidCGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidCGSTPer.Value));
                                    txtCGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100)).ToString("N2");
                                    //Presently GST Cess is not being used
                                    dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                    //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                    gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)) + Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100));
                                }
                                else if (gstType == 2)
                                {
                                    dIGST_Per = Convert.ToDouble(Convert.ToString(hidIGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidIGSTPer.Value));
                                    txtIGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100)).ToString("N2");
                                    //Presently GST Cess is not being used
                                    dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                    //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                    gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100));
                                }
                            }
                            else
                            {
                                txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
                            }
                            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + gst))).ToString("N2");
                            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + gst)).ToString("N2");

                        }
                    }
                    else
                    {
                        double dCartage = Convert.ToDouble((txtCartage.Text) == "" ? 0 : Convert.ToDouble(txtCartage.Text));
                        txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N3");
                        txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text)).ToString("N3");
                        double gst = 0;
                        double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
                        if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0) && ddlSrvcetax.SelectedValue == "1")
                        {
                            if (gstType == 1)
                            {
                                dSGST_Per = Convert.ToDouble(Convert.ToString(hidSGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidSGSTPer.Value));
                                txtSGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)).ToString("N3");
                                dCGST_Per = Convert.ToDouble(Convert.ToString(hidCGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidCGSTPer.Value));
                                txtCGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100)).ToString("N3");
                                //Presently GST Cess is not being used
                                dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSGST_Per) / 100)) + Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dCGST_Per) / 100));
                            }
                            else if (gstType == 2)
                            {
                                dIGST_Per = Convert.ToDouble(Convert.ToString(hidIGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidIGSTPer.Value));
                                txtIGSTAmnt.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100)).ToString("N3");
                                //Presently GST Cess is not being used
                                dGSTCess_Per = Convert.ToDouble(Convert.ToString(hidGSTCessPer.Value) == "" ? 0 : Convert.ToDouble(hidGSTCessPer.Value));
                                //txtGSTCessAmnt.Text = Convert.ToDouble(((Convert.ToDouble("0") * dGSTCess_Per) / 100)).ToString("N3");
                                gst = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dIGST_Per) / 100));
                            }
                        }
                        else
                        {
                            txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
                        }
                        txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + gst))).ToString("N2");
                        txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
                        TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + gst)).ToString("N2");

                    }
                    if (dSurchgPer > 0)
                    {
                        dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
                        txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
                    }
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
                            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text)).ToString("N2");

                            double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
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
                    }
                    else
                    {
                        double dCartage = Convert.ToDouble((txtCartage.Text) == "" ? 0 : Convert.ToDouble(txtCartage.Text));
                        txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + dCartage).ToString("N2");
                        txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble((txtBilty.Text) == "" ? 0 : Convert.ToDouble(txtBilty.Text)) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble((txtTollTax.Text) == "" ? 0 : Convert.ToDouble(txtTollTax.Text))).ToString("N2");

                        double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
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
                }
                #endregion
            }
            catch (Exception Ex)
            { }
        }
        //private void netamntcal()
        //{
        //    GRPrepDAL objGrprepDAL = new GRPrepDAL();
        //    try
        //    {
        //        if (ddlGRType.SelectedIndex == 1)
        //        {
        //            if (hidTBBType.Value == "False")
        //            {
        //                txtGrossAmnt.Text = txtCartage.Text = txtTotalAmnt.Text = txtSurchrge.Text = txtCommission.Text = txtBilty.Text = "0.00";
        //                txtWages.Text = txtPF.Text = txtSubTotal.Text = txtNetAmnt.Text = txtServTax.Text = txtkalyan.Text = txtSwchhBhartTx.Text = txtTollTax.Text = "0.00";
        //            }
        //            else
        //            {
        //                txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtCartage.Text)).ToString("N2");
        //                txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble(txtBilty.Text) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble(txtTollTax.Text)).ToString("N2");
        //                double ttlAmnt = Convert.ToDouble(txtSubTotal.Text ?? "0");
        //                int gstType = GetGSTType();
        //                //IF GST Calculation 
        //                if (gstType > 0)
        //                {
        //                    if (gstType == 1)
        //                    {
        //                        dSGSTPer = Convert.ToDouble(Convert.ToString(hidSGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidSGSTPer.Value));
        //                        txtSGSTAmnt.Text = (ttlAmnt * dSGSTPer/100).ToString("N3");
        //                        dCGSTPer = Convert.ToDouble(Convert.ToString(hidCGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidCGSTPer.Value));
        //                        txtCGSTAmnt.Text = (ttlAmnt * dCGSTPer / 100).ToString("N3");
        //                    }
        //                    else if (gstType == 2)
        //                    {
        //                        dIGSTPer = Convert.ToDouble(Convert.ToString(hidIGSTPer.Value) == "" ? 0 : Convert.ToDouble(hidIGSTPer.Value));
        //                        txtIGSTAmnt.Text = (ttlAmnt * dIGSTPer / 100).ToString("N3");
        //                    }
        //                    txtNetAmnt.Text = Convert.ToDouble(dSGSTPer + dCGSTPer + dIGSTPer).ToString("N2");
        //                }
        //                //IF VAT
        //                else
        //                {
        //                    double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
        //                    if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
        //                    {
        //                        dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));
        //                        txtServTax.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dServTaxPer) / 100)).ToString("N2");

        //                        dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
        //                        txtSwchhBhartTx.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSwacchBhrtTaxPer) / 100)).ToString("N2");

        //                        dKalyanTax = Convert.ToDouble(Convert.ToString(HiddKalyanTax.Value) == "" ? 0 : Convert.ToDouble(HiddKalyanTax.Value));
        //                        txtkalyan.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dKalyanTax) / 100)).ToString("N2");
        //                    }
        //                    else
        //                    {
        //                        txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
        //                    }
        //                    txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text)))).ToString("N2");
        //                }
        //                txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
        //                TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text))).ToString("N2");

        //            }
        //        }
        //        else
        //        {
        //            double dCartage = Convert.ToDouble((txtCartage.Text) == "" ? 0 : Convert.ToDouble(txtCartage.Text));
        //            txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + dCartage).ToString("N2");
        //            txtSubTotal.Text = Convert.ToDouble(Convert.ToDouble(txtTotalAmnt.Text) + Convert.ToDouble(txtSurchrge.Text) + Convert.ToDouble(txtCommission.Text) + Convert.ToDouble((txtBilty.Text) == "" ? 0 : Convert.ToDouble(txtBilty.Text)) + Convert.ToDouble(txtWages.Text) + Convert.ToDouble(txtPF.Text) + Convert.ToDouble((txtTollTax.Text) == "" ? 0 : Convert.ToDouble(txtTollTax.Text))).ToString("N2");

        //            double dServTaxExmpt = objGrprepDAL.SelectServTaxExmpt(Convert.ToInt32((ddlSender.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue)));
        //            if ((Convert.ToDouble(txtSubTotal.Text) >= dServTaxValid) && (dServTaxExmpt == 0))
        //            {
        //                dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));
        //                txtServTax.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dServTaxPer) / 100)).ToString("N2");

        //                dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
        //                txtSwchhBhartTx.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dSwacchBhrtTaxPer) / 100)).ToString("N2");

        //                dKalyanTax = Convert.ToDouble(Convert.ToString(HiddKalyanTax.Value) == "" ? 0 : Convert.ToDouble(HiddKalyanTax.Value));
        //                txtkalyan.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) * dKalyanTax) / 100)).ToString("N2");
        //            }
        //            else
        //            {
        //                txtServTax.Text = "0.00"; txtSwchhBhartTx.Text = "0.00";
        //            }

        //            txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text)))).ToString("N2");
        //            txtNetAmnt.Text = Math.Round(Convert.ToDouble(txtNetAmnt.Text)).ToString("N2");
        //            TxtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtServTax.Text) + Convert.ToDouble(txtSwchhBhartTx.Text) + Convert.ToDouble(txtkalyan.Text))).ToString("N2");
        //        }
        //        if (dSurchgPer > 0)
        //        {
        //            dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
        //            txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
        //        }
        //    }
        //    catch (Exception Ex)
        //    { }
        //}
        private void ValidateControls()
        {
            //txtPrefixNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtDateFromDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateToDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtInstDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtGRNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtDelvNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtManualNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtInstNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtPF.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtQuantity.Text = "1";
            txtQuantity.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtmobileno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtfaxno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtpinno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtOrderNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtFromNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtEWayBillNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtTaxInvoiceNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtExcInvoceNO.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
        }
        private void EnableDisableAtLoad()
        {
            lblmsg.Visible = false;
            lblmsg2.Visible = false;
            RDbDirect.Checked = true;
            RDbDirect.Enabled = true;
            if (ddlGRType.SelectedValue == "1")
            {
                ddlRcptType.Enabled = true;
            }
            else
            {
                ddlRcptType.Enabled = false;
                ddlRcptType.SelectedIndex = 0;
            }
            if (Convert.ToString(Hiditruckcitywise.Value) != "1")
            {
                ddlTruckNo.Visible = true; lblTruckNo.Visible = true;
                lnkbtnTruuckRefresh.Visible = true;
            }
            ddlFromCity.Enabled = true;
            ddlToCity.Enabled = true;
            ddlLocation.Enabled = true;
            ddlSender.Enabled = true;
            ddlReceiver.Enabled = true;
            ddlParty.Enabled = true;
            ddlDateRange.Enabled = true;
        }
        private bool CheckDuplicatieItem()
        {
            bool value = true;
            DataTable dtTemp = (DataTable)ViewState["dt"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0)) { foreach (DataRow row in dtTemp.Rows) { if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim()))) { value = false; } } }
            if (value == false) { return false; }
            else { return true; }
        }
        private void userprefForWeight()
        {
            RateMasterDAL objGrprepDAL = new RateMasterDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            IsWeight = Convert.ToBoolean(userpref.WeightWise_Rate);
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
                        //string toStateName = GetStateByCityId(Convert.ToInt64(ddlToCity.SelectedValue));
                        hidToStateName.Value = toState;
                        txtToState.Text = toState;
                        GetStateId(Convert.ToInt64(ddlToCity.SelectedValue));
                        if (Convert.ToString(toStateid) == compStateName.Trim().ToLower())
                            hidGstType.Value = "1";
                        else
                            hidGstType.Value = "2";
                        return Convert.ToInt32(hidGstType.Value);
                    }
                }
            }
            return 0;
        }
        public void GetStateId(long CityId)
        {
            hidToStateIdno.Value = "";
            hidToStateName.Value = "";                      
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                toState = (from s in db.tblStateMasters
                           join c in db.tblCityMasters on s.State_Idno equals c.State_Idno
                           where c.City_Idno == CityId
                           select s.State_Name).SingleOrDefault();
                toStateid = (from s in db.tblStateMasters
                           join c in db.tblCityMasters on s.State_Idno equals c.State_Idno
                           where c.City_Idno == CityId
                           select s.State_Idno).SingleOrDefault();
            }
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] GetSenderNo(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();
            List<string> PartyNumber = new List<string>();
            DataTable dtNames = new DataTable();
            AccountBookDAL obj = new AccountBookDAL();
            DataSet dt = obj.SelectPartyList(prefixText, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Name"]), Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Idno"]));
                    PartyNumber.Add(item);
                }
                return PartyNumber.ToArray();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Grid Event ...
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
                    ddlTruckNo_SelectedIndexChanged(null, null);
                    GRPrepDAL obj = new GRPrepDAL();
                    Double CommissionAmnt = 0;
                    DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
                    DateTime dtGRDate = strGrDate;
                    Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                    Int64 LorryType = 0;
                    if (HiddTruckIdno.Value != "")
                    {
                        LorryType = obj.SelectLorryType(Convert.ToInt64(HiddTruckIdno.Value));
                        if (LorryType == 1)
                        {
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
                                txtItmCommission.Visible = true;
                                txtItmCommission.Enabled = true;
                                DivCommissionUpdatebtn.Visible = true;
                            }
                        }
                        else
                        {
                            txtItmCommission.Text = "0.00";
                            DivCommission.Visible = false;
                            DivCommissionUpdatebtn.Visible = false;
                        }
                    }
                    ddlunitname.SelectedValue = Convert.ToString(drs[0]["Unit_Idno"]);
                    ddlRateType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["Rate_TypeIdno"]) == "" ? 0 : drs[0]["Rate_TypeIdno"]);
                    ddlItemName.Enabled = false;
                    ddlunitname.Enabled = false;
                    if (rdbAdvanceOrder.Checked == true)
                    {
                        lblPrevBalVal.Visible = true;
                        txtPrevBal.Visible = true;
                    }
                    else
                    {
                        lblPrevBalVal.Visible = false;
                        txtPrevBal.Visible = false;
                    }
                    //if (rdbAdvanceOrder.Checked == true)
                    //{
                    //    if (Convert.ToString(drs[0]["Rate_TypeIdno"]) == "1")
                    //    {
                    //        if ((string.IsNullOrEmpty(Convert.ToString(drs[0]["PREV_BAL"])) ? 0 :Convert.ToInt32(drs[0]["PREV_BAL"])) > 0)
                    //              txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["PREV_BAL"]) == "" ? 1 : Convert.ToInt64(drs[0]["PREV_BAL"]));
                    //        else
                    //              txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));

                    //        txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    //    }
                    //    else if (Convert.ToString(drs[0]["Rate_TypeIdno"]) == "2")
                    //    {
                    //        txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    //        if ((string.IsNullOrEmpty(Convert.ToString(drs[0]["PREV_BAL"])) ? 0 : Convert.ToInt32(drs[0]["PREV_BAL"])) > 0)
                    //             txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["PREV_BAL"]) == "" ? 0 : Convert.ToDouble(drs[0]["PREV_BAL"])));
                    //        else
                    //            txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    //    }
                    //}
                    //else
                    //{
                    //    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    //    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    //}

                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));

                    txtPrevBal.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["PREV_BAL"]) == "" ? 0 : Convert.ToDouble(drs[0]["PREV_BAL"])));
                    txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])));
                    txtdetail.Text = Convert.ToString(drs[0]["Detail"]);
                    txtul.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["UnloadWeight"]) == "" ? 0 : Convert.ToDouble(drs[0]["UnloadWeight"])));
                    //txtAmount.Text = String.Format("{0:0,0.00}", Convert.ToDouble(drs[0]["Amount"]));
                    //ddlRateType_SelectedIndexChanged(null, null);
                    hidratetype.Value = Convert.ToString(Convert.ToString(drs[0]["Rate_Type"]) == "" ? 0 : drs[0]["Rate_Type"]);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
                    hidShrtgRate.Value = Convert.ToString(Convert.ToString(drs[0]["Shrtg_Rate"]) == "" ? 0 : drs[0]["Shrtg_Rate"]);
                    hidShrtgLimit.Value = Convert.ToString(Convert.ToString(drs[0]["Shrtg_Limit"]) == "" ? 0 : drs[0]["Shrtg_Limit"]);

                    if (ddlItemName.SelectedIndex > 0)
                    {
                        if (ddlGRType.SelectedIndex == 1) //In case of Paid
                        {
                            if ((hidTBBType.Value) == "False")
                            { txtrate.Text = "0.00"; txtrate.Enabled = false; }
                            else
                            {
                                if ((ddlRateType.SelectedIndex) > 0)
                                {
                                    if (ddlRateType.SelectedIndex == 1)
                                    {
                                        this.GrRateControl(); txtweight.Text = "0.00"; //txtweight.Enabled = false; 
                                        txtQuantity.Enabled = true;
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(ddlType.SelectedValue) == 1) txtweight.Enabled = true; //txtQuantity.Enabled = false; 
                                        this.GrRateControl();
                                    }
                                }
                                else { txtrate.Text = "0.00"; }
                            }
                        }
                        else
                        {
                            if ((ddlRateType.SelectedIndex) > 0)
                            {
                                if (ddlRateType.SelectedIndex == 1)
                                {
                                    this.GrRateControl(); txtweight.Text = "0.00"; //txtweight.Enabled = false; 
                                    txtQuantity.Enabled = true;
                                }
                                else
                                {
                                    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                                        txtweight.Enabled = true;
                                    this.GrRateControl(); //txtQuantity.Enabled = false;
                                }
                            }
                            else { txtrate.Text = "0.00"; }
                        }
                    }
                    ddlRateType.Focus();
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
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Name"], rw["Item_Idno"], rw["Unit_Name"], rw["Unit_Idno"], rw["Rate_Type"],
                                                                rw["Rate_TypeIdno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"], rw["Detail"], rw["Shrtg_Limit"], rw["Shrtg_Rate"], rw["Shrtg_Limit_Other"], rw["Shrtg_Rate_Other"]
                                                                , rw["UnloadWeight"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
                netamntcal();
                Selectuserpref();
                ddlItemName.Focus();
            }

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (dtTemp == null)
            {
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    iqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "quantity"));
                    totalIqty += iqty;
                    itotWeght = itotWeght + Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Weight")) == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight")));
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    dtotrate = dtotrate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate"));
                    dtot = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    dul = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UnloadWeight"));


                    double total = dtot * (dul / 100);

                    add += total;
                    //Label lblunload = (Label)e.Row.FindControl("lblunload");
                    //lblunload.Text = add.ToString("N2");
                    
                    if ((Convert.ToString(hidGRHeadIdno.Value) == "") || (Convert.ToString(hidGRHeadIdno.Value) == "0"))
                    {
                        txtWages.Text = Convert.ToDouble(totalIqty * Convert.ToDouble(Convert.ToString(HiddWagsAmnt.Value) == "" ? 0 : Convert.ToDouble(HiddWagsAmnt.Value))).ToString("N2");
                        txtTollTax.Text = Convert.ToDouble(totalIqty * Convert.ToDouble(Convert.ToString(HiddTolltax.Value) == "" ? 0 : Convert.ToDouble(HiddTolltax.Value))).ToString("N2");
                        txtBilty.Text = Convert.ToDouble(Convert.ToString(HiddBiltyAmnt.Value) == "" ? 0 : Convert.ToDouble(HiddBiltyAmnt.Value)).ToString("N2");
                        if (dSurchgPer > 0)
                        {
                            dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
                            txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
                        }
                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                    lblQuantity.Text = totalIqty.ToString("N2");

                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    lblWeight.Text = itotWeght.ToString("N2");

                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = dtotAmnt.ToString("N2");

                    Label lblRate = (Label)e.Row.FindControl("lblRate");
                    lblRate.Text = dtotrate.ToString("N2");

                    //Label lblul = (Label)e.Row.FindControl("lblul");
                    //lblul.Text = dtotul.ToString("N2");
                    txtWages.Text = add.ToString("N2");

                    if (ddlType.SelectedValue == "2")
                        txtGrossAmnt.Text = txtFixedAmount.Text.Trim();
                    else
                        txtGrossAmnt.Text = dtotAmnt.ToString("N2");
                    txtTotalAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble((txtCartage.Text) == "" ? 0 : Convert.ToDouble(txtCartage.Text))).ToString("N2");
                    if (dSurgValue > 0)
                    {
                        dSurgValue = Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100);
                        txtSurchrge.Text = Convert.ToDouble(Math.Ceiling(Convert.ToDouble((Convert.ToDouble(txtTotalAmnt.Text) * dSurchgPer) / 100))).ToString("N2");
                    }
                    netamntcal();
                }
            }
        }
        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label LblTypeCash = (Label)e.Item.FindControl("LblTypeCash");
                Label LblTypeCheque = (Label)e.Item.FindControl("LblTypeCheque");
                if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "INST_TYPE")) == 0)
                {
                    LblTypeCash.Visible = true;
                    LblTypeCheque.Visible = false;
                }
                else
                {
                    LblTypeCash.Visible = false;
                    LblTypeCheque.Visible = true;
                }
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
        protected void grdGrdetals_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (grdGrdetals.HeaderRow != null)
            {
                chkSelectAllRows_CheckedChanged(null, null);
            }
        }
        protected void grdGrdetals_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (grdGrdetals.HeaderRow != null)
            {
                chkSelectAllRows_CheckedChanged(null, null);
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlGenericControl HideGrhdr = (HtmlGenericControl)e.Item.FindControl("HideGrhdr");
                if (chkbit == 1) { HideGrhdr.Visible = false; } else { HideGrhdr.Visible = true; }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl HideGritem = (HtmlGenericControl)e.Item.FindControl("HideGritem");
                if (chkbit == 1) { HideGritem.Visible = false; } else { HideGritem.Visible = true; }

                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlGenericControl hidfooterdetl = (HtmlGenericControl)e.Item.FindControl("hidfooterdetl");
                Label lblFTtotalWeight = (Label)e.Item.FindControl("lblFTtotalWeight");
                Label lblFTTotalAmnt = (Label)e.Item.FindControl("lblFTTotalAmnt");
                Label lblFTQty = (Label)e.Item.FindControl("lblFTQty");

                lblFTTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lblFTtotalWeight.Text = dtotwght.ToString("N2");
                lblFTQty.Text = dqtnty.ToString();

                if (chkbit == 1) { hidfooterdetl.Visible = false; lstInfoDiv.Visible = false; } else if (chkbit == 2) { hidfooterdetl.Visible = true; lstInfoDiv.Visible = false; }
            }
        }
        #endregion

        #region Control Events

        #region SelectedIndexChanged

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCitywithStateId(Convert.ToInt32(ddlstate.SelectedValue));
            ddlcity.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "ShowClient('dvPrtyDet');", true);
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
                DivAmount.Visible = false;
                txtFixedAmount.Text = "0.00";
                ddlType.Focus();
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
                    txtInstDate.Enabled = false; ddlRcptType.Enabled = false; rfvinstdate.Enabled = false;
                    ddlcustbank.Enabled = false; rfvddlcustbank.Enabled = false;
                }
                ddlRcptType.Focus();
            }
            catch (Exception Ex)
            { }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDateRange.SelectedValue) > 0)
            {
                if (txtGRDate.Text != "")
                {
                    if (Convert.ToInt32(ddlFromCity.SelectedValue) > 0)
                    {
                        if (Convert.ToInt32(ddlToCity.SelectedValue) > 0)
                        {
                            GRPrepDAL obj = new GRPrepDAL();
                            Int64 LorryType = 0;
                            LorryType = obj.SelectLorryType(Convert.ToInt64(ddlTruckNo.SelectedValue));
                            if (LorryType == 1)
                            {
                                DivCommission.Visible = true;
                                DivCommissionUpdatebtn.Visible = true;
                            }
                            else
                            {
                                DivCommission.Visible = false;
                                DivCommissionUpdatebtn.Visible = false;
                            }
                            ddlTruckNo.Focus();
                        }
                        else
                        {
                            this.ShowMessageErr("Please Select To City."); ddlTruckNo.SelectedIndex = 0; ddlToCity.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.ShowMessageErr("Please Select Loc[From]."); ddlTruckNo.SelectedIndex = 0; ddlFromCity.Focus();
                        return;
                    }
                }
                else
                {
                    this.ShowMessageErr("Please Select Gr Date."); ddlTruckNo.SelectedIndex = 0; txtGRDate.Focus();
                    return;
                }
            }
            else
            {
                this.ShowMessageErr("Please Select Date Range."); ddlTruckNo.SelectedIndex = 0; ddlDateRange.Focus();

                return;
            }
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            GRPrepDAL objGR = new GRPrepDAL();
            SetDate();
            this.BindMaxNo("BK", Convert.ToInt32((ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
            ddlDateRange.Focus();
        }
        protected void ddlRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillRate(); ddlRateType.Focus();
            }
            catch (Exception Ex)
            {
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
                    if (IsWeight == true)
                        FillRateWeightWiseRate();
                    else
                        FillRate();
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
                                                GRPrepDAL obj = new GRPrepDAL();
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
                            this.ClearItems();
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
                if (UserPrefGradeVal == false)
                    ddlunitname.Focus();
                else
                    ddlItemGrade.Focus();
                if (IsWeight == true)
                {
                    ddlRateType.SelectedIndex = 1;
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlGRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearItems(); dtTemp = null; ViewState["dt"] = dtTemp = null; grdMain.DataSource = dtTemp; grdMain.DataBind();
                if (ddlGRType.SelectedIndex == 0) //In Case of Paid
                { ddlRcptType.Enabled = true; lnkLorryType.Enabled = true; }
                else { ddlRcptType.Enabled = false; lnkLorryType.Enabled = false; }
                hidGRHeadIdno.Value = string.Empty; ddlGRType.Enabled = true;
                ddlDateRange_SelectedIndexChanged(null, null);
                hidrowid.Value = ""; hidDelvIdno.Value = string.Empty;
                ddlReceiver.SelectedIndex = ddlTruckNo.SelectedIndex = ddlParty.SelectedIndex = ddlRcptType.SelectedIndex = ddlToCity.SelectedIndex = 0;
                txtDelvNo.Text = txtInstNo.Text = TxtEGPNo.Text = "";
                ddlItemName.SelectedIndex = ddlRateType.SelectedIndex = 0;
                txtGrossAmnt.Text = txtCommission.Text = txtTollTax.Text = txtCartage.Text = txtBilty.Text = txtSubTotal.Text = txtTotalAmnt.Text = txtWages.Text = txtServTax.Text = txtSurchrge.Text = txtPF.Text = txtNetAmnt.Text = TxtRoundOff.Text = "0.00";
                grdMain.DataSource = null;
                grdMain.DataBind();
                ddlSender.Focus();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
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
                GRPrepDAL objGR = new GRPrepDAL();
                var lststate = objGR.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
                if (lststate != null)
                {
                    HiddServTaxPer.Value = Convert.ToString(objGR.SelectServiceTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()))));
                    dServTaxPer = Convert.ToDouble(Convert.ToString(HiddServTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddServTaxPer.Value));
                }
                string GRfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(GRfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                ddlToCity.Focus();
                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToString(txtDelvNo.Text.Trim()), Convert.ToString(txtTaxInvoiceNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                   
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate GR No. AND DI No. AND INV.No!");
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
                    this.ShowMessageErr("GR No.AND DI No. AND INV. No. can't be left blank.");
                    txtGRNo.Text = Convert.ToString(MaxGRNo);
                    txtGRNo.Focus(); txtGRNo.SelectText();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }
        protected void ddlTypeI_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTypeI.SelectedValue == "1")
                lblTypeI.Text = "CHA";
            else if (ddlTypeI.SelectedValue == "2")
                lblTypeI.Text = "Forwarder";
            else
                lblTypeI.Text = "Select";

            ddlTypeI.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        #endregion

        #region TextChanged
        protected void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtBillNo.Text.Trim() != "")
            //    {
            //        string strSql = "";
            //        if (ddlGRType.SelectedIndex == 4)
            //        {
            //            strSql = "Exec [spVoucherEntry] @ACTION='SelectPartyThrBillNo', @SBILL_NO = '" + Convert.ToString(txtBillNo.Text.Trim()) + "',@YEAR_IDNO='" + ddlDateRange.SelectedValue + "'";
            //            DataSet dsParty = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, strSql);
            //            if ((dsParty != null) && (dsParty.Tables.Count > 0) && (dsParty.Tables[0].Rows.Count > 0))
            //            {
            //                ddlParty.SelectedValue = Convert.ToString(dsParty.Tables[0].Rows[0]["BILLPARTY_IDNO"]);
            //                txtBillDate.Text = Convert.ToDateTime(dsParty.Tables[0].Rows[0]["SBILL_DATE"]).ToString("dd-MM-yyyy");
            //            }
            //            else
            //            {
            //                ddlParty.SelectedIndex = -1; txtBillNo.Text = txtOnAcCurBal.Text = "";
            //                txtBillDate.Text = "";
            //            }
            //        }
            //        else if (ddlGRType.SelectedIndex == 5)
            //        {
            //            strSql = "Exec [spVoucherEntry] @ACTION='SelectPartyThrBillNoForR', @SBILL_NO = '" + Convert.ToString(txtBillNo.Text.Trim()) + "',@YEAR_IDNO='" + ddlDateRange.SelectedValue + "'";
            //            DataSet dsParty = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, strSql);
            //            if ((dsParty != null) && (dsParty.Tables.Count > 0) && (dsParty.Tables[0].Rows.Count > 0))
            //            {
            //                ddlParty.SelectedValue = Convert.ToString(dsParty.Tables[0].Rows[0]["BILLPARTY_IDNO"]);
            //                txtBillDate.Text = Convert.ToDateTime(dsParty.Tables[0].Rows[0]["SBILL_DATE"]).ToString("dd-MM-yyyy");
            //            }
            //            else
            //            {
            //                ddlParty.SelectedIndex = -1; txtBillNo.Text = txtOnAcCurBal.Text = "";
            //                txtBillDate.Text = "";
            //            }
            //        }

            //    }
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
            //}
            //catch (Exception ex)
            //{
            //}
        }
        protected void txtDelvNo_TextChanged(object sender, EventArgs e)
        {
            //Int64 Deli = string.IsNullOrEmpty(Convert.ToString(txtDelvNo.Text.Trim())) ? 0 : Convert.ToInt64(txtDelvNo.Text.Trim());

            //string strTemp = "Exec [spReceiptEntry] @ACTION='SelectDelNotePrvHis', @DELV_IDNO='" + Deli + "',@YEAR_IDNO=" + ddlDateRange.SelectedValue;
            //DataSet dsDelNoteHis = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, strTemp);
            //if (dsDelNoteHis != null)
            //{
            //    if (dsDelNoteHis.Tables.Count > 0)
            //    {
            //        if (dsDelNoteHis.Tables[0].Rows.Count > 0)
            //        {
            //            ShowMessage("Delivery No.  " + Deli + "  is already saved.");
            //            return;
            //        }
            //    }
            // }

            //DataTable DtDnote = objclsReceiptEntry.DeliNoteDetl(Deli, Convert.ToInt32(ddlDateRange.SelectedValue));
            //if (DtDnote != null && DtDnote.Rows.Count > 0)
            //{
            //    //ddlSender.Enabled = ddlDsa.Enabled = ddlReceiver.Enabled = ddlParty.Enabled = ddlTruckNo.Enabled = true;
            //    hidDelvIdno.Value = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["DELV_IDNO"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["DELV_IDNO"]);
            //    ddlSender.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["FIN_IDNO"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["FIN_IDNO"]);
            //    ddlReceiver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["SE_IDNO"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["SE_IDNO"]);
            //    ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["ACNT_IDNO"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["ACNT_IDNO"]);
            //    ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["ITEM_IDNO"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["ITEM_IDNO"]);
            //    ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["Loc_Idno"])) ? "0" : Convert.ToString(DtDnote.Rows[0]["Loc_Idno"]);

            //    txtVehicleCost.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["VHCL_COST"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["VHCL_COST"]);
            //    txtTax.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["ITEMTAX"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["ITEMTAX"]);
            //    txtInsurance.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["INSU_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["INSU_AMNT"]);
            //    txtRtoAmnt.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["REGN_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["REGN_AMNT"]);
            //    txtOnRoad.Text = Convert.ToDouble(Convert.ToDouble(txtVehicleCost.Text) + Convert.ToDouble(txtTax.Text) + Convert.ToDouble(txtInsurance.Text) + Convert.ToDouble(txtRtoAmnt.Text)).ToString("N2");

            //    txtRRT.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["REGN_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["REGN_AMNT"]);
            //    txtCDInsurance.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["INSU_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["INSU_AMNT"]);
            //    txtTCR.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["TRC_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["TRC_AMNT"]);
            //    txtAccesory.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["ACCR_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["ACCR_AMNT"]);
            //    txtOthers.Text = string.IsNullOrEmpty(Convert.ToString(DtDnote.Rows[0]["OTHERACCR_AMNT"])) ? "0.00" : Convert.ToString(DtDnote.Rows[0]["OTHERACCR_AMNT"]);
            //    txtVehAc.Text = Convert.ToString(Convert.ToDouble(txtTotalCost.Text.Trim()) - Convert.ToDouble(txtRRT.Text) - Convert.ToDouble(txtTCR.Text) - Convert.ToDouble(txtAccesory.Text) - Convert.ToDouble(txtOthers.Text) - Convert.ToDouble(txtCDInsurance.Text));
            //}
            //else
            //{
            //    ShowMessage("Delivery No. not found."); return;
            //}
            ddlSender.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
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
                Selectuserpref();
                txtCartage.Focus();
            }
            catch (Exception Ex)
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
        protected void txtSwchhBhartTx_OnTextChanged(object sender, EventArgs e)
        {
            try { netamntcal(); txtSwchhBhartTx.Focus(); }
            catch (Exception Ex) { }
        }
        protected void txtNetAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GstCalGR();
                txtNetAmnt.Focus();
            }
            catch (Exception Ex)
            {

            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateEdit();
                txtQuantity.Focus();
            }
            catch (Exception Ex) { }
        }
        protected void TxtEGPNo_TextChanged(object sender, EventArgs e)
        {
            //if (CheckforDuplicates(Convert.ToString(txtTaxInvoiceNo.Text.Trim())))
            //{
            //    txtTaxInvoiceNo.Focus(); txtTaxInvoiceNo.SelectText();
            //    this.ShowMessageErr("Duplicate INV.No!");
            //    return;
            //}
        }
        protected void txtDelvNo_TextChanged1(object sender, EventArgs e)
        {
            //if (CheckforDuplicates(Convert.ToString(txtDelvNo.Text.Trim())))
            //{
            //    txtDelvNo.Focus(); txtDelvNo.SelectText();
            //    this.ShowMessageErr("Duplicate DI No.!"); 
            //    return;
            //}
        }
        protected void txtTaxInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            //if (CheckforDuplicates(Convert.ToString(txtDelvNo.Text.Trim())))
            //{
            //    txtTaxInvoiceNo.Focus(); txtTaxInvoiceNo.SelectText();
            //    this.ShowMessageErr("Duplicate Tax Inv No.!");
            //    return;
            //}
        }
        /// <summary>
        /// chhecking dublicate from DI_NO
        /// </summary>
        /// <param name="number"></param>
        /// <param name="Year_Idno"></param>
        /// <returns></returns>
        private bool CheckforDuplicatesDI_NO(string number,Int64 Year_Idno)
        {
            
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from m in db.TblGrHeads
                           where m.DI_NO == number && m.Year_Idno == Year_Idno
                        select m.DI_NO).Count() > 0;
                //return (lst.Count() > 0);
            }
        }
        /// <summary>
        /// chhecking dublicate from Tax_InvNo
        /// </summary>
        /// <param name="number"></param>
        /// <param name="Year_Idno"></param>
        /// <returns></returns>
        private bool CheckforDuplicatesTax_InvNo(string number, Int64 Year_Idno)
        {

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from m in db.TblGrHeads
                        where m.Tax_InvNo == number && m.Year_Idno == Year_Idno
                        select m.Tax_InvNo).Count() > 0;
                //return (lst.Count() > 0);
            }
        }
        protected void txtGRNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //  userpref();
                GRPrepDAL objGR = new GRPrepDAL();
                string GRfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(GRfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString());
                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToString(txtDelvNo.Text.Trim()), Convert.ToString(txtTaxInvoiceNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
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
        //protected void txtGRDate_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtGRDate.Focus(); txtGRDate.SelectText();
        //    }
        //    catch (Exception Ex) { }
        //}
        #endregion

        #region CheckedChanged
        protected void txtQuantity_Unload(object sender, EventArgs e)
        {

        }
        protected void hidShrtgRate_ValueChanged(object sender, EventArgs e)
        {

        }
        protected void chkId_CheckedChanged(object sender, EventArgs e)
        { }
        protected void chkSelectAllRows_CheckedChanged(object sender, EventArgs e)
        {
            if (grdGrdetals.HeaderRow != null)
            {
                CheckBox chkSelectAll = (CheckBox)grdGrdetals.HeaderRow.FindControl("chkSelectAll");
            }
            foreach (GridViewRow row in grdGrdetals.Rows)
            {
                if (grdGrdetals.HeaderRow != null)
                {
                    CheckBox chkSelectAll = (CheckBox)grdGrdetals.HeaderRow.FindControl("chkSelectAll");
                    // CheckBox chkId = (CheckBox)grdGrdetals.Rows.FindControl("chkId");
                    for (int i = 0; i < grdGrdetals.Rows.Count; i++)
                    {
                        CheckBox chkId = (CheckBox)grdGrdetals.Rows[i].FindControl("chkId");
                        if (chkSelectAll.Checked == true)
                        {

                            chkId.Checked = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);
                        }
                        else
                        {
                            chkId.Checked = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);

                        }
                    }

                }
                // CheckBox chkSelect = (CheckBox)row.FindControl("chkSelectAll");

            }
        }
        protected void CvtxtRate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((txtrate.Text == string.Empty) || (Convert.ToDouble(txtrate.Text) <= 0))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        #endregion

        #endregion

        #region Account Posting GR by Abhishek Sharma

        private bool PostIntoAccounts(DataTable DtTemp, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, double OtherAmnt, double TotAmnt, double SrvTaxAmmnt, double GrossAmnt, double RoundOfAmnt, double SwachhBharat, double KrishiKalyan, Int32 intRcptType, string InstNo, string InstDate, string CustBank, Int32 GrType, Int32 PostingAccIdno, Int32 intServFlag, string GrDate, Int32 intYearIdno, Int32 intInvGenType, double SGSTAmount, double CGSTAmount, double IGSTAmount)
        {
            #region Variables Declaration...
            GRPrepDAL objGR = new GRPrepDAL();
            AcntDS = objGR.DtAcntDS(ApplicationFunction.ConnectionString());
            DsTrAcnt = objGR.DsTrAcnt(ApplicationFunction.ConnectionString());
            BindDropdownDAL obj = new BindDropdownDAL();
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            Int64 intVchrIdno = 0; Int64 intValue = 0; Int32 TrAcntIdno = 0;
            hidpostingmsg.Value = string.Empty;
            Int32 ICAcnt_Idno = 0, IOTAcnt_Idno = 0, ISAcnt_Idno = 0, SwachBharat_Idno = 0, KrishiKalyan_Idno = 0, SGST_Idno = 0, CGST_Idno = 0 , IGST_Idno = 0;
            Int64 intDocNo = objGR.SelectGRnoByGRIdno(intDocIdno);

            if (intServFlag != 1) { SrvTaxAmmnt = 0.00; SwachhBharat = 0.00; KrishiKalyan = 0.00; }
            DateTime? dtBankDate = null;

            #endregion

            #region Account link Validations...
            if (AcntDS == null || AcntDS.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Account link is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Account link is not defined. Kindly define.";
                return false;
            }
            else
            {
                lblmessage.Visible = false; lblmessage.Text = "";
            }
            ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["CAcnt_Idno"]));
            IOTAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["OTAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["OTAcnt_Idno"]));
            ISAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["SAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["SAcnt_Idno"]));
            SwachBharat_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["SwachBharat_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["SwachBharat_Idno"]));
            KrishiKalyan_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["KrishiKalyan_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["KrishiKalyan_Idno"]));
            SGST_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["Sgst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["Sgst_Idno"]));
            CGST_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["Cgst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["Cgst_Idno"]));
            IGST_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["Igst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["Igst_Idno"]));
            if (IOTAcnt_Idno <= 0)
            {
                hidpostingmsg.Value = "Other Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Other Account is not defined. Kindly define.";
                return false;
            }
            
            if (ICAcnt_Idno <= 0)
            {
                hidpostingmsg.Value = "Commission Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Commission Account is not defined. Kindly define.";
                return false;
            }
           
            if (ISAcnt_Idno <= 0)
            {
                hidpostingmsg.Value = "Service Tax Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Service Tax Account is not defined. Kindly define.";
                return false;
            }
            
            if (SwachBharat_Idno <= 0)
            {
                hidpostingmsg.Value = "Swachh Bharat Cess Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Swachh Bharat Cess  Account is not defined. Kindly define.";
                return false;
            }
            
            if (KrishiKalyan_Idno <= 0)
            {
                hidpostingmsg.Value = "Krishi Kalyan Cess Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* Krishi Kalyan Cess  Account is not defined. Kindly define.";
                return false;
            }

            if (SGST_Idno <= 0) 
            {
                hidpostingmsg.Value = "SGST Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* SGST   Account is not defined. Kindly define.";
                return false;
            }

            if (CGST_Idno <= 0)
            {
                hidpostingmsg.Value = "CGST Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* CGST   Account is not defined. Kindly define.";
                return false;
            }
            
            if (IGST_Idno <= 0)
            {
                hidpostingmsg.Value = "IGST Account is not defined. Kindly define.";
                lblmessage.Visible = true; lblmessage.Text = "* IGST   Account is not defined. Kindly define.";
                return false;
            }
            
            #endregion

            if (GrType == 1) //1:paid //
            {
                #region Paid Type Posting Start...

                if (Request.QueryString["Gr"] == null)
                {
                    intValue = 1;
                }
                else
                {
                    intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
                }
                DataTable dt = obj.BindRcptTypeDel(intRcptType, ApplicationFunction.ConnectionString());

                if (intValue > 0)   /*Insert In VchrHead*/
                {
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrDate)),
                    2,
                    Convert.ToInt64(intRcptType),
                    " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                    true,
                    0,
                    strDocType,
                    Convert.ToInt64(PostingAccIdno),
                    0,
                    0,
                    ApplicationFunction.GetIndianDateTime().Date,
                    0,
                    0,
                    intYearIdno,
                    0, intUserIdno);
                    if (intValue > 0)
                    {
                        #region Party Account Posting...
                        /*Insert In VchrDetl*/
                        intVchrIdno = intValue;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt64(intRcptType),
                        " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                        (intServFlag != 1) ? (GrossAmnt + OtherAmnt) : (TotAmnt - RoundOfAmnt),
                        Convert.ToByte(2),
                        Convert.ToByte(0),
                        "",
                        true,
                        dtBankDate,  //please check here if date is Blank
                        "0", 0);
                        if (intValue > 0)
                        {
                            if (GrossAmnt > 0)
                            {
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                    intVchrIdno,
                                    PostingAccIdno,
                                   " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                    Convert.ToDouble(Math.Abs(GrossAmnt + OtherAmnt + RoundOfAmnt)),
                                    Convert.ToByte(1),
                                    Convert.ToByte(0),
                                    InstNo,
                                    false,
                                    (string.IsNullOrEmpty(InstDate)) ? dtBankDate : Convert.ToDateTime(ApplicationFunction.mmddyyyy(InstDate)),  //please check here if date is Blank
                                    CustBank, 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            if (SrvTaxAmmnt > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(ISAcnt_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(SrvTaxAmmnt)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            if (OtherAmnt > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(IOTAcnt_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(OtherAmnt)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }

                            }
                            if (SwachhBharat > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(SwachBharat_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(SwachhBharat)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            if (KrishiKalyan > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(KrishiKalyan_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(KrishiKalyan)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }

                            }
                            #region SGST/CGST/IGST Account........
                            if (SGSTAmount > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(SGST_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(SGSTAmount)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            if (CGSTAmount > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(CGST_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(CGSTAmount)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            if (IGSTAmount > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        Convert.ToInt32(IGST_Idno),
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                        Convert.ToDouble(Math.Abs(IGSTAmount)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                            #endregion
                            if (intValue > 0)
                            {
                                intValue = 0; /*Insert In VchrIdDetl*/
                                intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                if (intValue == 0)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }

                        #endregion

                        #region RoundOff Account Posting...

                        if (intValue > 0)
                        {
                            if (Convert.ToDouble(dblRndOff) != 0)
                            {
                                Int64 intRoundOffId = 0;
                                intRoundOffId = objclsAccountPosting.GetRoundOffId();
                                if (intRoundOffId == 0)
                                {
                                    hidpostingmsg.Value = "GR. has amount in roundoff, but Round Off Account is not defined. Kindly define.";
                                    return false;
                                }
                                intValue = objclsAccountPosting.InsertInVchrHead(
                                Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrDate)),
                                4,
                                0,
                               " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                true,
                                0,
                                strDocType,
                                0,
                                0,
                                0,
                                dtBankDate,
                                0,
                                0,
                                Convert.ToInt32(intYearIdno),
                                0, intUserIdno);
                                if (intValue > 0)
                                {
                                    intVchrIdno = intValue;
                                    intValue = 0;
                                    for (int i = 0; i < 2; i++)
                                    {
                                        intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            (i == 0 ? intRcptType : intRoundOffId),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(dblRndOff)),
                                            Convert.ToByte(i == 0 ? (dblRndOff < 0 ? 1 : 2) : (dblRndOff < 0 ? 2 : 1)),
                                            Convert.ToByte(0),
                                            "",
                                            Convert.ToBoolean(i == 0 ? true : false),
                                            null,
                                            "", 0);
                                        if (intValue == 0)
                                        {
                                            return false;
                                        }
                                    }
                                    if (intValue > 0)
                                    {
                                        intValue = 0;
                                        intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                        if (intValue == 0)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }

                            }
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                #endregion
            }
            else if (GrType == 3)//3:To Pay//
            {
                if (intInvGenType == 1)
                {
                    #region To Pay Type Posting Start...

                    if (DsTrAcnt == null || DsTrAcnt.Rows.Count <= 0)
                    {
                        hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                        lblmessage.Visible = true; lblmessage.Text = "* Transport Account is not defined. Kindly define.";
                        return false;
                    }
                    else
                    {
                        lblmessage.Visible = false; lblmessage.Text = "";
                        TrAcntIdno = Convert.ToInt32(DsTrAcnt.Rows[0]["TransportAccountID"]);
                    }

                    if (Request.QueryString["Gr"] == null)
                    {
                        intValue = 1;
                    }
                    else
                    {
                        intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
                    }
                    if (intValue > 0)
                    {
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrDate)),
                        4,
                        0,
                        " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                        true,
                        0,
                        strDocType,
                        0,
                        0,
                        Convert.ToInt64(intDocNo),
                        ApplicationFunction.GetIndianDateTime().Date,
                        0,
                        0,
                        Convert.ToInt32(intYearIdno),
                        0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;

                            #region Party Posting...

                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt64(PostingAccIdno),
                            " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                            (intServFlag != 1) ? (GrossAmnt + OtherAmnt) : (TotAmnt - RoundOfAmnt),
                            Convert.ToByte(2),
                            Convert.ToByte(0),
                            "",
                            true,
                            dtBankDate,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                if (GrossAmnt > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        TrAcntIdno,
                                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                       Convert.ToDouble(Math.Abs(GrossAmnt)),
                                        Convert.ToByte(1),
                                        Convert.ToByte(0),
                                        "",
                                        false,
                                        dtBankDate,  //please check here if date is Blank
                                        "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (OtherAmnt > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno,
                                       Convert.ToInt32(IOTAcnt_Idno),
                                      " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                      Convert.ToDouble(Math.Abs(OtherAmnt)),
                                       Convert.ToByte(1),
                                       Convert.ToByte(0),
                                       "",
                                       false,
                                       dtBankDate,  //please check here if date is Blank
                                       "", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (SrvTaxAmmnt > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno,
                                       Convert.ToInt32(ISAcnt_Idno),
                                      " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                      Convert.ToDouble(Math.Abs(SrvTaxAmmnt)),
                                       Convert.ToByte(1),
                                       Convert.ToByte(0),
                                       "",
                                       false,
                                       dtBankDate,  //please check here if date is Blank
                                       "", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (SwachhBharat > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(SwachBharat_Idno),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(SwachhBharat)),
                                            Convert.ToByte(1),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            dtBankDate,  //please check here if date is Blank
                                            "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (KrishiKalyan > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(KrishiKalyan_Idno),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(KrishiKalyan)),
                                            Convert.ToByte(1),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            dtBankDate,  //please check here if date is Blank
                                            "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }

                                }
                                #region SGST/CGST/IGST Account........
                                if (SGSTAmount > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(SGST_Idno),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(SGSTAmount)),
                                            Convert.ToByte(1),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            dtBankDate,  //please check here if date is Blank
                                            "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (CGSTAmount > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(CGST_Idno),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(CGSTAmount)),
                                            Convert.ToByte(1),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            dtBankDate,  //please check here if date is Blank
                                            "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (IGSTAmount > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(IGST_Idno),
                                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                            Convert.ToDouble(Math.Abs(IGSTAmount)),
                                            Convert.ToByte(1),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            dtBankDate,  //please check here if date is Blank
                                            "0", 0);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                #endregion
                                if (intValue > 0)
                                {
                                    intValue = 0; /*Insert In VchrIdDetl*/
                                    intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }

                            #endregion

                            #region RoundOff Account Posting...

                            if (intValue > 0)
                            {
                                if (Convert.ToDouble(dblRndOff) != 0)
                                {
                                    Int64 intRoundOffId = 0;
                                    intRoundOffId = objclsAccountPosting.GetRoundOffId();
                                    if (intRoundOffId == 0)
                                    {
                                        hidpostingmsg.Value = "GR. has amount in roundoff, but Round Off Account is not defined. Kindly define.";
                                        return false;
                                    }
                                    intValue = objclsAccountPosting.InsertInVchrHead(
                                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(GrDate)),
                                    4,
                                    0,
                                   " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                    true,
                                    0,
                                    strDocType,
                                    0,
                                    0,
                                    0,
                                    dtBankDate,
                                    0,
                                    0,
                                    Convert.ToInt32(intYearIdno),
                                    0, intUserIdno);
                                    if (intValue > 0)
                                    {
                                        intVchrIdno = intValue;
                                        intValue = 0;
                                        for (int i = 0; i < 2; i++)
                                        {
                                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                                intVchrIdno,
                                                (i == 0 ? PostingAccIdno : intRoundOffId),
                                               " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + GrDate,
                                                Convert.ToDouble(Math.Abs(dblRndOff)),
                                                Convert.ToByte(i == 0 ? (dblRndOff < 0 ? 1 : 2) : (dblRndOff < 0 ? 2 : 1)),
                                                Convert.ToByte(0),
                                                "",
                                                Convert.ToBoolean(i == 0 ? true : false),
                                                null,
                                                "", 0);
                                            if (intValue == 0)
                                            {
                                                return false;
                                            }
                                        }
                                        if (intValue > 0)
                                        {
                                            intValue = 0;
                                            intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                            if (intValue == 0)
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }

                                }
                            }
                            else
                            {
                                return false;
                            }
                            #endregion
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    #endregion
                }
            }

            #region Deallocate variables...

            objclsAccountPosting = null;

            return true;

            #endregion
        }

        #endregion

        #region Comment...

        //private void Recalculation(Int32 intGrPrepIdno)
        //{
        //    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        if (intGrPrepIdno > 0)
        //        {
        //            string strRoundOff = TxtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
        //            if (this.PostIntoAccounts(dtTemp, intGrPrepIdno, "GR", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), 0, 0, 0, 0) == true)
        //            {
        //                tScope.Complete();

        //                hidGRHeadIdno.Value = "";
        //                ViewState["dt"] = dtTemp = null;
        //                this.BindGridT();
        //                ddlGRType.Focus();
        //            }
        //            else
        //            {
        //                if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
        //                {

        //                    tScope.Dispose();
        //                }
        //                tScope.Dispose();
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
        //                return;
        //            }
        //        }
        //    }
        //}

        //private bool ChallanGen(Int64 FromCity, Int64 TruckIdno, Int64 DriverIdno, Int32 ServType, double CommistionAmnt, double NetAmnt, double AdvanceAmnt, Int64 PaymentType, DateTime Instdate, Int64 InstNo, Int64 BankIdno)
        //{

        //}


        //Acc Posting of Excel Importing GR By Peeyush   
        //private bool PostIntoAccountsForExcel(DataTable DtTemp, Int64 intDocIdno, Int32 intGrNo, string strGrType, string strDocType, double dTotAmnt, double dSrvTaxAmmnt, double GrossAmnt, double RoundOfAmnt, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, string strGrDate, string strSenderIdno, Int32 ISAcnt_Idno, Int32 IOTAcnt_Idno, int intRcptType, string strInsNo, string strInsDate, int intCustBank, Int64 intRoundOffId, Int32 TrAcntIdno, double OtherCharges)
        //{
        //    #region Variables Declaration...
        //    GRPrepDAL objGR = new GRPrepDAL();
        //    Int64 intVchrIdno = 0;
        //    Int64 intValue = 0, IntRcptType = 0;
        //    //Int32 TrAcntIdno = 0;

        //    DataSet ROUNDDStest = new DataSet();
        //    Int64 intDocNo = 0;
        //    intDocNo = objGR.SelectGRnoByGRIdno(intDocIdno);
        //    objGR = null;

        //    clsAccountPosting objclsAccountPosting = new clsAccountPosting();
        //    #endregion

        //    if (strGrType == "1") //1:paid //
        //    {
        //        #region Paid Type Posting Start...

        //        intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
        //        if (intValue > 0)   /*Insert In VchrHead*/
        //        {
        //            intValue = objclsAccountPosting.InsertInVchrHead(
        //            Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //            2,
        //            0, //PLEASE INSERT IN EXCEL AT GR AT PAID GR CONDITION     ---------------------------------------------------------------------PEEYUSH CHECK
        //            " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //            true,
        //            0,
        //            strDocType,
        //            Convert.ToInt64((strSenderIdno == "") ? 0 : Convert.ToInt64(strSenderIdno)),
        //            0,
        //            0,
        //            ApplicationFunction.GetIndianDateTime().Date,
        //            0,
        //            0,
        //            Convert.ToInt32(intYearIdno),
        //            0, intUserIdno);
        //            if (intValue > 0)
        //            {
        //                intVchrIdno = intValue;

        //                #region Party Account Posting + Other Amount + Service Tax + Gross Amount Posting...

        //                intValue = 0;
        //                //INS DATE  
        //                DateTime? dtBankDate;
        //                if (strInsDate == "")
        //                {
        //                    dtBankDate = null;
        //                }
        //                else
        //                {
        //                    dtBankDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(strInsDate));
        //                }
        //                //double TotAmnt = dTotAmnt;   
        //                /*Insert In VchrDetl*/
        //                intValue = objclsAccountPosting.InsertInVchrDetl(intVchrIdno, Convert.ToInt64((strSenderIdno == "") ? 0 : Convert.ToInt64(strSenderIdno)), " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate, dTotAmnt, Convert.ToByte(1), Convert.ToByte(0), txtInstNo.Text.Trim(), true,
        //                dtBankDate,  //please check here if date is Blank
        //                Convert.ToString(intCustBank), 0);
        //                if (intValue > 0)
        //                {
        //                    intVchrIdno = intValue;
        //                    intValue = 0;
        //                    for (int i = 0; i < 3; i++)
        //                    {
        //                        intValue = objclsAccountPosting.InsertInVchrDetl(
        //                            intVchrIdno,
        //                            ((i == 0) ? IntRcptType : (i == 1) ? Convert.ToInt32(ISAcnt_Idno) : Convert.ToInt32(IOTAcnt_Idno)),
        //                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                            Convert.ToDouble(Math.Abs(((i == 0) && (GrossAmnt > 0) ? GrossAmnt : (i == 1) && (dSrvTaxAmmnt > 0) ? dSrvTaxAmmnt : (i == 2) && (OtherCharges > 0) ? OtherCharges : (i == 100) ? 0 : 0))),
        //                            Convert.ToByte(2),
        //                            Convert.ToByte(0),
        //                            strInsNo,
        //                            false,
        //                            dtBankDate,  //please check here if date is Blank
        //                            Convert.ToString(intCustBank), 0);
        //                        if (intValue == 0)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    if (intValue > 0)
        //                    {
        //                        intValue = 0; /*Insert In VchrIdDetl*/
        //                        intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
        //                        if (intValue == 0)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //                #endregion

        //                #region RoundOff Account Posting...

        //                if (intValue > 0)
        //                {
        //                    if (Convert.ToDouble(RoundOfAmnt) != 0)
        //                    {
        //                        intValue = objclsAccountPosting.InsertInVchrHead(
        //                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //                        4,
        //                        0,
        //                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                        true,
        //                        0,
        //                        strDocType,
        //                        0,
        //                        0,
        //                        0,
        //                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //                        0,
        //                        0,
        //                        Convert.ToInt32(intYearIdno),
        //                        0, intUserIdno);
        //                        if (intValue > 0)
        //                        {
        //                            intVchrIdno = intValue;
        //                            intValue = 0;
        //                            for (int i = 0; i < 2; i++)
        //                            {
        //                                intValue = objclsAccountPosting.InsertInVchrDetl(
        //                                    intVchrIdno,
        //                                    (i == 0 ? Convert.ToInt64((strSenderIdno == "") ? 0 : Convert.ToInt64(strSenderIdno)) : intRoundOffId),
        //                                   " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                                    Convert.ToDouble(Math.Abs(RoundOfAmnt)),
        //                                    Convert.ToByte(i == 0 ? (RoundOfAmnt < 0 ? 2 : 1) : (RoundOfAmnt < 0 ? 1 : 2)),
        //                                    Convert.ToByte(0),
        //                                    "",
        //                                    Convert.ToBoolean(i == 0 ? true : false),
        //                                    null,
        //                                    "", 0);
        //                                if (intValue == 0)
        //                                {
        //                                    return false;
        //                                }
        //                            }
        //                            if (intValue > 0)
        //                            {
        //                                intValue = 0;
        //                                intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
        //                                if (intValue == 0)
        //                                {
        //                                    return false;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return false;
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        #endregion
        //    }
        //    else if (strGrType == "3")//3:To Pay//
        //    {
        //        #region To Pay Type Posting Start...

        //        intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);

        //        if (intValue > 0)
        //        {
        //            intValue = objclsAccountPosting.InsertInVchrHead(
        //            Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //            4,
        //            0,
        //            "GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //            true,
        //            0,
        //            strDocType,
        //            0,
        //            0,
        //            Convert.ToInt64(intDocNo),
        //            ApplicationFunction.GetIndianDateTime().Date,
        //            0,
        //            0,
        //            Convert.ToInt32(intYearIdno),
        //            0, intUserIdno);
        //            if (intValue > 0)
        //            {
        //                intVchrIdno = intValue;

        //                #region Party Account Posting + Other Amount + Service Tax + Gross Amount Posting...

        //                intValue = 0;
        //                /*Insert In VchrDetl*/
        //                intValue = objclsAccountPosting.InsertInVchrDetl(
        //                intVchrIdno,
        //                Convert.ToInt64((strSenderIdno == "") ? 0 : Convert.ToInt64(strSenderIdno)),
        //                " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                dTotAmnt,
        //                Convert.ToByte(2),
        //                Convert.ToByte(0),
        //                "",
        //                true,
        //                null,  //please check here if date is Blank
        //                "", 0);
        //                if (intValue > 0)
        //                {
        //                    intVchrIdno = intValue;
        //                    intValue = 0;
        //                    for (int i = 0; i < 3; i++)
        //                    {
        //                        intValue = objclsAccountPosting.InsertInVchrDetl(
        //                            intVchrIdno,
        //                            ((i == 0) ? TrAcntIdno : (i == 1) ? Convert.ToInt32(ISAcnt_Idno) : Convert.ToInt32(IOTAcnt_Idno)),
        //                           " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + intRcptType,
        //                           Convert.ToDouble(Math.Abs(((i == 0) && (GrossAmnt > 0) ? GrossAmnt : (i == 1) && (dSrvTaxAmmnt > 0) ? dSrvTaxAmmnt : (i == 2) && (OtherCharges > 0) ? OtherCharges : (i == 100) ? 0 : 0))),
        //                            Convert.ToByte(1),
        //                            Convert.ToByte(0),
        //                            "",
        //                            false,
        //                            null,  //please check here if date is Blank
        //                            "", 0);
        //                        if (intValue == 0)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                    if (intValue > 0)
        //                    {
        //                        intValue = 0; /*Insert In VchrIdDetl*/
        //                        intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
        //                        if (intValue == 0)
        //                        {
        //                            return false;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //                #endregion

        //                #region RoundOff Account Posting...

        //                if (intValue > 0)
        //                {
        //                    if (Convert.ToDouble(RoundOfAmnt) != 0)
        //                    {
        //                        intValue = objclsAccountPosting.InsertInVchrHead(
        //                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //                        4,
        //                        0,
        //                       " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                        true,
        //                        0,
        //                        strDocType,
        //                        0,
        //                        0,
        //                        0,
        //                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strGrDate)),
        //                        0,
        //                        0,
        //                        Convert.ToInt32(ddlDateRange.SelectedValue),
        //                        0, intUserIdno);
        //                        if (intValue > 0)
        //                        {
        //                            intVchrIdno = intValue;
        //                            intValue = 0;
        //                            for (int i = 0; i < 2; i++)
        //                            {
        //                                intValue = objclsAccountPosting.InsertInVchrDetl(
        //                                    intVchrIdno,
        //                                    (i == 0 ? Convert.ToInt64((strSenderIdno == "") ? 0 : Convert.ToInt64(strSenderIdno)) : intRoundOffId),
        //                                   " GR. No: " + Convert.ToString(intDocNo) + " GR. Date: " + strGrDate,
        //                                    Convert.ToDouble(Math.Abs(RoundOfAmnt)),
        //                                    Convert.ToByte(i == 0 ? (RoundOfAmnt < 0 ? 2 : 1) : (RoundOfAmnt < 0 ? 1 : 2)),
        //                                    Convert.ToByte(0),
        //                                    "",
        //                                    Convert.ToBoolean(i == 0 ? true : false),
        //                                    null,
        //                                    "", 0);
        //                                if (intValue == 0)
        //                                {
        //                                    return false;
        //                                }
        //                            }
        //                            if (intValue > 0)
        //                            {
        //                                intValue = 0;
        //                                intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
        //                                if (intValue == 0)
        //                                {
        //                                    return false;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return false;
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        #endregion
        //    }

        //    #region Deallocate variables...

        //    objclsAccountPosting = null;

        //    return true;

        //    #endregion
        //}

        #endregion

        #region Excel Uploading By Peeyush Kaushik

        protected string RandomNumber()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999); // creates a number between 1 and 12
            return Convert.ToString(myRandomNo);
        }

        protected void lnkbtnUpload_OnClick(object sender, EventArgs e)
        {
            Int64 CreatedBy = Convert.ToInt64(Session["UserIdno"]);
            GRPrepDAL objGRprep = new GRPrepDAL();
            string excelfilename = string.Empty;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            AcntDS = objGRprep.DtAcntDS(ApplicationFunction.ConnectionString());
            DsTrAcnt = objGRprep.DsTrAcnt(ApplicationFunction.ConnectionString());
            if (AcntDS == null || AcntDS.Rows.Count <= 0) { ShowMessageErr("Account link is not defined. Kindly define."); return; }
            if (DsTrAcnt != null && DsTrAcnt.Rows.Count <= 0) { ShowMessageErr("Transport Account is not defined. Kindly define."); return; }

            Int64 UserIdno = (string.IsNullOrEmpty(Convert.ToString(Session["UserIdno"])) ? 0 : Convert.ToInt64(Session["UserIdno"]));
            Int32 ICAcnt_Idno = 0, IOTAcnt_Idno = 0, ISAcnt_Idno = 0, TrAcntIdno = 0;
            Int64 intRoundOffId = 0; intRoundOffId = objclsAccountPosting.GetRoundOffId();
            ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["CAcnt_Idno"]));
            IOTAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["OTAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["OTAcnt_Idno"]));
            ISAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntDS.Rows[0]["SAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntDS.Rows[0]["SAcnt_Idno"]));
            TrAcntIdno = Convert.ToInt32(DsTrAcnt.Rows[0]["TransportAccountID"]);
            if (IOTAcnt_Idno <= 0) { ShowMessageErr("Other Account is not defined. Kindly define."); return; }
            if (ISAcnt_Idno <= 0) { ShowMessageErr("Service Tax Account is not defined. Kindly define."); return; }
            if (intRoundOffId <= 0) { ShowMessageErr("GR. has amount in roundoff, but Round Off Account is not defined. Kindly define."); return; }
            try
            {
                string strFilename = "ImportGrExcel" + Session["UserIdno"].ToString() + RandomNumber();
                //string directoryPath = Server.MapPath(string.Format("~/{0}/", "ItemsexcelGR"));
                //if (!Directory.Exists(directoryPath))
                //{
                //    Directory.CreateDirectory(directoryPath);
                //}

                #region UPLOAD EXCEL AT SERVER
                excelfilename = ApplicationFunction.UploadFileOnServer(FileUpload, "ItemsexcelGR", "GrExcelImport", strFilename);
                #endregion

                if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                {
                    GRPrepDAL objGRPrep = new GRPrepDAL();
                    string msg = string.Empty;
                    DataTable dt = new DataTable();
                    string filepath = Server.MapPath("~/ItemsexcelGR/" + excelfilename);
                    string constring = string.Empty;
                    if (System.IO.Path.GetExtension(filepath) == ".xls")
                    {
                        constring = "Provider=Microsoft.Jet.OLEDB.4.0;OLE DB Services=-4;Data Source='" + filepath + "';Extended Properties='Excel 8.0;HDR=Yes;'";
                    }
                    else if (System.IO.Path.GetExtension(filepath) == ".xlsx")
                    {
                        constring = "Provider= Microsoft.ACE.OLEDB.12.0;OLE DB Services=-4;Data Source='" + filepath + "'; Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    }
                    if (string.IsNullOrEmpty(constring) == false)
                    {
                        #region  Select Excel
                        OleDbConnection con = new OleDbConnection(constring);
                        con.Open();
                        DataTable ExcelTable = new DataTable();
                        ExcelTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        string SheetName = Convert.ToString(ExcelTable.Rows[0][2]);
                        //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "]", con);
                        OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "] WHERE [PerfNo] IS NOT NULL OR	[GRNo] IS NOT NULL OR [GRDate] IS NOT NULL OR [GrType] IS NOT NULL OR [FromCity] IS NOT NULL OR [ToCity] IS NOT NULL OR [TruckNo] IS NOT NULL OR [ItemName] IS NOT NULL OR [Unit] IS NOT NULL OR [RateType] IS NOT NULL OR [Qty] IS NOT NULL OR [Weight] IS NOT NULL OR [Rate] IS NOT NULL OR [ItemDetails] IS NOT NULL OR [Sender] IS NOT NULL OR [Receiver] IS NOT NULL OR [DeliveryPlace] IS NOT NULL OR [ViaCity] IS NOT NULL OR [DlNo] IS NOT NULL OR [EGPNo] IS NOT NULL OR [ShipmentNo] IS NOT NULL OR [Remark] IS NOT NULL OR [FixedAmount] IS NOT NULL OR [ReceiptType] IS NOT NULL OR [InstNo] IS NOT NULL OR [InstDate] IS NOT NULL OR [CustBank] IS NOT NULL OR [RefNo] IS NOT NULL OR [OrderNo] OR NOT NULL OR [FormNo] OR NULL ", con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        #endregion

                        dt = ApplicationFunction.CreateTable("tblGrUploadFromExcel", "PerfNo", "String", "GRNo", "String", "GRDate", "String", "GrType", "String", "FromCity", "String", "ToCity", "String", "TruckNo", "String", "ItemName", "String", "Unit", "String", "RateType", "String", "Qty", "String", "Weight", "String", "Rate", "String", "ItemDetails", "String", "Sender", "String", "Receiver", "String", "DeliveryPlace", "String", "ViaCity", "String", "DlNo", "String", "EGPNo", "String", "ShipmentNo", "String", "Remark", "String", "FixedAmount", "String", "ReceiptType", "String", "InstNo", "String", "InstDate", "String", "CustBank", "String", "RefNo", "String", "OrderNo", "String", "FormNo", "String", "Unloading", "String", "FromCityIdno", "String", "ToCityIdno", "String", "LorryIdno", "String", "ItemIdno", "String", "UnitIdno", "String", "SenderIdno", "String", "RecivrIdno", "String", "DelvryPlceIdno", "String", "ExistsFlag", "String");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns[0].Caption == "PerfNo" && ds.Tables[0].Columns[1].Caption == "GRNo" && ds.Tables[0].Columns[2].Caption == "GRDate" && ds.Tables[0].Columns[3].Caption == "GrType" && ds.Tables[0].Columns[4].Caption == "FromCity" && ds.Tables[0].Columns[5].Caption == "ToCity" && ds.Tables[0].Columns[6].Caption == "TruckNo" && ds.Tables[0].Columns[7].Caption == "ItemName" && ds.Tables[0].Columns[8].Caption == "Unit" && ds.Tables[0].Columns[9].Caption == "RateType" && ds.Tables[0].Columns[10].Caption == "Qty" && ds.Tables[0].Columns[11].Caption == "Weight" && ds.Tables[0].Columns[12].Caption == "Rate" && ds.Tables[0].Columns[13].Caption == "ItemDetails" && ds.Tables[0].Columns[14].Caption == "Sender" && ds.Tables[0].Columns[15].Caption == "Receiver" && ds.Tables[0].Columns[16].Caption == "DeliveryPlace" && ds.Tables[0].Columns[17].Caption == "ViaCity" && ds.Tables[0].Columns[18].Caption == "DlNo" && ds.Tables[0].Columns[19].Caption == "EGPNo" && ds.Tables[0].Columns[20].Caption == "ShipmentNo" && ds.Tables[0].Columns[21].Caption == "Remark" && ds.Tables[0].Columns[22].Caption == "FixedAmount" && ds.Tables[0].Columns[23].Caption == "ReceiptType" && ds.Tables[0].Columns[24].Caption == "InstNo" && ds.Tables[0].Columns[25].Caption == "InstDate" && ds.Tables[0].Columns[26].Caption == "CustBank" && ds.Tables[0].Columns[27].Caption == "RefNo" && ds.Tables[0].Columns[28].Caption == "OrderNo" && ds.Tables[0].Columns[29].Caption == "FormNo" && ds.Tables[0].Columns[30].Caption == "Unloading")
                            {
                                #region  Truncate Table First
                                int resultget = objGRPrep.TurncatetblGrUploadFromExcel(ApplicationFunction.ConnectionString());
                                #endregion

                                #region INSERT ONE BY ONE RECORD IN tblGrUploadFromExcel TABLE
                                Int64 intResult = 0;
                                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        DateTime? dtGr = null;
                                        DateTime? dtInst = null;
                                        if (ds.Tables[0].Rows[i]["GRDate"].ToString() != "") { dtGr = Convert.ToDateTime(ds.Tables[0].Rows[i]["GRDate"].ToString()); }
                                        if (ds.Tables[0].Rows[i]["InstDate"].ToString() != "") { dtInst = Convert.ToDateTime(ds.Tables[0].Rows[i]["InstDate"].ToString()); }
                                        intResult = objGRPrep.InsertInGrExcel(
                                            Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["GRNo"]) == "" ? "0" : ds.Tables[0].Rows[i]["GRNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["PerfNo"]) == "" ? "" : ds.Tables[0].Rows[i]["PerfNo"]),
                                            dtGr,
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["GrType"]) == "" ? "" : ds.Tables[0].Rows[i]["GrType"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["FromCity"]) == "" ? "" : ds.Tables[0].Rows[i]["FromCity"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ToCity"]) == "" ? "" : ds.Tables[0].Rows[i]["ToCity"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["TruckNo"]) == "" ? "" : ds.Tables[0].Rows[i]["TruckNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]) == "" ? "" : ds.Tables[0].Rows[i]["ItemName"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Unit"]) == "" ? "" : ds.Tables[0].Rows[i]["Unit"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["RateType"]) == "" ? "" : ds.Tables[0].Rows[i]["RateType"]),
                                            Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["Qty"]) == "" ? "1" : ds.Tables[0].Rows[i]["Qty"]),
                                            Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Weight"]) == "" ? "0" : Convert.ToDouble(ds.Tables[0].Rows[i]["Weight"]).ToString("N2")),
                                            Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Rate"]) == "" ? "0" : Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]).ToString("N2")),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ItemDetails"]) == "" ? "" : ds.Tables[0].Rows[i]["ItemDetails"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Sender"]) == "" ? "" : ds.Tables[0].Rows[i]["Sender"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Receiver"]) == "" ? "" : ds.Tables[0].Rows[i]["Receiver"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["DeliveryPlace"]) == "" ? "" : ds.Tables[0].Rows[i]["DeliveryPlace"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ViaCity"]) == "" ? "" : ds.Tables[0].Rows[i]["ViaCity"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["DlNo"]) == "" ? "" : ds.Tables[0].Rows[i]["DlNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["EGPNo"]) == "" ? "" : ds.Tables[0].Rows[i]["EGPNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ShipmentNo"]) == "" ? "" : ds.Tables[0].Rows[i]["ShipmentNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Remark"]) == "" ? "" : ds.Tables[0].Rows[i]["Remark"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["FixedAmount"]) == "" ? "" : Convert.ToDouble(ds.Tables[0].Rows[i]["FixedAmount"]).ToString("N2")),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ReceiptType"]) == "" ? "" : ds.Tables[0].Rows[i]["ReceiptType"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["InstNo"]) == "" ? "" : ds.Tables[0].Rows[i]["InstNo"]),
                                            dtInst,
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["CustBank"]) == "" ? "" : ds.Tables[0].Rows[i]["CustBank"]),
                                            Convert.ToInt64(ddlDateRange.SelectedValue == "" ? "0" : ddlDateRange.SelectedValue), base.CompId, base.UserIdno,
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["RefNo"]) == "" ? "" : ds.Tables[0].Rows[i]["RefNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]) == "" ? "" : ds.Tables[0].Rows[i]["OrderNo"]),
                                            Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["FormNo"]) == "" ? "" : ds.Tables[0].Rows[i]["FormNo"]),
                                            Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Unloading"]) == "" ? "0.00" : ds.Tables[0].Rows[i]["Unloading"]));
                                    }
                                    if (intResult > 0)
                                    {
                                        Tran.Complete();
                                    }
                                    else
                                    {
                                        Tran.Dispose();
                                    }
                                }
                                #endregion

                                dt = null;
                                DataTable dtDistinct = new DataTable();
                                DataTable dtSelect = new DataTable();
                                tblUserPref hiduserpref = objGRprep.selectuserpref();
                                int inttype = Convert.ToInt32(ddlExcelUploadTypeWise.SelectedValue);
                                dtDistinct = objGRPrep.SelectDistinctFromExcel(ApplicationFunction.ConnectionString(), inttype, "SelectDistinctFromExcel");
                                if (dtDistinct != null && dtDistinct.Rows.Count > 0)
                                {
                                    Int64 intGrPrepIdno = 0;
                                    dtTemp = CreateDt();
                                    dtTemp = (DataTable)ViewState["dt"];
                                    for (int i = 0; i < dtDistinct.Rows.Count; i++)
                                    {
                                        dtSelect = objGRPrep.SelectTableByGrNo(ApplicationFunction.ConnectionString(), dtDistinct.Rows[i]["PrefixGr_No"].ToString(), dtDistinct.Rows[i]["GR_No"].ToString(), dtDistinct.Rows[i]["City_Name"].ToString(), Convert.ToInt64(ddlDateRange.SelectedValue == "" ? "0" : ddlDateRange.SelectedValue), "SelectTableByGrNo");
                                        dt = objGRPrep.SelectTempTable(ApplicationFunction.ConnectionString(), dtDistinct.Rows[i]["PrefixGr_No"].ToString(), dtDistinct.Rows[i]["GR_No"].ToString(), dtDistinct.Rows[i]["City_Idno"].ToString(), Convert.ToInt64(ddlDateRange.SelectedValue == "" ? "0" : ddlDateRange.SelectedValue), "SelectTempTable");
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            DateTime dtGRDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(dtDistinct.Rows[i]["Gr_Date"].ToString()));
                                            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
                                            //Please Check
                                            int STaxPaidBy = 2; int Agent = 0;
                                            int ReceiptType = 0;
                                            string InstNo = ""; string strInstDate = "";
                                            DateTime? dtInstDate = null;
                                            int intcustBankIdno = 0;
                                            if ((string.IsNullOrEmpty(dt.Rows[0]["GR_TYPE"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["GR_TYPE"].ToString())) == 1) { ReceiptType = string.IsNullOrEmpty(dt.Rows[0]["GR_TYPE"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["GR_TYPE"].ToString()); InstNo = string.IsNullOrEmpty(dt.Rows[0]["InstNo"].ToString()) ? "" : Convert.ToString(dt.Rows[0]["InstNo"].ToString()); if ((string.IsNullOrEmpty(dt.Rows[0]["InstNo"].ToString()) ? "" : Convert.ToString(dt.Rows[0]["InstNo"].ToString())) != "") { strInstDate = dt.Rows[0]["InstDate"].ToString(); intcustBankIdno = string.IsNullOrEmpty(dt.Rows[0]["CustBankIdNo"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["CustBankIdNo"].ToString()); dtInstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt.Rows[0]["InstDate"].ToString())); } }
                                            //Declare Variables
                                            double GrossAmnt = 0.00; double Commission = 0.00; double TollTaxamnt = 0.00; double Cartage = 0.00; double Bilty = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(HiddBiltyAmnt.Value)) ? "0" : String.Format("{0:0,0.00}", (HiddBiltyAmnt.Value))); double DSubTotal = 0.00; double DTotalAmnt = 0.00; double DWages = 0.00; double DServTax = 0.00; double DSwchBrtTax = 0.00; double DSurchrge = 0.00; double DPF = 0.00; double DNetAmnt = 0.00; double DRoundOffAmnt = 0.00; Int64 RcptGoodHeadIdno = 0; Int64 AdvOrderGR_Idno = 0; Boolean isTBBRate = false; Int32 itruckcitywise = 0; string swages = "";
                                            double TypeAmnt = 0;
                                            var lststate = objGRprep.GetStateIdno(Convert.ToInt32(Convert.ToString(Convert.ToString(dtDistinct.Rows[i]["City_Idno"].ToString()) == "" ? "" : dtDistinct.Rows[i]["City_Idno"].ToString())));
                                            this.GrRateControl();
                                            if (lststate != null)
                                            {
                                                HiddServTaxPer.Value = Convert.ToString(objGRprep.SelectServiceTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(dtGRDate)));
                                                HiddSwachhBrtTaxPer.Value = Convert.ToString(objGRprep.SelectSwacchBrtTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(dtGRDate)));
                                                HiddKalyanTax.Value = Convert.ToString(objGRprep.SelectKalyanTaxFromTaxMaster(Convert.ToInt64(Convert.ToString(lststate.State_Idno) == "" ? 0 : Convert.ToInt64(lststate.State_Idno)), Convert.ToDateTime(dtGRDate)));
                                            }
                                            //Declare Variables
                                            double DKalyanTaxEx = 0.00;
                                            GrossAmnt = 0.00; Cartage = 0.00; TollTaxamnt = 0.00; DSurchrge = 0.00; Commission = 0.00; Bilty = 0.00;
                                            DWages = 0.00; DPF = 0.00; DSubTotal = 0.00; DNetAmnt = 0.00; DServTax = 0.00; DSwchBrtTax = 0.00;
                                            double ItemAmountSum = 0;
                                            dtTemp.Clear();
                                            for (int j = 0; j < dt.Rows.Count; j++)
                                            {
                                                if (CheckDuplicatieItemForExcel(dtTemp, Convert.ToString(dt.Rows[j]["Item_IdNo"].ToString()), Convert.ToString(dt.Rows[j]["UOM_IdNo"].ToString())) == false)
                                                {
                                                    Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                                                    int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                                                    string strItemName = dt.Rows[j]["Item_Name"].ToString();
                                                    string strItemNameId = dt.Rows[j]["Item_IdNo"].ToString();
                                                    string strUnitName = dt.Rows[j]["UOM_Name"].ToString();
                                                    string strUnitNameId = dt.Rows[j]["UOM_IdNo"].ToString();
                                                    string strRateType = dt.Rows[j]["Rate_TypeName"].ToString();
                                                    int strRateTypeIdno = Convert.ToInt32((dt.Rows[j]["Rate_Type"].ToString()) == "" ? "1" : dt.Rows[j]["Rate_Type"].ToString());
                                                    string strQty = Convert.ToString((dt.Rows[j]["QTY"].ToString()) == "" ? "0" : dt.Rows[j]["QTY"].ToString());
                                                    string strWeight = Convert.ToString((dt.Rows[j]["Item_Weight"].ToString()) == "" ? "0" : dt.Rows[j]["Item_Weight"].ToString());
                                                    string strRate = "0";
                                                    if (hiduserpref.TBB_Rate == true) { if (inttype == 1) { strRate = Convert.ToString((dt.Rows[j]["Item_Rate"].ToString()) == "" ? "0" : dt.Rows[j]["Item_Rate"].ToString()); } }
                                                    string strAmount = "0";
                                                    if (hiduserpref.TBB_Rate == true) { if (inttype == 1) { if (strRateTypeIdno == 1) { strAmount = Convert.ToDouble((Convert.ToDouble(strQty)) * (Convert.ToDouble(strRate))).ToString("N2"); } else { strAmount = Convert.ToDouble((Convert.ToDouble(strWeight)) * (Convert.ToDouble(strRate))).ToString("N2"); } } }
                                                    string strDetail = Convert.ToString((dt.Rows[j]["Detail"].ToString()) == "" ? "" : dt.Rows[j]["Detail"].ToString());
                                                    string strhidShrtgRate = "0"; string strhidShrtgRateOther = "0";
                                                    string strhidShrtgLimit = "0"; string strhidShrtgLimitOther = "0";
                                                    ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDetail, strhidShrtgLimit, strhidShrtgRate, strhidShrtgLimitOther, strhidShrtgRateOther);
                                                    ItemAmountSum = Convert.ToDouble(ItemAmountSum + Convert.ToDouble(strAmount));
                                                }
                                            }

                                            if (hiduserpref.TBB_Rate == true) { if (inttype != 1) { ItemAmountSum = Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["FixedAmount"].ToString()).ToString("N2")); } }
                                            GrossAmnt = ItemAmountSum;

                                            if (hiduserpref.TBB_Rate == true)
                                            {
                                                DSurchrge = Convert.ToDouble(Convert.ToDouble((GrossAmnt * (Convert.ToDouble(Convert.ToDouble(hiduserpref.Surchg_Per).ToString("N2"))) / 100)).ToString("N2"));
                                                DWages = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Unloading"])) ? 0 : Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Unloading"]).ToString("N2"));
                                                Bilty = Convert.ToDouble(Convert.ToDouble(hiduserpref.Bilty_Amnt).ToString("N2"));
                                                TollTaxamnt = Convert.ToDouble(Convert.ToDouble(hiduserpref.TollTax_Amnt).ToString("N2"));

                                                DTotalAmnt = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(GrossAmnt) + Convert.ToDouble(Cartage)).ToString("N2"));
                                                DSubTotal = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DTotalAmnt) + Convert.ToDouble(DSurchrge) + Convert.ToDouble(Commission) + Convert.ToDouble(Bilty) + Convert.ToDouble(DWages) + Convert.ToDouble(DPF) + Convert.ToDouble(TollTaxamnt)).ToString("N2"));

                                                DServTax = Convert.ToDouble(Convert.ToDouble(((Convert.ToDouble(DSubTotal) * Convert.ToDouble(HiddServTaxPer.Value)) / 100)).ToString("N2"));
                                                dSwacchBhrtTaxPer = Convert.ToDouble(Convert.ToString(HiddSwachhBrtTaxPer.Value) == "" ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value));
                                                DSwchBrtTax = Convert.ToDouble(Convert.ToDouble(((Convert.ToDouble(DSubTotal) * dSwacchBhrtTaxPer) / 100)).ToString("N2"));

                                                dKalyanTax = Convert.ToDouble(Convert.ToString(HiddKalyanTax.Value) == "" ? 0 : Convert.ToDouble(HiddKalyanTax.Value));
                                                DKalyanTaxEx = Convert.ToDouble(Convert.ToDouble(((Convert.ToDouble(DSubTotal) * dKalyanTax) / 100)).ToString("N2"));
                                            }
                                            DNetAmnt = Convert.ToDouble(Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(DSubTotal) + Convert.ToDouble(DServTax) + Convert.ToDouble(DSwchBrtTax) + Convert.ToDouble(DKalyanTaxEx)))).ToString("N2"));
                                            DRoundOffAmnt = Convert.ToDouble(Math.Round(Convert.ToDouble(DNetAmnt)).ToString("N2")) - Convert.ToDouble(DNetAmnt);
                                            DNetAmnt = Convert.ToDouble(Math.Round(Convert.ToDouble(DNetAmnt)).ToString("N2"));
                                            Double dblTaxValids = string.IsNullOrEmpty(HiddServTaxValid.Value) ? 0 : Convert.ToDouble(HiddServTaxValid.Value);
                                            Double dblServTaxPercs = string.IsNullOrEmpty(HiddServTaxPer.Value) ? 0 : Convert.ToDouble(HiddServTaxPer.Value);
                                            Double dblSwcgBrtTaxPercs = string.IsNullOrEmpty(HiddSwachhBrtTaxPer.Value) ? 0 : Convert.ToDouble(HiddSwachhBrtTaxPer.Value);
                                            Double dblKalyanTaxPers = string.IsNullOrEmpty(HiddKalyanTax.Value) ? 0 : Convert.ToDouble(HiddKalyanTax.Value);
                                            Double TotItemValue = string.IsNullOrEmpty(txtTotItemPrice.Text.Trim()) ? 0 : Convert.ToDouble(txtTotItemPrice.Text.Trim());
                                            Double dblFromKM = string.IsNullOrEmpty(txtFromKm.Text.Trim()) ? 0 : Convert.ToDouble(txtFromKm.Text.Trim());
                                            Double dblToKM = string.IsNullOrEmpty(txtToKM.Text.Trim()) ? 0 : Convert.ToDouble(txtToKM.Text.Trim());
                                            Double dblTotKM = string.IsNullOrEmpty(txtTotKM.Text.Trim()) ? 0 : Convert.ToDouble(txtTotKM.Text.Trim());
                                            double OtherCharges = 0;
                                            OtherCharges = DSubTotal - GrossAmnt;
                                            if (dtSelect.Rows.Count == dtTemp.Rows.Count)
                                            {
                                                if ((string.IsNullOrEmpty(dt.Rows[0]["GR_TYPE"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["GR_TYPE"].ToString())) == 1 && ReceiptType != 0)
                                                {

                                                }
                                                else
                                                {
                                                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                                                    {
                                                        intGrPrepIdno = objGRPrep.InsertGRByExcel(dtGRDate, 1, Convert.ToInt32(dt.Rows[0]["GR_TYPE"].ToString()), dt.Rows[0]["DI_NO"].ToString(), dt.Rows[0]["EGP_NO"].ToString(), Convert.ToInt64(dt.Rows[0]["GR_NO"].ToString()), Convert.ToInt32(dt.Rows[0]["Sender_Idno"].ToString()), Convert.ToInt32(dt.Rows[0]["Lorry_Idno"].ToString()), Convert.ToInt32(dt.Rows[0]["Recivr_Idno"].ToString()), Convert.ToInt32(dt.Rows[0]["From_CityIdNo"].ToString()), Convert.ToInt32(dt.Rows[0]["To_CityIdNo"].ToString()), Convert.ToInt32(dt.Rows[0]["DelvryPlce_Idno"].ToString()), Agent, dt.Rows[0]["Remark"].ToString(), STaxPaidBy, ReceiptType, InstNo, dtInstDate, intcustBankIdno, GrossAmnt, Commission, TollTaxamnt, Cartage, Bilty, DSubTotal, DTotalAmnt, DWages, DServTax, DSwchBrtTax, DSurchrge, DPF, DNetAmnt, DRoundOffAmnt, YearIdno, RcptGoodHeadIdno, AdvOrderGR_Idno, Convert.ToBoolean(hidTBBType.Value), itruckcitywise, swages, dtTemp, dt.Rows[0]["Shipment_No"].ToString(), Convert.ToString(dt.Rows[0]["PrefixGr_No"].ToString()), "", 0, 0, "", Convert.ToInt32(dt.Rows[0]["Cityvia_Idno"].ToString()), Convert.ToInt64(ddlExcelUploadTypeWise.SelectedValue), TypeAmnt, true, Convert.ToString(dt.Rows[0]["Ref_No"].ToString()), "", DKalyanTaxEx, "", UserIdno, Convert.ToString(txtconsnr.Text.Trim()), dblTaxValids, dblServTaxPercs, dblSwcgBrtTaxPercs, dblKalyanTaxPers, TotItemValue, Convert.ToString(dt.Rows[0]["Ordr_No"].ToString()), Convert.ToString(dt.Rows[0]["Form_No"].ToString()), "", "", '0', "", txtLoc.Text.Trim(), Convert.ToBoolean(hidDelPlace.Value), dblFromKM, dblToKM, dblTotKM, CreatedBy);
                                                        if (intGrPrepIdno != null && intGrPrepIdno > 0)
                                                        {
                                                            //AccountPosting FOR Excel
                                                            //if (Convert.ToInt32(dt.Rows[0]["GR_TYPE"].ToString()) != 2) { if (this.PostIntoAccountsForExcel(dtTemp, intGrPrepIdno, Convert.ToInt32(dt.Rows[0]["GR_NO"].ToString()), dt.Rows[0]["GR_TYPE"].ToString(), "GR", DTotalAmnt, DServTax, GrossAmnt, DRoundOffAmnt, 0, 0, 0, 0, dtDistinct.Rows[i]["Gr_Date"].ToString(), dt.Rows[0]["Sender_Idno"].ToString(), ICAcnt_Idno, IOTAcnt_Idno, ReceiptType, InstNo, strInstDate, intcustBankIdno, intRoundOffId, TrAcntIdno, OtherCharges) == true) { } }
                                                            for (int k = 0; k < dt.Rows.Count; k++)
                                                            {
                                                                objGRPrep.UpdateFlag(ApplicationFunction.ConnectionString(), Convert.ToInt64(dt.Rows[k]["Gr_Idno"].ToString()));
                                                            }
                                                            tScope.Complete();
                                                        }
                                                        tScope.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                        else { this.ShowMessageErr("No records match. Please Check Excel First!"); lblExcelMessage.InnerText = ""; FileUpload.Attributes.Clear(); }
                                    }
                                    dtTable = objGRPrep.DsErrorExcel(ApplicationFunction.ConnectionString());
                                    if (dtTable != null && dtTable.Rows.Count > 0) { lblExcelMessage.InnerText = "Some Record gives Error, Please Check the Error Excel."; FileUpload.Dispose(); Export(dtTable); }  // Export Error Excel
                                    else { this.ShowMessage("Record's Inserted Successfully."); lblExcelMessage.InnerText = ""; FileUpload.Dispose(); dt = null; dtTable = null; dtTemp = null; dtSelect = null; }
                                }
                                else
                                {
                                    ShowMessageErr("Please Check mandatory fields in Excel.");
                                    lblExcelMessage.InnerText = ""; FileUpload.Dispose();
                                }
                            }
                            else { dtTemp = null; dtTemp = CreateExcelHeaderFormat(); if (dtTemp != null) { lblExcelMessage.InnerText = "Excel Header Not match Please Check *GRExcelImportHeaderFormat.xls*"; FileUpload.Dispose(); ExportExcelHeader(dtTemp); } }

                        }
                        else { ShowMessageErr("No records found !"); dtTemp = CreateExcelHeaderFormat(); lblExcelMessage.InnerText = ""; FileUpload.Dispose(); dtTemp = null; return; }
                    }
                }
                else { ShowMessageErr("Please Browse a excel file !"); return; }
            }
            catch (System.Threading.ThreadAbortException Ex) { }
            catch (Exception Ex)
            {
                dtTemp = CreateExcelHeaderFormat();
                ShowMessageErr("Oops Something Went Wrong, Please Check Excel Files Data !");
                FileUpload.Attributes.Clear();
                if (dtTemp != null) { lblExcelMessage.InnerText = "Please Check Header."; FileUpload.Dispose(); ExportExcelHeader(dtTemp); }
                dtTemp = null; dtTable = null;
            }

        }

        #region Function for Upload Excel

        private DataTable CreateExcelHeaderFormat()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "PerfNo", "String",
                "GRNo", "String",
                "GRDate", "String",
                "GrType", "String",
                "FromCity", "String",
                "ToCity", "String",
                "TruckNo", "String",
                "ItemName", "String",
                "Unit", "String",
                "RateType", "String",
                "Qty", "String",
                "Weight", "String",
                "Rate", "String",
                "ItemDetails", "String",
                "Sender", "String",
                "Receiver", "String",
                "DeliveryPlace", "String",
                "ViaCity", "String",
                "DlNo", "String",
                "EGPNo", "String",
                "ShipmentNo", "String",
                "Remark", "String",
                "FixedAmount", "String",
                "ReceiptType", "String",
                "InstNo", "String",
                "InstDate", "String",
                "CustBank", "String",
                "RefNo", "String",
                "OrderNo", "String",
                "FormNo", "String",
                "Unloading", "String",
                "FromCityIdno", "String",
                "ToCityIdno", "String",
                "LorryIdno", "String",
                "ItemIdno", "String",
                "UnitIdno", "String",
                "SenderIdno", "String",
                "RecivrIdno", "String",
                "DelvryPlceIdno", "String",
                "ExistsFlag", "String");
            return dttemp;
        }

        private bool CheckDuplicatieItemForExcel(DataTable dtItemCheck, string ItemIdNo, string UOMIdNo)
        {
            bool value = false;
            if ((dtItemCheck != null) && (dtItemCheck.Rows.Count > 0))
            {
                foreach (DataRow row in dtItemCheck.Rows)
                {
                    if ((Convert.ToString(row["Item_IdNo"]) == ItemIdNo) && (Convert.ToString(row["Unit_Idno"]) == UOMIdNo))
                    {
                        value = true;
                    }
                }
            }
            return value;
        }
        private void Export(DataTable Dt)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ErrorExcel.xls"));
                Response.AppendHeader("content-disposition", "attachment; filename=ErrorExcel.xls");
                Response.ContentType = "application/ms-excel";
                string str = string.Empty;
                foreach (DataColumn dtcol in Dt.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in Dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.Flush();
                Response.End();
            }
            catch { }
        }
        private void ExportExcelHeader(DataTable Dt)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GRExcelImportHeaderFormat.xls"));
                Response.AppendHeader("content-disposition", "attachment; filename=GRExcelImportHeaderFormat.xls");
                Response.ContentType = "application/ms-excel";
                string str = string.Empty;
                foreach (DataColumn dtcol in Dt.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in Dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.Flush();
                Response.End();
            }
            catch { }
        }

        #endregion

        protected void txtweight_TextChanged(object sender, EventArgs e)
        {
            if (IsWeight == true)
            {
                FillRateWeightWiseRate();
                if ((hidTBBType.Value == "False") && (Convert.ToInt32(ddlGRType.SelectedValue) == 2))
                    txtrate.Text = "0.00";
            }
            else
            {
                GRPrepDAL objGr = new GRPrepDAL();
                if (HiddUserPrefCont.Value == "1")
                {
                    if (ddlRateType.SelectedValue == "2")
                    {
                        if ((Convert.ToString(txtweight.Text) != "0") || (Convert.ToString(txtweight.Text) != "0.00"))
                        {
                            if (ddlContainerSize.SelectedIndex == 0) { ShowMessageErr("Please define Container Size"); txtweight.Focus(); txtweight.Text = "0.00"; return; }
                            else { HiddConSize.Value = ddlContainerSize.SelectedValue; }
                            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
                            DateTime dtGRDate = strGrDate;

                            ItemWtAmnt = objGr.SelectItemWghtRateForTBBContWise(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue), Convert.ToDouble(txtweight.Text), Convert.ToInt32(HiddConSize.Value));
                            txtrate.Text = ItemWtAmnt.ToString("N2");

                            iWghtShrtgLimit = objGr.SelectWghtShrtgLimitContWise(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue), Convert.ToDouble(txtweight.Text), Convert.ToInt32(HiddConSize.Value));
                            hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");

                            iWghtShrtgRate = objGr.SelectWghtShrtgRateContWise(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), dtGRDate, Convert.ToInt64(ddlCityVia.SelectedValue), Convert.ToDouble(txtweight.Text), Convert.ToInt32(HiddConSize.Value));
                            hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");

                        }
                    }
                }
            }
            txtrate.Focus();
        }

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
        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void lnkJainPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void lnkOMCargo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void lnkwithoutamount_Click(object sender, EventArgs e)
        {
            hidPages.Value = ddlPage.SelectedValue;
            GRPrepDAL objGRDAL = new GRPrepDAL();
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
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                    {
                        string url = "PrintOMCargo.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else if(Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                    {
                        string url = "TrLogistics.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno);
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
                    chkbit = 1;
                    PrintGRPrep(Convert.ToInt32(Request.QueryString["Gr"]));
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
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                {
                    string url = "PrintOMCargo.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                {
                    string url = "TrLogistics.aspx" + "?q=" + Convert.ToInt64(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                {
                    string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(Request.QueryString["Gr"]);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }
        }
        protected void lnkWithamount_click(object sender, EventArgs e)
        {
            GRPrepDAL objGRDAL = new GRPrepDAL();
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
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                    {
                        string url = "PrintOMCargo.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                    {
                        string url = "TrLogistics.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                    {
                        string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else if (Convert.ToInt32(objuserpref.GRPrintPref) == 8)
                    {
                        string url = "GR.aspx" + "?q=" + Convert.ToInt64(iMaxGRIdno) + "&P=1"; //+ Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                        //PrintGRPrepJainBulk(iMaxGRIdno); ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
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
                    chkbit = 2;
                    PrintGRPrep(Convert.ToInt32(Request.QueryString["Gr"]));
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
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 4)
                {
                    string url = "PrintOMCargo.aspx" + "?q=" + Convert.ToInt32(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    //PrintGRPrepJainBulk(Convert.ToInt32(Request.QueryString["Gr"]));
                    //divAmntHead.Visible = true; divAmntvalue.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 6)
                {
                    string url = "TrLogistics.aspx" + "?q=" + Convert.ToInt64(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else if (Convert.ToInt32(objuserpref.GRPrintPref) == 7)
                {
                    string url = "GrPrintKajaria.aspx" + "?q=" + Convert.ToInt64(Request.QueryString["Gr"]) + "&P=" + Convert.ToInt64(ddlPage.SelectedValue);
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }
        }

        public void SGST_Changed(object sender, EventArgs e)
        {
            ReCalculateGST();
        }
        public void CGST_Changed(object sender, EventArgs e)
        {
            ReCalculateGST();
        }
        public void IGST_Changed(object sender, EventArgs e)
        {
            ReCalculateGST();
        }

        #endregion
    }
}