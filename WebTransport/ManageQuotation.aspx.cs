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
using System.Data;
namespace WebTransport
{
    public partial class ManageQuotation : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dNetAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtQutnDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtQutnDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtQutNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                    this.BindFromCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityFrom.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                this.BindSender();

                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                this.Countall();
                //this.binddate
                //selectectd
                //==0 or -1
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            QuotationDAL objclsCityMaster = new QuotationDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int QuNo = string.IsNullOrEmpty(Convert.ToString(txtQutNo.Text)) ? 0 : Convert.ToInt32(txtQutNo.Text);
            if (string.IsNullOrEmpty(Convert.ToString(txtQutnDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtQutnDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtQutnDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtQutnDateto.Text));
            }
            int citto = Convert.ToInt32(drpCityTo.SelectedValue);
            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int citydel = Convert.ToInt32(drpCityDelivery.SelectedValue);
            int sender = Convert.ToInt32(drpSender.SelectedValue == "" ? 0 : Convert.ToInt32(drpSender.SelectedValue));
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            var lstGridData = objclsCityMaster.SelectQuotation(QuNo, dtfrom, dtto, cityfrom, citydel, citto, sender, yearidno, UserIdno);
            objclsCityMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Sr No.", typeof(string));
                dt.Columns.Add("Quatation No.", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("From City", typeof(string));
                dt.Columns.Add("City To", typeof(string));
                dt.Columns.Add("Delivery Place", typeof(string));
                dt.Columns.Add("Sender", typeof(string));
                dt.Columns.Add("Net Amount", typeof(string));

                double TNet = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Sr No."] = Convert.ToString(i + 1);
                    dr["Quatation No."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "QuHead_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "QuHead_Date")).ToString("dd-MM-yyyy");
                    dr["From City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityFrom"));
                    dr["City To"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                    dr["Delivery Place"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityDely"));
                    dr["Sender"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Sender"));
                    dr["Net Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);

                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Sender"] = "Total";
                        drr["Net Amount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                grdprint.DataSource = lstGridData;
                grdprint.DataBind();

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
                lblTotalRecord.Text = "Total Record (s): 0 ";
                grdprint.DataSource = null;
                grdprint.DataBind();
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

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
                Response.Redirect("Quotation.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                QuotationDAL obj = new QuotationDAL();
                Int32 intValue = obj.DeleteQuotation(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtQutNo.Focus();
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

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt")); ;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = Convert.ToDouble((dNetAmnt)).ToString("N2");


            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            drpCityTo.DataSource = lst;
            drpCityTo.DataTextField = "City_Name";
            drpCityTo.DataValueField = "City_Idno";
            drpCityTo.DataBind();
            drpCityTo.Items.Insert(0, new ListItem("--Select--", "0"));

            drpCityDelivery.DataSource = lst;
            drpCityDelivery.DataTextField = "City_Name";
            drpCityDelivery.DataValueField = "City_Idno";
            drpCityDelivery.DataBind();
            drpCityDelivery.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Quotataion.xls"));
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

        private void BindFromCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            drpCityFrom.DataSource = lst;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindFromCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        public void BindSender()
        {
            LedgerAccountDAL obj = new LedgerAccountDAL();
            var lst = obj.FetchSender();
            obj = null;
            drpSender.DataSource = lst;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));
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
        public void Countall()
        {
            QuotationDAL objQuotation = new QuotationDAL();
            Int64 count = objQuotation.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
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
                    txtQutnDatefrom.Text = hidmindate.Value;
                    txtQutnDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtQutnDatefrom.Text = hidmindate.Value;
                    txtQutnDateto.Text = hidmaxdate.Value;
                }
            }
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
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }


    }
}