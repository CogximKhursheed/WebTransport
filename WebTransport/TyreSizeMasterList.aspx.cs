using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class TyreSizeMasterList : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RecordCount();
            }
        }
        #endregion

        #region Button
        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Function..
        private void RecordCount()
        {
            TyreSizeDAL DAL = new TyreSizeDAL();
            var lstGridData = DAL.Count();
            if (lstGridData != null && lstGridData.Count > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        public void BindGrid()
        {
            TyreSizeDAL DAL = new TyreSizeDAL();
            var Lst = DAL.SelectList(txttyresize.Text.Trim());
            if (Lst != null && Lst.Count > 0)
            {
                grdMain.DataSource = Lst;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + Lst.Count.ToString();

                lblTotalRecord.Text = "T. Record(s): " + Lst.Count;
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        #endregion

        #region GridEvents...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("TyreSizeMaster.aspx?TyresizeID=" + Convert.ToString(e.CommandArgument) + "");
            }
            else if (e.CommandName == "cmddelete")
            {
                TyreSizeDAL DAL = new TyreSizeDAL();
                Int64 Result = DAL.Delete(Convert.ToInt64(e.CommandArgument));
                if (Result > 0)
                {
                    BindGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('Record Deleted Successfully.')", true);
                }
                else if (Result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessageError('Record in Use.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessageError('Record Not Deleted.')", true);
                }
            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string TYresizeIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TyreSize_Idno"));
                if (TYresizeIdno != "")
                {
                    TyreSizeDAL DAL = new TyreSizeDAL();
                    //var ItemExist = ObjDAL.SelectSearch(Guid.Parse(Convert.ToString(MisIdno)));
                    //if (ItemExist != null && ItemExist > 0)
                    //{
                    //    imgBtnDelete.Visible = false;
                    //}
                    //else
                    //{
                    //    imgBtnDelete.Visible = true;
                    //}
                }
            }
        }
        #endregion
    }
}