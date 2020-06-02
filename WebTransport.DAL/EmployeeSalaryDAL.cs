using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class EmployeeSalaryDAL
    {

        public DataTable MonthNameList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MonthId", typeof(int));
            table.Columns.Add("MonthName", typeof(string));
            for (int month = 1; month <= 12; month++)
            {
                string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                table.Rows.Add(month.ToString(), monthName);
            }
            return table;
        }

        public DataTable YearList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("YearId", typeof(int));
            table.Columns.Add("YearName", typeof(string));

            Int32 Year = Convert.ToInt32(System.DateTime.Now.Year.ToString());

            for (int year = 2016; year <= Year; year++)
            {
                table.Rows.Add(year.ToString(), year);
            }
            return table;
        }

        public Int64 InsertEmployeeDetl(Int64 Emp_ID, double EmpSalary, Int32 MonthID, Int32 year, bool Status, DateTime DateAdd)
        {
            Int64 intValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblEmpSalary objSal = new tblEmpSalary();
                objSal.EmpSal_EmpID = Emp_ID;
                objSal.EmpSal_Salary = EmpSalary;
                objSal.EmpSal_Month = MonthID;
                objSal.EmpSal_Year = year;
                objSal.EmpSal_Status = Status;
                objSal.Date_Added = DateAdd;

                //if (IsExists(EmpName, 0) == true)
                //{
                //    intValue = -1;
                //}
                //else
                //{

                db.tblEmpSalaries.AddObject(objSal);
                db.SaveChanges();
                intValue = objSal.EmpSal_ID;
                // }
            }

            return intValue;
        }

        public Int64 UpdateEmployeeDetl(Int64 EmpSalryID, Int64 Emp_ID, double EmpSalary, Int32 MonthID, Int32 year, bool Status, DateTime DateAdd)
        {
            Int64 intValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblEmpSalary objSal = (from mast in db.tblEmpSalaries
                                       where mast.EmpSal_ID == EmpSalryID
                                       select mast).FirstOrDefault();
                if (objSal != null)
                {
                    objSal.EmpSal_EmpID = Emp_ID;
                    objSal.EmpSal_Salary = EmpSalary;
                    objSal.EmpSal_Month = MonthID;
                    objSal.EmpSal_Year = year;
                    objSal.EmpSal_Status = Status;
                    objSal.Date_Modified = DateAdd;
                    //if (IsExists(EmpName, EmpSalryID) == true)
                    //{
                    //    intValue = -1;
                    //}
                    //else
                    //{
                    db.SaveChanges();
                    intValue = EmpSalryID;
                    //}
                }
            }
            return intValue;
        }

        //public bool IsExists(string strItemName, Int64 intItemIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        tblEmpSalary objsale = new tblEmpSalary();
        //        if (intItemIdno <= 0)
        //        {
        //            objsale = (from mast in db.tblEmpSalaries
        //                       where mast.EmpSal_Name == strItemName
        //                       select mast).FirstOrDefault();
        //        }
        //        else if (intItemIdno > 0)
        //        {
        //            objsale = (from mast in db.tblEmpSalaries
        //                       where mast.EmpSal_Name == strItemName
        //                                 && mast.EmpSal_ID != intItemIdno
        //                       select mast).FirstOrDefault();
        //        }
        //        if (objsale != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}

        public IList SelectForSearch(string EmpName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var listDtls = (from obj in db.tblEmpSalaries
                                join AM in db.AcntMasts on obj.EmpSal_EmpID equals AM.Acnt_Idno
                                where AM.Acnt_Name.Contains(EmpName)
                                select new
                                {
                                    AM.Acnt_Name,
                                    obj.EmpSal_ID,
                                    obj.EmpSal_Month,
                                    obj.EmpSal_Salary,
                                    obj.EmpSal_Status,
                                    obj.EmpSal_Year,
                                }

                                ).ToList();
                return listDtls;
            }
        }

        public IList SelectEmployeeDetailAtEditTime(Int32 Emp_ID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var list = (from obj in db.tblEmpSalaries
                            join AM in db.AcntMasts on obj.EmpSal_EmpID equals AM.Acnt_Idno
                            where obj.EmpSal_ID == Emp_ID
                            select new
                            {
                                AM.Acnt_Name,
                                obj.EmpSal_ID,
                                obj.EmpSal_Month,
                                obj.EmpSal_Salary,
                                obj.EmpSal_Status,
                                obj.EmpSal_Year,
                                obj.EmpSal_EmpID
                            }

                           ).ToList();
                return list;
            }
        }

        public Int32 UpdateStatus(Int32 Emp_ID, bool bStatus)
        {
            Int32 intValue = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblEmpSalary objSal = (from mast in db.tblEmpSalaries
                                       where mast.EmpSal_ID == Emp_ID
                                       select mast).FirstOrDefault();
                if (objSal != null)
                {
                    objSal.EmpSal_Status = bStatus;

                    db.SaveChanges();
                    intValue = Emp_ID;
                }
            }

            return intValue;
        }

        public Int32 Delete(Int32 Emp_ID)
        {
            Int32 retVal = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblEmpSalary objsal = (from obj in db.tblEmpSalaries where obj.EmpSal_ID == Emp_ID select obj).FirstOrDefault();
                if (objsal != null)
                {
                    db.tblEmpSalaries.DeleteObject(objsal);
                    db.SaveChanges();
                    retVal = Emp_ID;
                }
            }
            return retVal;
        }

        public List<AcntMast> FetchEmployeeName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 9 orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
    }
}
