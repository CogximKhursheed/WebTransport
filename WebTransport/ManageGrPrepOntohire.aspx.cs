using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using System.IO;
using System.Collections;

namespace WebTransport
{
    public partial class ManageGrPrepOntohire : Pagebase
    {
        #region Private Variable....
        private int intFormId = 28; double dblTChallanAmnt = 0;
        // string con = ConfigurationManager.ConnectionStrings["TransportMandiConnectionString"].ConnectionString;
        #endregion

        #region Page Load...
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
               
                this.BindDateRange();
                ddldateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                ddldateRange_SelectedIndexChanged(null, null);
               
                txtReceiptDatefrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtReceiptDateto.Attributes.Add("onkeypress", "return notAllowAnything(event);");

             
                ChlnBookingDAL obj = new ChlnBookingDAL();
                DateTime? dtfrom = null;
                DateTime? dtto = null;
           
                if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
                {
                    dtfrom = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDatefrom.Text));
                }
                if (string.IsNullOrEmpty(Convert.ToString(txtReceiptDatefrom.Text)) == false)
                {
                    dtto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtReceiptDateto.Text));
                }
           

                Int64 UserIdno = 0;
                if (Convert.ToString(Session["Userclass"]) != "Admin")
                {
                    UserIdno = Convert.ToInt64(Session["UserIdno"]);
                }

            
                obj = null;
           
            }
        }
         #endregion
        protected override PageStatePersister PageStatePersister
        {
            get
            { return new SessionPageStatePersister(this);
            }
        }
        #region Function..
        private void BindGrid()
        {
            GrPrepOntohireDAL obj = new GrPrepOntohireDAL();
           
            DataTable dtRcptDetl = new DataTable();



            dtRcptDetl = obj.SelectGrPrep(Convert.ToInt64(ddldateRange.SelectedValue), txtReceiptDatefrom.Text, txtReceiptDateto.Text, ApplicationFunction.ConnectionString());

          
            if ((dtRcptDetl != null && dtRcptDetl.Rows.Count > 0))
            {
                grdMain.DataSource = dtRcptDetl;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record (s): " + dtRcptDetl.Rows.Count;

                Double TotalNetAmount = 0;

                for (int i = 0; i < dtRcptDetl.Rows.Count; i++)
                {
                    TotalNetAmount += Convert.ToDouble((dtRcptDetl.Rows[i]["Total_Amnt"]));
                }
                lblNetTotalAmount.Text = TotalNetAmount.ToString("N2");

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + dtRcptDetl.Rows.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
                imgBtnExcel.Visible = true;

                ViewState["Dt"] = dtRcptDetl;
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

        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GrPrepOnToHire.xls"));
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
        public void BindDateRange()
        {
            FinYearDAL obj = new FinYearDAL();
            var lst = obj.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddldateRange.DataSource = lst;
            ddldateRange.DataTextField = "DateRange";
            ddldateRange.DataValueField = "Id";
            ddldateRange.DataBind();
        }
        private void SetDate()
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                Int32 intyearid = Convert.ToInt32(ddldateRange.SelectedValue);
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(intyearid);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {

                    txtReceiptDatefrom.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        

                    txtReceiptDateto.Text =Convert.ToString(hidmaxdate.Value);
                }
                else
                {

                    txtReceiptDatefrom.Text = Convert.ToString(hidmindate.Value);
                    txtReceiptDateto.Text = Convert.ToString(hidmaxdate.Value);

                }
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
            
            if (e.CommandName == "cmddelete")
            {
                Int64 UserIdno = Convert.ToInt64(Session["UserIdno"]);
                GrPrepOntohireDAL obj = new GrPrepOntohireDAL();
                Int32 intValue = obj.DeleteHireGrDetail(Convert.ToInt32(e.CommandArgument));
                obj = null;
                if (intValue > 0)
                {
                    this.BindGrid();
                    strMsg = "Record deleted successfully.";

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
            try
            {
                double dblChallanAmnt = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                  LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");
                   dblChallanAmnt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total_Amnt"));
                   dblTChallanAmnt = dblChallanAmnt + dblTChallanAmnt;
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTChallanAmnt = (Label)e.Row.FindControl("lblNetAmnt");
                    lblTChallanAmnt.Text = dblTChallanAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Button Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #endregion

        #region Control Events...
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
            ddldateRange.Focus();
        }
        #endregion

        #region IMport excel....
        private void ExportGridView()
        {
            string attachment = "attachment; filename=GrPrepOnToHire.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdMain.Columns[1].Visible = false;
            grdMain.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            //if (ViewState["Dt"] != null)
            //{
            //    DataTable dt = new DataTable();
            //    dt = (DataTable)ViewState["Dt"];
            //    Export(dt);
            //}
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
                if (gv.Controls[i].GetType() == typeof(Label))
                {
                    l.Text = (gv.Controls[i] as Label).Text;
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
            grdMain.Columns[1].Visible = false;
        }
        #endregion



    }
}