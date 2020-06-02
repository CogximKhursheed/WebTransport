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
    public partial class DistrictMaster: Pagebase
    {
        #region Variables declaration...
        private int intFormId = 63;
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
                txtDistrictName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtAbbreviation.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                chkStatus.Checked = true;
                this.BindState();

                if (Request.QueryString["DistrictIdno"] != null)
                {
                    this.PopulateList(Convert.ToInt32(Request.QueryString["DistrictIdno"]));
                    
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                lnkbtnNew.Visible = false;

                drpState.Focus();
               
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            string DistrictName =string.IsNullOrEmpty(txtDistrictName.Text.Trim()) ? string.Empty :  Convert.ToString(txtDistrictName.Text.Trim());
            if (string.IsNullOrEmpty(DistrictName)) { ShowMessageErr("Please Enter District."); return; }
            DistrictMastDAL obj = new DistrictMastDAL();
            Int64 intDistrictIdno = 0;
            if (string.IsNullOrEmpty(hiddistrictidno.Value) == true)
            {
                intDistrictIdno = obj.Insert(DistrictName, txtNameHindi.Text.Trim(), txtAbbreviation.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intDistrictIdno = obj.Update(txtDistrictName.Text.Trim(), txtNameHindi.Text.Trim(), txtAbbreviation.Text.Trim(), Convert.ToInt64(drpState.SelectedValue), Convert.ToInt32(hiddistrictidno.Value), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            obj = null;

            if (intDistrictIdno > 0)
            {
                if (string.IsNullOrEmpty(hiddistrictidno.Value) == false)
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
            
            else if (intDistrictIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hiddistrictidno.Value) == false)
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
            Response.Redirect("DistrictMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddistrictidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hiddistrictidno.Value));
            }

        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="DistrictIdno"></param>
        private void Populate(int DistrictIdno)
        {
            CityMastDAL objclsCityMaster = new CityMastDAL();
            var objCityMast = objclsCityMaster.SelectById(DistrictIdno);
            objclsCityMaster = null;
            if (objCityMast != null)
            {
                drpState.SelectedValue = Convert.ToString(objCityMast.State_Idno);
                txtDistrictName.Text = Convert.ToString(objCityMast.City_Name);
                txtNameHindi.Text = Convert.ToString(objCityMast.CityName_Hindi);
                txtAbbreviation.Text = Convert.ToString(objCityMast.City_Abbr);
                chkStatus.Checked = Convert.ToBoolean(objCityMast.Status);
                hiddistrictidno.Value = Convert.ToString(objCityMast.City_Idno);
                //chkLocation.Checked = Convert.ToBoolean(objCityMast.AsLocation);
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
            txtDistrictName.Text = txtAbbreviation.Text = string.Empty;
            chkStatus.Checked = true;
            //chkLocation.Checked = false;
            txtNameHindi.Text = "";
            hiddistrictidno.Value = string.Empty;
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
        private void PopulateList(int DistrictIdno)
        {
            DistrictMastDAL obj = new DistrictMastDAL();
            var objDistrictMast = obj.SelectById(DistrictIdno);
            //objDistrictMast = null;
            if (objDistrictMast != null)
            {
                drpState.SelectedValue = Convert.ToString(objDistrictMast.State_Idno);
                txtDistrictName.Text = Convert.ToString(objDistrictMast.District_Name);
                txtNameHindi.Text = Convert.ToString(objDistrictMast.District_Hindi);
                txtAbbreviation.Text = Convert.ToString(objDistrictMast.District_Abbr);
                chkStatus.Checked = Convert.ToBoolean(objDistrictMast.Status);
                hiddistrictidno.Value = Convert.ToString(objDistrictMast.District_Idno);

                drpState.Focus();
                lnkbtnCancel.Visible = false;
            }
        }
        #endregion
    }
}