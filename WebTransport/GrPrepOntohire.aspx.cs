using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.DAL;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Services;

namespace WebTransport
{
    public partial class GrPrepOntohire : Pagebase
    {

        #region Page Event...
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDateRange();

        }
        #endregion

        #region Button Event............
        protected void lnkbtnUpload_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (FileUpload.HasFile)
            {
                GrPrepOntohireDAL obj = new GrPrepOntohireDAL();
                string excelfilename = string.Empty;

                #region UPLOAD EXCEL AT SERVER
                excelfilename = ApplicationFunction.UploadFileServerControl(FileUpload, "ItemsGronhire", "Grprepexcel");
                #endregion
                if ((System.IO.Path.GetExtension(excelfilename) == ".xls") || (System.IO.Path.GetExtension(excelfilename) == ".xlsx"))
                {
                    DataTable dt = new DataTable();
                    DataTable dtnew = new DataTable();
                    BindDropdownDAL objDal = new BindDropdownDAL();
                    string filepath = Server.MapPath(@"~/ItemsGronhire/" + excelfilename);
                    string constring = string.Empty;
                    if (System.IO.Path.GetExtension(filepath) == ".xls")
                    {
                        constring = "Provider=Microsoft.Jet.OLEDB.4.0;OLE DB Services=-4;Data Source='" + filepath + "';Extended Properties=\"Excel 8.0;HDR=Yes;\"";
                    }
                    else if (System.IO.Path.GetExtension(filepath) == ".xlsx")
                    {
                        constring = "Provider= Microsoft.ACE.OLEDB.12.0;OLE DB Services=-4;Data Source='" + filepath + "'; Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    }
                    #region  Select Excel
                    OleDbConnection con = new OleDbConnection(constring);
                    con.Open();
                    DataTable ExcelTable = new DataTable();
                    ExcelTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string SheetName = Convert.ToString(ExcelTable.Rows[0][2]);
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "] WHERE [GRNo] IS NOT NULL OR [GRDate] IS NOT NULL OR [CHDate] IS NOT NULL OR [CHNo] IS NOT NULL OR [From] IS NOT NULL OR [To] IS NOT NULL OR [LrryNo] IS NOT NULL OR [OwnerName] IS NOT NULL OR [PanNo] IS NOT NULL OR [DspQty] IS NOT NULL OR [Rate] IS NOT NULL OR [Amount] IS NOT NULL", con); DataSet ds = new DataSet();
                    da.Fill(ds);
                    #endregion

                     if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns[0].Caption == "SrNo" && ds.Tables[0].Columns[1].Caption == "GRNo" && ds.Tables[0].Columns[2].Caption == "GRDate" && ds.Tables[0].Columns[3].Caption == "CHDate" && ds.Tables[0].Columns[4].Caption == "CHNo" && ds.Tables[0].Columns[5].Caption == "From" && ds.Tables[0].Columns[6].Caption == "To" && ds.Tables[0].Columns[7].Caption == "LrryNo" && ds.Tables[0].Columns[8].Caption == "OwnerName" && ds.Tables[0].Columns[9].Caption == "PanNo" && ds.Tables[0].Columns[10].Caption == "DspQty" && ds.Tables[0].Columns[11].Caption == "Rate" && ds.Tables[0].Columns[12].Caption == "Amount")
                        {
                            #region INSERT RECORD IN tblChlnUploadFromExcel TABLE
                            Int64 intResult = 0;
                            using (TransactionScope Tran = new TransactionScope(TransactionScopeOption.Required))
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    intResult = obj.InsertInGrExcel(
                                       Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["GRDate"].ToString() == "" ? "" : ds.Tables[0].Rows[i]["GRDate"])),
                                       Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["GRNo"]) == "" ? "0" : ds.Tables[0].Rows[i]["GRNo"]),
                                       Convert.ToDateTime(Convert.ToString(ds.Tables[0].Rows[i]["CHDate"].ToString() == "" ? "" : ds.Tables[0].Rows[i]["CHDate"])),
                                       Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["CHNo"]) == "" ? "0" : ds.Tables[0].Rows[i]["CHNo"]),
                                       Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["From"]) == "" ? "" : ds.Tables[0].Rows[i]["From"]),
                                       Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["To"]) == "" ? "" : ds.Tables[0].Rows[i]["To"]),
                                       Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["LrryNo"]) == "" ? "" : ds.Tables[0].Rows[i]["LrryNo"]),
                                       Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["OwnerName"]) == "" ? "" : ds.Tables[0].Rows[i]["OwnerName"]),
                                       Convert.ToString(Convert.ToString(ds.Tables[0].Rows[i]["PanNo"]) == "" ? "" : ds.Tables[0].Rows[i]["PanNo"]),
                                       Convert.ToInt64(Convert.ToString(ds.Tables[0].Rows[i]["DspQty"]) == "" ? "0" : ds.Tables[0].Rows[i]["DspQty"]),
                                       Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Rate"]) == "" ? "" : ds.Tables[0].Rows[i]["Rate"]),
                                       Convert.ToDouble(Convert.ToString(ds.Tables[0].Rows[i]["Amount"]) == "" ? "" : ds.Tables[0].Rows[i]["Amount"])
                                       , Convert.ToInt64(ddldateRange.SelectedValue == "" ? "0" : ddldateRange.SelectedValue));
                                            
                                }
                                if (intResult > 0)
                                {
                                    msg = "Excel uploaded successfully";
                                    ShowMessage(msg);
                                    Tran.Complete(); 
                                }
                                else {
                                   
                                    Tran.Dispose();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            msg = "Excel is blank or Excel is not in correct format.";
                            ShowMessageErr(msg);
                            return;
                        }
                    }
                    
                }
                else
                {
                    msg = "Please Upload Correct Excel File";
                    ShowMessageErr(msg);
                    return;
                }
            }
        }
        protected void lnkbtnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/vnd.ms-excel";

            Response.AppendHeader("Content-Disposition", "attachment; filename=GrPrepOnToHire.xls");

            Response.TransmitFile(Server.MapPath("~/ItemsGronhire/GrPrepOnToHire.xls"));

            Response.End();

            //string dateFrom = null;
            //GrPrepOntohireDAL obj = new GrPrepOntohireDAL();
            //dateFrom = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //DataTable dttemp = ApplicationFunction.CreateTable("tbl",
            //   "SrNo", "String",
            //   "GRNo", "String",
            //   "GRDate", "String",
            //   "CHDate","String",
            //   "CHNo", "String",
            //   "From", "String",
            //   "To", "String",
            //   "LrryNo", "String",
            //   "OwnerName", "String",
            //   "PanNo", "String",
            //   "DspQty", "String",
            //   "Rate", "String",
            //   "Amount", "String"
            //   );
            //  DataRow dr = dttemp.NewRow();
            //    dr["SrNo"] = Convert.ToString(1);
            //    dr["GRNo"] = Convert.ToString(10055);
            //    dr["GRDate"] = Convert.ToString("01-04-2017");
            //    dr["CHDate"] = Convert.ToString("01-04-2017");
            //    dr["CHNo"] = Convert.ToString(1001);
            //    dr["From"] = Convert.ToString("Jaipur");
            //    dr["To"] = Convert.ToString("Ajmer");
            //    dr["LrryNo"] = Convert.ToString("RJ14SP2903");
            //    dr["OwnerName"] = Convert.ToString("Puneet");
            //    dr["PanNo"] = Convert.ToString("PHPAN123456");
            //    dr["DspQty"] = Convert.ToString("10");
            //    dr["Rate"] = Convert.ToString("5.00") ;
            //    dr["Amount"] = Convert.ToString("50.00"); 
            //    dttemp.Rows.Add(dr);
       
           // ExportExcelHeader(dttemp);
        }
        #endregion


        #region Function....
        private void ExportExcelHeader(DataTable Dt)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GRExcelImportHeaderFormat.xls"));
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
                Response.Flush();
                Response.End();
            }
            catch { }
        }

        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);
        }
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
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

        #endregion
    }
}