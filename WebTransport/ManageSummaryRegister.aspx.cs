using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Data;
namespace WebTransport
{
    public partial class ManageSummaryRegister : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dGrossAmnt = 0, dNetAmnt = 0;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtsummryno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtCHlnno.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
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
                    this.BindCityTo();
                }
                else
                {
                    this.BindCityTo(Convert.ToInt64(Session["UserIdno"]));
                }
                drpCityTo.SelectedValue = Convert.ToString(base.UserFromCity);
                Datefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                Dateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                //txtGRNo.Attributes.Add("onmouseover", "javascript:this.style.color='gold'");
                //txtGRNo.Attributes.Add("onmouseout", "javascript:this.style.color='black'");
                BindDropdown();
                this.BindDateRange();
                this.TotalRecords();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange_SelectedIndexChanged(null, null);
                // this.BindCity();
                //this.binddate
                //selectectd
                //==0 or -1
            }
        }
        #endregion

        #region Functions...

        private void TotalRecords()
        {
            SummaryRegisterDAL obj = new SummaryRegisterDAL();

            lblTotalRecord.Text = "T. Records: " + obj.TotalRecords().ToString();
        }

        private void BindDropdown()
        {
            SummaryRegisterDAL objsummary = new SummaryRegisterDAL();
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            //    var ToCity = obj.BindToCity();
            obj = null;

            var driver = objsummary.selectHireDriverName();
            objsummary = null;
            if (driver != null && driver.Count > 0)
            {
                ddlDriver.DataSource = driver;
                ddlDriver.DataTextField = "Driver_name";
                ddlDriver.DataValueField = "Driver_Idno";
                ddlDriver.DataBind();
                ddlDriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }


            drptruckno.DataSource = TruckNolst;
            drptruckno.DataTextField = "Lorry_No";
            drptruckno.DataValueField = "lorry_idno";
            drptruckno.DataBind();
            drptruckno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        private void BindGrid()
        {
            SummaryRegisterDAL obj = new SummaryRegisterDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            Int64 yearIDNO = Convert.ToInt32(ddlDateRange.SelectedValue);
            int summryno = string.IsNullOrEmpty(Convert.ToString(txtsummryno.Text)) ? 0 : Convert.ToInt32(txtsummryno.Text);
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(Datefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Dateto.Text));
            }
            int citto = Convert.ToInt32(drpCityTo.SelectedValue == "" ? 0 : Convert.ToInt32(drpCityTo.SelectedValue));
            int truckno = Convert.ToInt32(drptruckno.SelectedValue == "" ? 0 : Convert.ToInt32(drptruckno.SelectedValue));
            int driver = Convert.ToInt32(ddlDriver.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDriver.SelectedValue));
            int chlnno = Convert.ToInt32(txtCHlnno.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCHlnno.Text.Trim()));

            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            var lstGridData = obj.SelectSummary(summryno, citto, truckno, chlnno, driver, dtfrom, dtto, yearidno, UserIdno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("SummaryNo", typeof(string));
                dt.Columns.Add("SummaryDate", typeof(string));
                dt.Columns.Add("City To", typeof(string));
                dt.Columns.Add("Challan No", typeof(string));
                dt.Columns.Add("Truck No", typeof(string));
                dt.Columns.Add("Driver", typeof(string));
                dt.Columns.Add("NetAmount", typeof(string));

                double TNet = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["SummaryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SumReg_No"));
                    dr["SummaryDate"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "SumReg_Date")).ToString("dd-MM-yyyy");
                    dr["City To"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityTo"));
                    dr["Challan No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Chln_no"));
                    dr["Truck No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["Driver"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "driver"));
                    dr["NetAmount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt")).ToString("N2");
                    dt.Rows.Add(dr);

                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Driver"] = "Total";
                        drr["NetAmount"] = (TNet).ToString("N2");
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
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;
                //grdprint.DataSource = lstGridData;
                //grdprint.DataBind();
                imgBtnExcel.Visible = true;

                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
                lblTotalRecord.Text = "Total Record (s): 0 ";
                //grdprint.DataSource = null;
                //grdprint.DataBind();
                imgBtnExcel.Visible = false;

                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SummaryRegister.xls"));
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
        private void BindCityTo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;
            drpCityTo.DataSource = ToCity;
            drpCityTo.DataTextField = "City_Name";
            drpCityTo.DataValueField = "City_Idno";
            drpCityTo.DataBind();
            drpCityTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindCityTo(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpCityTo.DataSource = FrmCity;
            drpCityTo.DataTextField = "CityName";
            drpCityTo.DataValueField = "cityidno";
            drpCityTo.DataBind();
            drpCityTo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
                Response.Redirect("SummaryRegister.aspx?SummaryIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                SummaryRegisterDAL obj = new SummaryRegisterDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtsummryno.Focus();
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

                Label lblGridNo = (Label)e.Row.FindControl("lblGridNo");
                Int32 intGRIdno = Convert.ToInt32(lblGridNo.Text);
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = dNetAmnt.ToString("N2");
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region IMport excel....
        private void ExportGridView()
        {
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[1].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
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
                if (gv.Controls[i].GetType() == typeof(Label))
                {
                    l.Text = (gv.Controls[i] as Label).Text;
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
        #endregion
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