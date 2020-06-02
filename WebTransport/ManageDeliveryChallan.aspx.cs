using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
namespace WebTransport
{
    public partial class ManageDeliveryChallan : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28;
        double dGrossAmnt = 0, dNetAmnt = 0, dKattAmnt = 0, dOtherAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtChlnNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
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
                //this.BindState();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindFromCity();
                }
                else
                {
                    this.BindCityFrom(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityDelivery.SelectedValue = Convert.ToString(base.UserFromCity);
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtChlnNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                //txtGRNo.Attributes.Add("onmouseover", "javascript:this.style.color='gold'");
                //txtGRNo.Attributes.Add("onmouseout", "javascript:this.style.color='black'");
                Bind();
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);

                Countall();
                ddlDateRange.Focus();
                //this.binddate
                //selectectd
                //==0 or -1
                prints.Visible = false;
            }
        }
        #endregion

        #region Functions...
        public void Countall()
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();

            Int64 count = obj.countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
        }
        private void BindGrid()
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int32 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int ChallanNo = string.IsNullOrEmpty(Convert.ToString(txtChlnNo.Text)) ? 0 : Convert.ToInt32(txtChlnNo.Text);

            Int32 DriverIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);

            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            int cityfrom = Convert.ToInt32(drpCityDelivery.SelectedValue);
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            var lstGridData = obj.SearchDelvChallan(UserIdno, yearIDNO, dtfrom, dtto, cityfrom, ChallanNo, DriverIdno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("DelvChlnNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Lorry", typeof(string));
                dt.Columns.Add("Party", typeof(string));
                dt.Columns.Add("Groos Amount", typeof(string));
                dt.Columns.Add("Katt Amount", typeof(string));
                dt.Columns.Add("Other Amount", typeof(string));
                dt.Columns.Add("Net Amount", typeof(string));


                double TNet = 0, TKatt = 0, TGross = 0, TOther = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["DelvChlnNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DelvChln_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "DelvChln_Date")).ToString("dd-MM-yyyy");
                    dr["City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "City_Name"));
                    dr["Lorry"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["Party"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["Groos Amount"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Gross_Amnt"));
                    dr["Katt Amount"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Katt_Amnt"));
                    dr["Other Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Other_Amnt")).ToString("N2");
                    dr["Net Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);
                    TKatt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Katt_Amnt"));
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    TGross += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Gross_Amnt"));
                    TOther += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Other_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Party"] = "Total";
                        dr["Groos Amount"] = (TGross).ToString("N2");
                        dr["Katt Amount"] = (TKatt).ToString("N2");
                        dr["Other Amount"] = (TOther).ToString("N2");
                        dr["Net Amount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //grdprint.DataSource = lstGridData;
                //grdprint.DataBind();

                Double TotalNetAmount = 0; Double TotalGrossAmnt = 0; Double TotalKatAmnt = 0; Double TotalOtherAmnt = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalGrossAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Gross_Amnt"));
                    TotalKatAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Katt_Amnt"));
                    TotalOtherAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Other_Amnt"));
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalGrossAmnt.ToString("N2");

                lblKatAmount.Text = TotalKatAmnt.ToString("N2");
                lblNetAmount.Text = TotalNetAmount.ToString("N2");
                lblOtherAmount.Text = TotalOtherAmnt.ToString("N2");


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
                lblcontant.Visible = false;
                divpaging.Visible = false;
                //grdprint.DataSource = null;
                //grdprint.DataBind();
                prints.Visible = false;
            }
        }
        private void BindFromCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                drpCityDelivery.DataSource = lst;
                drpCityDelivery.DataTextField = "City_Name";
                drpCityDelivery.DataValueField = "City_Idno";
                drpCityDelivery.DataBind();
                drpCityDelivery.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void BindFromCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityDelivery.DataSource = FrmCity;
            drpCityDelivery.DataTextField = "CityName";
            drpCityDelivery.DataValueField = "CityIdno";
            drpCityDelivery.DataBind();
            obj = null;
            drpCityDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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


        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }

        private void Bind()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddlTruckNo.DataSource = lst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();

            }
            ddlTruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindCityFrom(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityDelivery.DataSource = FrmCity;
            drpCityDelivery.DataTextField = "CityName";
            drpCityDelivery.DataValueField = "cityidno";
            drpCityDelivery.DataBind();
            drpCityDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        ///// <summary>
        ///// To Bind State DropDown
        ///// </summary>
        //private void BindState()
        //{
        //    CityMastDAL objclsCityMaster = new CityMastDAL();
        //    var objCityMast = objclsCityMaster.SelectState();
        //    objclsCityMaster = null;
        //    drpState.DataSource = objCityMast;
        //    drpState.DataTextField = "State_Name";
        //    drpState.DataValueField = "State_Idno";
        //    drpState.DataBind();
        //    drpState.Items.Insert(0, new ListItem("< Choose State >", "0"));
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
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("DeliveryChallanDetails.aspx?dc=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtChlnNo.Focus();
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
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dGrossAmnt = dGrossAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
                dKattAmnt = dKattAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Katt_Amnt"));
                dOtherAmnt = dOtherAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Other_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblNetAmnt");
                lblAmount.Text = Convert.ToDouble(dNetAmnt).ToString("N2");
                Label lblGrossAmount = (Label)e.Row.FindControl("lblGrossAmnt");
                lblGrossAmount.Text = Convert.ToDouble(dGrossAmnt).ToString("N2");
                Label lblKattAmount = (Label)e.Row.FindControl("lblKattAmnt");
                lblKattAmount.Text = Convert.ToDouble(dKattAmnt).ToString("N2");
                Label lblOtherAmount = (Label)e.Row.FindControl("lblOtherAmnt");
                lblOtherAmount.Text = Convert.ToDouble(dOtherAmnt).ToString("N2");
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=ReceiptGoodsReceived.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    grdprint.AllowPaging = false;


            //    grdprint.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in grdprint.HeaderRow.Cells)
            //    {
            //        cell.BackColor = grdprint.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in grdprint.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = grdprint.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = grdprint.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    grdprint.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
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

    }    
}