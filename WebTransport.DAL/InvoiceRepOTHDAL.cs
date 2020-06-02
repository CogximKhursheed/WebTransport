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
    public class InvoiceRepOTHDAL
    {
        #region Comments...
        public List<AcntMast> BindRecever()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
        public List<AcntMast> BindSender()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
        public List<tblCityMaster> BindDelvryPlace()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, string LorryNo,Int64 iFrmCityIdno, Int64 iSenderIdno, Int32 iInvoiceNo, Int64 UserIdno, Int64 InvTyp, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DtFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DtTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@LorryNo", LorryNo);
            objSqlPara[4] = new SqlParameter("@FromCityIdno", iFrmCityIdno);
            objSqlPara[5] = new  SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[6] = new SqlParameter("@InvoiceNo", iInvoiceNo);
            objSqlPara[7] = new SqlParameter("@UserIdno", UserIdno);
            objSqlPara[8] = new SqlParameter("@InvoiceTyp", InvTyp);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvoiceReportOTH", objSqlPara);
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
        public DataTable selectInvoiceReportDetails(string strAction, Int32 Inv_Idno, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", strAction);
                objSqlPara[1] = new SqlParameter("@InvoiceNo", Inv_Idno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvoiceReport", objSqlPara);
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
        #endregion

        #region Invoice Report [OTH]...

        #endregion
    }

}
