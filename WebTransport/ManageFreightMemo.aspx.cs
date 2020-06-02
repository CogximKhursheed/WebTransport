using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WebTransport.DAL;
using System.Collections.Generic;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class ManageFreightMemo : Pagebase
    {
        #region Private Variable...
        string conString = string.Empty; double dNetAmnt = 0;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            SessionValues svalue = new SessionValues();
            //  base.UsessionValue = svalue.FetchUsersAndComp(string.IsNullOrEmpty(Convert.ToString(Session["Visageuseridno"])) ? 0 : Convert.ToInt32(Session["Visageuseridno"]));
            // conString = ConfigurationManager.ConnectionStrings["VisageConnectionString"].ToString();
            svalue = null;
            if (!Page.IsPostBack)
            {
                this.Title = "Manage Freight Memo";
                this.BindDateRange();
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                FreightMemoDAL objChallanDelverdDAL = new FreightMemoDAL();
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Countall();
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void btnTravellBillSearch_Click(object sender, EventArgs e)
        {
            Populate();
        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            Populate();
        }
        protected void btnResign_Click(object sender, EventArgs e)
        {
            //UserBLL userBLL = new UserBLL();
            //int value = userBLL.UpdateUserResign(Convert.ToInt32(hidstaffid.Value), Convert.ToDateTime(txtResignDate.Text), txtRemarks.Text);
            //userBLL = null;
            //if (value > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showmsg", "PassMessage('Record updated successfully!')", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showresign", "ShowModalPopup();", true);
            //}
        }

        #endregion

        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FreightMemo.xls"));
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
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                ddlTocity.DataSource = lst;
                ddlTocity.DataTextField = "City_Name";
                ddlTocity.DataValueField = "City_Idno";
                ddlTocity.DataBind();

            }
            ddlTocity.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlTocity.DataSource = FrmCity;
            ddlTocity.DataTextField = "CityName";
            ddlTocity.DataValueField = "cityidno";
            ddlTocity.DataBind();
            ddlTocity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        public void Countall()
        {
            FreightMemoDAL obj = new FreightMemoDAL();

            Int64 count = obj.Countall();
            if (count > 0)
            {
                lbltotalstaff.Text = "T. Record (s):" + count;
            }
        }
        private void Populate()
        {
            string action = string.Empty;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            string transferno = ""; Int32 rcptno = 0;
            int yearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
            if (txtDateFrom.Text != "")
            {

                dateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            }
            else
            {
                dateFrom = null;
            }
            if (txtDateTo.Text != "")
            {

                dateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            else
            {
                dateTo = null;
            }
            rcptno = (Convert.ToString(txtRcptNo.Text) != "" ? Convert.ToInt32(txtRcptNo.Text) : 0);
            Int32 ToCity = Convert.ToInt32(ddlTocity.SelectedValue);
            FreightMemoDAL objChallanDelverdDAL = new FreightMemoDAL();
            var lst = objChallanDelverdDAL.Search(dateFrom, dateTo, rcptno, ToCity, yearIdno);
            objChallanDelverdDAL = null;
            grdUser.DataSource = lst;
            grdUser.DataBind();
            lbltotalstaff.Text = "T. Record (s): " + lst.Count;

            if (lst != null && lst.Count > 0)
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("RecptDate", typeof(string));
                dt.Columns.Add("RecpNo", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));
              

                double TNet = 0;
                for (int i = 0; i < lst.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["RecptDate"] = Convert.ToDateTime(DataBinder.Eval(lst[i], "Rcpt_Date")).ToString("dd-MM-yyyy");
                    dr["RecpNo"] = Convert.ToString(DataBinder.Eval(lst[i], "Rcpt_No"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lst[i], "City_Name"));
                    dr["NetAmount"] = Convert.ToString(DataBinder.Eval(lst[i], "Net_Amnt"));
              
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(DataBinder.Eval(lst[i], "Net_Amnt"));
                    if (i == lst.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["ToCity"] = "Total";
                        drr["NetAmount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                Double TotalNetAmount = 0;

                for (int i = 0; i < lst.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lst[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdUser.PageIndex * grdUser.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdUser.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lst.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
                imgBtnExcel.Visible = true;
            }
            else
            {
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void BidFromCity()
        {
            ChallanDelverdDAL ObjChallanDelverdDAL = new ChallanDelverdDAL();
            var lst = ObjChallanDelverdDAL.BindFromCity();
            ObjChallanDelverdDAL = null;
            ddlTocity.DataSource = lst;
            ddlTocity.DataTextField = "City_Name";
            ddlTocity.DataValueField = "City_Idno";
            ddlTocity.DataBind();
            ddlTocity.Items.Insert(0, new ListItem("< Select >", "0"));
        }

        #endregion

        #region Grid Events...
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dNetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));

                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dNetAmnt.ToString("N2");
            }
        }
        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("FreightMemo.aspx?Freightidno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                FreightMemoDAL objChallanDelverdDAL = new FreightMemoDAL();
                long value = objChallanDelverdDAL.DeleteALL(Convert.ToInt64(e.CommandArgument));
                objChallanDelverdDAL = null;
                if (value > 0)
                {
                    this.Populate();
                }
            }
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            this.Populate();
        }
        protected void grdUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Date Range FinYear
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataSource = lst;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                SetDate();
            }

        }
        private void SetDate()
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                }
            }
        }
        #endregion

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
        }

    }
}
