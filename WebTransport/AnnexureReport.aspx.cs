using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
namespace WebTransport
{
    public partial class AnnexureReport : Pagebase
    {
        #region Variable ...
        public List<ClsAnnexureReport> LstDsoReport = new List<ClsAnnexureReport>();
        string conString = "";
        static FinYear UFinYear = new FinYear();
        //AnnexureReportDAL objGRDAL = new AnnexureReportDAL();
        //private int intFormId = 45;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
                //base.AutoRedirect();
            //}
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }                
                //this.BindDelvrPlce();
                BindDateRange();
                //BindAnnexure();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
            }
            //txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            //txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            //ddlDelvPlce.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
            PrintRpt();
        }
        protected void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            Int64 billno = string.IsNullOrEmpty(txtBillNo.Text.Trim()) ? 0 : Convert.ToInt64(txtBillNo.Text.Trim());
            BindAnnexure(billno);
        }
        #endregion

        #region Bind Event...
        //private void BindDelvrPlce()
        //{
        //    AnnexureReportDAL obj = new AnnexureReportDAL();
        //    var DelvrPlace = obj.BindDelvryPlace();
        //    ddlDelvPlce.DataSource = DelvrPlace;
        //    ddlDelvPlce.DataTextField = "City_Name";
        //    ddlDelvPlce.DataValueField = "City_Idno";
        //    ddlDelvPlce.DataBind();
        //    ddlDelvPlce.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void BindGrid()
        {
            AnnexureReportDAL obj = new AnnexureReportDAL();
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            if (string.IsNullOrEmpty(txtBillNo.Text.Trim()))
            {
                ShowMessageErr("Invoice No cannot be blank.");
            }
            else
            {
                DataTable list = obj.SelectRep("Annexure", txtBillNo.Text.Trim(), ddlAnnexure.SelectedValue, intyearid, conString);
                if ((list != null) && (list.Rows.Count > 0))
                {
                    
                    if (PrintRpt() == true)
                    {
                        ViewState["CSVdt"] = list;
                        fillAnnexureRpt();
                        lnkbtnPrint.Visible = true;
                        imgBtnExcel.Visible = true;
                    }
                    ViewState["CSVdt"] = list;
                    grdMain.DataSource = list;
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): " + list.Rows.Count;
                    lnkbtnPrint.Visible = true;
                    imgBtnExcel.Visible = true;
                }
                else
                {
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    ViewState["CSVdt"] = null;
                    lblTotalRecord.Text= "Total Record (s): 0 ";
                    lnkbtnPrint.Visible = false;
                    imgBtnExcel.Visible = false;
                }
            }
        }
        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "cmdedit")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            //}
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
            //    SurChrgeAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Surcrg_Amnt"));
            //    Wages += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Wages_Amnt"));
            //    ServTax += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ServTax_Amnt"));
            //    NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            //}
            ////if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblGross = (Label)e.Row.FindControl("lblgross");
            //    lblGross.Text = GrossAmnt.ToString("N2");

            //    Label lblSurchge = (Label)e.Row.FindControl("lblSurchrge");
            //    lblSurchge.Text = SurChrgeAmnt.ToString("N2");

            //    Label lblWages = (Label)e.Row.FindControl("lblWages");
            //    lblWages.Text = Wages.ToString("N2");

            //    Label lblSerTax = (Label)e.Row.FindControl("lblServtax");
            //    lblSerTax.Text = ServTax.ToString("N2");

            //    Label lblNetAMnt = (Label)e.Row.FindControl("lblNetAmnt");
            //    lblNetAMnt.Text = NetAmnt.ToString("N2");
            //}  
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
            //if (ddlDateRange.SelectedIndex != 0)
            //{
            //    txtDateFrom.Text = hidmindate.Value;
            //    txtDateTo.Text = hidmaxdate.Value;
            //}
            //else
            //{
            //    txtDateFrom.Text = hidmindate.Value;
            //    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //}
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            CSVTable = (DataTable)ViewState["CSVdt"];
            ExportDataTableToCSV(CSVTable, "AnnexureReport");
            //Response.Redirect("GrReport.aspx");
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
        #endregion

        #region Print Annexure Report
        public class ClsAnnexureReport
        {
            public string SrNo { get; set; }
            public string LRNo { get; set; }
            public string LRDate { get; set; }
            public string OrderNo { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public string DINo { get; set; }
            public string TruckNo { get; set; }
            public string Destination { get; set; }
            public string Weight { get; set; }
            public string Rate { get; set; }
            public string Amount { get; set; }
            public string TollPlaza { get; set; }
            public string UL { get; set; }
            public string Shorteg { get; set; }
            public string Ann { get; set; }
        }
        private bool PrintRpt()
        {   
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = ""; string CompGSTNNo = ""; 
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            #region Company Details
            if (CompDetl.Tables[0] != null && CompDetl.Tables[0].Rows.Count > 0)
            {
                CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
                Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
                Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
                City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
                State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
                PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
                FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
                PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
                CompGSTNNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
                //SACCode = Convert.ToString(CompDetl.Tables[0].Rows[0]["SAP_No"]);
                lblCompanyname.Text = CompName;
                lblCompAdd1.Text = Add1;
                lblCompAdd2.Text = Add2;
                lblCompCity.Text = City;
                lblCompState.Text = State;
                lblCompPhNo.Text = PhNo;
                lblPanNo.Text = PanNo;
                lblGSTNNo.Text = CompGSTNNo;
                LblSACCode.Text = "996511";
                lblCompname.Text = "For M/S. " + CompName;
                if (PanNo == "")
                {
                    lblPanNo.Visible = false; 
                }
                else
                {
                    lblPanNo.Text = PanNo;
                    lblPanNo.Visible = true; 
                }
                #endregion
                
                return true;
            }
            else
            {
                return false;
            }
        }
        private void fillAnnexureRpt()
        {
            DataTable dt = (DataTable)ViewState["CSVdt"];
            if (dt != null && dt.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsAnnexureReport objannxurerpt = new ClsAnnexureReport();
                    objannxurerpt.SrNo = dt.Rows[i]["Sr No."].ToString();
                    objannxurerpt.LRNo = dt.Rows[i]["LR No"].ToString();
                    objannxurerpt.LRDate = dt.Rows[i]["LR Date"].ToString();
                    objannxurerpt.OrderNo = dt.Rows[i]["Order No"].ToString();
                    objannxurerpt.InvoiceNo = dt.Rows[i]["Invoice No"].ToString();
                    objannxurerpt.InvoiceDate = dt.Rows[i]["Invoice Date"].ToString();
                    objannxurerpt.DINo = dt.Rows[i]["DI No"].ToString();
                    objannxurerpt.TruckNo = dt.Rows[i]["Truck No"].ToString();
                    objannxurerpt.Destination = dt.Rows[i]["Destination"].ToString();
                    objannxurerpt.Weight = dt.Rows[i]["Weight"].ToString();
                    objannxurerpt.Rate = dt.Rows[i]["Rate"].ToString();
                    objannxurerpt.Amount = dt.Rows[i]["Amount"].ToString();
                    objannxurerpt.TollPlaza = dt.Rows[i]["TOLLPLAZA"].ToString();
                    objannxurerpt.UL = dt.Rows[i]["UL"].ToString();
                    objannxurerpt.Shorteg = dt.Rows[i]["SHORTEG"].ToString();
                    objannxurerpt.Ann = dt.Rows[i]["AnnexNo"].ToString();
                    LstDsoReport.Add(objannxurerpt);
                }
                
                PrintInvoice(txtBillNo.Text);

                double Weight = Convert.ToDouble(string.IsNullOrEmpty( Convert.ToString( dt.Compute("SUM(Weight)", string.Empty))) ? "0.00" : dt.Compute("SUM(Weight)", string.Empty));
                double Rate = Convert.ToDouble( string.IsNullOrEmpty(Convert.ToString( dt.Compute("SUM(Rate)", string.Empty))) ? "0.00" : dt.Compute("SUM(Rate)",string.Empty));
                double Amount = Convert.ToDouble( string.IsNullOrEmpty(Convert.ToString( dt.Compute("SUM(Amount)", string.Empty))) ? "0.00" : dt.Compute("SUM(Amount)",string.Empty));
                double TollPlaza = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(dt.Compute("SUM(TOLLPLAZA)", string.Empty))) ? "0.00" : dt.Compute("SUM(TOLLPLAZA)", string.Empty));
                double UL = Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString( dt.Compute("SUM(UL)", string.Empty))) ? "0.00" : dt.Compute("SUM(UL)",string.Empty));
                double SHORTEG = Convert.ToDouble( string.IsNullOrEmpty(Convert.ToString( dt.Compute("SUM(SHORTEG)", string.Empty))) ? "0.00" : dt.Compute("SUM(SHORTEG)",string.Empty));

                Lbltotalweight.Text = Weight.ToString("N2");
                //Lbltotalrate.Text = Rate.ToString("N2");
                Lbltotalamt.Text = Amount.ToString("N2");
                Lbltollplaza.Text = TollPlaza.ToString("N2");
                LblUL.Text = UL.ToString("N2");
                Lblshorteg.Text = SHORTEG.ToString("N2");

                double lsttotal = Convert.ToDouble(Lbltotalamt.Text);
                string[] str1 = lsttotal.ToString().Split('.');
                string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                lblword.Text = numbertoent + " Only";
            }
        }
        private void PrintInvoice(string billno )
        {
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spAnnexure] @ACTION='BillINVOICE', @BillNo='"+ billno+ "', @AnexureNo='"+ddlAnnexure.SelectedValue+"'"); 
            lblcontname.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
            lbladd1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address1"]);
            lblcadd2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address2"]);
            lbldis.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
            diccode.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Pin_Code"]);
            lblst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]) + "(CODE :-  " + Convert.ToString(dsReport.Tables[0].Rows[0]["GSTState_Code"]) + ")";
            lblgst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_GSTINNo"]);
            lblAnnexNo.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Annexure_No"]);
            lblUnit.Text=Convert.ToString(dsReport.Tables[0].Rows[0]["unit"]);
        }
        private void BindAnnexure(Int64 billno)
        {
            AnnexureReportDAL obj = new AnnexureReportDAL();
            var lst = obj.selectannexureno(billno);
            obj = null;
            if (lst != null && lst.Count > 0)
            {
                ddlAnnexure.Enabled = true;
                ddlAnnexure.DataSource = lst;
                ddlAnnexure.DataTextField = "Name";
                ddlAnnexure.DataValueField = "Value";
                ddlAnnexure.DataBind();
                ddlAnnexure.Items.Insert(0, new ListItem("--Select--", "0"));
            }
           if (lst == null && lst.Count == 0)
            {
                ddlAnnexure.DataSource = null;
                ddlAnnexure.DataBind();
                ddlAnnexure.Enabled = false;
            }
        }
        public string NumberToText(int number)
        {
            if (number == 0) return "Zero";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        #endregion

        protected void ddlAnnexure_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlAnnexure.SelectedValue == "0")
            //{
            //    txtBillNo.Text = string.Empty;
            //}
        }
    }
}

