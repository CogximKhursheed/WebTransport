using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;

namespace WebTransport
{
    public partial class CurrentStockReport : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; DataTable dt = new DataTable(); int x = 0;
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
                this.SetDate();
                this.TotalRecords();
                ddlDateRange_SelectedIndexChanged(null, null);
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
                CurrentStockRpt obj = new CurrentStockRpt();
                Int64 YearId = Convert.ToInt64(ddlDateRange.SelectedValue);
                DataTable list1 = obj.SelectCurrentStockReport(ApplicationFunction.ConnectionString(), Convert.ToDateTime(hidmindate.Value), 0, "", 0, YearId);
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(list1.Rows.Count);

            }
        }
        private void BindGrid()
        {
            CurrentStockRpt obj = new CurrentStockRpt();
            Int64 ItemIdno = Convert.ToInt64((ddlItemName.SelectedValue) == "" ? "0" : ddlItemName.SelectedValue);
            string serialNo = txtSerialNo.Text.Trim();
            Int64 YearID = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 LocIdno = Convert.ToInt64((ddlFromCity.SelectedValue) == "" ? "0" : ddlFromCity.SelectedValue);
            dt = obj.SelectCurrentStockReport(ApplicationFunction.ConnectionString(), Convert.ToDateTime(hidmindate.Value), ItemIdno, serialNo, LocIdno, YearID);
            obj = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataColumn newNetTotal = new DataColumn("Qty", typeof(string));
                newNetTotal.AllowDBNull = true;
                dt.Columns.Add(newNetTotal);

                string strItemIdno = Convert.ToString(dt.Rows[0]["ItemIdno"]);

                //outer variable
                double dqty = 0;
                double dItemRate = 0;

                // inner variable
                double ddqty = 0;
                Double ddItemRate = 0;

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    dqty += 1;
                    dt.Rows[k]["Qty"] = 1;

                    dItemRate += Convert.ToDouble(dt.Rows[k]["RATE"].ToString());
                    // *****************************************************************************************************************************

                    if (Convert.ToString(dt.Rows[k]["ItemIdno"]) == strItemIdno)
                    {
                        //dt.Rows[k]["Qty"] = 1;
                        ddqty += 1;

                        ddItemRate += Convert.ToDouble(dt.Rows[k]["RATE"].ToString());
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr.BeginEdit();
                        dr[0] = ""; dr[1] = ""; dr[3] = "Group Total:"; dr[7] = ddItemRate.ToString("N2"); dr[8] = ddqty.ToString();
                        
                        dt.Rows.InsertAt(dr, k);
                        dt.AcceptChanges();

                        ddqty = 0; ddItemRate = 0;

                        if (k != 0)
                        {
                            ddqty += 1;

                            ddItemRate += Convert.ToDouble(dt.Rows[k+1]["RATE"].ToString());
                        }
                        k++;
                        strItemIdno = Convert.ToString(dt.Rows[k]["ItemIdno"]);
                    }

                    // *****************************************************************************************************************************
                }

                DataRow dr1 = dt.NewRow();
                dr1.BeginEdit();
                dr1[0] = ""; dr1[1] = ""; dr1[3] = "Group Total:"; dr1[7] = ddItemRate.ToString("N2"); dr1[8] = ddqty.ToString();


                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2.BeginEdit();
                dr2[0] = ""; dr2[1] = ""; dr2[2] = "";

                dt.Rows.Add(dr2);



                DataRow dr3 = dt.NewRow();
                dr3.BeginEdit();

                dr3[0] = ""; dr3[1] = ""; dr3[3] = "Total:"; dr3[7] = dItemRate.ToString("N2"); dr3[8] = dqty.ToString();

                dt.Rows.Add(dr3);

                grdMain.Columns.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    BoundField bfield = new BoundField();
                    bfield.HeaderText = Convert.ToString(dt.Columns[i].ColumnName);
                    bfield.DataField = Convert.ToString(dt.Columns[i].ColumnName);
                    grdMain.Columns.Add(bfield);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                grdMain.DataSource = dt;
                grdMain.DataBind();


                grdMain.HeaderRow.Cells[0].Text = "Item Name";
                grdMain.HeaderRow.Cells[1].Text = "Brand";
                grdMain.HeaderRow.Cells[2].Text = "Serial No";
                grdMain.HeaderRow.Cells[3].Text = "Type";
                grdMain.HeaderRow.Cells[4].Text = "Days";
                grdMain.HeaderRow.Cells[6].Text = "Location";
                grdMain.Columns[0].HeaderStyle.Width = 140;
                grdMain.Columns[1].HeaderStyle.Width = 140;
                grdMain.Columns[2].HeaderStyle.Width = 140;
                grdMain.Columns[3].HeaderStyle.Width = 140;
                grdMain.Columns[4].HeaderStyle.Width = 140;
                grdMain.Columns[6].HeaderStyle.Width = 130;
                grdMain.Columns[7].HeaderStyle.Width = 130;
                grdMain.Columns[8].HeaderStyle.Width = 100;

                grdMain.Columns[0].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[1].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                grdMain.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdMain.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdMain.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                grdMain.Columns[5].Visible = false;

                lblTotalRecord.Text = "T. Record : " + dqty.ToString();
                imgBtnExcel.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record : 0 ";
                imgBtnExcel.Visible = false;
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

        }

        private void BindItemName()
        {
            CurrentStockRpt obj = new CurrentStockRpt();
            var itmName = obj.BindActiveItemName();
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

        //protected void txtSerialNo_OnTextChanged(object sender, EventArgs e)
        //{
        //    CurrentStockRpt obj = new CurrentStockRpt();
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
                dt.Columns.Remove("ItemIdno");
                Export(dt);
            }
        }

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CurrentStockReport[Tyre].xls"));
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
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
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
            grdMain.Columns[5].Visible = false;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[0].Visible = true;
            
        }

        #endregion

        protected void grdMain_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int a = grdMain.Rows.Count + 1;
                if (a > 1)
                {
                    string s = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "STYPE"));
                    int i = Convert.ToInt32(e.Row.RowIndex);
                    if ((s == "Group Total:") || (s == "Total:") || s == "")
                    {
                        if (x > 0)
                        {
                            grdMain.Rows[x].Cells[3].Font.Bold = true;
                        }
                        x = i;
                    }
                }
            }
        }

    }
}