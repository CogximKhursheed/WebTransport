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
    public partial class RptInvoiceDetail : System.Web.UI.Page
    {
        #region Variables
        double dtotAmnt = 0;
        #endregion
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                //base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    PrintInvoiceRpt(Request.QueryString["q"].ToString().Replace("%2c", ","), Convert.ToInt64(Request.QueryString["P"].ToString().Replace("%2c", ",")), Convert.ToInt64(Request.QueryString["R"].ToString().Replace("%2c", ",")));
                   // PrintInvoiceRpt(Request.QueryString["q"].ToString().Replace("%2c", ","), Convert.ToInt64(Request.QueryString["P"].Replace("%2c", ",")), Convert.ToInt64(Request.QueryString["R"].Replace("%2c", ",")));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                }
                catch (Exception Exe)
                {
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
        private void PrintInvoiceRpt(String HeadIdno, Int64 year,Int64 city)
        {
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
           
            lblCompName.Text = lblsign.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lbltr.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            lbladdr.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            lblemail.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
            lblpin.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            lblmob.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            string Stat = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
            DataSet ds2 = new DataSet();
            DataSet dsMain = new DataSet();
            InvoiceDetailsDAL obj = new InvoiceDetailsDAL();
            ManageInvoiceDetails MI = new ManageInvoiceDetails();
            Int64 fromCity = city;
            Int64 year_idno = year;
            String InvoiceNo = HeadIdno;

            string Value = InvoiceNo;
            string[] Array1 = Value.Split(new char[] { ',' });
            for (int i = 0; i < Array1.Length; i++)
            {
                ds2 = obj.Report(year_idno, Array1[i], fromCity, ApplicationFunction.ConnectionString());

                    if (i == 0)
                        dsMain = ds2;
                    else
                        dsMain.Tables[0].Merge(ds2.Tables[0]);
                }

            if (dsMain != null && dsMain.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = dsMain.Tables[0];
                Repeater1.DataBind();

                lblcomp.Text = Convert.ToString(ds2.Tables[0].Rows[0]["Acnt_Name"]);
                lbladd.Text = Convert.ToString(ds2.Tables[0].Rows[0]["Address1"]);
                lbldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                lblbill.Text = Session["InvoiceNo"] as String;
                string acntstateid = Convert.ToString(ds2.Tables[0].Rows[0]["State_Idno"]);
                double total = Convert.ToDouble(lbltotal.Text);

                if (Stat == acntstateid)
                {
                    lblbillamnt.Text = total.ToString("N2");
                    lblsgstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsMain.Tables[0].Rows[0]["SGST_Amt"].ToString()));
                    lblcgstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsMain.Tables[0].Rows[0]["CGST_Amt"].ToString()));

                    lblsgstper.Text = "SGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["SGST_Per"].ToString()) + " %";
                    lblcgstper.Text = "CGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["CGST_Per"].ToString()) + " %";
                    lbligstper.Text = "IGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["IGST_Per"].ToString()) + " %";
                    lbligstamnt.Text = "0.00";

                    double sgst = Convert.ToDouble(lblsgstamnt.Text);
                    double cgst = Convert.ToDouble(lblcgstamnt.Text);
                    double TotalAmount = Convert.ToDouble(total + sgst + cgst);
                    lblTotalAmount.Text = TotalAmount.ToString("N2");
                    string[] str1 = TotalAmount.ToString().Split('.');
                    string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                    lblword.Text = numbertoent + " Only";
                    trigst.Visible = false;
                }
                else
                {
                    lblbillamnt.Text = total.ToString("N2");
                    lblsgstper.Text = "SGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["SGST_Per"].ToString()) + " %";
                    lblcgstper.Text = "CGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["CGST_Per"].ToString()) + " %";
                    lblsgstamnt.Text = "0.00";
                    lblcgstamnt.Text = "0.00";
                    lbligstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsMain.Tables[0].Rows[0]["IGST_Amt"].ToString()));
                    lbligstper.Text = "IGST " + Convert.ToDouble(dsMain.Tables[0].Rows[0]["IGST_Per"].ToString()) + " %";
                    double igst = Convert.ToDouble(lbligstamnt.Text);
                    double TotalAmount = Convert.ToDouble(total + igst);
                    lblTotalAmount.Text = TotalAmount.ToString("N2");
                    string[] str1 = TotalAmount.ToString().Split('.');
                    string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                    lblword.Text = numbertoent + " Only";
                    trsgst.Visible = false;
                    trcgst.Visible = false;
                }
            }
        }
        #endregion
        #region Repeater Commands...
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lbltotal.Text = dtotAmnt.ToString("N2");
            }
        }
        #endregion
    }
}