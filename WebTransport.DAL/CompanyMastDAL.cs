using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebTransport.DAL
{
    public class CompanyMastDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from tblCompanyMaster
        /// </summary>
        /// <returns></returns>
        public tblCompMast SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast Compmast = (from mast in db.tblCompMasts
                                        orderby mast.Comp_Name
                                        select mast).FirstOrDefault();

                return Compmast;
            }
        }
               

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intWorkCompIdno"></param>
        /// <param name="strWorkCompName"></param>
        ///// <returns></returns>

        public IList SelectForSearch(string strCompName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblCompMasts
                        where mast.Comp_Name == strCompName
                        orderby mast.Comp_Name
                        select mast).ToList();
            }
        }

        /// <summary>
        /// Select one record from tblCompanyMaster by WorkCompIdno
        /// </summary>
        /// <param name="intWorkCompIdno"></param>
        /// <returns></returns>
        public tblCompMast SelectById(int intWorkCompIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblCompMasts
                        //where mast.Comp_Idno == intWorkCompIdno
                        select mast).FirstOrDefault();
            }
        }

        //public IList SelectCountry()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from CM in db.tblCountryMasts select CM).ToList();
        //    }
        //}

        /// <summary>
        /// To check record existence in tblCompanyMaster
        /// </summary>
        /// <param name="strWorkCompName"></param>
        /// <param name="intWorkCompIdno"></param>
        /// <returns></returns>
        /// 


        public bool IsExists(string strWorkCompName, int intWorkCompIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast objtblCompanyMaster = new tblCompMast();
                if (intWorkCompIdno <= 0)
                {
                    objtblCompanyMaster = (from mast in db.tblCompMasts
                                           where mast.Comp_Name == strWorkCompName
                                           select mast).FirstOrDefault();
                }
                else if (intWorkCompIdno > 0)
                {
                    objtblCompanyMaster = (from mast in db.tblCompMasts
                                           where mast.Comp_Name == strWorkCompName
                                             && mast.Comp_Idno != intWorkCompIdno
                                           select mast).FirstOrDefault();
                }
                if (objtblCompanyMaster != null)
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

        public Int64 Insert(string CompName,string CompDesc ,string Address1, string Address2, string CityIdno, string StateIdno, string PinNo, string TinNo,
         string EmailId, string CSTNo, string TanNo, string PanNo, string FaxNo, string CodeNo, string ContactPN1, string ContactPN2, string Mobile1, string Mobile2, string EmailID1, string EmailID2, bool Active, string ServTaxNo,string SapNo,string RegNo,string GSTINNO)
        {
            Int64 intValue = 0;
            //try
            //{
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if (IsExists(CompName, 0) == true)
                {
                    intValue = -1;
                }
                else
                {
                    tblCompMast objtblCompanyMaster = new tblCompMast();
                    objtblCompanyMaster.Comp_Name = CompName;
                    objtblCompanyMaster.Adress1 = Address1;
                    objtblCompanyMaster.Adress2 = Address2;
                    objtblCompanyMaster.City_Idno = CityIdno;
                    objtblCompanyMaster.State_Idno = StateIdno;
                    objtblCompanyMaster.Pin_No = PinNo;
                    objtblCompanyMaster.TIN_NO = TinNo;
                    objtblCompanyMaster.CompDescription = CompDesc;
                    //objtblCompanyMaster.Phone_Off = PhoneOff;
                    //objtblCompanyMaster.Phone_Res = PhoneRes;
                    objtblCompanyMaster.Comp_Mail = EmailId;
                    //objtblCompanyMaster.Fax_No = FaxNo;
                    //objtblCompanyMaster.Code_No = CodeNo;
                    //objtblCompanyMaster.Trad_Cert = TradCert;
                    //objtblCompanyMaster.Comp_Jurs = CompJurs;
                    //objtblCompanyMaster.Serv_Tax = ServTax;
                    //objtblCompanyMaster.Year_Handle = YearHandle;
                    //objtblCompanyMaster.VAT_Reqd = VATReqd;
                    //objtblCompanyMaster.Pur_Rate = PurRate;
                    //objtblCompanyMaster.Sale_Bill = SaleBill;
                    //objtblCompanyMaster.Frie_Aft_Dis = FrieAftDis;
                    //objtblCompanyMaster.Sq_JobOrder = SqJobOrder;
                    //objtblCompanyMaster.Allow_Neg_Stk = AllowNegStk;
                    //objtblCompanyMaster.StkTran_Req = StkTranReq;
                    //objtblCompanyMaster.Ser_Tax_Req = SerTaxReq;
                    objtblCompanyMaster.CST_No = CSTNo;
                    objtblCompanyMaster.Tan_No = TanNo;
                    objtblCompanyMaster.Pan_No = PanNo;
                    objtblCompanyMaster.Fax_No = FaxNo;
                    objtblCompanyMaster.Code_No = CodeNo;
                    objtblCompanyMaster.Contact_PN1 = ContactPN1;
                    objtblCompanyMaster.Contact_PN2 = ContactPN2;
                    objtblCompanyMaster.Mobile_1 = Mobile1;
                    objtblCompanyMaster.Mobile_2 = Mobile2;
                    objtblCompanyMaster.Email_ID_1 = EmailID1;
                    objtblCompanyMaster.Email_ID_2 = EmailID2;
                    objtblCompanyMaster.Status = Active;
                    objtblCompanyMaster.Date_Added = System.DateTime.Now;
                    objtblCompanyMaster.ServTaxNo = ServTaxNo;
                    objtblCompanyMaster.SAP_No = SapNo;
                    objtblCompanyMaster.Reg_No = RegNo;
                    objtblCompanyMaster.CompGSTIN_No = GSTINNO;
                    //objtblCompanyMaster.Date_Modified = System.DateTime.Now;

                    db.AddTotblCompMasts(objtblCompanyMaster);
                    db.SaveChanges();
                    intValue = objtblCompanyMaster.Comp_Idno;
                }
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return intValue;
        }

        public int Update(string CompName, string CompDesc, string Address1, string Address2, string PinNo, string TinNo, string EmailId, string CSTNo, string TanNo, string PanNo, string FaxNo, string CodeNo, string ContactPN1, string ContactPN2, string Mobile1, string Mobile2, string EmailID1, string EmailID2, bool Active, int intWorkCompIdno, int iTotLoc, string OwnMobileNum, string PhoneNumber, string SMS_UserName, string SMS_Password, string SMS_SenderID, string SMS_ProfileID, string SMS_AuthType, string SMS_AuthKey, string ServTaxNo, string SapNo, string RegNo,string GSTINNo,string PropStatus)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCompMast objtblCompanyMaster = (from mast in db.tblCompMasts
                                                       where mast.Comp_Idno == intWorkCompIdno
                                                       && mast.Comp_Name == CompName
                                                       select mast).FirstOrDefault();
                    if (objtblCompanyMaster != null)
                    {
                        objtblCompanyMaster.Comp_Name = CompName;
                        objtblCompanyMaster.CompDescription = CompDesc;
                        objtblCompanyMaster.Adress1 = Address1;
                        objtblCompanyMaster.Adress2 = Address2;
                        objtblCompanyMaster.Pin_No = PinNo;
                        objtblCompanyMaster.TIN_NO = TinNo;
                        objtblCompanyMaster.Comp_Mail = EmailId;
                        objtblCompanyMaster.CST_No = CSTNo;
                        objtblCompanyMaster.Tan_No = TanNo;
                        objtblCompanyMaster.Pan_No = PanNo;
                        objtblCompanyMaster.Fax_No = FaxNo;
                        objtblCompanyMaster.Code_No = CodeNo;
                        objtblCompanyMaster.Contact_PN1 = ContactPN1;
                        objtblCompanyMaster.Contact_PN2 = ContactPN2;
                        objtblCompanyMaster.Mobile_1 = Mobile1;
                        objtblCompanyMaster.Mobile_2 = Mobile2;
                        objtblCompanyMaster.Email_ID_1 = EmailID1;
                        objtblCompanyMaster.Email_ID_2 = EmailID2;
                        objtblCompanyMaster.Status = Active;
                        objtblCompanyMaster.Tot_Loc = iTotLoc;
                        objtblCompanyMaster.Mobile_own = OwnMobileNum;
                        objtblCompanyMaster.Phone_Off = PhoneNumber;
                        objtblCompanyMaster.Date_Modified = System.DateTime.Now;
                        objtblCompanyMaster.PStatus = PropStatus;
                        objtblCompanyMaster.SMS_UserName = SMS_UserName;
                        objtblCompanyMaster.SMS_Password = SMS_Password;
                        objtblCompanyMaster.SMS_SenderID = SMS_SenderID;
                        objtblCompanyMaster.SMS_ProfileID = SMS_ProfileID;
                        objtblCompanyMaster.SMS_AuthType = SMS_AuthType;
                        objtblCompanyMaster.SMS_AuthKey = SMS_AuthKey;
                        objtblCompanyMaster.ServTaxNo = ServTaxNo;
                        objtblCompanyMaster.SAP_No = SapNo;
                        objtblCompanyMaster.Reg_No = RegNo;
                        objtblCompanyMaster.CompGSTIN_No = GSTINNo;
                        //if (IsExists(CompName, intWorkCompIdno) == true)
                        //{
                        //    intValue = -1;
                        //}
                        //else
                        //{
                           db.SaveChanges();
                           intValue = intWorkCompIdno;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        /// <summary>
        /// Delete record from tblCompanyMaster
        /// </summary>
        /// <param name="intWorkCompIdno"></param>
        /// <returns></returns>
        public int Delete(int intWorkCompIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCompMast objtblCompanyMaster = (from mast in db.tblCompMasts
                                                       where mast.Comp_Idno == intWorkCompIdno
                                                       select mast).FirstOrDefault();
                    if (objtblCompanyMaster != null)
                    {
                        db.DeleteObject(objtblCompanyMaster);
                        db.SaveChanges();
                        intValue = 1;
                    }
                }
            }
            catch
            {
            }
            return intValue;
        }

        public Int32 UpdateStatus(int intWorkCompIdno, bool Status)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCompMast objtblCompanyMaster = (from mast in db.tblCompMasts
                                                       where mast.Comp_Idno == intWorkCompIdno
                                                       select mast).FirstOrDefault();
                    if (objtblCompanyMaster != null)
                    {
                        objtblCompanyMaster.Status = Status;
                        objtblCompanyMaster.Date_Modified = System.DateTime.Now;
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
