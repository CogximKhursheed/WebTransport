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
    public class InvoiceRepDAL
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
                lst = (from cm in db.AcntMasts where cm.Acnt_Type==2 orderby cm.Acnt_Name ascending select cm).ToList();
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
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, Int64 iSenderIdno, Int32 iInvoiceNo,Int64 UserIdno,Int64 InvTyp,string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action",strAction);
            objSqlPara[1] = new SqlParameter("@DtFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DtTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[5] = new SqlParameter("@InvoiceNo", iInvoiceNo);
            objSqlPara[6] = new SqlParameter("@UserIdno", UserIdno);
            objSqlPara[7] = new SqlParameter("@InvoiceTyp", InvTyp);
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
        public Int64 InvIdno(string con, Int64 invNo, Int64 year, Int64 FromCityIdno)
        {
            Int64 InvNo = 0; string sqlSTR = "";
            sqlSTR = @"Select Inv_Idno FROM tblInvGenHead  WHERE Inv_No =" + invNo + " and Year_Idno =" + year + " and  BaseCity_Idno=" + FromCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                InvNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return InvNo;
        }
        public Int64 InvIdno(string con, Int64 year, Int64 FromCityIdno)
        {
            Int64 InvNo = 0; string sqlSTR = "";
            sqlSTR = @"Select Inv_Idno FROM tblInvGenHead  WHERE Year_Idno =" + year + " and  BaseCity_Idno=" + FromCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                InvNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return InvNo;
        }
        #endregion
    }

}
