using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Transactions;

namespace WebTransport
{
    public partial class EmployeeSalry : Pagebase
    {
        EmployeeSalaryDAL objemp = new EmployeeSalaryDAL();

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                MonthName(); YearName(); BindEmployees();

                if (Request["EmpSal"] != null)
                {
                    hdnQry.Value = Request["EmpSal"].ToString();
                    this.PopulateEmpDetl(Convert.ToInt32(hdnQry.Value));

                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }

                ddlEmpName.Focus();
            }
        }
        #endregion

        #region Functions...

        private void BindEmployees()
        {
            EmployeeSalaryDAL objemp = new EmployeeSalaryDAL();
            var lst = objemp.FetchEmployeeName();
            
            ddlEmpName.DataSource = lst;
            ddlEmpName.DataTextField = "Acnt_Name";
            ddlEmpName.DataValueField = "Acnt_Idno";
            ddlEmpName.DataBind();
            ddlEmpName.Items.Insert(0, new ListItem("< Choose emp name >", "0"));
        }

        private void MonthName()
        {
            DataTable dt = objemp.MonthNameList();
            ddlMonth.DataSource = dt;
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "MonthId";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("Select Month", "0"));
        }

        private void YearName()
        {
            DataTable dt = objemp.YearList();
            ddlYear.DataSource = dt;
            ddlYear.DataTextField = "YearName";
            ddlYear.DataValueField = "YearId";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));
        }

        private void ClearAllData()
        {
            hdnQry.Value = string.Empty;
            txtSalary.Text = "0.0";
            ddlMonth.SelectedIndex = ddlYear.SelectedIndex = ddlEmpName.SelectedIndex = 0; chkStatus.Checked = true;

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void PopulateEmpDetl(Int32 EmpID)
        {
            EmployeeSalaryDAL objEmpSal = new EmployeeSalaryDAL();
            var vartblEmpSal = objEmpSal.SelectEmployeeDetailAtEditTime(EmpID);
            if (vartblEmpSal != null)
            {
                ddlEmpName.SelectedValue = Convert.ToString(DataBinder.Eval(vartblEmpSal[0], "EmpSal_EmpID"));  
                txtSalary.Text = Convert.ToString(DataBinder.Eval(vartblEmpSal[0], "EmpSal_Salary"));
                ddlMonth.SelectedValue = Convert.ToString(DataBinder.Eval(vartblEmpSal[0], "EmpSal_Month"));
                ddlYear.SelectedValue = Convert.ToString(DataBinder.Eval(vartblEmpSal[0], "EmpSal_Year")); 

                chkStatus.Checked = Convert.ToBoolean(DataBinder.Eval(vartblEmpSal[0], "EmpSal_Status"));
            }
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

        #region Button events..
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeSalry.aspx");
        }

        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            EmployeeSalaryDAL Empobj = new EmployeeSalaryDAL();
            Int64 value = 0;
            string Date = DateTime.Now.Date.ToString("dd-MM-yyyy");
            DateTime DateAdd = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Date));
            using (TransactionScope dbTran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    if (string.IsNullOrEmpty(hdnQry.Value) == true)
                    {
                        value = Empobj.InsertEmployeeDetl(Convert.ToInt64(ddlEmpName.SelectedValue), Convert.ToDouble(txtSalary.Text.Trim()), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), chkStatus.Checked, DateAdd);
                    }
                    else
                    {
                        value = Empobj.UpdateEmployeeDetl(Convert.ToInt64(hdnQry.Value), Convert.ToInt64(ddlEmpName.SelectedValue), Convert.ToDouble(txtSalary.Text.Trim()), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), chkStatus.Checked, DateAdd);
                    }


                    if (value > 0)
                    {
                        if (string.IsNullOrEmpty(hdnQry.Value) == true)
                        {
                            ShowMessage("Records successfully inserted.");
                        }
                        else
                        {
                            lnkbtnNew.Visible = false;
                            ShowMessage("Records successfully updated.");
                        }
                        dbTran.Complete();
                        this.ClearAllData();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hdnQry.Value) == true)
                        {
                            ShowMessageErr("Records not inserted!");
                        }
                        else
                        {
                            ShowMessageErr("Records not updated!");
                        }
                        dbTran.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(hdnQry.Value) == true)
                    {
                        ShowMessageErr("Error in records inserting!");
                    }
                    else
                    {
                        ShowMessageErr("Error in Records updating!");
                    }
                }
            }
        }

        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdnQry.Value) == true)
            {
                Response.Redirect("EmployeeSalry.aspx");
            }
            else
            {
               
                this.PopulateEmpDetl(Convert.ToInt32(hdnQry.Value));
            }
        }

        #endregion
    }
}