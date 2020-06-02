using System;
using System.Data;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Web.Services;
using System.Collections.Generic;
using System.Collections;

namespace WebTransport
{
    public partial class MaterialIssue : Pagebase
    {
        #region Variable .....
        static FinYearA UFinYear = new FinYearA();
        // string con = ApplicationFunction.ConnectionString();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();

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
                    // lblViewList.Visible = false;
                }
                this.BindRcptSerialNo();
                this.BindDropdown();
                this.BindDateRange();
                this.BindTyreCategory();
                this.BindEmployee();
                this.BindTyresize();
                this.BindTyrePosition();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlPartyName.Enabled = false;
                txtOwner.Enabled = false;
                DivReport.Visible = false;
                txtGRDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                BindPartyName(0);
                txtPrevAlignDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange.Focus();
                HidiFromCity.Value = Convert.ToString(base.UserFromCity);
                ddlLocation.SelectedValue = Convert.ToString(HidiFromCity.Value);
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                // EnableDisableAtLoad();
                //to open grid by default at page load, thats y this funtn calling here 
                PrvIssuedGrd();

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                this.FillData();
                if (Request.QueryString["MatIssue"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["MatIssue"]));
                    imgPDF.Visible = false;
                    lnkbtnNew.Visible = true;
                    lnkPrint.Visible = false;
                    ddlDateRange.Enabled = false;
                    this.BindItemDropdown();
                }
                else
                {
                    imgPDF.Visible = false;
                    lnkbtnNew.Visible = false;
                    lnkPrint.Visible = false;
                    ddlDateRange.Enabled = true;
                    this.BindActiveItemDropdown();
                }
                SetDate();

                lnkbtnReport.Visible = false;
            }
        }
        #endregion

        #region Button Event .....

        protected void lnkTruckRefresh_Click(object sender, EventArgs e)
        {
            MaterialDAL obj = new MaterialDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            lnkTruckRefresh.Focus();
        }
        protected void lnkBtnLocation_Click(object sender, EventArgs e)
        {
            MaterialDAL obj = new MaterialDAL();
            var to = obj.BindToCity();
            ddlLocation.DataSource = to;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            lnkBtnLocation.Focus();
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
                { txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); txtAlignDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); }
                else { txtGRDate.Text = hidmindate.Value; txtAlignDate.Text = hidmindate.Value; txtAlignDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); }
            }
        }

        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            this.ClearItems();
        }

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            //if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0) { ShowMessageErr("Weight should be greater than zero."); txtweight.Focus(); return; } 
            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero."); txtQuantity.Focus(); return; }

            MaterialDAL objDAl = new MaterialDAL();
            int itemType = objDAl.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
            if(itemType == 1)
            {
                if (ddltyresize.SelectedIndex == 0) { ShowMessageErr("Please select Tyre size."); ddltyresize.Focus(); return; }
                if (ddltyreposition.SelectedIndex == 0) { ShowMessageErr("Please select Tyre position."); ddltyreposition.Focus(); return; }
            }
            //if (itemType != 1)
            //{
            //    rfvSerialNo.Enabled = false ;
            //}
            //else
            //{
            //    if (ddlSerialNo.SelectedIndex <= 0)
            //    {
            //        ShowMessageErr("Please select serial No.");
            //        txtrate.Focus();
            //        return;
            //    }
            //}
            if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0)
            {
                ShowMessageErr("Rate should be greater than zero.");
                txtrate.Focus();
                return;
            }
            MaterialDAL obj = new MaterialDAL();
            DataTable dt = obj.SelectCurrentStockSummary(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64(ddlLocation.SelectedValue), Convert.ToInt64(ddlItemName.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["CurStock"].ToString()) < Convert.ToInt32(txtQuantity.Text.Trim()))
                {
                    ddlItemName.SelectedIndex = 0;
                    string strMsg = "Item not in stock, Please select another item!";
                    ShowMessageErr(strMsg);
                    return;
                }
            }
            dtTemp = (DataTable)ViewState["dt"];

            CalculateEdit();
            string strAmount = "";

            dtTemp = (DataTable)ViewState["dt"];


            if (hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Item_Name"] = ddlItemName.SelectedItem.Text;
                        dtrow["Item_Idno"] = ddlItemName.SelectedValue;
                        if (itemType == 1)
                        {
                            dtrow["Tyresize"] = ddltyresize.SelectedItem.Text;
                            dtrow["Tyresize_Idno"] = ddltyresize.SelectedValue;
                        }
                        else
                        {
                            dtrow["Tyresize"] = "";
                            dtrow["Tyresize_Idno"] = 0;
                        }
                        if (itemType == 1)
                        {
                            dtrow["Tyreposition"] = ddltyreposition.SelectedItem.Text;
                            dtrow["Tyreposition_Idno"] = ddltyreposition.SelectedValue;
                        }
                        else
                        {
                            dtrow["Tyreposition"] = "";
                            dtrow["Tyreposition_Idno"] = 0;
                        }
                        dtrow["Quantity"] = txtQuantity.Text.Trim(); iqty += Convert.ToDouble(txtQuantity.Text.Trim());
                        dtrow["Weight"] = txtweight.Text.Trim();
                        dtrow["Rate"] = txtrate.Text.Trim();
                        dtrow["Amount"] = dtotalAmount.ToString("N2");
                        dtrow["SerialNo"] = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) == true ? "" : ddlSerialNo.SelectedItem.Text;
                        dtrow["SerialId"] = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) == true ? "0" : ddlSerialNo.SelectedValue;
                        dtrow["NSD"] = string.IsNullOrEmpty(txtNSD.Text) == true ? "0" : txtNSD.Text;
                        dtrow["PSI"] = string.IsNullOrEmpty(txtPSI.Text) == true ? "0" : txtPSI.Text;
                        dtrow["TType"] = ddlTType.SelectedValue;
                        string RserialNo = "";
                        if (ddlRSerialNo.SelectedValue != "0") { RserialNo = string.IsNullOrEmpty(ddlRSerialNo.SelectedValue) == true ? "" : ddlRSerialNo.SelectedItem.Text; }
                        dtrow["RSerialNo"] = RserialNo;
                        dtrow["RSerialId"] = string.IsNullOrEmpty(ddlRSerialNo.SelectedValue) == true ? "0" : ddlRSerialNo.SelectedItem.Value;
                        dtrow["RNSD"] = string.IsNullOrEmpty(txtRNSD.Text) == true ? "0" : txtRNSD.Text;
                        dtrow["RPSI"] = string.IsNullOrEmpty(txtRPSI.Text) == true ? "0" : txtRPSI.Text;
                        dtrow["RTType"] = ddlRType.SelectedValue;

                        dtrow["Align"] = Convert.ToString(ddlAlign.SelectedValue) == "1" ? "True" : "False";

                        dtrow["AlignDate"] = string.IsNullOrEmpty(txtAlignDate.Text) == true ? "" : txtAlignDate.Text;
                        dtrow["PrevAlignDate"] = string.IsNullOrEmpty(txtPrevAlignDate.Text) == true ? "" : txtPrevAlignDate.Text;
                        dtrow["RPrice"] = string.IsNullOrEmpty(txtPrice.Text) == true ? "0" : txtPrice.Text;
                        //kapil
                        //dtrow["Detail"] = string.Empty;
                        //dtrow["Detail"] = txtremark.Text.Trim();

                    }
                }

            }
            else
            {

                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (string.IsNullOrEmpty(ddlSerialNo.SelectedValue) != true)
                    {
                        if (Convert.ToString(dtrow["SerialNo"]) == ddlSerialNo.SelectedItem.Text || Convert.ToString(dtrow["RSerialNo"]) == ddlRSerialNo.SelectedItem.Text)
                        {
                            this.ShowMessageErr("Serial No Already Exist in List!");
                            ddlSerialNo.Focus();
                            return;
                        }
                    }
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemNameId = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                string strTyresize = ddltyresize.SelectedItem.Text.Trim();
                string strTyreSizeId = string.IsNullOrEmpty(ddltyresize.SelectedValue) ? "0" : (ddltyresize.SelectedValue);
                string strTyreposition = ddltyreposition.SelectedItem.Text.Trim();
                string strTyrepositionId = string.IsNullOrEmpty(ddltyreposition.SelectedValue) ? "0" : (ddltyreposition.SelectedValue);
                string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                string SerialNo = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) == true ? "" : ddlSerialNo.SelectedItem.Text;
                string SerialId = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) == true ? "0" : ddlSerialNo.SelectedValue;
                string NSD = string.IsNullOrEmpty(txtNSD.Text) == true ? "0" : txtNSD.Text;
                string PSI = string.IsNullOrEmpty(txtPSI.Text) == true ? "0" : txtPSI.Text;
                string RserialNo, RSerialIdno, Align, AlignDate, RPrice, RTType, PrevAlignDate, TTypeid = "";
                if (itemType == 1)
                {
                    if (ddlRSerialNo.SelectedValue == "0") { RserialNo = ""; } else { RserialNo = string.IsNullOrEmpty(ddlRSerialNo.SelectedValue) == true ? "" : ddlRSerialNo.SelectedItem.Text; }
                    RSerialIdno = string.IsNullOrEmpty(ddlRSerialNo.SelectedValue) == true ? "0" : ddlRSerialNo.SelectedItem.Value;
                    Align = Convert.ToString(ddlAlign.SelectedValue) == "1" ? "True" : "False";
                    AlignDate = string.IsNullOrEmpty(txtAlignDate.Text) == true ? "" : txtAlignDate.Text;
                    PrevAlignDate = string.IsNullOrEmpty(txtPrevAlignDate.Text) == true ? "" : txtPrevAlignDate.Text;
                    RPrice = string.IsNullOrEmpty(txtPrice.Text) == true ? "0" : txtPrice.Text;
                    RTType = string.IsNullOrEmpty(ddlRType.SelectedValue) == true ? "0" : ddlRType.SelectedValue;
                    TTypeid = string.IsNullOrEmpty(ddlTType.SelectedValue) == true ? "0" : ddlTType.SelectedValue;
                }
                else
                {
                    RserialNo = "";
                    RSerialIdno = "0";
                    Align = ""; PrevAlignDate = "0";
                    AlignDate = "0"; RPrice = "0"; RTType = "0"; TTypeid = "0";
                }


                string RNSD = string.IsNullOrEmpty(txtRNSD.Text) == true ? "0" : txtRNSD.Text;
                string RPSI = string.IsNullOrEmpty(txtRPSI.Text) == true ? "0" : txtRPSI.Text;



                strAmount = dtotalAmount.ToString("N2");
                //kapil
                //string strDetail = string.IsNullOrEmpty(txtremark.Text.Trim()) ? "" : (txtremark.Text.Trim());\
                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strQty, strWeight, strRate, strAmount, string.Empty, SerialNo, SerialId, NSD, PSI, TTypeid, RserialNo, RSerialIdno, RNSD, RPSI, RTType, Align, ApplicationFunction.mmddyyyy(PrevAlignDate), ApplicationFunction.mmddyyyy(AlignDate), RPrice, strTyresize, strTyreSizeId, strTyreposition, strTyrepositionId);
                ViewState["dt"] = dtTemp;
            }

            this.BindGridT();
            ddlItemName.Focus();
            ClearItems();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MaterialIssue.aspx");
        }

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            #region fill values to Variables
            MaterialDAL objGrprepDAL = new MaterialDAL();
            dtTemp = (DataTable)ViewState["dt"];
            #endregion

            #region Validation Messages

            //if (ddlReciver.SelectedIndex == 0) { this.ShowMessageErr("Please select Receiver's Name."); ddlReciver.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Receiver's Name."; return; }
            if (ddlLocation.SelectedIndex == 0) { this.ShowMessageErr("Please Select Delivery Place!"); ddlLocation.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please Select Delivery Place."; return; }
            if ((dtTemp != null) && (dtTemp.Rows.Count == 0)) { this.ShowMessageErr("Please Enter Item Details !"); ddlItemName.Focus(); return; }

            #endregion

            #region Declare Input Variables
            string strMsg = string.Empty;
            Int64 intMaterialIdno = 0;
            DateTime strMatDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            DateTime dtMatDate = strMatDate;
            Int32 IAgainst = Convert.ToInt32(1);
            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int64 intMatNo = string.IsNullOrEmpty(txtGRNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGRNo.Text.Trim());
            Int32 TruckNoIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);
            string km = string.IsNullOrEmpty(txtfitmentkm.Text.Trim()) ? "" : txtfitmentkm.Text.Trim();
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
                    MaterialDAL objMat = new MaterialDAL();
                    string Matfrom = "BK";
                    Int64 MaxIssueNo = 0;
                    Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                    MaxIssueNo = objMat.MaxNo(Matfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());
                    Int64 Party = Convert.ToInt64(Convert.ToString(ddlPartyName.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue));
                    Int64 IssueType = 0;
                    if (rdoSaleIssue.Checked) IssueType = 2; else IssueType = 1;

                    string OwnerName = string.IsNullOrEmpty(Convert.ToString(txtOwner.Text)) ? "0" : Convert.ToString(txtOwner.Text);
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
                        }
                        else
                        {
                            this.ShowMessageErr("Issue No. can't be left blank.");
                            txtGRNo.Text = Convert.ToString(MaxIssueNo);
                            txtGRNo.Focus(); txtGRNo.SelectText();
                            return;
                        }
                    }
                    MaterialDAL obj = new MaterialDAL();
                    if (Convert.ToString(hidGRHeadIdno.Value) != "")
                    {
                        intMaterialIdno = obj.MatUpdate(Convert.ToInt64(hidGRHeadIdno.Value), dtMatDate, IAgainst, intMatNo, intLoc_Id, TruckNoIdno, km, DNetAmnt, YearIdno, dtDetail, intIssueIDno, strRemarkhead, intDriver_Id, Party, OwnerName, IssueType);
                    }
                    else
                    {
                        intMaterialIdno = obj.InsertMat(dtMatDate, IAgainst, intMatNo, intLoc_Id, TruckNoIdno, km, DNetAmnt, YearIdno, dtDetail, intIssueIDno, strRemarkhead, intDriver_Id, Party, OwnerName, IssueType);
                    }
                    obj = null;
                    if (intMaterialIdno > 0)
                    {
                        this.ClearAll();
                        ViewState["dt"] = dtTemp = null;
                        this.BindGridT();
                        ddlDateRange.Focus();
                        tScope.Complete();

                        if (Convert.ToString(hidGRHeadIdno.Value) != null && Convert.ToString(hidGRHeadIdno.Value) != "")
                        {
                            lnkbtnNew.Visible = false;
                            this.ShowMessage("Record updated successfully.");
                        }
                        else
                        {
                            this.ShowMessage("Record saved successfully.");
                        }
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
                    }
                }

                else if (intMaterialIdno < 0)
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
            }

            Int64 iMaxGRNo = 0;
            MaterialDAL objGRDAL = new MaterialDAL();
            iMaxGRNo = objGRDAL.MaxNo("BK", Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 :
                                            Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());

            txtGRNo.Text = Convert.ToString(iMaxGRNo);
            dtTemp = CreateDt();
            ViewState["dt"] = dtTemp;
            #endregion
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (hidGRHeadIdno.Value != null && hidGRHeadIdno.Value != "")
            {
                ClearItems();
                Populate(Convert.ToInt32(hidGRHeadIdno.Value));
            }
            else
            {
                ClearAll(); ClearItems();
            }
            ddlDateRange.Focus();
        }

        #endregion

        #region misclenious Function.....
        private void BindCity()
        {
            MaterialDAL obj = new MaterialDAL();
            var lst = obj.BindToCity();
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
        private void BindTyresize()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var tyresize = obj.Bindtyresize();
            obj = null;
            ddltyresize.DataSource = tyresize;
            ddltyresize.DataTextField = "TyreSize";
            ddltyresize.DataValueField = "TyreSize_Idno";
            ddltyresize.DataBind();
            ddltyresize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTyrePosition()
        {
            MaterialDAL obj = new MaterialDAL();
            var tyreposition = obj.Bindtyreposition();
            obj = null;
            ddltyreposition.DataSource = tyreposition;
            ddltyreposition.DataTextField = "Position_name";
            ddltyreposition.DataValueField = "Position_id";
            ddltyreposition.DataBind();
            ddltyreposition.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindPartyName(Int32 LorryType)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            if (Convert.ToInt32(LorryType) == 0)
            {
                var LorrMast = obj.SelectPartyNameOwn();
                ddlPartyName.DataSource = LorrMast;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();
            }
            if (Convert.ToInt32(LorryType) == 1)
            {
                var LorrMast = obj.SelectPartyName();
                obj = null;
                ddlPartyName.DataSource = LorrMast;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();

            }
            ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
            ddlPartyName.Focus();
        }
        private void BindDriver(Int32 var)
        {
            MaterialDAL obj = new MaterialDAL();
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
            MaterialDAL obj = new MaterialDAL();
            Int64 MaxNo = obj.MaxNo(Matfrom, YearId, LocationId, ApplicationFunction.ConnectionString());
            txtGRNo.Text = Convert.ToString(MaxNo);
        }
        public void Populate(Int32 intMatIdNo)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "Check();", true);
            MaterialDAL obj = new MaterialDAL();
            MatIssHead objMatHead = obj.SelectMatHead(intMatIdNo);
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
                if (objMatHead.Issue_Type == 2)
                {
                    rdoSaleIssue.Checked = true;
                    rdoMIssue.Checked = false;
                    this.FillData();
                    ddlPartyName.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Prty_Idno)) ? "0" : Convert.ToString(objMatHead.Prty_Idno);
                }
                else
                {
                    rdoSaleIssue.Checked = false;
                    rdoMIssue.Checked = true;
                    ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Truck_Idno)) ? "0" : Convert.ToString(objMatHead.Truck_Idno);
                    ddlTruckNo_SelectedIndexChanged(null, null);
                }
                rdoSaleIssue.Enabled = rdoMIssue.Enabled = false;
                txtGRNo.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.MatIss_No)) ? "" : Convert.ToString(objMatHead.MatIss_No);
                txtfitmentkm.Text = string.IsNullOrEmpty(Convert.ToString(objMatHead.Fitment_km)) ? "" : Convert.ToString(objMatHead.Fitment_km);
                ddlDriver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Driver_Idno)) ? "0" : Convert.ToString(objMatHead.Driver_Idno);
                ddlReciver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Driver_Idno)) ? "0" : Convert.ToString(objMatHead.Driver_Idno);
                ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objMatHead.Loc_Idno)) ? "0" : Convert.ToString(objMatHead.Loc_Idno);
                ddlLocation.Enabled = false;
                lnkBtnLocation.Visible = false;
                var result = obj.GetMatrialIssueNo(Convert.ToInt64(intMatIdNo));
                if (result != null) { SpaMatrialIssue.Visible = true; lblMatrialIssueVal.Text = result.ToString(); } else { SpaMatrialIssue.Visible = false; }
                var Result = obj.CheckForDelete(Convert.ToInt64(intMatIdNo));
                if (Result != null) { DivSaveBtn.Visible = false; } else { DivSaveBtn.Visible = true; }

                ddlTruckNo.Enabled = false;
                dtTemp = CreateDt();
                for (int counter = 0; counter < objMatDetl.Count; counter++)
                {
                    string strItemName = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Name"));
                    string strItemNameId = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Iteam_Idno"));
                    string strTyreSize_Idno = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "TyresizeIdno"));
                    string strTyreSize = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "TyreSize"));
                    string strTyreposition_Idno = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Tyreposition_Idno"));
                    string strTyreposition = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Position_name"));
                    string strQty = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Qty"));
                    string strWeight = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Weght"));
                    string strRate = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Rate"));
                    string strAmount = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Item_Amnt"));
                    string strDetail = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Remark"));
                    string strSerialNo = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "SerialNo"));
                    string strSerialId = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "SerialId"));
                    string NSD = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "NSD"));
                    string PSI = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "PSI"));
                    string TTypeid = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "TType"));
                    string RserialNo = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RSerial_No"));
                    string RSerialIdno = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RSerial_Idno"));
                    string RNSD = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RNSD"));
                    string RPSI = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RPSI"));
                    string RTType = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RTType"));
                    string Align = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "Align"));
                    string AlignDate = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "AlignDate"));
                    string PrevAlignDate = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "PrevAlignDate"));
                    string RPrice = Convert.ToString(DataBinder.Eval(objMatDetl[counter], "RPrice"));

                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemName, strItemNameId, strQty, strWeight, strRate, strAmount, strDetail, strSerialNo, strSerialId, NSD, PSI, TTypeid, RserialNo, RSerialIdno, RNSD, RPSI, RTType, Align, PrevAlignDate, AlignDate, RPrice, strTyreSize, strTyreSize_Idno, strTyreposition,strTyreposition_Idno);
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
            MaterialDAL objTruck = new MaterialDAL();
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
        private void BindItemDropdown()
        {
            MaterialDAL objTruck = new MaterialDAL();
            var itemname = objTruck.BindItemName();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindRcptSerialNo()
        {
            MaterialDAL objDAL = new MaterialDAL();
            var Lst = objDAL.RTyreSerial();
            if (Lst != null && Lst.Count > 0)
            {
                ddlRSerialNo.DataSource = Lst;
                ddlRSerialNo.DataTextField = "SerialNo";
                ddlRSerialNo.DataValueField = "StckId";
                ddlRSerialNo.DataBind();
            }
            ddlRSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindActiveItemDropdown()
        {
            MaterialDAL objTruck = new MaterialDAL();
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
        private void BindTyreCategory()
        {
            BindDropdownDAL objBindDropdown = new BindDropdownDAL();
            var TyreCategory = objBindDropdown.BindTyreType();


            objBindDropdown = null;
            ddlTType.DataSource = TyreCategory;
            ddlTType.DataTextField = "TyreType";
            ddlTType.DataValueField = "TyreTypeIdno";
            ddlTType.DataBind();
            ddlTType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", "0"));

            ddlRType.DataSource = TyreCategory;
            ddlRType.DataTextField = "TyreType";
            ddlRType.DataValueField = "TyreTypeIdno";
            ddlRType.DataBind();
            ddlRType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select-", "0"));

        }
        private void BindEmployee()
        {
            MaterialDAL objm = new MaterialDAL();
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
                    ddlItemName.Enabled = true;
                }
            }
            else
            {
                ddlItemName.Enabled = true;
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }
        private void ClearItems()
        {
            hidrowid.Value = ""; lblmessage.Text = ""; ddlItemName.SelectedIndex = 0;
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
            txtNSD.Text = "";
            txtPSI.Text = "";
            ddlItemName.Enabled = true;
            ddlTType.SelectedIndex = 0;
            ddlRSerialNo.SelectedIndex = 0;
            txtRNSD.Text = "";
            txtRPSI.Text = "";
            ddlRType.SelectedIndex = 0;ddltyresize.SelectedIndex = 0;ddltyreposition.SelectedIndex = 0;
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
                "SerialNo", "String",
                "SerialId", "String",
                "NSD", "String",
                "PSI", "String",
                "TType", "String",
                "RSerialNo", "String",
                "RSerialId", "String",
                "RNSD", "String",
                "RPSI", "String",
                "RTTYpe", "String",
                "Align", "String",
                "PrevAlignDate", "String",
                "AlignDate", "String",
                "RPrice", "String",
                "Tyresize", "String",
                "Tyresize_Idno", "String",
                "Tyreposition", "String",
                "Tyreposition_Idno", "String"
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
                    if (txtQuantity.Text.Trim() != "")
                        dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtQuantity.Text));

                }
            }
        }
        private void ClearAll()
        {
            hidrowid.Value = string.Empty; hidGRHeadIdno.Value = string.Empty;
            ddlTruckNo.SelectedIndex = ddlLocation.SelectedIndex = ddlDriver.SelectedIndex = ddlReciver.SelectedIndex = 0;
            ddlItemName.SelectedIndex = 0;
            ddltyreposition.SelectedIndex = 0;
            ddltyresize.SelectedIndex = 0;
            txtNetAmnt.Text = "0.00";
            ViewState["dt"] = dtTemp = null;
            grdMain.DataSource = dtTemp;
            grdMain.DataBind();
            txtRemarkhead.Text = string.Empty;
            lnkbtnNew.Visible = false;
            txtAlignDate.Text = "";
            txtPrice.Text = "";
            txtPrevAlignDate.Text = "";
            PrvIssuedGrd();
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void EnableDisableAtLoad()
        {
            ddlLocation.Enabled = true;
            ddlDateRange.Enabled = true;
        }
        private void ClearAtddlocationChanged()
        {
            hidrowid.Value = ""; lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0;
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
        }

        private void PrintMaterial(MatIssHead MatHead, object Matdetl)
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

        private void FillSerialNumberInUpdateCase(Int64 SerialID, Int64 ItemIdno, Int64 tyresizeidno)
        {
            // in update case fill serial no new (which are not used as in case of new matissue) + Old(only used serialno)
            MaterialDAL objDAl = new MaterialDAL();
            Int64 LocIdno = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            if (LocIdno > 0)
            {
                var Lst = objDAl.TyreSerialInCaseUpdate(SerialID, ItemIdno, tyresizeidno);
                objDAl = null;
                if (Lst != null && Lst.Count > 0)
                {
                    ddlSerialNo.DataSource = Lst;
                    ddlSerialNo.DataTextField = "SerialNo";
                    ddlSerialNo.DataValueField = "StckId";
                    ddlSerialNo.DataBind();
                    ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            else
            {
                var Lst = objDAl.TyreSerialInCaseUpdateFromLoc(SerialID, ItemIdno, LocIdno, tyresizeidno);
                objDAl = null;
                if (Lst != null && Lst.Count > 0)
                {
                    ddlSerialNo.DataSource = Lst;
                    ddlSerialNo.DataTextField = "SerialNo";
                    ddlSerialNo.DataValueField = "StckId";
                    ddlSerialNo.DataBind();
                    ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
        }
        private void FillSerialNumber(Int32 ItemValue, Int64 tyresizeidno)
        {
            // in update case fill serial no new (which are not used as in case of new matissue) + Old(only used serialno)
            MaterialDAL objDAl = new MaterialDAL();

            Int64 LocIdnumber = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            if (LocIdnumber > 0)
            {
                var Lst = objDAl.TyreSerial(ItemValue, tyresizeidno);
                if (Lst != null && Lst.Count > 0)
                {
                    ddlSerialNo.DataSource = Lst;
                    ddlSerialNo.DataTextField = "SerialNo";
                    ddlSerialNo.DataValueField = "StckId";
                    ddlSerialNo.DataBind();
                    ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            else
            {
                var Lst = objDAl.TyreSerialFromLoc(ItemValue, LocIdnumber, tyresizeidno);
                if (Lst != null && Lst.Count > 0)
                {
                    ddlSerialNo.DataSource = Lst;
                    ddlSerialNo.DataTextField = "SerialNo";
                    ddlSerialNo.DataValueField = "StckId";
                    ddlSerialNo.DataBind();
                    ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
            }
            objDAl = null;

        }

        #endregion

        #region Main GRid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            MaterialDAL objDAl = new MaterialDAL();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    ddltyresize.SelectedValue = string.IsNullOrEmpty(Convert.ToString(drs[0]["Tyresize_Idno"])) ? "0" : Convert.ToString(drs[0]["Tyresize_Idno"]);
                    ddltyreposition.SelectedValue = string.IsNullOrEmpty(Convert.ToString(drs[0]["Tyreposition_Idno"])) ? "0" : Convert.ToString(drs[0]["Tyreposition_Idno"]);
                    int itemType = objDAl.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
                    if (itemType != 1)
                    {
                        txtNSD.Enabled = false; txtPSI.Enabled = false; ddlRSerialNo.Enabled = false;
                        txtRNSD.Enabled = false; txtRPSI.Enabled = false; ddlRType.Enabled = false; txtPrice.Enabled = false;
                        txtPrevAlignDate.Text = ""; txtAlignDate.Text = ""; txtAlignDate.Enabled = false;
                        txtNSD.Text = ""; txtPSI.Text = ""; ddlRSerialNo.SelectedIndex = 0; txtRNSD.Text = ""; txtRPSI.Text = ""; txtPrice.Text = ""; ddlAlign.Enabled = false;
                        ddltyresize.Enabled = false; ddltyreposition.Enabled = false;
                    }
                    else
                    {
                        txtNSD.Enabled = true; txtPSI.Enabled = true; ddlRSerialNo.Enabled = true; txtAlignDate.Enabled = true; ddlAlign.Enabled = true;
                        txtRNSD.Enabled = true; txtRPSI.Enabled = true; ddlRType.Enabled = true; txtPrice.Enabled = true; ddltyresize.Enabled = true;ddltyreposition.Enabled = true;
                    }
                    ddlItemName.Enabled = false;
                    FillSerialNumberInUpdateCase(Convert.ToInt64(drs[0]["SerialId"]), Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddltyresize.SelectedValue));

                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    txtrate.Text = Convert.ToString(drs[0]["Rate"]) == "" ? "0" : drs[0]["Rate"].ToString();

                    if (Convert.ToString(drs[0]["SerialId"]) != "") { ddlSerialNo.SelectedValue = Convert.ToString(drs[0]["SerialId"]); } else { ddlSerialNo.SelectedIndex = 0; }
                    txtNSD.Text = Convert.ToString(drs[0]["NSD"]);
                    txtPSI.Text = Convert.ToString(drs[0]["PSI"]);


                    //ddlRSerialNo.SelectedValue = Convert.ToString(drs[0]["RSerialId"]);
                    txtRNSD.Text = Convert.ToString(drs[0]["RNSD"]);
                    txtRPSI.Text = Convert.ToString(drs[0]["RPSI"]);
                    BindTyreCategory();
                    ddlTType.SelectedValue = Convert.ToString(drs[0]["TType"]);
                    ddlRType.SelectedValue = Convert.ToString(drs[0]["RTType"]);
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
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Name"], rw["Item_Idno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"], rw["Detail"], rw["SerialNo"], rw["SerialId"], rw["NSD"], rw["PSI"], rw["TType"], rw["RSerialNo"], rw["RSerialId"], rw["RNSD"], rw["RPSI"], rw["RTType"], rw["Align"], rw["AlignDate"], rw["RPrice"], rw["Tyresize"], rw["Tyresize_Idno"], rw["Tyreposition"], rw["Tyreposition_Idno"]);
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
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "Check();", true);
                ClearAtddlocationChanged();
                MaterialDAL objGR = new MaterialDAL();
                string Matfrom = "BK";
                Int64 MaxGRNo = 0; Int64 GrIdnos = Convert.ToInt64(Convert.ToString(hidGRHeadIdno.Value) == "" ? 0 : Convert.ToInt64(hidGRHeadIdno.Value));
                MaxGRNo = objGR.MaxNo(Matfrom, Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), ApplicationFunction.ConnectionString());

                if ((txtGRNo.Text.Trim() != "") && (Convert.ToInt64(txtGRNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtGRNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlLocation.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate Issue No.!");
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        ddlLocation.Focus();

                        return;
                    }
                    else
                    {
                        txtGRNo.Text = Convert.ToString(MaxGRNo);
                        ddlLocation.Focus();
                        return;
                    }
                }
                else
                {
                    this.ShowMessageErr("Issue No. can't be left blank.");
                    txtGRNo.Text = Convert.ToString(MaxGRNo);
                    ddlLocation.Focus();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }

        #endregion

        #region ControlEvent & Functions..
        public void GridFill()
        {
            MaterialDAL obj = new MaterialDAL();
            var lstGridData = obj.SelectForSearch(Convert.ToInt32(ddlItemName.SelectedValue));

            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                lblItemeRep.Text = Convert.ToString(DataBinder.Eval(lstGridData[0], "ItemName"));
                grdMatHis.DataSource = lstGridData;
                grdMatHis.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;

                //divpaging.Visible = true;
            }
            else
            {
                grdMatHis.DataSource = null;
                grdMatHis.DataBind();

                //divpaging.Visible = false;
            }
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "Check();", true);
            try
            {
                if ((ddlTruckNo.SelectedIndex > 0))
                {
                    MaterialDAL obj = new MaterialDAL();
                    BindDropdownDAL obj1 = new BindDropdownDAL();
                    var lorryType = obj.LorryType(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    var Party = obj.PartyName(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    if (lorryType != null) { BindPartyName(lorryType); if (Party != null) { ddlPartyName.SelectedValue = Convert.ToString(Party); ddlPartyName.Enabled = false; } else { ddlPartyName.SelectedValue = "0"; ddlPartyName.Enabled = true; } }
                    var prvRec = obj.PrvReciver(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    if (prvRec != null) { ddlReciver.SelectedValue = Convert.ToString(prvRec); } else { ddlReciver.SelectedValue = "0"; }
                    var Driver = obj.DriverName(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    if (Driver != null) { ddlDriver.SelectedValue = Convert.ToString(Driver); } else { ddlDriver.SelectedValue = "0"; }
                    var OwnerName = obj.OwnerName(Convert.ToInt64(ddlTruckNo.SelectedValue));
                    if (OwnerName != null) { txtOwner.Text = Convert.ToString(OwnerName); txtOwner.Enabled = false; } else { txtOwner.Text = ""; txtOwner.Enabled = true; }
                    PrvIssuedGrd();
                }
                else
                {
                    DivReport.Visible = false;
                    ddlPartyName.SelectedIndex = 0;
                    txtOwner.Text = "";
                    // lblRep.Visible = false;
                }
                ddlTruckNo.Focus();
                if (ddlItemName.SelectedIndex > 0)
                {
                    DivReport.Visible = true;
                    // lblRep.Visible = true;
                }

            }
            catch (Exception Ex)
            {

            }
        }

        private void PrvIssuedGrd()
        {
            MaterialDAL obj = new MaterialDAL();
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

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {

                MaterialDAL objDAl = new MaterialDAL();
                tblItemMastPur Itm = objDAl.GetItemDetails(Convert.ToInt64(ddlItemName.SelectedValue));

                if (Itm != null)
                {
                    txtrate.Text = string.IsNullOrEmpty(Convert.ToString(Itm.Pur_Rate)) == true ? "0" : Convert.ToString(Itm.Pur_Rate);
                }
                else
                {
                    txtrate.Text = "";
                }
                GridFill();
                int itemType = objDAl.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
                if (itemType != 1)
                {
                    txtNSD.Enabled = false; txtPSI.Enabled = false; ddlRSerialNo.Enabled = false;
                    txtRNSD.Enabled = false; txtRPSI.Enabled = false; ddlRType.Enabled = false; txtPrice.Enabled = false;
                    txtPrevAlignDate.Text = ""; txtAlignDate.Text = ""; txtAlignDate.Enabled = false;
                    txtNSD.Text = "0.00"; txtPSI.Text = "0.00"; ddlRSerialNo.SelectedIndex = 0; txtRNSD.Text = "0.00"; txtRPSI.Text = "0.00"; txtPrice.Text = "0.00"; ddlAlign.Enabled = false;
                    ddlTType.Enabled = false;
                    SpanIsType.Visible = SpanTyreAlign.Visible = SpanPrevAlignDate.Visible = SpanAlignDate.Visible = false;
                    rfvSerialNo.Enabled = RequiredFieldValidator4.Enabled = false;
                    SpanRcptSrNo.Visible  = false;
                    ddltyresize.Enabled = false;
                    ddltyreposition.Enabled = false;
                }
                else
                {
                    txtAlignDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtNSD.Enabled = true; txtPSI.Enabled = true; ddlRSerialNo.Enabled = true; txtAlignDate.Enabled = true; ddlAlign.Enabled = true;
                    txtRNSD.Enabled = true; txtRPSI.Enabled = true; ddlRType.Enabled = true; txtPrice.Enabled = true;
                    ddlTType.Enabled = true;
                    SpanIsType.Visible = SpanTyreAlign.Visible = SpanPrevAlignDate.Visible = SpanAlignDate.Visible = true;
                    rfvSerialNo.Enabled = RequiredFieldValidator4.Enabled = true;
                    if (rdoSaleIssue.Checked) { SpanRcptSrNo.Visible  = false; } else { SpanRcptSrNo.Visible  = true; }
                }

                if (itemType == 1)
                {
                    ddltyresize.Enabled = true;
                    ddltyreposition.Enabled = true;
                    rfvSerialNo.Enabled = true;
                    ddlSerialNo.Enabled = true;
                    txtQuantity.Enabled = false; txtQuantity.Text = "1";
                    if (Request.QueryString["MatIssue"] != null)
                    {
                        FillSerialNumberInUpdateCase(Convert.ToInt64(Request.QueryString["MatIssue"]), Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddltyresize.SelectedValue));
                    }
                    else
                    {
                        this.FillSerialNumber(string.IsNullOrEmpty(Convert.ToString(ddlItemName.SelectedValue)) == true ? 0 : Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt64(ddltyresize.SelectedValue));
                    }
                }
                else if (itemType == 2)
                {

                    MaterialDAL obj = new MaterialDAL();
                    AccessoryStockRpt obj1 = new AccessoryStockRpt();
                    Int64 YearIdno = Convert.ToInt64(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? "0" : ddlDateRange.SelectedValue);
                    Int64 ItemIdno = Convert.ToInt64(ddlItemName.SelectedValue);
                    Int64 LocIdno = Convert.ToInt64(string.IsNullOrEmpty(ddlLocation.SelectedValue) ? "0" : ddlLocation.SelectedValue);

                    //DataTable dt1 = obj1.SelectAccStockReport(ApplicationFunction.ConnectionString(), hidmindate.Value, DateTime.Now.Date.ToString("dd-MM-yyyy"), YearIdno, Convert.ToString(ddlItemName.SelectedValue), itemType, Convert.ToInt64(ddlLocation.SelectedValue));
                    DataTable dtnew = obj1.FetchAccStockReport(ApplicationFunction.ConnectionString());
                    obj1 = null;
                    if (dtnew != null && dtnew.Rows.Count > 0)
                    {
                        if ((string.IsNullOrEmpty(Convert.ToString(dtnew.Rows[0]["BalQty"])) ? 0 : Convert.ToDouble(dtnew.Rows[0]["BalQty"])) == 0)
                        {
                            ddlItemName.SelectedIndex = 0;
                            string strMsg = "Item not in stock, Please select another item!";
                            ShowMessageErr(strMsg);
                            return;
                        }
                        else if ((string.IsNullOrEmpty(Convert.ToString(dtnew.Rows[0]["BalQty"])) ? 0 : Convert.ToDouble(dtnew.Rows[0]["BalQty"])) < (Convert.ToDouble(txtQuantity.Text.Trim())))
                        {
                            ddlItemName.SelectedIndex = 0;
                            string strMsg = "Item Qty should be less than balance stock.";
                            ShowMessageErr(strMsg);
                            return;
                        }
                        else
                        {
                            ddlSerialNo.DataSource = null;
                            ddlSerialNo.DataBind();
                            ddlSerialNo.Items.Clear();
                            txtQuantity.Enabled = true;
                            rfvSerialNo.Enabled = false;
                            ddlSerialNo.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlItemName.SelectedIndex = 0;
                        string strMsg = "Item not in stock, Please select another item!";
                        ShowMessageErr(strMsg);
                        return;
                    }
                    //DataTable dt = obj.SelectCurrentStockSummary(ApplicationFunction.ConnectionString(), YearIdno, LocIdno, ItemIdno);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    if (Convert.ToInt32(dt.Rows[0]["CurStock"].ToString()) <= 0)
                    //    {
                    //        ddlItemName.SelectedIndex = 0;
                    //        string strMsg = "Item not in stock, Please select another item!";
                    //        ShowMessageErr(strMsg);
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        ViewState["ItemStock"] = dt.Rows[0]["CurStock"].ToString();
                    //        ddlSerialNo.DataSource = null;
                    //        ddlSerialNo.DataBind();
                    //        ddlSerialNo.Items.Clear();
                    //        txtQuantity.Enabled = true;
                    //        rfvSerialNo.Enabled = false;
                    //        ddlSerialNo.Enabled = false;
                    //    }
                    //}
                    //else
                    //{
                    //    ddlItemName.SelectedIndex = 0;
                    //    string strMsg = "Item not in stock, Please select another item!";
                    //    ShowMessageErr(strMsg);
                    //    return;
                    //}
                }
                else
                {
                    ddlSerialNo.DataSource = null;
                    ddlSerialNo.DataBind();
                    ddlSerialNo.Items.Clear();
                    txtQuantity.Enabled = true;
                    rfvSerialNo.Enabled = false;
                    ddlSerialNo.Enabled = false;
                }

                if (ddlTruckNo.SelectedIndex > 0)
                {
                    lnkbtnReport.Visible = true;
                    //lblRep.Visible = true;
                }
            }
            else
            {
                lnkbtnReport.Visible = false;
                //lblRep.Visible = false;
            }

            ddlItemName.Focus();
            if (ddlSerialNo.Items.Count > 0)
            {
                if (Convert.ToInt32(ddlSerialNo.SelectedValue) > 0)
                {
                    MaterialDAL objDAl = new MaterialDAL();
                    string PrevAlignDate = objDAl.ItemAlignDate(Convert.ToInt32(ddlItemName.SelectedValue), ddlSerialNo.SelectedItem.Text);
                    txtPrevAlignDate.Text = Convert.ToString(string.IsNullOrEmpty(PrevAlignDate) ? "" : Convert.ToDateTime(PrevAlignDate).ToString("dd-MM-yyyy"));
                }
            }

        }
        protected void lnkbtnReport_Click(object sender, EventArgs e)
        {

        }

        protected void grdMatHis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMatHis.PageIndex = e.NewPageIndex;
            this.GridFill();
        }

        protected void ddlSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaterialDAL objDAl = new MaterialDAL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "Check();", true);
            if (ddlItemName.Items.Count > 0)
            {
                if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
                {
                    string PrevAlignDate = objDAl.ItemAlignDate(Convert.ToInt32(ddlItemName.SelectedValue), ddlSerialNo.SelectedItem.Text);
                    txtPrevAlignDate.Text = Convert.ToString(string.IsNullOrEmpty(PrevAlignDate) ? "" : Convert.ToDateTime(PrevAlignDate).ToString("dd-MM-yyyy"));
                }
            }
            ddlSerialNo.Focus();
        }

        public void FillData()
        {
            if (rdoMIssue.Checked)
            {
                ddlPartyName.SelectedIndex = 0; ddlTruckNo.SelectedIndex = 0; txtOwner.Text = ""; ddlDriver.SelectedIndex = 0;  ddlPartyName.Enabled = false; ddlDriver.Enabled = true; txtOwner.Enabled = false; ddlTruckNo.Enabled = true; lnkTruckRefresh.Visible = true; SpanRcptSrNo.Visible = true;
                DivPartyName.Visible = false; rfvPartyName.Enabled = false;
            }
            else if (rdoSaleIssue.Checked)
            {
                SpanRcptSrNo.Visible = false;
                BindDropdownDAL obj = new BindDropdownDAL();
                var lst = obj.SelectPartyFill();
                if (lst != null && lst.Count > 0)
                {
                    ddlPartyName.DataSource = lst;
                    ddlPartyName.DataTextField = "Acnt_Name";
                    ddlPartyName.DataValueField = "Acnt_Idno";
                    ddlPartyName.DataBind();
                }
                ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
                ddlPartyName.Focus();
                ddlPartyName.SelectedValue = "0"; ddlTruckNo.SelectedIndex = 0; txtOwner.Text = ""; ddlDriver.SelectedIndex = 0; ddlPartyName.Enabled = true; ddlDriver.Enabled = false; txtOwner.Enabled = false; ddlTruckNo.Enabled = false; SpanRcptSrNo.Visible = false; lnkTruckRefresh.Visible = false;
                DivPartyName.Visible = true; rfvPartyName.Enabled = true;
            }
        }

        protected void rdoMIssue_CheckedChanged(object sender, EventArgs e)
        {
            this.FillData();
        }

        protected void rdoSaleIssue_CheckedChanged(object sender, EventArgs e)
        {
            this.FillData();
        }

        protected void ddlAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlign.SelectedValue == "2") { txtAlignDate.Text = ""; txtAlignDate.Enabled = false; } else { txtAlignDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); txtAlignDate.Enabled = true; }
        }
        #endregion

        //[System.Web.Services.WebMethod]
        //public static ArrayList BindDatatoDropdown()
        //{
        //    ArrayList list = new ArrayList();
        //    BindDropdownDAL obj = new BindDropdownDAL();
        //    var lst = obj.SelectPartyFill();
        //    if (lst != null && lst.Count > 0)
        //    {
        //        for (int i = 0; i < lst.Count; i++)
        //        {
        //            list.Add(new ListItem(Convert.ToString(DataBinder.Eval(lst[i], "Acnt_Name")), Convert.ToString(DataBinder.Eval(lst[i], "Acnt_Idno"))));
        //        }
        //    }
        //    return list;
        //}

        //private void PopulateDropDownList(ArrayList list, DropDownList ddl)
        //{
        //    ddl.DataSource = list;
        //    ddl.DataTextField = "Text";
        //    ddl.DataValueField = "Value";
        //    ddl.DataBind();
        //} 
    }
}