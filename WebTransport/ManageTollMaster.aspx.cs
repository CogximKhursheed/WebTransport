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
    public partial class ManageTollMaster : Pagebase
    {
        #region Private Variable....
        private int intFormId = 14;
        #endregion

        #region Page Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            txttollname.Attributes.Add("onkeypress", "return allowAlphabetAndNumer(event);");

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
                txttollname.Focus();
                BindCity();
                BindLorryType();
                TotalRecords();
                
            }
        }

        #endregion

        private void BindCity()
        {
            TollMastDAL objTollMastDAL = new TollMastDAL();
            var lst = objTollMastDAL.BindCityAll();
            drpCity.DataSource = lst;
            drpCity.DataTextField = "City_Name";
            drpCity.DataValueField = "City_Idno";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("< Choose City >", "0"));


            ddlToCity.DataSource = lst;
            ddlToCity.DataTextField = "City_Name";
            ddlToCity.DataValueField = "City_Idno";
            ddlToCity.DataBind();
            ddlToCity.Items.Insert(0, new ListItem("< Choose City >", "0"));
        }

        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TollMaster.xls"));
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

        private void TotalRecords()
        {
            using (TransportMandiEntities db = new TransportMandiEntities(MultipleDBDAL.strDynamicConString()))
            {
                lblTotalRecord.Text = "T. Record (s): " + Convert.ToString((from CT in db.tbltollmasters
                                                                            join sm in db.tblCityMasters on CT.city equals sm.City_Idno
                                                                            join sm2 in db.tblCityMasters on CT.Tocity equals sm2.City_Idno into gj
                                                                            from full in gj.DefaultIfEmpty()
                                                                            select CT).Count());

            }
        }
        private void BindLorryType()
        {
            TollMastDAL Obj = new TollMastDAL();
            var EMPS = Obj.SelectLorryType();
            ddllorytype.DataSource = EMPS;
            ddllorytype.DataTextField = "Lorry_Type";
            ddllorytype.DataValueField = "Id";
            ddllorytype.DataBind();
            ddllorytype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            Obj = null;
        }
        private void BindGrid()
        {
            var city = 0;
            if (drpCity.SelectedValue != "")
            {
                city = Convert.ToInt32(drpCity.SelectedValue);
            }
            TollMastDAL objDrivMast = new TollMastDAL();
            var lstGridData = objDrivMast.SelectForSearch(Convert.ToInt32(city), Convert.ToInt32(ddlToCity.SelectedValue), Convert.ToString(txttollname.Text.Trim()),Convert.ToInt32(ddllorytype.SelectedValue));
            objDrivMast = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("FromCity", typeof(string));
                dt.Columns.Add("ToCity", typeof(string));
                dt.Columns.Add("TollName", typeof(string));
                dt.Columns.Add("LorryType", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                dt.Columns.Add("Status", typeof(string));
              
                double TNet = 0; 
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["FromCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "CityName"));
                    dr["ToCity"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "ToCityName"));
                    dr["TollName"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TollTaxName"));
                    dr["LorryType"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_Type"));
                    dr["Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Ammount")).ToString("N2");
                    dr["Status"] = Convert.ToBoolean(DataBinder.Eval(lstGridData[i], "Status"))==true?"Active":"InActive";
                    dt.Rows.Add(dr);

                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Ammount"));
                    if (i == lstGridData.Count - 1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["LorryType"] = "Total";
                        drr["Amount"] = (TNet).ToString("N2");
                        dt.Rows.Add(drr);
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["Dt"] = dt;
                }


                //
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                Double TotalNetAmount = 0;

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Ammount"));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

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
                lblcontant.Visible = false;
                divpaging.Visible = false;
            }
        }
        #endregion

        #region Grid Events....
        Double drLblNet = 0;
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
                Response.Redirect("TolTaxMaster.aspx?TollTaxId=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
                TollMastDAL objTollMast = new TollMastDAL();
                Int32 intValue = objTollMast.Delete(Convert.ToInt32(e.CommandArgument));
                objTollMast = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    txttollname.Focus();
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
                int intTollIdno = 0;
                bool bStatus = false;
                string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strStatus.Length > 1)
                {
                    intTollIdno = Convert.ToInt32(strStatus[0]);
                    if (Convert.ToBoolean(strStatus[1]) == true)
                        bStatus = false;
                    else
                        bStatus = true;
                    TollMastDAL objDrivMast = new TollMastDAL();
                    int value = objDrivMast.UpdateStatus(intTollIdno, bStatus, empIdno);
                    objDrivMast = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Status updated successfully.";
                        txttollname.Focus();
                    }
                    else
                    {
                        strMsg = "Status not updated.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                }
            }
            txttollname.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));


                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                Int32 Tollid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TollTaxid"));
             

                drLblNet += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Ammount"));

                base.CheckUserRights(intFormId);
                if (base.Edit == false)
                {

                    imgBtnStatus.Visible = true;
                    lnkbtnEdit.Visible = false;
                    grdMain.Columns[3].Visible = false;
                }
                if (base.Delete == false)
                {
                    if (Tollid >0)
                    {
                        TollMastDAL obj = new TollMastDAL();
                        var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(Tollid));
                        if (ItemExist != null && ItemExist.Count > 0)
                        {
                            lnkbtnDelete.Visible = false;
                        }
                        else
                        {
                            lnkbtnDelete.Visible = true;
                        }
                   }
                }
                if ((base.Edit == false) && (base.Delete == false))
                {
                    grdMain.Columns[4].Visible = false;
                }
                if (Tollid > 0)
                {
                    TollMastDAL obj = new TollMastDAL();
                    var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(Tollid));
                    if (ItemExist != null && ItemExist.Count > 0)
                    {
                        lnkbtnDelete.Visible = false;
                    }
                    else
                    {
                        lnkbtnDelete.Visible = true;
                    }
                }
                if (status == false)
                    imgBtnStatus.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnStatus.ImageUrl = "~/Images/active.png";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalNet = (Label)e.Row.FindControl("lblTotalNet");
                lblTotalNet.Text = drLblNet.ToString("N2");
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