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
    public class SaleBillDAL
    {

        public bool CheckItem(string strItemName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                bool result = false;
                var lst1 = (from stckdetl in db.Stckdetls
                       where stckdetl.SerialNo == strItemName
                       select stckdetl).FirstOrDefault();
                if (lst1 != null && (string.IsNullOrEmpty(lst1.SerialNo) ? "" : lst1.SerialNo) != "")
                {
                    result = true;
                }
                return result;
            }
        }

        public IList BindModelName(Int64 SerlDetlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from IMP in db.tblItemMastPurs
                           join stckdetl in db.Stckdetls on IMP.Item_Idno equals stckdetl.ItemIdno
                           where stckdetl.SerlDetl_id == SerlDetlIdno
                           orderby IMP.Item_Name
                           select new
                               {
                                   ModelIdno = IMP.Item_Idno,
                                   ModelName = IMP.Item_Name,
                               })
                               .Union
                                (from IMP in db.tblItemMastPurs
                                 where IMP.ItemType == 2
                                 orderby IMP.Item_Name
                                 select new
                                 {
                                     ModelIdno = IMP.Item_Idno,
                                     ModelName = IMP.Item_Name,
                                 }).ToList();
                return lst;
            }
        }

        public Int64 SelectModel(Int64 serialIDNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 SNo = 0;
                SNo = (from IP in db.tblItemMastPurs join ST in db.Stckdetls on IP.Item_Idno equals ST.ItemIdno where ST.SerlDetl_id == serialIDNo select IP.Item_Idno).FirstOrDefault();
                return SNo;
            } 
        }

        public IList BindModelName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from IMP in db.tblItemMastPurs
                           join stckdetl in db.Stckdetls on IMP.Item_Idno equals stckdetl.ItemIdno
                           orderby IMP.Item_Name
                           select new
                           {
                               ModelIdno = IMP.Item_Idno,
                               ModelName = IMP.Item_Name,
                           })
                           .Union
                                (from IMP in db.tblItemMastPurs
                                 where IMP.ItemType == 2
                                 orderby IMP.Item_Name
                                 select new
                                 {
                                     ModelIdno = IMP.Item_Idno,
                                     ModelName = IMP.Item_Name,
                                 }).ToList();
                return lst;
            }
        }


        public Int64 GetSBillForLastPrint()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from SBH in db.tblSBillHeads select SBH.SBillHead_Idno).Max();
                if (max != null) { return Convert.ToInt64(max); } else { return 0; }

            }
        }
        public IList BindTyreType(Int64 SerlDetlIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stckdetl in db.Stckdetls
                           join TC in db.tblTyreCategories on stckdetl.Type equals TC.TyreType_IdNo
                           where stckdetl.SerlDetl_id == SerlDetlIdno && TC.Internal == true
                           orderby TC.TyreType_Name
                           select new
                           {
                               TyreType = TC.TyreType_Name,
                               TyreTypeIdno = TC.TyreType_IdNo,
                           }).ToList();
                return lst;
            }
        }
        public Int32 GetSBillNoMax(Int64 fromCity, string Prefix, Int64 BillType, Int64 YeaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from SBH in db.tblSBillHeads where SBH.FromLoc_Idno == fromCity && SBH.Prefix_No == Prefix && SBH.SBill_Type == BillType && SBH.Year_Idno == YeaIdno select SBH.SBill_No).Max() + 1;
                if (max != null) { return Convert.ToInt32(max); } else { return 1; }

            }
        }
        public Int64 Count()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from SBH in db.tblSBillHeads
                        select new
                        {
                            SBillHead_Idno = SBH.SBillHead_Idno,
                        }).Count();
            }
        }
        public string CheckOneParty(string con, string MIssueIdno, Int64 intYearIdno)
        {
            string Result = "0";
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "CheckOneParty");
            objSqlPara[1] = new SqlParameter("@MatIssIdno", MIssueIdno);
            objSqlPara[2] = new SqlParameter("@Year_Idno", intYearIdno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSaleBill", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    Result = objDsTemp.Tables[0].Rows[0]["Count"].ToString();
                }
            }
            return Result;
        }
        public IList SelectForSearch(Int64 Yearidno, DateTime? DateFrom, DateTime? DateTo, string PrefNo, Int64 SBillNo, Int64 FromLocation, Int64 BillType, Int64 PartyId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from SBH in db.tblSBillHeads
                           join cifrom in db.tblCityMasters on SBH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts in db.AcntMasts on SBH.Prty_Idno equals acnts.Acnt_Idno
                           select new
                           {
                               SbillHeadIdno = SBH.SBillHead_Idno,
                               SbillNo = SBH.SBill_No,
                               Against = ((SBH.Bill_Against == 1) ? "Counter" : "Matrial Issue"),
                               SBillDate = SBH.SBillHead_Date,
                               SbillTypeIdno = SBH.SBill_Type,
                               SbillType = ((SBH.SBill_Type == 1) ? "Credit" : "Cash"),
                               FromLocation = cifrom.City_Name,
                               Date = SBH.SBillHead_Date,
                               PrefNo = SBH.Prefix_No,
                               FromLocationIdno = cifrom.City_Idno,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               Billed = SBH.Billed,
                               YearIdno = SBH.Year_Idno,
                           }).ToList();
                if (Yearidno > 0)
                {
                    lst = (from l in lst where l.YearIdno == Yearidno select l).ToList();
                }
                if (SBillNo > 0)
                {
                    lst = (from l in lst where l.SbillNo == SBillNo select l).ToList();
                }
                if (PrefNo != "")
                {
                    lst = (from l in lst where l.PrefNo.ToLower().Contains(PrefNo.ToLower()) select l).ToList();
                }
                if (FromLocation > 0)
                {
                    lst = (from l in lst where l.FromLocationIdno == FromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Date).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Date).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (BillType > 0)
                {
                    lst = (from l in lst where l.SbillTypeIdno == BillType select l).ToList();
                }
                if (PartyId > 0)
                {
                    lst = (from l in lst where l.PartyIdno == PartyId select l).ToList();
                }
                return lst;
            }
        }
        public Int64 CheckClaimExists(Int64 SbillHead)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = Convert.ToInt64((from SBD in db.tblSBillDetls where SBD.SBillHead_Idno == SbillHead && (SBD.Claim_Idno != 0) select SBD).Count());
                return lst;
            }
        }

        public tblSBillHead Select_SaleBillHead(Int64 SBillHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblSBillHeads.Where(tpbh => (tpbh.SBillHead_Idno == SBillHead_Idno)).FirstOrDefault();
            }
        }



        public DataTable Select_SaleBillDetail(string con, Int64 SBillHead_Idno)
        {
            DataTable Result = null;
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SaleBillDetail");
            objSqlPara[1] = new SqlParameter("@BillHeadIdno", SBillHead_Idno);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSaleBill", objSqlPara);
            DataTable objDtTemp = new DataTable();
            if (objDsTemp.Tables.Count > 0)
            {
                if (objDsTemp.Tables[0].Rows.Count > 0)
                {
                    Result = objDsTemp.Tables[0];
                }
            }
            return Result;
        }

        //public IList Select_SaleBillDetail()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var Ilst = (from SBD in db.tblSBillDetls
        //                    join SD in db.Stckdetls on SBD.Item_Idno equals SD.SerlDetl_id
        //                    join IM in db.tblItemMastPurs on SD.ItemIdno equals IM.Item_Idno
        //                    join TC in db.tblTyreCategories on SBD.Tyre_Category equals TC.TyreType_IdNo
        //                    join MIss in db.MatIssHeads on SD.MtrlIssue_Idno equals MIss.MatIss_Idno into Issue
        //                    from mappingsIssue in Issue.DefaultIfEmpty()
        //                    where SBD.SBillHead_Idno == SBillHead_Idno
        //                    select new
        //                    {
        //                        SerialNo = SD.SerialNo,
        //                        SerialNoIdno = SD.ItemIdno,
        //                        ModelName = IM.Item_Name,
        //                        ModelNameIdno = IM.Item_Idno,
        //                        TyreType = TC.TyreType_IdNo,
        //                        TyreTypeName = TC.TyreType_Name,
        //                        RateTypeName = ((SBD.Rate_Type == 1) ? "Rate" : "Weight"),
        //                        RateType = SBD.Rate_Type,
        //                        Qty = SBD.Qty,
        //                        Rate = SBD.Item_Rate,
        //                        Weight = SBD.Item_Weigt,
        //                        ItemDiscType = SBD.Dis_Type,
        //                        ItemDiscTypeIdno = SBD.Dis_Type,
        //                        ItemDiscAMNT = SBD.Dis_Amnt,
        //                        Amount = SBD.Amount,
        //                        TaxAmnt = SBD.Item_Tax,
        //                        TaxRate = SBD.Tax_Rate,
        //                        MatIssNo = mappingsIssue.MatIss_No,
        //                        BillHeadIdno = SBD.SBillHead_Idno,
        //                        DiscountValue = SBD.Dis_Value,
        //                        ItemType= IM.ItemType,
        //                    }).ToList();

        //        return Ilst;
        //    }
        //}

        public DataTable SelectMIssueForSale(string con, Int64 iYearId, Int32 Stype, string AllMIssueIdno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SelectMIssueForSale");
            objSqlPara[1] = new SqlParameter("@MatIssIdno", AllMIssueIdno);
            objSqlPara[2] = new SqlParameter("@Year_Idno", iYearId);
            objSqlPara[3] = new SqlParameter("@STyp", Stype);

            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSaleBill", objSqlPara);
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

        public IList SelectMIssue(DateTime? DateFrom, DateTime? DateTo, Int64 YearIdno, Int64 FromCity)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Ilst = (from MIH in db.MatIssHeads
                            join LM in db.LorryMasts on MIH.Truck_Idno equals LM.Lorry_Idno into lm
                            from sub in lm.DefaultIfEmpty()
                            join CM in db.tblCityMasters on MIH.Loc_Idno equals CM.City_Idno
                            join MID in db.MatIssDetls on MIH.MatIss_Idno equals MID.MatIssHead_Idno
                            join AM in db.AcntMasts on MIH.Prty_Idno equals AM.Acnt_Idno into am
                            from subam in am.DefaultIfEmpty()
                            join SD in db.Stckdetls on MID.Serial_Number.Trim() equals SD.SerialNo.Trim() into SDD
                            from temp1 in SDD.DefaultIfEmpty()
                            where (MIH.Year_Idno == YearIdno) && (temp1.Billed == false || temp1.Billed == null) && (CM.City_Idno == FromCity) && (MIH.Issue_Type == 2)
                            orderby MIH.MatIss_Idno, MIH.Prty_Idno
                            select new
                            {
                                MatIss_Idno = MIH.MatIss_Idno,
                                MatIss_No = MIH.MatIss_No,
                                MatIss_Date = MIH.MatIss_Date,
                                Lorry_Idno = MIH.Loc_Idno,
                                Lorry_No = sub.Lorry_No,
                                City_Name = CM.City_Name,
                                PartyName = subam.Acnt_Name,
                                Prty_Idno = MIH.Prty_Idno,
                            }).Distinct().ToList();

                if (DateFrom != null)
                {
                    Ilst = Ilst.Where(l => Convert.ToDateTime(l.MatIss_Date).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    Ilst = Ilst.Where(l => Convert.ToDateTime(l.MatIss_Date).Date >= Convert.ToDateTime(DateTo).Date).ToList();
                }
                return Ilst;
            }
        }


        public int Delete(Int64 HeadId)
        {
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblSBillHead qth = db.tblSBillHeads.Where(h => h.SBillHead_Idno == HeadId).FirstOrDefault();
                List<tblSBillDetl> qtd = db.tblSBillDetls.Where(d => d.SBillHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblSBillDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblSBillHeads.DeleteObject(qth);
                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "SB");
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }


        #region Insert/ Update ..........

        public Int64 Insert(DateTime? SBillHead_Date, string Prefix_No, Int64 SBill_No, int SBill_Type, Int64 Against, int Tax_Type, Int64 Prty_Idno, Int64 FromLoc_Idno, string Remark, Double Tot_Tax, Double Other_Amnt, Double RndOff_Amnt, Double Net_Amnt, int Disc_type, Double Disc_Amnt, Double Other_Charges, Double Tot_Amnt, Int64 Year_Idno, DateTime? Date_Added, DataTable dtDetail)
        {
            Int64 SBillHead_Idno = 0;
            Int64 SBillDetailIdNo = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblSBillHead objBillHead = db.tblSBillHeads.Where(bill => (bill.SBill_No == SBill_No) && (bill.Year_Idno == Year_Idno) && (bill.Prefix_No == Prefix_No) && (bill.SBill_Type == SBill_Type) && (bill.FromLoc_Idno == FromLoc_Idno)).FirstOrDefault();
                    if (objBillHead == null)
                    {
                        objBillHead = new tblSBillHead();
                        objBillHead.SBillHead_Date = SBillHead_Date;
                        objBillHead.Prefix_No = Prefix_No;
                        objBillHead.SBill_No = SBill_No;
                        objBillHead.SBill_Type = SBill_Type;
                        objBillHead.Bill_Against = Against;
                        objBillHead.Tax_Type = Tax_Type;
                        objBillHead.Prty_Idno = Prty_Idno;
                        objBillHead.FromLoc_Idno = FromLoc_Idno;
                        objBillHead.Remark = Remark;
                        objBillHead.Tot_Tax = Tot_Tax;
                        objBillHead.Disc_type = Disc_type;
                        objBillHead.Disc_Amnt = Disc_Amnt;
                        objBillHead.Tot_Amnt = Tot_Amnt;
                        objBillHead.Other_Amnt = Other_Amnt;
                        objBillHead.RndOff_Amnt = RndOff_Amnt;
                        objBillHead.Net_Amnt = Net_Amnt;
                        objBillHead.Date_Added = Date_Added;
                        objBillHead.Billed = false;
                        objBillHead.Year_Idno = Year_Idno;

                        db.tblSBillHeads.AddObject(objBillHead);
                        db.SaveChanges();
                        SBillHead_Idno = objBillHead.SBillHead_Idno;

                        if (SBillHead_Idno > 0)
                        {
                            foreach (DataRow dr in dtDetail.Rows)
                            {
                                tblSBillDetl objBillDetl = new tblSBillDetl();
                                objBillDetl.SBillHead_Idno = SBillHead_Idno;
                                objBillDetl.Item_Idno = Convert.ToInt64(dr["SerialNoIdno"]);
                                objBillDetl.Model_Idno = Convert.ToInt64(dr["ModelIdNo"]);
                                objBillDetl.Tyre_Category = string.IsNullOrEmpty(Convert.ToString(dr["TyreTypeIdNo"])) ? 0 : Convert.ToInt64(dr["TyreTypeIdNo"]);
                                objBillDetl.Rate_Type = Convert.ToInt32(dr["RateTypeIdno"]);
                                objBillDetl.Qty = Convert.ToDouble(dr["Quantity"]);
                                objBillDetl.Item_Rate = Convert.ToDouble(dr["Rate"]);
                                objBillDetl.Item_Weigt = Convert.ToDouble(dr["Weight"]);
                                objBillDetl.Dis_Type = Convert.ToInt64(dr["DiscountType"]);
                                objBillDetl.Dis_Amnt = Convert.ToDouble(dr["Discount"]);
                                objBillDetl.Amount = Convert.ToDouble(dr["Amount"]);
                                objBillDetl.Item_Tax = Convert.ToDouble(dr["TaxAmnt"]);
                                objBillDetl.Tax_Rate = Convert.ToDouble(dr["TaxRate"]);
                                objBillDetl.Dis_Value = Convert.ToDouble(dr["DiscountValue"]);
                                objBillDetl.Item_Type = Convert.ToInt32(dr["ItemType"]);
                                objBillDetl.MatIss_Idno = Convert.ToInt32(dr["MatIss_Idno"]);
                                objBillDetl.Claim_Idno = 0;
                                db.tblSBillDetls.AddObject(objBillDetl);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return SBillHead_Idno;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return SBillHead_Idno;
            }
        }
        public Int64 Update(Int64 SBillHead_Idno, DateTime? SBillHead_Date, string Prefix_No, Int64 SBill_No, int SBill_Type, Int64 Against, int Tax_Type, Int64 Prty_Idno, Int64 FromLoc_Idno, string Remark, Double Tot_Tax, Double Other_Amnt, Double RndOff_Amnt, Double Net_Amnt, int Disc_type, Double Disc_Amnt, Double Other_Charges, Double Tot_Amnt, Int64 Year_Idno, DateTime? Date_Updated, DataTable dtDetail)
        {
            Int64 BillHead_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblSBillHead objBillHead = db.tblSBillHeads.Where(Bh => (Bh.SBillHead_Idno != SBillHead_Idno) && (Bh.SBill_No == SBill_No) && (Bh.Prefix_No == Prefix_No) && (Bh.SBill_Type == SBill_Type) && (Bh.FromLoc_Idno == FromLoc_Idno) && (Bh.Year_Idno == Year_Idno)).FirstOrDefault();
                    if (objBillHead == null)
                    {
                        tblSBillHead objBillHead1 = db.tblSBillHeads.Where(rh => rh.SBillHead_Idno == SBillHead_Idno).FirstOrDefault();
                        if (objBillHead1 != null)
                        {
                            objBillHead1.SBillHead_Date = SBillHead_Date;
                            objBillHead1.Prefix_No = Prefix_No;
                            objBillHead1.SBill_No = SBill_No;
                            objBillHead1.SBill_Type = SBill_Type;
                            objBillHead1.Bill_Against = Against;
                            objBillHead1.Tax_Type = Tax_Type;
                            objBillHead1.Prty_Idno = Prty_Idno;
                            objBillHead1.FromLoc_Idno = FromLoc_Idno;
                            objBillHead1.Remark = Remark;
                            objBillHead1.Tot_Tax = Tot_Tax;
                            objBillHead1.Disc_type = Disc_type;
                            objBillHead1.Disc_Amnt = Disc_Amnt;
                            objBillHead1.Tot_Amnt = Tot_Amnt;
                            objBillHead1.Other_Amnt = Other_Amnt;
                            objBillHead1.RndOff_Amnt = RndOff_Amnt;
                            objBillHead1.Net_Amnt = Net_Amnt;
                            objBillHead1.Date_Modified = Date_Updated;
                            objBillHead1.Year_Idno = Year_Idno;
                            db.SaveChanges();

                            BillHead_Idno = objBillHead1.SBillHead_Idno;
                            if (BillHead_Idno > 0)
                            {
                                List<tblSBillDetl> lstBillDetl = db.tblSBillDetls.Where(obj => obj.SBillHead_Idno == BillHead_Idno).ToList();
                                if (lstBillDetl.Count > 0)
                                {
                                    foreach (tblSBillDetl obj in lstBillDetl)
                                    {
                                        db.tblSBillDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }

                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    tblSBillDetl objBillDetl = new tblSBillDetl();
                                    objBillDetl.SBillHead_Idno = SBillHead_Idno;
                                    objBillDetl.Item_Idno = Convert.ToInt64(row["SerialNoIdno"]);
                                    objBillDetl.Model_Idno = Convert.ToInt64(row["ModelIdNo"]);
                                    objBillDetl.Tyre_Category = Convert.ToInt64(row["TyreTypeIdNo"]);
                                    objBillDetl.Rate_Type = Convert.ToInt32(row["RateTypeIdno"]);
                                    objBillDetl.Qty = Convert.ToDouble(row["Quantity"]);
                                    objBillDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objBillDetl.Item_Weigt = Convert.ToDouble(row["Weight"]);
                                    objBillDetl.Dis_Type = Convert.ToInt64(row["DiscountType"]);
                                    objBillDetl.Dis_Amnt = string.IsNullOrEmpty(row["Discount"].ToString()) ? 0 : Convert.ToDouble(row["Discount"]);
                                    objBillDetl.Amount = string.IsNullOrEmpty(row["Amount"].ToString()) ? 0 : Convert.ToDouble(row["Amount"]);
                                    objBillDetl.Item_Tax = string.IsNullOrEmpty(row["TaxAmnt"].ToString()) ? 0 : Convert.ToDouble(row["TaxAmnt"]);
                                    objBillDetl.Tax_Rate = string.IsNullOrEmpty(row["TaxRate"].ToString()) ? 0 : Convert.ToDouble(row["TaxRate"]);
                                    objBillDetl.Dis_Value = string.IsNullOrEmpty(row["DiscountValue"].ToString()) ? 0 : Convert.ToDouble(row["DiscountValue"]);
                                    objBillDetl.Item_Type = string.IsNullOrEmpty(row["ItemType"].ToString()) ? 0 : Convert.ToInt32(row["ItemType"]);
                                    objBillDetl.MatIss_Idno = string.IsNullOrEmpty(row["MatIss_Idno"].ToString()) ? 0 : Convert.ToInt32(row["MatIss_Idno"]);
                                    db.tblSBillDetls.AddObject(objBillDetl);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        BillHead_Idno = -1;
                    }
                }
                catch (Exception Ex)
                {
                    BillHead_Idno = 0;
                }
            }
            return BillHead_Idno;
        }

        #endregion


        public tblFleetAcntLink SelectAcntLink()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblFleetAcntLink AcntLink = (from UP in db.tblFleetAcntLinks select UP).FirstOrDefault();
                return AcntLink;
            }
        }

        public Int64 SelectSaleNoByIdno(Int64 PbillIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = Convert.ToInt64((from s in db.tblSBillHeads where s.SBillHead_Idno == PbillIdno select s.SBill_No).FirstOrDefault());
                return lst;
            }
        }

        public DataTable GetModelDetails(Int64 Item_Idno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetModelDetails");
            objSqlPara[1] = new SqlParameter("@Model_Idno", Convert.ToInt32(Item_Idno));
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spSaleBill", objSqlPara);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        public IList BindSerialNoPopulate(Int64 SaleBillIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stckdetl in db.Stckdetls
                           where (stckdetl.Item_from == "PB" || stckdetl.Item_from == "O" || stckdetl.Item_from == "MR") && (stckdetl.Billed == false) && (stckdetl.Is_Issued == null || stckdetl.Is_Issued == false)
                           orderby stckdetl.SerialNo
                           select new
                           {
                               SerialNo = stckdetl.SerialNo,
                               SerlDetlIdno = stckdetl.SerlDetl_id,
                           }).ToList()
                            .Union
                             (from obj in db.Stckdetls
                              where obj.SaleBill_Idno == SaleBillIdno
                              select new
                              {
                                  SerialNo = obj.SerialNo,
                                  SerlDetlIdno = obj.SerlDetl_id,
                              })
                            .Union
                                (from IMP in db.tblItemMastPurs
                                 where IMP.ItemType == 2
                                 orderby IMP.Item_Name
                                 select new
                                 {
                                     SerialNo = IMP.Item_Name,
                                     SerlDetlIdno = IMP.Item_Idno
                                 }).ToList();
                return lst;
            }
        }

    }
}
