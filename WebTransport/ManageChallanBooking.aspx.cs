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
using System.Collections.Generic;
namespace WebTransport
{
    public partial class ManageChallanBooking : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dblTChallanAmnt = 0;
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
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
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
                lstGridData = obj.searchBySP(ApplicationFunction.ConnectionString(), "SelectChallan", Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue);
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

            if (txttruck.Visible == true)
            {
                ddltruckNo.SelectedValue = hfTruckNoId.Value == "" ? "0" : hfTruckNoId.Value;
            }

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
                lstGridData = obj.searchBySP(ApplicationFunction.ConnectionString(), "SelectChallan", Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue);
            }
            else
            {
                dtRcptDetl = obj.SelectGRRChlnDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno, ddlGrtype.SelectedValue, Convert.ToString(ddlTranstype.SelectedValue));

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
                imgBtnExcel.Visible = true;
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
                imgBtnExcel.Visible = true;
            }
             else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
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

                ddlCity.DataSource = ToCity;
                ddlCity.DataTextField = "city_name";
                ddlCity.DataValueField = "city_idno";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
     
        private void PopulateChallanHead(long iChlnId)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            var data = obj.SelectChallanHeadById(ApplicationFunction.ConnectionString(), iChlnId);
            if (data.Rows.Count > 0)
            {
                lblChlnNo.Text = data.Rows[0]["Chln_No"].ToString();
                lblChlnDate.Text = data.Rows[0]["Chln_Date"].ToString();
                lblLorryno.Text = data.Rows[0]["Lorry_No"].ToString();
                lblPartyName.Text = data.Rows[0]["Acnt_Name"].ToString();
                lblLocFrm.Text = data.Rows[0]["From_City"].ToString();
                lblLocTo.Text = data.Rows[0]["To_City"].ToString();
            }
        }

        private void PopulateTruckLocation(Int64 iChlnId)
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            var data = obj.SelectLorryTrackingLoc(ApplicationFunction.ConnectionString(), iChlnId);
            if (data.Rows.Count > 0)
            {
                grdTruckPrevLoc.DataSource = data;
                grdTruckPrevLoc.DataBind();
            }
            else
            {
                grdTruckPrevLoc.DataSource = null;
                grdTruckPrevLoc.DataBind();
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
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("ChlnBooking.aspx?q=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                ChlnBookingDAL obj = new ChlnBookingDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString(),ddlGrtype.SelectedValue);
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtReceiptNo.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
            if (e.CommandName == "cmdAddTruckLoc")
            {
                Int64 iChlnId = 0;
                hidChlnidno.Value = e.CommandArgument.ToString();
                if (hidChlnidno.Value != "")
                {
                    iChlnId = Convert.ToInt64(e.CommandArgument.ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true);
                    //Show challan details
                    PopulateChallanHead(iChlnId);
                    PopulateTruckLocation(iChlnId);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true);
            }
        }

        protected void grdTruckPrevLoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                ViewState["Edit"] = true;
                Int64 iTrackIdno = Convert.ToInt64((e.CommandArgument == null ? "0" : e.CommandArgument).ToString());
                hidTrackIdno.Value = iTrackIdno.ToString();
                ChlnBookingDAL obj = new ChlnBookingDAL();
                var data = obj.SelectLorryTrackingLocByTrackIdno(ApplicationFunction.ConnectionString(), iTrackIdno);
                if (data.Rows.Count > 0)
                {
                    ddlCity.SelectedValue = data.Rows[0]["City_Idno"].ToString();
                    txtTruckCurrDate.Text = data.Rows[0]["Track_Date"].ToString();
                    txtTruckCurrTime.Text = data.Rows[0]["Track_Time"].ToString();
                    chkTrackingSMS.Checked = Convert.ToBoolean((data.Rows[0]["SMS_Sent"] == null ? "false" : data.Rows[0]["SMS_Sent"]).ToString());
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                Int64 iTrackIdno = Convert.ToInt64((e.CommandArgument == null ? "0" : e.CommandArgument).ToString());
                hidTrackIdno.Value = iTrackIdno.ToString();
                ChlnBookingDAL obj = new ChlnBookingDAL();
                Int32 data = obj.DeleteLorryTrackingLocByTrackIdno(ApplicationFunction.ConnectionString(), iTrackIdno);
                if (data < 0)
                {
                    //MESSAAGE
                    this.ShowMessage("Tracking detail deleted.");
                }
                else
                {
                    //MESSAAGE
                    this.ShowMessageErr("Error deleting tracking detail.");
                }
            }
            else if (e.CommandName == "cmdSendSMS")
            {
                Int64 iTrackIdno = Convert.ToInt64((e.CommandArgument == null ? "0" : e.CommandArgument).ToString());
                string strMobileNo = GetMobileNumbers(iTrackIdno);
                if(SendSMS(strMobileNo, GetMsg(iTrackIdno)))
                    this.ShowMessage("Message sent to party.");
                else
                    this.ShowMessageErr("Error sending message.");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true);
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
                    if ((obj.CheckBilled(ChlnIdno, ApplicationFunction.ConnectionString(), GRType)) > 0)
                    {
                        LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                        lnkbtnDelete.Visible = false;
                        e.Row.ForeColor = System.Drawing.Color.Maroon;
                    }

                    if (ChlnIdno > 0)
                    {
                        var ChlnExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(ChlnIdno));
                        LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                        if (ChlnExist != null && ChlnExist.Count > 0)
                        {
                            lnkbtnDelete.Visible = false;
                        }
                        else
                        {
                            if (base.CheckUserRights(intFormId) == false)
                            {
                                Response.Redirect("PermissionDenied.aspx");
                            }
                            if (base.Delete == false)
                                lnkbtnDelete.Visible = false;
                            else
                                lnkbtnDelete.Visible = true;
                        }
                    }

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

        protected void AddTrackingLocation(object sender, EventArgs e)
        {
            if (ddlCity.SelectedValue == "0")
            { ddlCity.Focus(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true); return; }
            if (txtTruckCurrDate.Text == String.Empty)
            { txtTruckCurrDate.Focus(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true); return; }
            if (txtTruckCurrTime.Text == String.Empty)
            { txtTruckCurrTime.Focus(); ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true); return; }
            if (hidChlnidno.Value != "")
            {
                Int64 iChlnIdno = Convert.ToInt64(hidChlnidno.Value);
                Int64 returnStatus = 0;
                ChlnBookingDAL obj = new ChlnBookingDAL();
                tblLorryTrackLoc tblTrackLoc = new tblLorryTrackLoc();
                tblTrackLoc.City_Idno = Convert.ToInt64(ddlCity.SelectedValue);
                tblTrackLoc.Chln_Idno = iChlnIdno;
                tblTrackLoc.Track_Date = Convert.ToDateTime(txtTruckCurrDate.Text).Date;
                tblTrackLoc.Track_Time = Convert.ToDateTime(txtTruckCurrTime.Text).TimeOfDay;
                tblTrackLoc.User_Idno = 2;
                tblTrackLoc.Date_Added = DateTime.Now;
                tblTrackLoc.Date_Modified = DateTime.Now;
                tblTrackLoc.SMS_Sent = chkTrackingSMS.Checked;
                if (Convert.ToBoolean(ViewState["Edit"] == null ? "False" : ViewState["Edit"]))
                {
                    returnStatus = obj.UpdateTrackingLocation(tblTrackLoc, Convert.ToInt64(hidTrackIdno.Value == "" ? "0" : hidTrackIdno.Value));
                    if (chkTrackingSMS.Checked)
                    {
                        string strMobileNo = GetMobileNumbers(returnStatus);
                        SendSMS(strMobileNo, GetMsg(returnStatus));
                    }
                }
                else
                {
                    returnStatus = obj.SaveTrackingLocation(tblTrackLoc);
                    if (chkTrackingSMS.Checked)
                    {
                        string strMobileNo = GetMobileNumbers(returnStatus);
                        SendSMS(strMobileNo, GetMsg(returnStatus));
                    }
                }
                if (hidChlnidno.Value != "")
                {
                    PopulateTruckLocation(Convert.ToInt64(hidChlnidno.Value == "" ? "0" : hidChlnidno.Value));
                    ddlCity.SelectedValue = "0";
                    txtTruckCurrDate.Text = "";
                    txtTruckCurrTime.Text = "";
                    chkTrackingSMS.Checked = false;
                }
                if(returnStatus > 0)
                    this.ShowMessage("Tracking detail saved.");
                else
                    this.ShowMessageErr("Error saving tracking detail.");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "PopUpTrackTruck()", true);
        }

        //public string SMSSentDisable(bool IsSMSSent)
        //{
        //    if (IsSMSSent)
        //        return "Dis";
        //}
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
                    ddltruckNo.Visible = true;
                    ddlTranstype.Enabled = true;
                    txttruck.Visible = false;
                    //ddltruckNo.DataSource = lst;
                    //ddltruckNo.DataTextField = "Lorry_No";
                    //ddltruckNo.DataValueField = "Lorry_Idno";
                    //ddltruckNo.DataBind();
                    //ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            else
            {
                ddlTranstype.SelectedIndex = 0;
                ddlTranstype.Enabled = false;
               

                obj = null;
                if (lst.Count > 0)
                {
                    txttruck.Visible = true;
                    ddltruckNo.Visible = false;
                    //ddltruckNo.DataSource = lst;
                    //ddltruckNo.DataTextField = "Lorry_No";
                    //ddltruckNo.DataValueField = "Lorry_Idno";
                    //ddltruckNo.DataBind();
                    //ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
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
    }
}