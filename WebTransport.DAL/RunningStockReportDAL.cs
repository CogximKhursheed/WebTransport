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
    public class RunningStockReportDAL
    {
        // Running stock report tyre fatch --- where item_From = 'LM'
        public IList SelectRunningStockReportTyre(Int32 LorryIdno, Int32 SerialIdno, Int32 PositionIdno, Int32 TypeIdno, string CompanyName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.Stckdetls
                           join objL in db.LorryMasts on obj.lorry_Idno equals objL.Lorry_Idno
                           join objP in db.tblPositionMasts on obj.Tyre_PostionIdno equals objP.Position_id
                           join objT in db.tblTyreCategories on obj.Type equals objT.TyreType_IdNo
                           where obj.Item_from=="LM"
                           orderby objL.Lorry_Idno
                           select new
                           {
                               SerialNo = obj.SerialNo,
                               LorryNo = objL.Lorry_No,
                               LorryIdno = objL.Lorry_Idno,
                               SerialIdno = obj.SerlDetl_id,
                               PositionIdno = objP.Position_id,
                               TypeIdno = objT.TyreType_IdNo,
                               Position = objP.Position_name,
                               type = objT.TyreType_Name,
                               CompanyName = obj.CompName,
                           }).ToList();

                if (TypeIdno > 0)
                {
                    lst = lst.Where(l => l.TypeIdno == TypeIdno).ToList();
                }
                if (PositionIdno > 0)
                {
                    lst = lst.Where(l => l.PositionIdno == PositionIdno).ToList();
                }
                if (LorryIdno > 0)
                {
                    lst = lst.Where(l => l.LorryIdno == LorryIdno).ToList();
                }
                if (SerialIdno > 0)
                {
                    lst = lst.Where(l => l.SerialIdno == SerialIdno).ToList();
                }
                if (string.IsNullOrEmpty(CompanyName) == false)
                {
                    lst = lst.Where(l => l.CompanyName.ToLower().Contains(CompanyName)).ToList();
                }
                return lst;
            }
        }
 
    }
}
