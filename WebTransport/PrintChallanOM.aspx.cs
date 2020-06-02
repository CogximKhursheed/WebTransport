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
    public partial class PrintChallanOM: System.Web.UI.Page
    {

        double TotalFright = 0, TotalLabour = 0, TotalQty = 0, Total = 0, TotalSTax = 0, TotalSBTaxAmnt = 0, TotalKKTaxAmnt = 0, TotalInvAmount = 0, TotalInvWeight = 0, TotQty, TotShortQty = 0;
        double dqty = 0, drate = 0, dfreight = 0, dshortage = 0, dtotal = 0;
        double dDiffShrtge = 0, dGrossShrtgeAmnt = 0, dNetShrtAmnt = 0, dTotShrtAmnt = 0, drecd = 0, ddis = 0, damnt = 0;
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0, dNetcommsn = 0; string StrPANNo = "",grdate=""; double dfWithoutWagesAmnt = 0; double dfWagesAmnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("LogOut.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]) != true && string.IsNullOrEmpty(Request.QueryString["P"]) != true)
                {
                    try
                    {
                        PrintChallan(Convert.ToInt64(Request.QueryString["q"].ToString()), Convert.ToString(Request.QueryString["P"].ToString()));
                      
                    }
                    catch (Exception Exe)
                    {

                    }
                }
            }

        }
        double totaltds = 0;
        private void PrintChallan(Int64 ChlnHeadIdno, string Grtype)
        {
            Repeater obj = new Repeater();

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string TinNo = ""; string  strcompdesc = "";//string ServTaxNo = ""; 
            string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            strcompdesc =Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            lblCompanyname.Text = CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (strcompdesc == "")
            {
                lblcompdesc.Visible = false;
            }
            else
            {
                lblcompdesc.Text = Convert.ToString(strcompdesc.Trim().Replace("@", "<br/>") + "<br/>");
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

            #endregion
           //string strOmobile = "", pan = "", chasisno = "", engineno = "" , model="";
            //string strDmobile = "", strlicenseno="", strvalidupto = "", strmobile = "";

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintHeadOM',@Id='" + ChlnHeadIdno + "',@GrType='" + Grtype + "'");
            dsReport.Tables[0].TableName = "GRPrinthead";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblChlnno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_No"]);
                lblchlnDate.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["Chln_Date"]).ToString("dd-MM-yyyy");
                lblTrckNo.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_No"]);

                //Lorry Details
                lblOwnr.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_Owner"]);
                lblowneraddrss.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Owner_Address"]);
                lbltxtownmobile.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Cont_Mobile"]);
                lbltxtpan.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Pan_No"]);
                lblchasisno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Chassis_no"]);
                lbltxtengineno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Eng_No"]);
                //lbltxtpermit.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0][""]);
                //lbltxtpermitvalid.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"]).ToString("dd-MM-yyyy");
                lbltxtmodel.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Lorry_Make"]);

                //
                //Driver Details

                lbldrivername.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                lbldriverAddress.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Driver_Address"]);
                lbltxtdrvlicenceno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLicnc_NO"]);
                lbltxtvalidupto.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"])) ? "" : Convert.ToDateTime(dsReport.Tables["GRPrinthead"].Rows[0]["DrvLNo_ExpDate"]).ToString("dd-MM-yyyy");
                lblmobtextdriver.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverMobile"]);
               // lbltxtinsured.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                //lbltxtpolicyno.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                //lbltxtinsvalidupto.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["DriverName_Eng"]);
                


                valuelblAdvanceAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Adv_Amnt"]));
               // valuelblcmmnsn.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Commsn_Amnt"]));
                //lblDieselAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Diesel_Amnt"]));
                valueLblTdsAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["TDSTax_Amnt"]));
                valuelblnetTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrinthead"].Rows[0]["Net_Amnt"]));
                totaltds = Convert.ToDouble(valueLblTdsAmnt.Text);
                // lbltxtdelivery.Text = Convert.ToString(dsReport.Tables["GRPrinthead"].Rows[0]["Delvry_Instrc"]);


            }
            DataSet dsReportDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spChlnBookng] @ACTION='SelectPrintDetlOM',@Id='" + ChlnHeadIdno + "'");
            dsReportDetl.Tables[0].TableName = "GRPrintdetl";
            if (dsReportDetl != null && dsReportDetl.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = dsReportDetl;
                Repeater1.DataBind();
                lblPrintHeadng.Text = "FREIGHT CUM TRANSIT CHALLAN";
            }

        }
       
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //  gives the sum in string Total.                 
                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                dfWithoutWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "WithoutUnloading_Amnt"));
                dfWagesAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Wages_Amnt"));
                grdate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "GRRet_Date"))) ? "" : Convert.ToDateTime(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "GRRet_Date"))).ToString("dd-MM-yyyy");
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //The following label displays the total
                Label lblTotalAmnt = (Label)e.Item.FindControl("lblTotalAmnt");
                Label lbltotalWeight = (Label)e.Item.FindControl("lbltotalWeight");
                Label lbltotalqty = (Label)e.Item.FindControl("lbltotalqty");
                Label lblWagesAmnt = (Label)e.Item.FindControl("lblWagesAmnt");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lbltotalWeight.Text = dtotwght.ToString("N2");
                lbltotalqty.Text = dqtnty.ToString();
                lblAmount.Text = dfWithoutWagesAmnt.ToString("N2");
                lblWagesAmnt.Text = dfWagesAmnt.ToString("N2");
                //

                lblgrdate.Text = grdate;
                lblfreightamount.Text = dfWithoutWagesAmnt.ToString("N2");
                lbltotalweight.Text = dtotwght.ToString("N2");
                lblhamali.Text = dfWagesAmnt.ToString("N2");
                lbltotalfreight.Text = ((dfWithoutWagesAmnt) - (totaltds)).ToString("N2");
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
                   // imgjkcement.Visible = true;
                    byte[] img = obj1.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    hideimgvalue.Value = "data:image/png;base64," + base64String;
                  // imgjkcement.ImageUrl = hideimgvalue.Value;
                }
            }
            else
            {
                //imgjkcement.Visible = false;
            }
        }
    }
}