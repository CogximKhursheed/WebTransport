using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class RateMasterDAL
    {
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public Int64 Kms(Int32 FromCityIDno, Int32 CityViaIDno, Int32 TocityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Dist = (from dist in db.DistanceMasts where dist.FrmCity_Idno == FromCityIDno && dist.ViaCity_Idno == CityViaIDno && dist.ToCity_Idno == TocityIdno select dist.KMs).FirstOrDefault();
                return Convert.ToInt64(Dist);
            }
        }
        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        public IList SelectForSearch(Int64 ItemId, string strRateType, Int64 FrmCityId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblRateMasts
                           where mast.Item_Idno == ItemId && mast.Rate_Type == strRateType && mast.FrmCity_Idno == FrmCityId
                           select new
                           {
                               Rate_Date = mast.Rate_Date,
                               ToCity_Idno = mast.ToCity_Idno,
                               CityVia = mast.Cityvia_Idno,
                               Item_Rate = mast.Item_Rate,
                               Item_WghtRate = mast.Item_WghtRate,
                               QtyShrtg_Limit = mast.QtyShrtg_Limit,
                               QtyShrtg_Rate = mast.QtyShrtg_Rate,
                               WghtShrtg_Limit = mast.WghtShrtg_Limit,
                               WghtShrtg_Rate = mast.WghtShrtg_Rate,
                               Item_Rate2 = mast.Item_Rate2,
                               Item_Rate3 = mast.Item_Rate3,
                               ItemRate_Type = mast.ItemRate_Type,
                               Status = mast.Status
                           }).ToList();

                return lst;
            }
        }
        public tblRateMast SelectById(int intRateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        where mast.Rate_Idno == intRateIdno
                        select mast).FirstOrDefault();
            }
        }
        public List<ItemMast> GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> lst = null;
                lst = (from cm in db.ItemMasts orderby cm.Item_Name ascending select cm).ToList();
                return lst;
            }
        }
        public IList GetContainerSize()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblContainerSizes where obj.Status == true select obj).ToList();
            }
        }
        public bool Insert(DataTable dT, Int32 itemidno, string strRateType, Int32 FrmCityIdno, Int32 ToCityIdno, Int32 empIdno)
        {
            bool value = true;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    for (int i = 0; i < dT.Rows.Count; i++)
                    {
                        tblRateMast obj = new tblRateMast();
                        obj.Item_Idno = itemidno;
                        obj.FrmCity_Idno = FrmCityIdno;
                        obj.ToCity_Idno = ToCityIdno;
                        obj.Emp_Idno = empIdno;
                        obj.Rate_Type = Convert.ToString(dT.Rows[i]["Rate_Type"]);
                        obj.Rate_Date = Convert.ToDateTime(dT.Rows[i]["Rate_Date"]);
                        obj.Item_Rate = Convert.ToDouble(dT.Rows[i]["Item_Rate"]);
                        obj.Item_WghtRate = Convert.ToDouble(dT.Rows[i]["Item_WghtRate"]);
                        obj.ToCity_Idno = Convert.ToInt32(dT.Rows[i]["ToCity_Idno"]);
                        obj.Cityvia_Idno = Convert.ToInt32(dT.Rows[i]["CityVia_Idno"]);
                        obj.QtyShrtg_Limit = Convert.ToDouble(dT.Rows[i]["QtyShrtg_Limit"]);
                        obj.QtyShrtg_Rate = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["QtyShrtg_Rate"])) ? 0 : Convert.ToDouble(dT.Rows[i]["QtyShrtg_Rate"]);
                        obj.WghtShrtg_Limit = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["WghtShrtg_Limit"])) ? 0 : Convert.ToDouble(dT.Rows[i]["WghtShrtg_Limit"]);
                        obj.WghtShrtg_Rate = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["WghtShrtg_Rate"])) ? 0 : Convert.ToDouble(dT.Rows[i]["WghtShrtg_Rate"]);
                        obj.Item_Rate2 = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Item_Rate2"])) ? 0 : Convert.ToDouble(dT.Rows[i]["Item_Rate2"]);
                        obj.Item_Rate3 = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Item_Rate3"])) ? 0 : Convert.ToDouble(dT.Rows[i]["Item_Rate3"]);
                        obj.ItemRate_Type = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["ItemRateType_Idno"])) ? 0 : Convert.ToInt32(dT.Rows[i]["ItemRateType_Idno"]);
                        obj.Dist_Km = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Dist_km"])) ? 0 : Convert.ToDouble(dT.Rows[i]["Dist_km"]);
                        obj.DistMast_Idno = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["DistanceIdno"])) ? 0 : Convert.ToInt64(dT.Rows[i]["DistanceIdno"]);
                        obj.Con_Size = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["ConSize"])) ? "0" : Convert.ToString(dT.Rows[i]["ConSize"]);
                        obj.Con_Weight = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["ConWeight"])) ? 0 : Convert.ToDouble(dT.Rows[i]["ConWeight"]);
                        obj.ConSize_ID = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["ConSizeID"])) ? 0 : Convert.ToInt32(dT.Rows[i]["ConSizeID"]);
                        obj.Item_Weight = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Item_Weight"])) ? 0 : Convert.ToDecimal(dT.Rows[i]["Item_Weight"]);
                        obj.IsWeight = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Item_Weight"])) ? false : Convert.ToBoolean(dT.Rows[i]["IsWeight"]);
                        obj.ItemRate_KM = Convert.ToDouble(dT.Rows[i]["ItemRate_KM"]);
                        obj.Status = true;
                        obj.Date_Added = DateTime.Now; obj.Date_Modified = DateTime.Now;
                        db.tblRateMasts.AddObject(obj);
                        db.SaveChanges();
                    }
                }
                catch (Exception Ex)
                {
                    value = false;
                }
            }
            return value;
        }
        public DataTable SelectDBData(Int64 Item_Idno, Int64 FrmCity_Idno, string RateType, Int32 tocity, string con)
        {
            string str = string.Empty;
            if (tocity > 0)
            {
                str = @"select Rate_Idno Rate_Idno,isnull(Rate_Date,0) Rate_Date, Item_Idno, Isnull(CM.City_Name,'') City_Name ,Isnull(CM2.City_Name,'') CityVia_Name,Isnull(Item_Rate,0) Item_Rate,
                    ISNULL(Rate_Type,0)Rate_Type,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,
                    ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0) WghtShrtg_Rate, ISNULL(Item_Rate2,0) Item_Rate2, ISNULL(Item_Rate3,0) Item_Rate3,
                    ISNULL(ItemRate_Type,0) ItemRate_Type,'' as ItemRate_TypeIdno, ISNULL(Tocity_Idno,0) Tocity_Idno ,ISNULL(Cityvia_Idno,0) Cityvia_Idno,
                    ISNULL(RM.FrmCity_Idno,0) FrmCity_Idno,isnull(Dist_Km,0) Dist_Km,isnull(DistMast_Idno,0) DistanceIdno,isnull(Con_Size,0) ConSize,isnull(Con_Weight,0) ConWeight,isnull(ConSize_ID,0) ConSizeID,Isnull(RM.Item_Weight,0.00)Item_Weight,IsWeight, ISNULL(ItemRate_KM,0) ItemRate_KM
                    from tblratemast RM Inner Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = RM.FrmCity_Idno 
                    Inner Join  tblCityMaster CM2  ON CM2.city_Idno  = RM.Cityvia_Idno  where RM.Item_Idno= " + Item_Idno + " and RM.FrmCity_Idno= CASE WHEN " + FrmCity_Idno + "!= 0 THEN " + FrmCity_Idno + " ELSE RM.FrmCity_Idno END and RM.Rate_Type= '" + RateType + "' and RM.ToCity_Idno= CASE WHEN " + tocity + "!= 0 THEN " + tocity + " ELSE RM.ToCity_Idno END order by RM.Rate_Date desc";
            }
            else
            {
                str = @"select Rate_Idno Rate_Idno,isnull(Rate_Date,0) Rate_Date, Item_Idno, Isnull(CM.City_Name,'') City_Name ,Isnull(CM2.City_Name,'') CityVia_Name,Isnull(Item_Rate,0) Item_Rate,
                    ISNULL(Rate_Type,0)Rate_Type,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,
                    ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0) WghtShrtg_Rate, ISNULL(Item_Rate2,0) Item_Rate2, ISNULL(Item_Rate3,0) Item_Rate3,
                    ISNULL(ItemRate_Type,0) ItemRate_Type,'' as ItemRate_TypeIdno, ISNULL(Tocity_Idno,0) Tocity_Idno ,ISNULL(Cityvia_Idno,0) Cityvia_Idno,
                    ISNULL(RM.FrmCity_Idno,0) FrmCity_Idno,isnull(Dist_Km,0) Dist_Km,isnull(DistMast_Idno,0) DistanceIdno,isnull(Con_Size,0) ConSize,isnull(Con_Weight,0) ConWeight,isnull(ConSize_ID,0) ConSizeID,Isnull(RM.Item_Weight,0.00)Item_Weight,IsWeight, ISNULL(ItemRate_KM,0) ItemRate_KM
                    from tblratemast RM left Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = RM.FrmCity_Idno 
                    left Join  tblCityMaster CM2  ON CM2.city_Idno  = RM.Cityvia_Idno  where RM.Item_Idno= " + Item_Idno + " and RM.FrmCity_Idno= CASE WHEN " + FrmCity_Idno + "!= 0 THEN " + FrmCity_Idno + " ELSE RM.FrmCity_Idno END and RM.Rate_Type= '" + RateType + "' and RM.ToCity_Idno= CASE WHEN " + tocity + "!= 0 THEN " + tocity + " ELSE RM.ToCity_Idno END order by RM.Rate_Date desc";
            }
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable SelectDBDataExport(Int64 Item_Idno, Int64 FrmCity_Idno, string RateType, string con)
        {
            string str = string.Empty;
            str = @"SELECT CONVERT(Varchar,Rate_Date,105) 'Rate Date', ISNULL(IM.Item_Name,'') 'Product Name', ISNULL(CM.City_Name,'') 'City Name', ISNULL(cm1.City_Name,'') 
                    'City From Name' ,ISNULL(CM2.City_Name,'') 'City Via',ISNULL(Item_Rate,0) 'Item Rate',ISNULL(Rate_Type,0) 'Rate Type',ISNULL(Item_WghtRate,0) 'Item Weight Rate',ISNULL(QtyShrtg_Limit,0) 
                    'Qty Shortage Limit',ISNULL(QtyShrtg_Rate,0)'Qty Shortage Rate',ISNULL(WghtShrtg_Limit,0) 'Weight Shortage Limit',ISNULL(WghtShrtg_Rate,0) 
                    'Weight Shortage Rate', ISNULL(Item_Rate2,0) 'Item Rate2', ISNULL(Item_Rate3,0) 'Item Rate3',ISNULL(ItemRate_Type,0) 'ItemRate Type',ISNULL(Dist_Km,0) 
                    'Distance (KM)' FROM tblratemast RM  inner join ItemMast IM on IM.Item_Idno=RM.Item_Idno Inner Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno 
                     Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = RM.FrmCity_Idno
                     Inner Join  tblCityMaster CM2  ON CM2.city_Idno  = RM.Cityvia_Idno where RM.Item_Idno= " + Item_Idno + " and RM.FrmCity_Idno=" + FrmCity_Idno + " and RM.Rate_Type='" + RateType + "' order by RM.Rate_Date";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable SelectDBDataExportContWise(Int64 Item_Idno, Int64 FrmCity_Idno, string RateType, string con)
        {
            string str = string.Empty;
            str = @"SELECT CONVERT(Varchar,Rate_Date,105) 'Rate Date', ISNULL(IM.Item_Name,'') 'Product Name', ISNULL(CM.City_Name,'') 'City Name', ISNULL(cm1.City_Name,'') 
                    'City From Name' ,ISNULL(CM2.City_Name,'') 'City Via',ISNULL(Item_Rate,0) 'Item Rate',ISNULL(Rate_Type,0) 'Rate Type',ISNULL(Item_WghtRate,0) 'Item Weight Rate',ISNULL(QtyShrtg_Limit,0) 
                    'Qty Shortage Limit',ISNULL(QtyShrtg_Rate,0)'Qty Shortage Rate',ISNULL(WghtShrtg_Limit,0) 'Weight Shortage Limit',ISNULL(WghtShrtg_Rate,0) 
                    'Weight Shortage Rate', ISNULL(Item_Rate2,0) 'Item Rate2', ISNULL(Item_Rate3,0) 'Item Rate3',ISNULL(ItemRate_Type,0) 'ItemRate Type',ISNULL(Dist_Km,0) 
                    'Distance (KM)', isnull(Con_Size,0) ConSize,isnull(Con_Weight,0) ConWeight
                     FROM tblratemast RM  inner join ItemMast IM on IM.Item_Idno=RM.Item_Idno Inner Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno 
                     Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = RM.FrmCity_Idno
                     Inner Join  tblCityMaster CM2  ON CM2.city_Idno  = RM.Cityvia_Idno where RM.Item_Idno= " + Item_Idno + " and RM.FrmCity_Idno=" + FrmCity_Idno + " and RM.Rate_Type='" + RateType + "' order by RM.Rate_Date";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        // by this is used to delete record by rateidno basis
        public int DeleteExistingRecord(Int32 Rate_Idno)
        {
            int intValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        IList<tblRateMast> objMast = (from mast in db.tblRateMasts where mast.Rate_Idno == Rate_Idno select mast).ToList();
                        if (objMast != null)
                        {
                            foreach (tblRateMast dr in objMast)
                            {
                                db.DeleteObject(dr);
                                db.SaveChanges();
                            }
                            dbTran.Commit();
                        }
                        intValue = 1;
                    }
                    catch (Exception Ex)
                    {
                        if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                        {
                            dbTran.Rollback();
                            intValue = -1;
                        }
                    }
                }
            }
            return intValue;
        }
        // end

        public int Delete(int intItemIdno, int intFrmCity, string RateType, int ToCity)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    IList<tblRateMast> objMast = (from mast in db.tblRateMasts
                                                  where mast.Item_Idno == intItemIdno && mast.Rate_Type == RateType
                                                  && mast.FrmCity_Idno == intFrmCity && mast.ToCity_Idno == ToCity
                                                  select mast).ToList();
                    if (objMast != null)
                    {
                        foreach (tblRateMast dr in objMast)
                        {
                            db.DeleteObject(dr);
                            db.SaveChanges();

                        }
                    }
                    intValue = 1;

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



        public IList CheckItemExistInOtherMaster(Int32 ItemIdno, Int32 FromCity, Int32 CityTo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj3 in db.tblQuatationDetls
                        join obj1 in db.tblQuatationHeads on obj3.QuHead_Idno equals obj1.QuHead_Idno
                        where obj3.Item_Idno == ItemIdno && obj1.FromCity_Idno == FromCity && obj1.ToCity_Idno == CityTo
                        select new { ItemGrp = obj3.Item_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.TblGrDetls
                           join obj2 in db.TblGrHeads on obj4.GrHead_Idno equals obj2.GR_Idno
                           where obj4.Item_Idno == ItemIdno && obj2.From_City == FromCity && obj2.To_City == CityTo
                           select new { ItemGrp = obj4.Item_Idno }
                          ).ToList();
            }
        }

        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }

        public int InsertPartyItemRate(tblPartyRateMast objParty, bool IsWeight)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                try
                {
                    if (IsWeight == false)
                    {
                        if (IsExistPartyItemRate(objParty.Rate_Date, objParty.Loc_Idno, objParty.Party_Idno, objParty.Item_Idno, objParty.FrmCity_Idno, objParty.ToCity_Idno) == 0)
                        {
                            db.tblPartyRateMasts.AddObject(objParty);
                            db.SaveChanges();
                            return 1;

                        }
                        else
                            return 2;
                    }

                    else
                    {
                        if (IsExistPartyItemRate(objParty.Rate_Date, objParty.Loc_Idno, objParty.Party_Idno, objParty.Item_Idno, objParty.FrmCity_Idno, objParty.ToCity_Idno, objParty.Item_Weight) == 0)
                        {
                            db.tblPartyRateMasts.AddObject(objParty);
                            db.SaveChanges();
                            return 1;
                        }
                        else
                            return 2;
                    }
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public int UpdatePartyItemRate(tblPartyRateMast objParty, Int64 PRateID, bool IsWeight)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    if (IsWeight == false)
                    {
                        if (IsExistPartyItemRate(objParty.Rate_Date, objParty.Loc_Idno, objParty.Party_Idno, objParty.Item_Idno, objParty.FrmCity_Idno, objParty.ToCity_Idno, PRateID) == 0)
                        {
                            tblPartyRateMast obj = (from PRM in db.tblPartyRateMasts where PRM.PRate_Idno == PRateID select PRM).SingleOrDefault();
                            obj.Party_Idno = objParty.Party_Idno;
                            obj.Item_Idno = objParty.Item_Idno;
                            obj.Loc_Idno = objParty.Loc_Idno;
                            obj.Item_Rate = objParty.Item_Rate;
                            obj.Date_Modified = objParty.Date_Modified;
                            obj.FrmCity_Idno = objParty.FrmCity_Idno;
                            obj.ToCity_Idno = objParty.ToCity_Idno;
                            obj.Emp_Idno = objParty.Emp_Idno;
                            obj.Rate_Date = objParty.Rate_Date;
                            db.SaveChanges();
                            return 1;
                        }
                        else
                            return 2;
                    }
                    else
                    {
                        if (IsExistPartyItemRate(objParty.Rate_Date, objParty.Loc_Idno, objParty.Party_Idno, objParty.Item_Idno, objParty.FrmCity_Idno, objParty.ToCity_Idno, PRateID, objParty.Item_Weight) == 0)
                        {
                            tblPartyRateMast obj = (from PRM in db.tblPartyRateMasts where PRM.PRate_Idno == PRateID select PRM).SingleOrDefault();
                            obj.Party_Idno = objParty.Party_Idno;
                            obj.Item_Idno = objParty.Item_Idno;
                            obj.Loc_Idno = objParty.Loc_Idno;
                            obj.Item_Rate = objParty.Item_Rate;
                            obj.Date_Modified = objParty.Date_Modified;
                            obj.FrmCity_Idno = objParty.FrmCity_Idno;
                            obj.ToCity_Idno = objParty.ToCity_Idno;
                            obj.Emp_Idno = objParty.Emp_Idno;
                            obj.Rate_Date = objParty.Rate_Date;
                            obj.Item_Weight = objParty.Item_Weight;
                            db.SaveChanges();
                            return 1;
                        }
                        else
                            return 2;

                    }

                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
        public int DeletePartyItemRate(Int64 PRateID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblPartyRateMast obj = (from PRM in db.tblPartyRateMasts where PRM.PRate_Idno == PRateID select PRM).SingleOrDefault();
                    db.tblPartyRateMasts.DeleteObject(obj);
                    db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public Int64 IsExistPartyItemRate(DateTime? date, Int64 Loc_id, Int64 PartyId, Int64 Item_id, Int64 FromCity, Int64 ToCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblPartyRateMast obj = new tblPartyRateMast();
                var lst = (from PRM in db.tblPartyRateMasts where PRM.Item_Idno == Item_id && PRM.Loc_Idno == Loc_id && PRM.Party_Idno == PartyId && PRM.Rate_Date == date && PRM.ToCity_Idno == ToCity && PRM.FrmCity_Idno == FromCity select PRM).SingleOrDefault();
                if (lst != null)
                {
                    return lst.PRate_Idno;
                }
                else
                {
                    return 0;
                }
            }
        }

        //For Weight wise rate Insert for (Rate Master Party wise)
        public Int64 IsExistPartyItemRate(DateTime? date, Int64 Loc_id, Int64 PartyId, Int64 Item_id, Int64 FromCity, Int64 ToCity, Decimal? Weight)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblPartyRateMast obj = new tblPartyRateMast();
                var lst = (from PRM in db.tblPartyRateMasts where PRM.Item_Idno == Item_id && PRM.Loc_Idno == Loc_id && PRM.Party_Idno == PartyId && PRM.Rate_Date == date && PRM.ToCity_Idno == ToCity && PRM.FrmCity_Idno == FromCity && PRM.Item_Weight == Weight select PRM).SingleOrDefault();
                if (lst != null)
                {
                    return lst.PRate_Idno;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Int64 IsExistPartyItemRate(DateTime? date, Int64 Loc_id, Int64 PartyId, Int64 Item_id, Int64 FromCity, Int64 ToCity, Int64 PRateId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblPartyRateMast obj = new tblPartyRateMast();
                var lst = (from PRM in db.tblPartyRateMasts where PRM.Item_Idno == Item_id && PRM.Loc_Idno == Loc_id && PRM.Party_Idno == PartyId && PRM.Rate_Date == date && PRM.ToCity_Idno == ToCity && PRM.FrmCity_Idno == FromCity && PRM.Party_Idno != PRateId select PRM).SingleOrDefault();
                if (lst == null)
                {
                    return 0;
                }
                else
                {
                    return PRateId;
                }
            }
        }


        //For Weight wise rate update
        public Int64 IsExistPartyItemRate(DateTime? date, Int64 Loc_id, Int64 PartyId, Int64 Item_id, Int64 FromCity, Int64 ToCity, Int64 PRateId, Decimal? Weight)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblPartyRateMast obj = new tblPartyRateMast();
                var lst = (from PRM in db.tblPartyRateMasts where PRM.Item_Idno == Item_id && PRM.Loc_Idno == Loc_id && PRM.Party_Idno == PartyId && PRM.Rate_Date == date && PRM.ToCity_Idno == ToCity && PRM.FrmCity_Idno == FromCity && PRM.Party_Idno != PRateId && PRM.Item_Weight == Weight select PRM).SingleOrDefault();
                if (lst == null)
                {
                    return 0;
                }
                else
                {
                    return PRateId;
                }
            }
        }

        public DataTable SelectPartyItemRateList(Int64 PartyID, Int64 Loc_id, Int64 Item_Id, string con)
        {
            string str = string.Empty;
            str = @"select PRM.PRate_Idno,IM.Item_Idno,IM.Item_Name,PRM.FrmCity_Idno as FromCityId,CM.City_Name as FromCity,PRM.ToCity_Idno,CM2.City_Name as ToCityName, PRM.Loc_Idno,CM1.City_Name as LocationName,PRM.Item_Rate,PRM.Party_Idno,AM.Acnt_Name,PRM.Rate_Date,Isnull(PRM.Item_Weight,0.00)Item_Weight,IsWeight
                    FROM tblPartyRateMast PRM  
                    inner join ItemMast IM on IM.Item_Idno=PRM.Item_Idno 
                    Inner Join tblCityMaster CM ON CM.city_Idno  = PRM.FrmCity_Idno 
                    Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = PRM.Loc_Idno
                    Inner Join  tblCityMaster CM2  ON CM2.city_Idno  = PRM.ToCity_Idno 
                    INNER JOIN AcntMast AM ON PRM.Party_Idno=AM.Acnt_Idno
                    where PRM.Item_Idno='" + Item_Id + "' and PRM.Loc_Idno='" + Loc_id + "' and PRM.Party_Idno='" + PartyID + "' order by PRM.Rate_Date  desc";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable SelectPartyItemRateListReport(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int32 PrtyIdno, Int64 Location, Int64 ItemIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@Location", Location);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@DtFrom", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@DtTo", dtToDate);
            objSqlPara[5] = new SqlParameter("@PartyIdno", PrtyIdno);
            objSqlPara[6] = new SqlParameter("@ItemIdno", ItemIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spRateMasterReport", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }


        public DataSet SelectPartyItemRate(Int64 PRateID, string con)
        {
            string str = string.Empty;
            str = @"select PRM.PRate_Idno,IM.Item_Idno,IM.Item_Name,PRM.FrmCity_Idno as FromCityId,CM.City_Name as FromCity,PRM.ToCity_Idno,CM2.City_Name as ToCityName, PRM.Loc_Idno,CM1.City_Name as LocationName,PRM.Item_Rate,PRM.Party_Idno,AM.Acnt_Name,PRM.Rate_Date,Isnull(PRM.Item_Weight,0.00)Item_Weight,IsWeight,PRM.Year_Idno
                    FROM tblPartyRateMast PRM  
                    inner join ItemMast IM on IM.Item_Idno=PRM.Item_Idno 
                    Inner Join tblCityMaster CM ON CM.city_Idno  = PRM.FrmCity_Idno 
                    Inner Join  tblCityMaster CM1  ON CM1.city_Idno  = PRM.Loc_Idno
                    Inner Join  tblCityMaster CM2  ON CM2.city_Idno  = PRM.ToCity_Idno 
                    INNER JOIN AcntMast AM ON PRM.Party_Idno=AM.Acnt_Idno
                    where PRM.PRate_Idno='" + PRateID + "' order by PRM.Rate_Date";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }
    }
}
