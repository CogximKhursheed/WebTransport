using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Collections;
using System.Web.Services;
using System.Collections.Generic;

namespace WebTransport
{
    public partial class TripEntry : Pagebase
    {
        #region Private Variables...
        DataSet DsDetl = new DataSet();
        DataTable DtTemp = new DataTable(); //string con = "";
        DataTable DtTempFuel = new DataTable();
        DataTable DtTempChln = new DataTable();
        DataTable DtTempToll = new DataTable();
        DataTable DtTempGR = new DataTable();

        double dChlnGrdTotAmnt = 0;
        double dChlnGrdGrossAmnt = 0;
        double dChlnGrdDesialAmnt = 0;
        double dChlnGrdAdvAmnt = 0;
        double dChlnGrdAdvTotAmnt = 0;
        double dFuelGrdTotAmnt = 0;
        double dGRGrdTotAmnt = 0;
        double dExpenGrdTotAmnt = 0;
        double dTollGrdTotAmnt = 0;
        private int intFormId = 62;
        int intDriverIdno = 0;
        double pChlnGross =0, pChlnTot = 0, pFuelTot = 0, pExpTot = 0, pTollTot = 0, pChlnDesial = 0;
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
                    lnkBtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lnkView.Visible = false;
                }
                this.Bind();
                //this.BindCity();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindFromCity();
                }
                else
                {
                    this.BindFromCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                this.BindRcpt();
                this.BindBank();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);

                TripEntryDAL objChlnBookingDAL = new TripEntryDAL();

                lnkBtnSave.Enabled = true;
                this.TripNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                DtTemp = CreateDt();
                ViewState["dt"] = DtTemp;
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtTripNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtExpAmnt.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtStratKms.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtEndKms.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtKMS.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtExpAmnt.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInsentive.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInstNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindExpenduture();
                this.BlnkGrids();
                ddldateRange.Focus();
                if (Request.QueryString["TripI"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["TripI"]));
                    hidid.Value = Convert.ToString(Request.QueryString["TripI"]);
                    lnkBtnNew.Visible = true;
                    lnkPrint.Visible = true;
                }
                else
                {
                    lnkBtnNew.Visible = false;
                    lnkPrint.Visible = false;
                }
            }
        }
        #endregion

        #region Button Evnets...
        protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["TripI"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["TripI"]));
            }
            else
            {
                Response.Redirect("TripEntry.aspx");
            }
        }
        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("TripEntry.aspx");
        }
        #endregion

        #region Functions...

        private bool PostIntoAccounts(DataTable DtExp, DataTable DtToll, Int32 DriverIdno, Int64 intTripIdno, string TripDate, string TripNo, Int32 DateRange, Int32 intUserIdno, double Advamnt, Int32 Rspttype, Int32 Instno, Int32 custBank, string InstDate)
        {
            bool Result = false;
            try
            {
                double dExpAmnt = 0, dTollAmnt = 0;
                Int64 intValue = 1; Int64 intVchrIdno = 0; Int64 IntVal = 1;
                BindDropdownDAL obj = new BindDropdownDAL();
                tblFleetAcntLink objAcntLink = obj.SelectAcntLink();
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();

                if (DtExp != null && DtExp.Rows.Count > 0)
                {
                    dExpAmnt = string.IsNullOrEmpty(Convert.ToString(DtExp.Compute("Sum(Amnt)", ""))) ? 0.00 : Convert.ToDouble(DtExp.Compute("Sum(Amnt)", ""));
                }
                if (DtToll != null && DtToll.Rows.Count > 0)
                {
                    dTollAmnt = string.IsNullOrEmpty(Convert.ToString(DtToll.Compute("Sum(Amnt)", ""))) ? 0.00 : Convert.ToDouble(DtToll.Compute("Sum(Amnt)", ""));
                }
                if (Request.QueryString["TripI"] == null)
                {
                    intValue = 1;
                }
                else
                {
                    intValue = objclsAccountPosting.DeleteAccountPosting(intTripIdno, "TRP");
                }
                if (dExpAmnt > 0 || dTollAmnt >0)
                {
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(TripDate)),
                    4,
                    0,
                    "Trip No : " + Convert.ToString(TripNo) + " Trip Date: " + TripDate,
                    true,
                    0,
                    "TRP",
                    0,
                    0,
                    0,
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(TripDate)),
                    0,
                    0,
                    DateRange,
                    Convert.ToInt32(base.CompId), intUserIdno);
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                                   intVchrIdno,
                                   DriverIdno,
                                   "",
                                   Convert.ToDouble(Math.Abs(dExpAmnt + dTollAmnt)),
                                   Convert.ToByte(1),
                                   Convert.ToByte(0),
                                   "",
                                   true,
                                   null,
                                   "", Convert.ToInt32(base.CompId));

                        if (dExpAmnt > 0)
                        {
                            foreach (DataRow Dr in DtExp.Rows)
                            {
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt32(Dr["Acnt_Idno"]),
                                            "",
                                            Math.Abs(Convert.ToDouble(Convert.ToString(Dr["Amnt"]))),
                                            Convert.ToByte(2),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            null,
                                            "", Convert.ToInt32(base.CompId));
                            }
                        }
                        if (dTollAmnt > 0)
                        {
                            if ((string.IsNullOrEmpty(Convert.ToString(objAcntLink.TollAcc_Idno)) ? 0 : Convert.ToInt32(objAcntLink.TollAcc_Idno)) <=0)
                            {
                                hidepostingMsg.Value = "Please Define Toll Account Link in Fleet!";
                                return false;
                            }
                            foreach (DataRow Dr in DtToll.Rows)
                            {
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                            intVchrIdno,
                                            Convert.ToInt64(objAcntLink.TollAcc_Idno),
                                            "",
                                            Math.Abs(Convert.ToDouble(Convert.ToString(Dr["Amnt"]))),
                                            Convert.ToByte(2),
                                            Convert.ToByte(0),
                                            "",
                                            false,
                                            null,
                                            "", Convert.ToInt32(base.CompId));
                            }
                        }
                        if (intValue == 0)
                        {
                            return false;
                        }
                        if (intValue > 0)
                        {
                            intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intTripIdno, "TRP");
                            if (intValue == 0)
                            {
                                return false;
                            }
                            else
                            {
                                Result = true;
                            }
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                #region VchrPosting For Advance Amount By Salman...
                if (Advamnt > 0)
                {
                    #region VchrHead Posting..
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(TripDate)),
                    1,
                    Rspttype,
                    "Trip No : " + Convert.ToString(TripNo) + " Trip Date: " + TripDate + "Total Amount: " + Advamnt  ,
                    true,
                    0,
                    "TRP",
                    DriverIdno,
                    0,
                    0,
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(TripDate)),
                    0,
                    0,
                    DateRange,
                    Convert.ToInt32(base.CompId), intUserIdno);
                    #endregion
                    Int32 Amnttype = 1;
                    Int32 Amnttype2 = 2;
                    string Paytype="Total Amount :";
                    string Paytype2 = "Total Amount :";
                    string Custbank = "";
                    #region VchrDetl Posting..
                    if (intValue > 0)
                    {
                        if (HidPaytype.Value == "Pay to Driver")
                        {
                            Amnttype = 1;
                            Paytype = "  Pay to Driver :";
                            Paytype2 ="  Pay to Driver :";
                            Amnttype2 = 2;
                        }
                        else
                        {
                            Paytype = " Rec From Driver :";
                            Paytype2 =" Rec From Driver :";
                            Amnttype = 2;
                            Amnttype2 = 1;
                        }
                        intVchrIdno = intValue;
                        intValue = 0;
                        IntVal = objclsAccountPosting.InsertInVchrDetl(
                                   intVchrIdno,
                                   Rspttype,
                                   "Receipt No :" + TripNo + " Receipt Date:" + TripDate + Paytype + Advamnt,
                                   Convert.ToDouble(Math.Abs(Advamnt)),
                                   Convert.ToByte(Amnttype),
                                   Convert.ToByte(0),
                                   Convert.ToString(Instno),
                                   true,
                                   null,
                                   "", Convert.ToInt32(base.CompId));
                        if (IntVal > 0)
                        {
                            if (ddlCusBank.SelectedIndex == 0)
                            {
                                Custbank = "";
                            }
                            else
                            {
                                Custbank = ddlCusBank.SelectedItem.Text;
                            }
                            intVchrIdno = IntVal;
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno,
                                       DriverIdno,
                                       "Receipt No :" + TripNo + " Receipt Date:" + TripDate + Paytype2 + Advamnt,
                                       Convert.ToDouble(Math.Abs(Advamnt)),
                                       Convert.ToByte(Amnttype2),
                                       Convert.ToByte(0),
                                       Convert.ToString(Instno),
                                       false,
                                       null,
                                      Custbank
                                       , Convert.ToInt32(base.CompId));
                        }
                        #region VchrDetlId Posting..
                        if (IntVal > 0)
                        {
                            intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intTripIdno, "TRP");
                            if (intValue == 0)
                            {
                                return false;
                            }
                            else
                            {
                                Result = true;
                            }
                        }
                        #endregion

                        else
                        {
                            return false;
                        }
                    }
                    
                    
                   
                    #endregion
                    
                   
                }
                #endregion
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        private void CalculateKms()
        {
            try
            {
                string StrIds = "";
                for (int i = 0; i < grdChlnDetl.Rows.Count; i++)
                {
                    HiddenField HidChlnIdno = (HiddenField)grdChlnDetl.Rows[i].FindControl("HidChlnIdno");
                    if (StrIds == "")
                    {
                        StrIds = Convert.ToString(HidChlnIdno.Value);
                    }
                    else
                    {
                        StrIds += "," + Convert.ToString(HidChlnIdno.Value);
                    }
                }
                txtKMS.Text = Convert.ToDouble(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, "exec [spTripSheet] @Action='FetchKMSByChallan',@strId='" + StrIds + "'")).ToString("N2");
            }
            catch (Exception ex)
            {

            }
        }
        private void AutofillDefault()
        {
            try
            {
                TripEntryDAL obj = new TripEntryDAL(); Int64 Yearidno = 0;
                Yearidno = obj.AutofillYear();
                ddldateRange.SelectedValue = Convert.ToString(Yearidno == 1 ? "1" : "2");
                txtDate.Text = obj.AutofillDate();
            }
            catch (Exception Ex)
            {
            }
        }
        private void TripNo(Int32 YearIdno, Int32 FromCityIdno)
        {
            TripEntryDAL obj = new TripEntryDAL();
            txtTripNo.Text = Convert.ToString(obj.GetTripNo(YearIdno, FromCityIdno, ApplicationFunction.ConnectionString()));
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
        private void BindFromCity()
        {
            TripEntryDAL obj = new TripEntryDAL();
            var lst = obj.SelectCityCombo();
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
        private void BindFromCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "CityIdno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDriver(Int32 var)
        {
            TripEntryDAL obj = new TripEntryDAL();
            if (var == 0)
            {
                ddldriverName.DataSource = null;
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Acnt_Name";
                    ddldriverName.DataValueField = "Acnt_Idno";
                    ddldriverName.DataBind();
                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddldriverName.DataSource = null;
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Driver_name";
                    ddldriverName.DataValueField = "Driver_Idno";
                    ddldriverName.DataBind();
                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void BindExpenduture()
        {
            TripEntryDAL obj = new TripEntryDAL();
            var lst = obj.BindExpens();
            obj = null;
            if (lst != null && lst.Count > 0)
            {
                ddlExpenese.DataSource = lst;
                ddlExpenese.DataTextField = "AcntName";
                ddlExpenese.DataValueField = "AcntIdno";
                ddlExpenese.DataBind();
            }
            ddlExpenese.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void Bind()
        {
            TripEntryDAL obj = new TripEntryDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddlTruckNo.DataSource = lst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();
            }
            ddlTruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    txtDateTo.Text = hidmindate.Value;
                }
            }
        }
        private void Populate(Int64 HeadId)
        {
            TripEntryDAL obj = new TripEntryDAL();
            tblTripHead Triphead = obj.selectHead(HeadId);

            ddldateRange.SelectedValue = Convert.ToString(Triphead.Year_Idno);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtTripNo.Text = Convert.ToString(Triphead.Trip_No);
            txtDate.Text = Convert.ToDateTime(Triphead.Trip_Date).ToString("dd-MM-yyyy");
            ddlFromCity.SelectedValue = Convert.ToString(Triphead.BaseCity_Idno);
            ddlTruckNo.SelectedValue = Convert.ToString(Triphead.Truck_Idno);
            ddlTruckNo_SelectedIndexChanged(null, null);
            ddldriverName.SelectedValue = Convert.ToString(Triphead.Driver_Idno);
            txtStratKms.Text = Convert.ToString(Triphead.StartKms);
            txtEndKms.Text = Convert.ToString(Triphead.EndKms);
            txtKMS.Text = Convert.ToString(Triphead.FKms);
            txtRemarks.Text = Convert.ToString(Triphead.remark);
            ddlFromCity.Enabled = false;
            txtInsentive.Text = Convert.ToDouble(Triphead.Insentive_Amnt).ToString("N2");
            lblStartKMS.Text = Convert.ToString(Triphead.StartKms);
            lblEndKms.Text = Convert.ToString(Triphead.EndKms);
            lblFinalKMs.Text = Convert.ToString(Triphead.FKms);
            ddlRcptType.SelectedValue = Convert.ToString(Triphead.RcptType_Idno);
            txtInstNo.Text = Convert.ToString(Triphead.Inst_No);
            txtInstDate.Text = ((Triphead.Inst_Dt == null) ? "" : Convert.ToDateTime(Triphead.Inst_Dt).ToString("dd-MM-yyyy"));
            ddlCusBank.SelectedValue = Convert.ToString(Triphead.Bank_Idno);
            txtAdvAmnt.Text = Convert.ToDouble(Triphead.Adv_Amnt).ToString("N2");
            //Print Section
            // PrintTripSheet();

            //
            DsDetl = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId);

            if (DsDetl != null && DsDetl.Tables.Count > 0)
            {
                TripEntryDAL ObjDAL = new TripEntryDAL();
                if (DsDetl.Tables[0].Rows.Count > 0)
                {
                    Int64[] strchkDetlValue = new Int64[DsDetl.Tables[0].Rows.Count];
                    int j = 0;
                    for (int count = 0; count < DsDetl.Tables[0].Rows.Count; count++)
                    {
                        strchkDetlValue[j] = Convert.ToInt64(DsDetl.Tables[0].Rows[count]["Chln_Idno"]);
                        j++;
                    }
                    var ChlnDetl = ObjDAL.BindChallan(strchkDetlValue);
                    if (ChlnDetl != null && ChlnDetl.Count > 0)
                    {
                        string DriverId = string.Empty;
                        DtTempChln = null;
                        DtTempChln = CreateDtChln();
                        for (int i = 0; i < ChlnDetl.Count; i++)
                        {
                            string ChlnIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "ChlnIdno"));
                            string DriverIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "DriverIdno"));
                            HidAdvAmnt.Value = Convert.ToDouble(DataBinder.Eval(ChlnDetl[i], "Advance")).ToString("N2");
                           
                            ApplicationFunction.DatatableAddRow(DtTempChln, ChlnIdno, DriverIdno);
                        }
                        ViewState["dtChln"] = DtTempChln;
                        grdChlnDetl.DataSource = ChlnDetl;
                        grdChlnDetl.DataBind();


                    }
                    else
                    {
                        grdChlnDetl.DataSource = null;
                        grdChlnDetl.DataBind();

                    }
                }
                if (DsDetl.Tables[1].Rows.Count > 0)
                {
                    Int64[] strchkDetlValue = new Int64[DsDetl.Tables[1].Rows.Count];
                    int j = 0;
                    for (int count = 0; count < DsDetl.Tables[1].Rows.Count; count++)
                    {
                        strchkDetlValue[j] = Convert.ToInt64(DsDetl.Tables[1].Rows[count]["Pbill_Idno"]);
                        j++;
                    }
                    var lst = ObjDAL.BindPetrolPump(strchkDetlValue);
                    if (lst != null && lst.Count > 0)
                    {
                        DtTempFuel = null;
                        DtTempFuel = CreateDtFuel();
                        for (int i = 0; i < lst.Count; i++)
                        {
                            string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "PbillIdno"));
                            string Amount = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Amount"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "Amount"));
                            ApplicationFunction.DatatableAddRow(DtTempFuel, PbillIdno, Amount);
                        }
                        ViewState["dtFuel"] = DtTempFuel;
                        GrdPumpDetl.DataSource = lst;
                        GrdPumpDetl.DataBind();
                    }
                    else
                    {
                        GrdPumpDetl.DataSource = null;
                        GrdPumpDetl.DataBind();

                    }
                }
                if (DsDetl.Tables[2].Rows.Count > 0)
                {
                    DtTemp = null;
                    DtTemp = CreateDt();

                    for (int i = 0; i < DsDetl.Tables[2].Rows.Count; i++)
                    {

                        Int32 ROWCount = Convert.ToInt32(DtTemp.Rows.Count) - 1;
                        int id = DtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(DtTemp.Rows[ROWCount]["id"])) + 1;
                        string strAcntName = Convert.ToString(DsDetl.Tables[2].Rows[i]["Acnt_Name"]);
                        string strAcntIdno = Convert.ToString(DsDetl.Tables[2].Rows[i]["Acnt_Idno"]);
                        string strAmount = Convert.ToString(DsDetl.Tables[2].Rows[i]["Amnt"]);
                        ApplicationFunction.DatatableAddRow(DtTemp, id, strAcntName, strAcntIdno, strAmount);
                    }
                    ViewState["dt"] = DtTemp;
                    this.BindGridExpn();
                }
                if (DsDetl.Tables[3].Rows.Count > 0)
                {
                    Int64[] strchkDetlValue = new Int64[DsDetl.Tables[3].Rows.Count];
                    int j = 0;
                    for (int count = 0; count < DsDetl.Tables[3].Rows.Count; count++)
                    {
                        strchkDetlValue[j] = Convert.ToInt64(DsDetl.Tables[3].Rows[count]["Toll_Idno"]);
                        j++;
                    }

                    var lst = ObjDAL.BindTollPump(strchkDetlValue);
                    if (lst != null && lst.Count > 0)
                    {
                        DtTempToll = null;
                        DtTempToll = CreateDtToll();
                        for (int i = 0; i < lst.Count; i++)
                        {
                            string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "TollIdno"));
                            string Amnt = Convert.ToString(DataBinder.Eval(lst[i], "Amount"));
                            ApplicationFunction.DatatableAddRow(DtTempToll, PbillIdno, Amnt);
                        }
                        ViewState["dtToll"] = DtTempToll;

                        GrdTollDetl.DataSource = lst;
                        GrdTollDetl.DataBind();
                    }
                    else
                    {
                        GrdTollDetl.DataSource = null;
                        GrdTollDetl.DataBind();

                    }
                }

            }

            PrintTripSheet(HeadId);
            Int64 value = 0;
            if (value > 0)
            {
                lnkBtnSave.Enabled = false;

            }
            else
            {
                lnkBtnSave.Enabled = true;
            }

            obj = null;
        }
        private void Clear()
        {
            ddlFromCity.Enabled = true;
            ViewState["dt"] = null;
            ViewState["dtChln"] = null;
            ViewState["dtFuel"] = null;
            ViewState["dtToll"] = null;

            DtTemp = null;
            DtTempChln = null;
            DtTempToll = null;
            DtTempFuel = null;

            grdChlnDetl.DataSource = null;
            grdChlnDetl.DataBind();

            GrdPumpDetl.DataSource = null;
            GrdPumpDetl.DataBind();

            GrdExpense.DataSource = null;
            GrdExpense.DataBind();

            GrdTollDetl.DataSource = null;
            GrdTollDetl.DataBind();

            txtGrosstotal.Text = "";
            txtNetAmnt.Text = "";

            hidid.Value = string.Empty;
            ddldriverName.SelectedValue = "0";
            ddlTruckNo.SelectedValue = "0";
            txtKMS.Text = "";
            txtStratKms.Text = "";
            txtEndKms.Text = "";
            //txtDate.Text = "";
            txtTripNo.Text = "";
            ddldateRange.Enabled = true;

            lnkBtnNew.Visible = false;
            lnkPrint.Visible = false;
            txtRemarks.Text = "";
            txtInsentive.Text = "";
            lnkBtnSave.Enabled = true;
            txtAdvAmnt.Text = "0.00";
            ddlRcptType.SelectedValue = "0";
            ddlCusBank.SelectedValue = "0";
            txtInstDate.Text = "";
            if (Convert.ToInt32(ddldateRange.SelectedValue) != 0 || Convert.ToUInt32(ddlFromCity.SelectedValue) != 0)
            {
                this.TripNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
            }

        }
        private void BlnkGrids()
        {
            grdChlnDetl.DataSource = null;
            grdChlnDetl.DataBind();

            GrdPumpDetl.DataSource = null;
            GrdPumpDetl.DataBind();

            GrdTollDetl.DataSource = null;
            GrdTollDetl.DataBind();


            GrdExpense.DataSource = null;
            GrdExpense.DataBind();
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Acnt_Name", "String",
                "Acnt_Idno", "String",
                "Amnt", "Double"
                );
            return dttemp;
        }
        private DataTable CreateDtChln()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Chln_Idno", "String",
                "Driver_Idno", "String"
                );
            return dttemp;
        }
        private DataTable CreateDtFuel()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Pbill_Idno", "String",
                "Amount", "Double"
                );
            return dttemp;
        }
        private DataTable CreateDtToll()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Toll_Idno", "String",
                "Amnt", "Double"
                );
            return dttemp;
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void NetCalculate()
        {
            double iChlnTotal = 0, iFuelToal = 0, iExpnTotal = 0, iTollTotal = 0;
            if (grdChlnDetl.Rows.Count > 0)
            {
                if (ViewState["TotalChlnDtl"] != null)
                {
                    iChlnTotal = Convert.ToDouble(ViewState["TotalChlnDtl"].ToString());
                }

            }
            if (GrdPumpDetl.Rows.Count > 0)
            {
                if (ViewState["TotalFuelDtl"] != null)
                {
                    iFuelToal = Convert.ToDouble(ViewState["TotalFuelDtl"].ToString());
                }
            }
            if (GrdExpense.Rows.Count > 0)
            {
                if (ViewState["TotalExpnDtl"] != null)
                {
                    iExpnTotal = Convert.ToDouble(ViewState["TotalExpnDtl"].ToString());
                }
            }
            if (GrdTollDetl.Rows.Count > 0)
            {
                if (ViewState["TotalTollDtl"] != null)
                {
                    iTollTotal = Convert.ToDouble(ViewState["TotalTollDtl"].ToString());
                }
            }
            txtGrosstotal.Text = Convert.ToDouble((iChlnTotal) - (iFuelToal + iExpnTotal + iTollTotal)).ToString("N2");
            txtNetAmnt.Text = (Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtInsentive.Text)).ToString("N2");
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void PrintTripSheet(Int64 TripHead)
        {
            double pChlnTot = 0, pFuelTot = 0, pExpTot = 0, pTollTot = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; //string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";

            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName;
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
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }

            #endregion

            TripEntryDAL obj = new TripEntryDAL();
            tblTripHead Triphead = obj.selectHead(TripHead);
            lblTripNo.Text = Convert.ToString(Triphead.Trip_No);
            lblTriDate.Text = Convert.ToDateTime(Triphead.Trip_Date).ToString("dd-MM-yyyy");
            lblLocation.Text = Convert.ToString(ddlFromCity.SelectedItem.Text);
            lblTruckNo.Text = Convert.ToString(ddlTruckNo.SelectedItem.Text);
            lblDriverName.Text = Convert.ToString(ddldriverName.SelectedItem.Text);
            lblRemarks.Text= Convert.ToString((Triphead.remark) == "" ? "" : Convert.ToString(Triphead.remark));
            if (Convert.ToDouble(Triphead.Insentive_Amnt) <= 0)
            {
                PrintFooterIncentive.Visible = false;
            }
            else
            {
                PrintFooterIncentive.Visible = true;
            }
            lblValueIncentiveAmnt.Text = Convert.ToDouble(Triphead.Insentive_Amnt).ToString("N2");
            lblFinalTotal.Text = Convert.ToDouble(Triphead.Net_Amnt).ToString("N2");
            double txtfinl = Convert.ToDouble(Triphead.Net_Amnt);
            string[] str1 = txtfinl.ToString().Split('.');
            string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
            lblAmountToword.Text = numbertoent;
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spTripSheet] @ACTION='SelectPrintDetl',@Id='" + TripHead + "'");
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                if (dsReport.Tables[0] != null && dsReport.Tables[0].Rows.Count > 0)
                {
                    RptChlnDetl.DataSource = dsReport.Tables[0];
                    RptChlnDetl.DataBind();
                    DivChlnDetl.Visible = true;
                    PrintFooterChlnTot.Visible = true;

                    DataColumn col = dsReport.Tables[0].Columns[1]; // Call this the one you have
                    DataTable tbl = col.Table;

                    var first = tbl.AsEnumerable()
                                   .Select(cols => cols.Field<DateTime>(col.ColumnName))
                                   .OrderBy(p => p.Ticks)
                                   .FirstOrDefault();
                    if (first != null) { lblChlnMinDate.Text = (Convert.ToDateTime(first)).ToString("dd-MM-yyyy"); }
                    var last = tbl.AsEnumerable()
                                  .Select(cols => cols.Field<DateTime>(col.ColumnName))
                                  .OrderByDescending(p => p.Ticks)
                                  .FirstOrDefault();
                    if (last != null) { lblChlnMaxDate.Text = (Convert.ToDateTime(last)).ToString("dd-MM-yyyy"); }
                }
                else
                {
                    DivChlnDetl.Visible = false;
                    PrintFooterChlnTot.Visible = false;
                }

                if (dsReport.Tables[1] != null && dsReport.Tables[1].Rows.Count > 0)
                {
                    RptFuelDetl.DataSource = dsReport.Tables[1];
                    RptFuelDetl.DataBind();
                    DivFuelDetl.Visible = true;
                    PrintFooterFuelTot.Visible = true;
                }
                else
                {
                    DivFuelDetl.Visible = false;
                    PrintFooterFuelTot.Visible = false;
                }

                if (dsReport.Tables[2] != null && dsReport.Tables[2].Rows.Count > 0)
                {
                    RptExpenseDetl.DataSource = dsReport.Tables[2];
                    RptExpenseDetl.DataBind();
                    DivExpenseDetl.Visible = true;
                    PrintFooterExpTotal.Visible = true;
                }
                else
                {
                    DivExpenseDetl.Visible = false;
                    PrintFooterExpTotal.Visible = false;
                }

                if (dsReport.Tables[3] != null && dsReport.Tables[3].Rows.Count > 0)
                {
                    RptTollDetl.DataSource = dsReport.Tables[3];
                    RptTollDetl.DataBind();
                    DivTollDetl.Visible = true;
                    PrintFooterTollTotal.Visible = true;
                }
                else
                {
                    PrintFooterTollTotal.Visible = false;
                    DivTollDetl.Visible = false;
                }
            }
        }
        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }
        private void BindGridExpn()
        {
            if (ViewState["dt"] != null)
            {
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp.Rows.Count > 0)
                {
                    GrdExpense.DataSource = DtTemp;
                    GrdExpense.DataBind();
                }
                else
                {

                    DtTemp = null;
                    GrdExpense.DataSource = DtTemp;
                    GrdExpense.DataBind();
                }
            }
            else
            {
                DtTemp = null;
                GrdExpense.DataSource = DtTemp;
                GrdExpense.DataBind();
            }
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
        #endregion

        #region Control Events...
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex >= 0)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }
        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddldateRange.SelectedValue) != 0 || Convert.ToUInt32(ddlFromCity.SelectedValue) != 0)
            {
                this.TripNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
            }
            ddlFromCity.Focus();
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTruckNo.SelectedValue) > 0)
            {
                TripEntryDAL ObjDAl = new TripEntryDAL();

                Int32 Typ = 0;
                Typ = ObjDAl.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                ddldriverName.DataSource = null;
                if (ddldriverName.Items.Count > 0)
                {
                    ddldriverName.Items.Clear();
                }
                BindDriver(Typ);
                txtStratKms.Focus();
            }
        }
        #endregion

        #region Grid Event...
        protected void GrdExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;

            if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in DtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Acnt_Name"], rw["Acnt_Idno"], rw["Amnt"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridExpn();
            }
            ddlExpenese.Focus();
        }

        protected void grdChlnDetl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dChlnGrdTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
                dChlnGrdGrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross"));
                dChlnGrdDesialAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Desial"));
                dChlnGrdAdvAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Advance"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblChlnTotal");
                lblTotal.Text = dChlnGrdTotAmnt.ToString("N2");
                Label lblFooterGross = (Label)e.Row.FindControl("lblFooterGross");
                lblFooterGross.Text = dChlnGrdGrossAmnt.ToString("N2");
                Label lblFooterAdvance = (Label)e.Row.FindControl("lblFooterAdvance");
                lblFooterAdvance.Text = dChlnGrdAdvAmnt.ToString("N2");
                Label lblFooterDesial = (Label)e.Row.FindControl("lblFooterDesial");
                lblFooterDesial.Text = dChlnGrdDesialAmnt.ToString("N2");

                //ViewState["TotalChlnDtl"] = dChlnGrdTotAmnt.ToString("N2");
                ViewState["TotalChlnDtl"] = dChlnGrdGrossAmnt.ToString("N2");
                NetCalculate();
            }

        }

        protected void GrdPumpDetl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dFuelGrdTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblFuelTotal");
                lblTotal.Text = dFuelGrdTotAmnt.ToString("N2");
                ViewState["TotalFuelDtl"] = dFuelGrdTotAmnt.ToString("N2");
                NetCalculate();
            }
        }
        protected void GrdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (DtTemp == null)
            {
                DtTemp = CreateDt();
                ViewState["dt"] = DtTemp;
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dExpenGrdTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amnt"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotal = (Label)e.Row.FindControl("lblExpnTotal");
                   
                    lblTotal.Text = dExpenGrdTotAmnt.ToString("N2");
                    ViewState["TotalExpnDtl"] = dExpenGrdTotAmnt.ToString("N2");
                    HidexpTotal.Value = dExpenGrdTotAmnt.ToString("N2");
                    NetCalculate();
                    CalculateAmount();
                }
            }
          
        }

        protected void GrdTollDetl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dTollGrdTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTollTotal");
                lblTotal.Text = dTollGrdTotAmnt.ToString("N2");
                ViewState["TotalTollDtl"] = dTollGrdTotAmnt.ToString("N2");
                NetCalculate();
            }
        }
        #endregion

        #region Control Event...
        protected void txtInsentive_TextChanged(object sender, EventArgs e)
        {
            if (txtInsentive.Text.Trim() == "")
            {
                txtInsentive.Text = "0.00";
            }
            else
            {
                NetCalculate();
            }
        }

        protected void RptChlnDetl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                pChlnGross += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Gross_Amnt"));
                pChlnTot += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));
                dChlnGrdAdvTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Adv_Amnt"));
                pChlnDesial += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Diesel_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblChlnGross = (Label)e.Item.FindControl("lblChlnGross");
                Label lblChlnTotal = (Label)e.Item.FindControl("lblChlnTotal");
                Label lblAdvTotal = (Label)e.Item.FindControl("lblAdvTotal");
                Label lblChlnDesial = (Label)e.Item.FindControl("lblChlnDesial");

                lblChlnGross.Text = pChlnGross.ToString("N2");
                lblChlnTotal.Text = pChlnTot.ToString("N2");
                lblAdvTotal.Text = dChlnGrdAdvTotAmnt.ToString("N2");
                lblChlnDesial.Text = pChlnDesial.ToString("N2");

                lblValueChlnToatl.Text = pChlnTot.ToString("N2");
            }
        }

        protected void RptFuelDetl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                pFuelTot += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                lblFuelTotal.Text = pFuelTot.ToString("N2");
                lblValueFuelTotal.Text = pFuelTot.ToString("N2");
            }
        }

        protected void RptExpenseDetl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                pExpTot += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amnt"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                lblExpenseTotal.Text = pExpTot.ToString("N2");
                
                lblValueExpTotal.Text = pExpTot.ToString("N2");
            }
           
        }

        protected void RptTollDetl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                pTollTot += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amnt"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                lblTollTotal.Text = pTollTot.ToString("N2");
                lblValueTollTOtal.Text = pTollTot.ToString("N2");
            }
        }
        #endregion

        #region Button Event...

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdDocdetals.DataSource = null;
            grdDocdetals.DataBind();
            if (ddlFromCity.SelectedIndex <= 0)
            {
                ShowMessageErr("Please Select From City !");
                return;
            }
            if (ddlTruckNo.SelectedIndex <= 0)
            {
                ShowMessageErr("Please Select Lorry !");
                return;
            }
            ddlSearchtyp.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyModal();", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Int32 iRateType = 0; double dcommssn = 0, dweight = 0, dqty = 0, dtotcommssn = 0;
            if ((grdDocdetals != null) && (grdDocdetals.Rows.Count > 0))
            {
                string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                Int64[] strchkDetlValue = new Int64[grdDocdetals.Rows.Count];
                int j = 0;
                for (int count = 0; count < grdDocdetals.Rows.Count; count++)
                {
                    CheckBox ChkGr = (CheckBox)grdDocdetals.Rows[count].FindControl("chkId");
                    if ((ChkGr != null) && (ChkGr.Checked == true))
                    {
                        HiddenField hidDocIdno = (HiddenField)grdDocdetals.Rows[count].FindControl("hidDocIdno");
                        strchkDetlValue[j] = Convert.ToInt64(hidDocIdno.Value);
                        j++;
                    }
                }

                if (strchkDetlValue.Length == 0)
                {
                    ShowMessageErr("Please select atleast one Document.");
                    ShowDiv("ShowClient('dvGrdetails')");
                }
                else
                {
                    TripEntryDAL ObjDAL = new TripEntryDAL();
                    if (Convert.ToInt32(ddlSearchtyp.SelectedValue) == 0 || Convert.ToInt32(ddlSearchtyp.SelectedValue) == 3)
                    {
                        var ChlnDetl = ObjDAL.BindChallan(strchkDetlValue);
                        if (ChlnDetl != null && ChlnDetl.Count > 0)
                        {
                            DtTempChln = null;
                            DtTempChln = CreateDtChln();
                            for (int i = 0; i < ChlnDetl.Count; i++)
                            {
                                string ChlnIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "ChlnIdno"));
                                string DriverIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "DriverIdno"));
                                ApplicationFunction.DatatableAddRow(DtTempChln, ChlnIdno, DriverIdno);
                                
                            }
                            ViewState["dtChln"] = DtTempChln;
                            grdChlnDetl.DataSource = ChlnDetl;
                            grdChlnDetl.DataBind();
                            CalculateKms();
                        }
                        else
                        {
                            grdChlnDetl.DataSource = null;
                            grdChlnDetl.DataBind();
                        }

                    }
                    else if (Convert.ToInt32(ddlSearchtyp.SelectedValue) == 1)
                    {
                        var lst = ObjDAL.BindPetrolPump(strchkDetlValue);
                        if (lst != null && lst.Count > 0)
                        {
                            DtTempFuel = null;
                            DtTempFuel = CreateDtFuel();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "PbillIdno"));
                                double Amount = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Amount"))) ? 0 : Convert.ToDouble(DataBinder.Eval(lst[i], "Amount"));
                                ApplicationFunction.DatatableAddRow(DtTempFuel, PbillIdno, Amount);
                            }
                            ViewState["dtFuel"] = DtTempFuel;

                            GrdPumpDetl.DataSource = lst;
                            GrdPumpDetl.DataBind();
                        }
                        else
                        {
                            GrdPumpDetl.DataSource = null;
                            GrdPumpDetl.DataBind();
                        }
                    }
                    else
                    {
                        var lst = ObjDAL.BindTollPump(strchkDetlValue);
                        if (lst != null && lst.Count > 0)
                        {
                            DtTempToll = null;
                            DtTempToll = CreateDtToll();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "TollIdno"));
                                double Amnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Amount"))) ? 0.00 : Convert.ToDouble(DataBinder.Eval(lst[i], "Amount"));
                                ApplicationFunction.DatatableAddRow(DtTempToll, PbillIdno, Amnt);
                            }
                            ViewState["dtToll"] = DtTempToll;

                            GrdTollDetl.DataSource = lst;
                            GrdTollDetl.DataBind();
                        }
                        else
                        {
                            GrdTollDetl.DataSource = null;
                            GrdTollDetl.DataBind();
                        }
                    }
                    grdDocdetals.DataSource = null;
                    grdDocdetals.DataBind();
                    ObjDAL = null;
                }
            }
        }

        protected void lnkBtnSearch_Click(object sender, EventArgs e)
        {
            grdDocdetals.DataSource = null;
            grdDocdetals.DataBind();
            if (ddlFromCity.SelectedIndex <= 0)
            {
                ddlFromCity.Focus();
                ShowMessageErr("Please Select From City !");
                return;
            }
            if (ddlTruckNo.SelectedIndex <= 0)
            {
                ddlTruckNo.Focus();
                ShowMessageErr("Please Select Lorry !");
                return;
            }
            ddlSearchtyp.Enabled = true; txtDateFrom.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openPartyModal()", true);
        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtExpAmnt.Text) <= 0) { ShowMessageErr("Please Enter Amount!."); txtExpAmnt.Focus(); return; }
            if (Hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowid.Value))
                    {
                        dtrow["Acnt_Name"] = ddlExpenese.SelectedItem.Text;
                        dtrow["Acnt_Idno"] = ddlExpenese.SelectedValue;
                        dtrow["Amnt"] = Convert.ToString(txtExpAmnt.Text);
                    }
                }
            }
            else
            {
                DtTemp = (DataTable)ViewState["dt"];
                Int32 ROWCount = Convert.ToInt32(DtTemp.Rows.Count) - 1;
                int id = DtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(DtTemp.Rows[ROWCount]["id"])) + 1;
                string strAcntName = ddlExpenese.SelectedItem.Text.Trim();
                string strAcntIdno = string.IsNullOrEmpty(ddlExpenese.SelectedValue) ? "0" : (ddlExpenese.SelectedValue);
                string strAmount = Convert.ToString(txtExpAmnt.Text);
                if (DtTemp != null && DtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < DtTemp.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(strAcntIdno) == Convert.ToInt64(DtTemp.Rows[i]["Acnt_Idno"].ToString()))
                        {
                            this.ShowMessageErr("already exists in Expense Details.");
                            return;
                        }
                    }
                }
                ApplicationFunction.DatatableAddRow(DtTemp, id, strAcntName, strAcntIdno, strAmount);
                ViewState["dt"] = DtTemp;
            }

            this.BindGridExpn();
            ddlExpenese.Focus();
            ddlExpenese.SelectedIndex = 0;
            txtExpAmnt.Text = "0.00";
        }

        protected void lnkBtnDetlSearch_Click(object sender, EventArgs e)
        {
            TripEntryDAL ObjDAl = new TripEntryDAL();
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
            var GrdDetl = ObjDAl.SearchGrD(Convert.ToInt32(ddlSearchtyp.SelectedValue), dtfrom, dtto, (string.IsNullOrEmpty(txtDocNo.Text) ? 0 : Convert.ToInt32(txtDocNo.Text)), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt64(ddlTruckNo.SelectedValue));
            if (GrdDetl != null)
            {
                grdDocdetals.DataSource = GrdDetl;
                grdDocdetals.DataBind();
                lnkDocOk.Visible = true; lnkBtnClose.Visible = true;
                ddlSearchtyp.Enabled = false;
            }
            else
            {
                grdDocdetals.DataSource = null;
                grdDocdetals.DataBind();
                lnkDocOk.Visible = false; lnkBtnClose.Visible = false;
                ddlSearchtyp.Enabled = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openPartyModal()", true);
        }

        protected void lnkDocOk_Click(object sender, EventArgs e)
        {
            if ((grdDocdetals != null) && (grdDocdetals.Rows.Count > 0))
            {
                string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                Int64[] strchkDetlValue = new Int64[grdDocdetals.Rows.Count];
                int j = 0;
                for (int count = 0; count < grdDocdetals.Rows.Count; count++)
                {
                    CheckBox ChkGr = (CheckBox)grdDocdetals.Rows[count].FindControl("chkId");
                    if ((ChkGr != null) && (ChkGr.Checked == true))
                    {
                        HiddenField hidFirstDrvidno = (HiddenField)grdDocdetals.Rows[0].FindControl("hidDrvidno");
                        HiddenField hidDrvidno = (HiddenField)grdDocdetals.Rows[count].FindControl("hidDrvidno");
                        HiddenField hidDocIdno = (HiddenField)grdDocdetals.Rows[count].FindControl("hidDocIdno");
                        if (hidFirstDrvidno.Value != hidDrvidno.Value)
                        {
                            this.ShowMessageErr("Selected Challan Driver is not Same.");
                            return;
                        }
                        strchkDetlValue[j] = Convert.ToInt64(hidDocIdno.Value);
                        j++;
                    }
                }

                if (strchkDetlValue.Length == 0)
                {
                    ShowMessageErr("Please select atleast one Document.");
                    ShowDiv("ShowClient('dvGrdetails')");
                }
                else
                {
                    TripEntryDAL ObjDAL = new TripEntryDAL();
                    if (Convert.ToInt32(ddlSearchtyp.SelectedValue) == 0 || Convert.ToInt32(ddlSearchtyp.SelectedValue) == 3)
                    {
                        var ChlnDetl = ObjDAL.BindChallan(strchkDetlValue);
                        if (ChlnDetl != null && ChlnDetl.Count > 0)
                        {
                            string Driver = string.Empty;
                            DtTempChln = null;
                            DtTempChln = CreateDtChln();
                            for (int i = 0; i < ChlnDetl.Count; i++)
                            {
                                HidAdvAmnt.Value = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "Advance"));
                                string ChlnIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "ChlnIdno"));
                                string DriverIdno = Convert.ToString(DataBinder.Eval(ChlnDetl[i], "DriverIdno"));
                                ApplicationFunction.DatatableAddRow(DtTempChln, ChlnIdno, DriverIdno);
                                Driver = Convert.ToString(DataBinder.Eval(ChlnDetl[0], "DriverIdno"));
                            }
                            ddldriverName.SelectedValue = Driver;
                            ViewState["dtChln"] = DtTempChln;
                            grdChlnDetl.DataSource = ChlnDetl;
                            grdChlnDetl.DataBind();

                            CalculateKms();
                        }
                        else
                        {
                            grdChlnDetl.DataSource = null;
                            grdChlnDetl.DataBind();
                        }

                    }
                    else if (Convert.ToInt32(ddlSearchtyp.SelectedValue) == 1)
                    {


                        var lst = ObjDAL.BindPetrolPump(strchkDetlValue);
                        if (lst != null && lst.Count > 0)
                        {
                            DtTempFuel = null;
                            DtTempFuel = CreateDtFuel();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "PbillIdno"));
                                double Amount = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Amount"))) ? 0.00 : Convert.ToDouble(DataBinder.Eval(lst[i], "Amount"));
                                ApplicationFunction.DatatableAddRow(DtTempFuel, PbillIdno,Amount);
                            }
                            ViewState["dtFuel"] = DtTempFuel;

                            GrdPumpDetl.DataSource = lst;
                            GrdPumpDetl.DataBind();
                        }
                        else
                        {
                            GrdPumpDetl.DataSource = null;
                            GrdPumpDetl.DataBind();
                        }
                    }
                    else
                    {
                        var lst = ObjDAL.BindTollPump(strchkDetlValue);
                        if (lst != null && lst.Count > 0)
                        {
                            DtTempToll = null;
                            DtTempToll = CreateDtToll();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                string PbillIdno = Convert.ToString(DataBinder.Eval(lst[i], "TollIdno"));
                                double Amnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Amount"))) ? 0.00 :  Convert.ToDouble(DataBinder.Eval(lst[i], "Amount"));
                                ApplicationFunction.DatatableAddRow(DtTempToll, PbillIdno, Amnt);
                            }
                            ViewState["dtToll"] = DtTempToll;

                            GrdTollDetl.DataSource = lst;
                            GrdTollDetl.DataBind();
                        }
                        else
                        {
                            GrdTollDetl.DataSource = null;
                            GrdTollDetl.DataBind();
                        }
                    }
                    grdDocdetals.DataSource = null;
                    grdDocdetals.DataBind();
                    ObjDAL = null;
                }
                lnkBtnSearch.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openPartyModal()", true);
            }
        }

        protected void lnkBtnClose_Click(object sender, EventArgs e)
        {
            txtDocNo.Text = "";
            ddlSearchtyp.SelectedIndex = 0;
            ddlSearchtyp.Enabled = true;
            grdDocdetals.DataSource = null;
            grdDocdetals.DataBind(); lnkDocOk.Visible = false; lnkBtnClose.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "openPartyModal()", true);
            ddlSearchtyp.Focus();
        }

        protected void lnkBtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("TripEntry.aspx");
        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            if (grdChlnDetl.Rows.Count <= 0)
            {
                ShowMessageErr("Please Select Challan Details!");
                return;
            }
            Int64 intTripIdno = 0;
            //if (Page.IsValid)
            
            
            //    using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    TripEntryDAL objDAL = new TripEntryDAL();
                    tblTripHead TripHead = new tblTripHead();
                    TripHead.Trip_No = Convert.ToString(txtTripNo.Text);
                    TripHead.Trip_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim().ToString()));
                    TripHead.BaseCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                    TripHead.Truck_Idno = Convert.ToInt64(ddlTruckNo.SelectedValue);
                    TripHead.Driver_Idno = Convert.ToInt64(ddldriverName.SelectedValue);
                    TripHead.StartKms = Convert.ToString(txtStratKms.Text);
                    TripHead.EndKms = Convert.ToString(txtEndKms.Text);
                    TripHead.FKms = Convert.ToString(txtKMS.Text);
                    TripHead.Year_Idno = Convert.ToInt64(ddldateRange.SelectedValue);
                    TripHead.Gross_Amnt = Convert.ToDouble(txtGrosstotal.Text);
                    TripHead.Insentive_Amnt = Convert.ToDouble(txtInsentive.Text);
                    TripHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                    TripHead.remark = Convert.ToString(txtRemarks.Text);
                    TripHead.RcptType_Idno = Convert.ToInt32((ddlRcptType.SelectedIndex < 0) ? "0" : Request.Form[ddlRcptType.UniqueID]);
                    TripHead.Inst_No = Convert.ToInt32(((txtInstNo.Text == "") ? "0" : Request.Form[txtInstNo.UniqueID]));
                    TripHead.Bank_Idno = Convert.ToInt32((ddlCusBank.SelectedIndex < 0) ? "0" : Request.Form[ddlCusBank.UniqueID]);


                     TripHead.Adv_Amnt = Convert.ToDouble(txtAdvAmnt.Text);
                    

                    
                    if (Request.Form[txtInstDate.UniqueID] == null)
                    {
                        TripHead.Inst_Dt = null;
                    }
                    else
                    {
                        TripHead.Inst_Dt = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
                    }
                    DtTempChln = (DataTable)ViewState["dtChln"];
                    DtTempFuel = (DataTable)ViewState["dtFuel"];
                    DtTempToll = (DataTable)ViewState["dtToll"];
                    DtTemp = (DataTable)ViewState["dt"];
                    if ((DtTempChln != null && DtTempChln.Rows.Count>0) && ((DtTempFuel != null && DtTempFuel.Rows.Count > 0) || (DtTempToll != null && DtTempToll.Rows.Count > 0) || (DtTemp != null && DtTemp.Rows.Count > 0)))
                    {
                        if (string.IsNullOrEmpty(hidid.Value) == true)
                        {
                            intTripIdno = objDAL.Insert(TripHead, DtTempChln, DtTempFuel, DtTemp, DtTempToll);
                        }
                        else
                        {
                            intTripIdno = objDAL.Update(TripHead, DtTempChln, DtTempFuel, DtTemp, DtTempToll, Convert.ToInt64(hidid.Value));
                        }
                        if (intTripIdno > 0)
                        {
                            if (this.PostIntoAccounts(DtTemp, DtTempToll, Convert.ToInt32(ddldriverName.SelectedValue), intTripIdno, Convert.ToString(txtDate.Text.Trim()), Convert.ToString(txtTripNo.Text.Trim()), Convert.ToInt32(ddldateRange.SelectedValue), (string.IsNullOrEmpty(Convert.ToString(Session["UserIdno"])) ? 0 : Convert.ToInt32(Session["UserIdno"])), Convert.ToDouble(txtAdvAmnt.Text), Convert.ToInt32((ddlRcptType.SelectedIndex < 0) ? "0" : Request.Form[ddlRcptType.UniqueID]), Convert.ToInt32(((txtInstNo.Text == "") ? "0" : Request.Form[txtInstNo.UniqueID])), Convert.ToInt32((ddlCusBank.SelectedIndex < 0) ? "0" : Request.Form[ddlCusBank.UniqueID]), Convert.ToString(txtInstDate.Text.Trim())) == true)
                            {
                                if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    this.ShowMessage("Record updated successfully.");
                                }
                                else
                                {
                                    this.ShowMessage("Record saved successfully.");
                                }
                                Clear();
                          //    Tran.Complete(); Tran.Dispose();
                            }
                            else
                            {
                                this.ShowMessageErr(hidepostingMsg.Value);
                        //       Tran.Dispose();
                            }
                        }
                        else if (intTripIdno == -1)
                        {
                            this.ShowMessage("Record Already Exist!");
                     //     Tran.Dispose();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {
                                this.ShowMessage("Record Not Updated!");
                            }
                            else
                            {
                                this.ShowMessage("Record Not Saved!");
                            }
                     //   Tran.Dispose();
                        }
                    }
                    else
                    {
                        ShowMessageErr("Plaese enter expenses.");
                    }
                }
            
        }

        protected void lnkBtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidid.Value) == true)
            {
                this.Clear();
            }
            else
            {
                this.Populate(Convert.ToInt64(hidid.Value) == 0 ? 0 : Convert.ToInt64(hidid.Value));
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "CallPrint('print')", true);
        }
        #endregion

        #region Calculation By Salman..........
        private void CalculateAmount()
        {
            if (Convert.ToDouble(HidexpTotal.Value) > Convert.ToDouble(HidAdvAmnt.Value))
            {
                txtAdvAmnt.Enabled = true;
                double PayTodriver = Convert.ToDouble(HidexpTotal.Value) - Convert.ToDouble(HidAdvAmnt.Value);
                HidActualamnt.Value = Convert.ToDouble(PayTodriver).ToString("N2");
                lblAdvAmnt.Text = "Pay to Driver";
                HidPaytype.Value = lblAdvAmnt.Text;
                txtAdvAmnt.Text = Convert.ToDouble(PayTodriver).ToString("N2");
            }
            else if (Convert.ToDouble(HidAdvAmnt.Value) > Convert.ToDouble(HidexpTotal.Value))
            {
                txtAdvAmnt.Enabled = true;
                double RecFdriver = Convert.ToDouble(HidAdvAmnt.Value) - Convert.ToDouble(HidexpTotal.Value);
                HidActualamnt.Value = Convert.ToDouble(RecFdriver).ToString("N2");
                lblAdvAmnt.Text = "Rec From Driver";
                HidPaytype.Value = lblAdvAmnt.Text;
                txtAdvAmnt.Text = Convert.ToDouble(RecFdriver).ToString("N2") ;
            }
            else
            {
                txtAdvAmnt.Enabled = false;
                txtAdvAmnt.Text = "0.00";
            }



        }
        
        private void BindRcpt()
        {
            TripEntryDAL obj = new TripEntryDAL();
            DataTable dtRcpt = obj.BindRcptType(ApplicationFunction.ConnectionString());
            if (dtRcpt != null && dtRcpt.Rows.Count > 0)
            {
                ddlRcptType.DataSource = null;
                ddlRcptType.DataSource = dtRcpt;
                ddlRcptType.DataTextField = "ACNT_NAME";
                ddlRcptType.DataValueField = "Acnt_Idno";
                ddlRcptType.DataBind();

            }

            ddlRcptType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindBank()
        {
            TripEntryDAL obj = new TripEntryDAL();
            DataTable dtBank = obj.BindBank(ApplicationFunction.ConnectionString());
            if (dtBank != null && dtBank.Rows.Count > 0)
            {
                ddlCusBank.DataSource = null;
                ddlCusBank.DataSource = dtBank;
                ddlCusBank.DataTextField = "ACNT_NAME";
                ddlCusBank.DataValueField = "Acnt_Idno";
                ddlCusBank.DataBind();

            }
            ddlCusBank.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        // By Salman
        [WebMethod]
        public static IList ProductList(string cust)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            IList ilist = new List<string>();
            //  DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32(Request.Form[ddlRcptType.UniqueID]), ApplicationFunction.ConnectionString());
            DataTable Product = obj.BindRcptTypeDetail(Convert.ToInt32(cust), ApplicationFunction.ConnectionString());
            if (Product != null && Product.Rows.Count > 0)
            {
                ilist.Add(Convert.ToString(Product.Rows[0]["ACNT_TYPE"]));

            }
            return ilist;
        }
        #endregion

    }
}

