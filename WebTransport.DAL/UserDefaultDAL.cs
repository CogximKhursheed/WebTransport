using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;


namespace WebTransport.DAL
{
  public class UserDefaultDAL
    {
      public tblUserDefault SelectExistRecord(Int64 UserId)
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              var obj = (from U in db.tblUserDefaults where U.User_idno == UserId select U).SingleOrDefault();
              return obj;
          }
      }
      public tblUserDefault SelectById(Int64 intUserIdno)
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              return (from dm in db.tblUserDefaults where dm.User_idno == intUserIdno select dm).FirstOrDefault();
          }
      }

      public IList<tblUserMast> SelectALLUsers()
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              var lst = (from u in db.tblUserMasts where u.Status == true select u).ToList();
              return lst;
          }
      }

      public IList<tblUserMast> SelectOnlyUsers()
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              var lst = (from u in db.tblUserMasts where u.Status == true && u.Class!="Admin" select u).ToList();
              return lst;
          }
      }

      public List<tblCityMaster> BindFromCity()
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              List<tblCityMaster> lst = null;
              lst = (from cm in db.tblCityMasters orderby cm.City_Name ascending select cm).ToList();
              return lst;
          }
      }
      //---------------------------------
      public Int64 Insert(Int64 User_idno, Int64 FromCity_idno, Int64 yearidno, Int64 cityid, Int64 stateid, Int64 Sender, Int64 Item, Int64 unit, Int32 STax_Typ,Int64 Gr_Typ)
      {
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
              UserDefaultDAL objDAL = new UserDefaultDAL();
              Int64 Value = 0;
              tblUserDefault objUsrDefault = new tblUserDefault();
              objUsrDefault.User_idno = User_idno;
              objUsrDefault.FromCity_idno = FromCity_idno;
              objUsrDefault.Year_idno = yearidno;
              objUsrDefault.CityId = cityid;
              objUsrDefault.StateId = stateid;
              objUsrDefault.Date_Added = MultipleDBDAL.GetIndianDateTime().Date;
              objUsrDefault.SenderIdno = Sender;
              objUsrDefault.ItemIdno = Item;
              objUsrDefault.UnitIdno = unit;
              objUsrDefault.STax_Typ = STax_Typ;
              objUsrDefault.Gr_Type = Gr_Typ;
              db.AddTotblUserDefaults(objUsrDefault);
              db.SaveChanges();
              Value = objUsrDefault.UserDefault_Idno;
              objDAL = null;
              return Value;
          }
      }

      public Int64 Update(Int64 User_idno, Int64 FromCity_idno, Int64 yearidno, Int64 UsrDefltID, Int64 cityid, Int64 stateid, Int64 Sender, Int64 Item, Int64 unit, Int32 STax_Typ, Int64 Gr_Typ)
      {
          Int64 Value= 0;
          using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
          {
                           
              tblUserDefault objHead = (from UD in db.tblUserDefaults
                                        where UD.UserDefault_Idno == UsrDefltID
                                        select UD).SingleOrDefault();

              if (objHead != null)
              {
                  objHead.FromCity_idno = FromCity_idno;
                  objHead.Year_idno = yearidno;
                  objHead.User_idno = User_idno;
                  objHead.StateId = stateid;
                  objHead.CityId = cityid;
                  objHead.SenderIdno = Sender;
                  objHead.ItemIdno = Item;
                  objHead.UnitIdno = unit;
                  objHead.Date_Modified = System.DateTime.Now;
                  objHead.STax_Typ = STax_Typ;
                  objHead.Gr_Type = Gr_Typ;
                  db.SaveChanges();
                  Value = objHead.UserDefault_Idno;
                 
              }
              return Value;
          }
      }

      //public List<tblUserDefault> SelectMultipleFromCity(int userIdno)
      //{
        //  using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
      //    {
      //        var LstComp = (from obj in db.tblCityMasters
      //                       join TD in db.tblWrkngCmpnyDetls on obj.Id equals TD.Comp_Idno
      //                       where obj.Status == true && TD.User_Idno == userIdno
      //                       orderby obj.WorkComp_Name
      //                       select obj).ToList();
      //        return LstComp;
      //    }
      //}
    }
}
