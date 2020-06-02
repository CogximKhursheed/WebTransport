using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class StockTransferReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        private int intFormId = 43;
        int rows;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.View == false)
                //{
                //    lnkbtnPreview.Visible = true;
                //}
                this.BindDateRange();
                SetDate();
                BindLocation(); BindItemType();
            }
        }
        #endregion

        #region Button Event...


        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
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
        public void BindLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            ddlIssLoc.DataSource = lst;
            ddlIssLoc.DataTextField = "City_Name";
            ddlIssLoc.DataValueField = "City_Idno";
            ddlIssLoc.DataBind();
            ddlIssLoc.Items.Insert(0, new ListItem("--Select--", "0"));

            obj = null;
            ddlRecLoc.DataSource = lst;
            ddlRecLoc.DataTextField = "City_Name";
            ddlRecLoc.DataValueField = "City_Idno";
            ddlRecLoc.DataBind();
            ddlRecLoc.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        private void BindItemType()
        {
            StockTransferDAL obj = new StockTransferDAL();
            var itemname = obj.BindItemName();
            drpItemName.DataSource = itemname;
            drpItemName.DataTextField = "Item_Name";
            drpItemName.DataValueField = "Item_Idno";
            drpItemName.DataBind();
            drpItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        public void BindItem()
        {
            //StockTransferDAL objStck = new StockTransferDAL();
            //var Serial = objStck.BindSerialNo(Convert.ToInt64(drpItemName.SelectedValue));
            //objStck = null;
            //drpSerialNo.DataSource = Serial;
            //drpSerialNo.DataTextField = "SerialNo";
            //drpSerialNo.DataValueField = "SerlDetl_id";
            //drpSerialNo.DataBind();
            //drpSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex != 0)
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = hidmaxdate.Value;
            }
            else
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }

        #endregion

        #region Bind Event...

        private void BindGrid()
        {
            try
            {
                DateTime? dtDateFrom = null;
                DateTime? dtDateTo = null;
                if (string.IsNullOrEmpty(txtDateFrom.Text.Trim()) == false)
                {
                    dtDateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim()));
                }
                if (string.IsNullOrEmpty(txtDateTo.Text.Trim()) == false)
                {
                    dtDateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
                }
                StockTransferDAL objStck = new StockTransferDAL();
                DataTable DsGrdetail = new DataTable();

                var detail = objStck.SelectStckTransferReport(dtDateFrom, dtDateTo, Convert.ToInt32(ddlIssLoc.SelectedValue), Convert.ToInt32(ddlRecLoc.SelectedValue), Convert.ToInt32(drpItemName.SelectedValue), string.IsNullOrEmpty(drpSerialNo.SelectedValue) ? 0 : Convert.ToInt32(drpSerialNo.SelectedValue));

                if (detail != null && detail.Count > 0)
                {

                    grdMain.DataSource = detail;
                    grdMain.DataBind();
                
               DataTable dttemp1 = ApplicationFunction.CreateTable("tbl",
                                   "SrNo", "String",
                                   "IssueNo", "String",
                                   "IssueDate", "String",
                                   "IssLoc", "String",
                                   "RecLoc", "String",
                                   "ItemName", "String",
                                   "ItemType", "String",
                                   "SerialNo", "String",
                                   "Qty", "String",
                                   "Rate", "String",
                                   "Amount", "String"
                                   );
                    for (int i = 0; i < detail.Count; i++)
                    {
                        DataRow dr = dttemp1.NewRow();
                        dr["SrNo"] = Convert.ToString(i + 1);
                        dr["IssueNo"] = Convert.ToString(DataBinder.Eval(detail[i], "StckTrans_No"));
                        dr["IssueDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(detail[i], "StckTrans_Date")).ToString("dd-MM-yyyy"));
                        dr["IssLoc"] = Convert.ToString(DataBinder.Eval(detail[i], "IssLoc"));
                        dr["RecLoc"] = Convert.ToString(DataBinder.Eval(detail[i], "RecLoc"));
                        dr["ItemName"] = Convert.ToString(DataBinder.Eval(detail[i], "ItemName"));
                        dr["ItemType"] = Convert.ToString(DataBinder.Eval(detail[i], "ItemType"));
                        dr["SerialNo"] = Convert.ToString(DataBinder.Eval(detail[i], "SerialNo"));
                        dr["Qty"] = Convert.ToString(DataBinder.Eval(detail[i], "Qty"));
                        dr["Rate"] = Convert.ToString(DataBinder.Eval(detail[i], "Rate"));
                        dr["Amount"] = Convert.ToString(DataBinder.Eval(detail[i], "Tot_Amnt"));
                        dttemp1.Rows.Add(dr);
                    }
                    ViewState["dtCSV"] = dttemp1;
                    

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + detail.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    imgBtnExcel.Visible = true;
                    lblTotalRecord.Text = "T. Record(s) :" + Convert.ToString(detail.Count);

                }
                else
                {
                    grdMain.DataSource = null; lblcontant.Visible = false;
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): 0 ";
                    imgBtnExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }

        #endregion

        private DataTable CreateDt()
        {
            DataTable DtTemp = ApplicationFunction.CreateTable("tbl", "Lorry_No", "String", "LicDate", "String", "RC", "String", "FitDate", "String", "Insurance", "String");
            return DtTemp;
        }
        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            //CSVTable = (DataTable)ViewState["CSV"];
            //CSVTable.Columns.Remove("LorryIdno");
            //CSVTable.Columns["LorryType"].SetOrdinal(0);
            //CSVTable.Columns["PartyName"].SetOrdinal(1);
            CSVTable = (DataTable)ViewState["dtCSV"];

            ExportDataTableToCSV(CSVTable, "StockTransferReport");
        }
      
        #endregion

        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }

        protected void drpItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            StockTransferDAL objStck = new StockTransferDAL();
            Int64 ItemType = objStck.SelectItemType(Convert.ToInt64(drpItemName.SelectedValue));
            if (ItemType == 1)
            {
                drpSerialNo.Enabled = true;
                var itemname = objStck.BindSerialNo(1);
                drpSerialNo.DataSource = itemname;
                drpSerialNo.DataTextField = "SerialNo";
                drpSerialNo.DataValueField = "SerlDetl_id";
                drpSerialNo.DataBind();
                drpSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
            else
            {
                drpSerialNo.Enabled = false; drpSerialNo.SelectedValue = "0";
            }
            
        }
    }
}

