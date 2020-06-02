using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class FuelSlip : Pagebase
    {
        #region Variables declaration...
        // private int intFormId = 19;
        DataTable dtTemp = new DataTable();
        DataTable dtTemp1 = new DataTable();
        DataTable dtDelete = new DataTable();
        DataTable dtfuel = new DataTable();
        double dblNetAmnt = 0, totQty = 0;
        int Count = 0;
        #endregion

        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                this.BindCity();
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                dtTemp = dtDelete = CreateDt();
                ViewState["dt"] = dtTemp;
                ViewState["dtDelete"] = dtDelete;
                this.BindTruck();
                this.BindPump();
                this.GetMax(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
                this.BindDriver();
                this.BindItems();
                BindGridT();
                ddlDateRange.Focus();
                if (Request.QueryString["FuelSlipIdno"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["FuelSlipIdno"]));
                    lnkbtnNew.Visible = true;
                    if (CompName == "WEST END ROADLINES")
                        lnkbtnPrintVoucher.Visible = true;
                    else
                        lnkbtnPrint.Visible = true;
                    
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
            }
        }

        #endregion

        #region Button Events...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (this.CheckExists(string.IsNullOrEmpty(Convert.ToString(txtSlipNo.Text.Trim())) ? 0 : Convert.ToInt32(txtSlipNo.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue)) == true)
            {
                txtSlipNo.Focus();
                return;
            }
            string strMsg = string.Empty;
            FuelSlipDAL objFuelSlip = new FuelSlipDAL();
            Int64 intSlip = 0;
            DateTime? FuelDate = null;
            FuelDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            DateTime DateAdded = System.DateTime.Now;
            Int64 yearId = Convert.ToInt32(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
            DataTable DT = (DataTable)ViewState["dt"];
            if (DT != null && DT.Rows.Count > 0)
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    //if (ViewState["dtDelete"] != null) { dtDelete = (DataTable)ViewState["dtDelete"]; }

                    if (string.IsNullOrEmpty(hidfuelIdno.Value) == true)
                    {
                        intSlip = objFuelSlip.Insert(string.IsNullOrEmpty(txtSlipNo.Text) ? 0 : Convert.ToInt64(txtSlipNo.Text), FuelDate, string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue), string.IsNullOrEmpty(ddlLorry.SelectedValue) ? 0 : Convert.ToInt64(ddlLorry.SelectedValue), string.IsNullOrEmpty(ddlPPump.SelectedValue) ? 0 : Convert.ToInt64(ddlPPump.SelectedValue), string.IsNullOrEmpty(ddlDriver.SelectedValue) ? 0 : Convert.ToInt64(ddlDriver.SelectedValue), yearId, DateAdded, Convert.ToDouble(txtNetAmnt.Text), DT, string.IsNullOrEmpty(Convert.ToString(txtInvoiceNo.Text.Trim())) ? "" : Convert.ToString(txtInvoiceNo.Text.Trim()));

                    }
                    else
                    {
                        intSlip = objFuelSlip.Update(Convert.ToInt64(hidfuelIdno.Value), string.IsNullOrEmpty(txtSlipNo.Text) ? 0 : Convert.ToInt64(txtSlipNo.Text), FuelDate, string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue), string.IsNullOrEmpty(ddlLorry.SelectedValue) ? 0 : Convert.ToInt64(ddlLorry.SelectedValue), string.IsNullOrEmpty(ddlPPump.SelectedValue) ? 0 : Convert.ToInt64(ddlPPump.SelectedValue), string.IsNullOrEmpty(ddlDriver.SelectedValue) ? 0 : Convert.ToInt64(ddlDriver.SelectedValue), yearId, DateAdded, Convert.ToDouble(txtNetAmnt.Text), DT, string.IsNullOrEmpty(Convert.ToString(txtInvoiceNo.Text.Trim())) ? "" : Convert.ToString(txtInvoiceNo.Text.Trim()));
                    }
                    objFuelSlip = null;
                    if (intSlip > 0)
                    {

                        if (this.PostIntoAccounts(intSlip, "FS", 0, 0, 0, 0) == true)
                        {

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(hidfuelIdno.Value)) == false)
                                {
                                    hidpostingmsg.Value = "Record(s) not updated.";
                                }
                                else
                                {
                                    hidpostingmsg.Value = "Record(s) not saved.";
                                }
                                Tran.Dispose();
                            }
                            Tran.Dispose();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessage('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                            return;
                        }
                    }
                    if (intSlip > 0)
                    {
                        if (string.IsNullOrEmpty(hidfuelIdno.Value) == false)
                        {
                            Tran.Complete();
                            strMsg = "Record updated successfully.";
                        }
                        else
                        {
                            Tran.Complete();
                            strMsg = "Record saved successfully.";
                        }
                        ClearControls();

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hidfuelIdno.Value) == false)
                        {
                            Tran.Dispose();
                            strMsg = "Record not updated.";
                        }
                        else
                        {
                            Tran.Dispose();
                            strMsg = "Record not saved.";
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            else
            {
                strMsg = "Please Select Item Details!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
        }

        private void GetMax(Int64 YearIdno)
        {
            FuelSlipDAL obj = new FuelSlipDAL();
            Int64 SlipNo = obj.GetMaxSlipNo(YearIdno);
            txtSlipNo.Text = (SlipNo + 1).ToString();

        }
        private bool CheckExists(Int32 intSlipNo, Int32 intYearIdno)
        {
            FuelSlipDAL obj = new FuelSlipDAL();
            var lst = obj.CheckNo(intSlipNo, intYearIdno);
            if (lst != null && lst.FuelSlip_No > 0)
            {
                ShowMessageErr("Slip No. " + lst.FuelSlip_No + "already exists.");
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("FuelSlip.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["FuelSlipIdno"] != null)
            {
                this.Populate(Convert.ToInt64(Request.QueryString["FuelSlipIdno"].ToString()));
                lnkbtnPrint.Visible = true;
                lnkBtnVouchr.Visible = true;
                lnkbtnNew.Visible = true;
            }
            else
            {
                Response.Redirect("FuelSlip.aspx");
            }

        }
        #endregion

        #region Miscellaneous Function...
        private void Populate(Int64 FuelSlipIdno)
        {
            FuelSlipDAL objMast = new FuelSlipDAL();
            var objFuelSlip = objMast.SelectById(FuelSlipIdno);
            if (objFuelSlip != null)
            {
                lnkBtnLast.Visible = false;
                lnkBtnVouchr.Visible = false;
                ddlDateRange.SelectedValue = Convert.ToString(objFuelSlip.Year_Idno);
                txtDate.Text = string.IsNullOrEmpty(objFuelSlip.FuelSlip_Date.ToString()) ? "" : Convert.ToDateTime(objFuelSlip.FuelSlip_Date).ToString("dd-MM-yyyy");
                txtSlipNo.Text = Convert.ToString(objFuelSlip.FuelSlip_No);
                ddlLocation.SelectedValue = Convert.ToString(objFuelSlip.Loc_Idno);
                ddlLorry.SelectedValue = Convert.ToString(objFuelSlip.Truck_Idno);
                ddlLorry_SelectedIndexChanged(null, null);
                ddlPPump.SelectedValue = Convert.ToString(objFuelSlip.Pump_Idno);
                ddlDriver.SelectedValue = Convert.ToString(objFuelSlip.Driver_Idno);
                txtInvoiceNo.Text = Convert.ToString(objFuelSlip.Invoice_No);
                txtSlipNo.Text = Convert.ToString(objFuelSlip.FuelSlip_No);
                hidfuelIdno.Value = Convert.ToString(objFuelSlip.FuelSlip_Idno);
                txtNetAmnt.Text = Convert.ToDouble(objFuelSlip.Net_Amnt).ToString("N2");
                var VarFuelDetl = objMast.SelectDetlById(FuelSlipIdno);
                dtTemp = dtDelete = CreateDt();

                if (VarFuelDetl != null && VarFuelDetl.Count > 0)
                {
                    for (int i = 0; i < VarFuelDetl.Count; i++)
                    {
                        ApplicationFunction.DatatableAddRow(dtTemp, i + 1, Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "LocName")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "LocId")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "LorryName")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "LorryId ")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "Pump")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "PumpId")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "Driver")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "DriverId")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "ItemName")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "ItemId")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "Qty")), Convert.ToString(DataBinder.Eval(VarFuelDetl[i], "Amount")));
                    }
                    ViewState["dt"] = dtTemp;
                    dtTemp = (DataTable)ViewState["dt"];
                    this.BindGridT();
                }

            }
            objMast = null;
            Print(FuelSlipIdno);
            Printf(FuelSlipIdno);
        }
        double rptTotalAmount = 0, rptTotalWeight = 0, rptTotalQty = 0;
        private void Print(Int64 FuelSlipIdno)
        {
            Repeater obj = new Repeater();

            string CompName = ""; string TinNo = ""; string FaxNo = "";

            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");

            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblCompAdd1.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            lblCompAdd2.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);

            lblCompPhNo.Text = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            lblCompCity.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            lblCompState.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spFuelBill] @ACTION='SelectPrint',@FuelSlip_Idno='" + FuelSlipIdno + "'");
            dsReport.Tables[0].TableName = "Print";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblSlipno.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["SLIPNO"]);
                lblSlipDate.Text = Convert.ToDateTime(dsReport.Tables["Print"].Rows[0]["SLIPDATE"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["LOCATION"]);
                lbllorry.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["LORRYNO"]);
                lblDriver.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["DRIVERNAME"]);
                lblpump.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["PUMPNAME"]);
                lblInvoiceNo.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["Invoice_No"]);

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();

            }
        }
        private void Printf(Int64 FuelSlipIdno)
        {
            string CompName = ""; string TinNo = ""; string FaxNo = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");

            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblCompAdd1f.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            lblCompAdd2f.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            lblCompPhNof.Text = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            lblCompCityf.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            lblCompStatef.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName; lblCompanynamef.Text = "For - " + CompName;
            if (FaxNo == "")
            {
                lblCompFaxNof.Visible = false; lblFaxNof.Visible = false;
            }
            else
            {
                lblCompFaxNof.Text = FaxNo;
                lblCompFaxNof.Visible = true; lblFaxNof.Visible = true;
            }

            if (TinNo == "")
            {
                lblCompTINf.Visible = false; lblTinf.Visible = false;
            }
            else
            {
                lblCompTINf.Text = TinNo;
                lblCompTINf.Visible = true; lblTinf.Visible = true;
            }

            #endregion


            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spFuelBillVoucher] @ACTION='SelectPrint',@FuelSlip_Idno='" + FuelSlipIdno + "'");
            dsReport.Tables[0].TableName = "Printf";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lbldate.Text = string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(dsReport.Tables["Printf"].Rows[0]["SLIPDATE"]));
                //lblDate.Text = Convert.ToDateTime(dsReport.Tables["Printf"].Rows[0]["SLIPDATE"]).ToString("dd-MM-yyyy");
                lblNo.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["NO"]);
                //lblFromCity.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["LOCATION"]);
                // lblDriver.Text = Convert.ToString(dsReport.Tables["Print"].Rows[0]["DRIVERNAME"]);
                lblpummp.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["PUMPNAME"]);
                lblTruckNo.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["TRUCKNO"]);
                lblGCNo.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["G.C.NO"]);
                lblAmt.Text = Convert.ToString(dsReport.Tables["Printf"].Rows[0]["AMOUNT"]);

               
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                rptTotalAmount += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                rptTotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblTotalAmnt.Text = rptTotalAmount.ToString("N2");
                lbltotalqty.Text = rptTotalQty.ToString();

            }
        }
        private void ClearControls()
        {
            this.GetMax(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
            ddlLocation.SelectedIndex = -1;
            ddlLocation.Enabled = true;
            ddlLorry.SelectedValue = "0";
            lnkBtnLast.Visible = true;
            lnkBtnVouchr.Visible = true;
            ddlDriver.SelectedValue = "0";
            ddlPPump.SelectedValue = "0";
            txtNetAmnt.Text = txtInvoiceNo.Text = string.Empty;
            hidfuelIdno.Value = string.Empty;
            dtTemp = null; ViewState["dt"] = null; this.BindGridT();
            dtTemp = CreateDt();
            ViewState["dt"] = dtTemp;
            dtfuel = null; ViewState["dt"] = null; this.BindGridT();
            dtfuel = CreateDt1();
            ViewState["dt"] = dtfuel;
            lnkbtnNew.Visible = false;

        }
        private void CalQty()
        {
            try
            {
                FuelSlipDAL obj = new FuelSlipDAL();
                DataTable dt = obj.GetItemDetails(Convert.ToInt64(ddlItemName.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(hidFuelPrice.Value) > 0)
                    {
                        txtQty.Text = Convert.ToString(Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(txtRate.Text.Trim().Replace(",", ""))) ? 0 : Convert.ToDouble(Convert.ToDouble(txtRate.Text.Trim().Replace(",", "")).ToString("N2"))) / Convert.ToDouble(hidFuelPrice.Value ?? "0")).ToString("N2"));
                        // txtRate.Text = Convert.ToDouble(dt.Rows[0]["Rate"]).ToString("N2");
                    }
                    else
                    {
                        txtQty.Text = "0.00";
                        txtRate.Text = "0.00";
                        hidFuelPrice.Value = "0";
                    }
                }
                else
                {
                    txtQty.Text = "0.00";
                    txtRate.Text = "0.00";
                }
                ddlItemName.Focus();
            }
            catch (Exception Ex) { }
        }
        private void FuelRate()
        {
            if (Convert.ToInt32(ddlPPump.SelectedValue) > 0)
            {
                ddlItemName.SelectedIndex = 0;
                FuelSlipDAL obj = new FuelSlipDAL();
                tblFuelRateMaster lst = obj.SelectRate(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())), Convert.ToInt32(ddlPPump.SelectedValue));
                if (lst != null && lst.FuelRate_Idno > 0)
                {
                    txtQty.Text = "0.00";
                    txtRate.Text = "0.00";
                    hidFuelPrice.Value = "0";
                }
                else
                {
                    txtQty.Text = "0.00";
                    txtRate.Text = "0.00";
                }
            }
            else
            {
                txtQty.Text = "0.00";
                txtRate.Text = "0.00";
            }

        }

        private void CalItemRate()
        {
            try
            {
                FuelSlipDAL obj = new FuelSlipDAL();
                DataTable dt = obj.GetItemDetails(Convert.ToInt64(ddlItemName.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(hidFuelPrice.Value) > 0)
                    {
                        txtRate.Text = Convert.ToDouble(hidFuelPrice.Value ?? "0").ToString("N2");
                        txtQty.Text = Convert.ToString(Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(txtRate.Text.Trim().Replace(",", ""))) ? 0 : Convert.ToDouble(Convert.ToDouble(txtRate.Text.Trim().Replace(",", "")).ToString("N2"))) / Convert.ToDouble(hidFuelPrice.Value ?? "0")).ToString("N2"));

                    }
                    else
                    {
                        txtQty.Text = "0.00";
                        txtRate.Text = "0.00";
                    }
                }
                else
                {
                    txtQty.Text = "0.00";
                    txtRate.Text = "0.00";
                }
                ddlItemName.Focus();
            }
            catch (Exception Ex) { }
        }
        private void CalAmnt()
        {
            try
            {
                FuelSlipDAL obj = new FuelSlipDAL();
                DataTable dt = obj.GetItemDetails(Convert.ToInt64(ddlItemName.SelectedValue), ApplicationFunction.ConnectionString());
                if (dt.Rows.Count > 0)
                {
                    if ((Convert.ToDouble(hidFuelPrice.Value) > 0) && (Convert.ToDouble(txtQty.Text.Trim()) > 0))
                    {
                        if ((string.IsNullOrEmpty(Convert.ToString(txtQty.Text.Trim())) ? 0 : Convert.ToDouble(txtQty.Text.Trim())) != 0)
                        {
                            txtRate.Text = Convert.ToString(Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(txtQty.Text.Trim().Replace(",", ""))) ? 0 : Convert.ToDouble(Convert.ToDouble(txtQty.Text.Trim().Replace(",", "")).ToString("N2"))) * Convert.ToDouble(hidFuelPrice.Value ?? "0")).ToString("N2"));
                        }
                        else
                        {
                            txtRate.Text = Convert.ToDouble(hidFuelPrice.Value ?? "0").ToString("N2");
                        }
                    }
                    else
                    {
                        txtRate.Text = "0.00";
                        txtQty.Text = "0.00";
                    }
                }
                else
                {
                    txtQty.Text = "0.00";
                    txtRate.Text = "0.00";
                }
                ddlItemName.Focus();
            }
            catch (Exception Ex) { }
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Location", "String",
                "LocationIdno", "String",
                "Lorry", "String",
                "LorryIdno", "String",
                "Pump", "String",
                "PumpIdno", "String",
                "Driver", "String",
                "DriverIdno", "String",
                "ItemName", "String",
                "ItemNameIdno", "String",
                "Qty", "String",
                "Rate", "String"
                );
            return dttemp;
        }
        private DataTable CreateDt1()
        {
            DataTable DTFUEL = ApplicationFunction.CreateTable("tbl",
                "ID", "String",
                "SLIPDATE", "String",
                "NO", "String",
                "PUMPNAME", "String",
                "PumpIdno", "String",
                "TRUCKNO", "String",
                "TRUCKIDNO", "String",
                "C.G.NO", "String",
                "AMOUNT", "String"
                );
            return DTFUEL;
        }
        private void BindCity()
        {
            OpenTyreDAL objTollMastDAL = new OpenTyreDAL();
            var lst = objTollMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("----Select ----", "0"));
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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
                }
                else
                {
                    grdMain.Visible = false;
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                grdMain.Visible = false;
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
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
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlLocation.Focus();
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEffectiveRate();
        }

        private void GetEffectiveRate()
        {
            FuelSlipDAL obj = new FuelSlipDAL();
            txtRate.Text = "0.00";
            txtQty.Text = "0.00";
            hidFuelPrice.Value = "0";
            if (ddlPPump.SelectedValue == "") return;
            if (ddlItemName.SelectedValue == "") return;
            DataTable dt = obj.GetEffectivePrice(Convert.ToInt64(ddlPPump.SelectedValue), Convert.ToInt64(ddlItemName.SelectedValue), txtDate.Text, ApplicationFunction.ConnectionString());
            if (dt.Rows.Count > 0)
            {
                try
                {
                    hidFuelPrice.Value = dt.Rows[0]["Fuel_Rate"].ToString();
                    txtRate.Text = dt.Rows[0]["Fuel_Rate"].ToString();
                    txtQty.Text = "1";
                }
                catch (Exception ex)
                {
                    txtRate.Text = "0.00";
                    txtQty.Text = "0.00";
                    hidFuelPrice.Value = "0";
                }
            }
            else
            {
                ShowMessageErr("No effective price for selected date.");
            }
        }

        private void BindPump()
        {
            FuelSlipDAL objclsFuelSlip = new FuelSlipDAL();
            var objFuelSlip = objclsFuelSlip.SelectPCompName();
            objclsFuelSlip = null;
            ddlPPump.DataSource = objFuelSlip;
            ddlPPump.DataTextField = "Acnt_Name";
            ddlPPump.DataValueField = "Acnt_Idno";
            ddlPPump.DataBind();
            ddlPPump.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }

        private void BindDriver()
        {
            FuelSlipDAL objclsFuelSlip = new FuelSlipDAL();
            var objFuelSlip = objclsFuelSlip.SelectDriver();
            objclsFuelSlip = null;
            ddlDriver.DataSource = objFuelSlip;
            ddlDriver.DataTextField = "Driver_Name";
            ddlDriver.DataValueField = "Driver_Idno";
            ddlDriver.DataBind();
            ddlDriver.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
        private void BindItems()
        {
            FuelSlipDAL objclsFuelSlip = new FuelSlipDAL();
            var objFuelSlip = objclsFuelSlip.SelectItemName();
            objclsFuelSlip = null;
            ddlItemName.DataSource = objFuelSlip;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_Idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
        private void BindTruck()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNoPurchase();
            ddlLorry.DataSource = TruckNolst;
            ddlLorry.DataTextField = "Lorry_No";
            ddlLorry.DataValueField = "lorry_idno";
            ddlLorry.DataBind();
            ddlLorry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private bool IsExist(string SerialNo)
        {
            int Count = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(clsMultipleDB.strDynamicConString()))
            {
                Count = db.Stckdetls.Where(r => r.SerialNo == SerialNo).Count();
            }
            if (Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region Account Posting GR by Mahesh Kumawat

        private bool PostIntoAccounts(Int64 intDocIdno, string strDocType, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno)
        {
            #region Variables Declaration...


            FuelSlipDAL DAL = new FuelSlipDAL();
            Int64 intVchrIdno = 0;
            Int64 intValue = 0, IntRcptType = 0; Int32 TrAcntIdno = 0;
            hidpostingmsg.Value = string.Empty;
            Int64 DrivrIdno = Convert.ToInt32(ViewState["LTyp"]) == 0 ? Convert.ToInt64(ddlDriver.SelectedValue) : Convert.ToInt64(DAL.GetOwnerIdbyLorryIdno(Convert.ToInt64(ddlDriver.SelectedValue)));
            double dTotAmnt = 0;

            dTotAmnt = Convert.ToDouble(Convert.ToString(txtNetAmnt.Text) == "" ? 0 : Convert.ToDouble(txtNetAmnt.Text));
            Int64 intDocNo = 0;

            intDocNo = Convert.ToInt64(txtSlipNo.Text.Trim());


            DateTime? dtGRDate = null;
            DateTime? dtBankDate = null;
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            #endregion

            #region  Posting Start...

            if (Request.QueryString["FuelSlipIdno"] == null)
            {
                intValue = 1;
            }
            else
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
            }
            if (intValue > 0)   /*Insert In VchrHead*/
            {
                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim())),
                4,
               0,
                "Fuel Slip No : " + Convert.ToString(intDocNo) + "  Date: " + txtDate.Text.Trim(),
                true,
                0,
                strDocType,
                Convert.ToInt64((ddlDriver.SelectedIndex == -1) ? 0 : Convert.ToInt64(ddlDriver.SelectedValue)),
                0,
                0,
                ApplicationFunction.GetIndianDateTime().Date,
                0,
                0,
                Convert.ToInt32(ddlDateRange.SelectedValue),
                0, intUserIdno);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;

                    #region Gross Amount Posting...
                    dtBankDate = null;

                    double TotAmnt = dTotAmnt;   /*Insert In VchrDetl*/
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    Convert.ToInt64((ddlPPump.SelectedIndex == -1) ? 0 : Convert.ToInt64(ddlPPump.SelectedValue)),
                   "Fuel Slip No : " + Convert.ToString(intDocNo) + "  Date: " + txtDate.Text.Trim(),
                    TotAmnt,
                    Convert.ToByte(1),
                    Convert.ToByte(0),
                    "",
                    true,
                    dtBankDate,  //please check here if date is Blank
                    "0",
                    0);
                    if (intValue > 0)
                    {
                        intVchrIdno = intValue;
                        intValue = 0; IntRcptType = 0;

                        intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt64((ddlDriver.SelectedIndex == -1) ? 0 : DrivrIdno),
                            "Fuel Slip No : " + Convert.ToString(intDocNo) + "  Date: " + txtDate.Text.Trim(),
                            TotAmnt,
                            Convert.ToByte(2),
                            Convert.ToByte(0),
                            "0",
                            false,
                            dtBankDate,  //please check here if date is Blank
                            "0",
                            0);
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

            #endregion

            #region Deallocate variables...

            objclsAccountPosting = null;

            return true;

            #endregion
        }

        #endregion
        #endregion

        #region Button Entry Event....
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            if (txtRate.Text == "" || Convert.ToDouble(txtRate.Text) <= 0) { ShowMessageErr("Rate should be greater than zero."); txtRate.Focus(); return; }

            string TotalAmount = string.Empty;

            dtTemp = (DataTable)ViewState["dt"];
            if (ViewState["ID"] != null)
            {
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(ViewState["ID"].ToString()))
                    {
                        dtrow["Location"] = ddlLocation.SelectedItem.Text;
                        dtrow["LocationIdno"] = ddlLocation.SelectedValue;
                        dtrow["Lorry"] = ddlLorry.SelectedItem.Text;
                        dtrow["LorryIdno"] = ddlLorry.SelectedValue;
                        dtrow["Pump"] = ddlPPump.SelectedItem.Text;
                        dtrow["PumpIdno"] = ddlPPump.SelectedValue;
                        dtrow["Driver"] = ddlDriver.SelectedItem.Text;
                        dtrow["DriverIdno"] = ddlDriver.SelectedValue;
                        dtrow["ItemName"] = ddlItemName.SelectedItem.Text;
                        dtrow["ItemNameIdno"] = ddlItemName.SelectedValue;
                        dtrow["Qty"] = txtQty.Text.Trim();
                        dtrow["Rate"] = Convert.ToDouble(txtRate.Text.Trim()).ToString("N2"); ;
                    }
                }
                ViewState["ID"] = null;
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        if (Convert.ToString(row["ItemNameIdno"]) == Convert.ToString(ddlItemName.SelectedValue))
                        {
                            string msg = "Item Name is Already Entered!";
                            ddlItemName.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                            return;
                        }
                    }
                }

                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string Location = ddlLocation.SelectedItem.Text;
                string LocationIdno = ddlLocation.SelectedValue;
                string Lorry = ddlLorry.SelectedItem.Text;
                string LorryIdno = ddlLorry.SelectedValue;
                string Pump = ddlPPump.SelectedItem.Text;
                string PumpIdno = ddlPPump.SelectedValue;
                string Driver = ddlDriver.SelectedItem.Text;
                string DriverIdno = ddlDriver.SelectedValue;
                string Item = ddlItemName.SelectedItem.Text;
                string ItemIdno = ddlItemName.SelectedValue;
                string Qty = txtQty.Text.ToString();
                string Rate = Convert.ToDouble(txtRate.Text.Trim()).ToString("N2");
                ApplicationFunction.DatatableAddRow(dtTemp, id, Location, LorryIdno, Lorry, LorryIdno, Pump, PumpIdno, Driver, DriverIdno, Item, ItemIdno, Qty, Rate);
            }
            ViewState["dt"] = dtTemp;
            this.BindGridT();
            ddlItemName.SelectedIndex = -1;
            txtQty.Text = "0.00";
            txtRate.Text = "0.00";
            hidFuelPrice.Value = "0";
            ddlItemName.Focus();
        }

        protected void lnkbtnNewClick_OnClick(object sender, EventArgs e)
        {
            ddlItemName.SelectedIndex = -1;
            txtQty.Text = "0.00";
            txtRate.Text = "0.00";
            hidFuelPrice.Value = "0";
            ddlItemName.Focus();

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ddlDateRange.Focus();
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        #endregion

        #region Grid Event....

        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Count = 0;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OpenTyreDAL obj = new OpenTyreDAL();
                int ICount = obj.ICount(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemName")));
                double amnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                dblNetAmnt = amnt + dblNetAmnt;
                if (ICount > 0)
                {
                    LinkButton lblDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    LinkButton lbledit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    lblDelete.Enabled = false;
                    lblDelete.ToolTip = "Issued";
                    lbledit.Enabled = false;
                    lbledit.ToolTip = "Issued";
                }
                Count++;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblRecordCount");
                Label lblAmntTot = (Label)e.Row.FindControl("lbltotAmount");
                lblQuantity.Text = Convert.ToString(grdMain.Rows.Count);
                txtNetAmnt.Text = dblNetAmnt.ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["ItemNameIdno"]);
                    txtQty.Text = Convert.ToString(drs[0]["Qty"]);
                    txtRate.Text = Convert.ToString(drs[0]["Rate"]);

                    ViewState["ID"] = Convert.ToString(drs[0]["id"]);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable dtInnerDelete = CreateDt();
                DataTable objDataTable = CreateDt();
                DataTable objDataTable1 = CreateDt1();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Location"], rw["LocationIdno"], rw["Lorry"], rw["LorryIdno"], rw["Pump"], rw["PumpIdno"], rw["Driver"], rw["DriverIdno"], rw["ItemName"], rw["ItemNameIdno"], rw["Qty"], rw["Rate"]);
                    }
                    else
                    {
                        ApplicationFunction.DatatableAddRow(dtInnerDelete, rw["id"], rw["Location"], rw["LocationIdno"], rw["Lorry"], rw["LorryIdno"], rw["Pump"], rw["PumpIdno"], rw["Driver"], rw["DriverIdno"], rw["ItemName"], rw["ItemNameIdno"], rw["Qty"], rw["Rate"]);
                    }
                }
                ViewState["dtDelete"] = dtInnerDelete;
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
            }

        }

        #endregion

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalQty();
            }
            catch (Exception Ex) { }
        }

        protected void ddlPPump_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.FuelRate();
            }
            catch (Exception Ex) { }

        }

        protected void ddlLorry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlLorry.SelectedValue) > 0)
            {
                FuelSlipDAL ObjDAl = new FuelSlipDAL();

                var lst = ObjDAl.selectTruckType(Convert.ToInt32(ddlLorry.SelectedValue));
                if (lst != null)
                {
                    ViewState["LTyp"] = lst.Lorry_Type;
                    ddlDriver.DataSource = null;
                    if (ddlDriver.Items.Count > 0)
                    {
                        ddlDriver.Items.Clear();
                    }
                    BindDriver(Convert.ToInt32(lst.Lorry_Type));
                    ddlDriver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(lst.Driver_Idno)) ? "0" : Convert.ToString(lst.Driver_Idno);
                }
            }
        }
        private void BindDriver(Int32 var)
        {
            FuelSlipDAL obj = new FuelSlipDAL();
            if (var == 0)
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Acnt_Name";
                    ddlDriver.DataValueField = "Acnt_Idno";
                    ddlDriver.DataBind();
                }
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Driver_name";
                    ddlDriver.DataValueField = "Driver_Idno";
                    ddlDriver.DataBind();
                }
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void txtSlipNo_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckExists(string.IsNullOrEmpty(Convert.ToString(txtSlipNo.Text.Trim())) ? 0 : Convert.ToInt32(txtSlipNo.Text.Trim()), string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue)) == true)
            {
                txtSlipNo.Focus();
                return;
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalAmnt();
            }
            catch (Exception Ex) { }
        }
        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            FuelSlipDAL objDAL = new FuelSlipDAL();
            Int64 iMaxIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString());
            if (iMaxIdno > 0)
            {
                Print(iMaxIdno);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
            else
            {
                ShowMessageErr("No Record For Print.");
            }
        }
        protected void lnkBtnVouchr_Click(object sender, EventArgs e)
        {
            FuelSlipDAL objDAL = new FuelSlipDAL();
            Int64 iMaxIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString());
            if (iMaxIdno > 0)
            {
                Printf(iMaxIdno);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrintf('printf')", true);
            }
            else
            {
                ShowMessageErr("No Record For Print.");
            }
        }


        private void ExportExcelHeader(DataTable Dt)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FlipSlipFormat.xls"));
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
                Response.Flush();
                Response.End();
            }
            catch { }
        }

        protected void lnkbtnExport_Click(object sender, EventArgs e)
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
               "SlipNo", "String",
               "LorryNo", "String",
               "Driver", "String",
               "ItemName", "String",
               "Amount", "String"
               );
            ExportExcelHeader(dttemp);
        }

        private bool CheckDuplicatieItemForExcel(DataTable dtItemCheck, string ItemName, string LorryNo)
        {
            bool value = false; int Rowscount = 0;
            if ((dtItemCheck != null) && (dtItemCheck.Rows.Count > 0))
            {
                foreach (DataRow row in dtItemCheck.Rows)
                {
                    if ((Convert.ToString(row["ItemName"]) == ItemName) && (Convert.ToString(row["LorryNo"]) == LorryNo))
                    {
                        Rowscount++;
                        //value = true;
                    }
                }
                if (Rowscount > 1)
                    value = true;
            }
            return value;
        }

        protected void lnkbtnUpload_Click(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedIndex == 0) { ShowMessageErr("Please select Location."); ddlLocation.Focus(); return; }
            if (ddlPPump.SelectedIndex == 0) { ShowMessageErr("Please select Petrol Pump."); ddlPPump.Focus(); return; }
            if (txtInvoiceNo.Text == "") { ShowMessageErr("Please select Invoice No."); txtInvoiceNo.Focus(); return; }

            string msg = string.Empty;
            if (FileUpload.HasFile)
            {
                FuelSlipDAL obj = new FuelSlipDAL();
                string excelfilename = string.Empty;

                #region UPLOAD EXCEL AT SERVER
                excelfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "ItemsexcelFuel", "FuelExcel");
                #endregion

                if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                {
                    DataTable dt = new DataTable();
                    DataTable dtnew = new DataTable();
                    string filepath = Server.MapPath("~/ItemsexcelFuel/" + excelfilename);
                    string constring = string.Empty;
                    if (System.IO.Path.GetExtension(filepath) == ".xls")
                    {
                        constring = "Provider=Microsoft.Jet.OLEDB.4.0;OLE DB Services=-4;Data Source='" + filepath + "';Extended Properties='Excel 8.0;HDR=Yes;'";
                    }
                    else if (System.IO.Path.GetExtension(filepath) == ".xlsx")
                    {
                        constring = "Provider= Microsoft.ACE.OLEDB.12.0;OLE DB Services=-4;Data Source='" + filepath + "'; Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    }

                    if (string.IsNullOrEmpty(constring) == false)
                    {
                        #region  Select Excel
                        OleDbConnection con = new OleDbConnection(constring);
                        con.Open();
                        DataTable ExcelTable = new DataTable();
                        ExcelTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        string SheetName = Convert.ToString(ExcelTable.Rows[0][2]);
                        OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "] WHERE [SlipNo] IS NOT NULL OR [LorryNo] IS NOT NULL OR [Driver] IS NOT NULL OR [ItemName] IS NOT NULL OR [Amount] IS NOT NULL", con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        #endregion

                        int resultget = obj.TurncatetblFuelSlipFromExcel(ApplicationFunction.ConnectionString());
                        #region INSERT RECORD IN tblChlnUploadFromExcel TABLE
                        Int64 intResult = 0;
                        using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                intResult = obj.InsertInFuelExcel(
                                    Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[i]["SlipNo"]) == "" ? "0" : ds.Tables[0].Rows[i]["SlipNo"]),
                                        Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["LorryNo"]) == "" ? "" : ds.Tables[0].Rows[i]["LorryNo"]),
                                        Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["Driver"]) == "" ? "" : ds.Tables[0].Rows[i]["Driver"]),
                                        Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["ItemName"]) == "" ? "" : ds.Tables[0].Rows[i]["ItemName"]),
                                        Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]) == "" ? "" : ds.Tables[0].Rows[i]["Amount"]));
                            }
                            if (intResult > 0)
                            { Tran.Complete(); }
                            else { Tran.Dispose(); }
                        }
                        #endregion
                        dtTemp1 = CreateDt();
                        dt = ApplicationFunction.CreateTable("tblFuelSlipFromExcel", "SlipNo", "String", "LorryNo", "String", "Driver", "String", "ItemName", "String", "Amount", "String");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns[0].Caption == "SlipNo" && ds.Tables[0].Columns[1].Caption == "LorryNo" && ds.Tables[0].Columns[2].Caption == "Driver" && ds.Tables[0].Columns[3].Caption == "ItemName" && ds.Tables[0].Columns[4].Caption == "Amount")
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (CheckDuplicatieItemForExcel(ds.Tables[0], Convert.ToString(ds.Tables[0].Rows[i]["ItemName"].ToString()), Convert.ToString(ds.Tables[0].Rows[i]["LorryNo"].ToString())) == false)
                                    {
                                        var Dist = obj.FuelExcelDistinctRecord();
                                        for (int x = 0; x < Dist.Count; x++)
                                        {
                                            dtTemp1 = CreateDt();
                                            var lorry = obj.FuelExcelRecord(Convert.ToString(DataBinder.Eval(Dist[x], "Lorry_No")));
                                            for (int y = 0; y < lorry.Count; y++)
                                            {
                                                txtSlipNo.Text = Convert.ToString(DataBinder.Eval(lorry[y], "Slip_No"));

                                                ddlLorry.SelectedValue = ddlLorry.Items.FindByText(Convert.ToString(DataBinder.Eval(lorry[y], "Lorry_No"))).Value;
                                                ddlLorry_SelectedIndexChanged(null, null);
                                                ddlDriver.SelectedValue = ddlDriver.Items.FindByText(Convert.ToString(DataBinder.Eval(lorry[y], "Driver"))).Value;

                                                ddlItemName.SelectedValue = ddlItemName.Items.FindByText(Convert.ToString(DataBinder.Eval(lorry[y], "ItemName"))).Value;
                                                // ddlItemName.SelectedItem.Text = Convert.ToString(DataBinder.Eval(lorry[y], "ItemName"));
                                                ddlPPump_SelectedIndexChanged(null, null);
                                                txtRate.Text = Convert.ToString(DataBinder.Eval(lorry[y], "Amount"));
                                                txtRate_TextChanged(null, null);

                                                //---------------------------------------------------------------------------------------

                                                Int32 ROWCount = Convert.ToInt32(dtTemp1.Rows.Count) - 1;
                                                int id = dtTemp1.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp1.Rows[ROWCount]["id"])) + 1;
                                                string Location = ddlLocation.SelectedItem.Text;
                                                string LocationIdno = ddlLocation.SelectedValue;
                                                string Lorry = ddlLorry.SelectedItem.Text;
                                                string LorryIdno = ddlLorry.SelectedValue;
                                                string Pump = ddlPPump.SelectedItem.Text;
                                                string PumpIdno = ddlPPump.SelectedValue;
                                                string Driver = ddlDriver.SelectedItem.Text;
                                                string DriverIdno = ddlDriver.SelectedValue;
                                                string Item = ddlItemName.SelectedItem.Text;
                                                string ItemIdno = ddlItemName.SelectedValue;
                                                string Qty = txtQty.Text.ToString();
                                                string Rate = Convert.ToDouble(txtRate.Text.Trim()).ToString("N2");
                                                ApplicationFunction.DatatableAddRow(dtTemp1, id, Location, LorryIdno, Lorry, LorryIdno, Pump, PumpIdno, Driver, DriverIdno, Item, ItemIdno, Qty, Rate);

                                            }
                                            string strMsg = string.Empty;
                                            FuelSlipDAL objFuelSlip = new FuelSlipDAL();
                                            Int64 intSlip = 0;
                                            DateTime? FuelDate = null;
                                            FuelDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                                            DateTime DateAdded = System.DateTime.Now;
                                            Int64 yearId = Convert.ToInt32(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
                                            //DataTable DT = (DataTable)ViewState["dt"];
                                            DataTable DT = dtTemp1;
                                            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                                            {
                                                //if (ViewState["dtDelete"] != null) { dtDelete = (DataTable)ViewState["dtDelete"]; }

                                                if (string.IsNullOrEmpty(hidfuelIdno.Value) == true)
                                                {
                                                    intSlip = objFuelSlip.Insert(string.IsNullOrEmpty(txtSlipNo.Text) ? 0 : Convert.ToInt64(txtSlipNo.Text), FuelDate, string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue), string.IsNullOrEmpty(ddlLorry.SelectedValue) ? 0 : Convert.ToInt64(ddlLorry.SelectedValue), string.IsNullOrEmpty(ddlPPump.SelectedValue) ? 0 : Convert.ToInt64(ddlPPump.SelectedValue), string.IsNullOrEmpty(ddlDriver.SelectedValue) ? 0 : Convert.ToInt64(ddlDriver.SelectedValue), yearId, DateAdded, Convert.ToDouble(txtNetAmnt.Text), DT, string.IsNullOrEmpty(Convert.ToString(txtInvoiceNo.Text.Trim())) ? "" : Convert.ToString(txtInvoiceNo.Text.Trim()));
                                                }

                                                objFuelSlip = null;
                                                if (intSlip > 0)
                                                {
                                                    if (this.PostIntoAccounts(intSlip, "FS", 0, 0, 0, 0) == true)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(hidpostingmsg.Value) == true)
                                                        {
                                                            if (string.IsNullOrEmpty(Convert.ToString(hidfuelIdno.Value)) == false)
                                                            {
                                                                hidpostingmsg.Value = "Record(s) not updated.";
                                                            }
                                                            else
                                                            {
                                                                hidpostingmsg.Value = "Record(s) not saved.";
                                                            }
                                                            Tran.Dispose();
                                                        }
                                                        Tran.Dispose();
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa", "PassMessage('" + Convert.ToString(hidpostingmsg.Value) + "')", true);
                                                        return;
                                                    }
                                                }
                                                if (intSlip > 0)
                                                {
                                                    Tran.Complete();
                                                    strMsg = "Record saved successfully.";

                                                }
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                                            }

                                            dtTemp1 = null; DT = null;
                                        }


                                    }

                                    else
                                    {
                                        msg = "Excel have duplicate records.";
                                        ShowMessageErr(msg);
                                        return;
                                    }
                                }

                                ClearControls();
                            }
                            else
                            {
                                msg = "Excel is blank or Excel is not in correct format.";
                                ShowMessageErr(msg);
                                return;
                            }
                        }

                    }


                }
                else
                {
                    msg = "Please Upload Correct Excel File";
                    ShowMessageErr(msg);
                    return;
                }
            }
            else
            {
                msg = "Please Upload Excel File";
                ShowMessageErr(msg);
                return;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FuelRate();
            }
            catch (Exception Ex) { }
        }
    }
}