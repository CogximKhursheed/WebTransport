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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Xml;

namespace WebTransport
{
    public partial class PJLBillSummary : System.Web.UI.Page
    {
        DataTable dttemp = new DataTable();
        double dtotAmnt = 0; double Dtotqty = 0; double Dtotoll = 0; double Dtotul = 0; double Dtotshort = 0; double Dtotweight = 0; double DTAMNT = 0;
        double per = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                //base.AutoRedirect();
            }

            try
            {
                PrintInvoice(Request.QueryString["q"].ToString().Replace("%2c", ","));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
            catch (Exception Exe)
            {

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
        private void PrintInvoice(String HeadIdno)
        {
            InvoiceDAL obj = new InvoiceDAL();
            String CityName = Session["fromcity"] as string;
            dttemp = obj.city(ApplicationFunction.ConnectionString(), CityName);

            String GRNo = HeadIdno;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = "";
            string GSTIN = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast A Left JOIN tblCITYMASTER CM On CM.city_idno=A.city_idno Left join tblStateMaster SM ON SM.state_idno=A.state_idno");
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PJLBillSummary',@Id='" + HeadIdno + "'");
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(dttemp.Rows[0]["Address1"]) + "," + Convert.ToString(dttemp.Rows[0]["Address2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Mobile_1"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]) + "(CODE :-  " + Convert.ToString(dsReport.Tables[0].Rows[0]["GSTState_Code"]) + ")";
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            GSTIN = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblpin.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);

            lblCompname.Text = CompName; lblcom.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblcity.Text = City;
            lblstate.Text = State;
            lblmobile.Text = PhNo;
            lbgst.Text = GSTIN;
            lblpan.Text = PanNo.ToString();

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
            }
            lblunit.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["unit"]);
            lblbillno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
            lblcontname.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
            lbladd1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address1"]);
            lblcadd2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address2"]);
            lbldis.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
            diccode.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Pin_Code"]);
            lblst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]) + "(CODE :-  " + Convert.ToString(dsReport.Tables[0].Rows[0]["GSTState_Code"]) + ")";
            lblgst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_GSTINNo"]);

        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Dtotweight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Dtotqty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Dtotoll += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "TollTax_Amnt"));
                Dtotul += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "UL"));
                Dtotshort += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "SHORT_MT"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {

                lbltfrieghtamount.Text = Dtotweight.ToString("N2");
                lbltqty.Text = Dtotqty.ToString("N2");
                lblttotal.Text = dtotAmnt.ToString("N2");
                lblttoll.Text = Dtotoll.ToString("N2");
                lbltunloading.Text = Dtotul.ToString("N2");
                lbltshorteg.Text = Dtotshort.ToString("N2");
            }
        }

    }
}