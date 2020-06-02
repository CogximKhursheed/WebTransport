using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace WebTransport.DAL
{
    public class RateMastFTDAL
    {
        #region Select ...

        public DataTable SelectDBData(Int64 LorrytypIdno, Int64 FrmCity_Idno, string con)
        {
            string str = string.Empty;
            str = @"select isnull(RateMastFT_Idno,0) Rate_Idno,isnull(RateMastFT_Date,0) Rate_Date, LorryTyp_idno as  Lorry_Idno,Isnull(Tot_Frght,0) Item_Rate,ISNULL(ToCity_Idno,0) ToCity_Idno,
                    Isnull(CM.City_Name,'') City_Name ,Isnull(CM1.City_Name,'') City_FName,
                    ISNULL(RM.Loc_Idno,0) City_FIdno,RM.Year_Idno,lt.Lorry_Type as Lorry_Type from ratemastft RM 
                    Inner Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno 
                    Inner Join tblCityMaster CM1  ON CM1.city_Idno  = RM.Loc_Idno
                    Inner join LorryType as lt on lt.id=rm.LorryTyp_idno 
                     where RM.LorryTyp_idno= " + LorrytypIdno + " and RM.Loc_Idno=" + FrmCity_Idno + " order by RM.RateMastFT_Date";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable SelectDBDataExport(Int64 LorrytypIdno, Int64 FrmCity_Idno, string con)
        {
            string str = string.Empty;
            str = @"select CONVERT(Varchar,RateMastFT_Date,105) 'Date', Isnull(Tot_Frght,0) 'Freight Amount',
                    Isnull(CM.City_Name,'') 'ToCity' ,Isnull(CM1.City_Name,'') 'From City',
                    lt.Lorry_Type as 'Lorry Type' from ratemastft RM 
                    Inner Join tblCityMaster CM ON CM.city_Idno  = RM.ToCity_Idno 
                    Inner Join tblCityMaster CM1  ON CM1.city_Idno  = RM.Loc_Idno
                    Inner join LorryType as lt on lt.id=rm.LorryTyp_idno 
                     where RM.LorryTyp_idno= " + LorrytypIdno + " and RM.Loc_Idno=" + FrmCity_Idno + " order by RM.RateMastFT_Date";

            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        #endregion

        #region Function.....

        public IList BindLorryType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryTypes
                                    orderby obj.Lorry_Type
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_Type,
                                        Lorry_Idno = obj.Id
                                    }).ToList();
                return objLorryMast;
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

        #endregion

        #region Insert Delete......
        //Insert
        public bool Insert(DataTable dT, Int32 YearIdno)
        {
            bool value = true;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    for (int i = 0; i < dT.Rows.Count; i++)
                    {
                        RateMastFT obj = new RateMastFT();
                        obj.Year_Idno = YearIdno;
                        obj.Loc_Idno = Convert.ToInt32(dT.Rows[i]["City_FIdno"]);
                        obj.LorryTyp_idno = Convert.ToInt32(dT.Rows[i]["Lorry_Idno"]);
                        obj.RateMastFT_Date = Convert.ToDateTime(dT.Rows[i]["Rate_Date"]);
                        obj.ToCity_Idno = Convert.ToInt32(dT.Rows[i]["ToCity_Idno"]);
                        obj.Tot_Frght = Convert.ToDouble(dT.Rows[i]["Item_Rate"]);
                        obj.Status = true;
                        obj.Date_Added = DateTime.Now; obj.Date_Modified = DateTime.Now;
                        db.RateMastFTs.AddObject(obj);
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

        //Delete
        public int Delete(int intLorryTypIdno, int intFrmCity)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    IList<RateMastFT> objMast = (from mast in db.RateMastFTs
                                                  where mast.LorryTyp_idno == intLorryTypIdno
                                                  && mast.Loc_Idno == intFrmCity
                                                  select mast).ToList();
                    if (objMast != null)
                    {
                        foreach (RateMastFT dr in objMast)
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

        #endregion

    }
}
