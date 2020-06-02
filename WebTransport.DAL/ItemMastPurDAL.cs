using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class ItemMastPurDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from ItemMastPur
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
        public List<tblIGrpMast> SelectGroupType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblIGrpMast> lst = null;
                lst = (from LM in db.tblIGrpMasts orderby LM.IGrp_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<tblIGrpMast> SelectActiveGroupType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblIGrpMast> lst = null;
                lst = (from LM in db.tblIGrpMasts where LM.Status == true orderby LM.IGrp_Name ascending select LM).ToList();
                return lst;
            }
        }
        public List<UOMMast> SelectUnitType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<UOMMast> lst = null;
                lst = (from LM in db.UOMMasts where LM.Status==true orderby LM.UOM_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<UOMMast> SelectActiveUnitType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<UOMMast> lst = null;
                lst = (from LM in db.UOMMasts where LM.Status == true orderby LM.UOM_Name ascending select LM).ToList();
                return lst;
            }
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblItemMastPurs
                        orderby mast.Item_Name
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <param name="strItemName"></param>
        /// <returns></returns>
        public IList SelectForSearch(int intItemTypeIdno, string strItemName, Int64 itemtype)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblItemMastPurs
                           join ig in db.tblIGrpMasts on mast.IGrp_Idno equals ig.IGrp_Idno
                           join IM in db.tblItemTypeMasts on mast.ItemType equals IM.ItemTpye_Idno
                           join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno

                           select new
                           {
                               ItemIdno = mast.Item_Idno,
                               ItemName = mast.Item_Name,
                               IgrpName = ig.IGrp_Name,
                               UOMName = u.UOM_Name,
                               ItemDesc = mast.Item_Desc,
                               PurRate = mast.Pur_Rate,
                               VATTaxPer = mast.VATTax_Per,
                               CSTTaxPer = mast.CSTTax_Per,
                               Status = mast.Status,
                               IGrp_Idno = ig.IGrp_Idno,
                               Item_TypeID = mast.ItemType,
                               TypeName = IM.ItemType_Name,
                               SGST= mast.SGST,
                               CGST= mast.CGST,
                               IGST = mast.IGST 
                           }).ToList();

                if (strItemName != "")
                {
                    lst = (from I in lst where I.ItemName.ToLower().Contains(strItemName.ToLower()) select I).ToList();
                }
                if (intItemTypeIdno > 0)
                {
                    lst = (from I in lst where I.IGrp_Idno == intItemTypeIdno select I).ToList();
                }
                if (itemtype > 0)
                {
                    lst = (from I in lst where I.Item_TypeID == itemtype select I).ToList();
                }
                return lst;
            }
        }


        /// <summary>
        /// Count All Record
        /// </summary>
      /// <returns></returns>
        public Int64 Countall()
        {
            using (TransportMandiEntities db=new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))

            {
                Int64 c = 0;
                c = (from mast in db.tblItemMastPurs
                           join ig in db.tblIGrpMasts on mast.IGrp_Idno equals ig.IGrp_Idno
                           join IM in db.tblItemTypeMasts on mast.ItemType equals IM.ItemTpye_Idno
                           join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno select mast.Item_Idno).Count();
                return c;
            }
        }


        public Int64 SelectSearch(Int64 itemId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst1 = (from mast in db.MatIssDetls
                            where mast.Iteam_Idno == itemId
                            select mast.Iteam_Idno).ToList();
                var count1 = lst1.Count;

                var lst2 = (from pmast in db.tblPBillDetls
                            where pmast.Item_Idno == itemId
                            select pmast.Item_Idno).ToList();
                var count2 = lst2.Count;

                var lst3 = (from pmast in db.tblPBillDetls
                            where pmast.Item_Idno == itemId
                            select pmast.Item_Idno).ToList();
                var count3 = lst3.Count;

                var lst4 = (from pmast in db.Stckdetls
                            where pmast.ItemIdno == itemId
                            select pmast.ItemIdno).ToList();
                var count4 = lst4.Count;

                var lst5 = (from pmast in db.StckMasts
                            where pmast.Item_Idno == itemId
                            select pmast.Item_Idno).ToList();
                var count5 = lst5.Count;

                var count = count1 + count2 + count3 + count4+count5;
                return count;
            }
        }
        /// <summary>
        /// Select one record from tblItemMastPur by ItemIdno
        /// </summary>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public tblItemMastPur SelectById(int intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblItemMastPurs
                        where mast.Item_Idno == intItemIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To check record existence in tblItemMastPur
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="intItemIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strItemName, Int64 intItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblItemMastPur objItemMast = new tblItemMastPur();
                if (intItemIdno <= 0)
                {
                    objItemMast = (from mast in db.tblItemMastPurs
                                   where mast.Item_Name == strItemName
                                   select mast).FirstOrDefault();
                }
                else if (intItemIdno > 0)
                {
                    objItemMast = (from mast in db.tblItemMastPurs
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

        //public ItemDetailForGrid GetItemByItemId(Int64 ItemId)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from item in db.tblItemMastPurs
        //                   join uom in db.UOMMasts on item.Unit_Idno equals uom.UOM_Idno
        //                   where item.Item_Idno == ItemId
        //                   select new { item.Item_Name, item.Item_Idno, Uom = uom.UOM_Name, item.Unit_Idno }
        //                     ).FirstOrDefault();
        //        ItemDetailForGrid idg = new ItemDetailForGrid();
        //        if (lst != null)
        //        {
        //            //idg.ItemId = lst.Item_Idno;
        //            //idg.ItemName = lst.Item_Name;
        //            //idg.Uom = lst.Uom;
        //            //idg.UomId = Convert.ToInt64(lst.Unit_Idno);
        //        }
        //        return idg;
        //    }
        //}
        public IList GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from item in db.tblItemMastPurs
                           select new { ItemName = item.Item_Name, ItemId = item.Item_Idno }
                             ).ToList();
                return lst;
            }
        }

        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in tblItemMastPur
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strItemDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strItemName, string strItemNameHindi, string strItemDesc, Int64 GrpType, Int64 ItemUnit, Double PurRate, Double VATTaxPer, Double CSTTaxPer, bool bStatus, Int64 ItemType, Int64 TyreIdno, string Companyname, string ModelName, Int32 empIdno,double sgst,double cgst,double igst,double itemmrp)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblItemMastPur objItemMast = new tblItemMastPur();
                    objItemMast.Item_Name = strItemName;
                    objItemMast.ItemNamel_Hindi = strItemNameHindi;
                    objItemMast.Item_Desc = strItemDesc;
                    objItemMast.Status = bStatus;
                    objItemMast.IGrp_Idno = GrpType;
                    objItemMast.Unit_Idno = ItemUnit;
                    objItemMast.Pur_Rate = PurRate;
                    objItemMast.VATTax_Per = VATTaxPer;
                    objItemMast.CSTTax_Per = CSTTaxPer;
                    objItemMast.Comp_Idno = intCompIdno;
                    objItemMast.Emp_Idno = empIdno;

                    objItemMast.ItemType = ItemType;
                    objItemMast.Tyre_Type = TyreIdno;
                    objItemMast.Comp_Name = Companyname;
                    objItemMast.Model_Name = ModelName;

                    objItemMast.SGST = sgst;
                    objItemMast.CGST = cgst;
                    objItemMast.IGST = igst;
                    objItemMast.Item_MRP = itemmrp;

                    objItemMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strItemName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblItemMastPurs.AddObject(objItemMast);
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
        /// Update records in tblItemMastPur
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public Int64 Update(string strItemName, string strItemNameHindi, string strItemDesc, Int64 GrpType, Int64 ItemUnit, Double PurRate, Double VATTaxPer, Double CSTTaxPer, bool bStatus, Int64 intItemIdno, Int64 ItemType, Int64 TyreIdno, string Companyname, string ModelName, Int32 empIdno, double sgst, double cgst, double igst, double itemmrp)
        {
            Int64 intValue = 0;
            //Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblItemMastPur objItemMast = (from mast in db.tblItemMastPurs
                                                  where mast.Item_Idno == intItemIdno
                                                  select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.Item_Name = strItemName;
                        objItemMast.ItemNamel_Hindi = strItemNameHindi;
                        objItemMast.Item_Desc = strItemDesc;
                        objItemMast.Pur_Rate = PurRate;
                        objItemMast.VATTax_Per = VATTaxPer;
                        objItemMast.CSTTax_Per = CSTTaxPer;
                        objItemMast.Status = bStatus;
                        objItemMast.Unit_Idno = ItemUnit;
                        objItemMast.IGrp_Idno = GrpType;
                        objItemMast.ItemType = ItemType;
                        objItemMast.Tyre_Type = TyreIdno;
                        objItemMast.Comp_Name = Companyname;
                        objItemMast.Model_Name = ModelName;
                        objItemMast.Emp_Idno = empIdno;

                        objItemMast.SGST = sgst;
                        objItemMast.CGST = cgst;
                        objItemMast.IGST = igst;
                        objItemMast.Item_MRP = itemmrp;
                        objItemMast.Date_Modified = System.DateTime.Now;
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
        /// Delete record from tblItemMastPur
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
                    tblItemMastPur objItemMast = (from mast in db.tblItemMastPurs
                                                  where mast.Item_Idno == intItemIdno
                                                  select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        db.tblItemMastPurs.DeleteObject(objItemMast);
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

        public Int32 UpdateStatus(int intItemIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblItemMastPur objItemMast = (from mast in db.tblItemMastPurs
                                                  where mast.Item_Idno == intItemIdno
                                                  select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.Status = Status;
                        objItemMast.Emp_Idno = empIdno;
                        objItemMast.Date_Modified = System.DateTime.Now;
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

        // For Item type (new added with table)
        public List<tblItemTypeMast> SelectItemType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemTypeMast> lst = null;
                lst = (from ITM in db.tblItemTypeMasts orderby ITM.ItemTpye_Idno ascending select ITM).ToList();
                return lst;
            }
        }

        
        public DataTable SelectType(string con,Int64 intValue)
        {
            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT DISTINCT TEMP2.ItemTpye_Idno AS IDNO FROM TBLIGRPMAST AS TEMP1 INNER JOIN TBLITEMTYPEMAST AS TEMP2 ON TEMP1.IGrp_Type=TEMP2.ItemTpye_Idno AND TEMP1.Status=1 WHERE TEMP1.IGrp_Idno=" + intValue + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        public Int64 SelectItemTypeFromItem(Int64 intItemValue)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Value = 0;
                Value = (from ITM in db.tblItemTypeMasts
                         join IM in db.tblItemMastPurs on ITM.ItemTpye_Idno equals IM.ItemType
                         select ITM.ItemTpye_Idno).FirstOrDefault();
                return Value;
            }
        }
        public List<tblTyreTypeMaster> SelectTyreType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblTyreTypeMaster> lst = null;
                lst = (from tyr in db.tblTyreTypeMasters where tyr.TyreType_Status == true orderby tyr.TyreType_Name ascending select tyr).ToList();
                return lst;
            }
        }
           public List<tblTyreTypeMaster> SelectTyreTypeAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblTyreTypeMaster> lst = null;
                lst = (from tyr in db.tblTyreTypeMasters orderby tyr.TyreType_Name ascending select tyr).ToList();
                return lst;
            }
        }
    }

    //public class ItemDetailForGrid
    //{
    //    public Int64 ItemId { get; set; }
    //    public Int64 UomId { get; set; }
    //    public string Uom { get; set; }
    //    public string ItemName { get; set; }
    //}
}