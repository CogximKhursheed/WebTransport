using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Transactions;

namespace WebTransport
{
    public partial class TyreIssue : Pagebase
    {
        //txtGrno==txtissueno
        #region Variable .....
        static FinYearA UFinYear = new FinYearA();
        // string con = ApplicationFunction.ConnectionString();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();
        //double dblTtAmnt = 0;
        //int rb = 0; Int32 iFromCity = 0, itruckcitywise = 0, iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0;
        //private int intFormId = 58;
        //string strSQL = "", sRenWages = "";// bool isTBBRate = false;
        //double dSurchgPer = 0, dWagsAmnt = 0, dBiltyAmnt = 0, dTolltax = 0;
        //double dSurgValue = 0, dSurgTmp = 0, t = 0;
        //Double iqty = 0; Double temp = 0, dServTaxPer = 0, dtotalAmount = 0;
        //double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;

        double dblTtAmnt = 0;
        int rb = 0; Int32 iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0;
        private int intFormId = 27;
        string strSQL = ""; double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;// bool isTBBRate = false;dtotlAmnt="";
        double dSurchgPer = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dtotalAmount = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;

        #endregion

        #region Page Load Event.....
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    imgBtnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}

                // ValidateControls();
                this.BindDropdown();
                this.BindDateRange();
                this.BindEmployee();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                txtGRDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                ddlDateRange.SelectedIndex = 0;
                ddlDateRange.Focus();
                HidiFromCity.Value = Convert.ToString(base.UserFromCity);
                ddlLocation.SelectedValue = Convert.ToString(HidiFromCity.Value);
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                string Matfrom = "BK";
                this.BindMaxNo(Matfrom, Convert.ToInt32((ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), intYearIdno);
                // EnableDisableAtLoad();
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                if (Request.QueryString["TyreIssue"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["TyreIssue"]));
                    imgPDF.Visible = false;
                    imgBtnNew.Visible = true;
                    imgPrint.Visible = true;
                    ddlDateRange.Enabled = false;
                    this.BindItemDropdown();
                }
                else
                {
                    imgPDF.Visible = false;
                    imgBtnNew.Visible = false;
                    imgPrint.Visible = false;
                    ddlDateRange.Enabled = true;
                    this.BindActiveItemDropdown();
                }
                SetDate();
                //    //AutofillDefault();
                //}
                //userpref();
            }
        }

        #endregion

        #region Button Event .....
        protected void ImgBtnLocation_Click(object sender, ImageClickEventArgs e)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var to = obj.BindLocFrom();
            ddlLocation.DataSource = to;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        protected void ImgBtnTruuckRefresh_Click(object sender, ImageClickEventArgs e)
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        protected void ImgBtnIssueTo_Click(object sender, ImageClickEventArgs e)
        {
            BindEmployee();
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
                { txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); }
                else { txtGRDate.Text = hidmindate.Value; }
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            { if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0) { ShowMessageErr("Weight should be greater than zero."); txtweight.Focus(); return; } }
            //  if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero."); txtQuantity.Focus(); return; }

            if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            {
                ShowMessageErr("Rate should be greater than zero.");
                txtrate.Focus();
                return;
            }



            else if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            {
                ShowMessageErr("Rate should be greater than zero.");
                txtrate.Focus();
                return;
            }

            //else
            //{
            //    txtrate.Text = "0.00";
            //}

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
                        dtrow["Quantity"] = Convert.ToString(1); iqty += Convert.ToDouble(1);
                        dtrow["Weight"] = txtweight.Text.Trim();
                        dtrow["Rate"] = txtrate.Text.Trim();
                        dtrow["Amount"] = dtotalAmount.ToString("N2");
                        dtrow["Serial_No"] = ddlSerialNo.SelectedItem.Text;
                        dtrow["Serial_Idno"] = ddlSerialNo.SelectedValue;
                        //kapil
                        //dtrow["Detail"] = string.Empty;
                        //dtrow["Detail"] = txtremark.Text.Trim();

                    }
                }

            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];

                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemNameId = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                //string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                string strQty = Convert.ToString(1);
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                string strSerialNo = string.IsNullOrEmpty(ddlSerialNo.SelectedItem.Text) ? "0" : (ddlSerialNo.SelectedItem.Text);
                string strSerialIdno = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? "0" : (ddlSerialNo.SelectedValue);

                strAmount = dtotalAmount.ToString("N2");
                //kapil
                //string strDetail = string.IsNullOrEmpty(txtremark.Text.Trim()) ? "" : (txtremark.Text.Trim());\
                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strQty, strWeight, strRate, strAmount, string.Empty, strSerialNo, strSerialIdno);
                //ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strQty, strWeight, strRate, strAmount, strDetail);
                ViewState["dt"] = dtTemp;
            }

            this.BindGridT();
            ddlItemName.Focus();
            ClearItems();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.ClearItems();
        }
        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {
            #region fill values to Variables
            TyreIssueDAL objGrprepDAL = new TyreIssueDAL();
            dtTemp = (DataTable)ViewState["dt"];
            //AcntDS = objGrprepDAL.DtAcntDS(ApplicationFunction.ConnectionString());
            //DsTrAcnt = objGrprepDAL.DsTrAcnt(ApplicationFunction.ConnectionString());
            #endregion

            #region Validation Messages

            //if (ddlReciver.SelectedIndex == 0) { this.ShowMessageErr("Please select Receiver's Name."); ddlReciver.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Receiver's Name."; return; }
            if (ddlLocation.SelectedIndex == 0) { this.ShowMessageErr("Please select Delivery Place."); ddlLocation.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Delivery Place."; return; } if ((dtTemp != null) && (dtTemp.Rows.Count == 0)) { this.ShowMessageErr("Please enter Item Details ."); return; }

            #endregion
            //txtGrNo=txtIssueNo 
            #region Declare Input Variables
            string strMsg = string.Empty;
            Int64 intMaterialIdno = 0;
            DateTime strMatDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtMatDate = strMatDate;
            Int32 IAgainst = Convert.ToInt32(1);
            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int64 intMatNo = string.IsNullOrEmpty(txtGRNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGRNo.Text.Trim());
            Int32 TruckNoIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);
            Int32 intIssueIDno = string.IsNullOrEmpty(ddlReciver.SelectedValue) ? 0 : Convert.ToInt32(ddlReciver.SelectedValue);
            Int32 intLoc_Id = string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt32(ddlLocation.SelectedValue);
            Int64 intDriver_Id = string.IsNullOrEmpty(ddlDriver.SelectedValue) ? 0 : Convert.ToInt32(ddlDriver.SelectedValue);
            //new 
            string strRemarkhead = txtRemarkhead.Text.Trim();
            string strNetAmnt = (Convert.ToString(txtNetAmnt.Text)).Replace(",", "");
            Double DNetAmnt = string.IsNullOrEmpty(strNetAmnt) ? 0 : Convert.ToDouble(strNetAmnt);
            DataTable dtDetail = (DataTable)ViewState["dt"];

            #endregion

            #region Insert/Update with Transaction
            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (grdMain.Rows.Count > 0 && dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    TyreIssueDAL objMat = new TyreIssueDAL();
                    string Matfrom = "BK";
                    Int64 MaxIssueNo = 0;
                    Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                    MaxIssueNo = objMat.MaxNo(Matfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());
                    if (Convert.ToString(hidGRHeadIdno.Value) == "")
                    {
                        if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                        {
                            var lst = objMat.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt64(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlLocation.SelectedValue)), Convert.ToInt64(ddlDateRange.SelectedValue));
                            if (lst.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirmAtSave()", true);
                                return;
                            }
                            //if ((txtGRNo.Text != Convert.ToString(MaxGRNo)) && (GrIdnos != Convert.ToInt32(txtGRNo.Text)))
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirm()", true);
                            //}
                        }
                        else
                        {
                            this.ShowMessageErr("Issue No. can't be left blank.");
                            txtGRNo.Text = Convert.ToString(MaxIssueNo);
                            txtGRNo.Focus(); txtGRNo.SelectText();
                            return;
                        }
                    }
                    TyreIssueDAL obj = new TyreIssueDAL();
                    if (Convert.ToString(hidGRHeadIdno.Value) != "")
                    {
                        intMaterialIdno = obj.MatUpdate(Convert.ToInt64(hidGRHeadIdno.Value), dtMatDate, IAgainst, intMatNo, intLoc_Id, TruckNoIdno, DNetAmnt, YearIdno, dtDetail, intIssueIDno, strRemarkhead, intDriver_Id);
                    }
                    else
                    {
                        intMaterialIdno = obj.InsertMat(dtMatDate, IAgainst, intMatNo, intLoc_Id, TruckNoIdno, DNetAmnt, YearIdno, dtDetail, intIssueIDno, strRemarkhead, intDriver_Id);
                    }
                    obj = null;
                    if (intMaterialIdno > 0)
                    {
                        if (Convert.ToString(hidGRHeadIdno.Value) != null && Convert.ToString(hidGRHeadIdno.Value) != "")
                        {
                            this.ShowMessage("Record updated successfully.");
                        }
                        else
                        {
                            this.ShowMessage("Record saved successfully.");
                        }
                        //ddlLocation_SelectedIndexChanged(null, null);
                        //ddlFromCity_SelectedIndexChanged(null, null);
                        this.ClearAll();
                        ViewState["dt"] = dtTemp = null;
                        this.BindGridT();
                        ddlDateRange.Focus();
                        tScope.Complete();
                        //kapil
                       // Response.Redirect("MaterialIssue.aspx");
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(Convert.ToString(hidGRHeadIdno.Value)) == false)
                        {
                            hidpostingmsg.Value = "Record(s) not updated.";
                        }
                        else
                        {
                            hidpostingmsg.Value = "Record(s) not saved.";
                        }
                        tScope.Dispose();

                        //tScope.Dispose();
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                        //return;
                    }

                }

                else if (intMaterialIdno < 0)
                {
                    //this.ShowMessageErr("Entry already made with this GR. No.");
                    if (txtGRNo.Text != "")
                    {
                        //  checkGRNoAtSave = true;
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
            }

            Int64 iMaxGRNo = 0;
            TyreIssueDAL objGRDAL = new TyreIssueDAL();
            iMaxGRNo = objGRDAL.MaxNo("BK", Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 :
                                            Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());

            txtGRNo.Text = Convert.ToString(iMaxGRNo);

            //ddlGRType.Focus();
            #endregion


        }
        protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)
        {
            ClearItems();
            ClearAll();
            Response.Redirect("MaterialIssue.aspx");
        }
        protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            if (hidGRHeadIdno.Value != null && hidGRHeadIdno.Value != "")
            {
                Populate(Convert.ToInt32(hidGRHeadIdno.Value));
                ClearItems();
            }
            else
            {
                this.ClearAll(); ddlLocation.SelectedValue = Convert.ToString(base.UserFromCity);
                // ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                string Matfrom = "BK";
                this.BindMaxNo(Matfrom, Convert.ToInt32((ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), intYearIdno);
            }
            ddlLocation.Focus();
        }
        #endregion

        #region misclenious Function.....
        private void BindCity()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.SelectCityCombo();
            obj = null;

            if (lst.Count > 0)
            {
                ddlLocation.DataSource = lst;
                ddlLocation.DataTextField = "City_Name";
                ddlLocation.DataValueField = "City_Idno";
                ddlLocation.DataBind();

            }
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindDriver(Int32 var)
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            if (var == 0)
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Acnt_Name";
                    ddlDriver.DataValueField = "Acnt_Idno";
                    ddlDriver.DataBind();

                }
                ddlDriver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            else
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Driver_name";
                    ddlDriver.DataValueField = "Driver_Idno";
                    ddlDriver.DataBind();

                }
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlLocation.DataSource = FrmCity;
            ddlLocation.DataTextField = "CityName";
            ddlLocation.DataValueField = "cityidno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindMaxNo(string Matfrom, Int32 LocationId, Int32 YearId)
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            Int64 MaxNo = obj.MaxNo(Matfrom, YearId, LocationId, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxNo);
        }
        public void Populate(Int32 intMatIdNo)
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            TyreIssHead objMatHead = obj.SelectMatHead(intMatIdNo);
            var objMatDetl = obj.SelectMatDetail(intMatIdNo);
            hidGRHeadIdno.Value = Convert.ToString(intMatIdNo);
            if (objMatHead != null)
            {
                ddlDateRange.SelectedValue = Convert.ToString(objMatHead.Year_Idno);
                txtGRDate.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.MatIss_Date)) ? "" : Convert.ToDateTime(objMatHead.MatIss_Date).ToString("dd-MM-yyyy");
                iGrAgainst = Convert.ToInt32(objMatHead.MatIss_Typ);

                // RDbDirect.Enabled = false; RDbRecpt.Enabled = false;
                //new 
                txtRemarkhead.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.ReMark)) ? "" : Convert.ToString(objMatHead.ReMark);

                txtGRNo.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.MatIss_No)) ? "" : Convert.ToString(objMatHead.MatIss_No);
                ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Truck_Idno)) ? "0" : Convert.ToString(objMatHead.Truck_Idno);
                ddlTruckNo_SelectedIndexChanged(null, null);
                ddlDriver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Driver_Idno)) ? "0" : Convert.ToString(objMatHead.Driver_Idno);
                ddlReciver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Driver_Idno)) ? "0" : Convert.ToString(objMatHead.Driver_Idno);
                ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Loc_Idno)) ? "0" : Convert.ToString(objMatHead.Loc_Idno);




                dtTemp = CreateDt();
                for (int counter = 0; counter < objMatDetl.Count; counter++)
                {
                    string strItemName = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Name"));
                    string strItemNameId = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Iteam_Idno"));
                    string strQty = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Qty"));
                    string strWeight = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Weght"));
                    string strRate = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Rate"));
                    string strAmount = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Amnt"));
                    string strDetail = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Remark"));
                    string strStckdetlNO = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "SerialNo"));
                    string strStckdetl_Idno = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "SerlDetl_id"));
                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemName, strItemNameId, strQty, strWeight, strRate, strAmount, strDetail, strStckdetlNO, strStckdetl_Idno);
                }
                ViewState["dt"] = dtTemp;
                BindGridT();
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.Net_Amnt)) ? "0" : String.Format("{0:0,0.00}", objMatHead.Net_Amnt);
                PrintMaterial(objMatHead, objMatDetl);
            }
            obj = null;
        }
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            TyreIssueDAL objTruck = new TyreIssueDAL();
            var TruckNolst = obj.BindTruckNo();
            var ToCity = obj.BindLocFrom();
            var itemname = objTruck.BindItemName();
            obj = null;
            ddlTruckNo.DataSource = objTruck.BindTruckNo();
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindSerialNo()
        {
            TyreIssueDAL objSerial = new TyreIssueDAL();
            var SerialNo = objSerial.BindSerialNo(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlLocation.SelectedValue));
            ddlSerialNo.DataSource = SerialNo;
            ddlSerialNo.DataTextField = "Serial_No";
            ddlSerialNo.DataValueField = "Serial_Idno";
            ddlSerialNo.DataBind();
            ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


        }
        private void BindItemDropdown()
        {
            TyreIssueDAL objTruck = new TyreIssueDAL();
            var itemname = objTruck.BindItemName();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void BindActiveItemDropdown()
        {
            TyreIssueDAL objTruck = new TyreIssueDAL();
            var itemname = objTruck.BindActiveItemName();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        private void BindEmployee()
        {
            TyreIssueDAL objm = new TyreIssueDAL();
            var EMPS = objm.SelectEmployee();
            ddlReciver.DataSource = EMPS;
            ddlReciver.DataTextField = "Acnt_Name";
            ddlReciver.DataValueField = "Acnt_Idno";
            ddlReciver.DataBind();
            ddlReciver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlDriver.DataSource = EMPS;
            ddlDriver.DataTextField = "Acnt_Name";
            ddlDriver.DataValueField = "Acnt_Idno";
            ddlDriver.DataBind();
            ddlDriver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            objm = null;
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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
        private void ClearItems()
        {
            hidrowid.Value = ""; lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0;
            ddlSerialNo.SelectedIndex = 0;
            //txtQuantity.Text = "1";
            txtweight.Text = "0.00";
            //txtremark.Text = "";
            txtrate.Text = "0.00";
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Item_Name", "String",
                "Item_Idno", "String",
                "Quantity", "String",
                "Weight", "String",
                "Rate", "String",
                "Amount", "String",
                "Detail", "String",
                "StckDetl_No", "String",
                "StckDetl_Idno", "String"
                );
            return dttemp;
        }
        private void CalculateEdit()
        {
            // GRPrepDAL objGrprepDAL = new GRPrepDAL();
            double iRate = 0; double EditRate = 0;
            DateTime strMatDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtMAtDate = strMatDate;
            if (ddlItemName.SelectedIndex > 0)
            {

                if (hidTBBType.Value == "False")
                {
                    txtrate.Text = "0.00";
                }
                else
                {
                    iRate = Convert.ToDouble(txtrate.Text);
                    //if (txtQuantity.Text.Trim() != "")
                    dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(1));

                }
                //AgentRate();
                //RcptAmtTot(dtTemp);
                //netamntcal();
            }

        }
        private void ClearAll()
        {

            // ddlDateRange.SelectedIndex = 0;
            //ddlDateRange_SelectedIndexChanged(null, null);
            hidrowid.Value = string.Empty;
            ddlTruckNo.SelectedIndex = 0;
            ddlItemName.SelectedIndex = 0;
            txtNetAmnt.Text = "0.00";
            ViewState["dt"] = dtTemp = null;
            grdMain.DataSource = dtTemp;
            grdMain.DataBind();
            txtRemarkhead.Text = string.Empty;
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void EnableDisableAtLoad()
        {
            lblmsg.Visible = false;
            lblmsg2.Visible = false;

            ddlLocation.Enabled = true;
            ddlDateRange.Enabled = true;
        }
        private void ClearAtddlocationChanged()
        {
            //hidGRHeadIdno.Value = string.Empty;
            hidrowid.Value = ""; lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0;
            //txtQuantity.Text = "1";
            txtweight.Text = "0.00"; txtrate.Text = "0.00";

        }
        private void PrintMaterial(TyreIssHead MatHead, object Matdetl)
        {
            Repeater obj = new Repeater();

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
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname.Text = CompName;
            lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompTIN.Text = TinNo.ToString();
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


            if (MatHead != null)
            {
                lblTransno.Text = Convert.ToString(MatHead.MatIss_No);
                lblTranDate.Text = Convert.ToString(MatHead.MatIss_Date);
                lblLoation.Text = Convert.ToString(ddlLocation.SelectedItem.Text);
                lblType.Text = Convert.ToString(MatHead.MatIss_Typ == 1 ? "Issue" : "Re-Issue");
                lblTruckNo.Text = Convert.ToString(ddlTruckNo.SelectedItem.Text);
                lblIssueTo.Text = Convert.ToString(ddlReciver.SelectedItem.Text);
                lblPrintHeadng.Text = "Material Issue - " + Convert.ToString((MatHead.MatIss_Typ) == 1 ? "Issue" : (MatHead.MatIss_Typ == 2) ? "Re-Issue" : "To issue");
            }

            //if (Matdetl != null)
            //{

            //    Repeater1.DataSource = Matdetl;
            //    Repeater1.DataBind();
            //}
        }
        #endregion

        #region Main GRid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    ddlSerialNo.SelectedValue = Convert.ToString(drs[0]["StckDetl_Idno"]);
                    // txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])));
                    //txtremark.Text = Convert.ToString(drs[0]["Detail"]);
                    //txtAmount.Text = String.Format("{0:0,0.00}", Convert.ToDouble(drs[0]["Amount"]));
                    //ddlRateType_SelectedIndexChanged(null, null);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
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

                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Name"], rw["Item_Idno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"], rw["Detail"], rw["StckDetl_No"], rw["StckDetl_Idno"]);

                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
            }


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

                    txtNetAmnt.Text = lblAmount.Text.ToString();

                }

            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
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

        #region Other Event.....

        protected void txtGRDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtGRDate.Focus(); txtGRDate.SelectText();
            }
            catch (Exception ex)
            {

            }

        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAtddlocationChanged();
                //ClearAtFromCityChanged();
                TyreIssueDAL objGR = new TyreIssueDAL();
                string Matfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(Matfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());
                ddlLocation.Focus();
                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate Issue No.!");
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        txtGRNo.Focus(); txtGRNo.SelectText();
                        return;
                    }
                    else
                    {
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        txtGRNo.Focus(); txtGRNo.SelectText();
                        return;
                    }
                    //if ((txtGRNo.Text != Convert.ToString(MaxGRNo)) && (GrIdnos != Convert.ToInt32(txtGRNo.Text)))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "ShowConfirm()", true);
                    //}
                }
                else
                {
                    this.ShowMessageErr("Issue No. can't be left blank.");
                    txtGRNo.Text = Convert.ToString(MaxGRNo);
                    txtGRNo.Focus(); txtGRNo.SelectText();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }

        #endregion

        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddlTruckNo.SelectedIndex > 0))
                {
                    TyreIssueDAL obj = new TyreIssueDAL();
                    //obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    //var lst = obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    //ViewState["isCalculateTDS"] = Convert.ToString(lst.Lorry_Type);
                    //Int32 Typ = 0;
                    //Typ = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    //ddlDriver.DataSource = null;
                    //if (ddlDriver.Items.Count > 0)
                    //{
                    //    ddlDriver.Items.Clear();
                    //}
                    //BindDriver(Typ);
                    //if (lst != null)
                    //{
                    //    ddlDriver.SelectedValue = Convert.ToString(lst.Driver_Idno);
                    //}
                    //else
                    //{
                    //    ddlDriver.SelectedValue = "0";
                    //}
                    var prvRec = obj.PrvReciver(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    if (prvRec != null)
                    {
                        ddlReciver.SelectedValue = Convert.ToString(prvRec);
                    }
                    else
                    {
                        ddlReciver.SelectedValue = "0";
                    }
                    PrvIssuedGrd();
                }

            }
            catch (Exception Ex)
            {

            }
        }
        private void PrvIssuedGrd()
        {
            TyreIssueDAL obj = new TyreIssueDAL();
            var objItem = obj.PrvItem(Convert.ToInt64(ddlTruckNo.SelectedValue));
            if (objItem != null && objItem.Count > 0)
            {
                grdPrvIssue.DataSource = objItem;
                grdPrvIssue.DataBind();
            }
            else
            {
                DataTable DsNew = new DataTable();
                DsNew.Columns.Add("Date", typeof(System.String));
                DsNew.Columns.Add("Item", typeof(System.String));
                DataRow DR = DsNew.NewRow();
                DsNew.Rows.Add(DR);
                DsNew.AcceptChanges();

                grdPrvIssue.DataSource = DsNew;
                grdPrvIssue.DataBind();
            }
        }
        protected void grdPrvIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPrvIssue.PageIndex = e.NewPageIndex;
            PrvIssuedGrd();
        }

        protected void ddlSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSerialNo.SelectedIndex > 0)
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] Dr = dtTemp.Select("StckDetl_Idno=" + Convert.ToString(ddlSerialNo.SelectedValue));
                if (Dr.Length > 0)
                {
                    ShowMessageErr("Serial No Alreay selected!.");
                    ddlSerialNo.SelectedIndex = 0;
                    ddlSerialNo.Focus();
                }
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt64(ddlLocation.SelectedValue) > 0)
            {
                BindSerialNo();
            }
            else
            {
                ShowMessageErr("Select location First!");
            }
        }
    }
}

