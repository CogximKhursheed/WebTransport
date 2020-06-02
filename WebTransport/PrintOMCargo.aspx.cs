using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTransport.Classes;
using WebTransport.DAL;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Transactions;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace WebTransport
{
    public partial class PrintOMCargo1 : System.Web.UI.Page
    {
        #region Variable ...
        static FinYearA UFinYear = new FinYearA();
        DataTable dtTemp = new DataTable(); DataTable AcntDS = new DataTable(); DataTable DsTrAcnt = new DataTable();
        DataTable dtTable = new DataTable(); bool IsWeight = false; Double iRate = 0.00;
        double dblTtAmnt = 0; static bool UserPrefGradeVal;
        int rb = 0; Int32 iGrAgainst = 0; Int64 RcptGoodHeadIdno = 0; Int64 AdvOrderGR_Idno = 0;
        private int intFormId = 27; Int32 comp_Id;
        string strSQL = ""; double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;// bool isTBBRate = false;dtotlAmnt="";
        double dSurchgPer = 0; double ItemWtAmnt = 0;
        double dCFT = 0;
        double dSurgValue = 0, dSurgTmp = 0, t = 0;
        Double iqty = 0; Double temp = 0, dServTaxPer = 0, dSwacchBhrtTaxPer = 0, dtotalAmount = 0;
        double totalIqty = 0; double itotWeght = 0; double dtotAmnt = 0, dtotrate = 0, dServTaxValid = 0, dSwacchBhrtTaxValid = 0, dKalyanTax = 0, iQtyShrtgRate = 0, iQtyShrtgLimit = 0, iWghtShrtgLimit = 0, iWghtShrtgRate = 0;
        int chkbit = 0;
        double grAmnt = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PrintGRPrep(Convert.ToInt64(Request.QueryString["q"]));
                hidPages.Value = Convert.ToString(Request.QueryString["P"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);
            }
        }

        #region Functions
        private void ShowMessageErr(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessageError('" + msg + "')", true);
        }
        private void ShowMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "PassMessage('" + msg + "')", true);

        }
        #endregion

        #region Print

        private void PrintGRPrep(Int64 GRHeadIdno)
        {
            visiFalse.Visible = false;
            Repeater obj = new Repeater();

            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
            hidRenamePF.Value = Convert.ToString(hiduserpref.PFLabel_GR);
            hidRenameToll.Value = Convert.ToString(hiduserpref.TollTaxLabel_GR);
            HidsRenCartage.Value = Convert.ToString(hiduserpref.CartageLabel_GR);
            HidRenCommission.Value = Convert.ToString(hiduserpref.CommissionLabel_Gr);
            HidRenBilty.Value = Convert.ToString(hiduserpref.BiltyLabel_GR);
            //if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
            //{
            //    imgLogoShow.Visible = true;
            //    byte[] img = hiduserpref.Logo_Image;
            //    string base64String = Convert.ToBase64String(img, 0, img.Length);
            //    imgLogoShow.ImageUrl = "data:image/png;base64," + base64String;
            //}
            //else
            //{
            //    imgLogoShow.Visible = false;
            //    imgLogoShow.ImageUrl = "";
            //}
            //if (Convert.ToString(hiduserpref.Terms) == "" && Convert.ToString(hiduserpref.Terms1) == "")
            //{
            //    lblTerms.Visible = false;
            //    lblterms1.Visible = false;

            //}
            //else
            //{
            //    lblTerms.Visible = true;
            //    lblterms1.Visible = true;

            //    lblTerms.Text = "'" + hiduserpref.Terms + "'";
            //    lblterms1.Text = hiduserpref.Terms1;
            //}
            if (Convert.ToString(HidsRenWages.Value) != "")
            {
                lblUnloadingPrint.Text = Convert.ToString(HidsRenWages.Value);
                labHam.Text = Convert.ToString(HidsRenWages.Value);
            }
            else
            {
                lblUnloadingPrint.Text = "Wages";
                labHam.Text = "Wages";
            }
            if (Convert.ToString(hidRenamePF.Value) != "")
            {
                lblCollChargePrint.Text = Convert.ToString(hidRenamePF.Value);
                labCollCha.Text = Convert.ToString(hidRenamePF.Value);
            }
            else
            {
                lblCollChargePrint.Text = "PF";
                labCollCha.Text = "PF";
            }
            if (Convert.ToString(hidRenameToll.Value) != "")
            {
                lblDelChargesPrint.Text = Convert.ToString(hidRenameToll.Value);
                labDelCha.Text = Convert.ToString(hidRenameToll.Value);
            }
            else
            {
                lblDelChargesPrint.Text = "Toll Tax";
                labDelCha.Text = "Toll Tax";
            }
            if (Convert.ToString(HidsRenCartage.Value) != "")
            {
                lblFOVPrint.Text = Convert.ToString(HidsRenCartage.Value);
                labFOV.Text = Convert.ToString(HidsRenCartage.Value);
            }
            else
            {
                lblFOVPrint.Text = "Cartage";
                labFOV.Text = "Cartage";
            }
            if (Convert.ToString(HidRenCommission.Value) != "")
            {
                lblOctroiPrint.Text = Convert.ToString(HidRenCommission.Value);
                labOctroi.Text = Convert.ToString(HidRenCommission.Value);
            }
            else
            {
                lblOctroiPrint.Text = "Commission";
                labOctroi.Text = "Commission";
            }
            if (Convert.ToString(HidRenBilty.Value) != "")
            {
                lblDemuChargesPrint.Text = Convert.ToString(HidRenBilty.Value);
                labDemCha.Text = Convert.ToString(HidRenBilty.Value);
            }
            else
            {
                labDemCha.Text = "Bilty";
                lblDemuChargesPrint.Text = "Bilty";
            }
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = "", CompDesc = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = ""; string RegNo = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            CompDesc = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            RegNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Reg_No"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;
            lblOWNER.Text = "Subject to " + Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]) + " Jurisdiction only";
            if (CompDesc == "")
            {
                lblCompDesc.Visible = false;
            }
            else
            {
                lblCompDesc.Text = Convert.ToString(CompDesc.Trim().Replace("@", "<br/>") + "<br/>");
            }
            if (FaxNo == "")
            {
                lblCompFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = "FAX No.:" + FaxNo;
                lblCompFaxNo.Visible = true;
            }
            if (RegNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = RegNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (PanNo == "")
            {
                lblPanNo.Visible = false; lbltxtPanNo.Visible = false;
            }
            else
            {
                lblPanNo.Text = PanNo;
                lblPanNo.Visible = true; lbltxtPanNo.Visible = true;
            }
            #endregion

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrintOM',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                lblValueViaCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblSubtotalP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SubTotal_Amnt"]);
                lblUnitP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Unit"]);
                lblDetailP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Detail"]);
                lblRefNoP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"]);
                //lblRefDateP.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Ref_Date"]).ToString("dd-MM-yyyy");
                lblTotP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"]));
                lblQtP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Qty"]);
                lblNoPckg.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Qty"]);
                //lblDimP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Dimension"]);
                //lblChWght.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ch_Weight"]);
                // lblPCFT.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CFT"]);
                //lblTCFT.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CFT"]);
                //lblActWght.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Act_Weight"]);
                lblRateRs.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Item_Rate"]));
                labFreightRs.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"]));
                labSurR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcharge_Amnt"]));
                labHamR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"]));
                labFOVR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartage_Amnt"]));
                labCollChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]));
                labDelChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]));
                labOctroiR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Commission_Amnt"]));
                labDemChaR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"]));
                labTotP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SubTotal_Amnt"]));
                labSerTaxR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                labGTotR.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Net_Amount"]));
                lblRemP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);
                //lblLSTP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lst_No"]);
                //lblCSTP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cst_No"]);
                // lblLSTP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lst_No1"]);
                //lblCSTP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cst_No1"]);
                lblMNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Manual_No"]);
                lblPhP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CNorMobile"]);
                lblPhP1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CNeeMobile"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]) == "")
                {
                    lbltxtagent.Visible = false; lblAgent.Visible = false;
                }
                else
                {
                    lblAgent.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]); lbltxtagent.Visible = true; lblAgent.Visible = true;
                }


                lblConsignorName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsignorAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"])) == true)
                {
                    Label5.Visible = false; lblConsigneeTin.Visible = false;
                }
                else
                {
                    lblConsigneeTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"]);
                }
                lblConsigeeName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsigneeAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"])) == true)
                { lblPrtyTinTxt.Visible = false; lblConsignorTin.Visible = false; }
                else
                { lblConsignorTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"]); }

                lblLorryNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true)
                {
                    lblOrderNo.Visible = false; lblOrderNoVal.Visible = false;
                }
                else { lblOrderNo.Visible = true; lblOrderNoVal.Visible = true; lblOrderNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true)
                {
                    lblFormNo.Visible = false; lblFormNoVal.Visible = false;
                }
                else { lblFormNo.Visible = true; lblFormNoVal.Visible = true; lblFormNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Form_No"].ToString(); }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                {
                    chkGoods.Checked = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "2")
                {
                    chkCNor.Checked = true;
                }
                else
                {
                    chkCNee.Checked = true;
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRRet_Typ"]) == "1")
                {
                    chkPaid.Checked = true;
                    lblCheP.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Inst_No"]);
                    lblInsDate.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Inst_Date"]);
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRRet_Typ"]) == "2")
                {
                    chkToBeld.Checked = true;
                }
                else
                {
                    chkToPay.Checked = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Container_No1"])) == true)
                {
                    tr1.Visible = false;
                }
                else
                {
                    tr1.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["EGPNo"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true)
                {
                    tr2.Visible = false;
                }
                else
                {
                    tr2.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"])) == true)
                {
                    tr3.Visible = false;
                }
                else
                {
                    tr3.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == true || string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"])) == true)
                {
                    tr4.Visible = false;
                }
                else
                {
                    tr4.Visible = true;
                }
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"])) == true)
                {
                    lblNameShipmentno.Visible = false; lblShipmentNo.Visible = false;
                }
                else { lblNameShipmentno.Visible = true; lblShipmentNo.Visible = true; lblShipmentNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Shipmnt_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Container_No1"])) == true)
                {
                    lblNameContnrNo.Visible = false; lblContainerNo.Visible = false;
                }
                else { lblNameContnrNo.Visible = true; lblContainerNo.Visible = true; lblContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Container_No1"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"])) == true)
                {
                    lblNameCntnrSize.Visible = false; lblContainerSize.Visible = false;
                }
                else
                { lblNameCntnrSize.Visible = true; lblContainerSize.Visible = true; lblContainerSize.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ConsigName"])) == true)
                {
                    lblConsName.Visible = false; lblvalConsName.Visible = false;
                }
                else
                { lblConsName.Visible = true; lblvalConsName.Visible = true; lblvalConsName.Text = dsReport.Tables["GRPrint"].Rows[0]["ConsigName"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["EGPNo"])) == true)
                {
                    lblEGPNo.Visible = false; lblEGPNoval.Visible = false;
                }
                else
                { lblEGPNo.Visible = true; lblEGPNoval.Visible = true; lblEGPNoval.Text = dsReport.Tables["GRPrint"].Rows[0]["EGPNo"].ToString(); }

                //if (Convert.ToBoolean(dsReport.Tables["GRPrint"].Rows[0]["MODVAT_CPY"]) == true)
                //{ lblModvatcpy.Text = "Yes"; }
                //else
                //{ lblModvatcpy.Text = "No"; }

                //Ref No.
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true)
                {
                    lblRefNo.Visible = false; lblrefnoval.Visible = false;
                }
                else
                { lblRefNo.Visible = true; lblrefnoval.Visible = true; lblrefnoval.Text = dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString(); }
                //
                //lblTranTypeP.Text = dsReport.Tables["GRPrint"].Rows[0]["Tran_Type"].ToString();
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"])) == "0")
                {
                    lblTotItem.Visible = false; lblTotItemValue.Visible = false;
                }
                else
                {
                    lblTotItem.Visible = true; lblTotItemValue.Visible = true; lblTotItemValue.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Price"]).ToString("N2");
                }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true)
                {
                    lblNameContnrType.Visible = false; lblCntnrType.Visible = false;
                }
                else
                { lblNameContnrType.Visible = true; lblCntnrType.Visible = true; lblCntnrType.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"])) == true)
                {
                    lblNameSealNo.Visible = false; lblSealNo.Visible = false;
                }
                else { lblNameSealNo.Visible = true; lblSealNo.Visible = true; lblSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Seal_No1"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true)
                {
                    lblDinNoText.Visible = false; lblDinNo.Visible = false;
                }
                else { lblDinNoText.Visible = true; lblDinNo.Visible = true; lblDinNo.Text = dsReport.Tables["GRPrint"].Rows[0]["DI_NO"].ToString(); }

                //.........................
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"])) == true)
                { trRemarks.Visible = false; }
                else
                { lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]); }
                if (Convert.ToString(hiduserpref.Terms_Con_Retailer) != "")
                { lblTnCGR.Text = Convert.ToString(hiduserpref.Terms_Con_Retailer); }
                else { trTnC.Visible = false; }
                lblOctroi.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Commission_Amnt"]));
                if (lblOctroi.Text != "")
                {
                    dcmsn = Convert.ToDouble(lblOctroi.Text);
                }
                else
                {
                    dcmsn = 0;
                }
                lblDemurrage.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"]));
                if (lblDemurrage.Text != "")
                {
                    dblty = Convert.ToDouble(lblDemurrage.Text);
                }
                else
                {
                    dblty = 0;
                }
                lblFOV.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartage_Amnt"]));
                if (lblFOV.Text != "")
                {
                    dcrtge = Convert.ToDouble(lblFOV.Text);
                }
                else
                {
                    dcrtge = 0;
                }
                lblSurchargeP.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcharge_Amnt"]));
                if (lblSurchargeP.Text != "")
                {
                    dsuchge = Convert.ToDouble(lblSurchargeP.Text);
                }
                else
                {
                    dsuchge = 0;
                }
                //if (Convert.ToString(HidsRenWages.Value) != "")
                //{
                //    lblwages.Text = Convert.ToString(HidsRenWages.Value);
                //}
                //else
                //{
                //    lblwages.Text = "Wages";
                //}
                //if (Convert.ToString(hidRenamePF.Value) != "")
                //{
                //    lblPFAmnt.Text = Convert.ToString(hidRenamePF.Value);
                //}
                //else
                //{
                //    lblPFAmnt.Text = "PF";
                //}
                //if (Convert.ToString(hidRenameToll.Value) != "")
                //{
                //    lblTollTax.Text = Convert.ToString(hidRenameToll.Value);
                //}
                //else
                //{
                //    lblTollTax.Text = "Toll Tax";
                //}
                lblUnloading.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"]));
                if (lblUnloading.Text != "")
                {
                    dwges = Convert.ToDouble(lblUnloading.Text);
                }
                else
                {
                    dwges = 0;
                }
                lblCollectionCharges.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]));
                if (lblCollectionCharges.Text != "")
                {
                    dPF = Convert.ToDouble(lblCollectionCharges.Text);
                }
                else
                {
                    dPF = 0;
                }
                //if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                //{

                //    valuelblservtaxConsigner.Text = "0.00";
                //}
                //else
                //{
                //    valuelblservtaxConsigner.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                //    valuelblservceTax.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //}
                lblDeliveryCharges.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]));
                lblSerTaxCharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                if (lblDeliveryCharges.Text != "")
                {
                    dtoll = Convert.ToDouble(lblDeliveryCharges.Text);
                }
                else
                {
                    dtoll = 0;
                }


                //if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])) == true)
                //{
                //    lblGrossAmnt.Visible = false; valuelblGrossAmnt.Visible = false;
                //}
                //else { lblGrossAmnt.Visible = true; valuelblGrossAmnt.Visible = true; valuelblGrossAmnt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Gross_Amnt"])); }

                //if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])) == true)
                //{
                //    valuelblTotal.Visible = false;
                //}
                //else { valuelblTotal.Visible = true; valuelblTotal.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Total_Amnt"])); }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["StaxPaid_ByIdno"]) == "1")
                {
                    lblSerTaxCharge.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["ServTax_Amnt"]));
                    if (lblSerTaxCharge.Text != "")
                    {
                        dtax = Convert.ToDouble(lblSerTaxCharge.Text);
                    }
                    else
                    {
                        dtax = 0;
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SwachhBhrtTax_Amnt"])) == true)
                    {
                        lblSwchBhrt.Visible = false; lblSwchBhrt.Visible = false;
                    }
                    else { lblSwchBhrt.Visible = true; lblSwchBhrt.Visible = true; lblSwchBhrt.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SwachhBhrtTax_Amnt"])); }

                    if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["KrishiKalyan_Amnt"])) == true)
                    {
                        lblKrishi.Visible = false; lblKrishi.Visible = false;
                    }
                    else { lblKrishi.Visible = true; lblKrishi.Visible = true; lblKrishi.Text = string.Format("{0:0,0.00}", Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["KrishiKalyan_Amnt"])); }
                }
                //else
                //{
                //    lblSwchBhrt.Visible = true; lblSwchBhrt.Visible = true; lblSwchBhrt.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //    lblKrishi.Visible = true; lblKrishi.Visible = true; lblKrishi.Text = string.Format("{0:0,0.00}", Convert.ToDouble("0"));
                //}

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();
                lblNetAmntP.Text = string.Format("{0:0,0.00}", (dcmsn + dblty + dcrtge + dPF + dsuchge + dtax + dwges + dtoll + dtotlAmnt));
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlGenericControl HideGrhdr = (HtmlGenericControl)e.Item.FindControl("HideGrhdr");
                if (chkbit == 1) { HideGrhdr.Visible = false; Table2.Visible = false; } else { HideGrhdr.Visible = true; Table2.Visible = true; }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl HideGritem = (HtmlGenericControl)e.Item.FindControl("HideGritem");
                if (chkbit == 1) { HideGritem.Visible = false; } else { HideGritem.Visible = true; }

                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                //dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Ch_Weight"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
                //dCFT += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "CFT"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlGenericControl hidfooterdetl = (HtmlGenericControl)e.Item.FindControl("hidfooterdetl");
                //Label lblFTtotalWeight = (Label)e.Item.FindControl("lblFTtotalWeight");
                Label lblFTTotalAmnt = (Label)e.Item.FindControl("lblFTTotalAmnt");
                Label lblFTQty = (Label)e.Item.FindControl("lblFTQty");
                // Label lblCFT = (Label)e.Item.FindControl("lblTotalCFT");

                lblFTTotalAmnt.Text = dtotlAmnt.ToString("N2");
                //lblFTtotalWeight.Text = dtotwght.ToString("N2");
                //lblCFT.Text = dCFT.ToString("N2");
                lblFTQty.Text = dqtnty.ToString();

                if (chkbit == 1)
                {
                    hidfooterdetl.Visible = false;
                    //lstInfoDiv.Visible = false; 
                }
                else if (chkbit == 2)
                {
                    hidfooterdetl.Visible = true;
                    //lstInfoDiv.Visible = false; 
                }
            }
        }
        #endregion       
    }
}