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
    public class SaleRegDAL
    {
        public IList SelectSBillReg(Int32 Yearidno, DateTime? DateFrom, DateTime? DateTo, Int32 FromLocation, Int32 BillType, string PrefNo, Int32 intSBillNo, Int64 intPartyId,Int32 intAgainst)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from SBH in db.tblSBillHeads
                           join cifrom in db.tblCityMasters on SBH.FromLoc_Idno equals cifrom.City_Idno
                           join acnts in db.AcntMasts on SBH.Prty_Idno equals acnts.Acnt_Idno
                           select new
                           {
                               PrefNo = SBH.Prefix_No,
                               SbillNo = SBH.SBill_No,
                               AgaintIdno = SBH.Bill_Against,
                               Against = ((SBH.Bill_Against == 1) ? "Counter" : "Matrial Issue"),
                               SbillTypeIdno = SBH.SBill_Type,
                               SbillType = ((SBH.SBill_Type == 1) ? "Credit" : "Cash"),
                               FromLocation = cifrom.City_Name,
                               Date = SBH.SBillHead_Date,
                               FromLocationIdno = cifrom.City_Idno,
                               PartyName = acnts.Acnt_Name,
                               PartyIdno = acnts.Acnt_Idno,
                               Billed = SBH.Billed,
                               TotTaxableAmnt = (from SBD in db.tblSBillDetls where SBD.SBillHead_Idno == SBH.SBillHead_Idno select SBD.Item_Rate).Sum(),
                               TotTax = (from SBD in db.tblSBillDetls where  SBD.SBillHead_Idno==SBH.SBillHead_Idno select SBD.Item_Tax).Sum(),
                               DiscAmnt = SBH.Disc_Amnt,
                               OtherAmnt = SBH.Other_Amnt,
                               NetAmnt = SBH.Net_Amnt,
                               YearIdno = SBH.Year_Idno,
                           }).ToList();
                if (Yearidno > 0)
                {
                    lst = (from l in lst where l.YearIdno == Yearidno select l).ToList();
                }
                if (intSBillNo > 0)
                {
                    lst = (from l in lst where l.SbillNo == intSBillNo select l).ToList();
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
                if (intPartyId > 0)
                {
                    lst = (from l in lst where l.PartyIdno == intPartyId select l).ToList();
                }
                if (intAgainst > 0)
                {
                    lst = (from l in lst where l.AgaintIdno == intAgainst select l).ToList();
                }
                return lst;
            }
        }
    }
}
