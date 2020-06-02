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
    public partial class PrintInvAmbujaCement : System.Web.UI.Page
    {
        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalInvAmount = 0, TotalInvWeight = 0, TotQty, TotShortQty = 0;
        double dDiffShrtge = 0, dGrossShrtgeAmnt = 0, dNetShrtAmnt = 0, dTotShrtAmnt = 0, dQty = 0, drate = 0, drecd = 0, ddis = 0, damnt = 0;
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
                    try
                    {
                        if (Request.QueryString["R"] == "2")
                        {
                            Header.Visible = false;
                            cmp.Visible = false;
                            trcompnay.Visible = false;
                            traddress.Visible = false;
                            trSerTnno.Visible = false;
                            tr2.Visible = false;
                        }
                        else
                        {
                            Header.Visible = true;
                            cmp.Visible = true;
                            trcompnay.Visible = true;
                            traddress.Visible = true;
                            trSerTnno.Visible = true;
                            tr2.Visible = true;
                        }

                        PrintASHOK(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                        // PrintHelper.PrintWebControl(PrintAS1);

                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }

        }

        private void PrintASHOK(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();

            //double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            string ServTaxNo = "";
            // int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
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
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanynameAS.Text = CompName; lblcompnameAS.Text = "For - " + CompName; lblcompanyFooterAS2.Text = "For - " + CompName;
            lblcompanynameAS1.Text = CompName;
            lblCompAdd1AS.Text = Add1;
           //lblCompTINAS.Text = TinNo.ToString();
            lblCompAdd2AS.Text = Add2;
            lblCompCityAS.Text = City;
            lblCompStateAS.Text = State;
           // lblCompPhNoAS.Text = PhNo;
            lblPanNoAS.Text = PanNo.ToString();
            lblcmpjurisdiction.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";


            #region FRIST PAGE....

            lblCompanynameAS2.Text = CompName; lblcompnameAS.Text = "For - " + CompName;
            lblCompAdd1AS2.Text = Add1;
            lblCompAdd2AS2.Text = Add2;
            lblCompCityAS2.Text = City;
          //lblCompPhNoAS2.Text = PhNo;
            lblCompStateAS2.Text = State;
            #endregion
            Loadimage();

            DataSet dschlncount = null;
            string strchlncount = "";
            DataSet dsReport = null;
            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrintAmbuja',@Id='" + HeadIdno + "'");

            dschlncount = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='chlncount',@Id='" + HeadIdno + "'");
            if (!string.IsNullOrEmpty( Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])))
            {
                lblCompPhNoAS2.Text = PhNo;
                lblCompPhNoAS.Text = PhNo;
            }
            if (ServTaxNo == "")
            {
                lblSrvtAS.Visible = false;
                trSerTnno.Visible = false;
                lblSerTAS2.Visible = false; lblvalSerTAS2.Visible = false;
                trtinAS2.Visible = false;
            }
            else
            {
                trSerTnno.Visible = true;
                lblvalSerTAS2.Visible = true;
                lblCompSertAS.Text = ServTaxNo;
                lblCompSertAS.Visible = true; lblSrvtAS.Visible = true;


                trtinAS2.Visible = true;
                lblvalSerTAS2.Text = ServTaxNo;
                lblvalSerTAS2.Visible = true; lblSerTAS2.Visible = true;
            }
            if (PanNo == "")
            {
                trpan.Visible = false;
                lblTxtPanNoAS.Visible = false; lblPanNoAS.Visible = false;

                trPanAS2.Visible = false;
                lblpanAS2.Visible = false; lblpanvalAS2.Visible = false;
            }
            else
            {
                trpan.Visible = true;
                lblPanNoAS.Text = PanNo;
                lblTxtPanNoAS.Visible = true; lblPanNoAS.Visible = true;

                trPanAS2.Visible = true;
                lblpanvalAS2.Text = PanNo;
                lblpanAS2.Visible = true; lblpanvalAS2.Visible = true;
            }

            #endregion

            if (dschlncount != null && dschlncount.Tables[0].Rows.Count > 0)
            {
                strchlncount = Convert.ToString(dschlncount.Tables[0].Rows[0]["TotlChln"]);
            }

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicvenoAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_prefix"]) + Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
                valuelblinvoicedateAS.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                string month = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("MMMM-yyyy");
                lblformont.Text = month;
                valuelblinvoicvenoAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_prefix"]) + Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
                valuelblinvoicedateAS2.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                lblSenderNameAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
                lblSenderNameAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);

                lblsenderaddressAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                lblsenderaddressAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                if (lblsenderaddressAS.Text == "")
                {
                    lblsenderaddressAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                    lblsenderaddressAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                }
                lblsenderstateAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
                lblsendercityAS.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                lblsenderstateAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
                lblsendercityAS2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);

                //From City Print
                lblloctionfromAS2.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                lblPartiloctionfromAS2.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                lblBilloctionfromAS1.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                lblloctionfromAS1.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["From_City"]);
                //
                if (dsReport != null && dsReport.Tables[2].Rows.Count > 0)
                {
                    lbldatefromAS2.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["DateFrom"]).ToString("dd-MM-yyyy");
                    lbldatetoAS2.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["Todate"]).ToString("dd-MM-yyyy");
                    lbldatefromparti.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["DateFrom"]).ToString("dd-MM-yyyy");
                    lbldatetoparti.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["Todate"]).ToString("dd-MM-yyyy");
                    lbldatefrom.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["DateFrom"]).ToString("dd-MM-yyyy");
                    lbldateto.Text = Convert.ToDateTime(dsReport.Tables[2].Rows[0]["Todate"]).ToString("dd-MM-yyyy");
                }
                else
                {
                    lbldatefromAS2.Text = "";
                    lbldatetoAS2.Text = "";
                }

                Repeater4.DataSource = dsReport.Tables[1];
                Repeater4.DataBind();
                if (Convert.ToInt32(Repeater4.Items.Count) > 0)
                {
                    lblenclouserchallanAS.Visible = true;
                    lblenclousergr.Visible = true;
                    lblvalueencosersAS.Visible = true;
                    lblchallanval.Visible = true;
                    lblencloChln.Visible = true;

                    lblenclouservalchln.Visible = true;
                    lblValenclousergrAS2.Visible = true;
                    lblenclousergrAS2.Visible = true;

                    lblchallanval.Text = strchlncount;
                    lblenclouservalchln.Text = strchlncount;
                    lblvalueencosersAS.Text = Convert.ToString(Repeater4.Items.Count);
                    lblValenclousergrAS2.Text = Convert.ToString(Repeater4.Items.Count);
                }
                else
                {
                    lblenclouserchallanAS.Visible = false;
                    lblenclousergr.Visible = false;
                    lblvalueencosersAS.Visible = false;
                    lblchallanval.Visible = false;

                    lblenclouservalchln.Visible = false;
                    lblValenclousergrAS2.Visible = false;
                    lblenclousergrAS2.Visible = false;
                }

                lblQtyparti.Text = Convert.ToString(drecd);
                lblNetAmntAS.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables[1].Rows[0]["Net_Amnt"]));
                lblfoteramnt.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables[1].Rows[0]["Net_Amnt"]));
                lbltotlamountAS2.Text = string.Format("{0:0,0.00}", Convert.ToString(dsReport.Tables[1].Rows[0]["Net_Amnt"]));

                lbltotlqtyAS2.Text = string.Format("{0:0,0.00}", (drecd));
                lblfoterqtytotlAS2.Text = string.Format("{0:0,0.00}", (drecd));

                double txtfinl = Convert.ToDouble(lblNetAmntAS.Text);
                string[] str1 = txtfinl.ToString().Split('.');
                string numtoint = NumberToText(Convert.ToInt32(str1[0])) + " Only.";
                lblamntword.Text = numtoint;
                lblamntinwrdAS2.Text = numtoint;

            }
        }

        protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ddis += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                drecd += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                drate += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Rate"));
                damnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // The following label displays the total
                lbldisfoter.Text = ddis.ToString("N2");
                lblrecdfoter.Text = drecd.ToString("N2");
                // lblfreightfoter.Text = drate.ToString("N2");
                lbltotlamntrfoter.Text = damnt.ToString("N2");

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
                    imgAmbujacemant.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                    imgAmbujacemant.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                imgAmbujacemant.Visible = false;
            }
        }
    }
}