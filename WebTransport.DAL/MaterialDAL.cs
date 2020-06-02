using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class MaterialDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion
        #region FOR DML STATEMENTS i.e INSERT/DELETE/UPDATE
        public Int64 InsertMat(DateTime? dtMat_Date, Int32 IAgainst, Int64 intMat_No, Int32 intLoc_Idno, Int32 TruckNoIdno,string km, Double DNetAmnt, Int32 YearIdno, DataTable dtDetail, Int32 intIssueTo, string strRemark, Int64 DriverIdno,Int64 intPrtyIdno,string strOwnerName,Int64 IssueType)
        {

            Int64 intMateIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    MatIssHead objMatHead = db.MatIssHeads.Where(rh => (rh.MatIss_No == intMat_No) && (rh.Loc_Idno == intLoc_Idno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();

                    if (objMatHead == null)
                    {
                        objMatHead = new MatIssHead();
                        objMatHead.MatIss_Date = dtMat_Date;
                        objMatHead.MatIss_Typ = IAgainst;
                        objMatHead.MatIss_No = intMat_No;
                        objMatHead.Loc_Idno = intLoc_Idno;
                        objMatHead.Truck_Idno = TruckNoIdno;
                        objMatHead.Fitment_km = km;
                        objMatHead.Net_Amnt = DNetAmnt;
                        objMatHead.Year_Idno = YearIdno;
                        objMatHead.Issue_To = intIssueTo;
                        objMatHead.ReMark = strRemark;
                        objMatHead.Date_Added = System.DateTime.Now;
                        objMatHead.Mat_Frm = "BK";
                        objMatHead.Driver_Idno = DriverIdno;
                        objMatHead.Prty_Idno = intPrtyIdno;
                        objMatHead.Issue_Type = IssueType;
                        objMatHead.Owner_Name = strOwnerName;
                        db.MatIssHeads.AddObject(objMatHead);
                        db.SaveChanges();
                        intMateIdno = objMatHead.MatIss_Idno;
                        if (intMateIdno > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                MatIssDetl objMatDetl = new MatIssDetl();
                                objMatDetl.MatIssHead_Idno = Convert.ToInt64(intMateIdno);
                                objMatDetl.Iteam_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objMatDetl.Item_Qty = Convert.ToInt64(row["Quantity"]);
                                objMatDetl.Item_Weght = Convert.ToDouble(row["Weight"]);
                                objMatDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                objMatDetl.Item_Amnt = Convert.ToDouble(row["Amount"]);
                                objMatDetl.Remark = Convert.ToString(row["Detail"]);
                                objMatDetl.Serial_Idno = Convert.ToInt64(row["SerialId"]);
                                objMatDetl.Serial_Number = Convert.ToString(row["SerialNo"]);
                                objMatDetl.NSD = Convert.ToString(row["NSD"]);
                                objMatDetl.PSI = Convert.ToString(row["PSI"]);
                                objMatDetl.TType = Convert.ToInt32(row["TType"]);
                                objMatDetl.RSerial_Idno = Convert.ToInt64(row["RSerialId"]);
                                objMatDetl.RSerial_No = Convert.ToString(row["RSerialNo"]);
                                objMatDetl.RNSD = Convert.ToString(row["RNSD"]);
                                objMatDetl.RPSI = Convert.ToString(row["RPSI"]);
                                objMatDetl.RTType = Convert.ToInt32(row["RTType"]);
                                objMatDetl.TyresizeIdno = Convert.ToInt32(row["Tyresize_Idno"]);
                                objMatDetl.Tyreposition_Idno = Convert.ToInt32(row["Tyreposition_Idno"]);
                                objMatDetl.Align = string.IsNullOrEmpty(Convert.ToString(row["Align"])) ? false : Convert.ToBoolean(row["Align"]);
                                if ((Convert.ToString(row["AlignDate"]) != ""))
                                {   
                                    objMatDetl.AlignDate = Convert.ToDateTime(row["AlignDate"]);
                                    
                                }
                                if ((Convert.ToString(row["PrevAlignDate"]) != ""))
                                { objMatDetl.Prev_AlignDate = Convert.ToDateTime(row["PrevAlignDate"]); }
                                objMatDetl.RPrice = Convert.ToDouble(row["RPrice"]);

                                db.MatIssDetls.AddObject(objMatDetl);
                                db.SaveChanges();

                                Int64 SrId = Convert.ToInt64(row["SerialId"]);
                                List<Stckdetl> StckDetl = db.Stckdetls.Where(rd => rd.SerlDetl_id == SrId).ToList();
                                foreach (var d in StckDetl)
                                {
                                    d.MtrlIssue_Idno = intMateIdno;
                                    d.Is_Issued = true;
                                    db.SaveChanges();
                                }

                                Int64 RSrId = Convert.ToInt64(row["RSerialId"]);
                                List<Stckdetl> RStckDetl = db.Stckdetls.Where(rd => rd.SerlDetl_id == RSrId).ToList();
                                foreach (var d in RStckDetl)
                                {
                                    d.MtrlIssue_Idno = 0;
                                    d.Is_Issued = false;
                                    db.SaveChanges();
                                }
                                MatIssIdDetl MtIdDetl = new MatIssIdDetl();
                                MtIdDetl.MtrlDetlIdno = objMatDetl.MatIssDet_Idno;
                                MtIdDetl.DocIdno = intMateIdno;
                                MtIdDetl.StckIdno = SrId;
                                MtIdDetl.DocType = "MT";
                                db.MatIssIdDetls.AddObject(MtIdDetl);
                                db.SaveChanges();
                            }
                            //    tScope.Complete();
                        }

                    }
                    else
                    {
                        intMateIdno = -1;
                    }

                }
                catch (Exception Ex)
                {

                    intMateIdno = 0;
                }

            }
            return intMateIdno;
        }

        public Int64 MatUpdate(Int64 intGRIdno, DateTime? dtMat_Date, Int32 IAgainst, Int64 intMat_No, Int32 intLoc_Idno, Int32 TruckNoIdno, string km, Double DNetAmnt, Int32 YearIdno, DataTable dtDetail, Int32 intIssueTo, string strRemark, Int64 DriverIdno, Int64 intPrtyIdno, string strOwnerName,Int64 IssueType)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    MatIssHead objGRHead = db.MatIssHeads.Where(rh => (rh.MatIss_Idno != intGRIdno) && (rh.MatIss_No == intMat_No) && (rh.Loc_Idno == intLoc_Idno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        MatIssHead objMatIssHead1 = db.MatIssHeads.Where(rh => rh.MatIss_Idno == intGRIdno).FirstOrDefault();
                        if (objMatIssHead1 != null)
                        {
                            objMatIssHead1.MatIss_Date = dtMat_Date;

                            objMatIssHead1.MatIss_Typ = IAgainst;
                            objMatIssHead1.MatIss_No = intMat_No;
                            objMatIssHead1.Loc_Idno = intLoc_Idno;
                            objMatIssHead1.Truck_Idno = TruckNoIdno;
                            objMatIssHead1.Fitment_km = km;
                            objMatIssHead1.Net_Amnt = DNetAmnt;
                            objMatIssHead1.Year_Idno = YearIdno;
                            objMatIssHead1.Issue_To = intIssueTo;
                            objMatIssHead1.ReMark = strRemark;
                            objMatIssHead1.Date_Modified = System.DateTime.Now;
                            objMatIssHead1.Mat_Frm = "BK";
                            objMatIssHead1.Driver_Idno = DriverIdno;
                            objMatIssHead1.Prty_Idno = intPrtyIdno;
                            objMatIssHead1.Issue_Type = IssueType;
                            objMatIssHead1.Owner_Name = strOwnerName;
                            db.SaveChanges();
                            intGrIdno = objMatIssHead1.MatIss_Idno;

                            if (intGrIdno > 0)
                            {
                                List<MatIssDetl> lstGrDetl = db.MatIssDetls.Where(obj => obj.MatIssHead_Idno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (MatIssDetl obj in lstGrDetl)
                                    {
                                        db.MatIssDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                List<MatIssIdDetl> lstIDDetl = db.MatIssIdDetls.Where(obj => obj.DocIdno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (MatIssIdDetl obj in lstIDDetl)
                                    {
                                        List<Stckdetl> StckDetl1 = db.Stckdetls.Where(rd => rd.SerlDetl_id == obj.StckIdno).ToList();
                                        foreach (var d in StckDetl1)
                                        {
                                            d.MtrlIssue_Idno = 0;
                                            d.Is_Issued = true;
                                            db.SaveChanges();
                                        }
                                        db.MatIssIdDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    MatIssDetl objMatIssDetl = new MatIssDetl();
                                    objMatIssDetl.MatIssHead_Idno = Convert.ToInt64(intGrIdno);
                                    objMatIssDetl.Iteam_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objMatIssDetl.Item_Qty = Convert.ToInt64(row["Quantity"]);
                                    objMatIssDetl.Item_Weght = Convert.ToDouble(row["Weight"]);
                                    objMatIssDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objMatIssDetl.Item_Amnt = Convert.ToDouble(row["Amount"]);
                                    objMatIssDetl.Remark = Convert.ToString(row["Detail"]);
                                    objMatIssDetl.Serial_Idno = Convert.ToInt64(row["SerialId"]);
                                    objMatIssDetl.Serial_Number = Convert.ToString(row["SerialNo"]);
                                    objMatIssDetl.NSD = Convert.ToString(row["NSD"]);
                                    objMatIssDetl.PSI = Convert.ToString(row["PSI"]);
                                    objMatIssDetl.TType = Convert.ToInt32(row["TType"]);
                                    objMatIssDetl.RSerial_Idno = Convert.ToInt64(row["RSerialId"]);
                                    objMatIssDetl.RSerial_No = Convert.ToString(row["RSerialNo"]);
                                    objMatIssDetl.RNSD = Convert.ToString(row["RNSD"]);
                                    objMatIssDetl.RPSI = Convert.ToString(row["RPSI"]);
                                    objMatIssDetl.RTType = Convert.ToInt32(row["RTType"]);
                                    objMatIssDetl.TyresizeIdno = Convert.ToInt32(row["Tyresize_Idno"]);
                                    objMatIssDetl.Tyreposition_Idno = Convert.ToInt32(row["Tyreposition_Idno"]);
                                    objMatIssDetl.Align = string.IsNullOrEmpty(Convert.ToString(row["Align"])) ? false : Convert.ToBoolean(row["Align"]);
                                    if ((Convert.ToString(row["AlignDate"]) != ""))
                                    {
                                        objMatIssDetl.AlignDate = Convert.ToDateTime(row["AlignDate"]);

                                    }
                                    objMatIssDetl.RPrice = Convert.ToDouble(row["RPrice"]);

                                    db.MatIssDetls.AddObject(objMatIssDetl);
                                    db.SaveChanges();

                                    Int64 SrId = Convert.ToInt64(row["SerialId"]);
                                    List<Stckdetl> StckDetl1 = db.Stckdetls.Where(rd => rd.SerlDetl_id == SrId).ToList();
                                    foreach (var d in StckDetl1)
                                    {
                                        d.MtrlIssue_Idno = intGrIdno;
                                        d.Is_Issued = true;
                                        db.SaveChanges();
                                    }

                                    MatIssIdDetl MtIdDetl = new MatIssIdDetl();
                                    MtIdDetl.MtrlDetlIdno = objMatIssDetl.MatIssDet_Idno;
                                    MtIdDetl.DocIdno = intGrIdno;
                                    MtIdDetl.StckIdno = SrId;
                                    MtIdDetl.DocType = "MT";
                                    db.MatIssIdDetls.AddObject(MtIdDetl);
                                    db.SaveChanges();
                                }

                            }
                        }
                    }
                    else
                    {
                        intGrIdno = -1;
                    }
                }
                catch (Exception Ex)
                {

                    intGrIdno = 0;
                }

            }
            return intGrIdno;
        }
        public int Delete(Int64 HeadId)
        {
            //clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                MatIssHead qth = db.MatIssHeads.Where(h => h.MatIss_Idno == HeadId).FirstOrDefault();
                List<MatIssDetl> qtd = db.MatIssDetls.Where(d => d.MatIssDet_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        if (d.Serial_Idno > 0)
                        {
                            Stckdetl Sd = db.Stckdetls.Where(r => r.SerlDetl_id == d.Serial_Idno).FirstOrDefault();
                            Sd.MtrlIssue_Idno = 0;
                            Sd.Is_Issued = false;

                            db.MatIssDetls.DeleteObject(d);
                            db.SaveChanges();
                        }
                    }
                    db.MatIssHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }

                List<MatIssIdDetl> MtId = db.MatIssIdDetls.Where(d => d.DocIdno == HeadId).ToList();
                if (MtId != null)
                {
                    foreach (var d in MtId)
                    {
                        if (d.StckIdno > 0)
                        {
                            Stckdetl Sd = db.Stckdetls.Where(r => r.SerlDetl_id == d.StckIdno).FirstOrDefault();
                            Sd.MtrlIssue_Idno = 0;
                            Sd.Is_Issued = false;
                            db.MatIssIdDetls.DeleteObject(d);
                            db.SaveChanges();
                        }
                    }
                }
            }
            return value;
        }
        #endregion
        public Int64 MaxNo(string MAtfrom, Int32 yearId, Int32 Location, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(MatIss_No),0) + 1 AS MAXID FROM MatIssHead WHERE Mat_Frm='" + MAtfrom + "' AND Loc_Idno=" + Location;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public IList SelectEmployee()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from AR in db.AcntMasts where AR.Acnt_Type == 9 select new { Acnt_Name = AR.Acnt_Name, Acnt_Idno = AR.Acnt_Idno }).ToList();
            }
        }
        public IList CheckDuplicateGrNo(Int64 intMatNo, Int64 LoctionIdno, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from m in db.MatIssHeads
                           where m.MatIss_No == intMatNo && m.Year_Idno == intYearIdno && m.Mat_Frm == "BK" && m.Loc_Idno == LoctionIdno
                           select new
                           {
                               m.MatIss_No
                           }).ToList();
                return lst;
            }
        }
        public tblItemMastPur GetItemDetails(Int64 Item_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblItemMastPurs.Where(r => r.Item_Idno == Item_Idno).Single();
            }
        }
        public IList SelectMatrial(DateTime? dtfrom, DateTime? dtto, Int64 location, Int64 truckno, Int32 yearidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mth in db.MatIssHeads
                           join cito in db.tblCityMasters on mth.Loc_Idno equals cito.City_Idno 
                           join lm in db.LorryMasts on mth.Truck_Idno equals lm.Lorry_Idno into temp
                           from ctemp in temp.DefaultIfEmpty()
                           select new
                           {
                               mth.MatIss_Idno,
                               mth.MatIss_No,
                               mth.MatIss_Date,
                               MatIss_Typ = ((mth.MatIss_Typ == 1) ? "Issue" : (mth.MatIss_Typ == 2) ? "ReIssue" : ""),
                               mth.Loc_Idno,
                               mth.Truck_Idno,
                               mth.Issue_To,
                               mth.Net_Amnt,
                               mth.Mat_Frm,
                               mth.ReMark,
                               mth.Year_Idno,
                               CityTo = cito.City_Name,
                               ctemp.Lorry_No
                           }).ToList();

                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.MatIss_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.MatIss_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (location > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == location).ToList();
                }
                if (truckno > 0)
                {
                    lst = lst.Where(l => l.Truck_Idno == truckno).ToList();
                }
                return lst;
            }
        }
        public MatIssHead SelectMatHead(Int64 MatIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.MatIssHeads.Where(tgh => (tgh.MatIss_Idno == MatIdno)).FirstOrDefault();
            }
        }
        public IList SelectMatDetail(int matIDNO)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GD in db.MatIssDetls
                           join IM in db.tblItemMastPurs on GD.Iteam_Idno equals IM.Item_Idno
                           join TS in db.TyreSizeMasters on GD.TyresizeIdno equals TS.TyreSize_Idno into temptyre
                           join TP in db.tblPositionMasts on GD.Tyreposition_Idno equals TP.Position_id into temptyreP
                           from TS in temptyre.DefaultIfEmpty()
                           from TP in temptyreP.DefaultIfEmpty()
                           where GD.MatIssHead_Idno == matIDNO
                           select new
                           {
                               GD.Item_Qty,
                               GD.Item_Weght,
                               GD.Item_Amnt,
                               GD.MatIssHead_Idno,
                               GD.Iteam_Idno,
                               GD.Item_Rate,
                               GD.Remark,
                               IM.Item_Name,
                               SerialNo = (from n in db.Stckdetls join MT in db.MatIssIdDetls on GD.MatIssHead_Idno equals MT.DocIdno where n.SerlDetl_id == MT.StckIdno && MT.MtrlDetlIdno == GD.MatIssDet_Idno select n.SerialNo).FirstOrDefault(),
                               SerialId = (from n in db.Stckdetls join MT in db.MatIssIdDetls on GD.MatIssHead_Idno equals MT.DocIdno where n.SerlDetl_id == MT.StckIdno && MT.MtrlDetlIdno == GD.MatIssDet_Idno select n.SerlDetl_id).FirstOrDefault(),
                               NSD = GD.NSD,
                               PSI = GD.PSI,
                               TType = GD.TType,
                               RSerial_No = GD.RSerial_No,
                               RSerial_Idno = GD.RSerial_Idno,
                               RNSD = GD.RNSD,
                               RPSI = GD.RPSI,
                               RTType = GD.RTType,
                               PrevAlignDate = GD.Prev_AlignDate,
                               Align = GD.Align,
                               AlignDate = GD.AlignDate,
                               RPrice = GD.RPrice,
                               GD.TyresizeIdno,
                               TS.TyreSize,
                               TP.Position_name,GD.Tyreposition_Idno
                           }).ToList();
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
        public List<AcntMast> selectOwnerDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from DR in db.AcntMasts where DR.Acnt_Type == 9 orderby DR.Acnt_Name ascending select DR).ToList();
                return lst;
            }
        }

        public dynamic CheckForDelete(Int64 MIssueIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from SD in db.Stckdetls where SD.MtrlIssue_Idno == MIssueIdno && SD.Billed == true select SD).FirstOrDefault();
            }
        }
        public dynamic GetMatrialIssueNo(Int64 MIssueIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from SD in db.Stckdetls
                        join Mt in db.MatIssHeads on SD.MtrlIssue_Idno equals Mt.MatIss_Idno
                        where SD.MtrlIssue_Idno == MIssueIdno && SD.Billed == true select Mt.MatIss_No).FirstOrDefault();
            }
        }

        public dynamic DriverName(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.LorryMasts where LM.Lorry_Idno == TruckIdno select LM.Driver_Idno).FirstOrDefault();
            }
        }

        public dynamic OwnerName(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.LorryMasts where LM.Lorry_Idno == TruckIdno select LM.Owner_Name).FirstOrDefault();
            }
        }
        public dynamic LorryType(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.LorryMasts where LM.Lorry_Idno == TruckIdno select LM.Lorry_Type).FirstOrDefault();
            }
        }
        public dynamic PartyName(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.LorryMasts where LM.Lorry_Idno == TruckIdno select LM.Prty_Idno).FirstOrDefault();
            }
        }
        public dynamic PrvReciver(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from N in db.MatIssHeads where N.Truck_Idno == TruckIdno orderby N.MatIss_Idno descending select N.Driver_Idno).FirstOrDefault(); ;
            }
        }
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                // only own lorry shows
                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Lorry_Type == 0 && obj.Status == true
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList PrvItem(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objA = (from M in db.MatIssHeads
                            join D in db.MatIssDetls on M.MatIss_Idno equals D.MatIssHead_Idno
                            join I in db.tblItemMastPurs on D.Iteam_Idno equals I.Item_Idno
                            where M.Truck_Idno == TruckIdno
                            orderby M.MatIss_Date ascending
                            select new
                            {
                                Date = M.MatIss_Date,
                                Item = I.Item_Name
                            }).ToList();
                return objA;
            }
        }
        public List<tblItemMastPur> BindItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               where obj.ItemType != 3
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public Int64 CountAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 c = 0;
                c = (from co in db.MatIssHeads select co).Count();
                return c;
            }
        }
        public List<tblItemMastPur> BindActiveItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               where obj.Status == true && obj.ItemType != 3
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public IList SelectMatrialIssueReport(DateTime? dtfrom, DateTime? dtto, Int64 location, Int64 truckno, Int32 yearidno, Int64 DriverIdno, Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mth in db.MatIssHeads
                           join mdtl in db.MatIssDetls on mth.MatIss_Idno equals mdtl.MatIssHead_Idno
                           join Itm in db.tblItemMastPurs on mdtl.Iteam_Idno equals Itm.Item_Idno
                           join cito in db.tblCityMasters on mth.Loc_Idno equals cito.City_Idno
                           join Acm in db.AcntMasts on mth.Driver_Idno equals Acm.Acnt_Idno into AMast
                           from Amtemp in AMast.DefaultIfEmpty()
                           join lm in db.LorryMasts on mth.Truck_Idno equals lm.Lorry_Idno into temp
                           from ctemp in temp.DefaultIfEmpty()
                           select new
                           {
                               mdtl.Iteam_Idno,
                               Itm.Item_Name,
                               Qty=mdtl.Item_Qty,
                               mth.Driver_Idno,
                               Amtemp.Acnt_Name,
                               mth.MatIss_Idno,
                               mth.MatIss_No,
                               mth.MatIss_Date,
                               MatIss_Typ = ((mth.MatIss_Typ == 1) ? "Issue" : (mth.MatIss_Typ == 2) ? "ReIssue" : ""),
                               mth.Loc_Idno,
                               mth.Truck_Idno,
                               mth.Issue_To,
                               mth.Net_Amnt,
                               mdtl.Align,
                               mdtl.Prev_AlignDate,
                               mdtl.AlignDate,
                               mdtl.Item_Amnt,
                               mth.Mat_Frm,
                               mth.ReMark,
                               mth.Year_Idno,
                               CityTo = cito.City_Name,
                               ctemp.Lorry_No
                           }).ToList();

                if (dtfrom != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.MatIss_Date).Date >= Convert.ToDateTime(dtfrom).Date select l).ToList();
                }
                if (dtto != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.MatIss_Date).Date <= Convert.ToDateTime(dtto).Date select l).ToList();
                }

                if (location > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == location).ToList();
                }
                if (truckno > 0)
                {
                    lst = (from I in lst where I.Truck_Idno == truckno select I).ToList();
                }
                if (DriverIdno > 0)
                {
                    lst = lst.Where(l => l.Driver_Idno == DriverIdno).ToList();
                }
                if (ItemIdno > 0)
                {
                    lst = lst.Where(l => l.Iteam_Idno == ItemIdno).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                return lst;
            }
        }
        public IList SelectMatrialIssueReportSummary(DateTime? dtfrom, DateTime? dtto, Int64 location, Int64 truckno, Int32 yearidno, Int64 DriverIdno, Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mth in db.MatIssHeads
                           join mdtl in db.MatIssDetls on mth.MatIss_Idno equals mdtl.MatIssHead_Idno
                           join cito in db.tblCityMasters on mth.Loc_Idno equals cito.City_Idno
                           join Itm in db.tblItemMastPurs on mdtl.Iteam_Idno equals Itm.Item_Idno
                           join Acm in db.AcntMasts on mth.Driver_Idno equals Acm.Acnt_Idno into AMast
                           from Amtemp in AMast.DefaultIfEmpty()
                           join lm in db.LorryMasts on mth.Truck_Idno equals lm.Lorry_Idno into temp
                           from ctemp in temp.DefaultIfEmpty()
                           select new
                           {
                               Iteam_Idno = mdtl.Iteam_Idno,
                               Item_Name = Itm.Item_Name,
                               Qty=(from  mdtl1 in db.MatIssDetls 
                                      where mdtl1.Iteam_Idno == mdtl.Iteam_Idno select mdtl1.Item_Qty).Sum(),
                               mth.Driver_Idno,
                               Amtemp.Acnt_Name,
                               mth.MatIss_Idno,
                               mth.MatIss_No,
                               mth.MatIss_Date,
                               MatIss_Typ = ((mth.MatIss_Typ == 1) ? "Issue" : (mth.MatIss_Typ == 2) ? "ReIssue" : ""),
                               mth.Loc_Idno,
                               mth.Truck_Idno,
                               mth.Issue_To,
                               mdtl.Align,
                               mdtl.Prev_AlignDate,
                               mdtl.AlignDate,
                               //Item_Amnt = mth.Net_Amnt,
                               Item_Amnt=(from Amnt in db.MatIssDetls where Amnt.Iteam_Idno==mdtl.Iteam_Idno select Amnt.Item_Qty * Amnt.Item_Amnt).Sum(),
                               mth.Mat_Frm,
                               mth.ReMark,
                               mth.Year_Idno,
                               CityTo = cito.City_Name,
                               ctemp.Lorry_No
                           }).GroupBy(X=>X.Iteam_Idno).Select(X=>X.FirstOrDefault()).ToList();

                if (dtfrom != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.MatIss_Date).Date >= Convert.ToDateTime(dtfrom).Date select l).ToList();
                }
                if (dtto != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.MatIss_Date).Date <= Convert.ToDateTime(dtto).Date select l).ToList();
                }

                if (location > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == location).ToList();
                }
                if (truckno > 0)
                {
                    lst = (from I in lst where I.Truck_Idno == truckno select I).ToList();
                }
                if (DriverIdno > 0)
                {
                    lst = lst.Where(l => l.Driver_Idno == DriverIdno).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                return lst;
            }
        }
        public Int32 ItemType(Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 ItemType = Convert.ToInt32(db.tblItemMastPurs.Where(r => r.Item_Idno == ItemIdno).Select(d => d.ItemType).SingleOrDefault());
                return ItemType;
            }
        }
        public string ItemAlignDate(Int32 ItemIdno,string SerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string ItemType = Convert.ToString(db.MatIssDetls.Where(r => r.Iteam_Idno == ItemIdno && r.Serial_Number == SerialNo).Select(d => d.AlignDate).Max());
                return ItemType;
            }
        }
        public IList TyreSerial(Int32 ItemIdno, Int64 tyresizeidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.Stckdetls
                           where N.Is_Issued == false && N.MtrlIssue_Idno == 0 && N.ItemIdno == ItemIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && N.Item_from !="CP"
                           select new
                           {
                               StckId = N.SerlDetl_id,
                               SerialNo = N.SerialNo
                           }).ToList();
                return Lst;
            }
        }
        public IList TyreSerialFromLoc(Int32 ItemIdno,Int64 LocIdno,Int64 tyresizeidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.Stckdetls
                           where N.Is_Issued == false && N.MtrlIssue_Idno == 0 && N.ItemIdno == ItemIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && (N.Loc_Idno == LocIdno) && (N.Item_from != "CP")
                           select new
                           {
                               StckId = N.SerlDetl_id,
                               SerialNo = N.SerialNo
                           }).ToList();
                return Lst;
            }
        }
        public IList RTyreSerial()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.Stckdetls
                        //Comment Peeyush Kaushik 10/06/2016      Because  It is more like opening Stock and Not Issue
                           where ((N.Is_Issued != false || N.Is_Issued == null) && (N.Billed == false || N.Billed==null))
                           select new
                           {
                               StckId = N.SerlDetl_id,
                               SerialNo = N.SerialNo
                           }).ToList();
                return Lst;
            }
        }
        public IList TyreSerialInCaseUpdate(Int64 SerialIdno, Int64 ItemIdno,Int64 tyresizeidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.Stckdetls
                           where N.Is_Issued == false && N.MtrlIssue_Idno == 0 && N.ItemIdno == ItemIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && (N.Item_from!="CP")
                           select new
                           {
                               StckId = N.SerlDetl_id,
                               SerialNo = N.SerialNo
                           }).ToList()
                           .Union
                           (from N in db.Stckdetls
                            where N.Is_Issued == true && N.ItemIdno == ItemIdno && N.SerlDetl_id == SerialIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && (N.Item_from != "CP")
                            select new
                            {
                                StckId = N.SerlDetl_id,
                                SerialNo = N.SerialNo
                            }).ToList();
                return Lst;
            }
        }
        public IList TyreSerialInCaseUpdateFromLoc(Int64 SerialIdno, Int64 ItemIdno,Int64 LocIdno, Int64 tyresizeidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from N in db.Stckdetls
                           where N.Is_Issued == false && N.MtrlIssue_Idno == 0 && N.ItemIdno == ItemIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && (N.Loc_Idno == LocIdno) && (N.Item_from != "CP")
                           select new
                           {
                               StckId = N.SerlDetl_id,
                               SerialNo = N.SerialNo
                           }).ToList()
                           .Union
                           (from N in db.Stckdetls
                            where N.Is_Issued == true && N.ItemIdno == ItemIdno && N.SerlDetl_id == SerialIdno && N.TyresizeIdno == tyresizeidno && (N.Billed == false || N.Billed == null) && (N.Loc_Idno == LocIdno) && (N.Item_from != "CP")
                            select new
                            {
                                StckId = N.SerlDetl_id,
                                SerialNo = N.SerialNo
                            }).ToList();
                return Lst;
            }
        }
        public List<tblCityMaster> BindToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    where obj.AsLocation == true && obj.Status == true
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public DataTable SelectCurrentStockSummary(string con, Int64 YearIdno, Int64 LocIdno, Int64 ItemIdno)
        {
            DataTable objDtTemp = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "CheckAccessoryInStock");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@LocIdno", LocIdno);
            objSqlPara[3] = new SqlParameter("@ItemIdno", ItemIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spStckSummary", objSqlPara);
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    objDtTemp = objDsTemp.Tables[0];
                }
            }

            return objDtTemp;
        }

        public IList SelectForSearch(Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.MatIssDetls
                           join ig in db.MatIssHeads on mast.MatIssHead_Idno equals ig.MatIss_Idno
                           join IT in db.tblItemMastPurs on mast.Iteam_Idno equals IT.Item_Idno
                           //join IM in db.tblItemTypeMasts on mast.ItemType equals IM.ItemTpye_Idno
                           //join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno

                           select new
                           {
                               Date = ig.MatIss_Date,
                               ItemIdno = mast.Iteam_Idno,
                               ItemName = IT.Item_Name,
                               Serial = mast.Serial_Number,
                               NSD = mast.NSD,
                               PSI = mast.PSI,
                               Type = mast.TType,
                               Align = ((mast.Align == true)?"Yes":"No"),
                               AlignDate = mast.AlignDate
                           }).ToList();

                if (ItemIdno > 0)
                {
                    lst = (from I in lst where I.ItemIdno == ItemIdno select I).ToList();
                }
                return lst;
            }
        }
        public List<tblPositionMast> Bindtyreposition()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblPositionMast> obj1 = new List<tblPositionMast>();

                obj1 = (from obj in db.tblPositionMasts
                        orderby obj.Position_name
                        select obj).ToList();

                return obj1;
            }
        }
    }
}
