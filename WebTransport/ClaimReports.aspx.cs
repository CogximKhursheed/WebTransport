using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;

namespace WebTransport
{
    public partial class ClaimReports : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    Response.Redirect("LogOut.aspx");
            //}
            if (!Page.IsPostBack)
            {
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                this.BindPartyName();
                this.BindCompany();
                if (Convert.ToString(Session["Userclass"]) == "Admin") { this.BindCity(); } else { this.BindCity(Convert.ToInt64(Session["UserIdno"])); }
                ddlDateRange_SelectedIndexChanged(null, null);
            }
        }
        #endregion

        #region Functions...

        private void BindGrid()
        {
            ClaimFrmDAL objClaim = new ClaimFrmDAL();

            string ClaimNo = string.IsNullOrEmpty(Convert.ToString(txtClaimNo.Text)) ? "" : Convert.ToString(txtClaimNo.Text);
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

            var lstGridData = objClaim.SelectSearchListReport(dtfrom, dtto, ClaimNo, PartyIdno, LocationFrom, CompanyIdno, YearIdno);

            objClaim = null;
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
            BindDropdownDAL obj = new BindDropdownDAL();
            DataTable dtParty = new DataTable();
            dtParty = obj.BindPartyForSale(ApplicationFunction.ConnectionString());
            obj = null;
            ddlPartyName.DataSource = dtParty;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Party-", "0"));
        }

        private void BindCompany()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var CompanyName = obj.SelectCompanyName();
            obj = null;
            if (CompanyName != null)
            {
                ddlCompName.DataSource = CompanyName;
                ddlCompName.DataTextField = "Acnt_Name";
                ddlCompName.DataValueField = "Acnt_Idno";
                ddlCompName.DataBind();
            }
            ddlCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Company-", "0"));
        }


        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = ToCity;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Location-", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            obj = null;
            if (FrmCity.Count > 0)
            {
                ddlLocation.DataSource = FrmCity;
                ddlLocation.DataTextField = "City_Name";
                ddlLocation.DataValueField = "City_Idno";
                ddlLocation.DataBind();
            }
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Select Location-", "0"));
        }

        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange();
            objFinYearDAL = null;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
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

        #endregion

        #region Button Events..
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ClaimReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);  
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
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region Controls Events..
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }  
      
        #endregion
    }
}