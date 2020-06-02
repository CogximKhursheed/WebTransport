using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebTransport
{
    public partial class AccountBookRpt : Pagebase
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
                    lnkbtnPrint.Visible = false;
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = false;
                }
                chkBal.Visible = false;
                //ddlParty.Enabled = true;
                txtParty.Enabled = true;
                //BindCompany();
                BindPartyName();
                BindDateRange();
                SetDate();
              
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
            //RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");

            //RangeValidator2.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
            //RangeValidator2.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");
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
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Validfrom date can not be greater than Validto date!');", true);
                        ddlDateRange.Focus();
                        return;
                    }
                }

                this.BindGrid();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void lnkBtnSetdate_Click(object sender, EventArgs e)
        {

            AccountBookDAL objAccBookDAL11 = new AccountBookDAL();
            Session["OpengBalCRDR"] = null;
            if (hfPartNoId.Value!="0")//if (ddlParty.SelectedValue != "0")
            {
                if ((Convert.ToString(txtDateFrom.Text) != "") && (Convert.ToString(txtDateTo.Text) != ""))
                {
                    double db, cr, dbopbal, cropbal, totdb, totcr;
                    db = cr = dbopbal = cropbal = totdb = totcr = 0;
                    string OpeningBal = "0";
                    SqlConnection con = new SqlConnection(ApplicationFunction.ConnectionString());

                    db = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectCrDrdatewise',@AMNTTYPE=2,@DateFrom='" + Convert.ToString(txtDateFrom.Text) + "', @ACNTIDNO='" + Convert.ToString(hfPartNoId.Value) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    cr = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectCrDrdatewise',@AMNTTYPE=1,@DateFrom='" + Convert.ToString(txtDateFrom.Text) + "', @ACNTIDNO='" + Convert.ToString(hfPartNoId.Value) + "',@YEARIDNO='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    dbopbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=2, @AcntIdno='" + Convert.ToString(hfPartNoId.Value) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    cropbal = clsDataAccessFunction.ExecuteScaler("Exec [spVoucherEntry] @Action='SelectOpBal',@OpenType=1, @AcntIdno='" + Convert.ToString(hfPartNoId.Value) + "',@YearIdno='" + Convert.ToString(ddlDateRange.SelectedValue) + "'", con, true);
                    totdb = db + dbopbal; totcr = cr + cropbal;
                    if (totdb > totcr)
                    {
                        OpeningBal = Convert.ToDouble(totdb - totcr).ToString("N2") + "  Dr";
                        Session["OpengBalCRDR"] = totdb + "  Dr";
                    }
                    else if (totdb < totcr)
                    {
                        OpeningBal = Convert.ToDouble(totcr - totdb).ToString("N2") + "  Cr";
                        Session["OpengBalCRDR"] = totcr + "  Cr";
                    }
                    else
                    {
                        OpeningBal = "0.00" + "  Cr";
                        Session["OpengBalCRDR"] = "0.00" + "  Cr";
                    }

                    if (chkDayWise.Checked == false)
                    {
                        string url = "AccBLedger.aspx?startdate=" + Convert.ToString(txtDateFrom.Text) + "&enddate=" + Convert.ToString(txtDateTo.Text) + "&party=" + Convert.ToInt32(hfPartNoId.Value) + "&OpeningBal=" + OpeningBal + "&PartyName=" + Convert.ToString(txtParty.Text);
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    }
                    else
                    {
                        OpeningBal = OpeningBal.Substring(0, OpeningBal.Length - 2);
                        Response.Redirect("DayWiseBalReport.aspx?startdate=" + Convert.ToString(txtDateFrom.Text) + "&enddate=" + Convert.ToString(txtDateTo.Text) + "&party=" + Convert.ToInt32(hfPartNoId.Value) + "&OpeningBal=" + OpeningBal + "&PartyName=" + Convert.ToString(txtParty.Text));

                    }
                }
                else
                {
                    ShowMessageErr("Please Enter From And to Date");
                }
            }
            else
            {
                ShowMessageErr("Please Selcet Party Name");
            }
        }
        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {

            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            lblCompName.Text = lblCompName.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblAddress.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]) + "</br>" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            lblPhone.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            lblCity.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            lblState.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
            lblpincode.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);

            lblReport.Text = txtParty.Text;//Convert.ToString(ddlParty.SelectedItem);
            lblReportType.Text = Convert.ToString(ddlType.SelectedItem);
            lblDate.Text = "( " + Convert.ToString(txtDateFrom.Text) + " to " + Convert.ToString(txtDateTo.Text) + " )";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('divPrint')", true);

        }
        #endregion

        #region Bind Event...
        //protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindPartyName();
        //    ddlParty.Focus();
        //}
        private void BindPartyName()
        {
            //ddlParty.DataSource = objAccountBookDAL.FillCompwiseParty(ApplicationFunction.ConnectionString(), 0);
            //ddlParty.DataTextField = "Acnt_Name";
            //ddlParty.DataValueField = "Acnt_Idno";
            //ddlParty.DataBind();
            //objAccountBookDAL = null;
            //ddlParty.Items.Insert(0, new ListItem("--Select Party--", "0"));
            objAccountBookDAL.FillCompwiseParty(ApplicationFunction.ConnectionString(), 0);
            DataSet ds = objAccountBookDAL.SelectpartyID(txtParty.Text.Trim(), ApplicationFunction.ConnectionString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                hfPartNoId.Value = Convert.ToString(ds.Tables[0].Rows[0]["Acnt_Idno"]);
            }
            else
                hfPartNoId.Value = "0";

            if ((string.IsNullOrEmpty(hfPartNoId.Value) ? 0 : Convert.ToInt32(hfPartNoId.Value)) <= 0)
            {
                this.ShowMessageErr("Please Select Party or Correct party."); return;
            }
        }
        private void BindPartyStore(Int64 Id, int Typ)
        {
            //ddlParty.DataSource = objAccountBookDAL.FillStoreParty(Id, Typ);
            //ddlParty.DataTextField = "Acnt_Name";
            //ddlParty.DataValueField = "Acnt_Idno";
            //ddlParty.DataBind();
            //objAccountBookDAL = null;
            //ddlParty.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindGrid()
        {
            try
            {
                if (ddlType.SelectedValue == "1")
                {
                    //if (ddlcompany.SelectedValue == "0")
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Please Select Company Name!');", true);
                    //    msg = "Please Select Company Name!";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                    //    ddlcompany.Focus();
                    //    return;
                    //}
                    //if (ddlParty.SelectedValue == "0")
                    //{
                    //    msg = "Please Select Party Name!";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                    //    ddlParty.Focus();
                    //    return;
                    //}
                }
                DataTable dsTable1 = ConvertToDatatable();
                if (dsTable1 != null && dsTable1.Rows.Count > 0)
                {
                    grdMain.DataSource = dsTable1;
                    if (ddlType.SelectedValue == "1")
                    {
                        chkDayWise.Enabled = true;
                       
                    }
                    else
                    {  
                        chkDayWise.Enabled = false;
                    }

                    Double TotalNetAmount = 0; Double TotalNetDebitAmount = 0;

                    for (int i = 0; i < dsTable1.Rows.Count; i++)
                    {
                        TotalNetAmount += Convert.ToDouble(dsTable1.Rows[i]["CREDIT"]);
                        TotalNetDebitAmount += Convert.ToDouble(dsTable1.Rows[i]["DEBIT"]);
                    }
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                    lblDebitAmount.Text = TotalNetDebitAmount.ToString("N2");

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dsTable1.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    divpaging.Visible = true;

                    grdMain.DataBind();
                    prints.Visible = true;

                    //if (rdoOB.Checked == true || rdoRP.Checked == true)
                    if (ddlType.SelectedValue == "2")
                    {
                        grdMain.ShowFooter = false;
                        grdMain.FooterRow.Visible = true;
                    }
                }
                else
                {
                    grdMain.DataSource = null; chkDayWise.Enabled = false;
                    grdMain.DataBind();
                    prints.Visible = false;

                    lblcontant.Visible = false;
                    divpaging.Visible = false;
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
            AccountBookDAL objAccBookDAL = new AccountBookDAL();
            DataTable dTN = new DataTable();
            hidstr.Value = "";
            string strAction = "";
            string strDateFrom ="";
            string strDateTo = "";
            int intyearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
 
            strDateFrom = Convert.ToString(hidmindate.Value);
            strDateTo=Convert.ToString(hidmaxdate.Value);
            
            //Int64 intCompIdno = ddlcompany.SelectedItem.Text == "All" ? 0 : Convert.ToInt64(ddlcompany.SelectedValue);
            if (ddlType.SelectedValue == "1")
            {
                #region Ledger Report
                dTN.Columns.Add("PERTI");
                dTN.Columns.Add("CREDIT");
                dTN.Columns.Add("DEBIT");
                dTN.Columns.Add("Balance");
                dTN.Columns.Add("Mon");
                dTN.Columns.Add("Acnt_Idno");
                strAction = "SelectLedgerRpt";
                DataSet dsTable = objAccBookDAL.Fillledgergrid(ApplicationFunction.ConnectionString(), strAction, Convert.ToInt64(hfPartNoId.Value), intyearIdno, strDateFrom, strDateTo);
                #region
                if (dsTable != null && dsTable.Tables.Count > 0 && dsTable.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsTable.Tables[0].Rows[0]["PERTI"]) != " Opening Balance")
                    {
                        var row1 = dTN.NewRow();
                        row1["PERTI"] = "Opening Balance";
                        row1["CREDIT"] = "0.00";
                        row1["DEBIT"] = "0.00";
                        row1["Balance"] = "";
                        row1["Mon"] = "";
                        row1["Acnt_Idno"] = "";
                        dTN.Rows.Add(row1);


                        DataRow r = dsTable.Tables[0].NewRow();
                        r["PERTI"] = "Opening Balance";
                        r["CREDIT"] = "0.00";
                        r["DEBIT"] = "0.00";
                        r["Balance"] = "";
                        r["Mon"] = "";
                        dsTable.Tables[0].Rows.InsertAt(r, 0);
                        dsTable.AcceptChanges();
                    }
                    else
                    {
                        var row = dTN.NewRow();
                        row["PERTI"] = dsTable.Tables[0].Rows[0]["PERTI"].ToString();
                        row["CREDIT"] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[0]["CREDIT"]));
                        row["DEBIT"] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[0]["DEBIT"]));
                        row["Balance"] = dsTable.Tables[0].Rows[0]["Balance"].ToString();
                        row["Mon"] = dsTable.Tables[0].Rows[0]["Mon"].ToString();
                        row["Acnt_Idno"] = "";
                        dTN.Rows.Add(row);
                    }
                    //var row = dTN.NewRow();
                    //row["PERTI"] = dsTable.Tables[0].Rows[0]["PERTI"].ToString();
                    //row["CREDIT"] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[0]["CREDIT"]));
                    //row["DEBIT"] = string.Format("{0:0,0.00}", Convert.ToString(dsTable.Tables[0].Rows[0]["DEBIT"]));
                    //row["Balance"] = dsTable.Tables[0].Rows[0]["Balance"].ToString();
                    //row["Mon"] = dsTable.Tables[0].Rows[0]["Mon"].ToString();
                    //row["Acnt_Idno"] = "";
                    //dTN.Rows.Add(row);

                    int i, j, m, mon; i = j = m = mon = 0;
                    mon = 4;
                    j = 1;
                    for (i = 1; i <= 12; i++)
                    {
                        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                        if (i > 0)
                        {
                            if (mon > 12) mon = 1;
                            dTN.Rows.Add();
                            dTN.Rows[i][0] = (new ListItem(info.GetMonthName(mon).ToString(), mon.ToString()));
                            dTN.Rows[i][1] = dTN.Rows[i][2] = dTN.Rows[i][3] = "00.00";
                            dTN.Rows[i][4] = mon; mon += 1;
                        }
                    }
                    for (i = 0; i <= 12; i++)
                    {
                        if (i > 0)
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
                                        j += 1;
                                    }
                                }
                            }
                            else { break; }
                        }
                    }
                    if (Convert.ToString(dsTable.Tables[0].Rows[0][2]) != "00.00")
                        bal = Convert.ToDouble(dsTable.Tables[0].Rows[0][2]);
                    if (Convert.ToString(dsTable.Tables[0].Rows[0][1]) != "00.00")
                        bal1 = Convert.ToDouble(dsTable.Tables[0].Rows[0][1]);
                    double tot = 0;
                    for (i = 1; i <= 12; i++)
                    {
                        bal = bal + Convert.ToDouble(dTN.Rows[i][2]);
                        bal1 = bal1 + Convert.ToDouble(dTN.Rows[i][1]);
                        tot = bal - bal1;
                        if (tot >= 0)
                            dTN.Rows[i][3] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Dr";
                        else
                            dTN.Rows[i][3] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Cr";
                    }
                }
                #endregion
                #endregion
            }
            else if (ddlType.SelectedValue == "2")
            {
                #region Opening Balance....
                DataTable dsTable = CreateOpeningBalance(ApplicationFunction.ConnectionString(), strDateFrom, strDateTo, intyearIdno);
                //DataSet dsTable = objAccBookDAL.Fillopeningbalgrid(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlParty.SelectedValue), intyearIdno);
                if (dsTable != null && dsTable.Rows.Count > 0)
                {
                    dTN = dsTable;
                    var row = dTN.NewRow();
                    row["Particulars"] = "Grand Total";
                    row["Debit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(Debit)", "")));
                    row["Credit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(Credit)", "")));
                    dTN.Rows.Add(row);
                    String AmntDC = string.Format("{0:0,0.00}", (Convert.ToDouble(row["Debit"]) - Convert.ToDouble(row["Credit"])));
                    string AmntDC1 = AmntDC.Substring(0, 1);
                    var row2 = dTN.NewRow();
                    row2["Particulars"] = "Difference in Total";
                    if (AmntDC1 == "-")
                    {
                        row2["Credit"] = string.Format("{0:0,0.00}", 0);
                        row2["Debit"] = string.Format("{0:0,0.00}", Math.Abs(Convert.ToDouble(row["Debit"]) - Convert.ToDouble(row["Credit"])));
                    }
                    else
                    {
                        row2["Debit"] = string.Format("{0:0,0.00}", 0);
                        row2["Credit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["Debit"]) - Convert.ToDouble(row["Credit"])));
                    }
                    dTN.Rows.Add(row2);
                    var row3 = dTN.NewRow();
                    row3["Particulars"] = "Net Total";
                    row3["Debit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["Debit"]) + (Convert.ToDouble(row2["Debit"]))));
                    row3["Credit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["Credit"]) + (Convert.ToDouble(row2["Credit"]))));
                    dTN.Rows.Add(row3);
                }
                #endregion
            }
            else if (ddlType.SelectedValue == "3")
            {
                //#region Receivable/Payable...
                //DataSet dsTable = new DataSet();
                //ddlParty.Enabled = true;
                //Int64 intPartyIdno = ddlParty.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlParty.SelectedValue);
                //if (ddlcompany.SelectedIndex == 0)
                //{
                //    if (ddlParty.SelectedIndex == 0)
                //    {
                //        if (chkBal.Checked == true)
                //        {
                //            strAction = "ComallPrtyallTrue";
                //        }
                //        else
                //        {
                //            strAction = "ComallPrtyallFalse";
                //        }
                //    }
                //    else
                //    {
                //        if (chkBal.Checked == true)
                //        {
                //            strAction = "ComallPrtyoneTrue";
                //        }
                //        else
                //        {
                //            strAction = "ComallPrtyoneFalse";
                //        }
                //    }
                //}
                //else
                //{
                //    if (ddlParty.SelectedIndex == 0)
                //    {
                //        if (chkBal.Checked == true)
                //        {
                //            strAction = "ComonePrtyallTrue";
                //        }
                //        else
                //        {
                //            strAction = "ComonePrtyallFalse";
                //        }
                //    }
                //    else
                //    {
                //        if (chkBal.Checked == true)
                //        {
                //            strAction = "ComonePrtyoneTrue";
                //        }
                //        else
                //        {
                //            strAction = "ComonePrtyoneFalse";
                //        }
                //    }
                //}
                //dsTable = objAccBookDAL.FillReceivablePayable(conString, strAction, strDateFrom, strDateTo, intPartyIdno, intCompIdno, intyearIdno);

                //#region
                //if (dsTable != null && dsTable.Tables.Count > 0 && dsTable.Tables[0].Rows.Count > 0)
                //{
                //    dTN = dsTable.Tables[0];
                //    var row = dTN.NewRow();
                //    row["Group"] = "Grand Total";
                //    row["SubGroup"] = "";
                //    row["Particulars"] = "";
                //    row["OpeningDebit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(OpeningDebit)", "")));
                //    row["OpeningCredit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(OpeningCredit)", "")));
                //    row["DebitAmount"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(DebitAmount)", "")));
                //    row["CreditAmount"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(CreditAmount)", "")));
                //    dTN.Rows.Add(row);
                //}
                //#endregion
                //#endregion
            }

            objAccountBookDAL = null;
            return dTN;
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private DataTable CreateOpeningBalance(string conString, string strDateFrom, string strDateTo, int intyearIdno)
        {

            AccountBookDAL objAccBookDAL11 = new AccountBookDAL();
            DataTable ds11 = new DataTable();
            hidstr.Value = "";
            ////if (ddlParty.SelectedValue.Count() > 0)
            //if (ddlParty.Items.Count > 1)
            //{
                //if (ddlParty.SelectedIndex == 0)
                //{
                //    for (int count = 1; count <= ddlParty.Items.Count - 1; count++)
                //    {
                //        hidstr.Value = ddlParty.Items[count].Value + "," + hidstr.Value;
                //    }
                //}
                //else
                //{
                //    hidstr.Value = hfPartNoId.SelectedValue + ",";
                //}
                 hidstr.Value = hfPartNoId.Value;
                if (hidstr.Value != "" && Convert.ToString(hidstr.Value).Length > 1)
                {
                    hidstr.Value = hidstr.Value.Substring(0, hidstr.Value.Length - 1);
                }
                string[] Arr = Convert.ToString(hidstr.Value).Split(new char[] { ',' });
                foreach (string I in Arr)
                {
                    DataTable dtOutput = objAccBookDAL11.OpeningBalGrid(ApplicationFunction.ConnectionString(), "OpeningBalGrdyr", strDateFrom, strDateTo, intyearIdno, Convert.ToInt64(I));
                    if (dtOutput != null && dtOutput.Rows.Count > 0)
                    {
                        if (ds11 == null || ds11.Rows.Count == 0)
                        {
                            ds11.Merge(dtOutput);
                        }
                        else
                        {
                            ds11.Merge(dtOutput);
                        }
                    }
                }
            //}
            return ds11;
        }
        #endregion

        #region Grid Events...
       
        
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdshowdetail")
            {
                if (chkDayWise.Checked == false)
                {
                    #region Ledger Account
                    string OpeningBal = "0";
                    DataTable DtLedger = ConvertToDatatable();
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int PreviousRow = row.RowIndex - 1;
                    LinkButton lnkBtnParticular = (LinkButton)row.FindControl("lnkBtnParticular");
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
                    int year = 0;
                    if (monthNo > 3 && monthNo <= 12)
                    {
                        year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year;
                    }
                    else
                    {
                        year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year + 1;
                    }

                    string startDate = Convert.ToString("01-" + monthNo + "-" + year); //dd-MM-yyyy
                    string endDate = Convert.ToString(DateTime.DaysInMonth(year, monthNo) + "-" + monthNo + "-" + year); //dd-MM-yyyy
                   
                     int partyId = Convert.ToInt32(hfPartNoId.Value);
                     string PartyName1 = Convert.ToString(txtParty.Text);
                     Session["PartyName"] = PartyName1;
                     string PartyName = PartyName1.Replace("&", " ");
                   
                    string url = "AccBLedger.aspx?startdate=" + startDate + "&enddate=" + endDate + "&party=" + partyId + "&OpeningBal=" + OpeningBal + "&PartyName=" + PartyName;
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    
                    #endregion
                }
                else
                {
                    #region Ledger Account
                    string OpeningBal = "0";
                    DataTable DtLedger = ConvertToDatatable();
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int PreviousRow = row.RowIndex - 1;
                    LinkButton lnkBtnParticular = (LinkButton)row.FindControl("lnkBtnParticular");
                    if (lnkBtnParticular.Text == "April")
                    {
                        Label lblDebitOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl2Debit");
                        Label lblCreditOP = (Label)grdMain.Rows[PreviousRow].FindControl("lbl3Credit");
                        Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                        Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                        if (DebitOP > CreditOP)
                        {
                            OpeningBal = DebitOP.ToString("N2");
                            Session["OpengBalCRDR"] = OpeningBal + "Dr";
                        }
                        else
                        {
                            OpeningBal = CreditOP.ToString("N2");
                            Session["OpengBalCRDR"] = OpeningBal + "Cr";
                        }
                    }
                    else
                    {

                        Label lbl4Balance = (Label)grdMain.Rows[PreviousRow].FindControl("lbl4Balance");
                        OpeningBal = lbl4Balance.Text;
                        OpeningBal = OpeningBal.Substring(0, OpeningBal.Length - 2);
                        Session["OpengBalCRDR"] = lbl4Balance.Text;
                    }
                    int monthNo = 0;
                    if (OpeningBal.ToLower() != "april")
                    {
                        monthNo = ApplicationFunction.monthNo(lnkBtnParticular.Text.Trim());
                    }
                    int year = 0;
                    if (monthNo > 3 && monthNo <= 12)
                    {
                        year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year;
                    }
                    else
                    {
                        year = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year + 1;
                    }
                    string startDate = Convert.ToString("01-" + monthNo + "-" + year); //dd-MM-yyyy
                    string endDate = Convert.ToString(DateTime.DaysInMonth(year, monthNo) + "-" + monthNo + "-" + year); //dd-MM-yyyy
                    int partyId = Convert.ToInt32(hfPartNoId.Value);
                    string PartyName1 = Convert.ToString(txtParty.Text);
                    Session["PartyName"] = PartyName1;
                    string PartyName = PartyName1.Replace("&", " ");
                    
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertpopup", "ShowPopup('" + startDate + "','" + endDate + "','" + partyId + "','" + OpeningBal + "','" + PartyName + "')", true);
                    Response.Redirect("DayWiseBalReport.aspx?startdate=" + startDate + "&enddate=" + endDate + "&party=" + partyId + "&OpeningBal=" + OpeningBal + "&PartyName=" + PartyName);

                    #endregion
                }
            }
            else if (e.CommandName == "cmdshowdetail1D")
            {

                if (chkDayWise.Checked == false)
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


                    string StartDate = Convert.ToString(txtDateFrom.Text.Trim());
                    string EndDate = Convert.ToString(txtDateTo.Text.Trim());
                    int PartyIdno = Convert.ToInt32(hfPartNoId.Value);
                    string FrmType = "LdgrAll";
                    string PartyName1 = Convert.ToString(txtParty.Text);
                    Session["PartyName"] = PartyName1;
                    string PartyName = PartyName1.Replace("&", " ");

                      string url = "AccBLedger.aspx?startdate=" + StartDate + "&enddate=" + EndDate + "&party=" + PartyIdno + "&FrmType=" + FrmType + "&OpeningBal=" + OpeningBal + "&PartyName=" + PartyName;
                        string fullURL = "window.open('" + url + "', '_blank' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                   
                    #endregion
                }
                else
                {
                    #region Ledger Year Wise Details...
                    string OpeningBal = "0";
                    Label lblDebitOP = (Label)grdMain.Rows[0].FindControl("lbl2Debit");
                    Label lblCreditOP = (Label)grdMain.Rows[0].FindControl("lbl3Credit");
                    Double DebitOP = Convert.ToDouble(lblDebitOP.Text.Trim());
                    Double CreditOP = Convert.ToDouble(lblCreditOP.Text.Trim());
                    if (DebitOP > CreditOP)
                    {
                        OpeningBal = DebitOP.ToString("N2");
                        Session["OpengBalCRDR"] = OpeningBal + "Cr";
                    }
                    else
                    {
                        OpeningBal = CreditOP.ToString("N2");
                        Session["OpengBalCRDR"] = OpeningBal + "Cr";
                    }

                    string StartDate = Convert.ToString(txtDateFrom.Text.Trim());
                    string EndDate = Convert.ToString(txtDateTo.Text.Trim());
                    int PartyIdno = Convert.ToInt32(hfPartNoId.Value);
                    string FrmType = "LdgrAll";
                    string PartyName1 = Convert.ToString(txtParty.Text);
                    Session["PartyName"] = PartyName1;
                    string PartyName = PartyName1.Replace("&", " ");
                    
                        Response.Redirect("DayWiseBalReport.aspx?startdate=" + StartDate + "&enddate=" + EndDate + "&party=" + PartyIdno + "&FrmType=" + FrmType + "&OpeningBal=" + OpeningBal + "&PartyName=" + PartyName);

                    #endregion
                }
            }
            else if (e.CommandName == "cmdshowdetailTB")
            {
                #region TrailBalance.
                int party = 0; string Party11 = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                LinkButton lnkbtn7Particular = (LinkButton)row.FindControl("lnkbtn7Particular");
                party = Convert.ToInt32(lnkbtn7Particular.CommandArgument);
                Party11 = lnkbtn7Particular.Text.Trim();
                string Date1 = DateTime.Now.Date.Year.ToString();
                string Datefrm = Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)).ToString("dd-MM-yyyy");
                string dateto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)).ToString("dd-MM-yyyy");
                int year = Convert.ToInt32(ddlDateRange.SelectedValue);
                int year1 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year;
                Session["Year1"] = year1;
                Session["PartyName"] = Party11;
                string PartyName = Party11.Replace("&", " ");
                
                
                Response.Redirect("AccBTrailBaln.aspx?party=" + party + "&Datefrm=" + Datefrm + "&DateTo=" + dateto + "&Year=" + year + "&PartyName=" + PartyName + "&Year1=" + year1);
                
                #endregion
            }
            else if (e.CommandName == "cmdshowdetailOB")
            {
                #region Opening Balance
                int partyId = 0; string Party11 = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                LinkButton lnkbtn16Ledger = (LinkButton)row.FindControl("lnkbtn16Ledger");
                partyId = Convert.ToInt32(lnkbtn16Ledger.CommandArgument);
                Party11 = lnkbtn16Ledger.Text.Trim();
                Session["partyId"] = partyId;
                string PartyName = Party11.Replace("&", " ");
                Response.Redirect("LedgerMaster.aspx?AcntIdno=" + partyId + "&DlrType=" + 0);
                #endregion
            }

        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdMain.Columns[0].Visible = grdMain.Columns[1].Visible = grdMain.Columns[2].Visible = grdMain.Columns[3].Visible = true;
                grdMain.Columns[4].Visible = grdMain.Columns[5].Visible = grdMain.Columns[6].Visible = grdMain.Columns[7].Visible = grdMain.Columns[8].Visible = false;
                grdMain.Columns[9].Visible = grdMain.Columns[10].Visible = grdMain.Columns[11].Visible = grdMain.Columns[12].Visible = false;
                grdMain.Columns[13].Visible = grdMain.Columns[14].Visible = grdMain.Columns[15].Visible = grdMain.Columns[16].Visible = grdMain.Columns[17].Visible = grdMain.Columns[18].Visible = false;
                if (ddlType.SelectedValue == "1")
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

                    grdMain.Columns[4].Visible = false;
                    grdMain.Columns[5].Visible = false;
                    grdMain.Columns[6].Visible = false;
                    grdMain.Columns[7].Visible = false;
                    grdMain.Columns[8].Visible = false;
                    grdMain.Columns[9].Visible = false;
                    grdMain.Columns[10].Visible = false;
                    grdMain.Columns[18].Visible = false;

                    dNetAmount1 = dNetAmount1 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DEBIT"));
                    dNetAmount2 = dNetAmount2 + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CREDIT"));
                    #endregion
                }
                //else if (rdoRP.Checked == true)
                //{
                //    #region FillGrid for Receivable and Payable Reports...

                //    Label lbl5Group = (Label)e.Row.FindControl("lbl5Group");
                //    lbl5Group.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                //    string Group = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                //    Label lbl6SubGroup = (Label)e.Row.FindControl("lbl6SubGroup");
                //    lbl6SubGroup.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SubGroup"));
                //    Label lbl7Particular = (Label)e.Row.FindControl("lbl7Particular");
                //    lbl7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                //    LinkButton lnkbtn7Particular = (LinkButton)e.Row.FindControl("lnkbtn7Particular");
                //    lnkbtn7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                //    Label lbl8ODebit = (Label)e.Row.FindControl("lbl8ODebit");
                //    lbl8ODebit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpeningDebit")).ToString("N", new CultureInfo("hi-IN")));
                //    Label lbl9OCredit = (Label)e.Row.FindControl("lbl9OCredit");
                //    lbl9OCredit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpeningCredit")).ToString("N", new CultureInfo("hi-IN")));
                //    Label lbl10DebitA = (Label)e.Row.FindControl("lbl10DebitA");
                //    lbl10DebitA.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount")).ToString("N", new CultureInfo("hi-IN")));
                //    Label lbl11CreditA = (Label)e.Row.FindControl("lbl11CreditA");
                //    lbl11CreditA.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CreditAmount")).ToString("N", new CultureInfo("hi-IN")));

                //    string DebitRP = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount")).ToString("N", new CultureInfo("hi-IN")));
                //    string CreditRP = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CreditAmount")).ToString("N", new CultureInfo("hi-IN")));
                //    if (DebitRP.ToLower() == "0.00" && CreditRP.ToLower() == "0.00")
                //    {
                //        lnkbtn7Particular.Visible = false;
                //        lbl7Particular.Visible = true;
                //        lbl7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                //    }
                //    else
                //    {
                //        lnkbtn7Particular.Visible = true;
                //        lbl7Particular.Visible = false;
                //        lnkbtn7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                //    }

                //    if (Group.ToLower() == "grand total")
                //    {
                //        e.Row.BackColor = System.Drawing.Color.DodgerBlue;
                //    }
                //    if (Group.ToLower() == "difference in total")
                //    {
                //        e.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
                //    }
                //    if (Group.ToLower() == "net total")
                //    {
                //        e.Row.BackColor = System.Drawing.Color.DarkTurquoise;
                //    }
                //    grdMain.Columns[0].Visible = false;
                //    grdMain.Columns[1].Visible = false;
                //    grdMain.Columns[2].Visible = false;
                //    grdMain.Columns[3].Visible = false;
                //    grdMain.Columns[4].Visible = true;
                //    grdMain.Columns[5].Visible = true;
                //    grdMain.Columns[6].Visible = true;
                //    if (chkBal.Checked == true)
                //    {
                //        grdMain.Columns[7].Visible = false;
                //        grdMain.Columns[8].Visible = false;
                //    }
                //    else
                //    {
                //        grdMain.Columns[7].Visible = true;
                //        grdMain.Columns[8].Visible = true;
                //    }
                //    grdMain.Columns[9].Visible = true;
                //    grdMain.Columns[10].Visible = true;
                //    #endregion
                //}
                else if (ddlType.SelectedValue == "2")
                {
                    #region FillGrid for Opening Balance Report...
                    grdMain.Columns[0].Visible = false;
                    grdMain.Columns[1].Visible = false;
                    grdMain.Columns[2].Visible = false;
                    grdMain.Columns[3].Visible = false;

                    grdMain.Columns[15].Visible = true;
                    grdMain.Columns[16].Visible = true;
                    grdMain.Columns[17].Visible = true;
                    grdMain.Columns[18].Visible = true;

                    Label lbl16Ledger = (Label)e.Row.FindControl("lbl16Ledger");
                    string Ledger = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                    lbl16Ledger.Text = Ledger;
                    LinkButton lnkbtn16Ledger = (LinkButton)e.Row.FindControl("lnkbtn16Ledger");
                    lnkbtn16Ledger.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                    lnkbtn16Ledger.Visible = true;
                    lbl16Ledger.Visible = false;

                    Label lbl17LH = (Label)e.Row.FindControl("lbl17LH");
                    lbl17LH.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Ldgr_Head"));
                    Label lbl18Dbt = (Label)e.Row.FindControl("lbl18Dbt");
                    lbl18Dbt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit")).ToString("N", new CultureInfo("hi-IN")));
                    Label lbl19Crd = (Label)e.Row.FindControl("lbl19Crd");
                    lbl19Crd.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")).ToString("N", new CultureInfo("hi-IN")));
                    if (Ledger.ToLower() == "grand total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DodgerBlue;
                        lbl16Ledger.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                        lnkbtn16Ledger.Visible = false;
                        lbl16Ledger.Visible = true;
                    }
                    if (Ledger.ToLower() == "difference in total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
                        lbl16Ledger.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                        lnkbtn16Ledger.Visible = false;
                        lbl16Ledger.Visible = true;
                    }
                    if (Ledger.ToLower() == "net total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DarkTurquoise;
                        lbl16Ledger.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                        lnkbtn16Ledger.Visible = false;
                        lbl16Ledger.Visible = true;
                    }
                    grdMain.FooterStyle.CssClass = "WhiteCss";
                    #endregion
                }
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
            if (ddlType.SelectedValue == "1")
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = hidmaxdate.Value;
            }
            else
            {
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
        }
        #endregion

        #region ddlType_OnSelectedIndexChanged
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "1")
            {

            }
            else if (ddlType.SelectedValue == "2")
            {
                chkBal.Visible = false;
                txtParty.Enabled = true;
                grdMain.DataSource = null; chkDayWise.Enabled = false;
                grdMain.DataBind();
                txtParty.Focus();
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
            else if (ddlType.SelectedValue == "3")
            {
                chkBal.Visible = true;
                chkBal.Checked = true;
                txtParty.Enabled = false; chkDayWise.Enabled = false;
                grdMain.DataSource = null;
                txtParty.Enabled = true;
                grdMain.DataBind();
                txtParty.Focus();
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }


        }
        #endregion

        //protected void txtParty_TextChanged(object sender, EventArgs e)
        //{
        //    AccountBookDAL obj = new AccountBookDAL();
        //    DataSet ds = obj.SelectpartyID(txtParty.Text.Trim(), ApplicationFunction.ConnectionString());
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        hfPartNoId.Value = Convert.ToString(ds.Tables[0].Rows[0]["Acnt_Idno"]);
        //    }
        //    else
        //        hfPartNoId.Value = "0";

        //    if ((string.IsNullOrEmpty(hfPartNoId.Value) ? 0 : Convert.ToInt32(hfPartNoId.Value)) <= 0)
        //    {
        //        this.ShowMessageErr("Please Select Party or Correct party."); return;
        //    }
        //}

        [System.Web.Script.Services.ScriptMethod()]

        [System.Web.Services.WebMethod]
        public static string[] GetPartNo(string prefixText)
        {
            string constr = ApplicationFunction.ConnectionString();
            List<string> PartNumber = new List<string>();
            DataTable dtNames = new DataTable();
            AccountBookDAL obj = new AccountBookDAL();
            DataSet dt = obj.SelectPartyList(prefixText, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Name"]), Convert.ToString(dt.Tables[0].Rows[i]["Acnt_Idno"]));
                    PartNumber.Add(item);
                }
                return PartNumber.ToArray();
            }
            else
            {
                return null;
            }
        }

    }
}