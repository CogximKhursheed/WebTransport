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
    public partial class LorryMasterReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        LorryMasterRepDAL objLorryDAL = new LorryMasterRepDAL();
        private int intFormId = 43;
        DataTable CSVTable = new DataTable();
        //  double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0,dConsignrServTaxAmount=0, dNetAmount = 0;
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
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
                txtDateTo.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDateFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                this.BindPartyName();
                TotalRecords();
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            }
            ddlLorryType.Focus();
        }
        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlLorryType.SelectedIndex = 0;
            txtLorryNo.Text = "";
            ddlPanNo.SelectedIndex = 0;
            ddlPartyName.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlLorryType.Focus();
        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region Bind Event...
        private void BindPartyName()
        {
            LorryMasterRepDAL obj = new LorryMasterRepDAL();
            var PartyName = obj.SelectPartyName();
            ddlPartyName.DataSource = PartyName;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            objLorryDAL = null;
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 iPanNoIDNO = (Convert.ToString(ddlPanNo.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPanNo.SelectedValue));
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                LorryMasterRepDAL obj = new LorryMasterRepDAL();
                DataTable list1 = obj.SelectForSearch(-1, "", 0, 0, txtPanNumber.Text.Trim(),Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), conString);
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }

        private void BindGrid()
        {
            try
            {
                LorryMasterRepDAL obj = new LorryMasterRepDAL();
                Int32 iLorryTypeIDNO = (Convert.ToString(ddlLorryType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlLorryType.SelectedValue));
                Int32 iPanNoIDNO = (Convert.ToString(ddlPanNo.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPanNo.SelectedValue));
                Int32 iPartyIdno = (Convert.ToString(ddlPartyName.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlPartyName.SelectedValue));
                string iLorryNo = (Convert.ToString(txtLorryNo.Text) == "" ? "" : Convert.ToString(txtLorryNo.Text));

                DataTable DsGrdetail = obj.SelectForSearch(iLorryTypeIDNO, Convert.ToString(iLorryNo), iPanNoIDNO, iPartyIdno, txtPanNumber.Text.Trim(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), conString);
                if (DsGrdetail != null && DsGrdetail.Rows.Count > 0)
                {
                    ViewState["CSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();
                    
                    lblTotalRecord.Text = "T. Record (s): " + DsGrdetail.Rows.Count.ToString();
                    imgBtnExcel.Visible = true;

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;
                }
                else
                {
                    lblcontant.Visible = false;
                    divpaging.Visible = false;

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

        //protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {

        //    }
        //}
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
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
            CSVTable = (DataTable)ViewState["CSV"];
            CSVTable.Columns.Remove("LorryIdno");
            CSVTable.Columns.Remove("Lorry_Type");
            CSVTable.Columns.Remove("prty_Idno");
            CSVTable.Columns.Remove("Status");
            CSVTable.Columns["LorryType"].SetOrdinal(0);
            CSVTable.Columns["PartyName"].SetOrdinal(1);
            CSVTable.Columns["LorryNo"].SetOrdinal(2);
            CSVTable.Columns["LorryMake"].SetOrdinal(3);
            CSVTable.Columns["ChasisNo"].SetOrdinal(4);
            CSVTable.Columns["EngineNo"].SetOrdinal(5);
            CSVTable.Columns["PanNo"].SetOrdinal(6);
            CSVTable.Columns["OwnerName"].SetOrdinal(7);
            ExportDataTableToCSV(CSVTable, "LorryMasterReport");
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
        #endregion

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

       
    }
}

