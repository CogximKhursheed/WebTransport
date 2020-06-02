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
    public partial class ManageAmntRcvdGR : Pagebase
    {
        #region Private Variable....
        private int intFormId = 32;
        double dblNetAmnt = 0;
        //  string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtRcptNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtGrDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtGrDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            if (!Page.IsPostBack)
            {
                this.BindDateRange();
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                DateTime? dtfrom = null;
                DateTime? dtto = null;
                Int64 GrNo = Convert.ToInt64((txtRcptNo.Text) == "" ? "0" : txtRcptNo.Text);
                if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
                {
                    dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDatefrom.Text));
                }
                if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
                {
                    dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDateto.Text));
                }

                int cityfrom = Convert.ToInt32((drpCityFrom.SelectedIndex != -1) ? "0" : drpCityFrom.SelectedValue);
                var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), GrNo, dtfrom, dtto, cityfrom);
                obj = null;
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                }
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 GrNo = Convert.ToInt64((txtRcptNo.Text) == "" ? "0" : txtRcptNo.Text);
            if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDateto.Text));
            }

            int cityfrom = Convert.ToInt32((drpCityFrom.SelectedIndex ==0) ? "0" : drpCityFrom.SelectedValue);
            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), GrNo, dtfrom, dtto, cityfrom);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("RecpNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["RecpNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Rcpt_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Rcpt_date")).ToString("dd-MM-yyyy");
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCity"));
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["NetAmount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);

                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Party"] = "Total";
                        drr["NetAmount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);

                        lblNetTotalAmount.Text = TNet.ToString("N2");
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): 0 ";
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Amount_Against_GR.xls"));
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

                    txtGrDatefrom.Text = Convert.ToString(hidmindate.Value);

                    txtGrDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {

                    txtGrDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtGrDateto.Text = Convert.ToString(hidmaxdate.Value);

                }
            }
        }

        private void BindCity()
        {
            CityMastDAL obj = new CityMastDAL();
            var lst = obj.SelectCityCombo();
            obj = null;
            drpCityFrom.DataSource = lst;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));
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
                Response.Redirect("AmntAgainstGr.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                RcvdAmntAgnstGRDAL obj = new RcvdAmntAgnstGRDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtRcptNo.Focus();
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
            double dblChallanAmnt = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dblNetAmnt.ToString("N2");

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
        #endregion

        #region Control Events.........................................

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