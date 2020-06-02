using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class ContainerTypeMasterDAL
    {
        #region Select Events...


        /// <summary>
        /// Fill Grid
        /// </summary>
        /// <returns></returns>
        public IList SelectAll(string strConTypeName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblContainerTypes
                           orderby mast.Container_Type ascending
                           select new
                           {
                               // Item_Name = loc.Item_Name,
                               Con_Type = mast.Container_Type,
                               ConType_Idno = mast.ContainerType_Idno,
                               Status = mast.Status
                           }).ToList();
                if (strConTypeName != "")
                {
                    lst = (from I in lst where I.Con_Type.ToLower().Contains(strConTypeName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select From Id
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public tblContainerType SelectById(int intConTypeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblContainerTypes
                        where mast.ContainerType_Idno == intConTypeIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To Check data Already Exist or Not
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strConType, int intConTypeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblContainerType objConTypeMast = new tblContainerType();
                if (intConTypeIdno <= 0)
                {
                    objConTypeMast = (from mast in db.tblContainerTypes
                                      where mast.Container_Type == strConType
                                   select mast).FirstOrDefault();
                }
                else if (intConTypeIdno > 0)
                {
                    objConTypeMast = (from mast in db.tblContainerTypes
                                      where mast.Container_Type == strConType
                                         && mast.ContainerType_Idno != intConTypeIdno
                                   select mast).FirstOrDefault();
                }
                if (objConTypeMast != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        #endregion

        #region InsertUpdateDelete Events...
        /// <summary>
        /// Insert records in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public Int64 Insert(string strConType,bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerType objConTypeMast = new tblContainerType();
                    objConTypeMast.Emp_Idno = empIdno;
                    objConTypeMast.Container_Type = strConType;
                    objConTypeMast.Status = bStatus;
                    objConTypeMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strConType, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblContainerTypes.AddObject(objConTypeMast);
                        db.SaveChanges();
                        intValue = objConTypeMast.ContainerType_Idno;
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        /// <summary>
        /// To Update Record in tblIGrpMast
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGrpType"></param>
        /// <param name="bStatus"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Update(string strConType, bool bStatus, int intConTypeIdno, Int32 empIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerType objConTypeMast = (from mast in db.tblContainerTypes
                                                       where mast.ContainerType_Idno == intConTypeIdno
                                                       select mast).FirstOrDefault();
                    if (objConTypeMast != null)
                    {
                        objConTypeMast.Emp_Idno = empIdno;
                        objConTypeMast.Container_Type = strConType;
                        objConTypeMast.Status = bStatus;
                        objConTypeMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strConType, intConTypeIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intConTypeIdno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ApplicationFunction.ErrorLog(ex.ToString());
            }
            return intValue;
        }

        /// <summary>
        ///  Delete Record from tblIGrpMast
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public int Delete(int intConTypeIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerType objConTypeMast = (from mast in db.tblContainerTypes
                                                       where mast.ContainerType_Idno == intConTypeIdno
                                               select mast).FirstOrDefault();
                    if (objConTypeMast != null)
                    {
                        db.tblContainerTypes.DeleteObject(objConTypeMast);
                        db.SaveChanges();
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

        public Int32 UpdateStatus(int intConTypeIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerType objConTypeMast = (from mast in db.tblContainerTypes
                                                       where mast.ContainerType_Idno == intConTypeIdno
                                               select mast).FirstOrDefault();
                    if (objConTypeMast != null)
                    {
                        objConTypeMast.Emp_Idno = empIdno;
                        objConTypeMast.Status = Status;
                        objConTypeMast.Date_Modified = System.DateTime.Now;
                        db.SaveChanges();
                        value = 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
        #endregion


        public IList CheckItemGrpExistInItemMaster(Int32 ConTypeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.TblGrHeads
                        where obj.GRContanr_Type == ConTypeIdno
                        select new
                        {
                            ItemGrp = obj.GRContanr_Type
                        }
                        ).ToList()
                        .Union
                       (from obj1 in db.tblAdvGrOrders
                        where obj1.Contanr_Type == ConTypeIdno
                        select new
                        {
                            ItemGrp = obj1.Contanr_Type
                        }).ToList();
            }
        }

        public Int64 SelectTotal()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblContainerTypes
                           orderby mast.Container_Type ascending
                           select new
                           {
                               mast
                           }).Count();

                return lst;
            }
        }
    }
}