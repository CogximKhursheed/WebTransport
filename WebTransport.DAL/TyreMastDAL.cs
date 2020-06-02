
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebTransport.DAL
{
    public class TyreMastDAL
    {

        #region  Select....
        public IList CheckTyreExistInOtherMaster(Int32 tyreid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblItemMastPurs
                        where obj.Tyre_Type == tyreid
                        select new
                        {
                            tyrIDNO = obj.Item_Idno
                        }
                        ).ToList();
               

            }
        }
        public IList SelectForSearch(string strItemName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblTyreTypeMasters
                           select new
                           {
                               mast.TyreType_Name,
                               mast.TyreType_Idno,
                               Status = mast.TyreType_Status
                           }).ToList();
                if (strItemName != "")
                {
                    lst = (from I in lst where I.TyreType_Name.ToLower().Contains(strItemName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        public tblTyreTypeMaster SelectById(int intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblTyreTypeMasters
                        where mast.TyreType_Idno == intItemIdno
                        select mast).FirstOrDefault();
            }
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 c = 0;
                c = (from co in db.tblTyreTypeMasters select co).Count();
                return c;
            }
        }
        #endregion

        #region Insert Update Delete...

        public Int64 Insert(string strItemName, bool bStatus)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblTyreTypeMaster ObjTyreMast = new tblTyreTypeMaster();
                    ObjTyreMast.TyreType_Name = strItemName;
                    ObjTyreMast.TyreType_Status = bStatus;
                    ObjTyreMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strItemName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblTyreTypeMasters.AddObject(ObjTyreMast);
                        db.SaveChanges();
                        intValue = ObjTyreMast.TyreType_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public Int64 Update(string strItemName, bool bStatus, Int64 intTyreIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblTyreTypeMaster objTyreMast = (from mast in db.tblTyreTypeMasters
                                                     where mast.TyreType_Idno == intTyreIdno
                                                     select mast).FirstOrDefault();
                    if (objTyreMast != null)
                    {
                        objTyreMast.TyreType_Name = strItemName;
                        objTyreMast.TyreType_Status = bStatus;
                        objTyreMast.Date_modified = System.DateTime.Now;
                        if (IsExists(strItemName, intTyreIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intTyreIdno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public int Delete(int intTyreIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {

                    tblTyreTypeMaster objTyreMast = (from mast in db.tblTyreTypeMasters
                                                     where mast.TyreType_Idno == intTyreIdno
                                                     select mast).FirstOrDefault();
                    if (objTyreMast != null)
                    {
                        db.tblTyreTypeMasters.DeleteObject(objTyreMast);
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

        public bool IsExists(string strItemName, Int64 intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblTyreTypeMaster objTyreMast = new tblTyreTypeMaster();
                if (intItemIdno <= 0)
                {
                    objTyreMast = (from mast in db.tblTyreTypeMasters
                                   where mast.TyreType_Name == strItemName
                                   select mast).FirstOrDefault();
                }
                else if (intItemIdno > 0)
                {
                    objTyreMast = (from mast in db.tblTyreTypeMasters
                                   where mast.TyreType_Name == strItemName
                                         && mast.TyreType_Idno != intItemIdno
                                   select mast).FirstOrDefault();
                }
                if (objTyreMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public Int32 UpdateStatus(int intItemIdno, bool Status)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblTyreTypeMaster objItemMast = (from mast in db.tblTyreTypeMasters
                                                     where mast.TyreType_Idno == intItemIdno
                                                     select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.TyreType_Status = Status;
                        objItemMast.Date_modified = System.DateTime.Now;
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
        #endregion

    }
}
