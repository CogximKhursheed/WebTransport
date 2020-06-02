using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;

namespace WebTransport.DAL
{
    public class DesigRightsDAL
    {
        #region Select Functions...
        public IList SelectDesignation()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from D in db.tblDesignations
                           where D.Status == true
                           orderby D.Desig_Name ascending
                           select new
                           {
                               D.Desig_Idno,
                               D.Desig_Name
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeForm(Int32 DesigIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from DR in db.tblDesigRights
                           join FM in db.tblFormMasts  on DR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "MS" && DR.Desig_Idno == DesigIdno
                           select new
                           {
                               DR.DesigRghts_Idno,
                               DesigId = DR.Desig_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = DR.ADD,
                               Delete = DR.Delete,
                               Edit = DR.Edit,
                               Print = DR.Print,
                               View = DR.View,
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeMenu(Int32 intDesigIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from DR in db.tblDesigRights
                           join FM in db.tblFormMasts on DR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "ME" && DR.Desig_Idno == intDesigIdno
                           select new
                           {
                               DR.DesigRghts_Idno,
                               DesigId = DR.Desig_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = DR.ADD,
                               Delete = DR.Delete,
                               Edit = DR.Edit,
                               Print = DR.Print,
                               View = DR.View,
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeRep(Int32 intDesigIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from DR in db.tblDesigRights
                           join FM in db.tblFormMasts on DR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "RP" && DR.Desig_Idno == intDesigIdno
                           select new
                           {
                               DR.DesigRghts_Idno,
                               DesigId = DR.Desig_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = DR.ADD,
                               Delete = DR.Delete,
                               Edit = DR.Edit,
                               Print = DR.Print,
                               View = DR.View,
                           }).ToList();
                return lst;
            }
        }
        #endregion

        #region InsertUpdate Events...
        public Int32 UpdateDesigRights(int intDesigIdno, bool bDesigRights)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesignation objDesignation = (from mast in db.tblDesignations
                                                    where mast.Desig_Idno == intDesigIdno
                                                    select mast).FirstOrDefault();
                    if (objDesignation != null)
                    {
                        objDesignation.UserRights_Status = bDesigRights;
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

        public Int32 UpdateAdd(int intDesigRghtsIdno, bool bADD, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    {

                        objDesigRight.ADD = bADD;
                        objDesigRight.Emp_Idno = empIdno;
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

        public Int32 UpdateEdit(int intDesigRghtsIdno, bool bEdit, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    {

                        objDesigRight.Edit = bEdit;
                        objDesigRight.Emp_Idno = empIdno;
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

        public Int32 UpdateView(int intDesigRghtsIdno, bool bView, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    { 
                        objDesigRight.View = bView;
                        objDesigRight.Emp_Idno = empIdno;
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

        public Int32 UpdateDelete(int intDesigRghtsIdno, bool bDelete, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    {
                        objDesigRight.Delete = bDelete;
                        objDesigRight.Emp_Idno = empIdno;
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

        public Int32 UpdatePrint(int intDesigRghtsIdno, bool bPrint, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    { 
                        objDesigRight.Print = bPrint;
                        objDesigRight.Emp_Idno = empIdno;
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

        public Int32 UpdateAll(int intDesigRghtsIdno, bool bAdd, bool bEdit, bool bView, bool bDelete, bool bPrint, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDesigRight objDesigRight = (from mast in db.tblDesigRights
                                                      where mast.DesigRghts_Idno == intDesigRghtsIdno
                                                      select mast).FirstOrDefault();
                    if (objDesigRight != null)
                    {
                        objDesigRight.Emp_Idno = empIdno;
                        objDesigRight.ADD = bAdd;
                        objDesigRight.Edit = bEdit;
                        objDesigRight.View = bView;
                        objDesigRight.Delete = bDelete;
                        objDesigRight.Print = bPrint;
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