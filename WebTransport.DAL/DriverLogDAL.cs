using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class DriverLogDAL
    {
        public DataTable SelectReport(string Con, string DtFrom, string DtTo, Int64 LorryIdno)
        {
            DataTable Dt = new DataTable();
            DataSet Ds = SqlHelper.ExecuteDataset(Con, CommandType.Text, "exec spDriverLogRpt @Action='SelectRpt',@DateFrom='" + DtFrom + "',@Dateto='" + DtTo + "',@LorryIdno=" + LorryIdno + "");
            if (Ds != null && Ds.Tables.Count>0)
            {
                Dt = Ds.Tables[0];
            }
            return Dt;
        }
    }
}
