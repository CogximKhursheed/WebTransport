using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;

namespace WebTransport
{
    public partial class ManageLedgerAccount : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 12;
        DataTable CSVTable = new System.Data.DataTable();
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                BindState();
                BindAcntType();
                //Panel1.Visible = false;
                txtAccountPrtyName.Focus();
               // this.TotalCount();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSearch_OnClick(object sender, EventArgs e)
        {
            BindGrid();
            //Panel1.Visible = true;
        }
        #endregion

        #region Functions...
        private void TotalCount()
        {
            LedgerAccountDAL objAccountDAL = new LedgerAccountDAL();
            Int64 CountGridData = objAccountDAL.CountTotal(0, Convert.ToInt32(ddlBalanceType.SelectedValue));
            if (CountGridData > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + CountGridData;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0";
            } 
        }
        private void BindGrid()
        {
            LedgerAccountDAL objAccountDAL = new LedgerAccountDAL();
            var lstGridData = objAccountDAL.SelectForSearch(txtAccountPrtyName.Text.Trim(), TxtMobile.Text.Trim(), Convert.ToInt32(ddlAccountType.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlBalanceType.SelectedValue));

            CSVTable.Columns.Add("S.No.", typeof(string));
            CSVTable.Columns.Add("Acnt_Name", typeof(string));
            CSVTable.Columns.Add("MobileNo", typeof(double));
            CSVTable.Columns.Add("AcntType_Name", typeof(string));
            CSVTable.Columns.Add("Address", typeof(string));
            CSVTable.Columns.Add("State_Name", typeof(string));
            CSVTable.Columns.Add("City_Name", typeof(string));
            CSVTable.Columns.Add("OpBalnce", typeof(double));

            int i = 0;
            foreach (var item in lstGridData)
            {
                DataRow dr = CSVTable.NewRow();
                dr["S.No."] = ++i;
                dr["Acnt_Name"] = DataBinder.Eval(item, "Acnt_Name");
                dr["MobileNo"] = Convert.ToString(DataBinder.Eval(item, "MobileNo")) == "" ? 0 : DataBinder.Eval(item, "MobileNo");
                dr["AcntType_Name"] = DataBinder.Eval(item, "AcntType_Name");
                dr["Address"] = Convert.ToString(DataBinder.Eval(item, "Address")).Contains(",") ? Convert.ToString(DataBinder.Eval(item, "Address")).Replace(',', ' ') : Convert.ToString(DataBinder.Eval(item, "Address"));
                dr["State_Name"] = DataBinder.Eval(item, "State_Name");
                dr["City_Name"] = DataBinder.Eval(item, "City_Name");
                dr["OpBalnce"] = DataBinder.Eval(item, "OpBalnce");
                CSVTable.Rows.Add(dr);
            }
            ViewState["CSVdt"] = CSVTable;

            objAccountDAL = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                grdUser.DataSource = lstGridData;
                grdUser.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = false;

                int startRowOnPage = (grdUser.PageIndex * grdUser.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdUser.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdUser.DataSource = null;
                grdUser.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        private void BindState()
        {
            LedgerAccountDAL objAccountDAL = new LedgerAccountDAL();
            var lst = objAccountDAL.SelectState(0);
            objAccountDAL = null;
            ddlState.DataSource = lst;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }

        private void BindAcntType()
        {
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            var lst = accountDAL.FetchAcntType();
            accountDAL = null;
            ddlAccountType.DataSource = lst;
            ddlAccountType.DataTextField = "AcntType_Name";
            ddlAccountType.DataValueField = "AcntType_Idno";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("--Select Type--", "0"));
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            CSVTable = (DataTable)ViewState["CSVdt"];
            if (CSVTable != null && CSVTable.Rows.Count > 0)
            {
                CSVTable.Columns["Acnt_Name"].ColumnName = "Party Name";
                CSVTable.Columns["AcntType_Name"].ColumnName = "Account Type";
                CSVTable.Columns["State_Name"].ColumnName = "State Name";
                CSVTable.Columns["City_Name"].ColumnName = "City Name";
                CSVTable.Columns["OpBalnce"].ColumnName = "Opening Balance";

                CSVTable.Columns[7].SetOrdinal(3);
                CSVTable.Columns[4].SetOrdinal(2);
                CSVTable.Columns[3].SetOrdinal(5);

                ExportDataTableToCSV(CSVTable, "LedgerAccountList");
                Response.Redirect("ManageLedgerAccount.aspx");
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }

        private void PrepareGridViewForExport(Control gv)
        {

            LinkButton lb = new LinkButton();

            Literal l = new Literal();

            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {

                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {

                    l.Text = (gv.Controls[i] as LinkButton).Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }

                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {

                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }

                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {

                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }

                if (gv.Controls[i].HasControls())
                {

                    PrepareGridViewForExport(gv.Controls[i]);

                }

            }

        }

        #endregion

        #region Grid Events... 
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LedgerAccountDAL accountDAL = new LedgerAccountDAL();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                Label lblDealerid = (Label)e.Row.FindControl("lblDealerid");
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                LinkButton lnkbtnClaimDetails = (LinkButton)e.Row.FindControl("lnkbtnClaimDetails");
                
                base.CheckUserRights(intFormId);

                // Used to hide Delete button if ItemgrpId exists in Lorry Master,Goods Received, Quotation,GR Preparation, Challan Booking,Voucher Entry,Bank Re-Conciliation, F.Mgmt Purchase Bill,F.Mgmt Material Issue
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string AcntIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AcntIdno"));
                string AcntTypeName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AcntType_Name"));
                if (AcntTypeName == "ClaimParty") { lnkbtnClaimDetails.Visible = true; } else { lnkbtnClaimDetails.Visible = false; }
                if (AcntIdno != "")
                {
                    LedgerAccountDAL obj = new LedgerAccountDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(AcntIdno));
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }
                // end----
                imgBtnStatus.Visible = true;

                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }

            accountDAL = null;
        }

        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string DlrHeadid = commandArgs[0];
                string DlrTyp = commandArgs[1];
                Response.Redirect("LedgerMaster.aspx?AcntIdno=" + DlrHeadid + "&DlrType=" + DlrTyp, true);
            }
            else if (e.CommandName == "cmddelete")
            {
                LedgerAccountDAL accountDAL = new LedgerAccountDAL();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string DlrHeadid = commandArgs[0];
                string DlrTyp = commandArgs[1];
                 
                long value = accountDAL.Delete(Convert.ToInt64(DlrHeadid), Convert.ToInt32(DlrTyp));
                accountDAL = null;
                if (value > 0)
                {
                    //tblmsgallSave.Visible = false; //commented by Lokesh
                }
                BindGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('Record deleted successfully')", true);
            }
            else if (e.CommandName == "cmdstatus")
            {
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intitemIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intitemIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    LedgerAccountDAL accountDAL = new LedgerAccountDAL();
                    int value = accountDAL.UpdateStatus(intitemIdno, bStatus, empIdno);
                    accountDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('Status updated successfully')", true);
                        txtAccountPrtyName.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('Status not updated.')", true);
                    }

                }
            }
            else if (e.CommandName == "CmdeditDetails")
            {
                string url = "VechDetlClmPrty.aspx?AcntIdno=" + e.CommandArgument;
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion
    }
}