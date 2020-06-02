using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;

namespace WebTransport
{
    public partial class AdvBookGR : Pagebase
    {
        #region GlobalVariable
        DataTable dtTemp = new DataTable();
        Double iRate = 0, dtotalAmount = 0; Double iqty = 0; double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, NetAmountPrint = 0; bool IsWeight = false;
        int chkbit = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!IsPostBack)
            {
                txtOrderDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRecDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBKGDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtBKGDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                this.BindDropDown();
                this.BindContainerDetails();
                this.BindAgentName();
                this.ddlDateRange_SelectedIndexChanged(sender, e);

                AdvBookGRDAL objGrprepDAL = new AdvBookGRDAL();

                hidTBBType.Value = Convert.ToString(objGrprepDAL.SelectTBBRate());
                if (Convert.ToBoolean(hidTBBType.Value) == true) { ViewState["TBBRate"] = "true"; }

                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
               
                if(Convert.ToString(base.UserFromCity)!="0")
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);

                if (Convert.ToString(base.UserToCity) != "0")
                ddlToCity.SelectedValue = Convert.ToString(base.UserToCity);
                drpBaseCity_OnSelectedIndexChanged(null, null);

                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    BindDropdownDAL obj = new BindDropdownDAL();
                    var itemname = obj.BindItemName();
                    ddlItemName.DataSource = itemname;
                    ddlItemName.DataTextField = "Item_name";
                    ddlItemName.DataValueField = "Item_idno";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindDropDown();
                }
                if (Convert.ToString(base.ItemName) != "0")
                ddlItemName.SelectedValue = Convert.ToString(base.ItemName);

                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    BindDropdownDAL obj = new BindDropdownDAL();
                    var itemname = obj.BindUnitName();
                    ddlunitname.DataSource = itemname;
                    ddlunitname.DataTextField = "UOM_Name";
                    ddlunitname.DataValueField = "UOM_Idno";
                    ddlunitname.DataBind();
                    ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindDropDown();
                }
                if (Convert.ToString(base.Unit) != "0")
                ddlunitname.SelectedValue = Convert.ToString(base.Unit);
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;

                if (Request["OrderID"] != null)
                {
                    lnkBtnLast.Visible = false;
                    lnkbtnNew.Visible = true; lnkbtnPrint.Visible = true;
                    populate(Convert.ToInt32(Request["OrderID"].ToString()));
                }
                else { lnkBtnLast.Visible = true;  lnkbtnNew.Visible = false; lnkbtnPrint.Visible = false; }

                userpref();
                if (IsWeight == true)
                {
                    ddlRateType.Enabled = false;
                    ddlRateType.SelectedIndex = 1;
                    rfvWeight.Enabled = true;
                }
                else
                {
                    rfvWeight.Enabled = false;
                    ddlRateType.Enabled = true;
                }
            }

            userpref();
            if (IsWeight == true)
            {
                ddlRateType.Enabled = false;
                ddlRateType.SelectedIndex = 1;
                rfvWeight.Enabled = true;
            }
            else
            {
                rfvWeight.Enabled = false;
                ddlRateType.Enabled = true;
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

        #region Button Controls...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            #region Validation Messages
            if (ddlParty.SelectedIndex == 0) { this.ShowMessageErr("Please select party's Name!"); ddlParty.Focus(); return; }
            if (drpBaseCity.SelectedIndex == 0) { this.ShowMessageErr("Please select location!"); drpBaseCity.Focus(); return; }
            if (ddlToCity.SelectedIndex == 0) { this.ShowMessageErr("Please select To City."); ddlToCity.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select To City."; return; }
            if (ddlLocation.SelectedIndex == 0) { this.ShowMessageErr("Please select Delivery Place."); ddlLocation.Focus(); lblmessage.Visible = true; lblmessage.Text = "* Please select Delivery Place."; return; }

            #endregion

            #region Declare Input Variables
            string strMsg = string.Empty;
            Int64 intGrPrepIdno = 0;

            DateTime strOrdrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtOrderDate.Text.Trim().ToString()));

            DateTime? strOrdrRecDate = null;
            if (!string.IsNullOrEmpty(txtRecDate.Text.Trim()))
            {
                strOrdrRecDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtRecDate.Text.Trim().ToString()));
            }
            DateTime? strBKGDateFrom = null;
            if (!string.IsNullOrEmpty(txtBKGDateFrom.Text.Trim()))
            {
                strBKGDateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBKGDateFrom.Text.Trim().ToString()));
            }
            DateTime? strBKGDateTo = null;
            if (!string.IsNullOrEmpty(txtBKGDateTo.Text.Trim()))
            {
                strBKGDateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtBKGDateTo.Text.Trim().ToString()));
            }
            Int32 YearIdno = Convert.ToInt32(ddlDateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 intLocIDno = string.IsNullOrEmpty(drpBaseCity.SelectedValue) ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue);
            Int64 intOrderNo = string.IsNullOrEmpty(txtOrderNo.Text.Trim()) ? 0 : Convert.ToInt64(txtOrderNo.Text.Trim());
            string ReferNum = txtReffrnceNumber.Text.Trim();
            Int32 TruckNoIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);
            Int32 intPartyIdno = string.IsNullOrEmpty(ddlParty.SelectedValue) ? 0 : Convert.ToInt32(ddlParty.SelectedValue);
            Int32 intTocityIDno = string.IsNullOrEmpty(ddlToCity.SelectedValue) ? 0 : Convert.ToInt32(ddlToCity.SelectedValue);
            Int32 intDelPlaceIdno = string.IsNullOrEmpty(ddlLocation.SelectedValue) ? 0 : Convert.ToInt32(ddlLocation.SelectedValue);
            Int64 intAgentIdno = string.IsNullOrEmpty(ddlAgent.SelectedValue) ? 0 : Convert.ToInt64(ddlAgent.SelectedValue);
            Int32 intcityViaIDno = string.IsNullOrEmpty(ddlviacity.SelectedValue) ? 0 : Convert.ToInt32(ddlviacity.SelectedValue);

            string ConsrName = string.IsNullOrEmpty(txtconsnr.Text.Trim()) ? "" : Convert.ToString(txtconsnr.Text.Trim());
            string strShipmentNo = string.IsNullOrEmpty(txtshipment.Text.Trim()) ? "" : Convert.ToString(txtshipment.Text.Trim());
            String ContainerNum = txtContainrNo.Text.Trim();
            String ContainerSealNum = txtContainerSealNo.Text.Trim();

            //Ajeet
            String ContainerNum2 = txtContainrNo2.Text.Trim();
            String ContainerSealNum2 = txtContainerSealNo2.Text.Trim();
            Int32 ImpExp_id = Convert.ToInt32(ddlTypeI.SelectedValue);
            string CharFrwder_Name = txtNameI.Text.Trim();

            Int64 ContainerSize = string.IsNullOrEmpty(ddlContainerSize.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerSize.SelectedValue);
            Int64 ContainerType = string.IsNullOrEmpty(ddlContainerType.SelectedValue) ? 0 : Convert.ToInt64(ddlContainerType.SelectedValue);
            string PortNum = txtPortNum.Text.Trim();

            Int32 GRType = Convert.ToInt32(ddlGRType.SelectedValue);
            Double GrossAmnt = Convert.ToDouble(txtGrossAmnt.Text.Trim());
            Double RoundOff = Convert.ToDouble(TxtRoundOff.Text.Trim());
            Double NetAmnt = Convert.ToDouble(txtNetAmnt.Text.Trim());

            string strRemark = string.IsNullOrEmpty(TxtRemark.Text.Trim()) ? "" : Convert.ToString(TxtRemark.Text.Trim());
            DataTable dtDetail = (DataTable)ViewState["dt"];
            #endregion

            #region Insert/Update with Transaction
            if (grdMain.Rows.Count > 0 && dtDetail != null && dtDetail.Rows.Count > 0)
            {
                AdvBookGRDAL obj = new AdvBookGRDAL();
                if (hidOrderIdno.Value != string.Empty)
                {
                    intGrPrepIdno = obj.UpdateAdvBookGROrder(Convert.ToInt64(hidOrderIdno.Value), YearIdno, intLocIDno, strOrdrDate, strOrdrRecDate, intOrderNo, ReferNum, TruckNoIdno, intPartyIdno, intTocityIDno, intcityViaIDno, intDelPlaceIdno, strShipmentNo, ContainerNum, ContainerSealNum, ContainerSize, ContainerType, PortNum, strRemark, GrossAmnt, RoundOff, NetAmnt, dtDetail, GRType, Convert.ToInt64(Session["UserIdno"]), intAgentIdno, strBKGDateFrom, strBKGDateTo, ConsrName, ContainerNum2, ContainerSealNum2, ImpExp_id, CharFrwder_Name);
                }
                else
                {
                    intGrPrepIdno = obj.InsertAdvBookGROrder(YearIdno, intLocIDno, strOrdrDate, strOrdrRecDate, intOrderNo, ReferNum, TruckNoIdno, intPartyIdno, intTocityIDno, intcityViaIDno, intDelPlaceIdno, strShipmentNo, ContainerNum, ContainerSealNum, ContainerSize, ContainerType, PortNum, strRemark, GrossAmnt, RoundOff, NetAmnt, dtDetail, GRType, Convert.ToInt64(Session["UserIdno"]), intAgentIdno, strBKGDateFrom, strBKGDateTo, ConsrName, ContainerNum2, ContainerSealNum2, ImpExp_id, CharFrwder_Name);
                }
                obj = null;
                if (intGrPrepIdno > 0)
                {
                    this.drpBaseCity_OnSelectedIndexChanged(new Object(), EventArgs.Empty);
                    if (Convert.ToString(hidOrderIdno.Value) != null && Convert.ToString(hidOrderIdno.Value) != "")
                    {
                        clearControls();
                        lnkbtnNew.Visible = false; lnkbtnPrint.Visible = false;
                        this.ShowMessage("Record updated successfully.");
                    }
                    else
                    {
                        clearControls();
                        this.ShowMessage("Record saved successfully.");
                    }
                }
                else
                {
                    if (Convert.ToString(hidOrderIdno.Value) != null && Convert.ToString(hidOrderIdno.Value) != "")
                    {
                        this.ShowMessageErr("Record not updated.");
                    }
                    else
                    {
                        this.ShowMessageErr("Record not saved.");
                    }
                }
            }
            else
            {
                this.ShowMessageErr("Please select items.");
            }
            #endregion
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request["OrderID"] != null)
            {
                populate(Convert.ToInt32(Request["OrderID"].ToString()));
            }
            else
            {
                clearControls();
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AdvBookGR.aspx");
        }

        protected void lnkbtnContnrDtl_OnClick(object sender, EventArgs e)
        {
            txtContainrNo.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }

        protected void lnkbtnContainerSubmit_OnClick(object sender, EventArgs e)
        {
            TxtRemark.Focus();
        }

        protected void lnkbtnClose_OnClick(object sender, EventArgs e)
        {
            if (Request["OrderID"] != null)
            {
                populate(Convert.ToInt32(Request["OrderID"].ToString()));

                TxtRemark.Focus();
            }
            else
            {
                txtContainrNo.Text = txtContainerSealNo.Text = txtPortNum.Text = "";
                ddlContainerSize.SelectedValue = ddlContainerType.SelectedValue = "0";

                TxtRemark.Focus();
            }
        }

        // For items

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            if (ddlunitname.SelectedIndex == 0) { ShowMessageErr("Please select Unit "); ddlunitname.Focus(); return; }
            if (ddlRateType.SelectedIndex == 0) { ShowMessageErr("Please select the Rate Type."); ddlRateType.Focus(); return; }
            if(IsWeight==true)
                if (Convert.ToDouble(txtweight.Text.Trim()) <= 0) { ShowMessageErr("Please Enter Weight more than Zero!"); txtweight.Focus(); return; }
            //if (ddlRateType.SelectedIndex != 1) { if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0) { ShowMessageErr("Weight should be greater than zero."); txtweight.Focus(); return; } }

            if (ddlRateType.SelectedValue != "2")
                if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0) { ShowMessageErr("Quantity should be greater than zero."); txtQuantity.Focus(); return; }

            if (ViewState["TBBRate"] != null)
            {
                if (txtrate.Text == "") { ShowMessageErr("Please enter item rate."); ddlItemName.Focus(); return; }
                if (Convert.ToDouble(txtrate.Text) <= 0)
                {
                    ShowMessageErr("Rate should be greater than zero.");
                    txtrate.Focus();
                    return;
                }
            }

            Calculations();
            string strAmount = "";
            if (hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["Item_Name"] = ddlItemName.SelectedItem.Text;
                        dtrow["Item_Idno"] = ddlItemName.SelectedValue;
                        dtrow["Unit_Name"] = ddlunitname.SelectedItem.Text;
                        dtrow["Unit_Idno"] = ddlunitname.SelectedValue;
                        dtrow["Rate_Type"] = ddlRateType.SelectedItem.Text;
                        dtrow["Rate_TypeIdno"] = ddlRateType.SelectedValue;
                        dtrow["Quantity"] = txtQuantity.Text.Trim(); iqty += Convert.ToDouble(txtQuantity.Text.Trim());
                        dtrow["Weight"] = string.IsNullOrEmpty(Convert.ToString(txtweight.Text.Trim())) ? "0.00" : Convert.ToString(txtweight.Text.Trim());
                        dtrow["Rate"] = string.IsNullOrEmpty(Convert.ToString(txtrate.Text.Trim())) ? "0.00" : Convert.ToString(txtrate.Text.Trim());
                        dtrow["Amount"] = dtotalAmount.ToString("N2");

                    }
                }
            }
            else
            {
                if (ViewState["dt"] != null)
                    dtTemp = (DataTable)ViewState["dt"];
                else
                    dtTemp = CreateDt();

                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemNameId = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                string strUnitName = ddlunitname.SelectedItem.Text.Trim();
                string strUnitNameId = string.IsNullOrEmpty(ddlunitname.SelectedValue) ? "0" : (ddlunitname.SelectedValue);
                string strRateType = ddlRateType.SelectedItem.Text.Trim();
                string strRateTypeIdno = string.IsNullOrEmpty(ddlRateType.SelectedValue) ? "0" : (ddlRateType.SelectedValue);
                string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0.00" : (txtQuantity.Text.Trim());
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0.00" : (txtweight.Text.Trim());
                string strRate = string.IsNullOrEmpty(txtrate.Text.Trim()) ? "0.00" : (txtrate.Text.Trim());
                strAmount = dtotalAmount.ToString("N2");

                ApplicationFunction.DatatableAddRow(dtTemp, id, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount);
                ViewState["dt"] = dtTemp;
            }

            this.BindGridT();
            ddlItemName.Focus();
            ClearItems();

        }

        protected void lnkbtnAdd_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["OrderID"] != null)
            {
                this.ClearItems();
            }
            else
            {
                this.ClearItems();
                this.populate(Convert.ToInt32(Request.QueryString["OrderID"]));
            }
        }


        #endregion

        #region Controls Events..
        protected void ddlGRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearItems(); ViewState["dt"] = dtTemp = null;
                ddlGRType.Enabled = true;
                //ddlDateRange.SelectedIndex = 0;
                //ddlDateRange_SelectedIndexChanged(null, null);
                hidrowid.Value = "";
                //ddlParty.SelectedIndex = ddlTruckNo.SelectedIndex = ddlParty.SelectedIndex = ddlLocation.SelectedIndex = ddlToCity.SelectedIndex = 0;

                ddlItemName.SelectedIndex = ddlunitname.SelectedIndex = ddlRateType.SelectedIndex = 0;
                txtNetAmnt.Text = TxtRoundOff.Text = "0.00";
                grdMain.DataSource = null;
                grdMain.DataBind();
                ddlParty.Focus();
                //if (Convert.ToString(Session["Userclass"]) == "Admin")
                //{
                //    this.BindCity();
                //}
                //else
                //{
                //    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                //}

                txtReffrnceNumber.Focus();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpBaseCity.SelectedIndex <= 0)
                {
                    this.ShowMessageErr("Please select Location."); ddlItemName.Focus();
                    return;
                }
                if (ddlToCity.SelectedIndex <= 0)
                {
                    this.ShowMessageErr("Please select To City."); ddlItemName.Focus();
                    return;
                }
                else
                {
                    if (IsWeight == true)
                        FillRateWeightWiseRate();
                    else
                    FillRate();
                }
                if (Convert.ToInt64(string.IsNullOrEmpty(hidrowid.Value) == true ? "-1" : hidrowid.Value) > 0)
                {
                    DataTable dtTemp = (DataTable)ViewState["dt"];
                    if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim())) && (Convert.ToInt64(row["id"]) != Convert.ToInt64(hidrowid.Value)))
                            {
                                this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type.");
                                this.ClearItems();
                                ddlItemName.Focus();
                                return;
                            }
                            else
                            {
                                ddlunitname.SelectedIndex = 0; ddlRateType.SelectedIndex = 0;
                                txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
                            }
                        }
                    }
                    ddlunitname.Focus();
                }
                else
                {
                    DataTable dtTemp = (DataTable)ViewState["dt"];
                    if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim())))
                            {
                                this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type.");
                                this.ClearItems();
                                ddlItemName.Focus();
                                return;
                            }
                            else
                            {
                                ddlunitname.SelectedIndex = 0; ddlRateType.SelectedIndex = 0;
                                txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
                            }
                        }
                    }
                    ddlunitname.Focus();
                }
                if (IsWeight == true)
                {
                    ddlRateType.SelectedIndex = 1;
                }
                
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlunitname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(string.IsNullOrEmpty(hidrowid.Value) == true ? "-1" : hidrowid.Value) > 0)
                {
                    DataTable dtTemp = (DataTable)ViewState["dt"];
                    if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim()) && (Convert.ToInt64(row["id"]) != Convert.ToInt64(hidrowid.Value))))
                            {
                                this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type.");
                                this.ClearItems();
                                ddlItemName.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    DataTable dtTemp = (DataTable)ViewState["dt"];
                    if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            if ((Convert.ToString(row["Item_Name"]) == Convert.ToString(ddlItemName.SelectedItem.Text.Trim())) && (Convert.ToString(row["Unit_Name"]) == Convert.ToString(ddlunitname.SelectedItem.Text.Trim())))
                            {
                                this.ShowMessageErr("" + ddlItemName.SelectedItem.Text + " already selected in grid  with same unit type.");
                                this.ClearItems();
                                ddlItemName.Focus();
                                return;
                            }
                        }
                    }
                }
           

                ddlRateType.Focus();
                //CalculateEdit();
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void ddlRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsWeight == true)
                    FillRateWeightWiseRate();
                else
                    FillRate();

                if (ddlRateType.SelectedValue == "1") { txtQuantity.Focus(); }
                else { txtweight.Focus(); }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void txtweight_OnTextChanged(object sender, EventArgs e)
        {
            if (IsWeight == true)
            {
                FillRateWeightWiseRate();
                if (hidTBBType.Value == "False")
                        txtrate.Text = "0.00";
            }
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
                    txtOrderDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtRecDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //txtBKGDateFrom.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //txtBKGDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtOrderDate.Text = hidmindate.Value;
                    txtRecDate.Text = hidmindate.Value;
                    //txtBKGDateFrom.Text = hidmindate.Value;
                    //txtBKGDateTo.Text = hidmindate.Value;
                }
            }
        }

        protected void drpBaseCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AdvBookGRDAL objGR = new AdvBookGRDAL();
                Int64 MaxGRNo = 0;
                MaxGRNo = objGR.MaxNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue)), ApplicationFunction.ConnectionString());
                txtOrderNo.Text = Convert.ToString(MaxGRNo);
                if ((txtOrderNo.Text.Trim() != "") && (Convert.ToInt64(txtOrderNo.Text.Trim()) > 0))
                {
                    var lst = objGR.CheckDuplicateGrNo(Convert.ToInt64(txtOrderNo.Text.Trim()), Convert.ToInt32(Convert.ToString(drpBaseCity.SelectedValue) == "" ? 0 : Convert.ToInt32(drpBaseCity.SelectedValue)), Convert.ToInt32(ddlDateRange.SelectedValue));
                    if (lst.Count > 0)
                    {
                        this.ShowMessageErr("Duplicate GR No.!");
                        txtOrderNo.Focus(); txtOrderNo.SelectText();
                        return;
                    }
                    else
                    {
                        drpBaseCity.Focus(); txtOrderNo.SelectText();
                    }
                }
                else
                {
                    this.ShowMessageErr("GR No. can't be left blank.");
                    txtOrderNo.Text = Convert.ToString(MaxGRNo);
                    txtOrderNo.Focus(); txtOrderNo.SelectText();
                    return;
                }
            }
            catch (Exception Ex)
            {

            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlGenericControl DivRepHead = (HtmlGenericControl)e.Item.FindControl("DivRepHead");
                if (chkbit == 1) { DivRepHead.Visible = false; } else { DivRepHead.Visible = true; }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                HtmlGenericControl DivRepdetails = (HtmlGenericControl)e.Item.FindControl("DivRepdetails");
                if (chkbit == 1) { DivRepdetails.Visible = false; } else { DivRepdetails.Visible = true; }

                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Quantity"));
                NetAmountPrint += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlGenericControl DivRepFooter = (HtmlGenericControl)e.Item.FindControl("DivRepFooter");
                Label lblFTQty = (Label)e.Item.FindControl("lblFTQty");
                Label lblFTWeight = (Label)e.Item.FindControl("lblFTWeight");
                Label lblFtTotal = (Label)e.Item.FindControl("lblFtTotal");

                lblFTQty.Text = dqtnty.ToString();
                lblFTWeight.Text = dtotwght.ToString("N2");
                lblFtTotal.Text = NetAmountPrint.ToString("N2");
                if (chkbit == 1) { DivRepFooter.Visible = false; } else { DivRepFooter.Visible = true; }
            }
        }
        protected void ddlTypeI_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTypeI.SelectedValue == "1")
                lblTypeI.Text = "CHA";
            else if (ddlTypeI.SelectedValue == "2")
                lblTypeI.Text = "Forwarder";
            else
                lblTypeI.Text = "Select";

            ddlTypeI.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
        }
        #endregion

        #region Functions...
        private void userpref()
        {
            RateMasterDAL objGrprepDAL = new RateMasterDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            IsWeight = Convert.ToBoolean(userpref.WeightWise_Rate);
        }
        private void BindAgentName()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var Agent = obj.BindAgent();
            obj = null;
            ddlAgent.DataSource = Agent;
            ddlAgent.DataTextField = "Acnt_Name";
            ddlAgent.DataValueField = "Acnt_Idno";
            ddlAgent.DataBind();
            ddlAgent.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "Item_Name", "String",
                "Item_Idno", "String",
                "Unit_Name", "String",
                "Unit_Idno", "String",
                "Rate_Type", "String",
                "Rate_TypeIdno", "String",
                "Quantity", "String",
                "Weight", "String",
                "Rate", "String",
                "Amount", "String"
                );
            return dttemp;
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

        private void populate(Int32 OrderID)
        {
            AdvBookGRDAL obj = new AdvBookGRDAL();
            tblAdvGrOrder objGRHead = obj.SelectTblAdvBookGRHead(OrderID);

            var objGRDetl = obj.SelectGRDetail(OrderID);
            hidOrderIdno.Value = Convert.ToString(OrderID);
            if (objGRHead != null)
            {
                ddlGRType.SelectedValue = Convert.ToString(objGRHead.GRType);
                ddlGRType.Enabled = false;

                ddlDateRange.SelectedValue = Convert.ToString(objGRHead.YearIdno);
                txtOrderDate.Text = Convert.ToDateTime(objGRHead.AdvOrder_Date).ToString("dd-MM-yyyy");
                txtRecDate.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.AdvOrder_Date)) ? "" : Convert.ToDateTime(objGRHead.AdvOrder_Date).ToString("dd-MM-yyyy");
                //TK1
                txtBKGDateFrom.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.BKGDate_From)) ? "" : Convert.ToDateTime(objGRHead.BKGDate_From).ToString("dd-MM-yyyy");
                txtBKGDateTo.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.BKGDate_To)) ? "" : Convert.ToDateTime(objGRHead.BKGDate_To).ToString("dd-MM-yyyy");
                txtOrderNo.Text = Convert.ToString(objGRHead.AdvOrder_No);

                txtReffrnceNumber.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Ref_No)) ? "" : Convert.ToString(objGRHead.Ref_No);
                ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Truck_Idno)) ? "" : Convert.ToString(objGRHead.Truck_Idno);
                ddlParty.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Party_Idno)) ? "" : Convert.ToString(objGRHead.Party_Idno);

                drpBaseCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Loc_Idno)) ? "" : Convert.ToString(objGRHead.Loc_Idno);
                ddlToCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Loc_To)) ? "" : Convert.ToString(objGRHead.Loc_To);
                ddlLocation.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Loc_DelvPlace)) ? "" : Convert.ToString(objGRHead.Loc_DelvPlace);
                ddlviacity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Cityvia_Idno)) ? "" : Convert.ToString(objGRHead.Cityvia_Idno);

                txtconsnr.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Consignor_Name)) ? "" : Convert.ToString(objGRHead.Consignor_Name);
                ddlAgent.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Agent_Idno)) ? "" : Convert.ToString(objGRHead.Agent_Idno);

                txtContainrNo.Text = Convert.ToString(objGRHead.Contanr_No);
                txtContainerSealNo.Text = Convert.ToString(objGRHead.Contanr_SealNo);
                ddlContainerSize.SelectedValue = Convert.ToString(objGRHead.Contanr_Size);
                ddlContainerType.SelectedValue = Convert.ToString(objGRHead.Contanr_Type);

                txtContainrNo2.Text = Convert.ToString(objGRHead.GRContanr_No2);
                txtContainerSealNo2.Text = Convert.ToString(objGRHead.GRContanr_SealNo2);
                txtNameI.Text = Convert.ToString(objGRHead.ChaFrwdr_Name);
                ddlTypeI.SelectedValue = Convert.ToString(objGRHead.ImpExp_idno);
                txtPortNum.Text = Convert.ToString(objGRHead.Port_no);

                TxtRemark.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Remark)) ? "" : Convert.ToString(objGRHead.Remark);
                txtshipment.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Shipment_No)) ? "" : Convert.ToString(objGRHead.Shipment_No);


                ddlTruckNo.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objGRHead.Truck_Idno)) ? "0" : Convert.ToString(objGRHead.Truck_Idno);

                txtGrossAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Gross_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Gross_Amnt);
                txtNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.Net_Amnt)) ? "0" : String.Format("{0:0,0.00}", objGRHead.Net_Amnt);
                TxtRoundOff.Text = string.IsNullOrEmpty(Convert.ToString(objGRHead.RoundOff_Amnt)) ? "0" : string.Format("{0:0,0.00}", objGRHead.RoundOff_Amnt);


                dtTemp = CreateDt();
                for (int counter = 0; counter < objGRDetl.Count; counter++)
                {
                    string strItemName = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Item_Name"));
                    string strItemNameId = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Item_Idno"));
                    string strUnitName = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "UOM_Name"));
                    string strUnitNameId = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Unit_Idno"));
                    string strRateType = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Rate_Type"));
                    string strRateTypeIdno = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "RateType_Idno"));
                    string strQty = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Quantity"));
                    string strWeight = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Item_Weight"));
                    string strRate = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Item_Rate"));
                    string strAmount = Convert.ToString(DataBinder.Eval(objGRDetl[counter], "Item_Amount"));
                    ApplicationFunction.DatatableAddRow(dtTemp, counter + 1, strItemName, strItemNameId, strUnitName, strUnitNameId, strRateType, strRateTypeIdno, strQty, strWeight, strRate, strAmount);
                }
                ViewState["dt"] = dtTemp;
                BindGridT();
                Int32 YearIdno = string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue);
                if (OrderID > 0)
                {
                    Int64 Exist = obj.IfExistsInGr(Convert.ToInt64(OrderID), YearIdno, ApplicationFunction.ConnectionString());
                    if (Exist > 0)
                    {
                        lnkbtnSave.Visible = false;
                        DivSaveButton.Visible = false;
                        foreach (GridViewRow grd in grdMain.Rows)
                        {
                            LinkButton lnkbtnDelete = (LinkButton)grd.FindControl("lnkbtnDelete");
                            LinkButton lnkbtnEdit = (LinkButton)grd.FindControl("lnkbtnEdit");
                            lnkbtnEdit.Visible = false;
                            lnkbtnDelete.Visible = false;
                        }
                    }
                    else
                    {
                        lnkbtnSave.Visible = true;
                    }
                }
                //PrintGRPrep(OrderID);
            }
            obj = null;
        }

        private void PrintGRPrep(Int64 OrderID)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spAdvanceBookGR] @ACTION='SelectPrint',@Id='" + OrderID + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AdvOrder_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["AdvOrder_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);

                lblSenderName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblValueConsign.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ConsigName"]);
                lblRoundOff.Text = dsReport.Tables["GRPrint"].Rows[0]["RoundOff_Amnt"].ToString();
                lblNetAmountrpt.Text = dsReport.Tables["GRPrint"].Rows[0]["Net_Amnt"].ToString();

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
            }
        }

        private void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataSource = lst;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindLocFrom();
            obj = null;
            drpBaseCity.DataSource = lst;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        }

        private void BindDropDown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "Lorry_Idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var senderLst = obj.BindSender();
            ddlParty.DataSource = senderLst;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var ToCity = obj.BindAllToCity();
            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var CityVia = obj.BindAllToCity();
            ddlviacity.DataSource = CityVia;
            ddlviacity.DataTextField = "city_name";
            ddlviacity.DataValueField = "city_idno";
            ddlviacity.DataBind();
            ddlviacity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlLocation.DataSource = ToCity;
            ddlLocation.DataTextField = "city_name";
            ddlLocation.DataValueField = "city_idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var itemname = obj.BindItemName();
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var UnitName = obj.BindUnitName();
            ddlunitname.DataSource = UnitName;
            ddlunitname.DataTextField = "UOM_Name";
            ddlunitname.DataValueField = "UOM_idno";
            ddlunitname.DataBind();
            ddlunitname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        public void BindContainerDetails()
        {
            AdvBookGRDAL obj = new AdvBookGRDAL();
            var varConainerType = obj.GetContainerType();
            ddlContainerType.DataSource = varConainerType;
            ddlContainerType.DataTextField = "Container_Type";
            ddlContainerType.DataValueField = "ContainerType_Idno";
            ddlContainerType.DataBind();
            ddlContainerType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            var varConainerSize = obj.GetContainerSize();
            ddlContainerSize.DataSource = varConainerSize;
            ddlContainerSize.DataTextField = "Container_Size";
            ddlContainerSize.DataValueField = "ContainerSize_Idno";
            ddlContainerSize.DataBind();
            ddlContainerSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void Calculations()
        {
            double iRate = 0; double EditRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtOrderDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (ddlItemName.SelectedIndex > 0)
            {
                if (ddlGRType.SelectedIndex == 1) //In case of TBB
                {
                    if (hidTBBType.Value == "False")
                    {
                        txtrate.Text = "0.00";
                    }
                    else
                    {
                        if ((ddlRateType.SelectedIndex) > 0)
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                iRate = Convert.ToDouble(txtrate.Text);
                                if (txtQuantity.Text.Trim() != "")
                                    dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtQuantity.Text));
                            }
                            else
                            {
                                iRate = Convert.ToDouble(txtrate.Text);
                                if (txtweight.Text.Trim() != "")
                                    dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtweight.Text));
                            }
                        }
                        else
                        {
                            txtrate.Text = "0.00";
                        }
                    }
                }
                else
                {
                    if ((ddlRateType.SelectedIndex) > 0)
                    {
                        if (ddlRateType.SelectedIndex == 1)
                        {
                            iRate = Convert.ToDouble(txtrate.Text);
                            if (txtrate.Text.Trim() != "")
                                dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtQuantity.Text));
                        }
                        else
                        {
                            iRate = Convert.ToDouble(txtrate.Text);
                            if (txtweight.Text.Trim() != "")
                                dtotalAmount = Convert.ToDouble(iRate * Convert.ToDouble(txtweight.Text));
                        }
                    }
                    else
                    {
                        txtrate.Text = "0.00";
                    }
                }
            }
        }

        private void ClearItems()
        {
            hidrowid.Value = string.Empty;
            ddlItemName.SelectedIndex = 0; ddlunitname.SelectedIndex = 0;
            if (IsWeight == true)
                ddlRateType.SelectedIndex = 1;
            else
            {
                ddlRateType.SelectedIndex = 0;
                ddlRateType.Enabled = false;
            }
            txtQuantity.Text = "1"; txtweight.Text = "0.00"; txtrate.Text = "0.00";
        }

        public void clearControls()
        {
            txtContainrNo.Text = txtContainerSealNo.Text = ""; txtshipment.Text = "";
            ddlContainerSize.SelectedValue = ddlContainerType.SelectedValue = "0";
            TxtRemark.Text = txtReffrnceNumber.Text = "";

            txtGrossAmnt.Text = TxtRoundOff.Text = txtNetAmnt.Text = "0.00";
            ddlTruckNo.SelectedIndex =ddlviacity.SelectedIndex= ddlParty.SelectedIndex = ddlLocation.SelectedIndex = ddlToCity.SelectedIndex = drpBaseCity.SelectedIndex = 0;

            hidrowid.Value = string.Empty;
            hidOrderIdno.Value = string.Empty;
            hidTBBType.Value = string.Empty;

            ViewState["dt"] = dtTemp = null;
            grdMain.DataSource = dtTemp;
            grdMain.DataBind();
        }

        private void FillRate()
        {
            AdvBookGRDAL objGrprepDAL = new AdvBookGRDAL();
            double iRate = 0;
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtOrderDate.Text.Trim().ToString()));
            DateTime dtGRDate = strGrDate;
            if (ddlItemName.SelectedIndex > 0)
            {
                if (ddlGRType.SelectedIndex == 1) //In case of Paid
                {
                    if (hidTBBType.Value == "False")
                    {
                        txtrate.Text = "0.00"; txtrate.Enabled = false;
                        if ((ddlRateType.SelectedIndex) > 0)
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                txtrate.Enabled = false; //CompareValidator1.Enabled = false;
                                txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Text = "1"; txtQuantity.Enabled = true;
                            }
                            else
                            {
                                txtweight.Enabled = true; txtQuantity.Text = "0";
                                txtQuantity.Enabled = true; txtrate.Enabled = false; //CompareValidator1.Enabled = false;
                            }
                        }
                        else
                        {
                            txtrate.Text = "0.00"; txtrate.Enabled = false; //CompareValidator1.Enabled = false;
                        }
                    }
                    else
                    {
                        if ((ddlRateType.SelectedIndex) > 0)
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                //CompareValidator1.Enabled = true;
                                iRate = objGrprepDAL.SelectItemRateForTBB(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtrate.Text = iRate.ToString("N2");
                                this.GrRateControl();
                                txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Enabled = true; txtQuantity.Text = "1";

                            }
                            else
                            {
                                //CompareValidator1.Enabled = true;
                                iRate = objGrprepDAL.SelectItemWghtRateForTBB(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtrate.Text = iRate.ToString("N2"); txtweight.Enabled = true; txtQuantity.Text = "0";
                                txtQuantity.Enabled = true; this.GrRateControl();
                            }
                        }
                        else
                        {
                            txtrate.Text = "0.00";
                        }

                    }
                }
                else
                {
                    if ((ddlRateType.SelectedIndex) > 0)
                    {
                        //CompareValidator1.Enabled = true;
                        if (hidTBBType.Value == "False")
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                txtrate.Enabled = false;// CompareValidator1.Enabled = false;
                                txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Enabled = true; txtQuantity.Text = "1";
                                //iRate = objGrprepDAL.SelectItemRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                //txtrate.Text = iRate.ToString("N2"); this.GrRateControl();
                                //txtweight.Text = "0.00"; txtweight.Enabled = false; txtQuantity.Enabled = true;
                            }
                            else
                            {
                                txtweight.Enabled = true; txtQuantity.Text = "0";
                                txtQuantity.Enabled = true; txtrate.Enabled = false; //CompareValidator1.Enabled = false;
                                //iRate = objGrprepDAL.SelectItemWghtRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                //txtrate.Text = iRate.ToString("N2"); txtweight.Enabled = true; txtQuantity.Text = "1";
                                //this.GrRateControl(); txtQuantity.Enabled = false;
                            }
                        }
                        else
                        {
                            if (ddlRateType.SelectedIndex == 1)
                            {
                                iRate = objGrprepDAL.SelectItemRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtrate.Text = iRate.ToString("N2"); this.GrRateControl();
                                txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Enabled = true; txtQuantity.Text = "1";
                            }
                            else
                            {
                                iRate = objGrprepDAL.SelectItemWghtRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), dtGRDate);
                                txtrate.Text = iRate.ToString("N2"); txtweight.Enabled = true; txtQuantity.Text = "0";
                                this.GrRateControl(); txtQuantity.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        txtrate.Text = "0.00";
                    }
                }
            }
        }
        private void FillRateWeightWiseRate()
        {
            DateTime strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtOrderDate.Text.Trim().ToString()));
            AdvBookGRDAL objGrprepDAL = new AdvBookGRDAL();
            if (txtweight.Text.Trim() != "" && Convert.ToDouble(txtweight.Text.Trim())>0.00)
            {
                iRate = objGrprepDAL.SelectItemWeightWiseRate(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(ddlToCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), strGrDate, Convert.ToDecimal(txtweight.Text.Trim()), Convert.ToInt64(ddlParty.SelectedValue));
            }
            txtrate.Text = Convert.ToDouble(iRate > 0 ? iRate : 0.00).ToString("N2");
        }
        private void GrRateControl()
        {
            GRPrepDAL objGrprepDAL = new GRPrepDAL();
            var lststate = objGrprepDAL.GetStateIdno(Convert.ToInt32(drpBaseCity.SelectedValue));
            tblUserPref obj2 = objGrprepDAL.selectuserpref();
            if (Convert.ToInt32(obj2.GRRate) == 1)
            {
                txtrate.Enabled = true;
            }
            else
            {
                txtrate.Enabled = false;
            }
        }
        #endregion

        #region Grid Event ...
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
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["Item_Idno"]);
                    ddlunitname.SelectedValue = Convert.ToString(drs[0]["Unit_Idno"]);
                    ddlRateType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["Rate_TypeIdno"]) == "" ? 0 : drs[0]["Rate_TypeIdno"]);
                    txtQuantity.Text = Convert.ToString(Convert.ToString(drs[0]["Quantity"]) == "" ? 1 : Convert.ToDouble(drs[0]["Quantity"]));
                    txtweight.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Weight"]) == "" ? 0 : Convert.ToDouble(drs[0]["Weight"])));
                    //txtrate.Text = String.Format("{0:0,0.00}", Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])));
                    txtrate.Text = Convert.ToDouble(Convert.ToString(drs[0]["Rate"]) == "" ? 0 : Convert.ToDouble(drs[0]["Rate"])).ToString();

                    hidratetype.Value = Convert.ToString(Convert.ToString(drs[0]["Rate_Type"]) == "" ? 0 : drs[0]["Rate_Type"]);
                    hidrowid.Value = Convert.ToString(drs[0]["id"]);
                    if (ddlItemName.SelectedIndex > 0)
                    {
                        if (ddlGRType.SelectedIndex == 1) //In case of Paid
                        {
                            if ((hidTBBType.Value) == "False") { txtrate.Text = "0.00"; txtrate.Enabled = false; } else { if ((ddlRateType.SelectedIndex) > 0) { if (ddlRateType.SelectedIndex == 1) { this.GrRateControl(); txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Enabled = true; } else { txtweight.Enabled = true; txtQuantity.Enabled = true; this.GrRateControl(); } } else { txtrate.Text = "0.00"; } }
                        }
                        else
                        {
                            if ((ddlRateType.SelectedIndex) > 0) { if (ddlRateType.SelectedIndex == 1) { this.GrRateControl(); txtweight.Text = "0.00"; txtweight.Enabled = true; txtQuantity.Enabled = true; } else { txtweight.Enabled = true; this.GrRateControl(); txtQuantity.Enabled = true; } } else { txtrate.Text = "0.00"; }
                        }
                    }

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
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["Item_Name"], rw["Item_Idno"], rw["Unit_Name"], rw["Unit_Idno"], rw["Rate_Type"],
                                                                rw["Rate_TypeIdno"], rw["Quantity"], rw["Weight"], rw["Rate"], rw["Amount"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
            }
            ddlGRType.Focus();
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (dtTemp == null)
            {
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    iqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "quantity"));
                    totalIqty += iqty;
                    itotWeght = itotWeght + Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Weight")) == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight")));
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                    dtotrate = dtotrate + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                    lblQuantity.Text = totalIqty.ToString("N2");

                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    lblWeight.Text = itotWeght.ToString("N2");

                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = dtotAmnt.ToString("N2");

                    Label lblRate = (Label)e.Row.FindControl("lblRate");
                    lblRate.Text = dtotrate.ToString("N2");

                    txtGrossAmnt.Text = dtotAmnt.ToString("N2");


                    txtNetAmnt.Text = Convert.ToDouble(Math.Round(Convert.ToDouble(txtGrossAmnt.Text))).ToString("N2");
                    TxtRoundOff.Text = (Convert.ToDouble(txtNetAmnt.Text.Trim()) - Convert.ToDouble(txtGrossAmnt.Text.Trim())).ToString("N2");

                }
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

        protected void lnkBtnLast_Click(object sender, EventArgs e)
        {
            if (drpBaseCity.SelectedValue == "0")
            {
                ShowMessageErr("Please Select From City for Last Print.");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
            }
        }
        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
        }
        protected void lnkwithoutamount_Click(object sender, EventArgs e)
        {
            GRPrepDAL objGRDAL = new GRPrepDAL();
            AdvBookGRDAL obj = new AdvBookGRDAL();
            if (Request.QueryString["OrderID"] == null)
            {
                Int64 iMaxGRIdno = obj.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(drpBaseCity.SelectedValue));
                if (iMaxGRIdno > 0)
                {
                    chkbit = 1; PrintGRPrep(iMaxGRIdno);
                    trAmount.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
            else
            {
                chkbit = 1; PrintGRPrep(Convert.ToInt64(Request.QueryString["OrderID"]));
                trAmount.Visible = false; ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
        }
        protected void lnkWithamount_click(object sender, EventArgs e)
        {
            
            GRPrepDAL objGRDAL = new GRPrepDAL();
            AdvBookGRDAL obj = new AdvBookGRDAL();
            if (Request.QueryString["OrderID"] == null)
            {
                Int64 iMaxGRIdno = obj.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(drpBaseCity.SelectedValue));
                if (iMaxGRIdno > 0)
                {
                    chkbit = 2; trAmount.Visible = true; PrintGRPrep(iMaxGRIdno);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                else
                {
                    ShowMessageErr("No Record For Print.");
                }
            }
            else
            {
                chkbit = 2; trAmount.Visible = true; PrintGRPrep(Convert.ToInt64(Request.QueryString["OrderID"]));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
 
            }
        }
    }
}