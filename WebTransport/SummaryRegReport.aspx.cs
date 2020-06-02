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
    public partial class SummaryRegReport: Pagebase
    {
        #region Variable ...
        static FinYear UFinYear = new FinYear();
        ShortageRepDAL objInvcDAL = new ShortageRepDAL();
        private int intFormId = 42;
        double dGross = 0, dkatt = 0, dlabour = 0, ddlvry = 0,doctrai=0, dNetAmount = 0;
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
                this.BindPartyName();
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
           // drpPartyName.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
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

        private void BindPartyName()
        {
            ShortageRepDAL obj = new ShortageRepDAL();
            var PartyName = obj.BindSender();
            //drpPartyName.DataSource = PartyName;
            //drpPartyName.DataTextField = "Acnt_Name";
            //drpPartyName.DataValueField = "Acnt_Idno";
            //drpPartyName.DataBind();
            //objInvcDAL = null;
            //drpPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            ShortageRepDAL obj = new ShortageRepDAL();
            var FrmCity = obj.BindFromCity();
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

        private void BindGrid()
        {
            try
            {
                SummaryRegisterDAL obj = new SummaryRegisterDAL();
                string userclass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (userclass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                DataTable DsGrdetail = null;
                if (iFromCityIDNO == 0)
                {
                    DsGrdetail = obj.SelectRep("Selectreport", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)),  UserIdno, ApplicationFunction.ConnectionString());

                }
                else
                {
                    DsGrdetail = obj.SelectreportwithCity("SelectreportwithCity", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, UserIdno, ApplicationFunction.ConnectionString());
                }
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    ViewState["dtCSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();
                    imgBtnExcel.Visible = true;
                    //printRep.Visible = true;
                    
                }
                else
                {
                    ViewState["dtCSV"] = null;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    //  printRep.Visible = false;
                    imgBtnExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        //private DataTable getInvoiceDetails(Int32 Inv_Idno)
        //{
        //    string str = string.Empty;
        //    string str1 = string.Empty;
        //    string str2 = string.Empty;
        //    string str3 = string.Empty;
        //    string str4 = string.Empty;
        //    ShortageRepDAL objclsInvoiceRepDAL = new ShortageRepDAL();
        //    DataTable lst = objclsInvoiceRepDAL.selectInvoiceReportDetails("SelectInvDet", Inv_Idno, ApplicationFunction.ConnectionString());
        //    return lst;

        //}

        #endregion

        #region Grid Event...
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
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "cmdedit")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            //}
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dGross = dGross + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total_Amnt1"));
                dkatt = dkatt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Katt_Amnt"));
                dlabour = dlabour + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Labour_Amnt"));
                ddlvry = ddlvry + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Delivery_Amnt"));
                doctrai = doctrai + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Octrai_Amnt"));
                dNetAmount = dNetAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblgrossamnt = (Label)e.Row.FindControl("lblgrossamnt");
                lblgrossamnt.Text = dGross.ToString("N2");
                Label lblKatt = (Label)e.Row.FindControl("lblKatt");
                lblKatt.Text = dkatt.ToString("N2");
                Label lbllabouramnt = (Label)e.Row.FindControl("lbllabouramnt");
                lbllabouramnt.Text = dlabour.ToString("N2");
                Label lbldelvryamnt = (Label)e.Row.FindControl("lbldelvryamnt");
                lbldelvryamnt.Text = ddlvry.ToString("N2");
                Label lbloctrai = (Label)e.Row.FindControl("lbloctrai");
                lbloctrai.Text = doctrai.ToString("N2");
                Label lblnetamnt = (Label)e.Row.FindControl("lblnetamnt");
                lblnetamnt.Text = dNetAmount.ToString("N2");
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
                CsvTable.Columns.Remove("Id");
                CsvTable.Columns["SNo"].ColumnName = "SNo";
                CsvTable.Columns["Summary_No"].ColumnName = "Summary No";
                CsvTable.Columns["Summary_Date"].ColumnName = "Summary Date";
                CsvTable.Columns["ToCity_Name"].ColumnName = "To City";
                CsvTable.Columns["Chln_No"].ColumnName = "Challan No.";
                CsvTable.Columns["Truck_No"].ColumnName = "Truck No";
                CsvTable.Columns["Driver_Name"].ColumnName = "Driver";
                CsvTable.Columns["Crossing_Amnt"].ColumnName = "Crossing Amnt.";
                CsvTable.Columns["Freight_Amnt"].ColumnName = "Freight Amnt.";
                CsvTable.Columns["Way_Amnt"].ColumnName = "Way Amnt.";
                CsvTable.Columns["Other_Charges"].ColumnName = "Other Charges";

                CsvTable.Columns["Total_Amnt1"].ColumnName = "Gross Amnt.";
                CsvTable.Columns["Katt_Amnt"].ColumnName = "Katt Amnt.";
                CsvTable.Columns["Delivery_Amnt"].ColumnName = "Delivery Amnt.";
                CsvTable.Columns["Labour_Amnt"].ColumnName = "Labour Amnt.";
                CsvTable.Columns["Octrai_Amnt"].ColumnName = "Octrai Amnt.";
                CsvTable.Columns["Total_Amnt2"].ColumnName = "Total";
                CsvTable.Columns["Net_Amnt"].ColumnName = "Net Amnt.";
                ExportDataTableToCSV(CsvTable, "SRReport");
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

