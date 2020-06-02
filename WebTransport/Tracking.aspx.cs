using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.Transactions;

namespace WebTransport
{
    public partial class Tracking : Pagebase
    {
        #region Variable ...
        DataTable dtTemp = new DataTable();
        private int intFormId = 30;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (base.CheckUserRights(intFormId) == false)
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            if (base.ADD == false)
            {
                lnkbtnSave.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                this.ValidateControls();
                this.BindDropdown();
                this.BindCityDropDown();
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                DdlVehicleNo.Focus();
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnMnNew.Visible = true;
                }
            }
        }
        #endregion

        #region controls
        private void ValidateControls()
        {  
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtLeg.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtETA.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtATA.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtETD.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtATD.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtTAThrs.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            Txtdelayhrs.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtremark.Attributes.Add("onkeypress", "return allowAlphabetAndNumerAndSymbols(event);");
            txtETA.Text= Convert.ToString("0:00");
            txtATA.Text= Convert.ToString("0:00");
            txtETD.Text= Convert.ToString("0:00");
            txtATD.Text= Convert.ToString("0:00");
            txtTAThrs.Text= Convert.ToString("0:00");
            Txtdelayhrs.Text= Convert.ToString("0:00");
        }
        #endregion

        #region Button ClickEvent
        protected void lnkbtnMnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Tracking.aspx");
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int64 intIdno = 0;

            #region Validation Messages for Tracking Details

            if (DdlVehicleNo.SelectedIndex == 0)
            {
                this.ShowMessage("Please Enter Vehicle No");
                DdlVehicleNo.Focus();
                return;
            }
            if (DdlLane.SelectedIndex == 0) 
            { 
                this.ShowMessage("Please select Lane");
                DdlLane.Focus();
                return;
            }
            if (DdlFromCity.SelectedIndex == 0) 
            { 
                this.ShowMessage("Please select From City");
                DdlFromCity.Focus(); 
                return;
            }
            if (DdlToCity.SelectedIndex == 0) 
            {
                this.ShowMessage("Please select To City");
                DdlToCity.Focus(); 
                return;
            }
            if (DdlCompName.SelectedIndex == 0)
            {
                this.ShowMessage("Please Enter Company Name");
                DdlCompName.Focus();
                return;
            }
            if (DdlFromLoc.SelectedIndex == 0)
            {
                this.ShowMessage("Please Enter From Location");
                DdlFromLoc.Focus();
                return;
            }

            dtTemp = (DataTable)ViewState["dt"];

            if (dtTemp != null)
            {
                if (dtTemp.Rows.Count <= 0)
                {
                    ShowMessage("Please enter details");
                    return;
                }
            }
            if (grdMain.Rows.Count <= 0)
            {
                ShowMessage("Please enter details");
                return;
            }

            #endregion

            Int64 trackingIdno = 0;
            TrackingDAL objtrackingDAL = new TrackingDAL();
            if (Request.QueryString["q"] != null)
            {
                trackingIdno = Convert.ToInt64(Request.QueryString["q"]);
            }

            Int64 VehicleNo = Convert.ToInt64(DdlVehicleNo.SelectedValue);
            string Date = txtDate.Text;
            Int64 LaneIdno = Convert.ToInt64(DdlLane.SelectedValue);
            Int64 CompName = Convert.ToInt64(DdlCompName.SelectedValue);
            Int64 FromLoc = Convert.ToInt64(DdlFromLoc.SelectedValue);
            Int64 FromCityIdno = Convert.ToInt64(DdlFromCity.SelectedValue);
            Int64 ToCityIdno = Convert.ToInt64(DdlToCity.SelectedValue);
            DataTable dtDetail = (DataTable)ViewState["dt"];
            if (this.ValidationEstNoSave((Request.QueryString["q"] != null) ? Convert.ToInt64(Request.QueryString["q"]) : 0))
                    // if (this.ValidationEstNoSave((Request.QueryString["q"] != null) ? Convert.ToInt64(Request.QueryString["q"]) : 0))
            {
                using (TransactionScope dbTran = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (grdMain.Rows.Count > 0 && dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(hidid.Value) == false)
                            {
                                intIdno = objtrackingDAL.Update(trackingIdno, VehicleNo, Date, LaneIdno, FromCityIdno, ToCityIdno, CompName, FromLoc, dtDetail);
                            }
                            else
                            {
                                intIdno = objtrackingDAL.Insert(trackingIdno, VehicleNo, Date, LaneIdno, FromCityIdno, ToCityIdno, CompName, FromLoc, dtDetail);
                            }
                            objtrackingDAL = null;
                        }
                        if (intIdno > 0)
                        {
                            dbTran.Complete();
                            dbTran.Dispose();
                            ShowMessage("Record  saved Successfully");
                        }
                        else if (intIdno < 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vehicle already made with this No.')", true);
                            dbTran.Dispose();
                        }
                        else
                        {
                            if (Request.QueryString["q"] != null)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record not updated.')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record(s) not saved.')", true);
                            }
                            dbTran.Dispose();
                        }
                        Clear();
                        ClearItems();
                        BindCityDropDown();
                        grdMain.DataSource = null;
                        grdMain.DataBind();
                    }
                    catch (Exception exe)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Some issue occure, Please Contact Cogxim Support Executive.')", true);
                        dbTran.Dispose();
                    }
                }
            }
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
            {
            if (DdlVehicleNo.SelectedValue == "0")
            {
                ShowMessage("Please Select Vehicle No.");
                DdlVehicleNo.Focus();
                return;
            }
            if (DdlLane.SelectedValue == "0")
            {
                ShowMessage("Please Select Lane.");
                DdlLane.Focus();
                return;
            }
            if (DdlFromCity.SelectedValue == "0")
            {
                ShowMessage("Please Enter from City.");
                DdlFromCity.Focus();
                return;
            }
            if (DdlToCity.SelectedValue == "0")
            {
                ShowMessage("Please Enter To City.");
                DdlToCity.Focus(); 
                return;
            }
            if (DdlFromLoc.SelectedValue == "0")
            {
                ShowMessage("Please Enter From Location.");
                DdlFromLoc.Focus();
                return;
            }
            if (DdlCompName.SelectedValue == "0")
            {
                ShowMessage("Please Enter Company Name.");
                DdlCompName.Focus();
                return;
            }
            if (DdlFromCty.SelectedValue == "0")
            {
                ShowMessage("Please Enter from City.");
                DdlFromCty.Focus();
                return;
            }
            if (DdlToCty.SelectedValue == "0")
            {
                ShowMessage("Please Enter To City.");
                DdlToCty.Focus();
                return;
            }
            if (txtLeg.Text == "")
            {
                ShowMessage("Leg should be greater than zero."); 
                txtLeg.Focus(); 
                return;
            }
            if (txtETA.Text == "" || txtETA.Text == "0:00")
            {
                ShowMessage("ETA should be greater than zero."); 
                txtETA.Focus(); 
                return;
            }
            if (txtATA.Text == "" || txtATA.Text == "0:00")
            {
                ShowMessage("ATA should be greater than zero."); 
                txtATA.Focus();
                return;
            }
            if (txtETD.Text == "" || txtETD.Text == "0:00")
            {
                ShowMessage("ETD should be greater than zero.");
                txtETD.Focus();
                return;
            }
            if (txtATD.Text == "" || txtATD.Text == "0:00")
            {
                ShowMessage("ATD should be greater than zero."); 
                txtATD.Focus(); 
                return;
            }
            if (txtTAThrs.Text == "" || txtTAThrs.Text == "0:00")
            {
                ShowMessage("TAT In Hrs should be greater than zero.");
                txtTAThrs.Focus();
                return;
            }
            if (Txtdelayhrs.Text == "" || Txtdelayhrs.Text == "0:00")
            {
                ShowMessage("Delay In Hrs should be greater than zero."); 
                Txtdelayhrs.Focus();
                return;
            }
            if (Hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["Id"]) == Convert.ToString(Hidrowid.Value))
                    {
                        dtrow["FromCity"] = DdlFromCty.SelectedItem.Text;
                        dtrow["ToCity"] = DdlToCty.SelectedItem.Text;
                        dtrow["Leg"] = string.IsNullOrEmpty(txtLeg.Text.Trim()) ? "0" : (txtLeg.Text.Trim());
                        dtrow["ETA"] = string.IsNullOrEmpty(txtETA.Text.Trim()) ? "0" : (txtETA.Text.Trim());
                        dtrow["ATA"] = string.IsNullOrEmpty(txtATA.Text.Trim()) ? "0" : (txtATA.Text.Trim());
                        dtrow["ETD"] = string.IsNullOrEmpty(txtETD.Text.Trim()) ? "0" : (txtETD.Text.Trim());
                        dtrow["ATD"] = string.IsNullOrEmpty(txtATD.Text.Trim()) ? "0" : (txtATD.Text.Trim());
                        dtrow["TAT_in_hrs"] = string.IsNullOrEmpty(txtTAThrs.Text.Trim()) ? "0" : (txtTAThrs.Text.Trim());
                        dtrow["Delay_in_hrs"] = string.IsNullOrEmpty(Txtdelayhrs.Text.Trim()) ? "0" : (Txtdelayhrs.Text.Trim());
                        dtrow["Remarks"] = txtremark.Text;
                        dtrow["FromCityIdno"] = string.IsNullOrEmpty(DdlFromCty.SelectedValue.ToString()) ? "0" : (DdlFromCty.SelectedValue.ToString());
                        dtrow["ToCityIdno"] = string.IsNullOrEmpty(DdlToCty.SelectedValue.ToString()) ? "0" : (DdlToCty.SelectedValue.ToString());
                    }
                }
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp == null)
                {
                    dtTemp = CreateDt();
                    ViewState["dt"] = dtTemp;
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["Id"])) + 1;
                string strFromCity = DdlFromCty.SelectedItem.Text;
                string strToCity = DdlToCty.SelectedItem.Text;
                string strLeg = string.IsNullOrEmpty(txtLeg.Text.Trim()) ? "0" : (txtLeg.Text.Trim());
                string strETA = string.IsNullOrEmpty(txtETA.Text.Trim()) ? "0" : (txtETA.Text.Trim());
                string strATA = string.IsNullOrEmpty(txtATA.Text.Trim()) ? "0" : (txtATA.Text.Trim());
                string strETD = string.IsNullOrEmpty(txtETD.Text.Trim()) ? "0" : (txtETD.Text.Trim());
                string strATD = string.IsNullOrEmpty(txtATD.Text.Trim()) ? "0" : (txtATD.Text.Trim());
                string strTAThrs = string.IsNullOrEmpty(txtTAThrs.Text.Trim()) ? "0" : (txtTAThrs.Text.Trim());
                string strdelayhrs = string.IsNullOrEmpty(Txtdelayhrs.Text.Trim()) ? "0" : (Txtdelayhrs.Text.Trim());
                string strReamrks = txtremark.Text.Trim();
                string strFromCityIdno = DdlFromCty.SelectedValue;
                string strToCityIdno = DdlToCty.SelectedValue;
                ApplicationFunction.DatatableAddRow(dtTemp, id, strFromCity, strToCity, strLeg, strETA, strATA, strETD, strATD, strTAThrs, strdelayhrs, strReamrks, strFromCityIdno, strToCityIdno);
                ViewState["dt"] = dtTemp;
            }
            this.BindGridT();
            ClearItems();
            BindCityDropDown();
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            this.Clear();
            this.ClearItems();
            DdlVehicleNo.Focus();
        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToString(Hidrowid.Value) == "")
            {   
                this.ClearItems();
                ViewState["dt"] = dtTemp = null;
                this.BindGridT();
            }
            else
            {
                this.ClearItems();
                this.BindCityDropDown();
            }
            DdlFromCty.Focus();
        }
        #endregion

        #region Functions
        private void Populate(Int64 HeadId)
        {
            TrackingDAL obj = new TrackingDAL();
            TrackingHead trackinghead = obj.selectHead(HeadId);
            TrackingDetl trackingdetl = obj.selectDetel(HeadId);
            DdlVehicleNo.SelectedValue = Convert.ToString(trackinghead.Vehicle_No);
            txtDate.Text = Convert.ToDateTime(trackinghead.Tracking_Date).ToString("dd-MM-yyyy");
            DdlLane.SelectedValue = Convert.ToString(trackinghead.Lane_Id);
            DdlLane_SelectedIndexChanged(null, null);
            DdlFromCity.SelectedValue = Convert.ToString(trackinghead.From_CityIdno);
            DdlFromCity_SelectedIndexChanged(null, null);
            DdlToCity.SelectedValue = Convert.ToString(trackinghead.To_CityIdno);
            DdlToCity_SelectedIndexChanged(null, null);
            DdlCompName.SelectedValue = Convert.ToString(trackinghead.Comp_Id);
            DdlFromLoc.SelectedValue = Convert.ToString(trackinghead.From_Loc);
            dtTemp = obj.selectDetls(ApplicationFunction.ConnectionString(), HeadId);
            ViewState["dt"] = dtTemp;
            this.BindGridT();
            lnkbtnSave.Enabled = true;
            obj = null;
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void BindDropdown()
        {
            TrackingDAL obj = new TrackingDAL();
            var LaneList = obj.BindLaneName();
            var ToCity = obj.BindAllToCity();
            var ToTruck = obj.BindLorry();
            var ToComp = obj.BindCompany();
            var ToFromLoc = obj.BindFromLoc();
            obj = null;

            DdlLane.DataSource = LaneList;
            DdlLane.DataTextField = "Lane_Name";
            DdlLane.DataValueField = "Lane_Idno";
            DdlLane.DataBind();
            DdlLane.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlFromCity.DataSource = ToCity;
            DdlFromCity.DataTextField = "city_name";
            DdlFromCity.DataValueField = "city_idno";
            DdlFromCity.DataBind();
            DdlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlToCity.DataSource = ToCity;
            DdlToCity.DataTextField = "city_name";
            DdlToCity.DataValueField = "city_idno";
            DdlToCity.DataBind();
            DdlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlVehicleNo.DataSource = ToTruck;
            DdlVehicleNo.DataTextField = "Lorry_No";
            DdlVehicleNo.DataValueField = "Lorry_Idno";
            DdlVehicleNo.DataBind();
            DdlVehicleNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlCompName.DataSource = ToComp;
            DdlCompName.DataTextField = "Comp_Name";
            DdlCompName.DataValueField = "Comp_Idno";
            DdlCompName.DataBind();
            DdlCompName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlFromLoc.DataSource = ToFromLoc;
            DdlFromLoc.DataTextField = "City_Name";
            DdlFromLoc.DataValueField = "City_Idno";
            DdlFromLoc.DataBind();
            DdlFromLoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id",  "String",
                "FromCity", "String",
                "ToCity" ,"String",
                "Leg", "String",
                "ETA", "String",
                "ATA", "String",
                "ETD", "String",
                "ATD", "String",
                "TAT_in_hrs" , "String",
                "Delay_in_hrs", "String",
                "Remarks", "String",
                "FromCityIdno","String",
                "ToCityIdno", "String"
                );
            return dttemp;
        }
        private void ClearItems()
        {
            Hidrowid.Value = "";
            DdlFromCty.SelectedIndex = 0;
            DdlToCty.SelectedIndex = 0;
            txtLeg.Text = "";
            txtETA.Text = "0:00";
            txtATA.Text = "0:00";
            txtETD.Text = "0:00";
            txtATD.Text = "0:00";
            txtTAThrs.Text = "0:00";
            Txtdelayhrs.Text = "0:00";
            txtremark.Text = "";
        }
        private void BindCityDropDown()
        {
            TrackingDAL obj = new TrackingDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;

            DdlFromCty.DataSource = ToCity;
            DdlFromCty.DataTextField = "city_name";
            DdlFromCty.DataValueField = "city_idno";
            DdlFromCty.DataBind();
            DdlFromCty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            DdlToCty.DataSource = ToCity;
            DdlToCty.DataTextField = "city_name";
            DdlToCty.DataValueField = "city_idno";
            DdlToCty.DataBind();
            DdlToCty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
                else
                {
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }
        private void Clear()
        {
            ViewState["dt"] = null;
            dtTemp = null;
            Hidrowid.Value = string.Empty;
            DdlVehicleNo.SelectedValue = "0";
            DdlLane.SelectedValue = "0";
            DdlFromCity.SelectedValue = "0";
            DdlToCity.SelectedValue = "0";
            DdlCompName.SelectedValue = "0";
            DdlFromLoc.SelectedValue = "0";
            BindGridT();
            lnkbtnSave.Enabled = true;
        }
        private Boolean ValidationEstNoSave(Int64 Idno)
        {
            //TrackingDAL objTrackingDAL = new TrackingDAL();
            //string vehno = Convert.ToString(txtvehicle.Text.Trim());
            //string compname = Convert.ToString(txtcompname.Text.Trim());
           // DataTable dtSave = objTrackingDAL.ValidateEstNo1(vehno, compname, Idno, ApplicationFunction.ConnectionString());
            //if (dtSave.Rows.Count > 0)
            //{
            //    ShowMessage("Invalid Tracking Details");
            //    return false;
            //}
            //else
            //{
                return true;
            //}
        }
        #endregion

        #region gridcommand
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    DdlFromCty.SelectedItem.Text = Convert.ToString(drs[0]["FromCity"]);
                    DdlToCty.SelectedItem.Text = Convert.ToString(drs[0]["ToCity"]); 
                    txtLeg.Text = Convert.ToString(drs[0]["Leg"]);
                    txtETA.Text = Convert.ToString(drs[0]["ETA"]);
                    txtATA.Text = Convert.ToString(drs[0]["ATA"]);
                    txtETD.Text = Convert.ToString(drs[0]["ETD"]);
                    txtATD.Text = Convert.ToString(drs[0]["ATD"]);
                    txtTAThrs.Text = Convert.ToString(drs[0]["TAT_in_hrs"]);
                    Txtdelayhrs.Text = Convert.ToString(drs[0]["Delay_in_hrs"]);
                    txtremark.Text = Convert.ToString(drs[0]["Remarks"]);
                    Hidrowid.Value = Convert.ToString(drs[0]["id"]);
                    DdlFromCty.SelectedValue = Convert.ToString(drs[0]["FromCityIdno"]);
                    DdlToCty.SelectedValue = Convert.ToString(drs[0]["ToCityIdno"]);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["FromCity"], rw["ToCity"], rw["Leg"], rw["ETA"], rw["ATA"], rw["ETD"], rw["ATD"], rw["TAT_in_hrs"], rw["Delay_in_hrs"], rw["Remarks"], rw["FromCityIdno"], rw["ToCityIdno"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
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
        protected void DdlLane_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DdlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DdlToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DdlFromCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlFromCty.SelectedValue != "0")
            {
                hidDdlFrom.Value = DdlFromCty.SelectedValue;
            }
            else
            {
                hidDdlFrom.Value = "0";
            }
        }
        protected void DdlToCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlToCity.SelectedValue != "0")
            {
                hidtocity.Value = DdlToCity.SelectedValue;
            }
            else
            {
                hidtocity.Value = "0";
            }
        }
        #endregion
    }
}
