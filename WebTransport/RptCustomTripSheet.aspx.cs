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


namespace WebTransport
{
    public partial class RptCustomTripSheet : Pagebase
    {
        #region Private Variable....
        private int intFormId = 27; double dGrossAmnt = 0, dNetAmnt = 0; DataTable ExportCSV = new DataTable();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
             txtTripNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    //imgBtnExcel.Visible = false;
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCityFrom();
                }
                else
                {
                    this.BindCityFrom(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityFrom.SelectedValue = Convert.ToString(base.UserFromCity);
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                this.BindLane();
                this.bindsender();
                this.BindLorry();
                imgBtnExcel.Visible = false;

                DateTime? dtfrom = null;
                DateTime? dtto = null;
                Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
                int TripNo = string.IsNullOrEmpty(Convert.ToString(txtTripNo.Text)) ? 0 : Convert.ToInt32(txtTripNo.Text);
                if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
                {
                    dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
                }
                if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
                {
                    dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
                }

                int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
                int senderr = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
                Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
                Int64 UserIdno = 0;
                if (Convert.ToString(Session["Userclass"]) != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }

                //var lstGridData = obj.SelectGR(GrNo, dtfrom, dtto, cityfrom, citto, senderr, yearidno, UserIdno);
                //obj = null;
                //if (lstGridData != null && lstGridData.Count > 0)
                //{
                //    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //}
            }
        }
         protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
       

        #region Functions...
        private void BindGrid()
        {
            CustomTripSheetDAL obj = new CustomTripSheetDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            Int64 Lane_Idno = Convert.ToInt64(ddlLane.SelectedValue);
            Int64 lorry_Idno = Convert.ToInt64(ddlLorry_No.SelectedValue);
            int TripNo = string.IsNullOrEmpty(Convert.ToString(txtTripNo.Text)) ? 0 : Convert.ToInt32(txtTripNo.Text);

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int sender = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            var lstGridData = obj.SelectTrip(TripNo, dtfrom, dtto, cityfrom, sender, yearidno, Lane_Idno, lorry_Idno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Trip No.", typeof(string));
                dt.Columns.Add("Pref No.", typeof(string));
                dt.Columns.Add("Trip_Date", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Truck No", typeof(string));
                dt.Columns.Add("Driver", typeof(string));
                dt.Columns.Add("Driver No", typeof(string));
                dt.Columns.Add("Vehicle Size", typeof(string));
                dt.Columns.Add("StartKm", typeof(string));
                dt.Columns.Add("EndKm", typeof(string));
                dt.Columns.Add("Total KM", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Lane", typeof(string));
                dt.Columns.Add("DSL_Card_Name", typeof(string));
                dt.Columns.Add("DSL_Card_Number", typeof(string));
                dt.Columns.Add("DSL_Qty", typeof(string));
                dt.Columns.Add("DSL_Rate", typeof(string));
                dt.Columns.Add("DSL_Amt", typeof(string));
                dt.Columns.Add("DSL_Card_Amt", typeof(string));
                dt.Columns.Add("Total_DSL_Qty", typeof(string));
                dt.Columns.Add("Total_DSL_Amt", typeof(string));
                dt.Columns.Add("Milage", typeof(string));
                dt.Columns.Add("Cash", typeof(string));
                dt.Columns.Add("Toll", typeof(string));
                dt.Columns.Add("Wages", typeof(string));
                dt.Columns.Add("Food_Exp", typeof(string));
                dt.Columns.Add("Repair", typeof(string));
                dt.Columns.Add("Adv_in_Driver", typeof(string));
                dt.Columns.Add("Other", typeof(string));
                dt.Columns.Add("Net Amount", typeof(string));
                dt.Columns.Add("Remark", typeof(string));

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Trip No."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Trip_Idno"));
                    dr["Trip_Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Trip_Date")).ToString("dd-MM-yyyy"); ;
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "City_Name"));
                    dr["Truck No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["Driver"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Driver_Name"));
                    dr["StartKm"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "StartKms"));
                    dr["EndKm"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "EndKms"));
                    dr["Total KM"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TotalKms"));
                    dr["Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Quantity"));
                    dr["Lane"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lane_Name"));
                    dr["Pref No."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Pref_No"));
                    dr["Driver No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Driver_No"));
                    dr["Vehicle Size"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Vehicle_Size"));
                    dr["DSL_Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Qty"));
                    dr["DSL_Rate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Rate"));
                    dr["DSL_Amt"] = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Amt"))) ? "0.0" : DataBinder.Eval(lstGridData[i], "DSL_Amt")).ToString("N2");
                    dr["DSL_Card_Amt"] = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Card_Amt"))) ? "0.0" : DataBinder.Eval(lstGridData[i], "DSL_Card_Amt")).ToString("N2");
                    dr["DSL_Card_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Card_Name"));
                    dr["DSL_Card_Number"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DSL_Card_Number"));
                    dr["Total_DSL_Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Total_DSL_Qty"));
                    dr["Total_DSL_Amt"] = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lstGridData[i], "Total_DSL_Amt"))) ? "0.0" : DataBinder.Eval(lstGridData[i], "Total_DSL_Amt")).ToString("N2");
                    dr["Cash"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Cash"));
                    dr["Toll"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Toll"));
                    dr["Wages"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Wages"));
                    dr["Food_Exp"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Food_Exp"));
                    dr["Repair"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Repair"));
                    dr["Adv_in_Driver"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Adv_in_Driver"));
                    dr["Other"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Other"));
                    dr["Milage"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Milage"));
                    dr["Net Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Total_Amt")).ToString("N2");
                    dr["Remark"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Remark"));
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                imgBtnExcel.Visible = true;
                
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                imgBtnExcel.Visible = false;
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
        private void BindCity()
        {
            //CityMastDAL obj = new CityMastDAL();
            //var lst = obj.SelectCityCombo();
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;

        }
        private void BindLorry()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNowithLastDigit();
            ddlLorry_No.DataSource = TruckNolst;
            ddlLorry_No.DataTextField = "Lorry_No";
            ddlLorry_No.DataValueField = "Lorry_IdNo";
            ddlLorry_No.DataBind();
            ddlLorry_No.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindLane()
        {
            CustomTripSheetDAL objDAL = new CustomTripSheetDAL();
            var objlist = objDAL.BindLane();
            ddlLane.DataSource = objlist;
            ddlLane.DataTextField = "Lane_Name";
            ddlLane.DataValueField = "Lane_Idno";
            ddlLane.DataBind();
            objDAL = null;
            ddlLane.Items.Insert(0, new ListItem("--- Select Lane ---", "0"));
        }
        private void bindsender()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            ddlSender.DataSource = senderLst;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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
        private void BindCityFrom()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            drpCityFrom.DataSource = ToCity;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindCityFrom(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void SetDate()
        {
            //Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            //FinYearDAL objDAL = new FinYearDAL();
            //var lst = objDAL.FilldateFromTo(intyearid);
            //hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            //hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            //if (ddlDateRange.SelectedIndex >= 0)
            //{
            //    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
            //    {
            //        Datefrom.Text = hidmindate.Value;
            //        Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //    }
            //    else
            //    {
            //        Datefrom.Text = hidmindate.Value;
            //        Dateto.Text = hidmaxdate.Value;
            //    }
            //}
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            Datefrom.Text = Convert.ToString(list.GetValue(0));
            Dateto.Text = Convert.ToString(list.GetValue(1));
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
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("CustomTripSheet.aspx?TripId=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                CustomTripSheetDAL obj = new CustomTripSheetDAL();
                Int32 intValue = obj.DeleteTrip(Convert.ToInt64(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtTripNo.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
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

        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
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
        }
    }
