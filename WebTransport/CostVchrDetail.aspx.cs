using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Globalization;

namespace WebTransport
{
    public partial class CostVchrDetail : Pagebase
    {
        #region Variabless...
        string conString = "";
        Double dNetAmount1 = 0;
        Double dNetAmount2 = 0;
        string LastBal = string.Empty;
        private int intFormId = 55;
        CostCategorySumryDAL objAccountBookDAL = new CostCategorySumryDAL();
        #endregion

        #region Pageload Event...
        protected void Page_Load(object sender, EventArgs e)
        {
			if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            if (!Page.IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.Print == false)
                {
                    imgBtnExcel.Visible = false;
                    printRep.Visible = false;
                }
                this.BindGrid();
            }
        }
        #endregion

        #region Grid Event...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dNetAmount1 = dNetAmount1 + (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit")));
                dNetAmount2 = dNetAmount2 + (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit")) == null ? 0 : Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit")));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblTDebit");
                lblDebit.Text = String.Format("{0:0,0.00}", dNetAmount1.ToString("N", new CultureInfo("hi-IN"))).ToString();
                Label lblCredit = (Label)e.Row.FindControl("lblTCredit");
                lblCredit.Text = String.Format("{0:0,0.00}", dNetAmount2.ToString("N", new CultureInfo("hi-IN"))).ToString();
            }
        }
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }
        #endregion

        #region Bind Event...
        private void BindGrid()
        {
            DataTable DT = ConvertToDataTable();
            if (DT != null && DT.Rows.Count > 0)
            {
                grdMain.DataSource = DT;
                grdMain.DataBind();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }
        private DataTable ConvertToDataTable()
        {
            string strDateFrom = "";// Convert.ToString(Request.QueryString["startdate"]);
            string strDateTo = "";// Convert.ToString(Request.QueryString["enddate"]);
            Int64 intpartyidno = 0;// Convert.ToInt64(Request.QueryString["party"]);
            string TruckNo = Convert.ToString(Session["TruckNo"]);
            string FrmType = Convert.ToString(Request.QueryString["FrmType"]);
            if (FrmType == null)
            {
                strDateFrom = Convert.ToString(Request.QueryString["startdate"]);
                strDateTo = Convert.ToString(Request.QueryString["enddate"]);
                intpartyidno = Convert.ToInt64(Request.QueryString["TruckIdno"]);
                lblTruckNo.Text = TruckNo + "-"+"  [" + strDateFrom + " To " + strDateTo + "]";
            }
            else
            {
                strDateFrom = Convert.ToString(Request.QueryString["startdate"]);
                strDateTo = Convert.ToString(Request.QueryString["enddate"]);
                intpartyidno = Convert.ToInt64(Request.QueryString["TruckIdno"]);
                lblTruckNo.Text = TruckNo + "-" + "  [" + strDateFrom + " To " + strDateTo + "]";
            }
            DataTable Ndt = new DataTable();
            DataSet Ndt1 = new DataSet();
            DataSet ds1 = new DataSet();
            ds1 = objAccountBookDAL.AfterDoubleClick1(ApplicationFunction.ConnectionString(), "SelectIdDetailReport", intpartyidno, strDateFrom, strDateTo);
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count>0)
            {
                #region Commented Code old Code....
                //    Ndt.Columns.Add("VCHR_DATE"); Ndt.Columns.Add("VCHR_NO"); Ndt.Columns.Add("PARTICULAR"); Ndt.Columns.Add("VCHR_TYPE");
                //    Ndt.Columns.Add("Credit"); Ndt.Columns.Add("Debit"); Ndt.Columns.Add("NARR_TEXT"); Ndt.Columns.Add("VCHR_IDNO");
                //    Ndt.Columns.Add("AMNT_TYPE"); Ndt.Columns.Add("Vchr_Frm"); Ndt.Columns.Add("DOC_IDNO"); Ndt.Columns.Add("Balance");
                //    if (ds1.Tables[0].Rows.Count > 0)
                //    {
                //        for (int x = 0; x < ds1.Tables[0].Rows.Count; x++)
                //        {
                //            DataSet ds2 = objAccountBookDAL.AfterDblClikForLedD(ApplicationFunction.ConnectionString(), "SelectLdgrDetOne", Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]), Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]), intpartyidno);
                //            if (ds2 != null && ds2.Tables.Count > 0)
                //            {
                //                if (ds2.Tables[0].Rows.Count > 0)
                //                {
                //                    for (int y = 0; y < ds2.Tables[0].Rows.Count; y++)
                //                    {
                //                        if (ds2.Tables[0].Rows.Count > 1)
                //                            Ndt.Rows.Add(Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_DATE"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_NO"]),
                //                                                   Convert.ToString(ds2.Tables[0].Rows[y]["PARTICULAR"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_TYPE"]),
                //                                                   Convert.ToDouble(ds2.Tables[0].Rows[y]["Credit"]),
                //                                                   Convert.ToDouble(ds2.Tables[0].Rows[y]["Debit"]),
                //                                                   Convert.ToString(ds2.Tables[0].Rows[y]["NARR_TEXT"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["Vchr_Frm"]),
                //                                                   Convert.ToString(ds1.Tables[0].Rows[x]["DOC_IDNO"]));
                //                        else
                //                            Ndt.Rows.Add(Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_DATE"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_NO"]),
                //                                                    Convert.ToString(ds2.Tables[0].Rows[y]["PARTICULAR"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_TYPE"]),
                //                                                    Convert.ToDouble(ds1.Tables[0].Rows[x]["Debit"]),
                //                                                    Convert.ToDouble(ds1.Tables[0].Rows[x]["Credit"]),
                //                                                    Convert.ToString(ds2.Tables[0].Rows[y]["NARR_TEXT"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["VCHR_IDNO"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["AMNT_TYPE"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["Vchr_Frm"]),
                //                                                    Convert.ToString(ds1.Tables[0].Rows[x]["DOC_IDNO"]));
                //                    }
                //                }
                //            }

                //            ds2 = null;
                //        }

                //        Double bal = 0;
                //        Double bal1 = 0;
                //        if (Convert.ToString(Ndt.Rows[0]["Debit"]) != "00.00")
                //            bal = Convert.ToDouble(Ndt.Rows[0]["Debit"]);
                //        if (Convert.ToString(Ndt.Rows[0]["Credit"]) != "00.00")
                //            bal1 = Convert.ToDouble(Ndt.Rows[0]["Credit"]);
                //        double tot = 0;
                //        for (int i = 1; i < Ndt.Rows.Count; i++)
                //        {
                //            bal = bal + Convert.ToDouble(Ndt.Rows[i]["Debit"]);
                //            bal1 = bal1 + Convert.ToDouble(Ndt.Rows[i]["Credit"]);
                //            tot = bal - bal1;
                //            if (tot >= 0)
                //                Ndt.Rows[i][11] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Dr";
                //            else
                //                Ndt.Rows[i][11] = string.Format("{0:0,0.00}", Convert.ToDouble(Math.Abs(tot)).ToString("N", new CultureInfo("hi-IN"))) + " Cr";
                //        }
                //    }
                //}
                #endregion
                 Ndt= ds1.Tables[0];
            }
            return Ndt;
        }

        #endregion

        #region Excel...
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
            string attachment = "attachment; filename=Report.xls";
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

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }
        #endregion Excel
    }
}