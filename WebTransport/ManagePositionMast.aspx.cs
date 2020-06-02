using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AutomobileOnline.Classes;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;

namespace WebTransport
{
    public partial class ManagePositionMast : Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPositionName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");

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
                txtPositionName.Focus();
                this.TotalReords();
                prints.Visible = false;
            }
        }

        #endregion

        #region Functions...

        private void TotalReords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString((from CT in db.tblPositionMasts
                                                                               select new
                                                                               {
                                                                                   PositIdno = CT.Position_id,
                                                                                   PositName = CT.Position_name,
                                                                                   Status = CT.IsActive,
                                                                               }).Count());
            }
        }
        private void BindGrid()
        {
            PositionMastDAL objPosition = new PositionMastDAL();
            var lstGridData = objPosition.SelectForSearch(Convert.ToString(txtPositionName.Text.Trim()));
            objPosition = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                prints.Visible = false;
                imgBtnExcel.Visible = false;
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
                lblTotalRecord.Text = "T. Record (s): 0 ";
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
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("TyrePositionMast.aspx?PostionId=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                PositionMastDAL objTollMast = new PositionMastDAL();
                Int32 intValue = objTollMast.Delete(Convert.ToInt64(e.CommandArgument));
                objTollMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtPositionName.Focus();
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
                int intPositIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intPositIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    PositionMastDAL objDrivMast = new PositionMastDAL();
                    int value = objDrivMast.UpdateStatus(intPositIdno, bStatus);
                    objDrivMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtPositionName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txtPositionName.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");

                int postionidno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PositIdno"));

                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {
                    imgBtnStatus.Visible = true;
                    lnkbtnEdit.Visible = false;
                    grdMain.Columns[3].Visible = false;
                }
                if (base.Delete == false)
                {
                    lnkbtnDelete.Visible = false;
                }
                if ((base.Edit == false) && (base.Delete == false))
                {
                    grdMain.Columns[4].Visible = false;
                }
                
                PositionMastDAL objPosition = new PositionMastDAL();
                var ItemExist = objPosition.existOrnot(Convert.ToInt32(postionidno));
                if (ItemExist != null && ItemExist > 0)
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
            string attachment = "attachment; filename=ManageColorMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.AllowPaging = false;
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