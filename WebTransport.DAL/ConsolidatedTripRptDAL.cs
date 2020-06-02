using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class ConsolidatedTripRptDAL
    {
        public DataTable SelectRep(int YearIdno, int Month, Int32 LorryIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallan");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@Month_Id", Month);
            objSqlPara[3] = new SqlParameter("@Lorry_Idno", LorryIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spConsolidRep", objSqlPara);
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
        public DataTable SelectTripDetl(Int64 TripIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectTrip");
            objSqlPara[1] = new SqlParameter("@Trip_Idno", TripIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spConsolidRep", objSqlPara);
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
        public DataTable SelectPurDetl(int YearIdno, int Month, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectPur");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@Month_Id", Month);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spConsolidRep", objSqlPara);
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
        public DataTable SelectVchrDetl(int YearIdno, int Month, Int32 LorryIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectVchr");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@Lorry_Idno", LorryIdno);
            objSqlPara[3] = new SqlParameter("@Month_Id", Month);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spConsolidRep", objSqlPara);
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

        public DataTable SelectOpening(int YearIdno, int Month, Int32 LorryIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "Opening");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@Month_Id", Month);
            objSqlPara[3] = new SqlParameter("@Lorry_Idno", LorryIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spConsolidRep", objSqlPara);
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
