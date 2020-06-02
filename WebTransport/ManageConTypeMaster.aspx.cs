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
    public partial class ManageConTypeMaster : Pagebase
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
                //this.BindGrid();
            }
            txtConType.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
            lblTotalRecord.Text = "T. Record (s): " + objclsConTypeMaster.SelectTotal();
            txtConType.Focus();
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
            var lstGridData = objclsConTypeMaster.SelectAll(Convert.ToString(txtConType.Text.Trim()));
            objclsConTypeMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("ContainerType", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["ContainerType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Con_Type"));
                    dr["Status"] = Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status")) == true ? "Active" : "InActive";
                    dt.Rows.Add(dr);

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
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
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string ConTypeIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ConType_Idno"));
                if (ConTypeIdno != "")
                {
                    ContainerTypeMasterDAL obj = new ContainerTypeMasterDAL();
                    //To hide delete button

                    var ItemExistInMast = obj.CheckItemGrpExistInItemMaster(Convert.ToInt32(ConTypeIdno));
                    if (ItemExistInMast != null && ItemExistInMast.Count > 0)
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
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("ContainerTypeMaster.aspx?ConType_Idno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
                Int32 intValue = objclsConTypeMaster.Delete(Convert.ToInt32(e.CommandArgument));
                objclsConTypeMaster = null;
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
                int intTypeIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intTypeIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    ContainerTypeMasterDAL objclsConTypeMaster = new ContainerTypeMasterDAL();
                    int value = objclsConTypeMaster.UpdateStatus(intTypeIdno, bStatus, empIdno);
                    objclsConTypeMaster = null;
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ContainerType.xls"));
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

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
    }
}