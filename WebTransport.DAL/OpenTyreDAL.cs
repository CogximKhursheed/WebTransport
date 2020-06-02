using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class OpenTyreDAL
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
        public Int32 Insert(DataTable DT, int yearId, int itemId, int locId, DataTable dtDelete)
        {
            Int32 value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    Int64 locidno = Convert.ToInt64(locId);
                    //For Deleting ID's
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        string SerialNo = Convert.ToString(dtDelete.Rows[i]["SerialNo"]);
                        Int64 ItemId = Convert.ToInt64(dtDelete.Rows[i]["TyreIdno"]);
                        List<Stckdetl> SD1 = db.Stckdetls.Where(r => r.ItemIdno == ItemId && r.Loc_Idno == locidno && r.SerialNo == SerialNo).ToList();
                        if (SD1 != null && SD1.Count > 0)
                        {
                            foreach (Stckdetl sd in SD1)
                            {
                                db.Stckdetls.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }
                    }
                    // End ID's

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string SerialNo = Convert.ToString(DT.Rows[i]["SerialNo"]);
                        Int64 ItemId = Convert.ToInt64(DT.Rows[i]["TyreIdno"]);
                        List<Stckdetl> SD = db.Stckdetls.Where(r => r.ItemIdno == ItemId && r.Loc_Idno == locidno && r.SerialNo == SerialNo).ToList();
                        if (SD != null && SD.Count > 0)
                        {
                            foreach (Stckdetl sd in SD)
                            {
                                db.Stckdetls.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }
                        Int64 SerailIdno = string.IsNullOrEmpty(Convert.ToString(DT.Rows[i]["SerialIdno"])) ? 0 : Convert.ToInt64(DT.Rows[i]["SerialIdno"]);
                        List<Stckdetl> SD1 = db.Stckdetls.Where(r => r.ItemIdno == ItemId && r.Loc_Idno == locidno && r.SerlDetl_id == SerailIdno).ToList();
                        if (SD1 != null && SD1.Count > 0)
                        {
                            foreach (Stckdetl sd in SD1)
                            {
                                db.Stckdetls.DeleteObject(sd);
                            }
                            db.SaveChanges();
                        }

                        Stckdetl TblStckDetl = new Stckdetl();

                        TblStckDetl.yearId = yearId;
                        TblStckDetl.SerialNo = Convert.ToString(DT.Rows[i]["SerialNo"]);
                        TblStckDetl.CompName = Convert.ToString(DT.Rows[i]["CompName"]);
                        TblStckDetl.Type = Convert.ToInt32(DT.Rows[i]["TypeId"]);
                        TblStckDetl.PurFrom = Convert.ToString(DT.Rows[i]["PurFrom"]);
                        TblStckDetl.OpenRate = Convert.ToDouble(DT.Rows[i]["OpenRate"]);
                        TblStckDetl.PBillIdno = 0;
                        TblStckDetl.Loc_Idno = locId;
                        TblStckDetl.ItemIdno = Convert.ToInt32(DT.Rows[i]["TyreIdno"]);
                        TblStckDetl.TyresizeIdno = string.IsNullOrEmpty(Convert.ToString(DT.Rows[i]["TyreSizeIdno"])) ? 0 : Convert.ToInt32(DT.Rows[i]["TyreSizeIdno"]);
                        TblStckDetl.Item_from = "O";
                        TblStckDetl.MtrlIssue_Idno = 0;
                        TblStckDetl.Is_Issued = false;
                        TblStckDetl.Billed = false;
                        db.Stckdetls.AddObject(TblStckDetl);
                        db.SaveChanges();
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

        public IList SelectForSearch(Int32 YearIdno, Int32 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.Stckdetls
                           join I in db.tblItemMastPurs on CT.ItemIdno equals I.Item_Idno
                           join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                           where I.ItemType == 1
                           select new
                           {
                               YearId = CT.yearId,
                               Id = CT.SerlDetl_id,
                               ItemName = I.Item_Name,
                               ItemIdno = CT.ItemIdno,
                               Qty = (from Amnt in db.Stckdetls
                                      join I1 in db.tblItemMastPurs on Amnt.ItemIdno equals I1.Item_Idno
                                      join sm1 in db.tblCityMasters on Amnt.Loc_Idno equals sm1.City_Idno
                                      where I1.ItemType == 1 && Amnt.Loc_Idno == CT.Loc_Idno && Amnt.yearId == YearIdno
                                      select Amnt.ItemIdno).Count(),
                               LocName = sm.City_Name,
                               LocIdno = CT.Loc_Idno,
                               Amount = (from Amnt in db.Stckdetls where Amnt.Loc_Idno == CT.Loc_Idno && Amnt.yearId == YearIdno select Amnt.OpenRate).Sum(),
                           }).GroupBy(x => x.LocIdno).Select(x => x.FirstOrDefault()).ToList();

                if (YearIdno > 0)
                {
                    lst = (from l in lst where l.YearId == YearIdno select l).ToList();
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
        public StckMast SelectById(Int64 intIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.StckMasts where mast.Id == intIdno select mast).FirstOrDefault();
            }
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
        public Stckdetl SelectHeadByItemIdno(Int64 intIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.Stckdetls where mast.ItemIdno == intIdno select mast).FirstOrDefault();
            }
        }
        public IList SelectDetlByItemIdno(Int64 YearIdno, Int64 LocIdno, Int64 itemIdno,Int64 tyresizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from mast in db.Stckdetls
                           join PM in db.tblItemMastPurs on mast.ItemIdno equals PM.Item_Idno
                           join L in db.tblCityMasters on mast.Loc_Idno equals L.City_Idno
                           join TS in db.TyreSizeMasters on mast.TyresizeIdno equals TS.TyreSize_Idno into temptyre
                           from TS in temptyre.DefaultIfEmpty()
                           where mast.Item_from == "O"
                           select new
                           {
                               Item_Name = PM.Item_Name,
                               YearIdno = mast.yearId,
                               Item_Idno = mast.ItemIdno,
                               Loc_Idno = mast.Loc_Idno,
                               Loc_Name = L.City_Name,
                               SerialNo = mast.SerialNo,
                               SerialIdno = mast.SerlDetl_id,
                               CompName = mast.CompName,
                               ItemType = mast.Type,
                               PurFrom = mast.PurFrom,
                               OpenRate = mast.OpenRate,
                               tyresizeidno = mast.TyresizeIdno,
                               tyresize = TS.TyreSize
                           }).ToList();

                if (YearIdno > 0)
                {
                    Lst = Lst.Where(r => r.YearIdno == YearIdno).ToList();
                }
                if (LocIdno > 0)
                {
                    Lst = Lst.Where(r => r.Loc_Idno == LocIdno).ToList();
                }
                if (itemIdno > 0)
                {
                    Lst = Lst.Where(r => r.Item_Idno == itemIdno).ToList();
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
        //public int Update(DataTable Dt, Int64 intStckid, int yearId, int itemId, int locId)
        //{
        //    int Value = 0;
        //    try
        //    {
        //        if (Delete(intStckid) > 0)
        //        {
        //            Value = Insert(Dt, yearId, itemId, locId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Value = 0;
        //    }
        //    return Value;
        //}
        public List<tblItemMastPur> BindPurchaseItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == 1
                               select obj).ToList();

                return objItemMast;
            }
        }

        public List<tblItemMastPur> BindPurchaseItemNameAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == 1
                               select obj).ToList();

                return objItemMast;
            }
        }

        public IList GetExcel(Int64 intLocIdno, Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.Stckdetls
                           join Im in db.tblItemMastPurs on CT.ItemIdno equals Im.Item_Idno
                           join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                           where CT.Loc_Idno == intLocIdno
                           orderby sm.City_Name
                           select new
                           {
                               ItemName = Im.Item_Name,
                               LoctionName = sm.City_Name,
                               SerialNo = CT.SerialNo,
                               CompanyName = CT.CompName,
                               Rate = CT.OpenRate,
                               Amount = CT.OpenRate,
                           }).Distinct().ToList();

                return lst;
            }

        }

        public Int64 Count(Int64 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from CT in db.Stckdetls
                        join I in db.tblItemMastPurs on CT.ItemIdno equals I.Item_Idno
                        join sm in db.tblCityMasters on CT.Loc_Idno equals sm.City_Idno
                        where I.ItemType == 1 && CT.yearId == YearIdno
                        select new
                        {
                            Id = CT.SerlDetl_id,
                            ItemIdno = CT.ItemIdno,
                            Qty = (from Amnt in db.Stckdetls
                                   join I1 in db.tblItemMastPurs on Amnt.ItemIdno equals I1.Item_Idno
                                   join sm1 in db.tblCityMasters on Amnt.Loc_Idno equals sm1.City_Idno
                                   where I1.ItemType == 1 && Amnt.Loc_Idno == CT.Loc_Idno && Amnt.yearId == YearIdno
                                   select Amnt.ItemIdno).Count(),
                            LocIdno = CT.Loc_Idno,
                        }).GroupBy(x => x.LocIdno).Select(x => x.FirstOrDefault()).Count();
            }

        }
        public List<TyreSizeMaster> Bindtyresize()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<TyreSizeMaster> obj1 = new List<TyreSizeMaster>();

                obj1 = (from obj in db.TyreSizeMasters
                               orderby obj.TyreSize
                               select obj).ToList();

                return obj1;
            }
        }

        #region ForExcelReading

        public int TurncatePartsAccessoriesFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblOpngStockTyreFromExcel";
            int result = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return result;
        }

        public Int64 InsertPartsByExcel(DataTable dt, string ConString)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string modelName = row["TyreName"].ToString();
                    tblOpngStockTyreFromExcel obj = (from objstock in db.tblOpngStockTyreFromExcels
                                                     where objstock.Item_Name == modelName
                                                     select objstock).FirstOrDefault();
                    //if (obj == null)
                    //{
                    tblOpngStockTyreFromExcel objPart = new tblOpngStockTyreFromExcel();
                    objPart.Item_Name = Convert.ToString(row["TyreName"]);
                    objPart.Item_Rate = Convert.ToDouble(row["OpeningRate"]);
                    objPart.SerialNo = Convert.ToString(row["SerialNo"]);
                    objPart.CompanyName = Convert.ToString(row["CompanyName"]);
                    objPart.PurchaseFrom = Convert.ToString(row["PurchaseFrom"]);
                    objPart.Type = Convert.ToString(row["Type"]);
                    objPart.Type_Idno = Convert.ToInt32(row["Type_Idno"]);
                    db.tblOpngStockTyreFromExcels.AddObject(objPart);
                    db.SaveChanges();
                    value = objPart.Stck_Idno;
                    // }
                }
                UpdatefromExcel2(ConString);
            }
            return value;
        }

        public DataTable UpdatefromExcel2(string ConString)
        {
            string str = string.Empty;
            str = @"UPDATE tblOpngStockTyreFromExcel SET ITEM_IDNO = I.Item_Idno FROM tblItemMastPur I WHERE tblOpngStockTyreFromExcel.Item_Name = I.Item_Name";
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
            str = @"SELECT * FROM tblOpngStockTyreFromExcel";
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
    }
}
