using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using System.IO;
//using WebTransport.Model;

namespace WebTransport
{
    public partial class UserPreference : Pagebase
    {
        #region Private Variable..
        private int intFormId = 53;
        static Byte[] bytes;
        FileStream fs;
        BinaryReader br;
        #endregion

        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    btnSave.Visible = false;
                //}
                txtRenameWages.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtTollRename.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtrefrename.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtPFChanges.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtServTax.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtBilty.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtSurchage.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtTollTax.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtWages.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtServTaxValid.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TxtStaxPan.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                chkTbbRate.Checked = true; this.BindBaseCity(); this.BindItemGroup(); this.Populate(); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "Logo()", true);
                ddlType.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnUpload_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            try
            {
                if (flupPic.HasFile)
                {
                    UserPrefenceDAL obj = new UserPrefenceDAL();
                    fileName = flupPic.FileName;
                    filePath = Server.MapPath("LogoImage/" + System.Guid.NewGuid() + fileName);
                    flupPic.SaveAs(filePath);
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    bytes = br.ReadBytes(Convert.ToInt32(fs.Length)); br.Close(); fs.Close();
                    
                    Int64 Result = obj.UpdateLogo(bytes, ((chkLogoReq.Checked == true) ? true : false));
                    if (Result > 0)
                    {
                        this.Populate();
                        this.ShowMessage("Logo Upload successfully.");
                    }
                    else
                    {
                        this.ShowMessage("Logo not Upload.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage("Logo not Upload.");
                return;
            } 
        }
        protected void lnkbtnSaveGroup_OnClick(object sender, EventArgs e)
        {
            UserPrefenceDAL obj = new UserPrefenceDAL();
            
            Int32 intItemType = Convert.ToInt32((ddlItemGropForPopup.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlItemGropForPopup.SelectedValue));
            Int32 intTaxType = Convert.ToInt32((ddlTaxType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlTaxType.SelectedValue));
            double dValue = Convert.ToDouble((txtPercentage.Text.Trim()) == "" ? 0.00 : Convert.ToDouble(txtPercentage.Text.Trim()));

            DataTable dt = obj.UpdateTaxVat(intItemType, intTaxType, dValue,ApplicationFunction.ConnectionString());
            if (dt != null && dt.Rows.Count > 0)
            {
                ShowMessage("Record updated successfully.");
                ddlItemGropForPopup.SelectedValue = ddlTaxType.SelectedValue = "0"; txtPercentage.Text = "0.00";
            }
            else
            {
                ShowMessageErr("Record Not updated.");
            }
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            bool bLessChlnAmntInv = false;
            if (hidToggleWeightWiseRate.Value == "1")
                bLessChlnAmntInv = true;
            if(chkLogoReq.Checked)
            {
                if (hideimgvalue.Value == "")
                {
                    this.ShowMessageErr("Logo Required.");
                    imgLogoShow.ImageUrl = hideimgvalue.Value;
                    return;
                }
            }
            string strMsg = string.Empty;
            UserPrefenceDAL objUserPrefence = new UserPrefenceDAL();
            Int64 intUsrPrefIdno = 0;
            Int64 iCity = 0;
            Int32 itype = Convert.ToInt32((ddlType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlType.SelectedValue));
            Int32 iInvoice = Convert.ToInt32((ddlInvoicePrt.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlInvoicePrt.SelectedValue));
            Int32 iChallan = Convert.ToInt32((ddlChallanPrint.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlChallanPrint.SelectedValue));
            String sWagesAs = Convert.ToString((txtRenameWages.Text.Trim()) == "" ? "" : Convert.ToString(txtRenameWages.Text.Trim()));
            String PFAs = Convert.ToString((txtPFChanges.Text.Trim()) == "" ? "" : Convert.ToString(txtPFChanges.Text.Trim()));
            String RenameTollTax = Convert.ToString((txtTollRename.Text.Trim()) == "" ? "" : Convert.ToString(txtTollRename.Text.Trim()));
            String RenameRefno = Convert.ToString((txtrefrename.Text.Trim()) == "" ? "" : Convert.ToString(txtrefrename.Text.Trim()));
            double dServTax = Convert.ToDouble((txtServTax.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtServTax.Text.Trim()));
            double dSurchge = Convert.ToDouble((txtSurchage.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtSurchage.Text.Trim()));
            double dBility = Convert.ToDouble((txtBilty.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtBilty.Text.Trim()));
            double dWages = Convert.ToDouble((txtWages.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtWages.Text.Trim()));
            double dTaxToll = Convert.ToDouble((txtTollTax.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtTollTax.Text.Trim()));
            double dServTxValid = Convert.ToDouble((txtServTaxValid.Text.Trim()) == "" ? 0 : Convert.ToDouble(txtServTaxValid.Text.Trim()));
            double dStaxPan = Convert.ToDouble((TxtStaxPan.Text.Trim()) == "" ? 0 : Convert.ToDouble(TxtStaxPan.Text.Trim()));
            Int32 AmntRcvdAgnst = Convert.ToInt32((ddlamntrcptagnst.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlamntrcptagnst.SelectedValue));
            Int32 Rate_Invo_Gr = Convert.ToInt32(ddlRateInvoGr.SelectedValue);
            Int32 PayChlnPrint = Convert.ToInt32(ddlPayChln.SelectedValue);
            Int32 GRPrint = Convert.ToInt32(ddlGRPrint.SelectedValue);
            bool LogoReq = Convert.ToBoolean(chkLogoReq.Checked);
            bool WeightWiseRate = Convert.ToBoolean(chkWeightWiseRate.Checked);
            bool TypeDelPlace = Convert.ToBoolean(chkDelPlace.Checked);
            Int32 InvGrType = Convert.ToInt32(ddlInvGen.SelectedValue);
            string Terms = Convert.ToString((txtterms.Text.Trim()) == "" ? "" : Convert.ToString(txtterms.Text.Trim()));
            string Terms1 = Convert.ToString((txtterms2.Text.Trim()) == "" ? "" : Convert.ToString(txtterms2.Text.Trim()));
            string TnCGRRetailer = Convert.ToString((txtGRRetTnC.Text.Trim()) == "" ? "" : Convert.ToString(txtGRRetTnC.Text.Trim()));
            string strCartage = Convert.ToString((txtCartageRename.Text.Trim()) == "" ? "" : Convert.ToString(txtCartageRename.Text.Trim()));
            string strCommission = Convert.ToString((txtComRename.Text.Trim()) == "" ? "" : Convert.ToString(txtComRename.Text.Trim()));
            string strBilty = Convert.ToString((txtBiltyRename.Text.Trim()) == "" ? "" : Convert.ToString(txtBiltyRename.Text.Trim()));
            string strStCharge = Convert.ToString((txtStCharge.Text.Trim()) == "" ? "" : Convert.ToString(txtStCharge.Text.Trim()));
            //string Termcondition = Terms.Replace("#", "</br>");
            //string Terms2 = Terms1.Replace("#", "</br>");
            string hireSlipTerms = Convert.ToString((txttermshireslip.Text.Trim()) == "" ? "" : Convert.ToString(txttermshireslip.Text.Trim()));
            bool strWithGstAmnt = Convert.ToBoolean(ChkWithoutGst.Checked);
            //string TermconditionHireSlip = hireSlipTerms.Replace("#", "</br>");
            intUsrPrefIdno = objUserPrefence.UpdateUserPrefence(iCity, itype, iInvoice, iChallan, sWagesAs, dServTax, dSurchge, dBility, dWages, dTaxToll, dServTxValid, dStaxPan, Convert.ToBoolean(chkTbbRate.Checked), Convert.ToBoolean(chkTDS.Checked), Convert.ToBoolean(chkGRRate.Checked), AmntRcvdAgnst, Rate_Invo_Gr, Convert.ToBoolean(chkChlnUpload.Checked), PayChlnPrint, Convert.ToBoolean(chkContRate.Checked), Convert.ToBoolean(chkCntrSBillReq.Checked), Convert.ToBoolean(chkShrtgEdit.Checked), Convert.ToBoolean(chkdisChln.Checked), Convert.ToBoolean(chkAdminApp.Checked), Convert.ToBoolean(chkheader.Checked), GRPrint, Convert.ToBoolean(chkGradeReq.Checked), PFAs, RenameTollTax, RenameRefno, LogoReq, (string.IsNullOrEmpty(ddlInvGen.SelectedValue) ? 1 : Convert.ToInt32(ddlInvGen.SelectedValue)), Terms, Terms1, TnCGRRetailer, WeightWiseRate, hireSlipTerms, Convert.ToBoolean(chkRateReq.Checked), strCartage, strCommission, strBilty, TypeDelPlace, strStCharge, strWithGstAmnt, bLessChlnAmntInv , Convert.ToBoolean(ChkGstCalGr.Checked));
            objUserPrefence = null;
            if (intUsrPrefIdno > 0)
            {
                strMsg = "Record updated successfully.";
                this.ClearControls();
            }
            Populate();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            this.ClearControls();
            this.Populate();

        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("UserPreference.aspx");
        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="LorryIdno"></param>
        private void Populate()
        {
            UserPrefenceDAL objUserPrefence = new UserPrefenceDAL();
            var objUserPref = objUserPrefence.SelectById();
            objUserPrefence = null;
            if (objUserPref != null)
            {
                txtBilty.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.Bilty_Amnt)) ? "0" : String.Format("{0:0,0.00}", objUserPref.Bilty_Amnt);
                ddlType.SelectedValue = Convert.ToString(objUserPref.Work_Type);
                ddlInvoicePrt.SelectedValue = Convert.ToString(objUserPref.InvPrint_Type);
                chkTbbRate.Checked = Convert.ToBoolean(objUserPref.TBB_Rate);
                ddlRateInvoGr.SelectedValue = Convert.ToString(objUserPref.Rate_Invo_Gr);
                chkTbbRate_OnCheckedChanged(new Object(), EventArgs.Empty);
                hidUserPrefidno.Value = Convert.ToString(objUserPref.UserPref_Idno);
                ddlChallanPrint.SelectedValue = Convert.ToString(objUserPref.ChlnPrint_Type);
                txtServTax.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.ServTax_Perc)) ? "0" : String.Format("{0:0,0.00}", objUserPref.ServTax_Perc);
                txtSurchage.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.Surchg_Per)) ? "0" : String.Format("{0:0,0.00}", objUserPref.Surchg_Per);
                txtTollTax.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.TollTax_Amnt)) ? "0" : String.Format("{0:0,0.00}", objUserPref.TollTax_Amnt);
                txtWages.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.Wages_Amnt)) ? "" : String.Format("{0:0,0.00}", objUserPref.Wages_Amnt);
                txtRenameWages.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.WagesLabel_Print)) ? "" : String.Format("{0:0,0.00}", objUserPref.WagesLabel_Print);
                txtServTaxValid.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.ServTax_Valid)) ? "0" : String.Format("{0:0,0.00}", objUserPref.ServTax_Valid);
                TxtStaxPan.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.STaxPer_TDS)) ? "0" : String.Format("{0:0,0.00}", objUserPref.STaxPer_TDS);
                ddlamntrcptagnst.SelectedValue = Convert.ToString(objUserPref.AmntRecvdAgnst_GRInvce);
                chkTDS.Checked = Convert.ToBoolean(objUserPref.TDSEdit);
                chkGRRate.Checked = Convert.ToBoolean(objUserPref.GRRate);
                chkChlnUpload.Checked = Convert.ToBoolean(objUserPref.Chln_Excel);
                ddlPayChln.SelectedValue = Convert.ToString(objUserPref.PayChln_Print);
                chkContRate.Checked = Convert.ToBoolean(objUserPref.Cont_Rate);
                chkCntrSBillReq.Checked = Convert.ToBoolean(objUserPref.CntrSBill_Req);
                chkShrtgEdit.Checked = Convert.ToBoolean(objUserPref.ShrtgRateChlnConf);
                chkdisChln.Checked = Convert.ToBoolean(objUserPref.DisableChlnEntry);
                chkAdminApp.Checked = Convert.ToBoolean(objUserPref.AdminApp_Inv);
                chkWeightWiseRate.Checked = Convert.ToBoolean(objUserPref.WeightWise_Rate);
                chkheader.Checked = Convert.ToBoolean(objUserPref.GRPrint_WithoutHeader);
                chkGradeReq.Checked = Convert.ToBoolean(objUserPref.ItemGrade_Req);
                txtPFChanges.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.PFLabel_GR)) ? "" : String.Format("{0:0,0.00}", objUserPref.PFLabel_GR);
                txtTollRename.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.TollTaxLabel_GR)) ? "" : String.Format("{0:0,0.00}", objUserPref.TollTaxLabel_GR);
                txtrefrename.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.Reflabel_Gr)) ? "" : String.Format("{0:0,0.00}", objUserPref.Reflabel_Gr);
                txtCartageRename.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.CartageLabel_GR)) ? "" : String.Format("{0:0,0.00}", objUserPref.CartageLabel_GR);
                txtComRename.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.CommissionLabel_Gr)) ? "" : String.Format("{0:0,0.00}", objUserPref.CommissionLabel_Gr);
                txtBiltyRename.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.BiltyLabel_GR)) ? "" : String.Format("{0:0,0.00}", objUserPref.BiltyLabel_GR);
                txtStCharge.Text = string.IsNullOrEmpty(Convert.ToString(objUserPref.StChargLabel_GR)) ? "" : String.Format("{0:0,0.00}", objUserPref.StChargLabel_GR);
                chkLogoReq.Checked = Convert.ToBoolean(objUserPref.Logo_Req);
                ddlInvGen.SelectedValue = (string.IsNullOrEmpty(objUserPref.InvGen_GrType.ToString()) ? "1" : Convert.ToString(objUserPref.InvGen_GrType));
                ddlGRPrint.SelectedValue = (string.IsNullOrEmpty(objUserPref.GRPrintPref.ToString()) ? "1" : Convert.ToString(objUserPref.GRPrintPref));
                string Terms1 = objUserPref.Terms;
                txtterms.Text = string.IsNullOrEmpty(Terms1)==true? "" : Convert.ToString(Terms1);
                string Terms = objUserPref.Terms1;
                txtterms2.Text = string.IsNullOrEmpty(Terms) == true ? "" : Convert.ToString(Terms);
                string Termshieslip = objUserPref.HireslipTerms;
                txttermshireslip.Text = string.IsNullOrEmpty(Termshieslip) == true ? "" : Convert.ToString(Termshieslip);
                string TermsGRRet = objUserPref.Terms_Con_Retailer;
                txtGRRetTnC.Text = string.IsNullOrEmpty(TermsGRRet) == true ? "" : Convert.ToString(TermsGRRet);
                chkRateReq.Checked = Convert.ToBoolean(objUserPref.GRRetRequired);
                chkDelPlace.Checked = Convert.ToBoolean(objUserPref.TypeDelPlace);
                ChkWithoutGst.Checked = Convert.ToBoolean(objUserPref.With_GstAmnt);
                chkLessChlnAmntInv.Checked = Convert.ToBoolean(objUserPref.LessChlnAmnt_Inv);
                ChkGstCalGr.Checked = Convert.ToBoolean(objUserPref.GstCal_Gr);
                if (chkWeightWiseRate.Checked)
                    hidToggleWeightWiseRate.Value = "1";
                if (Convert.ToBoolean(objUserPref.Logo_Req))
                {
                    if (objUserPref.Logo_Image != null)
                    {
                        byte[] img = objUserPref.Logo_Image;
                        string base64String = Convert.ToBase64String(img, 0, img.Length);
                        hideimgvalue.Value = "data:image/png;base64," + base64String;
                        imgLogoShow.ImageUrl = hideimgvalue.Value;
                    }
                }
                else
                {
                    hideimgvalue.Value = "";
                    imgLogoShow.ImageUrl = hideimgvalue.Value;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "hide();", true);
                }
            }
        }

        private void BindBaseCity()
        {
            UserPrefenceDAL objUserPrefence = new UserPrefenceDAL();
            var objUserPref = objUserPrefence.SelectBaseCity();
            objUserPrefence = null;
            // ddlCity.DataSource = objUserPref;
            // ddlCity.DataTextField = "City_Name";
            //ddlCity.DataValueField = "City_Idno";
            // ddlCity.DataBind();
            // ddlCity.Items.Insert(0, new ListItem("< Choose City Name >", "0"));
        }
        private void BindItemGroup()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var objUserPref = obj.BindItemGroup();
            obj = null;
            if (objUserPref != null && objUserPref.Count > 0)
            {
                ddlItemGropForPopup.DataSource = objUserPref;
                ddlItemGropForPopup.DataTextField = "ItemType_Name";
                ddlItemGropForPopup.DataValueField = "ItemTpye_Idno";
                ddlItemGropForPopup.DataBind();
            }
            ddlItemGropForPopup.Items.Insert(0, new ListItem("-- Select --", "0"));
        }


        
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            //  ddlCity.SelectedValue = "1";
            ddlType.SelectedValue = "1";
            ddlChallanPrint.SelectedValue = "1";
            ddlInvoicePrt.SelectedValue = "1";
            txtBilty.Text = txtServTax.Text = string.Empty;
            txtServTaxValid.Text = "";
            TxtStaxPan.Text = "";
            chkTbbRate.Checked = true;
            hidUserPrefidno.Value = string.Empty;
            txtSurchage.Text = string.Empty;
            txtTollTax.Text = string.Empty;
            txtWages.Text = string.Empty;
            txtRenameWages.Text = string.Empty;
            chkLessChlnAmntInv.Checked = false;
            //ddlCity.Focus();
        }

        #endregion

        #region Control Events
        protected void txtServTax_TextChanged(object sender, EventArgs e)
        {
            if (txtServTax.Text.Trim() == "")
            {
                txtServTax.Text = "0.00";
            }
            else
            {
                txtServTax.Text = Convert.ToDouble(txtServTax.Text).ToString("N2");
            }
        }
        protected void txtSurchage_TextChanged(object sender, EventArgs e)
        {
            if (txtSurchage.Text == "")
            {
                txtSurchage.Text = "0.00";
            }
            else
            {
                txtSurchage.Text = Convert.ToDouble(txtSurchage.Text).ToString("N2");
            }

        }
        protected void txtWages_TextChanged(object sender, EventArgs e)
        {
            if (txtWages.Text == "")
            {
                txtWages.Text = "0.00";
            }
            else
            {
                txtWages.Text = Convert.ToDouble(txtWages.Text).ToString("N2");
            }

        }
        protected void txtTollTax_TextChanged(object sender, EventArgs e)
        {
            if (txtTollTax.Text == "")
            {
                txtTollTax.Text = "0.00";
            }
            else
            {
                txtTollTax.Text = Convert.ToDouble(txtTollTax.Text).ToString("N2");
            }

        }
        protected void txtServTaxValid_TextChanged(object sender, EventArgs e)
        {
            if (txtServTaxValid.Text == "")
            {
                txtServTaxValid.Text = "0.00";
            }
            else
            {
                txtServTaxValid.Text = Convert.ToDouble(txtServTaxValid.Text).ToString("N2");
            }
        }
        protected void txtBilty_TextChanged(object sender, EventArgs e)
        {
            if (txtBilty.Text == "")
            {
                txtBilty.Text = "0.00";
            }
            else
            {
                txtBilty.Text = Convert.ToDouble(txtBilty.Text).ToString("N2");
            }

        }
        protected void TxtStaxPan_TextChanged(object sender, EventArgs e)
        {
            if (TxtStaxPan.Text == "")
            {
                TxtStaxPan.Text = "0.00";
            }
            else
            {
                TxtStaxPan.Text = Convert.ToDouble(TxtStaxPan.Text).ToString("N2");
            }

        }

        protected void chkTbbRate_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkTbbRate.Checked)
            {
                ddlRateInvoGr.SelectedIndex = 0;
                ddlRateInvoGr.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
            }
            else
            {
                ddlRateInvoGr.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
            }
        }
        #endregion

    }
}
