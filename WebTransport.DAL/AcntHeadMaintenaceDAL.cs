using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using WebTransport.Classes;

namespace WebTransport.DAL
{
    public class AcntHeadMaintenaceDAL
    {
        #region Select Events...
        /// <summary>
        /// Select all records from AcntHeadGroup
        /// </summary>
        /// <returns></returns>

        public List<AcntHeadGroup> SelectAHGroup()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntHeadGroup> lst = null;
                lst = (from AHG in db.AcntHeadGroups orderby AHG.AcntGH_Name ascending select AHG).ToList();
                return lst;
            }
        }

        /// <summary>
        /// Select all records from AcntHead
        /// </summary>
        /// <param name="intMainHeadIdno"></param>
        /// <param name="strAHeadName"></param>
        /// <returns></returns>

        public IList SelectForSearch(string strAHeadName, Int32 intMainHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AH in db.AcntHeads
                           join sm in db.AcntHeadGroups on AH.MainHead_Idno equals sm.MainHead_Idno
                           select new
                           {
                               AHeadIdno = AH.AHead_Idno,
                               AHeadName = AH.AHead_Name,
                               Status = AH.Status,
                               MainHeadIdno = AH.MainHead_Idno,
                               AcntGHName = sm.AcntGH_Name
                           }).ToList();
                if (intMainHeadIdno >= 0)
                {
                    lst = (from l in lst where l.MainHeadIdno == intMainHeadIdno select l).ToList();
                }
                if (strAHeadName != "")
                {
                    lst = (from l in lst where l.AHeadName.ToLower().Contains(strAHeadName.ToLower()) select l).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from AcntHead by AHeadIdno
        /// </summary>
        /// <param name="intAHeadIdno"></param>
        /// <returns></returns>
        public AcntHead SelectById(int intAHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.AcntHeads where mast.AHead_Idno == intAHeadIdno select mast).FirstOrDefault();
            }
        }
        #endregion

        #region InsertUpdateDelete Events...
        /// <summary>
        /// Insert records in AcntHead
        /// </summary>
        /// <param name="strAHeadName"></param>
        /// <param name="intMainHeadIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertAcntHead(string strAHeadName, Int64 intMainHeadIdno, bool bStatus, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                AcntHead ObjAcntHeadMast = new AcntHead();
                if (IsExists(strAHeadName, 0))
                {
                    value = -1;
                }
                else
                {
                    ObjAcntHeadMast.AHead_Name = strAHeadName;
                    ObjAcntHeadMast.MainHead_Idno = intMainHeadIdno;
                    ObjAcntHeadMast.Status = bStatus;
                    ObjAcntHeadMast.Emp_Idno = empIdno;
                    ObjAcntHeadMast.Comp_Idno = intCompIdno;
                    ObjAcntHeadMast.Date_Added = System.DateTime.Now;
                    db.AddToAcntHeads(ObjAcntHeadMast);
                    db.SaveChanges();
                    value = ObjAcntHeadMast.AHead_Idno;
                }
                return value;
            }
        }

        /// <summary>
        /// Update records in AcntHead
        /// </summary>
        /// <param name="strAHeadName"></param>
        /// <param name="bStatus"></param>
        /// <param name="intAHeadIdno"></param>
        /// <param name="intMainHeadIdno"></param>
        /// <returns></returns>

        public Int64 UpdateAcntHead(string strAHeadName, Int64 intMainHeadIdno, Int64 intAHeadIdno, bool bStatus, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntHead objAcntHead = (from mast in db.AcntHeads
                                            where mast.AHead_Idno == intAHeadIdno
                                            select mast).FirstOrDefault();
                if (objAcntHead != null)
                {
                    objAcntHead.AHead_Name = strAHeadName;
                    objAcntHead.MainHead_Idno = intMainHeadIdno;
                    objAcntHead.Status = bStatus;
                    objAcntHead.Emp_Idno = empIdno;
                    objAcntHead.Comp_Idno = intCompIdno;
                    objAcntHead.Date_Modified = System.DateTime.Now;
                    if (IsExists(strAHeadName, intAHeadIdno) == true)
                    {
                        value = -1;
                    }
                    else
                    {
                        db.SaveChanges();
                        value = intAHeadIdno;
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// To check record existence in AcntHead
        /// </summary>
        /// <param name="strAHeadName"></param>
        /// <param name="intMainHeadIdno"></param>
        /// <param name="intAHeadIdno"></param>
        /// <returns></returns>

        public bool IsExists(string strAHeadName, Int64 intAHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntHead objAcntHeadMast = null;

                if (intAHeadIdno > 0)///for update
                {
                    objAcntHeadMast = (from AH in db.AcntHeads
                                   where AH.AHead_Name == strAHeadName //&& AH.MainHead_Idno == intMainHeadIdno
                                  && AH.AHead_Idno != intAHeadIdno
                                   select AH).FirstOrDefault();
                }
                else /// for insert
                {
                    objAcntHeadMast = (from AH in db.AcntHeads
                                       where AH.AHead_Name == strAHeadName //&& AH.MainHead_Idno == intMainHeadIdno
                                       select AH).FirstOrDefault();

                }
                if (objAcntHeadMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// Delete record from AcntHead
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intAHeadIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntHead objMast = (from mast in db.AcntHeads
                                          where mast.AHead_Idno == intAHeadIdno
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

        public Int32 UpdateStatus(int intAHeadIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntHead objAcntHeadMast = (from mast in db.AcntHeads
                                              where mast.AHead_Idno == intAHeadIdno
                                              select mast).FirstOrDefault();
                    if (objAcntHeadMast != null)
                    {

                        objAcntHeadMast.Status = bStatus;
                        objAcntHeadMast.Emp_Idno = empIdno;
                        objAcntHeadMast.Date_Modified = System.DateTime.Now;
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

        public IList CheckItemExistInOtherMaster(Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.AcntSubHeads
                        where obj.AHead_Idno == ItemIdno
                        select new
                        {
                            ItemGrp = obj.AHead_Idno
                        }
                        ).ToList();
                         
                         
            }
        }
        #endregion
    }
}