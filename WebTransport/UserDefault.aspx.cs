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
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;

namespace WebTransport
{
    public partial class UserDefault : Pagebase
    {
        #region Variables declaration...

        string conString = "";
        static FinYear UFinYear = new FinYear();
        UserDefaultDAL objUserDefaultDAL = new UserDefaultDAL();
        private int intFormId = 26;

        #endregion

        #region PageLoad Events...

        protected void Page_Load(object sender, EventArgs e)
        {
            //conString = ConfigurationManager.ConnectionStrings["MisConnectionString"].ToString();
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                //Response.Redirect("PermissionDenied.aspx");
            }
            if (!Page.IsPostBack)
            {
                BindDateRange();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                this.BindUSERs();
                this.BindState();
                BindItemUpdate();
                BindDropdown();
                if (base.UserIdno != 1)
                {

                }
                ddlDateRange.Focus();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                    drpUserNm.Enabled = true;
                else
                {
                    drpUserNm.Enabled = false;
                    Populate(Convert.ToInt32(Session["UserIdno"]));
                }
             }
        }
        private void Populate(int useridno)
        {
            UserDefaultDAL objUserDefaultDAL = new UserDefaultDAL();
            var objUserDefault = objUserDefaultDAL.SelectById(useridno);
            objUserDefaultDAL = null;
            try
            {
                if (objUserDefault != null)
                {
                    ddlState.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.StateId)) ? "0" : Convert.ToString(objUserDefault.StateId);
                    this.ddlState_SelectedIndexChanged(null, null);
                    ddlCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.CityId)) ? "0" : Convert.ToString(objUserDefault.CityId);
                    //BindCity(Convert.ToInt32(ddlState.SelectedValue));
                    ddlFromCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.FromCity_idno)) ? "0" : Convert.ToString(objUserDefault.FromCity_idno);
                    hidUsrDefaultID.Value = string.IsNullOrEmpty(Convert.ToString(objUserDefault.UserDefault_Idno)) ? "0" : Convert.ToString(objUserDefault.UserDefault_Idno);
                    ddlDateRange.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.Year_idno)) ? "0" : Convert.ToString(objUserDefault.Year_idno);
                    ddlSender.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.SenderIdno)) ? "0" : Convert.ToString(objUserDefault.SenderIdno);
                    ddlItemName.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.ItemIdno)) ? "0" : Convert.ToString(objUserDefault.ItemIdno);
                    ddlunitname.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.UnitIdno)) ? "0" : Convert.ToString(objUserDefault.UnitIdno);
                    drpUserNm.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.User_idno)) ? "0" : Convert.ToString(objUserDefault.User_idno);
                    ddlTaxPaidBy.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.STax_Typ)) ? "0" : Convert.ToString(objUserDefault.STax_Typ);
                    ddlGRType.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objUserDefault.Gr_Type)) ? "0" : Convert.ToString(objUserDefault.Gr_Type);
                }
                else
                {
                    hidUsrDefaultID.Value = "0";
                    ddlFromCity.SelectedIndex = 0;
                    ddlDateRange.SelectedIndex = ddlState.SelectedIndex = ddlCity.SelectedIndex = ddlunitname.SelectedIndex = ddlTaxPaidBy.SelectedIndex = ddlItemName.SelectedIndex =ddlGRType.SelectedIndex =0;
                    
                }
            }
            catch(Exception e) { }
        }

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            objUserDefaultDAL = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        private void BindUSERs()
        {
            UserDefaultDAL obj = new UserDefaultDAL();
            var lst = obj.SelectALLUsers();
            drpUserNm.DataSource = lst;
            drpUserNm.DataTextField = "Emp_Name";
            drpUserNm.DataValueField = "User_Idno";
            drpUserNm.DataBind();
            drpUserNm.Items.Insert(0, new ListItem("--Select--", "0"));
            //if (base.UserIdno != 1)
            //{
            //    drpUserNm.SelectedValue = Convert.ToString(Session["UserIdno"]);
            //    drpUserNm.Enabled = false;
            //}
            if (drpUserNm.SelectedIndex == 0)
            {
                ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
                ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
            }
            drpUserNm_SelectedIndexChanged(null, null);
        }

        private void BindFromCity(Int64 User_idno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }


        #endregion

        #region Buttons Events......

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            UserDefaultDAL ObjDAL = new UserDefaultDAL();
            Int64 intUserDefaultIdno = 0; Int64 userId = 0; Int64 FromCity = 0;
            if (drpUserNm.SelectedIndex <= 0)
            {
                strMsg = "Select user Name!";
                drpUserNm.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + strMsg + "')", true);
                return;
            }
            else
            {
                userId = Convert.ToInt64(drpUserNm.SelectedValue);
            }
            if (ddlFromCity.SelectedIndex <= 0)
            {
                strMsg = "Select From City!";
                drpUserNm.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + strMsg + "')", true);
                return;
            }
            else
            {
                FromCity = Convert.ToInt64(ddlFromCity.SelectedValue);
            }
            if (Convert.ToInt32(hidUsrDefaultID.Value) > 0)
            {
                intUserDefaultIdno = ObjDAL.Update(userId, FromCity, Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64((hidUsrDefaultID.Value) == "" ? "0" : hidUsrDefaultID.Value), Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlSender.SelectedValue), Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlunitname.SelectedValue), Convert.ToInt32(ddlTaxPaidBy.SelectedValue), Convert.ToInt32(ddlGRType.SelectedValue));
            }
            else
            {
                intUserDefaultIdno = ObjDAL.Insert(userId, FromCity, Convert.ToInt64(ddlDateRange.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt64(ddlSender.SelectedValue), Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlunitname.SelectedValue), Convert.ToInt32(ddlTaxPaidBy.SelectedValue), Convert.ToInt32(ddlGRType.SelectedValue));
            }
            ObjDAL = null;
            if (intUserDefaultIdno > 0)
            {
                strMsg = "Record Save successfully.";
            }
            else
            {
                strMsg = "Record Update successfully.";
            }
            if (Convert.ToString(Session["UserClass"]) == "Admin")
                ClearAllControl();
            else
                Populate(Convert.ToInt32(Session["UserIdno"]));
            ShowMessage(strMsg);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hidUsrDefaultID.Value) > 0)
            {
                Populate(Convert.ToInt32(drpUserNm.SelectedValue));
            }
            else
            {
                ClearAllControl();
            }
        }

        public void ClearAllControl()
        {
            hidUsrDefaultID.Value = string.Empty;
            drpUserNm.SelectedIndex = ddlState.SelectedIndex = ddlCity.SelectedIndex = ddlFromCity.SelectedIndex = ddlDateRange.SelectedIndex =ddlGRType.SelectedIndex =0;
        }
        #endregion

        #region DropdownList Events....

        protected void drpUserNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUserNm.SelectedIndex > 0)
            {
                Populate(Convert.ToInt32(drpUserNm.SelectedValue));
            }
            drpUserNm.Focus();
        }

        #endregion

        #region Functions...
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "CityIdno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity(int stateIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCity(stateIdno);
            ddlCity.DataSource = FrmCity;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            obj = null;
            ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindState()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindState();
            ddlState.DataSource = FrmCity;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            obj = null;
            ddlState.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindItemUpdate()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var itemname = obj.BindItemNameUpdate();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            obj = null;
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            var UnitName = obj.BindUnitName();

            ddlunitname.DataSource = UnitName;
            ddlunitname.DataTextField = "UOM_Name";
            ddlunitname.DataValueField = "UOM_idno";
            ddlunitname.DataBind();
            obj = null;
            ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            obj = null;
            ddlSender.DataSource = senderLst;
            ddlSender.DataTextField = "Acnt_Name";
            ddlSender.DataValueField = "Acnt_Idno";
            ddlSender.DataBind();
            obj = null;
            ddlSender.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
   #endregion

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity(Convert.ToInt32(ddlState.SelectedValue));
          
            ddlState.Focus();
        }
    }
}