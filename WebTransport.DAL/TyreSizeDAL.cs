using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
     public class TyreSizeDAL
    {

        public Int64 Save(string size)
        {
            int Value = 0;
            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                    {
                        TyreSizeMaster tyreSize = db.TyreSizeMasters.Where(c => c.TyreSize == size).FirstOrDefault();
                        if (tyreSize == null)
                        {
                            TyreSizeMaster tbl = new TyreSizeMaster();
                            tbl.TyreSize = size;
                            tbl.Date_Added = System.DateTime.Now;
                            db.TyreSizeMasters.AddObject(tbl);
                            db.SaveChanges();
                            Value = 1;
                            Tran.Complete();
                        }
                        else
                        {
                            Value = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Value = 0;
                }
            }
            return Value;
        }
        public Int64 Update(Int64 TyresizeIdno, string Size)
        {
            int Value = 0;
            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                    {
                        TyreSizeMaster tyreSize = db.TyreSizeMasters.Where(c => c.TyreSize == Size && c.TyreSize_Idno != TyresizeIdno).FirstOrDefault();
                        if (tyreSize == null)
                        {
                            TyreSizeMaster tbl = db.TyreSizeMasters.Where(c => c.TyreSize_Idno == TyresizeIdno).FirstOrDefault();
                            tbl.TyreSize = Size;
                            tbl.Date_Modified = System.DateTime.Now;
                            db.SaveChanges();
                            Value = 1;
                            Tran.Complete();
                        }
                        else
                        {
                            Value = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Value = 0;
                }
            }
            return Value;
        }
        public List<TyreSizeMaster> SelectList(string Size)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = db.TyreSizeMasters.OrderBy(obj => obj.TyreSize).ToList();
                if (Size != "")
                {
                    Lst = Lst.Where(e => e.TyreSize.ToLower().Contains(Size.ToLower())).ToList();
                }
                return Lst;
            }
        }
        public TyreSizeMaster SelectByID(Int64 TyresizeId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                TyreSizeMaster tbl = db.TyreSizeMasters.Where(t => t.TyreSize_Idno == TyresizeId).FirstOrDefault();
                return tbl;
            }
        }
        public Int64 Delete(Int64 TyresizeId)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    TyreSizeMaster tbl = db.TyreSizeMasters.Where(t => t.TyreSize_Idno == TyresizeId).FirstOrDefault();
                    db.TyreSizeMasters.DeleteObject(tbl);
                    db.SaveChanges();
                    Value = 1;

                }
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public List<TyreSizeMaster> Count()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = db.TyreSizeMasters.ToList();

                return Lst;
            }
        }
        public IList FillColor()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var obj = (from MM in db.LaneMasters orderby MM.Lane_Idno select MM).ToList();
                return obj;
            }
        }
    }
}
