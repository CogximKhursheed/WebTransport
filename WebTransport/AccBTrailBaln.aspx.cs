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

namespace WebTransport
{
    public partial class AccBTrailBaln : Pagebase
    {
        #region Variable
        string conString = "";
        Double dNetAmount1 = 0;
        Double dNetAmount2 = 0;
        DataSet ds;
        Double bal, bal1;
        private int intFormId = 2;
        DataTable Ndt = new DataTable();
        AccountBookDAL objAccountBookDAL = new AccountBookDAL();
        #endregion

        #region PageLoad Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                conString = ApplicationFunction.ConnectionString();
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                    lnkbtnPrint.Visible = false;
                }
                BindGrid();
            }
        }
        #endregion

        #region Bind Event...
        private void BindGrid()
        {
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
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                lblDebitAmount.Text = TotalNetDebitAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DT.Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();

                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
            objAccountBookDAL = null;
        }
        private DataTable ConvertToDataTable()
        {
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = "";
         
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
           
            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
           
            #endregion
            AccountBookDAL objAccBookDAL = new AccountBookDAL();
            Int64 partyId = Convert.ToInt64(Request.QueryString["party"]);
            string Datefrm = Convert.ToString(Request.QueryString["Datefrm"]);
            string DateTo = Convert.ToString(Request.QueryString["DateTo"]);
            int YearId = Convert.ToInt32(Request.QueryString["Year"]);
            string PartyName = Convert.ToString(Session["PartyName"]);

            Session["PartyName"] = null;
            lblPartyName.Text = PartyName + "  [" + Datefrm + " To " + DateTo + "]";
            ds = objAccBookDAL.Fillledgergrid(conString, "SelectLedgerRpt", partyId, YearId, Datefrm, DateTo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Ndt.Columns.Add("Perti");
                Ndt.Columns.Add("Credit");
                Ndt.Columns.Add("Debit");
                Ndt.Columns.Add("Balance");
                Ndt.Columns.Add("Mon");
                BindDataSet(); BalCalLR();
            }
            return Ndt;
        }
        private void BalCalLR()
        {
            if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "00.00")
                bal = Convert.ToDouble(ds.Tables[0].Rows[0][2]);
            if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "00.00")
                bal1 = Convert.ToDouble(ds.Tables[0].Rows[0][1]);
            double tot = 0;
            for (int i = 1; i <= 12; i++)
            {
                bal = bal + Convert.ToDouble(Ndt.Rows[i][2]);
                bal1 = bal1 + Convert.ToDouble(Ndt.Rows[i][1]);
                tot = bal - bal1;
                if (tot >= 0)
                    Ndt.Rows[i][3] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Dr";
                else
                    Ndt.Rows[i][3] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Cr";
            }

        }
        private void BindDataSet()
        {
            var row = Ndt.NewRow();
            row["PERTI"] = ds.Tables[0].Rows[0]["PERTI"].ToString();
            row["CREDIT"] = string.Format("{0:0,0.00}", Convert.ToString(ds.Tables[0].Rows[0]["CREDIT"]));
            row["DEBIT"] = string.Format("{0:0,0.00}", Convert.ToString(ds.Tables[0].Rows[0]["DEBIT"]));
            row["Balance"] = ds.Tables[0].Rows[0]["Balance"].ToString();
            row["Mon"] = ds.Tables[0].Rows[0]["Mon"].ToString();
            Ndt.Rows.Add(row);
            int i, j, m, mon; i = j = m = mon = 0;
            mon = 4;
            j = 1;
            for (i = 1; i <= 12; i++)
            {
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                if (i > 0)
                {
                    if (mon > 12) mon = 1;
                    Ndt.Rows.Add();
                    Ndt.Rows[i][0] = (new ListItem(info.GetMonthName(mon).ToString(), mon.ToString()));
                    Ndt.Rows[i][1] = Ndt.Rows[i][2] = Ndt.Rows[i][3] = "00.00";
                    Ndt.Rows[i][4] = mon; mon += 1;
                }
            }
            for (i = 0; i <= 12; i++)
            {
                if (i > 0)
                {
                    if (j < ds.Tables[0].Rows.Count)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[j][4]) != "")
                        {
                            if (Convert.ToDouble(Ndt.Rows[i][4]) == Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[j][4]).Substring(4, 2)))
                            {
                                Ndt.Rows[i][0] = Convert.ToString(ds.Tables[0].Rows[j][0]);
                                Ndt.Rows[i][1] = string.Format("{0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[j][1]).ToString("N", new CultureInfo("hi-IN")));
                                Ndt.Rows[i][2] = string.Format("{0:0,0.00}", Convert.ToDouble(ds.Tables[0].Rows[j][2]).ToString("N", new CultureInfo("hi-IN")));
                                j += 1;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        #endregion

        #region Grid Event...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblParticular = (Label)e.Row.FindControl("lblParticular");
                string part = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI")).Trim();
                lblParticular.Text = part;
                LinkButton lnkBtnParticular = (LinkButton)e.Row.FindControl("lnkBtnParticular");
                string particular = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI")).Trim();
                lnkBtnParticular.Text = particular;
                Label lbl2Debit = (Label)e.Row.FindControl("lbl2Debit");
                lbl2Debit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                string Debit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                Label lbl3Credit = (Label)e.Row.FindControl("lbl3Credit");
                lbl3Credit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                string Credit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                Label lbl4Balance = (Label)e.Row.FindControl("lbl4Balance");
                lbl4Balance.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Balance"));

                if (Debit.ToLower() == "0.00" && Credit.ToLower() == "0.00")
                {
                    lblParticular.Visible = true;
                    lnkBtnParticular.Visible = false;
                    lblParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI"));
                }
                else
                {
                    lblParticular.Visible = false;
                    lnkBtnParticular.Visible = true;
                    lnkBtnParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI"));
                }
                if (part.ToLower() == "opening balance")
                {
                    lblParticular.Visible = true;
                    lnkBtnParticular.Visible = false;
                }
                dNetAmount1 = dNetAmount1 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"))); //== null ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Debit"));
                dNetAmount2 = dNetAmount2 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")));// == null ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                lblDebit.Text = String.Format("{0:0,0.00}", dNetAmount1.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                lblCredit.Text = String.Format("{0:0,0.00}", dNetAmount2.ToString("N", new CultureInfo("hi-IN"))).ToString();

                string DebitA = lblDebit.Text;
                string CreditA = lblCredit.Text;
                LinkButton lnkBtn2Debit = (LinkButton)e.Row.FindControl("lnkBtn2Debit");
                LinkButton lnkBtn2Credit = (LinkButton)e.Row.FindControl("lnkBtn2Credit");

                if (DebitA.ToLower() == "00.00" && CreditA.ToLower() == "00.00")
                {
                    lblDebit.Visible = true;
                    lblCredit.Visible = true;
                    lnkBtn2Debit.Visible = false;
                    lnkBtn2Credit.Visible = false;
                    lblDebit.Text = Convert.ToString(lblDebit.Text);
                    lblCredit.Text = Convert.ToString(lblCredit.Text);
                }
                else
                {
                    lblDebit.Visible = false;
                    lblCredit.Visible = false;
                    lnkBtn2Debit.Visible = true;
                    lnkBtn2Credit.Visible = true;
                    lnkBtn2Debit.Text = Convert.ToString(lblDebit.Text);
                    lnkBtn2Credit.Text = Convert.ToString(lblCredit.Text);
                }
            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdshowdetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                LinkButton lnkBtnParticular = (LinkButton)row.FindControl("lnkBtnParticular");
                int PreviousRow = row.RowIndex;
                string OpeningBal = "0";
                if (lnkBtnParticular.Text == "April")
                {
                    Label lblDebitOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl2Debit");
                    Label lblCreditOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl3Credit");
                    Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                    Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                    if (DebitOP > CreditOP)
                    {
                        OpeningBal = lblDebitOP.Text + " Dr";
                    }
                    else
                    {
                        OpeningBal = lblCreditOP.Text + " Cr";
                    }
                }
                else
                {
                    Label lbl4Balance = (Label)grdMain.Rows[PreviousRow].FindControl("lbl4Balance");
                    OpeningBal = lbl4Balance.Text;
                }
                int monthNo = 0;
                if (OpeningBal.ToLower() != "april")
                {
                    monthNo = ApplicationFunction.monthNo(lnkBtnParticular.Text.Trim());
                }
                Int64 partyId = Convert.ToInt64(Request.QueryString["party"]);
                int year = 0;
                if (monthNo > 3 && monthNo <= 12)
                {
                    year = Convert.ToInt32(Request.QueryString["Year1"]);
                }
                else
                {
                    year = Convert.ToInt32(Request.QueryString["Year1"]) + 1;
                }
                string startDate = Convert.ToString("01-" + monthNo + "-" + year);
                string endDate = Convert.ToString(DateTime.DaysInMonth(year, monthNo) + "-" + monthNo + "-" + year);
                string PartyName1 = Convert.ToString(Request.QueryString["PartyName"]);
                Session["PartyName"] = PartyName1;
                Int64 Comp_idno = Convert.ToInt64(Request.QueryString["Comp_Idno"]);
                string PartyName = PartyName1.Replace("&", " ");
                string url = "AccBLedger.aspx?startdate=" + startDate + "&enddate=" + endDate + "&party=" + partyId + "&OpeningBal=" + OpeningBal + "&PartyName=" + PartyName + "&Comp_Idno=" + Comp_idno;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else if (e.CommandName == "cmdshowdetail1D")
            {
                #region Ledger Year Wise Details...
                string OpeningBal = "0";
                Label lblDebitOP = (Label)grdMain.Rows[0].FindControl("lbl2Debit");
                Label lblCreditOP = (Label)grdMain.Rows[0].FindControl("lbl3Credit");
                Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                if (DebitOP > CreditOP)
                {
                    OpeningBal = lblDebitOP.Text + " Dr";
                }
                else
                {
                    OpeningBal = lblCreditOP.Text + " Cr";
                }
                string StartDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Request.QueryString["Datefrm"]))).ToString("dd-MM-yyyy");
                string EndDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Request.QueryString["DateTo"]))).ToString("dd-MM-yyyy");
                int PartyIdno = Convert.ToInt32(Request.QueryString["party"]); //Convert.ToInt32(ddlParty.SelectedValue);
                string FrmType = "LdgrAll";
                string PartyName1 = Convert.ToString(Request.QueryString["PartyName"]); //Convert.ToString(ddlParty.SelectedItem.Text);
                Int64 Comp_idno = Convert.ToInt64(Request.QueryString["Comp_Idno"]);
                Session["PartyName"] = PartyName1;
                string PartyName = PartyName1.Replace("&", " ");
                Response.Redirect("AccBLedger.aspx?startdate=" + StartDate + "&enddate=" + EndDate + "&party=" + PartyIdno + "&PartyName=" + PartyName + "&FrmType=" + FrmType + "&OpeningBal=" + OpeningBal + "&Comp_Idno=" + Comp_idno);
                #endregion
            }
        }
        #endregion

        #region Excel...
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
        }
        private void PrepareGridViewForExport(Control gv)
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
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion Excel
    }
}