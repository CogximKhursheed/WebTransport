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
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Drawing;

namespace WebTransport
{
    public partial class ChallanConfirmationRep : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        InvoiceRepDAL objInvcDAL = new InvoiceRepDAL();
        private int intFormId = 41;
        double dTotQty = 0, dToAmnt = 0,dToAdvAmnt = 0;
        DataTable CSVTable = new DataTable();
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
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                BindTruckNo();
                SetDate();
                TotalRecords();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            obj = null;
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
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTruckNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            ddlTruckNo.DataSource = lst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            objInvcDAL = null;
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindGrid()
        {
            try
            {
                ChlnConfirmationRepDAL obj = new ChlnConfirmationRepDAL();
                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iTruckIDNO = (Convert.ToString(ddlTruckNo.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
                Int32 ichlnType = (Convert.ToString(ddlChlnType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlChlnType.SelectedValue));
                DateTime? DtFrom;
                DateTime? DtTo;
                if (txtDateFrom.Text == "")
                {
                    DtFrom = null;
                }
                else
                {
                    DtFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
                }
                if (txtDateTo.Text == "")
                {
                    DtTo = null;
                }
                else
                {
                    DtTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
                }
                if (txtDateFrom.Text == "")
                {
                    DtFrom = null;
                }
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                DataTable lst = obj.SelectRep(DtFrom, DtTo, iFromCityIDNO, iTruckIDNO, ichlnType, UserIdno, conString);
                if ((lst != null) && (lst.Rows.Count > 0))
                {

                    ViewState["CSV"] = lst;
                    grdMain.DataSource = lst;
                    grdMain.DataBind();
                    Double TotalNetAmount = 0, TotalQty = 0, TotAdvAmount = 0;

                    for (int i = 0; i < lst.Rows.Count; i++)
                    {
                        TotalQty += Convert.ToDouble(lst.Rows[i]["Qty"]);
                        TotalNetAmount += Convert.ToDouble(lst.Rows[i]["Amount"]);
                        TotAdvAmount += Convert.ToDouble(lst.Rows[i]["Adv_Amnt"]);
                    }
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");
                    lblQty.Text = TotalQty.ToString("N2");
                    lblAdvAmnt.Text = TotAdvAmount.ToString("N2");

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lst.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    imgBtnExcel.Visible = true;
                    lblTotalRecord.Text = "T. Record (s): :" + Convert.ToString(lst.Rows.Count);


                }
                else
                {
                    ViewState["CSV"] = null;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    lblcontant.Visible = false;
                    divpaging.Visible = false;
                    imgBtnExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }

        public DataTable ToDataTable<T>(IList<T> data)// T is any generic type
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
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
                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iTruckIDNO = (Convert.ToString(ddlTruckNo.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
                Int32 ichlnType = (Convert.ToString(ddlChlnType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlChlnType.SelectedValue));
                ChlnConfirmationRepDAL obj = new ChlnConfirmationRepDAL();
                DataTable list1 = obj.SelectRep(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, iTruckIDNO, ichlnType, UserIdno, conString);
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }

        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                dToAmnt = dToAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dTotQty = dTotQty + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dToAdvAmnt = dToAdvAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Adv_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmount = (Label)e.Row.FindControl("lblNetAmount");
                lblNetAmount.Text = dToAmnt.ToString("N2");
                Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
                lblTotQty.Text = dTotQty.ToString("N2");
                Label lblChlnAmount = (Label)e.Row.FindControl("lblChlnAmount");
                lblChlnAmount.Text = dToAdvAmnt.ToString("N2");

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
            CSVTable = (DataTable)ViewState["CSV"];

            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                CSVTable.Columns.Remove("Delivered");
                CSVTable.Columns.Remove("Truck_Idno");
                CSVTable.Columns.Remove("Chln_Idno");
                CSVTable.Columns.Remove("BaseCity_Idno");
                CSVTable.Columns["From_City"].SetOrdinal(2);
                CSVTable.Columns["To_City"].SetOrdinal(3);
                CSVTable.Columns["Sender_name"].SetOrdinal(4);
                CSVTable.Columns["Reciver_Name"].SetOrdinal(5);
                CSVTable.Columns["Lorry_No"].SetOrdinal(6);
                CSVTable.Columns["Gr_No"].SetOrdinal(7);
                CSVTable.Columns["Gr_Date"].SetOrdinal(8);
                CSVTable.Columns["Qty"].SetOrdinal(9);
                CSVTable.Columns["Amount"].SetOrdinal(10);
                CSVTable.Columns["Gr_No"].ColumnName = "Gr No";
                CSVTable.Columns["Chln_No"].ColumnName = "Challan No.";
                CSVTable.Columns["Chln_Date"].ColumnName = "Challan Date";
                CSVTable.Columns["Lorry_No"].ColumnName = "Truck No.";
                CSVTable.Columns["Gr_Date"].ColumnName = "Gr.Date";
                CSVTable.Columns["Sender_name"].ColumnName = "Sender Name";
                CSVTable.Columns["Reciver_Name"].ColumnName = "Reciver Name";
                CSVTable.Columns["Qty"].ColumnName = "Qty";
                CSVTable.Columns["To_City"].ColumnName = "To City";
                CSVTable.Columns["From_City"].ColumnName = "From City";
                CSVTable.Columns["Amount"].ColumnName = "Amount";


                ExportDataTableToCSV(CSVTable, "ChallanConfirmation" + txtDateFrom.Text + "_To_" + txtDateTo.Text);
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
