using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
namespace WebTransport.DAL
{
    public class ProfitLossBookDAL
    {
        public DataSet SelectFinalBookPL1(string conString, string Actions, string VCHR_DATE, string DateTo, int YEAR_IDNO)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Actions);
            objSqlPara[1] = new SqlParameter("@VCHR_DATE", VCHR_DATE);
            objSqlPara[2] = new SqlParameter("@DateTo", DateTo);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YEAR_IDNO);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spProfitLossAc", objSqlPara);
            return objDataSet;
        }
        public DataSet SelectFinalBookProfitLosswtDtRnge(string conString, string Actions, string VCHR_DATE, string DATETO, int YEAR_IDNO)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Actions);
            objSqlPara[1] = new SqlParameter("@VCHR_DATE", VCHR_DATE);
            objSqlPara[2] = new SqlParameter("@DATETO", DATETO);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YEAR_IDNO);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spProfitLossAc", objSqlPara);
            return objDataSet;
        }

        #region Final Book-Balance Sheet
        public DataSet SelectFinalBookBS(string conString, string Actions, string VCHR_DATE, string DATETO, int YEAR_IDNO)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Actions);
            objSqlPara[1] = new SqlParameter("@VCHR_DATE", VCHR_DATE);
            objSqlPara[2] = new SqlParameter("@DATETO", DATETO);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YEAR_IDNO);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spProfitLossAc", objSqlPara);
            return objDataSet;
        }
        public DataSet SelectFinalBookwtDtRngeBS(string conString, string Actions, string VCHR_DATE, string DATETO, int YEAR_IDNO)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Actions);
            objSqlPara[1] = new SqlParameter("@VCHR_DATE", VCHR_DATE);
            objSqlPara[2] = new SqlParameter("@DATETO", DATETO);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", YEAR_IDNO);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spProfitLossAc", objSqlPara);
            return objDataSet;
        }
        #endregion
    }
}
