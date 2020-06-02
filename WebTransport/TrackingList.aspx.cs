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
using System.Configuration;
using System.Data;
namespace WebTransport
{
    public partial class TrackingList : Pagebase
    {
        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {  
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                this.BindDateRange();
                this.BindLane();
                this.BindFromCity();
                this.BindToCity();
                this.BindDropDown();
                DdlLane.SelectedValue = Convert.ToString(base.UserFromCity);
                DdlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                DdlFromCity.SelectedValue = Convert.ToString(base.UserToCity);
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);
                this.Countall();
                txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            }
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
        #endregion

        #region Functions...
        public void Countall()
        {
            TrackingDAL obj = new TrackingDAL();
            Int64 count = obj.CountALL();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindGrid()
        {
            TrackingDAL obj = new TrackingDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            int vehicleno = Convert.ToInt32((DdlVehicleNo.SelectedIndex <= 0) ? "0" : DdlVehicleNo.SelectedValue);
            
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }
            int fromloc = Convert.ToInt32((DdlFromLoc.SelectedIndex <= 0) ? "0" : DdlFromLoc.SelectedValue);
            int compname = Convert.ToInt32((DdlCompName.SelectedIndex <= 0) ? "0" : DdlCompName.SelectedValue);
            int Lane = Convert.ToInt32((DdlLane.SelectedIndex <= 0) ? "0" : DdlLane.SelectedValue);
            int FromCity = Convert.ToInt32((DdlFromCity.SelectedIndex <= 0) ? "0" : DdlFromCity.SelectedValue);
            int ToCity = Convert.ToInt32((DdlToCity.SelectedIndex <= 0) ? "0" : DdlToCity.SelectedValue);
            Int64 UserIdno = 0;
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }
            var lstGridData = obj.search(Convert.ToInt32(ddldateRange.SelectedValue), dtfrom, dtto, vehicleno, fromloc, compname, Lane, FromCity, ToCity, UserIdno);
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Sr No.", typeof(string));
                dt.Columns.Add("Vehicle No.", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("From Loc.", typeof(string));
                dt.Columns.Add("Company Name", typeof(string));
                dt.Columns.Add("Lane", typeof(string));
                dt.Columns.Add("From City", typeof(string));
                dt.Columns.Add("To City", typeof(string));
               
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Convert.ToString(i + 1);
                    dr["Vehicle No."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Tracking_Date")).ToString("dd-MM-yyyy");
                    dr["From Loc."] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LocFrom"));
                    dr["Company Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Comp_Name"));
                    dr["Lane"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lane_Name"));
                    dr["From City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCity"));
                    dr["To City"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ToCity"));
                    dt.Rows.Add(dr);
                   
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Vehicle No."] = "";
                        drr["Date"] = "";
                        drr["From Loc."] = "";
                        drr["Company Name"] = "";
                        drr["Lane"] = "";
                        drr["From City"] = "";
                        drr["To City"] = "";

                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
            }
        }
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataSource = lst;
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
        }
        private void SetDate()
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtReceiptDatefrom.Text = hidmindate.Value;
                    txtReceiptDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtReceiptDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtReceiptDateto.Text = Convert.ToString(hidmaxdate.Value);
                }
            }
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void BindLane()
        {
            TrackingDAL obj = new TrackingDAL();
            var LaneList = obj.BindLaneName();
            obj = null;
            if (LaneList.Count > 0)
            {
                DdlLane.DataSource = LaneList;
                DdlLane.DataTextField = "Lane_Name";
                DdlLane.DataValueField = "Lane_Idno";
                DdlLane.DataBind();
                DdlLane.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }
        private void BindDropDown()
        {
            TrackingDAL obj = new TrackingDAL();
            var ToComp = obj.BindCompany();
            var ToFromLoc = obj.BindFromLoc();
            var ToTruck = obj.BindLorry();
            obj = null;

            DdlCompName.DataSource = ToComp;
            DdlCompName.DataTextField = "Comp_Name";
            DdlCompName.DataValueField = "city_idno";
            DdlCompName.DataBind();
            DdlCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlFromLoc.DataSource = ToFromLoc;
            DdlFromLoc.DataTextField = "City_Name";
            DdlFromLoc.DataValueField = "City_Idno";
            DdlFromLoc.DataBind();
            DdlFromLoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlVehicleNo.DataSource = ToTruck;
            DdlVehicleNo.DataTextField = "Lorry_No";
            DdlVehicleNo.DataValueField = "Lorry_Idno";
            DdlVehicleNo.DataBind();
            DdlVehicleNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindFromCity()
        {
            TrackingDAL obj = new TrackingDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;
            if (ToCity.Count > 0)
            {
                DdlFromCity.DataSource = ToCity;
                DdlFromCity.DataTextField = "city_name";
                DdlFromCity.DataValueField = "city_idno";
                DdlFromCity.DataBind();
                DdlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }
        private void BindToCity()
        {
            TrackingDAL obj = new TrackingDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;
            if (ToCity.Count > 0)
            {
                DdlToCity.DataSource = ToCity;
                DdlToCity.DataTextField = "city_name";
                DdlToCity.DataValueField = "city_idno";
                DdlToCity.DataBind();
                DdlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }
        #endregion

        #region Grid Events....
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = "";
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("Tracking.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                TrackingDAL obj = new TrackingDAL();
                Int32 intValue =  obj.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    ShowMessage("Record  deleted Successfully");
                    DdlVehicleNo.Focus();
                }
                else
                {
                    if (intValue == -1)
                        ShowMessage("Record can not be deleted. It is in use.");
                    
                    else
                        ShowMessage("Record not deleted.");

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TrackingDAL obj = new TrackingDAL();
                    Int64 TrackingIdno = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "TrackingHead_Idno"));
                    if ((obj.CheckBilled(TrackingIdno, ApplicationFunction.ConnectionString())) > 0)
                    {
                        LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    }
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region Control Events...
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }
        #endregion

        #region Export to excel
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            grdMain.Columns[1].Visible = false;
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Tracking.xls"));
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
        #endregion
        #region GridViewEvents
        protected void DdlLane_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DdlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DdlToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        #endregion
    }
}