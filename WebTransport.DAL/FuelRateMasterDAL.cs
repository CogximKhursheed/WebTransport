using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using VisageWeb.Utility;
using System.Transactions;
//using VisageWeb.Utility;

namespace WebTransport.DAL
{
    public class FuelRateMasterDAL
    {

        public List<AcntMast> BindPump()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = new List<AcntMast>();
                lst = (from cm in db.AcntMasts
                       where cm.Acnt_Type == 10 && cm.Status == true
                       orderby cm.Acnt_Name
                       select cm).ToList();
                return lst;
            }
        }

        public Int64 Insert(tblFuelRateMaster Master)
        {
            Int64 Idno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var objtbl = (from obj in db.tblFuelRateMasters where obj.Acnt_Idno == Master.Acnt_Idno && obj.FuelRate_Date == Master.FuelRate_Date && obj.ItemIdno != null && obj.ItemIdno == Master.ItemIdno select obj).ToList();
                    if (objtbl.Count == 0)
                    {
                        db.AddTotblFuelRateMasters(Master);
                        db.SaveChanges();
                        Idno = Convert.ToInt32(Master.FuelRate_Idno);
                    }
                    else
                    {
                        Idno = -1;        
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Idno;
        }

        public tblFuelRateMaster SelectByID(int IdNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblFuelRateMasters
                        where obj.FuelRate_Idno == IdNo
                        select obj).SingleOrDefault();
            }
        }

        public Int64 Update(tblFuelRateMaster Master, Int32 id)
        {
            Int64 Idno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblFuelRateMaster objtbl = (from obj in db.tblFuelRateMasters
                                                where obj.FuelRate_Idno == id
                                                select obj).SingleOrDefault();
                    if (objtbl != null)
                    {  
                        objtbl.FuelRate_Date = Master.FuelRate_Date;
                        objtbl.Acnt_Idno = Master.Acnt_Idno;
                        objtbl.Fuel_Rate = Master.Fuel_Rate;
                        objtbl.Status = Master.Status;
                        objtbl.ItemIdno = Master.ItemIdno;
                        objtbl.Year_Idno = Master.Year_Idno;
                        objtbl.Date_Modified = System.DateTime.Now;
                        db.SaveChanges();
                        Idno = Convert.ToInt32(objtbl.FuelRate_Idno);
                    }
                }
            }
            catch (Exception ex)
            {
                Idno = 0;
            }
            return Idno;
        }

        public IList BindRecords(int AcntIdno, int YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblFuelRateMasters join AM in db.AcntMasts on obj.Acnt_Idno equals AM.Acnt_Idno where obj.Acnt_Idno == AcntIdno && obj.Year_Idno == YearIdno select new { obj.Fuel_Rate, obj.FuelRate_Date, AM.Acnt_Name, obj.FuelRate_Idno }).ToList();
            }
        }

        public int Delete(int Idno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblFuelRateMaster TaxMast = (from Obj in db.tblFuelRateMasters
                                                 where Obj.FuelRate_Idno == Idno
                                                 select Obj).SingleOrDefault();
                    if (TaxMast != null)
                    {
                        db.tblFuelRateMasters.DeleteObject(TaxMast);
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch
            {
                
            }
            return value;
        }

        public IList SelectItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblItemMastPurs
                        where mast.ItemType == 3
                        orderby mast.Item_Name
                        select mast).ToList();
            }
        }
        //public IList SelectAllTaxByID(int StateIdno, int CompID)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        IList lst = (from objTaxTyp in db.tblFuelRateMasters
        //                     join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                     join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                     join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                     from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                     where objTaxTyp.State_Idno == StateIdno //&& objTaxTyp.Comp_Idno == CompID
        //                     orderby objState.State_Name
        //                     select new
        //                     {
        //                         TaxNm = objTax.TaxType_Name,
        //                         TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                         StateNm = objState.State_Name,
        //                         TaxRate = objTaxTyp.Tax_Rate,
        //                         TaxDate = objTaxTyp.Tax_Date,
        //                         Stateid = objState.State_Idno,
        //                         TaxMastId = objTaxTyp.TaxMast_Idno,
        //                         PANType = objPANType.PANType_Name,
        //                         LorryFrom = objTaxTyp.LorryCnt_From,
        //                         LorryTo = objTaxTyp.LorryCnt_To,
        //                         CalculateOnDF = objTaxTyp.CalOn_DF
        //                     }).ToList();
        //        return lst;
        //    }
        //}
        //public IList SelectAllTaxTypeByID(int stateidno, int TaxtypeIdno, int CompID)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        if (stateidno == 0)
        //        {
        //            IList lst = (from objTaxTyp in db.tblFuelRateMasters
        //                         join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                         join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                         join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                         from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                         where objTax.TaxType_Idno == TaxtypeIdno
        //                         orderby objTax.TaxType_Name
        //                         select new
        //                         {
        //                             TaxNm = objTax.TaxType_Name,
        //                             TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                             StateNm = objState.State_Name,
        //                             TaxRate = objTaxTyp.Tax_Rate,
        //                             TaxDate = objTaxTyp.Tax_Date,
        //                             Stateid = objState.State_Idno,
        //                             TaxMastId = objTaxTyp.TaxMast_Idno,
        //                             PANType = objPANType.PANType_Name,
        //                             LorryFrom = objTaxTyp.LorryCnt_From,
        //                             LorryTo = objTaxTyp.LorryCnt_To,
        //                             CalculateOnDF = objTaxTyp.CalOn_DF
        //                         }).ToList();
        //            return lst;
        //        }
        //        else
        //        {
        //            IList lst = (from objTaxTyp in db.tblFuelRateMasters
        //                         join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                         join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                         join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                         from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                         where objTax.TaxType_Idno == TaxtypeIdno && objState.State_Idno == stateidno
        //                         orderby objTax.TaxType_Name
        //                         select new
        //                         {
        //                             TaxNm = objTax.TaxType_Name,
        //                             TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                             StateNm = objState.State_Name,
        //                             TaxRate = objTaxTyp.Tax_Rate,
        //                             TaxDate = objTaxTyp.Tax_Date,
        //                             Stateid = objState.State_Idno,
        //                             TaxMastId = objTaxTyp.TaxMast_Idno,
        //                             PANType = objPANType.PANType_Name,
        //                             LorryFrom = objTaxTyp.LorryCnt_From,
        //                             LorryTo = objTaxTyp.LorryCnt_To,
        //                             CalculateOnDF = objTaxTyp.CalOn_DF
        //                         }).ToList();
        //            return lst;
        //        }
        //    }
        //}

     
        //public int Delete(int TaxMastID)
        //{
        //    int value = 0;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            tblFuelRateMaster TaxMast = (from objTaxTyp in db.tblFuelRateMasters
        //                                    where objTaxTyp.TaxMast_Idno == TaxMastID
        //                                    select objTaxTyp).SingleOrDefault();
        //            if (TaxMast != null)
        //            {
        //                db.tblFuelRateMasters.DeleteObject(TaxMast);
        //                db.SaveChanges();
        //                value = 1;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return value;
        //}
        
        //public IList Select(int CompID, int StateIdno, int TaxTypeIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        //from person in people
        //        //        join pet in pets on person equals pet.Owner into gj
        //        //        from subpet in gj.DefaultIfEmpty()

        //        if (StateIdno == 0)
        //        {
        //            var lst = (from objTaxTyp in db.tblFuelRateMasters
        //                       join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                       join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                       join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                       from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                       orderby objState.State_Name
        //                       select new
        //                       {
        //                           TaxNm = objTax.TaxType_Name,
        //                           TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                           StateNm = objState.State_Name,
        //                           TaxRate = objTaxTyp.Tax_Rate,
        //                           TaxDate = objTaxTyp.Tax_Date,
        //                           Stateid = objState.State_Idno,
        //                           TaxMastId = objTaxTyp.TaxMast_Idno,
        //                           PANType = objPANType.PANType_Name,
        //                           LorryFrom = objTaxTyp.LorryCnt_From,
        //                           LorryTo = objTaxTyp.LorryCnt_To,
        //                           CalculateOnDF = objTaxTyp.CalOn_DF
        //                       }).ToList();
        //            if (TaxTypeIdno > 0)
        //            {
        //                lst = lst.Where(l => l.TaxTyp_Idno == TaxTypeIdno).ToList();
        //            }
        //            return lst;
        //        }
        //        else if (StateIdno != 0)
        //        {
        //            var lst = (from objTaxTyp in db.tblFuelRateMasters
        //                       join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                       join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                       join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                       from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                       where objTaxTyp.State_Idno == StateIdno
        //                       orderby objState.State_Name
        //                       select new
        //                       {
        //                           TaxNm = objTax.TaxType_Name,
        //                           TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                           StateNm = objState.State_Name,
        //                           TaxRate = objTaxTyp.Tax_Rate,
        //                           TaxDate = objTaxTyp.Tax_Date,
        //                           Stateid = objState.State_Idno,
        //                           TaxMastId = objTaxTyp.TaxMast_Idno,
        //                           PANType = objPANType.PANType_Name,
        //                           LorryFrom = objTaxTyp.LorryCnt_From,
        //                           LorryTo = objTaxTyp.LorryCnt_To,
        //                           CalculateOnDF = objTaxTyp.CalOn_DF
        //                       }).ToList();
        //            if (TaxTypeIdno > 0)
        //            {
        //                lst = lst.Where(l => l.TaxTyp_Idno == TaxTypeIdno).ToList();
        //            }
        //            return lst;

        //        }
        //        else if (TaxTypeIdno == 0)
        //        {
        //            IList lst = (from objTaxTyp in db.tblFuelRateMasters
        //                         join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                         join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                         join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                         from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                         orderby objTax.TaxType_Name
        //                         select new
        //                         {
        //                             TaxNm = objTax.TaxType_Name,
        //                             TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                             StateNm = objState.State_Name,
        //                             TaxRate = objTaxTyp.Tax_Rate,
        //                             TaxDate = objTaxTyp.Tax_Date,
        //                             Stateid = objState.State_Idno,
        //                             TaxMastId = objTaxTyp.TaxMast_Idno,
        //                             PANType = objPANType.PANType_Name,
        //                             LorryFrom = objTaxTyp.LorryCnt_From,
        //                             LorryTo = objTaxTyp.LorryCnt_To,
        //                             CalculateOnDF = objTaxTyp.CalOn_DF
        //                         }).ToList();
        //            return lst;
        //        }
        //        else
        //        {
        //            IList lst = (from objTaxTyp in db.tblFuelRateMasters
        //                         join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
        //                         join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
        //                         join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
        //                         from objPANType in objPANLeftJoin.DefaultIfEmpty()
        //                         where objTaxTyp.TaxTyp_Idno == TaxTypeIdno
        //                         orderby objState.State_Name
        //                         select new
        //                         {
        //                             TaxNm = objTax.TaxType_Name,
        //                             TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
        //                             StateNm = objState.State_Name,
        //                             TaxRate = objTaxTyp.Tax_Rate,
        //                             TaxDate = objTaxTyp.Tax_Date,
        //                             Stateid = objState.State_Idno,
        //                             TaxMastId = objTaxTyp.TaxMast_Idno,
        //                             PANType = objPANType.PANType_Name,
        //                             LorryFrom = objTaxTyp.LorryCnt_From,
        //                             LorryTo = objTaxTyp.LorryCnt_To,
        //                             CalculateOnDF = objTaxTyp.CalOn_DF
        //                         }).ToList();
        //            return lst;

        //        }
        //    }
        //}

        //public Int64 Insert(int taxtypeIdno, int stateIdno, double rate, int compID, DateTime TaxDate, int PANtype, int lorryfrom, int lorryto, bool CalonDf, Int32 empIdno)
        //{
        //    Int64 TaxIdno = 0;
        //    try
        //    {
        //        if (IsExists(0, stateIdno, TaxDate, taxtypeIdno, PANtype, lorryfrom, lorryto) == false)
        //        {
        //            tblFuelRateMaster TaxMast = new tblFuelRateMaster();
        //            TaxMast.TaxTyp_Idno = taxtypeIdno;
        //            TaxMast.State_Idno = stateIdno;
        //            TaxMast.Emp_Idno = empIdno;
        //            TaxMast.Tax_Rate = rate;
        //            TaxMast.Date_Added = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
        //            TaxMast.Date_Modified = DateTime.Now.Date;
        //            TaxMast.Comp_Idno = compID;
        //            TaxMast.Tax_Date = TaxDate;
        //            TaxMast.PANTyp_Idno = PANtype;
        //            TaxMast.LorryCnt_From = lorryfrom;
        //            TaxMast.LorryCnt_To = lorryto;
        //            TaxMast.CalOn_DF = CalonDf;
        //            TaxMast.Status = 1;
        //            FuelRateMasterDAL objTaxMastDLL = new FuelRateMasterDAL();
        //            TaxIdno = objTaxMastDLL.Insert(TaxMast);
        //            objTaxMastDLL = null;
        //            TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
        //        }
        //        else
        //        {
        //            TaxIdno = -1;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return TaxIdno;
        //}

        
        //public Int64 Insert(tblFuelRateMaster TaxMast)
        //{
        //    Int64 TaxIdno = 0;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            var objtbl = (from obj in db.tblFuelRateMasters where obj.State_Idno == TaxMast.State_Idno && obj.TaxTyp_Idno == TaxMast.TaxTyp_Idno select obj).ToList();
        //            if (objtbl.Count > 0)
        //            {
        //                for (int i = 0; i < objtbl.Count; i++)
        //                {
        //                    objtbl[i].Status = 0;
        //                    db.SaveChanges();

        //                }
        //                //objtbl[0].Status = 0;
        //            }

        //            db.AddTotblFuelRateMasters(TaxMast);
        //            db.SaveChanges();
        //            TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ApplicationFunction.ErrorLog(ex.ToString());
        //    }
        //    return TaxIdno;
        //}

        //public Int64 Update(tblFuelRateMaster TaxMast, Int32 id)
        //{
        //    Int64 TaxIdno = 0;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        { 
        //            tblFuelRateMaster objTaxMast = (from objTaxTyp in db.tblFuelRateMasters
        //                                       where objTaxTyp.TaxTyp_Idno == TaxMast.TaxTyp_Idno && objTaxTyp.State_Idno == TaxMast.State_Idno
        //                                       && objTaxTyp.Tax_Date == TaxMast.Tax_Date && objTaxTyp.PANTyp_Idno == TaxMast.PANTyp_Idno
        //                                       && objTaxTyp.TaxMast_Idno == id
        //                                       select objTaxTyp).SingleOrDefault();
        //            if (objTaxMast != null)
        //            {
        //                objTaxMast.TaxTyp_Idno = TaxMast.TaxTyp_Idno;
        //                objTaxMast.State_Idno = TaxMast.State_Idno;
        //                objTaxMast.Tax_Rate = TaxMast.Tax_Rate;
        //                objTaxMast.Date_Added = TaxMast.Date_Added;
        //                objTaxMast.Date_Modified = TaxMast.Date_Modified;
        //                objTaxMast.Comp_Idno = TaxMast.Comp_Idno;
        //                objTaxMast.Tax_Date = TaxMast.Tax_Date;
        //                objTaxMast.Emp_Idno = TaxMast.Emp_Idno;
        //                objTaxMast.PANTyp_Idno = TaxMast.PANTyp_Idno;
        //                objTaxMast.LorryCnt_From = TaxMast.LorryCnt_From;
        //                objTaxMast.LorryCnt_To = TaxMast.LorryCnt_To;
        //                objTaxMast.CalOn_DF = TaxMast.CalOn_DF;
        //                db.SaveChanges();
        //                TaxIdno = Convert.ToInt32(objTaxMast.TaxTyp_Idno);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ApplicationFunction.ErrorLog(ex.ToString());
        //    }
        //    return TaxIdno;
        //}

        
        //public Int64 Update(int id, int taxtypeIdno, int stateIdno, double rate, int compID, DateTime TaxDate, int PANtype, int lorryfrom, int lorryto, bool CalonDf, Int32 empIdno)
        //{
        //    Int64 TaxIdno = 0;
        //    try
        //    {
        //        if (IsExists(id, stateIdno, TaxDate, taxtypeIdno, PANtype, lorryfrom, lorryto) == false)
        //        {
        //            tblFuelRateMaster TaxMast = new tblFuelRateMaster();
        //            TaxMast.TaxTyp_Idno = taxtypeIdno;
        //            TaxMast.State_Idno = stateIdno;
        //            TaxMast.Tax_Rate = rate;
        //            TaxMast.Emp_Idno = empIdno;
        //            TaxMast.Date_Added = DateTime.Now.Date;
        //            TaxMast.Date_Modified = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
        //            TaxMast.Comp_Idno = compID;
        //            TaxMast.Tax_Date = TaxDate;
        //            TaxMast.PANTyp_Idno = PANtype;
        //            TaxMast.LorryCnt_From = lorryfrom;
        //            TaxMast.LorryCnt_To = lorryto;
        //            TaxMast.CalOn_DF = CalonDf;
        //            FuelRateMasterDAL objTaxMastDLL = new FuelRateMasterDAL();
        //            TaxIdno = objTaxMastDLL.Update(TaxMast, id);
        //            TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
        //            objTaxMastDLL = null;
        //        }
        //        else
        //        {
        //            TaxIdno = -1;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return TaxIdno;
        //}

        
        //public bool IsExists(int Id, int stateIdno, DateTime TaxDate, int taxtypeIdno, int PANtype, int lorryfrom, int lorryto)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        tblFuelRateMaster TaxMast = new tblFuelRateMaster();
        //        if (Id <= 0)
        //        {
        //            TaxMast = (from objTaxTyp in db.tblFuelRateMasters
        //                       where objTaxTyp.TaxTyp_Idno == taxtypeIdno
        //                               && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
        //                               && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_From == lorryfrom && objTaxTyp.LorryCnt_To == lorryto
        //                       select objTaxTyp).SingleOrDefault();

        //            var lst = (from objTaxTyp in db.tblFuelRateMasters
        //                       where objTaxTyp.TaxTyp_Idno == taxtypeIdno
        //                               && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
        //                               && objTaxTyp.Tax_Date == TaxDate
        //                       select objTaxTyp).ToList();
        //            if (lst.Count > 0)
        //            {
        //                foreach (var check in lst)
        //                {
        //                    if (lorryfrom <= check.LorryCnt_From || lorryto <= check.LorryCnt_To || lorryfrom <= check.LorryCnt_To)
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }

        //        }
        //        else if (Id > 0)
        //        {
        //            TaxMast = (from objTaxTyp in db.tblFuelRateMasters
        //                       where objTaxTyp.TaxTyp_Idno == taxtypeIdno
        //                               && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
        //                               && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_From == lorryfrom && objTaxTyp.LorryCnt_To == lorryto
        //                               && objTaxTyp.TaxMast_Idno != Id
        //                       select objTaxTyp).SingleOrDefault();
        //            var lst = (from objTaxTyp in db.tblFuelRateMasters
        //                       where objTaxTyp.TaxTyp_Idno == taxtypeIdno
        //                               && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
        //                               && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_To <= lorryto
        //                               && objTaxTyp.TaxMast_Idno != Id
        //                       select objTaxTyp).ToList();
        //            if (lst.Count > 0)
        //            {
        //                foreach (var check in lst)
        //                {
        //                    if (lorryfrom <= check.LorryCnt_From || lorryto <= check.LorryCnt_To || lorryfrom <= check.LorryCnt_To)
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //}

    }

}
