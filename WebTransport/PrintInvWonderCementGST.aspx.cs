using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Drawing;


namespace WebTransport
{
    public partial class PrintInvWonderCementGST : System.Web.UI.Page
    {
        #region Private Variables...
        public List<GSTTotalValues> LstGst = new List<GSTTotalValues>();
        public int RASUn = 0; double TotalFright = 0, TotalWedges = 0, TotalLabour = 0, TotalQty = 0, TotalWeight = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalSHBages = 0;
        double TotalUnLoading = 0; double TotalLoading = 0; double RasTotalFright = 0, RasTotalWedges = 0, RasTotalLabour = 0, RasTotalQty = 0, RasTotalWeight = 0, RasTotal = 0, RasTotalSTax = 0, RasTotalSBTaxAmnt = 0, RasTotalKKTaxAmnt = 0, RasTotalSHBages = 0;
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
                       
                    }
                    else
                    {
                        header.Visible = true;
                       
                    }
                    try
                    {
                        
                        printShreeCementLtdBeawer.Visible = true;
                        PrintMangrolGST(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                        PrintHelper.PrintWebControl(printShreeCementLtdBeawer);
                       
                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }
        }
        #endregion

        #region Print Wonder Cement [Nimbahera GST]...
        private void PrintMangrolGST(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty; string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty; string ServTaxNo = string.Empty;
            DataSet CompDetl = null;
            CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..
            if (CompDetl.Tables[0].Rows[0]["CompGSTIN_No"] != null && CompDetl.Tables[0].Rows[0]["CompGSTIN_No"].ToString() != "")
                lblGSTINComp.Text = "GSTIN No.: " + Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"] == null ? "" : CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            else lblGSTINComp.Visible = false;
           
            if (CompDetl.Tables[0].Rows[0]["PStatus"] != null && CompDetl.Tables[0].Rows[0]["PStatus"].ToString() != "")
                lblStatus1.Text = "Status: " + Convert.ToString(CompDetl.Tables[0].Rows[0]["PStatus"]);
            else lblStatus1.Visible = false;
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
            lblJurCity.Text = "All Subjects To " + City.ToUpper() + " Jurisdiction";
            if (string.IsNullOrEmpty(PhNo) == false) { lblOwnerPhoneNo.Text = PhNo.ToUpper(); } else { lblOwnerPhoneNo.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblCompName.Text = CompName.ToUpper(); } else { lblCompName.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblCompAddress1.Text = Add1.ToUpper(); } else { lblCompAddress1.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblCompAddress2.Text = Add2.ToUpper(); } else { lblCompAddress2.Visible = false; }

            #endregion
            DataSet PartyDetails = null;
            PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.LdgrGSTIN_No,'') AS SenderGSTIN, ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(A.Pan_No,0) Pan_No,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                if (PartyDetails.Tables[0].Rows[0]["SenderGSTIN"] != null && PartyDetails.Tables[0].Rows[0]["SenderGSTIN"].ToString() != null)
                lblGSTINSender.Text = "GSTIN No.:" + PartyDetails.Tables[0].Rows[0]["SenderGSTIN"].ToString();
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
            if (Request.QueryString["S"] != null && Request.QueryString["S"] == "GR")
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintJKGrGST',@Id='" + HeadIdno + "'");
            }
            else if (Request.QueryString["S"] != null && Request.QueryString["S"] == "GRR")
            {
                dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintJKGrrGST',@Id='" + HeadIdno + "'");
            }
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                GetState((dsReport.Tables[0].Rows[0]["State_Name"] == null && dsReport.Tables[0].Rows[0]["State_Name"].ToString() == "") ? "" : dsReport.Tables[0].Rows[0]["State_Name"].ToString());
                GetCityDetails((dsReport.Tables[0].Rows[0]["City_Name"] == null && dsReport.Tables[0].Rows[0]["City_Name"].ToString() == "") ? "" : dsReport.Tables[0].Rows[0]["City_Name"].ToString());
                string invDate = dsReport.Tables[0].Rows[0]["Inv_Date"].ToString();
                GetGSTType(invDate);
                lblBillNo.Text = "Bill No : " + (dsReport.Tables[0].Rows[0]["Inv_prefix"] == null ? "" : dsReport.Tables[0].Rows[0]["Inv_prefix"].ToString()) + (dsReport.Tables[0].Rows[0]["Inv_No"] == null ? "" : dsReport.Tables[0].Rows[0]["Inv_No"].ToString());
                lblBillDate.Text = "Bill Date : " + Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                string numbertoent = "RUPEES IN WORDS: " + NumberToText(Convert.ToInt32(Total));
                lblRupeesWord.Text = numbertoent.ToUpper() + " ONLY.";
                //lblDivServTaxTotal.Text = TotalSTax.ToString("N2");
                //lblDivSwachBhTotal.Text = TotalSBTaxAmnt.ToString("N2");
                //lblDivKissanTaxTotal.Text = TotalKKTaxAmnt.ToString("N2");
            }

            if (dsReport.Tables[3] != null)
            {
                if (dsReport.Tables[3].Rows.Count > 0)
                {
                    int rows = dsReport.Tables[3].Rows.Count;
                    GSTTotalValues gst = new GSTTotalValues();
                    for(int r = 0; r<= rows - 1;r++)
                    {
                        gst.SGSTAmt = Convert.ToDouble(dsReport.Tables[3].Rows[r][0]);
                        gst.CGSTAmt = Convert.ToDouble(dsReport.Tables[3].Rows[r][1]);
                        gst.IGSTAmt = Convert.ToDouble(dsReport.Tables[3].Rows[r][2]);
                        gst.SGSTPer = Convert.ToDouble(dsReport.Tables[3].Rows[r][3]);
                        gst.CGSTPer = Convert.ToDouble(dsReport.Tables[3].Rows[r][4]);
                        gst.IGSTPer = Convert.ToDouble(dsReport.Tables[3].Rows[r][5]);
                        LstGst.Add(gst);
                    }
                }
            }
        }

        private void GetState(string StateName)
        {
            if (StateName != "")
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var stateDetl = db.tblStateMasters.Where(x => x.State_Name == StateName).FirstOrDefault();
                    if (stateDetl != null)
                    {
                        lblSupplyStateName.Text = "Place of supply: " + stateDetl.State_Name;
                        lblSupplyStateId.Text = "State Code: " + stateDetl.State_Idno.ToString();
                    }
                }
            }
        }
        private void GetCityDetails(string CityName)
        {
            if (CityName != "")
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var cityDetl = db.tblCityMasters.Where(x => x.City_Name == CityName).FirstOrDefault();
                    if (cityDetl != null)
                    {
                        if (cityDetl.Code_sap != null && cityDetl.Code_sap != "")
                            lblCodeSap.Text = "Code-Sap: " + Convert.ToString(cityDetl.Code_sap);
                        else lblCodeSap.Visible = false;
                        if (cityDetl.sac_Code != null && cityDetl.Code_sap != "")
                            lblSacCode.Text = "SAC Code: " + Convert.ToString(cityDetl.sac_Code);
                        else lblCodeSap.Visible = false;
                    }
                }
            }
        }

        public class GSTTotalValues
        {
            public double SGSTPer { get; set; }
            public double CGSTPer { get; set; }
            public double IGSTPer { get; set; }
            public double SGSTAmt { get; set; }
            public double CGSTAmt { get; set; }
            public double IGSTAmt { get; set; }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            hidIsGST.Value = "1";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get Values based on GST Date
                Panel pnlVATContent = e.Item.FindControl("pnlVATContent") as Panel;
                Panel pnlGSTContent = e.Item.FindControl("pnlGSTContent") as Panel;
                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVATContent != null) pnlVATContent.Visible = true;
                        if (pnlGSTContent != null) pnlGSTContent.Visible = false;
                    }
                    else
                    {
                        if (pnlVATContent != null) pnlVATContent.Visible = false;
                        if (pnlGSTContent != null) pnlGSTContent.Visible = true;
                    }
                }
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;

                TotalLoading += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Loaded"));
                TotalUnLoading += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "UnLoaded"));
                TotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "amount"));
                //TotalLabour += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Labour"));
                //TotalWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                TotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                Total += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "amount"));
                //TotalSTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ServTax_Amnt"));
                //TotalSBTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                //TotalKKTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanKalyan_Amnt"));
                TotalSHBages += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //Get Values based on GST Date
                Panel pnlVATFooter = e.Item.FindControl("pnlVATFooter") as Panel;
                Panel pnlGSTFooter = e.Item.FindControl("pnlGSTFooter") as Panel;
                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVATFooter != null) pnlVATFooter.Visible = true;
                        if (pnlGSTFooter != null) pnlGSTFooter.Visible = false;
                    }
                    else
                    {
                        if (pnlVATFooter != null) pnlVATFooter.Visible = false;
                        if (pnlGSTFooter != null) pnlGSTFooter.Visible = true;
                    }
                }

                Label lblTotalFreight = e.Item.FindControl("lblTotalFreight") as Label;
                Label lblTotalUnloading = e.Item.FindControl("lblTotalUnloading") as Label;
                Label lblTotalloading = e.Item.FindControl("lblTotalloading") as Label;
                Label lblTotalQty = e.Item.FindControl("lblTotalQty") as Label;
                Label lblTotalWeight = e.Item.FindControl("lblTotalWeight") as Label;
                //Label lblTotalLabour = e.Item.FindControl("lblTotalLabour") as Label;
                Label lblTotalFrt = e.Item.FindControl("lblTotalFrt") as Label;
                Label lblTotalSTaxAmnt = e.Item.FindControl("lblTotalSTaxAmnt") as Label;
                Label lblTotalSBTaxAmnt = e.Item.FindControl("lblTotalSBTaxAmnt") as Label;
                Label lblTotalKKAmnt = e.Item.FindControl("lblTotalKKAmnt") as Label;
                Label lblTotalShBags = e.Item.FindControl("lblTotalShBags") as Label;


                lblTotalloading.Text = TotalLoading.ToString("N2");
                lblTotalUnloading.Text = TotalUnLoading.ToString("N2");
                lblTotalFreight.Text = TotalFright.ToString("N2");
                lblTotalQty.Text = TotalQty.ToString();
                lblTotalWeight.Text = TotalWeight.ToString("N2");
                //lblTotalLabour.Text = TotalLabour.ToString("N2");
                //lblTotalFrt.Text = Total.ToString("N2");
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
                //Get Values based on GST Date
                Panel pnlVATHeader = e.Item.FindControl("pnlVATHeader") as Panel;
                Panel pnlGSTHeader = e.Item.FindControl("pnlGSTHeader") as Panel;
                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVATHeader != null) pnlVATHeader.Visible = true;
                        if (pnlGSTHeader != null) pnlGSTHeader.Visible = false;
                    }
                    else
                    {
                        if (pnlVATHeader != null) pnlVATHeader.Visible = false;
                        if (pnlGSTHeader != null) pnlGSTHeader.Visible = true;
                    }
                }
            }

        }
        #endregion


        #region Functions...

        //Upadhyay #GST
        public void GetGSTType(string date)
        {
            if (date != "")
            {
                date = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
                string dt = GetGSTDate();
                if ((Convert.ToString(dt) != "") && (Convert.ToDateTime(ApplicationFunction.mmddyyyy(date.Trim().ToString())) >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(dt))))
                {
                    hidIsGST.Value = "1";
                }
                else
                {
                    hidIsGST.Value = "0";
                }
            }
        }

        //Upadhyay #GST
        private string GetGSTDate()
        {
            DateTime gstDate;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                gstDate = (from i in db.tblUserPrefs select i.GST_Date).FirstOrDefault();
                return gstDate.ToString("dd/MM/yyyy");
            }
        }

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