using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;

namespace WebTransport.DAL
{
    public class TrackingDAL
    {  
        public List<LaneMaster> BindLaneName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<LaneMaster> objLANMast = new List<LaneMaster>();
                objLANMast = (from obj in db.LaneMasters
                              orderby obj.Lane_Name
                              select obj).ToList();
                return objLANMast;
            }
        }
        public List<tblCityMaster> BindAllToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public List<tblCompMast> BindCompany()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCompMast> objtblCompMast = new List<tblCompMast>();
                objtblCompMast = (from obj in db.tblCompMasts
                                  orderby obj.Comp_Name
                                  select obj).ToList();
                return objtblCompMast;
            }
        }
        public List<LorryMast> BindLorry()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<LorryMast> objLorryMast = new List<LorryMast>();
                objLorryMast = (from obj in db.LorryMasts
                                orderby obj.Lorry_No
                                select obj).ToList();
                return objLorryMast;
            }
        }
        public List<tblCityMaster> BindFromLoc()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    where obj.AsLocation == true
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public Int64 Insert(Int64 trackingIdno , Int64 VehicleNo, string Date, Int64 LaneIdno, Int64 FromCityIdno, Int64 ToCityIdno, Int64 CompName, Int64 FromLoc, DataTable dtDetail)
        {
            Int64 intValue = 0; 
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TrackingHead objTrackingHead = (from es in db.TrackingHeads where es.TrackingHead_Idno == trackingIdno && es.Vehicle_No == VehicleNo select es).FirstOrDefault();
                    if (objTrackingHead == null)
                    {
                        objTrackingHead = new TrackingHead();
                        objTrackingHead.Vehicle_No = VehicleNo;
                        objTrackingHead.Tracking_Date = Convert.ToDateTime(Date);
                        objTrackingHead.Comp_Id = CompName;
                        objTrackingHead.From_Loc = FromLoc;
                        objTrackingHead.Lane_Id = LaneIdno;
                        objTrackingHead.From_CityIdno = FromCityIdno;
                        objTrackingHead.To_CityIdno = ToCityIdno;
                        db.TrackingHeads.AddObject(objTrackingHead);
                        db.SaveChanges();
                        intValue = objTrackingHead.TrackingHead_Idno;
                        if (intValue > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                TrackingDetl objTrackingDetl = new TrackingDetl();
                                objTrackingDetl.TrackingHead_Idno = intValue;
                                objTrackingDetl.leg = Convert.ToString(row["Leg"]);
                                objTrackingDetl.eta = Convert.ToString(row["ETA"]);
                                objTrackingDetl.ata = Convert.ToString(row["ATA"]);
                                objTrackingDetl.etd = Convert.ToString(row["ETD"]);
                                objTrackingDetl.atd = Convert.ToString(row["ATD"]);
                                objTrackingDetl.tat_in_hrs = Convert.ToString(row["TAT_in_hrs"]);
                                objTrackingDetl.delay_in_hrs = Convert.ToString(row["Delay_in_hrs"]);
                                objTrackingDetl.remarks = Convert.ToString(row["Remarks"]);
                                objTrackingDetl.FromCity = Convert.ToInt64(row["FromCityIdno"]);
                                objTrackingDetl.ToCity = Convert.ToInt64(row["ToCItyIdno"]);
                                db.TrackingDetls.AddObject(objTrackingDetl);
                            }
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        intValue = -1;
                    }
                }
                catch
                {
                    intValue = 0;
                }
            }
            return intValue;
        }
        public Int64 CountALL()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from TH in db.TrackingHeads
                       select TH.TrackingHead_Idno).Count();
            }
        }
        public IList search(Int32 yearid,  DateTime? dtfrom, DateTime? dtto, Int64 vehicleno, Int64 fromloc, Int64 compname, Int32 Lane, Int32 FromCity , Int32 ToCity, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from TH in db.TrackingHeads
                           join cifrom in db.tblCityMasters on TH.From_CityIdno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on TH.To_CityIdno equals cito.City_Idno

                           join LM in db.LaneMasters on TH.Lane_Id equals LM.Lane_Idno

                           join cm in db.tblCompMasts on TH.Comp_Id equals cm.Comp_Idno
                           join locfrom in db.tblCityMasters on TH.From_Loc equals locfrom.City_Idno
                           join vehno in db.LorryMasts on TH.Vehicle_No equals vehno.Lorry_Idno

                           select new
                           {
                               TH.Vehicle_No,
                               TH.Tracking_Date,
                               TH.Comp_Id,
                               TH.From_Loc,
                               TH.Lane_Id,
                               LM.Lane_Name,
                               TH.TrackingHead_Idno,
                               FromCity = cifrom.City_Name,
                               TH.From_CityIdno,
                               ToCity=cito.City_Name,
                               TH.To_CityIdno,
                               cm.Comp_Name,
                              LocFrom= locfrom.City_Name,
                               vehno.Lorry_No,
                           }).ToList();
                if (vehicleno > 0)
                {
                    lst = lst.Where(l => l.Vehicle_No == vehicleno).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Tracking_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Tracking_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (fromloc > 0)
                {
                    lst = lst.Where(l => l.From_Loc == fromloc).ToList();
                }
                //if (fromloc != "")
                //{
                //    lst = lst.Where(l => l.From_Loc == fromloc).ToList();
                //}
                if (compname > 0)
                {
                    lst = lst.Where(l => l.Comp_Id == compname).ToList();
                }
                if (Lane > 0)
                {
                    lst = lst.Where(l => l.Lane_Id == Lane).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.From_CityIdno == FromCity).ToList();
                }
                if (ToCity > 0)
                {
                    lst = lst.Where(l => l.To_CityIdno == ToCity).ToList();
                }
                return lst;
            }
        }
        public int Delete(Int64 HeadId, Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TrackingHead TH = db.TrackingHeads.Where(h => h.TrackingHead_Idno == HeadId).FirstOrDefault();
                List<TrackingDetl> TD = db.TrackingDetls.Where(d => d.TrackingHead_Idno == HeadId).ToList();

                if (TH != null)
                {
                    foreach (var d in TD)
                    {
                        db.TrackingDetls.DeleteObject(d);

                        db.SaveChanges();
                    }
                    db.TrackingHeads.DeleteObject(TH);
                  
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
        public Int32 CheckBilled(Int64 Id, string con)
        {
            Int32 value = 0;
            SqlParameter[] objSqlParameter = new SqlParameter[2];
            objSqlParameter[0] = new SqlParameter("@Action", "SelectBilled");
            objSqlParameter[1] = new SqlParameter("@Id", Id);
            value = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spTracking", objSqlParameter));
            return value;
        }
        public TrackingHead selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TrackingHeads.Where(h => h.TrackingHead_Idno == HeadId).FirstOrDefault();
            }
        }
        public TrackingDetl selectDetel(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TrackingDetls.Where(h => h.TrackingHead_Idno == HeadId).FirstOrDefault();
            }
        }
        public DataTable selectDetls(string con, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spTracking", objSqlPara);
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
        public Int64 Update(Int64 trackingIdno, Int64 VehicleNo, string Date, Int64 LaneIdno, Int64 FromCityIdno, Int64 ToCityIdno, Int64 CompName, Int64 FromLoc,DataTable dtDetail)
        {
            Int64 intValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TrackingHead objTrackingHead = (from es in db.TrackingHeads where es.TrackingHead_Idno == trackingIdno && es.Vehicle_No == VehicleNo select es).FirstOrDefault();
                    
                    if (objTrackingHead == null)
                    {
                        objTrackingHead = (from tran in db.TrackingHeads
                                           where tran.TrackingHead_Idno == trackingIdno
                                           select tran).FirstOrDefault();
                    }
                    else
                    {
                        if (objTrackingHead != null)
                        {
                            objTrackingHead.Vehicle_No = VehicleNo;
                            objTrackingHead.Tracking_Date = Convert.ToDateTime(Date);
                            objTrackingHead.Comp_Id = CompName;
                            objTrackingHead.From_Loc = FromLoc;
                            objTrackingHead.Lane_Id = LaneIdno;
                            objTrackingHead.From_CityIdno = FromCityIdno;
                            objTrackingHead.To_CityIdno = ToCityIdno;
                            db.SaveChanges();
                            intValue = objTrackingHead.TrackingHead_Idno;
                            if (intValue > 0)
                            {
                                List<TrackingDetl> lstNEstDetl = db.TrackingDetls.Where(obj => obj.TrackingHead_Idno == intValue).ToList();
                                if (lstNEstDetl.Count > 0)
                                {
                                    foreach (TrackingDetl obj in lstNEstDetl)
                                    {
                                        db.TrackingDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    TrackingDetl objNEstDetl = new TrackingDetl();
                                    objNEstDetl.TrackingHead_Idno = intValue;
                                    objNEstDetl.leg = Convert.ToString(row["Leg"]);
                                    objNEstDetl.eta = Convert.ToString(row["ETA"]);
                                    objNEstDetl.ata = Convert.ToString(row["ATA"]);
                                    objNEstDetl.etd = Convert.ToString(row["ETD"]);
                                    objNEstDetl.atd = Convert.ToString(row["ATD"]);
                                    objNEstDetl.tat_in_hrs = Convert.ToString(row["TAT_in_hrs"]);
                                    objNEstDetl.delay_in_hrs = Convert.ToString(row["Delay_in_hrs"]);
                                    objNEstDetl.remarks = Convert.ToString(row["Remarks"]);
                                    objNEstDetl.FromCity = Convert.ToInt64(row["FromCityIdno"]);
                                    objNEstDetl.ToCity = Convert.ToInt64(row["ToCItyIdno"]);
                                    db.TrackingDetls.AddObject(objNEstDetl);
                                }
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            intValue = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    intValue = 0;
                }
            }
            return intValue;
        }

        // uses for validation and excel for all tracking details...
        //public DataTable ValidateEstNo1(String vehno, String compname, Int64 Idno, string con)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = @"SELECT TrackingHead_Idno FROM trackinghead WHERE  Vehicle_No ='" + vehno + "' and Comp_Name='" + compname + "' and TrackingHead_Idno<>" + Idno ;

        //    DataSet dsTemp = SqlHelper.ExecuteDataset(con, CommandType.Text, strSQL);
        //    DataTable dtTemp = new DataTable();
        //    if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
        //    {
        //        dtTemp = dsTemp.Tables[0];
        //    }
        //    return dtTemp;
        //}



        //public DataTable selectExcelDetails(string con)
        //{
        //    SqlParameter[] objSqlPara = new SqlParameter[2];
        //    objSqlPara[0] = new SqlParameter("@Action", "SelectExcel");
        //    DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spTracking", objSqlPara);

        //    DataTable objDtTemp = new DataTable();
        //    if (objDsTemp.Tables.Count > 0)
        //    {
        //        if (objDsTemp.Tables[0].Rows.Count > 0)
        //        {
        //            objDtTemp = objDsTemp.Tables[0];
        //        }
        //    }
        //    return objDtTemp;
        //}
    }
}



