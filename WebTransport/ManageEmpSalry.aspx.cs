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
    public partial class ManageEmpSalry : Pagebase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //}
            if (!IsPostBack)
            {
                txtEmpName.Focus();
            }
        }
        #endregion

        #region ButtonEvents..
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
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
        #endregion

        #region Functions...
        private void BindGrid()
        {
            EmployeeSalaryDAL objSalry = new EmployeeSalaryDAL();
            string strEmpName = txtEmpName.Text.Trim();

            var lstGridData = objSalry.SelectForSearch(strEmpName);

            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("EmpSal_ID", typeof(string));
                dt.Columns.Add("Acnt_Name", typeof(string));
                dt.Columns.Add("EmpSal_Salary", typeof(string));
                dt.Columns.Add("EmpSal_Year", typeof(string));
                dt.Columns.Add("EmpSal_Status", typeof(string));

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpSal_ID"] = Convert.ToString(i + 1);
                    dr["Acnt_Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Acnt_Name"));
                    dr["EmpSal_Salary"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "EmpSal_Salary"));
                    dr["EmpSal_Year"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "EmpSal_Year"));
                    dr["EmpSal_Status"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "EmpSal_Status"));

                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }
                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): " + lstGridData.Count;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                imgBtnExcel.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s): 0 ";
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ItemMaster.xls"));
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
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
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
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("EmployeeSalry.aspx?EmpSal=" + e.CommandArgument, true);
            }
            else if (e.CommandName == "cmdstatus")
            {
                EmployeeSalaryDAL objemp = new EmployeeSalaryDAL();
                int EmpSal_ID = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    EmpSal_ID = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;

                    Int32 value = objemp.UpdateStatus(EmpSal_ID, bStatus);

                    if (value > 0)
                    {
                        this.BindGrid();
                        ShowMessage("Status updated successfully.");

                    }
                    else
                    {
                        ShowMessageErr("Status not updated!");
                    }
                }
            }
            else if (e.CommandName == "cmddelete")
            {

                EmployeeSalaryDAL objsal = new EmployeeSalaryDAL();
                Int32 intValue = objsal.Delete(Convert.ToInt32(e.CommandArgument));

                if (intValue > 0)
                {
                    this.BindGrid();
                    ShowMessage("Record deleted successfully.");
                }
                else
                {
                    ShowMessage("Record not deleted!");
                }
            } 
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "EmpSal_Status"));

                //// Used to hide Delete button if ItemgrpId exists in Rate Master,Goods Received, Quotation,GR Preparation,Commission Master
                //LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                //string ItemIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemIdno"));
                //if (ItemIdno != "")
                //{
                //    ItemMasterDAL obj = new ItemMasterDAL();
                //    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(ItemIdno));
                //    if (ItemExist != null && ItemExist.Count > 0)
                //    {
                //        lnkbtnDelete.Visible = false;
                //    }
                //    else
                //    {
                //        lnkbtnDelete.Visible = true;
                //    }
                //}
                //// end----

                imgBtnStatus.Visible = true;

                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }
        #endregion

    }
}