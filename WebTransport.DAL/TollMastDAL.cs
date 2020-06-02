using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class TollMastDAL
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
        public IList<LorryType> SelectLorryType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                IList<LorryType> Obj = null;
                Obj = db.LorryTypes.Select(R => R).ToList();
                return Obj;
            }
        }
        /// <summary>
        /// Select all records from 
        /// </summary>
        /// <param name="intCityIdno"></param>
        /// <param name="strtolltaxname"></param>
        /// <returns></returns>

        public IList SelectForSearch(Int32 cityid, Int32 ToCity, string strtollname, Int32 Lorry)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.tbltollmasters
                           join sm in db.tblCityMasters on CT.city equals sm.City_Idno
                           join sm2 in db.tblCityMasters on CT.Tocity equals sm2.City_Idno into gj
                           from full in gj.DefaultIfEmpty()
                           join LT in db.LorryTypes on CT.LorryType_Idno equals LT.Id into Lg from LTG in Lg.DefaultIfEmpty()
                           select new
                           {
                               TollTaxid = CT.Toll_id,
                               Cityid = CT.city,
                               Status = CT.Status,
                               CityName = sm.City_Name,
                               ToCityName = full.City_Name,
                               ToCityIdNo = full.City_Idno,
                               TollTaxName = CT.Tolltax_name,
                               Lorry_Type=LTG.Lorry_Type,
                               LorryType_Idno= CT.LorryType_Idno,
                               Ammount = CT.Amount
                           }).ToList();


                if (cityid > 0)
                {
                    lst = (from l in lst where l.Cityid == cityid select l).ToList();
                }
                if (ToCity > 0)
                {
                    lst = (from l in lst where l.ToCityIdNo == ToCity select l).ToList();
                }
                if (strtollname != "")
                {

                    lst = (from I in lst where I.TollTaxName.ToLower().Contains(strtollname.ToLower()) select I).ToList();
                }
                if (Lorry > 0)
                {
                    lst = (from I in lst where I.LorryType_Idno == Lorry select I).ToList();
                }

                return lst;
            }
        }

        /// <summary>
        /// Select one record from CityMaster by CityIdno
        /// </summary>
        /// <param name="intCityIdno"></param>
        /// <returns></returns>
        public tbltollmaster SelectById(int intCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tbltollmasters where mast.Toll_id == intCityIdno select mast).FirstOrDefault();
            }
        }

        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblCityMasters orderby obj.City_Name select obj).ToList();
                return lst;
                //   return db.tblCityMasters.Select(cm => new { cm.City_Name, cm.City_Idno }).ToList();
            }
        }

        #endregion

        #region Insert Update Delete Events...
        /// <summary>
        /// Insert records in TllMast
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertTollMaster(string strTollTaxName, Int64 intCityIdno, Int64 intToCityIdno, float famount, bool bstatus, Int32 LorryTypeIdno, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                tbltollmaster ObjTollMast = new tbltollmaster();
                if (IsExists(strTollTaxName, 0, intCityIdno, intToCityIdno, LorryTypeIdno))
                {
                    value = -1;
                }
                else
                {
                    ObjTollMast.Emp_Idno = empIdno;
                    ObjTollMast.Tolltax_name = strTollTaxName;
                    ObjTollMast.Amount = famount;
                    ObjTollMast.city = intCityIdno;
                    ObjTollMast.Tocity = intToCityIdno;
                    ObjTollMast.Date_Added = System.DateTime.Now;
                    ObjTollMast.Status = bstatus;
                    ObjTollMast.LorryType_Idno = LorryTypeIdno; 
                    db.AddTotbltollmasters(ObjTollMast);
                    db.SaveChanges();
                    value = ObjTollMast.Toll_id;
                }
                return value;
            }
        }
         
        public Int64 UpdateTollMaster(string strTollTaxName, Int64 intTollTaxIdno, Int64 intcity, Int64 intToCityIdno, float famount, bool bstatus, Int32 LorryTypeIdno, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tbltollmaster objTollMaster = (from mast in db.tbltollmasters
                                               where mast.Toll_id == intTollTaxIdno
                                               select mast).FirstOrDefault();
                if (objTollMaster != null)
                {
                    objTollMaster.Emp_Idno = empIdno;
                    objTollMaster.Tolltax_name = strTollTaxName;
                    objTollMaster.Amount = famount;
                    objTollMaster.city = intcity;
                    objTollMaster.Tocity = intToCityIdno;
                    objTollMaster.Status = bstatus;
                    objTollMaster.LorryType_Idno = LorryTypeIdno;
                    if (IsExists(strTollTaxName, intTollTaxIdno,intcity,intToCityIdno,LorryTypeIdno) == true)
                    {
                        value = -1;
                    }
                    else
                    {
                        db.SaveChanges();
                        value = intTollTaxIdno;
                    }
                }
            }
            return value;
        }
        public List<tblCityMaster> BindCityAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objCityMasterList = new List<tblCityMaster>();

                objCityMasterList = (from obj in db.tblCityMasters
                                     where obj.Status == true
                                     orderby obj.City_Name
                                     select obj).ToList();

                return objCityMasterList;
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

        /// <summary>
        /// To check record existence in CityMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="intCityIdno"></param>
        /// <returns></returns>
        // public bool IsExists(string strCityName, Int64 intStateIdno, Int64 intCityIdno)
        public bool IsExists(string strTollTaxName, Int64 intTollTaxIdno, Int64 FromCityIdno, Int64 ToCityIdno, Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tbltollmaster objTollMast = null;

                if (intTollTaxIdno > 0)///for update
                {
                    objTollMast = (from CM in db.tbltollmasters
                                   where CM.Tolltax_name == strTollTaxName && CM.Toll_id != intTollTaxIdno && CM.city == FromCityIdno && CM.Tocity == ToCityIdno && CM.LorryType_Idno == LorryIdno
                                   select CM).FirstOrDefault();
                }
                else /// for insert
                {
                    objTollMast = (from nhu in db.tbltollmasters
                                   where nhu.Tolltax_name == strTollTaxName && nhu.city == FromCityIdno && nhu.Tocity == ToCityIdno && nhu.LorryType_Idno == LorryIdno
                                   select nhu).FirstOrDefault();

                }
                if (objTollMast != null)
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
        /// Delete record from CityMaster
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intToll_id)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tbltollmaster objMast = (from mast in db.tbltollmasters
                                             where mast.Toll_id == intToll_id
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
                    tbltollmaster objTollMast = (from mast in db.tbltollmasters
                                                 where mast.Toll_id == intCityIdno
                                                 select mast).FirstOrDefault();
                    if (objTollMast != null)
                    {
                        objTollMast.Emp_Idno = empIdno;
                        objTollMast.Status = bStatus;
                        objTollMast.Date_Modified = System.DateTime.Now;
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

        public IList CheckItemExistInOtherMaster(Int32 TollIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblTripTollDetls
                        where obj.Toll_Idno == TollIdno
                        select new
                        {
                            ItemGrp = obj.Toll_Idno
                        }
                        ).ToList();
            }
        }
        #endregion
    }
}