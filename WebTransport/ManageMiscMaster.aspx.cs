using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using System.Threading;
using System.Data;


namespace WebTransport
{
    public partial class ManageMiscMaster : Pagebase
    {
        #region "Page Load"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                ddlType.Focus();
            }
        }
        #endregion

        #region "Functions"
        private void BindGrid()
        {
            MiscDAL objM = new MiscDAL();
            Int64 IMiscType = Convert.ToInt64(ddlType.SelectedValue);
            string strName = Convert.ToString(txtName.Text.Trim());
            var lst = objM.SelectForSearch(IMiscType, strName);
            objM = null;
            if (lst != null && lst.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Name", typeof(string));            
                for (int i = 0; i < lst.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Type"] = Convert.ToString(DataBinder.Eval(lst[i], "Tran_Type"));
                    dr["Name"] = Convert.ToString(DataBinder.Eval(lst[i], "Misc_Name"));
                    
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["dt"] = dt;
                }
                grdMain.DataSource = lst;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s):" + lst.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lst.Count.ToString();
                lblcontant.Visible = true;
               // imgBtnExcel.Visible = true;
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MiscMaster.xls"));
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
                Response.Redirect("MiscMaster.aspx?MID=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmdstatus")
            {
                //Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intMiscIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intMiscIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    MiscDAL objItemMast = new MiscDAL();
                    int value = objItemMast.UpdateStatus(intMiscIdno, bStatus);
                    objItemMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtName.Focus();
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
                MiscDAL objMisc = new MiscDAL();
                long intValue = objMisc.DeleteMisc(Convert.ToInt32(e.CommandArgument));
                objMisc = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlType.Focus();
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
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Misc_Status"));

                // Used to hide Delete button if ItemgrpId exists in Rate Master,Goods Received, Quotation,GR Preparation,Commission Master
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string MiscIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Misc_Idno"));
                if (MiscIdno != "")
                {
                    MiscDAL obj = new MiscDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(MiscIdno));
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

        #region "Button Event"
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dt"];
                Export(dt);
            }
        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region "Print"
        private void ExportGridView()
        {
            Int32 intItemTypeIdno = Convert.ToInt32(ddlType.SelectedValue);
            string strItemName = Convert.ToString(txtName.Text.Trim());
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
        #endregion
    }
}