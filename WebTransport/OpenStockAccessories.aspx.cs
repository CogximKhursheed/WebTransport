using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using WebTransport.Classes;
using WebTransport.DAL;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Transactions;
using WebTransport.DAL;
using System.Data.OleDb;
using System.Globalization;
using System.Text;

namespace WebTransport
{
    public partial class OpenStockAccessories : Pagebase
    {
        #region Variables declaration...
        // private int intFormId = 19;
        DataTable dtTemp = new DataTable();
        DataTable dtDelete = new DataTable();
        int Count = 0;
        double dTotQty = 0;
        double dTotRate = 0;
        #endregion

        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.ADD == false)
                //{
                //    btnSave.Visible = false;
                //}
                //if (base.View == false)
                //{
                //    lblViewList.Visible = false;
                //}

                this.BindDateRange();
                BindDropdown();
                this.BindCity();
                dtTemp = dtDelete = CreateDt();
                ViewState["dt"] = dtTemp;
                ViewState["dtDelete"] = dtDelete;
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Button Events...

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            OpenAccDAL objOpenStock = new OpenAccDAL();
            Int64 intStck = 0;

            DataTable DT = (DataTable)ViewState["dt"];
            if (ViewState["dtDelete"] != null) { dtDelete = (DataTable)ViewState["dtDelete"]; }
            if (DT != null && DT.Rows.Count > 0)
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    //if (string.IsNullOrEmpty(hidstckidno.Value) == true)
                    //{
                    intStck = objOpenStock.Insert(DT, dtDelete);

                    //}
                    //else
                    //{
                    //    intStck = objOpenStock.Update(DT, Convert.ToInt64(hidstckidno.Value));
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
            Response.Redirect("OpenStockAccessories.aspx");
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if ((Convert.ToInt64(ddlLocation.SelectedValue) != 0) || (Convert.ToInt64(ddlItemName.SelectedValue)) != 0)
            {
                this.FetchRecords();
            }
            else
            {
                ClearControls();
            }
        }

        protected void lnkbtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex == 0) { ShowMessageErr("Please select Item."); ddlItemName.Focus(); return; }
            if (ddlLocation.SelectedIndex == 0) { ShowMessageErr("Please select Location."); ddlLocation.Focus(); return; }
            if (txtQty.Text == "") { ShowMessageErr("Enter Qty"); txtQty.Focus(); return; }
            if (txtOpenRate.Text == "") { ShowMessageErr("Enter Qty"); txtQty.Focus(); return; }
            string TotalAmount = string.Empty;

            dtTemp = (DataTable)ViewState["dt"];

            if (ViewState["ID"] != null)
            {
                foreach (DataRow dtrow in dtTemp.Rows)
                {
                    if (Convert.ToString(dtrow["id"]) == Convert.ToString(ViewState["ID"].ToString()))
                    {
                        dtrow["YearIdno"] = ddlDateRange.SelectedValue;
                        dtrow["LocName"] = ddlLocation.SelectedItem.Text;
                        dtrow["LocIdno"] = ddlLocation.SelectedValue;
                        dtrow["ItemName"] = ddlItemName.SelectedItem.Text;
                        dtrow["ItemIdno"] = ddlItemName.SelectedValue;
                        dtrow["Qty"] = txtQty.Text.Trim();
                        dtrow["Rate"] = txtOpenRate.Text.Trim();
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
                        if (Convert.ToString(row["ItemIdno"]) == Convert.ToString(ddlItemName.SelectedValue) && Convert.ToString(row["LocIdno"]) == Convert.ToString(ddlLocation.SelectedValue))
                        {
                            string msg = "Item Already Entered for same location";
                            ddlItemName.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
                            return;
                        }
                    }
                }
                Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                string strYearIdno = string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? "0" : (ddlDateRange.SelectedValue);
                string strItemName = ddlItemName.SelectedItem.Text.Trim();
                string strItemIdno = string.IsNullOrEmpty(ddlItemName.SelectedValue) ? "0" : (ddlItemName.SelectedValue);
                string strLocName = ddlLocation.SelectedItem.Text.Trim();
                string strLocIdno = string.IsNullOrEmpty(ddlLocation.SelectedValue) ? "0" : (ddlLocation.SelectedValue);
                string strQty = txtQty.Text.Trim();
                string strRate = txtOpenRate.Text.Trim();
                ApplicationFunction.DatatableAddRow(dtTemp, id, strYearIdno, strLocIdno, strLocName, strItemIdno, strItemName, strQty, strRate);
            }
            ViewState["dt"] = dtTemp;

            this.BindGridT();
            txtQty.Text = "";
            txtOpenRate.Text = "";
            ddlItemName.SelectedValue = "0";
            ddlLocation.SelectedValue = "0";
        }

        protected void lnkbtnNewClick_OnClick(object sender, EventArgs e)
        {
            ddlLocation.SelectedValue = ddlItemName.SelectedValue = "0";
            txtQty.Text = txtOpenRate.Text = "";
            ddlDateRange.Focus();
        }
        #endregion

        #region Miscellaneous Events...
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
            txtQty.Text = string.Empty;
            txtOpenRate.Text = string.Empty;
            ddlLocation.SelectedIndex = -1;
            ddlItemName.SelectedIndex = -1;
            ddlLocation.Enabled = true;
            ddlItemName.Enabled = true;
            hidstckidno.Value = string.Empty;
            dtTemp = dtDelete = null; ViewState["dt"] = null;
            this.BindGridT();
            dtTemp = dtDelete = CreateDt();
            ViewState["dt"] = dtTemp;
            ViewState["dtDelete"] = dtDelete;
            lnkbtnNew.Visible = false;
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "Id", "String",
                "YearIdno", "String",
                "LocIdno", "String",
                "LocName", "String",
                "ItemIdno", "String",
                "ItemName", "String",
                "Qty", "String",
                "Rate", "String"
                );
            return dttemp;
        }
        private void BindCity()
        {
            OpenAccDAL objTollMastDAL = new OpenAccDAL();
            var lst = objTollMastDAL.BindCityAll();
            ddlLocation.DataSource = lst;
            ddlLocation.DataTextField = "City_Name";
            ddlLocation.DataValueField = "City_Idno";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Choose Location >", "0"));
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
                    lnkbtnNew.Visible = true;
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
            txtQty.Text = ""; txtOpenRate.Text = "";
        }
        private void BindDropdown()
        {
            OpenAccDAL obj = new OpenAccDAL();
            var itemname = obj.BindPurchaseItemName();
            obj = null;
            ddlItemName.DataSource = itemname;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
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
        private void BindDateRange()
        {
            FinYearDAL objDAL = new FinYearDAL();
            ddlDateRange.DataSource = objDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objDAL = null;

            ddlDateRange.Focus();
        }
        private void FetchRecords()
        {

            Int64 YearIdno = Convert.ToInt64(ddlDateRange.SelectedValue);
            Int64 LocIdno = ddlLocation.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlLocation.SelectedValue);
            Int64 ItemIdno = ddlItemName.SelectedIndex == 0 ? 0 : Convert.ToInt64(ddlItemName.SelectedValue);
            OpenAccDAL objMast = new OpenAccDAL();
            var Mast = objMast.SelectStckMast(YearIdno, LocIdno, ItemIdno);

            if (Mast != null && Mast.Count > 0)
            {

                dtTemp = (DataTable)ViewState["dt"];
                if (dtTemp.Rows.Count > 0)
                {
                    dtTemp = null;
                    dtTemp = CreateDt();
                    ViewState["dt"] = dtTemp;
                }
                for (int i = 0; i < Mast.Count; i++)
                {
                    ApplicationFunction.DatatableAddRow(dtTemp, i + 1, Convert.ToString(DataBinder.Eval(Mast[i], "YearIdno")), Convert.ToString(DataBinder.Eval(Mast[i], "LocIdno")), Convert.ToString(DataBinder.Eval(Mast[i], "LocName")), Convert.ToString(DataBinder.Eval(Mast[i], "ItemIdno")), Convert.ToString(DataBinder.Eval(Mast[i], "ItemName")), string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(Mast[i], "Qty"))) == true ? "0" : Convert.ToString(DataBinder.Eval(Mast[i], "Qty")), string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(Mast[i], "Rate"))) == true ? "0.00" : Convert.ToString(DataBinder.Eval(Mast[i], "Rate")));
                }
                ViewState["dt"] = dtTemp;

                dtTemp = (DataTable)ViewState["dt"];
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
                dTotQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                dTotRate += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Rate"));
                Count++;

                // Used to hide Delete button if ItemgrpId exists in Rate Master,Goods Received, Quotation,GR Preparation,Commission Master
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string LocIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LocIdno"));
                string ItemIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemIdno"));
                if (ItemIdno != "")
                {
                    OpenAccDAL obj = new OpenAccDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(ItemIdno),Convert.ToInt32(LocIdno));
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
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblRecordCount");
                lblQuantity.Text = Count.ToString();
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
                    ddlDateRange.SelectedValue = Convert.ToString(drs[0]["YearIdno"]);
                    ddlLocation.SelectedValue = Convert.ToString(drs[0]["LocIdno"]);
                    ddlItemName.SelectedValue = Convert.ToString(drs[0]["ItemIdno"]);
                    txtQty.Text = string.IsNullOrEmpty(Convert.ToString(drs[0]["Qty"])) == true ? "0" : Convert.ToString(drs[0]["Qty"]);
                    txtOpenRate.Text = string.IsNullOrEmpty(Convert.ToString(drs[0]["Rate"])) == true ? "0.00" : Convert.ToDouble(drs[0]["Rate"]).ToString("N2");
                    ViewState["ID"] = Convert.ToString(drs[0]["id"]);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DataTable dtInnerDelete = CreateDt();
                DataTable objDataTable = CreateDt();
                foreach (DataRow rw in dtTemp.Rows)
                {
                    int ridd = Convert.ToInt32(Convert.ToString(rw["id"]));

                    if (id != ridd)
                    {
                        ApplicationFunction.DatatableAddRow(objDataTable, rw["id"], rw["YearIdno"], rw["LocIdno"], rw["LocName"], rw["ItemIdno"], rw["ItemName"], rw["Qty"], rw["Rate"]);
                    }
                    else
                    {
                        ApplicationFunction.DatatableAddRow(dtInnerDelete, rw["id"], rw["YearIdno"], rw["LocIdno"], rw["LocName"], rw["ItemIdno"], rw["ItemName"], rw["Qty"], rw["Rate"]);
                    }
                }
                ViewState["dt"] = objDataTable;
                ViewState["dtDelete"] = dtInnerDelete;
                this.AccDelete();
                objDataTable.Dispose();
                this.BindGridT();

              
            }

        }

        public void AccDelete()
        {
            OpenAccDAL objOpenStock = new OpenAccDAL();
            Int64 intStck = 0; string strMsg = string.Empty;
            DataTable DT = (DataTable)ViewState["dt"];
            if (ViewState["dtDelete"] != null) { dtDelete = (DataTable)ViewState["dtDelete"]; }
            if (dtDelete != null && dtDelete.Rows.Count > 0)
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    intStck = objOpenStock.Delete(dtDelete);
                    objOpenStock = null;
                    if (intStck > 0)
                    {
                        Tran.Complete();
                        strMsg = "Record updated successfully.";

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
        }
        #endregion

        #region Control Event.......
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FetchRecords();
            txtQty.Focus();
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FetchRecords();
            ddlItemName.Focus();
        }
        #endregion

        #region For uploading Excel
        protected void lnkbtnUpload_OnClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!FileUpload.HasFile)
            {
                FileUpload.Focus();
                msg = "Please Select Excel file!";
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

            OpenAccDAL objOpenStockAccess = new OpenAccDAL();
            string excelfilename = string.Empty;
            try
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    excelfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "ExcelAccessories", "OpeningStockAccessories");
                    if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                    {
                        DataTable dt = ReadExcelFile("~/ExcelAccessories/" + excelfilename);
                        Int64 value = 0;
                        OpenAccDAL clsOpngStck = new OpenAccDAL();
                        DataTable DtnotUpload = dt.Clone();

                        OpenAccDAL objItem = new OpenAccDAL();
                        for (int count = 0; count < dt.Rows.Count; count++)
                        {
                            var lst = objItem.GetItemDetailsExl(Convert.ToString(dt.Rows[i]["Item"]).Trim());
                            if (lst.Count <= 0)
                            {
                                ShowMessageErr(Convert.ToString(dt.Rows[i]["Item"].ToString()).Trim() + " Item Does Not Exist!");
                                return;
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
                dt = ApplicationFunction.CreateTable("tblOpngStckAccessoriesFromExcel", "Item", "String", "Qty", "String", "Rate", "String");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Columns[0].Caption == "Item" && ds.Tables[0].Columns[1].Caption == "Qty" && ds.Tables[0].Columns[2].Caption == "Rate")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                ApplicationFunction.DatatableAddRow(dt,
                                    Convert.ToString(ds.Tables[0].Rows[count]["Item"]),
                                     Convert.ToString(ds.Tables[0].Rows[count]["Qty"]),
                                    Convert.ToString(ds.Tables[0].Rows[count]["Rate"])
                                     );
                            }
                        }
                        else
                        {
                            ShowMessageErr("Excel Is Blank.Please Upload With Data.");
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
            OpenAccDAL objOpeningStock = new OpenAccDAL();

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
                            if (Convert.ToString(row["ItemIdno"]) == dtItem.Rows[i]["Item_Idno"].ToString() && Convert.ToString(row["LocIdno"]) == Convert.ToString(ddlLocation.SelectedValue))
                            {
                                string msg = "Item Already Exist For Same Location";
                                ddlItemName.Focus();
                                ShowMessageErr(msg);
                                return;
                            }
                        }
                    }
                    Int32 ROWCount = Convert.ToInt32(dtTemp.Rows.Count) - 1;
                    int id = dtTemp.Rows.Count == 0 ? 1 : (Convert.ToInt32(dtTemp.Rows[ROWCount]["id"])) + 1;
                    string strYearIdno = string.IsNullOrEmpty(ddlDateRange.SelectedValue) ? "0" : (ddlDateRange.SelectedValue);
                    string strItemName = dtItem.Rows[i]["Item_Name"].ToString();
                    string strItemIdno = string.IsNullOrEmpty(dtItem.Rows[i]["Item_Idno"].ToString()) ? "0" : (dtItem.Rows[i]["Item_Idno"].ToString());
                    string strLocName = ddlLocation.SelectedItem.Text.Trim();
                    string strLocIdno = string.IsNullOrEmpty(ddlLocation.SelectedValue) ? "0" : (ddlLocation.SelectedValue);
                    string strQty = string.IsNullOrEmpty(dtItem.Rows[i]["Item_Qty"].ToString()) ? "0" : (dtItem.Rows[i]["Item_Qty"].ToString());
                    string strRate = string.IsNullOrEmpty(dtItem.Rows[i]["Item_Rate"].ToString()) ? "0" : (dtItem.Rows[i]["Item_Rate"].ToString());
                    ApplicationFunction.DatatableAddRow(dtTemp, id, strYearIdno, strLocIdno, strLocName, strItemIdno, strItemName, strQty, strRate);
                }
            }

            ViewState["dt"] = dtTemp;
            this.BindGridT();
            txtQty.Text = "";
            txtOpenRate.Text = "";
            ddlItemName.SelectedValue = "0";
            ddlLocation.SelectedValue = "0";

        }
        #endregion
    }
}