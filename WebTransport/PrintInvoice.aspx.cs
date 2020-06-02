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
    public partial class PrintInvoice : Pagebase
    {
        InvoiceDAL objInvoiceDAL = new InvoiceDAL();
        double dtotAmnt = 0;
        double dTotReptWeight = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotReptServTax = 0, delQty = 0, totserv = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                this.Bind();
                Populate(Convert.ToInt64(Request.QueryString["Id"]));
                PrintHelper.PrintWebControl(print);
                //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            }
        }

        private void PrintInvoice1(Int64 HeadIdno)
        {
            Repeater obj = new Repeater(); string logopath = string.Empty;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + " " + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + "   " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompCityPin.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            lblCompanyname.Text = CompName; lblcompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompTIN.Text = TinNo.ToString();
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            //lblCompState.Text = State;
            //lblCompPhNo.Text = PhNo;
            lblPanNo.Text = PanNo.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])))
            {
                lblCompPhNo.Text = PhNo;
            }
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
                lblTxtPanNo.Visible = false; lblPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblTxtPanNo.Visible = true; lblPanNo.Visible = true;
            }
            Loadimage();
            //logopath = "Images/Sweta_Entp_Logo.jpg";
            //imglogo.ImageUrl = logopath;
            #endregion
            //DataSet CodeNo = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "SELECT ISNULL(A.ACNT_NAME,'') Party_Name from AcntMast A Inner Join tblInvgenHead I ON I.sendr_idno=A.acnt_idno  WHERE acnt_name like '%shree cement%' AND I.Inv_Idno='" + HeadIdno + "' ");
            //if (CodeNo != null && CodeNo.Tables[0].Rows.Count > 0)
            //{
            //    lblcodeno.Visible = true;
            //    lblvaluecodeno.Visible = true;
            //    lblvaluecodeno.Text = "T0073";
            //}
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectDaulatPrint',@Id='" + HeadIdno + "'");
            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicveno.Text = Convert.ToString(txtInvPreIx.Text) + Convert.ToString(txtinvoicNo.Text);
                valuelblinvoicedate.Text = (txtDate.Text);
                lblSenderName.Text = Convert.ToString(ddlSenderName.SelectedItem.Text);
                lblSender.Text = Convert.ToString(ddlSenderName.SelectedItem.Text);
                lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                if (lblsenderaddress.Text == "")
                {
                    lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                }
                lblsenderstate.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
                lblsendercity.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                lblsendcity.Text = ", " + Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                lblSTax.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrSwchBrtTax"]);
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                //lblvalueencosers.Text = Convert.ToString(Repeater1.Items.Count);
                for (int i = 0; i < dsReport.Tables[1].Rows.Count; i++)
                {
                    totserv += Convert.ToDouble(dsReport.Tables[1].Rows[i]["servTax_Amnt"]);
                }
                Double num1 = 0; double res;
                string[] str = dTotNetAmnt.ToString().Split('.');
                if (str.Length == 2)
                {
                    num1 = Convert.ToDouble("0." + str[1]);
                }
                if (num1 < 0.51)
                {
                    res = Math.Floor(dTotNetAmnt);

                    lblNetAmnt.Text = String.Format("{0:0,0.00}", res);
                }
                else
                {
                    res = Math.Round(dTotNetAmnt);

                    lblNetAmnt.Text = String.Format("{0:0,0.00}", res);
                }

                double txtfinl = Convert.ToDouble(lblNetAmnt.Text);
                string[] str1 = txtfinl.ToString().Split('.');
                string numbertoent = NumberToText(Convert.ToInt32(str1[0]));
                lblTowords.Text = numbertoent;
                //lblNetAmnt.Text = string.Format("{0:0,0.00}", (dTotNetAmnt));
                //double serv= 14 * 30 * dTotNetAmnt / 10000;
                double Stax = Convert.ToDouble(lblSTax.Text);
                lblSTax.Text = "";
                double swatch = Stax * 30 * dTotNetAmnt / 10000;
                lblServ.Text = string.Format("{0:0,0.00}", (totserv));
                lblSwach.Text = string.Format("{0:0,0.00}", (swatch));
                double tottax = totserv + swatch;
                lblTot.Text = tottax.ToString();
                string[] strTax = lblTot.ToString().Split('.');
                if (strTax.Length == 2)
                {
                    num1 = Convert.ToDouble("0." + strTax[1]);
                }
                if (num1 < 0.51)
                {
                    res = Math.Floor(tottax);

                    lblTot.Text = String.Format("{0:0,0.00}", res);
                }
                else
                {
                    res = Math.Round(tottax);

                    lblTot.Text = String.Format("{0:0,0.00}", res);
                }

            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //gives the sum in string Total.                 
                // double dTotReptWeight = 0, dTOtAmnt = 0, dTotUnloading = 0, dTotNetAmnt = 0, dTotShortage = 0, dTotServTax = 0;
                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dTotReptWeight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                delQty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Del_Qty"));
                dTotUnloading += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                dTotNetAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));
                dTotShortage += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage"));
                dTotReptServTax += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "servTax_Amnt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lbltotalWeight.Text = dTotReptWeight.ToString("N2");
                lblWeight.Text = delQty.ToString("N2");
                //lblAmount.Text = dtotAmnt.ToString("N2");
                //lblUnloading.Text = dTotUnloading.ToString("N2");
                lblTotAmnt.Text = dTotNetAmnt.ToString("N2");
                //lblShtg.Text = dTotShortage.ToString("N2");
                //lblTotServTax.Text = dTotReptServTax.ToString("N2");lblt

            }
        }

        private void Populate(Int64 HeadId)
        {
            InvoiceDAL obj = new InvoiceDAL();
            tblInvGenHead chlnBookhead = obj.selectHead(HeadId);
            txtinvoicNo.Text = Convert.ToString(chlnBookhead.Inv_No);
            txtInvPreIx.Text = Convert.ToString(chlnBookhead.Inv_prefix); ;
            txtDate.Text = Convert.ToDateTime(chlnBookhead.Inv_Date).ToString("dd-MM-yyyy");
            ddlSenderName.SelectedValue = Convert.ToString(chlnBookhead.Sendr_Idno);
            //txtCSServTax.Text = Convert.ToDouble(chlnBookhead.ConsignrServTax).ToString("N2");

            //txtTrSwchBrtTax.Text = Convert.ToDouble(chlnBookhead.TrSwchBrtTax_Amnt).ToString("N2");
            //txtCSSwchBrtTax.Text = Convert.ToDouble(chlnBookhead.ConsignrSwchBrtTax).ToString("N2");

            //txtGrosstotal.Text = Convert.ToDouble(chlnBookhead.GrossTot_Amnt).ToString("N2");
            //txtNetAmnt.Text = Convert.ToDouble(chlnBookhead.Net_Amnt).ToString("N2");
            //txtRoundOff.Text = Convert.ToDouble(chlnBookhead.RoundOff_Amnt).ToString("N2");
            //ddlFromCity.SelectedValue = Convert.ToString(chlnBookhead.BaseCity_Idno);
            //ddlFromCity.Enabled = false;
            double RcpttAmnt = obj.SelectUpdateRcptAmnt(HeadId, ApplicationFunction.ConnectionString());
            obj = null;
            //netamntcal();
            PrintInvoice1(HeadId);
        }

        private void Bind()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.selectSenderName();
            obj = null;
            if (lst.Count > 0)
            {
                ddlSenderName.DataSource = lst;
                ddlSenderName.DataTextField = "Acnt_Name";
                ddlSenderName.DataValueField = "Acnt_Idno";
                ddlSenderName.DataBind();
            }
            //ddlSenderName.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    imglogo.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                    imglogo.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                imglogo.Visible = false;
            }
        }
    }
}