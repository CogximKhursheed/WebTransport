using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using VisageWeb.Utility;
using System.Transactions;
//using VisageWeb.Utility;

namespace WebTransport.DAL
{
    public class VechDetlClmPrtyDAL
    {
        public Stckdetl SelectById(Int64 intStckdetlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Stckdetl lst = (from tbl in db.Stckdetls where tbl.SerlDetl_id == intStckdetlIdno select tbl).FirstOrDefault();
                return lst;
            }
        }

        public AcntMast SelectAcntName(Int64 intAcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast lst = (from tbl in db.AcntMasts where tbl.Acnt_Idno == intAcntIdno select tbl).FirstOrDefault();
                return lst;
            }
        }

        public IList SelectAllDetails(Int64 intAcntIdno,Int64 intLocIdno,int intYearIdno )
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stck in db.Stckdetls
                           join led in db.AcntMasts on stck.Acnt_Idno equals led.Acnt_Idno
                           join cat in db.tblTyreCategories on stck.Type equals cat.TyreType_IdNo
                           join IP in db.tblItemMastPurs on stck.ItemIdno equals IP.Item_Idno
                           where stck.Acnt_Idno == intAcntIdno && stck.Item_from == "CP" && stck.yearId == intYearIdno
                           select new
                           {
                               stck.SerlDetl_id,
                               stck.SerialNo,
                               stck.Loc_Idno,
                               stck.ItemIdno,
                               IP.Item_Name,
                               stck.Item_from,
                               stck.CompName,
                               cat.TyreType_IdNo,
                               cat.TyreType_Name,
                               stck.PurFrom,
                               stck.OpenRate,
                               stck.yearId,
                               stck.Acnt_Idno,
                               led.Acnt_Name,
                           }).ToList();
                if (intLocIdno > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == intLocIdno).ToList();
                }
                return lst;
            }
        }

        public Int64 Insert(string strSerialNo,Int64 intLocIdno,Int64 intItemIdNo,string strCompName,Int32 intTyreType,string strPurFrom,Int32 intYearIdno,double dblRate,Int64 intAcntIdno)
        {
            Int64 StckDetlIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (IsExists(strSerialNo) == false)
                    {
                        
                        Stckdetl obj = new Stckdetl();
                        obj.SerialNo = strSerialNo;
                        obj.Loc_Idno = intLocIdno;
                        obj.ItemIdno = intItemIdNo;
                        obj.CompName = strCompName;
                        obj.Type = intTyreType;
                        obj.PurFrom = strPurFrom;
                        obj.yearId = intYearIdno;
                        obj.OpenRate = dblRate;
                        obj.Acnt_Idno = intAcntIdno;
                        obj.PBillIdno = 0;
                        obj.Item_from = "CP";
                        obj.SaleBill_Idno = 0;
                        obj.lorry_Idno = 0;
                        obj.Tyre_PostionIdno = 0;
                        obj.MtrlIssue_Idno = 0;
                        obj.OpenAmnt = 0;
                        obj.Is_Issued = false;
                        obj.Claim_Idno = 0;
                        obj.Billed = true;
                        db.AddToStckdetls(obj);
                        db.SaveChanges();
                        StckDetlIdno= obj.SerlDetl_id;
                    }
                    else
                    {
                        StckDetlIdno = -1;
                    }
                }
            }
            catch
            {
                StckDetlIdno=0;
            }
            return StckDetlIdno;
        }

        public Int64 Update(Int64 intStckDetlIdno, string strSerialNo, Int64 intLocIdno, Int64 intItemIdNo, string strCompName, Int32 intTyreType, string strPurFrom, Int32 intYearIdno, double dblRate, Int64 intAcntIdno)
        {
            Int64 Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (IsExists(strSerialNo) == false)
                    {
                        Stckdetl stckdetlValue = (from obj in db.Stckdetls
                                             where obj.SerlDetl_id == intStckDetlIdno
                                             select obj).SingleOrDefault();
                        if (stckdetlValue != null)
                        {
                            stckdetlValue.SerialNo = strSerialNo;
                            stckdetlValue.Loc_Idno = intLocIdno;
                            stckdetlValue.ItemIdno = intItemIdNo;
                            stckdetlValue.CompName = strCompName;
                            stckdetlValue.Type = intTyreType;
                            stckdetlValue.PurFrom = strPurFrom;
                            stckdetlValue.yearId = intYearIdno;
                            stckdetlValue.OpenRate = dblRate;
                            stckdetlValue.Acnt_Idno = intAcntIdno;
                            db.SaveChanges();
                            Value = stckdetlValue.SerlDetl_id;
                        }
                    }
                    else
                    {
                        Value = -1;
                    }
                }
            }
            catch
            {
            }
            return Value;
        }

        public bool IsExists(string strSerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.Stckdetls
                           where obj.SerialNo.ToLower() == strSerialNo
                           select obj).ToList();
                if (lst.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
        public int Delete(Int64 intstckDetlIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    Stckdetl objMast = (from mast in db.Stckdetls
                                        where mast.SerlDetl_id == intstckDetlIdno
                                             select mast).FirstOrDefault();
                    if (objMast != null)
                    {
                        db.DeleteObject(objMast);
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


    }

}
