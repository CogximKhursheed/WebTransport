using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data.Common;
using System.Transactions;

namespace WebTransport
{
    public partial class DistanceMast : Pagebase
    {
        #region Private Variable....
        private int intFormId = 60;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Login.aspx");
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
                txtkms.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                chkStatus.Checked = true;
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                this.ToCity();
                if (Request.QueryString["DistmastId"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["DistmastId"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                ddlFromCity.Focus();
            }
        }
        #endregion

        #region Functions...

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        public bool CheckUserRights(int intFormIdno)
        {
            bool bvalue = false;
            try
            {
                WebTransport.DAL.LoginDAL objLoginDAL = new WebTransport.DAL.LoginDAL();
                UserIdno = Convert.ToInt32(Session["UserIdno"].ToString());
                tblUserRight objUserRghts = objLoginDAL.SelectUserRights(UserIdno, intFormIdno);
                UserRgt_Idno = Convert.ToInt32(objUserRghts.UserRgt_Idno);
                Form_Idno = Convert.ToInt32(objUserRghts.Form_Idno);
                ADD = Convert.ToBoolean(objUserRghts.ADD);
                Edit = Convert.ToBoolean(objUserRghts.Edit);
                View = Convert.ToBoolean(objUserRghts.View);
                Delete = Convert.ToBoolean(objUserRghts.Delete);
                Print = Convert.ToBoolean(objUserRghts.Print);
                if (ADD == false && Edit == false && View == false && Delete == false && Print == false)
                {
                    bvalue = false;
                }
                else
                {
                    bvalue = true;
                }
                objLoginDAL = null;
                return bvalue;
            }
            catch (Exception Ex)
            {
            }
            return bvalue;
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void ToCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


            ddlViaCity.DataSource = ToCity;
            ddlViaCity.DataTextField = "city_name";
            ddlViaCity.DataValueField = "city_idno";
            ddlViaCity.DataBind();
            ddlViaCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void Populate(int ItemIdno)
        {
            DistanceMastDAL objDistMast = new DistanceMastDAL();
            var objDistanceMast = objDistMast.SelectById(ItemIdno);
            objDistMast = null;
            if (objDistanceMast != null)
            {
                ddlFromCity.SelectedValue = Convert.ToString(objDistanceMast.FrmCity_Idno);
                ddlToCity.SelectedValue = Convert.ToString(objDistanceMast.ToCity_Idno);
                ddlViaCity.SelectedValue = Convert.ToString(objDistanceMast.ViaCity_Idno);
                txtkms.Text = Convert.ToString(objDistanceMast.KMs);
                chkStatus.Checked = Convert.ToBoolean(objDistanceMast.Status);
                hidDisMastIdno.Value = Convert.ToString(objDistanceMast.Distance_Idno);
            }
        }
        private void Clear()
        {
            ddlFromCity.SelectedIndex = 0;
            ddlToCity.SelectedIndex = 0;
            txtkms.Text = "";
            ddlViaCity.SelectedIndex = 0;
        }
        #endregion

        #region ButtonEvents....
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            int IntValue = 0;

            if (IsValid)
            {
                if (ddlFromCity.SelectedValue != ddlToCity.SelectedValue)
                {
                    using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DistanceMastDAL objDAL = new DistanceMastDAL();
                        if (Convert.ToString(hidDisMastIdno.Value) == "")
                        {
                            IntValue = objDAL.Insert(Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt64(txtkms.Text.Trim()), chkStatus.Checked, Convert.ToInt64(ddlViaCity.SelectedValue), empIdno);
                        }
                        else
                        {
                            IntValue = objDAL.Update(Convert.ToInt64(hidDisMastIdno.Value), Convert.ToInt64(ddlFromCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt64(txtkms.Text.Trim()), chkStatus.Checked, Convert.ToInt64(ddlViaCity.SelectedValue), empIdno);
                        }

                        if (IntValue > 0 && Convert.ToString(hidDisMastIdno.Value) == "")
                        {
                            Tran.Complete();
                            ShowMessage("Record Save Successfully.");
                            Clear();
                            lnkbtnNew.Visible = false;
                        }
                        else if (IntValue > 0 && Convert.ToString(hidDisMastIdno.Value) != "")
                        {
                            Tran.Complete();
                            ShowMessage("Record Update Successfully.");
                            Clear();
                            lnkbtnNew.Visible = false;
                        }
                        else if (IntValue == -1)
                        {
                            Tran.Dispose();
                            ShowMessageErr("Record already Exist.");
                        }
                        else
                        {
                            Tran.Dispose();
                            ShowMessageErr("Record Not Save.");

                        }
                    }
                }
                else
                { 
                    ShowMessageErr("From City & To City Can not Same.");
                }
            }

        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["DistmastId"] != null)
            {
                this.Populate(Convert.ToInt32(Request.QueryString["DistmastId"]));
                lnkbtnNew.Visible = true;
            }
            else
            {
                Response.Redirect("DistanceMast.aspx");
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("DistanceMast.aspx");
        }
        #endregion

    }
}