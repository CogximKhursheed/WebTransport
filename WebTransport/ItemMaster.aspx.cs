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
    public partial class ItemMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 8;
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
                txtItemName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtDescription.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtHSNSAC.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                

                chkStatus.Checked = true;
                this.BindGroupType();
                this.BindItemUnit();
                if (Request.QueryString["ItemIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["ItemIdno"]));
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

        #region DIV BUTTON EVENT...

        protected void lnkgrpsave_Click(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            Int64 intIGrpIdno = 0;            
            int IGrpType = 0;

            if (txtGName.Text != "")
            {
                intIGrpIdno = objclsItmGrpMaster.Insert(txtGName.Text.Trim(), IGrpType, Convert.ToBoolean(chkactive.Checked), empIdno);
            }
            
            if ((intIGrpIdno > 0))
            {
                strMsg = "Record saved successfully";
                txtGName.Text = "";
                BindGroupType();
                ddlGroupType.SelectedValue =Convert.ToString(intIGrpIdno);
               ddlGroupType.Focus();

            }
            else if (intIGrpIdno == -1)
            {
                strMsg = "Record already exists!";

            }
            else
            {
                strMsg = "Oops technical error occurs!";

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);


        }
        protected void lnkuomsave_Click(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
            Int64 intUOMIdno = 0;
            if (txtUOMNameu.Text != "")
            {
                intUOMIdno = objclsUOMMaster.Insert(txtUOMNameu.Text.Trim(), txtNameHindiu.Text.Trim(), txtDescriptionu.Text.Trim(), Convert.ToBoolean(chkStatusu.Checked), empIdno);
            }
            objclsUOMMaster = null;

            if (intUOMIdno > 0)
            {

                strMsg = "Record saved successfully.";
                ClearUOM();
                BindItemUnit();
                ddlItemUnit.SelectedValue = Convert.ToString(intUOMIdno);
                ddlItemUnit.Focus();
            }
            else if (intUOMIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                strMsg = "Record not saved.";

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
           
        }
        private void ClearUOM()
        {
            txtDescriptionu.Text = txtNameHindiu.Text = txtUOMNameu.Text = "";
        }
        #endregion

        #region Button Events...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            Int64 intItemIdno = 0;
            string HSNSAC = string.IsNullOrEmpty(Convert.ToString(txtHSNSAC.Text.Trim())) ? "" : Convert.ToString(txtHSNSAC.Text.Trim());
            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                intItemIdno = objItemMast.Insert(txtItemName.Text.Trim(), txtItemNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToInt64(ddlGroupType.SelectedValue), Convert.ToInt64(ddlItemUnit.SelectedValue), Convert.ToBoolean(chkStatus.Checked), empIdno, HSNSAC);
            }
            else
            {
                intItemIdno = objItemMast.Update(txtItemName.Text.Trim(), txtItemNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToInt64(ddlGroupType.SelectedValue), Convert.ToInt64(ddlItemUnit.SelectedValue), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidItemidno.Value), empIdno, HSNSAC);
            }
            objItemMast = null;

            if (intItemIdno > 0)
            {
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (intItemIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            }
            txtItemName.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidItemidno.Value));
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemMaster.aspx");
        }
        #endregion

        #region Miscellaneous Events...
        private void BindGroupType()
        {
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var lst = objItemMast.SelectGroupType();
            objItemMast = null;
            ddlGroupType.DataSource = lst;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            ddlGroupType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindItemUnit()
        {
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var lst = objItemMast.SelectUnitType();
            objItemMast = null;
            ddlItemUnit.DataSource = lst;
            ddlItemUnit.DataTextField = "UOM_Name";
            ddlItemUnit.DataValueField = "UOM_Idno";
            ddlItemUnit.DataBind();
            ddlItemUnit.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="ItemIdno"></param>
        private void Populate(int ItemIdno)
        {
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var objitmMast = objItemMast.SelectById(ItemIdno);
            objItemMast = null;
            if (objitmMast != null)
            {
                txtItemName.Text = Convert.ToString(objitmMast.Item_Name);
                txtItemNameHindi.Text = Convert.ToString(objitmMast.ItemNamel_Hindi);
                txtDescription.Text = Convert.ToString(objitmMast.Item_Desc);
                ddlGroupType.SelectedValue = Convert.ToString(objitmMast.IGrp_Idno);
                ddlItemUnit.SelectedValue = Convert.ToString(objitmMast.Unit_Idno);
                chkStatus.Checked = Convert.ToBoolean(objitmMast.Status);
                hidItemidno.Value = Convert.ToString(objitmMast.Item_Idno);
                txtHSNSAC.Text=string.IsNullOrEmpty(Convert.ToString(objitmMast.HSNSAC_No))?"":Convert.ToString(objitmMast.HSNSAC_No);
                txtItemName.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            ddlGroupType.SelectedValue = "0";
            ddlItemUnit.SelectedValue = "0";
            txtItemName.Text = txtDescription.Text = string.Empty;
            chkStatus.Checked = true;
            txtItemNameHindi.Text = "";
            hidItemidno.Value = string.Empty;
            txtItemName.Focus();
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