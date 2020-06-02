using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.Objects;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class DriverMastDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from DriverMast
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.DriverMasts
                        orderby mast.Driver_Name
                        select mast).ToList();
            }
        }
        public List<tblUserMast> Selectemp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblUserMast> lst = null;
                lst = (from cm in db.tblUserMasts orderby cm.Emp_Name ascending select cm).ToList();
                return lst;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intItemTypeIdno"></param>
        /// <param name="strItemName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strDriverName, string strLicense, int varifiedall, Int32 guarantor)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.DriverMasts
                           join um in db.tblUserMasts on mast.Guarantor equals um.User_Idno into gj
                           from full in gj.DefaultIfEmpty()
                           select new
                           {
                               DriverIdno = mast.Driver_Idno,
                               DriverName = mast.Driver_Name,
                               DriverNameHindi = mast.DriverName_Hindi,
                               Status = mast.Status,
                               LicenseNo = mast.License_No,
                               ExpiryDate = mast.Expiry_Date,
                               Varified = mast.Varified,
                               AccountNo = mast.Account_no,
                               GuarantorNm = full.User_Name,
                               Guarantor = mast.Guarantor,
                               Authority = mast.LicenseAuthority,

                               Varified_Nm = (mast.Varified == true ? "Yes" : "No"),
                               Statuss = (mast.Status == true ? "Yes" : "No")

                           }).ToList();
                if (strDriverName != "")
                {

                    lst = (from I in lst where I.DriverName.ToLower().Contains(strDriverName.ToLower()) select I).ToList();
                }
                if (varifiedall >= 0)
                {
                    lst = (from I in lst where I.Varified == Convert.ToBoolean(varifiedall) select I).ToList();
                }
                if (strLicense != "")
                {
                    lst = (from I in lst where I.LicenseNo == strLicense && I.LicenseNo != null select I).ToList();
                }
                if (guarantor > 0)
                {
                    lst = (from I in lst where I.Guarantor == guarantor select I).ToList();
                }
                return lst;
            }
        }

        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 sc = (from co in db.DriverMasts where co.Status == true select co).Count();
                return sc;
            }

        }
        /// <summary>
        /// Select one record from DriverMast by DriverIdno
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>
        public DriverMast SelectById(int intDriverIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.DriverMasts
                        where mast.Driver_Idno == intDriverIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To check record existence in DriverMast
        /// </summary>
        /// <param name="strDriverName"></param>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strDriverName, int intDriverIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                DriverMast objDrivMast = new DriverMast();
                if (intDriverIdno <= 0)
                {
                    objDrivMast = (from mast in db.DriverMasts
                                   where mast.Driver_Name == strDriverName
                                   select mast).FirstOrDefault();
                }
                else if (intDriverIdno > 0)
                {
                    objDrivMast = (from mast in db.DriverMasts
                                   where mast.Driver_Name == strDriverName
                                         && mast.Driver_Idno != intDriverIdno
                                   select mast).FirstOrDefault();
                }
                if (objDrivMast != null)
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
        /// Insert records in DriverMast
        /// </summary>
        /// <param name="strDriverName"></param>
        /// <param name="strColrCode"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>//

        public int Insert(string strDriverName, string strDriverNameHindi, bool bStatus, string strLicense_No, DateTime? dtExpiryDate, bool bVarified, string strAccount_No, Int64 strGaurantor, string strAuthority, Int32 empIdno)
        {
            int intValue = 0;
            Int32 intDriverIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DriverMast objDrivMast = new DriverMast();
                    objDrivMast.Driver_Name = strDriverName;
                    objDrivMast.DriverName_Hindi = strDriverNameHindi;
                    objDrivMast.Status = bStatus;
                    objDrivMast.Emp_Idno = empIdno;
                    objDrivMast.Comp_Idno = intCompIdno;
                    objDrivMast.Date_Added = System.DateTime.Now;
                    objDrivMast.License_No = strLicense_No;
                    objDrivMast.Expiry_Date = dtExpiryDate;
                    objDrivMast.Varified = bVarified;
                    objDrivMast.Account_no = strAccount_No;
                    objDrivMast.Guarantor = strGaurantor;
                    objDrivMast.LicenseAuthority = strAuthority;
                    if (IsExists(strDriverName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.DriverMasts.AddObject(objDrivMast);
                        db.SaveChanges();
                        intValue = Convert.ToInt32(objDrivMast.Driver_Idno);
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
        /// Update records in DriverMast
        /// </summary>
        /// <param name="strDriverName"></param>
        /// <param name="strColrCode"></param>
        /// <param name="bStatus"></param>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>
        /// 
        
        public int Update(string strDriverName, string strDriverNameHindi, bool bStatus, Int32 intDriverIdno, string strLicense_No, DateTime? dtExpiryDate, bool bVarified, string strAccount_No, Int64 strGaurantor, string strAuthority, Int32 empIdno)
        {

            int intValue = 0;
            // Int32 intDriverIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DriverMast objDrivMast = (from mast in db.DriverMasts
                                              where mast.Driver_Idno == intDriverIdno
                                              select mast).FirstOrDefault();
                    if (objDrivMast != null)
                    {
                        objDrivMast.Emp_Idno = empIdno;
                        objDrivMast.Driver_Name = strDriverName;
                        objDrivMast.DriverName_Hindi = strDriverNameHindi;
                        objDrivMast.Status = bStatus;
                        objDrivMast.Comp_Idno = intCompIdno;
                        objDrivMast.Date_Modified = System.DateTime.Now;
                        objDrivMast.License_No = strLicense_No;
                        objDrivMast.Expiry_Date = dtExpiryDate;
                        objDrivMast.Varified = bVarified;
                        objDrivMast.Account_no = strAccount_No;
                        objDrivMast.Guarantor = strGaurantor;
                        objDrivMast.LicenseAuthority = strAuthority;
                        if (IsExists(strDriverName, intDriverIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intDriverIdno;
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
        /// Delete record from DriverMast
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>
        public int Delete(int intDriverIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DriverMast objDrivMast = (from mast in db.DriverMasts
                                              where mast.Driver_Idno == intDriverIdno
                                              select mast).FirstOrDefault();
                    if (objDrivMast != null)
                    {
                        db.DriverMasts.DeleteObject(objDrivMast);
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

        public Int32 UpdateStatus(int intDriverIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DriverMast objDrivMast = (from mast in db.DriverMasts
                                              where mast.Driver_Idno == intDriverIdno
                                              select mast).FirstOrDefault();
                    if (objDrivMast != null)
                    {
                        objDrivMast.Emp_Idno = empIdno;
                        objDrivMast.Status = Status;                        
                        objDrivMast.Date_Modified = System.DateTime.Now;
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


        public IList CheckItemExistInOtherMaster(Int32 DriverIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj11 in db.LorryMasts
                        where obj11.Driver_Idno == DriverIdno
                        select new { ItemGrp = obj11.Driver_Idno }
                          ).ToList()
                           .Union
                          (from obj13 in db.tblChlnBookHeads
                           where obj13.Driver_Idno == DriverIdno
                           select new { ItemGrp = obj13.Driver_Idno }
                          ).ToList()
                          .Union
                          (from obj14 in db.tblSummaryRegisters
                           where obj14.Driver_idno == DriverIdno
                           select new { ItemGrp = obj14.Driver_idno }
                          ).ToList()
                         ;
            }
        }

        public long? intCompIdno { get; set; }

        public string Driver_Name { get; set; }

        public string DriverName_Hindi { get; set; }

        public string Driver_Idno { get; set; }

        public DateTime Status { get; set; }

        //public string License_No { get; set; }

        //public DateTime Expiry_Date { get; set; }

        //public DateTime Varified { get; set; }

        //public string Account_No { get; set; }
        //public string Gaurantor { get; set; }


    }
}