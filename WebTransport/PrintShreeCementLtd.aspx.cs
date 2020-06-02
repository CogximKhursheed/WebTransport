using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Private Variables...
        public int RASUn = 0; double TotalFright = 0, TotalWedges = 0, TotalLabour = 0, TotalQty = 0, TotalWeight = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalSHBages = 0;
        double RasTotalFright = 0, RasTotalWedges = 0, RasTotalLabour = 0, RasTotalQty = 0, RasTotalWeight = 0, RasTotal = 0, RasTotalSTax = 0, RasTotalSBTaxAmnt = 0, RasTotalKKTaxAmnt = 0, RasTotalSHBages = 0;
        double JobnerTotalFright = 0, JobnerTotalWedges = 0, JobnerTotalLabour = 0, JobnerTotalQty = 0, JobnerTotalWeight = 0, JobnerTotal = 0, JobnerTotalSTax = 0, JobnerTotalSBTaxAmnt = 0, JobnerTotalKKTaxAmnt = 0, JobnerTotalSHBages = 0;
        #endregion

        #region Form Evnets...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]) != true && string.IsNullOrEmpty(Request.QueryString["P"]) != true && string.IsNullOrEmpty(Request.QueryString["R"]) != true)
                {
                    string PrintFormat = Request.QueryString["P"].ToString();
                    if (Request.QueryString["R"] == "2")
                    {
                        header.Visible = false;
                        Header1.Visible = false;
                        Header2.Visible = false;
                    }
                    else
                    {
                        header.Visible = true;
                        Header1.Visible = true;
                        Header2.Visible = true;
                    }
                    try
                    {
                        if (Convert.ToInt32(PrintFormat) == 1)          //  Beawar
                        {
                            printShreeCementLtdBeawer.Visible = true;
                            PrintInvoiceBeawer(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                            PrintHelper.PrintWebControl(printShreeCementLtdBeawer);
                        }
                        else if (Convert.ToInt32(PrintFormat) == 2)     //  RAS Basic
                        {
                            //Puneet Upadhyay
                            RASUn = 0;
                            printShreeCementLtdRas.Visible = true;
                            PrintInvoiceRas(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                            PrintHelper.PrintWebControl(printShreeCementLtdRas);
                        }

                        //Puneet Upadhyay
                        else if (Convert.ToInt32(PrintFormat) == 11)    //  RAS Unloading
                        {
                            RASUn = 1;
                            printShreeCementLtdRas.Visible = true;
                            PrintInvoiceRas(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                            PrintHelper.PrintWebControl(printShreeCementLtdRas);
                        }
                        else if (Convert.ToInt32(PrintFormat) == 7)     //  JOBNER
                        {
                            printShreeCementLtdJobner.Visible = true;
                            PrintInvoiceJobner(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                            PrintHelper.PrintWebControl(printShreeCementLtdJobner);
                        }
                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }
        }
        #endregion

        #region Print ShreeCement Limited [Beawar]...
        private void PrintInvoiceBeawer(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty; string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty; string ServTaxNo = string.Empty;
            DataSet CompDetl = null;
            CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..

            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"])) == false)
            {
                lblServTaxNo.Visible = true;
                ServTaxNo = "Serv Tax No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            }
            else
            {
                lblServTaxNo.Visible = false;
            }
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = "Pan No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            lblSignCompName.Text = CompName.ToString();
            lblJurCity.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            if (string.IsNullOrEmpty(PhNo) == false) { lblOwnerPhoneNo.Text = PhNo.ToUpper(); } else { lblOwnerPhoneNo.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblCompName.Text = CompName.ToUpper(); } else { lblCompName.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblCompAddress1.Text = Add1.ToUpper(); } else { lblCompAddress1.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblCompAddress2.Text = Add2.ToUpper(); } else { lblCompAddress2.Visible = false; }

            #endregion
            DataSet PartyDetails = null;
            PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(A.Pan_No,0) Pan_No,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                lblPartyName.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblPartyAddress1.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) == false && Convert.ToInt32(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) > 0) { lblPartyPinCode.Text = " - " + PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString(); } else { lblPartyPinCode.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString()) == false) { lblPartyPanNo.Text = "Pan No : " + PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString(); } else { lblPartyPanNo.Visible = false; }
            }
            lblCompPanNo.Text = PanNo;
            lblCode1.Text = CodeNo;
            lblServTaxNo.Text = ServTaxNo;

            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintGCA',@Id='" + HeadIdno + "',@PrintFormat='" + PrintFormat + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                lblBillNo.Text = "Bill No : " + dsReport.Tables[1].Rows[0]["Inv_No"].ToString();
                lblBillDate.Text = "Bill Date : " + Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");
                Repeater1.DataSource = dsReport.Tables[0];
                Repeater1.DataBind();
                string numbertoent = "RUPEES : " + NumberToText(Convert.ToInt32(Total));
                lblRupeesWord.Text = numbertoent.ToUpper() + " ONLY.";
                lblDivServTaxTotal.Text = TotalSTax.ToString("N2");
                lblDivSwachBhTotal.Text = TotalSBTaxAmnt.ToString("N2");
                lblDivKissanTaxTotal.Text = TotalKKTaxAmnt.ToString("N2");
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;

                TotalWedges += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                TotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Freight"));
                //TotalLabour += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Labour"));
                TotalWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                TotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Dsp_Qty"));
                Total += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotalFrt"));
                TotalSTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ServTax_Amnt"));
                TotalSBTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                TotalKKTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanKalyan_Amnt"));
                TotalSHBages += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage_Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotalFreight = e.Item.FindControl("lblTotalFreight") as Label;
                Label lblTotalUnloading = e.Item.FindControl("lblTotalUnloading") as Label;
                Label lblTotalQty = e.Item.FindControl("lblTotalQty") as Label;
                Label lblTotalWeight = e.Item.FindControl("lblTotalWeight") as Label;
                //Label lblTotalLabour = e.Item.FindControl("lblTotalLabour") as Label;
                Label lblTotalFrt = e.Item.FindControl("lblTotalFrt") as Label;
                Label lblTotalSTaxAmnt = e.Item.FindControl("lblTotalSTaxAmnt") as Label;
                Label lblTotalSBTaxAmnt = e.Item.FindControl("lblTotalSBTaxAmnt") as Label;
                Label lblTotalKKAmnt = e.Item.FindControl("lblTotalKKAmnt") as Label;
                Label lblTotalShBags = e.Item.FindControl("lblTotalShBags") as Label;



                lblTotalFreight.Text = TotalFright.ToString("N2");
                lblTotalUnloading.Text = TotalWedges.ToString("N2");
                lblTotalQty.Text = TotalQty.ToString();
                lblTotalWeight.Text = TotalWeight.ToString("N2");
                //lblTotalLabour.Text = TotalLabour.ToString("N2");
                lblTotalFrt.Text = Total.ToString("N2");
                lblTotalSTaxAmnt.Text = TotalSTax.ToString("N2");
                lblTotalSBTaxAmnt.Text = TotalSBTaxAmnt.ToString("N2");
                lblTotalKKAmnt.Text = TotalKKTaxAmnt.ToString("N2");
                lblTotalShBags.Text = TotalSHBages.ToString("N2");
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                UserPrefenceDAL obj = new UserPrefenceDAL();
                tblUserPref Pref = obj.SelectById();
                Label lblHeadWages = e.Item.FindControl("lblHeadWages") as Label;
                if (string.IsNullOrEmpty(Pref.WagesLabel_Print))
                {
                    lblHeadWages.Text = "Wages";
                }
                else
                {
                    lblHeadWages.Text = Pref.WagesLabel_Print;
                }
            }

        }
        #endregion

        #region Print ShreeCement Limited [RAS]...
        private void PrintInvoiceRas(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty; string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty; string ServTaxNo = string.Empty;
            DataSet CompDetl = null;
            CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..

            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"])) == false)
            {
                lblServTaxNo.Visible = true;
                ServTaxNo = "Serv Tax No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            }
            else
            {
                lblServTaxNo.Visible = false;
            }
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = "Pan No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            lblRasSignCompName.Text = CompName.ToString();
            lblRasJurCity.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            if (string.IsNullOrEmpty(PhNo) == false) { lblRasOwnerPhoneNo.Text = PhNo.ToUpper(); } else { lblRasOwnerPhoneNo.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblRasCompName.Text = CompName.ToUpper(); } else { lblRasCompName.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblRasCompAddress1.Text = Add1.ToUpper(); } else { lblRasCompAddress1.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblRasCompAddress2.Text = Add2.ToUpper(); } else { lblRasCompAddress2.Visible = false; }

            #endregion
            DataSet PartyDetails = null;
            PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(A.Pan_No,0) Pan_No,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                lblRasPartyName.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblRasPartyAddress1.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblRasPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblRasPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblRasPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblRasPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) == false && Convert.ToInt32(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) > 0) { lblRasPartyPinCode.Text = " - " + PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString(); } else { lblRasPartyPinCode.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString()) == false) { lblRasPartyPanNo.Text = "Pan No : " + PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString(); } else { lblRasPartyPanNo.Visible = false; }
            }
            lblRasCompPanNo.Text = PanNo;
            lblRasCode1.Text = CodeNo;
            lblRasServTaxNo.Text = ServTaxNo;


            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintGCA',@Id='" + HeadIdno + "',@PrintFormat='" + PrintFormat + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                lblRasBillNo.Text = "Bill No : " + dsReport.Tables[1].Rows[0]["Inv_No"].ToString();
                lblRasBillDate.Text = "Bill Date : " + Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");

                Repeater2.DataSource = dsReport.Tables[0];
                Repeater2.DataBind();
                string numbertoent = NumberToText(Convert.ToInt32(RasTotal));
                lblRasRupeesWord.Text = "RUPEES : " + numbertoent.ToUpper() + " ONLY.";
                lblRasDivServTaxTotal.Text = RasTotalSTax.ToString("N2");
                lblRasDivSwachBhTotal.Text = RasTotalSBTaxAmnt.ToString("N2");
                lblRasDivKissanTaxTotal.Text = RasTotalKKTaxAmnt.ToString("N2");
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                RasTotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Freight"));
                RasTotalWedges += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                //RasTotalLabour += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Labour"));
                RasTotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Dsp_Qty"));
                RasTotalWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                RasTotal += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotalFrt"));
                RasTotalSTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ServTax_Amnt"));
                RasTotalSBTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                RasTotalKKTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanKalyan_Amnt"));
                RasTotalSHBages += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage_Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblRasTotalFreight = e.Item.FindControl("lblRasTotalFreight") as Label;
                //Label lblRasUnloading = e.Item.FindControl("lblRasUnloading") as Label;
                Label lblRasTotalQty = e.Item.FindControl("lblRasTotalQty") as Label;
                //Label lblRasTotalWeight = e.Item.FindControl("lblRasTotalWeight") as Label;
                Label lblRasTotalLabour = e.Item.FindControl("lblRasTotalLabour") as Label;
                Label lblRasTotalFrt = e.Item.FindControl("lblRasTotalFrt") as Label;
                Label lblRasTotalSTaxAmnt = e.Item.FindControl("lblRasTotalSTaxAmnt") as Label;
                Label lblRasTotalSBTaxAmnt = e.Item.FindControl("lblRasTotalSBTaxAmnt") as Label;
                Label lblRasTotalKKAmnt = e.Item.FindControl("lblRasTotalKKAmnt") as Label;
                Label lblRasTotalShBags = e.Item.FindControl("lblRasTotalShBags") as Label;

                //lblRasUnloading.Text = RasTotalWedges.ToString("N2");
                lblRasTotalFreight.Text = RasTotalFright.ToString("N2");
                lblRasTotalQty.Text = RasTotalQty.ToString();
                //lblRasTotalWeight.Text = RasTotalWeight.ToString("N2");
                lblRasTotalLabour.Text = RasTotalLabour.ToString("N2");
                lblRasTotalFrt.Text = RasTotal.ToString("N2");
                lblRasTotalSTaxAmnt.Text = RasTotalSTax.ToString("N2");
                lblRasTotalSBTaxAmnt.Text = RasTotalSBTaxAmnt.ToString("N2");
                lblRasTotalKKAmnt.Text = RasTotalKKTaxAmnt.ToString("N2");
                lblRasTotalShBags.Text = RasTotalSHBages.ToString("N2");
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                UserPrefenceDAL obj = new UserPrefenceDAL();
                tblUserPref Pref = obj.SelectById();
                //Label lblRasHeadWages = e.Item.FindControl("lblRasHeadWages") as Label;
                //if (string.IsNullOrEmpty(Pref.WagesLabel_Print))
                //{
                //    lblRasHeadWages.Text = "Wages";
                //}
                //else
                //{
                //    lblRasHeadWages.Text = Pref.WagesLabel_Print;
                //}
            }
        }
        #endregion

        #region Print ShreeCement Limited [JOBNER]...
        private void PrintInvoiceJobner(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty; string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty; string ServTaxNo = string.Empty;
            DataSet CompDetl = null;
            CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..

            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"])) == false)
            {
                lblServTaxNo.Visible = true;
                ServTaxNo = "Serv Tax No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            }
            else
            {
                lblServTaxNo.Visible = false;
            }
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = "Pan No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);

            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            lblJobnerSignCompName.Text = CompName.ToString();
            lblJobnerJurCity.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            if (string.IsNullOrEmpty(PhNo) == false) { lblJobnerOwnerPhoneNo.Text = PhNo.ToUpper(); } else { lblJobnerOwnerPhoneNo.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblJobnerCompName.Text = CompName.ToUpper(); } else { lblJobnerCompName.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblJobnerCompAddress1.Text = Add1.ToUpper(); } else { lblJobnerCompAddress1.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblJobnerCompAddress2.Text = Add2.ToUpper(); } else { lblJobnerCompAddress2.Visible = false; }

            #endregion
            DataSet PartyDetails = null;
            PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(A.Pan_No,0) Pan_No,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                lblJobnerPartyName.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblJobnerPartyAddress1.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblJobnerPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblJobnerPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblJobnerPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblJobnerPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) == false && Convert.ToInt32(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) > 0) { lblJobnerPartyPinCode.Text = " - " + PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString(); } else { lblJobnerPartyPinCode.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString()) == false) { lblJobnerPartyPanNo.Text = "Pan No : " + PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString(); } else { lblJobnerPartyPanNo.Visible = false; }
            }
            lblJobnerCompPanNo.Text = PanNo;
            lblJobnerCode1.Text = CodeNo;
            lblJobnerServTaxNo.Text = ServTaxNo;

            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintGCA',@Id='" + HeadIdno + "',@PrintFormat='" + PrintFormat + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                lblJobnerBillNo.Text = "Bill No : " + dsReport.Tables[1].Rows[0]["Inv_No"].ToString();
                lblJobnerBillDate.Text = "Bill Date : " + Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");

                Repeater3.DataSource = dsReport.Tables[0];
                Repeater3.DataBind();
                string numbertoent = NumberToText(Convert.ToInt32(JobnerTotal));
                lblJobnerRupeesWord.Text = "RUPEES : " + numbertoent.ToUpper() + " ONLY.";
                lblJobnerDivServTaxTotal.Text = JobnerTotalSTax.ToString("N2");
                lblJobnerDivSwachBhTotal.Text = JobnerTotalSBTaxAmnt.ToString("N2");
                lblJobnerDivKissanTaxTotal.Text = JobnerTotalKKTaxAmnt.ToString("N2");
            }
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                JobnerTotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Freight"));
                JobnerTotalWedges += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                //JobnerTotalLabour += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Labour"));
                JobnerTotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Dsp_Qty"));
                JobnerTotalWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                JobnerTotal += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotalFrt"));
                JobnerTotalSTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ServTax_Amnt"));
                JobnerTotalSBTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                JobnerTotalKKTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanKalyan_Amnt"));
                JobnerTotalSHBages += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage_Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblJobnerTotalFreight = e.Item.FindControl("lblJobnerTotalFreight") as Label;
                Label lblJobnerUnloading = e.Item.FindControl("lblJobnerUnloading") as Label;
                //Label lblJobnerTotalQty = e.Item.FindControl("lblJobnerTotalQty") as Label;
                Label lblJobnerTotalWeight = e.Item.FindControl("lblJobnerTotalWeight") as Label;
                //Label lblJobnerTotalLabour = e.Item.FindControl("lblJobnerTotalLabour") as Label;
                Label lblJobnerTotalFrt = e.Item.FindControl("lblJobnerTotalFrt") as Label;
                Label lblJobnerTotalSTaxAmnt = e.Item.FindControl("lblJobnerTotalSTaxAmnt") as Label;
                Label lblJobnerTotalSBTaxAmnt = e.Item.FindControl("lblJobnerTotalSBTaxAmnt") as Label;
                Label lblJobnerTotalKKAmnt = e.Item.FindControl("lblJobnerTotalKKAmnt") as Label;
                Label lblJobnerTotalShBags = e.Item.FindControl("lblJobnerTotalShBags") as Label;

                lblJobnerUnloading.Text = JobnerTotalWedges.ToString("N2");
                lblJobnerTotalFreight.Text = JobnerTotalFright.ToString("N2");
                //lblJobnerTotalQty.Text = JobnerTotalQty.ToString();
                lblJobnerTotalWeight.Text = JobnerTotalWeight.ToString("N2");
                //lblJobnerTotalLabour.Text = JobnerTotalLabour.ToString("N2");
                lblJobnerTotalFrt.Text = JobnerTotal.ToString("N2");
                lblJobnerTotalSTaxAmnt.Text = JobnerTotalSTax.ToString("N2");
                lblJobnerTotalSBTaxAmnt.Text = JobnerTotalSBTaxAmnt.ToString("N2");
                lblJobnerTotalKKAmnt.Text = JobnerTotalKKTaxAmnt.ToString("N2");
                lblJobnerTotalShBags.Text = JobnerTotalSHBages.ToString("N2");
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                UserPrefenceDAL obj = new UserPrefenceDAL();
                tblUserPref Pref = obj.SelectById();
                Label lblJobnerHeadWages = e.Item.FindControl("lblJobnerHeadWages") as Label;
                if (string.IsNullOrEmpty(Pref.WagesLabel_Print))
                {
                    lblJobnerHeadWages.Text = "Wages";
                }
                else
                {
                    lblJobnerHeadWages.Text = Pref.WagesLabel_Print;
                }
            }
        }
        #endregion

        #region Functions...
        public string NumberToText(int number)
        {
            if (number == 0) return "Zero";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        #endregion
    }
}