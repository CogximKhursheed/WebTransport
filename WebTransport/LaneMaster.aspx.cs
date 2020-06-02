using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
namespace WebTransport
{
    public partial class LaneMaster : Pagebase
    {
        #region Private Variables...
        private int intFormId = 17;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
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
                if (Request.QueryString["LaneID"] != null)
                {
                    hidLaneidno.Value = Convert.ToString(Request.QueryString["LaneID"]);
                   this.Populate(Convert.ToInt16(Request.QueryString["LaneID"]));
                    lnkbtnNew.Visible = true;
                    lnkbtnSave.Text = "Update";
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("LaneMaster.aspx");
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidLaneidno.Value) == false)
            {
                //this.Populate(Convert.ToInt16(hidLaneidno.Value));
            }
            else
            {
               txtLaneName.Text = "";
                //ddltype.SelectedIndex = 0;
            }
        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            int Result = 0;
            LaneMasterDAL DAL = new LaneMasterDAL();
            if (string.IsNullOrEmpty(hidLaneidno.Value) == false)
            {
                Result = DAL.Update(Convert.ToInt16(hidLaneidno.Value), txtLaneName.Text.Trim());
            }
            else
            {
                Result = DAL.Save(txtLaneName.Text.Trim());
            }

            string strMsg = "";
            if (Result > 0)
            {
                if (string.IsNullOrEmpty(hidLaneidno.Value) == false)
                {
                    ShowMessage("Record updated successfully.");
                }
                else
                {
                    ShowMessage("Record saved successfully.");
                }
                this.ClearControls();
            }
            else if (Result < 0)
            {
                ShowMessageErr("Record already exists.");
            }
            else
            {
                if (string.IsNullOrEmpty(hidLaneidno.Value) == false)
                {
                    ShowMessageErr("Record not updated.");
                }
                else
                {
                    ShowMessageErr("Record not saved.");
                }
            }
            //ddltype.Focus();
        }
        #endregion

        #region Functions..

        public void Populate(int LaneId)
        {
            LaneMasterDAL DAL = new LaneMasterDAL();
            var  objLanMaster= DAL.SelectByID(LaneId);
            if (objLanMaster != null)
            {
                //ddltype.SelectedValue = Convert.ToString(tblMIS.MIS_Type);
                txtLaneName.Text = Convert.ToString(objLanMaster.Lane_Name);

            }
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ClearControls()
        {
            txtLaneName.Text = "";
            hidLaneidno.Value = "";
            
        }
        #endregion
    }
}