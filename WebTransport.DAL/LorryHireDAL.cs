using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace WebTransport.DAL
{
    public class LorryHireDAL
    {

        #region Function.....
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Lorry_Type == 1
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public List<tblCityMaster> SelectCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public LorryMast selectOwnerName(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                LorryMast lst = ((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault());
                return lst;
            }
        }
     
        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblCityMasters where obj.AsLocation == true orderby obj.City_Name select obj).ToList();
                return lst;
               
            }
        }



        public Int64 MaxLorrySlipNo(Int32 intYearIdno,Int32 Locidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 intMaxLorrySlipNo = 0;
                intMaxLorrySlipNo = Convert.ToInt64((from lh in db.tblLorryHireSlips where lh.Year_Idno == intYearIdno && lh.Loc_Idno==Locidno select lh.Lry_SlipNo).Max());
                return intMaxLorrySlipNo + 1;
            }
        }
        //
        public Int64 countallslip()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 count = 0;
                count = (from lorryhire in db.tblLorryHireSlips
                         join Listlorry in db.LorryMasts on lorryhire.Lry_Idno equals Listlorry.Lorry_Idno
                         join ListCity in db.tblCityMasters on lorryhire.Loc_Idno equals ListCity.City_Idno
                         select lorryhire.LryHire_Idno).Count();
                return count;

            }
        }
        //
        public IList SearchLorryHire(Int64 intLorryidno, Int32 intYearId, Int64 intlocidno, Int32 intslipno, DateTime? dtfrom, DateTime? dtto)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from lorryhire in db.tblLorryHireSlips
                           join Listlorry in db.LorryMasts on lorryhire.Lry_Idno equals Listlorry.Lorry_Idno
                           join ListCity in db.tblCityMasters on lorryhire.Loc_Idno equals ListCity.City_Idno
                          
                           select new
                           {
                               lorryhire.Loc_Idno,
                               lorryhire.Lry_Date,
                               lorryhire.Lry_SlipNo,
                               lorryhire.Net_amnt,
                               lorryhire.AdvanceAmnt,
                               lorryhire.SupliedTo,
                               lorryhire.TotalFrghtAmnt,
                               lorryhire.Lry_Idno,
                               lorryhire.LryHire_Idno,
                              
                               lorryhire.Year_Idno,
                               Listlorry.Lorry_No,
                               ListCity.City_Name,

                               
                           }).Distinct().ToList();

                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Lry_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Lry_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (intYearId > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == intYearId).ToList();
                }
                if (intlocidno > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == intlocidno).ToList();
                }
                if(intslipno>0)
                {
                    lst = lst.Where(l => l.Lry_SlipNo == intslipno).ToList();
                }
                if (intLorryidno > 0)
                {
                    lst = lst.Where(l => l.Lry_Idno == intLorryidno).ToList();
                }
                return lst;
            }
        }
        //
        /// <summary>
        /// Delete record from ItemMast
        /// </summary>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public int Delete(int intSlipIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblLorryHireSlip objLrySlip = (from mast in db.tblLorryHireSlips
                                            where mast.LryHire_Idno == intSlipIdno
                                            select mast).FirstOrDefault();
                    if (objLrySlip != null)
                    {
                        db.tblLorryHireSlips.DeleteObject(objLrySlip);
                        db.SaveChanges();
                        intValue = 1;
                    }
                    tblLorryHireSlipGRDetl objLrydetl = (from mast in db.tblLorryHireSlipGRDetls
                                                         where mast.LryHire_Idno == intSlipIdno
                                                         select mast).FirstOrDefault();
                    if (objLrydetl != null)
                    {
                        db.tblLorryHireSlipGRDetls.DeleteObject(objLrydetl);
                        db.SaveChanges();
                        intValue = 1;
                    }
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
     
        public tblLorryHireSlip SelectById(Int64 lryid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblLorryHireSlips.Where(l => l.LryHire_Idno == lryid).FirstOrDefault();
            }
        }

        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireLorryDetl", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }
        #endregion

        #region Insert Delete......
        //Insert
        public Int64 Insert(Int64 Slipno, DateTime? Lorry_Date, Int64 Loc_Idno, Int64 Truck_Idno, string strsupllyby, Int64 Year_Idno, Double totlfrghtamnt, Double advanceamnt, Double netamnt, DateTime? Date_Added, string strremarks, Double othercharg, Double unloading, Double detentioncharg, Double diesel, Double TDS, DataTable Dttemp)
        {
            Int64 LorryHire_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblLorryHireSlip objlorry = db.tblLorryHireSlips.Where(slipno => (slipno.Lry_Idno == Truck_Idno) && (slipno.Year_Idno == Year_Idno) && (slipno.Loc_Idno == Loc_Idno)).FirstOrDefault();
                    if (objlorry == null)
                    {
                        objlorry = new tblLorryHireSlip();
                        objlorry.Lry_SlipNo = Slipno;
                        objlorry.Lry_Date = Lorry_Date;
                        objlorry.Loc_Idno = Loc_Idno;
                        objlorry.Lry_Idno = Truck_Idno;
                        objlorry.SupliedTo = strsupllyby;
                        objlorry.Year_Idno = Year_Idno;
                        objlorry.Date_Added = Date_Added;
                        objlorry.TotalFrghtAmnt = totlfrghtamnt;
                        objlorry.AdvanceAmnt = advanceamnt;
                        objlorry.Net_amnt = netamnt;
                        objlorry.Remark = strremarks;
                        objlorry.OtherCharges = othercharg;
                        objlorry.Unloading = unloading;
                        objlorry.DetectionCharges = detentioncharg;
                        objlorry.Diesel = diesel;
                        objlorry.TDS = TDS;
                        db.tblLorryHireSlips.AddObject(objlorry);
                        db.SaveChanges();
                        LorryHire_Idno = objlorry.LryHire_Idno;
                         if (Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in Dttemp.Rows)
                                {
                                    tblLorryHireSlipGRDetl objHireSlipDetl = new tblLorryHireSlipGRDetl();
                                    objHireSlipDetl.LryHire_Idno = LorryHire_Idno;
                                    objHireSlipDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                                    db.tblLorryHireSlipGRDetls.AddObject(objHireSlipDetl);
                                    db.SaveChanges();
                                }
                            }
                         
                    }
                    else
                    {
                        return LorryHire_Idno;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return LorryHire_Idno;
            }
        }
        //Update
        public Int64 Update(Int64 lorryhireIdno, Int64 Slipno, DateTime? Lorry_Date, Int64 Loc_Idno, Int64 Truck_Idno, string strsupllyby, Int64 Year_Idno, Double totlfrghtamnt, Double advanceamnt, Double netamnt, DateTime? Date_Modified, string strremarks, Double othercharg, Double unloading, Double detentioncharg, Double diesel, Double TDS,DataTable Dttemp)
        {
            Int64 LorryHire_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                  
                        tblLorryHireSlip objlorryupdate = db.tblLorryHireSlips.Where(rh => rh.LryHire_Idno == lorryhireIdno).FirstOrDefault();
                        if (objlorryupdate != null)
                        {
                            objlorryupdate.Lry_SlipNo = Slipno;
                            objlorryupdate.Lry_Date = Lorry_Date;
                            objlorryupdate.Loc_Idno = Loc_Idno;
                            objlorryupdate.Lry_Idno = Truck_Idno;
                            objlorryupdate.SupliedTo = strsupllyby;
                            objlorryupdate.Year_Idno = Year_Idno;
                            objlorryupdate.Date_Modified = Date_Modified;
                            objlorryupdate.TotalFrghtAmnt = totlfrghtamnt;
                            objlorryupdate.AdvanceAmnt = advanceamnt;
                            objlorryupdate.Remark = strremarks;
                            objlorryupdate.Net_amnt = netamnt;
                            objlorryupdate.OtherCharges = othercharg;
                            objlorryupdate.Unloading = unloading;
                            objlorryupdate.DetectionCharges = detentioncharg;
                            objlorryupdate.Diesel = diesel;
                            objlorryupdate.TDS = TDS;
                            db.SaveChanges();
                            LorryHire_Idno = objlorryupdate.LryHire_Idno;

                            List<tblLorryHireSlipGRDetl> HireLorryDetl = db.tblLorryHireSlipGRDetls.Where(rd => rd.LryHire_Idno == LorryHire_Idno).ToList();
                            foreach (tblLorryHireSlipGRDetl rgd in HireLorryDetl)
                            {
                                db.tblLorryHireSlipGRDetls.DeleteObject(rgd);
                                db.SaveChanges();
                            }
                            if (Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in Dttemp.Rows)
                                {
                                    tblLorryHireSlipGRDetl objHireSlipDetl = new tblLorryHireSlipGRDetl();
                                    objHireSlipDetl.LryHire_Idno = LorryHire_Idno;
                                    objHireSlipDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                                    db.tblLorryHireSlipGRDetls.AddObject(objHireSlipDetl);
                                    db.SaveChanges();
                                  
                                }
                                //  dbTran.Commit();
                            }
                        }
                    
                    else
                    {
                        LorryHire_Idno=-1;
                    }
                }
                catch (Exception ex)
                {
                    LorryHire_Idno=0;
                }
                return LorryHire_Idno;
            }
        }




        //Delete
        public void DeleteAll(Int64 LorryHire_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblLorryHireSlip> objSerial = db.tblLorryHireSlips.Where(lryhead => lryhead.LryHire_Idno == LorryHire_Idno).ToList();
                if (objSerial != null)
                {
                    foreach (var H in objSerial)
                    {
                        db.tblLorryHireSlips.DeleteObject(H);
                        db.SaveChanges();
                    }
                }
                List<tblLorryHireSlipGRDetl> HireLorryDetl = db.tblLorryHireSlipGRDetls.Where(rd => rd.LryHire_Idno == LorryHire_Idno).ToList();
                foreach (tblLorryHireSlipGRDetl rgd in HireLorryDetl)
                {
                    db.tblLorryHireSlipGRDetls.DeleteObject(rgd);
                    db.SaveChanges();
                }
            }
        }
       
        #endregion

        #region Select Detail...
        public DataTable SelectGRDetailTruckNo(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int32 FromCityIdno,Int64 LorryIdno, string con )
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@LorryIdno", LorryIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@FrmDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@FromCityIdno", FromCityIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireLorryDetl", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }
        public DataTable SelectGrChallanDetails(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetailInChln");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireLorryDetl", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }
        #endregion
    }
}
