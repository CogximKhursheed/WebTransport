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
    public partial class AFCGSTInvoice : System.Web.UI.Page
    {
        double Freight = 0, Weight = 0, Rate = 0, TotalAmount, CGST = 0, SGST = 0, IGST = 0;
        DataSet InvDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            string InvID = Request.QueryString["q"];
          //if (string.IsNullOrEmpty(InvID) == false)
          //{
          //    BindDetails(Convert.ToInt64(InvID));
          //}
            if (string.IsNullOrEmpty(Request.QueryString["q"]) != true  && string.IsNullOrEmpty(Request.QueryString["R"]) != true)
            {
                BindDetails(Convert.ToInt64(InvID));
                 if (Request.QueryString["R"] == "2")
                        header.Visible = false;
                    else
                       header.Visible = true;
            }
        }
        public void CalculateFooter()
        {
            if (InvDetails != null && InvDetails.Tables.Count > 0 && InvDetails.Tables[1].Rows.Count > 0)
            {
                Freight = Freight + Convert.ToDouble(InvDetails.Tables[1].Compute("SUM(Amount)", ""));
                Weight = Weight + Convert.ToDouble(InvDetails.Tables[1].Compute("SUM(Weight)", ""));
                Rate = Rate + Convert.ToDouble(InvDetails.Tables[1].Compute("SUM(Rate)", ""));
                TotalAmount = TotalAmount + Convert.ToDouble(InvDetails.Tables[1].Compute("SUM(TotalAmount)", ""));
            }
        }
       
        public void BindDetails(Int64 InvIDno)
        {
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select ISNULL(cm.Comp_Name,'')Comp_Name,ISNULL(cm.Adress1,'')as Address1,ISNULL(cm.Adress2,'')as Address2,ISNULL(cm.City_Idno,'')as City_Name,ISNULL(cm.State_Idno,'')as State_Name,ISNULL(cm.Phone_Off,'')as Phone_Off,ISNULL(cm.Pan_No,'')as PAN_No,ISNULL(cm.CompGSTIN_No,'')as GST_No,ISNULL(cm.Pin_No,'')as Pin_No from tblcompmast cm ");
            # region Company Details
            if (CompDetl != null && CompDetl.Tables.Count > 0 && CompDetl.Tables[0].Rows.Count > 0)
            {
                string CompName = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
                string Add1 = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Address1"]);
                string Add2 = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Address2"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Address2"]);
                string PhNo = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                string City =string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
                string State = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]);
                string Pin_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
                string GST_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["GST_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["GST_No"]); ;
                string PAN_No = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["PAN_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["PAN_No"]);


                if (string.IsNullOrEmpty(Add2) == false)
                {
                    Add1 = Add1 + "," + Add2;
                }
                if (string.IsNullOrEmpty(City) == false)
                {
                    Add1 = Add1 + "," + City;
                }
                if (string.IsNullOrEmpty(State) == false)
                {
                    Add1 = Add1 + "," + State;
                }
                if (string.IsNullOrEmpty(Pin_No) == false)
                {
                    Add1 = Add1 + "," + Pin_No;
                }
                if (string.IsNullOrEmpty(PhNo) == false)
                {
                    PhNo = "PHONE:- " + PhNo;
                }
                if (string.IsNullOrEmpty(PAN_No) == false)
                {
                    PAN_No = "PAN:- " + PAN_No;
                }
                if (string.IsNullOrEmpty(GST_No) == false)
                {
                    GST_No = "GST No:- " + GST_No;
                }
                lblCompName.Text = CompName;
                lblName.Text = "For :- " + CompName;
                lblCompAddress.Text = Add1;
                lblCompPhone.Text = PhNo;
                lblGST.Text = GST_No;
                lblPAN.Text = PAN_No;
            }

            #endregion
            string action = String.Empty;
            if (Request.QueryString["P"] == "13")
            {
                action = "JobnerGST";
                lblJobnerPayableBy.Visible = true;
             }
            else
            {
                action = "RASGST";
                lblJobnerPayableBy.Visible = false;
            }
            InvDetails = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvoicePrintAFC] @Action = " + action + ",@InvIDno='" + InvIDno + "'");
            if (InvDetails != null && InvDetails.Tables.Count > 0 && InvDetails.Tables[0].Rows.Count>0)
            {
                string PartyName = Convert.ToString(InvDetails.Tables[0].Rows[0]["Sender"]);
                string PartyAdd1 = Convert.ToString(InvDetails.Tables[0].Rows[0]["Address1"]);
                string PartyAdd2 = Convert.ToString(InvDetails.Tables[0].Rows[0]["Address2"]);

                string PartyCity = Convert.ToString(InvDetails.Tables[0].Rows[0]["City_Name"]);
                string PartyState = Convert.ToString(InvDetails.Tables[0].Rows[0]["State_Name"]);
                string PartyGST_No = Convert.ToString(InvDetails.Tables[0].Rows[0]["GST_No"]);
                string PartyPAN_No = Convert.ToString(InvDetails.Tables[0].Rows[0]["Pan_No"]);
                string Inv_No =  Convert.ToString(InvDetails.Tables[0].Rows[0]["Inv_Prefix"]) + Convert.ToString(InvDetails.Tables[0].Rows[0]["Inv_No"]);
                string Inv_Date = Convert.ToDateTime(InvDetails.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy");

                if (string.IsNullOrEmpty(PartyAdd2) == false)
                {
                    PartyAdd1 = PartyAdd1 + "," + PartyAdd2;
                }
                if (string.IsNullOrEmpty(PartyCity) == false)
                {
                    PartyAdd1 = PartyAdd1 + "," + PartyCity;
                }
                if (string.IsNullOrEmpty(PartyState) == false)
                {
                    PartyAdd1 = PartyAdd1 + "," + PartyState;
                }
                if (string.IsNullOrEmpty(PartyPAN_No) == false)
                {
                    PartyPAN_No = "PAN:- " + PartyPAN_No;
                }
                if (string.IsNullOrEmpty(PartyGST_No) == false)
                {
                    PartyGST_No = "GST No:- " + PartyGST_No;
                }
                GetState(Convert.ToString(InvDetails.Tables[0].Rows[0]["State_Name"]));
                //lblSenderName.Text = "To " + PartyName;
                //lblSenderAddress.Text = PartyAdd1;
                //lblSenderPAN.Text = PartyPAN_No;
                //lblSenderGST.Text = PartyGST_No;
                lblBillDate.Text = Inv_Date;
                lblBillNo.Text = Inv_No;
            }

            if (InvDetails != null && InvDetails.Tables.Count > 0 && InvDetails.Tables[1].Rows.Count > 0)
            {
                grdMain.DataSource = InvDetails.Tables[1];
                grdMain.DataBind();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }
        private void GetState(string StateName)
        {
            if (StateName != "")
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var stateDetl = db.tblStateMasters.Where(x => x.State_Name == StateName).FirstOrDefault();
                    if (stateDetl != null)
                    {
                        lblSupplyStateName.Text = stateDetl.State_Name;
                        lblSupplyStateId.Text = stateDetl.GSTState_Code.ToString();
                    }
                }
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int row = e.Row.RowIndex;
                Label lblCheckDupGrNo = (Label)e.Row.FindControl("lblCheckDupGrNo");
                Label lblSGST = (Label)e.Row.FindControl("lblSGST");
                Label lblCGST = (Label)e.Row.FindControl("lblCGST");
                Label lblIGST = (Label)e.Row.FindControl("lblIGST");
                if (hidCheckDupGRNo.Value == null || hidCheckDupGRNo.Value == "")
                {
                    lblSGST.Text = (Convert.ToDouble(lblSGST.Text == "" ? "" : lblSGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["SGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["SGST_Amt"])).ToString();
                    lblCGST.Text = (Convert.ToDouble(lblCGST.Text == "" ? "" : lblCGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["CGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["CGST_Amt"])).ToString();
                    lblIGST.Text = (Convert.ToDouble(lblIGST.Text == "" ? "" : lblIGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["IGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["IGST_Amt"])).ToString();
                }
                else
                {
                    if (lblCheckDupGrNo != null && lblCheckDupGrNo.Text != hidCheckDupGRNo.Value)
                    {
                        lblSGST.Text = (Convert.ToDouble(lblSGST.Text == "" ? "" : lblSGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["SGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["SGST_Amt"])).ToString();
                        lblCGST.Text = (Convert.ToDouble(lblCGST.Text == "" ? "" : lblCGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["CGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["CGST_Amt"])).ToString();
                        lblIGST.Text = (Convert.ToDouble(lblIGST.Text == "" ? "" : lblIGST.Text) + Convert.ToDouble(InvDetails.Tables[1].Rows[row]["IGST_Amt"] == null ? "0.00" : InvDetails.Tables[1].Rows[row]["IGST_Amt"])).ToString();
                    }
                    else
                    {
                        lblSGST.Text = "0.00";
                        lblCGST.Text = "0.00";
                        lblIGST.Text = "0.00";
                    }
                }
                hidCheckDupGRNo.Value = lblCheckDupGrNo.Text;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                this.CalculateFooter();
                Label lblTotRate = (Label)e.Row.FindControl("lblTotRate");
                Label lblTotWeight = (Label)e.Row.FindControl("lblTotWeight");
                Label lblTotAmount = (Label)e.Row.FindControl("lblTotAmount");
                //Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
                Label lblTotalCGST = (Label)e.Row.FindControl("lblTotalCGST");
                Label lblTotalSGST = (Label)e.Row.FindControl("lblTotalSGST");
                Label lblTotalIGST = (Label)e.Row.FindControl("lblTotalIGST");

                lblTotRate.Text = Convert.ToDouble(Rate).ToString("N2");
                lblTotWeight.Text = Convert.ToDouble(Weight).ToString("N2");
                lblTotAmount.Text = Convert.ToDouble(Freight).ToString("N2");
                //lblTotalAmount.Text = Convert.ToDouble(TotalAmount).ToString("N2");
                lblTotalCGST.Text = Convert.ToDouble(CGST).ToString("N2");
                lblTotalSGST.Text = Convert.ToDouble(SGST).ToString("N2");
                lblTotalIGST.Text = Convert.ToDouble(IGST).ToString("N2");
            }
        }
    }
}