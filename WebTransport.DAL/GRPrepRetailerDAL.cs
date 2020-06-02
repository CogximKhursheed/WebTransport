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
    public class GRPrepRetailerDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region "Insert/Update Data"
        public Int64 Insert(Int64 YearIdno, DateTime Date, string PrefNo, Int64 intGrNo, int Against, int GrType, Int64 SenderIdno, Int64 ReceiverIdno, Int64 LocFrom,
                    Int64 ToCity, Int64 DelvPlaceIdno, Int64 ViaCityIdno, int StaxPaidBy, Int64 RcptType, string InstNumber, DateTime? InstDate, Int64 CustBankIdno,
                    string Dino, string EGPNo, string RefNo, DateTime? RefDate, string OrdrNo, string FormNo, string Consignor,
                    Int64 AgentIdno, string ManualNo, Int64 TranType, Int64 TruckIdno, string ShipmentNo, string ContainerNo1, string ContainerNo2, string SealNo1,
                    string SealNo2, Int64 ContainerType, Int64 ContainerSize, string PortNo, Int64 ContainerTypeWay, string Remark, double GrossAmnt, double CartageAmnt,
                    double TotalAmnt, double SurchargeAmnt, double CommissionAmnt, double BiltyAmnt, double WagesAmnt, double PFAmnt, double TollTaxAmnt, double SubTotalAmnt,
                    double ServTaxAmnt, double SwachhBhrtCessAmnt, double KrishiAmnt, double TotalPrice, double RoundoffAmnt, double NetAmnt, bool MODVAT, DataTable dtdetl,
                    double dblKalyanTaxPer, double dblServTaxPerc, double dblSwcgBrtTaxPerc, double dblTaxValid, Int64 Type, double FixedAmnt, Boolean isTBBRate,double StrCharges,
                    Int64 CreatedBy, Double dSGST_Amt, Double dCGST_Amt, Double dIGST_Amt, Double dGSTCess_Amt, Double dSGST_Per, Double dCGST_Per, Double dIGST_Per, Double dGSTCess_Per, int GST_Idno)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblGrRetailerHead objHead = db.tblGrRetailerHeads.Where(rh => (rh.GRRet_Pref == PrefNo) && (rh.GRRet_No == intGrNo) && (rh.From_City == LocFrom) && (rh.Year_Idno == YearIdno)).FirstOrDefault();
                    if (objHead == null)
                    {
                        objHead = new tblGrRetailerHead();
                        objHead.Year_Idno = YearIdno; objHead.GRRet_Date = Date; objHead.GRRet_Pref = PrefNo; objHead.GRRet_No = intGrNo; objHead.GRRet_Agnst = Against; objHead.GRRet_Typ = GrType;
                        objHead.Sender_Idno = SenderIdno; objHead.Recivr_Idno = ReceiverIdno; objHead.From_City = LocFrom; objHead.To_City = ToCity; objHead.DelvryPlce_Idno = DelvPlaceIdno;
                        objHead.Via_City = ViaCityIdno; objHead.StaxPaid_ByIdno = StaxPaidBy; objHead.Rcpt_Type = RcptType; objHead.Inst_No = InstNumber; objHead.Inst_Date = InstDate;
                        objHead.CustBank_Idno = CustBankIdno; objHead.Agnt_Idno = AgentIdno; objHead.Manual_No = ManualNo; objHead.Tran_Type = TranType; objHead.Lorry_Idno = TruckIdno;
                        objHead.Shipmnt_No = ShipmentNo; objHead.Container_No1 = ContainerNo1; objHead.Container_No2 = ContainerNo2; objHead.Seal_No1 = SealNo1; objHead.Seal_No2 = SealNo2;
                        objHead.Container_type = ContainerType; objHead.Container_Size = ContainerSize; objHead.Port_No = PortNo; objHead.ContainerType_Way = ContainerTypeWay;
                        objHead.Remark = Remark; objHead.Gross_Amnt = GrossAmnt; objHead.Cartage_Amnt = CartageAmnt; objHead.Total_Amnt = TotalAmnt; objHead.Surcharge_Amnt = SurchargeAmnt;
                        objHead.Commission_Amnt = CommissionAmnt; objHead.Bilty_Amnt = BiltyAmnt; objHead.Wages_Amnt = WagesAmnt; objHead.PF_Amnt = PFAmnt; objHead.TollTax_Amnt = TollTaxAmnt;
                        objHead.SubTotal_Amnt = SubTotalAmnt; objHead.ServTax_Amnt = ServTaxAmnt; objHead.SwachhBhrtTax_Amnt = SwachhBhrtCessAmnt; objHead.KrishiKalyan_Amnt = KrishiAmnt;
                        objHead.Total_Price = TotalPrice; objHead.RoundOff_Amnt = RoundoffAmnt; objHead.Net_Amount = NetAmnt; objHead.DI_NO = Dino; objHead.Consignor_Name = Consignor;
                        objHead.MODVAT_CPY = MODVAT; objHead.Billed = Convert.ToBoolean(0); objHead.Chln_Idno = 0; objHead.EGP_No = EGPNo; objHead.Ref_No = RefNo; objHead.Order_No = OrdrNo;
                        objHead.Form_No = FormNo; objHead.KisanKalyan_Per = dblKalyanTaxPer; objHead.SwcgBrtTax_Perc = dblSwcgBrtTaxPerc; objHead.ServTax_Perc = dblServTaxPerc;
                        objHead.ServTax_Valid = dblTaxValid; objHead.TBB_Rate = isTBBRate; objHead.Type_Amnt = FixedAmnt; objHead.TypeId = Type;
                        objHead.DateAdded = DateTime.Now;
                        objHead.DateModified = DateTime.Now;
                        objHead.Created_By = CreatedBy;
                        objHead.Ref_Date = RefDate; objHead.Stcharg_Amnt = StrCharges;
                        objHead.SGST_Per = dSGST_Per;
                        objHead.CGST_Per = dCGST_Per;
                        objHead.IGST_Per = dIGST_Per;
                        objHead.GSTCess_Per = dGSTCess_Per;
                        objHead.SGST_Amt = dSGST_Amt;
                        objHead.CGST_Amt = dCGST_Amt;
                        objHead.IGST_Amt = dIGST_Amt;
                        objHead.GSTCess_Amt = dGSTCess_Amt;
                        objHead.GST_Idno = GST_Idno;
                        db.tblGrRetailerHeads.AddObject(objHead);
                        db.SaveChanges();
                        intGrIdno = objHead.GRRetHead_Idno;
                        if (intGrIdno > 0)
                        {
                            foreach (DataRow row in dtdetl.Rows)
                            {
                                tblGrRetailerDetl objDetl = new tblGrRetailerDetl();
                                objDetl.GRRetHead_Idno = Convert.ToInt64(intGrIdno);
                                objDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                objDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["RateType_Idno"])) ? 0 : Convert.ToInt32(row["RateType_Idno"]);
                                objDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                objDetl.Act_Weight = Convert.ToDouble(row["Act_Weight"]);
                                objDetl.Ch_Weight = Convert.ToDouble(row["Ch_Weight"]);
                                objDetl.Item_Rate = Convert.ToDouble(row["Item_Rate"]);
                                objDetl.Amount = Convert.ToInt32(row["Amount"]);
                                objDetl.Detail = Convert.ToString(row["Detail"]);
                                objDetl.Packet_No = Convert.ToString(row["Packet_No"]);
                                objDetl.Dimension = string.IsNullOrEmpty(Convert.ToString(row["Dimension"])) ? "" : Convert.ToString(row["Dimension"]);
                                objDetl.CFT = string.IsNullOrEmpty(Convert.ToString(row["CFT"])) ? "" : Convert.ToString(row["CFT"]);
                                //objDetl.Shrtg_Limit_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Limit_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Limit_Other"]));
                                //objDetl.Shrtg_Rate_Other = Convert.ToDouble(Convert.ToString(row["Shrtg_Rate_Other"]) == "" ? 0 : Convert.ToDouble(row["Shrtg_Rate_Other"]));
                                db.tblGrRetailerDetls.AddObject(objDetl);
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

        public Int64 Update(Int64 intGRIdno, Int64 YearIdno, DateTime Date, string PrefNo, Int64 intGrNo, int Against, int GrType, Int64 SenderIdno, Int64 ReceiverIdno, Int64 LocFrom,
                    Int64 ToCity, Int64 DelvPlaceIdno, Int64 ViaCityIdno, int StaxPaidBy, Int64 RcptType, string InstNumber, DateTime? InstDate, Int64 CustBankIdno,
                    string Dino, string EGPNo, string RefNo, DateTime? RefDate, string OrdrNo, string FormNo, string Consignor,
                    Int64 AgentIdno, string ManualNo, Int64 TranType, Int64 TruckIdno, string ShipmentNo, string ContainerNo1, string ContainerNo2, string SealNo1,
                    string SealNo2, Int64 ContainerType, Int64 ContainerSize, string PortNo, Int64 ContainerTypeWay, string Remark, double GrossAmnt, double CartageAmnt,
                    double TotalAmnt, double SurchargeAmnt, double CommissionAmnt, double BiltyAmnt, double WagesAmnt, double PFAmnt, double TollTaxAmnt, double SubTotalAmnt,
                    double ServTaxAmnt, double SwachhBhrtCessAmnt, double KrishiAmnt, double TotalPrice, double RoundoffAmnt, double NetAmnt, bool MODVAT, DataTable dtdetl,
                    double dblKalyanTaxPer, double dblServTaxPerc, double dblSwcgBrtTaxPerc, double dblTaxValid, Int64 Type, double FixedAmnt, Boolean isTBBRate,
                    double StrCharges, Int64 CreatedBy, Double dSGST_Amt, Double dCGST_Amt, Double dIGST_Amt, Double dGSTCess_Amt, Double dSGST_Per, Double dCGST_Per, Double dIGST_Per, Double dGSTCess_Per, int GST_Idno)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblGrRetailerHead objGRHead = db.tblGrRetailerHeads.Where(rh => (rh.GRRet_Pref == PrefNo) && (rh.GRRet_No == intGrNo) && (rh.From_City == LocFrom) && (rh.Year_Idno == YearIdno) && rh.GRRetHead_Idno != intGRIdno).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        tblGrRetailerHead objGRHead1 = db.tblGrRetailerHeads.Where(rh => (rh.GRRetHead_Idno == intGRIdno)).FirstOrDefault();
                        if (objGRHead1 != null)
                        {
                            objGRHead1.Year_Idno = YearIdno;
                            objGRHead1.Year_Idno = YearIdno; objGRHead1.GRRet_Date = Date; objGRHead1.GRRet_Pref = PrefNo; objGRHead1.GRRet_No = intGrNo; objGRHead1.GRRet_Agnst = Against; objGRHead1.GRRet_Typ = GrType;
                            objGRHead1.Sender_Idno = SenderIdno; objGRHead1.Recivr_Idno = ReceiverIdno; objGRHead1.From_City = LocFrom; objGRHead1.To_City = ToCity; objGRHead1.DelvryPlce_Idno = DelvPlaceIdno;
                            objGRHead1.Via_City = ViaCityIdno; objGRHead1.StaxPaid_ByIdno = StaxPaidBy; objGRHead1.Rcpt_Type = RcptType; objGRHead1.Inst_No = InstNumber; objGRHead1.Inst_Date = InstDate;
                            objGRHead1.CustBank_Idno = CustBankIdno; objGRHead1.Agnt_Idno = AgentIdno; objGRHead1.Manual_No = ManualNo; objGRHead1.Tran_Type = TranType; objGRHead1.Lorry_Idno = TruckIdno;
                            objGRHead1.Shipmnt_No = ShipmentNo; objGRHead1.Container_No1 = ContainerNo1; objGRHead1.Container_No2 = ContainerNo2; objGRHead1.Seal_No1 = SealNo1; objGRHead1.Seal_No2 = SealNo2;
                            objGRHead1.Container_type = ContainerType; objGRHead1.Container_Size = ContainerSize; objGRHead1.Port_No = PortNo; objGRHead1.ContainerType_Way = ContainerTypeWay;
                            objGRHead1.Remark = Remark; objGRHead1.Gross_Amnt = GrossAmnt; objGRHead1.Cartage_Amnt = CartageAmnt; objGRHead1.Total_Amnt = TotalAmnt; objGRHead1.Surcharge_Amnt = SurchargeAmnt;
                            objGRHead1.Commission_Amnt = CommissionAmnt; objGRHead1.Bilty_Amnt = BiltyAmnt; objGRHead1.Wages_Amnt = WagesAmnt; objGRHead1.PF_Amnt = PFAmnt; objGRHead1.TollTax_Amnt = TollTaxAmnt;
                            objGRHead1.SubTotal_Amnt = SubTotalAmnt; objGRHead1.ServTax_Amnt = ServTaxAmnt; objGRHead1.SwachhBhrtTax_Amnt = SwachhBhrtCessAmnt; objGRHead1.KrishiKalyan_Amnt = KrishiAmnt;
                            objGRHead1.Total_Price = TotalPrice; objGRHead1.RoundOff_Amnt = RoundoffAmnt; objGRHead1.Net_Amount = NetAmnt; objGRHead1.DI_NO = Dino; objGRHead1.Consignor_Name = Consignor;
                            objGRHead1.MODVAT_CPY = MODVAT; objGRHead1.Billed = Convert.ToBoolean(0); objGRHead1.Chln_Idno = 0; objGRHead1.EGP_No = EGPNo; objGRHead1.Ref_No = RefNo; objGRHead1.Order_No = OrdrNo;
                            objGRHead1.Form_No = FormNo; objGRHead1.KisanKalyan_Per = dblKalyanTaxPer; objGRHead1.SwcgBrtTax_Perc = dblSwcgBrtTaxPerc; objGRHead1.ServTax_Perc = dblServTaxPerc;
                            objGRHead1.ServTax_Valid = dblTaxValid; objGRHead1.TBB_Rate = isTBBRate; objGRHead1.TypeId = Type; objGRHead1.Type_Amnt = FixedAmnt;
                            objGRHead1.Ref_Date = RefDate; objGRHead1.Stcharg_Amnt = StrCharges;
                            objGRHead1.DateModified = DateTime.Now;
                            objGRHead1.Created_By = CreatedBy;
                            objGRHead1.SGST_Per = dSGST_Per;
                            objGRHead1.CGST_Per = dCGST_Per;
                            objGRHead1.IGST_Per = dIGST_Per;
                            objGRHead1.GSTCess_Per = dGSTCess_Per;
                            objGRHead1.SGST_Amt = dSGST_Amt;
                            objGRHead1.CGST_Amt = dCGST_Amt;
                            objGRHead1.IGST_Amt = dIGST_Amt;
                            objGRHead1.GSTCess_Amt = dGSTCess_Amt;
                            objGRHead1.GST_Idno = GST_Idno;
                            db.SaveChanges();
                            intGrIdno = objGRHead1.GRRetHead_Idno;
                            if (intGrIdno > 0)
                            {
                                List<tblGrRetailerDetl> lstGrDetl = db.tblGrRetailerDetls.Where(obj => obj.GRRetHead_Idno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (tblGrRetailerDetl obj in lstGrDetl)
                                    {
                                        db.tblGrRetailerDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtdetl.Rows)
                                {
                                    tblGrRetailerDetl objDetl = new tblGrRetailerDetl();
                                    objDetl.GRRetHead_Idno = Convert.ToInt64(intGrIdno);
                                    objDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objDetl.Unit_Idno = string.IsNullOrEmpty(Convert.ToString(row["Unit_Idno"])) ? 0 : Convert.ToInt32(row["Unit_Idno"]);
                                    objDetl.Rate_Type = string.IsNullOrEmpty(Convert.ToString(row["RateType_Idno"])) ? 0 : Convert.ToInt32(row["RateType_Idno"]);
                                    objDetl.Qty = Convert.ToInt64(row["Quantity"]);
                                    objDetl.Act_Weight = Convert.ToDouble(row["Act_Weight"]);
                                    objDetl.Ch_Weight = Convert.ToDouble(row["Ch_Weight"]);
                                    objDetl.Item_Rate = Convert.ToDouble(row["Item_Rate"]);
                                    objDetl.Amount = Convert.ToInt32(row["Amount"]);
                                    objDetl.Detail = Convert.ToString(row["Detail"]);
                                    objDetl.Packet_No = Convert.ToString(row["Packet_No"]);
                                    objDetl.CFT = Convert.ToString(row["CFT"]);
                                    objDetl.Dimension = Convert.ToString(row["Dimension"]);
                                    db.tblGrRetailerDetls.AddObject(objDetl);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }

                }
                catch (Exception Ex)
                {
                    intGrIdno = 0;
                }
            }
            return intGrIdno;
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
        #endregion

        #region "Grid"

        public IList SelectGRRetailer(int GrNo, DateTime? dtfrom, DateTime? dtto, int cityfrom, int citydely, int cityto, int sender, Int32 yearidno, Int64 UserIdno, string strGrPrefix,string MnNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblGrRetailerHeads
                           join LR in db.LorryMasts on hd.Lorry_Idno equals LR.Lorry_Idno into lorry
                           from mappingslorry in lorry.DefaultIfEmpty()
                           join MI in db.tblMiscMasters on hd.Lorry_Idno equals MI.Misc_Idno into Misc
                           from mappingsMisc in Misc.DefaultIfEmpty()
                           join cifrom in db.tblCityMasters on hd.From_City equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.To_City equals cito.City_Idno
                           join cidVia in db.tblCityMasters on hd.Via_City equals cidVia.City_Idno into Cidl
                           from mappingsCidl in Cidl.DefaultIfEmpty()
                           join cidVia in db.tblCityMasters on hd.Via_City equals cidVia.City_Idno into ViaCity
                           from mappingsViaCity in ViaCity.DefaultIfEmpty()
                           join acnts in db.AcntMasts on hd.Sender_Idno equals acnts.Acnt_Idno
                           join acntss in db.AcntMasts on hd.Recivr_Idno equals acntss.Acnt_Idno
                           join TType in db.tblTranTypes on hd.Tran_Type equals TType.TranType_Idno into Tran
                           from mappingsTran in Tran.DefaultIfEmpty()                           
                           //where hd.GR_Frm == "BK"
                           //where hd.Tran_Type== Trantype
                           select new
                           {
                               hd.GRRet_Pref,
                               hd.DelvryPlce_Idno,
                               hd.From_City,
                               hd.To_City,
                               hd.GRRet_Date,
                               hd.GRRetHead_Idno,
                               hd.GRRet_No,
                               hd.Sender_Idno,
                               hd.Remark,
                               hd.Shipmnt_No,
                               hd.Tran_Type,
                               GR_Typ = ((hd.GRRet_Typ == 1) ? "Paid GR" : (hd.GRRet_Typ == 2) ? "TBB GR" : "To Pay GR"),
                               hd.Recivr_Idno,
                               hd.Gross_Amnt,
                               hd.Net_Amount,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityVia = mappingsViaCity.City_Name,
                               CityDely = mappingsCidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               Receiver = acntss.Acnt_Name,
                               Year_Idno = hd.Year_Idno,
                               GRTran = hd.Tran_Type,
                               //Lorry_No = ((hd.Tran_Type == 0) ? mappingsTran.Tran_Type + " " + LR.Lorry_No : mappingsMisc.Tran_Idno+"" +MI.Misc_Name),
                               //Lorry_No = mappingsTran.Tran_Type + " " + LR.Lorry_No,
                               Lorry_No = ((hd.Tran_Type == 0) ? (mappingsTran.Tran_Type + " [" + mappingslorry.Lorry_No + "]") : (mappingsTran.Tran_Type + " [" + mappingsMisc.Misc_Name + "]")),
                               Owner_Name = mappingslorry.Owner_Name,
                               Qty = ((from N in db.tblGrRetailerDetls where N.GRRetHead_Idno == hd.GRRetHead_Idno select N.Qty).Sum()),
                               hd.Manual_No
                               //Qty = 1
                           }).ToList();
                if (GrNo > 0)
                {
                    lst = lst.Where(l => l.GRRet_No == GrNo).ToList();
                }

                if (strGrPrefix != null && strGrPrefix != "")
                {
                    lst = lst.Where(l => l.GRRet_Pref.Contains(strGrPrefix)).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.GRRet_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.GRRet_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
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
                if (MnNo != "")
                {
                    lst = lst.Where(l => l.Manual_No == MnNo).ToList();
                }
                return lst;
            }
        }

        #endregion

        #region "Delete Data"
        public int DeleteGR(Int64 HeadId)
        {
            clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblGrRetailerHead qth = db.tblGrRetailerHeads.Where(h => h.GRRetHead_Idno == HeadId).FirstOrDefault();
                List<tblGrRetailerDetl> qtd = db.tblGrRetailerDetls.Where(d => d.GRRetHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblGrRetailerDetls.DeleteObject(d);
                        db.SaveChanges();
                    }
                    db.tblGrRetailerHeads.DeleteObject(qth);
                    Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "GR");
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
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
        #endregion

        #region "Select"
        public tblGrRetailerHead SelectTblGRHead(int GRIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblGrRetailerHeads.Where(tgh => (tgh.GRRetHead_Idno == GRIdno)).FirstOrDefault();
            }
        }
        public DataTable SelectGRDetail(int GrIDNO, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[2];
                objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetail");
                objSqlPara[1] = new SqlParameter("@Id", GrIDNO);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRRetailPrep", objSqlPara);
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
        public Int64 SelectLorryType(Int64 LorryIdno)
        {
            Int64 LorryType = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                LorryType = Convert.ToInt64((from obj in db.LorryMasts where obj.Lorry_Idno == LorryIdno select obj.Lorry_Type).FirstOrDefault());
                return LorryType;
            }
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
        public double SelectWghtShrtgRate(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectWghtShrtgLimit(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgRate(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.QtyShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectQtyShrtgLimit(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Cityvia_Idno == CityviaIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.QtyShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBBContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno, double Weight, int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectWghtShrtgRateContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno, double Weight, int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.WghtShrtg_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectWghtShrtgLimitContWise(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno, double Weight, int ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0; double? WghtRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    WghtRate = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight >= Weight && RM.ConSize_ID == ConSizeIdno select RM.Con_Weight).Min();
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Con_Weight == WghtRate && RM.ConSize_ID == ConSizeIdno select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.WghtShrtg_Limit).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectItemRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "IR" && RM.Cityvia_Idno == CityviaIdno && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "IR" && RM.Cityvia_Idno == CityviaIdno && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Int64 CityviaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMastRetailers where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.Cityvia_Idno == CityviaIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public tblCityMaster GetStateIdno(Int32 cityidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCityMaster lst = (from cm in db.tblCityMasters where cm.City_Idno == cityidno orderby cm.City_Name ascending select cm).FirstOrDefault();
                return lst;
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
        public double SelectServTaxExmpt(Int32 SenderIdno)
        {
            Int32 ServTaxExmpt = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ServTaxExmpt = Convert.ToInt32((from UP in db.AcntMasts where UP.Acnt_Idno == SenderIdno select UP.ServTax_Exmpt).FirstOrDefault());
                return ServTaxExmpt;
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
        #endregion

        #region "Check"
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
        public IList CheckDuplicateGrNo(Int64 intGrNo, Int32 FromCityIdno, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from m in db.tblGrRetailerHeads
                           where m.GRRet_No == intGrNo && m.Year_Idno == intYearIdno && m.From_City == FromCityIdno
                           //&& m.GR_Frm == "BK"
                           select new
                           {
                               m.GRRet_No,
                           }).ToList();
                return lst;
            }
        }
        #endregion

        #region "User Pref"
        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }
        #endregion

        #region "MAX ID"
        public Int64 MaxNo(Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GRRet_NO),0) + 1 AS MAXID FROM tblGrRetailerHead WHERE  From_City='" + FromCityIdno + "'  AND YEAR_IDNO=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        public Int64 MaxIdno(string con, Int64 FromCityIdno)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(GRRetHead_Idno),0)  AS MAXID FROM tblGrRetailerHead WHERE FROM_CITY=" + FromCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
        #endregion


        #region "Select GR For Delete" By Salman...
        public Int64 CheckChallanDetails(Int64 GRHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 ChallanNo = 0;
                var temp = (from a in db.tblGrRetailerHeads
                            join b in db.tblChlnBookHeads on a.Chln_Idno equals b.Chln_Idno
                            where a.GRRetHead_Idno == GRHeadIdno && a.Chln_Idno > 0
                            select b.Chln_No
                         ).FirstOrDefault();
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

        public Int64 CheckInvoiceDetails(Int64 GRHeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 InvoiceNo = 0;
                InvoiceNo = Convert.ToInt64((from a in db.tblGrRetailerHeads
                                             join b in db.tblInvGenDetls on a.GRRetHead_Idno equals b.GR_Idno
                                             join c in db.tblInvGenHeads on b.InvGenHead_Idno equals c.Inv_Idno
                                             where a.GRRetHead_Idno == GRHeadIdno && a.Chln_Idno > 0 && a.Billed == true
                                             select c.Inv_No).FirstOrDefault());
                return InvoiceNo;
            }
        }

        #endregion
        public DataTable SelectGRPrintKajaria(string con, Int64 GRIdno)
        {
            SqlParameter[] objsqlpara = new SqlParameter[2];
            objsqlpara[0] = new SqlParameter("@Action", "SelectKajariaPrint");
            objsqlpara[1] = new SqlParameter("@Id", GRIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spGRRetailPrep", objsqlpara);
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
