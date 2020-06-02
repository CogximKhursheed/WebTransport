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
    public partial class ConsolidetedGrReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double GrossAmnt = 0.00;
        double SurChrgeAmnt = 0.00;
        double Wages = 0.00;
        double ServTax = 0.00;
        double NetAmnt = 0.00;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.View == false)
                //{
                //    lnkbtnPreview.Visible = true;
                //}


                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);

                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                
                TotalRecords();
            }


            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;

           
            drpBaseCity.Focus();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            GRPrep();
        }
        #endregion

        #region Bind Event...

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objGRDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objGRDAL = null;
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



            }
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


        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                CSVTable.Columns.Remove("GR_Idno");
                CSVTable.Columns.Remove("Cartg_Amnt");
                CSVTable.Columns.Remove("AgntComisn_Amnt");
                CSVTable.Columns.Remove("Bilty_Amnt");
                CSVTable.Columns.Remove("PF_Amnt");
                CSVTable.Columns["Gr_No"].ColumnName = "GR.No";
                CSVTable.Columns["Gr_Date"].ColumnName = "GR.Date";
                CSVTable.Columns["GR_Typ"].ColumnName = "GR.Type";
                CSVTable.Columns["Delivery_Place"].ColumnName = "Delivery Place";
                CSVTable.Columns["From_City"].ColumnName = "From City";
                CSVTable.Columns["Receiver"].ColumnName = "Reciver";
                CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";

                CSVTable.Columns["Surcrg_Amnt"].ColumnName = "Surcharge Amount";
                CSVTable.Columns["Wages_Amnt"].ColumnName = "Wages Amount";
                CSVTable.Columns["ServTax_Amnt"].ColumnName = "Service Tax";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amount";
                ExportDataTableToCSV(CSVTable, "ConsolidatedGrReport");
                Response.Redirect("GrReport.aspx");
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
        #endregion


        private void GRPrep()
        {
            Repeater obj = new Repeater();

            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (PanNo == "")
            {
                lblPanNo.Visible = false; lbltxtPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblPanNo.Visible = true; lbltxtPanNo.Visible = true;
            }
            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [SpConsolidatedGrReport] @Year=" + ddlDateRange.SelectedValue + ",@Location=" + drpBaseCity.SelectedValue + ",@GrNo='"+txtGrNo.Text+"'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lnkbtnPrint.Visible = true;
                tblPrint.Visible = true;
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["FromCity"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ToCity"]);
                lblSenderName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Consigner"]);
                lblRecvrName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Consignee"]);
                lblGrType.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRType"]);
                lblLorryType.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["LorryTYpe"]);
                lblLorryNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry_No"]);
                lblFrieght.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Freight"]).ToString("N2");
                lblServTax.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax"]).ToString("N2");
                lblUser.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["UserNam"]);
                if (dsReport.Tables["Table2"].Rows.Count > 0)
                {
                    tdChlnDetailsTable.Visible = true;
                    lblChlnNo.Text = Convert.ToString(dsReport.Tables[2].Rows[0]["Chln_No"]);
                    lblChlnDate.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");
                    lblChlnFromCity.Text = Convert.ToString(dsReport.Tables[2].Rows[0]["FrmCity"]);
                    lblChlnLorryNo.Text = Convert.ToString(dsReport.Tables[2].Rows[0]["Lorry_No"]);
                    lblChlnLorryType.Text = Convert.ToString(dsReport.Tables[2].Rows[0]["LorryType"]);
                }
                else
                {
                    tdChlnDetailsTable.Visible = false;
                }
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();

            }
            else
            {
                lnkbtnPrint.Visible = false;
                tblPrint.Visible = false;
            }
        }


    }
}

