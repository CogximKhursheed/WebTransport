using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using WebTransport;


namespace WebTransport.DAL
{
    public class TyreMoveRpt
    {
        public List<tblItemMastPur> BindActiveItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               join ITM in db.tblItemTypeMasts on obj.ItemType equals ITM.ItemTpye_Idno
                               where obj.Status == true && ITM.ItemTpye_Idno == 1
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }

        public Int64 TotalRecords(DateTime? dtfrom, DateTime? dtto, Int32 yearidno, Int64 ItemIdno, string serailNum)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int Count = 0;
                var lst = (from stc in db.Stckdetls
                           join loc in db.tblCityMasters on stc.Loc_Idno equals loc.City_Idno
                           join Itm in db.tblItemMastPurs on stc.ItemIdno equals Itm.Item_Idno
                           join mat in db.MatIssHeads on stc.MtrlIssue_Idno equals mat.MatIss_Idno into material
                           from MTRL in material.DefaultIfEmpty()
                           join Lm in db.LorryMasts on MTRL.Truck_Idno equals Lm.Lorry_Idno into LRY
                           from Lorry in LRY.DefaultIfEmpty()
                           join PBH in db.tblPBillHeads on stc.PBillIdno equals PBH.PBillHead_Idno
                           join acnt in db.AcntMasts on Lorry.Driver_Idno equals acnt.Acnt_Idno into driver
                           from DRVR in driver.DefaultIfEmpty()
                           select new
                           {
                                stc.ItemIdno,
                                DocDate = PBH.PBillHead_Date,
                                YearIdno = PBH.Year_Idno,
                                SerialNum = stc.SerialNo
                           }).ToList();
                if (yearidno > 0)
                {
                    Count = lst.Where(l => l.YearIdno == yearidno).Count();
                }
                if (dtto != null)
                {
                    Count = lst.Where(l => Convert.ToDateTime(l.DocDate).Date <= Convert.ToDateTime(dtto).Date).Count();
                }
                if (dtfrom != null)
                {
                    Count = lst.Where(l => Convert.ToDateTime(l.DocDate).Date >= Convert.ToDateTime(dtfrom).Date).Count();
                }
                if (serailNum != "")
                {
                    Count = lst.Where(l => l.SerialNum == serailNum).Count();
                }
                if (ItemIdno > 0)
                {
                    Count = lst.Where(l => l.ItemIdno == ItemIdno).Count();
                }

                return Count;
            }
        }
        public IList SelectTyreMovementReport(DateTime? dtfrom, DateTime? dtto, Int32 yearidno, Int64 ItemIdno, string serailNum)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stc in db.Stckdetls
                           join loc in db.tblCityMasters on stc.Loc_Idno equals loc.City_Idno
                           join Itm in db.tblItemMastPurs on stc.ItemIdno equals Itm.Item_Idno
                           join mat in db.MatIssHeads on stc.MtrlIssue_Idno equals mat.MatIss_Idno into material
                           from MTRL in material.DefaultIfEmpty()
                           join Lm in db.LorryMasts on MTRL.Truck_Idno equals Lm.Lorry_Idno into LRY 
                           from Lorry in LRY.DefaultIfEmpty()
                           join PBH in db.tblPBillHeads on stc.PBillIdno equals PBH.PBillHead_Idno
                           join acnt in db.AcntMasts on Lorry.Driver_Idno equals acnt.Acnt_Idno into driver
                           from DRVR in driver.DefaultIfEmpty()
                           
                           select new
                           {
                               SerialNum = stc.SerialNo,
                               ItemName = Itm.Item_Name,
                               ItemIdno = stc.ItemIdno,
                               LorryNum = Lorry.Lorry_No,
                               //LorryNum = Lm.Lorry_No,
                               DrvrName = DRVR.Acnt_Name,
                               DocNo = PBH.PBillHead_No,
                               DocDate = PBH.PBillHead_Date,
                               YearIdno = PBH.Year_Idno,
                               //Location = loc.Loc_Name,
                               Location = loc.City_Name,
                               StckOut = ((MTRL.MatIss_Typ == 1) ? "Material Issue" : "NA"),
                               StckIn = stc.Item_from

                           }).ToList();
                if (yearidno > 0)
                {
                    lst = lst.Where(l => l.YearIdno == yearidno).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.DocDate).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.DocDate).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }
                if (serailNum != "")
                {
                    lst = lst.Where(l => l.SerialNum == serailNum).ToList();
                }
                if (ItemIdno > 0)
                {
                    lst = lst.Where(l => l.ItemIdno == ItemIdno).ToList();
                }

                return lst;
            }
        }

        public Stckdetl GetItemInfo(string serialNum)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from stc in db.Stckdetls where stc.SerialNo == serialNum select stc).FirstOrDefault();

            }
        }
    }
}
