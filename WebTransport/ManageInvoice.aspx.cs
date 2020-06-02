using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
namespace WebTransport
{
    public partial class ManageInvoice : Pagebase
    {
        #region Private Variable....
        private int intFormId = 31; double dblTInvoiceAmnt = 0;
        // string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtReceiptNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            tblUserPref obj = objInvoiceDAL.SelectUserPref();
            hidAdminApp.Value = Convert.ToString(obj.AdminApp_Inv);

            if (Convert.ToBoolean(hidAdminApp.Value) == true)
            {
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    grdMain.Columns[7].Visible = true;
                }
                else
                {
                    grdMain.Columns[7].Visible = false;
                }
            }
            else
            {
                grdMain.Columns[7].Visible = false;
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
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                Bind();
                this.CountTotalRecords();
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
        private void CountTotalRecords()
        {
            InvoiceDAL obj = new InvoiceDAL();

            lblTotalRecord.Text = "T. Record (s): " + obj.TotalRecords().ToString();
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.SelectCityCombo();
            obj = null;

            if (lst.Count > 0)
            {
                ddlFromCity.DataSource = lst;
                ddlFromCity.DataTextField = "City_Name";
                ddlFromCity.DataValueField = "City_Idno";
                ddlFromCity.DataBind();
                ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void Bind()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.selectSenderName();
            obj = null;
            if (lst.Count > 0)
            {
                ddlSenderName.DataSource = lst;
                ddlSenderName.DataTextField = "Acnt_Name";
                ddlSenderName.DataValueField = "Acnt_Idno";
                ddlSenderName.DataBind();
                ddlSenderName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void BindGrid()
        {
            InvoiceDAL obj = new InvoiceDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int32 challanNo = ((txtReceiptNo.Text) == "" ? 0 : Convert.ToInt32(txtReceiptNo.Text));
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }
            int SenderId = Convert.ToInt32((ddlSenderName.SelectedIndex != -1) ? (ddlSenderName.SelectedValue) : "0");
            int fromCity = Convert.ToInt32((ddlFromCity.SelectedIndex != -1) ? (ddlFromCity.SelectedValue) : "0");

            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), challanNo, dtfrom, dtto, SenderId, fromCity, ddlGrType.SelectedValue);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("InvoiceNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("Sender", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["InvoiceNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Inv_prefix")) + Convert.ToString(DataBinder.Eval(lstGridData[i], "Inv_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Inv_Date")).ToString("dd-MM-yyyy");
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "City_Name"));
                    dr["Sender"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
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
                lblNetTotalAmount.Text = (TNet).ToString("N2");

                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                grdprint.DataSource = lstGridData;
                grdprint.DataBind();
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
                lblTotalRecord.Text = "T. Record (s): 0 ";
                grdprint.DataSource = null;
                grdprint.DataBind();
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
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    BindDropdownDAL obj = new BindDropdownDAL();
                    Array list = obj.BindDate();
                    txtReceiptDatefrom.Text = Convert.ToString(list.GetValue(0));
                    txtReceiptDateto.Text = Convert.ToString(list.GetValue(1));
                }
                else
                {
                    txtReceiptDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtReceiptDateto.Text = Convert.ToString(hidmaxdate.Value);

                }
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice.xls"));
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
                Response.Redirect("Invoice.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                InvoiceDAL obj = new InvoiceDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString(), ddlGrType.SelectedValue);
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
                    LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    if (base.CheckUserRights(intFormId) == false)
                    {
                        Response.Redirect("PermissionDenied.aspx");
                    }
                    if (base.Delete == false)
                        imgBtnDelete.Visible = false;
                    else
                        imgBtnDelete.Visible = true;

                    dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                    dblTInvoiceAmnt = dblChallanAmnt + dblTInvoiceAmnt;
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkAdminApp");
                    chk.Checked = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Admin_Approval"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTChallanAmnt = (Label)e.Row.FindControl("lblTotNeAmnt");
                    lblTChallanAmnt.Text = dblTInvoiceAmnt.ToString("N2");
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
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }

            ddldateRange.Focus();
        }
        #endregion

        #region Control Events...
        protected void chkAdminApp_CheckedChanged(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            InvoiceDAL obj = new InvoiceDAL();
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox ddlchk = (CheckBox)grdMain.Rows[selRowIndex].FindControl("chkAdminApp");
            HiddenField Idno = (HiddenField)grdMain.Rows[selRowIndex].FindControl("hidInvIdno");
            Int64 value = obj.UpdateAdminApproval(string.IsNullOrEmpty(Idno.Value) ? 0 : Convert.ToInt32(Idno.Value), ddlchk.Checked);
            if (value > 0)
                strMsg = "Record Updated Successfully";
            else
                strMsg = "Record Not Updated";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }
        #endregion
    }
}