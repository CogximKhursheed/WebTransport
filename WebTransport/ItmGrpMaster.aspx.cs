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
    public partial class ItmGrpMaster : Pagebase
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
                txtGName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                this.BindGroupType(); txtGName.Focus(); BindItemType();
                if (Request.QueryString["IGrp_Idno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["IGrp_Idno"]));
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
        public void BindItemType()
        {
            ItmGrpMasterDAL objDAL = new ItmGrpMasterDAL();
            var Lst = objDAL.BindItemType();
            if (Lst != null && Lst.Count > 0)
            {
                ddlItemType.DataSource = Lst;
                ddlItemType.DataTextField = "ItemName";
                ddlItemType.DataValueField = "ItemType";
                ddlItemType.DataBind();
                ddlItemType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            Int64 intIGrpIdno = 0;
            int IGrpType = Convert.ToInt32(ddlItemType.SelectedValue);
            if (string.IsNullOrEmpty(hidIGrpMastidno.Value) == true)
            {
                intIGrpIdno = objclsItmGrpMaster.Insert(txtGName.Text.Trim(), IGrpType, Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                intIGrpIdno = objclsItmGrpMaster.Update(txtGName.Text.Trim(), IGrpType, Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidIGrpMastidno.Value), empIdno);
            }
            objclsItmGrpMaster = null;
            if (intIGrpIdno > 0)
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
                lnkbtnNew.Visible = false;
            }
            else if (intIGrpIdno < 0)
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
            Response.Redirect("ItmGrpMaster.aspx");
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

        /// <summary>
        /// To Clear Feilds
        /// </summary>
        private void ClearControls()
        {
            txtGName.Text = string.Empty;
            //ddlGroupType.SelectedValue = "0";
            //ddlGroupType.SelectedIndex = 0;
            ddlItemType.SelectedIndex = 0;
            hidIGrpMastidno.Value = string.Empty;
            //ddlGroupType.Focus();
        }

        /// <summary>
        /// To Populate Data
        /// </summary>
        /// <param name="ColrIdno"></param>
        private void Populate(int IGrpIdno)
        {
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            var objIgrpMast = objclsItmGrpMaster.SelectById(IGrpIdno);
            objclsItmGrpMaster = null;
            if (objIgrpMast != null)
            {
                txtGName.Text = Convert.ToString(objIgrpMast.IGrp_Name);
                ddlItemType.SelectedValue = Convert.ToString(objIgrpMast.IGrp_Type);
                //  ddlGroupType.SelectedValue = Convert.ToString(objIgrpMast.IGrp_Type);
                chkStatus.Checked = Convert.ToBoolean(objIgrpMast.Status);
                hidIGrpMastidno.Value = Convert.ToString(objIgrpMast.IGrp_Idno);
                // ddlGroupType.Focus();
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

        #region Fuctions...
        private void BindGroupType()
        {
            //ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            //var lst = objclsItmGrpMaster.SelectGroupTypeForItemGrp();
            //objclsItmGrpMaster = null;
            //ddlGroupType.DataSource = lst;
            //ddlGroupType.DataTextField = "Item_Name";
            //ddlGroupType.DataValueField = "ItemType_Idno";
            //ddlGroupType.DataBind();
            //ddlGroupType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        #endregion


    }
}