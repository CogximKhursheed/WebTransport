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
    public partial class DispatchRegister : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double GrossAmnt = 0.00;
        double TotalWeight = 0.00;
        double SurChrgeAmnt = 0.00;
        double Commssion = 0.00;
        double Total = 0.00;
        double NetAmnt = 0.00;
        double dTotalDieselAmnt = 0.00;
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
                    this.BindDestination();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    this.BindDestination();
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
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

        private void BindDestination()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var DestiCity = obj.BindAllToCity();
            ddlDestination.DataSource = DestiCity;
            ddlDestination.DataTextField = "City_Name";
            ddlDestination.DataValueField = "City_Idno";
            ddlDestination.DataBind();
            objGRDAL = null;
            ddlDestination.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindGrid()
        {

            DispatchRegDAL obj = new DispatchRegDAL();
          
            Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            DataTable list = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                          Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, Convert.ToInt64(ddllorrytype.SelectedValue), Convert.ToInt64(ddlDestination.SelectedValue), conString);

            DataRow drw = list.NewRow();

            if ((list != null) && (list.Rows.Count > 0))
            {
                CalculateTotal(list);
                grdMain.DataSource = list;
                grdMain.DataBind();
                drw["OwnerName"] = "Total";
                drw["Tot_Weght"] = TotalWeight;
                drw["Amount"] = GrossAmnt;
                drw["Adv_Amnt"] = SurChrgeAmnt;
                drw["Commsn_Amnt"] = Commssion;
                drw["Net_Amnt"] = Total;
                list.Rows.Add(drw);
                ViewState["CSVdt"] = list;
                Double TotalNetAmount = 0, TotGrossAmnt = 0,  TotSurcharge = 0, TotWages = 0, TotServTax = 0;

                
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    //TotGrossAmnt += Convert.ToDouble(list.Rows[i]["Gross_Amnt"]);
                    
                    //TotSurcharge += Convert.ToDouble(list.Rows[i]["Surcrg_Amnt"]);
                    //TotWages += Convert.ToDouble(list.Rows[i]["Wages_Amnt"]);
                    //TotServTax += Convert.ToDouble(list.Rows[i]["ServTax_Amnt"]);
                    //TotalNetAmount += Convert.ToDouble(list.Rows[i]["Net_Amnt"]);
                }
               //lblGrossAmnt.Text = TotGrossAmnt.ToString("N2");
              
                   
                //lblSurcharge.Text = TotSurcharge.ToString("N2");
                //lblWages.Text = TotWages.ToString("N2");
                //lblServtax.Text = TotServTax.ToString("N2");
                //lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
        private void CalculateTotal(DataTable dt)
        {
            TotalWeight = Convert.ToDouble(dt.Compute("Sum(Tot_Weght)", ""));
            GrossAmnt = Convert.ToDouble(dt.Compute("Sum(Amount)", ""));
            SurChrgeAmnt = Convert.ToDouble(dt.Compute("Sum(Adv_Amnt)", ""));
            Commssion = Convert.ToDouble(dt.Compute("Sum(Commsn_Amnt)", ""));
            Total = Convert.ToDouble(dt.Compute("Sum(Net_Amnt)", ""));
            dTotalDieselAmnt = Convert.ToDouble(dt.Compute("Sum(Diesel_Amnt)", ""));
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
                DispatchRegDAL obj = new DispatchRegDAL();
                DataTable list1 = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)),
                           iFromCityIDNO, Convert.ToInt64(ddllorrytype.SelectedValue), Convert.ToInt64(ddlDestination.SelectedValue), conString);
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
                //GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                //GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
                //SurChrgeAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Surcrg_Amnt"));
                //Wages += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Wages_Amnt"));
                //ServTax += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ServTax_Amnt"));
                //NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //DataTable list = ViewState["CSVdt"];
                //GrossAmnt = Convert.ToDouble(list.Rows[i]["Amount"]);
                //lblGrossAmnt.Text = list.Compute("Sum(TotGrossAmnt)", "");
                

                //lblGrossAmnt.Text=
                //Label lblGross = (Label)e.Row.FindControl("lblGrossAmnt");
                lbltotweight.Text = TotalWeight.ToString("N2");
                lblGrossAmnt.Text = GrossAmnt.ToString("N2");
                lblSurcharge.Text = SurChrgeAmnt.ToString("N2");
                lblNetTotalAmount.Text = Total.ToString("N2");
                lblWages.Text = Commssion.ToString("N2");
                lblDieselTotal.Text = dTotalDieselAmnt.ToString("N2");
                //Label lblSurchge = (Label)e.Row.FindControl("lblSurchrge");
                //lblSurchge.Text = SurChrgeAmnt.ToString("N2");

                //Label lblWages = (Label)e.Row.FindControl("lblWages");
                //lblWages.Text = Wages.ToString("N2");

                //Label lblSerTax = (Label)e.Row.FindControl("lblServtax");
                //lblSerTax.Text = ServTax.ToString("N2");

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

            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                //CSVTable.Columns.Remove("GR_Idno");
                //CSVTable.Columns.Remove("Cartg_Amnt");
                //CSVTable.Columns.Remove("AgntComisn_Amnt");
                //CSVTable.Columns.Remove("Bilty_Amnt");
                //CSVTable.Columns.Remove("PF_Amnt");
                //CSVTable.Columns["Gr_No"].ColumnName = "GR.No";
                //CSVTable.Columns["Gr_Date"].ColumnName = "GR.Date";
                //CSVTable.Columns["GR_Typ"].ColumnName = "GR.Type";
                //CSVTable.Columns["Delivery_Place"].ColumnName = "Delivery Place";
                //CSVTable.Columns["From_City"].ColumnName = "From City";
                //CSVTable.Columns["Receiver"].ColumnName = "Reciver";
                //CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";

                //CSVTable.Columns["Surcrg_Amnt"].ColumnName = "Surcharge Amount";
                //CSVTable.Columns["Wages_Amnt"].ColumnName = "Wages Amount";
                //CSVTable.Columns["ServTax_Amnt"].ColumnName = "Service Tax";
                //CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amount";
                ExportDataTableToCSV(CSVTable, "DispatchRegister" + txtDateFrom.Text + "TO" + txtDateTo.Text);
                Response.Redirect("DispatchRegister.aspx");
            }
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

