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
    public class TyreStockRpt
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

        public DataTable SelectTyreStockReport(string con, string dtfrom, string dtto, Int32 yearidno, Int64 ItemIdno, Int32 TyreType,Int64 LocIdno)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", "SelectTyreStockReport");
            objSqlPara[1] = new SqlParameter("@FromDate", dtfrom);
            objSqlPara[2] = new SqlParameter("@ToDate", dtto);
            objSqlPara[3] = new SqlParameter("@ItemIdno", ItemIdno);
            objSqlPara[4] = new SqlParameter("@TyreType", TyreType);
            objSqlPara[5] = new SqlParameter("@LocIdno", LocIdno);
            DataSet objDsTemp = new DataSet();
            objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSelectTyreStockReport", objSqlPara);
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
