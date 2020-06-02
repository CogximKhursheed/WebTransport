using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Transactions;
using System.Web.Services;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class HireInvoice : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable();
        DataTable dtTemp = new DataTable(); string con = "";
        private int intFormId = 72;
        double dTOtAmnt = 0, GrdTotalAmount = 0, Netamount=0 ,Advanceamnt=0, Dieselamnt = 0 ,grossamount=0; bool IsWeight = false; Double iRate = 0.00; Double dtotalAmount = 0;
        InvoiceDAL objInvoiceDAL = new InvoiceDAL();
        Int32 totrecords = 0; Double iqty = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0;
        double dblTtAmnt = 0; double dtotul = 0; double dtot = 0; double dul = 0; double add = 0; 
        Double AdvAMnt = 0; Int64 iprintType = 0; double totprintweight = 0, totprintshortage = 0, printtotamnt = 0, printctax = 0, printstax = 0, printrcptAmnt = 0, printnetamnt = 0;
        #endregion

        #region Page Enents...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                tblUserPref obj = objInvoiceDAL.SelectUserPref();
                hidPrintType.Value = Convert.ToString(obj.InvPrint_Type);
                hidrefno.Value = Convert.ToString(obj.Reflabel_Gr);
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
                //    lblViewList.Visible = false;
                //}
                maxno();
                this.BindDateRange();
                BindRcpt();
                BindBank();
                BindDropdown();
                BindUnit();
                BindItemInsert();
                //ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);

                if (Request.QueryString["HI"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["HI"]));
                    ddldateRange.Enabled = false;
                    lnkbtnNew.Visible = true;
                    lnkBtnLast.Visible = false;
                    ImgPrint2.Visible = true;
                    PrintInvGeneral(Convert.ToInt64(Request.QueryString["HI"]));
                }
                else
                {
                    lnkbtnNew.Visible = false;
                    lnkBtnLast.Visible = true;
                    ImgPrint2.Visible = false;
                }
                txtinvoicNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtdate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtAdvAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

            }
        }
        #endregion

        #region Functions...

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddldateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
            objDAL = null;
        }
        
        #region Control..
        // By Salman
        [WebMethod]
        public static IList ProductList(string cust)
        {
            HireInvDAL obj = new HireInvDAL();
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
        private void BindRcpt()
        {
            HireInvDAL obj = new HireInvDAL();
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
            HireInvDAL OBJ = new HireInvDAL();
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
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            var LorrMast = objLorrMast.SelectPartyName();
            var senderLst = obj.BindSender();
            var TruckNolst = obj.BindTruckNo();
            var ToCity = obj.BindAllToCity();
            var UnitName = obj.BindUnitName();
            var lst = obj.BindLocFrom();
            obj = null;
            objLorrMast = null;


            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


            ddlpartyname.DataSource = LorrMast;
            ddlpartyname.DataTextField = "Acnt_Name";
            ddlpartyname.DataValueField = "Acnt_Idno";
            ddlpartyname.DataBind();
            ddlpartyname.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlviacity.DataSource = ToCity;
            ddlviacity.DataTextField = "city_name";
            ddlviacity.DataValueField = "city_idno";
            ddlviacity.DataBind();
            ddlviacity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlFromCity.DataSource = lst;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));

            ddllocation.DataSource = lst;
            ddllocation.DataTextField = "City_Name";
            ddllocation.DataValueField = "City_Idno";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new ListItem("--Select--", "0"));

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
        private void BindUnit()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var UnitName = obj.BindUnitName();
            ddlunitname.DataSource = UnitName;
            ddlunitname.DataTextField = "UOM_Name";
            ddlunitname.DataValueField = "UOM_idno";
            ddlunitname.DataBind();
            ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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


                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateFrm.Text = Convert.ToString(hidmindate.Value);
                    txtdate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDatetwo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateFrm.Text = Convert.ToString(hidmindate.Value);
                    txtdate.Text = Convert.ToString(hidmaxdate.Value);
                    txtDatetwo.Text = Convert.ToString(hidmaxdate.Value);
                }
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

        HireInvDAL obj = new HireInvDAL();
        public void maxno()
        {
            txtinvoicNo.Text = Convert.ToString(obj.MaxHireNo());
        }
        private void Clear()
        {
            ddlFromCity.Enabled = true;
            ddlpartyname.SelectedValue = "0";
            ViewState["dt"] = null;
            DtTemp = null;
            txtinvoicNo.Text = "";
            ddldateRange.Enabled = true;
            //ddldateRange.SelectedIndex = 0;

            ddlpartyname.Enabled = true;
            tblUserPref obj = objInvoiceDAL.SelectUserPref();
            ddlFromCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "id", "String",
                "Truck_Idno", "String",
                "TruckName", "String",
                "Party_Idno", "String",
                "PartyName", "String",
                "FrmCity_idno", "String",
                "FrmCityName", "String",

                  "ViaCity_Idno", "String",
                "ViaCityName", "String",

                "ToCity_Idno", "String",
                "ToCityName", "String",

                "From_Date", "String",
                "Return_Date", "String",
                "Amount", "String"
                );
            return dttemp;
        }
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }
        private void ClearItems()
        {
            txtDateFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //txtDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ddlToCity.SelectedIndex = ddlFromCity.SelectedIndex = ddlpartyname.SelectedIndex = 0;
            Hidrowid.Value = string.Empty;
            ViewState["id"] = null;

        }
        private void EditGridFunction()
        {
            int id = 0;
            if (ViewState["id"] != null)
                id = Convert.ToInt32(ViewState["id"].ToString());

            DtTemp = (DataTable)ViewState["dt"];
            DataRow[] drs = DtTemp.Select("Id='" + id + "'");

            if (drs.Length > 0)
            {
                ddlTruckNo.SelectedValue = Convert.ToString(drs[0]["Truck_Idno"]);
                ddlpartyname.SelectedValue = Convert.ToString(drs[0]["Party_Idno"]);
                ddlFromCity.SelectedValue = Convert.ToString(drs[0]["FrmCity_idno"]);
                ddlToCity.SelectedValue = Convert.ToString(drs[0]["ToCity_Idno"]);
                ddlviacity.SelectedValue = Convert.ToString(drs[0]["ViaCity_Idno"]);

                txtDateFrom.Text = Convert.ToDateTime(drs[0]["From_Date"]).ToString("dd-MM-yyyy");
                txtDateTo.Text = string.IsNullOrEmpty(Convert.ToString(drs[0]["Return_Date"])) ? "" : Convert.ToDateTime(drs[0]["Return_Date"]).ToString("dd-MM-yyyy");

                Hidrowid.Value = Convert.ToString(drs[0]["id"]);
               // txtamount.Text = Convert.ToDouble(drs[0]["Amount"]).ToString("N2");
            }
            ddldateRange.Focus();
            BindGrid();
        }

        private void BindGride()
        {
            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();

            GrMainDetail.DataSource = (DataTable)ViewState["dtt"];
            GrMainDetail.DataBind();


        }

        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            if (e.CommandName == "cmdedit")
            {
                EditGridFunction();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataRow drr = DtTemp.Select("id='" + ViewState["id"].ToString() + "'").Single();
                drr.Delete();
                DtTemp.AcceptChanges();
                ViewState["dt"] = DtTemp;
                this.BindGrid();
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                totrecords = grdMain.Rows.Count + 1;

                string Truckidno = Convert.ToString(string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? "0" : ddlTruckNo.SelectedValue);
                string Location = Convert.ToString(string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? "0" : ddlFromCity.SelectedValue);
              //  GrdTotalAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotRecords = (Label)e.Row.FindControl("lbltotRecords");
                lbltotRecords.Text = Convert.ToString(totrecords);

                //Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                //lblAmount.Text = GrdTotalAmount.ToString("N2");
                //hidgrossamnt.Value = GrdTotalAmount.ToString("N2");

            }
           // txttotalamount.Text = GrdTotalAmount.ToString("N2");
        }
        #endregion

        #region Button Event...
        
        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0")
            {
                ShowMessageErr("Please Select From City for Last Print.");
            }
            else
            {
                BindDropdownDAL objCom = new BindDropdownDAL();
                HireInvDAL objDAL = new HireInvDAL();
                Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
                iMaxInvIdno1.Value = Convert.ToString(iMaxInvIdno);
                if (iMaxInvIdno > 0)
                {
                    Int64 senderidno = objDAL.sender(ApplicationFunction.ConnectionString(), iMaxInvIdno);
                    Int64 printFormat = 0; //Only for general print format

                    printFormat1.Value = Convert.ToString(printFormat);
                    //if (Convert.ToInt32(printFormat) > 0)
                    //{
                    //    hidePrintMultipal.Value = Convert.ToString(1);
                    //    string lst1 = objCom.SelectPages(Convert.ToInt64(printFormat));
                    //    objCom = null;
                    //    if (string.IsNullOrEmpty(lst1) == false)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Printwith();", true);
                    //    }
                    //}
                    //else
                    //{
                        PrintInvGeneral(iMaxInvIdno);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint()", true);
                    //}
                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                printtotamnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Gross_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lblTotalAmnt.Text = printtotamnt.ToString("N2");
            }
        }
        #region General Print By Upadhyay Puneet
        private void PrintInvGeneral(Int64 GRHeadIdno)
        {
            Repeater obj = new Repeater();
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string ServTaxNo = ""; string FaxNo = ""; 
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
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
            //Loadimage();
            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrintGeneralHire',@Id='" + GRHeadIdno + "'");
            
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                valuelbltxtPartyName.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
                valuelblnetamntAtbttm.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Net_Amnt"]);
                valuelblAdvAmnt.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Adv_Amnt"]);
                lblPartyAddress1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Add1"]) + ",";
                lblPartyAddress2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Add2"]);
            }
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");

                Repeater2.DataSource = dsReport.Tables[0];
                Repeater2.DataBind();
            }
         
        }
        #endregion
      
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (ddllocation.SelectedIndex == 0) { ShowMessageErr("Please select Location"); ddllocation.Focus(); return; }
            Int64 varForSave = 0;
            DateTime? InvoiceDate = null;
            DateTime? InstDate = null;
            DateTime? Datefrom = null;
            DateTime? ReturnDate = null;
            Int32 instno = 0;
            Int32 recpttypeidno = 0 ,bankidno=0;

            Int32 locidno = Convert.ToInt32(Convert.ToString(ddllocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddllocation.SelectedValue));
            Int64 Partyidno = Convert.ToInt32(Convert.ToString(ddlpartyname.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlpartyname.SelectedValue));
            Int64 FromCityidno = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
            Int64 CityToidno = Convert.ToInt32(Convert.ToString(ddlToCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlToCity.SelectedValue));
            Int64 ViaCityidno = Convert.ToInt32(Convert.ToString(ddlviacity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlviacity.SelectedValue));
            Int64 Truckidno = Convert.ToInt32(Convert.ToString(ddlTruckNo.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue)); 
            Int64 useridno = Convert.ToInt64(Session["UserIdno"]);
            InvoiceDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdate.Text));
            Datefrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                ReturnDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            bool Accposting = false;
            Int32 CompIdno = 0;
            //Netamount = string.IsNullOrEmpty(txttotalamount.Text.Trim()) ? 0.00 : Convert.ToDouble(txttotalamount.Text.Trim());
            Netamount = string.IsNullOrEmpty(txttotalamount.Text.Trim()) ? 0.00 : Convert.ToDouble(Request.Form[txttotalamount.UniqueID]);
            //
            Advanceamnt = string.IsNullOrEmpty(txtAdvAmnt.Text.Trim()) ? 0.00 : Convert.ToDouble(Request.Form[txtAdvAmnt.UniqueID]);
            Dieselamnt = string.IsNullOrEmpty(txtdieselamt.Text.Trim()) ? 0.00 : Convert.ToDouble(Request.Form[txtdieselamt.UniqueID]);
            grossamount = string.IsNullOrEmpty(hidgrossamnt.Value.Trim()) ? 0.00 : Convert.ToDouble(hidgrossamnt.Value.Trim());

            if (Netamount <= 0)
            {
                this.ShowMessageErr("Total Amount Can't Be Zero Amonunt");
                return;
            }
            if ((Advanceamnt) > (Convert.ToDouble(hidgrossamnt.Value)))
            {
                this.ShowMessageErr("Advance Amount Can't be greater than total amount");
                return ;
            }

            if (Request.Form[txtInstDate.UniqueID] == null)
            {
                InstDate = null;
            }
            else
            {
                InstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
            }
            recpttypeidno= Convert.ToInt32((ddlRcptType.SelectedIndex < 0) ? "0" : Request.Form[ddlRcptType.UniqueID]);
            bankidno= Convert.ToInt32((ddlCusBank.SelectedIndex < 0) ? "0" : Request.Form[ddlCusBank.UniqueID]);
            instno = Convert.ToInt32(((txtInstNo.Text == "") ? "0" : Request.Form[txtInstNo.UniqueID]));
           
           // DataTable dtDetail = (DataTable)ViewState["dt"];
            DataTable  dtitemdetail = (DataTable)ViewState["dttemp"];
            HireInvDAL obj = new HireInvDAL();
            if (dtitemdetail != null)
            {
                if (dtitemdetail.Rows.Count > 0)
                {

                   using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (string.IsNullOrEmpty(HidHireIdno.Value) != true)
                        {
                            varForSave = obj.Update(Convert.ToInt64(HidHireIdno.Value), Convert.ToInt64(ddldateRange.SelectedValue), InvoiceDate, Convert.ToInt64(txtinvoicNo.Text), locidno, useridno, CompIdno, Accposting, txtremarks.Text,Truckidno, Partyidno, FromCityidno, CityToidno, ViaCityidno, Datefrom, ReturnDate, Netamount, Advanceamnt, Dieselamnt, recpttypeidno, instno, InstDate, bankidno, grossamount, HidGrIdno.Value, dtitemdetail, ApplicationFunction.ConnectionString());
                            if (varForSave > 0)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(HidHireIdno.Value)) == false)
                                {
                                  Tran.Complete();
                                    lnkbtnNew.Visible = false; //lnkbtnPrint.Visible = false;
                                    msg = "Record(s) Updated Successfully.";
                                }
                                this.clearAllControl();
                                this.ShowMessage(msg);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(HidHireIdno.Value)) == false)
                                {
                                  Tran.Dispose();
                                    msg = "Record(s) Not Updated!";
                                }
                                ShowMessageErr(msg);
                            }

                        }
                        else
                        {
                            varForSave = obj.Insert(Convert.ToInt64(ddldateRange.SelectedValue), InvoiceDate, Convert.ToInt64(txtinvoicNo.Text), locidno, useridno, CompIdno, Accposting, txtremarks.Text, Truckidno, Partyidno, FromCityidno,CityToidno,ViaCityidno,Datefrom,ReturnDate, Netamount, Advanceamnt, Dieselamnt, recpttypeidno, instno, InstDate, bankidno,grossamount,HidGrIdno.Value, dtitemdetail,ApplicationFunction.ConnectionString());
                            if (varForSave > 0)
                            {

                                if (string.IsNullOrEmpty(Convert.ToString(HidHireIdno.Value)) == true)
                                {
                                    msg = "Record(s) Saved Successfully.";
                                 Tran.Complete();
                                 Tran.Dispose();
                                }
                                this.clearAllControl();
                                this.ShowMessage(msg);
                                maxno();
                            }
                            else
                            {
                                this.ShowMessageErr("Record Not Saved!");
                            }
                        }
                    }

                }
            }
            else
            {
                this.ShowMessageErr("Please Select Lorry Details!");
            }

        }
        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HidHireIdno.Value) == true)
            {
                Response.Redirect("HireInvoice.aspx");
            }
            else
            {
                this.Populate(Convert.ToInt32(HidHireIdno.Value) == 0 ? 0 : Convert.ToInt32(HidHireIdno.Value));
            }
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            clearAllControl();
            lnkbtnNew.Visible = false;
        }

        // By salman
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlTruckNo.SelectedIndex <= 0)
            {
                ShowMessageErr("Please Select From City");
                return;
            } 

               ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                HireInvDAL obj = new HireInvDAL();
                 DataTable DsGrdetail;

                 DsGrdetail = obj.SelectGrPrep(Convert.ToInt64(ddldateRange.SelectedValue), Convert.ToString(txtDateFrm.Text.Trim()), Convert.ToString(txtDatetwo.Text), ApplicationFunction.ConnectionString());
                 if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    //lnkbtnSubmit.Visible = true;
                    lnkbtnCloase.Visible = true;

                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    //lnkbtnSubmit.Visible = false;
                    lnkbtnCloase.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                lnkbtnSearch.Focus();
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }

        protected void lnkbtenSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL objbind1 = new BindDropdownDAL();
                Int32 iRateType = 0; double dcommssn = 0, dweight = 0, dqty = 0, dtotcommssn = 0;
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

                            HidGrIdno.Value = strchkDetlValue;
                        }
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Gr.");
                        ShowDiv("ShowClient('dvGrdetails')");
                    }
                    else
                    {
                        HireInvDAL ObjChlnBookingDAL = new HireInvDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable();
                        DataTable dtKMDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = ObjChlnBookingDAL.SelectGrONtoHireDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));

                        

                      
                        ViewState["dtt"] = dtRcptDetl;
                        BindGride();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                    }
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }


        }

        #endregion

        #region Control Events...
        private void clearAllControl()
        {
            ddlviacity.SelectedValue = ddlToCity.SelectedValue = ddlTruckNo.SelectedValue = ddlFromCity.SelectedValue = "0"; ddlpartyname.SelectedValue = "0";
            ddlRcptType.SelectedIndex = ddlCusBank.SelectedIndex = 0; txtInstNo.Text = "";ddlviacity.SelectedValue="0" ;
            ddlRcptType.Enabled = false;
            hidgrossamnt.Value = "0.00";
            txtAdvAmnt.Text = "0.00";
            txtremarks.Text = "";
            txttotalamount.Text = "0.00";
            txtdieselamt.Text = "0.00";
            HidHireIdno.Value = string.Empty;
            DtTemp = null; ViewState["dt"] = null; this.BindGrid();
            dtTemp = null; ViewState["dttemp"] = null; this.BindGridT();

            GrMainDetail.DataSource = null;
            GrMainDetail.DataBind();
        }
        private void Populate(Int64 HireHead_Idno)
        {
            HireInvDAL Obj = new HireInvDAL();
            TblHireInvHead ObjHead = Obj.SelectHireHead(HireHead_Idno);
            DataTable objhireDetl = obj.SelecthireDetail(HireHead_Idno, ApplicationFunction.ConnectionString());
            if (ObjHead != null)
            {
                ddldateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtinvoicNo.Text = ObjHead.Hire_InvNo.ToString();
                txtdate.Text = string.IsNullOrEmpty(ObjHead.Hire_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.Hire_Date).ToString("dd-MM-yyyy");
                ddllocation.SelectedValue = ObjHead.Loc_Idno.ToString();
                HidHireIdno.Value = Convert.ToString(ObjHead.Hire_Idno);
                txtremarks.Text = ObjHead.Remark.ToString();
                ddlTruckNo.SelectedValue = Convert.ToString(ObjHead.Truck_Idno);
                ddlpartyname.SelectedValue = Convert.ToString(ObjHead.Party_Idno);
                ddlFromCity.SelectedValue = Convert.ToString(ObjHead.LocFrm_Idno);
                ddlToCity.SelectedValue = Convert.ToString(ObjHead.ToCity_Idno);
                ddlviacity.SelectedValue = Convert.ToString(ObjHead.ViaCity_Idno);
                txtDateFrom.Text = string.IsNullOrEmpty(ObjHead.DateFrom.ToString()) ? "" : Convert.ToDateTime(ObjHead.DateFrom).ToString("dd-MM-yyyy");
                txtDateTo.Text = string.IsNullOrEmpty(ObjHead.ReturnDate.ToString()) ? "" : Convert.ToDateTime(ObjHead.ReturnDate).ToString("dd-MM-yyyy");
                //txttotalamount.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
                //hidgrossamnt.Value = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
                txtAdvAmnt.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Adv_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Adv_Amnt);
                txtdieselamt.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Diesel_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Diesel_Amnt);
                hidgrossamnt.Value = string.IsNullOrEmpty(Convert.ToString(ObjHead.Gross_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Gross_Amnt);
                if (Convert.ToDouble(ObjHead.Adv_Amnt) > 0)
                {
                    ddlRcptType.SelectedValue = Convert.ToString(ObjHead.RcptType_Idno);
                    ddlCusBank.SelectedValue = Convert.ToString(ObjHead.Bank_Idno);
                }
                else
                {
                    ddlRcptType.SelectedIndex = 0;
                    ddlCusBank.SelectedIndex = 0;
                }
                txtInstNo.Text = Convert.ToString(ObjHead.Inst_No);
                txtInstDate.Text = ((ObjHead.Inst_Dt == null) ? "" : Convert.ToDateTime(ObjHead.Inst_Dt).ToString("dd-MM-yyyy"));

                dtTemp = CreateDtnew();
                for (int counter = 0; counter < objhireDetl.Rows.Count; counter++)
                {
                    string strItemName = Convert.ToString(objhireDetl.Rows[counter]["Item_Name"]);
                    string strItemNameId = Convert.ToString(objhireDetl.Rows[counter]["Item_Idno"]);
                    string strUnitName = Convert.ToString(objhireDetl.Rows[counter]["UOM_Name"]);
                    string strUnitNameId = Convert.ToString(objhireDetl.Rows[counter]["Unit_Idno"]);
                    string strRateType = Convert.ToString(objhireDetl.Rows[counter]["Rate_Type"]);
                    string strRateTypeIdno = Convert.ToString(objhireDetl.Rows[counter]["RateType_Idno"]);
                    string strQty = Convert.ToString(objhireDetl.Rows[counter]["Qty"]);
                    string strWeight = Convert.ToString(objhireDetl.Rows[counter]["Tot_Weght"]);
                    string strRate = Convert.ToString(objhireDetl.Rows[counter]["Item_Rate"]);
                    string strAmount = Convert.ToString(objhireDetl.Rows[counter]["Amount"]);
                    string strDetail = Convert.ToString(objhireDetl.Rows[counter]["Detail"]);
                    string strul = Convert.ToString(objhireDetl.Rows[counter]["UnloadWeight"]);

                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDetail, "", "", "", "", "", "", "", "", strul);
                }
                ViewState["dttemp"] = dtTemp;
                BindGridT();
                txttotalamount.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
                //hidgrossamnt.Value = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
            }
        }
          private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }
        #endregion

        #region New Section 
        private DataTable CreateDtnew()
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
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateEdit();
                txtQuantity.Focus();
            }
            catch (Exception Ex) { }
        }
        private void CalculateEdit()
        {
            double iRate = 0; double EditRate = 0;
            DateTime strDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdate.Text.Trim().ToString()));
            DateTime dtDate = strDate;
          
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
        private bool CheckDuplicatieItem()
        {
            bool value = true;
            DataTable dtTemp = (DataTable)ViewState["dttemp"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0)) { foreach (DataRow row in dtTemp.Rows) { if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim()))) { value = false; } } }
            if (value == false) { return false; }
            else { return true; }
        }

        private void FillRateWeightWiseRate()
        {
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdate.Text.Trim().ToString()));
            AdvBookGRDAL objGrprepDAL = new AdvBookGRDAL();
            if (txtweight.Text.Trim() != "" && Convert.ToDouble(txtweight.Text.Trim()) > 0.00)
            {
                iRate = objGrprepDAL.SelectItemWeightWiseRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), strGrDate, Convert.ToDecimal(txtweight.Text.Trim()), Convert.ToInt64(ddlpartyname.SelectedValue));
            }
            txtrate.Text = Convert.ToDouble(iRate > 0 ? iRate : 0.00).ToString("N2");
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
                HireInvDAL obj = new HireInvDAL();
                if (ddlTruckNo.SelectedIndex == 0) { ShowMessageErr("Please select Lorry No."); ddlTruckNo.Focus(); return; }
                ddlTruckNo.Focus();
            
            #region Check Item Duplicate on ItemName and ItemUnit


            if (Hidrowidno.Value == string.Empty)
            {
                if (CheckDuplicatieItem() == false) { this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type."); this.ClearItem(); ddlItemName.Focus(); } else { ddlunitname.Focus(); }
            }

            #endregion
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            if (ddlunitname.SelectedIndex == 0) { ShowMessageErr("Please select Unit "); ddlunitname.Focus(); return; }
            if (ddlRateType.SelectedIndex == 0) { ShowMessageErr("Please select the Rate Type."); ddlRateType.Focus(); return; }
            if (IsWeight == true)
                if (Convert.ToDouble(txtweight.Text.Trim()) <= 0) { ShowMessageErr("Weight should be greater than zero!"); txtweight.Focus(); return; }
            if (ddlRateType.SelectedIndex != 1) { if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0) { ShowMessageErr("Weight should be greater than zero!"); txtweight.Focus(); return; } }
            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero!"); txtQuantity.Focus(); return; }

            if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            {
                ShowMessageErr("Rate should be greater than zero!");
                txtrate.Focus();
                return;
            }
            
            CalculateEdit();
            string strAmount = "";
            if (Hidrowidno.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dttemp"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowidno.Value))
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
                        dtrow["UnloadWeight"] = txtul.Text.Trim();
                    }
                }
              
            }
            else
            {
                dtTemp = (DataTable)ViewState["dttemp"];
                if (dtTemp == null)
                {
                    dtTemp = CreateDtnew();
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
                string strunloading = string.IsNullOrEmpty(txtul.Text.Trim()) ? "0" : (txtul.Text.Trim());
                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDetail, "", "", "", "", "", "", "", "", strunloading);
                ViewState["dttemp"] = dtTemp;
            }
            //  ddlItemName_SelectedIndexChanged(null,null);
            this.BindGridT();
             ddlItemName.Focus();
            ClearItem();
        }
        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            this.ClearItem();
        }
        private void ClearItem()
        {
            Hidrowidno.Value = ""; 
            ddlItemName.Enabled = ddlunitname.Enabled = true;
            ddlItemName.SelectedIndex = 0;
           ddlRateType.SelectedIndex = 0;
            txtInstNo.Text = "";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtdetail.Text = ""; txtrate.Text = "0.00";  txtul.Text = "0";
        }
        private void BindGridT()
        {
            if (ViewState["dttemp"] != null)
            {
                dtTemp = (DataTable)ViewState["dttemp"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain2.DataSource = dtTemp;
                    grdMain2.DataBind();
                }
                else
                {
                    dtTemp = null;
                    grdMain2.DataSource = dtTemp;
                    grdMain2.DataBind();
                    
                }
            }
            else
            {
                dtTemp = null;
                grdMain2.DataSource = dtTemp;
                grdMain2.DataBind();
            }
        }
       
        #endregion
        #region Grid Event ...
        protected void grdMain2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dttemp"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dttemp"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    ddlunitname.SelectedValue = Convert.ToString(drs[0]["Unit_Idno"]);
                    ddlRateType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["Rate_TypeIdno"]) == "" ? 0 : drs[0]["Rate_TypeIdno"]);
                    ddlItemName.Enabled = false;
                    ddlunitname.Enabled = false;
                    Hidrowidno.Value = Convert.ToString(drs[0]["id"]);
                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])));
                    txtdetail.Text = Convert.ToString(drs[0]["Detail"]);
                    txtul.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["UnloadWeight"]) == "" ? 0 : Convert.ToDouble(drs[0]["UnloadWeight"])));
                    ddlRateType.Focus();
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDtnew();
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
                ViewState["dttemp"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
                ddlItemName.Focus();
            }
        }
        protected void grdMain2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain2.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (dtTemp == null)
            {
                dtTemp = CreateDtnew();
                ViewState["dttemp"] = dtTemp;
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
                }
                hidgrossamnt.Value = (dtotAmnt + add).ToString("N2");
                txttotalamount.Text = (dtotAmnt + add).ToString("N2");
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
    }
}