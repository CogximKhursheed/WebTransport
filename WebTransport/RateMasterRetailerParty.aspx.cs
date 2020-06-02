using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
namespace WebTransport
{
        public partial class RateMasterRetailerParty : Pagebase
        {
            #region Private Variables...
            DataTable DtTemp = new DataTable(); string con = ""; Int32 iFromCity = 0; Int32 totrecords = 0;
            private int intFormId = 70;
            private bool IsWeight = true;
            DataTable dtDelete = new DataTable(); DataRow DrDel;
            DataTable dtGrid = new DataTable();
            Double TotalRate = 0.00;
            #endregion

            #region "Page Load..."
            protected void Page_Load(object sender, EventArgs e)
            {
                if (Request.UrlReferrer == null)
                {
                    base.AutoRedirect();
                }
                con = ApplicationFunction.ConnectionString();
                if (Request.QueryString["FTyp"] != null)
                {
                    HidInvoiceTyp.Value = Convert.ToString(Request.QueryString["FTyp"]);
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
                    this.BindDateRange();
                    this.BindTranType();
                    ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                    ddlDateRange.SelectedIndex = 0;
                    Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                    //txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    this.BindItems();
                    this.BindCity();
                    this.BindViaCity();
                    this.BindParty();
                    if (Convert.ToString(Session["Userclass"]) == "Admin")
                    {
                        CityMastDAL obj = new CityMastDAL();
                        var lst = obj.SelectCityCombo();
                        obj = null;
                        drpBaseCity.DataSource = lst;
                        drpBaseCity.DataTextField = "City_Name";
                        drpBaseCity.DataValueField = "City_Idno";
                        drpBaseCity.DataBind();
                        drpBaseCity.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    else
                    {
                        this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                    }

                    userpref();
                    //if (IsWeight == true)
                    //{
                    //    txt_Weight.Visible = true;
                    //    rfv_Weight.Enabled = true;
                    //    lblWeight.Visible = true;
                    //}
                    //else
                    //{
                    //    txt_Weight.Visible = false;
                    //    lblWeight.Visible = false;
                    //    rfv_Weight.Enabled = false;
                    //}
                    drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                }


                if (Request.QueryString["RateIdno"] != null)
                {
                    //Populate(Convert.ToInt32(Request.QueryString["RateIdno"]));
                }
                txtDateRate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtItemRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txt_Weight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtItemRate.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate.ClientID + "')");
                txt_Weight.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate.ClientID + "')");
                HidFrmCityIdno.Value = drpBaseCity.SelectedValue;
                ddlDateRange.Focus();
                userpref();
                //if (IsWeight == true)
                //{
                //    txt_Weight.Visible = true;
                //    rfv_Weight.Enabled = true;
                //    lblWeight.Visible = true;
                //}
                //else
                //{
                //    txt_Weight.Visible = false;
                //    lblWeight.Visible = false;
                //    rfv_Weight.Enabled = false;
                //}
            }
            #endregion

            #region Functions...
            private void BindParty()
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                var senderLst = obj.BindSender();
                ddlPartyName.DataSource = senderLst;
                ddlPartyName.DataTextField = "Acnt_Name";
                ddlPartyName.DataValueField = "Acnt_Idno";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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

            private void BindCity()
            {
                RateMasterRetailerDAL obj = new RateMasterRetailerDAL();
                var lst = obj.SelectCityCombo();
                obj = null;
                ddlCity.DataSource = lst;
                ddlCity.DataTextField = "City_Name";
                ddlCity.DataValueField = "City_Idno";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            private void BindViaCity()
            {
                RateMasterRetailerDAL obj = new RateMasterRetailerDAL();
                var lst = obj.SelectCityCombo();
                obj = null;
                ddlCityVia.DataSource = lst;
                ddlCityVia.DataTextField = "City_Name";
                ddlCityVia.DataValueField = "City_Idno";
                ddlCityVia.DataBind();
                ddlCityVia.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            private void ClearAll()
            {
                ddlCity.SelectedValue = "0";
                txtItemRate.Text = "0.00";
                txt_Weight.Text = "0.00";
            }
            private void ClearItems()
            {
                //ddlCity.SelectedValue = "0";
                txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtItemRate.Text = "0.00";

                ddlCityVia.SelectedIndex = ddlCity.SelectedIndex = 0;

                Hidrowid.Value = string.Empty;

            }
            private void userpref()
            {
                RateMasterRetailerDAL objGrprepDAL = new RateMasterRetailerDAL();
                tblUserPref userpref = objGrprepDAL.selectuserpref();
                iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
                //IsWeight = Convert.ToBoolean(userpref.WeightWise_Rate);
            }
            private void Clear()
            {
                hidrateid.Value = string.Empty;
                txtItemRate.Text = "0.00";
                ddlCity.SelectedValue = "0";
                ddlCityVia.SelectedValue = "0";
                drpBaseCity.SelectedValue = "0";
                ddlPartyName.SelectedValue = "0";
                ddlItemName.SelectedValue = "0";
                ddlTranType.SelectedValue = "0";
                txt_Weight.Text = "0.00";
            }

            private void ShowMessage(string msg)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
            }

            private void ShowMessageErr(string msg)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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
            public void BindItems()
            {
                RateMasterRetailerDAL obj = new RateMasterRetailerDAL();
                var lst = obj.GetItems();
                ddlItemName.DataSource = lst;
                ddlItemName.DataTextField = "Item_Name";
                ddlItemName.DataValueField = "Item_Idno";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
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
                        txtDateRate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtDateRate.Text = hidmindate.Value;
                    }
                }

            }

            private void BindGrid()
            {
                Int64 Loc_id = Convert.ToInt64(drpBaseCity.SelectedValue);
                Int64 Item_id = Convert.ToInt64(ddlItemName.SelectedValue);
                Int64 Party_Id = Convert.ToInt64(ddlPartyName.SelectedValue);
                Int64 TranType_Idno = Convert.ToInt64(ddlTranType.SelectedValue);
                RateMasterRetailerDAL obj = new RateMasterRetailerDAL();
                dtGrid = obj.SelectPartyItemRateList(Party_Id, Loc_id, Item_id, TranType_Idno, ApplicationFunction.ConnectionString());
                if (dtGrid != null)
                {

                    GridDiv.Visible = true;
                    grdMain.DataSource = dtGrid;
                    grdMain.DataBind();
                    if (IsWeight == false)
                    {
                        //grdMain.HeaderRow.Cells[5].Visible = false;
                        grdMain.Columns[5].Visible = false;
                    }
                    else
                    {
                        //grdMain.HeaderRow.Cells[5].Visible = true;
                        grdMain.Columns[5].Visible = true;
                    }
                    //hidSaveType.Value = "0";
                }
            }

            private void BindTranType()
            {
                BindDropdownDAL obj = new BindDropdownDAL();
                ddlTranType.DataSource = obj.BindTranType();
                ddlTranType.DataTextField = "Tran_Type";
                ddlTranType.DataValueField = "TranType_Idno";
                ddlTranType.DataBind();                
            }

            private void Populate(Int64 PRateID)
            {
                RateMasterRetailerDAL obj = new RateMasterRetailerDAL();
                DataSet dt = obj.SelectPartyItemRate(PRateID, ApplicationFunction.ConnectionString());
                try
                {
                    lnkbtnNew.Visible = true;
                    if (dt != null && dt.Tables[0].Rows.Count > 0)
                    {
                        ddlCity.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["ToCity_Idno"]);
                        ddlCityVia.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["FromCityId"]);
                        drpBaseCity.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["Loc_Idno"]);
                        txtItemRate.Text = Convert.ToString(dt.Tables[0].Rows[0]["Item_Rate"]);
                        txtDateRate.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["Rate_Date"]).ToString("dd-MM-yyyy");
                        ddlPartyName.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["Party_Idno"]);
                        ddlItemName.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["Item_Idno"]);
                        txt_Weight.Text = Convert.ToString(dt.Tables[0].Rows[0]["Item_Weight"]);
                        bool Is_Weight = Convert.ToBoolean(dt.Tables[0].Rows[0]["IsWeight"]);
                        ddlDateRange.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["Year_Idno"]);
                        ddlTranType.SelectedValue = Convert.ToString(dt.Tables[0].Rows[0]["TranType_Idno"]);
                        if (Is_Weight == true)
                        {
                            txt_Weight.Visible = true;
                            rfv_Weight.Enabled = true;
                            lblWeight.Visible = true;
                        }
                        else
                        {
                            txt_Weight.Visible = false;
                            lblWeight.Visible = false;
                            rfv_Weight.Enabled = false;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            private void CalculateTotal(DataTable dt)
            {
                TotalRate = Convert.ToDouble(dt.Compute("Sum(Item_Rate)", ""));
            }
            #endregion

            #region Control Events...

            protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ddlItemName.SelectedIndex > 0 && drpBaseCity.SelectedIndex > 0 && ddlPartyName.SelectedIndex > 0 && ddlTranType.SelectedIndex > 0)
                {
                    BindGrid();
                    //txtDateRate.Focus();
                }
                else
                {
                    GridDiv.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                }

            }
            protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ddlItemName.SelectedIndex > 0 && drpBaseCity.SelectedIndex > 0 && ddlPartyName.SelectedIndex > 0 && ddlTranType.SelectedIndex > 0)
                {
                    BindGrid();
                    //txtDateRate.Focus();
                }
                else
                {
                    GridDiv.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                }

            }
            protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ddlItemName.SelectedIndex > 0 && drpBaseCity.SelectedIndex > 0 && ddlPartyName.SelectedIndex > 0 && ddlTranType.SelectedIndex > 0)
                {
                    BindGrid();
                    //txtDateRate.Focus();
                }
                else
                {
                    GridDiv.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                }

            }
            protected void ddlCityVia_SelectedIndexChanged(object sender, EventArgs e)
            {
                ddlCity.SelectedValue = ddlCityVia.SelectedValue;

            }
            protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ddlItemName.SelectedIndex > 0 && drpBaseCity.SelectedIndex > 0 && ddlPartyName.SelectedIndex > 0 && ddlTranType.SelectedIndex > 0)
                {
                    BindGrid();
                }
                else
                {
                    GridDiv.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                }
            }

            #endregion

            #region Grid Events...


            protected void grdMain_RowCommand1(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "cmdedit")
                {
                    Populate(Convert.ToInt64(e.CommandArgument));
                    hidSaveType.Value = Convert.ToString(e.CommandArgument);
                }
                else if (e.CommandName == "cmddelete")
                {
                    RateMasterRetailerDAL RM = new RateMasterRetailerDAL();
                    int value = RM.DeletePartyItemRate(Convert.ToInt64(e.CommandArgument));
                    if (value == 1)
                    {
                        BindGrid();
                        Clear();
                        ShowMessage("Record Deleted Successfully");
                    }
                    else
                    {
                        Clear();
                        BindGrid();
                        ShowMessage("Record Not Deleted!");
                    }

                }
            }

            protected void grdMain_PageIndexChanging1(object sender, GridViewPageEventArgs e)
            {
                grdMain.PageIndex = e.NewPageIndex;
                BindGrid();
            }

            protected void grdMain_RowDataBound1(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < grdMain.Columns.Count; i++)
                    {
                        if (IsWeight == true)
                        {
                            grdMain.HeaderRow.Cells[6].Visible = true;
                            grdMain.Columns[6].Visible = true;
                        }
                        else
                        {
                            grdMain.HeaderRow.Cells[6].Visible = false;
                            grdMain.Columns[6].Visible = false;
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (dtGrid != null)
                        CalculateTotal(dtGrid);
                    Label lblTotOpening = (Label)e.Row.FindControl("lblItemTotalRate");
                    //lblTotOpening.Text = string.Format("{0:0,0.00}", TotalRate.ToString());
                    lblTotOpening.Text = String.Format("{0:0,0.00}", Convert.ToDouble(TotalRate));
                }
            }
            public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
            {
                /* Verifies that the control is rendered */
            }           
            #endregion

            #region Button Event...
            protected void lnkbtnSave_Click(object sender, EventArgs e)
            {
                if (drpBaseCity.SelectedValue == "0")
                {
                    ShowMessageErr("Please Select Location!");
                    return;
                }
                if (ddlPartyName.SelectedValue == "0")
                {
                    ShowMessageErr("Please Select Party!");
                    return;
                }
                if (ddlItemName.SelectedValue == "0")
                {
                    ShowMessageErr("Please Select Item!");
                    return;
                }
                if (ddlCityVia.SelectedValue == "0")
                {
                    ShowMessageErr("Please Select To Via City!");
                    return;
                }
                if (ddlCity.SelectedValue == "0")
                {
                    ShowMessageErr("Please Select To City!");
                    return;
                }
                if (Convert.ToDouble(txt_Weight.Text) <= 0.00 && IsWeight == true)
                {
                    ShowMessageErr("Please Enter Weight More than 0!");
                    return;
                }
                Int64 empIdno = Convert.ToInt64((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                tblPartyRateRetailerMast obj = new tblPartyRateRetailerMast();
                obj.Loc_Idno = Convert.ToInt64(drpBaseCity.SelectedValue);
                obj.Party_Idno = Convert.ToInt64(ddlPartyName.SelectedValue);
                obj.Item_Idno = Convert.ToInt64(ddlItemName.SelectedValue);
                obj.Rate_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text.Trim()).ToString());
                obj.FrmCity_Idno = Convert.ToInt64(ddlCityVia.SelectedValue);
                obj.ToCity_Idno = Convert.ToInt64(ddlCity.SelectedValue);
                obj.Item_Rate = Convert.ToDecimal(txtItemRate.Text.Trim());
                obj.Emp_Idno = empIdno;
                obj.Date_Added = Convert.ToDateTime(DateTime.Now);
                obj.Date_Modified = Convert.ToDateTime(DateTime.Now);
                obj.Status = true;
                obj.Year_idno = Convert.ToInt32(ddlDateRange.SelectedValue);
                obj.TranType_Idno = Convert.ToInt64(ddlTranType.SelectedValue);
                obj.Tran_Type = Convert.ToString(ddlTranType.SelectedItem.Text);
                RateMasterRetailerDAL RM = new RateMasterRetailerDAL();
                if (IsWeight == false)
                {// For Without Weight Wise
                    obj.Item_Weight = 0;
                    obj.IsWeight = false;
                }
                else
                {
                    obj.Item_Weight = Convert.ToDecimal(txt_Weight.Text);
                    obj.IsWeight = true;
                }
                if ((hidSaveType.Value != "") && (Convert.ToInt64(hidSaveType.Value)) > 0)
                {
                    int value = RM.UpdatePartyItemRate(obj, Convert.ToInt64(Convert.ToInt64(hidSaveType.Value)));
                    if (value == 1)
                    {
                        BindGrid();
                        Clear();
                        ShowMessage("Record Updated Successfully");
                    }
                    else if (value == 2)
                    {
                        BindGrid();
                        ShowMessageErr("Record Already Exists!");
                    }
                    else
                    {
                        BindGrid();
                        ShowMessageErr("Record Not Updated!");
                    }
                }

                else
                {
                    int value = RM.InsertPartyItemRate(obj);
                    if (value == 1)
                    {
                        BindGrid();
                        Clear();
                        ShowMessage("Record Inserted Successfully");
                    }
                    else if (value == 2)
                    {
                        BindGrid();
                        Clear();
                        ShowMessageErr("Record Already Exists!");
                    }
                    else
                    {
                        BindGrid();
                        Clear();
                        ShowMessageErr("Record Not Inserted!");
                    }
                }
            }
            protected void lnkbtnCancel_Click(object sender, EventArgs e)
            {
                if ((hidSaveType.Value != "") && (Convert.ToInt64(hidSaveType.Value)) > 0)
                {
                    Populate(Convert.ToInt64(hidSaveType.Value));
                }
                else
                {
                    Clear();
                }
            }
            protected void lnkbtnNew_OnClick(object sender, EventArgs e)
            {
                lnkbtnNew.Visible = false;
                Response.Redirect("~/RateMasterRetailerParty.aspx");
            }
            #endregion           
            
        }
    }
