using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AutomobileOnline.Classes;
using WebTransport.Classes;
using WebTransport.DAL;
using System.IO;
using System.Data;

namespace WebTransport
{
    public partial class ManageDriverMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDriverName.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");
           
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
                txtDriverName.Focus();
                this.countall();
                this.Bindemp();
            }
        }
        public void countall()
        {
            DriverMastDAL objclsCityMaster = new DriverMastDAL();
            Int64 total = 0;

            total = objclsCityMaster.selectcount();
            if (total > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + total;
            }
            else
            {
                lblTotalRecord.Text = "T. Record (s): 0 ";
            }
        }
        private void Bindemp()
        {
            DriverMastDAL objclsCityMaster = new DriverMastDAL();
            var objCityMast = objclsCityMaster.Selectemp();
            objclsCityMaster = null;
            drpguarntor.DataSource = objCityMast;
            drpguarntor.DataTextField = "Emp_Name";
            drpguarntor.DataValueField = "User_Idno";
            drpguarntor.DataBind();
            drpguarntor.Items.Insert(0, new ListItem(" ----Select---- ", "0"));
        }
        #endregion
       
        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DriverMaster.xls"));
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
        private void BindGrid()
        {
            var guarntor=0;
            if(drpguarntor.SelectedValue!="")
            {
                guarntor = Convert.ToInt32(drpguarntor.SelectedValue);
            }
   
            DriverMastDAL objDrivMast = new DriverMastDAL();
            var lstGridData = objDrivMast.SelectForSearch(Convert.ToString(txtDriverName.Text.Trim()), Convert.ToString(txtlicense.Text.Trim()), Convert.ToInt32(drpvarified.SelectedValue), Convert.ToInt32(guarntor));
            objDrivMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("DriverName", typeof(string));
                dt.Columns.Add("License", typeof(string));
                dt.Columns.Add("License Authority", typeof(string));
                dt.Columns.Add("Expiry", typeof(string));
                dt.Columns.Add("AC No", typeof(string));
                dt.Columns.Add("Guarantor", typeof(string));
                dt.Columns.Add("Verified", typeof(string));
                dt.Columns.Add("Status", typeof(string));
               
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["DriverName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "DriverName"));
                    dr["License"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "LicenseNo"));
                    dr["License Authority"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Authority"));
                    dr["Expiry"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "ExpiryDate")).ToString("dd-MM-yyyy");
                    dr["AC No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AccountNo"));
                    dr["Guarantor"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "GuarantorNm"));
                    dr["Verified"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Varified_Nm"));
                    dr["Status"] = Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status"))==true?"Active":"InActive";

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
                imgBtnExcel.Visible = true;

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            {
                grdMain.DataSource =null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): 0" ;
                imgBtnExcel.Visible = false;
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
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
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("DriverMaster.aspx?DriverIdno=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                DriverMastDAL objDrivMast = new DriverMastDAL();
                Int32 intValue = objDrivMast.Delete(Convert.ToInt32(e.CommandArgument));
                objDrivMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txtDriverName.Focus();
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
            else if (e.CommandName == "cmdstatus")
            {
                int intDriverIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intDriverIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    DriverMastDAL objDrivMast = new DriverMastDAL();
                    int value = objDrivMast.UpdateStatus(intDriverIdno, bStatus, empIdno);
                    objDrivMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txtDriverName.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txtDriverName.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
              //  ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
              ////  ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
              //  base.CheckUserRights(intFormId);
              //  if (base.Edit == false)
              //  {
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

                // Used to hide Delete button if ItemgrpId exists in Lorry Master,Challan Booking,Summary Register, Delivery Challan Details 
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                string DriverIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DriverIdno"));
                if (DriverIdno != "")
                {
                    DriverMastDAL obj = new DriverMastDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(DriverIdno));
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
        }
        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
           
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ManageColorMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.AllowPaging = false;
            grdMain.Columns[3].Visible = false;
            grdMain.Columns[4].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

     
        
    }
}