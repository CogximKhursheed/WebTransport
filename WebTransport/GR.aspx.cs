using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class GR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"].ToString()))
                    PrintGRPrep(0);
                else
                    PrintGRPrep(Convert.ToInt64(Request.QueryString["q"]));              
               hidPages.Value = string.IsNullOrEmpty(Request.QueryString["P"].ToString()) ? "1" :Convert.ToString(Request.QueryString["P"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
           
        }

        private void PrintGRPrep(Int64 GRHeadIdno)
        {
            GRPrepDAL obj1 = new GRPrepDAL();
            if (GRHeadIdno != 0)
            {
                //DataSet ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrintOM',@Id='" + GRHeadIdno + "'");
                DataSet ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
                ds.Tables[0].TableName = "GRPrint";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    lblTexInvoice.Text =  Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Tax_InvNo"]);
                    lblExciceInvoice.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Exc_InvNo"]);
                    lblGRno.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Gr_No"]);
                    lblGrDate.Text = string.Format("{0:MMMM dd, yyyy}", Convert.ToDateTime(ds.Tables["GRPrint"].Rows[0]["Gr_Date"]));//.ToString("dd-MM-yyyy");
                    lblOrderNO.Text = ds.Tables["GRPrint"].Rows[0]["Ordr_No"].ToString();

                    lblConsignorName1.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Sender"]);// ds.Tables["GRPrint"].Rows[0]["ConsigName"].ToString();
                

                    lblConsigneeName1.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Receiver"]);
                    lblConsigneeAddress1.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                    lblDeliverCity.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["To_City"]);
                    lblDeliverPlace.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                    lblDeliverDistrict.Text = "";

                    lblLorryNo.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                    lblQty.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Qty"]); //Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Unit"]);
                    lblWeight.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Tot_Weght"]);
                    lblItemName.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Item_Modl"]);

                    lblRemark.Text = Convert.ToString(ds.Tables["GRPrint"].Rows[0]["Remark"]);                   
                    lblTotalAMT.Text = string.Format("{0:0,0.00}", Convert.ToDouble(ds.Tables["GRPrint"].Rows[0]["Net_Amnt"]));
                    lblAdvance.Text = "";
                    lblFuelPayment.Text = "";
                    lblTotAdvance.Text = "";


                    lblLorryOwner.Text = "";
                    lblLorryAddress.Text = "";
                    lblLorryDriver.Text = "";




                }
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alearMessage", "alert('Wrong GR No. please correct !')", true);
            }
        }
    }
}