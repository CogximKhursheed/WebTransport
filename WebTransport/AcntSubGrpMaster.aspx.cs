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
    public partial class AcntSubGrpMaster : Pagebase
    {

        #region Variables declaration...
        private int intFormId = 15;
        Int32 ASubHeadIdno = 0;
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
                //if (base.ADD == false)
                //{
                //    btnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}
                txtAcntSubGrp.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
              
                ASubHeadIdno = Convert.ToInt32(Request.QueryString["ASubHeadIdno"]);

                if (Request.QueryString["ASubHeadIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["ASubHeadIdno"]));
                    lnkbtnNew.Visible = true;
                    this.BindHeadAll();
                }
                else
                {
                    lnkbtnNew.Visible = false;
                    this.BindHead();
                }
                drpAGrp.Focus();
            }
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            Int64 intSubHeadIdno = 0;
            ASubHeadIdno = Convert.ToInt32(Request.QueryString["ASubHeadIdno"]);
            if (string.IsNullOrEmpty(hidacntsubheadidno.Value) == true)
            {
                intSubHeadIdno = objclsAcntSubGrpMaster.InsertAcntHead(txtAcntSubGrp.Text.Trim(), Convert.ToInt64(drpAGrp.SelectedValue), Convert.ToBoolean(chkStatus.Checked), empIdno);
            }
            else
            {
                if (ASubHeadIdno > 23)
                {
                    intSubHeadIdno = objclsAcntSubGrpMaster.UpdateAcntHead(txtAcntSubGrp.Text.Trim(), Convert.ToInt64(drpAGrp.SelectedValue), Convert.ToInt32(hidacntsubheadidno.Value), Convert.ToBoolean(chkStatus.Checked), empIdno);
                }
            }
            objclsAcntSubGrpMaster = null;

            if (intSubHeadIdno > 0)
            {
                if (string.IsNullOrEmpty(hidacntsubheadidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessageErr("Record saved successfully.");
                }
                this.ClearControls();
                drpAGrp.DataSource = null;
                drpAGrp.DataBind();
                this.BindHead();
            }
            else if (intSubHeadIdno < 0)
            {
                ShowMessageErr("Record already exists!");
            }
            else
            {
                if (string.IsNullOrEmpty(hidacntsubheadidno.Value) == false)
                {
                    ShowMessageErr("Record not updated!");
                }
                else
                {
                   ShowMessageErr("Record not saved!");
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            drpAGrp.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidacntsubheadidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidacntsubheadidno.Value));
            }

        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AcntSubGrpMaster.aspx");
        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="SubHeadIdno"></param>
        private void Populate(int SubHeadIdno)
        {
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            var objAHeadMast = objclsAcntSubGrpMaster.SelectById(SubHeadIdno);
            objclsAcntSubGrpMaster = null;
            if (objAHeadMast != null)
            {
                drpAGrp.SelectedValue = Convert.ToString(objAHeadMast.AHead_Idno);
                txtAcntSubGrp.Text = Convert.ToString(objAHeadMast.ASubHead_Name);
                chkStatus.Checked = Convert.ToBoolean(objAHeadMast.Status);
                hidacntsubheadidno.Value = Convert.ToString(objAHeadMast.ASubHead_Idno);
                drpAGrp.Focus();
            }
        }

        /// <summary>
        /// To Bind Head DropDown
        /// </summary>
        private void BindHead()
        {
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            var objAHeadMast = objclsAcntSubGrpMaster.SelectAHGroupActiveOnly();
            objclsAcntSubGrpMaster = null;
            drpAGrp.DataSource = objAHeadMast;
            drpAGrp.DataTextField = "AHead_Name";
            drpAGrp.DataValueField = "AHead_Idno";
            drpAGrp.DataBind();
            drpAGrp.Items.Insert(0, new ListItem("< Choose Group >", "0"));
        }
        private void BindHeadAll()
        {
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            var objAHeadMast = objclsAcntSubGrpMaster.SelectAHGroupAll();
            objclsAcntSubGrpMaster = null;
            drpAGrp.DataSource = objAHeadMast;
            drpAGrp.DataTextField = "AHead_Name";
            drpAGrp.DataValueField = "AHead_Idno";
            drpAGrp.DataBind();
            drpAGrp.Items.Insert(0, new ListItem("< Choose Group >", "0"));
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            drpAGrp.SelectedValue = "0";
            txtAcntSubGrp.Text = string.Empty;
            chkStatus.Checked = true;
            hidacntsubheadidno.Value = string.Empty;
            drpAGrp.Focus();
        }

        #endregion

        #region

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