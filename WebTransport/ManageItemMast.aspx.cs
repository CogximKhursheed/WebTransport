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
using System.Threading;
using System.Data;

namespace WebTransport
{
    public partial class ManageItemMast : Pagebase
    {
        #region Private Variable....
        private int intFormId = 8;
        DataTable dt = new DataTable();
        DataTable CSVTable = new DataTable();
        bool st;
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
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }

                this.BindGroupType();
                this.BindItemType();
                this.Countall();
                ddlGroupType.Focus();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            Int32 intItemTypeIdno = Convert.ToInt32(ddlGroupType.SelectedValue);
            string strItemName = Convert.ToString(txtItemName.Text.Trim());
            Int64 strItemTyp = Convert.ToInt64(ddlItemType.SelectedValue);
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lstGridData = objItemMast.SelectForSearch(intItemTypeIdno, strItemName, strItemTyp);

            objItemMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
              
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;
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
        public void Countall()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            Int64 count = objItemMast.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }

        private void BindItemType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var lst = objItemMast.SelectItemType();
            ddlItemType.DataSource = lst;
            ddlItemType.DataTextField = "ItemType_Name";
            ddlItemType.DataValueField = "ItemTpye_Idno";
            ddlItemType.DataBind();
            objItemMast = null;
            ddlItemType.Items.Insert(0, new ListItem("-- Select Item Type --", "0"));
        }
        private void BindGroupType()
        {
            ItemMastPurDAL objItemMast = new ItemMastPurDAL();
            var objlist = objItemMast.SelectGroupType();
            ddlGroupType.DataSource = objlist;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            objItemMast = null;
            ddlGroupType.Items.Insert(0, new ListItem("--- Select Item ---", "0"));
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
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("ItemMast.aspx?ItemIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmdstatus")
            {
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intitemIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intitemIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    ItemMastPurDAL objItemMast = new ItemMastPurDAL();
                    int value = objItemMast.UpdateStatus(intitemIdno, bStatus, empIdno);
                    objItemMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtItemName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated!";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                ItemMastPurDAL objItemMast = new ItemMastPurDAL();
                long intValue = objItemMast.Delete(Convert.ToInt32(e.CommandArgument));
                objItemMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlGroupType.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use!";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                //
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                int itemidno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ItemIdno"));

                if (itemidno > 0)
                {

                    ItemMastPurDAL objItemMast = new ItemMastPurDAL();
                    var ItemExist = objItemMast.SelectSearch(Convert.ToInt32(itemidno));
                    if (ItemExist != null && ItemExist > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                    imgBtnStatus.Visible = true;

                    if (status == false)
                        imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                    else
                        imgBtnStatus.ImageUrl = "~/Images/active.png";
                }
                
            }
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ItemReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            // grdMain.Columns[3].Visible = false;
            grdMain.Columns[9].Visible = false;
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
                else if (gv.Controls[i].GetType() == typeof(ImageButton))
                {
                    l.Text = (gv.Controls[i] as ImageButton).ImageUrl == "~/Images/inactive.png" ? "InActive" : "Active";
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
            grdMain.Columns[8].Visible = true;
            grdMain.Columns[9].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        } 
        #endregion
    }
}