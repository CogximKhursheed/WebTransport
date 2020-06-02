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
namespace WebTransport
{
    public partial class CommissionMaster : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable();
        DataTable DtTemp1 = new DataTable();
        string con = ""; Int32 iFromCity = 0;
        private int intFormId = 22;
        #endregion

        #region Page Enents...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            con = ApplicationFunction.ConnectionString();
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
                this.BindDateRange();
                ddlDateRange.SelectedIndex = 0;
                hidsave.Value = "0";
               // ddlDateRange_SelectedIndexChanged(null, null);
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                this.BindItems();
                BindState();
                txtCommsision.Enabled = true;
                userpref();
                drpBaseCity.SelectedValue = Convert.ToString(iFromCity);
                txtDateRate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtCommsision.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
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

        #region Button Evnets...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlItemName.SelectedValue) <= 0)
            {
                ShowMessageErr("Please select Item!"); ddlItemName.Focus();
                return;
            }
            if (Convert.ToInt32(ddlState.SelectedValue) <= 0)
            {
                ShowMessageErr("Please select State!"); ddlState.Focus();
                return;
            }
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            if (grdMain.Rows.Count > 0)
            { 
                CommissionMasterDAL obj = new CommissionMasterDAL(); bool isinsert = false;
                tblCommmissionMastHead objRGH = new tblCommmissionMastHead();
                objRGH.Emp_Idno = empIdno;
                objRGH.Item_Idno = Convert.ToInt32(ddlItemName.SelectedValue);
                objRGH.Date = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text));
                objRGH.Com_Type = Convert.ToString(ddlType.SelectedIndex);
                objRGH.Year_Idno = Convert.ToInt32(ddlDateRange.SelectedValue);
                objRGH.FromCity_Idno = Convert.ToInt32(drpBaseCity.SelectedValue);
                objRGH.State_Idno = Convert.ToInt32(ddlState.SelectedValue);
                DtTemp = CreateDt();
                hidsave.Value = "1";
                int Id = 1;
                    foreach (GridViewRow dr in grdMain.Rows)
                    {
                        HiddenField hidTocity_Idno = (HiddenField)dr.FindControl("hidTocity_Idno");
                        TextBox txtCommissionAmnt = (TextBox)dr.FindControl("txtCommissionAmnt");
                        Label lblCityName = (Label)dr.FindControl("lblCityName");
                        if (Convert.ToDouble(Request.Form[txtCommissionAmnt.UniqueID]) >= 0)
                        {
                            Double amt = Convert.ToDouble(Request.Form[txtCommissionAmnt.UniqueID]);
                            ApplicationFunction.DatatableAddRow(DtTemp, Id, hidTocity_Idno.Value, lblCityName.Text, Request.Form[txtCommissionAmnt.UniqueID]);
                        }
                        Id++;
                    }
                //Int64 HeadId = obj.HeadId(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(ddlState.SelectedValue),Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)));
                if (Convert.ToInt32(ddlItemName.SelectedValue) <= 0)
                {
                    ShowMessageErr("Please select Item"); ddlItemName.Focus();
                    return;
                }
                else
                {
                    Int64 value = obj.Delete(Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(drpBaseCity.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateRate.Text)));
                    if (value > 0)
                    {
                        isinsert = obj.Insert(DtTemp, objRGH);
                        this.Clear();
                    }
                    else
                    {
                        ShowMessageErr("Record not saved successfully");
                    }
                    obj = null;
                    if (isinsert == true)
                    {
                        ShowMessage("Record save successfully");
                    }
                    else
                    {
                        ShowMessageErr("Record not saved succesfully");
                    }
                }
            }
            else
            {
                ShowMessage("Please Enter Details");
                ddlState.Focus();
                return;
            }
        }
        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if ((drpBaseCity.SelectedIndex > 0) && (ddlItemName.SelectedIndex > 0) && (ddlState.SelectedIndex > 0))
            {
                //ddlState_SelectedIndexChanged(null, null);
                this.Clear();
            }
            else
            {
                this.ClearItems();
            }
        }

        //protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        //{
        //    ClearItems();
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtCommsision.Text == "")
            {
                txtCommsision.Text = "0.00";
            }
            int Id = 1;
            txtCommsision.Text = Convert.ToDouble(txtCommsision.Text).ToString("N2");
            if (Convert.ToDouble(txtCommsision.Text) > 0)
            {
                if (grdMain.Rows.Count > 0)
                {
                    DtTemp = CreateDt();

                    int a = grdMain.PageIndex;
                    for (int i = 0; i < grdMain.PageCount; i++)
                    {
                        grdMain.SetPageIndex(i);
                        foreach (GridViewRow dr in grdMain.Rows)
                        {
                            HiddenField hidTocity_Idno = (HiddenField)dr.FindControl("hidTocity_Idno");
                            TextBox txtCommissionAmnt = (TextBox)dr.FindControl("txtCommissionAmnt");
                            Label lblCityName = (Label)dr.FindControl("lblCityName");
                            txtCommissionAmnt.Text = Convert.ToDouble(txtCommsision.Text).ToString("N2");
                            ApplicationFunction.DatatableAddRow(DtTemp,Id, hidTocity_Idno.Value, lblCityName.Text, txtCommissionAmnt.Text);
                            Id++;
                        }
                        
                    }
                    grdMain.SetPageIndex(a);
                    ViewState["dt"] = DtTemp;
                    grdMain.DataSource = DtTemp;
                    grdMain.DataBind();
                }

                else
                {
                    ShowMessageErr("Please enter details for Save Data");
                    ddlState.Focus(); txtCommsision.Text = "0.00";
                    return;
                }
            }
            else
            {
                ShowMessage("Please enter Amount Greater Than Zero");
                txtCommsision.Focus();
            }
        }
        protected void imgBtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommissionMaster.aspx");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            BindGridDB();
        }
        #endregion

        #region Functions...
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void BindState()
        {
            CommissionMasterDAL obj = new CommissionMasterDAL();
            var lst = obj.selectState();
            obj = null;
            ddlState.DataSource = lst;
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Idno";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindGridDB()
        {
            CommissionMasterDAL objRateMst = new CommissionMasterDAL();
            DataTable dt1 = new DataTable();
            dt1 = objRateMst.SelectDBData(Convert.ToInt64(ddlItemName.SelectedValue), Convert.ToInt64(drpBaseCity.SelectedValue), Convert.ToInt64(ddlState.SelectedValue), Convert.ToInt32(ddlDateRange.SelectedValue), ApplicationFunction.ConnectionString(),Convert.ToString(txtDateRate.Text.Trim()));
            if (dt1!= null)
            {
                grdMain.DataSource = dt1;
                grdMain.DataBind();
                Grid.Visible = true;
            }
            else
            {
                Grid.Visible = false;
                grdMain.DataSource =null;
                grdMain.DataBind();
            }
        }

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

        private void ClearAll()
        {
            ddlState.SelectedValue = "0";
            ddlItemName.SelectedValue = "0";
            txtDateRate.Text = "0.00";
        }
        public void BindItems()
        {
            CommissionMasterDAL obj = new CommissionMasterDAL();
            var lst = obj.GetItems();
            obj = null;
            ddlItemName.DataSource = lst;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_Idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private DataTable CreateDt()
        {
            DataTable dttemp = ApplicationFunction.CreateTable("tbl", "Id", "String" ,"Tocity_Idno", "String", "City_Name", "String", "Comision_Amnt", "String");
            return dttemp;
        }
        private void ClearItems()
        {
            ddlState.SelectedValue = "0";
            txtDateRate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtCommsision.Enabled = true;
        }
        private void userpref()
        {
            CommissionMasterDAL objGrprepDAL = new CommissionMasterDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
        }
        private void Clear()
        {
            drpBaseCity.SelectedValue = "0";
            ddlItemName.SelectedValue = "0";
            //  ViewState["dt"] = null;
            // DtTemp = null;
            hidrateid.Value = string.Empty;

            txtCommsision.Text = "0.00";
            ddlState.SelectedValue = "0";

            //  BindGrid();
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        private void BindGrid1st()
        {
            if (Convert.ToDouble(txtCommsision.Text) > 0.00)
            {
                if (ddlType.SelectedValue == "1")
                {
                    foreach (GridViewRow dr in grdMain.Rows)
                        {
                            TextBox txtCommissionAmnt = (TextBox)dr.FindControl("txtCommissionAmnt");
                            txtCommissionAmnt.Text = txtCommsision.Text.Trim();
                        }
                }
            }
        }
        private void BindGrid()
        {
            if (ddlType.SelectedValue == "1")
            {
                grdMain.DataSource = (DataTable)ViewState["dt"];
                grdMain.DataBind();
            }
            else
            {
                ViewState["dt"] = DtTemp1;
                grdMain.DataSource = (DataTable)ViewState["dt"];
                grdMain.DataBind();
            }
           
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
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
                    txtDateRate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDateRate.Text = hidmindate.Value;
                }
            }

        }
        #endregion

        #region Control Events...
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "1")
                txtCommsision.Enabled = true;
            else
            {
                txtCommsision.Enabled = false;
                txtCommsision.Text = "0.00";
            }
            ddlItemName.SelectedValue="0";
            ddlState.SelectedValue = "0";
            drpBaseCity.SelectedValue = "0";
            grdMain.DataSource = null;
            grdMain.DataBind();
            Grid.Visible = false;
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            Grid.Visible = false;
        }
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMain.DataSource = null;
            grdMain.DataBind();
            Grid.Visible = false;
        }
        //protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetDate();
        //    ddlDateRange.Focus();
        //}
        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpBaseCity.SelectedIndex > 0)
            {
                ViewState["dt"] = null;
                grdMain.DataSource = null;
                grdMain.DataBind();
                Grid.Visible = false;
            }
            else if (drpBaseCity.SelectedIndex <= 0)
            {
                ddlState.SelectedIndex = 0;
            }
            ddlState.Focus();
        }
        protected void txtCommsision_TextChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "1")
            {
                if (Convert.ToDouble(txtCommsision.Text.Trim()) > 0.00)
                    BindGrid1st();
            }

        }

        protected void imgPreviousDate_Click(object sender, ImageClickEventArgs e)
        {
            CommissionMasterDAL obj = new CommissionMasterDAL();
            var lst = obj.PreviousDateList();
            if (lst != null)
            {
                grdPreviusDates.DataSource = lst;
                grdPreviusDates.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JScript", "openModal();", true);
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //for (int i = 0; i < grdMain.Columns.Count; i++)
                //{
                //    // e.Row.Cells[i].ToolTip = grdMain.Columns[i].HeaderText;
                //    if (grdMain.Columns[i].HeaderText == "Action")
                //    {
                //        e.Row.Cells[i].ToolTip = "Action";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "Sr.No")
                //    {
                //        e.Row.Cells[i].ToolTip = "Serial No.";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "RateDate")
                //    {
                //        e.Row.Cells[i].ToolTip = "Rate Date";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "ToCity")
                //    {
                //        e.Row.Cells[i].ToolTip = "To City";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "ItemRate")
                //    {
                //        e.Row.Cells[i].ToolTip = "Rate of Item";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "IW.Rate")
                //    {
                //        e.Row.Cells[i].ToolTip = "Weight Rate of Item";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "QS.Limit")
                //    {
                //        e.Row.Cells[i].ToolTip = "Shortage Limit of Quantity";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "QS.Rate")
                //    {
                //        e.Row.Cells[i].ToolTip = "Shortage Rate of Quantity";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "WS.Limit")
                //    {
                //        e.Row.Cells[i].ToolTip = "Shortage Limit of Weight";
                //    }
                //    else if (grdMain.Columns[i].HeaderText == "WS.Rate")
                //    {
                //        e.Row.Cells[i].ToolTip = "Shortage Weight of Rate";
                //    }
                //}
                TextBox txtCommissionAmnt = (TextBox)e.Row.FindControl("txtCommissionAmnt");
                if (ddlType.SelectedValue == "1")
                {
                    //txtCommissionAmnt.Enabled = false;
                    txtCommissionAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                }
                else
                {
                   txtCommissionAmnt.Attributes.Add("onkeypress", "return allowOnlyFloatNumber(event);");
                }
            }

        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ddlType.SelectedValue == "1")
            {
                grdMain.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            else
            {
                DtTemp1 = (DataTable)ViewState["dt"];
                DataColumn[] keyColumns = new DataColumn[1];
                keyColumns[0] = DtTemp1.Columns["Tocity_Idno"];
                DtTemp1.PrimaryKey = keyColumns;
                foreach (GridViewRow grv in this.grdMain.Rows)
                {
                    DataRow dRow = DtTemp1.Rows.Find(this.grdMain.DataKeys[grv.RowIndex].Value);
                    dRow["Comision_Amnt"] = ((TextBox)grv.FindControl("txtCommissionAmnt")).Text;
                }
                grdMain.PageIndex = e.NewPageIndex;
                BindGrid();
            }
           

        }
        #endregion
        
    }
}




