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
using WebTransport;

namespace WebTransport.DAL
{
    public class PendGrForInvDAL
    {
        public DataTable selectGrDetails(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, Int64 iSenderIdno, Int32 iYearId, Int64 UserIdno,Int64 intgrno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
            objSqlPara[3] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[4] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[5] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[6] = new SqlParameter("@UserIdno", UserIdno);  //@GRno
            objSqlPara[7] = new SqlParameter("@GRno", intgrno); 

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPendgGRForInvceReport", objSqlPara);
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
