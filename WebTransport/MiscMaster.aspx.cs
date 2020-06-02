using System;
using System.Web.UI;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class MiscMaster : Pagebase
    {
        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                BindParty();
                txtName.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
                chkStatus.Checked = true;
                if (Request.QueryString["MID"] != null)
                {
                    this.Populate(Convert.ToInt64(Request.QueryString["MID"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtName.Focus();
                
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            #region "Object"
            MiscDAL objMisc = new MiscDAL();
            #endregion

            #region "Declaring Variables"
            Int64 iMiscIdno = 0;
            Int64 iMiscTypeIdno = string.IsNullOrEmpty(ddlType.SelectedValue) ? 0 : Convert.ToInt64(ddlType.SelectedValue);
            string strMiscTypeName = string.IsNullOrEmpty(ddlType.SelectedItem.Text) ? "" : Convert.ToString(ddlType.SelectedItem.Text);
            string strMiscName = string.IsNullOrEmpty(txtName.Text.Trim()) ? "" : Convert.ToString(txtName.Text.Trim());
            #endregion
            string strMsg = string.Empty;
            if (string.IsNullOrEmpty(hidMiscidno.Value) == true)
            {
                iMiscIdno = objMisc.InsertMisc(strMiscTypeName, iMiscTypeIdno, strMiscName, Convert.ToBoolean(chkStatus.Checked),Convert.ToInt32(ddlParty.SelectedValue));
            }
            else
            {
                iMiscIdno = objMisc.UpdateMisc(Convert.ToInt64(hidMiscidno.Value), strMiscTypeName, iMiscTypeIdno, strMiscName, Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(ddlParty.SelectedValue));
            }
            objMisc = null;
            if (iMiscIdno > 0)
            {
                if (string.IsNullOrEmpty(hidMiscidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (iMiscIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidMiscidno.Value) == false)
                {
                    ShowMessageErr("Record cannot be updated.");
                }
                else
                {
                    ShowMessageErr("Record cannot be saved.");
                }
            }
        }
        #endregion

        #region Functions...
        private void ClearControls()
        {
            ddlType.SelectedValue = "0";
            txtName.Text = string.Empty;
            chkStatus.Checked = true;
            hidMiscidno.Value = string.Empty;
            ddlParty.SelectedValue= "0";
            txtName.Focus();
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        public void Populate(Int64 iMiscId)
        {
            MiscDAL objM = new MiscDAL();
            var  obj = objM.SelectDetail(iMiscId);
            hidMiscidno.Value = Convert.ToString(iMiscId);
            if (obj != null)
            {

                ddlType.SelectedValue = Convert.ToString(DataBinder.Eval(obj[i], "Tran_Idno"));
                ddlType.SelectedItem.Text = Convert.ToString(DataBinder.Eval(obj[i],"Tran_Type"));
                txtName.Text = Convert.ToString(DataBinder.Eval(obj[i], "Misc_Name"));
                chkStatus.Checked = Convert.ToBoolean(DataBinder.Eval(obj[i], "Misc_Status"));
                ddlParty.SelectedValue = Convert.ToString(DataBinder.Eval(obj[i], "Acnt_Idno"));
                txtName.Focus();
            }
            objM = null;
        }
        #endregion

        public void BindParty()
        {
            MiscDAL obj = new MiscDAL();
            var senderLst = obj.BindParty();
            ddlParty.DataSource = senderLst;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        
       
       
    }
}