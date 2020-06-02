using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class LorryMasterRepDAL
    {
        #region Select Events...

        /// <summary>
        /// Select all records from LorryMast
        /// </summary>
        /// <returns></returns>
        /// 

        public List<AcntMast> SelectPartyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intNarrIdno"></param>
        /// <param name="strOwnerName"></param>
        /// <returns></returns>


        public DataTable SelectForSearch(Int32 iLorryTypeIDNO, string iLorryNo, Int32 iPanNoIDNO, Int32 iPartyIdno, string PanNumber, DateTime dtFromDate, DateTime dtToDate, string con)
        {
            #region Old Process by linq !List
            //using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            //{
            //    var lst = (from mast in db.LorryMasts
            //               join Party in db.AcntMasts on mast.Prty_Idno equals Party.Acnt_Idno
            //               select new
            //               {
            //                   LorryIdno = mast.Lorry_Idno,
            //                   LorryNo = mast.Lorry_No,
            //                   Lorry_Type = mast.Lorry_Type,
            //                   LorryMake = mast.Lorry_Make,
            //                   ChasisNo = mast.Chassis_no,
            //                   EngineNo = mast.Eng_No,
            //                   PartyName = Party.Acnt_Name,
            //                   prty_Idno = mast.Prty_Idno,
            //                   PanNo = mast.Pan_No,
            //                   OwnerName = mast.Owner_Name,
            //                   Status = mast.Status,
            //                   LorryType = (mast.Lorry_Type == 0 ? "Own" : "Hire"),
            //               }).ToList();

            //    if (iLorryTypeIDNO >= 0)
            //    {
            //        lst = (from I in lst where I.Lorry_Type == iLorryTypeIDNO select I).ToList();
            //    }

            //    if (iLorryNo != "")
            //    {
            //        lst = (from I in lst where I.LorryNo.Contains(iLorryNo) select I).ToList();
            //    }

            //    if (iPanNoIDNO > 0)
            //    {
            //        lst = (from I in lst where I.PanNo == "" select I).ToList();
            //    }

            //    if (iPartyIdno > 0)
            //    {
            //        lst = (from I in lst where I.prty_Idno == iPartyIdno select I).ToList();
            //    }
            //    return lst;
            //}



            #endregion

            SqlParameter[] objSqlPara = new SqlParameter[8];
            objSqlPara[0] = new SqlParameter("@Action", "SelectRep");
            objSqlPara[1] = new SqlParameter("@LorryType_idno", iLorryTypeIDNO);
            objSqlPara[2] = new SqlParameter("@Lorry_no", iLorryNo);
            objSqlPara[3] = new SqlParameter("@PanNo_Idno", iPanNoIDNO);
            objSqlPara[4] = new SqlParameter("@Party_idno", iPartyIdno);
            objSqlPara[5] = new SqlParameter("@Pan_No", PanNumber);
            objSqlPara[6] = new SqlParameter("@From_Date", dtFromDate);
            objSqlPara[7] = new SqlParameter("@To_Date", dtToDate);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLorryRep", objSqlPara);
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

        public DataTable SelectLorrySummary(Int32 LorryId, Int32 YearIdno, string FromDate, string ToDate, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@LorryIdNo", LorryId);
            objSqlPara[1] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[2] = new SqlParameter("@FromDate", FromDate);
            objSqlPara[3] = new SqlParameter("@ToDate", ToDate);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spLorrySummaryReport", objSqlPara);
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

        /// <summary>
        /// Select one record from LorryMast by LorryIdno
        /// </summary>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>
        //public LorryMast SelectById(int intLorryIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        return (from mast in db.LorryMasts where mast.Lorry_Idno == intLorryIdno
        //                select mast).FirstOrDefault();
        //    }
        //}


        //public IList SelectAll()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        return (from mast in db.LorryMasts orderby mast.Owner_Name
        //                select mast).ToList();
        //    }
        //}
        /// <summary>
        /// To check record existence in LorryMast
        /// </summary>
        /// <param name="strOwnerrName"></param>
        /// <param name="intLorryIdno"></param>
        /// <returns></returns>
        //public bool IsExists(string strOwnerrName, Int64 intLorryIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        LorryMast objLorryMast = new LorryMast();
        //        if (intLorryIdno <= 0)
        //        {
        //            objLorryMast = (from mast in db.LorryMasts
        //                            where mast.Owner_Name == strOwnerrName
        //                            select mast).FirstOrDefault();
        //        }
        //        else if (intLorryIdno > 0)
        //        {
        //            objLorryMast = (from mast in db.LorryMasts
        //                            where mast.Owner_Name == strOwnerrName
        //                                  && mast.Lorry_Idno != intLorryIdno
        //                            select mast).FirstOrDefault();
        //        }
        //        if (objLorryMast != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}

        //#endregion

        //#region InsertUpdateDelete Events...

        ///// <summary>
        ///// Insert records in LorryMast
        ///// </summary>
        ///// <param name="strNarrName"></param>
        ///// <param name="strLorryMake"></param>
        ///// <param name="bStatus"></param>
        ///// <returns></returns>
        //public Int64 InsertLorryMast(string strLorryMake, string strOwnerName, Int32 LorryType, Int64 PartyName, string strChasisNo, string strEngineNo, string strLorryNo, string strPanNo, bool bStatus)
        //{
        //    Int64 intValue = 0;
        //    Int32 intCompIdno = 1;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            LorryMast objLorryMast = new LorryMast();

        //            objLorryMast.Lorry_Make = strLorryMake;
        //            objLorryMast.Owner_Name = strOwnerName;
        //            objLorryMast.Lorry_Type = LorryType;
        //            objLorryMast.Status = bStatus;
        //            objLorryMast.Comp_Idno = intCompIdno;

        //            objLorryMast.Prty_Idno = PartyName;
        //            objLorryMast.Chassis_no = strChasisNo;
        //            objLorryMast.Eng_No = strEngineNo;
        //            objLorryMast.Lorry_No = strLorryNo;
        //            objLorryMast.Pan_No = strPanNo;

        //            objLorryMast.Date_Added = System.DateTime.Now;
        //            if (IsExists(strOwnerName, 0) == true)
        //            {
        //                intValue = -1;
        //            }
        //            else
        //            {
        //                db.LorryMasts.AddObject(objLorryMast);
        //                db.SaveChanges();
        //                intValue = objLorryMast.Lorry_Idno;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ApplicationFunction.ErrorLog(ex.ToString());
        //    }
        //    return intValue;
        //}
        ///// <summary>
        ///// Update records in LorryMast
        ///// </summary>
        ///// <param name="strNarrName"></param>
        ///// <param name="strNarrDesc"></param>
        ///// <param name="bStatus"></param>
        ///// <param name="intNarrIdno"></param>
        ///// <returns></returns>
        //public Int64 UpdateLorryMast(string strLorryMake, string strOwnerName, Int32 LorryType, Int64 PartyName, string strChasisNo, string strEngineNo, string strLorryNo, string strPanNo, bool bStatus, Int64 intLorryIdno)
        //{
        //    Int64 intValue = 0;
        //    //Int32 intLorryIdno = 1;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            LorryMast objLorryMast = (from mast in db.LorryMasts
        //                                      where mast.Lorry_Idno == intLorryIdno
        //                                      select mast).FirstOrDefault();
        //            if (objLorryMast != null)
        //            {
        //                objLorryMast.Lorry_Make = strLorryMake;
        //                objLorryMast.Owner_Name = strOwnerName;
        //                objLorryMast.Lorry_Type = LorryType;
        //                objLorryMast.Status = bStatus;

        //                objLorryMast.Prty_Idno = PartyName;
        //                objLorryMast.Chassis_no = strChasisNo;
        //                objLorryMast.Eng_No = strEngineNo;
        //                objLorryMast.Lorry_No = strLorryNo;
        //                objLorryMast.Pan_No = strPanNo;

        //                objLorryMast.Date_Added = System.DateTime.Now;
        //                if (IsExists(strOwnerName, intLorryIdno) == true)
        //                {
        //                    intValue = -1;
        //                }
        //                else
        //                {
        //                    db.SaveChanges();
        //                    intValue = intLorryIdno;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ApplicationFunction.ErrorLog(ex.ToString());
        //    }
        //    return intValue;
        //}
        ///// <summary>
        ///// Delete record from LorryMast
        ///// </summary>
        ///// <param name="intLorryIdno"></param>
        ///// <returns></returns>
        //public int Delete(int intLorryIdno)
        //{
        //    int intValue = 0;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            LorryMast objLorryMast = (from mast in db.LorryMasts
        //                                      where mast.Lorry_Idno == intLorryIdno
        //                                      select mast).FirstOrDefault();
        //            if (objLorryMast != null)
        //            {
        //                db.LorryMasts.DeleteObject(objLorryMast);
        //                db.SaveChanges();
        //                intValue = 1;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
        //        {
        //            intValue = -1;
        //        }
        //    }
        //    return intValue;
        //}
        //public Int32 UpdateStatus(int intLorryIdno, bool Status)
        //{
        //    int value = 0;
        //    try
        //    {
        //        using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //        {
        //            LorryMast objLorryMast = (from mast in db.LorryMasts
        //                                      where mast.Lorry_Idno == intLorryIdno
        //                                      select mast).FirstOrDefault();
        //            if (objLorryMast != null)
        //            {

        //                objLorryMast.Status = Status;
        //                objLorryMast.Date_Modified = System.DateTime.Now;
        //                db.SaveChanges();
        //                value = 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return value;
        //}

        #endregion
    }
}
