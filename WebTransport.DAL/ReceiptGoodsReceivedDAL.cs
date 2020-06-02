using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using System.Web;

namespace WebTransport.DAL
{
    public class ReceiptGoodsReceivedDAL
    {
        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }
        public Int64 Insert(tblRcptGoodHead rcptGoodHead, List<tblRcptGoodDetl> rcptGoodDetl)
        {
            Int64 RcptGoodHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblRcptGoodHead RH = db.tblRcptGoodHeads.Where(rh => rh.RcptGoodHead_No == rcptGoodHead.RcptGoodHead_No && rh.FromCity_Idno == rcptGoodHead.FromCity_Idno && rh.Year_Idno == rcptGoodHead.Year_Idno).FirstOrDefault();
                        if (RH != null)
                        {
                            RcptGoodHeadId = -1;
                        }
                        else
                        {
                            db.tblRcptGoodHeads.AddObject(rcptGoodHead);
                            db.SaveChanges();
                            RcptGoodHeadId = rcptGoodHead.RcptGoodHead_Idno;
                            if (rcptGoodDetl.Count > 0)
                            {
                                foreach (tblRcptGoodDetl rgd in rcptGoodDetl)
                                {
                                    rgd.RcptGoodHead_Idno = RcptGoodHeadId;
                                    db.tblRcptGoodDetls.AddObject(rgd);
                                    db.SaveChanges();
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
            return RcptGoodHeadId;
        }
        public Int64 Update(tblRcptGoodHead rcptGoodHead, List<tblRcptGoodDetl> rcptGoodDetl)
        {
            Int64 RcptGoodHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblRcptGoodHead RH = db.tblRcptGoodHeads.Where(rh => rh.RcptGoodHead_No == rcptGoodHead.RcptGoodHead_No && rh.FromCity_Idno == rcptGoodHead.FromCity_Idno && rh.Year_Idno == rcptGoodHead.Year_Idno && rh.RcptGoodHead_Idno != rcptGoodHead.RcptGoodHead_Idno).FirstOrDefault();
                        if (RH != null)
                        {
                            RcptGoodHeadId = -1;
                        }
                        else
                        {
                            tblRcptGoodHead rgh = db.tblRcptGoodHeads.Where(rh => rh.RcptGoodHead_Idno == rcptGoodHead.RcptGoodHead_Idno).FirstOrDefault();
                            if (rgh != null)
                            {
                                rgh.Agent_Idno = rcptGoodHead.Agent_Idno;
                                rgh.Date_Modified = DateTime.Now;
                                rgh.DelvryPlc_Idno = rcptGoodHead.DelvryPlc_Idno;
                                rgh.FromCity_Idno = rcptGoodHead.FromCity_Idno;
                                rgh.GRHead_Idno = rcptGoodHead.GRHead_Idno;
                                rgh.RcptGoodHead_Date = rcptGoodHead.RcptGoodHead_Date;
                                rgh.RcptGoodHead_No = rcptGoodHead.RcptGoodHead_No;
                                rgh.Recevr_Idno = rcptGoodHead.Recevr_Idno;
                                rgh.Sender_Idno = rcptGoodHead.Sender_Idno;
                                rgh.ToCity_Idno = rcptGoodHead.ToCity_Idno;
                                rgh.Sender_No = rcptGoodHead.Sender_No;
                                rgh.Year_Idno = rcptGoodHead.Year_Idno;
                                db.SaveChanges();
                                RcptGoodHeadId = rcptGoodHead.RcptGoodHead_Idno;
                                List<tblRcptGoodDetl> rcptdetl = db.tblRcptGoodDetls.Where(rd => rd.RcptGoodHead_Idno == rcptGoodHead.RcptGoodHead_Idno).ToList();
                                foreach (tblRcptGoodDetl rgd in rcptdetl)
                                {
                                    db.tblRcptGoodDetls.DeleteObject(rgd);
                                    db.SaveChanges();
                                }
                                foreach (tblRcptGoodDetl rgd in rcptGoodDetl)
                                {
                                    rgd.RcptGoodHead_Idno = RcptGoodHeadId;
                                    db.tblRcptGoodDetls.AddObject(rgd);
                                    db.SaveChanges();
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
            return RcptGoodHeadId;
        }

        public Int64 GetMaxNo(Int64 YearIdno, Int64 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from hd in db.tblRcptGoodHeads where hd.Year_Idno == YearIdno && hd.FromCity_Idno == LocIdno select hd.RcptGoodHead_No).Max() + 1;
                return Convert.ToInt64(max);
            }

        }

        public IList SelectAcntMastByType(Int64 AcntTypeId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.AcntMasts.Where(am => am.Acnt_Type == AcntTypeId && am.Status == true).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
            }
        }

        public IList SelectAcntMastByTypePopulate(Int64 AcntTypeId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.AcntMasts.Where(am => am.Acnt_Type == AcntTypeId).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
            }
        }

        public tblRcptGoodHead SelectByReceiptGoodReceivedByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblRcptGoodHeads.Where(h => h.RcptGoodHead_Idno == HeadId).FirstOrDefault();
            }
        }

        public IList SelectReceiptGoodDetailByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblRcptGoodDetls
                           join um in db.UOMMasts on hd.Unit_idno equals um.UOM_Idno
                           join item in db.ItemMasts on hd.Item_Idno equals item.Item_Idno
                           where hd.RcptGoodHead_Idno == HeadId
                           select new
                           {
                               hd.Item_Idno,
                               hd.Qty,
                               hd.RcptGoodDetl_Idno,
                               hd.RcptGoodHead_Idno,
                               hd.Unit_idno,
                               hd.Weight,
                               hd.Remark,
                               um.UOM_Name,
                               item.Item_Name
                           }).ToList();
                return lst;
            }
        }

        public IList SelectReceiptGoods(int RecptNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, int receiver, Int32 yearidno, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblRcptGoodHeads
                           join cifrom in db.tblCityMasters on hd.FromCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.ToCity_Idno equals cito.City_Idno
                           join cidl in db.tblCityMasters on hd.DelvryPlc_Idno equals cidl.City_Idno
                           join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           join acntr in db.AcntMasts on hd.Recevr_Idno equals acntr.Acnt_Idno
                           //join acnta in db.AcntMasts on hd.Agent_Idno equals acnta.Acnt_Idno
                           select new
                           {
                               //hd.Agent_Idno,
                               hd.DelvryPlc_Idno,
                               hd.FromCity_Idno,
                               hd.RcptGoodHead_Date,
                               hd.RcptGoodHead_Idno,
                               hd.RcptGoodHead_No,
                               hd.Recevr_Idno,
                               hd.Sender_Idno,
                               hd.Sender_No,
                               hd.Remark,
                               hd.ToCity_Idno,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityDely = cidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               Receiver = acntr.Acnt_Name,
                               //Agent = acnta.Acnt_Name,
                               Year_Idno = hd.Year_Idno

                           }).ToList();
                if (RecptNo > 0)
                {
                    lst = lst.Where(l => l.RcptGoodHead_No == RecptNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.RcptGoodHead_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.RcptGoodHead_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.ToCity_Idno == cityto).ToList();
                }
                if (citydely > 0)
                {
                    lst = lst.Where(l => l.DelvryPlc_Idno == citydely).ToList();
                }
                if (sender > 0)
                {
                    lst = lst.Where(l => l.Sender_Idno == sender).ToList();
                }
                if (receiver > 0)
                {
                    lst = lst.Where(l => l.Recevr_Idno == receiver).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.FromCity_Idno == cityfrom).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.FromCity_Idno)).ToList();
                }
                return lst;
            }
        }

        public int DeleteReceiptGoods(Int64 HeadId, Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblRcptGoodHead rgh = db.tblRcptGoodHeads.Where(h => h.RcptGoodHead_Idno == HeadId).FirstOrDefault();
                List<tblRcptGoodDetl> rgd = db.tblRcptGoodDetls.Where(d => d.RcptGoodHead_Idno == HeadId).ToList();
                if (rgh != null)
                {
                    SqlParameter[] objSqlPara = new SqlParameter[3];
                    objSqlPara[0] = new SqlParameter("@Action", "DeleteReceiptGoodsRecvd");
                    objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                    objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                    Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                    foreach (var d in rgd)
                    {
                        db.tblRcptGoodDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblRcptGoodHeads.DeleteObject(rgh);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblCityMasters where obj.Status == true && obj.AsLocation == true orderby obj.City_Name select obj).ToList();
                return lst;
            }
        }

        public Int64 Select_ReceiptGoodsCount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = (from hd in db.tblRcptGoodHeads
                             join cifrom in db.tblCityMasters on hd.FromCity_Idno equals cifrom.City_Idno
                             join cito in db.tblCityMasters on hd.ToCity_Idno equals cito.City_Idno
                             join cidl in db.tblCityMasters on hd.DelvryPlc_Idno equals cidl.City_Idno
                             join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                             join acntr in db.AcntMasts on hd.Recevr_Idno equals acntr.Acnt_Idno
                             select hd).Count();

                return lst;
            }
        }

        public IList CheckItemExistInOtherMaster(string RcptGoodId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.TblGrHeads
                        where obj.AgnstRcpt_No == RcptGoodId
                        select new
                        {
                            GoodId = obj.AgnstRcpt_No
                        }
                        ).ToList();
            }
        }

    }
}
