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
    public partial class LorrySummaryReport : Pagebase
    {
        #region Variable ...
        string InfoMessage = String.Empty;
        Int32 iLorry_Id = 0;
        Int32 iYear_Id = 0;
        string strDateFrom = String.Empty;
        string strDateTo = String.Empty;
        string conString = "";
        static FinYear UFinYear = new FinYear();
        LorryMasterRepDAL objLorryDAL = new LorryMasterRepDAL();
        private int intFormId = 78;
        DataTable CSVTable = new DataTable();
        //  double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0,dConsignrServTaxAmount=0, dNetAmount = 0;
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
                    //Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }
                BindLorry();
                BindDateRange();
                SetDate();
                BindGrid();
            }
        }
        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLorryNo.Text = "";
        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region Bind Event...
        public void BindLorry()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lstLorry = (from lm in db.LorryMasts orderby lm.Lorry_No ascending select lm).ToList();
                if (lstLorry != null)
                {
                    rptLorryList.DataSource = lstLorry;
                    rptLorryList.DataBind();
                }
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
        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
               
            }
        }

        private void BindGrid()
        {
            bool error = false;
            iLorry_Id = Convert.ToInt32((hidLorryIdno.Value == null || hidLorryIdno.Value == "") ? "0" : hidLorryIdno.Value);
            iYear_Id = Convert.ToInt32(ddlDateRange.SelectedValue == null ? "0" : ddlDateRange.SelectedValue);
            strDateFrom = txtDatefrom.Text;
            strDateTo = txtDateto.Text;
            //Validate Form Fields
            if (iYear_Id == 0)
            { InfoMessage = "Year cannot be null."; ddlDateRange.Focus(); error = true; return; }
            else if (strDateFrom == "")
            { InfoMessage = "From date cannot be blank."; txtDatefrom.Focus(); error = true; return; }
            else if (strDateTo == "")
            { InfoMessage = "To date cannot be blank."; txtDateto.Focus(); error = true; return; }
            else if (iLorry_Id == 0)
            { InfoMessage = "Please choose a lorry number."; txtLorryNo.Focus(); error = true; return; }
            if (error)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "PassMessageError('" + Convert.ToString(InfoMessage) + "')", true);
            }
            //////////////////////
            else
            {
                try
                {
                    LorryMasterRepDAL obj = new LorryMasterRepDAL();
                    string iLorryNo = (Convert.ToString(txtLorryNo.Text) == "" ? "" : Convert.ToString(txtLorryNo.Text));
                    DataTable DsGrdetail = obj.SelectLorrySummary(iLorry_Id, iYear_Id, strDateFrom, strDateTo, conString);
                    lblOwnerName.Text = DsGrdetail.Rows[0]["Owner Name"].ToString();
                    lblDriverName.Text = DsGrdetail.Rows[0]["Driver Name"].ToString();
                    //DataTable DsGrdetail = obj.SelectForSearch('', conString);
                    if (DsGrdetail != null && DsGrdetail.Rows.Count > 0)
                    {
                        ViewState["CSV"] = DsGrdetail;
                        grdReport.DataSource = DsGrdetail;
                        grdReport.DataBind();

                        lblTotalRecord.Text = DsGrdetail.Rows.Count.ToString();
                        imgBtnExcel.Visible = true;

                        int startRowOnPage = (grdReport.PageIndex * grdReport.PageSize) + 1;
                        int lastRowOnPage = startRowOnPage + grdReport.Rows.Count - 1;
                        lblcontant.Text = startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                        lblcontant.Visible = true;
                        divpaging.Visible = true;
                    }
                    else
                    {
                        lblcontant.Visible = false;
                        divpaging.Visible = false;

                        grdReport.DataSource = null;
                        grdReport.DataBind();
                        lblTotalRecord.Text = "0";
                        imgBtnExcel.Visible = false;
                    }

                }
                catch (Exception Ex)
                {
                    throw (Ex);
                }
            }
        }
        #endregion

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
            grdReport.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Functions
        private void SetDate()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            txtDatefrom.Text = Convert.ToString(list.GetValue(0));
            txtDateto.Text = Convert.ToString(list.GetValue(1));
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
            //grdMain.GridLines = GridLines.Both;
            //PrepareGridViewForExport(grdMain);
            //ExportGridView();
            									
            CSVTable = (DataTable)ViewState["CSV"];
            CSVTable.Columns["Sr No"].SetOrdinal(0);
            CSVTable.Columns["Date"].SetOrdinal(1);
            CSVTable.Columns["GR No"].SetOrdinal(2);
            CSVTable.Columns["Lorry No"].SetOrdinal(3);
            CSVTable.Columns["Destination"].SetOrdinal(4);
            CSVTable.Columns["Qty"].SetOrdinal(5);
            CSVTable.Columns["Rate"].SetOrdinal(6);
            CSVTable.Columns["TDS Amnt"].SetOrdinal(7);
            CSVTable.Columns["Advance"].SetOrdinal(8);
            CSVTable.Columns["Diesel Amnt"].SetOrdinal(9);
            CSVTable.Columns["Commission Amnt"].SetOrdinal(10);
            CSVTable.Columns["Parking Chrg"].SetOrdinal(11);
            CSVTable.Columns["Owner Name"].SetOrdinal(12);
            CSVTable.Columns["Driver Name"].SetOrdinal(13);
            ExportDataTableToCSV(CSVTable, "LorryMasterReport");
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
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
        private void ExportGridView()
        {
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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

