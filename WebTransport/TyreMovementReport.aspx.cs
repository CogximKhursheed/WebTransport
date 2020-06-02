using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;


namespace WebTransport
{
    public partial class TyreMovementReport : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
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
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.BindDateRange();
                this.BindItemName();
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
                TyreMoveRpt obj = new TyreMoveRpt();
                Int64 list1 = obj.TotalRecords(Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text)), Convert.ToInt32(ddlDateRange.SelectedIndex), 0, "");
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1);

            }
        }

        private void BindGrid()
        {
            TyreMoveRpt obj = new TyreMoveRpt();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 ItemIdno = Convert.ToInt64(ddlItemName.SelectedValue);
            string serialNo = txtSerialNo.Text.Trim();

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));

            var lstGridData = obj.SelectTyreMovementReport(dtfrom, dtto, yearidno, ItemIdno, serialNo);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Item Name", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("In Type", typeof(string));
                dt.Columns.Add("Doc No.", typeof(string));
                dt.Columns.Add("Doc Date", typeof(string));
                dt.Columns.Add("Out Type", typeof(string));

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Item Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ItemName"));
                    dr["Location"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Location"));
                    dr["In Type"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "StckIn"));
                    dr["Doc No."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DocNo"));
                    dr["Doc Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "DocDate")).ToString("dd-MM-yyyy");
                    dr["Out Type"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "StckOut"));
                    dt.Rows.Add(dr);
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Item Name"] = "";
                        drr["Location"] = "";
                        drr["In Type"] = "";
                        drr["Doc No."] = "";

                        drr["Doc Date"] = "";
                        drr["Out Type"] = "";

                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;
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
            TyreMoveRpt obj = new TyreMoveRpt();
            var itmName = obj.BindActiveItemName();
            ddlItemName.DataSource = itmName;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

        //protected void txtSerialNo_OnTextChanged(object sender, EventArgs e)
        //{
        //    TyreMoveRpt obj = new TyreMoveRpt();
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TyreMovementReport.xls"));
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