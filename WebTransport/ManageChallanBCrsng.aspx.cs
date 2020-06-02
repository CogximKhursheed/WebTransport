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
    public partial class ManageChallanBCrsng : Pagebase
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
                //this.BindState();
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
                this.Countall();
                this.BindTruckNo();
                txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
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
        public void Countall()
        {
            ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
            Int64 count = obj.CountALL();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindGrid()
        {
            ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
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
            //  int delvPlace = Convert.ToInt32(ddlDelvPlacce.SelectedValue);
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
                dt.Columns.Add("ChlnDate", typeof(string));
                dt.Columns.Add("ChlnNo", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("NetAmnt", typeof(string));

                double TNet = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Convert.ToString(i + 1);
                    dr["ChlnDate"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Chln_Date")).ToString("dd-MM-yyyy");
                    dr["ChlnNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Chln_No"));
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCity"));
                    dr["NetAmnt"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["ChlnDate"] = "";
                        drr["ChlnNo"] = "";
                        drr["LorryNo"] = "";
                        drr["FromCity"] = "Total";
                        drr["NetAmnt"] = (TNet).ToString("N2");
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

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ChallanCrsngBooking.xls"));
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
            CityMastDAL obj = new CityMastDAL();
            var lst = obj.SelectCityCombo();
            obj = null;
            drpCityFrom.DataSource = lst;
            drpCityFrom.DataTextField = "City_Name";
            drpCityFrom.DataValueField = "City_Idno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new ListItem("--Select--", "0"));

            //ddlDelvPlacce.DataSource = lst;
            //ddlDelvPlacce.DataTextField = "City_Name";
            //ddlDelvPlacce.DataValueField = "City_Idno";
            //ddlDelvPlacce.DataBind();
            //ddlDelvPlacce.Items.Insert(0, new ListItem("--Select--", "0"));



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
            ChlnBookingDAL obj = new ChlnBookingDAL();
            var lst = obj.selectTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddltruckNo.DataSource = lst;
                ddltruckNo.DataTextField = "Lorry_No";
                ddltruckNo.DataValueField = "Lorry_Idno";
                ddltruckNo.DataBind();
                ddltruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
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
                Response.Redirect("ChlnBookingCrsng.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                ChlnBookingDAL obj = new ChlnBookingDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString(),"GR");
                obj = null;
                if (intValue > 0)
                {
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
            //else if (e.CommandName == "cmdstatus")
            //{
            //    int intCityIdno = 0;
            //    bool bStatus = false;
            //    string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
            //    if (strStatus.Length > 1)
            //    {
            //        intCityIdno = Convert.ToInt32(strStatus[0]);
            //        if (Convert.ToBoolean(strStatus[1]) == true)
            //            bStatus = false;
            //        else
            //            bStatus = true;
            //        CityMastDAL objclsCityMaster = new CityMastDAL();
            //        int value = objclsCityMaster.UpdateStatus(intCityIdno, bStatus);
            //        objclsCityMaster = null;
            //        if (value > 0)
            //        {
            //            this.BindGrid();
            //            strMsg = "Status updated successfully.";
            //            drpState.Focus();
            //        }
            //        else
            //        {
            //            strMsg = "Status not updated.";
            //        }
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            //    }
            //}
            //drpState.Focus();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                double dblChallanAmnt = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
                    Int64 ChlnIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Chln_Idno"));
                    if ((obj.CheckBilled(ChlnIdno, ApplicationFunction.ConnectionString())) > 0)
                    {
                        LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                        lnkbtnDelete.Visible = false;
                        e.Row.ForeColor = System.Drawing.Color.Maroon;
                    }
                    obj = null;

                    dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                    dblTChallanAmnt = dblChallanAmnt + dblTChallanAmnt;
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                    lblTChallanAmnt.Text = dblTChallanAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
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
            ddldateRange.Focus();
        }
        #endregion

        #region IMport excel....
        private void ExportGridView()
        {
            //string attachment = "attachment; filename=Report.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.Columns[1].Visible = false;
            //grdMain.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

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
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[1].Visible = false;
        }
        #endregion
    }
}