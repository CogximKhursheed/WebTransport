using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;


namespace WebTransport.DAL
{
    public class PositionMastDAL
    {
        public Int64 Insert(string PositonName, bool Status)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int Exist = db.tblPositionMasts.Where(r => r.Position_name == PositonName).Count();
                if (Exist == 0)
                {
                    tblPositionMast ObjPosit = new tblPositionMast();
                    ObjPosit.Position_name = PositonName;
                    ObjPosit.IsActive = Status;
                    db.tblPositionMasts.AddObject(ObjPosit);
                    db.SaveChanges();
                    value = ObjPosit.Position_id;
                }
                else
                {
                    value = -1;
                }
            }
            return value;
        }

        public bool IsExists(string PositonName, Int64 PositiIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblPositionMast ObjPosit = new tblPositionMast();
                if (PositiIdno <= 0)
                {
                    ObjPosit = (from mast in db.tblPositionMasts
                                where mast.Position_name == PositonName
                                   select mast).FirstOrDefault();
                }
                else if (PositiIdno > 0)
                {
                    ObjPosit = (from mast in db.tblPositionMasts
                                where mast.Position_name == PositonName
                                         && mast.Position_id != PositiIdno
                                   select mast).FirstOrDefault();
                }
                if (ObjPosit != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public Int64 Update(Int64 PositiIdno,string PositonName, bool Status)
        {
            Int64 value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPositionMast ObjPosit = db.tblPositionMasts.Where(r => r.Position_id == PositiIdno).FirstOrDefault();
                    if (ObjPosit != null)
                    {
                            ObjPosit.Position_name = PositonName;
                            ObjPosit.IsActive = Status;
                            value = ObjPosit.Position_id;

                            if (IsExists(PositonName, PositiIdno) == true)
                            {
                                value = -1;
                            }
                            else
                            {
                                db.SaveChanges();
                                value = PositiIdno;
                            }
                    }
                  
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }

        public tblPositionMast SelectById(Int64 intPositionIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblPositionMasts where mast.Position_id == intPositionIdno select mast).FirstOrDefault();
            }
        }

        public IList SelectForSearch(string PostionName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.tblPositionMasts
                           select new
                           {
                               PositIdno = CT.Position_id,
                               PositName = CT.Position_name,
                               Status = CT.IsActive,
                           }).ToList();
                if (PostionName != "")
                {

                    lst = (from I in lst where I.PositName.ToLower().Contains(PostionName.ToLower()) select I).ToList();
                }

                return lst;
            }
        }

        public int Delete(Int64 PostionIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPositionMast objMast = (from mast in db.tblPositionMasts
                                               where mast.Position_id == PostionIdno
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

        public Int64 existOrnot(Int64 positionid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst1 = (from mast in db.LorryTyreDetls
                            where mast.Tyre_PostionIdno == positionid
                            select mast.Tyre_PostionIdno).ToList();
                var count1 = lst1.Count;
                return count1;
              
            }
        }
        public Int32 UpdateStatus(Int64 PostionIdno, bool bStatus)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPositionMast objPostionMast = (from mast in db.tblPositionMasts
                                                 where mast.Position_id == PostionIdno
                                                 select mast).FirstOrDefault();
                    if (objPostionMast != null)
                    {

                        objPostionMast.IsActive = bStatus;
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
