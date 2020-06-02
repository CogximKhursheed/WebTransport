using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WebTransport.DAL
{
   public class MiscDAL
    {
        #region "Insert/Update/Delete"
        public Int64 InsertMisc(string Misc_Type, Int64 MiscType_Idno, string Misc_Name, bool MStatus,Int32 AcntIdno)
        {
            Int64 intMisc_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                try
                {
                    tblMiscMaster objMisc = new tblMiscMaster();
                    
                    objMisc.Tran_Idno = MiscType_Idno;
                    objMisc.Misc_Name = Misc_Name;
                    objMisc.Misc_Status = MStatus;
                    objMisc.Party_idno = AcntIdno;
                    objMisc.Date_Added = System.DateTime.Now;
                    db.tblMiscMasters.AddObject(objMisc);
                    db.SaveChanges();
                    intMisc_Idno = objMisc.Misc_Idno;
                }
                catch (Exception e)
                {
                    intMisc_Idno = 0;
                }
            return intMisc_Idno;
        }

        public Int64 UpdateMisc(Int64 Misc_Idno, string Misc_Type, Int64 Tran_idno, string Misc_Name, bool MStatus, Int32 AcntIdno)
        {
            Int64 intMisc_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                try
                {
                    tblMiscMaster objMisc = db.tblMiscMasters.Where(tbl => (tbl.Tran_Idno == Misc_Idno && tbl.Misc_Name == Misc_Name && tbl.Misc_Idno != Misc_Idno)).FirstOrDefault();

                    if (objMisc != null)
                    {
                        intMisc_Idno = -1;
                    }
                    else
                    {
                        tblMiscMaster objMisc1 = db.tblMiscMasters.Where(tbl => (tbl.Misc_Idno == Misc_Idno)).FirstOrDefault();
                        objMisc1.Tran_Idno = Tran_idno;
                        objMisc1.Misc_Name = Misc_Name;
                        objMisc1.Misc_Status = MStatus;
                        objMisc1.Party_idno = AcntIdno;
                        objMisc1.Date_Added = System.DateTime.Now;
                        db.SaveChanges();
                        intMisc_Idno = Misc_Idno;
                  
                    }

                    
                }
                catch (Exception e)
                {
                    intMisc_Idno = 0;
                }
            return intMisc_Idno;
        }

        public Int64 DeleteMisc(Int64 Misc_Idno)
        {
            clsAccountPosting objcls = new clsAccountPosting();
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblMiscMaster objM = db.tblMiscMasters.Where(tbl => tbl.Misc_Idno == Misc_Idno).FirstOrDefault();
                    if (objM != null)
                    {
                        db.tblMiscMasters.DeleteObject(objM);
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch (Exception e)
            {
                value = -1;
            }
            return value;
        }
       

        public Int32 UpdateStatus(int intItemIdno, bool Status)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblMiscMaster objMisc = (from mast in db.tblMiscMasters
                                            where mast.Misc_Idno == intItemIdno
                                            select mast).FirstOrDefault();
                    if (objMisc != null)
                    {
                        objMisc.Misc_Status = Status;
                        objMisc.Data_Modified = System.DateTime.Now;
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

        #region "Grid Event"   

        public tblMiscMaster Select(Int64 iMiscID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblMiscMasters.Where(tbl => (tbl.Misc_Idno == iMiscID)).FirstOrDefault();
            }
        }
        #endregion
       
        #region "Check for delete"
        public IList CheckItemExistInOtherMaster(Int32 MiscIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.tblGrRetailerHeads
                        where obj.Misc_Idno == MiscIdno
                        select new
                        {
                            MiscGrp = obj.Misc_Idno
                        }
                        ).ToList();
            }
        } 
        #endregion

        #region "Search"
        public IList SelectForSearch(Int64 intMiscTypeIdno, string strMiscName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from misc in db.tblMiscMasters
                           join tran in db.tblTranTypes on misc.Tran_Idno equals tran.TranType_Idno

                           select new
                           {
                               misc.Tran_Idno,
                               misc.Misc_Name,
                               misc.Misc_Status,
                               misc.Misc_Idno,
                               tran.Tran_Type
                           }
           ).ToList();
                           
                if (strMiscName != "")
                {
                    lst = (from I in lst where I.Misc_Name.ToLower().Contains(strMiscName.ToLower()) select I).ToList();
                }
                if (intMiscTypeIdno > 0)
                {
                    lst = (from I in lst where I.Tran_Idno == intMiscTypeIdno select I).ToList();
                }
                return lst;
            }
        }
        #endregion

        #region "Populate"..
        public IList SelectDetail(Int64 Misc_idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
             {
                var lst = (from misc in db.tblMiscMasters
                           join tran in db.tblTranTypes on misc.Tran_Idno equals tran.TranType_Idno
                           join Acnt in db.AcntMasts on misc.Party_idno equals Acnt.Acnt_Idno
                           where misc.Misc_Idno==Misc_idno

                           select new
                           {
                               misc.Tran_Idno,
                               misc.Misc_Name,
                               misc.Misc_Status,
                               misc.Misc_Idno,
                               tran.Tran_Type,
                               Acnt.Acnt_Name,
                               Acnt.Acnt_Idno,
                           }
                ).ToList();
                return lst;
            }
        }

        #endregion
        #region Bind..
        public List<AcntMast> BindParty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objAcntMast = new List<AcntMast>();

                objAcntMast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 2 && obj.INTERNAL == false && obj.Status == true
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objAcntMast;
            }
        }
        #endregion
    }
}
