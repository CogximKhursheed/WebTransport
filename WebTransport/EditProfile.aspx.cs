using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class EditProfile : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 12;
        DataTable DtTempA = new DataTable();

        //string siteUrl = ConfigurationManager.AppSettings["siteurl"];
        //string conString = ConfigurationManager.ConnectionStrings["MisConnectionString"].ToString();
        #endregion

        #region Page Load Event.........
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                txtPassword.Attributes.Add("value", "");
                txtEmail.Text = string.Empty;
                this.BindMultpleFromCity();
                this.BindDesignation();
                this.BindState();
                txtUserName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event)");
                txtFatherName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event)");
                txtDOB.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDOJ.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtMobile.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtPhoneNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                this.Populate(Convert.ToInt32(Session["UserIdno"]));
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    lblloc.Visible = false;
                    chklistFromcity.Visible = false;
                }
                else
                {
                    lblloc.Visible = true;
                    chklistFromcity.Visible = true;
                }
                if (chklistFromcity.Items.Count == 0)
                    lblloc.Visible = false;
            }
        }
        #endregion

        #region Control Events................
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
            {
                this.BindCity();
            }
        }
        #endregion

        #region private function...

        private void Populate(int userid)
        {
            EditProfileDal EditDAL = new EditProfileDal();
            tblUserMast userMast = EditDAL.Select(userid);
            string gender = "";
            if (userMast != null)
            {
                txtAddress.Text = userMast.Address;
                ddlState.SelectedValue = userMast.State_Idno.ToString();
                ddlState_SelectedIndexChanged(null, null);
                ddlCity.SelectedValue = userMast.City_Idno.ToString();
                gender = userMast.Gender;

                ddlGender.SelectedValue = gender;
                //if (gender == "M")
                //{
                //    rdbmale.Checked = true;
                //}
                //else
                //{
                //    rdbfemale.Checked = true;
                //}

                if (Convert.ToString(userMast.DOB) != "")
                {
                    txtDOB.Text = Convert.ToString(Convert.ToDateTime(userMast.DOB).ToString("dd-MM-yyyy"));
                }
                else
                {
                    txtDOB.Text = "";
                }
                if (Convert.ToString(userMast.DOJ) != "")
                {
                    txtDOJ.Text = Convert.ToString(Convert.ToDateTime(userMast.DOJ).ToString("dd-MM-yyyy"));
                }
                else
                {
                    txtDOJ.Text = "";
                }
                var lst1 = EditDAL.SelectFromCity(userid);
                var lst2 = EditDAL.SelectFromCityIdno(userid);
                if (lst1.Count > 0)
                {
                    chklistFromcity.DataSource = null;
                    chklistFromcity.DataSource = lst1;
                    chklistFromcity.DataTextField = "city_name";
                    chklistFromcity.DataValueField = "City_Idno";
                    chklistFromcity.DataBind();
                    if (lst2.Count > 0)
                    {
                        for (int i = 0; i < chklistFromcity.Items.Count; i++)
                        {
                            for (int j = 0; j < lst2.Count; j++)
                            {

                                if (Convert.ToInt64(chklistFromcity.Items[i].Value) == lst2[j].FrmCity_Idno)
                                {
                                    chklistFromcity.Items[i].Selected = true;
                                }

                            }
                        }
                    }
                }

                txtEmail.Text = userMast.User_EmailId;
                txtEmail.Enabled = false;
                txtFatherName.Text = userMast.User_FName;
                txtMobile.Text = userMast.Mobile_No;
                txtPassword.Text = WebTransport.Classes.EncryptDecryptPass.decryptPassword(userMast.User_Password);
                txtPassword.Attributes.Add("value", WebTransport.Classes.EncryptDecryptPass.decryptPassword(userMast.User_Password));
                hidPass.Value = WebTransport.Classes.EncryptDecryptPass.decryptPassword(userMast.User_Password);
                txtPhoneNo.Text = userMast.Phone_No;
                txtUserName.Text = userMast.User_Name;
                ddlDesig.SelectedValue = userMast.Desig_Idno.ToString();
                ddlComputerUser.SelectedValue = userMast.Computer_User.ToString();
                hid.Value = userMast.User_Idno.ToString();
            }

            EditDAL = null;
        }

        private void BindCity()
        {

            CityMastDAL OBJCityStateBLL = new CityMastDAL();
            var lst = OBJCityStateBLL.SelectCityCombo();
            OBJCityStateBLL = null;
            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("< Choose City >", "0"));
        }

        private void BindState()
        {
            CityMastDAL obj = new CityMastDAL();
            var lst = obj.SelectState();
            ddlState.DataSource = lst;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("< Choose State >", "0"));
        }

        private void BindDesignation()
        {

            DesignationMasterDAL designation = new DesignationMasterDAL();
            var lst = designation.Select();
            designation = null;
            ddlDesig.DataSource = lst;
            ddlDesig.DataTextField = "Desig_Name";
            ddlDesig.DataValueField = "Desig_Idno";
            ddlDesig.DataBind();
        }

        private void ClearControl()
        {
            txtEmail.Text = string.Empty;
            txtEmail.Enabled = true;
            txtFatherName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPassword.Attributes.Add("value", "");
            hidPass.Value = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlDesig.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            // chkStatus.Enabled = false;
            hid.Value = string.Empty;
            ddlComputerUser.SelectedValue = "0";
        }

        private void BindMultpleFromCity()
        {
            EditProfileDal obj = new EditProfileDal();
            var ToCity = obj.BindToCity();
            obj = null;
            chklistFromcity.DataSource = ToCity;
            chklistFromcity.DataTextField = "city_name";
            chklistFromcity.DataValueField = "City_Idno";
            chklistFromcity.DataBind();
        }

        #endregion

    }
}