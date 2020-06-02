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
using WebTransport.Account;

namespace WebTransport
{
    public partial class DueDateLorryReport : Pagebase
    {
        #region Variable ...
        string conString = "";
        static FinYear UFinYear = new FinYear();
        LorryMasterRepDAL objLorryDAL = new LorryMasterRepDAL();
        private int intFormId = 43;
        DataTable CSVTable = new DataTable();
        int Fit, Ins, RC, Nat, Auth = 0;
        //  double dGrossAmount = 0, dBiltyAmount = 0, dShortAmount = 0, dTrServTaxAmount = 0,dConsignrServTaxAmount=0, dNetAmount = 0;
        #endregion

        #region Page Load Event ...
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                base.AutoRedirect();
            }
            conString = ApplicationFunction.ConnectionString();
            UFinYear = base.FatchFinYear(1);
            if (!Page.IsPostBack)
            {
                //if (base.CheckUserRights(intFormId) == false)
                //{
                //    Response.Redirect("PermissionDenied.aspx");
                //}
                if (base.View == false)
                {
                    
                }
                if (Request.QueryString["LorryDt"] != null)
                {
                    string date = Convert.ToString(Request.QueryString["LorryDt"]);
                    LorryMasterDAL objDash = new LorryMasterDAL();
                    DataTable Dt = objDash.SelectLorryIDs(date,ApplicationFunction.ConnectionString());
                    string strAllIds = ""; int iCount = 0;
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        strAllIds = strAllIds + Convert.ToString(Dt.Rows[i]["ID"]) + ",";
                        iCount++;
                    }
                    if (strAllIds.Length != 0)
                    {
                        strAllIds = strAllIds.Substring(0, strAllIds.Length - 1);
                    }
                    DataTable DtDet = objDash.SelectLorryDetDateWise(date, ApplicationFunction.ConnectionString());

                    DataTable DTemp = objDash.SelectLorrys(strAllIds, ApplicationFunction.ConnectionString());
                    
                    
                        //Fitness Date
                    if (DTemp.Rows.Count == 1)
                    {
                        for (int i = 0; i < DTemp.Rows.Count; i++)
                        {
                            for (int j = 0; j < DtDet.Rows.Count; j++)
                            {

                                if (Convert.ToInt32(DtDet.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[i]["Lorry_Idno"]) && Convert.ToString(DtDet.Rows[j]["Type"]) == "FIT")
                                {
                                    Fit += 1;
                                }
                                if (Convert.ToInt32(DtDet.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[i]["Lorry_Idno"]) && Convert.ToString(DtDet.Rows[j]["Type"]) == "INS")
                                {
                                    Ins += 1;
                                }
                                if (Convert.ToInt32(DtDet.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[i]["Lorry_Idno"]) && Convert.ToString(DtDet.Rows[j]["Type"]) == "RC")
                                {
                                    RC += 1;
                                }
                                if (Convert.ToInt32(DtDet.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[i]["Lorry_Idno"]) && Convert.ToString(DtDet.Rows[j]["Type"]) == "NAT")
                                {
                                    Nat += 1;
                                }
                                if (Convert.ToInt32(DtDet.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[i]["Lorry_Idno"]) && Convert.ToString(DtDet.Rows[j]["Type"]) == "AUTH")
                                {
                                    Auth += 1;
                                }

                            }
                            if (Fit == 0)
                            {
                                DTemp.Rows[i]["Fitness_Date"] = "";
                            }
                            if (Ins == 0)
                            {
                                DTemp.Rows[i]["Ins_Valid_Date"] = "";
                            }
                            if (RC == 0)
                            {
                                DTemp.Rows[i]["RC_Date"] = "";
                            }
                            if (Nat == 0)
                            {
                                DTemp.Rows[i]["Nat_Permit_Date"] = "";
                            }
                            if (Auth == 0)
                            {
                                DTemp.Rows[i]["Auth_Permit_Date"] = "";
                            }
                        }
                    }
                    else
                    {
                        DataTable dtnew = new DataTable();
                        DataColumn dtCol; dtCol = new DataColumn("ID", typeof(string));
                        dtnew.Columns.Add(dtCol); dtCol = new DataColumn("FIT", typeof(string));
                        dtnew.Columns.Add(dtCol); dtCol = new DataColumn("INS", typeof(string));
                        dtnew.Columns.Add(dtCol); dtCol = new DataColumn("RC", typeof(string));
                        dtnew.Columns.Add(dtCol); dtCol = new DataColumn("NAT", typeof(string));
                        dtnew.Columns.Add(dtCol); dtCol = new DataColumn("AUTH", typeof(string));
                        dtnew.Columns.Add(dtCol);

                        DataRow dr = dtnew.NewRow();
                        dr.BeginEdit();
                        dr[0] = ""; dr[1] = ""; dr[2] = ""; dr[3] = ""; dr[4] = ""; dr[5] = ""; dtnew.Rows.InsertAt(dr, 0); dtnew.AcceptChanges();
                        DataRow dr1 = dtnew.NewRow();
                        dr1.BeginEdit();
                        dr1[0] = ""; dr1[1] = ""; dr1[2] = ""; dr1[3] = ""; dr1[4] = ""; dr1[5] = ""; dtnew.Rows.InsertAt(dr1, 1); dtnew.AcceptChanges();
                        DataRow dr2 = dtnew.NewRow();
                        dr2.BeginEdit();
                        dr2[0] = ""; dr2[1] = ""; dr2[2] = ""; dr2[3] = ""; dr2[4] = ""; dr2[5] = ""; dtnew.Rows.InsertAt(dr2, 2); dtnew.AcceptChanges();
                        DataRow dr3 = dtnew.NewRow();
                        dr3.BeginEdit();
                        dr3[0] = ""; dr3[1] = ""; dr3[2] = ""; dr3[3] = ""; dr3[4] = ""; dr3[5] = ""; dtnew.Rows.InsertAt(dr3, 2); dtnew.AcceptChanges();
                        DataRow dr4 = dtnew.NewRow();
                        dr4.BeginEdit();
                        dr4[0] = ""; dr4[1] = ""; dr4[2] = ""; dr4[3] = ""; dr4[4] = ""; dr4[5] = ""; dtnew.Rows.InsertAt(dr4, 2); dtnew.AcceptChanges();
                        for (int k = 0; k < Dt.Rows.Count; k++)
                        {
                            DataTable DtID = objDash.SelectLorryDetDateIDWise(date, Convert.ToString(Dt.Rows[k][0].ToString()), ApplicationFunction.ConnectionString());
                            for (int j = 0; j < DtID.Rows.Count; j++)
                            {

                                if (Convert.ToInt32(DtID.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[k]["Lorry_Idno"]) && Convert.ToString(DtID.Rows[j]["Type"]) == "FIT")
                                {
                                    Fit += 1;
                                    dtnew.Rows[k]["FIT"] = "1";
                                }
                                if (Convert.ToInt32(DtID.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[k]["Lorry_Idno"]) && Convert.ToString(DtID.Rows[j]["Type"]) == "INS")
                                {
                                    Ins += 1;
                                    dtnew.Rows[k]["INS"] = "1";
                                }
                                if (Convert.ToInt32(DtID.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[k]["Lorry_Idno"]) && Convert.ToString(DtID.Rows[j]["Type"]) == "RC")
                                {
                                    RC += 1;
                                    dtnew.Rows[k]["RC"] = "1";
                                }
                                if (Convert.ToInt32(DtID.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[k]["Lorry_Idno"]) && Convert.ToString(DtID.Rows[j]["Type"]) == "NAT")
                                {
                                    Nat += 1; dtnew.Rows[k]["NAT"] = "1";
                                }
                                if (Convert.ToInt32(DtID.Rows[j]["ID"]) == Convert.ToInt32(DTemp.Rows[k]["Lorry_Idno"]) && Convert.ToString(DtID.Rows[j]["Type"]) == "AUTH")
                                {
                                    Auth += 1; dtnew.Rows[k]["AUTH"] = "1";
                                }


                            }
                            for (int l = 0; l <= k; l++)
                            {
                                if (Convert.ToString(dtnew.Rows[l]["FIT"]) == "")
                                {
                                    DTemp.Rows[l]["Fitness_Date"] = "";
                                }
                                if (dtnew.Rows[l]["INS"] == "")
                                {
                                    DTemp.Rows[l]["Ins_Valid_Date"] = "";
                                }
                                if (dtnew.Rows[l]["RC"] == "")
                                {
                                    DTemp.Rows[l]["RC_Date"] = "";
                                }
                                if (dtnew.Rows[l]["NAT"] == "")
                                {
                                    DTemp.Rows[l]["Nat_Permit_Date"] = "";
                                }
                                if (dtnew.Rows[l]["AUTH"] == "")
                                {
                                    DTemp.Rows[l]["Auth_Permit_Date"] = "";
                                }
                            }
                            Fit = 0; Ins = 0; RC = 0; Nat = 0; Auth = 0;
                        }
                    }
                    
                   

                    if (DTemp != null && DTemp.Rows.Count > 0)
                    {
                        grdMain.DataSource = DTemp;
                        grdMain.DataBind();
                    }
                }
            }
            
        }
        #endregion

        #region Button Event...

       

        

        #endregion

        #region Bind Event...
     
    

       

        #endregion

        #region Grid Event...
        protected void grdMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                Response.Redirect("LorryMaster.aspx?LorryIdno=" + e.CommandArgument, true);
            }
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }

        #endregion

        #region Excel...
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
              server control at run time. */
        }
        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            //grdMain.GridLines = GridLines.Both;
            //PrepareGridViewForExport(grdMain);
            //ExportGridView();
            CSVTable = (DataTable)ViewState["CSV"];
            CSVTable.Columns.Remove("LorryIdno");
            CSVTable.Columns.Remove("Lorry_Type");
            CSVTable.Columns.Remove("prty_Idno");
            CSVTable.Columns.Remove("Status");
            CSVTable.Columns["LorryType"].SetOrdinal(0);
            CSVTable.Columns["PartyName"].SetOrdinal(1);
            CSVTable.Columns["LorryNo"].SetOrdinal(2);
            CSVTable.Columns["LorryMake"].SetOrdinal(3);
            CSVTable.Columns["ChasisNo"].SetOrdinal(4);
            CSVTable.Columns["EngineNo"].SetOrdinal(5);
            CSVTable.Columns["PanNo"].SetOrdinal(6);
            CSVTable.Columns["OwnerName"].SetOrdinal(7);
            ExportDataTableToCSV(CSVTable, "LorryMasterReport");
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
        #endregion

        private void ExportDataTableToCSV(DataTable dataTable, string CSVFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + CSVFileName + ".csv");
            //Write a row for column names
            foreach (DataColumn dataColumn in dataTable.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
            StringWriter sw = new StringWriter();
            context.Response.Write(Environment.NewLine);
            //Write one row for each DataRow
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int dataColumnCount = 0; dataColumnCount < dataTable.Columns.Count; dataColumnCount++)
                    context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
            Response.End();
        }
    }
}

