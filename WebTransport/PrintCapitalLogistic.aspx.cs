using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTransport
{
    public partial class PrintCapitalLogistic : System.Web.UI.Page
    {
        string CapAdd1 = String.Empty;
        string CapAdd2 = String.Empty;
        string CapAdd3 = String.Empty;
        string CapVehNo = String.Empty;
        string CapMtQty = String.Empty;
        string CapBags = String.Empty;
        string CapGSTINo = String.Empty;
        string CapTotAmnt = String.Empty;
        string CapOrderNo = String.Empty;
        string CapInvNo = String.Empty;
        string CapGrDate = String.Empty;
        string CapFromCity = String.Empty;
        string CapToCity = String.Empty;
        string CapConsigneeName = String.Empty;
        string CapGRNo = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CapTotAmnt"] != null)
            {
                CapTotAmnt = Request.QueryString["CapTotAmnt"];
                CapOrderNo = Request.QueryString["CapOrderNo"];
                CapGrDate = Request.QueryString["CapGrDate"];
                CapConsigneeName = Request.QueryString["CapConsigneeName"];
                CapFromCity = Request.QueryString["CapFromCity"];
                CapToCity = Request.QueryString["CapToCity"];
                CapAdd1 = Request.QueryString["CapAdd1"];
                CapAdd2 = Request.QueryString["CapAdd2"];
                CapAdd3 = Request.QueryString["CapAdd3"];
                CapGSTINo = Request.QueryString["CapGSTINo"];
                CapInvNo = Request.QueryString["CapInvNo"];
                CapVehNo = Request.QueryString["CapVehNo"];
                CapMtQty = Request.QueryString["CapMtQty"];
                CapBags = Request.QueryString["CapBags"];
                CapGRNo = Request.QueryString["CapGRNo"];

                lblTotAmnt2.Text = CapTotAmnt;
                lblOrderNo.Text = CapOrderNo;
                lblGrDate.Text = CapGrDate;
                lblConsigneeName.Text = CapConsigneeName;
                lblFromCity.Text = CapFromCity;
                lblToCity.Text = CapToCity;
                lblAdd1.Text = CapAdd1;
                lblAdd2.Text = CapAdd2;
                lblAdd3.Text = CapAdd3;
                if (CapGSTINo != "") lblGSTINNo.Text = "GST No.: " + CapGSTINo;
                lblInvNo.Text = CapInvNo;
                lblVehNo.Text = CapVehNo;
                lblMTQty.Text = CapMtQty;
                lblBags.Text = CapBags;
                lblGRNo.Text = CapGRNo;
            }
        }
    }
}