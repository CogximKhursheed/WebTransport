using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Globalization;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class AccBLedger : Pagebase
    {
        #region Variabless...
        string conString = "";
        Double dNetAmount1 = 0;
        Double dNetAmount2 = 0;
        string LastBal = string.Empty;
        private int intFormId = 55;
        AccountBookDAL objAccountBookDAL = new AccountBookDAL();
        #endregion

        #region Pageload Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                    lnkbtnPrint.Visible = false;
                }
                this.BindGrid();
            }
        }
        #endregion

        #region Grid Event...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVchrType = (Label)e.Row.FindControl("lblVchrType");
                LinkButton lnkBtnVchrType = (LinkButton)e.Row.FindControl("lnkBtnVchrType");
                HiddenField hidVchrFrm = (HiddenField)e.Row.FindControl("hidVchrFrm");
                string FTyp = hidVchrFrm.Value.ToUpper();

                if (FTyp == "IB") { lnkBtnVchrType.Text = "Inv"; }
                else if (FTyp == "ARIV") { lnkBtnVchrType.Text = "RcptInv"; }
                else if (FTyp == "CB") { lnkBtnVchrType.Text = "ChlnBook"; }
                else if (FTyp == "FS") { lnkBtnVchrType.Text = "FuelSlip"; }
                else if (FTyp == "GR") { lnkBtnVchrType.Text = "Ag.GR"; }
                else if (FTyp == "PTD") { lnkBtnVchrType.Text = "Ag.PayOwn"; }
                else if (FTyp == "ACB") { lnkBtnVchrType.Text = "Ag.Chln"; }
                else if (FTyp == "PB") { lnkBtnVchrType.Text = "PBill"; }
                else if (FTyp == "SB") { lnkBtnVchrType.Text = "SBill"; }

                else { lnkBtnVchrType.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VCHR_TYPE")); }
                dNetAmount1 = dNetAmount1 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit")));
                dNetAmount2 = dNetAmount2 + (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit")) == null ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")));
                LastBal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Balance"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblTDebit");
                lblDebit.Text = String.Format("{0:0,0.00}", dNetAmount1.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lblCredit = (Label)e.Row.FindControl("lblTCredit");
                lblCredit.Text = String.Format("{0:0,0.00}", dNetAmount2.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                lblBalance.Text = LastBal;

                //lblAmountBal.Text = LastBal;
            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showdetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                LinkButton lnkBtnVchrType = (LinkButton)row.FindControl("lnkBtnVchrType");
                string Vchr_Type = lnkBtnVchrType.Text;
                HiddenField hidVchrIdno = (HiddenField)row.FindControl("hidVchrIdno");
                int Vchr_Idno = Convert.ToInt32(hidVchrIdno.Value);
                HiddenField hidDoc = (HiddenField)row.FindControl("hidDoc");
                int HId = Convert.ToInt32(hidDoc.Value);
                HiddenField hidVchrFrm = (HiddenField)row.FindControl("hidVchrFrm");
                string FTyp = hidVchrFrm.Value.ToUpper();
                string PageName = string.Empty;

                if (string.IsNullOrEmpty(FTyp) == false)
                {
                    if (FTyp == "IB") { PageName = "Invoice.aspx?q="; }
                    else if (FTyp == "ARIV") { PageName = "AmntAgainstInvoice.aspx?q="; }
                    else if (FTyp == "CB") { PageName = "ChlnBooking.aspx?q="; }
                    else if (FTyp == "FS") { PageName = "FuelSlip.aspx?FuelSlipIdno="; }
                    else if (FTyp == "GR") { PageName = "GRPrep.aspx?Gr="; }
                    else if (FTyp == "PTD") { PageName = "PaymentToOwn.aspx?q="; }
                    else if (FTyp == "ACB") { PageName = "ChlnAmntPayment.aspx?q="; }
                    else if (FTyp == "PB") { PageName = "PurchaseBill.aspx?PB="; }
                    else if (FTyp == "SB") { PageName = "SaleBill.aspx?SbillIdno="; }
                    if (string.IsNullOrEmpty(PageName) == false)
                    {
                        if (FTyp == "CB")
                        {
                            AccountBookDAL obj = new AccountBookDAL();
                            var lst=obj.SelectChallanType(Convert.ToInt64(HId));
                            string GRType = "";
                            if (lst != null)
                            {
                                GRType = Convert.ToString(lst.Gr_Type);
                                string url = PageName + HId + "-" + GRType;
                                string fullURL = "window.open('" + url + "', '_blank' );";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                            }
                        }
                        else if (FTyp == "IB")
                        {
                            AccountBookDAL obj = new AccountBookDAL();
                            var lst = obj.SelectInvoiceType(Convert.ToInt64(HId));
                            string GRType = "";
                            if (lst != null)
                            {
                                GRType = Convert.ToString(lst.Gr_Type);
                                string url = PageName + HId + "-" + GRType;
                                string fullURL = "window.open('" + url + "', '_blank' );";
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                            }
                        }

                        else
                        {
                            string url = PageName + HId;
                            string fullURL = "window.open('" + url + "', '_blank' );";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        }
                        
                    }

                }
                else
                {
                    string url = "VchrEntry.aspx?VchrIdno=" + HId;
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }
        }
        #endregion

        #region Bind Event...
        private void BindGrid()
        {
           
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; //string ServTaxNo = ""; 
           string FaxNo = "";
         
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;

            #endregion

            DataTable DT = ConvertToDataTable();
            if (DT != null && DT.Rows.Count > 0)
            {
                grdMain.DataSource = DT;
                grdMain.DataBind();

                Double TotalNetAmount = 0; Double TotalNetDebitAmount = 0;

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DT.Rows[i]["CREDIT"]);
                    TotalNetDebitAmount += Convert.ToDouble(DT.Rows[i]["DEBIT"]);
                }
                //lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                //lblDebitAmount.Text = TotalNetDebitAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                //lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DT.Rows.Count.ToString();
                //lblcontant.Visible = true;
                //divpaging.Visible = true;
            }
            else
            {
                //lblcontant.Visible = false;
                //divpaging.Visible = false;
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }
        private DataTable ConvertToDataTable()
        {
            string strDateFrom = "";// Convert.ToString(Request.QueryString["startdate"]);
            string strDateTo = "";// Convert.ToString(Request.QueryString["enddate"]);
            Int64 intpartyidno = 0;// Convert.ToInt64(Request.QueryString["party"]);
            string OpeningBal = "";// Convert.ToString(Request.QueryString["OpeningBal"]);
            string CrDrAmount = "";// OpeningBal.Substring(OpeningBal.Length - Math.Min(2, OpeningBal.Length));
            string PartyName = Convert.ToString(Session["PartyName"]);
            Session["PartyName"] = null;
            string FrmType = Convert.ToString(Request.QueryString["FrmType"]);
            if (FrmType == null)
            {
                strDateFrom = Convert.ToString(Request.QueryString["startdate"]);
                strDateTo = Convert.ToString(Request.QueryString["enddate"]);
                intpartyidno = Convert.ToInt64(Request.QueryString["party"]);
                OpeningBal = Convert.ToString(Request.QueryString["OpeningBal"]);
                CrDrAmount = OpeningBal.Substring(OpeningBal.Length - Math.Min(2, OpeningBal.Length));
                lblPartyName.Text = PartyName + "  [" + strDateFrom + " To " + strDateTo + "]";
            }
            else
            {
                strDateFrom = Convert.ToString(Request.QueryString["startdate"]);
                strDateTo = Convert.ToString(Request.QueryString["enddate"]);
                intpartyidno = Convert.ToInt64(Request.QueryString["party"]);
                OpeningBal = Convert.ToString(Request.QueryString["OpeningBal"]);
                CrDrAmount = OpeningBal.Substring(OpeningBal.Length - Math.Min(2, OpeningBal.Length));
                lblPartyName.Text = PartyName + "  [" + strDateFrom + " To " + strDateTo + "]";
            }
            String NewString;
            NewString = string.IsNullOrEmpty(OpeningBal) ? "0" : OpeningBal.Remove(OpeningBal.Length - 2, 2);
            DataTable Ndt = new DataTable();
            DataSet Ndt1 = new DataSet();
            DataSet ds1 = new DataSet();

            if (FrmType == null)
            {
                ds1 = objAccountBookDAL.AfterDoubleClick1(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneById", intpartyidno, strDateFrom, strDateTo);
            }
            else
            {
                ds1 = objAccountBookDAL.AfterDoubleClick1(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneById", intpartyidno, strDateFrom, strDateTo);
            }
            if (ds1 != null && ds1.Tables.Count > 0)
            {
                Ndt.Columns.Add("VCHR_DATE"); Ndt.Columns.Add("VCHR_NO"); Ndt.Columns.Add("PARTICULAR"); Ndt.Columns.Add("VCHR_TYPE");
                Ndt.Columns.Add("Credit"); Ndt.Columns.Add("Debit"); Ndt.Columns.Add("NARR_TEXT"); Ndt.Columns.Add("VCHR_IDNO");
                Ndt.Columns.Add("AMNT_TYPE"); Ndt.Columns.Add("Vchr_Frm"); Ndt.Columns.Add("DOC_IDNO"); Ndt.Columns.Add("Balance");
                var row = Ndt.NewRow();
                row["PARTICULAR"] = "";
                row["Debit"] = "";
                row["Credit"] = "";
                Ndt.Rows.Add(row);
                Ndt.Rows[0]["PARTICULAR"] = "Opening Balance";

                if (CrDrAmount == "Dr")
                {
                    Ndt.Rows[0]["Credit"] = "00.00";
                    Ndt.Rows[0]["Debit"] = NewString;
                }
                else
                {
                    Ndt.Rows[0]["Credit"] = NewString;
                    Ndt.Rows[0]["Debit"] = "00.00";
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int x = 0; x < ds1.Tables[0].Rows.Count; x++)
                    {
                        DataSet ds2 = objAccountBookDAL.AfterDblClikForLedD(ApplicationFunction.ConnectionString(), "SelectLdgrDetOne", Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]), Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]), intpartyidno);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                for (int y = 0; y < ds2.Tables[0].Rows.Count; y++)
                                {
                                    if (ds2.Tables[0].Rows.Count > 1)
                                        Ndt.Rows.Add(Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_DATE"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_NO"]),
                                                               Convert.ToString(ds2.Tables[0].Rows[y]["PARTICULAR"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_TYPE"]),
                                                               Convert.ToDouble(ds2.Tables[0].Rows[y]["Credit"]),
                                                               Convert.ToDouble(ds2.Tables[0].Rows[y]["Debit"]),
                                                               Convert.ToString(ds2.Tables[0].Rows[y]["NARR_TEXT"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["Vchr_Frm"]),
                                                               Convert.ToString(ds1.Tables[0].Rows[x]["DOC_IDNO"]));
                                    else
                                        Ndt.Rows.Add(Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_DATE"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_NO"]),
                                                                Convert.ToString(ds2.Tables[0].Rows[y]["PARTICULAR"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_TYPE"]),
                                                                Convert.ToDouble(ds1.Tables[0].Rows[x]["Debit"]),
                                                                Convert.ToDouble(ds1.Tables[0].Rows[x]["Credit"]),
                                                                Convert.ToString(ds2.Tables[0].Rows[y]["NARR_TEXT"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["Vchr_Frm"]),
                                                                Convert.ToString(ds1.Tables[0].Rows[x]["DOC_IDNO"]));
                                }
                            }
                        }

                        ds2 = null;
                    }

                    Double bal = 0;
                    Double bal1 = 0;
                    if (Convert.ToString(Ndt.Rows[0]["Debit"]) != "00.00")
                        bal = Convert.ToDouble(Ndt.Rows[0]["Debit"]);
                    if (Convert.ToString(Ndt.Rows[0]["Credit"]) != "00.00")
                        bal1 = Convert.ToDouble(Ndt.Rows[0]["Credit"]);
                    double tot = 0;
                    for (int i = 1; i < Ndt.Rows.Count; i++)
                    {
                        bal = bal + Convert.ToDouble(Ndt.Rows[i]["Debit"]);
                        bal1 = bal1 + Convert.ToDouble(Ndt.Rows[i]["Credit"]);
                        tot = bal - bal1;
                        if (tot >= 0)
                            Ndt.Rows[i][11] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Dr";
                        else
                            Ndt.Rows[i][11] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Cr";
                    }
                }
            }
            return Ndt;
        }

        #endregion

        #region Excel...
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
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

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }
        #endregion Excel
    }
}