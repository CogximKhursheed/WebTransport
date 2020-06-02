using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Web;

namespace WebTransport.DAL
{
    public class ChallanConfirmationDAL
    {

        public tblUserPref SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                return (from obj in db.tblUserPrefs select obj).FirstOrDefault();
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters where cm.AsLocation==true orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }

        public Int64 Insert(tblChlnBookHead ChallanBookHead, List<tblChlnBookDetl> ChallanBookDetl)
        {
            Int64 ChallanBookHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblChlnBookHead RH = db.tblChlnBookHeads.Where(rh => rh.Chln_No == ChallanBookHead.Chln_No).FirstOrDefault();
                        if (RH != null)
                        {
                            ChallanBookHeadId = -1;
                        }
                        else
                        {
                            db.tblChlnBookHeads.AddObject(ChallanBookHead);
                            db.SaveChanges();
                            ChallanBookHeadId = ChallanBookHead.Chln_Idno;
                            if (ChallanBookDetl.Count > 0)
                            {
                                foreach (tblChlnBookDetl rgd in ChallanBookDetl)
                                {
                                    rgd.ChlnBookHead_Idno = ChallanBookHeadId;
                                    db.tblChlnBookDetls.AddObject(rgd);
                                    db.SaveChanges();
                                }
                                dbTran.Commit();
                            }
                        }


                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return ChallanBookHeadId;
        }
 
        public Int64 Update(DataTable   ChallanBookDetl, Int32  Chlnidno)
        {
            Int64 ChallanBookHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {

                        List<tblChlnBookDetl> rcptdetl = db.tblChlnBookDetls.Where(rd => rd.ChlnBookHead_Idno == Chlnidno).ToList();
                        //foreach (tblChlnBookDetl rgd in rcptdetl)
                        //{
                        //    db.tblChlnBookDetls.DeleteObject(rgd);
                        //    db.SaveChanges();
                        //}
                        tblChlnBookDetl obj=new tblChlnBookDetl ();
                        foreach (DataRow rgd in ChallanBookDetl.Rows)
                        {
                            if (Convert.ToBoolean(rgd["Delivered"]) == true)
                            {
                                obj.Delivered = Convert.ToBoolean(rgd["Delivered"]);
                                obj.Delvry_Date = Convert.ToDateTime((rgd["Delvry_Date"]));
                                obj.Shrtg = Convert.ToBoolean(rgd["Shrtg"]);
                                obj.remark = Convert.ToString(rgd["remark"]);
                                db.SaveChanges();
                            }
                        }
                        dbTran.Commit();
                    }

                    catch
                    {
                        dbTran.Rollback();
                    }
                }

                return ChallanBookHeadId;
            }
        }

        public Int64 GetMaxNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from hd in db.tblChlnBookHeads select hd.Chln_No).Max() + 1;
                return Convert.ToInt64(max);
            }

        }

        public DataTable FetchChallanDetail(string ConString,int yearid,Int32 FromCityIdno,string GrType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[4];
                objSqlPara[0] = new SqlParameter("@Action", "SelectChlnDetByGR");
                objSqlPara[1] = new SqlParameter("@FromCityIdno",FromCityIdno);
                objSqlPara[2] = new SqlParameter("@YearIdno", yearid);
                objSqlPara[3] = new SqlParameter("@Grtype", GrType);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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
        }

        public DataTable FetchChallanDetailByChlnIdno(string ConString, Int32 ChlnIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "SelectChalanDetailsByChlnIdNo");
                objSqlPara[1] = new SqlParameter("@ChlnIdNo", ChlnIdno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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
        }

        public DataTable GetChallanDetailByChlnno(string ConString, Int32 yearid, Int32 FromCityIdno, Int32 Chlnno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[4];
                objSqlPara[0] = new SqlParameter("@Action", "SelectChlnDetByChlnNo");
                objSqlPara[1] = new SqlParameter("@FromCityIdno", FromCityIdno);
                objSqlPara[2] = new SqlParameter("@YearIdno", yearid);
                objSqlPara[3] = new SqlParameter("@ChlnNo", Chlnno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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
        }

        public tblChlnBookHead SelectByChlnConfrmByHeadId(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId).FirstOrDefault();
            }
        }
        
        public int DeleteChallanDetail(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnBookHead rgh = db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId).FirstOrDefault();
                List<tblChlnBookDetl> rgd = db.tblChlnBookDetls.Where(d => d.ChlnBookHead_Idno == HeadId).ToList();
                if (rgh != null)
                {
                    foreach (var d in rgd)
                    {
                        db.tblChlnBookDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblChlnBookHeads.DeleteObject(rgh);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public DataTable SelectchallanDetl(string ConString, string action, Int64 chlnIdno,string Grtype)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", action);
            objSqlPara[1] = new SqlParameter("@Id", chlnIdno);
            objSqlPara[2] = new SqlParameter("@GrType", Grtype);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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

        public DataTable SelectchallanDetlGR(string ConString, string action, Int64 chlnIdno, string GrNo,Int64 LocId,string GrType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", action);
            objSqlPara[1] = new SqlParameter("@Id", chlnIdno);
            objSqlPara[2] = new SqlParameter("@GRNO", GrNo);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", LocId);
            objSqlPara[4] = new SqlParameter("@Grtype", GrType);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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

        public DataTable SelectchlnDetlGRwise(string ConString, string action, string GrNo,Int32 YearIdno,Int64 FromCityIdno,string GrType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", action);
            objSqlPara[1] = new SqlParameter("@GRNO", GrNo);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", FromCityIdno);
            objSqlPara[4] = new SqlParameter("@Grtype", GrType);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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

        public DataTable GrShortageDetails(string ConString, string action, String GrIdno,Int32 YearIdno,string Grtype)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", action);
            objSqlPara[1] = new SqlParameter("@StrGrIdno", GrIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[3] = new SqlParameter("@Grtype", Grtype);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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

        public bool UpdateDlvryStatus(DataTable Dttemp)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (Dttemp != null && Dttemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dttemp.Rows.Count; i++)
                        {
                            //if (Convert.ToBoolean(Dttemp.Rows[i]["Delivered"]) == true)
                            //{
                            Int32 GrIdno = Convert.ToInt32(Dttemp.Rows[i]["Gr_Idno"]);
                            List<tblChlnBookDetl> ChlnDetl = db.tblChlnBookDetls.Where(rd => rd.GR_Idno == GrIdno).ToList();
                            foreach (tblChlnBookDetl obj in ChlnDetl)
                            {
                                obj.Delivered = Convert.ToBoolean((Dttemp.Rows[i]["Delivered"]));
                                if (Convert.IsDBNull(Dttemp.Rows[i]["Delvry_Date"]))
                                {
                                    obj.Delvry_Date = null;
                                }
                                else
                                {
                                    obj.Delvry_Date = Convert.ToDateTime(Dttemp.Rows[i]["Delvry_Date"]);
                                }

                                obj.remark = Convert.ToString(Dttemp.Rows[i]["remark"]);
                                obj.Shrtg = Convert.ToBoolean(Dttemp.Rows[i]["Shrtg"]);
                                db.SaveChanges();
                            }

                            //}
                        }
                    }
                    else
                    {
                        return true;
                    }
                }

                return true;
            }
            catch(Exception Ex)
            {

                return false;
            }
        }

        public Boolean UpdateGrHeadForKm( string ConString, Int64 GrId, double Frmkm, double Tokm, double Totkm)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {

                    TblGrHead objtblGrhead = (from obj in db.TblGrHeads where obj.GR_Idno == GrId select obj).FirstOrDefault();

                    if (objtblGrhead != null)
                    {
                        objtblGrhead.From_KM = Frmkm;
                        objtblGrhead.To_KM = Tokm;
                        objtblGrhead.Tot_KM = Totkm;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            catch (Exception EX)
            {
                return false;
            }
        }

        public Boolean UpdateShortingForUpdate(Int32 ChlnIdno, string ConString, Int64 GrId, string Grtype)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var lst = (from obj in db.tblChlnBookDetls where obj.ChlnBookHead_Idno == ChlnIdno && obj.GR_Idno == GrId select obj.GR_Idno).Distinct().ToList();

                    if (Grtype == "GR")
                    {
                        IList<TblGrDetl> objTblGrDetl = (from obj in db.TblGrDetls where lst.Contains(obj.GrHead_Idno) select obj).ToList();
                        foreach (var L in objTblGrDetl)
                        {
                            L.shortage_Amount = 0;
                            L.shortage = 0;
                            L.shortage_Diff = 0;
                            L.Shortage_Qty = 0;
                            L.UnloadWeight = 0;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        IList<tblGrRetailerDetl> objTblGrretDetl = (from obj in db.tblGrRetailerDetls where lst.Contains(obj.GRRetHead_Idno) select obj).ToList();
                        foreach (var L in objTblGrretDetl)
                        {
                            L.shortage_Amount = 0;
                            L.shortage = 0;
                            L.shortage_Diff = 0;
                            L.Shortage_Qty = 0;
                            L.UnloadWeight = 0;
                            db.SaveChanges();
                        }

                    }
                    IList<tblChlnBookDetl> objtblChlnBookDetl = (from obj in db.tblChlnBookDetls where obj.ChlnBookHead_Idno == ChlnIdno && obj.GR_Idno == GrId select obj).ToList();
                    foreach (var L in objtblChlnBookDetl)
                    {
                        L.Shrtg = false;
                        db.SaveChanges();
                    }
                    return true;
                }

            }
            catch (Exception EX)
            {
                return false;
            }
        }

        public bool UpdateShorting(DataTable DtTemp, string Con,string Grtype)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {

                        foreach (DataRow dr in DtTemp.Rows)
                        {
                            if (Convert.ToDouble(dr["ShrtgType"]) > 0)
                            {
                                Int32 GrdetlIdno = Convert.ToInt32(dr["GrDetl_Idno"]);
                                Int32 GrIdno = Convert.ToInt32(dr["Gr_Idno"]);

                                if (Grtype == "GR")
                                {
                                    IList<TblGrDetl> objTblGrDetl = (from obj in db.TblGrDetls where obj.GrDetl_Idno == GrdetlIdno select obj).ToList();
                                    foreach (var lst in objTblGrDetl)
                                    {
                                        lst.shortage = Convert.ToDouble(dr["ShrtgType"]);
                                        lst.shortage_Amount = Convert.ToDouble(dr["shortage_Amount"]);
                                        lst.Shortage_Qty = Convert.ToDouble(dr["ShrtgType"]);
                                        lst.shortage_Diff = Convert.ToDouble(dr["shortage_Diff"]);
                                        lst.Shrtg_Type = Convert.ToInt32(dr["ShrtgTypeId"]);
                                        lst.UnloadWeight = Convert.ToDouble(dr["UnloadWeight"]);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    IList<tblGrRetailerDetl> objTblGrdetl = (from obj in db.tblGrRetailerDetls where obj.GRRetDetl_Idno == GrdetlIdno select obj).ToList();
                                    foreach (var L in objTblGrdetl)
                                    {
                                        L.shortage = Convert.ToDouble(dr["ShrtgType"]);
                                        L.shortage_Amount = Convert.ToDouble(dr["shortage_Amount"]);
                                        L.Shortage_Qty = Convert.ToDouble(dr["ShrtgType"]);
                                        L.shortage_Diff = Convert.ToDouble(dr["shortage_Diff"]);
                                        L.Shrtg_Type = Convert.ToInt32(dr["ShrtgTypeId"]);
                                        L.UnloadWeight = Convert.ToDouble(dr["UnloadWeight"]);
                                        db.SaveChanges();
                                    }
                                }
                                IList<tblChlnBookDetl> objtblChlnBookDetl = (from obj in db.tblChlnBookDetls where obj.GR_Idno == GrIdno select obj).ToList();
                                foreach (var lst in objtblChlnBookDetl)
                                {
                                    //  Update tblChlnBookDetl set  Shrtg=@Shrtg where Gr_Idno=@GrIdno
                                    lst.Shrtg = true;
                                    db.SaveChanges();
                                }
                            }

                        }
                        dbTran.Commit();
                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return true;
        }

        public Int64 GrIdno(string GrNo, Int32 yearId, Int32 FromCityIdno, string con,string GrType)
        {
            Int64 GrId = 0;
            string sqlSTR = "";
            if (GrType == "GR")
            {
                sqlSTR = @"SELECT GR_Idno FROM TblGrHead WHERE (PrefixGr_No+''+ convert(nvarchar(255), gr_no))= '" + GrNo + "' AND From_City='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;

            }
            else
            {
                sqlSTR = @"SELECT GRRetHead_Idno as GR_Idno FROM tblgrretailerhead WHERE (Grret_Pref+''+ convert(nvarchar(255), grRet_no))= '" + GrNo + "' AND From_City='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;

            }
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                GrId = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return GrId;
        }

        public DataTable selectDetl(string con, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectShortageAmount");
            objSqlPara[1] = new SqlParameter("@ChlnIdNo", HeadId);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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

        public DataTable DsShortageAcnt(string con)
        {
            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT Acnt_Idno AS 'ShortageIdno' FROM ACNTMAST WHERE ACNT_NAME='SHORTAGE CHARGES' AND INTERNAL=1";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataSet AccPosting(string conString, string Action, Int64 IdFrom, Int64 IdTo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@From", IdFrom);
            objSqlPara[2] = new SqlParameter("@To", IdTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDemoAccPosting", objSqlPara);
            //if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            //{
            //    dt = objDataSet.Tables[0];
            //}
            return objDataSet;
        }

        public DataSet SelectTollTax(string prefix, string con)
        {
            string str = string.Empty;
            str = @"SELECT Toll_id, ISNUll(Tolltax_name ,'' ) AS Tolltax_name from  tbltollmaster where  Tolltax_name like '" + prefix + "%'  order by Tolltax_name Asc";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }

        public DataSet SelectAmount(string tollid, string con)
        {
            string str = string.Empty;
            str = @"SELECT Toll_id, ISNUll(Tolltax_name ,'' ) AS Tolltax_name ,ISNULL(Amount,0)AS Amount from  tbltollmaster where Toll_id= '" + tollid + " ' order by Tolltax_name Asc";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }

        //public Int64 InsertTollNumber(DataTable dtDetail, Int64 GrId, Int64 UserIdno, string con)
        //{
        //    Int64 intGrIdno = 0;
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        db.Connection.Open();
        //        //using (DbTransaction dbTran = db.Connection.BeginTransaction())
        //        //{
        //        try
        //        {
        //            TblGrHead objGRHead = (from obj in db.TblGrHeads where obj.GR_Idno == GrId select obj).FirstOrDefault();
        //            if (objGRHead == null)
        //            {
        //                objGRHead = new TblGrHead();
        //                db.TblGrHeads.AddObject(objGRHead);
        //                db.SaveChanges();
        //                intGrIdno = objGRHead.GR_Idno;
        //                if (intGrIdno > 0)
        //                {
        //                    List<tblTollDetl> lstGrDetl = db.tblTollDetls.Where(obj => obj.GR_Idno == intGrIdno).ToList();
        //                    if (lstGrDetl.Count > 0)
        //                    {
        //                        foreach (tblTollDetl obj in lstGrDetl)
        //                        {
        //                            db.tblTollDetls.DeleteObject(obj);
        //                        }
        //                        db.SaveChanges();
        //                    }
        //                    foreach (DataRow row in dtDetail.Rows)
        //                    {
        //                        tblTollDetl objTollDetl = new tblTollDetl();
        //                        objTollDetl.GR_Idno = Convert.ToInt64(intGrIdno);
        //                        objTollDetl.Chln_Idno = 0;
        //                        objTollDetl.Toll_Idno = Convert.ToInt32(row["Toll_Idno"]);
        //                        //}objTollDetl.Toll_Amt = Convert.ToDecimal(row["Toll_Amt"]);
        //                        objTollDetl.Ticket_No = Convert.ToString(row["Ticket_No"]);
        //                        objTollDetl.AddedBy_UserIdno = UserIdno;
        //                        objTollDetl.Date_Added = System.DateTime.Now;
        //                        objTollDetl.Date_Modified = System.DateTime.Now;
        //                        db.tblTollDetls.AddObject(objTollDetl);
        //                        db.SaveChanges();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                intGrIdno = -1;
        //            }
        //        }
        //        catch (Exception Ex)
        //        {
        //            intGrIdno = 0;
        //        }
        //        //}
        //    }
        //    return intGrIdno;


        public bool InsertTollNumber(DataTable DtTempToll)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (DtTempToll != null && DtTempToll.Rows.Count > 0)
                    {

                        foreach (DataRow row in DtTempToll.Rows)
                        {
                           tblTollDetl Todelt = new tblTollDetl();
                          
                            Todelt.Toll_Idno = Convert.ToInt64(row["Toll_Idno"]);
                            //Todelt.GR_Idno = Convert.ToInt64(row["GR_Idno"]);
                            Todelt.Chln_Idno = Convert.ToInt64(row["Chln_Idno"]);
                            Todelt.Toll_Amt = Convert.ToDecimal(row["Toll_Amt"]);
                            Todelt.Ticket_No = Convert.ToString(row["Ticket_No"]);
                            Todelt.AddedBy_UserIdno = 1;
                            Todelt.Date_Added = DateTime.Now;
                            Todelt.Date_Modified = DateTime.Now;
                            db.tblTollDetls.AddObject(Todelt);
                            db.SaveChanges();
                        }
                    }

                    else
                    {
                        return true;
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {

                return false;
            }
        }
        public Int64 cityID(string con, Int64 FronCityIdno)
        {
            Int64 cityid = 0; string sqlSTR = "";
            sqlSTR = @"Select cm.City_Idno from tblCityMaster cm inner join TblGrHead IGH on cm.City_Idno= IGH.From_City WHERE From_City=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                cityid = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return cityid;
        }
        public Int64 ChallanID(string con, string GrNo,Int64 year, Int64 city)
        {
            Int64 Challanid = 0; string sqlSTR = "";
            sqlSTR = @"select distinct chln_idno from TblGrHead WHERE Gr_No in (" + GrNo + ") and Year_Idno =" + year + " and From_City=" + city + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                Challanid = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return Challanid;
        }
        //public Int64 GRNo(string con, string ChallnNo, Int64 year, Int64 city)
        //{
        //    Int64 GrID = 0; string sqlSTR = "";
        //    sqlSTR = @"  SELECT Gr_No FROM TblGrHead gh inner join tblChlnBookHead ch on gh.Chln_Idno= ch.Chln_Idno WHERE ch.Chln_No =" + ChallnNo + " and gh.Year_Idno =" + year + " and gh.From_City=" + city + "";
        //      DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
        //    if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
        //    { 
        //        GrID = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
        //    }
        //    return GrID;
        //}
        public DataTable GRNo(string con, string ChallnNo, Int64 year, Int64 city)
        {
            string sqlSTR = string.Empty;
            sqlSTR = @"  SELECT Gr_No FROM TblGrHead gh inner join tblChlnBookHead ch on gh.Chln_Idno= ch.Chln_Idno WHERE ch.Chln_No =" + ChallnNo + " and gh.Year_Idno =" + year + " and gh.From_City=" + city + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataSet Report(Int64 YearIdno, String GRNo , Int64 iFrmCityIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectPrint");
            objSqlPara[1] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[2] = new SqlParameter("@GRNO", GRNo);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", iFrmCityIdno);
          
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
        }
        public DataSet Report1(Int64 YearIdno, String GRNo, Int64 iFrmCityIdno, Int64 chln, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "TaxInvoicePrint");
            objSqlPara[1] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[2] = new SqlParameter("@GRNO", GRNo);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@ChlnNo", chln);
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);

        }
        public DataSet TaxInvoice(Int64 YearIdno, String GRNo, Int64 iFrmCityIdno, Int64 chln, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "printtaxinvoice");
            objSqlPara[1] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[2] = new SqlParameter("@GRNO", GRNo);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@ChlnNo", chln);
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
        }
    }
}
