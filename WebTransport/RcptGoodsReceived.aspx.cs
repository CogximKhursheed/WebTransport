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
    public partial class RcptGoodsReceived : Pagebase
    {
        #region Private Variables...
        static FinYearA UFinYear = new FinYearA();
        int iFinYrID;
        private int intFormId = 25;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0;
        int totalQty = 0; double TotWeight = 0;
        DataTable DtTemp = new DataTable();
        #endregion

        #region Page Events...
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
                txtReceiptDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.GetAllItems();
                this.BindCity();
               
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    ReceiptGoodsReceivedDAL obj1 = new ReceiptGoodsReceivedDAL();
                    var lst = obj1.SelectCityCombo();
                    obj1 = null;
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
                 ReceiptGoodsReceivedDAL objChlnBookingDAL = new ReceiptGoodsReceivedDAL();
                tblUserPref obj = objChlnBookingDAL.selectUserPref();
                if (obj != null)
                {
                    drpBaseCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    ReceiptGoodsReceivedDAL obja = new ReceiptGoodsReceivedDAL();
                    var SenderReceiver = obja.SelectAcntMastByType(2);
                    BindDropdownDAL objagent = new BindDropdownDAL();
                    var Agent = objagent.BindAgent();
                    obj = null;
                    drpSender.DataSource = SenderReceiver;
                    drpSender.DataTextField = "Acnt_Name";
                    drpSender.DataValueField = "Acnt_Idno";
                    drpSender.DataBind();
                    drpSender.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindSenderReceiverAgent();
                }
                drpSender.SelectedValue = Convert.ToString(base.Sender);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    ItemMasterDAL obja = new ItemMasterDAL();
                    var lst = obja.GetItems();
                    obj = null;
                    drpItemName.DataSource = lst;
                    drpItemName.DataTextField = "ItemName";
                    drpItemName.DataValueField = "ItemId";
                    drpItemName.DataBind();
                    drpItemName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    this.GetAllItems();
                }
                drpItemName.SelectedValue = Convert.ToString(base.ItemName);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    UOMMasterDAL obja = new UOMMasterDAL();
                    var lst = obja.GetUnit();
                    obj = null;
                    drpUnitName.DataSource = lst;
                    drpUnitName.DataTextField = "UOMName";
                    drpUnitName.DataValueField = "UOMId";
                    drpUnitName.DataBind();
                    drpUnitName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    this.GetAllUnit();
                }
                drpUnitName.SelectedValue = Convert.ToString(base.Unit);
                //this.GetAllUnit();
                this.BindDateRange();
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlDateRange.SelectedIndex = 0;
                drpBaseCity.Enabled = true;
                this.GetReceiptNo();
                if (Request.QueryString["q"] != null)
                {
                    this.BindSenderReceiverAgentPopulate();
                    Populate(Convert.ToInt64(Request.QueryString["q"]));
                    hidgoodsreceivedid.Value = Convert.ToString(Request.QueryString["q"]);
                    btnNew.Visible = true;
                    lnkbtnNew.Visible = true;
                    ddlDateRange.Enabled = false; imgPrint.Visible = true;
                }

                else
                {
                    lnkbtnNew.Visible = false;
                    ddlDateRange.Enabled = true;
                    imgPrint.Visible = false;
                    //this.BindSenderReceiverAgent();
                }

                txtWeight.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtQty.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtSenderNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtReceiptDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtRemark.Attributes.Add("onkeypress", "return notAllowSpecialCharacters_Spaceallow(event);");

            }
            Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
        }
        #endregion

        #region Bind Functions...
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
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            drpToCity.DataSource = lst;
            drpToCity.DataTextField = "City_Name";
            drpToCity.DataValueField = "City_Idno";
            drpToCity.DataBind();
            drpToCity.Items.Insert(0, new ListItem("--Select--", "0"));

            drpCityVia.DataSource = lst;
            drpCityVia.DataTextField = "City_Name";
            drpCityVia.DataValueField = "City_Idno";
            drpCityVia.DataBind();
            drpCityVia.Items.Insert(0, new ListItem("--Select--", "0"));

            drpDeliveryPlace.DataSource = lst;
            drpDeliveryPlace.DataTextField = "City_Name";
            drpDeliveryPlace.DataValueField = "City_Idno";
            drpDeliveryPlace.DataBind();
            drpDeliveryPlace.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        private void BindSenderReceiverAgent()
        {
            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            var SenderReceiver = obj.SelectAcntMastByType(2);
            BindDropdownDAL objagent = new BindDropdownDAL();
            var Agent = objagent.BindAgent();
            obj = null;
            drpSender.DataSource = SenderReceiver;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));

            drpReceiver.DataSource = SenderReceiver;
            drpReceiver.DataTextField = "Acnt_Name";
            drpReceiver.DataValueField = "Acnt_Idno";
            drpReceiver.DataBind();
            drpReceiver.Items.Insert(0, new ListItem("--Select--", "0"));

            drpAgentName.DataSource = Agent;
            drpAgentName.DataTextField = "Acnt_Name";
            drpAgentName.DataValueField = "Acnt_Idno";
            drpAgentName.DataBind();
            drpAgentName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindSenderReceiverAgentPopulate()
        {
            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            var SenderReceiver = obj.SelectAcntMastByTypePopulate(2);
            BindDropdownDAL objagent = new BindDropdownDAL();
            var Agent = objagent.BindAgent();
            obj = null;
            drpSender.DataSource = SenderReceiver;
            drpSender.DataTextField = "Acnt_Name";
            drpSender.DataValueField = "Acnt_Idno";
            drpSender.DataBind();
            drpSender.Items.Insert(0, new ListItem("--Select--", "0"));

            drpReceiver.DataSource = SenderReceiver;
            drpReceiver.DataTextField = "Acnt_Name";
            drpReceiver.DataValueField = "Acnt_Idno";
            drpReceiver.DataBind();
            drpReceiver.Items.Insert(0, new ListItem("--Select--", "0"));

            drpAgentName.DataSource = Agent;
            drpAgentName.DataTextField = "Acnt_Name";
            drpAgentName.DataValueField = "Acnt_Idno";
            drpAgentName.DataBind();
            drpAgentName.Items.Insert(0, new ListItem("--Select--", "0"));
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
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }
        #endregion

        #region Other Functions...
        private void PrintGRPrep(Int64 GRHeadIdno)
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spRcptGoodRecd] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["RcptGoodHead_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["RcptGoodHead_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]) == "")
                {
                    lbltxtagent.Visible = false; lblAgent.Visible = false; TdlblAgent.Visible = false;
                }
                else
                {
                    lblAgent.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]); lbltxtagent.Visible = true; lblAgent.Visible = true; TdlblAgent.Visible = true;
                }

                lblSenderName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblRecvrName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
            }
        }
        private void Populate(Int64 HeadId)
        {
            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            tblRcptGoodHead RcptGoodHead = obj.SelectByReceiptGoodReceivedByHeadId(HeadId);
            var RcptGoodDetlList = obj.SelectReceiptGoodDetailByHeadId(HeadId);
            obj = null;

            if (RcptGoodHead != null)
            {
                txtReceiptNo.Text = Convert.ToString(RcptGoodHead.RcptGoodHead_No);
                txtReceiptDate.Text = Convert.ToDateTime(RcptGoodHead.RcptGoodHead_Date).ToString("dd-MM-yyyy");
                txtSenderNo.Text = Convert.ToString(RcptGoodHead.Sender_No);
                drpAgentName.SelectedValue = Convert.ToString(RcptGoodHead.Agent_Idno);
                drpBaseCity.SelectedValue = Convert.ToString(RcptGoodHead.FromCity_Idno);
                drpDeliveryPlace.SelectedValue = Convert.ToString(RcptGoodHead.DelvryPlc_Idno);
                drpToCity.SelectedValue = Convert.ToString(RcptGoodHead.ToCity_Idno);
                drpCityVia.SelectedValue = Convert.ToString(RcptGoodHead.CityVia_Idno);
                drpSender.SelectedValue = Convert.ToString(RcptGoodHead.Sender_Idno);
                drpReceiver.SelectedValue = Convert.ToString(RcptGoodHead.Recevr_Idno);
                ddlDateRange.SelectedValue = Convert.ToString(RcptGoodHead.Year_Idno);
                drpBaseCity.Enabled = false;
                DtTemp = CreateDt();
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ViewState["dt"] = DtTemp;
                foreach (var RGD in RcptGoodDetlList)
                {
                    ApplicationFunction.DatatableAddRow(DtTemp, id, Convert.ToString(DataBinder.Eval(RGD, "Item_Idno")),
                        Convert.ToString(DataBinder.Eval(RGD, "Item_Name")),
                        Convert.ToString(DataBinder.Eval(RGD, "Qty")),
                        Convert.ToString(DataBinder.Eval(RGD, "Weight")),
                        Convert.ToString(DataBinder.Eval(RGD, "Unit_idno")),
                        Convert.ToString(DataBinder.Eval(RGD, "UOM_Name")),
                        Convert.ToString(DataBinder.Eval(RGD, "Remark")));
                    id++;
                }
                ViewState["dt"] = DtTemp;
            }
            this.BindGrid();
            PrintGRPrep(HeadId);
        }


        private void GetReceiptNo()
        {
            Int64 YearIdno = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 LocIdno = Convert.ToInt64(drpBaseCity.SelectedValue);

            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            Int64 max = obj.GetMaxNo(YearIdno, LocIdno);
            obj = null;
            txtReceiptNo.Text = Convert.ToInt64(max) <= 0 ? "1" : Convert.ToString(max);
        }
        private void ClearAll()
        {
            this.GetReceiptNo();
            drpAgentName.SelectedValue = "0";
            drpReceiver.SelectedValue = "0";
            drpSender.SelectedValue = "0";
            drpToCity.SelectedValue = "0";
            txtSenderNo.Text = "";
            drpDeliveryPlace.SelectedValue = "0";
            grdMain.DataSource = null;
            grdMain.DataBind();
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
                    txtReceiptDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtReceiptDate.Text = hidmindate.Value;

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
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "id", "String", "ITEM_ID", "String", "ITEM_NAME", "String", "ITEM_QTY", "String", "ITEM_WEIGHT", "String", "UOM_IDNO", "String", "UOM_NAME", "String",
                                                                   "ITEM_REMARK", "String");
            return dttemp;
        }
        private void ClearItems()
        {
            drpBaseCity.Enabled = true;
            drpItemName.SelectedValue = "0";
            txtQty.Text = "";
            drpUnitName.SelectedValue = "0";
            txtRemark.Text = "";
            txtWeight.Text = "";
            hiduomid.Value = "";
            hidrowid.Value = string.Empty;
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        #endregion

        #region Button Events...
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["q"]));
            }
            else
            {
                this.ClearItems(); this.ClearAll();
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            if (txtQty.Text == string.Empty)
            {
                this.ShowMessage("Please enter qty!");
                return;
            }
            else if (txtWeight.Text == string.Empty)
            {
                this.ShowMessage("Please enter weight!");
                return;
            }
            else
            {
                if (txtQty.Text == string.Empty || txtQty.Text == "0")
                {
                    this.ShowMessage("Qty must be greater than 0!");
                    return;
                }
                else if (txtWeight.Text == string.Empty || txtWeight.Text == "0")
                {
                    this.ShowMessage("Weight must be greater than 0!");
                    return;
                }
            }
            if (hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(hidrowid.Value))
                    {
                        dtrow["ITEM_ID"] = drpItemName.SelectedValue;
                        dtrow["ITEM_NAME"] = drpItemName.SelectedItem.Text;
                        dtrow["ITEM_QTY"] = txtQty.Text;
                        dtrow["ITEM_WEIGHT"] = txtWeight.Text;
                        dtrow["UOM_IDNO"] = drpUnitName.SelectedValue;
                        dtrow["UOM_NAME"] = drpUnitName.SelectedItem.Text;
                        dtrow["ITEM_REMARK"] = txtRemark.Text;
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
                        if (Convert.ToInt32(row["ITEM_ID"]) == Convert.ToInt32(drpItemName.SelectedValue))
                        {
                            msg = "Item Already Selected!";
                            drpItemName.Focus();
                            this.ShowMessage(msg);
                            return;
                        }
                    }
                }
                else
                { DtTemp = CreateDt(); }
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ApplicationFunction.DatatableAddRow(DtTemp, id, drpItemName.SelectedValue, drpItemName.SelectedItem.Text, txtQty.Text,
                                                   txtWeight.Text, drpUnitName.SelectedValue, drpUnitName.SelectedItem.Text, txtRemark.Text);
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            this.ClearItems();
            drpItemName.Focus();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearItems();
                drpItemName.Focus();
            }
            catch (Exception Ex)
            {
            }
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            tblRcptGoodHead objRGH = new tblRcptGoodHead();
            objRGH.Agent_Idno = Convert.ToInt32(drpAgentName.SelectedValue);
            objRGH.Date_Added = System.DateTime.Now;
            objRGH.Date_Modified = System.DateTime.Now;
            objRGH.DelvryPlc_Idno = Convert.ToInt32(drpDeliveryPlace.SelectedValue);
            objRGH.FromCity_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
            objRGH.GRHead_Idno = 0;
            objRGH.RcptGoodHead_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDate.Text));
            objRGH.RcptGoodHead_No = Convert.ToInt32(txtReceiptNo.Text);
            objRGH.Recevr_Idno = Convert.ToInt32(drpReceiver.SelectedValue);
            objRGH.Sender_Idno = Convert.ToInt32(drpSender.SelectedValue);
            objRGH.Sender_No = Convert.ToString(txtSenderNo.Text);
            objRGH.Status = true;
            objRGH.UserIdno = Convert.ToInt64(Session["UserIdno"]);
            objRGH.ToCity_Idno = Convert.ToInt32(drpToCity.SelectedValue);
            objRGH.CityVia_Idno = Convert.ToInt32(drpCityVia.SelectedValue);
            objRGH.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);

            List<tblRcptGoodDetl> RgDlst = new List<tblRcptGoodDetl>();
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp != null)
            {
                foreach (DataRow dtrow in DtTemp.Rows)
                {
                    tblRcptGoodDetl rgd = new tblRcptGoodDetl();
                    rgd.Item_Idno = Convert.ToInt64(dtrow["ITEM_ID"]);
                    rgd.Qty = Convert.ToDouble(dtrow["ITEM_QTY"]);
                    rgd.Weight = Convert.ToDouble(dtrow["ITEM_WEIGHT"]);
                    rgd.Unit_idno = Convert.ToInt64(dtrow["UOM_IDNO"]);
                    rgd.Remark = Convert.ToString(dtrow["ITEM_REMARK"]);

                    RgDlst.Add(rgd);
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
            Int64 ReceiptGoodId = 0;
            ReceiptGoodsReceivedDAL obj = new ReceiptGoodsReceivedDAL();
            if (Convert.ToInt32(hidgoodsreceivedid.Value) > 0)
            {
                objRGH.RcptGoodHead_Idno = Convert.ToInt64(hidgoodsreceivedid.Value);
                ReceiptGoodId = obj.Update(objRGH, RgDlst);
                lnkbtnNew.Visible = false;
            }
            else
            {
                ReceiptGoodId = obj.Insert(objRGH, RgDlst);
                this.ClearAll();
            }

            obj = null;
            if (ReceiptGoodId > 0)
            {
                ShowMessage("Record save successfully");
                this.ClearAll();
            }
            else if (ReceiptGoodId < 0)
            {
                ShowMessage("Receipt No already exists");
            }
            else
            {
                ShowMessage("Record not saved successfully");
            }


        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("RcptGoodsReceived.aspx", false);
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            //GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                DtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = DtTemp.Select("Id='" + id + "'");

                if (drs.Length > 0)
                {

                    drpItemName.SelectedValue = Convert.ToString(drs[0]["ITEM_ID"]);
                    txtQty.Text = Convert.ToString(drs[0]["ITEM_QTY"]);
                    txtWeight.Text = Convert.ToString(drs[0]["ITEM_WEIGHT"]);
                    drpUnitName.SelectedValue = Convert.ToString(drs[0]["UOM_IDNO"]);
                    txtRemark.Text = Convert.ToString(drs[0]["ITEM_REMARK"]);
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

                        ApplicationFunction.DatatableAddRow(dt, rw["id"], rw["ITEM_ID"], rw["ITEM_NAME"], rw["ITEM_QTY"],
                                                                rw["ITEM_WEIGHT"], rw["UOM_IDNO"], rw["UOM_NAME"], rw["ITEM_REMARK"]);

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
                int qty = 0;
                string billed = string.Empty;
                qty = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ITEM_QTY"));
                totalQty += qty;

                TotWeight = TotWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ITEM_WEIGHT"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblqtytotal = (Label)e.Row.FindControl("lblqtytotal");
                lblqtytotal.Text = Convert.ToString(totalQty);
                Label lblWeighttotal = (Label)e.Row.FindControl("lblWeighttotal");
                lblWeighttotal.Text = Convert.ToDouble(TotWeight).ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Control Events...

        protected void drpBaseCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetReceiptNo();
            drpBaseCity.Focus();
        }

        protected void drpItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemMasterDAL obj = new ItemMasterDAL();
            ItemDetailForGrid idg = obj.GetItemByItemId(Convert.ToInt32(drpItemName.SelectedValue));
            obj = null;
            if (idg != null)
            {
                drpUnitName.Focus();
            }
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 

                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total

                lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();

            }
        }
        #endregion
    }
}