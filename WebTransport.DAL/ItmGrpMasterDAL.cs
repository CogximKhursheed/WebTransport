using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class ItmGrpMasterDAL
    {
        #region Select Events...
        public List<ItemType> SelectGroupTypeForItemGrp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemType> lst = null;
                lst = (from LM in db.ItemTypes orderby LM.Item_Name ascending select LM).ToList();
                return lst;
            }
        }

        /// <summary>
        /// Fill ddl Group Type
        /// </summary>
        /// <returns></returns>
        //public IList SelectGroupTypeForItemGrp()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from GType in db.ItemTypes
        //                   orderby GType.Item_Name ascending
        //                   //where GType.ItemType_Idno != 1 && GType.ItemType_Idno != 11
        //                   select new
        //                   {
        //                       Item_Name = GType.Item_Name,
        //                       Item_Type = GType.ItemType_Idno
        //                   }).ToList();
        //        return lst;
        //    }
        //}

        public IList SelectGroupTypeForVhclGrp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GType in db.ItemTypes
                           orderby GType.Item_Name ascending
                           where GType.ItemType_Idno == 1 && GType.ItemType_Idno == 11
                           select new
                           {
                               Item_Name = GType.Item_Name,
                               Item_Type = GType.ItemType_Idno
                           }).ToList();
                return lst;
            }
        }

        /// <summary>
        /// Fill Grid
        /// </summary>
        /// <returns></returns>
        public IList SelectAll(string strGrpName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblIGrpMasts
                           // join loc in db.ItemTypes on mast.IGrp_Type equals loc.ItemType_Idno
                           orderby mast.IGrp_Name ascending
                           select new
                           {
                               // Item_Name = loc.Item_Name,
                               IGrp_Name = mast.IGrp_Name,
                               IGrp_Idno = mast.IGrp_Idno,
                               IGrp_Type = mast.IGrp_Type,
                               Status = mast.Status
                           }).ToList();
                //if (intIGrpIdno > 0)
                //{
                //    lst = (from I in lst where I.IGrp_Type == intIGrpIdno select I).ToList();
                //}
                if (strGrpName != "")
                {
                    lst = (from I in lst where I.IGrp_Name.ToLower().Contains(strGrpName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select From Id
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public tblIGrpMast SelectById(int intIGropIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblIGrpMasts
                        where mast.IGrp_Idno == intIGropIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To Check data Already Exist or Not
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strIGroupName, int intIGropIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblIGrpMast objIGrpMast = new tblIGrpMast();
                if (intIGropIdno <= 0)
                {
                    objIGrpMast = (from mast in db.tblIGrpMasts
                                   where mast.IGrp_Name == strIGroupName
                                   select mast).FirstOrDefault();
                }
                else if (intIGropIdno > 0)
                {
                    objIGrpMast = (from mast in db.tblIGrpMasts
                                   where mast.IGrp_Name == strIGroupName
                                         && mast.IGrp_Idno != intIGropIdno
                                   select mast).FirstOrDefault();
                }
                if (objIGrpMast != null)
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
        /// Insert records in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strIGroupName, int intIGrpType, bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblIGrpMast objIGrpMast = new tblIGrpMast();
                    objIGrpMast.IGrp_Name = strIGroupName;
                    objIGrpMast.IGrp_Type = intIGrpType;
                    objIGrpMast.Status = bStatus;
                    objIGrpMast.Emp_Idno = empIdno;
                    objIGrpMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strIGroupName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblIGrpMasts.AddObject(objIGrpMast);
                        db.SaveChanges();
                        intValue = objIGrpMast.IGrp_Idno;
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
        /// To Update Record in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Update(string strIGroupName, int intIGrpType, bool bStatus, int intIGropIdno, Int32 empIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblIGrpMast objIGrpMast = (from mast in db.tblIGrpMasts
                                               where mast.IGrp_Idno == intIGropIdno
                                               select mast).FirstOrDefault();
                    if (objIGrpMast != null)
                    {
                        objIGrpMast.IGrp_Name = strIGroupName;
                        objIGrpMast.IGrp_Type = intIGrpType;
                        objIGrpMast.Status = bStatus;
                        objIGrpMast.Emp_Idno = empIdno;
                        objIGrpMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strIGroupName, intIGropIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intIGropIdno;
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
        ///  Delete Record from tblIGrpMast
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Delete(int intIGropIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblIGrpMast objIGrpMast = (from mast in db.tblIGrpMasts
                                               where mast.IGrp_Idno == intIGropIdno
                                               select mast).FirstOrDefault();
                    if (objIGrpMast != null)
                    {
                        db.tblIGrpMasts.DeleteObject(objIGrpMast);
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

        public Int32 UpdateStatus(int intIGrpIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblIGrpMast objIGrpMast = (from mast in db.tblIGrpMasts
                                               where mast.IGrp_Idno == intIGrpIdno
                                               select mast).FirstOrDefault();
                    if (objIGrpMast != null)
                    {

                        objIGrpMast.Status = Status;
                        objIGrpMast.Emp_Idno = empIdno;
                        objIGrpMast.Date_Modified = System.DateTime.Now;
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


        public IList CheckItemGrpExistInItemMaster(Int32 IGrpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.ItemMasts
                        where obj.IGrp_Idno == IGrpIdno
                        select new
                        {
                            ItemGrp = obj.IGrp_Idno
                        }
                        ).ToList()
                        .Union
                        (from obj1 in db.tblItemMastPurs
                         where obj1.IGrp_Idno == IGrpIdno
                         select new
                         {
                             ItemGrp = obj1.IGrp_Idno
                         }
                         ).ToList();
            }
        }

        public IList BindItemType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.tblItemTypeMasts
                           select new
                           {
                               ItemName = N.ItemType_Name,
                               ItemType = N.ItemTpye_Idno
                           }).ToList();
                return Lst;
            }
        }
    }
}