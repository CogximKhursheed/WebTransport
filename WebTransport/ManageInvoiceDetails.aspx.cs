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
using System.IO;
using System.Text;
using System.Xml;

namespace WebTransport
{
    public partial class ManageInvoiceDetails : Pagebase
    {
        #region Private Variable....
        private int intFormId = 31; double dblTInvoiceAmnt = 0;
        string ar;
        // string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            //txtReceiptNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
            txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            InvoiceDAL objInvoiceDAL = new InvoiceDAL();
            tblUserPref obj = objInvoiceDAL.SelectUserPref();
            hidAdminApp.Value = Convert.ToString(obj.AdminApp_Inv);
            Session["InvoiceNo"] = txtReceiptNo.Text;

            //if (Convert.ToBoolean(hidAdminApp.Value) == true)
            //{
            //    if (Convert.ToString(Session["Userclass"]) == "Admin")
            //    {
            //        grdMain.Columns[7].Visible = true;
            //    }
            //    else
            //    {
            //        grdMain.Columns[7].Visible = false;
            //    }
            //}
            //else
            //{
            //    grdMain.Columns[7].Visible = false;
            //}
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                ddlFromCity.SelectedValue = Convert.ToString(base.UserFromCity);
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
                this.CountTotalRecords();
            }
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
        #endregion

        #region Functions...
        private void CountTotalRecords()
        {
            InvoiceDAL obj = new InvoiceDAL();

            lblTotalRecord.Text = "T. Record (s): " + obj.TotalRecords().ToString();
        }

        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            ddlFromCity.DataSource = FrmCity;
            ddlFromCity.DataTextField = "CityName";
            ddlFromCity.DataValueField = "cityidno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindCity()
        {
            InvoiceDAL obj = new InvoiceDAL();
            var lst = obj.SelectCityCombo();
            obj = null;

            if (lst.Count > 0)
            {
                ddlFromCity.DataSource = lst;
                ddlFromCity.DataTextField = "City_Name";
                ddlFromCity.DataValueField = "City_Idno";
                ddlFromCity.DataBind();
                ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        private void BindGrid()
        {

            DataSet ds2 = new DataSet();
            DataSet dsMain = new DataSet();
            InvoiceDetailsDAL obj = new InvoiceDetailsDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
            }

            Int64 fromCity = Convert.ToInt64((ddlFromCity.SelectedIndex != -1) ? (ddlFromCity.SelectedValue) : "0");
            Int32 year_idno = Convert.ToInt32(ddldateRange.SelectedValue);
            String InvoiceNo = Convert.ToString(txtReceiptNo.Text);
            if (InvoiceNo.Contains("-"))
            {
                String value = InvoiceNo;
                var lowEnd = value.Split('-')[0];
                var highEnd = value.Split('-')[1];

                int first = Convert.ToInt32(lowEnd);
                int last = Convert.ToInt32(highEnd);
    
                for (int i = first; i <= last; i++)
                {
                    if (i == last)
                    {
                        ar += i;
                    }
                    else
                    {
                        var arr = (i + ", ");
                        ar += arr;
                    }
                }
 
                string val = ar;
                string[] Arr = val.Split(new char[] { ',' });
                for (int l = 0; l < Arr.Length; l++)
                {
                    ds2 = obj.SelectRep(year_idno, dtfrom, dtto, Arr[l], fromCity, ApplicationFunction.ConnectionString());
                    if (l == 0)
                        dsMain = ds2;
                    else
                        dsMain.Tables[0].Merge(ds2.Tables[0]);

                    grdMain.DataSource = dsMain.Tables[0];
                    grdMain.DataBind();

                    lblTotalRecord.Text = "T. Record (s): " + dsMain.Tables[0].Rows.Count;
                }

                lnkprint111.Visible = true;
               // imgBtnExcel.Visible = true;

                }
            else
            {
                string Value = InvoiceNo;
                string[] Array1 = Value.Split(new char[] { ',' });
                for (int i = 0; i < Array1.Length; i++)
                {
                    ds2 = obj.SelectRep(year_idno, dtfrom, dtto, Array1[i], fromCity, ApplicationFunction.ConnectionString());
                    if (i == 0)
                        dsMain = ds2;
                    else
                        dsMain.Tables[0].Merge(ds2.Tables[0]);

                    grdMain.DataSource = dsMain.Tables[0];
                    grdMain.DataBind();

                    lblTotalRecord.Text = "T. Record (s): " + dsMain.Tables[0].Rows.Count;
                }

                lnkprint111.Visible = true;
              //  imgBtnExcel.Visible = true;
            }
        }
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataSource = lst;
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
        }
        private void SetDate()
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    txtReceiptDatefrom.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    txtReceiptDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtReceiptDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtReceiptDateto.Text = Convert.ToString(hidmaxdate.Value);

                }
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice.xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in Dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in Dt.Rows)
            {
                str = "";
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("Invoice.aspx?q=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                InvoiceDetailsDAL obj1 = new InvoiceDetailsDAL();
                Int32 intValue = obj1.Delete(Convert.ToInt32(e.CommandArgument), UserIdno, ApplicationFunction.ConnectionString());
                obj1 = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtReceiptNo.Focus();
                }
                else
                {
                    if (intValue == -1)
                        strMsg = "Record can not be deleted. It is in use.";
                    else
                        strMsg = "Record not deleted.";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            }
            //else if (e.CommandName == "cmdstatus")
            //{
            //    int intCityIdno = 0;
            //    bool bStatus = false;
            //    string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
            //    if (strStatus.Length > 1)
            //    {
            //        intCityIdno = Convert.ToInt32(strStatus[0]);
            //        if (Convert.ToBoolean(strStatus[1]) == true)
            //            bStatus = false;
            //        else
            //            bStatus = true;
            //        CityMastDAL objclsCityMaster = new CityMastDAL();
            //        int value = objclsCityMaster.UpdateStatus(intCityIdno, bStatus);
            //        objclsCityMaster = null;
            //        if (value > 0)
            //        {
            //            this.BindGrid();
            //            strMsg = "Status updated successfully.";
            //            drpState.Focus();
            //        }
            //        else
            //        {
            //            strMsg = "Status not updated.";
            //        }
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
            //    }
            //}
            //drpState.Focus();
        }
    
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        protected void lnkinvoicePrint_Click(object sender, EventArgs e)
        {
            this.PrintInvoice();
        }
        private void PrintInvoice()
        {
            InvoiceDAL objDAL = new InvoiceDAL();
            Int64 iMaxInvIdno = objDAL.MaxIdno(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            Int64 icityIdno = objDAL.cityID(ApplicationFunction.ConnectionString(), Convert.ToInt64(ddlFromCity.SelectedValue));
            String InvoiceNo = txtReceiptNo.Text;

            if (String.IsNullOrEmpty(txtReceiptNo.Text) != true)
            {
                if (InvoiceNo.Contains("-"))
                {
                    String value = InvoiceNo;
                    var lowEnd = value.Split('-')[0];
                    var highEnd = value.Split('-')[1];

                    int first = Convert.ToInt32(lowEnd);
                    int last = Convert.ToInt32(highEnd);

                    for (int i = first; i <= last; i++)
                    {
                        if (i == last)
                        {
                            ar += i;
                        }
                        else
                        {
                            var arr = (i + ", ");
                            ar += arr;
                        }
                    }

                    string val = ar;
                    string url = "RptInvoiceDetail.aspx" + "?q=" + val + "&P=" + Convert.ToInt64(ddldateRange.SelectedValue) + "&R=" + icityIdno;
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else
                {
                    string url = "RptInvoiceDetail.aspx" + "?q=" + Convert.ToString(txtReceiptNo.Text) + "&P=" + Convert.ToInt64(ddldateRange.SelectedValue) + "&R=" + icityIdno;
                    string fullURL = "window.open('" + url + "', '_blank' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
            }
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }

            ddldateRange.Focus();
        }
        #endregion

        #region Control Events...
    
        #endregion
    }
}