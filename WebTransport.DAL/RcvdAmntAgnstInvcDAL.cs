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
using System.Transactions;

namespace WebTransport.DAL
{
    public class RcvdAmntAgnstInvcDAL
    {
        string sqlSTR = "";

        public tblAcntLink SelectAcntLink()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblAcntLink AcntLink = (from UP in db.tblAcntLinks select UP).FirstOrDefault();
                return AcntLink;
            }
        }

        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        public Int64 MaxNo(Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Convert(Numeric(20),Isnull(Rcpt_no,0))),0) + 1 AS MAXID FROM tblAmntRecvdInv_Head WHERE BaseCity_idno='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }
        public List<AcntMast> BindBank()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objacntmast = new List<AcntMast>();

                objacntmast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 4
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objacntmast;
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public Int64 GetMaxNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from hd in db.tblAmntRecvdInv_Head select hd.Rcpt_No).Max() + 1;
                return Convert.ToInt64(max);
            }

        }


        public tblAmntRecvdInv_Head selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblAmntRecvdInv_Head.Where(h => h.InvHead_Idno == HeadId).FirstOrDefault();
            }
        }
        public DataTable selectDetail(Int64 HeadId, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                sqlSTR = @"SELECT ISNULL(SUM(Recvd_Amnt),0.00)as Recvd_Amnt,ISNULL(SUM(TDS_Amnt),0.00)as TDS_Amnt,ISNULL(SUM(Dr_Note),0.00)as Dr_Note from tblAmntRecvInv_Detl where InvHead_idno='" + HeadId + "'";
                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
                DataTable dt = new DataTable();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
        }


        public List<ItemMast> GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> lst = null;
                lst = (from cm in db.ItemMasts orderby cm.Item_Name ascending select cm).ToList();
                return lst;
            }
        }
        public Int64 Insert(tblAmntRecvdInv_Head obj, DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblAmntRecvdInv_Head AmtHead = new tblAmntRecvdInv_Head();
                db.Connection.Open();
                // using (DbTransaction dbTran = db.Connection.BeginTransaction())
                // {
                try
                {
                    tblAmntRecvdInv_Head RH = db.tblAmntRecvdInv_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                    if (RH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        db.tblAmntRecvdInv_Head.AddObject(obj);

                        db.SaveChanges();
                        AmntHeadId = obj.InvHead_Idno;
                        if (Dttemp != null && Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                if (Convert.ToDouble(Dr["Recv_Amount"]) > 0 || Convert.ToDouble(Dr["TDS_Amnt"]) > 0 || Convert.ToDouble(Dr["Dr_Note"]) > 0)
                                {
                                    tblAmntRecvInv_Detl objtblAmntRecvInv_Detl = new tblAmntRecvInv_Detl();
                                    //objtblAmntRecvInv_Detl.InvDetl_Idno = Convert.ToInt32(Dr["InvDetl_Idno"]);
                                    objtblAmntRecvInv_Detl.InvHead_Idno = Convert.ToInt64(AmntHeadId);
                                    objtblAmntRecvInv_Detl.Inv_Idno = Convert.ToInt64(Dr["Inv_Idno"]);
                                    objtblAmntRecvInv_Detl.Inv_No = Convert.ToString(Dr["Inv_No"]);
                                    objtblAmntRecvInv_Detl.Inv_Date = Convert.ToDateTime(Dr["Inv_Date"]);
                                    objtblAmntRecvInv_Detl.Recivr_Idno = Convert.ToInt64(Dr["Recivr_Idno"]);
                                    objtblAmntRecvInv_Detl.Inv_Amnt = Convert.ToDouble(Dr["Amount"]);
                                    objtblAmntRecvInv_Detl.TDSRcvd_Amnt = Convert.ToDouble(Dr["TDS_Rcvd"]);
                                    objtblAmntRecvInv_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recv_Amount"]);
                                    objtblAmntRecvInv_Detl.TDS_Amnt = Convert.ToDouble(Dr["TDS_Amnt"]);
                                    objtblAmntRecvInv_Detl.Dr_Note = Convert.ToDouble(Dr["Dr_Note"]);
                                    objtblAmntRecvInv_Detl.Cur_Bal = Convert.ToDouble(Dr["Cur_Bal"]);
                                    objtblAmntRecvInv_Detl.Tot_Recvd = Convert.ToDouble(Dr["Tot_Recvd"]);
                                    db.tblAmntRecvInv_Detl.AddObject(objtblAmntRecvInv_Detl);
                                    db.SaveChanges();
                                }
                            }
                            // dbTran.Commit();
                        }
                    }
                }
                catch
                {
                    //dbTran.Rollback();
                }
                //     }
                return AmntHeadId;
            }

        }


        public Int64 Update(tblAmntRecvdInv_Head obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                //  using (DbTransaction dbTran = db.Connection.BeginTransaction())
                // {
                try
                {
                    tblAmntRecvdInv_Head CH = db.tblAmntRecvdInv_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.InvHead_Idno != Head_Idno && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                    if (CH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        tblAmntRecvdInv_Head TBH = db.tblAmntRecvdInv_Head.Where(rh => rh.InvHead_Idno == Head_Idno).FirstOrDefault();
                        if (TBH != null)
                        {
                            TBH.Rcpt_No = obj.Rcpt_No;
                            TBH.FromCity_Idno = obj.FromCity_Idno;
                            TBH.Rcpt_date = obj.Rcpt_date;
                            TBH.Year_IdNo = obj.Year_IdNo;
                            TBH.RcptType_Idno = obj.RcptType_Idno;
                            TBH.Party_IdNo = obj.Party_IdNo;
                            TBH.Inst_No = obj.Inst_No;
                            TBH.Inst_Dt = obj.Inst_Dt;
                           // TBH.Bank_Idno = obj.Bank_Idno;
                            TBH.Bank_Name = obj.Bank_Name;
                            TBH.Comp_Idno = obj.Comp_Idno;
                            TBH.Remark = obj.Remark;
                            TBH.Status = obj.Status;
                            TBH.Date_Modified = obj.Date_Modified;
                            TBH.Net_Amnt = obj.Net_Amnt;
                            db.SaveChanges();
                            AmntHeadId = TBH.InvHead_Idno;
                            List<tblAmntRecvInv_Detl> ChlnDetl = db.tblAmntRecvInv_Detl.Where(rd => rd.InvHead_Idno == Head_Idno).ToList();
                            foreach (tblAmntRecvInv_Detl rgd in ChlnDetl)
                            {
                                db.tblAmntRecvInv_Detl.DeleteObject(rgd);
                                db.SaveChanges();
                            }
                            if (Dttemp != null && Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in Dttemp.Rows)
                                {
                                    tblAmntRecvInv_Detl objtblAmntRecvInv_Detl = new tblAmntRecvInv_Detl();

                                    //objtblAmntRecvInv_Detl.InvDetl_Idno = Convert.ToInt32(Dr["InvDetl_Idno"]);
                                    if (Convert.ToDouble(Dr["Recv_Amount"]) > 0 || Convert.ToDouble(Dr["TDS_Amnt"]) > 0 || Convert.ToDouble(Dr["Dr_Note"]) > 0)
                                    {
                                        objtblAmntRecvInv_Detl.InvHead_Idno = Head_Idno;
                                        objtblAmntRecvInv_Detl.Inv_Idno = Convert.ToInt64(Dr["Inv_Idno"]);
                                        objtblAmntRecvInv_Detl.Inv_No = Convert.ToString(Dr["Inv_No"]);
                                        objtblAmntRecvInv_Detl.Inv_Date = Convert.ToDateTime(Dr["Inv_Date"]);
                                        objtblAmntRecvInv_Detl.Recivr_Idno = Convert.ToInt64(Dr["Recivr_Idno"]);
                                        objtblAmntRecvInv_Detl.Inv_Amnt = Convert.ToDouble(Dr["Amount"]);
                                        objtblAmntRecvInv_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recv_Amount"]);
                                        objtblAmntRecvInv_Detl.TDS_Amnt = Convert.ToDouble(Dr["TDS_Amnt"]);
                                        objtblAmntRecvInv_Detl.Dr_Note = Convert.ToDouble(Dr["Dr_Note"]);
                                        objtblAmntRecvInv_Detl.Cur_Bal = Convert.ToDouble(Dr["Cur_Bal"]);
                                        objtblAmntRecvInv_Detl.Tot_Recvd = Convert.ToDouble(Dr["Tot_Recvd"]);
                                        objtblAmntRecvInv_Detl.Vchr_RecAmnt = Convert.ToDouble(Dr["Vchr_Amnt"]);
                                        db.tblAmntRecvInv_Detl.AddObject(objtblAmntRecvInv_Detl);
                                        db.SaveChanges();
                                    }
                                }
                            }

                            // dbTran.Commit();
                        }
                    }



                }
                catch
                {
                    // dbTran.Rollback();
                }
                //  }
            }
            return AmntHeadId;
        }

        public IList search(Int32 yearid, string InvceNo, DateTime? dtfrom, DateTime? dtto, Int32 Loc_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblAmntRecvdInv_Head                           
                           join CD in db.tblAmntRecvInv_Detl on CH.InvHead_Idno equals CD.InvHead_Idno
                           join IH in db.tblInvGenHeads on CD.Inv_Idno equals IH.Inv_Idno
                           join AM in db.AcntMasts on CH.Party_IdNo equals AM.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                           select new
                           {
                               Inv_No = IH.Inv_No,
                               CH.BaseCity_Idno,
                               Inv_Date = CH.Rcpt_date,
                               Rcpt_No = CH.Rcpt_No,
                               Inv_Idno = CH.InvHead_Idno,
                               FromCity = cito.City_Name,
                               SenderName = AM.Acnt_Name,
                               Net_Amnt = CH.Net_Amnt,
                               Loc_Idno = CH.FromCity_Idno,
                               CH.Year_IdNo

                           }).Distinct().ToList();
                if (InvceNo != "")
                {
                    lst = lst.Where(l => l.Rcpt_No == InvceNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Inv_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Inv_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (Loc_Idno > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == Loc_Idno).ToList();
                }
                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_IdNo == yearid).ToList();
                }

                return lst;
            }
        }
        public DataTable BindRcptTypeDel(Int32 intRcptTypeId, string con)
        {

            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4,12) and Acnt_Idno=" + intRcptTypeId + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        public DataTable SelectDBData(Int64 Item_Idno, string con)
        {
            string str = string.Empty;
            str = @"select isnull(Rate_Idno,0) Rate_Idno,isnull(Rate_Date,0) Rate_Date,Isnull(CM.City_Name,'') City_Name ,Isnull(Item_Rate,0) Item_Rate,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0)WghtShrtg_Rate,ISNULL(Tocity_Idno,0) Tocity_Idno from tblRateMast RM Inner Join
           tblCityMaster CM ON CM.city_Idno=RM.ToCity_Idno where RM.Item_Idno= " + Item_Idno + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        public DataTable SelectInvPaymentDetail(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 PrtIdno, Int64 frmcity, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@YEARIDNO", YearId);
            objSqlPara[2] = new SqlParameter("@InvfrmDate", dtFrmDate);
            objSqlPara[3] = new SqlParameter("@InvToDate", dtToDate);
            objSqlPara[4] = new SqlParameter("@PrtyIdno", PrtIdno);
            objSqlPara[5] = new SqlParameter("@FromCity", frmcity);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdInvoice", objSqlPara);
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


        public DataTable SelectInvDetail(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectInvDetail");
            objSqlPara[1] = new SqlParameter("@InvIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdInvoice", objSqlPara);
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

        public DataTable BindRcptType(string con)
        {
            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4,12) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
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


        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdInvoice", objSqlPara);
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
        public int Delete(Int64 HeadId, Int64 UserIdno, string con)
        {
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblAmntRecvdInv_Head CH = db.tblAmntRecvdInv_Head.Where(h => h.InvHead_Idno == HeadId).FirstOrDefault();
                List<tblAmntRecvInv_Detl> CD = db.tblAmntRecvInv_Detl.Where(d => d.InvHead_Idno == HeadId).ToList();
                if (CH != null)
                {
                    //SqlParameter[] objSqlPara = new SqlParameter[3];
                    //objSqlPara[0] = new SqlParameter("@Action", "DeleteAmntRcvdAgnstInvDet");
                    //objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                    //objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                    //Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                    foreach (var d in CD)
                    {
                        db.tblAmntRecvInv_Detl.DeleteObject(d);

                        db.SaveChanges();
                    }
                    db.tblAmntRecvdInv_Head.DeleteObject(CH);
                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "ARIV");

                    db.SaveChanges();

                    if (intValue > 0)
                    {
                        value = 1;
                    }

                }
            }
            return value;
        }
        public DataTable selectVchr(string con,Int32 acntId, DateTime dtDateFrom, DateTime dtDateTo,int InvId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "SelectVchr");
            objSqlPara[1] = new SqlParameter("@Acnt_Idno", acntId);
            objSqlPara[2] = new SqlParameter("@InvfrmDate", dtDateFrom);
            objSqlPara[3] = new SqlParameter("@InvToDate", dtDateTo);
            objSqlPara[4] = new SqlParameter("@InvIdno", InvId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdInvoice", objSqlPara);
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
        public IList VchrParty(Int32 acntId, DateTime? dtDateFrom, DateTime? dtDateTo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from VH in db.VchrHeads
                           join VD in db.VchrDetls on VH.Vchr_Idno equals VD.Vchr_Idno
                           where VD.Acnt_Idno == acntId && VH.Vchr_Type == 2
                           select new
                           {
                               VH.Vchr_Idno,
                               VH.Vchr_Date,
                               VD.Acnt_Amnt
                           }).GroupBy(x => x.Vchr_Idno).Select(x => x.FirstOrDefault()).ToList();
                if (dtDateFrom != null)
                {
                    lst = (from obj in lst where obj.Vchr_Date >= dtDateFrom select obj).ToList();
                }
                if (dtDateTo != null)
                {
                    lst = (from obj in lst where obj.Vchr_Date <= dtDateTo select obj).ToList();
                }
                return lst;
            }
        }

        public Int64 VchrSave(DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
               db.Connection.Open();
               using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
               {
                   try
                   {
                       if (Dttemp != null && Dttemp.Rows.Count > 0)
                       {  
                           foreach (DataRow Dr in Dttemp.Rows)
                           {
                               int inv = Convert.ToInt32(Dr["InvNo"]);
                               int vchr = Convert.ToInt32(Dr["VchrId"]);
                               List<VchrRef> objTd = db.VchrRefs.Where(td => (td.InvId == inv) && (td.VchrId == vchr)).ToList();
                               if (objTd != null)
                               {
                                   if (objTd.Count > 0)
                                   {
                                       foreach (VchrRef td in objTd)
                                       {
                                           db.DeleteObject(td);
                                           db.SaveChanges();
                                       }

                                   }
                               }

                               //TranId = objTranHead.Tran_Idno;
                               VchrRef objtblVchrRef = new VchrRef();
                               objtblVchrRef.InvId = Convert.ToInt32(Dr["InvNo"]);
                               objtblVchrRef.VchrId = Convert.ToInt32(Dr["VchrId"]);
                               objtblVchrRef.Amount = Convert.ToDouble(Dr["Amount"]);
                               db.VchrRefs.AddObject(objtblVchrRef);
                               db.SaveChanges();
                           }
                           tScope.Complete();
                           AmntHeadId = 1;
                       }
                   }
                   catch
                   {
                       tScope.Dispose();
                   }
                   return AmntHeadId;
               }
            }
        }

        public double TotVchrAmnt(Int32 acntId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from VH in db.VchrRefs
                           where VH.InvId == acntId
                           select new
                           {
                               VH.Amount
                           }).Sum(m => m.Amount);

                return Convert.ToDouble(lst);
            }
        }

        public void UpdateIsPosting(Int64 InvHeadID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblAmntRecvdInv_Head GH = (from G in db.tblAmntRecvdInv_Head where G.InvHead_Idno == InvHeadID select G).FirstOrDefault();
                    if (GH != null)
                    {
                        GH.IS_Post = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

    }
}
