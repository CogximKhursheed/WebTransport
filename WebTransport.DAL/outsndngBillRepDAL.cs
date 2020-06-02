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
    public class outsndngBillRepDAL
    {
        #region Invoice Report...

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
                lst = (from cm in db.AcntMasts
                       join obj in db.tblInvGenHeads on cm.Acnt_Idno equals obj.Sendr_Idno
                       where cm.INTERNAL==false orderby cm.Acnt_Name ascending select cm).Distinct().ToList();
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
        //public List<spInvoiceReport_Result> SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, string con)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from s in db.spGRRep(strAction, Convert.ToString(dtFromDate), Convert.ToString(dtToDate), iFrmCityIdno) select s).ToList();
        //        return lst;
        //    }
        //}
        public DataTable SelectRep(string strAction, string dtFromDate, string dtToDate, Int64 iFrmCityIdno, Int64 iSenderIdno, Int32 iInvoiceNo, Int64 UserIdno, string con,string Inv_prefix)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action",strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[5] = new SqlParameter("@InvoiceNo", iInvoiceNo);
            objSqlPara[6] = new SqlParameter("@UserIdno", UserIdno);
            objSqlPara[7] = new SqlParameter("@Inv_prefix", Inv_prefix);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spOutstndngBillReport", objSqlPara);
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
    }

}
