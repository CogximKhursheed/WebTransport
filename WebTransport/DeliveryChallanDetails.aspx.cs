using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
namespace WebTransport
{
    public partial class DeliveryChallanDetails : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); //string con = "";
        double dblNetAmnt = 0; Int32 iscmbtype = 0; DataSet DsUserPref; string sqlSTR = ""; double dTotQty = 0; double dTotWeight = 0; double dGrAmnt = 0;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0, dNetcommsn = 0;
        private int intFormId = 38;
        #endregion

        #region Page Enents...
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
                this.Bind();
                this.BindCity();
                this.BindDropdown();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindFromCity();
                }
                else
                {
                    this.BindFromCity(Convert.ToInt64(Session["UserIdno"]));
                    ddlDelvryPlace.SelectedValue = Convert.ToString(base.UserFromCity);
                }

                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);



                DeliveryChallanDetailsDAL objDeliveryChallanDetailsDAL = new DeliveryChallanDetailsDAL();
                tblUserPref obj = objDeliveryChallanDetailsDAL.selectUserPref();
                this.ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue));
                if (Request.QueryString["dc"] != null)
                {
                    Populate(Convert.ToInt64(Request.QueryString["dc"]));
                    hidid.Value = Convert.ToString(Request.QueryString["dc"]);
                    lnkbtnNew.Visible = true;
                    lnkbtnPrint.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                    lnkbtnPrint.Visible = false;
                }

                txtOwnrNme.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtchallanNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                txtkatt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                ddldateRange.Focus();

            }
        }
        #endregion

        #region Button Evnets...

        protected void lnkbtnokClick_OnClick(object sender, EventArgs e)
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
                            HiddenField hidGrIdno = (HiddenField)grdGrdetals.Rows[count].FindControl("hidGrIdno");
                            strchkDetlValue = strchkDetlValue + hidGrIdno.Value + ",";
                            Icount++;
                        }
                    }
                    if (Icount > 1)
                    {
                        ////ShowMessage("Please check only one Gr.");
                        //lblmsg.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);
                        //// ShowDiv("ShowClient('dvGrdetails')");
                        //return;
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Please select at least one Gr.";
                    }
                    if (strchkDetlValue != "")
                    {
                        strchkDetlValue = strchkDetlValue.Substring(0, strchkDetlValue.Length - 1);
                    }
                    if (strchkDetlValue == "")
                    {
                        lblmsg.Text = "Please select atleast one Gr.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                        //ShowMessage("Please check atleast one Gr.");
                        // ShowDiv("ShowClient('dvGrdetails')");
                        return;
                    }
                    else
                    {
                        lblmsg.Visible = false;
                        DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
                        string strSbillNo = String.Empty;
                        DataTable dtRcptDetl = new DataTable();
                        dtRcptDetl = obj.SelectDELVGrDetails(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToString(strchkDetlValue));
                        ViewState["dt"] = dtRcptDetl;

                        grdMain.DataSource = null;
                        //foreach (GridViewRow row in grdMain.Rows)
                        //{
                        //    ImageButton imgbtndelete = (ImageButton)row.FindControl("imgbtndelete");
                        //    imgbtndelete.Enabled = false;
                        //}
                        grdMain.DataSource = dtRcptDetl;
                        grdMain.DataBind();
                        ddlDelvryPlace.SelectedValue = ddldelvplace.SelectedValue;
                    }
                }
                else
                {
                    ShowMessageErr("Gr Details not found.");
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
                }
                ddlTruckNo.Focus();
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowClient('dvGrdetails')", true);
            //ddldelvplace.SelectedIndex = 0;
            //grdGrdetals.DataSource = null;
            //grdGrdetals.DataBind(); btnSubmit.Visible = false; BtnClerForPurOdr.Visible = false;
        }
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
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
            Int64 RateIdno = 0; bool isinsert = false;
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            tblDelvChlnHead objChlnDelvHead = new tblDelvChlnHead();
            objChlnDelvHead.DelvChln_No = Convert.ToInt64(txtchallanNo.Text);
            objChlnDelvHead.DelvChln_Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            objChlnDelvHead.DelvryPlc_Idno = Convert.ToInt32((ddlDelvryPlace.SelectedIndex <= 0) ? "0" : ddlDelvryPlace.SelectedValue);
            objChlnDelvHead.Truck_Idno = Convert.ToInt32((ddlTruckNo.SelectedIndex <= 0) ? "0" : ddlTruckNo.SelectedValue);
            objChlnDelvHead.Year_Idno = Convert.ToInt32((ddldateRange.SelectedIndex < 0) ? "0" : ddldateRange.SelectedValue);
            objChlnDelvHead.Driver_Idno = Convert.ToInt32((ddldriverName.SelectedIndex <= 0) ? "0" : ddldriverName.SelectedValue);
            objChlnDelvHead.Delvry_Instrc = txtDelvInstruction.Text.Trim().Replace("'", "");
            objChlnDelvHead.Transprtr_Idno = Convert.ToInt32((ddlTransportName.SelectedIndex <= 0) ? "0" : ddlTransportName.SelectedValue);
            objChlnDelvHead.Inv_Idno = 0;
            objChlnDelvHead.Gross_Amnt = Convert.ToDouble(txtGrosstotal.Text);
            objChlnDelvHead.Katt_Amnt = Convert.ToDouble(txtkatt.Text);
            objChlnDelvHead.DelvChln_type = 1;
            objChlnDelvHead.Other_Amnt = Convert.ToDouble(txtOtherAmnt.Text);
            objChlnDelvHead.Net_Amnt = Convert.ToDouble(txtNetAmnt.Text);
            objChlnDelvHead.Date_Added = Convert.ToDateTime(DateTime.Now);
            Int64 value = 0;

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (string.IsNullOrEmpty(hidid.Value) == true)
                {

                    value = obj.Insert(objChlnDelvHead, DtTemp, Convert.ToInt32(ddlDelvryPlace.SelectedValue), ApplicationFunction.ConnectionString());

                    tScope.Complete();

                    obj = null;
                }
                else
                {
                    value = obj.Update(objChlnDelvHead, Convert.ToInt32(hidid.Value), DtTemp, Convert.ToInt32(txtchallanNo.Text), ApplicationFunction.ConnectionString());
                    tScope.Complete();
                }
                if ((string.IsNullOrEmpty(hidid.Value) == false))
                {
                    if (value > 0)
                    {
                        ShowMessage("Record Update successfully");
                        Clear();
                    }
                    else if (value == -1)
                    {
                        ShowMessageErr("Challan No Already Exist");
                    }
                    else
                    {
                        ShowMessageErr("Record  Not Update");
                    }
                }
                else if (string.IsNullOrEmpty(hidid.Value) == true)
                {
                    if (value > 0)
                    {
                        ShowMessage("Record  saved Successfully ");
                        Clear();
                    }
                    else if (value == -1)
                    {
                        ShowMessageErr("Challan No Already Exist");
                    }
                    else
                    {
                        ShowMessageErr("Record Not  saved Successfully ");
                    }
                }
            }
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["dc"] != null)
            {
                Populate(Convert.ToInt64(Request.QueryString["dc"]));
            }
            else
            {
                Clear();
               
            }

        }
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryChallanDetails.aspx");
        }
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdGrdetals.DataSource = null;
            grdGrdetals.DataBind();

            // txtTdsAmnt.Text = "0.00";
            ddlTruckNo.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            lnkbtnokClick.Visible = false;
            //if (Convert.ToInt32(hidWorkType.Value) > 1)
            //{
            //    lblDelvSerch.Text = "Truck No.";
            //    DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            //    var lst = obj.selectTruckNo();
            //    obj = null;
            //    if (lst.Count > 0)
            //    {
            //        ddldelvplace.DataSource = lst;
            //        ddldelvplace.DataTextField = "Lorry_No";
            //        ddldelvplace.DataValueField = "Lorry_Idno";
            //        ddldelvplace.DataBind();
            //        ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));
            //    }
            //}
            //else
            //{

            //lblDelvSerch.Text = "Delv. Place";
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                ddldelvplace.DataSource = lst;
                ddldelvplace.DataTextField = "City_Name";
                ddldelvplace.DataValueField = "City_Idno";
                ddldelvplace.DataBind();
                ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            //}
            txtDateFrom.Focus();

        }

        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                grdGrdetals.DataSource = null;
                grdGrdetals.DataBind();

                Int32 YearIdno = Convert.ToInt32(ddldateRange.SelectedValue);
                DateTime DtFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
                DateTime DtTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
                Int64 DelvPlace = Convert.ToInt64(ddldelvplace.SelectedValue);

                DeliveryChallanDetailsDAL Obj = new DeliveryChallanDetailsDAL();
                var Lst = Obj.SearchGR(YearIdno, DtFrom, DtTo, DelvPlace);
                if (Lst != null && Lst.Count > 0)
                {
                    grdGrdetals.DataSource = Lst;
                    grdGrdetals.DataBind();
                    lnkbtnokClick.Visible = true;
                }
                else
                {
                    lnkbtnokClick.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal();", true);
            }
            catch (Exception Ex)
            {
                ApplicationFunction.ErrorLog(Ex.Message);
            }
        }
        #endregion

        #region Functions...
        private void BindDropdown()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var Transpoter = obj.BindTranspoter();
            ddlTransportName.DataSource = Transpoter;
            ddlTransportName.DataTextField = "Acnt_Name";
            ddlTransportName.DataValueField = "Acnt_Idno";
            ddlTransportName.DataBind();
            ddlTransportName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void ChallanNo(Int32 YearIdno)
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            txtchallanNo.Text = Convert.ToString(obj.GetChallanNo(YearIdno, ApplicationFunction.ConnectionString()));
        }

        private void netamntcal()
        {
            try
            {
                //txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text)) - (Convert.ToDouble(txtkatt.Text) + Convert.ToDouble(txtAdvAmnt.Text) + Convert.ToDouble(txtTdsAmnt.Text))).ToString("N2");
                txtNetAmnt.Text = Convert.ToDouble((Convert.ToDouble(txtGrosstotal.Text)) - (Convert.ToDouble(txtkatt.Text)) + (Convert.ToDouble(txtOtherAmnt.Text))).ToString("N2");
                if (Convert.ToDouble(txtNetAmnt.Text) < 0)
                {
                    txtNetAmnt.Text = "0.00";
                }
            }
            catch (Exception Ex)
            { }
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

        private void BindCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                ddldelvplace.DataSource = lst;
                ddldelvplace.DataTextField = "City_Name";
                ddldelvplace.DataValueField = "City_Idno";
                ddldelvplace.DataBind();


                //ddlFromCity.DataSource = lst;
                //ddlFromCity.DataTextField = "City_Name";
                //ddlFromCity.DataValueField = "City_Idno";
                //ddlFromCity.DataBind();


                ddlDelvryPlace.DataSource = lst;
                ddlDelvryPlace.DataTextField = "City_Name";
                ddlDelvryPlace.DataValueField = "City_Idno";
                ddlDelvryPlace.DataBind();

            }
            ddldelvplace.Items.Insert(0, new ListItem("--Select--", "0"));
            //  ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDelvryPlace.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindFromCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindAllToCity();
            obj = null;

            if (lst.Count > 0)
            {
                ddlDelvryPlace.DataSource = lst;
                ddlDelvryPlace.DataTextField = "City_Name";
                ddlDelvryPlace.DataValueField = "City_Idno";
                ddlDelvryPlace.DataBind();
                ddlDelvryPlace.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        private void BindFromCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlDelvryPlace.DataSource = FrmCity;
            ddlDelvryPlace.DataTextField = "CityName";
            ddlDelvryPlace.DataValueField = "CityIdno";
            ddlDelvryPlace.DataBind();
            obj = null;
            ddlDelvryPlace.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDriver(Int32 var)
        {
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            if (var == 0)
            {
                var lst = obj.selectOwnerDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Acnt_Name";
                    ddldriverName.DataValueField = "Acnt_Idno";
                    ddldriverName.DataBind();

                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                var lst = obj.selectHireDriverName();
                obj = null;
                if (lst != null && lst.Count > 0)
                {
                    ddldriverName.DataSource = lst;
                    ddldriverName.DataTextField = "Driver_name";
                    ddldriverName.DataValueField = "Driver_Idno";
                    ddldriverName.DataBind();

                }
                ddldriverName.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        private void Bind()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddlTruckNo.DataSource = lst;
                ddlTruckNo.DataTextField = "Lorry_No";
                ddlTruckNo.DataValueField = "Lorry_Idno";
                ddlTruckNo.DataBind();

            }
            ddlTruckNo.Items.Insert(0, new ListItem("--Select--", "0"));
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

                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                }
                else
                {
                    txtDate.Text = hidmindate.Value;
                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                    txtDateTo.Text = hidmindate.Value;
                }
            }
        }

        private void Populate(Int64 HeadId)
        {
            imgSearch.Enabled = false;
            ddlDelvryPlace.Enabled = false;
            DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
            tblDelvChlnHead chlnhead = obj.selectHead(HeadId);
            ddldateRange.SelectedValue = Convert.ToString(chlnhead.Year_Idno);
            ddldateRange_SelectedIndexChanged(null, null);
            ddldateRange.Enabled = false;
            txtchallanNo.Text = Convert.ToString(chlnhead.DelvChln_No);
            txtDate.Text = Convert.ToDateTime(chlnhead.DelvChln_Date).ToString("dd-MM-yyyy");
            ddlDelvryPlace.SelectedValue = Convert.ToString(chlnhead.DelvryPlc_Idno);
            ddlTransportName.SelectedValue = Convert.ToString(chlnhead.Transprtr_Idno);
            ddlTruckNo.SelectedValue = Convert.ToString(chlnhead.Truck_Idno);
            ddlTruckNo_SelectedIndexChanged(null, null);
            ddldriverName.SelectedValue = Convert.ToString(chlnhead.Driver_Idno);
            txtDelvInstruction.Text = Convert.ToString(chlnhead.Delvry_Instrc);
            txtkatt.Text = Convert.ToDouble(chlnhead.Katt_Amnt).ToString("N2");
            txtNetAmnt.Text = Convert.ToDouble(chlnhead.Net_Amnt).ToString("N2");
            txtOtherAmnt.Text = Convert.ToDouble(chlnhead.Other_Amnt).ToString("N2");
            txtGrosstotal.Text = Convert.ToDouble(chlnhead.Gross_Amnt).ToString("N2");


            //if (Convert.ToInt32(hidWorkType.Value) > 1)
            //{  
            //    ddlDelvryPlace.Visible = false;
            //    ddlTruckNo.Enabled = false;
            //}
            //else
            //{

            //    ddlDelvryPlace.Visible = true;
            //    ddlTruckNo.Enabled = true;
            //    ddlDelvryPlace.Enabled = false;
            //}
            DtTemp = obj.selectDetl(ApplicationFunction.ConnectionString(), Convert.ToInt32(ddldateRange.SelectedValue), HeadId);
            ViewState["dt"] = DtTemp;
            this.BindGrid();
            imgSearch.Enabled = false;
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
            PrintChallan(HeadId);
            obj = null;
        }

        private void Clear()
        {
            //   ddlFromCity.Enabled = true;
            ddlDelvryPlace.SelectedValue = "0";
            ViewState["dt"] = null;
            DtTemp = null;
            hidid.Value = string.Empty; hidOwnerId.Value = string.Empty;
            ddldriverName.SelectedValue = "0";
            ddlTruckNo.SelectedValue = "0";
            //  ddlFromCity.SelectedValue = "0";
            ddldelvplace.SelectedValue = "0"; txtOwnrNme.Text = "";
            txtDate.Text = "";
            txtchallanNo.Text = "";
            ddldateRange.SelectedIndex = 0; ;
            //  ddldateRange_SelectedIndexChanged(null, null);
            BindGrid();
            ddlDelvryPlace.Enabled = true;
            txtDelvInstruction.Text = "";
            txtGrosstotal.Text = "0.00";

            txtNetAmnt.Text = "0.00";
            txtkatt.Text = "0.00";
            ddldateRange.Enabled = true;
            ddldateRange.SelectedIndex = 0;
            imgSearch.Enabled = true;
            //DeliveryChallanDetailsDAL objDeliveryChallanDetailsDAL = new DeliveryChallanDetailsDAL(); 
            //tblUserPref obj = objDeliveryChallanDetailsDAL.selectUserPref();
            //if (obj != null)
            //{
            //    hidWorkType.Value = Convert.ToString(obj.Work_Type);
            //}
            lnkbtnSave.Enabled = true;
            // Response.Redirect("DeliveryChallanDetails.aspx");
            Label lblEmptyMessage = grdMain.Controls[0].Controls[0].FindControl("lblnorecord") as Label;
            lblEmptyMessage.Visible = false;
            ddlDelvryPlace.Enabled = false;
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

        private void ShowDiv(string FunNm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", FunNm, true);
        }

        private void BindNullGird()
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            //ViewState["dt"] = null;
            //DtTemp = null;
        }



        private void PrintChallan(Int64 ChlnHeadIdno)
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

            lblCompanyname.Text = CompName;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spDeliveryChallan] @ACTION='SelectPrint',@Id='" + ChlnHeadIdno + "'");
            dsReport.Tables[0].TableName = "Printhead";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblChlnno.Text = Convert.ToString(dsReport.Tables["Printhead"].Rows[0]["DelvChln_No"]);
                lblchlnDate.Text = Convert.ToDateTime(dsReport.Tables["Printhead"].Rows[0]["DelvChln_Date"]).ToString("dd-MM-yyyy");
                lblOwnr.Text = Convert.ToString(dsReport.Tables["Printhead"].Rows[0]["Lorry_Owner"]);
                lblTrckNo.Text = Convert.ToString(dsReport.Tables["Printhead"].Rows[0]["Lorry_No"]);
                lblDrvrName.Text = Convert.ToString(dsReport.Tables["Printhead"].Rows[0]["Driver_Name"]);
                valuelblgrossAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["Printhead"].Rows[0]["Gross_Amnt"]));
                valuelblKatt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["Printhead"].Rows[0]["katt_Amnt"]));
                valueOtherAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["Printhead"].Rows[0]["Other_Amnt"]));
                valuelblnetTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["Printhead"].Rows[0]["Net_Amnt"]));
            }
            //  DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintDetl',@Id='" + ChlnHeadIdno + "'");
            dsReport.Tables[1].TableName = "Printdetl";
            if (dsReport.Tables[1] != null && dsReport.Tables[1].Rows.Count > 0)
            {
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                lblPrintHeadng.Text = "Delivery Challan";
            }

        }
        #endregion

        #region Control Events...

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex >= 0)
            {
                SetDate();
            }
        }
        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTruckNo.SelectedIndex != -1)
            //{
            //  
            //}\

            try
            {
                if ((ddlTruckNo.SelectedIndex > 0))
                {
                    DeliveryChallanDetailsDAL obj = new DeliveryChallanDetailsDAL();
                    obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    var lst = obj.selectOwnerName(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    if (lst != null)
                    {
                        txtOwnrNme.Text = Convert.ToString(lst.Owner_Name + '-' + ((lst.Pan_No == null) ? "" : lst.Pan_No) + "-" + ((lst.Lorry_Type == 0) ? "O" : "H"));
                        hidOwnerId.Value = Convert.ToString(lst.Prty_Idno);
                    }
                    Int32 Typ = 0;
                    Typ = obj.selectTruckType(Convert.ToInt32(ddlTruckNo.SelectedValue));
                    BindDriver(Typ);
                    if ((Convert.ToInt32(lst.Lorry_Type) == 1) && Convert.ToString(lst.Pan_No) == "")
                    {
                        dGrAmnt = 0;
                        if (grdMain.Rows.Count > 0)
                        {
                            foreach (GridViewRow Dr in grdMain.Rows)
                            {
                                Label lblSubTotAmnt = (Label)Dr.FindControl("lblSubTotAmnt");
                                dGrAmnt += Convert.ToDouble(lblSubTotAmnt.Text);
                            }
                            //  txtTdsAmnt.Text = Convert.ToDouble(((dGrAmnt * Convert.ToDouble(hidTdsTaxPer.Value)) / 100)).ToString("N2");

                        }
                        else
                        {
                            // txtTdsAmnt.Text = "0.00";
                        }
                    }
                    else
                    {
                        //txtTdsAmnt.Text = "0.00";
                    }
                    netamntcal();
                }
                else
                {
                    txtOwnrNme.Text = "";
                }
              
            }

            catch (Exception Ex)
            {

            }
            ddlTruckNo.Focus();
        }
        protected void txtkatt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtkatt.Text.Trim() == "")
                {
                    txtkatt.Text = "0.00";
                }
                else
                {
                    txtkatt.Text = Convert.ToDouble(txtkatt.Text).ToString("N2");
                }
                netamntcal();
            }
            catch (Exception Ex)
            {

            }
        }
        protected void txtOtherAmnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtOtherAmnt.Text.Trim() == "")
                {
                    txtOtherAmnt.Text = "0.00";
                }
                else
                {
                    txtOtherAmnt.Text = Convert.ToDouble(txtOtherAmnt.Text).ToString("N2");
                }
                netamntcal();
            }
            catch (Exception Ex)
            {

            }
        }
        //protected void ddlFromCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlFromCity.SelectedIndex > 0)
        //        {

        //            ChallanNo(Convert.ToInt32(ddldateRange.SelectedValue), Convert.ToInt32(ddlFromCity.SelectedValue));
        //            grdMain.DataSource = null;
        //            grdMain.DataBind();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //    }
        //}

        #endregion

        #region Grid Events....
        protected void grdMain_DataBound(object sender, EventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double dblChallanAmnt = 0; double dQtty = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dblNetAmnt = dblChallanAmnt + dblNetAmnt;
                dQtty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotQty = dQtty + dTotQty;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                lblTChallanAmnt.Text = dblNetAmnt.ToString("N2");
                txtGrosstotal.Text = dblNetAmnt.ToString("N2");

                Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
                lblTotQty.Text = dTotQty.ToString("N2");

                txtkatt.Enabled = true;
                if (Convert.ToDouble(txtGrosstotal.Text) <= 0)
                {
                    txtkatt.Enabled = false;

                }
                else
                {
                    txtkatt.Enabled = true;
                    netamntcal();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                //   dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                //    lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();

            }
        }
        #endregion
    }
}

