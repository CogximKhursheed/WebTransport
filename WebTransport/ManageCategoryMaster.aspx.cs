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
    public partial class ManageCategoryMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 9;
        #endregion

        #region Page Load Events...
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
                prints.Visible = false;
                //this.BindGrid();
            }
            txtConType.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            CategoryMasterDAL objclsCategoryMaster = new CategoryMasterDAL();
            lblTotalRecord.Text = "T. Record (s): " + objclsCategoryMaster.SelectTotal();
            txtConType.Focus();
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            CategoryMasterDAL objclsCategoryMaster = new CategoryMasterDAL();
            var lstGridData = objclsCategoryMaster.SelectAll(Convert.ToString(txtConType.Text.Trim()));
            objclsCategoryMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();

                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;
                prints.Visible = false;
                imgBtnExcel.Visible = false;
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));

                // Used to hide Delete button if ItemgrpId exists in Item Master,Fleet Mgmt ItemMast
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string CatIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Cat_Idno"));
                if (CatIdno != "")
                {
                    CategoryMasterDAL obj = new CategoryMasterDAL();
                    //To hide delete button

                    var CatExistInMast = obj.CheckCategoryExistInLedgerMaster(Convert.ToInt32(CatIdno));
                    if (CatExistInMast != null && CatExistInMast.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }
                // end----

                imgBtnStatus.Visible = true;

                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("CategoryMaster.aspx?Category_Idno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                CategoryMasterDAL objclsCategoryMaster = new CategoryMasterDAL();
                Int32 intValue = objclsCategoryMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsCategoryMaster = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    //  ddlGroupType.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                this.BindGrid();

            }
            else if (e.CommandName == "cmdstatus")
            {
                int intTypeIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intTypeIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    CategoryMasterDAL objclsCategoryMaster = new CategoryMasterDAL();
                    int value = objclsCategoryMaster.UpdateStatus(intTypeIdno, bStatus, empIdno);
                    objclsCategoryMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        //ddlGroupType.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            //ddlGroupType.Focus();
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion

        #region Buttons Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            CategoryMasterDAL objclsCategoryMaster = new CategoryMasterDAL();
            var lstGridData = objclsCategoryMaster.SelectAll(Convert.ToString(txtConType.Text.Trim()));
            objclsCategoryMaster = null;
            grdMain.AllowPaging = false;
            string attachment = "attachment; filename=CategoryMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[3].Visible = false;
            grdMain.Columns[4].Visible = false;
            grdMain.DataSource = lstGridData;
            grdMain.DataBind();
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