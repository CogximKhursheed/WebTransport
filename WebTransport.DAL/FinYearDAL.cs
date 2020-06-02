using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using WebTransport;

namespace WebTransport.DAL
{
    public class FinYearDAL
    {
        #region Year wise DateRange For All
        public DataTable FillYrwiseDateRange(string conString)
        {
            string strSQL = string.Empty;
            DataTable Ndt = new DataTable();
            if (conString == "Data Source=136.243.149.22,1443;Initial Catalog=;Integrated Security=False;User ID=sa;Password=41kc*mRq4IWyUK5eW6E")
            {
                conString = MultipleDBDAL.strDynamicConString();
            }
            else
            {
                strSQL = @"select Fin_Idno as Id,(convert(nvarchar(11),convert(datetime,StartDate,105),106) +space(3) + 'to'+ space(3)+ convert(nvarchar(11),convert(datetime,EndDate,105),106)) as DateRange from tblFinYear  order by Fin_Idno desc";
                DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Ndt = ds.Tables[0];
                }
                return Ndt;
            }
            return Ndt;
        }
        public IList FilldateFromTo(Int32 yearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var obj = (from FY in db.tblFinYears where FY.Fin_Idno == yearIdno orderby FY.Fin_Idno select FY).ToList();
                return obj;
            }
        }
        public Int32 ValidDateCheck(string conString, string DF, string DT, Int32 yearIdno)
        {
            string strSQL = string.Empty;
            Int32 Check = 0;
            strSQL = @"select * from finyear where Fin_Idno =" + yearIdno + " and convert(datetime,StartDate,105) <= convert(datetime,'" + DF +
                "',105) and convert(datetime,EndDate,105) >= convert(datetime,'" + DT + "',105)";
            DataSet ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, strSQL);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Check = 1;
            }
            return Check;
        }
        public IList FillYrwiseDateRange()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblFinYears.OrderByDescending(obj => obj.Fin_Idno).AsEnumerable().Select(obj => new { Id = obj.Fin_Idno, DateRange = (Convert.ToDateTime(obj.StartDate).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(obj.EndDate).ToString("dd-MMM-yyyy")).ToString() }).ToList();
            }
        }
        public Int64 SelectCurrentYear(DateTime date)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = (from CM in db.tblFinYears where CM.StartDate <= date orderby CM.StartDate descending select CM.Fin_Idno).FirstOrDefault();
                return lst;
            }
        }
        #endregion
    }
}