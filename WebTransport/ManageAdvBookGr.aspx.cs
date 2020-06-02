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
using System.Collections;


namespace WebTransport
{
    public partial class ManageAdvBookGr : Pagebase
    {
        #region GlobalVariable
        double dNetAmnt = 0;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
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

                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                this.BindDropDown();
                ddlDateRange_SelectedIndexChanged(null, null);
                this.Countall();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    AdvBookGRDAL obj = new AdvBookGRDAL();
                    var lst = obj.SelectCityCombo();
                    obj = null;
                    drpBaseCity.DataSource = lst;
                    drpBaseCity.DataTextField = "City_Name";
                    drpBaseCity.DataValueField = "City_Idno";
                    drpBaseCity.DataBind();
                    drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
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

        #region Control Events...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        #endregion

        #region Functions...

        private void BindGrid()
        {
            AdvBookGRDAL obj = new AdvBookGRDAL();
            DateTime? FromDate = null;
            DateTime? ToDate = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 OrderNumber = string.IsNullOrEmpty(Convert.ToString(txtOrderNumb.Text)) ? 0 : Convert.ToInt32(txtOrderNumb.Text);
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                FromDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                ToDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }

            Int32 LocationTo = string.IsNullOrEmpty(drpCityTo.SelectedValue) ? 0 : Convert.ToInt32(drpCityTo.SelectedValue);
            Int32 Location = string.IsNullOrEmpty(drpBaseCity.SelectedValue) ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue);
            Int32 LocationDelivery = string.IsNullOrEmpty(drpCityDelivery.SelectedValue) ? 0 : Convert.ToInt32(drpCityDelivery.SelectedValue);
            Int32 SenderIdno = string.IsNullOrEmpty(ddlSender.SelectedValue) ? 0 : Convert.ToInt32(ddlSender.SelectedValue == "" ? 0 : Convert.ToInt32(ddlSender.SelectedValue));
            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int32 GRType = Convert.ToInt32(ddlGRType.SelectedValue);

            var lstGridData = obj.SelectAdvGRDetails(OrderNumber, FromDate, ToDate, Location, LocationDelivery, LocationTo, SenderIdno, YearIdno, GRType);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Order No", typeof(string));
                dt.Columns.Add("GR Type", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("To City", typeof(string));
                dt.Columns.Add("Del. Place", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Ref No", typeof(string));
                dt.Columns.Add("Stuff Date", typeof(string));
                dt.Columns.Add("Remark", typeof(string));
                dt.Columns.Add("Cont. No", typeof(string));
                dt.Columns.Add("Cont. Size", typeof(string));
                dt.Columns.Add("Cont Type", typeof(string));
                dt.Columns.Add("Bkg From", typeof(string));
                dt.Columns.Add("Bkg To", typeof(string));
                dt.Columns.Add("Port", typeof(string));

                Double TotalNetAmount = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    //for printing dt code start here
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Order No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AdvOrder_No"));
                    dr["GR Type"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "GRTYPENAME"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "AdvOrder_Date")).ToString("dd-MM-yyyy");
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Sender"));
                    dr["Location"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityFrom"));
                    dr["To City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                    dr["Del. Place"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityDely"));
                    dr["Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dr["Ref No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "RefNo"));
                    dr["Stuff Date"] = string.IsNullOrEmpty(DataBinder.Eval(lstGridData[i], "StuffDate").ToString())?"N/A": Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "StuffDate")).ToString("dd-MM-yyyy");
                    dr["Remark"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Remark"));
                    dr["Cont. No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ContNo"));
                    dr["Cont. Size"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ContSiz"));
                    dr["Cont Type"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ContType"));
                    dr["Bkg From"] = DataBinder.Eval(lstGridData[i], "BkgDateFrom")==null?"": Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "BkgDateFrom")).ToString("dd-MM-yyyy");
                    dr["Bkg To"] = DataBinder.Eval(lstGridData[i], "BkgDateTo")==null? "" : Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "BkgDateTo")).ToString("dd-MM-yyyy");
                    dr["Port"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Port"));

                    dt.Rows.Add(dr);

                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Del. Place"] = "Total";
                        drr["Amount"] = (TotalNetAmount).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
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

        private void SetDate()
        {
            //Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            //FinYearDAL objDAL = new FinYearDAL();
            //var lst = objDAL.FilldateFromTo(intyearid);
            //hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            //hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            //if (ddlDateRange.SelectedIndex >= 0)
            //{
            //    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
            //    {
            //        Datefrom.Text = hidmindate.Value;
            //        Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //    }
            //    else
            //    {
            //        Datefrom.Text = hidmindate.Value;
            //        Dateto.Text = hidmaxdate.Value;
            //    }
            //}
            BindDropdownDAL obj = new BindDropdownDAL();
            Array list = obj.BindDate();
            Datefrom.Text = Convert.ToString(list.GetValue(0));
            Dateto.Text = Convert.ToString(list.GetValue(1));

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

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "cityidno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindDropDown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            ddlSender.DataSource = senderLst;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var ToCity = obj.BindAllToCity();
            drpCityTo.DataSource = ToCity;
            drpCityTo.DataTextField = "city_name";
            drpCityTo.DataValueField = "city_idno";
            drpCityTo.DataBind();
            drpCityTo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            drpCityDelivery.DataSource = ToCity;
            drpCityDelivery.DataTextField = "city_name";
            drpCityDelivery.DataValueField = "city_idno";
            drpCityDelivery.DataBind();
            drpCityDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        public void Countall()
        {
            AdvBookGRDAL obj = new AdvBookGRDAL();
            Int64 count = obj.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + count;
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
                Response.Redirect("AdvBookGR.aspx?OrderID=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                AdvBookGRDAL obj = new AdvBookGRDAL();
                Int32 intValue = obj.DeleteGR(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                obj = null;
                if (intValue > 0)
                {
                    strMsg = "Record deleted successfully.";
                    this.ShowMessage(strMsg);

                    this.BindGrid();
                    txtOrderNumb.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";

                    this.ShowMessageErr(strMsg);
                }
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
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt")); ;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = Convert.ToDouble((dNetAmnt)).ToString("N2");
            }

            LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
            Int32  YearIdno=string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 AdvOrderGRIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AdvOrderGR_Idno"))) ? 0 : Convert.ToInt32(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AdvOrderGR_Idno")));
            if (AdvOrderGRIdno > 0)
            {
                AdvBookGRDAL obj = new AdvBookGRDAL();
                Int64 Exist = obj.IfExistsInGr(Convert.ToInt64(AdvOrderGRIdno), YearIdno, ApplicationFunction.ConnectionString());
                if (Exist > 0)
                {
                    lnkbtnDelete.Visible = false;
                }
                else
                {
                    lnkbtnDelete.Visible = true;
                }
            }

        }
        #endregion

        #region Prints...
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

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AdvanceGr.xls"));
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion
    }
}