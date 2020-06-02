using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;

namespace WebTransport.DAL
{
    public class PrtyDetlDAL
    {

        public List<tblCityMaster> selectLocation(Int32 PrtyIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst1 = (from obj in db.tblChlnAmntPayment_Head
                            join DETl in db.tblChlnAmntPayment_Detl on obj.Id equals DETl.Head_Idno
                            join CH in db.tblChlnBookHeads on DETl.Chln_Idno equals CH.Chln_Idno
                            where obj.Party_IdNo == PrtyIdno  select obj.Loc_Idno).Distinct().ToList();

                List <tblCityMaster> lst     = (from CM in db.tblCityMasters
                       where lst1.Contains(CM.City_Idno) select CM
                       
                      
                       ).Distinct().ToList();

                return lst;
            }
        }
        public List<AcntMast> BindPrty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts
                       join LM in db.LorryMasts on cm.Acnt_Idno equals LM.Prty_Idno
                       where cm.INTERNAL == false
                       select cm).OrderBy(c=>c.Acnt_Name).Distinct().ToList();
                return lst;
            }
        }
        public DataTable BindPartynew(string strAction, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara);
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
        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }


        public double SelectLocationAmnt(string strAction, Int32 LocIdno, Int32 ChlnIdno, string con, DateTime dtFromDate, DateTime dtToDate)
        {
            double dAmnt=0;
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@LocIdno", LocIdno);
            objSqlPara[2] = new SqlParameter("@ChlnIdno", ChlnIdno);
            objSqlPara[3] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[4] = new SqlParameter("@To_Date", dtToDate);
            dAmnt  = Convert.ToDouble(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara));
            return dAmnt;
        }

        public DataTable SelectPayRecvdAmnt(string strAction, Int64 SenderIdNo, string con, DateTime dtFromDate, DateTime dtToDate)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@SenderIdno", SenderIdNo);
            objSqlPara[2] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[3] = new SqlParameter("@To_Date", dtToDate);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara);
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

        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iSenderIdno, Int64 iFrmCityIdno,Int64 iUserIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[4] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[5] = new SqlParameter("@UserIdno", iUserIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara);
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
        public DataTable SelectVchrReport(string strAction, Int64 iSenderIdno, string con, DateTime dtFromDate, DateTime dtToDate)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[2] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[3] = new SqlParameter("@To_Date", dtToDate);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara);
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
        public double SelectShrtgAmnt(string strAction, Int64 iSenderIdno, DateTime dtFromDate, string con)
        {
            double shrtgAmnt = 0;
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[2] = new SqlParameter("@From_Date", dtFromDate);
            shrtgAmnt = Convert.ToDouble(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spLedgerReport", objSqlPara));
            return shrtgAmnt;
        }
        public DataTable SelectPartyNetBalance(string strAction, string dtFromDate, string dtToDate, Int64 iSenderIdno,Int64 CityID, Int64 LorryType, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@FromDate", dtFromDate);
            objSqlPara[2] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[3] = new SqlParameter("@AcntId", iSenderIdno);
            objSqlPara[4] = new SqlParameter("@cityID", CityID);
            objSqlPara[5] = new SqlParameter("@LorryType", LorryType);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSelectPartyAmount", objSqlPara);
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
        public DataSet SelectPartyDetailReport(string DateFrom, string DateTo, string PartyID, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectReport");
            objSqlPara[1] = new SqlParameter("@DateFrom", DateFrom);
            objSqlPara[2] = new SqlParameter("@DateTo", DateTo);
            objSqlPara[3] = new SqlParameter("@AcntID", PartyID);
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPartyDetailReport", objSqlPara);
            return ds;
        }
        public DataSet SelectPartyDetailReportArv(string DateFrom, string DateTo, string PartyID, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectReport");
            objSqlPara[1] = new SqlParameter("@DateFrom", DateFrom);
            objSqlPara[2] = new SqlParameter("@DateTo", DateTo);
            objSqlPara[3] = new SqlParameter("@AcntID", PartyID);
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPartyDetailReportArv", objSqlPara);
            return ds;
        }
    }
}
