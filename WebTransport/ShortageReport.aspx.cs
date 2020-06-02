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
using System.Drawing;

namespace WebTransport
{
    public partial class ShortageReport : Pagebase
    {
        #region Variable ...
        static FinYear UFinYear = new FinYear();
        ShortageRepDAL objInvcDAL = new ShortageRepDAL();
        private int intFormId = 42;
        double dWeight = 0, dAmount = 0, dShortage = 0, dShortageAmount = 0, dInvNetAmount = 0;
        DataTable CsvTable = new DataTable();
        #endregion

        #region Page Load Event ...

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            // conString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
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
                this.BindPartyName();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }

                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                TotalRecords();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }

        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            drpPartyName.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            drpBaseCity.Focus();
        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...

        private void BindPartyName()
        {
            //ShortageRepDAL obj = new ShortageRepDAL();
            BindDropdownDAL obj = new BindDropdownDAL();
            var PartyName = obj.BindSender();
            drpPartyName.DataSource = PartyName;
            drpPartyName.DataTextField = "Acnt_Name";
            drpPartyName.DataValueField = "Acnt_Idno";
            drpPartyName.DataBind();
            objInvcDAL = null;
            drpPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {

            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserId)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserId);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                ShortageRepDAL obj = new ShortageRepDAL();
                DataTable list1 = obj.SelectRep("SelectRepWithoutParty", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), 0, 0, UserIdno, ApplicationFunction.ConnectionString());
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }

        private void BindGrid()
        {
            try
            {
                ShortageRepDAL obj = new ShortageRepDAL();
                string userclass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (userclass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iSenderIDNO = (Convert.ToString(drpPartyName.SelectedValue) == "" ? 0 : Convert.ToInt64(drpPartyName.SelectedValue));
                DataTable DsGrdetail = null;
                if (iSenderIDNO == 0)
                {
                    DsGrdetail = obj.SelectRep("SelectRepWithoutParty", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, iSenderIDNO, UserIdno, ApplicationFunction.ConnectionString());

                }
                else
                {
                    DsGrdetail = obj.SelectRep("SelectRepWithParty", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, iSenderIDNO, UserIdno, ApplicationFunction.ConnectionString());
                }
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    ViewState["dtCSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();
                    Double TotalNetAmount = 0,TotWeight = 0, TotAmount = 0, TotShortageQty = 0, TotShortageAmnt=0;

                    for (int i = 0; i < DsGrdetail.Rows.Count; i++)
                    {
                        TotWeight += Convert.ToDouble(DsGrdetail.Rows[i]["TOT_WEGHT"]);
                        TotAmount += Convert.ToDouble(DsGrdetail.Rows[i]["AMOUNT"]);
                        TotShortageQty += Convert.ToDouble(DsGrdetail.Rows[i]["SHORTAGE_QTY"]);
                        TotShortageAmnt += Convert.ToDouble(DsGrdetail.Rows[i]["SHORTAGE_AMOUNT"]);
                        TotalNetAmount += Convert.ToDouble(DsGrdetail.Rows[i]["INV_NETAMNT"]);
                    }
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");
                    lblWeight.Text = TotWeight.ToString("N2");
                    lblAmount.Text = TotAmount.ToString("N2");
                    lblshrtgQty.Text = TotShortageQty.ToString("N2");
                    lblshrtgAmnt.Text = TotShortageAmnt.ToString("N2");

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    imgBtnExcel.Visible = true;
                    lblTotalRecord.Text = "T. Record(s) :" + Convert.ToString(DsGrdetail.Rows.Count);

                }
                else
                {
                    ViewState["dtCSV"] = null;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): 0 ";
                    lblcontant.Visible = false;
                    divpaging.Visible = false;
                    imgBtnExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
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
                dWeight = dWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TOT_WEGHT"));
                dAmount = dAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
                dShortage = dShortage + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SHORTAGE_QTY"));
                dShortageAmount = dShortageAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SHORTAGE_AMOUNT"));
                dInvNetAmount = dInvNetAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "INV_NETAMNT"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                lblWeight.Text = dWeight.ToString("N2");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dAmount.ToString("N2");
                Label lblshortage = (Label)e.Row.FindControl("lblshortage");
                lblshortage.Text = dShortage.ToString("N2");
                Label lblshortageAmount = (Label)e.Row.FindControl("lblshortageAmount");
                lblshortageAmount.Text = dShortageAmount.ToString("N2");
                Label lblInvNetAmount = (Label)e.Row.FindControl("lblInvNetAmount");
                lblInvNetAmount.Text = dInvNetAmount.ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
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
            CsvTable = (DataTable)ViewState["dtCSV"];
            if (CsvTable != null && CsvTable.Rows.Count > 0)
            {
                CsvTable.Columns.Remove("SENDER_NAME");
                CsvTable.Columns.Remove("RECIVR_NAME");
                CsvTable.Columns["INV_NO"].ColumnName = "Inv. No";
                CsvTable.Columns["INV_DATE"].ColumnName = "Inv. Date";
                CsvTable.Columns["GR_NO"].ColumnName = "GR. No";
                CsvTable.Columns["GR_DATE"].ColumnName = "GR Date";
                CsvTable.Columns["CHLN_NO"].ColumnName = "Challan No";
                CsvTable.Columns["LORRY_NO"].ColumnName = "Lorry No";
                CsvTable.Columns["FROM_CITY"].ColumnName = "From City";
                CsvTable.Columns["TO_CITY"].ColumnName = "To City";
                //CsvTable.Columns["SENDER_NAME"].ColumnName = "Sender";
                //CsvTable.Columns["RECIVR_NAME"].ColumnName = "Reciver";
                CsvTable.Columns["ITEM_NAME"].ColumnName = "Item Name";
                CsvTable.Columns["TOT_WEGHT"].ColumnName = "Total Weight";
                CsvTable.Columns["ITEM_RATE"].ColumnName = "Item Rate";
                CsvTable.Columns["SHORTAGE_QTY"].ColumnName = "Shortage Qty";
                CsvTable.Columns["SHORTAGE_AMOUNT"].ColumnName = "Shortage Amount";
                CsvTable.Columns["INV_NETAMNT"].ColumnName = "Net Amount";
                ExportDataTableToCSV(CsvTable, "ShortageReport");
            }
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
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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

        
    }
}
