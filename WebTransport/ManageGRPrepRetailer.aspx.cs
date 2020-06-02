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
    public partial class ManageGRPrepRetailer : Pagebase
    {
        #region Private Variable....
        private int intFormId = 27; double dGrossAmnt = 0, dNetAmnt = 0; DataTable ExportCSV = new DataTable();
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPrefixNum.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtGRNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");

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
                    imgBtnExcel.Visible = false;
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCityFrom();
                }
                else
                {
                    this.BindCityFrom(Convert.ToInt64(Session["UserIdno"]));
                }
               // drpCityFrom.SelectedValue = Convert.ToString(base.UserFromCity);
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                bindsender();

                GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
                DateTime? dtfrom = null;
                DateTime? dtto = null;
                Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
                int GrNo = string.IsNullOrEmpty(Convert.ToString(txtGRNo.Text)) ? 0 : Convert.ToInt32(txtGRNo.Text);
                //if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
                //{
                //    dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
                //{
                //    dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
                //}
                string MnNo = string.IsNullOrEmpty(Convert.ToString(txtManNo.Text)) ? "0" : Convert.ToString(txtManNo.Text);
                string strPrefixNum = txtPrefixNum.Text.Trim();
                int citto = Convert.ToInt32(drpCityTo.SelectedValue);
                int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
                int citydel = Convert.ToInt32(drpCityDelivery.SelectedValue);
                int senderr = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
                Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
                Int64 UserIdno = 0;
                if (Convert.ToString(Session["Userclass"]) != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }

                var lstGridData = obj.SelectGRRetailer(GrNo, dtfrom, dtto, cityfrom, citydel, citto, senderr, yearidno, UserIdno, strPrefixNum, MnNo);
                obj = null;
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                }
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
            GRPrepRetailerDAL objRet = new GRPrepRetailerDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int GrNo = string.IsNullOrEmpty(Convert.ToString(txtGRNo.Text)) ? 0 : Convert.ToInt32(txtGRNo.Text);
            string MnNo = string.IsNullOrEmpty(Convert.ToString(txtManNo.Text)) ? "" : Convert.ToString(txtManNo.Text);

            string strPrefixNum = txtPrefixNum.Text.Trim();

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            int citto = Convert.ToInt32(drpCityTo.SelectedValue);
            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int citydel = Convert.ToInt32(drpCityDelivery.SelectedValue);
            int sender = Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            var lstGridData = objRet.SelectGRRetailer(GrNo, dtfrom, dtto, cityfrom, citydel, citto, sender, yearidno, UserIdno, strPrefixNum,MnNo);
            objRet = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("GrNo", typeof(string));
                dt.Columns.Add("GrDate", typeof(string));
                dt.Columns.Add("GRType", typeof(string));
                dt.Columns.Add("Sender", typeof(string));
                dt.Columns.Add("Receiver", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("ViaCity", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("OwnerName", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));                
                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {                    
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["GrNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "GRRet_Pref")) + "" + Convert.ToString(DataBinder.Eval(lstGridData[i], "GrRet_No"));
                    dr["GrDate"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "GRRet_Date")).ToString("dd-MM-yyyy");
                    dr["GRType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "GR_Typ"));
                    dr["Sender"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Sender"));
                    dr["Receiver"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Receiver"));
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityFrom"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                    dr["ViaCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityVia"));
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["OwnerName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Owner_Name"));
                    dr["Qty"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Qty"));
                    dr["Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Gross_Amnt")).ToString("N2");
                    dr["NetAmount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amount")).ToString("N2");
                    dt.Rows.Add(dr);
                    TAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Gross_Amnt"));
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amount"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["ToCity"] = "Total";
                        drr["Amount"] = (TAmnt).ToString("N2");
                        drr["NetAmount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                Double TotalGrossNetAmount = 0;
                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalGrossNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Gross_Amnt"));
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amount"));

                }
                lblNetGrossTotalAmount.Text = TotalGrossNetAmount.ToString("N2");
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //grdprint.DataSource = lstGridData;
                //grdprint.DataBind();
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                //grdprint.DataSource = null;
                //grdprint.DataBind();
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
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
            //drpCityFrom.DataSource = ToCity;
            //drpCityFrom.DataTextField = "City_Name";
            //drpCityFrom.DataValueField = "City_Idno";
            //drpCityFrom.DataBind();
            //drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));

            drpCityTo.DataSource = ToCity;
            drpCityTo.DataTextField = "City_Name";
            drpCityTo.DataValueField = "City_Idno";
            drpCityTo.DataBind();
            drpCityTo.Items.Insert(0, new ListItem("--Select--", "0"));

            drpCityDelivery.DataSource = ToCity;
            drpCityDelivery.DataTextField = "City_Name";
            drpCityDelivery.DataValueField = "City_Idno";
            drpCityDelivery.DataBind();
            drpCityDelivery.Items.Insert(0, new ListItem("--Select--", "0"));

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
            if (ddlDateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int numDays = DateTime.DaysInMonth(year, month);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {                    
                    Datefrom.Text = Convert.ToString(hidmindate.Value);
                    Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");                    
                }
                else
                {
                    
                    Datefrom.Text = Convert.ToString(hidmindate.Value);
                    Dateto.Text = Convert.ToString(hidmaxdate.Value);
                    //Dateto.Text = hidmindate.Value;
                }
            }
            //BindDropdownDAL obj = new BindDropdownDAL();
            //Array list = obj.BindDate();
            //Datefrom.Text = Convert.ToString(list.GetValue(0));
            //Dateto.Text = Convert.ToString(list.GetValue(1));
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
                Response.Redirect("GrPrepRetailer.aspx?Gr=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
                Int32 intValue = obj.DeleteGR(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtGRNo.Focus();
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
            if (e.CommandName == "cmdexport")
            {
                GRPrepDAL obj = new GRPrepDAL();
                Int32 gr_id = Convert.ToInt32(e.CommandArgument);
                Int32 grtype = obj.SelectGrTpe(gr_id);
                ExportCSV = obj.GrCSVReport1(gr_id, grtype, ApplicationFunction.ConnectionString());

                obj = null;
                if (ExportCSV != null)
                {
                    grdReport.DataSource = ExportCSV;
                    grdReport.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "openModalGrdReport();", true);
                }
                else
                {
                    grdReport.DataSource = null;
                    grdReport.DataBind();
                    lblTotalRecord.Text = "Total Record (s): 0 ";
                }
                obj = null;
            }
            if (e.CommandName == "Pay")
            {
                GRPrepDAL obj = new GRPrepDAL();
                Int32 gr_id = Convert.ToInt32(e.CommandArgument);
                Int32 ChlnIdno = obj.SelectChlnIdno(gr_id);
                if (ChlnIdno > 0)
                    Response.Redirect("PaymentToOwn.aspx?ChlnIdno=" + ChlnIdno, true);
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblGridNo = (Label)e.Row.FindControl("lblGridNo");
                Int32 intGRIdno = Convert.ToInt32(lblGridNo.Text);
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                LinkButton lnkbtnSold = (LinkButton)e.Row.FindControl("lnkbtnSold");
                LinkButton lnkbtnchallan = (LinkButton)e.Row.FindControl("lnkbtnchallan");
                LinkButton lnkbtnPay = (LinkButton)e.Row.FindControl("lnkbtnPay");
                GRPrepRetailerDAL obj = new GRPrepRetailerDAL();
                Int64 ChallanNo = 0; ChallanNo = obj.CheckChallanDetails(intGRIdno);
                Int64 InvoiceNo = 0; InvoiceNo = obj.CheckInvoiceDetails(intGRIdno);
                
                obj = null;
                if (ChallanNo > 0)
                {
                    lnkbtnDelete.Visible = false;
                    lnkbtnchallan.Visible = true;
                    e.Row.ForeColor = System.Drawing.Color.Maroon;
                    lnkbtnSold.Visible = false;
                }

                else if (InvoiceNo > 0)
                {
                    lnkbtnDelete.Visible = false; lnkbtnchallan.Visible = false;
                    e.Row.ForeColor = System.Drawing.Color.Maroon;
                    lnkbtnSold.Visible = true;
                }
                else
                {
                    if (base.CheckUserRights(intFormId) == false)
                    {
                        Response.Redirect("PermissionDenied.aspx");
                    }
                    if (base.Delete == false)
                        lnkbtnDelete.Visible = false;
                    else
                        lnkbtnDelete.Visible = true;
                    lnkbtnchallan.Visible = false; lnkbtnSold.Visible = false;
                }
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