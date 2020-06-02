using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Drawing;

namespace WebTransport
{
    public partial class PartyDetlRep : Pagebase
    {
        #region Variable ...
        string conString = ""; DataTable list, DtVchr,PrevAmntList;
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 44;
        double dTotDebit = 0; double dTotCredit = 0, totshrtg = 0;
        DataTable CSVTable = new DataTable();
        double NetMinShrt = 0; object SumObj;
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = true;
                }

                this.BindSenderName();
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            drpBaseCity.SelectedIndex = 0;
            grdMain.DataSource = null;
            grdMain.DataBind();
            drpBaseCity.Focus();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();

            // For printing
            PrintGRPrep();
        }
        protected void imgCSV_Click(object sender, ImageClickEventArgs e)
        {

            CSVTable = (DataTable)ViewState["CSVdt"];
           
            if (list != null && list.Rows.Count > 0)
            {
                CSVTable.Columns.Remove("Acnt_Name");
                CSVTable.Columns.Remove("Address");
                CSVTable.Columns.Remove("city_Name1");
                CSVTable.Columns.Remove("State_Name");
                CSVTable.Columns.Remove("Chln_Idno");
                CSVTable.Columns.Remove("Gr_Idno");

                CSVTable.Columns["Chln_Date"].ColumnName = "Challan Date";
                CSVTable.Columns["Chln_No"].ColumnName = "Challan No";
                CSVTable.Columns["Gr_No"].ColumnName = "GR.No";
                CSVTable.Columns["Item_Name"].ColumnName = "Item Name";
                CSVTable.Columns["Lorry_No"].ColumnName = "Lorry No";
                CSVTable.Columns["City_Name"].ColumnName = "City Name";
                CSVTable.Columns["Tot_Weght"].ColumnName = "Total Weight";
                CSVTable.Columns["Item_Rate"].ColumnName = "Item Rate";
                CSVTable.Columns["Adv_Amnt"].ColumnName = "Advance Amount";
                CSVTable.Columns["Shortage_Qty"].ColumnName = "Shortage Qty";
                CSVTable.Columns["shortage_Amount"].ColumnName = "Shortage Amount";
            }
            else if (DtVchr != null && DtVchr.Rows.Count > 1)
            {
                CSVTable.Columns["Vchr_Date"].ColumnName = "Vchr Date";
                CSVTable.Columns["Vchr_Type"].ColumnName = "Vchr Type";
                CSVTable.Columns["Debit"].ColumnName = "Debit";
                CSVTable.Columns["Credit"].ColumnName = "Credit";
            }


            ExportDataTableToCSV(CSVTable, "PartyDetail-" + ddlParty.SelectedItem.Text);
            Response.Redirect("PartyDetlRep.aspx");
            //}
        }
        #endregion

        private void PrintGRPrep()
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
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
            if (PanNo == "")
            {
                lblPanNo.Visible = false; lbltxtPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblPanNo.Visible = true; lbltxtPanNo.Visible = true;
            }
            #endregion

            #region CodeForPrint Details
            //DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            //dsReport.Tables[0].TableName = "GRPrint";
            if (list != null && list.Rows.Count > 0)
            {
                if (drpBaseCity.SelectedIndex > 0)
                {
                    divLocation.Visible = true;
                    lblLocation.Text = (drpBaseCity.SelectedIndex > 0) ? drpBaseCity.SelectedValue : "";
                }
                else { divLocation.Visible = false; }

                lblAddresss.Text = list.Rows[0]["Address"].ToString();
                lblStateName.Text = list.Rows[0]["State_Name"].ToString();
                lblPartyName.Text = list.Rows[0]["Acnt_Name"].ToString();
                lblCityname.Text = list.Rows[0]["city_Name1"].ToString();
                lblDateFrom.Text = txtDateFrom.Text.Trim();
                lblDateto.Text = txtDateTo.Text.Trim();
                //    valuelblnetAmnt.Text = string.Format("{0:0,0.00}", (dcmsn + dblty + dcrtge + dPF + dsuchge + dtax + dwges + dtoll + dtotlAmnt));
            }
            #endregion


        }

        #region Bind Event...
        private void BindSenderName()
        {
            PrtyDetlDAL obj = new PrtyDetlDAL();
            DataTable SenderName = obj.BindPartynew("BindPartynew",ApplicationFunction.ConnectionString());
            ddlParty.DataSource = SenderName;
            ddlParty.DataTextField = "Acnt_Name";
            ddlParty.DataValueField = "Acnt_Idno";
            ddlParty.DataBind();
            objGRDAL = null;
            ddlParty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "CityIdno";
            drpBaseCity.DataBind();
            objGRDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            PrtyDetlDAL obj = new PrtyDetlDAL();
            var FrmCity = obj.BindFromCity();
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "City_Name";
            drpBaseCity.DataValueField = "City_Idno";
            drpBaseCity.DataBind();
            objGRDAL = null;
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindGrid()
        {

            //try
            //{
            PrtyDetlDAL obj = new PrtyDetlDAL();

            Int64 iSenderIDNO = (ddlParty.SelectedIndex <= 0 ? 0 : Convert.ToInt64(ddlParty.SelectedValue));
            Int64 iFromCityIDNO = (drpBaseCity.SelectedIndex <= 0 ? 0 : Convert.ToInt64(drpBaseCity.SelectedValue));
            string UserClass = Convert.ToString(Session["Userclass"]);
            Int64 UserIdno = 0;
            if (UserClass != "Admin")
            {
                UserIdno = Convert.ToInt64(Session["UserIdno"]);
            }

            if (ddlGrAccordng.SelectedValue == "2")
            {
                list = obj.SelectRep("SelectRep", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                         Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)),
                         iSenderIDNO, iFromCityIDNO, UserIdno, ApplicationFunction.ConnectionString());
                //DateTime date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
                //date = date.Subtract(TimeSpan.FromDays(1));

                if (Convert.ToString(Session["DBName"]) == "ACMPLLive")
                    totshrtg = obj.SelectShrtgAmnt("SelectShortageAravali", iSenderIDNO, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)).AddDays(-1), ApplicationFunction.ConnectionString());
                else
                    totshrtg = obj.SelectShrtgAmnt("SelectShortage", iSenderIDNO, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), ApplicationFunction.ConnectionString());
            }
            else
            {
                list = obj.SelectRep("SelectRepAll", Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                         Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)),
                         iSenderIDNO, iFromCityIDNO, UserIdno, ApplicationFunction.ConnectionString());
            }



            DtVchr = obj.SelectVchrReport("SelectVcrDetl", iSenderIDNO, ApplicationFunction.ConnectionString(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)));
            string[] arr = new string[] { }; double[] arrTotal = new double[] { }; double[] arrGrpTotal = new double[] { };
            if ((list != null) && (list.Rows.Count > 0))
            {
                grdMain.Width = new Unit("1480px");
                IList<tblCityMaster> lst = obj.selectLocation(Convert.ToInt32(ddlParty.SelectedValue));
                if (lst != null)
                {
                    arr = new string[Convert.ToInt32(lst.Count)]; int ar = 0;
                    arrTotal = new double[Convert.ToInt32(lst.Count)];
                    arrGrpTotal = new double[Convert.ToInt32(lst.Count)];
                    foreach (var l in lst)
                    {
                        string ColName = "";
                        ColName = Convert.ToString(l.City_Name); ;
                        DataColumn newCol = new DataColumn(Convert.ToString(l.City_Name), typeof(string));
                        newCol.AllowDBNull = true;
                        list.Columns.Add(newCol);
                        arr[ar] = Convert.ToString(newCol);

                        ar++;
                        for (int i = 0; i < list.Rows.Count; i++)
                        {
                            Int32 p = 0; Int32 LChlnIdno = 0;
                            LChlnIdno = Convert.ToInt32(list.Rows[i]["Chln_Idno"]);
                            if (i == 0)
                            {
                                double Amnt = obj.SelectLocationAmnt("SelectAmnt", Convert.ToInt32(l.City_Idno), LChlnIdno, ApplicationFunction.ConnectionString(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)));
                                list.Rows[i][ColName] = Amnt.ToString("N2").Replace(",", "");
                                p++;
                            }
                            else
                            {
                                if (LChlnIdno == Convert.ToInt32(list.Rows[i - 1]["Chln_Idno"]))
                                {
                                    list.Rows[i][ColName] = "0.00";
                                }
                                else
                                {
                                    p = 0;
                                    double Amnt = obj.SelectLocationAmnt("SelectAmnt", Convert.ToInt32(l.City_Idno), LChlnIdno, ApplicationFunction.ConnectionString(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)));
                                    list.Rows[i][ColName] = Amnt.ToString("N2").Replace(",", "");

                                }
                            }
                        }
                    }
                }
                Int32 j = 0; Int32 GrIdno = 0; Int32 ChlnIdno = 0;
                for (int k = 0; k < list.Rows.Count; k++)
                {
                    GrIdno = Convert.ToInt32(list.Rows[k]["Gr_Idno"]);
                    ChlnIdno = Convert.ToInt32(list.Rows[k]["Chln_Idno"]);
                    if (k == 0)
                    {
                        list.Rows[k]["Loading"] = Convert.ToString(list.Rows[k]["Loading"]);
                        list.Rows[k]["BC"] = Convert.ToString(list.Rows[k]["BC"]);
                        list.Rows[k]["Adv_Amnt"] = Convert.ToString(list.Rows[k]["Adv_Amnt"]);
                        list.Rows[k]["TDS_Amnt"] = Convert.ToString(list.Rows[k]["TDS_Amnt"]);
                        list.Rows[k]["Diesel_Amnt"] = Convert.ToString(list.Rows[k]["Diesel_Amnt"]);
                        j++;
                    }
                    else
                    {

                        if (GrIdno == Convert.ToInt32(list.Rows[k - 1]["Gr_Idno"]))
                        {

                            list.Rows[k]["Loading"] = "0.00";// Convert.ToString(Dr[10]);
                        }
                        else
                        {
                            j = 0;
                            list.Rows[k]["Loading"] = Convert.ToString(list.Rows[k]["Loading"]);

                        }
                        if (ChlnIdno == Convert.ToInt32(list.Rows[k - 1]["Chln_Idno"]))
                        {

                            list.Rows[k]["BC"] = "0.00";// Convert.ToString(Dr[10]);
                            list.Rows[k]["Adv_Amnt"] = "0.00";
                            list.Rows[k]["TDS_Amnt"] = "0.00";
                            list.Rows[k]["Diesel_Amnt"] = "0.00";
                        }
                        else
                        {
                            j = 0;
                            list.Rows[k]["BC"] = Convert.ToString(list.Rows[k]["BC"]);
                            list.Rows[k]["Adv_Amnt"] = Convert.ToString(list.Rows[k]["Adv_Amnt"]);
                            list.Rows[k]["TDS_Amnt"] = Convert.ToString(list.Rows[k]["TDS_Amnt"]);
                            list.Rows[k]["Diesel_Amnt"] = Convert.ToString(list.Rows[k]["Diesel_Amnt"]);
                        }
                    }
                }
                double dWeight = 0; double dAmount = 0; double dAdvAmnt = 0; double dBalAmnt = 0; double dDelvWeight = 0; double dShrtgAmnt = 0; double dLoading = 0; double dBC = 0;
                double dBalAcmpl = 0; double dNetAmnt = 0; double dTDS = 0; double dDesial = 0;
                //Net Total
                DataColumn newNetTotal = new DataColumn("Net Total", typeof(string));
                newNetTotal.AllowDBNull = true;
                list.Columns.Add(newNetTotal);
                    list.Columns[27].SetOrdinal(18);
                if(list.Columns.Count > 28)
                {
                    list.Columns[28].SetOrdinal(19);
                    if (list.Columns.Count > 29)
                      list.Columns[29].SetOrdinal(20);
                    if (list.Columns.Count > 30)
                    {
                        list.Columns[30].SetOrdinal(21);
                        if (list.Columns.Count > 31)
                           list.Columns[31].SetOrdinal(22);
                    }

                }
                
                // *****************************************************************************************************************************
                double dGrpTotWeight = 0; double dGrpTotAmount = 0; double dGrpTotAdvAmnt = 0; double dGrpTotBalAmnt = 0; double dGrpTotDelvWeight = 0; double dGrpTotShrtgAmnt = 0; double dGrpTotLoading = 0; double dGrpTotBC = 0;
                double dGrpTotBalAcmpl = 0; double dGrpTotNetAmnt = 0; double dGrpTotTDS = 0; double dGrpTotDesial = 0;
                string strGrpTotLory_No = Convert.ToString(list.Rows[0]["Lorry_No"]);
                //int fortotrow = 0;

                // *****************************************************************************************************************************

                for (int k = 0; k < list.Rows.Count; k++)
                {
                    list.Rows[k]["Balance Amount"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Amount"]) - (Convert.ToDouble(list.Rows[k]["Adv_Amnt"]) + Convert.ToDouble(list.Rows[k]["TDS_Amnt"]) + Convert.ToDouble(list.Rows[k]["Diesel_Amnt"]))).ToString("N2").Replace(",", "");
                    list.Rows[k]["Balance ACMPL"] = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance Amount"]) - Convert.ToDouble(list.Rows[k]["BC"])) + Convert.ToDouble(list.Rows[k]["Loading"])).ToString("N2").Replace(",", "");
                    double dLocAmnt = 0;

                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            dLocAmnt += Convert.ToDouble(list.Rows[k][arr[i]]);
                        }
                    }
                    list.Rows[k]["Net Total"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance ACMPL"]) - dLocAmnt - Convert.ToDouble(list.Rows[k]["Rassa_Chrg"]) - Convert.ToDouble(list.Rows[k]["shortage_Amount"])).ToString("N2").Replace(",", "");//1
                    dWeight += Convert.ToDouble(list.Rows[k]["Tot_Weght"]);
                    dAmount += Convert.ToDouble(list.Rows[k]["Amount"]);
                    dAdvAmnt += Convert.ToDouble(list.Rows[k]["Adv_Amnt"]);
                    dTDS += Convert.ToDouble(list.Rows[k]["TDS_Amnt"]);
                    dDesial += Convert.ToDouble(list.Rows[k]["Diesel_Amnt"]);
                    dBalAmnt += Convert.ToDouble(list.Rows[k]["Balance Amount"]);
                    dDelvWeight += Convert.ToDouble(list.Rows[k]["Delivery Weight"]);
                    dShrtgAmnt += Convert.ToDouble(list.Rows[k]["shortage_Amount"]);
                    dLoading += Convert.ToDouble(list.Rows[k]["Loading"]);
                    dBC += Convert.ToDouble(list.Rows[k]["BC"]);
                    dBalAcmpl += Convert.ToDouble(list.Rows[k]["Balance ACMPL"]);
                    dNetAmnt += Convert.ToDouble(list.Rows[k]["Net Total"]);
                    int iTot = 0;
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arrTotal[iTot] += Convert.ToDouble(list.Rows[k][arr[i]]);
                            iTot++;
                        }
                    }


                    // *****************************************************************************************************************************

                    if (Convert.ToString(list.Rows[k]["Lorry_No"]) == strGrpTotLory_No)
                    {
                        list.Rows[k]["Balance Amount"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Amount"]) - (Convert.ToDouble(list.Rows[k]["Adv_Amnt"]) + Convert.ToDouble(list.Rows[k]["TDS_Amnt"]) + Convert.ToDouble(list.Rows[k]["Diesel_Amnt"]))).ToString("N2").Replace(",", "");
                        list.Rows[k]["Balance ACMPL"] = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance Amount"]) - Convert.ToDouble(list.Rows[k]["BC"])) + Convert.ToDouble(list.Rows[k]["Loading"])).ToString("N2").Replace(",", "");
                        double dGrpTotLocAmnt = 0;
                        if (arr.Length > 0)
                        {

                            for (int i = 0; i < arr.Length; i++)
                            {
                                dGrpTotLocAmnt += Convert.ToDouble(list.Rows[k][arr[i]]);
                            }
                        }
                        //list.Rows[k]["Net Total"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance ACMPL"]) - dGrpTotLocAmnt).ToString("N2").Replace(",", "");//2
                        list.Rows[k]["Net Total"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance ACMPL"]) - dGrpTotLocAmnt - Convert.ToDouble(list.Rows[k]["Rassa_Chrg"]) - Convert.ToDouble(list.Rows[k]["shortage_Amount"])).ToString("N2").Replace(",", "");//2
                        dGrpTotWeight += Convert.ToDouble(list.Rows[k]["Tot_Weght"]);
                        dGrpTotAmount += Convert.ToDouble(list.Rows[k]["Amount"]);
                        dGrpTotAdvAmnt += Convert.ToDouble(list.Rows[k]["Adv_Amnt"]);
                        dGrpTotTDS += Convert.ToDouble(list.Rows[k]["TDS_Amnt"]);
                        dGrpTotDesial += Convert.ToDouble(list.Rows[k]["Diesel_Amnt"]);
                        dGrpTotBalAmnt += Convert.ToDouble(list.Rows[k]["Balance Amount"]);
                        dGrpTotDelvWeight += Convert.ToDouble(list.Rows[k]["Delivery Weight"]);
                        dGrpTotShrtgAmnt += Convert.ToDouble(list.Rows[k]["shortage_Amount"]);
                        dGrpTotLoading += Convert.ToDouble(list.Rows[k]["Loading"]);
                        dGrpTotBC += Convert.ToDouble(list.Rows[k]["BC"]);
                        dGrpTotBalAcmpl += Convert.ToDouble(list.Rows[k]["Balance ACMPL"]);
                        dGrpTotNetAmnt += Convert.ToDouble(list.Rows[k]["Net Total"]);
                        int iGrpTotTot = 0;
                        if (arr.Length > 0)
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                arrGrpTotal[iGrpTotTot] += Convert.ToDouble(list.Rows[k][arr[i]]);
                                iGrpTotTot++;
                            }
                        }

                    }
                    else
                    {
                        DataRow dr = list.NewRow();
                        dr.BeginEdit();
                        dr[0] = ""; dr[1] = ""; dr[2] = ""; dr[3] = ""; dr[4] = ""; dr[5] = ""; dr[6] = ""; dr[7] = "Group Total:"; dr[8] = dGrpTotWeight.ToString("N2").Replace(",", "");
                        dr[9] = ""; dr[10] = dGrpTotAmount.ToString("N2").Replace(",", ""); dr[11] = dGrpTotAdvAmnt.ToString("N2").Replace(",", ""); dr[12] = dGrpTotTDS.ToString("N2").Replace(",", ""); dr[13] = dGrpTotDesial.ToString("N2").Replace(",", ""); dr[14] = dGrpTotBalAmnt.ToString("N2").Replace(",", ""); dr[15] = dGrpTotDelvWeight.ToString("N2").Replace(",", ""); dr[16] = ""; dr[17] = dGrpTotBC.ToString("N2").Replace(",", ""); dr[18] = dGrpTotLoading.ToString("N2").Replace(",", ""); 
                        //dr[19] = dGrpTotBC.ToString("N2").Replace(",", "");
                        dr["Balance ACMPL"] = dGrpTotBalAcmpl.ToString("N2").Replace(",", ""); dr["shortage_Amount"] = dGrpTotShrtgAmnt.ToString("N2").Replace(",", ""); dr["Net Total"] = dGrpTotNetAmnt.ToString("N2").Replace(",", "");

                        if (arr.Length > 0)
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                dr[arr[i]] = Convert.ToDouble(arrGrpTotal[i]).ToString("N2").Replace(",", "");
                            }
                        }
                        list.Rows.InsertAt(dr, k);
                        list.AcceptChanges();

                        dGrpTotWeight = 0; dGrpTotAmount = 0; dGrpTotAdvAmnt = 0; dGrpTotBalAmnt = 0; dGrpTotDelvWeight = 0; dGrpTotShrtgAmnt = 0; dGrpTotLoading = 0; dGrpTotBC = 0; dGrpTotBalAcmpl = 0; dGrpTotNetAmnt = 0; dGrpTotTDS = 0; dGrpTotDesial = 0;
                        if (arr.Length > 0)
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                arrGrpTotal[i] = 0.00;
                            }
                        }
                        if (k != 0)
                        {
                            list.Rows[k + 1]["Balance Amount"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k + 1]["Amount"]) - (Convert.ToDouble(list.Rows[k + 1]["Adv_Amnt"]) + Convert.ToDouble(list.Rows[k + 1]["TDS_Amnt"]) + Convert.ToDouble(list.Rows[k + 1]["Diesel_Amnt"]))).ToString("N2").Replace(",", "");
                            list.Rows[k + 1]["Balance ACMPL"] = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(list.Rows[k + 1]["Balance Amount"]) - Convert.ToDouble(list.Rows[k + 1]["BC"])) + Convert.ToDouble(list.Rows[k + 1]["Loading"])).ToString("N2").Replace(",", "");
                            double dGrpTotLocAmnt = 0;
                            if (arr.Length > 0)
                            {

                                for (int i = 0; i < arr.Length; i++)
                                {
                                    dGrpTotLocAmnt += Convert.ToDouble(list.Rows[k + 1][arr[i]]);
                                }
                            }
                            //list.Rows[k + 1]["Net Total"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k + 1]["Balance ACMPL"]) - dGrpTotLocAmnt).ToString("N2").Replace(",", "");//3
                            list.Rows[k]["Net Total"] = Convert.ToDouble(Convert.ToDouble(list.Rows[k]["Balance ACMPL"]) - dGrpTotLocAmnt - Convert.ToDouble(list.Rows[k]["Rassa_Chrg"]) - Convert.ToDouble(list.Rows[k]["shortage_Amount"])).ToString("N2").Replace(",", "");//3
                            dGrpTotWeight += Convert.ToDouble(list.Rows[k + 1]["Tot_Weght"]);
                            dGrpTotAmount += Convert.ToDouble(list.Rows[k + 1]["Amount"]);
                            dGrpTotAdvAmnt += Convert.ToDouble(list.Rows[k + 1]["Adv_Amnt"]);
                            dGrpTotTDS += Convert.ToDouble(list.Rows[k + 1]["TDS_Amnt"]);
                            dGrpTotDesial += Convert.ToDouble(list.Rows[k + 1]["Diesel_Amnt"]);
                            dGrpTotBalAmnt += Convert.ToDouble(list.Rows[k + 1]["Balance Amount"]);
                            dGrpTotDelvWeight += Convert.ToDouble(list.Rows[k + 1]["Delivery Weight"]);
                            dGrpTotShrtgAmnt += Convert.ToDouble(list.Rows[k + 1]["shortage_Amount"]);
                            dGrpTotLoading += Convert.ToDouble(list.Rows[k + 1]["Loading"]);
                            dGrpTotBC += Convert.ToDouble(list.Rows[k + 1]["BC"]);
                            dGrpTotBalAcmpl += Convert.ToDouble(list.Rows[k + 1]["Balance ACMPL"]);
                            dGrpTotNetAmnt += Convert.ToDouble(list.Rows[k + 1]["Net Total"]);
                            int iGrpTotTot = 0;
                            if (arr.Length > 0)
                            {
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    arrGrpTotal[iGrpTotTot] += Convert.ToDouble(list.Rows[k + 1][arr[i]]);
                                    iGrpTotTot++;
                                }
                            }
                        }
                        k++;
                        strGrpTotLory_No = Convert.ToString(list.Rows[k]["Lorry_No"]);
                    }
                    // *****************************************************************************************************************************
                }

                DataRow dr1 = list.NewRow();
                dr1.BeginEdit();
                dr1[0] = ""; dr1[1] = ""; dr1[2] = ""; dr1[3] = ""; dr1[4] = ""; dr1[5] = ""; dr1[6] = ""; dr1[7] = "Group Total:"; dr1[8] = dGrpTotWeight.ToString("N2").Replace(",", "");
                dr1[9] = ""; dr1[10] = dGrpTotAmount.ToString("N2").Replace(",", ""); dr1[11] = dGrpTotAdvAmnt.ToString("N2").Replace(",", ""); dr1[12] = dGrpTotTDS.ToString("N2").Replace(",", ""); dr1[13] = dGrpTotDesial.ToString("N2").Replace(",", ""); dr1[14] = dGrpTotBalAmnt.ToString("N2").Replace(",", ""); dr1[15] = dGrpTotDelvWeight.ToString("N2").Replace(",", ""); dr1[16] = dGrpTotLoading.ToString("N2").Replace(",", ""); dr1[17] = dGrpTotBC.ToString("N2").Replace(",", ""); dr1[18] = dGrpTotBalAcmpl.ToString("N2").Replace(",", ""); dr1[22] = "";
                dr1["Balance ACMPL"] = dGrpTotBalAcmpl.ToString("N2").Replace(",", ""); ; dr1["shortage_Amount"] = dGrpTotShrtgAmnt.ToString("N2").Replace(",", ""); dr1["Net Total"] = dGrpTotNetAmnt.ToString("N2").Replace(",", "");


                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        dr1[arr[i]] = Convert.ToDouble(arrGrpTotal[i]).ToString("N2").Replace(",", "");
                    }
                }
                //list.Rows.InsertAt(dr1, k);
                list.Rows.Add(dr1);

                DataRow dr2 = list.NewRow();
                dr2.BeginEdit();
                dr2[0] = ""; dr2[1] = ""; dr2[2] = ""; dr2[3] = ""; dr2[4] = ""; dr2[5] = ""; dr2[6] = ""; dr2[7] = ""; dr2[8] = "";
                dr2[9] = ""; dr2[10] = ""; dr2[11] = ""; dr2[12] = ""; dr2[13] = ""; dr2[14] = ""; dr2[15] = ""; dr2[16] = ""; dr2[17] = "";
                dr2[18] = ""; dr2[19] = ""; dr2[20] = ""; dr2["Net Total"] = "";
                list.Rows.Add(dr2);
                DataRow dr3 = list.NewRow();
                dr3.BeginEdit();
                dr3[0] = ""; dr3[1] = ""; dr3[2] = ""; dr3[3] = ""; dr3[4] = ""; dr3[5] = ""; dr3[6] = ""; dr3[7] = "Total:"; dr3[8] = dWeight.ToString("N2").Replace(",", "");
                dr3[9] = ""; dr3[10] = dAmount.ToString("N2").Replace(",", ""); dr3[11] = dAdvAmnt.ToString("N2").Replace(",", ""); dr3[12] = dTDS.ToString("N2").Replace(",", ""); dr3[13] = dDesial.ToString("N2").Replace(",", ""); dr3[14] = dBalAmnt.ToString("N2").Replace(",", ""); dr3[15] = dDelvWeight.ToString("N2").Replace(",", ""); dr3[16] = dLoading.ToString("N2").Replace(",", ""); dr3[17] = dBC.ToString("N2").Replace(",", ""); dr3[18] = dBalAcmpl.ToString("N2").Replace(",", ""); dr3[22] = "";
                dr3["shortage_Amount"] = dShrtgAmnt.ToString("N2").Replace(",", ""); dr3["Net Total"] = dNetAmnt.ToString("N2").Replace(",", "");

                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        dr3[arr[i]] = Convert.ToDouble(arrTotal[i]).ToString("N2").Replace(",", "");
                    }
                }
                list.Rows.Add(dr3);
                //-----------------------------------------------------Modified by JEET for Prev Amnt Recvd-----------------------------
                //------------------------------------------------DO NOT DELETE BELOW COMMENTED LINES----------------------------------

                PrevAmntList = obj.SelectPayRecvdAmnt("AdvPaymentAmnt", iSenderIDNO, ApplicationFunction.ConnectionString(), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)),
                        Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)) );
                //if (PrevAmntList.Rows.Count > 0)
                //{
                //    DataRow drrow1 = list.NewRow();
                //    drrow1.BeginEdit();
                //    drrow1[0] = ""; drrow1["Chln_Date"] = "PREV AMNT RECVD"; drrow1[2] = ""; drrow1[3] = ""; drrow1[4] = ""; drrow1[5] = ""; drrow1[6] = ""; drrow1[7] = ""; drrow1[8] = "";
                //    drrow1[9] = ""; drrow1[10] = ""; drrow1[11] = ""; drrow1[12] = ""; drrow1[13] = ""; drrow1[14] = ""; drrow1[15] = ""; drrow1[16] = ""; drrow1[17] = "";
                //    drrow1[18] = ""; drrow1["Net Total"] = "";
                //    list.Rows.Add(drrow1);
                //}
                //for (int k = 0; k < PrevAmntList.Rows.Count; k++)
                //{
                //    DataRow drNewRow = list.NewRow();
                //    drNewRow.BeginEdit();
                //    drNewRow["SNo"] = Convert.ToString(PrevAmntList.Rows[k]["SNo"]);
                //    drNewRow["Chln_Date"] = Convert.ToString(PrevAmntList.Rows[k]["Chln_Date"]);
                //    drNewRow["Chln_No"] = Convert.ToString(PrevAmntList.Rows[k]["Chln_No"]);
                //    drNewRow["Gr_No"] = Convert.ToString(PrevAmntList.Rows[k]["GR_no"]);
                //    drNewRow["Office"] = Convert.ToString(PrevAmntList.Rows[k]["Office"]);
                //    drNewRow["Item_Name"] = Convert.ToString(PrevAmntList.Rows[k]["Item_Name"]);
                //    drNewRow["Lorry_No"] = Convert.ToString(PrevAmntList.Rows[k]["Lorry_No"]);
                //    drNewRow["City_Name"] = Convert.ToString(PrevAmntList.Rows[k]["City"]);
                //    drNewRow["Tot_Weght"] = Convert.ToString(PrevAmntList.Rows[k]["Tot_Weght"]);
                //    drNewRow["Item_Rate"] = Convert.ToString(PrevAmntList.Rows[k]["Item_Rate"]);
                //    drNewRow["Amount"] = Convert.ToString(PrevAmntList.Rows[k]["Amount"]);
                //    drNewRow["Adv_Amnt"] = Convert.ToString(PrevAmntList.Rows[k]["Adv_Amnt"]);
                //    drNewRow["TDS_Amnt"] = Convert.ToString(PrevAmntList.Rows[k]["TDS_Amnt"]);
                //    drNewRow["Balance Amount"] = Convert.ToString(PrevAmntList.Rows[k]["Balance Amount"]);
                //    drNewRow["Delivery Weight"] = Convert.ToString(PrevAmntList.Rows[k]["Delivery Weight"]);
                //    drNewRow["Loading"] = Convert.ToString(PrevAmntList.Rows[k]["Loading"]);
                //    drNewRow["BC"] = Convert.ToString(PrevAmntList.Rows[k]["BC"]);
                //    drNewRow["Shortage_Qty"] = Convert.ToString(PrevAmntList.Rows[k]["Shortage_Qty"]);
                //    drNewRow["shortage_Amount"] = Convert.ToString(PrevAmntList.Rows[k]["shortage_Amount"]);
                //    drNewRow["Balance ACMPL"] = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(PrevAmntList.Rows[k]["Balance Amount"]) - Convert.ToDouble(PrevAmntList.Rows[k]["BC"])) + Convert.ToDouble(PrevAmntList.Rows[k]["Loading"])).ToString("N2").Replace(",", "");
                //    drNewRow[Convert.ToString(PrevAmntList.Rows[k]["City"])] = Convert.ToString(PrevAmntList.Rows[k]["Recvd_Amnt"]);

                //    list.Rows.Add(drNewRow);
                //}
                if (PrevAmntList.Rows.Count > 0)
                {
                    SumObj = PrevAmntList.Compute("Sum(Recvd_Amnt)", "");

                    //DataRow drrow = list.NewRow();
                    //drrow.BeginEdit();
                    //drrow[0] = ""; drrow[1] = ""; drrow[2] = ""; drrow[3] = ""; drrow[4] = ""; drrow[5] = ""; drrow[6] = ""; drrow[7] = "Total:"; drrow[8] = "";
                    //drrow[9] = ""; drrow[10] = ""; drrow[11] = ""; drrow[12] = ""; drrow[13] = ""; drrow[14] = ""; drrow[15] = ""; drrow[16] = ""; drrow[17] = "";
                    //drrow[Convert.ToString(PrevAmntList.Rows[0]["City"])] = Convert.ToDouble(SumObj).ToString("N2"); drrow["Net Total"] = "";
                    //list.Rows.Add(drrow);
                }
                //-----------------------------------------------------------------------------------------------
                if (DtVchr != null && DtVchr.Rows.Count > 1)
                {
                    DataRow drNew1 = list.NewRow();
                    drNew1.BeginEdit();
                    drNew1[0] = ""; drNew1[1] = ""; drNew1[2] = ""; drNew1[3] = ""; drNew1[4] = ""; drNew1[5] = ""; drNew1[6] = ""; drNew1[7] = ""; drNew1[8] = "";
                    drNew1[9] = ""; drNew1[10] = ""; drNew1[11] = ""; drNew1[12] = ""; drNew1[13] = ""; drNew1[14] = ""; drNew1[15] = ""; drNew1[16] = ""; drNew1[17] = "";
                    drNew1[18] = ""; drNew1[19] = ""; drNew1[20] = ""; drNew1["Net Total"] = "";
                    list.Rows.Add(drNew1);
                    for (int i = 0; i < DtVchr.Rows.Count; i++)
                    {
                        DataRow DrNew = list.NewRow();
                        DrNew.BeginEdit();
                        if (i == 0)
                        {
                            DrNew["Delivery Weight"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Date"]);
                            DrNew["Loading"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Type"]);
                            DrNew["BC"] = Convert.ToString(DtVchr.Rows[i]["Debit"]);
                            DrNew["Balance ACMPL"] = Convert.ToString(DtVchr.Rows[i]["Credit"]);
                            DrNew["Net Total"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Narr"]);
                        }
                        else
                        {
                            DrNew["Delivery Weight"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Date"]);
                            DrNew["Loading"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Type"]);
                            DrNew["BC"] = Convert.ToString(DtVchr.Rows[i]["Debit"]);
                            if (i == 1)
                            {     //Comment by salman
                                //double BalWoutShrtg = Convert.ToDouble(DtVchr.Rows[i]["Credit"]) - totshrtg;
                                double BalWoutShrtg = Convert.ToDouble(DtVchr.Rows[i]["Credit"]) ;
                                DrNew["Balance ACMPL"] = Convert.ToString(BalWoutShrtg);
                                dTotCredit = Convert.ToDouble(BalWoutShrtg);
                            }
                            else
                            {
                                DrNew["Balance ACMPL"] = Convert.ToString(DtVchr.Rows[i]["Credit"]);
                                dTotCredit += Convert.ToDouble(DtVchr.Rows[i]["Credit"]);
                            }
                            DrNew["Net Total"] = Convert.ToString(DtVchr.Rows[i]["Vchr_Narr"]);
                            dTotDebit += Convert.ToDouble(DtVchr.Rows[i]["Debit"]);

                        }

                        list.Rows.Add(DrNew);
                    }
                    DataRow drNew6 = list.NewRow();
                    drNew6.BeginEdit();
                    drNew6["Loading"] = "Total";
                    drNew6["BC"] = Convert.ToDouble(dTotDebit).ToString("N2").Replace(",", "");
                    drNew6["Balance ACMPL"] = Convert.ToDouble(dTotCredit).ToString("N2").Replace(",", ""); ;
                    list.Rows.Add(drNew6);
                    DataRow drNew2 = list.NewRow();
                    drNew2.BeginEdit();
                    drNew2[0] = ""; drNew2[1] = ""; drNew2[2] = ""; drNew2[3] = ""; drNew2[4] = ""; drNew2[5] = ""; drNew2[6] = ""; drNew2[7] = ""; drNew2[8] = "";
                    drNew2[9] = ""; drNew2[10] = ""; drNew2[11] = ""; drNew2[12] = ""; drNew2[13] = ""; drNew2[14] = ""; drNew2[15] = ""; drNew2[16] = ""; drNew2[17] = "";
                    drNew2[18] = ""; drNew2[19] = ""; drNew2[20] = ""; drNew2["Net Total"] = "";
                    list.Rows.Add(drNew2);
                    DataRow drNew3 = list.NewRow();
                    drNew3.BeginEdit();

                    drNew3["Balance Amount"] = "Total Challan Amount";
                    drNew3["Delivery Weight"] = "Prev Pay Amount";
                    //drNew3["Delivery Weight"] = "Total Challan Amount";
                    drNew3["Loading"] = "Total Shortage Amount";
                    drNew3["BC"] = "Total Vchr Amount";
                    drNew3["Net Total"] = "Net Balance (Without Shortage)";
                    drNew3["Balance ACMPL"] = "Bal With Shortage";
                    list.Rows.Add(drNew3);
                    DataRow drNew4 = list.NewRow();
                    drNew4.BeginEdit();
                    drNew4["Balance Amount"] = (dNetAmnt).ToString("N2").Replace(",", "");
                    drNew4["Delivery Weight"] = Convert.ToDouble(Convert.ToString((SumObj)==null?"0.00":SumObj)).ToString("N2").Replace(",", "");
                    //drNew4["Delivery Weight"] = dNetAmnt.ToString("N2").Replace(",", "");
                    drNew4["Loading"] = dShrtgAmnt.ToString("N2").Replace(",", "");
                    drNew4["BC"] = Convert.ToDouble(dTotDebit - dTotCredit).ToString("N2").Replace(",", "");
                    //NetAmount Calculation
                    NetMinShrt = dNetAmnt  - Convert.ToDouble(SumObj);
                    drNew4["Net Total"] = Convert.ToDouble(NetMinShrt - Convert.ToDouble(dTotDebit - dTotCredit)).ToString("N2").Replace(",", "");
                    drNew4["Balance ACMPL"] = Convert.ToDouble(NetMinShrt - Convert.ToDouble(dTotDebit - dTotCredit) + dShrtgAmnt).ToString("N2").Replace(",", "");

                    list.Rows.Add(drNew4);
                }
                grdMain.Columns.Clear();
                for (int i = 0; i < list.Columns.Count; i++)
                {
                    BoundField bfield = new BoundField();
                    bfield.HeaderText = Convert.ToString(list.Columns[i].ColumnName);
                    bfield.DataField = Convert.ToString(list.Columns[i].ColumnName);
                    grdMain.Columns.Add(bfield);
                }
               // list.Columns["Balance ACMPL"].ColumnName = "Paid Amount";
                //list.AcceptChanges();
                ViewState["CSVdt"] = list;
                grdMain.DataSource = list;
                grdMain.DataBind();
                if(list.Rows.Count>0)
                     lnkbtnPrint.Visible = true;
                //prints.Visible = true;
                imgCSV.Visible = true;
                lblTotalRecord.Text = "Total Record (s): " + list.Rows.Count;
                grdMain.HeaderRow.Cells[0].Text = "SNO.";
                grdMain.HeaderRow.Cells[1].Text = "Chln Date";
                grdMain.HeaderRow.Cells[2].Text = "Office";
                grdMain.HeaderRow.Cells[3].Text = "Chln No";
                grdMain.HeaderRow.Cells[4].Text = "Gr No.";
                grdMain.HeaderRow.Cells[5].Text = "Item Name";
                grdMain.HeaderRow.Cells[6].Text = "Lorry No";
                grdMain.HeaderRow.Cells[7].Text = "To city";
                grdMain.HeaderRow.Cells[8].Text = "Weight";
                grdMain.HeaderRow.Cells[9].Text = "Rate";
                grdMain.HeaderRow.Cells[10].Text = "Amount";
                grdMain.HeaderRow.Cells[11].Text = "Adv. Amnt";
                grdMain.HeaderRow.Cells[12].Text = "TDS Amnt";
                grdMain.HeaderRow.Cells[13].Text = "Diesel Amnt";
                grdMain.HeaderRow.Cells[14].Text = "Bal. Amnt";
                grdMain.HeaderRow.Cells[15].Text = "Delv.Weight";
                grdMain.HeaderRow.Cells[16].Text = "Loading";
                grdMain.HeaderRow.Cells[17].Text = "BC";
                grdMain.HeaderRow.Cells[18].Text = "Paid Amount";
                grdMain.HeaderRow.Cells[18].Width = 50;
                
                if (list.Columns.Count < 30)
                {
                    grdMain.HeaderRow.Cells[22].Text = "Shrtg.";
                    grdMain.HeaderRow.Cells[23].Text = "Shrt. Amnt";
                }
                
                grdMain.Columns[19].Visible = true;
                grdMain.Columns[20].Visible = true;
                grdMain.Columns[21].Visible = true;
                grdMain.Columns[24].Visible = false;
                grdMain.Columns[25].Visible = false;
                grdMain.Columns[26].Visible = false;
                grdMain.Columns[27].Visible = false;
                if (list.Columns.Count == 29)
                {
                    grdMain.Columns[28].Visible = false;
                    grdMain.Columns[23].Visible = false;
                    grdMain.HeaderRow.Cells[21].Text = "Shrtg.";
                    grdMain.HeaderRow.Cells[22].Text = "Shrt. Amnt";
                }
                else if (list.Columns.Count >29)
                {
                    grdMain.Columns[28].Visible = false;
                    grdMain.Columns[29].Visible = false;
                    if (list.Columns.Count > 30)
                    {
                        grdMain.HeaderRow.Cells[23].Text = "Shrtg.";
                        grdMain.HeaderRow.Cells[24].Text = "Shrt. Amnt";
                        grdMain.Columns[24].Visible = true;
                        grdMain.Columns[25].Visible = false;
                        grdMain.Columns[30].Visible = false;
                        if (list.Columns.Count > 31)
                        {
                            grdMain.HeaderRow.Cells[24].Text = "Shrtg.";
                            grdMain.HeaderRow.Cells[25].Text = "Shrt. Amnt";
                            grdMain.Columns[24].Visible = true;
                            grdMain.Columns[25].Visible = true;
                            grdMain.Columns[30].Visible = false;
                            grdMain.Columns[31].Visible = false;
                        }
                    }
                }
                else // for AGTCLive
                {
                    grdMain.HeaderRow.Cells[20].Text = "Shrtg.";
                    grdMain.HeaderRow.Cells[21].Text = "Shrt. Amnt";
                    grdMain.Columns[22].Visible = false;
                    grdMain.Columns[23].Visible = false;
                }
                #region This is for printGrid.....
                grdPrintDtl.Width = new Unit("1400px");
                grdPrintDtl.Columns.Clear();
                for (int i = 0; i < list.Columns.Count; i++)
                {
                    BoundField bfield = new BoundField();
                    bfield.HeaderText = Convert.ToString(list.Columns[i].ColumnName);
                    bfield.DataField = Convert.ToString(list.Columns[i].ColumnName);
                    grdPrintDtl.Columns.Add(bfield);
                }
                grdPrintDtl.DataSource = list;
                grdPrintDtl.DataBind();

                grdPrintDtl.HeaderStyle.Width = new Unit("90px");
                grdPrintDtl.HeaderRow.Cells[0].Text = "SNO.";
                grdPrintDtl.HeaderRow.Cells[1].Text = "Date";
                grdPrintDtl.HeaderRow.Cells[2].Text = "Office";
                grdPrintDtl.HeaderRow.Cells[3].Text = "Chln No";
                grdPrintDtl.HeaderRow.Cells[4].Text = "GR No.";
                grdPrintDtl.HeaderRow.Cells[5].Text = "Item";
                grdPrintDtl.HeaderRow.Cells[6].Text = "Lorry No";
                grdPrintDtl.HeaderRow.Cells[7].Text = "City";
                grdPrintDtl.HeaderRow.Cells[8].Text = "Tot Weght";
                grdPrintDtl.HeaderRow.Cells[9].Text = "Item Rate";
                grdPrintDtl.HeaderRow.Cells[10].Text = "Amount";
                grdPrintDtl.HeaderRow.Cells[11].Text = "Adv. Amnt";
                grdPrintDtl.HeaderRow.Cells[12].Text = "TDS Amnt";
                grdPrintDtl.HeaderRow.Cells[13].Text = "Diesel Amnt";
                grdPrintDtl.HeaderRow.Cells[14].Text = "Bal. Amnt";
                grdPrintDtl.HeaderRow.Cells[15].Text = "Delvry Weight";
                grdPrintDtl.HeaderRow.Cells[16].Text = "Loading";
                grdPrintDtl.HeaderRow.Cells[17].Text = "BC";
                grdPrintDtl.HeaderRow.Cells[18].Text = "Paid Amount";
                if (list.Columns.Count < 30)
                {
                    grdPrintDtl.HeaderRow.Cells[22].Text = "Shrtg.";
                    grdPrintDtl.HeaderRow.Cells[23].Text = "Shrt. Amnt";
                }
                if (ddlParty.SelectedValue == "0")
                {
                    grdPrintDtl.HeaderRow.Cells[18].Text = "Paid Amount";
                    grdPrintDtl.Columns[19].Visible = false;
                    grdPrintDtl.Columns[20].Visible = false;
                }
                else
                {
                    grdPrintDtl.Columns[19].Visible = true;
                    grdPrintDtl.Columns[20].Visible = true;
                }
                grdPrintDtl.Columns[21].Visible = false;
                grdPrintDtl.Columns[24].Visible = false;
                grdPrintDtl.Columns[25].Visible = false;
                grdPrintDtl.Columns[26].Visible = false;
                grdPrintDtl.Columns[27].Visible = false;
                if (list.Columns.Count == 29)
                {
                    grdPrintDtl.Columns[28].Visible = false;
                    grdPrintDtl.Columns[23].Visible = false;
                    grdPrintDtl.Columns[21].Visible = true;
                    grdPrintDtl.HeaderRow.Cells[21].Text = "Shrtg.";
                    grdPrintDtl.HeaderRow.Cells[22].Text = "Shrt. Amnt";
                }
                else if (list.Columns.Count > 29)
                {
                    grdPrintDtl.Columns[28].Visible = false;
                    grdPrintDtl.Columns[29].Visible = false;
                    if (list.Columns.Count > 30)
                    {
                        grdPrintDtl.HeaderRow.Cells[23].Text = "Shrtg.";
                        grdPrintDtl.HeaderRow.Cells[24].Text = "Shrt. Amnt";
                        grdPrintDtl.Columns[22].Visible = false;
                        grdPrintDtl.Columns[24].Visible = true;
                        grdPrintDtl.Columns[25].Visible = false;
                        grdPrintDtl.Columns[30].Visible = false;
                        if (list.Columns.Count > 31)
                        {
                            grdPrintDtl.HeaderRow.Cells[24].Text = "Shrtg.";
                            grdPrintDtl.HeaderRow.Cells[25].Text = "Shrt. Amnt";
                            grdPrintDtl.Columns[22].Visible = false;
                            grdPrintDtl.Columns[24].Visible = true;
                            grdPrintDtl.Columns[25].Visible = true;
                            grdPrintDtl.Columns[30].Visible = false;
                            grdPrintDtl.Columns[31].Visible = false;
                        }
                    }
                }
                else // for AGTCLive
                {
                    grdPrintDtl.HeaderRow.Cells[20].Text = "Shrtg.";
                    grdPrintDtl.HeaderRow.Cells[21].Text = "Shrt. Amnt";
                    grdPrintDtl.Columns[20].Visible = true;
                    grdPrintDtl.Columns[21].Visible = true;
                    grdPrintDtl.Columns[22].Visible = false;
                    grdPrintDtl.Columns[23].Visible = false;
                }
                grdPrintDtl.Columns[9].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[10].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[11].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[12].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[13].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[14].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[15].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[16].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[17].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[18].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[19].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdPrintDtl.Columns[20].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                #endregion

            }
            else if (DtVchr != null && DtVchr.Rows.Count > 1)
            {
                DtVchr.Rows.RemoveAt(0);
                for (int i = 0; i < DtVchr.Rows.Count; i++)
                {
                    dTotDebit += Convert.ToDouble(DtVchr.Rows[i]["Debit"]);
                    if (i == 0)
                    { 
                        //Comment by salman
                        //dTotCredit += Convert.ToDouble(DtVchr.Rows[i]["Credit"]) - totshrtg;
                        dTotCredit += Convert.ToDouble(DtVchr.Rows[i]["Credit"]);
                        DtVchr.Rows[i][3] = Convert.ToDouble(dTotCredit);
                    }
                    else
                        dTotCredit += Convert.ToDouble(DtVchr.Rows[i]["Credit"]);
                }
                DataRow drNew3 = DtVchr.NewRow();
                drNew3.BeginEdit();
                drNew3["Vchr_Type"] = "";
                drNew3["Debit"] = "";
                drNew3["Credit"] = "";
                DtVchr.Rows.Add(drNew3);
                DataRow drNew4 = DtVchr.NewRow();
                drNew4.BeginEdit();
                drNew4["Vchr_Type"] = "Total";
                drNew4["Debit"] = Convert.ToDouble(dTotDebit).ToString("N2").Replace(",", "");
                drNew4["Credit"] = Convert.ToDouble(dTotCredit).ToString("N2").Replace(",", "");
                DtVchr.Rows.Add(drNew4);
                DataRow drNew2 = DtVchr.NewRow();
                drNew2.BeginEdit();
                drNew2[0] = ""; drNew2[1] = ""; drNew2[2] = ""; drNew2[3] = "";
                DtVchr.Rows.Add(drNew2);
                DataRow drNew5 = DtVchr.NewRow();
                drNew5.BeginEdit();
                drNew5["Vchr_Type"] = "Total Challan Amount";
                drNew5["Debit"] = "Total Vchr Amount";
                drNew5["Credit"] = "Net Balance";
                DtVchr.Rows.Add(drNew5);
                DataRow drNew6 = DtVchr.NewRow();
                drNew6.BeginEdit();
                drNew6["Vchr_Type"] = "0.00";
                drNew6["Debit"] = Convert.ToDouble(dTotDebit - dTotCredit).ToString("N2").Replace(",", "");
                drNew6["Credit"] = Convert.ToDouble(0 - Convert.ToDouble(dTotDebit - dTotCredit)).ToString("N2").Replace(",", "");
                DtVchr.Rows.Add(drNew6);

                grdMain.Width = new Unit("600px");
                grdMain.Columns.Clear();
                for (int i = 0; i < DtVchr.Columns.Count; i++)
                {
                    BoundField bfield = new BoundField();
                    bfield.HeaderText = Convert.ToString(DtVchr.Columns[i].ColumnName);
                    bfield.DataField = Convert.ToString(DtVchr.Columns[i].ColumnName);
                    grdMain.Columns.Add(bfield);
                }


                ViewState["CSVdt"] = DtVchr;
                grdMain.DataSource = DtVchr;
                grdMain.DataBind();
                int temp = grdMain.Columns.Count;
                if (list.Rows.Count > 0)
                    lnkbtnPrint.Visible = true;
                grdMain.HeaderRow.Cells[0].Text = "Vchr Date";
                grdMain.HeaderRow.Cells[1].Text = "Vchr Type";
                grdMain.HeaderRow.Cells[2].Text = "Debit";
                grdMain.HeaderRow.Cells[3].Text = "Credit";

                #region This is for Printing Grid.....
                grdPrintDtl.Width = new Unit("1400px");
                grdPrintDtl.Columns.Clear();
                for (int i = 0; i < DtVchr.Columns.Count; i++)
                {
                    BoundField bfield = new BoundField();
                    bfield.HeaderText = Convert.ToString(DtVchr.Columns[i].ColumnName);
                    bfield.DataField = Convert.ToString(DtVchr.Columns[i].ColumnName);
                    grdPrintDtl.Columns.Add(bfield);
                }
                grdPrintDtl.DataSource = DtVchr;
                grdPrintDtl.DataBind();
                grdPrintDtl.HeaderRow.Cells[0].Text = "Vchr Date";
                grdPrintDtl.HeaderRow.Cells[1].Text = "Vchr Type";
                grdPrintDtl.HeaderRow.Cells[2].Text = "Debit";
                grdPrintDtl.HeaderRow.Cells[3].Text = "Credit";

                #endregion
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();

                grdPrintDtl.DataSource = null;
                grdPrintDtl.DataBind();

                //prints.Visible = false;
                //   printRep.Visible = false;
                imgCSV.Visible = false;
                lnkbtnPrint.Visible = false;
                lblTotalRecord.Text = "Total Record (s): 0 ";
            }
            //}
            //catch (Exception Ex)
            //{

            //}

        }
        #endregion

        #region Grid Event...

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int temp = grdMain.Columns.Count;
            if (temp > 17)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (e.Row.Cells[17].Text == "Balance ACMPL")
                    {
                        e.Row.Cells[17].Text = "Amount";
                    }
                    if (e.Row.Cells[16].Text == "Balance ACMPL")
                    {
                        e.Row.Cells[16].Text = "Amount";
                    }
                    if (e.Row.Cells[18].Text == "Balance ACMPL")
                    {
                        e.Row.Cells[18].Text = "Amount";
                    }
                    if (e.Row.Cells[19].Text == "Balance ACMPL")
                    {
                        e.Row.Cells[19].Text = "Amount";
                    }

                }
            }           
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (list != null && list.Rows.Count > 0)
                    {
                        TableCell cell = new TableCell();
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[0].Width = new Unit("50px");
                    e.Row.Cells[1].Width = new Unit("80px");
                    e.Row.Cells[2].Width = new Unit("100px");
                    e.Row.Cells[3].Width = new Unit("60px");
                    e.Row.Cells[4].Width = new Unit("60px");
                    e.Row.Cells[5].Width = new Unit("100px");
                    e.Row.Cells[6].Width = new Unit("100px");
                    e.Row.Cells[7].Width = new Unit("100px");

                    e.Row.Cells[8].Width = new Unit("100px");
                    e.Row.Cells[9].Width = new Unit("100px");
                    e.Row.Cells[10].Width = new Unit("100px");
                    e.Row.Cells[11].Width = new Unit("100px");
                    e.Row.Cells[12].Width = new Unit("100px");
                    e.Row.Cells[13].Width = new Unit("100px");
                    e.Row.Cells[14].Width = new Unit("100px");
                    e.Row.Cells[15].Width = new Unit("100px");
                    e.Row.Cells[16].Width = new Unit("100px");
                    e.Row.Cells[17].Width = new Unit("100px");
                    e.Row.Cells[18].Width = new Unit("100px");
                    e.Row.Cells[19].Width = new Unit("100px");

                    for (int i = 19; i < e.Row.Cells.Count; i++)
                    {
                        cell = e.Row.Cells[i];
                        cell.HorizontalAlign = HorizontalAlign.Right;
                        cell.Width = new Unit("100px");
                    }
                    if (e.Row.RowIndex == list.Rows.Count - 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.SkyBlue;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Font.Bold = true;// = System.Drawing.Color.Black;
                        e.Row.Font.Size = 10;
                    }

                    if (e.Row.RowType == DataControlRowType.Header)
                    {

                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;

                        e.Row.Cells[0].Width = new Unit("50px");
                        e.Row.Cells[1].Width = new Unit("80px");
                        e.Row.Cells[2].Width = new Unit("100px");
                        e.Row.Cells[3].Width = new Unit("60px");
                        e.Row.Cells[4].Width = new Unit("60px");
                        e.Row.Cells[5].Width = new Unit("100px");
                        e.Row.Cells[6].Width = new Unit("100px");
                        e.Row.Cells[7].Width = new Unit("100px");

                        e.Row.Cells[8].Width = new Unit("100px");
                        e.Row.Cells[9].Width = new Unit("100px");
                        e.Row.Cells[10].Width = new Unit("100px");
                        e.Row.Cells[11].Width = new Unit("100px");
                        e.Row.Cells[12].Width = new Unit("100px");
                        e.Row.Cells[13].Width = new Unit("100px");
                        e.Row.Cells[14].Width = new Unit("100px");
                        e.Row.Cells[15].Width = new Unit("100px");
                        e.Row.Cells[16].Width = new Unit("100px");
                        e.Row.Cells[17].Width = new Unit("100px");
                        e.Row.Cells[18].Width = new Unit("100px");
                        e.Row.Cells[19].Width = new Unit("100px");
                        for (int i = 19; i < e.Row.Cells.Count; i++)
                        {
                            cell = e.Row.Cells[i];

                            // right-align each of the column cells after the first
                            // and set the width
                            cell.HorizontalAlign = HorizontalAlign.Right;
                            cell.Width = new Unit("100px");
                        }
                    }


                }
                else if (DtVchr != null && DtVchr.Rows.Count > 0)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[0].Width = new Unit("80px");
                        e.Row.Cells[1].Width = new Unit("100px");
                        e.Row.Cells[2].Width = new Unit("100px");
                        e.Row.Cells[3].Width = new Unit("100px");
                    }
                    else if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[0].Width = new Unit("80px");
                        e.Row.Cells[1].Width = new Unit("100px");
                        e.Row.Cells[2].Width = new Unit("100px");
                        e.Row.Cells[3].Width = new Unit("100px");
                    }
                }
            }

        }
        protected void grdPrintDtl_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.Cells[17].Text == "Balance ACMPL")
            {
                e.Row.Cells[17].Text = "Amount";
            }
            if (e.Row.Cells[16].Text == "Balance ACMPL")
            {
                e.Row.Cells[16].Text = "Amount";
            }
            if (e.Row.Cells[18].Text == "Balance ACMPL")
            {
                e.Row.Cells[18].Text = "Amount";
            }
            if (e.Row.Cells[19].Text == "Balance ACMPL")
            {
                e.Row.Cells[19].Text = "Amount";
            }
            

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Date Range FinYear ...
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex != 0)
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = hidmaxdate.Value;
            }
            else
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
        }
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
        private void ExportGridView()
        {
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void ExportDataTableToCSV(DataTable dataTable, string FileName)
        {
            string CSVFileName = FileName.Replace(" ", "_");
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }
        #endregion
    }
}