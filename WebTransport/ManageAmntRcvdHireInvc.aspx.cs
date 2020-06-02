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
    public partial class ManageAmntRcvdHireInvc : Pagebase
    {
        #region Private Variable....
        private int intFormId = 33;
        //string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString; 
        double dNetAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtRcptNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtInvcDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtInvcDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
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
                this.BindDateRange();
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);

                RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
                var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), txtRcptNo.Text, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInvcDatefrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInvcDateto.Text)));
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                }
                ddldateRange.Focus();

            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            String InvoiceNo = txtRcptNo.Text;
            if (string.IsNullOrEmpty(Convert.ToString(txtInvcDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInvcDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtInvcDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInvcDateto.Text));
            }

            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), InvoiceNo, dtfrom, dtto);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("InvoiceNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                //dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("Sender", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["InvoiceNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Inv_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Inv_Date")).ToString("dd-MM-yyyy");
                    //dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCity"));
                    dr["Sender"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SenderName"));
                    dr["NetAmount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);

                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Sender"] = "Total";
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

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;


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


                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
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
        //    if (ddldateRange.SelectedIndex != -1)
        //    {
        //        Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
        //        FinYearDAL objDAL = new FinYearDAL();
        //        var lst = objDAL.FilldateFromTo(intyearid);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
        //            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
        //            if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
        //            {

        //                txtInvcDatefrom.Text = Convert.ToString(hidmindate.Value);

        //                txtInvcDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
        //            }
        //            else
        //            {
        //                txtInvcDatefrom.Text = Convert.ToString(hidmindate.Value);
        //                txtInvcDateto.Text = Convert.ToString(hidmaxdate.Value);
        //            }
        //        }
        //    }
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            txtInvcDatefrom.Text = Convert.ToString(list.GetValue(0));
            txtInvcDateto.Text = Convert.ToString(list.GetValue(1));
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AmountRecvAgainInvoice.xls"));
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
                Response.Redirect("AmntAgainstInvoiceOTH.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = 0;
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
                RcvdAmntAgnstHireInvcDAL obj = new RcvdAmntAgnstHireInvcDAL();
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
                        strMsg = "Record can not be deleted. It is in use!";
                    else
                        strMsg = "Record not deleted!";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }

        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dNetAmnt.ToString("N2");
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

        #region control Events
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