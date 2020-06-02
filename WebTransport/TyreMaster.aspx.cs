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
    public partial class TyreMaster : Pagebase
    {
        #region Page_Load
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

                txtItemName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["TyreIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["TyreIdno"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtItemName.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int64 intTyreIdno = 0; string strMsg = string.Empty;
            TyreMastDAL objTyreMast = new TyreMastDAL();

            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                intTyreIdno = objTyreMast.Insert(txtItemName.Text.Trim(), Convert.ToBoolean(chkStatus.Checked));
            }
            else
            {
                intTyreIdno = objTyreMast.Update(txtItemName.Text.Trim(), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt64(hidItemidno.Value));
            }

            if (intTyreIdno > 0)
            {
                this.ClearControls();
                lnkbtnNew.Visible = false;
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }

            }
            else if (intTyreIdno < 0)
            {
                ShowMessageErr("Record already exists!");
            }
            else
            {
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    ShowMessageErr("Record not updated!");
                }
                else
                {
                    ShowMessageErr("Record not saved!");
                }
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("TyreMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidItemidno.Value) == 0 ? 0 : Convert.ToInt32(hidItemidno.Value));
            }
        }

        #endregion

        #region Functions....
        private void ClearControls()
        {
            txtItemName.Text = "";
            hidItemidno.Value = null; chkStatus.Checked = true;
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void Populate(Int32 TyreIdno)
        {
            TyreMastDAL objTyreMast = new TyreMastDAL();
            var objitmMast = objTyreMast.SelectById(TyreIdno);
            objTyreMast = null;
            if (objitmMast != null)
            {
                txtItemName.Text = Convert.ToString(objitmMast.TyreType_Name);
                chkStatus.Checked = Convert.ToBoolean(objitmMast.TyreType_Status);
                hidItemidno.Value = Convert.ToString(objitmMast.TyreType_Idno);
                txtItemName.Focus();
            }
        }


        #endregion
    }
}