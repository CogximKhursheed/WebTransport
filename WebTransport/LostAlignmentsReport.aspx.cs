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
using System.Net;
using System.Net.Mail;

namespace WebTransport
{
    public partial class LostAlignmentsReport : Pagebase
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
                this.BindLorryNo();


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
            drpLorryNo.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlDateRange.Focus();
        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...

        private void BindLorryNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var LorryNo = obj.BindTruckNo();
            drpLorryNo.DataSource = LorryNo;
            drpLorryNo.DataTextField = "Lorry_No";
            drpLorryNo.DataValueField = "Lorry_Idno";
            drpLorryNo.DataBind();
            obj = null;
            drpLorryNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                //ShortageRepDAL obj = new ShortageRepDAL();
                //DataTable list1 = obj.SelectRep("SelectRepWithoutParty", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), 0, 0, UserIdno, ApplicationFunction.ConnectionString());
                //lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }

        private void BindGrid()
        {
            try
            {
                LostAlignRepDAL obj = new LostAlignRepDAL();

                Int64 LorryIdno = (Convert.ToString(drpLorryNo.SelectedValue) == "" ? 0 : Convert.ToInt64(drpLorryNo.SelectedValue));

                var DsGrdetail = obj.SelectForSearch(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), LorryIdno);

            
                if ((DsGrdetail != null) && (DsGrdetail.Count > 0))
                {

                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();
                    DataTable dttemp1 = ApplicationFunction.CreateTable("tbl",
                                   "SrNo", "String",
                                   "Date", "String",
                                   "LorryNo", "String",
                                   "ItemName", "String",
                                   "SerialNo", "String",
                                   "PrevAlignDate", "String",
                                   "AlignDate", "String"
                                   );
                    for (int i = 0; i < DsGrdetail.Count; i++)
                    {
                        DataRow dr = dttemp1.NewRow();
                        dr["SrNo"] = Convert.ToString(i + 1);
                        
                        dr["Date"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(DsGrdetail[i], "Date")).ToString("dd-MM-yyyy"));
                        dr["LorryNo"] = Convert.ToString(DataBinder.Eval(DsGrdetail[i], "LorryNo"));
                        dr["ItemName"] = Convert.ToString(DataBinder.Eval(DsGrdetail[i], "ItemName"));
                        dr["SerialNo"] = Convert.ToString(DataBinder.Eval(DsGrdetail[i], "SerialNo"));
                        dr["PrevAlignDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(DsGrdetail[i], "PrevAlignDate")).ToString("dd-MM-yyyy"));
                        dr["AlignDate"] = Convert.ToString(Convert.ToDateTime(DataBinder.Eval(DsGrdetail[i], "AlignDate")).ToString("dd-MM-yyyy"));
                        dttemp1.Rows.Add(dr);
                    }
                    ViewState["dtCSV"] = dttemp1;
                    

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    imgBtnExcel.Visible = true;
                    lblTotalRecord.Text = "T. Record(s) :" + Convert.ToString(DsGrdetail.Count);

                }
                else
                {
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): 0 ";
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

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
             
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
                //CsvTable.Columns.Remove("SENDER_NAME");
                //CsvTable.Columns.Remove("RECIVR_NAME");
                //CsvTable.Columns["INV_NETAMNT"].ColumnName = "Net Amount";

                ExportDataTableToCSV(CsvTable, "DueAlignReport");
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

        protected void imgBtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            for(int i=0;i<=grdMain.Rows.Count-1;i++)
            {
               CheckBox chk=(CheckBox) grdMain.Rows[i].FindControl("chkSave4SMS");
               chk.Checked = true;
            }
        }

        protected void imgBtnDeselect_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i <= grdMain.Rows.Count - 1; i++)
            {
                CheckBox chk = (CheckBox)grdMain.Rows[i].FindControl("chkSave4SMS");
                chk.Checked = false;
            }
        }

        protected void lnkSendSMS_Click(object sender, EventArgs e)
        {
           
           
        }

      
    }
}
