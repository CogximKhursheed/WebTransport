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
using System.Data.SqlClient;

namespace WebTransport
{
    public partial class ManageLorryHire: Pagebase
    {

    
        #region Private Variable....
        private int intFormId = 28;
        double dfreightAmnt = 0, dNetAmnt = 0, dAdvanceAmnt = 0;
        #endregion
     
        #region Page Load Events...
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                }
                this.BindDateRange();
                this.BindLorry();
                ddlDateRange_SelectedIndexChanged(null, null);
                txtdatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtdateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                if (Convert.ToString(Session["Userclass"]) == "Admin")
                {
                    LorryHireDAL obj = new LorryHireDAL();
                    var lst = obj.SelectCityCombo();
                    obj = null;
                    ddlFromCity.DataSource = lst;
                    ddlFromCity.DataTextField = "City_Name";
                    ddlFromCity.DataValueField = "City_Idno";
                    ddlFromCity.DataBind();
                    ddlFromCity.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    //this.BindCity(Convert.ToInt64(Session["UserIdno"]));
                }
                Countalllorryslip();
                ddlDateRange.Focus();
            }
        }
        #endregion

        #region Functions...
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "LorryMaster.xls"));
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
        public void Countalllorryslip()
        {
           LorryHireDAL obj = new LorryHireDAL();

            Int64 count = obj.countallslip();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s):" + count;
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
        }
        private void BindLorry()
        {
            LorryHireDAL obj = new LorryHireDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddllorryno.DataSource = lst;
                ddllorryno.DataTextField = "Lorry_No";
                ddllorryno.DataValueField = "Lorry_Idno";
                ddllorryno.DataBind();
            }
            ddllorryno.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objDAL = new FinYearDAL();
            var lst = objDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));

            if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
            {
                txtdatefrom.Text = hidmindate.Value;
                txtdateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
            else
            {
                txtdatefrom.Text = hidmindate.Value;
                txtdateto.Text = hidmaxdate.Value;
            }
        }
        private void BindGrid()
        {
            LorryHireDAL obj = new LorryHireDAL();
            DateTime? dtfrom = null;
            DateTime? dtto = null;
            int slipno = string.IsNullOrEmpty(Convert.ToString(txtslipno.Text)) ? 0 : Convert.ToInt32(txtslipno.Text);

            Int32 lorryidno = string.IsNullOrEmpty(ddllorryno.SelectedValue) ? 0 : Convert.ToInt32(ddllorryno.SelectedValue);

            if (string.IsNullOrEmpty(Convert.ToString(txtdatefrom.Text)) == false)
            {
                dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdatefrom.Text));
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtdatefrom.Text)) == false)
            {
                dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtdateto.Text));
            }
            int cityfrom = Convert.ToInt32(ddlFromCity.SelectedValue);
            Int32 yearidno = Convert.ToInt32(ddlDateRange.SelectedValue == "" ? 0 : Convert.ToInt32(ddlDateRange.SelectedValue));
            var lstGridData = obj.SearchLorryHire(lorryidno, yearidno, cityfrom,slipno ,dtfrom, dtto );
            obj = null;
            if (lstGridData != null && lstGridData.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Slip No", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Lorry No", typeof(string));
                dt.Columns.Add("Supplied To", typeof(string));
                dt.Columns.Add("Frieght Amount", typeof(string));
                dt.Columns.Add("Advance Amount", typeof(string));
                dt.Columns.Add("Net Amount", typeof(string));


                double TNet = 0, advance = 0, freight =  0;
                for (int i = 0; i < lstGridData.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Slip No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lry_SlipNo"));
                    dr["Date"] = Convert.ToDateTime(DataBinder.Eval(lstGridData[i], "Lry_Date")).ToString("dd-MM-yyyy");
                    dr["Location"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "City_Name"));
                    dr["Lorry No"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "Lorry_No"));
                    dr["Supplied To"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "SupliedTo"));
                    dr["Frieght Amount"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "TotalFrghtAmnt"));
                    dr["Advance Amount"] = Convert.ToString(DataBinder.Eval(lstGridData[i], "AdvanceAmnt"));
                    dr["Net Amount"] = Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_amnt")).ToString("N2");
                    dt.Rows.Add(dr);
                    freight += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "TotalFrghtAmnt"));
                    advance += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "AdvanceAmnt"));
                    TNet += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                   

                    if (i == lstGridData.Count-1)
                    {
                        DataRow drr = dt.NewRow();
                        drr["Supplied To"] = "Total";
                        drr["Advance Amount"] = (advance).ToString("N2");
                        drr["Frieght Amount"] = (freight).ToString("N2");
                        drr["Net Amount"] = (TNet).ToString("N2");
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
                lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                //grdprint.DataSource = lstGridData;
                //grdprint.DataBind();

                Double TotalNetAmount = 0; Double TotalFreightAmnt = 0; Double TotalAdvanceAmnt = 0; 

                for (int i = 0; i < lstGridData.Count; i++)
                {
                    TotalFreightAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "TotalFrghtAmnt"));
                    TotalAdvanceAmnt += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "AdvanceAmnt"));
                    TotalNetAmount += Convert.ToDouble(DataBinder.Eval(lstGridData[i], "Net_Amnt"));
                }

                lblFreightAmount.Text = TotalFreightAmnt.ToString("N2");
                lblAdvanceAmount.Text = TotalAdvanceAmnt.ToString("N2");
                lblNetAmount.Text = TotalNetAmount.ToString("N2");
               
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
                //grdprint.DataSource = null;
                //grdprint.DataBind();
            }
        }

        /// <summary>
        /// To Bind Party Name DropDown
        /// </summary>
  
        #endregion

        #region Grid Events....
        //protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdMain.PageIndex = e.NewPageIndex;
        //   this.BindGrid();
        //}

        //protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    string strMsg = string.Empty;
        //    if (e.CommandName == "cmdedit")
        //    {
        //        Response.Redirect("LorryMaster.aspx?LorryIdno=" + e.CommandArgument, true);
        //    }
        //    if (e.CommandName == "cmddelete")
        //    {
        //        LorryMasterDAL objLorrMast = new LorryMasterDAL();
        //        Int32 intValue = objLorrMast.Delete(Convert.ToInt32(e.CommandArgument));
        //        objLorrMast = null;
        //        if (intValue > 0)
        //        {
        //            this.BindGrid();
        //            strMsg = "Record deleted successfully.";
        //            ddlPartyName.Focus();
        //        }
        //        else
        //        {
        //            if (intValue == -1)
        //                strMsg = "Record can not be deleted. It is in use.";
        //            else
        //                strMsg = "Record not deleted.";
        //        }
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        //    }
        //    else if (e.CommandName == "cmdstatus")
        //    {
        //        int intLorryIdno = 0;
        //        bool bStatus = false;
        //        string[] strStatus = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
        //        if (strStatus.Length > 1)
        //        {
        //            intLorryIdno = Convert.ToInt32(strStatus[0]);
        //            if (Convert.ToBoolean(strStatus[1]) == true)
        //                bStatus = false;
        //            else
        //                bStatus = true;
        //            LorryMasterDAL objLorryMast = new LorryMasterDAL();
        //            int value = objLorryMast.UpdateStatus(intLorryIdno, bStatus);
        //            objLorryMast = null;
        //            if (value > 0)
        //            {
        //                this.BindGrid();
        //                strMsg = "Status updated successfully.";
        //                ddlPartyName.Focus();
        //            }
        //            else
        //            {
        //                strMsg = "Status not updated.";
        //            }
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        //        }
        //    }
        //    ddlPartyName.Focus();
        //}

        //protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");
        //        bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Status"));
        //        //ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
        //        ////ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
        //        //base.CheckUserRights(intFormId);
        //        //if (base.Edit == false)
        //        {
        //            imgBtnStatus.Visible = true;
        //            //imgBtnEdit.Visible = false;
        //            //grdMain.Columns[4].Visible = false;
        //        }
        //        //if (base.Delete == false)
        //        //{
        //        //    imgBtnDelete.Visible = false;
        //        //}
        //        //if ((base.Edit == false) && (base.Delete == false))
        //        //{
        //        //    grdMain.Columns[5].Visible = false;
        //        //}
        //        if (status == false)
        //            imgBtnStatus.ImageUrl = "~/Images/inactive.png";
        //        else
        //            imgBtnStatus.ImageUrl = "~/Images/active.png";

        //        // Used to hide Delete button if ItemgrpId exists in GR Preparation,F.Mgmt--->  Purchase Bill ,Material Issue, Trip Start
        //        LinkButton imgBtnDelete = (LinkButton)e.Row.FindControl("lnkBtnDelete");
        //        string LorryIdno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LorryIdno"));
        //        if (LorryIdno != "")
        //        {
        //            ItemMasterDAL obj = new ItemMasterDAL();
        //            var ItemExist = obj.CheckItemExistInOtherMaster(Convert.ToInt32(LorryIdno));
        //            if (ItemExist != null && ItemExist.Count > 0)
        //            {
        //                imgBtnDelete.Visible = false;
        //            }
        //            else
        //            {
        //                imgBtnDelete.Visible = true;
        //            }
        //        }
        //        // end----

        //    }
        //}
        #endregion

        #region Button Events...
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {

            if (ViewState["Dt"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Dt"];
                Export(dt);
            }
        }
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region print...
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ManageLOcationMasterReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[4].Visible = false;
            grdMain.Columns[5].Visible = false;
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
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedIndex >= 0)
            {
                SetDate();
            }
        }

        #region Grid Event....

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                dNetAmnt = dNetAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Net_Amnt"));
                dfreightAmnt = dfreightAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalFrghtAmnt"));
                dAdvanceAmnt = dAdvanceAmnt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AdvanceAmnt"));
              
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblNetAmnt");
                lblAmount.Text = Convert.ToDouble(dNetAmnt).ToString("N2");
                Label lblFreightAmount = (Label)e.Row.FindControl("lbltotalfrieght");
                lblFreightAmount.Text = Convert.ToDouble(dfreightAmnt).ToString("N2");
                Label lblAdvanceAmount = (Label)e.Row.FindControl("lbltotaladvance");
                lblAdvanceAmount.Text = Convert.ToDouble(dAdvanceAmnt).ToString("N2");
         
            }
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("LorryHireSlip.aspx?Lh=" + e.CommandArgument, true);
            }
            if (e.CommandName == "cmddelete")
            {
               LorryHireDAL obj = new LorryHireDAL();
                Int32 intValue = obj.Delete(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";
                    ddlDateRange.Focus();
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

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion
    }
}