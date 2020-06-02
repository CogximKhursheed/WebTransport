using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class RegisterDAL
    {

        public List<tblStateMaster> BindState()
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                List<tblStateMaster> objStateMasterList = new List<tblStateMaster>();

                objStateMasterList = (from obj in db.tblStateMasters
                                      where obj.Status == true
                                      orderby obj.State_Name
                                      select obj).ToList();

                return objStateMasterList;
            }
        }

        public List<tblCityMaster> BindCity(Int64 intStateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                List<tblCityMaster> objCityMasterList = new List<tblCityMaster>();

                objCityMasterList = (from obj in db.tblCityMasters
                                     where obj.Status == true && obj.State_Idno == intStateIdno
                                     orderby obj.City_Name
                                     select obj).ToList();

                return objCityMasterList;
            }
        }

        public Int64 Insert(string CompName, string OwnerName, string LastName, string EmailId, string OwnPhoneNum, string OwnMobileNum, string Ipaddress, Boolean Status)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities())
                {
                    if (IsExists(EmailId) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        tblNewReg objtblRegister = new tblNewReg();
                        objtblRegister.Comp_Name = CompName;
                        objtblRegister.Name = OwnerName;
                        objtblRegister.LastName = LastName;
                        //objtblRegister.NewReg_Addr2 = Address2;
                        //objtblRegister.City_Idno = CityIdno;
                        //objtblRegister.State_Idno = StateIdno;
                        //objtblRegister.Pin_No = PinNo;
                        objtblRegister.Email_Id = EmailId;

                        //objtblRegister.Phone_No = OwnPhoneNum;
                        objtblRegister.Mobile_No = OwnMobileNum;

                        objtblRegister.Date_Addedd = System.DateTime.Now;
                        objtblRegister.Ipaddress = Ipaddress;
                        objtblRegister.Status = Status;

                        db.AddTotblNewRegs(objtblRegister);
                        db.SaveChanges();
                        intValue = objtblRegister.NewReg_Idno;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return intValue;
        }


        public bool IsExists(string EmailId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {

                tblLoginEmail objtblRegister = new tblLoginEmail();
                objtblRegister = (from mast in db.tblLoginEmails
                                    where  mast.LoginEmail == EmailId
                                    select mast).FirstOrDefault();

                if (objtblRegister != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public tblEmailTemplate SelectUserMailTemplates(string Title)
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                var lst = (from obj in db.tblEmailTemplates where obj.Status == true && obj.Title == Title select obj).FirstOrDefault();
                return lst;
            }
        }

        public tblCompMast SelectSmsDetails()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCompMast Compmast = (from mast in db.tblCompMasts
                                        orderby mast.Comp_Name
                                        select mast).FirstOrDefault();

                return Compmast;
            }
        }
    }
}
