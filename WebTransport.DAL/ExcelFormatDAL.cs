using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WebTransport;

namespace WebTransport.DAL
{
    public class ExcelFormatDAL
    {
        public IList SelectForSearch()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lsts = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                
                    var lst = (from CT in db.ExlFormats
                               select new
                               {
                                   CT.Menu_Form,
                                   CT.Status,
                                   CT.Form_Name,
                                   CT.ExlFormat_Idno,
                                   CT.Exl_Name,
                                   CT.TYPE,

                               }).ToList();

                    if (lsts.Menu_FleetMgmt == false)
                    {
                        lst = lst.Where(l => l.TYPE != "FL").ToList();
                    }
                return lst;
            }

        }

        public IList GetExcel(Int64 intLocIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CT in db.ExlFormats
                           where CT.ExlFormat_Idno == intLocIdno
                           orderby CT.Menu_Form
                           select new
                           {
                              CT.Menu_Form,
                              CT.Form_Name,
                              CT.Exl_Name,
                              CT.ExlFormat_Idno,
                              
                              
                           }).Distinct().ToList();

                return lst;
            }

        }
    }
}
