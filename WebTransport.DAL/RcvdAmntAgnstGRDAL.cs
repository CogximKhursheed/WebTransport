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
    public class RcvdAmntAgnstGRDAL
    {
        string sqlSTR = "";

        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }
        public Int32 GetRcptNo(Int32 YearIdno, Int32 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32((from obj in db.tblAmntRecvdGR_Head where  (obj.Year_IdNo == YearIdno) && (obj.BaseCity_Idno == FromCity) select obj.Rcpt_No).Max());
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
                var max = (from hd in db.tblAmntRecvdGR_Head select hd.Rcpt_No).Max() + 1;
                return Convert.ToInt64(max);
            }

        }

     
        public tblAmntRecvdGR_Head selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblAmntRecvdGR_Head.Where(h => h.Head_Idno == HeadId).FirstOrDefault();
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


        public Int64 Insert(tblAmntRecvdGR_Head obj,DataTable Dttemp)
        {
            Int64 AmntHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblAmntRecvdGR_Head AmtHead = new tblAmntRecvdGR_Head();
                db.Connection.Open();
                //using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //{
                    try
                    {
                        tblAmntRecvdGR_Head RH = db.tblAmntRecvdGR_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.BaseCity_Idno==obj.BaseCity_Idno).FirstOrDefault();
                        if (RH != null)
                        {
                            AmntHeadId = -1;
                        }
                        else
                        {
                            db.tblAmntRecvdGR_Head.AddObject(obj);

                            db.SaveChanges();
                            AmntHeadId = obj.Head_Idno;
                            if (Dttemp !=null && Dttemp.Rows.Count > 0)
                            {
                                foreach (DataRow  Dr in Dttemp.Rows)
                                {
                                    if (Convert.ToDouble(Dr["Recv_Amount"]) > 0)
                                    {


                                        tblAmntRecvGR_Detl objtblAmntRecvGR_Detl = new tblAmntRecvGR_Detl();
                                        objtblAmntRecvGR_Detl.Head_Idno = AmntHeadId;
                                        objtblAmntRecvGR_Detl.Gr_Idno = Convert.ToInt64(Dr["Gr_Idno"]);
                                        objtblAmntRecvGR_Detl.Gr_No = Convert.ToInt64(Dr["Gr_no"]);
                                        objtblAmntRecvGR_Detl.Gr_Date = Convert.ToDateTime(Dr["Gr_Date"]);
                                        objtblAmntRecvGR_Detl.Recivr_Idno = Convert.ToInt64(Dr["Recivr_Idno"]);
                                        objtblAmntRecvGR_Detl.GR_Frm = Convert.ToString(Dr["GR_From"]);
                                        objtblAmntRecvGR_Detl.To_City = Convert.ToInt64(Dr["To_City"]);
                                        objtblAmntRecvGR_Detl.From_City = Convert.ToInt64(Dr["From_City"]);
                                        objtblAmntRecvGR_Detl.Gr_Amnt = Convert.ToDouble(Dr["Amount"]);
                                        objtblAmntRecvGR_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recv_Amount"]);
                                        objtblAmntRecvGR_Detl.Cur_Bal = Convert.ToDouble(Dr["cur_Bal"]);
                                        objtblAmntRecvGR_Detl.Tot_Recvd = Convert.ToDouble(Dr["Tot_Recvd"]);
                                        db.tblAmntRecvGR_Detl.AddObject(objtblAmntRecvGR_Detl);
                                        db.SaveChanges();
                                    }
                                }


                              //  dbTran.Commit();
                            }


                        }


                    }
                    catch
                    {
                        //dbTran.Rollback();
                    }
               // }

                return AmntHeadId;


            }

        }


        public Int64 Update(tblAmntRecvdGR_Head obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 AmntHeadId  = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                //using (DbTransaction dbTran = db.Connection.BeginTransaction())
                //{
                    try
                    {
                        tblAmntRecvdGR_Head CH = db.tblAmntRecvdGR_Head.Where(rh => rh.Rcpt_No == obj.Rcpt_No && rh.Head_Idno != Head_Idno && rh.BaseCity_Idno == obj.BaseCity_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            AmntHeadId = -1;
                        }
                        else
                        {
                            tblAmntRecvdGR_Head TBH = db.tblAmntRecvdGR_Head.Where(rh => rh.Head_Idno == Head_Idno).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Rcpt_No = obj.Rcpt_No;
                                TBH.Rcpt_date = obj.Rcpt_date;
                                TBH.Year_IdNo = obj.Year_IdNo;
                                TBH.RcptTyp_Idno = obj.RcptTyp_Idno;
                                TBH.Party_IdNo = obj.Party_IdNo;
                                TBH.Inst_No = obj.Inst_No;
                                TBH.Inst_Date = obj.Inst_Date;
                                TBH.CustBank_Idno = obj.CustBank_Idno;
                                TBH.Comp_Id = obj.Comp_Id;
                                TBH.Remark = obj.Remark;
                                TBH.status = obj.status;
                                TBH.Date_Modified = obj.Date_Modified;
                                TBH.Net_Amnt = obj.Net_Amnt;
                                TBH.FromCity_Idno = obj.FromCity_Idno;
                                db.SaveChanges();
                                AmntHeadId = TBH.Head_Idno;
                                List<tblAmntRecvGR_Detl> ChlnDetl = db.tblAmntRecvGR_Detl.Where(rd => rd.Head_Idno == Head_Idno).ToList();
                                foreach (tblAmntRecvGR_Detl rgd in ChlnDetl)
                                {
                                    db.tblAmntRecvGR_Detl.DeleteObject(rgd);
                                    db.SaveChanges();
                                }
                                if (Dttemp != null && Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        if (Convert.ToDouble(Dr["Recv_Amount"]) > 0)
                                        {

                                            tblAmntRecvGR_Detl objtblAmntRecvGR_Detl = new tblAmntRecvGR_Detl();
                                            objtblAmntRecvGR_Detl.Head_Idno = Head_Idno;
                                            objtblAmntRecvGR_Detl.Gr_Idno = Convert.ToInt64(Dr["Gr_Idno"]);
                                            objtblAmntRecvGR_Detl.Gr_No = Convert.ToInt64(Dr["Gr_no"]);
                                            objtblAmntRecvGR_Detl.Gr_Date = Convert.ToDateTime(Dr["Gr_Date"]);
                                            objtblAmntRecvGR_Detl.Recivr_Idno = Convert.ToInt64(Dr["Recivr_Idno"]);
                                            objtblAmntRecvGR_Detl.GR_Frm = Convert.ToString(Dr["GR_From"]);
                                            objtblAmntRecvGR_Detl.To_City = Convert.ToInt64(Dr["To_City"]);
                                            objtblAmntRecvGR_Detl.From_City = Convert.ToInt64(Dr["From_City"]);
                                            objtblAmntRecvGR_Detl.Gr_Amnt = Convert.ToDouble(Dr["Amount"]);
                                            objtblAmntRecvGR_Detl.Recvd_Amnt = Convert.ToDouble(Dr["Recv_Amount"]);
                                            objtblAmntRecvGR_Detl.Cur_Bal = Convert.ToDouble(Dr["Cur_Bal"]);
                                            objtblAmntRecvGR_Detl.Tot_Recvd = Convert.ToDouble(Dr["Tot_Recvd"]);
                                            db.tblAmntRecvGR_Detl.AddObject(objtblAmntRecvGR_Detl);
                                            db.SaveChanges();
                                        }
                                    }
                                }

                              //  dbTran.Commit();
                            }
                        }



                    }
                    catch
                    {
                       // dbTran.Rollback();
                    }
                //}
            }
            return AmntHeadId;
        }

        public IList search(Int32 yearid, Int64 GrNo, DateTime? dtfrom, DateTime? dtto, int FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblAmntRecvdGR_Head
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno
                           join AM in db.AcntMasts on CH.Party_IdNo equals AM.Acnt_Idno
                           select new
                           {
                               CH.BaseCity_Idno,
                               CH.Rcpt_date,
                               CH.Rcpt_No,
                               CH.Head_Idno,
                               FromCity = cito.City_Name,
                               CH.Year_IdNo,
                               AM.Acnt_Name,
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


        public DataTable SelectGRPaymentDetail(string Action, Int64 YearId, DateTime dtFrmDate, DateTime dtToDate, Int64 PrtIdno, string strGrFrm, Int32 FromCity, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@YEARIDNO", YearId);
            objSqlPara[2] = new SqlParameter("@GR_frmDate", dtFrmDate);
            objSqlPara[3] = new SqlParameter("@GR_ToDate", dtToDate);
            objSqlPara[4] = new SqlParameter("@PrtyIdno", PrtIdno);
            objSqlPara[5] = new SqlParameter("@GrFrm", strGrFrm);
            objSqlPara[6] = new SqlParameter("@FromCity", FromCity);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdGR", objSqlPara);
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


        public DataTable SelectGRDetail(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetail");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdGR", objSqlPara);
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
        public List<AcntMast> BindParty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type==2 && cm.INTERNAL==false   orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }


        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spAmntRecvdGR", objSqlPara);
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
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                tblAmntRecvdGR_Head CH = db.tblAmntRecvdGR_Head.Where(h => h.Head_Idno == HeadId).FirstOrDefault();
                List<tblAmntRecvGR_Detl> CD = db.tblAmntRecvGR_Detl.Where(d => d.Head_Idno == HeadId).ToList();
                if (CH != null)
                {
                    foreach (var d in CD   )
                    {
                        SqlParameter[] objSqlPara = new SqlParameter[3];
                        objSqlPara[0] = new SqlParameter("@Action", "DeleteRcvdAmntAgnstGR");
                        objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                        objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                        Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                        db.tblAmntRecvGR_Detl.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblAmntRecvdGR_Head.DeleteObject(CH);

                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "ARGR");
                    
                    db.SaveChanges();
                   
                    if (intValue > 0)
                    {
                        value = 1;
                    }
                }
            }
            return value;
        }

        public DataSet SelectBankName(string prefix, string con)
        {
            string str = string.Empty;
            str = @"SELECT Acnt_Name,Acnt_Idno FROM acntmast WHERE Acnt_Type=4 and Acnt_Name like '" + prefix + "%'  order by Acnt_Name Asc";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            return ds;
        }

        public DataSet SelectpartyName(int AcntID ,string con)
        {
            string Query = "SELECT Acnt_Name,Acnt_Idno FROM  AcntMast where Acnt_Idno =" + AcntID + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, Query);
            return ds;
        }

    }
}
