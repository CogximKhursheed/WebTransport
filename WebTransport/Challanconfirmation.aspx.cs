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
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class Challanconfirmation : Pagebase
    {
        #region Variable Declaration...
         double dtotAmnt = 0; double Dtotqty = 0; double DTAMNT = 0;
         double sper = 6;
         double cper = 6;
         double iper = 12;
        static FinYearA UFinYear = new FinYearA();
        // ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
        int iFinYrID;
        int totalQty = 0;
        DataTable DtTemp = new DataTable();
        DataTable DtTempToll = new DataTable();
        DataTable DtTempNew = new DataTable();
        DataTable DT = new DataTable();
        ChallanConfirmationDAL objDal = new ChallanConfirmationDAL();
        double dTotNetAmnt, dTotQty, dTotWeight = 0;
        private int intFormId = 29;
        DataTable AcntLinkDS;
        Int64 PostingPartyIdno = 0;
        double ShortageAmnt = 0.0;
        #endregion

        #region Page Load Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (base.CheckUserRights(intFormId) == false)
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            if (base.ADD == false)
            {
                lnkbtnSave.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                TextBox txtdelvDate = new TextBox();
                txtdelvDate = (TextBox)grdMain.FindControl("txtdelvDate");
                txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ChallanConfirmationDAL objChlnConf = new ChallanConfirmationDAL();
                tblUserPref objuserpref = objChlnConf.SelectUserPref();
                hidRateEdit.Value = Convert.ToBoolean(objuserpref.ShrtgRateChlnConf).ToString();
                hidPrintType.Value = Convert.ToString(objuserpref.InvPrint_Type);
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                ddlFromCity_SelectedIndexChanged(null, null);
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                //txtGrNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                rdbtnChallanDetail.Checked = false;
                rdbtnGrNo.Checked = true;
                ddlDateRange_SelectedIndexChanged(null, null);
                //RangeValidator1.MinimumValue = Convert.ToString(ApplicationFunction.mmddyyyy(hidmindate.Value));
                //RangeValidator1.MaximumValue = Convert.ToString(ApplicationFunction.mmddyyyy(hidmaxdate.Value));
                DtTempToll = CreateToll();
                ViewState["dt"] = DtTempToll;
             
            }
            Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            bool IsUpdate = false; string strAllIds = ""; Int32 iCount = 0, strgCount = 0, savedRecordCount = 0;
            List<tblChlnBookDetl> RgDlst = new List<tblChlnBookDetl>();
            DtTemp = CreateDt();
            if (grdMain.Rows.Count > 0)
            {
                
                foreach (GridViewRow row in grdMain.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkDelvrd");
                    TextBox txtdelvDate = (TextBox)row.FindControl("txtdelvDate");
                    CheckBox chkShrtg = (CheckBox)row.FindControl("chkShrtg");
                    TextBox txtRemrk = (TextBox)row.FindControl("txtRemrk");
                    TextBox txtFromKm = (TextBox)row.FindControl("txtFromKm");
                    TextBox txtToKm = (TextBox)row.FindControl("txtToKm");
                    TextBox txttotalKm = (TextBox)row.FindControl("txttotalKm");
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                    if ((chk.Checked) == true)
                    {
                        if (txtdelvDate.Text == "")
                        {
                            ShowMessageErr("Please enter  Deliverey Date");
                            txtdelvDate.Focus();
                            return;
                        }
                    }
                    if (txtRemrk.Text != "" || txtdelvDate.Text != "")
                    {
                        if ((chk.Checked) == false)
                        {
                            ShowMessageErr("Please Check  Delivered");
                            txtRemrk.Text = "";
                            txtdelvDate.Text = "";
                            chkShrtg.Checked = false;
                            return;
                        }
                    }
                    if ((chkShrtg.Checked) == true)
                    {
                        if ((chk.Checked) == false)
                        {
                            ShowMessageErr("Please Check  Delivered");
                            chkShrtg.Checked = false;
                            return;
                        }
                    }
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, (chk.Checked == true) ? true : false, (ApplicationFunction.mmddyyyy(txtdelvDate.Text)), txtRemrk.Text, (chkShrtg.Checked == true) ? true : false,
                                                      "", "", "", "", "", "", "", "");
                    bool bFlag = false; strAllIds = "";
                    iCount = 0;
                    for (int i = 0; i < DtTemp.Rows.Count; i++)
                    {
                        if ((Convert.ToBoolean(DtTemp.Rows[i]["Shrtg"]) == true))
                        {
                            strAllIds = strAllIds + Convert.ToString(DtTemp.Rows[i]["Gr_Idno"]) + ",";
                            bFlag = true;
                            iCount++;
                        }
                    }

                    if (strAllIds.Length != 0)
                    {
                        strAllIds = strAllIds.Substring(0, strAllIds.Length - 1);
                    }
                    ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
                    IsUpdate = obj.UpdateDlvryStatus(DtTemp);
                    Int64 GrId = 0;
                    if (rdbtnGrNo.Checked) { GrId = obj.GrIdno(Convert.ToString(txtGrNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlDateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue)), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString(), ddlGrtype.SelectedValue); }
                    else if (rdbtnChallanDetail.Checked) { GrId = Convert.ToInt64((DtTemp.Rows[i]["Gr_Idno"].ToString()) == "" ? "0" : DtTemp.Rows[i]["Gr_Idno"].ToString()); }
                    if ((iCount < 1) || (iCount == 1))
                    {
                        //obj.UpdateShortingForUpdate(Convert.ToInt32((drpChallanDetail.SelectedValue == null || drpChallanDetail.SelectedValue == "") ? hidChallanIdno.Value : drpChallanDetail.SelectedValue), ApplicationFunction.ConnectionString(), GrId, ddlGrtype.SelectedValue);
                         
                        //   obj.UpdateShortingForUpdate(Convert.ToInt32(hidChallanIdno.Value), ApplicationFunction.ConnectionString(), GrId, ddlGrtype.SelectedValue);
                        Clear();
                        hidModalStatus.Value = iCount.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "CloseModal();", true);
                    }

                    if (ddlGrtype.SelectedValue == "GR")
                    {
                        obj.UpdateGrHeadForKm(ApplicationFunction.ConnectionString(), GrId, Convert.ToDouble(txtFromKm.Text), Convert.ToDouble(txtToKm.Text), Convert.ToDouble(txttotalKm.Text));
                    }

                    if (iCount > 0)
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        if (chkRate.Checked)
                        {
                            dtRcptDetl = obj.GrShortageDetails(ApplicationFunction.ConnectionString(), "GrShortageDetailsNew", strAllIds, Convert.ToInt32(ddlDateRange.SelectedValue), ddlGrtype.SelectedValue);
                            ViewState["dtNew"] = dtRcptDetl;
                        }
                        else
                        {
                            dtRcptDetl = obj.GrShortageDetails(ApplicationFunction.ConnectionString(), "GrShortageDetails", strAllIds, Convert.ToInt32(ddlDateRange.SelectedValue), ddlGrtype.SelectedValue);
                            ViewState["dtNew"] = dtRcptDetl;
                        }
                        grdGrdetals.DataSource = null;
                        BindGrDetailGrid();
                        iCount = 0;
                    }
                    if (IsUpdate == true)
                    {
                        this.ClearAll();
                        savedRecordCount++;
                    }
                    obj = null;
                }
                if (savedRecordCount > 0)
                {
                    ShowMessage(savedRecordCount + " record Update successfully");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Toll", "openModal();", true);
                }
                else
                {
                    ShowMessageErr("Records not Updated");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "CloseModal();", true);
                }
                for (int j = 0; j < DtTemp.Rows.Count; j++)
                {
                    if ((Convert.ToBoolean(DtTemp.Rows[j]["Shrtg"]) == true))
                    {
                        strgCount++;
                    }
                }
                if (strgCount == 0)
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "CloseModal();", true);
                }

                DtTemp = CreateDt();
            }
            else
            {
                ShowMessageErr("please Enter details");
                DtTemp = CreateDt();
                return;
            }
        }
        protected void lnkbtnNewMain_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Challanconfirmation.aspx");
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                if (rdbtnGrNo.Checked)
                {
                    if ((txtGrNo.Text == "") || (txtGrNo.Text == "0"))
                    {
                        txtGrNo.Focus();
                        ShowMessageErr("please Enter Gr No.");
                        return;
                    }
                    string IntGRNo = string.Empty; Int64 GetChlnIdno = 0; Int64 FromCityIdno = 0;
                    IntGRNo = Convert.ToString(txtGrNo.Text.Trim() == "" ? "0" : txtGrNo.Text.Trim());
                    FromCityIdno = Convert.ToInt64(((ddlFromCity.SelectedIndex <= 0) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue)));

                    ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
                    DataTable dt = obj.SelectchlnDetlGRwise(ApplicationFunction.ConnectionString(), "SelectChlnIdno", IntGRNo, Convert.ToInt32(ddlDateRange.SelectedValue), FromCityIdno, ddlGrtype.SelectedValue);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.ChallanDetail();
                        //drpChallanDetail.SelectedValue = dt.Rows[0][0].ToString();
                        hidChallanIdno.Value = dt.Rows[0][0].ToString();
                        txtChlnDetailAutoComplete.Text = GetChallanDetailChlnIdno(hidChallanIdno.Value);
                        drpChallanDetail_SelectedIndexChanged(null, null);
                        //drpChallanDetail.Enabled = false;
                        lnktollNumber.Visible = true;
                       
                    }
                    else
                    {
                        Gridmainhead.DataSource = null;
                        grdMain.DataSource = null;
                        Gridmainhead.DataBind();
                        grdMain.DataBind();
                        ShowMessageErr("Record not found.");
                        lnktollNumber.Visible = false;
                    }
                }
                else
                {
                    SearchBytxtChallanDetail();
                }
            }
            else
            {
                ShowMessageErr("please  select From city");
                return;
            }

        }
        protected void lnkBtnSaveShortage_OnClick(object sender, EventArgs e)
        {
            //Run only when Challan Id not null
            if (hidChallanIdno.Value != String.Empty)
                if (grdGrdetals.Rows.Count > 0)
                {
                    DtTempNew = CreateDtNew();
                    foreach (GridViewRow row in grdGrdetals.Rows)
                    {
                        Label lblGrNo = (Label)row.FindControl("lblGrNo");
                        TextBox txtShortage = (TextBox)row.FindControl("txtShortage");
                        TextBox txtDiff = (TextBox)row.FindControl("txtDiff");
                        TextBox lblShortageAmnt = (TextBox)row.FindControl("lblShortageAmnt");
                        HiddenField hidShortageGrIdno = (HiddenField)row.FindControl("hidShortageGrIdno");
                        HiddenField hidGrDetlIdno = (HiddenField)row.FindControl("hidGrDetlIdno");
                        DropDownList ShrtgType = (DropDownList)row.FindControl("ddlShrtgType");
                        ShortageAmnt = ShortageAmnt + Convert.ToDouble(lblShortageAmnt.Text);
                        TextBox txtUnloadWeight = (TextBox)row.FindControl("txtUnloadWeight");
                        //"Gr_No", "String", "ShrtgType", "String", "shortage_Diff", "String", "shortage_Amount", "String", "GrDetl_Idno", "String", "Gr_Idno", "String"
                        ApplicationFunction.DatatableAddRow(DtTempNew, lblGrNo.Text, txtShortage.Text, txtDiff.Text, lblShortageAmnt.Text, hidGrDetlIdno.Value, hidShortageGrIdno.Value, ShrtgType.SelectedValue, txtUnloadWeight.Text);
                    }
                    ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
                    Int64 GrId = obj.GrIdno(Convert.ToString(txtGrNo.Text.Trim()), Convert.ToInt32(Convert.ToString(ddlDateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue)), Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue)), ApplicationFunction.ConnectionString(), ddlGrtype.SelectedValue);
                    //bool IsUpdate = obj.UpdateShortingForUpdate(Convert.ToInt32((drpChallanDetail.SelectedValue == null || drpChallanDetail.SelectedValue == "") ? hidChallanIdno.Value : drpChallanDetail.SelectedValue), ApplicationFunction.ConnectionString(), GrId, ddlGrtype.SelectedValue);
                    bool IsUpdate = obj.UpdateShortingForUpdate(Convert.ToInt32(hidChallanIdno.Value), ApplicationFunction.ConnectionString(), GrId, ddlGrtype.SelectedValue);
                    if (IsUpdate == true)
                    {
                        bool IsShortage = obj.UpdateShorting(DtTempNew, ApplicationFunction.ConnectionString(), ddlGrtype.SelectedValue);
                        if (IsShortage == true)
                        {
                            hidModalStatus.Value = "0";
                            ShowMessage("shortage Saved");
                            //-------------Posting--------------//
                            PostIntoAccounts(ShortageAmnt,Convert.ToInt64(hidChallanIdno.Value),"CCB",0,0,0,0,0,Convert.ToInt32(ddlDateRange.SelectedValue));
      
                            Clear();
                        }
                        else
                        {
                            ShowMessageErr("shortage Not Saved");

                        }
                    }
                    else
                    {
                        ShowMessageErr("shortage Not Saved");
                    }

                }
                else
                {
                    ShowMessageErr("please Enter details");
                    DtTemp = CreateDt();
                    return;
                }
            txtGrNo.Text = "";
        }
        private void ShortagePosting()
        {
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            //Clear();
            //drpChallanDetail_SelectedIndexChanged(null,null);
            ClearAll();
        }
        #endregion

        #region functions...
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
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void BindCity()
        {
            try
            {
                ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
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
            catch (Exception Ex)
            {
            }

        }
        private void GetChallanNo()
        {
            ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
            Int64 max = obj.GetMaxNo();
            obj = null;

        }
        private void ClearAll()
        {
            // this.GetChallanNo();
            //drpChallanDetail.SelectedIndex = 0;
            Gridmainhead.DataSource = null;
            Gridmainhead.DataBind();
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlFromCity_SelectedIndexChanged(null, null);
            //if (drpChallanDetail.Items.Count > 0)
            //{
            //    drpChallanDetail.SelectedIndex = 0;
            //}
            //ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
            //ddlFromCity_SelectedIndexChanged(null, null);
        }
        private void Clear()
        {
            Gridmainhead.DataSource = null;
            Gridmainhead.DataBind();
            grdMain.DataSource = null;
            grdMain.DataBind();

        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;

            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            //hidmindt.Value = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            hidmindt.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
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
        public void ChallanDetail()
        {
            BindChallan();
        }

        protected void GrType_electedIndexChanged(object sender, EventArgs e)
        {
            BindChallan();
        }

        private void BindChallan()
        {
            if (rdbtnChallanDetail.Checked)
            {
                ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
                DataTable lst = obj.FetchChallanDetail(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), ddlGrtype.SelectedValue);
                //if (lst.Rows.Count > 0)
                //{
                //    drpChallanDetail.DataSource = lst;
                //    drpChallanDetail.DataTextField = "chln_Detl";
                //    drpChallanDetail.DataValueField = "Chln_Idno";
                //    drpChallanDetail.DataBind();

                //}
                //drpChallanDetail.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
       
        public string GetChallanDetailChlnIdno(string ChlnIdno)
        {
            string challanDetail = String.Empty;
            string constr = ApplicationFunction.ConnectionString();
            ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
            DataTable dt = obj.FetchChallanDetailByChlnIdno(ApplicationFunction.ConnectionString(), Convert.ToInt32(ChlnIdno));

            if (dt != null && dt.Rows.Count > 0)
            {
                challanDetail = dt.Rows[0][0].ToString();
                ChallanNumber.Value = dt.Rows[0]["Chln_No"].ToString();
            }
            return challanDetail;
        }
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }
        private void BindGridToll()
        {
            grdmaintoll.DataSource = (DataTable)ViewState["dt"];
            grdmaintoll.DataBind();
        }

        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }
        private void BindGrDetailGrid()
        {
            grdGrdetals.DataSource = (DataTable)ViewState["dtNew"];
            grdGrdetals.DataBind();
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion

        #region Controls Events...

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                this.ChallanDetail();
                rdbtnGrNo_CheckedChanged(null, null);
                rdbtnChallanDetail_CheckedChanged(null, null);

            }
            ddlFromCity.Focus();

        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void rdbtnGrNo_CheckedChanged(object sender, EventArgs e)
        {
            txtChlnDetailAutoComplete.Text = String.Empty;
            txtGrNo.Enabled = true;
            //lnkbtnSearch.Visible = true; 
            txtChlnDetailAutoComplete.Enabled = true;
            if (rdbtnGrNo.Checked == true)
            {
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtGrNo.Enabled = true;
                    //lnkbtnSearch.Visible = true;
                    //rfvtxtGr.Enabled = true;
                    //rfvdrpChallanDetail.Enabled = false;
                    txtChlnDetailAutoComplete.Enabled = false;
                    Gridmainhead.DataSource = null;
                    Gridmainhead.DataBind();
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    Gridmainhead.DataSource = null;
                    Gridmainhead.DataBind();
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    //if (drpChallanDetail.Items.Count > 0)
                    //{
                    //    drpChallanDetail.SelectedIndex = 0;
                    //}
                }
                else
                {
                    ShowMessageErr("please  select From city");
                    return;

                }
            }
            else if (rdbtnChallanDetail.Checked == true)
            {
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtGrNo.Text = "";
                    txtGrNo.Enabled = false;
                    //rfvtxtGr.Enabled = false;
                    //rfvdrpChallanDetail.Enabled = true;
                    //lnkbtnSearch.Visible = false;
                    txtChlnDetailAutoComplete.Enabled = true;
                    //if (drpChallanDetail.Items.Count > 0)
                    //{
                    //    drpChallanDetail.SelectedIndex = 0;
                    //}
                }
                else
                {
                    ShowMessageErr("please  select From city");
                    return;

                }
            }
        }

        protected void rdbtnChallanDetail_CheckedChanged(object sender, EventArgs e)
        {
            //rfvtxtGr.Enabled = true;
            //rfvdrpChallanDetail.Enabled = true;
            txtGrNo.Enabled = true;
            //lnkbtnSearch.Visible = true;
            txtChlnDetailAutoComplete.Enabled = true;
            if (rdbtnChallanDetail.Checked == true)
            {
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtGrNo.Text = "";
                    txtGrNo.Enabled = false;
                    //lnkbtnSearch.Visible = false;
                    txtChlnDetailAutoComplete.Enabled = true;
                    Gridmainhead.DataSource = null;
                    Gridmainhead.DataBind();
                    grdMain.DataSource = null;
                    //rfvtxtGr.Enabled = false;
                    //rfvdrpChallanDetail.Enabled = true;
                    BindChallan();
                    grdMain.DataBind();
                    Gridmainhead.DataSource = null;
                    Gridmainhead.DataBind();
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    //if (drpChallanDetail.Items.Count > 0)
                    //{
                    //    drpChallanDetail.SelectedIndex = 0;
                    //}
                }
                else
                {
                    ShowMessageErr("please  select From city");
                    return;

                }
            }
            else if (rdbtnGrNo.Checked == true)
            {
                if (ddlFromCity.SelectedIndex > 0)
                {
                    txtGrNo.Enabled = true;
                    //lnkbtnSearch.Visible = true;
                    //if (drpChallanDetail.Items.Count > 0)
                    //{
                    //    drpChallanDetail.SelectedIndex = 0;
                    //}
                    txtChlnDetailAutoComplete.Enabled = false;
                    //rfvtxtGr.Enabled = true;
                    //rfvdrpChallanDetail.Enabled = false;
                }
                else
                {
                    ShowMessageErr("please  select From city");
                    return;

                }
            }
        }

        protected void drpChallanDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 drpChallan = 0;
            //if (drpChallanDetail.SelectedIndex > 0)
            //{
            //    drpChallan = Convert.ToInt32((drpChallanDetail.SelectedValue) == "" ? 0 : Convert.ToInt32(drpChallanDetail.SelectedValue));
            //}
            if (hidChallanIdno.Value != String.Empty)
            {
                drpChallan = Convert.ToInt32((hidChallanIdno.Value) == "" ? 0 : Convert.ToInt32(hidChallanIdno.Value));
            }
            else
            {
                ShowMessageErr("Please enter challan no.");
                return;
            }
            DataTable dt1;

            ChallanConfirmationDAL obj1 = new ChallanConfirmationDAL();
            DataTable dt = obj1.SelectchallanDetl(ApplicationFunction.ConnectionString(), "FillChlnDetMast", drpChallan, ddlGrtype.SelectedValue);
            
            if (dt.Rows.Count > 0)
            {
                PostingPartyIdno = Convert.ToInt64(dt.Rows[0]["PrtyIdno"]);
            }
            Gridmainhead.DataSource = dt;
            Gridmainhead.DataBind();

            if (rdbtnChallanDetail.Checked == true)
                dt1 = obj1.SelectchallanDetl(ApplicationFunction.ConnectionString(), "FillGRDetMast", drpChallan, ddlGrtype.SelectedValue);
            else
                dt1 = obj1.SelectchallanDetlGR(ApplicationFunction.ConnectionString(), "FillGRDetMast", drpChallan, Convert.ToString(txtGrNo.Text.Trim()), Convert.ToInt64(ddlFromCity.SelectedValue), ddlGrtype.SelectedValue);

            hidmindt.Value = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[0]["GR_Date"])) ? "" : Convert.ToString(Convert.ToDateTime(dt1.Rows[0]["GR_Date"]).ToString("dd-MM-yyyy"));
            grdMain.DataSource = dt1;
            grdMain.DataBind();
        }

        public void SearchBytxtChallanDetail()
        {
            Int32 drpChallan = 0;
            hidChallanIdno.Value = String.Empty;
        
            if (txtChlnDetailAutoComplete.Text == String.Empty)
            {
                ShowMessageErr("Please enter challan number.");
                return;
            }
            string strChallanNo = txtChlnDetailAutoComplete.Text;
            int num1;
            Int32 ChallanNo = 0;
            bool isInt = int.TryParse(strChallanNo, out num1);
            if (isInt == true)
            {
                ChallanNo = Convert.ToInt32(txtChlnDetailAutoComplete.Text);
                ChallanNumber.Value =Convert.ToString(ChallanNo);
            }
            else
            {
                try
                {
                    string newString = txtChlnDetailAutoComplete.Text.Replace(" ", "").Substring(0, txtChlnDetailAutoComplete.Text.Replace(" ", "").IndexOf('-'));
                    ChallanNo = newString == String.Empty ? 0 : Convert.ToInt32(newString);
                    ChallanNumber.Value = Convert.ToString(ChallanNo);
                }
                catch (Exception ex)
                {
                    ChallanNo = 0;
                }
            }
            
            DataTable dt1;

            ChallanConfirmationDAL obj1 = new ChallanConfirmationDAL();
            DataTable dtChallanDetl = obj1.GetChallanDetailByChlnno(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue), ChallanNo);

            if (dtChallanDetl.Rows.Count > 0)
            {
                drpChallan = Convert.ToInt32(dtChallanDetl.Rows[0][1]);
                hidChallanIdno.Value = drpChallan.ToString();
                txtChlnDetailAutoComplete.Text = dtChallanDetl.Rows[0][0].ToString();

                DataTable dt = obj1.SelectchallanDetl(ApplicationFunction.ConnectionString(), "FillChlnDetMast", drpChallan, ddlGrtype.SelectedValue);

                if (dt.Rows.Count > 0)
                {
                    PostingPartyIdno = Convert.ToInt64(dt.Rows[0]["PrtyIdno"]);
                }
                Gridmainhead.DataSource = dt;
                Gridmainhead.DataBind();
                if (rdbtnChallanDetail.Checked == true)
                    dt1 = obj1.SelectchallanDetl(ApplicationFunction.ConnectionString(), "FillGRDetMast", drpChallan, ddlGrtype.SelectedValue);
                else
                    dt1 = obj1.SelectchallanDetlGR(ApplicationFunction.ConnectionString(), "FillGRDetMast", drpChallan, Convert.ToString(txtGrNo.Text.Trim()), Convert.ToInt64(ddlFromCity.SelectedValue), ddlGrtype.SelectedValue);

                hidmindt.Value = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[0]["GR_Date"])) ? "" : Convert.ToString(Convert.ToDateTime(dt1.Rows[0]["GR_Date"]).ToString("dd-MM-yyyy"));
                grdMain.DataSource = dt1;
                grdMain.DataBind();
                lnktollNumber.Visible = true;
            }
            else
            {
                lnktollNumber.Visible = false;
                ShowMessageErr("Record not found.");
                return;
            }
        }
        private DataTable CreateToll()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Id", "String", "Toll_Idno", "String", "Toll_Name", "String",
                "GR_Idno", "String",
                "Chln_Idno", "String",
                "Toll_Amt", "String",
                "Ticket_No", "String"
                );

            return dttemp;
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Delivered", "String", "Delvry_Date", "String", "remark", "String", "Shrtg", "String", "Gr_No", "String", "Gr_Date", "String",
                                                                 "Recvr_Name", "String", "Sender_Name", "String", "Via_City", "String", "To_City", "String", "Qty", "String", "Tot_Weght", "String", "Amount", "String");
            return dttemp;
        }

        private DataTable CreateDtNew()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_No", "String", "ShrtgType", "String", "shortage_Diff", "String", "shortage_Amount", "String", "GrDetl_Idno", "String", "Gr_Idno", "String", "ShrtgTypeId", "String", "UnloadWeight", "String");
            return dttemp;
        }

        protected void txtDiff_changed(object sender, EventArgs e)
        {

            TextBox txtDiff = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtDiff.Parent.Parent;
            TextBox txtDiffrnce = (TextBox)currentRow.FindControl("txtDiff");
            if (txtDiffrnce.Text == "")
            {
                txtDiffrnce.Text = "0";
            }
            else
            {
                TextBox txtShrtage = (TextBox)currentRow.FindControl("txtShortage");
                Label lblRateType = (Label)currentRow.FindControl("lblRateType");
                DropDownList ddlShrtgType = (DropDownList)currentRow.FindControl("ddlShrtgType");
                Label lblQty = (Label)currentRow.FindControl("lblQty");
                Label lblWeghtKg = (Label)currentRow.FindControl("lblWeghtKg");
                TextBox lblShortageAmnt = (TextBox)currentRow.FindControl("lblShortageAmnt");
                Label lblRate = (Label)currentRow.FindControl("lblRate");
                HiddenField hidShrtgLimit = (HiddenField)currentRow.FindControl("hidShrtgLimit");
                HiddenField hidShrtgRate = (HiddenField)currentRow.FindControl("hidShrtgRate");
                HiddenField hidShrtgItemRate = (HiddenField)currentRow.FindControl("hidShrtgItemRate");

                // if (lblRateType.Text != "" && lblRateType.Text == "Rate")
                if (ddlShrtgType.SelectedItem.Text == "Rate")
                {
                    if (Convert.ToDouble(txtDiffrnce.Text) > Convert.ToDouble(lblQty.Text))
                    {
                        txtDiffrnce.Text = "0"; txtShrtage.Text = "0"; lblShortageAmnt.Text = "0.00";
                        ShowMessageErr("Receipt Shortage can't be Greater than qty");
                        return;
                    }
                    if (Convert.ToDouble(txtShrtage.Text) <= Convert.ToDouble(txtDiffrnce.Text))
                    {
                        if (lblQty.Text != "")
                        {
                            txtShrtage.Text = Convert.ToDouble(Convert.ToDouble(lblQty.Text.Trim()) - (Convert.ToDouble((txtDiffrnce.Text.Trim() == "") ? "0.00" : txtDiffrnce.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        if (lblQty.Text.Trim() != "")
                        {
                            txtShrtage.Text = Convert.ToDouble(Convert.ToDouble(lblQty.Text.Trim()) - (Convert.ToDouble((txtDiffrnce.Text.Trim() == "") ? "0.00" : txtDiffrnce.Text.Trim()))).ToString("N2");
                        }
                    }
                }
                else
                {
                    if (Convert.ToDouble(txtDiffrnce.Text.Trim()) > Convert.ToDouble(lblWeghtKg.Text.Trim()))
                    {
                        txtDiffrnce.Text = "0"; txtShrtage.Text = "0"; lblShortageAmnt.Text = "0.00";
                        ShowMessageErr("Receipt Shortage can't be Greater than Weight");
                        return;
                    }
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(txtDiffrnce.Text.Trim()))
                    {
                        if (lblWeghtKg.Text.Trim() != "")
                        {
                            txtShrtage.Text = Convert.ToDouble(Convert.ToDouble(lblWeghtKg.Text.Trim()) - (Convert.ToDouble((txtDiffrnce.Text.Trim() == "") ? "0.00" : txtDiffrnce.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        if (lblWeghtKg.Text.Trim() != "")
                        {
                            txtShrtage.Text = Convert.ToDouble(Convert.ToDouble(lblWeghtKg.Text.Trim()) - (Convert.ToDouble((txtDiffrnce.Text.Trim() == "") ? "0.00" : txtDiffrnce.Text.Trim()))).ToString("N2");
                        }
                    }
                }


                //if (lblRateType.Text.Trim() != "" && lblRateType.Text.Trim() == "Rate")
                if (ddlShrtgType.SelectedItem.Text.Trim() == "Rate")
                {

                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(hidShrtgLimit.Value))
                    {
                        if (hidShrtgItemRate.Value != "")
                        {
                            lblShortageAmnt.Text = Convert.ToDouble(Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        double bahda = Convert.ToDouble((Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))));
                        double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim())) - Convert.ToDouble(hidShrtgLimit.Value)) * (Convert.ToDouble((hidShrtgRate.Value == "") ? "0.00" : hidShrtgRate.Value)));

                        lblShortageAmnt.Text = (bahda + ShrtgRate).ToString("N2");
                    }

                }
                // else if (lblRateType.Text.Trim() != "" && lblRateType.Text.Trim() == "Weight")
                else if (ddlShrtgType.SelectedItem.Text.Trim() == "Weight")
                {
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(hidShrtgLimit.Value))
                    {

                        lblShortageAmnt.Text = Convert.ToDouble(Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                    }
                    else
                    {
                        double bahda = Convert.ToDouble((Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))));
                        double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim())) - Convert.ToDouble(hidShrtgLimit.Value)) * (Convert.ToDouble((hidShrtgRate.Value == "") ? "0.00" : hidShrtgRate.Value)));

                        lblShortageAmnt.Text = (bahda + ShrtgRate).ToString("N2");
                    }

                }
            }
            txtDiff.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }

        protected void txtShrtgLim_changed(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox text = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShrtgLim");
            HiddenField hid = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgLimit");
            hid.Value = text.Text.Trim();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        protected void txtShrtgRate_changed(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox txtShrtgRate = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShrtgRate");
            HiddenField hid = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgRate");
            hid.Value = txtShrtgRate.Text.Trim();

            TextBox txtShortage = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShortage");
            HiddenField hidShrtgRate = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgRate"); ;

            TextBox lblShortageAmnt = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("lblShortageAmnt");
            double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtgRate.Text.Trim() == "") ? "0.00" : txtShrtgRate.Text.Trim()))) * (Convert.ToDouble((txtShortage.Text == "") ? "0.00" : txtShortage.Text)));
            lblShortageAmnt.Text = ShrtgRate.ToString("N2");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        protected void txtShortage_changed(object sender, EventArgs e)
        {

            TextBox txtShortage = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)txtShortage.Parent.Parent;
            TextBox txtShrtage = (TextBox)currentRow.FindControl("txtShortage");
            Label lblRateType = (Label)currentRow.FindControl("lblRateType");
            DropDownList ddlShrtgType = (DropDownList)currentRow.FindControl("ddlShrtgType");
            Label lblQty = (Label)currentRow.FindControl("lblQty");
            Label lblWeghtKg = (Label)currentRow.FindControl("lblWeghtKg");
            TextBox txtDiffrnce = (TextBox)currentRow.FindControl("txtDiff");
            TextBox lblShortageAmnt = (TextBox)currentRow.FindControl("lblShortageAmnt");
            Label lblRate = (Label)currentRow.FindControl("lblRate");
            HiddenField hidShrtgLimit = (HiddenField)currentRow.FindControl("hidShrtgLimit");
            HiddenField hidShrtgRate = (HiddenField)currentRow.FindControl("hidShrtgRate");
            HiddenField hidShrtgItemRate = (HiddenField)currentRow.FindControl("hidShrtgItemRate");
            TextBox txtUnloadWeight = (TextBox)currentRow.FindControl("txtUnloadWeight");
            TextBox txtShrtgRate = (TextBox)currentRow.FindControl("txtShrtgRate");
            if (txtShrtage.Text.Trim() == "")
            {
                txtShrtage.Text = "0.00";
                lblShortageAmnt.Text = "0.00";
            }
            else
            {

                //if (lblRateType.Text.Trim() != "" && lblRateType.Text.Trim() == "Rate")
                if (ddlShrtgType.SelectedItem.Text.Trim() == "Rate")
                {
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) > Convert.ToDouble(lblQty.Text.Trim()))
                    {
                        txtShrtage.Text = "0"; txtDiffrnce.Text = "0"; lblShortageAmnt.Text = "0.00";
                        //ShowMessage("Shortage Qty can't be Greater than qty");

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop1", "openModal();", true);
                        return;
                    }
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(hidShrtgLimit.Value))
                    {
                        if (hidShrtgItemRate.Value != "")
                        {
                            lblShortageAmnt.Text = Convert.ToDouble(Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        //double bahda = Convert.ToDouble((Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))));
                        double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim())) - Convert.ToDouble(hidShrtgLimit.Value)) * (Convert.ToDouble((hidShrtgRate.Value == "") ? "0.00" : hidShrtgRate.Value)));

                        //lblShortageAmnt.Text = (bahda + ShrtgRate).ToString("N2");
                        lblShortageAmnt.Text = (ShrtgRate).ToString("N2");
                    }
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(txtDiffrnce.Text.Trim()))
                    {
                        if (txtDiffrnce.Text.Trim() != "")
                        {
                            txtDiffrnce.Text = Convert.ToDouble(Convert.ToDouble(lblQty.Text.Trim()) - (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        if (lblQty.Text.Trim() != "")
                        {
                            txtDiffrnce.Text = Convert.ToDouble(Convert.ToDouble(lblQty.Text.Trim()) - (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                }
                //else if (lblRateType.Text.Trim() != "" && lblRateType.Text.Trim() == "Weight")
                else if (ddlShrtgType.SelectedItem.Text.Trim() == "Weight")
                {

                    if (Convert.ToDouble(txtShrtage.Text.Trim()) > Convert.ToDouble(lblWeghtKg.Text.Trim()))
                    {
                        txtShrtage.Text = "0"; txtDiffrnce.Text = "0"; lblShortageAmnt.Text = "0.00";
                        //ShowMessageErr("Shortage Weight can't be Greater than Weight");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop1", "openModal();", true);
                        return;
                    }
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(hidShrtgLimit.Value))
                    {

                        lblShortageAmnt.Text = Convert.ToDouble(Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                    }
                    else
                    {
                        //double bahda = Convert.ToDouble((Convert.ToDouble(hidShrtgItemRate.Value) * (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))));
                        //double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim())) - Convert.ToDouble(hidShrtgLimit.Value)) * (Convert.ToDouble((hidShrtgRate.Value == "") ? "0.00" : hidShrtgRate.Value)));
                        double ShrtgRate = Convert.ToDouble(((Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim())) - Convert.ToDouble(hidShrtgLimit.Value)) * (Convert.ToDouble((txtShrtgRate.Text == "") ? "0.00" : txtShrtgRate.Text)));
                        //lblShortageAmnt.Text = (bahda + ShrtgRate).ToString("N2");
                        lblShortageAmnt.Text = (ShrtgRate).ToString("N2");
                    }
                    if (Convert.ToDouble(txtShrtage.Text.Trim()) <= Convert.ToDouble(txtDiffrnce.Text.Trim()))
                    {
                        if (txtDiffrnce.Text.Trim() != "")
                        {
                            txtDiffrnce.Text = Convert.ToDouble(Convert.ToDouble(lblWeghtKg.Text.Trim()) - (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                    else
                    {
                        if (lblWeghtKg.Text.Trim() != "")
                        {
                            txtDiffrnce.Text = Convert.ToDouble(Convert.ToDouble(lblWeghtKg.Text.Trim()) - (Convert.ToDouble((txtShrtage.Text.Trim() == "") ? "0.00" : txtShrtage.Text.Trim()))).ToString("N2");
                        }
                    }
                }


            }
            txtShrtage.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        #endregion

        #region  Grid Enents...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //double dNetAmnt, dQty, dWeight = 0;
            Int64 totalkm, fromkm, tokm;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkShrtg = (CheckBox)e.Row.FindControl("chkShrtg");
                TextBox txtdelvDate = (TextBox)e.Row.FindControl("txtdelvDate");
                CheckBox chkDelvrd = (CheckBox)e.Row.FindControl("chkDelvrd");
                TextBox txtRemrk = (TextBox)e.Row.FindControl("txtRemrk");
                TextBox txtFromKm = (TextBox)e.Row.FindControl("txtFromKm");
                TextBox txtToKm = (TextBox)e.Row.FindControl("txtToKm");
                Label GrDate = (Label)e.Row.FindControl("lblGrDate");

                if (ddlGrtype.SelectedValue == "GR")
                {
                    txtFromKm.Enabled = true;
                    txtToKm.Enabled = true;
                }
                else
                {
                    txtFromKm.Enabled = false;
                    txtToKm.Enabled = false;
                }


                //  txtdelvDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRemrk.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtdelvDate.Enabled = true; txtRemrk.Enabled = true; chkShrtg.Enabled = true; chkDelvrd.Enabled = true;

                //hidmindt.Value = string.IsNullOrEmpty(Convert.ToString(GrDate.Text)) ? "01-04-2016" : Convert.ToString(Convert.ToDateTime(GrDate.Text).ToString("dd-MM-yyyy"));
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Date", "datecontrol();", true);
                //txtdelvDate.Text = hidmindt.Value;

                HiddenField HidInvIdno = (HiddenField)e.Row.FindControl("hidInvIdno");
                dTotNetAmnt = dTotNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dTotQty = dTotQty + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotWeight = dTotWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Weght"));
                if (Convert.ToInt32(HidInvIdno.Value) > 0)
                {
                    txtdelvDate.Enabled = false; txtRemrk.Enabled = false; chkShrtg.Enabled = false; chkDelvrd.Enabled = false;
                    e.Row.ForeColor = System.Drawing.Color.Maroon;
                    //HtmlTableRow tr = e.Row.FindControl("tr") as HtmlTableRow;
                    //string backgroundColor = "Red";
                    //tr.Style.Add(HtmlTextWriterStyle.BackgroundColor, backgroundColor);
                    //tr.Style.Add(HtmlTextWriterStyle.Color, backgroundColor);
                }
                else
                {
                    txtdelvDate.Enabled = true; txtRemrk.Enabled = true; chkShrtg.Enabled = true; chkDelvrd.Enabled = true;
                }
                //TextBox tbOne = e.Row.FindControl("txtdelvDate") as TextBox;
                //if (tbOne != null)
                //{
                //    string js = "$(function() { $('#" + tbOne.ClientID + "').datepicker();  });";
                //    ClientScript.RegisterStartupScript(this.GetType(), "DatePickJS_" + Guid.NewGuid().ToString("N"), js, true);
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dTotNetAmnt.ToString("N2");
                Label lblTotWeight = (Label)e.Row.FindControl("lblTotWeight");

                Label lblTotqy = (Label)e.Row.FindControl("lblTotqy");
                lblTotWeight.Text = dTotWeight.ToString(); lblTotqy.Text = dTotQty.ToString("N2");
            }
        }
        protected void grdGrdetals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtShortage = (TextBox)e.Row.FindControl("txtShortage");
                TextBox txtDiff = (TextBox)e.Row.FindControl("txtDiff");
                TextBox lblShortageAmnt = (TextBox)e.Row.FindControl("lblShortageAmnt");
                txtShortage.Enabled = false;
                txtDiff.Enabled = true;
                txtShortage.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtDiff.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                lblShortageAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

                TextBox txtShrtgLim = (TextBox)e.Row.FindControl("txtShrtgLim");
                TextBox txtShrtgRate = (TextBox)e.Row.FindControl("txtShrtgRate");
                txtShrtgLim.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtShrtgRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TextBox txtUnloadweight = (TextBox)e.Row.FindControl("txtUnloadWeight");

                double RateType = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RateType_Idno"));
                DropDownList ddlShrtgType = (DropDownList)e.Row.FindControl("ddlShrtgType");
                if (RateType == 1)
                    ddlShrtgType.SelectedValue = "1";
                else if (RateType == 2)
                    ddlShrtgType.SelectedValue = "2";

                if (hidRateEdit.Value == "True")
                {
                    txtShrtgLim.Enabled = true;
                    txtShrtgRate.Enabled = true;
                }
                else
                {
                    txtShrtgLim.Enabled = false;
                    txtShrtgRate.Enabled = true;
                }
                HiddenField HidShrtgInvIdno = (HiddenField)e.Row.FindControl("HidShrtgInvIdno");
                if (Convert.ToInt32(HidShrtgInvIdno.Value) > 0)
                {
                    txtShortage.Enabled = false; txtDiff.Enabled = false;
                    e.Row.ForeColor = System.Drawing.Color.Maroon;
                }
                else
                {
                    txtShortage.Enabled = true; txtDiff.Enabled = true;
                }


            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowState == DataControlRowState.Edit)
            //{
            //    var textBox = e.Row.FindControl("txtdelvDate") as TextBox;
            //    ClientScript.RegisterStartupScript(this.GetType(), "datepick",
            //    "$(function () { $('#" + textBox.ClientID + "').datepick({ dateFormat: 'dd/mm/yyyy' });  })", true);
            //}
        }
        #endregion

        protected void txtFromKm_TextChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox Fromkm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txtFromKm");
            TextBox Tokm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txtToKm");
            TextBox Totkm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txttotalKm");

            if (Convert.ToInt32(Tokm.Text) > Convert.ToInt32(Fromkm.Text))
            {
                Int32 Totalkm = Convert.ToInt32(Tokm.Text) - Convert.ToInt32(Fromkm.Text);
                Totkm.Text = Convert.ToString(Totalkm);
            }
            else
            {
                ShowMessageErr("To Km can not less than From Km");
            }

        }
        protected void txtToKm_TextChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox Fromkm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txtFromKm");
            TextBox Tokm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txtToKm");
            TextBox Totkm = (TextBox)grdMain.Rows[selRowIndex].FindControl("txttotalKm");

            if (Convert.ToInt32(Tokm.Text) > Convert.ToInt32(Fromkm.Text))
            {
                Int32 Totalkm = Convert.ToInt32(Tokm.Text) - Convert.ToInt32(Fromkm.Text);
                Totkm.Text = Convert.ToString(Totalkm);
            }
            else
            {
                ShowMessageErr("Please Enter To Km always Greater than From Km ");
            }

        }
        protected void chkDelvrd_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox chk = (CheckBox)sender;
            //GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            //string i = grdMain.DataKeys[gr.RowIndex].Value.ToString();


            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox cb = (CheckBox)grdMain.Rows[selRowIndex].FindControl("chkDelvrd");
            Label date = (Label)grdMain.Rows[selRowIndex].FindControl("lblGrDate");
            TextBox txtdt = (TextBox)grdMain.Rows[selRowIndex].FindControl("txtdelvDate");

            if (cb.Checked)
            {
                hidmindt.Value = string.IsNullOrEmpty(Convert.ToString(date.Text)) ? "" : Convert.ToString(Convert.ToDateTime(ApplicationFunction.mmddyyyy(date.Text)).ToString("dd-MM-yyyy"));
                txtdt.Text = date.Text.ToString();
            }
        }
        protected void ddlShrtgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
            DropDownList ddlShrtgType = (DropDownList)grdGrdetals.Rows[selRowIndex].FindControl("ddlShrtgType");
            TextBox ShrgLim = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShrtgLim");
            TextBox ShrtgRate = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShrtgRate");
            TextBox txtShortage = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShortage");
            TextBox txtDiff = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtDiff");
            TextBox lblShortageAmnt = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("lblShortageAmnt");
            HiddenField HidRateType = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidRateType");
            HiddenField HidShrgLim = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgLimit");
            HiddenField HidShrtgRate = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgRate");
            HiddenField HidShrgLimOther = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgLimitOther");
            HiddenField HidShrtgRateOther = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgRateOther");
            HiddenField hidShrtgLimitDefault = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgLimitDefault");
            HiddenField hidShrtgRateDefault = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidShrtgRateDefault");

            if ((ddlShrtgType.SelectedValue == "1" && HidRateType.Value == "1") || (ddlShrtgType.SelectedValue == "2" && HidRateType.Value == "2"))
            {
                ShrgLim.Text = hidShrtgLimitDefault.Value;
                ShrtgRate.Text = hidShrtgRateDefault.Value;
            }
            if ((ddlShrtgType.SelectedValue == "1" && HidRateType.Value == "2") || (ddlShrtgType.SelectedValue == "2" && HidRateType.Value == "1"))
            {
                ShrgLim.Text = HidShrgLimOther.Value;
                ShrtgRate.Text = HidShrtgRateOther.Value;
                HidShrgLim.Value = HidShrgLimOther.Value.ToString();
                HidShrtgRate.Value = HidShrtgRateOther.Value.ToString();
            }
            txtShortage.Text = "0"; txtDiff.Text = "0"; lblShortageAmnt.Text = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }

        #region Posting A/C...........
        protected void lnkbtnAccPosting_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                int Count = 0; DataTable dt = CreateDt();
                ChallanConfirmationDAL objDal = new ChallanConfirmationDAL();
                BindDropdownDAL obj1 = new BindDropdownDAL();
                clsAccountPosting objVATInvoicePOSDAL = new clsAccountPosting();
                DataSet objDataSet = objDal.AccPosting(ApplicationFunction.ConnectionString(), "ChallanConfirmationPos", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));

                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
                        {
                            Int64 Idno = (string.IsNullOrEmpty(objDataSet.Tables[0].Rows[i]["Chln_Idno"].ToString()) ? 0 : Convert.ToInt64(objDataSet.Tables[0].Rows[i]["Chln_Idno"].ToString()));
                            string Type = (string.IsNullOrEmpty(objDataSet.Tables[0].Rows[i]["Gr_type"].ToString()) ? "" : Convert.ToString(objDataSet.Tables[0].Rows[i]["Gr_type"]));
                     
                            if (Idno > 0)
                            {
                                ChlnBookingDAL ChlnBookHead = new ChlnBookingDAL();
                                tblChlnBookHead chlnBookhead = ChlnBookHead.selectHead(Idno, Type);
                                DataTable objDetl = objDal.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt64(chlnBookhead.Chln_Idno));
                                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                                {
                                    if (this.RecPostIntoAccounts(Convert.ToDouble(objDetl.Rows[0]["shortage_Amount"]), Convert.ToInt32(Idno), "CCB", 0, 0, 0, 0, 0, Convert.ToInt32(chlnBookhead.Year_Idno), Convert.ToInt32(chlnBookhead.Truck_Idno), Convert.ToString(chlnBookhead.Inst_Dt), (string.IsNullOrEmpty(chlnBookhead.Inst_No.ToString()) ? "0" : Convert.ToString(chlnBookhead.Inst_No)), (string.IsNullOrEmpty(chlnBookhead.Driver_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Driver_Idno)), Convert.ToDateTime(chlnBookhead.Chln_Date).ToString("dd-MM-yyyy"), Convert.ToInt32(chlnBookhead.Chln_No), (string.IsNullOrEmpty(chlnBookhead.RcptType_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.RcptType_Idno)), (string.IsNullOrEmpty(chlnBookhead.Bank_Idno.ToString()) ? 0 : Convert.ToInt32(chlnBookhead.Bank_Idno)), Convert.ToDouble(chlnBookhead.Gross_Amnt), Convert.ToDouble(chlnBookhead.Commsn_Amnt), Convert.ToDouble(chlnBookhead.TDSTax_Amnt), (string.IsNullOrEmpty(chlnBookhead.Diesel_Amnt.ToString()) ? 0.00 : Convert.ToDouble(chlnBookhead.Diesel_Amnt))) == true)
                                    {
                                        Count++;
                                        tScope.Complete(); tScope.Dispose();
                                      //  objDal.UpdateIsPosting(Idno);
                                    }
                                    else
                                    {
                                        tScope.Dispose(); 
                                        this.PostingLeft();
                                        this.ShowMessageErr(hidpostingmsg.Value);
                                        return;
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
                    catch (Exception exe)
                    {

                    }
                    this.PostingLeft();
                }
            }
        }
        
       
        #endregion
        #region Pos Function..........
        private void PostingLeft()
        {
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                clsAccountPosting clsobj = new clsAccountPosting();
                ChallanConfirmationDAL objDal = new ChallanConfirmationDAL();
                DataSet objDataSets = objDal.AccPosting(ApplicationFunction.ConnectionString(), "ChallanConfirmationPos", string.IsNullOrEmpty(Convert.ToString(txtIdFrom.Text.Trim())) ? 0 : Convert.ToInt64(txtIdFrom.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(txtIdTo.Text.Trim())) ? 0 : Convert.ToInt64(txtIdTo.Text.Trim()));
                if (objDataSets != null && objDataSets.Tables.Count > 0 && objDataSets.Tables[1].Rows.Count > 0)
                {
                    lblPostingLeft.Text = "Record(s) : " + Convert.ToString(objDataSets.Tables[1].Rows[0][0]);
                }
                else
                {
                    lblPostingLeft.Text = "Record(s) : 0";
                }
            }
        }

        protected void txtUnloadWeight_TextChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox txtUnloadWeight = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtUnloadWeight");
            HiddenField hid = (HiddenField)grdGrdetals.Rows[selRowIndex].FindControl("hidUnloadWeight");
            TextBox txtShortage = (TextBox)grdGrdetals.Rows[selRowIndex].FindControl("txtShortage");
            Label lblWeghtKg = (Label)grdGrdetals.Rows[selRowIndex].FindControl("lblWeghtKg");
            hid.Value = txtUnloadWeight.Text.Trim();

            double Shotage = Convert.ToDouble((Convert.ToDouble(lblWeghtKg.Text) - (Convert.ToDouble((txtUnloadWeight.Text.Trim() == "") ? "0.00" : txtUnloadWeight.Text.Trim()))));
            txtShortage.Text = Shotage.ToString("N2");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }

        private bool PostIntoAccounts(double Amount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno)
        {
            #region Variables Declaration...


            hidpostingmsg.Value = string.Empty;
            Int64 intVchrIdno = 0, intValue = 0 ; DateTime? dtBankDate = null;
            Int32 Shortage_Idno = 0;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            
            #endregion

          

            #region Account link Validations...
            ChallanConfirmationDAL objChallanconfirmation = new ChallanConfirmationDAL();

            DataTable DsHire = objChallanconfirmation.DsShortageAcnt(ApplicationFunction.ConnectionString());
            if (DsHire == null || DsHire.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                return false;
            }
            else
            {
                Shortage_Idno = Convert.ToInt32(DsHire.Rows[0]["ShortageIdno"]);
            }
            
            #endregion


            #region To Shorting Posting Start ...

            

            if (Request.QueryString["q"] == null)
            {
                intValue = 1;
            }
            else
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
            }
            if (Shortage_Idno > 0 && PostingPartyIdno > 0)
            {
                if (intValue > 0)
                {
                    Int64 VchrNo = objclsAccountPosting.GetMaxVchrNo(4, 0, intYearIdno);
                    intValue = objclsAccountPosting.InsertInVchrHead(
                    Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)),
                    4,
                    0,
                    "Challan. No: " + ChallanNumber.Value + " Challan Confirmation . Date: " + txtDate.Text,
                    true,
                    0,
                    strDocType,
                    0,
                    0,
                    Convert.ToInt64(ChallanNumber.Value),
                    ApplicationFunction.GetIndianDateTime().Date,
                    VchrNo,
                    0,
                    intYearIdno,
                    0, intUserIdno);
                    if (intValue > 0)
                    {
                        #region Sender Account Posting ...

                        intVchrIdno = intValue;
                        intValue = 0; /*Insert In VchrDetl*/
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt64(PostingPartyIdno),
                        "Challan. No: " + ChallanNumber.Value + " Challan Confirmation . Date: " + txtDate.Text + "Shortage Amount " + Amount,
                        Amount,
                        Convert.ToByte(2),
                        Convert.ToByte(0),
                        "",
                        true,
                        null,  //please check here if date is Blank
                        "", 0);
                        if (intValue > 0)
                        {
                            intVchrIdno = intValue;
                            if (Amount > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                    intVchrIdno,
                                   Shortage_Idno,
                                   " Challan. No: " + ChallanNumber.Value + " Challan Confirmation . Date: " + txtDate.Text + "Shortage Amount " + Amount,
                                   Convert.ToDouble(Amount),
                                    Convert.ToByte(1),
                                    Convert.ToByte(0),
                                    "",
                                    false,
                                    dtBankDate,  //please check here if date is Blank
                                    "0", 0);
                                if (intValue == 0)
                                {
                                    return false;
                                }
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

            #endregion


            #region Deallocate variables...

            objclsAccountPosting = null;

            return true;

            #endregion
        }


        private bool RecPostIntoAccounts(double AdvAmount, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno, Int32 YearIdno, Int32 TruckIdno, string InstDate, string InstNo, Int32 DriverIdno, string strDate, Int32 intChlnNo, Int32 intRcptType, Int32 intCustBIdno, double dGrossAmnt, double dCommissionAmnt, double dTdsAmnt, double dDiesel)
        {
            #region Variables Declaration...
            Int64 intVchrIdno = 0; Int64 intValue = 0; Int32 IAcntIdno = 0; DateTime? dtBankDate = null;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            BindDropdownDAL objDal = new BindDropdownDAL();
            ChallanConfirmationDAL objChallanconfirmation = new ChallanConfirmationDAL();
            Int64 Shortage_Idno = 0;

            DataTable DsHire = objChallanconfirmation.DsShortageAcnt(ApplicationFunction.ConnectionString());
            if (DsHire == null || DsHire.Rows.Count <= 0)
            {
                hidpostingmsg.Value = "Transport Account is not defined. Kindly define.";
                return false;
            }
            else
            {
                Shortage_Idno = Convert.ToInt32(DsHire.Rows[0]["ShortageIdno"]);
            }
            #endregion

            DataSet dsLD = objDal.GetLorryDetails(ApplicationFunction.ConnectionString(), "GetLorryDetails", TruckIdno, strDate);

            if (dsLD != null && dsLD.Tables.Count > 0 && dsLD.Tables[0].Rows.Count > 0)
            {
                Int32 intLtype = string.IsNullOrEmpty(dsLD.Tables[0].Rows[0]["Lorry_Type"].ToString()) ? 0 : Convert.ToInt32(dsLD.Tables[0].Rows[0]["Lorry_Type"]);
                string strLorryNo = Convert.ToString(dsLD.Tables[0].Rows[0]["Lorry_No"]);
                Int32 PartyIdno = Convert.ToInt32(dsLD.Tables[0].Rows[0]["Acnt_Idno"]);

                if (intLtype == 0) { IAcntIdno = DriverIdno; } else { IAcntIdno = PartyIdno; }

                

                #region Amount Posting............

                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);

                #region Gross Amount Posting...
                if (AdvAmount > 0)
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
                            "Challan No: " + Convert.ToString(intChlnNo) + " Challan Date: " + strDate + " Lorry: " + strLorryNo +"Shortage Amount " +AdvAmount,
                            AdvAmount, Convert.ToByte(2), Convert.ToByte(0), "", true, null,  //please check here if date is Blank
                            "", 0);
                            if (intValue > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(intVchrIdno, Shortage_Idno,
                                      "Challan. No: " + Convert.ToString(intChlnNo) + " Challan. Date: " + strDate + " Lorry: " + strLorryNo + "Shortage Amount " + AdvAmount,
                                       Convert.ToDouble(AdvAmount), Convert.ToByte(1), Convert.ToByte(0), "", false, null,  //  please check here if date is Blank
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


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] GettollTaxNumber(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();
            List<string> PartyNumber = new List<string>();
            DataTable dtNames = new DataTable();
            ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
            DataSet dt = obj.SelectTollTax(prefixText, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Tables[0].Rows[i]["Tolltax_name"]), Convert.ToString(dt.Tables[0].Rows[i]["Toll_id"]));
                    PartyNumber.Add(item);
                }
                return PartyNumber.ToArray();
            }
            else
            {
                return null;
            }
        }


        protected void txtTollTax_TextChanged(object sender, EventArgs e)
        {
            ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
            DataSet ds = obj.SelectAmount(Convert.ToString(hidtollTaxID.Value), ApplicationFunction.ConnectionString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txttollAmnt.Text = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Amount"])) ? "0" : Convert.ToString(ds.Tables[0].Rows[0]["Amount"]);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Toll", "openModal();", true);
        }

        protected void lnkbtnsubmit_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (hidtollTaxID.Value == "0" || hidtollTaxID.Value == "")
            {
                this.ShowMessage("Please Select Toll Tax Name!");
                txtTollTax.Focus();
                return;
            }
            if (hidrowid.Value != string.Empty)
            {
                DtTempToll = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTempToll.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Toll_Name"] = txtTollTax.Text.Trim();
                        dtrow["Toll_Amt"] = txttollAmnt.Text.Trim();
                        dtrow["Ticket_No"] = txtTktNo.Text.Trim();
                    }
                }
            }
            else
            {
                DtTempToll = (DataTable)ViewState["dt"];
                if ((DtTempToll != null) && (DtTempToll.Rows.Count > 0))
                {
                    foreach (DataRow row in DtTempToll.Rows)
                    {
                        if (Convert.ToInt32(row["Toll_Idno"]) == Convert.ToInt32(hidtollTaxID.Value))
                        {
                            msg = "Toll Name Already Selected!";
                            txtTollTax.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                            return;
                        }
                    }
                }
                else
                { DtTempToll = CreateToll(); }
                Int32 ROWCount = Convert.ToInt32(DtTempToll.Rows.Count) - 1;
                int id = DtTempToll.Rows.Count == 0 ? 1 : (Convert.ToInt32(DtTempToll.Rows[ROWCount]["id"])) + 1;

                ApplicationFunction.DatatableAddRow(DtTempToll,
                    id,
                    hidtollTaxID.Value,
                    txtTollTax.Text,
                    hidGrIdno.Value,
                    hidChallanIdno.Value,
                    txttollAmnt.Text,
                    txtTktNo.Text
                   );
                ViewState["dt"] = DtTempToll;
            }
            this.BindGridToll();
            cleartoll();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Toll", "openModal();", true);
        }

        protected void cleartoll()
        {
            txtTollTax.Text = "";
            txttollAmnt.Text = "";
            txtTktNo.Text = "";
        }

        protected void grdmaintoll_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTempToll = (DataTable)ViewState["dt"];
            if (e.CommandName == "cmdedittoll")
            {
                DtTempToll = (DataTable)ViewState["dt"];
                DataRow[] drs = DtTempToll.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {
                    hidTollId.Value = Convert.ToString(drs[0]["Toll_Idno"]);
                    hidGrIdno.Value = Convert.ToString(drs[0]["Gr_Idno"]);
                    hidChallanIdno.Value = Convert.ToString(drs[0]["Chln_Idno"]);
                    txtTollTax.Text = Convert.ToString(drs[0]["Toll_Name"]);
                    txttollAmnt.Text = Convert.ToString(drs[0]["Toll_Amt"]);
                    txtTktNo.Text = Convert.ToString(drs[0]["Ticket_No"]);
                }
            }
            else if (e.CommandName == "cmddeletetoll")
            {
                DataTable objDataTable = CreateToll();
                foreach (DataRow rw in DtTempToll.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Toll_Idno"], rw["Toll_Name"], rw["GR_Idno"],
                                                                rw["Chln_Idno"], rw["Toll_Amt"], rw["Ticket_No"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridToll();
            }

        }
        #endregion
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            ChallanConfirmationDAL obj = new ChallanConfirmationDAL();
            bool IsUpdate = false; 
            List<tblTollDetl> RDtoll = new List<tblTollDetl>();
            DtTempToll = CreateToll();
            if (grdmaintoll.Rows.Count > 0)
            {

                foreach (GridViewRow row in grdmaintoll.Rows)
                {

                    Label lblamount = (Label)row.FindControl("lblamount");
                    Label lblticket = (Label)row.FindControl("lblticket");
                    Label lbltollNo = (Label)row.FindControl("lbltollNo");
                    HiddenField hidTollId = (HiddenField)row.FindControl("hidTollId");
                    HiddenField hidtollTaxID = (HiddenField)row.FindControl("hidtollTaxID");
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGrIdno");
                    HiddenField hidChallanIdno = (HiddenField)row.FindControl("hidChallanIdno");


                    ApplicationFunction.DatatableAddRow(DtTempToll, 0, hidTollId.Value, lbltollNo.Text, hidGrIdno.Value, hidChallanIdno.Value, lblamount.Text, lblticket.Text);

                    
                }
                IsUpdate = obj.InsertTollNumber(DtTempToll);
                if (IsUpdate == true)
                {

                    ShowMessage(" record Save successfully");

                }
                else
                {
                    ShowMessageErr("record Not Save ");
                }
                obj = null;

               
            }

        }
      
    }
}