using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

namespace WebTransport.DAL
{
    public class DispatchRegDAL
    {
        #region GR Report  ...

    
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate,Int64 iFrmCityIdno,Int64 LorryType,Int64 DestinID,string con)
        {
           

            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@LorryType", LorryType);
            objSqlPara[5] = new SqlParameter("@Destination", DestinID);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spDipatchReg", objSqlPara);
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
        #endregion
    }
}
