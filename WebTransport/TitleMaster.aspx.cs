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
    public partial class TitleMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 23;
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
                txtTitle.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                txtDesc.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["TitlIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["TitlIdno"]));
                    txtTitle.Focus();
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtTitle.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("TitleMaster.aspx"); 
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
            Int64 intTitleIdno = 0;
            if (string.IsNullOrEmpty(hidTitleidno.Value) == true)
            {
                intTitleIdno = objclsTitleMaster.Insert(txtTitle.Text.Trim(), txtDesc.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intTitleIdno = objclsTitleMaster.Update(txtTitle.Text.Trim(), txtDesc.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidTitleidno.Value), empIdno);
            }
            objclsTitleMaster = null;

            if (intTitleIdno > 0)
            {
                if (string.IsNullOrEmpty(hidTitleidno.Value) == false)
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
            else if (intTitleIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidTitleidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            } 
            txtTitle.Focus();
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidTitleidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidTitleidno.Value));
            }

        }
        #endregion

        #region Miscellaneous Events...

        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="TitleIdno"></param>
        private void Populate(int Titleidno)
        {
            TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
            var objTitleMast = objclsTitleMaster.SelectById(Titleidno);
            objclsTitleMaster = null;
            if (objTitleMast != null)
            {
                txtTitle.Text = Convert.ToString(objTitleMast.Titl_Name);
                txtDesc.Text = Convert.ToString(objTitleMast.Titl_Desc);
                chkStatus.Checked = Convert.ToBoolean(objTitleMast.Status);
                hidTitleidno.Value = Convert.ToString(objTitleMast.Titl_Idno);
                txtTitle.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtTitle.Text = txtDesc.Text = string.Empty;
            chkStatus.Checked = true;
            hidTitleidno.Value = string.Empty;
            txtTitle.Focus();
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

        #region ControlsEvents..
        protected void txtTitle_TextChanged(object sender, EventArgs e)
        {
            txtDesc.Text = txtTitle.Text; txtDesc.Focus();
        }
        #endregion

    }
}