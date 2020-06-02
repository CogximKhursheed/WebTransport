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
    public class TdsReportDAL
    {
        #region Tds Report...
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, string iVehicleNo,Int64 iType,Int64 PartyIdno,string PanNo, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action",strAction);
            objSqlPara[1] = new SqlParameter("@DtFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DtTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@VehicleNo", iVehicleNo);
            objSqlPara[4] = new SqlParameter("@Type", iType);
            objSqlPara[5] = new SqlParameter("@PrtyIdno", PartyIdno);
            objSqlPara[6] = new SqlParameter("@PanNo", PanNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spVehicleReport", objSqlPara);
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
