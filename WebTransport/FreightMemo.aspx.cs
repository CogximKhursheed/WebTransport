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
    public partial class FreightMemo : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 37;
        #endregion

        #region PageLaod Events...
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
                txtServiceCharg.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtLabbrCharg.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtDelvAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtOctraiAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtDamage.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                txtGRDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFromDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateToDiv.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                BindDateRange();
                BindReciver();
                ddldateRange_SelectedIndexChanged(null, null);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                MaxRecptNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue));
                imgSearch.Enabled = true; ddlToCity.Enabled = true;
                if (Request.QueryString["Freightidno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["Freightidno"]));
                    lnkbtnNew.Visible = true; imgSearch.Enabled = false; ddlToCity.Enabled = false;
                    lnkbtnPrint.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false; imgSearch.Enabled = true; ddlToCity.Enabled = true;
                    lnkbtnPrint.Visible = false;
                }
                ddldateRange.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            FreightMemoDAL objItemMast = new FreightMemoDAL();
            Int64 intValue = 0;
            if ((txtGrNo.Text == "") && (ddlRecivr.SelectedIndex <= 0) && (txtQty.Text == "") && (txtWeight.Text == "") && (txtFreight.Text == ""))
            {
                ShowMessageErr("please Enter Details");
                ddlToCity.Focus();
                return;
            }
            tblFreightMemo objtblFreightMemo = new tblFreightMemo();
            objtblFreightMemo.Rcpt_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtGRDate.Text));
            objtblFreightMemo.Rcpt_No = Convert.ToInt64(txtRcptNo.Text);
            objtblFreightMemo.ToCity_Idno = Convert.ToInt64(ddlToCity.SelectedValue);
            objtblFreightMemo.Gr_No = Convert.ToInt64(txtGrNo.Text);
            objtblFreightMemo.Receiver_Idno = Convert.ToInt64(ddlRecivr.SelectedValue);
            objtblFreightMemo.Tot_Qty = Convert.ToInt64(txtQty.Text);
            objtblFreightMemo.Tot_Weight = Convert.ToDouble(txtWeight.Text);
            objtblFreightMemo.Freight_Amnt = Convert.ToDouble(txtFreight.Text);
            objtblFreightMemo.Service_Amnt = Convert.ToDouble(txtServiceCharg.Text);
            objtblFreightMemo.Labour_Amnt = Convert.ToDouble(txtLabbrCharg.Text);
            objtblFreightMemo.Delivery_Amnt = Convert.ToDouble(txtDelvAmnt.Text);
            objtblFreightMemo.Octrai_Amnt = Convert.ToDouble(txtOctraiAmnt.Text);
            objtblFreightMemo.Damage_Amnt = Convert.ToDouble(txtDamage.Text);
            objtblFreightMemo.Net_Amnt = Convert.ToDouble(txtNetTotal.Text);
            objtblFreightMemo.GR_Idno = Convert.ToInt64(hidGrIdno.Value);
            objtblFreightMemo.Year_Idno = Convert.ToInt64(ddldateRange.SelectedValue);
            objtblFreightMemo.Remarks = Convert.ToString(txtremark.Text);
            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                intValue = objItemMast.Insert(objtblFreightMemo);
            }
            else
            {
                intValue = objItemMast.Update(objtblFreightMemo, Convert.ToInt64(hidItemidno.Value));
            }
            objItemMast = null;

            if (intValue > 0)
            {
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    strMsg = "Record updated successfully.";
                }
                else
                {
                    strMsg = "Record saved successfully.";
                }
                this.ClearControls();
            }
            else if (intValue < 0)
            {
                strMsg = "Record already exists.";
            }
            else
            {
                if (string.IsNullOrEmpty(hidItemidno.Value) == false)
                {
                    strMsg = "Record not updated.";
                }
                else
                {
                    strMsg = "Record not saved.";
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            txtRcptNo.Focus();
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidItemidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidItemidno.Value));
            }
        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("FreightMemo.aspx");
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                FreightMemoDAL obj = new FreightMemoDAL();
                Int32 iToCityIdno = (ddlToCity.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlToCity.SelectedValue));
                string strAction = "";
                string StrGrno = "";
                StrGrno = txtSrchGrNo.Text;
                DataTable DsGrdetail = obj.selectGrDetails("SelectGRDetail", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFromDiv.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateToDiv.Text)), StrGrno, ApplicationFunction.ConnectionString(), iToCityIdno);
                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnSubmitok.Visible = true; btnbtnClear.Visible = true;
                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnSubmitok.Visible = false; btnbtnClear.Visible = false;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {

            if (ddlToCity.SelectedIndex <= 0)
            {
                ShowMessageErr("Please select To city");
                ddlToCity.Focus();
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true); lnkbtnSubmitok.Visible = false; btnbtnClear.Visible = false;
            txtDateFromDiv.Focus();
        }
        protected void lnkbtnSubmitok_OnClick(object sender, EventArgs e)
        {
            try
            {
                Int32 iRateType = 0; double dcommssn = 0, dweight = 0, dqty = 0, dtotcommssn = 0;
                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    string strchkDetlValue = string.Empty; Int32 Count = 0;
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdGrdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                            Count = Count + 1;
                        }
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Gr.");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        return;
                    }
                    if (Count > 1)
                    {
                        ShowMessageErr("Please select only one Gr. at a time");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        return;
                    }
                    else
                    {
                        FreightMemoDAL ObjFreightMemoDAL = new FreightMemoDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable(); DataRow Dr;
                        dtRcptDetl = ObjFreightMemoDAL.SelectGRDetailInRcpt(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;
                        BindValues(dtRcptDetl);
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                    }
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    ShowDiv("ShowBillAgainst('dvGrdetails')");
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }

        }
        #endregion

        #region Miscellaneous Events...
        private void FreightMemoo(Int64 SRHeadIdno)
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spFreightMemo] @ACTION='PrintSelect',@Id='" + SRHeadIdno + "'");
            dsReport.Tables[0].TableName = "FreightPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblrecpt.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["Recpt_No"]);
                lblGRDate.Text = Convert.ToDateTime(dsReport.Tables["FreightPrint"].Rows[0]["Recpt_Date"]).ToString("dd-MM-yyyy");
                lblBillty.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["Billty_No"]);
                lblQty.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["Tot_Qty"]);
                lblfromcity.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["City_Name"]);
                lblweight.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["Tot_Weight"]);
                lblPrty.Text = Convert.ToString(dsReport.Tables["FreightPrint"].Rows[0]["Recv_Name"]);
                lblValueFreight.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Freight_Amnt"]));
                lblvalueSC.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Service_Amnt"]));
                lblValuelabour.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Labour_Amnt"]));
                lblValueDC.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Delivery_Amnt"]));
                lblValueOthers.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Octrai_Amnt"]));
                lblvalueDamage.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Damage_Amnt"]));
                lblvaluetotal2.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["FreightPrint"].Rows[0]["Net_Amnt"]));


            }
        }

        private void MaxRecptNo(Int32 YearIdno, Int32 TocityIdno)
        {
            FreightMemoDAL obj = new FreightMemoDAL();
            txtRcptNo.Text = Convert.ToString(obj.GetRcptNo(YearIdno, TocityIdno));
        }

        public void NetAmntCal()
        {
            txtNetTotal.Text = Convert.ToDouble(Convert.ToDouble(txtFreight.Text) + Convert.ToDouble(txtServiceCharg.Text) + Convert.ToDouble(txtLabbrCharg.Text) + Convert.ToDouble(txtDelvAmnt.Text) + Convert.ToDouble(txtOctraiAmnt.Text) + Convert.ToDouble(txtDamage.Text)).ToString("N2");
        }

        public void BindValues(DataTable Dt)
        {
            if (Dt != null && Dt.Rows.Count > 0)
            {
                txtGrNo.Text = Convert.ToString(Dt.Rows[0]["GR_No"]);
                ddlRecivr.SelectedValue = Convert.ToString(Dt.Rows[0]["Recivr_Idno"]);
                txtQty.Text = Convert.ToString(Dt.Rows[0]["Qty"]);
                txtWeight.Text = Convert.ToString(Dt.Rows[0]["Tot_Weght"]);
                txtFreight.Text = Convert.ToString(Dt.Rows[0]["Amount"]);
                hidGrIdno.Value = Convert.ToString(Dt.Rows[0]["Gr_Idno"]);
                NetAmntCal();
            }
        }

        public void BindReciver()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var receiverLst = obj.BindSender();
            ddlRecivr.DataSource = receiverLst;
            ddlRecivr.DataTextField = "acnt_name";
            ddlRecivr.DataValueField = "acnt_idno";
            ddlRecivr.DataBind();
            ddlRecivr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                ddlToCity.DataSource = lst;
                ddlToCity.DataTextField = "City_Name";
                ddlToCity.DataValueField = "City_Idno";
                ddlToCity.DataBind();

            }
            ddlToCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlToCity.DataSource = FrmCity;
            ddlToCity.DataTextField = "CityName";
            ddlToCity.DataValueField = "cityidno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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

                    txtGRDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFromDiv.Text = Convert.ToString(hidmindate.Value);
                    txtDateToDiv.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtGRDate.Text = hidmindate.Value;
                    txtDateFromDiv.Text = Convert.ToString(hidmindate.Value);
                    txtDateToDiv.Text = hidmaxdate.Value;
                }
            }
        }

        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }

        private void Populate(int HeadIdno)
        {
            FreightMemoDAL objItemMast = new FreightMemoDAL();
            tblFreightMemo objitmMast = objItemMast.SelectById(HeadIdno);
            objItemMast = null;
            if (objitmMast != null)
            {
                hidItemidno.Value = Convert.ToString(objitmMast.FMemo_Idno);
                ddldateRange.SelectedValue = Convert.ToString(objitmMast.Year_Idno);
                ddlToCity.SelectedValue = Convert.ToString(objitmMast.ToCity_Idno);
                txtRcptNo.Text = Convert.ToString(objitmMast.Rcpt_No);
                txtGRDate.Text = Convert.ToDateTime(objitmMast.Rcpt_Date).ToString("dd-MM-yyyy");
                ddlRecivr.SelectedValue = Convert.ToString(objitmMast.Receiver_Idno);
                txtGrNo.Text = Convert.ToString(objitmMast.Gr_No);
                txtQty.Text = Convert.ToString(objitmMast.Tot_Qty);
                txtWeight.Text = Convert.ToDouble(objitmMast.Tot_Weight).ToString("N2");
                txtFreight.Text = Convert.ToDouble(objitmMast.Freight_Amnt).ToString("N2");
                txtServiceCharg.Text = Convert.ToDouble(objitmMast.Service_Amnt).ToString("N2");
                txtLabbrCharg.Text = Convert.ToDouble(objitmMast.Labour_Amnt).ToString("N2");
                txtDelvAmnt.Text = Convert.ToDouble(objitmMast.Delivery_Amnt).ToString("N2");
                txtOctraiAmnt.Text = Convert.ToDouble(objitmMast.Octrai_Amnt).ToString("N2");
                txtDamage.Text = Convert.ToDouble(objitmMast.Damage_Amnt).ToString("N2");
                txtNetTotal.Text = Convert.ToDouble(objitmMast.Net_Amnt).ToString("N2");
                hidGrIdno.Value = Convert.ToString(objitmMast.GR_Idno);
                txtremark.Text = Convert.ToString(objitmMast.Remarks);
            }
            FreightMemoo(HeadIdno);
        }

        private void ClearControls()
        {
            txtGrNo.Text = "";
            txtQty.Text = "";
            txtWeight.Text = "0.00";
            txtFreight.Text = "0.00";
            txtServiceCharg.Text = "0.00";
            txtLabbrCharg.Text = "0.00";
            txtDelvAmnt.Text = "0.00";
            txtOctraiAmnt.Text = "0.00";
            txtDamage.Text = "0.00";
            ddlRecivr.SelectedIndex = 0;
            txtNetTotal.Text = "0.00";
            txtremark.Text = "";
            ddldateRange.SelectedIndex = 0;
            MaxRecptNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue));
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        #endregion

        #region  Control Events...
        protected void txtServiceCharg_TextChanged(object sender, EventArgs e)
        {
            if (txtServiceCharg.Text == "")
            {
                txtServiceCharg.Text = "0.00";
            }
            else
            {
                txtServiceCharg.Text = Convert.ToDouble(txtServiceCharg.Text).ToString("N2");
            }
            NetAmntCal();
            txtServiceCharg.Focus();
        }

        protected void txtLabbrCharg_TextChanged(object sender, EventArgs e)
        {
            if (txtLabbrCharg.Text == "")
            {
                txtLabbrCharg.Text = "0.00";
            }
            else
            {
                txtLabbrCharg.Text = Convert.ToDouble(txtLabbrCharg.Text).ToString("N2");
            }
            NetAmntCal();
            txtLabbrCharg.Focus();
        }

        protected void txtDelvAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtDelvAmnt.Text == "")
            {
                txtDelvAmnt.Text = "0.00";
            }
            else
            {
                txtDelvAmnt.Text = Convert.ToDouble(txtDelvAmnt.Text).ToString("N2");
            }
            NetAmntCal();

            txtDelvAmnt.Focus();
        }

        protected void txtOctraiAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtOctraiAmnt.Text == "")
            {
                txtOctraiAmnt.Text = "0.00";
            }
            else
            {
                txtOctraiAmnt.Text = Convert.ToDouble(txtOctraiAmnt.Text).ToString("N2");
            }
            NetAmntCal();
            txtOctraiAmnt.Focus();
        }

        protected void txtDamage_TextChanged(object sender, EventArgs e)
        {
            if (txtDamage.Text == "")
            {
                txtDamage.Text = "0.00";
            }
            else
            {
                txtDamage.Text = Convert.ToDouble(txtDamage.Text).ToString("N2");
            }
            NetAmntCal();
            txtDamage.Focus();
        }

        protected void ddlToCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToCity.SelectedIndex > 0)
            {
                MaxRecptNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlToCity.SelectedValue));
                ClearControls();
            }
            ddlToCity.Focus();
        }
        #endregion

    }
}