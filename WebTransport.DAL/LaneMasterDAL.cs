using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport.DAL
{
    public class LaneMasterDAL
    {
        public int Save(string Name)
        {
            int Value = 0;
            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                    {
                        LaneMaster Exsit = db.LaneMasters.Where(c => c.Lane_Name == Name).FirstOrDefault();
                        if (Exsit == null)
                        {
                            LaneMaster tbl = new LaneMaster();
                            tbl.Lane_Name = Name;
                            tbl.Date_Added = System.DateTime.Now;
                            db.LaneMasters.AddObject(tbl);
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
        public int Update(int LaneIdno, string Name)
        {
            int Value = 0;
            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                    {
                        LaneMaster Exsit = db.LaneMasters.Where(c => c.Lane_Name == Name && c.Lane_Idno != LaneIdno).FirstOrDefault();
                        //tblMISMast Exsit = db.tblMISMasts.Where(e => e.MIS_Name == Name && e.MIS_Type == Type && e.MIS_Idno != MISId).FirstOrDefault();
                        if (Exsit == null)
                        {
                            LaneMaster tbl = db.LaneMasters.Where(c => c.Lane_Idno == LaneIdno).FirstOrDefault();
                            tbl.Lane_Name = Name;
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
        public List<LaneMaster> SelectList(string Name)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = db.LaneMasters.OrderBy(obj => obj.Lane_Name).ToList();
                if (Name != "")
                {
                    Lst = Lst.Where(e => e.Lane_Name.ToLower().Contains(Name.ToLower())).ToList();
                }
                return Lst;
            }
        }
        public LaneMaster SelectByID(int LaneId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                LaneMaster tbl = db.LaneMasters.Where(t => t.Lane_Idno == LaneId).FirstOrDefault();
                return tbl;
            }
        }
        public int Delete(int LaneId)
        {
            int Value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    LaneMaster tbl = db.LaneMasters.Where(t => t.Lane_Idno == LaneId).FirstOrDefault();
                        db.LaneMasters.DeleteObject(tbl);
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

        public List<LaneMaster> Count()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var Lst = db.LaneMasters.ToList();

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
