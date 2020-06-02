using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class DayBookReportDAL
    {
        public DataSet SelectOpenPayRecWithBnk(string conString, string strAction, Int64 intPartyIdno, string strDateFrom, Int64 YearIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@YEAR_IDNO", YearIdno);
            objSqlPara[2] = new SqlParameter("@Date", strDateFrom);
            objSqlPara[3] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDayBook", objSqlPara);
            return objDataSet;
        }

        public DataSet SelectLdgrDetOne(string conString, string strAction, Int64 intPartyIdno, Int64 VchrIdno, Int64 YearIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@YEAR_IDNO", YearIdno);
            objSqlPara[2] = new SqlParameter("@VCHR_IDNO", VchrIdno);
            objSqlPara[3] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDayBook", objSqlPara);
            return objDataSet;
        }

        public Double SELECTCRDR(string conString, string strAction, Int64 AmntType, Int64 intPartyIdno, string DateFor, Int64 YearIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@AMNT_TYPE", AmntType);
            objSqlPara[2] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YearIdno);
            objSqlPara[4] = new SqlParameter("@Date", DateFor);
            Double objDataSet = Convert.ToDouble(SqlHelper.ExecuteScalar(conString, CommandType.StoredProcedure, "spDayBook", objSqlPara));
            return objDataSet;
        }

        public Double SELECTOPBAL(string conString, string strAction, Int64 OpenType, Int64 intPartyIdno, Int64 YearIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@OPEN_TYPE", OpenType);
            objSqlPara[2] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YearIdno);
            Double objDataSet = Convert.ToDouble(SqlHelper.ExecuteScalar(conString, CommandType.StoredProcedure, "spDayBook", objSqlPara));
            return objDataSet;
        }

        public IList FillPartyDetail()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from Acnt in db.AcntMasts
                           where Acnt.Acnt_Type == 3
                           select new
                               {
                                   Acnt.Acnt_Name,
                                   Acnt.Acnt_Idno
                               }
                           ).ToList();
                return lst;

            }
        }
    }
}