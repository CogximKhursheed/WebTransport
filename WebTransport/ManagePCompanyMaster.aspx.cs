using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using System.Data;

namespace WebTransport
{
    public partial class ManagePCompanyMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPetrolCompanyName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);"); 
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
                this.countall();
                txtPetrolCompanyName.Focus();

                //this.BindState();
                //drpState.Focus();
            }
        }
        #endregion

        #region Functions...
        public void countall()
        {
            PetrolCompanyMasterDAL objclsPetrolCompanyMaster = new PetrolCompanyMasterDAL();
             Int64 total = 0;

             total = objclsPetrolCompanyMaster.selectcount();
            if (total > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + total;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindGrid()
        {
            PetrolCompanyMasterDAL objclsPetrolCompanyMaster = new PetrolCompanyMasterDAL();
            var lstGridData = objclsPetrolCompanyMaster.SelectForSearch(Convert.ToString(txtPetrolCompanyName.Text.Trim()));
            objclsPetrolCompanyMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("Company Name", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["Company Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PCompName"));
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
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;

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
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("PetrolCompanyMaster.aspx?PCompIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                PetrolCompanyMasterDAL objclsPetrolCompanyMaster = new PetrolCompanyMasterDAL();
                Int32 intValue = objclsPetrolCompanyMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsPetrolCompanyMaster = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
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
                int intCityIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intCityIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    PetrolCompanyMasterDAL objclsPetrolCompanyMaster = new PetrolCompanyMasterDAL();
                    int value = objclsPetrolCompanyMaster.UpdateStatus(intCityIdno, bStatus, empIdno);
                    objclsPetrolCompanyMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                    }
                    else
                    {
                        strMsg = "Status not updated.";
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
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                //ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ////ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                //base.CheckUserRights(intFormId);
                //if (base.Edit == false)
                //{
                   imgBtnStatus.Visible = true;
                //    imgBtnEdit.Visible = false;
                //    grdMain.Columns[4].Visible = false;
                //}
                ////if (base.Delete == false)
                ////{
                ////    imgBtnDelete.Visible = false;
                ////}
                //if ((base.Edit == false) && (base.Delete == false))
                //{
                //    grdMain.Columns[5].Visible = false;
                //}
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
                //if (Convert.ToInt32(drpState.SelectedValue) > 0)
                //{
                //    grdMain.Columns[1].Visible = false;
                //}
                //else
                //{
                //    grdMain.Columns[1].Visible = true;
                //}
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string ItemIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PCompIdno"));
                if (ItemIdno != "")
                {
                    PetrolCompanyMasterDAL obj = new PetrolCompanyMasterDAL();
                   
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Petrolcompanymaster.xls"));
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
            //string attachment = "attachment; filename=ManageCityMasterReport.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            ////grdMain.Columns[5].Visible = false;
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
            
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

      
    }
}