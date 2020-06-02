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
    public class ManualTripSheetDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region FOR DML STATEMENTS i.e INSERT/DELETE/UPDATE
        public Int64 InsertTripSheet(Int64 intTripNo, DateTime dtTripDate, Int32 intBaseCityIdno, Int32 TruckNoIdno, Int32 intPartyIDno, Int32 intFromcityIDno, Int32 intTocityIDno, String strDriverName, String strStartKM, String strEndKM, Int32 intYear_Idno,
            String ItemName, String ItemSize, Int32 Rate_Type, Double Qty, Double GrossWeight, Double ActualWeight, Double ItemRate, Double PartyAdvance, Double PartyCommission, Double TotalPartyAdv, Double RTOChallan, Double Detention, Double Freight_Amnt, Double GrossFreight_Amnt,
            Double Received_Amnt, Int32 Rec_Type, Double TotalParty_Bal, Double DriverCash_Amnt, Double Diesel_Amnt, Double DriverAC_Amnt, Double TotalVev_Amnt, Double NetTrip_Profit, Boolean FixRate)
        {
            Int64 TripIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblManualTripHead objTripHead = db.tblManualTripHeads.Where(t=> t.Trip_No == intTripNo && t.BaseCity_Idno == intBaseCityIdno).FirstOrDefault();
                    if (objTripHead == null)
                    {
                        objTripHead = new tblManualTripHead();
                        objTripHead.Trip_No = intTripNo;
                        objTripHead.Trip_Date = dtTripDate;
                        objTripHead.BaseCity_Idno = intBaseCityIdno;
                        objTripHead.Truck_Idno = TruckNoIdno;
                        objTripHead.Party_Idno = intPartyIDno;
                        objTripHead.FromCity_idno = intFromcityIDno;
                        objTripHead.ToCity_idno = intTocityIDno;
                        objTripHead.Driver_Name = strDriverName;
                        objTripHead.StartKms = strStartKM;
                        objTripHead.EndKms = strEndKM;
                        objTripHead.Year_Idno = intYear_Idno;
                        objTripHead.Item_Name = ItemName;
                        objTripHead.Item_Size = ItemSize;
                        objTripHead.Rate_Type = Rate_Type;
                        objTripHead.Quantity = Qty;
                        objTripHead.Gross_Weight = GrossWeight;
                        objTripHead.Actual_Weight = ActualWeight;
                        objTripHead.Item_Rate = ItemRate;
                        objTripHead.Party_Adv = PartyAdvance;
                        objTripHead.Party_Comm = PartyCommission;
                        objTripHead.TotalParty_Adv = TotalPartyAdv;
                        objTripHead.RTO_Chln = RTOChallan;
                        objTripHead.Detention = Detention;
                        objTripHead.Freight_Amnt = Freight_Amnt;
                        objTripHead.GrossFreight_Amnt = GrossFreight_Amnt;
                        objTripHead.Received_Amnt = Received_Amnt;
                        objTripHead.Rec_Type = Rec_Type;
                        objTripHead.TotalParty_Bal = TotalParty_Bal;
                        objTripHead.DriverCash_Amnt = DriverCash_Amnt;
                        objTripHead.Diesel_Amnt = Diesel_Amnt;
                        objTripHead.DriverAC_Amnt = DriverAC_Amnt;
                        objTripHead.TotalVeh_Amnt = TotalVev_Amnt;
                        objTripHead.NetTrip_Profit = NetTrip_Profit;
                        objTripHead.FixRate = FixRate;

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
        public Int64 UpdateTripSheet(Int64 intTripIdno, Int64 intTripNo, DateTime dtTripDate, Int32 intBaseCityIdno, Int32 TruckNoIdno, Int32 intPartyIDno, Int32 intFromcityIDno, Int32 intTocityIDno, String strDriverName, String strStartKM, String strEndKM, Int32 intYear_Idno, 
            String ItemName, String ItemSize, Int32 Rate_Type, Double Qty, Double GrossWeight, Double ActualWeight, Double ItemRate, Double PartyAdvance, Double PartyCommission, Double TotalPartyAdv, Double RTOChallan, Double Detention, Double Freight_Amnt, Double GrossFreight_Amnt, 
            Double Received_Amnt, Int32 Rec_Type, Double TotalParty_Bal, Double DriverCash_Amnt, Double Diesel_Amnt, Double DriverAC_Amnt, Double TotalVev_Amnt, Double NetTrip_Profit, Boolean FixRate)
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
                        objTripHead.FromCity_idno = intFromcityIDno;
                        objTripHead.ToCity_idno = intTocityIDno;
                        objTripHead.Driver_Name = strDriverName;
                        objTripHead.StartKms = strStartKM;
                        objTripHead.EndKms = strEndKM;
                        objTripHead.Year_Idno = intYear_Idno;
                        objTripHead.Item_Name = ItemName;
                        objTripHead.Item_Size = ItemSize;
                        objTripHead.Rate_Type = Rate_Type;
                        objTripHead.Quantity = Qty;
                        objTripHead.Gross_Weight = GrossWeight;
                        objTripHead.Actual_Weight = ActualWeight;
                        objTripHead.Item_Rate = ItemRate;
                        objTripHead.Party_Adv = PartyAdvance;
                        objTripHead.Party_Comm = PartyCommission;
                        objTripHead.TotalParty_Adv = TotalPartyAdv;
                        objTripHead.RTO_Chln = RTOChallan;
                        objTripHead.Detention = Detention;
                        objTripHead.Freight_Amnt = Freight_Amnt;
                        objTripHead.GrossFreight_Amnt = GrossFreight_Amnt;
                        objTripHead.Received_Amnt = Received_Amnt;
                        objTripHead.Rec_Type = Rec_Type;
                        objTripHead.TotalParty_Bal = TotalParty_Bal;
                        objTripHead.DriverCash_Amnt = DriverCash_Amnt;
                        objTripHead.Diesel_Amnt = Diesel_Amnt;
                        objTripHead.DriverAC_Amnt = DriverAC_Amnt;
                        objTripHead.TotalVeh_Amnt = TotalVev_Amnt;
                        objTripHead.NetTrip_Profit = NetTrip_Profit;
                        objTripHead.FixRate = FixRate;
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
        #endregion

        #region SELECT STATEMENTS
        
        public DataTable DtAcntDS(string con)
        {
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno, ISNULL(SwachBharat_Idno,0) SwachBharat_Idno, ISNULL(KrishiKalyan_Idno,0) KrishiKalyan_Idno FROM tblAcntLink";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        
        public IList SelectGR(int GrNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, Int32 yearidno, Int64 UserIdno, string strGrPrefix)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.TblGrHeads
                           join LR in db.LorryMasts on hd.Lorry_Idno equals LR.Lorry_Idno 
                           join cifrom in db.tblCityMasters on hd.From_City equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.To_City equals cito.City_Idno
                           join cidVia in db.tblCityMasters on hd.DelvryPlce_Idno equals cidVia.City_Idno into Cidl 
                           from mappingsCidl in Cidl.DefaultIfEmpty()
                           join cidVia in db.tblCityMasters on hd.Cityvia_Idno equals cidVia.City_Idno into ViaCity 
                           from mappingsViaCity in ViaCity.DefaultIfEmpty()
                           join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           join acntss in db.AcntMasts on hd.Recivr_Idno equals acntss.Acnt_Idno
                           where hd.GR_Frm == "BK"
                           select new
                           {
                               hd.PrefixGr_No,
                               hd.DelvryPlce_Idno,
                               hd.From_City,
                               hd.To_City,
                               hd.Gr_Date,
                               hd.GR_Idno,
                               hd.Gr_No,
                               hd.Sender_Idno,
                               hd.Remark,
                               hd.Shipment_No,
                               GR_Typ = ((hd.GR_Typ == 1) ? "Paid GR" : (hd.GR_Typ == 2) ? "TBB GR" : "To Pay GR"),
                               hd.Recivr_Idno,
                               hd.Gross_Amnt,
                               hd.Net_Amnt,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityVia = mappingsViaCity.City_Name,
                               CityDely = mappingsCidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               Receiver = acntss.Acnt_Name,
                               Year_Idno = hd.Year_Idno,
                               Lorry_No=LR.Lorry_No,
                               Owner_Name= LR.Owner_Name,
                               hd.Tot_KM,
                               Qty = ((from N in db.TblGrDetls  where N.GrHead_Idno == hd.GR_Idno select N.Qty).Sum())
                               //Qty = 1
                               
                           }).ToList();
                if (GrNo > 0)
                {
                    lst = lst.Where(l => l.Gr_No == GrNo).ToList();
                }
                if (strGrPrefix != null && strGrPrefix != "")
                {
                    lst = lst.Where(l => l.PrefixGr_No.Contains(strGrPrefix)).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.To_City == cityto).ToList();
                }
                if (citydely > 0)
                {
                    lst = lst.Where(l => l.DelvryPlce_Idno == citydely).ToList();
                }
                if (sender > 0)
                {
                    lst = lst.Where(l => l.Sender_Idno == sender).ToList();
                }

                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.From_City == cityfrom).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.From_City)).ToList();
                }
                return lst;
            }
        }

        public Int64 GetMaxTripNo(Int32 yearId, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Trip_No),0) + 1 AS MAXID FROM tblManualTripHead WHERE YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public IList SelectTrip(int TripNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int sender, Int32 yearidno, Int64 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //var lst = db.tblManualTripHeads.Where(x=> x.Trip_No == TripNo && x.Trip_Date >= dtfrom && x.Trip_Date <= dtto && x.BaseCity_Idno == cityfrom && x.Party_Idno == sender && x.Year_Idno == yearidno).ToList();
                var lst = (from mt in db.tblManualTripHeads 
                            join am in db.AcntMasts on mt.Party_Idno equals am.Acnt_Idno
                            join cm in db.tblCityMasters on mt.BaseCity_Idno equals cm.City_Idno
                            join lm in db.LorryMasts on mt.Truck_Idno equals lm.Lorry_Idno 
                            where mt.Trip_Date >= dtfrom 
                            && mt.Year_Idno == yearidno select new{
                            mt.Party_Idno,
                            mt.BaseCity_Idno,
                            mt.Trip_Idno,
                            mt.Trip_No,
                            mt.Trip_Date,
                            mt.TotalParty_Adv,
                            mt.TotalParty_Bal,
                            mt.TotalVeh_Amnt,
                            mt.Driver_Name,
                            mt.EndKms,
                            mt.StartKms,
                            mt.Freight_Amnt,
                            mt.Gross_Weight,
                            mt.Item_Name,
                            mt.NetTrip_Profit,
                            mt.Quantity,
                            lm.Lorry_No,
                            am.Acnt_Name,
                            cm.City_Name,
                            mt.Truck_Idno
                            }).ToList();
                if (cityfrom != 0)
                {
                    lst = lst.Where(x => x.BaseCity_Idno == cityfrom).ToList();
                }
                if(sender != 0)
                {
                    lst = lst.Where(x => x.Party_Idno == sender).ToList();
                }
                if (TripNo != 0)
                {
                    lst = lst.Where(x => x.Trip_No == TripNo).ToList();
                }
                if (LorryIdno != 0)
                {
                    lst = lst.Where(x => x.Truck_Idno == LorryIdno).ToList();
                }
                return lst;
            }
        }

        //Upadhyay #GST
        //public double SelectSGSTMaster(DateTime Grdate)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
        //        Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 5 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
        //        if (Max > 0)
        //        {
        //            MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 5 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
        //        }
        //        if (MaxId > 0)
        //        {
        //            TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
        //        }
        //        return TaxRate;
        //    }
        //}

        //Upadhyay #GST
        //public double SelectCGSTMaster(DateTime Grdate)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
        //        Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 6 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
        //        if (Max > 0)
        //        {
        //            MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 6 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
        //        }
        //        if (MaxId > 0)
        //        {
        //            TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
        //        }
        //        return TaxRate;
        //    }
        //}

        //Upadhyay #GST
        //public double SelectIGSTMaster(DateTime Grdate)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
        //        Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 7 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
        //        if (Max > 0)
        //        {
        //            MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 7 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
        //        }
        //        if (MaxId > 0)
        //        {
        //            TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
        //        }
        //        return TaxRate;
        //    }
        //}
        #endregion

        #region FUNCTIONS

        public IList CheckDuplicateGrNo(Int64 intGrNo, Int32 FromCityIdno, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from m in db.TblGrHeads
                           where m.Gr_No == intGrNo && m.Year_Idno == intYearIdno && m.GR_Frm == "BK" && m.From_City == FromCityIdno
                           select new
                           {
                               m.Gr_No,
                           }).ToList();
                return lst;
            }
        }
        public Int64 MaxNo(string GRfrom, Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GR_NO),0) + 1 AS MAXID FROM TBLGRHEAD WHERE GR_Frm='" + GRfrom + "' AND From_City='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public Int64 MaxIdno(string con,Int64 FronCityIdno)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GR_Idno),0)  AS MAXID FROM TBLGRHEAD WHERE FROM_CITY=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        #endregion

    }
}
