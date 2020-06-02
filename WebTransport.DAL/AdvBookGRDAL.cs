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
    public class AdvBookGRDAL
    {
        #region DECLARE VARIABLES
        string sqlSTR = string.Empty;
        #endregion

        #region Functions...
        public IList SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblCityMasters where obj.Status == true && obj.AsLocation == true orderby obj.City_Name select obj).ToList();
                return lst;
            }
        }

        public Int64 MaxNo(Int32 yearId, Int32 FromCityIdno, string con)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(AdvOrder_No),0) + 1 AS MAXID FROM tblAdvGrOrder WHERE Loc_Idno='" + FromCityIdno + "'  AND YearIdno=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public IList CheckDuplicateGrNo(Int64 intGrNo, Int32 FromCityIdno, Int32 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from m in db.tblAdvGrOrders
                           where m.AdvOrder_No == intGrNo && m.YearIdno == intYearIdno && m.Loc_Idno == FromCityIdno
                           select new
                           {
                               m.AdvOrder_No,
                           }).ToList();
                return lst;
            }
        }


        public tblAdvGrOrder SelectTblAdvBookGRHead(Int32 AdvOrderGR_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblAdvGrOrders.Where(tgh => (tgh.AdvOrderGR_Idno == AdvOrderGR_Idno)).FirstOrDefault();
            }
        }
        #endregion

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

        #region INSER/UPDATE
        public Int64 InsertAdvBookGROrder(Int64 YearIdno, Int64 intLocIDno, DateTime strOrdrDate, DateTime? strOrdrRecDate, Int64 intOrderNo, String ReferNum, Int64 TruckNoIdno, Int64 intPartyIdno, Int64 intTocityIDno, Int32 intCityViaIdno, Int64 intDelPlaceIdno, String strShipmentNo, string ContainerNum, string ContainerSealNum, Int64 ContainerSize, Int64 ContainerType, String PortNum, String strRemark, Double GrossAmnt, Double RoundOff, Double NetAmnt, DataTable dtDetail, Int32 GRType, Int64 UserIdno, Int64 AgentIdno, DateTime? strBKGDateFrom, DateTime? strBKGDateTo, string ConsrName, string Container_Num2, string SealNum_2, Int32 ExpImp_id, string CharFrwder_Name)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblAdvGrOrder objGRHead = db.tblAdvGrOrders.Where(rh => (rh.AdvOrder_No == intOrderNo) && (rh.Loc_Idno == intLocIDno) && (rh.YearIdno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        objGRHead = new tblAdvGrOrder();
                        objGRHead.YearIdno = YearIdno;
                        objGRHead.Loc_Idno = intLocIDno;
                        objGRHead.AdvOrder_Date = strOrdrDate;
                        objGRHead.AdvOrderRec_Date = strOrdrRecDate;
                        objGRHead.AdvOrder_No = intOrderNo;
                        objGRHead.Ref_No = ReferNum;
                        objGRHead.Truck_Idno = TruckNoIdno;
                        objGRHead.Party_Idno = intPartyIdno;
                        objGRHead.Loc_To = intTocityIDno;
                        objGRHead.Loc_DelvPlace = intDelPlaceIdno;

                        objGRHead.Shipment_No = strShipmentNo;
                        objGRHead.Contanr_No = ContainerNum;
                        objGRHead.Contanr_Size = ContainerSize;
                        objGRHead.Contanr_Type = ContainerType;
                        objGRHead.Contanr_SealNo = ContainerSealNum;
                        objGRHead.Port_no = PortNum;
                        objGRHead.Remark = strRemark;
                        objGRHead.GRType = GRType;
                        objGRHead.Gross_Amnt = GrossAmnt;
                        objGRHead.RoundOff_Amnt = RoundOff;
                        objGRHead.Net_Amnt = NetAmnt;
                        objGRHead.Agent_Idno = AgentIdno;
                        objGRHead.BKGDate_From = strBKGDateFrom;
                        objGRHead.BKGDate_To = strBKGDateTo;
                        objGRHead.Cityvia_Idno = intCityViaIdno;
                        objGRHead.Consignor_Name = ConsrName;
                        objGRHead.UserIdno = UserIdno;
                        objGRHead.GRContanr_No2 = Container_Num2;
                        objGRHead.GRContanr_SealNo2 = SealNum_2;
                        objGRHead.ImpExp_idno = ExpImp_id;
                        objGRHead.ChaFrwdr_Name = CharFrwder_Name;

                        db.tblAdvGrOrders.AddObject(objGRHead);
                        db.SaveChanges();
                        intGrIdno = objGRHead.AdvOrderGR_Idno;
                        if (intGrIdno > 0)
                        {
                            foreach (DataRow row in dtDetail.Rows)
                            {
                                tblAdvGrOrderDetl objGRDetl = new tblAdvGrOrderDetl();
                                objGRDetl.AdvOrderGRHead_Idno = Convert.ToInt64(intGrIdno);
                                objGRDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                objGRDetl.Unit_Idno = Convert.ToInt32(row["Unit_Idno"]);
                                objGRDetl.RateType_Idno = Convert.ToInt32(row["Rate_TypeIdno"]);
                                objGRDetl.Quantity = Convert.ToInt64(row["Quantity"]);
                                objGRDetl.Item_Weight = Convert.ToDouble(row["Weight"]);
                                objGRDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                objGRDetl.Item_Amount = Convert.ToDouble(row["Amount"]);
                                db.tblAdvGrOrderDetls.AddObject(objGRDetl);
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

        public Int64 UpdateAdvBookGROrder(Int64 AdvOrderGR_Idno, Int64 YearIdno, Int64 intLocIDno, DateTime strOrdrDate, DateTime? strOrdrRecDate, Int64 intOrderNo, String ReferNum, Int64 TruckNoIdno, Int64 intPartyIdno, Int64 intTocityIDno, Int32 intCityViaIdno, Int64 intDelPlaceIdno, String strShipmentNo, string ContainerNum, string ContainerSealNum, Int64 ContainerSize, Int64 ContainerType, String PortNum, String strRemark, Double GrossAmnt, Double RoundOff, Double NetAmnt, DataTable dtDetail, Int32 GRType, Int64 UserIdno, Int64 AgentIdno, DateTime? strBKGDateFrom, DateTime? strBKGDateTo,string ConsrName, string Container_Num2, string SealNum_2, Int32 ExpImp_id, string CharFrwder_Name)
        {
            Int64 intGrIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblAdvGrOrder objGRHead = db.tblAdvGrOrders.Where(rh => (rh.AdvOrderGR_Idno != AdvOrderGR_Idno) && (rh.AdvOrder_No == intOrderNo) && (rh.Loc_Idno == intLocIDno) && (rh.YearIdno == YearIdno)).FirstOrDefault();
                    if (objGRHead == null)
                    {
                        tblAdvGrOrder objGRHead1 = db.tblAdvGrOrders.Where(rh => rh.AdvOrderGR_Idno == AdvOrderGR_Idno).FirstOrDefault();
                        if (objGRHead1 != null)
                        {
                            objGRHead1.YearIdno = YearIdno;
                            objGRHead1.Loc_Idno = intLocIDno;
                            objGRHead1.AdvOrder_Date = strOrdrDate;
                            objGRHead1.AdvOrderRec_Date = strOrdrRecDate;
                            objGRHead1.AdvOrder_No = intOrderNo;
                            objGRHead1.Ref_No = ReferNum;
                            objGRHead1.Truck_Idno = TruckNoIdno;
                            objGRHead1.Party_Idno = intPartyIdno;
                            objGRHead1.Loc_To = intTocityIDno;
                            objGRHead1.Loc_DelvPlace = intDelPlaceIdno;

                            objGRHead1.GRType = GRType;
                            objGRHead1.Shipment_No = strShipmentNo;
                            objGRHead1.Contanr_No = ContainerNum;
                            objGRHead1.Contanr_Size = ContainerSize;
                            objGRHead1.Contanr_Type = ContainerType;
                            objGRHead1.Contanr_SealNo = ContainerSealNum;
                            objGRHead1.Port_no = PortNum;
                            objGRHead1.Remark = strRemark;

                            objGRHead1.BKGDate_From = strBKGDateFrom;
                            objGRHead1.BKGDate_To = strBKGDateTo;
                            objGRHead1.UserIdno = UserIdno;
                            objGRHead1.Gross_Amnt = GrossAmnt;
                            objGRHead1.RoundOff_Amnt = RoundOff;
                            objGRHead1.Net_Amnt = NetAmnt;
                            objGRHead1.Agent_Idno = AgentIdno;
                            objGRHead1.Cityvia_Idno = intCityViaIdno;
                            objGRHead1.Consignor_Name = ConsrName;
                            objGRHead1.GRContanr_No2 = Container_Num2;
                            objGRHead1.GRContanr_SealNo2 = SealNum_2;
                            objGRHead1.ImpExp_idno = ExpImp_id;
                            objGRHead1.ChaFrwdr_Name = CharFrwder_Name;

                            db.SaveChanges();
                            intGrIdno = objGRHead1.AdvOrderGR_Idno;

                            if (intGrIdno > 0)
                            {
                                List<tblAdvGrOrderDetl> lstGrDetl = db.tblAdvGrOrderDetls.Where(obj => obj.AdvOrderGRHead_Idno == intGrIdno).ToList();
                                if (lstGrDetl.Count > 0)
                                {
                                    foreach (tblAdvGrOrderDetl obj in lstGrDetl)
                                    {
                                        db.tblAdvGrOrderDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                }
                                foreach (DataRow row in dtDetail.Rows)
                                {
                                    tblAdvGrOrderDetl objGRDetl = new tblAdvGrOrderDetl();
                                    objGRDetl.AdvOrderGRHead_Idno = Convert.ToInt64(intGrIdno);
                                    objGRDetl.Item_Idno = Convert.ToInt32(row["Item_Idno"]);
                                    objGRDetl.Unit_Idno = Convert.ToInt32(row["Unit_Idno"]);
                                    objGRDetl.RateType_Idno = Convert.ToInt32(row["Rate_TypeIdno"]);
                                    objGRDetl.Quantity = Convert.ToInt64(row["Quantity"]);
                                    objGRDetl.Item_Weight = Convert.ToDouble(row["Weight"]);
                                    objGRDetl.Item_Rate = Convert.ToDouble(row["Rate"]);
                                    objGRDetl.Item_Amount = Convert.ToDouble(row["Amount"]);
                                    db.tblAdvGrOrderDetls.AddObject(objGRDetl);
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
        #endregion

        public IList SelectAdvGRDetails(Int32 OrderNumber, DateTime? FromDate, DateTime? ToDate, Int32 Location, Int32 LocationDelivery, Int32 LocationTo, Int32 SenderIdno, Int32 YearIdno, Int32 Grtype)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblAdvGrOrders
                           join cifrom in db.tblCityMasters on hd.Loc_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.Loc_To equals cito.City_Idno
                           join cidl in db.tblCityMasters on hd.Loc_DelvPlace equals cidl.City_Idno
                           join acnts in db.AcntMasts on hd.Party_Idno equals acnts.Acnt_Idno
                           join ContSize in db.tblContainerSizes on hd.Contanr_Size equals ContSize.ContainerSize_Idno into a
                           from temp1 in a.DefaultIfEmpty()
                           join ContType in db.tblContainerTypes on hd.Contanr_Type equals ContType.ContainerType_Idno into b
                           from temp2 in b.DefaultIfEmpty()
                           select new
                           {
                               hd.GRType,
                               GRTYPENAME = (hd.GRType == 1) ? "Paid GR" : (hd.GRType == 2) ? "TBB GR" : "To Pay GR",
                               hd.Net_Amnt,
                               hd.Loc_DelvPlace,
                               hd.Loc_Idno,
                               hd.Loc_To,
                               hd.AdvOrder_Date,
                               hd.AdvOrderGR_Idno,
                               hd.AdvOrder_No,
                               hd.Party_Idno,
                               hd.YearIdno,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityDely = cidl.City_Name,
                               Sender = acnts.Acnt_Name,
                               RefNo=hd.Ref_No,
                               StuffDate=hd.AdvOrderRec_Date,
                               Remark=hd.Remark,
                               ContNo=hd.Contanr_No,
                               ContSiz = temp1.Container_Size,
                               ContType = temp2.Container_Type,
                               BkgDateFrom = hd.BKGDate_From,
                               BkgDateTo = hd.BKGDate_To,
                               Port=hd.Port_no
                           }).ToList();
                if (Grtype > 0)
                {
                    lst = lst.Where(l => l.GRType == Grtype).ToList();
                }
                if (OrderNumber > 0)
                {
                    lst = lst.Where(l => l.AdvOrder_No == OrderNumber).ToList();
                }
                if (FromDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.AdvOrder_Date).Date >= Convert.ToDateTime(FromDate).Date).ToList();
                }
                if (ToDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.AdvOrder_Date).Date <= Convert.ToDateTime(ToDate).Date).ToList();
                }
                if (Location > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == Location).ToList();
                }
                if (LocationDelivery > 0)
                {
                    lst = lst.Where(l => l.Loc_DelvPlace == LocationDelivery).ToList();
                }
                if (LocationTo > 0)
                {
                    lst = lst.Where(l => l.Loc_To == LocationTo).ToList();
                }
                if (SenderIdno > 0)
                {
                    lst = lst.Where(l => l.Party_Idno == SenderIdno).ToList();
                }

                if (YearIdno > 0)
                {
                    lst = lst.Where(l => l.YearIdno == YearIdno).ToList();
                }

                return lst;
            }
        }
        public IList SelectAdvGRDetailsReport(Int32 OrderNumber, DateTime? FromDate, DateTime? ToDate, Int32 Location, Int32 LocationDelivery, Int32 LocationTo, Int32 SenderIdno, Int32 YearIdno, Int32 Grtype)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from hd in db.tblAdvGrOrders
                           join AD in db.tblAdvGrOrderDetls on hd.AdvOrderGR_Idno equals AD.AdvOrderGRHead_Idno
                           join cifrom in db.tblCityMasters on hd.Loc_Idno equals cifrom.City_Idno
                           join cito in db.tblCityMasters on hd.Loc_To equals cito.City_Idno
                           join lm in db.LorryMasts on hd.Truck_Idno equals lm.Lorry_Idno
                           join cidl in db.tblCityMasters on hd.Loc_DelvPlace equals cidl.City_Idno
                           join acnts in db.AcntMasts on hd.Party_Idno equals acnts.Acnt_Idno
                           select new
                           {
                               hd.GRType,
                               GRTYPENAME = (hd.GRType == 1) ? "Paid GR" : (hd.GRType == 2) ? "TBB GR" : "To Pay GR",
                               hd.Net_Amnt,
                               hd.Loc_DelvPlace,
                               hd.Loc_Idno,
                               hd.Loc_To,
                               hd.AdvOrder_Date,
                               hd.AdvOrderGR_Idno,
                               hd.AdvOrder_No,
                               hd.Party_Idno,
                               hd.YearIdno,
                               TotQty=(from hd1 in db.tblAdvGrOrders join AD1 in db.tblAdvGrOrderDetls on hd1.AdvOrderGR_Idno equals AD1.AdvOrderGRHead_Idno
                                       where hd1.AdvOrderGR_Idno == hd.AdvOrderGR_Idno select AD1.Quantity).Sum(),

                               TotWeight = (from hd1 in db.tblAdvGrOrders
                                         join AD1 in db.tblAdvGrOrderDetls on hd1.AdvOrderGR_Idno equals AD1.AdvOrderGRHead_Idno
                                         where hd1.AdvOrderGR_Idno == hd.AdvOrderGR_Idno
                                         select AD1.Item_Weight).Sum(),

                               TotGRQty = (from GH in db.TblGrHeads
                                         join GD in db.TblGrDetls on GH.GR_Idno equals GD.GrHead_Idno
                                         where GH.AdvOrderGRHead_Idno == hd.AdvOrderGR_Idno
                                         select GD.Qty).Sum(),

                               TotGRWeight = (from GH in db.TblGrHeads
                                           join GD in db.TblGrDetls on GH.GR_Idno equals GD.GrHead_Idno
                                           where GH.AdvOrderGRHead_Idno == hd.AdvOrderGR_Idno
                                           select GD.Tot_Weght).Sum(),
                               hd.Ref_No,
                               lm.Lorry_No,
                               CityTo = cito.City_Name,
                               CityFrom = cifrom.City_Name,
                               CityDely = cidl.City_Name,
                               Sender = acnts.Acnt_Name

                           }).GroupBy(x => x.AdvOrder_No).Select(x => x.FirstOrDefault()).OrderBy(x=> x.AdvOrder_Date).ThenBy(x=> x.AdvOrder_No).ToList();
                if (Grtype > 0)
                {
                    lst = lst.Where(l => l.GRType == Grtype).ToList();
                }
                if (OrderNumber > 0)
                {
                    lst = lst.Where(l => l.AdvOrder_No == OrderNumber).ToList();
                }
                if (FromDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.AdvOrder_Date).Date >= Convert.ToDateTime(FromDate).Date).ToList();
                }
                if (ToDate != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.AdvOrder_Date).Date <= Convert.ToDateTime(ToDate).Date).ToList();
                }
                if (Location > 0)
                {
                    lst = lst.Where(l => l.Loc_Idno == Location).ToList();
                }
                if (LocationDelivery > 0)
                {
                    lst = lst.Where(l => l.Loc_DelvPlace == LocationDelivery).ToList();
                }
                if (LocationTo > 0)
                {
                    lst = lst.Where(l => l.Loc_To == LocationTo).ToList();
                }
                if (SenderIdno > 0)
                {
                    lst = lst.Where(l => l.Party_Idno == SenderIdno).ToList();
                }

                if (YearIdno > 0)
                {
                    lst = lst.Where(l => l.YearIdno == YearIdno).ToList();
                }

                return lst;
            }
        }
        public IList SelectGRDetail(int GrIDNO)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from GD in db.tblAdvGrOrderDetls
                           join IM in db.ItemMasts on GD.Item_Idno equals IM.Item_Idno
                           join UM in db.UOMMasts on GD.Unit_Idno equals UM.UOM_Idno
                           where GD.AdvOrderGRHead_Idno == GrIDNO
                           select new
                           {
                               GD.Quantity,
                               GD.Item_Weight,
                               GD.Item_Amount,
                               GD.AdvOrderGRHead_Idno,
                               GD.Item_Idno,
                               GD.Unit_Idno,
                               Rate_Type = (GD.RateType_Idno == 1 ? "Rate" : "Weight"),
                               RateType_Idno = GD.RateType_Idno,
                               GD.Item_Rate,
                               IM.Item_Name,
                               UM.UOM_Name
                           }).ToList();
                return lst;
            }
        }
        public int DeleteGR(Int64 HeadId, Int64 UserIdno, string con)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblAdvGrOrder qth = db.tblAdvGrOrders.Where(h => h.AdvOrderGR_Idno == HeadId).FirstOrDefault();

                List<tblAdvGrOrderDetl> CD = db.tblAdvGrOrderDetls.Where(d => d.AdvOrderGRHead_Idno == HeadId).ToList();

                if (qth != null)
                {
                    SqlParameter[] objSqlPara = new SqlParameter[3];
                    objSqlPara[0] = new SqlParameter("@Action", "DeleteAdvBookGR");
                    objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                    objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                    Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);

                    foreach (var d in CD)
                    {
                        db.tblAdvGrOrderDetls.DeleteObject(d);

                        db.SaveChanges();
                    }

                    db.tblAdvGrOrders.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }
        public double SelectItemRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRateForTBB(Int32 ItemIdno, Int32 TocityIdno, Int32 FromcityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "TBB" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectItemRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select (RM.Rate_Idno)).Max();
                }
                if (MaxId > 0)
                {
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_Rate).FirstOrDefault());
                }
                return ItemRate;
            }
        }
        public double SelectItemWghtRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double ItemWghtRate = 0;
                Max = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from RM in db.tblRateMasts where RM.Rate_Type == "IR" && RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate select RM.Rate_Idno).Max();
                }
                if (MaxId > 0)
                {
                    ItemWghtRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Rate_Idno == MaxId select RM.Item_WghtRate).FirstOrDefault());
                }
                return ItemWghtRate;
            }
        }
        public double SelectItemWeightWiseRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Decimal Weight,Int64 PartyId)
        {
            double ItemRate = 0.00;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ItemRate = Convert.ToDouble((from RM in db.tblPartyRateMasts where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Loc_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Item_Weight >= Weight && RM.Party_Idno==PartyId orderby RM.Item_Weight ascending  select RM.Item_Rate).FirstOrDefault());
                if (ItemRate > 0.00)
                    return ItemRate;
                else
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMasts where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.Rate_Date <= Grdate && RM.Item_Weight >= Weight orderby RM.Item_Weight ascending select RM.Item_WghtRate).FirstOrDefault());
                return ItemRate;
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
        public Int64 IfExistsInGr(Int64 AdvGrIdno, Int32 yearId, string con)
        {
            Int64 CountValue = 0;
            sqlSTR = @"SELECT COUNT(AdvOrderGRHead_Idno) AS 'COUNT' FROM TblGrHead AS GH INNER JOIN TblGrDetl GD ON GH.GR_Idno=GD.GrHead_Idno WHERE GH.AdvOrderGRHead_Idno=" + AdvGrIdno + "  AND GH.Year_Idno=" + yearId;
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                CountValue = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return CountValue;
        }
        public Int64 Countall()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int32 Count = 0;
                Count = (from hd in db.tblAdvGrOrders
                         join cifrom in db.tblCityMasters on hd.Loc_Idno equals cifrom.City_Idno
                         join cito in db.tblCityMasters on hd.Loc_To equals cito.City_Idno
                         join cidl in db.tblCityMasters on hd.Loc_DelvPlace equals cidl.City_Idno
                         join acnts in db.AcntMasts on hd.Party_Idno equals acnts.Acnt_Idno
                         select hd.AdvOrder_No).Count();

                return Count;
            }

        }
        public Int64 MaxIdno(string con, Int64 FronCityIdno)
        {
            Int64 MaxNo = 0;
            sqlSTR = @"SELECT ISNULL(MAX(AdvOrderGR_Idno),0)  AS MAXID FROM TBLADVGRORDER WHERE LOC_IDNO=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }
    }
}
