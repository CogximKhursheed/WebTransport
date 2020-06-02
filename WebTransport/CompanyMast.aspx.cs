using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class CompanyMast : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 8;
        private bool Active = true;
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
                txtCompany.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                //txtCompDescription.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtAdrs1.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
                txtAdrs2.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
                txtpincode.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");

                txtMobile.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtMobileNumber.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");

                txtSMSAuthType.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");

                txtTinNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtemailid.Attributes.Add("onkeypress", "return allowEmail(event)");
                txtCSTNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtTanNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtPanNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtFaxNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtCodeNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtCntPrsnNm1.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtCntPrsnNm2.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event)");
                txtCntPrsnNo1.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtCntPrsnNo2.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtTotLoc.Attributes.Add("onkeypress", "return allowOnlyNumber(event)");
                txtCntPrsnEmail1.Attributes.Add("onkeypress", "return allowEmail(event)");
                txtCntPrsnEmail2.Attributes.Add("onkeypress", "return allowEmail(event)");

                this.BindState();
                this.BindCity();

                if (Session["CogximAdmin"] != null)
                {
                    txtCompany.Enabled = true;
                    ddlCity.Enabled = true;
                    ddlState.Enabled = true;
                    txtSMSProfileID.Enabled = true;
                    txtSMSAuthKey.Enabled = true;
                }
                else
                {
                    txtCompany.Enabled = false;
                    ddlCity.Enabled = false;
                    ddlState.Enabled = false;
                    //txtSMSProfileID.Enabled = false;
                    //txtSMSAuthKey.Enabled = false;
                }

                this.Populate(0);
                txtCompany.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            bool validGSTIN = Convert.ToBoolean((hidValidGSTIN.Value == null || hidValidGSTIN.Value == "") ? "true" : hidValidGSTIN.Value);
            if (!validGSTIN) return;
            string strMsg = string.Empty;
            CompanyMastDAL objCompanyMastDAL = new CompanyMastDAL();
            Int64 intWorkCompIdno = 0;
            string CompName = txtCompany.Text.Trim();
            string CompDesc = txtCompDescription.Text.Trim().Replace("'", "");
            string Address1 = txtAdrs1.Text.Trim();
            string Address2 = txtAdrs2.Text.Trim();
            string CityIdno = Convert.ToString(ddlCity.SelectedItem.Text);
            string StateIdno = Convert.ToString(ddlState.SelectedItem.Text);
            string PinNo = Convert.ToString(txtpincode.Text.Trim());
            int iTotLoc = (Convert.ToString(txtTotLoc.Text.Trim()) == "" ? 0 : Convert.ToInt32(txtTotLoc.Text));
            string TinNo = txtTinNo.Text.Trim();
            string EmailId = txtemailid.Text.Trim();
            string CSTNo = txtCSTNo.Text.Trim();
            string TanNo = txtTanNo.Text.Trim();
            string PanNo = txtPanNo.Text.Trim();
            string FaxNo = txtFaxNo.Text.Trim();
            string CodeNo = txtCodeNo.Text.Trim();
            string ContactPN1 = txtCntPrsnNm1.Text.Trim();
            string Mobile1 = txtCntPrsnNo1.Text.Trim();
            string EmailID1 = txtCntPrsnEmail1.Text.Trim();
            string ContactPN2 = txtCntPrsnNm2.Text.Trim();
            string Mobile2 = txtCntPrsnNo2.Text.Trim();
            string EmailID2 = txtCntPrsnEmail2.Text.Trim();
            string OwnMobileNum = txtMobileNumber.Text.Trim();
            string PhoneNumber = txtMobile.Text.Trim();
            string PropStatus = txtPStatus.Text.Trim();
            string SMS_UserName = txtSMSUserName.Text.Trim();
            string SMS_Password = txtSMSPassword.Text.Trim();
            string SMS_SenderID = txtSMSSenderID.Text.Trim();
            string SMS_ProfileID = txtSMSProfileID.Text.Trim();
            string SMS_AuthType = txtSMSAuthType.Text.Trim();
            string SMS_AuthKey = txtSMSAuthKey.Text.Trim();

            string ServTaxNo = txtServTaxNo.Text.Trim();
            string SapNo = txtSapNo.Text.Trim();
            string GSTTINNo = string.IsNullOrEmpty(Convert.ToString(txtGSTIN.Text.Trim())) ? "" : Convert.ToString(txtGSTIN.Text.Trim());
            if (txtGSTIN.Text.Trim()!="")
                if (GSTTINNo.Length != 15)
                {
                    ShowMessageErr("GST Number must 15 digit!");
                    return;
                }

            string RegNo = txtRegNo.Text.Trim();
            #region Comments...
            //Int64 CompanyType = ddlCompanyType.SelectedIndex;
            //string StateIdno = Convert.ToString(ddlState.SelectedValue);
            //  Int64 CityIdno = Convert.ToInt64(ddlCity.SelectedValue);
            //Int64 StateIdno = Convert.ToInt64(ddlState.SelectedValue);
            //  int Pincode = Convert.ToInt32(txtpincode.Text.Trim());
            // bool Active = Convert.ToBoolean(ChkActive.Checked);
            //if ((StdCode == "" || StdNo == "")== true && (Mobile == "")== true)
            //{
            //    strMsg = "Please Fill Phone No. Or Mobile ";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            //    return;
            //}
            //string Website = txtwebsite.Text.Trim();
            //string Mobile = txtMobile.Text.Trim();
            //string CinNo = txtCINNo.Text.Trim();
            //string CPName1 = txtCntPrsnNm1.Text.Trim();
            //string CPMob1 = txtCntPrsnNo1.Text.Trim();
            //string CPEmail1 = txtCntPrsnEmail1.Text.Trim();
            //string CPName2 = txtCntPrsnNm2.Text.Trim();
            //string CPMob2 = txtCntPrsnNo2.Text.Trim();
            //string CPEmail2 = txtCntPrsnEmail2.Text.Trim();
            //if (string.IsNullOrEmpty(hidcompidno.Value) == true)
            //{

            // intWorkCompIdno = objCompanyMastDAL.Insert(CompName, CompanyType, Address1, Address2, Pincode, EmailId, Website, Mobile, StateIdno, CityIdno, TinNo, TanNo, CSTNo, PanNo, CinNo, CPName1, CPMob1, CPEmail1, CPName2, CPMob2, CPEmail2, Active);
            // intWorkCompIdno = objCompanyMastDAL.Insert(CompName, Address1, Address2, CityIdno, StateIdno, PinNo, TinNo, EmailId, CSTNo, TanNo, PanNo, FaxNo, CodeNo, ContactPN1, ContactPN2, Mobile1, Mobile2, EmailID1, EmailID2, Active);
            //}
            //else
            //{
            //intWorkCompIdno = objCompanyMastDAL.Update(CompanyName, CompanyType, Address1, Address2, Pincode, EmailId, Website, Mobile, StateIdno, CityIdno, TinNo, TanNo, CSTNo, PanNo, CinNo, CPName1, CPMob1, CPEmail1, CPName2, CPMob2, CPEmail2, Active, Convert.ToInt32(hidcompidno.Value));
            //intWorkCompIdno = objCompanyMastDAL.Update(CompName, Address1, Address2, CityIdno, StateIdno, PinNo, TinNo, EmailId, CSTNo, TanNo, PanNo, FaxNo, CodeNo, ContactPN1, ContactPN2, Mobile1, Mobile2, EmailID1, EmailID2, Active, Convert.ToInt32(hidcompidno.Value));            
            #endregion

            intWorkCompIdno = objCompanyMastDAL.Update(CompName, CompDesc, Address1, Address2, PinNo, TinNo, EmailId, CSTNo, TanNo, PanNo, FaxNo, CodeNo, ContactPN1, ContactPN2, Mobile1, Mobile2, EmailID1, EmailID2, Active, Convert.ToInt32(hidcompidno.Value), iTotLoc, OwnMobileNum, PhoneNumber, SMS_UserName, SMS_Password, SMS_SenderID, SMS_ProfileID, SMS_AuthType, SMS_AuthKey, ServTaxNo, SapNo, RegNo, GSTTINNo, PropStatus);
            //}
            objCompanyMastDAL = null;
            if (intWorkCompIdno > 0)
            {
                if (string.IsNullOrEmpty(hidcompidno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                this.Populate(0);
                //this.ClearControls();
            }
            else if (intWorkCompIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                if (string.IsNullOrEmpty(hidcompidno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                //else
                //{
                //    strMsg = "Record not saved.";
                //}
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            txtCompany.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidcompidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidcompidno.Value));
            }

        }
        #endregion
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #region Miscellaneous Events...
        private void BindState()
        {
            BindDropdownDAL BindDropdownDAL = new BindDropdownDAL();
            var lst = BindDropdownDAL.BindState();
            //.SelectState();
            BindDropdownDAL = null;
            ddlState.DataSource = lst;
            //ddlState.DataTextField = "Name";
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("< Choose State >", "0"));
        }

        private void BindCity()
        {
            BindDropdownDAL BindDropdownDAL = new BindDropdownDAL();
            var lst = BindDropdownDAL.BindCity(Convert.ToInt64(ddlState.SelectedValue));
            BindDropdownDAL = null;
            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("< Choose City >", "0"));
        }

        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="WorkCompIdno"></param>
        private void Populate(int CompIdno)
        {
            CompanyMastDAL objCompanyMastDAL = new CompanyMastDAL();
            var objRelationMast = objCompanyMastDAL.SelectById(CompIdno);
            objCompanyMastDAL = null;
            if (objRelationMast != null)
            {
                //Comp_Name	Adress1	Adress2	State_Idno	City_Idno	Pin_No	Comp_Mail	Phone_Off	TIN_NO	CST_No	Pan_No	Tan_No	Fax_No	Code_No
                // Trad_Cert Comp_Jurs	Contact_PN1	Contact_PN2	Mobile_1	Mobile_2	Email_ID_1	Email_ID_2	Status	Date_Added	Date_Modified

                txtCompany.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Comp_Name)) ? "" : Convert.ToString(objRelationMast.Comp_Name);
                txtCompDescription.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.CompDescription)) ? "" : Convert.ToString(objRelationMast.CompDescription);
                txtAdrs1.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Adress1)) ? "" : Convert.ToString(objRelationMast.Adress1);
                txtAdrs2.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Adress2)) ? "" : Convert.ToString(objRelationMast.Adress2);
                //ddlState.SelectedItem.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.State_Idno)) ? "" : Convert.ToString(objRelationMast.State_Idno);
                //ddlCity.SelectedItem.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.City_Idno)) ? "" : Convert.ToString(objRelationMast.City_Idno);
                //ddlState.SelectedItem.Value = string.IsNullOrEmpty(Convert.ToString(objRelationMast.State_Idno)) ? "" : Convert.ToString(objRelationMast.State_Idno);
                ddlState.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objRelationMast.State_Idno)) ? "" : Convert.ToString(objRelationMast.State_Idno);
                ddlState_SelectedIndexChanged(null, null);
                ddlCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objRelationMast.City_Idno)) ? "" : Convert.ToString(objRelationMast.City_Idno);
                txtpincode.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Pin_No)) ? "" : Convert.ToString(objRelationMast.Pin_No);
                txtemailid.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Comp_Mail)) ? "" : Convert.ToString(objRelationMast.Comp_Mail);
                txtMobile.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Phone_Off)) ? "" : Convert.ToString(objRelationMast.Phone_Off).Trim();
                txtTinNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.TIN_NO)) ? "" : Convert.ToString(objRelationMast.TIN_NO);
                txtCSTNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.CST_No)) ? "" : Convert.ToString(objRelationMast.CST_No);
                txtPanNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Pan_No)) ? "" : Convert.ToString(objRelationMast.Pan_No);
                txtTanNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Tan_No)) ? "" : Convert.ToString(objRelationMast.Tan_No);
                txtFaxNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Fax_No)) ? "" : Convert.ToString(objRelationMast.Fax_No);
                txtCodeNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Code_No)) ? "" : Convert.ToString(objRelationMast.Code_No);
                txtCntPrsnNm1.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Contact_PN1)) ? "" : Convert.ToString(objRelationMast.Contact_PN1);
                txtCntPrsnNo1.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Mobile_1)) ? "" : Convert.ToString(objRelationMast.Mobile_1).Trim();
                txtCntPrsnEmail1.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Email_ID_1)) ? "" : Convert.ToString(objRelationMast.Email_ID_1);
                txtCntPrsnNm2.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Contact_PN2)) ? "" : Convert.ToString(objRelationMast.Contact_PN2);
                txtCntPrsnNo2.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Mobile_2)) ? "" : Convert.ToString(objRelationMast.Mobile_2).Trim();
                txtCntPrsnEmail2.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Email_ID_2)) ? "" : Convert.ToString(objRelationMast.Email_ID_2);
                txtTotLoc.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Tot_Loc)) ? "1" : Convert.ToString(objRelationMast.Tot_Loc);
                txtMobileNumber.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Mobile_own)) ? "" : Convert.ToString(objRelationMast.Mobile_own);
                hidcompidno.Value = Convert.ToString(objRelationMast.Comp_Idno);
                txtPStatus.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.PStatus)) ? "" : Convert.ToString(objRelationMast.PStatus);
                txtSMSUserName.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_UserName)) ? "" : Convert.ToString(objRelationMast.SMS_UserName);
                txtSMSPassword.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_Password)) ? "" : Convert.ToString(objRelationMast.SMS_Password);
                txtSMSSenderID.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_SenderID)) ? "" : Convert.ToString(objRelationMast.SMS_SenderID);
                txtSMSProfileID.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_ProfileID)) ? "" : Convert.ToString(objRelationMast.SMS_ProfileID);
                txtSMSAuthType.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_AuthType)) ? "" : Convert.ToString(objRelationMast.SMS_AuthType);
                txtSMSAuthKey.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SMS_AuthKey)) ? "" : Convert.ToString(objRelationMast.SMS_AuthKey);

                txtServTaxNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.ServTaxNo)) ? "" : Convert.ToString(objRelationMast.ServTaxNo);
                txtSapNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.SAP_No)) ? "" : Convert.ToString(objRelationMast.SAP_No);
                txtRegNo.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Reg_No)) ? "" : Convert.ToString(objRelationMast.Reg_No);
                txtGSTIN.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.CompGSTIN_No)) ? "" : Convert.ToString(objRelationMast.CompGSTIN_No);
                txtCompany.Focus();

                // ChkActive.Checked = Convert.ToBoolean(objRelationMast.Status);
                // ddlCompanyType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Company_Type)) ? "0" : Convert.ToString(objRelationMast.Company_Type);
                //  ddlCompanyType_SelectedIndexChanged(null, null);
                // txtwebsite.Text = string.IsNullOrEmpty(Convert.ToString(objRelationMast.Website)) ? "" : Convert.ToString(objRelationMast.Website);
                //  ddlState.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objRelationMast.State_Idno)) ? "" : Convert.ToString(objRelationMast.State_Idno);
                //   ddlState_SelectedIndexChanged(null, null);
                // ddlCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objRelationMast.City_Idno)) ? "" : Convert.ToString(objRelationMast.City_Idno);

            }
        }



        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtCompany.Text = string.Empty;
            //  ddlCompanyType.SelectedIndex = 0;
            txtAdrs1.Text = string.Empty;
            txtAdrs2.Text = string.Empty;
            txtpincode.Text = string.Empty;
            txtemailid.Text = string.Empty;
            //    txtwebsite.Text = string.Empty;
            txtMobile.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            txtTinNo.Text = string.Empty;
            txtTanNo.Text = string.Empty;
            txtCSTNo.Text = string.Empty;
            txtPanNo.Text = string.Empty;
            txtCodeNo.Text = string.Empty;
            txtFaxNo.Text = string.Empty;
            txtTotLoc.Text = "1";
            txtCntPrsnNm1.Text = string.Empty;
            txtCntPrsnNo1.Text = string.Empty;
            txtCntPrsnEmail1.Text = string.Empty;
            txtCntPrsnNm2.Text = string.Empty;
            txtCntPrsnNo2.Text = string.Empty;
            txtCntPrsnEmail2.Text = string.Empty;
            //ddlState.SelectedIndex = 0;
            //ddlCity.SelectedIndex = 0;
            //  ChkActive.Checked = true;
            hidcompidno.Value = string.Empty;
            txtSMSUserName.Text = string.Empty;
            txtSMSSenderID.Text = string.Empty;
            txtSMSAuthType.Text = string.Empty;
            txtCompany.Focus();
            txtRegNo.Text = string.Empty;
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
            {
                this.BindCity();
            }
            ddlCity.Focus();
        }

        //protected void ddlCompanyType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCompanyType.SelectedValue == "2")
        //    {
        //        divCINNo.Visible = true;
        //    }
        //    else
        //    {
        //        divCINNo.Visible = false;
        //    }
        //    txtCINNo.Text = string.Empty;
        //}

        #endregion
    }
}