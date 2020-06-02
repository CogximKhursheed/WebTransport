using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.Account;
using System.IO;
using WebTransport.DAL;


namespace WebTransport
{
    public partial class ManageDesignationMaster : Pagebase
    {
        private int intFormId = 42;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.Print == false)
                //{
                //    imgBtnExcel.Visible = false;
                //} 
                txtDesigNm.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtDesigNm.Focus();
                //this.countall();
            }
        }

        #region Button Event ...

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
            txtDesigNm.Focus(); 
        }
        #endregion

        #region Bind Event ...
        private void BindGrid()
        {
            DesignationMasterDAL objclsDesignationMasterDAL = new DesignationMasterDAL();
            string strEmpName = string.IsNullOrEmpty(txtDesigNm.Text.Trim()) ? "" : (txtDesigNm.Text.Trim());
            var objEmpMaster = objclsDesignationMasterDAL.Select(strEmpName);
            objclsDesignationMasterDAL = null;
            if (objEmpMaster != null && objEmpMaster.Count > 0)
            {
                grdMain.DataSource = objEmpMaster;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + objEmpMaster.Count;
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + objEmpMaster.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
                divpaging.Visible = false;
                lblcontant.Visible = false;
            } 
        }
        #endregion

        #region Grid Event ...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("DesignationMaster.aspx?DesigIdno=" + e.CommandArgument, true);
            }
            //if (e.CommandName == "cmddelete")
            //{
            //    Int32 intCompIdno = 1;
            //    clsDesignationMasterDAL objclsDesignationMasterDAL = new clsDesignationMasterDAL();
            //    int value = objclsDesignationMasterDAL.Delete(Convert.ToInt32(e.CommandArgument));
            //    objclsDesignationMasterDAL = null;
            //    if (value > 0)
            //    {
            //        this.BindGrid();
            //    }
            //}
            else if (e.CommandName == "cmdstatus")
            {
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intDesgID = 0;
                bool bStatus = false; string strMsg = string.Empty;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intDesgID = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    DesignationMasterDAL objclsDesignationMasterDAL = new DesignationMasterDAL();
                    int value = objclsDesignationMasterDAL.UpdateStatus(intDesgID, bStatus, empIdno);
                    objclsDesignationMasterDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Designation updated successfully.";
                        txtDesigNm.Focus();
                    }
                    else
                    {
                        strMsg = "Designation not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit"); 
                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                { 
                    imgBtnEdit.Visible = false;

                } 
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }

        #endregion

        #region print...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }
        public void countall()
        {
             DesignationMasterDAL objclsDesignationMasterDAL = new DesignationMasterDAL();
            Int64 total=0;
            
            total=objclsDesignationMasterDAL.selectcount();
            if (total > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + total;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ManageDesignation.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[3].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
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
            grdMain.Columns[3].Visible = false;
        }

        #endregion

    }
}