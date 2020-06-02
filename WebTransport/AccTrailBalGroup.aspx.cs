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
    public partial class AccTrailBalGroup : Pagebase
    {
        #region Variables....
        string conString = "";
        AccountBookDAL objAccountBookDAL = new AccountBookDAL();
        Double dNetAmount1 = 0; Double dNetAmount2 = 0;
        Double bal; Double bal1;
        int Id = 0; string DateFrm = ""; int year1 = 0; string DateTo = ""; int YearIDno = 0;
        private int intFormId = 2;
        string type="";string chkBal="";string Action="";string AgrpIdno="";string DateFrom="";
        string DateToSes = ""; int yearidno = 0; string party = ""; string partyName = ""; string chkYearOpng = "";
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

                type = Request.QueryString["type"].ToString();
                chkBal = Request.QueryString["ChkBal"].ToString();
                Action = Request.QueryString["Action"].ToString();
                AgrpIdno = Request.QueryString["AgrpIdno"].ToString();
                DateFrom = Request.QueryString["Datefrm"].ToString();
                DateToSes = Request.QueryString["DateTo"].ToString();
                yearidno = Convert.ToInt32(Request.QueryString["Year"].ToString());
                party = Request.QueryString["party"].ToString();
                partyName = Request.QueryString["PartyName"].ToString();
                year1 = Convert.ToInt32(Request.QueryString["Year1"].ToString());
                partyName = Request.QueryString["PartyName"].ToString();
                chkYearOpng = Request.QueryString["chkYearOpng"].ToString();
                hidmindate.Value = DateFrom.ToString();
                hidmaxdate.Value = DateToSes.ToString();
                BindGrid();
            }
        }
        #endregion

        #region Functions...


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
                if (dsTable1 != null && dsTable1.Rows.Count > 0)
                {
                    if (Print() == true)
                    {
                        lnkbtnPrint.Visible = true;
                        Repeater1.DataSource = dsTable1;
                        Repeater1.DataBind();
                    }
                    grdMain.DataSource = dsTable1;
                    grdMain.DataBind();
                    prints.Visible = true;
                }
                else
                {
                    lnkbtnPrint.Visible = false;
                    grdMain.DataSource = null;
                    grdMain.DataBind();
                    prints.Visible = false;
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
            string strAction = "";
         
            DataSet dsTable = new DataSet();
            #region Action Selection...
            if (type == "1") // Ledger Wise
            {
                if (chkYearOpng == "true")
                {
                    if (chkBal == "true")
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
                    if (chkBal == "true")
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
                if (chkYearOpng == "true")
                {
                    if (chkBal == "true")
                    {
                   
                         strAction = "TBGrpYrCompOneTrue"; 
                }
                    else
                    {
                     
                         strAction = "TBGrpYrCompOneFalse"; 
                    }
                }
                else
                {
                    if (chkBal == "true")
                    {
                     
                        strAction = "TBGrpCompOneTrue";
                    }
                    else
                    {
                     
                        strAction = "TBGrpCompOneFalse";
                    }
                }
            }
            #endregion
            dsTable = objAccBookDAL.FillTrialBalanceGrid(conString, strAction, DateFrom, DateToSes, yearidno, AgrpIdno);

            #region
            if (dsTable != null && dsTable.Tables[0].Rows.Count > 0)
            {
                dTN = dsTable.Tables[0];
                chkYearOpng = "true";
                var row = dTN.NewRow();
                if (type == "0")
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
                if (type == "0")
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
                if (type == "0")
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
                int party = 0; string Party11 = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                if (type == "0")
                {
                    LinkButton lnkbtn7Particular = (LinkButton)row.FindControl("lnkbtn7Particular");
                    party = Convert.ToInt32(lnkbtn7Particular.CommandArgument);
                    Party11 = lnkbtn7Particular.Text.Trim();
                }
                else
                {
                    LinkButton lnkbtn7Particular = (LinkButton)row.FindControl("lnkbtn7Particular");
                    party = Convert.ToInt32(lnkbtn7Particular.CommandArgument);
                    Party11 = lnkbtn7Particular.Text.Trim();
                }
                string Date1 = DateTime.Now.Date.Year.ToString();
                string Datefrm = Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmindate.Value)).ToString("dd-MM-yyyy");
                string dateto = Convert.ToDateTime(ApplicationFunction.mmddyyyy(hidmaxdate.Value)).ToString("dd-MM-yyyy");
                int year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                int year1 = Convert.ToDateTime(ApplicationFunction.mmddyyyy(Request.QueryString["DateTo"].ToString())).Year;
                Session["Year1"] = year1;
                Session["PartyName"] = Party11;
                string PartyName = Party11.Replace("&", " ");

                string url = "AccBTrailBaln.aspx?party=" + party + "&Datefrm=" + Datefrm + "&DateTo=" + dateto + "&Year=" + year + "&PartyName=" + PartyName + "&Year1=" + year1;
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

                if (type == "0") // Group Wise
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
                if (type == "1") // Ledger Wise
                {
                    if (chkBal == "true")
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
                    if (chkBal == "true")
                    {
                        grdMain.Columns[0].Visible = true;
                        grdMain.Columns[1].Visible = true;
                        grdMain.Columns[3].Visible = false;
                        grdMain.Columns[4].Visible = false;

                    }
                    else
                    {
                        grdMain.Columns[0].Visible = true;
                        grdMain.Columns[1].Visible = true;
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
            }
        }


    }
}