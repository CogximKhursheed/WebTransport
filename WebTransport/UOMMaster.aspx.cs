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
    public partial class UOMMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 10;
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
                txtUOMName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtDescription.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["UOMIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["UOMIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtUOMName.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
            Int64 intUOMIdno = 0;
            if (string.IsNullOrEmpty(hiduomidno.Value) == true)
            {
                intUOMIdno = objclsUOMMaster.Insert(txtUOMName.Text.Trim(), txtNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intUOMIdno = objclsUOMMaster.Update(txtUOMName.Text.Trim(), txtNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hiduomidno.Value), empIdno);
            }
            objclsUOMMaster = null;

            if (intUOMIdno > 0)
            {
                if (string.IsNullOrEmpty(hiduomidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
                lnkbtnNew.Visible = false;
            }
            else if (intUOMIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hiduomidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            }
            txtUOMName.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiduomidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hiduomidno.Value));
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("UOMMaster.aspx");
        }
        #endregion

        #region Miscellaneous Events...

        private void Populate(int UOMIdno)
        {
            UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
            var objUOMMast = objclsUOMMaster.SelectById(UOMIdno);
            objclsUOMMaster = null;
            if (objUOMMast != null)
            {
                txtUOMName.Text = Convert.ToString(objUOMMast.UOM_Name);
                txtNameHindi.Text = Convert.ToString(objUOMMast.UOMName_Hindi);
                txtDescription.Text = Convert.ToString(objUOMMast.UOM_Desc);
                chkStatus.Checked = Convert.ToBoolean(objUOMMast.Status);
                hiduomidno.Value = Convert.ToString(objUOMMast.UOM_Idno);
                txtUOMName.Focus();
            }
        }

        private void ClearControls()
        {
            txtUOMName.Text = txtDescription.Text = string.Empty;
            chkStatus.Checked = true;
            txtNameHindi.Text = "";
            hiduomidno.Value = string.Empty;
            txtUOMName.Focus();
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

        #region Control Events...
        protected void txtUOMName_TextChanged(object sender, EventArgs e)
        {
            txtDescription.Text = txtUOMName.Text; txtNameHindi.Focus();
        }
        #endregion
    }
}