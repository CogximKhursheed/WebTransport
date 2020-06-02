using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class EmployeeMasterDAL
    {
        #region Select Events...
        /// <summary>
        /// Select all records from tblUserMast
        /// </summary>
        /// <returns></returns>
        public IList Select(string strEmpName, Int32 DesigId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblUserMasts
                           //  join w in db.tblLocMasts on mast.WorkArea_Idno equals w.LocArea_Idno
                           join D in db.tblDesignations on mast.Desig_Idno equals D.Desig_Idno
                           orderby mast.Emp_Name
                           select new
                           {
                               EmpIdno = mast.User_Idno,
                               Name = mast.Emp_Name,
                               Email = mast.User_EmailId,
                               DOJ = mast.DOJ,
                               Username = mast.User_Name,
                               //  WorkAreaid = mast.WorkArea_Idno,
                               //  WorkArea = w.Loc_AreaName,
                               IsActive = mast.Status,
                               DesigIdno = mast.Desig_Idno,
                               D.Desig_Abbr,
                               D.Desig_Name

                           }).ToList();
                if (strEmpName != "")
                {
                    lst = (from I in lst where I.Name.ToLower().Contains(strEmpName.ToLower()) select I).ToList();
                }
                if (DesigId > 0)
                {
                    lst = (from I in lst where I.DesigIdno == DesigId select I).ToList();
                }
                return lst;
            }
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 c = 0;
                c = (from co in db.tblUserMasts join D in db.tblDesignations on co.Desig_Idno equals D.Desig_Idno orderby co.Emp_Name select co).Count();
                return c;
            }
        }
        public List<tblCityMaster> BindToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    where obj.AsLocation == true
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }

        /// <summary>
        /// Select one record from tblUserMast by EmpIdno
        /// </summary>
        /// <param name="intEmpIdno"></param>
        /// <returns></returns>
        public IList SelectById(Int64 intEmpIdno)
        {
            IList lst = null;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                lst = (from U in db.tblUserMasts
                       where U.User_Idno == intEmpIdno
                       select new
                       {
                           EmpIdno = U.User_Idno,
                           EmpName = U.Emp_Name,
                           FName = U.User_FName,
                           Computer_User = U.Computer_User,
                           DOB = U.DOB,
                           DOJ = U.DOJ,
                           DOR = U.DOL,
                           Phone = U.Phone_No,
                           Mobile = U.Mobile_No,
                           EMAIL = U.User_EmailId,
                           Address = U.Address,
                           City = U.City_Idno,
                           State = U.State_Idno,
                           Desig = U.Desig_Idno,
                           PinCode = U.Pin_No,
                           U.Gender,
                           //  SupIdno = U.Superviser_Idno,
                           Photo = U.PHOTO,
                           Remark = U.REMARKS,
                           Status = U.Status,
                           UserName = U.User_Name,
                           Password = U.User_Password,
                           // WrkArea = U.WorkArea_Idno,
                           //   CareOf_Idno = U.CareOf_Idno,
                       }).ToList();
            return lst;
        }

        //public List<CareOF> SelectCareOF()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {

        //        return (from CO in db.CareOFs where CO.Status == true select CO).ToList();
        //    }

        //}
        public tblDesignation SelectDesigRghtsStatus(Int32 intDesigIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from DR in db.tblDesignations where DR.Desig_Idno == intDesigIdno select DR).FirstOrDefault();
                return lst;
            }
        }

        public bool SelectExistInMasterDB(String Email, String Pass, Int32 EmpIdnno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                if (EmpIdnno > 0)
                {
                    var lst = (from DR in db.tblLoginEmails where DR.LoginEmail == Email && DR.Password == Pass && DR.UserID != EmpIdnno select DR).FirstOrDefault();
                    if (lst != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    var lst = (from DR in db.tblLoginEmails where DR.LoginEmail == Email && DR.Password == Pass select DR).FirstOrDefault();
                    if (lst != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        //public List<LocAreaMast> SelectLocArea()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from LA in db.LocAreaMasts select LA).ToList();
        //    }
        //}

        public IList SelectLocation(Int32 intLocAreaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.tblLocMasts
                        where LM.Status == true
                        select new
                            {
                                LM.Loc_Idno,
                                LM.Loc_Name
                            }).ToList();
            }
        }

        //public List<EmpLocDetl> SelectEmpLocDetl(Int64 intUserIdno)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from mast in db.EmpLocDetls
        //                   where mast.Emp_Idno == intUserIdno
        //                   select mast).ToList();
        //        return lst;
        //    }
        //}

        public DataSet PopulateFromCityMultiple(int UserIdno, string constring)
        {
            SqlConnection con = new SqlConnection(constring);
            string strSql = "";
            strSql = "SELECT A.City_Name,A.City_Idno FROM (SELECT TC.City_Name,TC.City_Idno,isnull(TD.frmcity_idno,0)fromcity_idno,'1' typ FROM tblcitymaster TC inner JOIN tblFrmCityDetl TD on TC.city_idno=TD.frmcity_idno inner join tblUserMast UM on UM.User_Idno=TD.User_Idno where TD.User_Idno=" + UserIdno + " UNION ALL SELECT TC.City_Name,TC.City_Idno,isnull(TC.City_Idno,0)fromcity_idno,'0' typ FROM tblcitymaster TC  WHERE tc.aslocation=1 and tc.City_Idno not in (SELECT frmcity_idno FROM tblFrmCityDetl WHERE User_Idno=" + UserIdno + ")) A ORDER BY A.Typ DESC,A.City_Name";
            SqlDataAdapter sda = new SqlDataAdapter(strSql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public List<tblFrmCityDetl> SelectFromCityMultiple(Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from P in db.tblFrmCityDetls
                           where P.User_Idno == UserIdno
                           orderby P.FrmCityDetl_Idno
                           select P).ToList();
                return lst;
            }
        }

        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in USER
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Insert records in USER
        /// </summary>
        /// <returns></returns>
        public Int64 Insert(string strName, string strFName, DateTime? dtDOB, DateTime? dtDOJ, DateTime? dtDOR, string strPhone, string strMobile, string strEmail, string strloginname,
                            string strPassword, string strAddress, Int64 intCityIdno, Int64 intStateIdno, Int64 intDesigIdno, string strPinCode,
                            string strPhoto, string strRemarks, bool bStatus, Int32 intYearIdno, Int32 intCompIdno, string Gndr, bool IntComputer, Int32 empIdno, Int32 LoginCompIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserMast objUsers = new tblUserMast();
                    if (IsExists(strName, strEmail, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        objUsers.Emp_Name = strName;
                        objUsers.User_FName = strFName;
                        objUsers.DOB = dtDOB;
                        objUsers.DOJ = dtDOJ;
                        objUsers.DOL = dtDOR;
                        objUsers.Emp_Idno = empIdno;
                        objUsers.Phone_No = strPhone;
                        objUsers.Mobile_No = strMobile;
                        objUsers.User_EmailId = strEmail;
                        objUsers.User_Name = strloginname;
                        objUsers.User_Password = strPassword;
                        objUsers.Address = strAddress;
                        objUsers.City_Idno = intCityIdno;
                        objUsers.State_Idno = intStateIdno;
                        objUsers.Desig_Idno = intDesigIdno;
                        objUsers.Class = "Staff";
                        objUsers.Pin_No = strPinCode;
                        objUsers.PHOTO = strPhoto;
                        objUsers.REMARKS = strRemarks;
                        objUsers.Status = bStatus;
                        objUsers.Comp_Idno = intCompIdno;
                        objUsers.Gender = Gndr;
                        objUsers.Computer_User = IntComputer;
                        objUsers.Date_Added = System.DateTime.Now;
                        objUsers.Date_Modified = System.DateTime.Now;
                        objUsers.LoginComp_Idno = LoginCompIdno;
                        db.tblUserMasts.AddObject(objUsers);
                        db.SaveChanges();
                        intValue = objUsers.User_Idno;
                    }
                }
            }
            catch
            {
                intValue = 0;
            }
            return intValue;
        }

        /// <summary>
        /// Update records in USER
        /// </summary>
        /// <returns></returns>
        public Int64 Update(Int64 intEmpIdno, string strName, string strFName, DateTime? dtDOB, DateTime? dtDOJ, DateTime? dtDOR, string strPhone, string strMobile, string strEmail,
                            string strloginname, string strPassword, string strAddress, Int64 intCityIdno, Int64 intStateIdno, Int64 intDesigIdno, string strPinCode,
                             string strPhoto, string strRemarks, bool bStatus, Int32 intCompIdno, string Gndr, bool bComputer, Int32 empIdno, Int32 LoginCompIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserMast objUsers = (from mast in db.tblUserMasts
                                            where mast.User_Idno == intEmpIdno && mast.Comp_Idno == intCompIdno
                                            select mast).FirstOrDefault();
                    if (objUsers != null)
                    {
                        if (IsExists(strName, strEmail, intEmpIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            objUsers.Emp_Name = strName;
                            objUsers.User_FName = strFName;
                            objUsers.DOB = dtDOB;
                            objUsers.DOJ = dtDOJ;
                            objUsers.DOL = dtDOR;
                            objUsers.Emp_Idno = empIdno;
                            objUsers.Phone_No = strPhone;
                            objUsers.Mobile_No = strMobile;
                            objUsers.User_EmailId = strEmail;
                            objUsers.User_Name = strloginname;
                            objUsers.User_Password = strPassword;
                            objUsers.Address = strAddress;
                            objUsers.City_Idno = intCityIdno;
                            objUsers.State_Idno = intStateIdno;
                            objUsers.Desig_Idno = intDesigIdno;
                            objUsers.Pin_No = strPinCode;
                            objUsers.Gender = Gndr;
                            objUsers.Computer_User = bComputer;
                            if (string.IsNullOrEmpty(strPhoto) == true)
                            {
                                objUsers.PHOTO = objUsers.PHOTO;
                            }
                            else
                            {
                                objUsers.PHOTO = strPhoto;
                            }
                            objUsers.REMARKS = strRemarks;
                            objUsers.Status = bStatus;
                            objUsers.Comp_Idno = intCompIdno;
                            objUsers.Date_Modified = System.DateTime.Now;
                            objUsers.LoginComp_Idno = LoginCompIdno;
                            intValue = objUsers.User_Idno;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch
            {
                intValue = 0;
            }
            return intValue;
        }

        /// <summary>
        /// Delete record from USER
        /// </summary>
        /// <param name="intEmpIdno"></param>
        /// /// <param name="intCompIdno"></param>
        /// <returns></returns>
        public int Delete(Int64 intEmpIdno, Int32 intCompIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserMast objUseres = (from mast in db.tblUserMasts
                                             where mast.User_Idno == intEmpIdno
                                             select mast).FirstOrDefault();
                    if (objUseres != null)
                    {
                        db.tblUserMasts.DeleteObject(objUseres);
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

        public int DeleteFromLogin(Int64 intEmpIdno, Int32 intCompIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblLogin objLogin = (from mast in db.tblLogins
                                         where mast.EmpId == intEmpIdno
                                         select mast).FirstOrDefault();
                    if (objLogin != null)
                    {
                        db.tblLogins.DeleteObject(objLogin);
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

        public Int64 InsertMultpleFromCity(Int64 HeadIdno, System.Web.UI.WebControls.CheckBoxList chklistFromcity)
        {
            Int64 fromcityDetlIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {

                    for (int i = 0; i < chklistFromcity.Items.Count; i++)
                    {
                        if (chklistFromcity.Items[i].Selected == true)
                        {
                            tblFrmCityDetl obj = new tblFrmCityDetl();
                            obj.User_Idno = HeadIdno;
                            obj.FrmCity_Idno = Convert.ToInt32(chklistFromcity.Items[i].Value);
                            db.AddTotblFrmCityDetls(obj);
                            db.SaveChanges();
                            fromcityDetlIdno = obj.FrmCityDetl_Idno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return fromcityDetlIdno;

        }

        public Int64 UpdateMultpleFromCity(Int64 HeadIdno, System.Web.UI.WebControls.CheckBoxList chklistFromcity)
        {
            Int64 ID = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    List<tblFrmCityDetl> objDetls = db.tblFrmCityDetls.Where(pcd => pcd.User_Idno == HeadIdno).ToList();
                    if (objDetls.Count > 0)
                    {
                        foreach (tblFrmCityDetl pcd in objDetls)
                        {
                            db.DeleteObject(pcd);
                            db.SaveChanges();
                        }
                    }
                    for (int i = 0; i < chklistFromcity.Items.Count; i++)
                    {
                        if (chklistFromcity.Items[i].Selected == true)
                        {
                            tblFrmCityDetl obj = new tblFrmCityDetl();
                            obj.User_Idno = HeadIdno;
                            obj.FrmCity_Idno = Convert.ToInt32(chklistFromcity.Items[i].Value);
                            db.AddTotblFrmCityDetls(obj);
                            db.SaveChanges();
                            ID = obj.FrmCityDetl_Idno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return ID;
        }

        public Int64 InsertIntoUserRights(Int64 intUserIdno, Int32 intDesigIdno, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var lst = (from FM in db.tblDesigRights where FM.Desig_Idno == intDesigIdno select FM).ToList();
                    foreach (var UR in lst)
                    {
                        tblUserRight objUserRight = new tblUserRight();
                        objUserRight.Emp_Idno = Convert.ToInt32(intUserIdno);
                        objUserRight.Form_Idno = UR.Form_Idno;
                        objUserRight.View = UR.View;
                        objUserRight.User_Idno = empIdno; // this is user idno is that idno = whi is updated , deleted , modified, inserted 
                        objUserRight.Edit = UR.Edit;
                        objUserRight.Delete = UR.Delete;
                        objUserRight.Print = UR.Print;
                        objUserRight.ADD = UR.ADD;
                        db.tblUserRights.AddObject(objUserRight);
                        db.SaveChanges();
                        intValue = Convert.ToInt32(objUserRight.Emp_Idno);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return intValue;
        }

        /// <summary>
        /// To check record existence in login
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public bool IsExists(string strLoginName, string strEmail, Int64 EmpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserMast objtblUser = new tblUserMast();
                if (EmpIdno <= 0)
                {
                    objtblUser = (from mast in db.tblUserMasts
                                  where mast.Emp_Name == strLoginName && mast.User_EmailId == strEmail
                                  select mast).FirstOrDefault();
                }
                if (EmpIdno > 0)
                {
                    objtblUser = (from mast in db.tblUserMasts
                                  where mast.Emp_Name == strLoginName && mast.User_EmailId == strEmail && mast.User_Idno != EmpIdno
                                  select mast).FirstOrDefault();
                }
                if (objtblUser != null && objtblUser.User_Idno > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public Int32 UpdateStatus(int intEmpIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserMast objUsrs = (from mast in db.tblUserMasts
                                           where mast.User_Idno == intEmpIdno
                                           select mast).FirstOrDefault();
                    if (objUsrs != null)
                    {

                        objUsrs.Status = Status;
                        objUsrs.Emp_Idno = empIdno;
                        objUsrs.Date_Modified = System.DateTime.Now;
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

        public bool EmailExist(String Email, Int64 CompIdno, Int32 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (CompIdno > 0)
                {
                    var lst = (from DR in db.tblUserMasts where DR.User_EmailId == Email && DR.Comp_Idno == CompIdno && DR.User_Idno != UserIdno select DR).FirstOrDefault();
                    if (lst != null && lst.User_Idno > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    var lst = (from DR in db.tblUserMasts where DR.User_EmailId == Email select DR).FirstOrDefault();
                    if (lst != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }
        #endregion
    }
}