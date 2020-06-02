using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebTransport.DAL
{
    public class CityMastDAL
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

        public Int64 Count()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from CT in db.tblCityMasters
                           join sm in db.tblStateMasters on CT.State_Idno equals sm.State_Idno
                           select new
                           {
                               CityIdno = CT.City_Idno,
                           }).Count();
            }
 
        }


        /// <summary>
        /// Select all records from CityMaster
        /// </summary>
        /// <param name="intCityIdno"></param>
        /// <param name="strCityName"></param>
        /// <returns></returns>

        public IList SelectForSearch(string strCityName, Int32 intStateIdno, Int32 AsLoc)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.tblCityMasters
                           join sm in db.tblStateMasters on CT.State_Idno equals sm.State_Idno
                           select new
                           {
                               CityIdno = CT.City_Idno,
                               CityName = CT.City_Name,
                               Status = CT.Status,
                               Abbreviation = CT.City_Abbr,
                               StateIdno = CT.State_Idno,
                               StateName = sm.State_Name,
                               AsLoc=CT.AsLocation
                           }).ToList();
                if (intStateIdno > 0)
                {
                    lst = (from l in lst where l.StateIdno == intStateIdno select l).ToList();
                }
                if (strCityName != "")
                {
                    lst = (from l in lst where l.CityName.ToLower().Contains(strCityName.ToLower()) select l).ToList();
                }
                if (AsLoc>0)
                {
                    lst = (from l in lst where l.AsLoc == true select l).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from CityMaster by CityIdno
        /// </summary>
        /// <param name="intCityIdno"></param>
        /// <returns></returns>
        public tblCityMaster SelectById(int intCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblCityMasters where mast.City_Idno == intCityIdno select mast).FirstOrDefault();
            }
        }

        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblCityMasters where obj.AsLocation==true orderby obj.City_Name select obj).ToList();
                return lst;
                //   return db.tblCityMasters.Select(cm => new { cm.City_Name, cm.City_Idno }).ToList();
            }
        }

        #endregion

        #region Insert Update Delete Events...
        /// <summary>
        /// Insert records in CityMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertCityMaster(string strCityName, string strCityNameHindi, string strAbb, Int64 intStateIdno, bool bStatus, bool bStatusLocation, Int32 empIdno, string GSTIN_No, string Code_Sap, string SACCode, string add1, string add2)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                tblCityMaster ObjcityMast = new tblCityMaster();
                if (IsExists(strCityName, intStateIdno, 0))
                {
                    value = -1;
                }
                else if (IsCityLimit() == true && bStatusLocation == true)
                {
                    value = -2;
                }
                else
                {
                    ObjcityMast.Emp_Idno = empIdno;
                    ObjcityMast.City_Name = strCityName;
                    ObjcityMast.City_Abbr = strAbb;
                    ObjcityMast.State_Idno = intStateIdno;
                    ObjcityMast.Status = bStatus;
                    ObjcityMast.CityName_Hindi = strCityNameHindi;
                    ObjcityMast.AsLocation = bStatusLocation;
                    ObjcityMast.GSTIN_No = GSTIN_No;
                    ObjcityMast.Address1 = add1;
                    ObjcityMast.Address2 = add2;
                    //ObjcityMast.Comp_Idno = intCompIdno;
                    ObjcityMast.Date_Added = System.DateTime.Now;
                    db.AddTotblCityMasters(ObjcityMast);
                    db.SaveChanges();
                    value = ObjcityMast.City_Idno;
                }
                return value;
            }
        }

        /// <summary>
        /// Update records in CityMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="bStatus"></param>
        /// <param name="intCityIdno"></param>
        /// <param name="intStateIdno"></param>
        /// <returns></returns>

        public Int64 UpdateCityMaster(string strCityName, string strCityNameHindi, string strAbb, Int64 intStateIdno, Int64 intCityIdno, bool bStatus, bool bStatusLocation, Int32 empIdno, string GSTIN_No, string Code_Sap, string SACCode, string add1, string add2)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster objCityMaster = (from mast in db.tblCityMasters
                                               where mast.City_Idno == intCityIdno
                                               select mast).FirstOrDefault();
                if (objCityMaster != null)
                {
                    objCityMaster.City_Name = strCityName;
                    objCityMaster.City_Abbr = strAbb;
                    objCityMaster.Emp_Idno = empIdno;
                    objCityMaster.CityName_Hindi = strCityNameHindi;
                    objCityMaster.State_Idno = intStateIdno;
                    objCityMaster.Status = bStatus;
                    objCityMaster.AsLocation = bStatusLocation;
                    objCityMaster.GSTIN_No = GSTIN_No;
                    objCityMaster.sac_Code = SACCode;
                    objCityMaster.Code_sap = Code_Sap;
                    objCityMaster.Address1 = add1;
                    objCityMaster.Address2 = add2;
                    //objCityMaster.Comp_Idno = intCompIdno;
                    objCityMaster.Date_Modified = System.DateTime.Now;
                    if (IsExists(strCityName, intStateIdno, intCityIdno) == true)
                    {
                        value = -1;
                    }
                    else if (IsCityLimit() == true && bStatusLocation == true)
                    {
                        value = -2;
                    }
                    else
                    {
                        db.SaveChanges();
                        value = intCityIdno;
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

        public bool IsExists(string strCityName, Int64 intStateIdno, Int64 intCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster objCityMast = null;

                if (intCityIdno > 0)///for update
                {
                    objCityMast = (from CM in db.tblCityMasters
                                   where CM.City_Name == strCityName && CM.State_Idno == intStateIdno
                                  && CM.City_Idno != intCityIdno
                                   select CM).FirstOrDefault();
                }
                else /// for insert
                {
                    objCityMast = (from nhu in db.tblCityMasters
                                   where nhu.City_Name == strCityName && nhu.State_Idno == intStateIdno
                                   select nhu).FirstOrDefault();

                }
                if (objCityMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool IsCityLimit()
        {
            Int32 CountLoc = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast objCityMast = db.tblCompMasts.FirstOrDefault();

                if (objCityMast != null)
                {
                    CountLoc = (from CM in db.tblCityMasters
                                where CM.AsLocation == true
                                select CM).Count();

                    if (CountLoc > objCityMast.Tot_Loc)
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
                    return true;
                }
            }
        }
        /// <summary>
        /// Delete record from CityMaster
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intCityIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCityMaster objMast = (from mast in db.tblCityMasters
                                             where mast.City_Idno == intCityIdno
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

        public Int32 UpdateStatus(int intCityIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCityMaster objCityMast = (from mast in db.tblCityMasters
                                                 where mast.City_Idno == intCityIdno
                                                 select mast).FirstOrDefault();
                    if (objCityMast != null)
                    {

                        objCityMast.Status = bStatus;
                        objCityMast.Emp_Idno = empIdno;
                        objCityMast.Date_Modified = System.DateTime.Now;
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

        public IList CheckItemExistInOtherMaster(Int32 CityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblRateMasts
                        where obj.FrmCity_Idno == CityIdno || obj.ToCity_Idno == CityIdno
                        select new
                        {
                            ItemGrp = obj.FrmCity_Idno
                        }
                        ).ToList()
                         .Union
                         (from obj2 in db.AcntMasts
                          where obj2.City_Idno == CityIdno
                          select new
                          {
                              ItemGrp = obj2.City_Idno
                          }
                          ).ToList()
                          .Union
                          (from obj3 in db.tblUserMasts
                           where obj3.City_Idno == CityIdno
                           select new { ItemGrp = obj3.City_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.tblCommmissionMastHeads
                           where obj4.FromCity_Idno == CityIdno
                           select new { ItemGrp = obj4.FromCity_Idno }
                          ).ToList()
                          .Union
                          (from obj5 in db.DistanceMasts
                           where obj5.FrmCity_Idno == CityIdno || obj5.ToCity_Idno == CityIdno
                           select new { ItemGrp = obj5.FrmCity_Idno }
                          ).ToList()
                           .Union
                          (from obj6 in db.tbltollmasters
                           where obj6.city == CityIdno || obj6.Tocity == CityIdno
                           select new { ItemGrp = obj6.city }
                          ).ToList()
                           .Union
                          (from obj7 in db.PetrolPumpMasters
                           where obj7.PPump_City == CityIdno
                           select new { ItemGrp = obj7.PPump_City }
                          ).ToList()
                           .Union
                          (from obj8 in db.tblRcptGoodHeads
                           where obj8.FromCity_Idno == CityIdno || obj8.ToCity_Idno == CityIdno
                           select new { ItemGrp = obj8.FromCity_Idno }
                          ).ToList()
                           .Union
                          (from obj9 in db.tblQuatationHeads
                           where obj9.FromCity_Idno == CityIdno || obj9.ToCity_Idno == CityIdno
                           select new { ItemGrp = obj9.FromCity_Idno }
                          ).ToList()
                           .Union
                          (from obj10 in db.TblGrHeads
                           where obj10.From_City == CityIdno || obj10.To_City == CityIdno
                           select new { ItemGrp = obj10.From_City }
                          ).ToList()
                          .Union
                          (from obj11 in db.StckMasts
                           where obj11.Loc_Idno == CityIdno
                           select new { ItemGrp = obj11.Loc_Idno }
                          ).ToList()
                           .Union
                          (from obj12 in db.tblPBillHeads
                           where obj12.Loc_Idno == CityIdno
                           select new { ItemGrp = obj12.Loc_Idno }
                          ).ToList()
                           .Union
                          (from obj13 in db.MatIssHeads
                           where obj13.Loc_Idno == CityIdno
                           select new { ItemGrp = obj13.Loc_Idno }
                          ).ToList()
                          .Union
                          (from obj14 in db.tblTripHeads
                           where obj14.BaseCity_Idno == CityIdno
                           select new { ItemGrp = obj14.BaseCity_Idno }
                          ).ToList()
                          .Union
                          (from obj15 in db.tblAdvGrOrders
                           where obj15.Loc_Idno == CityIdno || obj15.Loc_To == CityIdno || obj15.Loc_DelvPlace == CityIdno
                           select new
                           {
                               ItemGrp = obj15.Loc_Idno

                           }).ToList();
                         

            
            }
        }
    }
}