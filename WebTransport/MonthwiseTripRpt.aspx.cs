using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Drawing;

namespace WebTransport
{
    public partial class MonthwiseTripRpt : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        GRRepDAL objGRDAL = new GRRepDAL();
        private int intFormId = 39;
        double NetAmnt = 0.00;
        double GrossAmnt = 0.00;
        double IncetAmnt = 0.00;
        double ChlnTot = 0.00;
        double FuelTot = 0.00;
        double ExpnTot = 0.00;
        double TollTot = 0.00;
        double FTripNetTot = 0;
        double FPurNetTot = 0;
        double FVchrNetTot = 0;
        DataTable CSVTable = new DataTable();
        int Is = 0;
        int Is2 = 0;
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
                //    if (base.CheckUserRights(intFormId) == false)
                //    {
                //        Response.Redirect("PermissionDenied.aspx");
                //    }
                //    if (base.View == false)
                //    {
                //        btnSearch.Visible = true;
                //    }

                this.BindTruckNo();
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
            }
            ddlDateRange.Focus();
        }
        #endregion

        #region Button Event...
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlDateRange.SelectedIndex = 0;
            ddlTruckNo.SelectedIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Bind Event...
        private void BindTruckNo()
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
        private void BindGrid()
        {

            //try
            //{
            FTripNetTot = 0;
            FPurNetTot = 0;
            FVchrNetTot = 0;
            MonthWiseTripRptDAL obj = new MonthWiseTripRptDAL();
            Int32 iYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
            Int32 iTruckIdno = (ddlTruckNo.SelectedIndex <= 0 ? 0 : Convert.ToInt32(ddlTruckNo.SelectedValue));
            Int32 iMonth = Convert.ToInt32(ddlMonth.SelectedValue);

            DataTable list = obj.SelectRep(iYearIdno, iMonth, iTruckIdno, conString);
            DataTable listPur = obj.SelectPurDetl(iYearIdno, iMonth, conString);
            DataTable listVchr = obj.SelectVchrDetl(iYearIdno, iMonth, iTruckIdno, conString);
            DataTable DtOpen = obj.SelectOpening(iYearIdno, iMonth, iTruckIdno, conString);
            if (list != null && list.Rows.Count > 0)
            {
                #region DateConverstion in String / DTChild Converstion in String
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    list.Rows[i]["Gr_Date"] = Convert.ToDateTime(list.Rows[i]["Gr_Date"]).ToString("dd-MM-yyyy");
                    list.AcceptChanges();
                }
                DataTable DtChild = list.Clone();
                for (int i = 0; i < DtChild.Columns.Count; i++)
                {
                    DtChild.Columns[i].DataType = typeof(string);
                }
                #endregion

                int PrvId = 0;
                int PrvTripId = 0;
                double dAdvAmnt = 0;
                double dCommAmnt = 0;
                double dTDSAmnt = 0;
                double dNetAmnt = 0;
                for (int i = 0; i < list.Rows.Count; i++)
                {

                    if (list.Rows.Count == 1) // Only If List Have Single Record
                    {
                        #region Only When List Have Single Records
                        DataRow DDr = DtChild.NewRow();
                        DDr = list.Rows[i];
                        DtChild.ImportRow(DDr);

                        dNetAmnt += Convert.ToDouble(list.Rows[i]["Net_Amnt"]);

                        //
                        dAdvAmnt = Convert.ToDouble(list.Rows[i]["Adv_Amnt"]);
                        dCommAmnt = Convert.ToDouble(list.Rows[i]["Commsn_Amnt"]);
                        dTDSAmnt = Convert.ToDouble(list.Rows[i]["TDSTax_Amnt"]);
                        DataRow Dr = DtChild.NewRow();
                        Dr["Acnt_Name"] = Convert.ToString("Invoice Total");
                        Dr["Chln_No"] = "Freight";
                        Dr["Gr_Date"] = "Adv. Amnt";
                        Dr["FromCity"] = "Comm. Amnt";
                        Dr["ToCity"] = "TDS Amnt";
                        Dr["Net_Amnt"] = "Balance";
                        DtChild.Rows.Add(Dr);

                        //
                        DataRow Drr = DtChild.NewRow();
                        Drr["Chln_No"] = Convert.ToDouble(dNetAmnt).ToString("N2");
                        Drr["Gr_Date"] = Convert.ToDouble(dAdvAmnt).ToString("N2");
                        Drr["FromCity"] = Convert.ToDouble(dCommAmnt).ToString("N2");
                        Drr["ToCity"] = Convert.ToDouble(dTDSAmnt).ToString("N2");
                        Drr["Net_Amnt"] = (dNetAmnt - (dAdvAmnt + dCommAmnt + dTDSAmnt)).ToString("N2");
                        DtChild.Rows.Add(Drr);
                        #endregion
                    }
                    else  // if List Have More then One Records
                    {
                        if (i == 0 && list.Rows.Count > 1)
                        {
                            //DataRow Dr = DtChild.NewRow();
                            //Dr = list.Rows[i];
                            //DtChild.ImportRow(Dr);
                            PrvId = Convert.ToInt32(list.Rows[i]["Chln_Idno"]);
                            //dNetAmnt = Convert.ToDouble(list.Rows[i]["Net_Amnt"]);
                            PrvTripId = Convert.ToInt32(list.Rows[i]["Trip_Idno"]);
                            //continue;
                        }
                        if (i != list.Rows.Count - 1 && PrvId != Convert.ToInt32(list.Rows[i + 1]["Chln_Idno"])) // if Not Last Record & Challan No Not Change
                        {
                            DataRow Drrr = DtChild.NewRow();
                            Drrr = list.Rows[i];
                            DtChild.ImportRow(Drrr);
                            PrvId = Convert.ToInt32(list.Rows[i]["Chln_Idno"]);

                            dNetAmnt += Convert.ToDouble(list.Rows[i]["Net_Amnt"]);

                            //
                            dAdvAmnt = Convert.ToDouble(list.Rows[i]["Adv_Amnt"]);
                            dCommAmnt = Convert.ToDouble(list.Rows[i]["Commsn_Amnt"]);
                            dTDSAmnt = Convert.ToDouble(list.Rows[i]["TDSTax_Amnt"]);
                            DataRow Dr = DtChild.NewRow();
                            Dr["Acnt_Name"] = Convert.ToString("Invoice Total");
                            Dr["Chln_No"] = "Freight";
                            Dr["Gr_Date"] = "Adv. Amnt";
                            Dr["FromCity"] = "Comm. Amnt";
                            Dr["ToCity"] = "TDS Amnt";
                            Dr["Net_Amnt"] = "Balance";
                            DtChild.Rows.Add(Dr);

                            //
                            DataRow Drr = DtChild.NewRow();
                            Drr["Chln_No"] = Convert.ToDouble(dNetAmnt).ToString("N2");
                            Drr["Gr_Date"] = Convert.ToDouble(dAdvAmnt).ToString("N2");
                            Drr["FromCity"] = Convert.ToDouble(dCommAmnt).ToString("N2");
                            Drr["ToCity"] = Convert.ToDouble(dTDSAmnt).ToString("N2");
                            Drr["Net_Amnt"] = (dNetAmnt - (dAdvAmnt + dCommAmnt + dTDSAmnt)).ToString("N2");
                            DtChild.Rows.Add(Drr);
                           
                            dNetAmnt = 0;
                        }
                        else
                        {
                            DataRow Drrr = DtChild.NewRow();
                            Drrr = list.Rows[i];
                            DtChild.ImportRow(Drrr);
                            PrvId = Convert.ToInt32(list.Rows[i]["Chln_Idno"]);
                            dNetAmnt += Convert.ToDouble(list.Rows[i]["Net_Amnt"]);

                            if (i == list.Rows.Count - 1)
                            {
                                dAdvAmnt = Convert.ToDouble(list.Rows[i]["Adv_Amnt"]);
                                dCommAmnt = Convert.ToDouble(list.Rows[i]["Commsn_Amnt"]);
                                dTDSAmnt = Convert.ToDouble(list.Rows[i]["TDSTax_Amnt"]);
                                DataRow Dr = DtChild.NewRow();
                                Dr["Acnt_Name"] = Convert.ToString("Invoice Total");
                                Dr["Chln_No"] = "Freight";
                                Dr["Gr_Date"] = "Adv. Amnt";
                                Dr["FromCity"] = "Comm. Amnt";
                                Dr["ToCity"] = "TDS Amnt";
                                Dr["Net_Amnt"] = "Balance";
                                DtChild.Rows.Add(Dr);

                                //
                                DataRow Drr = DtChild.NewRow();
                                Drr["Chln_No"] = Convert.ToDouble(dNetAmnt).ToString("N2");
                                Drr["Gr_Date"] = Convert.ToDouble(dAdvAmnt).ToString("N2");
                                Drr["FromCity"] = Convert.ToDouble(dCommAmnt).ToString("N2");
                                Drr["ToCity"] = Convert.ToDouble(dTDSAmnt).ToString("N2");
                                Drr["Net_Amnt"] = (dNetAmnt - (dAdvAmnt + dCommAmnt + dTDSAmnt)).ToString("N2");
                                DtChild.Rows.Add(Drr);
                            }
                        }

                    }

                    #region Summary of Trips

                    if (i == list.Rows.Count - 1)
                    {
                        DataRow DrT = DtChild.NewRow();
                        DrT["Acnt_Name"] = Convert.ToString("Trip Total");
                        DrT["Chln_No"] = "Advance";
                        DrT["Gr_Date"] = "Fuel";
                        DrT["FromCity"] = "Toll";
                        DrT["ToCity"] = "Expense";
                        DrT["Net_Amnt"] = "Balance";
                        DtChild.Rows.Add(DrT);

                        DataTable DtTrip = obj.SelectTripDetl(Convert.ToInt64(list.Rows[i]["Trip_Idno"]), conString);
                        DataRow DrrT = DtChild.NewRow();
                        if (DtTrip != null && DtTrip.Rows.Count > 0)
                        {
                            DrrT["Chln_No"] = Convert.ToDouble(DtTrip.Rows[0]["InseAmnt"]).ToString("N2");
                            DrrT["Gr_Date"] = Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]).ToString("N2");
                            DrrT["FromCity"] = Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]).ToString("N2");
                            DrrT["ToCity"] = Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"]).ToString("N2");
                            FTripNetTot += (Convert.ToDouble(DtChild.Rows[DtChild.Rows.Count - 2]["Net_Amnt"])  - (Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"])));
                            DrrT["Net_Amnt"] = (Convert.ToDouble(DtChild.Rows[DtChild.Rows.Count - 2]["Net_Amnt"]) - (Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]) +Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"]))).ToString("N2");
                        }
                        DtChild.Rows.Add(DrrT);
                        continue;
                    }

                    if (PrvTripId != Convert.ToInt32(list.Rows[i + 1]["Trip_Idno"]))
                    {
                        DataRow DrT1 = DtChild.NewRow();
                        DrT1["Acnt_Name"] = Convert.ToString("Trip Total");
                        DrT1["Chln_No"] = "Advance";
                        DrT1["Gr_Date"] = "Fuel";
                        DrT1["FromCity"] = "Toll";
                        DrT1["ToCity"] = "Expense";
                        DrT1["Net_Amnt"] = "Balance";
                        DtChild.Rows.Add(DrT1);

                        DataTable DtTrip = obj.SelectTripDetl(Convert.ToInt64(list.Rows[i]["Trip_Idno"]), conString);
                        DataRow DrrT1 = DtChild.NewRow();
                        if (DtTrip != null && DtTrip.Rows.Count > 0)
                        {
                            DrrT1["Chln_No"] = Convert.ToDouble(DtTrip.Rows[0]["InseAmnt"]).ToString("N2");
                            DrrT1["Gr_Date"] = Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]).ToString("N2");
                            DrrT1["FromCity"] = Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]).ToString("N2");
                            DrrT1["ToCity"] = Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"]).ToString("N2");
                            FTripNetTot += (Convert.ToDouble(DtChild.Rows[DtChild.Rows.Count - 2]["Net_Amnt"])  - (Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"])));
                            DrrT1["Net_Amnt"] = (Convert.ToDouble(DtChild.Rows[DtChild.Rows.Count - 2]["Net_Amnt"]) - (Convert.ToDouble(DtTrip.Rows[0]["FuelAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["TollAmnt"]) + Convert.ToDouble(DtTrip.Rows[0]["ExpAmnt"]))).ToString("N2");
                            DtChild.Rows.Add(DrrT1);
                        }
                        PrvTripId = Convert.ToInt32(list.Rows[i + 1]["Trip_Idno"]);
                    }

                    #endregion
                }
                #region Purchase Detail
                if (listPur != null && listPur.Rows.Count > 0)
                {
                    int o = 0;
                    foreach (DataRow D in listPur.Rows)
                    {
                        if (o == 0)
                        {
                            DataRow Drr = DtChild.NewRow();
                            Drr["SrNo"] = Convert.ToString("Pur. Date");
                            Drr["Acnt_Name"] = Convert.ToString("Pur. No");
                            Drr["Gr_No"] = Convert.ToString("Serial No");
                            Drr["Chln_No"] = Convert.ToString("Tyre Comp.");
                            Drr["Gr_Date"] = Convert.ToString("Rate");
                            Drr["FromCity"] = Convert.ToString("Vchr. Party");
                            Drr["ToCity"] = Convert.ToString("Vchr. Detl");
                            Drr["Net_Amnt"] = Convert.ToString("Vchr. Amnt.");
                            DtChild.Rows.Add(Drr);
                            o++;
                        }
                        DataRow Dr = DtChild.NewRow();
                        Dr["SrNo"] = Convert.ToDateTime(D["PbillHead_Date"]).ToString("dd-MM-yyyy");
                        Dr["Acnt_Name"] = Convert.ToString(D["PBillHead_No"]);
                        Dr["Gr_No"] = Convert.ToString(D["SerialNo"]);
                        Dr["Chln_No"] = Convert.ToString(D["TyreType_Name"]);
                        Dr["Gr_Date"] = Convert.ToDouble(D["Item_Rate"]).ToString("N2");
                        DtChild.Rows.Add(Dr);
                    }
                    FPurNetTot += Convert.ToDouble(listPur.Compute("Sum(Item_Rate)", ""));
                }
                #endregion
                #region Vchr Details
                if (listVchr != null && listVchr.Rows.Count > 0)
                {
                    for (int i = 0; i < listVchr.Rows.Count; i++)
                    {
                        if (listPur.Rows.Count == 0 && i == 0)
                        {
                            DataRow Drr = DtChild.NewRow();
                            Drr["FromCity"] = Convert.ToString("Vchr. Party");
                            Drr["ToCity"] = Convert.ToString("Vchr. Detl");
                            Drr["Net_Amnt"] = Convert.ToString("Vchr. Amnt.");
                            DtChild.Rows.Add(Drr);
                            //if (i == 0)
                            //{
                            //    DataRow Drr1 = DtChild.NewRow();
                            //    DtChild.Rows.Add(Drr1);
                            //}
                        }

                        if (listPur.Rows.Count < listVchr.Rows.Count)
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < (listVchr.Rows.Count - listPur.Rows.Count); j++)
                                {
                                    DataRow Drr = DtChild.NewRow();
                                    DtChild.Rows.Add(Drr);
                                }
                            }
                        }
                        DtChild.Rows[((DtChild.Rows.Count - 1) - listVchr.Rows.Count) + i + 1]["FromCity"] = Convert.ToString(listVchr.Rows[i]["Acnt_Name"]);
                        DtChild.Rows[((DtChild.Rows.Count - 1) - listVchr.Rows.Count) + i + 1]["ToCity"] = Convert.ToString(listVchr.Rows[i]["vchrDetl"]);
                        DtChild.Rows[((DtChild.Rows.Count - 1) - listVchr.Rows.Count) + i + 1]["Net_Amnt"] = Convert.ToDouble(listVchr.Rows[i]["Acnt_Amnt"]).ToString("N2");

                    }
                    FVchrNetTot += Convert.ToDouble(listVchr.Compute("Sum(Acnt_Amnt)", ""));
                }
                #endregion
                #region Opening Calculation
                DataRow DTRow = DtChild.NewRow();
                DtChild.Rows.InsertAt(DTRow, 0);
                if (DtOpen != null && DtOpen.Rows.Count > 0)
                {
                    DtChild.Rows[0]["Acnt_Name"] = "Opening of Prev. Month";
                    DtChild.Rows[0]["Net_Amnt"] = Convert.ToString(DtOpen.Rows[0][0]);
                }
                #endregion
                #region Footer Calculation
                DataRow dtF1 = DtChild.NewRow();
                dtF1["Acnt_Name"] = "Purchase Total";
                dtF1["Gr_Date"] = FPurNetTot.ToString("N2");
                dtF1["FromCity"] = "Vchr. Total";
                dtF1["Net_Amnt"] = FVchrNetTot.ToString("N2");
                DtChild.Rows.Add(dtF1);

                DataRow dtF = DtChild.NewRow();
                dtF["Acnt_Name"] = "Trip Net Total";
                dtF["Gr_Date"] = FTripNetTot.ToString("N2");
                dtF["FromCity"] = "Month Opening";
                dtF["Net_Amnt"] = Convert.ToString(DtOpen.Rows[0][0]);
                DtChild.Rows.Add(dtF);

                DataRow dtF2 = DtChild.NewRow();
                dtF2["FromCity"] = "Net Balance";
                dtF2["Net_Amnt"] = ((FTripNetTot - FPurNetTot) - FVchrNetTot).ToString("N2");
                DtChild.Rows.Add(dtF2);
                #endregion
                //Removing Invoice Detail
                //int RowCount = 0;
                //for (int i = DtChild.Rows.Count - 1; i >= 0; i--)
                //{
                //    DataRow dr = DtChild.Rows[i];
                //    if (Convert.ToString(dr["Acnt_Name"]) == "Challan Total")
                //    {
                //        RowID = i;
                //        dr.Delete();
                //        break;
                //    }
                //}
               // DtChild.Rows[RowID].Delete();
                DtChild.AcceptChanges();
                GridView1.DataSource = DtChild;
                ViewState["Dt"] = DtChild;
                GridView1.DataBind();
                imgBtnExcel.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                //  printRep.Visible = false;
                imgBtnExcel.Visible = false;

            }
            //}
            //catch (Exception Ex)
            //{

            //}

        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TripSheet.xls"));
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

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "cmdedit")
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "TestArrayScript", "ShowClient()", true);

            //}
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acnt_Name")) == "Challan Total")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#BDDBED");
                    e.Row.Font.Bold = true;
                    Is = 1;
                    Is2 = 1;
                }
                else if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acnt_Name")) == "Trip Total")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#60B1E0");
                    e.Row.Font.Bold = true;
                    Is = 1;
                    Is2 = 2;
                }
                else if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FromCity")) == "Net Balance")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#191547");
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = Color.White;

                }
                else if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SrNo")) == "Pur. Date" || Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FromCity")) == "Vchr. Party")
                {
                    e.Row.Cells[0].BackColor = ColorTranslator.FromHtml("#102AED");
                    e.Row.Cells[1].BackColor = ColorTranslator.FromHtml("#102AED");
                    e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#102AED");
                    e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#102AED");
                    e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#102AED");
                    e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#0A009B");
                    e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#0A009B");
                    e.Row.Cells[7].BackColor = ColorTranslator.FromHtml("#0A009B");
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = Color.White;
                }
                else if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acnt_Name")) == "Purchase Total" || Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Acnt_Name")) == "Trip Net Total")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#191547");
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = Color.White;
                }
                else
                {
                    if (Is == 1)
                    {
                        if (Is2 == 1)
                        {
                            e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#BDDBED");
                            e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#BDDBED");
                            e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#BDDBED");
                            e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#BDDBED");
                            e.Row.Cells[7].BackColor = ColorTranslator.FromHtml("#BDDBED");
                        }
                        else
                        {
                            e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#60B1E0");
                            e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#60B1E0");
                            e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#60B1E0");
                            e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#60B1E0");
                            e.Row.Cells[7].BackColor = ColorTranslator.FromHtml("#60B1E0");
                        }
                        e.Row.Font.Bold = true;
                        Is = 0;
                        Is2 = 0;
                    }
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    NetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
            //    GrossAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Gross_Amnt"));
            //    IncetAmnt += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Insentive_Amnt"));
            //    ChlnTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Chln_NetAmnt"));
            //    FuelTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Fuel_Amnt"));
            //    ExpnTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Exp_Amnt"));
            //    TollTot += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Toll_Amnt"));
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblNetAmnt = (Label)e.Row.FindControl("lblNetAmnt");
            //    lblNetAmnt.Text = NetAmnt.ToString("N2");

            //    Label lblGrossAmnt = (Label)e.Row.FindControl("lblGrossAmnt");
            //    lblGrossAmnt.Text = GrossAmnt.ToString("N2");

            //    Label lblIncAmnt = (Label)e.Row.FindControl("lblIncAmnt");
            //    lblIncAmnt.Text = IncetAmnt.ToString("N2");

            //    Label lblChlnTot = (Label)e.Row.FindControl("lblChlnTot");
            //    lblChlnTot.Text = ChlnTot.ToString("N2");

            //    Label lblFuelTot = (Label)e.Row.FindControl("lblFuelTot");
            //    lblFuelTot.Text = FuelTot.ToString("N2");

            //    Label lblExpAmnt = (Label)e.Row.FindControl("lblExpAmnt");
            //    lblExpAmnt.Text = ExpnTot.ToString("N2");

            //    Label lblTollAmnt = (Label)e.Row.FindControl("lblTollAmnt");
            //    lblTollAmnt.Text = TollTot.ToString("N2");
            //}
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
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


            }
            else
            {


            }
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
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

        protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}