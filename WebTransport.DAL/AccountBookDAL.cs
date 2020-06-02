using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class AccountBookDAL
    {
        // Account Book's DropDown List Function
        public DataTable FillPrtyAndItem(string conString, string strActions)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", strActions);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookRpt", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }
        public DataSet SelectpartyID(string AcntName, string con)
        {
            string Query = "Select Acnt_Idno, Acnt_Name+' ['+ CONVERT(VARCHAR(20), Acnt_Idno)+']' From AcntMast Where Acnt_Type IN(5, 9) AND Acnt_Name ='" + AcntName + "'";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, Query);//ApplicationFunction.ConnectionString()
            return ds;
        }
        public DataSet SelectPartyList(string prefix, string con)
        {
            string str = string.Empty;
            str = @"select distinct Top 25 Acnt_Idno,Acnt_Name=Acnt_Name +' ['+Cast(Acnt_Idno as varchar(100))+']' from AcntMast AM where Acnt_Name like '" + prefix + "%'  order by Acnt_Name Asc";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }
        public DataTable FillCompwiseParty(string conString, Int64 CompId)
        {
            string strSQL = string.Empty;
            strSQL = @"select Acnt_Idno,Acnt_Name + ' [' + CONVERT(NVARCHAR(15),Acnt_Idno) + ']' Acnt_Name from AcntMast where Status=1 order by Acnt_Name";
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }
        public IList FillStoreParty(Int64 StoreIdno, int Typ)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //if (Typ == 1)
                //{
                var lst = (from Acnt in db.AcntMasts where (Acnt.Acnt_Type == 5 || Acnt.Acnt_Type == 9) && Acnt.Status == true select Acnt).ToList();
                return lst;
                //}
                //else
                //{
                //    var lst = (from Acnt in db.tblAcntMasts where Acnt.Type_Idno == Typ && Acnt.Store_Idno == StoreIdno && (Acnt.Acnt_Type == 5 || Acnt.Acnt_Type == 9) select Acnt).ToList();
                //    return lst;
                //}
            }
        }

        // Account Book's Ledger Report Function
        public DataSet Fillledgergrid(string conString, string strActions, Int64 intPartyIdno, int intyeatidno, string strDateFrom, string strDateTo)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", strActions);
            objSqlPara[1] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            objSqlPara[2] = new SqlParameter("@YEAR_IDNO", intyeatidno);
            objSqlPara[3] = new SqlParameter("@DateFrom", strDateFrom);
            objSqlPara[4] = new SqlParameter("@DateTo", strDateTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookRpt", objSqlPara);
            return objDataSet;
        }

        public DataSet AfterDblClikForLedD(string conString, string strAction, string VCHR_IDNO, string AMNT_TYPE, Int64 PartyId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@VCHR_IDNO", VCHR_IDNO);
            objSqlPara[2] = new SqlParameter("@AMNT_TYPE", AMNT_TYPE);
            objSqlPara[3] = new SqlParameter("@Acnt_Idno", PartyId);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookRpt", objSqlPara);
            return objDataSet;
        }

        public DataSet AfterDoubleClick1(string conString, string strAction, Int64 intPartyIdno, string strDateFrom, string strDateTo)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@Acnt_Idno", intPartyIdno);
            objSqlPara[2] = new SqlParameter("@DateFrom", strDateFrom);
            objSqlPara[3] = new SqlParameter("@DateTo", strDateTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookRpt", objSqlPara);
            return objDataSet;
        }

        // Account Book's  Opening Balance Function
        public DataTable OpeningBalGrid(string conString, string strAction, string strDateFrom, string strDateTo, int intyeatidno, Int64 intPartyIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", strDateFrom);
            objSqlPara[2] = new SqlParameter("@DateTo", strDateTo);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", intyeatidno);
            objSqlPara[4] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            DataTable Ndt = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookOpeningBal", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            {
                Ndt = objDataSet.Tables[0];
            }
            return Ndt;
        }

        //Account Book's Receivable Payable Function
        public DataSet FillReceivablePayable(string conString, string strAction, string strDateFrom, string strDateTo, Int64 PartyIdno, Int64 CompIdno, int intyearIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", strDateFrom);
            objSqlPara[2] = new SqlParameter("@DateTo", strDateTo);
            objSqlPara[3] = new SqlParameter("@ACNT_IDNO", PartyIdno);
            objSqlPara[4] = new SqlParameter("@COMPIDNO", CompIdno);
            objSqlPara[5] = new SqlParameter("@YEAR_IDNO", intyearIdno);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookRecPay", objSqlPara);
            return objDataSet;
        }

        // Account Book's Trial Balance Function
        public DataSet FillTrialBalanceGrid(string conString, string strAction, string strDateFrom, string strDateTo, int intyearIdno, string strGroup)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@DATEFROM", strDateFrom);
            objSqlPara[2] = new SqlParameter("@DATETO", strDateTo);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", intyearIdno);
            objSqlPara[4] = new SqlParameter("@AGRP_IDNO", strGroup);

            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookTrialBal", objSqlPara);
            return objDataSet;
        }

        public DataSet FillTrialBalanceGrid(string conString, string strAction, string strDateFrom, string strDateTo, int intyearIdno, string strGroup,Int32 Type,Int32 PartyID)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@ACTION", strAction);
            objSqlPara[1] = new SqlParameter("@DATEFROM", strDateFrom);
            objSqlPara[2] = new SqlParameter("@DATETO", strDateTo);
            objSqlPara[3] = new SqlParameter("@YEAR_IDNO", intyearIdno);
            objSqlPara[4] = new SqlParameter("@AGRP_IDNO", strGroup);
            objSqlPara[5] = new SqlParameter("@LorryType", Type);
            objSqlPara[6] = new SqlParameter("@partyID",PartyID);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spAccBookTrialBalPartyWise", objSqlPara);
            return objDataSet;
        }

        public DataSet FilllDayWiseBalgrid(string conString, string strActions, Int64 intPartyIdno,  string strDateFrom, string strDateTo)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strActions);
            objSqlPara[1] = new SqlParameter("@ACNT_IDNO", intPartyIdno);
            objSqlPara[2] = new SqlParameter("@DateFrom", strDateFrom);
            objSqlPara[3] = new SqlParameter("@DateTo", strDateTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDayWiseBalRpt", objSqlPara);
            return objDataSet;
        }
        //public IList FillPartyDetail(Int64 PartyIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from Acnt in db.AcntMasts
        //                   join ADetl in db.AcntDetls on Acnt.Acnt_Idno equals ADetl.Acnt_Idno into AD
        //                   from ACD in AD.DefaultIfEmpty()
        //                   join CM in db.tblCityMasters on ACD.CityId equals CM.City_Idno into CM
        //                   from CMD in CM.DefaultIfEmpty()
        //                   join StM in db.tblStateMasters on ACD.StateId equals StM.State_Idno into StM
        //                   from SMD in StM.DefaultIfEmpty()
        //                   where Acnt.Acnt_Idno == PartyIdno
        //                   select
        //                       new
        //                       {
        //                           Acnt.Acnt_Name,
        //                           ACD.Temp_Adrs,
        //                           ACD.Temp_Adrs2,
        //                           CityName = CMD.Name,
        //                           ACD.Prty_Pin,
        //                           StateName = SMD.Name
        //                       }
        //                   ).ToList();
        //        return lst;

        //    }
        //}
        public tblChlnBookHead SelectChallanType(Int64 ChlnID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnBookHead obj = (from t in db.tblChlnBookHeads where t.Chln_Idno == ChlnID select t).FirstOrDefault();
                return obj;
            }
        }
        public tblInvGenHead SelectInvoiceType(Int64 ChlnID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblInvGenHead obj = (from t in db.tblInvGenHeads where t.Inv_Idno == ChlnID select t).FirstOrDefault();
                return obj;
            }
        }
    }
}