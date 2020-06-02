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
    public partial class ChlnRpt : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
        private int intFormId = 73;
        double GrossAmnt = 0.00, Amount = 0.00, TDSAmnt = 0.00, NetAmnt = 0.00, Weight = 0.00, ShrtQty = 0.00, ShrtAmnt = 0.00, Comm = 0.00, Dieselamnt = 0.00;
        DataTable CSVTable = new DataTable();
        string Query = " AND 1=1";
        System.Globalization.CultureInfo cul = new System.Globalization.CultureInfo("ru-RU");
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
                if (base.CheckUserRights(intFormId) == false && Convert.ToString(Session["Userclass"]) != "Admin")
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }

                this.BindTruckNo();
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
                //TotalRecords();
                BindPartyName();
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


        #endregion

        #region Bind Event...
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
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objChlnBookingDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindPartyName()
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            var LorrMast = objLorrMast.SelectPartyName();
            objLorrMast = null;
            ddlPartyName.DataSource = LorrMast;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objChlnBookingDAL = null;
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
            objChlnBookingDAL = null;
            ddlDestination.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindGrid()
        {
            ChlnBookingDAL obj = new ChlnBookingDAL();
            DataTable list=null;
            Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            Int64 ITruckId = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
            Int64 ITruckType = Convert.ToInt64(ddllorrytype.SelectedValue);
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                Query += " AND BaseCity_Idno in (select FrmCity_Idno from tblfrmcitydetl where User_Idno= " + Convert.ToInt64(Session["UserIdno"]) +")";
            }
            if (txtDateFrom.Text != "")
            {
                Query += " AND CONVERT(DATE,Chln_Date)>='" + Convert.ToDateTime(txtDateFrom.Text.Trim(), cul).ToString("yyyy-MM-dd") + "'";
               // Query += " AND CONVERT(DATETIME,Chln_Date,105) >='" + Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text))+"'";
            }
            if (txtDateTo.Text != "")
            {
                Query += " AND CONVERT(DATE,Chln_Date)<='" + Convert.ToDateTime(txtDateTo.Text.Trim(), cul).ToString("yyyy-MM-dd") + "'";
                //Query += " AND CONVERT(DATETIME,Chln_Date,105) <='" + Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)) + "'";
            }
            if(Convert.ToInt32(drpBaseCity.SelectedValue) >0)
            {
                 Query += " AND BaseCity_Idno=" +Convert.ToInt32(drpBaseCity.SelectedValue);
            }
            if (Convert.ToInt64(ddlTruckNo.SelectedValue) > 0)
            {
                Query += " AND Truck_Idno=" + Convert.ToInt64(ddlTruckNo.SelectedValue);
            }
            if (Convert.ToInt64(ddllorrytype.SelectedValue) !=2)
            {
                Query += " AND Lorry_Type=" + Convert.ToInt64(ddllorrytype.SelectedValue);
            }
            if (Convert.ToInt64(ddlDestination.SelectedValue) > 0)
            {
                Query += " AND GH.DelvryPlce_Idno=" + Convert.ToInt64(ddlDestination.SelectedValue);
            }

            if (ddlReportType.SelectedValue == "0")
                list = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, ITruckId, ITruckType, UserIdno, Convert.ToInt64(ddlPartyName.SelectedValue), Convert.ToInt64(ddlDestination.SelectedValue), conString);
            if (ddlReportType.SelectedValue == "1")
                list = obj.SelectChlnGrWiseRep("SelectRepGRWise", Query, conString);

            if ((list != null) && (list.Rows.Count > 0))
            {
                ViewState["CSVdt"]  = list;
                ////  CALC Footer OF GRID
                Amount = Amount + Convert.ToDouble(list.Compute("SUM(Adv_Amnt)", ""));
                TDSAmnt = TDSAmnt + Convert.ToDouble(list.Compute("SUM(TDS_Amnt)", ""));
                GrossAmnt = GrossAmnt + Convert.ToDouble(list.Compute("SUM(Gross_Amnt)", ""));
                NetAmnt = NetAmnt + Convert.ToDouble(list.Compute("SUM(Net_Amnt)", ""));
                Weight = Weight + Convert.ToDouble(list.Compute("SUM(Tot_Weight)", ""));
                ShrtQty = ShrtQty + Convert.ToDouble((list.Compute("SUM(Shortage_Qty)", "")) is DBNull ? 0 : (list.Compute("SUM(Shortage_Qty)", "")));
                Comm = Comm + Convert.ToDouble(list.Compute("SUM(Comm_Amnt)", ""));
                Dieselamnt = Dieselamnt + Convert.ToDouble(list.Compute("SUM(Diesel_Amnt)", ""));
                ShrtAmnt = ShrtAmnt + Convert.ToDouble((list.Compute("SUM(Shortage_Amount)", "")) is DBNull ? 0 : (list.Compute("SUM(Shortage_Amount)", "")));

                grdMain.DataSource = list;
                grdMain.DataBind();

               

                if (ddlReportType.SelectedValue == "0")
                {
                    grdMain.Columns[3].Visible = false;
                    grdMain.Columns[4].Visible = false;
                    grdMain.Columns[5].Visible = false;

                    grdMain.Columns[12].Visible = false;
                    grdMain.Columns[11].Visible = false;
                    grdMain.Columns[15].Visible = false;
                }
                else
                {
                    grdMain.Columns[3].Visible = true;
                    grdMain.Columns[4].Visible = true;
                    grdMain.Columns[5].Visible = true;
                    grdMain.Columns[12].Visible = true;
                    grdMain.Columns[11].Visible = true;
                    grdMain.Columns[15].Visible = true;
                }
                if (CheckBox1.Checked)
                {                    
                    grdMain.Columns[16].Visible = true;
                    var test3 = grdMain.Columns[11].ToString();
                }
                else
                {                 
                    grdMain.Columns[16].Visible = false;
                }

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;

                imgBtnExcel.Visible = true;
                lnkbtnPrint.Visible = true;
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lnkbtnPrint.Visible = false;
                imgBtnExcel.Visible = false;
                lblTotalRecord.Text = "T. Record (s): 0 ";
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
                Int64 ITruckType = Convert.ToInt64(ddllorrytype.SelectedValue);
                Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 ITruckId = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlTruckNo.SelectedValue));
                ChlnBookingDAL obj = new ChlnBookingDAL();
                DataTable list1 = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, ITruckId, ITruckType, UserIdno, string.IsNullOrEmpty(ddlPartyName.SelectedValue) ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue), Convert.ToInt64(ddlDestination.SelectedValue), conString);
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
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = Amount.ToString("N2");

                Label lblTDSTax = (Label)e.Row.FindControl("lblTDSTax");
                lblTDSTax.Text = TDSAmnt.ToString("N2");

                Label lblGross = (Label)e.Row.FindControl("lblgross");
                lblGross.Text = GrossAmnt.ToString("N2");

                Label lblNetAMnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAMnt.Text = NetAmnt.ToString("N2");

                Label lblFtWeight = (Label)e.Row.FindControl("lblFtWeight");
                lblFtWeight.Text = Weight.ToString("N2");
               
                Label lblFTCommi = (Label)e.Row.FindControl("lblFTCommi");
                lblFTCommi.Text = Comm.ToString("N2");

                Label lblFTShrtQty = (Label)e.Row.FindControl("lblFTShrtQty");
                lblFTShrtQty.Text = ShrtQty.ToString("N2");

                Label lblDieselamnt = (Label)e.Row.FindControl("lblDieselamnt");
                lblDieselamnt.Text = Dieselamnt.ToString("N2");

                Label lblShrtAmnt = (Label)e.Row.FindControl("lblShrtAmnt");
                lblShrtAmnt.Text = ShrtAmnt.ToString("N2");
            }
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
            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {

                CSVTable.Columns["Chln_Date"].ColumnName = "Chln. Date";
                CSVTable.Columns["Chln_No"].ColumnName = "Chln. No";
                CSVTable.Columns["City_Name"].ColumnName = "Location";
                CSVTable.Columns["Lorry_No"].ColumnName = "Truck No.";
            //    CSVTable.Columns["Driver_Name"].ColumnName = "Driver Name";
                //CSVTable.Columns["To_City"].ColumnName = "Destination";
                CSVTable.Columns["TDS_Amnt"].ColumnName = "TDSTax Amnt.";
                CSVTable.Columns["Adv_Amnt"].ColumnName = "Amount";
                CSVTable.Columns["Diesel_Amnt"].ColumnName = "Diesel Amount";
                CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amount";
                CSVTable.Columns["Lorry_Type"].ColumnName = "Lorry Type";
                CSVTable.Columns["Shortage_Qty"].ColumnName = "Shortage Qty";
                CSVTable.Columns["Comm_Amnt"].ColumnName = "Comm Amnt";
                CSVTable.Columns["Tot_Weight"].ColumnName = "Tot Weight";
                
                
                if (CheckBox1.Checked)
                {
                    CSVTable.Columns["To_City"].SetOrdinal(5);
                    CSVTable.Columns["Item_Rate"].SetOrdinal(6);
                    CSVTable.Columns["Item_Rate"].ColumnName = "Item Rate";
                    CSVTable.Columns["To_City"].ColumnName = "To City";
                }
                else
                {
                    CSVTable.Columns.Remove("To_City");
                    CSVTable.Columns.Remove("Item_Rate");
                }
                CSVTable.Columns["Owner_Name"].ColumnName = "Owner Name";
                CSVTable.Columns["Pan_No"].ColumnName = "PAN No.";
                CSVTable.Columns["Recvd_Amnt"].ColumnName = "Recvd Amnt.";
                CSVTable.Columns["BalanceAmnt"].ColumnName = "Balance Amnt.";
                ExportDataTableToCSV(CSVTable, "ChlnRpt" + txtDateFrom.Text + "TO" + txtDateTo.Text);
                Response.Redirect("ChlnRpt.aspx");
            }
        }


        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
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

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            lblCompName.Text = lblCompName.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblAddress.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]) + "</br>" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])))
                lblPhone.Visible = false;
            else
               lblPhone.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
               lblCity.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
               lblState.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
               lblpincode.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
              //lblReport.Text = Convert.ToString(ddlParty.SelectedItem);
              //lblReportType.Text = Convert.ToString(ddlType.SelectedItem);
               lblDate.Text = "( " + Convert.ToString(txtDateFrom.Text) + " to " + Convert.ToString(txtDateTo.Text) + " )";
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('divPrint')", true);
              //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Call();", true);
        }
    }
}

