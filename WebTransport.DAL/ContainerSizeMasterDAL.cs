using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
//using AutomobileOnline.Classes;

namespace WebTransport.DAL
{
    public class ContainerSizeMasterDAL
    {
        #region Select Events...


        /// <summary>
        /// Fill Grid
        /// </summary>
        /// <returns></returns>
        public IList SelectAll(string strConSizeName)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblContainerSizes
                           orderby mast.Container_Size ascending
                           select new
                           {
                               // Item_Name = loc.Item_Name,
                               Con_Size = mast.Container_Size,
                               ConSize_Idno = mast.ContainerSize_Idno,
                               Status = mast.Status
                           }).ToList();
                if (strConSizeName != "")
                {
                    lst = (from I in lst where I.Con_Size.ToLower().Contains(strConSizeName.ToLower()) select I).ToList();
                }
                return lst;
            }
        }

        /// <summary>
        /// Select From Id
        /// </summary>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public tblContainerSize SelectById(int intConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from mast in db.tblContainerSizes
                        where mast.ContainerSize_Idno == intConSizeIdno
                        select mast).FirstOrDefault();
            }
        }

        /// <summary>
        /// To Check data Already Exist or Not
        /// </summary>
        /// <param name="strIGroupName"></param>
        /// <param name="intIGropIdno"></param>
        /// <returns></returns>
        public bool IsExists(string strConSize, int intConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                tblContainerSize objConSizeMast = new tblContainerSize();
                if (intConSizeIdno <= 0)
                {
                    objConSizeMast = (from mast in db.tblContainerSizes
                                      where mast.Container_Size == strConSize
                                      select mast).FirstOrDefault();
                }
                else if (intConSizeIdno > 0)
                {
                    objConSizeMast = (from mast in db.tblContainerSizes
                                      where mast.Container_Size == strConSize
                                         && mast.ContainerSize_Idno != intConSizeIdno
                                      select mast).FirstOrDefault();
                }
                if (objConSizeMast != null)
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
        public Int64 Insert(string strConSize, bool bStatus, Int32 empIdno)
        {
            Int64 intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerSize objConSizeMast = new tblContainerSize();
                    objConSizeMast.Emp_Idno = empIdno;
                    objConSizeMast.Container_Size = strConSize;
                    objConSizeMast.Status = bStatus;
                    objConSizeMast.Date_Added = System.DateTime.Now;
                    if (IsExists(strConSize, 0) == true)
                    {
                        intValue = -1;
                    }
                    else
                    {
                        db.tblContainerSizes.AddObject(objConSizeMast);
                        db.SaveChanges();
                        intValue = objConSizeMast.ContainerSize_Idno;
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
        public int Update(string strConSize, bool bStatus, int intConSizeIdno, Int32 empIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerSize objConSizeMast = (from mast in db.tblContainerSizes
                                                       where mast.ContainerSize_Idno == intConSizeIdno
                                                       select mast).FirstOrDefault();
                    if (objConSizeMast != null)
                    {
                        objConSizeMast.Emp_Idno = empIdno;
                        objConSizeMast.Container_Size = strConSize;
                        objConSizeMast.Status = bStatus;
                        objConSizeMast.Date_Modified = System.DateTime.Now;
                        if (IsExists(strConSize, intConSizeIdno) == true)
                        {
                            intValue = -1;
                        }
                        else
                        {
                            db.SaveChanges();
                            intValue = intConSizeIdno;
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
        public int Delete(int intConSizeIdno)
        {
            int intValue = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerSize objConSizeMast = (from mast in db.tblContainerSizes
                                                       where mast.ContainerSize_Idno == intConSizeIdno
                                                       select mast).FirstOrDefault();
                    if (objConSizeMast != null)
                    {
                        db.tblContainerSizes.DeleteObject(objConSizeMast);
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

        public Int32 UpdateStatus(int intConSizeIdno, bool Status, Int32 empIdno)
        {
            int value = 0;
            try
            {
                using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
                {
                    tblContainerSize objConSizeMast = (from mast in db.tblContainerSizes
                                                       where mast.ContainerSize_Idno == intConSizeIdno
                                                       select mast).FirstOrDefault();
                    if (objConSizeMast != null)
                    {
                        objConSizeMast.Emp_Idno = empIdno;
                        objConSizeMast.Status = Status;
                        objConSizeMast.Date_Modified = System.DateTime.Now;
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


        public IList CheckItemGrpExistInGRPrep(Int64 ConSizeIdno)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                return (from obj in db.TblGrHeads
                        where obj.GRContanr_Size == ConSizeIdno
                        select new
                        {
                            ItemGrp = obj.GRContanr_Size
                        }
                        ).ToList()
                        .Union
                       (from obj1 in db.tblAdvGrOrders
                        where obj1.Contanr_Size == ConSizeIdno
                        select new
                        {
                            ItemGrp = obj1.Contanr_Size
                        }).ToList();
                      
            }
        }

        public Int64 SelectTotal()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from mast in db.tblContainerSizes
                           orderby mast.Container_Size ascending
                           select new
                           {
                               mast
                           }).Count();

                return lst;
            }
        }
    }
}