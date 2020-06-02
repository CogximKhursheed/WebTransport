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
    public partial class PurchaseReg : Pagebase
    {
        #region Private Variable....
        private double dGrossAmnt = 0, dNetAmnt = 0, dQty = 0, dVat = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!IsPostBack)
            {
                ddlDateRange.Focus();
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange(); this.BindCityFrom(); this.bindsender();
                this.ddlDateRange_SelectedIndexChanged(sender, e);

                TotalRecordCount();
            }
        }
        #endregion

        #region Functions...

        private void TotalRecordCount()
        {
            PurchaseBillDAL obj = new PurchaseBillDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;

            int BillNo = string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text)) ? 0 : Convert.ToInt32(txtBillNo.Text);
            Int32 purType = Convert.ToInt32(ddlPurchaseType.SelectedValue);

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }

            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int sender = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            var lstGridData = obj.Select_PurchaseBillRegister(BillNo, datefromValue, dateToValue, cityfrom, sender, yearidno, UserIdno, purType);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count.ToString();
            }
            else { lblTotalRecord.Text = "T. Record (s): 0 "; }
        }

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PurchaseRegister.xls"));
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

            PurchaseBillDAL obj = new PurchaseBillDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;

            int BillNo = string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text)) ? 0 : Convert.ToInt32(txtBillNo.Text);
            Int32 purType = Convert.ToInt32(ddlPurchaseType.SelectedValue);

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }

            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int sender = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            var lstGridData = obj.Select_PurchaseBillRegister(BillNo, datefromValue, dateToValue, cityfrom, sender, yearidno, UserIdno, purType);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("BillNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Truck No", typeof(string));
                dt.Columns.Add("BillType", typeof(string));
                dt.Columns.Add("PurType", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("NetAmnt", typeof(string));
                dt.Columns.Add("Vat", typeof(string));

                double TNet = 0; double TQty = 0; double TVAt = 0; double Rate = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["BillNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PBillHead_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "PBillHead_Date")).ToString("dd-MM-yyyy");
                    dr["BillType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Bill_Type"));
                    dr["Truck No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["PurType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PurType"));
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["Location"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "City_Name"));
                    dr["Qty"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Qty")).ToString("N2");
                    dr["Rate"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Rate")).ToString("N2");
                    dr["NetAmnt"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    dr["Vat"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Vat")).ToString("N2");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    TQty += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Qty"));
                    TVAt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Vat"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Party"] = "Total";
                        drr["NetAmnt"] = (TNet).ToString("N2");
                        drr["Qty"] = (TQty).ToString("N2");
                        drr["Vat"] = (TVAt).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }

                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = false;

                Double TotalNetAmount = 0;
                Double TotalQty = 0;
                Double TotalVat = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    TotalQty += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Qty"));
                    TotalVat += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Vat"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");
                lblNQty.Text = TotalQty.ToString("N2");
                lblNVat.Text = TotalVat.ToString("N2");

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
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
        }

        private void bindsender()
        {
            PurchaseBillDAL objBill = new PurchaseBillDAL();
            DataTable dt = objBill.BindSenderForPurchaseBill(ApplicationFunction.ConnectionString());
            ddlSender.DataSource = dt;
            ddlSender.DataTextField = "Acnt Name";
            ddlSender.DataValueField = "Acnt Idno";
            ddlSender.DataBind();
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("PurchaseBill.aspx?PB=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {

                PurchaseBillDAL obj = new PurchaseBillDAL();
                Int32 intValue = obj.DeletePurchaseBill(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtBillNo.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }

        }
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
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dQty = dQty + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dVat = dVat + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Vat"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dNetAmnt.ToString("N2");

                Label lblQty = (Label)e.Row.FindControl("lblQty");
                lblQty.Text = dQty.ToString("N2");

                Label lblVat = (Label)e.Row.FindControl("lblVat");
                lblVat.Text = dVat.ToString("N2");
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