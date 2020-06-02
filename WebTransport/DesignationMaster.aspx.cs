using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.IO;
using System.Transactions;

namespace WebTransport
{
    public partial class DesignationMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 17;
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
                txtDesignation.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["DesigIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["DesigIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtDesignation.Focus();
            }
        }
        #endregion 

        #region Button Events...
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("DesignationMaster.aspx");
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            DesignationMasterDAL objDesignationMasterDAL = new DesignationMasterDAL();
            Int64 intDesignIdno = 0;
            Int64 intDesignRightsIdno = 0;
            if (string.IsNullOrEmpty(hiddesignidno.Value) == true)
            {
                intDesignIdno = objDesignationMasterDAL.Insert(txtDesignation.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
                if (intDesignIdno > 0)
                {
                    intDesignRightsIdno = objDesignationMasterDAL.InsertIntotblDesigRightss(intDesignIdno, empIdno);
                }
            }
            else
            {
                intDesignIdno = objDesignationMasterDAL.Update(txtDesignation.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hiddesignidno.Value), empIdno);
            }
            objDesignationMasterDAL = null;
            if (intDesignIdno > 0)
            {
                if (string.IsNullOrEmpty(hiddesignidno.Value) == false)
                {
                    lnkbtnNew.Visible = false;
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                    lblAlert.Text = "Do you want to assign rights for this designation ?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "ShowDesigRights()", true);
                }
                this.ClearControls();
            }
            else if (intDesignIdno < 0)
            {
                ShowMessageErr("Record already exists!");
            }
            else
            {
                if (string.IsNullOrEmpty(hiddesignidno.Value) == false)
                {
                    ShowMessageErr("Record not updated!");
                }
                else
                {
                   ShowMessageErr("Record not saved!");
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            txtDesignation.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddesignidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hiddesignidno.Value));
            }
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            DesignationMasterDAL objDesignationMasterDAL = new DesignationMasterDAL();
            Int64 intDesignIdno = 0;
            intDesignIdno = objDesignationMasterDAL.SelectDesigIdno();
            Response.Redirect("DesigRights.aspx?DesigIdno=" + intDesignIdno, true);
        }

        protected void BtnCancelUser_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "HideClient()", true);
        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="DesignIdno"></param>
        private void Populate(int DesignIdno)
        {
            DesignationMasterDAL objDesignationBLL = new DesignationMasterDAL();
            var objDesigMast = objDesignationBLL.SelectById(DesignIdno);
            objDesignationBLL = null;
            if (objDesigMast != null)
            {
                txtDesignation.Text = Convert.ToString(objDesigMast.Desig_Name);
                chkStatus.Checked = Convert.ToBoolean(objDesigMast.Status);
                hiddesignidno.Value = Convert.ToString(objDesigMast.Desig_Idno);
                txtDesignation.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtDesignation.Text = string.Empty;
            chkStatus.Checked = true;
            hiddesignidno.Value = string.Empty;
            txtDesignation.Focus();
        }
        #endregion

        #region Function...
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