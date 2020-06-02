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

namespace WebTransport
{
    public partial class CostCategorySumryRep : Pagebase
    {
        #region Variable...
        string conString = "", msg = "";
        Double dNetAmount1 = 0, dNetAmount2 = 0, bal = 0, bal1 = 0;
        private int intFormId = 55;
        CostCategorySumryDAL objAccountBookDAL = new CostCategorySumryDAL();
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            conString = ApplicationFunction.ConnectionString();
            if (Request.UrlReferrer == null)
            {
                //base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                    printRep.Visible = false;
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = false;
                }
                BindDateRange();
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Validfrom date can not be greater than Validto date!');", true);
                        ddlDateRange.Focus();
                        return;
                    }
                }
                this.BindGrid();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Bind Event...
        private void BindGrid()
        {
            try
            {   
                DataTable dsTable1 = ConvertToDatatable();
                if (dsTable1 != null && dsTable1.Rows.Count > 0)
                {
                    grdMain.DataSource = dsTable1;
                    grdMain.DataBind();
                    imgBtnExcel.Visible = true;
                    printRep.Visible = true;
                    lblTotalRecord.Text = "T. Record (s): " + dsTable1.Rows.Count;
                    //if (rdoOB.Checked == true || rdoRP.Checked == true)
                }
                else
                {
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    imgBtnExcel.Visible = false;
                    printRep.Visible = false;
                    lblTotalRecord.Text = "T. Record (s): 0 ";
                }


                objAccountBookDAL = null;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        private DataTable ConvertToDatatable()
        {
            CostCategorySumryDAL objcostDAL = new CostCategorySumryDAL();
            DataTable dTN = new DataTable();
            hidstr.Value = "";
            string strAction = "";
            DateTime? strDateFrom; DateTime? strDateTo;
            if (txtDateFrom.Text == "")
            {
                strDateFrom = null;
            }
            else
            {
                strDateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            }
            if (txtDateTo.Text == "")
            {
                strDateTo = null;
            }
            else
            {
                strDateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            int intyearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
            //Int64 intCompIdno = ddlcompany.SelectedItem.Text == "All" ? 0 : Convert.ToInt64(ddlcompany.SelectedValue);
            #region Ledger Report
            DataTable dsTable = objcostDAL.SelectRep("SelectReport", strDateFrom, strDateTo, intyearIdno, ApplicationFunction.ConnectionString());
            objcostDAL = null;
            return dsTable;
        }
            #endregion
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdshowdetail")
            {
                #region Ledger Account
                DataTable DtLedger = ConvertToDatatable();
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int PreviousRow = row.RowIndex - 1;
                LinkButton lnkBtnParticular = (LinkButton)row.FindControl("lnkBtnParticular");
                HiddenField hidTruckIdno = (HiddenField)row.FindControl("hidTruckIdno");
                Int64 TruckIdno = Convert.ToInt64(hidTruckIdno.Value);
                Session["TruckIdno"] = TruckIdno;
                Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                string startDate=txtDateFrom.Text;
                string EndDate= txtDateTo.Text; 
                Session["TruckNo"] = lnkBtnParticular.Text;
                Session["StartDate"] = Convert.ToString(txtDateFrom.Text);
                Session["EndDate"] = Convert.ToString(txtDateTo.Text);
                Response.Redirect("CostDetailMonthwiseRep.aspx?YearIdno=" + YearIdno+"&StartDate="+startDate+"&Enddate="+EndDate);
                #endregion
            }

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  grdMain.Columns[0].Visible = grdMain.Columns[1].Visible = grdMain.Columns[2].Visible = grdMain.Columns[3].Visible = true;
                //  grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = grdMain.Columns[8].Visible = false;
                //  grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = grdMain.Columns[11].Visible = grdMain.Columns[12].Visible = false;
                // grdMain.Columns[13].Visible = grdMain.Columns[14].Visible = grdMain.Columns[15].Visible = grdMain.Columns[16].Visible = grdMain.Columns[17].Visible = grdMain.Columns[18].Visible = false;

                #region FillGrid for Ladger Reports
                Label lblParticular = (Label)e.Row.FindControl("lblParticular");
                LinkButton lnkBtnParticular = (LinkButton)e.Row.FindControl("lnkBtnParticular");
                string particular = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI")).Trim();
                lblParticular.Text = particular;
                Label lbl2Debit = (Label)e.Row.FindControl("lbl2Debit");
                lbl2Debit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                string Debit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                Label lbl3Credit = (Label)e.Row.FindControl("lbl3Credit");
                lbl3Credit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                string Credit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                Label lbl4Balance = (Label)e.Row.FindControl("lbl4Balance");
                lbl4Balance.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Balance"));
                HiddenField hidTruckIdno = (HiddenField)e.Row.FindControl("hidTruckIdno");
                hidTruckIdno.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TRUCK_IDNO"));
                if (Debit.ToLower() == "0.00" && Credit.ToLower() == "0.00")
                {
                    lblParticular.Visible = true;
                    lnkBtnParticular.Visible = false;
                    lblParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI"));
                }
                else
                {
                    lblParticular.Visible = false;
                    lnkBtnParticular.Visible = true;
                  lnkBtnParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI"));
                }
                if (particular.ToLower() == "opening balance")
                {
                    lblParticular.Visible = true;
                    lnkBtnParticular.Visible = false;
                }

                //grdMain.Columns[0].Visible = true;
                //grdMain.Columns[1].Visible = true;
                //grdMain.Columns[2].Visible = true;
                //grdMain.Columns[3].Visible = true;

                //grdMain.Columns[4].Visible = false;
                //grdMain.Columns[5].Visible = false;
                //grdMain.Columns[6].Visible = false;
                //grdMain.Columns[7].Visible = false;
                //grdMain.Columns[8].Visible = false;
                //grdMain.Columns[9].Visible = false;
                //grdMain.Columns[10].Visible = false;
                //grdMain.Columns[18].Visible = false;

                dNetAmount1 = dNetAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT"));
                dNetAmount2 = dNetAmount2 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT"));
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl2TDebit = (Label)e.Row.FindControl("lbl2TDebit");
                lbl2TDebit.Text = String.Format("{0:0,0.00}", dNetAmount1.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lbl3TCredit = (Label)e.Row.FindControl("lbl3TCredit");
                lbl3TCredit.Text = String.Format("{0:0,0.00}", dNetAmount2.ToString("N", new CultureInfo("hi-IN"))).ToString();
            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
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
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
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
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            txtDateFrom.Text = hidmindate.Value;
            txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

        }
        #endregion

    }
}