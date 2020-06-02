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
    public partial class PetrolCompanyMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 35;
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
                txtPCompanyName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");

                chkStatus.Checked = true;

                if (Request.QueryString["PCompIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["PCompIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtPCompanyName.Focus();

                if (Session["Message"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + Session["Message"].ToString() + "');", true);
                    Session["Message"] = null;

                }

            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            PetrolCompanyMasterDAL objclsPetrolCompanyMasterDAL = new PetrolCompanyMasterDAL();
            Int64 intCityIdno = 0;
            if (string.IsNullOrEmpty(hidPCompIdno.Value) == true)
            {
                intCityIdno = objclsPetrolCompanyMasterDAL.InsertPCompanyMaster(txtPCompanyName.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intCityIdno = objclsPetrolCompanyMasterDAL.UpdatePCompanyMaster(txtPCompanyName.Text.Trim(), Convert.ToInt32(hidPCompIdno.Value), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            objclsPetrolCompanyMasterDAL = null;

            if (intCityIdno > 0)
            {
                if (string.IsNullOrEmpty(hidPCompIdno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    strMsg = "Record saved successfully.";
                }
                this.ClearControls();
            }
            else if (intCityIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                if (string.IsNullOrEmpty(hidPCompIdno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                else
                {
                    strMsg = "Record not saved.";
                }
            }

            Session["Message"] = strMsg.ToString();

            Response.Redirect("PetrolCompanyMaster.aspx");
        }


        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidPCompIdno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidPCompIdno.Value) == 0 ? 0 : Convert.ToInt32(hidPCompIdno.Value));
              
            }

        }

        #endregion

        #region Miscellaneous Events...
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void Populate(int PCompIdno)
        {
            PetrolCompanyMasterDAL objclsPetrolCompanyMaster = new PetrolCompanyMasterDAL();
            var objPetrolCompanyMaster = objclsPetrolCompanyMaster.SelectById(PCompIdno);
            objclsPetrolCompanyMaster = null;
            if (objPetrolCompanyMaster != null)
            {
                txtPCompanyName.Text = Convert.ToString(objPetrolCompanyMaster.PComp_Name);
                chkStatus.Checked = Convert.ToBoolean(objPetrolCompanyMaster.Status);
                hidPCompIdno.Value = Convert.ToString(objPetrolCompanyMaster.PComp_Idno);
                txtPCompanyName.Focus();
            }
        }
        private void ClearControls()
        {
            // drpState.SelectedValue = "0";
            txtPCompanyName.Text = string.Empty;
            chkStatus.Checked = true;
            hidPCompIdno.Value = string.Empty;
            txtPCompanyName.Focus();
        }
        #endregion

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        { 
            Response.Redirect("PetrolCompanyMaster.aspx");
        }

    }
}