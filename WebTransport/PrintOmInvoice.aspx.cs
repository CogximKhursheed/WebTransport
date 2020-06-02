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
    public partial class PrintOmInvoice : System.Web.UI.Page
    {
        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalInvAmount = 0, TotalInvWeight = 0, TotQty, TotShortQty = 0;
        double dqty = 0, drate = 0, dfreight = 0, dshortage = 0, dtotal = 0, dweight = 0, ExtraCharge = 0, ExtraAMnt=0;
        double dDiffShrtge = 0, dGrossShrtgeAmnt = 0, dNetShrtAmnt = 0, dTotShrtAmnt = 0, drecd = 0, ddis = 0, damnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]) != true && string.IsNullOrEmpty(Request.QueryString["P"]) != true && string.IsNullOrEmpty(Request.QueryString["S"]) !=true )
            {
                try
                {
                    hidPages.Value =Request.QueryString["S"].ToString();

                   
                   PrintInvoice(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToInt64(Request.QueryString["P"].ToString()));


                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
                   //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                   
                }
                catch (Exception Exe)
                {

                }
            }
            
        }

        private void PrintInvoice(Int64 HeadIdno, Int64 PrintFormat)
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
            string CompDesc = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            lblCompGSTIN.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            Bitmap bitmap = new Bitmap(200, 200);
            //graphics = Graphics.FromImage(bitmap);
            //graphics.DrawLine(new Pen(Color.Black), 0, 0, 200, 200);


            lblCompDesc.Text = Convert.ToString(CompDesc.Trim().Replace("@", "<br/>") + "<br/>" );
            lblCompName.Text = "For - " + CompName;
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
                lblCompTIN.Visible = false;
            }
            else
            {
                lblCompTIN.Text = ServTaxNo;
                lblCompTIN.Visible = true;
            }
            if (PanNo == "")
            {
                lblPanNo.Visible = false; lblPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblPanNo.Visible = true; lblPanNo.Visible = true;
            }
            
            

            #endregion
            DataSet dsReport = null;

            dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrintOM',@Id='" + HeadIdno + "'");

            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                valuelblinvoicveno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"] == null ? "" : dsReport.Tables[0].Rows[0]["Inv_No"]);
                valuelblinvoicedate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");
                GetGSTType(valuelblinvoicedate.Text);
                lblCustomer.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
                Address.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Prty_Address"]);
                lblCname.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Recivr_Name"]);
                lblMode.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Mode"]);
                lblLorryNo.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Lorry_No"]);
                lblSenderGSTIN.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["SenderGSTIN"]);
                lblReceiverGSTIN.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["ReceiverGSTIN"]);
                if (dsReport.Tables[1].Rows[0]["Supply_Date"] != null && dsReport.Tables[1].Rows[0]["Supply_Date"] != "")
                {
                    string suppdate = Convert.ToString(dsReport.Tables[1].Rows[0]["Supply_Date"]);
                    if (suppdate != "")
                    { lblSupplyDate.Text = Convert.ToDateTime(suppdate).ToString("dd/MM/yyyy"); }
                }
                if (Address.Text == "")
                {
                    Address.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                }
          
                lblsendercity.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
                origion.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Origin"]);
                lbldesc.Text = Convert.ToString(dsReport.Tables[1].Rows[0]["Delvry_Place"]);

                ExtraAMnt = Convert.ToDouble(dsReport.Tables[0].Rows[0]["Bilty_Chrgs"]);
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
                var a = dsReport.Tables[0];
                var b = dsReport.Tables[1];
             
                 //lblExtraAmnt.text = ExtraAMnt;
                string Amountinword = Convert.ToString(Convert.ToDouble(dsReport.Tables[1].Rows[0]["Net_Amnt"]) + Convert.ToDouble(dsReport.Tables[0].Rows[0]["Bilty_Chrgs"]) + Convert.ToDouble(dsReport.Tables[1].Rows[0]["Stcharg_Amnt"]));

                string numtoint = NumberToText(Convert.ToInt32(Amountinword)) + " Only.";

                lblrsinword.Text = numtoint;
              
            }
            
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ri = e.Item;
            if (e.Item.ItemType == ListItemType.Header)
            {
                Panel pnlVat = (Panel)ri.FindControl("pnlVat");
                Panel pnlGST = (Panel)ri.FindControl("pnlGST");
                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVat != null) pnlVat.Visible = true;
                        if (pnlGST != null) pnlGST.Visible = false;
                    }
                    else
                    {
                        if (pnlVat != null) pnlVat.Visible = false;
                        if (pnlGST != null) pnlGST.Visible = true;
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                Panel pnlVat = (Panel)ri.FindControl("pnlVat");
                Panel pnlVat2 = (Panel)ri.FindControl("pnlVATValue");
                Panel pnlVat4 = (Panel)ri.FindControl("pnlVATFoot2");
                Panel pnlVat3 = (Panel)ri.FindControl("pnlVATFoot1");

                Panel pnlGST = (Panel)ri.FindControl("pnlGST");
                Panel pnlGST2 = (Panel)ri.FindControl("pnlGSTValues");
                Panel pnlGST3 = (Panel)ri.FindControl("pnlGSTFoot1");
                Panel pnlGST4 = (Panel)ri.FindControl("pnlGSTFoot2");

                Label lblgrdQty = e.Item.FindControl("lblgrdQty") as Label;
                Label lblgrdSGST = e.Item.FindControl("lblgrdSGST") as Label;
                Label lblgrdCGST = e.Item.FindControl("lblgrdCGST") as Label;
                Label lblgrdIGST = e.Item.FindControl("lblgrdIGST") as Label;
                Label lblgrdVAT = e.Item.FindControl("lblgrdVAT") as Label;
                Label lblRowTotal = e.Item.FindControl("lblRowTotal") as Label;
                if (lblRowTotal != null)
                {
                    dtotal += Convert.ToDouble(lblRowTotal.Text == "" ? "0" : lblRowTotal.Text);
                }
                if (lblgrdQty != null)
                {
                    if (hidTotalQty.Value == null || hidTotalQty.Value == "") hidTotalQty.Value = lblgrdQty.Text;
                    else hidTotalQty.Value = (Convert.ToDouble(hidTotalQty.Value) + Convert.ToDouble(lblgrdQty.Text)).ToString();
                }
                if (lblgrdSGST != null)
                {
                    if (hidTotalSGST.Value == null || hidTotalSGST.Value == "") hidTotalSGST.Value = lblgrdSGST.Text;
                    else hidTotalSGST.Value = (Convert.ToDouble(hidTotalSGST.Value) + Convert.ToDouble(lblgrdSGST.Text)).ToString();
                }
                if (lblgrdCGST != null)
                {
                    if (hidTotalCGST.Value == null || hidTotalCGST.Value == "") hidTotalCGST.Value = lblgrdCGST.Text;
                    else hidTotalCGST.Value = (Convert.ToDouble(hidTotalCGST.Value) + Convert.ToDouble(lblgrdCGST.Text)).ToString();
                }
                if (lblgrdIGST != null)
                {
                    if (hidTotalIGST.Value == null || hidTotalIGST.Value == "") hidTotalIGST.Value = lblgrdIGST.Text;
                    else hidTotalIGST.Value = (Convert.ToDouble(hidTotalIGST.Value) + Convert.ToDouble(lblgrdIGST.Text)).ToString();                    
                }
                if (lblgrdVAT != null)
                {
                    if (hidTotalVAT.Value == null || hidTotalVAT.Value == "") hidTotalVAT.Value = lblgrdVAT.Text;
                    else hidTotalVAT.Value = (Convert.ToDouble(hidTotalVAT.Value) + Convert.ToDouble(lblgrdVAT.Text)).ToString();
                }
                
                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVat != null) pnlVat.Visible = true;
                        if (pnlVat2 != null) pnlVat2.Visible = true;
                        if (pnlVat3 != null) pnlVat3.Visible = true;
                        if (pnlVat4 != null) pnlVat4.Visible = true;
                        if (pnlGST != null) pnlGST.Visible = false;
                        if (pnlGST2 != null) pnlGST2.Visible = false;
                        if (pnlGST3 != null) pnlGST3.Visible = false;
                        if (pnlGST4 != null) pnlGST4.Visible = false;
                    }
                    else
                    {
                        if (pnlVat != null) pnlVat.Visible = false;
                        if (pnlVat2 != null) pnlVat2.Visible = false;
                        if (pnlVat3 != null) pnlVat3.Visible = false;
                        if (pnlVat4 != null) pnlVat4.Visible = false;
                        if (pnlGST != null) pnlGST.Visible = true;
                        if (pnlGST2 != null) pnlGST2.Visible = true;
                        if (pnlGST3 != null) pnlGST3.Visible = true;
                        if (pnlGST4 != null) pnlGST4.Visible = true;
                    }
                }
                dqty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
               // Weight
                dweight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));

                drate += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Rate"));
                dfreight += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Panel pnlVat = (Panel)ri.FindControl("pnlVat");
                Panel pnlVat2 = (Panel)ri.FindControl("pnlVATValue");
                Panel pnlVat3 = (Panel)ri.FindControl("pnlVATFoot1");

                Panel pnlGST = (Panel)ri.FindControl("pnlGST");
                Panel pnlGST2 = (Panel)ri.FindControl("pnlGSTValues");
                Panel pnlGST3 = (Panel)ri.FindControl("pnlGSTFoot1");

                Label lblTotalQty = e.Item.FindControl("lblTotalQty") as Label;
                Label lblTotalSGST = e.Item.FindControl("lblTotalSGST") as Label;
                Label lblTotalCGST = e.Item.FindControl("lblTotalCGST") as Label;
                Label lblTotalIGST = e.Item.FindControl("lblTotalIGST") as Label;
                Label lblTotalVAT = e.Item.FindControl("lblTotalVAT") as Label;

                lblTotalQty.Text = hidTotalQty.Value;
                lblTotalSGST.Text = hidTotalSGST.Value;
                lblTotalCGST.Text = hidTotalCGST.Value;
                lblTotalIGST.Text = hidTotalIGST.Value;
                lblTotalVAT.Text = hidTotalVAT.Value;

                if (hidIsGST.Value != null && hidIsGST.Value != "")
                {
                    if (hidIsGST.Value == "0")
                    {
                        if (pnlVat != null) pnlVat.Visible = true;
                        if (pnlVat2 != null) pnlVat2.Visible = true;
                        if (pnlVat3 != null) pnlVat3.Visible = true;
                        if (pnlGST != null) pnlGST.Visible = false;
                        if (pnlGST2 != null) pnlGST2.Visible = false;
                        if (pnlGST3 != null) pnlGST3.Visible = false;
                    }
                    else
                    {
                        if (pnlVat != null) pnlVat.Visible = false;
                        if (pnlVat2 != null) pnlVat2.Visible = false;
                        if (pnlVat3 != null) pnlVat3.Visible = false;
                        if (pnlGST != null) pnlGST.Visible = true;
                        if (pnlGST2 != null) pnlGST2.Visible = true;
                        if (pnlGST3 != null) pnlGST3.Visible = true;
                    }
                }
                Label lblNetAmntMinusExtra = e.Item.FindControl("lblNetAmntMinusExtra") as Label;
                lblExtraAmnt.Text = string.Format("{0:0,0.00}", (ExtraAMnt));
                lblNetAmnt.Text = string.Format("{0:0,0.00}", (dtotal));
                lblNetAmntMinusExtra.Text = (Convert.ToDouble(lblNetAmnt.Text == "" ? "0" : lblNetAmnt.Text) - Convert.ToDouble(lblExtraAmnt.Text == "" ? "0" : lblExtraAmnt.Text)).ToString();
            }
        }

        //Upadhyay #GST
        public void GetGSTType(string date)
        {
            if (date != "")
            {
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
                return gstDate.ToString("dd-MM-yyyy");
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

    }
}