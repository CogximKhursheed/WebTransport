using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Data;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;

namespace WebTransport
{
    public partial class MatIssueReport : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dGrossAmnt = 0, dNetAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.Print == false)
                //{
                //    imgBtnExcel.Visible = false;
                //}
                //this.BindState();
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                this.BindLocation();
                this.BindTruckNo();
                this.BindDriverName();
                this.BindItemName();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
                this.countall();
                SetDate();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            MaterialDAL obj = new MaterialDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 location = Convert.ToInt32(ddlLocation.SelectedValue);
            Int64 truckno = Convert.ToInt32(ddlTruckNo.SelectedValue);
            Int64 ItemIdno = Convert.ToInt64(ddlItemName.SelectedValue);
            Int64 DriverIdno = Convert.ToInt64(ddlDriver.SelectedValue);

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));


            if (ddlReportType.SelectedValue == "1")
            {
                var lstGridData = obj.SelectMatrialIssueReportSummary(dtfrom, dtto, location, truckno, yearidno, DriverIdno, ItemIdno);
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    grdMain.DataSource = lstGridData;
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                    imgBtnExcel.Visible = true;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SrNo", typeof(string));
                    dt.Columns.Add("MatIss_No", typeof(string));
                    dt.Columns.Add("MatIss_Date", typeof(string));
                    dt.Columns.Add("CityTo", typeof(string));
                    dt.Columns.Add("Item_Name", typeof(string));
                    dt.Columns.Add("Item_Qty", typeof(string));
                    dt.Columns.Add("Acnt_Name", typeof(string));
                    dt.Columns.Add("Lorry_No", typeof(string));
                    dt.Columns.Add("Item_Amnt", typeof(string));
                    dt.Columns.Add("Align", typeof(string));
                    dt.Columns.Add("Prev_AlignDate", typeof(string));
                    dt.Columns.Add("AlignDate", typeof(string));

                    for (int i = 0; i < lstGridData.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SrNo"] = Convert.ToString(i + 1);
                        dr["MatIss_No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "MatIss_No"));
                        dr["MatIss_Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "MatIss_Date")).ToString("dd-MM-yyyy");
                        dr["CityTo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                        dr["Item_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Item_Name"));
                        dr["Item_Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Qty"));
                        dr["Acnt_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                        dr["Lorry_No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                        dr["Item_Amnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Item_Amnt"));
                        dr["Align"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Align"));
                        dr["Prev_AlignDate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Prev_AlignDate"));
                        dr["AlignDate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AlignDate"));
                        dt.Rows.Add(dr);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ViewState["Dt"] = dt;
                    }

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
            else
            {
                var lstGridData = obj.SelectMatrialIssueReport(dtfrom, dtto, location, truckno, yearidno, DriverIdno, ItemIdno);
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SrNo", typeof(string));
                    dt.Columns.Add("MatIss_No", typeof(string));
                    dt.Columns.Add("MatIss_Date", typeof(string));
                    dt.Columns.Add("CityTo", typeof(string));
                    dt.Columns.Add("Item_Name", typeof(string));
                    dt.Columns.Add("Item_Qty", typeof(string));
                    dt.Columns.Add("Acnt_Name", typeof(string));
                    dt.Columns.Add("Lorry_No", typeof(string));
                    dt.Columns.Add("Item_Amnt", typeof(string));
                    dt.Columns.Add("Align", typeof(string));
                    dt.Columns.Add("Prev_AlignDate", typeof(string));
                    dt.Columns.Add("AlignDate", typeof(string));

                    for (int i = 0; i < lstGridData.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SrNo"] = Convert.ToString(i + 1);
                        dr["MatIss_No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "MatIss_No"));
                        dr["MatIss_Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "MatIss_Date")).ToString("dd-MM-yyyy");
                        dr["CityTo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                        dr["Item_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Item_Name"));
                        dr["Item_Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Qty"));
                        dr["Acnt_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                        dr["Lorry_No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                        dr["Item_Amnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Item_Amnt"));
                        dr["Align"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Align"));
                        dr["Prev_AlignDate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Prev_AlignDate"));
                        dr["AlignDate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AlignDate"));
                        dt.Rows.Add(dr);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ViewState["Dt"] = dt;
                    }

                    Double TotalNetAmount = 0;

                    for (int i = 0; i < lstGridData.Count; i++)
                    {
                        TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Item_Amnt"));
                    }
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
            obj = null;
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

        private void BindLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var to = obj.BindLocFrom();
            ddlLocation.DataSource = to;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindTruckNo()
        {
            MaterialDAL obj = new MaterialDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = hidmaxdate.Value;
                }
            }
        }

        private void BindDriverName()
        {
            MaterialDAL objm = new MaterialDAL();
            var EMPS = objm.SelectEmployee();

            ddlDriver.DataSource = EMPS;
            ddlDriver.DataTextField = "Acnt_Name";
            ddlDriver.DataValueField = "Acnt_Idno";
            ddlDriver.DataBind();
            ddlDriver.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            objm = null;
        }

        public void countall()
        {
            MaterialDAL obj = new MaterialDAL();
            Int64 count = 0;
            count = obj.CountAll();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "Total Record (s): 0 ";
            }
        }

        private void BindItemName()
        {
            MaterialDAL objTruck = new MaterialDAL();
            var itemname = objTruck.BindItemName();

            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                if (ddlReportType.SelectedValue == "1")
                {
                    e.Row.Cells[8].Visible = false; // hides the first column
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Visible = false; 
                }
            }
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {

                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                if (ddlReportType.SelectedValue == "1")
                {
                    e.Row.Cells[8].Visible = false; // hides the first column
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Visible = false;
                }

            }
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Footer)
            {
                if (ddlReportType.SelectedValue == "1")
                {
                    e.Row.Cells[8].Visible = false; // hides the first column
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Visible = false;
                }
            }
        }

        Double ItemAmntTotl = 0;
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Double ItemAmnt = 0;
                ItemAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Item_Amnt"));

                ItemAmntTotl += ItemAmnt;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetamnt = (Label)e.Row.FindControl("lblNetamnt");
                lblNetamnt.Text = ItemAmntTotl.ToString("N2");

            }
        }
        #endregion

        #region Button Events...


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion

        #region control events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        #endregion

        #region print...

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MatIssueReport.xls"));
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


        #endregion

        #region Control Events And button events
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void LnkExcel_Click(object sender, ImageClickEventArgs e)
        {
        //    if (ViewState["Dt"] != null)
        //    {
        //        DataTable dt = new DataTable();
        //        dt = (DataTable)ViewState["Dt"];
        //        Export(dt);
        //    }
            DataTable CSVTable = (DataTable)ViewState["Dt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                CSVTable.Columns["MatIss_No"].ColumnName = "MatIss No.";
                CSVTable.Columns["MatIss_Date"].ColumnName = "MatIss Date";
                CSVTable.Columns["Item_Name"].ColumnName = "Item Name";
                CSVTable.Columns["Acnt_Name"].ColumnName = "A/c Name";
                CSVTable.Columns["Lorry_No"].ColumnName = "Lorry No";
                CSVTable.Columns["Item_Amnt"].ColumnName = "Item Amnt";
                CSVTable.Columns["Item_Qty"].ColumnName = "Item Qty";
                CSVTable.Columns["Prev_AlignDate"].ColumnName = "Prev AlignDate";
                ExportDataTableToCSV(CSVTable, "MatIssReport");
                Response.Redirect("MatIssueReport.aspx");
            }
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

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "1")
            {
                ddlItemName.Enabled = false;
                ddlItemName.SelectedValue = "0";
            }
            else
            {
                ddlItemName.Enabled = true;
            }
            ddlReportType.Focus();
        }

        #endregion
    }
}