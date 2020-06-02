using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.Transactions;

namespace WebTransport
{
    public partial class ChallanDelverd : Pagebase
    {
        #region Private Variable...
        //static UserSessionValue USessionValue = new UserSessionValue();
        //static FinYear UFinYear = new FinYear();
        string SSnd = ConfigurationManager.AppSettings["SSnd"];
        DataTable dt = new DataTable();
        int dTotIssueQty = 0; double dTotRcptQty = 0; double dtotWeight = 0;
        double dTotIssueAmnt = 0; DataTable DtTemp = new DataTable();Double RecFooAmt=0.00,RecFooQty=0.00,RecFooWt=0.00;
        private int intFormId = 35; double LocGrAmnt = 0; double OutGrAmnt=0;
        #endregion

        #region Page events...
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionValues svalue = new SessionValues();
          //  base.UsessionValue = svalue.FetchUsersAndComp(string.IsNullOrEmpty(Convert.ToString(Session["Visageuseridno"])) ? 0 : Convert.ToInt32(Session["Visageuseridno"]));
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
                    lnkBtnSave.Visible = false;
                }
                if (base.View == false)
                {
                    lblViewList.Visible = false;
                }
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                this.BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BidFromCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
              
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                tblUserPref obj = objChallanDelverdDAL.selectUserPref();
                ddlToCity.SelectedValue = Convert.ToString(base.UserFromCity);
                BindChlnNo();
                ViewState["dt"] = null;
                if (Request.QueryString["chlnrcptidno"] != null)
                {
                    lnkSubmit.Visible = false; ddlChallanDetl.Enabled = false; DivRcptNo.Visible = true; txtRcptNo.Visible = true; ddlToCity.Enabled = false;
                    
                    this.Populate(Convert.ToInt32(Request.QueryString["chlnrcptidno"]));
                    this.Title = "Edit Delivery Challan";
                    lnkBtnNew.Visible = true;
                }
                else
                {
                    lnkSubmit.Visible = true; ddlChallanDetl.Enabled = true; txtRcptNo.Visible = false; DivRcptNo.Visible = false; lblRcptDtNo.Text = "Receipt Date"; ddlToCity.Enabled = true;
                    lnkBtnNew.Visible = false;
                }
               ddlDateRange.Focus();
            }
            svalue = null;
        }
        #endregion

        #region Button Events...
        //protected void imgBtnNew_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("ChallanDelverd.aspx");
        //}
        //protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        //{
        //    string msg = string.Empty; bool bflag = false; double IssueQty = 0;
        //    if (grdMain.Rows.Count <= 0)
        //    {
        //        msg = "Please Enter Challan Details";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        //        return;
        //    }
        //    else
        //    {
        //        DtTemp = (DataTable)ViewState["dt"];
        //        DtTemp = CreateDt();
        //        ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
        //        Int64 MtrlRcptId = 0, MtrlRcptDetlId = 0;
        //        string strMsgTyp = String.Empty;
        //        int yearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);

        //        Int64 Value = 0;
        //        tblChlnDelvHead ObjMtrlRcptHead = new tblChlnDelvHead();
        //        ObjMtrlRcptHead.ChlnDelv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
        //        ObjMtrlRcptHead.ToCity_Idno = Convert.ToInt32(ddlToCity.SelectedValue);
        //        ObjMtrlRcptHead.ChlnTransf_Idno = Convert.ToInt32(ddlChallanDetl.SelectedValue);
        //        ObjMtrlRcptHead.Date_Added = ApplicationFunction.GetIndianDateTime().Date;   // DateTime.Now.Date;
        //        ObjMtrlRcptHead.Date_Modified = ApplicationFunction.GetIndianDateTime().Date;
        //        ObjMtrlRcptHead.ChlnDelv_No = MaxRcptHeadNo(Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue));
        //        ObjMtrlRcptHead.CrsngGR_Amnt = Convert.ToDouble(txtoutGrAmnt.Text);
        //        ObjMtrlRcptHead.LocGR_Amnt = Convert.ToDouble(txtLocGrAmnt.Text);
        //        ObjMtrlRcptHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
        //        ObjMtrlRcptHead.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
        //        foreach (GridViewRow row in grdMain.Rows)
        //        {
        //            HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGridno");
        //            HiddenField hidItemidno = (HiddenField)row.FindControl("hidItemidno");
        //            HiddenField hidUnitidno = (HiddenField)row.FindControl("hidUnitidno");
        //            Label lblrateType = (Label)row.FindControl("lblrateType");
        //            Label lblQty = (Label)row.FindControl("lblQty");
        //            Label lblWeight = (Label)row.FindControl("lblWeight");
        //            Label lblAmount = (Label)row.FindControl("lblAmount");
        //            TextBox txtRecQty = (TextBox)row.FindControl("txtRecQty");
        //            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
        //            if (lblrateType.Text == "Rate")
        //            {
        //                IssueQty = Convert.ToDouble(lblQty.Text);
        //            }
        //            else
        //            {
        //                IssueQty = Convert.ToDouble(lblWeight.Text);
        //            }
        //            if (IssueQty < Convert.ToDouble(txtRecQty.Text))
        //            {
        //                strMsgTyp = "RcptQtyGr"; txtRecQty.Text = "0.00";
        //                txtRecQty.Focus();
        //                return;
        //            }
        //            if (txtRecQty.Text == "")
        //            {
        //                strMsgTyp = "qtyblnk";
        //                MtrlRcptDetlId = 0; txtRecQty.Focus();
        //                return;
        //            }
        //            ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemidno.Value, hidUnitidno.Value, ((lblrateType.Text) == "Rate") ? "1" : "2", lblQty.Text, lblWeight.Text,
        //                                                 lblAmount.Text, txtRecQty.Text, txtRemark.Text);

        //        }
        //        using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            try
        //            {
        //                ChallanDelverdDAL obj = new ChallanDelverdDAL();
        //                DateTime dMtrlDt = obj.MtrlTransfDate(Convert.ToInt64(ddlChallanDetl.SelectedValue));
        //                hidMtrlTrnsfDt.Value = dMtrlDt.ToString("dd-MM-yyyy");
        //                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidMtrlTrnsfDt.Value)) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)))
        //                {
        //                    txtDate.Focus();
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('Delivery Date must be greater than or equal to Challan Date [" + dMtrlDt.Date.ToString("dd-MMM-yyyy") + "].')", true);
        //                    return;
        //                }
        //                if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
        //                {

        //                    MtrlRcptId = obj.InsertMtrlRcptHOHead(ObjMtrlRcptHead, DtTemp);
        //                    if (MtrlRcptId > 0)
        //                    {
        //                        tScope.Complete();
        //                        msg = "Record Saved Successffully";
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        //                    }
        //                }
        //                else            // Update
        //                {
                            
        //                    MtrlRcptId = obj.UpdateMtrlRcptHOHead(ObjMtrlRcptHead, DtTemp, Convert.ToInt32(hidMtrlRcptid.Value));
        //                    if ((MtrlRcptId > 0))
        //                    {
        //                        tScope.Complete();
        //                        msg = "Record Update Successffully"; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        //                        txtDate.Focus();
        //                    }
        //                }
        //            }

        //            catch (Exception ex)
        //            {
        //                // ApplicationFunction.ErrorLog(ex.ToString());
        //            }
        //        }

        //        if (MtrlRcptId == -1)
        //        {
        //            msg = "Challan No. already  Delivered !"; bflag = false; strMsgTyp = "RecExst"; ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        //            ddlChallanDetl.Focus();
        //        }
        //        else
        //        {
        //            ClearControls();
        //        }


        //    }
        //}
        //protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
        //    {
        //        this.ClearControls();
        //    }
        //    else
        //    {
        //        lnkSubmit.Visible = false; ddlChallanDetl.Enabled = false; txtRcptNo.Visible = true; DivRcptNo.Visible = true;
                
        //        this.Populate(Convert.ToInt32(hidMtrlRcptid.Value));
        //        this.Title = "Edit Delivery Challan";
        //    }
        //}
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkBtnNew_Click(object sender, EventArgs e)
        {

            Response.Redirect("ChallanDelverd.aspx");
        }
        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty; bool bflag = false; double IssueQty = 0,IssueWT=0;
            if (grdMain.Rows.Count <= 0)
            {
                ShowMessage("Please Enter Challan Details");

            }
            else
            {

                DtTemp = (DataTable)ViewState["dt"];
                DtTemp = CreateDt();
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                Int64 MtrlRcptId = 0, MtrlRcptDetlId = 0;
                string strMsgTyp = String.Empty;
                int yearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);

                Int64 Value = 0;
                tblChlnDelvHead ObjMtrlRcptHead = new tblChlnDelvHead();
                ObjMtrlRcptHead.ChlnDelv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                ObjMtrlRcptHead.ToCity_Idno = Convert.ToInt32(ddlToCity.SelectedValue);
                ObjMtrlRcptHead.ChlnTransf_Idno = Convert.ToInt32(ddlChallanDetl.SelectedValue);
                ObjMtrlRcptHead.Date_Added = ApplicationFunction.GetIndianDateTime().Date;   // DateTime.Now.Date;
                ObjMtrlRcptHead.Date_Modified = ApplicationFunction.GetIndianDateTime().Date;
                ObjMtrlRcptHead.ChlnDelv_No = MaxRcptHeadNo(Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue));
                ObjMtrlRcptHead.CrsngGR_Amnt = Convert.ToDouble(txtoutGrAmnt.Text);
                ObjMtrlRcptHead.LocGR_Amnt = Convert.ToDouble(txtLocGrAmnt.Text);
                ObjMtrlRcptHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                ObjMtrlRcptHead.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
                foreach (GridViewRow row in grdMain.Rows)
                {
                    HiddenField hidGrIdno = (HiddenField)row.FindControl("hidGridno");
                    HiddenField hidItemidno = (HiddenField)row.FindControl("hidItemidno");
                    HiddenField hidUnitidno = (HiddenField)row.FindControl("hidUnitidno");
                    Label lblrateType = (Label)row.FindControl("lblrateType");
                    Label lblQty = (Label)row.FindControl("lblQty");
                    Label lblWeight = (Label)row.FindControl("lblWeight");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    TextBox txtRecQty = (TextBox)row.FindControl("txtRecQty");
                    TextBox txtRecWT = (TextBox)row.FindControl("txtRecWT");
                    TextBox txtRecWt = (TextBox)row.FindControl("txtRecWt");
                    TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                    TextBox txtRecAmt = (TextBox)row.FindControl("txtRecAmt");
                    if (lblrateType.Text == "Rate")
                    {
                        if (txtRecQty.Text == "")
                        {
                            ShowMessageErr("Please Enter Receiving QTY!");
                            return;
                        }
                        if (Convert.ToDouble(txtRecQty.Text) < 0.00)
                        {
                            ShowMessageErr("You Cannot Enter Receiving QTY Less than 1!");
                            return;
                        }
                         
                        IssueQty = Convert.ToDouble(lblQty.Text);
                        if (IssueQty < Convert.ToDouble(txtRecQty.Text))
                        {
                            txtRecQty.Focus();
                            txtRecQty.Text = "0.00";
                            ShowMessageErr("You Cannot Enter Receiving QTY more than GR QTY!");
                            return;
                        }
                        
                    }
                    else
                    {

                        if (txtRecWT.Text == "")
                        {
                            ShowMessageErr("Please Enter Receiving Weight!");
                            return;
                        }
                        if (Convert.ToDouble(txtRecWT.Text) < 0.00)
                        {
                            ShowMessageErr("You Cannot Enter Receiving Weight Less than 1!");
                            return;
                        }
                        IssueWT = Convert.ToDouble(lblWeight.Text);
                        if (IssueWT < Convert.ToDouble(txtRecWT.Text))
                        {
                            txtRecWT.Focus(); txtRecWT.Text = "0.00";
                            ShowMessageErr("You Cannot Enter Receiving Weight more than GR Weight!");
                            return;
                        }

                    }
                    ApplicationFunction.DatatableAddRow(DtTemp, hidGrIdno.Value, hidItemidno.Value, hidUnitidno.Value, ((lblrateType.Text) == "Rate") ? "1" : "2", lblQty.Text, lblWeight.Text,
                                                         lblAmount.Text, txtRecQty.Text,txtRecWt.Text,txtRecAmt.Text, txtRemark.Text);

                }
                using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        ChallanDelverdDAL obj = new ChallanDelverdDAL();
                        DateTime dMtrlDt = obj.MtrlTransfDate(Convert.ToInt64(ddlChallanDetl.SelectedValue));
                        hidMtrlTrnsfDt.Value = dMtrlDt.ToString("dd-MM-yyyy");
                        if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidMtrlTrnsfDt.Value)) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)))
                        {
                            txtDate.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('Delivery Date must be greater than or equal to Challan Date [" + dMtrlDt.Date.ToString("dd-MMM-yyyy") + "].')", true);
                            return;
                        }
                        if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
                        {

                            MtrlRcptId = obj.InsertMtrlRcptHOHead(ObjMtrlRcptHead, DtTemp);
                            if (MtrlRcptId > 0)
                            {
                                tScope.Complete();

                            }
                        }
                        else
                        {
                            tblChlnDelvHead ObjMtrlRcptHeadU = new tblChlnDelvHead();
                            ObjMtrlRcptHeadU.ChlnDelv_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
                            ObjMtrlRcptHeadU.ToCity_Idno = Convert.ToInt32(ddlToCity.SelectedValue);
                            ObjMtrlRcptHeadU.ChlnTransf_Idno = Convert.ToInt32(ddlChallanDetl.SelectedValue);
                            ObjMtrlRcptHeadU.Date_Modified = ApplicationFunction.GetIndianDateTime().Date;
                            ObjMtrlRcptHeadU.CrsngGR_Amnt = Convert.ToDouble(txtoutGrAmnt.Text);
                            ObjMtrlRcptHeadU.LocGR_Amnt = Convert.ToDouble(txtLocGrAmnt.Text);
                            ObjMtrlRcptHeadU.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
                            ObjMtrlRcptHeadU.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
                            MtrlRcptId = obj.UpdateMtrlRcptHOHead(ObjMtrlRcptHeadU, DtTemp, Convert.ToInt32(hidMtrlRcptid.Value));
                            if ((MtrlRcptId > 0))
                            {
                                tScope.Complete();
                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        // ApplicationFunction.ErrorLog(ex.ToString());
                    }
                }
                if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
                {


                    if (MtrlRcptId > 0)
                    {
                        ShowMessage("Record Saved Successffully");

                    }
                }
                else
                {

                    if ((MtrlRcptId > 0))
                    {

                        ShowMessage("Record Update successfully");

                    }
                }
                if (MtrlRcptId == -1)
                {
                    ShowMessage("Challan No. already  Delivered !");
                    bflag = false;
                    strMsgTyp = "RecExst";
                    ddlChallanDetl.Focus();
                }
                else
                {
                    ClearControls();
                }
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dTotIssueQty = dTotIssueQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotIssueAmnt = dTotIssueAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dTotRcptQty = dTotRcptQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Delv_Qty"));
                dtotWeight = dtotWeight + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "weight"));
                TextBox txtRecQty = (TextBox)e.Row.FindControl("txtRecQty");
                txtRecQty.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                TextBox txtRecWT = (TextBox)e.Row.FindControl("txtRecWT");
                txtRecWT.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotIssueQty = (Label)e.Row.FindControl("lblTotIssueQty");
                Label lblTotIssueAmnt = (Label)e.Row.FindControl("lblTotAmount");
                Label lblTotRcptQty = (Label)e.Row.FindControl("lblTotRcptQty");
                Label lblTotWeight = (Label)e.Row.FindControl("lblTotWeight");
                lblTotIssueQty.Text = dTotIssueQty.ToString();
                lblTotIssueAmnt.Text = String.Format("{0:0,0.00}", dTotIssueAmnt);
                lblTotRcptQty.Text = dTotRcptQty.ToString("N2");
                lblTotWeight.Text = dtotWeight.ToString("N2");
                ViewState["Amnt"] = lblTotIssueAmnt.Text;
            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "cmdedit")
            //{
            //    this.Populate(Convert.ToInt32(e.CommandArgument));
            //    txtSalePrice.Focus();
            //}
            //else if (e.CommandName == "cmddelete")
            //{
            //    string ItemIdno = "", StateIdno = "";
            //    ItemIdno = drpItemName.SelectedValue; 
            //    StateIdno = drpState.SelectedValue;
            //    RateMastBLL objRateMastBLL = new RateMastBLL();
            //    objRateMastBLL.DeleteRateMast(Convert.ToInt32(e.CommandArgument));
            //    objRateMastBLL = null;
            //    this.ClearControls();
            //    drpItemName.SelectedValue = ItemIdno;
            //    drpState.SelectedValue = StateIdno;
            //    BindGrid();
            //    drpItemName.Focus();
            //}
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion

        #region Functions....
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindLocFromByUserId(UserIdno);
            ddlToCity.DataSource = FrmCity;
            ddlToCity.DataTextField = "City_Name";
            ddlToCity.DataValueField = "City_Idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataSource = lst;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }

           private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Item_Idno", "String", "Unit_Idno", "String", "Rate_Type", "String", "Qty", "String", "Weight", "String", "Amount", "String", "Delv_Qty", "String", "Delv_Wt", "String","Delv_Amount","String" ,"Reamrk", "String");
            return dttemp;
        }

        public DateTime MtrlTransfDate(Int64 MtrlTransfIdno)
        {
            DateTime Value;
            ChallanDelverdDAL objMtrlRcptHODAL = new ChallanDelverdDAL();
            Value = objMtrlRcptHODAL.MtrlTransfDate(MtrlTransfIdno);
            objMtrlRcptHODAL = null;
            return Value;
        }

        public Int64 MaxRcptHeadNo(int fromcity, int YearIdno)
        {
            ChallanDelverdDAL objMtrlRcptHODAL = new ChallanDelverdDAL();
            Int64 RecHeadNo = objMtrlRcptHODAL.MaxRcptHeadNo(fromcity, YearIdno);
            objMtrlRcptHODAL = null;
            return RecHeadNo;
        }

        private void BidFromCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;
            ddlToCity.DataSource = lst;
            ddlToCity.DataTextField = "City_Name";
            ddlToCity.DataValueField = "City_Idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new ListItem("< Select >", "0"));
        }

        private void BindChlnNo()
        {
                Int32 fromCityIdno = Convert.ToInt32(ddlToCity.SelectedValue);
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                int Typ = 0;
                if (Request.QueryString["chlnrcptidno"] != null)
                    Typ = 1;
                var lst = objChallanDelverdDAL.SelectMatTransfNo(fromCityIdno, Typ, Convert.ToInt32(ddlDateRange.SelectedValue));
                objChallanDelverdDAL = null;
                ddlChallanDetl.DataSource = lst;
                ddlChallanDetl.DataTextField = "Chln_no";
                ddlChallanDetl.DataValueField = "chln_Idno";
                ddlChallanDetl.DataBind();
                ddlChallanDetl.Items.Insert(0, new ListItem("--Select Challan No.--", "0"));
        }

        private void ClearControls()
        {
            lnkBtnNew.Visible = false;
            txtLocGrAmnt.Text="";
            txtoutGrAmnt.Text = "";
            txtNetAmnt.Text = "";

            ddlToCity.SelectedIndex = 0;
            ddlChallanDetl.SelectedValue = "0";
            txtRcptNo.Text = string.Empty;
            hidMtrlRcptid.Value = hidMtrlTrnsfDt.Value = string.Empty;
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlToCity.Enabled = true;
            ddlChallanDetl.Enabled = true;
            lnkSubmit.Visible = true; ddlChallanDetl.Enabled = true; txtRcptNo.Visible = false; DivRcptNo.Visible = false; lblRcptDtNo.Text = "Receipt Date"; ddlToCity.Enabled = true;
            ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
            tblUserPref obj = objChallanDelverdDAL.selectUserPref();
            ddlToCity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
            
        }

        private void BindGrid()
        {
            if ((ddlChallanDetl.SelectedIndex > 0) && (txtDate.Text.Trim() != ""))
            {
                ChallanDelverdDAL obj = new ChallanDelverdDAL();
                int rec = 0;
                Int64 MtrlRcptIdno = (Convert.ToString(hidMtrlRcptid.Value) != "" ? Convert.ToInt64(hidMtrlRcptid.Value) : 0);
                rec = obj.IsExeistMtrlTransf(Convert.ToInt64(ddlChallanDetl.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue), MtrlRcptIdno);
                if (rec > 0)
                {
                    grdMain.Visible = false;
                    string msg = "Receipt of the transfer no. : " + ddlChallanDetl.SelectedItem.Text + " is already generated.";
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
                }
                else
                {
                    DateTime dMtrlDt = obj.MtrlTransfDate(Convert.ToInt64(ddlChallanDetl.SelectedValue));
                    hidMtrlTrnsfDt.Value = dMtrlDt.ToString("dd-MM-yyyy");
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidMtrlTrnsfDt.Value)) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text)))
                    {
                        grdMain.Visible = false;
                        grdMain.DataSource = null; grdMain.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('Date must be Greater than or equal to Transfer Date [" + dMtrlDt.Date.ToString("dd-MMM-yyyy") + "].')", true);
                    }
                    else
                    {
                        DataSet Ds = obj.SelectGridDetl(ApplicationFunction.ConnectionString(),"SelectTransfDetl", Convert.ToInt64(ddlChallanDetl.SelectedValue),Convert.ToInt32(ddlToCity.SelectedValue));
                        obj = null;
                        LocGrAmnt = 0; OutGrAmnt = 0;
                        if (Ds!=null && Ds.Tables[0].Rows.Count>0)
                        {
                            foreach (DataRow dr in Ds.Tables[0].Rows)
                            {
                                Int32 DelvPlcIdno = Convert.ToInt32(dr["DelvryPlce_Idno"]);
                              double dGrAmnt=Convert.ToDouble(dr["Amount"]);
                              if (Convert.ToInt32(ddlToCity.SelectedValue) == DelvPlcIdno)
                              {
                                  LocGrAmnt += dGrAmnt;
                              }
                              else
                              {
                                  OutGrAmnt += dGrAmnt;
                              }
                            }
                        }
                        txtLocGrAmnt.Text = LocGrAmnt.ToString("N2");
                        txtoutGrAmnt.Text = OutGrAmnt.ToString("N2");
                        txtNetAmnt.Text = Convert.ToDouble(LocGrAmnt+OutGrAmnt).ToString("N2");
                        ViewState["dt"] = (DataTable)Ds.Tables[0];
                        grdMain.DataSource = ViewState["dt"];
                        grdMain.DataBind();
                        grdMain.Visible = true;
                    }
                }
                obj = null;
            }
            else
            {
                grdMain.Visible = false;
                ddlChallanDetl.SelectedIndex=0;
                ddlChallanDetl.Focus();
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }

        private void Populate(Int64 MtrlRcptHeadId)
        {
            try
            {
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                tblChlnDelvHead objtblChlnDelvHead = objChallanDelverdDAL.SelectHeadInEdit(MtrlRcptHeadId);
                if (objtblChlnDelvHead != null)
                {
                    txtDate.Text = Convert.ToDateTime(objtblChlnDelvHead.ChlnDelv_Date).Date.ToString("dd-MM-yyyy");
                    ddlDateRange.SelectedValue = Convert.ToString(objtblChlnDelvHead.Year_Idno);
                    ddlToCity.SelectedValue = Convert.ToString(objtblChlnDelvHead.ToCity_Idno);
                    ddlToCity_SelectedIndexChanged(null, null);
                    txtRcptNo.Text = objtblChlnDelvHead.ChlnDelv_No.ToString();
                    ddlChallanDetl.SelectedValue = objtblChlnDelvHead.ChlnTransf_Idno.ToString();
                    hidMtrlRcptid.Value = objtblChlnDelvHead.ChlnDelvHead_Idno.ToString();
                    txtLocGrAmnt.Text = Convert.ToDouble(objtblChlnDelvHead.LocGR_Amnt).ToString("N2");
                    txtoutGrAmnt.Text = Convert.ToDouble(objtblChlnDelvHead.CrsngGR_Amnt).ToString("N2");
                    txtNetAmnt.Text = Convert.ToDouble(objtblChlnDelvHead.Net_Amnt).ToString("N2");
                    lblRcptDtNo.Text = "Receipt Date";
                }
                DataSet Ds = objChallanDelverdDAL.SelectGridDetlInEdit(ApplicationFunction.ConnectionString(), MtrlRcptHeadId);
                objChallanDelverdDAL = null;
                ViewState["dt"] = (DataTable)Ds.Tables[0];
                grdMain.DataSource = ViewState["dt"];
                grdMain.DataBind();
            }
            catch(Exception e) { }
            
        }
        private void SetDate()
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                }
            }
        }
       
        #endregion

        #region Control Events...
        protected void ddlChallanDetl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToCity.SelectedIndex > 0)
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }

        protected void imgBtnOK_Click(object sender, ImageClickEventArgs e)
        {
            BindGrid();
        }

        protected void ddlToCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.BindChlnNo();
            grdMain.DataSource = null;
            grdMain.DataBind();
            ddlChallanDetl.Focus();
        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                SetDate();
            }
        }

        protected void txtRecQty_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtRecAmt = (TextBox)row.FindControl("txtRecAmt");
            TextBox txtRecQty = (TextBox)row.FindControl("txtRecQty");
            Label lblrateType = (Label)row.FindControl("lblrateType");
            Label lblQty = (Label)row.FindControl("lblQty");
            Label ItemRate = (Label)row.FindControl("lblItemRate");
            Label lblTotRcptWT = (Label)grdMain.FooterRow.FindControl("lblTotRcptWT");
            Label lblTotRcptQty = (Label)grdMain.FooterRow.FindControl("lblTotRcptQty");
            Label lblRecAmt = (Label)grdMain.FooterRow.FindControl("lblRecAmt");
            if (lblrateType.Text == "Rate")
            {
                if (Convert.ToDouble(lblQty.Text) <= 0)
                {
                    ShowMessageErr("Qty cannot less than 1!");
                    return;
                }
                else if (Convert.ToDouble(lblQty.Text) < (Convert.ToDouble(txtRecQty.Text)))
                {
                    ShowMessageErr("Qty cannot more than Gr Qty!");
                    return;
                }
                else
                {
                    txtRecAmt.Text=Convert.ToString((Convert.ToDouble(txtRecQty.Text))*Convert.ToDouble(ItemRate.Text));
                    calculateTotal();
                    lblTotRcptQty.Text = RecFooQty.ToString("N2");
                    lblTotRcptWT.Text = RecFooWt.ToString("N2");
                    lblRecAmt.Text = RecFooAmt.ToString("N2");
                }   
            }
        }
        protected void txtRecWT_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtRecAmt = (TextBox)row.FindControl("txtRecAmt");
            TextBox txtRecWT = (TextBox)row.FindControl("txtRecWT");
            Label lblrateType = (Label)row.FindControl("lblrateType");
            Label lblWeight = (Label)row.FindControl("lblWeight");
            Label ItemRate = (Label)row.FindControl("lblItemRate");
            Label lblTotRcptWT = (Label)grdMain.FooterRow.FindControl("lblTotRcptWT");
            Label lblTotRcptQty = (Label)grdMain.FooterRow.FindControl("lblTotRcptQty");
            Label lblRecAmt = (Label)grdMain.FooterRow.FindControl("lblRecAmt");
            if (lblrateType.Text == "Weight")
            {
                if (Convert.ToDouble(lblWeight.Text) <= 0)
                {
                    ShowMessageErr("Weight cannot less than 1!");
                    return;
                }
                else if (Convert.ToDouble(lblWeight.Text) < (Convert.ToDouble(txtRecWT.Text)))
                {
                    ShowMessageErr("Weight cannot more than Gr Weight!");
                    return;
                }
                else
                {
                    txtRecAmt.Text = Convert.ToString((Convert.ToDouble(txtRecWT.Text)) * Convert.ToDouble(ItemRate.Text));
                    calculateTotal();
                    lblTotRcptQty.Text = RecFooQty.ToString("N2");
                    lblTotRcptWT.Text = RecFooWt.ToString("N2");
                    lblRecAmt.Text = RecFooAmt.ToString("N2");
                }
            }
        }
        #endregion

       
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        protected void lnkBtnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidMtrlRcptid.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                lnkSubmit.Visible = false; ddlChallanDetl.Enabled = false; txtRcptNo.Visible = true; DivRcptNo.Visible = true;

                this.Populate(Convert.ToInt32(hidMtrlRcptid.Value));
                this.Title = "Edit Delivery Challan";
            }
        }

        private void calculateTotal()
        {
            Double LocalGrAmt = 0.00, Outside = 0.00; Double RecAmt = 0.00,FooQty=0.00,FooAmt=0.00,FooWt=0.00; Int64 DeliveryId = 0;
                Int64 ToCityId = Convert.ToInt64(ddlToCity.SelectedValue);
            if (ToCityId > 0)
            {
                for (int j=0;j<grdMain.Rows.Count;j++)
                {
                    for (int i = 0; i < grdMain.Columns.Count; i++)
                    {
                        string lblDelivery = ((Label)grdMain.Rows[j].Cells[i].FindControl("lblDelivery")).Text;
                        DeliveryId = Convert.ToInt64(lblDelivery);
                        RecAmt = Convert.ToDouble(((TextBox)grdMain.Rows[j].Cells[i].FindControl("txtRecAmt")).Text);
                        FooAmt = Convert.ToDouble(((TextBox)grdMain.Rows[j].Cells[i].FindControl("txtRecAmt")).Text);
                        FooQty = Convert.ToDouble(((TextBox)grdMain.Rows[j].Cells[i].FindControl("txtRecQty")).Text);
                        FooWt = Convert.ToDouble(((TextBox)grdMain.Rows[j].Cells[i].FindControl("txtRecWT")).Text);
                    }
                    if (DeliveryId != ToCityId)
                        Outside = Outside + RecAmt;
                    else
                        LocalGrAmt = LocalGrAmt + RecAmt;
                    RecFooQty = RecFooQty + FooQty;
                    RecFooAmt = RecFooAmt + FooAmt;
                    RecFooWt = RecFooWt + FooWt;
                }
                txtLocGrAmnt.Text = Convert.ToDouble(LocalGrAmt).ToString("N2");
                txtoutGrAmnt.Text = Convert.ToDouble(Outside).ToString("N2"); 
                txtNetAmnt.Text = Convert.ToDouble(LocalGrAmt + Outside).ToString("N2"); 
            }
        }
    }
}
