using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class PartyDetailReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCity();
                BindSenderName();
                BindDateRange();
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        private void BindSenderName()
        {
            PrtyDetlDAL obj = new PrtyDetlDAL();
            DataTable SenderName = obj.BindPartynew("BindPartynew", ApplicationFunction.ConnectionString());
            ddlParty.DataSource = SenderName;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        private void PrintGRPrep()
        {
            Repeater obj = new Repeater();

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

            #region CodeForPrint Details
            //DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            //dsReport.Tables[0].TableName = "GRPrint";
            DataSet PartyDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select ISNULL(AM.Acnt_Name,'')as Acnt_Name,(ISNULL(AM.Address1,'')+' '+ISNULL(AM.Address2,'')) as [Address],ISNULL(CM.City_Name,'') as City_Name,ISNULL(SM.State_Name,'')as State_Name from AcntMast AM inner join tblCityMaster CM on  AM.City_idno=CM.City_idno inner join tblStateMaster SM on Am.State_idno=SM.State_idno where am.Acnt_idno='" + Convert.ToString(ddlParty.SelectedValue) + "'");
            if (PartyDetl != null && PartyDetl.Tables.Count > 0 && PartyDetl.Tables[0].Rows.Count > 0)
            {
                lblAddresss.Text = PartyDetl.Tables[0].Rows[0]["Address"].ToString();
                lblStateName.Text = PartyDetl.Tables[0].Rows[0]["State_Name"].ToString();
                lblPartyName.Text = PartyDetl.Tables[0].Rows[0]["Acnt_Name"].ToString();
                lblCityname.Text = PartyDetl.Tables[0].Rows[0]["City_Name"].ToString();
                lblDateFrom.Text = txtDateFrom.Text.Trim();
                lblDateto.Text = txtDateTo.Text.Trim();
                //    valuelblnetAmnt.Text = string.Format("{0:0,0.00}", (dcmsn + dblty + dcrtge + dPF + dsuchge + dtax + dwges + dtoll + dtotlAmnt));
            }
            #endregion


        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PartyDetailReport.xls"));
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = (DataSet)ViewState["dt"];
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                Export(dt);
            }
        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {   
                this.BindGrid();
                PrintGRPrep();
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
        public void BindGrid()
        {
            string DateFrom = ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim());
            string DateTo = ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim());
            string PartyID = ddlParty.SelectedValue;
            PrtyDetlDAL obj = new PrtyDetlDAL();
            DataSet Ds = new DataSet();
            Ds = obj.SelectPartyDetailReport(DateFrom, DateTo, PartyID, ApplicationFunction.ConnectionString());
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                grdMain.DataSource = Ds;
                grdMain.DataBind();

                grdPrintDtl.DataSource = Ds;
                grdPrintDtl.DataBind();

                ViewState["dt"] = Ds;
                imgBtnExcel.Visible = true;
                lnkbtnPrint.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                grdPrintDtl.DataSource = null;
                grdPrintDtl.DataBind();

                imgBtnExcel.Visible = false;
                lnkbtnPrint.Visible = false;
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblTotalAmnt = (Label)e.Row.FindControl("lblTotalAmnt");
                Label lblTotalAt = (Label)e.Row.FindControl("lblTotalAt");
                Label lblT = (Label)e.Row.FindControl("lblT");
                if (Convert.ToString(lblTotal.Text).ToUpper() == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9999");
                }
                if (Convert.ToString(lblTotalAmnt.Text).ToUpper() == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9999");
                }
                if (Convert.ToString(lblTotalAt.Text).ToUpper() == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9999");
                }
                if (Convert.ToString(lblT.Text).ToUpper() == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9999");
                }
            }
        }
    }
}