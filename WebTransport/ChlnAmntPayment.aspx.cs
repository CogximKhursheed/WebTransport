using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Configuration;
using System.Transactions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.ApplicationBlocks.Data;
namespace WebTransport
{
    public partial class ChlnAmntPayment : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; DataTable dtPartName = new DataTable(); DataTable DtGrdetail = new DataTable();
        double dblNetAmnt = 0; Int32 iFromCity = 0, intYearIdno = 0; double dTotCurBal, dTotRecvdAmnt, dblAdvAmt, dblTdsAmnt, dblDisAmt , dblCommision= 0;
        private int intFormId = 34; double dtotWt = 0, dtotwt = 0, dAmnt = 0, dTDS = 0, dShor = 0, dGross = 0, dAdv = 0, dBal = 0, dPay = 0;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.lnkbtnPrintClick);
            //con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
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
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.userpref(); this.BindDateRange();
                Int32 intYearIdno = Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue));
                //  ddlFromCity.SelectedValue = Convert.ToString(iFromCity);

                this.BindReceiptType();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                this.BindMaxNo(Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)));
                this.BindCityANDLocation();
                this.BindPartName();
                this.BindCustBank();

                challanAmntpaymentDAL objChlnPayDAL = new challanAmntpaymentDAL();
                tblUserPref obj = objChlnPayDAL.selectuserpref();
                hidPrintType.Value = Convert.ToString(obj.PayChln_Print);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                lnkimgbtnSearch.Enabled = true;
                ddlRcptType_SelectedIndexChanged(null, null);
                if (Request.QueryString["q"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnNew.Visible = true;

                    Int64 Idno = objChlnPayDAL.getcompid(ApplicationFunction.ConnectionString());
                    if (Idno == 1)
                    {
                        print1.Visible = true;
                    }
                    if ((string.IsNullOrEmpty(hidPrintType.Value) ? 0 : Convert.ToInt32(hidPrintType.Value)) == 1)
                    {
                        lnkbtnPrintClick.Visible = true;
                        LinkButton1.Visible = false;
                    }
                    else
                    {
                        lnkbtnPrintClick.Visible = false;
                        LinkButton1.Visible = true;
                        print1.Visible = false;

                    }
                }
                else
                {
                    lnkbtnPrintClick.Visible = false;
                    lnkbtnNew.Visible = false;
                    LinkButton1.Visible = false;
                    print1.Visible = false;

                }
                if (Request.QueryString["ChlnIdno"] != null)
                {
                    DirectPaymentFromGr(Convert.ToInt64(Request.QueryString["ChlnIdno"]));
                }
                ddldateRange.Focus();
            }
        }
        #endregion

        #region Button Evnets...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {

            //DtTemp = (DataTable)ViewState["dt"];
            //BindGrid();
            //if (DtTemp != null && DtTemp.Rows.Count <= 0)
            //{
            //    if (DtTemp.Rows.Count <= 0)
            //    {
            //        ShowMessage("Please enter details");
            //        return;
            //    }
            //}

            if (Convert.ToDouble(txtNetAmnt.Text) <= 0)
            {
                ShowMessageErr("Net Amount Must be Greater Than Zero.");
                grdMain.Focus();
                return;
            }
            if (grdMain.Rows.Count > 0)
            {
                DtTemp = CreateDt();

                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                    Label lblGrno = (Label)row.FindControl("lblGrno");
                    Label lblGrDate = (Label)row.FindControl("lblGrDate");
                    HiddenField hidRecivrIdno = (HiddenField)row.FindControl("hidRecivrIdno");
                    Label lblGrFrom = (Label)row.FindControl("lblGrFrom");
                    HiddenField hidToCityIdno = (HiddenField)row.FindControl("hidToCityIdno");
                    HiddenField hidFromCityIdno = (HiddenField)row.FindControl("hidFromCityIdno");
                    HiddenField hidTruck_Idno = (HiddenField)row.FindControl("hidTruck_Idno");
                    HiddenField hidDriver_Idno = (HiddenField)row.FindControl("hidDriver_Idno");
                    Label lblAdvAmt = (Label)row.FindControl("lblAdvAmt");
                    Label lblDisAmount = (Label)row.FindControl("lblDisAmount");
                    Label lblTdsAmount = (Label)row.FindControl("lblTdsAmount");
                    Label lblComisionAmount = (Label)row.FindControl("lblComisionAmount");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblTotRecvd = (Label)row.FindControl("lblTotRecvd");
                    Label lblCurBal = (Label)row.FindControl("lblCurBal");
                    TextBox txtRcvdAmnt = (TextBox)row.FindControl("txtRcvdAmnt");
                   //("tbl", "Chln_Idno", "String", "Chln_No", "String", "Chln_Date", "String", "Fromcity_Idno", "String", "Truck_Idno", "String", "Driver_Idno", "String",
                    // "Challan_Amnt", "String", "Recvd_Amnt", "String");
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, lblGrno.Text, (ApplicationFunction.mmddyyyy(lblGrDate.Text)), hidFromCityIdno.Value, hidTruck_Idno.Value, hidDriver_Idno.Value,lblAmount.Text, txtRcvdAmnt.Text, lblAdvAmt.Text, lblDisAmount.Text, lblTdsAmount.Text, lblComisionAmount.Text);

                }
                challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
                tblChlnAmntPayment_Head objRGH = new tblChlnAmntPayment_Head();
                objRGH.Rcpt_No = Convert.ToInt64(txtRcptNo.Text);
                objRGH.BaseCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                objRGH.Rcpt_date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text));
                objRGH.Party_IdNo = Convert.ToInt64(ddlPartyName.SelectedValue);
                objRGH.Inst_No = Convert.ToInt64(Convert.ToString(txtInstNo.Text) == "" ? 0 : Convert.ToInt64(txtInstNo.Text));
                objRGH.Date_Added = System.DateTime.Now;
                objRGH.Date_Modified = null;
                objRGH.Bank_Idno = Convert.ToInt32(ddlCustmerBank.SelectedValue);
                objRGH.RcptType_Idno = Convert.ToInt32(ddlRcptTyp.SelectedValue);
                objRGH.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                if (txtInstDate.Text == "")
                {
                    objRGH.Inst_Dt = null;
                }
                else
                {
                    objRGH.Inst_Dt = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
                }

                objRGH.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                objRGH.Remark = Convert.ToString(TxtRemark.Text);
                objRGH.Year_IdNo = Convert.ToInt32(ddldateRange.SelectedValue);
                objRGH.Loc_Idno = Convert.ToInt32(ddlLocation.SelectedValue);
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        value = obj.Insert(objRGH, DtTemp);
                    }
                    else
                    {

                        value = obj.Update(objRGH, Convert.ToInt32(hidid.Value), DtTemp);
                    }
                    if (value > 0)
                    {
                        if (this.PostIntoAccounts(Convert.ToDouble(txtNetAmnt.Text), value, "ACB", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue)) == true)
                        {
                            obj.UpdateIsPosting(value);
                            if ((string.IsNullOrEmpty(hidid.Value) == false))
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                {
                                    ShowMessage("Record Update successfully");
                                    tScope.Complete(); tScope.Dispose();
                                    this.Clear();
                                    ddlFromCity_SelectedIndexChanged(null, null);
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Pay No. Already Exist!");
                                }
                                else if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    ShowMessageErr("Record  Not Update!");
                                }
                                tScope.Dispose();
                            }
                            else
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                {
                                    ShowMessage("Record Saved successfully");
                                    tScope.Complete(); tScope.Dispose();
                                    this.Clear();
                                    ddlFromCity_SelectedIndexChanged(null, null);
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Pay No. Already Exist!");
                                }
                                else if ((string.IsNullOrEmpty(hidid.Value) == true))
                                {
                                    ShowMessageErr("Record  Not Saved successfully!");
                                }
                                tScope.Dispose();
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                            return;
                        }

                    }
                }



            }
            else
            {
                ShowMessageErr("Please Enter Detais");
                return;
            }

        }

        private DataTable CreateDt()//Chln_Idno,Chln_No,Chln_Date,Fromcity_Idno,Truck_Idno,Driver_Idno,Challan_Amnt,Recvd_Amnt
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Chln_Idno", "String", "Chln_No", "String", "Chln_Date", "String", "Fromcity_Idno", "String", "Truck_Idno", "String", "Driver_Idno", "String",
                                                                   "Challan_Amnt", "String", "Recvd_Amnt", "String" , "Adv_Amnt", "String" , "Diesel_Amnt", "String" , "TDSTax_Amnt","String", "Commsn_Amnt", "String");
            return dttemp;
        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
                Int64 iPRTYIDNO = (ddlPartyName.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue));
                Int64 iFromcity;
                if (ddlFromCity.SelectedIndex <= 0)
                    iFromcity = 0;
                else
                    iFromcity = (ddlFromCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                Int32 iChallanno = Convert.ToInt32((txtchlnNoSearch.Text.Trim()) == "" ? 0 : Convert.ToInt32(txtchlnNoSearch.Text.Trim()));
                string strGrFrm = ""; string action = "";
                strGrFrm = "BK";
                if (iChallanno > 0)
                {
                    action = "SelectChallanPaymentDetailWithChallanNo";

                }
                else
                {
                    action = "SelectChallanPaymentDetailWithoutChallanNo";
                }
                DataTable DsGrdetail = obj.SelectChallanPaymentDetail(action, Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iPRTYIDNO, iChallanno, ApplicationFunction.ConnectionString(), iFromcity);
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    ViewState["DtGrdetail"] = DsGrdetail;
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnOk.Visible = true;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnOk.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);

            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (hidid.Value != null && hidid.Value != "")
            {
                Populate(Convert.ToInt64(hidid.Value));
            }
            else
            {
                Clear();
            }
        }

        private void GetReceiptNo()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            Int64 max = obj.GetMaxNo();
            obj = null;
            txtRcptNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ChlnAmntPayment.aspx");
        }

        protected void lnkimgbtnSearch_OnClick(object sender, EventArgs e)
        {
            //if (ddlFromCity.SelectedIndex <= 0)
            //{
            //    ShowMessageErr("Please select From City.");
            //}
            //else 
            if (ddlPartyName.SelectedIndex <= 0)
            {
                ShowMessageErr("Please select Party Name.");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                txtDateFrom.Focus();
            }
        }

        protected void lnkbtnOk_OnClick(object sender, EventArgs e)
        {
            try
            {
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
                        ShowMessageErr("Please check atleast one record.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                    }
                    else
                    {
                        challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
                        string strSbillNo = String.Empty;

                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = obj.SelectChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "CloseModal();", true);
                    }
                    ddlRcptTyp.Focus();
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);

                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }

        }

        #endregion

        #region Functions...
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
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        public void userpref()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            tblUserPref userpref = obj.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
        }


        private void BindMaxNo(Int32 FromCityIdno, Int32 YearId)
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            Int64 MaxNo = obj.MaxNo(YearId, FromCityIdno, ApplicationFunction.ConnectionString());
            txtRcptNo.Text = Convert.ToString(MaxNo);
        }

        private void NetAmntCal()
        {
            double dblAmnt = 0;
            foreach (GridViewRow dr in grdMain.Rows)
            {
                TextBox txtRcvdAmnt = (TextBox)dr.FindControl("txtRcvdAmnt");
                dblAmnt += Convert.ToDouble(txtRcvdAmnt.Text);
            }

            txtNetAmnt.Text = txtAmount.Text = dblAmnt.ToString("N2");
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

        private void BindPartName()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            dtPartName = obj.BindPartyOwner(ApplicationFunction.ConnectionString());
            ddlPartyName.DataSource = dtPartName;
            ddlPartyName.DataTextField = "ACNT_NAME";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindReceiptType()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            var RcptType = obj.BindRcptType(ApplicationFunction.ConnectionString());
            ddlRcptTyp.DataSource = RcptType;
            ddlRcptTyp.DataTextField = "ACNT_NAME";
            ddlRcptTyp.DataValueField = "Acnt_Idno";
            ddlRcptTyp.DataBind();
            ddlRcptTyp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCustBank()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            ddlCustmerBank.DataSource = obj.BindBank();
            ddlCustmerBank.DataTextField = "Acnt_Name";
            ddlCustmerBank.DataValueField = "Acnt_Idno";
            ddlCustmerBank.DataBind();
            ddlCustmerBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCityANDLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
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

                    txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);

                }
            }
        }

        private void Populate(Int64 HeadId)
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            tblChlnAmntPayment_Head AmntGrhead = obj.selectHead(HeadId);
            ddlLocation.SelectedValue = Convert.ToString(AmntGrhead.Loc_Idno);
            ddldateRange.SelectedValue = Convert.ToString(AmntGrhead.Year_IdNo);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtRcptNo.Text = Convert.ToString(AmntGrhead.Rcpt_No);
            txtRcptNo.Visible = true; ddlFromCity.SelectedValue = Convert.ToString(AmntGrhead.BaseCity_Idno);
            txtGRDate.Text = Convert.ToDateTime(AmntGrhead.Rcpt_date).ToString("dd-MM-yyyy");
            ddlPartyName.SelectedValue = Convert.ToString(AmntGrhead.Party_IdNo);
            ddlRcptTyp.SelectedValue = Convert.ToString(AmntGrhead.RcptType_Idno);
            ddlRcptType_SelectedIndexChanged(null, null);
            ddlCustmerBank.SelectedValue = Convert.ToString(AmntGrhead.Bank_Idno);
            txtInstNo.Text = Convert.ToString(AmntGrhead.Inst_No);
            txtInstDate.Text = (Convert.ToString(AmntGrhead.Inst_Dt) == "") ? "" : Convert.ToDateTime(AmntGrhead.Inst_Dt).ToString("dd-MM-yyyy");
            TxtRemark.Text = Convert.ToString(AmntGrhead.Remark);
            txtNetAmnt.Text = Convert.ToDouble(AmntGrhead.Net_Amnt).ToString("N2");
            DtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId);

            ViewState["dt"] = DtTemp;
            this.BindGrid();
            lnkimgbtnSearch.Enabled = false;


            //For Daulat Print
            string strChlnIdno = string.Empty;
            for (int j = 0; j <= DtTemp.Rows.Count - 1; j++)
            {
                strChlnIdno = strChlnIdno + DtTemp.Rows[j]["Chln_Idno"].ToString() + ",";
            }
            if (strChlnIdno != "")
            {
                strChlnIdno = strChlnIdno.Substring(0, strChlnIdno.Length - 1);
            }

            //Int64 ChlnIdno = obj.GetChlnIdno(HeadId);
            if (Convert.ToString(hidPrintType.Value) != "")
            {
                if (Convert.ToInt32(hidPrintType.Value) == 2)
                {
                    if (strChlnIdno != "")
                    {
                        PrintChlnDetails(strChlnIdno);
                    }

                }
            }
            obj = null;
            Printf(HeadId);

        }

        private void Printf(Int64 HeadId)
        {
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0;
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnAmntPayment] @ACTION='PRINTVCHRSLIP',@Id='" + HeadId + "'");
            dsReport.Tables[0].TableName = "Printf";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblcash.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Net_Amnt"]));
                lbldiesel.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Diesel_Amnt"]));
                lblcomssion.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Commsn_Amnt"]));
                lbltotal.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Gross_Amnt"]));
                lblTruckno.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Lorry_No"]);
                lbltruckowner.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Lorry_Owner"]);
                lbldate.Text = string.Format("{0:MMMM dd, yy}", Convert.ToDateTime(dsReport.Tables["Printf"].Rows[0]["Vchr_Date"]));
                lblvoucher.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Vchr_No"]);
                lblvillage.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Village"]);
                lbltalika.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["TALUKA"]);
                lbldist.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["DISTRICT"]);
                lblrate.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["RATE"]);
                lblwt.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["WEIGHT"]);
                dcmsn = Convert.ToDouble(lblcash.Text);
                dblty = Convert.ToDouble(lbldiesel.Text);
                dcrtge = Convert.ToDouble(lblcomssion.Text);
                dsuchge = Convert.ToDouble(lbltotal.Text);
                lbltotaldue.Text = string.Format("{0:0,0.00}", (dsuchge - (dcmsn + dblty + dcrtge)));
            }
        }

        private void Print1(Int64 HeadId)
        {
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0;
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnAmntPayment] @ACTION='PRINTVCHRSLIP',@Id='" + HeadId + "'");
            dsReport.Tables[0].TableName = "Printf";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblcash.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Net_Amnt"]));
                lbldiesel.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Diesel_Amnt"]));
                lblcomssion.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Commsn_Amnt"]));
                lbltotal.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Gross_Amnt"]));
                lblTruckno.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Lorry_No"]);
                lbltruckowner.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Lorry_Owner"]);
                lbldate.Text = string.Format("{0:MMMM dd, yy}", Convert.ToDateTime(dsReport.Tables["Printf"].Rows[0]["Vchr_Date"]));
                lblvoucher.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Vchr_No"]);
                lblvillage.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["Village"]);
                lbltalika.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["TALUKA"]);
                lbldist.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["DISTRICT"]);
                lblrate.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["RATE"]);
                lblwt.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["WEIGHT"]);
                dcmsn = Convert.ToDouble(lblcash.Text);
                dblty = Convert.ToDouble(lbldiesel.Text);
                dcrtge = Convert.ToDouble(lblcomssion.Text);
                dsuchge = Convert.ToDouble(lbltotal.Text);
                lbltotaldue.Text = string.Format("{0:0,0.00}", (dsuchge - (dcmsn + dblty + dcrtge)));
            }
        }

        private void Clear()
        {
            hidid.Value = string.Empty;
            lnkbtnPrintClick.Visible = false;
            lnkbtnNew.Visible = false;

            ViewState["dt"] = null;
            DtTemp = null;
            ddlPartyName.SelectedIndex = 0;
            TxtRemark.Text = "";
            hidid.Value = string.Empty;
            /// txtGRDate.Text = "";
            //txtRcptNo.Text = "";
            // ddldateRange.SelectedIndex = 0; ;
            // ddldateRange_SelectedIndexChanged(null, null);
            BindGrid();
            txtNetAmnt.Text = "0.00";
            ddldateRange.Enabled = true;
            ddldateRange.SelectedIndex = 0;
            lnkimgbtnSearch.Enabled = true;
            txtInstDate.Text = "";
            txtInstNo.Text = "";
            ddlRcptTyp.SelectedIndex = 0;
            ddlCustmerBank.SelectedIndex = 0; ddlRcptTyp.SelectedIndex = ddlCustmerBank.SelectedIndex = 0; txtInstNo.Text = ""; ddlLocation.SelectedIndex = 0;
        }

        private void BindGrid()
        {
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp != null && DtTemp.Rows.Count > 0) { txtAmount.Enabled = true; } else { txtAmount.Enabled = false; }
            grdMain.DataSource = DtTemp;
            grdMain.DataBind();
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

        }

        private bool PostIntoAccounts(double Amount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno)
        {
            #region Variables Declaration...

            Int64 intVchrIdno = 0;
            Int64 intValue = 0;
            hidpostingmsg.Value = string.Empty;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            double dblNetAmnt = 0;
            double dblDiscAmnt = 0;
            if ((string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) == false) && (Convert.ToDouble(txtNetAmnt.Text.Trim()) > 0))
            {
                if (Convert.ToDouble(txtNetAmnt.Text) > 0)
                {
                    dblNetAmnt = Convert.ToDouble(txtNetAmnt.Text.Trim());
                }

            }
            DateTime? dtPBillDate = null;
            DateTime? dtBankDate = null;
            if (string.IsNullOrEmpty(txtInstDate.Text) == false)
            {
                dtBankDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
            }
            #endregion

            #region Account link Validations...

            //clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            //   tblAcntLink objAcntLink = objclsAccountPosting.GetAccountLinkData();
            //    if (objAcntLink == null)
            //    {
            //       hidpostingmsg.Value = " Account link is not defined. Kindly define.";
            //    }
            //    else
            //    {

            //    }


            #endregion
            ChlnBookingDAL obj = new ChlnBookingDAL();
            Int32 IAcntIdno = 0;
            // Int32 ILType = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue)); //Convert.ToInt32(clsDataAccessFunction.ExecuteScaler("select Lorry_type from LorryMast where Lorry_Idno=" + Convert.ToInt32(cmbTruckNo.SelectedValue) + "", Tran, Program.DataConn));
            //if (ILType == 1)
            //{
            //IAcntIdno = Convert.ToInt32(ddlPartyName.SelectedValue);
            //}
            //else
            //{
            //    IAcntIdno = Convert.ToInt32((string.IsNullOrEmpty((hidOwnerId.Value)) ? "0" : hidOwnerId.Value));
            //}
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
                IAcntIdno = Convert.ToInt32(ddlPartyName.SelectedValue);
                Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(2, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                //strSQL = "Exec spVoucherEntry @Action='INSERTVHEAD', @VCHR_NO='" + ((iVchrNo == 0) ? 1 : iVchrNo) + "', @VCHR_DATE='" + dtpDate.Text.Trim() + "',
                //@VCHR_TYPE=" + 1 + ", @VCHR_MODE=" + ((cmbRcptType.SelectedIndex == -1) ? 0 : cmbRcptType.SelectedValue) + ",
                //@VCHR_NARR='" + "Receipt No : " + Convert.ToString(txtRcptNo.Text.Trim()) + "  And Total Amount received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "',
                //@VCHR_HIDN='0',@YEAR_IDNO=" + Program.iFinYrID + ",@VCHR_SUSP='0',@VCHR_FRM='ACB',@ACNT_IDNO=" + cmbSenderName.SelectedValue + ",@PRINTED=0,@SBILL_NO='0',@SBILL_DATE='', @DCN_NO='',@RCPT_NO='',
                //@RCPT_DATE='" + dtpDate.Text.Trim() + "'";

                //    InsertInVchrHead(DateTime VchrDate, byte VchrType, Int64 VchrMode, string VchrNarr, bool VchrHidn, byte VchrSusp, string
                //VchrFrm, Int64 AcntIdno, Int16 Printed, Int64 SbillNo, DateTime? SbillDate, Int64 VchrNo, double DcnNo, int YearIdno, int CompIdno, int UserIdno

                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())),
                1,
                Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue)),
                "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtGRDate.Text.Trim() + "Total Amount received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "  ",
                true,
                0,
                strDocType,
                IAcntIdno,
                0,
                0,
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())),
                ((VchrNo == 0) ? 1 : VchrNo),
                0,
               YearIdno,
               0, 0);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;
                    intValue = 0; IAcntIdno = Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue));
                    //strSQL = "Exec spVoucherEntry @Action='INSERTVDETL',@VCHR_IDNO=" + iVchrId + ", @ACNT_IDNO=" + ((cmbRcptType.SelectedIndex == -1) ? 0 : cmbRcptType.SelectedValue) + ",
                    //@NARR_TEXT='" + "Receipt No : " + Convert.ToString(txtRcptNo.Text.Trim()) + "  And Total Amount Received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "', @ACNT_AMNT=" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + ", 
                    //@AMNT_TYPE=1, @INST_TYPE= 0 , @INST_NO='" + txtInstNo.Text.Trim() + "',@DETL_HIDN='1',@BANK_DATE='" + ((dtInstDate.Text == "  /  /") ? "" : dtInstDate.Text.Trim()) + "',@Cust_Bank=' " + ((cmbCustBank.SelectedIndex == -1) ? 0 : cmbCustBank.SelectedValue) + "',@RType_Idno='0'";
                    //          InsertInVchrDetl(Int64 VchrIdno, Int64 AcntIdno, string NarrText, double AcntAmnt, byte AmntType, byte InstType, string InstNo, bool DetlHidn,
                    //DateTime? BankDate, string CustBank, int CompIdno)
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue)),
                   "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtGRDate.Text.Trim() + "Total Amount received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "  ",
                    Amount,
                    Convert.ToByte(1),
                    Convert.ToByte(0),
                    Convert.ToString(txtInstNo.Text),
                    true,
                  dtBankDate,
                    Convert.ToString(ddlCustmerBank.SelectedValue), 0);
                    // strSQL = "Exec spVoucherEntry @Action='INSERTVDETL',@VCHR_IDNO=" + iVchrId + ", @ACNT_IDNO=" + ((cmbSenderName.SelectedIndex == -1) ? 0 : cmbSenderName.SelectedValue) + ", @NARR_TEXT='" + "Receipt No : " + Convert.ToString(txtRcptNo.Text.Trim()) + "  And Total Amount Received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "', 
                    //@ACNT_AMNT=" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + ", @AMNT_TYPE=2, @INST_TYPE=0, @INST_NO='" + txtInstNo.Text.Trim() + "',@DETL_HIDN='0',@BANK_DATE='" + ((dtInstDate.Text == "  /  /") ? "" : dtInstDate.Text.Trim()) + "',@Cust_Bank=' " + ((cmbCustBank.SelectedIndex == -1) ? 0 : cmbCustBank.SelectedValue) + "',@RType_Idno='2' ";
                    //          InsertInVchrDetl(Int64 VchrIdno, Int64 AcntIdno, string NarrText, double AcntAmnt, byte AmntType, byte InstType, string InstNo, bool DetlHidn,
                    //DateTime? BankDate, string CustBank, int CompIdno)
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt32(ddlPartyName.SelectedValue),
                     "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtGRDate.Text.Trim() + "Total Amount received :" + Convert.ToDouble(txtNetAmnt.Text.Trim().Replace("'", "")) + "  ",
                        Amount,
                        Convert.ToByte(2),
                        Convert.ToByte(0),
                         Convert.ToString(txtInstNo.Text),
                         false,
                          dtBankDate,
                        Convert.ToString(ddlCustmerBank.SelectedValue), 0);
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

            #region Deallocate variables...

            objclsAccountPosting = null;
            return true;

            #endregion

        }
        #endregion

        #region Control Events...

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
                BindMaxNo(Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)));
                txtGRDate.Focus();
            }

        }

        protected void ddlRcptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInstNo.Text = ""; rfvinstDate.Enabled = false; rfvinstno.Enabled = false; //rfvCusBank.Enabled = false; // lokesh, its remove b/c its not required
            txtInstNo.Enabled = false;
            txtInstDate.Enabled = false; ddlCustmerBank.Enabled = false;
            if (ddlRcptTyp.SelectedIndex > 0)
            {
                challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
                DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32((ddlRcptTyp.SelectedValue) == "" ? "0" : ddlRcptTyp.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                    if (intAcnttype == 4)
                    {

                        txtInstNo.Enabled = true;
                        txtInstDate.Enabled = true;
                        ddlCustmerBank.Enabled = true;
                        rfvinstDate.Enabled = true; rfvinstno.Enabled = true; //rfvCusBank.Enabled = true;
                    }
                }
            }


        }

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlPartyName.SelectedIndex <= 0)
            {
                lnkimgbtnSearch.Enabled = false;
            }
            else
            {
                lnkimgbtnSearch.Enabled = true;
            }
            lnkimgbtnSearch.Focus();
        }

        protected void txt_txtRcvdAmnt(object sender, EventArgs e)
        {
            TextBox txtRcvdAmnt = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtRcvdAmnt.Parent.Parent;
            TextBox txtRcvdAmnt1 = (TextBox)currentRow.FindControl("txtRcvdAmnt");
            Label lblCurBal = (Label)currentRow.FindControl("lblCurBal");
            if (txtRcvdAmnt1.Text == "")
            {
                txtRcvdAmnt1.Text = "0.00";
            }
            else
            {
                txtRcvdAmnt1.Text = Convert.ToDouble(txtRcvdAmnt.Text).ToString("N2");
                if (Convert.ToDouble(txtRcvdAmnt.Text) > Convert.ToDouble(lblCurBal.Text))
                {
                    txtRcvdAmnt.Text = "0.00";
                    string msg = "";
                    msg = "Recvd Amount Can't be Greater Than Cur. Balance";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('Recvd Amount Can't be Greater Than Cur. Balance')", true);
                    // Response.Write("ShowDiv("Showalert()");


                }
                NetAmntCal();
            }
        }

        protected void ddlRcptTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInstNo.Text = ""; ddlCustmerBank.SelectedIndex = 0; txtInstDate.Text = "";
            rfvinstDate.Enabled = rfvinstno.Enabled = false;
            txtInstNo.Enabled = false; //rfvCusBank.Enabled = false;
            txtInstDate.Enabled = false;
            ddlCustmerBank.Enabled = false; //rfvCusBank.Enabled = false;
            ChlnBookingDAL obj = new ChlnBookingDAL();
            DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32(ddlRcptTyp.SelectedValue), ApplicationFunction.ConnectionString());
            if (dt != null && dt.Rows.Count > 0)
            {
                Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                if (intAcnttype == 4)
                {
                    rfvinstno.Enabled = true;
                    txtInstNo.Enabled = true; rfvinstno.Enabled = rfvinstDate.Enabled = true;
                    txtInstDate.Enabled = true;
                    ddlCustmerBank.Enabled = true; //rfvCusBank.Enabled = true;
                }
            }
            ddlRcptTyp.Focus();
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            this.BindMaxNo(Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)));
            ddlPartyName.SelectedIndex = ddlRcptTyp.SelectedIndex = ddlCustmerBank.SelectedIndex = ddlLocation.SelectedIndex = 0; txtInstNo.Text = txtInstDate.Text = TxtRemark.Text = ""; txtAmount.Text = txtNetAmnt.Text = "0.00";
            ddlFromCity.Focus();

        }

        protected void ddlPartyName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlPartyName.Focus();

        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double damnt = 0;
                if (txtAmount.Text == "")
                {
                    txtAmount.Text = "0.00";
                }
                if (grdMain.Rows.Count > 0)
                {
                    damnt = Convert.ToDouble(txtAmount.Text.Trim());
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        Label lblCurBal = (Label)row.FindControl("lblCurBal");
                        TextBox txtRcvdAmnt = (TextBox)row.FindControl("txtRcvdAmnt"); ;
                        if (Convert.ToDouble(lblCurBal.Text) < damnt)
                        {
                            txtRcvdAmnt.Text = Convert.ToDouble(lblCurBal.Text).ToString("N2");
                            damnt = damnt - Convert.ToDouble(lblCurBal.Text);
                        }
                        else
                        {
                            txtRcvdAmnt.Text = (damnt).ToString("N2");
                            damnt = 0;
                        }

                    }
                    if (Convert.ToDouble(txtAmount.Text) > 0)
                    {
                        NetAmntCal();
                        //txtAmount.Text = txtNetAmnt.Text;
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt =  0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtRcvdAmnt = (TextBox)e.Row.FindControl("txtRcvdAmnt");
                txtRcvdAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dblAdvAmt = dblAdvAmt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Adv_Amnt"));
                dblDisAmt = dblDisAmt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Diesel_Amnt"));
                dblTdsAmnt = dblTdsAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TDSTax_Amnt"));
                dTotRecvdAmnt = dTotRecvdAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Recvd"));
                dTotCurBal = dTotCurBal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "cur_Bal"));
                dblCommision = dblCommision + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Commsn_Amnt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dblNetAmnt.ToString("N2");

                Label lblAdvAmt = (Label)e.Row.FindControl("lblAdvAmt");
                lblAdvAmt.Text = dblAdvAmt.ToString("N2");

                Label lblDisAmount = (Label)e.Row.FindControl("lblDisAmount");
                lblDisAmount.Text = dblDisAmt.ToString("N2");

                Label lblTdsAmount = (Label)e.Row.FindControl("lblTdsAmount");
                lblTdsAmount.Text = dblTdsAmnt.ToString("N2");

                Label lblComisionAmount = (Label)e.Row.FindControl("lblComisionAmount");
                lblComisionAmount.Text = dblCommision.ToString("N2");

                Label lblFTotRecvd = (Label)e.Row.FindControl("lblFTotRecvd");
                lblFTotRecvd.Text = dTotRecvdAmnt.ToString("N2");

                Label lblFTotCurBal = (Label)e.Row.FindControl("lblFTotCurBal");
                lblFTotCurBal.Text = dTotCurBal.ToString("N2");
            }
        }
        protected void lnkbtnClose_OnClick(object sender, EventArgs e)
        {
            txtchlnNoSearch.Text = "";
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "CloseModal();", true);
        }

        protected void grdGrdetals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGrdetals.PageIndex = e.NewPageIndex;
            DtGrdetail = (DataTable)ViewState["DtGrdetail"];
            grdGrdetals.DataSource = DtGrdetail;
            grdGrdetals.DataBind();
        }

        #endregion

        protected void lnkbtnPrintClick_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hidPrintType.Value) != 2)
            {
                Populate(Convert.ToInt64(Request.QueryString["q"]));
                ExportGridToPDF();
            }
            else
            {

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {

            challanAmntpaymentDAL objChlnPayDAL = new challanAmntpaymentDAL();
            Int64 iMaxIdno = objChlnPayDAL.MaxIdno(ApplicationFunction.ConnectionString());
            if (iMaxIdno > 0)
            {
                Printf(iMaxIdno);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrintf('printf')", true);
            }
            else
            {
                ShowMessageErr("No Record For Print.");
            }

        }
        protected void btnPrintClick_OnClick(object sender, EventArgs e)
        {
            Print1(Convert.ToInt64(Request.QueryString["q"]));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint1('printf')", true);
        }

        private void ExportGridToPDF()
        {
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; //string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");

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

            var titleFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            var boldFooterFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var bodyFont2 = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            string html = "";
            html = html + "<html><head></head><Body>";
            html = html + "<table width='100%' border='0'>";
            html = html + "<tr><td align='center'></td></tr>";
            html = html + "<tr><td align='center'><strong>" + CompName + "</strong></td></tr>";
            html = html + "<tr><td align='center' style='font-size:small;'>" + Add1 + "," + Add2 + "," + City + "," + State + "</td></tr>";
            html = html + "<tr><td align='center' style='font-size:small;'>" + PhNo + "</td></tr>";
            html = html + "<tr><td align='center'></td></tr>";
            html = html + "<tr><td align='center'><strong>Challan Payment</strong></td></tr>";
            html = html + "<tr><td><div style='border-width:1px;border-color:#000;border-style:solid;Height:40px;'>";
            html = html + "</div></td></tr></table>";
            html = html + "</body></html>";

            DataSet PrtyAdd = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select AM.Address1,Am.Address2,CM.City_Name,SM.State_Name from acntmast AM inner join tblcitymaster CM on AM.City_idno=CM.City_Idno inner join tblstatemaster SM on CM.State_idno=SM.State_idno where AM.Acnt_Idno=" + ddlPartyName.SelectedValue + "");
            PdfPTable tblprtyAdd = new PdfPTable(1);
            tblprtyAdd.WidthPercentage = 90;
            tblprtyAdd.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell CellAdd = new PdfPCell(new Phrase(("Party Address  : " + Convert.ToString(PrtyAdd.Tables[0].Rows[0][0]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][1]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][02]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][3])), bodyFont2));
            CellAdd.BorderWidth = 0;
            PdfPCell CellAdd1 = new PdfPCell(new Phrase((""), bodyFont2));
            CellAdd1.BorderWidth = 0;
            tblprtyAdd.AddCell(CellAdd);
            tblprtyAdd.AddCell(CellAdd1);

            PdfPTable tblprty = new PdfPTable(3);
            tblprty.WidthPercentage = 90;


            PdfPCell cell1Text = new PdfPCell(new Phrase(("Pay No             : " + txtRcptNo.Text), bodyFont2));
            cell1Text.BorderWidth = 0;
            PdfPCell cell2Text = new PdfPCell(new Phrase(("Date       : " + txtGRDate.Text), bodyFont2));
            cell2Text.BorderWidth = 0;
            PdfPCell cell3Text = new PdfPCell(new Phrase(("From City : " + ddlFromCity.SelectedItem.Text), bodyFont2));
            cell3Text.BorderWidth = 0;
            PdfPCell cell4Text = new PdfPCell(new Phrase(("Party Name     : " + ddlPartyName.SelectedItem.Text), bodyFont2));
            cell4Text.Colspan = 2;
            cell4Text.BorderWidth = 0;
            PdfPCell cell5Text = new PdfPCell(new Phrase(("Location : " + ddlLocation.SelectedItem.Text), bodyFont2));
            cell5Text.BorderWidth = 0;
            PdfPCell cell6Text = new PdfPCell(new Phrase(("Pay Type : " + ddlRcptTyp.SelectedItem.Text), bodyFont2));
            cell6Text.Colspan = 2;
            cell6Text.BorderWidth = 0;

            PdfPCell cell7Text = new PdfPCell(new Phrase(("Isnt. No : " + txtInstNo.Text.Trim()), bodyFont2));
            cell7Text.BorderWidth = 0;
            PdfPCell cell8Text = new PdfPCell(new Phrase(("Inst. Date : " + txtInstDate.Text.Trim()), bodyFont2));
            cell8Text.BorderWidth = 0;
            PdfPCell cell9Text = new PdfPCell(new Phrase(("Cust. Bank : " + ddlCustmerBank.SelectedItem.Text), bodyFont2));
            cell9Text.Colspan = 2;
            cell9Text.BorderWidth = 0;

            tblprty.AddCell(cell1Text);
            tblprty.AddCell(cell2Text);
            tblprty.AddCell(cell3Text);
            tblprty.AddCell(cell4Text);
            tblprty.AddCell(cell5Text);
            tblprty.AddCell(cell6Text);
            if (txtInstNo.Enabled == true && txtInstDate.Enabled == true && ddlCustmerBank.Enabled == true)
            {
                tblprty.AddCell(cell7Text);
                tblprty.AddCell(cell8Text);
                if (ddlCustmerBank.SelectedIndex > 0)
                {
                    tblprty.AddCell(cell9Text);
                }
            }
            PdfPTable table = new PdfPTable(grdMain.Columns.Count);
            table.WidthPercentage = 90;

            //Transfer rows from GridView to table

            //Header
            for (int i = 0; i < grdMain.Columns.Count; i++)
            {
                string cellText = Server.HtmlDecode(grdMain.Columns[i].HeaderText);
                PdfPCell cell = new PdfPCell(new Phrase((cellText), titleFont));
                var subTitleFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                cell.BackgroundColor = new BaseColor(54, 157, 225);
                table.AddCell(cell);
            }

            //Body
            for (int i = 0; i < grdMain.Rows.Count; i++)
            {
                if (grdMain.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    string lblGrcellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblGrno") as Label).Text);
                    PdfPCell lblGrNocell = new PdfPCell(new Phrase((lblGrcellText), bodyFont));

                    if (i % 2 != 0)
                    {
                        lblGrNocell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblGrNocell);

                    string lblGrtypecellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblGrDate") as Label).Text);
                    PdfPCell lblTypecell = new PdfPCell(new Phrase((lblGrtypecellText), bodyFont));
                    if (i % 2 != 0)
                    {
                        lblTypecell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblTypecell);

                    string lblFromCitycellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblFromcity") as Label).Text);
                    PdfPCell lblFromCitycell = new PdfPCell(new Phrase((lblFromCitycellText), bodyFont));
                    if (i % 2 != 0)
                    {
                        lblFromCitycell.BackgroundColor = new BaseColor(245, 250, 255);
                    }

                    table.AddCell(lblFromCitycell);

                    string lblTruckNocellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblTruckNo") as Label).Text);
                    PdfPCell lbltruckNocell = new PdfPCell(new Phrase((lblTruckNocellText), bodyFont));
                    if (i % 2 != 0)
                    {
                        lbltruckNocell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lbltruckNocell);


                    string lblDrivercellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblDriver") as Label).Text);
                    PdfPCell lblDrivecell = new PdfPCell(new Phrase((lblDrivercellText), bodyFont));
                    if (i % 2 != 0)
                    {
                        lblDrivecell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    //lblNarrcell.Colspan = 2;
                    table.AddCell(lblDrivecell);

                    string lblAmountcellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblAmount") as Label).Text);
                    PdfPCell lblAmountcell = new PdfPCell(new Phrase((lblAmountcellText), bodyFont));
                    lblAmountcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    if (i % 2 != 0)
                    {
                        lblAmountcell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblAmountcell);

                    string lblReccellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblTotRecvd") as Label).Text);
                    PdfPCell lblReccell = new PdfPCell(new Phrase((lblReccellText), bodyFont));
                    lblReccell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    if (i % 2 != 0)
                    {
                        lblReccell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblReccell);

                    string lblCurBalcellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("lblCurBal") as Label).Text);
                    PdfPCell lblBalcell = new PdfPCell(new Phrase((lblCurBalcellText), bodyFont));
                    lblBalcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    if (i % 2 != 0)
                    {
                        lblBalcell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblBalcell);

                    string lblRecvcellText = Server.HtmlDecode((grdMain.Rows[i].FindControl("txtRcvdAmnt") as TextBox).Text);
                    PdfPCell lblRecvcell = new PdfPCell(new Phrase((lblRecvcellText), bodyFont));
                    lblRecvcell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    if (i % 2 != 0)
                    {
                        lblRecvcell.BackgroundColor = new BaseColor(245, 250, 255);
                    }
                    table.AddCell(lblRecvcell);
                }
            }

            //Footer(Start)
            string ColText1 = "";
            PdfPCell Col1 = new PdfPCell(new Phrase((ColText1), boldFooterFont));
            Col1.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col1);

            string ColText2 = "";
            PdfPCell Col2 = new PdfPCell(new Phrase((ColText2), boldFooterFont));
            Col2.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col2);

            string ColText3 = "";
            PdfPCell Col3 = new PdfPCell(new Phrase((ColText3), boldFooterFont));
            Col3.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col3);

            string ColText4 = "";
            PdfPCell Col4 = new PdfPCell(new Phrase((ColText4), boldFooterFont));
            Col4.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col4);

            string ColText5 = "";
            PdfPCell Col5 = new PdfPCell(new Phrase((ColText5), boldFooterFont));
            Col5.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col5);

            string ColText6 = Server.HtmlDecode((grdMain.FooterRow.FindControl("lblAmount") as Label).Text);
            PdfPCell Col6 = new PdfPCell(new Phrase((ColText6), boldFooterFont));
            Col6.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            Col6.BackgroundColor = new BaseColor(54, 157, 225);
            table.AddCell(Col6);

            string ColText7 = Server.HtmlDecode((grdMain.FooterRow.FindControl("lblFTotRecvd") as Label).Text);
            PdfPCell Col7 = new PdfPCell(new Phrase((ColText7), boldFooterFont));
            Col7.BackgroundColor = new BaseColor(54, 157, 225);
            Col7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            table.AddCell(Col7);

            string ColText8 = Server.HtmlDecode((grdMain.FooterRow.FindControl("lblFTotCurBal") as Label).Text);
            PdfPCell Col8 = new PdfPCell(new Phrase((ColText8), boldFooterFont));
            Col8.BackgroundColor = new BaseColor(54, 157, 225);
            Col8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            table.AddCell(Col8);

            string ColText9 = txtNetAmnt.Text;
            PdfPCell Col9 = new PdfPCell(new Phrase((ColText9), boldFooterFont));
            Col9.BackgroundColor = new BaseColor(54, 157, 225);
            Col9.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            table.AddCell(Col9);
            //End


            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StringReader(html), null);
            for (int k = 0; k < htmlarraylist.Count; k++)
            {
                IElement element = (IElement)htmlarraylist[k];
                PdfPTable Htmltable = element as PdfPTable;
                pdfDoc.Add(element);
            }
            pdfDoc.Add(tblprty);
            pdfDoc.Add(tblprtyAdd);
            pdfDoc.Add(table);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=PartyPayment.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void grdMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PrintChlnDetails(string HeadIdno)
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
            lblCompanyname.Text = CompName; //lblcompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            //lblCompTIN.Text = TinNo.ToString();
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;

            #endregion

            DataSet dsReport = null;
            if (Convert.ToInt32(hidPrintType.Value) == 2)
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnAmntPayment] @ACTION='DaulatPrint',@ChlnIdnos='" + HeadIdno + "'");
            }

            Repeater1.DataSource = dsReport.Tables[0];
            Repeater1.DataBind();

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                dtotWt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotWeight"));
                dAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "GrossAmnt"));
                dTDS += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TDS"));
                dShor += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ShortageAmount"));
                dGross += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "GrossFreight"));
                dAdv += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "AdvanceAmount"));
                dBal += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "BalancePayment"));
                dPay += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "PayablePayment"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                Label totwt = (Label)e.Item.FindControl("lblFTotWt");
                totwt.Text = dtotWt.ToString("N2");

                Label totAmnt = (Label)e.Item.FindControl("lblFAmnt");
                totAmnt.Text = dAmnt.ToString("N2");

                Label TDS = (Label)e.Item.FindControl("lblFTDS");
                TDS.Text = dTDS.ToString("N2");

                Label Shor = (Label)e.Item.FindControl("lblShor");
                Shor.Text = dShor.ToString("N2");

                Label Gross = (Label)e.Item.FindControl("lblFGross");
                Gross.Text = dGross.ToString("N2");

                Label Adv = (Label)e.Item.FindControl("lblFAdv");
                Adv.Text = dAdv.ToString("N2");

                Label Bal = (Label)e.Item.FindControl("lblBal");
                Bal.Text = dBal.ToString("N2");

                Label Pay = (Label)e.Item.FindControl("lblPayable");
                Pay.Text = dPay.ToString("N2");
            }
        }

        private void DirectPaymentFromGr(Int64 ChlnIdno)
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            string strSbillNo = String.Empty;

            DataTable dtRcptDetl = new DataTable(); DataRow Dr;
            dtRcptDetl = obj.SelectChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(ChlnIdno));
            ViewState["dt"] = dtRcptDetl;
            if (dtRcptDetl != null && dtRcptDetl.Rows.Count > 0)
            {
                ddlPartyName.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Prty_Idno"]);
                ddlFromCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["FromCity_Idno"]);
            }
            BindGrid();
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
        }
    }
}
