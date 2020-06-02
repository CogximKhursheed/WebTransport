using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Net;
using System.Collections.Generic;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class VchrEntry : Pagebase
    {
        #region Private Variable...
        string conString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
        DataSet dsVchrEntry, dsVchrEntryDet, dsDgvMainCombo, dsLoad; DataSet DSCostPopulate = new DataSet();
        DataTable DtTemp = new DataTable(); DataTable dt;
        SqlTransaction Tran;
        string msg = string.Empty, IdNo = string.Empty, strDrCr = string.Empty;
        int itm_id = 0;
        String BoxNo = string.Empty;
        Double dTotAmnt = 0, dTotAmount = 0, dblTemp = 0, staticValue = 0;
        int totQty = 0;
        double totamnt = 0, rptTotalAmount = 0, creditTotalAmount = 0, debitTotalAmount = 0;
        private int intFormId = 49; DataTable DtCost = new DataTable(); DataTable DtCostGrid = new DataTable();
        #endregion

        #region Page Event...
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (base.Print == false)
                {
                    lnkbtnPrint.Visible = false;
                }
                ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
                _scriptMan.AsyncPostBackTimeout = 36000;
                Program.Main();
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtVchrModeAmnt.Attributes.Add("onkeypress", "return validateQty(event);");
                txtLdgrAmnt.Attributes.Add("onkeypress", "return validateQty(event);");
                txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtInstNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtCustBankName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                BindDateRange();
                DtCost = CreateCostcenterDt();
                ViewState["DtCost"] = DtCost;
                ViewState["DtGridCost"] = DtCost;
                SetDate();
                HidVchrTyp.Value = "1"; lblMsgGrid.Text = "";
                //   BindCompany();
                //this.BindCostCenterVchrMode();
                //this.BindCostCenterLedgrName();
                DtTemp = CreateDt();
                ViewState["dt"] = DtTemp;
                grdMain.DataSource = DtTemp;
                grdMain.DataBind();
                ddlLdgrTyp.SelectedIndex = 1;
                if (Request.QueryString["VchrIdno"] != null)
                {
                    IdNo = HidheadID.Value = Request.QueryString["VchrIdno"];
                    loaddsVchrEntry(Convert.ToInt32(IdNo));
                    if ((dsVchrEntry != null) && (dsVchrEntry.Tables.Count > 0) && (dsVchrEntry.Tables[0].Rows.Count > 0))
                    {
                        loaddsVchrEntryDet(Convert.ToInt32(IdNo));
                        loaddsVchrCostDet(Convert.ToInt32(IdNo));
                        if ((dsVchrEntryDet != null) && (dsVchrEntryDet.Tables.Count > 0) && (dsVchrEntryDet.Tables[0].Rows.Count > 0))
                        {
                            this.Populate(Convert.ToInt32(Request.QueryString["VchrIdno"]));
                        }
                    }
                    //  ddlcompany.Enabled = ddlVchrType.Enabled = ddlVchrModeTyp.Enabled = false;
                }
                if (Request.QueryString["ACB"] == "")
                {
                    //lblbreadcrumb.Text = "Account Book Report";
                    //lblbreadcrumb1.Text = "Ledger Report Details";
                }
                else
                {
                    //lblbreadcrumb.Visible = false;
                    //lblbreadcrumb1.Visible = false;
                }
            }
            if (hidmaxdate.Value.Length > 10)
            {
                hidmaxdate.Value = Convert.ToString(hidmaxdate.Value).Substring(0, 10);
            }
            if (hidmindate.Value.Length > 10)
            {
                hidmindate.Value = Convert.ToString(hidmindate.Value).Substring(0, 10);
            }
            //  ddlcompany.Focus();
            ddlDateRange.Focus();
            //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
            //RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");
        }
        #endregion

        #region Bind Functions...
        private void SetPriviousData()
        {
            DtCostGrid = CreateCostcenterDt();
            ViewState["DtGridCost"] = DtCostGrid;
            foreach (GridViewRow row in grdCostdetls.Rows)
            {
                DropDownList ddltruckno = (DropDownList)row.FindControl("ddlTruckno");
                HiddenField hidTruckIdno = (HiddenField)row.FindControl("hidTruckIdno");
                HiddenField hidCostGriRowId = (HiddenField)row.FindControl("hidCostRowId");
                TextBox txtGridAmnt = (TextBox)row.FindControl("txtAmnt");
                if (ddltruckno.SelectedIndex <= 0)
                {
                    lblMsgGrid.Text = "please select Truck No";
                    ddltruckno.Focus();
                    return;
                }
                else
                {
                    lblMsgGrid.Text = "";
                }
                if (txtGridAmnt.Text == "" || (Convert.ToDouble(txtGridAmnt.Text) <= 0))
                {
                    txtGridAmnt.Text = "0.00";
                    lblMsgGrid.Text = "please enter Amount";
                    txtGridAmnt.Focus();
                    return;
                }
                else
                {
                    lblMsgGrid.Text = "";
                }
                ApplicationFunction.DatatableAddRow(DtCostGrid, Convert.ToString(ddltruckno.Text), Convert.ToDouble(txtGridAmnt.Text).ToString("N2"), hidTruckIdno.Value, hidCostGriRowId.Value);
            }
            double SumAmnt = GetSum(DtCostGrid);
            if (Convert.ToDouble((Convert.ToDouble(hidAmount.Value) - SumAmnt)) > 0)
            {
                ApplicationFunction.DatatableAddRow(DtCostGrid, "", ((Convert.ToDouble((Convert.ToDouble(hidAmount.Value) - SumAmnt))) > 0) ? Convert.ToDouble((Convert.ToDouble(hidAmount.Value) - SumAmnt)).ToString("N2") : "0.00", 0, Convert.ToInt32(hidCostRowId.Value));
            }
            ViewState["DtGridCost"] = DtCostGrid;
            BindCostCenterGrid();
        }
        private double GetSum(DataTable Dt)
        {
            double dAmnt = 0;
            if (Dt != null && Dt.Rows.Count > 0)
            {
                foreach (DataRow Dr in Dt.Rows)
                {
                    if ((Convert.ToInt32(Dr["Truck_Idno"]) > 0) && (Convert.ToDouble(Dr["Amount"]) > 0))
                    {
                        dAmnt += Convert.ToDouble(Dr["Amount"]);
                    }

                }
            }
            return dAmnt;
        }
        private void btnVtCommonFun(int iVType)
        {
            // Int64 CompanyIdno = Convert.ToInt64(ddlcompany.SelectedValue);
            if (iVType == 1)
            {
                ddlVchrModeTyp.SelectedIndex = 0; ddlVchrModeTyp.Enabled = false;
                ddlLdgrTyp.SelectedIndex = 1;
            }
            else if (iVType == 2)
            {
                ddlVchrModeTyp.SelectedIndex = 1; ddlVchrModeTyp.Enabled = false;
                ddlLdgrTyp.SelectedIndex = 0;
            }
            else if (iVType == 7)
            {
                ddlVchrModeTyp.SelectedIndex = 0; ddlVchrModeTyp.Enabled = false;
                ddlLdgrTyp.SelectedIndex = 1; ddlLdgrTyp.Enabled = false;
            }
            else
            {
                ddlVchrModeTyp.SelectedIndex = 0; ddlVchrModeTyp.Enabled = true;
                ddlLdgrTyp.SelectedIndex = 1;
            }
            if (HidheadID.Value == string.Empty)
            {
                txtVchrNo.Text = "Trn " + Convert.ToString(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(MAX(VCHR_NO),0) + 1 AS MAXID FROM VchrHead WHERE VCHR_TYPE='" + iVType + "' AND VCHR_HIDN = 0 AND Year_Idno='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "'"));
                txtDate.Text = ApplicationFunction.GetIndianDateTime().Date.ToString("dd-MM-yyyy");
                txtVchrNarr.Text = txtVchrNarr.Text = "";
                txtLdgrAmnt.Text = txtVchrModeAmnt.Text = "0.00";
                loaddsLoadBlank();
                // ddlcompany.Enabled = ddlVchrType.Enabled = true;
            }
            lblVchrHdrName.Text = Convert.ToString(ddlVchrType.SelectedItem.Text);
            BindVchrMode(iVType);//, CompanyIdno);
            BindLedger(iVType);//, CompanyIdno);
        }

        private void BindTruckNo(DropDownList ddl)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddl.DataSource = lst;
                ddl.DataTextField = "Lorry_No";
                ddl.DataValueField = "Lorry_Idno";
                ddl.DataBind();

            }
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        //private void BindCompany()
        //{
        //    BindDropdownDAL CompanyDAL = new BindDropdownDAL();
        //    Int32 ALL = 1;
        //    if (base.UserIdno == 1)
        //    {
        //        ALL = 1;
        //    }
        //    else
        //    {
        //        ALL = Convert.ToInt32(base.CompId);
        //    }

        //    var lst = CompanyDAL.SelectCompany(ALL);
        //    CompanyDAL = null;
        //    ddlcompany.DataSource = lst;
        //    ddlcompany.DataTextField = "Name";
        //    ddlcompany.DataValueField = "CompMast_Idno";
        //    ddlcompany.DataBind();
        //    if (base.UserIdno == 1)
        //    {
        //        ddlcompany.Items.Insert(0, new ListItem("< Choose Company >", "0"));
        //    }
        //    else
        //    {
        //        btnVtCommonFun(Convert.ToInt32(HidVchrTyp.Value));
        //    }
        //}

        public void BindVchrMode(int iVType)//, Int64 CompanyIdno)
        {
            string strAct = String.Empty;
            strAct = "SelectVchrMode";
            VchrEntryDAL objVchrEntryDAL = new VchrEntryDAL();
            var lstLocation = objVchrEntryDAL.spVoucherEntry(ApplicationFunction.ConnectionString(), "SelectVchrMode", 0, "", "", iVType, 0, "", 0, 0, 0, "", 0, 0, 0, 0, "", 0, 0, "", 0, 0, 0, "", 0, "", "", 0, 0, "", "", 0, 0, 0, 0, ""); //, CompanyIdno);
            ddlVchrMode.DataSource = lstLocation;
            ddlVchrMode.DataTextField = "Acnt_Name";
            ddlVchrMode.DataValueField = "ID";
            ddlVchrMode.DataBind();
            ddlVchrMode.Items.Insert(0, new ListItem("--- Select Ledger Name ---", "0"));
            objVchrEntryDAL = null;
        }

        public void BindLedger(int iVType)//, Int64 CompanyIdno)
        {
            VchrEntryDAL objVchrEntryDAL = new VchrEntryDAL();
            dsDgvMainCombo = objVchrEntryDAL.spVoucherEntry(ApplicationFunction.ConnectionString(), "SelectLedger", 0, "", "", iVType, 0, "", 0, 0, 0, "", 0, 0, 0, 0, "", 0, 0, "", 0, 0, 0, "", 0, "", "", 0, 0, "", "", 0, 0, 0, 0, ""); //, CompanyIdno);
            if ((dsDgvMainCombo != null) && (dsDgvMainCombo.Tables.Count > 0) && (dsDgvMainCombo.Tables[0].Rows.Count > 0))
            {
                if (dsDgvMainCombo.Tables[0].Rows.Count > 0)
                    ddlLedgrName.DataSource = null;
                ddlLedgrName.DataBind();
                ddlLedgrName.DataSource = dsDgvMainCombo;
                ddlLedgrName.DataTextField = "ACNT_NAME";
                ddlLedgrName.DataValueField = "ID";
                ddlLedgrName.DataBind();
                objVchrEntryDAL = null;
                ddlLedgrName.Items.Insert(0, new ListItem("--Select Ledger--", "0"));
            }
        }

        private void loaddsLoadBlank()
        {
            dsLoad = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select '' as Ledger_Nm,'   ' as Amount,'' as Narration,'' as Curr_Bal,'' as Ledger_Id,'' as Vchr_IdNo,'' AS Inst_Type,'' AS Inst_No, '' AS Inst_Date,'' as Bank_Date, '' AS Cust_Bank, '' AS InstType_Id,'' as Id");
            dsLoad.Tables[0].Rows[0].Delete();
            grdMain.DataSource = dsLoad.Tables[0];
            grdMain.DataBind();
        }

        private void Populate(int PackListHeadIdno)
        {
            lblInfobtn.Visible = true;
            int iVchrTyp = (Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_TYPE"]) == "" ? 0 : Convert.ToInt32(dsVchrEntry.Tables[0].Rows[0]["VCHR_TYPE"]));
            btnVtCommonFun(iVchrTyp);
            if (dsVchrEntry.Tables[0].Rows.Count > 0)
            {
                HidVchrTyp.Value = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_TYPE"].ToString());
                HidheadID.Value = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_IDNO"].ToString());
                txtDate.Text = Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["VCHR_DATE"]).ToString("dd-MM-yyyy");
                txtVchrNo.Text = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_NO"].ToString());
                txtVchrNarr.Text = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_NARR"].ToString());
                ddlVchrType.SelectedValue = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_TYPE"].ToString());
                // ddlcompany.SelectedValue = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["Comp_Idno"].ToString());
                ddlcompany_SelectedIndexChanged(null, null);
                //  ddlcompany.Enabled = false;
                ddlVchrModeTyp.SelectedValue = Convert.ToString(dsVchrEntry.Tables[1].Rows[0]["Amnt_Type"].ToString());
                ddlVchrModeTyp_SelectedIndexChanged(null, null);
                ddlVchrMode.SelectedValue = Convert.ToString(dsVchrEntry.Tables[1].Rows[0]["Acnt_Idno"].ToString());
                txtVchrModeAmnt.Text = Convert.ToDouble(dsVchrEntry.Tables[1].Rows[0]["Acnt_Amnt"]).ToString("N2");
                ddlVchrMode_SelectedIndexChanged(null, null);
                //ddlVchrModeCostCnter.SelectedValue = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["CostCenterIdno"].ToString());
                //hidRcptNo.Value = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["PayRcpt_Idno"]);
                try
                {
                    lblGeneratedByName.Text = Convert.ToString((dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"] == null) ? "" : "Generated by: " + dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"]) + " - " + ((dsVchrEntry.Tables[0].Rows[0]["Date_Added"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["Date_Added"] == null) ? "" : Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["Date_Added"]).ToString("MM/dd/yyyy h:mm tt"));
                    lblLastUpdatedByName.Text = Convert.ToString((dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"] == null) ? "" : "Last Updated by: " + dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"]) + " - " + ((dsVchrEntry.Tables[0].Rows[0]["Date_Modified"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["Date_Modified"] == null) ? "" : Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["Date_Modified"]).ToString("MM/dd/yyyy h:mm tt"));
                    lblFrmGeneratedByName.Text = Convert.ToString((dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"] == null) ? "" : dsVchrEntry.Tables[0].Rows[0]["GeneratedBy"]) + " - " + ((dsVchrEntry.Tables[0].Rows[0]["Date_Added"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["Date_Added"] == null) ? "" : Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["Date_Added"]).ToString("MM/dd/yyyy h:mm tt"));
                    lblFrmLastUpdatedByName.Text = Convert.ToString((dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"] == null) ? "" : dsVchrEntry.Tables[0].Rows[0]["ModifiedBy"]) + " - " + ((dsVchrEntry.Tables[0].Rows[0]["Date_Modified"].ToString() == "" || dsVchrEntry.Tables[0].Rows[0]["Date_Modified"] == null) ? "" : Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["Date_Modified"]).ToString("MM/dd/yyyy h:mm tt"));
                }
                catch (Exception ex)
                {
                    lblGeneratedByName.Text = "";
                    lblLastUpdatedByName.Text = "";
                    lblFrmGeneratedByName.Text = "-NAN-";
                    lblFrmLastUpdatedByName.Text = "-NAN-";
                }
            }
            if (lblVchrModeCurBal.Text.Trim() != "")
            {
                strDrCr = Convert.ToString(Convert.ToString(lblVchrModeCurBal.Text).Substring(Convert.ToString(lblVchrModeCurBal.Text).Length - 3));
                staticValue = Convert.ToDouble(lblVchrModeCurBal.Text.Substring(0, lblVchrModeCurBal.Text.LastIndexOf("  ")));
            }
            //  Select All From tblVchrListDetl
            if (dsVchrEntryDet.Tables[0].Rows.Count > 0)
            {
                DtTemp = (DataTable)ViewState["dt"];
                DtTemp.Clear();
                if (ViewState["dt"] != null)
                {
                    for (int i = 0; i < dsVchrEntryDet.Tables[0].Rows.Count; i++)
                    {
                        //int id = 1;
                        //if (i != 0)
                        //{
                        //    if ((DtTemp != null) && (DtTemp.Rows.Count > 0))
                        //    {
                        //        id = DtTemp.Rows.Count + 1;
                        //    }
                        //}
                        string strDt = (Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Inst_Date"]) != "" ? (Convert.ToDateTime(dsVchrEntryDet.Tables[0].Rows[i]["Inst_Date"]) > Convert.ToDateTime("01-01-1901") ? Convert.ToDateTime(dsVchrEntryDet.Tables[0].Rows[i]["Inst_Date"]).ToString("dd-MM-yyyy") : "") : "");
                        string strRecon = (Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Bank_Date"]) != "" ? (Convert.ToDateTime(dsVchrEntryDet.Tables[0].Rows[i]["Bank_Date"]) > Convert.ToDateTime("01-01-1901") ? Convert.ToDateTime(dsVchrEntryDet.Tables[0].Rows[i]["Bank_Date"]).ToString("dd-MM-yyyy") : "") : "");

                        ApplicationFunction.DatatableAddRow(DtTemp, Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Ledger_Nm"]), Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Amount"]),
                        Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Curr_Bal"]), Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Ledger_Id"]), Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Vchr_IdNo"]),
                        Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Inst_Type"]), Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Inst_No"]), strDt, strRecon, Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["Cust_Bank"]),
                        Convert.ToString(dsVchrEntryDet.Tables[0].Rows[i]["InstType_Id"]), Convert.ToInt64(dsVchrEntryDet.Tables[0].Rows[i]["Id"]));
                    }
                }
            }
            if ((DSCostPopulate != null) && (DSCostPopulate.Tables.Count > 0) && (DSCostPopulate.Tables[0].Rows.Count > 0))
            {
                DtCost = (DataTable)ViewState["DtCost"];
                DtCost.Clear();
                if (ViewState["DtCost"] != null)
                {
                    for (int i = 0; i < DSCostPopulate.Tables[0].Rows.Count; i++)
                    {
                        //int id = 1;
                        //if (i != 0)
                        //{
                        //    if ((DtCost != null) && (DtCost.Rows.Count > 0))
                        //    {
                        //        id = DtCost.Rows.Count + 1;
                        //    }
                        //}

                        ApplicationFunction.DatatableAddRow(DtCost, Convert.ToString(DSCostPopulate.Tables[0].Rows[i]["Truck_No"]), Convert.ToString(DSCostPopulate.Tables[0].Rows[i]["Amount"]), Convert.ToString(DSCostPopulate.Tables[0].Rows[i]["Truck_Idno"]), Convert.ToInt64(DSCostPopulate.Tables[0].Rows[i]["Id"]));
                    }
                    ViewState["DtCost"] = DtCost;
                }
            }
            #region printing start.......
            VchrEntryDAL objDAL = new VchrEntryDAL();
            //  int CompIdno = Convert.ToInt32(ddlcompany.SelectedValue);
            //  CompMast lst = objDAL.SelectCompDetails(CompIdno);
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            # region Company Details  # region Company Details
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
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
            lblCompanyname.Text = CompName; //lblcompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompTIN.Text = TinNo.ToString();
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;

            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }

            #endregion
            if (ddlVchrType.SelectedValue == "1" || ddlVchrType.SelectedValue == "2")
            {
                if (DtTemp.Rows.Count > 0)
                {
                    rptpaymentvoucher.DataSource = DtTemp;
                    rptpaymentvoucher.DataBind();
                    System.Web.UI.Control HeaderTemplate = rptpaymentvoucher.Controls[0].Controls[0];
                    System.Web.UI.Control FooterTemplate = rptpaymentvoucher.Controls[rptpaymentvoucher.Controls.Count - 1].Controls[0];
                    Label lblPrintHeadng = HeaderTemplate.FindControl("lblPrintHeadng") as Label;
                    lblPrintHeadng.Text = Convert.ToString(ddlVchrType.SelectedItem.Text);
                    Label lblno = HeaderTemplate.FindControl("lblno") as Label;
                    lblno.Text = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_NO"].ToString());
                    Label lbldated = HeaderTemplate.FindControl("lbldated") as Label;
                    lbldated.Text = Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["VCHR_DATE"]).ToString("dd-MM-yyyy");
                    Label lblthrough = FooterTemplate.FindControl("lblthrough") as Label;
                    lblthrough.Text = ddlVchrMode.SelectedItem.Text;
                    Label lblthroughnarration = FooterTemplate.FindControl("lblthroughnarration") as Label;
                    lblthroughnarration.Text = Convert.ToString(dsVchrEntry.Tables[1].Rows[0]["Narr_Text"].ToString());
                    Label lblamountinwords = FooterTemplate.FindControl("lblamountinwords") as Label;
                    Label lblremarks = FooterTemplate.FindControl("lblremarks") as Label;
                    lblremarks.Text = string.IsNullOrEmpty(Convert.ToString(txtVchrNarr.Text)) ? "" : "<strong>" + lblVchrHdrName.Text + " Remarks:</strong> " + txtVchrNarr.Text;
                    lblamountinwords.Text = "Rs. " + NumberToText(Convert.ToInt32(rptTotalAmount)) + " Only";
                    Label lblcomp = HeaderTemplate.FindControl("lblcomp") as Label;
                    //if (lst != null)
                    //{
                    //    lblcomp.Text = "<strong>" + lst.Name + "</strong><br/>" + lst.Adress1 + " , " + Environment.NewLine + lst.Adress2 + " - " + lst.Pin;
                    //}
                    lnkbtnPrint.Visible = true;
                }
            }
            else if (ddlVchrType.SelectedValue == "3" || ddlVchrType.SelectedValue == "4" || ddlVchrType.SelectedValue == "5" || ddlVchrType.SelectedValue == "6" || ddlVchrType.SelectedValue == "7")
            {
                rptJournalContra.DataSource = DtTemp;
                rptJournalContra.DataBind();
                System.Web.UI.Control HeaderTemplate = rptJournalContra.Controls[0].Controls[0];
                System.Web.UI.Control FooterTemplate = rptJournalContra.Controls[rptJournalContra.Controls.Count - 1].Controls[0];

                Label lblPrintHeadng = HeaderTemplate.FindControl("lblPrintHeadng") as Label;
                lblPrintHeadng.Text = Convert.ToString(ddlVchrType.SelectedItem.Text);
                Label lblno = HeaderTemplate.FindControl("lblno") as Label;
                lblno.Text = Convert.ToString(dsVchrEntry.Tables[0].Rows[0]["VCHR_NO"].ToString());
                Label lbldated = HeaderTemplate.FindControl("lbldated") as Label;
                lbldated.Text = Convert.ToDateTime(dsVchrEntry.Tables[0].Rows[0]["VCHR_DATE"]).ToString("dd-MM-yyyy");
                Label lblParticulars = HeaderTemplate.FindControl("lblParticulars") as Label;
                lblParticulars.Text = ddlVchrMode.SelectedItem.Text;
                Label lblParticularNarration = HeaderTemplate.FindControl("lblParticularNarration") as Label;
                lblParticularNarration.Text = Convert.ToString(dsVchrEntry.Tables[1].Rows[0]["Narr_Text"].ToString());

                if (ddlVchrModeTyp.SelectedValue == "1")//  credit
                {
                    Label lblCredit = HeaderTemplate.FindControl("lblCredit") as Label;
                    lblCredit.Text = Convert.ToDouble(dsVchrEntry.Tables[1].Rows[0]["Acnt_Amnt"]).ToString("N2");
                    creditTotalAmount += Convert.ToDouble(dsVchrEntry.Tables[1].Rows[0]["Acnt_Amnt"]);
                }
                else
                {
                    Label lblDebit = HeaderTemplate.FindControl("lblDebit") as Label;
                    lblDebit.Text = Convert.ToDouble(dsVchrEntry.Tables[1].Rows[0]["Acnt_Amnt"]).ToString("N2");
                    debitTotalAmount += Convert.ToDouble(dsVchrEntry.Tables[1].Rows[0]["Acnt_Amnt"]);
                }
                Label lblremarks = FooterTemplate.FindControl("lblremarks") as Label;
                lblremarks.Text = string.IsNullOrEmpty(Convert.ToString(txtVchrNarr.Text)) ? "" : "<strong>" + ddlVchrType.SelectedItem.Text + " Remarks:</strong> " + txtVchrNarr.Text;
                Label lblDebitAmount = FooterTemplate.FindControl("lblDebitAmount") as Label;
                Label lblCreditAmount = FooterTemplate.FindControl("lblCreditAmount") as Label;
                lblDebitAmount.Text = debitTotalAmount.ToString("N2");
                lblCreditAmount.Text = creditTotalAmount.ToString("N2");
                Label lblcomp = HeaderTemplate.FindControl("lblcomp") as Label;
                //if (lst != null)
                //{
                //    lblcomp.Text = "<strong>" + lst.Name + "</strong><br/>" + lst.Adress1 + " , " + Environment.NewLine + lst.Adress2 + " - " + lst.Pin;
                //}
                lnkbtnPrint.Visible = true;
            }
            #endregion

            this.BindGrid();
        }

        public string NumberToText(int number)
        {
            if (number == 0) return "Zero";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        private Int64 checkNegStock(Int64 ItemIdno)
        {
            long iCurrentStock = 0;
            return iCurrentStock;
        }
        #endregion

        #region Other Functions...

        private void CurBalLoad(int c)
        {
            double db, cr, dbopbal, cropbal, totdb, totcr;
            db = cr = dbopbal = cropbal = totdb = totcr = 0;

            if (HidheadID.Value == string.Empty)
            {
                if ((c == 1) && (Convert.ToString(ddlLedgrName.SelectedValue) != "0") && (txtDate.Text != ""))
                {
                    SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString());
                    db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=2, @ACNTIDNO='" + Convert.ToString(ddlLedgrName.SelectedValue) + "',@YEARIDNO='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "'", con,true);
                    cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=1, @ACNTIDNO='" + Convert.ToString(ddlLedgrName.SelectedValue) + "',@YEARIDNO='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "'", con,true);
                    dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(ddlLedgrName.SelectedValue) + "',@YearIdno='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "'", con, true);
                    cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(ddlLedgrName.SelectedValue) + "',@YearIdno='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "'", con, true);
                    totdb = db + dbopbal; totcr = cr + cropbal;
                    if (totdb > totcr)
                    {
                        lblLedgrNmCurBal.Text = Convert.ToDouble(totdb - totcr).ToString("N2") + "  Dr.";
                    }
                    else if (totdb < totcr)
                    {
                        lblLedgrNmCurBal.Text = Convert.ToDouble(totcr - totdb).ToString("N2") + "  Cr.";
                    }
                    else
                        lblLedgrNmCurBal.Text = "0.00  Dr.";
                }
            }
            else
            {
                if ((c == 1) && DtTemp.Rows.Count > 0 && (txtDate.Text != ""))
                {
                    for (int i = 0; i < DtTemp.Rows.Count; i++)
                    {
                        SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString());
                        db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=2, @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                        cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=1, @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                        dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                        cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                        totdb = db + dbopbal; totcr = cr + cropbal;
                        if (totdb > totcr)
                        {
                            DtTemp.Rows[i]["Curr_Bal"] = Convert.ToDouble(totdb + Convert.ToDouble((DtTemp.Rows[i]["Amount"] == "" ? "0" : DtTemp.Rows[i]["Amount"])) - totcr).ToString("N2") + "  Dr.";
                        }
                        else if (totdb < totcr)
                        {
                            DtTemp.Rows[i]["Curr_Bal"] = Convert.ToDouble(totcr + Convert.ToDouble((DtTemp.Rows[i]["Amount"] == "" ? "0" : DtTemp.Rows[i]["Amount"])) - totdb).ToString("N2") + "  Cr.";
                        }
                        else
                            DtTemp.Rows[i]["Curr_Bal"] = "0.00  Dr.";
                    }
                }

            }
            if (c == 2)    // CURRENT BALANCE OF A VOUCHER MODE, DISPLAY IN TEXT BOX
            {
                if (ddlVchrMode.SelectedIndex >= 0)
                {
                    SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString());
                    db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=2, @ACNTIDNO='" + Convert.ToString(ddlVchrMode.SelectedValue) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SELECTCRDR',@AMNTTYPE=1, @ACNTIDNO='" + Convert.ToString(ddlVchrMode.SelectedValue) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(ddlVchrMode.SelectedValue) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(ddlVchrMode.SelectedValue) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    totdb = db + dbopbal; totcr = cr + cropbal;
                    if (totdb > totcr)
                        lblVchrModeCurBal.Text = Convert.ToDouble(totdb - totcr).ToString("N2") + "  Dr";
                    else if (totdb < totcr)
                        lblVchrModeCurBal.Text = Convert.ToDouble(totcr - totdb).ToString("N2") + "  Cr";
                    else
                        lblVchrModeCurBal.Text = "0.00  Dr";
                }
                else
                    lblVchrModeCurBal.Text = "";
                hidCurbal.Value = lblVchrModeCurBal.Text;
            }
        }

        private void ClearControl()
        {
            ddlLedgrName.SelectedValue = "0";
            txtLdgrAmnt.Text = "0.00";
            lblLedgrNmCurBal.Text = string.Empty;
            imgbtnInstDetEdit.Visible = false; hidrowid.Value = string.Empty;
            txtInstNo.Text = txtInstDate.Text = txtCustBankName.Text = string.Empty;
            txtEntryNarr.Text = string.Empty;
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private bool ValidateGrid()
        {
            string strMsg = string.Empty;
            if (txtDate.Text == "" && Convert.ToString(txtDate.Text).Length != 10)
            {
                txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
                strMsg = "Please fill the voucher date properly.";
                ShowMessageErr(strMsg);
                txtDate.Focus(); return false;
            }
            else if (Convert.ToDouble(txtVchrModeAmnt.Text) == 0)
            {
                strMsg = "Please enter Amount.";
                ShowMessageErr(strMsg);
                txtVchrModeAmnt.Focus(); return false;
            }
            else if (Convert.ToDouble(txtLdgrAmnt.Text) == 0)
            {
                strMsg = "Please enter Amount.";
                ShowMessageErr(strMsg);
                txtLdgrAmnt.Focus(); return false;
            }
            if (ddlVchrType.SelectedIndex < 2)
            {
                int iLdrtyp = Convert.ToInt32(SqlHelper.ExecuteScalar(ApplicationFunction.ConnectionString(), CommandType.Text, "Exec [spVoucherEntry] @Action='SelectLdrType',@AcntIdno='" + Convert.ToString(ddlVchrMode.SelectedValue) + "'"));
                if (iLdrtyp == 4)
                {
                    lblHeader.Text = ddlVchrMode.SelectedItem.Text;
                    if (ddlInstType.SelectedIndex == 0)
                    {
                       
                            //strMsg = "Please select instrument type for bank account " + ddlVchrMode.SelectedItem.Text;
                            strMsg = "Please select instrument type.";
                            lblErrorMsg.Text = strMsg;
                            DivError.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                            ddlInstType.Focus();
                            return false;
                       
                    }
                    else if ((txtInstNo.Text.Replace(",", "").Trim() == "") || (txtInstNo.Text.Replace(",", "").Trim().Length != 6))
                    {
                        if ((ddlInstType.SelectedValue != "5") && (ddlVchrType.SelectedValue != "1"))
                        {
                            txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                            txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                            txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
                            //strMsg = "Please fill 6 digit instrument no. for bank account " + ddlVchrMode.SelectedItem.Text;
                            strMsg = "Instrument No. should be in 6 digit.";
                            lblErrorMsg.Text = strMsg;
                            DivError.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                            txtInstNo.Focus();
                            return false;
                        }
                       
                    }
                    else if (txtInstDate.Text.Replace(",", "").Trim() == "")
                    {
                       
                            txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                            txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                            txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
                            //strMsg = "Please select instrument date for bank account " + ddlVchrMode.SelectedItem.Text;
                            strMsg = "Please select instrument date.";
                            lblErrorMsg.Text = strMsg;
                            DivError.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                            txtInstDate.Focus();
                            return false;
                      
                    }
                    else if (txtCustBankName.Text.Replace(",", "").Trim() == "")
                    {
                      

                        if (Convert.ToInt32(ddlInstType.SelectedValue) != 2)
                        {
                            if ((ddlInstType.SelectedValue != "5") && (ddlVchrType.SelectedValue != "1"))
                            {
                                txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                                txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                                txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
                                //strMsg = "Please fill Bank Name for bank account " + ddlVchrMode.SelectedItem.Text;
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + strMsg + "')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                                txtCustBankName.Focus();
                                return false;
                            }
                           
                        }
                       
                    }
                }
                txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
            }
            return true;
        }

        private DataTable CreateCostcenterDt()
        {
            DataTable Dt = ApplicationFunction.CreateTable("tbl", "Truck_No", "String", "Amount", "String", "Truck_Idno", "String", "Id", "String");
            return Dt;
        }

        private DataTable CreateDt()
        {
            DataTable DtTemp = ApplicationFunction.CreateTable("tbl", "Ledger_Nm", "String", "Amount", "String", "Curr_Bal", "String", "Ledger_Id", "String", "Vchr_IdNo", "String",
                                                                "Inst_Type", "String", "Inst_No", "String", "Inst_Date", "String", "Bank_Date", "String", "Cust_Bank", "String", "InstType_Id", "String",
                                                                "Id", "String", "EntryNarr", "String");
            return DtTemp;
        }

        private bool checkDup()
        {
            try
            {
                string ledger = Convert.ToString(ddlLedgrName.SelectedItem.Text);
                DtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    if (ledger == Convert.ToString(dtrow["Ledger_Nm"]))
                    {
                        string msg = "Duplicate Ledger " + ledger + "";
                        ShowMessageErr(msg);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception Ex)
            {
                return true;
            }
        }

        private void BindGrid()
        {
            if (ViewState["dt"] != null)
            {
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp.Rows.Count > 0)
                {
                    grdMain.DataSource = DtTemp;
                    grdMain.DataBind();
                }
                else
                {

                    DtTemp = null;
                    grdMain.DataSource = DtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                DtTemp = null;
                grdMain.DataSource = DtTemp;
                grdMain.DataBind();
            }
        }

        private void BindCostCenterGrid()
        {
            if (ViewState["DtGridCost"] != null)
            {
                DtCostGrid = (DataTable)ViewState["DtGridCost"];
                if (DtCostGrid.Rows.Count > 0)
                {
                    grdCostdetls.DataSource = DtCostGrid;
                    grdCostdetls.DataBind();
                }
                else
                {

                    DtCostGrid = null;
                    grdCostdetls.DataSource = DtCostGrid;
                    grdCostdetls.DataBind();
                }
            }
            else
            {
                DtCostGrid = null;
                grdCostdetls.DataSource = DtCostGrid;
                grdCostdetls.DataBind();
            }
        }

        private bool ValidateData()
        {
            double TotAmnt = 0, TottxtAmnt = 0; double dTotCostAmnt = 0; Int32 Id = 0;
            TottxtAmnt = (Convert.ToString(txtVchrModeAmnt.Text.Trim()) == "" ? 0 : Math.Round(Convert.ToDouble(txtVchrModeAmnt.Text.Trim()), 2));
            for (int x = 0; x < DtTemp.Rows.Count; x++)
            {
                TotAmnt = Math.Round((TotAmnt + Convert.ToDouble(DtTemp.Rows[x]["Amount"])), 2);
            }
            if (TotAmnt != TottxtAmnt)
            {
                ShowMessageErr("Debit and Credit Amount are not equal, Please Rectify.");
                ddlLedgrName.Focus(); return false;
            }
            if (DtCost != null && DtCost.Rows.Count > 0)
            {
                for (int x = 0; x < DtTemp.Rows.Count; x++)
                {
                    dTotCostAmnt = 0;
                    Id = Convert.ToInt32(DtTemp.Rows[x]["Id"]);
                    totamnt = Convert.ToDouble(DtTemp.Rows[x]["Amount"]);
                    DataRow[] DrRow = DtCost.Select("Id=" + Id + "");
                    if (DrRow != null && DrRow.Length > 0)
                    {
                        for (int i = 0; i < DtCost.Rows.Count; i++)
                        {
                            if (Id == Convert.ToInt32(DtCost.Rows[i]["Id"]))
                            {
                                dTotCostAmnt += Convert.ToDouble(DtCost.Rows[i]["Amount"]);
                            }
                        }
                        if (totamnt != dTotCostAmnt)
                        {
                            ShowMessageErr("Cost Center Amount is  not equal Total Amount for Ledger " + Convert.ToString(DtTemp.Rows[x]["Ledger_Nm"]) + ", Please Rectify.");
                            grdMain.Focus();
                            return false;
                        }
                    }

                }
            }

            return true;
        }

        private void ClearAll()
        {
            DtTemp = CreateDt();
            ViewState["dt"] = DtTemp;
            DtCost = CreateCostcenterDt();
            ViewState["DtCost"] = DtCost;
            DtCostGrid = CreateCostcenterDt();
            ViewState["DtCost"] = DtCostGrid;
            grdMain.DataSource = null;
            grdMain.DataBind();
            grdCostdetls.DataSource = null;
            grdCostdetls.DataBind();
            ddlVchrType.SelectedValue = "1";
            ddlVchrType_SelectedIndexChanged(null, null);
            //  ddlcompany.SelectedIndex = 0;
            ddlcompany_SelectedIndexChanged(null, null);
            //btnVtCommonFun(1);
            lblVchrModeCurBal.Text = "";
            lblLedgrNmCurBal.Text = ""; lblMsgGrid.Text = "";

        }

        private void loaddsVchrEntry(int vIdNo)
        {
            try
            {
                if (dsVchrEntry != null) dsVchrEntry.Tables.Clear();
                if (dsVchrEntryDet != null) dsVchrEntryDet.Tables.Clear();
                dsVchrEntry = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "Exec [spVoucherEntry] @Action='SelectHeadById', @VCHRIDNO=" + Convert.ToString(vIdNo));
            }
            catch (Exception Ex)
            {
            }
        }

        private void loaddsVchrEntryDet(int cnt)
        {
            if (dsVchrEntry.Tables[0].Rows.Count > 0)
                dsVchrEntryDet = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "Exec [spVoucherEntry] @Action='SelectDetById', @VCHRIDNO='" + cnt + "'");
        }
        private void loaddsVchrCostDet(int cnt)
        {
            if (dsVchrEntry.Tables[0].Rows.Count > 0)
                DSCostPopulate = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "Exec [spVoucherEntry] @Action='SelectVchrCostDetById', @VCHRIDNO='" + cnt + "'");
        }
        private bool AddRecord(SqlTransaction objTran)
        {
            try
            {
                Int32 UserId = Convert.ToInt32(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                if ((string.IsNullOrEmpty(Convert.ToString(HidVchrTyp.Value)) ? "0" : Convert.ToString(HidVchrTyp.Value)) == "0")
                {
                    return false;
                }
                Double dtotAmnt = 0;
                //  Int64 CompanyIdno = Convert.ToInt64(ddlcompany.SelectedValue);
                for (int i = 0; i < DtTemp.Rows.Count; i++)
                {
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DtTemp.Rows[i]["Amount"]);
                }
                DataTable dt = DtTemp;
                string strVchrNo = Convert.ToString(SqlHelper.ExecuteScalar(objTran, CommandType.Text, "SELECT ISNULL(MAX(VCHR_NO),0) + 1 AS MAXID FROM VchrHead WHERE VCHR_TYPE='" + HidVchrTyp.Value + "' AND VCHR_HIDN = 0"));
                string strSQL = String.Empty, SBILLDATE = String.Empty;
                strSQL = "Exec [spVoucherEntry] @Action='InsertVhead', @VCHRNO='" + strVchrNo + "', @VCHRDATE='" + ApplicationFunction.mmddyyyy(Convert.ToString(txtDate.Text)) + "', @VCHRTYPE='" + HidVchrTyp.Value + "', @VCHRMODE='" + Convert.ToString(ddlVchrMode.SelectedValue) + "', @VCHRNARR='" + txtVchrNarr.Text.Trim().Replace("'", "") + "', @VCHRHIDN='0',@YEARIDNO='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "',@VCHRSUSP='0',@VCHRFRM='',@ACNTIDNO='0',@PRINTED=0,@SBILLNO='0',@SBILLDATE=null, @DCNNO='',@UserIdno='" + UserId + "',@UserType='0',@VchrForIdno='0'";
                int value = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                if (value == -1)
                {
                    int nVIdNo; string strInstTypeId = "", strInstNo = "", strInstDate = "", strCustBank = "";
                    int iAmntType = 0;
                    nVIdNo = Convert.ToInt32(SqlHelper.ExecuteScalar(objTran, CommandType.Text, "SELECT MAX(VCHR_IDNO) AS VCHR_IDNO FROM VchrHead"));
                    double dTotAmnt = 0.00;
                    dTotAmnt = dtotAmnt;
                    iAmntType = Convert.ToInt32(ddlLdgrTyp.SelectedItem.Value);
                    if (iAmntType != 0)
                    {
                        strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(nVIdNo) + ",  @ACNTIDNO='" + Convert.ToString(ddlVchrMode.SelectedValue) + "', @NARRTEXT='" + "" + "', @ACNTAMNT='" + Convert.ToDouble(txtVchrModeAmnt.Text.Replace(",", "")) + "', @AMNTTYPE='" + ddlVchrModeTyp.SelectedItem.Value + "', @INSTTYPE='', @INSTNO='',@DETLHIDN=1,@BANKDATE=null,@CUSTBANK = ''";
                        int value1 = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                        if (value1 == -1)
                        {
                            for (int i = 0; i < DtTemp.Rows.Count; i++)
                            {
                                int nVIddETLNo = 0; Int64 RowId = 0;
                                RowId = Convert.ToInt64(DtTemp.Rows[i]["Id"]);
                                if (Convert.ToString(DtTemp.Rows[i]["InstType_Id"]) != "0")
                                {
                                    strInstTypeId = Convert.ToString(DtTemp.Rows[i]["InstType_Id"]);
                                    strInstNo = Convert.ToString(DtTemp.Rows[i]["Inst_No"]);
                                    strInstDate = Convert.ToString(DtTemp.Rows[i]["Inst_Date"]);
                                    strCustBank = Convert.ToString(DtTemp.Rows[i]["Cust_Bank"]);
                                }
                                //strLedgerCstCnter = Convert.ToString(DtTemp.Rows[i]["CostCenterIdno"]);
                                //  CREDIT = 1   DEBIT = 2  (AMNT_TYPE)
                                if (HidVchrTyp.Value == "1" || (HidVchrTyp.Value == "7"))
                                {
                                    strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(nVIdNo) + ", @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "', @NARRTEXT='" + Convert.ToString(DtTemp.Rows[i]["EntryNarr"]) + "', @ACNTAMNT='" + Convert.ToString(DtTemp.Rows[i]["Amount"]).Replace(",", "") + "', @AMNTTYPE=2, @INSTTYPE='" + Convert.ToString(strInstTypeId) + "', @INSTNO='" + Convert.ToString(strInstNo) + "',@DETLHIDN='',@BANKDATE='" + (Convert.ToString(strInstDate) == "" ? null : ApplicationFunction.mmddyyyy(strInstDate)) + "',@CUSTBANK = '" + Convert.ToString(strCustBank) + "'";
                                }
                                else if ((HidVchrTyp.Value == "2") || (HidVchrTyp.Value == "8"))
                                {
                                    strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(nVIdNo) + ", @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "', @NARRTEXT='" + Convert.ToString(DtTemp.Rows[i]["EntryNarr"]) + "', @ACNTAMNT='" + Convert.ToString(DtTemp.Rows[i]["Amount"]).Replace(",", "") + "', @AMNTTYPE=1, @INSTTYPE='" + Convert.ToString(strInstTypeId) + "', @INSTNO='" + Convert.ToString(strInstNo) + "',@DETLHIDN='',@BANKDATE='" + (Convert.ToString(strInstDate) == "" ? null : ApplicationFunction.mmddyyyy(strInstDate)) + "',@CUSTBANK = '" + Convert.ToString(strCustBank) + "'";
                                }
                                else if ((HidVchrTyp.Value == "3") || (HidVchrTyp.Value == "4") || (HidVchrTyp.Value == "5") || (HidVchrTyp.Value == "6"))
                                {
                                    if (Convert.ToString(DtTemp.Rows[i]["Amount"]) != "0.00")
                                    {
                                        strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(nVIdNo) + ", @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "', @NARRTEXT='" + Convert.ToString(DtTemp.Rows[i]["EntryNarr"]) + "', @ACNTAMNT='" + Convert.ToString(DtTemp.Rows[i]["Amount"]).Replace(",", "") + "', @AMNTTYPE='" + iAmntType + "', @INSTTYPE='" + Convert.ToString(strInstTypeId) + "', @INSTNO='" + Convert.ToString(strInstNo) + "',@DETLHIDN='',@BANKDATE='" + (Convert.ToString(strInstDate) == "" ? null : ApplicationFunction.mmddyyyy(strInstDate)) + "',@CUSTBANK = '" + Convert.ToString(strCustBank) + "'";
                                    }
                                }
                                int value2 = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                                if (value2 != -1)
                                {
                                    return false;
                                }
                                nVIddETLNo = Convert.ToInt32(SqlHelper.ExecuteScalar(objTran, CommandType.Text, "SELECT MAX(VCHRdETL_IDNO) AS VCHRdETL_IDNO FROM VchrDetl"));
                                VchrEntryDAL obj = new VchrEntryDAL();
                                Int64 ivalue = 0;
                                if (DtCost != null && DtCost.Rows.Count > 0)
                                {
                                    DataRow[] DrRow = DtCost.Select("Id=" + Convert.ToInt64(RowId) + "");
                                    if (DrRow != null && DrRow.Length > 0)
                                    {
                                        ivalue = obj.InserCostCenterdetail(DrRow, Convert.ToInt64(nVIdNo), Convert.ToInt32(nVIddETLNo));
                                        if (ivalue <= 0)
                                        {
                                            return false;
                                        }
                                    }

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

                    }
                    /********* End Chnage Here ***************/
                }
                else
                    return false;

                return true;
            }
            catch (Exception Ex)
            {
                string str = Ex.Message;
                return false;
            }
        }

        private bool UpdateRecord(SqlTransaction objTran)
        {
            try
            {
                Int32 UserId = Convert.ToInt32(Session["UserIdno"] == null ? "0" : Session["UserIdno"]);
                Double dtotAmnt = 0;
                //  Int64 CompanyIdno = Convert.ToInt64(ddlcompany.SelectedValue);
                for (int i = 0; i < DtTemp.Rows.Count; i++)
                {
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DtTemp.Rows[i]["Amount"]);
                }
                DataTable dt = DtTemp;
                string strSQL = String.Empty, SBILLDATE = String.Empty;
                strSQL = "Exec [spVoucherEntry] @Action='UpdateVHead', @VCHRNO='" + txtVchrNo.Text + "', @VCHRDATE='" + ApplicationFunction.mmddyyyy(Convert.ToString(txtDate.Text)) + "', @VCHRTYPE='" + HidVchrTyp.Value + "', @VCHRMODE='" + Convert.ToString(ddlVchrMode.SelectedValue) + "', @VCHRNARR='" + txtVchrNarr.Text.Trim().Replace("'", "") + "', @VCHRHIDN='0',@YEARIDNO='" + Convert.ToInt32(ddlDateRange.SelectedValue) + "',@VCHRSUSP='0',@VCHRFRM='',@ACNTIDNO='0',@PRINTED=0,@SBILLNO='0',@SBILLDATE='', @DCNNO='',@VchrIdno='" + HidheadID.Value + "',@UserIdno='" + UserId + "',@UserType='0',@VchrForIdno='0'";
                int value = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                if (value == -1)
                {
                    strSQL = "Exec [spVoucherEntry] @Action='DeleteDet', @VchrIdno='" + Convert.ToString(HidheadID.Value) + "'";
                    int value1 = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                    if (value1 == -1)
                    {
                        string strInstTypeId = "", strInstNo = "", strInstDate = "", strCustBank = "";
                        int iAmntType = 0; double dTotAmnt = 0.00;
                        dTotAmnt = dtotAmnt;
                        iAmntType = Convert.ToInt32(ddlLdgrTyp.SelectedItem.Value);

                        if (iAmntType != 0)
                        {
                            strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(HidheadID.Value) + ",  @ACNTIDNO='" + Convert.ToString(ddlVchrMode.SelectedValue) + "', @NARRTEXT='" + "" + "', @ACNTAMNT='" + Convert.ToDouble(txtVchrModeAmnt.Text.Replace(",", "")) + "', @AMNTTYPE='" + ddlVchrModeTyp.SelectedItem.Value + "', @INSTTYPE='', @INSTNO='',@DETLHIDN=1,@BANKDATE=null,@CUSTBANK = ''";
                            int value2 = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                            if (value2 == -1)
                            {
                                for (int i = 0; i < DtTemp.Rows.Count; i++)
                                {
                                    int nVIddETLNo = 0; Int64 RowId = 0;
                                    RowId = Convert.ToInt64(DtTemp.Rows[i]["Id"]);
                                    if (Convert.ToString(DtTemp.Rows[i]["InstType_Id"]) != "0")
                                    {
                                        strInstTypeId = Convert.ToString(DtTemp.Rows[i]["InstType_Id"]);
                                        strInstNo = Convert.ToString(DtTemp.Rows[i]["Inst_No"]);
                                        strInstDate = Convert.ToString(DtTemp.Rows[i]["Inst_Date"]);
                                        strCustBank = Convert.ToString(DtTemp.Rows[i]["Cust_Bank"]);
                                        //strLedgerCstCnter = Convert.ToString(DtTemp.Rows[i]["CostCenterIdno"]);
                                    }
                                    //  CREDIT = 1   DEBIT = 2  (AMNT_TYPE)
                                    strSQL = "Exec [spVoucherEntry] @Action='INSERTVDETL',@VCHRIDNO=" + Convert.ToString(HidheadID.Value) + ",  @ACNTIDNO='" + Convert.ToString(DtTemp.Rows[i]["Ledger_Id"]) + "', @NARRTEXT='" + "" + "', @ACNTAMNT='" + Convert.ToString(DtTemp.Rows[i]["Amount"]).Replace(",", "") + "', @AMNTTYPE='" + ddlLdgrTyp.SelectedItem.Value + "', @INSTTYPE='" + Convert.ToString(strInstTypeId) + "', @INSTNO='" + Convert.ToString(strInstNo) + "',@DETLHIDN='',@BANKDATE='" + (Convert.ToString(strInstDate) == "" ? null : ApplicationFunction.mmddyyyy(strInstDate)) + "',@CUSTBANK = '" + Convert.ToString(strCustBank) + "'";
                                    int value3 = SqlHelper.ExecuteNonQuery(objTran, CommandType.Text, strSQL);
                                    if (value3 != -1)
                                    {
                                        return false;
                                    }
                                    nVIddETLNo = Convert.ToInt32(SqlHelper.ExecuteScalar(objTran, CommandType.Text, "SELECT MAX(VCHRdETL_IDNO) AS VCHRdETL_IDNO FROM VchrDetl"));
                                    VchrEntryDAL obj = new VchrEntryDAL();
                                    Int64 ivalue = 0;
                                    if (DtCost != null && DtCost.Rows.Count > 0)
                                    {
                                        DataRow[] DrRow = DtCost.Select("Id=" + Convert.ToInt64(RowId) + "");
                                        if (DrRow != null && DrRow.Length > 0)
                                        {
                                            ivalue = obj.InserCostCenterdetail(DrRow, Convert.ToInt64((HidheadID.Value)), Convert.ToInt32(nVIddETLNo));
                                            if (ivalue <= 0)
                                            {
                                                return false;
                                            }
                                        }

                                    }
                                }


                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                    return false;

                return true;
            }
            catch (Exception Ex)
            {
                string str = Ex.Message;
                return false;
            }
        }

        private void AddROwcostCenter(string ddlvalue, string Amount, Int64 TruckIdnoId, Int32 Id)
        {
            DtCostGrid = (DataTable)ViewState["DtGridCost"];
            ApplicationFunction.DatatableAddRow(DtCostGrid, ddlvalue, Amount, TruckIdnoId, Id);
            ViewState["DtGridCost"] = DtCostGrid;
            BindCostCenterGrid();
        }

        //private void BindCostCenterVchrMode()
        //{
        //    clsVchrEntry objVchrEntryDAL = new clsVchrEntry();
        //    Int64 CostCenter = 0;

        //    CostCenter = objVchrEntryDAL.CheckCostCenter(Convert.ToInt64(ddlVchrMode.SelectedValue == "" ? 0 : Convert.ToInt64(ddlVchrMode.SelectedValue)));
        //    ddlVchrModeCostCnter.Items.Clear();
        //    VchrCost.Visible = false;
        //    if (CostCenter == 1)
        //    {
        //        VchrCost.Visible = true;
        //        var lst = objVchrEntryDAL.SelectCostCenter(Convert.ToInt64(ddlcompany.SelectedValue));
        //        ddlVchrModeCostCnter.DataSource = lst;
        //        ddlVchrModeCostCnter.DataTextField = "Name";
        //        ddlVchrModeCostCnter.DataValueField = "CostCenter_Idno";
        //        ddlVchrModeCostCnter.DataBind();

        //    }
        //    ddlVchrModeCostCnter.Items.Insert(0, new ListItem("< Choose CC >", "0"));
        //    ddlVchrModeCostCnter.SelectedIndex = 0;
        //}

        //private void BindCostCenterLedgrName()
        //{
        //    clsVchrEntry objVchrEntryDAL = new clsVchrEntry();
        //    Int64 CostCenter = 0;
        //    CostCenter = objVchrEntryDAL.CheckCostCenter(Convert.ToInt64(ddlLedgrName.SelectedValue == "" ? 0 : Convert.ToInt64(ddlLedgrName.SelectedValue)));
        //    ddlLedgrNameCstCnter.Items.Clear();
        //    lblLedgerCstCenter.Visible = false;
        //    ddlLedgrNameCstCnter.Visible = false;
        //    if (CostCenter == 1)
        //    {
        //        lblLedgerCstCenter.Visible = true;
        //        ddlLedgrNameCstCnter.Visible = true;
        //        var lst = objVchrEntryDAL.SelectCostCenter(Convert.ToInt64(ddlcompany.SelectedValue));
        //        ddlLedgrNameCstCnter.DataSource = lst;
        //        ddlLedgrNameCstCnter.DataTextField = "Name";
        //        ddlLedgrNameCstCnter.DataValueField = "CostCenter_Idno";
        //        ddlLedgrNameCstCnter.DataBind();

        //    }
        //    ddlLedgrNameCstCnter.Items.Insert(0, new ListItem("< Choose CC >", "0"));
        //    ddlLedgrNameCstCnter.SelectedIndex = 0;
        //}
        #endregion

        #region Control Events...

        protected void ddlVchrModeTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToInt64(ddlcompany.SelectedValue) == 0)
                //{
                //    msg = "Please Select Company First";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                //    return;
                //}
                if (ddlVchrModeTyp.SelectedIndex == 0)
                {
                    ddlLdgrTyp.SelectedIndex = 1;
                }
                else if (ddlVchrModeTyp.SelectedIndex > 0)
                {
                    ddlLdgrTyp.SelectedIndex = 0;
                }
                ddlVchrMode.Focus();
            }
            catch (Exception Ex)
            {
            }
        }

        protected void ddlVchrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlVchrType.SelectedIndex >= 0)
                {
                    HidVchrTyp.Value = ddlVchrType.SelectedValue;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    DtTemp = CreateDt();
                    ViewState["dt"] = DtTemp;
                    DtCost = CreateCostcenterDt();
                    ViewState["DtCost"] = DtCost;
                }
                else
                {
                    HidVchrTyp.Value = "0";
                }
                btnVtCommonFun(Convert.ToInt32(HidVchrTyp.Value));
                txtDate.Focus();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void ddlVchrMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LorryMasterDAL objDal = new LorryMasterDAL();
                CurBalLoad(2);
                var lst = objDal.selectPrtyLorryDetails(Convert.ToInt32(ddlVchrMode.SelectedValue));
                if (lst != null && lst.Count > 0)
                {
                    lnkPrtyLorry1.Visible = true;
                    grdGrdetals.DataSource = lst;
                    grdGrdetals.DataBind();
                }
                else
                {
                    lnkPrtyLorry1.Visible = false;
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                }
                txtVchrModeAmnt.SelectText(); txtVchrModeAmnt.Focus();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void ddlLedgrName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iLdrtyp = 0;
                //BindCostCenterLedgrName();
                DtTemp = (DataTable)ViewState["dt"];
                if (ddlVchrMode.SelectedIndex <= 0)
                {
                    string msg = "Please select " + ddlVchrModeTyp.SelectedItem.Text + " ledger.";
                    ShowMessageErr(msg);
                    ddlVchrModeTyp.Focus(); return;
                }
                else
                {
                    if (ddlVchrMode.SelectedValue == ddlLedgrName.SelectedValue)
                    {
                        string msg = "Duplicate Ledger " + ddlVchrMode.SelectedItem.Text + " Found Please Change Ledger.";
                        ShowMessageErr(msg);
                        ddlLedgrName.SelectedIndex = 0;
                        ddlLedgrName.Focus(); return;
                    }
                    if ((ddlLedgrName.SelectedIndex > 0) && (ddlVchrType.SelectedIndex < 2))
                    {
                        iLdrtyp = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectLdrType',@AcntIdno='" + Convert.ToString(ddlVchrMode.SelectedValue) + "'");
                        if (iLdrtyp == 4)
                        {
                            if (Convert.ToInt32(ddlVchrType.SelectedValue) == 1)
                            {
                                lblBankName.Visible = false;
                                txtCustBankName.Visible = false;
                            }
                            ddlInstType.SelectedValue = "2";
                            txtInstDate.Text = ApplicationFunction.GetIndianDateTime().Date.ToString("dd-MM-yyyy");
                            txtInstNo.Text = txtCustBankName.Text = String.Empty;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
                            ddlInstType.Focus();
                        }
                    }
                }

                //if (checkDup() == true)
                //    return;
                CurBalLoad(1);
                #region Show approx amount...

                if (Convert.ToString(txtVchrModeAmnt.Text.Trim()) == "")
                    txtVchrModeAmnt.Text = "0.00";

                if (DtTemp != null && DtTemp.Rows.Count > 0)
                {
                    DataTable dtCloned = DtTemp.Clone();
                    dtCloned.Columns["Amount"].DataType = typeof(double);
                    foreach (DataRow row in DtTemp.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }
                    double dblAprxAmnt = Convert.ToDouble(txtVchrModeAmnt.Text.Trim()) - Convert.ToDouble(dtCloned.Compute("SUM(Amount)", ""));
                    if (dblAprxAmnt >= 0)
                    {
                        txtLdgrAmnt.Text = dblAprxAmnt.ToString("N2");
                    }
                    else
                    {
                        txtLdgrAmnt.Text = Convert.ToDouble(txtVchrModeAmnt.Text.Trim()).ToString("N2");
                    }
                    dtCloned = null;
                }
                else
                {
                    txtLdgrAmnt.Text = Convert.ToDouble(txtVchrModeAmnt.Text.Trim()).ToString("N2");
                }
                #endregion

                if (lblLedgrNmCurBal.Text != "")
                {
                    dblTemp = Convert.ToDouble(lblLedgrNmCurBal.Text.Substring(0, lblLedgrNmCurBal.Text.LastIndexOf("  ")));
                    hidtemp.Value = Convert.ToDouble(lblLedgrNmCurBal.Text.Substring(0, lblLedgrNmCurBal.Text.LastIndexOf("  "))).ToString("N2");
                }
                LorryMasterDAL objDal = new LorryMasterDAL();
                var lst = objDal.selectPrtyLorryDetails(Convert.ToInt32(ddlLedgrName.SelectedValue));
                if (lst != null && lst.Count > 0)
                {
                    lnkPrtyLorry2.Visible = true;
                    grdGrdetals1.DataSource = lst;
                    grdGrdetals1.DataBind();
                }
                else
                {
                    lnkPrtyLorry2.Visible = false;
                    grdGrdetals1.DataSource = null;
                    grdGrdetals1.DataBind();
                }
                txtVchrModeAmnt.SelectText(); txtVchrModeAmnt.Focus();
                txtLdgrAmnt.Focus();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void txtVchrModeAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtVchrModeAmnt.Text == "")
            {
                txtVchrModeAmnt.Text = "0.00";
            }
            else
            {
                txtVchrModeAmnt.Text = Convert.ToDouble(txtVchrModeAmnt.Text).ToString("N2");
            }
            txtVchrModeAmnt.Focus();
        }

        protected void txtLdgrAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtLdgrAmnt.Text == "")
            {
                txtLdgrAmnt.Text = "0.00";
            }
            else
            {
                txtLdgrAmnt.Text = Convert.ToDouble(txtLdgrAmnt.Text).ToString("N2");
            }
            txtLdgrAmnt.Focus();
        }

        protected void drpCFA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HidheadID.Value) == true)
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                itm_id = 0;
                ViewState["dt"] = null;
            }
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotShipQty = (Label)e.Item.FindControl("lblTotShipQty");
                Label lblTotBillQty = (Label)e.Item.FindControl("lblTotBillQty");
                Label lblTotAmnt = (Label)e.Item.FindControl("lblTotAmnt");
                lblTotBillQty.Text = lblTotShipQty.Text = totQty.ToString();
                lblTotAmnt.Text = String.Format("{0:0.00}", totamnt);
            }
        }

        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnVtCommonFun(Convert.ToInt32(HidVchrTyp.Value));
            ddlVchrType.Focus();
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            DtCostGrid = (DataTable)ViewState["DtGridCost"];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            DropDownList ddlTruckno = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)ddlTruckno.Parent.Parent;
            DropDownList ddltruckno = (DropDownList)currentRow.FindControl("ddlTruckno");
            HiddenField hidTruckIdno = (HiddenField)currentRow.FindControl("hidTruckIdno");
            hidTruckIdno.Value = Convert.ToString(ddltruckno.SelectedValue);
            DropDownList ddl1 = (DropDownList)grdCostdetls.Rows[0].FindControl("ddlTruckNo");
            ddl1.Focus();

        }
        #endregion

        #region Button Events...

        protected void lnkPrtyLorry1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyModal();", true);
        }
        protected void lnkPrtyLorry2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openPartyModal1();", true);
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ValidateGrid() == false)
            {
                txtDate.Text = txtDate.Text.Replace(",", "").Trim();
                txtCustBankName.Text = txtCustBankName.Text.Replace(",", "").Trim();
                txtInstNo.Text = txtInstNo.Text.Replace(",", "").Trim();
                return;
            }
            if (hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Ledger_Nm"] = ddlLedgrName.SelectedItem.Text;
                        dtrow["Amount"] = Convert.ToDouble(txtLdgrAmnt.Text).ToString("N2");
                        dtrow["Curr_Bal"] = lblLedgrNmCurBal.Text;
                        dtrow["Ledger_Id"] = Convert.ToString(ddlLedgrName.SelectedValue);
                        dtrow["Vchr_IdNo"] = "0";
                        dtrow["Inst_Type"] = ddlInstType.SelectedIndex == 0 ? "" : ddlInstType.SelectedItem.Text.Replace(",", "").Trim();
                        dtrow["Inst_No"] = txtInstNo.Text.Replace(",", "").Trim();
                        dtrow["Inst_Date"] = (Convert.ToString(txtInstNo.Text.Replace(",", "").Trim()) != "" ? Convert.ToString(txtInstDate.Text.Replace(",", "").Trim()) : null);
                        dtrow["Cust_Bank"] = txtCustBankName.Text.Replace(",", "").Trim();
                        dtrow["InstType_Id"] = ddlInstType.SelectedItem.Value;
                        dtrow["EntryNarr"] = Convert.ToString(txtEntryNarr.Text);
                    }
                }
            }
            else
            {
                //For Insert
                DtTemp = (DataTable)ViewState["dt"];
                int id = 1; double dAmnt = (ddlLdgrTyp.SelectedIndex == 1 ? Convert.ToDouble(txtLdgrAmnt.Text) : 0);
                if ((DtTemp != null) && (DtTemp.Rows.Count > 0))
                {
                    int rowc = DtTemp.Rows.Count;
                    if (rowc >= 1 && Convert.ToInt32(ddlVchrType.SelectedValue) == 2)
                    {
                        msg = "Sorry You Can'nt Select more than 1 Ledger on selection of Receipt No!";
                        ShowMessageErr(msg);
                        return;
                    }
                    id = DtTemp.Rows.Count + 1;
                }
                else
                {
                    DtTemp = CreateDt();
                }
                ApplicationFunction.DatatableAddRow(DtTemp, ddlLedgrName.SelectedItem.Text, Convert.ToDouble(txtLdgrAmnt.Text).ToString("N2"), lblLedgrNmCurBal.Text, Convert.ToString(ddlLedgrName.SelectedValue),
                                                    "0", ddlInstType.SelectedIndex == 0 ? "" : (ddlInstType.SelectedItem.Text), txtInstNo.Text.Replace(",", "").Trim(), null,
                                                    (Convert.ToString(txtInstDate.Text.Replace(",", "").Trim()) != "" ? Convert.ToString(txtInstDate.Text.Replace(",", "").Trim()) : null),
                                                    txtCustBankName.Text.Replace(",", "").Trim(), ddlInstType.SelectedItem.Value, id, Convert.ToString(txtEntryNarr.Text));
                ViewState["dt"] = DtTemp;
                dt = DtTemp;
                ddlLedgrName.SelectedValue = "0";
            }
            this.BindGrid();
            this.ClearControl();
            ddlLedgrName.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HidheadID.Value) == true)
            {
                this.ClearAll();
                HidheadID.Value = string.Empty;
            }
            else
            {
                DtTemp = null;
                this.ClearAll();
                loaddsVchrEntry(Convert.ToInt32(Convert.ToInt32(HidheadID.Value)));
                if ((dsVchrEntry != null) && (dsVchrEntry.Tables.Count > 0) && (dsVchrEntry.Tables[0].Rows.Count > 0))
                {
                    loaddsVchrEntryDet(Convert.ToInt32(HidheadID.Value));
                    loaddsVchrCostDet(Convert.ToInt32(HidheadID.Value));
                    if ((dsVchrEntryDet != null) && (dsVchrEntryDet.Tables.Count > 0) && (dsVchrEntryDet.Tables[0].Rows.Count > 0))
                    {
                        this.Populate(Convert.ToInt32(HidheadID.Value));
                    }
                }
                //loaddsVchrEntry(Convert.ToInt32(HidheadID.Value));
                //loaddsVchrCostDet(Convert.ToInt32(HidheadID.Value));
                //if ((dsVchrEntry != null) && (dsVchrEntry.Tables.Count > 0) && (dsVchrEntry.Tables[0].Rows.Count > 0))
                //{
                //    if ((dsVchrEntryDet != null) && (dsVchrEntryDet.Tables.Count > 0) && (dsVchrEntryDet.Tables[0].Rows.Count > 0))
                //    {
                //        this.Populate(Convert.ToInt32(Request.QueryString["VchrIdno"]));
                //    }
                //}
                //  ddlcompany.Enabled = ddlVchrType.Enabled = ddlVchrModeTyp.Enabled = false;
                this.Populate(Convert.ToInt32(HidheadID.Value));
                this.ClearControl();
            }
            //   ddlcompany.Focus();
        }

        protected void btnInstDetOK_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "HideClient()", true);
            txtLdgrAmnt.Focus();
        }

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            DtTemp = (DataTable)ViewState["dt"];
            DtCost = (DataTable)ViewState["DtCost"];
            if ((DtTemp == null) || (DtTemp.Rows.Count <= 0))
            {
                msg = "Please Enter Detail!";
                ErrorMessage(msg);
                return;
            }
            if (ddlVchrType.SelectedValue == "0")
            {
                msg = "Please Select Voucher Type!";
                ErrorMessage(msg);
                return;
            }
            try
            {
                /*                      Validate Data before save                                                           */
                if (ValidateData() == false) return;

                if (string.IsNullOrEmpty(HidheadID.Value) == true)
                {
                    using (SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString()))
                    {
                        con.Open();
                        SqlTransaction tran = con.BeginTransaction();
                        if (AddRecord(tran) == true)
                        {
                            tran.Commit();
                            msg = "Record Saved. Sucessfully";
                            HidheadID.Value = string.Empty;
                            this.ClearAll();
                            ddlVchrType.Focus();
                        }
                        else
                        {
                            tran.Rollback();
                            msg = "Record not Saved.";
                        }
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString()))
                    {
                        con.Open();
                        SqlTransaction tran = con.BeginTransaction();
                        if (UpdateRecord(tran) == true)
                        {
                            tran.Commit();
                            msg = "Record Updated Sucessfully";
                            HidheadID.Value = string.Empty;
                            this.ClearAll();
                            ddlVchrType.Focus();
                        }
                        else
                        {
                            tran.Rollback();
                            msg = "Record not Updated!";
                        }
                    }
                }
                // ddlcompany.Focus();
            }
            catch (Exception Ex)
            {
                msg = "Oops something went wrong!";
            }

            SuccessMessage(msg);
        }

        protected void lnklblInstDiv_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToInt32(ddlVchrType.SelectedValue) == 1)
            {
                lblBankName.Visible = false;
                txtCustBankName.Visible = false;
            }
            ddlInstType.SelectedValue = "2";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            ddlInstType.Focus();
        }
        protected void lnkbtnCostSubmit_OnClick(object sender, EventArgs e)
        {
            DtCost = (DataTable)ViewState["DtCost"];
            DtCostGrid = CreateCostcenterDt();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "CloseModal();", true);
            foreach (GridViewRow row in grdCostdetls.Rows)
            {
                DropDownList ddltruckno = (DropDownList)row.FindControl("ddlTruckno");
                HiddenField hidTruckIdno = (HiddenField)row.FindControl("hidTruckIdno");
                HiddenField hidCostGriRowId = (HiddenField)row.FindControl("hidCostRowId");
                TextBox txtGridAmnt = (TextBox)row.FindControl("txtAmnt");
                ApplicationFunction.DatatableAddRow(DtCostGrid, Convert.ToString(ddltruckno.Text), Convert.ToDouble(txtGridAmnt.Text).ToString("N2"), hidTruckIdno.Value, hidCostGriRowId.Value);
            }
            ViewState["DtGridCost"] = DtCostGrid;
            if (DtCostGrid != null && DtCostGrid.Rows.Count > 0)
            {
                if (DtCostGrid != null && DtCostGrid.Rows.Count > 0)
                {
                    double SumAmnt = 0;
                    SumAmnt = GetSum(DtCostGrid);
                    if (SumAmnt > Convert.ToDouble(hidAmount.Value) || SumAmnt < Convert.ToDouble(hidAmount.Value))
                    {
                        string msg = "";
                        msg = "Amount Must be Equal to Total Amount";
                        lblMsgGrid.Text = msg; BindCostCenterGrid();
                        grdCostdetls.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        return;
                    }
                    else
                    {
                        lblMsgGrid.Text = "";
                    }
                }
                if (DtCost != null && DtCost.Rows.Count > 0)
                {

                    DataRow[] DrRow = DtCost.Select("Id=" + Convert.ToInt32(hidCostRowId.Value) + "");
                    if (DrRow != null && DrRow.Length > 0)
                    {
                        foreach (DataRow dr in DrRow)
                        {
                            DtCost.Rows.Remove(dr);
                        }
                    }
                }
                foreach (DataRow item in DtCostGrid.Rows)
                {
                    if ((Convert.ToInt32(item["Truck_Idno"]) > 0) && (Convert.ToDouble(item["Amount"]) > 0))
                    {
                        ApplicationFunction.DatatableAddRow(DtCost, "", item["Amount"], item["Truck_Idno"], item["Id"]);
                    }
                }
                ViewState["DtCost"] = DtCost;
            }
        }
        #endregion

        #region GridView Event...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dTotAmount = dTotAmount + (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Amount")).Trim() == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotAmnt = (Label)e.Row.FindControl("lblTotAmnt");
                HidTotAmnt.Value = lblTotAmnt.Text = dTotAmount.ToString("N2");
                grdMain.HeaderRow.Cells[1].Text = (ddlLdgrTyp.SelectedIndex == 0 ? "Cr" : "Dr") + " Amnt";
            }
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            DtCost = (DataTable)ViewState["DtCost"];
            if (e.CommandName == "cmddelete")
            {
                DataTable dt = CreateDt();
                foreach (DataRow rw in DtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(dt, rw["Ledger_Nm"], rw["Amount"], rw["Curr_Bal"], rw["Ledger_Id"],
                                                                rw["Vchr_IdNo"], rw["Inst_Type"], rw["Inst_No"], rw["Inst_Date"], rw["Bank_Date"], rw["Cust_Bank"],
                                                                rw["InstType_Id"], rw["Id"], rw["EntryNarr"]);
                    }
                }
                if (DtCost != null && DtCost.Rows.Count > 0)
                {

                    DataRow[] DrRow = DtCost.Select("Id=" + id + "");
                    if (DrRow != null && DrRow.Length > 0)
                    {
                        foreach (DataRow dr in DrRow)
                        {
                            DtCost.Rows.Remove(dr);
                        }
                    }
                }
                ViewState["dt"] = dt;
                ViewState["DtCost"] = DtCost;
                dt.Dispose();
                this.BindGrid();
            }
            else if (e.CommandName == "cmdedit")
            {
                DataRow[] rw = DtTemp.Select("Id='" + id + "'");
                if (rw.Length > 0)
                {
                    hidrowid.Value = Convert.ToString(rw[0]["Id"]);
                    ddlLedgrName.SelectedValue = Convert.ToString(rw[0]["Ledger_Id"]);
                    txtInstNo.Text = txtInstDate.Text = txtCustBankName.Text = string.Empty;
                    txtInstNo.Text = Convert.ToString(rw[0]["Inst_No"]);
                    txtInstDate.Text = Convert.ToString(rw[0]["Inst_Date"]).Trim();
                    txtCustBankName.Text = Convert.ToString(rw[0]["Cust_Bank"]);
                    ddlInstType.SelectedValue = Convert.ToString(rw[0]["InstType_Id"]);
                    ddlLedgrName_SelectedIndexChanged(null, null);
                    txtLdgrAmnt.Text = Convert.ToDouble(rw[0]["Amount"]).ToString("N2");
                    txtEntryNarr.Text = Convert.ToString(rw[0]["EntryNarr"]);
                    //ddlLedgrNameCstCnter.SelectedValue = Convert.ToString(rw[0]["CostCenterIdno"]);
                    //ddlLedgrNameCstCnter.SelectedItem.Text = Convert.ToString(rw[0]["CostCenterName"]);
                    int iLdrtyp = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectLdrType',@AcntIdno='" + Convert.ToString(ddlVchrMode.SelectedValue) + "'");
                    if (iLdrtyp == 4)
                    {
                        imgbtnInstDetEdit.Visible = true;
                    }
                    else
                    {
                        imgbtnInstDetEdit.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "cmdcostcenter")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                double dAmnt = 0;
                hidCostRowId.Value = Convert.ToString(e.CommandArgument);
                DtCost = (DataTable)ViewState["DtCost"];
                DtCostGrid = CreateCostcenterDt();
                ViewState["DtGridCost"] = DtCostGrid; lblMsgGrid.Text = "";
                if (DtCost != null && DtCost.Rows.Count > 0)
                {
                    DataRow[] rw = DtTemp.Select("Id='" + Convert.ToInt32(hidCostRowId.Value) + "'");
                    if (rw.Length > 0)
                    {
                        dAmnt = Convert.ToDouble(rw[0]["Amount"]);
                    }
                    lblCostLedgerName.Text = Convert.ToString(rw[0]["Ledger_Nm"]);
                    hidAmount.Value = Convert.ToString(dAmnt);
                    DataRow[] rwGrid = DtCost.Select("Id='" + Convert.ToInt32(hidCostRowId.Value) + "'");
                    if (rwGrid != null && (rwGrid.Length) > 0)
                    {
                        DtCostGrid = (DataTable)ViewState["DtGridCost"];
                        foreach (DataRow Dr in rwGrid)
                        {
                            /// DataTable Dt = ApplicationFunction.CreateTable("tbl", "Truck_No", "String", "Amount", "String", "Truck_Idno", "String", "Id", "String");
                            ApplicationFunction.DatatableAddRow(DtCostGrid, "", Dr["Amount"], Dr["Truck_Idno"], Dr["Id"]);
                        }
                        ViewState["DtGridCost"] = DtCostGrid;
                        BindCostCenterGrid();
                    }
                    else
                    {
                        DtCostGrid = CreateCostcenterDt();
                        ViewState["DtGridCost"] = DtCostGrid;
                        AddROwcostCenter("0", Convert.ToDouble(hidAmount.Value).ToString("N2"), 0, Convert.ToInt32(hidCostRowId.Value));
                    }

                }
                else
                {
                    DataRow[] rw = DtTemp.Select("Id='" + Convert.ToInt32(hidCostRowId.Value) + "'");
                    if (rw.Length > 0)
                    {
                        dAmnt = Convert.ToDouble(rw[0]["Amount"]);
                    }
                    lblCostLedgerName.Text = Convert.ToString(rw[0]["Ledger_Nm"]);
                    hidAmount.Value = Convert.ToString(dAmnt);
                    DtCostGrid = CreateCostcenterDt();
                    ViewState["DtGridCost"] = DtCostGrid;
                    AddROwcostCenter("0", Convert.ToDouble(hidAmount.Value).ToString("N2"), 0, Convert.ToInt32(hidCostRowId.Value));
                }


            }
            else if (e.CommandName == "cmdCostRemove")
            {
                DtCost = (DataTable)ViewState["DtCost"];
                if (DtCost != null && DtCost.Rows.Count > 0)
                {

                    DataRow[] DrRow = DtCost.Select("Id=" + id + "");
                    if (DrRow != null && DrRow.Length > 0)
                    {
                        foreach (DataRow dr in DrRow)
                        {
                            DtCost.Rows.Remove(dr);
                        }
                    }
                }
                ViewState["DtCost"] = DtCost;
            }
            if (grdCostdetls.Rows.Count > 0)
            {
                DropDownList ddl1 = (DropDownList)grdCostdetls.Rows[0].FindControl("ddlTruckNo");
                ddl1.Focus();
            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdCostdetls_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DtCostGrid = (DataTable)ViewState["DtGridCost"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlTruckNo = (DropDownList)e.Row.FindControl("ddlTruckNo");
                TextBox txtAmnt = (TextBox)e.Row.FindControl("txtAmnt");
                HiddenField hidTruckIdno = (HiddenField)e.Row.FindControl("hidTruckIdno");
                ImageButton imgAddnew = (ImageButton)e.Row.FindControl("imgAddnew");
                BindTruckNo(ddlTruckNo);
                ddlTruckNo.SelectedValue = Convert.ToString(hidTruckIdno.Value);
                txtAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                if (DtCostGrid != null && DtCostGrid.Rows.Count > 0)
                {
                    if (e.Row.RowIndex == DtCostGrid.Rows.Count - 1)
                    {
                        imgAddnew.Visible = true;
                    }
                    else
                    {
                        imgAddnew.Visible = false;
                    }
                }
            }
        }

        protected void grdCostdetls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNewRow")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                DtCostGrid = (DataTable)ViewState["DtGridCost"];
                SetPriviousData();

            }
        }
        #endregion

        #region Export to Excel
        //protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        //{
        //}

        //protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        //{
        //}

        protected void rptpaymentvoucher_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                double amt = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                rptTotalAmount += amt;
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblrpttotalamount = e.Item.FindControl("lblrpttotalamount") as Label;
                lblrpttotalamount.Text = rptTotalAmount.ToString("N2");
            }
        }

        protected void rptJournalContra_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptJournalContra_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                double amt = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Label lbldebit = e.Item.FindControl("lbldebit") as Label;
                Label lblcredit = e.Item.FindControl("lblcredit") as Label;
                if (ddlLdgrTyp.SelectedValue == "1")//credit
                {
                    lblcredit.Text = amt.ToString("N2");
                    creditTotalAmount += amt;
                }
                else
                {
                    lbldebit.Text = amt.ToString("N2");
                    debitTotalAmount += amt;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
            }
        }
        #endregion

        #region Date Range FinYear ...

        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));

            if (ddlDateRange.SelectedIndex == 0)
            {
                txtDate.Text = hidmindate.Value;
                txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
            else
            {
                txtDate.Text = hidmindate.Value;
            }
        }
        #endregion

        #region Functions...

        [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]
        public static string[] GetNarration(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();

            List<string> NARR_NAME = new List<string>();
            DataTable dtNames = new DataTable();
            string sqlQuery = "SELECT NARR_NAME FROM NARRMAST WHERE NARR_NAME LIKE '" + prefixText + "' + '%'";
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                da.Fill(dtNames);
                foreach (DataRow row in dtNames.Rows)
                {
                    string name = Convert.ToString(row["NARR_NAME"]);
                    NARR_NAME.Add(name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NARR_NAME.ToArray<string>();

        }
        public void SuccessMessage(string strMsg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        public void ErrorMessage(string strMsg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessageError('" + strMsg + "')", true);
        }
        #endregion
    }
}