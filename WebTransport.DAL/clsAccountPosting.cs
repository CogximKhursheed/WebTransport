using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data.Common;
using System.Collections;

namespace WebTransport.DAL
{
    public class clsAccountPosting
    {
        #region Insert records...

        /// <summary>
        /// Insert records in VchrHead table
        /// </summary>
        public Int64 InsertInVchrHead(DateTime VchrDate, byte VchrType, Int64 VchrMode, string VchrNarr, bool VchrHidn, byte VchrSusp, string
            VchrFrm, Int64 AcntIdno, Int16 Printed, Int64 SbillNo, DateTime? SbillDate, Int64 VchrNo, double DcnNo, int YearIdno, int CompIdno, int UserIdno
            )
        {
            Int64 intValue = 0;
            VchrHead objVchrHead = new VchrHead();
            objVchrHead.Vchr_Date = VchrDate;
            objVchrHead.Vchr_Type = VchrType;
            objVchrHead.Vchr_Mode = VchrMode;
            objVchrHead.Vchr_Narr = VchrNarr;
            objVchrHead.Vchr_Hidn = VchrHidn;
            objVchrHead.Vchr_Susp = VchrSusp;
            objVchrHead.Vchr_Frm = VchrFrm;
            objVchrHead.Acnt_Idno = AcntIdno;
            objVchrHead.Printed = Printed;
            objVchrHead.Sbill_No = 0;
            objVchrHead.Sbill_Date = null;
            if (VchrNo > 0)
            {
                objVchrHead.Vchr_No = VchrNo;
            }
            else
            {
                objVchrHead.Vchr_No = this.GetMaxVchrNo(VchrType, CompIdno, YearIdno);
            }
            objVchrHead.Dcn_No = DcnNo;
            objVchrHead.Year_Idno = YearIdno;
            objVchrHead.Comp_Idno = CompIdno;
            objVchrHead.User_Idno = UserIdno;
            objVchrHead.User_Type = 0;
            objVchrHead.VchrFor_Idno = 0;
            objVchrHead.VchrManual_No = "";
            objVchrHead.Date_Modified = SbillDate;
            objVchrHead.Date_Added = VchrDate;// ApplicationFunction.GetIndianDateTime().Date;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    db.VchrHeads.AddObject(objVchrHead);
                    db.SaveChanges();
                    intValue = objVchrHead.Vchr_Idno;
                }
                catch (Exception ex)
                {
                    intValue = 0;
                    //  ApplicationFunction.ErrorLog(ex.ToString());
                }
            }
            return intValue;
        }

        /// <summary>
        /// Insert records in VchrDetl table
        /// </summary>
        public Int64 InsertInVchrDetl(Int64 VchrIdno, Int64 AcntIdno, string NarrText, double AcntAmnt, byte AmntType, byte InstType, string InstNo, bool DetlHidn,
           DateTime? BankDate, string CustBank, int CompIdno)
        {
            Int64 intValue = 0;
            VchrDetl objVchrDetl = new VchrDetl();
            objVchrDetl.Vchr_Idno = VchrIdno;
            objVchrDetl.Acnt_Idno = AcntIdno;
            objVchrDetl.Narr_Text = NarrText;
            objVchrDetl.Acnt_Amnt = AcntAmnt;
            objVchrDetl.Amnt_Type = AmntType;
            objVchrDetl.Inst_Type = InstType;
            objVchrDetl.Inst_No = InstNo;
            objVchrDetl.Detl_Hidn = DetlHidn;
            objVchrDetl.Bank_Date = BankDate;
            objVchrDetl.Cust_Bank = CustBank;
            objVchrDetl.Comp_Idno = CompIdno;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    db.VchrDetls.AddObject(objVchrDetl);
                    db.SaveChanges();
                    intValue = Convert.ToInt64(objVchrDetl.Vchr_Idno);
                }
                catch (Exception ex)
                {
                    intValue = 0;
                    // using WebTransport.Classes. ApplicationFunction.ErrorLog(ex.ToString());
                }
            }
            return intValue;
        }

        /// <summary>
        /// Insert record in VchrIdDetl table
        /// </summary>
        /// <param name="VchrIdno"></param>
        /// <param name="DocIdno"></param>
        /// <param name="DocType"></param>
        /// <returns></returns>
        public Int64 InsertInVchrIdDetl(Int64 VchrIdno, Int64 DocIdno, string DocType)
        {
            Int64 intValue = 0;
            VchrIdDetl objVchrIdDetl = new VchrIdDetl();
            objVchrIdDetl.Vchr_Idno = VchrIdno;
            objVchrIdDetl.Doc_Idno = DocIdno;
            objVchrIdDetl.Doc_Type = DocType;
            objVchrIdDetl.Comp_Idno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    db.VchrIdDetls.AddObject(objVchrIdDetl);
                    db.SaveChanges();
                    intValue = Convert.ToInt64(objVchrIdDetl.Vchr_Idno);
                }
                catch (Exception ex)
                {
                    intValue = 0;
                    // ApplicationFunction.ErrorLog(ex.ToString());
                }
            }
            return intValue;
        }

        #endregion

        #region Select records...

        /// <summary>
        /// Get Max VchrNo from VchrHead table
        /// </summary>
        /// <param name="VchrType"></param>
        /// <param name="CompIdno"></param>
        /// <param name="YearIdno"></param>
        /// <returns></returns>
        public Int64 GetMaxVchrNo(byte VchrType, int CompIdno, int YearIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 intVchrNo = 0;
                intVchrNo = Convert.ToInt64((from obj in db.VchrHeads
                                             where obj.Vchr_Type == VchrType
                                             && obj.Comp_Idno == CompIdno
                                             && obj.Year_Idno == YearIdno
                                             select obj.Vchr_No).Max());
                return (intVchrNo + 1);
            }

        }

        #endregion

        #region Delete records...

        /// <summary>
        /// Delete posted records from VchrHead, VchrDetl and VchrIdDetl tables
        /// </summary>
        /// <param name="intDocIdno"></param>
        /// <param name="strDocType"></param>
        /// <returns></returns>
        public Int64 DeleteAccountPosting(Int64 intDocIdno, string strDocType)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var lst = (from obj in db.VchrIdDetls
                               where obj.Doc_Idno == intDocIdno
                               && obj.Doc_Type == strDocType
                               select obj.Vchr_Idno).ToList();

                    if (lst.Count > 0)
                    {
                        intValue = 1;
                        List<VchrHead> objVchrHead = (from obj in db.VchrHeads where lst.Contains(obj.Vchr_Idno) select obj).ToList();
                        if (objVchrHead != null)
                        {
                            if (objVchrHead.Count > 0)
                            {
                                intValue = 0;
                                foreach (VchrHead obj in objVchrHead)
                                {
                                    db.VchrHeads.DeleteObject(obj);
                                }
                                db.SaveChanges();
                                intValue = 1;
                            }
                        }
                        if (intValue > 0)
                        {
                            intValue = 0;
                            List<VchrDetl> objVchrDetl = (from obj in db.VchrDetls where lst.Contains(obj.Vchr_Idno) select obj).ToList();
                            if (objVchrDetl != null)
                            {
                                if (objVchrDetl.Count > 0)
                                {
                                    intValue = 0;
                                    foreach (VchrDetl obj in objVchrDetl)
                                    {
                                        db.VchrDetls.DeleteObject(obj);
                                    }
                                    db.SaveChanges();
                                    intValue = 1;
                                }
                            }
                            if (intValue > 0)
                            {
                                intValue = 0;
                                List<VchrIdDetl> objVchrIdDetl = (from obj in db.VchrIdDetls where lst.Contains(obj.Vchr_Idno) select obj).ToList();
                                if (objVchrIdDetl != null)
                                {
                                    if (objVchrIdDetl.Count > 0)
                                    {
                                        intValue = 0;
                                        foreach (VchrIdDetl obj in objVchrIdDetl)
                                        {
                                            db.VchrIdDetls.DeleteObject(obj);
                                        }
                                        db.SaveChanges();
                                        intValue = 1;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        intValue = 1;
                    }
                }
            }
            catch (Exception Ex)
            {
                intValue = 0;
                //  ApplicationFunction.ErrorLog(Ex.ToString());
            }
            return intValue;
        }

        #endregion

        #region Get Others...

        public DataTable GetTableWithGroup(string strGroupByColumn, string strAggregateColumn, DataTable dtSourceDataTable)
        {
            DataTable dtCloned = dtSourceDataTable.Clone();
            dtCloned.Columns[strAggregateColumn].DataType = typeof(double);

            foreach (DataRow row in dtSourceDataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }
            DataView dv = new DataView(dtCloned);

            //getting distinct values for group column
            DataTable dtGroup = dv.ToTable(true, new string[] { strGroupByColumn });

            //adding column for the row count
            dtGroup.Columns.Add("SumColumn", typeof(double));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["SumColumn"] = dtCloned.Compute("Sum(" + strAggregateColumn + ")", strGroupByColumn + " = '" + dr[strGroupByColumn] + "'");
            }

            //returning grouped/counted result
            return dtGroup;
        }

        public tblAcntLink GetAccountLinkData()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblAcntLink objAcntLink = new tblAcntLink();
                objAcntLink = (from obj in db.tblAcntLinks
                               //  where obj.IGrp_Idno == intIGrpIdno
                               select obj).FirstOrDefault();
                return objAcntLink;
            }
        }
        //public IGrpMast GetGroupNameWithGroupId(Int32 intIGrpIdno)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities(clsMultipleDB.strDynamicConString()))
        //    {
        //        IGrpMast objIGrpMast = new IGrpMast();
        //        objIGrpMast = (from obj in db.IGrpMasts where obj.IGrp_Idno == intIGrpIdno select obj).FirstOrDefault();
        //        return objIGrpMast;
        //    }
        //}

        public Int64 GetRoundOffId()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 intRoundOffId = 0;
                intRoundOffId = (from obj in db.AcntMasts
                                 where obj.Acnt_Name == "Round Off A/C" //&& obj.InterNal == true 
                                 select obj.Acnt_Idno).FirstOrDefault();
                return intRoundOffId;
            }
        }

        #endregion

        public DataSet AccPosting(string conString, string Action, Int64 IdFrom, Int64 IdTo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@From", IdFrom);
            objSqlPara[2] = new SqlParameter("@To", IdTo);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spDemoAccPosting", objSqlPara);
            //if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
            //{
            //    dt = objDataSet.Tables[0];
            //}
            return objDataSet;
        }

    }
}