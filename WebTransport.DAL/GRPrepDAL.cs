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
    public class GRPrepDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region FOR DML STATEMENTS i.e INSERT/DELETE/UPDATE
        public Int64 InsertGR(DateTime? dtGRDate, Int32 IAgainst, Int32 intGRType, String DIno, String EGPNo, Int64 intGRNo, Int32 intSenderIdno, Int32 TruckNoIdno,
            Int32 intRecvrIDno, Int32 intFromcityIDno, Int32 intTocityIDno, Int32 intDelPlaceIdno, Int32 intAgentIdno, String strRemark, Int32 intServTaxPaidBy,
            Int32 intRcptypeIdno, String InstNo, DateTime? dtInstDate, Int32 intcustBankIdno, Double DGrossAmnt, Double DCommission, Double DTollTax,
            Double DCartage, Double DBilty, Double DSubTotal, Double DTotalAmnt, Double DWages, Double DServTax, Double DSwchBrtTax, Double DSurchrge,
            Double DPF, Double DNetAmnt, Double DRoundOffAmnt, Int32 YearIdno, Int64 RcptGoodHeadIdno, Int64 AdvOrderGR_Idno, Boolean isTBBRate,
            Int32 itruckcitywise, string swages, DataTable dtDetail, string strShipmentNo, string PreFixGRNum, string ContainerNum, Int64 ContainerSize,
            Int64 ContainerType, string ContainerSealNum, Int64 iCityviaIdno, Int64 intType, Double dblTypeAmnt, bool FromExcel, string strRefNo,
            string strPortNum, Double KalyanTax, string ManualNo, Int64 UserIdno, string ConsName, Double dblTaxValid, Double dblServTaxPerc,
            Double dblSwcgBrtTaxPerc, Double dblKalyanTaxPer, Double TotItemValue, string strOrderNo, string strFormNo, string Container_Num2, string SealNum_2,
            Int32 ExpImp_id, string CharFrwder_Name, string DelPlace, bool TypeDelPlace, double dblFromKM, double dblToKM, double dblTotKM, Int64 Created_By,
            Double dSGST_Amt, Double dCGST_Amt, Double dIGST_Amt, Double dGSTCess_Amt, Double dSGST_Per, Double dCGST_Per, Double dIGST_Per, Double dGSTCess_Per, int GST_Idno, DateTime? dtEGPDate, string strEwayBill, String strTaxInvNo, string strExcInvNo, DateTime? dtDIDate, DateTime? dtTaxInvDate)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead objGRHead = db.TblGrHeads.Where(rh => (rh.PrefixGr_No == PreFixGRNum) && (rh.Gr_No == intGRNo) && (rh.DI_NO == DIno) && (rh.Tax_InvNo == strTaxInvNo) && (rh.From_City == intFromcityIDno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        objGRHead = new TblGrHead();
                        objGRHead.Gr_Date = dtGRDate;
                        objGRHead.GR_Agnst = IAgainst;
                        objGRHead.GR_Typ = intGRType;
                        objGRHead.DI_NO = DIno;
                        objGRHead.EGP_NO = EGPNo;
                        objGRHead.Gr_No = intGRNo;
                        objGRHead.PrefixGr_No = PreFixGRNum;
                        objGRHead.Lorry_Idno = TruckNoIdno;
                        objGRHead.Sender_Idno = intSenderIdno;
                        objGRHead.Recivr_Idno = intRecvrIDno;
                        objGRHead.From_City = intFromcityIDno;
                        objGRHead.To_City = intTocityIDno;
                        objGRHead.DelvryPlce_Idno = intDelPlaceIdno;
                        objGRHead.Agnt_Idno = intAgentIdno;
                        objGRHead.Remark = strRemark;
                        objGRHead.STax_Typ = intServTaxPaidBy;
                        objGRHead.RcptType_Idno = intRcptypeIdno;
                        objGRHead.Inst_No = InstNo;
                        objGRHead.Inst_Dt = dtInstDate;
                        objGRHead.Bank_Idno = intcustBankIdno;
                        objGRHead.Gross_Amnt = DGrossAmnt;
                        objGRHead.AgntComisn_Amnt = DCommission;
                        objGRHead.TollTax_Amnt = DTollTax;
                        objGRHead.Cartg_Amnt = DCartage;
                        objGRHead.Bilty_Amnt = DBilty;
                        objGRHead.SubTot_Amnt = DSubTotal;
                        objGRHead.Total_Amnt = DTotalAmnt;
                        objGRHead.Wages_Amnt = DWages;
                        objGRHead.ServTax_Amnt = DServTax;
                        objGRHead.SwchBrtTax_Amt = DSwchBrtTax;
                        objGRHead.Surcrg_Amnt = DSurchrge;
                        objGRHead.PF_Amnt = DPF;
                        objGRHead.Net_Amnt = DNetAmnt;
                        objGRHead.RndOff_Amnt = DRoundOffAmnt;
                        objGRHead.Year_Idno = YearIdno;
                        objGRHead.TBB_Rate = isTBBRate;
                        objGRHead.cmb_type = itruckcitywise;
                        objGRHead.WagesLabel_Print = swages;
                        objGRHead.Shipment_No = strShipmentNo;
                        objGRHead.Cityvia_Idno = iCityviaIdno;
                        objGRHead.AdvOrderGRHead_Idno = AdvOrderGR_Idno;
                        objGRHead.GRContanr_No = ContainerNum;
                        objGRHead.GRContanr_Size = ContainerSize;
                        objGRHead.GRContanr_Type = ContainerType;
                        objGRHead.GRContanr_SealNo = ContainerSealNum;
                        objGRHead.TypeId = intType;
                        objGRHead.FromExcel = FromExcel;
                        objGRHead.Type_Amnt = dblTypeAmnt;
                        objGRHead.Ref_No = strRefNo;
                        objGRHead.ManualNo = ManualNo;
                        objGRHead.UserIdno = UserIdno;
                        objGRHead.Consignor_Name = ConsName;
                        objGRHead.GRContanr_Port = strPortNum;
                        objGRHead.GR_Frm = "BK"; objGRHead.AgnstRcpt_No = Convert.ToString(RcptGoodHeadIdno) == null ? "" : Convert.ToString(RcptGoodHeadIdno);
                        objGRHead.Chln_Idno = 0; objGRHead.ChlnCrsng_Idno = 0; objGRHead.Billed = false; objGRHead.KisanKalyan_Amnt = KalyanTax;
                        objGRHead.ServTax_Valid = dblTaxValid;
                        objGRHead.ServTax_Perc = dblServTaxPerc;
                        objGRHead.SwcgBrtTax_Perc = dblSwcgBrtTaxPerc;
                        objGRHead.KisanKalyan_Per = dblKalyanTaxPer;
                        objGRHead.TotItem_Value = TotItemValue;
                        objGRHead.Ordr_No = strOrderNo;
                        objGRHead.Form_No = strFormNo;
                        objGRHead.GRContanr_No2 = Container_Num2;
                        objGRHead.GRContanr_SealNo2 = SealNum_2;
                        objGRHead.ImpExp_idno = ExpImp_id;
                        objGRHead.ChaFrwdr_Name = CharFrwder_Name;
                        objGRHead.DelPlace_Val = DelPlace;
                        objGRHead.TypeDelPlace = TypeDelPlace;
                        objGRHead.From_KM = dblFromKM;
                        objGRHead.To_KM = dblToKM;
                        objGRHead.Tot_KM = dblTotKM;
                        objGRHead.DateAdded = DateTime.Now;
                        objGRHead.DateModified = DateTime.Now;
                        //objGRHead.Created_By = Created_By;
                        //Upadhyay #GST values
                        objGRHead.SGST_Per = dSGST_Per;
                        objGRHead.CGST_Per = dCGST_Per;
                        objGRHead.IGST_Per = dIGST_Per;
                        objGRHead.GSTCess_Per = dGSTCess_Per;
                        objGRHead.SGST_Amt = dSGST_Amt;
                        objGRHead.CGST_Amt = dCGST_Amt;
                        objGRHead.IGST_Amt = dIGST_Amt;
                        objGRHead.GSTCess_Amt = dGSTCess_Amt;
                        objGRHead.GST_Idno = GST_Idno;
                        objGRHead.EGP_Date = dtEGPDate;
                        objGRHead.User_AddedBy = Created_By;
                        objGRHead.EWay_BillNO = strEwayBill;
                        objGRHead.Tax_InvNo = strTaxInvNo;
                        objGRHead.Exc_InvNo = strExcInvNo;
                        objGRHead.DI_Date = dtDIDate;
                        objGRHead.Tax_Date = dtTaxInvDate;
                        db.TblGrHeads.AddObject(objGRHead);
                        db.SaveChanges();
                        intGrIdno = objGRHead.GR_Idno;
                        if (intGrIdno > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                TblGrDetl objGRDetl = new TblGrDetl();
                                objGRDetl.GrHead_Idno = Convert.ToInt64(intGrIdno);
                                objGRDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objGRDetl.ItemGrade_Idno = string.IsNullOrEmpty(Convert.ToString(row["Grade_Idno"])) ? 0 : Convert.ToInt32(row["Grade_Idno"]);
                                objGRDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                objGRDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["Rate_TypeIdno"])) ? 0 : Convert.ToInt32(row["Rate_TypeIdno"]);
                                objGRDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                objGRDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                objGRDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                objGRDetl.Amount = Convert.ToDouble(row["Amount"]);
                                objGRDetl.Detail = Convert.ToString(row["Detail"]);
                                objGRDetl.Shrtg_Limit = Convert.ToDouble(Convert.ToString(row["Shrtg_Limit"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Limit"]));
                                objGRDetl.Shrtg_Rate = Convert.ToDouble(Convert.ToString(row["Shrtg_Rate"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Rate"]));
                                objGRDetl.Shrtg_Limit_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Limit_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Limit_Other"]));
                                objGRDetl.Shrtg_Rate_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Rate_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Rate_Other"]));
                                objGRDetl.UnloadWeight = Convert.ToDouble(row["UnloadWeight"]);
                                db.TblGrDetls.AddObject(objGRDetl);
                                db.SaveChanges();
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
        public Int64 GRPrepUpdate(Int64 intGRIdno, DateTime? dtGRDate, Int32 IAgainst, Int32 intGRType, String DIno, String EGPNo, Int64 intGRNo, 
            Int32 intSenderIdno, Int32 TruckNoIdno, Int32 intRecvrIDno, Int32 intFromcityIDno, Int32 intTocityIDno, Int32 intDelPlaceIdno, Int32 intAgentIdno, 
            String strRemark, Int32 intServTaxPaidBy, Int32 intRcptypeIdno, String InstNo, DateTime? dtInstDate, Int32 intcustBankIdno, Double DGrossAmnt, 
            Double DCommission, Double DTollTax, Double DCartage, Double DBilty, Double DSubTotal, Double DTotalAmnt, Double DWages, Double DServTax, 
            Double DSwchBrtTax, Double DSurchrge, Double DPF, Double DNetAmnt, Double DRoundOffAmnt, Int32 YearIdno, Int64 RcptGoodHeadIdno, Int64 AdvOrderGR_Idno, 
            Boolean isTBBRate, Int32 itruckcitywise, string swages, DataTable dtDetail, string strShipmentNo, string PreFixGRNum, string ContainerNum, 
            Int64 ContainerSize, Int64 ContainerType, string ContainerSealNum, Int64 iCityviaIdno, Int64 intType, Double dblTypeAmnt, string strRefNo, 
            string strPortNum, Double KalyanTax, string ManualNo, Int64 UserIdno, string ConsName, Double dblTaxValid, Double dblServTaxPerc, 
            Double dblSwcgBrtTaxPerc, Double dblKalyanTaxPer, Double TotItemValue, string strOrderNo, string strFormNo, string Container_Num2, 
            string SealNum_2, Int32 ExpImp_id, string CharFrwder_Name, string DelPlace, bool TypeDelPlace, double dblFromKM, double dblToKM, double dblTotKM,
            Int64 Created_By, Double dSGST_Amt, Double dCGST_Amt, Double dIGST_Amt, Double dGSTCess_Amt, Double dSGST_Per, Double dCGST_Per, Double dIGST_Per, Double dGSTCess_Per, int GST_Idno, DateTime? dtEGPDate, string strEwayBill, String strTaxInvNo, string strExcInvNo, DateTime? dtDIDate, DateTime? dtTaxInvDate)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead objGRHead = db.TblGrHeads.Where(rh => (rh.GR_Idno != intGRIdno) && (rh.Gr_No == intGRNo) && (rh.DI_NO == DIno) && (rh.Tax_InvNo == strTaxInvNo) && (rh.From_City == intFromcityIDno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        TblGrHead objGRHead1 = db.TblGrHeads.Where(rh => (rh.GR_Idno == intGRIdno)).FirstOrDefault();
                        if (objGRHead1 != null)
                        {
                            objGRHead1.Year_Idno = YearIdno;
                            objGRHead1.Gr_Date = dtGRDate;
                            objGRHead1.Gr_No = intGRNo;
                            objGRHead1.PrefixGr_No = PreFixGRNum;
                            objGRHead1.GR_Agnst = IAgainst;
                            objGRHead1.GR_Typ = intGRType;
                            objGRHead1.Sender_Idno = intSenderIdno;
                            objGRHead1.Recivr_Idno = intRecvrIDno;
                            objGRHead1.From_City = intFromcityIDno;
                            objGRHead1.To_City = intTocityIDno;
                            objGRHead1.DelvryPlce_Idno = intDelPlaceIdno;
                            objGRHead1.Cityvia_Idno = iCityviaIdno;
                            objGRHead1.Agnt_Idno = intAgentIdno;
                            objGRHead1.STax_Typ = intServTaxPaidBy;
                            objGRHead1.RcptType_Idno = intRcptypeIdno;
                            objGRHead1.Inst_No = InstNo;
                            objGRHead1.Inst_Dt = dtInstDate;
                            objGRHead1.Bank_Idno = intcustBankIdno;
                            objGRHead1.DI_NO = DIno;
                            objGRHead1.EGP_NO = EGPNo;
                            objGRHead1.Lorry_Idno = TruckNoIdno;
                            objGRHead1.Remark = strRemark;
                            objGRHead1.Gross_Amnt = DGrossAmnt;
                            objGRHead1.AgntComisn_Amnt = DCommission;
                            objGRHead1.TollTax_Amnt = DTollTax;
                            objGRHead1.Cartg_Amnt = DCartage;
                            objGRHead1.Bilty_Amnt = DBilty;
                            objGRHead1.SubTot_Amnt = DSubTotal;
                            objGRHead1.Total_Amnt = DTotalAmnt;
                            objGRHead1.Wages_Amnt = DWages;
                            objGRHead1.ServTax_Amnt = DServTax;
                            objGRHead1.SwchBrtTax_Amt = DSwchBrtTax;
                            objGRHead1.Surcrg_Amnt = DSurchrge;
                            objGRHead1.PF_Amnt = DPF;
                            objGRHead1.Net_Amnt = DNetAmnt;
                            objGRHead1.RndOff_Amnt = DRoundOffAmnt;
                            objGRHead1.TBB_Rate = isTBBRate;
                            objGRHead1.cmb_type = itruckcitywise;
                            objGRHead1.WagesLabel_Print = swages;
                            objGRHead1.Shipment_No = strShipmentNo;
                            objGRHead1.GRContanr_No = ContainerNum;
                            objGRHead1.GRContanr_Size = ContainerSize;
                            objGRHead1.GRContanr_Type = ContainerType;
                            objGRHead1.GRContanr_SealNo = ContainerSealNum;
                            objGRHead1.TypeId = intType;
                            objGRHead1.Type_Amnt = dblTypeAmnt;
                            objGRHead1.Ref_No = strRefNo;
                            objGRHead1.GRContanr_Port = strPortNum;
                            objGRHead1.ManualNo = ManualNo;
                            //objGRHead1.UserIdno = UserIdno;
                            objGRHead1.Consignor_Name = ConsName;
                            objGRHead1.GR_Frm = "BK"; objGRHead1.AgnstRcpt_No = Convert.ToString(RcptGoodHeadIdno) == null ? "" : Convert.ToString(RcptGoodHeadIdno);
                            objGRHead1.Chln_Idno = 0; objGRHead1.ChlnCrsng_Idno = 0; objGRHead1.Billed = false; objGRHead1.KisanKalyan_Amnt = KalyanTax;
                            objGRHead1.ServTax_Valid = dblTaxValid;
                            objGRHead1.ServTax_Perc = dblServTaxPerc;
                            objGRHead1.SwcgBrtTax_Perc = dblSwcgBrtTaxPerc;
                            objGRHead1.KisanKalyan_Per = dblKalyanTaxPer;
                            objGRHead1.TotItem_Value = TotItemValue;
                            objGRHead1.Ordr_No = strOrderNo;
                            objGRHead1.Form_No = strFormNo;
                            objGRHead1.GRContanr_No2 = Container_Num2;
                            objGRHead1.GRContanr_SealNo2 = SealNum_2;
                            objGRHead1.ImpExp_idno = ExpImp_id;
                            objGRHead1.ChaFrwdr_Name = CharFrwder_Name;
                            objGRHead1.DelPlace_Val = DelPlace;
                            objGRHead1.TypeDelPlace = TypeDelPlace;
                            objGRHead1.From_KM = dblFromKM;
                            objGRHead1.To_KM = dblToKM;
                            objGRHead1.Tot_KM = dblTotKM;
                            objGRHead1.DateModified = DateTime.Now;
                            //objGRHead1.Created_By=Created_By;
                            //Upadhyay #GST values
                            objGRHead1.SGST_Per = dSGST_Per;
                            objGRHead1.CGST_Per = dCGST_Per;
                            objGRHead1.IGST_Per = dIGST_Per;
                            objGRHead1.GSTCess_Per = dGSTCess_Per;
                            objGRHead1.SGST_Amt = dSGST_Amt;
                            objGRHead1.CGST_Amt = dCGST_Amt;
                            objGRHead1.IGST_Amt = dIGST_Amt;
                            objGRHead1.GSTCess_Amt = dGSTCess_Amt;
                            objGRHead1.GST_Idno = GST_Idno;
                            objGRHead1.User_ModifiedBy = Created_By;
                            objGRHead1.EWay_BillNO = strEwayBill;
                            objGRHead1.EGP_Date = dtEGPDate;
                            objGRHead1.Tax_InvNo = strTaxInvNo;
                            objGRHead1.Exc_InvNo = strExcInvNo;
                            objGRHead1.DI_Date = dtDIDate;
                            objGRHead1.Tax_Date = dtTaxInvDate;
                            db.SaveChanges();
                            intGrIdno = objGRHead1.GR_Idno;
                            if (intGrIdno > 0)
                            {
                                List<TblGrDetl> lstGrDetl = db.TblGrDetls.Where(obj => obj.GrHead_Idno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (TblGrDetl obj in lstGrDetl)
                                    {
                                        db.TblGrDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    TblGrDetl objGRDetl = new TblGrDetl();
                                    objGRDetl.GrHead_Idno = Convert.ToInt64(intGrIdno);
                                    objGRDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objGRDetl.ItemGrade_Idno = string.IsNullOrEmpty(Convert.ToString(row["Grade_Idno"])) ? 0 : Convert.ToInt32(row["Grade_Idno"]);
                                    objGRDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                    objGRDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["Rate_TypeIdno"])) ? 0 : Convert.ToInt32(row["Rate_TypeIdno"]);
                                    objGRDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                    objGRDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                    objGRDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objGRDetl.Amount = Convert.ToDouble(row["Amount"]);
                                    objGRDetl.Detail = Convert.ToString(row["Detail"]);
                                    objGRDetl.Shrtg_Limit = Convert.ToDouble(row["Shrtg_Limit"] == "" ? 0 : row["Shrtg_Limit"]);
                                    objGRDetl.Shrtg_Rate = Convert.ToDouble(row["Shrtg_Rate"] == "" ? 0 : row["Shrtg_Rate"]);
                                    objGRDetl.Shrtg_Limit_Other = Convert.ToDouble(row["Shrtg_Limit_Other"] == "" ? 0 : row["Shrtg_Limit_Other"]);
                                    objGRDetl.Shrtg_Rate_Other = Convert.ToDouble(row["Shrtg_Rate_Other"] == "" ? 0 : row["Shrtg_Rate_Other"]);
                                    objGRDetl.UnloadWeight = Convert.ToDouble(row["UnloadWeight"]);
                                    db.TblGrDetls.AddObject(objGRDetl);
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

        public Int64 InsertGRByExcel(DateTime? dtGRDate, Int32 IAgainst, Int32 intGRType, String DIno, String EGPNo, Int64 intGRNo, Int32 intSenderIdno, Int32 TruckNoIdno,
            Int32 intRecvrIDno, Int32 intFromcityIDno, Int32 intTocityIDno, Int32 intDelPlaceIdno, Int32 intAgentIdno, String strRemark, Int32 intServTaxPaidBy,
            Int32 intRcptypeIdno, String InstNo, DateTime? dtInstDate, Int32 intcustBankIdno, Double DGrossAmnt, Double DCommission, Double DTollTax,
            Double DCartage, Double DBilty, Double DSubTotal, Double DTotalAmnt, Double DWages, Double DServTax, Double DSwchBrtTax, Double DSurchrge,
            Double DPF, Double DNetAmnt, Double DRoundOffAmnt, Int32 YearIdno, Int64 RcptGoodHeadIdno, Int64 AdvOrderGR_Idno, Boolean isTBBRate,
            Int32 itruckcitywise, string swages, DataTable dtDetail, string strShipmentNo, string PreFixGRNum, string ContainerNum, Int64 ContainerSize,
            Int64 ContainerType, string ContainerSealNum, Int64 iCityviaIdno, Int64 intType, Double dblTypeAmnt, bool FromExcel, string strRefNo,
            string strPortNum, Double KalyanTax, string ManualNo, Int64 UserIdno, string ConsName, Double dblTaxValid, Double dblServTaxPerc,
            Double dblSwcgBrtTaxPerc, Double dblKalyanTaxPer, Double TotItemValue, string strOrderNo, string strFormNo, string Container_Num2, string SealNum_2,
            Int32 ExpImp_id, string CharFrwder_Name, string DelPlace, bool TypeDelPlace, double dblFromKM, double dblToKM, double dblTotKM, Int64 Created_By
            //,Double dSGST, Double dCGST, Double dIGST
            )
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead objGRHead = db.TblGrHeads.Where(rh => (rh.PrefixGr_No == PreFixGRNum) && (rh.Gr_No == intGRNo) && (rh.From_City == intFromcityIDno) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        objGRHead = new TblGrHead();
                        objGRHead.Gr_Date = dtGRDate;
                        objGRHead.GR_Agnst = IAgainst;
                        objGRHead.GR_Typ = intGRType;
                        objGRHead.DI_NO = DIno;
                        objGRHead.EGP_NO = EGPNo;
                        objGRHead.Gr_No = intGRNo;
                        objGRHead.PrefixGr_No = PreFixGRNum;
                        objGRHead.Lorry_Idno = TruckNoIdno;
                        objGRHead.Sender_Idno = intSenderIdno;
                        objGRHead.Recivr_Idno = intRecvrIDno;
                        objGRHead.From_City = intFromcityIDno;
                        objGRHead.To_City = intTocityIDno;
                        objGRHead.DelvryPlce_Idno = intDelPlaceIdno;
                        objGRHead.Agnt_Idno = intAgentIdno;
                        objGRHead.Remark = strRemark;
                        objGRHead.STax_Typ = intServTaxPaidBy;
                        objGRHead.RcptType_Idno = intRcptypeIdno;
                        objGRHead.Inst_No = InstNo;
                        objGRHead.Inst_Dt = dtInstDate;
                        objGRHead.Bank_Idno = intcustBankIdno;
                        objGRHead.Gross_Amnt = DGrossAmnt;
                        objGRHead.AgntComisn_Amnt = DCommission;
                        objGRHead.TollTax_Amnt = DTollTax;
                        objGRHead.Cartg_Amnt = DCartage;
                        objGRHead.Bilty_Amnt = DBilty;
                        objGRHead.SubTot_Amnt = DSubTotal;
                        objGRHead.Total_Amnt = DTotalAmnt;
                        objGRHead.Wages_Amnt = DWages;
                        objGRHead.ServTax_Amnt = DServTax;
                        objGRHead.SwchBrtTax_Amt = DSwchBrtTax;
                        objGRHead.Surcrg_Amnt = DSurchrge;
                        objGRHead.PF_Amnt = DPF;
                        objGRHead.Net_Amnt = DNetAmnt;
                        objGRHead.RndOff_Amnt = DRoundOffAmnt;
                        objGRHead.Year_Idno = YearIdno;
                        objGRHead.TBB_Rate = isTBBRate;
                        objGRHead.cmb_type = itruckcitywise;
                        objGRHead.WagesLabel_Print = swages;
                        objGRHead.Shipment_No = strShipmentNo;
                        objGRHead.Cityvia_Idno = iCityviaIdno;
                        objGRHead.AdvOrderGRHead_Idno = AdvOrderGR_Idno;
                        objGRHead.GRContanr_No = ContainerNum;
                        objGRHead.GRContanr_Size = ContainerSize;
                        objGRHead.GRContanr_Type = ContainerType;
                        objGRHead.GRContanr_SealNo = ContainerSealNum;
                        objGRHead.TypeId = intType;
                        objGRHead.FromExcel = FromExcel;
                        objGRHead.Type_Amnt = dblTypeAmnt;
                        objGRHead.Ref_No = strRefNo;
                        objGRHead.ManualNo = ManualNo;
                        objGRHead.UserIdno = UserIdno;
                        objGRHead.Consignor_Name = ConsName;
                        objGRHead.GRContanr_Port = strPortNum;
                        objGRHead.GR_Frm = "BK"; objGRHead.AgnstRcpt_No = Convert.ToString(RcptGoodHeadIdno) == null ? "" : Convert.ToString(RcptGoodHeadIdno);
                        objGRHead.Chln_Idno = 0; objGRHead.ChlnCrsng_Idno = 0; objGRHead.Billed = false; objGRHead.KisanKalyan_Amnt = KalyanTax;
                        objGRHead.ServTax_Valid = dblTaxValid;
                        objGRHead.ServTax_Perc = dblServTaxPerc;
                        objGRHead.SwcgBrtTax_Perc = dblSwcgBrtTaxPerc;
                        objGRHead.KisanKalyan_Per = dblKalyanTaxPer;
                        objGRHead.TotItem_Value = TotItemValue;
                        objGRHead.Ordr_No = strOrderNo;
                        objGRHead.Form_No = strFormNo;
                        objGRHead.GRContanr_No2 = Container_Num2;
                        objGRHead.GRContanr_SealNo2 = SealNum_2;
                        objGRHead.ImpExp_idno = ExpImp_id;
                        objGRHead.ChaFrwdr_Name = CharFrwder_Name;
                        objGRHead.DelPlace_Val = DelPlace;
                        objGRHead.TypeDelPlace = TypeDelPlace;
                        objGRHead.From_KM = dblFromKM;
                        objGRHead.To_KM = dblToKM;
                        objGRHead.Tot_KM = dblTotKM;
                        objGRHead.DateAdded = DateTime.Now;
                        objGRHead.DateModified = DateTime.Now;
                        objGRHead.Created_By = Created_By;
                        //objGRHead.SGST = dSGST;
                        //objGRHead.CGST = dCGST;
                        //objGRHead.IGST = dIGST;
                        db.TblGrHeads.AddObject(objGRHead);
                        db.SaveChanges();
                        intGrIdno = objGRHead.GR_Idno;
                        if (intGrIdno > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                TblGrDetl objGRDetl = new TblGrDetl();
                                objGRDetl.GrHead_Idno = Convert.ToInt64(intGrIdno);
                                objGRDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objGRDetl.ItemGrade_Idno = string.IsNullOrEmpty(Convert.ToString(row["Grade_Idno"])) ? 0 : Convert.ToInt32(row["Grade_Idno"]);
                                objGRDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                objGRDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["Rate_TypeIdno"])) ? 0 : Convert.ToInt32(row["Rate_TypeIdno"]);
                                objGRDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                objGRDetl.Tot_Weght = Convert.ToDouble(row["Weight"]);
                                objGRDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                objGRDetl.Amount = Convert.ToDouble(row["Amount"]);
                                objGRDetl.Detail = Convert.ToString(row["Detail"]);
                                objGRDetl.Shrtg_Limit = Convert.ToDouble(Convert.ToString(row["Shrtg_Limit"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Limit"]));
                                objGRDetl.Shrtg_Rate = Convert.ToDouble(Convert.ToString(row["Shrtg_Rate"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Rate"]));
                                objGRDetl.Shrtg_Limit_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Limit_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Limit_Other"]));
                                objGRDetl.Shrtg_Rate_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Rate_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Rate_Other"]));
                                db.TblGrDetls.AddObject(objGRDetl);
                                db.SaveChanges();
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

        public Double SelectCommission(Int64 ItemIdno, DateTime? dtDate, Int64 FromCityIdno, Int64 ToCityIdno, Int64 YearIdno)
        {
            Double intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    Int64 StateId = 0;
                    StateId = Convert.ToInt64((from mast in db.tblCityMasters where mast.City_Idno == ToCityIdno select mast.State_Idno).FirstOrDefault());

                    intValue = Convert.ToDouble((from CMH in db.tblCommmissionMastHeads
                                                 join CMD in db.tblCommmissionMastDetls on CMH.Head_Idno equals CMD.Head_Idno
                                                 where CMH.Item_Idno == ItemIdno && CMH.FromCity_Idno == FromCityIdno && CMD.Tocity_Idno == ToCityIdno && CMH.Year_Idno == YearIdno && CMH.State_Idno == StateId
                                                 select CMD.Comsn_Amnt).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {

            }
            return intValue;
        }
        public Int64 InsertInComMaster(Int64 ItemIdno, string ComType, DateTime? dtDate, Int64 FromCityIdno, Int64 ToCityIdno, Int64 YearIdno, Double ComsnAmnt)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    Int64 StateId = 0;
                    StateId = Convert.ToInt64((from mast in db.tblCityMasters where mast.City_Idno == ToCityIdno select mast.State_Idno).FirstOrDefault());
                    tblCommmissionMastHead obj1 = new tblCommmissionMastHead();
                    obj1.Item_Idno = ItemIdno;
                    obj1.Com_Type = ComType;
                    obj1.Date = dtDate;
                    obj1.FromCity_Idno = FromCityIdno;
                    obj1.State_Idno = StateId;
                    obj1.Year_Idno = YearIdno;
                    Int64 HeadIdno = IsExists(ItemIdno, ComType, dtDate, FromCityIdno, StateId, YearIdno, ToCityIdno);
                    if (HeadIdno > 0)
                    {
                        DeleteFromComMastHead(HeadIdno);
                        DeleteFromComMastDetl(HeadIdno);

                        db.tblCommmissionMastHeads.AddObject(obj1);
                        db.SaveChanges();
                        intValue = obj1.Head_Idno;

                        tblCommmissionMastDetl obj2 = new tblCommmissionMastDetl();
                        obj2.Head_Idno = intValue;
                        obj2.Tocity_Idno = ToCityIdno;
                        obj2.Comsn_Amnt = ComsnAmnt;
                        db.tblCommmissionMastDetls.AddObject(obj2);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.tblCommmissionMastHeads.AddObject(obj1);
                        db.SaveChanges();
                        intValue = obj1.Head_Idno;

                        tblCommmissionMastDetl obj2 = new tblCommmissionMastDetl();
                        obj2.Head_Idno = intValue;
                        obj2.Tocity_Idno = ToCityIdno;
                        obj2.Comsn_Amnt = ComsnAmnt;
                        db.tblCommmissionMastDetls.AddObject(obj2);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }
        public Int64 IsExists(Int64 ItemIdno, string ComType, DateTime? dtDate, Int64 FromCityIdno, Int64 StateIdno, Int64 YearIdno, Int64 ToCityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCommmissionMastHead obj1 = new tblCommmissionMastHead();
                obj1 = (from CMH in db.tblCommmissionMastHeads
                        join CD in db.tblCommmissionMastDetls on CMH.Head_Idno equals CD.Head_Idno
                        where CMH.Item_Idno == ItemIdno && CMH.Com_Type == ComType && CMH.Date == dtDate && CMH.FromCity_Idno == FromCityIdno && CMH.State_Idno == StateIdno && CMH.Year_Idno == YearIdno
                        && CD.Tocity_Idno == ToCityIdno
                        select CMH).FirstOrDefault();

                if (obj1 != null)
                {
                    return obj1.Head_Idno;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int DeleteFromComMastHead(Int64 ComHeadIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCommmissionMastHead objComMastHead = (from mast in db.tblCommmissionMastHeads
                                                             where mast.Head_Idno == ComHeadIdno
                                                             select mast).FirstOrDefault();
                    if (objComMastHead != null)
                    {
                        db.tblCommmissionMastHeads.DeleteObject(objComMastHead);
                        db.SaveChanges();
                        intValue = 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                {
                    intValue = -1;
                }
            }
            return intValue;
        }

        public int DeleteFromComMastDetl(Int64 ComHeadIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCommmissionMastDetl objComHeadDetl = (from mast in db.tblCommmissionMastDetls
                                                             where mast.Head_Idno == ComHeadIdno
                                                             select mast).FirstOrDefault();
                    if (objComHeadDetl != null)
                    {
                        db.tblCommmissionMastDetls.DeleteObject(objComHeadDetl);
                        db.SaveChanges();
                        intValue = 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                {
                    intValue = -1;
                }
            }
            return intValue;
        }

        public Int64 SelectLorryType(Int64 LorryIdno)
        {
            Int64 LorryType = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                LorryType = Convert.ToInt64((from obj in db.LorryMasts where obj.Lorry_Idno == LorryIdno select obj.Lorry_Type).FirstOrDefault());
                return LorryType;
            }
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
        #endregion

        #region SELECT STATEMENTS
        public DataTable DsTrAcnt(string con)
        {
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT Acnt_Idno AS 'TransportAccountID' FROM ACNTMAST WHERE ACNT_NAME='Transport Charges' AND INTERNAL=1";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable DtAcntDS(string con)
        {
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno, ISNULL(SwachBharat_Idno,0) SwachBharat_Idno, ISNULL(KrishiKalyan_Idno,0) KrishiKalyan_Idno,ISNULL(Sgst_Idno,0) as Sgst_Idno,Isnull(Cgst_Idno,0) as Cgst_Idno,Isnull(Igst_Idno,0) as Igst_Idno FROM tblAcntLink";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
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
        public double SelectWghtShrtgRate(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectWghtShrtgLimit(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgRate(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgLimit(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Cityvia_Idno == CityviaIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.QtyShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectRatePartyWise(Int32 ItemIdno, Int32 TocityIdno, Int32 LocationIdno, DateTime Grdate, Int64 CityviaIdno,Int32 PartyIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblPartyRateMasts where RM.Party_Idno == PartyIdno && RM.Item_Idno == ItemIdno && RM.Loc_Idno == LocationIdno && RM.FrmCity_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.PRate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblPartyRateMasts where RM.Party_Idno == PartyIdno && RM.Item_Idno == ItemIdno && RM.Loc_Idno == LocationIdno && RM.FrmCity_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.Rate_Date <= Grdate select RM.PRate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblPartyRateMasts where RM.PRate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectItemRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Cityvia_Idno == CityviaIdno && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Cityvia_Idno == CityviaIdno && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectServiceTaxFromTaxMaster(Int64 StateIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 1 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 1 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }
        public double SelectSwacchBrtTaxFromTaxMaster(Int64 StateIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 3 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 3 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        public double SelectItemWghtRateForTBBContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno,double Weight,int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectWghtShrtgRateContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno, double Weight, int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();            
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectWghtShrtgLimitContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno, double Weight, int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();        
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
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
        public IList SelectGR(int GrNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, Int32 yearidno, Int64 UserIdno, string strGrPrefix)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.TblGrHeads
                           join LR in db.LorryMasts on hd.Lorry_Idno equals LR.Lorry_Idno 
                           join cifrom in db.tblCityMasters on hd.From_City equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.To_City equals cito.City_Idno
                           join cidVia in db.tblCityMasters on hd.DelvryPlce_Idno equals cidVia.City_Idno into Cidl 
                           from mappingsCidl in Cidl.DefaultIfEmpty()
                           join cidVia in db.tblCityMasters on hd.Cityvia_Idno equals cidVia.City_Idno into ViaCity 
                           from mappingsViaCity in ViaCity.DefaultIfEmpty()
                           //join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           join acntss in db.AcntMasts on hd.Recivr_Idno equals acntss.Acnt_Idno
                           where hd.GR_Frm == "BK"
                           select new
                           {
                               hd.PrefixGr_No,
                               hd.DelvryPlce_Idno,
                               hd.From_City,
                               hd.To_City,
                               hd.Gr_Date,
                               hd.GR_Idno,
                               hd.Gr_No,
                               hd.Sender_Idno,
                               hd.Remark,
                               hd.Shipment_No,
                               GR_Typ = ((hd.GR_Typ == 1) ? "Paid GR" : (hd.GR_Typ == 2) ? "TBB GR" : "To Pay GR"),
                               hd.Recivr_Idno,
                               hd.Gross_Amnt,
                               hd.Net_Amnt,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityVia = mappingsViaCity.City_Name,
                               CityDely = mappingsCidl.City_Name,
                               //Sender = acnts.Acnt_Name,
                               Sender=hd.Consignor_Name,
                               Receiver = acntss.Acnt_Name,
                               Year_Idno = hd.Year_Idno,
                               Lorry_No=LR.Lorry_No,
                               Owner_Name= LR.Owner_Name,
                               hd.Tot_KM,
                               Qty = ((from N in db.TblGrDetls  where N.GrHead_Idno == hd.GR_Idno select N.Qty).Sum())
                               //Qty = 1
                               
                           }).ToList();
                if (GrNo > 0)
                {
                    lst = lst.Where(l => l.Gr_No == GrNo).ToList();
                }
                if (strGrPrefix != null && strGrPrefix != "")
                {
                    lst = lst.Where(l => l.PrefixGr_No.Contains(strGrPrefix)).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Gr_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
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
                if (cityfrom > 0)
                {
                    lst = lst.Where(l => l.From_City == cityfrom).ToList();
                }
                else if (UserIdno > 0)
                {
                    var CityLst = db.tblFrmCityDetls.Where(U => U.User_Idno == UserIdno).Select(p => p.FrmCity_Idno).ToList();
                    lst = lst.Where(o => CityLst.Contains(o.From_City)).ToList();
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
        public double SelectKalyanTaxFromTaxMaster(Int64 StateIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 4 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 4 && TM.State_Idno == StateIdno && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        //Upadhyay #GST
        public double SelectSGSTMaster(DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 5 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 5 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        //Upadhyay #GST
        public double SelectCGSTMaster(DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 6 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 6 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        //Upadhyay #GST
        public double SelectIGSTMaster(DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 7 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters where TM.TaxTyp_Idno == 7 && TM.Tax_Date <= Grdate select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        public DataTable SelectGRDetail(int GrIDNO,string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action","SelectGRDetail");
                objSqlPara[1] = new SqlParameter("@Id", GrIDNO);
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
        }
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
        public DataTable SelectRECPTGrDetails(string con, Int64 iYearId, string AllItmIdno, string Action)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
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
        public tblCityMaster GetStateIdno(Int32 cityidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster lst = (from cm in db.tblCityMasters where cm.City_Idno == cityidno orderby cm.City_Name ascending select cm).FirstOrDefault();
                return lst;
            }
        }
        #endregion

        #region FUNCTIONS

        public Int64 AutofillYear()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 yearIdno = 0, GrIdno = 0;
                GrIdno = Convert.ToInt64((from a in db.TblGrHeads select a.GR_Idno).Max());
                yearIdno = Convert.ToInt64((from a in db.TblGrHeads where a.GR_Idno == GrIdno select a.Year_Idno).FirstOrDefault());
                return yearIdno;
            }
        }
        public IList ItemGrade()
        {
             using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
             {
                 var grade = (from IG in db.tblGradeMasters select new { IG.Grade_Idno,IG.Grade_Name }).ToList();
                 return grade;
             }
         }
        public String AutofillDate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string Date = ""; Int64 GrIdno = 0;
                GrIdno = Convert.ToInt64((from a in db.TblGrHeads select a.GR_Idno).Max());
                Date = string.IsNullOrEmpty(Convert.ToString((from a in db.TblGrHeads where a.GR_Idno == GrIdno select a.Gr_Date).FirstOrDefault())) ? "" : Convert.ToDateTime((from a in db.TblGrHeads where a.GR_Idno == GrIdno select a.Gr_Date).FirstOrDefault()).ToString("dd-MM-yyyy");
                return Date;
            }
        }
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
                var temp = (from a in db.TblGrHeads
                            join b in db.tblChlnBookHeads on a.Chln_Idno equals b.Chln_Idno
                            where a.GR_Idno == GRHeadIdno && a.Chln_Idno > 0
                            select b.Chln_No).FirstOrDefault();
                if (temp != null)
                {
                    ChallanNo = Convert.ToInt64(temp);
                }
                else
                {
                    ChallanNo = 0;
                }
                return ChallanNo;
            }
        }
        public Int64 CheckChallanDetailsForPayment(Int64 GRHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 ChallanNo = 0;
                var temp = (from a in db.TblGrHeads
                            join b in db.tblChlnBookHeads on a.Chln_Idno equals b.Chln_Idno
                            join LM in db.LorryMasts on a.Lorry_Idno equals LM.Lorry_Idno
                            where a.GR_Idno == GRHeadIdno && a.Chln_Idno > 0 && LM.Lorry_Type == 0
                            select b.Chln_No).FirstOrDefault();
                if (temp != null)
                {
                    ChallanNo = Convert.ToInt64(temp);
                }
                else
                {
                    ChallanNo = 0;
                }
                return ChallanNo;
            }
        }
        public IList CheckDuplicateGrNo(Int64 intGrNo,string strDIno ,string strInvNo ,Int32 FromCityIdno, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from m in db.TblGrHeads
                           where m.Gr_No == intGrNo  && m.DI_NO == strDIno && m.Tax_InvNo == strInvNo &&  m.Year_Idno == intYearIdno && m.GR_Frm == "BK" && m.From_City == FromCityIdno
                           select new
                           {
                               m.Gr_No,
                           }).ToList();
                return lst;
            }
        }
        public Int64 MaxNo(string GRfrom, Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GR_NO),0) + 1 AS MAXID FROM TBLGRHEAD WHERE GR_Frm='" + GRfrom + "' AND From_City='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public Int64 MaxIdno(string con,Int64 FronCityIdno)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GR_Idno),0)  AS MAXID FROM TBLGRHEAD WHERE FROM_CITY=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        #endregion

        public DataTable GrCSVReport1(Int64 GRIdno, Int32 GRType, string con)
        {
            sqlSTR = string.Empty;
            if (GRType == 2)
            {
                sqlSTR = @" SELECT CH.Chln_Date,CH.Chln_No,CONVERT(NUMERIC(25,2),CH.Adv_Amnt) Adv_Amnt,CONVERT(NUMERIC(25,2),CH.TDSTax_Amnt) TDSTax_Amnt,
                            CONVERT(NUMERIC(25,2),CH.Net_Amnt) Net_Amnt,CASE WHEN (CD.Delivered=1) THEN 'Yes' Else 'No' END Delivered,
                            CASE WHEN (CD.Delivered=1) THEN (CONVERT(NVARCHAR(10), CD.Delvry_Date, 120)) ELSE '' END Delvry_Date,LM.Lorry_No
                            FROM tblChlnBookHead CH join tblChlnBookDetl CD on CH.Chln_Idno=CD.ChlnBookHead_Idno 
                            join LorryMast LM on CH.Truck_Idno=LM.Lorry_Idno where CD.GR_Idno='" + GRIdno + "' ";
                sqlSTR = sqlSTR + @"UNION ALL SELECT IH.Inv_Date,IH.Inv_No,CONVERT(NUMERIC(25,2),0) Adv_Amnt,CONVERT(NUMERIC(25,2),IH.ServTax_Amnt) TDSTax_Amnt,
                            CONVERT(NUMERIC(25,2),IH.Net_Amnt) Net_Amnt,''Delivered,'' Delvry_Date,''Owner_Name
                            FROM tblInvGenHead IH join tblInvGenDetl ID on IH.Inv_Idno=ID.InvGenHead_Idno
                            where ID.GR_Idno='" + GRIdno + "'";
            }
            else
            {
                sqlSTR = @"SELECT CH.Chln_Date,CH.Chln_No,CONVERT(NUMERIC(25,2),CH.Adv_Amnt) Adv_Amnt,CONVERT(NUMERIC(25,2),CH.TDSTax_Amnt) TDSTax_Amnt,
	                         CONVERT(NUMERIC(25,2),CH.Net_Amnt) Net_Amnt, CASE WHEN (CD.Delivered=1) THEN 'Yes' Else 'No' END Delivered,
                             CASE WHEN (CD.Delivered=1) THEN (CONVERT(NVARCHAR(20), CD.Delvry_Date, 120)) ELSE '' END Delvry_Date,LM.Lorry_No
                             FROM tblChlnBookHead CH join tblChlnBookDetl CD on CH.Chln_Idno=CD.ChlnBookHead_Idno 
                             join LorryMast LM on CH.Truck_Idno=LM.Lorry_Idno where CD.GR_Idno='" + GRIdno + "'  ";
            }
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public Int32 SelectGrTpe(Int32 GrIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 GrType = Convert.ToInt32((from AM in db.TblGrHeads where AM.GR_Idno == GrIdno select AM.GR_Typ).FirstOrDefault());
                return GrType;
            }
        }

        public Int32 SelectChlnIdno(Int32 GrIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 ChlnIdno = Convert.ToInt32((from AM in db.TblGrHeads where AM.GR_Idno == GrIdno select AM.Chln_Idno).FirstOrDefault());
                return ChlnIdno;
            }
        }

        #region BindContainerDetails
        public IList GetContainerType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblContainerTypes where obj.Status == true select obj).ToList();
            }
        }

        public IList GetContainerSize()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblContainerSizes where obj.Status == true select obj).ToList();
            }
        }
        #endregion

        #region ExcelUploding (BY PEEYUSH KAUSHIK)...

        public int TurncatetblGrUploadFromExcel(string ConString)
        {
            string str = string.Empty;
            str = @"TRUNCATE TABLE tblGrUploadFromExcel";
            int result = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return result;
        }
        public int UpdateFlag(string ConString,Int64 GrIdno)
        {
            string str = string.Empty;
            str = @"Update tblGrUploadFromExcel SET Error_Flag=1 WHERE GR_Idno=" + GrIdno + "";
            int result = SqlHelper.ExecuteNonQuery(ConString, CommandType.Text, str);
            return result;
        }
        public DataTable DsErrorExcel(string con)
        {
            sqlSTR = string.Empty;
            sqlSTR = @"SELECT [PrefixGr_No] AS  PerfNo ,[Gr_No] AS GRNo ,CONVERT(NVARCHAR,[Gr_Date],105) AS GRDate ,[GR_Type] AS GrType ,[From_City] AS FromCity ,[To_City] AS ToCity ,[Lorry_Name] AS TruckNo ,[Item_Name] AS ItemName ,[Unit] AS Unit,[Rate_Type] AS RateType ,[Qty] AS Qty,[Item_Weight] AS [Weight],[Item_Rate] AS Rate ,[Detail] AS ItemDetails,[Sender_Name] AS Sender  ,[Recivr_Name] AS Receiver,[DelvryPlce_Name] AS DeliveryPlace ,[Cityvia_Name] AS ViaCity,[DI_NO] AS DlNo ,[EGP_NO] AS EGPNo ,[Shipment_No] AS ShipmentNo ,[Remark] AS Remark,[FixedAmount] AS FixedAmount,ISNULL([ReceiptType],'') AS ReceiptType,ISNULL([InstNo],'') AS InstNo, (CASE WHEN (CONVERT(DateTime, ISNULL([InstDate],''),105)= '1900-01-01 00:00:00.000') THEN '' ELSE (CAST([InstDate] AS Nvarchar)) END)  AS InstDate,ISNULL([CustBank],'') AS CustBank, ISNULL([Ref_No],'') AS RefNo,ISNULL([Ordr_No],'') AS OrderNo ,ISNULL([Form_No],'') AS FormNo ,ISNULL([Unloading],'') AS Unloading,ISNULL(From_CityIdno,0) FromCityIdno ,ISNULL(To_CityIdno,0) ToCityIdno, ISNULL(Lorry_Idno,0) LorryIdno, ISNULL(Item_Idno,0) ItemIdno,ISNULL(Unit_Idno,0) UnitIdno,ISNULL(Sender_Idno,0) SenderIdno, ISNULL(Recivr_Idno,0) RecivrIdno, ISNULL(DelvryPlce_Idno,0)DelvryPlceIdno,ISNULL(Exists_Flag,'') ExistsFlag FROM [tblGrUploadFromExcel] WHERE Error_Flag = 0";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public Int64 InsertInGrExcel(Int64 intGrNo, string strPrefNo, DateTime? strGrDate, string strGrType, string strFromCity, string strToCity, string strLorryName, string strItemName, string strUnit, string strRateType, Int64 intQty, double intWeight, double intItemRate, string strDetail, string strSenderName, string strRecivedName, string strDelvPlaceName, string strViaCity, string strDINo, string strEgpNo, string strshipmentNo, string strRemark, string FixedAmnt, string strReceiptType, string strInstNo, DateTime? dtInstDate, string strCustBank, Int64 intYearIdNo, int intCompIdNo, Int64 intUserIdNo, string RefNo, string strOrderNo, string strFormNo, double dUnloading)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 value = 0;
                try
                {
                    tblGrUploadFromExcel Obj = new tblGrUploadFromExcel();
                    Obj.Gr_No = intGrNo;
                    Obj.PrefixGr_No = strPrefNo;
                    Obj.Gr_Date = strGrDate;
                    Obj.GR_Type = strGrType;
                    Obj.From_City = strFromCity;
                    Obj.To_City = strToCity;
                    Obj.Lorry_Name = strLorryName;
                    Obj.Item_Name = strItemName;
                    Obj.Unit = strUnit;
                    Obj.Rate_Type = strRateType;
                    Obj.Qty = intQty;
                    Obj.Item_Weight = intWeight;
                    Obj.Item_Rate = intItemRate;
                    Obj.Detail = strDetail;
                    Obj.Sender_Name = strSenderName;
                    Obj.Recivr_Name = strRecivedName;
                    Obj.DelvryPlce_Name = strDelvPlaceName;
                    Obj.Cityvia_Name = strViaCity;
                    Obj.DI_NO = strDINo;
                    Obj.EGP_NO = strEgpNo;
                    Obj.Shipment_No = strshipmentNo;
                    Obj.Remark = strRemark;
                    Obj.FixedAmount = FixedAmnt;
                    Obj.ReceiptType = strReceiptType;
                    Obj.InstNo = strInstNo;
                    Obj.InstDate = dtInstDate;
                    Obj.CustBank = strCustBank;
                    Obj.Year_Idno = intYearIdNo;
                    Obj.CompId = intCompIdNo;
                    Obj.UserIdno = intUserIdNo;
                    Obj.Error_Flag = false;
                    Obj.Ref_No = RefNo;
                    Obj.Ordr_No = strOrderNo;
                    Obj.Form_No = strFormNo;
                    Obj.Unloading = dUnloading;
                    db.AddTotblGrUploadFromExcels(Obj);
                    db.SaveChanges();
                    value = Obj.GR_Idno;
                }
                catch (Exception Exe)
                {
                    value = -1;
                }
                return value;
            }
        }
        public DataTable SelectTempTable(string con, string prefNo, string GrNo, string CityIdno, Int64 YearIdno, string Action)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@FROMCITY", CityIdno);
            objSqlPara[2] = new SqlParameter("@YEARIDNO", YearIdno);
            objSqlPara[3] = new SqlParameter("@GRNO", GrNo);
            objSqlPara[4] = new SqlParameter("@PREFNO", prefNo);
            DataSet objDsTemp1 = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrepUploadExcel", objSqlPara);
            DataTable objDtTemp1 = new DataTable();
            if (objDsTemp1.Tables.Count > 0)
            {
                if (objDsTemp1.Tables[0].Rows.Count > 0)
                {
                    objDtTemp1 = objDsTemp1.Tables[0];
                }
            }
            return objDtTemp1;
        }
        public DataTable SelectTableByGrNo(string con, string prefNo, string GrNo, string CityName, Int64 YearIdno,string Action)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@FROMCITYNAME", CityName);
            objSqlPara[2] = new SqlParameter("@YEARIDNO", YearIdno);
            objSqlPara[3] = new SqlParameter("@GRNO", GrNo);
            objSqlPara[4] = new SqlParameter("@PREFNO", prefNo);
            DataSet objDsTemp2 = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrepUploadExcel", objSqlPara);
            DataTable objDtTemp2 = new DataTable();
            if (objDsTemp2.Tables.Count > 0)
            {
                if (objDsTemp2.Tables[0].Rows.Count > 0)
                {
                    objDtTemp2 = objDsTemp2.Tables[0];
                }
            }
            return objDtTemp2;
        }
        public DataTable SelectDistinctFromExcel(string con,Int32 Type,string Action)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@Type", Type);
            DataSet objDsTemp3 = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrepUploadExcel", objSqlPara);
            DataTable objDtTemp3 = new DataTable();
            if (objDsTemp3.Tables.Count > 0)
            {
                if (objDsTemp3.Tables[0].Rows.Count > 0)
                {
                    objDtTemp3 = objDsTemp3.Tables[0];
                }
            }
            return objDtTemp3;
        }

        #region Comment By Peeyush....................

//        public DataTable SelectTempTable(string con, string prefNo, string GrNo, string CityIdno, Int64 YearIdno)
//        {
//            sqlSTR = string.Empty;
//            sqlSTR = @"SELECT Gr_Idno,GR_NO,PrefixGr_No,GR_DATE, CASE WHEN RTRIM(LTRIM(UPPER(GR_TYPE)))='PAID GR' THEN 1 WHEN RTRIM(LTRIM(UPPER(GR_TYPE)))='TBB GR' THEN 2 WHEN RTRIM(LTRIM(UPPER(GR_TYPE)))='TO PAY GR' THEN 3 ELSE 1  END AS GR_TYPE ,CMFROM.City_Idno AS From_CityIdNo,CMFROM.City_Name AS From_CityName,CMTO.City_Idno AS To_CityIdNo,CMTO.City_Name AS To_CityName,LM.Lorry_Idno AS Lorry_Idno,LM.Lorry_No AS Lorry_No,IM.Item_idno AS Item_IdNo,IM.Item_Name AS Item_Name,UOM.UOM_Idno AS UOM_IdNo,UOM.UOM_Name AS UOM_Name,RTRIM(LTRIM(UPPER(Rate_Type))) AS Rate_TypeName,CASE WHEN RTRIM(LTRIM(UPPER(Rate_Type)))='RATE' THEN 1 WHEN RTRIM(LTRIM(UPPER(Rate_Type)))='WEIGHT' THEN 2 ELSE 1  END AS Rate_Type,ISNULL(QTY,0) AS QTY, ISNULL(Item_Weight,0) AS Item_Weight,ISNULL(Item_Rate,0) AS Item_Rate,ISNULL(Detail,'') AS Detail,ISNULL(AMSENDER.Acnt_Name,'') AS Sender_Name,ISNULL(AMSENDER.Acnt_Idno,0) AS Sender_Idno,ISNULL(AMRECIVER.Acnt_Name,'') AS Recivr_Name,ISNULL(AMRECIVER.Acnt_Idno,0) AS Recivr_Idno,ISNULL(CMDEL.City_Name,'') AS DelvryPlce_Name,ISNULL(CMDEL.City_Idno,0) AS DelvryPlce_Idno,ISNULL(CMVIA.City_Name,'') AS Cityvia_Name,ISNULL(CMVIA.City_Idno,0) AS Cityvia_Idno,ISNULL(DI_NO,'') AS DI_NO,ISNULL(EGP_NO,'') AS EGP_NO,ISNULL(Shipment_No,'') AS Shipment_No,ISNULL(Remark,'') AS Remark,ISNULL(FixedAmount,0) AS FixedAmount
//                        FROM tblGrUploadFromExcel AS EXL INNER JOIN tblCityMaster AS CMFROM ON LTRIM(RTRIM(UPPER(EXL.From_City)))=LTRIM(RTRIM(UPPER(CMFROM.City_Name))) AND CMFROM.ASLOCATION=1 INNER JOIN tblCityMaster AS CMTO ON LTRIM(RTRIM(UPPER(EXL.To_City)))=LTRIM(RTRIM(UPPER(CMTO.City_Name))) INNER JOIN LORRYMAST AS LM ON LTRIM(RTRIM(UPPER(EXL.Lorry_Name)))=LTRIM(RTRIM(UPPER(LM.Lorry_No))) AND LM.STATUS=1 INNER JOIN ItemMast AS IM ON LTRIM(RTRIM(UPPER(EXL.Item_Name)))=LTRIM(RTRIM(UPPER(IM.Item_Name))) INNER JOIN UOMMast AS UOM ON LTRIM(RTRIM(UPPER(EXL.UNIT)))=LTRIM(RTRIM(UPPER(UOM.UOM_Name))) INNER JOIN AcntMast AS AMSENDER ON LTRIM(RTRIM(UPPER(EXL.Sender_Name)))=LTRIM(RTRIM(UPPER(AMSENDER.Acnt_Name))) AND AMSENDER.Acnt_Type=2 AND AMSENDER.INTERNAL=0 AND AMSENDER.Status=1 INNER JOIN AcntMast AS AMRECIVER ON LTRIM(RTRIM(UPPER(EXL.Recivr_Name)))=LTRIM(RTRIM(UPPER(AMRECIVER.Acnt_Name))) AND AMRECIVER.Acnt_Type=2 AND AMRECIVER.INTERNAL=0 AND AMRECIVER.Status=1 LEFT OUTER JOIN tblCityMaster AS CMDEL ON LTRIM(RTRIM(UPPER(EXL.To_City)))=LTRIM(RTRIM(UPPER(CMDEL.City_Name)))  LEFT OUTER JOIN tblCityMaster AS CMVIA ON LTRIM(RTRIM(UPPER(EXL.To_City)))=LTRIM(RTRIM(UPPER(CMVIA.City_Name))) WHERE GR_Type IN ('PAID GR','TBB GR','TO PAY GR') AND GR_DATE<>''AND (PrefixGr_No+''+CONVERT(NVARCHAR,GR_NO)) NOT IN  (SELECT (PrefixGr_No+''+CONVERT(NVARCHAR,GR_NO)) AS GR_NO FROM TblGrHead WHERE From_City='" + CityIdno + "' AND YEAR_IDNO='" + YearIdno + "'AND GR_NO='" + GrNo + "' AND PrefixGr_No='" + prefNo + "') AND CMFROM.City_Idno='" + CityIdno + "' AND EXL.Year_Idno='" + YearIdno + "' AND EXL.GR_NO='" + GrNo + "' AND PrefixGr_No='" + prefNo + "' ORDER BY GR_NO,PrefixGr_No,GR_DATE,From_CityIdNo";
//            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
//            DataTable dt = new DataTable();
//            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            {
//                dt = ds.Tables[0];
//            }
//            return dt;
        //        }

        
        //public DataTable SelectTableByGrNo(string con, string prefNo, string GrNo, string CityName, Int64 YearIdno)
        //{
        //    sqlSTR = string.Empty;
        //    sqlSTR = @"SELECT * FROM tblGrUploadFromExcel WHERE PrefixGr_No='" + prefNo + "' AND Gr_No='" + GrNo + "'AND From_City='" + CityName + "' AND Year_Idno=" + YearIdno + "";
        //    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
        //    DataTable dt = new DataTable();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    return dt;
        //}

        //public DataTable SelectDistinctFromExcel(string con,Int32 Type)
        //{
        //    sqlSTR = string.Empty;
        //    if (Type == 2) { sqlSTR = "SELECT ISNULL(LTRIM(RTRIM(EXL.PrefixGr_No)),'') AS PrefixGr_No,Convert(NVARCHAR,Gr_Date,105) AS Gr_Date,LTRIM(RTRIM(EXL.GR_No)) AS GR_No,CMFROM.City_Idno AS City_Idno,CMFROM.City_Name FROM tblGrUploadFromExcel AS EXL INNER JOIN tblCityMaster AS CMFROM ON LTRIM(RTRIM(UPPER(EXL.From_City)))=LTRIM(RTRIM(UPPER(CMFROM.City_Name))) AND CMFROM.ASLOCATION=1 INNER JOIN tblCityMaster AS CMTO ON LTRIM(RTRIM(UPPER(EXL.To_City)))=LTRIM(RTRIM(UPPER(CMTO.City_Name)))  INNER JOIN LORRYMAST AS LM ON LTRIM(RTRIM(UPPER(EXL.Lorry_Name)))=LTRIM(RTRIM(UPPER(LM.Lorry_No))) AND LM.STATUS=1 INNER JOIN ItemMast AS IM ON LTRIM(RTRIM(UPPER(EXL.Item_Name)))=LTRIM(RTRIM(UPPER(IM.Item_Name))) INNER JOIN UOMMast AS UOM ON LTRIM(RTRIM(UPPER(EXL.UNIT)))=LTRIM(RTRIM(UPPER(UOM.UOM_Name))) WHERE EXL.FixedAmount>0 GROUP BY EXL.PrefixGr_No ,Gr_Date,EXL.GR_No,CMFROM.City_Idno,CMFROM.City_Name ORDER BY EXL.PrefixGr_No,EXL.Gr_Date,EXL.GR_No,CMFROM.City_Idno"; }
        //    else { sqlSTR = @"SELECT ISNULL(LTRIM(RTRIM(EXL.PrefixGr_No)),'') AS PrefixGr_No,Convert(NVARCHAR,Gr_Date,105) AS Gr_Date,LTRIM(RTRIM(EXL.GR_No)) AS GR_No,CMFROM.City_Idno AS City_Idno,CMFROM.City_Name FROM tblGrUploadFromExcel AS EXL INNER JOIN tblCityMaster AS CMFROM ON LTRIM(RTRIM(UPPER(EXL.From_City)))=LTRIM(RTRIM(UPPER(CMFROM.City_Name))) AND CMFROM.ASLOCATION=1 INNER JOIN tblCityMaster AS CMTO ON LTRIM(RTRIM(UPPER(EXL.To_City)))=LTRIM(RTRIM(UPPER(CMTO.City_Name))) INNER JOIN LORRYMAST AS LM ON LTRIM(RTRIM(UPPER(EXL.Lorry_Name)))=LTRIM(RTRIM(UPPER(LM.Lorry_No))) AND LM.STATUS=1 INNER JOIN ItemMast AS IM ON LTRIM(RTRIM(UPPER(EXL.Item_Name)))=LTRIM(RTRIM(UPPER(IM.Item_Name))) INNER JOIN UOMMast AS UOM ON LTRIM(RTRIM(UPPER(EXL.UNIT)))=LTRIM(RTRIM(UPPER(UOM.UOM_Name))) GROUP BY EXL.PrefixGr_No ,Gr_Date,EXL.GR_No,CMFROM.City_Idno,CMFROM.City_Name ORDER BY EXL.PrefixGr_No,EXL.Gr_Date,EXL.GR_No,CMFROM.City_Idno"; }
        //    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
        //    DataTable dt = new DataTable();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    return dt;
        //}
            #endregion
        public Int64 UpdateRemarkData(Int64 intGRIdno, string Remark)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead objGRHead = db.TblGrHeads.Where(rh => (rh.GR_Idno == intGRIdno)).FirstOrDefault();
                    if (objGRHead != null)
                    {
                        {
                            objGRHead.Remark = Remark.Trim();
                            db.SaveChanges();
                            intGrIdno = objGRHead.GR_Idno;
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
                return intGrIdno;
            }
            
        }
        public Int64 UpdateData(Int64 intGRIdno,string No,Int32 Type)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead objGRHead = db.TblGrHeads.Where(rh => (rh.GR_Idno == intGRIdno)).FirstOrDefault();
                    if (objGRHead != null)
                    {
                        if (string.IsNullOrEmpty(No) == false)
                        {
                            if (Type == 1)          //1 - DINo 
                            {
                                objGRHead.DI_NO = No;
                            }
                            else if (Type == 2)     // 2 -EGP No
                            {
                                objGRHead.EGP_NO = No;
                            }
                            else if (Type == 3)     // 2 -Ref No
                            {
                                objGRHead.Ref_No = No;
                            }
                            else if (Type == 4)     // 2 -Manual No
                            {
                                objGRHead.ManualNo = No;
                            }
                            else if (Type == 5)     // 2 -Ordr No
                            {
                                objGRHead.Ordr_No = No;
                            }
                            else if (Type == 6)     // 2 -Form No
                            {
                                objGRHead.Form_No = No;
                            }
                            db.SaveChanges();
                            intGrIdno = objGRHead.GR_Idno;
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
        #endregion

        public void UpdateIsPosting(Int64 Gr_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    TblGrHead GH=(from G in db.TblGrHeads where G.GR_Idno==Gr_Idno select G).FirstOrDefault();
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

        //GET SENDER MOBILE NUMBER
        public string GetPartyMobileNoBySenderIdno(Int64 SenderIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string strMobileNumber = (from AM in db.AcntMasts where AM.Acnt_Idno == SenderIdno select AM.Cont_Mobile).FirstOrDefault();
                return strMobileNumber;
            }
        }
        public tblUserPref GetUserPref()
        {
            tblUserPref lst = null;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    lst = db.tblUserPrefs.SingleOrDefault();
                    return lst;
                }
            }
            catch (Exception ex)
            {
                return lst;
            }
        }
        public int SaveUserPrefDetail(tblUserPref tblData)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var data = db.tblUserPrefs.SingleOrDefault();
                    data.Send_GrSMS = tblData.Send_GrSMS;
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable SelectGRPrintKajaria(string con, Int64 GRIdno)
        {
            SqlParameter[] objsqlpara = new SqlParameter[2];
            objsqlpara[0] = new SqlParameter("@Action", "SelectKajariaPrint");
            objsqlpara[1] = new SqlParameter("@Id", GRIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRPrep", objsqlpara);
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
    }
}
