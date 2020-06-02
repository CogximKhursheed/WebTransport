using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;


namespace WebTransport
{
    public partial class ManageTyreMaster : Pagebase
    {
        #region Page Events...
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

                this.Countall();
                txtTyreName.Focus();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region Functions...
        public void Countall()
        {
            TyreMastDAL objTyreMast = new TyreMastDAL();
            Int64 count = objTyreMast.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindGrid()
        {
            string strItemName = txtTyreName.Text.Trim();
            TyreMastDAL objTyreMast = new TyreMastDAL();
            var lstGridData = objTyreMast.SelectForSearch(strItemName);
            objTyreMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
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
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;

                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
            txtTyreName.Focus();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=TyreMasterReport.xls";
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
                    l.Text = (gv.Controls[i] as ImageButton).ImageUrl == "~/Images/inactive.png" ? "InActiv" : "Active";
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
                Response.Redirect("TyreMaster.aspx?TyreIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmdstatus")
            {
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
                    TyreMastDAL objItemMast = new TyreMastDAL();
                    int value = objItemMast.UpdateStatus(intitemIdno, bStatus);
                    objItemMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        ShowMessage("Status updated successfully.");
                        txtTyreName.Focus();
                    }
                    else
                    {
                        ShowMessageErr("Status not updated.");
                    }
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                TyreMastDAL objItemMast = new TyreMastDAL();
                long intValue = objItemMast.Delete(Convert.ToInt32(e.CommandArgument));
                objItemMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();

                    ShowMessage("Record deleted successfully.");

                }
                else
                {
                    if (intValue == -1)
                        ShowMessageErr("Record can not be deleted. It is in use!");
                    else
                        ShowMessageErr("Record not deleted!");
                }
            }

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                int Tyreiddno = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TyreType_Idno"));
                TyreMastDAL obj = new TyreMastDAL();
                var TyreExist = obj.CheckTyreExistInOtherMaster(Convert.ToInt32(Tyreiddno));
                if (TyreExist != null && TyreExist.Count > 0)
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
        #endregion
    }
}