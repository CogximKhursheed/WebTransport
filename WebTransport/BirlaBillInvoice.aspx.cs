using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Configuration;
using System.Transactions;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Xml;

namespace WebTransport
{
    public partial class BirlaBillInvoice : System.Web.UI.Page
    {
        DataTable dttemp = new DataTable();
        double dtotAmnt = 0; double Dtotqty = 0; double DTAMNT =  0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                //base.AutoRedirect();
            }

            try
            {
                PrintInvoice(Request.QueryString["q"].ToString().Replace("%2c", ","));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
            catch (Exception Exe)
            {

            }

        }
       
        private void PrintInvoice(String HeadIdno)
        {

            InvoiceDAL obj = new InvoiceDAL();
            Int64 inv = Convert.ToInt64(HeadIdno);
            Int64 chlnidno = obj.ChlnIdno(ApplicationFunction.ConnectionString(), inv);
            String chln = Convert.ToString(chlnidno);

            String CityName = Session["fromcity"] as string;
            dttemp = obj.city(ApplicationFunction.ConnectionString(), CityName);


            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string FaxNo = "";
            string GSTIN = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast A Left JOIN tblCITYMASTER CM On CM.city_idno=A.city_idno Left join tblStateMaster SM ON SM.state_idno=A.state_idno");
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='SelectPrint1',@Id='" + HeadIdno + "'");
            DataSet TOLLinfo = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spInvGen] @ACTION='TOLLINFO',@Id='" + chln + "'");
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(dttemp.Rows[0]["Address1"]) + "," + Convert.ToString(dttemp.Rows[0]["Address2"]);
            PhNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Mobile_1"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Name"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Name"]) + "(CODE :-  " + Convert.ToString(dsReport.Tables[0].Rows[0]["GSTState_Code"]) + ")";
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            GSTIN = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblpin.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            lblCompname.Text = CompName; //lblcomp.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;

            lbladd2.Text = Add2;
            lblcity.Text = City;
            lblstate.Text = State;
            lblmobile.Text = PhNo;
            lblgstin.Text = GSTIN;
            lblpan.Text = PanNo.ToString();
            if (dsReport != null && dsReport.Tables[1].Rows.Count > 0)
            {
                Repeater1.DataSource = dsReport.Tables[1];
                Repeater1.DataBind();
            }
            if (TOLLinfo != null)
            {
                Repeater2.DataSource = TOLLinfo.Tables[0];
                Repeater2.DataBind();
            }
            lblunit.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["unit"]);
            lblbillno.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Inv_No"]);
            lblbilldate.Text = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["Inv_Date"]).ToString("dd-MM-yyyy"); 
            lblcontname.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_Name"]);
            lbladd1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address1"]);
            lblcadd2.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Address2"]);
            lblcity1.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["City_Name"]);
            lblst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["State_Name"]);
            lblgst.Text = Convert.ToString(dsReport.Tables[0].Rows[0]["Party_GSTINNo"]);
           Double T1 =  Convert.ToDouble(lbltotal.Text);
           Double T2 =  Convert.ToDouble(lbltot.Text);

           lblgrandtotal.Text = Convert.ToString((T1 + T2));
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dtotAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Dtotqty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lbltotal.Text = dtotAmnt.ToString("N2");
                lbltotqty.Text = Dtotqty.ToString("N2");
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DTAMNT += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Toll_Amt"));   
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                lbltot.Text = DTAMNT.ToString("N2");   
            }
        }
    }
}