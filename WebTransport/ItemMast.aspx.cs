using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;

namespace WebTransport
{
    public partial class ItemMast : Pagebase
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
                    //lblViewList.Visible = false;
                }
                txtItemName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtDescription.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                txtVAT.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                chkStatus.Checked = true;

                BindItemType();
                BindItemGroup();
                ddlAddType_OnSelectedIndexChanged(sender, e);
                if (Request.QueryString["ItemIdno"] != null)
                {
                    lnkbtnNew.Visible = true;
                    this.BindGroupType();
                    this.BindItemUnit();
                    this.Populate(Convert.ToInt32(Request.QueryString["ItemIdno"]));
                }
                else
                {
                    this.BindActiveGroupType();
                    this.BindActiveItemUnit();
                    lnkbtnNew.Visible = false;
                }
                txtItemName.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemName.Text) == true)
            {
                this.ShowMessageErr("Please Enter Item Name !"); txtItemName.Focus(); return;
            }
            else if (ddlGroupType.SelectedIndex == 0)
            { this.ShowMessageErr("Please Select Item Grp!"); ddlGroupType.Focus(); return; }
            else if (ddlItemUnit.SelectedIndex == 0)
            { this.ShowMessageErr("Please Select Item Unit!"); ddlItemUnit.Focus(); return; }
            else if (ddlTyreType.SelectedIndex == 0)
            { this.ShowMessageErr("Please Select Tyre Type!"); ddlTyreType.Focus(); return; }
            //else if (string.IsNullOrEmpty(txtitemmrp.Text) == true || (Convert.ToDouble(txtitemmrp.Text) <= 0))
            //{
            //    this.ShowMessageErr("Please Enter Item MRP !"); txtitemmrp.Focus(); return;
            //}
           else if (string.IsNullOrEmpty(txtsgst.Text) == true)
            {
                this.ShowMessageErr("Please Enter SGST !"); txtsgst.Focus(); return;
            }
           else if (string.IsNullOrEmpty(txtcgst.Text) == true)
            {
                this.ShowMessageErr("Please Enter CGST !"); txtcgst.Focus(); return;
            }
           else if (string.IsNullOrEmpty(txtigst.Text) == true)
            {
                this.ShowMessageErr("Please Enter IGST !"); txtigst.Focus(); return;
            }
      
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            Int64 intItemIdno = 0;
            Int64 ItemType = string.IsNullOrEmpty(ddlAddType.SelectedValue) ? 0 : Convert.ToInt64(ddlAddType.SelectedValue);
            Int64 TyreType = string.IsNullOrEmpty(ddlTyreType.SelectedValue) ? 0 : Convert.ToInt64(ddlTyreType.SelectedValue);
            Double sgst = string.IsNullOrEmpty(txtsgst.Text) ? 0 :Convert.ToDouble(txtsgst.Text.Trim());
            Double cgst = string.IsNullOrEmpty(txtcgst.Text) ? 0 : Convert.ToDouble(txtcgst.Text.Trim());
            Double igst = string.IsNullOrEmpty(txtigst.Text) ? 0 : Convert.ToDouble(txtigst.Text.Trim());
            Double Itemmrp = string.IsNullOrEmpty(txtitemmrp.Text) ? 0 : Convert.ToDouble(txtitemmrp.Text.Trim());

            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                intItemIdno = objItemMast.Insert(txtItemName.Text.Trim(), txtItemNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToInt64(ddlGroupType.SelectedValue), Convert.ToInt64(ddlItemUnit.SelectedValue), Convert.ToDouble(TxtPurchaseRate.Text.Trim()), Convert.ToDouble(txtVAT.Text.Trim()), Convert.ToDouble(txtCST.Text.Trim()), Convert.ToBoolean(chkStatus.Checked), ItemType, TyreType, txtCompanyName.Text.Trim(), txtModelName.Text.Trim(), empIdno, sgst,cgst,igst, Itemmrp);
            }
            else
            {
                intItemIdno = objItemMast.Update(txtItemName.Text.Trim(), txtItemNameHindi.Text.Trim(), txtDescription.Text.Trim(), Convert.ToInt64(ddlGroupType.SelectedValue), Convert.ToInt64(ddlItemUnit.SelectedValue), Convert.ToDouble(TxtPurchaseRate.Text.Trim()), Convert.ToDouble(txtVAT.Text.Trim()), Convert.ToDouble(txtCST.Text.Trim()), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(hidItemidno.Value), ItemType, TyreType, txtCompanyName.Text.Trim(), txtModelName.Text.Trim(), empIdno, sgst, cgst, igst, Itemmrp);
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
                lnkbtnNew.Visible = false;
            }
            else if (intItemIdno < 0)
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
            Response.Redirect("ItemMast.aspx");
        }
        #endregion

        #region Miscellaneous Events...





        private void BindItemType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectItemType();
            ddlAddType.DataSource = lst;
            ddlAddType.DataTextField = "ItemType_Name";
            ddlAddType.DataValueField = "ItemTpye_Idno";
            ddlAddType.DataBind();

            ddlItemGropForPopup.DataSource = lst;
            ddlItemGropForPopup.DataTextField = "ItemType_Name";
            ddlItemGropForPopup.DataValueField = "ItemTpye_Idno";
            ddlItemGropForPopup.DataBind();

            objItemMast = null;
        }

        private void BindTyreType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectTyreType();
            ddlTyreType.DataSource = lst;
            ddlTyreType.DataTextField = "TyreType_Name";
            ddlTyreType.DataValueField = "TyreType_Idno";
            ddlTyreType.DataBind();
            ddlTyreType.Items.Insert(0, new ListItem("--Select--", "0"));
            objItemMast = null;

        }
        private void BindTyreTypeALL()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectTyreTypeAll();
            ddlTyreType.DataSource = lst;
            ddlTyreType.DataTextField = "TyreType_Name";
            ddlTyreType.DataValueField = "TyreType_Idno";
            ddlTyreType.DataBind();
            ddlTyreType.Items.Insert(0, new ListItem("--Select--", "0"));
            objItemMast = null;

        }

        private void BindGroupType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectGroupType();
            objItemMast = null;
            ddlGroupType.DataSource = lst;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            ddlGroupType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindActiveGroupType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectActiveGroupType();
            objItemMast = null;
            ddlGroupType.DataSource = lst;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            ddlGroupType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindItemUnit()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectUnitType();
            objItemMast = null;
            ddlItemUnit.DataSource = lst;
            ddlItemUnit.DataTextField = "UOM_Name";
            ddlItemUnit.DataValueField = "UOM_Idno";
            ddlItemUnit.DataBind();
            ddlItemUnit.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindActiveItemUnit()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectActiveUnitType();
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
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var objitmMast = objItemMast.SelectById(ItemIdno);
            objItemMast = null;
            if (objitmMast != null)
            {
                txtItemName.Text = Convert.ToString(objitmMast.Item_Name);
                txtItemNameHindi.Text = Convert.ToString(objitmMast.ItemNamel_Hindi);
                txtDescription.Text = Convert.ToString(objitmMast.Item_Desc);
                ddlGroupType.SelectedValue = Convert.ToString(objitmMast.IGrp_Idno);
                ddlItemUnit.SelectedValue = Convert.ToString(objitmMast.Unit_Idno);

                if (!string.IsNullOrEmpty(Convert.ToString(objitmMast.ItemType)))
                {
                    this.BindTyreTypeALL();
                    ddlAddType.SelectedValue = Convert.ToString(objitmMast.ItemType);
                    ddlAddType_OnSelectedIndexChanged(new Object(), EventArgs.Empty);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(objitmMast.Tyre_Type)))
                {

                    ddlTyreType.SelectedValue = Convert.ToString(objitmMast.Tyre_Type);
                }

                txtCompanyName.Text = Convert.ToString(objitmMast.Comp_Name);
                txtModelName.Text = Convert.ToString(objitmMast.Model_Name);

                TxtPurchaseRate.Text = Convert.ToDouble(objitmMast.Pur_Rate).ToString("N2");
                txtVAT.Text = Convert.ToDouble(objitmMast.VATTax_Per).ToString("N2");
                txtCST.Text = Convert.ToDouble(objitmMast.CSTTax_Per).ToString("N2");
                txtsgst.Text = Convert.ToDouble(objitmMast.SGST).ToString("N2");
                txtcgst.Text = Convert.ToDouble(objitmMast.CGST).ToString("N2");
                txtigst.Text = Convert.ToDouble(objitmMast.IGST).ToString("N2");
                txtitemmrp.Text = Convert.ToDouble(objitmMast.Item_MRP).ToString("N2");
                chkStatus.Checked = Convert.ToBoolean(objitmMast.Status);
                hidItemidno.Value = Convert.ToString(objitmMast.Item_Idno);
                txtItemName.Focus();
            }
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtItemName.Text = txtDescription.Text = string.Empty;
            chkStatus.Checked = true;
            ddlTyreType.SelectedValue = "0";
            txtItemNameHindi.Text = txtModelName.Text = txtCompanyName.Text = "";
            TxtPurchaseRate.Text = "0.00";
            txtVAT.Text = "0.00";
            txtCST.Text = "0.00";
            txtsgst.Text = "0.00";
            txtcgst.Text = "0.00";
            txtigst.Text = "0.00";

            if (string.IsNullOrEmpty(hidItemidno.Value) == false)
            {
                ddlGroupType.Items.Clear(); ddlItemUnit.Items.Clear();
                BindActiveGroupType(); BindActiveItemUnit();
            }
            hidItemidno.Value = string.Empty;
            txtItemName.Focus();
            ddlGroupType.SelectedValue = "0";
            ddlItemUnit.SelectedValue = "0"; ddlAddType.SelectedValue = "1";
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

        #region ControlEvents..
        protected void ddlAddType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAddType.SelectedValue == "1")
            {
                ddlTyreType.Enabled = true; txtCompanyName.Enabled = true; txtModelName.Enabled = true;
                BindTyreType();
            }
            else
            {
                txtModelName.Text = txtCompanyName.Text = "";
                ddlTyreType.Enabled = false; txtCompanyName.Enabled = false; txtModelName.Enabled = false;
                ddlTyreType.Items.Clear();
            }
            if (ddlAddType.SelectedValue == "4")
                lblPur.Text = "Labour Rate";
            else
                lblPur.Text = "Purchase Rate";

            ddlAddType.Focus();
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtDescription.Text = txtItemName.Text;
            txtItemNameHindi.Focus();
        }
        protected void txtVAT_TextChanged(object sender, EventArgs e)
        {
            if (txtVAT.Text.Trim() == "")
                txtVAT.Text = "0.00";
            string myString = txtVAT.Text;
            string Tax = myString.Split('.')[0];
            if (Tax != string.Empty)
            {
                if ((Convert.ToDouble(Tax) >= 100) || (Convert.ToDouble(myString) >= 100))
                {
                    txtVAT.Text = "0.00";
                    string strMsg = "TAX cannot be greater than or equal to 100%";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                    txtVAT.Focus();
                }
                else { txtCST.Focus(); }
            }
            else
            {
                txtVAT.Text = "0.00";
            }
            txtVAT.Focus();
        }
        protected void txtCST_TextChanged(object sender, EventArgs e)
        {
            if (txtCST.Text.Trim() == "")
                txtCST.Text = "0.00";
            string myString = txtCST.Text;
            string s1 = myString.Split('.')[0];
            if (s1 != string.Empty)
            {
                if ((Convert.ToDouble(s1) >= 100) || (Convert.ToDouble(myString) >= 100))
                {
                    txtCST.Text = "0.00";
                    string strMsg = "TAX cannot be greater than or equal to 100%";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                    txtCST.Focus();
                }
                else { ddlAddType.Focus(); }
            }
            else
            {
                txtCST.Text = "0.00";
            }

            txtCST.Focus();
        }

        #endregion

        #region add Pop for grouptype BYLokesh
        private void ClearControlsForGroup()
        {
            ddlItemGropForPopup.SelectedIndex = 0;
            txtGroupNameForPopup.Text = string.Empty;
        }

        protected void lnkbtnSaveGroup_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            Int64 intIGrpIdno = 0;
            int IGrpType = Convert.ToInt32(ddlItemGropForPopup.SelectedValue);
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            intIGrpIdno = objclsItmGrpMaster.Insert(txtGroupNameForPopup.Text.Trim(), IGrpType, true, empIdno);

            objclsItmGrpMaster = null;
            if (intIGrpIdno > 0)
            {
                BindGroupType();
                strMsg = "Record saved successfully.";

                this.ClearControlsForGroup();
            }
            else if (intIGrpIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                strMsg = "Record not saved.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        protected void lnkbtnUom_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
            Int64 intUOMIdno = 0;
            intUOMIdno = objclsUOMMaster.Insert(txtUOMName.Text.Trim(), "", "", true, empIdno);

            objclsUOMMaster = null;

            if (intUOMIdno > 0)
            {
                txtUOMName.Text = string.Empty;
                BindItemUnit();
                strMsg = "Record saved successfully.";
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
        #endregion

        protected void imgBtnSave_Click(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            Int64 intIGrpIdno = 0;
            int IGrpType = Convert.ToInt32(ddlItemType.SelectedValue);
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());

            intIGrpIdno = objclsItmGrpMaster.Insert(txtGName.Text.Trim(), IGrpType, Convert.ToBoolean(chkStatus.Checked), empIdno);

            objclsItmGrpMaster = null;
            if (intIGrpIdno > 0)
            {
                //lnkbtnNew.Visible = false;
            }
            else if (intIGrpIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                strMsg = "Record not saved.";
            }
        }

        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemMastPurDAL obj=new ItemMastPurDAL ();
            DataTable Dttemp=new DataTable();
            Int64 Value = string.IsNullOrEmpty(Convert.ToString(ddlGroupType.SelectedValue)) ? 0 : Convert.ToInt64(ddlGroupType.SelectedValue);
            Dttemp = obj.SelectType(ApplicationFunction.ConnectionString(), Value);
            if (Dttemp != null && Dttemp.Rows.Count > 0)
            {                
                ddlAddType.SelectedValue = Dttemp.Rows[0]["IDNO"].ToString();
            }
        }
       
        private void BindItemGroup()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var objUserPref = obj.BindItemGroup();
            obj = null;
            if (objUserPref != null && objUserPref.Count > 0)
            {
                ddlItemGroupForPopup.DataSource = objUserPref;
                ddlItemGroupForPopup.DataTextField = "ItemType_Name";
                ddlItemGroupForPopup.DataValueField = "ItemTpye_Idno";
                ddlItemGroupForPopup.DataBind();
            }
            ddlItemGroupForPopup.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void lnkbtnSaveGrop_Click(object sender, EventArgs e)
        {
            UserPrefenceDAL obj = new UserPrefenceDAL();

            Int32 intItemType = Convert.ToInt32((ddlItemGroupForPopup.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlItemGroupForPopup.SelectedValue));
            Int32 intTaxType = Convert.ToInt32((ddlTaxType.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlTaxType.SelectedValue));
            double dValue = Convert.ToDouble((txtPercentage.Text.Trim()) == "" ? 0.00 : Convert.ToDouble(txtPercentage.Text.Trim()));

            DataTable dt = obj.UpdateTaxVat(intItemType, intTaxType, dValue, ApplicationFunction.ConnectionString());
            if (dt != null && dt.Rows.Count > 0)
            {
                ShowMessage("Record updated successfully.");
                ddlItemGroupForPopup.SelectedValue = ddlTaxType.SelectedValue = "0"; txtPercentage.Text = "0.00";
            }
            else
            {
                ShowMessageErr("Record Not updated.");
            }
        }   
    }
}