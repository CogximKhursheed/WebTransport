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
    public class TyreIssueDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region FOR DML STATEMENTS i.e INSERT/DELETE/UPDATE
        public Int64 InsertMat(DateTime? dtMat_Date, Int32 IAgainst, Int64 intMat_No, Int32 intLoc_Idno, Int32 TruckNoIdno, Double DNetAmnt, Int32 YearIdno, DataTable dtDetail, Int32 intIssueTo, string strRemark, Int64 DriverIdno)
        {

            Int64 intMateIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                try
                {

                    TyreIssHead objMatHead = db.TyreIssHeads.Where(rh => (rh.MatIss_No == intMat_No) && (rh.Loc_Idno == intLoc_Idno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();

                    if (objMatHead == null)
                    {

                        objMatHead = new TyreIssHead();
                        objMatHead.MatIss_Date = dtMat_Date;
                        objMatHead.MatIss_Typ = IAgainst;
                        objMatHead.MatIss_No = intMat_No;
                        objMatHead.Loc_Idno = intLoc_Idno;
                        objMatHead.Truck_Idno = TruckNoIdno;
                        objMatHead.Net_Amnt = DNetAmnt;
                        objMatHead.Year_Idno = YearIdno;
                        objMatHead.Issue_To = intIssueTo;
                        objMatHead.ReMark = strRemark;
                        objMatHead.Date_Added = System.DateTime.Now;
                        objMatHead.Mat_Frm = "BK";
                        objMatHead.Driver_Idno = DriverIdno;
                        db.TyreIssHeads.AddObject(objMatHead);
                        db.SaveChanges();
                        intMateIdno = objMatHead.MatIss_Idno;
                        if (intMateIdno > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                               Int64 StckDetlIdno=Convert.ToInt64(row["StckDetl_Idno"]);
                               Stckdetl objStckDetl = db.Stckdetls.Where(r => r.SerlDetl_id == StckDetlIdno).SingleOrDefault();
                                objStckDetl.MtrlIssue_Idno = Convert.ToInt64(intMateIdno);
                                objStckDetl.Is_Issued = true;

                                TyreIssDetl objMatDetl = new TyreIssDetl();
                                objMatDetl.MatIssHead_Idno = Convert.ToInt64(intMateIdno);
                                objMatDetl.Iteam_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objMatDetl.Item_Qty = Convert.ToInt64(row["Quantity"]);
                                objMatDetl.Item_Weght = Convert.ToDouble(row["Weight"]);
                                objMatDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                objMatDetl.Item_Amnt = Convert.ToDouble(row["Amount"]);
                                objMatDetl.Remark = Convert.ToString(row["Detail"]);
                                objMatDetl.StckDetl_Idno= Convert.ToInt64(row["StckDetl_Idno"]);

                                db.TyreIssDetls.AddObject(objMatDetl);
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

        public Int64 MatUpdate(Int64 intGRIdno, DateTime? dtMat_Date, Int32 IAgainst, Int64 intMat_No, Int32 intLoc_Idno, Int32 TruckNoIdno, Double DNetAmnt, Int32 YearIdno, DataTable dtDetail, Int32 intIssueTo, string strRemark, Int64 DriverIdno)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TyreIssHead objGRHead = db.TyreIssHeads.Where(rh => (rh.MatIss_Idno != intGRIdno) && (rh.MatIss_No == intMat_No) && (rh.Loc_Idno == intLoc_Idno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        TyreIssHead objMatIssHead1 = db.TyreIssHeads.Where(rh => rh.MatIss_Idno == intGRIdno).FirstOrDefault();
                        if (objMatIssHead1 != null)
                        {
                            objMatIssHead1.MatIss_Date = dtMat_Date;

                            objMatIssHead1.MatIss_Typ = IAgainst;
                            objMatIssHead1.MatIss_No = intMat_No;
                            objMatIssHead1.Loc_Idno = intLoc_Idno;
                            objMatIssHead1.Truck_Idno = TruckNoIdno;
                            objMatIssHead1.Net_Amnt = DNetAmnt;
                            objMatIssHead1.Year_Idno = YearIdno;
                            objMatIssHead1.Issue_To = intIssueTo;
                            objMatIssHead1.ReMark = strRemark;
                            objMatIssHead1.Date_Modified = System.DateTime.Now;
                            objMatIssHead1.Mat_Frm = "BK";
                            objMatIssHead1.Driver_Idno = DriverIdno;
                            db.SaveChanges();
                            intGrIdno = objMatIssHead1.MatIss_Idno;

                            if (intGrIdno > 0)
                            {
                                List<TyreIssDetl> lstGrDetl = db.TyreIssDetls.Where(obj => obj.MatIssHead_Idno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (TyreIssDetl obj in lstGrDetl)
                                    {
                                        db.TyreIssDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                List<Stckdetl> ObjStckDetl = db.Stckdetls.Where(h => h.MtrlIssue_Idno == intGrIdno).ToList();
                                if (ObjStckDetl != null)
                                {
                                    foreach (var b in ObjStckDetl)
                                    {
                                        b.MtrlIssue_Idno = 0;
                                        b.Is_Issued = false;
                                        db.SaveChanges();
                                    }
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                   
                                    Int64 StckDetlIdno = Convert.ToInt64(row["StckDetl_Idno"]);
                                    Stckdetl objStckDetl = db.Stckdetls.Where(r => r.SerlDetl_id == StckDetlIdno).SingleOrDefault();
                                    objStckDetl.MtrlIssue_Idno = Convert.ToInt64(intGrIdno);
                                    objStckDetl.Is_Issued = true;

                                    TyreIssDetl objMatIssDetl = new TyreIssDetl();
                                    objMatIssDetl.MatIssHead_Idno = Convert.ToInt64(intGrIdno);
                                    objMatIssDetl.Iteam_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objMatIssDetl.Item_Qty = Convert.ToInt64(row["Quantity"]);
                                    objMatIssDetl.Item_Weght = Convert.ToDouble(row["Weight"]);
                                    objMatIssDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objMatIssDetl.Item_Amnt = Convert.ToDouble(row["Amount"]);
                                    objMatIssDetl.Remark = Convert.ToString(row["Detail"]);
                                    objMatIssDetl.StckDetl_Idno = Convert.ToInt64(row["StckDetl_Idno"]);
                                    db.TyreIssDetls.AddObject(objMatIssDetl);
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
                TyreIssHead qth = db.TyreIssHeads.Where(h => h.MatIss_Idno == HeadId).FirstOrDefault();
                List<Stckdetl> ObjStckDetl = db.Stckdetls.Where(h => h.MtrlIssue_Idno == HeadId).ToList();
                List<TyreIssDetl> qtd = db.TyreIssDetls.Where(d => d.MatIssHead_Idno == HeadId).ToList();
                if (qth != null && ObjStckDetl!=null)
                {
                    foreach (var d in qtd)
                    {
                        db.TyreIssDetls.DeleteObject(d);
                        db.SaveChanges();
                    }

                    foreach (var b in ObjStckDetl)
                    {
                        b.MtrlIssue_Idno = 0;
                        b.Is_Issued = false;
                        db.SaveChanges();
                    }
                    db.TyreIssHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
        #endregion
        public Int64 MaxNo(string MAtfrom, Int32 yearId, Int32 Location, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(MatIss_No),0) + 1 AS MAXID FROM TyreIssHead WHERE Mat_Frm='" + MAtfrom + "' AND Loc_Idno=" + Location;
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
                db.Connection.Open();
                var lst = (from m in db.TyreIssHeads
                           where m.MatIss_No == intMatNo && m.Year_Idno == intYearIdno && m.Mat_Frm == "BK" && m.Loc_Idno == LoctionIdno
                           select new
                           {
                               m.MatIss_No
                           }).ToList();
                return lst;
                db.Connection.Close();
            }
        }
        public IList SelectMatrial(DateTime? dtfrom, DateTime? dtto, Int64 location, Int64 truckno, Int32 yearidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mth in db.TyreIssHeads
                           join cito in db.tblCityMasters on mth.Loc_Idno equals cito.City_Idno
                           join lm in db.LorryMasts on mth.Truck_Idno equals lm.Lorry_Idno
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
                               lm.Lorry_No

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
                    lst = lst.Where(l => l.Truck_Idno == location).ToList();
                }
                return lst;
            }
        }
        public TyreIssHead SelectMatHead(Int64 MatIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TyreIssHeads.Where(tgh => (tgh.MatIss_Idno == MatIdno)).FirstOrDefault();
            }
        }
        public IList SelectMatDetail(int matIDNO)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GH in db.TyreIssHeads
                    join GD in db.TyreIssDetls on GH.MatIss_Idno equals GD.MatIssHead_Idno
                           join IM in db.tblItemMastPurs on GD.Iteam_Idno equals IM.Item_Idno
                           join SD in db.Stckdetls on GD.StckDetl_Idno equals SD.SerlDetl_id
                           where GH.MatIss_Idno == matIDNO
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
                               SD.SerialNo,
                               SD.SerlDetl_id
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
        public dynamic PrvReciver(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from N in db.TyreIssHeads where N.Truck_Idno == TruckIdno orderby N.MatIss_Idno descending select N.Driver_Idno).FirstOrDefault(); ;
            }
        }
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Lorry_Type == 1
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        //Lorry_No = ((obj.Lorry_No.Length)>4)?((obj.Lorry_No).Substring(obj.Lorry_No.Length - 4, obj.Lorry_No.Length)) + 
                                        //((obj.Lorry_No).Substring(0, obj.Lorry_No.Length - 4)):obj.Lorry_No, Lorry_Idno = obj.Lorry_Idno
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList BindSerialNo( Int64 Item_Idno,Int64 LocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.Stckdetls
                                    where obj.MtrlIssue_Idno == 0 && obj.Is_Issued == false && obj.ItemIdno == Item_Idno && obj.Loc_Idno == LocIdno
                                    orderby obj.SerialNo
                                    select new
                                    {
                                        Serial_No = obj.SerialNo,
                                        Serial_Idno = obj.SerlDetl_id
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList PrvItem(Int64 TruckIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objA = (from M in db.TyreIssHeads
                            join D in db.TyreIssDetls on M.MatIss_Idno equals D.MatIssHead_Idno
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

                objItemMast = (from obj in db.tblItemMastPurs where obj.ItemType==1 && obj.ItemType !=null
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }

        public List<tblItemMastPur> BindActiveItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               where obj.Status==true
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        // Matrial issue Report

        public IList SelectMatrialIssueReport(DateTime? dtfrom, DateTime? dtto, Int64 location, Int64 truckno, Int32 yearidno, Int64 DriverIdno, Int64 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mth in db.TyreIssHeads
                           join mdtl in db.TyreIssDetls on mth.MatIss_Idno equals mdtl.MatIssHead_Idno
                           join Itm in db.tblItemMastPurs on mdtl.Iteam_Idno equals Itm.Item_Idno
                           join cito in db.tblCityMasters on mth.Loc_Idno equals cito.City_Idno
                           join lm in db.LorryMasts on mth.Truck_Idno equals lm.Lorry_Idno
                           join Acm in db.AcntMasts on mth.Driver_Idno equals Acm.Acnt_Idno
                           select new
                           {
                               mdtl.Iteam_Idno,
                               Itm.Item_Name,
                               mth.Driver_Idno,
                               Acm.Acnt_Name,
                               mth.MatIss_Idno,
                               mth.MatIss_No,
                               mth.MatIss_Date,
                               MatIss_Typ = ((mth.MatIss_Typ == 1) ? "Issue" : (mth.MatIss_Typ == 2) ? "ReIssue" : ""),
                               mth.Loc_Idno,
                               mth.Truck_Idno,
                               mth.Issue_To,
                               mth.Net_Amnt,
                               mdtl.Item_Amnt,
                               mth.Mat_Frm,
                               mth.ReMark,
                               mth.Year_Idno,
                               CityTo = cito.City_Name,
                               lm.Lorry_No
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
                    lst = lst.Where(l => l.Truck_Idno == location).ToList();
                }
                if (DriverIdno > 0)
                {
                    lst = lst.Where(l => l.Driver_Idno == DriverIdno).ToList();
                }
                if (ItemIdno > 0)
                {
                    lst = lst.Where(l => l.Iteam_Idno == ItemIdno).ToList();
                }

                return lst;
            }
        }
    }
}
