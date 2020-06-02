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
    public partial class Ultratech : System.Web.UI.Page
    {
        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalSHBages = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
            }
             if (!Page.IsPostBack)
             {
                 if (string.IsNullOrEmpty(Request.QueryString["q"]) != true && string.IsNullOrEmpty(Request.QueryString["P"]) != true && string.IsNullOrEmpty(Request.QueryString["R"]) !=true)
                 {
                     string PrintFormat = Request.QueryString["P"].ToString();

                     if (Request.QueryString["R"] == "2")
                     {
                         header.Visible = false;
                         lblJurCity.Visible = false;
                         lblOwnerPhoneNo.Visible = false;
                     }
                     else
                     {
                         header.Visible = true;
                         lblOwnerPhoneNo.Visible = true;
                         lblJurCity.Visible = true;
                     }

                     try
                     {
                         printUltraTech.Visible = true;
                         PrintInvoiceBeawer(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                         PrintHelper.PrintWebControl(printUltraTech);
                     }
                     catch (Exception exe)
                     {

                     }
                 }
             }
        }
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
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
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
            Loadimage();  // print logo
            DataSet PartyDetails = null;
            PartyDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name,ISNULL(A.Address1,'') Address1,ISNULL(A.Address2,'') Address2,ISNULL(A.Pin_Code,0) Pin_Code,ISNULL(A.Pin_Code,0) Pan_No,ISNULL(SM.State_Name,'') State_Name,ISNULL(CM.City_Name,'') City_Name from AcntMast A  Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno LEFT OUTER JOIN tblCityMaster CM ON CM.City_Idno=A.City_Idno LEFT OUTER JOIN tblStateMaster SM ON A.State_Idno=SM.State_Idno WHERE I.Inv_Idno='" + HeadIdno + "' ");
            if (PartyDetails != null && PartyDetails.Tables[0].Rows.Count > 0)
            {
                lblPartyName.Text = PartyDetails.Tables[0].Rows[0]["Party_Name"].ToString();
                lblPartyAddress1.Text = PartyDetails.Tables[0].Rows[0]["Address1"].ToString();
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Address2"].ToString()) == false) { lblPartyAddress2.Text = PartyDetails.Tables[0].Rows[0]["Address2"].ToString(); } else { lblPartyAddress2.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) == false && Convert.ToInt32(PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString()) > 0) { lblPartyPinCode.Text = " - " + PartyDetails.Tables[0].Rows[0]["Pin_Code"].ToString(); } else { lblPartyPinCode.Visible = false; }
                if (string.IsNullOrEmpty(PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString()) == false && Convert.ToInt32(PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString()) > 0) { lblPartyPanNo.Text = "Pan No : " + PartyDetails.Tables[0].Rows[0]["Pan_No"].ToString(); } else { lblPartyPanNo.Visible = false; }
            }
            lblCompPanNo.Text = PanNo;
            lblServTaxNo.Text = ServTaxNo;

            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintGCA',@Id='" + HeadIdno + "',@PrintFormat='" + PrintFormat + "'");

            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0 && dsReport.Tables[1].Rows.Count > 0)
            {
                lblBillNo.Text = "Bill No : " + dsReport.Tables[1].Rows[0]["Inv_No"].ToString();
                lblBillDate.Text = "Bill Date : " + Convert.ToDateTime(dsReport.Tables[1].Rows[0]["Inv_Date"].ToString()).ToString("dd-MM-yyyy");
                Repeater1.DataSource = dsReport.Tables[0];
                Repeater1.DataBind();
                string numbertoent = NumberToText(Convert.ToInt32(Total));
                lblRupeesWord.Text = "RUPEES : " + numbertoent.ToUpper() + " ONLY.";
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                TotalFright += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Freight"));
                TotalQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Dsp_Qty"));
                TotalSHBages += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage_Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotalFreight = e.Item.FindControl("lblTotalFreight") as Label;
                Label lblTotalQty = e.Item.FindControl("lblTotalQty") as Label;
                Label lblTotalShBags = e.Item.FindControl("lblTotalShBags") as Label;

                lblTotalFreight.Text = TotalFright.ToString("N2");
                lblTotalQty.Text = TotalQty.ToString("N2");
                lblTotalShBags.Text = TotalSHBages.ToString("N2");
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
                    imgultratech.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                    imgultratech.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                imgultratech.Visible = false;
            }
        }
    }
}