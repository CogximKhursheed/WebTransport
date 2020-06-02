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
    public partial class WhatsNew : Pagebase
    {
        #region Private Variable....
        private int intFormId = 9;
        #endregion

        #region Page Load Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.lnkbtnExcel);
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {
                SetDate();
                this.BindFormName();
            }
            txtDate.Attributes.Add("onkeypress", "return notAllowAnything(event);");
        }
        #endregion

        #region Functions...
        
        #endregion

        #region Buttons Events...
        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {

        }
        #endregion

        #region print...
      
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
        private void Export(DataTable Dt)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WhatsNew.xls"));
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
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
          
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            DateTime? DateOn;
            WhatsNewDAL obj1 = new WhatsNewDAL();

            DateOn = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            Int64 intWhatsIdno = 0;
            intWhatsIdno = obj1.Insert(DateOn,Convert.ToInt64(ddlForm.SelectedValue), txtDetails.Text);
            if (intWhatsIdno > 0)
            {
                ShowMessage("Record saved successfully.");
                this.ClearControls();
            }
        }

        private void BindFormName()
        {
            WhatsNewDAL obj = new WhatsNewDAL();
            var FrmName = obj.BindFormName();
            ddlForm.DataSource = FrmName;
            ddlForm.DataTextField = "FormName";
            ddlForm.DataValueField = "FormIdno";
            ddlForm.DataBind();
            obj = null;
            ddlForm.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void ClearControls()
        {
            txtDetails.Text = "";
            ddlForm.SelectedIndex = 0;
        }
        private void SetDate()
        {
                FinYearDAL objDAL = new FinYearDAL();
                var lst = objDAL.FilldateFromTo(3);
                hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
                hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
                if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)) >= DateTime.Now.Date && DateTime.Now.Date >= Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)))
                {
                    txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                else
                {
                    txtDate.Text = hidmindate.Value; 
                }
            
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }

        protected void lnkbtnExcel_Click(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            DateTime? DateOn;
            DataTable dt = new DataTable();
            WhatsNewDAL obj1 = new WhatsNewDAL();
            DateOn = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDate.Text));
            var selectdata = obj1.SelectAll(DateOn, Convert.ToInt64(ddlForm.SelectedValue));
            obj1 = null;
            if (selectdata != null && selectdata.Count > 0)
            {
                dt.Columns.Add("SrNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("FormName", typeof(string));
                dt.Columns.Add("Details", typeof(string));

                for (int i = 0; i < selectdata.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["SrNo"] = Convert.ToString(i + 1);
                    dr["Date"] = Convert.ToString(DataBinder.Eval(selectdata[i], "Date"));
                    dr["FormName"] = Convert.ToString(DataBinder.Eval(selectdata[i], "FormName"));
                    dr["Details"] = Convert.ToString(DataBinder.Eval(selectdata[i], "Details"));
                    dt.Rows.Add(dr);
                }
            }
            Export(dt);
        }
    }
}