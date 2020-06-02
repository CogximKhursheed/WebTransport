using System;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;

namespace WebTransport
{
    public partial class DocumentReport : Pagebase
    {
        #region Variables....
        private int intFormId = 67;
        #endregion

        #region PageLoad Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!IsPostBack)
            {
                if (base.CheckUserRights(intFormId) == false)
                {
                    Response.Redirect("PermissionDenied.aspx");
                }
                if (base.View == false)
                {
                    lnkbtnPreview.Visible = false;
                }
                //if (base.Print == false)
                //{
                //    lnkbtnPrint.Visible = imgBtnExcel.Visible = false;
                //}
                BindEmployee();
                SetDate();
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDocumentNo.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
        }
        #endregion

        #region Function..
        public void BindEmployee()
        {
            DocumentDAL obj = new DocumentDAL();
            var lst = obj.EmployeeList();
            if (lst != null && lst.Count > 0)
            {
                ddlEmployee.DataSource = lst;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "User_Idno";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("< Choose Employee Name >", "0"));
            }
            else
            {
                ddlEmployee.Items.Clear();
                ddlEmployee.Items.Insert(0, new ListItem("< Choose Employee Name >", "0"));
            }
        }
        private void SetDate()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int numDays = DateTime.DaysInMonth(year, month);
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).ToString("dd-MM-yyyy");// strdate;
            txtDateTo.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, numDays).ToString("dd-MM-yyyy");// enddate;
        }
        public void BindGrid()
        {
            string DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? "" : txtDateFrom.Text.Trim();
            string DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? "" : txtDateTo.Text.Trim();
            Int32 DocType = Convert.ToInt32(ddlDoucmentType.SelectedValue);
            Int32 EmpID = Convert.ToInt32(ddlEmployee.SelectedValue);
            Int64 DocNo = string.IsNullOrEmpty(txtDocumentNo.Text.Trim()) ? 0 : Convert.ToInt64(txtDocumentNo.Text.Trim());
            DocumentDAL obj = new DocumentDAL();
            DataSet ds = obj.SelectDocumentRecord(DateFrom, DateTo, DocType, EmpID, DocNo, ApplicationFunction.ConnectionString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdMain.DataSource = ds;
                grdMain.DataBind();
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
            }
        }
        #endregion

        #region GridEvents..
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }
        #endregion

        #region Buttun Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}