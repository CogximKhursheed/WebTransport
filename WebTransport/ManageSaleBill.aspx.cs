using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AutomobileOnline.Classes;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using System.Threading;
using System.Data;

namespace WebTransport
{
    public partial class ManageSaleBill : Pagebase
    {

        #region Private Variable....
        private int intFormId = 223;
        BindDropdownDAL obj;
        SaleBillDAL objSaleBillDAL;
        FinYearDAL objFinYearDAL;
        DataTable dt = new DataTable();
        bool st;
        #endregion

        #region Page Events...
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

                txtBillDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBillDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                this.BindCity();
                this.BindParty();
                this.ddlDateRange_SelectedIndexChanged(null, null);
                this.Countall();
            }
        }
        #endregion

        #region Functions....

        private void SetDate()
        {
            Int32 intyearid = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            objFinYearDAL = null;
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtBillDateFrom.Text = hidmindate.Value;
                    txtBillDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtBillDateFrom.Text = hidmindate.Value;
                    txtBillDateTo.Text = hidmaxdate.Value;
                }
            }
        }

        private void BindDateRange()
        {
            objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange();
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }
        private void BindParty()
        {
            obj = new BindDropdownDAL();
            var PartyName = obj.BindParty();
            obj = null;
            ddlPartyName.DataSource = PartyName;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party...", "0"));
        }
        private void BindCity()
        {
            obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }
        public void Countall()
        {
            objSaleBillDAL = new SaleBillDAL();
            Int64 count = objSaleBillDAL.Count();
            objSaleBillDAL = null;
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }

        private void BindGrid()
        {
            objSaleBillDAL = new SaleBillDAL();
            DateTime? datefromValue = null;
            DateTime? dateToValue = null;
            Int64 yearIDNO = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 BillNo = string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text)) ? 0 : Convert.ToInt64(txtBillNo.Text);
            Int64 BillType = string.IsNullOrEmpty(Convert.ToString(ddlBillType.SelectedValue)) ? 0 : Convert.ToInt64(ddlBillType.SelectedValue);
            Int64 CityFrom = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);
            Int64 Party = string.IsNullOrEmpty(Convert.ToString(ddlPartyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue);
            if (string.IsNullOrEmpty(Convert.ToString(txtBillDateFrom.Text)) == false)
            {
                datefromValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDateFrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtBillDateTo.Text)) == false)
            {
                dateToValue = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDateTo.Text));
            }
            var lstGridData = objSaleBillDAL.SelectForSearch(yearIDNO, datefromValue, dateToValue, txtprefNo.Text, BillNo, CityFrom, BillType, Party);
            objSaleBillDAL = null;
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
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("SaleBill.aspx?SbillIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                objSaleBillDAL = new SaleBillDAL();
                long intValue = objSaleBillDAL.Delete(Convert.ToInt32(e.CommandArgument));
                objSaleBillDAL = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlDateRange.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use!";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                Int64 intSbillIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "SbillHeadIdno"));

                if (intSbillIdno > 0)
                {
                    objSaleBillDAL = new SaleBillDAL();
                    var SbillExist = objSaleBillDAL.CheckClaimExists(Convert.ToInt64(intSbillIdno));
                    objSaleBillDAL = null;
                    if (SbillExist != null && SbillExist > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }

            }
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=SaleBillList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[0].Visible = false;
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

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
    }
}