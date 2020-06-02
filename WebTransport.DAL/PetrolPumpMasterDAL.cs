using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WebTransport.DAL
{
    public class PetrolPumpMasterDAL
    {
        #region Select Events...
        /// <summary>
        /// Select all records from StateMaster
        /// </summary>
        /// <returns></returns>

        public List<tblStateMaster> SelectState()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblStateMaster> lst = null;
                lst = (from cm in db.tblStateMasters orderby cm.State_Name ascending select cm).ToList();
                return lst;
            }
        }


        public List<tblPCompanyMaster> SelectPCompName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblPCompanyMaster> lst = null;
                lst = (from cm in db.tblPCompanyMasters orderby cm.PComp_Idno ascending select cm).ToList();
                return lst;
            }
        }

        public IList SelectForSearch(string strPPumpName, Int32 intStateIdno, Int32 intCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.PetrolPumpMasters
                           join sm in db.tblStateMasters on CT.PPump_State equals sm.State_Idno
                           join cm in db.tblCityMasters on CT.PPump_City equals cm.City_Idno
                           join com in db.tblPCompanyMasters on CT.PComp_Idno equals com.PComp_Idno
                           select new
                           {
                               PPumpIdno = CT.PPump_Idno,
                               PPumpName = CT.PPump_Name,
                               PCompIdno=com.PComp_Idno,
                               PCompName = com.PComp_Name,
                               PPumpPersonName=CT.PPumpPerson_Name,
                               PPumpPersonDesignation = CT.PPumpPerson_Designation,
                               PPumpLadlineCode=CT.PPump_LadlineCode,
                               PPumpLadlineno=CT.PPump_Ladlineno,
                               PPumpMobileno=CT.PPump_Mobileno,
                               PPumpAddress1=CT.PPump_Address1,
                               PPumpAddress2 = CT.PPump_Address2,
                               PPumpStateIdno = sm.State_Idno,
                               PPumpCityIdno = cm.City_Idno,
                            
                               PPumpState = sm.State_Name,
                               PPumpCity= cm.City_Name,
                               Status = CT.Status,
                           }).ToList();
                if (intStateIdno > 0 )
                {
                    lst = (from l in lst where l.PPumpStateIdno == intStateIdno select l).ToList();
                }
                if (intCityIdno > 0)
                {
                    lst = (from l in lst where l.PPumpCityIdno == intCityIdno select l).ToList();
                }
                if (strPPumpName != "")
                {
                    lst = (from l in lst where l.PPumpName.ToLower().Contains(strPPumpName.ToLower()) select l).ToList();
                }
                return lst;
            }
        }
        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 sc = (from co in db.PetrolPumpMasters select co).Count();
                return sc;
            }

        }
        public PetrolPumpMaster SelectById(int intPPumpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.PetrolPumpMasters where mast.PPump_Idno == intPPumpIdno select mast).FirstOrDefault();
            }
        }
        #endregion

        #region Insert Update Delete Events...
        /// <summary>
        /// Insert records in PetrolPumpMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertPPumpMaster(string strPPumpName, Int64 PetrolCompanyIdno, string strPPumpPersonName, string strPPersonDesignation, string strPPumpLadlineCode, string strPPumpLadlineno, string strPPumpMobileno, string strPPumpAddress1, string strPPumpAddress2, Int64 intStateIdno, Int64 intCityIdno, bool bStatus, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                PetrolPumpMaster ObjPetrolPumpMaster = new PetrolPumpMaster();
                if (IsExists(strPPumpName, intStateIdno, intCityIdno, 0))
                {
                    value = -1;
                }
                else
                {
                    ObjPetrolPumpMaster.Emp_Idno = empIdno;
                    ObjPetrolPumpMaster.PPump_Name = strPPumpName;
                    ObjPetrolPumpMaster.PComp_Idno = PetrolCompanyIdno;
                    ObjPetrolPumpMaster.PPumpPerson_Name = strPPumpPersonName;
                    ObjPetrolPumpMaster.PPumpPerson_Designation = strPPersonDesignation;
                    ObjPetrolPumpMaster.PPump_LadlineCode = strPPumpLadlineCode;
                    ObjPetrolPumpMaster.PPump_Ladlineno = strPPumpLadlineno;
                    ObjPetrolPumpMaster.PPump_Mobileno = strPPumpMobileno;
                    ObjPetrolPumpMaster.PPump_Address1 = strPPumpAddress1;
                    ObjPetrolPumpMaster.PPump_Address2 = strPPumpAddress2;
                    ObjPetrolPumpMaster.PPump_State = intStateIdno;
                    ObjPetrolPumpMaster.PPump_City = intCityIdno;
                    ObjPetrolPumpMaster.Status = bStatus;
                    
                    ObjPetrolPumpMaster.Date_Added = System.DateTime.Now;
                    db.AddToPetrolPumpMasters(ObjPetrolPumpMaster);
                    db.SaveChanges();
                    value = ObjPetrolPumpMaster.PPump_Idno;
                }
                return value;
            }
        }

        /// <summary>
        /// Update records in PetrolPumpMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="bStatus"></param>
        /// <param name="intCityIdno"></param>
        /// <param name="intStateIdno"></param>
        /// <returns></returns>

        public Int64 UpdatePPumpMaster(string strPPumpName, Int64 PetrolCompanyIdno, string strPPumpPersonName, string strPPersonDesignation, string strPPumpLadlineCode, string strPPumpLadlineno, string strPPumpMobileno, string strPPumpAddress1, string strPPumpAddress2, Int64 intStateIdno, Int64 intCityIdno, Int64 intPPumpIdno, bool bStatus, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                PetrolPumpMaster objPetrolPumpMaster = (from mast in db.PetrolPumpMasters
                                                  where mast.PPump_Idno == intPPumpIdno
                                               select mast).FirstOrDefault();
                if (objPetrolPumpMaster != null)
                {
                    objPetrolPumpMaster.Emp_Idno = empIdno;
                    objPetrolPumpMaster.PPump_Name = strPPumpName;
                    objPetrolPumpMaster.PComp_Idno = PetrolCompanyIdno;
                    objPetrolPumpMaster.PPumpPerson_Name = strPPumpPersonName;
                    objPetrolPumpMaster.PPumpPerson_Designation = strPPersonDesignation;
                    objPetrolPumpMaster.PPump_LadlineCode = strPPumpLadlineCode;
                    objPetrolPumpMaster.PPump_Ladlineno = strPPumpLadlineno;
                    objPetrolPumpMaster.PPump_Mobileno = strPPumpMobileno;
                    objPetrolPumpMaster.PPump_Address1 = strPPumpAddress1;
                    objPetrolPumpMaster.PPump_Address2 = strPPumpAddress2;
                    objPetrolPumpMaster.PPump_State = intStateIdno;
                    objPetrolPumpMaster.PPump_City = intCityIdno;
                    objPetrolPumpMaster.Status = bStatus;
                    
                    objPetrolPumpMaster.Date_Modified = System.DateTime.Now;
                    if (IsExists(strPPumpName, intStateIdno, intCityIdno, intPPumpIdno) == true)
                    {
                        value = -1;
                    }
                    else
                    {
                        db.SaveChanges();
                        value = intPPumpIdno;
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// To check record existence in CityMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="intCityIdno"></param>
        /// <returns></returns>

        public bool IsExists(string strPumpName, Int64 intStateIdno, Int64 intCityIdno, Int64 intPPumpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                PetrolPumpMaster objPetrolPumpMast = null;

                if (intPPumpIdno > 0)///for update
                {
                    objPetrolPumpMast = (from CM in db.PetrolPumpMasters
                                         where CM.PPump_Name == strPumpName && CM.PPump_State == intStateIdno && CM.PPump_City != intCityIdno && CM.PPump_Idno != intPPumpIdno
                                   select CM).FirstOrDefault();
                }
                else /// for insert
                {
                    objPetrolPumpMast = (from nhu in db.PetrolPumpMasters
                                         where nhu.PPump_Name == strPumpName && nhu.PPump_State == intStateIdno && nhu.PPump_City != intCityIdno
                                   select nhu).FirstOrDefault();

                }
                if (objPetrolPumpMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// Delete record from 
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intPPumpIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    PetrolPumpMaster objMast = (from mast in db.PetrolPumpMasters
                                                where mast.PComp_Idno == intPPumpIdno
                                             select mast).FirstOrDefault();
                    if (objMast != null)
                    {
                        db.DeleteObject(objMast);
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

        public Int32 UpdateStatus(int intPPumpIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    PetrolPumpMaster objPetrolPumpMast = (from mast in db.PetrolPumpMasters
                                                    where mast.PPump_Idno == intPPumpIdno
                                                 select mast).FirstOrDefault();
                    if (objPetrolPumpMast != null)
                    {
                        objPetrolPumpMast.Emp_Idno = empIdno;
                        objPetrolPumpMast.Status = bStatus;
                        objPetrolPumpMast.Date_Modified = System.DateTime.Now;
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
