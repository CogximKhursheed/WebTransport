using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Transactions;
using System.Data;


namespace WebTransport.DAL
{
    public class LedgerAccountDAL
    {
        //#region Select Events...

        /// <summary>
        /// Select all records from AcntMast
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.AcntMasts
                        orderby mast.Acnt_Name
                        select mast).ToList();
            }
        }

        public IList SelectStateCity(Int64 UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblUserDefaults
                        where mast.User_idno==UserId
                        select new 
                        {
                          mast.StateId,
                          mast.CityId,
                         
                         }).ToList();
            }
        }
        
        public IList SelectForSearch(string strPartyName, string strMobileNo, int intAcntType, int intState, int intBalanceType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.AcntMasts
                           join SM in db.tblStateMasters on mast.State_Idno equals SM.State_Idno
                           into temp
                           from j in temp.DefaultIfEmpty()
                           join CM in db.tblCityMasters on mast.City_Idno equals CM.City_Idno
                           into temp1
                           from j1 in temp1.DefaultIfEmpty()
                           join A in db.AcntTypes on mast.Acnt_Type equals A.AcntType_Idno
                           where mast.INTERNAL == false
                           select new
                           {
                               AcntIdno = mast.Acnt_Idno,
                               AcntType = mast.Acnt_Type,
                               AcntType_Name = A.AcntType_Name,
                               MobileNo = mast.Cont_Mobile,
                               Address = mast.Address1 + mast.Address2,
                               Title = mast.Titl_Idno,
                               BalanceType = mast.Bal_Type == 1 ? "Cr" : "Dr",
                               Acnt_Name = mast.Acnt_Name,
                               State_Idno = mast.State_Idno,
                               State_Name = j.State_Name,
                               City_Idno = mast.City_Idno,
                               City_Name = j1.City_Name,
                               OpBalnce = mast.Open_Bal,
                               Status = mast.Status
                           }).ToList();
                if (strPartyName != "")
                {
                    lst = (from I in lst where I.Acnt_Name.ToLower().Contains(strPartyName.ToLower()) select I).ToList();
                }
                if (strMobileNo != "")
                {
                    lst = (from I in lst where I.MobileNo == (strMobileNo) select I).ToList();
                }
                if (intAcntType > 0)
                {
                    lst = (from I in lst where I.AcntType == intAcntType select I).ToList();
                }
                if (intState > 0)
                {
                    lst = (from I in lst where I.State_Idno == intState select I).ToList();
                }
                string BalTYpe = intBalanceType == 1 ? "Cr" : "Dr";
                if (intBalanceType > 0)
                {
                    lst = (from I in lst where I.BalanceType == BalTYpe select I).ToList();
                }

                return lst;
            }
        }
         public Int64 CountTotal(int intAcntType,  int intBalanceType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 lst = 0;
                lst = (from mast in db.AcntMasts
                           join SM in db.tblStateMasters on mast.State_Idno equals SM.State_Idno
                           into temp
                           from j in temp.DefaultIfEmpty()
                           join CM in db.tblCityMasters on mast.City_Idno equals CM.City_Idno
                           into temp1
                           from j1 in temp1.DefaultIfEmpty()
                           join A in db.AcntTypes on mast.Acnt_Type equals A.AcntType_Idno
                           select new
                           {
                               AcntIdno = mast.Acnt_Idno,

                           }).Count();

                return lst;
            }
        }
        public AcntMast SelectById(int intAcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.AcntMasts
                        where mast.Acnt_Idno == intAcntIdno
                        select mast).FirstOrDefault();
            }
        }
        public IList SelectDocHolder(Int32 Acnt_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblDocHolders
                           where obj.Acnt_idno == Acnt_Idno
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
        public List<AcntSubHead> FetchAcntSubHead()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntSubHead> lst = null;
                lst = (from cm in db.AcntSubHeads where cm.Status==true orderby cm.ASubHead_Name ascending select  cm).ToList();
                return lst;
            }
        }
        public List<AcntMast> FetchSender()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from cm in db.AcntMasts where cm.Acnt_Type == 2 && cm.INTERNAL == false orderby cm.Acnt_Name ascending select cm).ToList();
                return lst;
            }
        }
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

        public List<tblDistrictMaster> BindDistrictwithStateId(int stateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblDistrictMaster> lst = new List<tblDistrictMaster>();
                if (stateIdno > 0)
                {
                    lst = (from cm in db.tblDistrictMasters
                           where cm.State_Idno == stateIdno && cm.Status == true
                           orderby cm.District_Name
                           select cm).ToList();
                }
                return lst;
            }
        }
        public List<tblDistrictMaster> BindToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblDistrictMaster> objtblDistrictMaster = new List<tblDistrictMaster>();
                objtblDistrictMaster = (from obj in db.tblDistrictMasters
                                    orderby obj.District_Name
                                    select obj).ToList();
                return objtblDistrictMaster;
            }
        }
        public List<tblCityMaster> BindToDistrict()
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

        //public List<tblStateMaster> SelectState()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        List<tblStateMaster> lst = null;
        //        lst = (from cm in db.tblStateMasters orderby cm.State_Name ascending select cm).ToList();
        //        return lst;
        //    }
        //}
        // public List<tblCityMaster> SelectCity()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        List<tblCityMaster> lst = null;
        //        lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
        //        return lst;
        //    }
        //}
        public List<AcntType> FetchAcntType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntType> lst = null;
                lst = (from cm in db.AcntTypes orderby cm.AcntType_Name ascending select cm).ToList();
                return lst;
            }
        }

        public List<tblCategoryMast> FetchCategory()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCategoryMast> lst = null;
                lst = (from cm in db.tblCategoryMasts orderby cm.Category_Name ascending select cm).ToList();
                return lst;
            }
        }
        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in UOMMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(Int64 Title, string strPartyName, string strPartyNameHindi, Int64 AcntType, Int64 AcntSubGroup, int BalnceType, double OpenBalns, double AgentComm, bool bServTaxExmpt,
            string strContPerson, string strContMobile, string strContEmail, string strAddress1, string strAddress2, Int64 state, Int64 city, Int64 district, bool bStatus, int Pincode, string strFaxNo, Int32 Year_Idno, string LincenceNo, DateTime? ExpiryDate, string Authority, string AccountNo, int Guarantor, Boolean varified, string FatherName, string DriverAddress, string Mobile1, string Mobile2, string BankName, string BankAddress, string RTGScode, string HazardousLicence, DateTime? HazardousExpiryDate, Int64 PCompIdno, Int32 CatIdno, string TinNo, Int32 empIdno, Int64 intPincIdno, string strPanNo, string AccNo, string AccBankName, string AccBranchName, string AccIfscCode, double DetentionPlantcharge, double DetentionPortcharge, double Containerliftcharge, string Lst_No, string Cst_No, string GSTINNo, string ShortName, DataTable dt)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            Int64 AcntHeadIdno = 0;

            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    long AgrpIndo = Convert.ToInt64((from obj in db.AcntSubHeads where obj.ASubHead_Idno == AcntSubGroup select obj.AHead_Idno).FirstOrDefault());
                    AcntMast objAcntMast = new AcntMast();
                    objAcntMast.Emp_Idno = empIdno;
                    objAcntMast.Acnt_Name = strPartyName;
                    objAcntMast.AcntName_Hindi = strPartyNameHindi;
                    objAcntMast.Titl_Idno = Title;
                    objAcntMast.Acnt_Type = AcntType;
                    objAcntMast.AGrp_Idno = Convert.ToInt64(AgrpIndo);
                    objAcntMast.ASubGrp_Idno = AcntSubGroup;
                    objAcntMast.Bal_Type = BalnceType;
                    objAcntMast.Open_Bal = OpenBalns;
                    objAcntMast.Agnt_Commson = AgentComm;
                    objAcntMast.ServTax_Exmpt = bServTaxExmpt;
                    objAcntMast.Cont_Person = strContPerson;
                    objAcntMast.Cont_Mobile = strContMobile;
                    objAcntMast.Cont_Email = strContEmail;
                    objAcntMast.Address1 = strAddress1;
                    objAcntMast.Address2 = strAddress2;
                    objAcntMast.State_Idno = state;
                    objAcntMast.City_Idno = city;
                    objAcntMast.District_Idno = district;
                    objAcntMast.Pin_Code = Pincode;
                    objAcntMast.Fax_No = strFaxNo;
                    objAcntMast.INTERNAL = false;
                    objAcntMast.Status = bStatus;
                    objAcntMast.Year_Idno = Year_Idno;
                    objAcntMast.Comp_Idno = intCompIdno;
                    objAcntMast.Date_Added = System.DateTime.Now;

                    objAcntMast.DrvLicnc_NO = LincenceNo;
                    objAcntMast.DrvLNo_ExpDate = ExpiryDate;
                    objAcntMast.DrvAuthrty_Plc = Authority;
                    objAcntMast.DrvAcnt_No = AccountNo;
                    objAcntMast.DrvLNo_Verfd = varified;
                    objAcntMast.DrvGurntr_Idno = Guarantor;

                    objAcntMast.DrvFather_Nm = FatherName;
                    objAcntMast.Drv_Adres = DriverAddress;
                    objAcntMast.Drv_MobNo1 = Mobile1;
                    objAcntMast.Drv_MobNo2 = Mobile2;
                    objAcntMast.DrvBankAc_Nm = BankName;
                    objAcntMast.DrvBankAc_Adres = BankAddress;
                    objAcntMast.DrvAc_RtgsCode = RTGScode;
                    objAcntMast.DrvHazardLic_No = HazardousLicence;
                    objAcntMast.DrvHazardLic_NoExpDt = HazardousExpiryDate;
                    objAcntMast.PetrolComp_Idno = PCompIdno;
                    objAcntMast.Pan_No = strPanNo;

                    objAcntMast.Category_Idno = CatIdno;
                    objAcntMast.TinNo = TinNo;
                    objAcntMast.PComp_Idno = intPincIdno;
                    objAcntMast.Account_No = AccNo;
                    objAcntMast.Bank_Name = AccBankName;
                    objAcntMast.Branch_Name = AccBranchName;
                    objAcntMast.Ifsc_Code = AccIfscCode;
                    objAcntMast.PComp_Idno = intPincIdno;
                    objAcntMast.detenPlant_charg = DetentionPlantcharge;
                    objAcntMast.detenPort_charg = DetentionPortcharge;
                    objAcntMast.Container_charg = Containerliftcharge;
                    objAcntMast.Lst_No=Lst_No;
                    objAcntMast.Cst_No = Cst_No;
                    objAcntMast.LdgrGSTIN_No = GSTINNo;
                    objAcntMast.Short_Name = ShortName;
                    if (IsExists(strPartyName, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.AcntMasts.AddObject(objAcntMast);
                        db.SaveChanges();
                        intValue = objAcntMast.Acnt_Idno;
                    }
                    if (intValue > 0)
                    {
                        if (IsExistsOpen(Convert.ToInt64(Year_Idno), intValue) == true)
                        {
                            AcntOpen open = (from o in db.AcntOpens where o.Acnt_Idno == intValue && o.Year_Idno == Year_Idno select o).FirstOrDefault();
                            open.Open_Bal = OpenBalns;
                            open.Year_Idno = Year_Idno;
                            open.Acnt_Idno = intValue;
                            open.DateModified = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            AcntOpen open = new AcntOpen();
                            open.Open_Bal = OpenBalns;
                            open.Open_Type = BalnceType;
                            open.Year_Idno = Year_Idno;
                            open.Acnt_Idno = intValue;
                            open.DateModified = DateTime.Now;
                            open.DateAdded = DateTime.Now;
                            db.AcntOpens.AddObject(open);
                            db.SaveChanges();
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            tblDocHolder objDocHolderDelete = (from mast in db.tblDocHolders
                                                               where mast.Acnt_idno == intValue
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
                                objDocHolderInsert.Acnt_idno = intValue;
                                objDocHolderInsert.Date_Added = System.DateTime.Now;
                                db.tblDocHolders.AddObject(objDocHolderInsert);
                                db.SaveChanges();
                            }
                        }
                    }
                    return intValue;
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        /// <summary>
        /// Update records in AcntMast
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>
        public int Update(Int64 Title, string strPartyName, string strPartyNameHindi, Int64 AcntType, Int64 AcntSubGroup, int BalnceType, double OpenBalns, double AgentComm, bool bServTaxExmpt,
            string strContPerson, string strContMobile, string strContEmail, string strAddress1, string strAddress2, Int64 state, Int64 city, Int64 district, bool bStatus, int Pincode, string strFaxNo, int intAcntIdno, Int32 Year_Idno, string LincenceNo, DateTime? ExpiryDate, string Authority, string AccountNo, int Guarantor, Boolean varified, string FatherName, string DriverAddress, string Mobile1, string Mobile2, string BankName, string BankAddress, string RTGScode, string HazardousLicence, DateTime? HazardousExpiryDate, Int64 PCompIdno, Int32 CatIdno, string TinNo, Int32 empIdno, Int64 intPincIdno, string strPanNo, string AccNo, string AccBankName, string AccBranchName, string AccIfscCode, double DetentionPlantcharge, double DetentionPortcharge, double Containerliftcharge, string Lst_No, string Cst_No, string GSTINNo, string ShortName, DataTable dt)
        {
            int intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntMast objAcntMast1 = (from mast in db.AcntMasts
                                             where mast.Acnt_Idno == intAcntIdno
                                             select mast).FirstOrDefault();
                    if (objAcntMast1 != null)
                    {
                        objAcntMast1.Acnt_Name = strPartyName;
                        objAcntMast1.Emp_Idno = empIdno;
                        objAcntMast1.AcntName_Hindi = strPartyNameHindi;
                        objAcntMast1.Titl_Idno = Title;
                        objAcntMast1.Acnt_Type = AcntType;
                        objAcntMast1.AGrp_Idno = Convert.ToInt64((from obj in db.AcntSubHeads where obj.ASubHead_Idno == AcntSubGroup select obj.AHead_Idno).FirstOrDefault());
                        objAcntMast1.ASubGrp_Idno = AcntSubGroup;
                        objAcntMast1.Bal_Type = BalnceType;
                        objAcntMast1.Open_Bal = OpenBalns;
                        objAcntMast1.Agnt_Commson = AgentComm;
                        objAcntMast1.ServTax_Exmpt = bServTaxExmpt;
                        objAcntMast1.Cont_Person = strContPerson;
                        objAcntMast1.Cont_Mobile = strContMobile;
                        objAcntMast1.Cont_Email = strContEmail;
                        objAcntMast1.Address1 = strAddress1;
                        objAcntMast1.Address2 = strAddress2;
                        objAcntMast1.State_Idno = state;
                        objAcntMast1.Year_Idno = Year_Idno;
                        objAcntMast1.City_Idno = city;
                        objAcntMast1.District_Idno = district;
                        objAcntMast1.Pin_Code = Pincode;
                        objAcntMast1.Fax_No = strFaxNo;
                        objAcntMast1.INTERNAL = false;
                        objAcntMast1.Status = bStatus;
                        objAcntMast1.Comp_Idno = intCompIdno;
                        objAcntMast1.Date_Modified = System.DateTime.Now;

                        objAcntMast1.DrvLicnc_NO = LincenceNo;
                        objAcntMast1.DrvLNo_ExpDate = ExpiryDate;
                        objAcntMast1.DrvAuthrty_Plc = Authority;
                        objAcntMast1.DrvAcnt_No = AccountNo;
                        objAcntMast1.DrvLNo_Verfd = varified;
                        objAcntMast1.DrvGurntr_Idno = Guarantor;

                        objAcntMast1.DrvFather_Nm = FatherName;
                        objAcntMast1.Drv_Adres = DriverAddress;
                        objAcntMast1.Drv_MobNo1 = Mobile1;
                        objAcntMast1.Drv_MobNo2 = Mobile2;
                        objAcntMast1.DrvBankAc_Nm = BankName;
                        objAcntMast1.DrvBankAc_Adres = BankAddress;
                        objAcntMast1.DrvAc_RtgsCode = RTGScode;
                        objAcntMast1.DrvHazardLic_No = HazardousLicence;
                        objAcntMast1.DrvHazardLic_NoExpDt = HazardousExpiryDate;
                        objAcntMast1.PetrolComp_Idno = PCompIdno;
                        objAcntMast1.Category_Idno = CatIdno;
                        objAcntMast1.TinNo = TinNo;
                        objAcntMast1.Pan_No = strPanNo;
                        objAcntMast1.Account_No = AccNo;
                        objAcntMast1.Bank_Name = AccBankName;
                        objAcntMast1.Branch_Name = AccBranchName;
                        objAcntMast1.Ifsc_Code = AccIfscCode;
                        objAcntMast1.PComp_Idno = intPincIdno;

                        objAcntMast1.detenPlant_charg = DetentionPlantcharge;
                        objAcntMast1.detenPort_charg = DetentionPortcharge;
                        objAcntMast1.Container_charg = Containerliftcharge;

                        objAcntMast1.Lst_No = Lst_No;
                        objAcntMast1.Cst_No = Cst_No;
                        objAcntMast1.LdgrGSTIN_No = GSTINNo;
                        objAcntMast1.Short_Name = ShortName;
                        if (IsExists(strPartyName, intAcntIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intAcntIdno;
                        }
                        if (intValue > 0)
                        {
                            if (IsExistsOpen(Convert.ToInt64(Year_Idno), intAcntIdno) == true)
                            {
                                AcntOpen open = (from o in db.AcntOpens where o.Acnt_Idno == intValue && o.Year_Idno == Year_Idno select o).FirstOrDefault();
                                open.Open_Bal = OpenBalns;
                                open.Open_Type = BalnceType;
                                open.Acnt_Idno = intValue;
                                open.DateModified = DateTime.Now;
                                db.SaveChanges();
                            }
                            else
                            {
                                AcntOpen open = new AcntOpen();
                                open.Open_Bal = OpenBalns;
                                open.Open_Type = BalnceType;
                                open.Year_Idno = Year_Idno;
                                open.Acnt_Idno = intValue;
                                open.DateModified = DateTime.Now;
                                open.DateAdded = DateTime.Now;
                                db.AcntOpens.AddObject(open);
                                db.SaveChanges();
                            }
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                tblDocHolder objDocHolderDelete = (from mast in db.tblDocHolders
                                                                   where mast.Acnt_idno == intValue
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
                                    objDocHolderInsert.Acnt_idno = intValue;
                                    objDocHolderInsert.Date_Added = System.DateTime.Now;
                                    db.tblDocHolders.AddObject(objDocHolderInsert);
                                    db.SaveChanges();
                                }
                            }
                        }
                        return intValue;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }
        #endregion

        public bool IsExists(string strPartyName, int intAcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast objAcntMast = new AcntMast();
                if (intAcntIdno <= 0)
                {
                    objAcntMast = (from mast in db.AcntMasts
                                   where mast.Acnt_Name == strPartyName
                                   select mast).FirstOrDefault();
                }
                else if (intAcntIdno > 0)
                {
                    objAcntMast = (from mast in db.AcntMasts
                                   where mast.Acnt_Name == strPartyName
                                        && mast.Acnt_Idno != intAcntIdno
                                   select mast).FirstOrDefault();
                }
                if (objAcntMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool IsExistsOpen(Int64 YearID,Int64 Acnt_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntOpen obj = (from open in  db.AcntOpens where open.Acnt_Idno==Acnt_Idno && open.Year_Idno==YearID select open).FirstOrDefault();
                if(obj!=null)
                    return true;
                else
                    return false;
                
            }
        }
        //public int Delete(Int64 ledgrAcntId, Int32 DlrTyp)
        //{
        //    int Value = 0;
        //    LedgerAccountDAL objLedgrDAL = new LedgerAccountDAL();
        //    if (DlrTyp == 5 || DlrTyp == 9)
        //    {
        //        Value = objLedgrDAL.DeleteWorkComp(ledgrAcntId, DlrTyp);
        //    }
        //    else
        //    {
        //        Value = objLedgrDAL.Delete(ledgrAcntId);
        //    }

        //    objLedgrDAL = null;
        //    return Value;

        //}
        public int Delete(Int64 ledgrAcntId, Int32 DlrTyp)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntMast objAcntMast = (from mast in db.AcntMasts
                                            where mast.Acnt_Idno == ledgrAcntId
                                            select mast).FirstOrDefault();
                    if (objAcntMast != null)
                    {
                        db.AcntMasts.DeleteObject(objAcntMast);
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
        public Int32 UpdateStatus(int intAcntIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AcntMast objAcntMast = (from mast in db.AcntMasts  where mast.Acnt_Idno == intAcntIdno  select mast).FirstOrDefault();
                    if (objAcntMast != null)
                    { 
                        objAcntMast.Status = Status;
                        objAcntMast.Emp_Idno = empIdno;
                        objAcntMast.Date_Modified = System.DateTime.Now;
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

       
        public IList CheckItemExistInOtherMaster(Int32 AcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.LorryMasts
                        where obj.Prty_Idno == AcntIdno
                        select new
                        {
                            ItemGrp = obj.Prty_Idno
                        }
                        ).ToList()
                         .Union
                         (from obj2 in db.tblRcptGoodHeads
                          where obj2.Sender_Idno == AcntIdno || obj2.Recevr_Idno == AcntIdno
                          select new
                          {
                              ItemGrp = obj2.Sender_Idno
                          }
                          ).ToList()
                          .Union
                          (from obj3 in db.tblQuatationHeads
                           where obj3.Sender_Idno == AcntIdno
                           select new { ItemGrp = obj3.Sender_Idno }
                          ).ToList()
                           .Union
                          (from obj4 in db.TblGrHeads
                           where obj4.Sender_Idno == AcntIdno || obj4.Recivr_Idno == AcntIdno
                           select new { ItemGrp = obj4.Sender_Idno }
                          ).ToList()
                          .Union
                          (from obj5 in db.VchrDetls
                           where obj5.Acnt_Idno == AcntIdno
                           select new { ItemGrp = obj5.Acnt_Idno }
                          ).ToList()
                          .Union
                          (from obj6 in db.tblPBillHeads
                           where obj6.Prty_Idno == AcntIdno
                           select new { ItemGrp = obj6.Prty_Idno }
                          ).ToList()
                          .Union
                          (from obj7 in db.MatIssHeads
                           where obj7.Driver_Idno == AcntIdno
                           select new { ItemGrp = obj7.Driver_Idno }
                          ).ToList()
                         ;
            }
        }
        public Int64 CheckInvGen(Int64 Acntid)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblInvGenHeads
                        where mast.Sendr_Idno == Acntid
                        select mast).Count();
            }
        }

    }
}





