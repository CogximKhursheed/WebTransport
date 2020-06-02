using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class AcntLinkDAL
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
                return (from mast in db.tblAcntLinks
                        orderby mast.AcntLink_Idno
                        select mast).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intUOMIdno"></param>
        /// <param name="strUOMName"></param>
        /// <returns></returns>


        /// <summary>
        /// Select one record from tblAcntLink by AcntLinkIdno
        /// </summary>
        /// <param name="intAcntLinkIdno"></param>
        /// <returns></returns>
        public tblAcntLink SelectById(int Igrpidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblAcntLinks
                        where mast.Igrp_Idno == Igrpidno
                        select mast).FirstOrDefault();
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
        public IList BindGeneralandPetrolPump(Int64 IgrpIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from AM in db.AcntMasts
                           where AM.Acnt_Type == 10
                           orderby AM.Acnt_Name ascending
                           select new
                           {
                               Acnt_Idno = AM.Acnt_Idno,
                               Acnt_Name = AM.Acnt_Name
                           }
             ).ToList();
                return lst;
            }
            //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            //{
            //    var lst = (from AM in db.AcntMasts
            //               where (AM.Acnt_Type == 1 && AM.ASubGrp_Idno == IgrpIdno) || (AM.Acnt_Type==10)
            //               orderby AM.Acnt_Name ascending
            //               select new
            //               {
            //                   Acnt_Idno = AM.Acnt_Idno,
            //                   Acnt_Name = AM.Acnt_Name

            //               }
            // ).ToList();
            //    return lst;
            //}
        }



        /// <summary>
        /// To check record existence in tblAcntLink
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="intUOMIdno"></param>
        /// <returns></returns>


        #endregion

        #region InsertUpdateDelete Events...

        /// <summary>
        /// Insert records in tblAcntLink
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        /// 

        public Int64 InsertPurAccountHead(string strAccountHead, Int32 AcntIdno, Int32 CompId, Int32 Acnt_type, Int32 empIdno)
        {
            Int64 intValue = 0;
            bool OpenType = true;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast objAcntMast = new AcntMast();
                objAcntMast.Acnt_Name = strAccountHead;
                objAcntMast.AGrp_Idno = AcntIdno;
                objAcntMast.Emp_Idno = empIdno;
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

        public List<tblIGrpMast> SelectGroupTypeForItemGrp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblIGrpMast> lst = null;
                lst = (from LM in db.tblIGrpMasts orderby LM.IGrp_Name ascending select LM).ToList();
                return lst;
            }
        }
        public Int64 Insert(Int64 Cmmn, Int64 OthrAmnt, Int64 GrpTyp, Int64 intSTAcntIdno, Int64 IntTdsAcntIdno, Int64 intDebitIdno, Int64 intTDSAmnt, Int32 empIdno, Int64 intSwachhBha, Int64 intKrishiKalyan, Int64 intDiesel, Int64 intSGST, Int64 intCGST, Int64 intIGST)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblAcntLink objAcntLink = new tblAcntLink();
                    objAcntLink.Commsn_Idno = Cmmn;
                    objAcntLink.Emp_Idno = empIdno;
                    objAcntLink.SAcnt_Idno = intSTAcntIdno;
                    objAcntLink.OthrAc_Idno = OthrAmnt;
                    objAcntLink.TDS_Idno = IntTdsAcntIdno;
                    objAcntLink.Igrp_Idno = GrpTyp;

                    objAcntLink.Comp_Idno = intCompIdno;
                    objAcntLink.Date_Added = System.DateTime.Now;
                    objAcntLink.Debit_Idno = intDebitIdno;
                    objAcntLink.TDSAmnt_Idno = intTDSAmnt;
                    objAcntLink.SwachBharat_Idno = intSwachhBha;
                    objAcntLink.KrishiKalyan_Idno= intKrishiKalyan;
                    objAcntLink.DieselAcc_Idno = intDiesel;
                    objAcntLink.Sgst_Idno = intSGST;
                    objAcntLink.Cgst_Idno = intCGST;
                    objAcntLink.Igst_Idno = intIGST;
                    {
                        db.tblAcntLinks.AddObject(objAcntLink);
                        db.SaveChanges();
                        intValue = objAcntLink.AcntLink_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }
        // public Int64 InsertAccount(string strAcntHead, Int64 intAcntLinkidno, Int64 intCompId)
        // {
        //     Int64 intValue = 0;
        ////     Int32 intCompIdno = 1;
        //     try
        //     {
        //       using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //         {
        //             tblAcntLink objAcntLink = new tblAcntLink();
        //             objAcntLink. = Cmmn;

        //             objAcntLink.OthrAc_Idno = OthrAmnt;
        //             objAcntLink.Igrp_Idno = GrpTyp;

        //             objAcntLink.Comp_Idno = intCompIdno;
        //             objAcntLink.Date_Added = System.DateTime.Now;

        //             {
        //                 db.tblAcntLinks.AddObject(objAcntLink);
        //                 db.SaveChanges();
        //                 intValue = objAcntLink.AcntLink_Idno;
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         //ApplicationFunction.ErrorLog(ex.ToString());
        //     }
        //     return intValue;
        // }

        /// <summary>
        /// Update records in tblAcntLink
        /// </summary>
        /// <param name="strUOMName"></param>
        /// <param name="strUOMDesc"></param>
        /// <param name="bStatus"></param>
        /// <param name="intAcntLinkIdno"></param>
        /// <returns></returns>
        public Int64 Update(Int64 Cmmn, Int64 OthrAmnt, Int64 GrpTyp, Int64 intSTAcntIdno, Int64 IntTdsAcntIdno, Int64 intDebitIdno, Int64 intTDSAmnt, Int32 empIdno, Int64 intSwachhBha, Int64 intKrishiKalyan, Int64 intDiesel, Int64 intSGST, Int64 intCGST, Int64 intIGST)
        {
            Int64 intValue = 0;
            Int32 intCompIdno = 1;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblAcntLink objAcntLink = (from mast in db.tblAcntLinks
                                               where mast.Igrp_Idno == GrpTyp
                                               select mast).FirstOrDefault();
                    if (objAcntLink != null)
                    { 
                        objAcntLink.Commsn_Idno = Cmmn;
                        objAcntLink.SAcnt_Idno = intSTAcntIdno;
                        objAcntLink.OthrAc_Idno = OthrAmnt;
                        objAcntLink.Emp_Idno = empIdno;
                        objAcntLink.TDS_Idno = IntTdsAcntIdno;
                        objAcntLink.Igrp_Idno = GrpTyp;
                        objAcntLink.Comp_Idno = intCompIdno;
                        objAcntLink.Date_modified = System.DateTime.Now;
                        objAcntLink.Debit_Idno = intDebitIdno;
                        objAcntLink.TDSAmnt_Idno = intTDSAmnt;
                        objAcntLink.SwachBharat_Idno = intSwachhBha;
                        objAcntLink.KrishiKalyan_Idno = intKrishiKalyan;
                        objAcntLink.DieselAcc_Idno = intDiesel;
                        objAcntLink.Sgst_Idno = intSGST;
                        objAcntLink.Cgst_Idno = intCGST;
                        objAcntLink.Igst_Idno = intIGST;
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

        /// <summary>
        /// Delete record from tblAcntLink
        /// </summary>
        /// <param name="intAcntLinkIdno"></param>
        /// <returns></returns>
        public int Delete(int Igrpidno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblAcntLink objAcntLink = (from mast in db.tblAcntLinks
                                               where mast.AcntLink_Idno == Igrpidno
                                               select mast).FirstOrDefault();
                    if (objAcntLink != null)
                    {
                        db.tblAcntLinks.DeleteObject(objAcntLink);
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

        public Int32 UpdateStatus(int Igrpidno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblAcntLink objAcntLink = (from mast in db.tblAcntLinks
                                               where mast.AcntLink_Idno == Igrpidno
                                               select mast).FirstOrDefault();
                    if (objAcntLink != null)
                    {


                        objAcntLink.Date_modified = System.DateTime.Now;
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

        public bool IsExists(Int32 igrpidno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblAcntLink objAcntLink = new tblAcntLink();
                if (igrpidno > 0)
                {
                    objAcntLink = (from mast in db.tblAcntLinks
                                   where mast.Igrp_Idno == igrpidno
                                   select mast).FirstOrDefault();
                }
                if (objAcntLink != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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