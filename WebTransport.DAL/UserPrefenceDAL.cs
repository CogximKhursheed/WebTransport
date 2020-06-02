using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class UserPrefenceDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from LorryMast
        /// </summary>
        /// <returns></returns>
        /// 

        public List<tblCityMaster> SelectBaseCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from LM in db.tblCityMasters orderby LM.City_Name ascending select LM).ToList();
                return lst;
            }
        }
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblUserPrefs
                        orderby mast.UserPref_Idno
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <param name="strOwnerName"></param>
        /// <returns></returns>


        /// <summary>
        /// Select one record from LorryMast by LorryIdno
        /// </summary>
        /// <param name="intUserIdno"></param>
        /// <returns></returns>
        public tblUserPref SelectById()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblUserPrefs select mast).FirstOrDefault();
            }
        }
        

        /// <summary>
        /// To check record existence in LorryMast
        /// </summary>
        /// <param name="strOwnerrName"></param>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>

public bool IsExists(Int64 intUsrPrefIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref objUserPrefence = new tblUserPref();
                if (intUsrPrefIdno <= 0)
                {
                    objUserPrefence = (from mast in db.tblUserPrefs
                                       where mast.UserPref_Idno == intUsrPrefIdno
                                       select mast).FirstOrDefault();
                }
                else if (intUsrPrefIdno > 0)
                {
                    objUserPrefence = (from mast in db.tblUserPrefs
                                       where mast.UserPref_Idno == intUsrPrefIdno
                                       select mast).FirstOrDefault();
                }
                if (objUserPrefence != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
        //public Int64 InsertLorryMast(string strLorryMake, string strOwnerName, Int32 LorryType, Int64 PartyName, string strChasisNo, string strEngineNo, string strLorryNo, string strPanNo, bool bStatus)
        //{
        //    Int64 intValue = 0;
        //    Int32 intCompIdno = 1;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities())
        //        {
        //            LorryMast objLorryMast = new LorryMast();

        //            objLorryMast.Lorry_Make = strLorryMake;
        //            objLorryMast.Owner_Name = strOwnerName;
        //            objLorryMast.Lorry_Type = LorryType;
        //            objLorryMast.Status = bStatus;
        //            objLorryMast.Comp_Idno = intCompIdno;

        //            objLorryMast.Prty_Idno = PartyName;
        //            objLorryMast.Chassis_no = strChasisNo;
        //            objLorryMast.Eng_No = strEngineNo;
        //            objLorryMast.Lorry_No = strLorryNo;
        //            objLorryMast.Pan_No = strPanNo;

        //            objLorryMast.Date_Added = System.DateTime.Now;
        //            if (IsExists(strOwnerName, 0) == true)
        //            {
        //                intValue = -1;
        //            }
        //            else
        //            {
        //                db.LorryMasts.AddObject(objLorryMast);
        //                db.SaveChanges();
        //                intValue = objLorryMast.Lorry_Idno;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ApplicationFunction.ErrorLog(ex.ToString());
        //    }
        //    return intValue;
        //}

        /// <summary>
        /// Update records in LorryMast
        /// </summary>
        /// <param name="strNarrName"></param>
        /// <param name="strNarrDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intNarrIdno"></param>
        /// <returns></returns>
        public Int64 UpdateUserPrefence(Int64 BaseCity, Int32 CmbType, Int32 InvoicePrint, Int32 ChallanPrint, String strWagesAs, Double ServTax, Double Surchage, Double Bility, Double Wages, Double TollTax, Double ServTxValid, Double StaxPan, bool bTbbRate, bool bTDS, bool GRrate, Int32 AmntRcvdAgnst, Int32 Rate_Invo_Gr, bool ExcelChln, Int32 PayChlnPrint, bool ContRate, bool CntrSBillReq, bool RateEdit, bool DisChlnEntry, bool AdminApproval, bool GrPrintWithoutHeader, int GRPrint, bool ItemGrade, String PFChanges, String TollTaxRename, String refrename, bool LogoReq, Int32 InvGrType, string Terms, string Terms1, string TnCGRRet, bool WeightWise_R, string HireslipTerms, bool GRRetRequired, string strCartage, string strCommission, string strBilty, bool TypeDel, string strStCharge, bool strWithGstAmnt, bool bLessChlnAmntInv, bool GstCalGr)
        {
            Int64 intValue = 0;
            try
            {
               
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserPref objUserPrefence = (from mast in db.tblUserPrefs
                                                   select mast).FirstOrDefault();
                    if (objUserPrefence != null)
                    {
                        objUserPrefence.BaseCity_Idno = BaseCity;
                        objUserPrefence.Work_Type = CmbType;
                        objUserPrefence.InvPrint_Type = InvoicePrint;
                        objUserPrefence.ChlnPrint_Type = ChallanPrint;
                        objUserPrefence.WagesLabel_Print = strWagesAs;
                        objUserPrefence.ServTax_Perc = ServTax;
                        objUserPrefence.Surchg_Per = Surchage;
                        objUserPrefence.Bilty_Amnt = Bility;
                        objUserPrefence.Wages_Amnt = Wages;
                        objUserPrefence.TollTax_Amnt = TollTax;
                        objUserPrefence.GRRate = GRrate;
                        objUserPrefence.TDSEdit = bTDS;
                        objUserPrefence.ServTax_Valid = ServTxValid;
                        objUserPrefence.STaxPer_TDS = StaxPan;
                        objUserPrefence.TBB_Rate = bTbbRate;
                        objUserPrefence.Date_Added = System.DateTime.Now;
                        objUserPrefence.AmntRecvdAgnst_GRInvce = AmntRcvdAgnst;
                        objUserPrefence.Rate_Invo_Gr = Rate_Invo_Gr;
                        objUserPrefence.Chln_Excel = ExcelChln;
                        objUserPrefence.PayChln_Print = PayChlnPrint;
                        objUserPrefence.Cont_Rate = ContRate;
                        objUserPrefence.CntrSBill_Req = CntrSBillReq;
                        objUserPrefence.ShrtgRateChlnConf = RateEdit;
                        objUserPrefence.DisableChlnEntry = DisChlnEntry;
                        objUserPrefence.AdminApp_Inv = AdminApproval;
                        objUserPrefence.GRPrint_WithoutHeader = GrPrintWithoutHeader;
                        objUserPrefence.ItemGrade_Req = ItemGrade;
                        objUserPrefence.PFLabel_GR = PFChanges;
                        objUserPrefence.TollTaxLabel_GR = TollTaxRename;
                        objUserPrefence.Reflabel_Gr = refrename;
                        objUserPrefence.GRPrintPref = GRPrint;
                        objUserPrefence.Logo_Req = LogoReq;
                        objUserPrefence.InvGen_GrType = InvGrType;
                        objUserPrefence.Terms = Terms;
                        objUserPrefence.Terms1 = Terms1;
                        objUserPrefence.WeightWise_Rate = WeightWise_R;
                        objUserPrefence.HireslipTerms = HireslipTerms;
                        objUserPrefence.GRRetRequired = GRRetRequired;
                        objUserPrefence.CartageLabel_GR = strCartage;
                        objUserPrefence.CommissionLabel_Gr = strCommission;
                        objUserPrefence.BiltyLabel_GR = strBilty;
                        objUserPrefence.Terms_Con_Retailer = TnCGRRet;
                        objUserPrefence.TypeDelPlace = TypeDel;
                        objUserPrefence.StChargLabel_GR = strStCharge;
                        objUserPrefence.With_GstAmnt = strWithGstAmnt;
                        objUserPrefence.GstCal_Gr = GstCalGr;
                        if (LogoReq == false) { objUserPrefence.Logo_Image = null; }
                        db.SaveChanges();
                        intValue = objUserPrefence.UserPref_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public Int64 UpdateLogo(byte[] logo,bool Flag)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserPref objUserPrefence = (from mast in db.tblUserPrefs
                                                   select mast).FirstOrDefault();

                    if (objUserPrefence != null)
                    {
                        objUserPrefence.Logo_Req = Flag; 
                        objUserPrefence.Logo_Image = logo;
                        db.SaveChanges();
                        intValue = objUserPrefence.UserPref_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                intValue = 0;
            }
            return intValue;
        }


        public DataTable UpdateTaxVat(Int32 ItemType, Int32 TaxType, double Value, string con)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                SqlParameter[] objSqlPara = new SqlParameter[4];
                objSqlPara[0] = new SqlParameter("@Action", "UpdateTaxVat");
                objSqlPara[1] = new SqlParameter("@ItemType", ItemType);
                objSqlPara[2] = new SqlParameter("@TaxType", TaxType);
                objSqlPara[3] = new SqlParameter("@Value", Value);
                DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spUpdateTaxVat", objSqlPara);
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
        
        public int Delete(int intUsrPrefIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserPref objUserPrefence = (from mast in db.tblUserPrefs
                                                   where mast.UserPref_Idno == intUsrPrefIdno
                                                   select mast).FirstOrDefault();
                    if (objUserPrefence != null)
                    {
                        db.tblUserPrefs.DeleteObject(objUserPrefence);
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

        public Int32 UpdateStatus(int intUsrPrefIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblUserPref objUserPrefence = (from mast in db.tblUserPrefs
                                                   where mast.UserPref_Idno == intUsrPrefIdno
                                                   select mast).FirstOrDefault();
                    if (objUserPrefence != null)
                    {

                        objUserPrefence.Date_Modified = System.DateTime.Now;
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
    }
}
