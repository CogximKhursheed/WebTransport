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
    public partial class AmntAgainstInvoiceOTH : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; DataTable DtGrdetail = new DataTable();
        double dblNetAmnt = 0; Int32 iFromCity = 0, intYearIdno = 0; double dTotCurBal, dTotRecvdAmnt = 0; double dblChallanAmnt = 0, due = 0, vchrPendAmnt = 0;
        private int intFormId = 33; double printtotamnt, PrintRecvdTotAmnt, PrintCurBa, PtintRecvd = 0; double prevAmnt = 0;
        DataTable DtNew = new DataTable("DtNew");
        DataTable DtNew1 = new DataTable("DtNew1");
        DataTable dtVchrtemp = new DataTable();
        DataTable dtVchr = new DataTable();
        #endregion

        #region Page Enents...
        protected void Page_Load(object sender, EventArgs e)
        {
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
                txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                // this.userpref();
                this.BindDateRange();
                Int32 intYearIdno = Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue));
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    //this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                //this.BindMaxNo(Convert.ToInt32(ddlFromCity.SelectedValue), intYearIdno);
                this.BindReceiptType();
                this.BindPartName();
                this.BindCustBank();
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                lnkimgSearch.Visible = true;
                ddlRcptType_SelectedIndexChanged(null, null);
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnPrint.Visible = true;
                    lnkbtnNew.Visible = true;
                    lnkbtnAmntSave.Visible = false;
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        ImageButton img = (ImageButton)row.FindControl("btnVchrRef");
                        img.Enabled = true;
                    }
                }

                else
                {
                    GetReceiptNo();
                    lnkbtnNew.Visible = false;
                    lnkbtnPrint.Visible = false;
                }
                // RcvdAmntAgnstInvcDAL obj = new RcvdAmntAgnstInvcDAL();
                hidtotVchrAmnt.Value = "0";
                ddldateRange.Focus();
            }
        }
        #endregion

        #region Button Evnets...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtNetAmnt.Text) <= 0)
            {
                ShowMessageErr("Net Amount Must be Greater Than Zero.");
                grdMain.Focus();
                return;
            }
            if (grdMain.Rows.Count > 0)
            {
                DtTemp = CreateDt();


                DtNew.Columns.Add("DebitAmnt", typeof(double), null);
                DtNew.Columns.Add("TdsAmnt", typeof(double), null);
                DtNew.Columns.Add("Recamnt", typeof(double), null);

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
                    TextBox txtTdsAmnt = (TextBox)row.FindControl("txtTdsAmnt");
                    TextBox txtDebitNote = (TextBox)row.FindControl("txtDebitNote");
                    TextBox txtRefNo = (TextBox)row.FindControl("txtRefNo");

                    DtNew.Rows.Add(Convert.ToDouble(txtDebitNote.Text), Convert.ToDouble(txtTdsAmnt.Text), Convert.ToDouble(txtRcvdAmnt.Text));
                    if (Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt.Text) + Convert.ToDouble(txtDebitNote.Text) + Convert.ToDouble(txtTdsAmnt.Text)) > Convert.ToDouble(lblCurBal.Text))
                    {
                        ShowMessageErr("Total Amount Cannot be greater than Cur. Balance For Invoice No." + Convert.ToString(lblGrno.Text));
                        grdMain.Focus();
                        return;
                    }
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, lblGrno.Text, (ApplicationFunction.mmddyyyy(lblGrDate.Text)), hidRecivrIdno.Value, lblAmount.Text, lblTotRecvd.Text, lblCurBal.Text, Convert.ToDouble(txtRcvdAmnt.Text), Convert.ToDouble(txtTdsAmnt.Text), Convert.ToDouble(txtDebitNote.Text), Convert.ToDouble(txtRefNo.Text));

                }
                RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
                tblAmntRecvdHireInv_Head objRGH = new tblAmntRecvdHireInv_Head();
                objRGH.Rcpt_No = Convert.ToString(txtRcptNo.Text);
                //objRGH.BaseCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                //objRGH.FromCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                objRGH.Rcpt_date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text));
                objRGH.Party_IdNo = Convert.ToInt64(ddlPartyName.SelectedValue);
                objRGH.Inst_No = Convert.ToString(txtInstNo.Text);
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
                objRGH.Status = true;
                objRGH.Year_IdNo = Convert.ToInt32(ddldateRange.SelectedValue);
                Int64 value = 0;
           //   Remove Commnt
           //     using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
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
                        if (this.PostIntoAccounts(Convert.ToDouble(txtNetAmnt.Text), value, "ARIV", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue), DtNew) == true)
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
                    //   Remove Commnt

                    //        tScope.Complete();


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
                           //   Remove Commnt
                           //     tScope.Dispose();
                            }
                          //   Remove Commnt
                         //   tScope.Dispose();
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
            //DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Gr_No", "String", "Gr_Date", "String", "Recivr_Idno", "String", "GR_From", "String", "To_City", "String", "From_City", "String",
            //                                                       "Amount", "String", "Tot_Recvd", "String", "cur_Bal", "String", "Recv_Amount", "String");
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Inv_Idno", "String", "Inv_No", "String", "Inv_Date", "String", "Recivr_Idno", "String",
                                                                  "Amount", "String", "Tot_Recvd", "String", "cur_Bal", "String", "Recv_Amount", "String", "TDS_Amnt",
                                                                  "String", "Dr_Note", "String", "Vchr_Amnt", "String");
            return dttemp;
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
                Int64 iPRTYIDNO = (ddlPartyName.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue));
                Int32 iFromcity = (ddlFromCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                string strGrFrm = "";
                strGrFrm = "BK";//string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 PrtIdno, Int64 frmcity, string con
                DataTable DsGrdetail = obj.SelectInvPaymentDetail("SelectInvPaymentDetail", Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iPRTYIDNO, iFromcity, ApplicationFunction.ConnectionString());
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    ViewState["DtGrdetail"] = DsGrdetail;
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnsubmit.Visible = true;

                    lblTotalRecord.Text = "T. Record (s): " + DsGrdetail.Rows.Count;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnsubmit.Visible = false;

                    lblTotalRecord.Text = "T. Record (s): ";
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
            RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
            Int64 max = obj.GetMaxNo();
            obj = null;
            txtRcptNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("AmntAgainstInvoice.aspx");
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient()", true);
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
                        RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
                        string strSbillNo = String.Empty;

                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = obj.SelectInvDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                    }
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();


                }
                lnkimgSearch.Focus();
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }

        }


        protected void lnkbtnAmntSave_Click(object sender, EventArgs e)
        {
            string strMsg;
            TextBox totVchrAmnt = (TextBox)grdVchr.FooterRow.FindControl("txttotAmnt");
            if (totVchrAmnt.Text != "")
            {
                if (Convert.ToDouble(totVchrAmnt.Text) < Convert.ToDouble(hidrowindex.Value))
                {
                    if (grdVchr.Rows.Count > 0)
                    {
                        dtVchrtemp = CreateVchrDt();

                        foreach (GridViewRow row in grdVchr.Rows)
                        {
                            Label lblVchrId = (Label)row.FindControl("lblVchrId");
                            Label lblVchrAmnt = (Label)row.FindControl("lblVchrAmnt");
                            TextBox txtamnt = (TextBox)row.FindControl("txtAmount");
                            if (txtamnt.Text != "00.00")
                            {
                                ApplicationFunction.DatatableAddRow(dtVchrtemp, hidInvNo.Value, lblVchrId.Text, txtamnt.Text);
                            }
                        }
                        RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
                        Int64 intVchr = 0;
                        if (string.IsNullOrEmpty(hidid.Value) == false)
                        {
                            intVchr = obj.VchrSave(dtVchrtemp);
                        }
                        obj = null;
                        if (intVchr > 0)
                        {
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {
                                strMsg = "Record updated successfully.";
                            }
                            else
                            {
                                strMsg = "Record saved successfully.";
                            }
                            this.ClearControls();
                        }
                        else if (intVchr < 0)
                        {
                            strMsg = "Record already exists.";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {
                                strMsg = "Record not updated.";
                            }
                            else
                            {
                                strMsg = "Record not saved.";
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                    }

                }
                else
                {
                    lblmsg.Text = "Amount cannot be greater than Prev Bal";
                }

            }
            ClearControls();
            int index = Convert.ToInt32(hidgrdindex.Value);
            TextBox VchrAmnt = ((TextBox)grdMain.Rows[Convert.ToInt32(index)].FindControl("txtRefNo"));
            TextBox txt = grdVchr.FooterRow.FindControl("txttotAmnt") as TextBox;
            VchrAmnt.Text = Convert.ToDouble(txt.Text).ToString("N2");
            NetAmntCal();
        }
        protected void btnVchrRef_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hidgrdindex.Value = row.RowIndex.ToString();
            string CurPrevBal = ((Label)grdMain.Rows[Convert.ToInt32(row.RowIndex)].FindControl("lblCurBal")).Text;
            string TDS = ((TextBox)grdMain.Rows[Convert.ToInt32(row.RowIndex)].FindControl("txtTdsAmnt")).Text;
            string DrNote = ((TextBox)grdMain.Rows[Convert.ToInt32(row.RowIndex)].FindControl("txtDebitNote")).Text;
            string RcvdAmnt = ((TextBox)grdMain.Rows[Convert.ToInt32(row.RowIndex)].FindControl("txtRcvdAmnt")).Text;

            double bal = Convert.ToDouble(CurPrevBal) - Convert.ToDouble(TDS) + Convert.ToDouble(DrNote) + Convert.ToDouble(RcvdAmnt);
            hidrowindex.Value = bal.ToString();

            hidInvNo.Value = ((Label)grdMain.Rows[Convert.ToInt32(row.RowIndex)].FindControl("lblGrno")).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openpopup();", true);
        }
        protected void lnkclose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closepopup();", true);
        }
        protected void lnkbtnFetch_Click(object sender, EventArgs e)
        {
            lnkbtnAmntSave.Visible = true;
            RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
            dtVchr = obj.selectVchr(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlPartyName.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), Convert.ToInt32(hidInvNo.Value));

            if (dtVchr != null && dtVchr.Rows.Count > 0)
            {
                grdVchr.DataSource = dtVchr;
                grdVchr.DataBind();
            }
            else
            {
                grdVchr.DataSource = null;
                grdVchr.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openpopup();", true);
        }

        #endregion

        #region Functions...

        private void PrintAgainstInvoice(Int64 HeadIdno)
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spAmntRecvdHireInvoice] @ACTION='SelectPrint',@Id='" + HeadIdno + "'");

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

        //public void userpref()
        //{
        //    RcvdAmntAgnstInvcDAL obj = new RcvdAmntAgnstInvcDAL();
        //    tblUserPref userpref = obj.selectuserpref();
        //    iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
        //}

        //private void BindCity(Int64 UserIdno)
        //{
        //    BindDropdownDAL obj = new BindDropdownDAL();
        //    var FrmCity = obj.BindCityUserWise(UserIdno);
        //    ddlFromCity.DataSource = FrmCity;
        //    ddlFromCity.DataTextField = "cityname";
        //    ddlFromCity.DataValueField = "cityidno";
        //    ddlFromCity.DataBind();
        //    ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}

        //private void BindMaxNo(Int32 FromCityIdno, Int32 YearId)
        //{
        //    RcvdAmntAgnstInvcDAL obj = new RcvdAmntAgnstInvcDAL();
        //    Int64 MaxNo = obj.MaxNo(YearId, FromCityIdno, ApplicationFunction.ConnectionString());
        //    txtRcptNo.Text = Convert.ToString(MaxNo);
        //}

        private void NetAmntCal()
        {
            double dblAmnt = 0;
            double dueAmnt = 0;
            foreach (GridViewRow dr in grdMain.Rows)
            {
                TextBox txtRcvdAmnt = (TextBox)dr.FindControl("txtRcvdAmnt");
                TextBox txtTdsAmnt = (TextBox)dr.FindControl("txtTdsAmnt");
                TextBox txtDebitNote = (TextBox)dr.FindControl("txtDebitNote");
                Label lblDue = (Label)dr.FindControl("lblDue");
                Label lblCurrBAL = (Label)dr.FindControl("lblCurBal");
                TextBox txtRefNo = (TextBox)dr.FindControl("txtRefNo");

                dblAmnt += Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt.Text) + Convert.ToDouble(txtTdsAmnt.Text) + Convert.ToDouble(txtDebitNote.Text) + Convert.ToDouble(txtRefNo.Text));
                dueAmnt = (Convert.ToDouble(lblCurrBAL.Text) - Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt.Text) + Convert.ToDouble(txtTdsAmnt.Text) + Convert.ToDouble(txtDebitNote.Text) + +Convert.ToDouble(txtRefNo.Text)));
                lblDue.Text = dueAmnt.ToString("N2");
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

        private void BindCity()
        {
            //RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            //var lst = obj.SelectCityCombo();
            //obj = null;
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
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
                   // txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                   // txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFromSearch.Text = Convert.ToString(hidmindate.Value);
                    txtDateToSearch.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    //txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    //txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                    txtDateToSearch.Text = Convert.ToString(hidmaxdate.Value);
                }
            }
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            txtDateFrom.Text = Convert.ToString(list.GetValue(0));
            txtDateTo.Text = Convert.ToString(list.GetValue(1));
        }

        private void Populate(Int64 HeadId)
        {
            RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
            tblAmntRecvdHireInv_Head AmntGrhead = obj.selectHead(HeadId);
            ddldateRange.SelectedValue = Convert.ToString(AmntGrhead.Year_IdNo);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtRcptNo.Text = Convert.ToString(AmntGrhead.Rcpt_No);
            //txtRcptNo.Visible = true; ddlFromCity.SelectedValue = Convert.ToString(AmntGrhead.BaseCity_Idno);
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

            obj = null;
            lnkimgSearch.Visible = false;
            PrintAgainstInvoice(HeadId);
        }

        private void Clear()
        {
            hidid.Value = string.Empty;
            lnkbtnNew.Visible = false;
            lnkbtnPrint.Visible = false;

            ViewState["dt"] = null;
            DtTemp = null; hidid.Value = "";
            ddlPartyName.SelectedIndex = 0;
            TxtRemark.Text = "";
            hidid.Value = string.Empty;
            // txtGRDate.Text = "";
            txtRcptNo.Text = "";
            // ddldateRange.SelectedIndex = 0; ;
            // ddldateRange_SelectedIndexChanged(null, null);
            BindGrid();
            txtNetAmnt.Text = "0.00";
            ddldateRange.Enabled = true;
            ddldateRange.SelectedIndex = 0;
            lnkimgSearch.Visible = true;
            txtInstDate.Text = "";
            txtInstNo.Text = "";
            ddlRcptTyp.SelectedIndex = 0;
            ddlCustmerBank.SelectedIndex = 0; ddlRcptTyp.SelectedIndex = ddlCustmerBank.SelectedIndex = 0; txtInstNo.Text = "";
            //this.BindMaxNo(Convert.ToInt32(ddlFromCity.SelectedValue), Convert.ToInt32(ddldateRange.SelectedValue));
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

        private DataTable CreateVchrDt()
        {
            dtVchrtemp = ApplicationFunction.CreateTable("tbl", "InvNo", "String", "VchrId", "String", "Amount", "String");
            return dtVchrtemp;
        }

        public void ClearControls()
        {
            grdVchr.DataSource = null;
            grdVchr.DataBind();
        }

        private bool PostIntoAccounts(double Amount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno, DataTable Dt)
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
            ChlnBookingDAL obj = new ChlnBookingDAL();
            Int32 IAcntIdno = 0;
            RcvdAmntAgnstHireInvcDAL obj1 = new RcvdAmntAgnstHireInvcDAL();
            tblAcntLink objAcntLink = obj1.SelectAcntLink();

            double DebitNAmnt = Convert.ToDouble(Dt.Compute("Sum(TdsAmnt)", ""));
            double TdsAmnt = Convert.ToDouble(Dt.Compute("Sum(DebitAmnt)", ""));
            double RecAmnt = Convert.ToDouble(Dt.Compute("Sum(Recamnt)", ""));
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
                    if (intValue > 0 && DebitNAmnt > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt32(objAcntLink.Debit_Idno),
                         "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                        DebitNAmnt,
                        Convert.ToByte(1),
                        Convert.ToByte(0),
                         Convert.ToString(txtInstNo.Text),
                         false,
                          dtBankDate,
                        Convert.ToString(ddlCustmerBank.SelectedValue), 0);
                    }
                    if (intValue > 0 && TdsAmnt > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt32(objAcntLink.TDSAmnt_Idno),
                         "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                        TdsAmnt,
                        Convert.ToByte(1),
                        Convert.ToByte(0),
                         Convert.ToString(txtInstNo.Text),
                         false,
                          dtBankDate,
                        Convert.ToString(ddlCustmerBank.SelectedValue), 0);
                    }
                    if (intValue > 0 && RecAmnt > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt32(ddlPartyName.SelectedValue),
                         "Invoice No: " + Convert.ToString(txtRcptNo.Text) + " Invoice Date: " + txtGRDate.Text.Trim(),
                        RecAmnt,
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

        #region Control Events...
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
            Label lblGrno = (Label)currentRow.FindControl("lblGrno");
            TextBox txtRcvdAmnt1 = (TextBox)currentRow.FindControl("txtRcvdAmnt");
            TextBox txtDebitNote = (TextBox)currentRow.FindControl("txtDebitNote");
            TextBox txtTdsAmnt = (TextBox)currentRow.FindControl("txtTdsAmnt");
            Label lblCurBal = (Label)currentRow.FindControl("lblCurBal");
            Label lblDue = (Label)currentRow.FindControl("lblDue");
            TextBox txtRefNo = (TextBox)currentRow.FindControl("txtRefNo");
            if (txtRcvdAmnt1.Text == "")
            {
                txtRcvdAmnt1.Text = "0.00";
            }
            else
            {
                txtRcvdAmnt1.Text = Convert.ToDouble(txtRcvdAmnt1.Text).ToString("N2");
                if (Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt1.Text) + Convert.ToDouble(txtDebitNote.Text) + Convert.ToDouble(txtTdsAmnt.Text) + +Convert.ToDouble(txtRefNo.Text)) > Convert.ToDouble(lblCurBal.Text))
                {
                    ShowMessageErr("Total Amount Cannot be greater than Cur. Balance For Invoice No." + Convert.ToString(lblGrno.Text));
                    txtRcvdAmnt1.Text = "0.00"; txtRcvdAmnt.Focus();

                }
                NetAmntCal();
            }
        }

        protected void txt_txtTdsAmnt(object sender, EventArgs e)
        {
            TextBox txtTdsAmnt = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtTdsAmnt.Parent.Parent;
            Label lblGrno = (Label)currentRow.FindControl("lblGrno");
            TextBox txtRcvdAmnt = (TextBox)currentRow.FindControl("txtRcvdAmnt");
            TextBox txtDebitNote = (TextBox)currentRow.FindControl("txtDebitNote");
            TextBox txtTdsAmnt1 = (TextBox)currentRow.FindControl("txtTdsAmnt");
            Label lblCurBal = (Label)currentRow.FindControl("lblCurBal");
            TextBox txtRefNo = (TextBox)currentRow.FindControl("txtRefNo");
            if (txtTdsAmnt1.Text == "")
            {
                txtTdsAmnt1.Text = "0.00";
            }
            else
            {
                txtTdsAmnt1.Text = Convert.ToDouble(txtTdsAmnt1.Text).ToString("N2");
                if (Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt.Text) + Convert.ToDouble(txtDebitNote.Text) + Convert.ToDouble(txtTdsAmnt1.Text) + Convert.ToDouble(txtRefNo.Text)) > Convert.ToDouble(lblCurBal.Text))
                {
                    ShowMessageErr("Total Amount Cannot be greater than Cur. Balance For Invoice No." + Convert.ToString(lblGrno.Text));
                    txtTdsAmnt1.Text = "0.00"; txtTdsAmnt.Focus();

                }
                NetAmntCal();
                txtDebitNote.Focus();
            }
        }

        protected void txt_txtDebitNote(object sender, EventArgs e)
        {
            TextBox txtDebitNote = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtDebitNote.Parent.Parent;
            Label lblGrno = (Label)currentRow.FindControl("lblGrno");
            TextBox txtRcvdAmnt = (TextBox)currentRow.FindControl("txtRcvdAmnt");
            TextBox txtDebitNote1 = (TextBox)currentRow.FindControl("txtDebitNote");
            TextBox txtTdsAmnt = (TextBox)currentRow.FindControl("txtTdsAmnt");
            Label lblCurBal = (Label)currentRow.FindControl("lblCurBal");
            TextBox txtRefNo = (TextBox)currentRow.FindControl("txtRefNo");
            if (txtDebitNote1.Text == "")
            {
                txtDebitNote1.Text = "0.00";
            }
            else
            {
                txtDebitNote1.Text = Convert.ToDouble(txtDebitNote1.Text).ToString("N2");
                if (Convert.ToDouble(Convert.ToDouble(txtRcvdAmnt.Text) + Convert.ToDouble(txtDebitNote1.Text) + Convert.ToDouble(txtTdsAmnt.Text) + Convert.ToDouble(txtRefNo.Text)) > Convert.ToDouble(lblCurBal.Text))
                {
                    ShowMessageErr("Total Amount Cannot be greater than Cur. Balance For Invoice No." + Convert.ToString(lblGrno.Text));
                    txtDebitNote1.Text = "0.00"; txtDebitNote.Focus();

                }
                NetAmntCal();
                txtRcvdAmnt.Focus();
            }
        }

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
            ddlRcptTyp.Focus();
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            iFromCity = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
            intYearIdno = Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue));
            //this.BindMaxNo(iFromCity, intYearIdno);
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlFromCity.Focus();

        }

        protected void ddlPartyName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlPartyName.Focus();
        }
        #endregion

        #region Grid Events...
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtRcvdAmnt = (TextBox)e.Row.FindControl("txtRcvdAmnt");
                txtRcvdAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TextBox txtTDSAmnt = (TextBox)e.Row.FindControl("txtTdsAmnt");
                txtTDSAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TextBox txtDebitNote = (TextBox)e.Row.FindControl("txtDebitNote");
                txtDebitNote.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TextBox txtRefNo = (TextBox)e.Row.FindControl("txtRefNo");


                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dTotRecvdAmnt = dTotRecvdAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Recvd")); ;
                dTotCurBal = dTotCurBal + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "cur_Bal"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblFAmount");
                lblAmount.Text = dblNetAmnt.ToString("N2");
                Label lblFTotRecvd = (Label)e.Row.FindControl("lblFTotRecvd");
                lblFTotRecvd.Text = dTotRecvdAmnt.ToString("N2");
                Label lblFTotCurBal = (Label)e.Row.FindControl("lblFTotCurBal");
                lblFTotCurBal.Text = dTotCurBal.ToString("N2");
            }
        }
        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            lnkbtnClear.OnClientClick = "openModal('gr_details_form')";
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();
        }
        protected void grdGrdetals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGrdetals.PageIndex = e.NewPageIndex;
            DtGrdetail = (DataTable)ViewState["DtGrdetail"];
            grdGrdetals.DataSource = DtGrdetail;
            grdGrdetals.DataBind();
        }
        protected void grdGrdetals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkId");
                //bool AdminAppr = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Admin_Approval"));
                //if (AdminAppr == true)
                //    chk.Enabled = true;
                //else
                //    chk.Enabled = false;
            }
        }
        #endregion

        #region grdvchrGridEvent...
        protected void grdVchr_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void grdVchr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");

                vchrPendAmnt += Convert.ToDouble(txtAmount.Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtFTAmnt = (TextBox)e.Row.FindControl("txttotAmnt");
                txtFTAmnt.Text = vchrPendAmnt.ToString();
            }
        }
        #endregion

        #region TextChangedEvent....
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            hidtotVchrAmnt.Value = "0";
            for (int i = 0; i < grdVchr.Rows.Count; i++)
            {
                TextBox tb = (TextBox)sender;
                GridViewRow gvr = (GridViewRow)tb.Parent.Parent;
                int rowindex = gvr.RowIndex;
                double tot = 0;
                string a = ((TextBox)grdVchr.Rows[i].FindControl("txtAmount")).Text;
                if (string.IsNullOrEmpty(a) == false)
                {
                    if (Convert.ToDouble(hidrowindex.Value) > Convert.ToDouble(hidtotVchrAmnt.Value))
                    {
                        tot = Convert.ToDouble(a.ToString()) + Convert.ToDouble(hidtotVchrAmnt.Value);
                        hidtotVchrAmnt.Value = tot.ToString("N2");
                    }
                    else
                    {
                        ShowMessageErr("Amount cannot be greater than Total Amount");
                    }
                }
            }
            TextBox txt = grdVchr.FooterRow.FindControl("txttotAmnt") as TextBox;
            txt.Text = hidtotVchrAmnt.Value.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openpopup();", true);
        }
        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            //NetAmntCal();
        }
        #endregion

    }
}