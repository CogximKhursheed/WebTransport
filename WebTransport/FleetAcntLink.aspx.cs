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
    public partial class FleetAcntLink : Pagebase
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
              
                BindALLGenerals();
                btnNew.Visible = false;
                populateControls();
                ddlVatPur.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (ddlVatPur.SelectedValue != "0" || ddlCSTPur.SelectedValue != "0" || ddlVat.SelectedValue != "0" || ddlCST.SelectedValue != "0" || ddlDiscount.SelectedValue != "0" || ddlOtherChrg.SelectedValue != "0" || ddlCash.SelectedValue != "0" || ddlVatSale.SelectedValue != "0" || ddlCstSale.SelectedValue != "0" || ddlTollAcc.SelectedValue != "0")
            {
            }
            else
            {
                msg = "Please Select At List One Account!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                return;
            }


            FleetAcntLinkDAL objAcntLink = new FleetAcntLinkDAL();

            Int64 iAcntLnk_Idno = 0;

            Int64 intVATPurAcntIdno = Convert.ToInt64(ddlVatPur.SelectedValue);
            Int64 intCSTPurAcntIdno = Convert.ToInt64(ddlCSTPur.SelectedValue);
            Int64 intVatAcntIdno = Convert.ToInt64(ddlVat.SelectedValue);
            Int64 intCSTAcntIdno = Convert.ToInt64(ddlCST.SelectedValue);
            Int64 intDiscIdno = Convert.ToInt64(ddlDiscount.SelectedValue);
            Int64 intOtherAmnt = Convert.ToInt64(ddlOtherChrg.SelectedValue);
            Int64 intCashAmnt = Convert.ToInt64(ddlCash.SelectedValue);
            Int64 intVATSaleIdno= Convert.ToInt64(ddlVatSale.SelectedValue);
            Int64 intCSTSaleIdno = Convert.ToInt64(ddlCstSale.SelectedValue);
            Int64 intTollAccIdno = Convert.ToInt64(ddlTollAcc.SelectedValue);
            iAcntLnk_Idno = objAcntLink.Update(intVATPurAcntIdno, intCSTPurAcntIdno, intVatAcntIdno, intCSTAcntIdno, intDiscIdno, intOtherAmnt, intCashAmnt, intVATSaleIdno, intCSTSaleIdno, intTollAccIdno);
            //  ClearControls();
            BindALLGenerals();

            if ((iAcntLnk_Idno > 0))
            {
                msg = "Record saved successfully";

            }
            else if (iAcntLnk_Idno == -1)
            {
                msg = "Record already exists!";

            }
            else
            {
                msg = "Oops some technical error has occurred! Please contact support desk at +91-141-6672222!";

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("FleetAcntLink.aspx");

        }

        protected void btnNew__OnClick(object sender, EventArgs e)
        {
            Response.Redirect("FleetAcntLink.aspx");
        }

        protected void imgOthrAcnt_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(16,1,'Other Account')", true);
            lblAcnt.Text = "Other Account";
        }
        protected void imgservtax_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
            lblAcnt.Text = "Service Tax Account";
        }
        protected void imgCmmn_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(11)", true);
            lblAcnt.Text = "Commission Account";
        }
        protected void imgBtnSave_Click(object sender, EventArgs e)
        {
            FleetAcntLinkDAL objAcntLink = new FleetAcntLinkDAL();
            string msg = string.Empty;
            Int64 value = 0;
            value = objAcntLink.InsertPurAccountHead(txtPurAcntHead.Text.Trim(), Convert.ToInt32((hidAcntLinkidno.Value) == "" ? "0" : hidAcntLinkidno.Value), Convert.ToInt32(base.CompId), Convert.ToInt32((hidAcntType.Value) == "" ? "0" : hidAcntType.Value));
            if ((value > 0))
            {
                msg = "Record saved successfully";
                txtPurAcntHead.Text = string.Empty;
                BindALLGenerals();
            }
            else if (value == -1)
            {
                msg = "Record already exists!";

            }
            else
            {
                msg = "Oops technical error occurs!";

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        protected void btnRefresh1_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlVatPur, 20);
        }
        protected void btnRefresh2_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlCSTPur, 20);
        }
        protected void btnRefresh3_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlVat, 10);
        }
        protected void btnRefresh4_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlCST, 10);
        }
        protected void btnRefresh5_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlDiscount, 11);
        }
        protected void btnRefresh6_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlOtherChrg, 16);
        }
        protected void btnRefresh7_Click(object sender, ImageClickEventArgs e)
        {
            BindCash(ddlCash, 22);
        }
        protected void RefreshTollAcc_Click(object sender, ImageClickEventArgs e)
        {
            BindGeneral(ddlTollAcc, 11);
        }
        protected void imgTdstax_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient(10)", true);
            lblAcnt.Text = "TDS Account";
        }
        #endregion

        #region Miscellaneous Events...
        private void ClearControls()
        {
            ddlCSTPur.SelectedValue = "0";
            ddlVatPur.SelectedValue = "0";
            ddlVat.SelectedValue = "0";
            ddlCST.SelectedValue = "0";
            ddlDiscount.SelectedValue = "0";
            ddlOtherChrg.SelectedValue = "0";
        }
        private void BindALLGenerals()
        {
            if (ddlVatPur.SelectedIndex <= 0)
            {
                BindGeneral(ddlVatPur, 20);
            }
            if (ddlCSTPur.SelectedIndex <= 0)
            {
                BindGeneral(ddlCSTPur, 20);
            }
            if (ddlVat.SelectedIndex <= 0)
            {
                BindGeneral(ddlVat, 10);
            }
            if (ddlCST.SelectedIndex <= 0)
            {
                BindGeneral(ddlCST, 10);
            }
            if (ddlDiscount.SelectedIndex <= 0)
            {
                BindGeneral(ddlDiscount, 11);
            }
            if (ddlOtherChrg.SelectedIndex <= 0)
            {
                BindGeneral(ddlOtherChrg, 16);
            }
            if (ddlCash.SelectedIndex <= 0)
            {
                BindCash(ddlCash, 22);
            }
            if (ddlVatSale.SelectedIndex <= 0)
            {
                BindGeneral(ddlVatSale, 21);
            }
            if (ddlCstSale.SelectedIndex <= 0)
            {
                BindGeneral(ddlCstSale, 21);
            }
            if (ddlTollAcc.SelectedIndex <= 0)
            {
                BindGeneral(ddlTollAcc, 11);
            }
        }
       
        private void BindGeneral(DropDownList ddl, Int64 IgrpIdno)
        {
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.DataBind();
            FleetAcntLinkDAL objAcntLinkDAL = new FleetAcntLinkDAL();
            var lst = objAcntLinkDAL.BindGeneral(IgrpIdno);
            ddl.DataSource = lst;
            ddl.DataTextField = "Acnt_Name";
            ddl.DataValueField = "Acnt_Idno";
            ddl.DataBind();
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Account Name", "0"));
        }
        private void BindCash(DropDownList ddl, Int64 IgrpIdno)
        {
            ddl.DataSource = null;
            ddl.Items.Clear();
            ddl.DataBind();
            FleetAcntLinkDAL objAcntLinkDAL = new FleetAcntLinkDAL();
            var lst = objAcntLinkDAL.BindCash(IgrpIdno);
            ddl.DataSource = lst;
            ddl.DataTextField = "Acnt_Name";
            ddl.DataValueField = "Acnt_Idno";
            ddl.DataBind();
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Account Name", "0"));
        }
        private void populateControls()
        {
            FleetAcntLinkDAL Obj = new FleetAcntLinkDAL();
            var FleetAcntlink = Obj.SelectAll();
            if (FleetAcntlink != null && FleetAcntlink.Count > 0)
            {
                ddlVatPur.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "VPur_Idno"));
                ddlCSTPur.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "CPur_Idno"));
                ddlVat.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "Vat_Idno"));
                ddlCST.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "CST_Idno"));
                ddlDiscount.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "Disc_Idno"));
                ddlOtherChrg.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "Other_Idno"));
                ddlCash.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "Cash_Idno"));
                ddlVatSale.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "VSale_Idno"));
                ddlCstSale.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "CSale_Idno"));
                ddlTollAcc.SelectedValue = Convert.ToString(DataBinder.Eval(FleetAcntlink[0], "TollAcc_Idno"));
            }
            else
            {
                ddlVatPur.SelectedValue = "0";
                ddlCSTPur.SelectedValue = "0";
                ddlVat.SelectedValue = "0";
                ddlCST.SelectedValue = "0";
                ddlDiscount.SelectedValue = "0";
                ddlOtherChrg.SelectedValue = "0";
                ddlCash.SelectedValue = "0";
                ddlVatSale.SelectedValue = "0";
                ddlCstSale.SelectedValue = "0";
                ddlTollAcc.SelectedValue = "0";
            }
        }
        protected void chkboxroundof_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
