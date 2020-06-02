using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class CategoryMasterDAL
    {
        #region Select Events...


        /// <summary>
        /// Fill Grid
        /// </summary>
        /// <returns></returns>
        public IList SelectAll(string strCategoryName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblCategoryMasts
                           orderby mast.Category_Name ascending
                           select new
                           {
                               // Item_Name = loc.Item_Name,
                               Cat_Name = mast.Category_Name,
                               Cat_Idno = mast.Category_Idno,
                               Status = mast.Status
                           }).ToList();
                if (strCategoryName != "")
                {
                    lst = (from I in lst where I.Cat_Name.ToLower().Contains(strCategoryName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select From Id
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public tblCategoryMast SelectById(int intCategoryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblCategoryMasts
                        where mast.Category_Idno == intCategoryIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To Check data Already Exist or Not
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public bool IsExists(string categoryName, int intCategoryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCategoryMast objCategoryMast = new tblCategoryMast();
                if (intCategoryIdno <= 0)
                {
                    objCategoryMast = (from mast in db.tblCategoryMasts
                                       where mast.Category_Name == categoryName
                                   select mast).FirstOrDefault();
                }
                else if (intCategoryIdno > 0)
                {
                    objCategoryMast = (from mast in db.tblCategoryMasts
                                       where mast.Category_Name == categoryName
                                         && mast.Category_Idno != intCategoryIdno
                                   select mast).FirstOrDefault();
                }
                if (objCategoryMast != null)
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
        /// Insert records in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string CategoryName,bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCategoryMast objCategoryMast = new tblCategoryMast();
                    objCategoryMast.Emp_Idno = empIdno;
                    objCategoryMast.Category_Name = CategoryName;
                    objCategoryMast.Status = bStatus;
                    objCategoryMast.Date_Added = System.DateTime.Now;
                    if (IsExists(CategoryName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblCategoryMasts.AddObject(objCategoryMast);
                        db.SaveChanges();
                        intValue = objCategoryMast.Category_Idno;
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
        /// To Update Record in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Update(string CategoryName, bool bStatus, int intCategoryIdno, Int32 empIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCategoryMast objCategoryMast = (from mast in db.tblCategoryMasts
                                                      where mast.Category_Idno == intCategoryIdno
                                                       select mast).FirstOrDefault();
                    if (objCategoryMast != null)
                    {
                        objCategoryMast.Emp_Idno = empIdno;
                        objCategoryMast.Category_Name = CategoryName;
                        objCategoryMast.Status = bStatus;
                        objCategoryMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(CategoryName, intCategoryIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intCategoryIdno;
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
        ///  Delete Record from tblIGrpMast
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Delete(int intCategoryIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCategoryMast objCategoryMast = (from mast in db.tblCategoryMasts
                                                       where mast.Category_Idno == intCategoryIdno
                                               select mast).FirstOrDefault();
                    if (objCategoryMast != null)
                    {
                        db.tblCategoryMasts.DeleteObject(objCategoryMast);
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

        public Int32 UpdateStatus(int intCategoryIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCategoryMast objCategoryMast = (from mast in db.tblCategoryMasts
                                                       where mast.Category_Idno == intCategoryIdno
                                               select mast).FirstOrDefault();
                    if (objCategoryMast != null)
                    {
                        objCategoryMast.Emp_Idno = empIdno;
                        objCategoryMast.Status = Status;
                        objCategoryMast.Date_Modified = System.DateTime.Now;
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
        
        public IList CheckCategoryExistInLedgerMaster(Int32 categoryId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.AcntMasts
                        where obj.Category_Idno == categoryId
                        select new
                        {
                           obj.Category_Idno
                        }
                        ).ToList()
                        ;
            }
        }
        public Int64 SelectTotal()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblCategoryMasts
                           orderby mast.Category_Name ascending
                           select new
                           {
                               mast
                           }).Count();
                return lst;
            }
        }
    }
}