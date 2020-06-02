using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;

namespace WebTransport
{
    public partial class ManageCompanyMast : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 8;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCompanyName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                this.BindGrid();
            }
        }
        #endregion

        #region Button Events...
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            CompanyMastDAL objCompanyMastDAL = new CompanyMastDAL();
            var lstGridData = objCompanyMastDAL.SelectForSearch(txtCompanyName.Text.Trim());
            //.SelectForSearch(Convert.ToString(txtCompanyName.Text.Trim()));
            objCompanyMastDAL = null;
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
            lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
        }

        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("CompanyMast.aspx?CompanyMaster_Idno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                CompanyMastDAL objCompanyMastDAL = new CompanyMastDAL();
                Int32 intValue = objCompanyMastDAL.Delete(Convert.ToInt32(e.CommandArgument));
                objCompanyMastDAL = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtCompanyName.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
            else if (e.CommandName == "cmdstatus")
            {
                int intCompIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intCompIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    CompanyMastDAL objCompanyMastDAL = new CompanyMastDAL();
                    int value = objCompanyMastDAL.UpdateStatus(intCompIdno, bStatus);
                    objCompanyMastDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtCompanyName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txtCompanyName.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {
                    imgBtnStatus.Visible = false;
                    imgBtnEdit.Visible = false;
                    grdMain.Columns[4].Visible = false;
                }
                if (base.Delete == false)
                {
                    imgBtnDelete.Visible = false;
                }
                if ((base.Edit == false) && (base.Delete == false))
                {
                    grdMain.Columns[5].Visible = false;
                }
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }
        #endregion
    }
}