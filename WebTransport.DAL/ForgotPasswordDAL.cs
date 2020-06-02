using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;


namespace WebTransport.DAL
{
    public class ForgotPasswordDAL
    {


        #region SelectUserForgotPassword
        public DataTable SelectUserForgotPassword(string conString, string EmailAddress)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "ForgotPassword");
            objSqlPara[1] = new SqlParameter("@Eamil", EmailAddress);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spForgotPassword", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }

        public DataTable CheckUserForgotLink(string conString, string UID, string USID, string QryRestPswd)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "CheckDetails");
            objSqlPara[1] = new SqlParameter("@UID", Convert.ToInt64(UID));
            objSqlPara[2] = new SqlParameter("@USID", Convert.ToInt32(USID));
            objSqlPara[3] = new SqlParameter("@ResetCode", QryRestPswd);
            objSqlPara[4] = new SqlParameter("@DateTime", System.DateTime.Now);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spForgotPassword", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }

        public DataTable UpdateUserPassword(string conString, string UID, string USID, string password)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "UpdatePswd");
            objSqlPara[1] = new SqlParameter("@UID", Convert.ToInt64(UID));
            objSqlPara[2] = new SqlParameter("@USID", Convert.ToInt32(USID));
            objSqlPara[3] = new SqlParameter("@Password", password);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spForgotPassword", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }

        public tblEmailTemplate SelectUserMailTemplates(string Title)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from obj in db.tblEmailTemplates where obj.Status == true && obj.Title == Title select obj).FirstOrDefault();
                return lst;
            }
        }

        #endregion
    }
}
