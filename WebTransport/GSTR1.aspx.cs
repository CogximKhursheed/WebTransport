using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.IO;

namespace WebTransport
{
    public partial class GSTR1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDateRange();
                BindLocation();
                //BindMonth();
                BindGSTR1();
                lnkbtnExport2Excel.Visible = false;
            }
        }

        #region Bind
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }

        private void BindLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            ddlFromCity.DataBind();
        }

        //private void BindMonth()
        //{
        //    DateTime date = Convert.ToDateTime(ApplicationFunction.mmddyyyy("01-01-1900"));
        //    List<string> month = new List<string>();
        //    for (int i = 0; i < 12; i++)
        //    {
        //        month.Add(date.ToString("MMMM"));
        //        date = date.AddMonths(1);
        //    }
        //    ddlMonthRange.DataSource = month;
        //    ddlMonthRange.DataBind();
        //}
        #endregion

        #region Button Click
        public void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            BindGSTR1();
        }

        public void lnkbtnExport2Excel_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["GSTRData"] as DataTable;
            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                string attachment2 = "attachment; filename=GSTR1-Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment2);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dgGrid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        private void BindGSTR1()
        {
            String strConString = ApplicationFunction.ConnectionString();
            String strAction = "SelectGSTR1";
            Int32 iYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 iFromLoc = Convert.ToInt32(ddlFromCity.SelectedValue);
            Int32 iMonthIdno = Convert.ToInt32(ddlMonthRange.SelectedValue);
            GSTRegisterDAL objGSTR = new GSTRegisterDAL();
            DataTable dtGSTR = objGSTR.SelectGSTR1(strConString, strAction, iYearIdno, iFromLoc, iMonthIdno);
            Double dTaxableValue = 0;
            Double dIGSTAmt = 0;
            Double dSGSTAmt = 0;
            Double dCGSTAmt = 0;
            Double dUGSTAmt = 0;
            Double dGSTCessAmt = 0;
            if (dtGSTR != null && dtGSTR.Rows.Count > 0)
            {
                grdMain.DataSource = dtGSTR;
                grdMain.DataBind();
                //Calculate Sum and display in Footer Row
                foreach (GridViewRow rows in grdMain.Rows)
                {
                    //TAXABLE TOTAL
                    Label lblTaxable = (Label)rows.FindControl("lblTaxableValue");
                    dTaxableValue += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblTaxable.Text);
                    //GST AMOUNT TOTAL
                    Label lblIGST = (Label)rows.FindControl("lblIGSTValue");
                    dIGSTAmt += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblIGST.Text);
                    Label lblSGST = (Label)rows.FindControl("lblSGSTValue");
                    dSGSTAmt += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblSGST.Text);
                    Label lblCGST = (Label)rows.FindControl("lblCGSTValue");
                    dCGSTAmt += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblCGST.Text);
                    Label lblUGST = (Label)rows.FindControl("lblUGSTValue");
                    dUGSTAmt += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblUGST.Text);
                    Label lblGSTCess = (Label)rows.FindControl("lblGSTCessValue");
                    dGSTCessAmt += Convert.ToDouble(lblTaxable.Text == "" ? "0" : lblGSTCess.Text);
                }
                Label lblTaxableTotalValue = (Label)grdMain.FooterRow.FindControl("lblTaxableTotalValue");
                Label lblIGSTTotalValue = (Label)grdMain.FooterRow.FindControl("lblIGSTTotalValue");
                Label lblSGSTTotalValue = (Label)grdMain.FooterRow.FindControl("lblSGSTTotalValue");
                Label lblCGSTTotalValue = (Label)grdMain.FooterRow.FindControl("lblCGSTTotalValue");
                Label lblUGSTTotalValue = (Label)grdMain.FooterRow.FindControl("lblUGSTTotalValue");
                Label lblGSTCessTotalValue = (Label)grdMain.FooterRow.FindControl("lblGSTCessTotalValue");

                if (lblTaxableTotalValue != null) lblTaxableTotalValue.Text = dTaxableValue.ToString("N2");
                if (lblIGSTTotalValue != null) lblIGSTTotalValue.Text = dIGSTAmt.ToString("N2");
                if (lblSGSTTotalValue != null) lblSGSTTotalValue.Text = dSGSTAmt.ToString("N2");
                if (lblCGSTTotalValue != null) lblCGSTTotalValue.Text = dCGSTAmt.ToString("N2");
                if (lblUGSTTotalValue != null) lblUGSTTotalValue.Text = dUGSTAmt.ToString("N2");
                if (lblGSTCessTotalValue != null) lblGSTCessTotalValue.Text = dGSTCessAmt.ToString("N2");
                DataRow total = dtGSTR.NewRow();
                dtGSTR.Rows.InsertAt(total, dtGSTR.Rows.Count);
                dtGSTR.Rows.Add("", "", "", "", "", "", "", "", "Total", Convert.ToDouble(dTaxableValue), "", "", Convert.ToDouble(dIGSTAmt), Convert.ToDouble(dSGSTAmt), Convert.ToDouble(dCGSTAmt), Convert.ToDouble(dUGSTAmt), Convert.ToDouble(dGSTCessAmt), "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                ViewState["GSTRData"] = dtGSTR;
                lnkbtnExport2Excel.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lnkbtnExport2Excel.Visible = false;
            }
        }
        #endregion

        #region Grid Events
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGSTR1();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lblGrossAmount = (Label)e.Row.FindControl("lblGrossAmount");
                //lblGrossAmount.Text = dGrossAmount.ToString("N2");
                //Label lblBiltyAmount = (Label)e.Row.FindControl("lblBiltyAmount");
            }
        }
        #endregion
    }
}