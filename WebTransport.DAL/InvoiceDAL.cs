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
    public class InvoiceDAL
    {
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

        public double SelectServiceTaxFromTaxMaster(DateTime InvDate, Int32 CityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters
                       join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                       where TM.TaxTyp_Idno == 1 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                       select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters
                             join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                             where TM.TaxTyp_Idno == 1 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                             select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        public double SelectSwchBrtTaxFromTaxMaster(DateTime InvDate, Int32 CityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters
                       join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                       where TM.TaxTyp_Idno == 3 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                       select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters
                             join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                             where TM.TaxTyp_Idno == 3 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                             select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
            }
        }

        public Int64 SelectMaxInvNo(Int32 YearIdno, Int32 FromCity, String Prefix)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 MaxNo = 0;
                MaxNo = Convert.ToInt32((from obj in db.tblInvGenHeads where obj.BaseCity_Idno == FromCity && obj.Year_Idno == YearIdno && obj.Inv_prefix == Prefix select obj.Inv_No).Max());
                return MaxNo = MaxNo + 1;
            }
        }

        public IList SelectAll()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblRateMasts
                        orderby mast.Item_Rate
                        select mast).ToList();
            }
        }

        public tblUserPref SelectUserPref()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                return (from obj in db.tblUserPrefs select obj).FirstOrDefault();
            }
        }

        public double SelectSaveRcptAmnt(string GrIdnos, string con)
        {
            double rcptAmnt = 0;
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectSaveRcptAmnt");
            objSqlPara[1] = new SqlParameter("@InvIdno", 0);
            objSqlPara[2] = new SqlParameter("@GrIdnos", GrIdnos);
            rcptAmnt = Convert.ToDouble(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spInvGen", objSqlPara));
            return rcptAmnt;
        }
        public double SelectUpdateRcptAmnt(Int64 InvIdno, string con)
        {
            double rcptAmnt = 0;
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectUpdateRcptAmnt");
            objSqlPara[1] = new SqlParameter("@InvIdno", InvIdno);
            rcptAmnt = Convert.ToDouble(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "spInvGen", objSqlPara));
            return rcptAmnt;
        }
        public bool selectServTaxExmpt(Int32 PartyIdo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return Convert.ToBoolean((from obj in db.AcntMasts where obj.Acnt_Idno == PartyIdo select obj.ServTax_Exmpt).FirstOrDefault());
            }
        }
        public List<tblCityMaster> SelectCityCombo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> lst = null;
                lst = (from cm in db.tblCityMasters where cm.AsLocation == true orderby cm.City_Name ascending select cm).ToList();
                return lst;
            }
        }

        public List<DriverMast> selectDriverName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<DriverMast> lst = null;
                lst = (from DR in db.DriverMasts orderby DR.Driver_Name ascending select DR).ToList();
                return lst;
            }
        }

        public bool CheckServTaxPerExmpt(Int32 SenderIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                bool lst = false;
                lst = Convert.ToBoolean((from DR in db.AcntMasts where DR.Acnt_Idno == SenderIdno select DR.ServTax_Exmpt).FirstOrDefault());
                return lst;
            }
        }

        public double selectServTaxPer()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                double lst = 0;
                lst = Convert.ToDouble((from DR in db.tblUserPrefs select DR.ServTax_Perc).FirstOrDefault());
                return lst;
            }
        }

        public IList selectSenderName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var lst = (from AM in db.AcntMasts
                           join GH in db.TblGrHeads on AM.Acnt_Idno equals GH.Sender_Idno
                           where GH.GR_Frm == "BK" && (GH.GR_Typ == 2) && (AM.Acnt_Type == 2) && AM.INTERNAL == false
                           orderby AM.Acnt_Name ascending
                           select new
                           {

                               AM.Acnt_Idno,
                               AM.Acnt_Name,
                           }
                           )
                           .Union
                           (
                from AM in db.AcntMasts
                join GH in db.tblGrRetailerHeads on AM.Acnt_Idno equals GH.Sender_Idno
                where (GH.GRRet_Typ == 2) && (AM.Acnt_Type == 2) && AM.INTERNAL == false
                orderby AM.Acnt_Name ascending
                select new
                {
                    AM.Acnt_Idno,
                    AM.Acnt_Name,
                }
                ).Distinct().ToList();
                           
                return lst;
            }
        }

        public IList search(Int32 yearid, Int32 InvNo, DateTime? dtfrom, DateTime? dtto, int SenderIdno, Int32 FromCity, string GrType)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblInvGenHeads
                           join AM in db.AcntMasts on CH.Sendr_Idno equals AM.Acnt_Idno
                           join CM in db.tblCityMasters on CH.BaseCity_Idno equals CM.City_Idno
                           where CH.Gr_Type == GrType
                           select new
                             {
                                 CH.Inv_Idno,
                                 CH.Sendr_Idno,
                                 AM.Acnt_Name,
                                 CH.Inv_Date,
                                 CH.Inv_No,
                                 CH.Inv_prefix,
                                 CH.Net_Amnt,
                                 CH.Year_Idno,
                                 CH.BaseCity_Idno,
                                 CM.City_Name,
                                 CH.Admin_Approval,
                                 CH.Gr_Type,
                             }).ToList();
                if (InvNo > 0)
                {
                    lst = lst.Where(l => l.Inv_No == InvNo).ToList();
                }
                if (dtto != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Inv_Date).Date <= Convert.ToDateTime(dtto).Date).ToList();
                }
                if (dtfrom != null)
                {
                    lst = lst.Where(l => Convert.ToDateTime(l.Inv_Date).Date >= Convert.ToDateTime(dtfrom).Date).ToList();
                }


                if (SenderIdno > 0)
                {
                    lst = lst.Where(l => l.Sendr_Idno == SenderIdno).ToList();
                }
                if (FromCity > 0)
                {
                    lst = lst.Where(l => l.BaseCity_Idno == FromCity).ToList();
                }
                if (yearid > 0)
                {
                    lst = lst.Where(l => l.Year_Idno == yearid).ToList();
                }

                return lst;
            }
        }

        public DataTable DsTrAcnt(string con)
        {
            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT Acnt_Idno AS 'TransportAccountID' FROM ACNTMAST WHERE ACNT_NAME='Transport Charges' AND INTERNAL=1";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable DtAcntDS(string con)
        {

            string sqlSTR = string.Empty;
            sqlSTR = @"SELECT ISNULL(AcntLink_Idno,0) AS ID,ISNULL(IGrp_Idno,0) AS IGrp_Idno,ISNULL(Commsn_Idno,0) AS CAcnt_Idno,ISNULL(OthrAc_Idno,0) AS OTAcnt_Idno,ISNULL(SAcnt_Idno,0) AS SAcnt_Idno,ISNULL(SwachBharat_Idno,0) AS SwachBharat_Idno ,ISNULL(KrishiKalyan_Idno,0) AS KrishiKalyan_Idno, Isnull(Igst_Idno,0) as Igst_Idno, Isnull(Cgst_Idno,0) as Cgst_Idno,Isnull(Sgst_Idno,0) as Sgst_Idno FROM tblAcntLink";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }
        public tblInvGenHead selectHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblInvGenHeads.Where(h => h.Inv_Idno == HeadId).FirstOrDefault();
            }
        }

        public AcntMast SelectPricIdno(Int64 intAcntIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.AcntMasts.Where(h => h.Acnt_Idno == intAcntIdno).FirstOrDefault();

            }
        }
        public TblGrHead selectGRHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.TblGrHeads.Where(h => h.GR_Idno == HeadId).FirstOrDefault();
            }
        }
        public tblGrRetailerHead selectGRRetailerHead(Int64 HeadId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return db.tblGrRetailerHeads.Where(h => h.GRRetHead_Idno == HeadId).FirstOrDefault();
            }
        }
        public double SelectKisanTaxFromTaxMaster(DateTime InvDate, Int32 CityIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Max = 0; Int64 MaxId = 0; double TaxRate = 0;
                Max = (from TM in db.tblTaxMasters
                       join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                       where TM.TaxTyp_Idno == 4 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                       select TM.TaxMast_Idno).Count();
                if (Max > 0)
                {
                    MaxId = (from TM in db.tblTaxMasters
                             join CM in db.tblCityMasters on TM.State_Idno equals CM.State_Idno
                             where TM.TaxTyp_Idno == 4 && CM.City_Idno == CityIdno && TM.Tax_Date <= InvDate
                             select TM.TaxMast_Idno).Max();
                }
                if (MaxId > 0)
                {
                    TaxRate = Convert.ToDouble((from TM in db.tblTaxMasters where TM.TaxMast_Idno == MaxId select TM.Tax_Rate).FirstOrDefault());
                }
                return TaxRate;
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

        public Int64 Insert(tblInvGenHead obj, DataTable DtTemp, string GrType)
        {
            Int64 InvIdno = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                tblChlnBookHead CHead = new tblChlnBookHead();
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        tblInvGenHead RH = db.tblInvGenHeads.Where(rh => rh.Inv_No == obj.Inv_No && rh.Inv_prefix == obj.Inv_prefix && rh.BaseCity_Idno == obj.BaseCity_Idno && rh.Year_Idno == obj.Year_Idno).FirstOrDefault();
                        if (RH != null)
                        {
                            InvIdno = -1;
                        }
                        else
                        {
                            obj.Date_Added = DateTime.Now;
                            obj.Date_Modified = DateTime.Now;
                            db.tblInvGenHeads.AddObject(obj);

                            db.SaveChanges();
                            InvIdno = obj.Inv_Idno;
                            if (DtTemp.Rows.Count > 0)
                            {
                                foreach (DataRow Dr in DtTemp.Rows)
                                {
                                    tblInvGenDetl objtblInvGenDetl = new tblInvGenDetl();
                                    objtblInvGenDetl.InvGenHead_Idno = InvIdno;
                                    objtblInvGenDetl.GR_Idno = Convert.ToInt64(Dr["GR_Idno"]);
                                    objtblInvGenDetl.Item_Idno = Convert.ToInt64(Dr["Item_Idno"]);
                                    objtblInvGenDetl.Item_Rate = Convert.ToDouble(Dr["Item_Rate"]);
                                    objtblInvGenDetl.Unit_Idno = Convert.ToInt64(Dr["Unit_Idno"]);
                                    objtblInvGenDetl.Amount = Convert.ToDouble(Dr["Amount"]);
                                    objtblInvGenDetl.Wayges = Convert.ToDouble(Dr["Wayges"]);
                                    objtblInvGenDetl.Net_Amnt = Convert.ToDouble(Dr["Net_Amnt"]);
                                    objtblInvGenDetl.Other_Amnt = Convert.ToDouble(Dr["Other_Amnt"]);
                                    objtblInvGenDetl.ServTax_Amnt = Convert.ToDouble(Dr["ServTax_Amnt"]);
                                    objtblInvGenDetl.ServTax_Perc = Convert.ToDouble(Dr["ServTax_Perc"]);
                                    objtblInvGenDetl.ServTax_Valid = Convert.ToDouble(Dr["ServTax_Valid"]);
                                    objtblInvGenDetl.SwchBrtTax_Amnt = Convert.ToDouble(Dr["SwchBrtTax_Amnt"]);
                                    objtblInvGenDetl.KisanTax_Amnt = Convert.ToDouble(Dr["KisanKalyan_Amnt"]);
                                    //#GST
                                    objtblInvGenDetl.SGST_Amt = Convert.ToDouble(Dr["SGST_Amt"]);
                                    objtblInvGenDetl.CGST_Amt = Convert.ToDouble(Dr["CGST_Amt"]);
                                    objtblInvGenDetl.IGST_Amt = Convert.ToDouble(Dr["IGST_Amt"]);
                                    objtblInvGenDetl.Annexure_No = Convert.ToString(Dr["Annexure_No"]);
                                    db.tblInvGenDetls.AddObject(objtblInvGenDetl);
                                    db.SaveChanges();
                                }


                            }
                            if (DtTemp.Rows.Count > 0)
                            {
                                if (GrType == "GR")
                                {

                                    foreach (DataRow Dr in DtTemp.Rows)
                                    {

                                        Int32 GrIdno = Convert.ToInt32(Dr["GR_Idno"]);
                                        TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                        objTblGrHead.Billed = true;
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    foreach (DataRow Dr in DtTemp.Rows)
                                    {

                                        Int32 GrIdno = Convert.ToInt32(Dr["GR_Idno"]);
                                        tblGrRetailerHead objTblGrHead = (from obj1 in db.tblGrRetailerHeads where obj1.GRRetHead_Idno == GrIdno select obj1).FirstOrDefault();
                                        objTblGrHead.Billed = true;
                                        db.SaveChanges();
                                    }
                                }
                            }

                            dbTran.Commit();
                        }


                    }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }

                return InvIdno;


            }

        }

        public Int64 Update(tblInvGenHead obj, Int32 InvIdno, DataTable DtTemp, string Grtype)
        {
            Int64 InvHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {
                        //tblInvGenHead CH = db.tblInvGenHeads.Where(rh => rh.Inv_No == obj.Inv_No && rh.Inv_Idno != InvIdno && rh.Inv_prefix == obj.Inv_prefix && rh.BaseCity_Idno == obj.BaseCity_Idno).FirstOrDefault();
                        //if (CH != null)
                        //{
                        //    InvHeadId = -1;
                        //}
                        //else
                        //{
                            tblInvGenHead TBH = db.tblInvGenHeads.Where(rh => rh.Inv_Idno == InvIdno).FirstOrDefault();
                            if (TBH != null)
                            {
                                TBH.Inv_No = obj.Inv_No;
                                TBH.Inv_prefix = obj.Inv_prefix;
                                TBH.Inv_Date = obj.Inv_Date;
                                TBH.Sendr_Idno = obj.Sendr_Idno;
                                TBH.BaseCity_Idno = obj.BaseCity_Idno;
                                TBH.Year_Idno = obj.Year_Idno;
                                TBH.GrossTot_Amnt = obj.GrossTot_Amnt;
                                TBH.ServTax_Amnt = obj.ServTax_Amnt;
                                TBH.Short_Amnt = obj.Short_Amnt;
                                TBH.Net_Amnt = obj.Net_Amnt;
                                TBH.Bilty_Chrgs = obj.Bilty_Chrgs;
                                TBH.TBB_Rate = obj.TBB_Rate;
                                TBH.TrServTax_Amnt = obj.TrServTax_Amnt;
                                TBH.TrSwchBrtTax_Amnt = obj.TrSwchBrtTax_Amnt;
                                TBH.ConsignrServTax = obj.ConsignrServTax;
                                TBH.TrKisanKalyanTax_Amnt = obj.TrKisanKalyanTax_Amnt;
                                TBH.ConsignrKisanTax_Amnt = obj.ConsignrKisanTax_Amnt;
                                TBH.RoundOff_Amnt = obj.RoundOff_Amnt;
                                TBH.Admin_Approval = obj.Admin_Approval;
                                TBH.Print_Format = obj.Print_Format;
                                TBH.Plant_InDate = obj.Plant_InDate;
                                TBH.Plant_OutDate = obj.Plant_OutDate;
                                TBH.Port_InDate = obj.Port_InDate;
                                TBH.Port_OutDate = obj.Port_OutDate;
                                TBH.PlantAmount = obj.PlantAmount;
                                TBH.PortAmount = obj.PortAmount;
                                TBH.PortDays = obj.PortDays;
                                TBH.PlantDays = obj.PlantDays;
                                TBH.HQCharges_Amnt = obj.HQCharges_Amnt;
                                TBH.AddCharges_Amnt = obj.AddCharges_Amnt;
                                TBH.Charges1_Name = obj.Charges1_Name;
                                TBH.Charges1_Amnt = obj.Charges1_Amnt;
                                TBH.Charges2_Name = obj.Charges2_Name;
                                TBH.Charges2_Amnt = obj.Charges2_Amnt;
                                TBH.Date_Modified = DateTime.Now;
                                TBH.ShtgGST_Amt = obj.ShtgGST_Amt;
                                TBH.User_ModifiedBy = obj.User_ModifiedBy;
                                TBH.Delivery_Add = obj.Delivery_Add;
                                db.SaveChanges();
                                InvHeadId = TBH.Inv_Idno;
                                List<tblInvGenDetl> InvDetl = db.tblInvGenDetls.Where(rd => rd.InvGenHead_Idno == InvIdno).ToList();
                                foreach (tblInvGenDetl rgd in InvDetl)
                                {
                                    db.tblInvGenDetls.DeleteObject(rgd);
                                    db.SaveChanges();
                                }

                                if (DtTemp.Rows.Count > 0)
                                {
                                    foreach (DataRow Dr in DtTemp.Rows)
                                    {
                                        tblInvGenDetl objtblInvGenDetl = new tblInvGenDetl();
                                        objtblInvGenDetl.InvGenHead_Idno = InvIdno;
                                        objtblInvGenDetl.GR_Idno = Convert.ToInt64(Dr["GR_Idno"]);
                                        objtblInvGenDetl.Item_Idno = Convert.ToInt64(Dr["Item_Idno"]);
                                        objtblInvGenDetl.Item_Rate = Convert.ToDouble(Dr["Item_Rate"]);
                                        objtblInvGenDetl.Unit_Idno = Convert.ToInt64(Dr["Unit_Idno"]);
                                        objtblInvGenDetl.Amount = Convert.ToDouble(Dr["Amount"]);
                                        objtblInvGenDetl.Wayges = Convert.ToDouble(Dr["Wayges"]);
                                        objtblInvGenDetl.Net_Amnt = Convert.ToDouble(Dr["Net_Amnt"]);
                                        objtblInvGenDetl.Other_Amnt = Convert.ToDouble(Dr["Other_Amnt"]);
                                        objtblInvGenDetl.ServTax_Amnt = Convert.ToDouble(Dr["ServTax_Amnt"]);
                                        objtblInvGenDetl.ServTax_Perc = Convert.ToDouble(Dr["ServTax_Perc"]);
                                        objtblInvGenDetl.ServTax_Valid = Convert.ToDouble(Dr["ServTax_Valid"]);
                                        objtblInvGenDetl.SwchBrtTax_Amnt = Convert.ToDouble(Dr["SwchBrtTax_Amnt"]);
                                        objtblInvGenDetl.KisanTax_Amnt = Convert.ToDouble(Dr["KisanKalyan_Amnt"]);
                                        objtblInvGenDetl.Annexure_No = Convert.ToString(Dr["Annexure_No"]);
                                        db.tblInvGenDetls.AddObject(objtblInvGenDetl);
                                        db.SaveChanges();
                                    }

                                }

                                if (DtTemp.Rows.Count > 0)
                                {
                                    if (Grtype == "GR")
                                    {

                                        foreach (DataRow Dr in DtTemp.Rows)
                                        {

                                            Int32 GrIdno = Convert.ToInt32(Dr["GR_Idno"]);
                                            TblGrHead objTblGrHead = (from obj1 in db.TblGrHeads where obj1.GR_Idno == GrIdno select obj1).FirstOrDefault();
                                            objTblGrHead.Billed = true;
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow Dr in DtTemp.Rows)
                                        {

                                            Int32 GrIdno = Convert.ToInt32(Dr["GR_Idno"]);
                                            tblGrRetailerHead objTblGrHead = (from obj1 in db.tblGrRetailerHeads where obj1.GRRetHead_Idno == GrIdno select obj1).FirstOrDefault();
                                            objTblGrHead.Billed = true;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                dbTran.Commit();
                            }
                        }



                   // }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return InvHeadId;
        }

        public Int64 UpdateAdminApproval(Int32 InvIdno, bool AdminApproval)
        {
            Int64 value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblInvGenHead objTblInvHead = (from obj1 in db.tblInvGenHeads where obj1.Inv_Idno == InvIdno select obj1).FirstOrDefault();
                    objTblInvHead.Admin_Approval = AdminApproval;
                    db.SaveChanges();
                    value = Convert.ToInt64(objTblInvHead.Inv_Idno);
                }
            }
            catch (Exception Ex)
            {
                value = 0;
            }
            return value;
        }
        public DataTable SelectDBData(Int64 Item_Idno, string con)
        {
            string str = string.Empty;
            str = @"select isnull(Rate_Idno,0) Rate_Idno,isnull(Rate_Date,0) Rate_Date,Isnull(CM.City_Name,'') City_Name ,Isnull(Item_Rate,0) Item_Rate,ISNULL(Item_WghtRate,0) Item_WghtRate,ISNULL(QtyShrtg_Limit,0)QtyShrtg_Limit,ISNULL(QtyShrtg_Rate,0)QtyShrtg_Rate,ISNULL(WghtShrtg_Limit,0)WghtShrtg_Limit,ISNULL(WghtShrtg_Rate,0)WghtShrtg_Rate,ISNULL(Tocity_Idno,0) Tocity_Idno from tblRateMast RM Inner Join
           tblCityMaster CM ON CM.city_Idno=RM.ToCity_Idno where RM.Item_Idno= " + Item_Idno + "";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, str);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable selectGrDetails(string Action, Int64 YearId, string dtFrmDate, string dtToDate, Int64 SenderIdno, string con, Int32 FromCity, string Grtype)
        {
            SqlParameter[] objSqlPara = new SqlParameter[7];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@SendrIdno", SenderIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", YearId);
            objSqlPara[3] = new SqlParameter("@InvDate", dtFrmDate);
            objSqlPara[4] = new SqlParameter("@ToDate", dtToDate);
            objSqlPara[5] = new SqlParameter("@FromCity", FromCity);
            objSqlPara[6] = new SqlParameter("@GRTYPES", Grtype);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public DataTable SelectGrChallanDetails(string con, Int64 iYearId, string AllItmIdno, DateTime Invdt, bool TbbRate)
        {
            SqlParameter[] objSqlPara = new SqlParameter[5];
            objSqlPara[0] = new SqlParameter("@Action", "SelectGRDetailInInv");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
            objSqlPara[3] = new SqlParameter("@InvDate", Invdt);
            objSqlPara[4] = new SqlParameter("@TbbRateCheck", TbbRate);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public DataTable SelectGrChallanRetailerDetails(string con, Int64 iYearId, string AllItmIdno, DateTime Invdt, bool TbbRate)
        {
            SqlParameter[] objSqlPara = new SqlParameter[4];
            objSqlPara[0] = new SqlParameter("@Action", "SELECTGRRetailerDetailInInv");
            objSqlPara[1] = new SqlParameter("@GRIdnos", AllItmIdno);
            objSqlPara[2] = new SqlParameter("@YearIdno", iYearId);
            objSqlPara[3] = new SqlParameter("@InvDate", Invdt);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public DataTable GetChallanDetail(string con, Int64 iGr_Idno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallanDetlByGrIdno");
            objSqlPara[1] = new SqlParameter("@Gr_Idno", iGr_Idno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public DataTable GetUserPrefLessChlnAmnt(string con, Int64 iGr_Idno)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "SelectChallanDetlByGrIdno");
            objSqlPara[1] = new SqlParameter("@Gr_Idno", iGr_Idno);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public DataTable selectDetl(string con, Int64 iYearId, Int64 HeadId, string GrType)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", "SelectDetlNew");
            objSqlPara[1] = new SqlParameter("@Id", HeadId);
            objSqlPara[2] = new SqlParameter("@GRTYPES", GrType);
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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

        public int Delete(Int64 HeadId, Int64 UserIdno, string con, string Grtype)
        {
            int value = 0; clsAccountPosting objclsAccountPosting = new clsAccountPosting();
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblInvGenHead CH = db.tblInvGenHeads.Where(h => h.Inv_Idno == HeadId).FirstOrDefault();
                List<tblInvGenDetl> CD = db.tblInvGenDetls.Where(d => d.InvGenHead_Idno == HeadId).ToList();
                if (CD != null && CH != null)
                {
                    var lst=(from AI in db.tblAmntRecvInv_Detl where AI.Inv_Idno==HeadId select AI).ToList();
                    
                    if(lst!=null && lst.Count>0)
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

                            if (Grtype == "GR")
                            {
                                foreach (var d in CD)
                                {
                                    TblGrHead objTblGrHead = (from obj in db.TblGrHeads where obj.GR_Idno == d.GR_Idno select obj).FirstOrDefault();
                                    objTblGrHead.Billed = false;
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                foreach (var d in CD)
                                {
                                    tblGrRetailerHead objtblGrRetailerHead = (from obj in db.tblGrRetailerHeads where obj.GRRetHead_Idno == d.GR_Idno select obj).FirstOrDefault();
                                    objtblGrRetailerHead.Billed = false;
                                    db.SaveChanges();
                                }
                            }
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

        public Int64 TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from CH in db.tblInvGenHeads
                           join AM in db.AcntMasts on CH.Sendr_Idno equals AM.Acnt_Idno
                           join CM in db.tblCityMasters on CH.BaseCity_Idno equals CM.City_Idno
                           select new
                           {
                               CH.Inv_Idno,
                           }).Count();
                return lst;
            }
        }
        public Int64 MaxIdno(string con, Int64 FronCityIdno)
        {
            Int64 MaxNo = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(MAX(Inv_Idno),0)  AS MAXID FROM tblInvGenHead WHERE BaseCity_Idno=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                MaxNo = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return MaxNo;
        }

        public Int64 cityID(string con, Int64 FronCityIdno)
        {
            Int64 cityid = 0; string sqlSTR = "";
           sqlSTR = @"Select cm.City_Idno from tblCityMaster cm inner join tblInvGenHead IGH on cm.City_Idno= IGH.BaseCity_Idno WHERE BaseCity_Idno=" + FronCityIdno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                cityid = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return cityid;
        }
        public Int64 sender(string con, Int64 inviidno)
        {
            Int64 sendner = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(Sendr_Idno,0) as Sendr_Idno FROM tblInvGenHead WHERE  Inv_Idno=" + inviidno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                sendner = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return sendner;
        }
        public Int64 lastprint(string con, Int64 inviidno)
        {
            Int64 sendner = 0; string sqlSTR = "";
            sqlSTR = @"Select Isnull(am.PComp_Idno,0) as PComp_Idno from tblInvGenHead gh inner join AcntMast am  on gh.Sendr_Idno= am.Acnt_Idno where gh.Inv_Idno=" + inviidno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                sendner = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return sendner;
        }
        public Int64 printFormat(string con, Int64 inviidno)
        {
            Int64 sendner = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ISNULL(Print_Format,0) as Sendr_Idno FROM tblInvGenHead WHERE  Inv_Idno=" + inviidno + "";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                sendner = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return sendner;
        }

        public AcntMast SelectAcnt(Int64 AcntID)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                AcntMast am = (from a in db.AcntMasts where a.Acnt_Idno == AcntID select a).FirstOrDefault();
                return am;
            }
        }

        public void UpdateIsPosting(Int64 Inv_Idno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                try
                {
                    tblInvGenHead GH = (from G in db.tblInvGenHeads where G.Inv_Idno == Inv_Idno select G).FirstOrDefault();
                    if (GH != null)
                    {
                        GH.IS_Post = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        public tblUserPref GetUserPrefDetail()
        {
            tblUserPref uPref = null;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                uPref = db.tblUserPrefs.SingleOrDefault();
            }
            return uPref;
        }

        public int SaveUserPrefDetail(tblUserPref tblData)
        {
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    var data = db.tblUserPrefs.SingleOrDefault();
                    data.ReqShtgGST = tblData.ReqShtgGST;
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Int64 ChlnIdno(string con, Int64 invidno)
        {
            Int64 inv = 0; string sqlSTR = "";
            sqlSTR = @"SELECT ChlnBookHead_Idno FROM tblChlnBookDetl CBD WHERE CBD.GR_Idno IN (SELECT DISTINCT GR_Idno FROM tblInvGenDetl WHERE InvGenHead_Idno =" + invidno + ")";
            DataSet dt = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                inv = Convert.ToInt64(dt.Tables[0].Rows[0][0]);
            }
            return inv;
        }
        public DataTable city(string con, string cityname)
        {
            SqlParameter[] objSqlPara = new SqlParameter[2];
            objSqlPara[0] = new SqlParameter("@Action", "Selectlocation");
            objSqlPara[1] = new SqlParameter("@Cityname", cityname);
          
            DataSet objDsTemp = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "spInvGen", objSqlPara);
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
        public Int64 Updatebilled( Int64 GrIdno)
        {
            Int64 GrHeadId = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                db.Connection.Open();
                using (DbTransaction dbTran = db.Connection.BeginTransaction())
                {
                    try
                    {

                        TblGrHead TBH = db.TblGrHeads.Where(rh => rh.GR_Idno == GrIdno).FirstOrDefault();
                        if (TBH != null)
                        {
                            TBH.Billed = false;
                            db.SaveChanges();
                            //GrHeadId = TBH.GR_Idno;
                            //List<tblInvGenDetl> InvDetl = db.tblInvGenDetls.Where(rd => rd.GR_Idno == GrIdno).ToList();
                            //foreach (tblInvGenDetl rgd in InvDetl)
                            //{
                            //    db.tblInvGenDetls.DeleteObject(rgd);
                            //    db.SaveChanges();
                            //}
                            dbTran.Commit();
                        }
                    }
                   // }
                    catch
                    {
                        dbTran.Rollback();
                    }
                }
            }
            return GrHeadId;
        }
    }
}
