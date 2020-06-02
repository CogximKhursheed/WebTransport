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
    public class GRRepDAL
    {
        #region GR Report  ...

        public List<AcntMast> BindRecever()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type==2 orderby cm.Acnt_Name ascending select cm).ToList();
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
        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iRecvrIdno, Int64 iSenderIdno, Int64 iFrmCityIdno, Int64 iDelPlcIdno, Int32 iGRTyp,Int64 UserIdno,string con)
        {
            //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            //{
            //    var lst = (from s in db.spGRRep(strAction,dtFromDate, dtToDate, iRecvrIdno, iSenderIdno, iFrmCityIdno, iDelPlcIdno, iGRTyp) select s).ToList();
            //    return lst;
            //}

            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@Recvr_Idno", iRecvrIdno);
            objSqlPara[4] = new SqlParameter("@SenderIdno", iSenderIdno);
            objSqlPara[5] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[6] = new SqlParameter("@DelPlc_Idno", iDelPlcIdno);
            objSqlPara[7] = new SqlParameter("@GR_Typ", iGRTyp);
            objSqlPara[8] = new SqlParameter("@UserIdno",UserIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRRep", objSqlPara);
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


        //public DataTable SelectRep(string Action,  DateTime dtFrmDate, DateTime dtToDate, Int64 RecvrIdno, Int64 SenderIdno, Int64 DelvryPlceIdno, Int64 GRTypIdno, Int64 FromCityIdno,  string con)
        //{
        //    SqlParameter[] objSqlPara = new SqlParameter[8];
        //    objSqlPara[0] = new SqlParameter("@Action", Action);
        //    objSqlPara[1] = new SqlParameter("@From_Date", dtFrmDate);
        //    objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
        //    objSqlPara[3] = new SqlParameter("@StrRecvrIdno", RecvrIdno);
        //    objSqlPara[4] = new SqlParameter("@StrDelPlcIdno", DelvryPlceIdno);
        //    objSqlPara[5] = new SqlParameter("@StrGRTyp", GRTypIdno);
        //    objSqlPara[6] = new SqlParameter("@StrSenderIdno", SenderIdno);
        //    objSqlPara[7] = new SqlParameter("@strFrmCityIdno", FromCityIdno);
        //public DataTable SelectRep(string Action, DateTime dtFrmDate, DateTime dtToDate, string con)
        //{
        //    SqlParameter[] objSqlPara = new SqlParameter[3];
        //    objSqlPara[0] = new SqlParameter("@Action", Action);
        //    objSqlPara[1] = new SqlParameter("@From_Date", dtFrmDate);
        //    objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
        //    DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRRep", objSqlPara);
        //    DataTable objDtTemp = new DataTable();
        //    if (objDsTemp.Tables.Count > 0)
        //    {
        //        if (objDsTemp.Tables[0].Rows.Count > 0)
        //        {
        //            objDtTemp = objDsTemp.Tables[0];
        //        }
        //    }
        //    return objDtTemp;
        //}

        #endregion

    }
}
