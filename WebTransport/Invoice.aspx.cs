using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;

namespace WebTransport
{
    public partial class Invoice : Pagebase
    {
        #region Private Variables...
        double dShortageGST = 0;
        DataTable DtTemp = new DataTable();
        DataTable childTemp = new DataTable();
        DataTable sortedDT = new DataTable();
        DataTable dtTemp = new DataTable(); string con = "";
        double dblNetAmnt = 0; bool IsTBBRate = false;
        double dDiffShrtge = 0, dGrossShrtgeAmnt = 0, dNetShrtAmnt = 0, dTotShrtAmnt = 0, dQty;
        bool bBilled = false;
        int chkbit = 0;
        Double dServTaxPer = 0, dTotServTax = 0, dNetAmnt = 0, dOtherAmnt = 0, dWages = 0, dAmnt = 0; double dtotServTaxAmnt = 0; double dTotWeight = 0;
        bool ServTaxExmpt = false; double lblQty = 0, lblRate = 0, lbllAmount = 0, lblWayges = 0, lblOtherAmnt = 0, lblSwchBrtNet = 0, lblKisanNet = 0, lblServNet = 0, lblSGSTNet = 0, lblCGSTNet = 0, lblIGSTNet = 0, lblNetAmount = 0;
        double dTotServTaxTrnsportr = 0; double dTotSwchBrtTrnsportr = 0; double dTotKisanTaxConsgn = 0; double dTotKisanTaxTrnsportr = 0; double dTotServTaxConsgn = 0; double dTotSwchBrtTaxConsgn = 0; double dtotAmnt = 0; bool TbbRate = false; DataTable AcntLinkDS, DsTransAcnt;
        double dTotSGSTConsigner, dTotCGSTConsigner, dTotIGSTConsigner, dTotGSTCessConsigner = 0;
        double dTotSGSTTransporter, dTotCGSTTransporter, dTotIGSTTransporter, dTotGSTCessTransporter = 0;
        double dTotReptWeight = 0, dTOtAmnt = 0, dTotOther = 0, dShorAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotReptServTax = 0, dTotSwatchTax, dTotKisanTax; Int32 companyId;
        Int32 totqty = 0, iprintType = 0; double totprintweight = 0, totprintshortage = 0, printtotamnt = 0, printctax = 0, printstax = 0, printrcptAmnt = 0, printnetamnt = 0;
        private int intFormId = 31;
        double FreightAmount = 0.0; double HQChargesAmount = 0.0; double ChargeAmount1 = 0.0; double ChargeAmount2 = 0.0; string ChargesName1 = ""; string ChargesName2 = "";
        static string TaxPadiBy = "";
        double sper = 6;
        double cper = 6;
        double iper = 12;
        double dtotA = 0; double Dtotqty = 0; double DTAMNT = 0;
        //Int64 printFormat;
        InvoiceDAL objInvoiceDAL = new InvoiceDAL();
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
           Session["RCM"] = Convert.ToString(CheckRCM.Checked);
            
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
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                InvoiceDAL objInvoiceDAL = new InvoiceDAL();
                ddlGrType.SelectedValue = "GR";
                this.Bind();
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                // BindCity();
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                lnkbtnSearch.Visible = true;
                tblUserPref obj = objInvoiceDAL.SelectUserPref();
                HidTbbRate.Value = Convert.ToString(obj.TBB_Rate);
                hidPrintType.Value = Convert.ToString(obj.InvPrint_Type);
                hidAdminApp.Value = Convert.ToString(obj.AdminApp_Inv);
                hidrefno.Value = Convert.ToString(obj.Reflabel_Gr);
                if (Convert.ToBoolean(hidAdminApp.Value) == true)
                {
                    if (Convert.ToString(Session["Userclass"]) == "Admin")
                    {
                        DivApprov.Visible = true;
                    }
                }
                else
                {
                    DivApprov.Visible = false;
                }

                if (Request.QueryString["q"] != null  && Convert.ToInt32(hidPrintType.Value) == 6 )
                {
                    lnkprint111.Visible = true;

                }
                else 
                {
                    lnkprint111.Visible = false;
                }
                //if (Request.QueryString["q"] != null && Convert.ToInt32(hidPrintType.Value) == 7)
                //{
                //    string Value = Request.QueryString["q"];
                //    string[] Array = Value.Split(new char[] { '-' });
                //    string ID = Array[0].ToString();
                //    HidGrId.Value = ID;
                //    string Type = Array[1].ToString();
                //    HidGrType.Value = Type;
                //    Populate(Convert.ToInt64(ID), Type);
                //    lnkprint111.Visible = false;
                //    lnkbtnpr.Visible = true;  
                //    InvoiceDAL obj1 = new InvoiceDAL();
                //    var lst = obj1.SelectPricIdno(Convert.ToInt64(ddlSenderName.SelectedValue));
                //    Session["fromcity"] = ddlFromCity.SelectedItem.Text;
                //    obj = null;
                //    if (lst != null && (Convert.ToInt32(lst.PComp_Idno) == 8))
                //    {
                //        LinkButton1.Visible = true;
                //        lnkprintInvoice.Visible = true;
                //        lnkbtn.Visible = false;
                //        lnkbtnpr.Visible = false;  
                //    }

                //   else
                //   {
                //       LinkButton1.Visible = false;
                //       lnkprintInvoice.Visible = false;
                //        lnkbtn.Visible = false;
                //        //lnkbtnpr.Visible = true;  
                //    }
                //    if (lst != null && (Convert.ToInt32(lst.PComp_Idno) == 9))
                //    {
                //        lnktaxinvoice.Visible = true;
                //        lnkpjlbillinvoice.Visible = true;
                //        lnkpjlbillSummary.Visible = true;
                //        lnkbtnpr.Visible = false;  
                //    }
                //    else
                //   {
                //        lnktaxinvoice.Visible = false;
                //        lnkpjlbillinvoice.Visible = false;
                //        lnkpjlbillSummary.Visible = false;
                //       // lnkbtnpr.Visible = true;  
                //    }
 
                //}
                //else
                //{
                //    LinkButton1.Visible = false;
                //    lnkprintInvoice.Visible = false;
                //    lnktaxinvoice.Visible = false;
                //    lnkbtnpr.Visible = false;
                //    lnkbtn.Visible = false;
                //}
                if (Convert.ToInt32(hidPrintType.Value) == 1)
                {
                    if (Request.QueryString["q"] != null)
                    {
                        drpPrintType.Visible = true;
                        lblPrintType.Visible = true;
                    }
                    else
                    {
                        drpPrintType.Visible = false;
                        lblPrintType.Visible = false;
                    }
                }
                else
                {
                    drpPrintType.Visible = false;
                    lblPrintType.Visible = false;
                }
                ddlFromCity.Enabled = true;
                if (ddlFromCity.SelectedIndex > 0)
                {

                    if (ddlFromCity.SelectedIndex > 0)
                    {
                        txtinvoicNo.Text = Convert.ToString(objInvoiceDAL.SelectMaxInvNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), txtInvPreIx.Text));
                    }
                }
                if (Request.QueryString["q"] != null)
                {

                    string Value = Request.QueryString["q"];

                    //string[] strMails = Request.QueryString["q"].Split('-');
                    string[] Array = Value.Split(new char[] { '-' });

                    string ID = Array[0].ToString();

                    HidGrId.Value = ID;
                    string Type = Array[1].ToString();

                    HidGrType.Value = Type;


                    Populate(Convert.ToInt64(ID), Type);
                    hidid.Value = Convert.ToString(ID);
                    lnkbtnNew.Visible = true;
                    //ImgPrint1.Visible = true;
                    if (Convert.ToInt32(hidePrintMultipal.Value) == 1)
                    {
                        ImgPrint1.Visible = false;
                        imgPrint.Visible = false;
                        ImgPrint2.Visible = false;
                        ImgPrint3.Visible = false;
                        ImgPrintMultipal.Visible = true;
                        if (Convert.ToInt32(hidPrintType.Value) == 7)
                        {
                            ImgPrintMultipal.Visible = false;
                        }
                        DivPrintFormat.Visible = true;
                        lnkbtnPrint.Visible = false;

                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 1)
                    {
                        ImgPrint1.Visible = true;
                        imgPrint.Visible = false;
                        ImgPrint2.Visible = false;
                        ImgPrint3.Visible = false;
                        ImgPrintMultipal.Visible = false;
                        DivPrintFormat.Visible = false;
                        lnkbtnPrint.Visible = false;
                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 4)
                    {
                        imgPrint.Visible = false;
                        ImgPrint1.Visible = false;
                        ImgPrintMultipal.Visible = false;
                        DivPrintFormat.Visible = false;
                        lnkbtnPrint.Visible = false;
                        if (hidjainbulk.Value == "1")
                            ImgPrint2.Visible = true;
                        else
                            ImgPrint3.Visible = true;
                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 5)
                    {
                        ImgPrint1.Visible = false;
                        imgPrint.Visible = false;
                        ImgPrint2.Visible = false;
                        ImgPrint3.Visible = false;
                        ImgPrintMultipal.Visible = false;
                        DivPrintFormat.Visible = false;
                        lnkbtnPrint.Visible = true;

                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 7)
                    {
                        lnkprint111.Visible = false;
                        lnkbtnpr.Visible = true;
                        InvoiceDAL obj1 = new InvoiceDAL();
                        var lst = obj1.SelectPricIdno(Convert.ToInt64(ddlSenderName.SelectedValue));
                        Session["fromcity"] = ddlFromCity.SelectedItem.Text;
                        obj = null;
                        if (lst != null && (Convert.ToInt32(lst.PComp_Idno) == 8))
                        {
                            LinkButton1.Visible = true;
                            lnkprintInvoice.Visible = true;
                            lnkbtn.Visible = false;
                            lnkbtnpr.Visible = false;
                        }

                        else
                        {
                            LinkButton1.Visible = false;
                            lnkprintInvoice.Visible = false;
                            lnkbtn.Visible = false;
                            //lnkbtnpr.Visible = true;  
                        }
                        if (lst != null && (Convert.ToInt32(lst.PComp_Idno) == 9))
                        {
                            lnktaxinvoice.Visible = true;
                            lnkpjlbillinvoice.Visible = true;
                            lnkpjlbillSummary.Visible = true;
                            lnkbtnpr.Visible = false;
                        }
                        else
                        {
                            lnktaxinvoice.Visible = false;
                            lnkpjlbillinvoice.Visible = false;
                            lnkpjlbillSummary.Visible = false;
                            // lnkbtnpr.Visible = true;  
                        }
 
                    }
                    else
                    {
                        if (Convert.ToInt32(hidPrintType.Value) == 6)
                        {
                            imgPrint.Visible = false;
                        }
                        if (Convert.ToInt32(hidPrintType.Value) == 7)
                        {
                            imgPrint.Visible = false;
                        }
                        ImgPrint3.Visible = false;
                        ImgPrint1.Visible = false;
                        ImgPrint2.Visible = false;
                        ImgPrintMultipal.Visible = false;
                        DivPrintFormat.Visible = false;
                        lnkbtnPrint.Visible = false;
                        LinkButton1.Visible = false;
                        lnkprintInvoice.Visible = false;
                        lnktaxinvoice.Visible = false;
                        lnkbtnpr.Visible = false;
                        lnkbtn.Visible = false;
                    }
                }
                else
                {
                    lnkbtnNew.Visible = false;
                    imgPrint.Visible = false;
                    ImgPrint1.Visible = false;
                    ImgPrintMultipal.Visible = false;
                    lnkbtnPrint.Visible = false;
                    this.PostingLeft();
                }
                txtinvoicNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBiltyCharges.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtPlantInDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtPlantOutDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtPortinDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtPortoutDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtPlantAmount.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtPortAmount.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtAddFreight.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtHQCharges.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtOtherChargesAmount1.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtOtherChargesAmount2.Attributes.Add("onkeypress", "allowOnlyFloatNumber(event)");
                txtOtherCharge1.Attributes.Add("onkeypress", "allowAlphabetAndNumer(event)");
                txtOtherCharge2.Attributes.Add("onkeypress", "allowAlphabetAndNumer(event)");
                ddlSenderName.Enabled = true;
                //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
                //RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");
                GetPreferences();
            }
        }
        #endregion

        #region Button Evnets...
        protected void lnkbtnclear_OnClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind(); lnkbtnSubmit.Visible = false; lnkbtnclear.Visible = false;
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            //if ((dtTemp != null) && (dtTemp.Rows.Count == 0)) { this.ShowMessageErr("Please enter Details ."); return; }
            if (grdMain.Rows.Count > 0)
            {
                DtTemp = (DataTable)ViewState["dt"];

                DtTemp = CreateDt();
                InvoiceDAL obj = new InvoiceDAL();
                #region Parent Entery...
                tblInvGenHead objtblInvGenHead = new tblInvGenHead();
                Int32 InvNO = 0;
                if (txtinvoicNo.Text != "")
                {
                    InvNO = Convert.ToInt32(txtinvoicNo.Text);
                }
                objtblInvGenHead.Inv_No = InvNO;
                objtblInvGenHead.Inv_prefix = txtInvPreIx.Text;
                objtblInvGenHead.Inv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                objtblInvGenHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
                objtblInvGenHead.Sendr_Idno = Convert.ToInt32((ddlSenderName.SelectedIndex <= 0) ? "0" : ddlSenderName.SelectedValue);
                objtblInvGenHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);
                objtblInvGenHead.GrossTot_Amnt = Convert.ToDouble(txtGrosstotal.Text);
                objtblInvGenHead.TrServTax_Amnt = Convert.ToDouble(txtTrServTax.Text);
                objtblInvGenHead.ConsignrServTax = Convert.ToDouble(txtCSServTax.Text);

                objtblInvGenHead.TrSwchBrtTax_Amnt = Convert.ToDouble(txtTrSwchBrtTax.Text);
                objtblInvGenHead.ConsignrSwchBrtTax = Convert.ToDouble(txtCSSwchBrtTax.Text);

                objtblInvGenHead.TrKisanKalyanTax_Amnt = Convert.ToDouble(txtKisanTaxTrnptr.Text);
                objtblInvGenHead.ConsignrKisanTax_Amnt = Convert.ToDouble(txtKisanTax.Text);

                objtblInvGenHead.Short_Amnt = Convert.ToDouble(txtShortageAmnt.Text);
                objtblInvGenHead.Bilty_Chrgs = Convert.ToDouble(txtBiltyCharges.Text);
                objtblInvGenHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                objtblInvGenHead.RoundOff_Amnt = Convert.ToDouble(txtRoundOff.Text);
                objtblInvGenHead.TBB_Rate = Convert.ToBoolean(HidTbbRate.Value);
                objtblInvGenHead.Print_Format = Convert.ToInt64(ddlPrintLoc.Text);
                objtblInvGenHead.PlantAmount = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount.Text.Trim());
                objtblInvGenHead.PortAmount = string.IsNullOrEmpty(Convert.ToString(txtPortAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount.Text.Trim());
                objtblInvGenHead.PlantDays = string.IsNullOrEmpty(Convert.ToString(txtPlantDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays.Text.Trim());
                objtblInvGenHead.PortDays = string.IsNullOrEmpty(Convert.ToString(txtPortDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays.Text.Trim());

                //PlantPort Details - 2
                objtblInvGenHead.PlantAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount2.Text.Trim());
                objtblInvGenHead.PortAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPortAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount2.Text.Trim());
                objtblInvGenHead.PlantDays2 = string.IsNullOrEmpty(Convert.ToString(txtPlantDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays2.Text.Trim());
                objtblInvGenHead.PortDays2 = string.IsNullOrEmpty(Convert.ToString(txtPortDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays2.Text.Trim());

                objtblInvGenHead.Charges1_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge1.Text)) ? "" : Convert.ToString(txtOtherCharge1.Text);
                objtblInvGenHead.Charges2_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge2.Text)) ? "" : Convert.ToString(txtOtherCharge2.Text);

                objtblInvGenHead.Charges1_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount1.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount1.Text);
                objtblInvGenHead.Charges2_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount2.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount2.Text);

                objtblInvGenHead.AddCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtAddFreight.Text)) ? 0.00 : Convert.ToDouble(txtAddFreight.Text);
                objtblInvGenHead.HQCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtHQCharges.Text)) ? 0.00 : Convert.ToDouble(txtHQCharges.Text);

                //#GST 
                objtblInvGenHead.TrSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_SGST.Text)) ? 0.00 : Convert.ToDouble(txtT_SGST.Text);
                objtblInvGenHead.TrCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_CGST.Text)) ? 0.00 : Convert.ToDouble(txtT_CGST.Text);
                objtblInvGenHead.TrIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_IGST.Text)) ? 0.00 : Convert.ToDouble(txtT_IGST.Text);
                objtblInvGenHead.ConSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_SGST.Text)) ? 0.00 : Convert.ToDouble(txtC_SGST.Text);
                objtblInvGenHead.ConCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_CGST.Text)) ? 0.00 : Convert.ToDouble(txtC_CGST.Text);
                objtblInvGenHead.ConIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_IGST.Text)) ? 0.00 : Convert.ToDouble(txtC_IGST.Text);

                objtblInvGenHead.ConGSTCess_Amt = 0;
                objtblInvGenHead.TrGSTCess_Amt = 0;
                objtblInvGenHead.ShtgGST_Amt = Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text);
                DateTime? DtPlantInDate = null;
                DateTime? DtPlantOutDate = null;
                DateTime? DtPlantInDate2 = null;
                DateTime? DtPlantOutDate2 = null;
                if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false)
                {
                    DtPlantInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate.Text));
                }
                if (string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
                {
                    DtPlantOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate.Text));
                }
                if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false)
                {
                    DtPlantInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate2.Text));
                }
                if (string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
                {
                    DtPlantOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate2.Text));
                }
                DateTime? DtPortInDate = null;
                DateTime? DtPortOutDate = null;
                DateTime? DtPortInDate2 = null;
                DateTime? DtPortOutDate2 = null;
                if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false)
                {
                    DtPortInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate.Text));
                }
                if (string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
                {
                    DtPortOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate.Text));
                }
                if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false)
                {
                    DtPortInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate2.Text));
                }
                if (string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
                {
                    DtPortOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate2.Text));
                }
                objtblInvGenHead.Plant_InDate = DtPlantInDate;
                objtblInvGenHead.Plant_OutDate = DtPlantOutDate;
                objtblInvGenHead.Port_InDate = DtPortInDate;
                objtblInvGenHead.Port_OutDate = DtPortOutDate;
                objtblInvGenHead.Plant_InDate2 = DtPlantInDate2;
                objtblInvGenHead.Plant_OutDate2 = DtPlantOutDate2;
                objtblInvGenHead.Port_InDate2 = DtPortInDate2;
                objtblInvGenHead.Port_OutDate2 = DtPortOutDate2;

                objtblInvGenHead.Date_Added = Convert.ToDateTime(DateTime.Now);

                if (ddlGrType.SelectedValue == "GR")
                {
                    objtblInvGenHead.Gr_Type = "GR";
                }
                else
                {
                    objtblInvGenHead.Gr_Type = "GRR";
                }

                if (Convert.ToBoolean(hidAdminApp.Value) == true)
                    objtblInvGenHead.Admin_Approval = Convert.ToBoolean(chkApproved.Checked);
                else
                    objtblInvGenHead.Admin_Approval = Convert.ToBoolean(1);

                objtblInvGenHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                objtblInvGenHead.User_AddedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                objtblInvGenHead.Delivery_Add = txtDelvAddress.Text.Trim();
                
                #endregion
                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                    HiddenField hidItemIdno = (HiddenField)row.FindControl("hidItemIdno");
                    HiddenField hidUnitIdno = (HiddenField)row.FindControl("hidUnitIdno");
                    HiddenField hidDeliveryPlace = (HiddenField)row.FindControl("hidDeliveryPlace");

                    Label lblItemRate = (Label)row.FindControl("lblItemRate");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblNetAmnt = (Label)row.FindControl("lblNetAmnt");
                    Label lblServTaxAmnt = (Label)row.FindControl("lblServTaxAmnt");
                    TextBox txtOtherAmnt = (TextBox)row.FindControl("txtOtherAmnt");
                    TextBox txtWagesAmnt = (TextBox)row.FindControl("txtWagesAmnt");
                    HiddenField hidServTaxPerc = (HiddenField)row.FindControl("hidServTaxPerc");
                    HiddenField hidServTaxValid = (HiddenField)row.FindControl("hidServTaxValid");
                    Label lblSwchBrtTaxAmnt = (Label)row.FindControl("lblSwchBrtTaxAmnt");
                    Label lblKisanTaxAmnt = (Label)row.FindControl("lblKisanTaxAmnt");
                    HiddenField lblGrTypeId = (HiddenField)row.FindControl("hidGrtype");
                    //#GST
                    Label lblSGSTAmnt = (Label)row.FindControl("lblSGSTAmnt");
                    Label lblCGSTAmnt = (Label)row.FindControl("lblCGSTAmnt");
                    Label lblIGSTAmnt = (Label)row.FindControl("lblIGSTAmnt");
                    //  "Gr_Idno", "String", "Item_Idno", "String", "Item_Rate", "String", "Amount", "String", "Unit_Idno", "String", "Wayges", "String", "Net_Amnt", "String",
                    //                                              "Other_Amnt", "String", "ServTax_Amnt"
                    if ((Convert.ToDouble(lblItemRate.Text) <= 0) && Convert.ToDouble(lblGrTypeId.Value) == 1)
                    {
                        ShowMessageErr("Rate Should be grater than Zero Please Define Item Rate in Rate Master");
                        txtDate.Focus();
                        return;
                    }
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemIdno.Value, lblItemRate.Text, lblAmount.Text, hidUnitIdno.Value,
                                                      txtWagesAmnt.Text, lblNetAmnt.Text, txtOtherAmnt.Text, lblServTaxAmnt.Text, hidServTaxPerc.Value, 
                                                      hidServTaxValid.Value, lblSwchBrtTaxAmnt.Text, lblKisanTaxAmnt.Text, lblSGSTAmnt.Text, 
                                                      lblCGSTAmnt.Text, lblIGSTAmnt.Text,hidDeliveryPlace.Value);
                }

                #region For Annexure...

                DataView dv = DtTemp.DefaultView;
                dv.Sort = "Del_Place,Gr_Idno asc";
                sortedDT = dv.ToTable();
                if ((sortedDT != null) && (sortedDT.Rows.Count > 0))
                {
                    childTemp = CreateDt();
                    string OldPlace = "";
                    Int32 AnnNo = 0, iRowCount = 0;
                    foreach (DataRow row in sortedDT.Rows)
                    {
                        if(string.IsNullOrEmpty(OldPlace.ToString()))
                        {
                            OldPlace = Convert.ToString(row["Del_Place"]);
                            AnnNo = AnnNo + 1;
                        }

                        if (OldPlace == Convert.ToString(row["Del_Place"]))
                        {
                            iRowCount = iRowCount + 1;
                            if (iRowCount == 14)
                            {
                                iRowCount = 0;
                                AnnNo = AnnNo + 1;
                            }
                        }
                        else
                        {
                            iRowCount = 0;
                            OldPlace = Convert.ToString(row["Del_Place"]);
                            AnnNo = AnnNo + 1;
                        }
                        
                        ApplicationFunction.DatatableAddRow(childTemp, Convert.ToString(row["Gr_Idno"]), Convert.ToString(row["Item_Idno"]), Convert.ToString(row["Item_Rate"]), Convert.ToString(row["Amount"]), Convert.ToString(row["Unit_Idno"]), Convert.ToString(row["Wayges"]), Convert.ToString(row["Net_Amnt"]), Convert.ToString(row["Other_Amnt"]),
                            Convert.ToString(row["ServTax_Amnt"]), Convert.ToString(row["ServTax_Perc"]), Convert.ToString(row["ServTax_Valid"]), Convert.ToString(row["SwchBrtTax_Amnt"]), Convert.ToString(row["KisanKalyan_Amnt"]), Convert.ToString(row["SGST_Amt"]), Convert.ToString(row["CGST_Amt"]), Convert.ToString(row["IGST_Amt"]), Convert.ToString(row["Del_Place"]),
                            Convert.ToString(AnnNo));
                    }
                }
                #endregion
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        value = obj.Insert(objtblInvGenHead, childTemp, ddlGrType.SelectedValue);
                    }
                    else
                    {
                        objtblInvGenHead.User_ModifiedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                        lnkbtnNew.Visible = false; imgPrint.Visible = false;
                        value = obj.Update(objtblInvGenHead, Convert.ToInt32(hidid.Value), childTemp, ddlGrType.SelectedValue);
                    }

                    if (value > 0)
                    {
                        double SGSTAmnt = 0.0,CGSTAmnt=0.0,IGSTAmnt=0.0;


                            SGSTAmnt = Convert.ToDouble(txtT_SGST.Text);

                            CGSTAmnt = Convert.ToDouble(txtT_CGST.Text);


                            IGSTAmnt = Convert.ToDouble(txtT_IGST.Text);

                        double AddFreight = Convert.ToDouble(string.IsNullOrEmpty(txtAddFreight.Text) ? "0" : txtAddFreight.Text);
                        double HQCharges = Convert.ToDouble(string.IsNullOrEmpty(txtHQCharges.Text) ? "0" : txtHQCharges.Text);
                        double PlantAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount.Text) ? "0" : txtPlantAmount.Text);
                        double PlantAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount2.Text) ? "0" : txtPlantAmount2.Text);
                        double PortAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount.Text) ? "0" : txtPortAmount.Text);
                        double PortAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount2.Text) ? "0" : txtPortAmount2.Text);
                        double BiltyCharges = Convert.ToDouble(string.IsNullOrEmpty(txtBiltyCharges.Text) ? "0" : txtBiltyCharges.Text);
                        double OtherAmount = Convert.ToDouble(string.IsNullOrEmpty(txtOtherAmount.Text) ? "0" : txtOtherAmount.Text);
                        double OtherCharge1 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount1.Text) ? "0" : txtOtherChargesAmount1.Text);
                        double OtherCharge2 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount2.Text) ? "0" : txtOtherChargesAmount2.Text);

                        double ExtraAmnt = AddFreight + HQCharges + PlantAmount + PlantAmount2 + PortAmount + PortAmount2 + BiltyCharges + OtherAmount+ OtherCharge1+ OtherCharge2;
                        //Comment By salman 
                        //if (this.PostIntoAccounts(value, "IB", Convert.ToString(txtinvoicNo.Text.Trim()), Convert.ToString(txtDate.Text.Trim()), (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", ""))), 0, 0, 0, 0, Convert.ToDouble((string.IsNullOrEmpty(txtOtherAmount.Text.Trim()) ? 0.00 : Convert.ToDouble(txtOtherAmount.Text.Trim().Replace(",", "")))), Convert.ToDouble((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrServTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrServTax.Text.Trim().Replace(",", ""))), Convert.ToDouble((string.IsNullOrEmpty(txtGrosstotal.Text.Trim()) ? 0.00 : Convert.ToDouble(txtGrosstotal.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtShortageAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtShortageAmnt.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrSwchBrtTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrSwchBrtTax.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(txtKisanTaxTrnptr.Text.Trim()) ? 0.00 : Convert.ToDouble(txtKisanTaxTrnptr.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(ddlSenderName.SelectedValue) ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue)), (string.IsNullOrEmpty(ddldateRange.SelectedValue) ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                        if (this.PostIntoAccounts(value, "IB", Convert.ToString(txtinvoicNo.Text.Trim()), Convert.ToString(txtDate.Text.Trim()), (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", ""))), 0, 0, 0, 0, ExtraAmnt, Convert.ToDouble((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrServTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrServTax.Text.Trim().Replace(",", ""))), Convert.ToDouble((string.IsNullOrEmpty(txtGrosstotal.Text.Trim()) ? 0.00 : Convert.ToDouble(txtGrosstotal.Text.Trim().Replace(",", ""))) ), (string.IsNullOrEmpty(txtTrSwchBrtTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrSwchBrtTax.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(txtKisanTaxTrnptr.Text.Trim()) ? 0.00 : Convert.ToDouble(txtKisanTaxTrnptr.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(ddlSenderName.SelectedValue) ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue)), (string.IsNullOrEmpty(ddldateRange.SelectedValue) ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                        {
                            obj.UpdateIsPosting(value);
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                {

                                    ShowMessage("Record Update successfully"); Clear();
                                     tScope.Complete();
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Invoice No Already Exist");
                                    tScope.Dispose();
                                }
                                else
                                {
                                    ShowMessageErr("Record  Not Update");
                                      tScope.Dispose();
                                }
                            }
                            else
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                {
                                    ShowMessage("Record  saved Successfully ");
                                    Clear();
                                      tScope.Complete();
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Invoice No Already Exist");
                                      tScope.Dispose();
                                }
                                else
                                {
                                    ShowMessageErr("Record Not  saved Successfully ");
                                    tScope.Dispose();
                                }
                            }

                        }
                        else
                        {

                            if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(hidid.Value)) == false)
                                {
                                    hidpostingmsg.Value = "Record(s) not updated.";
                                }
                                else
                                {
                                    hidpostingmsg.Value = "Record(s) not saved.";
                                }
                                  tScope.Dispose();
                            }
                            tScope.Dispose();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                            return;
                        }
                    }
                }




            }
            else
            {
                ShowMessageErr("please Enter Details");
            }

        }
        protected void lnkbtnSaveMulti_OnClick(object sender, EventArgs e)
        {
            if (grdMain.Rows.Count > 0)
            {
                DtTemp = (DataTable)ViewState["dt"];
                DataTable dtt = DtTemp;
                double NetAmnt = 0, TollAmnt = 0, UnloadingAmnt = 0, nSGSTAmnt = 0, nCGSTAmnt = 0, nIGSTAmnt = 0, nSGSTPer = 0, nCGSTPer = 0, nIGSTPer = 0;
                int GSTType = 0;
                NetAmnt = string.IsNullOrEmpty(Convert.ToString(DtTemp.Compute("Sum(Amount)", ""))) ? 0 : Convert.ToDouble(DtTemp.Compute("Sum(Amount)", ""));
                UnloadingAmnt = string.IsNullOrEmpty(Convert.ToString(DtTemp.Compute("Sum(Wages_Amnt)", ""))) ? 0 : Convert.ToDouble(DtTemp.Compute("Sum(Wages_Amnt)", ""));
                TollAmnt = string.IsNullOrEmpty(Convert.ToString(DtTemp.Compute("Sum(Toll_Amnt)", ""))) ? 0 : Convert.ToDouble(DtTemp.Compute("Sum(Toll_Amnt)", ""));
                if (NetAmnt > 0)
                {
                    InvoiceDAL obj = new InvoiceDAL();
                    #region Parent Entry...
                    tblInvGenHead objtblInvGenHead = new tblInvGenHead();
                    Int32 InvNO = 0;
                    if (txtinvoicNo.Text != "")
                    {
                        InvNO = Convert.ToInt32(txtinvoicNo.Text);
                    }
                    objtblInvGenHead.Inv_No = InvNO;
                    objtblInvGenHead.Inv_prefix = txtInvPreIx.Text;
                    objtblInvGenHead.Inv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                    objtblInvGenHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
                    objtblInvGenHead.Sendr_Idno = Convert.ToInt32((ddlSenderName.SelectedIndex <= 0) ? "0" : ddlSenderName.SelectedValue);
                    objtblInvGenHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);
                    objtblInvGenHead.GrossTot_Amnt = Convert.ToDouble(NetAmnt);
                    objtblInvGenHead.TrServTax_Amnt = Convert.ToDouble(txtTrServTax.Text);
                    objtblInvGenHead.ConsignrServTax = Convert.ToDouble(txtCSServTax.Text);

                    objtblInvGenHead.TrSwchBrtTax_Amnt = Convert.ToDouble(txtTrSwchBrtTax.Text);
                    objtblInvGenHead.ConsignrSwchBrtTax = Convert.ToDouble(txtCSSwchBrtTax.Text);

                    objtblInvGenHead.TrKisanKalyanTax_Amnt = Convert.ToDouble(txtKisanTaxTrnptr.Text);
                    objtblInvGenHead.ConsignrKisanTax_Amnt = Convert.ToDouble(txtKisanTax.Text);

                    objtblInvGenHead.Short_Amnt = Convert.ToDouble(txtShortageAmnt.Text);
                    objtblInvGenHead.Bilty_Chrgs = Convert.ToDouble(txtBiltyCharges.Text);
                    objtblInvGenHead.Net_Amnt = Convert.ToDouble(NetAmnt.ToString("N2"));
                    double RndAmnt = NetAmnt - Math.Round(NetAmnt);
                    objtblInvGenHead.RoundOff_Amnt = Convert.ToDouble(RndAmnt);
                    objtblInvGenHead.TBB_Rate = Convert.ToBoolean(HidTbbRate.Value);
                    objtblInvGenHead.Print_Format = Convert.ToInt64(ddlPrintLoc.Text);
                    objtblInvGenHead.PlantAmount = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount.Text.Trim());
                    objtblInvGenHead.PortAmount = string.IsNullOrEmpty(Convert.ToString(txtPortAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount.Text.Trim());
                    objtblInvGenHead.PlantDays = string.IsNullOrEmpty(Convert.ToString(txtPlantDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays.Text.Trim());
                    objtblInvGenHead.PortDays = string.IsNullOrEmpty(Convert.ToString(txtPortDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays.Text.Trim());

                    //PlantPort Details - 2
                    objtblInvGenHead.PlantAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount2.Text.Trim());
                    objtblInvGenHead.PortAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPortAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount2.Text.Trim());
                    objtblInvGenHead.PlantDays2 = string.IsNullOrEmpty(Convert.ToString(txtPlantDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays2.Text.Trim());
                    objtblInvGenHead.PortDays2 = string.IsNullOrEmpty(Convert.ToString(txtPortDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays2.Text.Trim());

                    objtblInvGenHead.Charges1_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge1.Text)) ? "" : Convert.ToString(txtOtherCharge1.Text);
                    objtblInvGenHead.Charges2_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge2.Text)) ? "" : Convert.ToString(txtOtherCharge2.Text);

                    objtblInvGenHead.Charges1_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount1.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount1.Text);
                    objtblInvGenHead.Charges2_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount2.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount2.Text);

                    objtblInvGenHead.AddCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtAddFreight.Text)) ? 0.00 : Convert.ToDouble(txtAddFreight.Text);
                    objtblInvGenHead.HQCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtHQCharges.Text)) ? 0.00 : Convert.ToDouble(txtHQCharges.Text);

                    //#GST 
                    objtblInvGenHead.TrSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_SGST.Text)) ? 0.00 : Convert.ToDouble(txtT_SGST.Text);
                    objtblInvGenHead.TrCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_CGST.Text)) ? 0.00 : Convert.ToDouble(txtT_CGST.Text);
                    objtblInvGenHead.TrIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_IGST.Text)) ? 0.00 : Convert.ToDouble(txtT_IGST.Text);
                    objtblInvGenHead.ConSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_SGST.Text)) ? 0.00 : Convert.ToDouble(txtC_SGST.Text);
                    objtblInvGenHead.ConCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_CGST.Text)) ? 0.00 : Convert.ToDouble(txtC_CGST.Text);
                    objtblInvGenHead.ConIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_IGST.Text)) ? 0.00 : Convert.ToDouble(txtC_IGST.Text);

                    objtblInvGenHead.ConGSTCess_Amt = 0;
                    objtblInvGenHead.TrGSTCess_Amt = 0;
                    objtblInvGenHead.ShtgGST_Amt = Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text);
                    DateTime? DtPlantInDate = null;
                    DateTime? DtPlantOutDate = null;
                    DateTime? DtPlantInDate2 = null;
                    DateTime? DtPlantOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false)
                    {
                        DtPlantInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
                    {
                        DtPlantOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false)
                    {
                        DtPlantInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
                    {
                        DtPlantOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate2.Text));
                    }
                    DateTime? DtPortInDate = null;
                    DateTime? DtPortOutDate = null;
                    DateTime? DtPortInDate2 = null;
                    DateTime? DtPortOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false)
                    {
                        DtPortInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
                    {
                        DtPortOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false)
                    {
                        DtPortInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
                    {
                        DtPortOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate2.Text));
                    }
                    objtblInvGenHead.Plant_InDate = DtPlantInDate;
                    objtblInvGenHead.Plant_OutDate = DtPlantOutDate;
                    objtblInvGenHead.Port_InDate = DtPortInDate;
                    objtblInvGenHead.Port_OutDate = DtPortOutDate;
                    objtblInvGenHead.Plant_InDate2 = DtPlantInDate2;
                    objtblInvGenHead.Plant_OutDate2 = DtPlantOutDate2;
                    objtblInvGenHead.Port_InDate2 = DtPortInDate2;
                    objtblInvGenHead.Port_OutDate2 = DtPortOutDate2;

                    objtblInvGenHead.Date_Added = Convert.ToDateTime(DateTime.Now);

                    if (ddlGrType.SelectedValue == "GR")
                    {
                        objtblInvGenHead.Gr_Type = "GR";
                    }
                    else
                    {
                        objtblInvGenHead.Gr_Type = "GRR";
                    }

                    if (Convert.ToBoolean(hidAdminApp.Value) == true)
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(chkApproved.Checked);
                    else
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(1);

                    objtblInvGenHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                    objtblInvGenHead.User_AddedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                    objtblInvGenHead.Delivery_Add = txtDelvAddress.Text.Trim();
                    #endregion
                    #region Child Entry
                    DtTemp = CreateDt();
                    int a = 0;
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                        HiddenField hidItemIdno = (HiddenField)row.FindControl("hidItemIdno");
                        HiddenField hidUnitIdno = (HiddenField)row.FindControl("hidUnitIdno");
                        HiddenField hidDeliveryPlace = (HiddenField)row.FindControl("hidDeliveryPlace");

                        Label lblItemRate = (Label)row.FindControl("lblItemRate");
                        Label lblAmount = (Label)row.FindControl("lblAmount");
                        //Label lblNetAmnt = (Label)row.FindControl("lblNetAmnt");
                        Label lblServTaxAmnt = (Label)row.FindControl("lblServTaxAmnt");
                        TextBox txtOtherAmnt = (TextBox)row.FindControl("txtOtherAmnt");
                        //TextBox txtWagesAmnt = (TextBox)row.FindControl("txtWagesAmnt");
                        HiddenField hidServTaxPerc = (HiddenField)row.FindControl("hidServTaxPerc");
                        HiddenField hidServTaxValid = (HiddenField)row.FindControl("hidServTaxValid");
                        Label lblSwchBrtTaxAmnt = (Label)row.FindControl("lblSwchBrtTaxAmnt");
                        Label lblKisanTaxAmnt = (Label)row.FindControl("lblKisanTaxAmnt");
                        HiddenField lblGrTypeId = (HiddenField)row.FindControl("hidGrtype");
                        //#GST
                        Label lblSGSTAmnt = (Label)row.FindControl("lblSGSTAmnt");
                        Label lblCGSTAmnt = (Label)row.FindControl("lblCGSTAmnt");
                        Label lblIGSTAmnt = (Label)row.FindControl("lblIGSTAmnt");
                        nSGSTPer = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[a]["SGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[a]["SGST_Per"]);
                        nCGSTPer = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[a]["CGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[a]["CGST_Per"]);
                        nIGSTPer = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[a]["IGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[a]["IGST_Per"]);
                        GSTType = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[a]["GST_Idno"])) ? 0 : Convert.ToInt16(dtt.Rows[a]["GST_Idno"]);
                        if (GSTType == 1)
                        {
                            nSGSTAmnt = ((string.IsNullOrEmpty(lblAmount.Text) ? 0 : Convert.ToDouble(lblAmount.Text)) * nSGSTPer) / 100;
                            nCGSTAmnt = ((string.IsNullOrEmpty(lblAmount.Text) ? 0 : Convert.ToDouble(lblAmount.Text)) * nCGSTPer) / 100;
                            nIGSTAmnt = 0;
                        }
                        else if (GSTType == 2)
                        {
                            nSGSTAmnt = 0;
                            nCGSTAmnt = 0;
                            nIGSTAmnt = ((string.IsNullOrEmpty(lblAmount.Text) ? 0 : Convert.ToDouble(lblAmount.Text)) * nIGSTPer) / 100;
                        }
                        else
                            if ((Convert.ToDouble(lblItemRate.Text) <= 0) && Convert.ToDouble(lblGrTypeId.Value) == 1)
                            {
                                ShowMessageErr("Rate Should be grater than Zero Please Define Item Rate in Rate Master");
                                txtDate.Focus();
                                return;
                            }
                        ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemIdno.Value, lblItemRate.Text, lblAmount.Text, hidUnitIdno.Value,
                                                          "0", lblAmount.Text, txtOtherAmnt.Text, lblServTaxAmnt.Text, hidServTaxPerc.Value,
                                                          hidServTaxValid.Value, lblSwchBrtTaxAmnt.Text, lblKisanTaxAmnt.Text, nSGSTAmnt.ToString("N2"),
                                                          nCGSTAmnt.ToString("N2"), nIGSTAmnt.ToString("N2"), hidDeliveryPlace.Value);
                        a++;
                    }
                    #endregion
                    #region For Annexure...
                    DataView dv = DtTemp.DefaultView;
                    dv.Sort = "Del_Place,Gr_Idno asc";
                    sortedDT = dv.ToTable();
                    if ((sortedDT != null) && (sortedDT.Rows.Count > 0))
                    {
                        childTemp = CreateDt();
                        string OldPlace = "";
                        Int32 AnnNo = 0, iRowCount = 0;
                        foreach (DataRow row in sortedDT.Rows)
                        {
                            if (string.IsNullOrEmpty(OldPlace.ToString()))
                            {
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            if (OldPlace == Convert.ToString(row["Del_Place"]))
                            {
                                iRowCount = iRowCount + 1;
                                if (iRowCount == 14)
                                {
                                    iRowCount = 0;
                                    AnnNo = AnnNo + 1;
                                }
                            }
                            else
                            {
                                iRowCount = 0;
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            ApplicationFunction.DatatableAddRow(childTemp, Convert.ToString(row["Gr_Idno"]), Convert.ToString(row["Item_Idno"]), Convert.ToString(row["Item_Rate"]), Convert.ToString(row["Amount"]), Convert.ToString(row["Unit_Idno"]), Convert.ToString(row["Wayges"]), Convert.ToString(row["Net_Amnt"]), Convert.ToString(row["Other_Amnt"]),
                                Convert.ToString(row["ServTax_Amnt"]), Convert.ToString(row["ServTax_Perc"]), Convert.ToString(row["ServTax_Valid"]), Convert.ToString(row["SwchBrtTax_Amnt"]), Convert.ToString(row["KisanKalyan_Amnt"]), Convert.ToString(row["SGST_Amt"]), Convert.ToString(row["CGST_Amt"]), Convert.ToString(row["IGST_Amt"]), Convert.ToString(row["Del_Place"]),
                                Convert.ToString(AnnNo));
                        }
                    }
                    #endregion
                    #region Posting..
                    Int64 value = 0;
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == true)
                        {
                            value = obj.Insert(objtblInvGenHead, childTemp, ddlGrType.SelectedValue);
                        }
                        else
                        {
                            objtblInvGenHead.User_ModifiedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                            lnkbtnNew.Visible = false; imgPrint.Visible = false;
                            value = obj.Update(objtblInvGenHead, Convert.ToInt32(hidid.Value), childTemp, ddlGrType.SelectedValue);
                        }

                        if (value > 0)
                        {
                            double SGSTAmnt = 0.0, CGSTAmnt = 0.0, IGSTAmnt = 0.0;


                            SGSTAmnt = Convert.ToDouble(nSGSTAmnt);

                            CGSTAmnt = Convert.ToDouble(nCGSTAmnt);


                            IGSTAmnt = Convert.ToDouble(nIGSTAmnt);

                            double AddFreight = Convert.ToDouble(string.IsNullOrEmpty(txtAddFreight.Text) ? "0" : txtAddFreight.Text);
                            double HQCharges = Convert.ToDouble(string.IsNullOrEmpty(txtHQCharges.Text) ? "0" : txtHQCharges.Text);
                            double PlantAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount.Text) ? "0" : txtPlantAmount.Text);
                            double PlantAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount2.Text) ? "0" : txtPlantAmount2.Text);
                            double PortAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount.Text) ? "0" : txtPortAmount.Text);
                            double PortAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount2.Text) ? "0" : txtPortAmount2.Text);
                            double BiltyCharges = Convert.ToDouble(string.IsNullOrEmpty(txtBiltyCharges.Text) ? "0" : txtBiltyCharges.Text);
                            double OtherAmount = Convert.ToDouble(string.IsNullOrEmpty(txtOtherAmount.Text) ? "0" : txtOtherAmount.Text);
                            double OtherCharge1 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount1.Text) ? "0" : txtOtherChargesAmount1.Text);
                            double OtherCharge2 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount2.Text) ? "0" : txtOtherChargesAmount2.Text);

                            double ExtraAmnt = AddFreight + HQCharges + PlantAmount + PlantAmount2 + PortAmount + PortAmount2 + BiltyCharges + OtherAmount + OtherCharge1 + OtherCharge2;
                            if (this.PostIntoAccounts(value, "IB", Convert.ToString(txtinvoicNo.Text.Trim()), Convert.ToString(txtDate.Text.Trim()), (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", ""))), 0, 0, 0, 0, ExtraAmnt, Convert.ToDouble((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrServTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrServTax.Text.Trim().Replace(",", ""))), Convert.ToDouble((string.IsNullOrEmpty(txtGrosstotal.Text.Trim()) ? 0.00 : Convert.ToDouble(txtGrosstotal.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrSwchBrtTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrSwchBrtTax.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(txtKisanTaxTrnptr.Text.Trim()) ? 0.00 : Convert.ToDouble(txtKisanTaxTrnptr.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(ddlSenderName.SelectedValue) ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue)), (string.IsNullOrEmpty(ddldateRange.SelectedValue) ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                            //if(1==1)
                            {
                                //obj.UpdateIsPosting(value);
                                if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                    {

                                        ShowMessage("Record Update successfully"); 
                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record  Not Update");
                                        tScope.Dispose();
                                    }
                                }
                                else
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                    {
                                        ShowMessage("Record  saved Successfully ");
                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record Not  saved Successfully ");
                                        tScope.Dispose();
                                    }
                                }

                            }
                            else
                            {

                                if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                                {
                                    if (string.IsNullOrEmpty(Convert.ToString(hidid.Value)) == false)
                                    {
                                        hidpostingmsg.Value = "Record(s) not updated.";
                                    }
                                    else
                                    {
                                        hidpostingmsg.Value = "Record(s) not saved.";
                                    }
                                    tScope.Dispose();
                                }
                                tScope.Dispose();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                if (UnloadingAmnt > 0)
                {
                    InvoiceDAL obj = new InvoiceDAL();
                    #region Parent Entry...
                    tblInvGenHead objtblInvGenHead = new tblInvGenHead();
                    Int32 InvNO = 0;
                    if (txtinvoicNo.Text != "")
                    {
                        InvNO = Convert.ToInt32(txtinvoicNo.Text) + 1;
                    }
                    objtblInvGenHead.Inv_No = InvNO;
                    objtblInvGenHead.Inv_prefix = txtInvPreIx.Text;
                    objtblInvGenHead.Inv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                    objtblInvGenHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
                    objtblInvGenHead.Sendr_Idno = Convert.ToInt32((ddlSenderName.SelectedIndex <= 0) ? "0" : ddlSenderName.SelectedValue);
                    objtblInvGenHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);
                    objtblInvGenHead.GrossTot_Amnt = Convert.ToDouble(UnloadingAmnt);
                    objtblInvGenHead.TrServTax_Amnt = Convert.ToDouble(txtTrServTax.Text);
                    objtblInvGenHead.ConsignrServTax = Convert.ToDouble(txtCSServTax.Text);

                    objtblInvGenHead.TrSwchBrtTax_Amnt = Convert.ToDouble(txtTrSwchBrtTax.Text);
                    objtblInvGenHead.ConsignrSwchBrtTax = Convert.ToDouble(txtCSSwchBrtTax.Text);

                    objtblInvGenHead.TrKisanKalyanTax_Amnt = Convert.ToDouble(txtKisanTaxTrnptr.Text);
                    objtblInvGenHead.ConsignrKisanTax_Amnt = Convert.ToDouble(txtKisanTax.Text);

                    objtblInvGenHead.Short_Amnt = Convert.ToDouble(txtShortageAmnt.Text);
                    objtblInvGenHead.Bilty_Chrgs = Convert.ToDouble(txtBiltyCharges.Text);
                    objtblInvGenHead.Net_Amnt = Convert.ToDouble(UnloadingAmnt.ToString("N2"));
                    double RndAmnt = UnloadingAmnt - Math.Round(UnloadingAmnt);
                    objtblInvGenHead.RoundOff_Amnt = Convert.ToDouble(RndAmnt);
                    objtblInvGenHead.TBB_Rate = Convert.ToBoolean(HidTbbRate.Value);
                    objtblInvGenHead.Print_Format = Convert.ToInt64(ddlPrintLoc.Text);
                    objtblInvGenHead.PlantAmount = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount.Text.Trim());
                    objtblInvGenHead.PortAmount = string.IsNullOrEmpty(Convert.ToString(txtPortAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount.Text.Trim());
                    objtblInvGenHead.PlantDays = string.IsNullOrEmpty(Convert.ToString(txtPlantDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays.Text.Trim());
                    objtblInvGenHead.PortDays = string.IsNullOrEmpty(Convert.ToString(txtPortDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays.Text.Trim());

                    //PlantPort Details - 2
                    objtblInvGenHead.PlantAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount2.Text.Trim());
                    objtblInvGenHead.PortAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPortAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount2.Text.Trim());
                    objtblInvGenHead.PlantDays2 = string.IsNullOrEmpty(Convert.ToString(txtPlantDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays2.Text.Trim());
                    objtblInvGenHead.PortDays2 = string.IsNullOrEmpty(Convert.ToString(txtPortDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays2.Text.Trim());

                    objtblInvGenHead.Charges1_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge1.Text)) ? "" : Convert.ToString(txtOtherCharge1.Text);
                    objtblInvGenHead.Charges2_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge2.Text)) ? "" : Convert.ToString(txtOtherCharge2.Text);

                    objtblInvGenHead.Charges1_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount1.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount1.Text);
                    objtblInvGenHead.Charges2_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount2.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount2.Text);

                    objtblInvGenHead.AddCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtAddFreight.Text)) ? 0.00 : Convert.ToDouble(txtAddFreight.Text);
                    objtblInvGenHead.HQCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtHQCharges.Text)) ? 0.00 : Convert.ToDouble(txtHQCharges.Text);

                    //#GST 
                    objtblInvGenHead.TrSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_SGST.Text)) ? 0.00 : Convert.ToDouble(txtT_SGST.Text);
                    objtblInvGenHead.TrCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_CGST.Text)) ? 0.00 : Convert.ToDouble(txtT_CGST.Text);
                    objtblInvGenHead.TrIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_IGST.Text)) ? 0.00 : Convert.ToDouble(txtT_IGST.Text);
                    objtblInvGenHead.ConSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_SGST.Text)) ? 0.00 : Convert.ToDouble(txtC_SGST.Text);
                    objtblInvGenHead.ConCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_CGST.Text)) ? 0.00 : Convert.ToDouble(txtC_CGST.Text);
                    objtblInvGenHead.ConIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_IGST.Text)) ? 0.00 : Convert.ToDouble(txtC_IGST.Text);

                    objtblInvGenHead.ConGSTCess_Amt = 0;
                    objtblInvGenHead.TrGSTCess_Amt = 0;
                    objtblInvGenHead.ShtgGST_Amt = Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text);
                    DateTime? DtPlantInDate = null;
                    DateTime? DtPlantOutDate = null;
                    DateTime? DtPlantInDate2 = null;
                    DateTime? DtPlantOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false)
                    {
                        DtPlantInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
                    {
                        DtPlantOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false)
                    {
                        DtPlantInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
                    {
                        DtPlantOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate2.Text));
                    }
                    DateTime? DtPortInDate = null;
                    DateTime? DtPortOutDate = null;
                    DateTime? DtPortInDate2 = null;
                    DateTime? DtPortOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false)
                    {
                        DtPortInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
                    {
                        DtPortOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false)
                    {
                        DtPortInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
                    {
                        DtPortOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate2.Text));
                    }
                    objtblInvGenHead.Plant_InDate = DtPlantInDate;
                    objtblInvGenHead.Plant_OutDate = DtPlantOutDate;
                    objtblInvGenHead.Port_InDate = DtPortInDate;
                    objtblInvGenHead.Port_OutDate = DtPortOutDate;
                    objtblInvGenHead.Plant_InDate2 = DtPlantInDate2;
                    objtblInvGenHead.Plant_OutDate2 = DtPlantOutDate2;
                    objtblInvGenHead.Port_InDate2 = DtPortInDate2;
                    objtblInvGenHead.Port_OutDate2 = DtPortOutDate2;

                    objtblInvGenHead.Date_Added = Convert.ToDateTime(DateTime.Now);

                    if (ddlGrType.SelectedValue == "GR")
                    {
                        objtblInvGenHead.Gr_Type = "GR";
                    }
                    else
                    {
                        objtblInvGenHead.Gr_Type = "GRR";
                    }

                    if (Convert.ToBoolean(hidAdminApp.Value) == true)
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(chkApproved.Checked);
                    else
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(1);

                    objtblInvGenHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                    objtblInvGenHead.User_AddedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                    objtblInvGenHead.Delivery_Add = txtDelvAddress.Text.Trim();
                    #endregion
                    #region Child Entry
                    DtTemp = CreateDt();
                    int b = 0;
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                        HiddenField hidItemIdno = (HiddenField)row.FindControl("hidItemIdno");
                        HiddenField hidUnitIdno = (HiddenField)row.FindControl("hidUnitIdno");
                        HiddenField hidDeliveryPlace = (HiddenField)row.FindControl("hidDeliveryPlace");

                        Label lblItemRate = (Label)row.FindControl("lblItemRate");
                        Label lblAmount = (Label)row.FindControl("lblAmount");
                        //Label lblNetAmnt = (Label)row.FindControl("lblNetAmnt");
                        Label lblServTaxAmnt = (Label)row.FindControl("lblServTaxAmnt");
                        TextBox txtOtherAmnt = (TextBox)row.FindControl("txtOtherAmnt");
                        TextBox txtWagesAmnt = (TextBox)row.FindControl("txtWagesAmnt");
                        HiddenField hidServTaxPerc = (HiddenField)row.FindControl("hidServTaxPerc");
                        HiddenField hidServTaxValid = (HiddenField)row.FindControl("hidServTaxValid");
                        Label lblSwchBrtTaxAmnt = (Label)row.FindControl("lblSwchBrtTaxAmnt");
                        Label lblKisanTaxAmnt = (Label)row.FindControl("lblKisanTaxAmnt");
                        HiddenField lblGrTypeId = (HiddenField)row.FindControl("hidGrtype");
                        //#GST
                        Label lblSGSTAmnt = (Label)row.FindControl("lblSGSTAmnt");
                        Label lblCGSTAmnt = (Label)row.FindControl("lblCGSTAmnt");
                        Label lblIGSTAmnt = (Label)row.FindControl("lblIGSTAmnt");
                        nSGSTPer = 9;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["SGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["SGST_Per"]);
                        nCGSTPer = 9;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["CGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["CGST_Per"]);
                        nIGSTPer = 18;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["IGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["IGST_Per"]);
                        GSTType = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["GST_Idno"])) ? 0 : Convert.ToInt16(dtt.Rows[b]["GST_Idno"]);
                        if (GSTType == 1)
                        {
                            nSGSTAmnt = ((string.IsNullOrEmpty(txtWagesAmnt.Text) ? 0 : Convert.ToDouble(txtWagesAmnt.Text)) * nSGSTPer) / 100;
                            nCGSTAmnt = ((string.IsNullOrEmpty(txtWagesAmnt.Text) ? 0 : Convert.ToDouble(txtWagesAmnt.Text)) * nCGSTPer) / 100;
                            nIGSTAmnt = 0;
                        }
                        else if (GSTType == 2)
                        {
                            nSGSTAmnt = 0;
                            nCGSTAmnt = 0;
                            nIGSTAmnt = ((string.IsNullOrEmpty(txtWagesAmnt.Text) ? 0 : Convert.ToDouble(txtWagesAmnt.Text)) * nIGSTPer) / 100;
                        }
                        if ((Convert.ToDouble(lblItemRate.Text) <= 0) && Convert.ToDouble(lblGrTypeId.Value) == 1)
                        {
                            ShowMessageErr("Rate Should be grater than Zero Please Define Item Rate in Rate Master");
                            txtDate.Focus();
                            return;
                        }
                        ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemIdno.Value, lblItemRate.Text, txtWagesAmnt.Text, hidUnitIdno.Value,
                                                          "0", txtWagesAmnt.Text, txtOtherAmnt.Text, lblServTaxAmnt.Text, hidServTaxPerc.Value,
                                                          hidServTaxValid.Value, lblSwchBrtTaxAmnt.Text, lblKisanTaxAmnt.Text, nSGSTAmnt.ToString("N2"),
                                                          nCGSTAmnt.ToString("N2"), nIGSTAmnt.ToString("N2"), hidDeliveryPlace.Value);
                        b++;
                    }
                    #endregion
                    #region For Annexure...
                    DataView dv = DtTemp.DefaultView;
                    dv.Sort = "Del_Place,Gr_Idno asc";
                    sortedDT = dv.ToTable();
                    if ((sortedDT != null) && (sortedDT.Rows.Count > 0))
                    {
                        childTemp = CreateDt();
                        string OldPlace = "";
                        Int32 AnnNo = 0, iRowCount = 0;
                        foreach (DataRow row in sortedDT.Rows)
                        {
                            if (string.IsNullOrEmpty(OldPlace.ToString()))
                            {
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            if (OldPlace == Convert.ToString(row["Del_Place"]))
                            {
                                iRowCount = iRowCount + 1;
                                if (iRowCount == 14)
                                {
                                    iRowCount = 0;
                                    AnnNo = AnnNo + 1;
                                }
                            }
                            else
                            {
                                iRowCount = 0;
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            ApplicationFunction.DatatableAddRow(childTemp, Convert.ToString(row["Gr_Idno"]), Convert.ToString(row["Item_Idno"]), Convert.ToString(row["Item_Rate"]), Convert.ToString(row["Amount"]), Convert.ToString(row["Unit_Idno"]), Convert.ToString(row["Wayges"]), Convert.ToString(row["Net_Amnt"]), Convert.ToString(row["Other_Amnt"]),
                                Convert.ToString(row["ServTax_Amnt"]), Convert.ToString(row["ServTax_Perc"]), Convert.ToString(row["ServTax_Valid"]), Convert.ToString(row["SwchBrtTax_Amnt"]), Convert.ToString(row["KisanKalyan_Amnt"]), Convert.ToString(row["SGST_Amt"]), Convert.ToString(row["CGST_Amt"]), Convert.ToString(row["IGST_Amt"]), Convert.ToString(row["Del_Place"]),
                                Convert.ToString(AnnNo));
                        }
                    }
                    #endregion
                    #region Posting..
                    Int64 value = 0;
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == true)
                        {
                            value = obj.Insert(objtblInvGenHead, childTemp, ddlGrType.SelectedValue);
                        }
                        else
                        {
                            objtblInvGenHead.User_ModifiedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                            lnkbtnNew.Visible = false; imgPrint.Visible = false;
                            value = obj.Update(objtblInvGenHead, Convert.ToInt32(hidid.Value), childTemp, ddlGrType.SelectedValue);
                        }

                        if (value > 0)
                        {
                            double SGSTAmnt = 0.0, CGSTAmnt = 0.0, IGSTAmnt = 0.0;


                            SGSTAmnt = Convert.ToDouble(txtT_SGST.Text);

                            CGSTAmnt = Convert.ToDouble(txtT_CGST.Text);


                            IGSTAmnt = Convert.ToDouble(txtT_IGST.Text);

                            double AddFreight = Convert.ToDouble(string.IsNullOrEmpty(txtAddFreight.Text) ? "0" : txtAddFreight.Text);
                            double HQCharges = Convert.ToDouble(string.IsNullOrEmpty(txtHQCharges.Text) ? "0" : txtHQCharges.Text);
                            double PlantAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount.Text) ? "0" : txtPlantAmount.Text);
                            double PlantAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount2.Text) ? "0" : txtPlantAmount2.Text);
                            double PortAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount.Text) ? "0" : txtPortAmount.Text);
                            double PortAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount2.Text) ? "0" : txtPortAmount2.Text);
                            double BiltyCharges = Convert.ToDouble(string.IsNullOrEmpty(txtBiltyCharges.Text) ? "0" : txtBiltyCharges.Text);
                            double OtherAmount = Convert.ToDouble(string.IsNullOrEmpty(txtOtherAmount.Text) ? "0" : txtOtherAmount.Text);
                            double OtherCharge1 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount1.Text) ? "0" : txtOtherChargesAmount1.Text);
                            double OtherCharge2 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount2.Text) ? "0" : txtOtherChargesAmount2.Text);

                            double ExtraAmnt = AddFreight + HQCharges + PlantAmount + PlantAmount2 + PortAmount + PortAmount2 + BiltyCharges + OtherAmount + OtherCharge1 + OtherCharge2;
                            if (this.PostIntoAccounts(value, "IB", Convert.ToString(txtinvoicNo.Text.Trim()), Convert.ToString(txtDate.Text.Trim()), (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", ""))), 0, 0, 0, 0, ExtraAmnt, Convert.ToDouble((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrServTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrServTax.Text.Trim().Replace(",", ""))), Convert.ToDouble((string.IsNullOrEmpty(txtGrosstotal.Text.Trim()) ? 0.00 : Convert.ToDouble(txtGrosstotal.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrSwchBrtTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrSwchBrtTax.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(txtKisanTaxTrnptr.Text.Trim()) ? 0.00 : Convert.ToDouble(txtKisanTaxTrnptr.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(ddlSenderName.SelectedValue) ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue)), (string.IsNullOrEmpty(ddldateRange.SelectedValue) ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                            //if (1 == 1)
                            {
                                obj.UpdateIsPosting(value);
                                if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                    {

                                        ShowMessage("Record Update successfully");
                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record  Not Update");
                                        tScope.Dispose();
                                    }
                                }
                                else
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                    {
                                        ShowMessage("Record  saved Successfully ");
                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record Not  saved Successfully ");
                                        tScope.Dispose();
                                    }
                                }

                            }
                            else
                            {

                                if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                                {
                                    if (string.IsNullOrEmpty(Convert.ToString(hidid.Value)) == false)
                                    {
                                        hidpostingmsg.Value = "Record(s) not updated.";
                                    }
                                    else
                                    {
                                        hidpostingmsg.Value = "Record(s) not saved.";
                                    }
                                    tScope.Dispose();
                                }
                                tScope.Dispose();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                if (TollAmnt > 0)
                {
                    InvoiceDAL obj = new InvoiceDAL();
                    #region Parent Entry...
                    tblInvGenHead objtblInvGenHead = new tblInvGenHead();
                    Int32 InvNO = 0;
                    if (txtinvoicNo.Text != "")
                    {
                        if (UnloadingAmnt > 0)
                        {
                            InvNO = Convert.ToInt32(txtinvoicNo.Text) + 2;
                        }
                        else
                        {
                            InvNO = Convert.ToInt32(txtinvoicNo.Text) + 1;
                        }
                    }
                    objtblInvGenHead.Inv_No = InvNO;
                    objtblInvGenHead.Inv_prefix = txtInvPreIx.Text;
                    objtblInvGenHead.Inv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                    objtblInvGenHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
                    objtblInvGenHead.Sendr_Idno = Convert.ToInt32((ddlSenderName.SelectedIndex <= 0) ? "0" : ddlSenderName.SelectedValue);
                    objtblInvGenHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);
                    objtblInvGenHead.GrossTot_Amnt = Convert.ToDouble(TollAmnt);
                    objtblInvGenHead.TrServTax_Amnt = Convert.ToDouble(txtTrServTax.Text);
                    objtblInvGenHead.ConsignrServTax = Convert.ToDouble(txtCSServTax.Text);

                    objtblInvGenHead.TrSwchBrtTax_Amnt = Convert.ToDouble(txtTrSwchBrtTax.Text);
                    objtblInvGenHead.ConsignrSwchBrtTax = Convert.ToDouble(txtCSSwchBrtTax.Text);

                    objtblInvGenHead.TrKisanKalyanTax_Amnt = Convert.ToDouble(txtKisanTaxTrnptr.Text);
                    objtblInvGenHead.ConsignrKisanTax_Amnt = Convert.ToDouble(txtKisanTax.Text);

                    objtblInvGenHead.Short_Amnt = Convert.ToDouble(txtShortageAmnt.Text);
                    objtblInvGenHead.Bilty_Chrgs = Convert.ToDouble(txtBiltyCharges.Text);
                    objtblInvGenHead.Net_Amnt = Convert.ToDouble(TollAmnt.ToString("N2"));
                    double RndAmnt = TollAmnt - Math.Round(TollAmnt);
                    objtblInvGenHead.RoundOff_Amnt = Convert.ToDouble(RndAmnt);
                    objtblInvGenHead.TBB_Rate = Convert.ToBoolean(HidTbbRate.Value);
                    objtblInvGenHead.Print_Format = Convert.ToInt64(ddlPrintLoc.Text);
                    objtblInvGenHead.PlantAmount = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount.Text.Trim());
                    objtblInvGenHead.PortAmount = string.IsNullOrEmpty(Convert.ToString(txtPortAmount.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount.Text.Trim());
                    objtblInvGenHead.PlantDays = string.IsNullOrEmpty(Convert.ToString(txtPlantDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays.Text.Trim());
                    objtblInvGenHead.PortDays = string.IsNullOrEmpty(Convert.ToString(txtPortDays.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays.Text.Trim());

                    //PlantPort Details - 2
                    objtblInvGenHead.PlantAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPlantAmount2.Text.Trim());
                    objtblInvGenHead.PortAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPortAmount2.Text.Trim())) ? Convert.ToDouble(0.0) : Convert.ToDouble(txtPortAmount2.Text.Trim());
                    objtblInvGenHead.PlantDays2 = string.IsNullOrEmpty(Convert.ToString(txtPlantDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPlantDays2.Text.Trim());
                    objtblInvGenHead.PortDays2 = string.IsNullOrEmpty(Convert.ToString(txtPortDays2.Text.Trim())) ? 0 : Convert.ToInt64(txtPortDays2.Text.Trim());

                    objtblInvGenHead.Charges1_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge1.Text)) ? "" : Convert.ToString(txtOtherCharge1.Text);
                    objtblInvGenHead.Charges2_Name = string.IsNullOrEmpty(Convert.ToString(txtOtherCharge2.Text)) ? "" : Convert.ToString(txtOtherCharge2.Text);

                    objtblInvGenHead.Charges1_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount1.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount1.Text);
                    objtblInvGenHead.Charges2_Amnt = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount2.Text)) ? 0.00 : Convert.ToDouble(txtOtherChargesAmount2.Text);

                    objtblInvGenHead.AddCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtAddFreight.Text)) ? 0.00 : Convert.ToDouble(txtAddFreight.Text);
                    objtblInvGenHead.HQCharges_Amnt = string.IsNullOrEmpty(Convert.ToString(txtHQCharges.Text)) ? 0.00 : Convert.ToDouble(txtHQCharges.Text);

                    //#GST 
                    objtblInvGenHead.TrSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_SGST.Text)) ? 0.00 : Convert.ToDouble(txtT_SGST.Text);
                    objtblInvGenHead.TrCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_CGST.Text)) ? 0.00 : Convert.ToDouble(txtT_CGST.Text);
                    objtblInvGenHead.TrIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtT_IGST.Text)) ? 0.00 : Convert.ToDouble(txtT_IGST.Text);
                    objtblInvGenHead.ConSGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_SGST.Text)) ? 0.00 : Convert.ToDouble(txtC_SGST.Text);
                    objtblInvGenHead.ConCGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_CGST.Text)) ? 0.00 : Convert.ToDouble(txtC_CGST.Text);
                    objtblInvGenHead.ConIGST_Amt = string.IsNullOrEmpty(Convert.ToString(txtC_IGST.Text)) ? 0.00 : Convert.ToDouble(txtC_IGST.Text);

                    objtblInvGenHead.ConGSTCess_Amt = 0;
                    objtblInvGenHead.TrGSTCess_Amt = 0;
                    objtblInvGenHead.ShtgGST_Amt = Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text);
                    DateTime? DtPlantInDate = null;
                    DateTime? DtPlantOutDate = null;
                    DateTime? DtPlantInDate2 = null;
                    DateTime? DtPlantOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false)
                    {
                        DtPlantInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
                    {
                        DtPlantOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false)
                    {
                        DtPlantInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantInDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
                    {
                        DtPlantOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPlantOutDate2.Text));
                    }
                    DateTime? DtPortInDate = null;
                    DateTime? DtPortOutDate = null;
                    DateTime? DtPortInDate2 = null;
                    DateTime? DtPortOutDate2 = null;
                    if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false)
                    {
                        DtPortInDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
                    {
                        DtPortOutDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false)
                    {
                        DtPortInDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortinDate2.Text));
                    }
                    if (string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
                    {
                        DtPortOutDate2 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtPortoutDate2.Text));
                    }
                    objtblInvGenHead.Plant_InDate = DtPlantInDate;
                    objtblInvGenHead.Plant_OutDate = DtPlantOutDate;
                    objtblInvGenHead.Port_InDate = DtPortInDate;
                    objtblInvGenHead.Port_OutDate = DtPortOutDate;
                    objtblInvGenHead.Plant_InDate2 = DtPlantInDate2;
                    objtblInvGenHead.Plant_OutDate2 = DtPlantOutDate2;
                    objtblInvGenHead.Port_InDate2 = DtPortInDate2;
                    objtblInvGenHead.Port_OutDate2 = DtPortOutDate2;

                    objtblInvGenHead.Date_Added = Convert.ToDateTime(DateTime.Now);

                    if (ddlGrType.SelectedValue == "GR")
                    {
                        objtblInvGenHead.Gr_Type = "GR";
                    }
                    else
                    {
                        objtblInvGenHead.Gr_Type = "GRR";
                    }

                    if (Convert.ToBoolean(hidAdminApp.Value) == true)
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(chkApproved.Checked);
                    else
                        objtblInvGenHead.Admin_Approval = Convert.ToBoolean(1);

                    objtblInvGenHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                    objtblInvGenHead.User_AddedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                    objtblInvGenHead.Delivery_Add = txtDelvAddress.Text.Trim();
                    #endregion
                    #region Child Entry
                    DtTemp = CreateDt(); 
                    int c = 0;
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                        HiddenField hidItemIdno = (HiddenField)row.FindControl("hidItemIdno");
                        HiddenField hidUnitIdno = (HiddenField)row.FindControl("hidUnitIdno");
                        HiddenField hidDeliveryPlace = (HiddenField)row.FindControl("hidDeliveryPlace");

                        Label lblItemRate = (Label)row.FindControl("lblItemRate");
                        Label lblAmount = (Label)row.FindControl("lblAmount");
                        //Label lblNetAmnt = (Label)row.FindControl("lblNetAmnt");
                        Label lblServTaxAmnt = (Label)row.FindControl("lblServTaxAmnt");
                        TextBox txtOtherAmnt = (TextBox)row.FindControl("txtOtherAmnt");
                        //TextBox txtWagesAmnt = (TextBox)row.FindControl("txtWagesAmnt");
                        HiddenField hidServTaxPerc = (HiddenField)row.FindControl("hidServTaxPerc");
                        HiddenField hidServTaxValid = (HiddenField)row.FindControl("hidServTaxValid");
                        Label lblSwchBrtTaxAmnt = (Label)row.FindControl("lblSwchBrtTaxAmnt");
                        Label lblKisanTaxAmnt = (Label)row.FindControl("lblKisanTaxAmnt");
                        HiddenField lblGrTypeId = (HiddenField)row.FindControl("hidGrtype");
                        //#GST
                        Label lblSGSTAmnt = (Label)row.FindControl("lblSGSTAmnt");
                        Label lblCGSTAmnt = (Label)row.FindControl("lblCGSTAmnt");
                        Label lblIGSTAmnt = (Label)row.FindControl("lblIGSTAmnt");
                        nSGSTPer = 9;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["SGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["SGST_Per"]);
                        nCGSTPer = 9;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["CGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["CGST_Per"]);
                        nIGSTPer = 18;//string.IsNullOrEmpty(Convert.ToString(dtt.Rows[b]["IGST_Per"])) ? 0 : Convert.ToDouble(dtt.Rows[b]["IGST_Per"]);
                        GSTType = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[c]["GST_Idno"])) ? 0 : Convert.ToInt16(dtt.Rows[c]["GST_Idno"]);
                        if (GSTType == 1)
                        {
                            nSGSTAmnt = ((string.IsNullOrEmpty(Convert.ToString(dtt.Rows[c]["Toll_Amnt"])) ? 0 : Convert.ToDouble(dtt.Rows[c]["Toll_Amnt"])) * nSGSTPer) / 100;
                            nCGSTAmnt = ((string.IsNullOrEmpty(Convert.ToString(dtt.Rows[c]["Toll_Amnt"])) ? 0 : Convert.ToDouble(dtt.Rows[c]["Toll_Amnt"])) * nCGSTPer) / 100;
                            nIGSTAmnt = 0;
                        }
                        else if (GSTType == 2)
                        {
                            nSGSTAmnt = 0;
                            nCGSTAmnt = 0;
                            nIGSTAmnt = ((string.IsNullOrEmpty(Convert.ToString(dtt.Rows[c]["Toll_Amnt"])) ? 0 : Convert.ToDouble(dtt.Rows[c]["Toll_Amnt"])) * nIGSTPer) / 100;
                        }
                        if ((Convert.ToDouble(lblItemRate.Text) <= 0) && Convert.ToDouble(lblGrTypeId.Value) == 1)
                        {
                            ShowMessageErr("Rate Should be grater than Zero Please Define Item Rate in Rate Master");
                            txtDate.Focus();
                            return;
                        }
                        ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemIdno.Value, lblItemRate.Text, dtt.Rows[c]["Toll_Amnt"], hidUnitIdno.Value,
                                                          "0", dtt.Rows[c]["Toll_Amnt"], txtOtherAmnt.Text, lblServTaxAmnt.Text, hidServTaxPerc.Value,
                                                          hidServTaxValid.Value, lblSwchBrtTaxAmnt.Text, lblKisanTaxAmnt.Text, nSGSTAmnt.ToString("N2"),
                                                          nCGSTAmnt.ToString("N2"), nIGSTAmnt.ToString("N2"), hidDeliveryPlace.Value);
                        c++;
                    }
                    #endregion
                    #region For Annexure...
                    DataView dv = DtTemp.DefaultView;
                    dv.Sort = "Del_Place,Gr_Idno asc";
                    sortedDT = dv.ToTable();
                    if ((sortedDT != null) && (sortedDT.Rows.Count > 0))
                    {
                        childTemp = CreateDt();
                        string OldPlace = "";
                        Int32 AnnNo = 0, iRowCount = 0;
                        foreach (DataRow row in sortedDT.Rows)
                        {
                            if (string.IsNullOrEmpty(OldPlace.ToString()))
                            {
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            if (OldPlace == Convert.ToString(row["Del_Place"]))
                            {
                                iRowCount = iRowCount + 1;
                                if (iRowCount == 14)
                                {
                                    iRowCount = 0;
                                    AnnNo = AnnNo + 1;
                                }
                            }
                            else
                            {
                                iRowCount = 0;
                                OldPlace = Convert.ToString(row["Del_Place"]);
                                AnnNo = AnnNo + 1;
                            }

                            ApplicationFunction.DatatableAddRow(childTemp, Convert.ToString(row["Gr_Idno"]), Convert.ToString(row["Item_Idno"]), Convert.ToString(row["Item_Rate"]), Convert.ToString(row["Amount"]), Convert.ToString(row["Unit_Idno"]), Convert.ToString(row["Wayges"]), Convert.ToString(row["Net_Amnt"]), Convert.ToString(row["Other_Amnt"]),
                                Convert.ToString(row["ServTax_Amnt"]), Convert.ToString(row["ServTax_Perc"]), Convert.ToString(row["ServTax_Valid"]), Convert.ToString(row["SwchBrtTax_Amnt"]), Convert.ToString(row["KisanKalyan_Amnt"]), Convert.ToString(row["SGST_Amt"]), Convert.ToString(row["CGST_Amt"]), Convert.ToString(row["IGST_Amt"]), Convert.ToString(row["Del_Place"]),
                                Convert.ToString(AnnNo));
                        }
                    }
                    #endregion
                    #region Posting..
                    Int64 value = 0;
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == true)
                        {
                            value = obj.Insert(objtblInvGenHead, childTemp, ddlGrType.SelectedValue);
                        }
                        else
                        {
                            objtblInvGenHead.User_ModifiedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                            lnkbtnNew.Visible = false; imgPrint.Visible = false;
                            value = obj.Update(objtblInvGenHead, Convert.ToInt32(hidid.Value), childTemp, ddlGrType.SelectedValue);
                        }

                        if (value > 0)
                        {
                            double SGSTAmnt = 0.0, CGSTAmnt = 0.0, IGSTAmnt = 0.0;


                            SGSTAmnt = Convert.ToDouble(txtT_SGST.Text);

                            CGSTAmnt = Convert.ToDouble(txtT_CGST.Text);


                            IGSTAmnt = Convert.ToDouble(txtT_IGST.Text);

                            double AddFreight = Convert.ToDouble(string.IsNullOrEmpty(txtAddFreight.Text) ? "0" : txtAddFreight.Text);
                            double HQCharges = Convert.ToDouble(string.IsNullOrEmpty(txtHQCharges.Text) ? "0" : txtHQCharges.Text);
                            double PlantAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount.Text) ? "0" : txtPlantAmount.Text);
                            double PlantAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPlantAmount2.Text) ? "0" : txtPlantAmount2.Text);
                            double PortAmount = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount.Text) ? "0" : txtPortAmount.Text);
                            double PortAmount2 = Convert.ToDouble(string.IsNullOrEmpty(txtPortAmount2.Text) ? "0" : txtPortAmount2.Text);
                            double BiltyCharges = Convert.ToDouble(string.IsNullOrEmpty(txtBiltyCharges.Text) ? "0" : txtBiltyCharges.Text);
                            double OtherAmount = Convert.ToDouble(string.IsNullOrEmpty(txtOtherAmount.Text) ? "0" : txtOtherAmount.Text);
                            double OtherCharge1 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount1.Text) ? "0" : txtOtherChargesAmount1.Text);
                            double OtherCharge2 = Convert.ToDouble(string.IsNullOrEmpty(txtOtherChargesAmount2.Text) ? "0" : txtOtherChargesAmount2.Text);

                            double ExtraAmnt = AddFreight + HQCharges + PlantAmount + PlantAmount2 + PortAmount + PortAmount2 + BiltyCharges + OtherAmount + OtherCharge1 + OtherCharge2;
                            if (this.PostIntoAccounts(value, "IB", Convert.ToString(txtinvoicNo.Text.Trim()), Convert.ToString(txtDate.Text.Trim()), (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", ""))), 0, 0, 0, 0, ExtraAmnt, Convert.ToDouble((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text.Trim().Replace(",", ""))) - (string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0.00 : Convert.ToDouble(txtRoundOff.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrServTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrServTax.Text.Trim().Replace(",", ""))), Convert.ToDouble((string.IsNullOrEmpty(txtGrosstotal.Text.Trim()) ? 0.00 : Convert.ToDouble(txtGrosstotal.Text.Trim().Replace(",", "")))), (string.IsNullOrEmpty(txtTrSwchBrtTax.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTrSwchBrtTax.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(txtKisanTaxTrnptr.Text.Trim()) ? 0.00 : Convert.ToDouble(txtKisanTaxTrnptr.Text.Trim().Replace(",", ""))), (string.IsNullOrEmpty(ddlSenderName.SelectedValue) ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue)), (string.IsNullOrEmpty(ddldateRange.SelectedValue) ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                            //if (1 == 1)
                            {
                                obj.UpdateIsPosting(value);
                                if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                    {

                                        ShowMessage("Record Update successfully"); 
                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record  Not Update");
                                        tScope.Dispose();
                                    }
                                }
                                else
                                {
                                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                    {
                                        ShowMessage("Record  saved Successfully ");

                                        tScope.Complete();
                                    }
                                    else if (value == -1)
                                    {
                                        ShowMessageErr("Invoice No Already Exist");
                                        tScope.Dispose();
                                    }
                                    else
                                    {
                                        ShowMessageErr("Record Not  saved Successfully ");
                                        tScope.Dispose();
                                    }
                                }

                            }
                            else
                            {

                                if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                                {
                                    if (string.IsNullOrEmpty(Convert.ToString(hidid.Value)) == false)
                                    {
                                        hidpostingmsg.Value = "Record(s) not updated.";
                                    }
                                    else
                                    {
                                        hidpostingmsg.Value = "Record(s) not saved.";
                                    }
                                    tScope.Dispose();
                                }
                                tScope.Dispose();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                Clear();
            }
            else
            {
                ShowMessageErr("please Enter Details");
            }

        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(HidGrId.Value), Convert.ToString(HidGrType.Value));
            }
            else
            {
                Clear();
            }

        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("Invoice.aspx");
        }
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex <= 0)
            {
                ShowMessageErr("Please select From City");
                ddlFromCity.Focus();
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
            txtDateFrom.Focus();
        }
        protected void lnkCharges_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openOtherCharges();", true);
            txtDateFrom.Focus();
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            //GetGSTType();
            try
            {
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtinvoicNo.Text = Convert.ToString(objInvoiceDAL.SelectMaxInvNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), txtInvPreIx.Text));
                }
                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    string strchkDetlValue = string.Empty;
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdGrdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                        }
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Gr.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                    }
                    else
                    {
                        InvoiceDAL ObjInvoiceDAL = new InvoiceDAL();
                        string strSbillNo = String.Empty;
                        Int32 j = 0; string Grno = ""; dTotSwchBrtTrnsportr = 0; dTotServTaxTrnsportr = 0; dTotServTaxConsgn = 0; dTotSwchBrtTaxConsgn = 0; bool bServTaxExmpt = false; Int32 prtyidno = 0; bool ISTBBRate = false;
                        prtyidno = Convert.ToInt32((ddlSenderName.SelectedIndex <= 0) ? "0" : (ddlSenderName.SelectedValue)); dTotKisanTaxTrnsportr = 0; dTotKisanTaxConsgn = 0;
                        bServTaxExmpt = Convert.ToBoolean(ObjInvoiceDAL.selectServTaxExmpt(prtyidno));
                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        tblUserPref obj = objInvoiceDAL.SelectUserPref();

                        TbbRate = Convert.ToBoolean(obj.TBB_Rate);
                        //Int64 GrType = ObjInvoiceDAL.selectGrType(strchkDetlValue);
                        //if (GrType == 2) { TbbRate = true; }
                        //else { TbbRate = false; }

                        HidUserprefSurchrge.Value = Convert.ToString(obj.Surchg_Per);
                        HidUserprefBilty.Value = Convert.ToString(obj.Bilty_Amnt);
                        hidServTaxLimit.Value = Convert.ToString(obj.ServTax_Valid);
                        HidwagesUserpref.Value = Convert.ToString(obj.Wages_Amnt);
                        HidUserprefTollTax.Value = Convert.ToString(obj.TollTax_Amnt);
                        // hidServTaxPercnt.Value = Convert.ToString(obj.ServTax_Perc);
                        hidServTaxPercnt.Value = Convert.ToString(ObjInvoiceDAL.SelectServiceTaxFromTaxMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), Convert.ToInt32(ddlFromCity.SelectedValue)));
                        hidSwchBrtTaxPercnt.Value = Convert.ToString(ObjInvoiceDAL.SelectSwchBrtTaxFromTaxMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), Convert.ToInt32(ddlFromCity.SelectedValue)));
                        HiddKisanTax.Value = Convert.ToString(ObjInvoiceDAL.SelectKisanTaxFromTaxMaster(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), Convert.ToInt32(ddlFromCity.SelectedValue)));

                        if (ddlGrType.SelectedValue == "GR")
                        {
                            dtRcptDetl = ObjInvoiceDAL.SelectGrChallanDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), TbbRate);
                        }
                        else
                        {
                            dtRcptDetl = ObjInvoiceDAL.SelectGrChallanRetailerDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), TbbRate);
                        }

                        //UserPref Minus Challan To Pay amount 
                        tblUserPref uf = objInvoiceDAL.GetUserPrefDetail();
                        //To pay challan amount
                        if (dtRcptDetl != null && dtRcptDetl.Rows.Count > 0 && uf.LessChlnAmnt_Inv == true)
                        {
                            Int32 iGrIdnoForChallan = Convert.ToInt32((dtRcptDetl.Rows[0]["GST_Idno"] == null || dtRcptDetl.Rows[0]["GST_Idno"] == "") ? "0" : dtRcptDetl.Rows[0]["GST_Idno"]);
                            int GrType = Convert.ToInt32(dtRcptDetl.Rows[0]["GR_Typ"]);
                            var dt = objInvoiceDAL.GetChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt64(iGrIdnoForChallan));
                            hidLessChallanAmnt.Value = "";
                            if (dt != null && dt.Rows.Count > 0 && GrType == 3)
                            {
                                hidLessChallanAmnt.Value = "1";
                                txtChallanAmountOnToPay.Enabled = true;
                                txtChallanAmountOnToPay.Text = dt.Rows[0]["ChlnNet_Amnt"].ToString();
                            }
                            else
                            {
                                hidLessChallanAmnt.Value = "0";
                                txtChallanAmountOnToPay.Text = "0.00";
                                txtChallanAmountOnToPay.Visible = false;
                            }
                        }

                        for (int k = 0; k < dtRcptDetl.Rows.Count; k++)
                        {
                            string GSTTypeIdno = (dtRcptDetl.Rows[k]["GST_Idno"] == null ? "0" : dtRcptDetl.Rows[k]["GST_Idno"]).ToString();
                            hidGstType.Value = GSTTypeIdno.ToString();
                            Grno = Convert.ToString((dtRcptDetl.Rows[k]["Gr_No"]) == "" ? "0" : dtRcptDetl.Rows[k]["Gr_No"]);
                            ISTBBRate = Convert.ToBoolean(dtRcptDetl.Rows[k]["TBB_Rate"]);
                            int GrType = Convert.ToInt32(dtRcptDetl.Rows[k]["GR_Typ"]);
                            
                            if (k == 0)
                            {
                                if (ISTBBRate == false)
                                {
                                    dtRcptDetl.Rows[k]["other_Amnt"] = ((Convert.ToDouble(dtRcptDetl.Rows[k]["Amount"]) * (Convert.ToDouble(HidUserprefSurchrge.Value))) / 100) + (Convert.ToDouble(HidUserprefBilty.Value)) + ((Convert.ToDouble(HidUserprefTollTax.Value)) * (Convert.ToDouble(dtRcptDetl.Rows[k]["Qty"])));
                                }
                                dtRcptDetl.Rows[k]["Wages_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["Wages_Amnt"]);
                                dtRcptDetl.Rows[k]["other_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["other_Amnt"]);
                                dtRcptDetl.Rows[k]["ServTax_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                //GST
                                dtRcptDetl.Rows[k]["SGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                dtRcptDetl.Rows[k]["CGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                dtRcptDetl.Rows[k]["IGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                j++;
                            }
                            else
                            {
                                if (Grno == Convert.ToString((dtRcptDetl.Rows[k - 1]["Gr_No"]) == "" ? "0" : dtRcptDetl.Rows[k - 1]["Gr_No"]))
                                {
                                    if (ISTBBRate == false)
                                    {
                                        dtRcptDetl.Rows[k]["other_Amnt"] = ((Convert.ToDouble(dtRcptDetl.Rows[k]["Amount"]) * (Convert.ToDouble(HidUserprefSurchrge.Value))) / 100) + ((Convert.ToDouble(HidUserprefTollTax.Value)) * (Convert.ToDouble(dtRcptDetl.Rows[k]["Qty"])));
                                        dtRcptDetl.Rows[k]["Wages_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = 0;
                                        //GST
                                        dtRcptDetl.Rows[k]["SGST_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["CGST_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["IGST_Amt"] = 0;
                                    }
                                    else
                                    {
                                        dtRcptDetl.Rows[k]["Wages_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["other_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = 0;
                                        //GST
                                        dtRcptDetl.Rows[k]["SGST_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["CGST_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["IGST_Amt"] = 0;
                                    }
                                }
                                else
                                {
                                    j = 0;
                                    if (ISTBBRate == false)
                                    {
                                        dtRcptDetl.Rows[k]["other_Amnt"] = ((Convert.ToDouble(dtRcptDetl.Rows[k]["Amount"]) * (Convert.ToDouble(HidUserprefSurchrge.Value))) / 100) + (Convert.ToDouble(HidUserprefBilty.Value)) + ((Convert.ToDouble(HidUserprefTollTax.Value)) * (Convert.ToDouble(dtRcptDetl.Rows[k]["Qty"])));
                                        dtRcptDetl.Rows[k]["Wages_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["Wages_Amnt"]);
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                        //GST
                                        dtRcptDetl.Rows[k]["SGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                        dtRcptDetl.Rows[k]["CGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                        dtRcptDetl.Rows[k]["IGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                    }
                                    else
                                    {
                                        dtRcptDetl.Rows[k]["Wages_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["Wages_Amnt"]);
                                        dtRcptDetl.Rows[k]["other_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["other_Amnt"]);
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = Convert.ToString(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                        //GST
                                        dtRcptDetl.Rows[k]["SGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                        dtRcptDetl.Rows[k]["CGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                        dtRcptDetl.Rows[k]["IGST_Amt"] = Convert.ToString(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                    }
                                }
                            }
                            if (Convert.ToInt32(dtRcptDetl.Rows[k]["GTypeID"]) != 2)
                            {
                                dtRcptDetl.Rows[k]["Net_Amnt"] = Convert.ToDouble(dtRcptDetl.Rows[k]["other_Amnt"]) + Convert.ToDouble(dtRcptDetl.Rows[k]["Wages_Amnt"]) + Convert.ToDouble(dtRcptDetl.Rows[k]["Amount"]);
                            }
                            else if (Convert.ToInt32(dtRcptDetl.Rows[k]["GTypeID"]) == 2)
                            {
                                if (k == 0)
                                {
                                    dtRcptDetl.Rows[k]["Amount"] = Convert.ToDouble(dtRcptDetl.Rows[k]["TypeAmnt"]);
                                }
                                else if (Convert.ToString(Grno) != (Convert.ToString(dtRcptDetl.Rows[k - 1]["Gr_No"]) == "" ? "0" : Convert.ToString(dtRcptDetl.Rows[k - 1]["Gr_No"])))
                                {
                                    dtRcptDetl.Rows[k]["Amount"] = Convert.ToDouble(dtRcptDetl.Rows[k]["TypeAmnt"]);
                                }
                            }

                            if (ISTBBRate == true)
                            {
                                if (Convert.ToString(dtRcptDetl.Rows[k]["STax_Typ"]) == "Transporter")
                                {
                                    double ttlGSTPercent = 0;
                                    if (GSTTypeIdno == "0")
                                    {
                                        dTotServTaxTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                        dTotSwchBrtTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                        dTotKisanTaxTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                        dTotSGSTTransporter = 0;
                                        dTotCGSTTransporter = 0;
                                        dTotIGSTTransporter = 0;
                                    }
                                    else 
                                    {
                                        // #GST
                                        dTotServTaxTrnsportr = 0;
                                        dTotSwchBrtTrnsportr = 0;
                                        dTotKisanTaxTrnsportr = 0;
                                        dTotSGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                        dTotCGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                        dTotIGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                        if (GSTTypeIdno == "1")
                                            ttlGSTPercent = Convert.ToDouble(dtRcptDetl.Rows[k]["SGST_Per"] == null ? "0" : dtRcptDetl.Rows[k]["SGST_Per"]) + Convert.ToDouble(dtRcptDetl.Rows[k]["CGST_Per"] == null ? "0" : dtRcptDetl.Rows[k]["CGST_Per"]);
                                        else if (GSTTypeIdno == "2")
                                            ttlGSTPercent =  Convert.ToDouble(dtRcptDetl.Rows[k]["IGST_Per"] == null ? "0" : dtRcptDetl.Rows[k]["IGST_Per"]);
                                        dShortageGST += (Convert.ToDouble(dtRcptDetl.Rows[k]["shortage_Amount"]) * ttlGSTPercent)/100;
                                    }
                                }
                                else
                                {
                                    if (GSTTypeIdno == "0")
                                    {
                                        dTotServTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                        dTotSwchBrtTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                        dTotKisanTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                        dTotSGSTConsigner = 0;
                                        dTotCGSTConsigner = 0;
                                        dTotIGSTConsigner = 0;
                                    }
                                    else
                                    {
                                        dTotServTaxConsgn =0;
                                        dTotSwchBrtTaxConsgn =0;
                                        dTotKisanTaxConsgn = 0;
                                        // #GST
                                        dTotSGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                        dTotCGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                        dTotIGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                    }
                                    //dTotGSTCessConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["GSTCess_Amnt"]);
                                }
                            }
                            else if (ISTBBRate == false)
                            {
                                if (((bServTaxExmpt) == false))
                                {
                                    if (Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) <= (Convert.ToDouble(hidServTaxLimit.Value)))
                                    {
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = 0;
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = 0;
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = 0;
                                    }
                                    else if ((Convert.ToDouble(hidServTaxLimit.Value) == 0))
                                    {
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(hidServTaxPercnt.Value))) / 100)).ToString("N2");
                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(hidSwchBrtTaxPercnt.Value))) / 100)).ToString("N2");
                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(HiddKisanTax.Value))) / 100)).ToString("N2");
                                    }
                                    else
                                    {
                                        dtRcptDetl.Rows[k]["ServTax_Amnt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(hidServTaxPercnt.Value))) / 100)).ToString("N2");

                                        dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(hidSwchBrtTaxPercnt.Value))) / 100)).ToString("N2");

                                        dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = Convert.ToDouble(((Convert.ToDouble(dtRcptDetl.Rows[k]["Net_Amnt"]) * (Convert.ToDouble(HiddKisanTax.Value))) / 100)).ToString("N2");
                                    }
                                    if (Convert.ToString(dtRcptDetl.Rows[k]["STax_Typ"]) == "Transporter")
                                    {
                                        if (hidGstType.Value == "0")
                                        {
                                            dTotServTaxTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                            dTotSwchBrtTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                            dTotKisanTaxTrnsportr += Convert.ToDouble(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                            dTotSGSTTransporter = 0;
                                            dTotCGSTTransporter = 0;
                                            dTotIGSTTransporter = 0;
                                        }
                                        else
                                        {
                                            // #GST
                                            dTotServTaxTrnsportr = 0;
                                            dTotSwchBrtTrnsportr = 0;
                                            dTotKisanTaxTrnsportr = 0;
                                            dTotSGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                            dTotCGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                            dTotIGSTTransporter += Convert.ToDouble(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                        }
                                    }
                                    else
                                    {
                                        if (GSTTypeIdno == "0")
                                        {
                                            dTotServTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["ServTax_Amnt"]);
                                            dTotSwchBrtTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["SwchBrtTax_Amt"]);
                                            dTotKisanTaxConsgn += Convert.ToDouble(dtRcptDetl.Rows[k]["KisanKalyan_Amnt"]);
                                            dTotSGSTConsigner = 0;
                                            dTotCGSTConsigner = 0;
                                            dTotIGSTConsigner = 0;
                                        }
                                        else
                                        {
                                            dTotServTaxConsgn = 0;
                                            dTotSwchBrtTaxConsgn = 0;
                                            dTotKisanTaxConsgn = 0;
                                            // #GST
                                            dTotSGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["SGST_Amt"]);
                                            dTotCGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["CGST_Amt"]);
                                            dTotIGSTConsigner += Convert.ToDouble(dtRcptDetl.Rows[k]["IGST_Amt"]);
                                        }
                                    }
                                }
                                else
                                {
                                    dtRcptDetl.Rows[k]["ServTax_Amnt"] = 0;
                                    dtRcptDetl.Rows[k]["SwchBrtTax_Amt"] = 0;
                                    dtRcptDetl.Rows[k]["KisanKalyan_Amnt"] = 0;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(hidid.Value) == false)
                        {
                            double RcpttAmnt = ObjInvoiceDAL.SelectSaveRcptAmnt(Convert.ToString(strchkDetlValue), ApplicationFunction.ConnectionString());
                            txtPrevRcptt.Text = RcpttAmnt.ToString("N2");
                        }

                        txtShortageGSTAmnt.Text = dShortageGST.ToString("N2");
                        dNetAmnt = Convert.ToDouble(dtRcptDetl.Compute("SUM(Net_Amnt)", ""));
                        dtotAmnt = Convert.ToDouble(dtRcptDetl.Compute("SUM(Amount)", ""));
                        dTotShrtAmnt = Convert.ToDouble(dtRcptDetl.Compute("SUM(shortage_Amount)", ""));
                        //Check
                        txtShortageAmnt.Text = dTotShrtAmnt.ToString("N2");
                        dNetShrtAmnt = dTotShrtAmnt;
                        dtotServTaxAmnt = (dTotServTaxTrnsportr + dTotServTaxConsgn);
                        txtGrosstotal.Text = dtotAmnt.ToString("N2");
                        txtTrServTax.Text = dTotServTaxTrnsportr.ToString("N2");
                        txtCSServTax.Text = dTotServTaxConsgn.ToString("N2");
                        txtKisanTax.Text = dTotKisanTaxConsgn.ToString("N2");
                        txtKisanTaxTrnptr.Text = dTotKisanTaxTrnsportr.ToString("N2");
                        txtTrSwchBrtTax.Text = dTotSwchBrtTrnsportr.ToString("N2");
                        txtCSSwchBrtTax.Text = dTotSwchBrtTaxConsgn.ToString("N2");

                        // #GST
                        txtC_SGST.Text = dTotSGSTConsigner.ToString("N2");
                        txtC_CGST.Text = dTotCGSTConsigner.ToString("N2");
                        txtC_IGST.Text = dTotIGSTConsigner.ToString("N2");
                        txtT_SGST.Text = dTotSGSTTransporter.ToString("N2");
                        txtT_CGST.Text = dTotCGSTTransporter.ToString("N2");
                        txtT_IGST.Text = dTotIGSTTransporter.ToString("N2");

                        ShowDiv("HideBillAgainst('dvGrdetails')");
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        netamntcal();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                        ddlSenderName.SelectedValue = ddlSenderName.SelectedValue;
                        ddlSenderName.Enabled = false;
                    }
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();

                    ShowDiv("HideBillAgainst('dvGrdetails')");
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }

        }
        protected void lnkbtnAccPosting_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                int Count = 0; DataTable dt = CreateDt();
                InvoiceDAL objDal = new InvoiceDAL();
                BindDropdownDAL obj1 = new BindDropdownDAL();
                clsAccountPosting objVATInvoicePOSDAL = new clsAccountPosting();
                DataSet objDataSet = objDal.AccPosting(ApplicationFunction.ConnectionString(), "InvoicePOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));

                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
                        {
                            tblInvGenHead chlnBookhead = objDal.selectHead(Convert.ToInt64(objDataSet.Tables[0].Rows[i]["Inv_Idno"]));
                            DataTable objDetl = objDal.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(chlnBookhead.Year_Idno), Convert.ToInt64(chlnBookhead.Inv_Idno), Convert.ToString(chlnBookhead.Gr_Type));
                            double RcpttAmnt = objDal.SelectUpdateRcptAmnt(chlnBookhead.Inv_Idno, ApplicationFunction.ConnectionString());
                            double dOtherAmnt = Convert.ToDouble(objDetl.Compute("SUM(Other_Amnt)", "")) + Convert.ToDouble(objDetl.Compute("SUM(Wages_Amnt)", ""));

                            double SGSTAmnt = 0.0, CGSTAmnt = 0.0, IGSTAmnt = 0.0;

                                SGSTAmnt = Convert.ToDouble(txtT_SGST.Text);
                                CGSTAmnt = Convert.ToDouble(txtT_CGST.Text);
                                IGSTAmnt = Convert.ToDouble(txtT_IGST.Text);

                            double AddCharges_Amnt = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.AddCharges_Amnt.ToString()) ? 0.00 : chlnBookhead.AddCharges_Amnt);
                            double HQCharges_Amnt = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.HQCharges_Amnt.ToString()) ? 0.00 : chlnBookhead.HQCharges_Amnt);
                            double PlantAmount = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.PlantAmount.ToString()) ? 0.00 : chlnBookhead.PlantAmount);
                            double PlantAmount2 = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.PlantAmount2.ToString()) ? 0.00 : chlnBookhead.PlantAmount2);
                            double PortAmount = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.PortAmount.ToString()) ? 0.00 : chlnBookhead.PortAmount);
                            double PortAmount2 = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.PortAmount2.ToString()) ? 0.00 : chlnBookhead.PortAmount2);
                            double Charges1_Amnt = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.Charges1_Amnt.ToString()) ? 0.00 : chlnBookhead.Charges1_Amnt);
                            double Charges2_Amnt = Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.Charges2_Amnt.ToString()) ? 0.00 : chlnBookhead.Charges2_Amnt);
                            double Totalamount = AddCharges_Amnt + HQCharges_Amnt + PlantAmount + PlantAmount2 + PortAmount + PortAmount2+ Charges1_Amnt+ Charges2_Amnt;
                            //Comment by salman 
                            //  if (this.PostIntoAccounts(Convert.ToInt64(chlnBookhead.Inv_Idno), "IB", chlnBookhead.Inv_No.ToString(), Convert.ToDateTime(chlnBookhead.Inv_Date).ToString("dd-MM-yyyy"), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.RoundOff_Amnt.ToString()) ? 0.00 : chlnBookhead.RoundOff_Amnt), 0, 0, 0, 0, Convert.ToDouble(dOtherAmnt + (string.IsNullOrEmpty(chlnBookhead.Bilty_Chrgs.ToString()) ? 0.00 : chlnBookhead.Bilty_Chrgs)), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.Net_Amnt.ToString()) ? 0.00 : chlnBookhead.Net_Amnt) - (string.IsNullOrEmpty(chlnBookhead.RoundOff_Amnt.ToString()) ? 0.00 : chlnBookhead.RoundOff_Amnt)), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrServTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrServTax_Amnt), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.GrossTot_Amnt.ToString()) ? 0.00 : chlnBookhead.GrossTot_Amnt) - (string.IsNullOrEmpty(chlnBookhead.Short_Amnt.ToString()) ? 0.00 : chlnBookhead.Short_Amnt)), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrSwchBrtTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrSwchBrtTax_Amnt), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrKisanKalyanTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrKisanKalyanTax_Amnt), Convert.ToInt64(chlnBookhead.Sendr_Idno), Convert.ToInt32(chlnBookhead.Year_Idno), SGSTAmnt, CGSTAmnt, IGSTAmnt) == true)
                            if (this.PostIntoAccounts(Convert.ToInt64(chlnBookhead.Inv_Idno), "IB", chlnBookhead.Inv_No.ToString(), Convert.ToDateTime(chlnBookhead.Inv_Date).ToString("dd-MM-yyyy"), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.RoundOff_Amnt.ToString()) ? 0.00 : chlnBookhead.RoundOff_Amnt), 0, 0, 0, 0, Convert.ToDouble(dOtherAmnt + (string.IsNullOrEmpty(chlnBookhead.Bilty_Chrgs.ToString()) ? 0.00 : chlnBookhead.Bilty_Chrgs)+ Totalamount), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.Net_Amnt.ToString()) ? 0.00 : chlnBookhead.Net_Amnt) - (string.IsNullOrEmpty(chlnBookhead.RoundOff_Amnt.ToString()) ? 0.00 : chlnBookhead.RoundOff_Amnt)), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrServTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrServTax_Amnt), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.GrossTot_Amnt.ToString()) ? 0.00 : chlnBookhead.GrossTot_Amnt)), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrSwchBrtTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrSwchBrtTax_Amnt), Convert.ToDouble(string.IsNullOrEmpty(chlnBookhead.TrKisanKalyanTax_Amnt.ToString()) ? 0.00 : chlnBookhead.TrKisanKalyanTax_Amnt), Convert.ToInt64(chlnBookhead.Sendr_Idno), Convert.ToInt32(chlnBookhead.Year_Idno), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.TrSGST_Amt.ToString()) ? 0.00 : chlnBookhead.TrSGST_Amt)), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.TrCGST_Amt.ToString()) ? 0.00 : chlnBookhead.TrCGST_Amt)), Convert.ToDouble((string.IsNullOrEmpty(chlnBookhead.TrIGST_Amt.ToString()) ? 0.00 : chlnBookhead.TrIGST_Amt))) == true)
                            {
                                objDal.UpdateIsPosting(Convert.ToInt64(chlnBookhead.Inv_Idno));
                                Count = Count + 1;
                            }
                        }
                    }
                    catch (Exception exe)
                    {

                    }
                    this.PostingLeft();
                }
            }
        }
        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceDAL obj = new InvoiceDAL();

                Int64 iSenderIdno = (ddlSenderName.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSenderName.SelectedValue));
                Int32 iFromCityIdno = (ddlFromCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                string strGrFrm = "";

                DataTable DsGrdetail = obj.selectGrDetails("SelectGRDetail", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(txtDateFrom.Text), Convert.ToString(txtDateTo.Text), iSenderIdno, ApplicationFunction.ConnectionString(), iFromCityIdno, ddlGrType.SelectedValue);
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnSubmit.Visible = true; lnkbtnclear.Visible = true;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnSubmit.Visible = false; lnkbtnclear.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);

            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkSave_OnClick(object sender, EventArgs e)
        {
            netamntcal();
        }

        #endregion
        #region Functions...

        private void PostingLeft()
        {
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                clsAccountPosting clsobj = new clsAccountPosting();
                DataSet objDataSets = objInvoiceDAL.AccPosting(ApplicationFunction.ConnectionString(), "InvoicePOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
                if (objDataSets != null && objDataSets.Tables.Count > 0 && objDataSets.Tables[1].Rows.Count > 0)
                {
                    lblPostingLeft.Text = "Record(s) : " + Convert.ToString(objDataSets.Tables[1].Rows[0][0]);
                }
                else
                {
                    lblPostingLeft.Text = "Record(s) : 0";
                }
            }
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void PrintInvoice(Int64 HeadIdno)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            # region Company Details  # region Company Details
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname.Text = CompName; lblcompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompTIN.Text = TinNo.ToString();
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            lblPanNo.Text = PanNo.ToString();
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (PanNo == "")
            {
                lblTxtPanNo.Visible = false; lblPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblTxtPanNo.Visible = true; lblPanNo.Visible = true;
            }


            DataSet CodeNo = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name from AcntMast A Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno  WHERE acnt_name like '%shree cement%' AND I.Inv_Idno='" + HeadIdno + "' ");
            if (CodeNo != null && CodeNo.Tables[0].Rows.Count > 0)
            {
                lblcodeno.Visible = true;
                lblvaluecodeno.Visible = true;
                lblvaluecodeno.Text = "T0073";
            }
            #endregion
            DataSet dsReport = null;
            if (Convert.ToInt32(hidPrintType.Value) == 2)
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrint',@Id='" + HeadIdno + "'");
            }
            if (Convert.ToInt32(hidPrintType.Value) == 3)
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectDaulatPrint',@Id='" + HeadIdno + "'");
            }
            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicveno.Text = Convert.ToString(txtInvPreIx.Text) + Convert.ToString(txtinvoicNo.Text);
                valuelblinvoicedate.Text = (txtDate.Text);
                lblSenderName.Text = Convert.ToString(ddlSenderName.SelectedItem.Text);
                lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                lblSenderGSTINNo.Text = "GSTIN No. : " + Convert.ToString(dsReport.Tables[1].Rows[0]["Party_GSTINNo"]);
                if (lblSenderGSTINNo.Text == "") lblSenderGSTINNo.Visible = false;
                if (lblsenderaddress.Text == "")
                {
                    lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                }
                lblsenderstate.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
                lblsendercity.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                lblvalueencosers.Text = Convert.ToString(Repeater1.Items.Count);
                lblNetAmnt.Text = string.Format("{0:0,0.00}", (txtNetAmnt.Text));
                valuelbltxtctax.Text = string.Format("{0:0,0.00}", (txtCSServTax.Text));
                valuelblservtax.Text = string.Format("{0:0,0.00}", (txtTrServTax.Text));
                valueCSBTax.Text = string.Format("{0:0,0.00}", (txtCSSwchBrtTax.Text));
                valueTSBTax.Text = string.Format("{0:0,0.00}", (txtTrSwchBrtTax.Text));
                ValueCKisanTax.Text = string.Format("{0:0,0.00}", (txtKisanTax.Text));
                ValueTKisanTax.Text = string.Format("{0:0,0.00}", (txtKisanTaxTrnptr.Text));
                lblShotageAmnt.Text = string.Format("{0:0,0.00}", (txtShortageAmnt.Text));
                lblOtherTotAmnt.Text = string.Format("{0:0,0.00}", (txtOtherAmount.Text));
            }
            Loadimage();


        }
        public void Loadimage()
        {
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            tblUserPref obj1 = objInvoiceDAL.SelectUserPref();
            if (Convert.ToBoolean(obj1.Logo_Req))
            {
                byte[] img = obj1.Logo_Image;
                string base64String = Convert.ToBase64String(img, 0, img.Length);
                hideimgvalue.Value = "data:image/png;base64," + base64String;
                if (obj1.Logo_Image != null)
                {
                    if (hidPrintType.Value == "1")
                    {
                        imgprintGen.Visible = true;
                        imgprintGen.ImageUrl = hideimgvalue.Value;
                    }
                    else if (hidPrintType.Value == "2")
                    {
                        imageprint.Visible = true;
                        imageprint.ImageUrl = hideimgvalue.Value;
                    }
                    else if (hidPrintType.Value == "4")
                    {
                        if (chkbit == 1)
                        {
                            Imgjain.Visible = true;
                            Imgjain.ImageUrl = hideimgvalue.Value;
                        }
                        else if (chkbit == 2)
                        {
                            //imgjainsingle.Visible = true;
                            //imgjainsingle.ImageUrl = hideimgvalue.Value;
                        }
                    }
                }
                else
                {
                    imgprintGen.Visible = false;
                    imageprint.Visible = false;
                    Imgjain.Visible = false;
                    //imgjainsingle.Visible = false;
                }
            }
            else
            {
                imageprint.Visible = false;
                //imgprintGen.Visible = false;
                //imgjainsingle.Visible = false;
                //Imgjain.Visible = false;
            }
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Item_Idno", "String", "Item_Rate", "String", "Amount", "String", "Unit_Idno", "String", "Wayges", "String", "Net_Amnt", "String",
                                                                 "Other_Amnt", "String", "ServTax_Amnt", "String", "ServTax_Perc", "String", "ServTax_Valid", "String", "SwchBrtTax_Amnt", "String", "KisanKalyan_Amnt", "String",
                                                                 "SGST_Amt", "String", "CGST_Amt", "String", "IGST_Amt", "String", "Del_Place", "String", "Annexure_No", "String", "Toll_amnt", "String");
            return dttemp;
        }
        private DataTable CreateGRDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "GR_No", "String", "GR_Date", "String", "Ref_No", "String", "Delvry_Place", "String", "Item_Name", "String", "Unit", "String", "ItemRate_type", "String",
                                                                 "Qty", "String", "Weight", "String", "Rate", "String", "Amount", "String", "STax_Typ", "String", "Wages_Amnt", "String", "SGST_Amt", "String", "CGST_Amt", "String", "IGST_Amt", "String",
                                                                  "GSTCess_Amt", "String", "UGST_Amt", "String", "ServTax_Amnt", "String", "SwchBrtTax_Amt", "String", "KisanKalyan_Amnt", "String", "GrTypeId", "String", "Net_Amnt", "String", "GR_Idno", "String", "Item_Idno", "String"
                                                                  ,"Unit_Idno", "String", "Other_Amnt", "String", "shortage", "String", "shortage_Amount", "String", "Shrtg_Limit", "String", "Shrtg_Rate", "String", "Shortage_Qty", "String", "Gr_Typ", "String", "TBB_RATE", "String"
                                                         ,"GrType", "String", "ServTax_Perc", "String", "SwchBrtTax_Perc", "String", "KisanTax_Perc", "String", "ServTax_Valid", "String", "CityVia_Name", "String", "GTypeID", "String", "TypeAmnt", "String", "Rate_Type", "String"
                                                              ,"SGST_Per", "String", "CGST_Per", "String", "IGST_Per", "String", "GST_Idno" ,"String");
            return dttemp;
        }
        private DataTable CreateGRListDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "GST_Idno", "String", "GR_No", "String", "GR_Date", "String", "Ref_No", "String", "Delvry_Place", "String", "Item_Name", "String", "Unit", "String",
                                                                 "Qty", "String", "Weight", "String", "GrTypeId", "String", "Rate", "String", "Amount", "String", "Wages_Amnt", "String", "Net_Amnt", "String", "Other_Amnt", "String", "GR_Idno", "String",
                                                                  "Item_Idno", "String", "Unit_Idno", "String", "STax_Typ", "String", "ServTax_Amnt", "String", "SwchBrtTax_Amt", "String", "TBB_RATE", "String", "KisanKalyan_Amnt", "String", "ServTax_Perc", "String", "SwchBrtTax_Perc", "String"
                                                                  , "KisanTax_Perc", "String", "ServTax_Valid", "String", "SGST_Amt", "String", "CGST_Amt", "String", "IGST_Amt", "String", "SGST_Per", "String", "CGST_Per", "String", "IGST_Per", "String", "CityVia_Name", "String");
            return dttemp;
        }
	
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            if (lst.Count > 0)
            {
                ddlFromCity.DataSource = lst;
                ddlFromCity.DataTextField = "City_Name";
                ddlFromCity.DataValueField = "City_Idno";
                ddlFromCity.DataBind();

            }
            ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            obj = null;
            if (FrmCity.Count > 0)
            {
                ddlFromCity.DataSource = FrmCity;
                ddlFromCity.DataTextField = "City_Name";
                ddlFromCity.DataValueField = "City_Idno";
                ddlFromCity.DataBind();
            }
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindPrincLoc(Int64 intPCIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindPrincLoc(intPCIdno);
            obj = null;
            if (lst.Count > 0)
            {
                ddlPrintLoc.DataSource = lst;
                ddlPrintLoc.DataTextField = "PCompLoc_Name";
                ddlPrintLoc.DataValueField = "PCompLoc_Idno";
                ddlPrintLoc.DataBind();
            }
        }

        private void netamntcal()
        {
            try
            {
                ChargesName1 = txtOtherCharge1.Text.Trim();
                ChargesName2 = txtOtherCharge2.Text.Trim();
                ChargeAmount1 = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount1.Text.Trim())) ? 0 : Convert.ToDouble(txtOtherChargesAmount1.Text.Trim());
                ChargeAmount2 = string.IsNullOrEmpty(Convert.ToString(txtOtherChargesAmount2.Text.Trim())) ? 0 : Convert.ToDouble(txtOtherChargesAmount2.Text.Trim());
                FreightAmount = string.IsNullOrEmpty(Convert.ToString(txtAddFreight.Text.Trim())) ? 0 : Convert.ToDouble(txtAddFreight.Text.Trim());
                HQChargesAmount = string.IsNullOrEmpty(Convert.ToString(txtHQCharges.Text.Trim())) ? 0 : Convert.ToDouble(txtHQCharges.Text.Trim());
                double plantAmount = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount.Text.Trim())) ? 0 : Convert.ToDouble(txtPlantAmount.Text.Trim());
                double plantAmount2 = string.IsNullOrEmpty(Convert.ToString(txtPlantAmount2.Text.Trim())) ? 0 : Convert.ToDouble(txtPlantAmount2.Text.Trim());
                double portamount = string.IsNullOrEmpty(Convert.ToString(txtPortAmount.Text.Trim())) ? 0 : Convert.ToDouble(txtPortAmount.Text.Trim());
                double portamount2 = string.IsNullOrEmpty(Convert.ToString(txtPortAmount2.Text.Trim())) ? 0 : Convert.ToDouble(txtPortAmount2.Text.Trim());
                double bilityAmount = string.IsNullOrEmpty(Convert.ToString(txtBiltyCharges.Text.Trim())) ? 0 : Convert.ToDouble(txtBiltyCharges.Text.Trim());
                double dblAmnt = 0; double dConsingerTax = 0; double dTrServTax = 0; double dOtherAmnt = 0; double dExtraAmnt = 0;
                foreach (GridViewRow dr in grdMain.Rows)
                {
                    Label lblNetAmnt = (Label)dr.FindControl("lblAmount");
                    dblAmnt += Convert.ToDouble(lblNetAmnt.Text);
                    TextBox txtWagesAmnt = (TextBox)dr.FindControl("txtWagesAmnt");
                    TextBox txtOtherAmnt = (TextBox)dr.FindControl("txtOtherAmnt");
                    dOtherAmnt += Convert.ToDouble(txtWagesAmnt.Text) + Convert.ToDouble(txtOtherAmnt.Text);
                    Label lblSTaxpaidBy = (Label)dr.FindControl("lblSTaxpaidBy");
                    Label lblServTaxAmnt = (Label)dr.FindControl("lblServTaxAmnt");
                    if (lblSTaxpaidBy.Text == "Transporter")
                    {
                        dTrServTax += Convert.ToDouble(lblServTaxAmnt.Text);
                    }
                    else
                    {
                        dConsingerTax += Convert.ToDouble(lblServTaxAmnt.Text);
                    }
                }
                dExtraAmnt += FreightAmount + plantAmount + portamount + plantAmount2 + portamount2 + bilityAmount;
                if (ChargesName1 != "" && ChargeAmount1 >= 0.00)
                    dExtraAmnt += ChargeAmount1;
                if (ChargesName2 != "" && ChargeAmount2 >= 0.00)
                    dExtraAmnt += ChargeAmount2;
                txtGrosstotal.Text = dblAmnt.ToString("N2");
                txtOtherAmount.Text = dOtherAmnt.ToString("N2");
                txtTrServTax.Text = Convert.ToDouble(dTrServTax).ToString("N2");
                txtCSServTax.Text = Convert.ToDouble(dConsingerTax).ToString("N2");
                double dNetAmnt = 0;
                Int64 iNetAmnt = 0;
                double dChlnAmntToLess = 0;
                //If Challan is ToPay and UserPref LessChlnamnt_Inv is true
                if (hidLessChallanAmnt.Value == "1")
                    dChlnAmntToLess = Convert.ToDouble(txtChallanAmountOnToPay.Text == "" ? "0" : txtChallanAmountOnToPay.Text);

                if (hidGstType.Value == "1")
                {
                    if (hidRequiredShortageGST.Value == "1")
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtT_SGST.Text) + Convert.ToDouble(txtT_CGST.Text) + Convert.ToDouble(txtT_IGST.Text) + Convert.ToDouble(txtOtherAmount.Text) - (Convert.ToDouble(txtShortageAmnt.Text)) - (Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text)) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    else
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtT_SGST.Text) + Convert.ToDouble(txtT_CGST.Text) + Convert.ToDouble(txtT_IGST.Text) + Convert.ToDouble(txtOtherAmount.Text) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    txtRecivable.Text = Convert.ToDouble((Convert.ToDouble(txtNetAmnt.Text)) - (Convert.ToDouble(txtPrevRcptt.Text))).ToString("N2");
                    
                }
                else if (hidGstType.Value == "2")
                {
                    if (hidRequiredShortageGST.Value == "1")
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtT_SGST.Text) + Convert.ToDouble(txtT_CGST.Text) + Convert.ToDouble(txtT_IGST.Text) + Convert.ToDouble(txtOtherAmount.Text) - (Convert.ToDouble(txtShortageAmnt.Text)) - (Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text)) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    else
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtT_SGST.Text) + Convert.ToDouble(txtT_CGST.Text) + Convert.ToDouble(txtT_IGST.Text) + Convert.ToDouble(txtOtherAmount.Text) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    
                    txtRecivable.Text = Convert.ToDouble((Convert.ToDouble(txtNetAmnt.Text)) - (Convert.ToDouble(txtPrevRcptt.Text))).ToString("N2");
                    
                }
                else
                {
                    if (hidRequiredShortageGST.Value == "1")
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtTrServTax.Text) + Convert.ToDouble(txtTrSwchBrtTax.Text) + Convert.ToDouble(txtKisanTaxTrnptr.Text) + Convert.ToDouble(txtOtherAmount.Text) - (Convert.ToDouble(txtShortageAmnt.Text)) - (Convert.ToDouble(txtShortageGSTAmnt.Text == "" ? "0" : txtShortageGSTAmnt.Text)) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    else
                    {
                        dNetAmnt = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtTrServTax.Text) + Convert.ToDouble(txtTrSwchBrtTax.Text) + Convert.ToDouble(txtKisanTaxTrnptr.Text) + Convert.ToDouble(txtOtherAmount.Text) + HQChargesAmount + dExtraAmnt - dChlnAmntToLess));
                        txtNetAmnt.Text = Convert.ToString(Math.Round(dNetAmnt));
                        iNetAmnt = Convert.ToInt64(dNetAmnt);
                        txtRoundOff.Text = Convert.ToDouble(iNetAmnt - dNetAmnt).ToString("N2");
                    }
                    
                    txtRecivable.Text = Convert.ToDouble((Convert.ToDouble(txtNetAmnt.Text)) - (Convert.ToDouble(txtPrevRcptt.Text))).ToString("N2");
                    
                }

                
            }
            catch (Exception Ex)
            { }
        }

        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataSource = lst;
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
        }

        private void Bind()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.selectSenderName();
            obj = null;
            if (lst.Count > 0)
            {
                ddlSenderName.DataSource = lst;
                ddlSenderName.DataTextField = "Acnt_Name";
                ddlSenderName.DataValueField = "Acnt_Idno";
                ddlSenderName.DataBind();
            }
            ddlSenderName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPrintLoc.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void SetDate()
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);

                }
            }
        }

        private void Populate(Int64 HeadId, string type)
        {
            LinkButton2.Visible = false;
            lnkBtnUpd.Visible = true;
            InvoiceDAL obj = new InvoiceDAL();
            tblInvGenHead chlnBookhead = obj.selectHead(HeadId);

            ddldateRange.SelectedValue = Convert.ToString(chlnBookhead.Year_Idno);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtinvoicNo.Text = Convert.ToString(chlnBookhead.Inv_No);
            txtInvPreIx.Text = Convert.ToString(chlnBookhead.Inv_prefix); ;
            txtDate.Text = Convert.ToDateTime(chlnBookhead.Inv_Date).ToString("dd-MM-yyyy");
            ddlSenderName.SelectedValue = Convert.ToString(chlnBookhead.Sendr_Idno);

            GetPartyDetails(Convert.ToInt64(chlnBookhead.Sendr_Idno));
            var lst = obj.SelectPricIdno(Convert.ToInt64(chlnBookhead.Sendr_Idno));
            if (lst != null && (Convert.ToInt32(lst.PComp_Idno) > 1))
            {
                hidePrintMultipal.Value = Convert.ToString(1);
                this.BindPrincLoc(Convert.ToInt64(lst.PComp_Idno));
            }
            else
            {
                hidePrintMultipal.Value = Convert.ToString(0);
            }
            txtBiltyCharges.Text = Convert.ToDouble(chlnBookhead.Bilty_Chrgs).ToString("N2");
            txtShortageAmnt.Text = Convert.ToDouble(chlnBookhead.Short_Amnt).ToString("N2");
            txtTrServTax.Text = Convert.ToDouble(chlnBookhead.TrServTax_Amnt).ToString("N2");
            txtCSServTax.Text = Convert.ToDouble(chlnBookhead.ConsignrServTax).ToString("N2");
            txtPlantAmount.Text = Convert.ToDouble(chlnBookhead.PlantAmount).ToString("N2");
            txtPortAmount.Text = Convert.ToDouble(chlnBookhead.PortAmount).ToString("N2");

            txtTrSwchBrtTax.Text = Convert.ToDouble(chlnBookhead.TrSwchBrtTax_Amnt).ToString("N2");
            txtCSSwchBrtTax.Text = Convert.ToDouble(chlnBookhead.ConsignrSwchBrtTax).ToString("N2");

            txtKisanTax.Text = Convert.ToDouble(chlnBookhead.ConsignrKisanTax_Amnt).ToString("N2");
            txtKisanTaxTrnptr.Text = Convert.ToDouble(chlnBookhead.TrKisanKalyanTax_Amnt).ToString("N2");

            txtPlantInDate.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Plant_InDate)) ? "" : Convert.ToDateTime(chlnBookhead.Plant_InDate).ToString("dd-MM-yyyy");
            txtPlantOutDate.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Plant_OutDate)) ? "" : Convert.ToDateTime(chlnBookhead.Plant_OutDate).ToString("dd-MM-yyyy");
            txtPortinDate.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Port_InDate)) ? "" : Convert.ToDateTime(chlnBookhead.Port_InDate).ToString("dd-MM-yyyy");
            txtPortoutDate.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Port_OutDate)) ? "" : Convert.ToDateTime(chlnBookhead.Port_OutDate).ToString("dd-MM-yyyy");

            txtPlantDays.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.PlantDays)) ? "0" : Convert.ToString(chlnBookhead.PlantDays);
            txtPortDays.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.PortDays)) ? "0" : Convert.ToString(chlnBookhead.PortDays);

            txtHQCharges.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.HQCharges_Amnt)) ? "0.00" : Convert.ToString(chlnBookhead.HQCharges_Amnt);
            txtAddFreight.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.AddCharges_Amnt)) ? "0.00" : Convert.ToString(chlnBookhead.AddCharges_Amnt);

            txtOtherCharge1.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Charges1_Name)) ? "" : Convert.ToString(chlnBookhead.Charges1_Name);
            txtOtherCharge2.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Charges2_Name)) ? "" : Convert.ToString(chlnBookhead.Charges2_Name);

            txtOtherChargesAmount1.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Charges1_Amnt)) ? "" : Convert.ToString(chlnBookhead.Charges1_Amnt);
            txtOtherChargesAmount2.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Charges2_Amnt)) ? "" : Convert.ToString(chlnBookhead.Charges2_Amnt);

            txtGrosstotal.Text = Convert.ToDouble(chlnBookhead.GrossTot_Amnt).ToString("N2");
            txtNetAmnt.Text = Convert.ToDouble(chlnBookhead.Net_Amnt).ToString("N2");
            txtRoundOff.Text = Convert.ToDouble(chlnBookhead.RoundOff_Amnt).ToString("N2");
            ddlFromCity.SelectedValue = Convert.ToString(chlnBookhead.BaseCity_Idno);
            Session["fromcity"] = ddlFromCity.SelectedItem.Text;
            ddlFromCity.Enabled = false;
            ddlPrintLoc.Text = Convert.ToString(chlnBookhead.Print_Format);
            HidTbbRate.Value = Convert.ToString(chlnBookhead.TBB_Rate);
            DtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId, type);
            double RcpttAmnt = obj.SelectUpdateRcptAmnt(HeadId, ApplicationFunction.ConnectionString());
            txtPrevRcptt.Text = RcpttAmnt.ToString("N2");
            hidGstType.Value = Convert.ToString(DtTemp.Rows[0]["GST_Idno"] == null ? "0" : DtTemp.Rows[0]["GST_Idno"]);
            ddlGrType.SelectedValue = Convert.ToString(chlnBookhead.Gr_Type);
            tblUserPref obj1 = obj.SelectUserPref();
            hideUserPref.Value = Convert.ToString(obj1.InvPrint_Type);
            //#GST
            txtT_SGST.Text = Convert.ToDouble(chlnBookhead.TrSGST_Amt).ToString("N2");
            txtT_CGST.Text = Convert.ToDouble(chlnBookhead.TrCGST_Amt).ToString("N2");
            txtT_IGST.Text = Convert.ToDouble(chlnBookhead.TrIGST_Amt).ToString("N2");
            txtC_SGST.Text = Convert.ToDouble(chlnBookhead.ConSGST_Amt).ToString("N2");
            txtC_CGST.Text = Convert.ToDouble(chlnBookhead.ConCGST_Amt).ToString("N2");
            txtC_IGST.Text = Convert.ToDouble(chlnBookhead.ConIGST_Amt).ToString("N2");
            txtShortageGSTAmnt.Text = Convert.ToDouble(chlnBookhead.ShtgGST_Amt == null ? 0 : chlnBookhead.ShtgGST_Amt).ToString("N2");
            txtDelvAddress.Text = Convert.ToString(chlnBookhead.Delivery_Add);
            obj = null;
            ViewState["dt"] = DtTemp;
            this.BindGrid();
            netamntcal();
            lnkbtnSearch.Visible = false;
            if (Convert.ToInt32(hidPrintType.Value) == 1)
            {
                PrintInvGeneral(HeadId);
            }
            else if (Convert.ToInt32(hidPrintType.Value) == 4)
            {
                PrintInvoiceJainBulk(HeadId);
            }
            else
            {
                PrintInvoice(HeadId);
            }
        }

        private void PrintGCA()
        {
            if (Convert.ToInt32(hidePrintMultipal.Value) == 1)
            {
                string lst1 = "";
                BindDropdownDAL objDal = new BindDropdownDAL();
                if (printFormat1.Value != "" && printFormat1.Value != "0")
                {
                    lst1 = objDal.SelectPages(Convert.ToInt64(printFormat1.Value));

                    objDal = null;
                    if (string.IsNullOrEmpty(lst1) == false)
                    {
                        string url = lst1 + "?q=" + Convert.ToInt64(iMaxInvIdno1.Value) + "&P=" + Convert.ToInt64(ddlPrintLoc.SelectedValue) + "&R=" + Convert.ToString(ddlPrintwith.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }

                }
                else
                {
                    lst1 = objDal.SelectPages(Convert.ToInt64(ddlPrintLoc.SelectedValue));

                    objDal = null;
                    if (string.IsNullOrEmpty(lst1) == false)
                    {
                        string Value = Request.QueryString["q"];
                        string[] Array = Value.Split(new char[] { '-' });
                        string ID = Array[0].ToString();
                        string Type = Array[1].ToString();
                        string url = lst1 + "?q=" + Convert.ToInt64(ID) + "&P=" + Convert.ToInt64(ddlPrintLoc.SelectedValue) + "&S=" + Type + "&R=" + Convert.ToString(ddlPrintwith.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                }




            }
        }
        private void Clear()
        {
            ddlFromCity.Enabled = true;
            ddlSenderName.SelectedValue = "0";
            ViewState["dt"] = null;
            DtTemp = null;
            hidid.Value = string.Empty;
            txtinvoicNo.Text = "";
            BindGrid();
            txtGrosstotal.Text = "0.00";
            txtTrServTax.Text = "0.00"; txtCSServTax.Text = "0.00";
            txtNetAmnt.Text = "0.00";
            txtShortageAmnt.Text = "0.00"; txtOtherAmount.Text = "0.00";
            txtBiltyCharges.Text = "0.00"; txtPrevRcptt.Text = "0.00"; txtRecivable.Text = "0.00"; txtRoundOff.Text = "0.00";
            ddldateRange.Enabled = true;
            //ddldateRange.SelectedIndex = 0;
            lnkbtnSearch.Visible = true;
            ddlSenderName.Enabled = true;
            tblUserPref obj = objInvoiceDAL.SelectUserPref();
            ddlFromCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
            HidTbbRate.Value = Convert.ToString(obj.TBB_Rate);
            txtPlantInDate.Text = txtPlantOutDate.Text = txtPortinDate.Text = txtPortoutDate.Text = "";
            ImgPrint1.Visible = false; imgPrint.Visible = false; ImgPrint2.Visible = false; ImgPrint3.Visible = false; ImgPrintMultipal.Visible = false; DivPrintFormat.Visible = false;
            if (ddlFromCity.SelectedIndex > 0)
            {
                txtinvoicNo.Text = Convert.ToString(objInvoiceDAL.SelectMaxInvNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), txtInvPreIx.Text));
            }
            LinkButton2.Visible = true;
            lnkBtnUpd.Visible = false;
            txtDelvAddress.Text = string.Empty;
        }

        private void BindGrid()
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt != null && dt.Rows.Count > 0)
            {
                grdMain.Visible = true;
                grdMain.DataSource = dt;
                grdMain.DataBind();
                if (Request.QueryString["q"] == null)
                {
                    grdMain.Columns[0].Visible = false;
                }
                else if (Request.QueryString["q"] != null && grdMain.Rows.Count <=1)
                {
                    grdMain.Columns[0].Visible = false;
                }
                else
                {
                    grdMain.Columns[0].Visible = true;
                }

            }
            else
            {
                grdMain.Visible = false;
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }

        private void BindNullGird()
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            //ViewState["dt"] = null;
            //DtTemp = null;
        }
        private void GetPlantAmount(DateTime PlantIndate, DateTime PlantOutdate)
        {
            TimeSpan t = PlantOutdate - PlantIndate;
            double NrOfDays = t.TotalDays;
            if (NrOfDays > 0)
            {
                InvoiceDAL obj = new InvoiceDAL();
                AcntMast am = obj.SelectAcnt(Convert.ToInt64(ddlSenderName.SelectedValue));
                if (am != null)
                {
                    double TotalPlantAmnt = NrOfDays * Convert.ToDouble(am.detenPlant_charg);
                    txtPlantAmount.Text = TotalPlantAmnt.ToString("N2");
                }
                else
                    txtPlantAmount.Text = Convert.ToDouble(0).ToString("N2");
                txtPlantDays.Text = Convert.ToString(t.TotalDays);
            }
            else
                txtPlantDays.Text = "0";
            netamntcal();
        }
        private void GetPortAmount(DateTime PortIndate, DateTime PortOutdate)
        {
            TimeSpan t = PortOutdate - PortIndate;
            double NrOfDays = t.TotalDays;
            if (NrOfDays > 0)
            {
                InvoiceDAL obj = new InvoiceDAL();
                AcntMast am = obj.SelectAcnt(Convert.ToInt64(ddlSenderName.SelectedValue));
                if (am != null)
                {
                    double TotalPortAmnt = NrOfDays * Convert.ToDouble(am.detenPort_charg);
                    txtPortAmount.Text = TotalPortAmnt.ToString("N2");
                }
                else
                    txtPlantAmount.Text = Convert.ToDouble(0).ToString("N2");
                txtPortDays.Text = Convert.ToString(t.TotalDays);
            }
            else
                txtPortDays.Text = "0";
            netamntcal();
        }
        private void GetPlantAmount2(DateTime PlantIndate, DateTime PlantOutdate)
        {
            TimeSpan t = PlantOutdate - PlantIndate;
            double NrOfDays = t.TotalDays;
            if (NrOfDays > 0)
            {
                InvoiceDAL obj = new InvoiceDAL();
                AcntMast am = obj.SelectAcnt(Convert.ToInt64(ddlSenderName.SelectedValue));
                if (am != null)
                {
                    double TotalPlantAmnt = NrOfDays * Convert.ToDouble(am.detenPlant_charg);
                    txtPlantAmount2.Text = TotalPlantAmnt.ToString("N2");
                }
                else
                    txtPlantAmount2.Text = Convert.ToDouble(0).ToString("N2");
                txtPlantDays2.Text = Convert.ToString(t.TotalDays);
            }
            else
                txtPlantDays2.Text = "0";
            netamntcal();
        }
        private void GetPortAmount2(DateTime PortIndate, DateTime PortOutdate)
        {
            TimeSpan t = PortOutdate - PortIndate;
            double NrOfDays = t.TotalDays;
            if (NrOfDays > 0)
            {
                InvoiceDAL obj = new InvoiceDAL();
                AcntMast am = obj.SelectAcnt(Convert.ToInt64(ddlSenderName.SelectedValue));
                if (am != null)
                {
                    double TotalPortAmnt = NrOfDays * Convert.ToDouble(am.detenPort_charg);
                    txtPortAmount2.Text = TotalPortAmnt.ToString("N2");
                }
                else
                    txtPlantAmount2.Text = Convert.ToDouble(0).ToString("N2");
                txtPortDays2.Text = Convert.ToString(t.TotalDays);
            }
            else
                txtPortDays2.Text = "0";
            netamntcal();
        }
        #endregion
        #region Control Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdremove" && Request.QueryString["q"] == null)
            {
                DataTable objDataTable = CreateGRDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["Gr_Idno"]));
                    if (id != ridd)
                    {
                        //ApplicationFunction.DatatableAddRow(objDataTable, rw["Gr_Idno"], rw["Item_Idno"], rw["Item_Rate"], rw["Amount"], rw["Unit_Idno"], rw["Wayges"],
                        //                                        rw["Net_Amnt"], rw["Other_Amnt"], rw["ServTax_Amnt"], rw["ServTax_Perc"], rw["ServTax_Valid"], rw["SwchBrtTax_Amnt"], rw["KisanKalyan_Amnt"], rw["SGST_Amt"], rw["CGST_Amt"], rw["IGST_Amt"]
                        //   
                        ApplicationFunction.DatatableAddRow(objDataTable,
                            rw["GR_No"], rw["GR_Date"], rw["Ref_No"], rw["Delvry_Place"], rw["Item_Name"], rw["Unit"],
                                                              rw["ItemRate_type"], rw["Qty"], rw["Weight"], rw["Rate"], rw["Amount"],
                                                              rw["STax_Typ"], rw["Wages_Amnt"], rw["SGST_Amt"], rw["CGST_Amt"], rw["IGST_Amt"], "", "",
                                                              rw["ServTax_Amnt"], rw["SwchBrtTax_Amt"], rw["KisanKalyan_Amnt"], rw["GrTypeId"], rw["Net_Amnt"],
                                                              rw["GR_Idno"], rw["Item_Idno"], rw["Unit_Idno"], rw["Other_Amnt"],
                                                             rw["shortage"], rw["shortage_Amount"], rw["Shrtg_Limit"], rw["Shrtg_Rate"], rw["Shortage_Qty"],
                                                              rw["Gr_Typ"], rw["TBB_RATE"], rw["GrType"], rw["ServTax_Perc"], rw["SwchBrtTax_Perc"], rw["KisanTax_Perc"], rw["ServTax_Valid"], rw["CityVia_Name"], rw["GTypeID"], rw["TypeAmnt"],
                                                              rw["Rate_Type"], rw["SGST_Per"], rw["CGST_Per"], rw["IGST_Per"], rw["GST_Idno"]
                                                          );
                      
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                BindGrid();
                netamntcal();
            }
            else if (e.CommandName == "cmdremove" && Request.QueryString["q"] != null)
            {
                DataTable objDataTable = CreateGRListDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["Gr_Idno"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable,
                          rw["GST_Idno"], rw["GR_No"], rw["GR_Date"], rw["Ref_No"], rw["Delvry_Place"], rw["Item_Name"],
                                                            rw["Unit"], rw["Qty"], rw["Weight"], rw["GrTypeId"], rw["Rate"],
                                                            rw["Amount"], rw["Wages_Amnt"], rw["Net_Amnt"], rw["Other_Amnt"], rw["GR_Idno"],
                                                            rw["Item_Idno"], rw["Unit_Idno"], rw["STax_Typ"], rw["ServTax_Amnt"], rw["SwchBrtTax_Amt"],
                                                            rw["TBB_RATE"], rw["KisanKalyan_Amnt"], rw["ServTax_Perc"], rw["SwchBrtTax_Perc"],
                                                           rw["KisanTax_Perc"], rw["ServTax_Valid"], rw["SGST_Amt"], rw["CGST_Amt"], rw["IGST_Amt"],
                                                            rw["SGST_Per"], rw["CGST_Per"], rw["IGST_Per"], rw["CityVia_Name"]
                                                        );
                    }
                    else
                    {
                        if (dtTemp.Rows.Count > 0)
                        {
                            InvoiceDAL inv = new InvoiceDAL();
                            var lst = inv.selectGRHead(Convert.ToInt64(ridd));
                            inv = null;
                            if (lst.GR_Typ == 2)
                            {
                                foreach (DataRow Dr in dtTemp.Rows)
                                {
                                    Int32 GrIdno = Convert.ToInt32(Dr["GR_Idno"]);
                                    InvoiceDAL obj = new InvoiceDAL();
                                    var value = obj.Updatebilled(GrIdno);
                                    if (value > 0 )
                                    {
                                        ShowMessage("Row Delete successfully"); 
                                    }
                                //    TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                   // lst.Billed = false;
                                   // db.SaveChanges();
                                }
                            }
                            else
                            {
                                
                            }
                        }
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                BindGrid();
                netamntcal();
            }
        }
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt = 0; double Qty = 0, Rate = 0, Amount = 0, Wayges = 0, OtherAmnt = 0, SwchBrtTaxNet = 0, SGSTNet = 0, CGSTNet = 0, IGSTNet = 0, KisanTaxNet = 0, ServTaxNet = 0, NetAmnt = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (string.IsNullOrEmpty(Convert.ToString(hidrefno.Value)) == true)
                //{
                //    e.Row.Cells[17].Text = "Ref No.";
                //}
                //else
                //{
                //    e.Row.Cells[17].Text = hidrefno.Value;
                //}

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                Qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                lblQty = Qty + lblQty;
                Rate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Rate"))) ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                lblRate = Qty + lblRate;
                Amount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                lbllAmount = Amount + lbllAmount;

                SwchBrtTaxNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SwchBrtTax_Amt"));
                lblSwchBrtNet = lblSwchBrtNet + SwchBrtTaxNet;

                KisanTaxNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "KisanKalyan_Amnt"));
                lblKisanNet = lblKisanNet + KisanTaxNet;

                SGSTNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SGST_Amt"));
                lblSGSTNet = lblSGSTNet + SGSTNet;

                CGSTNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CGST_Amt"));
                lblCGSTNet = lblCGSTNet + CGSTNet;

                IGSTNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "IGST_Amt"));
                lblIGSTNet = lblIGSTNet + IGSTNet;

                ServTaxNet = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ServTax_Amnt"));
                lblServNet = lblServNet + ServTaxNet;

                NetAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                lblNetAmount = lblNetAmount + NetAmnt;


                dTotWeight = dTotWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight"));
                Wayges = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Wages_Amnt"));
                lblWayges = Wayges + lblWayges;
                OtherAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Other_Amnt"));
                lblOtherAmnt = OtherAmnt + lblOtherAmnt;
                HiddenField hidTbbType = (HiddenField)e.Row.FindControl("hidTbbType");
                TextBox txtWagesAmnt = (TextBox)e.Row.FindControl("txtWagesAmnt");
                TextBox txtOtherAmnt = (TextBox)e.Row.FindControl("txtOtherAmnt");
                HiddenField hidCurGrIdno = (HiddenField)e.Row.FindControl("hidGrIdno");
                txtWagesAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtOtherAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                if (Convert.ToBoolean(hidTbbType.Value) == true)
                {
                    txtOtherAmnt.Enabled = false;
                    txtWagesAmnt.Enabled = false;
                }
                else if (e.Row.RowIndex != 0)
                {
                    GridViewRow prevRow = this.grdMain.Rows[e.Row.RowIndex - 1];
                    HiddenField hidPrevGrIdno = (HiddenField)prevRow.FindControl("hidGrIdno");
                    if (Convert.ToString(hidPrevGrIdno.Value) == Convert.ToString(hidCurGrIdno.Value) && Convert.ToBoolean(hidTbbType.Value) == false)
                    {
                        txtOtherAmnt.Enabled = false;
                        txtWagesAmnt.Enabled = false;
                    }
                    else
                    {
                        txtOtherAmnt.Enabled = true;
                        txtWagesAmnt.Enabled = true;
                    }
                }
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblQtyTot = (Label)e.Row.FindControl("lblTotQty");
                lblQtyTot.Text = lblQty.ToString("N2");
                //  Label lblRateTot = (Label)e.Row.FindControl("lblTotRate");
                //  lblRateTot.Text = lblRate.ToString("N2");
                Label lblTotAmount = (Label)e.Row.FindControl("lblTotAmount");
                lblTotAmount.Text = lbllAmount.ToString("N2");
                Label lblTotWeight = (Label)e.Row.FindControl("lblTotWeight");
                lblTotWeight.Text = dTotWeight.ToString();

                Label lblSwchBrtTaxAmntNet = (Label)e.Row.FindControl("lblSwchBrtTaxAmntNet");
                lblSwchBrtTaxAmntNet.Text = lblSwchBrtNet.ToString("N2");

                Label lblKisanTaxAmntNet = (Label)e.Row.FindControl("lblKisanTaxAmntNet");
                lblKisanTaxAmntNet.Text = lblKisanNet.ToString("N2");

                Label lblServTaxAmntNet = (Label)e.Row.FindControl("lblServTaxAmntNet");
                lblServTaxAmntNet.Text = lblServNet.ToString("N2");

                Label lblSGSTAmntNet = (Label)e.Row.FindControl("lblSGSTAmntNet");
                lblSGSTAmntNet.Text = lblSGSTNet.ToString("N2");
                Label lblCGSTAmntNet = (Label)e.Row.FindControl("lblCGSTAmntNet");
                lblCGSTAmntNet.Text = lblCGSTNet.ToString("N2");
                Label lblIGSTAmntNet = (Label)e.Row.FindControl("lblIGSTAmntNet");
                lblIGSTAmntNet.Text = lblIGSTNet.ToString("N2");

                Label lblNetAmnt = (Label)e.Row.FindControl("lblTotNetAmnt");
                lblNetAmnt.Text = lblNetAmount.ToString("N2");
                txtGrosstotal.Text = dblNetAmnt.ToString("N2");
            }
        }

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }

            ddldateRange.Focus();
        }
        protected void txtOtherAmnt_TextChanged(object sender, EventArgs e)
        {
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            TextBox txtOtherAmnt = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtOtherAmnt.Parent.Parent;
            TextBox txtOTAmnt = (TextBox)currentRow.FindControl("txtOtherAmnt");
            Label lblNetAmnt = (Label)currentRow.FindControl("lblNetAmnt");
            TextBox txtWagesAmnt = (TextBox)currentRow.FindControl("txtWagesAmnt");
            HiddenField hidServTaxPerc = (HiddenField)currentRow.FindControl("hidServTaxPerc");
            HiddenField hidServTaxValid = (HiddenField)currentRow.FindControl("hidServTaxValid");
            Label lblServTaxAmnt = (Label)currentRow.FindControl("lblServTaxAmnt");
            HiddenField hidSwchBrtTaxPerc = (HiddenField)currentRow.FindControl("hidSwchBrtTaxPerc");
            Label lblSwchBrtTaxAmnt = (Label)currentRow.FindControl("lblSwchBrtTaxAmnt");
            Label lblAmount = (Label)currentRow.FindControl("lblAmount");
            if (txtOTAmnt.Text == "")
            {
                txtOTAmnt.Text = "0.00";
            }
            else
            {
                txtOTAmnt.Text = Convert.ToDouble(txtOTAmnt.Text).ToString("N2");
            }
            lblNetAmnt.Text = Convert.ToDouble(Convert.ToDouble(lblAmount.Text) + Convert.ToDouble(txtOTAmnt.Text) + Convert.ToDouble(txtWagesAmnt.Text)).ToString("N2");
            grdMain.FooterRow.Cells[12].Text = lblNetAmnt.Text;
            Int32 prtyidno = 0; bool bServTaxExmpt = false; dTotServTaxTrnsportr = 0; dTotServTaxConsgn = 0; int GrIdno = 0;
            prtyidno = Convert.ToInt32(Convert.ToString(ddlSenderName.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSenderName.SelectedValue));
            bServTaxExmpt = objInvoiceDAL.selectServTaxExmpt(prtyidno);
            if (((bServTaxExmpt) == false))
            {
                if (Convert.ToDouble(lblNetAmnt.Text.Trim()) <= (Convert.ToDouble(hidServTaxValid.Value)))
                {
                    lblServTaxAmnt.Text = "0.00";
                }
                else
                {
                    lblServTaxAmnt.Text = Convert.ToDouble(((Convert.ToDouble(lblNetAmnt.Text.Trim()) * (Convert.ToDouble(hidServTaxPerc.Value))) / 100)).ToString("N2");
                    grdMain.FooterRow.Cells[13].Text = lblServTaxAmnt.Text;

                    lblSwchBrtTaxAmnt.Text = Convert.ToDouble(((Convert.ToDouble(lblNetAmnt.Text.Trim()) * (Convert.ToDouble(hidSwchBrtTaxPerc.Value))) / 100)).ToString("N2");
                    grdMain.FooterRow.Cells[14].Text = lblSwchBrtTaxAmnt.Text;
                }
            }
            else
            {
                lblServTaxAmnt.Text = "0.00";
            }
            netamntcal();
        }
        protected void txtWagesAmnt_TextChanged(object sender, EventArgs e)
        {
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            TextBox txtWagesAmnt = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtWagesAmnt.Parent.Parent;
            TextBox txtOTAmnt = (TextBox)currentRow.FindControl("txtOtherAmnt");
            Label lblNetAmnt = (Label)currentRow.FindControl("lblNetAmnt");

            TextBox txtWsAmnt = (TextBox)currentRow.FindControl("txtWagesAmnt");
            HiddenField hidServTaxPerc = (HiddenField)currentRow.FindControl("hidServTaxPerc");
            HiddenField hidServTaxValid = (HiddenField)currentRow.FindControl("hidServTaxValid");
            Label lblServTaxAmnt = (Label)currentRow.FindControl("lblServTaxAmnt");
            HiddenField hidSwchBrtTaxPerc = (HiddenField)currentRow.FindControl("hidSwchBrtTaxPerc");
            Label lblSwchBrtTaxAmnt = (Label)currentRow.FindControl("lblSwchBrtTaxAmnt");
            Label lblAmount = (Label)currentRow.FindControl("lblAmount");

            if (txtWsAmnt.Text == "")
            {
                txtWsAmnt.Text = "0.00";
            }
            else
            {
                txtWsAmnt.Text = Convert.ToDouble(txtWsAmnt.Text).ToString("N2");
            }
            lblNetAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtOTAmnt.Text) + Convert.ToDouble(lblAmount.Text) + Convert.ToDouble(txtWsAmnt.Text)).ToString("N2");
            grdMain.FooterRow.Cells[12].Text = lblNetAmnt.Text;
            Int32 prtyidno = 0; bool bServTaxExmpt = false; dTotServTaxTrnsportr = 0; dTotServTaxConsgn = 0; int GrIdno = 0;
            prtyidno = Convert.ToInt32(Convert.ToString(ddlSenderName.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlSenderName.SelectedValue));
            bServTaxExmpt = objInvoiceDAL.selectServTaxExmpt(prtyidno);
            if (((bServTaxExmpt) == false))
            {
                if (Convert.ToDouble(lblNetAmnt.Text.Trim()) <= (Convert.ToDouble(hidServTaxValid.Value)))
                {
                    lblServTaxAmnt.Text = "0.00";
                    lblSwchBrtTaxAmnt.Text = "0.00";
                }
                else
                {
                    lblServTaxAmnt.Text = Convert.ToDouble(((Convert.ToDouble(lblNetAmnt.Text.Trim()) * (Convert.ToDouble(hidServTaxPerc.Value))) / 100)).ToString("N2");
                    grdMain.FooterRow.Cells[13].Text = lblServTaxAmnt.Text;

                    lblSwchBrtTaxAmnt.Text = Convert.ToDouble(((Convert.ToDouble(lblNetAmnt.Text.Trim()) * (Convert.ToDouble(hidSwchBrtTaxPerc.Value))) / 100)).ToString("N2");
                    grdMain.FooterRow.Cells[14].Text = lblSwchBrtTaxAmnt.Text;
                }
            }
            else
            {
                lblServTaxAmnt.Text = "0.00";
            }
            netamntcal();
        }
        protected void txtInvPreIx_TextChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                InvoiceDAL objInvoiceDAL = new InvoiceDAL();
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtinvoicNo.Text = Convert.ToString(objInvoiceDAL.SelectMaxInvNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), txtInvPreIx.Text));
                }
            }
            txtinvoicNo.Focus();
        }
        protected void txtBiltyCharges_TextChanged(object sender, EventArgs e)
        {
            if (txtBiltyCharges.Text == "")
            {
                txtBiltyCharges.Text = "0.00";
            }
            txtBiltyCharges.Text = Convert.ToDouble(txtBiltyCharges.Text).ToString("N2");
            netamntcal();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dTotReptWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dTotUnloading += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                dTotNetAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));
                dTotShortage += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage"));
                dTotReptServTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "servTax_Amnt"));
                dTotSwatchTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                dTotKisanTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanTax_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lbltotalWeight.Text = dTotReptWeight.ToString("N2");
                lblAmount.Text = dtotAmnt.ToString("N2");
                lblUnloading.Text = dTotUnloading.ToString("N2");
                lblNetTotAmnt.Text = dTotNetAmnt.ToString("N2");
                lblShtg.Text = dTotShortage.ToString("N2");
                lblTotServTax.Text = dTotReptServTax.ToString("N2");
                lblTotSBTax.Text = dTotSwatchTax.ToString("N2");
                lblKisanTax.Text = dTotKisanTax.ToString("N2");
                // lblShtg.Text=AcntLinkDS]
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Label lblshort = (Label)e.Item.FindControl("lblShort");
                Label lblrefno = (Label)e.Item.FindControl("lblrefno");
                if (string.IsNullOrEmpty(Convert.ToString(hidrefno.Value)) == true)
                {
                    lblrefno.Text = "Ref No.";
                }
                else
                { lblrefno.Text = hidrefno.Value; }




                if (drpPrintType.SelectedValue == "2")
                {
                    lblshort.Visible = false;
                }
                else
                {
                    lblshort.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                totqty += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Qty"));
                totprintweight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                totprintshortage += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage"));
                printtotamnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Label lblValueShort = (Label)e.Item.FindControl("lblValueShort");
                if (drpPrintType.SelectedValue == "2")
                {
                    lblValueShort.Visible = false;
                }
                else
                {
                    lblValueShort.Visible = true;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lbltotalqty.Text = Convert.ToString(totqty);
                lblweight.Text = totprintweight.ToString();
                lblshortage.Text = totprintshortage.ToString("N2");
                lblTotalAmnt.Text = printtotamnt.ToString("N2");

                if (drpPrintType.SelectedValue == "2")
                {
                    lblshortage.Visible = false;
                }
                else
                {
                    lblshortage.Visible = true;
                }
            }
        }
        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                txtGrosstotal.Text = "0.00";
                txtTrServTax.Text = "0.00"; txtCSServTax.Text = "0.00";
                txtNetAmnt.Text = "0.00";
                txtShortageAmnt.Text = "0.00"; txtOtherAmount.Text = "0.00";
                txtBiltyCharges.Text = "0.00"; txtPrevRcptt.Text = "0.00"; txtRecivable.Text = "0.00"; txtRoundOff.Text = "0.00";
                InvoiceDAL objInvoiceDAL = new InvoiceDAL();
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtinvoicNo.Text = Convert.ToString(objInvoiceDAL.SelectMaxInvNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), txtInvPreIx.Text));
                }
                Session["fromcity"] = ddlFromCity.SelectedItem.Text;
            }
            ddlFromCity.Focus();
        }
        #endregion
        #region Print....
        private void PrintInvoiceJainBulk(Int64 HeadIdno)
        {
            chkbit = 1;
            DataSet dsReport = null;
            if (Convert.ToInt32(hidPrintType.Value) == 4)
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='JainBulkPrint',@Id='" + HeadIdno + "'");
            }

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                if (dsReport.Tables[1].Rows.Count > 1)
                {
                    Repeater obj = new Repeater();
                    hidjainbulk.Value = "1";
                    double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
                    string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
                    int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
                    DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
                    # region Company Details
                    CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
                    Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
                    Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
                    //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
                    PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                    City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
                    State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
                    //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
                    Serv_No = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
                    FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
                    PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
                    lblCompName2.Text = CompName; lblCompname4.Text = "For - " + CompName;
                    lblCompAdd3.Text = Add1;

                    // lblTin2.Text = TinNo.ToString();
                    lblCompAdd4.Text = Add2;
                    lblCompCity2.Text = City;
                    lblCompState2.Text = State;
                    lblCompPhNo2.Text = PhNo;
                    //lbltxtPanNo1.Text = "PAN No : "+PanNo.ToString();
                    if (FaxNo == "")
                    {
                        lblFaxNo2.Visible = false; lblFaxNo.Visible = false;
                    }
                    else
                    {
                        lblFaxNo2.Text = FaxNo;
                        lblFaxNo2.Visible = true;
                    }
                    if (Serv_No == "")
                    {
                        lblCompTIN2.Visible = false;
                    }
                    else
                    {
                        lblCompTIN2.Text = Serv_No;
                        lblCompTIN2.Visible = true;
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

                    lblIvNo.Text = Convert.ToString(txtInvPreIx.Text) + Convert.ToString(txtinvoicNo.Text);
                    lblIvDt.Text = (txtDate.Text);
                    //lblPrtyName.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
                    lblConsigeeName.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Receiver"]);
                    lblConsigneeAddress.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Recriver Address"]);
                    lblConsigneeTin.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Receiver Tin"]);

                    lblConsignorName.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Sender"]);
                    lblConsignorAddress.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Sender Address"]);
                    lblConsignorTin.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Sender Tin"]);
                    Repeater3.DataSource = dsReport.Tables[1];
                    Repeater3.DataBind();
                    valuelblnetAmnt.Text = string.Format("{0:0,0.00}", (txtNetAmnt.Text));
                    //lblShotageAmnt.Text = string.Format("{0:0,0.00}", (txtShortageAmnt.Text));
                    //lblOtherTotAmnt.Text = string.Format("{0:0,0.00}", (txtOtherAmount.Text));
                    double txtfinl = Convert.ToDouble(valuelblnetAmnt.Text);
                    string[] str1 = txtfinl.ToString().Split('.');
                    string numtoint = NumberToText(Convert.ToInt32(str1[0])) + " Only.";
                    lblwordsVal.Text = numtoint;
                    Loadimage();
                    #endregion
                }
                else
                {
                    PrintSingleGRInvoiceJainBulk(HeadIdno);
                }
            }
        }
        private void PrintInvGeneral(Int64 GRHeadIdno)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname1.Text = CompName; lblCompname1.Text = "For - " + CompName;
            lblCompAdd11.Text = Add1;
            lblCompAdd22.Text = Add2;
            lblCompCity1.Text = City;
            lblCompState1.Text = State;
            lblCompPhNo1.Text = PhNo;
            if (FaxNo == "")
            {
                lblCompFaxNo1.Visible = false; lblFaxNo1.Visible = false;
            }
            else
            {
                lblCompFaxNo1.Text = FaxNo;
                lblCompFaxNo1.Visible = true; lblFaxNo1.Visible = true;
            }
            if (ServTaxNo == "")
            {
                lblCompTIN1.Visible = false; lblTin1.Visible = false;
            }
            else
            {
                lblCompTIN1.Text = ServTaxNo;
                lblCompTIN1.Visible = true; lblTin1.Visible = true;
            }
            Loadimage();

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrintGeneral',@Id='" + GRHeadIdno + "'");
            // valuelbltxtprevamnt.Text = (txtPrevRcptt.Text.Trim()=="" ? "0.00" :  Convert .ToDouble(txtPrevRcptt.Text.Trim()).ToString("N2"));
            // lblprintshortage.Text = String.Format("{0:0.00}", Convert.ToDouble(txtShortageAmnt.Text.Trim()));
            valuelbltxtprevamnt.Text = String.Format("{0:0.00}", Convert.ToDouble(txtPrevRcptt.Text.Trim()));
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                valuelbltxtPartyName.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);

                valuelblCtax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrServTax"]);
                valuelblStax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrServTax_Amnt"]);
                valuelblCSwachtax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrSwchBrtTax"]);
                valuelblSwachtax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrSwchBrtTax_Amnt"]);
                valuelblCKisantax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrKisanTax_Amnt"]);
                valueKisantax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrKisanKalyanTax_Amnt"]);
                //#GST Values
                prntTSGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrSGST_Amt"]);
                prntTCGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrCGST_Amt"]);
                prntTIGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["TrIGST_Amt"]);
                prntCSGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConSGST_Amt"]);
                prntCCGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConCGST_Amt"]);
                prntCIGST.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConIGST_Amt"]);

                valuelblnetamntAtbttm.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["NetAmnt"]);
            }
            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["GR_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");

                Repeater2.DataSource = dsReport.Tables[1];
                Repeater2.DataBind();
                if (drpPrintType.SelectedValue == "2")
                {
                    valuelblnetamntAtbttm.Text = lblTotalAmnt.Text.ToString();
                }
                // For shipment details & container details by lokesh

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[1].Rows[0]["Shipment_No"])) == true)
                {
                    lblNameShipmentno.Visible = false; lblShipmentNo.Visible = false;
                }
                else { lblNameShipmentno.Visible = true; lblShipmentNo.Visible = true; lblShipmentNo.Text = dsReport.Tables[1].Rows[0]["Shipment_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[1].Rows[0]["GRContanr_No"])) == true)
                {
                    lblNameContnrNo.Visible = false; lblContainerNo.Visible = false;
                }
                else { lblNameContnrNo.Visible = true; lblContainerNo.Visible = true; lblContainerNo.Text = dsReport.Tables[1].Rows[0]["GRContanr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[1].Rows[0]["GRContanr_Size"])) == true || Convert.ToInt32(dsReport.Tables[1].Rows[0]["GRContanr_Size"]) == 0)
                {
                    lblNameCntnrSize.Visible = false; lblContainerSize.Visible = false;
                }
                else
                { lblNameCntnrSize.Visible = true; lblContainerSize.Visible = true; lblContainerSize.Text = dsReport.Tables[1].Rows[0]["GRContanr_Size"].ToString(); }


                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[1].Rows[0]["GRContanr_Type"])) == true || Convert.ToInt32(dsReport.Tables[1].Rows[0]["GRContanr_Type"]) == 0)
                {
                    lblNameContnrType.Visible = false; lblCntnrType.Visible = false;
                }
                else
                { lblNameContnrType.Visible = true; lblCntnrType.Visible = true; lblCntnrType.Text = dsReport.Tables[1].Rows[0]["GRContanr_Type"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[1].Rows[0]["GRContanr_SealNo"])) == true)
                {
                    lblNameSealNo.Visible = false; lblSealNo.Visible = false;
                }
                else { lblNameSealNo.Visible = true; lblSealNo.Visible = true; lblSealNo.Text = dsReport.Tables[1].Rows[0]["GRContanr_SealNo"].ToString(); }

                //.........................

            }
            if (dsReport != null && dsReport.Tables[2].Rows.Count > 0)
            {
            }
        }
        private void PrintSingleGRInvoiceJainBulk(Int64 HeadIdno)
        {
            chkbit = 2;
            Repeater obj = new Repeater();
            hidjainbulk.Value = "0";
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //Serv_No = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            Serv_No = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname5.Text = CompName; lblcompname5.Text = "For - " + CompName;
            lblCompAdd5.Text = "Adm.Office :" + Add1;

            // lblTin2.Text = TinNo.ToString();
            lblCompAdd6.Text = Add2;
            lblCompCity5.Text = City;
            lblCompState5.Text = State;
            lblCompPhNo5.Text = PhNo;
            //lbltxtPanNo1.Text = "PAN No : "+PanNo.ToString();
            if (FaxNo == "")
            {
                lblFaxNo5.Visible = false; lblFaxNo5.Visible = false;
            }
            else
            {
                lblCompFaxNo5.Text = FaxNo;
                lblCompFaxNo5.Visible = true;
            }
            if (Serv_No == "")
            {
                lblCompTIN5.Visible = false;
            }
            else
            {
                lblCompTIN5.Text = Serv_No;
                lblCompTIN5.Visible = true;
            }
            if (PanNo == "")
            {
                lbltxtPanNo5.Visible = false; lblPanNo5.Visible = false;
            }
            else
            {
                lblPanNo5.Text = PanNo;
                lbltxtPanNo5.Visible = true; lblPanNo5.Visible = true;
            }

            #endregion
            //DataSet CodeNo = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name from AcntMast A Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno  WHERE acnt_name like '%shree cement%' AND I.Inv_Idno='" + HeadIdno + "' ");
            //if (CodeNo != null && CodeNo.Tables[0].Rows.Count > 0)
            //{
            //    lblcodeno.Visible = true;
            //    lblvaluecodeno.Visible = true;
            //    lblvaluecodeno.Text = "T0073";
            //}
            DataSet dsReport = null;
            if (Convert.ToInt32(hidPrintType.Value) == 4)
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='JainBulkPrint',@Id='" + HeadIdno + "'");
            }

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                lblInvNo5.Text = Convert.ToString(txtInvPreIx.Text) + Convert.ToString(txtinvoicNo.Text);
                lblInvdate5.Text = (txtDate.Text);
                //lblPrtyName.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);

                lblLorryNo.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["LORRY_NO"]);
                lblDispDate.Text = Convert.ToString(Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Dispatch_Date"]).ToString("dd-MM-yyyy"));
                lblGRDate5.Text = Convert.ToString(Convert.ToDateTime(dsReport.Tables[1].Rows[0]["GR_Date"]).ToString("dd-MM-yyyy"));
                lblFromCity5.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                //lblFromCity6.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                //lblToCity6.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["To_City"]);
                lblOtherChrgs.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["OtherAmnt"]);
                lblToCity5.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["To_City"]);
                lblDelPl.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["City_Via"]);
                lblContSize.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Cont_Size"]);
                lblGRNo5.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["GR_No"]);
                lblContainerNo5.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["GRContanr_No"]);
                lblRefNo.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Ref_No"]);
                if (lblContainerNo5.Text == "")
                {
                    lblContainerNo5.Visible = false;
                }
                else
                {
                    lblContainerNo5.Visible = true;
                }
                if (lblContSize.Text == "")
                {
                    lblContSize.Visible = false;
                }
                else
                {
                    lblContSize.Visible = true;
                }

                for (int count = 0; count < dsReport.Tables[1].Rows.Count; count++)
                {
                    lbltotalweightain.Text += Convert.ToString(dsReport.Tables[1].Rows[count]["Weight"]);
                }

                ///container Charges
                double dbcontainerchage = 0;
                if (Convert.ToDouble(dsReport.Tables[0].Rows[0]["Container_Charge"]) > 0)
                {
                    dbcontainerchage = Convert.ToDouble(dsReport.Tables[0].Rows[0]["Container_Charge"]);
                    lblOtherChrgs.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["Container_Charge"]));
                    lblcontainercharges.Visible = true;
                }
                else
                {
                    dbcontainerchage = 0;
                    lblOtherChrgs.Text = "";
                    lblcontainercharges.Visible = false;
                }

                /////////////INVOICE DETAILS
                double TotalPlantAmnt = 0;
                int PlantDiffDays = 0; int PortDiffDays = 0;
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_InDate"]))) == false && (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_OutDate"])) == false))
                {
                    double NrOfDays = 0;

                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_InDate"])) == false)
                    {
                        if (Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_InDate"]).ToString("dd-MM-yyyy") == "01-01-1900")
                            lblPrPlantInDate.Text = "";
                        else
                            lblPrPlantInDate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_InDate"]).ToString("dd-MM-yyyy");
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_OutDate"])) == false)
                    {
                        if (Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_OutDate"]).ToString("dd-MM-yyyy") == "01-01-1900")
                            lblPrPlantOutDate.Text = "";
                        else
                            lblPrPlantOutDate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_OutDate"]).ToString("dd-MM-yyyy");
                    }
                    DateTime d1 = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_InDate"]);
                    DateTime d2 = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Plant_OutDate"]);

                    TimeSpan t = d2 - d1;
                    NrOfDays = t.TotalDays;
                    if (NrOfDays > 0)
                    {
                        lblPrPlantRate.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_Days"])) ? "0" : Convert.ToString(dsReport.Tables[0].Rows[0]["Plant_Days"]) + "Days";
                        TotalPlantAmnt = (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["PlantAmount"])) ? 0 : Convert.ToDouble(dsReport.Tables[0].Rows[0]["PlantAmount"]));
                        lblTotalPlantAmnt.Text = TotalPlantAmnt.ToString("N2");
                    }
                }
                if (Convert.ToString(dsReport.Tables[0].Rows[0]["Charges1_Name"]) == "")
                    lblCharge1.Text = "E)&nbsp;&nbsp;&nbsp;&nbsp;.....................................................................................";
                else
                {
                    lblCharge1.Text = "E) " + Convert.ToString(dsReport.Tables[0].Rows[0]["Charges1_Name"]);
                    lblCharges1Amnt.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["Charges1_Amnt"]));
                }
                if (Convert.ToString(dsReport.Tables[0].Rows[0]["Charges2_Name"]) == "")
                    lblCharge1.Text = "F)&nbsp;&nbsp;&nbsp;&nbsp;.....................................................................................";
                else
                {
                    lblCharge2.Text = "F) " + Convert.ToString(dsReport.Tables[0].Rows[0]["Charges2_Name"]);
                    lblCharges2Amnt.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["Charges2_Amnt"]));
                }
                lblOtherChargesAmnt.Text = string.Format("{0:0,0.00}", (dsReport.Tables[1].Rows[0]["Other_Amnt"]));
                lblBilityCharges.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["Bilty_Chrgs"]));
                lblAddCharges.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["AddFreight_Amnt"]));
                lblHQCharges.Text = string.Format("{0:0,0.00}", (dsReport.Tables[0].Rows[0]["HQCharges_Amnt"]));
                lblSender.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Acnt_Name"]);
                lblCity.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["CityName"]);
                lblState.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["StateName"]);
                double TotalPortAmnt = 0;
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Port_InDate"])) == false) && (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Port_OutDate"])) == false))
                {
                    double NrOfDays = 0;

                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Port_InDate"])) == false)
                    {
                        if (Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_InDate"]).ToString("dd-MM-yyyy") == "01-01-1900")
                            lblPrPortInDate.Text = "";
                        else
                            lblPrPortInDate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_InDate"]).ToString("dd-MM-yyyy");
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Port_OutDate"])) == false)
                    {
                        if (Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_OutDate"]).ToString("dd-MM-yyyy") == "01-01-1900")
                            lblPrPortOutDate.Text = "";
                        else
                            lblPrPortOutDate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_OutDate"]).ToString("dd-MM-yyyy");
                    }
                    DateTime d1 = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_InDate"]);
                    DateTime d2 = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Port_OutDate"]);

                    TimeSpan t = d2 - d1;
                    NrOfDays = t.TotalDays;
                    if (NrOfDays > 0)
                    {
                        lblPrPortRate.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Port_Days"])) ? "0" : Convert.ToString(dsReport.Tables[0].Rows[0]["Port_Days"]) + "Days";
                        TotalPortAmnt = (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["PortAmount"])) ? 0 : Convert.ToDouble(dsReport.Tables[0].Rows[0]["PortAmount"]));
                        lblTotalPortAmnt.Text = TotalPortAmnt.ToString("N2");
                    }
                }

                lblAmnt5.Text = string.Format("{0:0,0.00}", (dsReport.Tables[1].Rows[0]["Amount"]));
                lblNetAmnt5.Text = string.Format("{0:0,0.00}", (dsReport.Tables[1].Rows[0]["Net_Amnt"]));
                double nettotal = Convert.ToDouble(lblNetAmnt5.Text) + dbcontainerchage + TotalPlantAmnt + TotalPortAmnt + (Convert.ToString(lblCharges1Amnt.Text.Trim()) == "" ? 0 : Convert.ToDouble(lblCharges1Amnt.Text.Trim())) + (Convert.ToString(lblCharges2Amnt.Text.Trim()) == "" ? 0 : Convert.ToDouble(lblCharges2Amnt.Text.Trim())) + (Convert.ToString(lblAddCharges.Text.Trim()) == "" ? 0 : Convert.ToDouble(lblAddCharges.Text.Trim())) + (Convert.ToString(lblHQCharges.Text.Trim()) == "" ? 0 : Convert.ToDouble(lblHQCharges.Text.Trim())) + (Convert.ToString(lblBilityCharges.Text.Trim()) == "" ? 0 : Convert.ToDouble(lblBilityCharges.Text.Trim()));
                lblNetAmnt5.Text = Convert.ToString(nettotal);
                double txtfinl = nettotal;
                string[] str1 = txtfinl.ToString().Split('.');
                string numtoint = NumberToText(Convert.ToInt32(str1[0])) + " Only.";
                lblAmntWords.Text = numtoint;
            }
            Loadimage();
        }
        private void PrintInvoice()
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];
                string[] Array = Value.Split(new char[] { '-' });
                string ID = Array[0].ToString();
                string Type = Array[1].ToString();
                string url = "PrintSaleInvoice.aspx" + "?q=" + Convert.ToInt64(ID) + "&P=" + Convert.ToInt64(ddlPrintLoc.SelectedValue) + "&S=" + ddlPage.SelectedValue;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
        }
        private void PrintOM()
        {
            InvoiceDAL objDAL = new InvoiceDAL();

            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];
                string[] Array = Value.Split(new char[] { '-' });
                string ID = Array[0].ToString();
                string Type = Array[1].ToString();
                string url = "PrintOmInvoice.aspx" + "?q=" + Convert.ToInt64(ID) + "&P=" + Convert.ToInt64(ddlPrintLoc.SelectedValue) + "&S=" + ddlPage.SelectedValue;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else
            {
                string url = "PrintOmInvoice.aspx" + "?q=" + Convert.ToInt64(iMaxInvIdno) + "&P=" + Convert.ToInt64(ddlPrintLoc.SelectedValue) + "&S=" + ddlPage.SelectedValue;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }




        }
        #endregion
        #region Repeater....
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 

                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dTotReptWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DelvQty"));
                dQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dShorAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ShortageAmnt"));
                dTotOther += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "OtherAmnt"));
                dTotNetAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblAmount1.Text = dtotAmnt.ToString("N2");
                lblQty3.Text = dQty.ToString("N2");
                lblDelQty3.Text = dTotReptWeight.ToString("N2");
                lblFshor.Text = dShorAmnt.ToString("N2");
                lblother.Text = dTotOther.ToString("N2");
                lblNetTotAmnt1.Text = dTotNetAmnt.ToString("N2");
                // lblShtg.Text=AcntLinkDS]
            }
        }
        #endregion
        #region Control Function....
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
        public void GetPreferences()
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            var data = objDAL.GetUserPrefDetail();
            chkRequiredShortageGST.Checked = data.ReqShtgGST;
            hidRequiredShortageGST.Value = ((data.ReqShtgGST == null ? true : data.ReqShtgGST) == true ? "1" : "0");
        }
        private void GetPartyAmount()
        {
            InvoiceDAL obj = new InvoiceDAL();
            AcntMast am = obj.SelectAcnt(Convert.ToInt64(ddlSenderName.SelectedValue));
            if (am != null)
            {
                hidPlantPortCharge.Value = (am.detenPlant_charg == null ? 0 : am.detenPlant_charg).ToString(); ;
            }
        }
        #endregion
        #region Event.........
        protected void ddlSenderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.SelectPricIdno(Convert.ToInt64(ddlSenderName.SelectedValue));
            obj = null;
            if (lst != null && (Convert.ToInt32(lst.PComp_Idno) > 1))
            {
                BindDropdownDAL obj1 = new BindDropdownDAL();
                var lst1 = obj1.BindPrincLocByGSTDate(Convert.ToInt64(lst.PComp_Idno), IsGST());
                obj1 = null;
                if (lst1.Count > 0)
                {
                    ddlPrintLoc.DataSource = lst1;
                    ddlPrintLoc.DataTextField = "PCompLoc_Name";
                    ddlPrintLoc.DataValueField = "PCompLoc_Idno";
                    ddlPrintLoc.DataBind();
                }
                DivPrintFormat.Visible = true;
            }
            else
            {
                ddlPrintLoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                DivPrintFormat.Visible = false;
            }
            txtPlantInDate.Text = string.Empty;
            txtPlantOutDate.Text = string.Empty;
            txtPortinDate.Text = string.Empty;
            txtPortoutDate.Text = string.Empty;
            GetPartyAmount();

        }
        protected void drpPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(HidGrId.Value), Convert.ToString(HidGrType.Value));
            }
        }
        protected void txtPlantInDate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false && string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate.Text.Trim())))
                {
                    ShowMessageErr("Plant In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPlantAmount(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash((txtPlantOutDate.Text.Trim()))));
                netamntcal();
            }
        }
        protected void txtPlantOutDate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlantInDate.Text.Trim()) == false && string.IsNullOrEmpty(txtPlantOutDate.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash((txtPlantInDate.Text.Trim()))) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate.Text.Trim())))
                {
                    ShowMessageErr("Plant In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPlantAmount(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate.Text.Trim())));
                netamntcal();
            }

        }
        protected void txtPortinDate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false && string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate.Text.Trim())))
                {
                    ShowMessageErr("Port In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPortAmount(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate.Text.Trim())));
                netamntcal();
            }
        }
        protected void txtPortoutDate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPortinDate.Text.Trim()) == false && string.IsNullOrEmpty(txtPortoutDate.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate.Text.Trim())))
                {
                    ShowMessageErr("Port In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPortAmount(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate.Text.Trim())));
                netamntcal();
            }
        }
        protected void txtPlantinDate2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false && string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate2.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate2.Text.Trim())))
                {
                    ShowMessageErr("Plant In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPlantAmount2(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate2.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash((txtPlantOutDate2.Text.Trim()))));
                netamntcal();
            }
        }
        protected void txtPlantoutDate2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlantInDate2.Text.Trim()) == false && string.IsNullOrEmpty(txtPlantOutDate2.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash((txtPlantInDate2.Text.Trim()))) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate2.Text.Trim())))
                {
                    ShowMessageErr("Plant In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPlantAmount2(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantInDate2.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPlantOutDate2.Text.Trim())));
                netamntcal();
            }

        }
        protected void txtPortinDate2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false && string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate2.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate2.Text.Trim())))
                {
                    ShowMessageErr("Port In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPortAmount2(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate2.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate2.Text.Trim())));
                netamntcal();
            }
        }
        protected void txtPortOutDate2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPortinDate2.Text.Trim()) == false && string.IsNullOrEmpty(txtPortoutDate2.Text.Trim()) == false)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate2.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate2.Text.Trim())))
                {
                    ShowMessageErr("Port In Date should be less than Out date!");
                    return;
                }
                if (Convert.ToInt64(ddlSenderName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please Select Pary Name!");
                    return;
                }
                GetPortAmount2(Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortinDate2.Text.Trim())), Convert.ToDateTime(ApplicationFunction.mmddyyyyDash(txtPortoutDate2.Text.Trim())));
                netamntcal();
            }
        }

        protected void txtPlantAmount_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }

        protected void txtPortAmount_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        #endregion
        #region Print Button........
        protected void lnkBtnSaveUserPref_OnClick(object sender, EventArgs e)
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            tblUserPref UsrPref = new tblUserPref();
            UsrPref.ReqShtgGST = chkRequiredShortageGST.Checked;
            int ReturnValue = objDAL.SaveUserPrefDetail(UsrPref);
            if (ReturnValue == 1)
            {
                GetPreferences();
                netamntcal();
                ShowMessage("User preference have been saved successfully.");
            }
            else
            {
                ShowMessageErr("Something went wrong, please contact your administer.");
            }
        }
        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0")
            {
                ShowMessageErr("Please Select From City for Last Print.");
            }
            else
            {
                BindDropdownDAL objCom = new BindDropdownDAL();
                InvoiceDAL objDAL = new InvoiceDAL();
                Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
                iMaxInvIdno1.Value = Convert.ToString(iMaxInvIdno);
                tblUserPref obj1 = objDAL.SelectUserPref();
                hideUserPref.Value = Convert.ToString(obj1.InvPrint_Type);
                if (iMaxInvIdno > 0)
                {
                    if (Convert.ToInt32(hidPrintType.Value) == 1)
                    {
                        Int64 senderidno = objDAL.sender(ApplicationFunction.ConnectionString(), iMaxInvIdno);
                        Int64 printFormat = objDAL.printFormat(ApplicationFunction.ConnectionString(), iMaxInvIdno);

                        printFormat1.Value = Convert.ToString(printFormat);
                        if (Convert.ToInt32(printFormat) > 0)
                        {
                            hidePrintMultipal.Value = Convert.ToString(1);
                            string lst1 = objCom.SelectPages(Convert.ToInt64(printFormat));
                            objCom = null;
                            if (string.IsNullOrEmpty(lst1) == false)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Printwith();", true);
                                //string url = lst1 + "?q=" + iMaxInvIdno + "&P=" + Convert.ToInt64(printFormat);
                                //string fullURL = "window.open('" + url + "', '_blank' );";
                                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                            }
                        }
                        else
                        {
                            PrintInvGeneral(iMaxInvIdno);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print1')", true);
                        }
                    }
                   else if (Convert.ToInt32(hidPrintType.Value) == 7)
                    {
                        Int64 Pcomidno = objDAL.lastprint(ApplicationFunction.ConnectionString(), iMaxInvIdno);
                        if (Convert.ToInt64(Pcomidno) == 8)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DivBirla();", true);
                        }
                       else if (Convert.ToInt64(Pcomidno) == 9)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DivPJL();", true);
                        }
                        else
                        {
                            PrintReport1(Convert.ToString(iMaxInvIdno));
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('StandrdPrint')", true);
                        }
                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 6)
                    {
                        string url = "PrintSaleInvoice.aspx" + "?q=" + Convert.ToInt64(string.IsNullOrEmpty(iMaxInvIdno1.Value) ? "0" :iMaxInvIdno1.Value) + "&P=1"; //+ Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }

                    else if (Convert.ToInt32(hidPrintType.Value) == 4)
                    {
                        PrintInvoiceJainBulk(iMaxInvIdno);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('Jainprint')", true);
                    }
                    else if (Convert.ToInt32(hidPrintType.Value) == 5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
                    }
                    else if (Convert.ToInt32(obj1.GRPrintPref) == 6)
                    {
                        string url = "PrintSaleInvoice.aspx" + "?q=" + Convert.ToInt64(string.IsNullOrEmpty(iMaxInvIdno1.Value) ? "0" : iMaxInvIdno1.Value) + "&P=1"; //+ Convert.ToInt64(ddlPage.SelectedValue);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else
                    {
                        PrintInvoice(iMaxInvIdno);
                        hidid.Value = iMaxInvIdno.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                    }

                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
        }
        protected void lnkBillInvoice_click(object sender, EventArgs e)
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            string url = "BirlaBillInvoice.aspx" + "?q=" + Convert.ToInt64(iMaxInvIdno) + "";
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkTaxInvoice_Click(object sender, EventArgs e)
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            string url = "TaxInvoiceBirla.aspx" + "?q=" + Convert.ToInt64(iMaxInvIdno) + "";
            string fullURL = "window.open('" + url + "', '_blank' );";
           ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void lnkPJLBillInvoice_click(object sender, EventArgs e)
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            string url = "PJLBillInvoice.aspx" + "?q=" + Convert.ToInt64(iMaxInvIdno) + "";
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
       
        protected void lnkPJLTaxInvoice_Click(object sender, EventArgs e)
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = 0;
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];

                string[] Array = Value.Split(new char[] { '-' });

                string ID = Array[0].ToString();
                iMaxInvIdno = string.IsNullOrEmpty(ID) ? 0 : Convert.ToInt64(ID);
            }
            else
                iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            string url = "PJLTaxInvoice.aspx" + "?q=" + Convert.ToInt64(iMaxInvIdno) + "";
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        protected void ImgPrintMultipal_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Printwith();", true);

        }
        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void lnkinvoicePrint_Click(object sender, EventArgs e)
        {
            this.PrintInvoice();
        }
        
        protected void lnkPrint_click(object sender, EventArgs e)
        {
            this.PrintOM();
        }
        protected void lnkbtnPrintwith_click(object sender, EventArgs e)
        {
            this.PrintGCA();
        }
        #endregion
        #region GST....
        //Upadhyay #GST
        public int GetGSTType()
        {
            if (txtDate.Text != "")
            {
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                {
                    Int64 billPrtyIdno = 0;
                    if (Convert.ToString(ddlSenderName.SelectedValue) != "" && ddlSenderName.SelectedValue != "0")
                    {
                        billPrtyIdno = Convert.ToInt32(ddlSenderName.SelectedValue);
                        GetPartyDetails(billPrtyIdno);

                        string compStateName = GetCompStateIdno();
                        Int64 partyStateIdno = Convert.ToInt64(hidPartyStateIdno.Value);
                        string partyStateName = hidPartyStateName.Value;
                        if (partyStateName.Trim().ToLower() == compStateName.Trim().ToLower())
                            hidGstType.Value = "1";
                        else
                            hidGstType.Value = "2";
                        return Convert.ToInt32(hidGstType.Value);
                    }
                    else if (Convert.ToString(ddlSenderName.SelectedValue) != "" && ddlSenderName.SelectedValue != "0")
                    {
                        billPrtyIdno = Convert.ToInt32(ddlSenderName.SelectedValue);
                        GetPartyDetails(billPrtyIdno);
                        string compStateName = GetCompStateIdno();
                        Int64 partyStateIdno = Convert.ToInt64(hidPartyStateIdno.Value);
                        string partyStateName = hidPartyStateName.Value;
                        if (partyStateName.Trim().ToLower() == compStateName.Trim().ToLower())
                            hidGstType.Value = "1";
                        else
                            hidGstType.Value = "2";
                        return Convert.ToInt32(hidGstType.Value);
                    }
                }
            }
            return 0;
        }

        private void GetPartyDetails(long billPrtyIdno)
        {
            hidPartyStateIdno.Value = "";
            hidPartyStateName.Value = "";
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var partyDetails = (from s in db.tblStateMasters
                                    join a in db.AcntMasts on s.State_Idno equals a.State_Idno
                                    where a.Acnt_Idno == billPrtyIdno
                                    select s).SingleOrDefault();
                hidPartyStateIdno.Value = String.IsNullOrEmpty(partyDetails.State_Idno.ToString()) ? "0" : partyDetails.State_Idno.ToString();
                hidPartyStateName.Value = String.IsNullOrEmpty(partyDetails.State_Name.ToString()) ? "0" : partyDetails.State_Name.ToString();
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

        public bool IsGST()
        {
            string dt = GetGSTDate();
            if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
            return true; 
            return false;
        }
        #endregion
        #region Posting A/C...............
        //private bool PostIntoAccounts(Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno)
        private bool PostIntoAccounts(Int64 intDocIdno, string strDocType, string strInvNo, string strInvDate, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, double dOtherAmnt, double dTotAmnt, double dSrvTaxAmmnt, double GrossAmnt, double dSwachhCess, double dKrishiCess, Int64 intPartyIdno, Int32 intYearIdno, double SGSTAmount, double CGSTAmount, double IGSTAmount)
        {
            #region Variables Declaration...


            hidpostingmsg.Value = string.Empty;
            Int64 intVchrIdno = 0, intValue = 0, intDocNo = 0; DateTime? dtBankDate = null;
            Int32 TrAcntIdno = 0, ICAcnt_Idno = 0, IOTAcnt_Idno = 0, ISAcnt_Idno = 0, SwachBharat_Idno = 0, KrishiKalyan_Idno = 0, SGST_Idno = 0, CGST_Idno = 0, IGST_Idno = 0;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            InvoiceDAL obj = new InvoiceDAL();
            AcntLinkDS = obj.DtAcntDS(ApplicationFunction.ConnectionString());
            DsTransAcnt = obj.DsTrAcnt(ApplicationFunction.ConnectionString());
            #endregion

            #region Account link Validations...
            if (AcntLinkDS == null || AcntLinkDS.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Account link is not defined. Kindly define.";
                return false;
            }
            ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["CAcnt_Idno"]));
            IOTAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["OTAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["OTAcnt_Idno"]));
            ISAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["SAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["SAcnt_Idno"]));
            SwachBharat_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["SwachBharat_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["SwachBharat_Idno"]));
            KrishiKalyan_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["KrishiKalyan_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["KrishiKalyan_Idno"]));
            SGST_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["Sgst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["Sgst_Idno"]));
            CGST_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["Cgst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["Cgst_Idno"]));
            IGST_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["Igst_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["Igst_Idno"]));
            if (IOTAcnt_Idno <= 0)
            {
                hidpostingmsg.Value = "Other Account is not defined. Kindly define.";
                return false;
            }
            if (ISAcnt_Idno <= 0)
            {
                hidpostingmsg.Value = "Service Tax Account is not defined. Kindly define.";
                return false;
            }
            if (SwachBharat_Idno <= 0)
            {
                hidpostingmsg.Value = "Swachh Bharat Cess Account is not defined. Kindly define.";
                return false;
            }

            if (KrishiKalyan_Idno <= 0)
            {
                hidpostingmsg.Value = "Krishi Kalyan Cess Account is not defined. Kindly define.";
                return false;
            }
            if (SGST_Idno <= 0)
            {
                hidpostingmsg.Value = "SGST Account is not defined. Kindly define.";
              
                return false;
            }

            if (CGST_Idno <= 0)
            {
                hidpostingmsg.Value = "CGST Account is not defined. Kindly define.";
          
                return false;
            }

            if (IGST_Idno <= 0)
            {
                hidpostingmsg.Value = "IGST Account is not defined. Kindly define.";
               
                return false;
            }
            #endregion


            #region To Sender Posting Start ...

            if (DsTransAcnt == null || DsTransAcnt.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                return false;
            }
            else
            {
                TrAcntIdno = Convert.ToInt32(DsTransAcnt.Rows[0]["TransportAccountID"]);
            }

            if (Request.QueryString["q"] == null)
            {
                intValue = 1;
            }
            else
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
            }
            if (intValue > 0)
            {
                Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, intYearIdno);
                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(strInvDate)),
                4,
                0,
                "Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                true,
                0,
                strDocType,
                0,
                0,
                Convert.ToInt64(strInvNo),
                ApplicationFunction.GetIndianDateTime().Date,
                VchrNo,
                0,
                intYearIdno,
                0, intUserIdno);
                if (intValue > 0)
                {
                    #region Sender Account Posting ...

                    intVchrIdno = intValue;
                    intValue = 0; /*Insert In VchrDetl*/
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    Convert.ToInt64(intPartyIdno),
                    " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                    dTotAmnt,
                    Convert.ToByte(2),
                    Convert.ToByte(0),
                    "",
                    true,
                    null,  //please check here if date is Blank
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
                               " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
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
                        if (dOtherAmnt > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                               intVchrIdno,
                               Convert.ToInt32(IOTAcnt_Idno),
                              " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                              Convert.ToDouble(Math.Abs(dOtherAmnt)),
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
                        if (dOtherAmnt < 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                               intVchrIdno,
                               Convert.ToInt32(intPartyIdno),
                              " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                              Convert.ToDouble(Math.Abs(dOtherAmnt)),
                               Convert.ToByte(2),
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
                        
                        if (dSrvTaxAmmnt > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                              intVchrIdno,
                              Convert.ToInt32(ISAcnt_Idno),
                              " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                              Convert.ToDouble(Math.Abs(dSrvTaxAmmnt)),
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
                        if (dSwachhCess > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                    intVchrIdno,
                                    Convert.ToInt32(SwachBharat_Idno),
                                   " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                                    Convert.ToDouble(Math.Abs(dSwachhCess)),
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
                        if (dKrishiCess > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                    intVchrIdno,
                                    Convert.ToInt32(KrishiKalyan_Idno),
                                    " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                                    Convert.ToDouble(Math.Abs(dKrishiCess)),
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
                                  " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
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
                                    " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
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
                                    " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
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
                                hidpostingmsg.Value = "Invoice. has amount in roundoff, but Round Off Account is not defined. Kindly define.";
                                return false;
                            }
                            intValue = objclsAccountPosting.InsertInVchrHead(
                            Convert.ToDateTime(ApplicationFunction.mmddyyyy(strInvDate)),
                            4,
                            0,
                           " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            0,
                            Convert.ToDateTime(ApplicationFunction.mmddyyyy(strInvDate)),
                            0,
                            0,
                            intYearIdno,
                            0, intUserIdno);
                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                intValue = 0;
                                for (int i = 0; i < 2; i++)
                                {
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        (i == 0 ? intPartyIdno : intRoundOffId),
                                        " Invoice. No: " + strInvNo + " Invoice. Date: " + strInvDate,
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


            #region Deallocate variables...

            objclsAccountPosting = null;

            return true;

            #endregion
        }

        #endregion
        #region Printing
       
        protected void lnkBtn1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];
                string[] Array = Value.Split(new char[] { '-' });
                string ID = Array[0].ToString();
                string Type = Array[1].ToString();
                PrintReport1(ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('StandrdPrint')", true);
            }
            
        }
           protected void lnkinvoicePrint_Click1(object sender, EventArgs e)
        {
            this.BirlaBILL();
        }
           protected void lnktaxinvoicePrint_Click(object sender, EventArgs e)
        {
            this.BirlaTax();
        }
          protected void lnkPrintTax_Click(object sender, EventArgs e)
        {
            this.PJLtaxinvoice();
        }
        protected void lnkPJLBillInvoice_Click(object sender, EventArgs e)
        {
            this.PJLBillinvoice();
        }
        protected void lnkBirlataxinvoicePrint_Click(object sender, EventArgs e)
        {
            this.BirlaTaxInvoice();
        }
        protected void lnkPJLBillSummary_Click(object sender, EventArgs e)
        {
            this.PJLBillSummary();
        }
           private void BirlaBILL()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "BirlaBillInvoice.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }
               
           }
           private void BirlaTax()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "TaxInvoiceBirla.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }
           }
           private void BirlaTaxInvoice()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "BirlaTaxInvoice.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }
           }
           private void PJLtaxinvoice()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "PJLTaxInvoice.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }

           }
           private void PJLBillinvoice()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "PJLBillInvoice.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }

           }
           private void PJLBillSummary()
           {
               if (Request.QueryString["q"] != null)
               {
                   string Value = Request.QueryString["q"];
                   string[] Array = Value.Split(new char[] { '-' });
                   string ID = Array[0].ToString();
                   string Type = Array[1].ToString();
                   string url = "PJLBillSummary.aspx" + "?q=" + ID + "";
                   string fullURL = "window.open('" + url + "', '_blank' );";
                   ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
               }
           }
           private void PrintReport1(String GRNo)
           {
               InvoiceDAL obj1 = new InvoiceDAL();
               String CityName = Session["fromcity"] as string;
               DtTemp = obj1.city(ApplicationFunction.ConnectionString(), CityName);

               string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = "";
               string GSTIN = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast A Left JOIN tblCITYMASTER CM On CM.city_idno = A.City_Idno Left join tblStateMaster SM ON SM.state_idno = A.State_Idno");
               //DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast A Left JOIN tblCITYMASTER CM On CM.city_idno=A.city_idno Left join tblStateMaster SM ON SM.state_idno=A.state_idno");
               Repeater obj = new Repeater();
               DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='printtaxinv',@Id='" + GRNo + "'");
               CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
               Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
               Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Address2"]);
			   //Add2 = Convert.ToString(DtTemp.Rows[0]["Address1"]) + "," + Convert.ToString(DtTemp.Rows[0]["Address2"]);
               PhNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Mobile_1"]);
               City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
               State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]) + "(CODE :-  " + Convert.ToString(dsReport.Tables[0].Rows[0]["GSTState_Code"]) + ")";
               TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
               FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
               PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
               GSTIN = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
               string Stat = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]);
               lblc.Text = CompName; //lblcomp.Text = "For - " + CompName;
               lblCA1.Text = Add1;

            if (string.IsNullOrEmpty(Add2) == false)
                lbladd2.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"]) + "," + Add2;
            else
                lbladd2.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"]);
            lblct.Text = City;
            lblstat.Text = State;
            lblmobile.Text = PhNo;
            lblgstin.Text = GSTIN;
            lblpan.Text = PanNo.ToString();
            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                Repeater4.DataSource = dsReport.Tables[1];
                Repeater4.DataBind();
            }
            lblbillno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
            lblShipingAdd.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Delvry_Place"]);
            lblbilldate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
            lblcontname.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
            lbladd1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address1"]);
            lblcadd2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address2"]);
            lblcity1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
            string acntst = lblst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
            lblgst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_GSTINNo"]);
            double total = Convert.ToDouble(lbltotal.Text);
            if (Stat == acntst)
            {
                double aftrsgst = (total * (sper / 100));
                double aftrcgst = (total * (cper / 100));
                lblsgst.Text = Convert.ToString(aftrsgst);
                lblcgst.Text = Convert.ToString(aftrcgst);
                lbligst.Text = "0";
                double tot = total + aftrsgst + aftrcgst;
                lbltobillamnt.Text = Convert.ToString(tot);
                double lsttotal = Convert.ToDouble(lbltobillamnt.Text);
                string[] str1 = lsttotal.ToString().Split('.');
                string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                lblwd.Text = numbertoent;
            }
            else
            {
                double aftrigst = (total * (iper / 100));
                lblsgst.Text = "0";
                lblcgst.Text = "0";
                lbligst.Text = Convert.ToString(aftrigst);
                double tot = total + aftrigst;
                lbltobillamnt.Text = Convert.ToString(tot);
                double lsttotal = Convert.ToDouble(lbltobillamnt.Text);
                string[] str1 = lsttotal.ToString().Split('.');
                string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                lblwd.Text = numbertoent;
            }
        }
        protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dtotA += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Dtotqty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lbltotal.Text = dtotA.ToString("N2");
                lbltotqty.Text = Dtotqty.ToString("N2");
            }
        }
        #endregion
    }
}