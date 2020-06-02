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
using System.Transactions;

namespace WebTransport.DAL
{
    public class StockTransferDAL
    {

        public List<tblItemTypeMast> BindItemType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemTypeMast> objItemMast = new List<tblItemTypeMast>();

                objItemMast = (from obj in db.tblItemTypeMasts
                               orderby obj.ItemTpye_Idno
                               select obj).ToList();

                return objItemMast;
            }
        }

        public List<tblItemMastPur> BindItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               select obj).ToList();

                return objItemMast;
            }
        }

        public List<Stckdetl> BindItemSerial(Int64 ItemType,Int64 LocIDno,Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<Stckdetl> objItemMast = new List<Stckdetl>();

                objItemMast = (from stckdetl in db.Stckdetls
                               where (stckdetl.Item_from == "PB" || stckdetl.Item_from == "O" || stckdetl.Item_from == "MR") && (stckdetl.Billed == false)
                               && (stckdetl.Is_Issued == false || stckdetl.Is_Issued == null) && (stckdetl.Type == ItemType) && (stckdetl.Loc_Idno == LocIDno) 
                               && (stckdetl.Br_Trans == false || stckdetl.Br_Trans == null) && (stckdetl.ItemIdno == ItemIdno)
                               select stckdetl).ToList();

                return objItemMast;
            }
        }

        public List<tblItemMastPur> BindItemName(Int64 ItemType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from item in db.tblItemMastPurs
                               where item.ItemType == ItemType
                               select item).ToList();

                return objItemMast;
            }
        }

        public double BindItemRate(Int64 StckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                double? objItemMast;

                objItemMast = (from stckdetl in db.Stckdetls
                               where (stckdetl.SerlDetl_id == StckIdno )
                               select stckdetl.OpenRate).FirstOrDefault();

                return Convert.ToDouble(objItemMast);
            }
        }

        public Int64 SelectMaxNo(Int32 YearIdno, Int32 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 MaxNo = 0;
                MaxNo = Convert.ToInt32((from obj in db.tblStckTrans_Head where obj.IssLoc_Idno == FromCity && obj.Year_Idno == YearIdno select obj.StckTrans_No).Max());
                return MaxNo = MaxNo + 1;
            }
        }

        public Int64 Insert(tblStckTrans_Head StckHead, List<tblStckTrans_Detl> StransDetl)
        {
            Int64? HeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblStckTrans_Head TH = db.tblStckTrans_Head.Where(SH => SH.StckTrans_No == StckHead.StckTrans_No).FirstOrDefault();
                        if (TH != null)
                        {
                            HeadId = -1;
                        }
                        else
                        {
                            db.tblStckTrans_Head.AddObject(StckHead);
                            db.SaveChanges();
                            HeadId = StckHead.StckTrans_Idno;
                            if (StransDetl.Count > 0)
                            {
                                foreach (tblStckTrans_Detl std in StransDetl)
                                {
                                    std.StckTrans_Idno = Convert.ToInt32(HeadId);
                                    db.tblStckTrans_Detl.AddObject(std);
                                    db.SaveChanges();

                                    if (std.ItemType_Idno == 1)
                                    {
                                        Stckdetl objStckDetl = (from obj1 in db.Stckdetls where obj1.SerlDetl_id == std.SerialNo_Idno select obj1).FirstOrDefault();
                                        objStckDetl.Br_Trans = true;
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
            return Convert.ToInt64(HeadId);
        }

        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from hd in db.tblStckTrans_Head
                         join grd in db.tblStckTrans_Detl on hd.StckTrans_Idno equals grd.StckTrans_Idno
                         join cifrom in db.tblCityMasters on hd.IssLoc_Idno equals cifrom.City_Idno
                         join cito in db.tblCityMasters on hd.RecLoc_Idno equals cito.City_Idno
                         join Itype in db.tblItemTypeMasts on grd.ItemType_Idno equals Itype.ItemTpye_Idno
                           select hd.StckTrans_Idno).Count();

                return Count;
            }
            
        }
        public Int64 Update(tblStckTrans_Head StckHead, List<tblStckTrans_Detl> StransDetl)
        {
            Int64 HeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblStckTrans_Head TH = db.tblStckTrans_Head.Where(SH => SH.StckTrans_No == StckHead.StckTrans_No && SH.StckTrans_Idno != StckHead.StckTrans_Idno).FirstOrDefault();
                        if (TH != null)
                        {
                            HeadId = -1;
                        }
                        else
                        {
                            tblStckTrans_Head Stk = db.tblStckTrans_Head.Where(sd => sd.StckTrans_Idno == StckHead.StckTrans_Idno).FirstOrDefault();
                            if (Stk != null)
                            {
                                //qtn.Chln_Idno = QtnHead.Chln_Idno;
                                Stk.Date_Modified = DateTime.Now;
                                Stk.StckTrans_Date = StckHead.StckTrans_Date;
                                Stk.IssLoc_Idno = StckHead.IssLoc_Idno;
                                Stk.RecLoc_Idno = StckHead.RecLoc_Idno;
                                Stk.User_Idno = StckHead.User_Idno;
                                Stk.Net_Amnt = StckHead.Net_Amnt;
                                Stk.Year_Idno = StckHead.Year_Idno;
                                Stk.Remark = StckHead.Remark;
                                db.SaveChanges();
                                HeadId = StckHead.StckTrans_Idno;
                                List<tblStckTrans_Detl> StkDel = db.tblStckTrans_Detl.Where(qnd => qnd.StckTrans_Idno == StckHead.StckTrans_Idno).ToList();
                                foreach (tblStckTrans_Detl qtd1 in StkDel)
                                {
                                    db.tblStckTrans_Detl.DeleteObject(qtd1);
                                    db.SaveChanges();
                                    if (qtd1.ItemType_Idno == 1)
                                    {
                                        Stckdetl objStckDetl = (from obj1 in db.Stckdetls where obj1.SerlDetl_id == qtd1.SerialNo_Idno select obj1).FirstOrDefault();
                                        objStckDetl.Br_Trans = false;
                                        db.SaveChanges();
                                    }
                                }
                                foreach (tblStckTrans_Detl std1 in StransDetl)
                                {
                                    std1.StckTrans_Idno = Convert.ToInt32(HeadId);
                                    db.tblStckTrans_Detl.AddObject(std1);
                                    db.SaveChanges();
                                    if (std1.ItemType_Idno == 1)
                                    {
                                        Stckdetl objStckDetl = (from obj1 in db.Stckdetls where obj1.SerlDetl_id == std1.SerialNo_Idno select obj1).FirstOrDefault();
                                        objStckDetl.Br_Trans = true;
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
            return HeadId;
        }
        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblCityMasters where obj.Status==true && obj.AsLocation==true orderby obj.City_Name select obj).ToList();
                return lst;
                //   return db.tblCityMasters.Select(cm => new { cm.City_Name, cm.City_Idno }).ToList();
            }
        }

        public tblUserPref SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref obj=((from UP in db.tblUserPrefs select UP).FirstOrDefault());
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

        public Int64 SelectItemType(Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 ItemType = 0;
                ItemType = Convert.ToInt32((from obj in db.tblItemMastPurs where obj.Item_Idno == ItemIdno select obj.ItemType).FirstOrDefault());
                return ItemType;
            }
        }

        public IList SelectAcntMastByType(Int64 AcntTypeId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.AcntMasts.Where(am => am.Acnt_Type == AcntTypeId).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
            }
        }

        public tblStckTrans_Head SelectByStckTransHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblStckTrans_Head.Where(h => h.StckTrans_Idno == HeadId).FirstOrDefault();
            }
        }

        public IList SelectStkTransDetailByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblStckTrans_Detl
                           join IType in db.tblItemTypeMasts on hd.ItemType_Idno equals IType.ItemTpye_Idno
                           join IT in db.tblItemMastPurs on hd.Item_Idno equals IT.Item_Idno
                           //join SD in db.Stckdetls on hd.SerialNo_Idno equals SD.SerlDetl_id
                           where hd.StckTrans_Idno == HeadId
                           select new
                           {
                               hd.ItemType_Idno,
                               hd.Qty,
                               hd.StckTransDetl_Idno,
                               hd.StckTrans_Idno,
                               hd.Rate,
                               hd.SerialNo_Idno,
                               IT.Item_Name,
                               ITypeName = IType.ItemType_Name,
                               Serial_No=hd.Item_Serial_No,
                               hd.TyreType_Idno,
                               hd.Item_Idno,
                               TyreType = (hd.TyreType_Idno == 1 ? "New" : hd.TyreType_Idno == 2? "Old":hd.TyreType_Idno == 3?"Retreated":"")
                           }).ToList();
                return lst;
            }
        }
        public List<Stckdetl> BindSerialNo(Int64 ItemType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<Stckdetl> objItemMast = new List<Stckdetl>();

                objItemMast = (from stckdetl in db.Stckdetls
                               where (stckdetl.Item_from == "PB" || stckdetl.Item_from == "O" || stckdetl.Item_from == "MR") && (stckdetl.Billed == false)
                               && (stckdetl.Is_Issued == false || stckdetl.Is_Issued == null) && (stckdetl.Type == ItemType)
                               select stckdetl).ToList();

                return objItemMast;
            }
        }



        public IList SelectStckTransfer(int IssNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int cityto, Int32 yearidno, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblStckTrans_Head
                           join grd in db.tblStckTrans_Detl on hd.StckTrans_Idno equals grd.StckTrans_Idno
                           join cifrom in db.tblCityMasters on hd.IssLoc_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.RecLoc_Idno equals cito.City_Idno
                           join Itype in db.tblItemTypeMasts on grd.ItemType_Idno equals Itype.ItemTpye_Idno
                           select new
                           {

                               hd.StckTrans_Date,
                               hd.IssLoc_Idno,
                               hd.RecLoc_Idno,
                               hd.User_Idno,
                               SerialNo=grd.Item_Serial_No,
                               CityFrom=cifrom.City_Name,
                               CityTo = cito.City_Name,
                               hd.StckTrans_No,
                               hd.StckTrans_Idno,
                               grd.Rate,
                               ItemType=Itype.ItemType_Name,
                               hd.Remark,
                               hd.Net_Amnt,
                               grd.ItemType_Idno,
                               Qty=(from Sd1 in db.tblStckTrans_Detl join sh1 in db.tblStckTrans_Head on Sd1.StckTrans_Idno equals sh1.StckTrans_Idno where Sd1.StckTrans_Idno == grd.StckTrans_Idno select Sd1.Qty).Sum(),
                               Year_Idno = hd.Year_Idno

                           }).GroupBy(x => x.StckTrans_No).Select(x => x.FirstOrDefault()).ToList();
                if (IssNo > 0)
                {
                    lst = lst.Where(l => l.StckTrans_No == IssNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.StckTrans_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.StckTrans_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.RecLoc_Idno == cityto).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.IssLoc_Idno == cityfrom).ToList();
                }
                //if (ItemTypeID > 0)
                //{
                //    lst = lst.Where(l => l.ItemType_ID == ItemTypeID).ToList();
                //}
                if (UserIdno > 0)
                {
                    lst = lst.Where(l => l.User_Idno == UserIdno).ToList();
                }
                return lst;
            }
        }

        public int DeleteStcktransfer(Int64 HeadId,Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblStckTrans_Head qth = db.tblStckTrans_Head.Where(h => h.StckTrans_Idno == HeadId).FirstOrDefault();
                List<tblStckTrans_Detl> qtd = db.tblStckTrans_Detl.Where(d => d.StckTrans_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblStckTrans_Detl.DeleteObject(d);
                        db.SaveChanges();
                        if (d.ItemType_Idno == 1)
                        {
                            Stckdetl objStckDetl = (from obj1 in db.Stckdetls where obj1.SerlDetl_id == d.SerialNo_Idno select obj1).FirstOrDefault();
                            objStckDetl.Br_Trans = false;
                            db.SaveChanges();
                        }
                    }
                    db.tblStckTrans_Head.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public IList SelectStckTransferReport(DateTime? dtfrom, DateTime? dtto, int cityfrom, int cityto, int ItemIdno, int SerialIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblStckTrans_Head
                           join grd in db.tblStckTrans_Detl on hd.StckTrans_Idno equals grd.StckTrans_Idno
                           join cifrom in db.tblCityMasters on hd.IssLoc_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.RecLoc_Idno equals cito.City_Idno
                           join IName in db.tblItemMastPurs on grd.Item_Idno equals IName.Item_Idno
                           join Itype in db.tblItemTypeMasts on grd.ItemType_Idno equals Itype.ItemTpye_Idno
                           select new
                           {
                               hd.StckTrans_No,
                               hd.StckTrans_Date,
                               hd.IssLoc_Idno,
                               hd.RecLoc_Idno,
                               IssLoc=cifrom.City_Name,
                               RecLoc=cito.City_Name,
                               grd.Item_Idno,
                               ItemName=IName.Item_Name,
                               ItemType=Itype.ItemType_Name,
                               SerialNo = grd.Item_Serial_No,
                               grd.SerialNo_Idno,
                               grd.Qty,
                               hd.StckTrans_Idno,
                               grd.Rate,
                               hd.Remark,
                               hd.Net_Amnt,
                               grd.Tot_Amnt,
                               grd.ItemType_Idno,
                               Year_Idno = hd.Year_Idno

                           }).ToList();

                //if (dtto != null)
                //{
                //    lst = lst.Where(l => Convert.ToDateTime(l.StckTrans_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                //}
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.StckTrans_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.RecLoc_Idno == cityto).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.IssLoc_Idno == cityfrom).ToList();
                }
                if (ItemIdno > 0)
                {
                    lst = lst.Where(l => l.Item_Idno == ItemIdno).ToList();
                }
                if (SerialIdno > 0)
                {
                    lst = lst.Where(l => l.SerialNo_Idno == SerialIdno).ToList();
                }
                return lst;
            }
        }
    }
}
