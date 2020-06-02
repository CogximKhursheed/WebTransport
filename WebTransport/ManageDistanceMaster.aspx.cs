using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls; 
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;

namespace WebTransport
{
    public partial class ManageDistanceMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 8;
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
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                //if (base.Print == false)
                //{
                //    imgBtnExcel.Visible = false;
                //}
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    this.BindCity();
                }
                else
                {
                    this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                this.ToCity();
                ddlFromCity.Focus();
                TotalRecord();

            }
        }
        #endregion

        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DistanceMaster.xls"));
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

        public void TotalRecord()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                lblTotalRecord.Text = "T. Record (s): " + ((from Dist in db.DistanceMasts
                                                               join F in db.tblCityMasters on Dist.FrmCity_Idno equals F.City_Idno
                                                               join T in db.tblCityMasters on Dist.ToCity_Idno equals T.City_Idno
                                                               join V in db.tblCityMasters on Dist.ViaCity_Idno equals V.City_Idno
                                                               select Dist).Count());

            }
        }
        private void BindGrid()
        {
            Int64 intFromCityIdno = Convert.ToInt64(ddlFromCity.SelectedValue);
            Int64 intToCityIdno = Convert.ToInt64(ddlToCity.SelectedValue);
            DistanceMastDAL objItemMast = new DistanceMastDAL();
            var lstGridData = objItemMast.SelectForSearch(intFromCityIdno, intToCityIdno);
            objItemMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("ViaCity", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("Kms", typeof(string));
                dt.Columns.Add("Status", typeof(string));
              
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "FromCityName"));
                    dr["ViaCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ViaCityName"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ToCity"));
                    dr["Kms"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "KMs"));
                    dr["Status"] = Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status")) == true ? "Active" : "InActive";
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }

                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
                imgBtnExcel.Visible = true;

            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                lblcontant.Visible = false;
                divpaging.Visible = false;
                imgBtnExcel.Visible = false;
            }
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
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindLocFrom();
            ddlFromCity.DataSource = ToCity;
            ddlFromCity.DataTextField = "city_name";
            ddlFromCity.DataValueField = "city_idno";
            ddlFromCity.DataBind();
            ddlFromCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void ToCity()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var ToCity = obj.BindAllToCity();
            ddlToCity.DataSource = ToCity;
            ddlToCity.DataTextField = "city_name";
            ddlToCity.DataValueField = "city_idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("DistanceMast.aspx?DistmastId=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmdstatus")
            {
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
                    DistanceMastDAL objItemMast = new DistanceMastDAL();
                    int value = objItemMast.UpdateStatus(intitemIdno, bStatus, empIdno);
                    objItemMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        ddlFromCity.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            else if (e.CommandName == "cmddelete")
            {
                DistanceMastDAL objItemMast = new DistanceMastDAL();
                long intValue = objItemMast.Delete(Convert.ToInt32(e.CommandArgument));
                objItemMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlFromCity.Focus();
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

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
                //ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                //ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                //base.CheckUserRights(intFormId);
                //if (base.Edit == false)
                //{
                imgBtnStatus.Visible = true;
                //    imgBtnEdit.Visible = false;
                //    grdMain.Columns[3].Visible = false;
                //}
                //if (base.Delete == false)
                //{
                //    imgBtnDelete.Visible = false;
                //}
                //if ((base.Edit == false) && (base.Delete == false))
                //{
                //    grdMain.Columns[4].Visible = false;
                //}

                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }
        #endregion

        #region Button Event...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }

    }
}