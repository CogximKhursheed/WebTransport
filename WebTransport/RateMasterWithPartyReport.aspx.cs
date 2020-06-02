using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using System;
using System.IO;

namespace WebTransport
{
    public partial class RateMasterWithPartyReport : Pagebase
    {
        #region Private Variables...
        DataTable DtTemp = new DataTable(); string con = ""; Int32 iFromCity = 0; Int32 totrecords = 0;
        private int intFormId = 65;
        private bool IsWeight = true;
        DataTable dtDelete = new DataTable(); DataRow DrDel;
        DataTable dtGrid = new DataTable();
        Double TotalRate = 0.00;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            con = ApplicationFunction.ConnectionString();
            if (Request.QueryString["FTyp"] != null)
            {
                HidInvoiceTyp.Value = Convert.ToString(Request.QueryString["FTyp"]);
            }
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = false;
                }
                this.BindDateRange();
                ddlDateRange.SelectedIndex = 0;
                Int32 intYearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
                this.BindItems();
                
                this.BindParty();
                SetDate();
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
                imgBtnExcel.Visible = false;
                
            }


              txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
              txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
   
            HidFrmCityIdno.Value = drpBaseCity.SelectedValue;
            ddlDateRange.Focus();
            userpref();
            
        }
        #endregion

        #region Functions...
        private void BindParty()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var senderLst = obj.BindSender();
            ddlPartyName.DataSource = senderLst;
            ddlPartyName.DataTextField = "Acnt_Name";
            ddlPartyName.DataValueField = "Acnt_Idno";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
           
        }
        private void ClearItems()
        {
            Hidrowid.Value = string.Empty;

        }
        private void userpref()
        {
            RateMasterDAL objGrprepDAL = new RateMasterDAL();
            tblUserPref userpref = objGrprepDAL.selectuserpref();
            iFromCity = Convert.ToInt32(userpref.BaseCity_Idno);
            IsWeight = Convert.ToBoolean(userpref.WeightWise_Rate);
        }
        private void Clear()
        {
            hidrateid.Value = string.Empty;
            drpBaseCity.SelectedValue = "0";
            ddlPartyName.SelectedValue = "0";
            ddlItemName.SelectedValue = "0";
           
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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
        public void BindItems()
        {
            RateMasterDAL obj = new RateMasterDAL();
            var lst = obj.GetItems();
            ddlItemName.DataSource = lst;
            ddlItemName.DataTextField = "Item_Name";
            ddlItemName.DataValueField = "Item_Idno";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
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
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = hidmaxdate.Value;
                }
                else
                {
                    txtDateFrom.Text = hidmindate.Value;
                    txtDateTo.Text = hidmaxdate.Value;
                }
            }

        }

        private void BindGrid()
        {
            Int64 Loc_id = Convert.ToInt64(drpBaseCity.SelectedValue);
            Int64 Item_id = Convert.ToInt64(ddlItemName.SelectedValue);
            Int64 Party_Id = Convert.ToInt64(ddlPartyName.SelectedValue);
            RateMasterDAL obj = new RateMasterDAL();
            dtGrid = obj.SelectPartyItemRateListReport("SelectRateRep", Convert.ToInt32(ddlDateRange.SelectedValue), Convert.ToString(txtDateFrom.Text.Trim()), Convert.ToString(txtDateTo.Text), Convert.ToInt32(ddlPartyName.SelectedValue), Loc_id,Item_id, ApplicationFunction.ConnectionString());

            if (dtGrid != null)
            {
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(dtGrid.Rows.Count);
                //GridDiv.Visible = true;
                grdMain.DataSource = dtGrid;
                grdMain.DataBind();
                imgBtnExcel.Visible = true;
                
           
            }
            else
            {
                lblTotalRecord.Text = "0";
                grdMain.DataSource = null;
                grdMain.DataBind();
                imgBtnExcel.Visible = false;
            }
        }

        
        private void CalculateTotal(DataTable dt)
        {
            TotalRate = Convert.ToDouble(dt.Compute("Sum(Item_Rate)", ""));
        }
        #endregion

        #region Control Events...

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        protected void drpBaseCity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalRecord.Text = "T. Record(s) : 0";
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
                for (int i = 0; i < grdMain.Columns.Count; i++)
                {
                    if (IsWeight == true)
                    {
                        grdMain.HeaderRow.Cells[7].Visible = true;
                        grdMain.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdMain.HeaderRow.Cells[7].Visible = false;
                        grdMain.Columns[7].Visible = false;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (dtGrid != null)
                    CalculateTotal(dtGrid);
                Label lblTotOpening = (Label)e.Row.FindControl("lblItemTotalRate");
                //lblTotOpening.Text = string.Format("{0:0,0.00}", TotalRate.ToString());
                lblTotOpening.Text = String.Format("{0:0,0.00}", Convert.ToDouble(TotalRate));
            }

        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            if (ddlPartyName.SelectedValue == "0")
            {
                ShowMessageErr("Please Select Party!");
                return;
            }
            BindGrid();
        }
        #endregion

        #region Exel..
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
        }
        private void PrepareGridViewForExport(System.Web.UI.Control gv)
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

        private void ExportGridView()
        {
            string attachment = "attachment; filename=RateMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        #endregion
    }
}