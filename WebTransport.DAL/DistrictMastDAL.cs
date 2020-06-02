using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebTransport.DAL
{
    public class DistrictMastDAL
    {

        #region Select Events...
        /// <summary>
        /// Select all records from DistrictMaster
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
                return (from CT in db.tblDistrictMasters
                        join sm in db.tblStateMasters on CT.State_Idno equals sm.State_Idno
                        select new
                        {
                            DistrictIdno = CT.District_Idno,
                        }).Count();
            }

        }


        /// <summary>
        /// Select all records from DistrictMaster
        /// </summary>
        /// <param name="intDistrictIdno"></param>
        /// <param name="strCityName"></param>
        /// <returns></returns>

        public IList SelectForSearch(string strDistrictName, Int32 intStateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.tblDistrictMasters
                           join sm in db.tblStateMasters on CT.State_Idno equals sm.State_Idno
                           select new
                           {
                               DistrictIdno = CT.District_Idno,
                               DistrictName = CT.District_Name,
                               Status = CT.Status,
                               Abbreviation = CT.District_Abbr,
                               StateIdno = CT.State_Idno,
                               StateName = sm.State_Name,
                               
                           }).ToList();
                if (intStateIdno > 0)
                {
                    lst = (from l in lst where l.StateIdno == intStateIdno select l).ToList();
                }
                if (strDistrictName != "")
                {
                    lst = (from l in lst where l.DistrictName.ToLower().Contains(strDistrictName.ToLower()) select l).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from CityMaster by DistrictIdno
        /// </summary>
        /// <param name="intDistrictIdno"></param>
        /// <returns></returns>
        public tblDistrictMaster SelectById(int intDistrictIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblDistrictMasters where mast.District_Idno == intDistrictIdno select mast).FirstOrDefault();
            }
        }

        public IList SelectDistrictCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblDistrictMasters where obj.Status == true orderby obj.District_Idno select obj).ToList();
                return lst;
                //   return db.tblCityMasters.Select(cm => new { cm.City_Name, cm.City_Idno }).ToList();
            }
        }

        #endregion

        #region Insert Update Delete Events...
        public Int64 Insert(string strDistrictName, string strDistrictNameHindi, string strAbb, Int64 intStateIdno, bool bStatus, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                tblDistrictMaster ObjMast = new tblDistrictMaster();
                if (IsExists(strDistrictName, intStateIdno, 0))
                {
                    value = -1;
                }
                else
                {
                    ObjMast.District_Name = strDistrictName;
                    ObjMast.District_Hindi = strDistrictNameHindi;
                    ObjMast.State_Idno = intStateIdno;
                    ObjMast.District_Abbr = strAbb;
                    ObjMast.Status = bStatus;
                    ObjMast.Emp_Idno = empIdno;
                    ObjMast.Date_Added = System.DateTime.Now;
                    db.AddTotblDistrictMasters(ObjMast);
                    db.SaveChanges();
                    value = ObjMast.District_Idno;
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
        /// <param name="intDistrictIdno"></param>
        /// <param name="intStateIdno"></param>
        /// <returns></returns>

        public Int64 Update(string strDistrictName, string strDistrictNameHindi, string strAbb, Int64 intStateIdno, Int64 intDistrictIdno, bool bStatus, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblDistrictMaster objMast = (from mast in db.tblDistrictMasters
                                               where mast.District_Idno == intDistrictIdno
                                               select mast).FirstOrDefault();
                if (objMast != null)
                {
                    objMast.District_Name = strDistrictName;
                    objMast.District_Abbr = strAbb;
                    objMast.Emp_Idno = empIdno;
                    objMast.District_Hindi = strDistrictNameHindi;
                    objMast.State_Idno = intStateIdno;
                    objMast.Status = bStatus;
                    objMast.Date_Modified = System.DateTime.Now;
                    if (IsExists(strDistrictName, intStateIdno, intDistrictIdno) == true)
                    {
                        value = -1;
                    }
                    
                    else
                    {
                        db.SaveChanges();
                        value = intDistrictIdno;
                    }
                }
            }
            return value;
        }


        public bool IsExists(string strDistrictName, Int64 intStateIdno, Int64 intDistrictIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblDistrictMaster ObjDistrictMast = null;

                if (intDistrictIdno > 0)///for update
                {
                    ObjDistrictMast = (from CM in db.tblDistrictMasters
                                       where CM.District_Name == strDistrictName && CM.State_Idno == intStateIdno
                                      && CM.District_Idno != intDistrictIdno
                                       select CM).FirstOrDefault();
                }
                else /// for insert
                {
                    ObjDistrictMast = (from nhu in db.tblDistrictMasters
                                   where nhu.District_Name == strDistrictName && nhu.State_Idno == intStateIdno
                                   select nhu).FirstOrDefault();

                }
                if (ObjDistrictMast != null)
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

        /// <summary>
        /// Delete record from CityMaster
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intdistrictIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDistrictMaster objMast = (from mast in db.tblDistrictMasters
                                             where mast.District_Idno == intdistrictIdno
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

        public Int32 UpdateStatus(int intDistrictIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblDistrictMaster objDistrictMast = (from mast in db.tblDistrictMasters
                                                     where mast.District_Idno == intDistrictIdno
                                                 select mast).FirstOrDefault();
                    if (objDistrictMast != null)
                    {

                        objDistrictMast.Status = bStatus;
                        objDistrictMast.Emp_Idno = empIdno;
                        objDistrictMast.Date_Modified = System.DateTime.Now;
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
        public IList CheckItemExistInOtherMaster(Int32 DistrictIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblRateMasts
                        where obj.FrmCity_Idno == DistrictIdno || obj.ToCity_Idno == DistrictIdno
                        select new
                        {
                            ItemGrp = obj.FrmCity_Idno
                        }
                        ).ToList()
                         .Union
                         (from obj2 in db.AcntMasts
                          where obj2.City_Idno == DistrictIdno
                          select new
                          {
                              ItemGrp = obj2.City_Idno
                          }
                          ).ToList()
                          .Union
                          (from obj3 in db.tblUserMasts
                           where obj3.City_Idno == DistrictIdno
                           select new { ItemGrp = obj3.City_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.tblCommmissionMastHeads
                           where obj4.FromCity_Idno == DistrictIdno
                           select new { ItemGrp = obj4.FromCity_Idno }
                          ).ToList()
                          .Union
                          (from obj5 in db.DistanceMasts
                           where obj5.FrmCity_Idno == DistrictIdno || obj5.ToCity_Idno == DistrictIdno
                           select new { ItemGrp = obj5.FrmCity_Idno }
                          ).ToList()
                           .Union
                          (from obj6 in db.tbltollmasters
                           where obj6.city == DistrictIdno || obj6.Tocity == DistrictIdno
                           select new { ItemGrp = obj6.city }
                          ).ToList()
                           .Union
                          (from obj7 in db.PetrolPumpMasters
                           where obj7.PPump_City == DistrictIdno
                           select new { ItemGrp = obj7.PPump_City }
                          ).ToList()
                           .Union
                          (from obj8 in db.tblRcptGoodHeads
                           where obj8.FromCity_Idno == DistrictIdno || obj8.ToCity_Idno == DistrictIdno
                           select new { ItemGrp = obj8.FromCity_Idno }
                          ).ToList()
                           .Union
                          (from obj9 in db.tblQuatationHeads
                           where obj9.FromCity_Idno == DistrictIdno || obj9.ToCity_Idno == DistrictIdno
                           select new { ItemGrp = obj9.FromCity_Idno }
                          ).ToList()
                           .Union
                          (from obj10 in db.TblGrHeads
                           where obj10.From_City == DistrictIdno || obj10.To_City == DistrictIdno
                           select new { ItemGrp = obj10.From_City }
                          ).ToList()
                          .Union
                          (from obj11 in db.StckMasts
                           where obj11.Loc_Idno == DistrictIdno
                           select new { ItemGrp = obj11.Loc_Idno }
                          ).ToList()
                           .Union
                          (from obj12 in db.tblPBillHeads
                           where obj12.Loc_Idno == DistrictIdno
                           select new { ItemGrp = obj12.Loc_Idno }
                          ).ToList()
                           .Union
                          (from obj13 in db.MatIssHeads
                           where obj13.Loc_Idno == DistrictIdno
                           select new { ItemGrp = obj13.Loc_Idno }
                          ).ToList()
                          .Union
                          (from obj14 in db.tblTripHeads
                           where obj14.BaseCity_Idno == DistrictIdno
                           select new { ItemGrp = obj14.BaseCity_Idno }
                          ).ToList()
                          .Union
                          (from obj15 in db.tblAdvGrOrders
                           where obj15.Loc_Idno == DistrictIdno || obj15.Loc_To == DistrictIdno || obj15.Loc_DelvPlace == DistrictIdno
                           select new
                           {
                               ItemGrp = obj15.Loc_Idno

                           }).ToList();



            }
        }
    }
}
