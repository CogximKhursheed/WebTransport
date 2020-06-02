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
    public partial class NarrationMast : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 24;
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
                txtNarration.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtDescription.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["NarrIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["NarrIdno"]));
                    txtNarration.Focus();
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtNarration.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("NarrationMast.aspx"); 
        }


        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            NarrationMastDAL objclsNarrationMast = new NarrationMastDAL();
            Int64 intNarrIdno = 0;
            if (string.IsNullOrEmpty(hidnarrationidno.Value) == true)
            {
                intNarrIdno = objclsNarrationMast.Insert(txtNarration.Text.Trim(), txtDescription.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intNarrIdno = objclsNarrationMast.Update(txtNarration.Text.Trim(), txtDescription.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidnarrationidno.Value), empIdno);
            }
            objclsNarrationMast = null;

            if (intNarrIdno > 0)
            {
                if (string.IsNullOrEmpty(hidnarrationidno.Value) == false)
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
            else if (intNarrIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidnarrationidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            } 
            txtNarration.Focus();
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidnarrationidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidnarrationidno.Value));
                lnkbtnNew.Visible = true;
            }

        }

        #endregion

        #region Miscellaneous Events...

        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="RateIdno"></param>
        private void Populate(int NarrIdno)
        {
            NarrationMastDAL objclsNarrationMast = new NarrationMastDAL();
            var objNarrationMast = objclsNarrationMast.SelectById(NarrIdno);
            objclsNarrationMast = null;
            if (objNarrationMast != null)
            {
                txtNarration.Text = Convert.ToString(objNarrationMast.Narr_Name);
                txtDescription.Text = Convert.ToString(objNarrationMast.Narr_Desc);
                chkStatus.Checked = Convert.ToBoolean(objNarrationMast.Status);
                hidnarrationidno.Value = Convert.ToString(objNarrationMast.Narr_Idno);
                txtNarration.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtNarration.Text = txtDescription.Text = string.Empty;
            chkStatus.Checked = true;
            hidnarrationidno.Value = string.Empty;
            txtNarration.Focus();
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