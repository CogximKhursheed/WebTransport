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
    public partial class DriverMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 21;
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
                txtDriverName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtDRVHindi.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                txtExpiryDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                Bindemp();
                if (Request.QueryString["DriverIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["DriverIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtDriverName.Focus();
            }
        }
        #endregion

        private void Bindemp()
        {
            DriverMastDAL objclsCityMaster = new DriverMastDAL();
            var objCityMast = objclsCityMaster.Selectemp();
            objclsCityMaster = null;
            Drpgurenter.DataSource = objCityMast;
            Drpgurenter.DataTextField = "Emp_Name";
            Drpgurenter.DataValueField = "User_Idno";
            Drpgurenter.DataBind();
            Drpgurenter.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }


        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            DriverMastDAL objDrivMast = new DriverMastDAL();
            int intDriverIdno = 0;
            if (string.IsNullOrEmpty(hidDrividno.Value) == true)
            {

                intDriverIdno = objDrivMast.Insert(txtDriverName.Text.Trim(), txtDRVHindi.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), txtlicense.Text.Trim(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtExpiryDate.Text)), Convert.ToBoolean(chkVarified.Checked), txtaccountno.Text.Trim(), Convert.ToInt64(Drpgurenter.SelectedValue), txtauthority.Text.Trim(), empIdno);
            }
            else
            {

                intDriverIdno = objDrivMast.Update(txtDriverName.Text.Trim(), txtDRVHindi.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidDrividno.Value), txtlicense.Text.Trim(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtExpiryDate.Text)), Convert.ToBoolean(chkVarified.Checked), txtaccountno.Text.Trim(), Convert.ToInt64(Drpgurenter.SelectedValue), txtauthority.Text.Trim(), empIdno);
            }
            objDrivMast = null;

            if (intDriverIdno > 0)
            {
                if (string.IsNullOrEmpty(hidDrividno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (intDriverIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidDrividno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            } 
            txtDriverName.Focus();
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidDrividno.Value) == true)
            {
                this.ClearControls(); 
            }
            else
            {
                this.Populate(Convert.ToInt32(hidDrividno.Value) == 0 ? 0 : Convert.ToInt32(hidDrividno.Value)); 
            } 
        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("DriverMaster.aspx"); 
        }
        #endregion

        #region Miscellaneous Events...

        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="ColrIdno"></param>
        private void Populate(int DriverIdno)
        {
            DriverMastDAL objDrivMast = new DriverMastDAL();
            var objDriv = objDrivMast.SelectById(DriverIdno);
            objDrivMast = null;
            if (objDriv != null)
            {
                txtDriverName.Text = Convert.ToString(objDriv.Driver_Name);
                txtDRVHindi.Text = Convert.ToString(objDriv.DriverName_Hindi);
                chkStatus.Checked = Convert.ToBoolean(objDriv.Status);
                hidDrividno.Value = Convert.ToString(objDriv.Driver_Idno);
                txtlicense.Text = Convert.ToString(objDriv.License_No);
                txtExpiryDate.Text = string.IsNullOrEmpty(Convert.ToString(objDriv.Expiry_Date)) ? "" : Convert.ToDateTime(objDriv.Expiry_Date).ToString("dd-MM-yyyy");

                txtaccountno.Text = Convert.ToString(objDriv.Account_no);
                chkVarified.Checked = Convert.ToBoolean(objDriv.Varified);
                Drpgurenter.Text = Convert.ToString(objDriv.Guarantor);
                txtauthority.Text = Convert.ToString(objDriv.LicenseAuthority);
                txtDriverName.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtDriverName.Text = txtDRVHindi.Text = txtaccountno.Text = txtExpiryDate.Text = txtlicense.Text = txtauthority.Text = string.Empty;
            chkStatus.Checked = true;
            hidDrividno.Value = string.Empty;
            chkVarified.Checked = false;
            Drpgurenter.SelectedIndex = -1;
            lnkbtnNew.Visible = false;
            txtDriverName.Focus();
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