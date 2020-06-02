using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace WebTransport
{
    public partial class SaleReg : Pagebase
    {
        #region Private Variable....
        private double dGrossAmnt = 0, dNetAmnt = 0, dtaxable = 0, dtax = 0, dDiscAmnt = 0, dOtherAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!IsPostBack)
            {
                ddlDateRange.Focus();
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBillNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtPerfNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                this.BindDateRange(); this.BindCityFrom(); this.bindsender();
                this.ddlDateRange_SelectedIndexChanged(sender, e);

                TotalRecordCount();
            }
        }
        #endregion

        #region Functions...

        private void TotalRecordCount()
        {
            SaleRegDAL obj = new SaleRegDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            Int32 intYearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 intLoc =string.IsNullOrEmpty(Convert.ToString(drpCityFrom.SelectedValue))?0:Convert.ToInt32(drpCityFrom.SelectedValue);
            Int32 intSaletype =string.IsNullOrEmpty(Convert.ToString(ddlSaleType.SelectedValue))?0:Convert.ToInt32(ddlSaleType.SelectedValue);

            int intBillNo =string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text.Trim()))?0:Convert.ToInt32(txtBillNo.Text.Trim());
            Int32 intPartyIdno=string.IsNullOrEmpty(Convert.ToString(ddlPartyName.SelectedValue))?0:Convert.ToInt32(ddlPartyName.SelectedValue);
            string strPerfNo =string.IsNullOrEmpty(Convert.ToString(txtPerfNo.Text.Trim()))?"":Convert.ToString(txtPerfNo.Text.Trim());
            Int32 intAgainst=string.IsNullOrEmpty(Convert.ToString(ddlAgainst.SelectedValue))?0:Convert.ToInt32(ddlAgainst.SelectedValue);

            var lstGridData = obj.SelectSBillReg(intYearIdno, datefromValue, dateToValue, intLoc, intSaletype, strPerfNo, intBillNo, intPartyIdno, intAgainst);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0) { lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count.ToString(); } else { lblTotalRecord.Text = "T. Record (s): 0 "; }
        }

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SaleRegister.xls"));
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
        private void BindGrid()
        {

            SaleRegDAL obj = new SaleRegDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            Int32 intYearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 intLoc = string.IsNullOrEmpty(Convert.ToString(drpCityFrom.SelectedValue)) ? 0 : Convert.ToInt32(drpCityFrom.SelectedValue);
            Int32 intSaletype = string.IsNullOrEmpty(Convert.ToString(ddlSaleType.SelectedValue)) ? 0 : Convert.ToInt32(ddlSaleType.SelectedValue);

            int intBillNo = string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text.Trim())) ? 0 : Convert.ToInt32(txtBillNo.Text.Trim());
            Int32 intPartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlPartyName.SelectedValue)) ? 0 : Convert.ToInt32(ddlPartyName.SelectedValue);
            string strPerfNo = string.IsNullOrEmpty(Convert.ToString(txtPerfNo.Text.Trim())) ? "" : Convert.ToString(txtPerfNo.Text.Trim());
            Int32 intAgainst = string.IsNullOrEmpty(Convert.ToString(ddlAgainst.SelectedValue)) ? 0 : Convert.ToInt32(ddlAgainst.SelectedValue);

            var lstGridData = obj.SelectSBillReg(intYearIdno, datefromValue, dateToValue, intLoc, intSaletype, strPerfNo, intBillNo, intPartyIdno, intAgainst);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("PerfNo", typeof(string));
                dt.Columns.Add("BillNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("BillType", typeof(string));
                dt.Columns.Add("Against", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("TotTaxable", typeof(string));
                dt.Columns.Add("TotTax", typeof(string));
                dt.Columns.Add("DiscAmnt", typeof(string));
                dt.Columns.Add("OtherAmnt", typeof(string));
                dt.Columns.Add("NetAmnt", typeof(string));

                double TNet = 0; double TTax = 0; double TTaxable = 0; double TDisc = 0; double TOther = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["PerfNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PrefNo"));
                    dr["BillNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SbillNo"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Date")).ToString("dd-MM-yyyy");
                    dr["BillType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SbillType"));
                    dr["Against"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Against"));
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PartyName"));
                    dr["Location"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromLocation"));
                    dr["TotTaxable"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TotTaxableAmnt"));
                    dr["TotTax"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TotTax"));
                    dr["DiscAmnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DiscAmnt"));
                    dr["OtherAmnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "OtherAmnt"));
                    dr["NetAmnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "NetAmnt"));
                    dt.Rows.Add(dr);

                    TTaxable += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "TotTax"));
                    TTax += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "TotTax"));
                    TDisc += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "DiscAmnt"));
                    TOther += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "OtherAmnt"));
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "NetAmnt"));

                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Party"] = "Total";
                        drr["TotTaxable"] = (TTaxable).ToString("N2");
                        drr["TotTax"] = (TTax).ToString("N2");
                        drr["DiscAmnt"] = (TDisc).ToString("N2");
                        drr["OtherAmnt"] = (TOther).ToString("N2");
                        drr["NetAmnt"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;

                
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
            }
        }

        private void bindsender()
        {
            PurchaseBillDAL objBill = new PurchaseBillDAL();
            DataTable dt = objBill.BindSenderForPurchaseBill(ApplicationFunction.ConnectionString());
            ddlPartyName.DataSource = dt;
            ddlPartyName.DataTextField = "Acnt Name";
            ddlPartyName.DataValueField = "Acnt Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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

        private void BindCityFrom()
        {
            PurchaseBillDAL obj = new PurchaseBillDAL();
            var ToCity = obj.BindFromCity();
            obj = null;
            drpCityFrom.DataSource = ToCity;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindCityFrom(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    txtDateTo.Text = hidmaxdate.Value;
                }
            }
        }

        #endregion

        #region Grid Events....
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "NetAmnt"));
                dtaxable = dtaxable + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotTaxableAmnt"));
                dtax = dtax + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotTax"));
                dDiscAmnt = dDiscAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DiscAmnt"));
                dOtherAmnt = dOtherAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OtherAmnt"));
                    
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lbltaxableAmnt = (Label)e.Row.FindControl("lbltaxableAmnt");
                Label lbltax = (Label)e.Row.FindControl("lbltax");
                Label lblDiscAmount = (Label)e.Row.FindControl("lblDiscAmount");
                Label lblOtherAmount = (Label)e.Row.FindControl("lblOtherAmount");
                
                lblAmount.Text = dNetAmnt.ToString("N2");
                lbltaxableAmnt.Text = dtaxable.ToString("N2");
                lbltax.Text = dtax.ToString("N2");
                lblDiscAmount.Text = dDiscAmnt.ToString("N2");
                lblOtherAmount.Text = dOtherAmnt.ToString("N2");
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }
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
    }
}