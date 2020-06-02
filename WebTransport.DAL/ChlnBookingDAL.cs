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
using System.Transactions;

namespace WebTransport.DAL
{
    public class ChlnBookingDAL
    {
        public Int64 AutofillYear()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 yearIdno = 0, GrIdno = 0;
                GrIdno = Convert.ToInt64((from a in db.tblChlnBookHeads select a.Chln_Idno).Max());
                yearIdno = Convert.ToInt64((from a in db.tblChlnBookHeads where a.Chln_Idno == GrIdno select a.Year_Idno).FirstOrDefault());
                return yearIdno;
            }
        }
        public String AutofillDate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string Date = ""; Int64 GrIdno = 0;
                GrIdno = Convert.ToInt64((from a in db.tblChlnBookHeads select a.Chln_Idno).Max());
                Date = string.IsNullOrEmpty(Convert.ToString((from a in db.tblChlnBookHeads where a.Chln_Idno == GrIdno select a.Chln_Date).FirstOrDefault())) ? "" : Convert.ToDateTime((from a in db.tblChlnBookHeads where a.Chln_Idno == GrIdno select a.Chln_Date).FirstOrDefault()).ToString("dd-MM-yyyy");
                return Date;
            }
        }

        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public DataSet AccPosting(string conString, string Action, Int64 IdFrom, Int64 IdTo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@From", IdFrom);
            objSqlPara[2] = new SqlParameter("@To", IdTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDemoAccPosting", objSqlPara);
            //if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            //{
            //    dt = objDataSet.Tables[0];
            //}
            return objDataSet;
        }

        public DataTable DsHireAcnt(string con)
        {
            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT Acnt_Idno AS 'HireAccountID' FROM ACNTMAST WHERE ACNT_NAME='Hire Charges' AND INTERNAL=1";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable DtAcntDS(string con)
        {
            //using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            //{
            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno,ISNULL(TDS_Idno,0) AS TDS_Idno,ISNULL(DieselAcc_Idno,0) DieselAcc_Idno FROM tblAcntLink";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                // tScope.Complete();
            }
            return dt;
            // }
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }

        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }

        public Int32 GetChallanNo(Int32 YearIdno, Int32 FromCity, string Constring)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[3];
                objSqlParameter[0] = new SqlParameter("@Action", "SelectMaxChlnNo");
                objSqlParameter[1] = new SqlParameter("@FromCityIDno", FromCity);
                objSqlParameter[2] = new SqlParameter("@YearIdno", YearIdno);
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32(SqlHelper.ExecuteScalar(Constring, CommandType.StoredProcedure, "spChlnBookng", objSqlParameter));
                return MaxNo;
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters where cm.AsLocation == true orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public List<tblCityMaster> SelectAllCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
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

        public LorryMast selectOwnerName(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                LorryMast lst = ((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault());
                return lst;
            }
        }
        public string selectPartyName(Int32 PrtyIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                string lst = Convert.ToString((from DR in db.AcntMasts where DR.Acnt_Idno == PrtyIdno select DR.Acnt_Name).FirstOrDefault());
                return lst;
            }
        }
        public Int32 selectTruckType(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Lst = 0;
                Lst = Convert.ToInt32((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR.Lorry_Type).FirstOrDefault());
                return Lst;
            }
        }

        public IList selectTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from LM in db.LorryMasts orderby LM.Lorry_No ascending select LM).ToList();
                return lst;
            }
        }
        public DataTable searchBySP(String con, String strAction, Int32 yearid, string chlnNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno, string GrType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@ChlnFromDate", dtfrom);
            objSqlPara[2] = new SqlParameter("@ChlnToDate", dtto);
            objSqlPara[3] = new SqlParameter("@BaseCityIdno", FromCity);
            objSqlPara[4] = new SqlParameter("@LorryIdno", TruckNo);
            objSqlPara[5] = new SqlParameter("@UserId", UserIdno);
            objSqlPara[6] = new SqlParameter("@GrType", GrType);
            objSqlPara[7] = new SqlParameter("@YearIdno", yearid);
            objSqlPara[8] = new SqlParameter("@ChlnNo", chlnNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public DataTable SearchChallnBulkUpdate(String con, String strAction, Int32 yearid, string chlnNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno, string GrType, Int64 PartyIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[10];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@ChlnFromDate", dtfrom);
            objSqlPara[2] = new SqlParameter("@ChlnToDate", dtto);
            objSqlPara[3] = new SqlParameter("@BaseCityIdno", FromCity);
            objSqlPara[4] = new SqlParameter("@LorryIdno", TruckNo);
            objSqlPara[5] = new SqlParameter("@UserId", UserIdno);
            objSqlPara[6] = new SqlParameter("@GrType", GrType);
            objSqlPara[7] = new SqlParameter("@YearIdno", yearid);
            objSqlPara[8] = new SqlParameter("@ChlnNo", chlnNo);
            objSqlPara[9] = new SqlParameter("@PartyIdno", PartyIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public IList search(Int32 yearid, string chlnNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno, string GrType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblChlnBookHeads
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                           join LM in db.LorryMasts on CH.Truck_Idno equals LM.Lorry_Idno
                           where CH.Chln_type == 1 
                           orderby CH.Chln_Date descending, CH.Chln_No
                           select new
                             {
                                 CH.DelvryPlc_Idno,
                                 CH.BaseCity_Idno,
                                 CH.Chln_Date,
                                 CH.Chln_No,
                                 CH.Truck_Idno,
                                 LM.Lorry_No,
                                 CH.Chln_Idno,
                                 CH.Gr_Type,
                                 FromCity = cito.City_Name,
                                 CH.Tran_Type,
                                 //  Driver_Name=(LM.Lorry_Type==1)?()
                                 CH.Year_Idno,
                                 CH.Net_Amnt
                             }).ToList();

                if (GrType == "GR")
                {
                    lst = lst.Where(l => l.Gr_Type == GrType).ToList();
                }
                
                if (chlnNo != "")
                {
                    lst = lst.Where(l => l.Chln_No == chlnNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Chln_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Chln_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }
                //if (DelvPlce > 0)
                //{
                //    lst = lst.Where(l => l.DelvryPlc_Idno == DelvPlce).ToList();
                //}

                if (TruckNo > 0)
                {
                    lst = lst.Where(l => l.Truck_Idno == TruckNo).ToList();
                }
                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearid).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.BaseCity_Idno)).ToList();
                }

                return lst;
            }
        }

        public tblChlnBookHead selectHead(Int64 HeadId, string type)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId && h.Gr_Type == type).FirstOrDefault();
            }
        }

        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, Int64 TruckNo, Int64 TruckType, Int64 UserIdno, Int64 partyid,Int64 destID, string con)
        {
            //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            //{
            //    var lst = (from s in db.spGRRep(strAction,dtFromDate, dtToDate, iRecvrIdno, iSenderIdno, iFrmCityIdno, iDelPlcIdno, iGRTyp) select s).ToList();
            //    return lst;
            //}

            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@TruckId", TruckNo);
            objSqlPara[5] = new SqlParameter("@UserIdno", UserIdno);
            objSqlPara[6] = new SqlParameter("@TruckType", TruckType);
            objSqlPara[7] = new SqlParameter("@PartyId", partyid);
            objSqlPara[8] = new SqlParameter("@DestinationID", destID);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnRep", objSqlPara);
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


        public DataTable SelectChlnGrWiseRep(string strAction, string Query, string con)
        {
            
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@Query", Query);
            
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnRep", objSqlPara);
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


        public List<ItemMast> GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> lst = null;
                lst = (from cm in db.ItemMasts orderby cm.Item_Name ascending select cm).ToList();
                return lst;
            }
        }

        public Int64 Insert(tblChlnBookHead obj, DataTable Dttemp, Int32 DelvPlcIdno, string Tantype, DataTable Dttempfuel)
        {
            Int64 chlnBookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnBookHead CHead = new tblChlnBookHead();
                //   db.Connection.Open();
                //using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //{
                try
                {
                    tblChlnBookHead RH = db.tblChlnBookHeads.Where(rh => (rh.Chln_No == obj.Chln_No) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno) && (rh.Chln_type == 1)).FirstOrDefault();
                    if (RH != null)
                    {
                        chlnBookId = -1;
                    }
                    else
                    {
                        obj.Date_Added = DateTime.Now;
                        obj.Date_Modified = DateTime.Now;
                        db.tblChlnBookHeads.AddObject(obj);

                        db.SaveChanges();
                        chlnBookId = obj.Chln_Idno;
                        if (Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                tblChlnBookDetl objtblChlnBookDetl = new tblChlnBookDetl();
                                objtblChlnBookDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                                objtblChlnBookDetl.DelvryPlce_Idno = DelvPlcIdno;
                                objtblChlnBookDetl.ChlnBookHead_Idno = chlnBookId;
                                db.tblChlnBookDetls.AddObject(objtblChlnBookDetl);
                                db.SaveChanges();
                            }
                            //  dbTran.Commit();
                        }
                        if (Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                if (Tantype == "2")
                                {
                                    Int32 GrIdno = 0;
                                    GrIdno = Convert.ToInt32(Dr["Gr_Idno"]);
                                    tblGrRetailerHead objTblGrRetailerHead = (from obj1 in db.tblGrRetailerHeads where obj1.GRRetHead_Idno == GrIdno select obj1).FirstOrDefault();
                                    objTblGrRetailerHead.Chln_Idno = Convert.ToInt32(chlnBookId);
                                    objTblGrRetailerHead.Lorry_Idno = obj.Truck_Idno;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    Int32 GrIdno = 0;
                                    GrIdno = Convert.ToInt32(Dr["Gr_Idno"]);
                                    TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                    objTblGrHead.Chln_Idno = chlnBookId;
                                    objTblGrHead.Lorry_Idno = obj.Truck_Idno;
                                    db.SaveChanges();
                                }
                            }
                        }
                        if (Dttempfuel.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttempfuel.Rows)
                            {
                                ChlnFuelExpense objchlnfuel = new ChlnFuelExpense();
                                objchlnfuel.Acnt_Idno = Convert.ToInt64(Dr["acnt_idno"]);
                                objchlnfuel.Item_Idno = Convert.ToInt64(Dr["itemidno"]);
                                objchlnfuel.ChlnBooking_Idno = Convert.ToInt64(chlnBookId);
                                objchlnfuel.Qty = Convert.ToDouble(Dr["Qty"]);
                                objchlnfuel.Rate = Convert.ToDouble(Dr["Rate"]);
                                objchlnfuel.Amount = Convert.ToDouble(Dr["Amt"]);

                                db.ChlnFuelExpenses.AddObject(objchlnfuel);
                                db.SaveChanges();
                            }
                            //  dbTran.Commit();
                        }
                    }
                }
                catch
                {
                    // dbTran.Rollback();
                }
                //}

                return chlnBookId;
            }

        }


        public Int64 Update(tblChlnBookHead obj, Int32 ChlnIdno, DataTable Dttemp, Int32 DelvPlcIdno, string Tantype, DataTable Dttempfuel)
        {
            Int64 chlnBoookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblChlnBookHead CH = db.tblChlnBookHeads.Where(rh => rh.Chln_No == obj.Chln_No && (rh.Chln_Idno != ChlnIdno) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno) && (rh.Chln_type == 1)).FirstOrDefault();
                       
                        if (CH != null)
                        {
                            chlnBoookId = -1;
                        }
                        else
                        {
                            tblChlnBookHead TBH = db.tblChlnBookHeads.Where(rh => rh.Chln_Idno == ChlnIdno).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Chln_No = obj.Chln_No;
                                TBH.Chln_Date = obj.Chln_Date;
                                TBH.BaseCity_Idno = obj.BaseCity_Idno;
                                TBH.DelvryPlc_Idno = obj.DelvryPlc_Idno;
                                TBH.Truck_Idno = obj.Truck_Idno;
                                TBH.Year_Idno = obj.Year_Idno;
                                TBH.Chln_type = obj.Chln_type;
                                TBH.Driver_Idno = obj.Driver_Idno;
                                TBH.Delvry_Instrc = obj.Delvry_Instrc;
                                TBH.Inv_Idno = obj.Inv_Idno;
                                TBH.Gross_Amnt = obj.Gross_Amnt;
                                TBH.Commsn_Amnt = obj.Commsn_Amnt;
                                TBH.Other_Amnt = obj.Other_Amnt;
                                TBH.Net_Amnt = obj.Net_Amnt;
                                TBH.Work_type = obj.Work_type;
                                TBH.Adv_Amnt = obj.Adv_Amnt;
                                TBH.RcptType_Idno = obj.RcptType_Idno;
                                TBH.Inst_No = obj.Inst_No;
                                TBH.Inst_Dt = obj.Inst_Dt;
                                TBH.Bank_Idno = obj.Bank_Idno;
                                TBH.STaxPer_TDS = obj.STaxPer_TDS;
                                TBH.TDSTax_Amnt = obj.TDSTax_Amnt;
                                TBH.Diesel_Amnt = obj.Diesel_Amnt;
                                TBH.DieselAcnt_IDno = obj.DieselAcnt_IDno;
                                TBH.ManualNo = obj.ManualNo;
                                TBH.Date_Modified = DateTime.Now;
                                //TBH.Created_By = obj.Created_By;
                                TBH.DelvDate = obj.DelvDate;
                                TBH.RateKM = obj.RateKM;
                                TBH.Freight = obj.Freight;
                                TBH.Start_Km = obj.Start_Km;
                                TBH.Close_Km = obj.Close_Km;
                                TBH.Late_Charge = obj.Late_Charge;
                                TBH.Dentation = obj.Dentation;
                                TBH.RassaTirpal_Chrg = obj.RassaTirpal_Chrg;
                                TBH.Hamali = obj.Hamali;
                                TBH.User_ModifiedBy = obj.User_ModifiedBy;
                                TBH.Hire_Amnt = obj.Hire_Amnt;
                                db.SaveChanges();
                                chlnBoookId = TBH.Chln_Idno;
                                List<tblChlnBookDetl> ChlnDetl = db.tblChlnBookDetls.Where(rd => rd.ChlnBookHead_Idno == ChlnIdno).ToList();
                                foreach (tblChlnBookDetl rgd in ChlnDetl)
                                {
                                    db.tblChlnBookDetls.DeleteObject(rgd);
                                    db.SaveChanges();
                                }
                                //foreach (tblChlnBookDetl rgd in ChlnDetl)
                                //{
                                //    rgd.ChlnBookHead_Idno = chlnBoookId;
                                //    db.tblChlnBookDetls.AddObject(rgd);
                                //    db.SaveChanges();
                                //}

                                if (Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        tblChlnBookDetl objtblChlnBookDetl = new tblChlnBookDetl();
                                        objtblChlnBookDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                                        objtblChlnBookDetl.DelvryPlce_Idno = DelvPlcIdno;
                                        objtblChlnBookDetl.ChlnBookHead_Idno = chlnBoookId;
                                        objtblChlnBookDetl.Delivered = Convert.ToBoolean(Dr["Delivered"]);
                                        objtblChlnBookDetl.Delvry_Date = Convert.ToDateTime(Dr["Delvry_Date"]);
                                        objtblChlnBookDetl.Shrtg = Convert.ToBoolean(Dr["Shrtg"]);
                                        objtblChlnBookDetl.remark = Convert.ToString(Dr["remark"]);
                                        db.tblChlnBookDetls.AddObject(objtblChlnBookDetl);
                                        db.SaveChanges();
                                    }
                                    //  dbTran.Commit();
                                }
                                if (Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        if (Tantype == "GRR")
                                        {

                                            Int32 GrIdno = 0;
                                            GrIdno = Convert.ToInt32(Dr["Gr_Idno"]); ;
                                            tblGrRetailerHead objTblGrRetailerHead = (from obj1 in db.tblGrRetailerHeads where obj1.GRRetHead_Idno == GrIdno select obj1).FirstOrDefault();
                                            objTblGrRetailerHead.Chln_Idno = Convert.ToInt32(chlnBoookId);
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            Int32 GrIdno = 0;
                                            GrIdno = Convert.ToInt32(Dr["Gr_Idno"]); ;
                                            TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                            objTblGrHead.Chln_Idno = chlnBoookId;
                                            db.SaveChanges();

                                        }
                                    }
                                }
                                if (Dttempfuel.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttempfuel.Rows)
                                    {
                                        ChlnFuelExpense objchlnfuel = new ChlnFuelExpense();
                                        objchlnfuel.Acnt_Idno = Convert.ToInt64(Dr["acnt_idno"]);
                                        objchlnfuel.Item_Idno = Convert.ToInt64(Dr["itemidno"]);
                                        objchlnfuel.ChlnBooking_Idno = Convert.ToInt64(chlnBoookId);
                                        objchlnfuel.Qty = Convert.ToDouble(Dr["Qty"]);
                                        objchlnfuel.Rate = Convert.ToDouble(Dr["Rate"]);
                                        objchlnfuel.Amount = Convert.ToDouble(Dr["Amt"]);

                                        db.ChlnFuelExpenses.AddObject(objchlnfuel);
                                        db.SaveChanges();
                                    }
                                    //  dbTran.Commit();
                                }
                                dbTran.Commit();
                            }
                        }
                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return chlnBoookId;
        }

        public DataTable SelectDBData(Int64 Item_Idno, string con)
        {
            string str = string.Empty;
            str = @"select isnull(Rate_Idno,0) Rate_Idno,isnull(Rate_Date,0) Rate_Date,Isnull(CM.City_Name,'') City_Name ,Isnull(Item_Rate,0) Item_Rate,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0)WghtShrtg_Rate,ISNULL(Tocity_Idno,0) Tocity_Idno from tblRateMast RM Inner Join
           tblCityMaster CM ON CM.city_Idno=RM.ToCity_Idno where RM.Item_Idno= " + Item_Idno + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        public DataTable selectGrDetails(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 CityIdno, string strGrFrm, string con, Int32 FromCityIdno, Int32 CityViaIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DelvryPlcIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@ChlnDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@GrFrm", strGrFrm);
            objSqlPara[6] = new SqlParameter("@FromCityIdno", FromCityIdno);
            objSqlPara[7] = new SqlParameter("@CityViaIdno", CityViaIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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


        public DataTable SelectGRDetailTruckNo(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int64 CityIdno, string strGrFrm, string con, Int32 FromCityIdno, Int64 ICityViaIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DelvryPlcIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@ChlnDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@GrFrm", strGrFrm);
            objSqlPara[6] = new SqlParameter("@FromCityIdno", FromCityIdno);
            objSqlPara[7] = new SqlParameter("@CityViaIdno", ICityViaIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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


        #region SelectGrRetailer...
        // By salman
        public DataTable SelectGRRetailer(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int64 CityIdno, Int64 strtrantype, string con, Int32 FromCityIdno, Int64 ICityViaIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DelvryPlcIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@ChlnDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@Trantype", strtrantype);
            objSqlPara[6] = new SqlParameter("@FromCityIdno", FromCityIdno);
            objSqlPara[7] = new SqlParameter("@CityViaIdno", ICityViaIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public DataTable SelectGrRetailerDetails(string con, Int64 iYearId, string AllItmIdno,Int64 Trantype)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRRetailerDetails");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
            objSqlPara[3] = new SqlParameter("@TranType", Trantype);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public DataTable SelectGRRChlnDetail(string con, Int32 yearid, string chlnNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno, string GrType, string TranType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallanDetail");
            objSqlPara[1] = new SqlParameter("@ChlnNo", chlnNo);
            objSqlPara[2] = new SqlParameter("@FromCity", FromCity);
            objSqlPara[3] = new SqlParameter("@Lorryno", TruckNo);
            objSqlPara[4] = new SqlParameter("@FromDate", dtfrom);
            objSqlPara[5] = new SqlParameter("@ToDate", dtto);
            objSqlPara[6] = new SqlParameter("@Yearidno", yearid);
            objSqlPara[7] = new SqlParameter("@GrType", GrType);
            objSqlPara[8] = new SqlParameter("@Trantype", TranType);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSelectGrRetailer", objSqlPara);
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

        public DataTable SelectGrChallanDetails(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetailInChln");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public DataTable SelectDataForCommissinAmnt(string con, string AllItmIdno,string Commdate)
        {
            SqlParameter[] objsqlpara = new SqlParameter[3];
            objsqlpara[0] = new SqlParameter("@Action", "SelectDataForCommissinAmnt");
            objsqlpara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objsqlpara[2] = new SqlParameter("@COMMDATE", Commdate);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objsqlpara);
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
        public DataTable SelectDataForTDS(string con, string PANNo, Int32 StateIdno, DateTime ChlnDAte)
        {
            SqlParameter[] objsqlpara = new SqlParameter[4];
            objsqlpara[0] = new SqlParameter("@Action", "SelectDataForTDS");
            objsqlpara[1] = new SqlParameter("@PANNo", PANNo);
            objsqlpara[2] = new SqlParameter("@StateIdno", StateIdno);
            objsqlpara[3] = new SqlParameter("@ChlnDate", ChlnDAte);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objsqlpara);
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

        //public LorryMast selectOwnerName(Int32 LorryIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {

        //        LorryMast lst = ((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault());
        //        return lst;
        //    }
        //}
        public tblCityMaster GetStateIdno(Int32 cityidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster lst = (from cm in db.tblCityMasters where cm.City_Idno == cityidno orderby cm.City_Name ascending select cm).FirstOrDefault();
                return lst;
            }
        }
        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId, string Type)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);
            objSqlPara[2] = new SqlParameter("@GrType", Type);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        public int Delete(Int64 HeadId, Int64 UserIdno, string con,string Grtype)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                tblChlnBookHead CH = db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId).FirstOrDefault();
                List<tblChlnBookDetl> CD = db.tblChlnBookDetls.Where(d => d.ChlnBookHead_Idno == HeadId).ToList();
                if (CH != null && CD != null)
                {
                    var lst = (from CBD in db.tblChlnBookDetls
                               join IGD in db.tblInvGenDetls on CBD.GR_Idno equals IGD.GR_Idno
                               where CBD.ChlnBookHead_Idno==HeadId
                               select new
                               {
                                   IGD.InvGenHead_Idno
                               }).ToList();
                    var lst1 = (from CBD in db.tblChlnBookDetls
                                where CBD.ChlnBookHead_Idno == HeadId && CBD.Delivered==true
                               select new
                               {
                                   CBD.ChlnBookHead_Idno
                               }).ToList();
                    if (lst1 != null && lst1.Count>0)
                    {
                        return -1;
                    }
                    else if (lst != null && lst.Count > 0)
                    {
                        return -1;
                    }
                    else
                    {
                        if (CH != null)
                        {
                            SqlParameter[] objSqlPara = new SqlParameter[3];
                            objSqlPara[0] = new SqlParameter("@Action", "DeleteChallanBookDetails");
                            objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                            objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                            Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                            if (Grtype == "GR")
                            {

                                foreach (var d in CD)
                                {
                                    TblGrHead objTblGrHead = (from obj in db.TblGrHeads where obj.GR_Idno == d.GR_Idno select obj).FirstOrDefault();
                                    objTblGrHead.Chln_Idno = 0;
                                    db.SaveChanges();
                                }
                                foreach (var d in CD)
                                {
                                    List<TblGrDetl> GD = db.TblGrDetls.Where(G => G.GrHead_Idno == d.GR_Idno).ToList();
                                    if (GD != null)
                                    {
                                        foreach (var objGD in GD)
                                        {
                                            // TblGrDetl objTblGrDetl = (from obj in db.TblGrDetls where obj.GrHead_Idno == objGD.GrHead_Idno select obj).FirstOrDefault();
                                            objGD.shortage = 0;
                                            objGD.shortage_Amount = 0;
                                            objGD.shortage_Diff = 0;
                                            objGD.Shortage_Qty = 0;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var d in CD)
                                {
                                    tblGrRetailerHead objtblGrRetailerHead = (from obj in db.tblGrRetailerHeads where obj.GRRetHead_Idno == d.GR_Idno select obj).FirstOrDefault();
                                    objtblGrRetailerHead.Chln_Idno = 0;
                                    db.SaveChanges();
                                }
                                foreach (var d in CD)
                                {
                                    List<tblGrRetailerDetl> GD = db.tblGrRetailerDetls.Where(G => G.GRRetHead_Idno == d.GR_Idno).ToList();
                                    if (GD != null)
                                    {
                                        foreach (var objGD in GD)
                                        {
                                            // TblGrDetl objTblGrDetl = (from obj in db.TblGrDetls where obj.GrHead_Idno == objGD.GrHead_Idno select obj).FirstOrDefault();
                                            objGD.shortage = 0;
                                            objGD.shortage_Amount = 0;
                                            objGD.shortage_Diff = 0;
                                            objGD.Shortage_Qty = 0;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                            foreach (var d in CD)
                            {
                                db.tblChlnBookDetls.DeleteObject(d);
                                db.SaveChanges();
                            }
                            db.tblChlnBookHeads.DeleteObject(CH);

                            Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "CB");
                            db.SaveChanges();
                            if (intValue > 0)
                            {
                                value = 1;
                            }

                        }
                    }

                }

            }
            return value;
        }

        public Int32 CheckBilled(Int64 Id, string con,string GrType)
        {
            Int32 value = 0;

            SqlParameter[] objSqlParameter = new SqlParameter[3];
            objSqlParameter[0] = new SqlParameter("@Action", "SelectBilled");
            objSqlParameter[1] = new SqlParameter("@Id", Id);
            objSqlParameter[2] = new SqlParameter("@GRType", GrType);
            value = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spChlnBookng", objSqlParameter));
            return value;

        }

        public DataTable BindRcptType(string Con)
        {
            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(Con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable BindBank(string Con)
        {
            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(Con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable BindRcptTypeDel(Int32 intAcntIdno, string con)
        {

            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) and Acnt_Idno=" + intAcntIdno + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        public DataTable BindRcptTypeDetail(Int32 intAcntIdno, string con)
        {
            string sqlSTR = @"SELECT ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) and Acnt_Idno=" + intAcntIdno + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

            

        }


        public IList CheckItemExistInOtherMaster(Int32 ChlnIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblTripChlnDetls
                        where obj.Chln_Idno == ChlnIdno
                        select new
                        {
                            ChlnIdno = obj.Chln_Idno
                        }
                        ).ToList()
                         //.Union
                         //(from obj2 in db.tblChlnBookDetls
                         // where obj2.ChlnBookHead_Idno == ChlnIdno && obj2.Delvry_Date != null
                         // select new
                         // {
                         //     ChlnIdno = obj2.ChlnBookHead_Idno
                         // }
                          //).ToList()
                          .Union
                         (from obj2 in db.tblChlnAmntPayment_Detl
                          where obj2.Chln_Idno == ChlnIdno 
                          select new
                          {
                              ChlnIdno = obj2.Chln_Idno
                          }
                          ).ToList();
            }
        }
        // Challan Excel Upload JEET
        public IList FetchGRno(DateTime? Date, Int64 LocIDno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Grno = (from GH in db.TblGrHeads
                            join CM in db.tblCityMasters on GH.From_City equals CM.City_Idno
                            where GH.Gr_Date <= Date && GH.From_City == LocIDno && GH.Chln_Idno == 0 && GH.Billed == false
                            select new
                            {
                                GRNo = GH.Gr_No,
                                GRDate = GH.Gr_Date,
                                FromCity = CM.City_Name,
                                Amount = GH.SubTot_Amnt
                            }).ToList();
                return Grno;
            }
        }

        public int TurncatetblChlnUploadFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblChlnUploadFromExcel";
            int result = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return result;
        }
        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        public Int64 InsertInChlnExcel(Int64 GRNo, DateTime? date, string fromcity, double amount, string driver, double advamount, string paytype, double comm, Int64 intYearIdNo, int intCompIdNo, Int64 intUserIdNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;
                try
                {
                    tblChlnUploadFromExcel Obj = new tblChlnUploadFromExcel();
                    Obj.GRNo = GRNo;
                    Obj.GRDate = date;
                    Obj.FromCity = fromcity;
                    Obj.Amount = amount;
                    Obj.Driver = driver;
                    Obj.AdvanceAmount = advamount;
                    Obj.PayType = paytype;
                    Obj.Comm = comm;
                    Obj.Year_Idno = intYearIdNo;
                    Obj.CompId = intCompIdNo;
                    Obj.UserIdno = intUserIdNo;
                    Obj.SaveFlag = false;
                    Obj.Error_Flag = false;
                    Obj.PostFlag = false;
                    db.tblChlnUploadFromExcels.AddObject(Obj);
                    //db.AddTotblChlnUploadFromExcels(Obj);
                    db.SaveChanges();
                    value = Obj.ID;
                }
                catch (Exception Exe)
                {
                    value = -1;
                }
                return value;
            }
        }

        public IList SelectData()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var GrDet = (from GH in db.tblChlnUploadFromExcels
                             join CM in db.tblCityMasters on GH.FromCity equals CM.City_Name
                             select new
                             {
                                 GRNo = GH.GRNo,
                                 GRDate = GH.GRDate,
                                 FromCity = CM.City_Idno,
                                 CityName = GH.FromCity,
                                 Amount = GH.Amount,
                                 Driver = GH.Driver,
                                 Comm = GH.Comm,
                                 AdvAmnt = GH.AdvanceAmount,
                                 RcptType = GH.PayType
                             }).ToList();
                return GrDet;
            }
        }

        public IList SelectGRData(Int64 GRNo, Int64 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var GrDet = (from GH in db.TblGrHeads
                             where GH.Gr_No == GRNo && GH.From_City == FromCity
                             select new
                             {
                                 GRIdno = GH.GR_Idno,
                                 GRno = GH.Gr_No,
                                 GrossAmnt = GH.SubTot_Amnt,
                                 LorryId = GH.Lorry_Idno
                             }).ToList();
                return GrDet;
            }
        }

        public Int32 RcptTypeId(string acntname)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int GrDet = Convert.ToInt32((from GH in db.AcntMasts
                                             where GH.Acnt_Name == acntname && GH.Acnt_Type == 3
                                             select GH.Acnt_Idno).FirstOrDefault());
                return GrDet;
            }
        }

        public Int64 UpdateInChlnExcelSave(Int64 GRNo, string fromcity, bool SaveFlag)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    tblChlnUploadFromExcel Obj = db.tblChlnUploadFromExcels.Where(CP => CP.GRNo == GRNo && CP.FromCity == fromcity).FirstOrDefault();
                    if (Obj != null)
                    {
                        Obj.SaveFlag = SaveFlag;
                        if (SaveFlag == true)
                        {
                            Obj.Error_Flag = false;
                        }
                        else
                        {
                            Obj.Error_Flag = true;
                        }
                        db.SaveChanges();
                        value = Obj.ID;
                        dbTran.Commit();
                    }
                }

                db.Dispose();
                return value;
            }
        }

        public Int64 UpdateInChlnExcelPost(Int64 GRNo, string fromcity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;

                tblChlnUploadFromExcel Obj = db.tblChlnUploadFromExcels.Where(CP => CP.GRNo == GRNo && CP.FromCity == fromcity).FirstOrDefault();
                if (Obj != null)
                {
                    Obj.PostFlag = true;
                    Obj.SaveFlag = true;
                    Obj.Error_Flag = true;
                    db.SaveChanges();
                    value = Obj.ID;

                }


                return value;
            }
        }

        public IList SelectExcelFinalData()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var GrDet = (from GH in db.tblChlnUploadFromExcels
                             join CM in db.tblCityMasters on GH.FromCity equals CM.City_Name
                             select new
                             {
                                 GRNo = GH.GRNo,
                                 GRDate = GH.GRDate,
                                 FromCity = GH.FromCity,
                                 Amount = GH.Amount,
                                 Driver = GH.Driver,
                                 Comm = GH.Comm,
                                 AdvAmnt = GH.AdvanceAmount,
                                 RcptType = GH.PayType,
                                 IsSaved = GH.Error_Flag
                             }).ToList();
                return GrDet;
            }
        }

        public tblChlnBookHead selectDieselIDno(Int64 ChlnID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnBookHead obj = (from CBH in db.tblChlnBookHeads where CBH.Chln_Idno == ChlnID select CBH).FirstOrDefault();
                return obj;
            }
        }

        #region BindTrantype...
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Status == true
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList BindTransportaion(Int64 Id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objMiscMast = (from obj in db.tblMiscMasters
                                   where obj.Misc_Status == true && obj.Tran_Idno == Id
                                   orderby obj.Misc_Name
                                   select new
                                   {
                                       Misc_Name = obj.Misc_Name,
                                       Misc_Idno = obj.Misc_Idno
                                   }).ToList();
                return objMiscMast;
            }
        }
        public IList BindTrantype()
        {

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objMiscMast = (from obj in db.tblTranTypes
                                   
                                   orderby obj.TranType_Idno
                                   select new
                                   {
                                       Tran_Name = obj.Tran_Type,
                                       Tran_Idno = obj.TranType_Idno
                                   }).ToList();
                return objMiscMast;
            }

        }

        public DataSet GetTranDetails(string conString, Int32 LorryIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GETTRANSPORTDETAIL");
            objSqlPara[1] = new SqlParameter("@Trantype", LorryIdno);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            return objDataSet;
        }

        #endregion
        public void UpdateIsPosting(Int64 Chln_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblChlnBookHead GH = (from G in db.tblChlnBookHeads where G.Chln_Idno == Chln_Idno select G).FirstOrDefault();
                    if (GH != null)
                    {
                        GH.IS_Post = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        //Get Challan head by challan idno
        public DataTable SelectChallanHeadById(string con, Int64 ChlnIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetChlnHeadByChlnId");
            objSqlPara[1] = new SqlParameter("@Id", ChlnIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        //Get All Tracking location
        public DataTable SelectLorryTrackingLoc(string con, Int64 ChlnIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetLorryTrackingLoc");
            objSqlPara[1] = new SqlParameter("@Id", ChlnIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        //Get All Tracking location
        public DataTable SelectLorryTrackingLocByTrackIdno(string con, Int64 TrackIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetLorryTrackingLocByTrackIdno");
            objSqlPara[1] = new SqlParameter("@Id", TrackIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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

        //Get All Tracking location
        public Int32 DeleteLorryTrackingLocByTrackIdno(string con, Int64 TrackIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "DeleteLorryTrackingLocByTrackIdno");
            objSqlPara[1] = new SqlParameter("@Id", TrackIdno);

            Int32 status = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            return status;
        }

        public Int64 SaveTrackingLocation(tblLorryTrackLoc obj)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                obj.Date_Added = DateTime.Now;
                obj.Date_Modified = DateTime.Now;
                db.tblLorryTrackLocs.AddObject(obj);
                db.SaveChanges();
                return obj.LTrackLoc_Idno;
            }
        }

        public Int64 UpdateTrackingLocation(tblLorryTrackLoc obj, Int64 TrackIdno)
        {
            if (TrackIdno != 0)
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblLorryTrackLoc obj2 = db.tblLorryTrackLocs.Where(x => x.LTrackLoc_Idno == TrackIdno).SingleOrDefault();
                    obj2.City_Idno = obj.City_Idno;
                    obj2.Date_Modified = DateTime.Now;
                    obj2.Track_Date = obj.Track_Date;
                    obj2.Track_Time = obj.Track_Time;
                    obj2.User_Idno = obj.User_Idno;
                    obj.SMS_Sent = obj.SMS_Sent;
                    db.SaveChanges();
                    return obj.LTrackLoc_Idno;
                }
            }
            else
            {
                return 0;
            }
        }

        //CREATE SMS BY TRACKIDNO
        public String CreateSMSByTrackIdno(string con, Int64 TrackIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "CreateSMSByTrackIdno");
            objSqlPara[1] = new SqlParameter("@Id", TrackIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            String SMS = String.Empty;
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    SMS = objDsTemp.Tables[0].Rows[0][0].ToString();
                }
            }
            return SMS;
        }

        //GET PARTY MOBILE NO BY TRACKIDNO
        public String GetPartyMobileNoByTrackIdno(string con, Int64 TrackIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetPartyMobileNoByTrackIdno");
            objSqlPara[1] = new SqlParameter("@Id", TrackIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            String SMS = String.Empty;
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    SMS = objDsTemp.Tables[0].Rows[0][0].ToString();
                }
            }
            return SMS;
        }

        //BULK UPDATE CHALLAN FOR TDS AND COMMISSION
        public bool BulkUpdateChallan(String con, Int64 chlnidno, Double commissionAmnt, Double tdsAmnt)
        {
            Int64 tds_idno = 0;
            Int64 comm_idno = 0;
            String VchrType = "CB";
            Int64 retValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    try
                    {
                        //tblChlnBookHead obj = db.tblChlnBookHeads.Where(x => x.Chln_Idno == chlnidno).SingleOrDefault();
                        //obj.Gross_Amnt = (obj.Gross_Amnt - (obj.TDSTax_Amnt + obj.Commsn_Amnt) + (commissionAmnt + tdsAmnt));
                        //obj.Commsn_Amnt = commissionAmnt;
                        //obj.TDSTax_Amnt = tdsAmnt;
                        //db.SaveChanges();
                        //retValue = obj.Chln_Idno;

                        //scope.Complete();
                        SqlParameter[] objSqlPara = new SqlParameter[4];
                        objSqlPara[0] = new SqlParameter("@Action", "BulkUpdateChallan");
                        objSqlPara[1] = new SqlParameter("@Id", chlnidno);
                        objSqlPara[2] = new SqlParameter("@CommsnAmnt", commissionAmnt);
                        objSqlPara[3] = new SqlParameter("@TDSAmnt", tdsAmnt);

                        SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
                        return true;
                    }
                    catch (Exception e)
                    {
                        //scope.Dispose();
                        return false;
                    }
                //}
            }
        }

        public int IsPosted(string con, string action, long chlnidno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", action);
            objSqlPara[1] = new SqlParameter("@Id", chlnidno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            int retVal = 0;
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    retVal = Convert.ToInt32(objDsTemp.Tables[0].Rows[0][0].ToString());
                }
            }
            return retVal;
        }
        public DataSet SelectTruckList(string prefix, string con)
        {
            string str = string.Empty;
            str = @"select distinct Top 25 Lorry_Idno,Lorry_No=Lorry_No +' ['+Cast(Lorry_Idno as varchar(100))+']' from LorryMast LM where Lorry_No like '%" + prefix + "%'  order by Lorry_No Asc";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }
        public bool InsertfuelDetails(DataTable DtTempFuel)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (DtTempFuel != null && DtTempFuel.Rows.Count > 0)
                    {

                        foreach (DataRow row in DtTempFuel.Rows)
                        {
                            ChlnFuelExpense fueldt = new ChlnFuelExpense();

                            fueldt.Acnt_Idno = Convert.ToInt64(row["acnt_idno"]);
                            fueldt.Item_Idno = Convert.ToInt64(row["itemidno"]);
                            fueldt.ChlnBooking_Idno = Convert.ToInt64(row["Chln_Idno"]);
                            fueldt.Qty = Convert.ToInt64(row["Qty"]);
                            fueldt.Rate = Convert.ToInt64(row["Rate"]);
                            fueldt.Amount = Convert.ToInt64(row["Amt"]);
                            db.ChlnFuelExpenses.AddObject(fueldt);
                            db.SaveChanges();
                        }
                    }

                    else
                    {
                        return true;
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {

                return false;
            }
        }
        public IList SelectItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.ItemMasts
                           orderby mast.Item_Name
                           where mast.IGrp_Idno == 1 || mast.IGrp_Idno == 2 && mast.Status == true
                           select new
                           {
                               mast.Item_Name,
                               mast.Item_Idno
                           }).ToList();
                return lst;
            }
        }
        public Int64 selectchlnidno(String chlnno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Lst = 0;
                Lst = Convert.ToInt64((from DR in db.tblChlnBookHeads where DR.Chln_No == chlnno select DR.Chln_Idno).FirstOrDefault());
                return Lst;
            }
        }
        public DataTable selectFuelDetl(string con, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectFuelDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
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
    }
}
