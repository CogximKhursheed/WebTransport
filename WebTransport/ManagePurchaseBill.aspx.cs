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
    public partial class ManagePurchaseBill : Pagebase
    {
        #region Private Variable....
        private double dGrossAmnt = 0, dNetAmnt = 0;
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

                PurchaseBillDAL Obj = new PurchaseBillDAL();
                DateTime? datefromValue = null;
                DateTime? dateToValue = null;
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
                lblTotalRecord.Text = "T. Record (s): " + Obj.Select_PurchaseBillCount(Convert.ToInt32(ddlDateRange.SelectedValue), datefromValue, dateToValue);
                prints.Visible = false;
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {

            PurchaseBillDAL obj = new PurchaseBillDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
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
            var lstGridData = obj.Select_PurchaseBillDetailList(BillNo, datefromValue, dateToValue, cityfrom, sender, yearidno, UserIdno, purType);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = false;
                prints.Visible = false;

                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
                prints.Visible = false;
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
            // Used to hide Delete and Edit button if Pbillidno exists in Trip Entry
            LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
            LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
            string PBillIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PBillHead_Idno"));

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dNetAmnt.ToString("N2");
            }
            string PurBillid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PBillHead_Idno"));
            if (PurBillid != "")
            {
                PurchaseBillDAL obj = new PurchaseBillDAL();
                var IdExist = obj.CheckPbill(Convert.ToInt32(PurBillid));
                if (IdExist != null && IdExist.SerlDetl_id > 0)
                {
                    lnkbtnDelete.Visible = false;
                }
                else
                {
                    lnkbtnDelete.Visible = true;
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
            ddlDateRange.Focus();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReceiptGoodsReceived.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                //grdprint.AllowPaging = false;


                //grdprint.HeaderRow.BackColor = Color.White;
                //foreach (TableCell cell in grdprint.HeaderRow.Cells)
                //{
                //    cell.BackColor = grdprint.HeaderStyle.BackColor;
                //}
                //foreach (GridViewRow row in grdprint.Rows)
                //{
                //    row.BackColor = Color.White;
                //    foreach (TableCell cell in row.Cells)
                //    {
                //        if (row.RowIndex % 2 == 0)
                //        {
                //            cell.BackColor = grdprint.AlternatingRowStyle.BackColor;
                //        }
                //        else
                //        {
                //            cell.BackColor = grdprint.RowStyle.BackColor;
                //        }
                //        cell.CssClass = "textmode";
                //    }
                //}

                //grdprint.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
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