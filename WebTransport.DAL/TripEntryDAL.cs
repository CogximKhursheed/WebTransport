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
    public class TripEntryDAL
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
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno,ISNULL(TDS_Idno,0) AS TDS_Idno FROM tblAcntLink";
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

        public Int32 GetTripNo(Int32 YearIdno, Int32 FromCity, string Constring)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[3];
                objSqlParameter[0] = new SqlParameter("@Action", "SelectMaxTripNo");
                objSqlParameter[1] = new SqlParameter("@FromCityIDno", FromCity);
                objSqlParameter[2] = new SqlParameter("@YearIdno", YearIdno);
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32(SqlHelper.ExecuteScalar(Constring, CommandType.StoredProcedure, "spTripSheet", objSqlParameter));
                return MaxNo;
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters where cm.AsLocation==true orderby cm.City_Name ascending select cm).ToList();
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

        public IList selectOwnerDriverNameReport()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from DR in db.AcntMasts join TH in db.tblTripHeads on DR.Acnt_Idno equals TH.Truck_Idno where DR.Acnt_Type == 9 orderby DR.Acnt_Name ascending select DR).GroupBy(X=>X.Acnt_Name).ToList();
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

        //kapil
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Lorry_Type == 0
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
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

        public IList search(Int32 yearid, string TripNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblTripHeads
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno

                           join LM in db.LorryMasts on CH.Truck_Idno equals LM.Lorry_Idno
                           select new
                             {
                                 CH.BaseCity_Idno,
                                 CH.Trip_Date,
                                 CH.Trip_No,
                                 CH.Truck_Idno,
                                 LM.Lorry_No,
                                 CH.Trip_Idno,
                                 FromCity = cito.City_Name,
                                 //  Driver_Name=(LM.Lorry_Type==1)?()
                                 CH.Year_Idno,
                                 CH.Net_Amnt
                             }).ToList();
                if (TripNo != "")
                {
                    lst = lst.Where(l => l.Trip_No == TripNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Trip_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Trip_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
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

        public tblTripHead selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblTripHeads.Where(h => h.Trip_Idno == HeadId).FirstOrDefault();
            }
        }

        //public DataTable SelectRep(string strAction, DateTime? dtFromDate, DateTime? dtToDate, Int64 iFrmCityIdno, Int64 TruckNo, Int64 UserIdno, string con)
        //{
        //    //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    //{
        //    //    var lst = (from s in db.spGRRep(strAction,dtFromDate, dtToDate, iRecvrIdno, iSenderIdno, iFrmCityIdno, iDelPlcIdno, iGRTyp) select s).ToList();
        //    //    return lst;
        //    //}

        //    SqlParameter[] objSqlPara = new SqlParameter[6];
        //    objSqlPara[0] = new SqlParameter("@Action", strAction);
        //    objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
        //    objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
        //    objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
        //    objSqlPara[4] = new SqlParameter("@TruckId", TruckNo);
        //    objSqlPara[5] = new SqlParameter("@UserIdno", UserIdno);
        //    DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnRep", objSqlPara);
        //    DataTable objDtTemp = new DataTable();
        //    if (objDsTemp.Tables.Count > 0)
        //    {
        //        if (objDsTemp.Tables[0].Rows.Count > 0)
        //        {
        //            objDtTemp = objDsTemp.Tables[0];
        //        }
        //    }
        //    return objDtTemp;
        //}

        public List<ItemMast> GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> lst = null;
                lst = (from cm in db.ItemMasts orderby cm.Item_Name ascending select cm).ToList();
                return lst;
            }
        }

        public Int64 Insert(tblTripHead obj, DataTable DtChln, DataTable DtFuel, DataTable DtExp, DataTable DtToll)
        {
            Int64 chlnBookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                db.Connection.Open();
                try
                {
                    tblTripHead RH = db.tblTripHeads.Where(rh => (rh.Trip_No == obj.Trip_No) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno)).FirstOrDefault();
                    if (RH != null)
                    {
                        chlnBookId = -1;
                    }
                    else
                    {
                        db.tblTripHeads.AddObject(obj);
                        db.SaveChanges();
                        chlnBookId = obj.Trip_Idno;
                        if (DtChln != null && DtChln.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in DtChln.Rows)
                            {
                                Int64 ChlnIdno = Convert.ToInt64(Dr["Chln_Idno"]);
                                tblTripChlnDetl objtblChlnDetl = new tblTripChlnDetl();
                                objtblChlnDetl.TripHead_Idno = chlnBookId;
                                objtblChlnDetl.Chln_Idno = ChlnIdno;
                                db.tblTripChlnDetls.AddObject(objtblChlnDetl);
                                db.SaveChanges();
                            }
                        }
                        if (DtFuel != null && DtFuel.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in DtFuel.Rows)
                            {
                                Int64 PbillIdno = Convert.ToInt64(Dr["PBill_Idno"]);
                                double Amount = Convert.ToDouble(Dr["Amount"]);
                                tblTripFuelDetl objtblFuelDetl = new tblTripFuelDetl();
                                objtblFuelDetl.TripHead_Idno = chlnBookId;
                                objtblFuelDetl.Pbill_Idno = PbillIdno;
                                objtblFuelDetl.Amount = Amount;
                                db.tblTripFuelDetls.AddObject(objtblFuelDetl);
                                db.SaveChanges();
                            }
                        }

                        if (DtExp != null && DtExp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in DtExp.Rows)
                            {
                                tblTripExpDetl objtblExpDetl = new tblTripExpDetl();
                                objtblExpDetl.TripHead_Idno = chlnBookId;
                                objtblExpDetl.Acnt_Idno = Convert.ToInt64(Dr["Acnt_Idno"]);
                                objtblExpDetl.Amnt = Convert.ToDouble(Dr["Amnt"]);
                                db.tblTripExpDetls.AddObject(objtblExpDetl);
                                db.SaveChanges();
                            }
                        }

                        if (DtToll != null && DtToll.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in DtToll.Rows)
                            {
                                tblTripTollDetl objtblTollDetl = new tblTripTollDetl();
                                objtblTollDetl.TripHead_Idno = chlnBookId;
                                objtblTollDetl.Toll_Idno = Convert.ToInt64(Dr["Toll_Idno"]);
                                objtblTollDetl.Amnt = Convert.ToInt64(Dr["Amnt"]);
                                db.tblTripTollDetls.AddObject(objtblTollDetl);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                catch
                {
                    chlnBookId = 0;
                }
                db.Connection.Close();
                return chlnBookId;
            }

        }

        public Int64 Update(tblTripHead obj, DataTable DtChln, DataTable DtFuel, DataTable DtExp, DataTable DtToll, Int64 TripIdno)
        {
            Int64 chlnBoookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();

                try
                {
                    tblTripHead CH = db.tblTripHeads.Where(rh => rh.Trip_No == obj.Trip_No && (rh.Trip_Idno != TripIdno) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno)).FirstOrDefault();
                    if (CH != null)
                    {
                        chlnBoookId = -1;
                    }
                    else
                    {
                        tblTripHead TBH = db.tblTripHeads.Where(rh => rh.Trip_Idno == TripIdno).FirstOrDefault();
                        if (TBH != null)
                        {
                            TBH.Trip_No = obj.Trip_No;
                            TBH.Trip_Date = obj.Trip_Date;
                            TBH.BaseCity_Idno = obj.BaseCity_Idno;
                            TBH.Truck_Idno = obj.Truck_Idno;
                            TBH.Year_Idno = obj.Year_Idno;
                            TBH.Driver_Idno = obj.Driver_Idno;
                            TBH.Gross_Amnt = obj.Gross_Amnt;
                            TBH.Insentive_Amnt = obj.Insentive_Amnt;
                            TBH.Net_Amnt = obj.Net_Amnt;
                            TBH.remark = obj.remark;
                            TBH.RcptType_Idno = obj.RcptType_Idno;
                            TBH.Inst_No = obj.Inst_No;
                            TBH.Inst_Dt = obj.Inst_Dt;
                            TBH.Bank_Idno = obj.Bank_Idno;
                            TBH.Adv_Amnt = obj.Adv_Amnt;
                            db.SaveChanges();
                            chlnBoookId = TBH.Trip_Idno;
                            List<tblTripChlnDetl> ChlnDetl = db.tblTripChlnDetls.Where(rd => rd.TripHead_Idno == TripIdno).ToList();
                            foreach (var d in ChlnDetl)
                            {
                                db.tblTripChlnDetls.DeleteObject(d);
                                db.SaveChanges();
                            }

                            List<tblTripFuelDetl> FuelDetl = db.tblTripFuelDetls.Where(rd => rd.TripHead_Idno == TripIdno).ToList();
                            foreach (var d in FuelDetl)
                            {
                                db.tblTripFuelDetls.DeleteObject(d);
                                db.SaveChanges();
                            }
                            List<tblTripExpDetl> ExpDetl = db.tblTripExpDetls.Where(rd => rd.TripHead_Idno == TripIdno).ToList();
                            foreach (var d in ExpDetl)
                            {
                                db.tblTripExpDetls.DeleteObject(d);
                                db.SaveChanges();
                            }
                            List<tblTripTollDetl> TollDetl = db.tblTripTollDetls.Where(rd => rd.TripHead_Idno == TripIdno).ToList();
                            foreach (var d in TollDetl)
                            {
                                db.tblTripTollDetls.DeleteObject(d);
                                db.SaveChanges();
                            }


                            if (DtChln != null && DtChln.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in DtChln.Rows)
                                {
                                    Int64 ChlnIdno = Convert.ToInt64(Dr["Chln_Idno"]);
                                    tblTripChlnDetl objtblChlnDetl = new tblTripChlnDetl();
                                    objtblChlnDetl.TripHead_Idno = TripIdno;
                                    objtblChlnDetl.Chln_Idno = ChlnIdno;
                                    db.tblTripChlnDetls.AddObject(objtblChlnDetl);
                                    db.SaveChanges();
                                }
                            }
                            if (DtFuel != null && DtFuel.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in DtFuel.Rows)
                                {
                                    Int64 PbillIdno = Convert.ToInt64(Dr["PBill_Idno"]);
                                    tblTripFuelDetl objtblFuelDetl = new tblTripFuelDetl();
                                    objtblFuelDetl.TripHead_Idno = TripIdno;
                                    objtblFuelDetl.Pbill_Idno = Convert.ToInt64(Dr["PBill_Idno"]);
                                    db.tblTripFuelDetls.AddObject(objtblFuelDetl);
                                    db.SaveChanges();
                                }
                            }

                            if (DtExp != null && DtExp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in DtExp.Rows)
                                {
                                    tblTripExpDetl objtblExpDetl = new tblTripExpDetl();
                                    objtblExpDetl.TripHead_Idno = TripIdno;
                                    objtblExpDetl.Acnt_Idno = Convert.ToInt64(Dr["Acnt_Idno"]);
                                    objtblExpDetl.Amnt = Convert.ToInt64(Dr["Amnt"]);
                                    db.tblTripExpDetls.AddObject(objtblExpDetl);
                                    db.SaveChanges();
                                }
                            }

                            if (DtToll != null && DtToll.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in DtToll.Rows)
                                {
                                    tblTripTollDetl objtblTollDetl = new tblTripTollDetl();
                                    objtblTollDetl.TripHead_Idno = TripIdno;
                                    objtblTollDetl.Toll_Idno = Convert.ToInt64(Dr["Toll_Idno"]);
                                    objtblTollDetl.Amnt = Convert.ToInt64(Dr["Amnt"]);
                                    db.tblTripTollDetls.AddObject(objtblTollDetl);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    chlnBoookId = 0;
                }
                db.Connection.Close();
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

        public DataTable selectGrDetails(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 CityIdno, string strGrFrm, string con, Int32 FromCityIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DelvryPlcIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@ChlnDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@GrFrm", strGrFrm);
            objSqlPara[6] = new SqlParameter("@FromCityIdno", FromCityIdno);
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

        public DataTable SelectGRDetailTruckNo(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 CityIdno, string strGrFrm, string con, Int32 FromCityIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DelvryPlcIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@ChlnDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@GrFrm", strGrFrm);
            objSqlPara[6] = new SqlParameter("@FromCityIdno", FromCityIdno);
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

        public DataTable SelectDataForCommissinAmnt(string con, string AllItmIdno)
        {
            SqlParameter[] objsqlpara = new SqlParameter[2];
            objsqlpara[0] = new SqlParameter("@Action", "SelectDataForCommissinAmnt");
            objsqlpara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
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

        public tblCityMaster GetStateIdno(Int32 cityidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster lst = (from cm in db.tblCityMasters where cm.City_Idno == cityidno orderby cm.City_Name ascending select cm).FirstOrDefault();
                return lst;
            }
        }

        public DataSet selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spTripSheet", objSqlPara);
            DataSet DS = new DataSet();
            if (objDsTemp != null)
            {
                DS = objDsTemp;
            }
            return DS;
        }

        public int Delete(Int64 HeadId)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {

                    tblTripHead CH = db.tblTripHeads.Where(h => h.Trip_Idno == HeadId).FirstOrDefault();
                    List<tblChlnBookDetl> CD = db.tblChlnBookDetls.Where(d => d.ChlnBookHead_Idno == HeadId).ToList();
                    List<tblTripChlnDetl> TD = db.tblTripChlnDetls.Where(d => d.TripHead_Idno == HeadId).ToList();
                    if (CH != null)
                    {
                        List<tblTripChlnDetl> ChlnDetl = db.tblTripChlnDetls.Where(rd => rd.TripHead_Idno == HeadId).ToList();
                        foreach (var d in ChlnDetl)
                        {
                            db.tblTripChlnDetls.DeleteObject(d);
                            db.SaveChanges();
                        }

                        List<tblTripFuelDetl> FuelDetl = db.tblTripFuelDetls.Where(rd => rd.TripHead_Idno == HeadId).ToList();
                        foreach (var d in FuelDetl)
                        {
                            db.tblTripFuelDetls.DeleteObject(d);
                            db.SaveChanges();
                        }
                        List<tblTripExpDetl> ExpDetl = db.tblTripExpDetls.Where(rd => rd.TripHead_Idno == HeadId).ToList();
                        foreach (var d in ExpDetl)
                        {
                            db.tblTripExpDetls.DeleteObject(d);
                            db.SaveChanges();
                        }
                        List<tblTripTollDetl> TollDetl = db.tblTripTollDetls.Where(rd => rd.TripHead_Idno == HeadId).ToList();
                        foreach (var d in TollDetl)
                        {
                            db.tblTripTollDetls.DeleteObject(d);
                            db.SaveChanges();
                        }
                        foreach (var d in TD)
                        {
                            tblChlnBookHead objChlnHead = (from obj in db.tblChlnBookHeads where obj.Chln_Idno == d.Chln_Idno select obj).FirstOrDefault();
                            objChlnHead.Trip_Billed = false;
                            db.SaveChanges();
                        }

                        db.tblTripHeads.DeleteObject(CH);
                        db.SaveChanges();
                        int intValue = 1;
                        if (intValue > 0)
                        {
                            value = 1;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                value = -1;
            }
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

        public IList BindPetrolPump(Int64[] Ids)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblPBillHeads
                        join AM in db.AcntMasts on obj.Prty_Idno equals AM.Acnt_Idno
                        where Ids.Contains(obj.PBillHead_Idno)
                        select new
                            {
                                PbillNo = obj.PBillHead_No,
                                PbillDate = obj.PBillHead_Date,
                                PumpName = AM.Acnt_Name,
                                PumpIdno = AM.Acnt_Idno,
                                Amount = obj.Net_Amnt,
                                PbillIdno = obj.PBillHead_Idno
                            }).ToList();
            }

        }

        public IList BindTollPump(Int64[] Ids)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tbltollmasters
                        where obj.Status == true && Ids.Contains(obj.Toll_id)
                        select new
                        {
                            TollName = obj.Tolltax_name,
                            TollIdno = obj.Toll_id,
                            Amount = obj.Amount
                        }).ToList();
            }

        }

        public IList BindChallan(Int64[] Ids)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblChlnBookHeads
                        join pay in db.tblPayToOwnAccs on obj.Chln_Idno equals pay.Chln_IdNo into temp
                        from ctemp in temp.DefaultIfEmpty()
                        where Ids.Contains(obj.Chln_Idno)
                        select new
                        {
                            ChlnNo = obj.Chln_No,
                            ChlnIdno = obj.Chln_Idno,
                            DriverIdno = obj.Driver_Idno,
                            Date = obj.Chln_Date,
                            Gross = (obj.Gross_Amnt),
                            Tot = (from pay1 in db.tblPayToOwnAccs where obj.Chln_Idno == pay1.Chln_IdNo select pay1.Amnt).Sum(),
                            Advance = (obj.Adv_Amnt) + ((from pay1 in db.tblPayToOwnAccs where pay1.Chln_IdNo == obj.Chln_Idno select pay1.Amnt).Sum() ?? 0),
                            Desial = (obj.Diesel_Amnt),
                            Total = (obj.Net_Amnt) - ((from pay1 in db.tblPayToOwnAccs where pay1.Chln_IdNo == obj.Chln_Idno select pay1.Amnt).Sum() ?? 0)
                        }).GroupBy(x => x.ChlnIdno).Select(x => x.FirstOrDefault()).ToList();
            }

        }

        public IList BindExpens()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from lst in db.AcntMasts
                        where lst.AGrp_Idno == 11
                        select new
                        {
                            AcntName = lst.Acnt_Name,
                            AcntIdno = lst.Acnt_Idno
                        }).ToList();
            }
        }

        public IList BindExpensReport()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from TH in db.tblTripHeads
                        join TE in db.tblTripExpDetls on TH.Trip_Idno equals TE.TripHead_Idno
                        join AM in db.AcntMasts on TE.Acnt_Idno equals AM.Acnt_Idno
                        select new
                        {
                            ExpIdno = TE.Acnt_Idno,
                            ExpName = AM.Acnt_Name
                        }
                        ).GroupBy(x => x.ExpName).Select(x => x.FirstOrDefault()).ToList()
                         .Union
                         (from TH1 in db.tblTripHeads
                          join FE in db.tblTripFuelDetls on TH1.Trip_Idno equals FE.TripHead_Idno
                          join PD in db.tblPBillDetls on FE.Pbill_Idno equals PD.PBillHead_Idno
                          join IP in db.tblItemMastPurs on PD.Item_Idno equals IP.Item_Idno
                          select new
                          {
                              ExpIdno = PD.Item_Idno,
                              ExpName = IP.Item_Name
                          }
                          ).GroupBy(x => x.ExpName).Select(x => x.FirstOrDefault()).ToList()
                          .Union
                          (from TH2 in db.tblTripHeads
                           join TT in db.tblTripTollDetls on TH2.Trip_Idno equals TT.TripHead_Idno
                           join TM in db.tbltollmasters on TT.Toll_Idno equals TM.Toll_id
                           select new { ExpIdno = TT.Toll_Idno, ExpName = TM.Tolltax_name }
                          ).GroupBy(x => x.ExpName).Select(x => x.FirstOrDefault()).ToList();
            }
        }

        public IList SearchGrD(int SearchType, DateTime? DocFromDate, DateTime? DocToDate, Int32 DocNo, Int64 LocIdno, Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (SearchType == 0)
                {
                    var Result = (from obj in db.tblChlnBookHeads
                                  join Dt in db.tblChlnBookDetls on obj.Chln_Idno equals Dt.ChlnBookHead_Idno
                                  where obj.BaseCity_Idno == LocIdno && obj.Truck_Idno == TruckIdno && (obj.Trip_Billed == null || obj.Trip_Billed == false) && Dt.Delivered == true
                                  select new
                                  {
                                      DocNo = obj.Chln_No,
                                      DocDate = obj.Chln_Date,
                                      DocAmnt = obj.Net_Amnt,
                                      DocType = "Challan",
                                      DocIdno = obj.Chln_Idno,
                                      Driver_Idno=obj.Driver_Idno
                                  }).Distinct().ToList();
                    if (DocToDate != null)
                    {
                        Result = Result.Where(l => Convert.ToDateTime(l.DocDate).Date <= Convert.ToDateTime(DocToDate).Date).ToList();
                    }
                    if (DocFromDate != null)
                    {
                        Result = Result.Where(l => Convert.ToDateTime(l.DocDate).Date >= Convert.ToDateTime(DocFromDate).Date).ToList();
                    }
                    if (Result.Count > 0)
                    {
                        if (DocNo != 0)
                        {
                            Result = Result.Where(r => r.DocNo == Convert.ToString(DocNo)).ToList();
                        }
                    }
                    return Result;
                }
                else if (SearchType == 1)
                {
                    var Result = (from obj in db.tblPBillHeads
                                  join AM in db.AcntMasts on obj.Prty_Idno equals AM.Acnt_Idno
                                  where obj.LorryIdno == TruckIdno && obj.Billed==false && obj.PBillHead_Date >= DocFromDate && obj.PBillHead_Date <= DocToDate
                                  select new
                                  {
                                      DocNo = obj.PBillHead_No,
                                      DocDate = obj.PBillHead_Date,
                                      DocAmnt = obj.Net_Amnt,
                                      DocType = AM.Acnt_Name,
                                      DocIdno = obj.PBillHead_Idno,
                                      Driver_Idno=0
                                  }).ToList();

                    if (Result.Count > 0)
                    {
                        if (DocNo != 0)
                        {
                            Result = Result.Where(r => r.DocNo == DocNo).ToList();
                        }
                    }
                    return Result;
                }
                else if (SearchType == 2)
                {
                    return (from obj in db.tbltollmasters
                            where obj.Status == true
                            select new
                            {
                                DocNo = "--",
                                DocDate = "--",
                                DocAmnt = obj.Amount,
                                DocType = obj.Tolltax_name,
                                DocIdno = obj.Toll_id,
                                Driver_Idno = 0
                            }).ToList();
                }
                 else 
                {
                    var Result = (from obj in db.tblChlnBookHeads
                                  join Dt in db.tblChlnBookDetls on obj.Chln_Idno equals Dt.ChlnBookHead_Idno
                                  join gr in db.TblGrHeads on Dt.GR_Idno equals gr.GR_Idno
                                  where obj.BaseCity_Idno == LocIdno && obj.Truck_Idno == TruckIdno && (obj.Trip_Billed == null || obj.Trip_Billed == false) && Dt.Delivered == true
                                  select new
                                  {
                                      DocNo = obj.Chln_No,
                                      DocDate = obj.Chln_Date,
                                      DocAmnt = obj.Net_Amnt,
                                      DocType = "Challan",
                                      DocIdno = obj.Chln_Idno,
                                      Driver_Idno = obj.Driver_Idno,
                                      grno  = gr.Gr_No
                                  }).Distinct().ToList();
                    if (DocToDate != null)
                    {
                        Result = Result.Where(l => Convert.ToDateTime(l.DocDate).Date <= Convert.ToDateTime(DocToDate).Date).ToList();
                    }
                    if (DocFromDate != null)
                    {
                        Result = Result.Where(l => Convert.ToDateTime(l.DocDate).Date >= Convert.ToDateTime(DocFromDate).Date).ToList();
                    }
                    if (Result.Count > 0)
                    {
                        if (DocNo != 0)
                        {
                            Result = Result.Where(r => r.grno == DocNo).ToList();
                        }
                    }
                    return Result;
                }
            }
        }


        public IList SelectRep1(DateTime? FromDate, DateTime? ToDate, Int32 DocNo, Int64 LocIdno, Int64 TruckIdno, Int64 DriverIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var LstChlnIds = (from CL in db.tblTripChlnDetls where CL.TripHead_Idno == 1 select CL.Chln_Idno).ToList(); 
                var Lst = (from l in db.tblTripHeads
                           join ChlnDetl in db.tblTripChlnDetls on l.Trip_Idno equals ChlnDetl.TripHead_Idno into C
                           from CD in C.DefaultIfEmpty()
                           join FuelDetl in db.tblTripFuelDetls on l.Trip_Idno equals FuelDetl.TripHead_Idno into F
                           from FD in F.DefaultIfEmpty()
                           //join Expn in db.tblTripExpDetls on l.Trip_Idno equals Expn.TripHead_Idno into E
                           //from ED in E.DefaultIfEmpty()
                           join TollDetl in db.tblTripTollDetls on l.Trip_Idno equals TollDetl.TripHead_Idno into T
                           from TD in T.DefaultIfEmpty()
                           join DM in db.DriverMasts on l.Driver_Idno equals DM.Driver_Idno
                           join LM in db.LorryMasts on l.Truck_Idno equals LM.Lorry_Idno
                           join CM in db.tblCityMasters on l.BaseCity_Idno equals CM.City_Idno
                           where l.Trip_Date >= FromDate && l.Trip_Date <= ToDate
                           select new
                           {
                               Trip_Idno =l.Trip_Idno,
                               Trip_No = l.Trip_No,
                               Trip_Date = l.Trip_Date,
                               Location = CM.City_Name,
                               TruckNo =LM.Lorry_No,
                               Driver = DM.Driver_Name,
                               KMS = l.FKms,
                               NetAmnt = l.Net_Amnt,
                               GrossAmnt =l.Gross_Amnt,
                               IncentiveAmnt =l.Insentive_Amnt
                               //ChlnAmnt = (from ND in db.tblChlnBookHeads where ND.Chln_Idno == CD.Chln_Idno select ND.Net_Amnt).Sum(),
                               //FuelAmnt =(from ND in db.tblPBillHeads where ND.PBillHead_Idno==FD.Pbill_Idno select ND.Net_Amnt).Sum(),
                               //ExpnAmnt = (from ND in db.tblTripExpDetls where ND.TripHead_Idno == l.Trip_Idno select ND.Amnt).Sum(),
                               //TollAmnt  = (from ND in db.tbltollmasters where ND.Toll_id==TD.Toll_Idno select ND.Amount).Sum()
                           }).ToList();

                if (Lst.Count > 0)
                {
                    
                    //if (DocNo != 0)
                    //{
                    //    Lst = Lst.Where(r => r.Trip_No == Convert.ToString(DocNo)).ToList();
                    //}
                }
                return Lst;
            }
        }

        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int32 DocNo, Int64 LocIdno, Int64 TruckIdno, Int64 DriverIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", LocIdno);
            objSqlPara[4] = new SqlParameter("@DriverIdno", DriverIdno);
            objSqlPara[5] = new SqlParameter("@LorryIdno", TruckIdno);
            objSqlPara[6] = new SqlParameter("@TripNo", DocNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spTripSheet", objSqlPara);
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

        public DataTable SelectExpRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 ExpNo, Int64 TruckIdno, Int64 DriverIdno, string ExpName, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@DriverIdno", DriverIdno);
            objSqlPara[4] = new SqlParameter("@LorryIdno", TruckIdno);
            objSqlPara[5] = new SqlParameter("@ExpNo", ExpNo);
            objSqlPara[6] = new SqlParameter("@ExpName", ExpName);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spTripSheet", objSqlPara);
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
