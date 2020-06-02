using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class TitleMasterDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from TitlMast
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.TitlMasts where mast.Status==true
                        orderby mast.Titl_Name
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intTitleIdno"></param>
        /// <param name="strtitleName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strTitlName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.TitlMasts
                           select new
                           {
                               TitlIdno = mast.Titl_Idno,
                               TitlName = mast.Titl_Name,
                               TitlDesc = mast.Titl_Desc,
                               Status = mast.Status
                           }).ToList();
                if (strTitlName != "")
                {
                    lst = (from I in lst where I.TitlName.ToLower().Contains(strTitlName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from TitlMast by TitlIdno
        /// </summary>
        /// <param name="intTitleIdno"></param>
        /// <returns></returns>
        public TitlMast SelectById(int intTitlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.TitlMasts
                        where mast.Titl_Idno == intTitlIdno
                        select mast).FirstOrDefault();
            }
        }
        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 sc = (from co in db.TitlMasts  select co).Count();
                return sc;
            }

        }
        /// <summary>
        /// To check record existence in TitlMast
        /// </summary>
        /// <param name="strTitlName"></param>
        /// <param name="intTitleIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strTitlName, int intTitlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TitlMast objTitlMast = new TitlMast();
                if (intTitlIdno <= 0)
                {
                    objTitlMast = (from mast in db.TitlMasts
                                   where mast.Titl_Name == strTitlName
                                   select mast).FirstOrDefault();
                }
                else if (intTitlIdno > 0)
                {
                    objTitlMast = (from mast in db.TitlMasts
                                   where mast.Titl_Name == strTitlName
                                         && mast.Titl_Idno != intTitlIdno
                                   select mast).FirstOrDefault();
                }
                if (objTitlMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IList CheckTitleExistInOtherMaster(Int32 titleid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.AcntMasts
                        where obj.Titl_Idno == titleid
                        select new
                        {
                            tilteid = obj.Titl_Idno
                        }
                        ).ToList();
                         
            }
        }
        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in TitlMast
        /// </summary>
        /// <param name="strTitlName"></param>
        /// <param name="strTitlDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strTitleName, string strTitleCode, bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    TitlMast objTitlMast = new TitlMast(); 
                    objTitlMast.Titl_Name = strTitleName;
                    objTitlMast.Emp_Idno = empIdno;
                    objTitlMast.Titl_Desc = strTitleCode;
                    objTitlMast.Status = bStatus;
                    objTitlMast.Comp_Idno = intCompIdno;
                    objTitlMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strTitleName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.TitlMasts.AddObject(objTitlMast);
                        db.SaveChanges();
                        intValue = objTitlMast.Titl_Idno;
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
        /// Update records in TitlMast
        /// </summary>
        /// <param name="strTitlName"></param>
        /// <param name="strTitlDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intTitleIdno"></param>
        /// <returns></returns>
        public int Update(string strTitlName, string strTitlDesc, bool bStatus, int strTitlIdno, Int32 empIdno)
        {
            int intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    TitlMast objTitlMast = (from mast in db.TitlMasts
                                            where mast.Titl_Idno == strTitlIdno
                                            select mast).FirstOrDefault();
                    if (objTitlMast != null)
                    {
                        objTitlMast.Emp_Idno = empIdno;
                        objTitlMast.Titl_Name = strTitlName;
                        objTitlMast.Titl_Desc = strTitlDesc;
                        objTitlMast.Status = bStatus;
                        objTitlMast.Comp_Idno = intCompIdno;
                        objTitlMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strTitlName, strTitlIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = strTitlIdno;
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
        /// Delete record from TitlMast
        /// </summary>
        /// <param name="strTitlIdno"></param>
        /// <returns></returns>
        public int Delete(int strTitlIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    TitlMast objTitlMast = (from mast in db.TitlMasts
                                            where mast.Titl_Idno == strTitlIdno
                                            select mast).FirstOrDefault();
                    if (objTitlMast != null)
                    {
                        db.TitlMasts.DeleteObject(objTitlMast);
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

        public Int32 UpdateStatus(int strTitlIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    TitlMast objTitlMast = (from mast in db.TitlMasts
                                            where mast.Titl_Idno == strTitlIdno
                                            select mast).FirstOrDefault();
                    if (objTitlMast != null)
                    {
                        objTitlMast.Emp_Idno = empIdno;
                        objTitlMast.Status = Status;
                        objTitlMast.Date_Modified = System.DateTime.Now;
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