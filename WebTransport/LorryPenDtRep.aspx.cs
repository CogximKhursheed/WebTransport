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
    public partial class LorryPenDtRep : Pagebase
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
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }
                this.BindDateRange();
                SetDate();
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
                TruckwiseRepDAL obj = new TruckwiseRepDAL();
                DataTable DsGrdetail = new DataTable();
                var detail = obj.SearchLorry(txtLorryNo.Text, dtDateFrom, dtDateTo, Convert.ToInt32(ddllorryType.SelectedValue), Convert.ToInt32(ddlReptype.SelectedValue));

                if (detail != null && detail.Count > 0)
                {

                    DsGrdetail = CreateDt();
                    ViewState["dt"] = DsGrdetail;

                    if (ViewState["dt"] != null)
                    {
                        for (int i = 0; i < detail.Count; i++)
                        {
                            ApplicationFunction.DatatableAddRow(DsGrdetail,
                                Convert.ToString(DataBinder.Eval(detail[i], "Lorry_No")),
                                Convert.ToDateTime(DataBinder.Eval(detail[i], "LicDate")).ToString("dd-MM-yyyy"),
                                Convert.ToDateTime(DataBinder.Eval(detail[i], "RC")).ToString("dd-MM-yyyy"),
                                Convert.ToDateTime(DataBinder.Eval(detail[i], "FitDate")).ToString("dd-MM-yyyy"),
                                Convert.ToDateTime(DataBinder.Eval(detail[i], "Insurance")).ToString("dd-MM-yyyy")
                            );
                        }
                    }
                    if (detail != null && detail.Count > 0)
                    {
                        ViewState["dt"] = DsGrdetail;
                        rows = detail.Count;
                        grdMain.DataSource = detail;
                        grdMain.DataBind();
                        imgBtnExcel.Visible = true;
                        lblTotalRecord.Text = string.Empty;
                        if (Convert.ToInt32(ddlReptype.SelectedValue) == 1)
                        {
                            grdMain.Columns[0].Visible = true;
                            grdMain.Columns[1].Visible = true;
                            grdMain.Columns[2].Visible = true;
                            grdMain.Columns[3].Visible = true;
                            grdMain.Columns[4].Visible = true;
                            grdMain.Columns[5].Visible = true;
                        }
                        else if (Convert.ToInt32(ddlReptype.SelectedValue) == 2)
                        {
                            grdMain.Columns[0].Visible = true;
                            grdMain.Columns[1].Visible = true;
                            grdMain.Columns[2].Visible = true;
                            grdMain.Columns[3].Visible = false;
                            grdMain.Columns[4].Visible = false;
                            grdMain.Columns[5].Visible = false;
                        }
                        else if (Convert.ToInt32(ddlReptype.SelectedValue) == 3)
                        {
                            grdMain.Columns[0].Visible = true;
                            grdMain.Columns[1].Visible = true;
                            grdMain.Columns[2].Visible = false;
                            grdMain.Columns[3].Visible = true;
                            grdMain.Columns[4].Visible = false;
                            grdMain.Columns[5].Visible = false;
                        }
                        else if (Convert.ToInt32(ddlReptype.SelectedValue) == 4)
                        {
                            grdMain.Columns[0].Visible = true;
                            grdMain.Columns[1].Visible = true;
                            grdMain.Columns[2].Visible = false;
                            grdMain.Columns[3].Visible = false;
                            grdMain.Columns[4].Visible = true;
                            grdMain.Columns[5].Visible = false;
                        }
                        else if (Convert.ToInt32(ddlReptype.SelectedValue) == 5)
                        {
                            grdMain.Columns[0].Visible = true;
                            grdMain.Columns[1].Visible = true;
                            grdMain.Columns[2].Visible = false;
                            grdMain.Columns[3].Visible = false;
                            grdMain.Columns[4].Visible = false;
                            grdMain.Columns[5].Visible = true;
                        }
                        if (detail.Count <= 0)
                        {
                            lblTotalRecord.Text = "T. Record(s): 0";
                            imgBtnExcel.Visible = false;
                        }
                        else
                        {
                            lblTotalRecord.Text = "T. Record(s): " + rows;
                            imgBtnExcel.Visible = true;
                        }
                    }
                    else
                    {
                        lblTotalRecord.Text = "T. Record(s): 0";
                        grdMain.DataSource = null;
                        grdMain.DataBind();
                        imgBtnExcel.Visible = false;
                    }
                }
                else
                {
                    lblTotalRecord.Text = "T. Record(s): 0";
                    grdMain.DataSource = null;
                    grdMain.DataBind();
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
            CSVTable = (DataTable)ViewState["dt"];

            ExportDataTableToCSV(CSVTable, "TruckWiseReport");
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
    }
}

