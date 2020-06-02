using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Configuration;
namespace WebTransport
{
    public partial class PaymentReceived : Pagebase
    {
        static FinYearA UFinYear = new FinYearA();
        string constring = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
        int iFinYrID;
        DataTable DtTemp = new DataTable(); double dTotRevdAmnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                //this.GetQutionNo();
                txtRecDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindDateRange();
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlDateRange.SelectedIndex = 0;
                this.ChallanDetail();
                if (Request.QueryString["q"] != null)
                {
                    hidPaymentid.Value = Convert.ToString(Request.QueryString["q"]);
                    ddlDateRange.Enabled = false;
                }
                else
                {
                   
                    ddlDateRange.Enabled = true;
                }

                txtReceivedAmount.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtSummaryNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtRemark.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
            }
            Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
        }


        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {

            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp == null && DtTemp.Rows.Count > 0)
            {
                ShowMessage("Please enter details");
                return;
            }

            Int64 HeadId = 0;
            PaymentRecDAL obj = new PaymentRecDAL();


            HeadId = obj.Insert(DtTemp, Convert.ToInt32(drpChallanDetail.SelectedValue), Convert.ToDouble(txtNetAmnt.Text));

            obj = null;
            if (HeadId > 0)
            {
                ShowMessage("Record save successfully");
            }
            else if (HeadId < 0)
            {
                ShowMessage("Summary No already exists");
            }
            else
            {
                ShowMessage("Record not saved successfully");
            }
            this.ClearAll();
        }

        protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                //Populate(Convert.ToInt64(Request.QueryString["q"]));
            }
            else
            {
                this.ClearAll();
            }
        }
        private void ClearAll()
        {
            drpChallanDetail.SelectedValue = "0";
            txtNetAmnt.Text = "0.00";
            txtRecDate.Text = "";
            txtReceivedAmount.Text = "0.00";
            txtSummaryNo.Text = "";
            txtRemark.Text = "";
            ViewState["dt"] = DtTemp = null;
            grdMain.DataSource = null;
            grdMain.DataBind();
            Gridmainhead.DataSource = null;
            Gridmainhead.DataBind();
        }
   
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DtTemp = (DataTable)ViewState["dt"];
            string msg = string.Empty;

            if (txtReceivedAmount.Text == string.Empty)
            {
                this.ShowMessage("Please enter Received Amount!");
                return;
            }
            else if (txtSummaryNo.Text == string.Empty)
            {
                this.ShowMessage("Please enter Summary No!");
                return;
            }
            else
            {
                if (txtReceivedAmount.Text == string.Empty || txtReceivedAmount.Text == "0")
                {
                    this.ShowMessage("Received Amount must be greater than 0!");
                    return;
                }
                else if (txtSummaryNo.Text == string.Empty || txtSummaryNo.Text == "0")
                {
                    this.ShowMessage(" Summary No must be greater than 0!");
                    return;
                }
            }
            if (Hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowid.Value))
                    {
                        dtrow["Sumry_No"] = txtSummaryNo.Text;
                        dtrow["Recvng_Date"] = txtRecDate.Text;
                        dtrow["Recvng_Amnt"] = txtReceivedAmount.Text;
                        dtrow["Remark"] = txtRemark.Text;
                       
                    }
                }
            }
            else
            {
                if (DtTemp == null)
                {
   DtTemp = CreateDt();
                }
          
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ApplicationFunction.DatatableAddRow(DtTemp, id,
                                                   txtSummaryNo.Text,txtRecDate.Text, txtReceivedAmount.Text,txtRemark.Text);
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            this.ClearItems();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.ClearItems();
        }


        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "Sumry_No", "String", "Recvng_Date", "String", "Recvng_Amnt", "String", "Remark", "String");
                                                             
            return dttemp;
        }
        private void ClearItems()
        {


            txtRecDate.Text = "";
            txtReceivedAmount.Text = "0.00";
            hidPaymentid.Value = "";
            Hidrowid.Value = string.Empty;
        
            txtSummaryNo.Text = "";
            txtRemark.Text = "";
        }
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }


    
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                DtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = DtTemp.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {


                    Hidrowid.Value = Convert.ToString(drs[0]["Id"]);
                    txtSummaryNo.Text = Convert.ToString(drs[0]["Sumry_No"]);
                    txtRecDate.Text = Convert.ToDateTime((drs[0]["Recvng_Date"])).ToString("dd-MM-yyyy");
                    txtReceivedAmount.Text = Convert.ToString(drs[0]["Recvng_Amnt"]);
                    txtRemark.Text = Convert.ToString(drs[0]["Remark"]);
                    Hidrowid.Value = Convert.ToString(drs[0]["id"]);
                }
                drpChallanDetail.Focus();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable dt = CreateDt();
                foreach (DataRow rw in DtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {

                        ApplicationFunction.DatatableAddRow(dt, rw["id"], rw["Sumry_No"], rw["Recvng_Date"],
                                                                rw["Recvng_Amnt"], rw["Remark"]);

                    }
                }
                ViewState["dt"] = dt;
                dt.Dispose();
                this.BindGrid();
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double DTotRecivd = 0;

            if (DtTemp == null)
            {
                DtTemp = CreateDt();
                ViewState["dt"] = DtTemp;
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblTol = (Label)e.Row.FindControl("lblRecvAmnt");
                    dTotRevdAmnt += Convert.ToDouble(lblTol.Text);
                  //  lblTol.Text = Convert.ToDouble((txtReceivedAmount.Text)).ToString("N2");

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                   
                    Label llblPendingAmnt = (Label)e.Row.FindControl("lblPendingAmnt");
                    llblPendingAmnt.Text = "pending Amnt:" + Convert.ToDouble((txtNetAmnt.Text)).ToString("N2");

                    //Label llbltolRecord = (Label)e.Row.FindControl("lblTotalRecd");
                    //llbltolRecord.Text = llbltolRecord;
                    
                    Label lblTol = (Label)e.Row.FindControl("lblTolRecvAmnt");
                    lblTol.Text = Convert.ToDouble((dTotRevdAmnt)).ToString("N2");
                    Label lblTotalRecd = (Label)e.Row.FindControl("lblTotalRecd");
                    lblTotalRecd.Text = "Total Record:"+Convert.ToInt32((grdMain.Rows.Count));

                    
 
                }

            }

        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        protected void imbBtnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("PaymentReceived.aspx", false);
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(constring);
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
                    txtRecDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtRecDate.Text = hidmindate.Value;

                }
            }

        }
        public void ChallanDetail()
        {
            PaymentRecDAL obj = new PaymentRecDAL();
            DataTable lst = obj.FetchChallanDetail(constring, Convert.ToInt32(ddlDateRange.SelectedValue));
            if (lst.Rows.Count > 0)
            {
                drpChallanDetail.DataSource = lst;
                drpChallanDetail.DataTextField = "chln_Detl";
                drpChallanDetail.DataValueField = "Chln_Idno";
                drpChallanDetail.DataBind();
                drpChallanDetail.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        protected void drpChallanDetail_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (drpChallanDetail.SelectedIndex >= 0)
                {
                    PaymentRecDAL obj = new PaymentRecDAL();


                   
                   DataTable   DsChlnDetMst = obj.FillChlnDetMast(ApplicationFunction.ConnectionString(),Convert.ToInt32(drpChallanDetail.SelectedValue));
                   Gridmainhead.DataSource = null;
                    Gridmainhead.DataSource = DsChlnDetMst;
                   Gridmainhead.DataBind();
                   txtNetAmnt.Text = Convert.ToDouble(obj.FillChlnDetNetAmnt(ApplicationFunction.ConnectionString(), Convert.ToInt32(drpChallanDetail.SelectedValue))).ToString("N2");

                    DataTable DtPay = obj.SelectPayDet(ApplicationFunction.ConnectionString(), Convert.ToInt32(drpChallanDetail.SelectedValue));
                    grdMain.DataSource = null;
                    grdMain .DataSource = DtPay;
                    grdMain.DataBind();
                    ViewState["dt"] = (DataTable)DtPay;
                }
                else
                {
                    Gridmainhead.DataSource = null;
                   
                    Gridmainhead.DataBind();

                   
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    txtNetAmnt.Text = "0.00";
                 }
            }
            catch (Exception Ex)
            {
            
            }
    
        }
        protected void txtReceivedAmount_TextChanged(object sender, EventArgs e)
        {

            if (txtReceivedAmount.Text.Trim() == "")
            {
                txtReceivedAmount.Text = "0.00";
            }
            else
            {
                txtReceivedAmount.Text = Convert.ToDouble(txtReceivedAmount.Text).ToString("N2");
            }
        }
        protected void txtNetAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtNetAmnt.Text.Trim() == "")
            {
                txtNetAmnt.Text = "0.00";
            }
            else
            {
                txtNetAmnt.Text = Convert.ToDouble(txtNetAmnt.Text).ToString("N2");
            }

        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if(drpChallanDetail.SelectedIndex>0)
                {
                PaymentRecDAL obj = new PaymentRecDAL();
                    int value=0;
                    value = obj.Delete(Convert.ToInt32(drpChallanDetail.SelectedValue));
                    if (value > 0)
                    {
                         ShowMessage("Record  delete successfully");
                    }
                    else
                    {
                        ShowMessage("Record not delete successfully");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}