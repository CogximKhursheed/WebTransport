using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class AcntSubGrpMasterDAL
    {
        #region Select Events...
        /// <summary>
        /// Select all records from AcntHead
        /// </summary>
        /// <returns></returns>

        public List<AcntHead> SelectAHGroupActiveOnly()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntHead> lst = null;
                lst = (from AH in db.AcntHeads where AH.Status==true orderby AH.AHead_Name ascending select AH).ToList();
                return lst;
            }
        }


        public List<AcntHead> SelectAHGroupAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntHead> lst = null;
                lst = (from AH in db.AcntHeads orderby AH.AHead_Name ascending select AH).ToList();
                return lst;
            }
        }

        /// <summary>
        /// Select all records from AcntSubHead
        /// </summary>
        /// <param name="intAHeadIdno"></param>
        /// <param name="strASubHeadName"></param>
        /// <returns></returns>

        public IList SelectForSearch(string strASubHeadName, Int32 intAHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AH in db.AcntSubHeads
                           join sm in db.AcntHeads on AH.AHead_Idno equals sm.AHead_Idno
                           select new
                           {
                               ASubHeadIdno = AH.ASubHead_Idno,
                               ASubHeadName = AH.ASubHead_Name,
                               Status = AH.Status,
                               AHeadIdno = AH.AHead_Idno,
                               AHeadName = sm.AHead_Name
                           }).ToList();
                if (intAHeadIdno > 0)
                {
                    lst = (from l in lst where l.AHeadIdno == intAHeadIdno select l).ToList();
                }
                if (strASubHeadName != "")
                {
                    lst = (from l in lst where l.ASubHeadName.ToLower().Contains(strASubHeadName.ToLower()) select l).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from AcntSubHead by AHeadIdno
        /// </summary>
        /// <param name="intASubHeadIdno"></param>
        /// <returns></returns>
        public AcntSubHead SelectById(int intASubHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.AcntSubHeads where mast.ASubHead_Idno == intASubHeadIdno select mast).FirstOrDefault();
            }
        }
        #endregion

        #region InsertUpdateDelete Events...
        /// <summary>
        /// Insert records in AcntSubHead
        /// </summary>
        /// <param name="strASubHeadName"></param>
        /// <param name="intAHeadIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertAcntHead(string strASubHeadName, Int64 intAHeadIdno, bool bStatus, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                AcntSubHead ObjAcntSubHead = new AcntSubHead();
                if (IsExists(strASubHeadName, 0))
                {
                    value = -1;
                }
                else
                {
                    ObjAcntSubHead.ASubHead_Name = strASubHeadName;
                    ObjAcntSubHead.AHead_Idno = intAHeadIdno;
                    ObjAcntSubHead.Emp_Idno = empIdno;
                    ObjAcntSubHead.Status = bStatus;
                    ObjAcntSubHead.Comp_Idno = intCompIdno;
                    ObjAcntSubHead.Date_Added = System.DateTime.Now;
                    db.AddToAcntSubHeads(ObjAcntSubHead);
                    db.SaveChanges();
                    value = ObjAcntSubHead.ASubHead_Idno;
                }
                return value;
            }
        }

        /// <summary>
        /// Update records in AcntSubHead
        /// </summary>
        /// <param name="strASubHeadName"></param>
        /// <param name="bStatus"></param>
        /// <param name="intAHeadIdno"></param>
        /// <param name="intAHeadIdno"></param>
        /// <returns></returns>

        public Int64 UpdateAcntHead(string strASubHeadName, Int64 intAHeadIdno, Int64 intASubHeadIdno, bool bStatus, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntSubHead objAcntSubHead = (from mast in db.AcntSubHeads
                                              where mast.ASubHead_Idno == intASubHeadIdno
                                        select mast).FirstOrDefault();
                if (objAcntSubHead != null)
                {
                    objAcntSubHead.ASubHead_Name = strASubHeadName;
                    objAcntSubHead.AHead_Idno = intAHeadIdno;
                    objAcntSubHead.Emp_Idno = empIdno;
                    objAcntSubHead.Status = bStatus;
                    objAcntSubHead.Comp_Idno = intCompIdno;
                    objAcntSubHead.Date_Modified = System.DateTime.Now;
                    if (IsExists(strASubHeadName, intASubHeadIdno) == true)
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
        /// To check record existence in AcntSubHead
        /// </summary>
        /// <param name="strASubHeadName"></param>
        /// <param name="intAHeadIdno"></param>
        /// <param name="intAHeadIdno"></param>
        /// <returns></returns>

        public bool IsExists(string strASubHeadName, Int64 intASubHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntSubHead objAcntSubHead = null;

                if (intASubHeadIdno > 0)///for update
                {
                    objAcntSubHead = (from AH in db.AcntSubHeads
                                       where AH.ASubHead_Name == strASubHeadName
                                      && AH.ASubHead_Idno != intASubHeadIdno
                                       select AH).FirstOrDefault();
                }
                else /// for insert
                {
                    objAcntSubHead = (from AH in db.AcntSubHeads
                                       where AH.ASubHead_Name == strASubHeadName
                                       select AH).FirstOrDefault();

                }
                if (objAcntSubHead != null)
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
        /// Delete record from AcntSubHead
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intASubHeadIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntSubHead objMast = (from mast in db.AcntSubHeads
                                           where mast.ASubHead_Idno == intASubHeadIdno
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

        public Int32 UpdateStatus(int intASubHeadIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntSubHead objAcntSubHead = (from mast in db.AcntSubHeads
                                                  where mast.ASubHead_Idno == intASubHeadIdno
                                                select mast).FirstOrDefault();
                    if (objAcntSubHead != null)
                    { 
                        objAcntSubHead.Status = bStatus;
                        objAcntSubHead.Emp_Idno = empIdno;
                        objAcntSubHead.Date_Modified = System.DateTime.Now;
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