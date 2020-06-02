using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Transactions;
using System.Data.OleDb;
using System.Text;

namespace WebTransport
{
    public partial class PurchaseBill : Pagebase
    {
        #region VariablesDeclarations..
        DataTable dtTemp = new DataTable();
        DataTable DtStockTemp = new DataTable();
        double iRate = 0; int intper = 0; double DivDiscount = 0; double DivOtherAmnt = 0; Double dtotalAmount = 0; double TotalTaxAmount = 0, GrdTotalVAT = 0, GrdTotalAddVAT = 0, GrdTotalDiscAmnt = 0, GrdTotalDiscOthAmnt = 0;
        double GrdTotalQuantity = 0, GrdTotalWeight = 0, GrdTotalAmount = 0, GrdTotalRate = 0;
        double ItemVAT = 0, ItemAdditVAT = 0;
        double Rate = 0, SGST = 0, CGST = 0, IGST = 0, Cess = 0;

        #endregion

        #region Page Load Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                txtBillDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBillNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtPrefixNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                this.BindDateRange();
                this.BindDropdown();
                this.BindTruck();
                this.BindPetrolPump();
                this.BindTyresize();
                this.BindItems(Convert.ToInt32(ddlPurchaseType.SelectedValue));
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                BindGridT();
                SetDate();

                if (Request.QueryString["PB"] != null)
                {
                    this.FillInformationDetails(Convert.ToInt64(Request.QueryString["PB"].ToString()));
                    lnkbtnPrint.Visible = true;
                    ddlDateRange.Enabled = false;
                    lnkExcelPop.Visible = false;
                    FileUpload.Enabled = false;
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                ddlDateRange.Focus();
            }

        }
        #endregion

        #region Functions..
        private bool PostIntoAccounts(DataTable dt, Int64 intDocIdno, string strDocType, double dblRndOff, Int32 intCompIdno, Int32 intUserIdno, Int32 intUserType, Int32 intVchrForIdno)
        {
            #region Variables Declaration...

            Int64 intVchrIdno = 0;
            Int64 intValue = 0;
            double dblOtherAmnt = Convert.ToDouble(string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? "0" : txtOtherCharges.Text.Trim());
            double dblDiscAmnt = Convert.ToDouble(string.IsNullOrEmpty(Request.Form[txtDiscount.UniqueID].Trim()) ? "0" : Request.Form[txtDiscount.UniqueID].Trim());
            double dblVatCST = 0;
            double dblNetAmnt = 0;
            Int64 RateType = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double Amount = 0;
                RateType = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Rate_TypeIdno"])) ? 0 : Convert.ToInt64(dt.Rows[i]["Rate_TypeIdno"]);
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

                dblVatCST += Convert.ToDouble(dt.Rows[i]["Vat"]);
            }
            PurchaseBillDAL obj1 = new PurchaseBillDAL();
            tblFleetAcntLink objAcntLink = obj1.SelectAcntLink();
            if (objAcntLink != null)
            {
                if (objAcntLink.CPur_Idno == 0 || objAcntLink.VPur_Idno == 0 || objAcntLink.Vat_Idno == 0 || objAcntLink.CST_Idno == 0 || objAcntLink.Disc_Idno == 0 || objAcntLink.Other_Idno == 0 || objAcntLink.Cash_Idno == 0)
                {
                    ShowMessageErr("Please Define Account Links!");
                    return false;
                }
            }
            Int32 intTaxType = (ddlPurchaseType.SelectedValue == "1" ? 1 : (ddlPurchaseType.SelectedValue == "2" ? 2 : 3));
            Int32 intBillType = rdoCredit.Checked == true ? 1 : 2;
            Int64 intPartyIdno = Convert.ToInt64((intBillType == 1) ? ((ddlSender.SelectedIndex == 0) ? 0 : Convert.ToInt64(ddlSender.SelectedValue)) : ((objAcntLink.Cash_Idno == 0) ? 0 : Convert.ToInt64(objAcntLink.Cash_Idno)));

            Int64 intDocNo = 0;
            if (Request.QueryString["PB"] == null)
            {
                PurchaseBillDAL objPurchaseBillDAL = new PurchaseBillDAL();
                intDocNo = objPurchaseBillDAL.SelectPurNoByIdno(intDocIdno);
                objPurchaseBillDAL = null;
            }
            else
            {
                intDocNo = Convert.ToInt64(txtBillNo.Text.Trim());
            }

            DateTime? dtPBillDate = null;
            DateTime? dtBankDate = null;

            clsAccountPosting objclsAccountPosting = new clsAccountPosting();


            #endregion

            #region Posting Start...

            if (Request.QueryString["PB"] == null)
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
                "Purchase Bill No: " + Convert.ToString(intDocNo) + " Purchase Bill Date: " + txtBillDate.Text.Trim(),
                true,
                0,
                strDocType,
                0,
                0,
                0,
                dtPBillDate,
                0,
                0,
                Convert.ToInt32(ddlDateRange.SelectedValue),
                Convert.ToInt32(base.CompId), intUserIdno);
                if (intValue > 0)
                {
                    intVchrIdno = intValue;

                    #region Party Account Posting...
                    intValue = 0; double TotAmnt = 0;

                    TotAmnt = Convert.ToDouble(dblNetAmnt + dblOtherAmnt);
                    intValue = objclsAccountPosting.InsertInVchrDetl(intVchrIdno, intPartyIdno, "", TotAmnt, Convert.ToByte(1), Convert.ToByte(0), "", true, dtBankDate, "", Convert.ToInt32(base.CompId));

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
                        if (ddlPurchaseType.SelectedValue == "1")
                        {
                            VatAcnt = Convert.ToInt64(objAcntLink.CST_Idno);
                        }
                        else
                        {
                            VatAcnt = Convert.ToInt64(objAcntLink.Vat_Idno);
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

                    #region Purchase Account Posting...

                    if (intValue > 0)
                    {
                        if (ddlPurchaseType.SelectedValue == "1")
                        {
                            #region CST ...
                            if (Convert.ToDouble(TotAmnt) > 0)
                            {
                                intValue = 0;
                                intValue = objclsAccountPosting.InsertInVchrDetl(
                                intVchrIdno,
                                Convert.ToInt64(objAcntLink.CPur_Idno),
                                "",
                                Math.Round(Convert.ToDouble(dblNetAmnt), 2),
                                    //- Convert.ToDouble(Convert.ToString(txtVatoutpt.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtVatoutpt.Text.Trim())),
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
                                Convert.ToInt64(objAcntLink.VPur_Idno),
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
                            "Purchase Bill No: " + Convert.ToString(intDocNo) + " Purchase Bill Date: " + txtBillDate.Text.Trim(),
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            0,
                            dtPBillDate,
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
                            "Purchase Bill No: " + Convert.ToString(intDocNo) + " Purchase Bill Date: " + txtBillDate.Text.Trim(),
                            true,
                            0,
                            strDocType,
                            0,
                            0,
                            0,
                            dtPBillDate,
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

        private void BindCity()
        {
            PurchaseBillDAL obj = new PurchaseBillDAL();
            var FrmCity = obj.BindFromCity();
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTyresize()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var tyresize = obj.Bindtyresize();
            obj = null;
            ddltyresize.DataSource = tyresize;
            ddltyresize.DataTextField = "TyreSize";
            ddltyresize.DataValueField = "TyreSize_Idno";
            ddltyresize.DataBind();
            ddltyresize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserId)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserId);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "CityIdno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;

            ddlDateRange.Focus();
        }

        private void BindItems(int ItemType)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            if (ItemType != 3)
            {
                var itemname = obj.BindPurchaseItemNameNew();
                ddlItemName.DataSource = itemname;
                ddlItemName.DataTextField = "Item_Name";
                ddlItemName.DataValueField = "Item_idno";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                ddlLorry.Enabled = false;
                rvfLorry.Enabled = false;
            }
            else
            {
                var itemname = obj.BindPurchaseItemName2(3);
                ddlItemName.DataSource = itemname;
                ddlItemName.DataTextField = "Item_Name";
                ddlItemName.DataValueField = "Item_idno";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                ddlLorry.Enabled = true;
                rvfLorry.Enabled = true;
            }

        }

        private void BindPetrolPump()
        {
            PurchaseBillDAL objBill = new PurchaseBillDAL();
            int PType = 2;
            if (ddlPurchaseType.SelectedValue == "3")
            {
                PType = 10;
                var obj = objBill.BindSenderForPurchaseBill1(PType, ApplicationFunction.ConnectionString());
                ddlSender.DataSource = obj;
                ddlSender.DataTextField = "Acnt Name";
                ddlSender.DataValueField = "Acnt Idno";
                ddlSender.DataBind();
                ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                ddlRateType.SelectedValue = "1"; ddlRateType.Enabled = false; txtweight.Enabled = false;
            }
            else
            {
                DataTable dt = objBill.BindSenderForPurchaseBill1(PType, ApplicationFunction.ConnectionString());
                ddlSender.DataSource = dt;
                ddlSender.DataTextField = "Acnt Name";
                ddlSender.DataValueField = "Acnt Idno";
                ddlSender.DataBind();
                ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                ddlRateType.Enabled = true; txtweight.Enabled = true;
            }
        }

        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var UnitName = obj.BindUnitName();
            var ToCity = obj.BindLocFrom();
            obj = null;

            ddlunitname.DataSource = UnitName;
            ddlunitname.DataTextField = "UOM_Name";
            ddlunitname.DataValueField = "UOM_idno";
            ddlunitname.DataBind();
            ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.Visible = true;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
                else
                {
                    grdMain.Visible = false;
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                grdMain.Visible = false;
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Item_Name", "String",
                "Item_Idno", "String",
                "Unit_Name", "String",
                "Unit_Idno", "String",
                "Rate_Type", "String",
                "Rate_TypeIdno", "String",
                "Quantity", "String",
                "Weight", "String",
                "Rate", "String",
                "Amount", "String",
                "IGrp_Idno", "String",
                "Vat", "String",
                "Addit_Vat", "String",
                "Item_type", "String",
                "TaxRate", "String",
                "DivDiscType", "String",
                "strDivDiscType", "String",
                "DivDiscValue", "String",
                "DivDiscAmnt", "String",
                "DivDiscOthAmnt", "String",
                "Tyresize", "String",
                "Tyresize_Idno", "String",
                 "SGST_Per", "String",
                 "CGST_Per", "String",
                 "IGST_Per", "String",
                 "SGST_Amt", "String",
                 "CGST_Amt", "String",
                 "IGST_Amt", "String",
                 "Gst_Idno", "String"
                );
            return dttemp;
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void CalculateEdit()
        {
            intper = string.IsNullOrEmpty(Convert.ToString(ddlDivPer.SelectedValue)) ? 1 : Convert.ToInt32(ddlDivPer.SelectedValue);
            if ((ddlRateType.SelectedIndex) > 0)
            {
                if (ddlRateType.SelectedIndex == 1)
                {
                    iRate = Convert.ToDouble(txtrate.Text);
                    DivOtherAmnt = string.IsNullOrEmpty(Convert.ToString(txtDivOtherAmnt.Text.Trim())) ? 1 : Convert.ToDouble(txtDivOtherAmnt.Text.Trim());
                    if (intper == 1) { DivDiscount = ((iRate * (string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text)) ? 0 : Convert.ToDouble(txtQuantity.Text))) * ((string.IsNullOrEmpty(Convert.ToString(txtDivDiscAmnt.Text.Trim())) ? 0.00 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim())) / 100)); }
                    else if (intper == 2) { DivDiscount = ((string.IsNullOrEmpty(Convert.ToString(txtDivDiscAmnt.Text.Trim())) ? 0.00 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim()))); }
                    if (txtQuantity.Text.Trim() != "")
                    {
                        double Tax = Convert.ToDouble(((((iRate * Convert.ToDouble(txtQuantity.Text)) - DivDiscount) + DivOtherAmnt) * Convert.ToDouble(string.IsNullOrEmpty(HidTaxRate.Value) ? "0" : HidTaxRate.Value)) / 100);
                        HidTax.Value = (Tax).ToString("N2");
                        dtotalAmount = Convert.ToDouble((((iRate * Convert.ToDouble(txtQuantity.Text)) + Tax) - DivDiscount) + DivOtherAmnt);
                    }
                }
                else
                {
                    iRate = Convert.ToDouble(txtrate.Text);
                    DivOtherAmnt = string.IsNullOrEmpty(Convert.ToString(txtDivOtherAmnt.Text.Trim())) ? 1 : Convert.ToDouble(txtDivOtherAmnt.Text.Trim());
                    if (intper == 1) { DivDiscount = ((iRate * (string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text)) ? 0 : Convert.ToDouble(txtQuantity.Text))) * ((string.IsNullOrEmpty(Convert.ToString(txtDivDiscAmnt.Text.Trim())) ? 0.00 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim())) / 100)); } else if (intper == 2) { DivDiscount = ((string.IsNullOrEmpty(Convert.ToString(txtDivDiscAmnt.Text.Trim())) ? 0.00 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim()))); }
                    if (txtweight.Text.Trim() != "")
                    {
                        double Tax = Convert.ToDouble(((((iRate * Convert.ToDouble(txtweight.Text)) - DivDiscount) + DivOtherAmnt) * Convert.ToDouble(HidTaxRate.Value)) / 100);
                        HidTax.Value = (Tax).ToString("N2");
                        dtotalAmount = Convert.ToDouble((((iRate * Convert.ToDouble(txtweight.Text)) + Tax) - DivDiscount) + DivOtherAmnt);
                    }
                }
            }
        }

        private void ClearItems()
        {
            lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0; ddlunitname.SelectedIndex = ddlRateType.SelectedIndex = 0;ddltyresize.SelectedIndex = 0; txtDivDiscAmnt.Text = "0.00"; txtDivOtherAmnt.Text = "0.00";
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
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
                { txtBillDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy"); }
                else { txtBillDate.Text = hidmindate.Value; }
            }
        }

        private void NetAmountCalculate()
        {
            double NetAmnt = 0;
            NetAmnt = string.IsNullOrEmpty(txtNetAmnt.Text) ? 0.00 : Convert.ToDouble(txtNetAmnt.Text);
            double netAmount = NetAmnt;

            double RoudedValue = Math.Round(netAmount);
            txtRoundOff.Text = (Convert.ToDouble(RoudedValue) - netAmount).ToString("N2");
            txtNetAmnt.Text = RoudedValue.ToString("N2");
        }

        private void clearAllControl()
        {
            ddlPurchaseType.Enabled = true;
            rdoCredit.Enabled = rdoCash.Enabled = true;
            ddlFromCity.Enabled = true;
            ddlSender.SelectedValue = ddlItemName.SelectedValue = ddlDiscountType.SelectedValue = ddlunitname.SelectedValue = ddltyresize.SelectedValue = ddlRateType.SelectedValue = "0";
            txtPrefixNo.Text = txtBillNo.Text = TxtRemark.Text = txtQuantity.Text = txtweight.Text = txtrate.Text = txtDiscount.Text = txtDiscountPer.Text = txtOtherCharges.Text = txtRoundOff.Text = txtTotalAmount.Text = txtNetAmnt.Text = "";
            HidPBID.Value = string.Empty;
            dtTemp = null; ViewState["dt"] = null; this.BindGridT();
        }

        private bool CheckBillExists()
        {
            PurchaseBillDAL Obj = new PurchaseBillDAL();
            string prefNo = string.IsNullOrEmpty(Convert.ToString(txtPrefixNo.Text.Trim())) ? "" : Convert.ToString(txtPrefixNo.Text.Trim());
            Int64 BillNo = string.IsNullOrEmpty(Convert.ToString(txtBillNo.Text.Trim())) ? 0 : Convert.ToInt64(txtBillNo.Text.Trim());
            DataTable dt = Obj.CheckBillNoExists(ddlFromCity.SelectedValue, ddlDateRange.SelectedValue, prefNo, BillNo, ApplicationFunction.ConnectionString());
            if (dt != null && (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["PbillNo"].ToString())) ? 0 : Convert.ToInt64(dt.Rows[0]["PbillNo"].ToString())) > 0) { return false; } else { return true; }
        }

        private void FillInformationDetails(Int64 PBillHead_Idno)
        {
            PurchaseBillDAL Obj = new PurchaseBillDAL();
            tblPBillHead ObjHead = Obj.Select_PurchaseBillHead(PBillHead_Idno);
            var ObjHeadDetail = Obj.Select_PurchaseBillDetail(PBillHead_Idno);

            if (ObjHead != null)
            {
                ViewState["PBillHead_Idno"] = PBillHead_Idno;
                ddlDateRange.SelectedValue = Convert.ToString(ObjHead.Year_Idno);
                txtPrefixNo.Text = ObjHead.Prefix_No.ToString();
                txtBillNo.Text = ObjHead.PBillHead_No.ToString();
                txtBillDate.Text = string.IsNullOrEmpty(ObjHead.PBillHead_Date.ToString()) ? "" : Convert.ToDateTime(ObjHead.PBillHead_Date).ToString("dd-MM-yyyy");
                ddlFromCity.SelectedValue = ObjHead.Loc_Idno.ToString();
                HidPBID.Value = Convert.ToString(ObjHead.PBillHead_Idno);


                TxtRemark.Text = ObjHead.Remark.ToString();

                Int32 BillType = Convert.ToInt32(ObjHead.Bill_Type);
                ddlPurchaseType.SelectedValue = Convert.ToString(ObjHead.Pur_Type);
                ddlPurchaseType.Enabled = false;
                ddlPurchaseType_SelectedIndexChanged(null, null);
                ddlSender.SelectedValue = ObjHead.Prty_Idno.ToString();
                ddlLorry.SelectedValue = Convert.ToString(ObjHead.LorryIdno);
                if (BillType == 1) { rdoCredit.Checked = true; rdoCash.Checked = false; } else if (BillType == 2) { rdoCash.Checked = true; rdoCredit.Checked = false; }
                rdoCredit.Enabled = rdoCash.Enabled = false;
                ddlFromCity.Enabled = false;
                dtTemp = CreateDt();
                double Amnt = 0;
                for (int counter = 0; counter < ObjHeadDetail.Count; counter++)
                {
                    string strItem_Idno = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Item_Idno"));
                    string strItem_Name = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Item_Name"));
                    string strTyreSize_Idno = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "TyresizeIdno"));
                    string strTyreSize = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "TyreSize"));
                    string strUnit_Idno = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Unit_Idno"));
                    string strUnit_Name = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "UOM_Name"));
                    string strRate_Type = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Rate_Type"));
                    string strRate_Name = string.Empty;
                    if (strRate_Type == "1") { strRate_Name = "Rate"; } else if (strRate_Type == "2") { strRate_Name = "Weight"; }
                    string strQty = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Qty"));
                    string strWeight = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Tot_Weght"));
                    string strRate = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Item_Rate"));
                    string strAmount = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Amount"));
                    string strIGrp_Idno = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "IGrp_Idno"));
                    string Vat = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Item_Tax"))) == true ? "0" : (Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Item_Tax")));
                    string Addit_Vat = "0";
                    string Item_type = Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "ItemType"));
                    string TaxRate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Tax_Rate"))) == true ? "0" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Tax_Rate"));

                    string strDivDiscType = string.Empty;
                    int DivDiscType = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Type"))) == true ? 1 : Convert.ToInt32(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Type"));
                    if (DivDiscType == 1) { strDivDiscType = "%"; } else if (DivDiscType == 2) { strDivDiscType = "Amnt"; }
                    string DivDiscValue = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Value"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Value"));
                    string DivDiscAmnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Amnt"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Amnt"));
                    string DivDiscOthAmnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Other_Amnt"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Other_Amnt"));
                    string SGST_Per = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "SGST_Per"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "SGST_Per"));
                    string CGST_Per = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "CGST_Per"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "CGST_Per"));
                    string IGST_Per = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "IGST_Per"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "IGST_Per"));
                    string SGST_Amnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "SGST_Amt"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "SGST_Amt"));
                    string CGST_Amnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "CGST_Amt"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "CGST_Amt"));
                    string IGST_Amnt = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "IGST_Amt"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "IGST_Amt"));
                    Amnt = Amnt + (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Amnt"))) == true ? 0.00 : Convert.ToDouble(DataBinder.Eval(ObjHeadDetail[counter], "Disc_Amnt")));
                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItem_Name, strItem_Idno, strUnit_Name, strUnit_Idno, strRate_Name, strRate_Type, strQty, strWeight, strRate, strAmount, strIGrp_Idno, Vat, Addit_Vat, Item_type, TaxRate, DivDiscType, strDivDiscType, DivDiscValue, DivDiscAmnt, DivDiscOthAmnt, strTyreSize, strTyreSize_Idno, SGST_Per, CGST_Per, IGST_Per, SGST_Amnt, CGST_Amnt, IGST_Amnt);
                }

                if (Amnt > 0)
                {
                    ddlDiscountType.Enabled = false; txtDiscountPer.Text = "0.00"; txtDiscountPer.Enabled = false;
                }
                else
                {
                    ddlDiscountType.Enabled = true; txtDiscountPer.Enabled = true;
                }
                ViewState["dt"] = dtTemp;
                BindGridT();

                txtTotalAmount.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Tot_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Tot_Amnt);
                txtOtherCharges.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Other_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Other_Amnt);
                txtRoundOff.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.RndOff_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.RndOff_Amnt);
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Net_Amnt)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Net_Amnt);
                ddlDiscountType.SelectedValue = ObjHead.Disc_type.ToString();
                txtDiscountPer.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Discount)) ? "0.00" : String.Format("{0:0,0.00}", ObjHead.Discount);

                if (ddlDiscountType.SelectedValue == "1") { txtDiscount.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Disc_Amnt)) ? "0" : String.Format("{0:0,0.00}", ObjHead.Disc_Amnt); }
                else if (ddlDiscountType.SelectedValue == "2") { txtDiscountPer.Text = string.IsNullOrEmpty(Convert.ToString(ObjHead.Disc_Amnt)) ? "0" : String.Format("{0:0,0.00}", (ObjHead.Disc_Amnt)); }
                this.ShowStock();
                PrintGRPrep(PBillHead_Idno);
            }
        }

        double rptTotalAmount = 0, rptTotalWeight = 0, rptTotalQty = 0;

        private void PrintGRPrep(Int64 PBillHead_Idno)
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spPurBill] @ACTION='SelectPrint',@PBillHead_Idno='" + PBillHead_Idno + "'");
            dsReport.Tables[0].TableName = "BillPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["PrefixNo"]) + " " + Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["BillNo"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["BillPrint"].Rows[0]["PBillHead_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["Location"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["PrtyName"]);

                if (Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["Remark"]) != "")
                    lblremark.Text = "Remark:<br/>" + Convert.ToString(dsReport.Tables["BillPrint"].Rows[0]["Remark"]);

                // lblTotalAmount.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["Tot_Amnt"]));

                if (dsReport.Tables["BillPrint"].Rows[0]["Other_Amnt"].ToString() != "0")
                    lblOtherChargesValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["Other_Amnt"]));
                else
                    trOtherchrg.Visible = false;

                if (dsReport.Tables["BillPrint"].Rows[0]["Disc_Amnt"].ToString() != "0")
                    lblDiscountValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["Disc_Amnt"]));
                else
                    trOtherchrgDiscount.Visible = false;

                lblRoundoffValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["RndOff_Amnt"]));

                lblNetAmountValue.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["BillPrint"].Rows[0]["Net_Amnt"]));

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();

            }
        }

        #endregion

        #region ButtonEvents ....
        protected void lnkbtnRefreshParty_OnClick(object sender, EventArgs e)
        {
            try
            {
                PurchaseBillDAL objBill = new PurchaseBillDAL();
                int PType = 2;
                if (ddlPurchaseType.SelectedValue == "3")
                {
                    var obj = objBill.BindSenderFromPetrolPumpMaster();
                    ddlSender.DataSource = obj;
                    ddlSender.DataTextField = "PPump_Name";
                    ddlSender.DataValueField = "PPump_Idno";
                    ddlSender.DataBind();
                    ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    DataTable dt = objBill.BindSenderForPurchaseBill1(PType, ApplicationFunction.ConnectionString());
                    ddlSender.DataSource = dt;
                    ddlSender.DataTextField = "Acnt Name";
                    ddlSender.DataValueField = "Acnt Idno";
                    ddlSender.DataBind();
                    ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }

                lnkbtnRefreshParty.Focus();
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkbtnDriverRefresh_OnClick(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var ToCity = obj.BindLocFrom();
                obj = null;
                ddlFromCity.DataSource = ToCity;
                ddlFromCity.DataTextField = "city_name";
                ddlFromCity.DataValueField = "city_idno";
                ddlFromCity.DataBind();
                ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                lnkbtnDriverRefresh.Focus();
            }
            catch (Exception Ex) { }
        }
        protected void lnkbtnSubmitClick_OnClick(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item!"); ddlItemName.Focus(); return; }
            if (ddlunitname.SelectedIndex == 0) { ShowMessageErr("Please select Unit!"); ddlunitname.Focus(); return; }
            if (ddlSender.SelectedIndex == 0) { ShowMessageErr("Please Select Party!"); ddlSender.Focus(); return; }
            PurchaseBillDAL objDAl = new PurchaseBillDAL();
            Int32 itemType = objDAl.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
            //if (string.IsNullOrEmpty(txtQuantity.Text)|| txtQuantity.Text == "0.00") { ShowMessageErr("Please Enter Qty!"); txtQuantity.Focus(); return; }
            //else if (ddlRateType.SelectedValue == "2")
            //{
            //    if ((string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim())) <= 0) { ShowMessageErr("Weight should be greater than zero!"); txtweight.Focus(); return; }
            //}
            //else if (ddlRateType.SelectedValue == "1")
            //{
            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero!"); txtQuantity.Focus(); return; }
           // }
            if (txtrate.Text == "" || Convert.ToDouble(txtrate.Text) <= 0) { ShowMessageErr("Rate should be greater than zero."); txtrate.Focus(); return; }
            Double DISCOUNTAMNT = string.IsNullOrEmpty(txtDivDiscAmnt.Text.Trim()) ? 0 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim());
            if (ddlDivPer.SelectedValue == "1") { if (DISCOUNTAMNT > 100) { this.ShowMessageErr("Discount value cannot greater than 100!"); return; } }

            if (GetCompStateID() == GetPartyStateID(Convert.ToInt64(ddlSender.SelectedValue)))
            {
                hdnGSTFlag.Value = "1";
                hdnStateID.Value = Convert.ToString(GetPartyStateID(Convert.ToInt64(ddlSender.SelectedValue)));
            }

            else
            {
                hdnGSTFlag.Value = "2";
                hdnStateID.Value = Convert.ToString(GetPartyStateID(Convert.ToInt64(ddlSender.SelectedValue)));
            }
            SetGSTPercent();
            CalculateWithEdit();
            // CalculateEdit();
            string TotalAmount = string.Empty;

            dtTemp = (DataTable)ViewState["dt"];

            if (ViewState["ID"] != null)
            {
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(ViewState["ID"].ToString()))
                    {
                        dtrow["Item_Name"] = ddlItemName.SelectedItem.Text;
                        dtrow["Item_Idno"] = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                        dtrow["Unit_Name"] = ddlunitname.SelectedItem.Text;
                        dtrow["Unit_Idno"] = string.IsNullOrEmpty(ddlunitname.SelectedValue) ? "0" : (ddlunitname.SelectedValue);
                        dtrow["Rate_Type"] = "";//ddlRateType.SelectedItem.Text;
                        dtrow["Rate_TypeIdno"] = 0;//ddlRateType.SelectedValue;
                        dtrow["Quantity"] = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim()); //iqty += Convert.ToDouble(txtQuantity.Text.Trim());
                        dtrow["Weight"] = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                        dtrow["Rate"] = txtrate.Text.Trim();
                        dtrow["Amount"] = hdnAmount.Value;
                        dtrow["Vat"] = string.IsNullOrEmpty(HidTax.Value) == true ? "0" : Convert.ToString(HidTax.Value);
                        dtrow["Addit_Vat"] = ItemAdditVAT;
                        dtrow["Item_type"] = "";
                        dtrow["TaxRate"] = string.IsNullOrEmpty(HidTaxRate.Value) == true ? "0" : Convert.ToString(HidTaxRate.Value);
                        dtrow["DivDiscType"] = string.IsNullOrEmpty(ddlDivPer.SelectedValue) ? 1 : Convert.ToInt32(ddlDivPer.SelectedValue);
                        if (dtrow["DivDiscType"] == "1") { dtrow["strDivDiscType"] = "%"; } else if (dtrow["DivDiscType"] == "2") { dtrow["strDivDiscType"] = "Amnt"; }
                        dtrow["DivDiscValue"] = string.IsNullOrEmpty(txtDivDiscAmnt.Text.Trim()) ? "0" : Convert.ToString(txtDivDiscAmnt.Text.Trim());
                        dtrow["DivDiscAmnt"] = hidDiscValue.Value;
                        dtrow["DivDiscOthAmnt"] = string.IsNullOrEmpty(txtDivOtherAmnt.Text.Trim()) ? "0.00" : Convert.ToString(txtDivOtherAmnt.Text.Trim());
                        if (itemType == 1)
                        {
                            dtrow["Tyresize"] = ddltyresize.SelectedItem.Text;
                            dtrow["Tyresize_Idno"] = string.IsNullOrEmpty(ddltyresize.SelectedValue) ? "0" : (ddltyresize.SelectedValue);
                        }
                        else
                        {
                            dtrow["Tyresize"] = "";
                            dtrow["Tyresize_Idno"] = 0;
                        }
                        dtrow["SGST_Per"] = string.IsNullOrEmpty(Convert.ToString(hdnSGSTPer.Value)) ? "0" : Convert.ToString(hdnSGSTPer.Value);
                        dtrow["CGST_Per"] = string.IsNullOrEmpty(Convert.ToString(hdnCGSTPer.Value)) ? "0" : Convert.ToString(hdnCGSTPer.Value);
                        dtrow["IGST_Per"] = string.IsNullOrEmpty(Convert.ToString(hdnIGSTPer.Value)) ? "0" : Convert.ToString(hdnIGSTPer.Value);
                        dtrow["SGST_Amt"] = string.IsNullOrEmpty(Convert.ToString(hdnSGSTAmt.Value)) ? "0" : Convert.ToString(hdnSGSTAmt.Value);
                        dtrow["CGST_Amt"] = string.IsNullOrEmpty(Convert.ToString(hdnCGSTAmt.Value)) ? "0" : Convert.ToString(hdnCGSTAmt.Value);
                        dtrow["IGST_Amt"] = string.IsNullOrEmpty(Convert.ToString(hdnIGSTAmt.Value)) ? "0" : Convert.ToString(hdnIGSTAmt.Value);
                        dtrow["GST_Idno"] = string.IsNullOrEmpty(Convert.ToString(hdnGSTFlag.Value)) ? "0" : Convert.ToString(hdnGSTFlag.Value);

                    }
                }
                ViewState["ID"] = null;
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        if (Convert.ToInt32(row["Item_Idno"]) == Convert.ToInt32(ddlItemName.SelectedValue))
                        {
                            string msg = "Item Already Selected!";
                            ddlItemName.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                            return;
                        }
                    }
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemNameId = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                string strUnitName = ddlunitname.SelectedItem.Text.Trim();
                string strUnitNameId = string.IsNullOrEmpty(ddlunitname.SelectedValue) ? "0" : (ddlunitname.SelectedValue);
                string strRateType = "";// ddlRateType.SelectedItem.Text.Trim();
                string strRateTypeIdno = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : (ddlRateType.SelectedValue);
                string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                string strIGrp_Idno = string.IsNullOrEmpty(ViewState["IGrp_Idno"].ToString()) ? "0" : ViewState["IGrp_Idno"].ToString();
                string Vat = string.IsNullOrEmpty(HidTax.Value) == true ? "0" : Convert.ToString(HidTax.Value);
                string Addit_Vat = "0";
                string Item_type = "";
                string TaxRate = string.IsNullOrEmpty(HidTaxRate.Value) == true ? "0" : Convert.ToString(HidTaxRate.Value);
                string DivDiscType = string.IsNullOrEmpty(ddlDivPer.SelectedValue) == true ? "0" : Convert.ToString(ddlDivPer.SelectedValue);
                string strDivDiscType = string.Empty;
                if (ddlDivPer.SelectedValue == "1") { strDivDiscType = "%"; } else { strDivDiscType = "Amnt"; }
                string DivDiscValue = string.IsNullOrEmpty(txtDivDiscAmnt.Text.Trim()) == true ? "0.00" : Convert.ToString(txtDivDiscAmnt.Text.Trim());
                string DivDiscAmnt = hidDiscValue.Value;
                string DivDiscOthAmnt = string.IsNullOrEmpty(txtDivOtherAmnt.Text.Trim()) == true ? "0.00" : Convert.ToString(txtDivOtherAmnt.Text.Trim());
                TotalAmount = hdnAmount.Value;
                string strTyreSize = string.Empty;
                string strTyreSizeId = "0";
                if (itemType == 1)
                {
                    strTyreSize = ddltyresize.SelectedItem.Text.Trim();
                    strTyreSizeId = string.IsNullOrEmpty(ddltyresize.SelectedValue) ? "0" : (ddltyresize.SelectedValue);
                }

                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, TotalAmount, strIGrp_Idno, Vat, Addit_Vat, Item_type, TaxRate, DivDiscType, strDivDiscType, DivDiscValue, DivDiscAmnt, DivDiscOthAmnt, strTyreSize, strTyreSizeId, hdnSGSTPer.Value,
                         hdnCGSTPer.Value,
                         hdnIGSTPer.Value,
                         hdnSGSTAmt.Value,
                         hdnCGSTAmt.Value,
                         hdnIGSTAmt.Value,
                         hdnGSTFlag.Value);
            }
            ViewState["dt"] = dtTemp;

            this.BindGridT();
            ddlItemName.Focus();
            ClearItems();
        }
        protected void lnkbtnNewClick_OnClick(object sender, EventArgs e)
        {
            ClearItems();
        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseBill.aspx");
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            //if (Request.QueryString["PB"] != null)
            //{
            //    this.FillInformationDetails(Convert.ToInt64(Request.QueryString["PB"].ToString()));
            //    lnkbtnPrint.Visible = true;
            //    ddlDateRange.Enabled = false;
            //    lnkExcelPop.Visible = false;
            //    FileUpload.Enabled = false;
            //}
            //else
            //{
            //    Response.Redirect("PurchaseBill.aspx");
            //}
            if (string.IsNullOrEmpty(HidPBID.Value) == true)
            {
                Response.Redirect("PurchaseBill.aspx");
            }
            else
            {
                this.FillInformationDetails(Convert.ToInt32(HidPBID.Value) == 0 ? 0 : Convert.ToInt32(HidPBID.Value));
            }
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string msg = "";
                Int64 varForSave = 0;
                DateTime? BillDate = null;
                BillDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text));
                int PurchaseType = Convert.ToInt32(ddlPurchaseType.SelectedValue);
                int BillType = 0; if (rdoCredit.Checked) { BillType = 1; } else if (rdoCash.Checked) { BillType = 2; }
                DateTime DateAdded = System.DateTime.Now;
                DataTable dtDetail = (DataTable)ViewState["dt"];

                double DiscountAmnt = 0; double Roundoff = 0; double NetAmnt = 0; double Discount = 0;
                DiscountAmnt = string.IsNullOrEmpty(Request.Form[txtDiscount.UniqueID]) ? 0.00 : Convert.ToDouble(Request.Form[txtDiscount.UniqueID].ToString());
                Roundoff = string.IsNullOrEmpty(Request.Form[txtRoundOff.UniqueID]) ? 0.00 : Convert.ToDouble(Request.Form[txtRoundOff.UniqueID].ToString());
                NetAmnt = string.IsNullOrEmpty(Request.Form[txtNetAmnt.UniqueID]) ? 0.00 : Convert.ToDouble(Request.Form[txtNetAmnt.UniqueID].ToString());
                Discount = string.IsNullOrEmpty(txtDiscountPer.Text.Trim()) ? 0.00 : Convert.ToDouble(txtDiscountPer.Text.Trim());
                PurchaseBillDAL obj = new PurchaseBillDAL();
                if (dtDetail.Rows.Count > 0)
                {
                    if (Convert.ToDouble(NetAmnt) > 0)
                    {
                       // using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                        {
                            if (ViewState["PBillHead_Idno"] != null)
                            {
                                varForSave = obj.Update(Convert.ToInt64(ViewState["PBillHead_Idno"].ToString()), Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64(ddlFromCity.SelectedValue), BillDate, txtPrefixNo.Text.Trim(), Convert.ToInt64(txtBillNo.Text), PurchaseType, Convert.ToInt64(ddlSender.SelectedValue), BillType, TxtRemark.Text.Trim(), Convert.ToDouble(txtTotalAmount.Text.Trim()), Convert.ToInt32(ddlDiscountType.SelectedValue), DiscountAmnt, Convert.ToDouble(txtOtherCharges.Text.Trim()), Roundoff, NetAmnt, DateAdded, dtDetail, Convert.ToInt64(ddlLorry.SelectedValue), Discount);
                                if (varForSave > 0)
                                {
                                    string strRoundOff = txtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                                    if (this.PostIntoAccounts(dtDetail, varForSave, "PB", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), base.CompId, base.UserIdno, 0, 0) == true)
                                    {

                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["PBillHead_Idno"])) == false)
                                        {
                                         //   Tran.Complete();
                                            lnkbtnNew.Visible = false; lnkbtnPrint.Visible = false;
                                            msg = "Record(s) Updated Successfully.";
                                        }
                                        this.clearAllControl();
                                        this.ShowMessage(msg);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["PBillHead_Idno"])) == false)
                                        {
                                          //  Tran.Dispose();
                                            msg = "Record(s) Not Updated!";
                                        }
                                        ShowMessageErr(msg);
                                    }
                                }
                                else
                                {
                                  //  Tran.Dispose();
                                    this.ShowMessageErr("Record Not Updated!");
                                }
                            }
                            else
                            {
                                if (CheckBillExists() == false) { ShowMessageErr("Bill number already exist!"); return; }
                                varForSave = obj.Insert(Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64(ddlFromCity.SelectedValue), BillDate, txtPrefixNo.Text.Trim(), Convert.ToInt64(txtBillNo.Text), PurchaseType, Convert.ToInt64(ddlSender.SelectedValue), BillType, TxtRemark.Text.Trim(), Convert.ToDouble(txtTotalAmount.Text.Trim()), Convert.ToInt32(ddlDiscountType.SelectedValue), DiscountAmnt, Convert.ToDouble(txtOtherCharges.Text.Trim()), Roundoff, NetAmnt, DateAdded, dtDetail, Convert.ToInt64(ddlLorry.SelectedValue), Discount);
                                if (varForSave > 0)
                                {
                                    string strRoundOff = txtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                                    if (this.PostIntoAccounts(dtDetail, varForSave, "PB", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), base.CompId, base.UserIdno, 0, 0) == true)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(ViewState["PBillHead_Idno"])) == true)
                                        {
                                            msg = "Record(s) Saved Successfully.";
                                     //       Tran.Complete();
                                        }
                                        this.clearAllControl();
                                        this.ShowMessage(msg);
                                    }
                                    else
                                    {
                                    //    Tran.Dispose();
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
        #endregion

        #region Events...

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                PurchaseBillDAL obj = new PurchaseBillDAL();
                int itemType = obj.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
                if (itemType == 1)
                {
                    ddltyresize.Enabled = true;
                }
                else
                {
                    ddltyresize.Enabled = false;
                }
                 DataTable dt;
                if (ddlPurchaseType.SelectedValue == "3")
                {
                    dt = obj.GetEffectivePrice(Convert.ToInt64(ddlSender.SelectedValue), Convert.ToInt64(ddlItemName.SelectedValue), txtBillDate.Text, ApplicationFunction.ConnectionString());
                }
                else
                {
                  dt = obj.GetItemDetails(Convert.ToInt64(ddlItemName.SelectedValue), ApplicationFunction.ConnectionString());
                }
                if (dt.Rows.Count > 0)
                {
                    ViewState["IGrp_Idno"] = dt.Rows[0]["IGrp_Idno"].ToString();
                    txtrate.Text = dt.Rows[0]["Rate"].ToString();
                    ddlunitname.SelectedValue = dt.Rows[0]["Uom"].ToString();
                    if (Convert.ToInt32(ddlPurchaseType.SelectedValue) > 0)
                    {
                        if (Convert.ToInt32(ddlPurchaseType.SelectedValue) == 1)
                        {
                            HidTaxRate.Value = string.IsNullOrEmpty(dt.Rows[0]["CstRate"].ToString()) ? "0" : dt.Rows[0]["CstRate"].ToString();
                        }
                        else if (Convert.ToInt32(ddlPurchaseType.SelectedValue) == 2)
                        {
                            HidTaxRate.Value = string.IsNullOrEmpty(dt.Rows[0]["TaxRate"].ToString()) ? "0" : dt.Rows[0]["TaxRate"].ToString();
                        }
                        else
                        {
                            HidTaxRate.Value = "0";
                        }
                    }
                }
                else
                {
                    ViewState["IGrp_Idno"] = "0";
                }
                ddlItemName.Focus();
            }
            catch (Exception Ex) { }
        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        { SetDate(); ddlDateRange.Focus(); }

        protected void ddlFromCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                PurchaseBillDAL Obj = new PurchaseBillDAL();
                DataTable dt = Obj.GetPurchaseBillNumber(ddlFromCity.SelectedValue, ddlDateRange.SelectedValue, ApplicationFunction.ConnectionString());
                txtBillNo.Text = dt.Rows[0][0].ToString();
            }
            ddlFromCity.Focus();
        }

        //protected void txtOtherCharges_OnTextChanged(object sender, EventArgs e)
        //{ NetAmountCalculate(); }

        //protected void txtDiscount_OnTextChanged(object sender, EventArgs e)
        //{
        //    if (ddlDiscountType.SelectedIndex > 0)
        //    {
        //        if (ddlDiscountType.SelectedValue == "1")
        //        {
        //            ViewState["TotalAmountDiscount"] = Convert.ToDouble(string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? "0" : txtDiscount.Text.Trim());
        //        }

        //        NetAmountCalculate();
        //        txtDiscountPer.Text = "0.00";
        //        txtDiscountPer.Enabled = false; txtDiscount.Enabled = true;
        //    }
        //    txtDiscount.Focus();
        //}

        //protected void txtDiscountPer_OnTextChanged(object sender, EventArgs e)
        //{
        //    if (ddlDiscountType.SelectedIndex > 0)
        //    {
        //        if (ddlDiscountType.SelectedValue == "2")
        //        {
        //            if (Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPer.Text) ? "0" : txtDiscountPer.Text.Trim()) < 100)
        //            {
        //                if (string.IsNullOrEmpty(txtDiscountPer.Text) == false)
        //                {
        //                    ViewState["TotalAmountDiscount"] = Convert.ToDouble(string.IsNullOrEmpty(txtTotalAmount.Text.Trim()) ? "0" : txtTotalAmount.Text.Trim()) * Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPer.Text.Trim()) ? "0" : txtDiscountPer.Text.Trim()) * 0.01;
        //                    txtDiscount.Text = ViewState["TotalAmountDiscount"].ToString();
        //                }
        //                else { ViewState["TotalAmountDiscount"] = 0; }

        //                NetAmountCalculate();
        //                txtDiscountPer.Enabled = true; txtDiscount.Enabled = false;
        //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "OnchangeDiscountType();", true);  
        //            }
        //            else
        //            {
        //                txtDiscountPer.Text = "0.00";
        //                txtDiscountPer_OnTextChanged(null, null);
        //                this.ShowMessage("Discount should be less then 100 %");
        //            }
        //        }
        //    }
        //    txtDiscount.Focus();
        //}

        protected void txtBillNo_OnTextChanged(object sender, EventArgs e)
        {
            if (ddlFromCity.SelectedIndex > 0)
            {
                if (string.IsNullOrEmpty(txtBillNo.Text) == false)
                {
                    if (CheckBillExists() == false) { ShowMessageErr("Bill number already exist!"); }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMsg", "PassMessageError('Please select Location!')", true);
            }
            txtBillNo.Focus();
        }

        protected void CvtxtRate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((txtrate.Text == string.Empty) || (Convert.ToDouble(txtrate.Text) <= 0))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                rptTotalAmount += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                rptTotalWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                rptTotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblTotalAmnt.Text = rptTotalAmount.ToString("N2");
                lbltotalWeight.Text = rptTotalWeight.ToString("N2");
                lbltotalqty.Text = rptTotalQty.ToString();

            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PurchaseBillDAL objDAl = new PurchaseBillDAL();
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    Int32 itemType = objDAl.ItemType(Convert.ToInt32(ddlItemName.SelectedValue));
                    if(itemType==1)
                    {
                        ddltyresize.Enabled = true;
                    }
                    else
                    {
                       ddltyresize.Enabled = false;
                    }
                    ddltyresize.SelectedValue = string.IsNullOrEmpty(Convert.ToString(drs[0]["Tyresize_Idno"]))?"0": Convert.ToString(drs[0]["Tyresize_Idno"]);
                    ddlunitname.SelectedValue = Convert.ToString(drs[0]["Unit_Idno"]);
                    ddlRateType.SelectedValue = Convert.ToString(drs[0]["Rate_TypeIdno"]);

                    if (ddlRateType.SelectedValue == "1")
                    {
                        txtQuantity.Enabled = true;
                        txtweight.Enabled = false;
                    }
                    else { txtQuantity.Enabled = false; txtweight.Enabled = true; }

                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 0 : Convert.ToInt64(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    txtrate.Text = Convert.ToString(drs[0]["Rate"]) == "" ? "0" : drs[0]["Rate"].ToString();
                    ddlDivPer.SelectedValue = Convert.ToString(drs[0]["DivDiscType"]) == "" ? "0" : drs[0]["DivDiscType"].ToString();
                    txtDivDiscAmnt.Text = Convert.ToString("DivDiscAmnt") == "" ? "0.00" : drs[0]["DivDiscAmnt"].ToString();
                    txtDivOtherAmnt.Text = Convert.ToString("DivDiscOthAmnt") == "" ? "0.00" : drs[0]["DivDiscOthAmnt"].ToString();

                    ItemVAT = Convert.ToDouble(drs[0]["Vat"]);
                    ItemAdditVAT = Convert.ToDouble(drs[0]["Addit_Vat"]);
                    GrdTotalVAT = 0; GrdTotalAddVAT = 0; GrdTotalDiscAmnt = 0; GrdTotalDiscOthAmnt = 0;
                    HidTaxRate.Value = Convert.ToString(drs[0]["TaxRate"]);
                    hdnSGSTAmt.Value = Convert.ToString(drs[0]["SGST_Amt"]);
                    hdnCGSTAmt.Value = Convert.ToString(drs[0]["CGST_Amt"]);
                    hdnIGSTAmt.Value = Convert.ToString(drs[0]["IGST_Amt"]);
                    hdnSGSTPer.Value = Convert.ToString(drs[0]["SGST_Per"]);
                    hdnCGSTPer.Value = Convert.ToString(drs[0]["CGST_per"]);
                    hdnIGSTPer.Value = Convert.ToString(drs[0]["IGST_Per"]);
                    hdnGSTFlag.Value = Convert.ToString(drs[0]["Gst_idno"]);
                    ViewState["ID"] = Convert.ToString(drs[0]["id"]);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Name"], rw["Item_Idno"], rw["Unit_Name"], rw["Unit_Idno"], rw["Rate_Type"], rw["Rate_TypeIdno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"], rw["IGrp_Idno"], rw["Vat"], rw["Addit_Vat"], rw["Item_type"], rw["TaxRate"], rw["DivDiscType"], rw["strDivDiscType"], rw["DivDiscValue"], rw["DivDiscAmnt"], rw["DivDiscOthAmnt"], rw["Tyresize"], rw["Tyresize_Idno"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
            }
            else if (e.CommandName == "cmdstck")
            {
                Int64 hditemid = Convert.ToInt64(e.CommandArgument);
                GridViewRow Grow = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
                Label lblitemname = (Label)Grow.FindControl("lblItemName");
                Label lblqty = (Label)Grow.FindControl("lblQty");
                HiddenField HidTyreSize_Idno = (HiddenField)Grow.FindControl("HidTyreSize_Idno");
                Int64 tyresizeidno = Convert.ToInt64(HidTyreSize_Idno.Value);

                int VehiclPurId = Convert.ToInt32(Request.QueryString["PB"]);

                if (VehiclPurId > 0)
                {
                    this.SelectStockforItemPurBill(VehiclPurId, Convert.ToInt32(hditemid), tyresizeidno, Convert.ToInt32(Convert.ToDouble(lblqty.Text).ToString("N0")));
                }
                lblmsg.Text = string.Empty;


                // this.SelectVehicleChallanForColorCount(VehiclPurId, Convert.ToInt32(hiddenitemid.Value));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GrdTotalQuantity += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "quantity"));
                GrdTotalWeight += Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Weight")) == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight")));

                GrdTotalVAT += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Vat"));
                GrdTotalAddVAT += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Addit_Vat"));

                GrdTotalDiscAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DivDiscAmnt"));
                GrdTotalDiscOthAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DivDiscOthAmnt"));

                //GrdTotalAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")) + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")) * Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Vat")) * 0.01) + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount")) * Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Addit_Vat")) * 0.01);
                GrdTotalAmount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                GrdTotalRate += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate"));
                SGST += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SGST_Amt"));
                CGST += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CGST_Amt"));
                IGST += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "IGST_Amt"));

                ImageButton imgstck = (ImageButton)e.Row.FindControl("imgstck");
                string ItemType = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Item_type"));
                if (ItemType == "1")
                {
                    imgstck.Visible = true;
                }
                else { imgstck.Visible = false; }
                //--------------------
                LinkButton lnkbtEdit = (LinkButton)e.Row.FindControl("lnkbtEdit");
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                HiddenField HidItem_Idno = (HiddenField)e.Row.FindControl("HidItem_Idno");
                string PurBillid = Convert.ToString(ViewState["PBillHead_Idno"]);


                if ((string.IsNullOrEmpty(PurBillid)==false) && (string.IsNullOrEmpty(HidItem_Idno.Value)==false))
                {
                    PurchaseBillDAL obj = new PurchaseBillDAL();
                    var IdExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(PurBillid), Convert.ToInt32(HidItem_Idno.Value));
                    if (IdExist != null && IdExist.Count > 0)
                    {
                        lnkbtEdit.Visible = false;
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtEdit.Visible = true;
                        lnkbtnDelete.Visible = true;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                lblQuantity.Text = GrdTotalQuantity.ToString("N2");

                Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                lblWeight.Text = GrdTotalWeight.ToString("N2");

                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = GrdTotalAmount.ToString("N2");

                Label lblVAT = (Label)e.Row.FindControl("lblVAT");
                lblVAT.Text = GrdTotalVAT.ToString("N2");

                Label lblAddVAT = (Label)e.Row.FindControl("lblAddVAT");
                lblAddVAT.Text = GrdTotalAddVAT.ToString("N2");

                Label lblDivDiscAmnt = (Label)e.Row.FindControl("lblDivDiscAmnt");
                lblDivDiscAmnt.Text = GrdTotalDiscAmnt.ToString("N2");

                Label lblDivDiscOthAmnt = (Label)e.Row.FindControl("lblDivDiscOthAmnt");
                lblDivDiscOthAmnt.Text = GrdTotalDiscOthAmnt.ToString("N2");
                Label lblSGSTAmt = (Label)e.Row.FindControl("lblSGSTAmt");
                Label lblCGSTAmt = (Label)e.Row.FindControl("lblCGSTAmt");
                Label lblIGSTAmt = (Label)e.Row.FindControl("lblIGSTAmt");
                lblSGSTAmt.Text = Convert.ToString(SGST);
                lblCGSTAmt.Text = Convert.ToString(CGST);
                lblIGSTAmt.Text = Convert.ToString(IGST);
            }
            EnableDisableCal();

            txtTotalAmount.Text = GrdTotalAmount.ToString("N2");
            txtNetAmnt.Text = GrdTotalAmount.ToString("N2");
            NetAmountCalculate();
        }

        private void EnableDisableCal()
        {
            if (DivDiscount > 0)
            {
                ddlDiscountType.Enabled = false; txtDiscountPer.Text = "0.00"; txtDiscountPer.Enabled = false;
            }
            else
            {
                ddlDiscountType.Enabled = true; txtDiscountPer.Enabled = true;
            }

        }
        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }
        #endregion

        #region Functions and Control Events..

        private void SelectStockforItemPurBill(Int64 PurBillId, Int64 itemId,Int64 tyresizeidno , int rows)
        {
            PurchaseBillDAL Obj = new DAL.PurchaseBillDAL();
            var lst = Obj.SelectStockforPurBill(PurBillId, itemId, tyresizeidno);
            Obj = null;
            DtStockTemp = this.CreateStockBlankDataTable();
            if (lst.Count > 0)
            {
                for (int Count = 0; Count < lst.Count; Count++)
                {
                    string SerialNo, SerlDetlIdno, CompName, Type, PurFrom,tyresizeid;
                    SerialNo = Convert.ToString(DataBinder.Eval(lst[Count], "SerialNo"));
                    SerlDetlIdno = Convert.ToString(DataBinder.Eval(lst[Count], "SerlDetl_id"));
                    CompName = Convert.ToString(DataBinder.Eval(lst[Count], "CompName"));
                    Type = Convert.ToString(DataBinder.Eval(lst[Count], "Type"));
                    PurFrom = Convert.ToString(DataBinder.Eval(lst[Count], "PurFrom"));
                    tyresizeid = Convert.ToString(DataBinder.Eval(lst[Count], "TyresizeIdno"));
                    ApplicationFunction.DatatableAddRow(DtStockTemp, SerialNo, itemId, SerlDetlIdno, CompName, Type, PurFrom, tyresizeid);
                }
                if (rows > lst.Count)
                {
                    for (int Count = lst.Count; Count < rows; Count++)
                    {
                        ApplicationFunction.DatatableAddRow(DtStockTemp, string.Empty, itemId, 0, string.Empty, string.Empty, string.Empty,string.Empty);
                    }
                }
            }
            else
            {
                this.ManageStockDetail(itemId, tyresizeidno, rows);
            }
            rptstck.DataSource = DtStockTemp;
            rptstck.DataBind();
        }

        private DataTable CreateStockBlankDataTable()
        {
            DataTable dtTemp = ApplicationFunction.CreateTable("tbl", "SerialNo", "String", "ItemIdno", "String", "SerlDetl_id", "String", "CompName", "String", "Type", "String", "PurFrom", "String", "tyresizeid", "String");
            return dtTemp;
        }

        private void ManageStockDetail(Int64 itemIdno,Int64 tyresizeidno , Int64 rows)
        {
            DtStockTemp = this.CreateStockBlankDataTable();

            for (int Count = 1; Count <= rows; Count++)
            {
                ApplicationFunction.DatatableAddRow(DtStockTemp, string.Empty, itemIdno, string.Empty, string.Empty, string.Empty, string.Empty, tyresizeidno);
            }
            rptstck.DataSource = DtStockTemp;
            rptstck.DataBind();
        }


        protected void lnkbtnStockSubmit_OnClick(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (RepeaterItem item in rptstck.Items)
            {
                string SerialNo = "";
                TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                if (txtserialNo.Text.Trim() == string.Empty)
                {
                    lblmsg.Text = "Serial No can't be left blank.";
                    txtserialNo.Focus();
                    flag = false;
                    break;
                }
                else
                {
                    BindDropdownDAL obj = new BindDropdownDAL();
                    Int64 Count = obj.CheckSerialNo(txtserialNo.Text.Trim());
                    if (Count != 0)
                    {
                        SerialNo = txtserialNo.Text.Trim();
                        lblmsg.Text = "Serial No : " + SerialNo + " already exists in stock.";
                        lblmsg.CssClass = "redfont";
                        flag = false;
                        break;
                    }
                }
            }

            string oldchasisno = string.Empty;
            for (int count = 0; count < rptstck.Items.Count; count++)
            {
                TextBox txtserialNo = (TextBox)rptstck.Items[count].FindControl("txtserialNo");
                for (int count1 = count + 1; count1 < rptstck.Items.Count; count1++)
                {
                    TextBox txtserialNo1 = (TextBox)rptstck.Items[count1].FindControl("txtserialNo");

                    if (txtserialNo.Text.Trim() == txtserialNo1.Text.Trim() && string.IsNullOrEmpty(txtserialNo1.Text) == false)
                    {
                        lblmsg.Text = "Serial No Already exists in list.";
                        lblmsg.CssClass = "redfont";
                        flag = false;
                        break;
                    }
                }
            }




            if (flag == true)
            {
                string msgAlreadyExists = string.Empty;
                string saveMsg = string.Empty;
                int VPurBillIdno = Convert.ToInt32(Request.QueryString["PB"]);


                foreach (RepeaterItem item in rptstck.Items)
                {
                    TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                    HiddenField hidStckIdno = (HiddenField)item.FindControl("hidSerialIdno");
                    PurchaseBillDAL obj = new PurchaseBillDAL();
                    if (string.IsNullOrEmpty(Convert.ToString(hidStckIdno.Value)))//for insert
                    {
                        if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), 0, "save") == true)
                        {
                            msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                        }
                    }
                    else
                    {
                        if (obj.CheckChasisForStck(txtserialNo.Text.Trim(), VPurBillIdno, "update") == true)
                        {
                            msgAlreadyExists += "Serial No: " + txtserialNo.Text + ",";
                        }
                    }
                }


                if (msgAlreadyExists != string.Empty)
                {
                    lblmsg.Text = msgAlreadyExists + "  already exists!";
                }
                else
                {
                    foreach (RepeaterItem item in rptstck.Items)
                    {
                        TextBox txtserialNo = (TextBox)item.FindControl("txtserialNo");
                        HiddenField hidSerialIdno = (HiddenField)item.FindControl("hidSerialIdno");
                        HiddenField hidItemIdno = (HiddenField)item.FindControl("hidItemIdno");
                        int itemIdno = Convert.ToInt32(hidItemIdno.Value);
                        HiddenField Hidtyresizeidno = (HiddenField)item.FindControl("Hidtyresizeidno");
                        Int64 tyresizeId = Convert.ToInt64(Hidtyresizeidno.Value);
                        TextBox txtCompany = (TextBox)item.FindControl("txtCompanys");
                        DropDownList ddlType = (DropDownList)item.FindControl("ddlType");
                        TextBox txtPurParty = (TextBox)item.FindControl("txtPurParty");
                        string serialNo = txtserialNo.Text;


                        PurchaseBillDAL obj = new PurchaseBillDAL();
                        Int64 value = 0;
                        if (string.IsNullOrEmpty(Convert.ToString(hidSerialIdno.Value)) || Convert.ToInt32(hidSerialIdno.Value) <= 0)//for insert
                        {
                            value = obj.InsertPurBillStock(VPurBillIdno, itemIdno, serialNo, Convert.ToString(txtCompany.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue), tyresizeId);
                            if (value <= 0)
                            {
                                saveMsg += serialNo + ",";
                            }
                        }
                        else
                        {
                            value = obj.UpdatePurBillStock(VPurBillIdno, itemIdno, serialNo, Convert.ToString(txtCompany.Text), Convert.ToInt32(ddlType.SelectedValue), Convert.ToString(txtPurParty.Text), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt32(string.IsNullOrEmpty(hidSerialIdno.Value) ? "0" : hidSerialIdno.Value), Convert.ToInt32(ddlDateRange.SelectedValue), tyresizeId);
                            if (value <= 0)
                            {
                                saveMsg += serialNo + ",";
                            }
                        }

                        obj = null;

                    }
                    if (saveMsg == string.Empty)
                    {
                        lblmsg.Text = "Record saved."; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", " HideStck()", true);
                        lblmsg.CssClass = "";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblmsg.Text = "Records not saved Serial " + saveMsg;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);

        }

        private void ShowStock()
        {
            if (Convert.ToInt32(Request.QueryString["PB"]) > 0)
            {
                foreach (GridViewRow row in grdMain.Rows)
                {
                    ImageButton imgstck = (ImageButton)row.FindControl("imgstck");
                    HiddenField HidItemType = (HiddenField)row.FindControl("HidItem_Type");
                    if (imgstck != null && HidItemType.Value == "1")
                    {
                        imgstck.Visible = true;
                    }
                }
            }
        }

        protected void ddlPurchaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindItems(Convert.ToInt32(ddlPurchaseType.SelectedValue));
            this.BindPetrolPump();
            dtTemp = (DataTable)ViewState["dt"];
            if (dtTemp != null)
            {
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }

            ddlPurchaseType.Focus();
        }

        private void BindTruck()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNoPurchase();
            ddlLorry.DataSource = TruckNolst;
            ddlLorry.DataTextField = "Lorry_No";
            ddlLorry.DataValueField = "lorry_idno";
            ddlLorry.DataBind();
            ddlLorry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        protected void lnkbtnUpload_OnClick(object sender, EventArgs e)
        {

            if (ddlFromCity.SelectedIndex == 0) { ShowMessageErr("Please Select Location!"); ddlFromCity.Focus(); return; }
            if (ddlPurchaseType.SelectedValue != "3") { ShowMessageErr("Please Select Purchase Type [Fuel]."); ddlPurchaseType.Focus(); return; }

            Int64 succ = 0;
            Int64 PrtyIdno = 0;
            Int64 LorryIdno = 0;
            Int64 BillNo = 0;
            string Foldername = string.Empty;
            string Extension = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName);
            string filename = Path.GetFileName(FileUpload.PostedFile.FileName.ToString());

            if (Extension == ".XLS" || Extension == ".XLSX" || Extension == ".xls" || Extension == ".xlsx")
            {
                Foldername = Server.MapPath("~/Itemsexcel/");
                FileUpload.PostedFile.SaveAs(Foldername + filename.ToString());
                string uploadfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "Itemsexcel", "Itemsexcel");
                string FilePath = Foldername + uploadfilename;
                DataSet ds = ImportFromExcel(FilePath);
                if (ds.Tables.Count > 0)
                {
                    using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns[0].Caption == "BillNo" && ds.Tables[0].Columns[1].Caption == "Bill Date" && ds.Tables[0].Columns[2].Caption == "Party" && ds.Tables[0].Columns[3].Caption == "Lorry" && ds.Tables[0].Columns[4].Caption == "Item" && ds.Tables[0].Columns[5].Caption == "Qty" && ds.Tables[0].Columns[6].Caption == "Rate" && ds.Tables[0].Columns[7].Caption == "Remark")
                        {
                            try
                            {
                                var nnn = ds.Tables[0].AsEnumerable().Select(s => new { BillNo = s.Field<double>("BillNo") }).Distinct().ToList();
                                for (int j = 0; j < nnn.Count; j++)
                                {

                                    DataTable DTChild = ds.Tables[0].Clone();
                                    DataRow[] Dr = ds.Tables[0].Select("BillNo=" + Convert.ToString(DataBinder.Eval(nnn[j], "BillNo")));

                                    foreach (DataRow dr in Dr)
                                    {
                                        DTChild.ImportRow(dr); //Error thrown here.
                                    }
                                    DTChild.AcceptChanges();

                                    PurchaseBillDAL obj = new PurchaseBillDAL();
                                    PrtyIdno = Convert.ToInt64(obj.GetPartyIdno(Convert.ToString(DTChild.Rows[0]["Party"]).Trim()));
                                    if (PrtyIdno <= 0) { ShowMessageErr(DTChild.Rows[0]["Party"].ToString() + " Party Does Not Exist!"); return; }
                                    LorryIdno = Convert.ToInt64(obj.GetLorryIdno(Convert.ToString(DTChild.Rows[0]["Lorry"]).Trim()));
                                    if (LorryIdno <= 0) { ShowMessageErr(DTChild.Rows[0]["Lorry"].ToString() + " Lorry Does Not Exist!"); return; }
                                    BillNo = Convert.ToInt64(DTChild.Rows[0]["BillNo"]);
                                    dtTemp = CreateDt();
                                    ViewState["dt"] = dtTemp;
                                    for (int i = 0; i < DTChild.Rows.Count; i++)
                                    {

                                        DataTable dt = obj.GetItemDetailsExl(Convert.ToString(DTChild.Rows[i]["Item"]).Trim(), ApplicationFunction.ConnectionString());
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            dtTemp = (DataTable)ViewState["dt"];
                                            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                                            {
                                                foreach (DataRow row in dtTemp.Rows)
                                                {
                                                    if (Convert.ToInt32(row["Item_Idno"]) == Convert.ToInt32(dt.Rows[0]["ItemIdno"]))
                                                    {
                                                        string msg = "Item Already Selected!";
                                                        ddlItemName.Focus();
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                                                        return;
                                                    }
                                                }
                                            }
                                            Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                                            int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                                            string strItemName = Convert.ToString(dt.Rows[0]["ItemName"]);
                                            string strItemNameId = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ItemIdno"])) ? "0" : Convert.ToString(dt.Rows[0]["ItemIdno"]);
                                            string strUnitName = Convert.ToString(dt.Rows[0]["UomName"]);
                                            string strUnitNameId = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["UomIdno"])) ? "0" : Convert.ToString(dt.Rows[0]["UomIdno"]);
                                            string strRateType = "Rate";
                                            string strRateTypeIdno = "1";
                                            string strQty = string.IsNullOrEmpty(Convert.ToString(DTChild.Rows[i]["Qty"])) ? "0" : (Convert.ToString(DTChild.Rows[i]["Qty"]));
                                            string strWeight = "0";
                                            string strRate = string.IsNullOrEmpty(Convert.ToString(DTChild.Rows[i]["Rate"])) ? "0.00" : (Convert.ToString(DTChild.Rows[i]["Rate"]));
                                            string strIGrp_Idno = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["IGrp_Idno"])) ? "0" : Convert.ToString(dt.Rows[0]["IGrp_Idno"]);
                                            string Vat = "0";
                                            string Addit_Vat = "0";
                                            string Item_type = "0";
                                            string Total = (Convert.ToDouble(DTChild.Rows[i]["Qty"]) * Convert.ToDouble(DTChild.Rows[i]["Rate"])).ToString("N2");
                                            ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, Total, strIGrp_Idno, Vat, Addit_Vat, Item_type, "0");
                                        }
                                        else
                                        {
                                            ShowMessageErr(DTChild.Rows[i]["Item"].ToString() + " Item Does Not Exist!"); return;
                                        }
                                        // ViewState["ds"] = ds;
                                    }

                                    ViewState["dt"] = dtTemp;

                                    txtBillDate.Text = Convert.ToDateTime(DTChild.Rows[0]["Bill Date"]).ToString("dd-MM-yyyy");
                                    txtBillNo.Text = Convert.ToString(DTChild.Rows[0]["BillNo"]);
                                    TxtRemark.Text = Convert.ToString(DTChild.Rows[0]["Remark"]);
                                    txtNetAmnt.Text = Convert.ToString(dtTemp.Rows[0]["Amount"]);
                                    txtTotalAmount.Text = Convert.ToString(dtTemp.Rows[0]["Amount"]);
                                    txtDiscount.Text = "0.00";
                                    txtOtherCharges.Text = "0.00";
                                    txtRoundOff.Text = "0.00";

                                    #region  SAVE PURCHASES
                                    if (IsValid)
                                    {
                                        Int64 varForSave = 0;
                                        DateTime? BillDate = null;
                                        BillDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBillDate.Text));
                                        int PurchaseType = Convert.ToInt32(ddlPurchaseType.SelectedValue);
                                        int BillType = 0; if (rdoCredit.Checked) { BillType = 1; } else if (rdoCash.Checked) { BillType = 2; }
                                        DateTime DateAdded = System.DateTime.Now;
                                        DataTable dtDetail = (DataTable)ViewState["dt"];
                                        double Discount = 0;

                                        if (Convert.ToDouble(txtNetAmnt.Text) > 0)
                                        {
                                            varForSave = obj.Insert(Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64(ddlFromCity.SelectedValue), BillDate, txtPrefixNo.Text.Trim(), BillNo, PurchaseType, PrtyIdno, BillType, TxtRemark.Text.Trim(), Convert.ToDouble(txtTotalAmount.Text.Trim()), Convert.ToInt32(ddlDiscountType.SelectedValue), Convert.ToDouble(txtDiscount.Text.Trim()), Convert.ToDouble(txtOtherCharges.Text.Trim()), Convert.ToDouble(txtRoundOff.Text.Trim()), Convert.ToDouble(txtNetAmnt.Text.Trim()), DateAdded, dtDetail, LorryIdno, Discount);
                                            if (varForSave > 0)
                                            {
                                                string strRoundOff = txtRoundOff.Text.Trim().Replace("(", "").Replace(")", "");
                                                if (this.PostIntoAccounts(dtDetail, varForSave, "PB", (Convert.ToString(strRoundOff) == "" ? 0 : Convert.ToDouble(strRoundOff)), base.CompId, base.UserIdno, 0, 0) == true)
                                                {
                                                    succ = varForSave;
                                                }

                                            }
                                            else
                                            {
                                                succ = 0;
                                                break;
                                            }
                                            //     this.clearAllControl();
                                        }
                                        else
                                        {
                                            Tran.Dispose();
                                            this.ShowMessageErr("Net amount should be greater than 0!");
                                        }

                                    }
                                    #endregion
                                }

                                if (succ > 0)
                                {
                                    Tran.Complete();
                                    string strMsg = "Record saved successfully.";
                                    this.ShowMessage(strMsg);
                                }
                                else if (succ == 0)
                                {
                                    Tran.Dispose();
                                    this.ShowMessageErr("Record Already Exist");
                                }
                                else
                                {
                                    Tran.Dispose();
                                    this.ShowMessageErr("Record Not Save");
                                }
                            }
                            catch (Exception ex)
                            {
                                Tran.Dispose();
                            }
                            this.clearAllControl();
                        }
                        else
                        {
                            this.ShowMessageErr("Excel Not In Correct Format.");
                        }
                    }
                }
            }
            else
            {
                this.ShowMessage("Please upload only excel file.");
            }

        }

        public DataSet ImportFromExcel(string file)
        {
            // Create new dataset
            DataSet ds = new DataSet();

            // -- Start of Constructing OLEDB connection string to Excel file
            Dictionary<string, string> props = new Dictionary<string, string>();

            // For Excel 2007/2010
            if (file.EndsWith(".xlsx"))
            {
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                props["Extended Properties"] = "Excel 12.0 XML";
            }
            // For Excel 2003 and older
            else if (file.EndsWith(".xls"))
            {
                props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                props["Extended Properties"] = "Excel 8.0";
            }
            else
                return null;

            props["Data Source"] = file;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            string connectionString = sb.ToString();
            // -- End of Constructing OLEDB connection string to Excel file

            // Connecting to Excel File
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();

                    // you can choose the colums you want.
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName.Replace("$", string.Empty);

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    // Add table into DataSet
                    ds.Tables.Add(dt);

                    break;
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }

        protected void rptstck_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PurchaseBillDAL obj = new PurchaseBillDAL();
                DropDownList DType = (DropDownList)e.Item.FindControl("ddlType");
                DataRowView Dr = (DataRowView)e.Item.DataItem;
                HiddenField hidItemIdno = (HiddenField)e.Item.FindControl("hidItemIdno");
                HiddenField hidSerialIdno = (HiddenField)e.Item.FindControl("hidSerialIdno");
                Int32 StckIdno = string.IsNullOrEmpty(hidSerialIdno.Value)?0 : Convert.ToInt32(hidSerialIdno.Value);
                TextBox txtserialNo = (TextBox)e.Item.FindControl("txtserialNo");
                TextBox txtCompanys = (TextBox)e.Item.FindControl("txtCompanys");
                DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlType");
                TextBox txtPurParty = (TextBox)e.Item.FindControl("txtPurParty");

                string PurBillid = Convert.ToString(ViewState["PBillHead_Idno"]);

                DType.SelectedValue = Convert.ToString(Dr["Type"]);
                if (string.IsNullOrEmpty(hidItemIdno.Value) == false)
                {
                    var Exist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(PurBillid), Convert.ToInt32(hidItemIdno.Value));
                    if (Exist != null && Exist.Count > 0)
                    {
                        for (int i = 0; i < Exist.Count; i++)
                        {
                            if (string.IsNullOrEmpty(txtserialNo.Text.Trim()) == false)
                            {
                                var value = obj.CheckSerialExists(txtserialNo.Text.Trim().ToString());
                                if (value != null && value.SerlDetl_id > 0)
                                {
                                    txtserialNo.Enabled = false; txtCompanys.Enabled = false; ddlType.Enabled = false; txtPurParty.Enabled = false;
                                }
                                else
                                {
                                    txtserialNo.Enabled = true; txtCompanys.Enabled = true; ddlType.Enabled = true; txtPurParty.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        public Int64 GetCompStateID()
        {
            PurchaseBillDAL obj = new PurchaseBillDAL();
            tblCompMast cm = obj.GetCompany();
            if (cm == null)
                return 0;
            else
                return string.IsNullOrEmpty(Convert.ToString(cm.State_Id)) ? 0 : Convert.ToInt64(cm.State_Id);
        }
        public Int64 GetPartyStateID(Int64 PartyID)
        {
            PurchaseBillDAL obj = new PurchaseBillDAL();
            AcntMast ad = obj.GetPartyDetail(PartyID);
            if (ad == null)
            {
                hdnStateID.Value = "0";
                return 0;
            }
            else
            {
                hdnStateID.Value = Convert.ToString(ad.State_Idno);
                return string.IsNullOrEmpty(Convert.ToString(ad.State_Idno)) ? 0 : Convert.ToInt64(ad.State_Idno);
            }
        }
        public void SetGSTPercent()
        {
            PurchaseBillDAL PurDAL = new PurchaseBillDAL();
            tblItemMastPur IM = PurDAL.GetVehDetail(Convert.ToInt64(ddlItemName.SelectedValue));
            if (IM != null)
            {
                hdnSGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.SGST)) ? "0" : Convert.ToString(IM.SGST);
                hdnCGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.CGST)) ? "0" : Convert.ToString(IM.CGST);
                hdnIGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.IGST)) ? "0" : Convert.ToString(IM.IGST);
            }
        }
        public void SetGSTPercent(Int64 ItemID)
        {
            PurchaseBillDAL PurDAL = new PurchaseBillDAL();
            tblItemMastPur IM = PurDAL.GetVehDetail(Convert.ToInt64(ItemID));
            if (IM != null)
            {
                hdnSGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.SGST)) ? "0" : Convert.ToString(IM.SGST);
                hdnCGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.CGST)) ? "0" : Convert.ToString(IM.CGST);
                hdnIGSTPer.Value = string.IsNullOrEmpty(Convert.ToString(IM.IGST)) ? "0" : Convert.ToString(IM.IGST);
            }
        }
        private void CalculateWithEdit()
        {
            double dRate = 0, dDisc = 0, dDiscValue = 0, dAmount = 0;
            double Amt = 0;
            double dSGSTAmt = 0, dCGSTAmt = 0, dIGSTAmt = 0, dothrAmt = 0;
            dRate = (txtrate.Text.Trim() == "") ? 0.00 : Convert.ToDouble(txtrate.Text.Trim());
            dDisc = (txtDivDiscAmnt.Text.Trim() == "") ? 0.00 : Convert.ToDouble(txtDivDiscAmnt.Text.Trim());
            dothrAmt = (txtDivOtherAmnt.Text.Trim() == "") ? 0.00 : Convert.ToDouble(txtDivOtherAmnt.Text.Trim());

            Amt = (Convert.ToDouble(txtQuantity.Text.Trim()) * dRate);

            if (ddlDivPer.SelectedItem.Text == "%")
            {
                if (Convert.ToString(hdnGSTFlag.Value) == "1")
                {
                    dSGSTAmt = (Amt - (Amt * dDisc / 100)) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnSGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnSGSTPer.Value))) / 100);
                    dCGSTAmt = (Amt - (Amt * dDisc / 100)) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnCGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnCGSTPer.Value))) / 100);

                }
                else
                {
                    dIGSTAmt = (Amt - (Amt * dDisc / 100)) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnIGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnIGSTPer.Value))) / 100);
                }
                dDiscValue = (Amt * dDisc / 100);

            }
            else
            {
                if (Convert.ToString(hdnGSTFlag.Value) == "1")
                {
                    dSGSTAmt = (Amt - dDisc) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnSGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnSGSTPer.Value))) / 100);
                    dCGSTAmt = (Amt - dDisc) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnCGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnCGSTPer.Value))) / 100);
                }
                else
                {
                    dIGSTAmt = (Amt - dDisc) * (Convert.ToDouble(((string.IsNullOrEmpty(hdnIGSTPer.Value) == true) ? 0 : Convert.ToDouble(hdnIGSTPer.Value))) / 100);
                }
                dDiscValue = dDisc;

            }

            if ((string.IsNullOrEmpty(hdnGSTFlag.Value) ? "0" : Convert.ToString(hdnGSTFlag.Value)) == "1")
            {
                dAmount = (Amt - dDiscValue) + dSGSTAmt + dCGSTAmt + dothrAmt;
                hdnSGSTAmt.Value = Convert.ToString(dSGSTAmt);
                hdnCGSTAmt.Value = Convert.ToString(dCGSTAmt);
                hdnIGSTAmt.Value = "0";
                hdnIGSTPer.Value = "0";
            }
            else
            {
                hdnIGSTAmt.Value = Convert.ToString(dIGSTAmt);
                hdnCGSTAmt.Value = "0";
                hdnSGSTAmt.Value = "0";

                hdnSGSTPer.Value = "0";
                hdnCGSTPer.Value = "0";
                dAmount = (Amt - dDiscValue) + dIGSTAmt + dothrAmt;
            }
            //txtAmount.Text = dAmount.ToString("N2");
            hdnAmount.Value = dAmount.ToString();
            hidDiscValue.Value = dDiscValue.ToString("N2");
        }
        private void CalculateNetAmount()
        {
            double SGSTamount = 0, CGSTamount = 0, IGSTamount = 0, UGSTamount = 0, Cessamount = 0;
            double amount = 0, dTNet = 0;
            double TSGST = 0, TCGST = 0, TIGST = 0, TUGST = 0, TCessAmount = 0;
            double dTAmount = 0, dTOther = 0, dTRound = 0;
            dtTemp = (DataTable)ViewState["dt"];
            foreach (DataRow dtrow in dtTemp.Rows)
            {

                SGSTamount = Convert.ToDouble(dtrow["SGST_Amt"]);
                CGSTamount = Convert.ToDouble(dtrow["CGST_Amt"]);
                IGSTamount = Convert.ToDouble(dtrow["IGST_Amt"]);
                amount = Convert.ToDouble(dtrow["Amount"]);

                TSGST += SGSTamount;
                TCGST += CGSTamount;
                TIGST += IGSTamount;
                dTAmount += amount;
            }

            dTOther = Convert.ToDouble(txtOtherCharges.Text.Trim());


            //            dTAmount = dTAmount + TSGST + TCGST + TIGST + TUGST + TCessAmount;
            dTNet = Convert.ToDouble(dTAmount + dTOther);
            // dTAmount = Convert.ToDouble(Math.Round(dTAmount));
            if (base.Pur_Round == true)
                dTAmount = Convert.ToDouble(Math.Round(dTAmount));
            else
                //Added for testing
                dTAmount = Convert.ToDouble(dTAmount);

            //txtTotalAmnt.Text = dTNet.ToString("N2");
            //txtNetAmount.Text = dTAmount.ToString("N2");
            //hidAmount.Value = txtNetAmount.Text.Trim();
            //dTRound = Convert.ToDouble(txtNetAmount.Text.Trim()) - (Convert.ToDouble(txtTotalAmnt.Text.Trim()) + dTOther);
            ////Added for testing
            //// dTRound = 0;
            //txtRound.Text = dTRound.ToString("N2");
        }
    }
}
