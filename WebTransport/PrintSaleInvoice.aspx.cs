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
    public partial class PrintSaleInvoice : System.Web.UI.Page
    {
        #region Variables...
        double dtotAmnt = 0;
        #endregion
        #region PageLoad...
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.UrlReferrer == null)
            {
                //base.AutoRedirect();
            }
             if (string.IsNullOrEmpty(Request.QueryString["q"]) != true)
             {
                 try
                 {
                     PrintInvoice(Convert.ToInt64(Request.QueryString["q"].ToString()));
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                     //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                 }
                 catch (Exception Exe)
                 {
                 }
             }
       }
        #endregion
        #region functions
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
       private void PrintInvoice(Int64 HeadIdno)
        {
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            lblCompName.Text = lblsign.Text =  Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lbltr.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            lbladdr.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            lblemail.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
            string Stat = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]);
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='Printsaleinvoice',@Id='" + HeadIdno + "'");
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                Repeater3.DataSource = dsReport.Tables[0];
                Repeater3.DataBind();
                lblcomp.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Acnt_Name"]);
                lbladd.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address1"]);
                lbldate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                lblbillno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
                string acntstateid = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Idno"]);
                double total = Convert.ToDouble(lbltotal.Text);

                if (Stat == acntstateid)
                {
                    lblbillamnt.Text = total.ToString("N2");
                    lblsgstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables[0].Rows[0]["SGST_Amt"].ToString()));
                    lblcgstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables[0].Rows[0]["CGST_Amt"].ToString()));
                    lblsgstper.Text = "SGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["SGST_Per"].ToString()) + " %";
                    lblcgstper.Text = "CGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["CGST_Per"].ToString()) + " %";
                    lbligstper.Text = "IGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["IGST_Per"].ToString()) + " %";
                    lbligstamnt.Text = "0.00";
                    double sgst = Convert.ToDouble(lblsgstamnt.Text);
                    double cgst = Convert.ToDouble(lblcgstamnt.Text);
                    double TotalAmount = Convert.ToDouble(total + sgst + cgst);
                    lblTotalAmount.Text = TotalAmount.ToString("N2");
                    string[] str1 = TotalAmount.ToString().Split('.');
                    string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                    lblword.Text = numbertoent + " Only";
                    lbligstper.Visible = false;
                    lbligstamnt.Visible = false;
                    thigstper.Visible = false;
                    thigstamnt.Visible = false;
                }
                else
                {
                    lblbillamnt.Text = total.ToString("N2");
                    lblsgstper.Text = "SGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["SGST_Per"].ToString()) + " %";
                    lblcgstper.Text = "CGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["CGST_Per"].ToString()) + " %";
                    lblsgstamnt.Text = "0.00";
                    lblcgstamnt.Text = "0.00";
                    lbligstamnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables[0].Rows[0]["IGST_Amt"].ToString()));
                    lbligstper.Text = "IGST " + Convert.ToDouble(dsReport.Tables[0].Rows[0]["IGST_Per"].ToString()) + " %";
                    double igst = Convert.ToDouble(lbligstamnt.Text);
                    double TotalAmount = Convert.ToDouble(total + igst);
                    lblTotalAmount.Text = TotalAmount.ToString("N2");
                    string[] str1 = TotalAmount.ToString().Split('.');
                    string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                    lblword.Text = numbertoent + " Only";
                    lblsgstper.Visible = false;
                    lblsgstamnt.Visible = false;
                    lblcgstper.Visible = false;
                    lblcgstamnt.Visible = false;
                    thsgstamnt.Visible = false;
                    thsgstper.Visible = false;
                    thcgstper.Visible = false;
                    thcgstamnt.Visible = false;
                }
            }
        }
        #endregion
        #region RepeaterDataBound
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
       {
           if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
           {
               dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
           }
           else if (e.Item.ItemType == ListItemType.Footer)
           {
               lbltotal.Text = dtotAmnt.ToString("N2");
           }
       }
        #endregion
    }
}