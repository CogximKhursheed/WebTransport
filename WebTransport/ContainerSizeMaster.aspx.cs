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
    public partial class ContainerSizeMaster : Pagebase
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
                txtConSize.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                txtConSize.Focus();
                if (Request.QueryString["ConSize_Idno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["ConSize_Idno"]));
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
            ContainerSizeMasterDAL objclsConSizeMaster = new ContainerSizeMasterDAL();
            Int64 intConSizeIdno = 0;
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                intConSizeIdno = objclsConSizeMaster.Insert(txtConSize.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intConSizeIdno = objclsConSizeMaster.Update(txtConSize.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidIGrpMastidno.Value), empIdno);
            }
            objclsConSizeMaster = null;
            if (intConSizeIdno > 0)
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
            else if (intConSizeIdno < 0)
            {
                ShowMessageErr("Record already exists!");
            }
            else
            {
                if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == false)
                {
                    ShowMessageErr("Record not updated!");
                }
                else
                {
                    ShowMessageErr("Record not saved!");
                }
            } 
            lnkbtnNew.Visible = false;
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ContainerSizeMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidIGrpMastidno.Value));
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
        /// <summary>
        /// To Clear Feilds
        /// </summary>
        private void ClearControls()
        {
            txtConSize.Text = string.Empty;
            hidIGrpMastidno.Value = string.Empty;
        }

        /// <summary>
        /// To Populate Data
        /// </summary>
        /// <param name="ColrIdno"></param>
        private void Populate(int IConSizeIdno)
        {
            ContainerSizeMasterDAL objclsConSizeMaster = new ContainerSizeMasterDAL();
            var objIgrpMast = objclsConSizeMaster.SelectById(IConSizeIdno);
            objclsConSizeMaster = null;
            if (objIgrpMast != null)
            {
                txtConSize.Text = Convert.ToString(objIgrpMast.Container_Size);
                chkStatus.Checked = Convert.ToBoolean(objIgrpMast.Status);
                hidIGrpMastidno.Value = Convert.ToString(objIgrpMast.ContainerSize_Idno);
            }
        }

        #endregion

    }
}