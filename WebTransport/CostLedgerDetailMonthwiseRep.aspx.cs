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
    public partial class CostLedgerDetailMonthwiseRep : Pagebase
    {
        #region Variable...
        string conString = "", msg = "";
        Double dNetAmount1 = 0, dNetAmount2 = 0, bal = 0, bal1 = 0;
        private int intFormId = 55;
        AccountBookDAL objAccountBookDAL = new AccountBookDAL();
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            conString = ApplicationFunction.ConnectionString();
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
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
                
        #region Button...
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            //    {
            //        if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
            //        {
            //            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Validfrom date can not be greater than Validto date!');", true);
            //            ddlDateRange.Focus();
            //            return;
            //        }
            //    }
            //    this.BindGrid();
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}
        }
        #endregion

        #region Bind Event...
        private void BindGrid()
        {
            try
            {

                DataTable dsTable1 = ConvertToDatatable();
                if (dsTable1 != null && dsTable1.Rows.Count > 0)
                {
                    grdMain.DataSource = dsTable1;
                    grdMain.DataBind();
                    imgBtnExcel.Visible = true;
                    printRep.Visible = true;

                }
                else
                {
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    imgBtnExcel.Visible = false;
                    printRep.Visible = false;
                }


                objAccountBookDAL = null;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        private DataTable ConvertToDatatable()
        {
            CostLedgerDAL objAccBookDAL = new CostLedgerDAL();
            DataTable dTN = new DataTable();
            hidstr.Value = "";
            Int64 TruckIdno = 0;
            Int32 YearIdno = 0;
            string PrtyName = "";
            Int64 PrtyIdno = 0;
            PrtyName = Convert.ToString(Session["PrtyName"]);
            lblLedgerName.Text = "[" + Convert.ToString(Session["PrtyName"]) + "]";
            PrtyIdno = Convert.ToInt64(Session["PrtyIdno"]);
            TruckIdno = Convert.ToInt64(Session["TruckIdno"]);
            TruckIdno = Convert.ToInt64(Session["TruckIdno"]);
            TruckIdno = Convert.ToInt64(Session["TruckIdno"]);
            YearIdno =  Convert.ToInt32(Request.QueryString["YearIdno"]);
            lblTruckNo.Text = "["+Convert.ToString(Session["TruckNo"])+"]";
            //Int64 intCompIdno = ddlcompany.SelectedItem.Text == "All" ? 0 : Convert.ToInt64(ddlcompany.SelectedValue);
            #region Ledger Report
            dTN.Columns.Add("PERTI");
            dTN.Columns.Add("DEBIT");
            dTN.Columns.Add("CREDIT");
            dTN.Columns.Add("Balance");
            dTN.Columns.Add("Mon");
            dTN.Columns.Add("Truck_Idno");
            string      strAction = "SelectDetailReport";
            DataSet dsTable = objAccBookDAL.FillCostgrid(ApplicationFunction.ConnectionString(), strAction,PrtyIdno, TruckIdno, YearIdno);
            #region
            if (dsTable != null && dsTable.Tables.Count > 0 && dsTable.Tables[0].Rows.Count > 0)
            {
                int i, j, m, mon; i = j = m = mon = 0;
                mon = 4;
                j = 0;
                for (i = 0; i < 12; i++)
                {
                    DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                        if (mon > 12) mon = 1;
                        dTN.Rows.Add();
                        dTN.Rows[i][0] = (new ListItem(info.GetMonthName(mon).ToString(), mon.ToString()));
                        dTN.Rows[i][1] = dTN.Rows[i][2] = dTN.Rows[i][3] = "00.00";
                        dTN.Rows[i][4] = mon; mon += 1;
                }
                for (i = 0; i < 12; i++)
                {
                        if (j < dsTable.Tables[0].Rows.Count)
                        {
                            if (Convert.ToString(dsTable.Tables[0].Rows[j][4]) != "")
                            {
                                if (Convert.ToDouble(dTN.Rows[i][4]) == Convert.ToDouble(Convert.ToString(dsTable.Tables[0].Rows[j][4]).Substring(4, 2)))
                                {
                                    dTN.Rows[i][0] = Convert.ToString(dsTable.Tables[0].Rows[j][0]);
                                    dTN.Rows[i][1] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[j][1]));
                                    dTN.Rows[i][2] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[j][2]));
                                    dTN.Rows[i][3] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[j][3]));
                                    j += 1;
                                }
                            }
                        }
                        else { break; }
                }
            }
            #endregion
            #endregion
            objAccountBookDAL = null;
            return dTN;
        }
        //private DataTable CreateOpeningBalance(string conString, string strDateFrom, string strDateTo, int intyearIdno)
        //{

        //    AccountBookDAL objAccBookDAL11 = new AccountBookDAL();
        //    DataTable ds11 = new DataTable();
        //    hidstr.Value = "";
        //    //if (ddlParty.SelectedValue.Count() > 0)
        //    if (ddlParty.Items.Count > 1)
        //    {
        //        if (ddlParty.SelectedIndex == 0)
        //        {
        //            for (int count = 1; count <= ddlParty.Items.Count - 1; count++)
        //            {
        //                hidstr.Value = ddlParty.Items[count].Value + "," + hidstr.Value;
        //            }
        //        }
        //        else
        //        {
        //            hidstr.Value = ddlParty.SelectedValue + ",";
        //        }
        //        if (hidstr.Value != "" && Convert.ToString(hidstr.Value).Length > 1)
        //        {
        //            hidstr.Value = hidstr.Value.Substring(0, hidstr.Value.Length - 1);
        //        }
        //        string[] Arr = Convert.ToString(hidstr.Value).Split(new char[] { ',' });
        //        foreach (string I in Arr)
        //        {
        //            DataTable dtOutput = objAccBookDAL11.OpeningBalGrid(ApplicationFunction.ConnectionString(), "OpeningBalGrdyr", strDateFrom, strDateTo, intyearIdno, Convert.ToInt64(I));
        //            if (dtOutput != null && dtOutput.Rows.Count > 0)
        //            {
        //                if (ds11 == null || ds11.Rows.Count == 0)
        //                {
        //                    ds11.Merge(dtOutput);
        //                }
        //                else
        //                {
        //                    ds11.Merge(dtOutput);
        //                }
        //            }
        //        }
        //    }
        //    return ds11;
        //}
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdshowdetail")
            {
                #region Ledger Account
                string OpeningBal = "0";
                DataTable DtLedger = ConvertToDatatable();
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                int PreviousRow = row.RowIndex - 1;
                LinkButton lnkBtnParticular = (LinkButton)row.FindControl("lnkBtnParticular");
                //if (lnkBtnParticular.Text == "April")
                //{
                //    Label lblDebitOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl2Debit");
                //    Label lblCreditOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl3Credit");
                //    Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                //    Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                //    if (DebitOP > CreditOP)
                //    {
                //        OpeningBal = lblDebitOP.Text + " Dr";
                //    }
                //    else
                //    {
                //        OpeningBal = lblCreditOP.Text + " Cr";
                //    }
                //}
                //else
                //{

                //    Label lbl4Balance = (Label)grdMain.Rows[PreviousRow].FindControl("lbl4Balance");
                //    OpeningBal = lbl4Balance.Text;
                //}
                int monthNo = 0;
                if (OpeningBal.ToLower() != "april")
                {
                    monthNo = ApplicationFunction.monthNo(lnkBtnParticular.Text.Trim());
                }
                int year = 0;
                if (monthNo > 3 && monthNo <= 12)
                {
                    year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Session["StartDate"]))).Year;
                }
                else
                {
                    year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Convert.ToString(Session["StartDate"]))).Year + 1;
                }
                string startDate = Convert.ToString("01-" + monthNo + "-" + year); //dd-MM-yyyy
                string endDate = Convert.ToString(DateTime.DaysInMonth(year, monthNo) + "-" + monthNo + "-" + year); //dd-MM-yyyy
                Int64 TruckIdno = Convert.ToInt64(Session["TruckIdno"]);
                string TruckNo = Convert.ToString(Session["TruckNo"]);
                Session["TruckNo"] = TruckNo;
               // string PartyName = TruckNo.Replace("&", " ");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertpopup", "ShowPopup('" + startDate + "','" + endDate + "','" + partyId + "','" + OpeningBal + "','" + PartyName + "')", true);
                Response.Redirect("CostLedgerVchrDetail.aspx?startdate=" + startDate + "&enddate=" + endDate + "&TruckIdno=" + TruckIdno + "&TruckNo=" + TruckNo);
                #endregion
            }
            else if (e.CommandName == "cmdshowdetail1D")
            {
                #region Ledger Year Wise Details...
                string StartDate = Convert.ToString(Convert.ToString(Session["StartDate"]));
                string EndDate = Convert.ToString(Session["EndDate"]);
                string FrmType = "LdgrAll";
                Int64 TruckIdno = Convert.ToInt64(Session["TruckIdno"]);
                string TruckNo = Convert.ToString(Session["TruckNo"]);
                Session["TruckNo"] = TruckNo;
                Response.Redirect("CostLedgerVchrDetail.aspx?startdate=" + StartDate + "&enddate=" + EndDate + "&TruckIdno=" + TruckIdno + "&FrmType=" + FrmType + "&TruckNo=" + TruckNo);
                #endregion
            }
          

        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    #region FillGrid for Ladger Reports
                    Label lblParticular = (Label)e.Row.FindControl("lblParticular");
                    LinkButton lnkBtnParticular = (LinkButton)e.Row.FindControl("lnkBtnParticular");
                    string particular = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PERTI")).Trim();
                   lblParticular.Text = particular;

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
                    if (particular.ToLower() == "opening balance")
                    {
                        lblParticular.Visible = true;
                        lnkBtnParticular.Visible = false;
                    }

                    grdMain.Columns[0].Visible = true;
                    grdMain.Columns[1].Visible = true;
                    grdMain.Columns[2].Visible = true;
                    grdMain.Columns[3].Visible = true;

                                     dNetAmount1 = dNetAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT"));
                    dNetAmount2 = dNetAmount2 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT"));
                    #endregion
               
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl2TDebit = (Label)e.Row.FindControl("lbl2TDebit");
                lbl2TDebit.Text = String.Format("{0:0,0.00}", dNetAmount1.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lbl3TCredit = (Label)e.Row.FindControl("lbl3TCredit");
                lbl3TCredit.Text = String.Format("{0:0,0.00}", dNetAmount2.ToString("N", new CultureInfo("hi-IN"))).ToString();

                string DebitA = lbl2TDebit.Text;
                string CreditA = lbl3TCredit.Text;
                LinkButton lnkBtn2Debit = (LinkButton)e.Row.FindControl("lnkBtn2Debit");
                LinkButton lnkBtn2Credit = (LinkButton)e.Row.FindControl("lnkBtn2Credit");

                if (DebitA.ToLower() == "00.00" && CreditA.ToLower() == "00.00")
                {
                    lbl2TDebit.Visible = true;
                    lbl3TCredit.Visible = true;
                    lnkBtn2Debit.Visible = false;
                    lnkBtn2Credit.Visible = false;
                    lbl2TDebit.Text = Convert.ToString(lbl2TDebit.Text);
                    lbl3TCredit.Text = Convert.ToString(lbl3TCredit.Text);
                }
                else
                {
                    lbl2TDebit.Visible = false;
                    lbl3TCredit.Visible = false;
                    lnkBtn2Debit.Visible = true;
                    lnkBtn2Credit.Visible = true;
                    lnkBtn2Debit.Text = Convert.ToString(lbl2TDebit.Text);
                    lnkBtn2Credit.Text = Convert.ToString(lbl3TCredit.Text);
                }
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
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
        #endregion

        
    }
}