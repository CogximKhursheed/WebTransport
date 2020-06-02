using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;


namespace WebTransport
{
    public partial class ManageOpenStock : Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                BindCity();
                BindDateRange();
                RecordCount();
            }
        }

        #endregion

        #region Functions...
      
        private void BindGrid()
        {
            OpenTyreDAL objDrivMast = new OpenTyreDAL();
            var lstGridData = objDrivMast.SelectForSearch(Convert.ToInt32(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? "0" : ddlDateRange.SelectedValue), Convert.ToInt32(string.IsNullOrEmpty(ddlLocation.SelectedValue) ? "0" : ddlLocation.SelectedValue));
            objDrivMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;

                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void RecordCount()
        {
            OpenTyreDAL objTyreMast = new OpenTyreDAL();
            var Rec = objTyreMast.SelectForSearch(Convert.ToInt32(ddlDateRange.SelectedValue),0);
            objTyreMast = null;
            if (Rec.Count > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + Rec.Count;
                imgBtnExcel.Visible = false;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindCity()
        {
            OpenTyreDAL objMastDAL = new OpenTyreDAL();
            var lst = objMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("< Choose City >", "0"));


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

        private DataTable CreateDt()
        {
            DataTable DtTemp = ApplicationFunction.CreateTable("tbl","SerialNo","String","CompanyName","String", "LoctionName", "String", "ItemName", "String", "Qty", "String", "Rate", "String", "Amount", "String");
            return DtTemp;
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
            if (e.CommandName == "GetExcel")
            {
                DataTable DtCsv = CreateDt();
                Int32 RowId = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurRow = (GridViewRow)grdMain.Rows[RowId];
                HiddenField hidLocId = (HiddenField)CurRow.FindControl("hidLocId");
                HiddenField hidYearId = (HiddenField)CurRow.FindControl("hidYearId");
                OpenTyreDAL objItemMastDAL = new OpenTyreDAL();
                var lst = objItemMastDAL.GetExcel(Convert.ToInt64(hidLocId.Value), Convert.ToInt64(hidYearId.Value));

                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        ApplicationFunction.DatatableAddRow(DtCsv, Convert.ToString(DataBinder.Eval(lst[i], "SerialNo")), Convert.ToString(DataBinder.Eval(lst[i], "CompanyName")), Convert.ToString(DataBinder.Eval(lst[i], "LoctionName")), Convert.ToString(DataBinder.Eval(lst[i], "ItemName")),
                                                            "", Convert.ToString(DataBinder.Eval(lst[i], "Rate")), "");
                    }
                    if ((DtCsv != null) && (DtCsv.Rows.Count != 0))
                    {
                        DtCsv.Columns["LoctionName"].Caption = "Location";
                        DtCsv.Columns["ItemName"].ColumnName = "TyreName";
                        DtCsv.Columns["Rate"].Caption = "Rate";

                        DtCsv.Columns["SerialNo"].SetOrdinal(0);
                        DtCsv.Columns["CompanyName"].SetOrdinal(1);
                        DtCsv.Columns["LoctionName"].SetOrdinal(2);
                        DtCsv.Columns["TyreName"].SetOrdinal(3);
                        DtCsv.Columns["Rate"].SetOrdinal(4);

                        DtCsv.Columns.Remove("Qty");
                        DtCsv.Columns.Remove("Amount");

                        DtCsv.AcceptChanges();
                        ExportDataTableToCSV(DtCsv, Convert.ToString("OpeningItemDetails"));
                        Response.Redirect("ManageOpenStock.aspx");
                    }
                }
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {
                    imgBtnStatus.Visible = true;
                    imgBtnEdit.Visible = false;
                    grdMain.Columns[3].Visible = false;
                }
                if (base.Delete == false)
                {
                    imgBtnDelete.Visible = false;
                }
                if ((base.Edit == false) && (base.Delete == false))
                {
                    grdMain.Columns[4].Visible = false;
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=CompletOpeningReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.AllowPaging = false;
            grdMain.Columns[4].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void PrepareGridViewForExport(Control gv)
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

    }
}