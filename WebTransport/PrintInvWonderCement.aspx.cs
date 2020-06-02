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

namespace WebTransport
{
    public partial class PrintInvWonderCement : System.Web.UI.Page
    {
        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalInvAmount = 0, TotalInvWeight=0,TotQty,TotShortQty=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]) != true && string.IsNullOrEmpty(Request.QueryString["P"]) != true && string.IsNullOrEmpty(Request.QueryString["R"]) != true )
                {
                    try
                    {
                        if (Request.QueryString["R"] == "2")
                        {
                            header.Visible = false;
                            Header1.Visible = false;
                            lblJurCity.Visible = false;
                            lblCompPhNo.Visible = false;
                            lblJurCity1.Visible = false;
                            lblCompPhNo1.Visible = false;
                        }
                        else
                        {
                            header.Visible = true;
                            Header1.Visible = true;
                            lblJurCity.Visible = true;
                            lblCompPhNo.Visible = true;
                            lblJurCity1.Visible = true;
                            lblCompPhNo1.Visible = true;
                        }
                        PrintInvoiceHead(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                        //PrintHelper.PrintWebControl(printNimbaheraHead);
                        PrintInvoiceDetl(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                        PrintHelper.PrintWebControl(printNimbaheraDetl);
                        
                        //printNimbaheraHead.Visible = true;
                        printNimbaheraDetl.Visible = true;
                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }

        }

        private void PrintInvoiceHead(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty, cityHeader = string.Empty, stateHeader = string.Empty; 
            string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; 
            string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty;
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..

            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            cityHeader = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            stateHeader = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = "Pan No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            lblCompname.Text = "For  " + CompName.ToString();
            lblJurCity.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])) == false) { lblCompPhNo.Text = PhNo.ToUpper(); } else { lblCompPhNo.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblCompanyname.Text = CompName.ToUpper(); } else { lblCompanyname.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblCompAdd1.Text = Add1.ToUpper(); } else { lblCompAdd1.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblCompAdd2.Text = Add2.ToUpper(); } else { lblCompAdd2.Visible = false; }
            if (string.IsNullOrEmpty(City) == false) { lblCompCity.Text = City.ToUpper(); } else { lblCompCity.Visible = false; }
            if (string.IsNullOrEmpty(State) == false) { lblCompState.Text = State.ToUpper(); } else { lblCompState.Visible = false; }
            #endregion
            DataSet PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                lblSenderName.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblsenderaddress.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblsenderaddress.Text = lblsenderaddress.Text + PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else {}
            }
            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintWonderCementHead',@Id='" + HeadIdno + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicveno.Text = dsReport.Tables[1].Rows[0]["Inv_No"].ToString();
                valuelblinvoicedate.Text =  Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");
                lblPanNo.Text = PanNo;
                //lblCode1.Text = CodeNo;
                Repeater1.DataSource = dsReport.Tables[0];
                Repeater1.DataBind();
                string numbertoent = NumberToText(Convert.ToInt32(TotalInvAmount));
                lblTowords.Text = numbertoent.ToUpper();
                lbltotalWeight.Text = TotalInvWeight.ToString("N2");
                lblTotalAmnt.Text = TotalInvAmount.ToString("N2");
                lblTotalShort.Text = TotShortQty.ToString("N2");
                lblTotGr.Text = TotQty.ToString("N2");

                lblTotFreightAmount.Text = TotalInvAmount.ToString("N2");
                lblTotAbatement.Text=((70 * TotalInvAmount)/100).ToString("N2");
                lblTotLessAbate.Text = ((30 * TotalInvAmount) / 100).ToString("N2");

                lblSerTax.Text = Convert.ToDouble(dsReport.Tables[0].Rows[0]["ServTax_Amnt"]).ToString("N2");
                lblSwatchBharatTax.Text = Convert.ToDouble(dsReport.Tables[0].Rows[0]["SwchBrtTax_Amt"]).ToString("N2");
                lblKrishiTaxHead.Text = Convert.ToDouble(dsReport.Tables[0].Rows[0]["KisanKalyan_Amnt"]).ToString("N2");

                lblTOTServTax.Text =string.Format("{0:0,0.00}", Convert.ToString(Convert.ToDouble(dsReport.Tables[0].Rows[0]["ServTax_Amnt"]) +
                                     Convert.ToDouble(dsReport.Tables[0].Rows[0]["SwchBrtTax_Amt"]) + Convert.ToDouble(dsReport.Tables[0].Rows[0]["KisanKalyan_Amnt"])));
            }
        }

        private void PrintInvoiceDetl(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();
            string CompName = string.Empty; string Add1 = string.Empty, Add2 = string.Empty, cityHeader = string.Empty, stateHeader = string.Empty;
            string PhNo = string.Empty; string City = string.Empty; string State = string.Empty; string PanNo; string TinNo = string.Empty; string TanNo = string.Empty;
            string Serv_No = string.Empty; string FaxNo = string.Empty; string CodeNo = string.Empty;
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details..

            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            cityHeader = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            stateHeader = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = "PAN No : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            TanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Tan_No"]);
            CodeNo = "Code No. : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            //Serv_No = "STax No : " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Code_No"]);
            lblCompname.Text = CompName.ToString();
            lblJurCity1.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            lblForComp.Text ="For " + CompName.ToString();
            if (string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])) == false) { lblCompPhNo1.Text = PhNo.ToUpper(); } else { lblCompPhNo1.Visible = false; }
            if (string.IsNullOrEmpty(CompName) == false) { lblCompanyname1.Text = CompName.ToUpper(); } else { lblCompanyname1.Visible = false; }
            if (string.IsNullOrEmpty(Add1) == false) { lblCompAdd3.Text = Add1.ToUpper(); } else { lblCompAdd3.Visible = false; }
            if (string.IsNullOrEmpty(Add2) == false) { lblCompAdd4.Text = Add2.ToUpper(); } else { lblCompAdd4.Visible = false; }
            if (string.IsNullOrEmpty(City) == false) { lblCompCity1.Text = City.ToUpper(); } else { lblCompCity1.Visible = false; }
            if (string.IsNullOrEmpty(State) == false) { lblDist.Text = State.ToUpper(); } else { lblDist.Visible = false; }
            #endregion
            Loadimage(); // print logo
            DataSet PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                Labparty.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblCompdec.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                LabpartyAdd.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { LabpartyAdd.Text = LabpartyAdd.Text + PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { }
                LabpartyCity.Text = CompDetl.Tables[0].Rows[0]["City_Idno"].ToString();
                LabpartyState.Text = CompDetl.Tables[0].Rows[0]["State_Idno"].ToString();
                lblFromAdd.Text = Labparty.Text + "," + LabpartyCity.Text;
            }
            DataSet dsReportDetl = null;
            dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintGCA',@Id='" + HeadIdno + "',@PrintFormat='" + PrintFormat + "'");

            if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0 && dsReportDetl.Tables[0].Rows.Count > 0)
            {
                valuelblinvoicveno1.Text = dsReportDetl.Tables[1].Rows[0]["Inv_No"].ToString();
                valuelblinvoicedate1.Text = Convert.ToDateTime(dsReportDetl.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");
                //lblCode1.Text = CodeNo;
                if (string.IsNullOrEmpty(PanNo) == false) { lblPanNo1.Text = PanNo.ToUpper(); } else { lblPanNo1.Visible = false; }
                if (string.IsNullOrEmpty(TanNo) == false) { lblTanNo.Text = "TAN No :" + TanNo.ToUpper(); } else { lblTanNo.Visible = false; }
                Repeater2.DataSource = dsReportDetl.Tables[0];
                Repeater2.DataBind();

                lblTotAmnt.Text = TotalFright.ToString("N2");
                lblWeight.Text = TotalQty.ToString("N2");
                string numbertoent = NumberToText(Convert.ToInt32(TotalFright));
                lblTowords1.Text = numbertoent.ToUpper();
                lblServiceTax.Text = TotalSTax.ToString("N2");
                lblSwachTax.Text = TotalSBTaxAmnt.ToString("N2");
                lblKrishiTax.Text = TotalKKTaxAmnt.ToString("N2");
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                TotalInvAmount += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotInvAmount"));
                TotalInvWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotInvWeight"));
                TotQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "GR_Count"));
                TotShortQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotInvShortage_Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                TotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TotalFrt"));
                TotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Dsp_Qty"));
                TotalSTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "ServTax_Amnt"));
                TotalSBTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SwchBrtTax_Amt"));
                TotalKKTaxAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "KisanKalyan_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotalFreight = e.Item.FindControl("lblTotAmnt") as Label;
                Label lblTotalQty = e.Item.FindControl("lblWeight") as Label;
                string s = TotalFright.ToString("N2");

                //lblTotalFreight.Text = TotalFright.ToString();
                //lblTotalQty.Text = TotalQty.ToString();
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
        public void Loadimage()
        {
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            tblUserPref obj1 = objInvoiceDAL.SelectUserPref();
            if (Convert.ToBoolean(obj1.Logo_Req))
            {
                if (obj1.Logo_Image != null)
                {
                    imgWondercement1.Visible = true;
                    imgWondercement2.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                    imgWondercement1.ImageUrl = hideimgvalue.Value;
                    imgWondercement2.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                imgWondercement1.Visible = false;
                imgWondercement2.Visible = false;
            }
        }
    }
}