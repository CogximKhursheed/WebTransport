using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace WebTransport.DAL
{
    public class PaymentToOwnDAL
    {
        string sqlSTR = "";
        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        public Int64 MaxNo(Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Rcpt_no),0) + 1 AS MAXID FROM tblPayToOwnAcc WHERE BaseCity_idno='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public Int32 GetRcptNo(Int32 YearIdno, Int32 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32((from obj in db.tblPayToOwnAccs where (obj.Year_IdNo == YearIdno) && (obj.BaseCity_Idno == FromCity) select obj.Rcpt_No).Max());
                MaxNo = MaxNo + 1;
                return MaxNo;
            }
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }
        public List<AcntMast> BindBank()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objacntmast = new List<AcntMast>();

                objacntmast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 4
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objacntmast;
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
        public Int64 GetMaxNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from hd in db.tblPayToOwnAccs select hd.Rcpt_No).Max() + 1;
                return Convert.ToInt64(max);
            }
        }
        public tblPayToOwnAcc selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblPayToOwnAccs.Where(h => h.Id == HeadId).FirstOrDefault();
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
        public Int64 Insert(tblPayToOwnAcc obj)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblPayToOwnAcc AmtHead = new tblPayToOwnAcc();
                db.Connection.Open();
                //    using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //   {
                try
                {
                    tblPayToOwnAcc RH = db.tblPayToOwnAccs.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                    if (RH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        obj.Date_Added = DateTime.Now;
                        obj.Date_Modified = DateTime.Now;
                        db.tblPayToOwnAccs.AddObject(obj);
                        db.SaveChanges();
                        AmntHeadId = obj.Id;
                    }

                }
                catch
                {
                    // dbTran.Rollback();
                }
                return AmntHeadId;
            }

        }
        public Int64 Update(tblPayToOwnAcc obj, Int32 Head_Idno)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //db.Connection.Open();
                //using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //{
                   try
                    {
                        tblPayToOwnAcc CH = db.tblPayToOwnAccs.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.Id != Head_Idno && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                        if (CH != null)
                        {
                            AmntHeadId = -1;
                        }
                        else
                        {
                            tblPayToOwnAcc TBH = (from mast in db.tblPayToOwnAccs
                                                  where mast.Id == Head_Idno
                                                  select mast).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Rcpt_No = obj.Rcpt_No;
                                TBH.Rcpt_date = obj.Rcpt_date;
                                TBH.Year_IdNo = obj.Year_IdNo;
                                TBH.RcptType_Idno = obj.RcptType_Idno;
                                TBH.Driver_IdNo = obj.Driver_IdNo;
                                TBH.Inst_No = obj.Inst_No;
                                TBH.Inst_Dt = obj.Inst_Dt;
                                TBH.Bank_Idno = obj.Bank_Idno;
                                TBH.Comp_Id = obj.Comp_Id;
                                TBH.Remark = obj.Remark;
                                TBH.Date_Modified = obj.Date_Modified;
                                TBH.Amnt = obj.Amnt;
                                TBH.UserIdno = obj.UserIdno;
                                TBH.BaseCity_Idno = obj.BaseCity_Idno;
                                TBH.Date_Modified = DateTime.Now;
                                db.SaveChanges();
                                AmntHeadId = TBH.Id;
                              
                                //dbTran.Commit();
                            }
                        }



                    }
                    catch
                    {
                        //dbTran.Rollback();
                    }
               // }
            }
            return AmntHeadId;
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from CH in db.tblPayToOwnAccs
                         //join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                         join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                         join AM in db.AcntMasts on CH.Driver_IdNo equals AM.Acnt_Idno
                         select CH.Id).Count();

                return Count;
            }

        }
        public IList search(Int32 yearid, Int64 RcptNo, DateTime? dtfrom, DateTime? dtto, int FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblPayToOwnAccs
                           //join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                           join AM in db.AcntMasts on CH.Driver_IdNo equals AM.Acnt_Idno
                           join CD in db.tblChlnBookHeads on CH.Chln_IdNo equals CD.Chln_Idno
                           select new
                           {
                               CH.BaseCity_Idno,
                               CH.Rcpt_date,
                               CH.Rcpt_No,
                               CH.Id,
                               CD.Chln_No,
                               FromCity = cito.City_Name,
                               CH.Year_IdNo,
                               AM.Acnt_Name,
                               CH.Amnt,
                               Driver_Name=AM.Acnt_Name
                           }).ToList();
                if (RcptNo > 0)
                {
                    lst = lst.Where(l => l.Rcpt_No == RcptNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Rcpt_date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Rcpt_date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }

                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_IdNo == yearid).ToList();
                }

                return lst;
            }
        }
        public DataTable BindRcptTypeDel(Int32 intRcptTypeId, string con)
        {

            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4,12) and Acnt_Idno=" + intRcptTypeId + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable SelectDBData(Int64 Item_Idno, string con)
        {
            string str = string.Empty;
            str = @"select isnull(Rate_Idno,0) Rate_Idno,isnull(Rate_Date,0) Rate_Date,Isnull(CM.City_Name,'') City_Name ,Isnull(Item_Rate,0) Item_Rate,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0)WghtShrtg_Rate,ISNULL(Tocity_Idno,0) Tocity_Idno from tblRateMast RM Inner Join
           tblCityMaster CM ON CM.city_Idno=RM.ToCity_Idno where RM.Item_Idno= " + Item_Idno + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable FetchChallanDetail(string ConString, int yearid, Int32 FromCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[3];
                objSqlPara[0] = new SqlParameter("@Action", "SelectChlnDet");
                objSqlPara[1] = new SqlParameter("@FromCity", FromCityIdno);
                objSqlPara[2] = new SqlParameter("@YearIdno", yearid);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spPayToDriver", objSqlPara);
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

        public double FetchLedgerBal(string ConString,string Action,int amnttype,string acnt_idno,int yearid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[4];
                objSqlPara[0] = new SqlParameter("@Action", Action);
                objSqlPara[1] = new SqlParameter("@AMNTTYPE", amnttype);
                objSqlPara[2] = new SqlParameter("@ACNTIDNO", acnt_idno);
                objSqlPara[3] = new SqlParameter("@YEARIDNO", yearid);
                var Temp = SqlHelper.ExecuteScalar(ConString, CommandType.StoredProcedure, "spVoucherEntry", objSqlPara);
           
                return Convert.ToDouble(Temp);
            }
        }

        public double FetchLedgerOpCl(string ConString, string Action, int opentype, string acnt_idno, int yearid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[4];
                objSqlPara[0] = new SqlParameter("@Action", Action);
                objSqlPara[1] = new SqlParameter("@OpenType", opentype);
                objSqlPara[2] = new SqlParameter("@AcntIdno", acnt_idno);
                objSqlPara[3] = new SqlParameter("@YearIdno", yearid);
                var Temp = SqlHelper.ExecuteScalar(ConString, CommandType.StoredProcedure, "spAcntOpen", objSqlPara);

                return Convert.ToDouble(Temp);
            }
        }

        public DataTable SelectChallanDetail(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallanDetail");
            objSqlPara[1] = new SqlParameter("@ChlnIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPayToDriver", objSqlPara);
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


        public double LedgerBal(string con, int AmntType, int AcntIdno, int YearIdno)
        {
            double bal = 0;
            sqlSTR = @"SELECT ISNULL(SUM(ACNT_AMNT),0) AS DEBCRED FROM (
	                    SELECT ACNT_AMNT FROM VchrHead V 
	                    INNER JOIN VchrDetl VD ON V.VCHR_IDNO = VD.VCHR_IDNO 
	                    INNER JOIN AcntMast A ON A.ACNT_IDNO=VD.ACNT_IDNO 
	                    WHERE V.VCHR_SUSP = 0 AND VD.ACNT_IDNO="+ AcntIdno +" AND VD.AMNT_TYPE = "+ AmntType +" AND V.YEAR_IDNO="+ YearIdno +") A";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                bal = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
            }
            return bal;
        }


        public double PreviousPay(string con, Int64 ChlnIdno, Int64 DriverId)
        {
            //sqlSTR = @"SELECT ISNULL(SUM(Amnt),0) FROM tblPayToOwnAcc WHERE Chln_IdNo=" + ChlnIdno + " AND Driver_IdNo=" + DriverId + "";
            //double amnt = SqlHelper.ExecuteNonQuery(con, CommandType.Text, sqlSTR);
            //return amnt;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                double? Sum = 0;
                Sum = (from CH in db.tblPayToOwnAccs
                       where CH.Chln_IdNo == ChlnIdno && CH.Driver_IdNo == DriverId
                       select CH.Amnt).Sum();

                return Convert.ToDouble(Sum);
            }
        }
        public DataTable BindRcptType(string con)
        {
            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable BindDriver(string con)
        {
            sqlSTR = @"select DISTINCT Acnt_Name,Acnt_Idno from AcntMast where Acnt_Type =9 and status=1";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public List<AcntMast> BindParty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
       
        public int Delete(Int64 HeadId,Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                tblPayToOwnAcc CH = db.tblPayToOwnAccs.Where(h => h.Id == HeadId).FirstOrDefault();
                if (CH != null)
                {
                    db.tblPayToOwnAccs.DeleteObject(CH);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public Int64 GetChlnIdno(Int64 HeadID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var ChlnId = (from hd in db.tblPayToOwnAccs
                              where hd.Id == HeadID select hd.Chln_IdNo).FirstOrDefault();
                return Convert.ToInt64(ChlnId);
            }
        }

        public IList DriverName(Int64 ChlnIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var driver = (from CH in db.tblChlnBookHeads
                              join A in db.AcntMasts on CH.Driver_Idno equals A.Acnt_Idno
                              where CH.Chln_Idno == ChlnIdno
                              select new { A.Acnt_Idno, A.Acnt_Name }).ToList();
                return driver;
            }
        }

        public IList SearchDetails(int ChlnIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblPayToOwnAccs
                           //join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                           join AM in db.AcntMasts on CH.Driver_IdNo equals AM.Acnt_Idno
                           join CD in db.tblChlnBookHeads on CH.Chln_IdNo equals CD.Chln_Idno
                           where CH.Chln_IdNo == ChlnIdno
                           select new
                           {
                               CH.BaseCity_Idno,
                               CH.Rcpt_date,
                               CH.Rcpt_No,
                               CH.Id,
                               CD.Chln_No,
                               FromCity = cito.City_Name,
                               CH.Year_IdNo,
                               AM.Acnt_Name,
                               CH.Amnt,
                               Driver_Name = AM.Acnt_Name
                           }).ToList();
                return lst;
            }
        }
        public void UpdateIsPosting(Int64 PayID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblPayToOwnAcc GH = (from G in db.tblPayToOwnAccs where G.Id == PayID select G).FirstOrDefault();
                    if (GH != null)
                    {
                        GH.IS_Post = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
