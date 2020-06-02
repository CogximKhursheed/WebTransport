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
    public class HireInvDAL
    {

        #region Function.....
    

        public bool IsExists(Int64 intTruckIdno,DateTime? HireDate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TblHireInvDetl objhiredetl = new TblHireInvDetl();
                 objhiredetl = (from id in db.TblHireInvDetls  where id.Truck_Idno == intTruckIdno && (id.ReturnDate > HireDate) select id).FirstOrDefault();
                
                if (objhiredetl != null)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }

            }
        }

        public Int64 MaxIdno(string con, Int64 FronCityIdno)
        {
            Int64 MaxNo = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(MAX(Hire_Idno),0)  AS MAXID FROM TblHireInvHead WHERE ViaCity_Idno =" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public tblUserPref SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                return (from obj in db.tblUserPrefs select obj).FirstOrDefault();
            }
        }

        public Int64 sender(string con, Int64 inviidno)
        {
            Int64 sendner = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(Party_Idno,0) as Sendr_Idno FROM TblHireInvDetl WHERE  Hire_Idno=" + inviidno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                sendner = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return sendner;
        }

        public bool IsLorryRetrun(Int64 intTruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TblHireInvDetl objhiredetl = new TblHireInvDetl();
                objhiredetl = (from id in db.TblHireInvDetls where id.Truck_Idno == intTruckIdno && (id.ReturnDate==null) select id).FirstOrDefault();
                if (objhiredetl != null)
                    return true;
                else
                    return false;
            }
        }
        
        public DataTable BindRcptType(string Con)
        {
            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(Con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable BindBank(string Con)
        {
            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(Con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable BindRcptTypeDetail(Int32 intAcntIdno, string con)
        {
            string sqlSTR = @"SELECT ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) and Acnt_Idno=" + intAcntIdno + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;



        }
        public Int64 MaxHireNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 intMaxHireNo = 0;
                intMaxHireNo = Convert.ToInt64((from H in db.TblHireInvHeads select H.Hire_InvNo).Max());
                return intMaxHireNo + 1;
            }
        }

        #endregion

        #region Insert Update Delete......

        public Int64 Insert(Int64 Year_Idno, DateTime? Invice_Date, Int64 Invioce_No, Int64 Loc_Idno, Int64 User_Idno, Int64 Comp_Idno, bool AccPost, string Remark,Int64 truckidno,Int64 partyidno,Int64 fromcityidno,Int64 citytoidno,Int64 viacityidno, DateTime? from_Date, DateTime? return_Date, double netamnt, double advamnt, double dieselamnt, Int32 recptidno, Int32 instno, DateTime? instdate, Int32 bankidno, double grossamnt,string GrIdno, DataTable dtitemdetail,string Con)
        {
            Int64 HireHead_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                     TblHireInvHead objHireHead = db.TblHireInvHeads.Where(bill => (bill.Hire_InvNo == Invioce_No) && (bill.Year_Idno == Year_Idno)).FirstOrDefault();
                    
                    if (objHireHead == null)
                    {
                        objHireHead = new TblHireInvHead();
                        objHireHead.Year_Idno = Year_Idno;
                        objHireHead.Hire_InvNo = Invioce_No;
                        objHireHead.Hire_Date = Invice_Date;
                        objHireHead.Date_Added = System.DateTime.Now;
                        objHireHead.Loc_Idno = Loc_Idno;
                        objHireHead.Comp_Idno= Comp_Idno;
                        objHireHead.User_Idno = User_Idno;
                        objHireHead.Acc_Post = AccPost;
                        objHireHead.Remark = Remark;
                        objHireHead.Net_Amnt = netamnt;
                        objHireHead.Gross_Amnt = grossamnt;
                        objHireHead.Adv_Amnt=advamnt;
                        objHireHead.Diesel_Amnt = dieselamnt;
                        objHireHead.RcptType_Idno=recptidno;
                        objHireHead.Inst_No=instno;
                        objHireHead.Inst_Dt=instdate;
                        objHireHead.Bank_Idno=bankidno;
                        objHireHead.Truck_Idno = Convert.ToInt64(Convert.ToString(truckidno) == "" ? 0 : truckidno);
                        objHireHead.Party_Idno = Convert.ToInt64(Convert.ToString(partyidno) == "" ? 0 : partyidno);
                        objHireHead.LocFrm_Idno = Convert.ToInt64(Convert.ToString(fromcityidno) == "" ? 0 : fromcityidno);
                        objHireHead.ViaCity_Idno = Convert.ToInt64(Convert.ToString(viacityidno) == "" ? 0 : viacityidno); 
                        objHireHead.ToCity_Idno = Convert.ToInt64(Convert.ToString(citytoidno) == "" ? 0 : citytoidno);
                        objHireHead.DateFrom = Convert.ToDateTime(from_Date);
                        objHireHead.ReturnDate=String.IsNullOrEmpty(Convert.ToString(return_Date)) ? (DateTime?)null : Convert.ToDateTime(return_Date);
                        db.TblHireInvHeads.AddObject(objHireHead);
                        db.SaveChanges();
                        HireHead_Idno = objHireHead.Hire_Idno;

                        if (HireHead_Idno > 0)
                        {
                            foreach (DataRow row in dtitemdetail.Rows)
                                {
                                    TblHireInvDetl objHireDetl = new TblHireInvDetl();
                                    objHireDetl.Hire_Idno = HireHead_Idno;
                                    objHireDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objHireDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                    objHireDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["Rate_TypeIdno"])) ? 0 : Convert.ToInt32(row["Rate_TypeIdno"]);
                                    objHireDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                    objHireDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                    objHireDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objHireDetl.Amount = Convert.ToDouble(row["Amount"]);
                                    objHireDetl.Detail = Convert.ToString(row["Detail"]);
                                    objHireDetl.UnloadWeight = Convert.ToDouble(row["UnloadWeight"]);
                                    db.TblHireInvDetls.AddObject(objHireDetl);
                                    db.SaveChanges();
                                 }
                                

                            if (GrIdno != "")
                            {
                                UpdateFlag(Con, GrIdno, HireHead_Idno);
                            }
                        }
    
                    }
                    else
                    {
                        return HireHead_Idno;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return HireHead_Idno;
            }
        }
        public TblHireInvHead SelectHireHead(Int64 HireHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TblHireInvHeads.Where(tpbh => (tpbh.Hire_Idno == HireHead_Idno)).FirstOrDefault();
            }
        }
        public IList SelectHireDetail(Int64 HireHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Ilst = (from hd in  db.TblHireInvDetls 
                           join  Lorry in db.LorryMasts on hd.Truck_Idno equals Lorry.Lorry_Idno
                           join cityFrm in db.tblCityMasters on hd.LocFrm_Idno equals cityFrm.City_Idno
                           join cityto  in db.tblCityMasters on hd.ToCity_Idno equals cityto.City_Idno
                           join acnts in db.AcntMasts on hd.Party_Idno equals acnts.Acnt_Idno
                           join ViaCity in db.tblCityMasters on hd.ViaCity_Idno equals ViaCity.City_Idno into temp
                           from ctemp in temp.DefaultIfEmpty()
                            where hd.Hire_Idno == HireHead_Idno
                            select new
                            {
                               hd.Hire_Idno,

                              
                               hd.Truck_Idno,
                               LorryNo=Lorry.Lorry_No,
                               hd.Party_Idno,
                               PartyName = acnts.Acnt_Name,
                               hd.LocFrm_Idno,
                               FromCity = cityFrm.City_Name,
                               hd.ViaCity_Idno,
                               ViaCity=ctemp.City_Name,
                               hd.ToCity_Idno,
                               Tocity= cityto.City_Name,
                               hd.DateFrom,
                               hd.ReturnDate,
                               hd.Amount,
                               hd.Item_Idno,
                               hd.Unit_Idno,
                               hd.Rate_Type
                            }
                            ).ToList();

                return Ilst;
            }
        }
        public IList SelectSerach(int HireNo, DateTime? HireDate, DateTime? HireDateTo, Int64 Loc_idno, Int32 yearidno, Int64 UserIdno )
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from PBH in db.TblHireInvHeads
                           join loc in db.tblCityMasters on PBH.Loc_Idno equals loc.City_Idno
                           select new
                           {
                               PBH.Hire_Idno,
                               PBH.Hire_InvNo,
                               PBH.Hire_Date,
                               PBH.Remark,
                               PBH.Loc_Idno,
                               PBH.Year_Idno,
                               PBH.User_Idno,
                               Location=loc.City_Name,
                               PBH.Net_Amnt,

                           }).ToList();
                if (HireNo > 0)
                {
                    lst = lst.Where(l => l.Hire_InvNo == HireNo).ToList();
                }
                if (HireDateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Hire_Date).Date <= Convert.ToDateTime(HireDateTo).Date).ToList();
                 
                }
                if (HireDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Hire_Date).Date >= Convert.ToDateTime(HireDate).Date).ToList();

                }
                if (Loc_idno > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == Loc_idno).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                //if (UserIdno > 0)
                //{
                //    lst = lst.Where(l => l.User_Idno == UserIdno).ToList();
                //}
                return lst;
            }
        }
        public int DeleteHireInvoice(int HireHead_Idno)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TblHireInvHead IH = db.TblHireInvHeads.Where(h => h.Hire_Idno == HireHead_Idno).FirstOrDefault();
                List<TblHireInvDetl> detl = db.TblHireInvDetls.Where(d => d.Hire_Idno == HireHead_Idno).ToList();
               
               
                if (IH != null)
                {
                    foreach (var d in detl)
                    {
                        db.TblHireInvDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.TblHireInvHeads.DeleteObject(IH);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
        public Int64 SelectHireInvoiceCount(Int32 yearIdno, DateTime? DateFrom, DateTime? DateTo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
               
                Int64 lst = (from PBH in db.TblHireInvHeads
                              join loc in db.tblCityMasters on PBH.Loc_Idno equals loc.City_Idno
                              select PBH).Count();
                return lst;
            }
        }
        public Int64 Update(Int64 HireHead_Idno, Int64 Year_Idno, DateTime? Invice_Date, Int64 Invioce_No, Int64 Loc_Idno, Int64 User_Idno, Int64 Comp_Idno, bool AccPost, string Remark, Int64 truckidno, Int64 partyidno, Int64 fromcityidno, Int64 citytoidno, Int64 viacityidno, DateTime? from_Date, DateTime? return_Date, double netamnt, double advamnt, double dieselamnt, Int32 recptidno, Int32 instno, DateTime? instdate, Int32 bankidno, double grossamnt, string GrIdno, DataTable dtitemdetail, string Con)
        {
            Int64 IHireHead_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {

                    TblHireInvHead objHireHead1 = (from mast in db.TblHireInvHeads
                                                   where mast.Hire_Idno == HireHead_Idno
                                              select mast).FirstOrDefault();
                        if (objHireHead1 != null)
                        {
                            objHireHead1.Year_Idno = Year_Idno;
                            objHireHead1.Hire_InvNo = Invioce_No;
                            objHireHead1.Hire_Date = Invice_Date;
                            objHireHead1.Loc_Idno = Loc_Idno;
                            objHireHead1.User_Idno = User_Idno;
                            objHireHead1.Comp_Idno = Comp_Idno;
                            objHireHead1.Acc_Post = AccPost;
                            objHireHead1.Remark = Remark;
                            objHireHead1.Date_Modified = System.DateTime.Now;
                            objHireHead1.Net_Amnt = netamnt;
                            objHireHead1.Gross_Amnt = grossamnt;
                            objHireHead1.Adv_Amnt = advamnt;
                            objHireHead1.Diesel_Amnt = dieselamnt;
                            objHireHead1.RcptType_Idno = recptidno;
                            objHireHead1.Inst_No = instno;
                            objHireHead1.Inst_Dt = instdate;
                            objHireHead1.Bank_Idno = bankidno;
                            objHireHead1.Truck_Idno = Convert.ToInt64(Convert.ToString(truckidno) == "" ? 0 : truckidno);
                            objHireHead1.Party_Idno = Convert.ToInt64(Convert.ToString(partyidno) == "" ? 0 : partyidno);
                            objHireHead1.LocFrm_Idno = Convert.ToInt64(Convert.ToString(fromcityidno) == "" ? 0 : fromcityidno);
                            objHireHead1.ViaCity_Idno = Convert.ToInt64(Convert.ToString(viacityidno) == "" ? 0 : viacityidno);
                            objHireHead1.ToCity_Idno = Convert.ToInt64(Convert.ToString(citytoidno) == "" ? 0 : citytoidno);
                            objHireHead1.DateFrom = Convert.ToDateTime(from_Date);
                            objHireHead1.ReturnDate = String.IsNullOrEmpty(Convert.ToString(return_Date)) ? (DateTime?)null : Convert.ToDateTime(return_Date);
                            db.SaveChanges();

                            IHireHead_Idno = HireHead_Idno;
                            if (IHireHead_Idno > 0)
                            {
                                List<TblHireInvDetl> lstHireDetl = db.TblHireInvDetls.Where(obj => obj.Hire_Idno == IHireHead_Idno).ToList();
                                if (lstHireDetl.Count > 0)
                                {
                                    foreach (TblHireInvDetl obj in lstHireDetl)
                                    {
                                        db.TblHireInvDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }

                                 
                             foreach (DataRow row in dtitemdetail.Rows)
                                {
                                    TblHireInvDetl objHireDetl = new TblHireInvDetl();
                                    objHireDetl.Hire_Idno = HireHead_Idno;
                                    objHireDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objHireDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                    objHireDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["Rate_TypeIdno"])) ? 0 : Convert.ToInt32(row["Rate_TypeIdno"]);
                                    objHireDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                    objHireDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                    objHireDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objHireDetl.Amount = Convert.ToDouble(row["Amount"]);
                                    objHireDetl.Detail = Convert.ToString(row["Detail"]);
                                    objHireDetl.UnloadWeight = Convert.ToDouble(row["UnloadWeight"]);
                                    db.TblHireInvDetls.AddObject(objHireDetl);
                                    db.SaveChanges();
                            }
                                    
                                }
                                if (GrIdno != "")
                                {
                                    UpdateFlag(Con, GrIdno, HireHead_Idno);
                                }
                            }
                        
                        else
                        {
                            IHireHead_Idno = -1;
                        }
                }
                catch (Exception Ex)
                {
                    IHireHead_Idno = 0;
                }
            }
            return IHireHead_Idno;
        }
      
        #endregion

        #region Report Function............

        public DataTable SelectRep(string dtFromDate, string dtToDate, Int64 iInvoiceNo, Int64 Location, Int64 PartyIdno, Int64 UserIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", "SelectInvwiseRep");
            objSqlPara[1] = new SqlParameter("@DtFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DtTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@InvoiceNo", iInvoiceNo);
            objSqlPara[4] = new SqlParameter("@Location", Location);
            objSqlPara[5] = new SqlParameter("@PartyIdno", PartyIdno);
            objSqlPara[6] = new SqlParameter("@UserIdno", UserIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireInvoiceRep", objSqlPara);
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

        public List<AcntMast> BindParty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }

        public DataTable selectInvoiceReportDetails(string strAction, Int32 Inv_Idno, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", strAction);
                objSqlPara[1] = new SqlParameter("@InvoiceNo", Inv_Idno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireInvoiceRep", objSqlPara);
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
        }

        // By salman
        public DataTable SelectGRPrepOntoHire(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int64 CityIdno, Int64 strtrantype, string con, Int32 FromCityIdno, Int64 ICityViaIdno)
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

        public DataTable SelectGrONtoHireDetails(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRHireDetails");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
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


        public DataTable UpdateFlag(string ConString, string GrIdno, Int64 InvIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "UpdateGrId");
            objSqlPara[1] = new SqlParameter("@GRIdnos", GrIdno);
            objSqlPara[2] = new SqlParameter("@InvIdno", InvIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(ConString, CommandType.StoredProcedure, "spSelectGrPepOntoHire", objSqlPara);
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

        public DataTable SelectGrByIdDetails(string con, Int64 iYearId, Int64 InvIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRByInvId");
            objSqlPara[1] = new SqlParameter("@InvIdno", InvIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
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

        public DataTable SelectGrPrep(Int64 YearId, string dtFrmDate, string dtToDate, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGrPrepOntohire");
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
        public DataTable SelecthireDetail(Int64 hireIDNO, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "SelectHireDetail");
                objSqlPara[1] = new SqlParameter("@Id", hireIDNO);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spHireLorryDetl", objSqlPara);
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
        }
    }
}
