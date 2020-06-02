using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class SummaryRegister : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 36;
        Int64 RcptGoodHeadIdno = 0; Int64 chlnIdno = 0;
        #endregion

        #region PageLoad Events...
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
                txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFromDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateToDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCityTo();
                }
                else
                {
                    this.BindCityTo(Convert.ToInt64(Session["UserIdno"]));
                }
                BindDropdown();
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);

                this.BindDateRange();
                ddldateRange.SelectedIndex = 0; ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);

                ddlFromCity_SelectedIndexChanged(null, null);
                ddlDateRange_SelectedIndexChanged(null, null);
                SummaryRegisterDAL obj = new SummaryRegisterDAL(); Int64 MaxGRNo = 0; Int32 FromCityIdno = 0; FromCityIdno = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                MaxGRNo = obj.MaxNo(Convert.ToInt32(ddldateRange.SelectedValue), FromCityIdno, ApplicationFunction.ConnectionString());
                txtRcptNo.Text = Convert.ToString(MaxGRNo);
                txtCrossing.Text = txtWAy.Text = txtfreightCharg.Text = txtotherCharg.Text = txttotal1.Text = "0.00";
                txtKatt.Text = txtlabour.Text = txtDelivery.Text = txtOctrai.Text = txttotal2.Text = txtNetTotal.Text = "0.00";
                if (Request.QueryString["SummaryIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["SummaryIdno"]));
                    lnkbtnNew.Visible = true; ddlFromCity.Enabled = false; lnkbtnPrintClick.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false; ddlFromCity.Enabled = true; lnkbtnPrintClick.Visible = false;
                }
                // txtItemName.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            SummaryRegisterDAL objItemMast = new SummaryRegisterDAL();
            Int64 intItemIdno = 0;
            tblSummaryRegister obj = new tblSummaryRegister();
            obj.Year_Idno = Convert.ToInt32(ddldateRange.SelectedValue);
            obj.SumReg_No = Convert.ToInt64(Convert.ToString(txtRcptNo.Text.Trim()) == "" ? 0 : Convert.ToInt64(txtRcptNo.Text.Trim()));
            obj.SumReg_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text.Trim().ToString()));
            obj.Date_Added = DateTime.Now;
            obj.Date_Modified = null;
            obj.FromCity_Idno = Convert.ToInt64(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue));
            obj.Chln_Idno = Convert.ToInt64(Convert.ToString(hidchlnIdno.Value) == "" ? 0 : Convert.ToInt64(hidchlnIdno.Value));
            obj.Chln_no = Convert.ToInt64(Convert.ToString(txtchlnNo.Text.Trim()) == "" ? 0 : Convert.ToInt64(txtchlnNo.Text.Trim()));
            obj.Truck_Idno = Convert.ToInt64(Convert.ToString(ddltruckno.SelectedValue) == "" ? 0 : Convert.ToInt64(ddltruckno.SelectedValue));
            obj.Driver_idno = Convert.ToInt64(Convert.ToString(ddldriver.SelectedValue) == "" ? 0 : Convert.ToInt64(ddldriver.SelectedValue));
            obj.Crossing_Amnt = string.IsNullOrEmpty((Convert.ToString(txtCrossing.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtCrossing.Text)).Replace(",", ""));
            obj.Way_Amnt = string.IsNullOrEmpty((Convert.ToString(txtWAy.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtWAy.Text)).Replace(",", ""));
            obj.Other_Charges = string.IsNullOrEmpty((Convert.ToString(txtotherCharg.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtotherCharg.Text)).Replace(",", ""));
            obj.Freight_Amnt = string.IsNullOrEmpty((Convert.ToString(txtfreightCharg.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtfreightCharg.Text)).Replace(",", ""));
            obj.Total_Amnt1 = string.IsNullOrEmpty((Convert.ToString(txttotal1.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txttotal1.Text)).Replace(",", ""));
            obj.Katt_Amnt = string.IsNullOrEmpty((Convert.ToString(txtKatt.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtKatt.Text)).Replace(",", ""));
            obj.Labour_Amnt = string.IsNullOrEmpty((Convert.ToString(txtlabour.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtlabour.Text)).Replace(",", ""));
            obj.Delivery_Amnt = string.IsNullOrEmpty((Convert.ToString(txtDelivery.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtDelivery.Text)).Replace(",", ""));
            obj.Octrai_Amnt = string.IsNullOrEmpty((Convert.ToString(txtOctrai.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtOctrai.Text)).Replace(",", ""));
            obj.Total_Amnt2 = string.IsNullOrEmpty((Convert.ToString(txttotal2.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txttotal2.Text)).Replace(",", ""));
            obj.Net_Amnt = string.IsNullOrEmpty((Convert.ToString(txtNetTotal.Text)).Replace(",", "")) ? 0 : Convert.ToDouble((Convert.ToString(txtNetTotal.Text)).Replace(",", ""));
            if (string.IsNullOrEmpty(hidSummryRegidno.Value) == true)
            {
                intItemIdno = objItemMast.Insert(obj);
            }
            else
            {
                lnkbtnNew.Visible = false; lnkbtnPrintClick.Visible = false;
                intItemIdno = objItemMast.Update(obj, Convert.ToInt32(Convert.ToString(hidSummryRegidno.Value) == "" ? 0 : Convert.ToInt32(hidSummryRegidno.Value)));
            }
            objItemMast = null;

            if (intItemIdno > 0)
            {
                if (string.IsNullOrEmpty(hidSummryRegidno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    strMsg = "Record saved successfully.";
                }
                this.ClearControls();
            }
            else if (intItemIdno < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                if (string.IsNullOrEmpty(hidSummryRegidno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                else
                {
                    strMsg = "Record not saved.";
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            // txtItemName.Focus();
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidSummryRegidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidSummryRegidno.Value));
            }

        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SummaryRegister.aspx");
        }
        protected void lnkbtnGrAgnst_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            txtDateFromDiv.Focus();
        }

        protected void lnkbtnSearchClick_OnClick(object sender, EventArgs e)
        {

            try
            {
                chkSelectAllRows.Checked = false;
                SummaryRegisterDAL obj = new SummaryRegisterDAL();
                Int64 Tocity = Convert.ToInt64((ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue));
                DataTable DsGrdetail = obj.SelectChlnDetail("SelectChlnDetail", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFromDiv.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateToDiv.Text)), Tocity, ApplicationFunction.ConnectionString());
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind(); //BtnClerForPurOdr.Visible = true;
                    // btnSubmit.Visible = true; chkSelectAllRows.Visible = true;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();// BtnClerForPurOdr.Visible = false;
                    // btnSubmit.Visible = false; chkSelectAllRows.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }

        protected void lnkbtnDivSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {

                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    string strchkDetlValue = string.Empty; int Icount = 0;
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {

                        CheckBox ChkGr = (CheckBox)grdGrdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            //HiddenField hidChlnIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidChlnIdno");
                            //chlnIdno = Convert.ToInt64(hidChlnIdno.Value);
                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                            RcptGoodHeadIdno = Convert.ToInt64(hidGrIdno.Value); HidGrAgnstRcptIdno.Value = (hidGrIdno.Value);
                            Icount++;

                        }

                    }
                    if (Icount > 1)
                    {
                        //ShowMessage("Please check only one Gr.");
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        // ShowDiv("ShowClient('dvGrdetails')");
                        return;

                    }
                    else
                    {
                        lblmsg.Visible = false;
                        lblmsg2.Visible = false;
                    }
                    if (strchkDetlValue != "")
                    {


                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        lblmsg.Visible = true;
                        lblmsg2.Visible = true;
                        lblmsg.Text = "Please select atleast one record.";
                        lblmsg2.Text = "Please select atleast one record.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        //ShowMessage("Please check atleast one Gr.");
                        // ShowDiv("ShowClient('dvGrdetails')");
                        return;

                    }
                    else
                    {
                        lblmsg.Visible = false;
                        lblmsg2.Visible = false;
                        SummaryRegisterDAL obj = new SummaryRegisterDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = obj.selectDetails("SelectChlnDetailInRcpt", Convert.ToString(strchkDetlValue), ApplicationFunction.ConnectionString());
                        ViewState["dt"] = dtRcptDetl;
                        hidchlnIdno.Value = Convert.ToString(dtRcptDetl.Rows[0]["Chln_idno"]);
                        ddlFromCity.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["ToCity_Idno"]); ddlFromCity.Enabled = false;
                        txtchlnNo.Text = Convert.ToString(dtRcptDetl.Rows[0]["Chln_No"]); txtchlnNo.Enabled = false;
                        txtCrossing.Text = Convert.ToString(dtRcptDetl.Rows[0]["CrsngGR_Amnt"]); txtCrossing.Enabled = false;
                        txtfreightCharg.Text = Convert.ToString(dtRcptDetl.Rows[0]["FreightAmnt"]); txtfreightCharg.Enabled = false;
                        ddltruckno.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Truck_Idno"]);// ddltruckno.Enabled = false;
                        ddldriver.SelectedValue = Convert.ToString(dtRcptDetl.Rows[0]["Driver_Idno"]); //ddldriver.Enabled = false;
                        totalAmntCal1();
                        NetAmntCal();
                    }
                    chkSelectAllRows.Checked = false;
                }
                else
                {
                    //ShowMessageErr("Gr Details not found.");
                    //grdMain.DataSource = null;
                    //grdMain.DataBind();
                    chkSelectAllRows.Checked = false;
                    // ddlDelvryPlace.Enabled = true;
                    //ShowDiv("ShowBillAgainst('dvGrdetails')");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        #endregion

        #region Functions
        private void PrintSummaryRegister(Int64 SRHeadIdno)
        {
            Repeater obj = new Repeater();
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string ServTaxNo = ""; string FaxNo = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = (Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]));
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);

            lblCompanyname.Text = CompName; //lblCompname.Text = "For - " + CompName;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spSummaryRegister] @ACTION='printSelect',@Id='" + SRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblSRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SUMMARY_NO"]);
                lblSrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["SUMMARY_DATE"]).ToString("dd-MM-yyyy");
                lbltooCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TOCITY"]);
                lblvalueTruck.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["LORRY_NO"]);
                lblvaluedriver.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DRIVER_NAME"]);
                lblvaluechlnno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Challan_No"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DRIVER_NAME"]) == "")
                {
                    lblvaluedriver.Visible = false; lbltxtdriver.Visible = false; Tdlbldriver.Visible = false;
                }
                else
                {
                    lblvaluedriver.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DRIVER_NAME"]); lbltxtdriver.Visible = true; Tdlbldriver.Visible = true; lblvaluedriver.Visible = true;
                }
                lblvaluecrossng.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["CROSSING_aMNT"]));
                lblvalueway.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["WAY_AMOUNT"]));
                lblvalueoctrai.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["OCTRAI_AMOUNT"]));
                lblPrintFrieght.Text = string.Format("{0:0,0.00}", Convert.ToDouble(txtfreightCharg.Text));
                lblvalueother.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["OTHER_AMNT"]));
                lblvaluedelivery.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["DELIVERY_AMNT"]));
                lblvaluelabour.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["LABOUR_AMNT"]));
                lblvalueKatt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["KATT_AMNT"]));
                Lbltotal1.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TOTAL1"]));
                lblvaluetotal2.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TOTAL2"]));
                lblvaluenetamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["NET_AMNT"]));

            }
        }

        public void Populate(Int32 intHeadIdno)
        {
            SummaryRegisterDAL obj = new SummaryRegisterDAL();
            tblSummaryRegister objHead = obj.SelectHead(intHeadIdno);
            hidSummryRegidno.Value = Convert.ToString(intHeadIdno);
            if (objHead != null)
            {
                ddldateRange.SelectedValue = Convert.ToString(objHead.Year_Idno);
                txtGRDate.Text = string.IsNullOrEmpty(Convert.ToString(objHead.SumReg_Date)) ? "" : Convert.ToDateTime(objHead.SumReg_Date).ToString("dd-MM-yyyy");
                txtchlnNo.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Chln_no)) ? "" : Convert.ToString(objHead.Chln_no);
                txtRcptNo.Text = string.IsNullOrEmpty(Convert.ToString(objHead.SumReg_No)) ? "" : Convert.ToString(objHead.SumReg_No);
                ddlFromCity.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objHead.FromCity_Idno)) ? "0" : Convert.ToString(objHead.FromCity_Idno);
                ddltruckno.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objHead.Truck_Idno)) ? "0" : Convert.ToString(objHead.Truck_Idno);
                ddldriver.SelectedValue = string.IsNullOrEmpty(Convert.ToString(objHead.Driver_idno)) ? "0" : Convert.ToString(objHead.Driver_idno);


                //ViewState["dt"] = dtTemp;
                //BindGridT();
                txtCrossing.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Crossing_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Crossing_Amnt);
                txtWAy.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Way_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Way_Amnt);
                txtfreightCharg.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Freight_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Freight_Amnt);
                txtotherCharg.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Other_Charges)) ? "0" : String.Format("{0:0,0.00}", objHead.Other_Charges);
                txttotal1.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Total_Amnt1)) ? "0" : String.Format("{0:0,0.00}", (objHead.Total_Amnt1));
                txtKatt.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Katt_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Katt_Amnt);
                txtlabour.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Labour_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Labour_Amnt);
                txtDelivery.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Delivery_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Delivery_Amnt);
                txtOctrai.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Octrai_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Octrai_Amnt);
                txttotal2.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Total_Amnt2)) ? "0" : String.Format("{0:0,0.00}", (objHead.Total_Amnt2));
                txtNetTotal.Text = string.IsNullOrEmpty(Convert.ToString(objHead.Net_Amnt)) ? "0" : String.Format("{0:0,0.00}", objHead.Net_Amnt);
                obj = null;
            }
            PrintSummaryRegister(intHeadIdno);
            obj = null;
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
                    txtDateFromDiv.Text = hidmindate.Value;
                    txtDateToDiv.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtDateFromDiv.Text = hidmindate.Value;
                    txtDateToDiv.Text = hidmaxdate.Value;
                }
            }

        }
        private void BindDropdown()
        {
            SummaryRegisterDAL objsummary = new SummaryRegisterDAL();
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNo();
            var ToCity = obj.BindLocFrom();
            obj = null;

            var driver = objsummary.selectHireDriverName();
            objsummary = null;
            if (driver != null && driver.Count > 0)
            {
                ddldriver.DataSource = driver;
                ddldriver.DataTextField = "Driver_name";
                ddldriver.DataValueField = "Driver_Idno";
                ddldriver.DataBind();
                ddldriver.Items.Insert(0, new ListItem("--Select--", "0"));
            }


            ddltruckno.DataSource = TruckNolst;
            ddltruckno.DataTextField = "Lorry_No";
            ddltruckno.DataValueField = "lorry_idno";
            ddltruckno.DataBind();
            ddltruckno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void BindCityTo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            obj = null;
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindCityTo(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "City_Name";
            ddlFromCity.DataValueField = "City_Idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void totalAmntCal1()
        {
            try
            {
                txttotal1.Text = Convert.ToDouble(Convert.ToDouble(txtCrossing.Text) + Convert.ToDouble(txtfreightCharg.Text) + Convert.ToDouble(txtWAy.Text) + Convert.ToDouble(txtotherCharg.Text)).ToString("N2");
            }
            catch (Exception Ex)
            {

            }

        }
        private void totalAmntCal2()
        {
            try
            {
                txttotal2.Text = Convert.ToDouble(Convert.ToDouble(txtKatt.Text) + Convert.ToDouble(txtlabour.Text) + Convert.ToDouble(txtDelivery.Text) + Convert.ToDouble(txtOctrai.Text)).ToString("N2");
            }
            catch (Exception Ex)
            {
            }

        }
        private void NetAmntCal()
        {
            try
            {
                txtNetTotal.Text = Convert.ToDouble(Convert.ToDouble(txttotal1.Text) - Convert.ToDouble(txttotal2.Text)).ToString("N2");
            }
            catch (Exception Ex)
            {
            }

        }

        private void ClearControls()
        {
            txtCrossing.Text = txtWAy.Text = txtfreightCharg.Text = txtotherCharg.Text = txttotal1.Text = "0.00"; txtchlnNo.Text = "";
            txtKatt.Text = txtlabour.Text = txtDelivery.Text = txtOctrai.Text = txttotal2.Text = txtNetTotal.Text = "0.00";
            ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
            ddldriver.SelectedValue = "0";
            ddltruckno.SelectedValue = "0";
            txtchlnNo.Enabled = txtCrossing.Enabled = txtfreightCharg.Enabled = false;
            ddlFromCity.Enabled = true;
        }
        #endregion

        #region control events
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddldateRange.Focus();
        }
        protected void txtWAy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(txtWAy.Text.Trim()) == "")
                {
                    txtWAy.Text = "0.00";
                }
                totalAmntCal1();
                NetAmntCal();
                txtWAy.Focus();
            }
            catch (Exception Ex)
            {
            }
        }
        protected void txtotherCharg_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtotherCharg.Text.Trim()) == "")
            {
                txtotherCharg.Text = "0.00";
            }
            totalAmntCal1();
            NetAmntCal();
            txtotherCharg.Focus();
        }
        protected void txtKatt_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtKatt.Text.Trim()) == "")
            {
                txtKatt.Text = "0.00";
            }
            totalAmntCal2();
            NetAmntCal();
            txtKatt.Focus();
        }
        protected void txtlabour_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtlabour.Text.Trim()) == "")
            {
                txtlabour.Text = "0.00";
            }
            totalAmntCal2();
            NetAmntCal();
            txtlabour.Focus();
        }
        protected void txtDelivery_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtDelivery.Text.Trim()) == "")
            {
                txtDelivery.Text = "0.00";
            }
            totalAmntCal2();
            NetAmntCal();
            txtDelivery.Focus();
        }
        protected void txtOctrai_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtOctrai.Text.Trim()) == "")
            {
                txtOctrai.Text = "0.00";
            }
            totalAmntCal2();
            NetAmntCal();
            txtOctrai.Focus();
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SummaryRegisterDAL obj = new SummaryRegisterDAL(); Int64 MaxGRNo = 0; Int32 FromCityIdno = 0; FromCityIdno = Convert.ToInt32(Convert.ToString(ddlFromCity.SelectedValue) == "" ? 0 : Convert.ToInt32(ddlFromCity.SelectedValue));
                MaxGRNo = obj.MaxNo(Convert.ToInt32(Convert.ToString(ddldateRange.SelectedValue) == "" ? 0 : Convert.ToInt32(ddldateRange.SelectedValue)), FromCityIdno, ApplicationFunction.ConnectionString());
                txtRcptNo.Text = Convert.ToString(MaxGRNo);
                if (ddlFromCity.SelectedIndex > 0)
                {
                    lnkbtnGrAgnst.Enabled = true;
                }
                else
                {
                    lnkbtnGrAgnst.Enabled = false;
                }

                ddlFromCity.Focus();
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion
    }
}