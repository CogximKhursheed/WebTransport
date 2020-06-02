using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Account;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class GradeMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 9;
        #endregion

        #region PageLoad Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    lnkbtnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}
                txtGrdName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtAbbreviation.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                txtGrdName.Focus();
                if (Request.QueryString["Grade_Idno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["Grade_Idno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
            }
        }
        #endregion

        #region Button Event...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            GradeMasterDAL objclsGradeMaster = new GradeMasterDAL();
            Int64 intCategoryIdno = 0;
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                intCategoryIdno = objclsGradeMaster.Insert(txtGrdName.Text.Trim(),txtAbbreviation.Text.Trim(), Convert.ToBoolean(chkStatus.Checked));
            }
            else
            {
                intCategoryIdno = objclsGradeMaster.Update(txtGrdName.Text.Trim(),txtAbbreviation.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidIGrpMastidno.Value));
            }
            objclsGradeMaster = null;
            if (intCategoryIdno > 0)
            {
                if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (intCategoryIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            } 
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("GradeMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidIGrpMastidno.Value) == 0 ? 0 : Convert.ToInt32(hidIGrpMastidno.Value));
              
            }
        }
        #endregion

        #region Miscellaneous Events...

        /// <summary>
        /// To Clear Feilds
        /// </summary>
        private void ClearControls()
        {
            lnkbtnNew.Visible = false;
            txtGrdName.Text = string.Empty;
            txtAbbreviation.Text = string.Empty;
            hidIGrpMastidno.Value = string.Empty;
        }

        /// <summary>
        /// To Populate Data
        /// </summary>
        /// <param name="ColrIdno"></param>
        private void Populate(int intIdno)
        {
            GradeMasterDAL objclsGradeMaster = new GradeMasterDAL();
            var objGMast = objclsGradeMaster.SelectById(intIdno);
            objclsGradeMaster = null;
            if (objGMast != null)
            {
                txtGrdName.Text = Convert.ToString(objGMast.Grade_Name);
                txtAbbreviation.Text = Convert.ToString(objGMast.Grade_Abbr);
                chkStatus.Checked = Convert.ToBoolean(objGMast.Status);
                hidIGrpMastidno.Value = Convert.ToString(objGMast.Grade_Idno);
            }
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