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
using System.Data;

namespace WebTransport
{
    public partial class ManageItmGrpMaster : Pagebase
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
                //ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
                //var lstGridData = objclsItmGrpMaster.SelectAll(Convert.ToString(txtGName.Text.Trim()));
                //if (lstGridData != null && lstGridData.Count > 0)
                //{
                //    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //}
             
            }
            txtGName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            txtGName.Focus();
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            var lstGridData = objclsItmGrpMaster.SelectAll(Convert.ToString(txtGName.Text.Trim()));
            objclsItmGrpMaster = null;

            if (lstGridData != null && lstGridData.Count > 0)
            {
                 
                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("Item Group Name", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["Item Group Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "IGrp_Name"));
                    if (Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status")) == true)
                    {
                        dr["Status"] = "Active";
                    }
                    else
                    {
                        dr["Status"] = "Inactive";
                    }
                    dt.Rows.Add(dr);
                   
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();

                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;
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
                ImageButton lnkbtnDelete = (ImageButton)e.Row.FindControl("lnkbtnDelete");
                string IGrpIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "IGrp_Idno"));
                if (IGrpIdno != "")
                {
                    ItmGrpMasterDAL obj = new ItmGrpMasterDAL();
                    var ItemGrpExistInItemMast = obj.CheckItemGrpExistInItemMaster(Convert.ToInt32(IGrpIdno));
                    if (ItemGrpExistInItemMast != null && ItemGrpExistInItemMast.Count > 0)
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
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("ItmGrpMaster.aspx?IGrp_Idno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            { 
                ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
                Int32 intValue = objclsItmGrpMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsItmGrpMaster = null;
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
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intIGrpIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intIGrpIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
                    int value = objclsItmGrpMaster.UpdateStatus(intIGrpIdno, bStatus, empIdno);
                    objclsItmGrpMaster = null;
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
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ItemGroupMaster.xls"));
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
        private void ExportGridView()
        {
            //ItmGrpMasterDAL objclsItmGrpMaster = new ItmGrpMasterDAL();
            //var lstGridData = objclsItmGrpMaster.SelectAll(Convert.ToString(txtGName.Text.Trim()));
            //objclsItmGrpMaster = null;
            //grdMain.AllowPaging = false;
            //string attachment = "attachment; filename=ManageItemGroupMasterReport.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.DataSource = lstGridData;
            //grdMain.DataBind();
            ////grdMain.Columns[2].Visible = false;
            //grdMain.Columns[3].Visible = false;
            //grdMain.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

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
                //else if (gv.Controls[i].GetType() == typeof(CheckBox))
                //{
                //    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                //    gv.Controls.Remove(gv.Controls[i]);
                //    gv.Controls.AddAt(i, l);
                //}
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
           //grdMain.Columns[2].Visible =true;
           //grdMain.Columns[3].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}