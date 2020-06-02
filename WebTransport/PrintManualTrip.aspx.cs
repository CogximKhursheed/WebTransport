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
using System.Linq;

namespace WebTransport
{
    public partial class PrintManualTrip : System.Web.UI.Page
    {
        public Int32 PrintType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PrintType"] != null)
            {
                if (Request.QueryString["PrintType"] == "1")
                {
                    CallPrint();
                    PrintType = 1;
                }
                else
                {
                    CallPrint();
                    PrintType = 2;
                }
            }
        }

        private void CallPrint()
        {
            try
            {
                DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
                lblAddress.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
                //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
                lblPhoneNo.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                //ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
                lblPanNo.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["PAN_No"]);
                lblEmail.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
                lblCompDescr.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);

                lblTripNo.Text = Request.QueryString["TripNo"].ToString();
                lblTripDate.Text = ((Request.QueryString["TripDate"] == null || Request.QueryString["TripDate"] == "") ? "" : Convert.ToDateTime(Request.QueryString["TripDate"]).ToString("dd-MM-yyyy")).ToString();
                //lblCompFromCity.Text = Request.QueryString["CompFromCity"].ToString();
                lblTruckNo.Text = GetTruckNo(Request.QueryString["TruckNo"].ToString());
                lblSender.Text = GetUserName(Request.QueryString["Sender"].ToString());
                lblFromCity.Text = GetCityName(Request.QueryString["FromCity"].ToString());
                lblToCity.Text = GetCityName(Request.QueryString["ToCity"].ToString());
                lblDriverName.Text = Request.QueryString["DriverName"].ToString();
                lblStartKms.Text = Request.QueryString["StartKms"].ToString();
                lblEndKms.Text = Request.QueryString["EndKms"].ToString();
                //lblDateRange.Text = Request.QueryString["DateRange"].ToString();
                lblItemName.Text = Request.QueryString["ItemName"].ToString();
                lblItemSize.Text = Request.QueryString["ItemSize"].ToString();
                //lblRateType.Text = Request.QueryString["RateType"].ToString();
                lblQuantity.Text = Request.QueryString["Quantity"].ToString();
                lblGweight.Text = Request.QueryString["Gweight"].ToString();
                //lblAweight .Text = Request.QueryString["Aweight"].ToString();
                lblrate.Text = Request.QueryString["rate"].ToString();
                lblAdvance.Text = Request.QueryString["Advance"].ToString();
                lblCommission.Text = Request.QueryString["Commission"].ToString();
                lblTotalPartyAdv.Text = Request.QueryString["TotalPartyAdv"].ToString();
                lblRTOChallan.Text = Request.QueryString["RTOChallan"].ToString();
                lblDetention.Text = Request.QueryString["Detention"].ToString();
                lblTotalAmount.Text = Request.QueryString["TotalAmount"].ToString();
                lblTotalFreight.Text = Request.QueryString["TotalFreight"].ToString();
                //lblReceived.Text = Request.QueryString["Received"].ToString();
                //lblRecType .Text = Request.QueryString["RecType"].ToString();
                lblTotalPartyBalance.Text = Request.QueryString["TotalPartyBalance"].ToString();
                lblDriver.Text = Request.QueryString["Driver"].ToString();
                lblDiesel.Text = Request.QueryString["Diesel"].ToString();
                lblDriverAc.Text = Request.QueryString["DriverAc"].ToString();
                lblTotalVehAdv.Text = Request.QueryString["TotalVehAdv"].ToString();
                lblNetTripProfit.Text = Request.QueryString["NetTripProfit"].ToString();
            }
            catch(Exception ex)
            {

            }
        }

        private string GetUserName(string AcntId)
        {
            Int32 AId = Convert.ToInt32(AcntId == "" ? "0" : AcntId);
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string AcntName = (from am in db.AcntMasts where am.Acnt_Idno == AId select am.Acnt_Name).SingleOrDefault().ToString();
                return AcntName;
            }
        }

        private string GetTruckNo(string LorryId)
        {
            Int32 LId = Convert.ToInt32(LorryId == "" ? "0" : LorryId);
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string LorryNo = (from lm in db.LorryMasts where lm.Lorry_Idno == LId select lm.Lorry_No).SingleOrDefault().ToString();
                return LorryNo;
            }
        }

        private string GetCityName(string CityId)
        {
            Int32 CId = Convert.ToInt32(CityId == "" ? "0" : CityId);
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string CityName = (from cm in db.tblCityMasters where cm.City_Idno == CId select cm.City_Name).SingleOrDefault().ToString();
                return CityName;
            }
        }
    }
}