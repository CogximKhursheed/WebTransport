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
    public partial class GRConsldRepRetlr : Pagebase
    {        
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRetailerRepDAL objGRDAL = new GRRetailerRepDAL();
        private int intFormId = 39;
        double GrossAmnt = 0.00;
        double Weight = 0.00;
        double Qty = 0.00;
        double Lorry_Freight = 0.00;
        double NetAmnt = 0.00;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event
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

                this.BindSenderName();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                //this.BindReceivrName();
                //this.BindDelvrPlce();
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
            //ddlRecvr.SelectedIndex = 0;
           // ddlDelvPlce.SelectedIndex = 0;
            ddlSender.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            drpBaseCity.Focus();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...



        private void BindSenderName()
        {
            GRRetailerRepDAL obj = new GRRetailerRepDAL();
            var SenderName = obj.BindSender();
            ddlSender.DataSource = SenderName;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            objGRDAL = null;
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objGRDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objGRDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        //private void BindReceivrName()
        //{
        //    GRRetailerRepDAL obj = new GRRetailerRepDAL();
        //    var RecevrName = obj.BindRecever();
        //    ddlRecvr.DataSource = RecevrName;
        //    ddlRecvr.DataTextField = "Acnt_Name";
        //    ddlRecvr.DataValueField = "Acnt_Idno";
        //    ddlRecvr.DataBind();
        //    objGRDAL = null;
        //    ddlRecvr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}
        //private void BindDelvrPlce()
        //{
        //    GRRetailerRepDAL obj = new GRRetailerRepDAL();
        //    var DelvrPlace = obj.BindDelvryPlace();
        //    ddlDelvPlce.DataSource = DelvrPlace;
        //    ddlDelvPlce.DataTextField = "City_Name";
        //    ddlDelvPlce.DataValueField = "City_Idno";
        //    ddlDelvPlce.DataBind();
        //    objGRDAL = null;
        //    ddlDelvPlce.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}

        private void BindGrid()
        {

            GRRetailerRepDAL obj = new GRRetailerRepDAL();
            Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            Int64 iSenderIDNO = (ddlSender.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSender.SelectedValue));            
            Int64 GrNO = string.IsNullOrEmpty(txtGrNo.Text.Trim())? 0 : Convert.ToInt64(txtGrNo.Text.Trim());
            string strreporttypr = "SelectRep";
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            DataTable list = obj.SelectRep(strreporttypr, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                          Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO,
                          iSenderIDNO, GrNO, conString);

            if ((list != null) && (list.Rows.Count > 0))
            {

                ViewState["CSVdt"] = list;
                grdMain.DataSource = list;
                grdMain.DataBind();

                Double TotGrossAmnt = 0, TotWeight = 0, TotQty = 0;

                for (int i = 0; i < list.Rows.Count; i++)
                {
                    TotGrossAmnt += Convert.ToDouble(list.Rows[i]["Gross_Amnt"]);
                    TotWeight += Convert.ToDouble(list.Rows[i]["Tot_ChWeight"]);
                    TotQty += Convert.ToDouble(list.Rows[i]["Tot_Qty"]);                    
                }
                lblGrossAmnt.Text = TotGrossAmnt.ToString("N2");
                lblWeight.Text = TotWeight.ToString("N2");
                lblQty.Text = TotQty.ToString("N2");
                lblGrossAmnt.Visible = false;
                lblWeight.Visible = false;
                lblQty.Visible = false;                

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + list.Rows.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                imgBtnExcel.Visible = false;
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }

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
                Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iSenderIDNO = (ddlSender.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSender.SelectedValue));
                Int64 GrNO = string.IsNullOrEmpty(txtGrNo.Text.Trim()) ? 0 : Convert.ToInt64(txtGrNo.Text.Trim());
                GRRetailerRepDAL obj = new GRRetailerRepDAL();
                DataTable list1 = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO,
                             iSenderIDNO, GrNO, conString);
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

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
                GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
                Weight += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_ChWeight"));
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Qty"));
                //Lorry_Freight += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Lorry_Frieght"));
                //NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGross = (Label)e.Row.FindControl("lblgross");
                lblGross.Text = GrossAmnt.ToString("N2");

                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                lblWeight.Text = Weight.ToString("N2");

                Label lblQty = (Label)e.Row.FindControl("lblQty");
                lblQty.Text = Qty.ToString("N2");

                //Label lblLorryFre = (Label)e.Row.FindControl("lblLorryFre");
                //lblLorryFre.Text = Lorry_Freight.ToString("N2");

                //Label lblNetAMnt = (Label)e.Row.FindControl("lblNetAmnt");
                //lblNetAMnt.Text = NetAmnt.ToString("N2");
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

        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            #region "Old Code for export"
            //CSVTable = (DataTable)ViewState["CSVdt"];
            //if (CSVTable != null && CSVTable.Rows.Count > 0)
            //{
            //    CSVTable.Columns.Remove("GR_Idno");
            //    CSVTable.Columns["GrRet_Date"].ColumnName = "GR.Date";
            //    CSVTable.Columns["GrRet_No"].ColumnName = "GR.No";
            //    CSVTable.Columns["Manual_No"].ColumnName = "Manual No";
            //    CSVTable.Columns["Sender"].ColumnName = "Sender";
            //    CSVTable.Columns["Receiver"].ColumnName = "Reciver";
            //    CSVTable.Columns["GRRet_Pref"].ColumnName = "Inv. No.";
            //    CSVTable.Columns["Ref_Date"].ColumnName = "Inv. Date";
            //    CSVTable.Columns["From_City"].ColumnName = "From City";
            //    CSVTable.Columns["Delivery_Place"].ColumnName = "Delivery Place";
            //    CSVTable.Columns["Tot_Qty"].ColumnName = "Quantity";
            //    CSVTable.Columns["Tot_ChWeight"].ColumnName = "Weight";
            //    CSVTable.Columns["Tot_Rate"].ColumnName = "Item Rate";
            //    CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";
            //    CSVTable.Columns["Lorry_No"].ColumnName = "Lorry No";

            //    CSVTable.Columns["Inv_No"].ColumnName = "Bill No";
            //    CSVTable.Columns["Inv_Date"].ColumnName = "Bill Date";

            //    CSVTable.Columns.Remove("GRRetHead_Idno");
            //    CSVTable.Columns.Remove("city_idno");
            //    CSVTable.Columns.Remove("Acnt_Idno");

            //    CSVTable.Columns["Lorry_Frieght"].ColumnName = "Lorry Freight";
            //    CSVTable.Columns["Chln_No"].ColumnName = "Challlan No";
            //    CSVTable.Columns["Chln_Date"].ColumnName = "Challlan Date";
            //    CSVTable.Columns["Delvry_Date"].ColumnName = "Delivery Date";
            //    //CSVTable.Columns.Remove("GR_Idno");
                
            //    CSVTable.Columns.Remove("GR_Idno1");
            //    ExportDataTableToCSV(CSVTable, "GRConsldRepRetlr" + txtDateFrom.Text + "TO" + txtDateTo.Text);
            //    Response.Redirect("GRConsldRepRetlr.aspx");
            //}
            #endregion
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
            string attachment = "attachment; filename=GRRetCombinedReport.xls";
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
        #region "Old Code for export"
        //private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        //{
        //    HttpContext context = HttpContext.Current;
        //    context.Response.Clear();
        //    context.Response.ContentType = "text/csv";
        //    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
        //    //Write a row for column names
        //    foreach (DataColumn dataColumn in dataTable.Columns)
        //        context.Response.Write(dataColumn.ColumnName + ",");
        //    StringWriter sw = new StringWriter();
        //    context.Response.Write(Environment.NewLine);
        //    //Write one row for each DataRow
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
        //            context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
        //        context.Response.Write(Environment.NewLine);
        //    }
        //    context.Response.End();
        //    Response.End();
        //}
        #endregion
        #endregion
    }
}