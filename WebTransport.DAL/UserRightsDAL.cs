using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using WebTransport.DAL;

namespace WebTransport.DAL
{
    public class UserRightsDAL
    {
        #region Select Functions...
        public IList SelectDesignation()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from D in db.tblDesignations
                           orderby D.Desig_Name ascending
                           select new
                           {
                               D.Desig_Idno,
                               D.Desig_Name
                           }).ToList();
                return lst;
            }
        }

        public IList SelectByAdminId(Int32 intUserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var usr = (from um in db.tblUserMasts
                           where um.Status == true && um.User_Idno == intUserId
                           orderby um.Emp_Name
                           select new
                           {
                               Emp_Name = um.Emp_Name,
                               User_Idno = um.User_Idno,
                           }).Distinct().ToList();

                return usr;
            }
        }

        public IList SelectUser()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from U in db.tblUserMasts
                           where U.Status == true && U.Class != "Admin"
                           orderby U.Emp_Name ascending
                           select new
                           {
                               U.User_Idno,
                               U.Emp_Name
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeForm(Int32 intUserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from UR in db.tblUserRights
                           join FM in db.tblFormMasts on UR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "MS" && UR.Emp_Idno == intUserIdno
                           select new
                           {
                               UR.UserRgt_Idno,
                               UserIdno = UR.Emp_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = UR.ADD,
                               Delete = UR.Delete,
                               Edit = UR.Edit,
                               Print = UR.Print,
                               View = UR.View,
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeMenu(Int32 intUserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from UR in db.tblUserRights
                           join FM in db.tblFormMasts on UR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "ME" && UR.Emp_Idno == intUserIdno
                           select new
                           {
                               UR.UserRgt_Idno,
                               UserIdno = UR.Emp_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = UR.ADD,
                               Delete = UR.Delete,
                               Edit = UR.Edit,
                               Print = UR.Print,
                               View = UR.View,
                           }).ToList();
                return lst;
            }
        }

        public IList SelectForGridTypeRep(Int32 intUserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from UR in db.tblUserRights
                           join FM in db.tblFormMasts on UR.Form_Idno equals FM.Form_Idno
                           orderby FM.Form_Menu ascending
                           where FM.Form_Type == "RP" && UR.Emp_Idno == intUserIdno
                           select new
                           {
                               UR.UserRgt_Idno,
                               UserIdno = UR.Emp_Idno,
                               Form_Idno = FM.Form_Idno,
                               FormName = FM.Form_Name,
                               FormMenu = FM.Form_Menu,
                               ADD = UR.ADD,
                               Delete = UR.Delete,
                               Edit = UR.Edit,
                               Print = UR.Print,
                               View = UR.View,
                           }).ToList();
                return lst;
            }
        }
        #endregion

        #region InsertUpdate Events...
        public Int32 UpdateAdd(int intUserRghtsIdno, bool bADD, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {
                        objtbltblUserRight.ADD = bADD;
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
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

        public Int32 UpdateEdit(int intUserRghtsIdno, bool bEdit, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {

                        objtbltblUserRight.Edit = bEdit;
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
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

        public Int32 UpdateView(int intUserRghtsIdno, bool bView, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {

                        objtbltblUserRight.View = bView;
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
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

        public Int32 UpdateDelete(int intUserRghtsIdno, bool bDelete, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {

                        objtbltblUserRight.Delete = bDelete;
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
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

        public Int32 UpdatePrint(int intUserRghtsIdno, bool bPrint, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {

                        objtbltblUserRight.Print = bPrint;
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
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

        public Int32 UpdateAll(int intUserRghtsIdno, bool bAdd, bool bEdit, bool bView, bool bDelete, bool bPrint, Int32 UpdateEmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserRight objtbltblUserRight = (from mast in db.tblUserRights
                                                       where mast.UserRgt_Idno == intUserRghtsIdno
                                                       select mast).FirstOrDefault();
                    if (objtbltblUserRight != null)
                    {
                        objtbltblUserRight.User_Idno = UpdateEmpIdno;
                        objtbltblUserRight.ADD = bAdd;
                        objtbltblUserRight.Edit = bEdit;
                        objtbltblUserRight.View = bView;
                        objtbltblUserRight.Delete = bDelete;
                        objtbltblUserRight.Print = bPrint;
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