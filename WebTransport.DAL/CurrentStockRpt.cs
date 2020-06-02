using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class CurrentStockRpt
    {
        public List<tblItemMastPur> BindActiveItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               join ITM in db.tblItemTypeMasts on obj.ItemType equals ITM.ItemTpye_Idno
                               where obj.Status == true && ITM.ItemTpye_Idno == 1
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<tblItemMastPur> BindActiveAssItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               join ITM in db.tblItemTypeMasts on obj.ItemType equals ITM.ItemTpye_Idno
                               where obj.Status == true && ITM.ItemTpye_Idno == 2
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public DataTable SelectCurrentStockReport(string con, DateTime? dtfrom, Int64 ItemIdno, string serailNum, Int64 LocIdno, Int64 YearID)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", "SelectCurrentStockReport");
            objSqlPara[1] = new SqlParameter("@FromDate", dtfrom);
            objSqlPara[2] = new SqlParameter("@ItemIdno", ItemIdno);
            objSqlPara[3] = new SqlParameter("@SerialNum", serailNum);
            objSqlPara[4] = new SqlParameter("@LocIdno", LocIdno);
            objSqlPara[5] = new SqlParameter("@YearID", YearID);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSelectCurrentStockReport", objSqlPara);
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }

            return objDtTemp;
        }
        public DataTable SelectCurrentStockSummary(string con, Int64 YearIdno, DateTime? dtfrom, DateTime? dtTo, Int64 LocIdno, Int64 ItemIdno)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", "SelectAcce ssSummary");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@From_Date", dtfrom);
            objSqlPara[3] = new SqlParameter("@To_Date", dtTo);
            objSqlPara[4] = new SqlParameter("@LocIdno", LocIdno);
            objSqlPara[5] = new SqlParameter("@ItemIdno", ItemIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spStckSummary", objSqlPara);
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }

            return objDtTemp;
        }
        public Stckdetl GetItemInfo(string serialNum)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from stc in db.Stckdetls where stc.SerialNo == serialNum select stc).FirstOrDefault();

            }
        }
    }
}
