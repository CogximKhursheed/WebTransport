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

namespace WebTransport
{
    public partial class TdsReport : Pagebase
    {
        #region Variable ...
        //  string conString = "";
        static FinYear UFinYear = new FinYear();
        LorryMasterRepDAL objLorryDAL = new LorryMasterRepDAL();
        private int intFormId = 51;
        int dVehicleNo = 0, dPANHolder = 0;
        double dTDSAmount = 0, dFrieghtAmount = 0;
        DataTable CSVTable = new DataTable();
        //double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0,dConsignrServTaxAmount=0, dNetAmount = 0;
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
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

                BindDateRange();
                SetDate();
                BindPartyName();
            }
            txtPan.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();

        }
        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            txtVehicleNo.Text = "";

            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlDateRange.Focus();

        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion

        #region Bind Event...

        private void BindGrid()
        {
            try
            {
                TdsReportDAL obj = new TdsReportDAL(); Int32 dt1count = 0, dt2count = 0; double dfreight = 0, dTDSamnt = 0, dBiltyComm = 0;
                string iVehicleNo = (Convert.ToString(txtVehicleNo.Text) == "" ? "" : Convert.ToString(txtVehicleNo.Text));
                Int32 Type = Convert.ToInt32(ddltype.SelectedValue);
                Int64 PartyIdno =(ddlParty.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlParty.SelectedValue));
                string PanNo = Convert.ToString(txtPan.Text.Trim());
                var DsGrdetail = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iVehicleNo,Type,PartyIdno,PanNo,ApplicationFunction.ConnectionString());
                if (DsGrdetail != null && DsGrdetail.Rows.Count > 0)
                {
                    DataTable dt1 = SelectDistinct("[CheckValue]", DsGrdetail, "VehicleNo");
                    dt1count = dt1.Rows.Count;
                    DataTable dt2 = SelectDistinct("[CheckValue]", DsGrdetail, "PANHolderName");
                    dt2count = dt2.Rows.Count;

                    var row1 = DsGrdetail.NewRow();
                    row1["SNO"] = "";
                    row1["Challan_No"] = "";
                    row1["Challan_Date"] = Convert.ToString("Total Vehicle");
                    row1["GRNo"] = "";
                    row1["VehicleNo"] = "";
                    row1["FrieghtAmount"] = "";
                    row1["PANNO"] = "";
                    row1["PANHolderName"] = "";
                    row1["TDSAmount"] = "";
                    //row1["Bilty_Commission"] = "";
                    DsGrdetail.Rows.Add(row1);

                    var row = DsGrdetail.NewRow();
                    row["SNO"] = "";
                    row["Challan_No"] = "";
                    row["Challan_Date"] = "Total PAN Holder";
                    row["GRNo"] = "";
                    row["VehicleNo"] = "";
                    row["FrieghtAmount"] = "";
                    row["PANNO"] = "";
                    row["PANHolderName"] = "";
                    row["TDSAmount"] = "";
                    //row["Bilty_Commission"] = "";
                    DsGrdetail.Rows.Add(row);

                    var row2 = DsGrdetail.NewRow();
                    row2["SNO"] = "";
                    row2["Challan_No"] = "";
                    row2["Challan_Date"] = "Total Amount";
                    row2["GRNo"] = "";
                    row2["VehicleNo"] = "";
                    row2["FrieghtAmount"] = "";
                    row2["PANNO"] = "";
                    row2["PANHolderName"] = "";
                    row2["TDSAmount"] = "";
                    //row2["Bilty_Commission"] = "";
                    DsGrdetail.Rows.Add(row2);



                    for (int i = 0; i < DsGrdetail.Rows.Count; i++)
                    {
                        dfreight = dfreight + Convert.ToDouble(Convert.ToString(DsGrdetail.Rows[i]["FrieghtAmount"]) == "" ? 0 : Convert.ToDouble(DsGrdetail.Rows[i]["FrieghtAmount"]));
                        dTDSamnt = dTDSamnt + Convert.ToDouble(Convert.ToString(DsGrdetail.Rows[i]["TDSAmount"]) == "" ? 0 : Convert.ToDouble(DsGrdetail.Rows[i]["TDSAmount"]));
                        //dBiltyComm= dBiltyComm + Convert.ToDouble(Convert.ToString(DsGrdetail.Rows[i]["Bilty_Commission"]) == "" ? 0 : Convert.ToDouble(DsGrdetail.Rows[i]["Bilty_Commission"]));
                    }

                    foreach (DataRow dtrow in DsGrdetail.Rows)
                    {

                        if (Convert.ToString(dtrow["Challan_Date"]) == Convert.ToString("Total Vehicle"))
                        {
                            dtrow["GRNo"] = dt1count; dtrow["SNO"] = "";
                        }
                        if (Convert.ToString(dtrow["Challan_Date"]) == Convert.ToString("Total PAN Holder"))
                        {
                            dtrow["GRNo"] = dt2count; dtrow["SNO"] = "";
                        }
                        if (Convert.ToString(dtrow["Challan_Date"]) == Convert.ToString("Total Amount"))
                        {
                            dtrow["FrieghtAmount"] = dfreight.ToString("N2"); dtrow["SNO"] = "";
                            dtrow["TDSAmount"] = dTDSamnt.ToString("N2");
                            //dtrow["Bilty_Commission"] = dBiltyComm.ToString("N2");
                        }
                    }

                    ViewState["CSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();
                    // printRep.Visible = true;
                    imgBtnExcel.Visible = true;

                    lblTotalRecord.Text = "T. Record (s): " + DsGrdetail.Rows.Count.ToString();
                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                }
                else
                {
                    ViewState["CSV"] = null;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    // printRep.Visible = false;
                    imgBtnExcel.Visible = false;
                    lblTotalRecord.Text = "T. Record (s): 0 ";
                    lblcontant.Visible = false;
                    divpaging.Visible = false;

                }

            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }

        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //lblDate

                Label lblDate = (Label)e.Row.FindControl("lblDate");


                if (lblDate.Text == "Total Amount")
                {
                    e.Row.BackColor = System.Drawing.Color.SkyBlue;
                }
                //dVehicleNo = dVehicleNo + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "VehicleNo"));
                //dPANHolder = dPANHolder + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PANHolderName"));

                // dFrieghtAmount = dFrieghtAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "FrieghtAmount"));
                //  dTDSAmount = dTDSAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TDSAmount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lblVehicleNo = (Label)e.Row.FindControl("lblVehicleNo");
                //lblVehicleNo.Text = dVehicleNo.ToString("N2");
                //Label lblPANHolder = (Label)e.Row.FindControl("lblPANHolder");
                //lblPANHolder.Text = dPANHolder.ToString("N2");




                //  Label lblFrieghtAmount = (Label)e.Row.FindControl("lblFrieghtAmount");
                //  lblFrieghtAmount.Text = dFrieghtAmount.ToString("N2");
                // Label lblTDSAmount = (Label)e.Row.FindControl("lblTDSAmount");
                // lblTDSAmount.Text = dTDSAmount.ToString("N2");
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
            CSVTable = (DataTable)ViewState["CSV"];
            for (int i = 0; i < CSVTable.Columns.Count; i++)
            {
                CSVTable.Rows[CSVTable.Rows.Count - 1][i] = Convert.ToString(CSVTable.Rows[CSVTable.Rows.Count - 1][i]).Replace(",","");
            }
            CSVTable.Columns["PrtyName"].SetOrdinal(6);
            CSVTable.Columns["SNO"].ColumnName = "S No.";
            CSVTable.Columns["Challan_No"].ColumnName = "Challan No.";
            CSVTable.Columns["Challan_Date"].ColumnName = "Challan Date";
            CSVTable.Columns["GRNo"].ColumnName = "Gr No.";
            CSVTable.Columns["PrtyName"].ColumnName = "Party Name";
            CSVTable.Columns["PANNO"].ColumnName = "Pan No";
            CSVTable.Columns["PANHolderName"].ColumnName = "Pan Holder";
            ExportDataTableToCSV(CSVTable, "TDS_Report" + txtDateFrom.Text + "_To_" + txtDateTo.Text);
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
        #endregion
        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            DataTable dt = new DataTable(TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            return dt;
        }
        private bool ColumnEqual(object A, object B)
        {

            if (A == DBNull.Value && B == DBNull.Value)
                return true;
            if (A == DBNull.Value || B == DBNull.Value)
                return false;
            return (A.Equals(B));

        }
        public List<AcntMast> BindPrty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
        private void BindPartyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 || cm.Acnt_Type == 5 || cm.Acnt_Type == 6 orderby cm.Acnt_Name ascending select cm).ToList();


                var SenderName = lst;
                ddlParty.DataSource = SenderName;
                ddlParty.DataTextField = "Acnt_Name";
                ddlParty.DataValueField = "Acnt_Idno";
                ddlParty.DataBind();

                ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
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

