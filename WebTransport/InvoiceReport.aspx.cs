﻿using System;
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
using System.Drawing;

namespace WebTransport
{
    public partial class InvoiceReport : Pagebase
    {
        #region Variable ...

        string conString = "";
        static FinYear UFinYear = new FinYear();
        InvoiceRepDAL objInvcDAL = new InvoiceRepDAL();
        private int intFormId = 40;
        double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0, dConsignrServTaxAmount = 0, dNetAmount = 0, dGrAmt = 0, dTotQty = 0, dTotWeight = 0;
        DataTable CSVTable = new DataTable();
        #endregion

        #region Page Load Event ...

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            // conString = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ToString();
            UFinYear = base.FatchFinYear(1);
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
                this.BindSenderName();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }

                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                TotalRecords();
                InvoiceDAL objInvoiceDAL = new InvoiceDAL();
                tblUserPref obj = objInvoiceDAL.SelectUserPref();
                hidPrintType.Value = Convert.ToString(obj.InvPrint_Type);
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }

        #endregion

        #region Button Event...

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            drpSenderName.SelectedIndex = 0;
            txtInvoiceNo.Text = "";
            grdMain.DataSource = null;
            grdMain.DataBind();
            drpBaseCity.Focus();
        }
       

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...

        private void BindSenderName()
        {
            InvoiceRepDAL obj = new InvoiceRepDAL();
            var SenderName = obj.BindSender();
            drpSenderName.DataSource = SenderName;
            drpSenderName.DataTextField = "Acnt_Name";
            drpSenderName.DataValueField = "Acnt_Idno";
            drpSenderName.DataBind();
            objInvcDAL = null;
            drpSenderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserId)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserId);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objInvcDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                InvoiceRepDAL obj = new InvoiceRepDAL();
                Int64 iInvTyp = (Convert.ToString(ddlInvType.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlInvType.SelectedValue)); 
                DataTable list1 = obj.SelectRep("SelectInvwiseRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), 0, 0, 0, UserIdno,iInvTyp, ApplicationFunction.ConnectionString());
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }
        private void BindGrid()
        {
            try
            {
                InvoiceRepDAL obj = new InvoiceRepDAL();

                string userclass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                if (userclass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }

                Int64 iFromCityIDNO = (Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
                Int64 iSenderIDNO = (Convert.ToString(drpSenderName.SelectedValue) == "" ? 0 : Convert.ToInt64(drpSenderName.SelectedValue));
                Int32 iInvoiceNo = (Convert.ToString(txtInvoiceNo.Text) == "" ? 0 : Convert.ToInt32(txtInvoiceNo.Text));
                Int64 iInvTyp = (Convert.ToString(ddlInvType.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlInvType.SelectedValue)); 
                string strAction = "";
                if (ddlRepType.SelectedIndex == 0)
                {
                    strAction = "SelectInvwiseRep";
                }
                else
                {
                    strAction = "SelectGrwiseRep";
                }
                DataTable DsGrdetail = obj.SelectRep(strAction, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)), iFromCityIDNO, iSenderIDNO, iInvoiceNo, UserIdno,iInvTyp, ApplicationFunction.ConnectionString());
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    Int32 j = 0; Int32 GrIdno = 0; Int32 InvIdno = 0;
                    if (ddlRepType.SelectedIndex == 1)
                    {
                        for (int k = 0; k < DsGrdetail.Rows.Count; k++)
                        {
                            GrIdno = Convert.ToInt32(DsGrdetail.Rows[k]["Gr_Idno"]);
                            InvIdno = Convert.ToInt32(DsGrdetail.Rows[k]["ID"]);
                            if (k == 0)
                            {
                                DsGrdetail.Rows[k]["Gross_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Gross_Amnt"]);
                                DsGrdetail.Rows[k]["Bilty_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Bilty_Amnt"]);
                                DsGrdetail.Rows[k]["Short_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Short_Amnt"]);
                                DsGrdetail.Rows[k]["ConsignrServTax"] = Convert.ToString(DsGrdetail.Rows[k]["ConsignrServTax"]);
                                DsGrdetail.Rows[k]["TrServTax_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["TrServTax_Amnt"]);
                                DsGrdetail.Rows[k]["Net_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Net_Amnt"]);
                                DsGrdetail.Rows[k]["Gr_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Gr_Amnt"]);
                                DsGrdetail.Rows[k]["Tot_Qty"] = Convert.ToString(DsGrdetail.Rows[k]["Tot_Qty"]);
                                DsGrdetail.Rows[k]["Tot_Weght"] = Convert.ToString(DsGrdetail.Rows[k]["Tot_Weght"]);
                                j++;
                            }
                            else
                            {

                                if (GrIdno == Convert.ToInt32(DsGrdetail.Rows[k - 1]["Gr_Idno"]))
                                {

                                    DsGrdetail.Rows[k]["Gr_Amnt"] = "";
                                    DsGrdetail.Rows[k]["Tot_Qty"] = "";
                                    DsGrdetail.Rows[k]["Tot_Weght"] = "";
                                }
                                else
                                {
                                    j = 0;
                                    DsGrdetail.Rows[k]["Gr_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Gr_Amnt"]);
                                    DsGrdetail.Rows[k]["Tot_Qty"] = Convert.ToString(DsGrdetail.Rows[k]["Tot_Qty"]);
                                    DsGrdetail.Rows[k]["Tot_Weght"] = Convert.ToString(DsGrdetail.Rows[k]["Tot_Weght"]);

                                }
                                if (InvIdno == Convert.ToInt32(DsGrdetail.Rows[k - 1]["ID"]))
                                {

                                    DsGrdetail.Rows[k]["Gross_Amnt"] = "";
                                    DsGrdetail.Rows[k]["Bilty_Amnt"] = "";
                                    DsGrdetail.Rows[k]["Short_Amnt"] = "";
                                    DsGrdetail.Rows[k]["ConsignrServTax"] = "";
                                    DsGrdetail.Rows[k]["TrServTax_Amnt"] = "";
                                    DsGrdetail.Rows[k]["Net_Amnt"] = "";
                                }
                                else
                                {
                                    j = 0;
                                    DsGrdetail.Rows[k]["Gross_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Gross_Amnt"]);
                                    DsGrdetail.Rows[k]["Bilty_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Bilty_Amnt"]);
                                    DsGrdetail.Rows[k]["Short_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Short_Amnt"]);
                                    DsGrdetail.Rows[k]["ConsignrServTax"] = Convert.ToString(DsGrdetail.Rows[k]["ConsignrServTax"]);
                                    DsGrdetail.Rows[k]["TrServTax_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["TrServTax_Amnt"]);
                                    DsGrdetail.Rows[k]["Net_Amnt"] = Convert.ToString(DsGrdetail.Rows[k]["Net_Amnt"]);

                                }
                            }
                        }
                    }
                    ViewState["dtCSV"] = DsGrdetail;
                    grdMain.DataSource = DsGrdetail;
                    grdMain.DataBind();

                    ShowHideColumns();
                    Double TotalNetAmount = 0, TotGrossAmnt = 0, TotBilAmnt = 0, TotShrtAmnt = 0, TotConServTax = 0, ToTrServTax = 0;


                    for (int i = 0; i < DsGrdetail.Rows.Count; i++)
                    {
                        TotGrossAmnt += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["Gross_Amnt"])) ? "0" : DsGrdetail.Rows[i]["Gross_Amnt"].ToString());
                        TotBilAmnt += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["Bilty_Amnt"])) ? "0" : DsGrdetail.Rows[i]["Bilty_Amnt"].ToString());
                        TotShrtAmnt += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["Short_Amnt"])) ? "0" : DsGrdetail.Rows[i]["Short_Amnt"].ToString());
                        TotConServTax += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["ConsignrServTax"])) ? "0" : DsGrdetail.Rows[i]["ConsignrServTax"].ToString());
                        ToTrServTax += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["TrServTax_Amnt"])) ? "0" : DsGrdetail.Rows[i]["TrServTax_Amnt"].ToString());
                        TotalNetAmount += Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(DsGrdetail.Rows[i]["Net_Amnt"])) ? "0" : DsGrdetail.Rows[i]["Net_Amnt"].ToString());
                    }
                    lblGrossAmnt.Text = TotGrossAmnt.ToString("N2");
                    lblBilAmnt.Text = TotBilAmnt.ToString("N2");
                    lblShrtAmnt.Text = TotShrtAmnt.ToString("N2");
                    lblConServtax.Text = TotConServTax.ToString("N2");
                    lblTrServTax.Text = ToTrServTax.ToString("N2");
                    lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                    int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                    int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                    lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + DsGrdetail.Rows.Count.ToString();
                    lblcontant.Visible = true;
                    imgBtnExcel.Visible = true;
                    if (Convert.ToInt32(hidPrintType.Value) == 7)
                    {
                        lnkBtn.Visible = true;
                    }
                    else
                    {
                        lnkBtn.Visible = false;
                    }
                    divpaging.Visible = true;
                    lblTotalRecord.Text = "T. Record(s): " + DsGrdetail.Rows.Count;


                    if (ddlRepType.SelectedIndex == 0)
                    {
                        foreach (DataControlField col in grdMain.Columns)
                        {
                            if ((col.HeaderText == "Gr No.") || (col.HeaderText == "Gr Date") || (col.HeaderText == "Qty") || (col.HeaderText == "Weight") || (col.HeaderText == "Gr Amnt") || (col.HeaderText == "Chln No") || (col.HeaderText == "Gr Shrtg Amnt") || (col.HeaderText == "Lorry No."))
                            {
                                col.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataControlField col in grdMain.Columns)
                        {
                            if ((col.HeaderText == "Gr No.") || (col.HeaderText == "Gr Date") || (col.HeaderText == "Qty") || (col.HeaderText == "Weight") || (col.HeaderText == "Gr Amnt") || (col.HeaderText == "Chln No") || (col.HeaderText == "Gr Shrtg Amnt") || (col.HeaderText == "Lorry No."))
                            {
                                col.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    ViewState["dtCSV"] = null;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    //printRep.Visible = false;
                    imgBtnExcel.Visible = false;
                    lnkBtn.Visible = false;
                    lblTotalRecord.Text = "Total Record (s): 0 ";
                    lblcontant.Visible = false;
                    divpaging.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }

        private void ShowHideColumns()
        {
            grdMain.Columns[19].Visible = true;
            grdMain.Columns[20].Visible = true;
            grdMain.Columns[21].Visible = true;
            grdMain.Columns[22].Visible = true;
            grdMain.Columns[23].Visible = true;
            grdMain.Columns[24].Visible = true;
            grdMain.Columns[18].Visible = true;
            grdMain.Columns[17].Visible = true;

            if (GetGSTType(txtDateTo.Text) == 0)
            {
                grdMain.Columns[19].Visible = false;
                grdMain.Columns[20].Visible = false;
                grdMain.Columns[21].Visible = false;
                grdMain.Columns[22].Visible = false;
                grdMain.Columns[23].Visible = false;
                grdMain.Columns[24].Visible = false;
            }
            else if (GetGSTType(txtDateTo.Text) > 0)
            {
                grdMain.Columns[18].Visible = false;
                grdMain.Columns[17].Visible = false;
            }
        }

        //Upadhyay #GST
        public int GetGSTType(string date)
        {
            if (date != "")
            {
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(date.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
        }

        //Upadhyay #GST
        private string GetGSTDate()
        {
            DateTime gstDate;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                gstDate = (from i in db.tblUserPrefs select i.GST_Date).FirstOrDefault();
                return gstDate.ToString("dd-MM-yyyy");
            }
        }

        private DataTable getInvoiceDetails(Int32 Inv_Idno)
        {
            string str = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            InvoiceRepDAL objclsInvoiceRepDAL = new InvoiceRepDAL();
            DataTable lst = objclsInvoiceRepDAL.selectInvoiceReportDetails("SelectInvDet", Inv_Idno, ApplicationFunction.ConnectionString());
            return lst;

        }

        #endregion

        #region Grid Event...

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnInvoiceDetails = (ImageButton)e.Row.FindControl("imgBtnInvoiceDetails");
                HiddenField HidInvoiceDetailsIdno = (HiddenField)e.Row.FindControl("hidInvoiceDetails_Idno");
                GridView grdDetails = (GridView)e.Row.FindControl("grdDetails");
                HtmlGenericControl dvInvoiceDetails = (HtmlGenericControl)e.Row.FindControl("dvInvoiceDetails");
                imgBtnInvoiceDetails.Attributes.Add("onmouseover", "ShowInvoiceDetails('" + dvInvoiceDetails.ClientID + "')");
                imgBtnInvoiceDetails.Attributes.Add("onmouseout", "HideInvoiceDetails('" + dvInvoiceDetails.ClientID + "')");
                DataTable Dt = getInvoiceDetails(Convert.ToInt32(HidInvoiceDetailsIdno.Value));
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    grdDetails.DataSource = Dt;
                    grdDetails.DataBind();
                }

                imgBtnInvoiceDetails.ImageUrl = "~/Images/search_icon.gif";
                dGrossAmount = dGrossAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Gross_Amnt")));
                dBiltyAmount = dBiltyAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Bilty_Amnt")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Bilty_Amnt")));
                dShortAmount = dShortAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Short_Amnt")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Short_Amnt")));
                dConsignrServTaxAmount = dConsignrServTaxAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ConsignrServTax")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "ConsignrServTax")));
                dTrServTaxAmount = dTrServTaxAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TrServTax_Amnt")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "TrServTax_Amnt")));
                dNetAmount = dNetAmount + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Net_Amnt")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Net_Amnt")));

                dTotQty = dTotQty + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tot_Qty")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Tot_Qty")));
                dTotWeight = dTotWeight + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tot_Weght")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Tot_Weght")));
                dGrAmt = dGrAmt + Convert.ToDouble(((Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tot_Weght")) == "") ? "0.00" : DataBinder.Eval(e.Row.DataItem, "Tot_Weght")));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrossAmount = (Label)e.Row.FindControl("lblGrossAmount");
                lblGrossAmount.Text = dGrossAmount.ToString("N2");
                Label lblBiltyAmount = (Label)e.Row.FindControl("lblBiltyAmount");
                lblBiltyAmount.Text = dBiltyAmount.ToString("N2");
                Label lblShortAmount = (Label)e.Row.FindControl("lblShortAmount");
                lblShortAmount.Text = dShortAmount.ToString("N2");
                Label lblConsignrServTaxAmount = (Label)e.Row.FindControl("lblConsignrServTaxAmount");
                lblConsignrServTaxAmount.Text = dConsignrServTaxAmount.ToString("N2");
                Label lblTrServTaxAmount = (Label)e.Row.FindControl("lblTrServTaxAmount");
                lblTrServTaxAmount.Text = dTrServTaxAmount.ToString("N2");
                Label lblNetAmount = (Label)e.Row.FindControl("lblNetAmount");
                lblNetAmount.Text = dNetAmount.ToString("N2");

                Label lblQty = (Label)e.Row.FindControl("lblQty");
                lblQty.Text = dTotQty.ToString("N2");

                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                lblWeight.Text = dTotWeight.ToString("N2");

                Label lblGrAmnt = (Label)e.Row.FindControl("lblGrAmnt");
                lblGrAmnt.Text = dGrAmt.ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
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

        #region Excel...

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            //grdMain.GridLines = GridLines.Both;
            //PrepareGridViewForExport(grdMain);
            //ExportGridView();
            CSVTable = (DataTable)ViewState["dtCSV"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {


                CSVTable.Columns.Remove("ID");
                CSVTable.Columns.Remove("Gr_Idno");
                if (ddlRepType.SelectedIndex == 0)
                {
                    CSVTable.Columns.Remove("Gr_No");
                    CSVTable.Columns.Remove("Gr_Date");
                    CSVTable.Columns.Remove("Tot_Qty");
                    CSVTable.Columns.Remove("Tot_Weght");
                    CSVTable.Columns.Remove("Gr_Amnt");
                    CSVTable.Columns["City_Name"].SetOrdinal(2);
                    CSVTable.Columns["SenderName"].SetOrdinal(3);
                    CSVTable.Columns["Receiver"].SetOrdinal(4);
                    CSVTable.Columns["Gross_Amnt"].SetOrdinal(5);
                    CSVTable.Columns["Bilty_Amnt"].SetOrdinal(6);
                    CSVTable.Columns["Short_Amnt"].SetOrdinal(7);
                    CSVTable.Columns["TrServTax_Amnt"].SetOrdinal(8);
                    CSVTable.Columns["ConsignrServTax"].SetOrdinal(9);
                    CSVTable.Columns["Net_Amnt"].SetOrdinal(10);
                }
                else
                {

                    CSVTable.Columns["City_Name"].SetOrdinal(2);
                    CSVTable.Columns["Gr_No"].SetOrdinal(3);
                    CSVTable.Columns["Gr_Date"].SetOrdinal(4);
                    CSVTable.Columns["Tot_Qty"].SetOrdinal(5);
                    CSVTable.Columns["Tot_Weght"].SetOrdinal(6);
                    CSVTable.Columns["SenderName"].SetOrdinal(7);
                    CSVTable.Columns["Receiver"].SetOrdinal(8);
                    CSVTable.Columns["Gr_Amnt"].SetOrdinal(9);
                    CSVTable.Columns["Gross_Amnt"].SetOrdinal(10);
                    CSVTable.Columns["Bilty_Amnt"].SetOrdinal(11);
                    CSVTable.Columns["Short_Amnt"].SetOrdinal(12);
                    CSVTable.Columns["TrServTax_Amnt"].SetOrdinal(13);
                    CSVTable.Columns["ConsignrServTax"].SetOrdinal(14);
                    CSVTable.Columns["Net_Amnt"].SetOrdinal(15);
                    CSVTable.Columns["Gr_No"].ColumnName = "Gr No";
                    CSVTable.Columns["Tot_Qty"].ColumnName = "Qty";
                    CSVTable.Columns["Tot_Weght"].ColumnName = "Weight";
                    CSVTable.Columns["Gr_Amnt"].ColumnName = "Gr Amount";
                    CSVTable.Columns["Gr_Date"].ColumnName = "Gr Date";
                }

                CSVTable.Columns["Inv_No"].ColumnName = "Invo.No";
                CSVTable.Columns["Inv_Date"].ColumnName = "Invo.Date";
                CSVTable.Columns["Receiver"].ColumnName = "Reciver";
                CSVTable.Columns["SenderName"].ColumnName = "Sender Name";
                CSVTable.Columns["Gross_Amnt"].ColumnName = "Gross Amount";
                CSVTable.Columns["Bilty_Amnt"].ColumnName = "Bilty Amount";
                CSVTable.Columns["Short_Amnt"].ColumnName = "Shortage Amount";
                CSVTable.Columns["ConsignrServTax"].ColumnName = "Consg.Ser.Tax";
                CSVTable.Columns["TrServTax_Amnt"].ColumnName = "Trans.Serv.Tax";
                CSVTable.Columns["Net_Amnt"].ColumnName = "Net Amout";
                CSVTable.Columns["City_Name"].ColumnName = "City Name";
                ExportDataTableToCSV(CSVTable, "InvoiceReport" + txtDateFrom.Text + "_TO_" + txtDateTo.Text);
                Response.Redirect("GrReport.aspx");
            }

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
        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        protected void lnkBtn_Click(object sender, EventArgs e)
        {
            InvoiceRepDAL objDAL = new InvoiceRepDAL();
            if (string.IsNullOrEmpty(txtInvoiceNo.Text) == true)
            {
                ShowMessageErr("Please Enter Invoice NO.!");
                return;
            }  
            String Inv = txtInvoiceNo.Text;
            Int64 date = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 city = Convert.ToInt64(drpBaseCity.SelectedValue);
            Int64 invIdno = objDAL.InvIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(Inv), date, city);
            PrintReport(invIdno,date,city);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
        }
        private void PrintReport(Int64 Inv,Int64 date, Int64 city)
        {
            Repeater obj = new Repeater();
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='REPORT',@Id='" + Inv + "',@YearIdno='" + date + "',@FromCityIdno='" + city + "'");
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = dsReport.Tables[0];
                Repeater1.DataBind();
            }
        }
        #endregion
    }
}


