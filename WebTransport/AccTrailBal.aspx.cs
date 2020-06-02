using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using WebTransport.Classes;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class AccTrailBal : Pagebase
    {
        #region Variables....
        string conString = ""; static string passAction = "";
        AccountBookDAL objAccountBookDAL = new AccountBookDAL();
        Double dNetAmount1 = 0; Double dNetAmount2 = 0;
        Double bal; Double bal1;
        int Id = 0; string DateFrm = ""; int year1 = 0; string DateTo = ""; int YearIDno = 0;
        private int intFormId = 2;
        #endregion

        #region PageLoad Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            conString = ApplicationFunction.ConnectionString();
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
                BindGroup();



                ddlTB.SelectedIndex = 1;

                //if (ddlTB.SelectedItem.Text == "Group Wise")
                //{

                //    if ((Request.QueryString["agrpId"] != null) && (Request.QueryString["cmpid"] != null))
                //    {
                //        for (int count = 0; count <= chkIemModel.Items.Count - 1; count++)
                //        {
                //            if (Convert.ToString(Request.QueryString["agrpId"]) == Convert.ToString(chkIemModel.Items[count].Value))
                //                chkIemModel.Items[count].Selected = true;
                //        }
                //    }
                //    else if (Request.QueryString["agrpId"] != null)
                //    {
                //        for (int count = 0; count <= chkIemModel.Items.Count - 1; count++)
                //        {
                //            if (Convert.ToString(Request.QueryString["agrpId"]) == Convert.ToString(chkIemModel.Items[count].Value))
                //                chkIemModel.Items[count].Selected = true;
                //        }
                //    }
                //    else
                //    {
                //        for (int count = 0; count <= chkIemModel.Items.Count - 1; count++)
                //        {
                //            chkIemModel.Items[count].Selected = true;
                //        }
                //    }
                //    chkIemModel.Enabled = chklist.Visible = true;
                //}
                //else { chkIemModel.Enabled = chklist.Visible = false; }
                chkBal.Checked = true;
                chkBal.Enabled = true;
                chkYearOpng.Visible = false;
                BindDateRange();
              
                    SetDate();
                
                //ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                //ddlcompany.SelectedValue = Convert.ToString(base.UserWrkComp);
            }
            txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            //RangeValidator1.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
            //RangeValidator1.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");

            //RangeValidator2.MinimumValue = Convert.ToDateTime(hidmindate.Value).ToString("dd-MM-yyyy");
            //RangeValidator2.MaximumValue = Convert.ToDateTime(hidmaxdate.Value).ToString("dd-MM-yyyy");
            ddlDateRange.Focus();

            if (Request.QueryString["DtrngId"] != null)
            {

                Int32 agrpId = Convert.ToInt32(Request.QueryString["agrpId"] == null ? "0" : Request.QueryString["agrpId"]);
                if (agrpId > 0)
                {
                    ddlTB.SelectedIndex = 0;
                    for (int i = 0; i < chkIemModel.Items.Count - 1; i++)
                    {
                        if (chkIemModel.Items[i].Value == agrpId.ToString())
                        {
                            chkIemModel.Items[i].Selected = true;
                        }
                    }
                }
                this.BindGrid();
            }
        }
        #endregion

        #region Functions...
        private void        BindGroup()
        {
            chkIemModel.DataSource = null;
            chkIemModel.Items.Clear();
            DataTable dt = objAccountBookDAL.FillPrtyAndItem(conString, "groups");
            if (dt.Rows.Count > 0)
            {
                chkIemModel.DataSource = dt;
                chkIemModel.DataTextField = "AHEAD_NAME";
                chkIemModel.DataValueField = "AHead_Idno";
                chkIemModel.DataBind();
                objAccountBookDAL = null;
            }
            else
            {
                chkIemModel.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void lnkbtnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())) > Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text.Trim())))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Validfrom date can not be greater than Validto date!');", true);
                        txtDateFrom.Focus();
                        return;
                    }
                }
                if (ddlTB.SelectedIndex == 0)
                {
                    if (chkIemModel.SelectedValue.Count() == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "ShowMessage('Please Select Group Name!');", true);
                        return;
                    }
                }
                this.BindGrid();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private bool Print()
        {
            Repeater obj = new Repeater();
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string email = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            if (CompDetl.Tables[0] != null && CompDetl.Tables[0].Rows.Count > 0)
            {
                CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
                Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
                Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
                email = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
                //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
                PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
                City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
                State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
                TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
                // ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Serv_Tax"]);
                FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
                PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
                lblCompanyname1.Text = CompName; //lblCompname1.Text = "For - " + CompName;
                lblCompAdd3.Text = Add1;
                lblCompAdd4.Text = Add2;
                lblCompCity1.Text = City;
                lblCompState1.Text = State;
                lblTin1.Text = TinNo;
                lbltxtPanNo1.Text = PanNo;
                #endregion
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void BindGrid()
        {
            try
            {
                Id = Convert.ToInt32(ViewState["Id"]);
                grdMain.DataSource = null;
                grdMain.DataBind();
                DataTable dsTable1 = ConvertToDatatable();
                if (dsTable1 != null)
                {
                    if (dsTable1.Rows.Count > 0)
                    {
                        if (Print() == true)
                        {
                            lnkbtnPrint.Visible = true;
                            Repeater1.DataSource = dsTable1;
                            Repeater1.DataBind();
                        }
                        grdMain.DataSource = dsTable1;
                        grdMain.DataBind();
                        view_print.Visible = true;
                    }
                }
                else
                {
                    lnkbtnPrint.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    view_print.Visible = false;
                }
                objAccountBookDAL = null;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        private DataTable ConvertToDatatable()
        {
            AccountBookDAL objAccBookDAL = new AccountBookDAL();
            DataTable dTN = new DataTable();
            hidstr.Value = "";
            if (chkIemModel.SelectedValue.Count() > 0)
            {
                for (int count = 0; count <= chkIemModel.Items.Count - 1; count++)
                {
                    if (chkIemModel.Items[count].Selected == true)
                    {
                        hidstr.Value = chkIemModel.Items[count].Value + "," + hidstr.Value;
                    }
                }
            }
            string strAction = "";
            
            string strDateFrom = Convert.ToString(txtDateFrom.Text.Trim());
            string strDateTo = Convert.ToString(txtDateTo.Text.Trim());
            int intyearIdno = Convert.ToInt32(ddlDateRange.SelectedValue);
         
            DataSet dsTable = new DataSet();
            #region Action Selection...
            if (ddlTB.SelectedIndex == 1) // Ledger Wise
            {
                if (chkYearOpng.Checked == true)
                {
                    if (chkBal.Checked == true)
                    {
                     
                      strAction = "TBYrCompOneTrue";
                    }
                    else
                    {
                        
                         strAction = "TBYrCompOneFalse"; 
                    }
                }
                else
                {
                    if (chkBal.Checked == true)
                    {
                        
                       strAction = "TBCompOneTrue"; 
                    }
                    else
                    {
                       
                       strAction = "TBCompOneFalse"; 
                    }
                }
            }
            else  // Group Wise
            {
                if (chkYearOpng.Checked == true)
                {
                    if (chkBal.Checked == true)
                    {
                   
                         strAction = "TBGrpYrCompOneTrueGroup";
                         passAction = "TBGrpYrCompOneTrue";
                    }
                    else
                    {
                     
                         strAction = "TBGrpYrCompOneFalseGroup";
                         passAction = "TBGrpYrCompOneFalse";
                    }
                }
                else
                {
                    if (chkBal.Checked == true)
                    {
                     
                        strAction = "TBGrpCompOneTrueGroup";
                        passAction = "TBGrpCompOneTrue";
                    }
                    else
                    {
                     
                        strAction = "TBGrpCompOneFalseGroup";
                        passAction = "TBGrpCompOneFalse";
                    }
                }
            }
            #endregion
            dsTable = objAccBookDAL.FillTrialBalanceGrid(conString, strAction, Request.QueryString["FDt"] == null ? strDateFrom : Request.QueryString["FDt"], Request.QueryString["TDt"] == null ? strDateTo : Request.QueryString["FDt"], Request.QueryString["DtrngId"] == null ? intyearIdno : Convert.ToInt32(Request.QueryString["DtrngId"]), hidstr.Value);

            #region
            if (dsTable != null && dsTable.Tables[0].Rows.Count > 0)
            {
                dTN = dsTable.Tables[0];
                chkYearOpng.Visible = true;
                var row = dTN.NewRow();
                if (ddlTB.SelectedValue == "0")
                {
                    row["Group"] = "Grand Total";
                    row["SubGroup"] = "";
                    row["Particulars"] = "";
                }
                else
                {
                    row["Particulars"] = "Grand Total";
                }
                row["OpeningDebit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(OpeningDebit)", "")));
                row["OpeningCredit"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(OpeningCredit)", "")));
                row["DebitAmount"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(DebitAmount)", "")));
                row["CreditAmount"] = string.Format("{0:0,0.00}", Convert.ToDouble(dTN.Compute("Sum(CreditAmount)", "")));
                dTN.Rows.Add(row);


                String OpnDC = string.Format("{0:0,0.00}", (Convert.ToDouble(row["OpeningDebit"]) - Convert.ToDouble(row["OpeningCredit"])));
                String AmntDC = string.Format("{0:0,0.00}", (Convert.ToDouble(row["DebitAmount"]) - Convert.ToDouble(row["CreditAmount"])));
                string OpnDC1 = OpnDC.Substring(0, 1);
                string AmntDC1 = AmntDC.Substring(0, 1);

                var row2 = dTN.NewRow();
                if (ddlTB.SelectedValue == "0")
                {
                    row2["Group"] = "Difference in Total";
                    row2["SubGroup"] = "";
                    row2["Particulars"] = "";
                }
                else
                {
                    row2["Particulars"] = "Difference in Total";
                }

                if (OpnDC1 == "-")
                {
                    row2["OpeningCredit"] = string.Format("{0:0,0.00}", 0);
                    row2["OpeningDebit"] = string.Format("{0:0,0.00}", Math.Abs(Convert.ToDouble(row["OpeningDebit"]) - Convert.ToDouble(row["OpeningCredit"])));
                }
                else
                {
                    row2["OpeningDebit"] = string.Format("{0:0,0.00}", 0);
                    row2["OpeningCredit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["OpeningDebit"]) - Convert.ToDouble(row["OpeningCredit"])));
                }
                if (AmntDC1 == "-")
                {
                    row2["CreditAmount"] = string.Format("{0:0,0.00}", 0);
                    row2["DebitAmount"] = string.Format("{0:0,0.00}", Math.Abs(Convert.ToDouble(row["DebitAmount"]) - Convert.ToDouble(row["CreditAmount"])));
                }
                else
                {
                    row2["DebitAmount"] = string.Format("{0:0,0.00}", 0);
                    row2["CreditAmount"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["DebitAmount"]) - Convert.ToDouble(row["CreditAmount"])));
                }
                dTN.Rows.Add(row2);
                var row3 = dTN.NewRow();
                if (ddlTB.SelectedValue == "0")
                {
                    row3["Group"] = "Net Total";
                    row3["SubGroup"] = "";
                    row3["Particulars"] = "";
                }
                else
                {
                    row3["Particulars"] = "Net Total";
                }
                row3["OpeningDebit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["OpeningDebit"]) + (Convert.ToDouble(row2["OpeningDebit"]))));
                row3["OpeningCredit"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["OpeningCredit"]) + (Convert.ToDouble(row2["OpeningCredit"]))));
                row3["DebitAmount"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["DebitAmount"]) + (Convert.ToDouble(row2["DebitAmount"]))));
                row3["CreditAmount"] = string.Format("{0:0,0.00}", (Convert.ToDouble(row["CreditAmount"]) + (Convert.ToDouble(row2["CreditAmount"]))));
                dTN.Rows.Add(row3);
            }
            #endregion

            return dTN;
        }
        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdMain.GridLines = GridLines.Both;
            PrepareGridViewForExport(grdMain);
            ExportGridView();
        }
        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            bool bVisible = true;
            string name = String.Empty;

            //lnkbtn7Particular
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    bVisible = (gv.Controls[i] as LinkButton).Visible;
                    gv.Controls.Remove(gv.Controls[i]);
                    if (bVisible == true)
                    {
                        gv.Controls.AddAt(i, l);
                    }
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
        #endregion Excel

        #region Events...
        protected void ddlTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTB.SelectedIndex == 0)
            {
                chkIemModel.Enabled = true;
                chklist.Visible = true;
            }
            else
            {
                chkIemModel.Enabled = false;
                chklist.Visible = false;
                for (int count = 0; count <= chkIemModel.Items.Count - 1; count++)
                {
                    chkIemModel.Items[count].Selected = false;
                }
                chkBal.Enabled = true;
            }
            grdMain.DataSource = null;
            grdMain.DataBind();
        }
        protected void chkYearOpng_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid Event ...
        protected void grdMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMain.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdshowdetailTB")
            {
                #region Redirect To other Page Code...
                int party = 0; string Party11 = ""; string type = ""; string AgrpIdno = ""; string PrtyIdno = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                if (ddlTB.SelectedValue == "1")
                {
                    PrtyIdno = Convert.ToString(e.CommandArgument);
                }
                else
                {
                    AgrpIdno = Convert.ToString(e.CommandArgument);
                }
                if (ddlTB.SelectedIndex == 0)
                {
                    type = "0";
                }
                else
                {
                    type = "1";
                }
                string Date1 = DateTime.Now.Date.Year.ToString();
                string Datefrm = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text)).ToString("dd-MM-yyyy");
                string dateto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateTo.Text)).ToString("dd-MM-yyyy");
                int year = Convert.ToInt32(ddlDateRange.SelectedValue);
                int year1 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(txtDateFrom.Text.Trim())).Year;
                Session["Year1"] = year1;
                Session["PartyName"] = Party11;
                string PartyName = Party11.Replace("&", " ");

                string url = "";
                if (ddlTB.SelectedValue == "1")
                {
                    url = "AccBTrailBaln.aspx?party=" + PrtyIdno + "&Datefrm=" + Datefrm + "&DateTo=" + dateto + "&Year=" + year + "&PartyName=" + PartyName + "&Year1=" + year1;
                }
                else
                {
                    url = "AccTrailBalGroup.aspx?party=" + party + "&Datefrm=" + Datefrm + "&DateTo=" + dateto + "&Year=" + year + "&PartyName=" + PartyName + "&Year1=" + year1 + "&AgrpIdno=" + AgrpIdno + "&Action=" + passAction + "&Type=" + type + "&ChkBal=" + chkBal.Checked + "&chkYearOpng=" + chkYearOpng.Checked;
                }
                string fullURL = "window.open('" + url + "', '_blank' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                #endregion
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl7Particular = (Label)e.Row.FindControl("lbl7Particular");
                LinkButton lnkbtn7Particular = (LinkButton)e.Row.FindControl("lnkbtn7Particular");//New Added
                string Particular = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));//new Added
                lbl7Particular.Text = Particular;//new Added
                lnkbtn7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));//new Added

                Label lbl8ODebit = (Label)e.Row.FindControl("lbl8ODebit");
                lbl8ODebit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpeningDebit")).ToString("N", new CultureInfo("hi-IN")));

                Label lbl9OCredit = (Label)e.Row.FindControl("lbl9OCredit");
                lbl9OCredit.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OpeningCredit")).ToString("N", new CultureInfo("hi-IN")));


                Label lbl10DebitA = (Label)e.Row.FindControl("lbl10DebitA");
                lbl10DebitA.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount")).ToString("N", new CultureInfo("hi-IN")));
                string debitTB = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount")).ToString("N", new CultureInfo("hi-IN")));

                Label lbl11CreditA = (Label)e.Row.FindControl("lbl11CreditA");
                lbl11CreditA.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CreditAmount")).ToString("N", new CultureInfo("hi-IN")));
                string CreditTB = string.Format("{0:0,0.00}", Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CreditAmount")).ToString("N", new CultureInfo("hi-IN")));

                if (ddlTB.SelectedValue == "0") // Group Wise
                {
                    lnkbtn7Particular.Visible = false;
                    Label lbl5Group = (Label)e.Row.FindControl("lbl5Group");
                    string Group = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                    lbl5Group.Text = Group;//end
                    LinkButton lnkBtn5Group = (LinkButton)e.Row.FindControl("lnkBtn5Group");
                    lnkBtn5Group.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));

                    Label lbl6SubGroup = (Label)e.Row.FindControl("lbl6SubGroup");
                    lbl6SubGroup.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SubGroup"));
                    if (Group.ToLower() == "grand total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DodgerBlue;
                    }
                    if (Group.ToLower() == "difference in total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
                    }
                    if (Group.ToLower() == "net total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DarkTurquoise;
                    }
                    if (debitTB.ToLower() == "0.00" && CreditTB.ToLower() == "0.00")
                    {
                        lbl5Group.Visible = true;
                        lnkBtn5Group.Visible = false;
                        lbl5Group.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                    }
                    else
                    {
                        lbl5Group.Visible = false;
                        lnkBtn5Group.Visible = true;
                        lnkBtn5Group.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                    }
                    if (Group.ToLower() == "grand total" || Group.ToLower() == "difference in total" || Group.ToLower() == "net total")
                    {
                        lnkBtn5Group.Visible = false;
                        lbl5Group.Visible = true;
                        lbl5Group.Text = Group;
                    }
                    else
                    {
                        lnkBtn5Group.Visible = true;
                        lbl5Group.Visible = false;
                        lnkBtn5Group.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Group"));
                    }
                }
                else// Ledger Wise
                {

                    if (debitTB.ToLower() == "0.00" && CreditTB.ToLower() == "0.00")
                    {
                        lnkbtn7Particular.Visible = false;
                        lbl7Particular.Visible = true;
                        lbl7Particular.Text = Particular;
                    }
                    else
                    {
                        lnkbtn7Particular.Visible = true;
                        lbl7Particular.Visible = false;
                        lnkbtn7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                    }
                    if (Particular.ToLower() == "grand total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DodgerBlue;
                    }
                    if (Particular.ToLower() == "difference in total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
                    }
                    if (Particular.ToLower() == "net total")
                    {
                        e.Row.BackColor = System.Drawing.Color.DarkTurquoise;
                    }
                    if (Particular.ToLower() == "grand total" || Particular.ToLower() == "difference in total" || Particular.ToLower() == "net total")
                    {
                        lnkbtn7Particular.Visible = false;
                        lbl7Particular.Visible = true;
                        lbl7Particular.Text = Particular;
                    }
                    else
                    {
                        lnkbtn7Particular.Visible = true;
                        lbl7Particular.Visible = false;
                        lnkbtn7Particular.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Particulars"));
                    }
                }
                #region Columns Visibility....
                grdMain.Columns[2].Visible = true;
                grdMain.Columns[5].Visible = true;
                grdMain.Columns[6].Visible = true;
                if (ddlTB.SelectedValue == "1") // Ledger Wise
                {
                    if (chkBal.Checked == true)
                    {
                        grdMain.Columns[0].Visible = false;
                        grdMain.Columns[1].Visible = false;
                        grdMain.Columns[3].Visible = false;
                        grdMain.Columns[4].Visible = false;

                    }
                    else
                    {
                        grdMain.Columns[0].Visible = false;
                        grdMain.Columns[1].Visible = false;
                        grdMain.Columns[3].Visible = true;
                        grdMain.Columns[4].Visible = true;
                    }
                }
                else // Group Wise
                {
                    if (chkBal.Checked == true)
                    {
                        grdMain.Columns[0].Visible = true;
                        grdMain.Columns[1].Visible = false;
                        grdMain.Columns[2].Visible = false;
                        grdMain.Columns[3].Visible = false;
                        grdMain.Columns[4].Visible = false;

                    }
                    else
                    {
                        grdMain.Columns[0].Visible = true;
                        grdMain.Columns[1].Visible = false;
                        grdMain.Columns[2].Visible = false;
                        grdMain.Columns[3].Visible = true;
                        grdMain.Columns[4].Visible = true;
                    }
                }
                #endregion
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }
        #endregion

        #region Date Range FinYear ...
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(conString);
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }
        private void SetDate()
        {
            Int32 intyearid = Convert.ToInt32(ddlDateRange.SelectedValue);
            FinYearDAL objFinYearDAL = new FinYearDAL();
            var lst = objFinYearDAL.FilldateFromTo(intyearid);
            hidmindate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "StartDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "StartDate")).ToString("dd-MM-yyyy"));
            hidmaxdate.Value = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(lst[0], "EndDate"))) ? "" : Convert.ToString(Convert.ToDateTime(DataBinder.Eval(lst[0], "EndDate")).ToString("dd-MM-yyyy"));
            if (ddlDateRange.SelectedIndex != 0)
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = hidmaxdate.Value;
            }
            else
            {
                txtDateFrom.Text = hidmindate.Value;
                txtDateTo.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            }
        }
        #endregion



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string Particular = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Group"));
                if (Particular.ToLower() == "grand total")
                {
                    e.Item.DataItem = System.Drawing.Color.DodgerBlue;
                }
                if (Particular.ToLower() == "difference in total")
                {
                    e.Item.DataItem = System.Drawing.Color.DeepSkyBlue;
                }
                if (Particular.ToLower() == "net total")
                {
                    e.Item.DataItem = System.Drawing.Color.DarkTurquoise;
                }
                if (ddlTB.SelectedValue == "1")
                {
                    //var lbGrp = e.Item.FindControl("lbGrp") as Control; lbGrp.Visible = false;
                    //var lbSubGrp = e.Item.FindControl("lbSubGrp"); lbSubGrp.Visible = false;
                    //var lbGrp1 = e.Item.FindControl("lbGrp1"); lbGrp1.Visible = false;
                    //var lbSubGrp1 = e.Item.FindControl("lbSubGrp1"); lbSubGrp1.Visible = false;
                }
                else
                {
                    //var lbPar1 = e.Item.FindControl("lbPar1"); lbPar1.Visible = false;
                    //var lbPar = e.Item.FindControl("lbPar"); lbPar1.Visible = false;
                }
            }
        }


    }
}