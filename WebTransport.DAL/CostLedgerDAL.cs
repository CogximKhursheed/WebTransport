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
    public class CostLedgerDAL
    {
        #region CostBreakupLedger Report  ...

        public DataSet FillLedger(string con)
        {
            DataSet ds = new DataSet();
            string strquery = "SELECT Acnt_Name +	SPACE(3) + CONVERT(NVARCHAR(20),Acnt_Idno) AS ACNT_NAME,Acnt_Idno AS ID FROM AcntMast WHERE Status=1 ORDER BY Acnt_Name";
            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, strquery);
            return ds;
        }

        public DataSet AfterDoubleClick1(string conString, string strAction, Int64 intPartyIdno,Int64 LederIdno, string strDateFrom, string strDateTo)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@TRUCKIDNO", intPartyIdno);
            objSqlPara[2] = new SqlParameter("@ACNTIDNO", LederIdno);
            objSqlPara[3] = new SqlParameter("@FromDate", strDateFrom);
            objSqlPara[4] = new SqlParameter("@ToDate", strDateTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spCostLedegerRep", objSqlPara);
            return objDataSet;
        }
        public DataTable SelectRep(string strAction,Int64 PrtyIdno, DateTime ? dtFromDate, DateTime ? dtToDate, Int64 iYearIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@ACNTIDNO", PrtyIdno);
            objSqlPara[2] = new SqlParameter("@FromDate", dtFromDate);
            objSqlPara[3] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[4] = new SqlParameter("@YEARIDNO", iYearIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spCostLedegerRep", objSqlPara);
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
        public DataSet FillCostgrid(string conString, string strActions,Int64 PrtyIdno, Int64 intTruckIdno, int intyeatidno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strActions);
            objSqlPara[1] = new SqlParameter("@TRUCKIDNO", intTruckIdno);
            objSqlPara[2] = new SqlParameter("@ACNTIDNO", PrtyIdno);
            objSqlPara[3] = new SqlParameter("@YEARIDNO", intyeatidno);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spCostLedegerRep", objSqlPara);
            return objDataSet;
        }
        #endregion

    }
}
