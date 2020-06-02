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
    public partial class ManageClaimFrm : Pagebase
    {
        #region Private Variable....
        private int intFormId = 0;

        BindDropdownDAL obj;
        FinYearDAL objFinYearDAL;
        ClaimFrmDAL objClaimFrmDAL;
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
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.Print == false)
                //{
                //    imgBtnExcel.Visible = false;
                //}
                this.BindDateRange();
                this.BindPartyName();
                this.BindCompany();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange.SelectedIndex = 0; ddlDateRange_SelectedIndexChanged(null, null);
                //this.Countall();
            }
        }
        #endregion

        #region Functions...

        private void BindGrid()
        {
            objClaimFrmDAL = new ClaimFrmDAL();
            string ClaimNo= string.IsNullOrEmpty(Convert.ToString(txtClaimNo.Text))? "" : Convert.ToString(txtClaimNo.Text);
            Int64 YearIdno = string.IsNullOrEmpty(Convert.ToString(ddlDateRange.SelectedValue)) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 PartyIdno = string.IsNullOrEmpty(Convert.ToString(ddlPartyName.SelectedValue)) ? 0 : Convert.ToInt64(ddlPartyName.SelectedValue);
            Int64 CompanyIdno = string.IsNullOrEmpty(Convert.ToString(ddlCompName.SelectedValue)) ? 0 : Convert.ToInt64(ddlCompName.SelectedValue);
            Int64 LocationFrom = string.IsNullOrEmpty(Convert.ToString(ddlLocation.SelectedValue)) ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            if (string.IsNullOrEmpty(Convert.ToString(txtDateFrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTo.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }

            var lstGridData = objClaimFrmDAL.SearchList(dtfrom, dtto, ClaimNo, PartyIdno, LocationFrom, CompanyIdno, YearIdno);

            objClaimFrmDAL = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

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

        private void BindPartyName()
        {
            obj = new BindDropdownDAL();
            DataTable dtParty = new DataTable();
            dtParty = obj.BindPartyForSale(ApplicationFunction.ConnectionString());
            obj = null;
            ddlPartyName.DataSource = dtParty;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));

        }

        private void BindCompany()
        {
            obj = new BindDropdownDAL();
            var CompanyName = obj.SelectCompanyName();
            obj = null;
            if (CompanyName != null)
            {
                ddlCompName.DataSource = CompanyName;
                ddlCompName.DataTextField = "Acnt_Name";
                ddlCompName.DataValueField = "Acnt_Idno";
                ddlCompName.DataBind();
            }
            ddlCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Party.......", "0"));
        }


        private void BindCity()
        {
            obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = ToCity;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            obj = null;
            if (FrmCity.Count > 0)
            {
                ddlLocation.DataSource = FrmCity;
                ddlLocation.DataTextField = "City_Name";
                ddlLocation.DataValueField = "City_Idno";
                ddlLocation.DataBind();
            }
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Choose Location...", "0"));
        }
        private void BindDateRange()
        {
            objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }


        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            objFinYearDAL = null;
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
            }

        }
        //public void Countall()
        //{
        //    ItemMastPurDAL objItemMast = new ItemMastPurDAL();
        //    Int64 count = objItemMast.Countall();
        //    if (count > 0)
        //    {
        //        lblTotalRecord.Text = "T. Record (s):" + count;
        //    }
        //    else
        //    {
        //        lblTotalRecord.Text = "T. Record (s): 0 ";
        //    }
        //}
        
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
                Response.Redirect("ClaimFrm.aspx?ClaimHeadIdno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                objClaimFrmDAL = new ClaimFrmDAL();
                long intValue = objClaimFrmDAL.Delete(Convert.ToInt32(e.CommandArgument));
                objClaimFrmDAL = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
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

        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ItemReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            // grdMain.Columns[3].Visible = false;
            grdMain.Columns[9].Visible = false;
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
            grdMain.Columns[8].Visible = true;
            grdMain.Columns[9].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }


        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        } 
        #endregion

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                Int64 intClaimHeadIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ClaimHead_Idno"));

                if (intClaimHeadIdno > 0)
                {
                    ClaimFrmDAL obj = new ClaimFrmDAL();
                    var ClaimExist = obj.Exists(Convert.ToInt64(intClaimHeadIdno));
                    obj = null;
                    if (ClaimExist != null && ClaimExist.Count>0)
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

        
        
    }
}