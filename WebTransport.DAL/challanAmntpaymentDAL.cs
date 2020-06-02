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
    public class challanAmntpaymentDAL
    {
        string sqlSTR = "";
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
            sqlSTR = @"SELECT ISNULL(MAX(Rcpt_no),0) + 1 AS MAXID FROM tblChlnAmntPayment_Head WHERE BaseCity_idno='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public Int32 GetRcptNo(Int32 YearIdno, Int32 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32((from obj in db.tblChlnAmntPayment_Head where (obj.Year_IdNo == YearIdno) && (obj.BaseCity_Idno == FromCity) select obj.Rcpt_No).Max());
                MaxNo = MaxNo + 1;
                return MaxNo;
            }
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
                var max = (from hd in db.tblChlnAmntPayment_Head select hd.Rcpt_No).Max() + 1;
                return Convert.ToInt64(max);
            }
        }

        public Int64 getcompid(string con)
        {
            Int64 id= 0; string sqlSTR = "";
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                sqlSTR = @"select Comp_Idno from tblCompMast where Comp_Name like '%aravali%'";
                DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
                if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                {
                    id = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
                }
                return id;
               
            }
        }
        public Int64 MaxIdno(string con)
        {
            Int64 MaxNo = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(MAX(Id),0)  AS MAXID FROM tblChlnAmntPayment_Head";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public tblChlnAmntPayment_Head selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblChlnAmntPayment_Head.Where(h => h.Id == HeadId).FirstOrDefault();
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
        public Int64 Insert(tblChlnAmntPayment_Head obj, DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnAmntPayment_Head AmtHead = new tblChlnAmntPayment_Head();
                db.Connection.Open();
                //    using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //   {
                try
                {
                    tblChlnAmntPayment_Head RH = db.tblChlnAmntPayment_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                    if (RH != null)
                    {
                        AmntHeadId = -1;
                    }
                    else
                    {
                        obj.Date_Added = DateTime.Now;
                        obj.Date_Modified = DateTime.Now;
                        db.tblChlnAmntPayment_Head.AddObject(obj);
                        db.SaveChanges();
                        AmntHeadId = obj.Id;
                        if (Dttemp != null && Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                tblChlnAmntPayment_Detl objtblAmntRecvGR_Detl = new tblChlnAmntPayment_Detl();
                                objtblAmntRecvGR_Detl.Head_Idno = AmntHeadId;
                                objtblAmntRecvGR_Detl.Chln_Idno = Convert.ToInt64(Dr["chln_Idno"]);
                                objtblAmntRecvGR_Detl.Chln_No = Convert.ToInt64(Dr["Chln_No"]);
                                objtblAmntRecvGR_Detl.Chln_Date = Convert.ToDateTime(Dr["Chln_Date"]);
                                objtblAmntRecvGR_Detl.FromCity_Idno = Convert.ToInt64(Dr["Fromcity_Idno"]);
                                objtblAmntRecvGR_Detl.Challan_Amnt = Convert.ToDouble(Dr["Challan_Amnt"]);
                                objtblAmntRecvGR_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recvd_Amnt"]);
                                objtblAmntRecvGR_Detl.Truck_Idno = Convert.ToInt64(Dr["Truck_Idno"]);
                                objtblAmntRecvGR_Detl.Driver_Idno = Convert.ToInt64(Dr["Driver_Idno"]);
                                db.tblChlnAmntPayment_Detl.AddObject(objtblAmntRecvGR_Detl);
                                db.SaveChanges();
                            }


                            //dbTran.Commit();
                        }


                    }


                }
                catch
                {
                    // dbTran.Rollback();
                }
                //   }
                return AmntHeadId;
            }

        }
        public Int64 Update(tblChlnAmntPayment_Head obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                //using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //{
                    try
                    {
                        tblChlnAmntPayment_Head CH = db.tblChlnAmntPayment_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.Id != Head_Idno && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_IdNo == obj.Year_IdNo).FirstOrDefault();
                        if (CH != null)
                        {
                            AmntHeadId = -1;
                        }
                        else
                        {
                            tblChlnAmntPayment_Head TBH = db.tblChlnAmntPayment_Head.Where(rh => rh.Id == Head_Idno).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Rcpt_No = obj.Rcpt_No;
                                TBH.Rcpt_date = obj.Rcpt_date;
                                TBH.Year_IdNo = obj.Year_IdNo;
                                TBH.RcptType_Idno = obj.RcptType_Idno;
                                TBH.Party_IdNo = obj.Party_IdNo;
                                TBH.Inst_No = obj.Inst_No;
                                TBH.Inst_Dt = obj.Inst_Dt;
                                TBH.Bank_Idno = obj.Bank_Idno;
                                TBH.Comp_Id = obj.Comp_Id;
                                TBH.Remark = obj.Remark;
                                TBH.Date_Modified = obj.Date_Modified;
                                TBH.Net_Amnt = obj.Net_Amnt;
                                TBH.BaseCity_Idno = obj.BaseCity_Idno;
                                TBH.Loc_Idno = obj.Loc_Idno;
                                TBH.Date_Modified = DateTime.Now;
                                db.SaveChanges();
                                AmntHeadId = TBH.Id;
                                List<tblChlnAmntPayment_Detl> ChlnDetl = db.tblChlnAmntPayment_Detl.Where(rd => rd.Head_Idno == Head_Idno).ToList();
                                foreach (tblChlnAmntPayment_Detl rgd in ChlnDetl)
                                {
                                    db.tblChlnAmntPayment_Detl.DeleteObject(rgd);
                                    db.SaveChanges();
                                }
                                if (Dttemp != null && Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        tblChlnAmntPayment_Detl objtblAmntRecvGR_Detl = new tblChlnAmntPayment_Detl();
                                        objtblAmntRecvGR_Detl.Head_Idno = AmntHeadId;
                                        objtblAmntRecvGR_Detl.Chln_Idno = Convert.ToInt64(Dr["chln_Idno"]);
                                        objtblAmntRecvGR_Detl.Chln_No = Convert.ToInt64(Dr["Chln_No"]);
                                        objtblAmntRecvGR_Detl.Chln_Date = Convert.ToDateTime(Dr["Chln_Date"]);
                                        objtblAmntRecvGR_Detl.FromCity_Idno = Convert.ToInt64(Dr["Fromcity_Idno"]);
                                        objtblAmntRecvGR_Detl.Challan_Amnt = Convert.ToDouble(Dr["Challan_Amnt"]);
                                        objtblAmntRecvGR_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recvd_Amnt"]);
                                        objtblAmntRecvGR_Detl.Truck_Idno = Convert.ToInt64(Dr["Truck_Idno"]);
                                        objtblAmntRecvGR_Detl.Driver_Idno = Convert.ToInt64(Dr["Driver_Idno"]);
                                        db.tblChlnAmntPayment_Detl.AddObject(objtblAmntRecvGR_Detl);
                                        db.SaveChanges();
                                    }
                                }
                                
                            }
                        }
                    }
                    catch
                    {
                        
                    }
                //}
            }
            return AmntHeadId;
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from CH in db.tblChlnAmntPayment_Head
                         //join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                         join cito in db.tblCityMasters on CH.Loc_Idno equals cito.City_Idno
                         join AM in db.AcntMasts on CH.Party_IdNo equals AM.Acnt_Idno
                         select CH.Id).Count();

                return Count;
            }

        }
        public IList search(Int32 yearid, Int64 GrNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 SenderIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblChlnAmntPayment_Head
                           //join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.Loc_Idno equals cito.City_Idno
                           join AM in db.AcntMasts on CH.Party_IdNo equals AM.Acnt_Idno
                           join Am1 in db.AcntMasts on CH.RcptType_Idno equals Am1.Acnt_Idno into AM2
                           from Am3 in AM2.DefaultIfEmpty()
                           select new
                           {
                               CH.BaseCity_Idno,
                               CH.Rcpt_date,
                               CH.Rcpt_No,
                               CH.Id,
                               CH.Inst_Dt,
                               CH.Inst_No,
                               FromCity = cito.City_Name,
                               CH.Year_IdNo,
                               AM.Acnt_Name,
                               AM.Acnt_Idno,
                               ReciptName=Am3.Acnt_Name,
                               CH.Net_Amnt
                           }).ToList();
                if (GrNo > 0)
                {
                    lst = lst.Where(l => l.Rcpt_No == GrNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Rcpt_date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Rcpt_date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }

                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_IdNo == yearid).ToList();
                }
                if (SenderIdno > 0)
                {
                    lst = lst.Where(l => l.Acnt_Idno == SenderIdno).ToList();
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
        public DataTable SelectChallanPaymentDetail(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 PrtIdno, Int32 ChallanNo, string con, Int64 fromcityIdno)
        {
            DataTable objDtTemp = new DataTable();
            if (Action == "SelectChallanPaymentDetailWithoutChallanNo")
            {
                SqlParameter[] objSqlPara = new SqlParameter[6];
                objSqlPara[0] = new SqlParameter("@Action", Action);
                objSqlPara[1] = new SqlParameter("@YEARIDNO", YearId);
                objSqlPara[2] = new SqlParameter("@Chln_frmDate", dtFrmDate);
                objSqlPara[3] = new SqlParameter("@Chln_ToDate", dtToDate);
                objSqlPara[4] = new SqlParameter("@PrtyIdno", PrtIdno);//@BaseCityIdno
                objSqlPara[5] = new SqlParameter("@BaseCityIdno", fromcityIdno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnAmntPayment", objSqlPara);
                if (objDsTemp.Tables.Count > 0)
                {
                    if (objDsTemp.Tables[0].Rows.Count > 0)
                    {
                        objDtTemp = objDsTemp.Tables[0];
                    }
                }
            }
            else
            {
                SqlParameter[] objSqlPara = new SqlParameter[7];
                objSqlPara[0] = new SqlParameter("@Action", Action);
                objSqlPara[1] = new SqlParameter("@YEARIDNO", YearId);
                objSqlPara[2] = new SqlParameter("@Chln_frmDate", dtFrmDate);
                objSqlPara[3] = new SqlParameter("@Chln_ToDate", dtToDate);
                objSqlPara[4] = new SqlParameter("@PrtyIdno", PrtIdno);
                objSqlPara[5] = new SqlParameter("@ChlnNo", ChallanNo);
                objSqlPara[6] = new SqlParameter("@BaseCityIdno", fromcityIdno);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnAmntPayment", objSqlPara);
                if (objDsTemp.Tables.Count > 0)
                {
                    if (objDsTemp.Tables[0].Rows.Count > 0)
                    {
                        objDtTemp = objDsTemp.Tables[0];
                    }
                }
            }
            return objDtTemp;
        }
        public DataTable SelectChallanDetail(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallanDetail");
            objSqlPara[1] = new SqlParameter("@ChlnIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnAmntPayment", objSqlPara);
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
            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable BindPartyOwner(string con)
        {
            sqlSTR = @"SELECT DISTINCT Acnt_Name,Acnt_Idno FROM ( SELECT A.Acnt_Name + '  [' + CONVERT(nvarchar(15),A.Acnt_Idno) + ' ]' Acnt_Name, A.Acnt_Idno FROM LorryMast LM INNER JOIN AcntMast A ON LM.Prty_Idno=A.Acnt_Idno WHERE LM.Lorry_Type=1) A ORDER BY Acnt_Name";
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


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnAmntPayment", objSqlPara);
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
        public int Delete(Int64 HeadId,Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                tblChlnAmntPayment_Head CH = db.tblChlnAmntPayment_Head.Where(h => h.Id == HeadId).FirstOrDefault();
                List<tblChlnAmntPayment_Detl> CD = db.tblChlnAmntPayment_Detl.Where(d => d.Head_Idno == HeadId).ToList();
                if (CH != null)
                {
                    SqlParameter[] objSqlPara = new SqlParameter[3];
                    objSqlPara[0] = new SqlParameter("@Action", "DeleteChlnAmntPayment");
                    objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                    objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                    Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);
                    foreach (var d in CD)
                    {
                        db.tblChlnAmntPayment_Detl.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblChlnAmntPayment_Head.DeleteObject(CH);
                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "ACB");
                    db.SaveChanges();
                    if (intValue > 0)
                    {
                        value = 1;
                    }
                }
            }
            return value;
        }

        public Int64 GetChlnIdno(Int64 HeadID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var ChlnId = (from hd in db.tblChlnAmntPayment_Head join CD in db.tblChlnAmntPayment_Detl on hd.Id equals CD.Head_Idno
                              where hd.Id == HeadID select CD.Chln_Idno).FirstOrDefault();
                return Convert.ToInt64(ChlnId);
            }
        }

        public void UpdateIsPosting(Int64 ID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblChlnAmntPayment_Head GH = (from G in db.tblChlnAmntPayment_Head where G.Id == ID select G).FirstOrDefault();
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
