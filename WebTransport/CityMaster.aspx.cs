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
    public partial class CityMaster : Pagebase
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
                txtCityName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtAbbreviation.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                chkStatus.Checked = true;
                this.BindState();
                if (Request.QueryString["CityIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["CityIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                drpState.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            CityMastDAL objclsCityMaster = new CityMastDAL();
            Int64 intCityIdno = 0;
            if (!chkLocation.Checked)
            {
                txtGSTIN_No.Text = String.Empty;
            }
            if (string.IsNullOrEmpty(hidcityidno.Value) == true)
            {
                intCityIdno = objclsCityMaster.InsertCityMaster(txtCityName.Text.Trim(), txtNameHindi.Text.Trim(), txtAbbreviation.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToBoolean(chkStatus.Checked), Convert.ToBoolean(chkLocation.Checked), empIdno, txtGSTIN_No.Text, txtCodeSap.Text, txtSACCode.Text, txtadd1.Text, txtadd2.Text);
            }
            else
            {
                intCityIdno = objclsCityMaster.UpdateCityMaster(txtCityName.Text.Trim(), txtNameHindi.Text.Trim(), txtAbbreviation.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToInt32(hidcityidno.Value), Convert.ToBoolean(chkStatus.Checked), Convert.ToBoolean(chkLocation.Checked), empIdno, txtGSTIN_No.Text, txtCodeSap.Text, txtSACCode.Text, txtadd1.Text, txtadd2.Text);
            }
            objclsCityMaster = null;

            if (intCityIdno > 0)
            {
                if (string.IsNullOrEmpty(hidcityidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                    lnkbtnNew.Visible = false;
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (intCityIdno == -2)
            {
                ShowMessageErr("Location limit defined in comapny Master is [ " + Convert.ToString(base.CompLocationLimit)+" ]");
            }
            else if (intCityIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidcityidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                   ShowMessageErr("Record not saved.");
                }
            }
            ClearControls(); 
            drpState.Focus();
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CityMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidcityidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidcityidno.Value));
            }

        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="CityIdno"></param>
        private void Populate(int CityIdno)
        {
            CityMastDAL objclsCityMaster = new CityMastDAL();
            var objCityMast = objclsCityMaster.SelectById(CityIdno);
            objclsCityMaster = null;
            if (objCityMast != null)
            {
                drpState.SelectedValue = Convert.ToString(objCityMast.State_Idno);
                txtCityName.Text = Convert.ToString(objCityMast.City_Name);
                txtGSTIN_No.Text = Convert.ToString(objCityMast.GSTIN_No == null ? "" : objCityMast.GSTIN_No);
                txtNameHindi.Text = Convert.ToString(objCityMast.CityName_Hindi);
                txtAbbreviation.Text = Convert.ToString(objCityMast.City_Abbr);
                chkStatus.Checked = Convert.ToBoolean(objCityMast.Status);
                hidcityidno.Value = Convert.ToString(objCityMast.City_Idno);
                chkLocation.Checked = Convert.ToBoolean(objCityMast.AsLocation);
                txtSACCode.Text = Convert.ToString(objCityMast.sac_Code == null ? "" : objCityMast.sac_Code);
                txtCodeSap.Text = Convert.ToString(objCityMast.Code_sap == null ? "" : objCityMast.Code_sap);
                txtadd1.Text = Convert.ToString(objCityMast.Address1 == null ? "" : objCityMast.Address1);
                txtadd2.Text = Convert.ToString(objCityMast.Address2 == null ? "" : objCityMast.Address2);
                drpState.Focus();
            }
        }

        /// <summary>
        /// To Bind State DropDown
        /// </summary>
        private void BindState()
        {
            CityMastDAL objclsCityMaster = new CityMastDAL();
            var objCityMast = objclsCityMaster.SelectState();
            objclsCityMaster = null;
            drpState.DataSource = objCityMast;
            drpState.DataTextField = "State_Name";
            drpState.DataValueField = "State_Idno";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }

        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            drpState.SelectedValue = "0";
            txtCityName.Text = txtAbbreviation.Text = string.Empty;
            chkStatus.Checked = true;
            chkLocation.Checked = false;
            txtNameHindi.Text = "";
            hidcityidno.Value = string.Empty;
            txtGSTIN_No.Text = string.Empty;
            drpState.Focus();
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion
    }
}