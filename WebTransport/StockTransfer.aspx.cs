using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class StockTransfer : Pagebase
    {
        #region variable Declarations...
        static FinYearA UFinYear = new FinYearA();
        string strMsg = string.Empty;
        int iFinYrID;
        private int intFormId = 26;

        double totalQty = 0; double GrossAmnt = 0; double TotWeight = 0;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;
        DataTable DtTemp = new DataTable();
        double dblTtAmnt = 0;
        int rb = 0;

        string strSQL = ""; bool isTBBRate = false, btruckcitywise = false;
        double dSurchgPer = 0, dWagsAmnt = 0, dBiltyAmnt = 0, dTolltax = 0; Int32 iFromCity = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dtotalAmount = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;
        #endregion

        #region Page_Load
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
                //if (base.ADD == false)
                //{
                //    lnkbtnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}

                txtDate.Text = ApplicationFunction.GetIndianDateTime().Date.ToString("dd-MM-yyyy");
                this.BindCity();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    BindDropdownDAL obj = new BindDropdownDAL();
                    var lst = obj.BindLocFrom();
                        drpBaseCity.DataSource = lst;
                        drpBaseCity.DataTextField = "City_Name";
                        drpBaseCity.DataValueField = "City_Idno";
                        drpBaseCity.DataBind();
                        drpBaseCity.Items.Insert(0, new ListItem("--Select--", "0"));

                        drpDeliveryPlace.DataSource = lst;
                        drpDeliveryPlace.DataTextField = "City_Name";
                        drpDeliveryPlace.DataValueField = "City_Idno";
                        drpDeliveryPlace.DataBind();
                        drpDeliveryPlace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);

                this.BindDateRange(); this.BindItemType();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlDateRange.SelectedIndex = 0;
                StockTransferDAL objstck = new StockTransferDAL();
                if (drpBaseCity.SelectedIndex > 0)
                {
                    txtIssueNo.Text = Convert.ToString(objstck.SelectMaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue)));
                }
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidStckid.Value = Convert.ToString(Request.QueryString["q"]);
                    //btnNew.Visible = true;
                    imgPrint.Visible = true;
                    lnkbtnAdd.Visible = true;
                    ddlDateRange.Enabled = false;
                }

                else
                {
                    lnkbtnAdd.Visible = false;
                    ddlDateRange.Enabled = true;
                    imgPrint.Visible = false;
                }
                txtQty.Text = "1";
                txtRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtQty.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRemark.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
                txtNetAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

            }

        }
        #endregion

        #region Button
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp == null || DtTemp.Rows.Count <= 0)
            {
                ShowMessage("Please enter details");
                drpItemType.Focus();
                return;
            }
            if (Convert.ToInt32(drpBaseCity.SelectedValue) == Convert.ToInt32(drpDeliveryPlace.SelectedValue))
            {
                 //ShowMessage("Issuing Location and Receiving Location can't be same"); 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Comparecity()", true);
                drpDeliveryPlace.Focus(); return; 
            }
            if (drpBaseCity.SelectedIndex == 0) { this.ShowMessage("Please select Issuing Location"); drpBaseCity.Focus(); return; }
            if (drpDeliveryPlace.SelectedIndex == 0) { this.ShowMessage("Please select Receiving Location"); drpDeliveryPlace.Focus(); return; }

            //StockTransferDAL objstck = new StockTransferDAL();
            tblStckTrans_Head objtblStck = new tblStckTrans_Head();

            objtblStck.StckTrans_No = Convert.ToInt32(txtIssueNo.Text.Trim());
            objtblStck.StckTrans_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim()));
            objtblStck.IssLoc_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
            objtblStck.RecLoc_Idno = Convert.ToInt32(drpDeliveryPlace.SelectedValue);
            objtblStck.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
            objtblStck.Remark = txtRemark.Text.Trim();
            objtblStck.User_Idno = Convert.ToInt32(Session["UserIdno"]);
            objtblStck.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text.Trim());
            objtblStck.Date_Created = DateTime.Now;
            objtblStck.Date_Modified = DateTime.Now;


            List<tblStckTrans_Detl> objstckTrDetl = new List<tblStckTrans_Detl>();
            if (DtTemp != null)
            {
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    tblStckTrans_Detl objDetl = new tblStckTrans_Detl();
                    objDetl.ItemType_Idno = Convert.ToInt64(dtrow["ITEM_TYPEID"]);
                    objDetl.SerialNo_Idno = Convert.ToInt64(Convert.ToString(dtrow["ITEM_SERIALID"]) == "" ? "0" : dtrow["ITEM_SERIALID"]);
                    objDetl.Item_Serial_No = Convert.ToString(dtrow["ITEM_SERIAL"]);
                    objDetl.TyreType_Idno = Convert.ToInt64(Convert.ToString(dtrow["TYRE_TYPEID"]) == "" ? "0" : dtrow["TYRE_TYPEID"]);
                    objDetl.Item_Idno = Convert.ToInt64(Convert.ToString(dtrow["ITEM_ID"]) == "" ? "0" : dtrow["ITEM_ID"]);
                    objDetl.Qty = Convert.ToDouble(dtrow["ITEM_QTY"]);
                    objDetl.Rate= Convert.ToDouble(dtrow["ITEM_RATE"]);
                    objDetl.Tot_Amnt = Convert.ToDouble(dtrow["ITEM_QTY"]) * Convert.ToDouble(dtrow["ITEM_RATE"]);
                    objstckTrDetl.Add(objDetl);
                }
            }
            else
            {
                DtTemp = CreateDt();

            }
            if (objstckTrDetl.Count <= 0)
            {
                ShowMessage("Please enter details");
                return;
            }
            Int64 InsertId = 0;
            Int64 UpdateId = 0;
            StockTransferDAL obj = new StockTransferDAL();
            if (Convert.ToInt32(hidStckid.Value) > 0)
            {
                objtblStck.StckTrans_Idno = Convert.ToInt32(hidStckid.Value);
                UpdateId = obj.Update(objtblStck, objstckTrDetl);
            }
            else
            {
                InsertId = obj.Insert(objtblStck, objstckTrDetl);
            }

            obj = null;
            if (InsertId > 0)
            {
                strMsg = "Record save successfully";
            }
            else if (UpdateId > 0)
            {
                strMsg = "Record Update successfully";
            }
            else if (InsertId < 0)
            {
                strMsg = "Receipt No already exists";
            }
            else if (UpdateId < 0)
            {
                strMsg = "Record not Update successfully";
            }
            else
            {
                strMsg = "Record not saved successfully";
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            //
            ShowMessage(strMsg);
            this.ClearAll();
        }

        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["q"]));
            }
            else
            {
                this.ClearAll();
            }
        }

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (drpItemType.SelectedIndex == 0)
            {
                this.ShowMessage("Please select Item Type");
                drpItemType.Focus();
                return;
            }
            if (drpItemType.SelectedIndex == 1)
            {
                if (ddlTyreType.SelectedIndex == 0)
                {
                    this.ShowMessage("Please Select Tyre Type");
                    ddlTyreType.Focus();
                    return;
                }
                if (drpSerialNo.SelectedIndex == 0)
                {
                    this.ShowMessage("Please Select Serial No or Part No.");
                    drpSerialNo.Focus();
                    return;
                }
                if (txtQty.Text == string.Empty || Convert.ToDouble(txtQty.Text) <= 0)
                {
                    this.ShowMessage("Qty must be greater than 0.");
                    txtQty.Focus();
                    return;
                }
                if (txtRate.Text == string.Empty || Convert.ToDouble(txtRate.Text) <= 0)
                {
                    this.ShowMessage("Rate must be greater than 0.");
                    txtRate.Focus();
                    return;
                }
                rfvSerialNo.Enabled = true;
            }
            else
            {

              
                if (txtQty.Text == string.Empty || Convert.ToDouble(txtQty.Text) <= 0)
                {
                    this.ShowMessage("Qty must be greater than 0.");
                    txtQty.Focus();
                    return;
                }
                if (txtRate.Text == string.Empty || Convert.ToDouble(txtRate.Text) <= 0)
                {
                    this.ShowMessage("Rate must be greater than 0.");
                    txtRate.Focus();
                    return;
                }
                rfvSerialNo.Enabled = false;
            }
            
            
            if (hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp != null && DtTemp.Rows.Count > 0)
                {

                    foreach (DataRow dtrow in DtTemp.Rows)
                    {

                        if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                        {
                            dtrow["ITEM_TYPE"] = drpItemType.SelectedItem.Text;
                            dtrow["ITEM_TYPEID"] = drpItemType.SelectedValue;
                            if (Convert.ToInt32(dtrow["ITEM_TYPEID"]) == 1)
                            {
                                dtrow["ITEM_SERIAL"] = string.IsNullOrEmpty(drpSerialNo.SelectedItem.Text) ? "" : Convert.ToString(drpSerialNo.SelectedItem.Text);
                                dtrow["TYRE_TYPE"] = ddlTyreType.SelectedItem.Text;
                            }
                            else
                            {
                                dtrow["ITEM_SERIAL"] = "";
                                dtrow["TYRE_TYPE"] = "";
                            }

                            dtrow["ITEM_SERIALID"] = string.IsNullOrEmpty(drpSerialNo.SelectedValue) ? 0 : Convert.ToInt32(drpSerialNo.SelectedValue);
                            dtrow["TYRE_TYPEID"] = ddlTyreType.SelectedValue;
                            dtrow["ITEM_QTY"] = txtQty.Text;
                            dtrow["ITEM_RATE"] = txtRate.Text;
                            dtrow["ITEM_AMOUNT"] = Convert.ToString(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text));
                            dtrow["ITEM_NAME"] = drpItemName.SelectedItem.Text;
                            dtrow["ITEM_ID"] = drpItemName.SelectedValue;
                            // dtrow["UOM_ID"] = hiduomid.Value;
                        }
                    }
                }
            }
            else
            {
                DtTemp = (DataTable)ViewState["dt"];
                if ((DtTemp != null) && (DtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in DtTemp.Rows)
                    {
                        if (Convert.ToInt32(row["ITEM_TYPEID"]) == 1)
                        {
                            if (Convert.ToInt32(row["ITEM_SERIALID"]) == Convert.ToInt32(string.IsNullOrEmpty(drpSerialNo.SelectedValue) ? 0 : Convert.ToInt32(drpSerialNo.SelectedValue)) && (Convert.ToInt32(row["ITEM_TYPEID"]) == Convert.ToInt32(drpItemType.SelectedValue)))
                            {
                                msg = "Item Already Selected!";
                                drpSerialNo.Focus(); drpSerialNo.SelectedIndex = 0;
                                this.ShowMessage(msg);
                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(row["ITEM_ID"]) == Convert.ToInt32(string.IsNullOrEmpty(drpItemName.SelectedValue) ? 0 : Convert.ToInt32(drpItemName.SelectedValue)) && (Convert.ToInt32(row["ITEM_TYPEID"]) == Convert.ToInt32(drpItemType.SelectedValue)))
                            {
                                msg = "Item Already Selected!";
                                drpItemName.Focus(); drpItemName.SelectedIndex = 0;
                                this.ShowMessage(msg);
                                return;
                            }
                        }
                    }
                }
                else
                { DtTemp = CreateDt(); }
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                if (drpItemType.SelectedValue == "1")
                {
                    ApplicationFunction.DatatableAddRow(DtTemp, id, drpItemType.SelectedItem.Text, drpItemType.SelectedValue, drpSerialNo.SelectedItem.Text,
                        drpSerialNo.SelectedValue, ddlTyreType.SelectedItem.Text, ddlTyreType.SelectedValue, txtQty.Text, txtRate.Text, Convert.ToString(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)),drpItemName.SelectedItem.Text, drpItemName.SelectedValue);
                }
                else
                {
                    ApplicationFunction.DatatableAddRow(DtTemp, id, drpItemType.SelectedItem.Text, drpItemType.SelectedValue, "",
                    "", "", "", txtQty.Text, txtRate.Text, Convert.ToString(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)), string.IsNullOrEmpty(drpItemName.SelectedValue) ? "" : Convert.ToString(drpItemName.SelectedItem.Text), string.IsNullOrEmpty(drpItemName.SelectedValue) ? "" : Convert.ToString(drpItemName.SelectedValue));
                }
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            this.ClearItems();
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            this.ClearItems();
        }

        #endregion

        #region GridEvents...

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }


        
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            StockTransferDAL objStck = new StockTransferDAL();
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            //GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                DtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = DtTemp.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {
                    drpItemType.SelectedValue = Convert.ToString(drs[0]["ITEM_TYPEID"]);
                    if (Convert.ToInt32(hidStckid.Value) > 0)
                    {
                        BindItemName();
                        
                            if ((drpItemType.SelectedValue == "1"))
                            {
                                drpItemName.SelectedValue = string.IsNullOrEmpty(drpItemName.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_ID"]);
                                BindItem();
                                ddlTyreType.SelectedValue = string.IsNullOrEmpty(ddlTyreType.SelectedValue) ? "0" : Convert.ToString(drs[0]["TYRE_TYPEID"]);
                                ddlTyreType.Enabled = true; txtQty.Text = "1"; ddlTyreType.Enabled = true;
                                txtQty.Enabled = false; rfvddlTyreType.Enabled = true; drpSerialNo.Enabled = true; rfvSerialNo.Enabled = true;
                            }
                            else
                            {
                                drpItemName.SelectedValue = string.IsNullOrEmpty(drpItemName.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_ID"]);
                                ddlTyreType.Enabled = false; txtQty.Enabled = true; rfvddlTyreType.Enabled = false; txtQty.Text = "1";
                                drpSerialNo.Enabled = false; rfvSerialNo.Enabled = false; ddlTyreType.Enabled = false;
                            }
                    }
                    else
                    {
                        if (drpItemType.SelectedValue == "1")
                        {
                            BindItemName();
                            drpItemName.SelectedValue = string.IsNullOrEmpty(drpItemName.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_ID"]);
                            BindItem();
                            drpSerialNo.SelectedValue = string.IsNullOrEmpty(drpSerialNo.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_SERIALID"]);
                            ddlTyreType.SelectedValue = Convert.ToString(drs[0]["TYRE_TYPEID"]);
                            ddlTyreType.Enabled = true; txtQty.Text = "1"; ddlTyreType.Enabled = true;
                            txtQty.Enabled = false; rfvddlTyreType.Enabled = true; drpSerialNo.Enabled = true; rfvSerialNo.Enabled = true;
                        }
                        else
                        {
                            BindItemName();
                            drpItemName.SelectedValue = string.IsNullOrEmpty(drpItemName.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_ID"]);
                            //drpSerialNo.SelectedValue = string.IsNullOrEmpty(drpSerialNo.SelectedValue) ? "0" : Convert.ToString(drs[0]["ITEM_SERIALID"]);
                            ddlTyreType.Enabled = false; txtQty.Enabled = true; rfvddlTyreType.Enabled = false; txtQty.Text = "1";
                            drpSerialNo.Enabled = false; rfvSerialNo.Enabled = false; ddlTyreType.Enabled = false;
                        }
                    }
                    txtQty.Text = Convert.ToString(drs[0]["ITEM_QTY"]);
                    txtRate.Text = Convert.ToString(drs[0]["ITEM_RATE"]);
                    
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
                }
                drpItemType.Focus();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable dt = CreateDt();
                foreach (DataRow rw in DtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {

                        ApplicationFunction.DatatableAddRow(dt, rw["id"], rw["ITEM_TYPE"], rw["ITEM_TYPEID"], rw["ITEM_SERIAL"],
                                                                rw["ITEM_SERIALID"], rw["TYRE_TYPE"], rw["TYRE_TYPEID"], rw["ITEM_QTY"], rw["ITEM_RATE"], rw["ITEM_AMOUNT"], rw["ITEM_NAME"], rw["ITEM_ID"]);

                    }
                }
                ViewState["dt"] = dt;
                dt.Dispose();

                this.BindGrid();
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double qty = 0; double gross = 0;
                string billed = string.Empty;
                qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ITEM_QTY"));
                totalQty += qty;
                gross = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ITEM_AMOUNT"));
                GrossAmnt += gross;
               

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblqtytotal = (Label)e.Row.FindControl("lblqtytotal");
                lblqtytotal.Text = Convert.ToDouble((totalQty)).ToString("N2");
                Label lblItemTotal = (Label)e.Row.FindControl("lblItemTotal");
                lblItemTotal.Text = "Total";
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDouble((GrossAmnt)).ToString("N2");
                txtNetAmnt.Text = GrossAmnt.ToString("N2");

            }
        }
        #endregion

        #region Functions..
        private void ClearAll()
        {
            drpBaseCity.SelectedValue = "0";
            drpDeliveryPlace.SelectedValue = "0";
            txtNetAmnt.Text = "0.00";
            txtRemark.Text = "";
            drpItemName.SelectedValue = "0";
            ddlTyreType.SelectedValue = "0";
            txtAmount.Text = "0.00";
            ViewState["dt"] = DtTemp = null;
            grdMain.DataSource = null;
            grdMain.DataBind();
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "cityidno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            drpDeliveryPlace.DataSource = FrmCity;
            drpDeliveryPlace.DataTextField = "City_Name";
            drpDeliveryPlace.DataValueField = "City_Idno";
            drpDeliveryPlace.DataBind();
            drpDeliveryPlace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            ////BindDropdownDAL obj = new BindDropdownDAL();
            //var lst = obj.BindCityUserWise(UserIdno);
            //drpDeliveryPlace.DataSource = lst;
            //drpDeliveryPlace.DataTextField = "City_Name";
            //drpDeliveryPlace.DataValueField = "City_Idno";
            //drpDeliveryPlace.DataBind();
            //drpDeliveryPlace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindItemType()
        {
            StockTransferDAL obj = new StockTransferDAL();
            var itemname = obj.BindItemType();
            drpItemType.DataSource = itemname;
            drpItemType.DataTextField = "ItemType_Name";
            drpItemType.DataValueField = "ItemTpye_Idno";
            drpItemType.DataBind();
            drpItemType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void ClearItems()
        {
            drpItemType.SelectedValue = "0";
            drpItemType.Enabled = true;
            txtQty.Text = "0.00";
              if(drpItemName.SelectedValue !=string.Empty)
            drpItemName.SelectedValue = "0";

              drpItemName.Enabled = true;
              if (drpSerialNo.SelectedValue != string.Empty)
            drpSerialNo.SelectedValue = "0";

              drpSerialNo.Enabled = true;
              if (ddlTyreType.SelectedValue != string.Empty)
                  ddlTyreType.SelectedValue = "0";
              ddlTyreType.Enabled = true;
            txtAmount.Text = "0.00";
            hidrowid.Value = string.Empty;
            txtRate.Text = "0.00";
        }
        private void PrintStck(Int64 HeadId)
        {
            Repeater obj = new Repeater();

            double dgross = 0, dothr = 0, dround = 0, dnetmnt = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (TinNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = TinNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spStockTransfer] @ACTION='SelectPrint',@Id='" + HeadId + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StckTrans_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["StckTrans_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Rec_Place"]);
                //lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);

                //lblSenderName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);

                valuelblNetAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["NetAmnt"]));
                if (valuelblNetAmnt.Text != "")
                {
                    dnetmnt = Convert.ToDouble(valuelblNetAmnt.Text);
                }
                else
                {
                    dnetmnt = 0;
                }

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
                //valuelblnetAmnt.Text = string.Format("{0:0,0.00}", (dgross + dothr + dround + dPF + dnetmnt + dtax + dwges + dtoll + dtotlAmnt));
            }
        }
        private void Populate(Int64 HeadId)
        {
            StockTransferDAL obkstkHead = new StockTransferDAL();
            tblStckTrans_Head Head = obkstkHead.SelectByStckTransHeadId(HeadId);
            var QtnDetlList = obkstkHead.SelectStkTransDetailByHeadId(HeadId);
            obkstkHead = null;

            if (Head != null)
            {
                txtIssueNo.Text = Convert.ToString(Head.StckTrans_No);
                txtDate.Text = Convert.ToDateTime(Head.StckTrans_Date).ToString("dd-MM-yyyy");
                txtRemark.Text = Convert.ToString(Head.Remark);
                drpBaseCity.SelectedValue = Convert.ToString(Head.IssLoc_Idno);
                drpDeliveryPlace.SelectedValue = Convert.ToString(Head.RecLoc_Idno);
                txtNetAmnt.Text = Convert.ToDouble(Head.Net_Amnt).ToString("N2");
                DtTemp = CreateDt();
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ViewState["dt"] = DtTemp;
                foreach (var STD in QtnDetlList)
                {
                    ApplicationFunction.DatatableAddRow(DtTemp, id, Convert.ToString(DataBinder.Eval(STD, "ITypeName")),
                        Convert.ToString(DataBinder.Eval(STD, "ItemType_Idno")),
                        Convert.ToString(DataBinder.Eval(STD, "Serial_No")),
                        Convert.ToString(DataBinder.Eval(STD, "SerialNo_Idno")),
                        Convert.ToString(DataBinder.Eval(STD, "TyreType")),
                        Convert.ToString(DataBinder.Eval(STD, "TyreType_Idno")),
                        Convert.ToString(DataBinder.Eval(STD, "Qty")),
                        Convert.ToDouble(DataBinder.Eval(STD, "Rate")).ToString("N2"),
                        Convert.ToDouble(Convert.ToDouble(DataBinder.Eval(STD, "Qty")) * Convert.ToDouble(DataBinder.Eval(STD, "Rate"))),
                        Convert.ToString(DataBinder.Eval(STD, "Item_Name")),
                        Convert.ToString(DataBinder.Eval(STD, "Item_Idno"))
                        );

                    id++;
                }
                ViewState["dt"] = DtTemp;
            }
            this.BindGrid();
            PrintStck(HeadId);
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
                    txtDate.Text = hidmindate.Value;
                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtDate.Text = hidmindate.Value;

                }
            }

        }
 

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "ITEM_TYPE", "String", "ITEM_TYPEID", "String", "ITEM_SERIAL", "String", "ITEM_SERIALID", "String", "TYRE_TYPE", "String", "TYRE_TYPEID", "String",
                                                                   "ITEM_QTY", "String", "ITEM_RATE", "String", "ITEM_AMOUNT", "String", "ITEM_NAME", "String", "ITEM_ID", "String");//ITEM_AMOUNT
            return dttemp;
        }

        private void BindGrid()
        {

            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }



        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", "PassMessageError('" + msg + "')", true);
        }


        #endregion

        #region Controls Events...

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if ((txtRate.Text != "") && (txtQty.Text != ""))
            {
                txtAmount.Text = Convert.ToString(Convert.ToDouble(Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text)).ToString("N2"));
            }
            txtRate.Focus();
        }



        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            if ((txtRate.Text != "") && (txtQty.Text != ""))
            {
                txtAmount.Text = Convert.ToString(Convert.ToDouble(Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text)).ToString("N2"));
            }
            lnkbtnSubmit.Focus();
        }


     

        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpBaseCity.SelectedIndex > 0)
            {
                StockTransferDAL objstck = new StockTransferDAL();
                txtIssueNo.Text = Convert.ToString(objstck.SelectMaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue)));
                DtTemp = null;
                grdMain.DataSource = null;
                ViewState["dt"] = DtTemp = null;
                grdMain.DataSource = null;
                grdMain.DataBind(); 
            }

            drpBaseCity.Focus();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();

            }
        }

        #endregion

        public void BindItem()
        {
            StockTransferDAL objStck = new StockTransferDAL();
            var Serial = objStck.BindItemSerial(Convert.ToInt64(drpItemType.SelectedValue), Convert.ToInt64(drpBaseCity.SelectedValue),Convert.ToInt64(drpItemName.SelectedValue));
            objStck = null;
            drpSerialNo.DataSource = Serial;
            drpSerialNo.DataTextField = "SerialNo";
            drpSerialNo.DataValueField = "SerlDetl_id";
            drpSerialNo.DataBind();
            drpSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        protected void drpItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpItemType.SelectedIndex == 0) { ShowMessageErr("Please select Item Type."); drpItemType.Focus(); drpItemType.SelectedValue = "0"; return; }
            if (drpBaseCity.SelectedIndex == 0) { ShowMessageErr("Please select Location From."); drpBaseCity.Focus(); drpItemType.SelectedValue = "0"; return; }

            StockTransferDAL objStck = new StockTransferDAL();
            if (drpItemType.SelectedValue == "1")
            {

                
                ddlTyreType.Enabled = true; txtQty.Text = "1";
                txtQty.Enabled = false; rfvddlTyreType.Enabled = true; drpSerialNo.Enabled = true; rfvSerialNo.Enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(drpSerialNo.SelectedValue))
                {}
                else
                { drpSerialNo.SelectedValue = "0"; }
                ddlTyreType.Enabled = false; txtQty.Enabled = true; rfvddlTyreType.Enabled = false; txtQty.Text = "1"; drpSerialNo.Enabled = false; rfvSerialNo.Enabled = false;
            }
            BindItemName();
           
        }
        public void BindItemName()
        {
            StockTransferDAL objStck = new StockTransferDAL();
            var ItemName = objStck.BindItemName(Convert.ToInt64(drpItemType.SelectedValue));
            drpItemName.DataSource = ItemName;
            drpItemName.DataTextField = "Item_Name";
            drpItemName.DataValueField = "Item_Idno";
            drpItemName.DataBind();
            drpItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            objStck = null;
        }

        protected void drpSerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSerialNo.SelectedIndex == 0) { ShowMessageErr("Please select Serial No."); drpSerialNo.Focus(); return; }
            StockTransferDAL objStck = new StockTransferDAL();
            double Rate = objStck.BindItemRate(Convert.ToInt64(drpSerialNo.SelectedValue));
            txtRate.Text = Rate.ToString("N2");
            if (Convert.ToDouble(txtRate.Text) > 0)
            {
                txtAmount.Text = Convert.ToString(Convert.ToDouble(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)).ToString("N2"));
            }
        }

        protected void drpDeliveryPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(drpBaseCity.SelectedValue) == Convert.ToInt32(drpDeliveryPlace.SelectedValue))
            {
                if (drpBaseCity.SelectedIndex == 0) { ShowMessage("Issuing Location and Receiving Location can't be same"); drpDeliveryPlace.Focus(); return; }
            }
        }

        protected void drpItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(drpItemType.SelectedValue) > 0)
            {
                if (drpItemType.SelectedValue == "1")
                {
                    BindItem();
                }
            }
        }
       
    }
}