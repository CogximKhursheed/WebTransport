using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class TyreSizeMaster : Pagebase
    {
        #region Private Variables...
        private int intFormId = 17;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
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
                if (Request.QueryString["TyresizeID"] != null)
                {
                    hidTyresizeIdno.Value = Convert.ToString(Request.QueryString["TyresizeID"]);
                    this.Populate(Convert.ToInt64(Request.QueryString["TyresizeID"]));
                    lnkbtnNew.Visible = true;
                    lnkbtnSave.Text = "Update";
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("TyreSizeMaster.aspx");
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidTyresizeIdno.Value) == false)
            {
            }
            else
            {
                txttyresize.Text = ""; 
            }
        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Int64 Result = 0;
            TyreSizeDAL DAL = new TyreSizeDAL();
            if (string.IsNullOrEmpty(hidTyresizeIdno.Value) == false)
            {
                Result = DAL.Update(Convert.ToInt64(hidTyresizeIdno.Value), txttyresize.Text.Trim());
            }
            else
            {
                Result = DAL.Save(txttyresize.Text.Trim());
            }

            string strMsg = "";
            if (Result > 0)
            {
                if (string.IsNullOrEmpty(hidTyresizeIdno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (Result < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidTyresizeIdno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            }
            //ddltype.Focus();
        }
        #endregion

        #region Functions..

        public void Populate(Int64 TyresizeId)
        {
            TyreSizeDAL DAL = new TyreSizeDAL();
            var objTyreSizeMaster = DAL.SelectByID(TyresizeId);
            if (objTyreSizeMaster != null)
            {
                txttyresize.Text = Convert.ToString(objTyreSizeMaster.TyreSize);

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

        private void ClearControls()
        {
            txttyresize.Text = "";
            hidTyresizeIdno.Value = "";

        }
        #endregion
    }
}