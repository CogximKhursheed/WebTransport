using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class Menus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

              //  this.GetAllUser();
            }
           // lblindiandatetime.Text = ApplicationFunction.GetIndianDateTime().ToString("dd-MMM-yyyy");
        }

        private void GetAllUser()
        {
            // userBLL = new UserBLL();
            //var lst = userBLL.SelectforBirthday();
            //userBLL = null;
            //DateTime indianDateTime = ApplicationFunction.GetIndianDateTime();  ///new code for indian standard time
            //var doblst = (from u in lst
            //              where Convert.ToDateTime(u.DOB).Month == indianDateTime.Month
            //                  && Convert.ToDateTime(u.DOB).Day == indianDateTime.Day
            //                  && u.Is_Active == true
            //              select u).ToList();

            //rptdob.DataSource = doblst;
            //rptdob.DataBind();
            //if (doblst.Count > 0)
            //{
            //    lblbirthday.Text = "Birthday";
            //}
            //else
            //{
            //    divBirthday.Visible = false;
            //}

            //var doalst = (from u in lst
            //              where Convert.ToString(u.DOA) != string.Empty && Convert.ToDateTime(u.DOA).Month == indianDateTime.Month
            //                  && Convert.ToDateTime(u.DOA).Day == indianDateTime.Day
            //                  && u.Is_Active == true
            //              select u).ToList();
            //rptdoa.DataSource = doalst;
            //rptdoa.DataBind();
            //if (doalst.Count > 0)
            //{
            //    lblaniversary.Text = "Aniversary";
            //}
            //else
            //{
            //    divAnivarsary.Visible = false;
            //}


            //List<CompletionYear> complyear = new List<CompletionYear>();

            //foreach (var user in lst)
            //{
            //    //int year = DateTime.Now.Date.Year - Convert.ToDateTime(user.DOJ).Year;
            //    int year = indianDateTime.Year - Convert.ToDateTime(user.DOJ).Year;
            //    int m = Convert.ToDateTime(user.DOJ).Month;
            //    int d = Convert.ToDateTime(user.DOJ).Day;
            //    int y = (Convert.ToDateTime(user.DOJ).Year) + year;
            //    //if (DateTime.Now.Year == y && DateTime.Now.Month == m && DateTime.Now.Day == d)
            //    if (indianDateTime.Year == y && indianDateTime.Month == m && indianDateTime.Day == d)
            //    {
            //        CompletionYear cy = new CompletionYear();
            //        cy.User_Name = user.User_Name;
            //        cy.Message = " : Years[" + year + "]";
            //        complyear.Add(cy);
            //        cy = null;
            //    }
            //}
            //if (complyear.Count > 0)
            //{
            //    lblCompletionYearmyd.Text = "Completion Year:";
            //    rptcymyd.DataSource = complyear;
            //    rptcymyd.DataBind();
            //}

            //if (doblst.Count <= 0 && doalst.Count <= 0 && complyear.Count <= 0)
            //{
            //    dvDOBDOA.Visible = false;
            //}
        }
       
    }
}