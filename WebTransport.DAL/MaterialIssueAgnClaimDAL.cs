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
    public class MaterialIssueAgnClaimDAL
    {
        public IList SearchList(DateTime? DateFrom, DateTime? DateTo, string strIssueNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblMatIssueAgnClaimHeads
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           select new
                           {
                               CH.MatIssAgnClaimHead_Idno,
                               IssueDate = CH.MatIssAgnClaimHead_Date,
                               PrefNo = CH.Prefix_No,
                               IssueNo = CH.MatIss_No,
                               YearIdno = CH.Year_Idno,
                               PartyIdno = acnts.Acnt_Idno,
                               PartyName = acnts.Acnt_Name,
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
                    lst = lst.Where(l => Convert.ToDateTime(l.IssueDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.IssueDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (strIssueNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.IssueNo)) == strIssueNo select l).ToList();
                }
                return lst;
            }
        }


        public IList Select(Int64 intYearIdno, string ClaimIdno)
        {
            Int64[] ClaimIds = ClaimIdno.Split(',').Select(str => Int64.Parse(str)).ToArray();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimToComHeads
                           join CD in db.tblClaimToComDetls on CH.ClaimToComHead_Idno equals CD.ClaimToComHead_Idno
                           join stck in db.Stckdetls on CD.New_SerialNo equals stck.SerialNo
                           join IM in db.tblItemMastPurs on stck.ItemIdno equals IM.Item_Idno
                           where CH.Year_Idno == intYearIdno
                           select new
                           {
                               ClaimHeadIdno = CH.ClaimToComHead_Idno,
                               SerialNo = stck.SerialNo,
                               ItemName = IM.Item_Name,
                               CRcvdDate = CH.ClaimToComRec_Date,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                           }).ToList();
                lst = lst.Where(s => ClaimIds.Contains(s.ClaimHeadIdno)).ToList();
                return lst; ;

            }
        }

        public IList SelectForSearch(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from HEAD in db.tblClaimToComHeads
                           join AM in db.AcntMasts on HEAD.Prty_Idno equals AM.Acnt_Idno
                           join CM in db.tblCityMasters on HEAD.FromLoc_Idno equals CM.City_Idno
                           join AM1 in db.AcntMasts on HEAD.Comp_Idno equals AM1.Acnt_Idno
                           where (AM.Acnt_Type == 2 || AM.Acnt_Type == 6 || AM.Acnt_Type==11) && (AM.Status == true) && (AM1.ASubGrp_Idno == 2) && (HEAD.Against == 2) && (HEAD.MatIssAgnClaimHead_Idno == 0 || HEAD.MatIssAgnClaimHead_Idno==null)
                           select new
                           {
                               HeadIdno = HEAD.ClaimToComHead_Idno,
                               HeadDate = HEAD.ClaimToComRec_Date,
                               PrefNo = HEAD.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == HEAD.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               PrtyIdno = AM.Acnt_Idno,
                               PrtyName = AM.Acnt_Name,
                               FromCityIdno = CM.City_Idno,
                               FromCityName = CM.City_Name,
                               CompName = AM1.Acnt_Name,
                               CompIdno = AM1.Acnt_Idno,
                               YearIdNo = HEAD.Year_Idno
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdNo == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.FromCityIdno == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.HeadDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.HeadDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PrtyIdno == intPrtyIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }

        

        //PEEYUSH
        public DataTable SelectDetails(string con, Int64 iYearId, string AllSBillIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "GETDETAILS");
            objSqlPara[1] = new SqlParameter("@ClaimNo", AllSBillIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spMatIssueAgnClaim", objSqlPara);
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

        public IList SelectClaimDetails(Int64 MatIssueHeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from MtIssueHead in db.tblMatIssueAgnClaimHeads
                           join MTissueDetl in db.tblMatIssueAgnClaimDetls on MtIssueHead.MatIssAgnClaimHead_Idno equals MTissueDetl.MatIssAgnClaimHead_Idno
                           join SD in db.Stckdetls on MTissueDetl.StckDetl_Idno equals SD.SerlDetl_id
                           join CTCH in db.tblClaimToComHeads on MtIssueHead.ClaimToComHead_Idno equals CTCH.ClaimToComHead_Idno
                           join CH in db.tblClaimHeads on CTCH.ClaimHead_Idno equals CH.ClaimHead_Idno
                           join IM in db.tblItemMastPurs on SD.ItemIdno equals IM.Item_Idno
                           where MTissueDetl.MatIssAgnClaimHead_Idno == MatIssueHeadId
                           select new
                           {
                               HeadIdno = MTissueDetl.MatIssAgnClaimHead_Idno,
                               ClaimHeadIdno=CTCH.ClaimToComHead_Idno,
                               SerialIdno=SD.SerlDetl_id,
                               SerialNo = SD.SerialNo,
                               ItemName = IM.Item_Name,
                               ClaimNo = CH.Claim_No,
                               CRcvdDate = CTCH.ClaimToComRec_Date,
                           }).ToList();
                return lst; ;

            }
        }


        public Int64 Insert(tblMatIssueAgnClaimHead obj, DataTable Dttemp)
        {
            Int64 MatIssueHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblMatIssueAgnClaimHead Claimhead = new tblMatIssueAgnClaimHead();
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblMatIssueAgnClaimHead CH = db.tblMatIssueAgnClaimHeads.Where(ch => ch.MatIss_No == obj.MatIss_No && ch.Prefix_No == obj.Prefix_No && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            MatIssueHeadIdno = -1;
                        }
                        else
                        {
                            db.tblMatIssueAgnClaimHeads.AddObject(obj);
                            db.SaveChanges();
                            MatIssueHeadIdno = obj.MatIssAgnClaimHead_Idno;
                            if (Dttemp != null && Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in Dttemp.Rows)
                                {
                                    tblMatIssueAgnClaimDetl objDetl = new tblMatIssueAgnClaimDetl();
                                    objDetl.MatIssAgnClaimHead_Idno = MatIssueHeadIdno;
                                    objDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                    db.tblMatIssueAgnClaimDetls.AddObject(objDetl);
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
                return MatIssueHeadIdno;
            }
        }
        public Int64 Update(tblMatIssueAgnClaimHead obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 MatIssueHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblMatIssueAgnClaimHead CH = db.tblMatIssueAgnClaimHeads.Where(ch => ch.MatIss_No == obj.MatIss_No && ch.MatIssAgnClaimHead_Idno != Head_Idno && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            MatIssueHeadIdno = -1;
                        }
                        else
                        {
                            tblMatIssueAgnClaimHead CHead = db.tblMatIssueAgnClaimHeads.Where(ch => ch.MatIssAgnClaimHead_Idno == Head_Idno).FirstOrDefault();
                            if (CHead != null)
                            {
                                db.SaveChanges();
                                MatIssueHeadIdno = CHead.MatIssAgnClaimHead_Idno;

                                List<tblMatIssueAgnClaimDetl> ClaimDetl = db.tblMatIssueAgnClaimDetls.Where(cd => cd.MatIssAgnClaimHead_Idno == Head_Idno).ToList();
                                foreach (tblMatIssueAgnClaimDetl tcd in ClaimDetl)
                                {
                                    db.tblMatIssueAgnClaimDetls.DeleteObject(tcd);
                                    db.SaveChanges();
                                }
                                if (Dttemp != null && Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        tblMatIssueAgnClaimDetl objDetl = new tblMatIssueAgnClaimDetl();
                                        objDetl.MatIssAgnClaimHead_Idno = MatIssueHeadIdno;
                                        objDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                        db.tblMatIssueAgnClaimDetls.AddObject(objDetl);
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
            return MatIssueHeadIdno;
        }


        public tblMatIssueAgnClaimHead SelectHead(Int64 HeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblMatIssueAgnClaimHeads.Where(tch => (tch.MatIssAgnClaimHead_Idno == HeadIdno)).FirstOrDefault();
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
        //Peeyush
        public Int32 GetMatIssueMax(Int64 fromCity, string Prefix, Int64 YeaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from Head in db.tblMatIssueAgnClaimHeads where Head.FromLoc_Idno== fromCity && Head.Prefix_No == Prefix && Head.Year_Idno == YeaIdno select Head.MatIss_No).Max() + 1;
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
                tblMatIssueAgnClaimHead qth = db.tblMatIssueAgnClaimHeads.Where(h => h.MatIssAgnClaimHead_Idno == HeadId).FirstOrDefault();
                List<tblMatIssueAgnClaimDetl> qtd = db.tblMatIssueAgnClaimDetls.Where(d => d.MatIssAgnClaimHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblMatIssueAgnClaimDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblMatIssueAgnClaimHeads.DeleteObject(qth);
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
