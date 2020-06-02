using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WebTransport.DAL
{
    public class LoginDAL
    {
        public Int64 Select(string Email, string Password)
        {
            Int64 intvalue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                intvalue = (from UM in db.tblUserMasts where UM.Status == true && UM.User_EmailId == Email && UM.User_Password == Password select UM).Count();
                return intvalue;
            }
        }

        public tblUserMast SelectUserLogin(string Email, string Password)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from UM in db.tblUserMasts
                           where UM.Status == true && UM.User_EmailId == Email && UM.User_Password == Password
                           select UM).FirstOrDefault();
                return lst;
            }
        }

        public tblUserRight SelectUserRights(Int32 intUserIdno, Int32 intFormIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from UM in db.tblUserRights where UM.Emp_Idno == intUserIdno && UM.Form_Idno == intFormIdno select UM).SingleOrDefault();
                return lst;
            }
        }

        public tblCompMast SelectCompanyDetl()
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from CM in db.tblCompMasts select CM).FirstOrDefault();
                return lst;
            }
        }
        
        public string SelectCompanyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string CompName = (from CM in db.tblCompMasts select CM.Comp_Name).FirstOrDefault();
                return CompName;
            }
        }

        public string SelectLastLoginOnUserId(string intUserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string LastLoginDate = "";

                tblLogDetail objtblLogDetail = (from mast in db.tblLogDetails where mast.User_Id == intUserId select mast).FirstOrDefault();

                if (objtblLogDetail != null)
                {
                    LastLoginDate = Convert.ToString((from LD in db.tblLogDetails where LD.User_Id == intUserId select LD.Login_DateTime).Max());
                }
                objtblLogDetail = null;
                return LastLoginDate;
            }
        }

        public Int64 InsertLogDetails(string UserId, string LoginEmail, string IPAddress, bool LoginStatus)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                tblLogDetail ObjtblLogDetail = new tblLogDetail();

                ObjtblLogDetail.User_Id = UserId;
                ObjtblLogDetail.User_LoginEmail = LoginEmail;
                ObjtblLogDetail.IPAddress = IPAddress;
                ObjtblLogDetail.Login_DateTime = System.DateTime.Now;
                //ObjtblLogDetail.Logout_Date = System.DateTime.Now;
                ObjtblLogDetail.Login_Status = LoginStatus;

                db.AddTotblLogDetails(ObjtblLogDetail);
                db.SaveChanges();
                value = ObjtblLogDetail.Id;
                return value;
            }
        }

        public Int64 UpdateLogDetails(Int64 LogId, bool LoginStatus)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblLogDetail objtblLogDetail = (from mast in db.tblLogDetails
                                                where mast.Id == LogId
                                                select mast).FirstOrDefault();
                if (objtblLogDetail != null)
                {
                    objtblLogDetail.Logout_DateTime = System.DateTime.Now;
                    objtblLogDetail.Login_Status = LoginStatus;
                    db.SaveChanges();
                    value = LogId;
                }
            }
            return value;
        }

        //public List<tblDesigRight> SelectDesigRights(Int32 intDesigIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from DR in db.tblDesigRights where DR.Desig_Idno == intDesigIdno select DR).ToList();
        //        return lst;
        //    }
        //}

        #region Multiple DB...

        public tblLoginEmail SelectUserLoginInMasterDB(string Email, string Password)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from UM in db.tblLoginEmails where UM.Status == true && UM.LoginEmail == Email && UM.Password == Password select UM).FirstOrDefault();
                return lst;
            }
        }

        public tblTranspCust SelectDBName(Int32 CompID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from UM in db.tblTranspCusts where UM.CompID == CompID && UM.Status == true select UM).FirstOrDefault();
                return lst;
            }
        }

        public string UserClass(Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string Class = Convert.ToString(db.tblUserMasts.Where(U => U.User_Idno == UserIdno).Select(p => p.Class).FirstOrDefault());
                return Class;
            }
        }

        public string UserActive(Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string Status = Convert.ToString(db.tblUserMasts.Where(U => U.User_Idno == UserIdno).Select(p => p.Status).FirstOrDefault());
                return Status;
            }
        }
        #endregion
    }
}
