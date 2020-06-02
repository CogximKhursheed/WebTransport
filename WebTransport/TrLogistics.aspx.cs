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
    public partial class TrLogistics : System.Web.UI.Page
    {
        double dtotlAmnt = 0, dqtnty = 0, dtotwght = 0, damot = 0;
        int chkbit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            hidHeadIdno.Value = Request.QueryString["q"];
            PrintGRPrep(Convert.ToInt64(Request.QueryString["q"]));
            hidPages.Value = Convert.ToString(Request.QueryString["P"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "CallPrint('print')", true);


        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlGenericControl HideGrhdr = (HtmlGenericControl)e.Item.FindControl("HideGrhdr");
                if (chkbit == 1) { HideGrhdr.Visible = false; } else { HideGrhdr.Visible = true; }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl HideGritem = (HtmlGenericControl)e.Item.FindControl("HideGritem");
                if (chkbit == 1) { HideGritem.Visible = false; } else { HideGritem.Visible = true; }

                dtotlAmnt += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"));
                dtotwght += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Tot_Weght"));
                dqtnty += Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Qty"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlGenericControl hidfooterdetl = (HtmlGenericControl)e.Item.FindControl("hidfooterdetl");
                Label lblFTtotalWeight = (Label)e.Item.FindControl("lblFTtotalWeight");
                Label lblFTTotalAmnt = (Label)e.Item.FindControl("lblFTTotalAmnt");
                Label lblFTQty = (Label)e.Item.FindControl("lblFTQty");

                lblFTTotalAmnt.Text = dtotlAmnt.ToString("N2");
                lblFTtotalWeight.Text = dtotwght.ToString("N3");
                lblFTQty.Text = dqtnty.ToString();

                if (chkbit == 1)
                {
                    hidfooterdetl.Visible = false; lstInfoDiv.Visible = false;
                    lst.Visible = false;
                }
                else if (chkbit == 2)
                {
                    hidfooterdetl.Visible = true; lstInfoDiv.Visible = false;
                    lst.Visible = false;
                }
            }
        }
        public void PrintGRPrep(Int64 intGRIdno)
        {
            Repeater obj = new Repeater();

            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            // HidsRenWages.Value = Convert.ToString(hiduserpref.WagesLabel_Print);
            if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
            {
                imgLogoShow.Visible = true;
                byte[] img = hiduserpref.Logo_Image;
                string base64String = Convert.ToBase64String(img, 0, img.Length);
                imgLogoShow.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                imgLogoShow.Visible = false;
                imgLogoShow.ImageUrl = "";
            }
            if (Convert.ToString(hiduserpref.Terms) == "" && Convert.ToString(hiduserpref.Terms1) == "")
            {
                lblTerms.Visible = false;
                lblterms1.Visible = false;

            }
            else
            {
                lblTerms.Visible = true;
                lblterms1.Visible = true;

                lblTerms.Text = "'" + hiduserpref.Terms + "'";
                lblterms1.Text = hiduserpref.Terms1;
            }
            double dcmsn = 0, dblty = 0, dcrtge = 0, dsuchge = 0, dwges = 0, dPF = 0, dtax = 0, dtoll = 0, dqty = 0, damnt = 0, dweight = 0;
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string Through = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            int iPrintOption, PrintRcptAmnt = 0; string strTermCond = ""; Int32 PriPrinted = 0; Int32 ReportTyp = 0; int compID = 0; string numbertoent = "";
            string CompGSTIN = ""; string Email = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            string CompDesc = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompDescription"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]) + "," + Convert.ToString(CompDetl.Tables[0].Rows[0]["Mobile_1"]);// Hold till get the allownumeric function //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            Email = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["Email_ID_1"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["Email_ID_1"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            //TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            CompGSTIN = string.IsNullOrEmpty(Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"])) ? "" : Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblCompanyname.Text = CompName; lblCompname.Text = "For - " + CompName;
            lblCompAdd1.Text = Add1;
            lblCompAdd2.Text = Add2;
            lblCompCity.Text = City;
            lblCompState.Text = State;
            lblCompPhNo.Text = PhNo;

            if (Email == "")
            {
                lblEmail.Visible = false;
            }
            else
            {
                lblEmail.Text = "Email :" + Email;
            }


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
                lblCompFaxNo.Visible = false; lblFaxNo.Visible = false;
            }
            else
            {
                lblCompFaxNo.Text = FaxNo;
                lblCompFaxNo.Visible = true; lblFaxNo.Visible = true;
            }
            if (ServTaxNo == "")
            {
                lblCompTIN.Visible = false; lblTin.Visible = false;
            }
            else
            {
                lblCompTIN.Text = ServTaxNo;
                lblCompTIN.Visible = true; lblTin.Visible = true;
            }
            if (CompGSTIN == "")
            {
                lblCompGST.Visible = false; lblCompGSTIN.Visible = false;

            }
            else
            {
                lblCompGSTIN.Text = CompGSTIN;
                lblCompGST.Visible = true; lblCompGSTIN.Visible = true;
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

            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='PrintTrLogisctics',@Id='" + hidHeadIdno.Value + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport.Tables[1].Rows.Count > 0)
            {
                dsReport.Tables[1].TableName = "UserPref";
            }
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                lblGRno.Text = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PrefixGr_No"])) ? Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]) : Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PrefixGr_No"]) + "-" + Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblDelvryPlace.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Delivery_Place"]);
                lblValueViaCity.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]) == "")
                {
                    lbltxtagent.Visible = false; lblAgent.Visible = false; trAgent.Visible = false;
                }
                else
                {
                    lblAgent.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Agent"]); lbltxtagent.Visible = true; lblAgent.Visible = true; trAgent.Visible = true;
                }





                // by lokesh For consignor and consignee portion add 
                lblConsigeeName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsigneeTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Tin"]);

                lblConsignorName.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblConsignorTin.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver Tin"]);
                lblPrtyGSTIN.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);
                lblConsignerGSTINValue.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);
                lblLorryNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                lbladvamnt.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Adv_Amnt"]).ToString("N2");
                valuelblDieselAmnt.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Diesel_Amnt"]).ToString("N2");
                valuelblBc.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["BC"]).ToString("N2");

                // end lokesh code
                // lblRefNo
                // For shipment details & container details by lokesh
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cartg_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Cartg_Amnt"])) == "0")
                {
                    lblCartage.Visible = false; lblCart.Visible = false;
                    cart.Visible = false;
                }
                else
                {
                    cart.Visible = true;
                    lblCart.Visible = true; lblCartage.Visible = true;
                    lblCartage.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Cartg_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Surcrg_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Surcrg_Amnt"])) == "0")
                {
                    lblSurchar.Visible = false; lblSurcharg.Visible = false;
                    Surcharg.Visible = false;
                }
                else
                {
                    Surcharg.Visible = true;
                    lblSurchar.Visible = true; lblCartage.Visible = true;
                    lblSurcharg.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["Surcrg_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"])) == "0")
                {
                    lblComis.Visible = false; lblComiss.Visible = false;
                    Comis.Visible = false;
                }
                else
                {
                    Comis.Visible = true;
                    lblComis.Visible = true; lblComiss.Visible = true;
                    lblComiss.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Bilty_Amnt"])) == "0")
                {
                    lblbilte.Visible = false; lblBilt.Visible = false;
                    Bilty.Visible = false;
                }
                else
                {
                    lblbilte.Visible = true; lblBilt.Visible = true;
                    Bilty.Visible = true;
                    lblBilt.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Wages_Amnt"])) == "0")
                {
                    lblUnloading.Visible = false; lblunchrg.Visible = false;
                    Unloding.Visible = false;
                }
                else
                {
                    lblUnloading.Visible = true; lblunchrg.Visible = true;
                    Unloding.Visible = true;
                    lblunchrg.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["AgntComisn_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"])) == "0")
                {
                    lblcollchrg.Visible = false; lblcolchrg.Visible = false;
                    colChrg.Visible = false;
                }
                else
                {
                    colChrg.Visible = true;
                    lblcollchrg.Visible = true; lblcolchrg.Visible = true;
                    lblcolchrg.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["PF_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"])) == "0")
                {
                    lblDelChrg.Visible = false; lbldlvchrg.Visible = false;
                    Delchrg.Visible = false;
                }
                else
                {
                    lblDelChrg.Visible = true; lbldlvchrg.Visible = true;
                    Delchrg.Visible = true;
                    lbldlvchrg.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TollTax_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SubTot_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SubTot_Amnt"])) == "0")
                {
                    lblSubTot.Visible = false; lblSubTotal.Visible = false;
                    SubTot.Visible = false;
                }
                else
                {
                    lblSubTot.Visible = true; lblSubTotal.Visible = true;
                    SubTot.Visible = true;
                    lblSubTotal.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SubTot_Amnt"]).ToString("N2");
                }
                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["RndOff_Amnt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["RndOff_Amnt"])) == "0")
                {

                    lblround.Visible = false; lblRoundAmnt.Visible = false;
                    lblRoundAmnt.Text = "0";
                }
                else
                {
                    lblround.Visible = true; lblRoundAmnt.Visible = true;
                    lblRoundAmnt.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["RndOff_Amnt"]).ToString("N2");
                }


                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"])) == true)
                {
                    lblOrderNo.Visible = false; lblOrderNoVal.Visible = false; trOrderNo.Visible = false;
                }
                else { lblOrderNo.Visible = true; lblOrderNoVal.Visible = true; trOrderNo.Visible = true; lblOrderNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Ordr_No"].ToString(); }


                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Form_No"])) == true)
                {
                    lblFormNo.Visible = false; lblFormNoVal.Visible = false; trFormNo.Visible = false;
                }
                else { lblFormNo.Visible = true; lblFormNoVal.Visible = true; trFormNo.Visible = true; lblFormNoVal.Text = dsReport.Tables["GRPrint"].Rows[0]["Form_No"].ToString(); }


                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"])) == true)
                {
                    lblNameShipmentno.Visible = false; lblShipmentNo.Visible = false; trShipNo.Visible = false;
                }
                else { lblNameShipmentno.Visible = true; lblShipmentNo.Visible = true; trShipNo.Visible = true; lblShipmentNo.Text = dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"])) == true)
                {
                    lblNameContnrNo.Visible = false; lblContainerNo.Visible = false; trContainerNo.Visible = false;
                }
                else { lblNameContnrNo.Visible = true; lblContainerNo.Visible = true; trContainerNo.Visible = true; lblContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"])) == true)
                {
                    lblNameCntnrSize.Visible = false; lblContainerSize.Visible = false; trsize.Visible = false;
                }
                else
                { lblNameCntnrSize.Visible = true; lblContainerSize.Visible = true; trsize.Visible = true; lblContainerSize.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Size"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ConsigName"])) == true)
                {
                    lblConsName.Visible = false; lblvalConsName.Visible = false;
                }
                else
                { lblConsName.Visible = true; lblvalConsName.Visible = true; lblvalConsName.Text = dsReport.Tables["GRPrint"].Rows[0]["ConsigName"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["EGPNo"])) == true)
                {
                    lblEGPNo.Visible = false; lblEGPNoval.Visible = false; trEGPNo.Visible = false;
                }
                else
                { lblEGPNo.Visible = true; lblEGPNoval.Visible = true; trEGPNo.Visible = true; lblEGPNoval.Text = dsReport.Tables["GRPrint"].Rows[0]["EGPNo"].ToString(); }

                //Ref No.
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"])) == true)
                {
                    lblRefNo.Visible = false; lblrefnoval.Visible = false; trRefNo.Visible = false;
                }
                else
                {
                    if ((dsReport != null) && (dsReport.Tables.Count > 0) && (dsReport.Tables[1].Rows.Count > 0))
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["UserPref"].Rows[0]["Reflabel_Gr"])) == true)
                        {
                            lblRefNo.Text = "Ref No.";
                        }
                        else
                        {
                            lblRefNo.Text = dsReport.Tables["UserPref"].Rows[0]["Reflabel_Gr"].ToString();
                        }
                    }

                    trRefNo.Visible = true;
                    lblRefNo.Visible = true; lblrefnoval.Visible = true; lblrefnoval.Text = dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString();
                }
                //


                if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"])) == "0")
                {
                    lblTotItem.Visible = false; lblTotItemValue.Visible = false; trTotItem.Visible = false;
                }
                else
                {
                    lblTotItem.Visible = true; lblTotItemValue.Visible = true; trTotItem.Visible = true; lblTotItemValue.Text = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["TotItem_Value"]).ToString("N2");
                }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"])) == true)
                {
                    lblNameContnrType.Visible = false; lblCntnrType.Visible = false; trCntrType.Visible = false;
                }
                else
                { lblNameContnrType.Visible = true; lblCntnrType.Visible = true; trCntrType.Visible = true; lblCntnrType.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_Type"].ToString(); }

                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"])) == true)
                {
                    lblNameSealNo.Visible = false; lblSealNo.Visible = false; trsealno.Visible = false;
                }
                else { lblNameSealNo.Visible = true; lblSealNo.Visible = true; trsealno.Visible = true; lblSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString(); }
                //------------------------- ADD BY PEEYUSH
                if (string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"])) == true)
                {
                    lblDinNoText.Visible = false; lblDinNo.Visible = false; trDinNo.Visible = false;
                }
                else { lblDinNoText.Visible = true; lblDinNo.Visible = true; trDinNo.Visible = true; lblDinNo.Text = dsReport.Tables["GRPrint"].Rows[0]["DI_NO"].ToString(); }

                //.........................


                if (((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"])) == "0.00") && ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CGST_Amt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CGST_Amt"])) == "0.00"))
                {
                    SGST.Visible = false; CGST.Visible = false;
                    lblSGST.Visible = false; lblCGST.Visible = false;
                    lblSGSTAmnt.Visible = false; lblCGSTAmnt.Visible = false;
                    lblSGSTAmnt.Text = "0";
                    lblCGSTAmnt.Text = "0";
                }
                else if ((string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"])) == true) || (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"])) == "0")
                {
                    SGST.Visible = false; CGST.Visible = false;
                    lblSGST.Visible = false; lblCGST.Visible = false;
                    lblSGSTAmnt.Visible = false; lblCGSTAmnt.Visible = false;
                    lblSGSTAmnt.Text = "0";
                    lblCGSTAmnt.Text = "0";

                }
                else
                {
                    lblSGSTAmnt.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SGST_Amt"]);
                    lblCGSTAmnt.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["CGST_Amt"]);
                }


                double frTotal = Convert.ToDouble(dsReport.Tables["GRPrint"].Rows[0]["SubTot_Amnt"]);
                lblremark.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Remark"]);
                lblTypeOfGr.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRType"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GSTFlag"]) == "GSTYes")
                {
                    trTINConsignee.Visible = false;
                    trTINConsigner.Visible = false;
                }

                Repeater1.DataSource = dsReport;
                Repeater1.DataBind();

                lblNetAmount.Text = string.Format("{0:0,0.00}", ((frTotal + Convert.ToDouble(lblSGSTAmnt.Text) + Convert.ToDouble(lblCGSTAmnt.Text)) - (Convert.ToDouble(lbladvamnt.Text) + Convert.ToDouble(valuelblDieselAmnt.Text) + Convert.ToDouble(valuelblBc.Text) + Convert.ToDouble(lblRoundAmnt.Text))));
                //lblNetAmount.Text = string.Format("{0:0,0.00}", ((dtotlAmnt + Convert.ToDouble(lblSGSTAmnt.Text) + Convert.ToDouble(lblCGSTAmnt.Text)) - (Convert.ToDouble(lbladvamnt.Text) + Convert.ToDouble(valuelblDieselAmnt.Text) + Convert.ToDouble(valuelblBc.Text) + Convert.ToDouble(lblRoundAmnt.Text))));
            }
        }
    }
}

