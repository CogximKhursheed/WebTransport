using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WebTransport.DAL;
using System.Collections.Generic;
using WebTransport.Classes;

namespace WebTransport
{
    public partial class ManageDeliveryRegister : Pagebase
    {
        #region Private Variable...
        string conString = string.Empty;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            SessionValues svalue = new SessionValues();
            //  base.UsessionValue = svalue.FetchUsersAndComp(string.IsNullOrEmpty(Convert.ToString(Session["Visageuseridno"])) ? 0 : Convert.ToInt32(Session["Visageuseridno"]));
            // conString = ConfigurationManager.ConnectionStrings["VisageConnectionString"].ToString();
            svalue = null;
            if (!Page.IsPostBack)
            {
                this.Title = "Manage Delivery Register";
                this.BindDateRange();
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                this.BidFromCity();
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                tblUserPref obj = objChallanDelverdDAL.selectUserPref();
                ddlTocity.SelectedValue = Convert.ToString(obj.BaseCity_Idno);
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                this.Countall();
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void btnTravellBillSearch_Click(object sender, EventArgs e)
        {
            Populate();
        }

        protected void btnResign_Click(object sender, EventArgs e)
        {
            //UserBLL userBLL = new UserBLL();
            //int value = userBLL.UpdateUserResign(Convert.ToInt32(hidstaffid.Value), Convert.ToDateTime(txtResignDate.Text), txtRemarks.Text);
            //userBLL = null;
            //if (value > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showmsg", "PassMessage('Record updated successfully!')", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showresign", "ShowModalPopup();", true);
            //}
        }

        #endregion

        #region Functions...
        public void Countall()
        {
            ChallanDelverdDAL obj = new ChallanDelverdDAL();
          
            Int64 count = obj.Countall();
            if (count > 0)
            {
                lbltotalstaff.Text = "T. Record (s):" + count;
            }
        }
        private void Populate()
        {
            string action = string.Empty;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            string transferno = ""; Int32 rcptno = 0;
            int yearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
            if (txtDateFrom.Text != "")
            {

                dateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text));
            }
            else
            {
                dateFrom = null;
            }
            if (txtDateTo.Text != "")
            {

                dateTo = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text));
            }
            else
            {
                dateTo = null;
            }
            transferno = Convert.ToString(txtMtrlTransfno.Text);
            rcptno = (Convert.ToString(txtRcptNo.Text) != "" ? Convert.ToInt32(txtRcptNo.Text) : 0);
            Int32 Fromcity = Convert.ToInt32(ddlTocity.SelectedValue);
            ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
            var lst = objChallanDelverdDAL.Search(dateFrom, dateTo, rcptno, transferno, Fromcity, yearIdno);
            objChallanDelverdDAL = null;
            if (lst != null && lst.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("DelvNo", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("ChallanNo", typeof(string));
                dt.Columns.Add("ChlnDate", typeof(string));


                double TNet = 0; double TAmnt = 0;
                for (int i = 0; i < lst.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lst[i], "ChlnDelv_Date")).ToString("dd-MM-yyyy");
                    dr["DelvNo"] = Convert.ToString(DataBinder.Eval(lst[i], "ChlnDelv_No"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lst[i], "City_Name"));
                    dr["ChallanNo"] = Convert.ToString(DataBinder.Eval(lst[i], "Chln_No"));
                    dr["ChlnDate"] = Convert.ToDateTime(DataBinder.Eval(lst[i], "Chln_Date")).ToString("dd-MM-yyyy");

                    dt.Rows.Add(dr);

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                grdUser.DataSource = lst;
                grdUser.DataBind();
                imgBtnExcel.Visible = true;
            }
            else
            {
                grdUser.DataSource = null;
                grdUser.DataBind();
                imgBtnExcel.Visible = false;
            }
            lbltotalstaff.Text = "T. Record (s): " + lst.Count;
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DeliveryRegister.xls"));
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

        private void BidFromCity()
        {
            ChallanDelverdDAL ObjChallanDelverdDAL = new ChallanDelverdDAL();
            var lst = ObjChallanDelverdDAL.BindFromCity();
            ObjChallanDelverdDAL = null;
            ddlTocity.DataSource = lst;
            ddlTocity.DataTextField = "City_Name";
            ddlTocity.DataValueField = "City_Idno";
            ddlTocity.DataBind();
            ddlTocity.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        #endregion

        #region Grid Events...
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //MtrlRcptFrmHOBLL objMtrlRcptFrmHOBLL = new MtrlRcptFrmHOBLL();
                    //ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    //ImageButton imgBtn = (ImageButton)e.Row.FindControl("imgBtn");
                    //Label lblMRcptid = (Label)e.Row.FindControl("lblMRcptid");
                    //int iRel = objMtrlRcptFrmHOBLL.checkRelation(Convert.ToInt64(lblMRcptid.Text));
                    //if (Convert.ToString(Session["CFAAdmin"]).Trim() == "1")
                    //{
                    //    if (iRel > 0)
                    //        imgBtnDelete.Visible = false;
                    //    else
                    //        imgBtnDelete.Visible = true;
                    //}
                    //else
                    //{
                    //    imgBtnDelete.Visible = false;
                    //}

                    ////if (iRel > 0)
                    ////{
                    ////    imgBtnDelete.Visible = imgBtn.Visible = false;
                    ////}
                    ////else
                    ////{
                    ////    imgBtnDelete.Visible = imgBtn.Visible = true;
                    ////}
                    //objMtrlRcptFrmHOBLL = null;
                }
            }
        }
        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("ChallanDelverd.aspx?chlnrcptidno=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                ChallanDelverdDAL objChallanDelverdDAL = new ChallanDelverdDAL();
                long value = objChallanDelverdDAL.DeleteALL(Convert.ToInt64(e.CommandArgument));
                objChallanDelverdDAL = null;
                if (value > 0)
                {
                    this.Populate();
                }
            }
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            this.Populate();
        }
        #endregion

        #region Date Range FinYear
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataSource = lst;
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                SetDate();
            }

        }
        private void SetDate()
        {
            if (ddlDateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    txtDateFrom.Text = Convert.ToString(hidmindate.Value);
                    txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = Convert.ToString(hidmaxdate.Value);
                }
            }
        }
        #endregion


        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            Populate();
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
        }
    }
}
