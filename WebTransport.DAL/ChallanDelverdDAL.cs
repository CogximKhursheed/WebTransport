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
using System.Data.Objects.SqlClient;

namespace WebTransport.DAL
{
    public class ChallanDelverdDAL
    {
        #region Material Receipt HO...
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from obj in db.tblChlnDelvHeads
                         join CH in db.tblChlnBookHeads on obj.ChlnTransf_Idno equals CH.Chln_Idno
                         join CM in db.tblCityMasters on obj.ToCity_Idno equals CM.City_Idno
                         select obj.ChlnDelvHead_Idno).Count();

                return Count;
            }

        }
        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }
        public IList Search (DateTime ? DtFrom,DateTime  ? DtTo,Int32 RcptNo,string TransfNo,Int32 FromCity,Int32 YearIdno)
          {
              using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
              {

                  var lst = (from obj in db.tblChlnDelvHeads
                             join CH in db.tblChlnBookHeads on obj.ChlnTransf_Idno equals CH.Chln_Idno
                             join CM in db.tblCityMasters on obj.ToCity_Idno equals CM.City_Idno
                              select
                                 new
                                 {
                                     obj.ChlnDelv_No,
                                     obj.ChlnDelv_Date,
                                     obj.ToCity_Idno,
                                     obj.Year_Idno,
                                      CH.Chln_No,
                                      CM.City_Name,
                                     CH.Chln_Date,
                                     obj.ChlnDelvHead_Idno
                                 }
                      ).ToList();
                  if (DtFrom!=null)
                  {
                      lst = (from l in lst where l.ChlnDelv_Date >= DtFrom select l).ToList();
                  }
                  if (DtTo != null)
                  {
                      lst = (from l in lst where l.ChlnDelv_Date <= DtTo select l).ToList();
                  }
                  if (FromCity>0)
                  {
                      lst = (from l in lst where l.ToCity_Idno == FromCity select l).ToList();
                  }
                  if (RcptNo > 0)
                  {
                      lst = (from l in lst where l.ChlnDelv_No == RcptNo select l).ToList();
                  }
                  if (TransfNo !="")
                  {
                      lst = (from l in lst where l.Chln_No == TransfNo select l).ToList();
                  }
                  if (YearIdno > 0)
                  {
                      lst = (from l in lst where l.Year_Idno == YearIdno select l).ToList();
                  }
                  return lst;
              }
          }
        public Int64 InsertMtrlRcptHOHead(tblChlnDelvHead objMtrlRcptHOHead,DataTable DtTemp)
        {
            Int64 RcptHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
              {
          
                try
            {

                tblChlnDelvHead RH = db.tblChlnDelvHeads.Where(rh => (rh.ChlnTransf_Idno == objMtrlRcptHOHead.ChlnTransf_Idno) && (rh.ToCity_Idno == objMtrlRcptHOHead.ToCity_Idno)).FirstOrDefault();
                if (RH != null)
                {
                    RcptHeadIdno = -1;
                }
                else
                {
                    db.tblChlnDelvHeads.AddObject(objMtrlRcptHOHead);
                    db.SaveChanges();
                    RcptHeadIdno = objMtrlRcptHOHead.ChlnDelvHead_Idno;
                    if (DtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow Dr in DtTemp.Rows)
                        {
                          //  ApplicationFunction.CreateTable("tbl", "Gr_Idno", "String", "Item_Idno", "String", "Unit_Idno", "String", "Rate_Type", "String", "Qty", "Weight", "", "String", "Amount", "String", "Delv_Qty", "String", "Reamrk", "String");
                            tblChlnDelvDetl objtblChlnDelvDetl = new tblChlnDelvDetl();
                            objtblChlnDelvDetl.Gr_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                            objtblChlnDelvDetl.Item_Idno = Convert.ToInt32(Dr["Item_Idno"]);
                            objtblChlnDelvDetl.Unit_Idno = Convert.ToInt32(Dr["Unit_Idno"]);
                            objtblChlnDelvDetl.Rate_Type = Convert.ToInt32(Dr["Rate_Type"]);
                            objtblChlnDelvDetl.Qty = Convert.ToInt32(Dr["Qty"]);
                            objtblChlnDelvDetl.Weight = Convert.ToDouble(Dr["Weight"]);
                            objtblChlnDelvDetl.Amount = Convert.ToDouble(Dr["Amount"]);
                            objtblChlnDelvDetl.Reamrk = Convert.ToString(Dr["Reamrk"]);
                            objtblChlnDelvDetl.Delv_Qty = Convert.ToDouble(Dr["Delv_Qty"]);
                            objtblChlnDelvDetl.Delv_Weight = Convert.ToDouble(Dr["Delv_Wt"]);
                            objtblChlnDelvDetl.Delv_Amount = Convert.ToDouble(Dr["Delv_Amount"]);
                            objtblChlnDelvDetl.Reamrk = Convert.ToString(Dr["Reamrk"]); 
                            objtblChlnDelvDetl.ChlnDelvHead_Idno = RcptHeadIdno;
                            db.tblChlnDelvDetls.AddObject(objtblChlnDelvDetl);
                            db.SaveChanges();
                        }
                     
                    }
                    if (DtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow Dr in DtTemp.Rows)
                        {
                            Int32 GrIdno = 0;
                          GrIdno=  Convert.ToInt32(Dr["Gr_Idno"]);
                          TblGrHead objTblGrHead = (from obj in db.TblGrHeads where obj.GR_Idno == GrIdno select obj).FirstOrDefault();
                          if (objTblGrHead!=null)
                          {
                              objTblGrHead.Gr_Delv = true;
                              objTblGrHead.ChlnDelv_Idno = RcptHeadIdno;
                              db.SaveChanges();
                          }
                        }
                      
                    }
                   
                }
            }
            catch
            {
                return 0;
            }
            return RcptHeadIdno;
        }
            
        }
       

        public Int64 UpdateMtrlRcptHOHead(tblChlnDelvHead objMtrlRcptHOHead,DataTable DtTemp,Int32  HeadIdno)
        {
            Int64 RcptHeadIdno = HeadIdno;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblChlnDelvHead RH = db.tblChlnDelvHeads.Where(rh => (rh.ChlnTransf_Idno == objMtrlRcptHOHead.ChlnTransf_Idno) && (rh.ToCity_Idno == objMtrlRcptHOHead.ToCity_Idno) && (rh.Year_Idno == objMtrlRcptHOHead.Year_Idno) && (rh.ChlnDelvHead_Idno != HeadIdno)).FirstOrDefault();
                   if (RH != null)
                   {
                       RcptHeadIdno = -1;
                   }
                   else
                   {
                       tblChlnDelvHead objRcptHOHead = (from obj in db.tblChlnDelvHeads
                                                        where obj.ChlnDelvHead_Idno == RcptHeadIdno
                                                        select obj).SingleOrDefault();
                       if (objRcptHOHead != null)
                       {
                           objRcptHOHead.ChlnDelv_Date = objMtrlRcptHOHead.ChlnDelv_Date;
                           objRcptHOHead.ToCity_Idno = objMtrlRcptHOHead.ToCity_Idno;
                           objRcptHOHead.ChlnTransf_Idno = objMtrlRcptHOHead.ChlnTransf_Idno;
                          objRcptHOHead.LocGR_Amnt=objMtrlRcptHOHead.LocGR_Amnt;
                          objRcptHOHead.CrsngGR_Amnt=objMtrlRcptHOHead.CrsngGR_Amnt;
                          objRcptHOHead.Net_Amnt = objMtrlRcptHOHead.Net_Amnt;
                           objRcptHOHead.Date_Modified = MultipleDBDAL.GetIndianDateTime().Date;   // DateTime.Now.Date;
                           objRcptHOHead.Year_Idno = objMtrlRcptHOHead.Year_Idno;
                           db.SaveChanges();

                       }

                       List<tblChlnDelvDetl> ChlnDetl = db.tblChlnDelvDetls.Where(rd => rd.ChlnDelvHead_Idno == RcptHeadIdno).ToList();
                       foreach (tblChlnDelvDetl rgd in ChlnDetl)
                       {
                           db.tblChlnDelvDetls.DeleteObject(rgd);
                           db.SaveChanges();
                       }


                       if (DtTemp.Rows.Count > 0)
                       {
                           foreach (DataRow Dr in DtTemp.Rows)
                           {
                               tblChlnDelvDetl objtblChlnDelvDetl = new tblChlnDelvDetl();
                               objtblChlnDelvDetl.Gr_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                               objtblChlnDelvDetl.Item_Idno = Convert.ToInt32(Dr["Item_Idno"]);
                               objtblChlnDelvDetl.Unit_Idno = Convert.ToInt32(Dr["Unit_Idno"]);
                               objtblChlnDelvDetl.Rate_Type = Convert.ToInt32(Dr["Rate_Type"]);
                               objtblChlnDelvDetl.Qty = Convert.ToInt32(Dr["Qty"]);
                               objtblChlnDelvDetl.Weight = Convert.ToDouble(Dr["Weight"]);
                               objtblChlnDelvDetl.Amount = Convert.ToDouble(Dr["Amount"]);
                               objtblChlnDelvDetl.Reamrk = Convert.ToString(Dr["Reamrk"]);
                               objtblChlnDelvDetl.Delv_Qty = Convert.ToDouble(Dr["Delv_Qty"]);
                               objtblChlnDelvDetl.Delv_Weight = Convert.ToDouble(Dr["Delv_Wt"]);
                               objtblChlnDelvDetl.Delv_Amount = Convert.ToDouble(Dr["Delv_Amount"]);
                               objtblChlnDelvDetl.Reamrk = Convert.ToString(Dr["Reamrk"]); 
                               objtblChlnDelvDetl.ChlnDelvHead_Idno = RcptHeadIdno;
                               db.tblChlnDelvDetls.AddObject(objtblChlnDelvDetl);
                               db.SaveChanges();
                           }
                       }
                       if (DtTemp.Rows.Count > 0)
                       {
                           foreach (DataRow Dr in DtTemp.Rows)
                           {
                               Int32 GrIdno = 0;
                               GrIdno = Convert.ToInt32(Dr["Gr_Idno"]);
                               TblGrHead objTblGrHead = (from obj in db.TblGrHeads where obj.GR_Idno == GrIdno select obj).FirstOrDefault();
                               if (objTblGrHead != null)
                               {
                                   objTblGrHead.Gr_Delv = true;
                                   objTblGrHead.ChlnDelv_Idno = RcptHeadIdno;
                                   db.SaveChanges();
                               }
                           }

                       }
                   }

                }
            }
            catch (Exception ex)
            {

            }
            return RcptHeadIdno;
        }

        public int DeleteMtrlRcptHODetl(Int64 RcptDetlIdno)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    List<tblChlnDelvDetl> obj = (from objWC in db.tblChlnDelvDetls
                                                 where objWC.ChlnDelvHead_Idno == RcptDetlIdno
                                                 select objWC).ToList();
                    if (obj != null)
                    {
                        db.DeleteObject(obj);
                        db.SaveChanges();
                        Value = 1;
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return Value;
        }
        public int DeleteMtrlRcptHO(Int64 RcptIdno)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        List<tblChlnDelvDetl> objRcptDetl = (from objDetl in db.tblChlnDelvDetls
                                                             where objDetl.Id == RcptIdno
                                                             select objDetl).ToList();
                        if (objRcptDetl != null)
                        {
                            db.DeleteObject(objRcptDetl);
                            db.SaveChanges();
                            tblChlnDelvHead objRcptHead = (from objHead in db.tblChlnDelvHeads
                                                           where objHead.ChlnDelvHead_Idno == RcptIdno
                                                           select objHead).SingleOrDefault();
                            if (objRcptHead != null)
                            {
                                db.DeleteObject(objRcptHead);
                                db.SaveChanges();
                                Value = 1;
                                tScope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return Value;
        }

        public Int64 MaxRcptHeadNo(int FromCity_Idno, int YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //tblMtrlRcptHOHead objtblCFAMast = new tblMtrlRcptHOHead();
                Int64 RecHeadNo = Convert.ToInt64((from obj in db.tblChlnDelvHeads
                                                   where (obj.ToCity_Idno == FromCity_Idno) && (obj.Year_Idno == YearIdno)
                                                   select obj.ChlnDelv_No).Max());
                return (RecHeadNo + 1);
            }
        }

        public IList SelectMatTransfNo(Int64 Tocity_Idno, int RepTyp, int YearId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (RepTyp == 0)
                {
                    var lstNo = (from  objDetl in db.tblChlnDelvDetls
                                 join objNo in db.tblChlnDelvHeads on objDetl.ChlnDelvHead_Idno equals objNo.ChlnDelvHead_Idno
                                 where objNo.ToCity_Idno == Tocity_Idno
                                 select objDetl.Gr_Idno).Distinct().ToList();
                    var lst = (from obj in db.tblChlnBookHeads
                               join CD in db.tblChlnBookDetls on obj.Chln_Idno equals CD.ChlnBookHead_Idno
                               join GH in db.TblGrHeads on CD.GR_Idno equals GH.GR_Idno
                               join CM in db.tblCityMasters on obj.BaseCity_Idno equals CM.City_Idno
                               where GH.To_City == Tocity_Idno 
                               && !lstNo.Contains(GH.GR_Idno)  
                               orderby obj.Chln_Date descending, obj.Chln_No
                               select 
                               new {
                                   chln_No = (string.IsNullOrEmpty(obj.Chln_No) ? "" : obj.Chln_No) + "-" +  ((CM.City_Name == null) ? "" : CM.City_Name),
                               chln_Idno=obj.Chln_Idno
                               }
                               ).Distinct().ToList();
                    return lst;
                    //
                }
                else
                {
                    var lstNo = (from  objDetl in db.tblChlnDelvDetls
                                 join objNo in db.tblChlnDelvHeads on objDetl.ChlnDelvHead_Idno equals objNo.ChlnDelvHead_Idno
                                 where objNo.ToCity_Idno == Tocity_Idno && objNo.Year_Idno == YearId
                                 select objDetl.Gr_Idno).Distinct().ToList();
                    var lst = (from obj in db.tblChlnBookHeads
                               join CD in db.tblChlnBookDetls on obj.Chln_Idno equals CD.ChlnBookHead_Idno
                               join GH in db.TblGrHeads on CD.GR_Idno equals GH.GR_Idno
                               join CM in db.tblCityMasters on obj.BaseCity_Idno equals CM.City_Idno
                               where GH.To_City == Tocity_Idno
                               && lstNo.Contains(GH.GR_Idno) //&& obj.Year_Idno == YearId
                               orderby obj.Chln_Date descending, obj.Chln_No
                              select 
                               new {
                                   chln_No = (string.IsNullOrEmpty(obj.Chln_No) ? "" : obj.Chln_No) + "-" + ((CM.City_Name == null) ? "" : CM.City_Name),
                               chln_Idno=obj.Chln_Idno
                               }).Distinct().ToList();
                    return lst;
                }
            }
        }

        public DataSet SelectGridDetl(string Constring,string Action, Int64 MatTransfHeadIdno,Int32 Tocity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
               SqlParameter [] objSqlParameter=new SqlParameter[3];
               objSqlParameter[0] = new SqlParameter("@Action","SelectTransfDetl");
               objSqlParameter[1] = new SqlParameter("@ChlnTransfIdno", MatTransfHeadIdno);
               objSqlParameter[2] = new SqlParameter("@Tocity", Tocity);
                DataSet ds = SqlHelper.ExecuteDataset(Constring, CommandType.StoredProcedure, "spChlnRecpt", objSqlParameter);
                return ds;
            }
        }
        

        public tblChlnDelvHead SelectHeadInEdit(Int64 MtrlRcptHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblChlnDelvHeads
                        where obj.ChlnDelvHead_Idno == MtrlRcptHeadIdno
                        select obj).SingleOrDefault();
            }
        }

        public DataSet SelectGridDetlInEdit(string Constring,  Int64 MtrlRcptHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[2];
                objSqlParameter[0] = new SqlParameter("@Action", "SelectDetl");
                objSqlParameter[1] = new SqlParameter("@RcptHeadIdno", MtrlRcptHeadIdno);
                DataSet ds = SqlHelper.ExecuteDataset(Constring, CommandType.StoredProcedure, "spChlnRecpt", objSqlParameter);
                return ds;
            }
        }

        public tblChlnDelvHead IsExists(Int64 MtrlTransfIdno, int FromCityIdno, Int64 MtrlRcptIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblChlnDelvHead objMtrlRcptHOHead = new tblChlnDelvHead();
                if (MtrlRcptIdno <= 0)
                {
                    objMtrlRcptHOHead = (from obj in db.tblChlnDelvHeads
                                         where obj.ChlnTransf_Idno == MtrlTransfIdno
                                         && obj.ToCity_Idno == FromCityIdno
                                         select obj).SingleOrDefault();
                }
                else if (MtrlRcptIdno > 0)
                {
                    objMtrlRcptHOHead = (from obj in db.tblChlnDelvHeads
                                         where obj.ChlnTransf_Idno == MtrlTransfIdno
                                            && obj.ToCity_Idno == FromCityIdno
                                           && obj.ChlnDelvHead_Idno != MtrlRcptIdno
                                         select obj).SingleOrDefault();
                }
                return objMtrlRcptHOHead;
            }
        }

        public int IsExeistMtrlTransf(Int64 MtrlTransfIdno, int FromCityIdno, Int64 MtrlRcptIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int Rec = 0;
                if (MtrlRcptIdno <= 0)
                {
                    Rec = Convert.ToInt32((from obj in db.tblChlnDelvHeads
                                           where obj.ToCity_Idno == FromCityIdno && obj.ChlnTransf_Idno == MtrlTransfIdno
                                           select obj).Count());
                }
                else if (MtrlRcptIdno > 0)
                {
                    Rec = Convert.ToInt32((from obj in db.tblChlnDelvHeads
                                           where obj.ToCity_Idno == FromCityIdno && obj.ChlnTransf_Idno == MtrlTransfIdno
                                           && obj.ChlnDelvHead_Idno != MtrlRcptIdno
                                           select obj).Count());
                }
                return (Rec);
            }
        }

        public DateTime MtrlTransfDate(Int64 MtrlTransfIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                DateTime dt;
                dt = Convert.ToDateTime((from obj in db.tblChlnBookHeads
                                         where obj.Chln_Idno == MtrlTransfIdno
                                         select obj.Chln_Date).FirstOrDefault());
                return (dt);
            }
        }

        public int checkRelation(Int64 MtrlTransfIdno)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    Value = (from obj in db.tblChlnBookHeads
                             where obj.Chln_Idno == MtrlTransfIdno
                             select obj.Chln_Idno).ToList().Count();
                }
            }
            catch
            {
            }
            return Value;
        }
        public int DeleteALL(Int64 HeadIdno)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        List<tblChlnDelvDetl> objTranDetl = (from objDetl in db.tblChlnDelvDetls
                                                             where objDetl.ChlnDelvHead_Idno == HeadIdno
                                                             select objDetl).ToList();
                        if (objTranDetl != null)
                        {
                            foreach (var d in objTranDetl)
                            {
                                db.tblChlnDelvDetls.DeleteObject(d);
                                db.SaveChanges();
                            }
                            List<TblGrHead> objTblGrHead = (from objDetl in db.TblGrHeads
                                                                 where objDetl.ChlnDelv_Idno == HeadIdno
                                                                 select objDetl).ToList();

                            if (objTblGrHead!=null)
                            {
                                foreach(var lst in objTblGrHead)
                                {
                                    lst.ChlnDelv_Idno = 0;
                                    lst.Gr_Delv = false;
                                    db.SaveChanges();
                                }
                                
                            }
                            tblChlnDelvHead objTransHead = (from objHead in db.tblChlnDelvHeads
                                                            where objHead.ChlnDelvHead_Idno == HeadIdno
                                                            select objHead).SingleOrDefault();
                            if (objTransHead != null)
                            {
                               
                                    db.tblChlnDelvHeads.DeleteObject(objTransHead);
                                    db.SaveChanges();
                                    Value = 1;
                                tScope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return Value;
        }

        
        public tblChlnDelvHead selectDocNo(Int64 RcptId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var tbl = (from user in db.tblChlnDelvHeads where user.ChlnDelvHead_Idno == RcptId select user).FirstOrDefault();
                return tbl;
            }
        }

        public DataSet SearchDelevryRegisterReport(DateTime? DtFrom, DateTime? DtTo, Int32 ToCity, Int32 YearIdno, Int32 IuserIdno, string conString)
        {
            DataSet Dsobj = new DataSet();
            SqlParameter[] objSqlParameter = new SqlParameter[6];
            objSqlParameter[0] = new SqlParameter("@Action", "SelectRep");
            objSqlParameter[1] = new SqlParameter("@DateFrom", DtFrom);
            objSqlParameter[2] = new SqlParameter("@DateTo", DtTo);
            objSqlParameter[3] = new SqlParameter("@ToCityIdno", ToCity);
            objSqlParameter[4] = new SqlParameter("@UserIdno", IuserIdno);
            objSqlParameter[5] = new SqlParameter("@YearIdno", YearIdno);
            Dsobj = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDelRegRep", objSqlParameter);
            return Dsobj;
        }
        #endregion
    }
}
