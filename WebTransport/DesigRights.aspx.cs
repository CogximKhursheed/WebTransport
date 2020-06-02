using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using WebTransport.Account;
using System.IO;
using System.Transactions;

namespace WebTransport
{
    public partial class DesigRights : Pagebase
    {
        #region Variables declaration...
        private int intFormId = 18;
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
                if (base.Edit == false)
                {
                    lnkbtnPreview.Visible = true;
                }
                selectall.Visible = false;
                this.BindDesignation();
                if (Request.QueryString["DesigIdno"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["DesigIdno"]));

                    this.BindGrid();
                }
            }
        }
        #endregion

        #region Functions...
        private void Populate(int DesignIdno)
        {
            this.BindDesignation();
            ddlDesign.SelectedValue = Convert.ToString(DesignIdno);
            ddlDesign.Enabled = true;
        }
        /// <summary>
        /// To Bind Grid
        /// </summary>
        private void BindGrid()
        {
            // AutomobileOnline.Model.clsDesigRightsDAL objclsDesigRightsDAL = new AutomobileOnline.Model.clsDesigRightsDAL();
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();

            if (ddlType.SelectedItem.Text == "Form")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeForm(Convert.ToInt32(ddlDesign.SelectedValue));
                objclsDesigRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else if (ddlType.SelectedItem.Text == "Menu")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeMenu(Convert.ToInt32(ddlDesign.SelectedValue));
                objclsDesigRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else if (ddlType.SelectedItem.Text == "Report")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeRep(Convert.ToInt32(ddlDesign.SelectedValue));
                objclsDesigRightsDAL = null;
                grdMain.DataSource = lstGridData;
                grdMain.DataBind();

                int startRowOnPage = (grdMain.PageIndex * grdMain.PageSize) + 1;
                int lastRowOnPage = startRowOnPage + grdMain.Rows.Count - 1;
                lblcontant.Text = "Showing " + startRowOnPage.ToString() + " - " + lastRowOnPage.ToString() + " of " + lstGridData.Count.ToString();
                lblcontant.Visible = true;
                divpaging.Visible = true;
            }
            else
            { lblcontant.Visible = false;
              divpaging.Visible = false;}


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
        private void BindDesignation()
        {
            //  AutomobileOnline.Model.clsDesigRightsDAL objclsDesigRightsDAL = new AutomobileOnline.Model.clsDesigRightsDAL();
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();
            var objDesignRights = objclsDesigRightsDAL.SelectDesignation();
            objclsDesigRightsDAL = null;
            ddlDesign.DataSource = objDesignRights;
            ddlDesign.DataTextField = "Desig_Name";
            ddlDesign.DataValueField = "Desig_Idno";
            ddlDesign.DataBind();
            ddlDesign.Items.Insert(0, new ListItem("< Choose >", "0"));
        }
        #endregion

        #region Grid Events...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int32 empIdno = Convert.ToInt32((Session["UserIdno"] == null) ? "0" : Session["UserIdno"].ToString());
            string strMsg = string.Empty;
            int intDesigRghtsIdno = 0;
            bool bAdd = false;
            bool bEdit = false;
            bool bView = false;
            bool bDelete = false;
            bool bPrint = false;
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();
            HiddenField hiddesigId = (HiddenField)grdMain.Rows[0].FindControl("hiddesigId");
            int intDesignIdno = Convert.ToInt32(hiddesigId.Value);
            if (e.CommandName == "cmdAdd")
            {
                string[] strAdd = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strAdd.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strAdd[0]);
                    if (Convert.ToBoolean(strAdd[1]) == true)
                        bAdd = false;
                    else
                        bAdd = true;

                    int value = objclsDesigRightsDAL.UpdateAdd(intDesigRghtsIdno, bAdd, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdEdit")
            {
                string[] strEdit = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strEdit.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strEdit[0]);
                    if (Convert.ToBoolean(strEdit[1]) == true)
                        bEdit = false;
                    else
                        bEdit = true;
                    int value = objclsDesigRightsDAL.UpdateEdit(intDesigRghtsIdno, bEdit, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdView")
            {
                string[] strView = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strView.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strView[0]);
                    if (Convert.ToBoolean(strView[1]) == true)
                        bView = false;
                    else
                        bView = true;
                    int value = objclsDesigRightsDAL.UpdateView(intDesigRghtsIdno, bView, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdDelete")
            {
                string[] strDelete = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strDelete.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strDelete[0]);
                    if (Convert.ToBoolean(strDelete[1]) == true)
                        bDelete = false;
                    else
                        bDelete = true;
                    int value = objclsDesigRightsDAL.UpdateDelete(intDesigRghtsIdno, bDelete, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdPrint")
            {
                string[] strPrint = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strPrint.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strPrint[0]);
                    if (Convert.ToBoolean(strPrint[1]) == true)
                        bPrint = false;
                    else
                        bPrint = true;
                    int value = objclsDesigRightsDAL.UpdatePrint(intDesigRghtsIdno, bPrint, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            if (e.CommandName == "cmdSelectAll")
            {
                string[] strAll = Convert.ToString(e.CommandArgument).Split(new char[] { '_' });
                if (strAll.Length > 1)
                {
                    intDesigRghtsIdno = Convert.ToInt32(strAll[0]);
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
                    value = objclsDesigRightsDAL.UpdateAll(intDesigRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                    objclsDesigRightsDAL = null;
                    if (value > 0)
                    {
                        this.BindGrid();
                        strMsg = "Record updated successfully.";
                    }
                }
            }
            bool bDesigRights = true;
            if (bAdd == true && bEdit == true && bView == true && bDelete == true && bPrint == true)
            {
                bDesigRights = true;
            }
            if (bAdd == false && bEdit == false && bView == false && bDelete == false && bPrint == false)
            {
                bDesigRights = false;
            }
            int intDesigRights = 0;
            //  AutomobileOnline.Model.clsDesigRightsDAL objclsDesigRightsDAL1 = new AutomobileOnline.Model.clsDesigRightsDAL();
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL1 = new WebTransport.DAL.DesigRightsDAL();
            intDesigRights = objclsDesigRightsDAL1.UpdateDesigRights(intDesignIdno, bDesigRights);
            objclsDesigRightsDAL1 = null;
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
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
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
            int intDesigRghtsIdno = 0;
            // AutomobileOnline.Model.clsDesigRightsDAL objclsDesigRightsDAL = new AutomobileOnline.Model.clsDesigRightsDAL();
            WebTransport.DAL.DesigRightsDAL objclsDesigRightsDAL = new WebTransport.DAL.DesigRightsDAL();
            if (ddlType.SelectedItem.Text == "Form")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeForm(Convert.ToInt32(ddlDesign.SelectedValue));
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
                        intDesigRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "DesigRghts_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objclsDesigRightsDAL.UpdateAll(intDesigRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                        bool bDesigRights = true;
                        if (bAdd == true && bEdit == true && bView == true && bDelete == true && bPrint == true)
                        {
                            bDesigRights = true;
                        }
                        if (bAdd == false && bEdit == false && bView == false && bDelete == false && bPrint == false)
                        {
                            bDesigRights = false;
                        }
                        int intDesigRights = 0;
                        intDesigRights = objclsDesigRightsDAL.UpdateDesigRights(Convert.ToInt32(ddlDesign.SelectedValue)/*intDesigRghtsIdno*/, bDesigRights);
                    }
                    i++;
                }
            }
            else if (ddlType.SelectedItem.Text == "Menu")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeMenu(Convert.ToInt32(ddlDesign.SelectedValue));
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
                        intDesigRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "DesigRghts_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objclsDesigRightsDAL.UpdateAll(intDesigRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                        bool bDesigRights = true;
                        if (bAdd == true && bEdit == true && bView == true && bDelete == true && bPrint == true)
                        {
                            bDesigRights = true;
                        }
                        if (bAdd == false && bEdit == false && bView == false && bDelete == false && bPrint == false)
                        {
                            bDesigRights = false;
                        }
                        int intDesigRights = 0;
                        intDesigRights = objclsDesigRightsDAL.UpdateDesigRights(Convert.ToInt32(ddlDesign.SelectedValue)/*intDesigRghtsIdno*/, bDesigRights);
                    }
                    i++;
                }
            }
            else if (ddlType.SelectedItem.Text == "Report")
            {
                var lstGridData = objclsDesigRightsDAL.SelectForGridTypeRep(Convert.ToInt32(ddlDesign.SelectedValue));
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
                        intDesigRghtsIdno = Convert.ToInt32(DataBinder.Eval(lstGridData[i], "DesigRghts_Idno"));
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked == true)
                        {
                            bAdd = true; bEdit = true; bView = true; bDelete = true; bPrint = true;
                        }
                        else if (chkSelect.Checked == false)
                        {
                            bAdd = false; bEdit = false; bView = false; bDelete = false; bPrint = false;
                        }
                        value = objclsDesigRightsDAL.UpdateAll(intDesigRghtsIdno, bAdd, bEdit, bView, bDelete, bPrint, empIdno);
                        bool bDesigRights = true;
                        if (bAdd == true && bEdit == true && bView == true && bDelete == true && bPrint == true)
                        {
                            bDesigRights = true;
                        }
                        if (bAdd == false && bEdit == false && bView == false && bDelete == false && bPrint == false)
                        {
                            bDesigRights = false;
                        }
                        int intDesigRights = 0;
                        intDesigRights = objclsDesigRightsDAL.UpdateDesigRights(Convert.ToInt32(ddlDesign.SelectedValue)/*intDesigRghtsIdno*/, bDesigRights);
                    }
                    i++;
                }
            }
            objclsDesigRightsDAL = null;
            if (value > 0)
            {
                this.BindGrid();
                strMsg = "Record updated successfully.";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
        }
        #endregion

        #region ddlDesign_SelectedIndexChanged
        protected void ddlDesign_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}