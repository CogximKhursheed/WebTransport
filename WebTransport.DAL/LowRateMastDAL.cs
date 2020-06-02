using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using VisageWeb.Utility;
using System.Transactions;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
//using VisageWeb.Utility;

namespace WebTransport.DAL
{
    public class LowRateMastDAL
    {

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

        public IList SelectPartyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from LM in db.LorryMasts
                        join AM in db.AcntMasts on LM.Prty_Idno equals AM.Acnt_Idno
                        select new
                        {
                            AM.Acnt_Idno,
                            AM.Acnt_Name
                        }).OrderBy(a=>a.Acnt_Idno).GroupBy(x=>x.Acnt_Idno).Select(x=>x.FirstOrDefault()).ToList();
            }
        }
        public List<LorryMast> SelectPANNo(Int64 PartyIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from objPAN in db.LorryMasts where objPAN.Prty_Idno == PartyIdno orderby objPAN.Pan_No select objPAN).Distinct().ToList();
            }
        }
        public DataSet SelectPANNoList(Int64 PartyIdno,string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                DataSet List = SqlHelper.ExecuteDataset(con, CommandType.Text, "select distinct Pan_No from LorryMast where Prty_Idno="+PartyIdno);
                return List;
            }
        }
        /// <summary>
        /// Select All From Tax Master By TaxId and StateIdno
        /// </summary>
        /// <returns>retun All </returns>
        public tblLowRateMaster SelectTaxByID(int RateMastID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from objRateTyp in db.tblLowRateMasters
                        where objRateTyp.LowRateMast_Idno == RateMastID
                        select objRateTyp).SingleOrDefault();
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
                                 CalculateOnDF = objTaxTyp.CalOn_DF,
                                 LowRate=objTaxTyp.LowRateWise
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
                                 join objState in db.tblStateMasters on objTaxTyp.State_Idno equals objState.State_Idno
                                 join objPAN in db.tblPANTypes on objTaxTyp.PANTyp_Idno equals objPAN.PANType_Idno into objPANLeftJoin
                                 from objPANType in objPANLeftJoin.DefaultIfEmpty()
                                 where objTax.TaxType_Idno == TaxtypeIdno
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
                                     CalculateOnDF = objTaxTyp.CalOn_DF,
                                     LowRate = objTaxTyp.LowRateWise
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
                                 where objTax.TaxType_Idno == TaxtypeIdno && objState.State_Idno == stateidno
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
                                     CalculateOnDF = objTaxTyp.CalOn_DF,
                                     LowRate = objTaxTyp.LowRateWise
                                 }).ToList();
                    return lst;
                }
            }
        }

        /// <summary>
        /// Set status false not deleting the Tax Master
        /// </summary>
        /// <returns>retun 0 or >0 </returns>
        public int Delete(int RateMastID)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblLowRateMaster RateMast = (from objRateTyp in db.tblLowRateMasters
                                            where objRateTyp.LowRateMast_Idno == RateMastID
                                            select objRateTyp).SingleOrDefault();
                    if (RateMast != null)
                    {
                        db.tblLowRateMasters.DeleteObject(RateMast);
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
        public IList Select(int AcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                    var lst = (from objRate in db.tblLowRateMasters
                               join objAM in db.AcntMasts on objRate.Acnt_Idno equals objAM.Acnt_Idno 
                               select new
                               {
                                   LowRateID=objRate.LowRateMast_Idno,
                                   PartyName = objAM.Acnt_Name,
                                   PartyIdno = objRate.Acnt_Idno,
                                   Date = objRate.Date,
                                   PAN = objRate.PAN,
                                   Rate = objRate.Tax_Rate
                               }).ToList();
                    if (AcntIdno > 0)
                    {
                        lst = lst.Where(l => l.PartyIdno == AcntIdno).ToList();
                    }
                return lst;
            }
        }

        public Int64 Insert(Int64 AcntIdno, string PanNo, double rate, int compID, DateTime TaxDate, Int32 empIdno)
        {
            Int64 RateIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (IsExists(TaxDate, PanNo) == false)
                    {
                        tblLowRateMaster TaxMast = new tblLowRateMaster();
                        TaxMast.Acnt_Idno = AcntIdno;
                        TaxMast.PAN = PanNo;
                        TaxMast.Emp_Idno = empIdno;
                        TaxMast.Tax_Rate = rate;
                        TaxMast.Date_Added = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
                        TaxMast.Date_Modified = DateTime.Now.Date;
                        TaxMast.Comp_Idno = compID;
                        TaxMast.Date = TaxDate;
                        TaxMast.Status = 1;
                        db.AddTotblLowRateMasters(TaxMast);
                        db.SaveChanges();
                        RateIdno = Convert.ToInt32(TaxMast.LowRateMast_Idno);
                    }
                    else
                    {
                        RateIdno = -1;
                    }
                }
            }
            catch
            {
            }
            return RateIdno;
        }

        /// <summary>
        /// Update Tax Master
        /// </summary>
        /// <param name="CityMast">table of Tax Master</param>
        /// <returns>return o or >0</returns>
        public Int64 Update(int id,Int64 AcntIdno, string PanNo, double rate, int compID, DateTime TaxDate, Int32 empIdno)
        {
            Int64 TaxIdno = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (IsExists(TaxDate, PanNo) == true)
                    {
                        tblLowRateMaster RateMast = (from objRate in db.tblLowRateMasters
                                                      where objRate.PAN == PanNo
                                                      && objRate.Date == TaxDate
                                                      && objRate.LowRateMast_Idno == id
                                                      select objRate).SingleOrDefault();
                        if (RateMast != null)
                        {
                            RateMast.Acnt_Idno = AcntIdno;
                            RateMast.PAN = PanNo;
                            RateMast.Tax_Rate = rate;
                            RateMast.Emp_Idno = empIdno;
                            RateMast.Date_Modified = DateTime.Now.Date;// ApplicationFunction.GetIndianDateTime().Date;
                            RateMast.Comp_Idno = compID;
                            RateMast.Date = TaxDate;
                            db.SaveChanges();
                            TaxIdno = Convert.ToInt32(RateMast.LowRateMast_Idno);
                        }
                    }
                    else
                    {
                        TaxIdno = -1;
                    }
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
        public bool IsExists(DateTime TaxDate, string PAN)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblLowRateMaster RateMast = new tblLowRateMaster();

                RateMast = (from objRate in db.tblLowRateMasters
                           where objRate.PAN == PAN
                           && objRate.Date == TaxDate
                           select objRate).SingleOrDefault();

                var lst = (from objRate1 in db.tblLowRateMasters
                               where objRate1.PAN == PAN
                                && objRate1.Date == TaxDate
                           select objRate1).ToList();
                if (lst.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }


    }

}
