using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;

using WebTransport.DAL;

namespace WebTransport.Classes
{
    public class Pagebase : System.Web.UI.Page
    {
        #region Variables...
        public Int32 Comp_Dlr;
        public Int32 UW_Target;
        public Int32 PW_Target;
        public double Yearly_Target;
        public bool Paid_Service;
        public double Paid_Days;
        public bool Retail_Invoice;
        public bool Accounts;
        public bool Job_RegNo;
        public bool Cust_Adrs_CSD;
        public bool Bill_MRP;
        public bool Credit_Vat;
        public bool Duplicate_Engine;
        public bool Mtrl_Lube;
        public bool Disp_BDay;
        public bool ReprDir_Print;
        public bool VhclDir_Print;
        public bool PrintWrnt;
        public bool PrintOnSave;
        public bool Selection_box;
        public bool Rate_Change;
        public Int32 Taxes;
        public bool EnRequire;
        public bool NoPlateChrges;
        public bool GoodLifeAmnt;
        public bool ExWrntyAmnt;
        public bool FollowwithNo;
        public byte CashCrSeq;
        public byte JobCounterSeq;
        public bool Repr_PrePrint;
        public bool Job_Open;
        public bool Disc_Prcnt;
        public bool Gate_Pass;
        public bool Cash_Counter_Text;
        public bool RoundOff_Sale;
        public bool Repr_terms;
        public bool OutServ_Tax;
        public bool Allow_DMS;
        public bool RBill_Scheme;
        public bool DiscPerValue;
        public bool PrintRateTotal;
        public bool TaxAmnt;
        public bool Disc_OnMRP;
        public bool bVatReqd;
        public bool Prev_Visit;
        public bool CSTRep_Bill;
        public bool AdviceRequired;
        public bool Repr_Vat;
        public string Job_Cash_Prefix;
        public string Job_Credit_Prefix;
        public string Counter_Cash_Prefix;
        public string Counter_Credit_Prefix;
        public string Vehicle_Bill_Prefix;
        public string Vehicle_DBill_Prefix;
        public string Vehicle_SChlnS_Prefix;
        public string Vehicle_SChlnM_Prefix;
        public string Vehicle_SaleCSD_Prefix;
        public string SaleLetterPref_Prefix;
        public string Estimate_Prefix;
        public bool Comp_Adrs;
        public bool Sale_Letter_Six;
        public bool SBill_Scheme;
        public bool Letter_Head;
        public bool SBill_Sep;
        public bool Vehicle_CashCr;
        public bool Vehicle_ThruRcpt;
        public bool DueAmnt;
        public bool CSD_Sep;
        public byte SubDlrSaleOnExShPrice;
        public bool Hide_Other;
        public bool GatePass_VSale;
        public bool SendSMSVSaleBill;
        public bool VSB_SType;
        public bool Battery_No;
        public bool Service_Coupon;
        public bool Key_No;
        public bool Fin_Amnt;
        public bool Modl_Narr;
        public bool VhclBill_Rcpt_Print;
        public string TermsQT;
        public double HYP_Amnt;
        public bool Trade_Cert;
        public bool Vhcl_Disc;
        public bool PrintTin_SalLtr;
        public bool Form22;
        public bool Show_TempAdrs;
        public bool PrintAddTaxOnCSD;
        public bool OnRoad_Billing;
        public bool TempAdd_Caption;
        public bool VehCost_WthDisc;
        public bool TradeDiscInSSo;
        public bool E_Mail;
        public bool Sale_Narration;
        public Int32 State;
        public double Abatement;
        public double Cenvat;
        public double EduCess;
        public double ShEcess;
        public double FrIns;
        public double CST;
        public bool SMS_Msg;
        public bool Hindi_SMS;
        public bool SMS_Lang;
        public Int16 GoldCust_Criteria;
        public Int32 Visit_Criteria;
        public DateTime SaleDate_from;
        public DateTime SaleDate_To;
        public Int32 Rcpt_Print;
        public Int32 cmbBillPrntParam;
        public Int32 Tax_With;
        public Int32 Holding_Days;
        public Int32 SaleLtr_NoPrn;
        public Int32 Invoice_NoPrn;
        public Int32 Repr_NoPrn;
        public bool SepSeq_TokNo;
        public bool Seq_AccidentalJob;
        public bool Chasis_Tax;
        public bool VSBill_AfterPDI;
        public bool Print_PreAuthen;
        public bool PurCalc_Rqty;
        public bool ReCalcRepr_Visible;
        public bool ReprBill_SixInch;
        public bool Reg_Counter;
        public bool SurChrge;
        public bool Calc_TVS;
        public bool Printed_Stationary;
        public bool AdTax_Reqd;
        public bool SendSMSVSaleChl;
        public bool Chln_Conti;
        public bool Open_Ledger;
        public bool TinTrade_PrePrint;
        public bool Prv_Edi;
        public bool TCR_Com;
        public bool TCR_Rqd;
        public bool PrintCustNameinTempSL;
        public bool StckTrnNDP;
        public bool Clos_Pur;
        public bool Logo_Required;
        public bool BillHeader_Surbhi;
        public Int32 Job_Cash;
        public Int32 Job_Credit;
        public Int32 Counter_Cash;
        public Int32 Counter_Credit;
        public Int32 TIN_COUNTER;
        public Int32 RecEntryPrint;
        public Int32 Repr_PrintCriteria;
        public string TermsRecEntry;
        public bool PrintAddrIn_ReprCntr;
        public bool Hide_SLBillNo;
        public bool PrintRcpt_Amnt;
        public bool PurIn_SaleRate;
        public Int32 VSBill_SingleCash;
        public Int32 VSBill_SingleCredit;
        public Int32 VSBill_MultipleCash;
        public Int32 VSBill_MultipleCredit;
        public Int32 VSBill_CSD;
        public Int32 VSBill_CSDCredit;
        public Int32 VSBill_PrintCriteria;
        public Int32 Sale_Letter;
        public bool Lower_Tin;
        public bool Shwbillno;
        public bool Tradecert_OnLeft;
        public bool ShowVecNo_CrTemp;
        public bool PrintChallanAmnt;
        public bool Show_Month;
        public bool Pur_Round;
        public bool Allow_Neg_Stk;
        public bool Repr_Preprint;
        public int intYearIdno = 1;
        public int CompId = 1;
        public string UserName;
        public string email;
        public int VAT_Tax;
        public int UserIdno;
        public int DesignIdno;
        public int WorkArea;
        public int LocId;
        public int[] arrayLoc = new int[5];
        public int i = 0, j = 0;
        public int LocationTypeIdno;
        public int UserDateRng;
        public string LocationName;
        public bool RepeatHeader;
        public int VatReqd;
        public int Serv_Tax;
        public string CompName = "";
        public string CompPhone = "";
        public string strMsgUserName = "";
        public string strMsgPassword = "";
        public int? intSMStype = 0;
        public string strMsgSenderID = "";
        public string strProfileId = "";

        public DataTable DtTempStore = new DataTable();
        public DataTable DtTempWH = new DataTable();


        public int UserRgt_Idno;
        public int Form_Idno;
        public bool ADD;
        public bool Edit;
        public bool View;
        public bool Delete;
        public bool Print;
        public bool Jurisdiction;

        // for User Defualt credentials
        public Int64 UserFromCity;
        public Int64 GRTyp;
        public Int64 UserToCity;
        public Int64 UserState;
        public Int64 Sender;
        public Int32 CompLocationLimit;
        public Int64 Unit;
        public Int64 ItemName;
        #endregion

        #region Page Events...
        public Pagebase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoad(System.EventArgs e)
        {
            try
            {
                this.CheckUserPreferences();
                this.CheckUserCompanyDetl();
                this.ExistFromCityAndDateRang();
            }
            catch (Exception Ex)
            {
            }
            base.OnLoad(e);

            // call load of all pages
            //base.OnLoad(e);
        }
        #endregion

        #region Functions...
        public void ExistFromCityAndDateRang()
        {
            DAL.UserDefaultDAL UserDfltDAL = new DAL.UserDefaultDAL();
            var lst = UserDfltDAL.SelectExistRecord(Convert.ToInt64(Session["UserIdno"]));
            if (lst != null)
            {
                UserFromCity = Convert.ToString(lst.FromCity_idno) == "" ? 0 : Convert.ToInt64(lst.FromCity_idno);
                UserToCity = Convert.ToString(lst.CityId) == "" ? 0 : Convert.ToInt64(lst.CityId);
                UserState = Convert.ToString(lst.StateId) == "" ? 0 : Convert.ToInt64(lst.StateId);
                Sender = Convert.ToString(lst.SenderIdno) == "" ? 0 : Convert.ToInt64(lst.SenderIdno);
                Unit = Convert.ToString(lst.UnitIdno) == "" ? 0 : Convert.ToInt64(lst.UnitIdno);
                ItemName=Convert.ToString(lst.ItemIdno) == "" ? 0 : Convert.ToInt64(lst.ItemIdno);
                UserDateRng = Convert.ToString(lst.Year_idno) == "" ? 0 : Convert.ToInt32(lst.Year_idno);
                GRTyp = Convert.ToString(lst.Gr_Type) == "" ? 0 : Convert.ToInt32(lst.Gr_Type);
            }
        }
        public void AutoRedirect()
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                string str_Script = @"
                                <script type='text/javascript'> 
                                function Redirect()
                                {
                                alert('Your session has been expired and system redirects to login page now.!\n\n');
                                window.location.href='/Login.aspx'; 
                                }
                                </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);
                Response.Redirect("Login.aspx");
            }
            catch (Exception Ex)
            {
            }
        }
        public void CheckUserPreferences()
        {
            //Model.clsUserPreference objUserPref = new Model.clsUserPreference();
            //Model.UserPref objlist = objUserPref.SelectGenDataXML();

            //Comp_Dlr = Convert.ToInt32(objlist.Comp_Dlr);
            //UW_Target = Convert.ToInt32(objlist.UW_Target);
            //PW_Target = Convert.ToInt32(objlist.PW_Target);
            //Yearly_Target = Convert.ToDouble(objlist.Yearly_Target);
            //Paid_Service = Convert.ToBoolean(objlist.Paid_Service);
            //Paid_Days = Convert.ToDouble(objlist.Paid_Days);
            //Retail_Invoice = Convert.ToBoolean(objlist.Retail_Invoice);
            //Accounts = Convert.ToBoolean(objlist.Accounts);
            //Job_RegNo = Convert.ToBoolean(objlist.Job_RegNo);
            //RoundOff_Sale = Convert.ToBoolean(objlist.RoundOff_Sale);
            //Cust_Adrs_CSD = Convert.ToBoolean(objlist.Cust_Adrs_CSD);
            //Bill_MRP = Convert.ToBoolean(objlist.Bill_MRP);
            //Credit_Vat = Convert.ToBoolean(objlist.Credit_Vat);
            //Duplicate_Engine = Convert.ToBoolean(objlist.Duplicate_Engine);
            //Mtrl_Lube = Convert.ToBoolean(objlist.Mtrl_Lube);
            //Disp_BDay = Convert.ToBoolean(objlist.Disp_BDay);
            //ReprDir_Print = Convert.ToBoolean(objlist.ReprDir_Print);
            //VhclDir_Print = Convert.ToBoolean(objlist.VhclDir_Print);
            //PrintWrnt = Convert.ToBoolean(objlist.PrintWrnt);
            //PrintOnSave = Convert.ToBoolean(objlist.PrintOnSave);
            //Selection_box = Convert.ToBoolean(objlist.Selection_box);
            //Rate_Change = Convert.ToBoolean(objlist.Rate_Change);
            //Taxes = Convert.ToInt32(objlist.Taxes);
            //EnRequire = Convert.ToBoolean(objlist.EnRequire);
            //NoPlateChrges = Convert.ToBoolean(objlist.NoPlateChrges);
            //GoodLifeAmnt = Convert.ToBoolean(objlist.GoodLifeAmnt);
            //ExWrntyAmnt = Convert.ToBoolean(objlist.ExWrntyAmnt);
            //FollowwithNo = Convert.ToBoolean(objlist.FollowwithNo);
            //CashCrSeq = Convert.ToByte(objlist.CashCrSeq);
            //JobCounterSeq = Convert.ToByte(objlist.JobCounterSeq);
            //Repr_PrePrint = Convert.ToBoolean(objlist.Repr_PrePrint);
            //Job_Open = Convert.ToBoolean(objlist.Job_Open);
            //Disc_Prcnt = Convert.ToBoolean(objlist.Disc_Prcnt);
            //Gate_Pass = Convert.ToBoolean(objlist.Gate_Pass);
            //Cash_Counter_Text = Convert.ToBoolean(objlist.Cash_Counter_Text);
            //Credit_Vat = Convert.ToBoolean(objlist.Credit_Vat);
            //RoundOff_Sale = Convert.ToBoolean(objlist.RoundOff_Sale);
            //Repr_terms = Convert.ToBoolean(objlist.Repr_terms);
            //OutServ_Tax = Convert.ToBoolean(objlist.OutServ_Tax);
            //Allow_DMS = Convert.ToBoolean(objlist.Allow_DMS);
            //RBill_Scheme = Convert.ToBoolean(objlist.RBill_Scheme);
            //DiscPerValue = Convert.ToBoolean(objlist.DiscPerValue);
            //PrintRateTotal = Convert.ToBoolean(objlist.PrintRateTotal);
            //Job_RegNo = Convert.ToBoolean(objlist.Job_RegNo);
            //TaxAmnt = Convert.ToBoolean(objlist.TaxAmnt);
            //Disc_OnMRP = Convert.ToBoolean(objlist.Disc_OnMRP);
            //Prev_Visit = Convert.ToBoolean(objlist.Prev_Visit);
            //CSTRep_Bill = Convert.ToBoolean(objlist.CSTRep_Bill);
            //AdviceRequired = Convert.ToBoolean(objlist.AdviceRequired);
            //Repr_Vat = Convert.ToBoolean(objlist.Repr_Vat);
            //Job_Cash_Prefix = Convert.ToString(objlist.Job_Cash_Prefix);
            //Job_Credit_Prefix = Convert.ToString(objlist.Job_Credit_Prefix);
            //Counter_Cash_Prefix = Convert.ToString(objlist.Counter_Cash_Prefix);
            //Counter_Credit_Prefix = Convert.ToString(objlist.Counter_Credit_Prefix);
            //Vehicle_Bill_Prefix = Convert.ToString(objlist.Vehicle_Bill_Prefix);
            //Vehicle_DBill_Prefix = Convert.ToString(objlist.Vehicle_DBill_Prefix);
            //Vehicle_SChlnS_Prefix = Convert.ToString(objlist.Vehicle_SChlnS_Prefix);
            //Vehicle_SChlnM_Prefix = Convert.ToString(objlist.Vehicle_SChlnM_Prefix);
            //Vehicle_SaleCSD_Prefix = Convert.ToString(objlist.Vehicle_SaleCSD_Prefix);
            //SaleLetterPref_Prefix = Convert.ToString(objlist.SaleLetterPref_Prefix);
            //Estimate_Prefix = Convert.ToString(objlist.Estimate_Prefix);
            //Comp_Adrs = Convert.ToBoolean(objlist.Comp_Adrs);
            //Sale_Letter_Six = Convert.ToBoolean(objlist.Sale_Letter_Six);
            //SBill_Scheme = Convert.ToBoolean(objlist.SBill_Scheme);
            //Letter_Head = Convert.ToBoolean(objlist.Letter_Head);
            //SBill_Sep = Convert.ToBoolean(objlist.SBill_Sep);
            //Cust_Adrs_CSD = Convert.ToBoolean(objlist.Cust_Adrs_CSD);
            //Vehicle_CashCr = Convert.ToBoolean(objlist.Vehicle_CashCr);
            //Vehicle_ThruRcpt = Convert.ToBoolean(objlist.Vehicle_ThruRcpt);
            //DueAmnt = Convert.ToBoolean(objlist.DueAmnt);
            //CSD_Sep = Convert.ToBoolean(objlist.CSD_Sep);
            //SubDlrSaleOnExShPrice = Convert.ToByte(objlist.SubDlrSaleOnExShPrice);
            //Hide_Other = Convert.ToBoolean(objlist.Hide_Other);
            //GatePass_VSale = Convert.ToBoolean(objlist.GatePass_VSale);
            //SendSMSVSaleBill = Convert.ToBoolean(objlist.SendSMSVSaleBill);
            //VSB_SType = Convert.ToBoolean(objlist.VSB_SType);
            //Battery_No = Convert.ToBoolean(objlist.Battery_No);
            //Service_Coupon = Convert.ToBoolean(objlist.Service_Coupon);
            //Key_No = Convert.ToBoolean(objlist.Key_No);
            //Fin_Amnt = Convert.ToBoolean(objlist.Fin_Amnt);
            //Modl_Narr = Convert.ToBoolean(objlist.Modl_Narr);
            //VhclBill_Rcpt_Print = Convert.ToBoolean(objlist.VhclBill_Rcpt_Print);
            //TermsQT = Convert.ToString(objlist.TermsQT);
            //HYP_Amnt = Convert.ToDouble(objlist.HYP_Amnt);
            //Trade_Cert = Convert.ToBoolean(objlist.Trade_Cert);
            //Vhcl_Disc = Convert.ToBoolean(objlist.Vhcl_Disc);
            //PrintTin_SalLtr = Convert.ToBoolean(objlist.PrintTin_SalLtr);
            //Form22 = Convert.ToBoolean(objlist.Form22);
            //Show_TempAdrs = Convert.ToBoolean(objlist.Show_TempAdrs);
            //PrintAddTaxOnCSD = Convert.ToBoolean(objlist.PrintAddTaxOnCSD);
            //OnRoad_Billing = Convert.ToBoolean(objlist.OnRoad_Billing);
            //TempAdd_Caption = Convert.ToBoolean(objlist.TempAdd_Caption);
            //VehCost_WthDisc = Convert.ToBoolean(objlist.VehCost_WthDisc);
            //TradeDiscInSSo = Convert.ToBoolean(objlist.TradeDiscInSSo);
            //E_Mail = Convert.ToBoolean(objlist.E_Mail);
            //Sale_Narration = Convert.ToBoolean(objlist.Sale_Narration);
            //State = Convert.ToInt32(objlist.State);
            //Abatement = Convert.ToDouble(objlist.Abatement);
            //Cenvat = Convert.ToDouble(objlist.Cenvat);
            //EduCess = Convert.ToDouble(objlist.EduCess);
            //ShEcess = Convert.ToDouble(objlist.ShEcess);
            //FrIns = Convert.ToDouble(objlist.FrIns);
            //CST = Convert.ToDouble(objlist.CST);
            //SMS_Msg = Convert.ToBoolean(objlist.SMS_Msg);
            //Hindi_SMS = Convert.ToBoolean(objlist.Hindi_SMS);
            //SMS_Lang = Convert.ToBoolean(objlist.SMS_Lang);
            //GoldCust_Criteria = Convert.ToInt16(objlist.GoldCust_Criteria);
            //Visit_Criteria = Convert.ToInt32(objlist.Visit_Criteria);
            //SaleDate_from = Convert.ToDateTime(objlist.SaleDate_from);
            //SaleDate_To = Convert.ToDateTime(objlist.SaleDate_To);
            //Rcpt_Print = Convert.ToInt32(objlist.Rcpt_Print);
            //cmbBillPrntParam = Convert.ToInt32(objlist.cmbBillPrntParam);
            //Tax_With = Convert.ToInt32(objlist.Tax_With);
            //Holding_Days = Convert.ToInt32(objlist.Holding_Days);
            //SaleLtr_NoPrn = Convert.ToInt32(objlist.SaleLtr_NoPrn);
            //Invoice_NoPrn = Convert.ToInt32(objlist.Invoice_NoPrn);
            //Repr_NoPrn = Convert.ToInt32(objlist.Repr_NoPrn);
            //SepSeq_TokNo = Convert.ToBoolean(objlist.SepSeq_TokNo);
            //Seq_AccidentalJob = Convert.ToBoolean(objlist.Seq_AccidentalJob);
            //Chasis_Tax = Convert.ToBoolean(objlist.Chasis_Tax);
            //VSBill_AfterPDI = Convert.ToBoolean(objlist.VSBill_AfterPDI);
            //Print_PreAuthen = Convert.ToBoolean(objlist.Print_PreAuthen);
            //PurCalc_Rqty = Convert.ToBoolean(objlist.PurCalc_Rqty);
            //ReCalcRepr_Visible = Convert.ToBoolean(objlist.ReCalcRepr_Visible);
            //ReprBill_SixInch = Convert.ToBoolean(objlist.ReprBill_SixInch);
            //Reg_Counter = Convert.ToBoolean(objlist.Reg_Counter);
            //SurChrge = Convert.ToBoolean(objlist.SurChrge);
            //Calc_TVS = Convert.ToBoolean(objlist.Calc_TVS);
            //Printed_Stationary = Convert.ToBoolean(objlist.Printed_Stationary);
            //AdTax_Reqd = Convert.ToBoolean(objlist.AdTax_Reqd);
            //SendSMSVSaleChl = Convert.ToBoolean(objlist.SendSMSVSaleChl);
            //Chln_Conti = Convert.ToBoolean(objlist.Chln_Conti);
            //Open_Ledger = Convert.ToBoolean(objlist.Open_Ledger);
            //TinTrade_PrePrint = Convert.ToBoolean(objlist.TinTrade_PrePrint);
            //Prv_Edi = Convert.ToBoolean(objlist.Prv_Edi);
            //TCR_Com = Convert.ToBoolean(objlist.TCR_Com);
            //TCR_Rqd = Convert.ToBoolean(objlist.TCR_Rqd);
            //PrintCustNameinTempSL = Convert.ToBoolean(objlist.PrintCustNameinTempSL);
            //TCR_Com = Convert.ToBoolean(objlist.TCR_Com);
            //StckTrnNDP = Convert.ToBoolean(objlist.StckTrnNDP);
            //Clos_Pur = Convert.ToBoolean(objlist.Clos_Pur);
            //Logo_Required = Convert.ToBoolean(objlist.Logo_Required);
            //BillHeader_Surbhi = Convert.ToBoolean(objlist.BillHeader_Surbhi);
            //Job_Cash = Convert.ToInt32(objlist.Job_Cash);
            //Job_Credit = Convert.ToInt32(objlist.Job_Credit);
            //Counter_Cash = Convert.ToInt32(objlist.Counter_Cash);
            //Counter_Credit = Convert.ToInt32(objlist.Counter_Credit);
            //TIN_COUNTER = Convert.ToInt32(objlist.TIN_COUNTER);
            //RecEntryPrint = Convert.ToInt32(objlist.RecEntryPrint);
            //Repr_PrintCriteria = Convert.ToInt32(objlist.Repr_PrintCriteria);
            //TermsQT = Convert.ToString(objlist.TermsQT);
            //TermsRecEntry = Convert.ToString(objlist.TermsRecEntry);
            //PrintAddrIn_ReprCntr = Convert.ToBoolean(objlist.PrintAddrIn_ReprCntr);
            //Hide_SLBillNo = Convert.ToBoolean(objlist.Hide_SLBillNo);
            //PrintRcpt_Amnt = Convert.ToBoolean(objlist.PrintRcpt_Amnt);
            //PurIn_SaleRate = Convert.ToBoolean(objlist.PurIn_SaleRate);
            //VSBill_SingleCash = Convert.ToInt32(objlist.VSBill_SingleCash);
            //VSBill_SingleCredit = Convert.ToInt32(objlist.VSBill_SingleCredit);
            //VSBill_MultipleCash = Convert.ToInt32(objlist.VSBill_MultipleCash);
            //VSBill_MultipleCredit = Convert.ToInt32(objlist.VSBill_MultipleCredit);
            //VSBill_CSD = Convert.ToInt32(objlist.VSBill_CSD);
            //VSBill_CSDCredit = Convert.ToInt32(objlist.VSBill_CSDCredit);
            //VSBill_PrintCriteria = Convert.ToInt32(objlist.VSBill_PrintCriteria);
            //Sale_Letter = Convert.ToInt32(objlist.Sale_Letter);
            //Lower_Tin = Convert.ToBoolean(objlist.Lower_Tin);
            //Shwbillno = Convert.ToBoolean(objlist.Shwbillno);
            //Tradecert_OnLeft = Convert.ToBoolean(objlist.Tradecert_OnLeft);
            //ShowVecNo_CrTemp = Convert.ToBoolean(objlist.ShowVecNo_CrTemp);
            //PrintChallanAmnt = Convert.ToBoolean(objlist.PrintChallanAmnt);
            //Show_Month = Convert.ToBoolean(objlist.Show_Month);
            //Pur_Round = Convert.ToBoolean(objlist.Pur_Round);
            //Repr_Preprint = Convert.ToBoolean(objlist.Repr_PrePrint);
            //RepeatHeader = Convert.ToBoolean(true);
            //VatReqd = 1;
            //VAT_Tax = 1;
            //Serv_Tax = 1;
            //Jurisdiction = false;
            //Allow_Neg_Stk = false;
        }
        public void CheckUserCompanyDetl()
        {

     

            //UserIdno = Convert.ToInt32(Session["UserIdno"].ToString());
            CompanyMastDAL ObjMast = new CompanyMastDAL();
            tblCompMast CompMAst = ObjMast.SelectAll();
            CompLocationLimit = Convert.ToInt32(CompMAst.Tot_Loc);
            //Model.clsLoginDAL objLoginDAL = new Model.clsLoginDAL();
            //Model.USER objUser = objLoginDAL.SelectUserDetl(UserIdno);
            //UserName = Convert.ToString(objUser.Name);
            //DesignIdno = Convert.ToInt32(objUser.Desig_Idno);
            //WorkArea = Convert.ToInt32(objUser.WorkArea_Idno);
            //CompId = Convert.ToInt32(objUser.Comp_Idno);
            //var CompDetl=objLoginDAL.SelectCompDetl(CompId);
            //CompName = CompDetl.Name;
            //CompPhone = CompDetl.Phone_Off;
            //strMsgUserName = CompDetl.UserName;
            //strMsgPassword = CompDetl.Password;
            //intSMStype = CompDetl.Sms_Type;
            //strMsgSenderID = CompDetl.Sender;
            //strProfileId = CompDetl.Profile_Id;


            //DtTempStore = ApplicationFunction.CreateTable("tbl", "id", "String", "LocationIdno", "String", "LocationName", "String");
            //DtTempWH = ApplicationFunction.CreateTable("tbl", "id", "String", "LocationIdno", "String", "LocationName", "String");

            //var lst = objLoginDAL.SelectEmpWHStoreDetl(UserIdno, WorkArea);
            //if (UserIdno == 1)
            //{
            //    //lst = objLoginDAL.SelectAdminStore(2); 
            //}

            //for (i = 0; i < lst.Count; i++)
            //{
            //    arrayLoc[i] = Convert.ToInt32(DataBinder.Eval(lst[i], "Location_Idno"));
            //}
            //for (j = 0; j < arrayLoc.Length; j++)
            //{
            //    if (WorkArea == 1)
            //    {
            //        var objLocationMastStore = objLoginDAL.SelectLocShowroom(arrayLoc[j]);
            //        for (int k = 0; k < objLocationMastStore.Count; k++)
            //        {
            //            string LocationIdno = Convert.ToString(DataBinder.Eval(objLocationMastStore[k], "Loc_Idno"));
            //            string LocationName = Convert.ToString(DataBinder.Eval(objLocationMastStore[k], "Loc_Name"));
            //            int id = DtTempStore.Rows.Count == 0 ? 1 : DtTempStore.Rows.Count + 1;
            //            ApplicationFunction.DatatableAddRow(DtTempStore, id, LocationIdno, LocationName);
            //        }
            //    }
            //    else if (WorkArea == 2)
            //    {
            //        var objLocationMastWh = objLoginDAL.SelectLocStore(arrayLoc[j]);
            //        for (int l = 0; l < objLocationMastWh.Count; l++)
            //        {
            //            string LocationIdno = Convert.ToString(DataBinder.Eval(objLocationMastWh[l], "Loc_Idno"));
            //            string LocationName = Convert.ToString(DataBinder.Eval(objLocationMastWh[l], "Loc_Name"));
            //            int id = DtTempWH.Rows.Count == 0 ? 1 : DtTempWH.Rows.Count + 1;
            //            ApplicationFunction.DatatableAddRow(DtTempWH, id, LocationIdno, LocationName);
            //        }
            //    }
            //    else if (WorkArea == 3)
            //    {
            //        var objLocationMastWh = objLoginDAL.SelectLocStore(arrayLoc[j]);
            //        for (int m = 0; m < objLocationMastWh.Count; m++)
            //        {
            //            string LocationIdno = Convert.ToString(DataBinder.Eval(objLocationMastWh[m], "Loc_Idno"));
            //            string LocationName = Convert.ToString(DataBinder.Eval(objLocationMastWh[m], "Loc_Name"));
            //            int id = DtTempWH.Rows.Count == 0 ? 1 : DtTempWH.Rows.Count + 1;
            //            ApplicationFunction.DatatableAddRow(DtTempWH, id, LocationIdno, LocationName);
            //        }
            //    }
            //}
            //StartYear = 2014;
            //objLoginDAL = null;
        }
        public bool CheckUserRights(int intFormIdno)
        {
            bool bvalue = false;
            try
            {
                WebTransport.DAL.LoginDAL objLoginDAL = new WebTransport.DAL.LoginDAL();
                UserIdno = Convert.ToInt32(Session["UserIdno"].ToString());
                tblUserRight objUserRghts = objLoginDAL.SelectUserRights(UserIdno, intFormIdno);
                UserRgt_Idno = Convert.ToInt32(objUserRghts.UserRgt_Idno);
                Form_Idno = Convert.ToInt32(objUserRghts.Form_Idno);
                ADD = Convert.ToBoolean(objUserRghts.ADD);
                Edit = Convert.ToBoolean(objUserRghts.Edit);
                View = Convert.ToBoolean(objUserRghts.View);
                Delete = Convert.ToBoolean(objUserRghts.Delete);
                Print = Convert.ToBoolean(objUserRghts.Print);
                if (ADD == false && Edit == false && View == false && Delete == false && Print == false)
                {
                    bvalue = false;
                }
                else
                {
                    bvalue = true;
                }
                objLoginDAL = null;
                return bvalue;
            }
            catch (Exception Ex)
            {
            }
            return bvalue;
        }
        public FinYear FatchFinYear(int CompId)
        {
            FinYear objFinYear = new FinYear();
            //Model.clsLoginDAL objLoginDAL = new Model.clsLoginDAL();
            //var lst = objLoginDAL.CurrentFinYear(CompId);
            //if (lst != null)
            //{
            //    objFinYear.StartDate = Convert.ToDateTime(lst.StartDate);
            //    objFinYear.EndYear = Convert.ToDateTime(lst.EndDate);
            //    objFinYear.YearId = Convert.ToInt32(lst.Fin_Idno);

            //}
            return objFinYear;
        }
        public class FinYear
        {
            public DateTime StartDate
            {
                get;
                set;
            }

            public DateTime EndYear
            {
                get;
                set;
            }

            public int YearId
            {
                get;
                set;
            }
        }

        public int StartYear
        {
            get;
            set;
        }
        #endregion
    }
}