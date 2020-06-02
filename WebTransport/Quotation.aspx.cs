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
    public partial class Quotation : Pagebase
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
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }

                txtQutNo.Visible = false;
                //btnNew.Visible = false;
                txtquotinDate.Text = ApplicationFunction.GetIndianDateTime().Date.ToString("dd-MM-yyyy");
               // this.BindCity();
                this.GetAllItems();
                this.BindSenderPopulate();
                this.GetAllUnit();
                drpUnitName.SelectedValue = Convert.ToString(base.Unit);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindAllCity();
                }
                else
                {
                    this.BindBaseCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                drpSender.SelectedValue = Convert.ToString(base.Sender);
                //drpItemName.SelectedValue = Convert.ToString(base.ItemName);
                //if (Convert.ToString(Session["Userclass"]) == "Admin")
                //{
                //    QuotationDAL obj = new QuotationDAL();
                //    var lst = obj.FetchSenderPopulate();
                //    obj = null;
                //    drpSender.DataSource = lst;
                //    drpSender.DataTextField = "Acnt_Name";
                //    drpSender.DataValueField = "Acnt_Idno";
                //    drpSender.DataBind();
                //    drpSender.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    this.BindSenderPopulate();
                //}
                

                //if (Convert.ToString(Session["Userclass"]) == "Admin")
                //{
                //    ItemMasterDAL obj = new ItemMasterDAL();
                //    var lst = obj.GetItems();
                //    obj = null;
                //    drpItemName.DataSource = lst;
                //    drpItemName.DataTextField = "ItemName";
                //    drpItemName.DataValueField = "ItemId";
                //    drpItemName.DataBind();
                //    drpItemName.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    this.GetAllItems();
                //}
                

               
                //if (Convert.ToString(Session["Userclass"]) == "Admin")
                //{
                //    UOMMasterDAL obj = new UOMMasterDAL();
                //    var lst = obj.GetUnit();
                //    obj = null;
                //    drpUnitName.DataSource = lst;
                //    drpUnitName.DataTextField = "UOMName";
                //    drpUnitName.DataValueField = "UOMId";
                //    drpUnitName.DataBind();
                //    drpUnitName.Items.Insert(0, new ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    this.GetAllUnit();
                //}
                

                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                this.BindRateType();
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                this.GetQutionNo(Convert.ToInt32((drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue)), Convert.ToInt32(intYearIdno));
                drpToCity.Enabled = true;
                drpBaseCity.Enabled = true; ;
                ddlType.Enabled = true;
                
                QuotationDAL objQuotationDAL = new QuotationDAL();
                tblUserPref objtblUserPref = objQuotationDAL.SelectUserPref();
                //drpBaseCity.SelectedValue = Convert.ToString(objtblUserPref.BaseCity_Idno);
                HidTbbType.Value = Convert.ToString(objtblUserPref.TBB_Rate);
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlDateRange.SelectedIndex = 0;
                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidquotationid.Value = Convert.ToString(Request.QueryString["q"]);
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
                    //this.BindSender();
                }
                txtQty.Text = "1";
                txtWeight.Text = "1";
                txtRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtQty.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtWeight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtquotinDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRemark.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");
                txtGrossAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtOtherAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtNetAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

                //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
                //RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");
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
                drpItemName.Focus();
                return;
            }
            tblQuatationHead objQTH = new tblQuatationHead();

            objQTH.Date_Added = DateTime.Now;
            objQTH.Date_Modified = DateTime.Now;
            objQTH.DelvryPlce_Idno = Convert.ToInt32(drpDeliveryPlace.SelectedValue);
            objQTH.FromCity_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
            objQTH.QuHead_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtquotinDate.Text));
            objQTH.QuHead_No = Convert.ToInt32(txtQutNo.Text);
            objQTH.QuHead_Typ = Convert.ToInt32(ddlType.SelectedValue);
            objQTH.Remark = Convert.ToString(txtRemark.Text);
            objQTH.RndOff_Amnt = Convert.ToDouble((txtRoundOff.Text == "") ? "0" : txtRoundOff.Text);
            objQTH.Gross_Amnt = Convert.ToDouble((txtGrossAmnt.Text == "") ? "0" : txtGrossAmnt.Text);
            objQTH.Net_Amnt = Convert.ToDouble((txtNetAmnt.Text == "") ? "0" : txtNetAmnt.Text);
            objQTH.Other_Amnt = Convert.ToDouble((txtOtherAmnt.Text == "") ? "0" : txtOtherAmnt.Text);
            objQTH.Sender_Idno = Convert.ToInt32(drpSender.SelectedValue == "" ? 0 : Convert.ToInt32(drpSender.SelectedValue));
            objQTH.Status = true;
            objQTH.ToCity_Idno = Convert.ToInt32(drpToCity.SelectedValue);
            objQTH.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
            objQTH.TBB_Rate = Convert.ToBoolean(HidTbbType.Value);
            objQTH.UserIdno = Convert.ToInt64(Session["UserIdno"]);
            List<tblQuatationDetl> RgDlst = new List<tblQuatationDetl>();

            if (DtTemp != null)
            {
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    tblQuatationDetl qtd = new tblQuatationDetl();
                    qtd.Item_Idno = Convert.ToInt64(dtrow["ITEM_IDNO"]);
                    qtd.Qty = Convert.ToDouble(dtrow["ITEM_QTY"]);
                    qtd.Tot_Weght = Convert.ToDouble(dtrow["ITEM_WEIGHT"]);
                    qtd.Unit_Idno = Convert.ToInt64(dtrow["UOM_IDNO"]);
                    qtd.Rate_Type = Convert.ToInt32(dtrow["RATE_IDNO"]);
                    qtd.Item_Rate = Convert.ToDouble(dtrow["ITEM_RATE"]);
                    qtd.Amount = Convert.ToDouble(dtrow["ITEM_AMOUNT"]);
                    RgDlst.Add(qtd);
                }
            }
            else
            {
                DtTemp = CreateDt();

            }
            if (RgDlst.Count <= 0)
            {
                ShowMessage("Please enter details");
                return;
            }
            Int64 QtnHeadId = 0;
            Int64 QtnHeaId = 0;
            QuotationDAL obj = new QuotationDAL();
            if (Convert.ToInt32(hidquotationid.Value) > 0)
            {
                objQTH.QuHead_Idno = Convert.ToInt64(hidquotationid.Value);
                QtnHeaId = obj.Update(objQTH, RgDlst);
            }
            else
            {
                QtnHeadId = obj.Insert(objQTH, RgDlst);
                // this.ClearAll();
            }

            obj = null;
            if (QtnHeadId > 0)
            {
                strMsg = "Record save successfully";
            }
            else if (QtnHeaId > 0)
            {
                strMsg = "Record Update successfully";
            }
            else if (QtnHeadId < 0)
            {
                strMsg = "Receipt No already exists";
            }
            else if (QtnHeaId < 0)
            {
                strMsg = "Record not Update successfully";
            }
            else
            {
                strMsg = "Record not saved successfully";
            }

            this.ClearAll();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            drpToCity.Enabled = true;
            drpBaseCity.Enabled = true;
            ddlType.Enabled = true;
            Response.Redirect("Quotation.aspx");
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
            if (ddlType.SelectedIndex == 0 || ddlType.SelectedIndex == 2)
            {
                if (ddlRateType.SelectedValue == "1")
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
                }
                else
                {

                    if (txtWeight.Text == string.Empty || Convert.ToDouble(txtWeight.Text) <= 0)
                    {
                        this.ShowMessage("Weight must be greater than 0!");
                        txtWeight.Focus();
                        return;
                    }
                    if (txtRate.Text == string.Empty || Convert.ToDouble(txtRate.Text) <= 0)
                    {
                        this.ShowMessage("Rate must be greater than 0!");
                        txtRate.Focus();
                        return;
                    }
                }
            }
            else
            {
                if (ddlRateType.SelectedValue == "1")
                {
                    if (txtQty.Text == string.Empty || Convert.ToDouble(txtQty.Text) <= 0)
                    {
                        this.ShowMessage("Qty must be greater than 0!");
                        txtQty.Focus();
                        return;
                    }
                    if (Convert.ToBoolean(HidTbbType.Value) == true)
                    {
                        if (txtRate.Text == string.Empty || Convert.ToDouble(txtRate.Text) <= 0)
                        {
                            this.ShowMessage("Rate must be greater than 0!");
                            txtRate.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    if (txtWeight.Text == string.Empty || Convert.ToDouble(txtWeight.Text) <= 0)
                    {
                        this.ShowMessage("Weight must be greater than 0!");
                        txtWeight.Focus();
                        return;
                    }
                    if (Convert.ToBoolean(HidTbbType.Value) == true)
                    {
                        if (txtRate.Text == string.Empty || Convert.ToDouble(txtRate.Text) <= 0)
                        {
                            this.ShowMessage("Rate must be greater than 0!");
                            txtRate.Focus();
                            return;
                        }
                    }
                }
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
                            //"id", "String", "ITEM_ID", "String", "ITEM_NAME", "String", "ITEM_QTY", "String", "ITEM_UOM", "String", 
                            //                                       "ITEM_WEIGHT", "String", "ITEM_REMARK", "String","UOM_ID","String"
                            //  dtrow["ITEM_ID"] = drpItemName.SelectedValue;
                            dtrow["ITEM_NAME"] = drpItemName.SelectedItem.Text;
                            dtrow["ITEM_QTY"] = txtQty.Text;
                            dtrow["ITEM_WEIGHT"] = txtWeight.Text;
                            dtrow["UOM_IDNO"] = drpUnitName.SelectedValue;
                            dtrow["UOM_NAME"] = drpUnitName.SelectedItem.Text;
                            dtrow["RATE_IDNO"] = ddlRateType.SelectedValue;
                            dtrow["RATE_TYPE"] = ddlRateType.SelectedItem.Text;
                            dtrow["ITEM_RATE"] = txtRate.Text;
                            dtrow["ITEM_AMOUNT"] = txtAmount.Text;
                            // dtrow["UOM_ID"] = hiduomid.Value;
                        }
                        //else if (drpItemName.SelectedIndex > 0 && drpUnitName.SelectedIndex > 0)
                        //{

                        //    if ((Convert.ToString(dtrow["ITEM_IDNO"]) == Convert.ToString(drpItemName.SelectedValue)) && (Convert.ToString(dtrow["UOM_IDNO"]) == Convert.ToString(drpUnitName.SelectedValue)))
                        //    {
                        //        msg = "Item Already Selected!";
                        //        drpItemName.Focus();
                        //        this.ShowMessage(msg);
                        //        return;
                        //    }

                        //}
                        {

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
                        if (Convert.ToInt32(row["ITEM_IDNO"]) == Convert.ToInt32(drpItemName.SelectedValue) && (Convert.ToInt32(row["UOM_IDNO"]) == Convert.ToInt32(drpUnitName.SelectedValue)))
                        {
                            msg = "Item Already Selected!";
                            drpItemName.Focus(); drpItemName.SelectedIndex = 0;
                            drpUnitName.SelectedIndex = 0;
                            this.ShowMessage(msg);
                            return;
                        }
                    }
                }
                else
                { DtTemp = CreateDt(); }
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ApplicationFunction.DatatableAddRow(DtTemp, id, drpItemName.SelectedValue, drpItemName.SelectedItem.Text, txtQty.Text,
                                                   txtWeight.Text, drpUnitName.SelectedValue, drpUnitName.SelectedItem.Text, ddlRateType.SelectedValue, ddlRateType.SelectedItem.Text, txtRate.Text, txtAmount.Text);
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            NetAmntcal();
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

        protected void drpItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((drpToCity.SelectedIndex <= 0))
                {
                    this.ShowMessage("Please select To City.");
                    drpToCity.Focus();
                    drpItemName.SelectedIndex = 0;
                    return;
                }
                if ((drpBaseCity.SelectedIndex <= 0))
                {

                    this.ShowMessage("Please select Loc[From].");
                    drpBaseCity.Focus(); drpItemName.SelectedIndex = 0;
                    return;
                }
                txtQty.Text = "1";
                txtWeight.Text = "0.00";
                txtAmount.Text = "0.00";
                txtRate.Text = "0.00";
                drpUnitName.SelectedIndex = 0;

                drpItemName.Focus();
            }
            catch (Exception Ex)
            {
            }
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            //GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                DtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = DtTemp.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {

                    drpItemName.SelectedValue = Convert.ToString(drs[0]["ITEM_IDNO"]);
                    ddlRateType.SelectedValue = Convert.ToString(drs[0]["RATE_IDNO"]);
                    ddlRateType_SelectedIndexChanged(null, null);
                    txtQty.Text = Convert.ToString(drs[0]["ITEM_QTY"]);
                    txtWeight.Text = Convert.ToString(drs[0]["ITEM_WEIGHT"]);
                    drpUnitName.SelectedValue = Convert.ToString(drs[0]["UOM_IDNO"]);
                    txtRate.Text = Convert.ToString(drs[0]["ITEM_RATE"]);
                    txtAmount.Text = Convert.ToString(drs[0]["ITEM_AMOUNT"]);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
                    hiduomid.Value = Convert.ToString(drs[0]["UOM_IDNO"]);
                }
                drpItemName.Focus();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable dt = CreateDt();
                foreach (DataRow rw in DtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {

                        ApplicationFunction.DatatableAddRow(dt, rw["id"], rw["ITEM_IDNO"], rw["ITEM_NAME"], rw["ITEM_QTY"],
                                                                rw["ITEM_WEIGHT"], rw["UOM_IDNO"], rw["UOM_NAME"], rw["RATE_IDNO"], rw["RATE_TYPE"], rw["ITEM_RATE"], rw["ITEM_AMOUNT"]);

                    }
                }
                ViewState["dt"] = dt;
                dt.Dispose();

                this.BindGrid();
                NetAmntcal();
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
                TotWeight = TotWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ITEM_WEIGHT"));
                gross = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ITEM_AMOUNT"));
                GrossAmnt += gross;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblqtytotal = (Label)e.Row.FindControl("lblqtytotal");
                lblqtytotal.Text = Convert.ToDouble((totalQty)).ToString("N2");
                Label lblItemTotal = (Label)e.Row.FindControl("lblItemTotal");
                lblItemTotal.Text = "Total";
                Label lblTotalweight = (Label)e.Row.FindControl("lblTotalweight");
                lblTotalweight.Text = TotWeight.ToString("N2");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDouble((GrossAmnt)).ToString("N2");
                txtGrossAmnt.Text = GrossAmnt.ToString("N2");

            }
        }
        #endregion

        #region Functions..
        private void ClearAll()
        {
            this.GetQutionNo(Convert.ToInt32((drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue)), Convert.ToInt32(intYearIdno));
            drpBaseCity.SelectedValue = "0";
            drpSender.SelectedValue = "0";
            drpToCity.SelectedValue = "0";
            drpDeliveryPlace.SelectedValue = "0";
            ddlType.SelectedIndex = 0;
            txtOtherAmnt.Text = "0.00";
            txtNetAmnt.Text = "0.00";
            txtGrossAmnt.Text = "0.00";
            txtRoundOff.Text = "0.00";
            txtRemark.Text = "0.00";
            ViewState["dt"] = DtTemp = null;
            grdMain.DataSource = null;
            grdMain.DataBind();
        }

        private void BindBaseCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "cityidno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var lst = obj.BindAllToCity();
            drpToCity.DataSource = lst;
            drpToCity.DataTextField = "City_Name";
            drpToCity.DataValueField = "City_Idno";
            drpToCity.DataBind();
            drpToCity.Items.Insert(0, new ListItem("--Select--", "0"));

            drpDeliveryPlace.DataSource = lst;
            drpDeliveryPlace.DataTextField = "City_Name";
            drpDeliveryPlace.DataValueField = "City_Idno";
            drpDeliveryPlace.DataBind();
            drpDeliveryPlace.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindAllCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            
            drpBaseCity.DataSource = lst;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new ListItem("--Select--", "0"));

            var list = obj.BindAllToCity();
            drpToCity.DataSource = list;
            drpToCity.DataTextField = "City_Name";
            drpToCity.DataValueField = "City_Idno";
            drpToCity.DataBind();
            drpToCity.Items.Insert(0, new ListItem("--Select--", "0"));

            drpDeliveryPlace.DataSource = list;
            drpDeliveryPlace.DataTextField = "City_Name";
            drpDeliveryPlace.DataValueField = "City_Idno";
            drpDeliveryPlace.DataBind();
            drpDeliveryPlace.Items.Insert(0, new ListItem("--Select--", "0"));
            obj = null;

        }

        private void PrintGRPrep(Int64 HeadId)
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
            lblPrintHeadng.Text = "Quotation - " + Convert.ToString(ddlType.SelectedItem.Text);
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spQuatation] @ACTION='SelectPrint',@Id='" + HeadId + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["QuHead_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["QuHead_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);

                lblSenderName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);

                valuelblgrossAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"]));
                if (valuelblgrossAmnt.Text != "")
                {
                    dgross = Convert.ToDouble(valuelblgrossAmnt.Text);
                }
                else
                {
                    dgross = 0;
                }
                valuelblOthramnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Other_Amnt"]));
                if (valuelblOthramnt.Text != "")
                {
                    dothr = Convert.ToDouble(valuelblOthramnt.Text);
                }
                else
                {
                    dothr = 0;
                }
                valuelblroundoffAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["RndOff_Amnt"]));
                if (valuelblroundoffAmnt.Text != "")
                {
                    dround = Convert.ToDouble(valuelblroundoffAmnt.Text);
                }
                else
                {
                    dround = 0;
                }
                valuelblNetAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Net_Amnt"]));
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
            QuotationDAL obj = new QuotationDAL();
            tblQuatationHead QtnHead = obj.SelectByQuotationHeadByHeadId(HeadId);
            var QtnDetlList = obj.SelectQuotationDetailByHeadId(HeadId);
            obj = null;

            if (QtnHead != null)
            {
                txtQutNo.Text = Convert.ToString(QtnHead.QuHead_No);
                txtquotinDate.Text = Convert.ToDateTime(QtnHead.QuHead_Date).ToString("dd-MM-yyyy");
                txtRemark.Text = Convert.ToString(QtnHead.Remark);
                ddlType.SelectedValue = Convert.ToString(QtnHead.QuHead_Typ);
                drpBaseCity.SelectedValue = Convert.ToString(QtnHead.FromCity_Idno);
                txtNetAmnt.Text = Convert.ToDouble(QtnHead.Net_Amnt).ToString("N2");
                txtRoundOff.Text = Convert.ToDouble(QtnHead.RndOff_Amnt).ToString("N2");
                txtGrossAmnt.Text = Convert.ToDouble(QtnHead.Gross_Amnt).ToString("N2");
                txtOtherAmnt.Text = Convert.ToDouble(QtnHead.Other_Amnt).ToString("N2");
                drpDeliveryPlace.SelectedValue = Convert.ToString(QtnHead.DelvryPlce_Idno);
                drpToCity.SelectedValue = Convert.ToString(QtnHead.ToCity_Idno);
                drpSender.SelectedValue = Convert.ToString(QtnHead.Sender_Idno);
                ddlDateRange.SelectedValue = Convert.ToString(QtnHead.Year_Idno);
                HidTbbType.Value = Convert.ToString(QtnHead.TBB_Rate);
                drpToCity.Enabled = false;
                drpBaseCity.Enabled = false; ;
                ddlType.Enabled = false;
                txtQutNo.Visible = true;
                DtTemp = CreateDt();
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ViewState["dt"] = DtTemp;
                foreach (var QTD in QtnDetlList)
                {
                    ApplicationFunction.DatatableAddRow(DtTemp, id, Convert.ToString(DataBinder.Eval(QTD, "Item_Idno")),
                        Convert.ToString(DataBinder.Eval(QTD, "Item_Name")),
                        Convert.ToString(DataBinder.Eval(QTD, "Qty")),
                        Convert.ToDouble(DataBinder.Eval(QTD, "Tot_Weght")).ToString("N2"),
                        Convert.ToString(DataBinder.Eval(QTD, "Unit_idno")),
                        Convert.ToString(DataBinder.Eval(QTD, "UOM_Name")),
                        Convert.ToString(DataBinder.Eval(QTD, "Rate_Idno")),
                        Convert.ToString(DataBinder.Eval(QTD, "Rate_Type")),
                        Convert.ToDouble(DataBinder.Eval(QTD, "Item_Rate")).ToString("N2"),
                        Convert.ToDouble(DataBinder.Eval(QTD, "Amount")).ToString("N2"));

                    id++;
                }
                ViewState["dt"] = DtTemp;
            }
            this.BindGrid();
            NetAmntcal();
            PrintGRPrep(HeadId);
        }

        private void GetQutionNo(Int32 CityIdno, Int32 YearId)
        {
            QuotationDAL obj = new QuotationDAL();
            Int64 max = obj.GetMaxNo(CityIdno, YearId);
            obj = null;
            txtQutNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }
        public void GetAllItems()
        {
            ItemMasterDAL obj = new ItemMasterDAL();
            var lst = obj.GetItems();
            obj = null;
            drpItemName.DataSource = lst;
            drpItemName.DataTextField = "ItemName";
            drpItemName.DataValueField = "ItemId";
            drpItemName.DataBind();
            drpItemName.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    txtquotinDate.Text = hidmindate.Value;
                    txtquotinDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtquotinDate.Text = hidmindate.Value;

                }
            }

        }
        public void GetAllUnit()
        {
            UOMMasterDAL obj = new UOMMasterDAL();
            var lst = obj.GetUnit();
            obj = null;
            drpUnitName.DataSource = lst;
            drpUnitName.DataTextField = "UOMName";
            drpUnitName.DataValueField = "UOMId";
            drpUnitName.DataBind();
            drpUnitName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindSenderPopulate()
        {
            QuotationDAL obj = new QuotationDAL();
            var lst = obj.FetchSenderPopulate();
            obj = null;
            drpSender.DataSource = lst;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindSender()
        {
            QuotationDAL obj = new QuotationDAL();
            var lst = obj.FetchSender();
            obj = null;
            drpSender.DataSource = lst;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindRateType()
        {
            QuotationDAL obj = new QuotationDAL();
            var lst = obj.FetchRate();
            obj = null;
            ddlRateType.DataSource = lst;
            ddlRateType.DataTextField = "Rate_Type";
            ddlRateType.DataValueField = "Rate_Idno";
            ddlRateType.DataBind();
            ddlRateType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "ITEM_IDNO", "String", "ITEM_NAME", "String", "ITEM_QTY", "String", "ITEM_WEIGHT", "String", "UOM_IDNO", "String", "UOM_NAME", "String",
                                                                   "RATE_IDNO", "String", "RATE_TYPE", "String", "ITEM_RATE", "String", "ITEM_AMOUNT", "String");//ITEM_AMOUNT
            return dttemp;
        }
        private void ClearItems()
        {
            drpItemName.SelectedValue = "0";
            txtQty.Text = "0.00";
            drpUnitName.SelectedValue = "0";
            txtWeight.Text = "0.00";
            hiduomid.Value = "";
            hidrowid.Value = string.Empty;
            ddlRateType.SelectedValue = "0";
            txtRate.Text = "0.00";
            txtAmount.Text = "0.00";
        }
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }


        private void NetAmntcal()
        {
            try
            {
                txtNetAmnt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + (Convert.ToDouble(txtOtherAmnt.Text)))).ToString("N2");
                txtRoundOff.Text = Convert.ToDouble(Convert.ToDouble(txtNetAmnt.Text) - Convert.ToDouble(Convert.ToDouble(txtGrossAmnt.Text) + Convert.ToDouble(txtOtherAmnt.Text))).ToString("N2");
            }
            catch (Exception e)
            {
            }

        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void CalculateEdit()
        {
            QuotationDAL objGrprepDAL = new QuotationDAL();
            //  isTBBRate = objGrprepDAL.SelectTBBRate();
            double iRate = 0; double EditRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtquotinDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (drpItemName.SelectedIndex > 0)
            {

                if ((ddlRateType.SelectedIndex) > 0)
                {
                    if (ddlRateType.SelectedIndex == 1)
                    {
                        if (txtRate.Text.Trim() != "")
                            dtotalAmount = Convert.ToDouble(Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text));
                        txtAmount.Text = dtotalAmount.ToString("N2");
                    }
                    else
                    {
                        if (txtWeight.Text.Trim() != "")
                            dtotalAmount = Convert.ToDouble(Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtWeight.Text));
                        txtAmount.Text = dtotalAmount.ToString("N2");
                    }
                }
            }
            RcptAmtTot(DtTemp);
            NetAmntcal();
        }

        private void FillRate()
        {
            // By Lokesh (actually i dont know wts going on , in this function , so i was commented existing code and write new code discuussed with pankanj ji )
            QuotationDAL objGrprepDAL = new QuotationDAL();
            double iRate = 0; double EditRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtquotinDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (drpItemName.SelectedIndex > 0)
            {
                if (ddlType.SelectedIndex == 1) //In case of Paid
                {
                    if (Convert.ToBoolean(HidTbbType.Value) == false)
                    {
                        txtRate.Text = "0.00";
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlRateType.SelectedValue) > 0)
                        {
                            if (Convert.ToInt32(ddlRateType.SelectedValue) == 1)
                            {
                                iRate = objGrprepDAL.SelectItemRateForTBB(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtRate.Text = iRate.ToString("N2");
                                txtQty.Text = "0.00"; txtWeight.Text = "0.00";
                                iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");
                                iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");
                            }
                            else
                            {
                                iRate = objGrprepDAL.SelectItemWghtRateForTBB(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtRate.Text = iRate.ToString("N2");
                                txtWeight.Text = "0.00"; txtQty.Text = "1";
                                iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");
                                iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");
                            }
                            //if (Convert.ToInt32(ddlRateType.SelectedValue) == 1)
                            //{
                            //    iRate = objGrprepDAL.SelectItemRateForTBB(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    txtRate.Text = iRate.ToString("N2"); txtRate.ReadOnly = false;
                            //    txtQty.Text = "0.00"; txtWeight.ReadOnly = true; txtQty.ReadOnly = false;
                            //    iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");
                            //    iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");
                            //}
                            //else
                            //{
                            //    iRate = objGrprepDAL.SelectItemWghtRateForTBB(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    txtRate.Text = iRate.ToString("N2"); txtWeight.ReadOnly = false;
                            //    txtWeight.Text = "0.00"; txtQty.ReadOnly = true;
                            //    iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");
                            //    iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            //    hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");
                            //}
                        }
                        else
                        {
                            txtRate.Text = "0.00";
                        }
                    }
                }
                else
                {
                    if ((ddlRateType.SelectedIndex) > 0)
                    {
                        if (Convert.ToInt32(ddlRateType.SelectedValue) == 1)
                        {
                            iRate = objGrprepDAL.SelectItemRate(Convert.ToInt64(drpItemName.SelectedValue), Convert.ToInt64(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            txtRate.Text = iRate.ToString("N2");
                            txtWeight.Text = "0.00"; txtQty.Text = "0.00";
                            iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");
                            iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");
                        }
                        else
                        {
                            iRate = objGrprepDAL.SelectItemWghtRate(Convert.ToInt64(drpItemName.SelectedValue), Convert.ToInt64(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            txtRate.Text = iRate.ToString("N2"); txtQty.Text = "0.00";
                            iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");
                            iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                            hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");
                        }

                        //if (Convert.ToInt32(ddlRateType.SelectedValue) == 1)
                        //{
                        //    iRate = objGrprepDAL.SelectItemRate(Convert.ToInt64(drpItemName.SelectedValue), Convert.ToInt64(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    txtRate.Text = iRate.ToString("N2"); txtRate.ReadOnly = true;
                        //    txtWeight.Text = "0.00"; txtWeight.ReadOnly = false; txtQty.Text = "0.00"; txtQty.ReadOnly = true;
                        //    iQtyShrtgRate = objGrprepDAL.SelectQtyShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    hidShrtgRate.Value = iQtyShrtgRate.ToString("N2");
                        //    iQtyShrtgLimit = objGrprepDAL.SelectQtyShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    hidShrtgLimit.Value = iQtyShrtgLimit.ToString("N2");
                        //}
                        //else
                        //{
                        //    iRate = objGrprepDAL.SelectItemWghtRate(Convert.ToInt64(drpItemName.SelectedValue), Convert.ToInt64(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    txtRate.Text = iRate.ToString("N2"); txtWeight.ReadOnly = true;
                        //    txtRate.ReadOnly = true; txtQty.Text = "0.00"; txtQty.ReadOnly = false;
                        //    iWghtShrtgLimit = objGrprepDAL.SelectWghtShrtgLimit(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    hidShrtgLimit.Value = iWghtShrtgLimit.ToString("N2");
                        //    iWghtShrtgRate = objGrprepDAL.SelectWghtShrtgRate(Convert.ToInt32(drpItemName.SelectedValue), Convert.ToInt32(drpToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                        //    hidShrtgRate.Value = iWghtShrtgRate.ToString("N2");
                        //}
                    }
                    else
                    {
                        txtRate.Text = "0.00";
                    }
                }
            }
        }
        private void RcptAmtTot(DataTable dtTemp)
        {
            try
            {
                int c = 0, itotRow = 0, itotQty = 0; double itotWeght = 0;
                double dtotAmnt = 0;
                c = grdMain.Rows.Count;
                if (c > 0)
                {
                    for (int i = 0; i < (c - 1); i++)
                    {
                        if ((Convert.ToString(dtTemp.Rows[i]["Item_Idno"]) != "") && (Convert.ToString(dtTemp.Rows[i]["unit_Idno"]) != ""))
                        {
                            itotQty = Convert.ToInt32(itotQty + Convert.ToInt32(dtTemp.Rows[i]["Quantity"]));
                            itotWeght = itotWeght + Convert.ToDouble(dtTemp.Rows[i]["Weight"]);
                            dtotAmnt = Convert.ToDouble(dtotAmnt + Convert.ToDouble(dtTemp.Rows[i]["Amount"]));
                        }
                    }

                }
                itotRow = grdMain.Rows.Count;

                txtGrossAmnt.Text = dtotAmnt.ToString("N2");

            }
            catch (Exception Ex)
            { }
        }
        #endregion

        #region Controls Events...

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void txtOtherAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherAmnt.Text == "")
            {
                txtOtherAmnt.Text = "0.00";

            }
            else
            {
                txtOtherAmnt.Text = Convert.ToDouble(txtOtherAmnt.Text).ToString("N2");
                NetAmntcal();

            }
        }

        protected void ddlRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRateType.SelectedIndex > 0)
                {
                    if (drpItemName.SelectedIndex > 0)
                    {
                        FillRate();
                        CalculateEdit();
                    }

                    if (Convert.ToInt32(ddlRateType.SelectedValue) == 1)  //rate
                    {
                        txtQty.Text = "0.00";
                        txtWeight.Text = "0.00";
                        txtQty.Enabled = true;
                        txtWeight.Enabled = false;
                        txtRate.Enabled = true;
                        SpanQty.Visible = true;
                        rfvQty.Enabled = true;
                        rfvQty.Visible = true;
                    }
                    else if (Convert.ToInt32(ddlRateType.SelectedValue) == 2)//weight
                    {
                        txtWeight.Enabled = true;
                        txtRate.Enabled = true;
                        txtQty.Enabled = false;

                        txtWeight.Text = "0.00";
                        txtQty.Text = "1";
                        SpanQty.Visible = false;
                        rfvQty.Enabled = false;
                        rfvQty.Visible = false;
                    }
                }

                ddlRateType.Focus();
            }
            catch (Exception Ex)
            {
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                txtQty.Text = "1";
            }
            else
            {
                txtQty.Text = Convert.ToDouble(txtQty.Text).ToString("N2");
                CalculateEdit();
            }
            txtQty.Focus();
        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtWeight.Text == "")
            {
                txtWeight.Text = "1";
            }
            else
            {
                //if (ddlType.SelectedIndex == 1)
                //{
                //    txtWeight.Text = "0";
                //}
                txtWeight.Text = Convert.ToDouble(txtWeight.Text).ToString("N2");
                CalculateEdit();
            }
            txtWeight.Focus();
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                txtAmount.Text = "0.00";
            }
            else
            {
                txtAmount.Text = Convert.ToDouble(txtAmount.Text).ToString("N2");
            }
            txtAmount.Focus();
        }

        protected void txtGrossAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtGrossAmnt.Text == "")
            {
                txtGrossAmnt.Text = "0.00";
            }
            else
            {
                txtGrossAmnt.Text = Convert.ToDouble(txtGrossAmnt.Text).ToString("N2");
                NetAmntcal();
            }
            txtGrossAmnt.Focus();
        }

        protected void txtNetAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtNetAmnt.Text == "")
            {
                txtNetAmnt.Text = "0.00";
            }
            else
            {
                txtNetAmnt.Text = Convert.ToDouble(txtNetAmnt.Text).ToString("N2");
                NetAmntcal();
            }
            txtNetAmnt.Focus();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            if (txtRate.Text == "")
            {
                txtRate.Text = "0.00";
            }
            else
            {
                txtRate.Text = Convert.ToDouble(txtRate.Text).ToString("N2");
                if (ddlType.SelectedIndex == 1) //In case of Paid
                {
                    if (Convert.ToBoolean(HidTbbType.Value) == false)
                    {
                        txtRate.Text = "0.00";
                    }
                }
                CalculateEdit();
            }
            txtRate.Focus();
        }

        protected void txtRoundOff_TextChanged(object sender, EventArgs e)
        {
            if (txtRoundOff.Text == "")
            {
                txtRoundOff.Text = "0.00";

            }
            else
            {
                txtRoundOff.Text = Convert.ToDouble(txtRoundOff.Text).ToString("N2");
                NetAmntcal();

            }
            txtRoundOff.Focus();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedIndex >= 0)
            {
                ClearItems();
                DtTemp = null;
                grdMain.DataSource = null;
                ViewState["dt"] = DtTemp = null;
                grdMain.DataSource = null;
                grdMain.DataBind(); drpItemName.Focus();
            }

            ddlType.Focus();
        }

        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpBaseCity.SelectedIndex > 0)
            {
                this.GetQutionNo(Convert.ToInt32((drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                ClearItems();
                DtTemp = null;
                grdMain.DataSource = null;
                ViewState["dt"] = DtTemp = null;
                grdMain.DataSource = null;
                grdMain.DataBind(); drpItemName.Focus();
            }

            drpBaseCity.Focus();
        }

        protected void drpToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpToCity.SelectedIndex > 0)
            {
                ClearItems();
                DtTemp = null;
                grdMain.DataSource = null;
                ViewState["dt"] = DtTemp = null;
                grdMain.DataSource = null;
                grdMain.DataBind();
                drpItemName.Focus();
            }
            drpToCity.Focus();
        }

        protected void drpUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (drpItemName.SelectedIndex > 0 && drpUnitName.SelectedIndex > 0)
                {
                    if (Convert.ToInt64(string.IsNullOrEmpty(hidrowid.Value) == true ? "-1" : hidrowid.Value) > 0)
                    {
                        DtTemp = (DataTable)ViewState["dt"];
                        if (DtTemp != null && DtTemp.Rows.Count > 0)
                        {
                            foreach (DataRow dtrow in DtTemp.Rows)
                            {
                                if ((Convert.ToString(dtrow["ITEM_IDNO"]) == Convert.ToString(drpItemName.SelectedValue)) && (Convert.ToString(dtrow["UOM_IDNO"]) == Convert.ToString(drpUnitName.SelectedValue)) && (Convert.ToInt64(dtrow["id"]) != Convert.ToInt64(hidrowid.Value)))
                                {
                                    string msg = "Item Already Selected With Unit!";
                                    drpItemName.SelectedIndex = 0;
                                    drpUnitName.SelectedIndex = 0;
                                    drpItemName.Focus();
                                    this.ShowMessageErr(msg);
                                    return;
                                }
                            }
                        }
                    }
             else
                {
                     DtTemp = (DataTable)ViewState["dt"];
                    if (DtTemp != null && DtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dtrow in DtTemp.Rows)
                        {
                            if ((Convert.ToString(dtrow["ITEM_IDNO"]) == Convert.ToString(drpItemName.SelectedValue)) && (Convert.ToString(dtrow["UOM_IDNO"]) == Convert.ToString(drpUnitName.SelectedValue)))
                            {
                                string msg = "Item Already Selected With Unit!";
                                drpItemName.SelectedIndex = 0;
                                drpUnitName.SelectedIndex = 0;
                                drpItemName.Focus();
                                this.ShowMessageErr(msg);
                                return;
                            }
                        }
                    }
                }
         }
            drpUnitName.Focus();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();

            }
        }

        #endregion
    }
}