using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class NarrationMastDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from NarrMast
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.NarrMasts
                        orderby mast.Narr_Name
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <param name="strNarrName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strNarrName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.NarrMasts
                           select new
                           {
                               NarrIdno = mast.Narr_Idno,
                               NarrName = mast.Narr_Name,
                               NarrDesc = mast.Narr_Desc,
                               Status = mast.Status
                           }).ToList();
                if (strNarrName != "")
                {
                    lst = (from I in lst where I.NarrName.ToLower().Contains(strNarrName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }
        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 NM = (from nm in db.NarrMasts select nm ).Count();
                return NM;
            }

        }
        /// <summary>
        /// Select one record from NarrMast by NarrIdno
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public NarrMast SelectById(int intNarrIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.NarrMasts
                        where mast.Narr_Idno == intNarrIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To check record existence in NarrMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strNarrName, int intNarrIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                NarrMast objNarrMast = new NarrMast();
                if (intNarrIdno <= 0)
                {
                    objNarrMast = (from mast in db.NarrMasts
                                   where mast.Narr_Name == strNarrName
                                   select mast).FirstOrDefault();
                }
                else if (intNarrIdno > 0)
                {
                    objNarrMast = (from mast in db.NarrMasts
                                   where mast.Narr_Name == strNarrName
                                         && mast.Narr_Idno != intNarrIdno
                                   select mast).FirstOrDefault();
                }
                if (objNarrMast != null)
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
        /// Insert records in NarrMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="strNarrDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strNarrName, string strNarrDesc, bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    NarrMast objNarrMast = new NarrMast();
                    objNarrMast.Emp_Idno = empIdno;
                    objNarrMast.Narr_Name = strNarrName;
                    objNarrMast.Narr_Desc = strNarrDesc;
                    objNarrMast.Status = bStatus;
                    objNarrMast.Comp_Idno = intCompIdno;
                    objNarrMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strNarrName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.NarrMasts.AddObject(objNarrMast);
                        db.SaveChanges();
                        intValue = objNarrMast.Narr_Idno;
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
        /// Update records in NarrMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="strNarrDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public int Update(string strNarrName, string strNarrDesc, bool bStatus, int intNarrIdno, Int32 empIdno)
        {
            int intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    NarrMast objNarrMast = (from mast in db.NarrMasts
                                            where mast.Narr_Idno == intNarrIdno
                                            select mast).FirstOrDefault();
                    if (objNarrMast != null)
                    {
                        objNarrMast.Emp_Idno = empIdno;
                        objNarrMast.Narr_Name = strNarrName;
                        objNarrMast.Narr_Desc = strNarrDesc;
                        objNarrMast.Status = bStatus;
                        objNarrMast.Comp_Idno = intCompIdno;
                        objNarrMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strNarrName, intNarrIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intNarrIdno;
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
        /// Delete record from NarrMast
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public int Delete(int intNarrIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    NarrMast objNarrMast = (from mast in db.NarrMasts
                                            where mast.Narr_Idno == intNarrIdno
                                            select mast).FirstOrDefault();
                    if (objNarrMast != null)
                    {
                        db.NarrMasts.DeleteObject(objNarrMast);
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

        public Int32 UpdateStatus(int intNarrIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    NarrMast objNarrMast = (from mast in db.NarrMasts
                                            where mast.Narr_Idno == intNarrIdno
                                            select mast).FirstOrDefault();
                    if (objNarrMast != null)
                    {
                        objNarrMast.Emp_Idno = empIdno;
                        objNarrMast.Status = Status;
                        objNarrMast.Date_Modified = System.DateTime.Now;
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