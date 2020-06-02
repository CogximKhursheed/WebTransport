using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;


namespace WebTransport
{
    public partial class ManageAcntHeadMaintenace : Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
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
                this.BindMainHead();
                drpMHGrp.Focus();

                AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
                var lstGridData = objclsAcntHeadMaintenace.SelectForSearch(Convert.ToString(txtAcntGrp.Text.Trim()), Convert.ToInt32(drpMHGrp.SelectedValue));
                if (lstGridData != null && lstGridData.Count > 0)
                {
                    lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                }

            }
            txtAcntGrp.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
               
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
            var lstGridData = objclsAcntHeadMaintenace.SelectForSearch(Convert.ToString(txtAcntGrp.Text.Trim()), Convert.ToInt32(drpMHGrp.SelectedValue));
            objclsAcntHeadMaintenace = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
               
                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("Acount HeadName", typeof(string));
                dt.Columns.Add("Acount GroupName", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["Acount HeadName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AHeadName"));
                    dr["Acount GroupName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AcntGHName"));
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
                grdMain.DataSource =null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private AcntHeadMaintenaceDAL newAcntHeadMaintenaceDAL()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To Bind Main Head DropDown
        /// </summary>
        private void BindMainHead()
        {
            AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
            var objAHeadMast = objclsAcntHeadMaintenace.SelectAHGroup();
            objclsAcntHeadMaintenace = null;
            drpMHGrp.DataSource = objAHeadMast;
            drpMHGrp.DataTextField = "AcntGH_Name";
            drpMHGrp.DataValueField = "MainHead_Idno";
            drpMHGrp.DataBind();
            drpMHGrp.Items.Insert(0, new ListItem("< Choose Main Group >", "-1"));
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
                Response.Redirect("AcntHeadMaintenace.aspx?AHeadIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
                Int32 intValue = objclsAcntHeadMaintenace.Delete(Convert.ToInt32(e.CommandArgument));
                objclsAcntHeadMaintenace = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    drpMHGrp.Focus();
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
                int intAHeadIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intAHeadIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    AcntHeadMaintenaceDAL objclsAcntHeadMaintenace = new AcntHeadMaintenaceDAL();
                    int value = objclsAcntHeadMaintenace.UpdateStatus(intAHeadIdno, bStatus, empIdno);
                    objclsAcntHeadMaintenace = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        drpMHGrp.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            drpMHGrp.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
               //// ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                Label lblAHeadIdno = (Label)e.Row.FindControl("lblAHeadIdno");
               bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));

                    imgBtnStatus.Visible = true;
 
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";

                if (Convert.ToInt64(lblAHeadIdno.Text.Trim()) > 25)
                {
                }
                else
                {
                }

                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string HeadIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AHeadIdno"));
                if (HeadIdno != "")
                {
                    AcntHeadMaintenaceDAL obj = new AcntHeadMaintenaceDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(HeadIdno));
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
        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            this.BindGrid();
            //Panel1.Visible = true;
        }
        #endregion

        #region print...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AcountGroupHead.xls"));
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
            //grdMain.AllowPaging = false;
            //string attachment = "attachment; filename=AcountHeadMaintenanceReport.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.Columns[3].Visible=false;
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
            //grdMain.Columns[4].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        
    }
}