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
    public class BankReConciliationDAL
    {
        public VchrDetl FetchBankDateById(Int64 VchrDetlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from ah in db.VchrDetls where ah.VchrDetl_Idno == VchrDetlIdno select ah).FirstOrDefault();
            }
        }
        public VchrHead FetchBankDateByvchrId(Int64 VchrIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from ah in db.VchrHeads where ah.Vchr_Idno == VchrIdno select ah).FirstOrDefault();
            }
        }
        public int Update(DateTime BankDate, int intVchrDetlIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {

                    VchrDetl objVchrDetl = (from mast in db.VchrDetls
                                            where mast.VchrDetl_Idno == intVchrDetlIdno
                                            select mast).FirstOrDefault();
                    if (objVchrDetl != null)
                    {
                        objVchrDetl.Bank_Date = BankDate;
                        db.SaveChanges();
                        intValue = intVchrDetlIdno;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return intValue;
        }

        #region Bank ReConcilition ...

        public DataTable BankName(string conString, string Action)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spBankReConc", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }

        public DataTable BankNamewithComp(string conString, string Action, Int32 Comp_Idno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@CompIdno", Comp_Idno);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spBankReConc", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }
        public List<AcntMast> BindBank()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type==4 orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }

        public DataSet SelectBankRC1(string conString, string Action, string DateFrom, string DateTo, Int64 Acnt_Idno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DateFrom", DateFrom);
            objSqlPara[2] = new SqlParameter("@DateTo", DateTo);
            objSqlPara[3] = new SqlParameter("@AcntIdno", Acnt_Idno);
            DataTable objDataTable = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spBankReConc", objSqlPara);
            return ds;
        }

        public DataSet SelectBankRC2(string conString, string Action, string VchrIdno, string AmntType, Int32 TranType, Int32 BankDt, Int64 Acnt_Idno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@VCHR_IDNO", VchrIdno);
            objSqlPara[2] = new SqlParameter("@AMNT_TYPE", AmntType);
            objSqlPara[3] = new SqlParameter("@TRANTYPE", TranType);
            objSqlPara[4] = new SqlParameter("@BANKDT", BankDt);
            objSqlPara[5] = new SqlParameter("@AcntIdno", Acnt_Idno);
            DataTable objDataTable = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spBankReConc", objSqlPara);
            return ds;
        }

        public DataTable OpenBal(string conString, Int32 Year_Idno, Int64 Acnt_Idno)
        {
            DataTable objDataTable = new DataTable();
            string strSQL = string.Empty;
            string ClosingBal = string.Empty;
            strSQL = @"IF((SELECT COUNT(*) FROM Acntmast WHERE ACNT_IDNO IN(" + Acnt_Idno + ") AND YEAR_IDNO = " + Year_Idno +
                ") > 0) SELECT CASE WHEN Bal_Type=1 THEN convert(nvarchar,CONVERT(NUMERIC(15,2),Open_Bal))+' Cr.' ELSE '0 Cr.' END AS 'CREDIT',CASE WHEN Bal_Type=2 THEN convert(nvarchar,CONVERT(NUMERIC(15,2),Open_Bal))+' Dr.' ELSE '0 Dr.' END AS 'DEBIT' FROM Acntmast WHERE ACNT_IDNO =" + Acnt_Idno +
                " AND YEAR_IDNO = " + Year_Idno + " ELSE SELECT  '0 Cr.' AS 'CREDIT','0 Dr.' AS 'DEBIT'";
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objDataTable = ds.Tables[0];
            }
            return objDataTable;
        }

        public DataTable OpeningBalance(string conString, string strDateFrom, Int64 Acnt_Idno)
        {
            DataTable objDataTable = new DataTable();
            string strSQL = string.Empty;
            string ClosingBal = string.Empty;
            strSQL = @"select isnull(sum(A.Debit),0) as Debit,isnull(sum(A.Credit),0) as Credit from 
                        (SELECT CONVERT(DATETIME, V.VCHR_DATE,105) AS VCHR_DATE,V.VCHR_NO,
                        CASE WHEN VD.AMNT_TYPE = 2 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE  0 END AS  'Debit',
                        CASE WHEN VD.AMNT_TYPE = 1 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE 0 END AS  'Credit',
                        V.VCHR_IDNO,VD.AMNT_TYPE,ISNULL(VID.DOC_IDNO,0) DOC_IDNO,CONVERT(DATETIME, VD.Bank_Date,105) AS Bank_DATE
                        FROM VCHRHEAD V INNER JOIN VCHRDETL VD ON V.VCHR_IDNO=VD.VCHR_IDNO LEFT OUTER JOIN VCHRIDDETL VID ON V.VCHR_IDNO=VID.VCHR_IDNO 
                        WHERE VD.ACNT_IDNO=" + Acnt_Idno + " AND CONVERT(DATETIME, V.VCHR_DATE,105) < CONVERT(DATETIME,'" + strDateFrom +
                                            "',105) and CONVERT(DATETIME, VD.Bank_Date,105) != CONVERT(DATETIME,'01-01-1900',105))A";
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objDataTable = ds.Tables[0];
            }
            return objDataTable;
        }

        public DataTable ClosingBalance(string conString, string strDateTo, Int64 Acnt_Idno)
        {
            DataTable objDataTable = new DataTable();
            string strSQL = string.Empty;
            string ClosingBal = string.Empty;
            strSQL = @"select isnull(sum(A.Debit),0) as Debit,isnull(sum(A.Credit),0) as Credit from 
                        (SELECT CONVERT(DATETIME, V.VCHR_DATE,105) AS VCHR_DATE,V.VCHR_NO,
                        CASE WHEN VD.AMNT_TYPE = 2 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE  0 END AS  'Debit',
                        CASE WHEN VD.AMNT_TYPE = 1 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE 0 END AS  'Credit',
                        V.VCHR_IDNO,VD.AMNT_TYPE,ISNULL(VID.DOC_IDNO,0) DOC_IDNO,CONVERT(DATETIME, VD.Bank_Date,105) AS Bank_DATE
                        FROM VCHRHEAD V INNER JOIN VCHRDETL VD ON V.VCHR_IDNO=VD.VCHR_IDNO LEFT OUTER JOIN VCHRIDDETL VID ON V.VCHR_IDNO=VID.VCHR_IDNO 
                        WHERE VD.ACNT_IDNO=" + Acnt_Idno + " AND CONVERT(DATETIME, V.VCHR_DATE,105) <= CONVERT(DATETIME,'" + strDateTo +
                                            "',105))A";
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objDataTable = ds.Tables[0];
            }
            return objDataTable;
        }

        public DataTable BalBank(string conString, string strDateTo, Int64 Acnt_Idno)
        {
            DataTable objDataTable = new DataTable();
            string strSQL = string.Empty;
            string ClosingBal = string.Empty;
            strSQL = @"select isnull(sum(A.Debit),0) as Debit,isnull(sum(A.Credit),0) as Credit from 
                        (SELECT CONVERT(DATETIME, V.VCHR_DATE,105) AS VCHR_DATE,V.VCHR_NO,
                        CASE WHEN VD.AMNT_TYPE = 2 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE  0 END AS  'Debit',
                        CASE WHEN VD.AMNT_TYPE = 1 THEN CAST(VD.ACNT_AMNT AS DECIMAL(25,2)) ELSE 0 END AS  'Credit',
                        V.VCHR_IDNO,VD.AMNT_TYPE,ISNULL(VID.DOC_IDNO,0) DOC_IDNO,CONVERT(DATETIME, VD.Bank_Date,105) AS Bank_DATE
                        FROM VCHRHEAD V INNER JOIN VCHRDETL VD ON V.VCHR_IDNO=VD.VCHR_IDNO LEFT OUTER JOIN VCHRIDDETL VID ON V.VCHR_IDNO=VID.VCHR_IDNO 
                        WHERE VD.ACNT_IDNO=" + Acnt_Idno + " AND CONVERT(DATETIME, V.VCHR_DATE,105) <= CONVERT(DATETIME,'" + strDateTo +
                                            "',105) and (isnull(VD.Bank_Date,'') = '' or  CONVERT(DATETIME, VD.Bank_Date,105) = CONVERT(DATETIME, '01-01-1900',105)))A";
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objDataTable = ds.Tables[0];
            }
            return objDataTable;
        }
        #endregion

    }
}
