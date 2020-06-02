using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class CommissionMasterDAL
    {

        public List<tblStateMaster> selectState()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblStateMaster> lst = null;
                lst = (from SM in db.tblStateMasters orderby SM.State_Name ascending select SM).ToList();
                return lst;
            }
        }

        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }

        public tblUserPref selectuserpref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserPref userpref = (from UP in db.tblUserPrefs select UP).FirstOrDefault();
                return userpref;
            }
        }


        public List<ItemMast> GetItems()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> lst = null;
                lst = (from cm in db.ItemMasts orderby cm.Item_Name ascending select cm).ToList();
                return lst;
            }
        }
        public bool Insert(DataTable dT, tblCommmissionMastHead objtblCommmissionMastHead)
        {
            Int64 value = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblCommmissionMastHead objHead = new tblCommmissionMastHead();
                db.tblCommmissionMastHeads.AddObject(objtblCommmissionMastHead);
                db.SaveChanges();
                value = objtblCommmissionMastHead.Head_Idno;

                if (dT != null && dT.Rows.Count > 0)
                {
                    for (int i = 0; i < dT.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dT.Rows[i]["Comision_Amnt"]) > 0)
                        {
                            tblCommmissionMastDetl obj = new tblCommmissionMastDetl();
                            obj.Head_Idno = value;
                            obj.Tocity_Idno = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Tocity_Idno"])) ? 0 : Convert.ToInt32(dT.Rows[i]["Tocity_Idno"]);
                            obj.Comsn_Amnt = string.IsNullOrEmpty(Convert.ToString(dT.Rows[i]["Comision_Amnt"])) ? 0 : Convert.ToDouble(dT.Rows[i]["Comision_Amnt"]);
                            db.tblCommmissionMastDetls.AddObject(obj);
                            db.SaveChanges();
                        }
                    }
                }
                return true;
            }

        }

        public DataTable SelectDBData(Int64 Item_Idno, Int64 FrmCity_Idno, Int64 StateIdno, Int32 YearIdno, string con,string Date)
        {
            string str = string.Empty;

            SqlParameter[] objSqlPara = new SqlParameter[6];
            objSqlPara[0] = new SqlParameter("@Action", "SelecAmnt");
            objSqlPara[1] = new SqlParameter("@ItemIdno", Item_Idno);
            objSqlPara[2] = new SqlParameter("@StateIdno", StateIdno);
            objSqlPara[3] = new SqlParameter("@FromCityIdno", FrmCity_Idno);
            objSqlPara[4] = new SqlParameter("@YearIdno", YearIdno);
            objSqlPara[5] = new SqlParameter("@Date", Date);
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spComissionMast", objSqlPara);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public Int64 HeadId(Int32 itemIdno, Int32 FromCityIdno, Int32 StateIdno,DateTime Date)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 objMast = Convert.ToInt64((from mast in db.tblCommmissionMastHeads
                                                 where mast.Item_Idno == itemIdno && mast.FromCity_Idno == FromCityIdno && mast.State_Idno == StateIdno && mast.Date == Date
                                                 select mast.Head_Idno
                                 ).FirstOrDefault());
                return objMast;
            }
        }

        public Int64 Delete(int intItemIdno, Int32 FromCityIdno, Int32 StateIdno,DateTime Date)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblCommmissionMastHead objMast = (from mast in db.tblCommmissionMastHeads
                                                      where mast.Item_Idno == intItemIdno && mast.FromCity_Idno == FromCityIdno && mast.State_Idno == StateIdno && mast.Date==Date
                                                      select mast).FirstOrDefault();
                    if (objMast != null)
                    {
                        intValue = objMast.Head_Idno;
                        db.tblCommmissionMastHeads.DeleteObject(objMast);
                        db.SaveChanges();
                        IList<tblCommmissionMastDetl> objtblCommmissionMastDetl = (from obj in db.tblCommmissionMastDetls where obj.Head_Idno == intValue select obj).ToList();
                        if (objtblCommmissionMastDetl != null)
                        {
                            foreach (var lst in objtblCommmissionMastDetl)
                            {
                                db.tblCommmissionMastDetls.DeleteObject(lst);
                                db.SaveChanges();
                            }
                            intValue = 1;
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
                if (Convert.ToBoolean(Ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint")) == true)
                {
                    intValue = -1;
                }
            }
            return intValue;
        }

        public IList PreviousDateList()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from cm in db.tblCommmissionMastHeads
                           join IM in db.ItemMasts on cm.Item_Idno equals IM.Item_Idno
                           join LM in db.tblCityMasters on cm.FromCity_Idno equals LM.City_Idno
                           select new { cm.Date,IM.Item_Name,LM.City_Name }).Distinct().OrderByDescending(x => x.Date).ToList();
                return lst;
            }
        }
    }
}
