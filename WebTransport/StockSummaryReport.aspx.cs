using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Data;

namespace WebTransport
{
    public partial class StockSummaryReport : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; DataTable dt = new DataTable(); int x = 0;
        double TOS = 0, TCL = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }
                this.BindDateRange();
                this.BindItemName();
                ddlDateRange_SelectedIndexChanged(null, null);
                TotalRecords();
            }
        }
        #endregion

        #region Functions...

        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string UserClass = Convert.ToString(Session["Userclass"]);
                Int64 UserIdno = 0;
                Int64 YearIdno = Convert.ToInt64(ddlDateRange.SelectedValue);
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                CurrentStockRpt obj = new CurrentStockRpt();
                DataTable list1 = obj.SelectCurrentStockSummary(ApplicationFunction.ConnectionString(), YearIdno, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTO.Text)), 0, 0);
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }

        private void BindGrid()
        {
            CurrentStockRpt obj = new CurrentStockRpt();
            Int64 YearIdno = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 ItemIdno = Convert.ToInt64(ddlItemName.SelectedValue);
            Int64 LocIdno = Convert.ToInt64(ddlFromCity.SelectedValue);
            DateTime? dtfrom = null;
            DateTime? dtto = null;


            if (string.IsNullOrEmpty(Convert.ToString(txtDate.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtDateTO.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTO.Text));
            }
            dt = obj.SelectCurrentStockSummary(ApplicationFunction.ConnectionString(), YearIdno, dtfrom, dtto, LocIdno, ItemIdno);
            obj = null;
            if (dt != null && dt.Rows.Count > 0)
            {

                DataTable DTT = dt.Clone();
                DTT = dt.Copy();
                Int64 OS = 0; Int64 CL = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OS += Convert.ToInt64(DTT.Rows[i]["OS"]);
                    CL += Convert.ToInt64(DTT.Rows[i]["CL"]);
                    if (i == dt.Rows.Count - 1)
                    {
                        DataRow drr = DTT.NewRow();
                        drr["Item_Name"] = "Total";
                        drr["OS"] = Convert.ToString(OS);
                        drr["CL"] = Convert.ToString(CL);
                        lblOpenTot.Text = Convert.ToString(OS);
                        lblClosTot.Text = Convert.ToString(CL);
                        DTT.Rows.Add(drr);
                        break;
                    }
                }
                if (DTT != null && DTT.Rows.Count > 0)
                {
                    DTT.Columns[0].Caption = "Item Name";
                    DTT.Columns[1].Caption = "Opening";
                    DTT.Columns[2].Caption = "Closing";
                    DTT.AcceptChanges();
                    ViewState["Dt"] = DTT;
                }


                grdMain.DataSource = dt;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record : " + (dt.Rows.Count).ToString();
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;


                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dt.Rows.Count.ToString();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record : 0 ";
                imgBtnExcel.Visible = false;
                divpaging.Visible = false;
            }
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
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GrPrepation.xls"));
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

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex != 0)
            {
                txtDate.Text = hidmindate.Value;
                txtDateTO.Text = hidmaxdate.Value;
            }
            else
            {
                txtDate.Text = hidmindate.Value;
                txtDateTO.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }

        private void BindItemName()
        {
            CurrentStockRpt obj = new CurrentStockRpt();
            var itmName = obj.BindActiveAssItemName();
            ddlItemName.DataSource = itmName;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFrom();
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserId)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserId);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "CityIdno";
            ddlFromCity.DataBind();
            obj = null;
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");


            }
        }

        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion

        #region control events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        #endregion

        #region print...

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }


        }

        #endregion

        protected void grdMain_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TOS += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OS"));
                TCL += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CL"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTOS = (Label)e.Row.FindControl("lblOSTotal");
                lblTOS.Text = TOS.ToString();


                Label lblTCL = (Label)e.Row.FindControl("lblCLTotal");
                lblTCL.Text = TCL.ToString();
            }
        }

    }
}