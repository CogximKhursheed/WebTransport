using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Transactions;

namespace WebTransport
{
    public partial class OpenStock : Pagebase
    {
        #region Variables declaration...
        // private int intFormId = 19;
        DataTable dtTemp = new DataTable();
        DataTable dtDelete = new DataTable();
        double dblNetAmnt = 0, totQty = 0;
        int Count = 0;
        #endregion

        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {

            ddlItemName.Focus();
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                txtSerialNo.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
                OpenTyreDAL objMast = new OpenTyreDAL();

                this.BindCity();
                this.BindDateRange();
                this.BindTyresize();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                dtTemp = dtDelete = CreateDt();
                ViewState["dt"] = dtTemp;
                ViewState["dtDelete"] = dtDelete;
                //if (Request.QueryString["OpenIdno"] != null)
                //{
                //    this.BindDropdownAll();
                //    this.Populate(Convert.ToInt32(Request.QueryString["OpenIdno"]));
                //    hidstckidno.Value = Convert.ToString(Request.QueryString["OpenIdno"]);
                //    lnkbtnNew.Visible = true; 
                //}
                //else
                //{
                BindDropdown();
                //   lnkbtnNew.Visible = false;
                // }

                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            OpenTyreDAL objOpenStock = new OpenTyreDAL();
            Int64 intStck = 0;
            int yearId = Convert.ToInt32(ddlDateRange.SelectedValue);
            int locId = Convert.ToInt32(ddlLocation.SelectedValue);
            int ItemId = Convert.ToInt32(ddlItemName.SelectedValue);
            int TyreSizeId = Convert.ToInt32(ddltyresize.SelectedValue);
            DataTable DT = (DataTable)ViewState["dt"];
            if ((DT != null && DT.Rows.Count > 0) || (ViewState["dtDelete"] != null))
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (ViewState["dtDelete"] != null) { dtDelete = (DataTable)ViewState["dtDelete"]; }

                    if (string.IsNullOrEmpty(hidstckidno.Value) == true)
                    {
                        intStck = objOpenStock.Insert(DT, yearId, ItemId, locId, dtDelete);
                    }
                    // this is commented b/c update mode not in use
                    //else
                    //{
                    //    intStck = objOpenStock.Update(DT, Convert.ToInt64(hidstckidno.Value), yearId, ItemId, locId);
                    //}
                    objOpenStock = null;

                    if (intStck > 0)
                    {
                        if (string.IsNullOrEmpty(hidstckidno.Value) == false)
                        {
                            Tran.Complete();
                            strMsg = "Record updated successfully.";
                        }
                        else
                        {
                            Tran.Complete();
                            strMsg = "Record saved successfully.";
                        }
                        ClearControls();

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hidstckidno.Value) == false)
                        {
                            Tran.Dispose();
                            strMsg = "Record not updated.";
                        }
                        else
                        {
                            Tran.Dispose();
                            strMsg = "Record not saved.";
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                    ddlLocation.Focus();
                }
            }

        }

        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            //for test
            Response.Redirect("OpenStock.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if ((Convert.ToInt64(ddlLocation.SelectedValue) != 0) || (Convert.ToInt64(ddlItemName.SelectedValue)) != 0)
            {
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                FetchRec();
            }
            else
            {
                ClearControls();
            }
            //if (string.IsNullOrEmpty(hidstckidno.Value) == true)
            //{
            //    this.ClearControls();
            //}
            //else
            //{
            //    dtTemp = CreateDt();
            //    ViewState["dt"] = dtTemp;
            //    FetchRec();
            //}

        }
        #endregion

        #region Miscellaneous Function...
        private void Populate(Int64 stckid)
        {
            OpenTyreDAL objMast = new OpenTyreDAL();
            StckMast objStckMast = objMast.SelectById(stckid);
            if (objStckMast != null)
            {
                var VarStckDetl = objMast.SelectDetlById(Convert.ToInt64(objStckMast.Item_Idno), Convert.ToInt64(objStckMast.Loc_Idno));
                if (VarStckDetl != null && VarStckDetl.Count > 0)
                {
                    for (int i = 0; i < VarStckDetl.Count; i++)
                    {
                        ApplicationFunction.DatatableAddRow(dtTemp, i + 1, Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Item_Name")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Item_Idno")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Loc_Name")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Loc_Idno")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "SerialNo")));
                    }
                    ViewState["dt"] = dtTemp;

                    dtTemp = (DataTable)ViewState["dt"];
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        ddlItemName.Enabled = false;
                        ddlLocation.Enabled = false;
                        ddlItemName.SelectedValue = Convert.ToString(DataBinder.Eval(VarStckDetl[0], "Item_Idno"));
                        ddlLocation.SelectedValue = Convert.ToString(DataBinder.Eval(VarStckDetl[0], "Loc_Idno"));
                    }
                    else
                    {
                        ddlItemName.Enabled = true;
                        ddlLocation.Enabled = true;
                    }
                    this.BindGridT();


                }
            }
            objMast = null;
        }
        private void ClearControls()
        {
            txtSerialNo.Text = string.Empty;
            ddlLocation.SelectedIndex = -1;
            ddlItemName.SelectedIndex = -1;
            txtOpenRate.Text = "";
            ddlLocation.Enabled = true;
            ddlItemName.Enabled = true;
            hidstckidno.Value = string.Empty;
            dtTemp = null; ViewState["dt"] = null; this.BindGridT();
            dtTemp = CreateDt();
            ViewState["dt"] = dtTemp;
            lnkbtnNew.Visible = false;

        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "SerialNo", "String",
                "CompName", "String",
                "TypeId", "String",
                "Type", "String",
                "PurFrom", "String",
                "OpenRate", "String",
                 "TyreName", "String",
                 "TyreIdno", "String",
                 "SerialIdno", "String",
                 "TyreSize", "String",
                 "TyreSizeIdno", "String"
                );
            return dttemp;
        }
        private void BindCity()
        {
            OpenTyreDAL objTollMastDAL = new OpenTyreDAL();
            var lst = objTollMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("< Choose Location >", "0"));
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void BindGridT()
        {
            if (ViewState["dt"] != null)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    grdMain.Visible = true;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
                else
                {
                    grdMain.Visible = false;
                    dtTemp = null;
                    grdMain.DataSource = dtTemp;
                    grdMain.DataBind();
                }
            }
            else
            {
                grdMain.Visible = false;
                dtTemp = null;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }
        }
        private void ClearItems()
        {
            lblmessage.Text = "";
            ddlItemName.SelectedIndex = 0; ddlLocation.SelectedIndex = 0;
            txtSerialNo.Text = "";
        }
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex >= 0)
            {
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                }
                else
                {

                }
            }

        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            // this.BindMaxNo(Convert.ToInt32(ddlDateRange.SelectedValue));
            ddlLocation.Focus();
        }
        private void BindDropdown()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var itemname = obj.BindPurchaseItemName();
            obj = null;
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindTyresize()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var tyresize = obj.Bindtyresize();
            obj = null;
            ddltyresize.DataSource = tyresize;
            ddltyresize.DataTextField = "TyreSize";
            ddltyresize.DataValueField = "TyreSize_Idno";
            ddltyresize.DataBind();
            ddltyresize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindDropdownAll()
        {
            OpenTyreDAL obj = new OpenTyreDAL();
            var itemname = obj.BindPurchaseItemNameAll();
            obj = null;
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private bool IsExist(string SerialNo)
        {
            int Count = 0;
            using (TransportMandiEntities db = new TransportMandiEntities(clsMultipleDB.strDynamicConString()))
            {
                Count = db.Stckdetls.Where(r => r.SerialNo == SerialNo).Count();
            }
            if (Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FetchRec()
        {

            OpenTyreDAL objMast = new OpenTyreDAL();
            Int64 YearIdno = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 LocIdno = ddlLocation.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            Int64 ItemIdno = ddlItemName.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlItemName.SelectedValue);
            Int64 tyresizeIdno = ddltyresize.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddltyresize.SelectedValue);

            //hidstckidno.Value = Convert.ToString(Mast.SerlDetl_id);
            var VarStckDetl = objMast.SelectDetlByItemIdno(YearIdno, LocIdno, ItemIdno, tyresizeIdno);
            if (VarStckDetl != null && VarStckDetl.Count > 0)
            {
                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    dtTemp = null;
                    dtTemp = CreateDt();
                    ViewState["dt"] = dtTemp;
                }
                for (int i = 0; i < VarStckDetl.Count; i++)
                {
                    int type = Convert.ToInt32(DataBinder.Eval(VarStckDetl[i], "ItemType")) - 1;

                    ApplicationFunction.DatatableAddRow(dtTemp, i + 1, Convert.ToString(DataBinder.Eval(VarStckDetl[i], "SerialNo")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "CompName")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "ItemType")), Convert.ToString(ddltype.Items[type].Text), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "PurFrom")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "OpenRate")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Item_Name")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "Item_Idno")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "SerialIdno")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "TyreSize")), Convert.ToString(DataBinder.Eval(VarStckDetl[i], "TyreSizeIdno")));
                }
                ViewState["dt"] = dtTemp;

                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    ddlItemName.Enabled = true;
                    ddlLocation.Enabled = true;
                    // ddlItemName.SelectedValue = Convert.ToString(DataBinder.Eval(VarStckDetl[0], "Item_Idno"));
                    //ddlLocation.SelectedValue = Convert.ToString(DataBinder.Eval(VarStckDetl[0], "Loc_Idno"));
                }
                else
                {
                    ddlItemName.Enabled = true;
                    ddlLocation.Enabled = true;
                }
                this.BindGridT();
            }
            else
            {
                dtTemp = null;
                dtTemp = CreateDt();
                ViewState["dt"] = dtTemp;
                grdMain.DataSource = dtTemp;
                grdMain.DataBind();
            }


        }
        #endregion

        #region Button Entry Event....
        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ViewState["ID"] == null)
            {
                if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Tyre."); ddlItemName.Focus(); return; }
            }
            if (ddlLocation.SelectedIndex == 0) { ShowMessageErr("Please select Location."); ddlLocation.Focus(); return; }
            if (ddltyresize.SelectedIndex == 0) { ShowMessageErr("Please select Tyre Size."); ddltyresize.Focus(); return; }
            if (txtSerialNo.Text == "") { ShowMessageErr("Enter Serial No."); txtSerialNo.Focus(); return; }
            if (IsExist(txtSerialNo.Text.Trim()) == true) { ShowMessageErr("Serial No:" + txtSerialNo.Text.Trim() + " already in stock"); txtSerialNo.Focus(); return; }
            string TotalAmount = string.Empty;

            dtTemp = (DataTable)ViewState["dt"];
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                ddlItemName.Enabled = false;
            }
            else
            {
                ddlItemName.Enabled = false;
            }

            if (ViewState["ID"] != null)
            {
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(ViewState["ID"].ToString()))
                    {
                        dtrow["SerialNo"] = txtSerialNo.Text.Trim();
                        dtrow["CompName"] = txtCompName.Text.Trim();
                        dtrow["TypeId"] = ddltype.SelectedValue;
                        dtrow["Type"] = ddltype.SelectedItem.Text;
                        dtrow["TyreSizeIdno"] = ddltyresize.SelectedValue;
                        dtrow["TyreSize"] = ddltyresize.SelectedItem.Text;
                        dtrow["PurFrom"] = txtPurchaseFrom.Text.Trim();
                        dtrow["OpenRate"] = Convert.ToDouble(txtOpenRate.Text.Trim()).ToString("N2");
                        lnkbtnSubmit.Focus();
                    }
                }
                ViewState["ID"] = null;
            }
            else
            {
                dtTemp = (DataTable)ViewState["dt"];
                if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        if (Convert.ToString(row["SerialNo"]) == Convert.ToString(txtSerialNo.Text.Trim()))
                        {
                            string msg = "Serial No Already Entered!";
                            ddlItemName.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                            return;
                        }
                    }
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string compName = string.IsNullOrEmpty(txtCompName.Text.Trim()) ? "" : txtCompName.Text.Trim();
                string strSerialNo = txtSerialNo.Text.Trim();
                string TypeID = string.IsNullOrEmpty(ddltype.SelectedValue) ? "0" : (ddltype.SelectedValue);
                string strType = ddltype.SelectedItem.Text.Trim();
                string strPur = txtPurchaseFrom.Text.Trim();
                string openRate = Convert.ToDouble(txtOpenRate.Text.Trim()).ToString("N2");
                string ItemName = ddlItemName.SelectedItem.Text;
                string ItemItem = ddlItemName.SelectedValue;
                string tyresize = ddltyresize.SelectedItem.Text;
                string tyresizeidno = ddltyresize.SelectedValue;
                ApplicationFunction.DatatableAddRow(dtTemp, id, strSerialNo, compName, TypeID, strType, strPur, openRate, ItemName, ItemItem,"", tyresize, tyresizeidno);
            }
            ViewState["dt"] = dtTemp;

            this.BindGridT();
            txtSerialNo.Text = "";
            txtCompName.Text = "";
            txtPurchaseFrom.Text = "";
            txtOpenRate.Text = "";
            ddlItemName.Enabled = true; rfvPartno.Enabled = true;
            lnkbtnNew.Visible = true;
            txtSerialNo.Focus();
        }

        protected void lnkbtnNewClick_OnClick(object sender, EventArgs e)
        {
            ddlItemName.SelectedValue = ddlLocation.SelectedValue = "0";ddltyresize.SelectedValue = "0";
            txtSerialNo.Text = "";
            txtCompName.Text = "";
            txtPurchaseFrom.Text = "";
            txtOpenRate.Text = "";
            ddltype.SelectedValue = "1";
            ddlDateRange.Focus();
            grdMain.DataSource = null;
            grdMain.DataBind();

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            txtSerialNo.Text = "";
            txtCompName.Text = "";
            txtPurchaseFrom.Text = "";
            txtOpenRate.Text = "";
            txtSerialNo.Focus();
        }
        #endregion

        #region Grid Event....

        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Count = 0;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OpenTyreDAL obj = new OpenTyreDAL();
                int ICount = obj.ICount(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SerialNo")));
                double amnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpenRate"));
                dblNetAmnt = amnt + dblNetAmnt;
                if (ICount > 0)
                {
                    LinkButton lblDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                    LinkButton lbledit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    lblDelete.Enabled = false;
                    lblDelete.ToolTip = "Issued";
                    lbledit.Enabled = false;
                    lbledit.ToolTip = "Issued";
                }
                Count++;

                // Used to hide Delete button if ItemgrpId exists in Rate Master,Goods Received, Quotation,GR Preparation,Commission Master
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                string ItemIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TyreIdno"));
                string SerialNo = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SerialNo"))) ? "0" : Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SerialNo"));
                if (ItemIdno != "")
                {
                    BindDropdownDAL obj1 = new BindDropdownDAL();
                    var ItemExist = obj1.CheckItemExistInOtherMaster(SerialNo);
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;
                        lnkbtnEdit.Visible = false;
                    }
                    else
                    {
                        lnkbtnEdit.Visible = true;
                        lnkbtnDelete.Visible = true;
                    }
                }
                // end----
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblRecordCount");
                Label lblAmntTot = (Label)e.Row.FindControl("lbltotAmount");
                lblQuantity.Text = Convert.ToDouble(grdMain.Rows.Count).ToString("N2");
                //lblAmntTot.Text = dblNetAmnt.ToString("N2");
            }
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGridT();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dtTemp = (DataTable)ViewState["dt"];
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
            if (e.CommandName == "cmdedit")
            {
                dtTemp = (DataTable)ViewState["dt"];
                DataRow[] drs = dtTemp.Select("Id='" + id + "'");
                if (drs.Length > 0)
                {
                    txtSerialNo.Text = Convert.ToString(drs[0]["SerialNo"]);
                    txtCompName.Text = Convert.ToString(drs[0]["CompName"]); 
                    ddltype.SelectedValue = Convert.ToString(drs[0]["TypeId"]);
                    ddltyresize.SelectedValue = string.IsNullOrEmpty(Convert.ToString(drs[0]["tyresizeidno"]))? "0" : Convert.ToString(drs[0]["tyresizeidno"]);
                    txtPurchaseFrom.Text = Convert.ToString(drs[0]["PurFrom"]);
                    txtOpenRate.Text = Convert.ToString(drs[0]["OpenRate"]);
                    //ddlItemName.SelectedValue = Convert.ToString(drs[0]["ItemIdno"]);
                    ViewState["ID"] = Convert.ToString(drs[0]["id"]);

                    ddlItemName.Enabled = false; rfvPartno.Enabled = false;
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                //DataTable dtInnerDelete = CreateDt();
                DataTable objDataTable = CreateDt();
                // dtTemp = (DataTable)ViewState["dt"];
                dtDelete = (DataTable)ViewState["dtDelete"];
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));
                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["SerialNo"], rw["CompName"], rw["TypeId"], rw["Type"], rw["PurFrom"], rw["OpenRate"], rw["TyreName"], rw["TyreIdno"], rw["TyreSize"], rw["TyreSizeIdno"]);
                    }
                    else
                    {
                        ApplicationFunction.DatatableAddRow(dtDelete, rw["id"], rw["SerialNo"], rw["CompName"], rw["TypeId"], rw["Type"], rw["PurFrom"], rw["OpenRate"], rw["TyreName"], rw["TyreIdno"], rw["TyreSize"], rw["TyreSizeIdno"]);
                    }
                }
                ViewState["dtDelete"] = dtDelete;
                ViewState["dt"] = objDataTable;
                objDataTable.Dispose();
                this.BindGridT();
            }

        }

        #endregion

        #region IndexChange Event....
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchRec();
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchRec();
        }
        #endregion

        #region For uploading Excel
        protected void lnkbtnUpload_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!FileUpload.HasFile)
            {
                FileUpload.Focus();
                msg = "Please Select Excel File!";
                ShowMessageErr(msg);
                return;
            }
            if (ddlDateRange.SelectedValue == "0")
            {
                ddlDateRange.Focus();
                msg = "Please Select Date Range!";
                ShowMessageErr(msg);
                return;
            }
            if (ddlLocation.SelectedValue == "0")
            {
                ddlLocation.Focus();
                msg = "Please Select Location!";
                ShowMessageErr(msg);
                return;
            }
            OpenTyreDAL objOpenStockAccess = new OpenTyreDAL();
            string excelfilename = string.Empty;
            try
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    excelfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "ExcelTyre", "OpeningStockAccessories");
                    if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                    {
                        DataTable dt = ReadExcelFile("~/ExcelTyre/" + excelfilename);
                        Int64 value = 0;
                        OpenTyreDAL clsOpngStck = new OpenTyreDAL();
                        DataTable DtnotUpload = dt.Clone();

                        OpenTyreDAL objItem = new OpenTyreDAL();
                        for (int count = 0; count < dt.Rows.Count; count++)
                        {
                            var lst = objItem.GetItemDetailsExl(Convert.ToString(dt.Rows[count]["TyreName"]).Trim());
                            if (lst.Count <= 0)
                            {
                                ShowMessageErr(Convert.ToString(dt.Rows[count]["TyreName"].ToString()).Trim() + " Item Does Not Exist!");
                                return;
                            }
                            else
                            {
                                string Type = dt.Rows[count]["Type"].ToString();
                                if (Type == "New" || Type == "Old" || Type == "Retrieted")
                                {
                                    string Type_Idno = (Type == "New") ? "1" : (Type == "Old") ? "2" : "3";
                                    dt.Rows[count]["Type_Idno"] = Type_Idno;
                                }
                                else
                                {
                                    ShowMessageErr(Convert.ToString(dt.Rows[count]["Type"].ToString()).Trim() + " Type Does Not Exist of " + dt.Rows[count]["TyreName"].ToString() + " Item!");
                                    return;
                                }
                            }
                        }
                        int dttruncate = objOpenStockAccess.TurncatePartsAccessoriesFromExcel(ApplicationFunction.ConnectionString());
                        value = clsOpngStck.InsertPartsByExcel(dt, ApplicationFunction.ConnectionString());

                        BindGridDB();

                    }
                    else
                    {
                        msg = "Excel File Format Not Supported!";
                        ShowMessageErr(msg);
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }

        public DataTable ReadExcelFile(string fileName)
        {
            string msg = string.Empty;
            DataTable dt = new DataTable();
            string filepath = Server.MapPath(fileName);
            string constring = string.Empty;
            if (System.IO.Path.GetExtension(filepath) == ".xls")
            {
                constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filepath + "';Extended Properties='Excel 8.0;HDR=Yes;'";
            }
            else if (System.IO.Path.GetExtension(filepath) == ".xlsx")
            {
                constring = "Provider= Microsoft.ACE.OLEDB.12.0;OLE DB Services=-4;Data Source='" + filepath + "'; Extended Properties=\"Excel 12.0;HDR=YES;\"";
            }
            if (string.IsNullOrEmpty(constring) == false)
            {
                OleDbConnection con = new OleDbConnection(constring);
                con.Open();
                DataTable ExcelTable = new DataTable();
                ExcelTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string SheetName = Convert.ToString(ExcelTable.Rows[0][2]);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "]", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ApplicationFunction.CreateTable("tblOpngStckAccessoriesFromExcel", "TyreName", "String", "SerialNo", "String", "CompanyName", "String", "PurchaseFrom", "String", "OpeningRate", "String", "Type", "String", "Type_Idno", "String");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Columns[0].Caption == "TyreName" && ds.Tables[0].Columns[1].Caption == "SerialNo" && ds.Tables[0].Columns[2].Caption == "CompanyName" && ds.Tables[0].Columns[3].Caption == "PurchaseFrom" && ds.Tables[0].Columns[4].Caption == "OpeningRate" && ds.Tables[0].Columns[5].Caption == "Type")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                ApplicationFunction.DatatableAddRow(dt,
                                    Convert.ToString(ds.Tables[0].Rows[count]["TyreName"]),
                                     Convert.ToString(ds.Tables[0].Rows[count]["SerialNo"]),
                                    Convert.ToString(ds.Tables[0].Rows[count]["CompanyName"]),
                                    Convert.ToString(ds.Tables[0].Rows[count]["PurchaseFrom"]),
                                    Convert.ToString(ds.Tables[0].Rows[count]["OpeningRate"]),
                                    Convert.ToString(ds.Tables[0].Rows[count]["Type"])
                                     );
                            }
                        }
                        else
                        {
                            ShowMessageErr("Excel is blank.Please upload with data.");
                        }
                    }
                    else
                    {
                        this.ShowMessageErr("Excel Header Not In Correct Format.");
                    }
                }
                else
                {
                    ShowMessageErr("Please Check Excel File Columns!");
                }
            }
            return dt;
        }

        private void BindGridDB()
        {
            OpenTyreDAL objOpeningStock = new OpenTyreDAL();

            DataTable dtItem = objOpeningStock.SelectPartFromExcel(ApplicationFunction.ConnectionString());
            if (dtItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    dtTemp = (DataTable)ViewState["dt"];
                    if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            if (Convert.ToString(row["SerialNo"]) == Convert.ToString(dtItem.Rows[i]["SerialNo"].ToString()))
                            {
                                string msg = "Serial No. Already Exist For Same Item";
                                ddlItemName.Focus();
                                ShowMessageErr(msg);
                                return;
                            }
                        }
                    }

                    Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                    int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                    string compName = dtItem.Rows[i]["CompanyName"].ToString();
                    string strSerialNo = dtItem.Rows[i]["SerialNo"].ToString();
                    string TypeID = dtItem.Rows[i]["Type_Idno"].ToString();
                    string strType = dtItem.Rows[i]["Type"].ToString();
                    string strPur = dtItem.Rows[i]["PurchaseFrom"].ToString();
                    string strItemName = dtItem.Rows[i]["Item_Name"].ToString();
                    string strItemIdno = dtItem.Rows[i]["Item_Idno"].ToString();
                    string openRate = Convert.ToDouble(dtItem.Rows[i]["Item_Rate"].ToString()).ToString("N2");
                    ApplicationFunction.DatatableAddRow(dtTemp, id, strSerialNo, compName, TypeID, strType, strPur, openRate, strItemName, strItemIdno);
                }
            }

            ViewState["dt"] = dtTemp;
            this.BindGridT();
        }
        #endregion
    }
}