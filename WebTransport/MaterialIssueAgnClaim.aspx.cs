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
    public partial class MaterialIssueAgnClaim : Pagebase
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

                txtMatIssNo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                this.BindDateRange();
                this.BindPartyName();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange.SelectedIndex = 0; ddlDateRange_SelectedIndexChanged(null, null);
                GetMaxMatIssueNo();
                if (Request.QueryString["MatIssIdno"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["MatIssIdno"].ToString()));
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
                    txtMatIssClaimDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDivDateFrom.Text = hidmindate.Value;
                    txtDivDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtMatIssClaimDate.Text = hidmindate.Value;
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

        private void Populate(Int64 HeadIdno)
        {
            MaterialIssueAgnClaimDAL objMaterialIssueAgnClaimDAL = new MaterialIssueAgnClaimDAL();
            tblMatIssueAgnClaimHead ObjHead = objMaterialIssueAgnClaimDAL.SelectHead(HeadIdno);

            if (ObjHead != null)
            {
                hidid.Value = HeadIdno.ToString();
                ddlDateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtPrefixNo.Text = ObjHead.Prefix_No.ToString();
                txtMatIssClaimDate.Text = string.IsNullOrEmpty(ObjHead.MatIssAgnClaimHead_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.MatIssAgnClaimHead_Date).ToString("dd-MM-yyyy");
                txtMatIssNo.Text = string.IsNullOrEmpty(ObjHead.MatIss_No.ToString()) ? "" : Convert.ToString(ObjHead.MatIss_No);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(ObjHead.FromLoc_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.FromLoc_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(ObjHead.Prty_Idno.ToString()) ? "0" : Convert.ToString(ObjHead.Prty_Idno);
                dtTemp = CreateDt();
                //string Result = String.Empty;
                //var ClaimNo = objMaterialIssueAgnClaimDAL.SelectSaleBillNo(Convert.ToInt64(HeadIdno)); if (ClaimNo != null && ClaimNo > 0) { lblSbillNo.Text = "Sale Bill No : " + ClaimNo.ToString(); }

                var lst= objMaterialIssueAgnClaimDAL.SelectClaimDetails(HeadIdno);
                if (lst != null && lst.Count > 0)
                {
                    grdMain.DataSource = lst;
                    grdMain.DataBind();
                    grdMain.Visible = true;
                }
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
                GetMaxMatIssueNo();
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

        private void GetMaxMatIssueNo()
        {
            MaterialIssueAgnClaimDAL objMaterialIssueAgnClaimDAL = new MaterialIssueAgnClaimDAL();
            txtMatIssNo.Text = objMaterialIssueAgnClaimDAL.GetMatIssueMax(string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(txtPrefixNo.Text.Trim()) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()),  Convert.ToInt64(ddlDateRange.SelectedValue)).ToString();
            objMaterialIssueAgnClaimDAL = null;
        }
        private DataTable CreateDivDt()
        {
            DataTable temp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "HeadIdno", "String",
                "HeadDate", "String",
                "ClaimNo", "String",
                "PrtyName", "String",
                "FromCityName", "String",
                "CompName","String");
            return temp;
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tblMain",
                "Id", "String",
                "SerialIdno", "String");
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
            if (ddlFromCity.SelectedValue == "0") { ShowMessageErr("Please Select first City."); return; }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true); }
        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterialIssueAgnClaim.aspx");
        }
        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MaterialIssueAgnClaimDAL objMaterialIssueAgnClaimDAL = new MaterialIssueAgnClaimDAL();
                if ((grdSearchRecords != null) && (grdSearchRecords.Rows.Count > 0))
                {   
                    string CalimIdDetlValue = string.Empty;
                    for (int count = 0; count < grdSearchRecords.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdSearchRecords.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidHeadIdno = (HiddenField)grdSearchRecords.Rows[count].FindControl("hidHeadIdno");
                            CalimIdDetlValue = CalimIdDetlValue + hidHeadIdno.Value + ",";
                        }
                    }
                    if (CalimIdDetlValue != "")
                    {
                        CalimIdDetlValue = CalimIdDetlValue.Substring(0, CalimIdDetlValue.Length - 1);
                    }
                    if (CalimIdDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Claim Number.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                    }
                    else
                    {
                        var Lst = objMaterialIssueAgnClaimDAL.Select(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(CalimIdDetlValue));
                        if (Lst != null && Lst.Count > 0)
                        {
                            grdMain.DataSource = Lst;
                            grdMain.DataBind();
                            grdMain.Visible = true;
                        }
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

            if (txtCalimNo.Text == "" && ddlDivPrtyName.SelectedValue == "0") { DivErrorMsg.Visible = true; lblDivErrorMsg.InnerText = "* Please Select Claim Number or Party."; ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true); return; }
            else
            {
                grdSearchRecords.Visible = true;
                MaterialIssueAgnClaimDAL objMaterialIssueAgnClaimDAL = new MaterialIssueAgnClaimDAL();
                DataTable dt = CreateDivDt();
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                DateTime? DateFrom = null; DateTime? DateTo = null;
                DateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateFrom.Text));
                DateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateTo.Text));
                Int64 Location = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlDivPrtyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlDivPrtyName.SelectedValue);
                string ClaimNo = (string.IsNullOrEmpty(Convert.ToString(txtCalimNo.Text.Trim())) ? "" : Convert.ToString(txtCalimNo.Text.Trim())) + "" + (string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()));

                var lst = objMaterialIssueAgnClaimDAL.SelectForSearch(DateFrom, DateTo, ClaimNo, PartyIdno, Location, intyearid);
                objMaterialIssueAgnClaimDAL = null;

                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string HeadIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "HeadIdno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "HeadIdno")));
                        string HeadDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "HeadDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[i], "HeadDate")).ToString("dd-MM-yyyy"));
                        string PrefNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PrefNo"));
                        string ClaimNumber = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "ClaimNo"));
                        string PrtyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PrtyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PrtyName"));
                        string FromCityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "FromCityName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "FromCityName"));
                        string CompName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CompName"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "CompName"));
                        //string CompIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "CompIdno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "CompIdno"));
                        //string FromCityIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "FromCityIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "FromCityIdno"));
                        

                        ApplicationFunction.DatatableAddRow(dt, i + 1, HeadIdno, ApplicationFunction.mmddyyyy(HeadDate), (PrefNo + "" + ClaimNumber), PrtyName, FromCityName, CompName);
                    }
                    HidHeadIdno.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "HeadIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[0], "HeadIdno"));
                    ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "PrtyIdno"))) ? "" : Convert.ToString(DataBinder.Eval(lst[0], "PrtyIdno"));
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
            ClaimDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtMatIssClaimDate.Text));
            MaterialIssueAgnClaimDAL objMaterialIssueAgnClaimDAL = new MaterialIssueAgnClaimDAL();
            DateTime CurrentDate = System.DateTime.Now;
            if (grdMain.Rows.Count > 0)
            {
                DTMain = CreateDt();
                
                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidSerialIdno = (HiddenField)row.FindControl("hidSerialIdno");
                    ApplicationFunction.DatatableAddRow(DTMain, row.RowIndex + 1, hidSerialIdno.Value);
                }

                tblMatIssueAgnClaimHead objClaimHead = new tblMatIssueAgnClaimHead();
                if (txtMatIssClaimDate.Text == "") { objClaimHead.MatIssAgnClaimHead_Date = null; } else { objClaimHead.MatIssAgnClaimHead_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtMatIssClaimDate.Text)); }
                objClaimHead.MatIss_No = string.IsNullOrEmpty(Convert.ToString(txtMatIssNo.Text.Trim())) ? 0 : Convert.ToInt64(txtMatIssNo.Text.Trim());
                objClaimHead.Prefix_No = string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim());
                objClaimHead.FromLoc_Idno = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
                objClaimHead.Year_Idno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
                objClaimHead.Prty_Idno = string.IsNullOrEmpty(Convert.ToString(ddlParty.SelectedValue)) ? 0 : Convert.ToInt64(ddlParty.SelectedValue);
                objClaimHead.ClaimToComHead_Idno = string.IsNullOrEmpty(Convert.ToString(HidHeadIdno.Value)) ? 0 : Convert.ToInt64(HidHeadIdno.Value);
                    
                Int64 value = 0;
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        objClaimHead.Date_Added = System.DateTime.Now;
                        value = objMaterialIssueAgnClaimDAL.Insert(objClaimHead, DTMain);
                        objMaterialIssueAgnClaimDAL = null;
                    }
                    else
                    {
                        objClaimHead.Date_Modified = System.DateTime.Now;
                        lnkbtnNew.Visible = false;
                        value = objMaterialIssueAgnClaimDAL.Update(objClaimHead, Convert.ToInt32(hidid.Value), DTMain);
                        objMaterialIssueAgnClaimDAL = null;
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
                            ShowMessageErr("Material Issue Number Already Exist");
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

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["MatIsAgnClmHeadIdno"] != null)
            {
                this.Populate(Convert.ToInt64(Request.QueryString["MatIsAgnClmHeadIdno"]));
            }
            else
            {
                Response.Redirect("MaterialIssueAgnClaim.aspx");
            }
        }

        #endregion
    }
}

