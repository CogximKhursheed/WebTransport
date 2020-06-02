using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

namespace WebTransport.DAL
{
    public class LostAlignRepDAL
    {
        #region Report...

        
        public bool IsExists(string LorryNo, DateTime? AlignDate)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AlignDate_SMS obj = new AlignDate_SMS();
              
                    obj = (from mast in db.AlignDate_SMS
                                      where mast.LorryNo == LorryNo && mast.AlignDate==AlignDate
                                      select mast).FirstOrDefault();
             
                
                if (obj != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public Int64 Update(DateTime? AlignDate, string LorryNo)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    AlignDate_SMS objSMSAlign = (from mast in db.AlignDate_SMS
                                                 where mast.AlignDate == AlignDate && mast.LorryNo == LorryNo 
                                                       select mast).FirstOrDefault();
                    if (objSMSAlign != null)
                    {
                        objSMSAlign.Status = true;
                        db.SaveChanges();
                        intValue = objSMSAlign.ID;
                    }
                    

                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }
        public IList SelectData()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var GrDet = (from GH in db.AlignDate_SMS
                             where GH.Status == false
                             select new
                             {
                                 MSG = GH.Message,
                                 AlignDate = GH.AlignDate,
                                 LorryNo=GH.LorryNo
                             }).ToList();
                return GrDet;
            }
        }

        public IList SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Comp = (from mast in db.tblCompMasts
                             select new
                             {
                                 UserName = mast.SMS_UserName,
                                 Password = mast.SMS_Password,
                                 SenderID=mast.SMS_SenderID,
                                 ProfileID=mast.SMS_ProfileID,
                                 AuthType=mast.SMS_AuthType,
                                 AuthKey=mast.SMS_AuthKey
                             }).ToList();
                return Comp;
            }
        }
        public IList SelectForSearch(DateTime? dtfrom, DateTime? dtto,Int64 LorryIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.MatIssDetls
                           join ig in db.MatIssHeads on mast.MatIssHead_Idno equals ig.MatIss_Idno
                           join IT in db.LorryMasts on ig.Truck_Idno equals IT.Lorry_Idno
                           join IM in db.tblItemMastPurs on mast.Iteam_Idno equals IM.Item_Idno
                           join AM in db.AcntMasts on IT.Driver_Idno equals AM.Acnt_Idno
                            where IM.ItemType ==1 && mast.AlignDate < DateTime.Today
                           //join IM in db.tblItemTypeMasts on mast.ItemType equals IM.ItemTpye_Idno
                           //join u in db.UOMMasts on mast.Unit_Idno equals u.UOM_Idno

                           select new
                           {
                               Date=ig.MatIss_Date,
                               LorryIdno=ig.Truck_Idno,
                               LorryNo=IT.Lorry_No,
                               PhoneNo=AM.Drv_MobNo1,
                               ItemName=IM.Item_Name,
                               SerialNo=mast.Serial_Number,
                               PrevAlignDate=mast.Prev_AlignDate,
                               AlignDate=mast.AlignDate
                           }).ToList();

                if (dtfrom != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.AlignDate).Date >= Convert.ToDateTime(dtfrom).Date select l).ToList();
                }
                if (dtto != null)
                {
                    lst = (from l in lst where Convert.ToDateTime(l.AlignDate).Date <= Convert.ToDateTime(dtto).Date select l).ToList();
                }
                if (LorryIdno > 0)
                {
                    lst = (from I in lst where I.LorryIdno == LorryIdno select I).ToList();
                }
                return lst;
            }
        }

        #endregion
    }

}
