using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AutomobileOnline.Classes;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;

namespace WebTransport
{
    public partial class ManageTitleMaster : Pagebase
    {
        #region Private variable...
        private int intFormId = 27;
        #endregion

        #region Page Load...
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
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                txtTitle.Focus();
            }
            txtTitle.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndDotAndSlash(event);");
            this.countall();
        }
        #endregion

        #region Functions...
        public void countall()
        {
            TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
           
            Int64 total = 0;

            total = objclsTitleMaster.selectcount();
            if (total > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + total;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindGrid()
        {
            
                
            TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
            var lstGridData = objclsTitleMaster.SelectForSearch(Convert.ToString(txtTitle.Text.Trim()));
            objclsTitleMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
            }
            else 
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): 0 ";
                imgBtnExcel.Visible = false;
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

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("TitleMaster.aspx?TitlIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
                Int32 intValue = objclsTitleMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsTitleMaster = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtTitle.Focus();
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
                int intTitlIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intTitlIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                  TitleMasterDAL objclsTitleMaster = new TitleMasterDAL();
                  int value = objclsTitleMaster.UpdateStatus(intTitlIdno, bStatus, empIdno);
                    objclsTitleMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtTitle.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txtTitle.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                int titleidno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TitlIdno"));

                if (titleidno > 0)
                {

                    TitleMasterDAL obj = new TitleMasterDAL();

                    var ItemExist = obj.CheckTitleExistInOtherMaster(Convert.ToInt32(titleidno));
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;

                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }

                    if (status == false)
                        imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                    else
                        imgBtnStatus.ImageUrl = "~/Images/active.png";
                }
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[3].Visible = false;
            grdMain.Columns[4].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[3].Visible = true;
            grdMain.Columns[4].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}