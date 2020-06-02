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
    public partial class ManageManulTripSheet : Pagebase
    {
        #region Private Variable....
        private int intFormId = 27; double dGrossAmnt = 0, dNetAmnt = 0; DataTable ExportCSV = new DataTable();
        #endregion

        #region Page Load...
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
                this.BindLorry();
                this.bindsender();

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
                prints.Visible = false;
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
        #endregion

        #region Functions...
        private void BindGrid()
        {
            ManualTripSheetDAL obj = new ManualTripSheetDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
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

            var lstGridData = obj.SelectTrip(TripNo, dtfrom, dtto, cityfrom, sender, yearidno, lorry_Idno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                prints.Visible = false;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                prints.Visible = false;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GrPrepation.xls"));
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
                Response.Redirect("ManualTripSheet.aspx?TripId=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                ManualTripSheetDAL obj = new ManualTripSheetDAL();
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