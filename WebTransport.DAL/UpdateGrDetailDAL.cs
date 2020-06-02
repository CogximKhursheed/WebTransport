using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using System.Web;


namespace WebTransport.DAL
{
  public class UpdateGrDetailDAL
  {
      #region DECLARE VARIABLES
      string sqlSTR = string.Empty;
      #endregion
      #region UPDATE SELECT DETAIL
      public DataSet SelecGRdetl(string Constring, string Action, Int64 Locid, Int64 Grno,Int64 YearId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[4];
                objSqlParameter[0] = new SqlParameter("@Action", Action);
                objSqlParameter[1] = new SqlParameter("@GrNO", Grno);
                objSqlParameter[2] = new SqlParameter("@LOCID", Locid);
                objSqlParameter[3] = new SqlParameter("@YearId", YearId);
                DataSet ds = SqlHelper.ExecuteDataset(Constring, CommandType.StoredProcedure, "spUpdateGr", objSqlParameter);
                return ds;
            }
        }

        public DataSet UpdateGrDetl(string Constring, string Action, Int64 Qty, double Weight, Int64 Grno, Int64 Locid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[6];
                objSqlParameter[0] = new SqlParameter("@Action", Action);
                objSqlParameter[1] = new SqlParameter("@Qty", Qty);
                objSqlParameter[2] = new SqlParameter("@Weight", Weight);
                objSqlParameter[3] = new SqlParameter("@GrNO", Grno);
                objSqlParameter[4] = new SqlParameter("@Locid", Locid);
                DataSet ds = SqlHelper.ExecuteDataset(Constring, CommandType.StoredProcedure, "spUpdateGr", objSqlParameter);
                return ds;
            }
        }

        public bool UpdateGrDetl(DataTable Dttemp, Int64 LocId)
        {
            Int64 Grdetlidno = 0;
            bool value = false;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (Dttemp.Rows.Count > 0)
                    {
                        foreach (DataRow Dr in Dttemp.Rows)
                        {

                            Grdetlidno = Convert.ToInt64(Dr["GrDetl_Idno"]);
                            TblGrDetl objtblGrdetl = (from obj in db.TblGrDetls
                                                      where obj.GrDetl_Idno == Grdetlidno
                                                      select obj).SingleOrDefault();
                            objtblGrdetl.Qty = Convert.ToInt32(Dr["Qty"]);
                            objtblGrdetl.Tot_Weght = Convert.ToDouble(Dr["Weight"]);
                            db.SaveChanges();
                            value = true;
                        }
                    }

                }

            }

            catch (Exception ex)
            {
                value = false;
            }
            return value;
        }
      #endregion

       

  }
}
