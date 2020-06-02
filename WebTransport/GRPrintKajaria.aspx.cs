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
    public partial class GRPrintKajaria : System.Web.UI.Page
    {
        public String GSTIdno = String.Empty;
        public String SGSTPer = String.Empty;
        public String CGSTPer = String.Empty;
        public String IGSTPer = String.Empty;
        public String UGSTPer = String.Empty;
        public String SGSTAmt = String.Empty;
        public String CGSTAmt = String.Empty;
        public String IGSTAmt = String.Empty;
        public String UGSTAmt = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Int64 GRIdno = Convert.ToInt64(Request.QueryString["q"] == null ? "0" : Request.QueryString["q"].ToString());
            PrintGRPrep(GRIdno);
        }

        private void PrintGRPrep(Int64 GRHeadIdno)
        {
            Repeater obj = new Repeater();

            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();

            lblConsigneeGSTIN.Text = "'" + hiduserpref.Terms + "'";
            lblConsigneeGSTIN.Text = hiduserpref.Terms1;

            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            GRPrepRetailerDAL objGRRet = new GRPrepRetailerDAL();
            DataTable dt = objGRRet.SelectGRPrintKajaria(ApplicationFunction.ConnectionString(), GRHeadIdno);
            //DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectKajariaPrint',@Id='" + GRHeadIdno + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                lblGRNo.Text = Convert.ToString(dt.Rows[0]["GrRet_No"]);
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["GrRet_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dt.Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dt.Rows[0]["To_City"]);
                lblConsigneeName.Text = Convert.ToString(dt.Rows[0]["Sender"]);
                lblConsigneeGSTIN.Text = Convert.ToString(dt.Rows[0]["SenderGSTIN"]);

                lblConsignorName.Text = Convert.ToString(dt.Rows[0]["Receiver"]);
                lblConsignorGSTIN.Text = Convert.ToString(dt.Rows[0]["ReceiverGSTIN"]);

                lblRemark.Text = Convert.ToString(dt.Rows[0]["Remark"]);
                lblDelvryPlace.Text = Convert.ToString(dt.Rows[0]["Delivery_Place"]);
                lblValue.Text = Convert.ToString(dt.Rows[0]["Value"]);
                lblRefInvNo.Text = Convert.ToString(dt.Rows[0]["Ref_No"]);

                lblFreight.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["Gross_Amnt"]));
                lblSurcharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["Stcharg_Amnt"]));
                lblLabour.Text = "0";
                lblPickUp.Text = "0";
                lblLocalFreight.Text = "0";
                lblBilty.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["Bilty_Amnt"]));
                lblDoorDel.Text = "0";
                lblTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["Total_Amnt"]));
                lblValue.Text = "0";
                GSTIdno = dt.Rows[0]["GST_Idno"].ToString();
                if (GSTIdno != String.Empty && GSTIdno == "1")
                {
                    SGSTPer = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["SGST_per"]));
                    CGSTPer = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["CGST_Per"]));
                    SGSTAmt = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["SGST_Amt"]));
                    CGSTAmt = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["CGST_Amt"]));
                    lblGST.Text = (Convert.ToDouble(SGSTAmt == "" ? "0" : SGSTAmt) + Convert.ToDouble(CGSTAmt == "" ? "0" : CGSTAmt)).ToString("N2");
                }
                if (GSTIdno != String.Empty && GSTIdno == "2")
                {
                    IGSTPer = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["IGST_Per"]));
                    IGSTAmt = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["IGST_Amt"]));
                    lblGST.Text = (Convert.ToDouble(IGSTAmt == "" ? "0" : IGSTAmt)).ToString("N2");
                }
                if (GSTIdno != String.Empty && GSTIdno == "3")
                {
                    UGSTPer = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["UGST_Per"]));
                    UGSTAmt = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["UGST_Amt"]));
                    lblGST.Text = (Convert.ToDouble(UGSTAmt == "" ? "0" : UGSTAmt)).ToString("N2");
                }

                lblGrossTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dt.Rows[0]["Gross_Amnt"]));
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }
    }
}