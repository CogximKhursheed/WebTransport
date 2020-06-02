using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WebTransport;

namespace WebTransport.DAL
{
    public class AccessoryStockRpt
    {
        public List<tblItemMastPur> BindActiveItemName()
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

        public DataTable SelectAccStockReport(string con, string dtfrom, string dtto, Int32 yearidno, string ItemIdno, Int32 ItemType,Int64 LocIdno)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", "InsertStckRepTemp");
            objSqlPara[1] = new SqlParameter("@Date", dtfrom);
            objSqlPara[2] = new SqlParameter("@DateTo", dtto);
            objSqlPara[3] = new SqlParameter("@Items", ItemIdno);
            objSqlPara[4] = new SqlParameter("@ITEM_TYPE", ItemType);
            objSqlPara[5] = new SqlParameter("@LOC_IDNO", LocIdno);
            objSqlPara[6] = new SqlParameter("@iPurThrRec", 1);
            objSqlPara[7] = new SqlParameter("@Year_Idno", yearidno);
            DataSet objDsTemp = new DataSet();
            objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAccReport", objSqlPara);
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }

        public DataTable FetchAccStockReport(string con)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", "SelectStckRepTemp");
            DataSet objDsTemp = new DataSet();
            objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAccReport", objSqlPara);
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }
            return objDtTemp;
        }
        public DataTable FetchItemIDno(string con)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", "SelectItem");
            DataSet objDsTemp = new DataSet();
            objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAccReport", objSqlPara);
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
