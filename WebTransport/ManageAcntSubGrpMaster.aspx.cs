using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Data;

namespace WebTransport
{
    public partial class ManageAcntSubGrpMaster : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 15;
        #endregion

        #region Page Load...
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
                //TotalRecords();
                this.BindHead();
                drpAGrp.Focus();
                AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
                var lstGridData = objclsAcntSubGrpMaster.SelectForSearch(Convert.ToString(txtAcntSubGrp.Text.Trim()), Convert.ToInt32(drpAGrp.SelectedValue));
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                }
            }
            txtAcntSubGrp.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");

        }
        #endregion

        #region Functions...
        //private void TotalRecords()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        lblTotalRecord.Text = "T. Record (s): " + Convert.ToString((from CT in db.AcntSubHeads
        //                                                                    select CT).Count());

        //    }
        //}
        private void BindGrid()
        {
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            var lstGridData = objclsAcntSubGrpMaster.SelectForSearch(Convert.ToString(txtAcntSubGrp.Text.Trim()), Convert.ToInt32(drpAGrp.SelectedValue));
            objclsAcntSubGrpMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("Acount Group Name", typeof(string));
                dt.Columns.Add("Acount Sub Group Name", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["Acount Group Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ASubHeadName"));
                    dr["Acount Sub Group Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AHeadName"));
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
                lblTotalRecord.Text = "T. Record (s): 0";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        /// <summary>
        /// To Bind Head DropDown
        /// </summary>
        private void BindHead()
        {
            AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            var objAHeadMast = objclsAcntSubGrpMaster.SelectAHGroupActiveOnly();
            objclsAcntSubGrpMaster = null;
            drpAGrp.DataSource = objAHeadMast;
            drpAGrp.DataTextField = "AHead_Name";
            drpAGrp.DataValueField = "AHead_Idno";
            drpAGrp.DataBind();
            drpAGrp.Items.Insert(0, new ListItem("< Choose Group >", "0"));
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
                Response.Redirect("AcntSubGrpMaster.aspx?ASubHeadIdno=" + e.CommandArgument, true);
            }
            //if (e.CommandName == "cmddelete")
            //{
            //    AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
            //    Int32 intValue = objclsAcntSubGrpMaster.Delete(Convert.ToInt32(e.CommandArgument));
            //    objclsAcntSubGrpMaster = null;
            //    if (intValue > 0)
            //    {
            //        this.BindGrid();
            //        strMsg = "Record deleted successfully.";
            //        drpAGrp.Focus();
            //    }
            //    else
            //    {
            //        if (intValue == -1)
            //            strMsg = "Record can not be deleted. It is in use.";
            //        else
            //            strMsg = "Record not deleted.";
            //    }
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            //}
            else if (e.CommandName == "cmdstatus")
            {
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intSubHeadIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intSubHeadIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    AcntSubGrpMasterDAL objclsAcntSubGrpMaster = new AcntSubGrpMasterDAL();
                    int value = objclsAcntSubGrpMaster.UpdateStatus(intSubHeadIdno, bStatus, empIdno);
                    objclsAcntSubGrpMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        drpAGrp.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);

                }
            }
            drpAGrp.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                ////  ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                //  ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                //  Label lblSubHeadIdno = (Label)e.Row.FindControl("lblSubHeadIdno");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                //  base.CheckUserRights(intFormId);
                //  if (base.Edit == false)
                //  {
                imgBtnStatus.Visible = true;
                //    imgBtnEdit.Visible = false;
                //    grdMain.Columns[3].Visible = false;
                //}
                ////if (base.Delete == false)
                ////{
                ////    imgBtnDelete.Visible = false;
                ////}
                //if ((base.Edit == false) && (base.Delete == false))
                //{
                //    grdMain.Columns[4].Visible = false;
                //}
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
                //if (Convert.ToInt32(drpAGrp.SelectedValue) > 0)
                //{
                //    grdMain.Columns[1].Visible = false;
                //}
                //else
                //{
                //    grdMain.Columns[1].Visible = true;
                //}
                //if (Convert.ToInt64(lblSubHeadIdno.Text.Trim()) > 25)
                //{
                //   // imgBtnDelete.Visible = true;
                //    imgBtnEdit.Visible = true;
                //}
                //else
                //{
                //   // imgBtnDelete.Visible = false;
                //    imgBtnEdit.Visible = false;
                //}
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AcountSubGroupHead.xls"));
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
            //string attachment = "attachment; filename=AcountSubGroupReport.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.Columns[3].Visible = false;
            //grdMain.Columns[4].Visible = false;
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
            //grdMain.Columns[3].Visible = true;
           // grdMain.Columns[4].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}