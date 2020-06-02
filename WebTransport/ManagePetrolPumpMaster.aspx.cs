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
    public partial class ManagePetrolPumpMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPPumpName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                this.BindState();
                drpState.Focus();
                this.countall();
            }
        }
        #endregion
      
        #region Control Events...
        protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCityDDL(Convert.ToInt64(drpState.SelectedValue));
            drpCity.Focus();
        }
        #endregion

        #region Functions...

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PetrolPumpMaster.xls"));
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

        public void countall()
        {

            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            Int64 total = 0;
            total = objclsPetrolPumpMaster.selectcount();
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
            //var DefaultCity = 0;
            Int32 intcityid=0;
            if (drpCity.SelectedValue != "")
            {
                Convert.ToInt32(drpCity.SelectedValue);
            }
           intcityid = (drpCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(drpCity.SelectedValue));
        
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            var lstGridData = objclsPetrolPumpMaster.SelectForSearch(Convert.ToString(txtPPumpName.Text.Trim()), Convert.ToInt32(drpState.SelectedValue), intcityid);
            objclsPetrolPumpMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("PumpName", typeof(string));
                dt.Columns.Add("Company", typeof(string));
                dt.Columns.Add("PersonName", typeof(string));
                dt.Columns.Add("Designation", typeof(string));
                dt.Columns.Add("MobileNo", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Status", typeof(string));
               

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["PumpName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpName"));
                    dr["Company"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PCompName"));
                    dr["PersonName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpPersonName"));
                    dr["Designation"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpPersonDesignation"));
                    dr["MobileNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpMobileno"));
                    dr["State"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpState"));
                    dr["City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "PPumpCity"));
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
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void BindCityDDL(Int64 intStateIdno)
        {
            BindDropdownDAL objClsBindDropDown = new BindDropdownDAL();
            var lst = objClsBindDropDown.BindCity(intStateIdno);
            drpCity.DataSource = lst;
            drpCity.DataTextField = "City_Name";
            drpCity.DataValueField = "City_Idno";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem(" ----Select----", "0"));
        }
        private void BindState()
        {
            PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
            var objPetrolPumpMaster = objclsPetrolPumpMaster.SelectState();
            objclsPetrolPumpMaster = null;
            drpState.DataSource = objPetrolPumpMaster;
            drpState.DataTextField = "State_Name";
            drpState.DataValueField = "State_Idno";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
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
                Response.Redirect("PetrolPumpMaster.aspx?PPumpIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                PetrolPumpMasterDAL objclsPetrolPumpMast = new PetrolPumpMasterDAL();
                Int32 intValue = objclsPetrolPumpMast.Delete(Convert.ToInt32(e.CommandArgument));
                objclsPetrolPumpMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    drpState.Focus();
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
                    PetrolPumpMasterDAL objclsPetrolPumpMaster = new PetrolPumpMasterDAL();
                    int value = objclsPetrolPumpMaster.UpdateStatus(intCityIdno, bStatus, empIdno);
                    objclsPetrolPumpMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        drpState.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            drpState.Focus();
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
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ManagePetrolPumpMasterReport.xls";
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