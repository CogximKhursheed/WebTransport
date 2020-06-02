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
    public  class TruckwiseRepDAL
    {
        public IList SearchLorry(string LorryNo, DateTime? dtfrom, DateTime? dtto, int LorryType,int RepType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from LM in db.LorryMasts
                           //where ((LM.Ins_Valid_Date >= dtfrom) && (LM.Ins_Valid_Date <= dtto))
                           //|| ((LM.RC_Date >= dtfrom) && (LM.RC_Date <= dtto))
                           //|| ((LM.RC_Date >= dtfrom) && (LM.RC_Date <= dtto))
                           //|| ((LM.Fitness_Date >= dtfrom) && (LM.Fitness_Date <= dtto))
                           select new
                           {
                               LM.Lorry_No,
                               LM.Lorry_Type,
                               LicDate=LM.ExpLicence_Date,
                               Insurance=LM.Ins_Valid_Date,
                               InsNo=LM.Ins_No,
                               RC=LM.RC_Date,
                               FitDate=LM.Fitness_Date
                           }).ToList();
                if (LorryNo !="")
                {
                    lst = lst.Where(l => l.Lorry_No == LorryNo).ToList();
                }
                if (RepType == 1)
                {
                    lst = lst.Where(l => ((Convert.ToDateTime(l.LicDate).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.LicDate).Date <= Convert.ToDateTime(dtto).Date)) || ((Convert.ToDateTime(l.RC).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.RC).Date <= Convert.ToDateTime(dtto).Date)) || ((Convert.ToDateTime(l.FitDate).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.FitDate).Date <= Convert.ToDateTime(dtto).Date)) || ((Convert.ToDateTime(l.Insurance).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.Insurance).Date <= Convert.ToDateTime(dtto).Date))).ToList();
                    //lst = lst.Where(l => (Convert.ToDateTime(l.RC).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.RC).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                    //lst = lst.Where(l => (Convert.ToDateTime(l.FitDate).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.FitDate).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                    //lst = lst.Where(l => (Convert.ToDateTime(l.Insurance).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.Insurance).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                }
                else if (RepType == 2)
                {
                    lst = lst.Where(l => (Convert.ToDateTime(l.LicDate).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.LicDate).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                }
                else if (RepType == 3)
                {
                    lst = lst.Where(l => (Convert.ToDateTime(l.RC).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.RC).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                }
                else if (RepType == 4)
                {
                    lst = lst.Where(l => (Convert.ToDateTime(l.FitDate).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.FitDate).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                }
                else if (RepType == 5)
                {
                    lst = lst.Where(l => (Convert.ToDateTime(l.Insurance).Date >= Convert.ToDateTime(dtfrom).Date) && (Convert.ToDateTime(l.Insurance).Date <= Convert.ToDateTime(dtto).Date)).ToList();
                }
                if (LorryType == 2)
                {
                    lst = lst.Where(l => l.Lorry_Type == 0).ToList();
                }
                else if (LorryType == 3)
                {
                    lst = lst.Where(l => l.Lorry_Type == 1).ToList();
                }
                
                return lst;
            }
        }
    }
}
