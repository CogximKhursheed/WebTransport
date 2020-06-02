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
    public partial class ManageChlnAmntPayment : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dblNetAmnt = 0;
        //string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtRcptNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtGrDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtGrDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
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
                this.BindDateRange();
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                BindCity();
                this.Countall();
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
        //private void bindsender()
        //{
        //    BindDropdownDAL obj = new BindDropdownDAL();
        //    var senderLst = obj.BindSender();
        //    ddlSender.DataSource = senderLst;
        //    ddlSender.DataTextField = "Acnt_Name";
        //    ddlSender.DataValueField = "Acnt_Idno";
        //    ddlSender.DataBind();
        //    ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        //}
        public void Countall()
        {
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            Int64 count = obj.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ChlnAmntPayment.xls"));
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
            challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 InvoiceNo = Convert.ToInt64(Convert.ToString(txtRcptNo.Text) == "" ? 0 : Convert.ToInt64(txtRcptNo.Text));
            if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtGrDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGrDateto.Text));
            }
            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), InvoiceNo, dtfrom, dtto, Convert.ToInt32(drpCityFrom.SelectedValue), Convert.ToInt32(hfPartyNoId.Value == "" ? "0" : hfPartyNoId.Value));
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Payment No", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Party Name", typeof(string));
                dt.Columns.Add("Receipt Name", typeof(string));
                dt.Columns.Add("Receipt No", typeof(string));
                dt.Columns.Add("Receipt Date", typeof(string));
                dt.Columns.Add("From City", typeof(string));
                dt.Columns.Add("Net Amount", typeof(string));
                dt.AcceptChanges();
       
                double TNet = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Payment No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Rcpt_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Rcpt_date")).ToString("dd-MM-yyyy");
                    dr["Party Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["From City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCity"));
                    dr["Net Amount"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    dr["Receipt Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ReciptName"));
                    dr["Receipt No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Inst_No"));
                    dr["Receipt Date"] =string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lstGridData[i], "Inst_Dt"))) ? "" : Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Inst_Dt")).ToString("dd-MM-yyyy");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["From City"] = "Total";
                        drr["Net Amount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                dt.AcceptChanges();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;

                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
            //if (ddldateRange.SelectedIndex != -1)
            //{
            //    Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
            //    FinYearDAL objDAL = new FinYearDAL();
            //    var lst = objDAL.FilldateFromTo(intyearid);
            //    hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            //    hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            //    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
            //    {

            //        txtGrDatefrom.Text = Convert.ToString(hidmindate.Value);

            //        txtGrDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //    }
            //    else
            //    {

            //        txtGrDatefrom.Text = Convert.ToString(hidmindate.Value);
            //        txtGrDateto.Text = Convert.ToString(hidmaxdate.Value);

            //    }
            //}
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            txtGrDatefrom.Text = Convert.ToString(list.GetValue(0));
            txtGrDateto.Text = Convert.ToString(list.GetValue(1));
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] GetPartyNo(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();
            List<string> PartyNumber = new List<string>();
            DataTable dtNames = new DataTable();
            AccountBookDAL obj = new AccountBookDAL();
            DataSet dt = obj.SelectPartyList(prefixText, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Name"]), Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Idno"]));
                    PartyNumber.Add(item);
                }
                return PartyNumber.ToArray();
            }
            else
            {
                return null;
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
                Response.Redirect("ChlnAmntPayment.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                challanAmntpaymentDAL obj = new challanAmntpaymentDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument),UserIdno, ApplicationFunction.ConnectionString());
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
                dblNetAmnt = dblNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt")); ;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotRecvd = (Label)e.Row.FindControl("lblFTotRecvd");
                lblTotRecvd.Text = dblNetAmnt.ToString("N2");
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

        #region IMport excel....
        private void ExportGridView()
        {
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].GetType() == typeof(Label))
                {
                    l.Text = (gv.Controls[i] as Label).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
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
        #endregion
    }
}