using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class GSTRegisterDAL
    {
        public DataTable SelectGSTR1(String ConnectionString, String Action, Int32 YearId, Int32 CityIdno, Int32 MonthNo)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@CityIdno", CityIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@MonthNo", MonthNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGSTRegister", objSqlPara);
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
}
