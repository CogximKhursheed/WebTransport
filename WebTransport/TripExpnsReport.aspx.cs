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
    public partial class TripExpnsReport : Pagebase
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
                this.BindDriver(0);
                this.BindTruckNo();
                this.BindExpense();
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                TotalRecordCount();
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
            drpExpense.SelectedIndex = 0;
            ddlDriver.SelectedIndex = 0;
            ddlTruckNo.SelectedIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();
            drpExpense.Focus();
        }
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region Functions...
        private void BindExpense()
        {
            TripEntryDAL obj = new TripEntryDAL();
            var lst = obj.BindExpensReport();
            obj = null;
            if (lst != null && lst.Count > 0)
            {
                drpExpense.DataSource = lst;
                drpExpense.DataTextField = "ExpName";
                drpExpense.DataValueField = "ExpIdno";
                drpExpense.DataBind();
            }
            drpExpense.Items.Insert(0, new ListItem("--Select--", "0"));
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
            Int64 iExpenseIdno = (drpExpense.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpExpense.SelectedValue));
            Int64 iTruckIdno = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
            Int64 iDriverIdno = (ddlDriver.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDriver.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            string ExpName = Convert.ToString((drpExpense.SelectedIndex <= 0 ? "" : Convert.ToString(drpExpense.SelectedItem.Text.Trim())));
            DateTime dtFromDate =Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text).ToString());
            DateTime dtToDate= Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
            DataTable list = obj.SelectExpRep("SelectTripExpReport", dtFromDate, dtToDate, iExpenseIdno, iTruckIdno, iDriverIdno, ExpName,conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
                DataTable Dt = list.Clone();
                Dt = list.Copy();
                Dt.Columns.Remove("Trip_Idno");

                Dt.AcceptChanges();
    
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = Dt;
                }

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
        private void TotalRecordCount()
        {
            TripEntryDAL obj = new TripEntryDAL();
            Int64 iExpenseIdno = (drpExpense.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpExpense.SelectedValue));
            Int64 iTruckIdno = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
            Int64 iDriverIdno = (ddlDriver.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDriver.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            Int32 TripNo = 0;
            DateTime dtFromDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text).ToString());
            DateTime dtToDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
            DataTable list = obj.SelectExpRep("SelectTripExpReport", dtFromDate, dtToDate, iExpenseIdno, iTruckIdno, iDriverIdno, "", conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else { lblTotalRecord.Text = "T. Record (s): 0"; }
        }
        #endregion

        #region Control Events...
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTruckNo.SelectedValue) > 0)
            {
                TripEntryDAL ObjDAl = new TripEntryDAL();

                Int32 Typ = 0;
                Typ = ObjDAl.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                ddlDriver.DataSource = null;
                if (ddlDriver.Items.Count > 0)
                {
                    ddlDriver.Items.Clear();
                }
                BindDriver(Typ);
            }
            else
            {
                BindDriver(0);
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
                NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Exp_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = NetAmnt.ToString("N2");
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
                dt.Columns["AcntName"].SetOrdinal(5);
                dt.Columns["AcntName"].ColumnName = "Expense Name";
                dt.Columns["Exp_Amnt"].ColumnName = "Exp Amount";
                Export(dt);
            }
        }
        #endregion
    }
}

