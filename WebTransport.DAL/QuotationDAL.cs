using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Transactions;

namespace WebTransport.DAL
{
    public class QuotationDAL
    {


        public Int64 Insert(tblQuatationHead QtnHead, List<tblQuatationDetl> QtnDetl)
        {
            Int64 QtnHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblQuatationHead QH = db.tblQuatationHeads.Where(qh => qh.QuHead_No == QtnHead.QuHead_No && qh.Year_Idno == QtnHead.Year_Idno).FirstOrDefault();
                        if (QH != null)
                        {
                            QtnHeadId = -1;
                        }
                        else
                        {
                            db.tblQuatationHeads.AddObject(QtnHead);
                            db.SaveChanges();
                            QtnHeadId = QtnHead.QuHead_Idno;
                            if (QtnDetl.Count > 0)
                            {
                                foreach (tblQuatationDetl qtd in QtnDetl)
                                {
                                    qtd.QuHead_Idno = QtnHeadId;
                                    db.tblQuatationDetls.AddObject(qtd);
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
            return QtnHeadId;
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from hd in db.tblQuatationHeads
                         // join grd in db.tblQuatationDetls on hd.QuHead_Idno equals grd.QuDetl_Idno
                         join cifrom in db.tblCityMasters on hd.FromCity_Idno equals cifrom.City_Idno
                         join cito in db.tblCityMasters on hd.ToCity_Idno equals cito.City_Idno
                         join cidl in db.tblCityMasters on hd.DelvryPlce_Idno equals cidl.City_Idno
                         join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                         select hd.QuHead_No).Count();

                return Count;
            }

        }
        public Int64 Update(tblQuatationHead QtnHead, List<tblQuatationDetl> QtnDetl)
        {
            Int64 QtnHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblQuatationHead QH = db.tblQuatationHeads.Where(qh => qh.QuHead_No == QtnHead.QuHead_No && qh.QuHead_Idno != QtnHead.QuHead_Idno).FirstOrDefault();
                        if (QH != null)
                        {
                            QtnHeadId = -1;
                        }
                        else
                        {
                            tblQuatationHead qtn = db.tblQuatationHeads.Where(qh => qh.QuHead_Idno == QtnHead.QuHead_Idno).FirstOrDefault();
                            if (qtn != null)
                            {
                                //qtn.Chln_Idno = QtnHead.Chln_Idno;
                                qtn.Date_Modified = DateTime.Now;
                                qtn.DelvryPlce_Idno = QtnHead.DelvryPlce_Idno;
                                qtn.FromCity_Idno = QtnHead.FromCity_Idno;
                                qtn.QuHead_Date = QtnHead.QuHead_Date;
                                qtn.QuHead_No = QtnHead.QuHead_No;
                                qtn.Sender_Idno = QtnHead.Sender_Idno;
                                qtn.ToCity_Idno = QtnHead.ToCity_Idno;
                                qtn.QuHead_Typ = QtnHead.QuHead_Typ;
                                qtn.RndOff_Amnt = QtnHead.RndOff_Amnt;
                                qtn.Gross_Amnt = QtnHead.Gross_Amnt;
                                qtn.Other_Amnt = QtnHead.Other_Amnt;
                                qtn.Net_Amnt = QtnHead.Net_Amnt;
                                qtn.Year_Idno = QtnHead.Year_Idno;
                                qtn.Remark = QtnHead.Remark;
                                db.SaveChanges();
                                QtnHeadId = QtnHead.QuHead_Idno;
                                List<tblQuatationDetl> QtnDel = db.tblQuatationDetls.Where(qnd => qnd.QuHead_Idno == QtnHead.QuHead_Idno).ToList();
                                foreach (tblQuatationDetl qtd1 in QtnDel)
                                {
                                    db.tblQuatationDetls.DeleteObject(qtd1);
                                    db.SaveChanges();
                                }
                                foreach (tblQuatationDetl qtd1 in QtnDetl)
                                {
                                    qtd1.QuHead_Idno = QtnHeadId;
                                    db.tblQuatationDetls.AddObject(qtd1);
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
            return QtnHeadId;
        }
        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblCityMasters where obj.Status == true && obj.AsLocation == true orderby obj.City_Name select obj).ToList();
                return lst;
                //   return db.tblCityMasters.Select(cm => new { cm.City_Name, cm.City_Idno }).ToList();
            }
        }

        public Int64 GetMaxNo(Int32 CityID, Int32 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from hd in db.tblQuatationHeads where hd.FromCity_Idno == CityID && hd.Year_Idno == YearIdno select hd.QuHead_No).Max() + 1;
                return Convert.ToInt64(max);
            }

        }

        public tblUserPref SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref obj = ((from UP in db.tblUserPrefs select UP).FirstOrDefault());
                return obj;
            }
        }

        public List<AcntMast> FetchSenderPopulate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 && cm.INTERNAL == false orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }

        public List<AcntMast> FetchSender()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 && cm.INTERNAL == false && cm.Status == true orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }

        public double SelectWghtShrtgRate(Int32 ItemIdno, Int32 Tocity, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }


        public double SelectWghtShrtgLimit(Int32 ItemIdno, Int32 Tocity, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }

        public double SelectQtyShrtgRate(Int32 ItemIdno, Int32 Tocity, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgLimit(Int32 ItemIdno, Int32 Tocity, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate && RM.ToCity_Idno == Tocity && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }

        public double SelectItemRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select (RM.Rate_Idno)).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select (RM.Rate_Idno)).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromCity, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate && RM.FrmCity_Idno == FromCity select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }


        public List<tblRateType> FetchRate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblRateType> lst = null;
                lst = (from cm in db.tblRateTypes orderby cm.Rate_Type ascending select cm).ToList();
                return lst;
            }
        }

        public IList SelectAcntMastByType(Int64 AcntTypeId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.AcntMasts.Where(am => am.Acnt_Type == AcntTypeId).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
            }
        }

        public tblQuatationHead SelectByQuotationHeadByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblQuatationHeads.Where(h => h.QuHead_Idno == HeadId).FirstOrDefault();
            }
        }

        public IList SelectQuotationDetailByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblQuatationDetls

                           join um in db.UOMMasts on hd.Unit_Idno equals um.UOM_Idno
                           join item in db.ItemMasts on hd.Item_Idno equals item.Item_Idno
                           join RType in db.tblRateTypes on hd.Rate_Type equals RType.Rate_Idno
                           where hd.QuHead_Idno == HeadId
                           select new
                           {
                               hd.Item_Idno,
                               hd.Qty,
                               hd.QuDetl_Idno,
                               hd.QuHead_Idno,
                               hd.Unit_Idno,
                               hd.Tot_Weght,
                               Rate_Idno = hd.Rate_Type,
                               RType.Rate_Type,
                               hd.Item_Rate,
                               hd.Amount,

                               um.UOM_Name,
                               item.Item_Name
                           }).ToList();
                return lst;
            }
        }

        public IList SelectQuotation(int QuNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, Int32 yearidno, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblQuatationHeads
                           // join grd in db.tblQuatationDetls on hd.QuHead_Idno equals grd.QuDetl_Idno
                           join cifrom in db.tblCityMasters on hd.FromCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.ToCity_Idno equals cito.City_Idno
                           join cidl in db.tblCityMasters on hd.DelvryPlce_Idno equals cidl.City_Idno
                           join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           select new
                           {

                               hd.DelvryPlce_Idno,
                               hd.FromCity_Idno,
                               hd.QuHead_Date,
                               hd.QuHead_Idno,
                               hd.QuHead_No,
                               hd.Sender_Idno,
                               hd.Remark,
                               hd.Net_Amnt,
                               hd.ToCity_Idno,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityDely = cidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               Year_Idno = hd.Year_Idno

                           }).ToList();
                if (QuNo > 0)
                {
                    lst = lst.Where(l => l.QuHead_No == QuNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.QuHead_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.QuHead_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.ToCity_Idno == cityto).ToList();
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

        public int DeleteQuotation(Int64 HeadId, Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblQuatationHead qth = db.tblQuatationHeads.Where(h => h.QuHead_Idno == HeadId).FirstOrDefault();
                List<tblQuatationDetl> qtd = db.tblQuatationDetls.Where(d => d.QuHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    SqlParameter[] objSqlPara = new SqlParameter[3];
                    objSqlPara[0] = new SqlParameter("@Action", "DeleteQuotationDet");
                    objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                    objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                    Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                    foreach (var d in qtd)
                    {
                        db.tblQuatationDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblQuatationHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

    }
}
