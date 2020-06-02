using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.IO;
using System.Transactions;
using System.Data;

namespace WebTransport
{
    public partial class ManageEmployee : Pagebase
    {
        #region Private Variable....
        private int intFormId = 44;
        #endregion

        #region PageLoad Event ...
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
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                txtEmpName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                txtEmpName.Focus();
                this.BindDesignation();
                this.Countall();
            }
        }
        #endregion
        
        #region Bind Event  and Function...
        public void Countall()
        {
            EmployeeMasterDAL obj = new EmployeeMasterDAL();
            Int64 count = obj.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void BindDesignation()
        {
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();
            var objDesignRights = objclsDesigRightsDAL.SelectDesignation();
            objclsDesigRightsDAL = null;
            ddlDesignation.DataSource = objDesignRights;
            ddlDesignation.DataTextField = "Desig_Name";
            ddlDesignation.DataValueField = "Desig_Idno";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("< Choose >", "0"));
        }
        private void BindGrid()
        {
            WebTransport.DAL.EmployeeMasterDAL objEmployeeMaster = new WebTransport.DAL.EmployeeMasterDAL();
            string strEmpName = string.IsNullOrEmpty(txtEmpName.Text.Trim()) ? "" : (txtEmpName.Text.Trim());
            Int32 DesigId = Convert.ToInt32(ddlDesignation.SelectedValue) > 0 ? Convert.ToInt32(ddlDesignation.SelectedValue) : 0;
            var lstGridData = objEmployeeMaster.Select(strEmpName, DesigId);
            objEmployeeMaster = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                  
                DataTable dt = new DataTable();
                dt.Columns.Add("S.No", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Date Of Join", typeof(string));
                dt.Columns.Add("User Name", typeof(string));
                dt.Columns.Add("Designation", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                int c = 0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["S.No"] = ++c;
                    dr["Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Name"));
                    dr["Email"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Email"));
                    dr["Date Of Join"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "DOJ")).ToString("dd-MM-yyyy");
                    dr["User Name"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Username"));
                    dr["Designation"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Desig_Name"));

                    if (Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "IsActive")) == true)
                    {
                        dr["Status"] = "Active";
                    }
                    else
                    {
                        dr["Status"] = "Inactive";
                    }
                    dt.Rows.Add(dr);

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }



                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0 ";
                imgBtnExcel.Visible = false;
                divpaging.Visible = false;
                lblcontant.Visible = false;
            }
            foreach (GridViewRow row in grdMain.Rows)
            {
                Literal ltrlHide = (Literal)row.FindControl("ltrlHide");
                Literal ltrlIsActive = (Literal)row.FindControl("ltrlIsActive");
                if (ltrlHide.Text == "False")
                {
                    ltrlIsActive.Text = "Inactive";
                }
                else
                {
                    ltrlIsActive.Text = "Active";
                }
            }
        }

      
        #endregion

        #region Grid Event ...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("EmployeeMaster.aspx?Emp_Idno=" + e.CommandArgument, true);
            }
            //if (e.CommandName == "cmddelete")
            //{
            //    Int32 intCompIdno = 1;
            //    AutomobileOnline.DAL.clsEmployeeMaster objEmployeeMaster = new AutomobileOnline.DAL.clsEmployeeMaster();
            //    int value = objEmployeeMaster.Delete(Convert.ToInt32(e.CommandArgument), intCompIdno);
            //    objEmployeeMaster = null;
            //    if (value > 0)
            //    {
            //        this.BindGrid();
            //    }
            //}
            else if (e.CommandName == "cmdstatus")
            {
                Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
                int intDesgID = 0;
                bool bStatus = false; string strMsg = string.Empty;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intDesgID = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    WebTransport.DAL.EmployeeMasterDAL objEmployeeMaster = new WebTransport.DAL.EmployeeMasterDAL();
                    int value = objEmployeeMaster.UpdateStatus(intDesgID, bStatus, empIdno);
                    objEmployeeMaster = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Employee Status updated successfully.";
                        txtEmpName.Focus();
                    }
                    else
                    {
                        strMsg = "Employee Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                HiddenField hidEmpId = (HiddenField)e.Row.FindControl("hidEmpId");
                //  ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {
                    imgBtnStatus.Visible = true;
                    lnkbtnEdit.Visible = true;
                    grdMain.Columns[6].Visible = false;
                }
                //if (base.Delete == false)
                //{
                //    imgBtnDelete.Visible = false;
                //}

                if ((string.IsNullOrEmpty(Convert.ToString(hidEmpId.Value)) ? 0 : Convert.ToInt64(hidEmpId.Value)) == 1)
                {
                    imgBtnStatus.Visible = false;
                }

                if ((base.Edit == false) && (base.Delete == false))
                {
                    grdMain.Columns[7].Visible = false;
                }
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }
        }

        #endregion

        #region Button Event ...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
            txtEmpName.Focus();
        }
        #endregion

        #region print...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "EmployeeMaster.xls"));
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
        private void ExportGridView()
        {
            //string attachment = "attachment; filename=ManageEmployeeReport.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //grdMain.Columns[8].Visible = false;
            //grdMain.Columns[9].Visible = false;
            //grdMain.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
            //grdMain.Columns[8].Visible = true;
            //grdMain.Columns[9].Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

    }
}