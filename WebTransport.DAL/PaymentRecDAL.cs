using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
namespace WebTransport.DAL
{
    public class PaymentRecDAL
    {
        public Int32 Insert(DataTable DtTemp, Int32 ChlnIdno, double NetAmnt)
        {
            Int32 HeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        IList<tblPayRecAgChlnBk> QH = db.tblPayRecAgChlnBks.Where(qh => qh.Chln_Idno == ChlnIdno).ToList();
                        if (QH != null)
                        {
                            foreach (tblPayRecAgChlnBk qtd in QH)
                            {
                                db.tblPayRecAgChlnBks.DeleteObject(qtd);
                                db.SaveChanges();
                            }
                            //tblPayRecAgChlnBk objpay = new tblPayRecAgChlnBk();
                            //foreach (DataRow Dt in DtTemp.Rows)
                            //{
                            //    //Sumry_No,Recvng_date,Recvng_Amnt,Remark
                            //    objpay.Chln_Idno = ChlnIdno;
                            //    objpay.Sumry_No = Convert.ToString(Dt["Sumry_No"]);
                            //    objpay.Recvng_date = Convert.ToDateTime((Dt["Recvng_date"]).ToString());
                            //    objpay.Recvng_Amnt = Convert.ToDouble(Dt["Recvng_Amnt"]);
                            //    objpay.Remark = Convert.ToString(Dt["Remark"]);
                            //    objpay.Net_Amnt = NetAmnt;
                            //    db.tblPayRecAgChlnBks.AddObject(objpay);
                            //    db.SaveChanges();
                            //}
                            //dbTran.Commit();
                        }
                        //else
                        //{

                        foreach (DataRow Dt in DtTemp.Rows)
                        {
                            tblPayRecAgChlnBk obj = new tblPayRecAgChlnBk();
                            //Sumry_No,Recvng_date,Recvng_Amnt,Remark
                            obj.Chln_Idno = ChlnIdno;
                            obj.Sumry_No = Convert.ToString(Dt["Sumry_No"]);
                            obj.Recvng_date = Convert.ToDateTime((Dt["Recvng_date"]).ToString());
                            obj.Recvng_Amnt = Convert.ToDouble(Dt["Recvng_Amnt"]);
                            obj.Remark = Convert.ToString(Dt["Remark"]);
                            obj.Net_Amnt = NetAmnt;
                            db.tblPayRecAgChlnBks.AddObject(obj);
                            db.SaveChanges();
                        }
                        dbTran.Commit();
                        //  }
                    }
                    catch (Exception Ex)
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return HeadId = 1;
        }
        public DataTable FillChlnDetMast(string ConString, int Id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "FillChlnDetMast");
                objSqlPara[1] = new SqlParameter("@Id", Id);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spPayRecAgChlnBk", objSqlPara);
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
        public Double FillChlnDetNetAmnt(string ConString, int id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "FillChlnDetNetAmnt");
                objSqlPara[1] = new SqlParameter("@Id", id);
                Double Amnt = Convert.ToDouble(SqlHelper.ExecuteScalar(ConString, CommandType.StoredProcedure, "spPayRecAgChlnBk", objSqlPara));

                return Amnt;
            }
        }
        public int Delete(int intChlnIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblPayRecAgChlnBk objpay = (from mast in db.tblPayRecAgChlnBks
                                                where mast.Chln_Idno == intChlnIdno
                                                select mast).FirstOrDefault();
                    if (objpay != null)
                    {
                        db.tblPayRecAgChlnBks.DeleteObject(objpay);
                        db.SaveChanges();
                        intValue = 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                {
                    intValue = -1;
                }
            }
            return intValue;
        }
        public DataTable SelectPayDet(string ConString, int id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "SelectPayDet");
                objSqlPara[1] = new SqlParameter("@Id", id);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spPayRecAgChlnBk", objSqlPara);
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
        public DataTable FetchChallanDetail(string ConString, int yearid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "SelectChlnDet");
                objSqlPara[1] = new SqlParameter("@YearIdno", yearid);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spChlnConfirm", objSqlPara);
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
}
