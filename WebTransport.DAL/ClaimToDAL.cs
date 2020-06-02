using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data.Common;

namespace WebTransport.DAL
{
    public class ClaimToDAL
    {
        public IList SelectForSearch(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimHeads
                           join CD in db.tblClaimDetls on CH.ClaimHead_Idno equals CD.ClaimHead_Idno
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join acntss in db.AcntMasts on CH.Comp_Idno equals acntss.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on CD.StckDetl_Idno equals stck.SerlDetl_id
                           where (CH.ClaimToComHead_Idno == 0) || (CH.ClaimToComHead_Idno == null)
                           select new
                           {
                               ClaimHeadIdno = CH.ClaimHead_Idno,
                               SerialNo = stck.SerialNo,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               CBillDate = CH.ClaimHead_Date,
                               SbillNo = (from N in db.tblSBillHeads where N.SBillHead_Idno == CD.SBillHead_Idno select N.SBill_No).FirstOrDefault(),
                               Location = CH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = CH.Year_Idno,
                               CompName = acntss.Acnt_Name,
                               CompIdno = acntss.Acnt_Idno,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.Location == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.CBillDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.CBillDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }

        public IList SelectForSearchRecvd(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimToComHeads
                           join CD in db.tblClaimToComDetls on CH.ClaimToComHead_Idno equals CD.ClaimToComHead_Idno
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join acntss in db.AcntMasts on CH.Comp_Idno equals acntss.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on CD.StckDetl_Idno equals stck.SerlDetl_id
                           where CH.Against == 1
                           select new
                           {
                               ClaimHeadIdno = CH.ClaimToComHead_Idno,
                               SerialNo = stck.SerialNo,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               CBillDate = CH.ClaimToComHead_Date,
                               SbillNo = (from N in db.tblSBillHeads where N.SBillHead_Idno == stck.SaleBill_Idno select N.SBill_No).FirstOrDefault(),
                               Location = CH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = CH.Year_Idno,
                               CompName = acntss.Acnt_Name,
                               CompIdno = acntss.Acnt_Idno,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.Location == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.CBillDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.CBillDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }


        public Int32 GetClaimMax(Int64 fromCity, string Prefix, Int64 YeaIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var max = (from TCH in db.tblClaimToComHeads where TCH.FromLoc_Idno == fromCity && TCH.Prefix_No == Prefix && TCH.Year_Idno == YeaIdno select TCH.ClaimToCom_No).Max() + 1;
                if (max != null) { return Convert.ToInt32(max); } else { return 1; }

            }
        }

        public IList Exists(Int64 HeadIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblMatIssueAgnClaimHeads where CH.ClaimToComHead_Idno == HeadIdno select CH.MatIssAgnClaimHead_Idno).ToList();
                return lst; ;
            }
        }
        

        public IList Select(string ClaimIdno)
        {
            Int64[] ClaimIds = ClaimIdno.Split(',').Select(str => Int64.Parse(str)).ToArray();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimHeads
                           join CD in db.tblClaimDetls on CH.ClaimHead_Idno equals CD.ClaimHead_Idno
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on CD.StckDetl_Idno equals stck.SerlDetl_id
                           join IM in db.tblItemMastPurs on stck.ItemIdno equals IM.Item_Idno
                           select new
                           {
                               ClaimHeadIdno = CH.ClaimHead_Idno,
                               ClaimDetailsIdno=0,
                               SerialNo = stck.SerialNo,
                               ItemName = IM.Item_Name,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               CBillDate = CH.ClaimHead_Date,
                               SbillNo = (from N in db.tblSBillHeads where N.SBillHead_Idno == CD.SBillHead_Idno select N.SBill_No).FirstOrDefault(),
                               Location = CH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = CH.Year_Idno,
                               DefectRemark = CD.Defect_Remark,
                               VehDetl = CD.VechApp_Details,
                               Status = "1",
                               Remark="",
                               NewSerialNo="",
                           }).ToList();


                lst = lst.Where(s => ClaimIds.Contains(s.ClaimHeadIdno)).ToList();
                return lst; ;

            }
        }
        public IList SelectClaimDetailsForRecvd(string ClaimToComHeadId)
        {
            Int64[] ClaimIds = ClaimToComHeadId.Split(',').Select(str => Int64.Parse(str)).ToArray();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CTH in db.tblClaimToComHeads
                           join CTD in db.tblClaimToComDetls on CTH.ClaimToComHead_Idno equals CTD.ClaimToComHead_Idno
                           join acnts in db.AcntMasts on CTH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CTH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on CTD.StckDetl_Idno equals stck.SerlDetl_id
                           join IM in db.tblItemMastPurs on stck.ItemIdno equals IM.Item_Idno
                           select new
                           {
                               ClaimHeadIdno = CTH.ClaimToComHead_Idno,
                               ClaimDetailsIdno=CTD.ClaimToComDetl_Idno,
                               SerialNo = stck.SerialNo,
                               ItemName = IM.Item_Name,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CTH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CTH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               CBillDate = CTH.ClaimToComRec_Date,
                               SbillNo = (from N in db.tblSBillHeads where N.SBillHead_Idno == stck.SaleBill_Idno select N.SBill_No).FirstOrDefault(),
                               Location = CTH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = CTH.Year_Idno,
                               DefectRemark = CTD.Defect_Remark,
                               VehDetl = CTD.VechApp_Details,
                               Status = CTD.Status,
                               Remark = CTD.Remark,
                               NewSerialNo = CTD.New_SerialNo,
                           }).ToList();

                lst = lst.Where(s => ClaimIds.Contains(s.ClaimHeadIdno)).ToList();

                return lst; ;

            }
        }



        public IList SelectClaimDetails(Int64 ClaimToComHeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CTH in db.tblClaimToComHeads
                           join CTD in db.tblClaimToComDetls on CTH.ClaimToComHead_Idno equals CTD.ClaimToComHead_Idno
                           join acnts in db.AcntMasts on CTH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CTH.FromLoc_Idno equals cifrom.City_Idno
                           join stck in db.Stckdetls on CTD.StckDetl_Idno equals stck.SerlDetl_id
                           join IM in db.tblItemMastPurs on stck.ItemIdno equals IM.Item_Idno
                           where CTH.ClaimToComHead_Idno == ClaimToComHeadId
                           select new
                           {
                               ClaimHeadIdno = CTH.ClaimToComHead_Idno,
                               ClaimDetailsIdno=CTD.ClaimToComDetl_Idno,
                               SerialNo = stck.SerialNo,
                               ItemName = IM.Item_Name,
                               SerialIdno = stck.SerlDetl_id,
                               PrefNo = CTH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CTH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               CBillDate = CTH.ClaimToComHead_Date,
                               CBIllRecDate = CTH.ClaimToComRec_Date,
                               SbillNo = (from N in db.tblSBillHeads where N.SBillHead_Idno == stck.SaleBill_Idno select N.SBill_No).FirstOrDefault(),
                               Location = CTH.FromLoc_Idno,
                               CityName = cifrom.City_Name,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               YearIdno = CTH.Year_Idno,
                               DefectRemark = CTD.Defect_Remark,
                               VehDetl = CTD.VechApp_Details,
                               Status = CTD.Status,
                               Remark=CTD.Remark,
                               NewSerialNo=CTD.New_SerialNo,
                           }).ToList();



                return lst; ;

            }
        }

        public Int64 Insert(tblClaimToComHead obj, DataTable Dttemp)
        {
            Int64 ClaimToComHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblClaimToComHead Claimhead = new tblClaimToComHead();
                try
                {
                    tblClaimToComHead CH = db.tblClaimToComHeads.Where(ch => ch.ClaimToCom_No == obj.ClaimToCom_No && ch.Prefix_No == obj.Prefix_No && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                    if (CH != null)
                    {
                        ClaimToComHeadIdno = -1;
                    }
                    else
                    {
                        db.tblClaimToComHeads.AddObject(obj);
                        db.SaveChanges();
                        ClaimToComHeadIdno = obj.ClaimToComHead_Idno;
                        if (Dttemp != null && Dttemp.Rows.Count > 0)
                        {
                            foreach (DataRow Dr in Dttemp.Rows)
                            {
                                tblClaimToComDetl objtblClaimDetl = new tblClaimToComDetl();
                                objtblClaimDetl.ClaimToComHead_Idno = ClaimToComHeadIdno;
                                objtblClaimDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                objtblClaimDetl.ClaimToHead_Idno = Convert.ToInt64(Dr["ClaimIdno"]);
                                objtblClaimDetl.Defect_Remark = Convert.ToString(Dr["DefectRemark"]);
                                objtblClaimDetl.VechApp_Details = Convert.ToString(Dr["VechAppDetails"]);
                                objtblClaimDetl.Status = Convert.ToInt64(Dr["Status"]);
                                objtblClaimDetl.Remark = Convert.ToString(Dr["Remark"]);
                                objtblClaimDetl.New_SerialNo = Convert.ToString(Dr["NewSerialNo"]);

                                db.tblClaimToComDetls.AddObject(objtblClaimDetl);
                                Stckdetl SD = db.Stckdetls.Where(d => d.SerlDetl_id == objtblClaimDetl.StckDetl_Idno).FirstOrDefault();
                                SD.Claim_Status = 1;
                                db.SaveChanges();
                            }
                        }

                    }
                }
                catch
                {
                    ClaimToComHeadIdno = 0;
                }

                return ClaimToComHeadIdno;
            }
        }

        public Int64 Update(tblClaimToComHead obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 ClaimHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblClaimToComHead CH = db.tblClaimToComHeads.Where(ch => ch.ClaimToCom_No == obj.ClaimToCom_No && ch.ClaimToComHead_Idno != Head_Idno && ch.Against == obj.Against && ch.FromLoc_Idno == obj.FromLoc_Idno && ch.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (CH != null)
                        {
                            ClaimHeadIdno = -1;
                        }
                        else
                        {
                            tblClaimToComHead CHead = db.tblClaimToComHeads.Where(ch => ch.ClaimToComHead_Idno == Head_Idno).FirstOrDefault();
                            if (CHead != null)
                            {
                                db.SaveChanges();
                                ClaimHeadIdno = CHead.ClaimToComHead_Idno;

                                List<tblClaimToComDetl> ClaimDetl = db.tblClaimToComDetls.Where(cd => cd.ClaimToHead_Idno == Head_Idno).ToList();
                                foreach (tblClaimToComDetl tcd in ClaimDetl)
                                {
                                    db.tblClaimToComDetls.DeleteObject(tcd);

                                    Stckdetl SD = db.Stckdetls.Where(d => d.SerlDetl_id == tcd.StckDetl_Idno).FirstOrDefault();
                                    SD.Claim_Status = 1;
                                    db.SaveChanges();
                                }
                                if (Dttemp != null && Dttemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in Dttemp.Rows)
                                    {
                                        tblClaimToComDetl objtblClaimDetl = new tblClaimToComDetl();
                                        objtblClaimDetl.ClaimToComHead_Idno = ClaimHeadIdno;
                                        objtblClaimDetl.StckDetl_Idno = Convert.ToInt64(Dr["SerialIdno"]);
                                        objtblClaimDetl.ClaimToHead_Idno = Convert.ToInt64(Dr["ClaimIdno"]);
                                        objtblClaimDetl.Defect_Remark = Convert.ToString(Dr["DefectRemark"]);
                                        objtblClaimDetl.VechApp_Details = Convert.ToString(Dr["VechAppDetails"]);
                                        objtblClaimDetl.Status = Convert.ToInt64(Dr["Status"]);
                                        objtblClaimDetl.Remark = Convert.ToString(Dr["Remark"]);
                                        objtblClaimDetl.New_SerialNo = Convert.ToString(Dr["NewSerialNo"]);
                                        db.tblClaimToComDetls.AddObject(objtblClaimDetl);

                                        Stckdetl SD = db.Stckdetls.Where(d => d.SerlDetl_id == objtblClaimDetl.StckDetl_Idno).FirstOrDefault();
                                        SD.Claim_Status = 1;
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
            return ClaimHeadIdno;
        }


        public Int64 UpdateForRecvd(tblClaimToComHead obj, Int32 Head_Idno, DataTable Dttemp)
        {
            Int64 ClaimHeadIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                try
                {
                    tblClaimToComHead CHead = db.tblClaimToComHeads.Where(ch => ch.ClaimToComHead_Idno == Head_Idno).FirstOrDefault();
                    if (CHead != null)
                    {
                        CHead.ClaimToComRec_Date = obj.ClaimToComRec_Date;
                        CHead.Against = obj.Against;
                        db.SaveChanges();
                        ClaimHeadIdno = CHead.ClaimToComHead_Idno;

                        List<tblClaimToComDetl> ClaimDetl = db.tblClaimToComDetls.Where(cd => cd.ClaimToComHead_Idno == Head_Idno).ToList();
                        foreach (tblClaimToComDetl tcd in ClaimDetl)
                        {
                            for (int i = 0; i < Dttemp.Rows.Count; i++)
                            {
                                Int64 Value = Convert.ToInt64(Dttemp.Rows[i]["ClaimDetailsIdno"]);
                                List<tblClaimToComDetl> ClaimDetls = db.tblClaimToComDetls.Where(cd => cd.ClaimToComDetl_Idno == Value).ToList();
                                foreach (tblClaimToComDetl tcds in ClaimDetls)
                                {
                                    tcds.Status = Convert.ToInt64(Dttemp.Rows[i]["Status"]);
                                    tcds.New_SerialNo = Convert.ToString(Dttemp.Rows[i]["NewSerialNo"]);
                                    tcds.Remark = Convert.ToString(Dttemp.Rows[i]["Remark"]);
                                    db.SaveChanges();

                                    string NewSerialNo =Dttemp.Rows[i]["NewSerialNo"].ToString();
                                    if (string.IsNullOrEmpty(NewSerialNo) == false)
                                    {
                                        Stckdetl SD = db.Stckdetls.Where(d => d.SerialNo == NewSerialNo).FirstOrDefault();
                                        tcds.NewStckDetl_Idno = SD.SerlDetl_id;
                                        db.SaveChanges();
                                    }
                                }
                            }

                        }

                    }

                }
                catch
                {
                    ClaimHeadIdno = 0;
                }

            }
            return ClaimHeadIdno;
        }


        public tblClaimToComHead Select_ClaimHead(Int64 ClaimHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblClaimToComHeads.Where(tch => (tch.ClaimToComHead_Idno == ClaimHead_Idno)).FirstOrDefault();
            }
        }

        public IList Select_ClaimDetl(Int64 ClaimHead_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Ilst = (from TCD in db.tblClaimToComDetls
                            join SD in db.Stckdetls on TCD.StckDetl_Idno equals SD.SerlDetl_id
                            join CM in db.tblCityMasters on SD.Loc_Idno equals CM.City_Idno
                            join IM in db.tblItemMastPurs on SD.ItemIdno equals IM.Item_Idno
                            join TSB in db.tblClaimToComHeads on TCD.ClaimToComHead_Idno equals TSB.ClaimToComHead_Idno
                            join AM in db.AcntMasts on TSB.Prty_Idno equals AM.Acnt_Idno
                            //join AM1 in db.AcntMasts on TSB.Prty_Idno equals AM1.Acnt_Idno
                            where TCD.ClaimToComHead_Idno == ClaimHead_Idno
                            select new
                            {
                                SD.SerlDetl_id,
                                SD.SerialNo,
                            }).ToList();

                return Ilst;
            }
        }

        public IList SearchList(DateTime? DateFrom, DateTime? DateTo, string strClaimNo, Int64 intPrtyIdno, Int64 intFromLocation, Int64 intCompIdno, Int64 intYearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblClaimToComHeads
                           join acnts in db.AcntMasts on CH.Prty_Idno equals acnts.Acnt_Idno
                           join cifrom in db.tblCityMasters on CH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts1 in db.AcntMasts on CH.Comp_Idno equals acnts1.Acnt_Idno
                           select new
                           {
                               CH.ClaimToComHead_Idno,
                               ClaimDate = CH.ClaimToComHead_Date,
                               PrefNo = CH.Prefix_No,
                               ClaimNo = (from N in db.tblClaimHeads where N.ClaimHead_Idno == CH.ClaimHead_Idno select N.Claim_No).FirstOrDefault(),
                               YearIdno = CH.Year_Idno,
                               PartyIdno = acnts.Acnt_Idno,
                               PartyName = acnts.Acnt_Name,
                               CompanyIdno = acnts1.Acnt_Idno,
                               CompanyName = acnts1.Acnt_Name,
                               CityIdno = cifrom.City_Idno,
                               CityName = cifrom.City_Name,
                           }).ToList();
                if (intYearIdno > 0)
                {
                    lst = (from l in lst where l.YearIdno == intYearIdno select l).ToList();
                }
                if (intFromLocation > 0)
                {
                    lst = (from l in lst where l.CityIdno == intFromLocation select l).ToList();
                }
                if (DateFrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date >= Convert.ToDateTime(DateFrom).Date).ToList();
                }
                if (DateTo != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.ClaimDate).Date <= Convert.ToDateTime(DateTo).Date).ToList();
                }
                if (intPrtyIdno > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPrtyIdno select l).ToList();
                }
                if (intCompIdno > 0)
                {
                    lst = (from l in lst where l.CompanyIdno == intCompIdno select l).ToList();
                }
                if (strClaimNo != "")
                {
                    lst = (from l in lst where ((l.PrefNo) + "" + (l.ClaimNo)) == strClaimNo select l).ToList();
                }
                return lst;
            }
        }

        public int Delete(Int64 HeadId)
        {
            int value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblClaimToComHead qth = db.tblClaimToComHeads.Where(h => h.ClaimToComHead_Idno == HeadId).FirstOrDefault();
                List<tblClaimToComDetl> qtd = db.tblClaimToComDetls.Where(d => d.ClaimToComHead_Idno == HeadId).ToList();
                if (qth != null)
                {
                    foreach (var d in qtd)
                    {
                        db.tblClaimToComDetls.DeleteObject(d);

                        Stckdetl SD = db.Stckdetls.Where(B => B.SerlDetl_id == d.StckDetl_Idno).FirstOrDefault();
                        SD.Claim_Status = 1;
                        db.SaveChanges();
                    }
                    db.tblClaimToComHeads.DeleteObject(qth);
                    db.SaveChanges();
                    value = 1;
                }
            }
            return value;
        }




    }
}
