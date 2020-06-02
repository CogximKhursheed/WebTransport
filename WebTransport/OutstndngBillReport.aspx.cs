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
    public partial class OutstndngBillReport : Pagebase
    {
        #region Variable ...

        string conString = "";
        static FinYear UFinYear = new FinYear();
        outsndngBillRepDAL objInvcDAL = new outsndngBillRepDAL();
        private int intFormId = 40;
        double dGrossAmount = 0, dRecvdAmnt = 0, dPendngAmnt = 0, dTrServTaxAmount = 0, dConsignrServTaxAmount = 0, dNetAmount = 0, dTDSAmnt = 0, dDBNote = 0;
        DataTable CSVTable = new DataTable();
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
                this.BindSenderName();
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
            drpSenderName.SelectedIndex = 0;
            txtInvoiceNo.Text = "";
            txtPrefixNo.Text = "";
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

        private void BindSenderName()
        {
            outsndngBillRepDAL obj = new outsndngBillRepDAL();
            var SenderName = obj.BindSender();
            drpSenderName.DataSource = SenderName;
            drpSenderName.DataTextField = "Acnt_Name";
            drpSenderName.DataValueField = "Acnt_Idno";
            drpSenderName.DataBind();
            objInvcDAL = null;
            drpSenderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                outsndngBillRepDAL obj = new outsndngBillRepDAL();
                DataTable list1 = obj.SelectRep("SelectRep", Convert.ToString(txtDateFrom.Text), Convert.ToString(txtDateTo.Text), 0, 0, 0, UserIdno, ApplicationFunction.ConnectionString(), Convert.ToString(txtPrefixNo.Text.Trim()));
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }
        private void BindGrid()
        {
            try
            {
                outsndngBillRepDAL obj = new outsndngBillRepDAL();

                string userclass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (userclass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }

                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iSenderIDNO = (Convert.ToString(drpSenderName.SelectedValue) == "" ? 0 : Convert.ToInt64(drpSenderName.SelectedValue));
                Int32 iInvoiceNo = (Convert.ToString(txtInvoiceNo.Text) == "" ? 0 : Convert.ToInt32(txtInvoiceNo.Text));
                DataTable DsGrdetail = obj.SelectRep("SelectRep", Convert.ToString(txtDateFrom.Text), Convert.ToString(txtDateTo.Text), iFromCityIDNO, iSenderIDNO, iInvoiceNo, UserIdno, ApplicationFunction.ConnectionString(), Convert.ToString(txtPrefixNo.Text.Trim()));
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    ViewState["dtCSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();

                    Double TotalNetAmount = 0, RecivdAmnt = 0, PendingAmnt = 0, TDSAmnt = 0, DBNoteAmnt = 0;

                    for (int i = 0; i < DsGrdetail.Rows.Count; i++)
                    {
                        RecivdAmnt += Convert.ToDouble(DsGrdetail.Rows[i]["Tot_Recvd"]);
                        PendingAmnt += Convert.ToDouble(DsGrdetail.Rows[i]["cur_Bal"]);
                        TDSAmnt += Convert.ToDouble(DsGrdetail.Rows[i]["TDS_Amount"]);
                        DBNoteAmnt += Convert.ToDouble(DsGrdetail.Rows[i]["DB_Note"]);
                        TotalNetAmount += Convert.ToDouble(DsGrdetail.Rows[i]["Amount"]);
                    }

                    lblNetTotalRecivdAmnt.Text = RecivdAmnt.ToString("N2");
                    lblNetTotallblPendingAmnt.Text = PendingAmnt.ToString("N2");
                    lblNetTotalTDSAmnt.Text = TDSAmnt.ToString("N2");
                    lblNetTotalDBNoteAmnt.Text = DBNoteAmnt.ToString("N2");
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    imgBtnExcel.Visible = true;
                    lblTotalRecord.Text = "T. Record(s) : " + Convert.ToString(DsGrdetail.Rows.Count);
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
                dGrossAmount = dGrossAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dRecvdAmnt = dRecvdAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Recvd"));
                dPendngAmnt = dPendngAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "cur_Bal"));
                dTDSAmnt = dTDSAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TDS_Amount"));
                dDBNote = dDBNote + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DB_Note"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dGrossAmount.ToString("N2");
                Label lblRecivdAmnt = (Label)e.Row.FindControl("lblRecivdAmnt");
                lblRecivdAmnt.Text = dRecvdAmnt.ToString("N2");
                Label lblPendingAmnt = (Label)e.Row.FindControl("lblPendingAmnt");
                lblPendingAmnt.Text = dPendngAmnt.ToString("N2");
                Label lblTDSAmnt = (Label)e.Row.FindControl("lblTDSAmnt");
                lblTDSAmnt.Text = dTDSAmnt.ToString("N2");
                Label lblDBNoteAmnt = (Label)e.Row.FindControl("lblDBNoteAmnt");
                lblDBNoteAmnt.Text = dDBNote.ToString("N2");
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
            CSVTable = (DataTable)ViewState["dtCSV"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {


                CSVTable.Columns.Remove("Inv_Idno");
                //CSVTable.Columns["City_Name"].SetOrdinal(2);
                //CSVTable.Columns["SenderName"].SetOrdinal(3);
                //CSVTable.Columns["Receiver"].SetOrdinal(4);
                //CSVTable.Columns["Gross_Amnt"].SetOrdinal(5);
                //CSVTable.Columns["Bilty_Amnt"].SetOrdinal(6);
                //CSVTable.Columns["Short_Amnt"].SetOrdinal(7);
                //CSVTable.Columns["TrServTax_Amnt"].SetOrdinal(8);
                //CSVTable.Columns["ConsignrServTax"].SetOrdinal(9);
                //  CSVTable.Columns["Net_Amnt"].SetOrdinal(10);
                CSVTable.Columns["Inv_No"].ColumnName = "Invoice No";
                CSVTable.Columns["Inv_Date"].ColumnName = "Invoice Date";
                CSVTable.Columns["Sendr_Name"].ColumnName = "Sender Name";
                CSVTable.Columns["From_City"].ColumnName = "From City";
                CSVTable.Columns["Amount"].ColumnName = "Invoice Amount";
                CSVTable.Columns["Tot_Recvd"].ColumnName = "Received Amount";
                CSVTable.Columns["cur_Bal"].ColumnName = "Pending Amount";
                ExportDataTableToCSV(CSVTable, "OutstndngBillReport" + txtDateFrom.Text + "_TO_" + txtDateTo.Text);
                Response.Redirect("OutstndngBillReport.aspx");
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

