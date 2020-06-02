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
using Microsoft.ApplicationBlocks.Data;
namespace WebTransport
{
    public partial class AmntAgainstGr : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); //string con = "";
        double dblNetAmnt = 0; double dTotCurBal, dTotRecvdAmnt = 0;
        private int intFormId = 32; double printtotamnt, PrintRecvdTotAmnt, PrintCurBa, PtintRecvd = 0;
        #endregion

        #region Page Enents...
        protected void Page_Load(object sender, EventArgs e)
        {
            // con ApplicationFunction.ConnectionString() ;//ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
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
                this.BindReceiptType();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }

                this.BindDateRange();
                this.BindPartName();
                this.BindCustBank();
                this.BindPartName();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0; txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRcptNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                ddldateRange_SelectedIndexChanged(null, null);
                RcvdAmntAgnstGRDAL objChlnBookingDAL = new RcvdAmntAgnstGRDAL();
                tblUserPref obj = objChlnBookingDAL.selectUserPref();
                //  ddlFromCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                lnkimgSearch.Enabled = true;
                ddlRcptType_SelectedIndexChanged(null, null);
                GetRcptNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                ddlFromCity.Enabled = true;
                ddlFromCity_SelectedIndexChanged(null, null);
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnPrint.Visible = true;
                    lnkbtnNew.Visible = true;
                }

                else
                {
                    lnkbtnPrint.Visible = false;
                    lnkbtnNew.Visible = false;

                }

            }
        }
        #endregion

        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
        }

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
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblTotRecvd = (Label)row.FindControl("lblTotRecvd");
                    Label lblCurBal = (Label)row.FindControl("lblCurBal");
                    TextBox txtRcvdAmnt = (TextBox)row.FindControl("txtRcvdAmnt");

                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, lblGrno.Text, (ApplicationFunction.mmddyyyy(lblGrDate.Text)), hidRecivrIdno.Value, "BK", hidToCityIdno.Value, hidFromCityIdno.Value, lblAmount.Text, lblTotRecvd.Text, lblCurBal.Text, txtRcvdAmnt.Text);

                }
                RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                tblAmntRecvdGR_Head objRGH = new tblAmntRecvdGR_Head();
                objRGH.Rcpt_No = Convert.ToInt64(txtRcptNo.Text);
                objRGH.BaseCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                objRGH.FromCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                objRGH.Rcpt_date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text));
                objRGH.Party_IdNo = Convert.ToInt64(ddlPartyName.SelectedValue);
                objRGH.Inst_No = Convert.ToString(txtInstNo.Text);
                objRGH.Date_Added = System.DateTime.Now;
                objRGH.Date_Modified = null;
                objRGH.CustBank_Idno = Convert.ToInt32(ddlCustmerBank.SelectedValue);
                objRGH.RcptTyp_Idno = Convert.ToInt32(ddlRcptTyp.SelectedValue);
                objRGH.UserIdno = Convert.ToInt64(Session["UserIdno"]);
                if (txtInstDate.Text == "")
                {
                    objRGH.Inst_Date = null;
                }
                else
                {
                    objRGH.Inst_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
                }

                objRGH.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                objRGH.Remark = Convert.ToString(TxtRemark.Text);
                objRGH.status = true;
                objRGH.Year_IdNo = Convert.ToInt32(ddldateRange.SelectedValue);
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        value = obj.Insert(objRGH, DtTemp);
                        obj = null;
                    }
                    else
                    {

                        value = obj.Update(objRGH, Convert.ToInt32((Convert.ToString(hidid.Value) == "" ? 0 : Convert.ToInt32(hidid.Value))), DtTemp);
                        obj = null;
                    }
                    if (value > 0)
                    {
                        if (this.PostIntoAccounts(Convert.ToDouble(txtNetAmnt.Text), value, "ARGR", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue)) == true)
                        {
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {

                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                {
                                    ShowMessage("Record Update successfully");
                                    this.Clear();
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Rcpt No Already Exist");
                                }
                                else if (string.IsNullOrEmpty(hidid.Value) == false)
                                {
                                    ShowMessageErr("Record  Not Update");
                                }
                            }
                            else
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                                {
                                    ShowMessage("Record Saved successfully");
                                    this.Clear();
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Rcpt No Already Exist");
                                }
                                else if ((string.IsNullOrEmpty(hidid.Value) == true))
                                {
                                    ShowMessageErr("Record  Not Saved successfully");
                                }
                            }
                            tScope.Complete();


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
                ShowMessageErr("Please Enter Detais");
                return;
            }

        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Gr_No", "String", "Gr_Date", "String", "Recivr_Idno", "String", "GR_From", "String", "To_City", "String", "From_City", "String",
                                                                   "Amount", "String", "Tot_Recvd", "String", "cur_Bal", "String", "Recv_Amount", "String");
            return dttemp;
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                Int64 iPRTYIDNO = (ddlPartyName.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue));
                Int32 iFromcity = (ddlFromCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                string strGrFrm = "";
                strGrFrm = "BK";
                DataTable DsGrdetail = obj.SelectGRPaymentDetail("SelectGRPaymentDetail", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iPRTYIDNO, strGrFrm, iFromcity, ApplicationFunction.ConnectionString());
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnsubmit.Visible = true;
                    lblTotalRecord.Text = "T. Record(s) : " + Convert.ToString(DsGrdetail.Rows.Count);
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnsubmit.Visible = false;
                    lblTotalRecord.Text = "T. Record(s) : 0";
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
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["q"]));
            }
            else
            {
                Clear();
            }

        }

        private void GetReceiptNo()
        {
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            Int64 max = obj.GetMaxNo();
            obj = null;
            txtRcptNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("AmntAgainstGr.aspx");
        }

        protected void lnkimgSearch_Click(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex <= 0)
            {
                ShowMessageErr("Please select From City.");
            }
            else if (ddlPartyName.SelectedIndex <= 0)
            {
                ShowMessageErr("Please select Party Name.");
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                txtDateFrom.Focus();
                //lnkbtnsubmit.Visible = false;
                //lnkbtnCancel.Visible = false;
            }

        }

        protected void lnkbtnsubmit_Click(object sender, EventArgs e)
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
                        ShowMessageErr("Please check atleast one Gr.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);

                    }
                    else
                    {

                        RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                        string strSbillNo = String.Empty;

                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = obj.SelectGRDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
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
        private void PrintAgainstGr(Int64 HeadIdno)
        {
            Repeater obj = new Repeater();


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
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
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
            if (TinNo == "")
            {
                lblCompTIN1.Visible = false; lblTin1.Visible = false;
            }
            else
            {
                lblCompTIN1.Text = TinNo;
                lblCompTIN1.Visible = true; lblTin1.Visible = true;
            }

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spAmntRecvdGR] @ACTION='SelectPrint',@Id='" + HeadIdno + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(txtRcptNo.Text);
                lblGrDate.Text = Convert.ToString(txtGRDate.Text);
                Repeater2.DataSource = dsReport.Tables[0];
                Repeater2.DataBind();
                valuelbltxtPartyName.Text = Convert.ToString(ddlPartyName.SelectedItem.Text);
                valuelblnetamntAtbttm.Text = Convert.ToString(txtNetAmnt.Text);
            }

        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void GetRcptNo(Int32 YearIdno, Int32 FromCityIdno)
        {
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            txtRcptNo.Text = Convert.ToString(obj.GetRcptNo(YearIdno, FromCityIdno));
        }
        private void NetAmntCal()
        {
            double dblAmnt = 0;
            foreach (GridViewRow dr in grdMain.Rows)
            {
                TextBox txtRcvdAmnt = (TextBox)dr.FindControl("txtRcvdAmnt");
                dblAmnt += Convert.ToDouble(txtRcvdAmnt.Text);
            }

            txtNetAmnt.Text = dblAmnt.ToString("N2");
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
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            var PartName = obj.BindParty();
            ddlPartyName.DataSource = PartName;
            ddlPartyName.DataTextField = "ACNT_NAME";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindReceiptType()
        {
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            var RcptType = obj.BindRcptType(ApplicationFunction.ConnectionString());
            ddlRcptTyp.DataSource = RcptType;
            ddlRcptTyp.DataTextField = "ACNT_NAME";
            ddlRcptTyp.DataValueField = "Acnt_Idno";
            ddlRcptTyp.DataBind();
            ddlRcptTyp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCustBank()
        {
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            ddlCustmerBank.DataSource = obj.BindBank();
            ddlCustmerBank.DataTextField = "Acnt_Name";
            ddlCustmerBank.DataValueField = "Acnt_Idno";
            ddlCustmerBank.DataBind();
            ddlCustmerBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            tblAmntRecvdGR_Head AmntGrhead = obj.selectHead(HeadId);
            ddldateRange.SelectedValue = Convert.ToString(AmntGrhead.Year_IdNo);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtRcptNo.Text = Convert.ToString(AmntGrhead.Rcpt_No);
            txtRcptNo.Visible = true; ddlFromCity.SelectedValue = Convert.ToString(AmntGrhead.BaseCity_Idno);
            txtGRDate.Text = Convert.ToDateTime(AmntGrhead.Rcpt_date).ToString("dd-MM-yyyy");
            ddlPartyName.SelectedValue = Convert.ToString(AmntGrhead.Party_IdNo);
            ddlRcptTyp.SelectedValue = Convert.ToString(AmntGrhead.RcptTyp_Idno);
            ddlRcptType_SelectedIndexChanged(null, null);
            ddlCustmerBank.SelectedValue = Convert.ToString(AmntGrhead.CustBank_Idno);
            txtInstNo.Text = Convert.ToString(AmntGrhead.Inst_No);
            txtInstDate.Text = (Convert.ToString(AmntGrhead.Inst_Date) == "") ? "" : Convert.ToDateTime(AmntGrhead.Inst_Date).ToString("dd-MM-yyyy");
            TxtRemark.Text = Convert.ToString(AmntGrhead.Remark);
            txtNetAmnt.Text = Convert.ToDouble(AmntGrhead.Net_Amnt).ToString("N2");
            DtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId);
            obj = null;
            ViewState["dt"] = DtTemp;
            this.BindGrid();
            lnkimgSearch.Visible = false;
            ddlFromCity.Enabled = false;
            PrintAgainstGr(HeadId);
        }

        private void Clear()
        {
            ddlFromCity.Enabled = true;
            ViewState["dt"] = null;
            DtTemp = null;
            ddlPartyName.SelectedIndex = 0;
            TxtRemark.Text = "";
            if (hidid.Value != null && hidid.Value != "")
            {
                lnkbtnNew.Visible = false;
                lnkbtnPrint.Visible = false;
            }
            hidid.Value = string.Empty;
            // txtGRDate.Text = "";
            txtRcptNo.Text = "";
            //ddldateRange.SelectedIndex = 0; ;
            // ddldateRange_SelectedIndexChanged(null, null);
            BindGrid();
            txtNetAmnt.Text = "0.00";
            ddldateRange.Enabled = true;
            // ddldateRange.SelectedIndex = 0;
            lnkimgSearch.Visible = true;
            txtInstDate.Text = "";
            txtInstNo.Text = "";
            ddlRcptTyp.SelectedIndex = 0;
            ddlCustmerBank.SelectedIndex = 0; ddlRcptTyp.SelectedIndex = ddlCustmerBank.SelectedIndex = 0; txtInstNo.Text = "";
            RcvdAmntAgnstGRDAL objChlnBookingDAL = new RcvdAmntAgnstGRDAL();
            tblUserPref obj = objChlnBookingDAL.selectUserPref();
            ddlFromCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
            ddlFromCity_SelectedIndexChanged(null, null);
        }

        private void BindGrid()
        {
            DtTemp = (DataTable)ViewState["dt"];
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
            //    IAcntIdno = Convert.ToInt32(ddldriverName.SelectedValue);
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
                Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(2, 0, Convert.ToInt32(ddldateRange.SelectedValue));
                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())),
                2,
                Convert.ToInt32(ddlRcptTyp.SelectedValue),
                "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                true,
                0,
                strDocType,
                0,
                0,
                0,
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim())),
                VchrNo,
                0,
               YearIdno,
               0, 0);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;
                    intValue = 0;
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    Convert.ToInt32(ddlRcptTyp.SelectedValue),
                     "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                    Amount,
                    Convert.ToByte(2),
                    Convert.ToByte(0),
                    Convert.ToString(txtInstNo.Text),
                    true,
                  dtBankDate,
                    Convert.ToString(ddlCustmerBank.SelectedValue), 0);
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt32(ddlPartyName.SelectedValue),
                         "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                        Amount,
                        Convert.ToByte(1),
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

        #region Control Events..............................

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
        }

        protected void ddlRcptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInstNo.Text = ""; rfvinstDate.Enabled = false; rfvinstno.Enabled = false; rfvCusBank.Enabled = false;
            txtInstNo.Enabled = false;
            txtInstDate.Enabled = false; ddlCustmerBank.Enabled = false;
            if (ddlRcptTyp.SelectedIndex > 0)
            {
                RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                DataTable dt = obj.BindRcptTypeDel(Convert.ToInt32((ddlRcptTyp.SelectedValue) == "" ? "0" : ddlRcptTyp.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int64 intAcnttype = Convert.ToInt64(dt.Rows[0]["ACNT_TYPE"]);
                    if (intAcnttype == 4)
                    {

                        txtInstNo.Enabled = true;
                        txtInstDate.Enabled = true;
                        ddlCustmerBank.Enabled = true;
                        rfvinstDate.Enabled = true; rfvinstno.Enabled = true; rfvCusBank.Enabled = true;
                    }
                }
            }

        }

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlPartyName.SelectedIndex <= 0)
            {
                lnkimgSearch.Visible = false;
            }
            else
            {
                lnkimgSearch.Visible = true;
            }

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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (grdGrdetals.Rows.Count > 0)
            {
                //if ()
                //{

                ////}
                foreach (GridViewRow row in grdGrdetals.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkId");
                    if (chkSelect.Checked == true)
                    {
                        chkSelect.Checked = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
                    }
                    else
                    {
                        chkSelect.Checked = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                    }
                }
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                printtotamnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                PrintRecvdTotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Recvd"));
                PrintCurBa += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "cur_Bal"));
                PtintRecvd += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Recv_Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lblPrintTotAmnt.Text = printtotamnt.ToString("N2");
                lblTorPrintRecvd.Text = PrintRecvdTotAmnt.ToString("N2");
                lblPrintCurBal.Text = PrintCurBa.ToString("N2");
                lblPrintRecvd.Text = PtintRecvd.ToString("N2");
            }
        }
        #endregion

        #region Grid Events...................
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtRcvdAmnt = (TextBox)e.Row.FindControl("txtRcvdAmnt");
                txtRcvdAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dTotRecvdAmnt = dTotRecvdAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Recvd")); ;
                dTotCurBal = dTotCurBal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "cur_Bal")); ;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dblNetAmnt.ToString("N2");
                Label lblFTotRecvd = (Label)e.Row.FindControl("lblFTotRecvd");
                lblFTotRecvd.Text = dTotRecvdAmnt.ToString("N2");
                Label lblFTotCurBal = (Label)e.Row.FindControl("lblFTotCurBal");
                lblFTotCurBal.Text = dTotCurBal.ToString("N2");
            }
        }


        #endregion

        protected void ddlRcptTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInstNo.Text = ""; ddlCustmerBank.SelectedIndex = 0; txtInstDate.Text = "";
            rfvinstDate.Enabled = rfvinstno.Enabled = false;
            txtInstNo.Enabled = false; rfvCusBank.Enabled = false;
            txtInstDate.Enabled = false;
            ddlCustmerBank.Enabled = false; rfvCusBank.Enabled = false;
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
                    ddlCustmerBank.Enabled = true; rfvCusBank.Enabled = true;
                }
            }
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                GetRcptNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }

    }
}