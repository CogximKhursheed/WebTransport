using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.Transactions;

namespace WebTransport
{
    public partial class UpdateGrDetail : Pagebase
    {

        #region Private Variable...
        string SSnd = ConfigurationManager.AppSettings["SSnd"];
        DataTable dt = new DataTable();
        int dTotIssueQty = 0; double dTotRcptQty = 0; double dtotWeight = 0;
        double dTotIssueAmnt = 0; DataTable DtTemp = new DataTable();
        private int intFormId = 27; double LocGrAmnt = 0; double OutGrAmnt = 0;
        #endregion

        #region Page events...
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionValues svalue = new SessionValues();
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
                    lnkBtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lnkBtnSave.Visible = false;
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                BindDateRange();
            }
            svalue = null;
        }
        #endregion

        #region Button Events...
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkBtnNew_Click(object sender, EventArgs e)
        {

            Response.Redirect("GrDelverd.aspx");
        }
        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            UpdateGrDetailDAL obj = new UpdateGrDetailDAL();
            int count = 0; Int64 GrDetail = 0;
            Int64 qty = 0, GrNumber = 0; double Weight = 0;
            if (grdMain.Rows.Count <= 0)
            {
                ShowMessageErr("Please Enter Grid Details");

            }
            else
            {
                CreateDt();
                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGridno");
                    HiddenField hidItemidno = (HiddenField)row.FindControl("hidItemidno");
                    HiddenField hidUnitidno = (HiddenField)row.FindControl("hidUnitidno");
                    HiddenField hidGrdetlidno = (HiddenField)row.FindControl("hidGrdetlidno");
                    Label lblrateType = (Label)row.FindControl("lblrateType");
                    TextBox txtQty = (TextBox)row.FindControl("txtQty");
                    qty = Convert.ToInt64(txtQty.Text);
                    TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                    Weight = Convert.ToDouble(txtWeight.Text);
                    Label GRno = (Label)row.FindControl("lblGrNo");
                    GrNumber = Convert.ToInt64(GRno.Text);
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrdetlidno.Value, hidGrIdno.Value, hidItemidno.Value, hidUnitidno.Value, ((lblrateType.Text) == "Rate") ? "1" : "2", qty, Weight);
                }
                if (qty > 0 && Weight > 0)
                {
                    bool value = obj.UpdateGrDetl(DtTemp, Convert.ToInt64(ddlLocation.SelectedValue));
                    ShowMessage("Update Successfully");
                }
                else
                {
                    ShowMessageErr("Quanity and Weight Must Be Greater then Zero");
                }
            }
        }
        protected void lnkBtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                lnkSubmit.Visible = false; ddlLocation.Enabled = false;
                this.Title = "Edit Gr Detail";
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox Qty = (TextBox)e.Row.FindControl("Qty");
                Qty.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                TextBox txtWeight = (TextBox)e.Row.FindControl("txtWeight");
                txtWeight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

            }

        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion

        #region Functions....
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private DataTable CreateDt()
        {
            DtTemp = ApplicationFunction.CreateTable("tbl", "GrDetl_Idno", "String", "Gr_Idno", "String", "Item_Idno", "String", "Unit_Idno", "String", "Rate_Type", "String", "Qty", "String", "Weight", "String", "Amount", "String");
            return DtTemp;
        }

        private void ClearControls()
        {
            lnkBtnNew.Visible = false;
            txtGrno.Text = "";
            ddlLocation.SelectedValue = "0";
            grdMain.Visible = false;
            hidMtrlRcptid.Value = hidMtrlTrnsfDt.Value = string.Empty;
            lnkSubmit.Visible = true; ddlLocation.Enabled = true;
            ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
            tblUserPref obj = objChallanDelverdDAL.selectUserPref();
            ddlLocation.SelectedValue = Convert.ToString(obj.BaseCity_Idno);

        }

        private void BindGrid()
        {
            if ((ddlLocation.SelectedIndex > 0) && (txtGrno.Text != ""))
            {
                UpdateGrDetailDAL obj = new UpdateGrDetailDAL();

                DataSet Ds = obj.SelecGRdetl(ApplicationFunction.ConnectionString(), "SelectGrDetl", Convert.ToInt64(ddlLocation.SelectedValue), Convert.ToInt64(txtGrno.Text), Convert.ToInt32(ddlDateRange.SelectedValue));
                obj = null;
                LocGrAmnt = 0; OutGrAmnt = 0;
                if (Ds != null && Ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in Ds.Tables[0].Rows)
                    {
                        Int32 DelvPlcIdno = Convert.ToInt32(dr["DelvryPlce_Idno"]);
                        double dGrAmnt = Convert.ToDouble(dr["Amount"]);
                        LocGrAmnt += dGrAmnt;
                        OutGrAmnt += dGrAmnt;

                    }
                }

                ViewState["dt"] = (DataTable)Ds.Tables[0];
                grdMain.DataSource = ViewState["dt"];
                grdMain.DataBind();
                grdMain.Visible = true;

                obj = null;
            }
            else
            {
                grdMain.Visible = false;

                ddlLocation.SelectedIndex = 0;
                ddlLocation.Focus();
                grdMain.DataSource = null;
                grdMain.DataBind();
                ShowMessageErr("Enter Gr no & Location..");
            }
            ;
        }

        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }


        #endregion

        #region Control Events...
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMain.Visible = false;
        }
        #endregion
    }
}