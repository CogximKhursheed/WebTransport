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
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{

    public partial class LorryHireSlip : Pagebase
    {

        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; Int32 iFromCity = 0; Int32 totrecords = 0; double dGrAmnt = 0;
        double dblNetAmnt = 0; Int32 iscmbtype = 0; DataSet DsUserPref; string sqlSTR = ""; double dTotQty = 0; double dTotWeight = 0;  double dGrCommiAmnt = 0; double dWithoutWagesAmnt = 0; double dWagesAmnt = 0;
      
        private int intFormId = 11;
        #endregion

        #region Form Load...
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
                this.BindDateRange();
                txtfreight.Attributes.Add("onChange", "SetNumFormt('" + txtfreight.ClientID + "')");
                txtadvance.Attributes.Add("onChange", "SetNumFormt('" + txtadvance.ClientID + "')");
                txtnetamnt.Attributes.Add("onChange", "SetNumFormt('" + txtnetamnt.ClientID + "')");
                txtothercharges.Attributes.Add("onChange", "SetNumFormt('" + txtothercharges.ClientID + "')");
                this.BindLorry();
                txtslipdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                SetDate();
                if (Request.QueryString["Lh"] != null)
                {
                    Populate(Convert.ToInt32(Request.QueryString["Lh"]));
                    PrintLorryHire(Convert.ToInt32(Request.QueryString["Lh"]));
                    lnkBtnNew.Visible = true;
                    lnkbtnPrint.Visible = true;
                     
                }
                else
                {
                     lnkBtnNew.Visible = false;
                     lnkbtnPrint.Visible = false;
                    
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    LorryHireDAL obj = new LorryHireDAL();
                    var lst = obj.SelectCityCombo();
                    obj = null;
                    ddlFromCity.DataSource = lst;
                    ddlFromCity.DataTextField = "City_Name";
                    ddlFromCity.DataValueField = "City_Idno";
                    ddlFromCity.DataBind();
                    ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    //this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }

            }
            ddlDateRange.Focus();


        }

        #endregion

        #region Functions...
        private void Populate(Int32 slipidno)
        {
            LorryHireDAL obj1 = new LorryHireDAL();
            tblLorryHireSlip obj = obj1.SelectById(slipidno);
           
            
            if (obj != null)
            {
                hidlorryhireidno.Value = Convert.ToString(obj.LryHire_Idno);
                ddlDateRange.SelectedValue = Convert.ToString(obj.Year_Idno);
                ddlDateRange_SelectedIndexChanged(null, null);
                ddlDateRange.Enabled = false;
                txtslipdate.Text = Convert.ToDateTime(obj.Lry_Date).ToString("dd-MM-yyyy");
                txtslipno.Text = Convert.ToString(obj.Lry_SlipNo);
                ddlFromCity.SelectedValue = Convert.ToString(obj.Loc_Idno);
                ddllorryno.SelectedValue = Convert.ToString(obj.Lry_Idno);

                ddllorryno_SelectedIndexChanged(null, null);
                txtsupliedby.Text = Convert.ToString(obj.SupliedTo);
                txtfreight.Text = Convert.ToDouble(obj.TotalFrghtAmnt).ToString("N2");
                txtadvance.Text = Convert.ToDouble(obj.AdvanceAmnt).ToString("N2");
                txtnetamnt.Text = Convert.ToDouble(obj.Net_amnt).ToString("N2");
                txtremark.Text = Convert.ToString(obj.Remark);
                txtothercharges.Text = Convert.ToDouble(obj.OtherCharges).ToString("N2");
                txtUnloading.Text = Convert.ToDouble(obj.Unloading).ToString("N2");
                txtDetectionCharges.Text = Convert.ToDouble(obj.DetectionCharges).ToString("N2");
                txtDiesel.Text = Convert.ToDouble(obj.Diesel).ToString("N2");
                txtTds.Text = Convert.ToDouble(obj.TDS).ToString("N2");
            }
            DtTemp = obj1.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), slipidno);
            obj1 = null;
            ViewState["dt"] = DtTemp;
            this.BindGrid();
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindLorry()
        {
            LorryHireDAL obj = new LorryHireDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddllorryno.DataSource = lst;
                ddllorryno.DataTextField = "Lorry_No";
                ddllorryno.DataValueField = "Lorry_Idno";
                ddllorryno.DataBind();
            }
            ddllorryno.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        /// <summary>
        /// To Clear all controls
        /// </summary>
        private void ClearControls()
        {
            txtslipno.Text = "";
            hidlorryhireidno.Value = string.Empty;
            ddlFromCity.SelectedValue = "0";
            ddllorryno.SelectedValue = "0";
            ddllorryno_SelectedIndexChanged(null, null);
            ddlDateRange.SelectedIndex = 0; ;
            txtsupliedby.Text = "";
            txtnetamnt.Text = "0.00";
            txtadvance.Text = "0.00";
            txtfreight.Text = "0.00";
            txtothercharges.Text = "0.00";
            txtUnloading.Text = "0.00";
            txtDiesel.Text = "0.00";
            txtTds.Text = "0.00";
            ddlDateRange.Enabled = true;
            ddlDateRange.SelectedIndex = 0;
            lnkBtnNew.Visible = false;
            
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
        //private void BindCity()
        //{
        //    LorryHireDAL obj = new LorryHireDAL();
        //    var lst = obj.SelectCity();
        //    obj = null;

        //    ddlToCity.DataSource = lst;
        //    ddlToCity.DataTextField = "City_Name";
        //    ddlToCity.DataValueField = "City_Idno";
        //    ddlToCity.DataBind();
        //    ddlToCity.Items.Insert(0, new ListItem("--Select--", "0"));

        //}
        private void HireSlipNo(Int32 YearIdno, Int32 FromCityIdno)
        {
            LorryHireDAL obj = new LorryHireDAL();
            txtslipno.Text = Convert.ToString(obj.MaxLorrySlipNo(YearIdno, FromCityIdno));
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
                    txtslipdate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtslipdate.Text = hidmindate.Value;
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                    txtDateTo.Text = hidmindate.Value;
                }
            }

        }
       

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RateMasterFTList.xls"));
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
        private void netamntcal()
        {
            try
            {
                double totalfreight = (Convert.ToDouble(txtfreight.Text)) + (Convert.ToDouble(txtothercharges.Text)) + (Convert.ToDouble(txtDetectionCharges.Text)) + (Convert.ToDouble(txtUnloading.Text));
                double Totaladvance=(Convert.ToDouble(txtadvance.Text)) + (Convert.ToDouble(txtDiesel.Text)) + (Convert.ToDouble(txtTds.Text));

                if ((totalfreight > 0) && totalfreight > Totaladvance )
                {
                    txtfreight.Text = Convert.ToDouble(txtfreight.Text).ToString("N2");
                   
                }
                else
                {
                    txtfreight.Text = "0.00";
                    ShowMessage("Freight Amount Should Be Greater Then Advance Amount");

                    if ((Totaladvance > 0) && (Totaladvance < totalfreight))
                    {
                        txtadvance.Text = Convert.ToDouble(txtadvance.Text).ToString("N2");
                    }
                    else
                    {
                        txtadvance.Text = "0.00";
                        ShowMessage("Advance Amount Should Be Less Then Freight Amount");
                    }
                }
                
              
                txtnetamnt.Text = Convert.ToDouble(totalfreight - Totaladvance).ToString("N2");
                if (Convert.ToDouble(txtnetamnt.Text) < 0)
                {
                    txtnetamnt.Text = "0.00";
                }
            }
            catch (Exception Ex)
            { }
        }
        private void BindGrid()
        {
            ChlnBookingDAL objChlnBookingDAL = new ChlnBookingDAL();
            tblUserPref obj = objChlnBookingDAL.selectUserPref();
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
        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }
        #endregion

        #region Grid Event

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion

        #region Control Event.....

        protected void ddllorryno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddllorryno.SelectedIndex > 0))
                {
                    LorryHireDAL obj = new LorryHireDAL();
                    obj.selectOwnerName(Convert.ToInt32(ddllorryno.SelectedValue));
                    var lst = obj.selectOwnerName(Convert.ToInt32(ddllorryno.SelectedValue));
                    if (lst != null)
                    {
                        txtsupliedby.Text = Convert.ToString(lst.Owner_Name + '-' + ((lst.Pan_No == null) ? "" : lst.Pan_No) + "-" + ((lst.Lorry_Type == 0) ? "O" : "H"));
                        // hidOwnerId.Value = Convert.ToString(lst.Prty_Idno);
                    }


                }
                else
                {
                    txtsupliedby.Text = "";
                }

            }

            catch (Exception Ex)
            {

            }
            ddllorryno.Focus();
        }

        protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDateRange.SelectedValue) != 0 || Convert.ToUInt32(ddlFromCity.SelectedValue) != 0)
            {
                this.HireSlipNo(Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
            }
            ddlFromCity.Focus();
        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedIndex >= 0)
            {
                SetDate();
            }
            ddlDateRange.Focus();
        }
        protected void txtfreight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double advance = 0, freight;
                freight = Convert.ToDouble(txtfreight.Text.Trim());
                advance = Convert.ToDouble(txtadvance.Text.Trim());
                //if ((freight > 0) && freight > advance)
                //{
                //    txtfreight.Text = Convert.ToDouble(txtfreight.Text).ToString("N2");
                //}
                //else
                //{
                //    txtfreight.Text = "0.00";
                //    ShowMessage("Freight Amount Should Be Greater Then Freight Amount");

                //}
                netamntcal();
            }
            catch (Exception Ex)
            {

            }
            txtfreight.Focus();
        }

        protected void txtadvance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double advance = 0, freight;
                freight = Convert.ToDouble(txtfreight.Text.Trim());
                advance = Convert.ToDouble(txtadvance.Text.Trim());
                //if ((advance > 0) && (advance < freight))
                //{
                //    txtadvance.Text = Convert.ToDouble(txtadvance.Text).ToString("N2");
                //}
                //else
                //{
                //    txtadvance.Text = "0.00";
                //    ShowMessage("Advance Amount Should Be Less Then Freight Amount");
                //}
                netamntcal();
            }
            catch (Exception Ex)
            {

            }
            txtadvance.Focus();
        }

        protected void txtothercharges_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        protected void txtUnloading_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        protected void txtDetectionCharges_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        protected void txtTds_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        protected void txtDiesel_TextChanged(object sender, EventArgs e)
        {
            netamntcal();
        }
        
        #endregion

        #region Button Event
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {

            string msg = "";
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp != null)
            {
                if (DtTemp.Rows.Count <= 0)
                {
                    ShowMessageErr("Please enter details");
                    return;
                }
            }
            if (grdMain.Rows.Count <= 0)
            {
                ShowMessageErr("Please enter details");
                return;
            }

            string strMsg = string.Empty;
            LorryHireDAL objlorry = new LorryHireDAL();
            Int64 intSlip = 0;
            DateTime? slipDate = null;
            slipDate = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtslipdate.Text));
            DateTime DateAdded = System.DateTime.Now;
            Int64 yearId = Convert.ToInt32(string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? 0 : Convert.ToInt64(ddlDateRange.SelectedValue));
            if (txtfreight.Text == string.Empty || Convert.ToDouble(txtfreight.Text) <= 0)
            {
                this.ShowMessage("Freight Amount must be greater than 0!"); txtfreight.Focus(); txtfreight.SelectText();
                return;
            }
            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {


                if (string.IsNullOrEmpty(hidlorryhireidno.Value) == true)
                {
                    intSlip = objlorry.Insert(string.IsNullOrEmpty(txtslipno.Text) ? 0 : Convert.ToInt64(txtslipno.Text), slipDate, string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(ddllorryno.SelectedValue) ? 0 : Convert.ToInt64(ddllorryno.SelectedValue), txtsupliedby.Text.Trim(), yearId, Convert.ToDouble(txtfreight.Text), Convert.ToDouble(txtadvance.Text), Convert.ToDouble(txtnetamnt.Text), DateAdded, txtremark.Text.Trim(), Convert.ToDouble(txtothercharges.Text), Convert.ToDouble(txtUnloading.Text), Convert.ToDouble(txtDetectionCharges.Text), Convert.ToDouble(txtDiesel.Text), Convert.ToDouble(txtTds.Text), DtTemp);

                }
                else
                {
                    intSlip = objlorry.Update(Convert.ToInt32(hidlorryhireidno.Value), string.IsNullOrEmpty(txtslipno.Text) ? 0 : Convert.ToInt64(txtslipno.Text), slipDate, string.IsNullOrEmpty(ddlFromCity.SelectedValue) ? 0 : Convert.ToInt64(ddlFromCity.SelectedValue), string.IsNullOrEmpty(ddllorryno.SelectedValue) ? 0 : Convert.ToInt64(ddllorryno.SelectedValue), txtsupliedby.Text.Trim(), yearId, Convert.ToDouble(txtfreight.Text), Convert.ToDouble(txtadvance.Text), Convert.ToDouble(txtnetamnt.Text), DateAdded, txtremark.Text.Trim(), Convert.ToDouble(txtothercharges.Text), Convert.ToDouble(txtUnloading.Text), Convert.ToDouble(txtDetectionCharges.Text), Convert.ToDouble(txtDiesel.Text), Convert.ToDouble(txtTds.Text),DtTemp);
                    }
                    objlorry = null;

                    if (intSlip > 0)
                    {
                        if (string.IsNullOrEmpty(hidlorryhireidno.Value) == false)
                        {
                            Tran.Complete();
                            strMsg = "Record updated successfully.";
                            grdMain.DataSource = null;
                            grdMain.DataBind();
                        }
                        else
                        {
                            Tran.Complete();
                            strMsg = "Record saved successfully.";
                        }
                         ClearControls();

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hidlorryhireidno.Value) == false)
                        {
                            Tran.Dispose();
                            strMsg = "Record not updated.";
                        }
                        else
                        {
                            Tran.Dispose();
                            strMsg = "Record not saved.";
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }

            }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidlorryhireidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt32(hidlorryhireidno.Value) == 0 ? 0 : Convert.ToInt32(hidlorryhireidno.Value));
            }
        }

        protected void lnkBtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("LorryHireSlip.aspx");
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ddllorryno.SelectedIndex <= 0 && ddlFromCity.SelectedIndex <=0)
            {
                ShowMessageErr("Please Select Lorry No & Location.");
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
        }
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                LorryHireDAL obj = new LorryHireDAL();
                Int64 LorryIdno = Convert.ToInt64(ddllorryno.SelectedValue);
                Int32 ifromCityIdno=Convert.ToInt32(ddlFromCity.SelectedValue);
                DataTable DsGrdetail;

                   DsGrdetail = obj.SelectGRDetailTruckNo("SelectGRDetailTruckNo", Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(txtDateFrom.Text.Trim()), Convert.ToString(txtDateTo.Text), ifromCityIdno, LorryIdno, ApplicationFunction.ConnectionString());
                
            

                if ((DsGrdetail != null) && (DsGrdetail.Rows.Count > 0))
                {
                    grdGrdetals.DataSource = DsGrdetail;
                    grdGrdetals.DataBind();
                    lnkbtnSubmit.Visible = true; lnkbtnCloase.Visible = true;

                }
                else
                {
                    grdGrdetals.DataSource = null;
                    grdGrdetals.DataBind();
                    lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                lnkbtnSearch.Focus();
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                BindDropdownDAL objbind1 = new BindDropdownDAL();
                if ((grdGrdetals != null) && (grdGrdetals.Rows.Count > 0))
                {
                    string strchkValue = string.Empty; string sAllItemIdnos = string.Empty;
                    string strchkDetlValue = string.Empty;
                    for (int count = 0; count < grdGrdetals.Rows.Count; count++)
                    {
                        CheckBox ChkGr = (CheckBox)grdGrdetals.Rows[count].FindControl("chkId");
                        if ((ChkGr != null) && (ChkGr.Checked == true))
                        {
                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                        }
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        ShowMessageErr("Please select atleast one Gr.");
                        ShowDiv("ShowClient('dvGrdetails')");
                    }
                    
                    else
                    {
                        LorryHireDAL ObjChlnBookingDAL = new LorryHireDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable();
                        DataTable dtKMDetl = new DataTable(); 
                        dtRcptDetl = ObjChlnBookingDAL.SelectGrChallanDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;
                        BindGrid();
                        grdGrdetals.DataSource = null;
                        grdGrdetals.DataBind();
                        if (grdMain.Rows.Count > 0)
                        {
                            foreach (GridViewRow Dr in grdMain.Rows)
                            {
                                Label lblSubTotAmnt = (Label)Dr.FindControl("lblSubTotAmnt");
                                dGrAmnt += Convert.ToDouble(lblSubTotAmnt.Text);
                            }
                        }
                        txtfreight.Text = Convert.ToString(dGrAmnt);
                        netamntcal();
                       
                        
                    }
  
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                  //  ddlDelvryPlace.Enabled = true;
                    ShowDiv("ShowBillAgainst('dvGrdetails')");
                }
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        protected void lnkbtnCloase_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);
           // ddldelvplace.SelectedIndex = 0;
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind(); lnkbtnSubmit.Visible = false; lnkbtnCloase.Visible = false;
           // ddlTruckNo.Focus();
        }
        #endregion
        #region Grid Events....
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt = 0; double dQtty = 0; double dWieght = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SubTot_Amnt"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dQtty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotQty = dQtty + dTotQty;
                dWieght = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Tot_Weght"));
                dTotWeight = dWieght + dTotWeight;
      
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblTChallanAmnt.Text = dblNetAmnt.ToString("N2");
                txtfreight.Text = dblNetAmnt.ToString("N2");
                Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
                lblTotQty.Text = dTotQty.ToString("N2");
                Label lblTotWeigh = (Label)e.Row.FindControl("lblTotWeigh");
                lblTotWeigh.Text = dTotWeight.ToString();
                
            }
        }

        
        #endregion

        #region Print...
        private void PrintLorryHire(Int64 ChlnHeadIdno)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; //string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
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

            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            lblDate.Text = txtslipdate.Text;
            lblSlipNo.Text = txtslipno.Text;

            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spHireLorryDetl] @ACTION='Print',@Id='" + ChlnHeadIdno + "'");
            dsReport.Tables[0].TableName = "HrPrinthead";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Total_Freight"]) != null)
                {
                    lblFreight.Text = "Rs." + "" + Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Total_Freight"]);
                }
                else
                {
                    lblFreight.Text = "";
                }
                if (Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["AdvanceAmount"]) != null)
                {
                    lbladvPaid.Text = "Rs." + "" + Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["AdvanceAmount"]);
                }
                else
                {
                    lbladvPaid.Text = "";
                }
                if (Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["TDS"]) != null)
                {
                    lblTds.Text = "Rs." + "" + Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["TDS"]);
                }
                else
                {
                    lblTds.Text = "";
                }
                if (Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["TDS"]) != null)
                {
                    lblTds.Text = "Rs." + "" + Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["TDS"]);
                }
                else
                {
                    lblTds.Text = "";
                }
                if (Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Total_Freight"]) != null)
                {
                    lblTotalFreight.Text = "Rs." + "" + Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Total_Freight"]);
                }
                else
                {
                    lblTotalFreight.Text = "";
                }
               
                lblRemark.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Remark"]);
                lblOName.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Owner_Name"]);
                lblOAddress.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Owner_Name"]);
                lblOpanno.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["OwnerPan_No"]);
                lblEngineno.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Engine_No"]);
                lblChasisNo.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Chasis_No"]);
                lblDriverAddress.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Driver_NameAddre"]);
                lblLicnenceNo.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Driver_LicenceNo"]);
                lblMake.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Lorry_Make"]);
                lblIns.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Ins_no"]);
                lblFitness.Text = Convert.ToDateTime(dsReport.Tables["HrPrinthead"].Rows[0]["FitnessDate"]).ToString("dd-MM-yyyy");
                lblBrokerName.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Broker_name"]);
                lblAddress.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Broker_Address"]);
                lblBPanNo.Text = Convert.ToString(dsReport.Tables["HrPrinthead"].Rows[0]["Broker_PanNo"]);
               


            }
            DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spHireLorryDetl] @ACTION='Print',@Id='" + ChlnHeadIdno + "'");
            dsReportDetl.Tables[0].TableName = "HrPrintdetl";
            if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = dsReportDetl;
                Repeater1.DataBind();
                
            }
            #region Terms&condition..
            DataSet HireslipTerms = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select HireslipTerms from tbluserpref");
            if (HireslipTerms != null)
            {
                lbltTerms.Visible = true;
                lblTermsCond.Visible = true;
                lblTermsCond.Text = Convert.ToString(HireslipTerms.Tables[0].Rows[0]["HireslipTerms"]); 
            }
            else
            {
                lbltTerms.Visible = false;
                lblTermsCond.Visible = false;
            }
            #endregion

        }
        #endregion


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    //  gives the sum in string Total.                 
            //    dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
            //    dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
            //    dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            //    dfWithoutWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "WithoutUnloading_Amnt"));
            //    dfWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
            //}
            //else if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    //The following label displays the total
            //    Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            //    Label lbltotalWeight = (Label)e.Item.FindControl("lbltotalWeight");
            //    Label lbltotalqty = (Label)e.Item.FindControl("lbltotalqty");
            //    Label lblWagesAmnt = (Label)e.Item.FindControl("lblWagesAmnt");
            //    Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            //    lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
            //    lbltotalWeight.Text = dtotwght.ToString("N2");
            //    lbltotalqty.Text = dqtnty.ToString();
            //    lblAmount.Text = dfWithoutWagesAmnt.ToString("N2");
            //    lblWagesAmnt.Text = dfWagesAmnt.ToString("N2");

            //}
        }

    }
 }
