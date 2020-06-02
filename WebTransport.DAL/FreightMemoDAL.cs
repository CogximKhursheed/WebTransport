using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class FreightMemoDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from ItemMast
        /// </summary>
        /// <returns></returns>
        /// 

        public Int64 GetRcptNo(Int64 YearIdno,Int32 Tocity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Maxxno = 0;
                Maxxno = Convert.ToInt64((from obj in db.tblFreightMemoes where obj.Year_Idno == YearIdno && obj.ToCity_Idno == Tocity select obj.Rcpt_No).Max());
                Maxxno = Maxxno + 1;
                return Maxxno;
            }

        }
        public DataTable selectGrDetails(string Action, DateTime dtFrmDate, DateTime dtToDate, string StrGrno, string con, Int32 ToCity)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@GRNo", StrGrno);
            objSqlPara[2] = new SqlParameter("@DateFrom", dtFrmDate);
            objSqlPara[3] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[4] = new SqlParameter("@ToCityIdno", ToCity);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spFreightMemo", objSqlPara);
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

        public DataTable SelectGRDetailInRcpt(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetailInRcpt");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spFreightMemo", objSqlPara);
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


        //SELECT  ROW_NUMBER() OVER (ORDER BY CONVERT(DATETIME,FM.Rcpt_Date,105)) as SNo,
        //        ISNULL(FM.Rcpt_Date,'''') AS Rcpt_Date,ISNULL(FM.Recpt_No,'''') AS Recpt_No,
        //        ISNULL(CM.Name,'''') AS Name,ISNULL(GH.Gr_No,0) AS GR_No,
        //        ISNULL(AM.Acnt_Name,0) AS Receiver_Name,ISNULL(FM.Tot_Qty,0) AS Tot_Qty,
        //        ISNULL(FM.Tot_Weight,0) AS Tot_Weight,
        //        CONVERT(NUMERIC(25,2),ISNULL(FM.Freight_Amnt,0)) AS Freight_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Service_Amnt,0)) AS Service_Amnt,   
        //        CONVERT(NUMERIC(25,2),ISNULL(FM.Labour_Amnt,0)) AS Labour_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Delivery_Amnt,0)) AS Delivery_Amnt,
        //        CONVERT(NUMERIC(25,2),ISNULL(FM.Octrai_Amnt,0)) AS Octrai_Amnt,CONVERT(NUMERIC(25,2),ISNULL(FM.Damage_Amnt,0)) AS Damage_Amnt,
        //        CONVERT(NUMERIC(25,2),ISNULL(FM.Net_Amnt,0)) AS Net_Amnt,ISNULL(FM.Id,0) AS Id


        public DataSet SearchFreightReport(DateTime? DtFrom, DateTime? DtTo, Int32 ToCity, Int32 YearIdno,Int32  IuserIdno,string conString)
        {
            DataSet Dsobj = new DataSet();
            SqlParameter[] objSqlParameter =new SqlParameter[6];
            objSqlParameter[0]=new SqlParameter ("@Action","SelectFreightMemo");
            objSqlParameter[1]=new SqlParameter ("@DateFrom",DtFrom);
            objSqlParameter[2]=new SqlParameter ("@DateTo",DtTo);
            objSqlParameter[3]=new SqlParameter ("@ToCityIdno",ToCity);
            objSqlParameter[4]=new SqlParameter ("@UserIdno",IuserIdno);
             objSqlParameter[5]=new SqlParameter ("@YearIdno",YearIdno);
             Dsobj=SqlHelper.ExecuteDataset(conString,CommandType.StoredProcedure,"spFreightMemoRep",objSqlParameter);
           return Dsobj;
        }
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
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from obj in db.tblFreightMemoes
                           join CM in db.tblCityMasters on obj.ToCity_Idno equals CM.City_Idno
                           select  obj.FMemo_Idno ).Count();

                return Count;
            }

        }
        public IList Search(DateTime? DtFrom, DateTime? DtTo, Int32 RcptNo,  Int32 ToCity, Int32 YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from obj in db.tblFreightMemoes
                           join CM in db.tblCityMasters on obj.ToCity_Idno equals CM.City_Idno
                           select
                              new
                              {
                                  obj.Rcpt_Date,
                                  obj.Rcpt_No,
                                  obj.ToCity_Idno,
                                  obj.Year_Idno,
                                  CM.City_Name,
                                  obj.FMemo_Idno,
                                  obj.Net_Amnt
                              }
                    ).ToList();
                if (DtFrom != null)
                {
                    lst = (from l in lst where l.Rcpt_Date >= DtFrom select l).ToList();
                }
                if (DtTo != null)
                {
                    lst = (from l in lst where l.Rcpt_Date <= DtTo select l).ToList();
                }
                if (ToCity > 0)
                {
                    lst = (from l in lst where l.ToCity_Idno == ToCity select l).ToList();
                }
                if (RcptNo > 0)
                {
                    lst = (from l in lst where l.Rcpt_No == RcptNo select l).ToList();
                }
                
                if (YearIdno > 0)
                {
                    lst = (from l in lst where l.Year_Idno == YearIdno select l).ToList();
                }
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
        public List<UOMMast> SelectUnitType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<UOMMast> lst = null;
                lst = (from LM in db.UOMMasts orderby LM.UOM_Name ascending select LM).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <param name="strItemName"></param>
        /// <returns></returns>
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
        public tblFreightMemo SelectById(int intHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblFreightMemoes
                        where mast.FMemo_Idno == intHeadIdno
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
        public Int64 Insert(tblFreightMemo obj)
        {
            Int64 intValue = 0;
            try
            {
                tblFreightMemo CHead = new tblFreightMemo();
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    db.tblFreightMemoes.AddObject(obj);
                        db.SaveChanges();
                        intValue = obj.FMemo_Idno;
                    
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
        public Int64 Update(tblFreightMemo obj,Int64 HeadIdno)
        {
            Int64 intValue = 0;
            //Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblFreightMemo objItemMast = (from mast in db.tblFreightMemoes
                                            where mast.FMemo_Idno == HeadIdno
                                            select mast).FirstOrDefault();
                    if (objItemMast != null)
                    {
                        objItemMast.Rcpt_Date = obj.Rcpt_Date;
                        objItemMast.Rcpt_No = obj.Rcpt_No;
                        objItemMast.Gr_No = obj.Gr_No;
                        objItemMast.ToCity_Idno = obj.ToCity_Idno;
                        objItemMast.Receiver_Idno = obj.Receiver_Idno;
                        objItemMast.Tot_Qty = obj.Tot_Qty;
                        objItemMast.Tot_Weight = obj.Tot_Weight;
                        objItemMast.Freight_Amnt = obj.Freight_Amnt;
                        objItemMast.Service_Amnt = obj.Service_Amnt;
                        objItemMast.Labour_Amnt = obj.Labour_Amnt;
                        objItemMast.Delivery_Amnt = obj.Delivery_Amnt;
                        objItemMast.Octrai_Amnt = obj.Octrai_Amnt;
                        objItemMast.Damage_Amnt = obj.Damage_Amnt;
                        objItemMast.Net_Amnt = obj.Net_Amnt;
                        objItemMast.GR_Idno = obj.GR_Idno;
                        objItemMast.Year_Idno = obj.Year_Idno;
                        objItemMast.Remarks = obj.Remarks;
                        db.SaveChanges();
                            intValue = objItemMast.FMemo_Idno;
                       // }
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

        public int DeleteALL(Int64 HeadIdno)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {

                        tblFreightMemo objTransHead = (from objHead in db.tblFreightMemoes
                                                       where objHead.FMemo_Idno == HeadIdno
                                                       select objHead).SingleOrDefault();
                        if (objTransHead != null)
                        {

                            db.tblFreightMemoes.DeleteObject(objTransHead);
                            db.SaveChanges();
                            Value = 1;
                            tScope.Complete();
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return Value;
        }
      

        #endregion
    }
}