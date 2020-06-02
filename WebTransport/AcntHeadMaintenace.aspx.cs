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
    public partial class AcntHeadMaintenace : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 14;
        Int32 aheadIdno = 0;
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
                    btnSavegroup.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                txtAcntGrp.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                this.BindMainHead();
                aheadIdno = Convert.ToInt32(Request.QueryString["AHeadIdno"]);
                if (Request.QueryString["AHeadIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["AHeadIdno"]));
                    btnNew.Visible = true;
                }
                else
                {
                    btnNew.Visible = false;
                }

                drpMHGrp.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void btnSavegroup_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
            Int64 intAHeadIdno = 0;
            aheadIdno = Convert.ToInt32(Request.QueryString["AHeadIdno"]);

            if (string.IsNullOrEmpty(hidaheadidno.Value) == true)
            {
                intAHeadIdno = objclsAcntHeadMaintenace.InsertAcntHead(txtAcntGrp.Text.Trim(), Convert.ToInt64(drpMHGrp.SelectedValue), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                if (aheadIdno > 23)
                {
                    intAHeadIdno = objclsAcntHeadMaintenace.UpdateAcntHead(txtAcntGrp.Text.Trim(), Convert.ToInt64(drpMHGrp.SelectedValue), Convert.ToInt32(hidaheadidno.Value), Convert.ToBoolean(chkStatus.Checked), empIdno);
                }
            }


            objclsAcntHeadMaintenace = null;

            if (intAHeadIdno > 0)
            {
                if (string.IsNullOrEmpty(hidaheadidno.Value) == false)
                {
                        ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (intAHeadIdno < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidaheadidno.Value) == false)
                {
                   ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            } 
            drpMHGrp.Focus();
        }

        protected void btnCancle_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidaheadidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidaheadidno.Value));
            }

        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AcntHeadMaintenace.aspx"); 
        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="AHeadIdno"></param>
        private void Populate(int AHeadIdno)
        {
            AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
            var objAHeadMast = objclsAcntHeadMaintenace.SelectById(AHeadIdno);
            objclsAcntHeadMaintenace = null;
            if (objAHeadMast != null)
            {
                drpMHGrp.SelectedValue = Convert.ToString(objAHeadMast.MainHead_Idno);
                txtAcntGrp.Text = Convert.ToString(objAHeadMast.AHead_Name);
                chkStatus.Checked = Convert.ToBoolean(objAHeadMast.Status);
                hidaheadidno.Value = Convert.ToString(objAHeadMast.AHead_Idno);
                drpMHGrp.Focus();
            }
        }

        /// <summary>
        /// To Bind Main Head DropDown
        /// </summary>
        private void BindMainHead()
        {
            AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
            var objAHeadMast = objclsAcntHeadMaintenace.SelectAHGroup();
            objclsAcntHeadMaintenace = null;
            drpMHGrp.DataSource = objAHeadMast;
            drpMHGrp.DataTextField = "AcntGH_Name";
            drpMHGrp.DataValueField = "MainHead_Idno";
            drpMHGrp.DataBind();
            drpMHGrp.Items.Insert(0, new ListItem("< Choose Main Group >", "-1"));
        }

        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            drpMHGrp.SelectedIndex = 0;
            txtAcntGrp.Text = string.Empty;
            chkStatus.Checked = true;
            hidaheadidno.Value = string.Empty;
            drpMHGrp.Focus();
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