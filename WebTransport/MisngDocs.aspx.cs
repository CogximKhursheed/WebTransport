using System;
using System.Data;
using System.Web.UI;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class MisngDocs : Pagebase
    {
        #region Private Variables...
        private int intFormId = 64;
        #endregion

        #region Page Events...
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
                
                if (base.View == false)
                {
                    lnkBtnPreview.Visible = false;
                }
                txtFrom.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtTo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                BindDateRange();
            }
        }
        #endregion
        
        #region Functions...
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        #endregion

        #region Button Event...
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            MissingDocsDAL missdoc = new MissingDocsDAL();
            DataSet ds = missdoc.GetMIssingDoc(ddlCategory.SelectedValue, ddlDateRange.SelectedValue, (string.IsNullOrEmpty(txtFrom.Text.Trim()) ? 0 : Convert.ToInt64(txtFrom.Text.Trim())), (string.IsNullOrEmpty(txtTo.Text.Trim()) ? 0 : Convert.ToInt64(txtTo.Text.Trim())), "MissingDocs", ApplicationFunction.ConnectionString());
            missdoc = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdMain.DataSource = ds;
                grdMain.DataBind();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }
        #endregion


        
    }
}