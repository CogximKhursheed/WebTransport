using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
namespace WebTransport
{
    public partial class UserRights : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 54;
        #endregion

        #region Page Load...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (Convert.ToString(Session["Userclass"]) != "Admin")
            {
                Response.Redirect("PermissionDenied.aspx");
            }
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                selectall.Visible = false;
                this.BindUser();
                if (Request.QueryString["UserIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["UserIdno"]));
                    this.BindGrid();
                }
                ddlUser.Focus();
            }
        }
        #endregion

        #region Functions...
        private void Populate(int intUserIdno)
        {
            this.BindUser();
            ddlUser.SelectedValue = Convert.ToString(intUserIdno);
            ddlUser.Enabled = false;
        }
        /// <summary>
        /// To Bind Grid
        /// </summary>
        private void BindGrid()
        {
            UserRightsDAL objUserRightsDAL = new UserRightsDAL();
            if (ddlType.SelectedItem.Text == "Form")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeForm(Convert.ToInt32(ddlUser.SelectedValue));
                objUserRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
            }
            else if (ddlType.SelectedItem.Text == "Menu")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeMenu(Convert.ToInt32(ddlUser.SelectedValue));
                objUserRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
            }
            else if (ddlType.SelectedItem.Text == "Report")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeRep(Convert.ToInt32(ddlUser.SelectedValue));
                objUserRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();
            }

            int count = grdMain.Rows.Count;
            int RowCount = 0;
            foreach (GridViewRow row in grdMain.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    RowCount++;
                }
            }
            if (RowCount == count)
            {
                chkSelectAllRows.Checked = true;
                imgBtnSelectAllRows.ImageUrl = "~/Images/SelectAll_Active.png";
            }
            else
            {
                chkSelectAllRows.Checked = false;
                imgBtnSelectAllRows.ImageUrl = "~/Images/SelectAll_Inactive.png";
            }
            if (count > 0)
                selectall.Visible = true;
            else
                selectall.Visible = false;
        }

        /// <summary>
        /// To Bind Desig DropDown
        /// </summary>
        private void BindUser()
        {
            UserRightsDAL objUserRightsDAL = new UserRightsDAL();
            if (Convert.ToString(Session["Userclass"]) == "Admin")
            {
                var objDesignRights = objUserRightsDAL.SelectUser();
                objUserRightsDAL = null;
                ddlUser.DataSource = objDesignRights;
                ddlUser.DataTextField = "Emp_Name";
                ddlUser.DataValueField = "user_idno";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("< Choose >", "0"));
            }
            else
            {
                var objDesignRights = objUserRightsDAL.SelectByAdminId(Convert.ToInt32(Session["UserIdno"]));
                objUserRightsDAL = null;
                ddlUser.DataSource = objDesignRights;
                ddlUser.DataTextField = "Emp_Name";
                ddlUser.DataValueField = "user_idno";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("< Choose >", "0"));
                //ddlUser.SelectedValue = Convert.ToString(base.UserIdno);
            }
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            int intUserRghtsIdno = 0;
            UserRightsDAL objUserRightsDAL = new UserRightsDAL();
            HiddenField hiduserId = (HiddenField)grdMain.Rows[0].FindControl("hiduserId");
            int intUserIdno = Convert.ToInt32(hiduserId.Value);
            if (e.CommandName == "cmdAdd")
            {

                bool bAdd = false;
                string[] strAdd = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strAdd.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strAdd[0]);
                    if (Convert.ToBoolean(strAdd[1]) == true)
                        bAdd = false;
                    else
                        bAdd = true;

                    int value = objUserRightsDAL.UpdateAdd(intUserRghtsIdno, bAdd, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdEdit")
            {
                bool bEdit = false;
                string[] strEdit = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strEdit.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strEdit[0]);
                    if (Convert.ToBoolean(strEdit[1]) == true)
                        bEdit = false;
                    else
                        bEdit = true;
                    int value = objUserRightsDAL.UpdateEdit(intUserRghtsIdno, bEdit, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdView")
            {
                bool bView = false;
                string[] strView = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strView.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strView[0]);
                    if (Convert.ToBoolean(strView[1]) == true)
                        bView = false;
                    else
                        bView = true;
                    int value = objUserRightsDAL.UpdateView(intUserRghtsIdno, bView, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdDelete")
            {
                bool bDelete = false;
                string[] strDelete = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strDelete.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strDelete[0]);
                    if (Convert.ToBoolean(strDelete[1]) == true)
                        bDelete = false;
                    else
                        bDelete = true;
                    int value = objUserRightsDAL.UpdateDelete(intUserRghtsIdno, bDelete, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdPrint")
            {
                bool bPrint = false;
                string[] strPrint = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strPrint.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strPrint[0]);
                    if (Convert.ToBoolean(strPrint[1]) == true)
                        bPrint = false;
                    else
                        bPrint = true;
                    int value = objUserRightsDAL.UpdatePrint(intUserRghtsIdno, bPrint, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdSelectAll")
            {
                bool bAdd = false;
                bool bEdit = false;
                bool bView = false;
                bool bDelete = false;
                bool bPrint = false;
                string[] strAll = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strAll.Length > 1)
                {
                    intUserRghtsIdno = Convert.ToInt32(strAll[0]);
                    int value = 0;
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect.Checked == true)
                    {
                        bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                    }
                    else if (chkSelect.Checked == false)
                    {
                        bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                    }
                    value = objUserRightsDAL.UpdateAll(intUserRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                    objUserRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            this.BindGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnAdd = (ImageButton)e.Row.FindControl("imgBtnAdd");
                bool ADD = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "ADD"));
                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                bool Edit = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Edit"));
                ImageButton imgBtnView = (ImageButton)e.Row.FindControl("imgBtnView");
                bool View = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "View"));
                ImageButton imgBtnDelete = (ImageButton)e.Row.FindControl("imgBtnDelete");
                bool Delete = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Delete"));
                ImageButton imgBtnPrint = (ImageButton)e.Row.FindControl("imgBtnPrint");
                bool Print = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Print"));
                ImageButton imgBtnSelectAll = (ImageButton)e.Row.FindControl("imgBtnSelectAll");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                if (ADD == false)
                    imgBtnAdd.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnAdd.ImageUrl = "~/Images/active.png";
                if (Edit == false)
                    imgBtnEdit.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnEdit.ImageUrl = "~/Images/active.png";
                if (View == false)
                    imgBtnView.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnView.ImageUrl = "~/Images/active.png";
                if (Delete == false)
                    imgBtnDelete.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnDelete.ImageUrl = "~/Images/active.png";
                if (Print == false)
                    imgBtnPrint.ImageUrl = "~/Images/inactive.png";
                else
                    imgBtnPrint.ImageUrl = "~/Images/active.png";
                if ((ADD == false) || (Edit == false) || (View == false) || (Delete == false) || (Print == false))
                {
                    imgBtnSelectAll.ImageUrl = "~/Images/SelectAll_Inactive.png";
                    chkSelect.Checked = false;
                }
                else
                {
                    imgBtnSelectAll.ImageUrl = "~/Images/SelectAll_Active.png";
                    chkSelect.Checked = true;
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    grdMain.Columns[3].Visible = false;
                    grdMain.Columns[4].Visible = false;
                    grdMain.Columns[2].Visible = true;
                    grdMain.Columns[1].Visible = true;
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    grdMain.Columns[4].Visible = false;
                    grdMain.Columns[2].Visible = false;
                    grdMain.Columns[1].Visible = false;
                    grdMain.Columns[3].Visible = true;
                }
                else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                {
                    grdMain.Columns[4].Visible = true;
                    grdMain.Columns[2].Visible = false;
                    grdMain.Columns[3].Visible = false;
                    grdMain.Columns[1].Visible = true;
                }



            }
        }

        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        #endregion

        #region Buttons Events...
        protected void lnkBtnPreview_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region Control Events...
        protected void chkSelectAllRows_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdMain.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelectAllRows.Checked == true)
                {
                    chkSelect.Checked = true;
                    imgBtnSelectAllRows.ImageUrl = "~/Images/SelectAll_Active.png";
                }
                else
                {
                    chkSelect.Checked = false;
                    imgBtnSelectAllRows.ImageUrl = "~/Images/SelectAll_Inactive.png";
                }
            }
        }

        protected void imgBtnSelectAllRows_Click(object sender, ImageClickEventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            int value = 0;
            string strMsg = string.Empty;
            int intUserRghtsIdno = 0;
            UserRightsDAL objUserRightsDAL = new UserRightsDAL();
            if (ddlType.SelectedItem.Text == "Form")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeForm(Convert.ToInt32(ddlUser.SelectedValue));
                int i = 0;
                foreach (GridViewRow row in grdMain.Rows)
                {
                    bool bAdd = false;
                    bool bEdit = false;
                    bool bView = false;
                    bool bDelete = false;
                    bool bPrint = false;
                    if (lstGridData.Count > 0)
                    {
                        intUserRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "UserRgt_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objUserRightsDAL.UpdateAll(intUserRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                    }
                    i++;
                }
            }
            else if (ddlType.SelectedItem.Text == "Menu")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeMenu(Convert.ToInt32(ddlUser.SelectedValue));
                int i = 0;
                foreach (GridViewRow row in grdMain.Rows)
                {
                    bool bAdd = false;
                    bool bEdit = false;
                    bool bView = false;
                    bool bDelete = false;
                    bool bPrint = false;
                    if (lstGridData.Count > 0)
                    {
                        intUserRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "UserRgt_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objUserRightsDAL.UpdateAll(intUserRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                    }
                    i++;
                }
            }
            else if (ddlType.SelectedItem.Text == "Report")
            {
                var lstGridData = objUserRightsDAL.SelectForGridTypeRep(Convert.ToInt32(ddlUser.SelectedValue));
                int i = 0;
                foreach (GridViewRow row in grdMain.Rows)
                {
                    bool bAdd = false;
                    bool bEdit = false;
                    bool bView = false;
                    bool bDelete = false;
                    bool bPrint = false;
                    if (lstGridData.Count > 0)
                    {
                        intUserRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "UserRgt_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objUserRightsDAL.UpdateAll(intUserRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                    }
                    i++;
                }
            }
            objUserRightsDAL = null;
            if (value > 0)
            {
                this.BindGrid();
                strMsg = "Record updated successfully.";
            }
            if (value <= 0)
            {
                strMsg = "Record not updated.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }
        #endregion 
      
    }
}