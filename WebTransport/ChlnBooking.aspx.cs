using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Services;

namespace WebTransport
{
    public partial class ChlnBooking : Pagebase
    {
        #region Private Variables...
        string Grno = "";
        DataTable DtTemp = new DataTable();
        DataTable DtTempFuel = new DataTable();//string con = "";
        double dblNetAmnt = 0; Int32 iscmbtype = 0; DataSet DsUserPref; string sqlSTR = ""; double dTotQty = 0; double dTotWeight = 0; double dGrAmnt = 0; double dGrCommiAmnt = 0; double dWithoutWagesAmnt = 0; double dWagesAmnt = 0;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0, dNetcommsn = 0; string StrPANNo = "", grdate = ""; double dfWithoutWagesAmnt = 0; double dfWagesAmnt = 0; double tolamnt = 0;
        private int intFormId = 28; DataTable AcntLinkDS; DataTable DsHire; Int64 ICAcnt_Idno = 0; Int64 IHireAcntIdno = 0; double dCommissionAmnt = 0;
        Int64 IntTDSAcntIdno = 0; Int64 IntDieselAcc_Idno = 0;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            // con = ApplicationFunction.ConnectionString();
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {

                txtOwnrNme.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtchallanNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtInstNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtAdvAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtcommission.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");


                //
                txtstartkm.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtCloseKm.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtLatecharge.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtHamaliCharge.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtDetention.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtRatePerKM.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtFreight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                //
                txtQTY.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtrate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtamount.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
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
                this.BindCity();
                BindRcpt();
                BindBank();
                this.Binditemlk();
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
                BindDieselAccountLink();
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
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0;
                ddlDelvryPlace.Enabled = true;
                lnkbtnSearch.Enabled = true;

                ddlCusBank.Enabled = false;
                txtInstDate.Text = ""; ddlFromCity.Enabled = true;
                txtInstDate.Enabled = txtInstNo.Enabled = false;
                ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
                tblUserPref obj = objChlnBookingDAL.selectUserPref();
                if (Convert.ToBoolean(obj.Chln_Excel) == true)
                {
                    lnkbtnExcel.Visible = true;
                }
                else
                {
                    lnkbtnExcel.Visible = false;
                }
                if (Convert.ToInt32(obj.GRRetRequired) == 1)
                {
                    imgbtnPopup.Visible = true;
                }
                else
                {
                    imgbtnPopup.Visible = false;
                }
                if (obj != null)
                {
                    hidWorkType.Value = Convert.ToString(obj.Work_Type);
                    hidTdsTaxPer.Value = Convert.ToString(obj.STaxPer_TDS);
                }
                obj = null;
                if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
                {
                    lblDelvPlace.Visible = false;
                    ddlDelvryPlace.Visible = false;
                }
                else
                {
                    lblDelvPlace.Visible = true;
                    ddlDelvryPlace.Visible = true;
                    ddlDelvryPlace.Enabled = true;
                }
                lnkbtnSave.Enabled = true;
                this.ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                ddldateRange_SelectedIndexChanged(null, null);
                if (Request.QueryString["q"] != null)
                {

                    this.BindPopulate();


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
                    lastprint.Visible = true;
                    lnkBtnLast.Visible = true;
                    divPosting.Visible = false;
                    userpref();
                    if (Convert.ToString(Session["Userclass"]) == "Admin")
                    {
                        txtTdsAmnt.Enabled = true;
                        LnkTDSUpdt.Enabled = true;
                        DataTable dtPosLeft = new DataTable();
                        DataSet objDataSets = objChlnBookingDAL.AccPosting(ApplicationFunction.ConnectionString(), "ChallanPOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
                        if (objDataSets != null && objDataSets.Tables.Count > 0 && objDataSets.Tables[1].Rows.Count > 0)
                        {
                            dtPosLeft = objDataSets.Tables[1];
                            lblPostingLeft.Text = Convert.ToString(dtPosLeft.Rows[0][0]);
                        }
                    }
                    else
                    {
                        tblUserPref obj1 = objChlnBookingDAL.selectUserPref();
                        if (Convert.ToInt32(obj1.TDSEdit) == 1)
                        {
                            txtTdsAmnt.Enabled = true;
                            LnkTDSUpdt.Enabled = true;
                        }
                        else
                        {
                            txtTdsAmnt.Enabled = false;
                            LnkTDSUpdt.Enabled = false;
                        }
                    }
                }
                else
                {
                    this.Bind();
                    this.PostingLeft();
                    lnkbtnNew.Visible = false;
                    divPosting.Visible = true;
                    lastprint.Visible = false;
                    lnkbtnprintOM.Visible = false;
                }
                if (Request.QueryString["GrHeadIdno"] != null)
                {
                    PopupGrDirectly(Convert.ToInt64(Request.QueryString["GrHeadIdno"]));
                }
                CheckLastPendingPrint();
            }
        }

        private void CheckLastPendingPrint()
        {
            bool instantPrint = true;
            PrintLastSavedChln.Visible = false;
            Int64 LastChlnIdno = Convert.ToInt64(Session["LastChlnIdno"] == null ? "0" : Session["LastChlnIdno"]);
            Session["LastChlnIdno"] = null;
            if (instantPrint == true && LastChlnIdno > 0)
            {
                hidLastChlnId.Value = LastChlnIdno.ToString();
                if (Session["DBName"] != null && Session["DBName"].ToString().ToLower() == "tromcargo")
                {
                    PrintLastSavedChln.Visible = true;
                }
                //PrintLastSavedChln.Visible = true;
            }
        }

        public void PrintLastSaved_Click(object sender, EventArgs e)
        {
            bool instantPrint = true;
            Int64 LastChlnIdno = Convert.ToInt64(hidLastChlnId.Value);
            if (instantPrint == true && LastChlnIdno > 0)
            {
                Session["InstantPrint"] = true;
                hidPages.Value = ddlPagesLastSaved.SelectedValue;
                if (Convert.ToBoolean(Session["InstantPrint"] == null ? false : Session["InstantPrint"]))
                {
                    hidPages.Value = ddlPagesLastSaved.SelectedValue;
                    PrintChallanOM(Convert.ToInt64(LastChlnIdno), Convert.ToString(HidGrType.Value));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrintOM('printOm')", true);
                }
                Session["InstantPrint"] = null;
                Session["LastChlnIdno"] = null;
                PrintLastSavedChln.Visible = false;
            }
        }

        #endregion

        #region Button Evnets...
        bool omprintshow = false;
        private void userpref()
        {
            ChlnBookingDAL objGrprepDAL = new ChlnBookingDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            omprintshow = Convert.ToBoolean(userpref.GRRetRequired);
            if (omprintshow == true)
            {
                lnkbtnprintOM.Visible = true;
                lastprint.Visible = false;
                divPosting.Visible = false;
                lnkbtnExcel.Visible = false;
                lnkbtnPrtyPmnt.Visible = false;
            }
            else
            {
                lnkbtnprintOM.Visible = false;
                lastprint.Visible = true;
                divPosting.Visible = true;
                lnkbtnExcel.Visible = true;
                lnkbtnPrtyPmnt.Visible = true;
            }
        }
        protected void lnkbtnDriverRefresh_OnClick(object sender, EventArgs e)
        {

            if ((ddlTruckNo.SelectedIndex > 0))
            {
                ChlnBookingDAL obj = new ChlnBookingDAL();
                Int32 Typ = 0;
                Typ = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                ddldriverName.DataSource = null;
                if (ddldriverName.Items.Count > 0)
                {
                    ddldriverName.Items.Clear();

                }
                BindDriver(Typ);
            }
            lnkbtnDriverRefresh.Focus();
        }
        protected void lnkbtnCloase_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);
            ddldelvplace.SelectedIndex = 0;
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind(); lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
            ddlTruckNo.Focus();
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string msg = "";
            DtTemp = (DataTable)ViewState["dt"];
            DtTempFuel = (DataTable)ViewState["dtfuel"];
            //Commentedf as Fuel is not Mandatory
            //if (DtTempFuel != null)
            //{
            //    if (DtTempFuel.Rows.Count <= 0)
            //    {
            //        ShowMessageErr("Please enter fuel details");
            //        return;
            //    }
            //}
            //if (grdmainFuel.Rows.Count <= 0)
            //{
            //    ShowMessageErr("Please enter fuel details");
            //    return;
            //}
            if (DtTemp != null)
            {
                if (DtTemp.Rows.Count <= 0)
                {
                    ShowMessageErr("Please enter details");
                    return;
                }
            }
            if (grdMain.Rows.Count <= 0)
            {
                ShowMessageErr("Please enter details");
                return;
            }
            if (Convert.ToDouble(txtAdvAmnt.Text == "" ? "0" : txtAdvAmnt.Text) > 0)
            {
                if (Convert.ToInt32(Request.Form[ddlRcptType.UniqueID]) == 0)
                {
                    ShowMessageErr("Please Select Pay. Type!.");
                    ddlRcptType.Focus();
                    return;
                }
            }
            //Check if truck is un-assigned
            if (IsUnAssigned(ddlTruckNo.SelectedValue))
            {
                ShowMessageErr("Please select truck number.");
                ddlTruckNo.Focus();
                return;
            }

            if ((txtHireAmnt.Text == "" || Convert.ToDouble(txtHireAmnt.Text) == 0) && txtHireAmnt.Visible == true)
            {

                ShowMessageErr("Hire Amount cannot be 0!.");
                ddlRcptType.Focus();
                return;

            }
            Int64 RateIdno = 0; bool isinsert = false;
            ChlnBookingDAL obj = new ChlnBookingDAL();
            tblChlnBookHead objtblChlnBookHead = new tblChlnBookHead();
            objtblChlnBookHead.Chln_No = txtchallanNo.Text;
            if (txtDate.Text != String.Empty || txtDate.Text != "01-01-0001" || txtDate.Text != "01/01/0001")
            {
                objtblChlnBookHead.Chln_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            }
            objtblChlnBookHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);
            objtblChlnBookHead.DelvryPlc_Idno = Convert.ToInt32((ddlDelvryPlace.SelectedIndex <= 0) ? "0" : ddlDelvryPlace.SelectedValue);
            if (ddlgrtyp.SelectedValue == "2")
            {
                objtblChlnBookHead.Truck_Idno = Convert.ToInt32((ddlTruckNo.SelectedIndex < 0) ? "0" : ddlTruckNo.SelectedValue);
            }
            else
            {
                objtblChlnBookHead.Truck_Idno = Convert.ToInt32((ddlTruckNo.SelectedIndex <= 0) ? "0" : ddlTruckNo.SelectedValue);
            }
            objtblChlnBookHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);

            objtblChlnBookHead.Driver_Idno = Convert.ToInt32((ddldriverName.SelectedIndex <= 0) ? "0" : ddldriverName.SelectedValue);
            objtblChlnBookHead.Delvry_Instrc = txtDelvInstruction.Text.Trim().Replace("'", "");
            //ADD COLUMN FOR OM CARGO
            objtblChlnBookHead.Start_Km = (txtstartkm.Text == "") ? 0.00 : Convert.ToDouble(txtstartkm.Text);
            objtblChlnBookHead.Close_Km = (txtCloseKm.Text == "") ? 0.00 : Convert.ToDouble(txtCloseKm.Text);
            objtblChlnBookHead.Late_Charge = (txtLatecharge.Text == "") ? 0.00 : Convert.ToDouble(txtLatecharge.Text);
            objtblChlnBookHead.Hamali = (txtHamaliCharge.Text == "") ? 0.00 : Convert.ToDouble(txtHamaliCharge.Text);
            objtblChlnBookHead.Dentation = (txtDetention.Text == "") ? 0.00 : Convert.ToDouble(txtDetention.Text);
            objtblChlnBookHead.RateKM = (txtRatePerKM.Text == "") ? 0.00 : Convert.ToDouble(txtRatePerKM.Text);
            if (txtDlyDate.Text != String.Empty)
            {
                objtblChlnBookHead.DelvDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDlyDate.Text));
            }
            objtblChlnBookHead.Freight = (txtFreight.Text == "") ? 0.00 : Convert.ToDouble(txtFreight.Text);
            //

            objtblChlnBookHead.Inv_Idno = 0;
            objtblChlnBookHead.Gross_Amnt = ((ViewState["LorryOwnerType"] != null && Convert.ToString(ViewState["LorryOwnerType"]) == "H") ? (txtHireAmnt.Text != "" ? Convert.ToDouble(txtHireAmnt.Text) : 0) : Convert.ToDouble(txtGrosstotal.Text));
            objtblChlnBookHead.Commsn_Amnt = Convert.ToDouble(Request.Form[txtcommission.UniqueID]);
            objtblChlnBookHead.TDSTax_Amnt = Convert.ToDouble(txtTdsAmnt.Text);
            objtblChlnBookHead.Chln_type = 1;
            objtblChlnBookHead.Other_Amnt = 0;
            objtblChlnBookHead.Net_Amnt = Convert.ToDouble(Request.Form[txtNetAmnt.UniqueID]);
            objtblChlnBookHead.Work_type = Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value));
            objtblChlnBookHead.Adv_Amnt = Convert.ToDouble(txtAdvAmnt.Text);
            objtblChlnBookHead.RcptType_Idno = Convert.ToInt32((ddlRcptType.SelectedIndex < 0) ? "0" : Request.Form[ddlRcptType.UniqueID]);
            objtblChlnBookHead.Bank_Idno = Convert.ToInt32((ddlCusBank.SelectedIndex < 0) ? "0" : Request.Form[ddlCusBank.UniqueID]);
            objtblChlnBookHead.Inst_No = Convert.ToInt32(((txtInstNo.Text == "") ? "0" : Request.Form[txtInstNo.UniqueID]));
            objtblChlnBookHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);
            objtblChlnBookHead.Diesel_Amnt = Convert.ToDouble(txtDieselAmnt.Text.Trim().Replace(",", ""));
            objtblChlnBookHead.Tran_Type = Convert.ToInt32((ddltrantype.SelectedIndex <= 0) ? "0" : ddltrantype.SelectedValue);
            objtblChlnBookHead.DieselAcnt_IDno = Convert.ToInt64(ddlacntname.SelectedValue);
            objtblChlnBookHead.ManualNo = txtMno.Text.Trim().Replace("'", "");
            objtblChlnBookHead.User_AddedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
            objtblChlnBookHead.RassaTirpal_Chrg = Convert.ToDouble(txtRassaTripal.Text.Trim().Replace(",", ""));
            objtblChlnBookHead.Hire_Amnt = txtHireAmnt.Text == "" ? 0 : Convert.ToDouble(txtHireAmnt.Text);

            if (ddlgrtyp.SelectedValue == "2")
            {
                objtblChlnBookHead.Gr_Type = "GRR";
            }
            else
            {
                objtblChlnBookHead.Gr_Type = "GR";
            }

            if (Request.Form[txtInstDate.UniqueID] != null)
            {
                if (txtInstDate.Text != String.Empty)
                {
                    objtblChlnBookHead.Inst_Dt = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text.Trim()));
                }
            }
            objtblChlnBookHead.Date_Added = Convert.ToDateTime(ApplicationFunction.mmddyyyy(DateTime.Now.ToString("dd-MM-yyyy")));
            objtblChlnBookHead.STaxPer_TDS = Convert.ToDouble(hidTdsTaxPer.Value);
            Int64 value = 0;
            AcntLinkDS = obj.DtAcntDS(ApplicationFunction.ConnectionString());
            DsHire = obj.DsHireAcnt(ApplicationFunction.ConnectionString());

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
            {
                if (string.IsNullOrEmpty(hidid.Value) == true)
                {
                    value = obj.Insert(objtblChlnBookHead, DtTemp, Convert.ToInt32(ddlDelvryPlace.SelectedValue), ddlgrtyp.SelectedValue, DtTempFuel);
                    // obj = null;
                }
                else
                {
                    objtblChlnBookHead.User_ModifiedBy = Convert.ToInt64(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                    value = obj.Update(objtblChlnBookHead, Convert.ToInt32(hidid.Value), DtTemp, Convert.ToInt32(ddlDelvryPlace.SelectedValue), (HidGrType.Value), DtTempFuel);
                }

                if (value > 0)
                {
                    Session["LastChlnIdno"] = value;
                    if (this.PostIntoAccounts(Convert.ToDouble(txtAdvAmnt.Text), value, "CB", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue)) == true)
                    {
                        obj.UpdateIsPosting(value);
                        if (string.IsNullOrEmpty(hidid.Value) == false)
                        {
                            if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                            {
                                ShowMessage("Record Update successfully");
                                //Response.Redirect("ChlnBooking.aspx");
                                ddlFromCity_SelectedIndexChanged(null, null);
                                Clear();
                            }
                            else if (value == -1)
                            {
                                ShowMessageErr("Challan No Already Exist");
                            }
                            else if (value == -1)
                            {
                                ShowMessageErr("Record  Not Update");
                            }
                        }
                        else
                        {

                            if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                            {
                                ShowMessage("Record  saved Successfully ");
                                Clear();
                                clearFuel();
                                clearfuelgrid();

                                ddlFromCity_SelectedIndexChanged(null, null);
                                ///Response.Redirect("ChlnBooking.aspx");
                            }
                            else if (value == -1)
                            {
                                ShowMessageErr("Challan No Already Exist");
                            }
                            else
                            {
                                ShowMessageErr("Record Not  saved Successfully ");
                            }
                        }
                        tScope.Complete();
                        if (Request.QueryString["GrHeadIdno"] != null)
                        {
                            Response.Redirect("GRPrep.aspx?submit=Success_");
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
                    CheckLastPendingPrint();
                }
                else if (value == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('Challan already booked with challan no '" + txtchallanNo.Text + "')", true);
                }
            }

        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];

                string[] Array = Value.Split(new char[] { '-' });

                string ID = Array[0].ToString();

                string Type = Array[1].ToString();

                Populate(Convert.ToInt64(ID), Type);
            }
            else
            {
                Clear();
            }
        }
        protected void lnkbtnPrtyPmnt_OnClick(object sender, EventArgs e)
        {
            string url = "ChlnAmntPayment.aspx?ChlnIdno=" + Convert.ToInt64(HidGrId.Value) + "";
            string fullURL = "window.open('" + url + "', '_blank' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ChlnBooking.aspx");
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {


            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            if (ddlFromCity.SelectedIndex <= 0)
            {
                ShowMessageErr("Please Select From City");
                return;
            }
            txtTdsAmnt.Text = "0.00";
            ddlTruckNo.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
            if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
            {
                ddltrantype.SelectedValue = "0";
                lblDelvSerch.Text = "Truck No.";
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var lst = obj.BindTrantype();
                ddltrantype.DataSource = lst;
                ddltrantype.DataTextField = "Tran_Name";
                ddltrantype.DataValueField = "Tran_Idno";
                ddltrantype.DataBind();
                var TruckNolst = obj.BindTruckNo();

                if (ddltrantype.SelectedValue == "0")
                {
                    if (TruckNolst.Count > 0)
                    {
                        ddldelvplace.DataSource = TruckNolst;
                        ddldelvplace.DataTextField = "Lorry_No";
                        ddldelvplace.DataValueField = "Lorry_Idno";
                        ddldelvplace.DataBind();
                        ddldelvplace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                        lblDelvSerch.Text = "Truck No.";
                    }
                }
                else
                {
                    if (ddltrantype.SelectedValue == "1")
                    {
                        lblDelvSerch.Text = "Flight";
                    }
                    else if (ddltrantype.SelectedValue == "2")
                    {
                        lblDelvSerch.Text = "Train";
                    }
                    else
                    {
                        lblDelvSerch.Text = "Bus";
                    }

                }
            }
            else
            {
                lblDelvSerch.Text = "Delv. Place";
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var lst = obj.SelectCityCombo();
                obj = null;

                if (lst.Count > 0)
                {
                    ddldelvplace.DataSource = lst;
                    ddldelvplace.DataTextField = "City_Name";
                    ddldelvplace.DataValueField = "City_Idno";
                    ddldelvplace.DataBind();
                    ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }

            txtDateFrom.Focus();
        }

        protected void imgbtnPopup_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openFreightDetail();", true);
        }

        protected void ddltrantype_SelectedIndexChanged(object sender, EventArgs e)
        {

            var MiscList = (IList)null;
            ChlnBookingDAL obj = new ChlnBookingDAL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            var TruckNolst = obj.BindTruckNo();
            if (ddltrantype.SelectedValue == "0")
            {
                if (TruckNolst.Count > 0)
                {
                    ddldelvplace.DataSource = TruckNolst;
                    ddldelvplace.DataTextField = "Lorry_No";
                    ddldelvplace.DataValueField = "Lorry_Idno";
                    ddldelvplace.DataBind();
                    ddldelvplace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                    lblDelvSerch.Text = "Truck No.";
                }
                rfvDriver.Enabled = true;
            }

            if (Convert.ToInt32(ddltrantype.SelectedValue) != 0)
            {
                MiscList = obj.BindTransportaion(Convert.ToInt64(ddltrantype.SelectedValue));
                rfvDriver.Enabled = false;

            }
            if (ddltrantype.SelectedValue == "1")
            {
                lblDelvSerch.Text = "Flight";
                rfvDriver.Enabled = false;
            }
            if (ddltrantype.SelectedValue == "2")
            {
                lblDelvSerch.Text = "Train";
                rfvDriver.Enabled = false;
            }
            else
            {
                lblDelvSerch.Text = "Bus";
                rfvDriver.Enabled = false;
            }

            //if (MiscList.Count > 0)
            //{
            ddldelvplace.DataSource = MiscList;
            ddldelvplace.DataTextField = "Misc_Name";
            ddldelvplace.DataValueField = "Misc_Idno";
            ddldelvplace.DataBind();
            ddldelvplace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            //   }


        }


        protected void ddlgrtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            if (ddlgrtyp.SelectedValue == "2")
            {
                ddldelvplace.Visible = true;
                ddltrantype.Enabled = true;
                txttruck.Visible = false;
            }
            else
            {
                ddldelvplace.Visible = false;
                ddltrantype.Enabled = false;
                txttruck.Visible = true;
            }
        }

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
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
                        ChlnBookingDAL ObjChlnBookingDAL = new ChlnBookingDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable();
                        DataTable dtKMDetl = new DataTable(); DataRow Dr;

                        if (ddlgrtyp.SelectedValue != "2")
                        {

                            dtRcptDetl = ObjChlnBookingDAL.SelectGrChallanDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        }
                        else
                        {
                            dtRcptDetl = ObjChlnBookingDAL.SelectGrRetailerDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue), Convert.ToInt64(ddltrantype.SelectedValue));

                        }

                        txtKms.Text = string.IsNullOrEmpty(Convert.ToString(dtRcptDetl.Rows[0]["Kms"])) ? "0" : Convert.ToString(dtRcptDetl.Rows[0]["Kms"]);
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                        //netamntcal();

                        //salman
                        ddlDelvryPlace.Enabled = false;
                        if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
                        {

                            if (ddlgrtyp.SelectedValue == "2")
                            {
                                if (ddltrantype.SelectedValue == "0")
                                {
                                    ddlTruckNo.SelectedValue = ddldelvplace.SelectedValue;
                                    ddlTruckNo.Enabled = false;
                                    Lorrytype.Text = "Truck No.";
                                    ddlTruckNo_SelectedIndexChanged(null, null);
                                }
                                else
                                {

                                    ChlnBookingDAL obja = new ChlnBookingDAL();
                                    var MiscList = obja.BindTransportaion(Convert.ToInt64(ddltrantype.SelectedValue));
                                    ddlTruckNo.DataSource = MiscList;
                                    ddlTruckNo.DataTextField = "Misc_Name";
                                    ddlTruckNo.DataValueField = "Misc_Idno";
                                    ddlTruckNo.DataBind();

                                    if (Convert.ToInt32(ddltrantype.SelectedValue) > 0)
                                    {
                                        ddlTruckNo.SelectedValue = ddltrantype.SelectedValue;
                                        ddlTruckNo.Enabled = false;
                                        ddlTruckNo_SelectedIndexChanged(null, null);

                                        if (ddltrantype.SelectedValue == "1")
                                        {
                                            Lorrytype.Text = "Flight";
                                        }
                                        else if (ddltrantype.SelectedValue == "2")
                                        {
                                            Lorrytype.Text = "Train";
                                        }
                                        else
                                        {
                                            Lorrytype.Text = "Bus";
                                        }

                                    }
                                }
                            }
                            else
                            {
                                ddlTruckNo.SelectedValue = ddldelvplace.SelectedValue;
                                ddlTruckNo.Enabled = false;
                                Lorrytype.Text = "Truck No.";
                                ddlTruckNo_SelectedIndexChanged(null, null);
                            }
                        }
                        else
                        {
                            ddlDelvryPlace.SelectedValue = ddldelvplace.SelectedValue;
                            ddlTruckNo.Enabled = true;
                            ddlDelvryPlace.Enabled = false;
                        }
                        ChlnBookingDAL obj = new ChlnBookingDAL();
                        var lststate = obj.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));

                        DataSet ds1 = objbind1.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToString(txtDate.Text.Trim()));
                        ViewState["isCalculateTDS"] = ds1.Tables[0].Rows[0]["Lorry_Type"];
                        if (ViewState["isCalculateTDS"] != null)
                        {
                            if (ViewState["isCalculateTDS"].ToString() == "0")
                            {
                                txtTdsAmnt.Text = "0.00"; txtDieselAmnt.Text = "0.00";
                            }
                            else
                            {
                                calculateTDS(Convert.ToString(ds1.Tables[0].Rows[0]["Pan_no"]), dGrAmnt, Convert.ToInt32(lststate.State_Idno), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)));
                            }
                        }
                        netamntcal();
                    }

                    txtAdvAmnt.Focus();
                    if (Convert.ToBoolean(hidIsUnassignedLorry.Value == "" ? "false" : hidIsUnassignedLorry.Value))
                    {
                        ddlTruckNo.Enabled = true;
                    }
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    ddlDelvryPlace.Enabled = true;
                    ShowDiv("ShowBillAgainst('dvGrdetails')");
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }

        protected void txtHireAmnt_TextChanged(object sender, EventArgs e)
        {
            txtGrosstotal.Text = txtHireAmnt.Text;

        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            if ((hfTruckNoId.Value == "") && (txttruck.Visible == true || string.IsNullOrEmpty(txttruck.Text) == true) && (ddldelvplace.Visible == true || Convert.ToInt64(ddldelvplace.SelectedValue) <= 0))
            {
                lbltr.Text = "Please enter Truck No";
                ShowMessage("Please Select Truck No.");
            }
            else
            {
                lbltr.Text = "";

                try
                {
                    hidIsUnassignedLorry.Value = IsUnAssigned(ddldelvplace.SelectedValue).ToString();
                    //   SelectGrRetailer
                    ChlnBookingDAL obj = new ChlnBookingDAL();
                    ddldelvplace.SelectedValue = hfTruckNoId.Value;
                    Int64 icityIdno = (ddldelvplace.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddldelvplace.SelectedValue));
                    string strGrFrm = "";
                    if (ddlgrtyp.SelectedIndex == 0)
                        strGrFrm = "BK";
                    else if (ddlgrtyp.SelectedValue == "1")
                        strGrFrm = "CC";
                    DataTable DsGrdetail;
                    Int32 ifromCityIdno = (ddlFromCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                    Int32 iViaCityIdno = (ddlViaCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlViaCity.SelectedValue));
                    if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
                    {
                        if (ddlgrtyp.SelectedValue == "2")
                        {
                            DsGrdetail = obj.SelectGRRetailer("SelectGrRetailer", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(txtDateFrom.Text.Trim()), Convert.ToString(txtDateTo.Text), icityIdno, Convert.ToInt64(ddltrantype.SelectedValue), ApplicationFunction.ConnectionString(), ifromCityIdno, iViaCityIdno);
                        }
                        else
                        {
                            DsGrdetail = obj.SelectGRDetailTruckNo("SelectGRDetailTruckNo", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(txtDateFrom.Text.Trim()), Convert.ToString(txtDateTo.Text), icityIdno, strGrFrm, ApplicationFunction.ConnectionString(), ifromCityIdno, iViaCityIdno);
                        }
                    }
                    else
                    {
                        DsGrdetail = obj.selectGrDetails("SelectGRDetail", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), icityIdno, strGrFrm, ApplicationFunction.ConnectionString(), ifromCityIdno, iViaCityIdno);
                    }

                    if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                    {
                        grdGrdetals.DataSource = DsGrdetail;
                        grdGrdetals.DataBind();
                        lnkbtnSubmit.Visible = true; lnkbtnCloase.Visible = true;
                    }
                    else
                    {
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                        lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                    lnkbtnSearch.Focus();
                }
                catch (Exception Ex)
                {
                    ApplicationFunction.ErrorLog(Ex.Message);
                }
            }
        }

        private Boolean IsUnAssigned(string LorryIdno)
        {
            int Lorry_Idno = Convert.ToInt32(LorryIdno == "" ? "0" : LorryIdno);
            Boolean isUnassigned = false;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                isUnassigned = db.LorryMasts.Where(x => x.Lorry_Idno == Lorry_Idno).Select(x => x.UnAssigned_Lorry).SingleOrDefault();
                if (isUnassigned != null)
                {
                    return isUnassigned;
                }
            }
            return isUnassigned;
        }
        protected void lnkbtnAccPosting_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                Int32 Count = 0;
                ChlnBookingDAL objDal = new ChlnBookingDAL();
                DataSet objDataSet = objDal.AccPosting(ApplicationFunction.ConnectionString(), "ChallanPOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                {
                    AcntLinkDS = objDal.DtAcntDS(ApplicationFunction.ConnectionString());
                    DsHire = objDal.DsHireAcnt(ApplicationFunction.ConnectionString());
                    for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
                    {
                        Int64 Idno = (string.IsNullOrEmpty(objDataSet.Tables[0].Rows[i]["Chln_Idno"].ToString()) ? 0 : Convert.ToInt64(objDataSet.Tables[0].Rows[i]["Chln_Idno"].ToString()));
                        string Type = (string.IsNullOrEmpty(objDataSet.Tables[0].Rows[i]["Gr_type"].ToString()) ? "" : Convert.ToString(objDataSet.Tables[0].Rows[i]["Gr_type"]));
                        if (Idno > 0)
                        {
                            tblChlnBookHead chlnBookhead = objDal.selectHead(Idno, Type);
                            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                if (this.RecPostIntoAccounts(Convert.ToDouble(chlnBookhead.Adv_Amnt), Convert.ToInt32(Idno), "CB", 0, 0, 0, 0, 0, Convert.ToInt32(chlnBookhead.Year_Idno), Convert.ToInt32(chlnBookhead.Truck_Idno), Convert.ToString(chlnBookhead.Inst_Dt), (string.IsNullOrEmpty(chlnBookhead.Inst_No.ToString()) ? "0" : Convert.ToString(chlnBookhead.Inst_No)), (string.IsNullOrEmpty(chlnBookhead.Driver_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Driver_Idno)), Convert.ToDateTime(chlnBookhead.Chln_Date).ToString("dd-MM-yyyy"), Convert.ToInt32(chlnBookhead.Chln_No), (string.IsNullOrEmpty(chlnBookhead.RcptType_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.RcptType_Idno)), (string.IsNullOrEmpty(chlnBookhead.Bank_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Bank_Idno)), Convert.ToDouble(chlnBookhead.Gross_Amnt), Convert.ToDouble(chlnBookhead.Commsn_Amnt), Convert.ToDouble(chlnBookhead.TDSTax_Amnt), (string.IsNullOrEmpty(chlnBookhead.Diesel_Amnt.ToString()) ? 0.00 : Convert.ToDouble(chlnBookhead.Diesel_Amnt))) == true)
                                {
                                    Count++;
                                    tScope.Complete(); tScope.Dispose();
                                    objDal.UpdateIsPosting(Idno);
                                }
                                else
                                {
                                    tScope.Dispose(); this.PostingLeft();
                                    this.ShowMessageErr(hidpostingmsg.Value);
                                    return;
                                }
                            }
                        }
                    }
                    this.PostingLeft();
                    if (Count <= 0)
                    {
                        if (string.IsNullOrEmpty(hidpostingmsg.Value))
                        {
                            this.ShowMessageErr("No Record(s) Posted.");
                        }
                        else
                        {
                            this.ShowMessageErr(hidpostingmsg.Value);
                        }
                    }
                }
            }
        }
        protected void lnkbtnPrtyBlance_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "showPrtyBal();", true);
        }
        protected void lnkwithoutamount_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                Label rate = item.FindControl("rate") as Label;
                Label amount = item.FindControl("amount") as Label;
                Label WagesAmnt = item.FindControl("WagesAmnt") as Label;
                Label Totalamount = item.FindControl("Totalamount") as Label;
                rate.Visible = false; amount.Visible = false; Totalamount.Visible = false; WagesAmnt.Visible = false;

                //Finding the HeaderTemplate and access its controls 
                Control HeaderTemplate = Repeater1.Controls[0].Controls[0];
                Label lblrate = HeaderTemplate.FindControl("dr") as Label;
                Label rashi = HeaderTemplate.FindControl("rashi") as Label;
                Label utrai = HeaderTemplate.FindControl("utrai") as Label;
                Label totalrashi = HeaderTemplate.FindControl("totalrashi") as Label;
                lblrate.Visible = false; rashi.Visible = false; utrai.Visible = false; totalrashi.Visible = false;

                //Finding the FooterTemplate and access its controls 
                Control FooterTemplate = Repeater1.Controls[Repeater1.Controls.Count - 1].Controls[0];
                Label lblAmount = FooterTemplate.FindControl("lblAmount") as Label;
                Label lblWagesAmnt = FooterTemplate.FindControl("lblWagesAmnt") as Label;
                Label lblTotalAmnt = FooterTemplate.FindControl("lblTotalAmnt") as Label;
                lblAmount.Visible = false; lblWagesAmnt.Visible = false; lblTotalAmnt.Visible = false;

                if (lbltxtdelivery.Text == "")
                {
                    showdetl.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                else
                {
                    showdetl.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
            }
        }
        #endregion

        #region Functions...
        private void CurBalLoad()
        {
            double db, cr, dbopbal, cropbal, totdb, totcr;
            db = cr = dbopbal = cropbal = totdb = totcr = 0;
            if (string.IsNullOrEmpty(hidOwnerId.Value) == false)
            {
                SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString());
                db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=2, @ACNTIDNO='" + Convert.ToString(hidOwnerId.Value) + "',@YEARIDNO='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", con, true);
                cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=1, @ACNTIDNO='" + Convert.ToString(hidOwnerId.Value) + "',@YEARIDNO='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", con, true);
                dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(hidOwnerId.Value) + "',@YearIdno='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", con, true);
                cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(hidOwnerId.Value) + "',@YearIdno='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", con, true);
                totdb = db + dbopbal; totcr = cr + cropbal;
                if (totdb > totcr)
                {
                    lblPrtyBal.Text = Convert.ToDouble(totdb - totcr).ToString("N2") + "  Dr.";
                }
                else if (totdb < totcr)
                {
                    lblPrtyBal.Text = Convert.ToDouble(totcr - totdb).ToString("N2") + "  Cr.";
                }
                else
                    lblPrtyBal.Text = "0.00  Dr.";
            }
            else
            {
                lblPrtyBal.Text = "0.00 ";
            }
        }
        private void PostingLeft()
        {
            DataTable dtPosLeft = new DataTable();
            ChlnBookingDAL objDal = new ChlnBookingDAL();
            DataSet objDataSets = objDal.AccPosting(ApplicationFunction.ConnectionString(), "ChallanPOS", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
            if (objDataSets != null && objDataSets.Tables.Count > 0 && objDataSets.Tables[1].Rows.Count > 0)
            {
                dtPosLeft = objDataSets.Tables[1];
            }
            lblPostingLeft.Text = Convert.ToString(dtPosLeft.Rows[0][0]);
        }

        private void AutofillDefault()
        {
            try
            {
                ChlnBookingDAL obj = new ChlnBookingDAL(); Int64 Yearidno = 0;
                Yearidno = obj.AutofillYear();
                ddldateRange.SelectedValue = Convert.ToString(Yearidno == 1 ? "1" : "2");
                txtDate.Text = obj.AutofillDate();
                txtDlyDate.Text = obj.AutofillDate();
            }
            catch (Exception Ex)
            {
            }
        }
        private void calculateTDS(string StrPANNo, double dGrAmnt, Int32 StateIdno, DateTime Chlndate)
        {
            ChlnBookingDAL ObjChlnBookingDAL = new ChlnBookingDAL();
            DataTable dtTDS = new DataTable();
            dGrCommiAmnt = Convert.ToDouble((txtcommission.Text) == "" ? "0" : txtcommission.Text);
            dtTDS = ObjChlnBookingDAL.SelectDataForTDS(ApplicationFunction.ConnectionString(), StrPANNo, StateIdno, Chlndate);
            if ((dtTDS != null) && (dtTDS.Rows.Count > 0))
            {
                for (int i = 0; i < dtTDS.Rows.Count; i++)
                {
                    if (StrPANNo != "")
                    {
                        if ((Convert.ToInt32(dtTDS.Rows[i]["TOTLorryNo"]) >= Convert.ToInt32(dtTDS.Rows[i]["LorryCnt_From"])) && (Convert.ToInt32(dtTDS.Rows[i]["LorryCnt_To"]) >= Convert.ToInt32(dtTDS.Rows[i]["TOTLorryNo"])))
                        {
                            if (Convert.ToBoolean(dtTDS.Rows[i]["CalOnDF_TM"]) == true && Convert.ToBoolean(dtTDS.Rows[i]["CalOnDF_LM"]) == false)
                            {
                                if (ViewState["isCalculateTDS"] != null)
                                {
                                    if (ViewState["isCalculateTDS"].ToString() == "0")
                                    {
                                        txtTdsAmnt.Text = "0.00";
                                    }
                                    else
                                    {
                                        txtTdsAmnt.Text = Convert.ToDouble((((dGrAmnt - dGrCommiAmnt) * Convert.ToDouble(dtTDS.Rows[i]["Tax_Rate"])) / 100)).ToString("N2");
                                    }
                                }
                            }
                            else if (Convert.ToBoolean(dtTDS.Rows[i]["CalOnDF_TM"]) == true && Convert.ToBoolean(dtTDS.Rows[i]["CalOnDF_LM"]) == true)
                            {
                                txtTdsAmnt.Text = "0.00";
                            }
                            else if (Convert.ToBoolean(dtTDS.Rows[i]["CalOnDF_TM"]) == false)
                            {
                                if (ViewState["isCalculateTDS"] != null)
                                {
                                    if (ViewState["isCalculateTDS"].ToString() == "0")
                                    {
                                        txtTdsAmnt.Text = "0.00";
                                    }
                                    else
                                    {
                                        txtTdsAmnt.Text = Convert.ToDouble((((dGrAmnt - dGrCommiAmnt) * Convert.ToDouble(dtTDS.Rows[i]["Tax_Rate"])) / 100)).ToString("N2");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        txtTdsAmnt.Text = Convert.ToDouble((((dGrAmnt - dGrCommiAmnt) * Convert.ToDouble(hidTdsTaxPer.Value)) / 100)).ToString("N2");
                    }
                }
            }
            else
            {
                if (ViewState["isCalculateTDS"] != null)
                {
                    txtTdsAmnt.Text = "0.00";
                }
            }
            if (ViewState["isCalculateTDS"] != null)
            {
                if (ViewState["isCalculateTDS"].ToString() == "0")
                {
                    txtDieselAmnt.Text = "0.00";
                }
            }
        }
        private void CalculateCommissionAmnt(Int32 TruckIdno, bool Calculate)
        {
            ChlnBookingDAL ObjChlnBookingDAL = new ChlnBookingDAL();
            string strchkDetlValue = "";
            if (ddlTruckNo.SelectedIndex > 0)
            {
                double dcommssn = 0;
                if (Calculate == true)
                {
                    if (grdMain.Rows.Count > 0)
                    {
                        DataTable DT1 = (DataTable)ViewState["dt"];
                        for (int count = 0; count < DT1.Rows.Count; count++)
                        {
                            string GRIdno = Convert.ToString(DT1.Rows[count]["Gr_Idno"]);
                            strchkDetlValue = strchkDetlValue + GRIdno + ",";
                        }
                        if (strchkDetlValue != "")
                        {
                            strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                        }
                        //  if (Convert.ToString((txtcommission.Text) == "" ? "0.00" : txtcommission.Text) == "0.00")
                        //{
                        DataTable dtCommssn = new DataTable(); DataRow Drc;
                        string date = txtDate.Text.Trim();
                        dtCommssn = ObjChlnBookingDAL.SelectDataForCommissinAmnt(ApplicationFunction.ConnectionString(), Convert.ToString(strchkDetlValue), date);
                        if ((dtCommssn != null) && (dtCommssn.Rows.Count > 0))
                        {

                            dcommssn = Convert.ToDouble(dtCommssn.Rows[0]["TOT_COMMSN"]);
                            txtcommission.Text = dcommssn.ToString("N2");
                        }
                        else
                        {
                            txtcommission.Text = "0.00";
                        }
                        //}
                    }

                }
                else
                {
                    txtcommission.Text = "0.00";
                }

            }
            else
            {
                txtcommission.Text = "";
            }
            netamntcal();
        }
        private void BindRcpt()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
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
            ChlnBookingDAL obj = new ChlnBookingDAL();
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
        private void ChallanNo(Int32 YearIdno, Int32 FromCityIdno)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            txtchallanNo.Text = Convert.ToString(obj.GetChallanNo(YearIdno, FromCityIdno, ApplicationFunction.ConnectionString()));
        }
        private void netamntcal()
        {
            try
            {
                string LorryOwnerType = ViewState["LorryOwnerType"] != null ? Convert.ToString(ViewState["LorryOwnerType"]) : "";
                string HireAmnt = txtHireAmnt.Text == "" ? "0" : txtHireAmnt.Text;

                txtGrosstotal.Text = LorryOwnerType == "H" ? Math.Round(Convert.ToDouble(HireAmnt)).ToString("N2") : Math.Round(Convert.ToDouble(txtGrosstotal.Text)).ToString("N2");
                txtcommission.Text = Convert.ToDouble(txtcommission.Text).ToString("N2");
                txtTdsAmnt.Text = Math.Round(Convert.ToDouble(txtTdsAmnt.Text)).ToString("N2");
                txtDieselAmnt.Text = Convert.ToDouble(txtDieselAmnt.Text).ToString("N2");

                txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text) + Convert.ToDouble(txtDetention.Text) + Convert.ToDouble(txtHamaliCharge.Text)) - (Convert.ToDouble(txtcommission.Text) + Convert.ToDouble(txtAdvAmnt.Text) + Convert.ToDouble(txtTdsAmnt.Text) + Convert.ToDouble(txtDieselAmnt.Text) + Convert.ToDouble(txtLatecharge.Text) + Convert.ToDouble(txtRassaTripal.Text))).ToString("N2");
                if (Convert.ToDouble(txtNetAmnt.Text) < 0)
                {
                    txtNetAmnt.Text = "0.00";
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
        private void BindCity()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            var lst = obj.SelectCityCombo();
            var ViaCity = obj.SelectAllCityCombo();
            obj = null;

            if (lst.Count > 0)
            {
                ddldelvplace.DataSource = lst;
                ddldelvplace.DataTextField = "City_Name";
                ddldelvplace.DataValueField = "City_Idno";
                ddldelvplace.DataBind();


                //ddlFromCity.DataSource = lst;
                //ddlFromCity.DataTextField = "City_Name";
                //ddlFromCity.DataValueField = "City_Idno";
                //ddlFromCity.DataBind();


                ddlDelvryPlace.DataSource = lst;
                ddlDelvryPlace.DataTextField = "City_Name";
                ddlDelvryPlace.DataValueField = "City_Idno";
                ddlDelvryPlace.DataBind();

            }
            ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDelvryPlace.Items.Insert(0, new ListItem("--Select--", "0"));


            if (ViaCity != null)
            {
                ddlDivTocity.DataSource = ViaCity;
                ddlDivTocity.DataTextField = "City_Name";
                ddlDivTocity.DataValueField = "City_Idno";
                ddlDivTocity.DataBind();
                ddlDivTocity.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlViaCity.DataSource = ViaCity;
                ddlViaCity.DataTextField = "City_Name";
                ddlViaCity.DataValueField = "City_Idno";
                ddlViaCity.DataBind();
                ddlViaCity.Items.Insert(0, new ListItem("--Select--", "0"));

            }


        }
        private void BindFromCity()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
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
            ChlnBookingDAL obj = new ChlnBookingDAL();
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

        //bind
        private void Bind()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;

            if (lst.Count > 0)
            {
                ddlTruckNo.DataSource = lst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();
                Lorrytype.Text = "Truck No.";
            }
            ddlTruckNo.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        private void BindPopulate()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNoPopulate();
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
                    txtDlyDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtInstDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                    //txtDateTo.Text = hidmindate.Value;
                }
            }
        }
        private void Populate(Int64 HeadId, string Type)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            tblChlnBookHead chlnBookhead = obj.selectHead(HeadId, Type);
            ddldateRange.SelectedValue = Convert.ToString(chlnBookhead.Year_Idno);

            Int32 trantype = Convert.ToInt32(chlnBookhead.Tran_Type);

            hidtrantype.Value = Convert.ToString(trantype);
            if (trantype == 0)
            {
                Bind();
                Lorrytype.Text = "Truck No.";
            }
            else if (trantype == 1)
            {
                ChlnBookingDAL obja = new ChlnBookingDAL();

                var lst = obja.BindTransportaion(Convert.ToInt64(trantype));
                if (lst.Count > 0)
                {
                    ddlTruckNo.DataSource = lst;
                    ddlTruckNo.DataTextField = "Misc_Name";
                    ddlTruckNo.DataValueField = "Misc_Idno";
                    ddlTruckNo.DataBind();
                    Lorrytype.Text = "By Air";
                }

            }
            else if (trantype == 2)
            {
                ChlnBookingDAL obja = new ChlnBookingDAL();

                var lst = obja.BindTransportaion(trantype);
                if (lst.Count > 0)
                {
                    ddlTruckNo.DataSource = lst;
                    ddlTruckNo.DataTextField = "Misc_Name";
                    ddlTruckNo.DataValueField = "Misc_Idno";
                    ddlTruckNo.DataBind();
                    Lorrytype.Text = "By Train";
                }
            }
            else
            {
                ChlnBookingDAL obja = new ChlnBookingDAL();

                var lst = obja.BindTransportaion(trantype);
                if (lst.Count > 0)
                {
                    ddlTruckNo.DataSource = lst;
                    ddlTruckNo.DataTextField = "Misc_Name";
                    ddlTruckNo.DataValueField = "Misc_Idno";
                    ddlTruckNo.DataBind();
                    Lorrytype.Text = "By Bus";
                }
            }

            //ADD COLUMN FOR OM CARGO 
            txtstartkm.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Start_Km)) ? "0" : Convert.ToDouble(chlnBookhead.Start_Km).ToString("N2");
            txtCloseKm.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Close_Km)) ? "0" : Convert.ToDouble(chlnBookhead.Close_Km).ToString("N2");
            txtLatecharge.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Late_Charge)) ? "0" : Convert.ToDouble(chlnBookhead.Late_Charge).ToString("N2");
            txtHamaliCharge.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Hamali)) ? "0" : Convert.ToDouble(chlnBookhead.Hamali).ToString("N2");
            txtDetention.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Dentation)) ? "0" : Convert.ToDouble(chlnBookhead.Dentation).ToString("N2");
            txtFreight.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Freight)) ? "0" : Convert.ToDouble(chlnBookhead.Freight).ToString("N2");
            txtDlyDate.Text = chlnBookhead.DelvDate == null ? "" : Convert.ToDateTime(chlnBookhead.DelvDate).ToString("dd-MM-yyyy");
            txtRatePerKM.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.RateKM)) ? "0" : Convert.ToDouble(chlnBookhead.RateKM).ToString("N2");
            txtRassaTripal.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.RassaTirpal_Chrg)) ? "0" : Convert.ToDouble(chlnBookhead.RassaTirpal_Chrg).ToString("N2");
            txtHireAmnt.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.Hire_Amnt)) ? "0" : Convert.ToDouble(chlnBookhead.Hire_Amnt).ToString("N2");
            //

            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtchallanNo.Text = chlnBookhead.Chln_No;
            txtDate.Text = chlnBookhead.Chln_Date == null ? "" : Convert.ToDateTime(chlnBookhead.Chln_Date).ToString("dd-MM-yyyy");
            ddlFromCity.SelectedValue = Convert.ToString(chlnBookhead.BaseCity_Idno);
            ddlDelvryPlace.SelectedValue = Convert.ToString(chlnBookhead.DelvryPlc_Idno);
            hidTdsTaxPer.Value = Convert.ToString(chlnBookhead.STaxPer_TDS);
            ddlTruckNo.SelectedValue = Convert.ToString(chlnBookhead.Truck_Idno);
            ddlTruckNo_SelectedIndexChanged(null, null);
            ddldriverName.SelectedValue = Convert.ToString(chlnBookhead.Driver_Idno);
            txtDelvInstruction.Text = Convert.ToString(chlnBookhead.Delvry_Instrc);
            txtAdvAmnt.Text = Convert.ToDouble(chlnBookhead.Adv_Amnt).ToString("N2");
            txtAdvAmnt_TextChanged(null, null);
            txtcommission.Text = Convert.ToDouble(chlnBookhead.Commsn_Amnt).ToString("N2");
            txtDieselAmnt.Text = Convert.ToDouble(chlnBookhead.Diesel_Amnt).ToString("N2");
            txtTdsAmnt.Text = Convert.ToDouble(chlnBookhead.TDSTax_Amnt).ToString("N2");
            txtNetAmnt.Text = Convert.ToDouble(chlnBookhead.Net_Amnt).ToString("N2");
            hidWorkType.Value = Convert.ToString(chlnBookhead.Work_type);
            ddlacntname.SelectedValue = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.DieselAcnt_IDno)) ? "0" : Convert.ToString(chlnBookhead.DieselAcnt_IDno);
            txtMno.Text = string.IsNullOrEmpty(Convert.ToString(chlnBookhead.ManualNo)) ? "" : Convert.ToString(chlnBookhead.ManualNo);
            if (Convert.ToDouble(chlnBookhead.Adv_Amnt) > 0)
            {
                ddlRcptType.SelectedValue = Convert.ToString(chlnBookhead.RcptType_Idno);
                ddlRcptType_SelectedIndexChanged(null, null);
                ddlCusBank.SelectedValue = Convert.ToString(chlnBookhead.Bank_Idno);
            }
            else
            {
                ddlRcptType.SelectedIndex = 0;
                ddlCusBank.SelectedIndex = 0;
            }

            divPrtyPmnt.Visible = true;
            txtInstNo.Text = Convert.ToString(chlnBookhead.Inst_No);
            txtInstDate.Text = ((chlnBookhead.Inst_Dt == null) ? "" : Convert.ToDateTime(chlnBookhead.Inst_Dt).ToString("dd-MM-yyyy"));
            ddlFromCity.Enabled = false;
            if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
            {
                lblDelvPlace.Visible = false;
                ddlDelvryPlace.Visible = false;
                ddlTruckNo.Enabled = false;
            }
            else
            {
                lblDelvPlace.Visible = true;
                ddlDelvryPlace.Visible = true;
                ddlTruckNo.Enabled = true;
                ddlDelvryPlace.Enabled = false;
            }
            DtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId, Type);
            DtTempFuel = obj.selectFuelDetl(ApplicationFunction.ConnectionString(), HeadId);
            txtKms.Text = string.IsNullOrEmpty(Convert.ToString(DtTemp.Rows[0]["Kms"])) ? "0" : Convert.ToString(DtTemp.Rows[0]["Kms"]);
            ViewState["dt"] = DtTemp;
            ViewState["dtfuel"] = DtTempFuel;

            this.BindGrid();
            this.BindGridFuel();
            ////for (i = 0; i < DtTemp.Rows.Count; i++)
            ////{
            ////    if (Convert.ToString(DtTemp.Rows[i]["Inv_No"]) != "")
            ////    {
            ////        LblChllnStatus.Text = "BUILD";
            ////    }
            ////}
            lnkbtnSearch.Enabled = false;
            Int64 value = 0;
            value = obj.CheckBilled(HeadId, ApplicationFunction.ConnectionString(), Type);
            if (value > 0)
            {
                lnkbtnSave.Enabled = true;

            }
            else
            {
                lnkbtnSave.Enabled = true;
            }
            PrintChallan(HeadId, Type);
            obj = null;
        }
        private void Clear()
        {
            ddlFromCity.Enabled = true;
            ddlDelvryPlace.SelectedValue = "0";
            ViewState["dt"] = null;
            DtTemp = null;
            hidid.Value = string.Empty; hidOwnerId.Value = string.Empty; hidePanNo.Value = hideStateId.Value = string.Empty;
            ddldriverName.SelectedValue = "0";
            //  ddlTruckNo.SelectedValue = "0";
            // ddlFromCity.SelectedValue = "0";
            ddldelvplace.SelectedValue = "0"; txtOwnrNme.Text = "";
            //txtDate.Text = "";
            txtchallanNo.Text = "";
            //ddldateRange.SelectedIndex = 0; ;
            // ddldateRange_SelectedIndexChanged(null, null);
            BindGrid();
            ddlDelvryPlace.Enabled = true;
            txtDelvInstruction.Text = "";
            txtGrosstotal.Text = "0.00";
            txtAdvAmnt.Text = "0.00";
            txtNetAmnt.Text = "0.00";
            txtcommission.Text = "0.00";

            txtstartkm.Text = "0.00";
            txtCloseKm.Text = "0.00";
            txtLatecharge.Text = "0.00";
            txtHamaliCharge.Text = "0.00";
            txtDetention.Text = "0.00";
            txtFreight.Text = "0.00";
            txtRatePerKM.Text = "0.00";
            txtRassaTripal.Text = "0.0";
            txtTdsAmnt.Text = "0.0";
            ddldateRange.Enabled = true;
            // ddldateRange.SelectedIndex = 0;
            lnkbtnSearch.Enabled = true; ddlRcptType.SelectedIndex = ddlCusBank.SelectedIndex = 0; txtInstNo.Text = "";
            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL(); txtInstDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            tblUserPref obj = objChlnBookingDAL.selectUserPref();
            if (obj != null)
            {
                hidWorkType.Value = Convert.ToString(obj.Work_Type);
            }
            lnkbtnSave.Enabled = true;
            // Response.Redirect("ChlnBooking.aspx");
            //lnkbtnPrint.Visible = false;

            ddlRcptType.Enabled = false;
            txtDieselAmnt.Text = "0.00";
            lnkbtnNew.Visible = false;
            divPosting.Visible = true;
            lastprint.Visible = false;
            lnkbtnprintOM.Visible = false;
            divPrtyPmnt.Visible = false;
            txtMno.Text = "";
            txtHireAmnt.Text = "0.00";
        }
        private void BindGrid()
        {
            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
            tblUserPref obj = objChlnBookingDAL.selectUserPref();
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
            if (string.IsNullOrEmpty(obj.WagesLabel_Print))
                grdMain.HeaderRow.Cells[10].Text = "Wadges Amnt";
            else
                grdMain.HeaderRow.Cells[10].Text = Convert.ToString(obj.WagesLabel_Print);

        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }

        private void BindNullGird()
        {
            grdMain.DataSource = null;
            grdMain.DataBind();

        }

        private bool PostIntoAccounts(double Amount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno)
        {
            #region Variables Declaration...

            Int64 intVchrIdno = 0;
            Int64 intValue = 0;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            double dblAdvanceAmnt = 0;
            if ((string.IsNullOrEmpty(txtAdvAmnt.Text.Trim()) == false) && (Convert.ToDouble(txtAdvAmnt.Text.Trim()) > 0))
            {
                if (Convert.ToDouble(txtAdvAmnt.Text) > 0)
                {
                    dblAdvanceAmnt = Convert.ToDouble(txtAdvAmnt.Text.Trim());
                }
            }

            DateTime? dtPBillDate = null;
            DateTime? dtBankDate = null;
            if (string.IsNullOrEmpty(txtInstDate.Text) == false)
            {
                dtBankDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
            }
            #endregion
            ChlnBookingDAL obj = new ChlnBookingDAL();
            Int32 ILType = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue)); //Convert.ToInt32(clsDataAccessFunction.ExecuteScaler("select Lorry_type from LorryMast where Lorry_Idno=" + Convert.ToInt32(cmbTruckNo.SelectedValue) + "", Tran, Program.DataConn));
            if (Request.QueryString["q"] == null)
            {
                intValue = 1;
            }
            else
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
            }
            #region  Advance Amount Posting....................

            if (Amount > 0)
            {
                Int32 IAcntIdno = 0;

                if (ILType == 0)
                {
                    IAcntIdno = Convert.ToInt32(ddldriverName.SelectedValue);
                }
                else
                {
                    IAcntIdno = Convert.ToInt32((string.IsNullOrEmpty((hidOwnerId.Value)) ? "0" : hidOwnerId.Value));
                }

                if (intValue > 0)
                {
                    Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(1, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                    intValue = objclsAccountPosting.InsertInVchrHead(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())), 1, Convert.ToInt32(Request.Form[ddlRcptType.UniqueID]),
                    "Challan No.:" + Convert.ToString(txtchallanNo.Text) + " Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(), true, 0, strDocType, 0, 0, 0,
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())), VchrNo, 0, YearIdno, 0, 0);
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno, //Convert.ToInt32(ddlRcptType.SelectedValue),
                        Convert.ToInt32(Request.Form[ddlRcptType.UniqueID]),
                         "Challan No.:" + Convert.ToString(txtchallanNo.Text) + " Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                        Amount,
                        Convert.ToByte(1),
                        Convert.ToByte(0),
                        Convert.ToString(txtInstNo.Text),
                        true,
                        dtBankDate,
                        Convert.ToString(ddlCusBank.SelectedValue), 0);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            IAcntIdno,
                             "Challan No.: " + Convert.ToString(txtchallanNo.Text) + " Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                            Amount,
                            Convert.ToByte(2),
                            Convert.ToByte(0),
                             Convert.ToString(txtInstNo.Text),
                            false,
                              dtBankDate,
                            Convert.ToString(ddlCusBank.SelectedValue), 0);
                        }
                        else
                        {
                            return false;
                        }
                        #region VchrIdDetl Posting...

                        if (intValue > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                            if (intValue == 0)
                            {
                                return false;
                            }
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
            }

            #endregion

            #region Hire  Amount Posting............

            if ((ILType == 1) || (ILType == 0))
            {
                #region Account link Validations...

                if (AcntLinkDS == null || AcntLinkDS.Rows.Count <= 0)
                {
                    hidpostingmsg.Value = "Account link is not defined. Kindly define.";
                    return false;
                }
                ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["CAcnt_Idno"]));
                if (ICAcnt_Idno <= 0)
                {
                    hidpostingmsg.Value = "Comission Account is not defined. Kindly define.";
                    return false;
                }

                if (DsHire == null || DsHire.Rows.Count <= 0)
                {
                    hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                    return false;
                }
                else
                {
                    IHireAcntIdno = Convert.ToInt32(DsHire.Rows[0]["HireAccountID"]);
                }

                #endregion
                double GrossAmnt = 0;
                if (ILType == 0)
                    GrossAmnt = Convert.ToDouble(txtNetAmnt.Text);
                else
                    GrossAmnt = Convert.ToDouble(txtGrosstotal.Text);
                dCommissionAmnt = Convert.ToDouble(txtcommission.Text);
                DateTime? dtInvoiceDate = null;
                if (GrossAmnt > 0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())), 4, 0,
                        "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                        true, 0, strDocType, 0, 0, Convert.ToInt64(txtchallanNo.Text.Trim()), ApplicationFunction.GetIndianDateTime().Date,
                        VchrNo, 0, Convert.ToInt32(ddldateRange.SelectedValue), 0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;

                            #region Sender Account Posting + Commission Amount + Gross Amount Posting...
                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno, string.IsNullOrEmpty(hidOwnerId.Value) ? 0 : Convert.ToInt64(hidOwnerId.Value)
                                ,
                            "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                            GrossAmnt, Convert.ToByte(1), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(intVchrIdno, IHireAcntIdno,
                                      "Challan. No: " + Convert.ToString(txtchallanNo.Text) + " Challan. Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                                       Convert.ToDouble(GrossAmnt), Convert.ToByte(2), Convert.ToByte(0), "", false, null,  //  please check here if date is Blank
                                       "", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
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
                }

                #region Commission Account Posting ................
                if (dCommissionAmnt > 0)
                {
                    intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, "CBU");
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())),
                        4,
                        0,
                        "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                        true,
                        0,
                        strDocType,
                        0,
                        0,
                        Convert.ToInt64(txtchallanNo.Text.Trim()),
                        ApplicationFunction.GetIndianDateTime().Date,
                        VchrNo,
                        0,
                        Convert.ToInt32(ddldateRange.SelectedValue),
                        0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;

                            #region Commission Amount  Posting...

                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt64(ICAcnt_Idno),
                            "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                            dCommissionAmnt,
                            Convert.ToByte(1),
                            Convert.ToByte(0),
                            "",
                            true,
                            null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                    intVchrIdno, string.IsNullOrEmpty(hidOwnerId.Value) ? 0 : Convert.ToInt64(hidOwnerId.Value),
                                      "Challan. No: " + Convert.ToString(txtchallanNo.Text) + " Challan. Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                                       Convert.ToDouble(dCommissionAmnt),
                                       Convert.ToByte(2),
                                       Convert.ToByte(0),
                                       "",
                                       false,
                                       null,  //please check here if date is Blank
                                       "", 0);

                                if (intValue == 0)
                                {
                                    return false;
                                }

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
                }
                #endregion
            }

            #endregion

            #region  TDS Account Posting..................
            if (AcntLinkDS == null || AcntLinkDS.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Account link is not defined. Kindly define.";
                return false;
            }
            IntTDSAcntIdno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["TDS_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["TDS_Idno"]));
            if (IntTDSAcntIdno <= 0)
            {
                hidpostingmsg.Value = "TDS Account is not defined. Kindly define.";
                return false;
            }
            double dTdsAmnt = 0;
            dTdsAmnt = Convert.ToDouble(txtTdsAmnt.Text);
            if (dTdsAmnt > 0)
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, "CBU");
                Int32 ITDSPrtyIdno = 0;
                if (ILType == 0)
                {
                    ITDSPrtyIdno = Convert.ToInt32(ddldriverName.SelectedValue);
                }
                else
                {
                    ITDSPrtyIdno = Convert.ToInt32((string.IsNullOrEmpty((hidOwnerId.Value)) ? "0" : hidOwnerId.Value));
                }
                if (intValue > 0)
                {
                    Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())), 4, 0, "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                    true, 0, strDocType, 0, 0, Convert.ToInt64(txtchallanNo.Text.Trim()), ApplicationFunction.GetIndianDateTime().Date, VchrNo, 0,
                    Convert.ToInt32(ddldateRange.SelectedValue), 0, intUserIdno); if (intValue > 0)
                    {
                        intVchrIdno = intValue;

                        #region TDS Account Posting .........

                        intValue = 0;
                        /*Insert In VchrDetl*/
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno, Convert.ToInt64(ITDSPrtyIdno), "Challan No: " + Convert.ToString(txtchallanNo.Text) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                        dTdsAmnt, Convert.ToByte(2), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                        "", 0);
                        if (intValue > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                   intVchrIdno, IntTDSAcntIdno, "Challan. No: " + Convert.ToString(txtchallanNo.Text) + " Challan. Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                                   Convert.ToDouble(dTdsAmnt), Convert.ToByte(1), Convert.ToByte(0), "", false,
                                   null,  //please check here if date is Blank
                                   "", 0);

                            if (intValue == 0)
                            {
                                return false;
                            }

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
            }

            #endregion

            #region Diesel Account Posting ................
            Double DieselAmnt = string.IsNullOrEmpty(Convert.ToString(txtDieselAmnt.Text)) ? 0.00 : Convert.ToDouble(txtDieselAmnt.Text);
            if (DieselAmnt > 0)
            {
                DtTempFuel = (DataTable)ViewState["dtfuel"];
                Int64 idno = Convert.ToInt64(DtTempFuel.Rows[0]["acnt_idno"]);
                IntDieselAcc_Idno = idno;//Convert.ToInt32(ddlacntname.SelectedValue);
                if (IntDieselAcc_Idno <= 0)
                {
                    hidpostingmsg.Value = "Diesel Account is not defined. Kindly define.";
                    return false;
                }
            }
            double dDieselAmnt = 0;
            dDieselAmnt = Convert.ToDouble(txtDieselAmnt.Text);
            if (dDieselAmnt > 0)
            {
                Int32 IAcntIdno = 0;

                if (ILType == 0)
                {
                    IAcntIdno = Convert.ToInt32(ddldriverName.SelectedValue);
                }
                else
                {
                    IAcntIdno = Convert.ToInt32((string.IsNullOrEmpty((hidOwnerId.Value)) ? "0" : hidOwnerId.Value));
                }

                if (intValue > 0)
                {
                    Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())),
                    4,
                    0,
                    "Challan No: " + Convert.ToString(txtchallanNo.Text.Trim()) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                    true,
                    0,
                    strDocType,
                    0,
                    0,
                    Convert.ToInt64(txtchallanNo.Text.Trim()),
                    ApplicationFunction.GetIndianDateTime().Date,
                    VchrNo,
                    0,
                    YearIdno,
                    0, intUserIdno);
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        #region Desial Amount  Posting...
                        intValue = 0;
                        /*Insert In VchrDetl*/
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt64(IntDieselAcc_Idno),
                        "Challan No: " + Convert.ToString(txtchallanNo.Text.Trim()) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                        dDieselAmnt,
                        Convert.ToByte(1),
                        Convert.ToByte(0),
                        "",
                        true,
                        null,  //please check here if date is Blank
                        "", 0);
                        if (intValue > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                                   intVchrIdno,
                                   Convert.ToInt64(IAcntIdno),
                                  "Challan No: " + Convert.ToString(txtchallanNo.Text.Trim()) + " Challan Date: " + txtDate.Text.Trim() + " Lorry: " + ddlTruckNo.SelectedItem.Text.Trim(),
                                   Convert.ToDouble(dDieselAmnt),
                                   Convert.ToByte(2),
                                   Convert.ToByte(0),
                                   "",
                                   false,
                                   null,  //please check here if date is Blank
                                   "", 0);

                            if (intValue == 0)
                            {
                                return false;
                            }

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

            }
            #endregion

            objclsAccountPosting = null;
            return true;

        }

        private bool RecPostIntoAccounts(double AdvAmount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno, Int32 TruckIdno, string InstDate, string InstNo, Int32 DriverIdno, string strDate, Int32 intChlnNo, Int32 intRcptType, Int32 intCustBIdno, double dGrossAmnt, double dCommissionAmnt, double dTdsAmnt, double dDiesel)
        {
            #region Variables Declaration...
            Int64 intVchrIdno = 0; Int64 intValue = 0; Int32 IAcntIdno = 0; DateTime? dtBankDate = null;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            BindDropdownDAL objDal = new BindDropdownDAL();
            #endregion

            DataSet dsLD = objDal.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", TruckIdno, strDate);

            if (dsLD != null && dsLD.Tables.Count > 0 && dsLD.Tables[0].Rows.Count > 0)
            {
                Int32 intLtype = string.IsNullOrEmpty(dsLD.Tables[0].Rows[0]["Lorry_Type"].ToString()) ? 0 : Convert.ToInt32(dsLD.Tables[0].Rows[0]["Lorry_Type"]);
                string strLorryNo = Convert.ToString(dsLD.Tables[0].Rows[0]["Lorry_No"]);
                Int32 PartyIdno = Convert.ToInt32(dsLD.Tables[0].Rows[0]["Acnt_Idno"]);

                if (intLtype == 0) { IAcntIdno = DriverIdno; } else { IAcntIdno = PartyIdno; }

                #region Account link Validations...

                if (AcntLinkDS == null || AcntLinkDS.Rows.Count <= 0)
                {
                    hidpostingmsg.Value = "Account link is not defined. Kindly define.";
                    return false;
                }

                ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["CAcnt_Idno"]));
                if (ICAcnt_Idno <= 0)
                {
                    hidpostingmsg.Value = "Commission Account is not defined. Kindly define.";
                    return false;
                }
                if (DsHire == null || DsHire.Rows.Count <= 0)
                {
                    hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                    return false;
                }
                else
                {
                    IHireAcntIdno = Convert.ToInt32(DsHire.Rows[0]["HireAccountID"]);
                }
                IntTDSAcntIdno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["TDS_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["TDS_Idno"]));
                if (IntTDSAcntIdno <= 0)
                {
                    hidpostingmsg.Value = "TDS Account is not defined. Kindly define.";
                    return false;
                }
                //IntDieselAcc_Idno = Convert.ToInt32(ddlAcntLink.SelectedValue);
                //if (IntDieselAcc_Idno <= 0)
                //{
                //    hidpostingmsg.Value = "Diesel Account is not defined. Kindly define.";
                //    return false;
                //}

                #endregion

                #region Amount Posting............

                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);

                #region Gross Amount Posting...
                if (dGrossAmnt > 0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)), 4, 0,
                        "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                        true, 0, strDocType, 0, 0, Convert.ToInt64(intChlnNo), ApplicationFunction.GetIndianDateTime().Date,
                        VchrNo, 0, YearIdno, 0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;
                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno, PartyIdno,
                            "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                            dGrossAmnt, Convert.ToByte(1), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(intVchrIdno, IHireAcntIdno,
                                      "Challan. No: " + Convert.ToString(intChlnNo) + " Challan. Date: " + strDate + " Lorry: " + strLorryNo,
                                       Convert.ToDouble(dGrossAmnt), Convert.ToByte(2), Convert.ToByte(0), "", false, null,  //  please check here if date is Blank
                                       "", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
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
                }
                #endregion

                #region  Advance Amount Posting....................
                if (AdvAmount > 0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(1, 0, YearIdno);
                        intValue = objclsAccountPosting.InsertInVchrHead(Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)), 1, Convert.ToInt32(intRcptType),
                        "Challan No.:" + Convert.ToString(intChlnNo) + " Date: " + strDate + " Lorry: " + strLorryNo, true, 0, strDocType, 0, 0, 0,
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)), VchrNo, 0, YearIdno, 0, 0);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt32(intRcptType),
                            "Challan No.:" + Convert.ToString(intChlnNo) + " Date: " + strDate + " Lorry: " + strLorryNo,
                            AdvAmount,
                            Convert.ToByte(1),
                            Convert.ToByte(0),
                            Convert.ToString(InstNo),
                            true,
                            (Convert.ToInt32(InstNo) > 0) ? Convert.ToDateTime(ApplicationFunction.mmddyyyy(InstDate)) : dtBankDate,
                            Convert.ToString(intCustBIdno), 0);
                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno,
                                IAcntIdno,
                                "Challan No.: " + Convert.ToString(intChlnNo) + " Date: " + strDate + " Lorry: " + strLorryNo,
                                AdvAmount,
                                Convert.ToByte(2),
                                Convert.ToByte(0),
                                Convert.ToString(InstNo),
                                false,
                                dtBankDate,
                                Convert.ToString(intCustBIdno), 0);
                            }
                            else
                            {
                                return false;
                            }
                            #region VchrIdDetl Posting...

                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                if (intValue == 0)
                                {
                                    return false;
                                }
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
                }
                #endregion

                #region Commission Account Posting ................
                if (dCommissionAmnt > 0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)),
                        4,
                        0,
                        "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                        true,
                        0,
                        strDocType,
                        0,
                        0,
                        Convert.ToInt64(intChlnNo),
                        ApplicationFunction.GetIndianDateTime().Date,
                        VchrNo,
                        0,
                        YearIdno,
                        0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;
                            #region Commission Amount  Posting...
                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt64(ICAcnt_Idno),
                            "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                            dCommissionAmnt,
                            Convert.ToByte(1),
                            Convert.ToByte(0),
                            "",
                            true,
                            null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno,
                                       Convert.ToInt64(PartyIdno),
                                      "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                                       Convert.ToDouble(dCommissionAmnt),
                                       Convert.ToByte(2),
                                       Convert.ToByte(0),
                                       "",
                                       false,
                                       null,  //please check here if date is Blank
                                       "", 0);

                                if (intValue == 0)
                                {
                                    return false;
                                }

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
                }
                #endregion

                #region  TDS Account Posting..................

                if (dTdsAmnt > 0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)), 4, 0, "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                        true, 0, strDocType, 0, 0, Convert.ToInt64(intChlnNo), ApplicationFunction.GetIndianDateTime().Date, VchrNo, 0,
                        YearIdno, 0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;

                            #region TDS Account Posting .........

                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno, Convert.ToInt64(IAcntIdno), "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                            dTdsAmnt, Convert.ToByte(2), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno, IntTDSAcntIdno, "Challan. No: " + Convert.ToString(intChlnNo) + " Challan. Date: " + strDate + " Lorry: " + strLorryNo,
                                       Convert.ToDouble(dTdsAmnt), Convert.ToByte(1), Convert.ToByte(0), "", false,
                                       null,  //please check here if date is Blank
                                       "", 0);

                                if (intValue == 0)
                                {
                                    return false;
                                }

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
                }

                #endregion

                #region Diesel Account Posting ................
                if (dDiesel > 0)
                {
                    ChlnBookingDAL obj = new ChlnBookingDAL();
                    tblChlnBookHead CBH = obj.selectDieselIDno(intDocIdno);
                    if (CBH != null)
                    {
                        IntDieselAcc_Idno = Convert.ToInt64(CBH.DieselAcnt_IDno);
                        if (intValue > 0)
                        {
                            Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                            intValue = objclsAccountPosting.InsertInVchrHead(
                            Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)),
                            4,
                            0,
                            "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            Convert.ToInt64(intChlnNo),
                            ApplicationFunction.GetIndianDateTime().Date,
                            VchrNo,
                            0,
                            YearIdno,
                            0, intUserIdno);
                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                #region Desial Amount  Posting...
                                intValue = 0;
                                /*Insert In VchrDetl*/
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno,
                                Convert.ToInt64(IntDieselAcc_Idno),
                                "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                                dDiesel,
                                Convert.ToByte(1),
                                Convert.ToByte(0),
                                "",
                                true,
                                null,  //please check here if date is Blank
                                "", 0);
                                if (intValue > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                           intVchrIdno,
                                           Convert.ToInt64(IAcntIdno),
                                          "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo,
                                           Convert.ToDouble(dDiesel),
                                           Convert.ToByte(2),
                                           Convert.ToByte(0),
                                           "",
                                           false,
                                           null,  //please check here if date is Blank
                                           "", 0);

                                    if (intValue == 0)
                                    {
                                        return false;
                                    }

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

                }
                #endregion

                #endregion

                objclsAccountPosting = null;
                return true;
            }
            else
            {
                objclsAccountPosting = null;
                return true;
            }

        }

        private void PrintChallan(Int64 ChlnHeadIdno, string Grtype)
        {
            Repeater obj = new Repeater();
            Repeater obj1 = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; //string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
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
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            #region 'Second Print'
            lblCompanyname1.Text = CompName;
            lblCompAdd12.Text = Add1;
            lblCompAdd22.Text = Add2;
            lblCompCity12.Text = City;
            lblCompState12.Text = State;
            lblCompPhNo12.Text = PhNo;
            #endregion
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
                lblCompFaxNo1.Visible = false; lblFaxNo1.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
                lblCompFaxNo1.Text = FaxNo;
                lblCompFaxNo1.Visible = true; lblFaxNo1.Visible = true;
            }
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
                lblCompTIN1.Visible = false; lblTin1.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
                lblCompTIN1.Text = TinNo;
                lblCompTIN1.Visible = true; lblTin1.Visible = true;
            }

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintHead',@Id='" + ChlnHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrinthead";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblChlnno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_No"]);
                lblchlnDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");
                lblOwnr.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["ownername"]);
                lblTrckNo.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_No"]);
                lblDrvrName.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                valuelblAdvanceAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Adv_Amnt"]));
                valuelblcmmnsn.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Commsn_Amnt"]));
                lblDieselAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Diesel_Amnt"]));
                valueLblTdsAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["TDSTax_Amnt"]));
                valuelblnetTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Net_Amnt"]));
                lblRasstripalChrg.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["RassaTirpal_Chrg"]));
                lbltxtdelivery.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Delvry_Instrc"]);
                lblPetrolPump.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["PetrolPumpName"]);
                try
                {
                    lblGeneratedByName.Text = Convert.ToString((dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"] == "" || dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"] == null) ? "" : "Generated by: " + dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"]);
                    lblLastUpdatedByName.Text = Convert.ToString((dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"] == "" || dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"] == null) ? "" : "Last Updated by: " + dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"]);
                }
                catch (Exception ex)
                {
                    lblGeneratedByName.Text = "";
                    lblLastUpdatedByName.Text = "";
                }
                #region'Second Print'
                lblChlnno1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_No"]);
                lblchlnDate1.Text = Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");
                lblOwnr1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["ownername"]);
                lblTrckNo1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_No"]);
                lblDrvrName1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                valuelblAdvanceAmnt1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Adv_Amnt"]));
                valuelblcmmnsn1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Commsn_Amnt"]));
                lblDieselAmnt1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Diesel_Amnt"]));
                valueLblTdsAmnt1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["TDSTax_Amnt"]));
                valuelblnetTotal1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Net_Amnt"]));
                lblRasstripalChrg1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["RassaTirpal_Chrg"]));
                lbltxtdelivery1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Delvry_Instrc"]);
                lblPetrolPump1.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["PetrolPumpName"]);
                try
                {
                    lblGeneratedByName1.Text = Convert.ToString((dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"] == "" || dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"] == null) ? "" : "Generated by: " + dsReport.Tables["GRPrinthead"].Rows[0]["GeneratedBy"]);
                    lblLastUpdatedByName1.Text = Convert.ToString((dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"] == "" || dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"] == null) ? "" : "Last Updated by: " + dsReport.Tables["GRPrinthead"].Rows[0]["ModifiedBy"]);
                }
                catch (Exception ex)
                {
                    lblGeneratedByName1.Text = "";
                    lblLastUpdatedByName1.Text = "";
                }
                #endregion
            }
            DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintDetl',@Id='" + ChlnHeadIdno + "'");
            dsReportDetl.Tables[0].TableName = "GRPrintdetl";
            if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = dsReportDetl;
                Repeater1.DataBind();
                lblPrintHeadng.Text = "Challan";
                Repeater22.DataSource = dsReportDetl;
                Repeater22.DataBind();
                lblPrintHeadng1.Text = "Challan";
            }

        }
        private void PopupGrDirectly(Int64 GrIdno)
        {
            if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
            {
                lblDelvSerch.Text = "Truck No.";
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var lst = obj.selectTruckNo();
                obj = null;
                if (lst.Count > 0)
                {
                    ddldelvplace.DataSource = lst;
                    ddldelvplace.DataTextField = "Lorry_No";
                    ddldelvplace.DataValueField = "Lorry_Idno";
                    ddldelvplace.DataBind();
                    ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            else
            {
                lblDelvSerch.Text = "Delv. Place";
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var lst = obj.SelectCityCombo();
                obj = null;

                if (lst.Count > 0)
                {
                    ddldelvplace.DataSource = lst;
                    ddldelvplace.DataTextField = "City_Name";
                    ddldelvplace.DataValueField = "City_Idno";
                    ddldelvplace.DataBind();
                    ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            ChlnBookingDAL ObjChlnBookingDAL = new ChlnBookingDAL();
            string strSbillNo = String.Empty;
            DataTable dtRcptDetl = new DataTable(); DataRow Dr;
            dtRcptDetl = ObjChlnBookingDAL.SelectGrChallanDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(GrIdno));

            txtKms.Text = string.IsNullOrEmpty(Convert.ToString(dtRcptDetl.Rows[0]["Kms"])) ? "0" : Convert.ToString(dtRcptDetl.Rows[0]["Kms"]);
            ViewState["dt"] = dtRcptDetl;
            BindGrid();
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            //netamntcal();
            ddlDelvryPlace.Enabled = false;
            if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
            {
                ddlTruckNo.SelectedValue = ddldelvplace.SelectedValue;
                ddlTruckNo.Enabled = false;
                ddlTruckNo_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlDelvryPlace.SelectedValue = ddldelvplace.SelectedValue;
                ddlTruckNo.Enabled = true;
                ddlDelvryPlace.Enabled = false;
            }
            ChlnBookingDAL obj1 = new ChlnBookingDAL();
            BindDropdownDAL objDal = new BindDropdownDAL();
            ddlFromCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["From_City"]);
            this.ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
            var lststate = obj1.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
            ddlTruckNo.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Lorry_Idno"]);
            ddlTruckNo_SelectedIndexChanged(null, null);
            DataSet ds2 = objDal.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToString(txtDate.Text.Trim()));
            ViewState["isCalculateTDS"] = ds2.Tables[0].Rows[0]["Lorry_Type"];
            if (ViewState["isCalculateTDS"] != null)
            {
                if (ViewState["isCalculateTDS"].ToString() == "0")
                {
                    txtTdsAmnt.Text = "0.00"; txtDieselAmnt.Text = "0.00";
                }
                else
                {
                    calculateTDS(Convert.ToString(ds2.Tables[0].Rows[0]["Pan_No"]), dGrAmnt, Convert.ToInt32(lststate.State_Idno), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)));
                }
            }
            netamntcal();
        }
        private void BindDieselAccountLink()
        {
            AcntLinkDAL objAcntLinkDAL = new AcntLinkDAL();
            var lst = objAcntLinkDAL.BindGeneralandPetrolPump(11);
            //ddlAcntLink.DataSource = lst;
            //ddlAcntLink.DataTextField = "Acnt_Name";
            //ddlAcntLink.DataValueField = "Acnt_Idno";
            //ddlAcntLink.DataBind();
            //ddlAcntLink.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Account Name", "0"));

            ddlacntname.DataSource = lst;
            ddlacntname.DataTextField = "Acnt_Name";
            ddlacntname.DataValueField = "Acnt_Idno";
            ddlacntname.DataBind();
            ddlacntname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Account Name", "0"));

            var list = objAcntLinkDAL.SelectById(1);
            if (list != null)
                ddlacntname.SelectedValue = Convert.ToString(list.DieselAcc_Idno);

        }
        private void Binditemlk()
        {
            ChlnBookingDAL objitemDAL = new ChlnBookingDAL();
            var lst = objitemDAL.SelectItemName();
            ddlitemname.DataSource = lst;
            ddlitemname.DataTextField = "Item_Name";
            ddlitemname.DataValueField = "Item_Idno";
            ddlitemname.DataBind();
            ddlitemname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose item Name", "0"));
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
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddlTruckNo.SelectedIndex > 0))
                {
                    ChlnBookingDAL obj = new ChlnBookingDAL();
                    BindDropdownDAL objBind = new BindDropdownDAL();
                    DataSet ds2 = objBind.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToString(txtDate.Text.Trim()));
                    ViewState["isCalculateTDS"] = Convert.ToString(ds2.Tables[0].Rows[0]["Lorry_Type"]);
                    if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        //Convert.ToString(obj.selectPartyName(Convert.ToInt32(ds2.Tables[0].Rows[0]["Acnt_Idno"])));
                        hidePanNo.Value = string.IsNullOrEmpty(ds2.Tables[0].Rows[0]["Pan_No"].ToString()) ? "" : ds2.Tables[0].Rows[0]["Pan_No"].ToString();
                        txtOwnrNme.Text = Convert.ToString(Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Name"].ToString()) + '-' + (string.IsNullOrEmpty(ds2.Tables[0].Rows[0]["Pan_No"].ToString()) ? "" : ds2.Tables[0].Rows[0]["Pan_No"].ToString()) + "-" + ((Convert.ToInt32(ds2.Tables[0].Rows[0]["Lorry_Type"].ToString()) == 0) ? "O" : "H"));
                        #region Lorry owner Type
                        string LorryOwnerType = ((Convert.ToInt32(ds2.Tables[0].Rows[0]["Lorry_Type"].ToString()) == 0) ? "O" : "H");
                        ViewState["LorryOwnerType"] = LorryOwnerType;
                        if (LorryOwnerType == "H") { lblHireAmnt.Visible = true; txtHireAmnt.Visible = true; RequiredFieldValidatorHireAmnt.Enabled = true; }
                        else if (LorryOwnerType == "O") { lblHireAmnt.Visible = false; txtHireAmnt.Visible = false; RequiredFieldValidatorHireAmnt.Enabled = false; }
                        #endregion
                        hidOwnerId.Value = Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Idno"].ToString());
                        if (string.IsNullOrEmpty(hidOwnerId.Value))
                        {
                            lnkbtnPrtyBlance.Visible = false;
                        }
                        else
                        {
                            lblBalPrtyName.Text = Convert.ToString(Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Name"].ToString()) + '-' + (string.IsNullOrEmpty(ds2.Tables[0].Rows[0]["Pan_No"].ToString()) ? "" : ds2.Tables[0].Rows[0]["Pan_No"].ToString()) + "-" + ((Convert.ToInt32(ds2.Tables[0].Rows[0]["Lorry_Type"].ToString()) == 0) ? "O" : "H"));
                            this.CurBalLoad();
                            lnkbtnPrtyBlance.Visible = true;
                        }
                    }
                    Int32 Typ = 0;
                    Typ = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    ddldriverName.DataSource = null;
                    if (ddldriverName.Items.Count > 0)
                    {
                        ddldriverName.Items.Clear();
                    }

                    BindDriver(Typ);
                    ddldriverName.SelectedValue = Convert.ToString(ds2.Tables[0].Rows[0]["Driver_Idno"].ToString());
                    CalculateCommissionAmnt(Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToBoolean(ds2.Tables[0].Rows[0]["chk_ComsnCal"].ToString()));
                    var lststate = obj.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
                    hideStateId.Value = Convert.ToString(lststate.State_Idno);
                    if (lststate != null)
                    {
                        if (grdMain.Rows.Count > 0)
                        {
                            foreach (GridViewRow Dr in grdMain.Rows)
                            {
                                Label lblSubTotAmnt = (Label)Dr.FindControl("lblSubTotAmnt");
                                dGrAmnt += Convert.ToDouble(lblSubTotAmnt.Text);
                            }
                            // txtTdsAmnt.Text = Convert.ToDouble(((dGrAmnt * Convert.ToDouble(hidTdsTaxPer.Value)) / 100)).ToString("N2");
                            if (ViewState["isCalculateTDS"] != null)
                            {
                                if (ViewState["isCalculateTDS"].ToString() == "0")
                                {
                                    txtTdsAmnt.Text = "0.00";
                                }
                                else
                                {
                                    calculateTDS(Convert.ToString(ds2.Tables[0].Rows[0]["Pan_No"].ToString()), dGrAmnt, Convert.ToInt32(lststate.State_Idno), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)));
                                }
                            }
                        }
                        else
                        {
                            txtTdsAmnt.Text = "0.00";
                        }
                    }
                    netamntcal();
                }
                else if (ddlTruckNo.SelectedIndex >= 0)
                {
                    ChlnBookingDAL obj = new ChlnBookingDAL();
                    DataSet ds2 = obj.GetTranDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlTruckNo.SelectedValue));
                    txtOwnrNme.Text = Convert.ToString(Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Name"].ToString()) + '-' + (Convert.ToString(ds2.Tables[0].Rows[0]["TRAN_TYPE"].ToString())));
                    hidOwnerId.Value = Convert.ToString(ds2.Tables[0].Rows[0]["Party_idno"].ToString());

                }
                else
                {
                    txtOwnrNme.Text = "";
                }

                if (ViewState["isCalculateTDS"] != null)
                {
                    if (ViewState["isCalculateTDS"].ToString() == "0")
                    {
                        txtDieselAmnt.Text = "0.00";
                    }
                }
                ddlTruckNo.Focus();
            }
            catch (Exception Ex)
            {

            }
        }
        protected void txtAdvAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.BindRcpt();
                if (txtAdvAmnt.Text.Trim() == "")
                {
                    txtAdvAmnt.Text = "0.00";

                }
                txtAdvAmnt.Text = Convert.ToDouble(txtAdvAmnt.Text).ToString("N2");
                if (Convert.ToDouble(txtAdvAmnt.Text) > 0)
                {

                    // ddlRcptType.Enabled = true; rfvRcptType.Enabled = true;
                    ddlRcptType_SelectedIndexChanged(null, null);
                }
                else
                {
                    //  ddlRcptType.Enabled = false; rfvRcptType.Enabled = false;
                    ddlRcptType_SelectedIndexChanged(null, null);
                }

                //netamntcal(); 
                ddlRcptType.Focus();
            }
            catch (Exception Ex)
            {

            }

        }
        protected void txtcommission_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcommission.Text.Trim() == "")
                {
                    txtcommission.Text = "0.00";
                }
                else
                {
                    txtcommission.Text = Convert.ToDouble(txtcommission.Text).ToString("N2");
                    // calculateTDS(Convert.ToString(string.IsNullOrEmpty(Convert.ToString(hidePanNo.Value)) ? "0" : Convert.ToString(hidePanNo.Value)), Convert.ToDouble(string.IsNullOrEmpty(txtGrosstotal.Text) ? 0 : Convert.ToDouble(txtGrosstotal.Text)), Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(hideStateId.Value)) ? "0" : Convert.ToString(hideStateId.Value)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)));
                    //ddlTruckNo_SelectedIndexChanged(null, null);
                }
                // netamntcal();
            }
            catch (Exception Ex)
            {

            }
        }
        protected void txtDieselAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // netamntcal();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlFromCity.SelectedIndex > 0)
                {

                    ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                }
                imgSearch.Focus();
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlRcptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ChlnBookingDAL obj = new ChlnBookingDAL();
            //DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32(Request.Form[ddlRcptType.UniqueID]), ApplicationFunction.ConnectionString());
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
            //    if (intAcnttype == 4)
            //    {
            //        rfvinstno.Enabled = true;
            //        txtInstNo.Enabled = true; rfvinstno.Enabled = rfvinstDate.Enabled = true;
            //        txtInstDate.Enabled = true;
            //        ddlCusBank.Enabled = true; rfvCusBank.Enabled = true;
            //        ddlRcptType.Enabled = true;
            //        txtInstDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //    }
            //    else
            //    {
            //        rfvinstno.Enabled = false;
            //        txtInstNo.Enabled = false; rfvinstno.Enabled = rfvinstDate.Enabled = false;
            //        txtInstDate.Enabled = false;
            //        ddlCusBank.Enabled = false; rfvCusBank.Enabled = false;
            //        ddlRcptType.Enabled = true;
            //        ddlCusBank.SelectedIndex = 0;
            //        txtInstDate.Text = "";
            //    }
            //}

            //txtInstNo.Focus();
        }

        #endregion

        #region Grid Events....
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt = 0; double dQtty = 0; double dWieght = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dQtty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotQty = dQtty + dTotQty;
                dWieght = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Weght"));
                dTotWeight = dWieght + dTotWeight;
                dWithoutWagesAmnt = dWithoutWagesAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "WithoutUnloading_Amnt"));
                dWagesAmnt = dWagesAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Wages_Amnt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblTChallanAmnt.Text = dblNetAmnt.ToString("N2");
                txtGrosstotal.Text = ViewState["LorryOwnerType"] != null && Convert.ToString(ViewState["LorryOwnerType"]) == "H" ? ((txtHireAmnt.Text == "" && Convert.ToDouble(txtHireAmnt.Text) < 0) ? "0" : txtHireAmnt.Text.Trim()) : dblNetAmnt.ToString("N2");
                Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
                lblTotQty.Text = dTotQty.ToString("N2");
                Label lblTotWeigh = (Label)e.Row.FindControl("lblTotWeigh");
                lblTotWeigh.Text = dTotWeight.ToString();
                Label lblWithoutUnloadingAmnt = (Label)e.Row.FindControl("lblWithoutUnloadingAmnt");
                lblWithoutUnloadingAmnt.Text = dWithoutWagesAmnt.ToString("N2");
                Label lblWagesAmnt = (Label)e.Row.FindControl("lblWagesAmnt");
                lblWagesAmnt.Text = dWagesAmnt.ToString("N2");
                txtcommission.Enabled = true;
                if (Convert.ToDouble(txtGrosstotal.Text) <= 0)
                {
                    txtcommission.Enabled = false;

                }
                else
                {
                    txtcommission.Enabled = true;
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dfWithoutWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "WithoutUnloading_Amnt"));
                dfWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
                Label lbltotalWeight = (Label)e.Item.FindControl("lbltotalWeight");
                Label lbltotalqty = (Label)e.Item.FindControl("lbltotalqty");
                Label lblWagesAmnt = (Label)e.Item.FindControl("lblWagesAmnt");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                //lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();
                lblAmount.Text = dfWithoutWagesAmnt.ToString("N2");
                lblWagesAmnt.Text = dfWagesAmnt.ToString("N2");

            }
        }

        protected void Repeater22_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dfWithoutWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "WithoutUnloading_Amnt"));
                dfWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt1");
                Label lbltotalWeight = (Label)e.Item.FindControl("lbltotalWeight1");
                Label lbltotalqty = (Label)e.Item.FindControl("lbltotalqty1");
                Label lblWagesAmnt = (Label)e.Item.FindControl("lblWagesAmnt1");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount1");
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                //lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();
                lblAmount.Text = dfWithoutWagesAmnt.ToString("N2");
                lblWagesAmnt.Text = dfWagesAmnt.ToString("N2");

            }
        }
        #endregion

        #region Control Events...
        protected void txtTdsAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtTdsAmnt.Text.Trim() == "")
            {
                txtTdsAmnt.Text = "0.00";

            }
            // netamntcal();
        }

        protected void LnkTDSUpdt_Click(object sender, EventArgs e)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(clsMultipleDB.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction Tran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        Int64 ChlnHeadIdno = Convert.ToInt64(hidid.Value);
                        tblChlnBookHead objHead = (from H in db.tblChlnBookHeads where H.Chln_Idno == ChlnHeadIdno select H).FirstOrDefault();
                        if (objHead != null)
                        {
                            objHead.TDSTax_Amnt = Convert.ToDouble(txtTdsAmnt.Text);
                            objHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                            db.SaveChanges();
                            Tran.Commit();
                            ShowMessage("TDS Updated Successfully.");
                        }
                    }
                    catch (Exception Ex)
                    {
                        Tran.Rollback();
                    }
                }
                db.Connection.Close();
            }
        }
        #endregion

        #region Excel Upload........
        private void ExportExcelHeader(DataTable Dt)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GRExcelImportHeaderFormat.xls"));
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

        protected void lnkbtnExport_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlFromCity.SelectedValue) == 0)
            {
                ShowMessageErr("Please Select Location");
                return;
            }
            DateTime? dateFrom = null;
            ChlnBookingDAL obj = new ChlnBookingDAL();
            dateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
               "SrNo", "String",
               "GRNo", "String",
               "GRDate", "String",
               "FromCity", "String",
               "Amount", "String",
               "Driver", "String",
               "AdvanceAmount", "String",
               "PayType", "String",
               "Comm", "String"
               );
            var Grno = obj.FetchGRno(dateFrom, Convert.ToInt64(ddlFromCity.SelectedValue));
            for (int i = 0; i < Grno.Count; i++)
            {
                DataRow dr = dttemp.NewRow();
                dr["SrNo"] = Convert.ToString(i + 1);
                dr["GRNo"] = Convert.ToString(DataBinder.Eval(Grno[i], "GRNo"));
                dr["GRDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(Grno[i], "GRDate")).ToString("dd-MM-yyyy"));
                dr["FromCity"] = Convert.ToString(DataBinder.Eval(Grno[i], "FromCity"));
                dr["Amount"] = Convert.ToString(DataBinder.Eval(Grno[i], "Amount"));
                dttemp.Rows.Add(dr);
            }
            ExportExcelHeader(dttemp);
        }

        protected void lnkbtnExcelUpload_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popayye", "CloseExcelDiv();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close", "CloseExcelDiv();", true);
        }

        protected void lnkbtnUpload_Click(object sender, EventArgs e)
        {

            string msg = string.Empty;
            if (FileUpload.HasFile)
            {
                ChlnBookingDAL obj = new ChlnBookingDAL();
                string excelfilename = string.Empty;

                #region UPLOAD EXCEL AT SERVER
                excelfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "ItemsexcelChln", "Itemsexcel");
                #endregion

                if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                {
                    DataTable dt = new DataTable();
                    DataTable dtnew = new DataTable();
                    BindDropdownDAL objDal = new BindDropdownDAL();
                    string filepath = Server.MapPath("~/ItemsexcelChln/" + excelfilename);
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
                        OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "] WHERE [GRNo] IS NOT NULL OR [GRDate] IS NOT NULL OR [FromCity] IS NOT NULL OR [Amount] IS NOT NULL OR [Driver] IS NOT NULL OR [AdvanceAmount] IS NOT NULL OR [PayType] IS NOT NULL OR [Comm] IS NOT NULL", con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        #endregion

                        dt = ApplicationFunction.CreateTable("tblChlnUploadFromExcel", "SrNo", "String", "GRNo", "String", "GRDate", "String", "FromCity", "String", "Amount", "String", "Driver", "String", "AdvanceAmount", "String", "PayType", "String", "Comm", "String");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns[0].Caption == "SrNo" && ds.Tables[0].Columns[1].Caption == "GRNo" && ds.Tables[0].Columns[2].Caption == "GRDate" && ds.Tables[0].Columns[3].Caption == "FromCity" && ds.Tables[0].Columns[4].Caption == "Amount" && ds.Tables[0].Columns[5].Caption == "Driver" && ds.Tables[0].Columns[6].Caption == "AdvanceAmount" && ds.Tables[0].Columns[7].Caption == "PayType" && ds.Tables[0].Columns[8].Caption == "Comm")
                            {

                                #region Validate EXCEL Before Uploading
                                DateTime? dateFrom = null;
                                dateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                                var Grno = obj.FetchGRno(dateFrom, Convert.ToInt64(ddlFromCity.SelectedValue));
                                DataTable dttemp1 = ApplicationFunction.CreateTable("tbl",
                                   "SrNo", "String",
                                   "GRNo", "String",
                                   "GRDate", "String",
                                   "FromCity", "String",
                                   "Amount", "String"
                                   );
                                for (int i = 0; i < Grno.Count; i++)
                                {
                                    DataRow dr = dttemp1.NewRow();
                                    dr["SrNo"] = Convert.ToString(i + 1);
                                    dr["GRNo"] = Convert.ToString(DataBinder.Eval(Grno[i], "GRNo"));
                                    dr["GRDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(Grno[i], "GRDate")).ToString("dd-MM-yyyy"));
                                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(Grno[i], "FromCity"));
                                    dr["Amount"] = Convert.ToString(DataBinder.Eval(Grno[i], "Amount"));
                                    dttemp1.Rows.Add(dr);
                                }
                                if (dttemp1.Rows.Count == ds.Tables[0].Rows.Count)
                                {
                                    for (int i = 0; i <= dttemp1.Rows.Count - 1; i++)
                                    {
                                        for (int j = 1; j <= dttemp1.Columns.Count - 1; j++)
                                        {
                                            if (j == 2)
                                            {
                                                if (Convert.ToString(dttemp1.Rows[i][j]) == Convert.ToString(Convert.ToDateTime(ds.Tables[0].Rows[i][j]).ToString("dd-MM-yyyy")))
                                                {
                                                    continue;
                                                }
                                            }
                                            else
                                                if (Convert.ToString(dttemp1.Rows[i][j]) == Convert.ToString(ds.Tables[0].Rows[i][j]))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                msg = "Incorrect Data. Please use exported Excel Sheet.";
                                                ShowMessageErr(msg);
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    msg = "Incorrect Data. Please use exported Excel Sheet.";
                                    ShowMessageErr(msg);
                                    return;
                                }
                                #endregion

                                #region  Truncate Table First
                                int resultget = obj.TurncatetblChlnUploadFromExcel(ApplicationFunction.ConnectionString());
                                #endregion

                                #region INSERT RECORD IN tblChlnUploadFromExcel TABLE
                                Int64 intResult = 0;
                                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        intResult = obj.InsertInChlnExcel(
                                            Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["GRNo"]) == "" ? "0" : ds.Tables[0].Rows[i]["GRNo"]),
                                                Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["GRDate"].ToString() == "" ? "" : ds.Tables[0].Rows[i]["GRDate"])),
                                                Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["FromCity"]) == "" ? "" : ds.Tables[0].Rows[i]["FromCity"]),
                                                Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]) == "" ? "" : ds.Tables[0].Rows[i]["Amount"]),
                                                Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Driver"]) == "" ? "" : ds.Tables[0].Rows[i]["Driver"]),
                                                Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["AdvanceAmount"]) == "" ? "" : ds.Tables[0].Rows[i]["AdvanceAmount"]),
                                                Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["PayType"]) == "" ? "" : ds.Tables[0].Rows[i]["PayType"]),
                                                Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["Comm"]) == "" ? "0" : ds.Tables[0].Rows[i]["Comm"]),
                                                Convert.ToInt64(ddldateRange.SelectedValue == "" ? "0" : ddldateRange.SelectedValue), base.CompId, base.UserIdno);
                                    }
                                    if (intResult > 0)
                                    { Tran.Complete(); }
                                    else { Tran.Dispose(); }
                                }
                                #endregion

                                var GrDet = obj.SelectData();
                                for (int i = 0; i < GrDet.Count; i++)
                                {
                                    var GrDet1 = obj.SelectGRData((Convert.ToInt64(DataBinder.Eval(GrDet[i], "GRNo"))), (Convert.ToInt64(DataBinder.Eval(GrDet[i], "FromCity"))));

                                    this.ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                                    tblChlnBookHead objtblChlnBookHead = new tblChlnBookHead();

                                    #region  Fill Controls

                                    ddlTruckNo.SelectedValue = DataBinder.Eval(GrDet1[i], "LorryId").ToString();
                                    txtAdvAmnt.Text = DataBinder.Eval(GrDet[i], "AdvAmnt").ToString();
                                    txtGrosstotal.Text = DataBinder.Eval(GrDet1[i], "GrossAmnt").ToString();
                                    txtcommission.Text = DataBinder.Eval(GrDet[i], "Comm").ToString();

                                    if (Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value)) > 1)
                                    {
                                        DataSet ds2 = objDal.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", Convert.ToInt32(ddlTruckNo.SelectedValue), Convert.ToString(txtDate.Text.Trim()));
                                        ViewState["isCalculateTDS"] = Convert.ToString(ds2.Tables[0].Rows[0]["Lorry_Type"]);
                                        if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                                        {
                                            hidePanNo.Value = Convert.ToString(string.IsNullOrEmpty(ds2.Tables[0].Rows[0]["Pan_No"].ToString()) ? "" : ds2.Tables[0].Rows[0]["Pan_No"].ToString());
                                            txtOwnrNme.Text = Convert.ToString(Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Name"]) + '-' + (string.IsNullOrEmpty(ds2.Tables[0].Rows[0]["Pan_No"].ToString()) ? "" : ds2.Tables[0].Rows[0]["Pan_No"].ToString() + "-" + ((Convert.ToInt32(ds2.Tables[0].Rows[0]["Lorry_Type"].ToString()) == 0) ? "O" : "H")));
                                            hidOwnerId.Value = Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Idno"]);
                                        }

                                    }

                                    #endregion

                                    #region  Fill Obj for insertion in ChlnHead

                                    objtblChlnBookHead.Chln_No = txtchallanNo.Text;//
                                    objtblChlnBookHead.Chln_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));//
                                    objtblChlnBookHead.BaseCity_Idno = Convert.ToInt32((ddlFromCity.SelectedIndex <= 0) ? "0" : ddlFromCity.SelectedValue);//
                                    objtblChlnBookHead.DelvryPlc_Idno = Convert.ToInt32((ddlDelvryPlace.SelectedIndex <= 0) ? "0" : ddlDelvryPlace.SelectedValue);//
                                    objtblChlnBookHead.Truck_Idno = Convert.ToInt32(Convert.ToInt32(DataBinder.Eval(GrDet1[i], "LorryId")) <= 0 ? "0" : DataBinder.Eval(GrDet1[i], "LorryId"));//
                                    objtblChlnBookHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);//

                                    Int32 Typ = 0; var lst1 = (IList<AcntMast>)null; var lst2 = (IList<DriverMast>)null; int DriverId = 0;
                                    Typ = obj.selectTruckType(Convert.ToInt32(DataBinder.Eval(GrDet1[i], "LorryId")));
                                    BindDriver(Typ);
                                    if (Typ == 0)
                                    {
                                        lst1 = obj.selectOwnerDriverName();
                                        if (lst1.Count > 0)
                                        {
                                            for (int a = 0; a < lst1.Count; a++)
                                            {
                                                if (Convert.ToString(DataBinder.Eval(lst1[a], "Acnt_Name")) == Convert.ToString(DataBinder.Eval(GrDet[i], "Driver")))
                                                {
                                                    DriverId = Convert.ToInt32(DataBinder.Eval(lst1[a], "Acnt_Idno"));
                                                    ddldriverName.SelectedValue = DriverId.ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = "Driver Name does not exist for GR No: '" + (Convert.ToInt64(DataBinder.Eval(GrDet[i], "GRNo"))) + "'";
                                            ShowMessageErr(msg);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        lst2 = obj.selectHireDriverName();
                                        if (lst2.Count > 0)
                                        {
                                            for (int b = 0; b < lst2.Count; b++)
                                            {
                                                if (Convert.ToString(DataBinder.Eval(lst2[b], "Driver_name")) == Convert.ToString(DataBinder.Eval(GrDet[i], "Driver")))
                                                {
                                                    DriverId = Convert.ToInt32(DataBinder.Eval(lst2[b], "Driver_Idno"));
                                                    ddldriverName.SelectedValue = DriverId.ToString();
                                                }
                                            }
                                        }
                                    }

                                    objtblChlnBookHead.Driver_Idno = Convert.ToInt32(DriverId);//Excel
                                    objtblChlnBookHead.Delvry_Instrc = txtDelvInstruction.Text.Trim().Replace("'", "");//

                                    objtblChlnBookHead.Inv_Idno = 0;//
                                    objtblChlnBookHead.Gross_Amnt = Convert.ToDouble(Convert.ToInt32(DataBinder.Eval(GrDet1[i], "GrossAmnt")) <= 0 ? "0" : DataBinder.Eval(GrDet1[i], "GrossAmnt"));//
                                    objtblChlnBookHead.Commsn_Amnt = Convert.ToDouble(DataBinder.Eval(GrDet[i], "Comm"));//Excel

                                    dGrAmnt = Convert.ToDouble(Convert.ToInt32(DataBinder.Eval(GrDet1[i], "GrossAmnt")) <= 0 ? "0" : DataBinder.Eval(GrDet1[i], "GrossAmnt"));
                                    var lststate = obj.GetStateIdno(Convert.ToInt32(ddlFromCity.SelectedValue));
                                    BindDropdownDAL obj1 = new BindDropdownDAL();
                                    //DataSet ds1 = obj1.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", Convert.ToInt32(DataBinder.Eval(GrDet1[i], "LorryId")), Convert.ToString(txtDate.Text.Trim()));
                                    var lst = obj.selectOwnerName(Convert.ToInt32(DataBinder.Eval(GrDet1[i], "LorryId")));
                                    ViewState["CalTDS"] = lst.Lorry_Type;

                                    if (ViewState["CalTDS"] != null)
                                    {
                                        if (ViewState["CalTDS"].ToString() == "0")
                                        {
                                            txtTdsAmnt.Text = "0.00";
                                        }
                                        else
                                        {
                                            calculateTDS(Convert.ToString(hidePanNo.Value), dGrAmnt, Convert.ToInt32(lststate.State_Idno), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)));
                                        }
                                    }
                                    txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(DataBinder.Eval(GrDet1[i], "GrossAmnt"))) - (Convert.ToDouble(DataBinder.Eval(GrDet[i], "Comm")) + Convert.ToDouble(DataBinder.Eval(GrDet[i], "AdvAmnt")) + Convert.ToDouble(txtTdsAmnt.Text))).ToString("N2");
                                    objtblChlnBookHead.TDSTax_Amnt = Convert.ToDouble(txtTdsAmnt.Text);//Cal


                                    objtblChlnBookHead.Chln_type = 1;//
                                    objtblChlnBookHead.Other_Amnt = 0;//
                                    objtblChlnBookHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);//Cal
                                    objtblChlnBookHead.Work_type = Convert.ToInt32(string.IsNullOrEmpty(hidWorkType.Value) ? 0 : Convert.ToInt32(hidWorkType.Value));//
                                    objtblChlnBookHead.Adv_Amnt = Convert.ToDouble(DataBinder.Eval(GrDet[i], "AdvAmnt"));//Excel
                                    int Rcpt_Id = 0;
                                    Rcpt_Id = obj.RcptTypeId(Convert.ToString(DataBinder.Eval(GrDet[i], "RcptType")));

                                    if (Rcpt_Id <= 0)
                                    {
                                        msg = "Receipt Name for Advance Amnt does not exist for GR No: '" + (Convert.ToInt64(DataBinder.Eval(GrDet[i], "GRNo"))) + "'";
                                        ShowMessageErr(msg);
                                        return;
                                    }
                                    ddlRcptType.SelectedValue = Rcpt_Id.ToString();
                                    objtblChlnBookHead.RcptType_Idno = Convert.ToInt32((Rcpt_Id <= 0) ? "0" : Rcpt_Id.ToString());//Excel

                                    objtblChlnBookHead.Bank_Idno = Convert.ToInt32((ddlCusBank.SelectedIndex < 0) ? "0" : ddlCusBank.SelectedValue);//
                                    objtblChlnBookHead.Inst_No = Convert.ToInt32(((txtInstNo.Text == "") ? "0" : txtInstNo.Text));//
                                    objtblChlnBookHead.UserIdno = Convert.ToInt64(Session["UserIdno"]);//

                                    #endregion
                                    //---------------------------------------------------
                                    DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                                    dtRcptDetl = obj.SelectGrChallanDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(DataBinder.Eval(GrDet1[i], "GRIdno")));
                                    Int64 value = 0;
                                    AcntLinkDS = obj.DtAcntDS(ApplicationFunction.ConnectionString());
                                    DsHire = obj.DsHireAcnt(ApplicationFunction.ConnectionString());
                                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                                    {
                                        value = obj.Insert(objtblChlnBookHead, dtRcptDetl, Convert.ToInt32(ddlDelvryPlace.SelectedValue), Convert.ToString(ddlgrtyp.SelectedValue), DtTempFuel);
                                        if (value > 0)
                                        {
                                            if (this.PostIntoAccounts(Convert.ToDouble(DataBinder.Eval(GrDet[i], "AdvAmnt")), value, "CB", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue)) == true)
                                            {
                                                obj.UpdateInChlnExcelPost((Convert.ToInt64(DataBinder.Eval(GrDet[i], "GRNo"))), (Convert.ToString(DataBinder.Eval(GrDet[i], "CityName"))));
                                                Clear();
                                                tScope.Complete();
                                            }
                                            else
                                            {
                                                tScope.Dispose();
                                            }

                                        }
                                        else
                                        {
                                            tScope.Dispose();
                                        }

                                    }
                                }
                                msg = "Excel uploaded successfully";
                                ShowMessage(msg);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "CloseUpload();", true);
                                #region Export Final EXCEL
                                DataTable dttemp_new = ApplicationFunction.CreateTable("tbl",
                                   "SrNo", "String",
                                   "GRNo", "String",
                                   "GRDate", "String",
                                   "FromCity", "String",
                                   "Amount", "String",
                                   "Driver", "String",
                                   "AdvAmnt", "String",
                                   "PayType", "String",
                                   "Comm", "String",
                                   "IsSaved", "String"
                                   );
                                var excelData = obj.SelectExcelFinalData();
                                for (int i = 0; i < excelData.Count; i++)
                                {
                                    DataRow dr = dttemp_new.NewRow();
                                    dr["SrNo"] = Convert.ToString(i + 1);
                                    dr["GRNo"] = Convert.ToString(DataBinder.Eval(excelData[i], "GRNo"));
                                    dr["GRDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(excelData[i], "GRDate")).ToString("dd-MM-yyyy"));
                                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(excelData[i], "FromCity"));
                                    dr["Amount"] = Convert.ToString(DataBinder.Eval(excelData[i], "Amount"));
                                    dr["Driver"] = Convert.ToString(DataBinder.Eval(excelData[i], "Driver"));
                                    dr["AdvAmnt"] = Convert.ToString(DataBinder.Eval(excelData[i], "AdvAmnt"));
                                    dr["Comm"] = Convert.ToString(DataBinder.Eval(excelData[i], "Comm"));
                                    dr["PayType"] = Convert.ToString(DataBinder.Eval(excelData[i], "RcptType"));
                                    dr["IsSaved"] = Convert.ToString(DataBinder.Eval(excelData[i], "IsSaved"));
                                    dttemp_new.Rows.Add(dr);
                                }
                                ExportExcelHeader(dttemp_new);
                                #endregion


                            }
                            else
                            {
                                msg = "Excel is blank or Excel is not in correct format.";
                                ShowMessageErr(msg);
                                return;
                            }
                        }
                    }


                }
                else
                {
                    msg = "Please Upload Correct Excel File";
                    ShowMessageErr(msg);
                    return;
                }
            }
            else
            {
                msg = "Please Upload Excel File";
                ShowMessageErr(msg);
                return;
            }
        }

        #endregion

        #region Control..
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

        protected void lnkbtnprintOM_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(hidid.Value) != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
                //string url = "PrintChallanOM.aspx" + "?q=" + Convert.ToInt64(hidid.Value) + "&P=" + Convert.ToString(HidGrType.Value);
                //string fullURL = "window.open('" + url + "', '_blank' );";
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
        }
        protected void lnkBtnPrint_Click(object sender, EventArgs e)
        {
            #region Print By Salman..

            //string Value = Request.QueryString["q"];

            ////string[] strMails = Request.QueryString["q"].Split('-');
            //string[] Array = Value.Split(new char[] { '-' });

            //string ID = Array[0].ToString();
            //string Type = Array[1].ToString();

            //string url = "PrintChallanOM.aspx" + "?q=" + (ID) + "&P=" + Convert.ToString(Type) + "&S=" + ddlPage.SelectedValue;
            //string fullURL = "window.open('" + url + "', '_blank' );";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            #endregion
            // AAMIR
            hidPages.Value = ddlPage.SelectedValue;
            PrintChallanOM(Convert.ToInt64(hidid.Value), Convert.ToString(HidGrType.Value));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrintOM('printOm')", true);
            //string url = "PrintChallanOM.aspx" + "?q=" + Convert.ToInt64(hidid.Value) + "&P=" + Convert.ToString(HidGrType.Value);
            //string fullURL = "window.open('" + url + "', '_blank' );";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);            
        }
        double totaltds = 0;

        #region Commented by salman....
        private void PrintChallanOM(Int64 ChlnHeadIdno, string Grtype)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, dRateamnt = 0, dweight = 0, dRatePRKM = 0, dTotalKM = 0;
            double dFreight = 0, dLessAdv = 0, dTDSAmnt = 0, dFinalTot = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string strcompdesc = "";//string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
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
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            strcompdesc = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            //lblCompanyname.Text = CompName;

            lblCompAdd3.Text = Add1;
            lblCompAdd4.Text = Add2;
            lblCompCity1.Text = City;
            lblCompState1.Text = State;
            lblCompPhNo.Text = PhNo;
            lblCompPhNo1.Text = PhNo;
            if (FaxNo == "")
            {

                lblFaxNo2.Visible = false;
            }
            else
            {
                lblFaxNo2.Text = "FAX No.:" + FaxNo;
                lblFaxNo2.Visible = true;
            }
            if (strcompdesc == "")
            {
                lblCompDesc.Visible = false;
            }
            else
            {
                lblCompDesc.Text = Convert.ToString(strcompdesc.Trim().Replace("@", "<br/>") + "<br/>");
            }


            #endregion

            DataSet dsReport = new DataSet();
            if (Grtype.ToUpper() == "GRR")
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintHeadOM',@Id='" + ChlnHeadIdno + "'");
            }
            else
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintHead',@Id='" + ChlnHeadIdno + "'");
            }

            //DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintHeadOM',@Id='" + ChlnHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrinthead";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblChlnnoO.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_No"]);
                if (Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["MANUALNO"]) != "")
                {
                    lblMno.Text = "/" + Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["MANUALNO"]);
                }
                else
                {
                    lblMno.Text = "";
                }
                dRatePRKM = Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["RateKM"]);
                dTotalKM = Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Total_KM"]);
                dTDSAmnt = Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["TDSTax_Amnt"]);
                dFreight = Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Freight"]);
                dLessAdv = Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Adv_Amnt"]);
                if (dRatePRKM > 0)
                {
                    dRateamnt = dRatePRKM * dTotalKM;
                    lblRateAmnt.Text = dRateamnt.ToString("N2");
                    lblfreightamount.Text = "0.00";
                    dFinalTot = dRateamnt - dTDSAmnt;
                    lbltotalfreight.Text = (dRateamnt - dTDSAmnt).ToString("N2");
                }
                else
                {
                    dFinalTot = dFreight - dTDSAmnt;
                    lbltotalfreight.Text = (dFreight - dTDSAmnt).ToString("N2");
                    lblRateAmnt.Text = "0.00";
                }
                lblchlnDateo.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");
                lblTrckNoO.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_No"]);
                lblIssueBranch.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["From_City"]);
                lbltoBranch.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["To_City"]);

                //Lorry Details
                lblOwnrO.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["ownername"]);
                lblowneraddrss.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Owner_Address"]);
                lblBrokerAddress.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Broker_Address"]);
                lbltxtownmobile.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["OwnerMobile"]);
                lblOwnerMobileNo.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Cont_Mobile"]);
                lbltxtpan.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["OwnerPan"]);
                lblchasisno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chassis_no"]);
                lbltxtengineno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Eng_No"]);
                //lbltxtpermit.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0][""]);
                //lbltxtpermitvalid.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"]).ToString("dd-MM-yyyy");
                lbltxtmodel.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_Make"]);
                lblBrokerName.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_Owner"]);

                //
                //Driver Details

                lbldrivername.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                lbldriverAddress.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Driver_Address"]);
                lbltxtdrvlicenceno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLicnc_NO"]);
                lbltxtvalidupto.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"]).ToString("dd-MM-yyyy");
                lblmobtextdriver.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverMobile"]);
                lblStartKM.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Start_Km"]);
                lblCloseKm.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Close_Km"]);
                lblLateDnD.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Late_D&D"]);
                hdnType.Value = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["TypeID"]);
                if (hdnType.Value == "2")
                    lbltotalweight.Text = "Fixed";

                lblDetention.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Detention"]);
                lblhamali.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Hamali"]);
                lblTotalKM.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Total_KM"]);
                lblfreightamount.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Freight"]);
                lblDlyDate.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DelvDate"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["DelvDate"]).ToString("dd-MM-yyyy");
                //lbltotalfreight.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Freight"]);
                lblRatePKm.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["RateKM"]);
                lblgrdate.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");


                // lbltxtinsured.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                //lbltxtpolicyno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                //lbltxtinsvalidupto.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);



                valuelblAdvanceAmntO.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Adv_Amnt"]));
                // valuelblcmmnsn.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Commsn_Amnt"]));
                //lblDieselAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Diesel_Amnt"]));
                valueLblTdsAmntO.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["TDSTax_Amnt"]));
                valuelblnetTotalO.Text = (dFinalTot - dLessAdv).ToString("N2");
                totaltds = Convert.ToDouble(valueLblTdsAmntO.Text);
                // lbltxtdelivery.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Delvry_Instrc"]);


            }
            DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintDetlOM',@Id='" + ChlnHeadIdno + "'");
            dsReportDetl.Tables[0].TableName = "GRPrintdetl";
            if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0)
            {

                Repeater2.DataSource = dsReportDetl;
                Repeater2.DataBind();
                lblPrintHeadng.Text = "FREIGHT CUM TRANSIT CHALLAN";
            }

        }
        #region Repeatercomment
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Total_Price"));
                if (hdnType.Value == "1")
                {
                    dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                }
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dfWithoutWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "WithoutUnloading_Amnt"));
                dfWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                grdate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "GRRet_Date"))) ? "" : Convert.ToDateTime(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "GRRet_Date"))).ToString("dd-MM-yyyy");
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
                Label lbltotalWeight = (Label)e.Item.FindControl("lbltotalWeight");
                Label lbltotalqty = (Label)e.Item.FindControl("lbltotalqty");
                Label lblWagesAmnt = (Label)e.Item.FindControl("lblWagesAmnt");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                if (hdnType.Value == "1")
                {
                    lbltotalWeight.Text = dtotwght.ToString("N2");
                }
                else
                {
                    lbltotalWeight.Text = "Fixed";
                }
                lbltotalqty.Text = dqtnty.ToString();
                lblAmount.Text = dfWithoutWagesAmnt.ToString("N2");
                lblWagesAmnt.Text = dfWagesAmnt.ToString("N2");
                //

                //lblgrdate.Text = grdate;
                //lblfreightamount.Text = dfWithoutWagesAmnt.ToString("N2");
                if (hdnType.Value == "1")
                {
                    lbltotalweight.Text = dtotwght.ToString("N2");
                }
                else
                {
                    lbltotalWeight.Text = "Fixed";
                }
                //lblhamali.Text = dfWagesAmnt.ToString("N2");
                //lbltotalfreight.Text = ((dfWithoutWagesAmnt) - (totaltds)).ToString("N2");
            }
        }
        #endregion
        #endregion

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
        public void Loadimage()
        {
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            tblUserPref obj1 = objInvoiceDAL.SelectUserPref();
            if (Convert.ToBoolean(obj1.Logo_Req))
            {
                if (obj1.Logo_Image != null)
                {
                    // imgjkcement.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                    // imgjkcement.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                //imgjkcement.Visible = false;
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] GetTruckNo(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();
            List<string> TruckNumber = new List<string>();
            DataTable dtNames = new DataTable();
            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
            DataSet dt = objChlnBookingDAL.SelectTruckList(prefixText, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Tables[0].Rows[i]["Lorry_No"]), Convert.ToString(dt.Tables[0].Rows[i]["Lorry_Idno"]));
                    TruckNumber.Add(item);
                }
                return TruckNumber.ToArray();
            }
            else
            {
                return null;
            }
        }

        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                string Value = Request.QueryString["q"];
                string[] Array = Value.Split(new char[] { '-' });
                string ID = Array[0].ToString();
                string Type = Array[1].ToString();
                FillChlnBookingPrint(ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrintf('printf')", true);
            }
        }
        private void FillChlnBookingPrint(String GRNo)
        {
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = "";
            string GSTIN = "";
            string StateCode;
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast A Left JOIN tblCITYMASTER CM On CM.city_idno=A.city_idno Left join tblStateMaster SM ON SM.state_idno=A.state_idno");
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]);
            StateCode = Convert.ToString(CompDetl.Tables[0].Rows[0]["GSTState_Code"]);
            PhNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Mobile_1"]);
            lblCompName.Text = CompName;
            lblAdd1.Text = Add1;
            lblcity.Text = City;
            lblstate.Text = State;
            bllgstcd.Text = StateCode;
            lblmobile.Text = PhNo;
            DataSet dsChlnReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectReportChlnPrint',@Id='" + GRNo + "'");
            if (dsChlnReport != null && dsChlnReport.Tables[0].Rows.Count > 0)
            {
                lblchallan.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["Chln_No"]);
                lbldate.Text = string.Format("{0:MMMM dd, yy}", Convert.ToDateTime(dsChlnReport.Tables[0].Rows[0]["Chln_Date"]));
                lblTruckno.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["lorry_No"]);
                lbltruckowner.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["DriverName_Eng"]);
                lblcomssion.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["Commsn_Amnt"]);
                lblcash.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["Adv_Amnt"]);
                lbltotaldue.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["Net_Amnt"]);
                Lbldesel.Text = Convert.ToString(dsChlnReport.Tables[0].Rows[0]["Diesel_Amnt"]);
                DataTable dtreport = (DataTable)ViewState["dt"];
                if (dtreport != null && dtreport.Rows.Count > 0)
                {
                    object sumObject;
                    sumObject = dtreport.Compute("Sum(Amount)", string.Empty);
                    lbltotal.Text = sumObject.ToString();
                    lblwt.Text = Convert.ToString(dtreport.Rows[i]["Tot_Weght"]);
                    lblvillage.Text = Convert.ToString(dtreport.Rows[i]["To_City"]);
                    lbltalika.Text = Convert.ToString(dtreport.Rows[i]["Delvplc_Name"]);
                    lbldist.Text = Convert.ToString(dtreport.Rows[i]["CityVia_Name"]);
                }
                DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintDetl',@Id='" + GRNo + "'");
                if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0)
                {
                    lblrate.Text = Convert.ToString(dsReportDetl.Tables[0].Rows[0]["Item_Rate"]);
                }
            }
        }
        #region Fuel Details..
        protected void clearFuel()
        {
            ddlacntname.SelectedIndex = 0;
            ddlitemname.SelectedIndex = 0;
            hidrowidno.Value = "";
            txtQTY.Text = "0";
            txtrate.Text = "0";
            txtamount.Text = "0";

        }
        protected void clearfuelgrid()
        {
            ViewState["dtfuel"] = DtTempFuel = null;
            grdmainFuel.DataSource = DtTempFuel;
            grdmainFuel.DataBind();
        }

        protected void imgfuelpopup_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openfuelDetail();", true);
        }
        protected void lnkbtnsubmt_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            ChlnBookingDAL obj = new ChlnBookingDAL();
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp == null)
            {
                msg = "Enter Challan Details";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                return;
            }
            String ChallanNo = txtchallanNo.Text;
            Int64 chlnidno = obj.selectchlnidno(ChallanNo);
            if (hidrowidno.Value != string.Empty)
            {
                DtTempFuel = (DataTable)ViewState["dtfuel"];
                foreach (DataRow dtrow in DtTempFuel.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowidno.Value))
                    {
                        dtrow["acnt_idno"] = ddlacntname.SelectedValue;
                        dtrow["acnt_name"] = ddlacntname.SelectedItem;
                        dtrow["itemidno"] = ddlitemname.SelectedValue;
                        dtrow["itemname"] = ddlitemname.SelectedItem;
                        dtrow["Chln_Idno"] = ChallanNo;
                        dtrow["Qty"] = txtQTY.Text.Trim();
                        dtrow["Rate"] = txtrate.Text.Trim();
                        dtrow["Amt"] = txtamount.Text.Trim();
                    }
                }
            }
            else
            {
                DtTempFuel = (DataTable)ViewState["dtfuel"];
                if ((DtTempFuel != null) && (DtTempFuel.Rows.Count > 0))
                {
                    foreach (DataRow row in DtTempFuel.Rows)
                    {
                        //if (Convert.ToInt64(row["acnt_idno"]) == Convert.ToInt64(ddlacntname.SelectedValue))
                        //{
                        //    msg = "Account Name Already Selected!";
                        //    ddlacntname.Focus();
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                        //    return;
                        //}
                    }
                }
                else
                { DtTempFuel = CreateFuel(); }
                Int32 ROWCount = Convert.ToInt32(DtTempFuel.Rows.Count) - 1;
                int id = DtTempFuel.Rows.Count == 0 ? 1 : (Convert.ToInt32(DtTempFuel.Rows[ROWCount]["id"])) + 1;
                txtQTY.Text = string.IsNullOrEmpty(Convert.ToString(txtQTY.Text)) ? "0" : Convert.ToString(txtQTY.Text);
                Double qty = Convert.ToDouble(txtQTY.Text);
                txtrate.Text = string.IsNullOrEmpty(Convert.ToString(txtrate.Text)) ? "0" : Convert.ToString(txtrate.Text);
                Double rate = Convert.ToDouble(txtrate.Text);
                Double amt = (qty * rate);
                if (amt > 0)
                    txtamount.Text = Convert.ToString(amt);
                else
                    txtamount.Text = string.IsNullOrEmpty(Convert.ToString(txtamount.Text)) ? "0" : Convert.ToString(txtamount.Text);
                ApplicationFunction.DatatableAddRow(DtTempFuel,
                    id,
                    ddlacntname.SelectedItem,
                    ddlacntname.SelectedValue,
                    ddlitemname.SelectedItem,
                    ddlitemname.SelectedValue,
                    ChallanNo,
                    txtQTY.Text,
                    txtrate.Text,
                    txtamount.Text
                   );
                ViewState["dtfuel"] = DtTempFuel;
            }
            this.BindGridFuel();
            netamntcal();
            clearFuel();
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Toll", "openfuelDetail();", true);
        }
        private void BindGridFuel()
        {
            grdmainFuel.DataSource = (DataTable)ViewState["dtfuel"];
            grdmainFuel.DataBind();
        }
        private DataTable CreateFuel()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Id", "String",
                "acnt_name", "String",
                "acnt_idno", "String",
                "itemname", "String",
                "itemidno", "String",
                "Chln_Idno", "String",
                "Qty", "String",
                "Rate", "String",
                "Amt", "String"
                );

            return dttemp;
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            bool IsUpdate = false;
            List<ChlnFuelExpense> li = new List<ChlnFuelExpense>();
            DtTempFuel = CreateFuel();
            if (grdmainFuel.Rows.Count > 0)
            {

                foreach (GridViewRow row in grdmainFuel.Rows)
                {

                    Label lblqty = (Label)row.FindControl("lblqty");
                    Label lblrate = (Label)row.FindControl("lblrate");
                    Label lblamnt = (Label)row.FindControl("lblamnt");
                    Label lblitemname = (Label)row.FindControl("lblitemname");
                    Label lblacntname = (Label)row.FindControl("lblacntname");

                    HiddenField hidacntId = (HiddenField)row.FindControl("hidacntId");
                    HiddenField hidChlnIdno = (HiddenField)row.FindControl("hidChlnIdno");
                    HiddenField hidItemIdno = (HiddenField)row.FindControl("hidItemIdno");
                    //HiddenField hidfuelId = (HiddenField)row.FindControl("hidfuelId");

                    ApplicationFunction.DatatableAddRow(DtTempFuel, 0, lblacntname.Text, hidacntId.Value, lblitemname.Text, hidItemIdno.Value, hidChlnIdno.Value, lblqty.Text, lblrate.Text, lblamnt.Text);

                }
                IsUpdate = obj.InsertfuelDetails(DtTempFuel);
                if (IsUpdate == true)
                {

                    ShowMessage(" record Save successfully");

                }
                else
                {
                    ShowMessageErr("record Not Save ");
                }
                obj = null;
                this.clearFuel();
                this.clearfuelgrid();

            }

        }

        protected void grdmainFuel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTempFuel = (DataTable)ViewState["dtfuel"];

            if (e.CommandName == "cmdeditfuel")
            {
                DtTempFuel = (DataTable)ViewState["dtfuel"];
                DataRow[] drs = DtTempFuel.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {
                    hidrowidno.Value = Convert.ToString(drs[0]["Id"]);
                    ddlacntname.SelectedValue = Convert.ToString(drs[0]["acnt_idno"]);
                    ddlitemname.SelectedValue = Convert.ToString(drs[0]["itemidno"]);
                    txtQTY.Text = Convert.ToString(drs[0]["Qty"]);
                    txtrate.Text = Convert.ToString(drs[0]["Rate"]);
                    txtamount.Text = Convert.ToString(drs[0]["Amt"]);
                }
            }
            else if (e.CommandName == "cmddeletefuel")
            {
                DataTable objDataTable = CreateFuel();
                foreach (DataRow rw in DtTempFuel.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["acnt_name"], rw["acnt_idno"], rw["itemname"],
                                                                rw["itemidno"], rw["Chln_Idno"], rw["Qty"], rw["Rate"], rw["Amt"]);
                    }
                }
                ViewState["dtfuel"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridFuel();
            }

        }
        protected void grdmainFuel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tolamnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblamt = (Label)e.Row.FindControl("lblamt");
                lblamt.Text = tolamnt.ToString("N2");
                txtDieselAmnt.Text = lblamt.Text;
            }

        }
        protected void txtrate_TextChanged(object sender, EventArgs e)
        {
            txtQTY.Text = string.IsNullOrEmpty(Convert.ToString(txtQTY.Text)) ? "0" : Convert.ToString(txtQTY.Text);
            Double qty = Convert.ToDouble(txtQTY.Text);
            txtrate.Text = string.IsNullOrEmpty(Convert.ToString(txtrate.Text)) ? "0" : Convert.ToString(txtrate.Text);
            Double rate = Convert.ToDouble(txtrate.Text);
            Double amt = (qty * rate);
            txtamount.Text = Convert.ToString(amt);

        }

        #endregion
    }
}

