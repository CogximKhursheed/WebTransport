using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace WebTransport
{
    public partial class AcntPostChkLst : System.Web.UI.Page
    {
        public static string VchrId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindDateRange();
                ddldateRange.SelectedIndex = 0;
                ddldateRange_SelectedIndexChanged(null, null);
            }
        }

        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] objSqlPara = new SqlParameter[3];
                objSqlPara[0] = new SqlParameter("@DateFrom", ApplicationFunction.mmddyyyy(txtInvcDatefrom.Text));
                objSqlPara[1] = new SqlParameter("@DateTo", ApplicationFunction.mmddyyyy(txtInvcDateto.Text));
                objSqlPara[2] = new SqlParameter("@YearId", Convert.ToInt32(ddldateRange.SelectedValue));
                DataSet Ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.StoredProcedure, "spAcntPostChlLst", objSqlPara);
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    grdMain.DataSource = Ds.Tables[0];
                    grdMain.DataBind();
                    lblTotalRecord.Text = "T. Record (s): " + Convert.ToString(Ds.Tables[0].Rows.Count);
                    lnkDeleteVchr.Visible = true;
                }
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    VchrId = "";
                    lnkDeleteVchr.Visible = true;
                    for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            VchrId = Convert.ToString(Ds.Tables[0].Rows[i][0]);
                        }
                        else
                        {
                            VchrId += "," + Convert.ToString(Ds.Tables[0].Rows[i][0]);
                        }
                    }
                }
                else
                {
                    lnkDeleteVchr.Visible = false;
                }

            }
            catch (Exception Ex)
            {

            }
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
                if (lst != null && lst.Count > 0)
                {
                    hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                    hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                    {

                        txtInvcDatefrom.Text = Convert.ToString(hidmindate.Value);

                        txtInvcDateto.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtInvcDatefrom.Text = Convert.ToString(hidmindate.Value);
                        txtInvcDateto.Text = Convert.ToString(hidmaxdate.Value);
                    }
                }
            }
        }

        protected void ddldateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldateRange.SelectedIndex != -1)
            {
                SetDate();
            }
        }

        protected void lnkDeleteVchr_Click(object sender, EventArgs e)
        {
            try
            {
                string StrQuery = @"Delete from vchrhead where Vchr_Idno in (" + VchrId + ")  Delete from vchrDetl where Vchr_Idno in (" + VchrId + ")  Delete from vchrIDDetl where Vchr_Idno in (" + VchrId + ")";
                int d = SqlHelper.ExecuteNonQuery(ApplicationFunction.ConnectionString(), CommandType.Text, StrQuery);
                if (d > 0)
                {
                    lnkbtnPreview_Click(null, null);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('Vchr Deleted Successfully.')", true);
                }
            }
            catch (Exception Ex)
            {
            }
        }

    }
}