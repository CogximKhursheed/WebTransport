using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;

namespace WebTransport
{
    public partial class AccessoryStockReport : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
        double TotOpening = 0.00;
        double TotPur = 0.00;
        double TotIssue = 0.00;
        double TotalSale = 0.00;
        double Total = 0.00;
        double TotBalance = 0.00;
        double TotTran = 0.00;
        double TotRecv = 0.00;

        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                this.BindItemName();
                this.BindLocation();

                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
                this.TotalRecords();
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
                if (UserClass != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }
                AccessoryStockRpt obj = new AccessoryStockRpt();
                Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedIndex);
                //DataTable list1 = obj.SelectAccStockReport(ApplicationFunction.ConnectionString(), Datefrom.Text.Trim(), Dateto.Text.Trim(), yearidno, Convert.ToInt64(ddlItemName.SelectedValue), 2, Convert.ToInt64(ddlLocation.SelectedValue));
                //lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }
        private void BindGrid()
        {
            AccessoryStockRpt obj = new AccessoryStockRpt();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            string strchkDetlValue = string.Empty;
            Int64 ItemIdno = Convert.ToInt64(ddlItemName.SelectedValue);
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));

            if (ddlItemName.SelectedValue != "0")
            {
                DataTable dt = obj.SelectAccStockReport(ApplicationFunction.ConnectionString(), Datefrom.Text.Trim(), Dateto.Text.Trim(), yearidno, Convert.ToString(ddlItemName.SelectedValue), 2, Convert.ToInt64(ddlLocation.SelectedValue));
            }
            else
            {
                DataTable grdGrdetals = obj.FetchItemIDno(ApplicationFunction.ConnectionString());
                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {

                        strchkDetlValue = strchkDetlValue + grdGrdetals.Rows[count]["ITEM_IDNO"] + ",";
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                }
                DataTable dt = obj.SelectAccStockReport(ApplicationFunction.ConnectionString(), Datefrom.Text.Trim(), Dateto.Text.Trim(), yearidno, strchkDetlValue, 2, Convert.ToInt64(ddlLocation.SelectedValue));
            }
            DataTable dtnew = obj.FetchAccStockReport(ApplicationFunction.ConnectionString());
            obj = null;
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                Double TotalIssue = 0, TotalOpening = 0, TotalPur =0, TotSale= 0, Total=0, TotalBal=0;


                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    TotalOpening += Convert.ToDouble(dtnew.Rows[i]["OpenBal"]);
                    TotalPur += Convert.ToDouble(dtnew.Rows[i]["Purchase"]);
                    TotalIssue += Convert.ToDouble(dtnew.Rows[i]["MtrlIss"]);
                    TotSale += Convert.ToDouble(dtnew.Rows[i]["Sale"]);
                    Total += Convert.ToDouble(dtnew.Rows[i]["Total"]);
                    TotalBal += Convert.ToDouble(dtnew.Rows[i]["BalQty"]);
                }
                lblTotOpening.Text = TotalOpening.ToString();
                lblTotPur.Text = TotalPur.ToString();
                lblTotSale.Text = TotSale.ToString();
                lblTotal.Text = Total.ToString();
                lblTotIssue.Text = TotalIssue.ToString();
                lblBalance.Text = TotalBal.ToString();
                lblTotRecv.Text = Convert.ToString(dtnew.Compute("Sum(StkRec)", ""));
                lblTotTran.Text = Convert.ToString(dtnew.Compute("Sum(StkTrns)", ""));
                if (dtnew != null && dtnew.Rows.Count > 0)
                {
                    ViewState["Dt"] = dtnew;
                }

                grdMain.DataSource = dtnew;
                grdMain.DataBind();
                //lblTotalRecord.Text = "T. Record(s): " + dt.Rows.Count;
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dtnew.Rows.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
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
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefrom.Text = hidmindate.Value;
                    Dateto.Text = hidmaxdate.Value;
                }
            }
        }

        private void BindItemName()
        {
            AccessoryStockRpt obj = new AccessoryStockRpt();
            var itmName = obj.BindActiveItemName();
            ddlItemName.DataSource = itmName;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindLocation()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
           
        }

        
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotOpening += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpenBal"));
                TotPur += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Purchase"));
                TotIssue += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MtrlIss"));
                TotalSale += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Sale"));
                Total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
                TotBalance += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BalQty"));
                //TotRecv += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RECV"));
                //TotTran += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TRAN"));
                
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotOpening = (Label)e.Row.FindControl("lblTotOpening");
                lblTotOpening.Text = TotOpening.ToString();

                Label lblTotPur = (Label)e.Row.FindControl("lblTotPur");
                lblTotPur.Text = TotPur.ToString();

                Label lblTotIssue = (Label)e.Row.FindControl("lblTotIssue");
                lblTotIssue.Text = TotIssue.ToString();

                Label lblTotSale = (Label)e.Row.FindControl("lblTotSale");
                lblTotSale.Text = TotalSale.ToString();

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = Total.ToString();

                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                lblBalance.Text = TotBalance.ToString();

                //Label lblTotRECV = (Label)e.Row.FindControl("lblTotRECV");
                //lblTotRECV.Text = TotRecv.ToString();

                //Label lblTotTRAN = (Label)e.Row.FindControl("lblTotTRAN");
                //lblTotTRAN.Text = TotTran.ToString();
            }

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

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        #endregion

        #region control events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        //protected void txtSerialNo_OnTextChanged(object sender, EventArgs e)
        //{
        //    TyreStockRpt obj = new TyreStockRpt();
        //    if (txtSerialNo.Text != "")
        //    {
        //        var serialno = obj.GetItemInfo(txtSerialNo.Text.Trim());
        //        if (serialno != null)
        //            ddlItemName.SelectedValue = Convert.ToString(serialno.ItemIdno);
        //    }
        //}

        #endregion

        #region print...
        private void ExportGridView()
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TyreStockReport.xls"));
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
            grdMain.Columns[0].Visible = true;
        }

        #endregion
    }
}