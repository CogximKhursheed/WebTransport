using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
//using System.Web.UI.WebControls;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class DesignationMasterDAL
    {
        #region Select Fxns...
        /// <summary>
        /// This for select all tblDesignation
        /// </summary>
        /// <returns>return list of tblDesignation</returns>
        public List<tblDesignation> Select()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from s in db.tblDesignations where s.Status == true select s).ToList();
                return lst;
            }
        }

        /// <summary>
        /// This is for select by tblDesignation Desig_Idno
        /// </summary>
        /// <param name="desigId">Desig_Idno of tblDesignation</param>
        /// <returns>return the table of tblDesignation</returns>
        public tblDesignation SelectById(int desigId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from dm in db.tblDesignations where dm.Desig_Idno == desigId select dm).FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intDepartmentIdno"></param>
        /// <param name="strDeptName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strDeptName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblDesignations
                           orderby mast.Desig_Name
                           select new
                           {
                               DesigIdno = mast.Desig_Idno,
                               DesigName = mast.Desig_Name,
                               Status = mast.Status,
                           }).ToList();
                if (strDeptName != "")
                {
                    lst = (from I in lst where I.DesigName.ToLower().Contains(strDeptName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        public Int64 SelectDesigIdno()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from D in db.tblDesignations orderby D.Desig_Idno descending select D.Desig_Idno).FirstOrDefault();
            }
        }

        public IList Select(string strDesigNM)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from dsg in db.tblDesignations
                           orderby dsg.Desig_Name
                           select new
                           {
                               DesgIdno=dsg.Desig_Idno,
                               DesgNm=dsg.Desig_Name,
                               IsActive = dsg.Status,
                              
                           }).ToList();
                if (strDesigNM != "")
                {
                    lst = (from I in lst where I.DesgNm.ToLower().StartsWith(strDesigNM.ToLower()) select I).ToList();
                }
                return lst;
            }
        }
    
        #endregion

        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 sc = (from co in db.tblDesignations where co.Status==true select co).Count();
                return sc;
            }

        }
        #region InsertUpdate...
        /// <summary>
        /// Insert records in tblDesignation
        /// </summary>
        /// <param name="strDesigName"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strDesigName, bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesignation objtblDesignation = new tblDesignation();
                    objtblDesignation.Desig_Name = strDesigName;
                    objtblDesignation.Status = bStatus;
                    objtblDesignation.Emp_Idno = empIdno;
                    objtblDesignation.Date_Added = System.DateTime.Now;
                    objtblDesignation.UserRights_Status = false;
                    if (IsExists(strDesigName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.AddTotblDesignations(objtblDesignation);
                        db.SaveChanges();
                        intValue = objtblDesignation.Desig_Idno;
                    }
                }
            }
            catch
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }
        
        public int Update(string strDesigName, bool bStatus, int intDesigIdno, Int32 empIdno)
        {
            int intValue = 0; 
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesignation objtblDesignation = (from mast in db.tblDesignations
                                                    where mast.Desig_Idno == intDesigIdno
                                                    select mast).FirstOrDefault();
                    if (objtblDesignation != null)
                    {
                        objtblDesignation.Desig_Name = strDesigName;
                        objtblDesignation.Status = bStatus;
                        objtblDesignation.Emp_Idno = empIdno;

                        if (objtblDesignation.UserRights_Status == true)
                        {
                            objtblDesignation.UserRights_Status = true;
                        }
                        else
                            objtblDesignation.UserRights_Status = false;
                            objtblDesignation.Status = false;
                        objtblDesignation.Date_Added = System.DateTime.Now;
                        objtblDesignation.Date_Modified = System.DateTime.Now;
                        if (IsExists(strDesigName, intDesigIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intDesigIdno;
                        }
                    }
                }
            }
            catch
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public bool IsExists(string strDesigName, int intDesigIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblDesignation objtblDesignation = new tblDesignation();
                if (intDesigIdno <= 0)
                {
                    objtblDesignation = (from mast in db.tblDesignations
                                       where mast.Desig_Name == strDesigName
                                       select mast).FirstOrDefault();
                }
                else if (intDesigIdno > 0)
                {
                    objtblDesignation = (from mast in db.tblDesignations
                                       where mast.Desig_Name == strDesigName
                                         && mast.Desig_Idno != intDesigIdno
                                       select mast).FirstOrDefault();
                }
                if (objtblDesignation != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
         
        public int Delete(int intDesigIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesignation objtblDesignation = (from mast in db.tblDesignations
                                                    where mast.Desig_Idno == intDesigIdno
                                                    select mast).FirstOrDefault();
                    if (objtblDesignation != null)
                    {
                        db.DeleteObject(objtblDesignation);
                        db.SaveChanges();
                        intValue = 1;
                    }
                }
            }
            catch
            {
            }
            return intValue;
        }

        public Int32 UpdateStatus(int intDesigIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesignation objtblDesignation = (from mast in db.tblDesignations
                                                    where mast.Desig_Idno == intDesigIdno
                                                    select mast).FirstOrDefault();
                    if (objtblDesignation != null)
                    {
                        objtblDesignation.Emp_Idno = empIdno;
                        objtblDesignation.Status = Status;
                        objtblDesignation.Date_Modified = System.DateTime.Now;
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch
            {

            }
            return value;
        }

        public Int64 InsertIntotblDesigRightss(Int64 intDesigIdno, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var lst = (from FM in db.tblFormMasts select FM).ToList();
                    foreach (var UR in lst)
                    {
                        tblDesigRight objtblDesigRights = new tblDesigRight();
                        if (UR.Rights_Default == true)
                        {
                            objtblDesigRights.Desig_Idno = intDesigIdno;
                            objtblDesigRights.Form_Idno = UR.Form_Idno;
                            objtblDesigRights.Emp_Idno = empIdno;
                            objtblDesigRights.View = true;
                            objtblDesigRights.Edit = true;
                            objtblDesigRights.Delete = true;
                            objtblDesigRights.Print = true;
                            objtblDesigRights.ADD = true;
                            
                            db.AddTotblDesigRights(objtblDesigRights);
                            db.SaveChanges();
                            intValue = objtblDesigRights.DesigRghts_Idno;
                        }
                        else
                        {
                            objtblDesigRights.Desig_Idno = intDesigIdno;
                            objtblDesigRights.Form_Idno = UR.Form_Idno;
                            objtblDesigRights.View = false;
                            objtblDesigRights.Edit = false;
                            objtblDesigRights.Delete = false;
                            objtblDesigRights.Print = false;
                            objtblDesigRights.ADD = false;
                            db.AddTotblDesigRights(objtblDesigRights);
                            db.SaveChanges();
                            intValue = objtblDesigRights.DesigRghts_Idno;
                        }
                    }
                }
            }
            catch
            {
            }
            return intValue;
        }
        #endregion
    }
}