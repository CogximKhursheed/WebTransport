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
    public partial class PaymentToOwn : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; DataTable dtPartName = new DataTable(); DataTable DtGrdetail = new DataTable();
        double dblNetAmnt = 0; Int32 iFromCity = 0, intYearIdno = 0; double dTotCurBal, dTotRecvdAmnt = 0;
        private int intFormId = 34; double dtotWt = 0, dtotwt = 0, dAmnt = 0, dTDS = 0, dShor = 0, dGross = 0, dAdv = 0, dBal = 0, dPay = 0;
        #endregion

        #region Page Enents...
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
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
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

                PaymentToOwnDAL objChlnPayDAL = new PaymentToOwnDAL();
                tblUserPref obj = objChlnPayDAL.selectuserpref();
                hidPrintType.Value = Convert.ToString(obj.PayChln_Print);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                ddlRcptType_SelectedIndexChanged(null, null);
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"])); GridChlnDetails(Convert.ToInt32(ddlChallan.SelectedValue));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnNew.Visible = true;
                    if ((string.IsNullOrEmpty(hidPrintType.Value) ? 0 : Convert.ToInt32(hidPrintType.Value)) == 1)
                    {
                        //lnkbtnPrintClick.Visible = true;
                        LinkButton1.Visible = false;
                    }
                    else
                    {
                        lnkbtnPrintClick.Visible = false;
                        //LinkButton1.Visible = true;
                    }
                }
                else
                {
                    lnkbtnPrintClick.Visible = false;
                    lnkbtnNew.Visible = false;
                    LinkButton1.Visible = false;
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
                DtTemp = CreateDt();
                PaymentToOwnDAL obj = new PaymentToOwnDAL();
                tblPayToOwnAcc objRGH = new tblPayToOwnAcc();
                objRGH.Rcpt_No = Convert.ToInt64(txtRcptNo.Text);
                objRGH.BaseCity_Idno = Convert.ToInt64(ddlFromCity.SelectedValue);
                objRGH.Rcpt_date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                objRGH.Chln_IdNo = Convert.ToInt64(ddlChallan.SelectedValue);
                objRGH.Driver_IdNo = Convert.ToInt64(hidDriverIdno.Value);
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

                objRGH.Remark = Convert.ToString(TxtRemark.Text);
                objRGH.Year_IdNo = Convert.ToInt32(ddldateRange.SelectedValue);
                objRGH.Amnt = Convert.ToDouble(txtAmount.Text.Trim());
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        value = obj.Insert(objRGH);
                    }
                    else
                    {

                        value = obj.Update(objRGH, Convert.ToInt32((Convert.ToString(hidid.Value) == "" ? 0 : Convert.ToInt32(hidid.Value))));
                    }
                    if (value > 0)  
                    {
                        if (this.PostIntoAccounts(Convert.ToDouble(txtAmount.Text), value, "PTD", 0, 0, 0, 0, 0, Convert.ToInt32(ddldateRange.SelectedValue)) == true)
                        {
                            obj.UpdateIsPosting(value);
                            if ((string.IsNullOrEmpty(hidid.Value) == false))
                            {
                                if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                                {
                                    ShowMessage("Record Update successfully");
                                    this.Clear();
                                    ddlFromCity_SelectedIndexChanged(null, null);
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Pay No. Already Exist");
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
                                    ddlFromCity_SelectedIndexChanged(null, null);
                                }
                                else if (value == -1)
                                {
                                    ShowMessageErr("Pay No. Already Exist");
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
                    else
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + Convert.ToString(hidid.Value) + "')", true);
                        return;
                    }
                }

                this.BindMaxNo(Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)));
        }

        private DataTable CreateDt()//Chln_Idno,Chln_No,Chln_Date,Fromcity_Idno,Truck_Idno,Driver_Idno,Challan_Amnt,Recvd_Amnt
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Chln_Idno", "String", "Chln_No", "String", "Chln_Date", "String", "Fromcity_Idno", "String", "Truck_Idno", "String", "Driver_Idno", "String",
                                                                   "Challan_Amnt", "String", "Recvd_Amnt", "String");
            return dttemp;
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
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            Int64 max = obj.GetMaxNo();
            obj = null;
            txtRcptNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PaymentToOwn.aspx");
        }

        protected void lnkimgbtnSearch_OnClick(object sender, EventArgs e)
        {

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
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            tblUserPref userpref = obj.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
        }


        private void BindMaxNo(Int32 FromCityIdno, Int32 YearId)
        {
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            Int64 MaxNo = obj.MaxNo(YearId, FromCityIdno, ApplicationFunction.ConnectionString());
            txtRcptNo.Text = Convert.ToString(MaxNo);
        }

        private void NetAmntCal()
        {
            double dblAmnt = 0;
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
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            DataTable lst = obj.FetchChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
            if (lst.Rows.Count > 0)
            {
                ddlChallan.DataSource = lst;
                ddlChallan.DataTextField = "chln_Detl";
                ddlChallan.DataValueField = "Chln_Idno";
                ddlChallan.DataBind();
                ddlChallan.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            
        }

        private void BindReceiptType()
        {
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            var RcptType = obj.BindRcptType(ApplicationFunction.ConnectionString());
            ddlRcptTyp.DataSource = RcptType;
            ddlRcptTyp.DataTextField = "ACNT_NAME";
            ddlRcptTyp.DataValueField = "Acnt_Idno";
            ddlRcptTyp.DataBind();
            ddlRcptTyp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCustBank()
        {
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            ddlCustmerBank.DataSource = obj.BindBank();
            ddlCustmerBank.DataTextField = "Acnt_Name";
            ddlCustmerBank.DataValueField = "Acnt_Idno";
            ddlCustmerBank.DataBind();
            ddlCustmerBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCityANDLocation()
        {
 
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
                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                }
            }
        }

        private void Populate(Int64 HeadId)
        {
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            tblPayToOwnAcc AmntGrhead = obj.selectHead(HeadId);
            ddldateRange.SelectedValue = Convert.ToString(AmntGrhead.Year_IdNo);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtRcptNo.Text = Convert.ToString(AmntGrhead.Rcpt_No);
            txtRcptNo.Visible = true; ddlFromCity.SelectedValue = Convert.ToString(AmntGrhead.BaseCity_Idno); BindPartName();
            txtDate.Text = Convert.ToDateTime(AmntGrhead.Rcpt_date).ToString("dd-MM-yyyy");
            //ddlDriverName.SelectedValue = Convert.ToString(AmntGrhead.Party_IdNo);
            ddlRcptTyp.SelectedValue = Convert.ToString(AmntGrhead.RcptType_Idno);
            ddlRcptType_SelectedIndexChanged(null, null);
            ddlChallan.SelectedValue = Convert.ToString(AmntGrhead.Chln_IdNo);
            ddlChallan_SelectedIndexChanged(null, null);
            ddlCustmerBank.SelectedValue = Convert.ToString(AmntGrhead.Bank_Idno);
            txtInstNo.Text = Convert.ToString(AmntGrhead.Inst_No);
            txtInstDate.Text = (Convert.ToString(AmntGrhead.Inst_Dt) == "") ? "" : Convert.ToDateTime(AmntGrhead.Inst_Dt).ToString("dd-MM-yyyy");
            TxtRemark.Text = Convert.ToString(AmntGrhead.Remark);
            txtAmount.Text = Convert.ToString(AmntGrhead.Amnt);

            ViewState["dt"] = DtTemp;


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
        }

        private void Clear()
        {
            hidid.Value = string.Empty;
            lnkbtnPrintClick.Visible = false;
            lnkbtnNew.Visible = false;
            txtCurrBal.Text = "0.00";
            ViewState["dt"] = null;
            DtTemp = null;
            ddlChallan.SelectedIndex = -1;
            TxtRemark.Text = "";
            txtAmount.Text = "0.00";
            hidid.Value = string.Empty;
            /// txtDate.Text = "";
            //txtRcptNo.Text = "";
            // ddldateRange.SelectedIndex = 0; ;
            // ddldateRange_SelectedIndexChanged(null, null);
            txtDriverName.Text = "";
            ddldateRange.Enabled = true;
            ddldateRange.SelectedIndex = 0;
            txtInstDate.Text = "";
            txtInstNo.Text = "";
            ddlRcptTyp.SelectedIndex = 0;
            ddlCustmerBank.SelectedIndex = 0; ddlRcptTyp.SelectedIndex = ddlCustmerBank.SelectedIndex = 0; txtInstNo.Text = "";
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
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            Int32 IAcntIdno = 0;
            // Int32 ILType = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue)); //Convert.ToInt32(clsDataAccessFunction.ExecuteScaler("select Lorry_type from LorryMast where Lorry_Idno=" + Convert.ToInt32(cmbTruckNo.SelectedValue) + "", Tran, Program.DataConn));
            //if (ILType == 1)
            //{
            //IAcntIdno = Convert.ToInt32(ddlDriverName.SelectedValue);
            //}
            //else
            //{
             IAcntIdno = Convert.ToInt32((string.IsNullOrEmpty((hidDriverIdno.Value)) ? "0" : hidDriverIdno.Value));
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
                //IAcntIdno = Convert.ToInt32(ddlDriverName.SelectedValue);
                IAcntIdno = 0;

                Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(2, 0, Convert.ToInt32(ddldateRange.SelectedValue));

                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())),
                1,
                Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue)),
                "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtDate.Text.Trim() + "Total Paid Amount :" + Convert.ToDouble(txtAmount.Text.Trim().Replace("'", "")) + "",
                true,
                0,
                strDocType,
                IAcntIdno,
                0,
                0,
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())),
                ((VchrNo == 0) ? 1 : VchrNo),
                0,
               YearIdno,
               0, 0);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;
                    intValue = 0; IAcntIdno = Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue));
                    
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    Convert.ToInt32((ddlRcptTyp.SelectedIndex == -1) ? 0 : Convert.ToInt32(ddlRcptTyp.SelectedValue)),
                   "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtDate.Text.Trim() + "Total Amount Paid :" + Convert.ToDouble(txtAmount.Text.Trim().Replace("'", "")) + "  ",
                    Amount,
                    Convert.ToByte(1),
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
                        Convert.ToInt32(hidDriverIdno.Value),
                     "Receipt No :" + Convert.ToString(txtRcptNo.Text) + " Receipt Date: " + txtDate.Text.Trim() + "Total Paid Received :" + Convert.ToDouble(txtAmount.Text.Trim().Replace("'", "")) + "  ",
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

        #region Control Events..............................

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
                txtDate.Focus();
            }

        }

        protected void ddlRcptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInstNo.Text = ""; rfvinstDate.Enabled = false; rfvinstno.Enabled = false; //rfvCusBank.Enabled = false; // lokesh, its remove b/c its not required
            txtInstNo.Enabled = false;
            txtInstDate.Enabled = false; ddlCustmerBank.Enabled = false;
            if (ddlRcptTyp.SelectedIndex > 0)
            {
                PaymentToOwnDAL obj = new PaymentToOwnDAL();
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
            this.BindPartName();
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

            DataSet PrtyAdd = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select AM.Address1,Am.Address2,CM.City_Name,SM.State_Name from acntmast AM inner join tblcitymaster CM on AM.City_idno=CM.City_Idno inner join tblstatemaster SM on CM.State_idno=SM.State_idno where AM.Acnt_Idno=" + hidDriverIdno.Value + "");
            PdfPTable tblprtyAdd = new PdfPTable(1);
            tblprtyAdd.WidthPercentage = 90;
            tblprtyAdd.DefaultCell.Border = Rectangle.NO_BORDER;
            if (PrtyAdd.Tables[0].Rows.Count > 0)
            {
                PdfPCell CellAdd = new PdfPCell(new Phrase(("Party Address  : " + Convert.ToString(PrtyAdd.Tables[0].Rows[0][0]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][1]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][02]) + "," + Convert.ToString(PrtyAdd.Tables[0].Rows[0][3])), bodyFont2));
                CellAdd.BorderWidth = 0;

                PdfPCell CellAdd1 = new PdfPCell(new Phrase((""), bodyFont2));
                CellAdd1.BorderWidth = 0;
                tblprtyAdd.AddCell(CellAdd);
                tblprtyAdd.AddCell(CellAdd1);
            }
            PdfPTable tblprty = new PdfPTable(3);
            tblprty.WidthPercentage = 90;


            PdfPCell cell1Text = new PdfPCell(new Phrase(("Pay No             : " + txtRcptNo.Text), bodyFont2));
            cell1Text.BorderWidth = 0;
            PdfPCell cell2Text = new PdfPCell(new Phrase(("Date       : " + txtDate.Text), bodyFont2));
            cell2Text.BorderWidth = 0;
            PdfPCell cell3Text = new PdfPCell(new Phrase(("From City : " + ddlFromCity.SelectedItem.Text), bodyFont2));
            cell3Text.BorderWidth = 0;
            PdfPCell cell4Text = new PdfPCell(new Phrase(("Driver Name     : " + txtDriverName.Text), bodyFont2));
            cell4Text.Colspan = 2;
            cell4Text.BorderWidth = 0;
            
            //PdfPCell cell6Text = new PdfPCell(new Phrase((""), bodyFont));
            //cell6Text.BorderWidth = 0;


            tblprty.AddCell(cell1Text);
            tblprty.AddCell(cell2Text);
            tblprty.AddCell(cell3Text);
            tblprty.AddCell(cell4Text);
            
            //tblprty.AddCell(cell6Text);
        }

        private void GridChlnDetails(int ChlnIdno)
        {
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            var lst = obj.SearchDetails(ChlnIdno);
            if (lst.Count > 0)
            {
                grdMain.DataSource = lst;
                grdMain.DataBind();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
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
            PaymentToOwnDAL obj = new PaymentToOwnDAL();
            string strSbillNo = String.Empty;

            DataTable dtRcptDetl = new DataTable(); DataRow Dr;
            dtRcptDetl = obj.SelectChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(ChlnIdno));
            ViewState["dt"] = dtRcptDetl;
            if (dtRcptDetl != null && dtRcptDetl.Rows.Count > 0)
            {
                ddlChallan.SelectedValue = Convert.ToString(ChlnIdno);
                ddlFromCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["FromCity_Idno"]);
                BindPartName();
                this.BindMaxNo(Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)));
                ddlChallan_SelectedIndexChanged(null,null);
            }
            
        }

        protected void ddlChallan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChallan.SelectedIndex > 0)
            {
                double db, cr, dbopbal, cropbal, totdb, totcr;
                db = cr = dbopbal = cropbal = totdb = totcr = 0;

                PaymentToOwnDAL obj = new PaymentToOwnDAL();
                var Driver = obj.DriverName(Convert.ToInt64(ddlChallan.SelectedValue));
                txtDriverName.Text = Convert.ToString(DataBinder.Eval(Driver[0], "Acnt_Name"));
                hidDriverIdno.Value = Convert.ToString(DataBinder.Eval(Driver[0], "Acnt_Idno"));

                //db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=2, @ACNTIDNO='" + Convert.ToString(hidDriverIdno.Value) + "',@YEARIDNO='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", true);
                //cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=1, @ACNTIDNO='" + Convert.ToString(hidDriverIdno.Value) + "',@YEARIDNO='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", true);
                dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spAcntOpen] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(hidDriverIdno.Value) + "',@YearIdno='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", true);
                cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spAcntOpen] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(hidDriverIdno.Value) + "',@YearIdno='" + Convert.ToInt32(ddldateRange.SelectedValue) + "'", true);

                db = obj.LedgerBal(ApplicationFunction.ConnectionString(), 2, Convert.ToInt32(hidDriverIdno.Value), Convert.ToInt32(ddldateRange.SelectedValue));
                cr = obj.LedgerBal(ApplicationFunction.ConnectionString(), 1, Convert.ToInt32(hidDriverIdno.Value), Convert.ToInt32(ddldateRange.SelectedValue));

                //dbopbal = obj.FetchLedgerOpCl(ApplicationFunction.ConnectionString(), "SelectOpBal", 2, Convert.ToString(hidDriverIdno.Value), Convert.ToInt32(ddldateRange.SelectedValue));
                //cropbal = obj.FetchLedgerOpCl(ApplicationFunction.ConnectionString(), "SelectOpBal", 1, Convert.ToString(hidDriverIdno.Value), Convert.ToInt32(ddldateRange.SelectedValue));

                totdb = db + dbopbal; totcr = cr + cropbal;
                if (totdb > totcr)
                {
                    txtCurrBal.Text = Convert.ToDouble(totdb - totcr).ToString("N2") + "  Dr.";
                }
                else if (totdb < totcr)
                {
                    txtCurrBal.Text = Convert.ToDouble(totcr - totdb).ToString("N2") + "  Cr.";
                }
                else
                    txtCurrBal.Text = "0.00  Dr.";

                //double Amnt = obj.PreviousPay(ApplicationFunction.ConnectionString(),Convert.ToInt64(ddlChallan.SelectedValue),Convert.ToInt64(hidDriverIdno.Value));
                //txtCurrBal.Text = Amnt.ToString("N2");
                //ddlChallan.Focus();
                GridChlnDetails(Convert.ToInt32(ddlChallan.SelectedValue));

            }
        }
    }
}
            #endregion