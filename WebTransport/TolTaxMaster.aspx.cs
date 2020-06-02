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
    public partial class TolTaxMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 61;
        #endregion

        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {

            drpCity.Focus();
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
                    lnkbtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                txtTolTaxName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                chkStatus.Checked = true;
                this.BindCity();
                this.BindLorryType();
                if (Request.QueryString["TollTaxId"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["TollTaxid"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                drpCity.Focus();
            }
        }
        #endregion
         
        #region Button Events...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            TollMastDAL objTolMast = new TollMastDAL();
            Int64 intCityIdno = 0;
            if (drpCity.SelectedValue != ddlToCity.SelectedValue)
            { 
                if (string.IsNullOrEmpty(hidcityidno.Value) == true)
                {
                    intCityIdno = objTolMast.InsertTollMaster(txtTolTaxName.Text.Trim(), Convert.ToInt64(drpCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), float.Parse(string.IsNullOrEmpty(txtAmount.Text) ? "0" : txtAmount.Text), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(ddllorytype.SelectedValue), empIdno);
                }
                else
                {
                    intCityIdno = objTolMast.UpdateTollMaster(txtTolTaxName.Text.Trim(), Convert.ToInt32(hidcityidno.Value), Convert.ToInt64(drpCity.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), float.Parse(txtAmount.Text), Convert.ToBoolean(chkStatus.Checked), Convert.ToInt32(ddllorytype.SelectedValue), empIdno);
                }
                objTolMast = null;

                if (intCityIdno > 0)
                {
                    if (string.IsNullOrEmpty(hidcityidno.Value) == false)
                    {
                        ShowMessage("Record updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Record saved successfully.");
                    }
                    ClearControls();
                    lnkbtnNew.Visible = false;

                }
                else if (intCityIdno < 0)
                {
                    ShowMessageErr("Record already exists.");
                }
                else
                {
                    if (string.IsNullOrEmpty(hidcityidno.Value) == false)
                    {
                        ShowMessageErr("Record not updated.");
                    }
                    else
                    {
                        ShowMessageErr("Record not saved.");
                    }
                } 
                drpCity.Focus();
            }
            else
            {
                ShowMessageErr("From City And To City Can't Same"); 
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            //for test
            Response.Redirect("TolTaxMaster.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(hidcityidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidcityidno.Value));
            }

        }
        #endregion

        #region Miscellaneous Events...
        private void Populate(int Tolltaxid)
        {
            TollMastDAL objtollmast = new TollMastDAL();
            var objTollMast = objtollmast.SelectById(Tolltaxid);
            objtollmast = null;
            if (objTollMast != null)
            {
                drpCity.SelectedValue = Convert.ToString(objTollMast.city);
                ddlToCity.SelectedValue = Convert.ToString(objTollMast.Tocity);
                txtTolTaxName.Text = Convert.ToString(objTollMast.Tolltax_name);
                txtAmount.Text = (txtAmount.Text);
                //txtAmount.Text = Convert.ToString(txtAmount.Text);
                txtAmount.Text = Convert.ToString(objTollMast.Amount);
                chkStatus.Checked = Convert.ToBoolean(objTollMast.Status);
                hidcityidno.Value = Convert.ToString(objTollMast.Toll_id);
                ddllorytype.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objTollMast.LorryType_Idno)) ? "0" : Convert.ToString(objTollMast.LorryType_Idno);
                drpCity.Focus();
            }
        }
        private void ClearControls()
        {
            txtAmount.Text = txtTolTaxName.Text = string.Empty;
            drpCity.SelectedIndex = -1;
            ddlToCity.SelectedIndex = -1;
            ddllorytype.SelectedIndex = 0;

        }
        private void BindLorryType()
        {
            TollMastDAL Obj = new TollMastDAL();
            var EMPS = Obj.SelectLorryType();
            ddllorytype.DataSource = EMPS;
            ddllorytype.DataTextField = "Lorry_Type";
            ddllorytype.DataValueField = "Id";
            ddllorytype.DataBind();
            ddllorytype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Obj = null;
        }
        private void BindCity()
        {
            TollMastDAL objTollMastDAL = new TollMastDAL();
            var lst = objTollMastDAL.BindCityAll();
            drpCity.DataSource = lst;
            drpCity.DataTextField = "City_Name";
            drpCity.DataValueField = "City_Idno";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("< Choose City >", "0"));


            ddlToCity.DataSource = lst;
            ddlToCity.DataTextField = "City_Name";
            ddlToCity.DataValueField = "City_Idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new ListItem("< Choose City >", "0"));
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        ///// <summary>
        ///// To Populate all controls
        ///// </summary>
        ///// <param name="tolltaxid"></param>
        #endregion
         
    }
}