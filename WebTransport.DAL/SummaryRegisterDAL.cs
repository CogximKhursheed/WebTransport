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
    public class SummaryRegisterDAL
    {
        string sqlSTR = "";

        #region Select
        public tblSummaryRegister SelectHead(int HeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblSummaryRegisters.Where(tgh => (tgh.SumReg_Idno == HeadIdno)).FirstOrDefault();
            }
        }

        public IList SelectSummary(int SummryNo, int cityto, int truckidno, int chlnno, int Driveridno, DateTime? dtfrom, DateTime? dtto, Int32 yearidno, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblSummaryRegisters
                           join cito in db.tblCityMasters on hd.FromCity_Idno equals cito.City_Idno
                           //join acnts in db.AcntMasts on hd.Driver_idno equals acnts.Acnt_Idno
                           join trk in db.LorryMasts on hd.Truck_Idno equals trk.Lorry_Idno

                           select new
                           {
                               hd.SumReg_Idno,
                               hd.SumReg_No,
                               hd.Chln_no,
                               hd.FromCity_Idno,
                               hd.SumReg_Date,
                               trk.Lorry_No,
                               hd.Net_Amnt,
                               hd.Truck_Idno,
                               hd.Driver_idno,
                               CityTo = cito.City_Name,
                               driver = ((trk.Lorry_Type == 0) ? ((from obj in db.AcntMasts where obj.Acnt_Idno == hd.Driver_idno select obj.Acnt_Name).FirstOrDefault()) : ((from obj1 in db.DriverMasts where obj1.Driver_Idno == hd.Driver_idno select obj1.Driver_Name).FirstOrDefault())),
                               Truckno = trk.Lorry_No,
                               Year_Idno = hd.Year_Idno

                           }).ToList();
                if (SummryNo > 0)
                {
                    lst = lst.Where(l => l.SumReg_No == SummryNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.SumReg_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.SumReg_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.FromCity_Idno == cityto).ToList();
                }
                if (truckidno > 0)
                {
                    lst = lst.Where(l => l.Truck_Idno == truckidno).ToList();
                }
                if (chlnno > 0)
                {
                    lst = lst.Where(l => l.Chln_no == chlnno).ToList();
                }

                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (Driveridno > 0)
                {
                    lst = lst.Where(l => l.Driver_idno == Driveridno).ToList();
                }
                //else if (UserIdno > 0)
                //{
                //    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                //    lst = lst.Where(o => CityLst.Contains(o.From_City)).ToList();
                //}
                return lst;
            }
        }

        public List<DriverMast> selectHireDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<DriverMast> lst = null;
                lst = (from DR in db.DriverMasts orderby DR.Driver_Name ascending select DR).ToList();
                return lst;
            }
        }

        public Int64 MaxNo(Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Sumreg_No),0) + 1 AS MAXID FROM tblsummaryregister WHERE  fromCity_Idno='" + FromCityIdno + "'  AND Year_idno=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public DataTable SelectChlnDetail(string Action, DateTime dtfromDate, DateTime dtToDate, Int64 CityIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtfromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", CityIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSummaryRegister", objSqlPara);
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

        public DataTable selectDetails(string Action, string AllItmIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@Id", AllItmIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSummaryRegister", objSqlPara);
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

        public DataTable SelectRep(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 UserIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@UserIdno", UserIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSummaryRegister", objSqlPara);
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
        public DataTable SelectreportwithCity(string strAction, DateTime dtFromDate, DateTime dtToDate, Int64 iFrmCityIdno, Int64 UserIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", strAction);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@ToCityIdno", iFrmCityIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSummaryRegister", objSqlPara);
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

        #region Insert/Update/Delete
        public Int64 Insert(tblSummaryRegister obj)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblSummaryRegister AmtHead = new tblSummaryRegister();
                db.Connection.Open();
                try
                {
                    tblSummaryRegister RH = db.tblSummaryRegisters.Where(rh => rh.SumReg_No == obj.SumReg_No && rh.FromCity_Idno == obj.FromCity_Idno && rh.Year_Idno == obj.Year_Idno).FirstOrDefault();
                    if (RH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        db.tblSummaryRegisters.AddObject(obj);
                        db.SaveChanges();
                        AmntHeadId = obj.SumReg_Idno;
                    }
                }
                catch
                {
                }
                return AmntHeadId;
            }
        }
        public Int64 Update(tblSummaryRegister obj, Int32 Head_Idno)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                //  using (DbTransaction dbTran = db.Connection.BeginTransaction())
                // {
                try
                {
                    tblSummaryRegister CH = db.tblSummaryRegisters.Where(rh => rh.SumReg_No == obj.SumReg_No && rh.SumReg_Idno != Head_Idno && rh.FromCity_Idno == obj.FromCity_Idno && rh.Year_Idno == obj.Year_Idno).FirstOrDefault();
                    if (CH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        tblSummaryRegister TBH = db.tblSummaryRegisters.Where(rh => rh.SumReg_Idno == Head_Idno).FirstOrDefault();
                        if (TBH != null)
                        {
                            TBH.SumReg_No = obj.SumReg_No;
                            TBH.FromCity_Idno = obj.FromCity_Idno;
                            TBH.SumReg_Date = obj.SumReg_Date;
                            TBH.Year_Idno = obj.Year_Idno;
                            TBH.Chln_Idno = obj.Chln_Idno;
                            TBH.Crossing_Amnt = obj.Crossing_Amnt;
                            TBH.Delivery_Amnt = obj.Delivery_Amnt;
                            TBH.Driver_idno = obj.Driver_idno;
                            TBH.Freight_Amnt = obj.Freight_Amnt;
                            TBH.Katt_Amnt = obj.Katt_Amnt;
                            TBH.Labour_Amnt = obj.Labour_Amnt;
                            TBH.Net_Amnt = obj.Net_Amnt;
                            TBH.Date_Modified = obj.Date_Modified;
                            TBH.Octrai_Amnt = obj.Octrai_Amnt;
                            TBH.Other_Charges = obj.Other_Charges;
                            TBH.Total_Amnt1 = obj.Total_Amnt1;
                            TBH.Total_Amnt2 = obj.Total_Amnt2;
                            TBH.Truck_Idno = obj.Truck_Idno;
                            TBH.Way_Amnt = obj.Way_Amnt;
                            TBH.Date_Modified = DateTime.Now;
                            db.SaveChanges();
                            AmntHeadId = obj.SumReg_Idno;
                            AmntHeadId = TBH.SumReg_Idno;
                        }
                    }
                }
                catch
                {
                }
            }
            return AmntHeadId;
        }
        public int Delete(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblSummaryRegister qth = db.tblSummaryRegisters.Where(h => h.SumReg_Idno == HeadId).FirstOrDefault();
                if (qth != null)
                {
                    db.tblSummaryRegisters.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
        #endregion


        public Int64 TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from hd in db.tblSummaryRegisters
                         join cito in db.tblCityMasters on hd.FromCity_Idno equals cito.City_Idno
                         join trk in db.LorryMasts on hd.Truck_Idno equals trk.Lorry_Idno

                         select hd.SumReg_Idno).Count();

                return Count;
            }

        }

    }
}
