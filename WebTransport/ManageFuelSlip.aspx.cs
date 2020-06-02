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
using System.Threading;
using System.Data;

namespace WebTransport
{
    public partial class ManageFuelSlip : Pagebase
    {
        #region Private Variable....
        private int intFormId = 8;
        DataTable dt = new DataTable();
        DataTable CSVTable = new DataTable();
        bool st;
        #endregion

        #region Page Events...
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
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BindCity();
                this.BindTruck();
                this.BindPump();
                this.BindDriver();
                this.Countall();
                ddlDateRange.Focus();
                prints.Visible = false;
            }
        }
        #endregion

        #region Functions...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlLocation.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }

        private void BindCity()
        {
            OpenTyreDAL objTollMastDAL = new OpenTyreDAL();
            var lst = objTollMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("----Select ----", "0"));
        }
        private void BindTruck()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNoPurchase();
            ddlLorry.DataSource = TruckNolst;
            ddlLorry.DataTextField = "Lorry_No";
            ddlLorry.DataValueField = "lorry_idno";
            ddlLorry.DataBind();
            ddlLorry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindPump()
        {
            FuelSlipDAL objclsFuelSlip = new FuelSlipDAL();
            var objFuelSlip = objclsFuelSlip.SelectPCompName();
            objclsFuelSlip = null;
            ddlPPump.DataSource = objFuelSlip;
            ddlPPump.DataTextField = "Acnt_Name";
            ddlPPump.DataValueField = "Acnt_Idno";
            ddlPPump.DataBind();
            ddlPPump.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }

        private void BindDriver()
        {
            FuelSlipDAL objclsFuelSlip = new FuelSlipDAL();
            var objFuelSlip = objclsFuelSlip.SelectDriver();
            objclsFuelSlip = null;
            ddlDriver.DataSource = objFuelSlip;
            ddlDriver.DataTextField = "Driver_Name";
            ddlDriver.DataValueField = "Driver_Idno";
            ddlDriver.DataBind();
            ddlDriver.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }

        private void BindGrid()
        {
            FuelSlipDAL objFuelSlipDAL = new FuelSlipDAL();
            var lstGridData = objFuelSlipDAL.Select(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue), string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue), string.IsNullOrEmpty(ddlLorry.SelectedValue) ? 0 : Convert.ToInt64(ddlLorry.SelectedValue), string.IsNullOrEmpty(ddlDriver.SelectedValue) ? 0 : Convert.ToInt64(ddlDriver.SelectedValue), string.IsNullOrEmpty(ddlPPump.SelectedValue) ? 0 : Convert.ToInt64(ddlPPump.SelectedValue), Convert.ToInt64((txtSlipNo.Text) == "" ? "0" : txtSlipNo.Text));
            objFuelSlipDAL = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
              
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
               // imgBtnExcel.Visible = true;
                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
                prints.Visible = false;
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
        public void Countall()
        {
            FuelSlipDAL objFuelSlip = new FuelSlipDAL();
            Int64 count = objFuelSlip.Countall(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
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
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("FuelSlip.aspx?FuelSlipIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                FuelSlipDAL objItemMast = new FuelSlipDAL();
                long intValue = objItemMast.Delete(Convert.ToInt32(e.CommandArgument));
                objItemMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    //ddlGroupType.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use!";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                
            }
        }
        #endregion

        //#region print...
        //private void ExportGridView()
        //{
        //    string attachment = "attachment; filename=ItemReport.xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    // grdMain.Columns[3].Visible = false;
        //    grdMain.Columns[9].Visible = false;
        //    grdMain.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //private void PrepareGridViewForExport(Control gv)
        //{
        //    LinkButton lb = new LinkButton();
        //    Literal l = new Literal();
        //    string name = String.Empty;
        //    for (int i = 0; i < gv.Controls.Count; i++)
        //    {
        //        if (gv.Controls[i].GetType() == typeof(LinkButton))
        //        {
        //            l.Text = (gv.Controls[i] as LinkButton).Text;
        //            gv.Controls.Remove(gv.Controls[i]);
        //            gv.Controls.AddAt(i, l);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(DropDownList))
        //        {
        //            l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
        //            gv.Controls.Remove(gv.Controls[i]);
        //            gv.Controls.AddAt(i, l);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(CheckBox))
        //        {
        //            l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
        //            gv.Controls.Remove(gv.Controls[i]);
        //            gv.Controls.AddAt(i, l);
        //        }
        //        else if (gv.Controls[i].GetType() == typeof(ImageButton))
        //        {
        //            l.Text = (gv.Controls[i] as ImageButton).ImageUrl == "~/Images/inactive.png" ? "InActive" : "Active";
        //            gv.Controls.Remove(gv.Controls[i]);
        //            gv.Controls.AddAt(i, l);
        //        }
        //        if (gv.Controls[i].HasControls())
        //        {
        //            PrepareGridViewForExport(gv.Controls[i]);
        //        }
        //    }
        //}

        //protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        //{
        //    grdMain.GridLines = GridLines.Both;
        //    PrepareGridViewForExport(grdMain);
        //    ExportGridView();
        //    grdMain.Columns[8].Visible = true;
        //    grdMain.Columns[9].Visible = true;
        //}
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}
        //#endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        } 
        #endregion
    }
}