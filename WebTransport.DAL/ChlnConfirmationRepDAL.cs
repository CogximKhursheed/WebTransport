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
    public class ChlnConfirmationRepDAL
    {
        #region ChlnConfirmation Report...


        public List<LorryMast> BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<LorryMast> lst = null;
                lst = (from LM in db.LorryMasts orderby LM.Lorry_No ascending select LM).ToList();
                return lst;
            }
        }

        public List<tblCityMaster> BindFromCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }

        public DataTable SelectRep(DateTime? dtFromDate, DateTime? dtToDate, Int64 iFrmCityIdno, Int64 iTruckIdno, Int32 iChlnType,Int64 UserIdno,string con)
        {
            #region Old Report by linq
            //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            //{
            //    var lst = (from CH in db.tblChlnBookHeads
            //               join CD in db.tblChlnBookDetls on CH.Chln_Idno equals CD.ChlnBookHead_Idno
            //               join LM in db.LorryMasts
            //               on CH.Truck_Idno equals LM.Lorry_Idno
            //               join GH in db.TblGrHeads on CD.GR_Idno equals GH.GR_Idno
            //               join AM in db.AcntMasts on GH.Sender_Idno equals AM.Acnt_Idno
            //               join AM1 in db.AcntMasts on GH.Recivr_Idno equals AM1.Acnt_Idno
            //               join CM in db.tblCityMasters on GH.To_City equals CM.City_Idno
            //               join CM1 in db.tblCityMasters on CH.BaseCity_Idno equals CM1.City_Idno
            //               select new
            //               {
            //                   CH.BaseCity_Idno,
            //                   CH.Truck_Idno,
            //                   CH.Chln_Idno,
            //                   CH.Chln_No,
            //                   CH.Chln_Date,
            //                   LM.Lorry_No,
            //                   GH.Gr_No,
            //                   GH.Gr_Date,
            //                   Sender_name = AM.Acnt_Name,
            //                   Reciver_Name = AM1.Acnt_Name,
            //                   Qty = (from obj in db.TblGrDetls where obj.GrHead_Idno == CD.GR_Idno select obj.Qty).Sum(),
            //                  To_City= CM.City_Name,
            //                From_City=  CM1.City_Name,
            //                   Amount = GH.Net_Amnt,
            //                   CD.Delivered
            //               }).ToList();

            //    if (dtFromDate!=null)
            //    {
            //        lst=(from l in lst where l.Chln_Date>=dtFromDate select l).ToList();
            //    }
            //    if (dtToDate != null)
            //    {
            //        lst = (from l in lst where l.Chln_Date <= dtToDate select l).ToList();
            //    }
            //    if (iFrmCityIdno > 0)
            //    {
            //        lst = (from l in lst where l.BaseCity_Idno == iFrmCityIdno select l).ToList();
            //    }
            //    if (iTruckIdno > 0)
            //    {
            //        lst = (from l in lst where l.Truck_Idno == iTruckIdno select l).ToList();
            //    }
            //    if (iChlnType >1)
            //    {
            //        lst = (from l in lst where l.Delivered == false select l).ToList();
            //    }
            //    return lst;
            //}
            #endregion
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", "SelectRep");
            objSqlPara[1] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[2] = new SqlParameter("@To_Date", dtToDate);
            objSqlPara[3] = new SqlParameter("@FrmCityIdno", iFrmCityIdno);
            objSqlPara[4] = new SqlParameter("@Truck_idno", iTruckIdno);
            objSqlPara[5] = new SqlParameter("@Chln_type", iChlnType);
            objSqlPara[6] = new SqlParameter("@UserIdno", UserIdno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spChlnConfmRep", objSqlPara);
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

        #endregion
    }

}
