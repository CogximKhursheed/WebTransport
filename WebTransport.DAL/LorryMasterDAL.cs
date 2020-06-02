using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class LorryMasterDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from LorryMast
        /// </summary>
        /// <returns></returns>
        /// 
        public List<DriverMast> selectHireDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<DriverMast> lst = null;
                lst = (from DR in db.DriverMasts orderby DR.Driver_Name ascending select DR).ToList();
                return lst;
            }
        }
        public List<AcntMast> selectOwnerDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from DR in db.AcntMasts where DR.Acnt_Type == 9 orderby DR.Acnt_Name ascending select DR).ToList();
                return lst;
            }
        }
        public IList selectPrtyLorryDetails(Int32 PrtyIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.LorryMasts
                           where obj.Prty_Idno == PrtyIdno
                           select
                               new
                               {
                                   obj.Lorry_No,
                                   LorryType = (obj.Lorry_Type == 0 ? "Own" : "Hire"),
                                   OwnerName = obj.Owner_Name,
                                   LorryMake = obj.Lorry_Make,
                                   PanNo = obj.Pan_No,
                                   ChasisNo = obj.Chassis_no,
                                   EngineNo = obj.Eng_No,
                                   obj.Lorry_Idno
                               }).ToList();
                return lst;
            }
        }

        public IList<LorryType> SelectLorryType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                IList<LorryType> Obj = null;
                Obj = db.LorryTypes.Select(R => R).ToList();
                return Obj;
            }
        }
        public List<AcntMast> SelectPartyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.Acnt_Type == 2 || LM.Acnt_Type==6 orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<AcntMast> SelectInsuranceComp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.Acnt_Type == 11 orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<AcntMast> SelectPartyNameOwn()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.Acnt_Type == 6 orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public IList FillOwnerDetl(Int64 AcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AM in db.AcntMasts
                           join LM in db.LorryMasts on AM.Acnt_Idno equals LM.Prty_Idno
                           where AM.Acnt_Idno == AcntIdno
                           orderby AM.Acnt_Name ascending
                           select new
                           {
                               OwnerName = LM.Owner_Name,
                               OwnerAddress = LM.Owner_Address,
                               Mobile = LM.Owner_Mobile,
                               PanNo = AM.Pan_No,
                           }).ToList();
                return lst;
            }
        }

        public List<tblPositionMast> SelectTyrePostion()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblPositionMast> lst = null;
                lst = (from LM in db.tblPositionMasts orderby LM.Position_name ascending select LM).ToList();
                return lst;
            }
        }

        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.LorryMasts
                        orderby mast.Owner_Name
                        select mast).ToList();
            }
        }

        public IList SelectDocHolder(Int32 Lorry_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblDocHolders
                           where obj.Lorry_Idno == Lorry_Idno
                           select
                               new
                               {
                                   DocIdno = obj.Doc_Idno,
                                   DocName = obj.Doc_Name,
                                   DocRemark = obj.Doc_Remark,
                                   DocImage = obj.Doc_Image,
                               }).ToList();
                return lst;
            }

        }

        public IList SelectLorryBasisonType(Int64 Lorry_Type)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.LorryMasts where mast.Lorry_Type == Lorry_Type orderby mast.Lorry_No select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <param name="strOwnerName"></param>
        /// <returns></returns>
        public IList SelectForSearch(string strOwnerName, Int64 PartyName, string strLorryNo, string strPanNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.LorryMasts
                           join obj in db.AcntMasts on mast.Prty_Idno equals obj.Acnt_Idno
                           join obj1 in db.DriverMasts on mast.Driver_Idno equals obj1.Driver_Idno into Cidl
                           from mappingsCidl in Cidl.DefaultIfEmpty()
                           join obj2 in
                               (from DR in db.AcntMasts where DR.Acnt_Type == 9 select DR) on mast.Driver_Idno equals obj2.Acnt_Idno into Cidd
                           from mappingsCidd in Cidd.DefaultIfEmpty()
                           orderby obj.Acnt_Name
                           select new
                           {
                               prty_Name = obj.Acnt_Name,
                               LorryIdno = mast.Lorry_Idno,
                               OwnerName = mast.Owner_Name,
                               LorryMake = mast.Lorry_Make,
                               PanNo = mast.Pan_No,
                               ChasisNo = mast.Chassis_no,
                               EngineNo = mast.Eng_No,
                               LorryNo = mast.Lorry_No,
                               DriverName = (mast.Lorry_Type == 0) ? (string.IsNullOrEmpty(mappingsCidd.Acnt_Name) ? "N/A" : mappingsCidd.Acnt_Name) : (string.IsNullOrEmpty(mappingsCidl.Driver_Name) ? "N/A" : mappingsCidl.Driver_Name),
                               LorryType = (mast.Lorry_Type == 0 ? "Own" : "Hire"),
                               Status = mast.Status,
                               Party_Idno = mast.Prty_Idno,
                               LowRate = mast.LowRateWise,
                               MobileNo=mast.Owner_Mobile,
                           }).ToList();
                if (PartyName != 0)
                {
                    lst = (from I in lst where I.Party_Idno == PartyName select I).ToList();
                }
                if (strOwnerName != "")
                {
                    lst = (from I in lst where I.OwnerName.ToLower().Contains(strOwnerName.ToLower()) select I).ToList();
                }
                if (strLorryNo != "")
                {
                    lst = (from I in lst where I.LorryNo.ToLower().Contains(strLorryNo.ToLower()) select I).ToList();
                }
                if (strPanNo != "")
                {
                    lst = (from I in lst where I.PanNo.ToLower().Contains(strPanNo.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select one record from LorryMast by LorryIdno
        /// </summary>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>
        public LorryMast SelectById(int intLorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.LorryMasts
                        where mast.Lorry_Idno == intLorryIdno
                        select mast).FirstOrDefault();
            }
        }



        /// <summary>
        /// To check record existence in LorryMast
        /// </summary>
        /// <param name="strOwnerrName"></param>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strOwnerrName, Int64 intLorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                LorryMast objLorryMast = new LorryMast();
                if (intLorryIdno <= 0)
                {
                    objLorryMast = (from mast in db.LorryMasts
                                    where mast.Lorry_No == strOwnerrName && mast.Status == true
                                    select mast).FirstOrDefault();
                }
                else if (intLorryIdno > 0)
                {
                    objLorryMast = (from mast in db.LorryMasts
                                    where mast.Lorry_No == strOwnerrName
                                          && mast.Lorry_Idno != intLorryIdno
                                    select mast).FirstOrDefault();
                }
                if (objLorryMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsDocExists(string strDocName, Int64 intDocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblDocHolder objDocHolder = new tblDocHolder();
                if (intDocIdno <= 0)
                {

                }
                else if (intDocIdno > 0)
                {
                    objDocHolder = (from mast in db.tblDocHolders
                                    where mast.Doc_Name == strDocName
                                          && mast.Doc_Idno != intDocIdno
                                    select mast).FirstOrDefault();
                }
                if (objDocHolder != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool CheckChasisForStck(string Serial, int LorryIdno, string SaveUpdate)
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
                    objStckDetl = db.Stckdetls.Where(stckd => stckd.SerialNo == Serial && stckd.lorry_Idno != LorryIdno).FirstOrDefault();
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

        public Int64 InsertPurBillStock(Int64 Lorry_Idno, string SerialNO, Int64 PositionIdno, string Company, Int32 Type, string purcFrom, string Kms)
        {
            Int64 SerialIdno = 0;

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Stckdetl objSerial = new Stckdetl();
                objSerial.SerialNo = SerialNO;
                objSerial.lorry_Idno = Lorry_Idno;
                objSerial.Tyre_PostionIdno = PositionIdno;
                objSerial.CompName = Company;
                objSerial.Type = Type;
                objSerial.Item_from = "LM";
                objSerial.PurFrom = purcFrom;
                objSerial.FittingKMs = Kms;
                db.Stckdetls.AddObject(objSerial);
                db.SaveChanges();
                SerialIdno = objSerial.SerlDetl_id;
                return SerialIdno;
            }
        }

        public void DeleteAll(Int64 Lorry_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<Stckdetl> objSerial = db.Stckdetls.Where(stckd => stckd.lorry_Idno == Lorry_Idno).ToList();
                if (objSerial != null)
                {
                    foreach (var H in objSerial)
                    {
                        db.Stckdetls.DeleteObject(H);
                        db.SaveChanges();
                    }
                }
            }
        }

        public Int64 UpdatePurBillStock(Int64 Lorry_Idno, string SerialNO, Int64 PositionIdno, string Company, Int32 Type, string purcFrom, string Kms)
        {
            Int64 SerialIdno = 0;

            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<Stckdetl> objSerial = db.Stckdetls.Where(stckd => stckd.lorry_Idno == Lorry_Idno).ToList();
                if (objSerial != null)
                {
                    foreach (var H in objSerial)
                    {
                        db.Stckdetls.DeleteObject(H);
                        db.SaveChanges();
                    }
                }

                Stckdetl objSerial1 = new Stckdetl();
                objSerial1.SerialNo = SerialNO;
                objSerial1.lorry_Idno = Lorry_Idno;
                objSerial1.Tyre_PostionIdno = PositionIdno;
                objSerial1.CompName = Company;
                objSerial1.Type = Type;
                objSerial1.PurFrom = purcFrom;
                objSerial1.FittingKMs = Kms;
                db.Stckdetls.AddObject(objSerial1);
                db.SaveChanges();
                SerialIdno = objSerial1.SerlDetl_id;
                return SerialIdno;
            }
            return SerialIdno;
        }

        public IList SelectStockforPurBill(Int64 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from sd in db.Stckdetls
                           where sd.lorry_Idno == LorryIdno
                           select new
                           {
                               LorryTyre_id = sd.SerlDetl_id,
                               Lorry_Idno = sd.lorry_Idno,
                               Serial_No = sd.SerialNo,
                               Position = sd.Tyre_PostionIdno,
                               Company = sd.CompName,
                               Type = sd.Type,
                               PurFrom = sd.PurFrom,
                               Kms = sd.FittingKMs
                           }

                             ).ToList();
                return lst;
            }
        }
        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in LorryMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="strLorryMake"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 InsertLorryMast(string strLorryMake, string strLorryMakeYar, string strOwnerName, Int32 LorryType, Int64 PartyName, string strChasisNo, string strNumberOfTyres, string strEngineNo, string strLorryNo, string strPanNo, bool bStatus, bool bcommsn, Int64 intType, int pantypeIdno, DateTime? FitnessDate, DateTime? RCDate, DateTime? NatPermitDate, DateTime? AuthPermitDate, string TrollyLength, string TrollyHeight, string TrollyWeight, string TrollyTyerNo, string InsNo, string InsCompName, DateTime? InsValidDate, string FinName, string FinAmount, string FinEMIAmount, string FinTotalInstall, DateTime? FinPeriodFrom, DateTime? FinPeriodTo, bool CalOnDF, Int64 DriverIdno, Double InsurAmount, int FITwarn, int INSwarn, int RCwarn, int NatWarn, int AuthWarn, Int32 empIdno, bool LowRate, string strOwneradd, string strOwnerMobile)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    LorryMast objLorryMast = new LorryMast();

                    objLorryMast.Lorry_Make = strLorryMake;
                    objLorryMast.Lorry_Year = strLorryMakeYar;
                    objLorryMast.Owner_Name = strOwnerName;
                    objLorryMast.Owner_Mobile = strOwnerMobile;
                    objLorryMast.Owner_Address = strOwneradd;
                    objLorryMast.Lorry_Type = LorryType;
                    objLorryMast.Status = bStatus;
                    objLorryMast.chk_ComsnCal = bcommsn;
                    objLorryMast.Comp_Idno = intCompIdno;
                    objLorryMast.Type = intType;
                    objLorryMast.Emp_Idno = empIdno;

                    objLorryMast.Prty_Idno = PartyName;
                    objLorryMast.Chassis_no = strChasisNo;
                    objLorryMast.Tyres_No = strNumberOfTyres;
                    objLorryMast.Eng_No = strEngineNo;
                    objLorryMast.Lorry_No = strLorryNo;
                    objLorryMast.Pan_No = strPanNo;
                    objLorryMast.PANTyp_Idno = pantypeIdno;
                    objLorryMast.Fitness_Date = FitnessDate;
                    objLorryMast.RC_Date = RCDate;
                    objLorryMast.Nat_Permit_Date = NatPermitDate;
                    objLorryMast.Auth_Permit_Date = AuthPermitDate;
                    objLorryMast.Trolly_Length = TrollyLength;
                    objLorryMast.Trolly_Height = TrollyHeight;
                    objLorryMast.Trolly_Weight = TrollyWeight;
                    objLorryMast.Trolly_TyerNo = TrollyTyerNo;
                    objLorryMast.Ins_No = InsNo;
                    objLorryMast.Ins_Comp_Name = InsCompName;
                    objLorryMast.Ins_Valid_Date = InsValidDate;
                    objLorryMast.Fin_Name = FinName;
                    objLorryMast.Fin_Amount = FinAmount;
                    objLorryMast.Fin_EMI_Amount = FinEMIAmount;
                    objLorryMast.Fin_Total_Install = FinTotalInstall;
                    objLorryMast.Fin_Period_From = FinPeriodFrom;
                    objLorryMast.Fin_Period_To = FinPeriodTo;
                    objLorryMast.Driver_Idno = DriverIdno;
                    objLorryMast.FITwarn = FITwarn;
                    objLorryMast.INSwarn = INSwarn;
                    objLorryMast.RCwarn = RCwarn;
                    objLorryMast.NatWarn = NatWarn;
                    objLorryMast.AuthWarn = AuthWarn;
                    objLorryMast.Ins_Amount = InsurAmount;

                    objLorryMast.CalOn_DF = CalOnDF;
                    objLorryMast.LowRateWise = LowRate;
                    objLorryMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strLorryNo, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.LorryMasts.AddObject(objLorryMast);
                        db.SaveChanges();
                        intValue = objLorryMast.Lorry_Idno;
                    }

                    if (intValue > 0)
                    {
                        tblLorryPrtyDetl lPrty = db.tblLorryPrtyDetls.Where(X => X.Lorry_Idno == intValue && X.Acnt_Idno == PartyName).FirstOrDefault();
                        if (lPrty == null)
                        {
                            //string strSQL = @"select Convert(nvarchar(11),convert(datetime,StartDate,105),105) AS SDate  from tblFinYear  order by Fin_Idno desc";
                            //DataSet ds = SqlHelper.ExecuteDataset(MultipleDBDAL.strDynamicConString(), CommandType.Text, strSQL);
                            //string strNewDate = ds.Tables[0].Rows[0]["SDate"].ToString();
                            var strNewDate = db.tblFinYears.Select(o => o.StartDate).Max();
                            tblLorryPrtyDetl objLorryDetl = new tblLorryPrtyDetl();
                            objLorryDetl.Lorry_Idno = intValue;
                            objLorryDetl.Acnt_Idno = PartyName;

                            objLorryDetl.Valid_From = Convert.ToDateTime(strNewDate);
                            //objLorryDetl.Valid_From =Convert.ToDateTime(new DateTime(DateTime.Now.Year, 04, 01).ToString("dd-MM-yyyy"));
                            //objLorryDetl.Valid_From = Convert.ToDateTime("01-04" + DateTime.Now.Year.ToString("N4"));
                            db.tblLorryPrtyDetls.AddObject(objLorryDetl);
                            db.SaveChanges();
                        }
                    }

                    IList<LorryMast> obj = db.LorryMasts.Where(lm => lm.Pan_No == objLorryMast.Pan_No).ToList();
                    if (obj.Count > 0)
                    {
                        foreach (LorryMast rw in obj)
                        {
                            rw.CalOn_DF = objLorryMast.CalOn_DF;
                            rw.LowRateWise = objLorryMast.LowRateWise;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public Int64 InsertDocHolderDetails(DataTable dt, Int64 intLorryIdno, Int32 empIdno)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tblDocHolder objDocHolderDelete = (from mast in db.tblDocHolders
                                                           where mast.Lorry_Idno == intLorryIdno
                                                           select mast).FirstOrDefault();
                        if (objDocHolderDelete != null)
                        {
                            db.tblDocHolders.DeleteObject(objDocHolderDelete);
                            db.SaveChanges();
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tblDocHolder objDocHolderInsert = new tblDocHolder();
                            objDocHolderInsert.Doc_Name = dt.Rows[i]["DocName"].ToString();
                            objDocHolderInsert.Doc_Remark = dt.Rows[i]["Remark"].ToString();
                            objDocHolderInsert.Doc_Image = dt.Rows[i]["Image"].ToString();
                            objDocHolderInsert.Emp_Idno = empIdno;
                            objDocHolderInsert.Lorry_Idno = intLorryIdno;
                            objDocHolderInsert.Date_Added = System.DateTime.Now;
                            db.tblDocHolders.AddObject(objDocHolderInsert);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public bool IsPartyExists(Int64 lorryIdno, Int64 AcntIdno, DateTime date)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblLorryPrtyDetl objLorryMast = new tblLorryPrtyDetl();
                objLorryMast = (from mast in db.tblLorryPrtyDetls
                                where mast.Acnt_Idno == AcntIdno
                                 && mast.Lorry_Idno == lorryIdno && mast.Valid_From == date
                                select mast).FirstOrDefault();
                if (objLorryMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool CheckOnDf(Int64 AcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                bool abc;
                abc = Convert.ToBoolean((from mast in db.LorryMasts
                                where mast.Prty_Idno == AcntIdno
                                select mast.CalOn_DF).FirstOrDefault());
                if (abc == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public Int64 InsertPartyChangeDetails(DataTable dt)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tblLorryPrtyDetl objNewPartyInsert = new tblLorryPrtyDetl();
                            if (IsPartyExists(Convert.ToInt64(dt.Rows[i]["LorryIdNo"]), Convert.ToInt64(dt.Rows[i]["AcntIdno"]), Convert.ToDateTime(dt.Rows[i]["ValidFrom"])) == true)
                            {
                                intValue = -1;
                            }
                            else
                            {
                                objNewPartyInsert.Lorry_Idno = Convert.ToInt64(dt.Rows[i]["LorryIdNo"]);
                                objNewPartyInsert.Acnt_Idno = Convert.ToInt64(dt.Rows[i]["AcntIdno"]);
                                objNewPartyInsert.Valid_From = Convert.ToDateTime(dt.Rows[i]["ValidFrom"]);
                                db.tblLorryPrtyDetls.AddObject(objNewPartyInsert);
                                db.SaveChanges();

                                LorryMast objLorryMast = new LorryMast();
                                Int64 LorryIdno = Convert.ToInt64(dt.Rows[i]["LorryIdNo"]);
                                objLorryMast = (from mast in db.LorryMasts
                                                where mast.Lorry_Idno == LorryIdno
                                                select mast).FirstOrDefault();
                                if (objLorryMast != null)
                                {
                                    objLorryMast.Prty_Idno = Convert.ToInt64(dt.Rows[i]["AcntIdno"]);
                                    db.SaveChanges();
                                }
                            }

                        }
                        intValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public IList BindPartyGrid(Int64 lorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int counter = 0;
                var lst = (from LP in db.tblLorryPrtyDetls
                           join LM in db.LorryMasts on LP.Lorry_Idno equals LM.Lorry_Idno
                           join AM in db.AcntMasts on LP.Acnt_Idno equals AM.Acnt_Idno
                           where LP.Lorry_Idno == lorryIdno
                           select new
                           {
                               ID = counter + 1,
                               LorryNo = LM.Lorry_No,
                               LorryIdno = LP.Lorry_Idno,
                               Lorryparty=LP.LorryPrtyDetl_Idno,
                               AcntName = AM.Acnt_Name,
                               AcntIdno = LP.Acnt_Idno,
                               ValidFrom = LP.Valid_From
                           }
                           ).ToList();
                return lst;
            }

        }

        public IList BindPartyGridid(Int64 LorrryPartidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                int counter = 0;
                var lst = (from LP in db.tblLorryPrtyDetls
                           join LM in db.LorryMasts on LP.Lorry_Idno equals LM.Lorry_Idno
                           join AM in db.AcntMasts on LP.Acnt_Idno equals AM.Acnt_Idno
                           where LP.Lorry_Idno == LorrryPartidno
                           select new
                           {
                               ID = counter + 1,
                               LorryNo = LM.Lorry_No,
                               LorryIdno = LP.Lorry_Idno,
                               Lorryparty = LP.LorryPrtyDetl_Idno,
                               AcntName = AM.Acnt_Name,
                               AcntIdno = LP.Acnt_Idno,
                               ValidFrom = LP.Valid_From
                           }
                           ).ToList();
                return lst;
            }

        }

        public IList FilldateFromTo(Int32 yearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var obj = (from FY in db.tblFinYears where FY.Fin_Idno == yearIdno orderby FY.Fin_Idno select FY).ToList();
                return obj;
            }
        }
        /// <summary>
        /// Update records in LorryMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="strNarrDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public Int64 UpdateLorryMast(string strLorryMake, string strLorryMakeYar, string strOwnerName, Int32 LorryType, Int64 PartyName, string strChasisNo, string strNumberOfTyres, string strEngineNo, string strLorryNo, string strPanNo, bool bStatus, bool bcommsn, Int64 intType, Int64 intLorryIdno, int pantypeIdno, DateTime? FitnessDate, DateTime? RCDate, DateTime? NatPermitDate, DateTime? AuthPermitDate, string TrollyLength, string TrollyHeight, string TrollyWeight, string TrollyTyerNo, string InsNo, string InsCompName, DateTime? InsValidDate, string FinName, string FinAmount, string FinEMIAmount, string FinTotalInstall, DateTime? FinPeriodFrom, DateTime? FinPeriodTo, bool CalOnDF, Int64 DriverIdno, Double InsurAmount, int FITwarn, int INSwarn, int RCwarn, int NatWarn, int AuthWarn, Int32 empIdno, bool LowRate, string strOwnerAdd, string strOwnerMobile)
        {
            Int64 intValue = 0;
            //Int32 intLorryIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    LorryMast objLorryMast = (from mast in db.LorryMasts
                                              where mast.Lorry_Idno == intLorryIdno
                                              select mast).FirstOrDefault();
                    if (objLorryMast != null)
                    {
                        objLorryMast.Lorry_Make = strLorryMake;
                        objLorryMast.Lorry_Year = strLorryMakeYar;
                        objLorryMast.Owner_Name = strOwnerName;
                        objLorryMast.Owner_Address = strOwnerAdd;
                        objLorryMast.Owner_Mobile = strOwnerMobile;
                        objLorryMast.Lorry_Type = LorryType;
                        objLorryMast.Status = bStatus;
                        objLorryMast.chk_ComsnCal = bcommsn;
                        objLorryMast.Type = intType;
                        objLorryMast.Ins_Amount = InsurAmount;
                        objLorryMast.Emp_Idno = empIdno;

                        objLorryMast.Prty_Idno = PartyName;
                        objLorryMast.Chassis_no = strChasisNo;
                        objLorryMast.Tyres_No = strNumberOfTyres;
                        objLorryMast.Eng_No = strEngineNo;
                        objLorryMast.Lorry_No = strLorryNo;
                        objLorryMast.Pan_No = strPanNo;
                        objLorryMast.PANTyp_Idno = pantypeIdno;

                        objLorryMast.Fitness_Date = FitnessDate;
                        objLorryMast.RC_Date = RCDate;
                        objLorryMast.Nat_Permit_Date = NatPermitDate;
                        objLorryMast.Auth_Permit_Date = AuthPermitDate;
                        objLorryMast.Trolly_Length = TrollyLength;
                        objLorryMast.Trolly_Height = TrollyHeight;
                        objLorryMast.Trolly_Weight = TrollyWeight;
                        objLorryMast.Trolly_TyerNo = TrollyTyerNo;

                        objLorryMast.Ins_No = InsNo;
                        objLorryMast.Ins_Comp_Name = InsCompName;
                        objLorryMast.Ins_Valid_Date = InsValidDate;
                        objLorryMast.Fin_Name = FinName;
                        objLorryMast.Fin_Amount = FinAmount;
                        objLorryMast.Fin_EMI_Amount = FinEMIAmount;
                        objLorryMast.Fin_Total_Install = FinTotalInstall;
                        objLorryMast.Fin_Period_From = FinPeriodFrom;
                        objLorryMast.Fin_Period_To = FinPeriodTo;
                        objLorryMast.Driver_Idno = DriverIdno;

                        objLorryMast.FITwarn = FITwarn;
                        objLorryMast.INSwarn = INSwarn;
                        objLorryMast.RCwarn = RCwarn;
                        objLorryMast.NatWarn = NatWarn;
                        objLorryMast.AuthWarn = AuthWarn;

                        objLorryMast.CalOn_DF = CalOnDF;
                        objLorryMast.LowRateWise = LowRate;
                        objLorryMast.Date_Added = System.DateTime.Now;
                        if (IsExists(strLorryNo, intLorryIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intLorryIdno;
                        }
                        if (intValue > 0)
                        {
                            tblLorryPrtyDetl lPrty = db.tblLorryPrtyDetls.Where(X => X.Lorry_Idno == intValue && X.Acnt_Idno == PartyName).FirstOrDefault();
                            if (lPrty != null)
                            {
                                //string strSQL = @"select Convert(nvarchar(11),convert(datetime,StartDate,105),105) AS SDate  from tblFinYear  order by Fin_Idno desc";
                                //DataSet ds = SqlHelper.ExecuteDataset(MultipleDBDAL.strDynamicConString(), CommandType.Text, strSQL);
                                //string strNewDate = ds.Tables[0].Rows[0]["SDate"].ToString();
                                var strNewDate = db.tblFinYears.Select(o => o.StartDate).Max();
                        //        lPrty.Lorry_Idno = intValue;
                        //        lPrty.Acnt_Idno = PartyName;
                                //lPrty.Valid_From = Convert.ToDateTime(strNewDate);
                                db.SaveChanges();
                            }
                        }
                        IList<LorryMast> obj = db.LorryMasts.Where(lm => lm.Pan_No == objLorryMast.Pan_No).ToList();
                        if (obj.Count > 0)
                        {
                            foreach (LorryMast rw in obj)
                            {
                                rw.CalOn_DF = objLorryMast.CalOn_DF;
                                rw.LowRateWise = objLorryMast.LowRateWise;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }


        public Int64 SelectIDforEdit(Int64 Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from FO in db.LorryMasts where FO.Lorry_Idno == Idno select FO.Lorry_Idno).Max();
            }
        }

        /// <summary>
        /// Delete record from LorryMast
        /// </summary>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>
        public int Delete(int intLorryIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    LorryMast objLorryMast = (from mast in db.LorryMasts
                                              where mast.Lorry_Idno == intLorryIdno
                                              select mast).FirstOrDefault();
                    if (objLorryMast != null)
                    {
                        db.LorryMasts.DeleteObject(objLorryMast);
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

        public Int32 UpdateStatus(int intLorryIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    LorryMast objLorryMast = (from mast in db.LorryMasts
                                              where mast.Lorry_Idno == intLorryIdno
                                              select mast).FirstOrDefault();
                    if (objLorryMast != null)
                    {

                        objLorryMast.Status = Status;
                        objLorryMast.Emp_Idno = empIdno;
                        objLorryMast.Date_Modified = System.DateTime.Now;
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }

        #endregion

        public IList CheckItemExistInOtherMaster(Int32 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj10 in db.TblGrHeads
                        where obj10.Lorry_Idno == LorryIdno
                        select new { ItemGrp = obj10.Lorry_Idno }
                          ).ToList()
                           .Union
                          (from obj12 in db.tblPBillHeads
                           where obj12.LorryIdno == LorryIdno
                           select new { ItemGrp = obj12.LorryIdno }
                          ).ToList()
                           .Union
                          (from obj13 in db.MatIssHeads
                           where obj13.Truck_Idno == LorryIdno
                           select new { ItemGrp = obj13.Truck_Idno }
                          ).ToList()
                          .Union
                          (from obj14 in db.tblTripHeads
                           where obj14.Truck_Idno == LorryIdno
                           select new { ItemGrp = obj14.Truck_Idno }
                          ).ToList()
                           .Union
                          (from obj15 in db.tblChlnBookHeads
                           where obj15.Truck_Idno == LorryIdno
                           select new { ItemGrp = obj15.Truck_Idno }
                          ).ToList()
                         ;
            }
        }

        public Int64 Count()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.LorryMasts
                           join obj in db.AcntMasts on mast.Prty_Idno equals obj.Acnt_Idno
                           select new
                           {

                               Party_Idno = mast.Prty_Idno
                           }).Count();
                return lst;
            }
        }


        public Int64 SavDriver(string DriverName, Int32 DateRange, Int64 StateId, Int64 CityId)
        {
            Int64 Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntMast ObjExist = (from N in db.AcntMasts where N.Acnt_Type == 9 && N.Acnt_Name == DriverName select N).FirstOrDefault();
                    if (ObjExist != null)
                    {
                        Value = -1;
                    }
                    else
                    {
                        AcntMast ObjNew = new AcntMast();
                        ObjNew.AGrp_Idno = 2;
                        ObjNew.ASubGrp_Idno = 2;
                        ObjNew.Open_Bal = 0;
                        ObjNew.Bal_Type = 1;
                        ObjNew.Acnt_Type = 9;
                        ObjNew.Titl_Idno = 1;
                        ObjNew.Acnt_Name = DriverName;
                        ObjNew.AcntName_Hindi = "";
                        ObjNew.Year_Idno = DateRange;
                        ObjNew.State_Idno = StateId;
                        ObjNew.City_Idno = CityId;
                        ObjNew.ServTax_Exmpt = false;
                        ObjNew.Agnt_Commson = 0;
                        ObjNew.Comp_Idno = 1;
                        ObjNew.Address1 = "";
                        ObjNew.Address2 = "";
                        ObjNew.Status = true;
                        db.AcntMasts.AddObject(ObjNew);
                        db.SaveChanges();
                        Value = ObjNew.Acnt_Idno; ;
                    }

                }
            }
            catch (Exception Ex)
            {
                Value = 0;
            }

            return Value;
        }

        public Int64 SavDriverHire(string DriverName, string LicenseNO)
        {
            Int64 Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    DriverMast ObjExist = (from N in db.DriverMasts where N.Driver_Name == DriverName select N).FirstOrDefault();
                    if (ObjExist != null)
                    {
                        Value = -1;
                    }
                    else
                    {
                        DriverMast ObjNew = new DriverMast();
                        ObjNew.Driver_Name = DriverName;
                        ObjNew.License_No = LicenseNO;
                        ObjNew.Status = true;
                        db.DriverMasts.AddObject(ObjNew);
                        db.SaveChanges();
                        Value = ObjNew.Driver_Idno;
                    }
                }
            }
            catch (Exception Ex)
            {
                Value = 0;
            }

            return Value;
        }

        public DataSet SelectLorryDetails(string conStrng)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", "CheckLorryDetails");
            DataSet objDsTemp = SqlHelper.ExecuteDataset(conStrng, CommandType.StoredProcedure, "spLorryDates", objSqlPara);
            return objDsTemp;
        }
        public DataTable SelectLorryIDs(string date, string conStrng)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "LorryDetailsDateWise");
            objSqlPara[1] = new SqlParameter("@Date", date);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(conStrng, CommandType.StoredProcedure, "spLorryDates", objSqlPara);
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
        public DataTable SelectLorrys(string idnos, string conStrng)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "LorryDetails");
            objSqlPara[1] = new SqlParameter("@StrIdnos", idnos);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(conStrng, CommandType.StoredProcedure, "spLorryDates", objSqlPara);
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
        public DataTable SelectLorryDetDateWise(string date, string conStrng)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "LorryDetDateWise");
            objSqlPara[1] = new SqlParameter("@Date", date);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(conStrng, CommandType.StoredProcedure, "spLorryDates", objSqlPara);
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
        public DataTable SelectLorryDetDateIDWise(string date, string ID, string conStrng)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "LorryDetDateIDWise");
            objSqlPara[1] = new SqlParameter("@Date", date);
            objSqlPara[2] = new SqlParameter("@ID", ID);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(conStrng, CommandType.StoredProcedure, "spLorryDates", objSqlPara);
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

        public int UpdateLorryByParty(Int32 PartyIdno, String PANNo)
        {
            int count = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    List<LorryMast> lst = db.LorryMasts.Where(x => x.Prty_Idno == PartyIdno).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (LorryMast lorry in lst)
                        {
                            lorry.Pan_No = PANNo;
                            db.SaveChanges();
                            count++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return count;
            }
            return count;
        }
    }
}
