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
   public class GrPrepOntohireDAL
   {
       #region Insert...
       public Int64 InsertInGrExcel(DateTime? date, Int64 GRNo, DateTime? Chln_date, Int64 ChNo, string fromcity, string Tocity, string LoryNo, string OName,string Panno, Int64 dspQty, double Rate, double amount,Int64 Year_idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;
                try
                {
                    tblGrPrepOntoHire Obj = new tblGrPrepOntoHire();
                    Obj.Gr_Date = date;
                    Obj.Gr_No = GRNo;
                    Obj.Chln_Date = Chln_date;
                    Obj.Chln_No = ChNo;
                    Obj.From_City = fromcity;
                    Obj.To_City = Tocity;
                    Obj.Lorry_No = LoryNo;
                    Obj.Owner_Name = OName;
                    Obj.Pan_no = Panno;
                    Obj.Dsp_Qty = dspQty;
                    Obj.Rate = Rate;
                    Obj.Total_Amnt = amount;
                    Obj.Year_Idno = Year_idno;
                    db.tblGrPrepOntoHires.AddObject(Obj);
                    
                    db.SaveChanges();
                    value = Obj.Gr_Idno;
                }
                catch (Exception Exe)
                {
                    value = -1;
                }
                return value;
            }
        }
       #endregion

       #region Select....
       public DataTable SelectGrPrep(Int64 YearId, string dtFrmDate, string dtToDate,string con)
       {
           SqlParameter[] objSqlPara = new SqlParameter[4];
           objSqlPara[0] = new SqlParameter("@Action", "SelectGrDetail");
           objSqlPara[1] = new SqlParameter("@YearIdno", YearId);
           objSqlPara[2] = new SqlParameter("@FromDate", dtFrmDate);
           objSqlPara[3] = new SqlParameter("@ToDate", dtToDate);

           DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSelectGrPepOntoHire", objSqlPara);
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

       #region Delete...
       public int DeleteHireGrDetail(int Gr_idno)
       {
           int value = 0;
           using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
           {
               tblGrPrepOntoHire IH = db.tblGrPrepOntoHires.Where(h => h.Gr_Idno == Gr_idno).FirstOrDefault();
              if (IH != null)
               { 
                  db.tblGrPrepOntoHires.DeleteObject(IH);
                   db.SaveChanges();
                   value = 1;
               }
           }
           return value;
       }
       #endregion
   }
}
