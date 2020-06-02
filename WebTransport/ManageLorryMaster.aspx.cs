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
using System.Data.SqlClient;

namespace WebTransport
{
    public partial class ManageLorryMaster : Pagebase
    {

        #region Private Variable....
        private int intFormId = 32;
        #endregion

        #region Page Load Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtOwnrNme.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                this.BindPartyName();
                ddlPartyName.Focus();
                LorryMasterDAL objLorrMast = new LorryMasterDAL();
                lblTotalRecord.Text = "T Record (s):" + objLorrMast.Count();
            }
        }
        #endregion

        #region Functions...
        private void BindGrid()
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            var lstGridData = objLorrMast.SelectForSearch(Convert.ToString(txtOwnrNme.Text.Trim()), Convert.ToInt32(ddlPartyName.SelectedValue), Convert.ToString(txtlryno.Text.Trim()),
                                                          Convert.ToString(txtpanno.Text.Trim()));
            objLorrMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("PrtyName", typeof(string));
                dt.Columns.Add("Owner", typeof(string));
                dt.Columns.Add("LorryType", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("DriverName", typeof(string));
                dt.Columns.Add("LorryMake", typeof(string));
                dt.Columns.Add("Pan", typeof(string));
                dt.Columns.Add("EngineNo", typeof(string));
                dt.Columns.Add("ChasisNo", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("LowRate", typeof(string));
                dt.Columns.Add("MobileNo", typeof(string));

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["PrtyName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "prty_Name"));
                    dr["Owner"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "OwnerName"));
                    dr["LorryType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LorryType"));
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LorryNo"));
                    dr["DriverName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DriverName"));
                    dr["LorryMake"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LorryMake"));
                    dr["Pan"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PanNo"));
                    dr["EngineNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "EngineNo"));
                    dr["ChasisNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ChasisNo"));
                    dr["LowRate"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LowRate"));
                    dr["MobileNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "MobileNo"));
                    dr["Status"] = (Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status"))==false)?"InActive":"Active";
                    dt.Rows.Add(dr);
                 
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T Record (s): 0";
                imgBtnExcel.Visible = false;
            }
        }

        /// <summary>
        /// To Bind Party Name DropDown
        /// </summary>
        private void BindPartyName()
        {
            LorryMasterDAL objLorrMast = new LorryMasterDAL();
            var LorrMast = objLorrMast.SelectPartyName();
            objLorrMast = null;
            ddlPartyName.DataSource = LorrMast;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new ListItem("< Choose Party Name >", "0"));
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
                Response.Redirect("LorryMaster.aspx?LorryIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                LorryMasterDAL objLorrMast = new LorryMasterDAL();
                Int32 intValue = objLorrMast.Delete(Convert.ToInt32(e.CommandArgument));
                objLorrMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlPartyName.Focus();
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
                int intLorryIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intLorryIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    LorryMasterDAL objLorryMast = new LorryMasterDAL();
                    int value = objLorryMast.UpdateStatus(intLorryIdno, bStatus, empIdno);
                    objLorryMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        ddlPartyName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            ddlPartyName.Focus();
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
                {
                    imgBtnStatus.Visible = true;
                    //imgBtnEdit.Visible = false;
                    //grdMain.Columns[4].Visible = false;
                }
                //if (base.Delete == false)
                //{
                //    imgBtnDelete.Visible = false;
                //}
                //if ((base.Edit == false) && (base.Delete == false))
                //{
                //    grdMain.Columns[5].Visible = false;
                //}
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";

                // Used to hide Delete button if ItemgrpId exists in GR Preparation,F.Mgmt--->  Purchase Bill ,Material Issue, Trip Start
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkBtnDelete");
                string LorryIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LorryIdno"));
                if (LorryIdno != "")
                {
                    LorryMasterDAL obj = new LorryMasterDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(LorryIdno));
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        imgBtnDelete.Visible = false;
                    }
                    else
                    {
                        imgBtnDelete.Visible = true;
                    }
                }
                // end----

            }
        }
        #endregion

        #region Button Events...
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ManageLOcationMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[4].Visible = false;
            grdMain.Columns[5].Visible = false;
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[4].Visible = true;
            grdMain.Columns[5].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        protected void LnkExcel_Click(object sender, EventArgs e)
        {

        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "LorryMaster.xls"));
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


        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void imgBtnExcel_Click1(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }
    }
}