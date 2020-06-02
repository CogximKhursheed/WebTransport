using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebTransport.DAL
{
    public class GradeMasterDAL
    {
        #region Select Events...

        public IList SelectAll(string strName,string strAbbr)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblGradeMasters
                           orderby mast.Grade_Name ascending
                           select new
                           {
                               Grd_Name = mast.Grade_Name,
                               Grd_Abbr = mast.Grade_Abbr,
                               Grd_Idno = mast.Grade_Idno,
                               Status = mast.Status
                           }).ToList();
                if (strName != "")
                {
                    lst = (from I in lst where I.Grd_Name.ToLower().Contains(strName.ToLower()) select I).ToList();
                }
                if (strAbbr != "")
                {
                    lst = (from I in lst where I.Grd_Abbr.ToLower().Contains(strAbbr.ToLower()) select I).ToList();
                }
                return lst;
            }
        }
        public tblGradeMaster SelectById(int intIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblGradeMasters
                        where mast.Grade_Idno == intIdno
                        select mast).FirstOrDefault();
            }
        }
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
        
        public Int64 Insert(string GradeName,string Abbr,bool bStatus)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblGradeMaster obj = new tblGradeMaster();
                    obj.Grade_Name = GradeName;
                    obj.Grade_Abbr = Abbr;
                    obj.Status = bStatus;
                    obj.Date_Added = System.DateTime.Now;
                    if (IsExists(GradeName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblGradeMasters.AddObject(obj);
                        db.SaveChanges();
                        intValue = obj.Grade_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return intValue;
        }

        public int Update(string GradeName,string Abrr, bool bStatus, int intGrdIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblGradeMaster obj1 = (from mast in db.tblGradeMasters
                                                      where mast.Grade_Idno == intGrdIdno
                                                       select mast).FirstOrDefault();
                    if (obj1 != null)
                    {

                        obj1.Grade_Name = GradeName;
                        obj1.Grade_Abbr = Abrr;
                        obj1.Status = bStatus;
                        obj1.Date_Modified = System.DateTime.Now;
                        if (IsExists(GradeName, intGrdIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intGrdIdno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return intValue;
        }

        public int Delete(int intGrdIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblGradeMaster obj2 = (from mast in db.tblGradeMasters
                                           where mast.Grade_Idno == intGrdIdno
                                           select mast).FirstOrDefault();
                    if (obj2 != null)
                    {
                        db.tblGradeMasters.DeleteObject(obj2);
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

        public Int32 UpdateStatus(int intGrdIdno, bool Status)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblGradeMaster obj3 = (from mast in db.tblGradeMasters
                                           where mast.Grade_Idno == intGrdIdno
                                           select mast).FirstOrDefault();
                    if (obj3 != null)
                    {
                        obj3.Status = Status;
                        obj3.Date_Modified = System.DateTime.Now;
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
        
        public Int64 SelectTotal()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblGradeMasters
                           orderby mast.Grade_Name ascending
                           select new
                           {
                               mast
                           }).Count();
                return lst;
            }
        }
    }
}