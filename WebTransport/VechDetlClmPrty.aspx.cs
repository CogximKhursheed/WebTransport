using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Net;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class VechDetlClmPrty : Pagebase
    {
        #region Private Variable...
        //int intFormId = 57;
        //int cOMPiD;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!Page.IsPostBack)
            {
                this.BindDateRange(); this.BindCity(); this.BindTyreName(); this.BindTyreType();
                this.BindGrid();
                this.ClearControls();
            }
        }
        #endregion

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            BindDropdownDAL objDropDal =new BindDropdownDAL();
            Int64 StckDetlIdno = 0;
            VechDetlClmPrtyDAL objDAL = new VechDetlClmPrtyDAL();
            Int32 intYearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int64 intLocIdno = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            Int64 intItemIdno = string.IsNullOrEmpty(Convert.ToString(ddlItemName.SelectedValue)) ? 0 : Convert.ToInt64(ddlItemName.SelectedValue);
            string strSerialNo = string.IsNullOrEmpty(Convert.ToString(txtSerialNo.Text)) ? "" : Convert.ToString(txtSerialNo.Text);
            string strCompName = string.IsNullOrEmpty(Convert.ToString(txtCompName.Text)) ? "" : Convert.ToString(txtCompName.Text);
            string strPurFrom = string.IsNullOrEmpty(Convert.ToString(txtPurchaseFrom.Text)) ? "" : Convert.ToString(txtPurchaseFrom.Text);
            Int32 intTyreType = string.IsNullOrEmpty(Convert.ToString(ddltype.SelectedValue)) ? 0 : Convert.ToInt32(ddltype.SelectedValue);
            Int64 intAcntIdno = string.IsNullOrEmpty(Convert.ToString(ddltype.SelectedValue)) ? 0 : Convert.ToInt64(ddltype.SelectedValue);
            double ddlOpenRate = string.IsNullOrEmpty(Convert.ToString(txtOpenRate.Text.Trim())) ? 0 : Convert.ToDouble(txtOpenRate.Text.Trim());
            //Check Conditions Before Save Peeyush Kasuhik
            var lst1 = objDropDal.CheckSerialNo(strSerialNo);
            if (lst1 != null && lst1 > 0) { ShowMessageErr("Serial No already Exists !"); return; }
            if (string.IsNullOrEmpty(hidstckidno.Value) == true)
            {
                StckDetlIdno = objDAL.Insert(strSerialNo, intLocIdno, intItemIdno, strCompName, intTyreType, strPurFrom, intYearIdno, ddlOpenRate, (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["AcntIdno"])) ? 0 : Convert.ToInt64(Request.QueryString["AcntIdno"])));
            }
            else
            {
                StckDetlIdno = objDAL.Update((string.IsNullOrEmpty(Convert.ToString(hidstckidno.Value)) ? 0 : Convert.ToInt64(hidstckidno.Value)), strSerialNo, intLocIdno, intItemIdno, strCompName, intTyreType, strPurFrom, intYearIdno, ddlOpenRate, (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["AcntIdno"])) ? 0 : Convert.ToInt64(Request.QueryString["AcntIdno"])));
            }
            if (StckDetlIdno == 0)
            {

                ShowMessageErr("Record  Not Saved ");
            }
            else if (StckDetlIdno == -1)
            {
                ShowMessageErr("Record already exists.");
            }
            else if (StckDetlIdno > 0)
            {
                if (hidstckidno.Value == "")
                {
                    ShowMessage("Record Saved Successfully");
                }
                else
                {
                    ShowMessage("Record Updated Successfully.");
                }
                this.ClearControls();
                this.BindGrid();
            }

        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);

        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        #region Bind DropDown...

        private void BindCity()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var lst = obj.BindCityAll();
            if (lst != null && lst.Count > 0)
            {
                ddlLocation.DataSource = lst;
                ddlLocation.DataTextField = "City_Name";
                ddlLocation.DataValueField = "City_Idno";
                ddlLocation.DataBind();
            }
            ddlLocation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindTyreType()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.SelectTyreType();
            obj = null;
            if (lst != null && lst.Count > 0)
            {
                ddltype.DataSource = lst;
                ddltype.DataTextField = "TyreType_Name";
                ddltype.DataValueField = "TyreType_IdNo";
                ddltype.DataBind();
            }
            ddltype.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindTyreName()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var itemname = obj.SelectOnlyTyre();
            obj = null;
            if (itemname != null && itemname.Count > 0)
            {
                ddlItemName.DataSource = itemname;
                ddlItemName.DataTextField = "Item_Name";
                ddlItemName.DataValueField = "Item_idno";
                ddlItemName.DataBind();
            }
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

        #region Functions...
        private void Populate(Int64 StckDetlIdno)
        {
            VechDetlClmPrtyDAL obj = new VechDetlClmPrtyDAL();
            Stckdetl lst = obj.SelectById(StckDetlIdno);
            if (lst != null)
            {
                ddlDateRange.SelectedValue = string.IsNullOrEmpty(Convert.ToString(lst.yearId)) ? "0" : Convert.ToString(lst.yearId);
                ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(lst.Loc_Idno)) ? "0" : Convert.ToString(lst.Loc_Idno);
                ddlItemName.SelectedValue = string.IsNullOrEmpty(Convert.ToString(lst.ItemIdno)) ? "0" : Convert.ToString(lst.ItemIdno);
                txtSerialNo.Text = string.IsNullOrEmpty(Convert.ToString(lst.SerialNo)) ? "" : Convert.ToString(lst.SerialNo);
                txtCompName.Text = string.IsNullOrEmpty(Convert.ToString(lst.CompName)) ? "" : Convert.ToString(lst.CompName);
                txtPurchaseFrom.Text = string.IsNullOrEmpty(Convert.ToString(lst.PurFrom)) ? "" : Convert.ToString(lst.PurFrom);
                ddltype.SelectedValue = string.IsNullOrEmpty(Convert.ToString(lst.Type)) ? "0" : Convert.ToString(lst.Type);
                txtOpenRate.Text = string.IsNullOrEmpty(Convert.ToString(lst.OpenRate)) ? "" : Convert.ToString(lst.OpenRate);
                hidstckidno.Value = StckDetlIdno.ToString();
            }
            else
            {
                this.ClearControls();
            }
        }
        private void ClearControls()
        {
            txtSerialNo.Text = txtCompName.Text = txtPurchaseFrom.Text = ""; txtOpenRate.Text = "0.00";
        }
        private void BindGrid()
        {
            VechDetlClmPrtyDAL objGrid = new VechDetlClmPrtyDAL();
            Int64 AcntIdno = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["AcntIdno"])) ? 0 : Convert.ToInt64(Request.QueryString["AcntIdno"]);
            if (AcntIdno > 0)
            {
                var lst = objGrid.SelectAcntName(AcntIdno);
                if (lst != null) { lblAcntName.Text = string.IsNullOrEmpty(Convert.ToString(lst.Acnt_Name)) ? "" : Convert.ToString(lst.Acnt_Name.ToUpper()); lblAcntName.Visible = true; }
                Int64 LocIdno = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
                Int32 YearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
                IList lstGridData = objGrid.SelectAllDetails(AcntIdno, LocIdno, YearIdno);
                objGrid = null;
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    grdMain.DataSource = lstGridData;
                    grdMain.DataBind();
                }
                else
                {
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                }
            }
            else
            {
                Response.Redirect("");
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.ClearControls();
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                Int64 stckDetlIdno = string.IsNullOrEmpty(Convert.ToString(e.CommandArgument)) ? 0 : Convert.ToInt64(e.CommandArgument);
                this.Populate(stckDetlIdno);
            }
            else if (e.CommandName == "cmddelete")
            {
                VechDetlClmPrtyDAL obj = new VechDetlClmPrtyDAL();
                Int64 stckDetlIdno = string.IsNullOrEmpty(Convert.ToString(e.CommandArgument)) ? 0 : Convert.ToInt64(e.CommandArgument);
                int value = obj.Delete(stckDetlIdno);
                obj = null;
                if(value > 0)
                  ShowMessage("Record Deleted Successfully"); 
                this.ClearControls();
                this.BindGrid();
            }
        }
        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");
                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }
        #endregion

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearControls();
            this.BindGrid();
            
        }

    }
}
