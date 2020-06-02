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
    public partial class ManageItemMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 8;
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
                ItemMasterDAL Obj = new ItemMasterDAL();
               // lblTotalRecord.Text = "T. Record (s): " + Obj.Select_ItemMastCount();
                ddlGroupType.Focus();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            Int32 intItemTypeIdno = Convert.ToInt32(ddlGroupType.SelectedValue);
            string strItemName = Convert.ToString(txtItemName.Text.Trim());
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var lstGridData = objItemMast.SelectForSearch(intItemTypeIdno, strItemName);
            objItemMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("GroupName", typeof(string));
                dt.Columns.Add("UOM", typeof(string));
               
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["ItemName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ItemName"));
                    dr["GroupName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "IgrpName"));
                    dr["UOM"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "UOMName"));

                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;

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
                lblTotalRecord.Text = "T. Record(s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ItemMaster.xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in Dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in Dt.Rows)
            {
                str = "";
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        private void BindGroupType()
        {
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var objlist = objItemMast.SelectGroupType();
            ddlGroupType.DataSource = objlist;
            ddlGroupType.DataTextField = "IGrp_Name";
            ddlGroupType.DataValueField = "IGrp_Idno";
            ddlGroupType.DataBind();
            objItemMast = null;
            ddlGroupType.Items.Insert(0, new ListItem("-- Select Item --", "0"));
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
                Response.Redirect("ItemMaster.aspx?ItemIdno=" + e.CommandArgument, true);
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
                    ItemMasterDAL objItemMast = new ItemMasterDAL();
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
                        strMsg = "Status not updated.";
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                ItemMasterDAL objItemMast = new ItemMasterDAL();
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
                        strMsg = "Record can not be deleted. It is in use.";
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
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));

                // Used to hide Delete button if ItemgrpId exists in Rate Master,Goods Received, Quotation,GR Preparation,Commission Master
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string ItemIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemIdno"));
                if (ItemIdno != "")
                {
                    ItemMasterDAL obj = new ItemMasterDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(ItemIdno));
                    if (ItemExist != null && ItemExist.Count > 0)
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
        #endregion

        #region print...
        private void ExportGridView()
        {
            Int32 intItemTypeIdno = Convert.ToInt32(ddlGroupType.SelectedValue);
            string strItemName = Convert.ToString(txtItemName.Text.Trim());
            ItemMasterDAL objItemMast = new ItemMasterDAL();
            var lstGridData = objItemMast.SelectForSearch(intItemTypeIdno, strItemName);
            objItemMast = null;
            grdMain.AllowPaging = false;
            string attachment = "attachment; filename=ManageItemMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[3].Visible = true;
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
            //grdMain.GridLines = GridLines.Both;
            //PrepareGridViewForExport(grdMain);
            //ExportGridView();
            //grdMain.Columns[3].Visible = true;
            //grdMain.Columns[4].Visible = true;

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
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