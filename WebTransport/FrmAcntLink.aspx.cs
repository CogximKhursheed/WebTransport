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
    public partial class FrmAcntLink : Pagebase
    {
        #region Private Variable..
        private int intFormId = 13;
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
                    btnSubmit.Visible = false;
                }
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}
                BindALLGenerals();
                this.BindGroupType();
                btnNew.Visible = false;
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
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            AcntLinkDAL objAcntLink = new AcntLinkDAL();
            string msg = string.Empty;
            Int64 iAcntLnk_Idno = 0;
            Int32 iGRPiDNO = Convert.ToInt32(ddlGroupType.SelectedValue);
            Int64 intOTAcntIdno = Convert.ToInt64(ddlOthrAmnt.SelectedValue);
            Int64 intCMAcntIdno = Convert.ToInt64(ddlComision.SelectedValue);
            Int64 intSTAcntIdno = Convert.ToInt64(ddlservcetax.SelectedValue);
            Int64 intSwachhBharat= Convert.ToInt64(ddlSwachhBharat.SelectedValue);
            Int64 intKrishiKalyan = Convert.ToInt64(ddlKrishiKalyan.SelectedValue);
            Int64 intTDSAcntIdno = Convert.ToInt64(ddlTdsAcnt.SelectedValue);
            Int64 intDebitIdno = Convert.ToInt64(ddlDebitnote.SelectedValue);
            Int64 intTDSAmnt = Convert.ToInt64(ddlTDSAmnt.SelectedValue);
            Int64 intDiesel = Convert.ToInt64(ddlDiesel.SelectedValue);
            bool bStatus = Convert.ToBoolean(1);

            Int32 intCompIdno = Convert.ToInt32(base.CompId);
            Int64 intSGST = Convert.ToInt64(ddlSGST.SelectedValue);
            Int64 intCGST = Convert.ToInt64(ddlCGST.SelectedValue);
            Int64 intIGST = Convert.ToInt64(ddlIGST.SelectedValue);
            if (objAcntLink.IsExists(iGRPiDNO) == true)
            {
                iAcntLnk_Idno = objAcntLink.Update(intCMAcntIdno, intOTAcntIdno, iGRPiDNO, intSTAcntIdno, intTDSAcntIdno, intDebitIdno, intTDSAmnt, empIdno, intSwachhBharat, intKrishiKalyan, intDiesel, intSGST, intCGST, intIGST);
                //ClearControls();
                BindALLGenerals();
            }
            else
            {
                iAcntLnk_Idno = objAcntLink.Insert(intCMAcntIdno, intOTAcntIdno, iGRPiDNO, intSTAcntIdno, intTDSAcntIdno, intDebitIdno, intTDSAmnt, empIdno, intSwachhBharat, intKrishiKalyan, intDiesel, intSGST, intCGST, intIGST);
                //  ClearControls();
                BindALLGenerals();
            }
            if ((iAcntLnk_Idno > 0))
            {
              ShowMessage("Record saved successfully");
                ddlGroupType.Focus();
            }
            else if (iAcntLnk_Idno == -1)
            {
                ShowMessageErr("Record already exists!");
                ddlGroupType.Focus();
            }
            else
            {
               ShowMessageErr("Oops some technical error has occurred! Please contact support desk at +91-141-6672222!");
                ddlGroupType.Focus();
            }
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            AcntLinkDAL objAcntLink = new AcntLinkDAL();
            Int32 IgrpIdno = Convert.ToInt32(ddlGroupType.SelectedValue);

            if (objAcntLink.IsExists(IgrpIdno) == true)
            {
                populateControls(IgrpIdno);
            }
            else
            {
                ClearControls();
                ddlGroupType.SelectedValue = Convert.ToString(IgrpIdno);

            }

        }

        protected void btnNew__OnClick(object sender, EventArgs e)
        {
            Response.Redirect("FrmAcntLink.aspx");
        }

        //protected void imgOthrAcnt_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(16,1,'Other Account')", true);
        //    lblAcnt.Text = "Other Account";
        //}
        //protected void imgservtax_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
        //    lblAcnt.Text = "Service Tax Account";
        //}
        //protected void imgSwachhBharat_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
        //    lblAcnt.Text = "Swachh Bharat Account";
        //}
        //protected void imgbtnKrishiKalyan_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
        //    lblAcnt.Text = "Krishi Kalyan Account";
        //}

        //protected void imgCmmn_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(11)", true);
        //    lblAcnt.Text = "Commission Account";
        //}
        //protected void ImgDiesel_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(11)", true);
        //    lblAcnt.Text = "Diesel Account";
        //}

        protected void imgBtnSave_Click(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            AcntLinkDAL objAcntLink = new AcntLinkDAL();
            string msg = string.Empty;
            Int64 value = 0;
            value = objAcntLink.InsertPurAccountHead(txtPurAcntHead.Text.Trim(), Convert.ToInt32(hidAcntLinkidno.Value), Convert.ToInt32(base.CompId), Convert.ToInt32(hidAcntType.Value), empIdno);
            if ((value > 0))
            {
               ShowMessage("Record saved successfully");
               txtPurAcntHead.Text = string.Empty;
               BindALLGenerals();
            }
            else if (value == -1)
            {
               ShowMessageErr("Record already exists!");
                ddlGroupType.Focus();
            }
            else
            {
                ShowMessageErr("Oops technical error occurs!");
                ddlGroupType.Focus();
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        protected void btnRefresh1_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlOthrAmnt, 16);
        }
        protected void refDiesel_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlDiesel, 11);
        }
        protected void btnRefresh2_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlComision, 11);
        }
        protected void btnRefresh3_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlservcetax, 10);
        }
        protected void btnRefSwachhBharat_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlSwachhBharat, 10);
        }
        protected void btnKrishiKalyan_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlKrishiKalyan, 10);
        }

        protected void imgTdstax_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
            lblAcnt.Text = "TDS Account";
        }
        protected void btnRefresh4_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlTdsAcnt, 10);
        }

        protected void btnRefresh5_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlDebitnote, 11);
        }

        protected void btnRefresh6_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlTDSAmnt, 7);
        }
        protected void Imgbtnsgst_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlSGST, 10);
        }
        protected void Imgbtncgst_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlCGST, 10);
        }
        protected void imgbtnIgst_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlIGST, 10);
        }
        #endregion

        #region Miscellaneous Events...
        /// <summary>
        /// To Populate all controls
        /// </summary>
        /// <param name="AcntLinkIdno"></param>

        private void BindGroupType()
        {
            AcntLinkDAL objclsItmGrpMaster = new AcntLinkDAL();
            var lst = objclsItmGrpMaster.SelectGroupTypeForItemGrp();
            objclsItmGrpMaster = null;
            ddlGroupType.DataSource = lst;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            ddlGroupType.Items.Insert(0, new ListItem("--Select--", "0"));

            if ((ddlGroupType != null) && (ddlGroupType.Items.Count > 1))
            {
                ddlGroupType.SelectedIndex = 1;
                ddlGroupType.Enabled = false;
                ddlGroupType_SelectedIndexChanged(null, null); ddlOthrAmnt.Focus();
            }
            else
            {
                ddlGroupType.Enabled = true; 
                ddlGroupType.Focus();
            }
        }

        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            ddlComision.SelectedValue = "0";
            ddlOthrAmnt.SelectedValue = "0";
            ddlGroupType.SelectedValue = "0";
            ddlservcetax.SelectedValue = "0";
            ddlTdsAcnt.SelectedValue = "0";
            ddlGroupType.Focus();
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindALLGenerals();
            AcntLinkDAL objAcntLink = new AcntLinkDAL();
            Int32 IgrpIdno = Convert.ToInt32(ddlGroupType.SelectedValue);

            if (objAcntLink.IsExists(IgrpIdno) == true)
            {
                populateControls(IgrpIdno);
            }
            else
            {
                ClearControls();
                ddlGroupType.SelectedValue = Convert.ToString(IgrpIdno);

            }
            ddlGroupType.Focus();
        }
        private void BindALLGenerals()
        {
            if (ddlOthrAmnt.SelectedIndex <= 0)
            {
                BindGeneral(ddlOthrAmnt, 16);
            }
            if (ddlComision.SelectedIndex <= 0)
            {
                BindGeneral(ddlComision, 11);
            }
            if (ddlservcetax.SelectedIndex <= 0)
            {
                BindGeneral(ddlservcetax, 10);
            }
            if (ddlTdsAcnt.SelectedIndex <= 0)
            {
                BindGeneral(ddlTdsAcnt, 10);
            }
            if (ddlDebitnote.SelectedIndex <= 0)
            {
                BindGeneral(ddlDebitnote, 11);
            }
            if (ddlTDSAmnt.SelectedIndex <= 0)
            {
                BindGeneral(ddlTDSAmnt, 7);
            }
            if (ddlSwachhBharat.SelectedIndex <= 0)
            {
                BindGeneral(ddlSwachhBharat, 10);
            }
            if (ddlKrishiKalyan.SelectedIndex <= 0)
            {
                BindGeneral(ddlKrishiKalyan, 10);
            }
            if (ddlDiesel.SelectedIndex <= 0)
            {
                BindGeneral(ddlDiesel, 11);
            }
            if (ddlSGST.SelectedIndex <= 0)
            {
                BindGeneral(ddlSGST, 10);
            }
            if (ddlCGST.SelectedIndex <= 0)
            {
                BindGeneral(ddlCGST, 10);
            }
            if (ddlIGST.SelectedIndex <= 0)
            {
                BindGeneral(ddlIGST, 10);
            }
        }
        private void BindGeneral(DropDownList ddl, Int64 IgrpIdno)
        {
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.DataBind();
            AcntLinkDAL objAcntLinkDAL = new AcntLinkDAL();
            var lst = objAcntLinkDAL.BindGeneral(IgrpIdno);
            ddl.DataSource = lst;
            ddl.DataTextField = "Acnt_Name";
            ddl.DataValueField = "Acnt_Idno";
            ddl.DataBind();
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Account Name", "0"));
        }
        private void populateControls(Int32 Igrpidno)
        {
            AcntLinkDAL objAcntLink = new AcntLinkDAL();
            var lst = objAcntLink.SelectById(Igrpidno);
            if (lst != null)
            {
                ddlComision.SelectedValue = Convert.ToString(lst.Commsn_Idno);
                ddlOthrAmnt.SelectedValue = Convert.ToString(lst.OthrAc_Idno);
                ddlservcetax.SelectedValue = Convert.ToString(lst.SAcnt_Idno);
                ddlTdsAcnt.SelectedValue = Convert.ToString(lst.TDS_Idno);
                ddlDebitnote.SelectedValue = Convert.ToString(lst.Debit_Idno);
                ddlTDSAmnt.SelectedValue = Convert.ToString(lst.TDSAmnt_Idno);
                ddlSwachhBharat.SelectedValue = Convert.ToString(lst.SwachBharat_Idno);
                ddlKrishiKalyan.SelectedValue = Convert.ToString(lst.KrishiKalyan_Idno);
                ddlDiesel.SelectedValue = Convert.ToString(lst.DieselAcc_Idno);
                ddlSGST.SelectedValue = Convert.ToString(lst.DieselAcc_Idno);
                ddlCGST.SelectedValue = Convert.ToString(lst.DieselAcc_Idno);
                ddlIGST.SelectedValue = Convert.ToString(lst.DieselAcc_Idno);

            }
            else
            {
                ddlOthrAmnt.SelectedIndex = 0;
                ddlComision.SelectedIndex = 0;
                ddlservcetax.SelectedIndex = 0;
                ddlTdsAcnt.SelectedIndex = 0;
                ddlDebitnote.SelectedIndex = 0;
                ddlTDSAmnt.SelectedIndex = 0;
                ddlSwachhBharat.SelectedIndex = 0;
                ddlKrishiKalyan.SelectedIndex = 0;
                ddlDiesel.SelectedIndex = 0;
                ddlSGST.SelectedIndex = 0;
                ddlCGST.SelectedIndex = 0;
                ddlIGST.SelectedIndex = 0;
            }
        }
        #endregion
    }
}
