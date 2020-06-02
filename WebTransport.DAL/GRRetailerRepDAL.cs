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
    public class GRRetailerRepDAL
    {
        #region GR Report  ...

        public List<AcntMast> BindRecever()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
        public List<AcntMast> BindSender()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 orderby cm.Acnt_Name ascending select cm).ToList();
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
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, Int64 iSenderIdno, Int64 GRNo, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[5] = new SqlParameter("@GR_No", GRNo);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRRetRep", objSqlPara);
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
