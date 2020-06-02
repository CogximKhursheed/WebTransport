using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;

namespace WebTransport.DAL
{
    public class ChlnBookingCrsngDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region FOR DML STATEMENTS i.e INSERT/DELETE/UPDATE
        public string InsertGR(string ChlnNo, Int32 TruckNoIdno, Int32 YearIdno, Boolean isTBBRate, Int32 itruckcitywise, DataTable dtDetail)
        {
            Int64 intGrIdno = 0;
            string GrIdnos = "";
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
                //{
                    try
                    {
                        TblGrHead objGRHead = null;
                        if (objGRHead == null)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                objGRHead = new TblGrHead();
                                objGRHead.Gr_Date = Convert.ToDateTime(row["GR_Date"]);
                                objGRHead.GR_Agnst = 1;
                                objGRHead.GR_Typ = Convert.ToInt32(row["Gr_TypeIdno"]);
                                objGRHead.DI_NO = "";
                                objGRHead.EGP_NO = "";
                                objGRHead.Gr_No = Convert.ToInt32(row["GR_No"]);
                                objGRHead.Lorry_Idno = TruckNoIdno;
                                objGRHead.Sender_Idno = Convert.ToInt64(row["SenderNameIdno"]);
                                objGRHead.Recivr_Idno = Convert.ToInt64(row["ReciverNameIdno"]);
                                objGRHead.From_City = Convert.ToInt64(row["FromCityIdno"]);
                                objGRHead.To_City = Convert.ToInt64(row["ToCityIdno"]);
                                objGRHead.DelvryPlce_Idno = 1;
                                objGRHead.Agnt_Idno = 0;
                                objGRHead.Remark = "Against Challan Crossing [ No." + ChlnNo + "]";
                                objGRHead.STax_Typ = 1;
                                objGRHead.RcptType_Idno = 1;
                                objGRHead.Inst_No = "";
                                objGRHead.Inst_Dt = Convert.ToDateTime(row["GR_Date"]);
                                objGRHead.Bank_Idno = 0;
                                objGRHead.Gross_Amnt = Convert.ToDouble(row["Amount"]);
                                objGRHead.AgntComisn_Amnt = 0.00;
                                objGRHead.TollTax_Amnt = 0.00;
                                objGRHead.Cartg_Amnt = 0.00;
                                objGRHead.Bilty_Amnt = 0.00;
                                objGRHead.SubTot_Amnt = 0.00;
                                objGRHead.Total_Amnt = Convert.ToDouble(row["Amount"]);
                                objGRHead.Wages_Amnt = 0.00;
                                objGRHead.ServTax_Amnt = 0.00;
                                objGRHead.Surcrg_Amnt = 0.00;
                                objGRHead.PF_Amnt = 0.00;
                                objGRHead.Net_Amnt = Convert.ToDouble(row["Amount"]);
                                objGRHead.RndOff_Amnt = 0.00;
                                objGRHead.Year_Idno = YearIdno;
                                objGRHead.TBB_Rate = isTBBRate;
                                objGRHead.cmb_type = itruckcitywise;
                                objGRHead.GR_Frm = "CC";
                                objGRHead.AgnstRcpt_No = "";
                                objGRHead.Chln_Idno = 0;
                                objGRHead.ChlnCrsng_Idno = 0;
                                objGRHead.Billed = false;
                                db.TblGrHeads.AddObject(objGRHead);
                                db.SaveChanges();
                                intGrIdno = objGRHead.GR_Idno;
                                if (intGrIdno > 0)
                                {
                                    GrIdnos += ","+intGrIdno;
                                    TblGrDetl objGRDetl = new TblGrDetl();
                                    objGRDetl.GrHead_Idno = Convert.ToInt64(intGrIdno);
                                    objGRDetl.Item_Idno = Convert.ToInt32(0);
                                    objGRDetl.Unit_Idno = Convert.ToInt32(0);
                                    objGRDetl.Rate_Type = Convert.ToInt32(1);
                                    objGRDetl.Qty = Convert.ToInt64(row["Qty"]);
                                    objGRDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                    objGRDetl.Item_Rate = 0.00;
                                    objGRDetl.Amount = Convert.ToDouble(row["Amount"]);
                                    objGRDetl.Detail = Convert.ToString(row["Detail"]);
                                    objGRDetl.Shrtg_Limit = Convert.ToDouble(0);
                                    objGRDetl.Shrtg_Rate = Convert.ToDouble(0);
                                    db.TblGrDetls.AddObject(objGRDetl);
                                    db.SaveChanges();

                                }
                            }
                             // tScope.Complete();
                        }
                        else
                        {
                            GrIdnos = "";
                        }
                    }
                    catch (Exception Ex)
                    {
                      //  tScope.Dispose();
                        GrIdnos = "";
                    }
                }
            //}
            return GrIdnos;
        }
        public int DeleteGR(Int64 HeadId)
        {
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TblGrHead qth = db.TblGrHeads.Where(h => h.GR_Idno == HeadId).FirstOrDefault();
                List<TblGrDetl> qtd = db.TblGrDetls.Where(d => d.GrHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.TblGrDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.TblGrHeads.DeleteObject(qth);
                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "GR");
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }

        public Int64 Insert(tblChlnBookHead obj, DataTable Dttemp, string GrHeadsIdno)
        {
            Int64 chlnBookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblChlnBookHead CHead = new tblChlnBookHead();
              
                    tblChlnBookHead RH = db.tblChlnBookHeads.Where(rh => (rh.Chln_No == obj.Chln_No) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno) && rh.Chln_type==2).FirstOrDefault();
                    if (RH != null)
                    {
                        chlnBookId = -1;
                    }
                    else
                    {
                        db.tblChlnBookHeads.AddObject(obj);
                        db.SaveChanges();
                        chlnBookId = obj.Chln_Idno;
                         string[] GrIdnos =GrHeadsIdno.Split(new string[]{","},StringSplitOptions.None);
                        if (Dttemp.Rows.Count > 0)
                        {
                           
                            for(int i=0;i<Dttemp.Rows.Count;i++)
                            {
                                  tblChlnBookDetl objtblChlnBookDetl = new tblChlnBookDetl();
                                objtblChlnBookDetl.GR_Idno = Convert.ToInt32(GrIdnos[i+1]);
                                objtblChlnBookDetl.DelvryPlce_Idno = Convert.ToInt32(Dttemp.Rows[i]["ToCityIdno"]);
                                objtblChlnBookDetl.ChlnBookHead_Idno = chlnBookId;
                                db.tblChlnBookDetls.AddObject(objtblChlnBookDetl);
                                db.SaveChanges();
                            }     
                        }
                        if (Dttemp.Rows.Count > 0)
                        {
                            for(int i=0;i<Dttemp.Rows.Count;i++)
                            {
                                Int32 GrIdno = 0;
                                GrIdno = Convert.ToInt32(GrIdnos[i + 1]);
                                TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                objTblGrHead.Chln_Idno = chlnBookId;
                                db.SaveChanges();
                            }
                          
                        }
                    }
                }
                return chlnBookId;
            }
        public Int64 Update(tblChlnBookHead obj, Int32 ChlnIdno, DataTable Dttemp,string GrHeadsIdno)
        {
            Int64 chlnBoookId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {

                        

                        tblChlnBookHead CH = db.tblChlnBookHeads.Where(rh => rh.Chln_No == obj.Chln_No && (rh.Chln_Idno != ChlnIdno) && (rh.Year_Idno == obj.Year_Idno) && (rh.BaseCity_Idno == obj.BaseCity_Idno) && (rh.Chln_type==2)).FirstOrDefault();
                        if (CH != null)
                        {
                            chlnBoookId = -1;
                        }
                        else
                        {
                            tblChlnBookHead TBH = db.tblChlnBookHeads.Where(rh => rh.Chln_Idno == ChlnIdno).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Chln_No = obj.Chln_No;
                                TBH.Chln_Date = obj.Chln_Date;
                                TBH.BaseCity_Idno = obj.BaseCity_Idno;
                                TBH.DelvryPlc_Idno = obj.DelvryPlc_Idno;
                                TBH.Truck_Idno = obj.Truck_Idno;
                                TBH.Year_Idno = obj.Year_Idno;
                                TBH.Chln_type = obj.Chln_type;
                                TBH.Driver_Idno = obj.Driver_Idno;
                                TBH.Delvry_Instrc = obj.Delvry_Instrc;
                                TBH.Inv_Idno = obj.Inv_Idno;
                                TBH.Gross_Amnt = obj.Gross_Amnt;
                                TBH.Commsn_Amnt = obj.Commsn_Amnt;
                                TBH.Other_Amnt = obj.Other_Amnt;
                                TBH.Net_Amnt = obj.Net_Amnt;
                                TBH.Work_type = obj.Work_type;
                                TBH.Adv_Amnt = obj.Adv_Amnt;
                                TBH.RcptType_Idno = obj.RcptType_Idno;
                                TBH.Inst_No = obj.Inst_No;
                                TBH.Inst_Dt = obj.Inst_Dt;
                                TBH.Bank_Idno = obj.Bank_Idno;
                                db.SaveChanges();
                                chlnBoookId = TBH.Chln_Idno;
                                List<tblChlnBookDetl> ChlnDetl = db.tblChlnBookDetls.Where(rd => rd.ChlnBookHead_Idno == ChlnIdno).ToList();
                                foreach (tblChlnBookDetl rgd in ChlnDetl)
                                {
                                    db.tblChlnBookDetls.DeleteObject(rgd);
                                    db.SaveChanges();
                                }
                                //foreach (tblChlnBookDetl rgd in ChlnDetl)
                                //{
                                //    rgd.ChlnBookHead_Idno = chlnBoookId;
                                //    db.tblChlnBookDetls.AddObject(rgd);
                                //    db.SaveChanges();
                                //}

                                string[] GrIdnos = GrHeadsIdno.Split(new string[] { "," }, StringSplitOptions.None);
                                if (Dttemp.Rows.Count > 0)
                                {

                                    for (int i = 0; i < Dttemp.Rows.Count; i++)
                                    {
                                        tblChlnBookDetl objtblChlnBookDetl = new tblChlnBookDetl();
                                        objtblChlnBookDetl.GR_Idno = Convert.ToInt32(GrIdnos[i + 1]);
                                        objtblChlnBookDetl.DelvryPlce_Idno = Convert.ToInt32(Dttemp.Rows[i]["ToCityIdno"]);
                                        objtblChlnBookDetl.ChlnBookHead_Idno = ChlnIdno;
                                        db.tblChlnBookDetls.AddObject(objtblChlnBookDetl);
                                        db.SaveChanges();
                                    }
                                }
                                if (Dttemp.Rows.Count > 0)
                                {
                                    for (int i = 0; i < Dttemp.Rows.Count; i++)
                                    {
                                        Int32 GrIdno = 0;
                                        GrIdno = Convert.ToInt32(GrIdnos[i + 1]);
                                        TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                        objTblGrHead.Chln_Idno = ChlnIdno;
                                        db.SaveChanges();
                                    }

                                }
                                dbTran.Commit();
                            }
                        }



                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return chlnBoookId;
        }
    
       #endregion
    
        #region FUNCTIONS
        public Int64 CheckInvoiceDetails(Int64 GRHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 InvoiceNo = 0;
                InvoiceNo = Convert.ToInt64((from a in db.TblGrHeads
                                             join b in db.tblInvGenDetls on a.GR_Idno equals b.GR_Idno
                                             join c in db.tblInvGenHeads on b.InvGenHead_Idno equals c.Inv_Idno
                                             where a.GR_Idno == GRHeadIdno && a.Chln_Idno > 0 && a.Billed == true
                                             select c.Inv_No).FirstOrDefault());
                return InvoiceNo;
            }
        }
        public Int64 CheckChallanDetails(Int64 GRHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 ChallanNo = 0;
                ChallanNo = Convert.ToInt64((from a in db.TblGrHeads
                                             join b in db.tblChlnBookHeads on a.Chln_Idno equals b.Chln_Idno
                                             where a.GR_Idno == GRHeadIdno && a.Chln_Idno > 0
                                             select b.Chln_No).FirstOrDefault());
                return ChallanNo;
            }
        }
        public IList CheckDuplicateGrNo(Int64 intGrNo, Int32 FromCityIdno, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from m in db.TblGrHeads
                           where m.Gr_No == intGrNo && m.Year_Idno == intYearIdno && m.GR_Frm == "BK" && m.From_City == FromCityIdno
                           select new
                           {
                               m.Gr_No,
                           }).ToList();
                return lst;
            }
        }
        public Int64 MaxNo( Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(Chln_No),0) + 1 AS MAXID FROM tblChlnBookHead WHERE Chln_type=2 AND BaseCity_Idno='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        //public Int32 GetChallanNo(Int32 YearIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        Int32 MaxNo = 0;
        //        MaxNo = Convert.ToInt32((from obj in db.tblChlnBookHeads where (obj.Chln_type == 2) && (obj.Year_Idno == YearIdno) select obj.Chln_No).Max());
        //        MaxNo = MaxNo + 1;
        //        return MaxNo;
        //    }
        //}

        public LorryMast selectOwnerName(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                LorryMast lst = ((from DR in db.LorryMasts where DR.Lorry_Idno == LorryIdno select DR).FirstOrDefault());
                return lst;
            }
        }
        #endregion

        #region SELECT STATEMENTS
        //public DataTable DsTrAcnt(string con)
        //{
        //    sqlSTR = string.Empty;
        //    sqlSTR = @"SELECT Acnt_Idno AS 'TransportAccountID' FROM ACNTMAST WHERE ACNT_NAME='Transport Charges' AND INTERNAL=1";
        //    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
        //    DataTable dt = new DataTable();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    return dt;
        //}
        public tblUserPref selectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref Objtbl = (from obj in db.tblUserPrefs select obj).FirstOrDefault();
                return Objtbl;
            }
        }
        public DataTable DtAcntDS(string con)
        {
            //using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required))
            //{
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno FROM tblAcntLink";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                // tScope.Complete();
            }
            return dt;
            /// }
        }
        public Int64 SelectGRnoByGRIdno(Int64 GRIdno)
        {
            Int64 GRno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                GRno = Convert.ToInt64((from gr in db.TblGrHeads where gr.GR_Idno == GRIdno select gr.Gr_No).FirstOrDefault());
                return GRno;
            }
        }
        public DataTable SelectByGRIdDetl(Int64 GRIdno, string con)
        {
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT CONVERT(NVARCHAR(50), RC.gr_idno) AS gr_idno,RC.RcptType_Idno,A.ACNT_NAME,RC.gr_no,RC.INST_NO,
                        RC.INST_DT,RC.Bank_Idno,A.ACNT_TYPE,A.Acnt_Name
                        FROM tblgrhead RC INNER JOIN ACNTmast A ON  RC.sender_idno=A.Acnt_Idno
                        WHERE RC.gr_idno=" + GRIdno;
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public bool SelectTBBRate()
        {
            Int32 ITBBRate = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ITBBRate = Convert.ToInt32((from UP in db.tblUserPrefs select UP.TBB_Rate).FirstOrDefault());
                if (ITBBRate == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public double SelectServTaxExmpt(Int32 SenderIdno)
        {
            Int32 ServTaxExmpt = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ServTaxExmpt = Convert.ToInt32((from UP in db.AcntMasts where UP.Acnt_Idno == SenderIdno select UP.ServTax_Exmpt).FirstOrDefault());
                return ServTaxExmpt;
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
        public double SelectWghtShrtgRate(Int32 ItemIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectWghtShrtgLimit(Int32 ItemIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgRate(Int32 ItemIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgLimit(Int32 ItemIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemRateForTBB(Int32 ItemIdno, Int32 TocityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBB(Int32 ItemIdno, Int32 TocityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectItemRate(Int64 ItemIdno, Int64 TocityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRate(Int64 ItemIdno, Int64 TocityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectAgntRate(Int32 AgentIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                double AgntRate = Convert.ToDouble((from AM in db.AcntMasts where AM.Acnt_Idno == AgentIdno select AM.Agnt_Commson).FirstOrDefault());
                return AgntRate;
            }
        }
        public IList SelectGR(int GrNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, Int32 yearidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.TblGrHeads
                           //join grd in db.TblGrDetls on hd.GR_Idno equals grd.GrHead_Idno
                           join cifrom in db.tblCityMasters on hd.From_City equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.To_City equals cito.City_Idno
                           join cidl in db.tblCityMasters on hd.DelvryPlce_Idno equals cidl.City_Idno
                           join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           select new
                           {
                               hd.DelvryPlce_Idno,
                               hd.From_City,
                               hd.To_City,
                               hd.Gr_Date,
                               hd.GR_Idno,
                               hd.Gr_No,
                               hd.Sender_Idno,
                               hd.Remark,
                               GR_Typ = ((hd.GR_Typ == 1) ? "Paid GR" : (hd.GR_Typ == 2) ? "TBB GR" : "To Pay GR"),
                               //grd.Qty,
                               //grd.Tot_Weght,
                               //grd.Amount,
                               hd.Recivr_Idno,
                               hd.Gross_Amnt,
                               hd.Net_Amnt,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityDely = cidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               Receiver = acnts.Acnt_Name,
                               Year_Idno = hd.Year_Idno

                           }).ToList();
                if (GrNo > 0)
                {
                    lst = lst.Where(l => l.Gr_No == GrNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.From_City == cityfrom).ToList();
                }
                if (cityto > 0)
                {
                    lst = lst.Where(l => l.To_City == cityto).ToList();
                }
                if (citydely > 0)
                {
                    lst = lst.Where(l => l.DelvryPlce_Idno == citydely).ToList();
                }
                if (sender > 0)
                {
                    lst = lst.Where(l => l.Sender_Idno == sender).ToList();
                }

                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                return lst;
            }
        }
        public TblGrHead SelectTblGRHead(int GRIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TblGrHeads.Where(tgh => (tgh.GR_Idno == GRIdno)).FirstOrDefault();
            }
        }
        public IList SelectGRDetail(int GrIDNO)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GD in db.TblGrDetls
                           join IM in db.ItemMasts on GD.Item_Idno equals IM.Item_Idno
                           join UM in db.UOMMasts on GD.Unit_Idno equals UM.UOM_Idno
                           where GD.GrHead_Idno == GrIDNO
                           select new
                           {
                               GD.Qty,
                               GD.Tot_Weght,
                               GD.Amount,
                               GD.GrHead_Idno,
                               GD.Item_Idno,
                               GD.Unit_Idno,
                               Rate_Type = (GD.Rate_Type == 1 ? "Rate" : "Weight"),
                               RateType_Idno = GD.Rate_Type,
                               GD.Item_Rate,
                               GD.Detail,
                               IM.Item_Name,
                               UM.UOM_Name
                           }).ToList();
                return lst;
            }
        }
        //public IList SelectGRPOPULATE(int GrIDNO, Int32 yearidno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from GH in db.TblGrHeads
        //                   join GD in db.TblGrDetls on GH.GR_Idno equals GD.GrHead_Idno
        //                   join cifrom in db.tblCityMasters on GH.From_City equals cifrom.City_Idno
        //                   join cito in db.tblCityMasters on GH.To_City equals cito.City_Idno
        //                   join cidl in db.tblCityMasters on GH.DelvryPlce_Idno equals cidl.City_Idno
        //                   join acnts in db.AcntMasts on GH.Sender_Idno equals acnts.Acnt_Idno
        //                   where GH.GR_Idno == GrIDNO && GH.Year_Idno == yearidno
        //                   select new
        //                   {
        //                       GH.DelvryPlce_Idno,
        //                       GH.From_City,
        //                       GH.To_City,
        //                       GH.Gr_Date,
        //                       GH.GR_Idno,
        //                       GH.Gr_No,
        //                       GH.Sender_Idno,
        //                       GH.Remark,
        //                       GH.GR_Typ,
        //                       GD.Qty,
        //                       GD.Tot_Weght,
        //                       GD.Amount,
        //                       GH.GR_Agnst,
        //                       GH.DI_NO,
        //                       GH.EGP_NO,
        //                       GH.Lorry_Idno,
        //                       GH.Recivr_Idno,
        //                       GH.Agnt_Idno,
        //                       GH.STax_Typ,
        //                       GH.RcptType_Idno,
        //                       GH.Inst_No,
        //                       GH.Inst_Dt,
        //                       GH.Bank_Idno,
        //                       GD.GrHead_Idno,
        //                       GD.Item_Idno,
        //                       GD.Unit_Idno,
        //                       GD.Rate_Type,
        //                       GD.Item_Rate,
        //                       GD.Detail,
        //                       GH.AgntComisn_Amnt,
        //                       GH.Bilty_Amnt,
        //                       GH.Gross_Amnt,
        //                       GH.Cartg_Amnt,
        //                       GH.Total_Amnt,
        //                       GH.Surcrg_Amnt,
        //                       GH.Wages_Amnt,
        //                       GH.PF_Amnt,
        //                       GH.TollTax_Amnt,
        //                       GH.ServTax_Amnt,
        //                       GH.Net_Amnt,
        //                       GH.SubTot_Amnt,
        //                       CityTo = cito.City_Name,
        //                       CityFrom = cifrom.City_Name,
        //                       CityDely = cidl.City_Name,
        //                       Sender = acnts.Acnt_Name,
        //                       Year_Idno = GH.Year_Idno

        //                   }).ToList();

        //        if (yearidno > 0)
        //        {
        //            lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
        //        }
        //        return lst;
        //    }
        //}
        public DataTable selectGrDetails(string Action, DateTime dtfromDate, DateTime dtToDate, Int64 RecvrIdno, Int64 CityIdno, Int64 DelvryPlceIdno, Int64 YearIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@GrDate", dtfromDate);
            objSqlPara[2] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[3] = new SqlParameter("@RecivrIdno", RecvrIdno);
            objSqlPara[4] = new SqlParameter("@ToCity", CityIdno);
            objSqlPara[5] = new SqlParameter("@DelvryPlceIdno", DelvryPlceIdno);
            objSqlPara[6] = new SqlParameter("@YearIdno", YearIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrep", objSqlPara);
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
        public DataTable SelectRECPTGrDetails(string con, Int64 iYearId, string AllItmIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectRcptDetailInGR");
            objSqlPara[1] = new SqlParameter("@RcptHeadIdno", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrep", objSqlPara);
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
        public Int32 CheckBilled(Int64 Id, string con)
        {
            Int32 value = 0;

            SqlParameter[] objSqlParameter = new SqlParameter[2];
            objSqlParameter[0] = new SqlParameter("@Action", "SelectBilled");
            objSqlParameter[1] = new SqlParameter("@Id", Id);
            value = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spChlnBookCrosng", objSqlParameter));
            return value;

        }
        public tblChlnBookHead selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId).FirstOrDefault();
            }
        }
        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetl");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);


            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnBookCrosng", objSqlPara);
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
        public Int64 CountALL()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from CH in db.tblChlnBookHeads
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno

                           join LM in db.LorryMasts on CH.Truck_Idno equals LM.Lorry_Idno
                           where CH.Chln_type == 2 select CH.Chln_Idno).Count();
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
                           where CH.Chln_type == 2
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
        public int Delete(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                tblChlnBookHead CH = db.tblChlnBookHeads.Where(h => h.Chln_Idno == HeadId).FirstOrDefault();
                List<tblChlnBookDetl> CD = db.tblChlnBookDetls.Where(d => d.ChlnBookHead_Idno == HeadId).ToList();
                if (CH != null)
                {
                    foreach (var d in CD)
                    {
                        TblGrHead objTblGrHead = (from obj in db.TblGrHeads where obj.GR_Idno == d.GR_Idno select obj).FirstOrDefault();
                        objTblGrHead.Chln_Idno = 0;
                        db.SaveChanges();
                    }

                    foreach (var d in CD)
                    {
                        db.tblChlnBookDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblChlnBookHeads.DeleteObject(CH);

                 TblGrHead GrHEad = db.TblGrHeads.Where(rd => rd.Chln_Idno == Convert.ToInt32(HeadId)).FirstOrDefault();
                        List<TblGrDetl> GrDetl = db.TblGrDetls.Where(rd => rd.GrHead_Idno == GrHEad.GR_Idno).ToList();
                        foreach (TblGrDetl rgd in GrDetl)
                        {
                            db.TblGrDetls.DeleteObject(rgd);
                            db.SaveChanges();
                        }

                        db.TblGrHeads.DeleteObject(GrHEad);
                        db.SaveChanges();
                

                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "CB");
                    db.SaveChanges();
                    if (intValue > 0)
                    {
                        value = 1;
                    }

                }
            }
            return value;
        }

        public Int64 TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblChlnBookHeads
                           join cifrom in db.tblCityMasters on CH.BaseCity_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on CH.BaseCity_Idno equals cito.City_Idno

                           join LM in db.LorryMasts on CH.Truck_Idno equals LM.Lorry_Idno
                           where CH.Chln_type == 2
                           select new
                           {

                               CH.Chln_No,

                           }).Count();
                return lst;
            }
        }
        #endregion

    }
}
