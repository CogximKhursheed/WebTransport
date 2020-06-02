using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using System.Web;

namespace WebTransport.DAL
{
    public class CustomTripSheetDAL
    {
        string sqlSTR = string.Empty;
        public List<LaneMaster> BindLane()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<LaneMaster> objLaneMaster = new List<LaneMaster>();

                objLaneMaster = (from obj in db.LaneMasters
                                 orderby obj.Lane_Name
                                 select obj).ToList();

                return objLaneMaster;
            }
        }

        public Int64 GetTripNo(Int32 yearId, Int32 CityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Trip_No),0) + 1 AS MAXID FROM tblManualTripHead WHERE YEAR_IDNO='" + yearId + "'  AND BaseCity_Idno=" + CityIdno;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public Int64 InsertTripSheet(Int64 intTripNo, DateTime dtTripDate, Int32 intBaseCityIdno, Int32 TruckNoIdno, Int32 intPartyIDno, Int32 intLaneIdno, String strDriverName, Int64 intDriverNo, String VehicleSize, String strStartKM, String strEndKM, String strTotalKM, Int32 intYear_Idno,
            Double DSLQty, Double DSLRate, Double DSLAmt, Double Cash, Double DSLCardAmt, String DSLCardRate, String DSLCardName, Double Toll, Double Dihadi, Double FoodExp, Double Repair,
            Double ExDSLAmt, Double ExDSLLtr, Double TotalDSLQty, Double TotalDSLAmt, Double Milage, Double AdvinDriver, Double Other, String strRemark, Double TotalAmt)
        {
            Int64 TripIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblManualTripHead objTripHead = db.tblManualTripHeads.Where(t => t.Trip_No == intTripNo && t.BaseCity_Idno == intBaseCityIdno && t.Year_Idno == intYear_Idno).FirstOrDefault();
                    if (objTripHead == null)
                    {
                        objTripHead = new tblManualTripHead();
                        objTripHead.Trip_No = intTripNo;
                        objTripHead.Trip_Date = dtTripDate;
                        objTripHead.BaseCity_Idno = intBaseCityIdno;
                        objTripHead.Truck_Idno = TruckNoIdno;
                        objTripHead.Party_Idno = intPartyIDno;
                        objTripHead.Lane_Idno = intLaneIdno;
                        objTripHead.Driver_Name = strDriverName;
                        objTripHead.Driver_No = intDriverNo;
                        objTripHead.Vehicle_Size = VehicleSize;
                        objTripHead.StartKms = strStartKM;
                        objTripHead.EndKms = strEndKM;
                        objTripHead.TotalKms = strTotalKM;
                        objTripHead.Year_Idno = intYear_Idno;
                        objTripHead.DSL_Qty = DSLQty;
                        objTripHead.DSL_Rate = DSLRate;
                        objTripHead.DSL_Amt = DSLAmt;
                        objTripHead.Cash = Cash;
                        objTripHead.DSL_Card_Amt = DSLCardAmt;
                        objTripHead.DSL_Card_Rate = DSLCardRate;
                        objTripHead.DSL_Card_Name = DSLCardName;
                        objTripHead.Toll = Toll;
                        objTripHead.Dihadi = Dihadi;
                        objTripHead.Food_Exp = FoodExp;
                        objTripHead.Repair = Repair;
                        objTripHead.Ex_DSL_Amt = ExDSLAmt;
                        objTripHead.Ex_DSL_Ltr = ExDSLLtr;
                        objTripHead.Total_DSL_Qty = TotalDSLQty;
                        objTripHead.Total_DSL_Amt = TotalDSLAmt;
                        objTripHead.Milage = Milage;
                        objTripHead.Adv_in_Driver = AdvinDriver;
                        objTripHead.Other = Other;
                        objTripHead.Remark = strRemark;
                        objTripHead.Total_Amt = TotalAmt;


                        db.tblManualTripHeads.AddObject(objTripHead);
                        db.SaveChanges();
                        TripIdno = objTripHead.Trip_Idno;
                    }
                    else
                    {
                        TripIdno = -1;
                    }
                }
                catch (Exception Ex)
                {
                    TripIdno = 0;
                }
            }
            return TripIdno;
        }
        public Int64 UpdateTripSheet(Int64 intTripIdno, Int64 intTripNo, DateTime dtTripDate, Int32 intBaseCityIdno, Int32 TruckNoIdno, Int32 intPartyIDno, Int32 intLaneIdno, String strDriverName, Int64 intDriverNo, String VehicleSize, String strStartKM, String strEndKM, String strTotalKM, Int32 intYear_Idno,
          Double DSLQty, Double DSLRate, Double DSLAmt, Double Cash, Double DSLCardAmt, String DSLCardRate, String DSLCardName, Double Toll, Double Dihadi, Double FoodExp, Double Repair,
          Double ExDSLAmt, Double ExDSLLtr, Double TotalDSLQty, Double TotalDSLAmt, Double Milage, Double AdvinDriver, Double Other, String strRemark, Double TotalAmt)
        {
            Int64 TripIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblManualTripHead objTripHead = db.tblManualTripHeads.Where(t => t.Trip_No == intTripNo && t.BaseCity_Idno == intBaseCityIdno).FirstOrDefault();
                    if (objTripHead != null)
                    {
                        objTripHead.Trip_No = intTripNo;
                        objTripHead.Trip_Date = dtTripDate;
                        objTripHead.BaseCity_Idno = intBaseCityIdno;
                        objTripHead.Truck_Idno = TruckNoIdno;
                        objTripHead.Party_Idno = intPartyIDno;
                        objTripHead.Lane_Idno = intLaneIdno;
                        objTripHead.Driver_Name = strDriverName;
                        objTripHead.Driver_No = intDriverNo;
                        objTripHead.Vehicle_Size = VehicleSize;
                        objTripHead.StartKms = strStartKM;
                        objTripHead.EndKms = strEndKM;
                        objTripHead.TotalKms = strTotalKM;
                        objTripHead.Year_Idno = intYear_Idno;
                        objTripHead.DSL_Qty = DSLQty;
                        objTripHead.DSL_Rate = DSLRate;
                        objTripHead.DSL_Amt = DSLAmt;
                        objTripHead.Cash = Cash;
                        objTripHead.DSL_Card_Amt = DSLCardAmt;
                        objTripHead.DSL_Card_Rate = DSLCardRate;
                        objTripHead.DSL_Card_Name = DSLCardName;
                        objTripHead.Toll = Toll;
                        objTripHead.Dihadi = Dihadi;
                        objTripHead.Food_Exp = FoodExp;
                        objTripHead.Repair = Repair;
                        objTripHead.Ex_DSL_Amt = ExDSLAmt;
                        objTripHead.Ex_DSL_Ltr = ExDSLLtr;
                        objTripHead.Total_DSL_Qty = TotalDSLQty;
                        objTripHead.Total_DSL_Amt = TotalDSLAmt;
                        objTripHead.Milage = Milage;
                        objTripHead.Adv_in_Driver = AdvinDriver;
                        objTripHead.Other = Other;
                        objTripHead.Remark = strRemark;
                        objTripHead.Total_Amt = TotalAmt;
                        db.SaveChanges();
                        TripIdno = objTripHead.Trip_Idno;
                    }
                    else
                    {
                        TripIdno = -1;
                    }
                }
                catch (Exception Ex)
                {
                    TripIdno = 0;
                }
            }
            return TripIdno;
        }

        public IList SelectTrip(int TripNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int sender, Int32 yearidno, Int64 LaneIdno, Int64 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //var lst = db.tblManualTripHeads.Where(x=> x.Trip_No == TripNo && x.Trip_Date >= dtfrom && x.Trip_Date <= dtto && x.BaseCity_Idno == cityfrom && x.Party_Idno == sender && x.Year_Idno == yearidno).ToList();
                var lst = (from mt in db.tblManualTripHeads
                           join am in db.AcntMasts on mt.Party_Idno equals am.Acnt_Idno
                           join cm in db.tblCityMasters on mt.BaseCity_Idno equals cm.City_Idno
                           join lm in db.LorryMasts on mt.Truck_Idno equals lm.Lorry_Idno
                           join lane in db.LaneMasters on mt.Lane_Idno equals lane.Lane_Idno
                           join loc in db.tblCityMasters on mt.BaseCity_Idno equals loc.City_Idno
                           where mt.Year_Idno == yearidno
                           select new
                           {
                               mt.Party_Idno,
                               mt.BaseCity_Idno,
                               mt.Trip_Idno,
                               mt.Trip_No,
                               Pref_No = loc.City_Abbr,
                               mt.Trip_Date,
                               mt.Milage,
                               mt.Driver_Name,
                               mt.EndKms,
                               mt.StartKms,
                               mt.TotalKms,
                               mt.Total_Amt,
                               mt.Freight_Amnt,
                               mt.Gross_Weight,
                               mt.Item_Name,
                               mt.NetTrip_Profit,
                               mt.Quantity,
                               lm.Lorry_No,
                               am.Acnt_Name,
                               cm.City_Name,
                               mt.Lane_Idno,
                               lane.Lane_Name,
                               mt.Driver_No,
                               mt.Vehicle_Size,
                               mt.DSL_Qty,
                               mt.DSL_Rate,
                               mt.DSL_Amt,
                               mt.Cash,
                               mt.DSL_Card_Amt,
                               mt.DSL_Card_Name,
                               DSL_Card_Number = mt.DSL_Card_Rate,
                               mt.Toll,
                               Wages = mt.Dihadi,
                               mt.Food_Exp,
                               mt.Repair,
                               mt.Ex_DSL_Amt,
                               mt.Ex_DSL_Ltr,
                               mt.Total_DSL_Amt,
                               mt.Total_DSL_Qty,
                               mt.Adv_in_Driver,
                               mt.Other,
                               mt.Remark,
                               mt.Truck_Idno
                           }).ToList();
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Trip_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Trip_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityfrom != 0)
                {
                    lst = lst.Where(x => x.BaseCity_Idno == cityfrom).ToList();
                }
                if (sender != 0)
                {
                    lst = lst.Where(x => x.Party_Idno == sender).ToList();
                }
                if (TripNo != 0)
                {
                    lst = lst.Where(x => x.Trip_No == TripNo).ToList();
                }
                if (LaneIdno != 0)
                {
                    lst = lst.Where(x => x.Lane_Idno == LaneIdno).ToList();
                }
                if (LorryIdno != 0)
                {
                    lst = lst.Where(x => x.Truck_Idno == LorryIdno).ToList();
                }
                return lst;
            }
        }


        public tblManualTripHead GetTripSheet(long TripIdno)
        {
            int value = 0;
            tblManualTripHead objTrip = new tblManualTripHead();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                objTrip = db.tblManualTripHeads.Where(t => t.Trip_Idno == TripIdno).FirstOrDefault();
            }
            return objTrip;
        }
        public int DeleteTrip(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblManualTripHead qth = db.tblManualTripHeads.Where(h => h.Trip_Idno == HeadId).FirstOrDefault();
                List<TblGrDetl> qtd = db.TblGrDetls.Where(d => d.GrHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    db.tblManualTripHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public tblCityMaster GEtLocationDetail(long LocIdno)
        {
            int value = 0;
            tblCityMaster objTrip = new tblCityMaster();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                objTrip = db.tblCityMasters.Where(t => t.City_Idno == LocIdno).FirstOrDefault();
            }
            return objTrip;
        }

        public Int64 GetMaxId(Int32 yearId, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT Trip_Idno AS MAXID FROM tblManualTripHead WHERE YEAR_IDNO=" + yearId + " Order by Trip_Idno desc";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
    }
}
