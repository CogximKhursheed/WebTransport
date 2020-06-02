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
    public partial class ManageReceiptGoodsReceived : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
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
            if (!Page.IsPostBack)
            {
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
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                this.BindSenderReceiverAgent();

                ReceiptGoodsReceivedDAL Obj = new ReceiptGoodsReceivedDAL();
                lblTotalRecord.Text = "T. Record (s): " + Obj.Select_ReceiptGoodsCount();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            ReceiptGoodsReceivedDAL objclsCityMaster = new ReceiptGoodsReceivedDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int recptno = string.IsNullOrEmpty(Convert.ToString(txtReceiptNo.Text)) ? 0 : Convert.ToInt32(txtReceiptNo.Text);
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }
            int citto = Convert.ToInt32(drpCityTo.SelectedValue);
            int cityfrom = Convert.ToInt32(drpCityFrom.SelectedValue);
            int citydel = Convert.ToInt32(drpCityDelivery.SelectedValue);
            int sender = Convert.ToInt32(drpSender.SelectedValue);
            int receiver = Convert.ToInt32(drpReceiver.SelectedValue);
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            var lstGridData = objclsCityMaster.SelectReceiptGoods(recptno, dtfrom, dtto, cityfrom, citydel, citto, sender, receiver, yearidno, UserIdno);
            objclsCityMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("RcptNo", typeof(string));
                dt.Columns.Add("RcptDate", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("DelvCity", typeof(string));
                dt.Columns.Add("Sender", typeof(string));
                dt.Columns.Add("Receiver", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["RcptNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "RcptGoodHead_No"));
                    dr["RcptDate"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "RcptGoodHead_Date")).ToString("dd-MM-yyyy");
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityFrom"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                    dr["DelvCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityDely"));
                    dr["Sender"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Sender"));
                    dr["Receiver"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Receiver"));

                    dt.Rows.Add(dr);

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
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
                lblTotalRecord.Text = "T. Record (s): 0 ";
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
                Response.Redirect("RcptGoodsReceived.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
                Int32 intValue = obj.DeleteReceiptGoods(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
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

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string Idno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RcptGoodHead_Idno"));

                if (Idno != "")
                {
                    ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToString(Idno));
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion


        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ReciptGoodRecived.xls"));
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
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            drpCityFrom.DataSource = FrmCity;
            drpCityFrom.DataTextField = "CityName";
            drpCityFrom.DataValueField = "cityidno";
            drpCityFrom.DataBind();
            drpCityFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindSenderReceiverAgent()
        {
            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            var SenderReceiver = obj.SelectAcntMastByType(2);
            obj = null;
            drpSender.DataSource = SenderReceiver;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));

            drpReceiver.DataSource = SenderReceiver;
            drpReceiver.DataTextField = "Acnt_Name";
            drpReceiver.DataValueField = "Acnt_Idno";
            drpReceiver.DataBind();
            drpReceiver.Items.Insert(0, new ListItem("--Select--", "0"));


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
                    txtReceiptDatefrom.Text = hidmindate.Value;
                    txtReceiptDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtReceiptDatefrom.Text = hidmindate.Value;
                    txtReceiptDateto.Text = hidmaxdate.Value;
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

        #endregion
    }
}