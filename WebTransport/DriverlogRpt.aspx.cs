using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.DAL;
using WebTransport.Classes;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Drawing;

namespace WebTransport
{
    public partial class DriverlogRpt : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDateRange();
                ddlDateRange.SelectedValue = Convert.ToString(base.UserDateRng);
                SetDate();
                BindTruckNo();
                txtDateFrom.Attributes.Add("onkeypress", "return notAllowAnything(event);");
                txtDateTo.Attributes.Add("onkeypress", "return notAllowAnything(event);");
            }
        }
        private void BingGrid()
        {

        }
        private void BindDateRange()
        {
            FinYearDAL objFinYearDAL = new FinYearDAL();
            ddlDateRange.DataSource = objFinYearDAL.FillYrwiseDateRange(ApplicationFunction.ConnectionString());
            ddlDateRange.DataTextField = "DateRange";
            ddlDateRange.DataValueField = "Id";
            ddlDateRange.DataBind();
            objFinYearDAL = null;
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
        private void BindTruckNo()
        {
            BindDropdownDAL obj = new BindDropdownDAL();
            var lst = obj.BindTruckNo();
            obj = null;
            if (lst.Count > 0)
            {
                ddlTruck.DataSource = lst;
                ddlTruck.DataTextField = "Lorry_No";
                ddlTruck.DataValueField = "Lorry_Idno";
                ddlTruck.DataBind();

            }
            ddlTruck.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindGrid()
        {
            DriverLogDAL objDAL = new DriverLogDAL();
            DataTable dt = objDAL.SelectReport(ApplicationFunction.ConnectionString(), ApplicationFunction.mmddyyyy(txtDateFrom.Text), ApplicationFunction.mmddyyyy(txtDateTo.Text), Convert.ToInt64(ddlTruck.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                Int64 TripIdno = 0;
                DataTable DtDemo = dt.Clone();
                for (int k = 0; k < DtDemo.Columns.Count; k++)
                {
                    DtDemo.Columns[k].DataType = typeof(string);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        DataRow dr = DtDemo.NewRow();
                        dr["Gr_No"] = dt.Rows[i]["Gr_No"];
                        dr["Gr_Date"] = dt.Rows[i]["Gr_Date"];
                        dr["FromCity"] = dt.Rows[i]["FromCity"];
                        dr["ToCity"] = dt.Rows[i]["ToCity"];
                        dr["Kms"] = dt.Rows[i]["Kms"];
                        dr["Item_Name"] = dt.Rows[i]["Item_Name"];
                        dr["DisPatchQty"] = dt.Rows[i]["DisPatchQty"];
                        dr["Recvd"] = dt.Rows[i]["Recvd"];
                        dr["Shortage"] = dt.Rows[i]["Shortage"];
                        dr["DriverName"] = dt.Rows[i]["DriverName"];
                        DtDemo.Rows.Add(dr);
                        TripIdno = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["TripHead_Idno"])) ? 0 : Convert.ToInt64(dt.Rows[i]["TripHead_Idno"]);
                      
                        //if (Convert.ToInt64(dt.Rows[i + 1]["TripHead_Idno"]) != TripIdno)
                        //{
                            DataSet Ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "exec spDriverLogRpt @Action='SelectOuterRpt',@TripIdno=" + Convert.ToString(dt.Rows[i]["TripHead_Idno"]) + "");
                            if (Ds != null && Ds.Tables.Count > 0)
                            {
                                Int32[] RowCounts = new Int32[3];
                                RowCounts[0] = Ds.Tables[0].Rows.Count;
                                RowCounts[1] = Ds.Tables[1].Rows.Count;
                                RowCounts[2] = Ds.Tables[2].Rows.Count;
                                int Max = RowCounts.Max();
                                for (int c = 0; c < Max; c++)
                                {
                                    DataRow drMax = DtDemo.NewRow();
                                    DtDemo.Rows.Add(drMax);
                                }
                                for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["FromCity"] = Ds.Tables[0].Rows[j]["Expn"];
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["ToCity"] = Convert.ToDouble(Ds.Tables[0].Rows[j]["Amnt"]).ToString("N2");
                                    if (j == 0)
                                    {
                                        DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["ToCity"] = "";
                                    }
                                }
                                for (int j = 0; j < Ds.Tables[1].Rows.Count; j++)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Kms"] = Ds.Tables[1].Rows[j]["Expn"];
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Item_Name"] = Convert.ToDouble(Ds.Tables[1].Rows[j]["Amnt"]).ToString("N2");
                                    if (j == 0)
                                    {
                                        DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Item_Name"] = "";
                                    }
                                }
                                for (int j = 0; j < Ds.Tables[2].Rows.Count; j++)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["DisPatchQty"] = Ds.Tables[2].Rows[j]["Expn"];
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Recvd"] = Convert.ToDouble(Ds.Tables[2].Rows[j]["Amnt"]).ToString("N2");
                                    if (j == 0)
                                    {
                                        DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Recvd"] = "";
                                    }
                                }
                            }
                       // }
                    
                    }
                    else if (Convert.ToInt64(dt.Rows[i]["TripHead_Idno"]) != TripIdno)
                    {
                        DataRow dr = DtDemo.NewRow();
                        dr["Gr_No"] = dt.Rows[i]["Gr_No"];
                        dr["Gr_Date"] = dt.Rows[i]["Gr_Date"];
                        dr["FromCity"] = dt.Rows[i]["FromCity"];
                        dr["ToCity"] = dt.Rows[i]["ToCity"];
                        dr["Kms"] = dt.Rows[i]["Kms"];
                        dr["Item_Name"] = dt.Rows[i]["Item_Name"];
                        dr["DisPatchQty"] = dt.Rows[i]["DisPatchQty"];
                        dr["Recvd"] = dt.Rows[i]["Recvd"];
                        dr["Shortage"] = dt.Rows[i]["Shortage"];
                        dr["DriverName"] = dt.Rows[i]["DriverName"];
                        DtDemo.Rows.Add(dr);
                        TripIdno = Convert.ToInt64(dt.Rows[i]["TripHead_Idno"]);

                        DataSet Ds = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "exec spDriverLogRpt @Action='SelectOuterRpt',@TripIdno=" + Convert.ToString(dt.Rows[i]["TripHead_Idno"]) + "");
                        if (Ds != null && Ds.Tables.Count > 0)
                        {
                            Int32[] RowCounts = new Int32[3];
                            RowCounts[0] = Ds.Tables[0].Rows.Count;
                            RowCounts[1] = Ds.Tables[1].Rows.Count;
                            RowCounts[2] = Ds.Tables[2].Rows.Count;
                            int Max = RowCounts.Max();
                            for (int c = 0; c < Max; c++)
                            {
                                DataRow drMax = DtDemo.NewRow();
                                DtDemo.Rows.Add(drMax);
                            }
                            for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
                            {
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["FromCity"] = Ds.Tables[0].Rows[j]["Expn"];
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["ToCity"] = Convert.ToDouble(Ds.Tables[0].Rows[j]["Amnt"]).ToString("N2");
                                if (j == 0)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["ToCity"] = "";
                                }
                            }
                            for (int j = 0; j < Ds.Tables[1].Rows.Count; j++)
                            {
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Kms"] = Ds.Tables[1].Rows[j]["Expn"];
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Item_Name"] = Convert.ToDouble(Ds.Tables[1].Rows[j]["Amnt"]).ToString("N2");
                                if (j == 0)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Item_Name"] = "";
                                }
                            }
                            for (int j = 0; j < Ds.Tables[2].Rows.Count; j++)
                            {
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["DisPatchQty"] = Ds.Tables[2].Rows[j]["Expn"];
                                DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Recvd"] = Convert.ToDouble(Ds.Tables[2].Rows[j]["Amnt"]).ToString("N2");
                                if (j == 0)
                                {
                                    DtDemo.Rows[(DtDemo.Rows.Count - Max) + j]["Recvd"] = "";
                                }
                            }
                        }
                    }
                }
                DtDemo.AcceptChanges();
                grdMain.DataSource = DtDemo;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s) : " + Convert.ToString(dt.Rows.Count);
            }
            else
            {
                grdMain.DataSource = null;
                grdMain.DataBind();
                lblTotalRecord.Text = "T. Record(s) : 0";
            }
        }
        protected void lnkbtnPreview_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate();
            ddlDateRange.Focus();
        }

        protected void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FromCity")) == "Other Expenses")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#BDDBED");
                    e.Row.Font.Bold = true;

                }
                else if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FromCity")) == "Total")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#BDDBED");
                    e.Row.Font.Bold = true;

                }
            }
        }
    }
}