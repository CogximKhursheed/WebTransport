using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Reflection;
namespace WebTransport
{
    public partial class DeliveryChallanRpt : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
        double dGrossAmnt = 0, dNetAmnt = 0, dKattAmnt = 0, dOtherAmnt = 0;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImgCSV);
            txtChlnNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                //this.BindState();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindFromCity();
                }
                else
                {
                    this.BindCityFrom(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityDelivery.SelectedValue = Convert.ToString(base.UserFromCity);
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtChlnNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                //txtGRNo.Attributes.Add("onmouseover", "javascript:this.style.color='gold'");
                //txtGRNo.Attributes.Add("onmouseout", "javascript:this.style.color='black'");

                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);



                //this.binddate
                //selectectd
                //==0 or -1
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int32 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int ChallanNo = string.IsNullOrEmpty(Convert.ToString(txtChlnNo.Text)) ? 0 : Convert.ToInt32(txtChlnNo.Text);
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            int cityfrom = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(drpCityDelivery.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            DataTable lstGridData = obj.ReportDelvChallan(UserIdno, yearIDNO, dtfrom, dtto, cityfrom, ChallanNo, ApplicationFunction.ConnectionString());
            obj = null;
            if (lstGridData != null && lstGridData.Rows.Count > 0)
            {

                ViewState["CSVdt"] = lstGridData;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Rows.Count;
                //grdprint.DataSource = lstGridData;
                //grdprint.DataBind();
                ImgCSV.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): 0 ";
                //grdprint.DataSource = null;
                //grdprint.DataBind();
                ImgCSV.Visible = false;
            }
        }
        private void BindFromCity()
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            var lst = obj.SelectCityCombo();
            obj = null;

            if (lst.Count > 0)
            {
                drpCityDelivery.DataSource = lst;
                drpCityDelivery.DataTextField = "City_Name";
                drpCityDelivery.DataValueField = "City_Idno";
                drpCityDelivery.DataBind();
                drpCityDelivery.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void BindFromCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityDelivery.DataSource = FrmCity;
            drpCityDelivery.DataTextField = "CityName";
            drpCityDelivery.DataValueField = "CityIdno";
            drpCityDelivery.DataBind();
            obj = null;
            drpCityDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
    
        private void BindCityFrom(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityDelivery.DataSource = FrmCity;
            drpCityDelivery.DataTextField = "CityName";
            drpCityDelivery.DataValueField = "cityidno";
            drpCityDelivery.DataBind();
            drpCityDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                    Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = hidmaxdate.Value;
                }
            }
        }
        ///// <summary>
        ///// To Bind State DropDown
        ///// </summary>
        //private void BindState()
        //{
        //    CityMastDAL objclsCityMaster = new CityMastDAL();
        //    var objCityMast = objclsCityMaster.SelectState();
        //    objclsCityMaster = null;
        //    drpState.DataSource = objCityMast;
        //    drpState.DataTextField = "State_Name";
        //    drpState.DataValueField = "State_Idno";
        //    drpState.DataBind();
        //    drpState.Items.Insert(0, new ListItem("< Choose State >", "0"));
        //}
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
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
                dGrossAmnt = dGrossAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
                dKattAmnt = dKattAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Katt_Amnt"));
                dOtherAmnt = dOtherAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Other_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblNetAmnt");
                lblAmount.Text = Convert.ToDouble(dNetAmnt).ToString("N2");
                Label lblGrossAmount = (Label)e.Row.FindControl("lblGrossAmnt");
                lblGrossAmount.Text = Convert.ToDouble(dGrossAmnt).ToString("N2");
                Label lblKattAmount = (Label)e.Row.FindControl("lblKattAmnt");
                lblKattAmount.Text = Convert.ToDouble(dKattAmnt).ToString("N2");
                Label lblOtherAmount = (Label)e.Row.FindControl("lblOtherAmnt");
                lblOtherAmount.Text = Convert.ToDouble(dOtherAmnt).ToString("N2");
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
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=ReceiptGoodsReceived.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    grdprint.AllowPaging = false;


            //    grdprint.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in grdprint.HeaderRow.Cells)
            //    {
            //        cell.BackColor = grdprint.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in grdprint.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = grdprint.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = grdprint.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    grdprint.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
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

        protected void ImgCSV_Click(object sender, ImageClickEventArgs e)
        {
            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {

                CSVTable.Columns["DelvChln_No"].ColumnName = "Challan No";
                CSVTable.Columns["DelvChln_Date"].ColumnName = "Challan Date";
                CSVTable.Columns["City_Name"].ColumnName = "Delivery Place";
                CSVTable.Columns["Lorry_No"].ColumnName = "Truck No";
                CSVTable.Columns["Acnt_name"].ColumnName = "Transpoter";
                CSVTable.Columns["Gross_amnt"].ColumnName = "Gross Amnt";
                CSVTable.Columns["Other_Amnt"].ColumnName = "Other Amnt";
                CSVTable.Columns["Katt_Amnt"].ColumnName = "Katt Amnt";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amnt";
                ExportDataTableToCSV(CSVTable, "DeliveryChallanReport" + Datefrom.Text + "_TO_" + Dateto.Text);
                Response.Redirect("DeliveryChallanReport.aspx");
            }
        }
    }
}