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
    public partial class PetrolPumpMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 19;
        #endregion

        #region PageLaod Events...
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
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                txtPumpName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtDesignation.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtLadlineCode.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtLadlineNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtMobileNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                chkStatus.Checked = true;
                this.BindPCompName();
                this.BindState();
                //this.BindCity();
                if (Request.QueryString["PPumpIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["PPumpIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtPumpName.Focus();

                if (Session["Message"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + Session["Message"] + "')", true);
                    Session["Message"] = null;
                }
            }

        }
        #endregion

        #region drpState_SelectedIndexChanged
        protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCityDDL(Convert.ToInt64(drpState.SelectedValue));
            ddlCity.Focus();
        }
        #endregion

        #region Button Event....

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            Int64 intPPumpIdno = 0;
            if (string.IsNullOrEmpty(hidPPumpidno.Value) == true)
            {
                intPPumpIdno = objclsPetrolPumpMaster.InsertPPumpMaster(txtPumpName.Text.Trim(), Convert.ToInt64(drpPetrolCompany.SelectedValue), txtPersonName.Text.Trim(), txtDesignation.Text.Trim(), txtLadlineCode.Text.Trim(), txtLadlineNo.Text.Trim(), txtMobileNo.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intPPumpIdno = objclsPetrolPumpMaster.UpdatePPumpMaster(txtPumpName.Text.Trim(), Convert.ToInt64(drpPetrolCompany.SelectedValue), txtPersonName.Text.Trim(), txtDesignation.Text.Trim(), txtLadlineCode.Text.Trim(), txtLadlineNo.Text.Trim(), txtMobileNo.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt32(hidPPumpidno.Value), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            objclsPetrolPumpMaster = null;

            if (intPPumpIdno > 0)
            {
                if (string.IsNullOrEmpty(hidPPumpidno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    strMsg = "Record saved successfully.";
                }
                this.ClearControls();

                SuccessfulMessage(strMsg);
            }
            else if (intPPumpIdno < 0)
            {
                strMsg = "Record already exists.";

                ShowMessageErr(strMsg);
            }
            else
            {
                if (string.IsNullOrEmpty(hidPPumpidno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                else
                {
                    strMsg = "Record not saved.";
                }

                ShowMessageErr(strMsg);
            }

        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PetrolPumpMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidPPumpidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidPPumpidno.Value));
            }
        }
        #endregion

        #region Miscellaneous Events...



        private void Populate(int PPumpIdno)
        {
            PetrolPumpMasterDAL objclsPetrolPumpMast = new PetrolPumpMasterDAL();
            var objPetrolPumpMast = objclsPetrolPumpMast.SelectById(PPumpIdno);
            objclsPetrolPumpMast = null;
            if (objPetrolPumpMast != null)
            {
                drpState.SelectedValue = Convert.ToString(objPetrolPumpMast.PPump_State);
                drpState_SelectedIndexChanged(null, null);
                drpPetrolCompany.SelectedValue = Convert.ToString(objPetrolPumpMast.PComp_Idno);
                ddlCity.SelectedValue = Convert.ToString(objPetrolPumpMast.PPump_City);
                txtPumpName.Text = Convert.ToString(objPetrolPumpMast.PPump_Name);
                txtPersonName.Text = Convert.ToString(objPetrolPumpMast.PPumpPerson_Name);
                txtDesignation.Text = Convert.ToString(objPetrolPumpMast.PPumpPerson_Designation);
                txtLadlineCode.Text = Convert.ToString(objPetrolPumpMast.PPump_LadlineCode);
                txtLadlineNo.Text = Convert.ToString(objPetrolPumpMast.PPump_Ladlineno);
                txtMobileNo.Text = Convert.ToString(objPetrolPumpMast.PPump_Mobileno);
                txtAddress1.Text = Convert.ToString(objPetrolPumpMast.PPump_Address1);
                txtAddress2.Text = Convert.ToString(objPetrolPumpMast.PPump_Address2);
                chkStatus.Checked = Convert.ToBoolean(objPetrolPumpMast.Status);
                hidPPumpidno.Value = Convert.ToString(objPetrolPumpMast.PPump_Idno);

                drpState.Focus();
            }
        }

        private void ClearControls()
        {
            // drpState.SelectedValue = "0";
            txtPumpName.Text = txtDesignation.Text =   txtPersonName.Text=txtLadlineCode.Text = txtLadlineNo.Text = txtMobileNo.Text = txtAddress1.Text = txtAddress2.Text = string.Empty;
            chkStatus.Checked = true;
            hidPPumpidno.Value = string.Empty;
            drpState.Focus();
            drpPetrolCompany.SelectedIndex = 0;
            lnkbtnNew.Visible = false;
        }


        private void BindState()
        {
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            var objPetrolPumpMaster = objclsPetrolPumpMaster.SelectState();
            objclsPetrolPumpMaster = null;
            drpState.DataSource = objPetrolPumpMaster;
            drpState.DataTextField = "State_Name";
            drpState.DataValueField = "State_Idno";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }

        private void BindCityDDL(Int64 intStateIdno)
        {
            BindDropdownDAL objClsBindDropDown = new BindDropdownDAL();
            var lst = objClsBindDropDown.BindCity(intStateIdno);
            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem(" ----Select----", "0"));
        }

        private void BindPCompName()
        {
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            var objPetrolPumpMaster = objclsPetrolPumpMaster.SelectPCompName();
            objclsPetrolPumpMaster = null;
            drpPetrolCompany.DataSource = objPetrolPumpMaster;
            drpPetrolCompany.DataTextField = "PComp_Name";
            drpPetrolCompany.DataValueField = "PComp_Idno";
            drpPetrolCompany.DataBind();
            drpPetrolCompany.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
     
        private void SuccessfulMessage(string strMsg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        #endregion
    }
}