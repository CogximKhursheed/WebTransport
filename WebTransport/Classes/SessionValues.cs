using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebTransport.DAL;

namespace WebTransport.Classes
{
    public class SessionValues
    {
        public FinYearA FatchFinYear(int CompId)
        {
            FinYearA objFinYear = new FinYearA();
            //clsUser userBLL = new clsUser();
            //var lst = userBLL.CurrentFinYear(CompId);
            //if (lst != null)
            //{
            //    objFinYear.StartDate = Convert.ToDateTime(lst.StartDate);
            //    objFinYear.EndYear = Convert.ToDateTime(lst.EndDate);
            //    objFinYear.YearId = Convert.ToInt32(lst.Fin_Idno);
            //}
            return objFinYear;
        }
    }
    public class FinYearA
    {
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndYear
        {
            get;
            set;
        }

        public int YearId
        {
            get;
            set;
        }
    }

}