using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;


namespace WebTransport
{
    public partial class ExcelFormat: Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
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
                this.BindGrid();
            }
        }

        #endregion

        #region Functions...
    private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void BindGrid()
        {
            ExcelFormatDAL obj = new ExcelFormatDAL();

            var lstGridData = obj.SelectForSearch();

            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
               

                lblcontant.Visible = false;
                divpaging.Visible = false;
            }

       }
   
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        string strMsg = "";
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GetExcel")
            {
                String msg = "";
                Int32 RowId = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurRow = (GridViewRow)grdMain.Rows[RowId];
                HiddenField hidLocId = (HiddenField)CurRow.FindControl("hidexcelid");

                ExcelFormatDAL obj = new ExcelFormatDAL();
                var lst = obj.GetExcel(Convert.ToInt64(hidLocId.Value));
                string ExcelName = (Convert.ToString(DataBinder.Eval(lst[0], "Exl_Name")) + ".xlsx");
                if (lst != null && lst.Count > 0)
                {
                   
                   if(ExcelName!="")
                   {
                       if ((System.IO.Path.GetExtension(ExcelName) == ".xlsx"))
                       {
                           Response.ContentType = "Application/xlsx";
                           Response.AppendHeader("Content-Disposition", "attachment; filename=" + ExcelName);
                           Response.TransmitFile(Server.MapPath("~/ExcelFormat/" + ExcelName));
                           Response.End();

                       }
                       else
                       {
                           msg = "Please Check File Extension only (.xlsx) File!";
                           ShowMessageErr(msg);
                       }

                
                   }
                   else
                   {
                       strMsg = "File Not Found.";
                   }
                  
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
            }
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


    }
}