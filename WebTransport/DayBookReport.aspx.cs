using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.IO;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;


namespace WebTransport
{
    public partial class DayBookReport : Pagebase
    {
        #region Private Variable...
        DataSet ds = new DataSet(); DataSet dsFinal = new DataSet(); DataSet dsFin = new DataSet(); 
        string strSQL = ""; double OpenBal, ClBal, iPayment, iReceipt;
        DataTable dt,List; DataRow drRow;
        DataSet dsOuter, dsTemp, dsOuter2;
        string logo = "";
        DataSet Dslogo = new DataSet();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null)
            //{
            //    base.AutoRedirect();
            //}
            if (!Page.IsPostBack)
            {
                Datefor.Attributes.Add("onkeypress", "return notAllowAnything(event);");

                this.BindDateRange();
                this.BindLedger();
                this.BindCity();
                ddlDateRange_SelectedIndexChanged(null, null);
                //this.Countall();
               
            }
        }
        #endregion

        #region Control Events...
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        #endregion

        #region Functions...

        private void AddTotalRow(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr[0] = "PertiDebit";
            dr[1] = "Debit";
            dr[2] = "PertiCredit";
            dr[3] = "Debit";
            dt.Rows.Add(dr);
        }
        private void BindGrid()
        {
            if (ddlCashAc.SelectedIndex > 0)
            {
                dsFinal = null; OpenBal = ClBal = iPayment = iReceipt = 0; dt = null;
                double db, cr, dbopbal, cropbal, totdb, totcr;
                db = cr = dbopbal = cropbal = totdb = totcr = 0;
                dt = new DataTable("DayBookSumRep"); DataTable dt1 = new DataTable();
                DataColumn dtCol; dtCol = new DataColumn("Particular1", typeof(string));
                dt.Columns.Add(dtCol); dtCol = new DataColumn("Payment", typeof(string));
                dt.Columns.Add(dtCol); dtCol = new DataColumn("Particular2", typeof(string));
                dt.Columns.Add(dtCol); dtCol = new DataColumn("Receipt", typeof(string));
                dt.Columns.Add(dtCol);

                DayBookReportDAL obj=new DayBookReportDAL();

                DataColumn dtCol1 = new DataColumn("PertiDebit", typeof(string));
                dt1.Columns.Add(dtCol1); dtCol1 = new DataColumn("Debit", typeof(string));
                dt1.Columns.Add(dtCol1); dtCol1 = new DataColumn("PertiCredit", typeof(string));
                dt1.Columns.Add(dtCol1); dtCol1 = new DataColumn("Credit", typeof(string));
                dt1.Columns.Add(dtCol1);

                ds.Tables.Add(dt1);

                dsOuter = obj.SelectOpenPayRecWithBnk(ApplicationFunction.ConnectionString(), "SelectLdgrDetOneById", Convert.ToInt64(ddlCashAc.SelectedValue), Datefor.Text.Trim(), Convert.ToInt64(ddlDateRange.SelectedValue));

                if (dsOuter != null && dsOuter.Tables.Count > 0 && dsOuter.Tables[1].Rows.Count > 0)
                {
                    for (int x = 0; x < dsOuter.Tables[1].Rows.Count; x++)
                    {
                        dsTemp = null;

                        dsTemp = obj.SelectLdgrDetOne(ApplicationFunction.ConnectionString(), "SelectLdgrDetOne", Convert.ToInt64(ddlCashAc.SelectedValue),Convert.ToInt64(dsOuter.Tables[1].Rows[x]["VCHR_IDNO"]), Convert.ToInt64(ddlDateRange.SelectedValue));

                        if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsTemp.Tables[1].Rows.Count; i++)
                            {
                                if (dsTemp.Tables[1].Rows.Count > 1)
                                {
                                    ds.Tables[0].Rows.Add(Convert.ToString(dsTemp.Tables[1].Rows[i]["PertiDebit"]), Convert.ToString(dsTemp.Tables[1].Rows[i]["Debit"]),
                                                           Convert.ToString(dsTemp.Tables[1].Rows[i]["PertiCredit"]), Convert.ToString(dsTemp.Tables[1].Rows[i]["Credit"]));

                                }
                                else
                                {
                                    ds.Tables[0].Rows.Add(Convert.ToString(dsTemp.Tables[1].Rows[i]["PertiDebit"]), Convert.ToString(dsTemp.Tables[1].Rows[i]["Debit"]),
                                                           Convert.ToString(dsTemp.Tables[1].Rows[i]["PertiCredit"]), Convert.ToString(dsTemp.Tables[1].Rows[i]["Credit"]));
                                }
                            }
                        }
                    }
                }

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dsOuter.Tables[0].TableName = "OpeningBal"; ds.Tables[0].TableName = "PayRec";
                        if (dsOuter.Tables["OpeningBal"].Rows.Count > 0)
                        {
                            OpenBal = Convert.ToDouble(dsOuter.Tables["OpeningBal"].Rows[0][0]);
                        }
                        //if (ds.Tables["PayRec"].Rows.Count > 0)
                        //{
                            DateTime DateFrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Datefor.Text.Trim().ToString()));

                            db = obj.SELECTCRDR(ApplicationFunction.ConnectionString(), "SELECTCRDR", 2, Convert.ToInt64(ddlCashAc.SelectedValue), Datefor.Text.Trim(), Convert.ToInt64(ddlDateRange.SelectedValue));
                            cr = obj.SELECTCRDR(ApplicationFunction.ConnectionString(), "SELECTCRDR", 1, Convert.ToInt64(ddlCashAc.SelectedValue), Datefor.Text.Trim(), Convert.ToInt64(ddlDateRange.SelectedValue));
                            dbopbal = obj.SELECTOPBAL(ApplicationFunction.ConnectionString(), "SELECTOPBAL", 2, Convert.ToInt64(ddlCashAc.SelectedValue), Convert.ToInt64(ddlDateRange.SelectedValue));
                            cropbal = obj.SELECTOPBAL(ApplicationFunction.ConnectionString(), "SELECTOPBAL",1, Convert.ToInt64(ddlCashAc.SelectedValue), Convert.ToInt64(ddlDateRange.SelectedValue));

                            totdb = db + dbopbal; totcr = cr + cropbal;
                            OpenBal = totdb - totcr;

                            List = dt1.Clone();

                            DataRow drNew1 = List.NewRow();
                            drNew1.BeginEdit();
                            drNew1[0] = "Opening Balance";
                            if(OpenBal>0)
                            drNew1[1] = OpenBal.ToString("N2");
                            else
                                drNew1[3] = Math.Abs(OpenBal).ToString("N2");
                            
                            List.Rows.Add(drNew1);

                            DataRow drNew2 = List.NewRow();
                            drNew2.BeginEdit();
                            drNew2[0] = "";
                            drNew2[1] = "";
                            List.Rows.Add(drNew2);
                            DataRow drNew7 = List.NewRow();
                            drNew7.BeginEdit();
                            drNew7[0] = ""; drNew7[2] = "";
                            drNew7[1] = ""; drNew7[3] = "";
                            List.Rows.Add(drNew7);

                            for (int i = 3, j = 2, cnt = 0; cnt < ds.Tables["PayRec"].Rows.Count; cnt++)
                            {
                               if (Convert.ToString(ds.Tables["PayRec"].Rows[cnt][0]) != "")
                                {
                                      DataRow drNew4 = List.NewRow();
                                      drNew4.BeginEdit();
                                      drNew4[0] = Convert.ToString(ds.Tables["PayRec"].Rows[cnt][0]);
                                      drNew4[1] = Convert.ToString(ds.Tables["PayRec"].Rows[cnt][1]);
                                      List.Rows.Add(drNew4);
                                      iPayment = iPayment + Convert.ToDouble(ds.Tables["PayRec"].Rows[cnt][1]); i++;
                                }
                                else if (Convert.ToString(ds.Tables["PayRec"].Rows[cnt][2]) != "")
                               {
                 
                                       List.Rows[j][2] = Convert.ToString(ds.Tables["PayRec"].Rows[cnt][2]);
                                       List.Rows[j][3] = Convert.ToString(ds.Tables["PayRec"].Rows[cnt][3]);
                                       iReceipt = iReceipt + Convert.ToDouble(ds.Tables["PayRec"].Rows[cnt][3]); j++;
                  
                               }
                            }
                            
                            ClBal = (OpenBal + iReceipt) - iPayment;


                            DataRow drNew6 = List.NewRow();
                            drNew6.BeginEdit();
                            drNew6[0] = "Total Debit"; drNew6[2] = "Total Credit";
                            drNew6[1] = Convert.ToDouble(iPayment).ToString("N2"); drNew6[3] = Convert.ToDouble(iReceipt).ToString("N2");
                            List.Rows.Add(drNew6);

                            DataRow drNew5 = List.NewRow();
                            drNew5.BeginEdit();
                            drNew5[1] = "";
                            List.Rows.Add(drNew5);

                            double ClBalnew = Math.Abs(ClBal);
                                

                            List.Rows[List.Rows.Count - 1][0] = "Closing Balance";
                            if(ClBal < 0)
                                List.Rows[List.Rows.Count - 1][1] = ClBalnew.ToString("N2");
                            else
                                List.Rows[List.Rows.Count - 1][3] = ClBalnew.ToString("N2");

                            grdMain.Width = new Unit("1065px");
                            //gridRowDel();
                            ViewState["Dt"] = List;
                            grdMain.DataSource = List;
                            grdMain.DataBind();

                            //lblTotalRecord.Text = "T. Record (s): " + lstGridData.Count;
                            int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                            int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                            //lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                            //lblcontant.Visible = true;
                            imgBtnExcel.Visible = true;
                            divpaging.Visible = true;
                        //}
                        //else
                        //{
                        //    grdMain.DataSource = null;
                        //    grdMain.DataBind();
                        //    lblTotalRecord.Text = "T. Record (s): 0 ";
                        //    imgBtnExcel.Visible = false;
                        //    lblcontant.Visible = false;
                        //    divpaging.Visible = false;
                        //}
                    }
                    else
                    {
                       //
                    }
                }

            }
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
                    Datefor.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    Datefor.Text = hidmindate.Value;
                }
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

        private void BindLedger()
        {
            DayBookReportDAL obj = new DayBookReportDAL();
            var PartyDet = obj.FillPartyDetail();
            ddlCashAc.DataSource = PartyDet;
            ddlCashAc.DataTextField = "Acnt_Name";
            ddlCashAc.DataValueField = "Acnt_Idno";
            ddlCashAc.DataBind();
            ddlCashAc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindCity()
        {
            
        }

        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        public void Countall()
        {
            AdvBookGRDAL obj = new AdvBookGRDAL();
            Int64 count = obj.Countall();
            if (count > 0)
            {
                lblTotalRecord.Text = "T. Record (s): " + count;
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
            string strMsg = string.Empty;
            if (e.CommandName == "cmdedit")
            {
              
            }
            if (e.CommandName == "cmddelete")
            {
               
            }
        }
        protected void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {

                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6CBFE8'");

                // when mouse leaves the row, change the bg color to its original value  
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");


            }
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (List != null && List.Rows.Count > 0)
                {
                    TableCell cell = new TableCell();
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;

                    e.Row.Cells[0].Width = new Unit("150px");
                    e.Row.Cells[1].Width = new Unit("100px");
                    e.Row.Cells[2].Width = new Unit("150px");
                    e.Row.Cells[3].Width = new Unit("100px");
                    if (e.Row.RowIndex == List.Rows.Count - 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.SkyBlue;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Font.Bold = true;
                        e.Row.Font.Size = 10;
                    }
                    if (e.Row.RowIndex == List.Rows.Count - 2)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Font.Bold = true;
                        e.Row.Font.Size = 10;
                    }
                    if (e.Row.RowIndex == 0)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        e.Row.Font.Bold = true;
                        e.Row.Font.Size = 10;
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[2].Width = new Unit("100px");
                e.Row.Cells[3].Width = new Unit("60px");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        #endregion

        #region Prints...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
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

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AdvanceGr.xls"));
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion
    }
}