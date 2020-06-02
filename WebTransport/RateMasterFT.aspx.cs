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

namespace WebTransport
{

    public partial class RateMasterFT : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; Int32 iFromCity = 0; Int32 totrecords = 0;
        private int intFormId = 11;
        #endregion

        #region Form Load...
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
                if (base.ADD == false)
                {
                    lnkbtnSave.Visible = false;
                }
                this.BindDateRange();
                this.BindCity();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddlDateRange.SelectedIndex = 0;
                ddlDateRange_SelectedIndexChanged(null, null);
                txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindLorryType();
                txtfrghtAmount.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");

                txtfrghtAmount.Attributes.Add("onChange", "SetNumFormt('" + txtfrghtAmount.ClientID + "')");

                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    CityMastDAL obj = new CityMastDAL();
                    var lst = obj.SelectCityCombo();
                    obj = null;
                    drpBaseCity.DataSource = lst;
                    drpBaseCity.DataTextField = "City_Name";
                    drpBaseCity.DataValueField = "City_Idno";
                    drpBaseCity.DataBind();
                    drpBaseCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                userpref();
                drpBaseCity.SelectedValue = Convert.ToString(base.UserFromCity);

            }
            HidFrmCityIdno.Value = drpBaseCity.SelectedValue;
            ddlDateRange.Focus();

        }

        #endregion

        #region Functions...
        private void BindCity(Int64 UserIdno)
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var FrmCity = obj.BindCityUserWise(UserIdno);
            drpBaseCity.DataSource = FrmCity;
            drpBaseCity.DataTextField = "CityName";
            drpBaseCity.DataValueField = "cityidno";
            drpBaseCity.DataBind();
            drpBaseCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
        private void BindCity()
        {
            RateMasterDAL obj = new RateMasterDAL();
            var lst = obj.SelectCityCombo();
            obj = null;

            ddlCity.DataSource = lst;
            ddlCity.DataTextField = "City_Name";
            ddlCity.DataValueField = "City_Idno";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        private void EditGridFunction()
        {
            int id = 0;
            if (ViewState["id"] != null)
                id = Convert.ToInt32(ViewState["id"].ToString());

            DtTemp = (DataTable)ViewState["dt"];
            DataRow[] drs = DtTemp.Select("Id='" + id + "'");

            if (drs.Length > 0)
            {
               
                ddlCity.SelectedValue = Convert.ToString(drs[0]["ToCity_Idno"]);
                txtDateRate.Text = Convert.ToDateTime(drs[0]["Rate_Date"]).ToString("dd-MM-yyyy");
                txtfrghtAmount.Text = Convert.ToDouble(drs[0]["Item_Rate"]).ToString("N2");
                drpBaseCity.SelectedValue = Convert.ToString(drs[0]["City_FIdno"]);
                Hidrowid.Value = Convert.ToString(drs[0]["id"]);
                Session["Value"] = Hidrowid.Value;

            }
            txtDateRate.Focus();
            BindGrid();
        }
        private void BindLorryType()
        {
            RateMastFTDAL obj = new RateMastFTDAL();
            var lst = obj.BindLorryType();
            obj = null;
            if (lst.Count > 0)
            {
                ddlLorryType.DataSource = lst;
                ddlLorryType.DataTextField = "Lorry_No";
                ddlLorryType.DataValueField = "Lorry_Idno";
                ddlLorryType.DataBind();
            }
            ddlLorryType.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    txtDateRate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateRate.Text = hidmindate.Value;
                }
            }

        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl",
                "id", "String",
                "City_FIdno", "String",
                "City_FName", "String",
                "Lorry_Idno", "String",
                "Lorry_Type", "String",
                "Rate_Date", "String",
                "ToCity_Idno", "String",
                "City_Name", "String",
                "Item_Rate", "String"
                );
            return dttemp;
        }
        private void BindGrid()
        {
            grdMain.DataSource = (DataTable)ViewState["dt"];
            grdMain.DataBind();
        }
        private void ClearItems()
        {
            //ddlCity.SelectedValue = "0";
            txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtfrghtAmount.Text = "0.00";
            ddlCity.SelectedIndex = 0;
            Hidrowid.Value = string.Empty;
            ViewState["id"] = null;
        }
        private void Clear()
        {
            
            ddlLorryType.SelectedValue = "0";
           // hidrateid.Value = string.Empty;
            ddlCity.SelectedValue = "0";
            txtfrghtAmount.Text = "0.00";
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        private void ClearAll()
        {

            ddlCity.SelectedValue = "0";
            ddlLorryType.SelectedValue = "0";
            txtfrghtAmount.Text = "0.00";
            
        }
        private void userpref()
        {
            RateMastFTDAL objGrprepDAL = new RateMastFTDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
        }
        private void BindGridDB()
        {
            RateMastFTDAL objRateMstFT = new RateMastFTDAL();
            DataTable dt1 = new DataTable();
            DataRow Dr;
            DtTemp = CreateDt();
            dt1 = objRateMstFT.SelectDBData(Convert.ToInt64(ddlLorryType.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), ApplicationFunction.ConnectionString());

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Dr = DtTemp.NewRow();
                    Dr.BeginEdit();
                    Dr[0] = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                    Dr[1] = Convert.IsDBNull(dt1.Rows[i]["City_FIdno"]) ? "0" : Convert.ToString(dt1.Rows[i]["City_FIdno"]);
                    Dr[2] = Convert.IsDBNull(dt1.Rows[i]["City_FName"]) ? "0" : Convert.ToString(dt1.Rows[i]["City_FName"]);
                    Dr[3] = Convert.IsDBNull(dt1.Rows[i]["Lorry_Idno"]) ? "0" : Convert.ToString(dt1.Rows[i]["Lorry_Idno"]);
                    Dr[4] = Convert.IsDBNull(dt1.Rows[i]["Lorry_Type"]) ? "0" : Convert.ToString(dt1.Rows[i]["Lorry_Type"]);
                    Dr[5] = Convert.IsDBNull(dt1.Rows[i]["Rate_Date"]) ? "" : Convert.ToDateTime((dt1.Rows[i]["Rate_Date"])).ToString();
                    Dr[6] = Convert.IsDBNull(dt1.Rows[i]["ToCity_Idno"]) ? "0" : Convert.ToString(dt1.Rows[i]["ToCity_Idno"]);
                    Dr[7] = Convert.IsDBNull(dt1.Rows[i]["City_Name"]) ? "0" : Convert.ToString(dt1.Rows[i]["City_Name"]);
                    Dr[8] = Convert.IsDBNull(dt1.Rows[i]["Item_Rate"]) ? "0" : Convert.ToDouble(dt1.Rows[i]["Item_Rate"]).ToString("N2");
                   
                    Dr.EndEdit();
                    DtTemp.Rows.Add(Dr);
                }

                ViewState["dt"] = DtTemp;
                ViewState["newNdt"] = DtTemp;
                BindGrid();
                imgBtnExcel.Visible = true;
            }
            else
            {
                ViewState["dt"] = null;
                BindGrid();
                imgBtnExcel.Visible = false;
            }

        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RateMasterFTList.xls"));
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

        #region Grid Event
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < grdMain.Columns.Count; i++)
                {
                    // e.Row.Cells[i].ToolTip = grdMain.Columns[i].HeaderText;
                    if (grdMain.Columns[i].HeaderText == "Action")
                    {
                        e.Row.Cells[i].ToolTip = "Action";
                    }
                    else if (grdMain.Columns[i].HeaderText == "Sr.No.")
                    {
                        e.Row.Cells[i].ToolTip = "Serial No.";
                    }
                    else if (grdMain.Columns[i].HeaderText == "Date")
                    {
                        e.Row.Cells[i].ToolTip = "Rate Date";
                    }
                    else if (grdMain.Columns[i].HeaderText == "ToCity")
                    {
                        e.Row.Cells[i].ToolTip = "To City";
                    }
                    else if (grdMain.Columns[i].HeaderText == "I.Rate")
                    {
                        e.Row.Cells[i].ToolTip = "Rate of Item";
                    }

                    
                    totrecords = grdMain.Rows.Count + 1;
                  
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotRecords = (Label)e.Row.FindControl("lbltotRecords");
                lbltotRecords.Text = Convert.ToString(totrecords);


            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
            DtTemp = (DataTable)ViewState["dt"];
            if (e.CommandName == "cmdedit")
            {
                EditGridFunction();
            }
            else if (e.CommandName == "cmddelete")
            {
                DataRow dr = DtTemp.Select("id=" + ViewState["id"].ToString()).Single();
                dr.Delete();
                DtTemp.AcceptChanges();

                ViewState["dt"] = DtTemp;
                this.BindGrid();
            }

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion

        #region Button Event
        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (ddlLorryType.SelectedIndex > 0)
            {
                if (txtfrghtAmount.Text == string.Empty || Convert.ToDouble(txtfrghtAmount.Text) <= 0)
                {
                    this.ShowMessage(" Freight Amount must be greater than 0!"); txtfrghtAmount.Focus(); txtfrghtAmount.SelectText();
                    return;
                }

            }
            else
            {
                this.ShowMessage(" please Select Lorry Type"); ddlLorryType.Focus();
                return;
            }
            if (Hidrowid.Value != string.Empty)
            {
                DtTemp = (DataTable)ViewState["dt"];
                if (DtTemp != null)
                {
                    foreach (DataRow dtrow in DtTemp.Rows)
                    {
                        if (Convert.ToString(dtrow["id"]) == Convert.ToString(Hidrowid.Value))
                        {

                            dtrow["City_FIdno"] = drpBaseCity.SelectedValue;
                            dtrow["City_FName"] = drpBaseCity.SelectedItem.Text;
                            dtrow["Lorry_Idno"] = ddlLorryType.SelectedValue;
                            dtrow["Lorry_Type"] = ddlLorryType.SelectedItem.Text;
                            dtrow["Rate_Date"] = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)).ToString();
                            dtrow["ToCity_Idno"] = ddlCity.SelectedValue;
                            dtrow["City_Name"] = ddlCity.SelectedItem.Text;
                            dtrow["Item_Rate"] = txtfrghtAmount.Text;
                            lnkbtnSubmit.Focus();
                        }
                    }
                }
            }
            else
            {
                DtTemp = (DataTable)ViewState["dt"];
                if ((DtTemp != null) && (DtTemp.Rows.Count > 0))
                {
                    foreach (DataRow row in DtTemp.Rows)
                    {
                        if ((Convert.ToInt32(row["ToCity_Idno"]) == Convert.ToInt32(ddlCity.SelectedValue)) && (Convert.ToDateTime(row["Rate_Date"]).ToString("dd-MM-yyyy") == Convert.ToString(txtDateRate.Text)))
                        {
                            msg = "Same city and date can not be selected!";
                            txtDateRate.Focus();
                            this.ShowMessage(msg);
                            return;
                        }
                    }
                }
                else
                { DtTemp = CreateDt(); }
                int id = DtTemp.Rows.Count == 0 ? 1 : DtTemp.Rows.Count + 1;
                ApplicationFunction.DatatableAddRow(DtTemp, id, drpBaseCity.SelectedValue, drpBaseCity.SelectedItem.Text, ddlLorryType.SelectedValue, ddlLorryType.SelectedItem.Text, Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)), ddlCity.SelectedValue, ddlCity.SelectedItem.Text, txtfrghtAmount.Text);
                ViewState["dt"] = DtTemp;
            }

            this.BindGrid();
            this.ClearItems();
            lnkbtnSubmit.Focus();
        }
        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            ClearItems();
            BindGridDB();

        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            if (Hidrowid.Value == string.Empty)
                ClearItems();
            else
                EditGridFunction();

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            RateMastFT objRGH = new RateMastFT();
            objRGH.LorryTyp_idno = Convert.ToInt32(ddlLorryType.SelectedValue);
            objRGH.Loc_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
            List<RateMastFT> RgDlst = new List<RateMastFT>();
            Int64 RateIdno = 0; bool isinsert = false;
            DtTemp = (DataTable)ViewState["dt"];
            if (DtTemp == null || DtTemp.Rows.Count <= 0)
            {
                ShowMessage("Please enter details");
                return;
            }
            RateMastFTDAL obj = new RateMastFTDAL();
            if (Convert.ToInt32(ddlLorryType.SelectedValue) <= 0)
            {
                ShowMessage("Please select Lorry Type"); ddlLorryType.Focus();
                return;
            }
            else
            {
                using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                {
                    int value = obj.Delete(Convert.ToInt32(ddlLorryType.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue));
                    if (value > 0)
                    {
                        isinsert = obj.Insert(DtTemp, Convert.ToInt32(ddlDateRange.SelectedValue));
                        this.Clear();

                    }
                    else
                    {
                       
                        ShowMessage("Record not saved successfully");
                    }

                    obj = null;
                    if (isinsert == true)
                    {
                        Tran.Complete();
                        ShowMessage("Record save successfully");
                        Clear();
                    }
                    else
                    {
                        Tran.Dispose();
                        ShowMessage("Record not saved successfully");
                    }
                }
            }
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            RateMastFTDAL objRateMst = new RateMastFTDAL();
            DataTable dt = new DataTable();
            dt = objRateMst.SelectDBDataExport(Convert.ToInt64(ddlLorryType.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), ApplicationFunction.ConnectionString());
            Export(dt);

        }

        #endregion

        #region Control Event.....

        protected void ddlLorryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLorryType.SelectedIndex > 0)
            {
                BindGridDB();
                txtDateRate.Focus();

            }
            else
            {
                ViewState["dt"] = null;
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
            txtDateRate.Focus();
        }

        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpBaseCity.SelectedIndex > 0)
            {
                ClearAll();
                grdMain.DataSource = null;
                grdMain.DataBind();
                drpBaseCity.SelectedValue = HidFrmCityIdno.Value;
                
            }
            else if (drpBaseCity.SelectedIndex <= 0)
            {
                ddlLorryType.SelectedIndex = 0;
            }
            ddlLorryType.Focus();
        }

      
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            drpBaseCity.Focus();
        }
        #endregion

       
       
    }
 }
