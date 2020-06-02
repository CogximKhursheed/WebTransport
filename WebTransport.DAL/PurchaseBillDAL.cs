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
    public class PurchaseBillDAL
    {
        public DataTable BindSenderForPurchaseBill(string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SenderName");
            objSqlPara[1] = new SqlParameter("@PTyp", 2);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public DataTable BindSenderForPurchaseBill1(int PType, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SenderName");
            objSqlPara[1] = new SqlParameter("@PTyp", PType);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public IList CheckIExistInOther(Int32 Pbillidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblTripFuelDetls
                        where obj.Pbill_Idno == Pbillidno
                        select new
                        {
                            ItemGrp = obj.Pbill_Idno
                        }
                        ).ToList();

            }
        }
        public DataTable GetPurchaseBillNumber(string FromLocationIdno, string Year_Idno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "GetBillNumber");
            objSqlPara[1] = new SqlParameter("@Loc_Idno", Convert.ToInt32(FromLocationIdno));
            objSqlPara[2] = new SqlParameter("@Year_Idno", Convert.ToInt32(Year_Idno));
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public DataTable CheckBillNoExists(string FromLocationIdno, string Year_Idno, string PrefNo,Int64 BillNo ,string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "CheckBillNoExists");
            objSqlPara[1] = new SqlParameter("@Loc_Idno", Convert.ToInt32(FromLocationIdno));
            objSqlPara[2] = new SqlParameter("@Year_Idno", Convert.ToInt32(Year_Idno));
            objSqlPara[3] = new SqlParameter("@PrefNo", Convert.ToString(PrefNo));
            objSqlPara[4] = new SqlParameter("@PBillHeadNo", Convert.ToString(BillNo));
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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



        public DataTable GetItemDetails(Int64 Item_Idno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetItemDetails");
            objSqlPara[1] = new SqlParameter("@Item_Idno", Convert.ToInt32(Item_Idno));
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public DataTable GetEffectivePrice(Int64 Pump_Idno, Int64 Item_Idno, string EffDate, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "GetEffectivePriceRate");
            objSqlPara[1] = new SqlParameter("@Item_Idno", Convert.ToInt32(Item_Idno));
            objSqlPara[2] = new SqlParameter("@Pump_Idno", Convert.ToInt32(Pump_Idno));
            objSqlPara[3] = new SqlParameter("@Date", EffDate.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public DataTable GetItemDetailsExl(string ItemName, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "GetItemDetailsExl");
            objSqlPara[1] = new SqlParameter("@ItemName", ItemName);
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spPurBill", objSqlPara);
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
        public Int64 GetPartyIdno(string PrtyName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 PrtyId = Convert.ToInt64(db.AcntMasts.Where(r => r.Acnt_Name == PrtyName).Select(r => r.Acnt_Idno).FirstOrDefault());
                return PrtyId;
            }

        }
        public Int64 GetLorryIdno(string LorryNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 LorryId = Convert.ToInt64(db.LorryMasts.Where(r => r.Lorry_No == LorryNo).Select(r => r.Lorry_Idno).FirstOrDefault());
                return LorryId;
            }

        }
        public Int64 GetItemGroupIdno(Int64 IGrp_Idno)
        {
            Int64 IGrpIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = (from Obj in db.ItemMasts
                           where Obj.Item_Idno == IGrp_Idno
                           select new
                           {
                               Obj.IGrp_Idno
                           }).FirstOrDefault();

                if (Lst != null) { IGrpIdno = Convert.ToInt64(Lst.IGrp_Idno); }
                else { IGrpIdno = 0; }

                return IGrpIdno;
            }
        }
        public tblFleetAcntLink SelectAcntLink()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblFleetAcntLink AcntLink = (from UP in db.tblFleetAcntLinks select UP).FirstOrDefault();
                return AcntLink;
            }
        }
        public IList checkPurchaseBillNumberExist(Int64 BillNumberm, Int64 BillYear)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblPBillHeads
                           where obj.Loc_Idno == BillNumberm && obj.Year_Idno == BillYear
                           select new
                           {
                               obj.Loc_Idno
                           }).ToList();
                return lst;
            }
        }
        public Int64 Insert(Int64 Year_Idno, Int64 Loc_Idno, DateTime? PBillHead_Date, string Prefix_No, Int64 PBillHead_No, int Pur_Type, Int64 Prty_Idno, int Bill_Type, string Remark, Double Tot_Amnt, int Disc_type, Double Disc_Amnt, Double Other_Amnt, Double RndOff_Amnt, Double Net_Amnt, DateTime? Date_Added, DataTable dtDetail, Int64 LorryIdno,Double Discount)
        {
            Int64 PBillHead_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblPBillHead objBillHead = db.tblPBillHeads.Where(bill => (bill.PBillHead_No == PBillHead_No) && (bill.Year_Idno == Year_Idno) && (bill.Loc_Idno == Loc_Idno)).FirstOrDefault();
                    if (objBillHead == null)
                    {
                        objBillHead = new tblPBillHead();
                        objBillHead.Year_Idno = Year_Idno;
                        objBillHead.Loc_Idno = Loc_Idno;
                        objBillHead.PBillHead_Date = PBillHead_Date;
                        objBillHead.Prefix_No = Prefix_No;
                        objBillHead.PBillHead_No = PBillHead_No;
                        objBillHead.Pur_Type = Pur_Type;
                        objBillHead.Prty_Idno = Prty_Idno;
                        objBillHead.Bill_Type = Bill_Type;
                        objBillHead.Remark = Remark;
                        objBillHead.Tot_Amnt = Tot_Amnt;
                        objBillHead.Disc_type = Disc_type;
                        objBillHead.Disc_Amnt = Disc_Amnt;
                        objBillHead.Other_Amnt = Other_Amnt;
                        objBillHead.RndOff_Amnt = RndOff_Amnt;
                        objBillHead.Net_Amnt = Net_Amnt;
                        objBillHead.Date_Added = Date_Added;
                        objBillHead.LorryIdno = LorryIdno;
                        objBillHead.Discount = Discount;
                        objBillHead.Billed = false;
                        db.tblPBillHeads.AddObject(objBillHead);
                        db.SaveChanges();
                        PBillHead_Idno = objBillHead.PBillHead_Idno;

                        if (PBillHead_Idno > 0)
                        {
                            foreach (DataRow dr in dtDetail.Rows)
                            {
                                tblPBillDetl objBillDetl = new tblPBillDetl();
                                objBillDetl.PBillHead_Idno = PBillHead_Idno;
                                objBillDetl.Item_Idno = Convert.ToInt64(dr["Item_Idno"]);
                                objBillDetl.IGrp_Idno = Convert.ToInt64(dr["IGrp_Idno"]);
                                objBillDetl.Qty = Convert.ToDouble(dr["Quantity"]);
                                objBillDetl.Unit_Idno = Convert.ToInt64(dr["Unit_Idno"]);
                                objBillDetl.Rate_Type = Convert.ToInt32(dr["Rate_TypeIdno"]);
                                objBillDetl.Item_Rate = Convert.ToDouble(dr["Rate"]);
                                objBillDetl.Amount = Convert.ToDouble(dr["Amount"]);
                                objBillDetl.Tot_Weght = Convert.ToDouble(dr["Weight"]);
                                objBillDetl.Item_Tax = Convert.ToDouble(dr["Vat"]);
                                objBillDetl.Tax_Rate = Convert.ToDouble(dr["TaxRate"]);
                                objBillDetl.Disc_Type = Convert.ToDouble(dr["DivDiscType"]);
                                objBillDetl.Disc_Value = Convert.ToDouble(dr["DivDiscValue"]);
                                objBillDetl.Disc_Amnt = Convert.ToDouble(dr["DivDiscAmnt"]);
                                objBillDetl.Other_Amnt = Convert.ToDouble(dr["DivDiscOthAmnt"]);
                                objBillDetl.TyresizeIdno = Convert.ToInt64(dr["Tyresize_Idno"]);
                                objBillDetl.SGST_Per = Convert.ToDouble(dr["SGST_Per"]);
                                objBillDetl.CGST_Per = Convert.ToDouble(dr["CGST_Per"]);
                                objBillDetl.IGST_Per = Convert.ToDouble(dr["IGST_Per"]);
                                objBillDetl.SGST_Amt = Convert.ToDouble(dr["SGST_Amt"]);
                                objBillDetl.CGST_Amt = Convert.ToDouble(dr["CGST_Amt"]);
                                objBillDetl.IGST_Amt = Convert.ToDouble(dr["IGST_Amt"]);
                             
                                db.tblPBillDetls.AddObject(objBillDetl);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return PBillHead_Idno;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return PBillHead_Idno;
            }
        }
        public Int64 Update(Int64 PBillHead_Idno, Int64 Year_Idno, Int64 Loc_Idno, DateTime? PBillHead_Date, string Prefix_No, Int64 PBillHead_No, int Pur_Type, Int64 Prty_Idno, int Bill_Type, string Remark, Double Tot_Amnt, int Disc_type, Double Disc_Amnt, Double Other_Amnt, Double RndOff_Amnt, Double Net_Amnt, DateTime? Date_Updated, DataTable dtDetail, Int64 LorryIdno,Double Discount)
        {
            Int64 BillHead_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblPBillHead objBillHead = db.tblPBillHeads.Where(Bh => (Bh.PBillHead_Idno != PBillHead_Idno) && (Bh.PBillHead_No == PBillHead_No) && (Bh.Loc_Idno == Loc_Idno) && (Bh.Year_Idno == Year_Idno)).FirstOrDefault();
                    if (objBillHead == null)
                    {
                        tblPBillHead objBillHead1 = db.tblPBillHeads.Where(rh => rh.PBillHead_Idno == PBillHead_Idno).FirstOrDefault();
                        if (objBillHead1 != null)
                        {
                            objBillHead1.Year_Idno = Year_Idno;
                            objBillHead1.Loc_Idno = Loc_Idno;
                            objBillHead1.PBillHead_Date = PBillHead_Date;
                            objBillHead1.Prefix_No = Prefix_No;
                            objBillHead1.PBillHead_No = PBillHead_No;
                            objBillHead1.Pur_Type = Pur_Type;
                            objBillHead1.Prty_Idno = Prty_Idno;
                            objBillHead1.Bill_Type = Bill_Type;
                            objBillHead1.Remark = Remark;
                            objBillHead1.Tot_Amnt = Tot_Amnt;
                            objBillHead1.Disc_type = Disc_type;
                            objBillHead1.Disc_Amnt = Disc_Amnt;
                            objBillHead1.Other_Amnt = Other_Amnt;
                            objBillHead1.RndOff_Amnt = RndOff_Amnt;
                            objBillHead1.Net_Amnt = Net_Amnt;
                            objBillHead1.Discount = Discount;
                            objBillHead1.Date_Modified = Date_Updated;
                            objBillHead1.LorryIdno = LorryIdno;
                            db.SaveChanges();

                            BillHead_Idno = objBillHead1.PBillHead_Idno;
                            if (BillHead_Idno > 0)
                            {
                                List<tblPBillDetl> lstBillDetl = db.tblPBillDetls.Where(obj => obj.PBillHead_Idno == BillHead_Idno).ToList();
                                if (lstBillDetl.Count > 0)
                                {
                                    foreach (tblPBillDetl obj in lstBillDetl)
                                    {
                                        db.tblPBillDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }

                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    tblPBillDetl objBillDetl = new tblPBillDetl();
                                    objBillDetl.PBillHead_Idno = Convert.ToInt64(BillHead_Idno);
                                    objBillDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objBillDetl.IGrp_Idno = Convert.ToInt64(row["IGrp_Idno"]);
                                    objBillDetl.Qty = Convert.ToDouble(row["Quantity"]);
                                    objBillDetl.Unit_Idno = Convert.ToInt32(row["Unit_Idno"]);
                                    objBillDetl.Rate_Type = Convert.ToInt32(row["Rate_TypeIdno"]);
                                    objBillDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objBillDetl.Amount = Convert.ToDouble(row["Amount"]);
                                    objBillDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                    objBillDetl.Item_Tax = Convert.ToDouble(row["Vat"]);
                                    objBillDetl.Tax_Rate = Convert.ToDouble(row["TaxRate"]);
                                    objBillDetl.Disc_Type = Convert.ToDouble(row["DivDiscType"]);
                                    objBillDetl.Disc_Value = Convert.ToDouble(row["DivDiscValue"]);
                                    objBillDetl.Disc_Amnt = Convert.ToDouble(row["DivDiscAmnt"]);
                                    objBillDetl.Other_Amnt = Convert.ToDouble(row["DivDiscOthAmnt"]);
                                    objBillDetl.TyresizeIdno = string.IsNullOrEmpty(Convert.ToString(row["Tyresize_Idno"])) ? 0 : Convert.ToInt64(row["Tyresize_Idno"]);
                                    objBillDetl.SGST_Per = string.IsNullOrEmpty(Convert.ToString(row["SGST_Per"])) ? 0 : Convert.ToDouble(row["SGST_Per"]);
                                    objBillDetl.CGST_Per = string.IsNullOrEmpty(Convert.ToString(row["CGST_Per"])) ? 0 : Convert.ToDouble(row["CGST_Per"]);
                                    objBillDetl.IGST_Per = string.IsNullOrEmpty(Convert.ToString(row["IGST_Per"])) ? 0 : Convert.ToDouble(row["IGST_Per"]);
                                    objBillDetl.SGST_Amt = string.IsNullOrEmpty(Convert.ToString(row["SGST_Amt"])) ? 0 : Convert.ToDouble(row["SGST_Amt"]);
                                    objBillDetl.CGST_Amt = string.IsNullOrEmpty(Convert.ToString(row["CGST_Amt"])) ? 0 : Convert.ToDouble(row["CGST_Amt"]);
                                    objBillDetl.IGST_Amt = string.IsNullOrEmpty(Convert.ToString(row["IGST_Amt"])) ? 0 : Convert.ToDouble(row["IGST_Amt"]);
                                    db.tblPBillDetls.AddObject(objBillDetl);
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
        public tblPBillHead Select_PurchaseBillHead(Int64 PBillHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblPBillHeads.Where(tpbh => (tpbh.PBillHead_Idno == PBillHead_Idno)).FirstOrDefault();
            }
        }
        public IList Select_PurchaseBillDetail(Int64 PBillHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Ilst = (from PBD in db.tblPBillDetls
                            join IM in db.tblItemMastPurs on PBD.Item_Idno equals IM.Item_Idno
                            join UM in db.UOMMasts on PBD.Unit_Idno equals UM.UOM_Idno
                            join TS in db.TyreSizeMasters on PBD.TyresizeIdno equals TS.TyreSize_Idno into temptyre
                            from TS in temptyre.DefaultIfEmpty()
                            where PBD.PBillHead_Idno == PBillHead_Idno
                            select new
                            {
                                PBD.PBillDetl_Idno,
                                PBD.PBillHead_Idno,
                                PBD.Item_Idno,
                                PBD.IGrp_Idno,
                                PBD.Qty,
                                PBD.Unit_Idno,
                                PBD.Rate_Type,
                                PBD.Item_Rate,
                                PBD.Amount,
                                PBD.Tot_Weght,
                                PBD.Item_Tax,
                                IM.Item_Name,
                                UM.UOM_Name,
                                IM.ItemType,
                                PBD.Tax_Rate,
                                PBD.Disc_Type,
                                PBD.Disc_Value,
                                PBD.Disc_Amnt,
                                PBD.Other_Amnt,
                                PBD.TyresizeIdno,
                                TS.TyreSize,
                                PBD.SGST_Amt,
                                PBD.SGST_Per,
                                PBD.CGST_Amt,
                                PBD.CGST_Per,PBD.IGST_Amt,PBD.IGST_Per
                            }
                            ).ToList();

                return Ilst;
            }
        }
        public IList Select_PurchaseBillDetailList(int BillNumber, DateTime? BillDate, DateTime? BillDateTo, int cityfrom, int Sender, Int32 yearidno, Int64 UserIdno, Int32 purType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from PBH in db.tblPBillHeads
                           join cifrom in db.tblCityMasters on PBH.Loc_Idno equals cifrom.City_Idno
                           join acnts in db.AcntMasts on PBH.Prty_Idno equals acnts.Acnt_Idno
                           select new
                           {
                               acnts.Acnt_Name,
                               cifrom.City_Name,
                               PBH.PBillHead_Idno,
                               PBH.Pur_Type,
                               PBH.Loc_Idno,
                               PBH.PBillHead_Date,
                               PBH.PBillHead_No,
                               PBH.Net_Amnt,
                               PBH.Prty_Idno,
                               PBH.Year_Idno,
                               Bill_Type = ((PBH.Bill_Type == 1) ? "Credit" : "Cash"),
                               PurType = ((PBH.Pur_Type == 1) ? "Taxable" : (PBH.Pur_Type == 2) ? "Vat Pur." : "Fuel")

                           }).ToList();
                if (BillNumber > 0)
                {
                    lst = lst.Where(l => l.PBillHead_No == BillNumber).ToList();
                }
                if (BillDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.PBillHead_Date).Date >= Convert.ToDateTime(BillDate).Date).ToList();
                }
                if (BillDateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.PBillHead_Date).Date <= Convert.ToDateTime(BillDateTo).Date).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == cityfrom).ToList();
                }
                if (Sender > 0)
                {
                    lst = lst.Where(l => l.Prty_Idno == Sender).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.Loc_Idno)).ToList();
                }
                if (purType > 0)
                {
                    lst = lst.Where(l => l.Pur_Type == purType).ToList();
                }
                return lst;
            }
        }
        public IList Select_PurchaseBillRegister(int BillNumber, DateTime? BillDate, DateTime? BillDateTo, int cityfrom, int Sender, Int32 yearidno, Int64 UserIdno, Int32 purType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from PBH in db.tblPBillHeads
                           join cifrom in db.tblCityMasters on PBH.Loc_Idno equals cifrom.City_Idno
                           join acnts in db.AcntMasts on PBH.Prty_Idno equals acnts.Acnt_Idno
                           join lorry in db.LorryMasts on PBH.LorryIdno equals lorry.Lorry_Idno
                           select new
                           {
                               acnts.Acnt_Name,
                               cifrom.City_Name,
                               lorry.Lorry_No,
                               PBH.PBillHead_Idno,
                               PBH.Pur_Type,
                               PBH.Loc_Idno,
                               PBH.PBillHead_Date,
                               PBH.PBillHead_No,
                               PBH.Net_Amnt,
                               PBH.Prty_Idno,
                               PBH.Year_Idno,
                               PBH.LorryIdno,
                               Bill_Type = ((PBH.Bill_Type == 1) ? "Credit" : "Cash"),
                               PurType = ((PBH.Pur_Type == 1) ? "Taxable" : (PBH.Pur_Type == 2) ? "Vat Pur." : "Fuel"),
                               Qty = (from N in db.tblPBillDetls where N.PBillHead_Idno == PBH.PBillHead_Idno select N.Qty).Sum(),
                               Rate =(from N in db.tblPBillDetls where N.PBillHead_Idno == PBH.PBillHead_Idno select N.Item_Rate).Sum(),
                               Vat = (from N in db.tblPBillDetls where N.PBillHead_Idno == PBH.PBillHead_Idno select N.Item_Tax).Sum()

                           }).ToList();
                if (BillNumber > 0)
                {
                    lst = lst.Where(l => l.PBillHead_No == BillNumber).ToList();
                }
                if (BillDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.PBillHead_Date).Date >= Convert.ToDateTime(BillDate).Date).ToList();
                }
                if (BillDateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.PBillHead_Date).Date <= Convert.ToDateTime(BillDateTo).Date).ToList();
                }
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == cityfrom).ToList();
                }
                if (Sender > 0)
                {
                    lst = lst.Where(l => l.Prty_Idno == Sender).ToList();
                }
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearidno).ToList();
                }
                if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.Loc_Idno)).ToList();
                }
                if (purType > 0)
                {
                    lst = lst.Where(l => l.Pur_Type == purType).ToList();
                }
                return lst;
            }
        }
        public Int64 Select_PurchaseBillCount(Int32 yearIdno, DateTime? DateFrom, DateTime? DateTo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = (from PBH in db.tblPBillHeads
                             join cifrom in db.tblCityMasters on PBH.Loc_Idno equals cifrom.City_Idno
                             join acnts in db.AcntMasts on PBH.Prty_Idno equals acnts.Acnt_Idno
                             where PBH.Year_Idno == yearIdno && PBH.PBillHead_Date >= DateFrom
                             && PBH.PBillHead_Date <= DateTo
                             select PBH).Count();

                return lst;
            }
        }
        public int DeletePurchaseBill(int PBillHead_Idno)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblPBillHead pbh = db.tblPBillHeads.Where(h => h.PBillHead_Idno == PBillHead_Idno).FirstOrDefault();
                List<tblPBillDetl> qtd = db.tblPBillDetls.Where(d => d.PBillHead_Idno == PBillHead_Idno).ToList();
                List<Stckdetl> StDetl = db.Stckdetls.Where(d => d.PBillIdno == PBillHead_Idno && d.MtrlIssue_Idno == 0 && d.Is_Issued == false).ToList();
                if (pbh.Billed == true)
                {
                    return value = -1;
                }
                if (pbh != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblPBillDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblPBillHeads.DeleteObject(pbh);
                    db.SaveChanges();
                    value = 1;
                }
                if (StDetl != null)
                {
                    foreach (var S in StDetl)
                    {
                        db.Stckdetls.DeleteObject(S);
                        db.SaveChanges();
                    }
                    //value = 1;
                }
                clsAccountPosting objclsAccountPosting = new clsAccountPosting();
                Int64 intValue = objclsAccountPosting.DeleteAccountPosting(PBillHead_Idno, "PB");
                db.SaveChanges();
                if (intValue > 0)
                {
                    value = 1;
                }
            }
            return value;
        }
        public IList SelectStockforPurBill(Int64 PurBillIdno, Int64 ItemId,Int64 tyresizeidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from sd in db.Stckdetls
                           where sd.ItemIdno == ItemId && sd.PBillIdno == PurBillIdno && sd.TyresizeIdno == tyresizeidno
                           orderby sd.SerialNo
                           select new
                           {
                               SerlDetl_id = sd.SerlDetl_id,
                               SerialNo = sd.SerialNo,
                               PBillIdno = sd.PBillIdno,
                               ItemIdno = sd.ItemIdno,
                               sd.CompName,
                               sd.Type,
                               sd.PurFrom,
                               sd.TyresizeIdno
                           }

                             ).ToList();
                return lst;
            }
        }
        public bool CheckChasisForStck(string Serial, int VPurBillId, string SaveUpdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Stckdetl objStckDetl = null;
                if (SaveUpdate.ToLower() == "save")
                {
                    objStckDetl = db.Stckdetls.Where(stckd => stckd.SerialNo == Serial).FirstOrDefault();
                }
                else
                {
                    objStckDetl = db.Stckdetls.Where(stckd => stckd.SerialNo == Serial && stckd.PBillIdno != VPurBillId).FirstOrDefault();
                }

                if (objStckDetl != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public Int64 InsertPurBillStock(Int64 PBill_Idno, Int64 Item_Idno, string SerialNO, string Company, Int32 Type, string PurcFrom, Int64 LocIdno, Int32 YearIdno,Int64 tyresizeId)
        {
            Int64 SerialIdno = 0;

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Stckdetl objSerial = new Stckdetl();
                objSerial.SerialNo = SerialNO;
                objSerial.PBillIdno = PBill_Idno;
                objSerial.ItemIdno = Item_Idno;
                objSerial.Item_from = "PB";
                objSerial.Loc_Idno = LocIdno;
                objSerial.MtrlIssue_Idno = 0;
                objSerial.Is_Issued = false;
                objSerial.CompName = Company;
                objSerial.Type = Type;
                objSerial.PurFrom = PurcFrom;
                objSerial.yearId = YearIdno;
                objSerial.Billed = false;
                objSerial.TyresizeIdno = tyresizeId;
                db.Stckdetls.AddObject(objSerial);
                db.SaveChanges();
                SerialIdno = objSerial.SerlDetl_id;
                return SerialIdno;
            }
        }
        public Int64 UpdatePurBillStock(Int64 PBill_Idno, Int64 Item_Idno, string SerialNO, string Company, Int32 Type, string PurcFrom, Int64 LocIdno, Int64 StckDetlId, Int32 YearIdno,Int64 tyresizeId )
        {
            Int64 SerialIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Stckdetl objSerial = db.Stckdetls.Where(stckd => stckd.SerlDetl_id == StckDetlId).FirstOrDefault();
                if (objSerial != null)
                {
                    objSerial.SerialNo = SerialNO;
                    objSerial.PBillIdno = PBill_Idno;
                    objSerial.ItemIdno = Item_Idno;
                    objSerial.Item_from = "PB";
                    objSerial.Loc_Idno = LocIdno;
                    objSerial.MtrlIssue_Idno = 0;
                    objSerial.Is_Issued = false;
                    objSerial.CompName = Company;
                    objSerial.Type = Type;
                    objSerial.PurFrom = PurcFrom;
                    objSerial.yearId = YearIdno;
                    objSerial.TyresizeIdno = tyresizeId;
                    db.SaveChanges();
                    SerialIdno = objSerial.SerlDetl_id;
                }
            }
            return SerialIdno;
        }
        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters where cm.Status == true && cm.AsLocation == true orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }
        public IList BindSenderFromPetrolPumpMaster()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from cm in db.PetrolPumpMasters where cm.Status == true orderby cm.PPump_Name ascending select cm).ToList();
                return lst;
            }
        }
        public Int64 SelectPurNoByIdno(Int64 PbillIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = Convert.ToInt64((from p in db.tblPBillHeads where p.PBillHead_Idno == PbillIdno select p.PBillHead_No).FirstOrDefault());
                return lst;
            }
        }
        public IList CheckItemExistInOtherMaster(Int32 PurBillIdno,Int32 ItemIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblPBillDetls
                        join obj1 in db.Stckdetls on obj.PBillHead_Idno equals obj1.PBillIdno
                        where obj.PBillHead_Idno == PurBillIdno && obj.Item_Idno == ItemIdno
                        orderby obj1.SerialNo
                        select new
                        {
                            SerialIdo = obj1.SerlDetl_id,

                        }).ToList();
            }
        }
        public Stckdetl CheckPbill(Int32 PbillIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.Stckdetls
                        where obj.PBillIdno == PbillIdno && (obj.Is_Issued == true || obj.Billed == true || obj.Br_Trans == true)
                        orderby obj.SerialNo
                        select obj).FirstOrDefault();
            }
        }
        public Stckdetl CheckSerialExists(string strSerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.Stckdetls
                        where obj.SerialNo.Trim().ToLower() == strSerialNo.Trim().ToLower() && (obj.Is_Issued == true || obj.Billed == true || obj.Br_Trans == true)
                        orderby obj.SerialNo
                        select obj).FirstOrDefault();
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
        public tblCompMast GetCompany(Int64 CompID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast cm = (from c in db.tblCompMasts select c).FirstOrDefault();
                return cm;
            }
        }
        public tblCompMast GetCompany()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast cm = (from c in db.tblCompMasts select c).FirstOrDefault();
                return cm;
            }
        }
        public tblCompMast GetCompanyState(Int64 state_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast cm = (from c in db.tblCompMasts where c.State_Id == state_Idno select c).FirstOrDefault();
                return cm;
            }
        }
        public AcntMast GetPartyDetail(Int64 PartyID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast cd = (from c in db.AcntMasts where c.Acnt_Idno == PartyID select c).FirstOrDefault();
                return cd;
            }
        }
        public tblItemMastPur GetVehDetail(Int64 Item_idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblItemMastPur IM = (from i in db.tblItemMastPurs where i.Item_Idno == Item_idno select i).FirstOrDefault();
                return IM;
            }
        }
    }
}
