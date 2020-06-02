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
using WebTransport;

namespace WebTransport.DAL
{
    public class ClaimFrmDAL
    {
        public IList SelectForSearch(DateTime? DateFrom, DateTime? DateTo, string strSBillNo,Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from SBH in db.tblSBillHeads
                           join SBD in db.tblSBillDetls on SBH.SBillHead_Idno equals SBD.SBillHead_Idno
                           join acnts in db.AcntMasts on SBH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on SBH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on SBD.Item_Idno equals stck.SerlDetl_id
                           where (SBD.Claim_Idno == 0 || SBD.Claim_Idno==null)
                           select new
                           {
                               SbillHeadIdno = SBH.SBillHead_Idno,
                               SerialNo = stck.SerialNo,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = SBH.Prefix_No,
                               SbillNo = SBH.SBill_No,
                               SBillDate = SBH.SBillHead_Date,
                               Location = SBH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = SBH.Year_Idno
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.Location == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.SBillDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.SBillDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (strSBillNo !="")
                {
                    lst = (from l in lst where ((l.PrefNo)+""+(l.SbillNo)) == strSBillNo select l).ToList();
                }
                return lst;
            }
        }

        public IList SearchByParty(Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stck in db.Stckdetls
                           join cifrom in db.tblCityMasters on stck.Loc_Idno equals cifrom.City_Idno
                           join AM in db.AcntMasts on stck.Acnt_Idno equals AM.Acnt_Idno
                           where (stck.Claim_Idno == 0 || stck.Claim_Idno ==null)
                           select new
                           {
                               SerialNo = stck.SerialNo,
                               SerialIdno = stck.SerlDetl_id,
                               Location = stck.Loc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = AM.Acnt_Name,
                               PartyIdno = AM.Acnt_Idno,
                               YearIdno = stck.yearId
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.Location == intFromLocation select l).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                return lst;
            }
        }

        public IList Exists(Int64 HeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimToComHeads where CH.ClaimHead_Idno == HeadIdno select CH.ClaimToComHead_Idno).ToList();
                return lst; ;
            }
        }

        public DataTable SelectClaimDetl(string con, Int64 iYearId, string AllIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "GETDETAILSFORCLAIM");
            objSqlPara[1] = new SqlParameter("@StckDetlIdno", AllIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spClaim", objSqlPara);
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
        

        public DataTable Select(string con, Int64 iYearId, string AllIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "GETDETAILS");
            objSqlPara[1] = new SqlParameter("@StckDetlIdno", AllIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spClaim", objSqlPara);
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

        public DataTable SelectClaimDetails(string con, Int64 ClaimHeadIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "CLAIMDETAILS");
            objSqlPara[1] = new SqlParameter("@ClaimHeadIdNo", ClaimHeadIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spClaim", objSqlPara);
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
        public DataTable SelectClaimParty(string con, Int64 ClaimHeadIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "CLAIMDETAILSONPARTYCASE");
            objSqlPara[1] = new SqlParameter("@ClaimHeadIdNo", ClaimHeadIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spClaim", objSqlPara);
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
        

        public Int64 Insert(tblClaimHead obj, DataTable Dttemp)
        {
            Int64 ClaimHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblClaimHead Claimhead = new tblClaimHead();
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblClaimHead CH = db.tblClaimHeads.Where(ch => ch.Claim_No == obj.Claim_No && ch.Prefix_No == obj.Prefix_No && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            ClaimHeadIdno = -1;
                        }
                        else
                        {
                            db.tblClaimHeads.AddObject(obj);
                            db.SaveChanges();
                            ClaimHeadIdno = obj.ClaimHead_Idno;
                            if (Dttemp != null && Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in Dttemp.Rows)
                                {
                                    tblClaimDetl objtblClaimDetl = new tblClaimDetl();
                                    objtblClaimDetl.ClaimHead_Idno = ClaimHeadIdno;
                                    objtblClaimDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                    objtblClaimDetl.SBillHead_Idno = Convert.ToInt64(Dr["SbillIdno"]);
                                    objtblClaimDetl.Defect_Remark = Convert.ToString(Dr["DefectRemark"]);
                                    objtblClaimDetl.VechApp_Details = Convert.ToString(Dr["VechAppDetails"]);
                                    db.tblClaimDetls.AddObject(objtblClaimDetl);
                                    db.SaveChanges();
                                }
                            }
                            dbTran.Commit();
                        }
                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
                return ClaimHeadIdno;
            }
        }
        public Int64 Update(tblClaimHead obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 ClaimHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblClaimHead CH = db.tblClaimHeads.Where(ch => ch.Claim_No == obj.Claim_No && ch.ClaimHead_Idno != Head_Idno && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            ClaimHeadIdno = -1;
                        }
                        else
                        {
                            tblClaimHead CHead = db.tblClaimHeads.Where(ch => ch.ClaimHead_Idno== Head_Idno).FirstOrDefault();
                            if (CHead != null)
                            {
                                db.SaveChanges();
                                ClaimHeadIdno = CHead.ClaimHead_Idno;

                                List<tblClaimDetl> ClaimDetl = db.tblClaimDetls.Where(cd => cd.ClaimHead_Idno == Head_Idno).ToList();
                                foreach (tblClaimDetl tcd in ClaimDetl)
                                {
                                    db.tblClaimDetls.DeleteObject(tcd);
                                    db.SaveChanges();
                                }
                                if (Dttemp != null && Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        tblClaimDetl objtblClaimDetl = new tblClaimDetl();
                                        objtblClaimDetl.ClaimHead_Idno = ClaimHeadIdno;
                                        objtblClaimDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                        objtblClaimDetl.SBillHead_Idno = Convert.ToInt64(Dr["SbillIdno"]);
                                        objtblClaimDetl.Defect_Remark = Convert.ToString(Dr["DefectRemark"]);
                                        objtblClaimDetl.VechApp_Details = Convert.ToString(Dr["VechAppDetails"]);
                                        db.tblClaimDetls.AddObject(objtblClaimDetl);
                                        db.SaveChanges();
                                    }
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
            return ClaimHeadIdno;
        }


        public tblClaimHead Select_ClaimHead(Int64 ClaimHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblClaimHeads.Where(tch => (tch.ClaimHead_Idno == ClaimHead_Idno)).FirstOrDefault();
            }
        }

        public IList Select_ClaimDetl(Int64 ClaimHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Ilst = (from TCD in db.tblClaimDetls
                            join SD in db.Stckdetls on TCD.StckDetl_Idno equals SD.SerlDetl_id
                            join CM in db.tblCityMasters on SD.Loc_Idno equals CM.City_Idno
                            join IM in db.tblItemMastPurs on SD.ItemIdno equals IM.Item_Idno
                            join TSB in db.tblSBillHeads on TCD.SBillHead_Idno equals TSB.SBillHead_Idno
                            join AM in db.AcntMasts on TSB.Prty_Idno equals AM.Acnt_Idno
                            where TCD.ClaimHead_Idno == ClaimHead_Idno
                            select new
                            {
                                SD.SerlDetl_id,
                                SD.SerialNo,
                                
                            }).ToList();

                return Ilst;
            }
        }

        public Int32 GetClaimMax(Int64 fromCity, string Prefix, Int64 YeaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from TCH in db.tblClaimHeads where TCH.FromLoc_Idno == fromCity && TCH.Prefix_No == Prefix && TCH.Year_Idno == YeaIdno select TCH.Claim_No).Max() + 1;
                if (max != null) { return Convert.ToInt32(max); } else { return 1; }

            }
        }

        public IList CountAll(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intCompIdno, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimHeads
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts1 in db.AcntMasts on CH.Comp_Idno equals acnts1.Acnt_Idno
                           select new
                           {
                               CH.ClaimHead_Idno,
                               ClaimDate = CH.ClaimHead_Date,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = CH.Claim_No,
                               YearIdno = CH.Year_Idno,
                               PartyIdno = acnts.Acnt_Idno,
                               PartyName = acnts.Acnt_Name,
                               CompanyIdno = acnts1.Acnt_Idno,
                               CompanyName = acnts1.Acnt_Name,
                               CityIdno = cifrom.City_Idno,
                               CityName = cifrom.City_Name,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.CityIdno == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (intCompIdno > 0)
                {
                    lst = (from l in lst where l.CompanyIdno == intCompIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }


        public IList SearchList(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation,Int64 intCompIdno, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimHeads
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts1 in db.AcntMasts on CH.Comp_Idno equals acnts1.Acnt_Idno
                           select new
                           {
                                CH.ClaimHead_Idno,
                                ClaimDate=CH.ClaimHead_Date,
                                PrefNo = CH.Prefix_No,
                                ClaimNo=CH.Claim_No,
                                YearIdno =CH.Year_Idno,
                                PartyIdno = acnts.Acnt_Idno,
                                PartyName=acnts.Acnt_Name,
                                CompanyIdno = acnts1.Acnt_Idno,
                                CompanyName=acnts1.Acnt_Name,
                                CityIdno = cifrom.City_Idno,
                                CityName=cifrom.City_Name,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.CityIdno == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (intCompIdno > 0)
                {
                    lst = (from l in lst where l.CompanyIdno == intCompIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }

        public int Delete(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblClaimHead qth = db.tblClaimHeads.Where(h => h.ClaimHead_Idno == HeadId).FirstOrDefault();
                List<tblClaimDetl> qtd = db.tblClaimDetls.Where(d => d.ClaimHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblClaimDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblClaimHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public Int64 SelectSaleBillNo(Int64 ClaimIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = Convert.ToInt64((from SH in db.tblSBillHeads
                                             join SD in db.tblSBillDetls on SH.SBillHead_Idno equals SD.SBillHead_Idno
                                             where SD.Claim_Idno == ClaimIdno select  SH.SBill_No).FirstOrDefault());
                return lst;
            }
        }

        //public Int64 SelectClaimByIdno(Int64 ClaimIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        Int64 lst = Convert.ToInt64((from SBH in db.tblClaimHeads where SBH.ClaimHead_Idno == ClaimIdno select SBH.ClaimHead_Idno).FirstOrDefault());
        //        return lst;
        //    }
        //}

        public IList SelectSearchListReport(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intCompIdno, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimHeads
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts1 in db.AcntMasts on CH.Comp_Idno equals acnts1.Acnt_Idno
                           select new
                           {
                               CH.ClaimHead_Idno,
                               ClaimDate = CH.ClaimHead_Date,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = CH.Claim_No,
                               YearIdno = CH.Year_Idno,
                               PartyIdno = acnts.Acnt_Idno,
                               PartyName = acnts.Acnt_Name,
                               CompanyIdno = acnts1.Acnt_Idno,
                               CompanyName = acnts1.Acnt_Name,
                               CityIdno = cifrom.City_Idno,
                               CityName = cifrom.City_Name,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.CityIdno == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (intCompIdno > 0)
                {
                    lst = (from l in lst where l.CompanyIdno == intCompIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }

    }
}
