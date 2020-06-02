using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class FleetAcntLinkDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from tblAcntLink
        /// </summary>
        /// <returns></returns>
        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblFleetAcntLinks
                        orderby mast.AcntLink_Idno
                        select mast).ToList();
            }
        }


        public IList BindGeneral(Int64 IgrpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AM in db.AcntMasts
                           where AM.Acnt_Type == 1 && AM.ASubGrp_Idno == IgrpIdno
                           orderby AM.Acnt_Name ascending
                           select new
                           {
                               Acnt_Idno = AM.Acnt_Idno,
                               Acnt_Name = AM.Acnt_Name

                           }
             ).ToList();
                return lst;
            }
        }
        public IList BindCash(Int64 IgrpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AM in db.AcntMasts
                           where AM.Acnt_Type == 3 && AM.ASubGrp_Idno == IgrpIdno
                           orderby AM.Acnt_Name ascending
                           select new
                           {
                               Acnt_Idno = AM.Acnt_Idno,
                               Acnt_Name = AM.Acnt_Name

                           }
             ).ToList();
                return lst;
            }
        }
        #endregion

        #region InsertUpdateDelete Events...
        public Int64 InsertPurAccountHead(string strAccountHead, Int32 AcntIdno, Int32 CompId, Int32 Acnt_type)
        {
            Int64 intValue = 0;
            bool OpenType = true;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast objAcntMast = new AcntMast();
                objAcntMast.Acnt_Name = strAccountHead;
                objAcntMast.AGrp_Idno = AcntIdno;
                objAcntMast.Open_Bal = 0;
                objAcntMast.Bal_Type = Convert.ToInt32(OpenType);
                objAcntMast.Acnt_Type = Acnt_type;
                objAcntMast.Titl_Idno = 0;
                objAcntMast.INTERNAL = false;
                objAcntMast.ASubGrp_Idno = AcntIdno;
                objAcntMast.Status = true;
                objAcntMast.Comp_Idno = CompId;
                objAcntMast.Date_Added = System.DateTime.Now;
                objAcntMast.Date_Modified = System.DateTime.Now;
                if (IfIsExists(strAccountHead) == true)
                {
                    intValue = -1;
                }
                else
                {
                    db.AcntMasts.AddObject(objAcntMast);
                    db.SaveChanges();
                    intValue = objAcntMast.Acnt_Idno;
                }
                return intValue;
            }
        }

        public Int64 Update(Int64 VatPur, Int64 CSTPur, Int64 Vat, Int64 CST, Int64 Disc, Int64 Other, Int64 Cash, Int64 VATSale, Int64 CSTSale, Int64 TollAcc)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblFleetAcntLink objAcntLink = (from mast in db.tblFleetAcntLinks
                                                    select mast).FirstOrDefault();
                    if (objAcntLink != null)
                    {
                        objAcntLink.VPur_Idno = VatPur;
                        objAcntLink.CPur_Idno = CSTPur;
                        objAcntLink.Vat_Idno = Vat;
                        objAcntLink.CST_Idno = CST;
                        objAcntLink.Disc_Idno = Disc;
                        objAcntLink.Other_Idno = Other;
                        objAcntLink.Cash_Idno = Cash;
                        objAcntLink.VSale_Idno = VATSale;
                        objAcntLink.CSale_Idno = CSTSale;
                        objAcntLink.TollAcc_Idno = TollAcc;
                        objAcntLink.Date_modified = System.DateTime.Now;
                        {
                            db.SaveChanges();
                            intValue = objAcntLink.AcntLink_Idno;
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

        #endregion



        public bool IfIsExists(string strAccountHead)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast objAcntMast = new AcntMast();
                if (strAccountHead != "")
                {
                    objAcntMast = (from mast in db.AcntMasts
                                   where mast.Acnt_Name == strAccountHead
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

    }
}