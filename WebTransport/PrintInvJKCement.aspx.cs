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
    public partial class PrintInvJKCement : System.Web.UI.Page
    {

        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalInvAmount = 0, TotalInvWeight = 0, TotQty, TotShortQty = 0;
        double dqty = 0, drate = 0, dfreight = 0, dshortage = 0, dtotal = 0;
        double dDiffShrtge = 0, dGrossShrtgeAmnt = 0, dNetShrtAmnt = 0, dTotShrtAmnt = 0, drecd = 0, ddis = 0, damnt = 0;
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
                    try
                    {
                        if (Request.QueryString["R"] == "2")
                        {
                            Header.Visible = false;
                        }
                        else
                        {
                            Header.Visible = true;
                        }
                        //printNimbaheraDetl.Visible = false;
                        //printNimbaheraHead.Visible = false;
                        PrintJKInvoice(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));
                        //PrintHelper.PrintWebControl(printNimbaheraHead);
                        //   PrintHelper.PrintWebControl(printNimbaheraDetl);
                        //printNimbaheraHead.Visible = true;
                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }

        }

        private void PrintJKInvoice(Int64 HeadIdno, Int64 PrintFormat)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string SapNo; string TinNo = ""; string Serv_No = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            string ServTaxNo = "";
            int grtype = 0;
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
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
            SapNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["SAP_No"]);
            lblCompanyname.Text = CompName; lblcompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompTIN.Text = ServTaxNo.ToString();
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            if (PhNo.ToString().Trim() != "")
            {
                lblCompPhNo.Text = PhNo;
                lblCompPhNo.Visible = true;
            }
            else
            {
                lblCompPhNo.Text = "";
                lblCompPhNo.Visible = false;
            }
            lblPanNo.Text = PanNo.ToString();

            if (ServTaxNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = ServTaxNo;
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
            if (SapNo == "")
            {
                lblsapvalno.Visible = false; lblsapno.Visible = false;
            }
            else
            {
                lblsapvalno.Text = SapNo;
                lblsapvalno.Visible = true; lblsapno.Visible = true;

            }
            
            #endregion
            DataSet dsReport = null;

            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='PrintJK',@Id='" + HeadIdno + "'");

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicveno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_prefix"]) + Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
                valuelblinvoicedate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                lblSenderName.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
                lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                if (lblsenderaddress.Text == "")
                {
                    lblsenderaddress.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                }
                lblsenderstate.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
                lblsendercity.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                lblvalueencosers.Text = Convert.ToString(Repeater1.Items.Count);

                string strGrtype = string.Empty;
                if (dsReport != null && dsReport.Tables[2].Rows.Count > 0)
                {
                    for (int k = 0; k < dsReport.Tables[2].Rows.Count; k++)
                    {
                        if(Convert.ToInt32(dsReport.Tables[2].Rows[k]["Gr_Typ"]) == 1)
                        {
                            strGrtype = strGrtype + " (PAID) ";
                        }
                        else if (Convert.ToInt32(dsReport.Tables[2].Rows[k]["Gr_Typ"]) == 2)
                        {
                            strGrtype = strGrtype + " (TO BILLED) ";
                        }
                        else if (Convert.ToInt32(dsReport.Tables[2].Rows[k]["Gr_Typ"]) == 3)
                        {
                            strGrtype = strGrtype + " (TO PAY GR) ";
                        }
                    }
                }
                if (string.IsNullOrEmpty(strGrtype))
                {
                    lblgrtype.Text = "";
                    trgrtype.Visible = false;
                }
                else
                {
                    trgrtype.Visible = true;
                    lblgrtype.Text = strGrtype;
                }


                lblNetAmnt.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["Net_Amnt"]))? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["Net_Amnt"])));
                valuelbltxtctax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["ServTax_Amnt"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["ServTax_Amnt"])));
                valueCSBTax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["TrSwchBrtTax_Amnt"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["TrSwchBrtTax_Amnt"])));
                ValueCKisanTax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["TrKisanKalyanTax_Amnt"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["TrKisanKalyanTax_Amnt"])));

                valuetxtCStax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrServTax"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrServTax"])));
                valueCCSBTax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrSwchBrtTax"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrSwchBrtTax"])));
                ValueCCKisanTax.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrKisanTax_Amnt"])) ? "0.00" : string.Format("{0:0,0.00}", (Convert.ToString(dsReport.Tables[0].Rows[0]["ConsignrKisanTax_Amnt"])));
            }
            Loadimage();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dqty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                drate += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Rate"));
                dfreight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "amount"));
                dshortage += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Shortage"));
                dtotal += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Net_Amnt"));

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblqtymt = e.Item.FindControl("lblqtymt") as Label;
                Label lblrate = e.Item.FindControl("lblrate") as Label;
                Label lblfreight = e.Item.FindControl("lblfreight") as Label;
                Label lblshortage = e.Item.FindControl("lblshortage") as Label;
                Label lbltotlamntgrid = e.Item.FindControl("lbltotlamntgrid") as Label;

                lblqtymt.Text = string.Format("{0:0,0.00}", (dqty));
                lblfreight.Text = string.Format("{0:0,0.00}", (dfreight));
                lblshortage.Text = string.Format("{0:0,0.00}", (dshortage));
                lbltotlamntgrid.Text = string.Format("{0:0,0.00}", (dtotal));

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
                    imgjkcement.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                   imgjkcement.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                imgjkcement.Visible = false;
            }
        }
    }
}