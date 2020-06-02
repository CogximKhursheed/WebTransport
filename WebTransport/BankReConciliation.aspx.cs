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

namespace WebTransport
{
    public partial class BankReConciliation : Pagebase
    {
        #region Variable ...
        string conString = "";
        Double Amount1 = 0, Amount2 = 0, Amount3 = 0, Amount4 = 0, dbCR = 0, dbDR = 0, dbCloseB = 0,
            dbCRvchr = 0, dbDRvchr = 0,
            dbCRCloseBal = 0, dbDRCloseBal = 0,
            dbCRBalnotref = 0, dbDRBalnotref = 0;
        Int32 intBalType = 0;

        static FinYear UFinYear = new FinYear();
        BankReConciliationDAL objBankReConciliationDAL = new BankReConciliationDAL();
        private int intFormId = 50;
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            //conString = ConfigurationManager.ConnectionStrings["AutomobileConnectionString"].ToString();
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
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
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }

                this.BindBankName();
                BindDateRange();
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtInstDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('From date can not be greater than To date!');", true);
                    txtDateFrom.Focus();
                    return;
                }
            }
            this.BindGrid();
            ddlDateRange.Focus();
        }
        protected void lnkbtnClear_OnClick(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            ddlBank.SelectedIndex = 0;
            ddlTType.SelectedIndex = 0;
            ddlBankDate.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            Calc.Visible = false;
            DivOpeningBal.Visible = false;

            imgBtnExcel.Visible = false;
            lnkbtnPrint.Visible = false;
            ddlDateRange.Focus();
            Label lblEmptyMessage = grdMain.Controls[0].Controls[0].FindControl("LblNoRecordFound") as Label;
            lblEmptyMessage.Visible = false;

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            DateTime InstDate = new DateTime();
            if (txtInstDate.Text == "")
            {
                InstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy("01-01-1900"));
            }
            else
            {
                InstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text.Trim()));
            }
            Int32 VchrIdno = objBankReConciliationDAL.Update(InstDate, Convert.ToInt32(hidacntheadid.Value));
            objBankReConciliationDAL = null;
            if (VchrIdno > 0)
            {
                strMsg = "Record saved successfully.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            this.BindGrid();
        }
        #endregion

        #region Bind Event...



        private void BindBankName()
        {
            BankReConciliationDAL obj = new BankReConciliationDAL();
            var BankName = obj.BindBank();
            ddlBank.DataSource = BankName;
            ddlBank.DataTextField = "Acnt_Name";
            ddlBank.DataValueField = "Acnt_Idno";
            ddlBank.DataBind();
            objBankReConciliationDAL = null;
            ddlBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindGrid()
        {
            if (ddlBank.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Please Select Bank Name!');", true);
                return;
            }
            DataTable dsTable = ConvertToDatatable();
            if (dsTable != null && dsTable.Rows.Count > 0)
            {
                grdMain.DataSource = dsTable;
                grdMain.DataBind();
                Calc.Visible = true;
                prints.Visible = true;
                DivOpeningBal.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                Calc.Visible = false;
                prints.Visible = false;
                DivOpeningBal.Visible = false;
            }
        }
        private void Populate(Int64 Id)
        {
            VchrDetl BankDate = objBankReConciliationDAL.FetchBankDateById(Id);
            VchrHead TranDate = objBankReConciliationDAL.FetchBankDateByvchrId(Convert.ToInt64(BankDate.Vchr_Idno));
            objBankReConciliationDAL = null;
            if (BankDate != null)
            {
                hidTranDate.Value = Convert.ToDateTime(TranDate.Vchr_Date).ToString("dd-MM-yyyy");
                txtInstDate.Text = string.IsNullOrEmpty(Convert.ToString(BankDate.Bank_Date)) ? DateTime.Now.Date.ToString("dd-MM-yyyy") : Convert.ToDateTime(BankDate.Bank_Date).ToString("dd-MM-yyyy") == "01-01-1900" ? DateTime.Now.Date.ToString("dd-MM-yyyy") : Convert.ToDateTime(BankDate.Bank_Date).ToString("dd-MM-yyyy");
                hidacntheadid.Value = BankDate.VchrDetl_Idno.ToString();
            }
        }
        private DataTable ConvertToDatatable()
        {
            bool First = true;
            bool Second = true;
            DataSet ds = null;
            DataSet ds2 = null;
            DataTable dTN = new DataTable();
            DataTable dTN2 = new DataTable();
            string strDateFrom = Convert.ToString(txtDateFrom.Text.Trim());
            string strDateTo = Convert.ToString(txtDateTo.Text.Trim());
            Int64 intBankIdno = Convert.ToInt64(ddlBank.SelectedValue);
            BankReConciliationDAL objBankReConciliationDAL1 = new BankReConciliationDAL();
            DataSet DsOuter = objBankReConciliationDAL1.SelectBankRC1(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneById", strDateFrom, strDateTo, intBankIdno);
            DataSet DsOuterBankBal = objBankReConciliationDAL1.SelectBankRC1(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneById2", strDateFrom, strDateTo, intBankIdno);
            if ((DsOuter != null) && (DsOuter.Tables.Count > 0) && (DsOuter.Tables[0].Rows.Count > 0))
            {
                for (int x = 0; x < DsOuter.Tables[0].Rows.Count; x++)
                {
                    DataSet dsTemp = null;
                    string strVchrIdno = Convert.ToString(DsOuter.Tables[0].Rows[x]["VCHR_IDNO"]);
                    string strAmntType = Convert.ToString(DsOuter.Tables[0].Rows[x]["AMNT_TYPE"]);
                    Int32 intTrnkType = Convert.ToInt32(ddlTType.SelectedValue);
                    Int32 intBankdt = Convert.ToInt32(ddlBankDate.SelectedValue);
                    dsTemp = objBankReConciliationDAL1.SelectBankRC2(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneBankRecon", strVchrIdno, strAmntType, intTrnkType, 0, intBankIdno);
                    if ((dsTemp != null) && (dsTemp.Tables.Count > 0) && (dsTemp.Tables[0].Rows.Count > 0))
                    {
                        if (First == true)
                        {
                            ds = dsTemp;
                            First = false;
                        }
                        else
                        {
                            ds.Tables[0].Merge(dsTemp.Tables[0]);
                        }
                    }
                }
                #region Forloop for Bank Balance ...
                if ((DsOuterBankBal != null) && (DsOuterBankBal.Tables.Count > 0) && (DsOuterBankBal.Tables[0].Rows.Count > 0))
                {
                    for (int x = 0; x < DsOuterBankBal.Tables[0].Rows.Count; x++)
                    {
                        DataSet dsTemp2 = null;
                        string strVchrIdno2 = Convert.ToString(DsOuterBankBal.Tables[0].Rows[x]["VCHR_IDNO"]);
                        string strAmntType2 = Convert.ToString(DsOuterBankBal.Tables[0].Rows[x]["AMNT_TYPE"]);
                        Int32 intTrnkType2 = Convert.ToInt32(ddlTType.SelectedValue);
                        Int32 intBankdt2 = Convert.ToInt32(ddlBankDate.SelectedValue);
                        dsTemp2 = objBankReConciliationDAL1.SelectBankRC2(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneBankRecon", strVchrIdno2, strAmntType2, intTrnkType2, 0, intBankIdno);
                        if ((dsTemp2 != null) && (dsTemp2.Tables.Count > 0) && (dsTemp2.Tables[0].Rows.Count > 0))
                        {
                            if (Second == true)
                            {
                                ds2 = dsTemp2;
                                Second = false;
                            }
                            else
                            {
                                ds2.Tables[0].Merge(dsTemp2.Tables[0]);
                            }
                        }
                    }
                }
                #endregion
            }
            if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                DataView dv = new DataView();
                dv.Table = ds.Tables[0];
                dv.Sort = "VCHR_DATE";
                ds.Tables.RemoveAt(0);
                ds.Tables.Add(dv.ToTable());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["PERTICULAR"] = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["PERTICULAR"]) + " " + Environment.NewLine + Convert.ToString(ds.Tables[0].Rows[i]["NARR_TEXT"]));
                }
                dTN = ds.Tables[0];
                #region BankBalance
                if ((ds2 != null) && (ds2.Tables.Count > 0) && (ds2.Tables[0].Rows.Count > 0))
                {
                    DataView dv2 = new DataView();
                    dv2.Table = ds2.Tables[0];
                    dv2.Sort = "VCHR_DATE";
                    ds2.Tables.RemoveAt(0);
                    ds2.Tables.Add(dv2.ToTable());
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        ds2.Tables[0].Rows[i]["PERTICULAR"] = Convert.ToString(Convert.ToString(ds2.Tables[0].Rows[i]["PERTICULAR"]) + " " + Environment.NewLine + Convert.ToString(ds2.Tables[0].Rows[i]["NARR_TEXT"]));
                    }
                    dTN2 = ds2.Tables[0];
                }
                #endregion
            }
            if (dTN != null && dTN.Rows.Count > 0)
            {
                #region Opening Balance ...
                BankReConciliationDAL objBankReConciliationDAL3 = new BankReConciliationDAL();
                lblOpeningBalance2.Text = "";
                DataTable dtOpenBalacntmast = objBankReConciliationDAL3.OpenBal(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), intBankIdno);
                if (dtOpenBalacntmast != null && dtOpenBalacntmast.Rows.Count > 0)
                {
                    string strCR1 = Convert.ToString(dtOpenBalacntmast.Rows[0][0]).Substring(0, Convert.ToString(dtOpenBalacntmast.Rows[0][0]).Length - 3).Trim();
                    string strDR1 = Convert.ToString(dtOpenBalacntmast.Rows[0][1]).Substring(0, Convert.ToString(dtOpenBalacntmast.Rows[0][1]).Length - 3).Trim();
                    dbCR = Convert.ToDouble(strCR1);
                    dbDR = Convert.ToDouble(strDR1);
                }
                else
                {
                    dbCR = 0;
                    dbDR = 0;
                }
                DataTable dtOpenBavchr = objBankReConciliationDAL3.OpeningBalance(ApplicationFunction.ConnectionString(), Convert.ToString(txtDateFrom.Text.Trim()), intBankIdno);
                if (dtOpenBavchr != null && dtOpenBavchr.Rows.Count > 0)
                {
                    string strDRopening = Convert.ToString(dtOpenBavchr.Rows[0][0]).Trim();
                    string strCRopening = Convert.ToString(dtOpenBavchr.Rows[0][1]).Trim();
                    dbCRvchr = Convert.ToDouble(strCRopening);
                    dbDRvchr = Convert.ToDouble(strDRopening);
                }
                else
                {
                    dbCRvchr = 0;
                    dbDRvchr = 0;
                }
                if (dbCR + dbCRvchr > dbDR + dbDRvchr)
                {
                    lblOpeningBalance2.Text = String.Format("{0:0,0.00}", Math.Abs((dbCR + dbCRvchr) - (dbDR + dbDRvchr)), 2).ToString() + " Cr.";
                }
                else
                {
                    lblOpeningBalance2.Text = String.Format("{0:0,0.00}", Math.Abs((dbDR + dbDRvchr) - (dbCR + dbCRvchr)), 2).ToString() + " Dr.";
                }
                #endregion
            }
            return dTN;
        }
        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBankName();
            grdMain.DataSource = null;
            grdMain.DataBind();
            Calc.Visible = false;
            DivOpeningBal.Visible = false;

        }
        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                txtInstDate.Text = String.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                this.Populate(Convert.ToInt64(e.CommandArgument));
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {
                    lnkbtnEdit.Visible = true;
                    grdMain.Columns[7].Visible = true;
                }
                else
                {
                    lnkbtnEdit.Visible = true;
                    grdMain.Columns[7].Visible = true;
                }
                string sBankDate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Bank_Date"));
                DateTime BankDate = new DateTime();
                if (string.IsNullOrEmpty(sBankDate) == false)
                {
                    BankDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Bank_Date"));
                }
                DateTime ToDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim()));
                if (Convert.ToDateTime(BankDate).ToString("dd-MM-yyyy") == "01-01-0001" || Convert.ToDateTime(BankDate).ToString("dd-MM-yyyy") == "01-01-1900" || (BankDate > ToDate))
                {
                    Amount1 = Math.Round((Amount1 + (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Debit"))) ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit")))), 2);
                    Amount2 = Math.Round((Amount2 + (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit"))) ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")))), 2);
                    if ((ddlBankDate.SelectedItem.Text.ToUpper()) == "YES")
                    {
                        e.Row.Visible = false;
                    }
                }
                else if ((ddlBankDate.SelectedItem.Text.ToUpper()) == "NO") { e.Row.Visible = false; }

                Amount3 = Math.Round(Amount3 + (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Debit"))) ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"))), 2);
                Amount4 = Math.Round(Amount4 + (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit"))) ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"))), 2);
                lblNetBalance2.Text = String.Format("{0:0,0.00}", Amount1).ToString() + " Dr. &nbsp;&nbsp;-&nbsp;&nbsp; " + String.Format("{0:0,0.00}", Amount2).ToString() + " Cr. ";
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount1 = (Label)e.Row.FindControl("lblAmount1");
                lblAmount1.Text = String.Format("{0:0,0.00}", Amount3).ToString();
                Label lblAmount2 = (Label)e.Row.FindControl("lblAmount2");
                lblAmount2.Text = String.Format("{0:0,0.00}", Amount4).ToString();

                double closingBalDr = 0;
                double closingBalCr = 0;
                double BankBalanceDR = (Amount3 - Amount1);
                double BankBalanceCR = (Amount4 - Amount2);

                if (lblOpeningBalance2.Text.ToUpper().Contains("DR"))
                {
                    closingBalDr = Amount3 + Convert.ToDouble(lblOpeningBalance2.Text.ToUpper().Replace("DR.", "").ToString());
                    closingBalCr = Amount4;
                    BankBalanceDR = BankBalanceDR + Convert.ToDouble(lblOpeningBalance2.Text.ToUpper().Replace("DR.", "").ToString());
                }
                else
                {
                    closingBalCr = Amount4 + Convert.ToDouble(lblOpeningBalance2.Text.ToUpper().Replace("CR.", "").ToString());
                    closingBalDr = Amount3;
                    BankBalanceCR = BankBalanceCR + Convert.ToDouble(lblOpeningBalance2.Text.ToUpper().Replace("CR.", "").ToString());
                }
                //Balance as per company books
                if (closingBalDr > closingBalCr)
                {
                    lblClosingBalance2.Text = String.Format("{0:0,0.00}", closingBalDr - closingBalCr) + "Dr.";
                }
                else
                {
                    lblClosingBalance2.Text = String.Format("{0:0,0.00}", closingBalCr - closingBalDr) + "Cr.";
                }
                //Balance as per bank
                if (BankBalanceDR > BankBalanceCR)
                {
                    lblBankBalance2.Text = String.Format("{0:0,0.00}", Math.Abs((BankBalanceDR - BankBalanceCR)), 2).ToString() + " Dr.";
                }
                else
                {
                    lblBankBalance2.Text = String.Format("{0:0,0.00}", Math.Abs((BankBalanceCR - BankBalanceDR)), 2).ToString() + " Cr.";
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
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=BankReconciliationReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grdMain.Columns[7].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[7].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}