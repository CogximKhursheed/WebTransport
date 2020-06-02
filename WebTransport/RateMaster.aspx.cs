using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Transactions;

namespace WebTransport
{
    public partial class RateMaster : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; Int32 iFromCity = 0; Int32 totrecords = 0; bool IsWeight = false;
        private int intFormId = 11;
        DataTable dtDelete = new DataTable(); DataRow DrDel;
        #endregion

        #region Page Events...
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
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }

                ViewState["dtDelete"] = CreateDeleteDt();
                this.BindDateRange();
                if (Convert.ToInt32(base.UserDateRng) > 0)
                    ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                //ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                //ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindItems();
                this.BindCity();
                this.BindViaCity();

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
                if (IsWeight == true)
                {
                    txtWeight.Visible = true;
                    rfvWeight.Enabled = true;
                    lblWeight.Visible = true;
                    rfvItemRate.Enabled = false;
                    rfvItemWghtRate.Enabled = true;
                }
                else
                {
                    rfvItemWghtRate.Enabled = false;
                    rfvItemRate.Enabled = true;
                    txtWeight.Visible = false;
                    lblWeight.Visible = false;
                    rfvWeight.Enabled = false;
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
            }
            userpref();
            if (IsWeight == true)
            {
                txtWeight.Visible = true;
                rfvWeight.Enabled = true;
                lblWeight.Visible = true;
                rfvItemRate.Enabled = false;
                rfvItemWghtRate.Enabled = true;
            }
            else
            {
                rfvItemWghtRate.Enabled = true;
                rfvItemRate.Enabled = true;
                txtWeight.Visible = false;
                lblWeight.Visible = false;
                rfvWeight.Enabled = false;
            }
            if (Request.QueryString["RateIdno"] != null)
            {
                Populate(Convert.ToInt32(Request.QueryString["RateIdno"]));
            }
            txtDateRate.Attributes.Add("onkeypress", "return notAllowAnything(event);");

            txtItemRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtItemWighRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtQtyShrLimit.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtQtyShrtgRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtWghtShgLimit.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtWghtShgRate.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtItemRate2.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtItemRate3.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtConWght.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtWeight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
            txtItemRateKM.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

            txtItemRate.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate.ClientID + "')");
            txtWeight.Attributes.Add("onChange", "SetNumFormt('" + txtWeight.ClientID + "')");
            txtItemWighRate.Attributes.Add("onChange", "SetNumFormt('" + txtItemWighRate.ClientID + "')");
            txtQtyShrLimit.Attributes.Add("onChange", "SetNumFormt('" + txtQtyShrLimit.ClientID + "')");
            txtQtyShrtgRate.Attributes.Add("onChange", "SetNumFormt('" + txtQtyShrtgRate.ClientID + "')");
            txtWghtShgLimit.Attributes.Add("onChange", "SetNumFormt('" + txtWghtShgLimit.ClientID + "')");
            txtWghtShgRate.Attributes.Add("onChange", "SetNumFormt('" + txtWghtShgRate.ClientID + "')");
            txtItemRate2.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate2.ClientID + "')");
            txtItemRate3.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate3.ClientID + "')");
            txtConWght.Attributes.Add("onChange", "SetNumFormt('" + txtItemRate3.ClientID + "')");
            HiItemRateType.Value = ddlItemratetYP.SelectedValue;
            HidFrmCityIdno.Value = drpBaseCity.SelectedValue;
            if (HidInvoiceTyp.Value == "IR")
            {
                lbltxt.Text = "Rate Master";
            }
            else if (HidInvoiceTyp.Value == "TBB")
            {
                lbltxt.Text = "Rate Master[TBB]";
            }
            else if (HidInvoiceTyp.Value == "IK")
            {
                lbltxt.Text = "Rate Master[Item Katt]";
            }

            ddlDateRange.Focus();
            RateMasterDAL objuser = new RateMasterDAL();
            tblUserPref objpref = objuser.selectUserPref();
            if (Convert.ToBoolean(objpref.Cont_Rate) == true)
            {
                //lblContSize.Visible = true;
                //drpConSize.Visible = true; lblContWght.Visible = true; txtConWght.Visible = true;
            }
            else
            {
                //lblContSize.Visible = false;
                //drpConSize.Visible = false; lblContWght.Visible = false; txtConWght.Visible = false;
            }
        }
        #endregion

        #region Button Evnets...

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            RateMasterDAL objRateMst = new RateMasterDAL();
            DataTable dt = new DataTable();
            tblUserPref objpref = objRateMst.selectUserPref();
            if (Convert.ToBoolean(objpref.Cont_Rate) == true)
            {
                dt = objRateMst.SelectDBDataExportContWise(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToString(HidInvoiceTyp.Value), ApplicationFunction.ConnectionString());
            }
            else
            {
                dt = objRateMst.SelectDBDataExport(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToString(HidInvoiceTyp.Value), ApplicationFunction.ConnectionString());
            }
            Export(dt);
        }

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            tblRateMast objRGH = new tblRateMast();
            objRGH.Item_Idno = Convert.ToInt32(ddlItemName.SelectedValue);
            objRGH.FrmCity_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
            objRGH.ItemRate_Type = Convert.ToInt32(ddlItemratetYP.SelectedValue);
            RateMasterDAL obj = new RateMasterDAL();

            // For deleting records by lokesh
            int value = 0;
            dtDelete = (DataTable)ViewState["dtDelete"];
            if (dtDelete != null && dtDelete.Rows.Count > 0)
            {
                for (int i = 0; i < dtDelete.Rows.Count; i++)
                {
                    value = obj.DeleteExistingRecord(Convert.ToInt32(dtDelete.Rows[i]["Rate_Idno"].ToString()));

                    if (value < 0)
                    {
                        ShowMessageErr(ddlItemName.SelectedItem.Text.Trim() + " record is already used!");
                    }
                }
            }
            //end

            List<tblRateMast> RgDlst = new List<tblRateMast>();
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp == null || DtTemp.Rows.Count <= 0)
            {
                ShowMessage("Please enter details");
                return;
            }
            bool isinsert = false;

            if (Convert.ToInt32(ddlItemName.SelectedValue) <= 0)
            {
                ShowMessage("Please select Item"); ddlItemName.Focus();
                return;
            }
            else
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    isinsert = obj.Insert(DtTemp, Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToString(HidInvoiceTyp.Value) == "" ? "IR" : Convert.ToString(HidInvoiceTyp.Value), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), empIdno);
                    this.Clear();

                    obj = null;
                    if (isinsert == true)
                    {
                        Tran.Complete();
                        ShowMessage("Record save successfully");
                        if (HidInvoiceTyp.Value == "IR")
                        {
                            lbltxt.Text = "Rate Master";
                        }
                        else if (HidInvoiceTyp.Value == "TBB")
                        {
                            lbltxt.Text = "Rate Master[TBB]";
                        }
                        else if (HidInvoiceTyp.Value == "IK")
                        {
                            lbltxt.Text = "Rate Master[Item Katt]";
                        }
                        Clear();
                    }
                    else
                    {
                        Tran.Dispose();
                        ShowMessage("Record not saved successfully");
                    }
                }
            }
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            ClearItems();
            BindGridDB();
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            if (Hidrowid.Value == string.Empty)
            {
                ClearItems(); BindGridDB();
            }
            else
                EditGridFunction();

        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (IsWeight == false)
            {
                if (ddlItemName.SelectedIndex > 0)
                {
                    if (((string.IsNullOrEmpty(Convert.ToString(txtItemRate.Text.Trim())) ? 0 : Convert.ToDouble(txtItemRate.Text.Trim())) <= 0) && ((string.IsNullOrEmpty(Convert.ToString(txtItemWighRate.Text.Trim())) ? 0 : Convert.ToDouble(txtItemWighRate.Text.Trim())) <= 0) && ((string.IsNullOrEmpty(Convert.ToString(txtItemRateKM.Text.Trim())) ? 0 : Convert.ToDouble(txtItemRateKM.Text.Trim())) <= 0))
                    {
                        this.ShowMessageErr(" Item Rate or Weight or KM wise must be greater than 0!"); txtItemRate.Focus(); txtItemRate.SelectText();
                        return;
                    }
                }
                else
                {
                    this.ShowMessage(" please Select Item"); ddlItemName.Focus();
                    return;
                }
            }
            if (IsWeight == true)
            {
                if (txtWeight.Text.Trim() == "" || Convert.ToDouble(txtWeight.Text.Trim()) <= 0.00)
                {
                    this.ShowMessageErr("Weight must be greater than 0!");
                    return;
                }

                if (txtItemWighRate.Text.Trim() == "" || Convert.ToDouble(txtItemWighRate.Text.Trim()) <= 0.00)
                {
                    this.ShowMessageErr("Rate must be greater than 0!");
                    return;
                }
            }
            if (CheckIsExist(DtTemp = (DataTable)ViewState["dt"]) == true)
                return;

            #region Get Distance Km From to place

            //RateMasterDAL objDal = new RateMasterDAL();
            //tblDistMast ObjDist = new tblDistMast();

            //ObjDist = objDal.SelectDistance(Convert.ToInt64(drpBaseCity.SelectedValue), Convert.ToInt64(ddlCity.SelectedValue));

            //if (ObjDist != null)
            //{
            //    ViewState["KM"] = Convert.ToString(ObjDist.Km);
            //}
            //else { ViewState["KM"] = 0; }

            #endregion

            if (Hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp != null)
                {
                    foreach (DataRow dtrow in DtTemp.Rows)
                    {
                        if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowid.Value))
                        {
                            dtrow["ToCity_Idno"] = ddlCity.SelectedValue;
                            dtrow["City_Name"] = ddlCity.SelectedItem.Text;
                            dtrow["CityVia_Idno"] = ddlCityVia.SelectedValue;
                            dtrow["CityVia_Name"] = ddlCityVia.SelectedItem.Text;
                            dtrow["Rate_Type"] = HidInvoiceTyp.Value;
                            dtrow["Rate_Date"] = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)).ToString();
                            dtrow["Item_Rate"] = txtItemRate.Text;
                            dtrow["Item_WghtRate"] = txtItemWighRate.Text;
                            dtrow["QtyShrtg_Limit"] = txtQtyShrLimit.Text;
                            dtrow["QtyShrtg_Rate"] = txtQtyShrtgRate.Text;
                            dtrow["WghtShrtg_Limit"] = txtWghtShgLimit.Text;
                            dtrow["WghtShrtg_Rate"] = txtWghtShgRate.Text;
                            dtrow["Item_Rate2"] = txtItemRate2.Text;
                            dtrow["Item_Rate3"] = txtItemRate3.Text;
                            dtrow["ItemRateType_Idno"] = ddlItemratetYP.SelectedValue;
                            dtrow["ItemRate_Type"] = ddlItemratetYP.SelectedItem.Text;
                            dtrow["Dist_Km"] = txtKMS.Text;
                            dtrow["DistanceIdno"] = HidDistanceMastId.Value;
                            dtrow["ConSize"] = Convert.ToString(drpConSize.SelectedValue) == "0" ? "0" : drpConSize.SelectedItem.Text;
                            dtrow["ConWeight"] = txtConWght.Text;
                            dtrow["ConSizeID"] = drpConSize.SelectedValue;
                            dtrow["Item_Weight"] = txtWeight.Text == "" ? "0" : txtWeight.Text.Trim();
                            dtrow["IsWeight"] = IsWeight;
                            dtrow["ItemRate_KM"] = (txtItemRateKM.Text.Trim() == "" ? "0" : txtItemRateKM.Text.Trim());
                            lnkbtnSubmit.Focus();
                        }
                    }
                }
            }
            else
            {
                DtTemp = (DataTable)ViewState["dt"];
                //if ((DtTemp != null) && (DtTemp.Rows.Count > 0))
                //{
                //    foreach (DataRow row in DtTemp.Rows)
                //    {
                //        if ((Convert.ToInt32(row["ToCity_Idno"]) == Convert.ToInt32(ddlCity.SelectedValue)) && (Convert.ToInt32(row["CityVia_Idno"]) == Convert.ToInt32(ddlCityVia.SelectedValue)) && (Convert.ToDateTime(row["Rate_Date"]).ToString("dd-MM-yyyy") == Convert.ToString(txtDateRate.Text)))
                //        {
                //            msg = "Same Date,FromCity,Tocity,ViaCity can not be selected!";
                //            txtDateRate.Focus();
                //            this.ShowMessage(msg);
                //            return;
                //        }
                //    }
                //}
                if ((DtTemp == null))
                { DtTemp = CreateDt(); }
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ApplicationFunction.DatatableAddRow(DtTemp, id, ddlCity.SelectedValue, ddlCity.SelectedItem.Text, ddlCityVia.SelectedValue, ddlCityVia.SelectedItem.Text, HidInvoiceTyp.Value, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)), txtItemRate.Text,
                    txtItemWighRate.Text, txtQtyShrLimit.Text, txtQtyShrtgRate.Text, txtWghtShgLimit.Text, txtWghtShgRate.Text, txtItemRate2.Text, txtItemRate3.Text, ddlItemratetYP.SelectedItem.Text, ddlItemratetYP.SelectedValue, txtKMS.Text, HidDistanceMastId.Value, Convert.ToString(drpConSize.SelectedValue) == "0" ? "0" : drpConSize.SelectedItem.Text, txtConWght.Text, drpConSize.SelectedValue, "0", txtWeight.Text.Trim() == "" ? "0" : txtWeight.Text.Trim(), IsWeight, (txtItemRateKM.Text.Trim() == "" ? "0" : txtItemRateKM.Text.Trim()));
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            this.ClearItems();
            txtDateRate.Focus();
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RateMasterList.xls"));
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

        #region Functions...

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

        private void BindGridDB()
        {
            RateMasterDAL objRateMst = new RateMasterDAL();
            DataTable dt1 = new DataTable();
            DataRow Dr;
            DtTemp = CreateDt();
            dtDelete = CreateDeleteDt(); // this is used to store rate_idno for delete..

            dt1 = objRateMst.SelectDBData(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToString(HidInvoiceTyp.Value), Convert.ToInt32(ddlCity.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCity.SelectedValue), ApplicationFunction.ConnectionString());

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Dr = DtTemp.NewRow();
                    Dr.BeginEdit();
                    Dr[0] = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                    Dr[1] = Convert.IsDBNull(dt1.Rows[i]["ToCity_Idno"]) ? "0" : Convert.ToString(dt1.Rows[i]["ToCity_Idno"]);
                    Dr[2] = Convert.IsDBNull(dt1.Rows[i]["City_Name"]) ? "0" : Convert.ToString(dt1.Rows[i]["City_Name"]);
                    Dr[3] = Convert.IsDBNull(dt1.Rows[i]["Cityvia_Idno"]) ? "0" : Convert.ToString(dt1.Rows[i]["Cityvia_Idno"]);
                    Dr[4] = Convert.IsDBNull(dt1.Rows[i]["CityVia_Name"]) ? "0" : Convert.ToString(dt1.Rows[i]["CityVia_Name"]);
                    Dr[5] = Convert.IsDBNull(dt1.Rows[i]["Rate_Type"]) ? "0" : Convert.ToString(dt1.Rows[i]["Rate_Type"]);
                    Dr[6] = Convert.IsDBNull(dt1.Rows[i]["Rate_Date"]) ? "" : Convert.ToDateTime((dt1.Rows[i]["Rate_Date"])).ToString();
                    Dr[7] = Convert.IsDBNull(dt1.Rows[i]["Item_Rate"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Item_Rate"]).ToString("N2");
                    Dr[8] = Convert.IsDBNull(dt1.Rows[i]["Item_WghtRate"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Item_WghtRate"]).ToString("N2");
                    Dr[9] = Convert.IsDBNull(dt1.Rows[i]["QtyShrtg_Limit"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["QtyShrtg_Limit"]).ToString("N2");
                    Dr[10] = Convert.IsDBNull(dt1.Rows[i]["QtyShrtg_Rate"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["QtyShrtg_Rate"]).ToString("N2");
                    Dr[11] = Convert.IsDBNull(dt1.Rows[i]["WghtShrtg_Limit"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["WghtShrtg_Limit"]).ToString("N2");
                    Dr[12] = Convert.IsDBNull(dt1.Rows[i]["WghtShrtg_Rate"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["WghtShrtg_Rate"]).ToString("N2");
                    Dr[13] = Convert.IsDBNull(dt1.Rows[i]["Item_Rate2"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Item_Rate2"]).ToString("N2");
                    Dr[14] = Convert.IsDBNull(dt1.Rows[i]["Item_Rate3"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Item_Rate3"]).ToString("N2");
                    Dr[17] = Convert.IsDBNull(dt1.Rows[i]["Dist_km"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Dist_km"]).ToString();
                    Dr[18] = Convert.IsDBNull(dt1.Rows[i]["DistanceIdno"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["DistanceIdno"]).ToString();
                    Dr[19] = Convert.IsDBNull(dt1.Rows[i]["ConSize"]) ? "0" : Convert.ToString(dt1.Rows[i]["ConSize"]).ToString();
                    Dr[20] = Convert.IsDBNull(dt1.Rows[i]["ConWeight"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["ConWeight"]).ToString();
                    Dr[21] = Convert.IsDBNull(dt1.Rows[i]["ConSizeID"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["ConSizeID"]).ToString();
                    Dr[22] = Convert.ToString(dt1.Rows[i]["Rate_Idno"]).ToString();
                    Dr[23] = Convert.ToString(dt1.Rows[i]["Item_Weight"]).ToString();
                    Dr[24] = Convert.ToString(dt1.Rows[i]["IsWeight"]).ToString();
                    Dr[25] = Convert.ToString(dt1.Rows[i]["ItemRate_KM"]).ToString();
                    //if (ViewState["KM"] != null)
                    //    Dr[17] = ViewState["KM"].ToString();
                    //else
                    //    Dr[17] = "0";

                    string S = "";
                    if (Convert.ToString(Convert.IsDBNull(dt1.Rows[i]["ItemRate_Type"]) ? "" : Convert.ToString(dt1.Rows[i]["ItemRate_Type"])) == "1")
                    {
                        S = "Type-1";
                    }
                    else if ((Convert.IsDBNull(dt1.Rows[i]["ItemRate_Type"]) ? "" : Convert.ToString(dt1.Rows[i]["ItemRate_Type"])) == "2")
                    {
                        S = "Type-2";
                    }
                    else
                    {
                        S = "Type-3";

                    }

                    Dr[15] = S;
                    Dr[16] = Convert.IsDBNull(dt1.Rows[i]["ItemRate_Type"]) ? "0" : Convert.ToString(dt1.Rows[i]["ItemRate_Type"]);

                    Dr.EndEdit();
                    DtTemp.Rows.Add(Dr);

                    // by lokesh for inserting or deleting existing record
                    DrDel = dtDelete.NewRow();
                    DrDel.BeginEdit();
                    DrDel[0] = dtDelete.Rows.Count == 0 ? 1 : dtDelete.Rows.Count + 1;
                    DrDel[1] = Dr[22].ToString();
                    DrDel.EndEdit();
                    dtDelete.Rows.Add(DrDel);
                    // end
                }

                ViewState["dtDelete"] = dtDelete;
                ViewState["dt"] = DtTemp;
                ViewState["newNdt"] = DtTemp;
                BindGrid();
                imgBtnExcel.Visible = true;
            }
            else
            {
                ViewState["dtDelete"] = null;
                ViewState["dt"] = null;
                BindGrid();
                imgBtnExcel.Visible = false;
            }

        }
        private void BindCity()
        {
            RateMasterDAL obj = new RateMasterDAL();
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
            RateMasterDAL obj = new RateMasterDAL();
            var lst = obj.SelectCityCombo();
            obj = null;
            ddlCityVia.DataSource = lst;
            ddlCityVia.DataTextField = "City_Name";
            ddlCityVia.DataValueField = "City_Idno";
            ddlCityVia.DataBind();
            ddlCityVia.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void Populate(int RateIdno)
        {
            //RateMasterDAL obj = new RateMasterDAL();
            //RateMast RatMst = obj.SelectById( RateIdno);
            //var RatemstlList = obj.SelectById(RateIdno);
            //obj = null;

            //if (RatMst != null)
            //{
            //    txtItemRate.Text = Convert.ToString(RatMst.Item_Rate);
            //    txtDateRate.Text = Convert.ToDateTime(RatMst.Rate_Date).ToString("dd-MM-yyyy");
            //    txtItemWighRate.Text = Convert.ToString(RatMst.Item_WghtRate);
            //    ddlCity.SelectedValue = Convert.ToString(RatMst.ToCity_Idno);
            //    ddlItemName.SelectedValue = Convert.ToString(RatMst.Item_Idno);
            //    DtTemp = CreateDt();
            //    int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;

            //    ViewState["dt"] = DtTemp;
            //    foreach (RateMast RGD in RatemstlList)
            //    {
            //        ApplicationFunction.DatatableAddRow(DtTemp, id, RGD.Item_Idno, "", RGD.Item_Rate, RGD.ToCity_Idno,
            //                                      RGD.Item_WghtRate, RGD.Remark);
            //    }
            //    ViewState["dt"] = DtTemp;
            //}
            //this.BindGrid();
        }
        private void ClearAll()
        {

            ddlCity.SelectedValue = "0";
            ddlItemName.SelectedValue = "0";
            txtItemRate.Text = "0.00";
            txtItemRateKM.Text = "0.00";
            txtItemWighRate.Text = "0.00";
            txtQtyShrLimit.Text = "0.00";
            txtQtyShrtgRate.Text = "0.00";
            txtWghtShgLimit.Text = "0.00";
            txtWghtShgRate.Text = "0.00";
            txtItemRate2.Text = "0.00";
            txtItemRate3.Text = "0.00";
            txtConWght.Text = "0.00";
            txtKMS.Text = "";
            ddlItemratetYP.SelectedIndex = 0;

        }
        public void BindItems()
        {
            RateMasterDAL obj = new RateMasterDAL();
            var lst = obj.GetItems();
            ddlItemName.DataSource = lst;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_Idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));

            var varConainerSize = obj.GetContainerSize();
            obj = null;
            drpConSize.DataSource = varConainerSize;
            drpConSize.DataTextField = "Container_Size";
            drpConSize.DataValueField = "ContainerSize_Idno";
            drpConSize.DataBind();
            drpConSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        public bool CheckIsExist(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                if (IsWeight == true)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((Convert.ToInt32(row["ToCity_Idno"]) == Convert.ToInt32(ddlCity.SelectedValue)) && (Convert.ToInt32(row["CityVia_Idno"]) == Convert.ToInt32(ddlCityVia.SelectedValue)) && (Convert.ToDateTime(row["Rate_Date"]).ToString("dd-MM-yyyy") == Convert.ToString(txtDateRate.Text)) && (Convert.ToDouble(row["Item_Weight"])) == Convert.ToDouble(txtWeight.Text.Trim()))
                        {
                            string msg = "Same Date,FromCity,Tocity,ViaCity,Same Weight Already Exist!";
                            txtDateRate.Focus();
                            this.ShowMessageErr(msg);
                            return true;
                        }
                    }
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((Convert.ToInt32(row["ToCity_Idno"]) == Convert.ToInt32(ddlCity.SelectedValue)) && (Convert.ToInt32(row["CityVia_Idno"]) == Convert.ToInt32(ddlCityVia.SelectedValue)) && (Convert.ToDateTime(row["Rate_Date"]).ToString("dd-MM-yyyy") == Convert.ToString(txtDateRate.Text)))
                        {
                            string msg = "Same Date,FromCity,Tocity,ViaCity Already Exist!";
                            txtDateRate.Focus();
                            this.ShowMessageErr(msg);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private DataTable CreateDeleteDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "Rate_Idno", "String");
            return dttemp;
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "id", "String",
                "ToCity_Idno", "String",
                "City_Name", "String",
                "CityVia_Idno", "String",
                "CityVia_Name", "String",
                "Rate_Type", "String",
                "Rate_Date", "String",
                "Item_Rate", "String",
                "Item_WghtRate", "String",
                "QtyShrtg_Limit", "String",
                "QtyShrtg_Rate", "String",
                "WghtShrtg_Limit", "String",
                "WghtShrtg_Rate", "String",
                "Item_Rate2", "String",
                "Item_Rate3", "String",
                "ItemRate_Type", "String",
                "ItemRateType_Idno", "String",
                "Dist_Km", "String",
                "DistanceIdno", "String",
                "ConSize", "String",
                "ConWeight", "String",
                "ConSizeID", "String",
                "Rate_Idno", "String",
                "Item_Weight", "String",
                "IsWeight", "String",
                "ItemRate_KM", "String"
                );
            //DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "ToCity_Idno", "String", "City_Name", "String", "FrmCity_Idno", "String", "City_Name", "String", "Rate_Type", "String", "Rate_Date", "String", "Item_Rate", "String",
            //                                                     "Item_WghtRate", "String", "QtyShrtg_Limit", "String", "QtyShrtg_Rate", "String", "WghtShrtg_Limit", "String", "WghtShrtg_Rate", "String");
            return dttemp;
        }
        private void ClearItems()
        {
            //ddlCity.SelectedValue = "0";
            txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtItemRate.Text = "0.00";
            txtItemRateKM.Text = "0.00";
            txtItemWighRate.Text = "0.00";
            txtQtyShrLimit.Text = "0.00";
            txtQtyShrtgRate.Text = "0.00";
            txtWghtShgLimit.Text = "0.00";
            txtWghtShgRate.Text = "0.00";
            txtItemRate2.Text = "0.00";
            txtItemRate3.Text = "0.00";
            txtConWght.Text = "0.00";
            ddlCityVia.SelectedIndex = ddlCity.SelectedIndex = 0;
            ddlItemratetYP.SelectedIndex = 0;
            Hidrowid.Value = string.Empty;
            ViewState["id"] = null;

        }
        private void userpref()
        {
            RateMasterDAL objGrprepDAL = new RateMasterDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
            IsWeight = Convert.ToBoolean(userpref.WeightWise_Rate);
        }
        private void Clear()
        {
            //drpBaseCity.SelectedValue = "0";
            ddlItemName.SelectedValue = "0";
            //  ViewState["dt"] = null;
            // DtTemp = null;
            hidrateid.Value = string.Empty;
            txtItemRate.Text = "0.00";
            txtItemRateKM.Text = "0.00";
            txtItemWighRate.Text = "0.00";
            ddlCity.SelectedValue = "0";
            ddlCityVia.SelectedValue = "0";
            txtQtyShrLimit.Text = "0.00";
            txtQtyShrtgRate.Text = "0.00";
            txtWghtShgLimit.Text = "0.00";
            txtWghtShgRate.Text = "0.00";
            txtItemRate2.Text = "0.00";
            txtItemRate3.Text = "0.00";
            ddlItemratetYP.SelectedIndex = 0;
            //ddlCity.SelectedIndex = 0;
            txtKMS.Text = "";
            txtWeight.Text = "0.00";
            //  BindGrid();
            grdMain.DataSource = null;
            grdMain.DataBind();
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
        private void DistanceCAlculation(Int32 BaseCity, Int32 ViaCity, Int32 ToCity)
        {
            if (Convert.ToInt32(drpBaseCity.SelectedValue) != 0 && Convert.ToInt32(ddlCity.SelectedValue) != 0)
            {
                using (TransportMandiEntities db = new TransportMandiEntities(clsMultipleDB.strDynamicConString()))
                {
                    var DistInfo = (from n in db.DistanceMasts
                                    where n.FrmCity_Idno == BaseCity && n.ToCity_Idno == ToCity && n.ViaCity_Idno == ViaCity
                                    select new
                                    {
                                        DistanceIdno = n.Distance_Idno,
                                        Kms = n.KMs
                                    }).ToList();

                    if (DistInfo != null && DistInfo.Count > 0)
                    {
                        HidDistanceMastId.Value = Convert.ToString(DataBinder.Eval(DistInfo[0], "DistanceIdno"));
                        txtKMS.Text = Convert.ToString(DataBinder.Eval(DistInfo[0], "Kms"));

                    }
                    else
                    {
                        HidDistanceMastId.Value = "";
                        txtKMS.Text = "";

                    }
                }

            }
        }
        #endregion

        #region Control Events...

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                //RateMasterDAL obj = new RateMasterDAL();
                //var idg = obj.SelectForSearch(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToString(HidInvoiceTyp.Value), Convert.ToInt64(drpBaseCity.SelectedValue));
                //obj = null;
                //if (idg != null)
                //{
                //    //txtDateRate.Text = string.IsNullOrEmpty(idg.Rate_Date) ? "" : idg.Rate_Date;
                //    txtDateRate.Focus();
                //}

                //RateMasterDAL objclsOpeningStock = new RateMasterDAL();
                //DtTemp = null;
                //ViewState["dt"] = null;
                //grdMain.DataSource = DtTemp;
                //grdMain.DataBind();
                BindGridDB();
                txtDateRate.Focus();

            }
            else
            {
                ViewState["dt"] = null;
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
            txtDateRate.Focus();
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            drpBaseCity.Focus();
        }
        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpBaseCity.SelectedIndex > 0)
            {
                ClearAll();
                grdMain.DataSource = null;
                grdMain.DataBind();
                drpBaseCity.SelectedValue = HidFrmCityIdno.Value;
                ddlItemratetYP.SelectedValue = HiItemRateType.Value;
                BindGridDB();
            }
            else if (drpBaseCity.SelectedIndex <= 0)
            {
                ddlItemName.SelectedIndex = 0;
            }
            ddlItemName.Focus();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0 && drpBaseCity.SelectedIndex > 0)
            {
                BindGridDB();
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp != null && DtTemp.Rows.Count > 0)
                {
                    foreach (DataRow row in DtTemp.Rows)
                    {
                        if ((Convert.ToInt32(row["ToCity_Idno"]) == Convert.ToInt32(ddlCity.SelectedValue)) && (Convert.ToDateTime(row["Rate_Date"]).ToString("dd-MM-yyyy") == Convert.ToString(txtDateRate.Text)))
                        {
                            string msg = "Same City And Date Already Exist!";
                            ddlCity.SelectedIndex = 0;

                            ddlCity.Focus();
                            this.ShowMessageErr(msg);
                            return;
                        }
                    }
                }
                txtItemWighRate.Focus();
                RateMasterDAL obj = new RateMasterDAL();
                txtKMS.Text = Convert.ToString(obj.Kms(Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(ddlCityVia.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue)));
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                ShowMessageErr("Please Select From City And Product Name!");
            }
        }
        protected void ddlCityVia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCity.SelectedValue = ddlCityVia.SelectedValue;
            RateMasterDAL obj = new RateMasterDAL();
            txtKMS.Text = Convert.ToString(obj.Kms(Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(ddlCityVia.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue)));
        }

        #endregion

        #region Grid Events...

        private void EditGridFunction()
        {
            int id = 0;
            if (ViewState["id"] != null)
                id = Convert.ToInt32(ViewState["id"].ToString());

            DtTemp = (DataTable)ViewState["dt"];
            DataRow[] drs = DtTemp.Select("Id='" + id + "'");

            if (drs.Length > 0)
            {
                ddlCity.SelectedValue = Convert.ToString(drs[0]["ToCity_Idno"]);
                ddlCityVia.SelectedValue = Convert.ToString(drs[0]["CityVia_Idno"]);
                txtDateRate.Text = Convert.ToDateTime(drs[0]["Rate_Date"]).ToString("dd-MM-yyyy");
                //txtDateRate.Text = Convert.ToString(drs[0]["Rate_Date"]);
                txtItemRate.Text = Convert.ToDouble(drs[0]["Item_Rate"]).ToString("N2");
                txtItemRateKM.Text = Convert.ToDouble(drs[0]["ItemRate_KM"]).ToString("N2");
                txtItemWighRate.Text = Convert.ToDouble(drs[0]["Item_WghtRate"]).ToString("N2");
                txtQtyShrLimit.Text = Convert.ToDouble(drs[0]["QtyShrtg_Limit"]).ToString("N2");
                txtQtyShrtgRate.Text = Convert.ToDouble(drs[0]["QtyShrtg_Rate"]).ToString("N2");
                txtWghtShgLimit.Text = Convert.ToDouble(drs[0]["WghtShrtg_Limit"]).ToString("N2");
                txtWghtShgRate.Text = Convert.ToDouble(drs[0]["WghtShrtg_Rate"]).ToString("N2");
                txtItemRate2.Text = Convert.ToDouble(drs[0]["Item_Rate2"]).ToString("N2");
                txtItemRate3.Text = Convert.ToDouble(drs[0]["Item_Rate3"]).ToString("N2");
                ddlItemratetYP.SelectedValue = Convert.ToString(drs[0]["ItemRateType_Idno"]);
                Hidrowid.Value = Convert.ToString(drs[0]["id"]);
                txtKMS.Text = Convert.ToString(drs[0]["Dist_km"]);
                drpConSize.SelectedValue = Convert.ToString(drs[0]["ConSizeID"]);
                txtConWght.Text = Convert.ToString(drs[0]["ConWeight"]);
                HidDistanceMastId.Value = Convert.ToString(drs[0]["DistanceIdno"]);
                Session["Value"] = Hidrowid.Value;
                txtWeight.Text = Convert.ToDouble(drs[0]["Item_Weight"]).ToString("N2");
            }
            txtDateRate.Focus();
            BindGrid();
        }


        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            if (e.CommandName == "cmdedit")
            {
                EditGridFunction();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataRow drr = DtTemp.Select("id='" + ViewState["id"].ToString() + "'").Single();

                drr.Delete();
                DtTemp.AcceptChanges();
                ViewState["dt"] = DtTemp;
                this.BindGrid();
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < grdMain.Columns.Count; i++)
                {
                    // e.Row.Cells[i].ToolTip = grdMain.Columns[i].HeaderText;
                    if (grdMain.Columns[i].HeaderText == "Action")
                    {
                        e.Row.Cells[i].ToolTip = "Action";
                    }
                    else if (grdMain.Columns[i].HeaderText == "Sr.No.")
                    {
                        e.Row.Cells[i].ToolTip = "Serial No.";
                    }
                    else if (grdMain.Columns[i].HeaderText == "Date")
                    {
                        e.Row.Cells[i].ToolTip = "Rate Date";
                    }
                    else if (grdMain.Columns[i].HeaderText == "ToCity")
                    {
                        e.Row.Cells[i].ToolTip = "To City";
                    }
                    else if (grdMain.Columns[i].HeaderText == "I.Rate")
                    {
                        e.Row.Cells[i].ToolTip = "Rate of Item";
                    }

                    else if (grdMain.Columns[i].HeaderText == "Rate2")
                    {
                        e.Row.Cells[i].ToolTip = "Rate-2 of Item";
                    }
                    else if (grdMain.Columns[i].HeaderText == "Rate3")
                    {
                        e.Row.Cells[i].ToolTip = "Rate-3 of Item";
                    }
                    else if (grdMain.Columns[i].HeaderText == "IR.Type")
                    {
                        e.Row.Cells[i].ToolTip = "Rate Type of Item";
                    }
                    else if (grdMain.Columns[i].HeaderText == "IW.Rate")
                    {
                        e.Row.Cells[i].ToolTip = "Weight Rate of Item";
                    }
                    else if (grdMain.Columns[i].HeaderText == "QS.Limit")
                    {
                        e.Row.Cells[i].ToolTip = "Shortage Limit of Quantity";
                    }
                    else if (grdMain.Columns[i].HeaderText == "QS.Rate")
                    {
                        e.Row.Cells[i].ToolTip = "Shortage Rate of Quantity";
                    }
                    else if (grdMain.Columns[i].HeaderText == "WS.Limit")
                    {
                        e.Row.Cells[i].ToolTip = "Shortage Limit of Weight";
                    }
                    else if (grdMain.Columns[i].HeaderText == "WS.Rate")
                    {
                        e.Row.Cells[i].ToolTip = "Shortage Weight of Rate";
                    }
                    totrecords = grdMain.Rows.Count + 1;

                    // Grid Columns for Hide/Unhide for Container Size and Weight
                    RateMasterDAL objuser = new RateMasterDAL();
                    tblUserPref objpref = objuser.selectUserPref();
                    if (IsWeight == true)
                    {
                        grdMain.HeaderRow.Cells[17].Visible = true;
                        grdMain.Columns[17].Visible = true;
                    }
                    else
                    {
                        grdMain.HeaderRow.Cells[17].Visible = false;
                        grdMain.Columns[17].Visible = false;
                    }

                    if (Convert.ToBoolean(objpref.Cont_Rate) == true)
                    {
                        grdMain.Columns[15].Visible = true; grdMain.Columns[16].Visible = true;
                    }
                    else
                    {
                        grdMain.Columns[15].Visible = false; grdMain.Columns[16].Visible = false;
                    }

                    // Used to hide Delete button if ItemgrpId exists in Quotation,GR Preparation
                    LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    Label lblCityIdno = (Label)e.Row.FindControl("lblCityIdno");

                    string ItemIdno = Convert.ToString(string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : ddlItemName.SelectedValue);
                    string Location = Convert.ToString(string.IsNullOrEmpty(drpBaseCity.SelectedValue) ? "0" : drpBaseCity.SelectedValue);


                    if (ItemIdno != "")
                    {
                        RateMasterDAL obj = new RateMasterDAL();
                        var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(ItemIdno), Convert.ToInt32(Location), Convert.ToInt32(lblCityIdno.Text));
                        if (ItemExist != null && ItemExist.Count > 0)
                        {
                            lnkbtnDelete.Visible = false;
                        }
                        else
                        {
                            lnkbtnDelete.Visible = true;
                        }
                    }
                    // end----
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotRecords = (Label)e.Row.FindControl("lbltotRecords");
                lbltotRecords.Text = Convert.ToString(totrecords);
            }
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        //protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DistanceCAlculation(Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(ddlCityVia.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue));
        //}
    }
}
