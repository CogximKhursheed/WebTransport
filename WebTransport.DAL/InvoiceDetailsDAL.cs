using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using WebTransport;

namespace WebTransport.DAL
{
    public class InvoiceDetailsDAL
    {
        public DataSet SelectRep(Int32 YearIdno,DateTime? dtFromDate, DateTime? dtToDate, String InvNo, Int64 iFrmCityIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[1] = new SqlParameter("@DateFrom", dtFromDate);
            objSqlPara[2] = new SqlParameter("@DateTo", dtToDate);
            objSqlPara[3] = new SqlParameter("@InvoiceNo", InvNo);
            objSqlPara[4] = new SqlParameter("@CityId", iFrmCityIdno);
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvoiceDetailsReport", objSqlPara);
        }

        public DataSet Report(Int64 YearIdno,String InvNo, Int64 iFrmCityIdno, string con)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "PrintInvoiceReport");
            objSqlPara[1] = new SqlParameter("@Year_Idno", YearIdno);
            objSqlPara[2] = new SqlParameter("@InvoiceNo", InvNo);
            objSqlPara[3] = new SqlParameter("@CityId", iFrmCityIdno);
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
        }
    public int Delete(Int64 HeadId, Int64 UserIdno, string con)
        {
            int value = 0; clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblInvGenHead CH = db.tblInvGenHeads.Where(h => h.Inv_Idno == HeadId).FirstOrDefault();
                List<tblInvGenDetl> CD = db.tblInvGenDetls.Where(d => d.InvGenHead_Idno == HeadId).ToList();
                if (CD != null && CH != null)
                {
                    var lst = (from AI in db.tblAmntRecvInv_Detl where AI.Inv_Idno == HeadId select AI).ToList();

                    if (lst != null && lst.Count > 0)
                    {
                        return -1;
                    }
                    else
                    {
                        if (CH != null)
                        {
                            SqlParameter[] objSqlPara = new SqlParameter[3];
                            objSqlPara[0] = new SqlParameter("@Action", "DeleteInvoiceDetails");
                            objSqlPara[1] = new SqlParameter("@Idno", HeadId);
                            objSqlPara[2] = new SqlParameter("@UserIdno", UserIdno);
                            Int32 del = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "spDeleteFunctionality", objSqlPara);
                            foreach (var d in CD)
                            {
                                db.tblInvGenDetls.DeleteObject(d);
                                db.SaveChanges();
                            }
                            db.tblInvGenHeads.DeleteObject(CH);
                            db.SaveChanges();
                            Int64 intValue = objclsAccountPosting.DeleteAccountPosting(HeadId, "IB");
                            db.SaveChanges();
                            if (intValue > 0)
                            {
                                value = 1;
                            }
                        }
                    }
                }

            }
            return value;
        }
    }
}
