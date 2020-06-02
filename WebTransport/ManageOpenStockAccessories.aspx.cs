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
    public partial class ManageOpenStockAccessories : Pagebase
    {
        #region Variables declaration...
        // private int intFormId = 19;
        DataTable dtTemp = new DataTable();
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
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    btnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}

                //txtTolTaxName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                this.BindDateRange();

                this.BindCity();

                //if (Request.QueryString["OpenIdno"] != null)
                //{
                //    this.Populate(Convert.ToInt32(Request.QueryString["OpenIdno"]));
                //    hidstckidno.Value = Convert.ToString(Request.QueryString["OpenIdno"]);
                //    imgBtnNew.Visible = true;
                //}
                //else
                //{
                //    imgBtnNew.Visible = false;
                //}
                RecordCount();
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Miscellaneous Events...
        private void BindCity()
        {
            OpenTyreDAL objTollMastDAL = new OpenTyreDAL();
            var lst = objTollMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("< Choose Location >", "0"));
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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
        private void BindGrid()
        {
            OpenAccDAL objDAL = new OpenAccDAL();
            var lstGridData = objDAL.SelectStckMastList(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt64(ddlLocation.SelectedIndex) == 0 ? 0 : Convert.ToInt32(ddlLocation.SelectedValue));
            objDAL = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;

                imgBtnExcel.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + "0";
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
        }
        private void RecordCount()
        {
            OpenAccDAL objDAL = new OpenAccDAL();
            Int64 lstGridData = objDAL.Count(Convert.ToInt64(ddlDateRange.SelectedValue));
            objDAL = null;
            if (lstGridData != null && lstGridData > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + lstGridData;
                imgBtnExcel.Visible = false;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private DataTable CreateDt()
        {
            DataTable DtTemp = ApplicationFunction.CreateTable("tbl", "LocName", "String", "ItemName", "String", "Qty", "String", "Rate", "String", "Amount", "String");
            return DtTemp;
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
        #endregion

        #region GridEvents...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
                OpenAccDAL objItemMastDAL = new OpenAccDAL();
                var lst = objItemMastDAL.GetExcel(Convert.ToInt64(hidLocId.Value), Convert.ToInt64(hidYearId.Value));

                if (lst != null && lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        ApplicationFunction.DatatableAddRow(DtCsv, Convert.ToString(DataBinder.Eval(lst[i], "LocName")), Convert.ToString(DataBinder.Eval(lst[i], "ItemName")),
                                                            Convert.ToString(DataBinder.Eval(lst[i], "Qty")), Convert.ToString(DataBinder.Eval(lst[i], "Rate")),
                                                            Convert.ToString(DataBinder.Eval(lst[i], "Amount")));
                    }
                    if ((DtCsv != null) && (DtCsv.Rows.Count != 0))
                    {
                        DtCsv.Columns["LocName"].Caption = "Location";
                        DtCsv.Columns["ItemName"].ColumnName = "TyreName";
                        DtCsv.Columns["Qty"].Caption = "Qty";
                        DtCsv.Columns["Rate"].Caption = "Rate";
                        DtCsv.Columns["Amount"].Caption = "Amount";
                        DtCsv.Columns["LocName"].SetOrdinal(0);
                        DtCsv.Columns["TyreName"].SetOrdinal(1);
                        DtCsv.Columns["Qty"].SetOrdinal(2);
                        DtCsv.Columns["Rate"].SetOrdinal(3);
                        DtCsv.Columns["Amount"].SetOrdinal(4);
                        DtCsv.AcceptChanges();
                        ExportDataTableToCSV(DtCsv, Convert.ToString("OpeningItemDetails"));
                        Response.Redirect("ManageOpenStockAccessories.aspx");
                    }
                }
            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=CompletStockAccessoriesReport.xls";
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