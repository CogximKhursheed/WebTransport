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
    public class TaxMastDAL
    {
        public List<tblStateMaster> SelectState(Int64 StateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblStateMaster> lst = null;
                if (StateIdno > 0)
                {
                    lst = (from sm in db.tblStateMasters where sm.State_Idno == StateIdno && sm.Status == true select sm).ToList();
                }
                else
                {
                    lst = (from sm in db.tblStateMasters where sm.Status == true orderby sm.State_Name select sm).ToList();
                }
                return lst;
            }
        }

        public List<tblCityMaster> BindCitywithStateId(int stateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = new List<tblCityMaster>();
                if (stateIdno > 0)
                {
                    lst = (from cm in db.tblCityMasters
                           where cm.State_Idno == stateIdno && cm.Status == true
                           orderby cm.City_Name
                           select cm).ToList();
                }
                return lst;
            }
        }

        public List<tblCityMaster> BindToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }

        public List<tblTaxType> SelectTax()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from objtax in db.tblTaxTypes orderby objtax.TaxType_Name select objtax).ToList();
            }
        }
        public List<tblPANType> SelectPANType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from objPAN in db.tblPANTypes orderby objPAN.PANType_Name select objPAN).ToList();
            }
        }

        /// <summary>
        /// Select All From Tax Master By TaxId and StateIdno
        /// </summary>
        /// <returns>retun All </returns>
        public tblTaxMaster SelectTaxByID(int TaxMastID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from objTaxTyp in db.tblTaxMasters
                        where objTaxTyp.TaxMast_Idno == TaxMastID
                        select objTaxTyp).SingleOrDefault();
            }
        }

        public IList SelectAllTaxByID(int StateIdno, int CompID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                IList lst = (from objTaxTyp in db.tblTaxMasters
                             join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                             join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                             join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                             from objPANType in objPANLeftJoin.DefaultIfEmpty()
                             where objTaxTyp.State_Idno == StateIdno //&& objTaxTyp.Comp_Idno == CompID
                             orderby objState.State_Name
                             select new
                             {
                                 TaxNm = objTax.TaxType_Name,
                                 TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                 StateNm = objState.State_Name,
                                 TaxRate = objTaxTyp.Tax_Rate,
                                 TaxDate = objTaxTyp.Tax_Date,
                                 Stateid = objState.State_Idno,
                                 TaxMastId = objTaxTyp.TaxMast_Idno,
                                 PANType = objPANType.PANType_Name,
                                 LorryFrom = objTaxTyp.LorryCnt_From,
                                 LorryTo = objTaxTyp.LorryCnt_To,
                                 CalculateOnDF = objTaxTyp.CalOn_DF
                             }).ToList();
                return lst;
            }
        }
        public IList SelectAllTaxTypeByID(int stateidno, int TaxtypeIdno, int CompID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (stateidno == 0)
                {
                    IList lst = (from objTaxTyp in db.tblTaxMasters
                                 join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                                 //join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno into objState1
                                 //from objState2 in objState1.DefaultIfEmpty()
                                 join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                                 from objPANType in objPANLeftJoin.DefaultIfEmpty()
                                 where objTax.TaxType_Idno == TaxtypeIdno
                                 orderby objTax.TaxType_Name
                                 select new
                                 {
                                     TaxNm = objTax.TaxType_Name,
                                     TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                     StateNm = "",
                                     TaxRate = objTaxTyp.Tax_Rate,
                                     TaxDate = objTaxTyp.Tax_Date,
                                     Stateid = 0,
                                     TaxMastId = objTaxTyp.TaxMast_Idno,
                                     PANType = objPANType.PANType_Name,
                                     LorryFrom = objTaxTyp.LorryCnt_From,
                                     LorryTo = objTaxTyp.LorryCnt_To,
                                     CalculateOnDF = objTaxTyp.CalOn_DF
                                 }).ToList();
                    return lst;
                }
                else
                {
                    IList lst = (from objTaxTyp in db.tblTaxMasters
                                 join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                                 join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno into objState1
                                 from objState2 in objState1.DefaultIfEmpty()
                                 join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                                 from objPANType in objPANLeftJoin.DefaultIfEmpty()
                                 where objTax.TaxType_Idno == TaxtypeIdno && objState2.State_Idno == stateidno
                                 orderby objTax.TaxType_Name
                                 select new
                                 {
                                     TaxNm = objTax.TaxType_Name,
                                     TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                     StateNm = objState2.State_Name,
                                     TaxRate = objTaxTyp.Tax_Rate,
                                     TaxDate = objTaxTyp.Tax_Date,
                                     Stateid = objState2.State_Idno,
                                     TaxMastId = objTaxTyp.TaxMast_Idno,
                                     PANType = objPANType.PANType_Name,
                                     LorryFrom = objTaxTyp.LorryCnt_From,
                                     LorryTo = objTaxTyp.LorryCnt_To,
                                     CalculateOnDF = objTaxTyp.CalOn_DF
                                 }).ToList();
                    return lst;
                }
            }
        }

        /// <summary>
        /// Set status false not deleting the Tax Master
        /// </summary>
        /// <returns>retun 0 or >0 </returns>
        public int Delete(int TaxMastID)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblTaxMaster TaxMast = (from objTaxTyp in db.tblTaxMasters
                                            where objTaxTyp.TaxMast_Idno == TaxMastID
                                            select objTaxTyp).SingleOrDefault();
                    if (TaxMast != null)
                    {
                        db.tblTaxMasters.DeleteObject(TaxMast);
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
        /// <summary>
        ///Select All Record For GrdMain
        /// <summary>
        public IList Select(int CompID, int StateIdno, int TaxTypeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                //from person in people
                //        join pet in pets on person equals pet.Owner into gj
                //        from subpet in gj.DefaultIfEmpty()

                if (StateIdno == 0)
                {
                    var lst = (from objTaxTyp in db.tblTaxMasters
                               join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                               join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                               join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                               from objPANType in objPANLeftJoin.DefaultIfEmpty()
                               orderby objState.State_Name
                               select new
                               {
                                   TaxNm = objTax.TaxType_Name,
                                   TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                   StateNm = objState.State_Name,
                                   TaxRate = objTaxTyp.Tax_Rate,
                                   TaxDate = objTaxTyp.Tax_Date,
                                   Stateid = objState.State_Idno,
                                   TaxMastId = objTaxTyp.TaxMast_Idno,
                                   PANType = objPANType.PANType_Name,
                                   LorryFrom = objTaxTyp.LorryCnt_From,
                                   LorryTo = objTaxTyp.LorryCnt_To,
                                   CalculateOnDF = objTaxTyp.CalOn_DF
                               }).ToList();
                    if (TaxTypeIdno > 0)
                    {
                        lst = lst.Where(l => l.TaxTyp_Idno == TaxTypeIdno).ToList();
                    }
                    return lst;
                }
                else if (StateIdno != 0)
                {
                    var lst = (from objTaxTyp in db.tblTaxMasters
                               join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                               join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                               join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                               from objPANType in objPANLeftJoin.DefaultIfEmpty()
                               where objTaxTyp.State_Idno == StateIdno
                               orderby objState.State_Name
                               select new
                               {
                                   TaxNm = objTax.TaxType_Name,
                                   TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                   StateNm = objState.State_Name,
                                   TaxRate = objTaxTyp.Tax_Rate,
                                   TaxDate = objTaxTyp.Tax_Date,
                                   Stateid = objState.State_Idno,
                                   TaxMastId = objTaxTyp.TaxMast_Idno,
                                   PANType = objPANType.PANType_Name,
                                   LorryFrom = objTaxTyp.LorryCnt_From,
                                   LorryTo = objTaxTyp.LorryCnt_To,
                                   CalculateOnDF = objTaxTyp.CalOn_DF
                               }).ToList();
                    if (TaxTypeIdno > 0)
                    {
                        lst = lst.Where(l => l.TaxTyp_Idno == TaxTypeIdno).ToList();
                    }
                    return lst;

                }
                else if (TaxTypeIdno == 0)
                {
                    IList lst = (from objTaxTyp in db.tblTaxMasters
                                 join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                                 join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                                 join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                                 from objPANType in objPANLeftJoin.DefaultIfEmpty()
                                 orderby objTax.TaxType_Name
                                 select new
                                 {
                                     TaxNm = objTax.TaxType_Name,
                                     TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                     StateNm = objState.State_Name,
                                     TaxRate = objTaxTyp.Tax_Rate,
                                     TaxDate = objTaxTyp.Tax_Date,
                                     Stateid = objState.State_Idno,
                                     TaxMastId = objTaxTyp.TaxMast_Idno,
                                     PANType = objPANType.PANType_Name,
                                     LorryFrom = objTaxTyp.LorryCnt_From,
                                     LorryTo = objTaxTyp.LorryCnt_To,
                                     CalculateOnDF = objTaxTyp.CalOn_DF
                                 }).ToList();
                    return lst;
                }
                else
                {
                    IList lst = (from objTaxTyp in db.tblTaxMasters
                                 join objTax in db.tblTaxTypes on objTaxTyp.TaxTyp_Idno equals objTax.TaxType_Idno
                                 join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                                 join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                                 from objPANType in objPANLeftJoin.DefaultIfEmpty()
                                 where objTaxTyp.TaxTyp_Idno == TaxTypeIdno
                                 orderby objState.State_Name
                                 select new
                                 {
                                     TaxNm = objTax.TaxType_Name,
                                     TaxTyp_Idno = objTaxTyp.TaxTyp_Idno,
                                     StateNm = objState.State_Name,
                                     TaxRate = objTaxTyp.Tax_Rate,
                                     TaxDate = objTaxTyp.Tax_Date,
                                     Stateid = objState.State_Idno,
                                     TaxMastId = objTaxTyp.TaxMast_Idno,
                                     PANType = objPANType.PANType_Name,
                                     LorryFrom = objTaxTyp.LorryCnt_From,
                                     LorryTo = objTaxTyp.LorryCnt_To,
                                     CalculateOnDF = objTaxTyp.CalOn_DF
                                 }).ToList();
                    return lst;

                }
            }
        }

        public Int64 Insert(int taxtypeIdno, int stateIdno, double rate, int compID, DateTime TaxDate, int PANtype, int lorryfrom, int lorryto, bool CalonDf, Int32 empIdno)
        {
            Int64 TaxIdno = 0;
            try
            {
                if (IsExists(0, stateIdno, TaxDate, taxtypeIdno, PANtype, lorryfrom, lorryto) == false)
                {
                    tblTaxMaster TaxMast = new tblTaxMaster();
                    TaxMast.TaxTyp_Idno = taxtypeIdno;
                    TaxMast.State_Idno = stateIdno;
                    TaxMast.Emp_Idno = empIdno;
                    TaxMast.Tax_Rate = rate;
                    TaxMast.Date_Added = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
                    TaxMast.Date_Modified = DateTime.Now.Date;
                    TaxMast.Comp_Idno = compID;
                    TaxMast.Tax_Date = TaxDate;
                    TaxMast.PANTyp_Idno = PANtype;
                    TaxMast.LorryCnt_From = lorryfrom;
                    TaxMast.LorryCnt_To = lorryto;
                    TaxMast.CalOn_DF = CalonDf;
                    TaxMast.Status = 1;
                    TaxMastDAL objTaxMastDLL = new TaxMastDAL();
                    TaxIdno = objTaxMastDLL.Insert(TaxMast);
                    objTaxMastDLL = null;
                    TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
                }
                else
                {
                    TaxIdno = -1;
                }
            }
            catch
            {
            }
            return TaxIdno;
        }

        /// <summary>
        /// Insert Branch Master
        /// </summary>
        /// <param name="objBrnchMst">table of Branch Master</param>
        /// <returns>return 0 or >0</returns>
        public Int64 Insert(tblTaxMaster TaxMast)
        {
            Int64 TaxIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var objtbl = (from obj in db.tblTaxMasters where obj.State_Idno == TaxMast.State_Idno && obj.TaxTyp_Idno == TaxMast.TaxTyp_Idno select obj).ToList();
                    if (objtbl.Count > 0)
                    {
                        for (int i = 0; i < objtbl.Count; i++)
                        {
                            objtbl[i].Status = 0;
                            db.SaveChanges();

                        }
                        //objtbl[0].Status = 0;
                    }

                    db.AddTotblTaxMasters(TaxMast);
                    db.SaveChanges();
                    TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return TaxIdno;
        }

        /// <summary>
        /// Update Branch Master
        /// </summary>
        /// <param name="objBrnchMst">table of Branch Master</param>
        /// <returns>return o or >0</returns>
        public Int64 Update(tblTaxMaster TaxMast, Int32 id)
        {
            Int64 TaxIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                { 
                    tblTaxMaster objTaxMast = (from objTaxTyp in db.tblTaxMasters
                                               where objTaxTyp.TaxTyp_Idno == TaxMast.TaxTyp_Idno && objTaxTyp.State_Idno == TaxMast.State_Idno
                                               && objTaxTyp.Tax_Date == TaxMast.Tax_Date && objTaxTyp.PANTyp_Idno == TaxMast.PANTyp_Idno
                                               && objTaxTyp.TaxMast_Idno == id
                                               select objTaxTyp).SingleOrDefault();
                    if (objTaxMast != null)
                    {
                        objTaxMast.TaxTyp_Idno = TaxMast.TaxTyp_Idno;
                        objTaxMast.State_Idno = TaxMast.State_Idno;
                        objTaxMast.Tax_Rate = TaxMast.Tax_Rate;
                        objTaxMast.Date_Added = TaxMast.Date_Added;
                        objTaxMast.Date_Modified = TaxMast.Date_Modified;
                        objTaxMast.Comp_Idno = TaxMast.Comp_Idno;
                        objTaxMast.Tax_Date = TaxMast.Tax_Date;
                        objTaxMast.Emp_Idno = TaxMast.Emp_Idno;
                        objTaxMast.PANTyp_Idno = TaxMast.PANTyp_Idno;
                        objTaxMast.LorryCnt_From = TaxMast.LorryCnt_From;
                        objTaxMast.LorryCnt_To = TaxMast.LorryCnt_To;
                        objTaxMast.CalOn_DF = TaxMast.CalOn_DF;
                        db.SaveChanges();
                        TaxIdno = Convert.ToInt32(objTaxMast.TaxTyp_Idno);
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return TaxIdno;
        }

        /// <summary>
        /// Update Tax Master
        /// </summary>
        /// <param name="CityMast">table of Tax Master</param>
        /// <returns>return o or >0</returns>
        public Int64 Update(int id, int taxtypeIdno, int stateIdno, double rate, int compID, DateTime TaxDate, int PANtype, int lorryfrom, int lorryto, bool CalonDf, Int32 empIdno)
        {
            Int64 TaxIdno = 0;
            try
            {
                if (IsExists(id, stateIdno, TaxDate, taxtypeIdno, PANtype, lorryfrom, lorryto) == false)
                {
                    tblTaxMaster TaxMast = new tblTaxMaster();
                    TaxMast.TaxTyp_Idno = taxtypeIdno;
                    TaxMast.State_Idno = stateIdno;
                    TaxMast.Tax_Rate = rate;
                    TaxMast.Emp_Idno = empIdno;
                    TaxMast.Date_Added = DateTime.Now.Date;
                    TaxMast.Date_Modified = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
                    TaxMast.Comp_Idno = compID;
                    TaxMast.Tax_Date = TaxDate;
                    TaxMast.PANTyp_Idno = PANtype;
                    TaxMast.LorryCnt_From = lorryfrom;
                    TaxMast.LorryCnt_To = lorryto;
                    TaxMast.CalOn_DF = CalonDf;
                    TaxMastDAL objTaxMastDLL = new TaxMastDAL();
                    TaxIdno = objTaxMastDLL.Update(TaxMast, id);
                    TaxIdno = Convert.ToInt32(TaxMast.TaxTyp_Idno);
                    objTaxMastDLL = null;
                }
                else
                {
                    TaxIdno = -1;
                }
            }
            catch
            {
            }
            return TaxIdno;
        }

        /// <summary>
        /// Record exists or not if TaxTyp_Idno>0 then check for update else check for insert
        /// </summary>
        /// <param name="brnchName">Name of Tax Master</param>
        /// <param name="CompIdno">Id of company</param>
        /// <param name="brnchIdno">Id of Tax Master</param>
        /// <returns>return single record</returns>
        public bool IsExists(int Id, int stateIdno, DateTime TaxDate, int taxtypeIdno, int PANtype, int lorryfrom, int lorryto)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblTaxMaster TaxMast = new tblTaxMaster();
                if (Id <= 0)
                {
                    TaxMast = (from objTaxTyp in db.tblTaxMasters
                               where objTaxTyp.TaxTyp_Idno == taxtypeIdno
                                       && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
                                       && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_From == lorryfrom && objTaxTyp.LorryCnt_To == lorryto
                               select objTaxTyp).SingleOrDefault();

                    var lst = (from objTaxTyp in db.tblTaxMasters
                               where objTaxTyp.TaxTyp_Idno == taxtypeIdno
                                       && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
                                       && objTaxTyp.Tax_Date == TaxDate
                               select objTaxTyp).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var check in lst)
                        {
                            if (lorryfrom <= check.LorryCnt_From || lorryto <= check.LorryCnt_To || lorryfrom <= check.LorryCnt_To)
                            {
                                return true;
                            }
                        }
                    }

                }
                else if (Id > 0)
                {
                    TaxMast = (from objTaxTyp in db.tblTaxMasters
                               where objTaxTyp.TaxTyp_Idno == taxtypeIdno
                                       && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
                                       && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_From == lorryfrom && objTaxTyp.LorryCnt_To == lorryto
                                       && objTaxTyp.TaxMast_Idno != Id
                               select objTaxTyp).SingleOrDefault();
                    var lst = (from objTaxTyp in db.tblTaxMasters
                               where objTaxTyp.TaxTyp_Idno == taxtypeIdno
                                       && objTaxTyp.PANTyp_Idno == PANtype && objTaxTyp.State_Idno == stateIdno
                                       && objTaxTyp.Tax_Date == TaxDate && objTaxTyp.LorryCnt_To <= lorryto
                                       && objTaxTyp.TaxMast_Idno != Id
                               select objTaxTyp).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var check in lst)
                        {
                            if (lorryfrom <= check.LorryCnt_From || lorryto <= check.LorryCnt_To || lorryfrom <= check.LorryCnt_To)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

    }

}
