using System;
using System.Linq;
using System.Collections;

namespace WebTransport.DAL
{
    public class DistanceMastDAL
    {
        public int Insert(Int64 FrmCity, Int64 ToCity, Int64 KMS, bool status, Int64 ViA, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DistanceMast Dist = db.DistanceMasts.Where(r => r.FrmCity_Idno == FrmCity && r.ViaCity_Idno == ViA && r.ToCity_Idno == ToCity).FirstOrDefault();
                    if (Dist == null)
                    {
                        DistanceMast ObjDistance = new DistanceMast();
                        ObjDistance.Emp_Idno = empIdno;
                        ObjDistance.FrmCity_Idno = FrmCity;
                        ObjDistance.ToCity_Idno = ToCity;
                        ObjDistance.KMs = KMS;
                        ObjDistance.Status = status;
                        ObjDistance.ViaCity_Idno = ViA;
                        db.DistanceMasts.AddObject(ObjDistance);
                        db.SaveChanges();
                        value = 1;
                    }
                    else
                    {
                        value = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                value = 0;
            }
            return value;
        }
        public int Update(Int64 DistIdno, Int64 FrmCity, Int64 ToCity, Int64 KMS, bool status, Int64 ViA, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DistanceMast Dist = db.DistanceMasts.Where(r => r.FrmCity_Idno == FrmCity && r.ViaCity_Idno == ViA && r.ToCity_Idno == ToCity && r.Distance_Idno != DistIdno).FirstOrDefault();
                    if (Dist == null)
                    {
                        DistanceMast ObjDistance = db.DistanceMasts.Where(d => d.Distance_Idno == DistIdno).FirstOrDefault();
                        ObjDistance.Emp_Idno = empIdno;
                        ObjDistance.FrmCity_Idno = FrmCity;
                        ObjDistance.ToCity_Idno = ToCity;
                        ObjDistance.KMs = KMS;
                        ObjDistance.ViaCity_Idno = ViA;
                        ObjDistance.Status = status;
                        db.SaveChanges();
                        value = 1;
                    }
                    else
                    {
                        value = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                value = 0;
            }
            return value;
        }
        public DistanceMast SelectById(int intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.DistanceMasts
                        where mast.Distance_Idno == intItemIdno
                        select mast).FirstOrDefault();
            }
        }
        public IList SelectForSearch(Int64 intFromCityIdno, Int64 intToCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from Dist in db.DistanceMasts
                           join F in db.tblCityMasters on Dist.FrmCity_Idno equals F.City_Idno
                           join T in db.tblCityMasters on Dist.ToCity_Idno equals T.City_Idno
                           join V in db.tblCityMasters on Dist.ViaCity_Idno equals V.City_Idno
                           //      where mast.Item_Idno == intItemTypeIdno
                           select new
                           {
                               Distance_Idno = Dist.Distance_Idno,
                               FromCityName = F.City_Name,
                               ToCity = T.City_Name,
                               KMs = Dist.KMs,
                               Status = Dist.Status,
                               FrmCityIdno = Dist.FrmCity_Idno,
                               ToCityIdno = Dist.ToCity_Idno,
                               ViaCityName = V.City_Name,
                               ViaCityIdno = V.City_Idno
                           }).ToList();

                if (intFromCityIdno > 0)
                {
                    lst = (from I in lst where I.FrmCityIdno == intFromCityIdno select I).ToList();
                }
                if (intToCityIdno > 0)
                {
                    lst = (from I in lst where I.ToCityIdno == intToCityIdno select I).ToList();
                }
                return lst;
            }
        }
        public int Delete(int intDistIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DistanceMast objDistMast = (from mast in db.DistanceMasts
                                                where mast.Distance_Idno == intDistIdno
                                                select mast).FirstOrDefault();
                    if (objDistMast != null)
                    {
                        db.DistanceMasts.DeleteObject(objDistMast);
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
        public Int32 UpdateStatus(int intDistIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DistanceMast objDistMast = (from mast in db.DistanceMasts
                                                where mast.Distance_Idno == intDistIdno
                                                select mast).FirstOrDefault();
                    if (objDistMast != null)
                    {
                        objDistMast.Emp_Idno = empIdno;
                        objDistMast.Status = Status;
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
    }
}
