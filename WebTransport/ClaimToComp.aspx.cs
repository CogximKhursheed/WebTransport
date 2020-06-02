using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using System.Data;
using WebTransport.Classes;
using System.Transactions;

namespace WebTransport
{
    public partial class ClaimToComp : System.Web.UI.Page
    {
        #region Variables Declarations..
        //private int intFormId = 222;
        BindDropdownDAL obj;
        FinYearDAL objFinYearDAL;
        ClaimToDAL objClaimFrmDAL;
        DataTable dtTemp = new DataTable();
        DataTable DTMain = new DataTable();
        DataTable dtDivTemp = new DataTable();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            #region is Postback............

            if (!Page.IsPostBack)
            {
                ClaimToDAL obj = new ClaimToDAL();

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                this.BindDateRange();
                this.BindPartyName();
                this.BindCompany();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                GetMaxClaimNoToCompany();
                rdoAgnSend.Checked = true;
                if (Request.QueryString["ClaimHeadIdno"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["ClaimHeadIdno"].ToString()));
                    var ClaimExist = obj.Exists(Convert.ToInt64(Request.QueryString["ClaimHeadIdno"]));
                    if (ClaimExist != null && ClaimExist.Count>0) { DivSave.Visible = false; } else { DivSave.Visible = true; }
                    ddlDateRange.Enabled = false;
                    lnkbtnNew.Visible = true;

                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
            }
            #endregion
        }
        #endregion

        #region UserDefine Function

        private DataTable CreateDivDt()
        {
            DataTable temp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "SerialIdno", "String",
                "SerialNo", "String",
                "ClaimIdno", "String",
                "ClaimNo", "String",
                "ClaimDate", "String",
                "CityName", "String",
                "PartyName", "String",
                "PartyIdno", "String",
                "SbillNo", "String");
            return temp;
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tblMain",
                "Id", "String",
                "SerialIdno", "String",
                "ClaimIdno", "String",
                "DefectRemark", "String",
                "VechAppDetails", "String",
                "Status", "String",
                "Remark", "String",
                "NewSerialNo", "String",
                "ClaimDetailsIdno","String");
            return dttemp;
        }

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            objFinYearDAL = null;
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtClaimDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDivDateFrom.Text = hidmindate.Value;
                    txtDivDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtClaimDate.Text = hidmindate.Value;
                    txtDivDateFrom.Text = hidmindate.Value;
                    txtDivDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
            }

        }

        private void BindPartyName()
        {
            obj = new BindDropdownDAL();
            DataTable dtParty = new DataTable();
            dtParty = obj.BindPartyForSale(ApplicationFunction.ConnectionString());
            obj = null;
            ddlDivPrtyName.DataSource = dtParty;
            ddlDivPrtyName.DataTextField = "Acnt_Name";
            ddlDivPrtyName.DataValueField = "Acnt_Idno";
            ddlDivPrtyName.DataBind();
            ddlDivPrtyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

            ddlParty.DataSource = dtParty;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

        }

        private void BindCompany()
        {
            obj = new BindDropdownDAL();
            var CompanyName = obj.SelectCompanyName();
            obj = null;
            if (CompanyName != null)
            {
                ddlCompanyName.DataSource = CompanyName;
                ddlCompanyName.DataTextField = "Acnt_Name";
                ddlCompanyName.DataValueField = "Acnt_Idno";
                ddlCompanyName.DataBind();
            }
            ddlCompanyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Company.......", "0"));
        }

        private void BindCity()
        {
            obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            obj = null;
            if (FrmCity.Count > 0)
            {
                ddlFromCity.DataSource = FrmCity;
                ddlFromCity.DataTextField = "City_Name";
                ddlFromCity.DataValueField = "City_Idno";
                ddlFromCity.DataBind();
            }
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }

        private void BindDateRange()
        {
            objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void GetMaxClaimNoToCompany()
        {
            objClaimFrmDAL = new ClaimToDAL();
            txtClaimNo.Text = objClaimFrmDAL.GetClaimMax(string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(txtPrefixNo.Text.Trim()) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()), Convert.ToInt64(ddlDateRange.SelectedValue)).ToString();
            objClaimFrmDAL = null;
        }

        private void BindDivGrid()
        {
            if (ViewState["dtDivGrid"] != null)
            {
                dtDivTemp = (DataTable)ViewState["dtDivGrid"];
                if (dtDivTemp.Rows.Count > 0)
                {
                    grdSearchRecords.DataSource = dtDivTemp;
                    grdSearchRecords.DataBind();
                    lblSearchRecords.InnerText = "T. Record(s) : " + dtDivTemp.Rows.Count + "";
                }
                else
                {
                    dtDivTemp = null;
                    grdSearchRecords.DataSource = dtDivTemp;
                    grdSearchRecords.DataBind();
                    lblSearchRecords.InnerText = "T. Record(s) : 0";
                }
            }
            else
            {
                dtDivTemp = null;
                grdSearchRecords.DataSource = dtDivTemp;
                grdSearchRecords.DataBind();
                lblSearchRecords.InnerText = "T. Record(s) : 0";
            }
        }

        private void Clear()
        {
            dtTemp = null; ViewState["dtDivGrid"] = null; dtDivTemp = null;
            grdMain.DataSource = dtTemp;
            grdMain.DataBind();

            ddlParty.SelectedIndex = 0;
            grdSearchRecords.DataSource = dtDivTemp;
            grdSearchRecords.DataBind();
            DivErrorMsg.Visible = false;
            lblmessage.Text = "";
        }

        private void Populate(Int64 ClaimHeadIdno)
        {
            objClaimFrmDAL = new ClaimToDAL();
            tblClaimToComHead ObjHead = objClaimFrmDAL.Select_ClaimHead(ClaimHeadIdno);

            if (ObjHead != null)
            {
                //SpanLastPrint.Visible = false;
                ViewState["ClaimHeadIdno"] = ClaimHeadIdno;
                hidid.Value = ClaimHeadIdno.ToString();
                ddlDateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtPrefixNo.Text = ObjHead.Prefix_No.ToString();
                txtClaimDate.Text = string.IsNullOrEmpty(ObjHead.ClaimToComHead_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.ClaimToComHead_Date).ToString("dd-MM-yyyy");
                txtClaimNo.Text = string.IsNullOrEmpty(ObjHead.ClaimToCom_No.ToString()) ? "" : Convert.ToString(ObjHead.ClaimToCom_No);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(ObjHead.FromLoc_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.FromLoc_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(ObjHead.Prty_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.Prty_Idno);
                if (ObjHead.Against == 2) { rdoAgnSend.Checked = false; rdoAgnReceived.Checked = true; } else { rdoAgnSend.Checked = true; rdoAgnReceived.Checked = false; }
                ddlCompanyName.SelectedValue = string.IsNullOrEmpty(ObjHead.Comp_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.Comp_Idno);
                dtTemp = CreateDt();
                string Result = String.Empty;
                if ((string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ClaimHeadIdno"].ToString())) ? 0 : Convert.ToInt64(Request.QueryString["ClaimHeadIdno"].ToString())) > 0) { ddlFromCity.Enabled = false; imgSearch.Visible = false; } else { ddlFromCity.Enabled = true; imgSearch.Visible = true; }
                var Lst = objClaimFrmDAL.SelectClaimDetails(ClaimHeadIdno);
                if (Lst != null && Lst.Count > 0)
                {
                    grdMain.DataSource = Lst;
                    grdMain.DataBind();
                    if (rdoAgnReceived.Checked) { grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = true; grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = false; } else { grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = true; grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = false; }
                }
            }
        }
        #endregion

        #region Dropdown Event
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue)) > 0)
            {
                GetMaxClaimNoToCompany();
            }
            Clear();
            grdMain.Visible = false;
            ddlFromCity.Focus();
        }
        #endregion

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0")
            {
                ShowMessageErr("Please Select first City."); return;
            }
            else
            {
                dtDivTemp = null;
                grdSearchRecords.DataSource = dtDivTemp;
                grdSearchRecords.DataBind();
                lblSearchRecords.InnerText = "T. Record(s) : 0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
            }
        }
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            grdSearchRecords.Visible = true;
            objClaimFrmDAL = new ClaimToDAL();
            DataTable dt = CreateDivDt();
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            DateTime? DateFrom = null; DateTime? DateTo = null;
            DateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateFrom.Text));
            DateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateTo.Text));
            Int64 Location = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
            Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlDivPrtyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlDivPrtyName.SelectedValue);
            string ClaimNo = (string.IsNullOrEmpty(Convert.ToString(txtSBillNo.Text.Trim())) ? "" : Convert.ToString(txtSBillNo.Text.Trim())) + "" + (string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()));
            Int64 Value = 1;
            if (rdoAgnReceived.Checked) { Value = 2; } else { Value = 1; }

            if (Value == 2)
            {
                var lst = objClaimFrmDAL.SelectForSearchRecvd(DateFrom, DateTo, ClaimNo, PartyIdno, Location, intyearid);
                objClaimFrmDAL = null;

                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string SerialIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialIdno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialIdno")));
                        string SerialNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialNo"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialNo")));
                        string PrefNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"));
                        string BillNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"));
                        string SbillIdNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "ClaimHeadIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "ClaimHeadIdno"));
                        string SBillDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CBillDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[i], "CBillDate")).ToString("dd-MM-yyyy"));
                        string SbillNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"))) ? "N/A" : Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"));
                        string CityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CityName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "CityName"));
                        string PartyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PartyName"));
                        string PartyId = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"));
                        hidid.Value = SbillIdNo;
                        ApplicationFunction.DatatableAddRow(dt, i + 1, SerialIdno, SerialNo, SbillIdNo, PrefNo + "" + BillNo, ApplicationFunction.mmddyyyy(SBillDate), CityName, PartyName, PartyId, SbillNo);
                    }
                    ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "PartyIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "PartyIdno")));
                    ddlCompanyName.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "CompIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "CompIdno")));

                    ViewState["dtDivGrid"] = dt;
                    dt.Dispose();
                    this.BindDivGrid();
                    DivErrorMsg.Visible = false;
                    lblDivErrorMsg.InnerText = "";
                }
                else
                {
                    DivErrorMsg.Visible = false;
                    lblDivErrorMsg.InnerText = "";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);

            }
            else
            {
                var lst = objClaimFrmDAL.SelectForSearch(DateFrom, DateTo, ClaimNo, PartyIdno, Location, intyearid);
                objClaimFrmDAL = null;

                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string SerialIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialIdno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialIdno")));
                        string SerialNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialNo"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialNo")));
                        string PrefNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"));
                        string ClaimNumber = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"));
                        string ClaimHeadIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "ClaimHeadIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "ClaimHeadIdno"));
                        string CBillDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CBillDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[i], "CBillDate")).ToString("dd-MM-yyyy"));
                        string SbillNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"))) ? "N/A" : Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"));
                        string CityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CityName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "CityName"));
                        string PartyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PartyName"));
                        string PartyId = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"));

                        ApplicationFunction.DatatableAddRow(dt, i + 1, SerialIdno, SerialNo, ClaimHeadIdno, PrefNo + "" + ClaimNumber, ApplicationFunction.mmddyyyy(CBillDate), CityName, PartyName, PartyId, SbillNo);
                    }
                    ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "PartyIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "PartyIdno")));
                    ddlCompanyName.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "CompIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "CompIdno")));
                    HidClaimHeadIdno.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "ClaimHeadIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "ClaimHeadIdno")));
                    ViewState["dtDivGrid"] = dt;
                    dt.Dispose();
                    this.BindDivGrid();
                    DivErrorMsg.Visible = false;
                    lblDivErrorMsg.InnerText = "";
                }
                else
                {
                    DivErrorMsg.Visible = false;
                    lblDivErrorMsg.InnerText = "";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);

            }

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime? ClaimDate = null;
            ClaimDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtClaimDate.Text));
            objClaimFrmDAL = new ClaimToDAL();
            DateTime CurrentDate = System.DateTime.Now;

            if (grdMain.Rows.Count > 0)
            {
                DTMain = CreateDt();
                if (rdoAgnReceived.Checked)
                {
                    foreach (GridViewRow row in grdMain.Rows)
                    {
                        Label lblGridSerialNo = (Label)row.FindControl("lblGridSerialNo");
                        TextBox txtNewSerialNo = (TextBox)row.FindControl("txtNewSerialNo");
                        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");

                        if (txtNewSerialNo.Text.Trim() == "") { ShowMessageErr("Please Fill Serial Number."); txtNewSerialNo.Focus(); return; }
                        if (lblGridSerialNo.Text.Trim() == txtNewSerialNo.Text.Trim()) { ShowMessageErr("New Serial Not same as old Serial,Check New Serial No." + txtNewSerialNo.Text.Trim() + ""); txtNewSerialNo.Focus(); return; }
                        if (ddlStatus.SelectedValue == "1") { ShowMessageErr("Please Check Status should not be submitted."); ddlStatus.Focus(); return; }
                        BindDropdownDAL obj = new BindDropdownDAL();
                        if (Request.QueryString["ClaimHeadIdno"] == null)
                        {
                            if (txtNewSerialNo.Text.Trim() != "")
                            {
                                Int64 Count = obj.CheckSerialNo(txtNewSerialNo.Text.Trim());
                                if (Count != 0)
                                {
                                    ShowMessageErr("Serial Number :" + txtNewSerialNo.Text.Trim() + " already exists in stock.");
                                    txtNewSerialNo.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidSerialIdno = (HiddenField)row.FindControl("hidSerialIdno");
                    HiddenField hidClaimIdno = (HiddenField)row.FindControl("hidClaimIdno");
                    Label txtDefRemark = (Label)row.FindControl("lblGridDefectNo");
                    Label txtVchAppDetl = (Label)row.FindControl("lblGridVehDetlNo");
                    DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtNewSerialNo = (TextBox)row.FindControl("txtNewSerialNo");
                    HiddenField HidClaimDetailsIdno = (HiddenField)row.FindControl("HidClaimDetailsIdno");

                    ApplicationFunction.DatatableAddRow(DTMain, row.RowIndex + 1, hidSerialIdno.Value, hidClaimIdno.Value, txtDefRemark.Text.Trim(), txtVchAppDetl.Text.Trim(), ddlStatus.SelectedValue, txtRemarks.Text.Trim(), txtNewSerialNo.Text.Trim(), HidClaimDetailsIdno.Value);

                }
                if (rdoAgnReceived.Checked)
                {
                    foreach (GridViewRow rows in grdMain.Rows)
                    {
                        TextBox txtNewSerialNo1 = (TextBox)rows.FindControl("txtNewSerialNo");
                        DataRow[] drs = DTMain.Select("NewSerialNo='" + txtNewSerialNo1.Text.Trim() + "'");
                        if (drs.Length > 1)
                        {
                            ShowMessageErr("Serial Number :" + txtNewSerialNo1.Text.Trim() + " already exists in list.");
                            txtNewSerialNo1.Focus();
                            return;
                        }
                    }
                }

                tblClaimToComHead objClaimHead = new tblClaimToComHead();

                if (rdoAgnReceived.Checked)
                {
                    if (txtClaimDate.Text == "") { objClaimHead.ClaimToComRec_Date = null; } else { objClaimHead.ClaimToComRec_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtClaimDate.Text)); }
                }
                else
                {
                    if (txtClaimDate.Text == "") { objClaimHead.ClaimToComHead_Date = null; } else { objClaimHead.ClaimToComHead_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtClaimDate.Text)); }
                }
                objClaimHead.ClaimToCom_No = string.IsNullOrEmpty(Convert.ToString(txtClaimNo.Text.Trim())) ? 0 : Convert.ToInt64(txtClaimNo.Text.Trim());
                objClaimHead.ClaimHead_Idno = string.IsNullOrEmpty(Convert.ToString(HidClaimHeadIdno.Value)) ? 0 : Convert.ToInt64(HidClaimHeadIdno.Value);
                objClaimHead.Prefix_No = string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim());
                objClaimHead.FromLoc_Idno = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                objClaimHead.Year_Idno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
                objClaimHead.Prty_Idno = string.IsNullOrEmpty(Convert.ToString(ddlParty.SelectedValue)) ? 0 : Convert.ToInt64(ddlParty.SelectedValue);
                objClaimHead.Comp_Idno = string.IsNullOrEmpty(Convert.ToString(ddlCompanyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlCompanyName.SelectedValue);

                Int64 Against = 0; if (rdoAgnSend.Checked) { Against = 1; } else { Against = 2; }
                objClaimHead.Against = Against;
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        objClaimHead.Date_Added = System.DateTime.Now;
                        value = objClaimFrmDAL.Insert(objClaimHead, DTMain);
                        objClaimFrmDAL = null;
                    }
                    else
                    {
                        objClaimHead.Date_Modified = System.DateTime.Now;
                        lnkbtnNew.Visible = false;
                        if (rdoAgnReceived.Checked)
                        {
                            value = objClaimFrmDAL.UpdateForRecvd(objClaimHead, Convert.ToInt32(hidid.Value), DTMain);
                        }
                        else
                        {
                            value = objClaimFrmDAL.Update(objClaimHead, Convert.ToInt32(hidid.Value), DTMain);
                        }

                        objClaimFrmDAL = null;
                    }

                    if (string.IsNullOrEmpty(hidid.Value) == false)
                    {
                        if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                        {
                            ShowMessage("Record Update successfully");
                            Clear();
                            tScope.Complete();
                        }
                        else if (value == -1)
                        {
                            ShowMessageErr("Claim Number Already Exist");
                            tScope.Dispose();
                        }
                        else
                        {
                            ShowMessageErr("Record  Not Update");
                            tScope.Dispose();
                        }
                    }
                    else
                    {
                        if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                        {
                            ShowMessage("Record  saved Successfully ");
                            Clear();
                            tScope.Complete();
                        }
                        else if (value == -1)
                        {
                            ShowMessageErr("Claim Number Already Exist");
                            tScope.Dispose();
                        }
                        else
                        {
                            ShowMessageErr("Record Not  saved Successfully ");
                            tScope.Dispose();
                        }
                    }
                }
            }
            else
            {
                ShowMessageErr("Please Search and Select Serial Number.");
                return;
            }
        }
        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objClaimFrmDAL = new ClaimToDAL();
                if ((grdSearchRecords != null) && (grdSearchRecords.Rows.Count > 0))
                {
                    string ClaimIdDetlValue = string.Empty;
                    for (int count = 0; count < grdSearchRecords.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdSearchRecords.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidClaimIdno = (HiddenField)grdSearchRecords.Rows[count].FindControl("HidClaimIdno");
                            ClaimIdDetlValue = ClaimIdDetlValue + hidClaimIdno.Value + ",";
                        }
                    }
                    if (ClaimIdDetlValue != "")
                    {
                        ClaimIdDetlValue = ClaimIdDetlValue.Substring(0, ClaimIdDetlValue.Length - 1);
                    }
                    if (ClaimIdDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Serial No.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                    }
                    else
                    {
                        objClaimFrmDAL = new ClaimToDAL();
                        if (rdoAgnReceived.Checked)
                        {
                            var Lst = objClaimFrmDAL.SelectClaimDetailsForRecvd(Convert.ToString(ClaimIdDetlValue));
                            if (Lst != null && Lst.Count > 0)
                            {
                                grdMain.DataSource = Lst;
                                grdMain.DataBind();
                                grdMain.Visible = true;
                                if (rdoAgnReceived.Checked) { grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = true; grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = false; } else { grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = true; grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = false; }
                            }
                        }
                        else
                        {
                            var Lst = objClaimFrmDAL.Select(Convert.ToString(ClaimIdDetlValue));

                            if (Lst != null && Lst.Count > 0)
                            {
                                grdMain.DataSource = Lst;
                                grdMain.DataBind();
                                grdMain.Visible = true;
                                if (rdoAgnReceived.Checked) { grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = true; grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = false; } else { grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = true; grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = false; }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }


        }
        public void ClearOnAgainstCheck()
        {
            ddlFromCity.Enabled = true; imgSearch.Visible = true;
            ddlParty.SelectedIndex = ddlCompanyName.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            ViewState["dtDivGrid"] = null; dtDivTemp = null;
            BindDivGrid();
        }
        protected void rdoAgnSend_CheckedChanged(object sender, EventArgs e)
        {
            ClearOnAgainstCheck();
        }
        protected void rdoAgnReceived_CheckedChanged(object sender, EventArgs e)
        {
            ClearOnAgainstCheck();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                ddlStatus.SelectedValue = lblStatus.Text;
                if (rdoAgnReceived.Checked) { ddlStatus.Enabled = true; } else { ddlStatus.Enabled = false; }
            }
        }
    }
}