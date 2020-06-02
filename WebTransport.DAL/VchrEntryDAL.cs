using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;

namespace WebTransport.DAL
{
    public class VchrEntryDAL
    {
        #region Select Events...

        public IList selectTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from LM in db.LorryMasts orderby LM.Lorry_No ascending select LM).ToList();
                return lst;
            }
        }
        public Int64 InserCostCenterdetail(DataRow [] DrRow, Int64 VchrIdno,Int32 VCHRDETLiDNO)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (DrRow != null && DrRow.Length > 0)
                {
                    foreach (DataRow Dr in DrRow)
                    {
                        VchrCostDetl obj = new VchrCostDetl();
                        obj.Vchr_Idno = VchrIdno;
                        obj.VchrDetl_Idno = Convert.ToInt64(VCHRDETLiDNO);
                        obj.Truck_Idno = Convert.ToInt64(Dr["Truck_idno"]);
                        obj.Amount = Convert.ToDouble(Dr["Amount"]);
                        db.VchrCostDetls.AddObject(obj);
                        db.SaveChanges();
                        value = obj.Id;
                    }

                }

                return value;
            }
        }

        //public Int64 UpdateCostCenterdetail(DataTable DtCost, Int64 VchrIdno)
        //{
        //    try
        //    {
        //        Int64 value = 0;
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            var lst = (from l in db.VchrCostDetls where l.Vchr_Idno == VchrIdno select l).ToList();
        //            if (lst != null && lst.Count > 0)
        //            {
        //                foreach (var l in lst)
        //                {
        //                    db.VchrCostDetls.DeleteObject(l);
        //                    db.SaveChanges();
                           
        //                }
        //                value = 1;
        //            }

        //            if (DtCost != null && DtCost.Rows.Count > 0)
        //            {
        //                foreach (DataRow Dr in DtCost.Rows)
        //                {
        //                    VchrCostDetl obj = new VchrCostDetl();
        //                    obj.Vchr_Idno = VchrIdno;
        //                    obj.VchrDetl_Idno = Convert.ToInt64(Dr["Id"]);
        //                    obj.Truck_Idno = Convert.ToInt64(Dr["Truck_idno"]);
        //                    obj.Amount = Convert.ToDouble(Dr["Amount"]);
        //                    db.VchrCostDetls.AddObject(obj);
        //                    db.SaveChanges();
        //                    value = obj.Id;
        //                }

        //            }

        //            return value;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return 0;
        //    }
           
        //}

        public DataSet spVoucherEntry(string conString, string Action, Int64 VCHRIDNO, string VCHRDATE, string VCHRDATETO, Int32 VCHRTYPE, Int64 VCHRMODE, string VCHRNARR, int VCHRHIDN,
                                      Int32 YEARIDNO, Int32 VCHRSUSP, string VCHRFRM, Int64 ACNTIDNO, float DCNNO, Int32 PRINTED, Int64 SBILLNO, string SBILLDATE, Int64 VCHRNO, Int64 ID,
                                      string NARRTEXT, float ACNTAMNT, Int32 AMNTTYPE, Int32 INSTTYPE, string INSTNO, int DETLHIDN, string BANKDATE, string CUSTBANK, Int32 TRANTYPE,
                                      Int32 BANKDT, string RCPTNO, string RCPTDATE, int NoName, int RTypeIdno, Int64 FINIDNO, Int64 DOCIDNO, string DocType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@ACTION", Action);
            objSqlPara[1] = new SqlParameter("@VCHRTYPE", VCHRTYPE);
          //  objSqlPara[2] = new SqlParameter("@CompIdno", comp_idno);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spVoucherEntry", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataSet;
        }
        public Int64 countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
       {       // select v.Vchr_Idno   FROM VchrHead V     
              //INNER JOIN VchrDetl VD ON V.Vchr_Idno = VD.VCHr_idno     
             //INNER JOIN AcntMast A ON V.Vchr_Mode = A.Acnt_Idno  where V.Year_Idno=2 and  VD.Detl_Hidn=1 and V.Vchr_Frm=''
           // where V.Year_Idno=2 and  VD.Detl_Hidn=1 and V.Vchr_Frm=''
                Int32 count = 0;
                count = (from vchHead in db.VchrHeads
                         join vdetl in db.VchrDetls on vchHead.Vchr_Idno equals vdetl.Vchr_Idno
                         join acm in db.AcntMasts on vchHead.Vchr_Mode equals acm.Acnt_Idno where vdetl.Detl_Hidn == true && vchHead.Vchr_Frm=="" 
                         select vchHead.Vchr_Idno ).Count();
                return count;

            }
        }
        public DataTable spVoucherEntrySearch(string conString, string Action, string DateFrom, string DateTo, Int32 VCHRTYPE, Int64 VchrNo, Int64 AcntId, string Amnt,
                                              Int64 YearId, Int64 UserIdno) //, Int64 CompIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[9];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@VCHRTYPE", VCHRTYPE);
            objSqlPara[2] = new SqlParameter("@dtFrom", DateFrom);
            objSqlPara[3] = new SqlParameter("@dtTo", DateTo);
            objSqlPara[4] = new SqlParameter("@VchrNo", VchrNo);
            objSqlPara[5] = new SqlParameter("@AcntId", AcntId);
            objSqlPara[6] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[7] = new SqlParameter("@UserIdno", UserIdno);
           // objSqlPara[8] = new SqlParameter("@CompId", CompIdno);
          //  objSqlPara[9] = new SqlParameter("@CompanyId", intCompanyIdno);
            objSqlPara[8] = new SqlParameter("@Amnt", Amnt);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spVoucherSearch", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }

        public int Deletefile(int VchrIdno, Int64 UserIdno, string con)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        List<VchrDetl> objVchrDetl = (from obj in db.VchrDetls
                                                      where obj.Vchr_Idno == VchrIdno
                                                      select obj).ToList();
                        if (objVchrDetl != null)
                        {

                            SqlParameter[] objSqlPara = new SqlParameter[3];
                            objSqlPara[0] = new SqlParameter("@Action", "DeleteVoucherEntry");
                            objSqlPara[1] = new SqlParameter("@Idno", VchrIdno);
                            objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                            Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);


                            foreach (var a in objVchrDetl)
                            {
                                db.DeleteObject(a);
                            }
                            db.SaveChanges();
                            var objVchrCost = (from obj in db.VchrCostDetls where obj.Vchr_Idno == VchrIdno select obj).ToList();
                            if (objVchrCost != null)
                            {
                                foreach (var lst in objVchrCost)
                                {
                                    db.DeleteObject(lst);
                                    db.SaveChanges();
                                }
                            }
                            VchrHead objHead = (from objH in db.VchrHeads
                                                where objH.Vchr_Idno == VchrIdno
                                                select objH).FirstOrDefault();
                            if (objHead != null)
                            {
                                db.DeleteObject(objHead);
                                db.SaveChanges();
                                Value = 1;
                                tScope.Complete();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return Value;
        }

        public VchrHead fetchtblVchrHeadById(Int32 Id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from VH in db.VchrHeads where VH.Vchr_Idno == Id select VH).FirstOrDefault();
            }
        }

        public List<VchrDetl> fetchtblVchrDetlById(Int32 Id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from VD in db.VchrDetls where VD.Vchr_Idno == Id select VD).ToList();
            }
        }

        //public List<tblLocationMast> SelectFactory()
        //{
        //    using (AutomobileEntities db = new AutomobileEntities(clsMultipleDB.strDynamicConString()))
        //    {
        //        List<tblLocationMast> lst = (from Loc in db.tblLocationMasts where Loc.LocationTyp_Idno == 2 select Loc).ToList();
        //        return lst;
        //    }
        //}

        //public List<tblLocationMast> SelectStore()
        //{
        //    using (AutomobileEntities db = new AutomobileEntities(clsMultipleDB.strDynamicConString()))
        //    {
        //        return (from Loc in db.tblLocationMasts where Loc.LocationTyp_Idno == 1 select Loc).ToList();
        //    }
        //}

        //public Int64 CheckCostCenter(Int64 LedgerIdno)
        //{
        //    using (AutomobileEntities db = new AutomobileEntities(clsMultipleDB.strDynamicConString()))
        //    {
        //        return Convert.ToInt64((from AM in db.AcntMasts where AM.Acnt_Idno == LedgerIdno select AM.CostCenter_Idno).FirstOrDefault());
        //    }
        //}

        //public IList SelectCostCenter(Int64 CompanyIdno)
        //{
        //    using (AutomobileEntities db = new AutomobileEntities(clsMultipleDB.strDynamicConString()))
        //    {
        //        var lst = (from CC in db.tblCostCenterMasters
        //                   where CC.Comp_Idno == CompanyIdno
        //                   select new
        //                   {
        //                       CC.CostCenter_Idno,
        //                       CC.Name
        //                   }).ToList();
        //        return lst;
        //    }
        //}

        //public IList SelectCompDetail(int CompanyIdno)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from CC in db.CompMasts
        //                   where CC.CompMast_Idno == CompanyIdno
        //                   select new
        //                   {
        //                       CC.CompMast_Idno,
        //                       CC.Name,
        //                       Detail = CC.Adress1 + Environment.NewLine + CC.Adress2 + " - " + CC.Pin
        //                   }).ToList();
        //        return lst;
        //    }
        //}

        //public CompMast SelectCompDetails(int Comp_IDno)
        //{
        //  v
        //    {
        //        return (from CM in db.CompMasts
        //                where CM.CompMast_Idno == Comp_IDno
        //                select CM).FirstOrDefault();
        //    }
        //}

        public IList SelectAcntName() //Int64 intCompanyIdno)   where AM.Comp_Idno == intCompanyIdno 
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from AM in db.AcntMasts orderby AM.Acnt_Name ascending select new { AM.Acnt_Idno, AM.Acnt_Name }).ToList();
            }
        }

        public int SelectAcntType(Int64 intLedgerId)
        {
            int intAcntType = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AM in db.AcntMasts where AM.Acnt_Idno == intLedgerId select AM).ToList();
                if (lst != null && lst.Count > 0)
                {
                    intAcntType = Convert.ToInt32((from l in lst select l.Acnt_Type).Max());
                }
                return intAcntType;
            }
        }

        //public List<CompMast> SelectWorkComp()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var LstComp = (from obj in db.CompMasts orderby obj.Name select obj).ToList();
        //        return LstComp;
        //    }
        //}

        //public List<CompMast> SelectWorkComp1(int compid)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var LstComp = (from obj in db.CompMasts where obj.CompMast_Idno == compid orderby obj.Name select obj).ToList();
        //        return LstComp;
        //    }
        //}

        public DataTable spGetVchrCheckList(string conString, string Action, string DateFrom, string DateTo) //, Int64 CompIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@dtFrom", DateFrom);
            objSqlPara[2] = new SqlParameter("@dtTo", DateTo);
            DataTable objDataTable = new DataTable();
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spVoucherSearch", objSqlPara);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
            {
                objDataTable = objDataSet.Tables[0];
            }
            return objDataTable;
        }


        #endregion
    }
}
