using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.IO;
using System.Collections;
using System.Net;
using System.Web;
namespace WebTransport
{
    public partial class ChallanBulkUpdate : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dblTChallanAmnt = 0;
        Int64 ICAcnt_Idno = 0; Int64 IHireAcntIdno = 0; double dCommissionAmnt = 0;
        Int64 IntTDSAcntIdno = 0; Int64 IntDieselAcc_Idno = 0; DataTable AcntLinkDS; DataTable DsHire; 
        // string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtReceiptNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                
                //this.BindState();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityFrom.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);
                this.BindTruckNo();
                BindLorryParty();
                txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                lblTruckNo.Text = "Truck No.";
                ChlnBookingDAL obj = new ChlnBookingDAL();
                DateTime? dtfrom = null;
                DateTime? dtto = null;
                String challanNo = txtReceiptNo.Text;
                if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
                {
                    dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
                }
                if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
                {
                    dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
                }
                int cityfrom = Convert.ToInt32((drpCityFrom.SelectedIndex <= 0) ? "0" : drpCityFrom.SelectedValue);

                int TruckId = Convert.ToInt32((ddltruckNo.SelectedIndex <= 0) ? "0" : ddltruckNo.SelectedValue);

                Int64 UserIdno = 0;
                if (Convert.ToString(Session["Userclass"]) != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                DataTable lstGridData = null;
                lstGridData = obj.SearchChallnBulkUpdate(ApplicationFunction.ConnectionString(), "SelectChallan", Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue, Convert.ToInt64(ddlLorryParty.SelectedValue == "" ? "0" : ddlLorryParty.SelectedValue));
                obj = null;
                if (lstGridData != null && lstGridData.Rows.Count > 0)
                {
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Rows.Count;
                }
                Trantype();
                BindCity();
            }
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
        #endregion

        #region Functions...

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);

        }
        private void Trantype()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            var lst = obj.BindTrantype();
            ddlTranstype.DataSource = lst;
            ddlTranstype.DataTextField = "Tran_Name";
            ddlTranstype.DataValueField = "Tran_Idno";
            ddlTranstype.DataBind();
        }

        private void BindGrid()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            String challanNo = txtReceiptNo.Text;
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }
            int cityfrom = Convert.ToInt32((drpCityFrom.SelectedIndex <= 0) ? "0" : drpCityFrom.SelectedValue);

            int TruckId = Convert.ToInt32((ddltruckNo.SelectedIndex <= 0) ? "0" : ddltruckNo.SelectedValue);

            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            //var lstGridData=(IList)null;
            DataTable lstGridData = null;
            DataTable dtRcptDetl = new DataTable();
          
            if (ddlGrtype.SelectedValue == "GR")
            {
                lstGridData = obj.SearchChallnBulkUpdate(ApplicationFunction.ConnectionString(), "AllChallan4BulkUpdate", Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue, Convert.ToInt64(ddlLorryParty.SelectedValue == "" ? "0" : ddlLorryParty.SelectedValue));
            }
            else
            {
                //dtRcptDetl = obj.SelectGRRChlnDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue, Convert.ToString(ddlTranstype.SelectedValue));

            }
            #region Gr....
            obj = null;
            if ((lstGridData != null && lstGridData.Rows.Count > 0 ))
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ChlnDate", typeof(string));
                dt.Columns.Add("ChlnNo", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("NetAmnt", typeof(string));

                double TNet = 0;
                for (int i = 0; i < lstGridData.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ChlnDate"] = Convert.ToDateTime(lstGridData.Rows[i]["Chln_Date"]).ToString("dd-MM-yyyy");
                    dr["ChlnNo"] = Convert.ToString(lstGridData.Rows[i]["Chln_No"]);
                    dr["LorryNo"] = Convert.ToString(lstGridData.Rows[i]["Lorry_No"]);
                    dr["FromCity"] = Convert.ToString(lstGridData.Rows[i]["FromCity"]);
                    dr["NetAmnt"] = Convert.ToDouble(lstGridData.Rows[i]["Net_Amnt"]).ToString("N2");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(lstGridData.Rows[i]["Net_Amnt"]);
                    if (i == lstGridData.Rows.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["ChlnDate"] = "";
                        drr["ChlnNo"] = "";
                        drr["LorryNo"] = "";
                        drr["FromCity"] = "Total";
                        drr["NetAmnt"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Rows.Count;

                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Rows.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(lstGridData.Rows[i]["Net_Amnt"]);
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
           else if ((dtRcptDetl != null && dtRcptDetl.Rows.Count > 0))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ChlnDate", typeof(string));
                dt.Columns.Add("ChlnNo", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("NetAmnt", typeof(string));

                double TNet = 0;
                for (int i = 0; i < dtRcptDetl.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ChlnDate"] = Convert.ToDateTime(dtRcptDetl.Rows[i]["Chln_Date"]).ToString("dd-MM-yyyy");
                    dr["ChlnNo"] = Convert.ToString((dtRcptDetl.Rows[i]["Chln_No"]));
                    dr["LorryNo"] = Convert.ToString((dtRcptDetl.Rows[i]["Lorry_No"]));
                    dr["FromCity"] = Convert.ToString((dtRcptDetl.Rows[i]["FromCity"]));
                    dr["NetAmnt"] = Convert.ToDouble((dtRcptDetl.Rows[i]["Net_Amnt"])).ToString("N2");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble((dtRcptDetl.Rows[i]["Net_Amnt"]));
                    if (i == dtRcptDetl.Rows.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["ChlnDate"] = "";
                        drr["ChlnNo"] = "";
                        drr["LorryNo"] = "";
                        drr["FromCity"] = "Total";
                        drr["NetAmnt"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                grdMain.DataSource = dtRcptDetl;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + dtRcptDetl.Rows.Count;

                Double TotalNetAmount = 0;

                for (int i = 0; i < dtRcptDetl.Rows.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble((dtRcptDetl.Rows[i]["Net_Amnt"]));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dtRcptDetl.Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
             else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
            #endregion
        }

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ChallanBooking.xls"));
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
            Response.End();
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
        private void SetDate()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            txtReceiptDatefrom.Text = Convert.ToString(list.GetValue(0));
            txtReceiptDateto.Text = Convert.ToString(list.GetValue(1));
        }
        private void BindCity()
        {
            try
            {
                CityMastDAL obj = new CityMastDAL();
                var lst = obj.SelectCityCombo();
                obj = null;
                drpCityFrom.DataSource = lst;
                drpCityFrom.DataTextField = "City_Name";
                drpCityFrom.DataValueField = "City_Idno";
                drpCityFrom.DataBind();
                drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));
            
                BindDropdownDAL obj2 = new BindDropdownDAL();
                var ToCity = obj2.BindAllToCity();
                obj2 = null;
            }
            catch (Exception Ex)
            {
            }

        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindTruckNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddltruckNo.DataSource = lst;
                ddltruckNo.DataTextField = "Lorry_No";
                ddltruckNo.DataValueField = "Lorry_Idno";
                ddltruckNo.DataBind();
                ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        private void BindLorryParty()
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var senderLst = obj.BindSender();
                obj = null;
                ddlLorryParty.DataSource = senderLst;
                ddlLorryParty.DataTextField = "Acnt_Name";
                ddlLorryParty.DataValueField = "Acnt_Idno";
                ddlLorryParty.DataBind();
                ddlLorryParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            catch (Exception Ex)
            {
            }
        }

        public bool SendSMS(string Mobile, string msg)
        {
            if (Mobile != String.Empty && msg != String.Empty)
                try
                {
                    WebClient objWebClient;
                    string sBaseURL;
                    Stream objStreamData;
                    StreamReader objReader;
                    string sResult;
                    objWebClient = new WebClient();
                    DueAlignRepDAL obj = new DueAlignRepDAL();
                    var Comp = obj.SelectUserPref();
                    string UserName = Convert.ToString(DataBinder.Eval(Comp[0], "UserName"));
                    string Password = Convert.ToString(DataBinder.Eval(Comp[0], "Password"));
                    string SenderID = Convert.ToString(DataBinder.Eval(Comp[0], "SenderID"));
                    string ProfileID = Convert.ToString(DataBinder.Eval(Comp[0], "ProfileID"));
                    string AuthType = Convert.ToString(DataBinder.Eval(Comp[0], "AuthType"));
                    string AuthKey = Convert.ToString(DataBinder.Eval(Comp[0], "AuthKey"));
                    //string UserName = "cogximsms";
                    //string Password = "teamcogximsms";//This may vary api to api. like ite may be password, secrate key, hash etc
                    //string SenderID = "Cogxim";
                    sBaseURL = "http://globesms.in/sendhttp.php?user=" + UserName + "&password=" + Password + "&authkey=" + AuthKey + "&type=" + AuthType + "&mobiles=91" + Mobile + "&message=" + HttpUtility.UrlEncode(msg) + "&sender=" + SenderID + "&route=1";
                    objStreamData = objWebClient.OpenRead(sBaseURL);
                    objReader = new StreamReader(objStreamData);
                    sResult = objReader.ReadToEnd();
                    objStreamData.Close();
                    objReader.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            else
                return false;
        }

        private string GetMsg(long iTrackIdno)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            String SMS = obj.CreateSMSByTrackIdno(ApplicationFunction.ConnectionString(), iTrackIdno);
            return SMS;
        }

        private string GetMobileNumbers(Int64 iTrackIdno)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            String strMobileNo = obj.GetPartyMobileNoByTrackIdno(ApplicationFunction.ConnectionString(), iTrackIdno);
            return strMobileNo;
        }

        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdupdate")
            {
                Int32 rowsUpdated = 0;
                Int64 chlnidno = 0;
                Double commissionAmnt = 0;
                Double tdsAmnt = 0;

                string[] arg = e.CommandArgument.ToString().Split(';');
                if (arg[0] != null && arg[1] != null)
                {
                    int index = Convert.ToInt16(arg[0]);
                    chlnidno = Convert.ToInt64(arg[1]);
                    
                    HiddenField hidIsUpdateComm = (HiddenField)grdMain.Rows[index].FindControl("hidIsUpdateComm");
                    HiddenField hidIsUpdateTds = (HiddenField)grdMain.Rows[index].FindControl("hidIsUpdateTds");
                    if ((hidIsUpdateTds != null && hidIsUpdateTds.Value.Trim().ToLower() == "true") || (hidIsUpdateComm != null && hidIsUpdateComm.Value.Trim().ToLower() == "true"))
                    {
                        TextBox txtTDSAmnt = ((TextBox)grdMain.Rows[index].FindControl("txtTDSAmnt"));
                        TextBox txtCommissionAmnt = ((TextBox)grdMain.Rows[index].FindControl("txtCommissionAmnt"));
                        commissionAmnt = Convert.ToDouble(txtCommissionAmnt.Text == "" ? "0" : txtCommissionAmnt.Text);
                        tdsAmnt = Convert.ToDouble(txtTDSAmnt.Text == "" ? "0" : txtTDSAmnt.Text);
                        if (this.UpdateChallan(chlnidno, commissionAmnt, tdsAmnt))
                        {
                            rowsUpdated++;
                            if (rowsUpdated > 0)
                            {
                                PostIntoAccount(chlnidno, "GR");
                                ShowMessage("Challan number " + chlnidno + " has been updated with TDS amnt: " + tdsAmnt + " and Commission amnt: " + commissionAmnt);
                                BindGrid();
                            }
                            else
                            {
                                ShowMessageErr("Challan(s) not updated.");
                            }
                        }
                    }
                }
            }
        }

        private void PostIntoAccount(long chlnidno, string GrType)
        {
            ChlnBookingDAL objChln = new ChlnBookingDAL();
            tblChlnBookHead chlnBookhead = objChln.selectHead(chlnidno, GrType);

            if (this.RecPostIntoAccounts(Convert.ToDouble(chlnBookhead.Adv_Amnt), Convert.ToInt32(chlnidno), "CBU", 0, 0, 0, 0, 0, Convert.ToInt32(chlnBookhead.Year_Idno), Convert.ToInt32(chlnBookhead.Truck_Idno), Convert.ToString(chlnBookhead.Inst_Dt), (string.IsNullOrEmpty(chlnBookhead.Inst_No.ToString()) ? "0" : Convert.ToString(chlnBookhead.Inst_No)), (string.IsNullOrEmpty(chlnBookhead.Driver_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Driver_Idno)), Convert.ToDateTime(chlnBookhead.Chln_Date).ToString("dd-MM-yyyy"), Convert.ToInt32(chlnBookhead.Chln_No), (string.IsNullOrEmpty(chlnBookhead.RcptType_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.RcptType_Idno)), (string.IsNullOrEmpty(chlnBookhead.Bank_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Bank_Idno)), Convert.ToDouble(chlnBookhead.Gross_Amnt), Convert.ToDouble(chlnBookhead.Commsn_Amnt), Convert.ToDouble(chlnBookhead.TDSTax_Amnt), (string.IsNullOrEmpty(chlnBookhead.Diesel_Amnt.ToString()) ? 0.00 : Convert.ToDouble(chlnBookhead.Diesel_Amnt))) == true)
            {
                //   tScope.Complete(); tScope.Dispose();
                objChln.UpdateIsPosting(chlnidno);
            }
        }

        private bool RecPostIntoAccounts(double AdvAmount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno, Int32 TruckIdno, string InstDate, string InstNo, Int32 DriverIdno, string strDate, Int32 intChlnNo, Int32 intRcptType, Int32 intCustBIdno, double dGrossAmnt, double dCommissionAmnt, double dTdsAmnt, double dDiesel)
        {
            #region Variables Declaration...
            Int64 intVchrIdno = 0; Int64 intValue = 0; Int32 IAcntIdno = 0; DateTime? dtBankDate = null;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            ChlnBookingDAL objAcnt = new ChlnBookingDAL();
            BindDropdownDAL objDal = new BindDropdownDAL();
            #endregion
            #region Start Ac/Posting.........
            AcntLinkDS = objAcnt.DtAcntDS(ApplicationFunction.ConnectionString());
            DsHire = objAcnt.DsHireAcnt(ApplicationFunction.ConnectionString());
            DataSet dsLD = objDal.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", TruckIdno, strDate);
            if (dsLD != null && dsLD.Tables.Count > 0 && dsLD.Tables[0].Rows.Count > 0)
            {
                Int32 intLtype = string.IsNullOrEmpty(dsLD.Tables[0].Rows[0]["Lorry_Type"].ToString()) ? 0 : Convert.ToInt32(dsLD.Tables[0].Rows[0]["Lorry_Type"]);
                string strLorryNo = Convert.ToString(dsLD.Tables[0].Rows[0]["Lorry_No"]);
                Int32 PartyIdno = Convert.ToInt32(dsLD.Tables[0].Rows[0]["Acnt_Idno"]);
                IAcntIdno = PartyIdno; 
                #region Account link Validations...

                if (AcntLinkDS == null || AcntLinkDS.Rows.Count <= 0)
                {
                    ShowMessageErr("Account link is not defined. Kindly define.");
                    return false;
                }

                ICAcnt_Idno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["CAcnt_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["CAcnt_Idno"]));
                if (ICAcnt_Idno <= 0)
                {
                    ShowMessageErr("Commission Account is not defined. Kindly define.");
                    return false;
                }
                if (DsHire == null || DsHire.Rows.Count <= 0)
                {
                    ShowMessageErr("Transport Account is not defined. Kindly define.");
                    return false;
                }
                else
                {
                    IHireAcntIdno = Convert.ToInt32(DsHire.Rows[0]["HireAccountID"]);
                }
                IntTDSAcntIdno = Convert.ToInt32(Convert.ToString(AcntLinkDS.Rows[0]["TDS_Idno"]) == "" ? 0 : Convert.ToInt32(AcntLinkDS.Rows[0]["TDS_Idno"]));
                if (IntTDSAcntIdno <= 0)
                {
                    ShowMessageErr("TDS Account is not defined. Kindly define.");
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
                        "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo + "Commission Posting",
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
                            "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo + "Commission Posting",
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
                                      "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo + "Commission Posting",
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

                if (dTdsAmnt > 0 && IAcntIdno>0)
                {
                    if (intValue > 0)
                    {
                        Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, YearIdno);
                        intValue = objclsAccountPosting.InsertInVchrHead(
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(strDate)), 4, 0, "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo + "TDS Posting",
                        true, 0, strDocType, 0, 0, Convert.ToInt64(intChlnNo), ApplicationFunction.GetIndianDateTime().Date, VchrNo, 0,
                        YearIdno, 0, intUserIdno);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;

                            #region TDS Account Posting .........

                            intValue = 0;
                            /*Insert In VchrDetl*/
                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno, Convert.ToInt64(IAcntIdno), "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo +"TDS Posting",
                            dTdsAmnt, Convert.ToByte(2), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                       intVchrIdno, IntTDSAcntIdno, "Challan. No: " + Convert.ToString(intChlnNo) + " Challan. Date: " + strDate + " Lorry: " + strLorryNo + "TDS Posting",
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

                #endregion
                objclsAccountPosting = null;
                return true;
            }
            else
            {
                objclsAccountPosting = null;
                return true;
            }
            #endregion

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                double dblChallanAmnt = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    ChlnBookingDAL obj = new ChlnBookingDAL();
                    Int64 ChlnIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Chln_Idno"));
                    string GRType = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Gr_Type"));
                    obj = null;
                    dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                    dblTChallanAmnt = dblChallanAmnt + dblTChallanAmnt;
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                    lblTChallanAmnt.Text = dblTChallanAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void lnkUpdateAll_OnClick(object sender, EventArgs e)
        {
            Int32 rowsUpdated = 0;
            Int64 chlnidno = 0;
            Double commissionAmnt  = 0;
            Double tdsAmnt = 0;
            try
            {
                if (grdMain.Rows.Count > 0)
                {
                    for (int i = 0; i <= grdMain.Rows.Count - 1; i++)
                    {
                        HiddenField hidIsUpdateTds = (HiddenField)grdMain.Rows[i].FindControl("hidIsUpdateTds");
                        HiddenField hidIsUpdateComm = (HiddenField)grdMain.Rows[i].FindControl("hidIsUpdateComm");
                        if ((hidIsUpdateTds != null && hidIsUpdateTds.Value.Trim().ToLower() == "true") || (hidIsUpdateComm != null && hidIsUpdateComm.Value.Trim().ToLower() == "true"))
                        {
                            HiddenField hidChlnidno = ((HiddenField)grdMain.Rows[i].FindControl("hidChlnidno"));
                            TextBox txtTDSAmnt = ((TextBox)grdMain.Rows[i].FindControl("txtTDSAmnt"));
                            TextBox txtCommissionAmnt = ((TextBox)grdMain.Rows[i].FindControl("txtCommissionAmnt"));
                            chlnidno = Convert.ToInt32(hidChlnidno.Value == "" ? "0" : hidChlnidno.Value);
                            commissionAmnt = Convert.ToDouble(txtCommissionAmnt.Text == "" ? "0" : txtCommissionAmnt.Text);
                            tdsAmnt = Convert.ToDouble(txtTDSAmnt.Text == "" ? "0" : txtTDSAmnt.Text);

                            if (this.UpdateChallan(chlnidno, commissionAmnt, tdsAmnt))
                            {
                                PostIntoAccount(chlnidno, "GR");
                                rowsUpdated++;
                            }
                        }
                    }
                    if (rowsUpdated > 0)
                    {
                        ShowMessage(rowsUpdated + " challan(s) updated successfully.");
                        BindGrid();
                    }
                    else
                    {
                        ShowMessageErr("Challan(s) not updated.");
                    }
                }
                else
                {
                    ShowMessageErr("No records found to update.");
                }
            }
            catch (Exception ex)
            {
                ShowMessageErr("Something went wrong. Please try again later.");
            }
        }

        public bool UpdateChallan(Int64 chlnidno, double commissionAmnt, double tdsAmnt)
        {
            ChlnBookingDAL objChallan = new ChlnBookingDAL();
            if (chlnidno > 0)
            {
                if (objChallan.BulkUpdateChallan(ApplicationFunction.ConnectionString(), chlnidno, commissionAmnt, tdsAmnt))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Control Events...
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }
        #endregion

        #region IMport excel....
        private void ExportGridView()
        {
            //string attachment = "attachment; filename=Report.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.Columns[1].Visible = false;
            //grdMain.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
        }
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Label))
                {
                    l.Text = (gv.Controls[i] as Label).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[1].Visible = false;
        }
        #endregion

        #region Dropdown Event..
        protected void ddlGrtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            if (ddlGrtype.SelectedValue == "GRR")
            {
                ddlTranstype.Enabled = true;
                if (lst.Count > 0)
                {
                    ddltruckNo.DataSource = lst;
                    ddltruckNo.DataTextField = "Lorry_No";
                    ddltruckNo.DataValueField = "Lorry_Idno";
                    ddltruckNo.DataBind();
                    ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            else
            {
                ddlTranstype.SelectedIndex = 0;
                ddlTranstype.Enabled = false;
               

                obj = null;
                if (lst.Count > 0)
                {
                    ddltruckNo.DataSource = lst;
                    ddltruckNo.DataTextField = "Lorry_No";
                    ddltruckNo.DataValueField = "Lorry_Idno";
                    ddltruckNo.DataBind();
                    ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }

            //lblTruckNo.Text = "Truck No.";
        }
        protected void ddlTranstype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTranstype.SelectedValue == "0")
            {
                BindTruckNo();
                lblTruckNo.Text = "Truck No.";
            }
            else if (ddlTranstype.SelectedValue == "1")
            {
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var MiscList = obj.BindTransportaion(Convert.ToInt64(ddlTranstype.SelectedValue));
                ddltruckNo.DataSource = MiscList;
                ddltruckNo.DataTextField = "Misc_Name";
                ddltruckNo.DataValueField = "Misc_Idno";
                ddltruckNo.DataBind();
                ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                lblTruckNo.Text = "Flight.";

            }
            else if (ddlTranstype.SelectedValue == "2")
            {
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var MiscList = obj.BindTransportaion(Convert.ToInt64(ddlTranstype.SelectedValue));
                ddltruckNo.DataSource = MiscList;
                ddltruckNo.DataTextField = "Misc_Name";
                ddltruckNo.DataValueField = "Misc_Idno";
                ddltruckNo.DataBind();
                ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                lblTruckNo.Text = "Train.";
            }
            else
            {
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var MiscList = obj.BindTransportaion(Convert.ToInt64(ddlTranstype.SelectedValue));
                ddltruckNo.DataSource = MiscList;
                ddltruckNo.DataTextField = "Misc_Name";
                ddltruckNo.DataValueField = "Misc_Idno";
                ddltruckNo.DataBind();
                ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                lblTruckNo.Text = "Bus.";
            }
        }
      
        #endregion
    }
}