using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class FuelSlipDAL
    {
        public Int64 MaxIdno(string con)
        {
            Int64 MaxNo = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(MAX(FuelSlip_Idno),0)  AS MAXID FROM tblFuelSlipHead";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public List<tblCityMaster> BindCityAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objCityMasterList = new List<tblCityMaster>();

                objCityMasterList = (from obj in db.tblCityMasters
                                     where obj.Status == true && obj.AsLocation == true
                                     orderby obj.City_Name
                                     select obj).ToList();
                return objCityMasterList;
            }
        }
        public IList SelectDriver()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.DriverMasts
                        orderby mast.Driver_Name
                        select mast).ToList();
            }
        }
        public LorryMast selectTruckType(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault();
            }
        }
        public tblFuelRateMaster SelectRate(DateTime Date,int AcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from FRM in db.tblFuelRateMasters join AM in db.AcntMasts on FRM.Acnt_Idno equals AM.Acnt_Idno where FRM.FuelRate_Date <= Date && AM.Acnt_Idno == AcntIdno orderby FRM.FuelRate_Date descending select FRM).FirstOrDefault();
            }
        }
        public DataTable GetItemDetails(Int64 Item_Idno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetItemDetails");
            objSqlPara[1] = new SqlParameter("@Item_Idno", Convert.ToInt32(Item_Idno));
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public DataTable GetEffectivePrice(Int64 Pump_Idno,Int64 Item_Idno, string EffDate, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "GetEffectivePrice");
            objSqlPara[1] = new SqlParameter("@Item_Idno", Convert.ToInt32(Item_Idno));
            objSqlPara[2] = new SqlParameter("@Pump_Idno", Convert.ToInt32(Pump_Idno));
            objSqlPara[3] = new SqlParameter("@Date", EffDate.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public IList SelectItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblItemMastPurs
                        where mast.ItemType == 3
                        orderby mast.Item_Name
                        select mast).ToList();
            }
        }
        public IList SelectPCompName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                IList lst = (from cm in db.AcntMasts where cm.Acnt_Type == 10 orderby cm.Acnt_Name ascending select new { Acnt_Idno = cm.Acnt_Idno, Acnt_Name = cm.Acnt_Name }).ToList();
                return lst;
            }
        }
        public tblFuelSlipHead CheckNo(Int32 SLipNo, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from temp in db.tblFuelSlipHeads where temp.FuelSlip_No == SLipNo && temp.Year_Idno== intYearIdno select temp).FirstOrDefault();
            }
        }
        public Int64 Insert(Int64 FuelSlip_No, DateTime? FuelSlip_Date, Int64 Loc_Idno, Int64 Truck_Idno, Int64 Pump_Idno, Int64 Driver_Idno, Int64 Year_Idno, DateTime? Date_Added, double NetAmnt, DataTable dtDetail,string InvoiceNo)
        {
            Int64 FuelSlip_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblFuelSlipHead objFuelSlipHead = db.tblFuelSlipHeads.Where(bill => (bill.FuelSlip_No == FuelSlip_No) && (bill.Year_Idno == Year_Idno) && (bill.Loc_Idno == Loc_Idno)).FirstOrDefault();
                    if (objFuelSlipHead == null)
                    {
                        objFuelSlipHead = new tblFuelSlipHead();
                        objFuelSlipHead.FuelSlip_No = FuelSlip_No;
                        objFuelSlipHead.FuelSlip_Date = FuelSlip_Date;
                        objFuelSlipHead.Loc_Idno = Loc_Idno;
                        objFuelSlipHead.Truck_Idno = Truck_Idno;
                        objFuelSlipHead.Pump_Idno = Pump_Idno;
                        objFuelSlipHead.Driver_Idno = Driver_Idno;
                        objFuelSlipHead.Year_Idno = Year_Idno;
                        objFuelSlipHead.Date_Added = Date_Added;
                        objFuelSlipHead.Net_Amnt = NetAmnt;
                        objFuelSlipHead.Invoice_No = InvoiceNo;
                        db.tblFuelSlipHeads.AddObject(objFuelSlipHead);
                        db.SaveChanges();
                        FuelSlip_Idno = objFuelSlipHead.FuelSlip_Idno;
                        if (FuelSlip_Idno > 0)
                        {
                            foreach (DataRow dr in dtDetail.Rows)
                            {
                                tblFuelSlipDetl objFuelSlipDetl = new tblFuelSlipDetl();
                                objFuelSlipDetl.FuelSlipHead_Idno = FuelSlip_Idno;
                                objFuelSlipDetl.Iteam_Idno = Convert.ToInt64(dr["ItemNameIdno"]);
                                objFuelSlipDetl.Item_Qty = Convert.ToDouble(String.Format("{0:0,0.00}", Convert.ToString(dr["Qty"])));
                                objFuelSlipDetl.Item_Rate= Convert.ToDouble(String.Format("{0:0,0.00}", Convert.ToString(dr["Rate"])));
                                objFuelSlipDetl.Item_Amnt = Convert.ToDouble(String.Format("{0:0,0.00}", Convert.ToString(dr["Amount"])));
                                db.tblFuelSlipDetls.AddObject(objFuelSlipDetl);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return FuelSlip_Idno;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return FuelSlip_Idno;
            }
        }


        public Int64 GetMaxSlipNo(Int64 YearIdno)
        {
            Int64 FuelSlipNo = 0;
            Int64 Count = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    Count = Convert.ToInt64((from cm in db.tblFuelSlipHeads where cm.Year_Idno == YearIdno select cm).Count());
                    if (Count > 0)
                    {
                        FuelSlipNo = Convert.ToInt64((from cm in db.tblFuelSlipHeads where cm.Year_Idno == YearIdno select cm.FuelSlip_No).Max());
                    }
                    else
                    {
                        FuelSlipNo = 0;
                    }
                }
                catch (Exception Ex)
                {
                    FuelSlipNo = 0;
                }
            }
            return FuelSlipNo;
        }
        public Int64 Update(Int64 FuelSlipHead_Idno, Int64 FuelSlip_No, DateTime? FuelSlip_Date, Int64 Loc_Idno, Int64 Truck_Idno, Int64 Pump_Idno, Int64 Driver_Idno, Int64 Year_Idno, DateTime? Date_Updated, double NetAmnt, DataTable dtDetail,string InvoiceNo)
        {
            Int64 FuelSlip_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblFuelSlipHead objFuelSlipHead = db.tblFuelSlipHeads.Where(bill => (bill.FuelSlip_No == FuelSlip_No) && (bill.FuelSlip_Idno != FuelSlipHead_Idno) && (bill.Year_Idno == Year_Idno) && (bill.Loc_Idno == Loc_Idno)).FirstOrDefault();
                    if (objFuelSlipHead == null)
                    {
                        tblFuelSlipHead tblFuelSlipHead1 = db.tblFuelSlipHeads.Where(rh => rh.FuelSlip_Idno == FuelSlipHead_Idno).FirstOrDefault();
                        if (tblFuelSlipHead1 != null)
                        {
                            tblFuelSlipHead1.FuelSlip_Date = FuelSlip_Date;
                            tblFuelSlipHead1.Loc_Idno = Loc_Idno;
                            tblFuelSlipHead1.Truck_Idno = Truck_Idno;
                            tblFuelSlipHead1.Pump_Idno = Pump_Idno;
                            tblFuelSlipHead1.Driver_Idno = Driver_Idno;
                            tblFuelSlipHead1.Year_Idno = Year_Idno;
                            tblFuelSlipHead1.Date_Modified = Date_Updated;
                            tblFuelSlipHead1.Year_Idno = Year_Idno;
                            tblFuelSlipHead1.Invoice_No = InvoiceNo;
                            tblFuelSlipHead1.Net_Amnt = NetAmnt;
                            db.SaveChanges();
                            FuelSlip_Idno = tblFuelSlipHead1.FuelSlip_Idno;
                            if (FuelSlip_Idno > 0)
                            {
                                List<tblFuelSlipDetl> lstFuelSlipDetl = db.tblFuelSlipDetls.Where(obj => obj.FuelSlipHead_Idno == FuelSlip_Idno).ToList();
                                if (lstFuelSlipDetl.Count > 0)
                                {
                                    foreach (tblFuelSlipDetl obj in lstFuelSlipDetl)
                                    {
                                        db.tblFuelSlipDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    tblFuelSlipDetl objFuelSlipDetl = new tblFuelSlipDetl();
                                    objFuelSlipDetl.FuelSlipHead_Idno = Convert.ToInt64(FuelSlip_Idno);
                                    objFuelSlipDetl.Iteam_Idno = Convert.ToInt64(row["ItemNameIdno"]);
                                    objFuelSlipDetl.Item_Qty = Convert.ToDouble(row["Qty"]);
                                    objFuelSlipDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objFuelSlipDetl.Item_Amnt = Convert.ToDouble(row["Amount"]);
                                    db.tblFuelSlipDetls.AddObject(objFuelSlipDetl);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        FuelSlip_Idno = -1;
                    }
                }
                catch (Exception Ex)
                {
                    FuelSlip_Idno = 0;
                }
            }
            return FuelSlip_Idno;
        }
        public tblFuelSlipHead SelectById(Int64 intFuelSlipIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblFuelSlipHeads
                        join mast1 in db.tblFuelSlipDetls on mast.FuelSlip_Idno equals mast1.FuelSlipHead_Idno
                        where mast.FuelSlip_Idno == intFuelSlipIdno
                        select mast).FirstOrDefault();
            }
        }
        public IList SelectDetlById(Int64 intFuelSlipIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from FSH in db.tblFuelSlipHeads
                        join FSD in db.tblFuelSlipDetls on FSH.FuelSlip_Idno equals FSD.FuelSlipHead_Idno
                        join CM in db.tblCityMasters on FSH.Loc_Idno equals CM.City_Idno
                        join LM in db.LorryMasts on FSH.Truck_Idno equals LM.Lorry_Idno
                        //join DM in db.DriverMasts on FSH.Driver_Idno equals DM.Driver_Idno
                        join PM in db.AcntMasts on FSH.Pump_Idno equals PM.Acnt_Idno
                        join IM in db.tblItemMastPurs on FSD.Iteam_Idno equals IM.Item_Idno
                        where FSH.FuelSlip_Idno == intFuelSlipIdno
                        select new
                        {
                            SlipNo = FSH.FuelSlip_No,
                            Id = FSH.FuelSlip_Idno,
                            LocId = FSH.Loc_Idno,
                            LocName = CM.City_Name,
                            LorryId = LM.Lorry_Idno,
                            LorryName = LM.Lorry_No,
                            DriverId = LM.Driver_Idno,
                            Driver = LM.Lorry_Type == 1 ? (from N in db.DriverMasts where N.Driver_Idno == LM.Driver_Idno select N.Driver_Name).FirstOrDefault() : (from M in db.AcntMasts where M.Acnt_Idno == FSH.Driver_Idno select M.Acnt_Name).FirstOrDefault(),
                            PumpId = PM.Acnt_Idno,
                            Pump = PM.Acnt_Name,
                            ItemName = IM.Item_Name,
                            ItemId = FSD.Iteam_Idno,
                            Qty = FSD.Item_Qty,
                            Amount = FSD.Item_Amnt,
                            Rate = FSD.Item_Rate,
                            Date = FSH.FuelSlip_Date,
                            YearIdno = FSH.Year_Idno,
                        }).ToList();
            }
        }
        public Int64 Countall(Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 c = 0;
                c = (from FSH in db.tblFuelSlipHeads
                     join FSD in db.tblFuelSlipDetls on FSH.FuelSlip_Idno equals FSD.FuelSlipHead_Idno
                     join CM in db.tblCityMasters on FSH.Loc_Idno equals CM.City_Idno
                     join LM in db.LorryMasts on FSH.Truck_Idno equals LM.Lorry_Idno
                     //join DM in db.DriverMasts on FSH.Driver_Idno equals DM.Driver_Idno
                     join PM in db.AcntMasts on FSH.Pump_Idno equals PM.Acnt_Idno
                     join IM in db.tblItemMastPurs on FSD.Iteam_Idno equals IM.Item_Idno
                     where FSH.Year_Idno == YearIdno
                     select FSH.FuelSlip_Idno).Count();
                return c;
            }
        }
        public IList Select(Int64 YearIdno, Int64 LocIdno, Int64 TruckIdno, Int64 DriverIdno, Int64 PumpIdno, Int64 SlipNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from FSH in db.tblFuelSlipHeads
                           join FSD in db.tblFuelSlipDetls on FSH.FuelSlip_Idno equals FSD.FuelSlipHead_Idno
                           join CM in db.tblCityMasters on FSH.Loc_Idno equals CM.City_Idno
                           join LM in db.LorryMasts on FSH.Truck_Idno equals LM.Lorry_Idno
                           // join DM in db.DriverMasts on FSH.Driver_Idno equals DM.Driver_Idno
                           join PM in db.AcntMasts on FSH.Pump_Idno equals PM.Acnt_Idno
                           join IM in db.tblItemMastPurs on FSD.Iteam_Idno equals IM.Item_Idno
                           select new
                           {
                               SlipNo = FSH.FuelSlip_No,
                               Id = FSH.FuelSlip_Idno,
                               LocId = FSH.Loc_Idno,
                               LocName = CM.City_Name,
                               LorryId = LM.Lorry_Idno,
                               LorryName = LM.Lorry_No,
                               DriverId = LM.Driver_Idno,
                               Driver = LM.Lorry_Type == 1 ? (from N in db.DriverMasts where N.Driver_Idno == LM.Driver_Idno select N.Driver_Name).FirstOrDefault() : (from M in db.AcntMasts where M.Acnt_Idno == FSH.Driver_Idno select M.Acnt_Name).FirstOrDefault(),
                               PumpId = PM.Acnt_Idno,
                               Pump = PM.Acnt_Name,
                               ItemName = IM.Item_Name,
                               Qty = FSD.Item_Qty,
                               Amount = FSD.Item_Amnt,
                               Date = FSH.FuelSlip_Date,
                               YearIdno = FSH.Year_Idno,
                           }).ToList();

                if (YearIdno > 0)
                {
                    Lst = Lst.Where(r => r.YearIdno == YearIdno).ToList();
                }
                if (LocIdno > 0)
                {
                    Lst = Lst.Where(r => r.LocId == LocIdno).ToList();
                }
                if (TruckIdno > 0)
                {
                    Lst = Lst.Where(r => r.LorryId == TruckIdno).ToList();
                }
                if (PumpIdno > 0)
                {
                    Lst = Lst.Where(r => r.PumpId == PumpIdno).ToList();
                }
                if (SlipNo > 0)
                {
                    Lst = Lst.Where(r => r.SlipNo == SlipNo).ToList();
                }
                return Lst;
            }
        }
        public int ICount(string SerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.Stckdetls.Where(r => r.SerialNo == SerialNo && r.MtrlIssue_Idno != 0 && r.Is_Issued == false).Count();
            }
        }
        public List<tblItemMastPur> BindPurchaseItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();
                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == 1
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<tblItemMastPur> BindPurchaseItemNameAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();
                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == 1
                               select obj).ToList();
                return objItemMast;
            }
        }
        public IList GetExcel(Int64 intLocIdno, Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.Stckdetls
                           join Im in db.tblItemMastPurs on CT.ItemIdno equals Im.Item_Idno
                           join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                           where CT.Loc_Idno == intLocIdno
                           orderby sm.City_Name
                           select new
                           {
                               ItemName = Im.Item_Name,
                               LoctionName = sm.City_Name,
                               SerialNo = CT.SerialNo,
                               CompanyName = CT.CompName,
                               Rate = CT.OpenRate,
                               Amount = CT.OpenRate,
                           }).Distinct().ToList();

                return lst;
            }
        }
        public Int64 Count(Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from CT in db.Stckdetls
                        join I in db.tblItemMastPurs on CT.ItemIdno equals I.Item_Idno
                        join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                        where CT.yearId == YearIdno && I.ItemType == 1
                        select new
                        {
                            LocIdno = CT.Loc_Idno,
                        }).GroupBy(x => x.LocIdno).Select(x => x.FirstOrDefault()).Count();
            }
        }
        public List<DriverMast> selectHireDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<DriverMast> lst = null;
                lst = (from DR in db.DriverMasts orderby DR.Driver_Name ascending select DR).ToList();
                return lst;
            }
        }
        public List<AcntMast> selectOwnerDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from DR in db.AcntMasts where DR.Acnt_Type == 9 orderby DR.Acnt_Name ascending select DR).ToList();
                return lst;
            }
        }
        public Int64 GetOwnerIdbyLorryIdno(Int64 LorryIdno)
        {
            Int64 OwnerIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    OwnerIdno = Convert.ToInt64((from cm in db.LorryMasts where cm.Lorry_Idno == LorryIdno select cm.Prty_Idno).FirstOrDefault());
                    //if (OwnerIdno > 0)
                    //{

                    //}
                    //else
                    //{
                    //    OwnerIdno = 0;
                    //}
                }
                catch (Exception Ex)
                {
                    OwnerIdno = 0;
                }
            }
            return OwnerIdno;
        }
        public int Delete(int intFuelSlipIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblFuelSlipHead objItemMast = (from mast in db.tblFuelSlipHeads
                                                   where mast.FuelSlip_Idno == intFuelSlipIdno
                                                   select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        db.tblFuelSlipHeads.DeleteObject(objItemMast);
                        db.SaveChanges();
                        intValue = 1;
                    }
                    List<tblFuelSlipDetl> Detl = (from M in db.tblFuelSlipDetls where M.FuelSlipHead_Idno == intFuelSlipIdno select M).ToList();
                    if (Detl != null && Detl.Count > 0)
                    {
                        foreach (var D in Detl)
                        {
                            db.tblFuelSlipDetls.DeleteObject(D);
                        }
                        db.SaveChanges();
                    }
                    clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                    objclsAccountPosting.DeleteAccountPosting(intFuelSlipIdno, "FS");
                    intValue = 1;
                }
            }
            catch (Exception Ex)
            {
                if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                {
                    intValue = -1;
                }
            }
            return intValue;
        }
        #region ForExcelReading
        public DataTable TurncatePartsAccessoriesFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblOpngStockTyreFromExcel";
            DataSet ds = SqlHelper.ExecuteDataset(ConString, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public Int64 InsertPartsByExcel(DataTable dt, string ConString)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string modelName = row["TyreName"].ToString();
                    tblOpngStockTyreFromExcel obj = (from objstock in db.tblOpngStockTyreFromExcels
                                                     where objstock.Item_Name == modelName
                                                     select objstock).FirstOrDefault();
                    //if (obj == null)
                    //{
                    tblOpngStockTyreFromExcel objPart = new tblOpngStockTyreFromExcel();
                    objPart.Item_Name = Convert.ToString(row["TyreName"]);
                    objPart.Item_Rate = Convert.ToDouble(row["OpeningRate"]);
                    objPart.SerialNo = Convert.ToString(row["SerialNo"]);
                    objPart.CompanyName = Convert.ToString(row["CompanyName"]);
                    objPart.PurchaseFrom = Convert.ToString(row["PurchaseFrom"]);
                    objPart.Type = Convert.ToString(row["Type"]);
                    objPart.Type_Idno = Convert.ToInt32(row["Type_Idno"]);
                    db.tblOpngStockTyreFromExcels.AddObject(objPart);
                    db.SaveChanges();
                    value = objPart.Stck_Idno;
                    // }
                }
                UpdatefromExcel2(ConString);
            }
            return value;
        }
        public DataTable UpdatefromExcel2(string ConString)
        {
            string str = string.Empty;
            str = @"UPDATE tblOpngStockTyreFromExcel SET ITEM_IDNO = I.Item_Idno FROM tblItemMastPur I WHERE tblOpngStockTyreFromExcel.Item_Name = I.Item_Name";
            DataSet ds = SqlHelper.ExecuteDataset(ConString, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable SelectPartFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"SELECT * FROM tblOpngStockTyreFromExcel";
            DataSet ds = SqlHelper.ExecuteDataset(ConString, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public IList GetItemDetailsExl(string ItemName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from obj in db.tblItemMastPurs where obj.Item_Name == ItemName select obj).ToList();

                return Lst;
            }
        }
        #endregion
        public IList CheckItemExistInOtherMaster(Int32 ItemIdno, string SerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.MatIssHeads
                        join obj1 in db.MatIssDetls on obj.MatIss_Idno equals obj1.MatIssHead_Idno
                        where obj1.Iteam_Idno == ItemIdno && obj1.Serial_Number == SerialNo
                        select new
                        {
                            ItemGrp = obj1.Iteam_Idno
                        }
                        ).ToList();
            }
        }
        public int TurncatetblFuelSlipFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblFuelSlipFromExcel";
            int result = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return result;
        }
        public Int64 InsertInFuelExcel(Int32 SlipNo, string LorryNo, string Driver, string ItemName, double Amount)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;
                try
                {
                    tblFuelSlipFromExcel Obj = new tblFuelSlipFromExcel();
                    Obj.Slip_No = SlipNo;
                    Obj.Lorry_No = LorryNo;
                    Obj.Driver = Driver;
                    Obj.Amount = Amount;
                    Obj.ItemName = ItemName;
                    db.tblFuelSlipFromExcels.AddObject(Obj);
                    //db.AddTotblChlnUploadFromExcels(Obj);
                    db.SaveChanges();
                    value = Obj.Slip_Idno;
                }
                catch (Exception Exe)
                {
                    value = -1;
                }
                return value;
            }
        }

        public IList FuelExcelRecord(string lorryNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lorry = db.tblFuelSlipFromExcels.Where(x => x.Lorry_No == lorryNo).ToList();
                return Lorry;
            }
        }
        public IList FuelExcelDistinctRecord()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lorry = db.tblFuelSlipFromExcels.GroupBy(x => x.Lorry_No).Select(x => x.FirstOrDefault()).ToList();
                return Lorry;
            }
        }
    }
}
