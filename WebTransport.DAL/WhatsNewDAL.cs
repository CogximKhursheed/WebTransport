using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WebTransport.DAL
{
   public class WhatsNewDAL
    {
        public Int64 Insert(DateTime? date, Int64 FormID, string Details)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblWhatsNew objWhatsNew = new tblWhatsNew();
                    objWhatsNew.Date = date;
                    objWhatsNew.FormId = FormID;
                    objWhatsNew.Details = Details;

                        db.tblWhatsNews.AddObject(objWhatsNew);
                        db.SaveChanges();
                        intValue = objWhatsNew.ID;
                    
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        public IList SelectAll(DateTime? date, Int64 FormId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblWhatsNews
                           join form in db.tblFormMasts on mast.FormId equals form.Form_Idno
                           orderby mast.Date ascending
                           select new
                           {
                               Date = mast.Date,
                               FormId = mast.FormId,
                               FormName=form.Form_Name,
                               Details = mast.Details
                           }).ToList();
                if (date != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Date).Date <= Convert.ToDateTime(date).Date).ToList();
                }
                if (FormId > 0)
                {
                    lst = lst.Where(l => l.FormId == FormId).ToList();
                }
                return lst;
            }
        }

        public IList BindFormName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var frm = (from frmmast in db.tblFormMasts
                            where frmmast.Status == true
                            select new
                            {
                                FormName = frmmast.Form_Name,
                                FormIdno = frmmast.Form_Idno
                            }).ToList();
                return frm;
            }
        }
    }
}
