using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;

namespace WebTransport
{
    public partial class SaleBill : Pagebase
    {
        #region VariablesDeclarations..
        private int intFormId = 222;
        BindDropdownDAL obj;
        SaleBillDAL objSaleBillDAL;
        BindDropdownDAL objBindDropdown;
        FinYearDAL objFinYearDAL;
        DataTable dtTemp = new DataTable();
        DataTable dtDivTemp = new DataTable();
        double Qty = 0.00, Rate = 0.00, Amount = 0.00, DiscountAmnt = 0.00,Tax = 0;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, dtotDis = 0;
        bool flagAmnt = false;
        Double iqty = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0,  dtotalDiscountAmnt = 0, GridDiscount=0, GrdTotalVAT = 0;
        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            #region is Postback............

            //string strPostBackControlName = Request.Params.Get("__EVENTTARGET");
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    lnkbtnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}
                txtBillDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDivDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDivDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                UserPrefenceDAL objUserPrefence = new UserPrefenceDAL();
                var objUserPref = objUserPrefence.SelectById();
                objUserPrefence = null;

                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                this.BindDateRange(); this.BindCity(); this.BindDropdown(); this.BindSerialNo(); this.BindModel(); this.BindTyreCategory();
                ddlDateRange.SelectedIndex = 0; ddlDateRange_SelectedIndexChanged(null, null);
                GetMaxBillNo();

                if (Request.QueryString["SbillIdno"] != null)
                {
                    this.FillInformationDetails(Convert.ToInt64(Request.QueryString["SbillIdno"].ToString()));
                    lnkbtnPrint.Visible = true;
                    ddlDateRange.Enabled = false;
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                objUserPrefence = null;
                if (objUserPref != null)
                {
                    if (Convert.ToBoolean(objUserPref.CntrSBill_Req)) { 
                        rdoMTIssue.Checked = true; rdoCounter.Enabled = false; DivItems.Visible = false; lnkbtnContnrDtl.Visible = true; 
                    } 
                    else 
                    {
                        rdoCounter.Checked = true; rdoCounter.Enabled = true; DivItems.Visible = true; lnkbtnContnrDtl.Visible = false; 
                    }
                }
            }
            #endregion
        }
        #endregion

        #region IndexChangeEvents........
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        protected void ddlDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CalcDiscountOnAllItem() == false)
                return;
            txtDiscountPer.Focus();
        }
        protected void ddlSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedValue == "0") { ShowMessageErr("Please select location first."); return; ddlFromCity.Focus(); ddlSerialNo.SelectedValue = "0"; }
            else
            {
                SaleBillDAL objSaleBillDAL = new SaleBillDAL();
                Int64 ValueSerial = (string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? 0 : Convert.ToInt64(ddlSerialNo.SelectedValue));
                string ItemName = string.IsNullOrEmpty(ddlSerialNo.SelectedItem.Text.Trim()) ? "" : Convert.ToString(ddlSerialNo.SelectedItem.Text.Trim().ToLower());
                if (string.IsNullOrEmpty(ItemName) == false)
                {
                    bool result = objSaleBillDAL.CheckItem(ItemName);
                    if (result == true)
                    {
                        this.BindModel(ValueSerial); this.BindTyreCategory(ValueSerial); Int64 ModelIdno= objSaleBillDAL.SelectModel(ValueSerial);
                        ddlModelName.SelectedValue = Convert.ToString(ModelIdno);
                        txtQuantity.Enabled = false; HideItemType.Value = Convert.ToString(1);
                        lblStockAss.Text = ""; txtQuantity.Text = "1";
                    }
                    else
                    {
                        this.BindModel(); this.BindTyreCategory();
                        ddlModelName.SelectedValue = ddlSerialNo.SelectedValue;
                        txtQuantity.Enabled = true; HideItemType.Value = Convert.ToString(2);
                        this.StockCalc(); this.CheckStock(); txtQuantity.Text = "0";
                    }

                }
                try
                {
                    DataTable dt = objSaleBillDAL.GetModelDetails(Convert.ToInt64(ddlModelName.SelectedValue), ApplicationFunction.ConnectionString());
                    objSaleBillDAL = null;
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ddlTaxType.SelectedValue) > 0)
                        {
                            if (Convert.ToInt32(ddlTaxType.SelectedValue) == 1)
                            {
                                HidTaxRate.Value = string.IsNullOrEmpty(dt.Rows[0]["TaxRate"].ToString()) ? "" : Convert.ToDouble(dt.Rows[0]["TaxRate"].ToString()).ToString("N2");
                            }
                            else if (Convert.ToInt32(ddlTaxType.SelectedValue) == 2)
                            {
                                HidTaxRate.Value = string.IsNullOrEmpty(dt.Rows[0]["CstRate"].ToString()) ? "0" : Convert.ToDouble(dt.Rows[0]["CstRate"].ToString()).ToString("N2");
                            }
                            else
                            {
                                HidTaxRate.Value = "0";
                            }
                        }
                    }
                    SetFocus(ddlRateType.ClientID);
                }
                catch (Exception Ex) { }
            }
        }
        protected void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            if (CalcDiscountOnAllItem() == false)
                return;
            if (txtDiscountPer.Text.ToString() == "") { txtDiscountPer.Text = "0.00"; }
            txtDiscountPer.Focus();
        }
        protected void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            NetAmountCalculate();
            if (txtOtherCharges.Text.ToString() == "") { txtOtherCharges.Text = "0.00"; }
            SetFocus(txtOtherCharges.ClientID);
        }
        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue)) > 0)
            {
                GetMaxBillNo();
            }
            if ((string.IsNullOrEmpty(HideItemType.Value) ? 0 : Convert.ToInt32(HideItemType.Value)) > 0)
            {
                this.CheckStock();
            }
            SetFocus(lnkbtnDriverRefresh.ClientID);
        }

        protected void rdoCounter_CheckedChanged(object sender, EventArgs e)
        {
            DivItems.Visible = true;
            lnkbtnContnrDtl.Visible = false;
            ddlTaxType.Enabled = true;
            dtTemp = CreateDt();
            ViewState["dt"] = dtTemp;
            ViewState["dtDivGrid"] = null;
            grdMain.DataSource = grdMIdetals.DataSource = null;
            lblSearchRecords.InnerText = "T. Record(s) : 0";
            grdMain.DataBind(); grdMIdetals.DataBind();
            SetFocus(rdoCounter.ClientID);
        }

        protected void rdoMTIssue_CheckedChanged(object sender, EventArgs e)
        {
            DivItems.Visible = false;
            lnkbtnContnrDtl.Visible = true;
            ddlTaxType.Enabled = true;
            dtTemp = CreateDt();
            ViewState["dt"] = dtTemp;
            ViewState["dtDivGrid"] = null;
            grdMain.DataSource = grdMIdetals.DataSource = null;
            lblSearchRecords.InnerText = "T. Record(s) : 0";
            grdMain.DataBind(); grdMIdetals.DataBind();
            SetFocus(lnkbtnContnrDtl.ClientID);
        }
        #endregion

        #region Grid Events.......

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                    ddlSerialNo.SelectedValue = Convert.ToString(drs[0]["SerialNoIdno"]);
                    ddlModelName.SelectedValue = Convert.ToString(drs[0]["ModelIdNo"]);
                    ddltype.SelectedValue = Convert.ToString(drs[0]["TyreTypeIdNo"]);
                    
                    ddlRateType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["RateTypeIdno"]) == "" ? 1 : drs[0]["RateTypeIdno"]);
                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])));
                    txtDiscountItem.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["DiscountValue"]) == "" ? 0.00 : Convert.ToDouble(drs[0]["DiscountValue"])));
                    ddlDiscountItemWise.SelectedValue = Convert.ToString(drs[0]["DiscountType"]);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
                }
                
                if (Request.QueryString["SbillIdno"] != null)
                { ddlSerialNo.Enabled = false; }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OnchangeRatetype(" + ddlRateType.SelectedValue + ")", true);
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["SerialNo"], rw["SerialNoIdno"], rw["ModelName"], rw["ModelIdNo"], rw["TyreType"], rw["TyreTypeIdNo"], rw["RateType"],
                                                                rw["RateTypeIdno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"], rw["DiscountType"], rw["Discount"], rw["TaxAmnt"], rw["TaxRate"], rw["DiscountValue"], rw["MatIssNo"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
                
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (dtTemp == null)
            {
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    iqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "quantity"));
                    totalIqty += iqty;
                    itotWeght = itotWeght + Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Weight")) == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight")));
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    dtotrate = dtotrate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate"));
                    GridDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                    dtotalDiscountAmnt = dtotalDiscountAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Discount"));
                    GrdTotalVAT = GrdTotalVAT + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TaxAmnt"));
                    
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotQuantity = (Label)e.Row.FindControl("lblTotQuantity");
                    lblTotQuantity.Text = totalIqty.ToString("N2");

                    Label lblDiscount= (Label)e.Row.FindControl("lblDiscount");
                    lblDiscount.Text = dtotalDiscountAmnt.ToString("N2");

                    Label lblVAT = (Label)e.Row.FindControl("lblVAT");
                    lblVAT.Text = GrdTotalVAT.ToString("N2");
                    
                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = dtotAmnt.ToString("N2");
                }
                
                EnableDisableCal();
            }
            txtTotalAmount.Text = (dtotAmnt).ToString("N2");
            txtDiscount.Text = dtotalDiscountAmnt.ToString("N2");
            
            NetAmountCalculate();
        }

        #endregion

        #region Functions..
        private bool PostIntoAccounts(DataTable dt, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno)
        {
            #region Variables Declaration...

            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            tblFleetAcntLink objAcntLink = objSaleBillDAL.SelectAcntLink();
            Int64 intVchrIdno = 0;
            Int64 intValue = 0;
            //hidpostingmsg.Value = string.Empty;
            double dblOtherAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? "0" : txtOtherCharges.Text.Trim());
            double dblDiscAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? "0" : txtDiscount.Text.Trim());
            double dblVatCST = 0;
            double dblNetAmnt = 0;
            Int64 RateType = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double Amount = 0;
                RateType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["RateTypeIdno"])) ? 0 : Convert.ToInt64(dt.Rows[i]["RateTypeIdno"]);
                if (RateType == 1)
                {
                    Amount = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Quantity"])) ? 0 : Convert.ToDouble(dt.Rows[i]["Quantity"])) * (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Rate"])) ? 0 : Convert.ToDouble(dt.Rows[i]["Rate"]));
                    dblNetAmnt = dblNetAmnt + Amount;
                }
                else
                {
                    Amount = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Quantity"])) ? 0 : Convert.ToDouble(dt.Rows[i]["Quantity"])) * (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Weight"])) ? 0 : Convert.ToDouble(dt.Rows[i]["Weight"]));
                    dblNetAmnt = dblNetAmnt + Amount;
                }

                dblVatCST += Convert.ToDouble(dt.Rows[i]["TaxAmnt"]);
            }
            if (objAcntLink != null)
            {
                if (objAcntLink.CSale_Idno == 0 || objAcntLink.VSale_Idno == 0 || objAcntLink.Vat_Idno == 0 || objAcntLink.CST_Idno == 0 || objAcntLink.Disc_Idno == 0 || objAcntLink.Other_Idno == 0 || objAcntLink.Cash_Idno == 0)
                {
                    ShowMessageErr("Please Define Account Links!");
                    return false;
                }
            }

            Int32 intTaxType = (ddlTaxType.SelectedValue == "1" ? 1 : (ddlTaxType.SelectedValue == "2" ? 2 : 3));
            Int32 intBillType = string.IsNullOrEmpty(Convert.ToString(ddlBillType.SelectedValue)) ? 1 : Convert.ToInt32(ddlBillType.SelectedValue);
            Int64 intPartyIdno = Convert.ToInt64((intBillType == 1) ? ((ddlParty.SelectedIndex == 0) ? 0 : Convert.ToInt64(ddlParty.SelectedValue)) : ((objAcntLink.Cash_Idno == 0) ? 0 : Convert.ToInt64(objAcntLink.Cash_Idno)));

            Int64 intDocNo = 0;
            if (Request.QueryString["SbillIdno"] == null)
            {
                intDocNo = objSaleBillDAL.SelectSaleNoByIdno(intDocIdno);
                objSaleBillDAL = null;
            }
            else
            {
                intDocNo = Convert.ToInt64(txtBillNo.Text.Trim());
            }

            DateTime? dtDate = null;
            DateTime? dtBankDate = null;

            clsAccountPosting objclsAccountPosting = new clsAccountPosting();


            #endregion

            #region Posting Start...

            if (Request.QueryString["SbillIdno"] == null)
            {
                intValue = 1;
            }
            else
            {
                intValue = objclsAccountPosting.DeleteAccountPosting(intDocIdno, strDocType);
            }
            if (intValue > 0)
            {
                intValue = objclsAccountPosting.InsertInVchrHead(
                Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text.Trim())),
                4,
                0,
                "Sale Bill No: " + Convert.ToString(intDocNo) + " Sale Bill Date: " + txtBillDate.Text.Trim(),
                true,
                0,
                strDocType,
                0,
                0,
                0,
                dtDate,
                0,
                0,
                Convert.ToInt32(ddlDateRange.SelectedValue),
                Convert.ToInt32(base.CompId), intUserIdno);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;

                    #region Party Account Posting...

                    intValue = 0;
                    double TotAmnt = 0;

                    TotAmnt = Convert.ToDouble(dblNetAmnt + dblOtherAmnt);
                    intValue = objclsAccountPosting.InsertInVchrDetl(
                    intVchrIdno,
                    intPartyIdno,
                    "",
                    TotAmnt,
                    Convert.ToByte(1),
                    Convert.ToByte(0),
                    "",
                    true,
                    dtBankDate,
                    "", Convert.ToInt32(base.CompId));

                    #endregion

                    #region Other Charges Account Posting...

                    if (intValue > 0)
                    {
                        if (Convert.ToDouble(dblOtherAmnt) > 0)
                        {

                            intValue = objclsAccountPosting.InsertInVchrDetl(
                            intVchrIdno,
                            Convert.ToInt64(objAcntLink.Other_Idno),
                            "",
                            Convert.ToDouble(dblOtherAmnt),
                            Convert.ToByte(2),
                            Convert.ToByte(0),
                            "",
                            false,
                            dtBankDate,
                            "", Convert.ToInt32(base.CompId));


                        }
                    }
                    else
                    {
                        return false;
                    }
                    #endregion

                    #region Vat/CST Account Posting...

                    if (intValue > 0 && dblVatCST > 0)
                    {
                        Int64 VatAcnt = 0;
                        if (ddlTaxType.SelectedValue == "1")
                        {
                            VatAcnt = Convert.ToInt64(objAcntLink.VSale_Idno);
                        }
                        else
                        {
                            VatAcnt = Convert.ToInt64(objAcntLink.CSale_Idno);
                        }
                        #region Vat and Retail Invoice Posting...
                        intValue = 0;
                        intValue = objclsAccountPosting.InsertInVchrDetl(
                        intVchrIdno,
                        Convert.ToInt64(VatAcnt),
                        "",
                        Math.Round(Convert.ToDouble(dblVatCST), 2),
                        Convert.ToByte(2),
                        Convert.ToByte(0),
                        "",
                        false,
                        dtBankDate,
                        "", Convert.ToInt32(base.CompId));

                        #endregion
                    }

                    #endregion

                    #region Sale Account Posting...

                    if (intValue > 0)
                    {

                        if (ddlTaxType.SelectedValue == "1")
                        {
                            #region CST ...
                            if (Convert.ToDouble(TotAmnt) > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno,
                                Convert.ToInt64(objAcntLink.VSale_Idno),
                                "",
                                Math.Round(Convert.ToDouble(dblNetAmnt), 2),
                                Convert.ToByte(2),
                                Convert.ToByte(0),
                                "",
                                false,
                                dtBankDate,
                                "", Convert.ToInt32(base.CompId));
                            }
                            #endregion
                        }
                        else
                        {
                            #region VAT....

                            if (Convert.ToDouble(TotAmnt) > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno,
                                Convert.ToInt64(objAcntLink.CSale_Idno),
                                "",
                                Math.Round(Convert.ToDouble(dblNetAmnt), 2),
                                Convert.ToByte(2),
                                Convert.ToByte(0),
                                "",
                                false,
                                dtBankDate,
                                "", Convert.ToInt32(base.CompId));
                            }
                            #endregion
                        }

                        if (intValue > 0)
                        {
                            intValue = 0;
                            intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                            if (intValue == 0)
                            {
                                return false;
                            }
                        }
                    }

                    #endregion

                    #region Discount Account Posting...

                    if (intValue > 0)
                    {
                        if (Convert.ToDouble(dblDiscAmnt) > 0)
                        {
                            intValue = objclsAccountPosting.InsertInVchrHead(
                            Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text.Trim())),
                            4,
                            0,
                            "Sale Bill No: " + Convert.ToString(intDocNo) + " Sale Bill Date: " + txtBillDate.Text.Trim(),
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            0,
                            dtDate,
                            0,
                            0,
                            base.intYearIdno,
                            Convert.ToInt32(base.CompId), intUserIdno);

                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                intValue = 0;
                                for (int i = 0; i < 2; i++)
                                {
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        (i == 0 ? intPartyIdno : Convert.ToInt64(objAcntLink.Disc_Idno)),
                                        "",
                                        Convert.ToDouble(Math.Abs(dblDiscAmnt)),
                                        Convert.ToByte(i == 0 ? 2 : 1),
                                        Convert.ToByte(0),
                                        "",
                                        Convert.ToBoolean(i == 0 ? true : false),
                                        null,
                                        "", Convert.ToInt32(base.CompId));
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (intValue > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }




                        }
                    }
                    else
                    {
                        return false;
                    }

                    #endregion

                    #region RoundOff Account Posting...

                    if (intValue > 0)
                    {
                        if (Convert.ToDouble(dblRndOff) != 0)
                        {
                            Int64 intRoundOffId = 0;
                            intRoundOffId = objclsAccountPosting.GetRoundOffId();
                            if (intRoundOffId == 0)
                            {
                                string Msg = "Invoice has amount in roundoff, but Round Off Account is not defined. Kindly define.";
                                ShowMessageErr(Msg);
                                return false;
                            }
                            intValue = objclsAccountPosting.InsertInVchrHead(
                            Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text.Trim())),
                            4,
                            0,
                            "Sale Bill No: " + Convert.ToString(intDocNo) + " Sale Bill Date: " + txtBillDate.Text.Trim(),
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            0,
                            dtDate,
                            0,
                            0,
                            base.intYearIdno,
                            Convert.ToInt32(base.CompId), intUserIdno);
                            if (intValue > 0)
                            {
                                intVchrIdno = intValue;
                                intValue = 0;
                                for (int i = 0; i < 2; i++)
                                {
                                    intValue = objclsAccountPosting.InsertInVchrDetl(
                                        intVchrIdno,
                                        (i == 0 ? intPartyIdno : intRoundOffId),
                                        "",
                                        Convert.ToDouble(Math.Abs(dblRndOff)),
                                        Convert.ToByte(i == 0 ? (dblRndOff < 0 ? 2 : 1) : (dblRndOff < 0 ? 1 : 2)),
                                        Convert.ToByte(0),
                                        "",
                                        Convert.ToBoolean(i == 0 ? true : false),
                                        null,
                                        "", Convert.ToInt32(base.CompId));
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                                if (intValue > 0)
                                {
                                    intValue = 0;
                                    intValue = objclsAccountPosting.InsertInVchrIdDetl(intVchrIdno, intDocIdno, strDocType);
                                    if (intValue == 0)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }

                        }
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            #endregion

            return true;
        }

        private void FillInformationDetails(Int64 SBillHead_Idno)
        {
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            tblSBillHead ObjHead = objSaleBillDAL.Select_SaleBillHead(SBillHead_Idno);
            DataTable ObjHeadDetail = objSaleBillDAL.Select_SaleBillDetail(ApplicationFunction.ConnectionString(), SBillHead_Idno);

            if (ObjHead != null)
            {
                BindSerialNo(SBillHead_Idno);
                SpanLastPrint.Visible = false;
                ViewState["SBillHead_Idno"] = SBillHead_Idno;
                ddlDateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtPrefixNo.Text = ObjHead.Prefix_No.ToString();
                txtBillNo.Text = ObjHead.SBill_No.ToString();
                txtBillDate.Text = string.IsNullOrEmpty(ObjHead.SBillHead_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.SBillHead_Date).ToString("dd-MM-yyyy");
                ddlParty.SelectedValue = ObjHead.Prty_Idno.ToString();
                ddlFromCity.SelectedValue = ObjHead.FromLoc_Idno.ToString();
                ddlTaxType.SelectedValue = ObjHead.Tax_Type.ToString();
                TxtRemark.Text = ObjHead.Remark.ToString();
                Int32 BillType = Convert.ToInt32(ObjHead.SBill_Type);
                var ClaimExists = objSaleBillDAL.CheckClaimExists(Convert.ToInt64(SBillHead_Idno));
                objSaleBillDAL = null;
                
                if (ClaimExists != null && ClaimExists > 0) { lnkbtnSave.Visible = false; DivbtnSave.Visible = false; } else { lnkbtnSave.Visible = true; DivbtnSave.Visible = true; }
                if (ObjHead.Bill_Against == 1)
                {
                    rdoCounter.Checked = true;
                    rdoCounter.Enabled = false;
                    grdMain.Columns[2].Visible = false;
                    DivItems.Visible = true; lnkbtnContnrDtl.Visible = false;
                    ddlFromCity.Enabled = false;
                    lnkbtnDriverRefresh.Visible = false;
                }
                else
                {
                    rdoMTIssue.Checked = true;
                    rdoMTIssue.Enabled = false;
                    grdMain.Columns[0].Visible = false;
                    DivItems.Visible = false; lnkbtnContnrDtl.Visible = true;
                    ddlFromCity.Enabled = false;
                    lnkbtnDriverRefresh.Visible = false;
                    ddlParty.Enabled = false;
                    lnkbtnRefreshParty.Visible = false;
                    

                }
                ddlBillType.Enabled = false;
                dtTemp = CreateDt();
                for (int counter = 0; counter < ObjHeadDetail.Rows.Count; counter++)
                {
                    string strSerialNo = Convert.ToString(ObjHeadDetail.Rows[counter]["SerialNo"]);
                    string strSerialNoIdno = Convert.ToString(ObjHeadDetail.Rows[counter]["SerialNoIdno"]);
                    string strModelName = Convert.ToString(ObjHeadDetail.Rows[counter]["ModelName"]);
                    string strModelIdNo = Convert.ToString(ObjHeadDetail.Rows[counter]["ModelNameIdno"]);
                    string strTyreType = Convert.ToString(ObjHeadDetail.Rows[counter]["TyreTypeName"]);
                    string strTyreTypeIdNo = Convert.ToString(ObjHeadDetail.Rows[counter]["TyreType"]);
                    string strRateType = Convert.ToString(ObjHeadDetail.Rows[counter]["RateTypeName"]);
                    string strRateTypeIdno = Convert.ToString(ObjHeadDetail.Rows[counter]["RateType"]);
                    string strQuantity = Convert.ToString(ObjHeadDetail.Rows[counter]["Qty"]);
                    string strWeight = Convert.ToString(ObjHeadDetail.Rows[counter]["Weight"]);
                    string strRate = Convert.ToString(ObjHeadDetail.Rows[counter]["Rate"]);
                    string strAmount = Convert.ToString(ObjHeadDetail.Rows[counter]["Amount"]);
                    string strDiscountType = Convert.ToString(ObjHeadDetail.Rows[counter]["ItemDiscType"]);
                    string strDiscount = Convert.ToString(ObjHeadDetail.Rows[counter]["ItemDiscAMNT"]);
                    string strTaxAmnt = Convert.ToString(ObjHeadDetail.Rows[counter]["TaxAmnt"]);
                    string strTaxRate = Convert.ToString(ObjHeadDetail.Rows[counter]["TaxRate"]);
                    string strDisValue = Convert.ToString(ObjHeadDetail.Rows[counter]["DiscountValue"]);
                    string strMatIssueNo = Convert.ToString(ObjHeadDetail.Rows[counter]["MatIssNo"]);
                    string strItemType = Convert.ToString(ObjHeadDetail.Rows[counter]["ItemType"]);
                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strSerialNo, strSerialNoIdno, strModelName, strModelIdNo, strTyreType, strTyreTypeIdNo, strRateType, strRateTypeIdno, strQuantity, strWeight, strRate, strAmount, strDiscountType, strDiscount, strTaxAmnt, strTaxRate, strDisValue, strMatIssueNo, strItemType);
                }
                

                ViewState["dt"] = dtTemp;
                BindGridT();
                txtDiscountPer.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Disc_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.Disc_Amnt);
                txtDiscountPer_TextChanged(null, null);
                txtTotalAmount.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Tot_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.Tot_Amnt);
                txtOtherCharges.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Other_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.Other_Amnt);
                txtRoundOff.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.RndOff_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.RndOff_Amnt);
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
                ddlDiscountType.SelectedValue = ObjHead.Disc_type.ToString();

                Print(SBillHead_Idno);
            }
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void clearAllControl()
        {
            ddlFromCity.Enabled = true; lnkbtnDriverRefresh.Visible = true; ddlBillType.Enabled = true; rdoCounter.Enabled = true; rdoMTIssue.Enabled = true;
            rdoCounter.Checked = true;
            ddlParty.Enabled = true; lnkbtnRefreshParty.Visible = true; ddlTaxType.Enabled = true;
            ddlModelName.SelectedValue= ddltype.SelectedValue=  ddlSerialNo.SelectedValue = ddlRateType.SelectedValue = "0";
            txtPrefixNo.Text = TxtRemark.Text = "";
            txtweight.Text = txtrate.Text = txtDiscount.Text = txtDiscountPer.Text = txtOtherCharges.Text = txtRoundOff.Text = txtTotalAmount.Text = txtNetAmnt.Text = "0.00";
            txtQuantity.Text = ddlDiscountType.SelectedValue = "1";
            ddlDiscountType.Enabled = false; txtDiscountPer.Enabled = false;
            dtTemp = null; ViewState["dt"] = null; this.BindGridT();
        }

        private void Print(Int64 SBillHead_Idno)
        {
            Repeater obj = new Repeater();

            string CompName = ""; string TinNo = ""; string FaxNo = "";

            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");

            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblCompAdd1.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            lblCompAdd2.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);

            lblCompPhNo.Text = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            lblCompCity.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            lblCompState.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }

            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spSaleBill] @ACTION='SelectPrint',@SBillHead_Idno='" + SBillHead_Idno + "'");
            dsReport.Tables[0].TableName = "BillPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblBillNoval.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["PrefixNo"]) + " " + Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["SBillNo"]);
                lblBillDateVal.Text = Convert.ToDateTime(dsReport.Tables["BillPrint"].Rows[0]["SBillDate"]).ToString("dd-MM-yyyy");
                lblFromCityVal.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["FromLocation"]);
                lblPartyVal.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["PartyName"]);
                lblBillTypeVal.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["BillType"]);
                lblTaxTypeVal.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["TaxType"]);

                if (Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["Remark"]) != "")
                    lblremark.Text = "Remark:<br/>" + Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["Remark"]);

                if (dsReport.Tables["BillPrint"].Rows[0]["OtherAmnt"].ToString() != "0")
                    lblOtherChargesValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["OtherAmnt"]));
                else
                    trOtherchrg.Visible = false;

                if (dsReport.Tables["BillPrint"].Rows[0]["DiscAmnt"].ToString() != "0")
                    lblDiscountValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["DiscAmnt"]));
                else
                    trOtherchrgDiscount.Visible = false;

                lblRoundoffValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["RndOffAmnt"]));

                lblNetAmountValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["NetAmnt"]));

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();

            }
        }

        private void GetMaxBillNo()
        {

            Int64 intBillType = string.IsNullOrEmpty(Convert.ToString(ddlBillType.SelectedValue)) ? 1 : Convert.ToInt64(ddlBillType.SelectedValue);
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            txtBillNo.Text = objSaleBillDAL.GetSBillNoMax(string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(txtPrefixNo.Text.Trim()) ? "" : Convert.ToString(txtPrefixNo.Text.Trim()), intBillType, Convert.ToInt64(ddlDateRange.SelectedValue)).ToString();
            objSaleBillDAL=null;
        }
        private void NetAmountCalculate()
        {
            double netAmount=0;
            if (ddlDiscountType.Enabled == true && txtDiscountPer.Enabled == true)
            {
                netAmount = ((string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTotalAmount.Text.Trim())) - (string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? 0.00 : Convert.ToDouble(txtDiscount.Text.Trim()))) + (string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0.00 : Convert.ToDouble(txtOtherCharges.Text.Trim()));
            }
            else
            {
                netAmount = (string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTotalAmount.Text.Trim())) + (string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0.00 : Convert.ToDouble(txtOtherCharges.Text.Trim()));
            }

            double RoudedValue = Math.Round(netAmount);
            txtRoundOff.Text = (Convert.ToDouble(RoudedValue) - netAmount).ToString("N2");
            txtNetAmnt.Text = RoudedValue.ToString("N2");
        }

        private void EnableDisableCal()
        {
            if (dtotalDiscountAmnt > 0)
            {
                ddlDiscountType.Enabled = false; txtDiscountPer.Text = "0.00"; txtDiscountPer.Enabled = false;
                txtDiscount.Text = dtotalDiscountAmnt.ToString("N2");
            }
            else
            {
                ddlDiscountType.Enabled = true; txtDiscountPer.Enabled = true;

                if (ddlDiscountType.SelectedValue == "1")
                {
                    txtDiscount.Text = txtDiscountPer.Text;
                }
                else if (ddlDiscountType.SelectedValue == "2")
                {
                    double DiscountPer = string.IsNullOrEmpty(txtDiscountPer.Text.Trim()) ? 0.00 : Convert.ToDouble(txtDiscountPer.Text.Trim());
                    double TotalAmnt = string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? 0.00 : Convert.ToDouble(txtTotalAmount.Text.Trim());
                    txtDiscount.Text = (Convert.ToDouble(TotalAmnt * DiscountPer) / 100).ToString("N2");
                    
                }

            }
            
        }

        private bool CalcDiscountOnAllItem()
        {
            double Discount=0;
            string TotalNetAmnt = "0.00";
            if ((string.IsNullOrEmpty(ddlDiscountType.SelectedValue) ? 1 : Convert.ToDouble(ddlDiscountType.SelectedValue)) == 1)
            {
                txtDiscount.Text = (string.IsNullOrEmpty(txtDiscountPer.Text) ? 0 : Convert.ToDouble(txtDiscountPer.Text)).ToString("N2");
                Discount = Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPer.Text.ToString()) ? 0 : Convert.ToDouble(txtDiscountPer.Text.ToString()));
                TotalNetAmnt = ((string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) ? 0 : Convert.ToDouble(txtTotalAmount.Text)) - (string.IsNullOrEmpty(txtDiscountPer.Text) ? 0 : Convert.ToDouble(txtDiscountPer.Text))).ToString("N2");
            }
            else if ((string.IsNullOrEmpty(ddlDiscountType.SelectedValue) ? 1 : Convert.ToDouble(ddlDiscountType.SelectedValue)) == 2)
            {
                txtDiscount.Text = (((string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) ? 0 : Convert.ToDouble(txtTotalAmount.Text)) * (string.IsNullOrEmpty(txtDiscountPer.Text) ? 0 : Convert.ToDouble(txtDiscountPer.Text))) / 100).ToString("N2");
                Discount = Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPer.Text.ToString()) ? 0 : Convert.ToDouble(txtDiscountPer.Text.ToString()));
                if (Discount >= 100) { ShowMessageErr("Discount % should be lasser than 100%."); txtDiscountPer.Text = "0.00"; txtDiscount.Text = "0.00"; txtDiscountPer.Focus(); return true; }
                TotalNetAmnt = ((string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) ? 0 : Convert.ToDouble(txtTotalAmount.Text)) - ((string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) ? 0 : Convert.ToDouble(txtTotalAmount.Text)) * (string.IsNullOrEmpty(txtDiscountPer.Text) ? 0 : Convert.ToDouble(txtDiscountPer.Text))) / 100).ToString("N2");
            }
            txtNetAmnt.Text = (Convert.ToDouble(TotalNetAmnt.ToString()) + (string.IsNullOrEmpty(Convert.ToString(txtOtherCharges.Text)) ? 0 : Convert.ToDouble(txtOtherCharges.Text)) + (string.IsNullOrEmpty(Convert.ToString(txtRoundOff.Text)) ? 0 : Convert.ToDouble(txtRoundOff.Text))).ToString("N2");
            if ((string.IsNullOrEmpty(Convert.ToString(txtTotalAmount.Text)) ? 0 : Convert.ToDouble(txtTotalAmount.Text)) <= Discount) { ShowMessageErr("Discount Should be lesser than Total Amount."); txtDiscountPer.Text = "0.00"; txtDiscount.Text = "0.00"; txtDiscountPer.Focus(); return false; }
            return true;
            
        }

        private bool CalcRate()
        {
            if ((string.IsNullOrEmpty(ddlRateType.SelectedValue) ? 0 : Convert.ToInt64(ddlRateType.SelectedValue)) > 0)
            {
                #region Comment By Peeyush Kaushik (Tyre Case Always Qty * Rate)......
                //if (Convert.ToInt64(ddlRateType.SelectedValue) == 1)
                //{
                //    Qty = string.IsNullOrEmpty(txtQuantity.Text) ? 1 : Convert.ToDouble(txtQuantity.Text);
                //    Rate = string.IsNullOrEmpty(txtrate.Text) ? 0.00 : Convert.ToDouble(txtrate.Text);
                //}
                //else if (Convert.ToInt64(ddlRateType.SelectedValue) == 2)
                //{
                //    Qty = string.IsNullOrEmpty(txtQuantity.Text) ? 1 : Convert.ToDouble(txtQuantity.Text);
                //    Rate = string.IsNullOrEmpty(txtweight.Text) ? 0.00 : Convert.ToDouble(txtweight.Text);
                //}
                #endregion
                Qty = string.IsNullOrEmpty(txtQuantity.Text) ? 1 : Convert.ToDouble(txtQuantity.Text);
                Rate = string.IsNullOrEmpty(txtrate.Text) ? 0.00 : Convert.ToDouble(txtrate.Text);

                if ((string.IsNullOrEmpty(ddlDiscountItemWise.SelectedValue) ? 1 : Convert.ToDouble(ddlDiscountItemWise.SelectedValue)) == 1)
                {
                    DiscountAmnt = string.IsNullOrEmpty(txtDiscountItem.Text) ? 0.00 : Convert.ToDouble(txtDiscountItem.Text);
                }
                else if ((string.IsNullOrEmpty(ddlDiscountItemWise.SelectedValue) ? 1 : Convert.ToDouble(ddlDiscountItemWise.SelectedValue)) == 2)
                {
                    if ((string.IsNullOrEmpty(txtDiscountItem.Text) ? 0.00 : Convert.ToDouble(txtDiscountItem.Text)) >= 100) { ShowMessageErr("Discount % should be lesser than 100%."); txtDiscountItem.Text = "0.00"; txtDiscountItem.Focus(); return false; }
                    DiscountAmnt = Convert.ToDouble(Convert.ToDouble(((Qty * Rate) * (string.IsNullOrEmpty(txtDiscountItem.Text) ? 0 : Convert.ToDouble(txtDiscountItem.Text)) / 100)).ToString("N2"));
                }

                Tax = Convert.ToDouble((((Rate * Qty) - DiscountAmnt) * Convert.ToDouble(string.IsNullOrEmpty(HidTaxRate.Value) ? "0" : HidTaxRate.Value)) / 100);
                HidTax.Value = (Tax).ToString("N2");
                Amount = Convert.ToDouble(Convert.ToDouble(((Qty * Rate) + Tax) - (DiscountAmnt)).ToString("N2"));
               
                if (Rate <= DiscountAmnt) { ShowMessageErr("Discount Should be lesser than Rate."); txtDiscountItem.Text = "0.00"; txtDiscountItem.Focus(); return false; }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BindModel()
        {
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            var ModelName = objSaleBillDAL.BindModelName();
            objSaleBillDAL = null;
            ddlModelName.DataSource = ModelName;
            ddlModelName.DataTextField = "ModelName";
            ddlModelName.DataValueField = "ModelIdno";
            ddlModelName.DataBind();
            ddlModelName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Model...", "0"));
            ddlModelName.SelectedIndex = 0;
        }
        
        private void BindModel(Int64 SrIdno)
        {
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            var ModelName = objSaleBillDAL.BindModelName(SrIdno);
            objSaleBillDAL = null;
            ddlModelName.DataSource = ModelName;
            ddlModelName.DataTextField = "ModelName";
            ddlModelName.DataValueField = "ModelIdno";
            ddlModelName.DataBind();
            ddlModelName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Model...", "0"));
            //if (ModelName.Count <= 0 || ModelName == null)
            //{ ddlModelName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Model...", "0")); }
            //ddlModelName.SelectedIndex = 0;
        }
        private void BindTyreCategory(Int64 SrIdno)
        {
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            var TyreCategory = objSaleBillDAL.BindTyreType(SrIdno);
            objSaleBillDAL = null;
            ddltype.DataSource = TyreCategory;
            ddltype.DataTextField = "TyreType";
            ddltype.DataValueField = "TyreTypeIdno";
            ddltype.DataBind();
            if (TyreCategory.Count <= 0 || TyreCategory == null)
            { ddltype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose type...", "0")); }
            ddltype.SelectedIndex = 0;
        }
        private void BindTyreCategory()
        {
            objBindDropdown = new BindDropdownDAL();
            var TyreCategory = objBindDropdown.BindTyreType();
            objBindDropdown = null;
            ddltype.DataSource = TyreCategory;
            ddltype.DataTextField = "TyreType";
            ddltype.DataValueField = "TyreTypeIdno";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose type...", "0")); 
            ddltype.SelectedIndex = 0;
        }
        private void BindDropdown()
        {
            obj = new BindDropdownDAL();
            DataTable dtParty = new DataTable();
            dtParty = obj.BindPartyForSale(ApplicationFunction.ConnectionString());
            obj = null;
            ddlParty.DataSource = dtParty;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party...", "0"));
        }
        private void BindCity()
        {
            obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }
        private void BindDateRange()
        {
            objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange();
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void BindSerialNo()
        {
            objBindDropdown = new BindDropdownDAL();
            ddlSerialNo.DataSource = objBindDropdown.BindSerialNo();
            objBindDropdown = null;
            ddlSerialNo.DataTextField = "SerialNo";
            ddlSerialNo.DataValueField = "SerlDetlIdno";
            ddlSerialNo.DataBind();
            ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Item....", "0"));
            ddlSerialNo.SelectedIndex = 0;
            
        }

        private void BindSerialNo(Int64 BillIdno)
        {
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            ddlSerialNo.DataSource = objSaleBillDAL.BindSerialNoPopulate(BillIdno);
            objBindDropdown = null;
            ddlSerialNo.DataTextField = "SerialNo";
            ddlSerialNo.DataValueField = "SerlDetlIdno";
            ddlSerialNo.DataBind();
            ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Item....", "0"));
            ddlSerialNo.SelectedIndex = 0;
            
        }
        



        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "SerialNo", "String",
                "SerialNoIdno","String",
                "ModelName", "String",
                "ModelIdNo", "String",
                "TyreType", "String",
                "TyreTypeIdNo", "String",
                "RateType", "String",
                "RateTypeIdno", "String",
                "Quantity", "String",
                "Weight", "String",
                "Rate", "String",
                "Amount", "String",
                "DiscountType","String",
                "Discount","String",
                "TaxAmnt","String",
                "TaxRate", "String",
                "DiscountValue", "String",
                "MatIssNo","String",
                "ItemType","String",
                "MatIss_Idno", "String");
            return dttemp;
        }

        private DataTable CreateDivDt()
        {
            DataTable temp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "MatIssIdno","String",
                "MatIssNo", "String",
                "MatIssDate","String",
                "LorryNo","String",
                "CityName","String",
                "PrtyIdno", "String",
                "PrtyName", "String");
            return temp;
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            objFinYearDAL = null;
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtBillDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDivDateFrom.Text = hidmindate.Value;
                    txtDivDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtBillDate.Text = hidmindate.Value;
                    txtDivDateFrom.Text = hidmindate.Value;
                    txtDivDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
            }

        }
        private void BindDivGrid()
        {
            if (ViewState["dtDivGrid"] != null)
            {
                dtDivTemp = (DataTable)ViewState["dtDivGrid"];
                if (dtDivTemp.Rows.Count > 0)
                {
                    grdMIdetals.DataSource = dtDivTemp;
                    grdMIdetals.DataBind();
                    lblSearchRecords.InnerText = "T. Record(s) : " + dtDivTemp.Rows.Count + "";
                    SetFocus(lnkbtnSearch.ClientID);
                }
                else
                {

                    dtDivTemp = null;
                    grdMIdetals.DataSource = dtDivTemp;
                    grdMIdetals.DataBind();
                    lblSearchRecords.InnerText = "T. Record(s) : 0";
                    SetFocus(lnkbtnSearch.ClientID);
                }
            }
            else
            {
                dtDivTemp = null;
                grdMIdetals.DataSource = dtDivTemp;
                grdMIdetals.DataBind();
                lblSearchRecords.InnerText = "T. Record(s) : 0";
                SetFocus(lnkbtnSearch.ClientID);
            }
        }


        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                    ddlTaxType.Enabled = false;
                }
                else
                {

                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                    hidrowid.Value = ""; lblmessage.Text = "";
                    ddlSerialNo.Enabled = true;
                    ddlTaxType.Enabled = true;
                    ddlSerialNo.SelectedIndex = 0; this.BindModel(); this.BindTyreCategory(); ddlRateType.SelectedIndex = 0; ddlDiscountItemWise.SelectedValue = "1";
                    txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00"; txtDiscountItem.Text = "0.00";
                }
            }
            else
            {
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
            SetFocus(TxtRemark.ClientID);
        }

        private void ClearItems()
        {
            hidrowid.Value = ""; lblmessage.Text = "";
            ddlSerialNo.Enabled = true;
            ddlSerialNo.SelectedIndex = 0; this.BindModel(); this.BindTyreCategory(); ddlRateType.SelectedIndex = 0; ddlDiscountItemWise.SelectedValue = "1";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00"; txtDiscountItem.Text = "0.00";
            
        }
        private bool CheckDuplicatieItem()
        {
            bool value = true;
            DataTable dtTemp = (DataTable)ViewState["dt"];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0)) { foreach (DataRow row in dtTemp.Rows) { if ((Convert.ToString(row["SerialNoIdno"]) == Convert.ToString(ddlSerialNo.SelectedValue))) { value = false; } } }
            if (value == false) { return false; }
            else { return true; }
        }

        private void ValidateControls()
        {
            //txtPrefixNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
         
        }
        #endregion

        #region Button Event..
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("SaleBill.aspx");
            
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            DataTable dt = CreateDivDt();
            DateTime? DateFrom = null;DateTime? DateTo = null;
            DateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateFrom.Text));
            DateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDivDateFrom.Text));
            SaleBillDAL objSaleBillDAL = new SaleBillDAL();
            Int64 Location = string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue);

            var lst = objSaleBillDAL.SelectMIssue(DateFrom, DateTo, (string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue)), Location);
            objSaleBillDAL = null;
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    string MatIssIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "MatIss_Idno"))) ? "" : Convert.ToString((DataBinder.Eval(lst[i], "MatIss_Idno")));
                    string MatIssNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "MatIss_No"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "MatIss_No"));
                    string MatIssDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "MatIss_Date"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[i], "MatIss_Date")).ToString("dd-MM-yyyy"));
                    string LorryNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Lorry_No"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "Lorry_No"));
                    string CityName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "City_Name"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "City_Name"));
                    string PrtyIdno = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "Prty_Idno"))) ? "0" : Convert.ToString(DataBinder.Eval(lst[i], "Prty_Idno"));
                    string PrtyName = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[i], "PartyName"))) ? "" : Convert.ToString(DataBinder.Eval(lst[i], "PartyName"));

                    ApplicationFunction.DatatableAddRow(dt, i + 1, MatIssIdno, MatIssNo, ApplicationFunction.mmddyyyy(MatIssDate), LorryNo, CityName, PrtyIdno, PrtyName);
                }
                ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "Prty_Idno"))) ? "0" : Convert.ToString((DataBinder.Eval(lst[0], "Prty_Idno")));
                ViewState["dtDivGrid"] = dt;
                dt.Dispose();
                this.BindDivGrid();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
        }
        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((grdMIdetals != null) && (grdMIdetals.Rows.Count > 0))
                {
                    string MIDetlValue = string.Empty;
                    for (int count = 0; count < grdMIdetals.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdMIdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidMIIdno = (HiddenField)grdMIdetals.Rows[count].FindControl("hidMIIdno");
                            MIDetlValue = MIDetlValue + hidMIIdno.Value + ",";
                        }
                    }
                    if (MIDetlValue != "")
                    {
                        MIDetlValue = MIDetlValue.Substring(0, MIDetlValue.Length - 1);
                    }
                    if (MIDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Material issue No.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                    }
                    else
                    {
                        grdMain.Columns[0].Visible = false;
                        string Result = String.Empty;
                        SaleBillDAL objSaleBillDAL = new SaleBillDAL();
                        Result = objSaleBillDAL.CheckOneParty(ApplicationFunction.ConnectionString(), Convert.ToString(MIDetlValue), Convert.ToInt32(ddlDateRange.SelectedValue));
                        if (Result != null && Result != "0" && Result == "1")
                        {
                            string strSbillNo = String.Empty;
                            DataTable dt = new DataTable();
                            dt = objSaleBillDAL.SelectMIssueForSale(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(ddlTaxType.SelectedValue), Convert.ToString(MIDetlValue));

                            objSaleBillDAL = null;
                            ViewState["dt"] = dt;
                            this.BindGridT();
                        }
                        else
                        {
                            ShowMessageErr("Please Check Only Same Party Matrial Issue.");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkbtnClose_Click(object sender, EventArgs e)
        {
            grdMIdetals.DataSource = null;
            grdMIdetals.DataBind();
            SetFocus(lnkbtnContnrDtl.UniqueID);
        }
        protected void lnkbtnSubmitClick_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? 0 : Convert.ToInt64(ddlSerialNo.SelectedValue)) == 0) { ShowMessageErr("Please Serial Number!"); ddlSerialNo.Focus(); return; }
            if ((string.IsNullOrEmpty(ddlModelName.SelectedValue) ? 0 : Convert.ToInt64(ddlModelName.SelectedValue)) == 0) { ShowMessageErr("Please select Model!"); ddlModelName.Focus(); return; }
            //if ((string.IsNullOrEmpty(ddltype.SelectedValue) ? 0 : Convert.ToInt64(ddltype.SelectedValue)) == 0) { ShowMessageErr("Please select Tyre type!"); ddltype.Focus(); return; }
            if ((string.IsNullOrEmpty(ddlRateType.SelectedValue) ? 0 : Convert.ToInt64(ddlRateType.SelectedValue)) == 0) { ShowMessageErr("Please select the Rate Type."); ddlRateType.Focus(); return; }
            if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0) { ShowMessageErr("Rate should be greater than zero."); txtrate.Focus(); return; }
            
            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero!"); txtQuantity.Focus(); return; }

            #region Check Item Duplicate on ItemName and ItemUnit (By Peeyush Kaushik)

            if (hidrowid.Value == string.Empty)
            {
                if (CheckDuplicatieItem() == false) { this.ShowMessageErr("Serial No. already selected!"); this.ClearItems(); ddlSerialNo.Focus(); return; }
            }
            #endregion

            dtTemp = (DataTable)ViewState["dt"];

            if (CalcRate())
            {
                if (hidrowid.Value != string.Empty)
                {
                    dtTemp = (DataTable)ViewState["dt"];
                    foreach (DataRow dtrow in dtTemp.Rows)
                    {
                        if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                        {
                            dtrow["SerialNo"] = string.IsNullOrEmpty(ddlSerialNo.SelectedItem.Text) ? "" : Convert.ToString(ddlSerialNo.SelectedItem.Text);
                            dtrow["SerialNoIdno"] = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? "0" : Convert.ToString(ddlSerialNo.SelectedValue);
                            dtrow["ModelName"] = string.IsNullOrEmpty(ddlModelName.SelectedItem.Text) ? "" : Convert.ToString(ddlModelName.SelectedItem.Text);
                            dtrow["ModelIdNo"] = string.IsNullOrEmpty(ddlModelName.SelectedValue) ? "0" : Convert.ToString(ddlModelName.SelectedValue);
                            dtrow["TyreType"] = string.IsNullOrEmpty(ddltype.SelectedItem.Text) ? "" : Convert.ToString(ddlSerialNo.SelectedItem.Text);
                            dtrow["TyreTypeIdNo"] = string.IsNullOrEmpty(ddltype.SelectedValue) ? "0" : Convert.ToString(ddltype.SelectedValue);
                            dtrow["RateType"] = string.IsNullOrEmpty(ddlRateType.SelectedItem.Text) ? "" : Convert.ToString(ddlSerialNo.SelectedItem.Text);
                            dtrow["RateTypeIdno"] = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : Convert.ToString(ddlRateType.SelectedValue);
                            dtrow["Quantity"] = string.IsNullOrEmpty(txtQuantity.Text) ? "0.00" : Convert.ToString(txtQuantity.Text);
                            iqty += Convert.ToDouble(txtQuantity.Text.Trim());
                            dtrow["Weight"] = string.IsNullOrEmpty(txtweight.Text) ? "0.00" : Convert.ToString(txtweight.Text);
                            dtrow["Rate"] = string.IsNullOrEmpty(txtrate.Text) ? "0.00" : Convert.ToString(txtrate.Text);
                            dtrow["Amount"] = Amount;
                            dtrow["DiscountType"] = string.IsNullOrEmpty(ddlDiscountItemWise.SelectedValue) ? "1" : Convert.ToString(ddlDiscountItemWise.SelectedValue);
                            dtrow["Discount"] = string.IsNullOrEmpty(DiscountAmnt.ToString()) ? "0.00" : Convert.ToString(DiscountAmnt.ToString());
                            dtrow["TaxAmnt"] = string.IsNullOrEmpty(HidTax.Value) == true ? "0.00" : Convert.ToString(HidTax.Value);
                            dtrow["TaxRate"] = string.IsNullOrEmpty(HidTaxRate.Value) == true ? "0.00" : Convert.ToString(HidTaxRate.Value);
                            dtrow["DiscountValue"] = string.IsNullOrEmpty(txtDiscountItem.Text.ToString()) ? "0.00" : Convert.ToString(txtDiscountItem.Text.ToString());
                            dtrow["MatIssNo"] = "";
                            dtrow["ItemType"] = string.IsNullOrEmpty(HideItemType.Value) ? 0 : Convert.ToInt32(HideItemType.Value);
                            dtrow["MatIss_Idno"] = 0; 
                        }
                    }

                    ViewState["ID"] = null;
                }
                else
                {
                    dtTemp = (DataTable)ViewState["dt"];
                    if (dtTemp == null)
                    {
                        dtTemp = CreateDt();
                    }
                    Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                    int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                    string strSerialNo = ddlSerialNo.SelectedItem.Text.Trim();
                    string strSerialNoIdno = string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? "0" : (ddlSerialNo.SelectedValue);
                    string strModelName = ddlModelName.SelectedItem.Text.Trim();
                    string strModelIdNo = string.IsNullOrEmpty(ddlModelName.SelectedValue) ? "0" : (ddlModelName.SelectedValue);
                    string strTyreType = ddltype.SelectedItem.Text.Trim();
                    string strTyreTypeIdNo = string.IsNullOrEmpty(ddltype.SelectedValue) ? "0" : (ddltype.SelectedValue);
                    string strRateType = ddlRateType.SelectedItem.Text.Trim();
                    string strRateTypeIdno = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : (ddlRateType.SelectedValue);
                    string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                    string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0.00" : (txtweight.Text.Trim());
                    string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                    string strAmount = Amount.ToString();
                    string strDiscountType = string.IsNullOrEmpty(ddlDiscountItemWise.SelectedValue) ? "1" : (ddlDiscountItemWise.SelectedValue);
                    string strDiscount = string.IsNullOrEmpty(DiscountAmnt.ToString()) ? "0.00" : (DiscountAmnt.ToString());
                    string strTaxAmnt = HidTax.Value;
                    string strTaxRate = HidTaxRate.Value;
                    string strDiscountValue = string.IsNullOrEmpty(txtDiscountItem.Text.ToString()) ? "0.00" : (txtDiscountItem.Text.ToString());
                    string strMatissueIdno = "0"; string strMatissueNo = "";
                    string strItemType = string.IsNullOrEmpty(HideItemType.Value) ? "0" : Convert.ToString(HideItemType.Value);
                    ApplicationFunction.DatatableAddRow(dtTemp, id, strSerialNo, strSerialNoIdno, strModelName, strModelIdNo, strTyreType, strTyreTypeIdNo, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount, strDiscountType, strDiscount, strTaxAmnt, strTaxRate, strDiscountValue, strMatissueNo, strItemType, strMatissueIdno);
                    ViewState["dt"] = dtTemp;
                }
            }
            else
            {
                return;
            }
            this.BindGridT();
            grdMain.Columns[2].Visible = false;
            ClearItems();
            ddlSerialNo.Focus();
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string msg = "";
                Int64 varForSave = 0;
                DateTime? BillDate = null;
                BillDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text));
                int BillType = 0; if ((string.IsNullOrEmpty(Convert.ToString(ddlBillType.SelectedValue)) ? 1 : Convert.ToInt32(ddlBillType.SelectedValue)) == 1) { BillType = 1; } else if ((string.IsNullOrEmpty(Convert.ToString(ddlBillType.SelectedValue)) ? 1 : Convert.ToInt32(ddlBillType.SelectedValue)) == 2) { BillType = 2; }
                Int64 Against = 0; if (rdoCounter.Checked == true) { Against = 1; } else if (rdoMTIssue.Checked == true) { Against = 2; }
                
                DateTime CurrentDate = System.DateTime.Now;
                DataTable dtDetail = (DataTable)ViewState["dt"];

                SaleBillDAL obj = new SaleBillDAL();
                if (dtDetail.Rows.Count > 0)
                {
                    if (Convert.ToDouble(txtNetAmnt.Text) > 0)
                    {
                        using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                        {
                            if (ViewState["SBillHead_Idno"] != null)
                            {
                                varForSave = obj.Update(Convert.ToInt64(ViewState["SBillHead_Idno"].ToString()), BillDate,  txtPrefixNo.Text.Trim(), Convert.ToInt64(txtBillNo.Text), BillType, Against, string.IsNullOrEmpty(ddlTaxType.SelectedValue) ? 0 : Convert.ToInt32(ddlTaxType.SelectedValue), string.IsNullOrEmpty(ddlParty.SelectedValue) ? 0 : Convert.ToInt64(ddlParty.SelectedValue), string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), TxtRemark.Text.Trim(), 0, string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0 : Convert.ToDouble(txtOtherCharges.Text.Trim()), string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0 : Convert.ToDouble(txtRoundOff.Text.Trim()), string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0 : Convert.ToDouble(txtNetAmnt.Text.Trim()), string.IsNullOrEmpty(ddlDiscountType.SelectedValue) ? 1 : Convert.ToInt32(ddlDiscountType.SelectedValue), string.IsNullOrEmpty(txtDiscountPer.Text.Trim()) ? 0 : Convert.ToDouble(txtDiscountPer.Text.Trim()), string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0 : Convert.ToDouble(txtOtherCharges.Text.Trim()), string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtTotalAmount.Text.Trim()), string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue), CurrentDate, dtDetail);
                                if (varForSave > 0)
                                {
                                    string strRoundOff = txtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                                    if (this.PostIntoAccounts(dtDetail, varForSave, "SB", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), base.CompId, base.UserIdno, 0, 0) == true)
                                    {

                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["SBillHead_Idno"])) == false)
                                        {
                                            Tran.Complete();
                                            lnkbtnNew.Visible = false; lnkbtnPrint.Visible = false;
                                            msg = "Record(s) Updated Successfully.";
                                        }
                                        this.clearAllControl();
                                        this.ShowMessage(msg);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["SBillHead_Idno"])) == false)
                                        {
                                            Tran.Dispose();
                                            msg = "Record(s) Not Updated!";
                                        }
                                        ShowMessageErr(msg);
                                    }
                                }
                                else
                                {
                                    Tran.Dispose();
                                    this.ShowMessageErr("Record Not Updated!");
                                }
                            }
                            else
                            {
                                varForSave = obj.Insert(BillDate, txtPrefixNo.Text.Trim(), Convert.ToInt64(txtBillNo.Text), BillType, Against, string.IsNullOrEmpty(ddlTaxType.SelectedValue) ? 0 : Convert.ToInt32(ddlTaxType.SelectedValue), string.IsNullOrEmpty(ddlParty.SelectedValue) ? 0 : Convert.ToInt64(ddlParty.SelectedValue), string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), TxtRemark.Text.Trim(), 0, string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0 : Convert.ToDouble(txtOtherCharges.Text.Trim()), string.IsNullOrEmpty(txtRoundOff.Text.Trim()) ? 0 : Convert.ToDouble(txtRoundOff.Text.Trim()), string.IsNullOrEmpty(txtNetAmnt.Text.Trim()) ? 0 : Convert.ToDouble(txtNetAmnt.Text.Trim()), string.IsNullOrEmpty(ddlDiscountType.SelectedValue) ? 1 : Convert.ToInt32(ddlDiscountType.SelectedValue), string.IsNullOrEmpty(txtDiscountPer.Text.Trim()) ? 0 : Convert.ToDouble(txtDiscountPer.Text.Trim()), string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? 0 : Convert.ToDouble(txtOtherCharges.Text.Trim()), string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? 0 : Convert.ToDouble(txtTotalAmount.Text.Trim()), string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue), CurrentDate, dtDetail);
                                if (varForSave > 0)
                                {
                                    string strRoundOff = txtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                                    if (this.PostIntoAccounts(dtDetail, varForSave, "SB", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), base.CompId, base.UserIdno, 0, 0) == true)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["PBillHead_Idno"])) == true)
                                        {
                                            
                                            Tran.Complete();
                                            msg = "Record(s) Saved Successfully.";
                                            this.clearAllControl();
                                            this.ShowMessage(msg);
                                        }
                                    }
                                    else
                                    {
                                        Tran.Dispose();
                                        msg = "Record(s) not saved!";

                                        ShowMessageErr(msg);
                                    }
                                   
                                }
                                else
                                {
                                    this.ShowMessageErr("Record Not Saved!");
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageErr("Net Amount Should Be Greater Than 0!");
                    }
                }
                else
                {
                    this.ShowMessageErr("Please Select Item Details!");
                }
            }
        }
        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["SbillIdno"] != null)
            {
                this.FillInformationDetails(Convert.ToInt64(Request.QueryString["SbillIdno"].ToString()));
                lnkbtnPrint.Visible = true;
                ddlDateRange.Enabled = false;
            }
            else
            {
                Response.Redirect("SaleBill.aspx");
            }
        }
        protected void lnkbtnLastPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SaleBillDAL objSaleBillDAL = new SaleBillDAL();
                Int64 SbillHeadIdNo = objSaleBillDAL.GetSBillForLastPrint();
                objSaleBillDAL = null;
                if (SbillHeadIdNo > 0)
                {
                    Print(SbillHeadIdNo);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMsg", "CallPrint('print')", true);
                }
                else
                {
                    ShowMessageErr("Sale Bill not Create Yet.");
                }
            }
            catch (Exception exe)
            {
                throw (exe);
            }
        }
        protected void lnkbtnContnrDtl_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Convert.ToString(ddlFromCity.SelectedValue)) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue)) > 0)
            {
                ViewState["dtDivGrid"] = null;
                grdMIdetals.DataSource = null;
                lblSearchRecords.InnerText = "T. Record(s) : 0";
                grdMIdetals.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openGridDetail();", true);
                txtDivDateFrom.Focus();
            }
            else
            {
                ShowMessageErr("Please Select Location First."); return;
            }
        }
        protected void lnkbtnNewClick_Click(object sender, EventArgs e)
        {
            ClearItems();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dtotDis += GridDiscount ;
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Quantity"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lbltotalWeight.Text = dtotwght.ToString("N2");
                lblTotalDiscount.Text=dtotDis.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();

            }
        }
        #endregion


        private void StockCalc()
        {
            if ((string.IsNullOrEmpty(HideItemType.Value) ? 0 : Convert.ToInt32(HideItemType.Value)) == 2)
            {
                AccessoryStockRpt obj = new AccessoryStockRpt();
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                try
                {
                    DataTable dt = obj.SelectAccStockReport(ApplicationFunction.ConnectionString(), hidmindate.Value, DateTime.Now.Date.ToString("dd-MM-yyyy"), intyearid, Convert.ToString(ddlSerialNo.SelectedValue), 2, Convert.ToInt64(ddlFromCity.SelectedValue));
                    DataTable dtnew = obj.FetchAccStockReport(ApplicationFunction.ConnectionString());
                    if (dtnew != null && dtnew.Rows.Count > 0)
                    {
                        lblStockAss.Text = "Balance Stock : " + Convert.ToString(dtnew.Rows[0]["BalQty"]);
                        HideBalanceStock.Value = string.IsNullOrEmpty(Convert.ToString(dtnew.Rows[0]["BalQty"])) ? "0" : Convert.ToString(dtnew.Rows[0]["BalQty"]);
                    }
                }
                catch (Exception exe)
                {
                    lblStockAss.Text = "";
                }
            }
            else
            {
                lblStockAss.Text = "";
            }
        }

        private void CheckStock()
        {
            double Balance = string.IsNullOrEmpty(HideBalanceStock.Value) ? 0 : Convert.ToDouble(HideBalanceStock.Value);
            double EnterQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? 0 : Convert.ToDouble(txtQuantity.Text.Trim());
            if (Balance < EnterQty)
            {
                ShowMessageErr("Accessory Qty Should be less than or equal from balance Qty.");
                return; txtQuantity.Focus();
            }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(HideItemType.Value) ? 0 : Convert.ToInt32(HideItemType.Value)) > 0)
            {
                this.CheckStock();
            }
        }
    }
}
