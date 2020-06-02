using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class OpenAccDAL
    {
        public List<tblCityMaster> BindCityAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objCityMasterList = new List<tblCityMaster>();

                objCityMasterList = (from obj in db.tblCityMasters
                                     where obj.Status == true && obj.AsLocation == true
                                     orderby obj.City_Name
                                     select obj).ToList();

                return objCityMasterList;
            }
        }
        public Int32 Insert(DataTable DT, DataTable dtDelete)
        {
            Int32 value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    //For Deleting ID's
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        Int64 ItemId = Convert.ToInt64(dtDelete.Rows[i]["ItemIdno"]);
                        Int64 locidno = Convert.ToInt64(dtDelete.Rows[i]["LocIdno"]);
                        List<StckMast> SD1 = db.StckMasts.Where(r => r.Item_Idno == ItemId && r.Loc_Idno == locidno).ToList();
                        if (SD1 != null && SD1.Count > 0)
                        {
                            foreach (StckMast sd in SD1)
                            {
                                db.StckMasts.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }
                    }
                    // End ID's

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        Int64 ItemId = Convert.ToInt64(DT.Rows[i]["ItemIdno"]);
                        Int64 locidno = Convert.ToInt64(DT.Rows[i]["LocIdno"]);
                        List<StckMast> SD = db.StckMasts.Where(r => r.Item_Idno == ItemId && r.Loc_Idno == locidno).ToList();
                        if (SD != null && SD.Count > 0)
                        {
                            foreach (StckMast sd in SD)
                            {
                                db.StckMasts.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }
                        StckMast Mast = new StckMast();
                        Mast.Item_Idno = Convert.ToInt64(DT.Rows[i]["ItemIdno"]);
                        Mast.Loc_Idno = Convert.ToInt64(DT.Rows[i]["LocIdno"]);
                        Mast.Year_Idno = Convert.ToInt64(DT.Rows[i]["YearIdno"]);
                        Mast.Open_Qty = Convert.ToInt64(DT.Rows[i]["Qty"]);
                        Mast.Open_Rate = Convert.ToDouble(DT.Rows[i]["Rate"]);
                        db.StckMasts.AddObject(Mast);
                        db.SaveChanges();
                        value = Convert.ToInt32(Mast.Id);
                    }
                }
                value = 1;
            }
            catch (Exception Ex)
            {
                value = 0;
            }
            return value;
        }
        //public int Update(DataTable Dt, Int64 intStckid)
        //{
        //    int Value = 0;
        //    try
        //    {
        //        if (Delete(intStckid) > 0)
        //        {
        //            Value = Insert(Dt);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Value = 0;
        //    }
        //    return Value;
        //}
        public IList SelectForSearch(Int32 itemIdno, Int32 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.StckMasts
                           join I in db.tblItemMastPurs on CT.Item_Idno equals I.Item_Idno
                           join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno into gj
                           from full in gj.DefaultIfEmpty()
                           select new
                           {
                               Id = CT.Id,
                               ItemName = I.Item_Name,
                               ItemIdno = CT.Item_Idno,
                               LocName = full.City_Name,
                               LocIdno = CT.Loc_Idno,
                               Qty = CT.Open_Qty,
                           }).ToList();
                if (itemIdno > 0)
                {
                    lst = (from l in lst where l.ItemIdno == itemIdno select l).ToList();
                }
                if (LocIdno > 0)
                {
                    lst = (from l in lst where l.LocIdno == LocIdno select l).ToList();
                }
                return lst;
            }
        }
        public int Delete(Int64 intStckid)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    StckMast objMast = (from mast in db.StckMasts
                                        where mast.Id == intStckid
                                        select mast).FirstOrDefault();
                    if (objMast != null)
                    {
                        Int64 ItemIdno = Convert.ToInt64(objMast.Item_Idno); Int64 Loc_idno = Convert.ToInt64(objMast.Loc_Idno);

                        List<Stckdetl> ObjDetl = db.Stckdetls.Where(r => r.ItemIdno == ItemIdno && r.Loc_Idno == Loc_idno && r.Item_from == "O" && r.MtrlIssue_Idno == 0 && r.Is_Issued == false).ToList();
                        if (ObjDetl.Count > 0)
                        {
                            foreach (Stckdetl sd in ObjDetl)
                            {
                                db.Stckdetls.DeleteObject(sd);
                            }
                        }

                        db.StckMasts.DeleteObject(objMast);
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

        public IList SelectDetlById(Int64 itemIdno, Int64 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.Stckdetls
                        join PM in db.tblItemMastPurs on mast.ItemIdno equals PM.Item_Idno
                        join L in db.tblCityMasters on mast.Loc_Idno equals L.City_Idno
                        where mast.ItemIdno == itemIdno && mast.Loc_Idno == LocIdno && mast.Item_from == "O"
                        select new
                        {
                            Item_Name = PM.Item_Name,
                            Item_Idno = mast.ItemIdno,
                            Loc_Idno = mast.Loc_Idno,
                            Loc_Name = L.City_Name,
                            SerialNo = mast.SerialNo

                        }).ToList();
            }
        }

        public IList SelectStckMast(Int64 YearIdno, Int64 LocIdno, Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from SM in db.StckMasts
                           join
                               IM in db.tblItemMastPurs on SM.Item_Idno equals IM.Item_Idno
                           join
                               CM in db.tblCityMasters on SM.Loc_Idno equals CM.City_Idno
                           select new
                           {
                               YearIdno = SM.Year_Idno,
                               LocIdno = SM.Loc_Idno,
                               LocName = CM.City_Name,
                               ItemIdno = SM.Item_Idno,
                               ItemName = IM.Item_Name,
                               Qty = SM.Open_Qty,
                               Rate = SM.Open_Rate
                           }).ToList();


                if (YearIdno > 0)
                {
                    Lst = Lst.Where(r => r.YearIdno == YearIdno).ToList();
                }
                if (LocIdno > 0)
                {
                    Lst = Lst.Where(r => r.LocIdno == LocIdno).ToList();
                }
                if (ItemIdno > 0)
                {
                    Lst = Lst.Where(r => r.ItemIdno == ItemIdno).ToList();
                }

                return Lst;
            }
        }
        public IList SelectStckMastList(Int64 YearIdno, Int64 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from SM in db.StckMasts
                           join
                               IM in db.tblItemMastPurs on SM.Item_Idno equals IM.Item_Idno
                           join
                               CM in db.tblCityMasters on SM.Loc_Idno equals CM.City_Idno
                           where IM.ItemType == 2
                           select new
                           {
                               YearIdno = SM.Year_Idno,
                               LocIdno = SM.Loc_Idno,
                               LocName = CM.City_Name,
                               Amount = (from Amnt in db.StckMasts where Amnt.Loc_Idno == SM.Loc_Idno && Amnt.Year_Idno == YearIdno select Amnt.Open_Rate * Amnt.Open_Qty).Sum(),
                               Qty = (from S in db.StckMasts
                                      join
                                          IM1 in db.tblItemMastPurs on S.Item_Idno equals IM1.Item_Idno
                                      join
                                          CM1 in db.tblCityMasters on S.Loc_Idno equals CM1.City_Idno
                                      where S.Loc_Idno == SM.Loc_Idno && IM1.ItemType == 2
                                      select S.Open_Qty).Sum()
                           }).Distinct().ToList();


                if (YearIdno > 0)
                {
                    Lst = Lst.Where(r => r.YearIdno == YearIdno).ToList();
                }
                if (LocIdno > 0)
                {
                    Lst = Lst.Where(r => r.LocIdno == LocIdno).ToList();
                }


                return Lst;
            }
        }
        public int ICount(string SerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.Stckdetls.Where(r => r.SerialNo == SerialNo && r.MtrlIssue_Idno != 0 && r.Is_Issued == false).Count();
            }
        }
        public List<tblItemMastPur> BindPurchaseItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == 2
                               select obj).ToList();

                return objItemMast;
            }
        }

        public IList GetExcel(Int64 intLocIdno, Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.StckMasts
                           join Im in db.tblItemMastPurs on CT.Item_Idno equals Im.Item_Idno
                           join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                           where CT.Loc_Idno == intLocIdno && Im.ItemType == 2
                           orderby sm.City_Name
                           select new
                           {
                               ItemName = Im.Item_Name,
                               LocName = sm.City_Name,
                               Qty = CT.Open_Qty,
                               Rate = CT.Open_Rate,
                               Amount = ((CT.Open_Qty) * (CT.Open_Rate))
                           }).Distinct().ToList();

                return lst;
            }
        }
        public Int64 Count(Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from SM in db.StckMasts
                        join
                            IM in db.tblItemMastPurs on SM.Item_Idno equals IM.Item_Idno
                        join
                            CM in db.tblCityMasters on SM.Loc_Idno equals CM.City_Idno
                        where SM.Year_Idno == YearIdno && IM.ItemType == 2
                        select new
                        {
                            YearIdno = SM.Year_Idno,
                            LocIdno = SM.Loc_Idno,
                            LocName = CM.City_Name,
                            Qty = (from S in db.StckMasts where S.Loc_Idno == SM.Loc_Idno select S.Open_Qty).Sum()
                        }).Distinct().Count();
            }

        }



        #region ForExcelReading

        public int TurncatePartsAccessoriesFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblOpngStockAccessFromExcel";
            int res = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return res;
        }

        public Int64 InsertPartsByExcel(DataTable dt, string ConString)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string modelName = row["Item"].ToString();
                    tblOpngStockAccessFromExcel obj = (from objstock in db.tblOpngStockAccessFromExcels
                                                       where objstock.Item_Name == modelName
                                                       select objstock).FirstOrDefault();
                    if (obj == null)
                    {
                        tblOpngStockAccessFromExcel objPart = new tblOpngStockAccessFromExcel();
                        objPart.Item_Name = Convert.ToString(row["Item"]);
                        objPart.Item_Rate = Convert.ToDouble(row["Rate"]);
                        objPart.Item_Qty = Convert.ToInt64(row["Qty"]);
                        db.tblOpngStockAccessFromExcels.AddObject(objPart);
                        db.SaveChanges();
                        value = objPart.Stck_Idno;
                    }
                }
                UpdatefromExcel2(ConString);
            }
            return value;
        }

        public DataTable UpdatefromExcel2(string ConString)
        {
            string str = string.Empty;
            str = @"UPDATE tblOpngStockAccessFromExcel SET ITEM_IDNO = I.Item_Idno FROM tblItemMastPur I WHERE tblOpngStockAccessFromExcel.Item_Name = I.Item_Name";
            DataSet ds = SqlHelper.ExecuteDataset(ConString, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable SelectPartFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"SELECT * FROM tblOpngStockAccessFromExcel";
            DataSet ds = SqlHelper.ExecuteDataset(ConString, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public IList GetItemDetailsExl(string ItemName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from obj in db.tblItemMastPurs where obj.Item_Name == ItemName select obj).ToList();

                return Lst;
            }
        }
        #endregion


        public IList CheckItemExistInOtherMaster(Int32 ItemIdno,Int32 Locidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.MatIssHeads
                        join obj1 in db.MatIssDetls on obj.MatIss_Idno equals obj1.MatIssHead_Idno
                        where obj1.Iteam_Idno == ItemIdno && obj.Loc_Idno == Locidno
                        select new
                        {
                            ItemGrp = obj1.Iteam_Idno,
                            Loc_Idno=obj.Loc_Idno
                        }
                        ).ToList();
            }
        }

        public Int32 Delete(DataTable dtDelete)
        {
            Int32 value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        Int64 ItemId = Convert.ToInt64(dtDelete.Rows[i]["ItemIdno"]);
                        Int64 locidno = Convert.ToInt64(dtDelete.Rows[i]["LocIdno"]);
                        List<StckMast> SD1 = db.StckMasts.Where(r => r.Item_Idno == ItemId && r.Loc_Idno == locidno).ToList();
                        if (SD1 != null && SD1.Count > 0)
                        {
                            foreach (StckMast sd in SD1)
                            {
                                db.StckMasts.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }
                    }
                }
                value = 1;
            }
            catch (Exception Ex)
            {
                value = 0;
            }
            return value;
        }
    }
}
