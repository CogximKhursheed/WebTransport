using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class GrReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double GrossAmnt = 0.00;
        double SurChrgeAmnt = 0.00;
        double Wages = 0.00;
        double ServTax = 0.00;
        double NetAmnt = 0.00;
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
                this.BindReceivrName();
                this.BindDelvrPlce();
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
            ddlRecvr.SelectedIndex = 0;
            ddlDelvPlce.SelectedIndex = 0;
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
            GRRepDAL obj = new GRRepDAL();
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
        private void BindReceivrName()
        {
            GRRepDAL obj = new GRRepDAL();
            var RecevrName = obj.BindRecever();
            ddlRecvr.DataSource = RecevrName;
            ddlRecvr.DataTextField = "Acnt_Name";
            ddlRecvr.DataValueField = "Acnt_Idno";
            ddlRecvr.DataBind();
            objGRDAL = null;
            ddlRecvr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDelvrPlce()
        {
            GRRepDAL obj = new GRRepDAL();
            var DelvrPlace = obj.BindDelvryPlace();
            ddlDelvPlce.DataSource = DelvrPlace;
            ddlDelvPlce.DataTextField = "City_Name";
            ddlDelvPlce.DataValueField = "City_Idno";
            ddlDelvPlce.DataBind();
            objGRDAL = null;
            ddlDelvPlce.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindGrid()
        {

            GRRepDAL obj = new GRRepDAL();
            Int64 iREcvrIDNO = (ddlRecvr.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlRecvr.SelectedValue));
            Int64 iSenderIDNO = (ddlSender.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSender.SelectedValue));
            Int64 iDElvryIDNO = (ddlDelvPlce.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDelvPlce.SelectedValue));
            Int32 iGRTypIDNO = (ddlGRType.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlGRType.SelectedValue));
            Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
           
            string strreporttypr ="";
            if(ddlreporttype.SelectedValue == "1")
            {
                strreporttypr = "SelectRep";
            }
            else
            {
                strreporttypr = "SelectRepItemWise";
            }
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            DataTable list = obj.SelectRep(strreporttypr, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                          Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iREcvrIDNO,
                          iSenderIDNO, iFromCityIDNO, iDElvryIDNO, iGRTypIDNO, UserIdno, conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
             
                ViewState["CSVdt"] = list;
                grdMain.DataSource = list;
                grdMain.DataBind();

                Double TotalNetAmount = 0, TotGrossAmnt = 0, TotSurcharge = 0, TotWages = 0, TotServTax = 0;

                for (int i = 0; i < list.Rows.Count; i++)
                {
                    TotGrossAmnt += Convert.ToDouble(list.Rows[i]["Gross_Amnt"]);
                    TotSurcharge += Convert.ToDouble(list.Rows[i]["Surcrg_Amnt"]);
                    TotWages += Convert.ToDouble(list.Rows[i]["Wages_Amnt"]);
                    TotServTax += Convert.ToDouble(list.Rows[i]["ServTax_Amnt"]);
                    TotalNetAmount += Convert.ToDouble(list.Rows[i]["Net_Amnt"]);
                }
                lblGrossAmnt.Text = TotGrossAmnt.ToString("N2");
                lblSurcharge.Text = TotSurcharge.ToString("N2");
                lblWages.Text = TotWages.ToString("N2");
                lblServtax.Text = TotServTax.ToString("N2");
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + list.Rows.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;

                if (ddlreporttype.SelectedValue == "1")
                {
                    grdMain.Columns[7].Visible = false;
                    grdMain.Columns[8].Visible = false;
                    //grdMain.Columns[9].Visible = false;
                    grdMain.Columns[10].Visible = false;
                    grdMain.Columns[11].Visible = false;
                    grdMain.Columns[12].Visible = false;
                    grdMain.Columns[13].Visible = false;
                }
                else
                {
                    grdMain.Columns[7].Visible = true;
                    grdMain.Columns[8].Visible = true;
                    //grdMain.Columns[9].Visible = true;
                    grdMain.Columns[10].Visible = true;
                    grdMain.Columns[11].Visible = true;
                    grdMain.Columns[12].Visible = true;
                    grdMain.Columns[13].Visible = true;
                }

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
                Int64 iREcvrIDNO = (ddlRecvr.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlRecvr.SelectedValue));
                Int64 iSenderIDNO = (ddlSender.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSender.SelectedValue));
                Int64 iDElvryIDNO = (ddlDelvPlce.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDelvPlce.SelectedValue));
                Int32 iGRTypIDNO = (ddlGRType.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlGRType.SelectedValue));
                Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                GRRepDAL obj = new GRRepDAL();
                DataTable list1 = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iREcvrIDNO,
                          iSenderIDNO, iFromCityIDNO, iDElvryIDNO, iGRTypIDNO, UserIdno, conString);
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
                SurChrgeAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Surcrg_Amnt"));
                Wages += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Wages_Amnt"));
                ServTax += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ServTax_Amnt"));
                NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGross = (Label)e.Row.FindControl("lblgross");
                lblGross.Text = GrossAmnt.ToString("N2");

                Label lblSurchge = (Label)e.Row.FindControl("lblSurchrge");
                lblSurchge.Text = SurChrgeAmnt.ToString("N2");

                Label lblWages = (Label)e.Row.FindControl("lblWages");
                lblWages.Text = Wages.ToString("N2");

                Label lblSerTax = (Label)e.Row.FindControl("lblServtax");
                lblSerTax.Text = ServTax.ToString("N2");

                Label lblNetAMnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAMnt.Text = NetAmnt.ToString("N2");
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
                CSVTable.Columns.Remove("GR_Idno");
                CSVTable.Columns.Remove("Cartg_Amnt");
                CSVTable.Columns.Remove("AgntComisn_Amnt");
                CSVTable.Columns.Remove("Bilty_Amnt");
                CSVTable.Columns.Remove("PF_Amnt");

                if (ddlreporttype.SelectedValue == "1")
                {
                    CSVTable.Columns.Remove("Item_Name");
                    CSVTable.Columns.Remove("Qty");
                    CSVTable.Columns.Remove("UnitType");
                    CSVTable.Columns.Remove("RateType");
                    CSVTable.Columns.Remove("Item_Rate");
                    CSVTable.Columns.Remove("Tot_Weght");
                }
                else
                {
                    CSVTable.Columns["Item_Name"].ColumnName = "Item Name";
                    CSVTable.Columns["Qty"].ColumnName = "Qty";
                    CSVTable.Columns["UnitType"].ColumnName = "Unit Type";
                    CSVTable.Columns["RateType"].ColumnName = "Rate Type";
                    CSVTable.Columns["Item_Rate"].ColumnName = "Item Rate";
                    CSVTable.Columns["Tot_Weght"].ColumnName = "Total Weight";
                   
                }
                


                CSVTable.Columns["Gr_No"].ColumnName = "GR.No";
                CSVTable.Columns["Gr_Date"].ColumnName = "GR.Date";
                CSVTable.Columns["GR_Typ"].ColumnName = "GR.Type";
                CSVTable.Columns["Delivery_Place"].ColumnName = "Delivery Place";
                CSVTable.Columns["From_City"].ColumnName = "From City";
                CSVTable.Columns["Receiver"].ColumnName = "Reciver";
                CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";

                CSVTable.Columns["Surcrg_Amnt"].ColumnName = "Surcharge Amount";
                CSVTable.Columns["Wages_Amnt"].ColumnName = "Wages Amount";
                CSVTable.Columns["ServTax_Amnt"].ColumnName = "Service Tax";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amount";
                ExportDataTableToCSV(CSVTable, "GrReport" + txtDateFrom.Text + "TO" + txtDateTo.Text);
                Response.Redirect("GrReport.aspx");
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

