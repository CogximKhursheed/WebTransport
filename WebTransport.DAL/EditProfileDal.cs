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

namespace WebTransport.DAL
{
    public class EditProfileDal
    {


        public List<tblUserMast> Select()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from u in db.tblUserMasts
                           //where u.Status == true && u.DOJ != null && u.Admin_Idno == 0
                           where u.Status == true && u.DOJ != null && u.User_Idno == 0
                           orderby u.User_Name
                           select u).ToList();

                return lst;
            }
        }

        public List<tblCityMaster> BindToCity()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                List<tblCityMaster> objtblCityMaster = new List<tblCityMaster>();
                objtblCityMaster = (from obj in db.tblCityMasters
                                    where obj.AsLocation == true
                                    orderby obj.City_Name
                                    select obj).ToList();
                return objtblCityMaster;
            }
        }

        public tblUserMast Select(int UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var tbluser = (from user in db.tblUserMasts where user.User_Idno == UserId select user).SingleOrDefault();
                return tbluser;
            }
         }
        public IList SelectFromCity(int UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblFrmCityDetls
                           join CM in db.tblCityMasters on obj.FrmCity_Idno equals CM.City_Idno
                           where obj.User_Idno == UserId
                           select
                               new
                               {
                                   CM.City_Name,
                                   CM.City_Idno
                               }
                         ).ToList();
                      return lst;
                   
            }
        }
        public IList<tblFrmCityDetl> SelectFromCityIdno(int UserId)
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                var lst = (from obj in db.tblFrmCityDetls
                           join CM in db.tblCityMasters on obj.FrmCity_Idno equals CM.City_Idno
                           where obj.User_Idno == UserId
                           select obj
                              
                         ).ToList();
                return lst;

            }
        }
    }
    
}
