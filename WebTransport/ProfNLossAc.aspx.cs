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
using Microsoft.ApplicationBlocks.Data;
using WebTransport.DAL;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class ProfNLossAc : Pagebase
    {
        string conString = "";
        double CloseBal = 0; double OpenBal = 0; double PurBal = 0; double SaleBal = 0;
        double profit = 0; double ExpBal = 0; double RevenBal = 0;
        int counter = 0, PGtRowNo = 0, ClStRowNo = 0, closBalOption = 0;
        DataSet Ds, DsTmpPur, DsTmpPurOneItem, DsOpRec;
        private int intFormId = 58;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                BindDateRange();
                SetDate();
                Datefrom.Attributes.Add("onkeypress", "return not AllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
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
                    Datefrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    txtDateTo.Text = hidmaxdate.Value;
                }
            }
        }

         private DataTable BindProfitLoss()
        {
            DataTable dtN = new DataTable();
            ProfitLossBookDAL objPLDAL = new ProfitLossBookDAL();
            DataSet dsGrossPL, dsExpend, dsReven;
            dsGrossPL = dsExpend = dsReven = null;

            

            //dsGrossPL = objPLDAL.SelectFinalBookPL1(conString, "SelectOpeningBal", txtdatefrm.Text.Trim(), txtdateto.Text.Trim(), Convert.ToInt32(ddlSeelction.SelectedValue), icompid);
            dsExpend = objPLDAL.SelectFinalBookProfitLosswtDtRnge(ApplicationFunction.ConnectionString(), "SelectExpnsWDtRng", Datefrom.Text.Trim(), txtDateTo.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));
            dsReven = objPLDAL.SelectFinalBookProfitLosswtDtRnge(ApplicationFunction.ConnectionString(), "SelectRevWDtRng", Datefrom.Text.Trim(), txtDateTo.Text.Trim(), Convert.ToInt32(ddlDateRange.SelectedValue));

            #region Create Row-Column...
            dtN.Columns.Add("Item_Name");
            dtN.Columns.Add("Open_Amnt");
            dtN.Columns.Add("Item_Namee");
            dtN.Columns.Add("Amount");
            dtN.Columns.Add("LH_AGRP_IDNO");
            dtN.Columns.Add("LHTyp");
            dtN.Columns.Add("RH_AGRP_IDNO");
            dtN.Columns.Add("RHTyp");
            dtN.Columns.Add("LTyp");
            dtN.Columns.Add("RTyp");
            var row = dtN.NewRow();
            row["Item_Name"] = "";
            row["Open_Amnt"] = "";
            row["Item_Namee"] = "";
            row["Amount"] = "";
            row["LH_AGRP_IDNO"] = "";
            row["LHTyp"] = "";
            row["RH_AGRP_IDNO"] = "";
            row["RHTyp"] = "";
            row["LTyp"] = "";
            row["RTyp"] = "";
            dtN.Rows.Add(row);
            #endregion

            double grosPrft, groslos; grosPrft = groslos = 0;
            if ((dsGrossPL != null) && (dsGrossPL.Tables.Count > 0) && (dsGrossPL.Tables[0].Rows.Count > 0))
            {
                if (Convert.ToDouble(dsGrossPL.Tables[0].Rows[0][0]) >= 0)
                {
                    dtN.Rows[0][2] = "Gross Profit";
                    dtN.Rows[0][3] = Convert.ToDouble(dsGrossPL.Tables[0].Rows[0][0]).ToString("N2");
                    grosPrft = Convert.ToDouble(dsGrossPL.Tables[0].Rows[0][0]); groslos = 0;
                }
                else
                {
                    dtN.Rows[0][0] = "Gross Loss";
                    dtN.Rows[0][1] = Convert.ToDouble(Math.Abs(Convert.ToDouble(dsGrossPL.Tables[0].Rows[0][0]))).ToString("N2");
                    grosPrft = 0; groslos = (Math.Abs(Convert.ToDouble(dsGrossPL.Tables[0].Rows[0][0])));
                }
            }
            else
            {
                dtN.Rows[0][2] = " Gross Profit";
                dtN.Rows[0][3] = "0.00";
            }

            #region Expenses
            counter = 2;
            dtN.Rows.Add();
            if ((dsExpend != null) && (dsExpend.Tables.Count > 0) && (dsExpend.Tables[0].Rows.Count > 0))
            {
                dsExpend.Tables[0].TableName = "Expense";
                ExpBal = Convert.ToDouble(dsExpend.Tables[0].Rows[dsExpend.Tables[0].Rows.Count - 1][1]);
                dtN.Rows.Add(dsExpend.Tables[0].Rows.Count + 2);
                dtN.Rows[counter - 1][0] = "Expenses"; dtN.Rows[counter - 1][1] = "";
                for (int i = 0; i < dsExpend.Tables[0].Rows.Count; i++)
                {
                    dtN.Rows.Add();
                    dtN.Rows[counter][0] = dsExpend.Tables[0].Rows[i]["Item_Name"];
                    dtN.Rows[counter][1] = dsExpend.Tables[0].Rows[i]["Amount"];
                    dtN.Rows[counter][1] = (Convert.ToString(dsExpend.Tables[0].Rows[i]["Amount"]) == "" ? "" : Convert.ToDouble(dsExpend.Tables[0].Rows[i]["Amount"]).ToString("N2"));
                    dtN.Rows[counter][4] = dsExpend.Tables[0].Rows[i]["AGRP_IDNO"];
                    dtN.Rows[counter][5] = "P";
                    dtN.Rows[counter][8] = dsExpend.Tables[0].Rows[i]["Typ"];
                    counter++;
                }
                counter += 2;
            }
            #endregion

            #region Revenue
            counter = 2;
            if ((dsReven != null) && (dsReven.Tables.Count > 0) && (dsReven.Tables[0].Rows.Count > 0))
            {
                dsReven.Tables[0].TableName = "Revenue";
                RevenBal = Convert.ToDouble(dsReven.Tables[0].Rows[dsReven.Tables[0].Rows.Count - 1]["Amount"]);
                int row1 = (dsReven.Tables[0].Rows.Count - dtN.Rows.Count) + 1;
                if (row1 > 2)
                    dtN.Rows.Add(row);
                dtN.Rows[counter - 1][2] = "Revenue"; dtN.Rows[counter - 1][3] = "";
                for (int i = 0, j = counter; i < dsReven.Tables[0].Rows.Count; i++, j++)
                {
                    //dtN.Rows.Add();
                    dtN.Rows[j][2] = dsReven.Tables[0].Rows[i]["Item_Name"];
                    dtN.Rows[j][3] = dsReven.Tables[0].Rows[i]["Amount"];
                    dtN.Rows[j][3] = (Convert.ToString(dsReven.Tables[0].Rows[i]["Amount"]) == "" ? "" : Convert.ToDouble(dsReven.Tables[0].Rows[i]["Amount"]).ToString("N2"));
                    dtN.Rows[j][6] = dsReven.Tables[0].Rows[i]["AGRP_IDNO"];
                    dtN.Rows[j][7] = "S";
                    dtN.Rows[j][9] = dsReven.Tables[0].Rows[i]["Typ"];
                    counter = j + 1;
                }
            }
            #endregion

            #region calac..
            dtN.Rows.Add(); double prof, los; prof = los = 0;
            profit = (RevenBal + grosPrft) - (ExpBal + groslos);
            //profit = RevenBal - ExpBal;
            if (profit > 0)
            {
                dtN.Rows[dtN.Rows.Count - 1][0] = "Net Profit";
                dtN.Rows[dtN.Rows.Count - 1][1] = profit.ToString("N2");
                prof = profit; los = 0;
                dtN.Rows.Add();
            }
            else
            {
                dtN.Rows[dtN.Rows.Count - 1][2] = "Net Loss";
                dtN.Rows[dtN.Rows.Count - 1][3] = Convert.ToDouble(Math.Abs(profit)).ToString("N2");
                prof = 0; los = (Math.Abs(profit));
                dtN.Rows.Add();
            }
            dtN.Rows[dtN.Rows.Count - 1][0] = "Net Total";
            dtN.Rows[dtN.Rows.Count - 1][1] = Convert.ToDouble(ExpBal + prof + groslos).ToString("N2");
            dtN.Rows[dtN.Rows.Count - 1][2] = "Net Total";
            dtN.Rows[dtN.Rows.Count - 1][3] = Convert.ToDouble(RevenBal + los + grosPrft).ToString("N2");
            #endregion

            return dtN;
        }

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
                double ItemRate = Convert.ToDouble(Ds.Tables[0].Rows[i]["ITEM_RATE"]); //Convert.IsDBNull(Convert.ToDouble(Ds.Tables[0].Rows[i]["ITEM_RATE"])) ? 0 :
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
                    //   Ds.Tables[0].Rows.RemoveRange((PGtRowNo + 1), (Ds.Tables[0].Rows.Count - 5) - PGtRowNo);
                    for (int i = PGtRowNo + 1; i <= (Dt.Rows.Count - 4); i++)
                    {
                        Dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                //if (ClStRowNo < (Ds.Tables[0].Rows.Count - 5))
                //    Ds.Tables[0].Rows.RemoveRange((ClStRowNo + 1), (Ds.Tables[0].Rows.Count - 5) - ClStRowNo);
            }
        }

       


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
            /* Verifies that the control is rendered */
        }
        #endregion

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

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Datefrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('From date can not be greater than To date!');", true);
                        Datefrom.Focus();
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
                DataTable dsTableMain = BindProfitLoss();
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

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.DataBind();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Response.Redirect("AccTrailBal.aspx?agrpId=" + e.CommandArgument + "&DtrngId=" + ddlDateRange.SelectedItem.Value + "&FDt=" + Datefrom.Text + "&TDt=" + txtDateTo.Text + "&RTyp=" + (e.CommandName == "cmdSaleDet" ? "S" : "P") + "", true);
            }
            catch (Exception Ex)
            {
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOpenAmnt = (Label)e.Row.FindControl("lblOpenAmnt");
                lblOpenAmnt.Text = (DataBinder.Eval(e.Row.DataItem, "Open_Amnt")).ToString() == "" ? "" : string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Open_Amnt")).ToString("N", new CultureInfo("hi-IN")));
                Label lblAmnt = (Label)e.Row.FindControl("lblAmnt");
                lblAmnt.Text = (DataBinder.Eval(e.Row.DataItem, "Amount")).ToString() == "" ? "" : string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")).ToString("N", new CultureInfo("hi-IN")));

                //  imgBtnPur
                string strLHTyp = (DataBinder.Eval(e.Row.DataItem, "LHTyp")).ToString();
                string strRHTyp = (DataBinder.Eval(e.Row.DataItem, "RHTyp")).ToString();
                Int32 strLHAgrpId = ((DataBinder.Eval(e.Row.DataItem, "LH_AGRP_IDNO")).ToString() == "" ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LH_AGRP_IDNO")));
                Int32 strRHAgrpId = ((DataBinder.Eval(e.Row.DataItem, "RH_AGRP_IDNO")).ToString() == "" ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RH_AGRP_IDNO")));
                double strLAmnt = ((DataBinder.Eval(e.Row.DataItem, "Open_Amnt")).ToString() == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Open_Amnt")));
                double strRAmnt = ((DataBinder.Eval(e.Row.DataItem, "Amount")).ToString() == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")));
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
                string strRItemNamee = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Item_Namee"));
                if ((strLItemName == "Gross Loss") || (strLItemName == "Expenses") || (strLItemName == "Expense(s) Total") || (strLItemName == "Indirect Expenses") || (strLItemName == "Indirect Expense(s) Total"))
                {
                    //e.Row.Cells[1].ForeColor = e.Row.Cells[2].ForeColor = System.Drawing.Color.DarkBlue;
                    //e.Row.Cells[1].BackColor = e.Row.Cells[2].BackColor = System.Drawing.Color.PowderBlue;
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                }
                if ((strRItemNamee == "Gross Profit") || (strRItemNamee == "Revenue") || (strRItemNamee == "Revenue(s) Total"))
                {
                    //e.Row.Cells[4].ForeColor = e.Row.Cells[5].ForeColor = System.Drawing.Color.DarkBlue;
                    //e.Row.Cells[4].BackColor = e.Row.Cells[5].BackColor = System.Drawing.Color.PowderBlue;
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
                if ((strLItemName == "Net Profit"))
                {
                    //e.Row.Cells[1].ForeColor = e.Row.Cells[2].ForeColor = System.Drawing.Color.RoyalBlue;
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                }
                if ((strRItemNamee == "Net Loss"))
                {
                    //e.Row.Cells[4].ForeColor = e.Row.Cells[5].ForeColor = System.Drawing.Color.RoyalBlue;
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
                if ((strLItemName == "Net Total") || (strRItemNamee == "Net Total"))
                {
                    //e.Row.Cells[1].ForeColor = e.Row.Cells[2].ForeColor = e.Row.Cells[4].ForeColor = e.Row.Cells[5].ForeColor = System.Drawing.Color.RoyalBlue;
                    //e.Row.Cells[1].BackColor = e.Row.Cells[2].BackColor = e.Row.Cells[4].BackColor = e.Row.Cells[5].BackColor = System.Drawing.Color.LightPink;
                    e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
                }
                //e.Row.Cells[3].BackColor = System.Drawing.Color.CadetBlue;
                //e.Row.Cells[1].Font.Bold = e.Row.Cells[2].Font.Bold = e.Row.Cells[4].Font.Bold = e.Row.Cells[5].Font.Bold = true;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {

            }
        }


    }
}