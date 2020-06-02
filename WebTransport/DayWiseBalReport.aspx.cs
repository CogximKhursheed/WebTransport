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
    public partial class DayWiseBalReport : Pagebase
    {
        #region Variable
        string conString = "";
        Double dNetAmount1 = 0; double dr = 0; double cr = 0;
        Double dNetAmount2 = 0;
        DataSet ds;
        Double bal, bal1;
        private int intFormId = 55;
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
                    printRep.Visible = false;
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
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
            objAccountBookDAL = null;
        }
        private DataTable ConvertToDataTable()
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

            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;

            #endregion

            AccountBookDAL objAccBookDAL = new AccountBookDAL();
            DataTable dTN = new DataTable();
            Int64 partyId = Convert.ToInt64(Request.QueryString["party"]);
            string Datefrm = Convert.ToString(Request.QueryString["startdate"]);
            string DateTo = Convert.ToString(Request.QueryString["enddate"]);
            string OpenIngBal = Convert.ToString(Request.QueryString["OpeningBal"]);
            string PartyName = Convert.ToString(Session["PartyName"]);
            string openingBalCRDR = Convert.ToString(Session["OpengBalCRDR"]);//).Substring((Convert.ToString(Session["OpengBalCRDR"])).Length-2, (Convert.ToString(Session["OpengBalCRDR"])).Length);
            Session["PartyIdno"] = partyId;
            openingBalCRDR = openingBalCRDR.Substring((openingBalCRDR.Length) - 2, 2);
            if (openingBalCRDR == "Dr")
            {
                dr = Convert.ToDouble(OpenIngBal); cr = 0;
            }
            else
            {
                dr = 0; cr = Convert.ToDouble(Math.Abs(Convert.ToDouble(OpenIngBal)));
            }
            // Session["PartyName"] = null;
            lblPartyName.Text = PartyName + "  [" + Datefrm + " To " + DateTo + "]";
            dTN.Columns.Add("Date");
            dTN.Columns.Add("NARR_TEXT");
            dTN.Columns.Add("DEBIT");
            dTN.Columns.Add("CREDIT");
            string strAction = "SelectLedgerRpt";
            DataSet dsTable = objAccBookDAL.FilllDayWiseBalgrid(ApplicationFunction.ConnectionString(), strAction, partyId, Datefrm, DateTo);
            if (dsTable != null && dsTable.Tables.Count > 0 && dsTable.Tables[0].Rows.Count > 0)
            {

                var row = dTN.NewRow();
                row["CREDIT"] = string.Format("{0:0,0.00}", Convert.ToString(cr));
                row["DEBIT"] = string.Format("{0:0,0.00}", Convert.ToString(dr));
                row["Date"] = "Opening Balance";
                row["NARR_TEXT"] = "";
                dTN.Rows.Add(row);
                for (int i = 0; i < dsTable.Tables[0].Rows.Count; i++)
                {
                    dTN.Rows.Add(Convert.ToString(dsTable.Tables[0].Rows[i]["Date"]),
                   Convert.ToString(dsTable.Tables[0].Rows[i]["NARR_TEXT"]),
                   Convert.ToString(dsTable.Tables[0].Rows[i]["Debit"]),
                   Convert.ToString(dsTable.Tables[0].Rows[i]["Credit"]));


                }
                double Amnt = 0;
                Amnt = Convert.ToDouble(dTN.Rows[0]["Debit"]) - Convert.ToDouble(dTN.Rows[0]["Credit"]);
                for (int i = 1; i < dTN.Rows.Count; i++)
                {
                    Amnt = (Amnt + Convert.ToDouble(dTN.Rows[i]["Debit"])) - Convert.ToDouble(dTN.Rows[i]["Credit"]);
                    if (Amnt > 0)
                    {
                        dTN.Rows[i]["Debit"] = Amnt.ToString("N2"); dTN.Rows[i]["Credit"] = "0.00";
                    }
                    else
                    {
                        dTN.Rows[i]["Credit"] = Convert.ToDouble(Math.Abs(Amnt)).ToString("N2"); dTN.Rows[i]["Debit"] = "0.00";
                    }
                }
            }
            return dTN;
        }
        #endregion

        #region Grid Event...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblParticular = (Label)e.Row.FindControl("lblParticular");
                string part = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Date")).Trim();
                lblParticular.Text = part;
                LinkButton lnkBtnParticular = (LinkButton)e.Row.FindControl("lnkBtnParticular");
                string particular = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Date")).Trim();
                lnkBtnParticular.Text = particular;
                Label lbl2Debit = (Label)e.Row.FindControl("lbl2Debit");
                lbl2Debit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                string Debit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT")).ToString("N", new CultureInfo("hi-IN")));
                Label lbl3Credit = (Label)e.Row.FindControl("lbl3Credit");
                lbl3Credit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                string Credit = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT")).ToString("N", new CultureInfo("hi-IN")));
                if (Debit.ToLower() == "0.00" && Credit.ToLower() == "0.00")
                {
                    lblParticular.Visible = true;
                    lnkBtnParticular.Visible = false;
                    lblParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Date"));
                }
                else
                {
                    lblParticular.Visible = false;
                    lnkBtnParticular.Visible = true;
                    lnkBtnParticular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Date"));
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
                int PreviousRow = row.RowIndex - 1;
                Int64 partyId = Convert.ToInt64(Request.QueryString["party"]);
                string startDate = Convert.ToString(lnkBtnParticular.Text.Trim());
                string endDate = Convert.ToString(lnkBtnParticular.Text.Trim());
                string PartyName1 = Convert.ToString(Request.QueryString["PartyName"]);
                Label lblDebitOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl2Debit");
                Label lblCreditOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl3Credit");
                Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                string op = "";
                if (DebitOP > CreditOP)
                {
                    op = DebitOP.ToString("N2");
                    Session["OpengBalCRDR"] = op + "Dr";
                }
                else
                {
                    op = CreditOP.ToString("N2");
                    Session["OpengBalCRDR"] = op + "Cr";
                }
                string OpenIngBal = Convert.ToString(Session["OpengBalCRDR"]);
                Session["PartyName"] = PartyName1;
                Int64 Comp_idno = Convert.ToInt64(Request.QueryString["Comp_Idno"]);
                string PartyName = PartyName1.Replace("&", " ");
                Response.Redirect("AccBLedger.aspx?startdate=" + startDate + "&enddate=" + endDate + "&party=" + partyId + "&OpeningBal=" + OpenIngBal + "&PartyName=" + PartyName + "&Comp_Idno=" + Comp_idno);
            }
            else if (e.CommandName == "cmdshowdetail1D")
            {
                #region Ledger Year Wise Details...
                Label lblDebitOP = (Label)grdMain.Rows[0].FindControl("lbl2Debit");
                Label lblCreditOP = (Label)grdMain.Rows[0].FindControl("lbl3Credit");
                Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                string OpenIngBal = Convert.ToString(Session["OpengBalCRDR"]);
                string StartDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Request.QueryString["startdate"]))).ToString("dd-MM-yyyy");
                string EndDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Request.QueryString["enddate"]))).ToString("dd-MM-yyyy");
                int PartyIdno = Convert.ToInt32(Request.QueryString["party"]); //Convert.ToInt32(ddlParty.SelectedValue);
                string FrmType = "LdgrAll";
                string PartyName1 = Convert.ToString(Request.QueryString["PartyName"]); //Convert.ToString(ddlParty.SelectedItem.Text);
                Int64 Comp_idno = Convert.ToInt64(Request.QueryString["Comp_Idno"]);
                Session["PartyName"] = PartyName1;
                string PartyName = PartyName1.Replace("&", " ");
                Response.Redirect("AccBLedger.aspx?startdate=" + StartDate + "&enddate=" + EndDate + "&party=" + PartyIdno + "&PartyName=" + PartyName + "&FrmType=" + FrmType + "&OpeningBal=" + OpenIngBal + "&Comp_Idno=" + Comp_idno);
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