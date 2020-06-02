using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Transactions;
using System.Web;

namespace WebTransport.DAL
{
    public class AdvBookGRRetDAL
    {
        #region "Functions..."
        public double SelectItemWeightWiseRate(Int64 ItemIdno, Int64 TocityIdno, Int32 FromcityIdno, DateTime Grdate, Decimal Weight, Int64 PartyId,Int64 TranType_Idno)
        {
            double ItemRate = 0.00;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                ItemRate = Convert.ToDouble((from RM in db.tblPartyRateRetailerMasts where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.Loc_Idno == FromcityIdno && RM.TranType_Idno == TranType_Idno && RM.Rate_Date <= Grdate && RM.Item_Weight >= Weight && RM.Party_Idno == PartyId orderby RM.Item_Weight ascending select RM.Item_Rate).FirstOrDefault());
                if (ItemRate > 0.00)
                    return ItemRate;
                else
                    ItemRate = Convert.ToDouble((from RM in db.tblRateMastRetailers where RM.Item_Idno == ItemIdno && RM.ToCity_Idno == TocityIdno && RM.FrmCity_Idno == FromcityIdno && RM.TranType_Idno == TranType_Idno && RM.Rate_Date <= Grdate && RM.Item_Weight >= Weight orderby RM.Item_Weight ascending select RM.Item_WghtRate).FirstOrDefault());
                return ItemRate;
            }
        }
        #endregion
    }
}
