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
    public class AnnexureReportDAL
    {       

        public List<tblCityMaster> BindDelvryPlace()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }

        public DataTable SelectRep(string strAction, string strBillNo, string AnexureNo , Int64 YearIdno,string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@BillNo", strBillNo);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[3] = new SqlParameter("@AnexureNo", AnexureNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAnnexure", objSqlPara);
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
        public IList selectannexureno(Int64 billno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                    var lst = (from AM in db.tblInvGenDetls 
                               join id in db.tblInvGenHeads on AM.InvGenHead_Idno equals id.Inv_Idno
                               where id.Inv_No ==billno
                           select new
                           {
                              Name= AM.Annexure_No,
                              Value=  AM.Annexure_No,
                           } ).Distinct().ToList();

                return lst;
            }
        }
        public DataTable SelectRepSummary(string strAction, string strBillNo, string AnexureNo, Int64 YearIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@BillNo", strBillNo);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[3] = new SqlParameter("@AnexureNo", AnexureNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAnnexure", objSqlPara);
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
}
