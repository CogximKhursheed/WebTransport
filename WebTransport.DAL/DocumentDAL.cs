using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using System.Web;

namespace WebTransport.DAL
{
    public class DocumentDAL
    {
        public IList EmployeeList()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from tu in db.tblUserMasts select tu).ToList();
                return lst;
            }
        }
        public DataSet SelectDocumentRecord(string DateFrom, string DateTo, int DocType, Int64 EmpID, Int64 DocNo, string Con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@DocNo", DocNo);
            objSqlPara[1] = new SqlParameter("@EmpID", EmpID);
            objSqlPara[2] = new SqlParameter("@DateFrom", DateFrom);
            objSqlPara[3] = new SqlParameter("@DateTo", DateTo);
            objSqlPara[4] = new SqlParameter("@DocType", DocType);
            DataSet  ds = SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_SelectDocumentReocrd", objSqlPara);
            return ds;
        }
    }
}
