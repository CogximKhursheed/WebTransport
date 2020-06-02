using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class UOMMasterDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from UOMMast
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.UOMMasts
                        orderby mast.UOM_Name
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <param name="strUOMName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strUOMName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.UOMMasts
                           select new
                           {
                               UOMIdno = mast.UOM_Idno,
                               UOMName = mast.UOM_Name,
                               UOMDesc = mast.UOM_Desc,
                               Status = mast.Status
                           }).ToList();
                if (strUOMName != "")
                {
                    lst = (from I in lst where I.UOMName.ToLower().Contains(strUOMName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from UOMMast by UOMIdno
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public UOMMast SelectById(int intUOMIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.UOMMasts
                        where mast.UOM_Idno == intUOMIdno
                        select mast).FirstOrDefault();
            }
        }
        public IList GetUnit()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from item in db.UOMMasts

                           select new { UOMName = item.UOM_Name, UOMId = item.UOM_Idno }
                             ).ToList();
                return lst;
            }
        }

        /// <summary>
        /// To check record existence in UOMMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strUOMName, int intUOMIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                UOMMast objUOMMast = new UOMMast();
                if (intUOMIdno <= 0)
                {
                    objUOMMast = (from mast in db.UOMMasts
                                  where mast.UOM_Name == strUOMName
                                  select mast).FirstOrDefault();
                }
                else if (intUOMIdno > 0)
                {
                    objUOMMast = (from mast in db.UOMMasts
                                  where mast.UOM_Name == strUOMName
                                        && mast.UOM_Idno != intUOMIdno
                                  select mast).FirstOrDefault();
                }
                if (objUOMMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in UOMMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strUOMName, string strUOMNameHindi, string strUOMDesc, bool bStatus, Int32 EmpIdno)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    UOMMast objUOMMast = new UOMMast();
                    objUOMMast.UOM_Name = strUOMName;
                    objUOMMast.UOMName_Hindi = strUOMNameHindi;
                    objUOMMast.UOM_Desc = strUOMDesc;
                    objUOMMast.Status = bStatus;
                    objUOMMast.Emp_Idno = EmpIdno;
                    objUOMMast.Comp_Idno = intCompIdno;
                    objUOMMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strUOMName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.UOMMasts.AddObject(objUOMMast);
                        db.SaveChanges();
                        intValue = objUOMMast.UOM_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        /// <summary>
        /// Update records in UOMMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public int Update(string strUOMName, string strUOMNameHindi, string strUOMDesc, bool bStatus, int intUOMIdno, Int32 EmpIdno)
        {
            int intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    UOMMast objUOMMast = (from mast in db.UOMMasts
                                          where mast.UOM_Idno == intUOMIdno
                                          select mast).FirstOrDefault();
                    if (objUOMMast != null)
                    {
                        objUOMMast.UOM_Name = strUOMName;
                        objUOMMast.UOMName_Hindi = strUOMNameHindi;
                        objUOMMast.UOM_Desc = strUOMDesc;
                        objUOMMast.Status = bStatus;
                        objUOMMast.Emp_Idno = EmpIdno;
                        objUOMMast.Comp_Idno = intCompIdno;
                        objUOMMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strUOMName, intUOMIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intUOMIdno;
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

        /// <summary>
        /// Delete record from UOMMast
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public int Delete(int intUOMIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    UOMMast objUOMMast = (from mast in db.UOMMasts
                                          where mast.UOM_Idno == intUOMIdno
                                          select mast).FirstOrDefault();
                    if (objUOMMast != null)
                    {
                        db.UOMMasts.DeleteObject(objUOMMast);
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

        public Int32 UpdateStatus(int intUOMIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    UOMMast objUOMMast = (from mast in db.UOMMasts
                                          where mast.UOM_Idno == intUOMIdno
                                          select mast).FirstOrDefault();
                    if (objUOMMast != null)
                    {

                        objUOMMast.Status = Status;
                        objUOMMast.Emp_Idno = empIdno;
                        objUOMMast.Date_Modified = System.DateTime.Now;
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

        public IList CheckUOMExistInOtherMaster(Int32 UOMIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.ItemMasts
                        where obj.Unit_Idno == UOMIdno
                        select new
                        {
                            ItemGrp = obj.IGrp_Idno
                        }
                        ).ToList()
                        .Union
                        (from obj1 in db.tblItemMastPurs
                         where obj1.Unit_Idno == UOMIdno
                         select new
                         {
                             ItemGrp = obj1.IGrp_Idno
                         }
                         ).ToList()
                         .Union
                         (from obj2 in db.tblRcptGoodDetls
                          where obj2.Unit_idno == UOMIdno
                          select new
                          {
                              ItemGrp = obj2.Unit_idno
                          }
                          ).ToList()
                          .Union
                          (from obj3 in db.tblQuatationDetls
                           where obj3.Unit_Idno == UOMIdno
                           select new { ItemGrp = obj3.Unit_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.TblGrDetls
                           where obj4.Unit_Idno == UOMIdno
                           select new { ItemGrp = obj4.Unit_Idno }
                          ).ToList()
                          .Union
                          (from obj5 in db.tblPBillDetls
                           where obj5.Unit_Idno == UOMIdno
                           select new { ItemGrp = obj5.Unit_Idno }
                          ).ToList()
                         ;
            }
        }
    }
}