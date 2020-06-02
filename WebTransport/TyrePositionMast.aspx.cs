using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Account;
using WebTransport.Classes;
using System.Transactions;

namespace WebTransport
{
    public partial class TyrePositionMast : Pagebase
    {
        #region PageLaod Events...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            if (!Page.IsPostBack)
            {

                txtPostionName.Attributes.Add("onkeypress", "return allowOnlyAlphabet(event);");
                chkStatus.Checked = true;

                if (Request.QueryString["PostionId"] != null)
                {
                    this.Populate(Convert.ToInt32(Request.QueryString["PostionId"]));
                    lnkbtnNew.Visible = true;
                }
                else
                {
                    lnkbtnNew.Visible = false;
                }
                txtPostionName.Focus();
            }
        }
        #endregion

        #region Button Events...
        protected void lnkbtnSave_OnClick(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            PositionMastDAL objTolMast = new PositionMastDAL();
            Int64 intCityIdno = 0;

            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
            {
                if (string.IsNullOrEmpty(hidPositionidno.Value) == true)
                {
                    intCityIdno = objTolMast.Insert(txtPostionName.Text.Trim(), Convert.ToBoolean(chkStatus.Checked)); 
                }
                else
                {
                    intCityIdno = objTolMast.Update(Convert.ToInt64(hidPositionidno.Value), txtPostionName.Text.Trim(), Convert.ToBoolean(chkStatus.Checked));
                }
                objTolMast = null;

                if (intCityIdno > 0)
                {
                    ClearControls();
                    lnkbtnNew.Visible = false;

                    if (string.IsNullOrEmpty(hidPositionidno.Value) == false)
                    {
                        strMsg = "Record updated successfully.";
                        Tran.Complete();
                    }
                    else
                    {
                        strMsg = "Record saved successfully.";
                        Tran.Complete();
                    }
                }
                else if (intCityIdno < 0)
                {
                    strMsg = "Record already exists.";
                    Tran.Dispose();
                }
                else
                {
                    if (string.IsNullOrEmpty(hidPositionidno.Value) == false)
                    {
                        strMsg = "Record not updated.";
                        Tran.Dispose();
                    }
                    else
                    {
                        strMsg = "Record not saved.";
                        Tran.Dispose();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertstrMsg", "PassMessage('" + strMsg + "')", true);
                txtPostionName.Focus();
            }
        }


        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("TyrePositionMast.aspx");
        }


        protected void lnkbtnCancel_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidPositionidno.Value) == true)
            {
                this.ClearControls();
            }
            else
            {
                this.Populate(Convert.ToInt64(hidPositionidno.Value) == 0 ? 0 : Convert.ToInt64(hidPositionidno.Value));
            }
        }

        #endregion

        #region Miscellaneous Events...
        private void Populate(Int64 PositionIdno)
        {
            PositionMastDAL objPosit = new PositionMastDAL();
            tblPositionMast objPositMast = objPosit.SelectById(PositionIdno);
            objPosit = null;
            if (objPositMast != null)
            {
                txtPostionName.Text = Convert.ToString(objPositMast.Position_name);
                chkStatus.Checked = Convert.ToBoolean(objPositMast.IsActive);
                hidPositionidno.Value = Convert.ToString(objPositMast.Position_id);
                txtPostionName.Focus();
            }
        }
        private void ClearControls()
        {
            txtPostionName.Text = string.Empty;
            hidPositionidno.Value = null;
            chkStatus.Checked = true;
        }
        #endregion

    }
}