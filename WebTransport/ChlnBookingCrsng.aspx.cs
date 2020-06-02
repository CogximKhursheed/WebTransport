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
    public partial class ChlnBookingCrsng : Pagebase
    {
        #region Variable ...
        static FinYearA UFinYear = new FinYearA();
        // string con = ApplicationFunction.ConnectionString();
        DataTable dtTemp = new DataTable();
        DataTable AcntDS = new DataTable();
        DataTable DsTrAcnt = new DataTable();
        double dblTtAmnt = 0;
        int rb = 0;
        Int32 iFromCity = 0, itruckcitywise = 0, iGrAgainst = 0;
        Int64 RcptGoodHeadIdno = 0;
        private int intFormId = 30;
        string strSQL = "", sRenWages = "";
        bool isTBBRate = false;
        double dSurchgPer = 0, dWagsAmnt = 0, dBiltyAmnt = 0, dTolltax = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dtotalAmount = 0;
        double totalIqty = 0;
        double itotWeght = 0;
        Int32 iCityIdno = 0;
        Int32 iBaseCityIdno = 0;
        double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;
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
            if (base.View == false)
            {
                lblViewList.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                this.ValidateControls();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                
                //if (Convert.ToString(Session["Userclass"]) == "Admin")
                //{
                //    BindDropdownDAL obja = new BindDropdownDAL();
                //    var senderLst = obja.BindSender();
                //    ddlSenderName.DataSource = senderLst;
                //    ddlSenderName.DataTextField = "Acnt_Name";
                //    ddlSenderName.DataValueField = "Acnt_Idno";
                //    ddlSenderName.DataBind();
                //    ddlSenderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                //}
                //else
                //{
                //    this.BindDropdown();
                //}
                this.BindDateRange();
                this.BindDropdown();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlSenderName.SelectedValue = Convert.ToString(base.Sender);
                DdlfromcityHead.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindMaxNo(Convert.ToInt32((DdlfromcityHead.SelectedValue) == "" ? 0 : Convert.ToInt32(DdlfromcityHead.SelectedValue)), Convert.ToInt32(ddldateRange.SelectedValue));
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                ChlnBookingCrsngDAL objChlnBookingDAL = new ChlnBookingCrsngDAL();
                tblUserPref obj = objChlnBookingDAL.selectUserPref();
                if (obj != null)
                {
                    hidWorkType.Value = Convert.ToString(obj.Work_Type);
                }

                if (Request.QueryString["q"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidid.Value = Convert.ToString(Request.QueryString["q"]);
                    lnkbtnMnNew.Visible = true;
                }
                else
                {
                    lnkbtnMnNew.Visible = false;
                }
                txtKattAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

                this.ddldateRange_SelectedIndexChanged(null, null);
            }
        }
        #endregion

        #region Functions
        private void BindMaxNo(Int32 FromCityIdno, Int32 YearId)
        {
            ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
            Int64 MaxNo = obj.MaxNo(YearId, FromCityIdno, ApplicationFunction.ConnectionString());
            txtchallanNo.Text = Convert.ToString(MaxNo);
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "GR_No", "String",
                "GR_Date", "String",
                "Gr_Type", "String",
                "Gr_TypeIdno", "String",
                "FromCity", "String",
                "FromCityIdno", "String",
                "ToCity", "String",
                "ToCityIdno", "String",
                "SenderName", "String",
                "SenderNameIdno", "String",
                "ReciverName", "String",
                "ReciverNameIdno", "String",
                "Qty", "String",
                "Weight", "String",
                "Amount", "String",
                "Detail", "String"
                );
            return dttemp;
        }
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            var receiverLst = obj.BindSender();
            var TruckNolst = obj.BindTruckNo();
            var ToCity = obj.BindAllToCity();
            var Agent = obj.BindAgent();
            var bank = obj.BindBank();
            var itemname = obj.BindItemName();
            var UnitName = obj.BindUnitName();
            var Transpoter = obj.BindTranspoter();
            obj = null;

            ddlSenderName.DataSource = senderLst;
            ddlSenderName.DataTextField = "Acnt_Name";
            ddlSenderName.DataValueField = "Acnt_Idno";
            ddlSenderName.DataBind();
            ddlSenderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlReciverName.DataSource = receiverLst;
            ddlReciverName.DataTextField = "acnt_name";
            ddlReciverName.DataValueField = "acnt_idno";
            ddlReciverName.DataBind();
            ddlReciverName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlTruckNo.DataSource = TruckNolst;
            ddlTruckNo.DataTextField = "Lorry_No";
            ddlTruckNo.DataValueField = "lorry_idno";
            ddlTruckNo.DataBind();
            ddlTruckNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddltoCity.DataSource = ToCity;
            ddltoCity.DataTextField = "city_name";
            ddltoCity.DataValueField = "city_idno";
            ddltoCity.DataBind();
            ddltoCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlfromCity.DataSource = ToCity;
            ddlfromCity.DataTextField = "city_name";
            ddlfromCity.DataValueField = "city_idno";
            ddlfromCity.DataBind();
            ddlfromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlTranspoter.DataSource = Transpoter;
            ddlTranspoter.DataTextField = "Acnt_Name";
            ddlTranspoter.DataValueField = "Acnt_Idno";
            ddlTranspoter.DataBind();
            ddlTranspoter.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddldateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
            objDAL = null;
        }
        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToFrom = obj.BindLocFrom();
            DdlfromcityHead.DataSource = ToFrom;
            DdlfromcityHead.DataTextField = "City_Name";
            DdlfromcityHead.DataValueField = "City_Idno";
            DdlfromcityHead.DataBind();
            obj = null;
            DdlfromcityHead.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindCityUserWise(UserIdno);
            DdlfromcityHead.DataSource = ToCity;
            DdlfromcityHead.DataTextField = "CityName";
            DdlfromcityHead.DataValueField = "CityIdno";
            DdlfromcityHead.DataBind();
            obj = null;
            DdlfromcityHead.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddldateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtDate.Text = hidmindate.Value;

                }
            }

        }
        private void ValidateControls()
        {
            txtGRDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtGrNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtQuantity.Text = "1";
            txtQuantity.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtweight.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtchlnRef.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
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
        private void ClearItems()
        {
            Hidrowid.Value = "";
            txtGrNo.Text = "";
            ddlGrType.SelectedIndex = 0; ddlfromCity.SelectedIndex = 0;
            ddltoCity.SelectedIndex = 0;
            ddlSenderName.SelectedIndex = 0;
            ddlReciverName.SelectedIndex = 0;
            txtQuantity.Text = "1";
            txtweight.Text = "1";
            txtAmount.Text = "0.00";
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void RcptAmtTot(DataTable dtTemp)
        {
            try
            {
                int c = 0, itotRow = 0;
                double dtotAmnt = 0;
                double dtNetamnt = 0;
                c = grdMain.Rows.Count;
                if (c > 0)
                {
                    for (int i = 0; i < c; i++)
                    {
                        dtotAmnt = Convert.ToDouble(dtotAmnt + Convert.ToDouble(dtTemp.Rows[i]["Amount"]));
                    }
                }
                itotRow = grdMain.Rows.Count;

                txtgrossAmnt.Text = dtotAmnt.ToString("N2");
                txtKattAmnt_TextChanged(null, null);
                //netamntcal();
            }
            catch (Exception Ex)
            { }
        }
        private void Populate(Int64 HeadId)
        {
            ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
            tblChlnBookHead chlnBookhead = obj.selectHead(HeadId);
            ddldateRange.SelectedValue = Convert.ToString(chlnBookhead.Year_Idno);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtchallanNo.Text = chlnBookhead.Chln_No;
            txtDate.Text = Convert.ToDateTime(chlnBookhead.Chln_Date).ToString("dd-MM-yyyy");
            ddlTranspoter.SelectedValue = Convert.ToString(chlnBookhead.Transprtr_Idno);
            txtchlnRef.Text = Convert.ToString(chlnBookhead.Ref_No);
            ddlTruckNo.SelectedValue = Convert.ToString(chlnBookhead.Truck_Idno);
            ddlTruckNo_SelectedIndexChanged(null, null);
            txtgrossAmnt.Text = Convert.ToDouble(chlnBookhead.Gross_Amnt).ToString("N2");
            txtKattAmnt.Text = Convert.ToDouble(chlnBookhead.Other_Amnt).ToString("N2");
            txtNetAmnt.Text = Convert.ToDouble(chlnBookhead.Net_Amnt).ToString("N2");
            hidWorkType.Value = Convert.ToString(chlnBookhead.Work_type);
            DdlfromcityHead.SelectedValue = Convert.ToString(chlnBookhead.BaseCity_Idno);
            dtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId);
            ViewState["dt"] = dtTemp;
            this.BindGridT();
            //  imgSearch.Enabled = false;
            Int64 value = 0;
            value = obj.CheckBilled(HeadId, ApplicationFunction.ConnectionString());
            if (value > 0)
            {
                lnkbtnSave.Enabled = false;

            }
            else
            {
                lnkbtnSave.Enabled = true;
            }
            obj = null;
        }

        private void Clear()
        {

            ViewState["dt"] = null;
            dtTemp = null;
            hidid.Value = string.Empty; hidOwnerId.Value = string.Empty;

            ddlTruckNo.SelectedValue = "0";
            txtOwnrNme.Text = "";
            txtDate.Text = "";
            txtchallanNo.Text = "";
            ddldateRange.SelectedIndex = 0; ;

            BindGridT();

            ddldateRange.Enabled = true;
            ddldateRange.SelectedIndex = 0;

            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
            tblUserPref obj = objChlnBookingDAL.selectUserPref();
            if (obj != null)
            {
                hidWorkType.Value = Convert.ToString(obj.Work_Type);
            }
            lnkbtnSave.Enabled = true;

        }

        #endregion

        #region Grid Events
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    txtGrNo.Text = Convert.ToString(drs[0]["GR_No"]);
                    txtGRDate.Text = Convert.ToDateTime(drs[0]["GR_Date"]).ToString("dd-MM-yyyy");
                    ddlGrType.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["Gr_TypeIdno"]) == "" ? 0 : drs[0]["Gr_TypeIdno"]);
                    ddlfromCity.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["FromCityIdno"]) == "" ? 1 : Convert.ToInt64(drs[0]["FromCityIdno"]));
                    ddltoCity.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["ToCityIdno"]) == "" ? 1 : Convert.ToInt64(drs[0]["ToCityIdno"]));
                    ddlSenderName.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["SenderNameIdno"]) == "" ? 1 : Convert.ToInt64(drs[0]["SenderNameIdno"]));
                    ddlReciverName.SelectedValue = Convert.ToString(Convert.ToString(drs[0]["ReciverNameIdno"]) == "" ? 1 : Convert.ToInt64(drs[0]["ReciverNameIdno"]));
                    txtQuantity.Text = Convert.ToString(drs[0]["Qty"]);
                    txtweight.Text = Convert.ToString(drs[0]["Weight"]);
                    txtAmount.Text = String.Format("{0:0,0.00}", Convert.ToDouble(drs[0]["Amount"]));
                    txtdetail.Text = Convert.ToString(drs[0]["Detail"]);
                    Hidrowid.Value = Convert.ToString(drs[0]["id"]);
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
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["GR_No"], rw["Gr_Date"], rw["Gr_Type"], rw["Gr_TypeIdno"], rw["FromCity"], rw["FromCityIdno"], rw["ToCity"], rw["ToCityIdno"], rw["SenderName"], rw["SenderNameIdno"],
                                                               rw["ReciverName"], rw["ReciverNameIdno"], rw["Qty"], rw["Weight"], rw["Amount"], rw["Detail"]);
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
                    iqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                    totalIqty += iqty;
                    itotWeght = itotWeght + Convert.ToDouble(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Weight")) == "" ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Weight")));
                    dtotAmnt = dtotAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblQuantity = (Label)e.Row.FindControl("lblQty");
                    lblQuantity.Text = totalIqty.ToString("N2");

                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    lblWeight.Text = itotWeght.ToString("N2");

                    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                    lblAmount.Text = dtotAmnt.ToString("N2");



                }

            }
        }
        protected void DdlfromcityHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChlnBookingCrsngDAL objGR = new ChlnBookingCrsngDAL();
                Int64 MaxCRNo = 0;
                MaxCRNo = objGR.MaxNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(Convert.ToString(DdlfromcityHead.SelectedValue) == "" ? 0 : Convert.ToInt32(DdlfromcityHead.SelectedValue)), ApplicationFunction.ConnectionString());
                txtchallanNo.Text = Convert.ToString(MaxCRNo);
                DdlfromcityHead.Focus(); txtchallanNo.SelectText();

                return;
            }
            catch (Exception Ex)
            {

            }
        }
        #endregion

        #region Textbox & Dropdowls Events
        protected void txtKattAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtKattAmnt.Text == "")
            {
                double grossAmnt = Convert.ToDouble(txtgrossAmnt.Text);
                txtKattAmnt.Text = "0.00";
                txtNetAmnt.Text = (grossAmnt).ToString("N2");
            }
            else
            {
                double grossAmnt = Convert.ToDouble(txtgrossAmnt.Text);
                double KattAmnt = Convert.ToDouble(txtKattAmnt.Text.Trim());
                if (grossAmnt > KattAmnt)
                {
                    txtNetAmnt.Text = (grossAmnt - KattAmnt).ToString("N2");
                }
                else
                {
                    txtKattAmnt.Text = "0.00";
                    txtNetAmnt.Text = (grossAmnt).ToString("N2");
                }
            }

        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex >= 0)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddlTruckNo.SelectedIndex >= 0))
                {
                    ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
                    obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    var lst = obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    if (lst != null)
                    {
                        txtOwnrNme.Text = Convert.ToString(lst.Owner_Name + '-' + ((lst.Pan_No == null) ? "" : lst.Pan_No) + "-" + ((lst.Lorry_Type == 0) ? "O" : "H"));
                        hidOwnerId.Value = Convert.ToString(lst.Prty_Idno);
                    }
                }
                else
                {
                    txtOwnrNme.Text = "";
                }

                ddlTruckNo.Focus();
            }
            catch (Exception Ex)
            {

            }
        }
        #endregion

        #region Button ClickEvent
        protected void lnkbtnMnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ChlnBookingCrsng.aspx");
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            #region Validation Messages for Challan Details

            if (ddlTranspoter.SelectedIndex == 0) { this.ShowMessage("Please select Transporter."); ddlTranspoter.Focus(); return; }
            if (ddlTruckNo.SelectedIndex == 0) { this.ShowMessage("Please select Truck No."); ddlTruckNo.Focus(); return; }
            if (txtchlnRef.Text == "") { this.ShowMessage("Please Enter Challan Ref.No ."); ddlTruckNo.Focus(); return; }

            string msg = "";
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

            #region Declare Input Variables for Challan Details
            //string strMsg = string.Empty;
            //Int64 intGrPrepIdno = 0;
            //Int32 YearIdno = Convert.ToInt32(ddldateRange.SelectedValue) == -1 ? 0 : Convert.ToInt32(ddldateRange.SelectedValue);
            //DateTime strChallanDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text.Trim().ToString()));
            //Int64 intChallanNo = string.IsNullOrEmpty(txtchallanNo.Text.Trim()) ? 0 : Convert.ToInt64(txtchallanNo.Text.Trim());
            //Int32 intTranspoter = string.IsNullOrEmpty(ddlTranspoter.SelectedValue) ? 0 : Convert.ToInt32(ddlTranspoter.SelectedValue);
            //Int32 TruckNoIdno = string.IsNullOrEmpty(ddlTruckNo.SelectedValue) ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue);
            //Int32 strChlnRefno = string.IsNullOrEmpty(txtchlnRef.Text) ? 0 : Convert.ToInt32(txtchlnRef.Text);

            //DateTime? dtInstDate;
            //if (txtInstDate.Text == "")
            //{
            //    dtInstDate = null;

            //}
            //else
            //{
            //    dtInstDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtInstDate.Text));
            //}

            #endregion

            #region Insert/Update with Transaction
            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            {
                string intGrPrepIdno = "";
                string ChlnNo = txtchallanNo.Text;
                Int32 TrunckIdno = Convert.ToInt32(ddlTruckNo.SelectedValue);
                Int32 YearIdno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
                GRPrepDAL objDAL = new GRPrepDAL();
                isTBBRate = objDAL.SelectTBBRate();
                tblUserPref userpref = objDAL.selectuserpref();
                itruckcitywise = Convert.ToInt32(userpref.Work_Type);

                DataTable dtDetail = (DataTable)ViewState["dt"];
                try
                {

                    ChlnBookingCrsngDAL obj = new ChlnBookingCrsngDAL();
                    tblChlnBookHead objtblChlnBookHead = new tblChlnBookHead();
                    objtblChlnBookHead.Chln_No = txtchallanNo.Text;
                    objtblChlnBookHead.Chln_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                    objtblChlnBookHead.BaseCity_Idno = Convert.ToInt32((DdlfromcityHead.SelectedIndex <= 0) ? "0" : DdlfromcityHead.SelectedValue);
                    objtblChlnBookHead.DelvryPlc_Idno = Convert.ToInt32(0);
                    objtblChlnBookHead.Truck_Idno = Convert.ToInt32((ddlTruckNo.SelectedIndex <= 0) ? "0" : ddlTruckNo.SelectedValue);
                    objtblChlnBookHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);

                    objtblChlnBookHead.Driver_Idno = Convert.ToInt32(1);
                    objtblChlnBookHead.Delvry_Instrc = "";

                    objtblChlnBookHead.Inv_Idno = 0;
                    objtblChlnBookHead.Gross_Amnt = Convert.ToDouble(txtgrossAmnt.Text);
                    objtblChlnBookHead.Commsn_Amnt = Convert.ToDouble(0.00);
                    objtblChlnBookHead.Transprtr_Idno = Convert.ToInt32(ddlTranspoter.SelectedValue);
                    objtblChlnBookHead.Chln_type = 2;
                    objtblChlnBookHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                    objtblChlnBookHead.Other_Amnt = Convert.ToDouble(txtKattAmnt.Text);
                    objtblChlnBookHead.Work_type = Convert.ToInt32(hidWorkType.Value);
                    objtblChlnBookHead.Ref_No = Convert.ToString(txtchlnRef.Text);
                    objtblChlnBookHead.Adv_Amnt = Convert.ToDouble(0.00);
                    objtblChlnBookHead.RcptType_Idno = Convert.ToInt32(1);
                    objtblChlnBookHead.Bank_Idno = Convert.ToInt32(0);
                    objtblChlnBookHead.Inst_No = Convert.ToInt32(0);
                    objtblChlnBookHead.Inst_Dt = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                    objtblChlnBookHead.Date_Added = Convert.ToDateTime(DateTime.Now);
                    Int64 value = 0;
                    if (string.IsNullOrEmpty(hidid.Value) == true)
                    {
                        if (grdMain.Rows.Count > 0 && dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            ChlnBookingCrsngDAL obj1 = new ChlnBookingCrsngDAL();
                            intGrPrepIdno = obj1.InsertGR(ChlnNo, TrunckIdno, YearIdno, isTBBRate, itruckcitywise, dtDetail);
                            obj1 = null;
                        }
                        value = obj.Insert(objtblChlnBookHead, dtTemp, intGrPrepIdno);
                        // obj = null;
                    }
                    else
                    {
                        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                        {
                            db.Connection.Open();
                            Int32 ChlnIdno = Convert.ToInt32(hidid.Value);
                            TblGrHead GrHEad = db.TblGrHeads.Where(rd => rd.Chln_Idno == ChlnIdno).FirstOrDefault();
                            List<TblGrDetl> GrDetl = db.TblGrDetls.Where(rd => rd.GrHead_Idno == GrHEad.GR_Idno).ToList();
                            foreach (TblGrDetl rgd in GrDetl)
                            {
                                db.TblGrDetls.DeleteObject(rgd);
                                db.SaveChanges();
                            }

                            db.TblGrHeads.DeleteObject(GrHEad);
                            db.SaveChanges();
                            db.Connection.Close();
                        }

                        if (grdMain.Rows.Count > 0 && dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            ChlnBookingCrsngDAL obj1 = new ChlnBookingCrsngDAL();
                            intGrPrepIdno = obj1.InsertGR(ChlnNo, TrunckIdno, YearIdno, isTBBRate, itruckcitywise, dtDetail);
                            obj1 = null;
                        }

                        value = obj.Update(objtblChlnBookHead, Convert.ToInt32(hidid.Value), dtTemp, intGrPrepIdno);
                    }
                    if (value > 0)
                    {
                        tScope.Complete();
                    }

                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == false))
                    {
                        ShowMessage("Record Update successfully");
                        Response.Redirect("ChlnBookingCrsng.aspx", false);
                    }
                    else if (value == -1)
                    {
                        ShowMessage("Challan No Already Exist");
                    }
                    else
                    {
                        ShowMessage("Record  Not Update");
                    }
                    if (value > 0 && (string.IsNullOrEmpty(hidid.Value) == true))
                    {
                        ShowMessage("Record  saved Successfully ");
                        Response.Redirect("ChlnBookingCrsng.aspx", false);
                    }
                    else if (value == -1)
                    {
                        tScope.Dispose();
                        ShowMessage("Challan No Already Exist");
                    }
                    else
                    {
                        ShowMessage("Record Not saved Successfully ");
                    }

                }
                catch (Exception Ex)
                {
                    tScope.Dispose();
                }

            #endregion

            }
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ddlGrType.SelectedIndex == 0) { ShowMessage("Please select GR Type."); ddlGrType.Focus(); return; }
            if (ddlfromCity.SelectedIndex == 0) { ShowMessage("Please select From City."); ddlfromCity.Focus(); return; }
            if (ddltoCity.SelectedIndex == 0) { ShowMessage("Please select To City."); ddltoCity.Focus(); return; }
            if (ddlSenderName.SelectedIndex == 0) { ShowMessage("Please select To City."); ddlSenderName.Focus(); return; }
            if (ddlReciverName.SelectedIndex == 0) { ShowMessage("Please select To City."); ddlReciverName.Focus(); return; }

            if (txtQuantity.Text == "" || Convert.ToDouble(txtQuantity.Text) <= 0)
            {
                ShowMessage("Quantity should be greater than zero."); txtQuantity.Focus(); return;
            }
            if (txtweight.Text == "" || Convert.ToDouble(txtweight.Text) <= 0)
            {
                ShowMessage("Weight should be greater than zero."); txtweight.Focus(); return;
            }
            if (txtAmount.Text == "" || Convert.ToDouble(txtAmount.Text) <= 0)
            {
                ShowMessage("Amount should be greater than zero."); txtAmount.Focus(); return;
            }

            if (Hidrowid.Value != string.Empty)
            {
                dtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowid.Value))
                    {
                        dtrow["GR_No"] = txtGrNo.Text;
                        dtrow["GR_Date"] = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text)).ToString();
                        dtrow["Gr_Type"] = ddlGrType.SelectedItem.Text;
                        dtrow["Gr_TypeIdno"] = ddlGrType.SelectedValue;
                        dtrow["FromCity"] = ddlfromCity.SelectedItem.Text;
                        dtrow["FromCityIdno"] = ddlfromCity.SelectedValue;
                        dtrow["ToCity"] = ddltoCity.SelectedItem.Text;
                        dtrow["ToCityIdno"] = ddltoCity.SelectedValue;
                        dtrow["SenderName"] = ddlSenderName.SelectedItem.Text;
                        dtrow["SenderNameIdno"] = ddlSenderName.SelectedValue;
                        dtrow["ReciverName"] = ddlReciverName.SelectedItem.Text;
                        dtrow["ReciverNameIdno"] = ddlReciverName.SelectedValue;
                        dtrow["Qty"] = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                        dtrow["Weight"] = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                        dtrow["Amount"] = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? "0.00" : (txtAmount.Text.Trim());
                        dtrow["Detail"] = string.IsNullOrEmpty(txtdetail.Text.Trim()) ? "" : (txtdetail.Text.Trim());

                    }
                }
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];

                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strGrNo = txtGrNo.Text.Trim();
                string strGrDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text)).ToString();
                string strGrType = ddlGrType.SelectedItem.Text;
                string strGrTypeIdno = ddlGrType.SelectedValue;
                string strFromCity = ddlfromCity.SelectedItem.Text;
                string strFromCityIdno = ddlfromCity.SelectedValue;
                string strToCity = ddltoCity.SelectedItem.Text;
                string strToCityIdno = ddltoCity.SelectedValue;
                string strSenderName = ddlSenderName.SelectedItem.Text;
                string strSenderNameIdno = ddlSenderName.SelectedValue;
                string strReciverName = ddlReciverName.SelectedItem.Text;
                string strReciverNameIdno = ddlReciverName.SelectedValue;
                string strQty = string.IsNullOrEmpty(txtQuantity.Text.Trim()) ? "0" : (txtQuantity.Text.Trim());
                string strWeight = string.IsNullOrEmpty(txtweight.Text.Trim()) ? "0" : (txtweight.Text.Trim());
                string strAmount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? "0.00" : (txtAmount.Text.Trim());
                string strDetail = string.IsNullOrEmpty(txtdetail.Text.Trim()) ? "" : (txtdetail.Text.Trim());

                ApplicationFunction.DatatableAddRow(dtTemp, id, strGrNo, strGrDate, strGrType, strGrTypeIdno, strFromCity, strFromCityIdno, strToCity, strToCityIdno, strSenderName, strSenderNameIdno, strReciverName, strReciverNameIdno, strQty, strWeight, strAmount, strDetail);
                ViewState["dt"] = dtTemp;
            }
            //  ddlItemName_SelectedIndexChanged(null,null);
            this.BindGridT();
            RcptAmtTot(dtTemp);
            ClearItems();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "filltxtthrough()", true);
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["q"]));
            }
            else
            {
                Clear();
            }
        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToString(hidid.Value) == "")
            {
                this.ClearItems();
                ViewState["dt"] = dtTemp = null;
                this.BindGridT();
            }
            else
            {
                //  dtTemp = null;
                this.ClearItems();
                this.Populate(Convert.ToInt32(Request.QueryString["q"]));
            }
            ddlTranspoter.Focus();
        }
        #endregion
    }
}
