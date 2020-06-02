using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace WebTransport.DAL
{
    public class PetrolCompanyMasterDAL
    {

        #region Select Events...
        /// <summary>
        /// Select all records from StateMaster
        /// </summary>
        /// <returns></returns>
        /// 
        public tblPCompanyMaster SelectById(int intPCompIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblPCompanyMasters where mast.PComp_Idno == intPCompIdno select mast).FirstOrDefault();
            }
        }

        public IList SelectForSearch(string strPCompName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.tblPCompanyMasters
                           //join sm in db.tblStateMasters on CT.PComp_Idno equals sm.intPCompIdno
                           select new
                           {
                               PCompIdno = CT.PComp_Idno,
                               PCompName = CT.PComp_Name,
                               Status = CT.Status,
                               //StateName = sm.State_Name
                           }).ToList();
                if (strPCompName != "")
                {
                    lst = (from l in lst where l.PCompName.ToLower().Contains(strPCompName.ToLower()) select l).ToList();
                }
                return lst;
            }
        }
        public Int64 selectcount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                Int64 sc = (from co in db.tblPCompanyMasters select co).Count();
                return sc;
            }

        }
        #endregion
        public IList CheckItemExistInOtherMaster(Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.PetrolPumpMasters
                        where obj.PComp_Idno == ItemIdno
                        select new
                        {
                            ItemGrp = obj.PComp_Idno
                        }
                        ).ToList();
                //.Union
                //(from obj2 in db.tblRcptGoodDetls
                // where obj2.Item_Idno == ItemIdno
                // select new
                // {
                //     ItemGrp = obj2.Item_Idno
                // }
                // ).ToList()
                // .Union
                // (from obj3 in db.tblQuatationDetls
                //  where obj3.Item_Idno == ItemIdno
                //  select new { ItemGrp = obj3.Item_Idno }
                // ).ToList()
                //  .Union
                // (from obj4 in db.TblGrDetls
                //  where obj4.Item_Idno == ItemIdno
                //  select new { ItemGrp = obj4.Item_Idno }
                // ).ToList()
                // .Union
                // (from obj5 in db.tblCommmissionMastHeads
                //  where obj5.Item_Idno == ItemIdno
                //  select new { ItemGrp = obj5.Item_Idno }
                // ).ToList()
                //;
            }
        }

        #region Insert Update Delete Events...
        /// <summary>
        /// Insert records in CityMaster
        /// </summary>
        /// <param name="strCityName"></param>
        /// <param name="strAbb"></param>
        /// <param name="intStateIdno"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertPCompanyMaster(string strPCompName, bool bStatus, Int32 empIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value;
                Int32 intCompIdno = 1;
                tblPCompanyMaster ObjtblPCompanyMaster = new tblPCompanyMaster();
                if (IsExists(strPCompName, 0))
                {
                    value = -1;
                }
                else
                {
                    ObjtblPCompanyMaster.Emp_Idno = empIdno;
                    ObjtblPCompanyMaster.PComp_Name = strPCompName;
                    ObjtblPCompanyMaster.Status = bStatus;
                    ObjtblPCompanyMaster.Date_Added = System.DateTime.Now;
                    db.AddTotblPCompanyMasters(ObjtblPCompanyMaster);
                    db.SaveChanges();
                    value = ObjtblPCompanyMaster.PComp_Idno;
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

        public Int64 UpdatePCompanyMaster(string strPCompName, Int64 intPCompIdno, bool bStatus, Int32 empIdno)
        {
            Int64 value = 0;
            Int32 intCompIdno = 1;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblPCompanyMaster objPCompanyMaster = (from mast in db.tblPCompanyMasters
                                                       where mast.PComp_Idno == intPCompIdno
                                                       select mast).FirstOrDefault();
                if (objPCompanyMaster != null)
                {
                    objPCompanyMaster.Emp_Idno = empIdno;
                    objPCompanyMaster.PComp_Name = strPCompName;
                    objPCompanyMaster.Status = bStatus;
                    objPCompanyMaster.Date_Modified = System.DateTime.Now;
                    if (IsExists(strPCompName, intPCompIdno) == true)
                    {
                        value = -1;
                    }
                    else
                    {
                        db.SaveChanges();
                        value = intPCompIdno;
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

        public bool IsExists(string strPCompName, Int64 intPCompIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblPCompanyMaster objPCompanyMaster = null;

                if (intPCompIdno > 0)///for update
                {
                    objPCompanyMaster = (from CM in db.tblPCompanyMasters
                                         where CM.PComp_Name == strPCompName && CM.PComp_Idno != intPCompIdno
                                         select CM).FirstOrDefault();
                }
                else /// for insert
                {
                    objPCompanyMaster = (from nhu in db.tblPCompanyMasters
                                         where nhu.PComp_Name == strPCompName
                                         select nhu).FirstOrDefault();

                }
                if (objPCompanyMaster != null)
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
        /// Delete record from PetrolCompany
        /// </summary>
        /// <param name="intColrIdno"></param>
        /// <returns></returns>

        public int Delete(int intPCompIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPCompanyMaster objMast = (from mast in db.tblPCompanyMasters
                                                 where mast.PComp_Idno == intPCompIdno
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

        public Int32 UpdateStatus(int intPCompIdno, bool bStatus, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPCompanyMaster objPCompanyMaster = (from mast in db.tblPCompanyMasters
                                                           where mast.PComp_Idno == intPCompIdno
                                                           select mast).FirstOrDefault();
                    if (objPCompanyMaster != null)
                    {
                        objPCompanyMaster.Emp_Idno = empIdno;
                        objPCompanyMaster.Status = bStatus;
                        objPCompanyMaster.Date_Modified = System.DateTime.Now;
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
