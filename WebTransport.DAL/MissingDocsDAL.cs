using System;
using System.Linq;
using System.Transactions;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;



namespace WebTransport.DAL
{
   public class MissingDocsDAL
    {
       public DataSet GetMIssingDoc(string Category, string YearId,Int64 From,Int64 To, string Action, string Con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@CategId", Category);
            objSqlPara[1] = new SqlParameter("@DateRange", YearId);
            objSqlPara[2] = new SqlParameter("@From", From);
            objSqlPara[3] = new SqlParameter("@To", To);
            objSqlPara[4] = new SqlParameter("@Action", "MissingDocs");
            DataSet objDsTemp = SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "spMissingDocs", objSqlPara);
            return objDsTemp;
        }
    }
}
