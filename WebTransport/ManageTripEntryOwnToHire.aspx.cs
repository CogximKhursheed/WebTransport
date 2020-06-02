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
using System.Configuration;
using System.Data;
namespace WebTransport
{
    public partial class ManageTripEntryOwnToHire: Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dblTChallanAmnt = 0;
        // string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtReceiptNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
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

                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityFrom.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);
                this.BindTruckNo();
                txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                ddldateRange.Focus();
                this.BindGrid();
                grdMain.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
        }
        #endregion

        #region Functions...

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TripEntry.xls"));
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

        private void BindGrid()
        {
            TripEntryOwnTohireDAL obj = new TripEntryOwnTohireDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            String challanNo = txtReceiptNo.Text;
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }
            int cityfrom = Convert.ToInt32((drpCityFrom.SelectedIndex <= 0) ? "0" : drpCityFrom.SelectedValue);

            int TruckId = Convert.ToInt32((ddltruckNo.SelectedIndex <= 0) ? "0" : ddltruckNo.SelectedValue);

            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, cityfrom, TruckId, UserIdno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("TripNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["TripNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TripOTH_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "TripOTH_Date")).ToString("dd-MM-yyyy");
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["NetAmount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["LorryNo"] = "Total";
                        drr["NetAmount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                Double TotalNetAmount = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetAmnt.Text = TotalNetAmount.ToString("N2");

                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
                imgBtnExcel.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Text = "Showing 0 - 0 of 0";
                lblNetAmnt.Text = "0";
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
        }
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataSource = lst;
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
        }
        private void SetDate()
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtReceiptDatefrom.Text = hidmindate.Value;
                    txtReceiptDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtReceiptDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtReceiptDateto.Text = Convert.ToString(hidmaxdate.Value);
                }
            }
        }
        private void BindCity()
        {
            TripEntryDAL obj = new TripEntryDAL();
            var lst = obj.SelectCityCombo();
            obj = null;
            drpCityFrom.DataSource = lst;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTruckNo()
        {
            TripEntryOwnTohireDAL obj = new TripEntryOwnTohireDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddltruckNo.DataSource = lst;
                ddltruckNo.DataTextField = "Lorry_No";
                ddltruckNo.DataValueField = "Lorry_Idno";
                ddltruckNo.DataBind();

            }
            ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
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
                Response.Redirect("TripSheetOwnToHire.aspx?TripI=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                TripEntryOwnTohireDAL obj = new TripEntryOwnTohireDAL();
                //clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                  //  objclsAccountPosting.DeleteAccountPosting(Convert.ToInt32(e.CommandArgument), "TRP");
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtReceiptNo.Focus();
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
        double dblChallanAmnt = 0;
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                    dblTChallanAmnt = dblChallanAmnt + dblTChallanAmnt;
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotalNet = (Label)e.Row.FindControl("lblTotalNet");
                    lblTotalNet.Text = dblTChallanAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region Button Events...
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            grdMain.Visible = true;
            lblcontant.Visible = true;
            divpaging.Visible = true;
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
        #endregion

        #region Control Events...
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
        }
        #endregion
    }
}