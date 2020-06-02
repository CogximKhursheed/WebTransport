using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class ItemMasterDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from ItemMast
        /// </summary>
        /// <returns></returns>
        /// 
        public IList SelectGroupTypeForItemGrp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GType in db.ItemTypes
                           orderby GType.Item_Name ascending
                           //where GType.ItemType_Idno != 1 && GType.ItemType_Idno != 11
                           select new
                           {
                               Item_Name = GType.Item_Name,
                               Item_Type = GType.ItemType_Idno
                           }).ToList();
                return lst;
            }
        }
        public IList SelectGroupType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from LM in db.tblIGrpMasts
                           orderby LM.IGrp_Name
                           where LM.Status == true
                           select new
                           {
                               LM.IGrp_Name,
                               LM.IGrp_Idno

                           }).ToList();
                return lst;
            }
        }
        public IList SelectUnitType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from LM in db.UOMMasts
                           orderby LM.UOM_Name
                           where LM.Status == true
                           select new
                               {
                                   LM.UOM_Name,
                                   LM.UOM_Idno
                               }
                                ).ToList();
                return lst;
            }
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.ItemMasts
                        orderby mast.Item_Name
                        select mast).ToList();
            }
        }

        public Int64 Select_ItemMastCount()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.ItemMasts
                           join ig in db.tblIGrpMasts on mast.IGrp_Idno equals ig.IGrp_Idno
                           join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno
                           //      where mast.Item_Idno == intItemTypeIdno
                           select new
                           {
                               mast
                           }).Count();

                return lst;
            }
        }

        public IList SelectForSearch(int intItemTypeIdno, string strItemName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.ItemMasts
                           join ig in db.tblIGrpMasts on mast.IGrp_Idno equals ig.IGrp_Idno
                           join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno
                           //      where mast.Item_Idno == intItemTypeIdno
                           select new
                           {
                               ItemIdno = mast.Item_Idno,
                               ItemName = mast.Item_Name,
                               IgrpName = ig.IGrp_Name,
                               UOMName = u.UOM_Name,
                               ItemDesc = mast.Item_Desc,
                               Status = mast.Status,
                               IGrp_Idno = ig.IGrp_Idno
                           }).ToList();
                if (strItemName != "")
                {
                    lst = (from I in lst where I.ItemName.ToLower().Contains(strItemName.ToLower()) select I).ToList();
                }
                if (intItemTypeIdno > 0)
                {
                    lst = (from I in lst where I.IGrp_Idno == intItemTypeIdno select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from ItemMast by ItemIdno
        /// </summary>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public ItemMast SelectById(int intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.ItemMasts
                        where mast.Item_Idno == intItemIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To check record existence in ItemMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strItemName, Int64 intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ItemMast objItemMast = new ItemMast();
                if (intItemIdno <= 0)
                {
                    objItemMast = (from mast in db.ItemMasts
                                   where mast.Item_Name == strItemName
                                   select mast).FirstOrDefault();
                }
                else if (intItemIdno > 0)
                {
                    objItemMast = (from mast in db.ItemMasts
                                   where mast.Item_Name == strItemName
                                         && mast.Item_Idno != intItemIdno
                                   select mast).FirstOrDefault();
                }
                if (objItemMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public ItemDetailForGrid GetItemByItemId(Int64 ItemId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from item in db.ItemMasts
                           join uom in db.UOMMasts on item.Unit_Idno equals uom.UOM_Idno
                           where item.Item_Idno == ItemId
                           select new { item.Item_Name, item.Item_Idno, Uom = uom.UOM_Name, item.Unit_Idno }
                             ).FirstOrDefault();
                ItemDetailForGrid idg = new ItemDetailForGrid();
                if (lst != null)
                {
                    idg.ItemId = lst.Item_Idno;
                    idg.ItemName = lst.Item_Name;
                    idg.Uom = lst.Uom;
                    idg.UomId = Convert.ToInt64(lst.Unit_Idno);
                }
                return idg;
            }
        }
        public IList GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from item in db.ItemMasts

                           select new { ItemName = item.Item_Name, ItemId = item.Item_Idno }
                             ).ToList();
                return lst;
            }
        }

        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in ItemMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strItemDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strItemName, string strItemNameHindi, string strItemDesc, Int64 GrpType, Int64 ItemUnit, bool bStatus, Int32 empIdno, string HSNSAC)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    ItemMast objItemMast = new ItemMast();
                    objItemMast.Item_Name = strItemName;
                    objItemMast.ItemNamel_Hindi = strItemNameHindi;
                    objItemMast.Item_Desc = strItemDesc;
                    objItemMast.Status = bStatus;
                    objItemMast.IGrp_Idno = GrpType;
                    objItemMast.Unit_Idno = ItemUnit;
                    objItemMast.Comp_IdNo = intCompIdno;
                    objItemMast.Emp_Idno = empIdno;
                    objItemMast.HSNSAC_No = HSNSAC;
                    objItemMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strItemName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.ItemMasts.AddObject(objItemMast);
                        db.SaveChanges();
                        intValue = objItemMast.Item_Idno;
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
        /// Update records in ItemMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public Int64 Update(string strItemName, string strItemNameHindi, string strItemDesc, Int64 GrpType, Int64 ItemUnit, bool bStatus, Int64 intItemIdno, Int32 empIdno, string HSNSAC)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    ItemMast objItemMast = (from mast in db.ItemMasts
                                            where mast.Item_Idno == intItemIdno
                                            select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.Item_Name = strItemName;
                        objItemMast.ItemNamel_Hindi = strItemNameHindi;
                        objItemMast.Item_Desc = strItemDesc;
                        objItemMast.Status = bStatus;
                        objItemMast.IGrp_Idno = GrpType;
                        objItemMast.Unit_Idno = ItemUnit;
                        objItemMast.Emp_Idno = empIdno;
                        objItemMast.HSNSAC_No = HSNSAC;
                        objItemMast.Date_modified = System.DateTime.Now;
                        if (IsExists(strItemName, intItemIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intItemIdno;
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
        /// Delete record from ItemMast
        /// </summary>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public int Delete(int intItemIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    ItemMast objItemMast = (from mast in db.ItemMasts
                                            where mast.Item_Idno == intItemIdno
                                            select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        db.ItemMasts.DeleteObject(objItemMast);
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

        public Int32 UpdateStatus(int intItemIdno, bool Status, Int32 EmpIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    ItemMast objItemMast = (from mast in db.ItemMasts
                                            where mast.Item_Idno == intItemIdno
                                            select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.Status = Status;
                        objItemMast.Emp_Idno = EmpIdno;
                        objItemMast.Date_modified = System.DateTime.Now;
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
        
        public IList CheckItemExistInOtherMaster(Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblRateMasts
                        where obj.Item_Idno == ItemIdno
                        select new
                        {
                            ItemGrp = obj.Item_Idno
                        }
                        ).ToList()
                         .Union
                         (from obj2 in db.tblRcptGoodDetls
                          where obj2.Item_Idno == ItemIdno
                          select new
                          {
                              ItemGrp = obj2.Item_Idno
                          }
                          ).ToList()
                          .Union
                          (from obj3 in db.tblQuatationDetls
                           where obj3.Item_Idno == ItemIdno
                           select new { ItemGrp = obj3.Item_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.TblGrDetls
                           where obj4.Item_Idno == ItemIdno
                           select new { ItemGrp = obj4.Item_Idno }
                          ).ToList()
                          .Union
                          (from obj5 in db.tblCommmissionMastHeads
                           where obj5.Item_Idno == ItemIdno
                           select new { ItemGrp = obj5.Item_Idno }
                          ).ToList()
                         ;
            }
        } 
    }

    public class ItemDetailForGrid
    {
        public Int64 ItemId { get; set; }
        public Int64 UomId { get; set; }
        public string Uom { get; set; }
        public string ItemName { get; set; }
    }
}