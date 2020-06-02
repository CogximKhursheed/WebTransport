using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using System.IO;
using System.Configuration;
using System.Globalization;
using WebTransport.DAL;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;


namespace WebTransport
{
    public partial class BalanceSheetAc : Pagebase
    {
        #region Variables...
        string conString = "";
        double CloseBal = 0; double OpenBal = 0; double PurBal = 0; double SaleBal = 0;
        double profit = 0;
        int counter = 0, PGtRowNo = 0, ClStRowNo = 0, closBalOption = 0;
        DataSet Ds, DsTmpPur, DsTmpPurOneItem, DsOpRec;
        private int intFormId = 59;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                BindDateRange();
                SetDate();
            }
            txtdatefrm.Attributes.Add("onkeypress", "return not AllowAnything(event);");
            txtdateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Controls Events...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetDate();
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtdatefrm.Text.Trim() != "" && txtdateto.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdatefrm.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdateto.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('From date can not be greater than To date!');", true);
                        txtdatefrm.Focus();
                        return;
                    }
                }
                DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select ISNULL(cm.Comp_Name,'')Comp_Name,ISNULL(cm.Adress1,'')as Address1,ISNULL(cm.Adress2,'')as Address2,ISNULL(cm.City_Idno,'')as City_Name,ISNULL(cm.State_Idno,'')as State_Name,ISNULL(cm.Phone_Off,'')as Phone_Off,ISNULL(cm.Pan_No,'')as PAN_No,ISNULL(cm.CompGSTIN_No,'')as GST_No,ISNULL(cm.Pin_No,'')as Pin_No from tblcompmast cm ");
                if (CompDetl != null)
                {
                    if (CompDetl != null && CompDetl.Tables.Count > 0 && CompDetl.Tables[0].Rows.Count > 0)
                    {
                        string CompName = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
                        string Add1 = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"]);
                        string Add2 = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Address2"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Address2"]);
                        string PhNo = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                        string City = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
                        string State = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]);
                        string Pin_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
                        string GST_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["GST_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["GST_No"]); ;
                        string PAN_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["PAN_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["PAN_No"]);


                        if (string.IsNullOrEmpty(Add2) == false)
                        {
                            Add1 = Add1 + "," + Add2;
                        }
                        if (string.IsNullOrEmpty(City) == false)
                        {
                            Add1 = Add1 + "," + City;
                        }
                        if (string.IsNullOrEmpty(State) == false)
                        {
                            Add1 = Add1 + "," + State;
                        }
                        if (string.IsNullOrEmpty(Pin_No) == false)
                        {
                            Add1 = Add1 + "," + Pin_No;
                        }
                        if (string.IsNullOrEmpty(PhNo) == false)
                        {
                            PhNo = "PHONE:- " + PhNo;
                        }
                        if (string.IsNullOrEmpty(PAN_No) == false)
                        {
                            PAN_No = "PAN:- " + PAN_No;
                        }
                        if (string.IsNullOrEmpty(GST_No) == false)
                        {
                            GST_No = "GST No:- " + GST_No;
                        }
                        lblCompName.Text = CompName;
                        lblCompAddress.Text = Add1;
                        lblCompPhone.Text = PhNo;
                        lblGST.Text = GST_No;
                        lblPAN.Text = PAN_No;
                    }
                }
                DataTable dsTableMain = BindBalanceSheet();
                if (dsTableMain != null && dsTableMain.Rows.Count > 0)
                {
                    grdMain.DataSource = dsTableMain;
                    grdMain.DataBind();
                    imgPrint.Visible = true;
                    imgBtnExcel.Visible = true;
                }
                else
                {
                    grdMain.DataSource = dsTableMain;
                    grdMain.DataBind();
                    imgPrint.Visible = false;
                    imgBtnExcel.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion

        #region Functions...
        private void TmpPurBalCal(int i, double Qty, string colAmt)
        {
            DataRow[] dr; double ClQty, ClBal; ClQty = ClBal = 0;
            ClQty = Qty;
            dr = DsTmpPur.Tables[0].Select("Item_Idno='" + Convert.ToString(Ds.Tables[0].Rows[i]["ID"]) + "'");
            for (int j = 0; j < dr.Length; j++)
            {
                if (j == (dr.Length - 1))
                {
                    ClBal = ClBal + ClQty * (Convert.ToDouble(dr[j]["Item_Rate"]));
                    Ds.Tables[0].Rows[i][colAmt] = ClBal.ToString("N2");
                }

                if (ClQty > Convert.ToDouble(dr[j]["Qty"]))
                {
                    ClQty = ClQty - Convert.ToDouble(dr[j]["Qty"]);
                    ClBal = Convert.ToDouble(dr[j]["Qty"]) * Convert.ToDouble(dr[j]["Item_Rate"]);
                }
            }
        }
        private void clBalBlnkCal()
        {
            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
            {
                double CloseQty = Convert.ToDouble(Ds.Tables[0].Rows[i]["CloseQty"]);
                double ItemRate = Convert.ToDouble(Ds.Tables[0].Rows[i]["ITEM_RATE"]);
                double openqty = Convert.ToDouble(Ds.Tables[0].Rows[i]["OpenQty"]);

                if (Convert.ToString(Ds.Tables[0].Rows[i]["CloseAmt"]) == "0.00")

                    Ds.Tables[0].Rows[i]["CloseAmt"] = Convert.ToDouble(CloseQty * ItemRate).ToString("N2");

                if (Convert.ToString(Ds.Tables[0].Rows[i]["OpenAmt"]) == "0.00")

                    Ds.Tables[0].Rows[i]["OpenAmt"] = Convert.ToDouble(openqty * ItemRate).ToString("N2");

                CloseBal = CloseBal + Convert.ToDouble(Ds.Tables[0].Rows[i]["CloseAmt"]);
            }
        }
        private void gridRowDel(DataTable Dt)
        {
            bool PGtRowNoFalg = false; bool ClStRowNoFlag = false;
            for (int i = Dt.Rows.Count - 1; i > 0; i--)
            {
                if (Convert.ToString(Dt.Rows[i][0]) == "Purchase(s) Total")
                {
                    if (PGtRowNoFalg == false)
                    {
                        PGtRowNo = i; PGtRowNoFalg = true;
                    }
                }
                if (Convert.ToString(Dt.Rows[i][2]) == "Sales(s) Total ")
                {
                    if (ClStRowNoFlag == false)
                    {
                        ClStRowNo = i; ClStRowNoFlag = true;
                    }
                }
            }
            if (PGtRowNo > ClStRowNo)
            {
                if (PGtRowNo < (Dt.Rows.Count - 5))
                {
                    for (int i = PGtRowNo + 1; i <= (Dt.Rows.Count - 4); i++)
                    {
                        Dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
            }
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
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
                    txtdatefrm.Text = hidmindate.Value;
                    txtdateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtdatefrm.Text = hidmindate.Value;
                    txtdateto.Text = hidmaxdate.Value;
                }
            }
        }
        private DataTable BindBalanceSheet()
        {
            DataTable dtN = new DataTable();
            ProfitLossBookDAL objBSDAL = new ProfitLossBookDAL();
            DataSet dsCurLiab, dsCurAsset, dsLoans, dsCapAc,
                dsCapAcPnL, dsExpRev;
            Double dCapAcPnL = 0, dExpRev = 0;
            double profit = 0; PurBal = SaleBal = counter = 0;

            dsCurLiab = objBSDAL.SelectFinalBookwtDtRngeBS(ApplicationFunction.ConnectionString(), "SelectCurLiabWDtRng", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));
            dsCurAsset = objBSDAL.SelectFinalBookwtDtRngeBS(ApplicationFunction.ConnectionString(), "SelectCurAssetWDtRng", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));
            dsLoans = objBSDAL.SelectFinalBookwtDtRngeBS(ApplicationFunction.ConnectionString(), "SelectLoansWDtRng", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));
            dsCapAc = objBSDAL.SelectFinalBookwtDtRngeBS(ApplicationFunction.ConnectionString(), "SelectCapAccWDtRng", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));

            dsCapAcPnL = objBSDAL.SelectFinalBookBS(ApplicationFunction.ConnectionString(), "SelectCapAcPnL", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));
            dCapAcPnL = Convert.ToDouble(dsCapAcPnL.Tables[0].Rows[0]["Open_Purchase_Sale"]);


            counter = 1;

            #region Colum nd row created
            dtN.Rows.Add();
            dtN.Columns.Add("Item_Name");
            dtN.Columns.Add("Amount");
            dtN.Columns.Add("Item_Name1");
            dtN.Columns.Add("Amount1");
            dtN.Columns.Add("LH_AGRP_IDNO");
            dtN.Columns.Add("LHTyp");
            dtN.Columns.Add("RH_AGRP_IDNO");
            dtN.Columns.Add("RHTyp");
            dtN.Columns.Add("LTyp");
            dtN.Columns.Add("RTyp");
            var row1 = dtN.NewRow();
            row1["Item_Name"] = "";
            row1["Amount"] = "";
            row1["Item_Name1"] = "";
            row1["Amount1"] = "";
            row1["LH_AGRP_IDNO"] = "";
            row1["LHTyp"] = "";
            row1["RH_AGRP_IDNO"] = "";
            row1["RHTyp"] = "";
            row1["LTyp"] = "";
            row1["RTyp"] = "";
            dtN.Rows.Add(row1);

            #endregion

            #region Current Liability
            if (dsCurLiab != null)
            {
                if (dsCurLiab.Tables.Count > 0)
                {
                    dsCurLiab.Tables[0].TableName = "CurrentLiability";
                    if (dsCurLiab.Tables[0].Rows.Count > 0)
                    {
                        PurBal = Convert.ToDouble(dsCurLiab.Tables[0].Rows[dsCurLiab.Tables[0].Rows.Count - 1][1]);
                        dtN.Rows.Add();

                        dtN.Rows[counter][0] = "Current Liabilities";
                        dtN.Rows[counter][1] = "";
                        counter += 1;
                        for (int i = 0; i < dsCurLiab.Tables[0].Rows.Count; i++)
                        {
                            dtN.Rows.Add();
                            dtN.Rows[counter][0] = dsCurLiab.Tables[0].Rows[i]["Item_Name"];
                            dtN.Rows[counter][1] = dsCurLiab.Tables[0].Rows[i]["Amount"];
                            dtN.Rows[counter][4] = dsCurLiab.Tables[0].Rows[i]["AGRP_IDNO"];
                            dtN.Rows[counter][5] = "P";
                            dtN.Rows[counter][8] = dsCurLiab.Tables[0].Rows[i]["Typ"];
                            counter++;
                        }
                        counter += 1;
                    }
                }
            }
            #endregion

            #region Loans Received
            if (dsLoans != null)
            {
                if (dsLoans.Tables.Count > 0)
                {
                    dsLoans.Tables[0].TableName = "LoansReceived";
                    if (dsLoans.Tables[0].Rows.Count > 0)
                    {
                        PurBal = PurBal + Convert.ToDouble(dsLoans.Tables[0].Rows[dsLoans.Tables[0].Rows.Count - 1][1]);
                        //if (Convert.ToDouble(dsLoans.Tables[0].Rows[dsLoans.Tables[0].Rows.Count - 1][1]) > 0)
                        //{
                        dtN.Rows.Add();
                        dtN.Rows.Add(dsLoans.Tables[0].Rows.Count + 4);
                        dtN.Rows[counter][0] = "Loans Received";
                        dtN.Rows[counter][1] = "";
                        counter += 1;
                        for (int i = 0; i < dsLoans.Tables[0].Rows.Count; i++)
                        {
                            dtN.Rows.Add();
                            dtN.Rows[counter][0] = dsLoans.Tables[0].Rows[i]["Item_Name"];
                            dtN.Rows[counter][1] = dsLoans.Tables[0].Rows[i]["Amount"];
                            dtN.Rows[counter][4] = dsLoans.Tables[0].Rows[i]["AGRP_IDNO"];
                            dtN.Rows[counter][5] = "P";
                            dtN.Rows[counter][8] = dsLoans.Tables[0].Rows[i]["Typ"];
                            counter++;
                        }
                        counter += 1;
                        //}
                    }
                }
            }
            #endregion

            #region Capital Account
            if (dsCapAc != null && dsCapAc.Tables.Count > 0)
            {
                dsCapAc.Tables[0].TableName = "Capitals";
                if (dsCapAc.Tables[0].Rows.Count > 0)
                {
                    PurBal = PurBal + Convert.ToDouble(dsCapAc.Tables[0].Rows[dsCapAc.Tables[0].Rows.Count - 1][1]);
                    dtN.Rows.Add();
                    dtN.Rows[counter][0] = "Capitals";
                    dtN.Rows[counter][1] = "";
                    counter += 1;
                    for (int i = 0; i < dsCapAc.Tables[0].Rows.Count; i++)
                    {
                        dtN.Rows.Add();
                        dtN.Rows[counter][0] = dsCapAc.Tables[0].Rows[i]["Item_Name"];
                        dtN.Rows[counter][1] = dsCapAc.Tables[0].Rows[i]["Amount"];
                        dtN.Rows[counter][4] = dsCapAc.Tables[0].Rows[i]["AGRP_IDNO"];
                        dtN.Rows[counter][5] = "P";
                        dtN.Rows[counter][8] = dsCapAc.Tables[0].Rows[i]["Typ"];
                        counter++;
                    }
                    counter += 1;
                }
            }
            #endregion

            dtN.Rows.Add();
            dtN.Rows.Add();
            dtN.Rows[counter][0] = "Profit & Loss Account";
            dtN.Rows[counter][1] = dCapAcPnL.ToString("N2").Replace(",", "");
            counter += 1;

            #region Current Assest
            counter = 1;
            if (dsCurAsset != null && dsCurAsset.Tables.Count > 0)
            {
                dsCurAsset.Tables[0].TableName = "CurrentAssets";
                if (dsCurAsset.Tables[0].Rows.Count > 0)
                {
                    SaleBal = Convert.ToDouble(dsCurAsset.Tables[0].Rows[dsCurAsset.Tables[0].Rows.Count - 1]["Amount"]);
                    int row = (dsCurAsset.Tables[0].Rows.Count - dtN.Rows.Count) + 1;
                    if (row > 0)
                        dtN.Rows.Add(row + 1);
                    dtN.Rows[counter][2] = "Current Assets"; dtN.Rows[counter][3] = "";
                    for (int i = 0, j = 2; i < dsCurAsset.Tables[0].Rows.Count; i++, j++)
                    {
                        //dtN.Rows.Add();
                        dtN.Rows[j][2] = dsCurAsset.Tables[0].Rows[i]["Item_Name"];
                        dtN.Rows[j][3] = dsCurAsset.Tables[0].Rows[i]["Amount"];
                        dtN.Rows[j][6] = dsCurAsset.Tables[0].Rows[i]["AGRP_IDNO"];
                        dtN.Rows[j][7] = "S";
                        dtN.Rows[j][9] = dsCurAsset.Tables[0].Rows[i]["Typ"];
                        counter = j + 1;
                    }
                }
            }
            #endregion



            #region calac..
            dtN.Rows.Add();
            dtN.Rows.Add(); double prof, los; prof = los = 0;
            //profit = (RevenBal + grosPrft) - (ExpBal + groslos);
            profit = SaleBal - PurBal - dCapAcPnL;
            if (profit > 0)
            {
                dtN.Rows[dtN.Rows.Count - 1][0] = "Difference";
                dtN.Rows[dtN.Rows.Count - 1][1] = profit.ToString("N2");
                prof = profit; los = 0;
                dtN.Rows.Add();
            }
            else
            {
                dtN.Rows[dtN.Rows.Count - 1][2] = "Difference";
                dtN.Rows[dtN.Rows.Count - 1][3] = Convert.ToDouble(Math.Abs(profit)).ToString("N2");
                prof = 0; los = (Math.Abs(profit));
                dtN.Rows.Add();
            }
            dtN.Rows[dtN.Rows.Count - 1][0] = "Net Total";
            dtN.Rows[dtN.Rows.Count - 1][1] = Convert.ToDouble(PurBal + dCapAcPnL + prof).ToString("N2");
            dtN.Rows[dtN.Rows.Count - 1][2] = "Net Total";
            dtN.Rows[dtN.Rows.Count - 1][3] = Convert.ToDouble(SaleBal + los).ToString("N2");
            #endregion

            return dtN;
        }
        #endregion

        #region Grid Events...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.DataBind();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOpenAmnt = (Label)e.Row.FindControl("lblOpenAmnt");
                lblOpenAmnt.Text = (DataBinder.Eval(e.Row.DataItem, "Amount")).ToString() == "" ? "" : string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")).ToString("N", new CultureInfo("hi-IN")));
                Label lblAmnt = (Label)e.Row.FindControl("lblAmnt");
                lblAmnt.Text = (DataBinder.Eval(e.Row.DataItem, "Amount1")).ToString() == "" ? "" : string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount1")).ToString("N", new CultureInfo("hi-IN")));

                //  imgBtnPur
                string strLHTyp = (DataBinder.Eval(e.Row.DataItem, "LHTyp")).ToString();
                string strRHTyp = (DataBinder.Eval(e.Row.DataItem, "RHTyp")).ToString();
                Int32 strLHAgrpId = ((DataBinder.Eval(e.Row.DataItem, "LH_AGRP_IDNO")).ToString() == "" ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LH_AGRP_IDNO")));
                Int32 strRHAgrpId = ((DataBinder.Eval(e.Row.DataItem, "RH_AGRP_IDNO")).ToString() == "" ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RH_AGRP_IDNO")));
                double strLAmnt = ((DataBinder.Eval(e.Row.DataItem, "Amount")).ToString() == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")));
                double strRAmnt = ((DataBinder.Eval(e.Row.DataItem, "Amount1")).ToString() == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount1")));
                LinkButton lnkbtnPur = (LinkButton)e.Row.FindControl("lnkbtnPur");
                LinkButton lnkbtnSale = (LinkButton)e.Row.FindControl("lnkbtnSale");
                lnkbtnPur.Visible = lnkbtnSale.Visible = false;
                if (((strLHTyp == "P") && (strLHAgrpId != 0) && (strLAmnt != 0)))
                {
                    lnkbtnPur.Visible = true;
                }
                if (((strRHTyp == "S") && (strRHAgrpId != 0) && (strRAmnt != 0)))
                {
                    lnkbtnSale.Visible = true;
                }

                string strLItemName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Item_Name"));
                string strRItemNamee = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Item_Name1"));
                if ((strLItemName == "Current Liabilities") || (strLItemName == "Current Liabilitie(s) Total") || (strLItemName == "Loans Received") || (strLItemName == "Loan(s) Received Total") || (strLItemName == "Capitals") || (strLItemName == "Capital Account Total") || (strLItemName == "Profit & Loss Account"))
                {
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                }
                if ((strRItemNamee == "Current Assets") || (strRItemNamee == "Current Asset(s) Total"))
                {
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
                if ((strLItemName == "Difference"))
                {
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                }
                if ((strRItemNamee == "Difference"))
                {
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
                if ((strLItemName == "Net Total") || (strRItemNamee == "Net Total"))
                {
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {

            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Response.Redirect("AccTrailBal.aspx?agrpId=" + e.CommandArgument + "&DtrngId=" + ddlDateRange.SelectedItem.Value + "&FDt=" + txtdatefrm.Text + "&TDt=" + txtdateto.Text + "&RTyp=" + (e.CommandName == "cmdSaleDet" ? "S" : "P") + "", true);
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region Excel Print..
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow grv in grdMain.Rows)
            {
                LinkButton lnkbtnPur = (LinkButton)grv.FindControl("lnkbtnPur");
                LinkButton lnkbtnSale = (LinkButton)grv.FindControl("lnkbtnSale");
                lnkbtnPur.Visible = false;
                lnkbtnSale.Visible = false;
            }
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();

            foreach (GridViewRow grv in grdMain.Rows)
            {
                LinkButton lnkbtnPur = (LinkButton)grv.FindControl("lnkbtnPur");
                LinkButton lnkbtnSale = (LinkButton)grv.FindControl("lnkbtnSale");
                lnkbtnPur.Visible = true;
                lnkbtnSale.Visible = true;
            }
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
            string attachment = "attachment; filename=P&LReport.xls";
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
        #endregion
    }
}