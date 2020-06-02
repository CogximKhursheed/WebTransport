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
    public partial class ContainerTypeMaster : Pagebase
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
                txtConType.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                txtConType.Focus();
                if (Request.QueryString["ConType_Idno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["ConType_Idno"]));
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
            ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
            Int64 intConTypeIdno = 0;
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                intConTypeIdno = objclsConTypeMaster.Insert(txtConType.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intConTypeIdno = objclsConTypeMaster.Update(txtConType.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidIGrpMastidno.Value), empIdno);
            }
            objclsConTypeMaster = null;
            if (intConTypeIdno > 0)
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
            else if (intConTypeIdno < 0)
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
            lnkbtnNew.Visible = false;
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ContainerTypeMaster.aspx");
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
            txtConType.Text = string.Empty;
            hidIGrpMastidno.Value = string.Empty;
        }

        /// <summary>
        /// To Populate Data
        /// </summary>
        /// <param name="ColrIdno"></param>
        private void Populate(int IConSizeIdno)
        {
            ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
            var objConTypeMast = objclsConTypeMaster.SelectById(IConSizeIdno);
            objclsConTypeMaster = null;
            if (objConTypeMast != null)
            {
                txtConType.Text = Convert.ToString(objConTypeMast.Container_Type);
                chkStatus.Checked = Convert.ToBoolean(objConTypeMast.Status);
                hidIGrpMastidno.Value = Convert.ToString(objConTypeMast.ContainerType_Idno);
            }
        }

        #endregion 
    }
}