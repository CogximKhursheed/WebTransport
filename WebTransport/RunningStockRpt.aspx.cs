using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class RunningStockRpt : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLorryNumber(); BindTyreCategory(); BindSerialNo(); BindTyrePosition();
            }
        }
        #endregion

        #region Functions..
        private void BindTyreCategory()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            ddlType.DataSource = obj.BindTyreType();
            ddlType.DataTextField = "TyreType";
            ddlType.DataValueField = "TyreTypeIdno";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Type--", "0"));
            obj = null;
        }

        private void BindSerialNo()
        {
            BindDropdownDAL objSerial = new BindDropdownDAL();
            ddlSerialNo.DataSource = objSerial.BindSerialNoForItemFromLM();
            ddlSerialNo.DataTextField = "SerialNo";
            ddlSerialNo.DataValueField = "SerlDetlIdno";
            ddlSerialNo.DataBind();
            ddlSerialNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Serial No--", "0"));
            objSerial = null;
        }

        private void BindTyrePosition()
        {
            BindDropdownDAL objTyre = new BindDropdownDAL();
            var LorrMast = objTyre.SelectTyrePostion();
            ddlTyrePosition.DataSource = LorrMast;
            ddlTyrePosition.DataTextField = "Position_name";
            ddlTyrePosition.DataValueField = "Position_id";
            ddlTyrePosition.DataBind();
            ddlTyrePosition.Items.Insert(0, new ListItem("--Select Position--", "0"));
            objTyre = null;
        }

        private void BindLorryNumber()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var TruckNolst = obj.BindTruckNoPurchase();
            ddlLorryNo.DataSource = TruckNolst;
            ddlLorryNo.DataTextField = "Lorry_No";
            ddlLorryNo.DataValueField = "lorry_idno";
            ddlLorryNo.DataBind();
            ddlLorryNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Lorry No--", "0"));
        }

        private void BindGrid()
        {
            RunningStockReportDAL obj = new RunningStockReportDAL();

            Int32 LorryIdno = Convert.ToInt32(string.IsNullOrEmpty(ddlLorryNo.SelectedValue) ? "0" : ddlLorryNo.SelectedValue);
            Int32 SerialIdno = Convert.ToInt32(string.IsNullOrEmpty(ddlSerialNo.SelectedValue) ? "0" : ddlSerialNo.SelectedValue);
            Int32 TyrePositionIdno = Convert.ToInt32(string.IsNullOrEmpty(ddlTyrePosition.SelectedValue) ? "0" : ddlTyrePosition.SelectedValue);
            Int32 TypeIdno = Convert.ToInt32(string.IsNullOrEmpty(ddlType.SelectedValue) ? "0" : ddlType.SelectedValue);
            string Company = txtCompanyName.Text.Trim();

            var lstGridData = obj.SelectRunningStockReportTyre(LorryIdno, SerialIdno, TyrePositionIdno, TypeIdno, Company);

            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Sr No.", typeof(string));
                dt.Columns.Add("SerialNo", typeof(string));
                dt.Columns.Add("LorryNo", typeof(string));
                dt.Columns.Add("Position", typeof(string));
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("CompanyName", typeof(string));

                double TNet = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Sr No."] = Convert.ToString(i + 1);
                    dr["SerialNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SerialNo"));
                    dr["LorryNo"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LorryNo"));
                    dr["Position"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Position"));
                    dr["type"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "type"));
                    dr["CompanyName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CompanyName"));
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["DtTyre"] = dt;
                }
                //

                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): " + lstGridData.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;

                obj = null;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "Total Record (s): 0 ";

                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RunningStockRpt.xls"));
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

        #region Button Events
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["DtTyre"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DtTyre"];
                Export(dt);
            }
        }
        #endregion

        #region Grid Events....
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        #endregion
    }
}