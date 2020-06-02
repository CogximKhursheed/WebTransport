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
    public class DeliveryChallanDetailsDAL
    {

        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }

        public tblUserPref selectUserPref()
        {

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                db.Connection.Close();
                return Objtbl;

            }
        }

        public Int32 GetChallanNo(Int32 YearIdno, string Constring)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlParameter = new SqlParameter[3];
                objSqlParameter[0] = new SqlParameter("@Action", "SelectMaxChlnNo");
                objSqlParameter[1] = new SqlParameter("@YearIdno", YearIdno);
                objSqlParameter[2] = new SqlParameter("@ChlnType", 1);
                Int32 MaxNo = 0;
                MaxNo = Convert.ToInt32(SqlHelper.ExecuteScalar(Constring, CommandType.StoredProcedure, "spDeliveryChallan", objSqlParameter));
                return MaxNo;
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

        public List<AcntMast> selectOwnerDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from DR in db.AcntMasts where DR.Acnt_Type == 9 orderby DR.Acnt_Name ascending select DR).ToList();
                return lst;
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
        public LorryMast selectOwnerName(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                LorryMast lst = ((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault());
                return lst;
            }
        }

        public Int32 selectTruckType(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Lst = 0;
                Lst = Convert.ToInt32((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR.Lorry_Type).FirstOrDefault());
                return Lst;
            }
        }

        public IList selectTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from LM in db.LorryMasts orderby LM.Lorry_No ascending select LM).ToList();
                return lst;
            }
        }

        public IList search(Int32 yearid, string chlnNo, DateTime? dtfrom, DateTime? dtto, int FromCity, Int32 TruckNo, Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblChlnBookHeads
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno

                           join LM in db.LorryMasts on CH.Truck_Idno equals LM.Lorry_Idno
                           select new
                             {

                                 CH.DelvryPlc_Idno,
                                 CH.BaseCity_Idno,
                                 CH.Chln_Date,
                                 CH.Chln_No,
                                 CH.Truck_Idno,
                                 LM.Lorry_No,
                                 CH.Chln_Idno,
                                 FromCity = cito.City_Name,
                                 //  Driver_Name=(LM.Lorry_Type==1)?()
                                 CH.Year_Idno,
                                 CH.Net_Amnt
                             }).ToList();
                if (chlnNo != "")
                {
                    lst = lst.Where(l => l.Chln_No == chlnNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Chln_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Chln_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }
                //if (DelvPlce > 0)
                //{
                //    lst = lst.Where(l => l.DelvryPlc_Idno == DelvPlce).ToList();
                //}

                if (TruckNo > 0)
                {
                    lst = lst.Where(l => l.Truck_Idno == TruckNo).ToList();
                }
                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearid).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.BaseCity_Idno)).ToList();
                }

                return lst;
            }
        }

        public IList SearchGR(Int32 YearId, DateTime? dtfrom, DateTime? dtto, Int64 DelvPlace)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from ListHead in db.tblChlnDelvHeads
                           join ListDetl in db.tblChlnDelvDetls on ListHead.ChlnDelvHead_Idno equals ListDetl.ChlnDelvHead_Idno
                           join ListGr in db.TblGrHeads on ListDetl.Gr_Idno equals ListGr.GR_Idno
                           join ListGrDetl in db.TblGrDetls on ListGr.GR_Idno equals ListGrDetl.GrHead_Idno
                           join ListAcnt in db.AcntMasts on ListGr.Recivr_Idno equals ListAcnt.Acnt_Idno
                           join ListAcnt2 in db.AcntMasts on ListGr.Sender_Idno equals ListAcnt2.Acnt_Idno
                           join ListCity in db.tblCityMasters on ListHead.ToCity_Idno equals ListCity.City_Idno
                           where ListDetl.DelvChlnHead_Idno == 0 || ListDetl.DelvChlnHead_Idno == null
                           select new
                               {
                                   ListGr.Gr_No,
                                   ListGr.Gr_Date,
                                   Reciver = ListAcnt.Acnt_Name,
                                   Sender = ListAcnt2.Acnt_Name,
                                   ListCity.City_Name,
                                   Qty = (from obj in db.TblGrDetls where obj.GrHead_Idno == ListGr.GR_Idno select obj.Qty).Sum(),
                                   ListGr.Net_Amnt,
                                   ListGr.Remark,
                                   ListHead.ChlnDelv_Date,
                                   ListHead.Year_Idno,
                                   ListGr.To_City,
                                   ListDetl.Gr_Idno

                               }).Distinct().ToList();

                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ChlnDelv_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ChlnDelv_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (YearId > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == YearId).ToList();
                }
                if (DelvPlace > 0)
                {
                    lst = lst.Where(l => l.To_City == DelvPlace).ToList();
                }
                return lst;
            }
        }
        public Int64 countall()
        { 
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
            Int32 count = 0;
            count = (from ListHead in db.tblDelvChlnHeads
                     join Listlorry in db.LorryMasts on ListHead.Truck_Idno equals Listlorry.Lorry_Idno
                     join ListCity in db.tblCityMasters on ListHead.DelvryPlc_Idno equals ListCity.City_Idno
                     join ListTrans in db.AcntMasts on ListHead.Transprtr_Idno equals ListTrans.Acnt_Idno
                     select ListHead.DelvChln_Idno).Count();
            return count;

            }
        }
        public IList SearchDelvChallan(Int64 UserIdno, Int32 YearId, DateTime? dtfrom, DateTime? dtto, Int64 DelvPlace, Int64 ChallanNo, Int32 DriverIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from ListHead in db.tblDelvChlnHeads
                           join Listlorry in db.LorryMasts on ListHead.Truck_Idno equals Listlorry.Lorry_Idno
                           join ListCity in db.tblCityMasters on ListHead.DelvryPlc_Idno equals ListCity.City_Idno
                           join ListTrans in db.AcntMasts on ListHead.Transprtr_Idno equals ListTrans.Acnt_Idno
                           select new
                           {
                               ListHead.DelvChln_No,
                               ListHead.DelvChln_Date,
                               ListCity.City_Name,
                               ListHead.Truck_Idno,
                               Listlorry.Lorry_No,
                               ListHead.Net_Amnt,
                               ListHead.DelvChln_Idno,
                               ListHead.Year_Idno,
                               ListHead.DelvryPlc_Idno,
                               ListTrans.Acnt_Name,
                               ListHead.Gross_Amnt,
                               ListHead.Katt_Amnt,
                               ListHead.Other_Amnt
                           }).Distinct().ToList();

                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.DelvChln_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.DelvChln_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (YearId > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == YearId).ToList();
                }
                if (DelvPlace > 0)
                {
                    lst = lst.Where(l => l.DelvryPlc_Idno == DelvPlace).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.DelvryPlc_Idno)).ToList();
                }
                if (ChallanNo > 0)
                {
                    lst = lst.Where(l => l.DelvChln_No == ChallanNo).ToList();
                }
                if (DriverIdno > 0)
                {
                    lst = lst.Where(l => l.Truck_Idno == DriverIdno).ToList();
                }
                return lst;
            }
        }
        public DataTable ReportDelvChallan(Int64 UserIdno, Int32 YearId, DateTime? dtfrom, DateTime? dtto, Int64 DelvPlace, Int64 ChallanNo, string Connstr)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", "ReportDelvChal");
            objSqlPara[1] = new SqlParameter("@ChlnDate", dtfrom);
            objSqlPara[2] = new SqlParameter("@ToDate", dtto);
            objSqlPara[3] = new SqlParameter("@UserIdno", UserIdno);
            objSqlPara[4] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[5] = new SqlParameter("@ChlnNo", ChallanNo);
            objSqlPara[6] = new SqlParameter("@DelvryPlcIdno", DelvPlace);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(Connstr, CommandType.StoredProcedure, "spDeliveryChallan", objSqlPara);
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
        public DataTable SelectDELVGrDetails(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDELVDetailInGR");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spDeliveryChallan", objSqlPara);
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

        public tblDelvChlnHead selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblDelvChlnHeads.Where(h => h.DelvChln_Idno == HeadId).FirstOrDefault();
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

        public Int64 Insert(tblDelvChlnHead obj, DataTable Dttemp, Int32 DelvPlcIdno, string ConnString)
        {
            Int64 chlnBookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                try
                {
                    tblDelvChlnHead RH = db.tblDelvChlnHeads.Where(rh => (rh.DelvChln_No == obj.DelvChln_No) && (rh.Year_Idno == obj.Year_Idno)).FirstOrDefault();
                    if (RH != null)
                    {
                        chlnBookId = -1;
                    }
                    else
                    {
                        db.tblDelvChlnHeads.AddObject(obj);
                        db.SaveChanges();
                        chlnBookId = obj.DelvChln_Idno;
                        if (Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                tblDelvChlnDetl objtblChlnDetl = new tblDelvChlnDetl();
                                objtblChlnDetl.DelvChlnHead_Idno = chlnBookId;
                                objtblChlnDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                                objtblChlnDetl.Delivered = false;
                                db.tblDelvChlnDetls.AddObject(objtblChlnDetl);
                                db.SaveChanges();
                            }

                        }
                        if (Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                string StrQuery = "Update tblchlndelvdetl set DelvChlnHead_Idno=" + chlnBookId + " where Gr_idno=" + Convert.ToInt32(Dr["Gr_Idno"]) + "";
                                SqlHelper.ExecuteNonQuery(ConnString, CommandType.Text, StrQuery);
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            return chlnBookId;
        }

        public Int64 Update(tblDelvChlnHead obj, Int32 ChlnIdno, DataTable Dttemp, Int64 Delvno, string ConnString)
        {
            Int64 chlnBoookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();

                try
                {
                    tblDelvChlnHead CH = db.tblDelvChlnHeads.Where(rh => rh.DelvChln_No == Delvno && (rh.DelvChln_Idno != ChlnIdno) && (rh.Year_Idno == obj.Year_Idno)).FirstOrDefault();
                    if (CH != null)
                    {
                        chlnBoookId = -1;
                    }
                    else
                    {
                        tblDelvChlnHead TBH = db.tblDelvChlnHeads.Where(rh => rh.DelvChln_Idno == ChlnIdno).FirstOrDefault();
                        if (TBH != null)
                        {
                            TBH.DelvChln_No = obj.DelvChln_No;
                            TBH.DelvChln_Date = obj.DelvChln_Date;
                            TBH.DelvryPlc_Idno = obj.DelvryPlc_Idno;
                            TBH.Truck_Idno = obj.Truck_Idno;
                            TBH.Year_Idno = obj.Year_Idno;
                            TBH.Driver_Idno = obj.Driver_Idno;
                            TBH.Delvry_Instrc = obj.Delvry_Instrc;
                            TBH.Transprtr_Idno = obj.Transprtr_Idno;
                            TBH.Inv_Idno = obj.Inv_Idno;
                            TBH.Gross_Amnt = obj.Gross_Amnt;
                            TBH.Other_Amnt = obj.Other_Amnt;
                            TBH.Net_Amnt = obj.Net_Amnt;
                            TBH.Katt_Amnt = obj.Katt_Amnt;
                            TBH.Date_Modifided = Convert.ToDateTime(DateTime.Now);
                            db.SaveChanges();
                            chlnBoookId = TBH.DelvChln_Idno;


                            //List<tblDelvChlnDetl> ChlnDetl = db.tblDelvChlnDetls.Where(rd => rd.DelvChlnHead_Idno == ChlnIdno).ToList();
                            //foreach (tblDelvChlnDetl rgd in ChlnDetl)
                            //{
                            //    db.tblDelvChlnDetls.DeleteObject(rgd);
                            //    db.SaveChanges();
                            //}

                            //if (Dttemp.Rows.Count > 0)
                            //{
                            //    foreach (DataRow Dr in Dttemp.Rows)
                            //    {
                            //        tblChlnBookDetl objtblChlnBookDetl = new tblChlnBookDetl();
                            //        objtblChlnBookDetl.GR_Idno = Convert.ToInt32(Dr["Gr_Idno"]);
                            //        objtblChlnBookDetl.DelvryPlce_Idno = DelvPlcIdno;
                            //        objtblChlnBookDetl.ChlnBookHead_Idno = chlnBoookId;
                            //        db.tblChlnBookDetls.AddObject(objtblChlnBookDetl);
                            //        db.SaveChanges();
                            //    }
                            //    //  dbTran.Commit();
                            //}
                            //if (Dttemp.Rows.Count > 0)
                            //{
                            //    foreach (DataRow Dr in Dttemp.Rows)
                            //    {
                            //        Int32 GrIdno = 0;
                            //        GrIdno = Convert.ToInt32(Dr["Gr_Idno"]); ;
                            //        TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                            //        objTblGrHead.Chln_Idno = chlnBoookId;
                            //        db.SaveChanges();
                            //    }
                            //}

                        }
                    }
                }
                catch
                {

                }

            }
            return chlnBoookId;
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

        public DataTable SelectDataForCommissinAmnt(string con, string AllItmIdno)
        {
            SqlParameter[] objsqlpara = new SqlParameter[2];
            objsqlpara[0] = new SqlParameter("@Action", "SelectDataForCommissinAmnt");
            objsqlpara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookng", objsqlpara);
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

        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spDeliveryChallan", objSqlPara);
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

        public int Delete(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblDelvChlnHead CH = db.tblDelvChlnHeads.Where(h => h.DelvChln_Idno == HeadId).FirstOrDefault();
                List<tblDelvChlnDetl> CD = db.tblDelvChlnDetls.Where(d => d.DelvChlnHead_Idno == HeadId).ToList();
                if (CH != null)
                {
                    foreach (var d in CD)
                    {
                        tblChlnDelvDetl objTblchln = (from obj in db.tblChlnDelvDetls where obj.Gr_Idno == d.GR_Idno select obj).FirstOrDefault();
                        objTblchln.DelvChlnHead_Idno = 0;
                        db.SaveChanges();
                    }

                    foreach (var d in CD)
                    {
                        db.tblDelvChlnDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblDelvChlnHeads.DeleteObject(CH);

                    //   Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "CB");
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public Int32 CheckBilled(Int64 Id, string con)
        {
            Int32 value = 0;

            SqlParameter[] objSqlParameter = new SqlParameter[2];
            objSqlParameter[0] = new SqlParameter("@Action", "SelectBilled");
            objSqlParameter[1] = new SqlParameter("@Id", Id);
            value = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spChlnBookng", objSqlParameter));
            return value;

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

        public DataTable BindRcptTypeDel(Int32 intAcntIdno, string con)
        {

            string sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4) and Acnt_Idno=" + intAcntIdno + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
