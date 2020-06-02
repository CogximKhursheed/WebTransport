using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using WebTransport.Classes;
using WebTransport.DAL;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Transactions;
using WebTransport.DAL;
using System.Data.OleDb;
using System.Globalization;
using System.Text;

namespace WebTransport
{
    public partial class ClaimFrm : Pagebase
    {
        #region VariablesDeclarations..
        //private int intFormId = 222;
        DataTable dtTemp = new DataTable();
        DataTable DTMain = new DataTable();
        DataTable dtDivTemp = new DataTable();
        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            #region is Postback............

            if (!Page.IsPostBack)
            {
                ClaimFrmDAL obj=new ClaimFrmDAL();
                txtClaimDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
               
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                this.BindDateRange();
                this.BindPartyName();
                this.BindCompany();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange.SelectedIndex = 0; ddlDateRange_SelectedIndexChanged(null, null);
                GetMaxClaimNo();
                if (Request.QueryString["ClaimHeadIdno"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["ClaimHeadIdno"].ToString()));
                     var ClaimExist = obj.Exists(Convert.ToInt64(Request.QueryString["ClaimHeadIdno"]));
                     if (ClaimExist != null && ClaimExist.Count>0) { DivSave.Visible = false; } else { DivSave.Visible = true; }
                    //lnkbtnPrint.Visible = true;
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


        #region Functions....

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
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
            BindDropdownDAL obj = new BindDropdownDAL();
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
        //private void BindClaimParty()
        //{
        //    BindDropdownDAL obj = new BindDropdownDAL();
        //    DataTable dtParty = new DataTable();
        //    dtParty = obj.BindPartyforClaim(ApplicationFunction.ConnectionString());
        //    obj = null;
        //    ddlDivPrtyName.DataSource = dtParty;
        //    ddlDivPrtyName.DataTextField = "Acnt_Name";
        //    ddlDivPrtyName.DataValueField = "Acnt_Idno";
        //    ddlDivPrtyName.DataBind();
        //    ddlDivPrtyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

        //    ddlParty.DataSource = dtParty;
        //    ddlParty.DataTextField = "Acnt_Name";
        //    ddlParty.DataValueField = "Acnt_Idno";
        //    ddlParty.DataBind();
        //    ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

        //}

        private void BindCompany()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
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
            BindDropdownDAL obj = new BindDropdownDAL();
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
            BindDropdownDAL obj = new BindDropdownDAL();
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
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange();
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void Populate(Int64 ClaimHeadIdno)
        {
            ClaimFrmDAL  objClaimFrmDAL = new ClaimFrmDAL();
            tblClaimHead ObjHead = objClaimFrmDAL.Select_ClaimHead(ClaimHeadIdno);
            
            if (ObjHead != null)
            {
                
                ViewState["ClaimHeadIdno"] = ClaimHeadIdno;
                ddlDateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtPrefixNo.Text = ObjHead.Prefix_No.ToString();
                txtClaimDate.Text = string.IsNullOrEmpty(ObjHead.ClaimHead_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.ClaimHead_Date).ToString("dd-MM-yyyy");
                if ((string.IsNullOrEmpty(Convert.ToString(ObjHead.Claim_Against)) ? 0 : Convert.ToInt64(ObjHead.Claim_Against)) > 0) { RDbParty.Checked = true; } else { RDbSaleBill.Checked = true; }
                txtClaimNo.Text = string.IsNullOrEmpty(ObjHead.Claim_No.ToString()) ? "" : Convert.ToString(ObjHead.Claim_No);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(ObjHead.FromLoc_Idno.ToString())? "0" : Convert.ToString(ObjHead.FromLoc_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(ObjHead.Prty_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.Prty_Idno);
                ddlCompanyName.SelectedValue = string.IsNullOrEmpty(ObjHead.Comp_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.Comp_Idno);
                
                RDbParty.Enabled = false; RDbSaleBill.Enabled = false;
                ddlFromCity.Enabled = false; imgSearch.Visible = false;
                dtTemp = CreateDt();
                string Result = String.Empty;
                var ClaimNo = objClaimFrmDAL.SelectSaleBillNo(Convert.ToInt64(ClaimHeadIdno)); if (ClaimNo != null && ClaimNo > 0) { lblSbillNo.Text = "Sale Bill No : " + ClaimNo.ToString(); }

                DataTable dt = new DataTable();
                if ((string.IsNullOrEmpty(Convert.ToString(ObjHead.Claim_Against)) ? 0 : Convert.ToInt64(ObjHead.Claim_Against)) > 0)
                {
                    dt = objClaimFrmDAL.SelectClaimParty(ApplicationFunction.ConnectionString(), ClaimHeadIdno);
                }
                else
                {
                    dt = objClaimFrmDAL.SelectClaimDetails(ApplicationFunction.ConnectionString(), ClaimHeadIdno);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    objClaimFrmDAL = null;
                    ViewState["dt"] = dt;
                    
                }
                this.BindGridT();
                ViewState["dt"] = dtTemp;
                this.BindGridT();
            }
        }

        #endregion
        #region Messages Function.....

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        #endregion

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue)) > 0)
            {
                GetMaxClaimNo();
            }
            Clear();
            grdMain.Visible = false;
            imgSearch.Focus();
        }


        #region IndexChangeEvents........
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        #endregion

        private void GetMaxClaimNo()
        {
            ClaimFrmDAL objClaimFrmDAL = new ClaimFrmDAL();
            txtClaimNo.Text = objClaimFrmDAL.GetClaimMax(string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(txtPrefixNo.Text.Trim()) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()),  Convert.ToInt64(ddlDateRange.SelectedValue)).ToString();
            objClaimFrmDAL = null;
        }
        private DataTable CreateDivDt()
        {
            DataTable temp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "SerialIdno","String",
                "SerialNo", "String",
                "SbillIdno","String",
                "SBillNo", "String",
                "SBillDate", "String",
                "CityName", "String",
                "PartyName", "String",
                "PartyIdno", "String");
            return temp;
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tblMain",
                "Id", "String",
                "SerialIdno", "String",
                "SbillIdno","String",
                "DefectRemark","String",
                "VechAppDetails","String");
            return dttemp;
        }
        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.Visible = true;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                    if (RDbParty.Checked == true) { grdMain.Columns[3].Visible = false; grdMain.Columns[4].Visible = false; }
                    SetFocus(imgSearch.ClientID);
                }
                else
                {
                    grdMain.Visible = false;
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                    SetFocus(imgSearch.ClientID);
                }
            }
            else
            {
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
                SetFocus(imgSearch.ClientID);
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
            lblSearchRecords.InnerText = "T. Record(s) : 0";
            DivErrorMsg.Visible = false;
            lblmessage.Text = "";
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
                    if (RDbParty.Checked == true) { grdSearchRecords.Columns[2].Visible = false; grdSearchRecords.Columns[3].Visible = false; }

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

        #region Button Events.....

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0") 
            { 
                ShowMessageErr("Please Select first City."); return; 
            }
            else
            {
                if (RDbSaleBill.Checked == true)
                {
                    txtSBillNo.Text = ""; DivDetails.Visible = true;
                    rfvRcptEntryDtFrm.Enabled = true; rfvRcptEntryDtTo.Enabled = true; rfvPrtyName.Enabled = false; SpanPartyName.Visible = false;
                }
                else
                {
                    txtSBillNo.Text = ""; DivDetails.Visible = false; rfvRcptEntryDtFrm.Enabled = false; rfvRcptEntryDtTo.Enabled = false; rfvPrtyName.Enabled = true; SpanPartyName.Visible = true;
                }
                dtDivTemp = null;
                grdSearchRecords.DataSource = dtDivTemp;
                grdSearchRecords.DataBind();
                lblSearchRecords.InnerText = "T. Record(s) : 0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
            }
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimFrm.aspx");
        }
        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ClaimFrmDAL objClaimFrmDAL = new ClaimFrmDAL();
                if ((grdSearchRecords != null) && (grdSearchRecords.Rows.Count > 0))
                {   
                    string SerialIdDetlValue = string.Empty;
                    for (int count = 0; count < grdSearchRecords.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdSearchRecords.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidSerialIdno = (HiddenField)grdSearchRecords.Rows[count].FindControl("hidSerialIdno");
                            SerialIdDetlValue = SerialIdDetlValue + hidSerialIdno.Value + ",";
                        }
                    }
                    if (SerialIdDetlValue != "")
                    {
                        SerialIdDetlValue = SerialIdDetlValue.Substring(0, SerialIdDetlValue.Length - 1);
                    }
                    if (SerialIdDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Serial No.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                    }
                    else
                    {
                        string Result = String.Empty;
                     
                        DataTable dt = new DataTable(); DataRow Dr;
                        if (RDbSaleBill.Checked == true)
                        {
                            dt = objClaimFrmDAL.Select(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(SerialIdDetlValue));
                        }
                        else 
                        {
                            dt = objClaimFrmDAL.SelectClaimDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(SerialIdDetlValue));
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            objClaimFrmDAL = null;
                            ViewState["dt"] = dt;
                        }
                        this.BindGridT();
                    }
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            ClaimFrmDAL objClaimFrmDAL = new ClaimFrmDAL();
            if (RDbSaleBill.Checked == true)
            {
                if (txtSBillNo.Text == "" && ddlDivPrtyName.SelectedValue == "0") { DivErrorMsg.Visible = true; lblDivErrorMsg.InnerText = "* Please Select Sale Bill Number or Party."; ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true); return; }
                else
                {
                    grdSearchRecords.Visible = true;
                    
                    DataTable dt = CreateDivDt();
                    Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                    DateTime? DateFrom = null; DateTime? DateTo = null;
                    DateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateFrom.Text));
                    DateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateTo.Text));
                    Int64 Location = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                    Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlDivPrtyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlDivPrtyName.SelectedValue);
                    string SbillNo = (string.IsNullOrEmpty(Convert.ToString(txtSBillNo.Text.Trim())) ? "" : Convert.ToString(txtSBillNo.Text.Trim()));

                    var lst = objClaimFrmDAL.SelectForSearch(DateFrom, DateTo, SbillNo, PartyIdno, Location, intyearid);

                    if (lst != null && lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            string SerialIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialIdno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialIdno")));
                            string SerialNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialNo"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialNo")));
                            string PrefNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"));
                            string BillNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "SbillNo"));
                            string SbillIdNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SbillHeadIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "SbillHeadIdno"));
                            string SBillDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SBillDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[i], "SBillDate")).ToString("dd-MM-yyyy"));

                            string CityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CityName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "CityName"));
                            string PartyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PartyName"));
                            string PartyId = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"));

                            ApplicationFunction.DatatableAddRow(dt, i + 1, SerialIdno, SerialNo, SbillIdNo, PrefNo + "" + BillNo, ApplicationFunction.mmddyyyy(SBillDate), CityName, PartyName, PartyId);
                        }
                        ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "PartyIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "PartyIdno")));

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
            else 
            {
                grdSearchRecords.Visible = true;
                DataTable dt = CreateDivDt();
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                Int64 Location = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlDivPrtyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlDivPrtyName.SelectedValue);

                var lst = objClaimFrmDAL.SearchByParty(PartyIdno, Location, intyearid);
                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string SerialIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialIdno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialIdno")));
                        string SerialNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "SerialNo"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "SerialNo")));
                        string PrefNo = ""; string BillNo = "0"; string SbillIdNo = "0"; string SBillDate = "";
                        string CityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CityName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "CityName"));
                        string PartyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PartyName"));
                        string PartyId = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "PartyIdno"));

                        ApplicationFunction.DatatableAddRow(dt, i + 1, SerialIdno, SerialNo, SbillIdNo, PrefNo + "" + BillNo, ApplicationFunction.mmddyyyy(SBillDate), CityName, PartyName, PartyId);
                    }
                    ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "PartyIdno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "PartyIdno")));

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
                objClaimFrmDAL = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);

            }
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime? ClaimDate = null;
            ClaimDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtClaimDate.Text));
            ClaimFrmDAL objClaimFrmDAL = new ClaimFrmDAL();
            DateTime CurrentDate = System.DateTime.Now;
            Int64 intAgainst= 0;
            if (RDbSaleBill.Checked == true) { intAgainst = 0; } else { intAgainst = 1; }
                
            if (grdMain.Rows.Count > 0)
            {
                DTMain = CreateDt();
                
                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidSerialIdno = (HiddenField)row.FindControl("hidSerialIdno");
                    HiddenField hidSbillIdno = (HiddenField)row.FindControl("hidSbillIdno");
                    TextBox txtDefRemark = (TextBox)row.FindControl("txtDefRemark");
                    TextBox txtVchAppDetl = (TextBox)row.FindControl("txtVchAppDetl");
                    ApplicationFunction.DatatableAddRow(DTMain, row.RowIndex + 1, hidSerialIdno.Value, hidSbillIdno.Value, txtDefRemark.Text, txtVchAppDetl.Text);
                }

                tblClaimHead objClaimHead = new tblClaimHead();
                if (txtClaimDate.Text == "") { objClaimHead.ClaimHead_Date = null; } else { objClaimHead.ClaimHead_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtClaimDate.Text)); }
                objClaimHead.Claim_No = string.IsNullOrEmpty(Convert.ToString(txtClaimNo.Text.Trim())) ? 0 : Convert.ToInt64(txtClaimNo.Text.Trim());
                objClaimHead.Prefix_No = string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim());
                objClaimHead.FromLoc_Idno = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                objClaimHead.Year_Idno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
                objClaimHead.Prty_Idno = string.IsNullOrEmpty(Convert.ToString(ddlParty.SelectedValue)) ? 0 : Convert.ToInt64(ddlParty.SelectedValue);
                objClaimHead.Claim_Against = intAgainst;
                objClaimHead.Comp_Idno = string.IsNullOrEmpty(Convert.ToString(ddlCompanyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlCompanyName.SelectedValue);
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (ViewState["ClaimHeadIdno"] != null)
                    {
                        objClaimHead.Date_Modified = System.DateTime.Now;
                        lnkbtnNew.Visible = false;
                        value = objClaimFrmDAL.Update(objClaimHead, Convert.ToInt32(ViewState["ClaimHeadIdno"].ToString()), DTMain);
                        objClaimFrmDAL = null;
                    }
                    else
                    {
                        objClaimHead.Date_Added = System.DateTime.Now;
                        value = objClaimFrmDAL.Insert(objClaimHead, DTMain);
                        objClaimFrmDAL = null;
                    }

                    if (ViewState["ClaimHeadIdno"] != null)
                    {
                        if (value > 0 && (ViewState["ClaimHeadIdno"] != null))
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
                        if (value > 0 && (ViewState["ClaimHeadIdno"] == null))
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

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ClaimHeadIdno"] != null)
            {
                this.Populate(Convert.ToInt64(Request.QueryString["ClaimHeadIdno"]));
            }
            else
            {
                Response.Redirect("ClaimFrm.aspx");
            }
        }

        #endregion
    }
}

