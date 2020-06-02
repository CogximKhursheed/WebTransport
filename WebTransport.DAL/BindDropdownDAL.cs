using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace WebTransport.DAL
{
    public class BindDropdownDAL
    {

        string sqlSTR = string.Empty;

        public IList CheckItemExistInOtherMaster(string SerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj2 in db.Stckdetls
                        where obj2.SerialNo == SerialNo && (obj2.Is_Issued == true || obj2.MtrlIssue_Idno > 0 || obj2.Billed == true || obj2.Br_Trans == true)
                        select new
                        {
                            Idno = obj2.SerlDetl_id
                        }).ToList();
            }
        }
        public Array BindDate()
        {
            List<string> [] list= new List<String> [2];
            list[0]=new List<string>();
            list[1]=new List<string>();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int numDays = DateTime.DaysInMonth(year, month);
            string StratDate = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).ToString("dd-MM-yyyy"));// strdate;
            string EndDate = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, numDays).ToString("dd-MM-yyyy"));

            string[] strArray = new string[2];
            strArray[0]=StratDate;
            strArray[1]=EndDate;
           // list[0].(StratDate);
            //list[1].Add(EndDate);
            return strArray;
        }
        public tblPopUpMessage CheckSiteoffMessage()
        {
            using (TransportMandiEntities db = new TransportMandiEntities())
            {
                return (from PM in db.tblPopUpMessages select PM).FirstOrDefault();
            }
        }
        public List<tblItemTypeMast> BindItemGroup()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from PM in db.tblItemTypeMasts select PM).ToList();
            }
        }

        public List<tblPrincCompMast> BindPrincComp()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from PM in db.tblPrincCompMasts where PM.Status == true select PM).ToList();
            }
        }
        public List<tblPrincCompLocMast> BindPrincLoc(Int64 intPCompIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from PM in db.tblPrincCompLocMasts where PM.PComp_Idno == intPCompIdno && PM.Status == true select PM).ToList();
            }
        }
        public List<tblPrincCompLocMast> BindPrincLocByGSTDate(Int64 intPCompIdno, bool isGST)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                if(isGST)
                return (from PM in db.tblPrincCompLocMasts where PM.PComp_Idno == intPCompIdno && PM.Status == true && PM.Is_GST == true select PM).ToList();
                else
                return (from PM in db.tblPrincCompLocMasts where PM.PComp_Idno == intPCompIdno && PM.Status == true && PM.Is_GST == false select PM).ToList();
            }
        }
        //Upadhyay #GST
        private string GetGSTDate()
        {
            DateTime gstDate;
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                gstDate = (from i in db.tblUserPrefs select i.GST_Date).FirstOrDefault();
                return gstDate.ToString("dd-MM-yyyy");
            }
        }
        public string SelectPages(Int64 intIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                string PageName = string.Empty;
                PageName = (from PM in db.tblPrincCompLocMasts where PM.PCompLoc_Idno == intIdno select PM.Page_Name).FirstOrDefault();
                return PageName;
            }
        }
        public List<AcntMast> BindBank()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objacntmast = new List<AcntMast>();

                objacntmast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 4
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objacntmast;
            }
        }
        public List<ItemMast> BindItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> objItemMast = new List<ItemMast>();

                objItemMast = (from obj in db.ItemMasts
                               where obj.Status == true
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<ItemMast> BindItemNameUpdate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<ItemMast> objItemMast = new List<ItemMast>();

                objItemMast = (from obj in db.ItemMasts
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<tblTranType> BindTranType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblTranType> objTran = new List<tblTranType>();

                objTran = (from obj in db.tblTranTypes                               
                               select obj).ToList();

                return objTran;
            }
        }
        public IList BindSerialNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stckdetl in db.Stckdetls
                           where (stckdetl.Item_from == "PB" || stckdetl.Item_from == "O" || stckdetl.Item_from == "MR") && (stckdetl.Billed == false) && (stckdetl.Is_Issued == null || stckdetl.Is_Issued == false)
                           orderby stckdetl.SerialNo
                           select new
                           {
                               SerialNo = stckdetl.SerialNo,
                               SerlDetlIdno = stckdetl.SerlDetl_id,
                           })
                           .Union
                                (from IMP in db.tblItemMastPurs
                                 where IMP.ItemType == 2
                                 orderby IMP.Item_Name
                                 select new
                                 {
                                     SerialNo = IMP.Item_Name,
                                     SerlDetlIdno = IMP.Item_Idno
                                 }).ToList();
                return lst;
            }
        }

        // BY LOKESH FOR RUNNING STOCK REPT FOR TYRE (Item_from-- LM)
        public IList BindSerialNoForItemFromLM()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from stckdetl in db.Stckdetls
                           where (stckdetl.Item_from == "LM") && (stckdetl.Billed == false || stckdetl.Billed == null) && (stckdetl.Is_Issued == null || stckdetl.Is_Issued == false)
                           orderby stckdetl.SerialNo
                           select new
                           {
                               SerialNo = stckdetl.SerialNo,
                               SerlDetlIdno = stckdetl.SerlDetl_id,
                           }).ToList();
                return lst;
            }
        }

        public List<tblItemMastPur> BindPurchaseItemName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<tblItemMastPur> BindPurchaseItemNameNew()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               where obj.ItemType != 3 && obj.Status == true
                               orderby obj.Item_Name
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<tblItemMastPur> BindPurchaseItemName2(int Type)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> objItemMast = new List<tblItemMastPur>();

                objItemMast = (from obj in db.tblItemMastPurs
                               orderby obj.Item_Name
                               where obj.ItemType == Type && obj.Status == true
                               select obj).ToList();

                return objItemMast;
            }
        }
        public List<AcntMast> BindSender()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objAcntMast = new List<AcntMast>();

                objAcntMast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 2 && obj.INTERNAL == false && obj.Status == true
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objAcntMast;
            }
        }
        public List<AcntMast> BindAgent()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objAcntMast = new List<AcntMast>();

                objAcntMast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 8
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objAcntMast;
            }
        }

        public DataTable BindRcptType(string con)
        {
            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4,12) ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable FillBank(string con)
        {
            sqlSTR = @"Select isnull(acnt_name,'')as acnt_name from acntmast where acnt_type = 4  Order by acnt_Name";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable BindRcptTypeDel(Int32 intRcptTypeId, string con)
        {

            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN(3,4,12) and Acnt_Idno=" + intRcptTypeId + " ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public IList BindTruckNo()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Status == true
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        //Lorry_No = ((obj.Lorry_No.Length)>4)?((obj.Lorry_No).Substring(obj.Lorry_No.Length - 4, obj.Lorry_No.Length)) + 
                                        //((obj.Lorry_No).Substring(0, obj.Lorry_No.Length - 4)):obj.Lorry_No, Lorry_Idno = obj.Lorry_Idno
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList BindTruckNowithLastDigit()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Status == true
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        //Lorry_No = ((obj.Lorry_No.Length)>4)?((obj.Lorry_No).Substring(obj.Lorry_No.Length - 4, obj.Lorry_No.Length)) + 
                                        //((obj.Lorry_No).Substring(0, obj.Lorry_No.Length - 4)):obj.Lorry_No, Lorry_Idno = obj.Lorry_Idno
                                        Lorry_No = obj.Lorry_No.Length>4?obj.Lorry_No.Substring(obj.Lorry_No.Length-4)+"["+obj.Lorry_No+"]":obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }

        public IList BindTransportaion(Int64 Id)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objMiscMast = (from obj in db.tblMiscMasters
                                    where obj.Misc_Status == true && obj.Tran_Idno == Id
                                    orderby obj.Misc_Name
                                    select new
                                    {
                                        //Lorry_No = ((obj.Lorry_No.Length)>4)?((obj.Lorry_No).Substring(obj.Lorry_No.Length - 4, obj.Lorry_No.Length)) + 
                                        //((obj.Lorry_No).Substring(0, obj.Lorry_No.Length - 4)):obj.Lorry_No, Lorry_Idno = obj.Lorry_Idno
                                        Misc_Name = obj.Misc_Name,
                                        Misc_Idno = obj.Misc_Idno
                                    }).ToList();
                return objMiscMast;
            }
        }

        public IList BindTruckNoPopulate()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public IList BindTruckNoPurchase()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {

                var objLorryMast = (from obj in db.LorryMasts
                                    where obj.Status == true && obj.Lorry_Type == 0
                                    orderby obj.Lorry_No
                                    select new
                                    {
                                        //Lorry_No = ((obj.Lorry_No.Length)>4)?((obj.Lorry_No).Substring(obj.Lorry_No.Length - 4, obj.Lorry_No.Length)) + 
                                        //((obj.Lorry_No).Substring(0, obj.Lorry_No.Length - 4)):obj.Lorry_No, Lorry_Idno = obj.Lorry_Idno
                                        Lorry_No = obj.Lorry_No,
                                        Lorry_Idno = obj.Lorry_Idno
                                    }).ToList();
                return objLorryMast;
            }
        }
        public List<tblCityMaster> BindLocFrom()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    where obj.AsLocation == true
                                    && obj.Status == true
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public List<tblCityMaster> BindLocFromByUserId(Int64 UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from frmcity in db.tblFrmCityDetls
                                    join cityMst in db.tblCityMasters on frmcity.FrmCity_Idno equals cityMst.City_Idno
                                    where frmcity.User_Idno == UserId && cityMst.AsLocation == true
                                    orderby cityMst.City_Name
                                    select cityMst).ToList();
                return objtblCityMaster;
            }
        }
        public List<tblCityMaster> BindAllToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }
        public List<tblStateMaster> BindState()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblStateMaster> objStateMasterList = new List<tblStateMaster>();

                objStateMasterList = (from obj in db.tblStateMasters
                                      where obj.Status == true
                                      orderby obj.State_Name
                                      select obj).ToList();

                return objStateMasterList;
            }
        }
        public List<tblCityMaster> BindCity(Int64 intStateIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objCityMasterList = new List<tblCityMaster>();

                objCityMasterList = (from obj in db.tblCityMasters
                                     where obj.Status == true && obj.State_Idno == intStateIdno
                                     orderby obj.City_Name
                                     select obj).ToList();

                return objCityMasterList;
            }
        }
        public List<tblLocMast> BindLocation(int LocArea)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblLocMast> objLocMastList = new List<tblLocMast>();

                objLocMastList = (from obj in db.tblLocMasts
                                  where obj.Status == true
                                  orderby obj.Loc_Name
                                  select obj).ToList();

                if (LocArea > 0)
                {
                    objLocMastList = (from o in objLocMastList where o.Loc_Idno == LocArea select o).ToList();
                }
                return objLocMastList;
            }
        }
        public List<tblIGrpMast> BindGroup()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblIGrpMast> objtblIGrpMastList = new List<tblIGrpMast>();

                objtblIGrpMastList = (from obj in db.tblIGrpMasts
                                      orderby obj.IGrp_Name
                                      select obj).ToList();

                return objtblIGrpMastList;
            }
        }

        //public List<AcntMast> BindGeneral(int Comp)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        List<AcntMast> objAcntMastList = new List<AcntMast>();

        //        objAcntMastList = (from obj in db.AcntMasts
        //                           orderby obj.Acnt_Name
        //                           where obj.Acnt_Type == 1 && obj.Comp_Idno == Comp
        //                           select obj).ToList();

        //        return objAcntMastList;
        //    }
        //}

        //public List<AcntMast> BindSupplier(int Comp)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        List<AcntMast> objAcntMastList = new List<AcntMast>();

        //        objAcntMastList = (from obj in db.AcntMasts
        //                           orderby obj.Acnt_Name
        //                           where obj.Acnt_Type == 2 && obj.Comp_Idno == Comp
        //                           select obj).ToList();

        //        return objAcntMastList;
        //    }
        //}

        //public List<USER> BindSupervisor()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        List<USER> objUserList = new List<USER>();

        //        objUserList = (from obj in db.USERS
        //                       orderby obj.Name
        //                       where obj.Desig_Idno == 4
        //                       select obj).ToList();

        //        return objUserList;
        //    }
        //}

        public List<tblDesignation> BindDesignation()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblDesignation> objDesignationList = new List<tblDesignation>();
                objDesignationList = (from obj in db.tblDesignations
                                      orderby obj.Desig_Name
                                      select obj).ToList();

                return objDesignationList;
            }
        }

        //public IList SelectModel()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        return (from IM in db.ItemMasts orderby IM.Item_Modl select new { Item_Modl = IM.Item_Modl, Id = IM.Item_Idno }).ToList();
        //    }
        //}

        //public List<DSAMast> BindTransport(int MasterType)
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        var lst = (from dsa in db.DSAMasts
        //                   where dsa.Master_Type == MasterType
        //                   select dsa).ToList();
        //        return lst;
        //    }
        //}

        //public IList BindFinancer()
        //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
        //    {
        //        return (from am in db.AcntMasts
        //                where am.Acnt_Type == 4 || am.Acnt_Type == 5 && am.InterNal == false
        //                orderby am.Acnt_Name
        //                select new
        //                {
        //                    Acnt_Idno = am.Acnt_Idno,
        //                    Acnt_Name = am.Acnt_Name
        //                }
        //                ).ToList();
        //    }
        //}

        //public IList BindFinancer(ArrayList acntType)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = db.AcntMasts.AsEnumerable().Where(am => acntType.Contains(am.Acnt_Type) && am.Status == true).OrderBy(am => am.Acnt_Name).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
        //        return lst;

        //    }
        //}

        //public IList BindScheme()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from dsa in db.DSAMasts
        //                where dsa.Master_Type == 1
        //                select new
        //                {
        //                    dsa.Dsa_Idno,
        //                    dsa.DSA_Name
        //                }
        //                    ).ToList();
        //    }
        //}

        //public IList BindExecutive()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from u in db.USERS
        //                where u.Desig_Idno == 2
        //                select new
        //                {
        //                    ExecutiveId = u.Emp_Idno,
        //                    ExecutiveName = u.Name

        //                }
        //                    ).ToList();
        //    }
        //}

        //public IList BindParty()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from am in db.AcntMasts
        //                join ah in db.AcntHeads on am.AGrp_Idno equals ah.AHead_Idno
        //                where (ah.AHead_Type == "SC" || ah.AHead_Type == "SD") && am.Acnt_Type != 1
        //                select new
        //                {
        //                    Acnt_Idno = am.Acnt_Idno,
        //                    Acnt_Name = am.Acnt_Name
        //                }
        //                ).ToList();
        //    }
        //}

        //public IList BindParty(ArrayList AcntType)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = db.AcntMasts.AsEnumerable().Where(am => AcntType.Contains(am.Acnt_Type)).OrderBy(am => am.Acnt_Name).Select(am => new { am.Acnt_Name, am.Acnt_Idno }).ToList();
        //        return lst;
        //    }
        //}

        //public List<AcntMast> BindCashAccount()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        List<AcntMast> objAcntMastList = new List<AcntMast>();

        //        objAcntMastList = (from obj in db.AcntMasts
        //                           where obj.Status == true
        //                           && obj.Acnt_Type == 3
        //                           orderby obj.Acnt_Name
        //                           select obj).ToList();

        //        return objAcntMastList;
        //    }
        //}

        //public List<DSAMast> BindDSA(int intMasterType)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        List<DSAMast> objDSAMastList = new List<DSAMast>();

        //        objDSAMastList = (from obj in db.DSAMasts
        //                          where obj.Master_Type == intMasterType && obj.DSA_Status == true
        //                          orderby obj.DSA_Name
        //                          select obj).ToList();

        //        return objDSAMastList;
        //    }
        //}

        //public IList BindChasisStckDetl(bool Billed)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        if (Billed == true)
        //        {
        //            return db.StckDetls.Where(stck => stck.Billed == true && stck.Chasis_No != "").OrderBy(stck => stck.Chasis_No).Select(stck => new { stck.Chasis_No, stck.Item_Idno }).ToList();
        //        }
        //        else
        //        {
        //            return db.StckDetls.Where(stck => stck.Billed == false && stck.Chasis_No != "").OrderBy(stck => stck.Chasis_No).Select(stck => new { stck.Chasis_No, stck.Item_Idno }).ToList();
        //        }
        //    }
        //}

        //public IList BindMachanic()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from usr in db.USERS
        //                   join log in db.logins on usr.Emp_Idno equals log.EmpId
        //                   where usr.Desig_Idno == 3 && log.IsActive == true
        //                   orderby usr.Name
        //                   select new
        //                   {
        //                       usr.Name,
        //                       usr.Emp_Idno
        //                   }).ToList();
        //        return lst;
        //    }
        //}
        //public DataSet BindHHPPNo()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        string str = "SELECT CHASIS_NO,StckDetl_Idno FROM STCKDETL WHERE  (BILLED = 1 AND S_TYP <> '' AND ITEM_FROM <> 'JO') OR (ITEM_FROM = 'JO') ORDER BY CHASIS_NO";
        //        //var lst=(from s in db.StckDetls where (s.Billed=1 && s.S_Typ !='' && s.Item_From <>'JO')||(s.Item_From=
        //        DataSet ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, str);
        //        if (ds != null)
        //        {

        //        }
        //        return ds;
        //    }
        //}

        //public IList BindItem(int ItemType)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return db.ItemMasts.Where(im => im.Item_Type == ItemType && im.Status == true).Select(im => new { im.Item_Modl, im.Item_Idno }).ToList();
        //    }
        //}

        //public IList BindSE(int DesignationId)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return db.USERS.Where(usr => usr.Desig_Idno == DesignationId && usr.Status == true).Select(usr => new { usr.Name, usr.Emp_Idno }).ToList();
        //    }
        //}

        //public IList BindExecutive(int DesignationId)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        return (from u in db.USERS
        //                where u.Desig_Idno == DesignationId
        //                select new
        //                {
        //                    ExecutiveId = u.Emp_Idno,
        //                    ExecutiveName = u.Name

        //                }
        //                    ).ToList();
        //    }
        //}
        //public List<USER> BindEmployee(int intDesignationID)
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        List<USER> objEmployeeMasterList = new List<USER>();

        //        objEmployeeMasterList = (from obj in db.USERS
        //                                 where obj.Desig_Idno == intDesignationID
        //                                 orderby obj.Name
        //                                 select obj).ToList();

        //        return objEmployeeMasterList;
        //    }
        //}

        //public IList BindTehsil()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from PM in db.PostMasts
        //                   join PDT in db.PostDistTehTypes on PM.Type_Idno equals PDT.Type_Idno
        //                   where PM.Type_Idno == 2
        //                   orderby PM.Post_Name
        //                   select new { Name = PM.Post_Name, Post_Idno = PM.Post_Idno }).ToList();
        //        return lst;
        //    }
        //}

        //public IList BindDistrict()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from PM in db.PostMasts
        //                   join PDT in db.PostDistTehTypes on PM.Type_Idno equals PDT.Type_Idno
        //                   where PM.Type_Idno == 3
        //                   orderby PM.Post_Name
        //                   select new { Name = PM.Post_Name, Post_Idno = PM.Post_Idno }).ToList();
        //        return lst;
        //    }
        //}

        //public IList BindPost()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from PM in db.PostMasts
        //                   join PDT in db.PostDistTehTypes on PM.Type_Idno equals PDT.Type_Idno
        //                   where PM.Type_Idno == 1
        //                   orderby PM.Post_Name
        //                   select new { Name = PM.Post_Name, Post_Idno = PM.Post_Idno }).ToList();
        //        return lst;
        //    }
        //}

        //public IList BindCityArea()
        //{
        //    using (TransportMandiEntities db = new TransportMandiEntities())
        //    {
        //        var lst = (from PM in db.PostMasts
        //                   join PDT in db.PostDistTehTypes on PM.Type_Idno equals PDT.Type_Idno
        //                   where PM.Type_Idno == 4
        //                   orderby PM.Post_Name
        //                   select new { Name = PM.Post_Name, Post_Idno = PM.Post_Idno }).ToList();
        //        return lst;
        //    }
        //}

        public List<UOMMast> BindUnitName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<UOMMast> objUOMMast = new List<UOMMast>();

                objUOMMast = (from obj in db.UOMMasts
                              orderby obj.UOM_Name
                              select obj).ToList();

                return objUOMMast;
            }
        }
        public List<AcntMast> BindTranspoter()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objAcntMast = new List<AcntMast>();

                objAcntMast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 5
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objAcntMast;
            }
        }
        public IList BindCityUserWise(Int64 UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var city = (from frmcity in db.tblFrmCityDetls
                            join cityMst in db.tblCityMasters on frmcity.FrmCity_Idno equals cityMst.City_Idno
                            where frmcity.User_Idno == UserId && cityMst.AsLocation == true
                            select new
                            {
                                CityName = cityMst.City_Name,
                                CityIdno = cityMst.City_Idno,
                                UserIdno = frmcity.User_Idno
                            }).ToList();
                return city;
            }
        }
        public List<AcntMast> BindParty()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> objAcntMast = new List<AcntMast>();

                objAcntMast = (from obj in db.AcntMasts
                               where obj.Acnt_Type == 2
                               orderby obj.Acnt_Name
                               select obj).ToList();

                return objAcntMast;
            }
        }
        public DataTable BindPartyForSale(string con)
        {

            sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN (2,6,11) and Status=1 ORDER BY ACNT_NAME";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        //public DataTable BindPartyforClaim(string con)
        //{

        //    sqlSTR = @"SELECT ACNT_NAME,Acnt_Idno,ACNT_TYPE FROM ACNTMAST WHERE ACNT_TYPE IN (11) and Status=1 ORDER BY ACNT_NAME";
        //    DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, sqlSTR);
        //    DataTable dt = new DataTable();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //    return dt;
        //}


        public List<AcntMast> SelectCompanyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.ASubGrp_Idno == 2  orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<AcntMast> SelectPartyName()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.Acnt_Type == 2 orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<AcntMast> SelectPartyNameOwn()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where LM.Acnt_Type == 6 orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public List<AcntMast> SelectPartyFill()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<AcntMast> lst = null;
                lst = (from LM in db.AcntMasts where (LM.Acnt_Type == 2 || LM.Acnt_Type == 3) orderby LM.Acnt_Name ascending select LM).ToList();
                return lst;
            }
        }

        public IList BindTyreType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from TC in db.tblTyreCategories
                           where TC.Internal == true
                           orderby TC.TyreType_Name
                           select new
                           {
                               TyreType = TC.TyreType_Name,
                               TyreTypeIdno = TC.TyreType_IdNo,
                           }).ToList();
                return lst;
            }
        }

        public List<tblPositionMast> SelectTyrePostion()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblPositionMast> lst = null;
                lst = (from LM in db.tblPositionMasts orderby LM.Position_name ascending select LM).ToList();
                return lst;
            }
        }

        public Int64 CheckSerialNo(string srtSerialNo)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                Int64 Value = 0;
                Value = (from tbl in db.Stckdetls where tbl.SerialNo == srtSerialNo select tbl.SerlDetl_id).Count();
                if(Value!=null && Value>0)
                {
                    Value = 1;
                }
                return Value;
            }
        }
        public List<tblItemMastPur> SelectOnlyTyre()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblItemMastPur> lst = null;
                lst = (from ITM in db.tblItemMastPurs where ITM.ItemType == 1 orderby ITM.Item_Name ascending select ITM).ToList();
                return lst;
            }
        }
        public List<tblTyreCategory> SelectTyreType()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblTyreCategory> lst = null;
                lst = (from tbl in db.tblTyreCategories where tbl.Internal == true orderby tbl.TyreType_Name ascending select tbl).ToList();
                return lst;
            }
        }

        public tblUserDefault SelectDefault(Int64 UserIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblUserDefault lst = (from tbl in db.tblUserDefaults where tbl.User_idno == UserIdno select tbl).SingleOrDefault();
                return lst;
            }
        }

        public DataSet GetLorryDetails(string conString, string Action, Int32 LorryIdno, string ChlnDate)
        {
            SqlParameter[] objSqlPara = new SqlParameter[3];
            objSqlPara[0] = new SqlParameter("@Action", Action);
            objSqlPara[1] = new SqlParameter("@LorryIdno", LorryIdno);
            objSqlPara[2] = new SqlParameter("@ChlnDate", ChlnDate);
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "spChlnBookng", objSqlPara);
            return objDataSet;
        }

        public DataSet GetGrChlnNotConfirm(string conString)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", "ChallanNotConfirm");
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "DashboardReports", objSqlPara);
            return objDataSet;
        }

        public DataSet GetOutstandingRepPartyWise(string conString)
        {
            SqlParameter[] objSqlPara = new SqlParameter[1];
            objSqlPara[0] = new SqlParameter("@Action", "OutstandingReportPartyWise");
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "DashboardReports", objSqlPara);
            return objDataSet;
        }

        public tblFleetAcntLink SelectAcntLink()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblFleetAcntLink AcntLink = (from UP in db.tblFleetAcntLinks select UP).FirstOrDefault();
                return AcntLink;
            }
        }

    }
}