using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
//using AutomobileOnline.Classes;
using System.IO;
using System.Data;

namespace WebTransport
{
    public partial class ManageUOMMaster : Pagebase
    {
        #region Private variable...
        private int intFormId = 10;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            txtUOMName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                //UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
                //var lstGridData = objclsUOMMaster.SelectForSearch(Convert.ToString(txtUOMName.Text.Trim()));
                //if (lstGridData != null && lstGridData.Count > 0)
                //{
                //    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //}
                txtUOMName.Focus();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
            var lstGridData = objclsUOMMaster.SelectForSearch(Convert.ToString(txtUOMName.Text.Trim()));
            objclsUOMMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("UOM Name", typeof(string));
                dt.Columns.Add("UOM Description", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c=0; 
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["UOM Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "UOMName"));
                    dr["UOM Description"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "UOMDesc"));
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
                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;
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
                lblTotalRecord.Text = "T. Record(s): 0";
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
                Response.Redirect("UOMMaster.aspx?UOMIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
                Int32 intValue = objclsUOMMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsUOMMaster = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtUOMName.Focus();
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
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intUOMIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intUOMIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    UOMMasterDAL objclsUOMMaster = new UOMMasterDAL();
                    int value = objclsUOMMaster.UpdateStatus(intUOMIdno, bStatus, empIdno);
                    objclsUOMMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtUOMName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txtUOMName.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));

                // Used to hide Delete button if ItemgrpId exists in Item Master,Fleet Mgmt  Item Master,Goods Received,Quotation, GR Preparation,Fleet Mgnt. Purchase Bill
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string UOMIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UOMIdno"));
                if (UOMIdno != "")
                {
                    UOMMasterDAL obj = new UOMMasterDAL();
                    var ItemGrpExist = obj.CheckUOMExistInOtherMaster(Convert.ToInt32(UOMIdno));
                    if (ItemGrpExist != null && ItemGrpExist.Count > 0)
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

        #region Button Events...
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "UomMaster.xls"));
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
           // string attachment = "attachment; filename=UomReport.xls";
           // Response.ClearContent();
           // Response.AddHeader("content-disposition", attachment);
           // Response.Charset = "";
           // Response.ContentType = "application/vnd.ms-excel";
           // StringWriter sw = new StringWriter();
           // HtmlTextWriter htw = new HtmlTextWriter(sw);
           //// grdMain.Columns[3].Visible = false;
           // grdMain.Columns[4].Visible = false;
           // grdMain.RenderControl(htw);
           // Response.Write(sw.ToString());
           // Response.End();
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
            grdMain.Columns[3].Visible = true;
            grdMain.Columns[4].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}