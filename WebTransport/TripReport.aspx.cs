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
    public partial class TripReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double NetAmnt = 0.00;
        double GrossAmnt = 0.00;
        double IncetAmnt = 0.00;
        double ChlnTot = 0.00;
        double FuelTot = 0.00;
        double ExpnTot = 0.00;
        double TollTot = 0.00;
        double AdvTot = 0.00;
        double DieselTot = 0.00;
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
                //    if (base.CheckUserRights(intFormId) == false)
                //    {
                //        Response.Redirect("PermissionDenied.aspx");
                //    }
                //    if (base.View == false)
                //    {
                //        btnSearch.Visible = true;
                //    }

                this.BindTruckNo();
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
                SetDate();

                TotalRecordCount();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtTripNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            ddlDateRange.Focus();
        }
        #endregion

        private void TotalRecordCount()
        {
            TripEntryDAL obj = new TripEntryDAL();
            Int64 iLocationIdno = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            Int64 iTruckIdno = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
            Int64 iDriverIdno = (ddlDriver.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDriver.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            Int32 TripNo = 0;
            if (string.IsNullOrEmpty(txtTripNo.Text) != true)
            {
                TripNo = Convert.ToInt32(txtTripNo.Text);
            }
            DateTime dtFromDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text).ToString());
            DateTime dtToDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
            DataTable list = obj.SelectRep("SelectReport", dtFromDate, dtToDate, TripNo, iLocationIdno, iTruckIdno, iDriverIdno, conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else { lblTotalRecord.Text = "T. Record (s): 0"; }
        }

        #region Button Event...
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            ddlDriver.SelectedIndex = 0;
            ddlTruckNo.SelectedIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();
            drpBaseCity.Focus();
        }
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
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

        private void BindDriver(Int32 var)
        {
            TripEntryDAL obj = new TripEntryDAL();
            if (var == 0)
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Acnt_Name";
                    ddlDriver.DataValueField = "Acnt_Idno";
                    ddlDriver.DataBind();

                }
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlDriver.DataSource = null;
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddlDriver.DataSource = lst;
                    ddlDriver.DataTextField = "Driver_name";
                    ddlDriver.DataValueField = "Driver_Idno";
                    ddlDriver.DataBind();

                }
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        private void BindTruckNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddlTruckNo.DataSource = lst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();

            }
            ddlTruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindGrid()
        {
            TripEntryDAL obj = new TripEntryDAL();
            Int64 iLocationIdno = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            Int64 iTruckIdno = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
            Int64 iDriverIdno = (ddlDriver.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDriver.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            Int32 TripNo = 0;
            if (string.IsNullOrEmpty(txtTripNo.Text) != true)
            {
                TripNo = Convert.ToInt32(txtTripNo.Text);
            }
            DateTime dtFromDate =Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text).ToString());
            DateTime dtToDate= Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
            DataTable list = obj.SelectRep("SelectReport", dtFromDate, dtToDate, TripNo, iLocationIdno, iTruckIdno, iDriverIdno, conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
                DataTable Dt = list.Clone();
                Dt = list.Copy();
                Dt.Columns.Remove("Trip_Idno");

                Dt.AcceptChanges();
                double TNet = 0, Tgross = 0, TInsentive = 0, TChlnAmnt = 0, TFuel = 0, TExpn = 0, TToll = 0, TAdv = 0,TDiesel=0;
                for (int i = 0; i < Dt.Rows.Count; i++)
                { 
                   //Dt.Rows[i]["Fuel_Amnt"]
                }

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    TNet += Convert.ToDouble(Dt.Rows[i]["Net_Amnt"]);
                    Tgross += Convert.ToDouble(Dt.Rows[i]["Gross_Amnt"]);
                    TInsentive += Convert.ToDouble(Dt.Rows[i]["Insentive_Amnt"]);
                    TChlnAmnt += Convert.ToDouble(Dt.Rows[i]["Chln_NetAmnt"]);
                    TFuel += Convert.ToDouble(Dt.Rows[i]["Fuel_Amnt"]);
                    TExpn += Convert.ToDouble(Dt.Rows[i]["Exp_Amnt"]);
                    TToll += Convert.ToDouble(Dt.Rows[i]["Toll_Amnt"]);
                    TAdv += Convert.ToDouble(Dt.Rows[i]["Adv_Amnt"]);
                    TDiesel += Convert.ToDouble(Dt.Rows[i]["Diesel_Amnt"]);
                    if (i == Dt.Rows.Count - 1)
                    {
                        DataRow drr = Dt.NewRow();
                        drr["FKms"] = "Total";
                        drr["Net_Amnt"] = (TNet).ToString("N2");
                        drr["Gross_Amnt"] = (Tgross).ToString("N2");
                        drr["Insentive_Amnt"] = (TInsentive).ToString("N2");
                        drr["Chln_NetAmnt"] = (TChlnAmnt).ToString("N2");
                        drr["Fuel_Amnt"] = (TFuel).ToString("N2");
                        drr["Exp_Amnt"] = (TExpn).ToString("N2");
                        drr["Toll_Amnt"] = (TToll).ToString("N2");
                        drr["Adv_Amnt"] = (TAdv).ToString("N2");
                        drr["Diesel_Amnt"] = (TDiesel).ToString("N2");
                        Dt.Rows.Add(drr);
                        break;
                    }
                }
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = Dt;
                }

                //
                GridView1.DataSource = list;
                GridView1.DataBind();
                imgBtnExcel.Visible = true;
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                imgBtnExcel.Visible = false;
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TripSheet.xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in Dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in Dt.Rows)
            {
                str = "";
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
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
                NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
                IncetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Insentive_Amnt"));
                ChlnTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Chln_NetAmnt"));
                FuelTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Fuel_Amnt"));
                ExpnTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Exp_Amnt"));
                TollTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Toll_Amnt"));
                AdvTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Adv_Amnt"));
                DieselTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Diesel_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = NetAmnt.ToString("N2");

                Label lblGrossAmnt = (Label)e.Row.FindControl("lblGrossAmnt");
                lblGrossAmnt.Text = GrossAmnt.ToString("N2");

                Label lblIncAmnt = (Label)e.Row.FindControl("lblIncAmnt");
                lblIncAmnt.Text = IncetAmnt.ToString("N2");

                Label lblChlnTot = (Label)e.Row.FindControl("lblChlnTot");
                lblChlnTot.Text = ChlnTot.ToString("N2");

                Label lblFuelTot = (Label)e.Row.FindControl("lblFuelTot");
                lblFuelTot.Text = FuelTot.ToString("N2");

                Label lblExpAmnt = (Label)e.Row.FindControl("lblExpAmnt");
                lblExpAmnt.Text = ExpnTot.ToString("N2");

                Label lblTollAmnt = (Label)e.Row.FindControl("lblTollAmnt");
                lblTollAmnt.Text = TollTot.ToString("N2");

                Label lblAdvAmnt = (Label)e.Row.FindControl("lblAdvAmnt");
                lblAdvAmnt.Text = AdvTot.ToString("N2");

                Label lblDieselAmnt = (Label)e.Row.FindControl("lblDieselAmnt");
                lblDieselAmnt.Text = DieselTot.ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
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

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                dt.Columns["Trip_No"].ColumnName = "Trip No";
                dt.Columns["Trip_Date"].ColumnName = "Trip Date";
                dt.Columns["City_Name"].ColumnName = "Location";
                dt.Columns["Lorry_No"].ColumnName = "Lorry No";
                dt.Columns["Driver_Name"].ColumnName = "Driver Name";
                dt.Columns["Net_Amnt"].ColumnName = "Net Amount";
                dt.Columns["Gross_Amnt"].ColumnName = "Gross Amount";
                dt.Columns["Insentive_Amnt"].ColumnName = "Insentive Amount";
                dt.Columns["Chln_NetAmnt"].ColumnName = "Chln NetAmount";
                dt.Columns["Fuel_Amnt"].ColumnName = "Fuel Amount";
                dt.Columns["Exp_Amnt"].ColumnName = "Exp Amount";
                dt.Columns["Toll_Amnt"].ColumnName = "Toll Amount";
                dt.Columns["FKms"].ColumnName = "KMS";
                dt.Columns["Adv_Amnt"].ColumnName = "Advance Amnt";
                dt.Columns["Diesel_Amnt"].ColumnName = "Diesel Amnt";
                Export(dt);
            }
        }
        #endregion
    }
}

