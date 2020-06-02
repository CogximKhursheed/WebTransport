using System;
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
using System.Drawing;

namespace WebTransport
{
    public partial class DailyReport : Pagebase
    {
        #region Variable ...

        string conString = "";
        static FinYear UFinYear = new FinYear();
        InvoiceRepDAL objInvcDAL = new InvoiceRepDAL();
        private int intFormId = 40;
        double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0, dConsignrServTaxAmount = 0, dNetAmount = 0, dGrAmt = 0, dTotQty = 0, dTotWeight = 0;
        DataTable CSVTable = new DataTable();
        string Query = " AND 1=1";
        System.Globalization.CultureInfo cul = new System.Globalization.CultureInfo("ru-RU");
        #endregion

        #region Page Load Event ...

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            // conString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
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
                this.BindSenderName();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }

                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                TotalRecords();
                InvoiceDAL objInvoiceDAL = new InvoiceDAL();
                tblUserPref obj = objInvoiceDAL.SelectUserPref();
                hidPrintType.Value = Convert.ToString(obj.InvPrint_Type);
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }

        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            drpSenderName.SelectedIndex = 0;
            txtInvoiceNo.Text = "";
            grdMain.DataSource = null;
            grdMain.DataBind();
            drpBaseCity.Focus();
        }


        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...

        private void BindSenderName()
        {
            InvoiceRepDAL obj = new InvoiceRepDAL();
            var SenderName = obj.BindSender();
            drpSenderName.DataSource = SenderName;
            drpSenderName.DataTextField = "Acnt_Name";
            drpSenderName.DataValueField = "Acnt_Idno";
            drpSenderName.DataBind();
            objInvcDAL = null;
            drpSenderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserId)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserId);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                InvoiceRepDAL obj = new InvoiceRepDAL();
                Int64 iInvTyp = (Convert.ToString(ddlInvType.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlInvType.SelectedValue));
                DataTable list1 = obj.SelectRep("SelectInvwiseRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), 0, 0, 0, UserIdno, iInvTyp, ApplicationFunction.ConnectionString());
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }
        private void BindGrid()
        {
            if (txtDateFrom.Text != "")
            {
                Query += " AND CONVERT(DATE,GR_Date)>='" + Convert.ToDateTime(txtDateFrom.Text.Trim(), cul).ToString("yyyy-MM-dd") + "'"; 
            }
            if (txtDateTo.Text != "")
            {
                Query += " AND CONVERT(DATE,GR_Date)<='" + Convert.ToDateTime(txtDateTo.Text.Trim(), cul).ToString("yyyy-MM-dd") + "'"; 
            }
            if (Convert.ToInt32(drpBaseCity.SelectedValue) > 0)
            {
                Query += " AND From_City=" + Convert.ToInt32(drpBaseCity.SelectedValue);
            }
            if (Convert.ToInt64(drpSenderName.SelectedValue) > 0)
            {
                Query += " AND Sender_Idno=" + Convert.ToInt64(drpSenderName.SelectedValue);
            }
         
            InvoiceRepDAL objDAL = new InvoiceRepDAL();
          
         
            String Inv = txtInvoiceNo.Text;
            Int64 date = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 city = Convert.ToInt64(drpBaseCity.SelectedValue);
            Int64 invIdno = objDAL.InvIdno(ApplicationFunction.ConnectionString(), date, city);
            PrintReport(date, city);
        }

        private void ShowHideColumns()
        {
            grdMain.Columns[19].Visible = true;
            grdMain.Columns[20].Visible = true;
            grdMain.Columns[21].Visible = true;
            grdMain.Columns[22].Visible = true;
            grdMain.Columns[23].Visible = true;
            grdMain.Columns[24].Visible = true;
            grdMain.Columns[18].Visible = true;
            grdMain.Columns[17].Visible = true;

            if (GetGSTType(txtDateTo.Text) == 0)
            {
                grdMain.Columns[19].Visible = false;
                grdMain.Columns[20].Visible = false;
                grdMain.Columns[21].Visible = false;
                grdMain.Columns[22].Visible = false;
                grdMain.Columns[23].Visible = false;
                grdMain.Columns[24].Visible = false;
            }
            else if (GetGSTType(txtDateTo.Text) > 0)
            {
                grdMain.Columns[18].Visible = false;
                grdMain.Columns[17].Visible = false;
            }
        }

        //Upadhyay #GST
        public int GetGSTType(string date)
        {
            if (date != "")
            {
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(date.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
        }

        //Upadhyay #GST
        private string GetGSTDate()
        {
            DateTime gstDate;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                gstDate = (from i in db.tblUserPrefs select i.GST_Date).FirstOrDefault();
                return gstDate.ToString("dd-MM-yyyy");
            }
        }

        private DataTable getInvoiceDetails(Int32 Inv_Idno)
        {
            string str = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            InvoiceRepDAL objclsInvoiceRepDAL = new InvoiceRepDAL();
            DataTable lst = objclsInvoiceRepDAL.selectInvoiceReportDetails("SelectInvDet", Inv_Idno, ApplicationFunction.ConnectionString());
            return lst;

        }

        #endregion

        #region Grid Event...

     
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
            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                CSVTable.Columns["Gr_No"].ColumnName = "GR NO";
                CSVTable.Columns["Gr_Date"].ColumnName = "GR DATE";
                CSVTable.Columns["Lorry_No"].ColumnName = "Truck No.";
                CSVTable.Columns["Ordr_No"].ColumnName = "Order No.";
                CSVTable.Columns["Inv_No"].ColumnName = "INVOICE NO.";
                CSVTable.Columns["Inv_Date"].ColumnName = "INV_DATE";
                CSVTable.Columns["DI_NO"].ColumnName = "DI.NO";
                CSVTable.Columns["Recivr_Name"].ColumnName = "PARTY";
                CSVTable.Columns["Delvry_Place"].ColumnName = "Destination";
                CSVTable.Columns["Weight"].ColumnName = "WEIGHT";
                CSVTable.Columns["Rate"].ColumnName = "RATE";
                CSVTable.Columns["TollTax_Amnt"].ColumnName = "TOLL";
                CSVTable.Columns["UL"].ColumnName = "U/L";
                CSVTable.Columns["SHORT_MT"].ColumnName = "SHORT MT";
                CSVTable.Columns["COMM"].ColumnName = "COMM";
                CSVTable.Columns["Amount"].ColumnName = "AMT.";
                CSVTable.Columns["Adv"].ColumnName = "ADV.";
                CSVTable.Columns["DIESEL"].ColumnName = "DEISEL";
                CSVTable.Columns["Owner_Name"].ColumnName = "OWNER NAME";
                CSVTable.Columns["PAN"].ColumnName = "PAN";
                CSVTable.Columns["Bill_No"].ColumnName = "BILL NO.";
                CSVTable.Columns["REMARK"].ColumnName = "REMARK";
                ExportDataTableToCSV(CSVTable, "DailyReport" + txtDateFrom.Text + "TO" + txtDateTo.Text);
                Response.Redirect("DailyReport.aspx");
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
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
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
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        //protected void lnkBtn_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('divPrint')", true);
        //}
        private void PrintReport(Int64 date, Int64 city)
        {
            DataTable list = null;
            DailyReportDAL obj = new DailyReportDAL();
            list = obj.SelectRep("REPORT", Query, conString);
           
            if ((list != null) && (list.Rows.Count > 0))
            {
                ViewState["CSVdt"] = list;
                grdMain.DataSource = list;
                grdMain.DataBind();
                imgBtnExcel.Visible = true;
                //lnkBtn.Visible = false;
                lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                //lnkBtn.Visible = false;
                imgBtnExcel.Visible = false;
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        #endregion
    }
}


