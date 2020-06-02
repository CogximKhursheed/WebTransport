﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class DeliveryRegisterReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double LocGrAmnt, crsngGrAmnt, dNetAmnt = 0;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpToCity.SelectedValue = Convert.ToString(base.UserFromCity);
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
       


        #endregion

        #region Bind Event...
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpToCity.DataSource = FrmCity;
            drpToCity.DataTextField = "CityName";
            drpToCity.DataValueField = "CityIdno";
            drpToCity.DataBind();
            objGRDAL = null;
            drpToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            GRRepDAL obj = new GRRepDAL();
            var FrmCity = obj.BindFromCity();
            drpToCity.DataSource = FrmCity;
            drpToCity.DataTextField = "City_Name";
            drpToCity.DataValueField = "City_Idno";
            drpToCity.DataBind();
            objGRDAL = null;
            drpToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        
        
        private void BindGrid()
        {

            ChallanDelverdDAL obj = new ChallanDelverdDAL();
            Int32 iTocityIdno = (drpToCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(drpToCity.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int32    UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt32(Session["UserIdno"]);
            }
            DataSet DsDetl = obj.SearchDelevryRegisterReport(Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                          Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iTocityIdno,Convert.ToInt32(ddlDateRange.SelectedValue), UserIdno,ApplicationFunction.ConnectionString());

            if ((DsDetl != null) && (DsDetl.Tables[0].Rows.Count > 0))
            {
                Double TotalNetAmountLocGRAmnt = 0;
                Double TotalNetAmountCrsngGRAmnt = 0;
                Double TotalNetAmount= 0;


                for (int i = 0; i < DsDetl.Tables[0].Rows.Count; i++)
                {
                    TotalNetAmountLocGRAmnt += Convert.ToDouble(DsDetl.Tables[0].Rows[i]["LocGR_Amnt"]);
                    TotalNetAmountCrsngGRAmnt += Convert.ToDouble(DsDetl.Tables[0].Rows[i]["CrsngGR_Amnt"]);
                    TotalNetAmount += Convert.ToDouble(DsDetl.Tables[0].Rows[i]["Net_Amnt"]);
                }
                lblNetTotalAmountLocGRAmnt.Text = TotalNetAmountLocGRAmnt.ToString("N2");
                lblNetTotalAmountCrsngGRAmnt.Text = TotalNetAmountCrsngGRAmnt.ToString("N2");
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                ViewState["CSVdt"] = DsDetl.Tables[0];
                grdMain.DataSource = DsDetl.Tables[0];
                grdMain.DataBind();
                imgBtnExcel.Visible = true;
                lblTotalRecord.Text = "Total Record (s): " + DsDetl.Tables[0].Rows.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsDetl.Tables[0].Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                imgBtnExcel.Visible = false;
                lblTotalRecord.Text = "Total Record (s): 0 ";
                
                lblcontant.Visible = false;
                divpaging.Visible = false;

            }
        }


        #endregion

        #region Grid Event...
        
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LocGrAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LocGR_Amnt"));
                crsngGrAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CrsngGR_Amnt"));
                dNetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblLocGRAmnt = (Label)e.Row.FindControl("lblLocGRAmnt");
                lblLocGRAmnt.Text = LocGrAmnt.ToString("N2");

                Label lblCrsngGRAmnt = (Label)e.Row.FindControl("lblCrsngGRAmnt");
                lblCrsngGRAmnt.Text = crsngGrAmnt.ToString("N2");
                Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblNetAmnt.Text = dNetAmnt.ToString("N2");
            }

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Date Range FinYear ...
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex != 0)
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = hidmaxdate.Value;
            }
            else
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            //GRRepDAL obj = new GRRepDAL();
            //Int64 iREcvrIDNO = (ddlRecvr.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlRecvr.SelectedValue));
            //Int64 iSenderIDNO = (ddlSender.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlSender.SelectedValue));
            //Int64 iDElvryIDNO = (ddlDelvPlce.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlDelvPlce.SelectedValue));
            //Int32 iGRTypIDNO = (ddlGRType.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlGRType.SelectedValue));
            //Int64 iFromCityIDNO = (drpToCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpToCity.SelectedValue));
            //DataTable list = obj.SelectRep("SelectRepCSV", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
            //              Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iREcvrIDNO,
            //              iSenderIDNO, iFromCityIDNO, iDElvryIDNO, iGRTypIDNO, conString);
            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {

                //SELECT  ROW_NUMBER() OVER (ORDER BY CONVERT(DATETIME,FM.Rcpt_Date,105)) as SNo,
                //ISNULL(FM.Rcpt_Date,'''') AS Rcpt_Date,ISNULL(FM.Rcpt_No,'''') AS Rcpt_No,
                //ISNULL(CM.City_Name,'''') AS City_Name,ISNULL(FM.Gr_No,0) AS GR_No,
                //ISNULL(AM.Acnt_Name,0) AS Acnt_Name,ISNULL(FM.Tot_Qty,0) AS Tot_Qty,
                //ISNULL(FM.Tot_Weight,0) AS Tot_Weight,
                //CONVERT(NUMERIC(25,2),ISNULL(FM.Freight_Amnt,0)) AS Freight_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Service_Amnt,0)) AS Service_Amnt,   
                //CONVERT(NUMERIC(25,2),ISNULL(FM.Labour_Amnt,0)) AS Labour_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Delivery_Amnt,0)) AS Delivery_Amnt,
                //CONVERT(NUMERIC(25,2),ISNULL(FM.Octrai_Amnt,0)) AS Octrai_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Damage_Amnt,0)) AS Damage_Amnt,
                //CONVERT(NUMERIC(25,2),ISNULL(FM.Net_Amnt,0)) AS Net_Amnt,ISNULL(FM.FMemo_Idno,0) AS Id

                CSVTable.Columns["Reg_Date"].ColumnName = "Date";
                CSVTable.Columns["Reg_No"].ColumnName = "Rcpt No";
                CSVTable.Columns["Tocity"].ColumnName = "To City";
                CSVTable.Columns["LocGR_Amnt"].ColumnName = "Local GR Amount";
                CSVTable.Columns["CrsngGR_Amnt"].ColumnName = "Crosing GR Amount";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amount";
                ExportDataTableToCSV(CSVTable, "Delivery Register" + txtDateFrom.Text + "TO" + txtDateTo.Text);
                Response.Redirect("DeliveryRegisterReport.aspx");
            }
        }


        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }
    }
}

