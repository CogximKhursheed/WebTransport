using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebTransport.Classes;
using Microsoft.ApplicationBlocks.Data;
using WebTransport.DAL;

namespace WebTransport
{
    public partial class JainGrPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //PrintGRJainBulk(Convert.ToInt32(1525));
            PrintGRJainBulk(Convert.ToInt64(Request.QueryString["q"].ToString()));
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Divopen();", true);
            
            hidPages.Value= Convert.ToString(Request.QueryString["P"].ToString());
            if (Convert.ToInt32(hidPages.Value) == 1)
            {
                divho.Visible = true;

            }
            else if (Convert.ToInt32(hidPages.Value)==4)
            {
                divho.Visible = true;
                DivDriver.Visible = true;
                Divconsigor.Visible = true;
                DivCosignee.Visible = true;
            }
        }
        private void PrintGRJainBulk(Int64 GRHeadIdno)
        {
            GRPrepDAL obj1 = new GRPrepDAL();
            tblUserPref hiduserpref = obj1.selectuserpref();
            string CompName = ""; string Add1 = "", Add2 = ""; string PhNo = ""; string City = ""; string State = ""; string PanNo; string TinNo = ""; string ServTaxNo = ""; string email = ""; string FaxNo = ""; string Remark = ""; string DelNoteRef = "";
            DataSet CompDetl = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "select * from tblcompmast");
            # region Company Details
            CompName = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            Add1 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress1"]);
            Add2 = Convert.ToString(CompDetl.Tables[0].Rows[0]["Adress2"]);
            email = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Mail"]);
            //PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"] + "," + CompDetl.Tables[0].Rows[0]["Phone_Res"]);
            PhNo = "Phone No. (O) :" + Convert.ToString(CompDetl.Tables[0].Rows[0]["Phone_Off"]);
            City = Convert.ToString(CompDetl.Tables[0].Rows[0]["City_Idno"]);
            State = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]) == "" ? Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) : Convert.ToString(CompDetl.Tables[0].Rows[0]["State_Idno"]) + " - " + Convert.ToString(CompDetl.Tables[0].Rows[0]["Pin_No"]);
            TinNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["TIN_NO"]);
            ServTaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["ServTaxNo"]);
            FaxNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Fax_No"]);
            PanNo = Convert.ToString(CompDetl.Tables[0].Rows[0]["Pan_No"]);
            lblauth.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblauthj.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["Comp_Name"]);
            lblMainCopyCompGSTIN.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblDriverCopyCompGSTIN.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblConsignerCopyCompGSTIN.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            lblConsigneeCopyCompGSTIN.Text = Convert.ToString(CompDetl.Tables[0].Rows[0]["CompGSTIN_No"]);
            if (lblConsigneeCopyCompGSTIN.Text == "") lblConsigneeCopyCompGSTIN.Visible = false;
            lblCompanyname1.Text = CompName; //lblCompname1.Text = "For - " + CompName;
            lblCompanyname1j.Text = CompName; //lblCompname1.Text = "For - " + CompName;
            lblCompanyname1c.Text = CompName; //lblCompname1.Text = "For - " + CompName;
            lblCompanyname1d.Text = CompName; //lblCompname1.Text = "For - " + CompName;

            lblCompAdd3.Text = Add1;
            lblCompAdd3j.Text = Add1;

            lblCompAdd3c.Text = Add1;
            lblCompAdd3d.Text = Add1;

            lblCompAdd4.Text = Add2;
            lblCompAdd4j.Text = Add2;

            lblCompAdd4c.Text = Add2;
            lblCompAdd4d.Text = Add2;

            lblCompCity1.Text = City;
            lblCompCity1j.Text = City;

            lblCompCity1c.Text = City;
            lblCompCity1d.Text = City;

            lblCompState1.Text = State;
            lblCompState1j.Text = State;

            lblCompState1c.Text = State;
            lblCompState1d.Text = State;

            //lbltinno.Text = TinNo;
            //lbltinnoj.Text = TinNo;

            //lbltinnoc.Text = TinNo;
            //lbltinnod.Text = TinNo;

            lblCompTIN1.Text = ServTaxNo;
            lblCompTIN1j.Text = ServTaxNo;

            lblCompTIN1c.Text = ServTaxNo;
            lblCompTIN1d.Text = ServTaxNo;

            lblPanNo1.Text = PanNo;
            lblPanNo1j.Text = PanNo;


            lblPanNo1c.Text = PanNo;
            lblPanNo1d.Text = PanNo;

            lblJurCity.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            lblJurCityj.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
          
            lblJurCityc.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
            lblJurCityd.Text = "All Subject To " + City.ToUpper() + " Jurisdiction";
          
          
            #endregion
            
            DataSet dsReport = SqlHelper.ExecuteDataset(ApplicationFunction.ConnectionString(), CommandType.Text, "EXEC [spGRPrep] @ACTION='SelectPrint',@Id='" + GRHeadIdno + "'");
            dsReport.Tables[0].TableName = "GRPrint";
            if (dsReport != null && dsReport.Tables[0].Rows.Count > 0)
            {
                if (hiduserpref.Logo_Req == true && hiduserpref.Logo_Image != null)
                {
                    ImgLogoJain.Visible = true;
                    byte[] img = hiduserpref.Logo_Image;
                    string base64String = Convert.ToBase64String(img, 0, img.Length);
                    ImgLogoJain.ImageUrl = "data:image/png;base64," + base64String;
                    ImgLogoJainj.ImageUrl = "data:image/png;base64," + base64String;
                    ImgLogoJainc.ImageUrl = "data:image/png;base64," + base64String;
                    ImgLogoJaind.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    ImgLogoJain.Visible = false;
                    ImgLogoJainj.Visible = false;
                    ImgLogoJainc.Visible = false;
                    ImgLogoJaind.Visible = false;

                    ImgLogoJain.ImageUrl = "";
                    ImgLogoJainj.ImageUrl = "";
                    ImgLogoJainc.ImageUrl = "";
                    ImgLogoJaind.ImageUrl = "";
                }
                AmountType1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AmountType"]);
                AmountType2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AmountType"]);
                AmountType3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AmountType"]);
                AmountType4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["AmountType"]);
                lblGRno1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate1.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lbldeliveryat.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblJainVia.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblConsigeeName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsignorName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblLorryNo1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                lblJainContainerNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString(); 
                lblJainSealNo.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString();
                 lbltinno1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TinNo"]);
                lblRefNo1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString());
                lblInvNoValue1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"]);
                lblShipmentNo1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"]);
                lblDetails1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Detail"]);
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_NO2"]) == "" && Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_SEALNO2"]) == "")
                {
                    lblContainerNo2.Visible = false;
                    lblSealNo2.Visible = false;
                    Container.Visible = false;
                    seal2.Visible = false;
                    Container3.Visible = false;
                    lblJainContainerNo2.Visible = false;
                    seal.Visible = false;
                    lblJainSealNo2.Visible = false;
                    container5.Visible = false;
                    lblContainerNoc1.Visible = false;
                    seal3.Visible = false;
                    lblJainSealNoc1.Visible = false;
                    Container4.Visible = false;
                    lblContainerNod1.Visible = false;
                    Seal4.Visible = false;
                    lblJainSealNod1.Visible = false;
                }
                else
                {
                    lblContainerNo2.Visible = true;
                    lblSealNo2.Visible = true;
                    Container.Visible = true;
                    seal2.Visible = true;
                    Container3.Visible = true;
                    lblJainContainerNo2.Visible = true;
                    seal.Visible = true;
                    lblJainSealNo2.Visible = true;
                    container5.Visible = true;
                    lblContainerNoc1.Visible = true;
                    seal3.Visible = true;
                    lblJainSealNoc1.Visible = true;
                    Container4.Visible = true;
                    lblContainerNod1.Visible = true;
                    Seal4.Visible = true;
                    lblJainSealNod1.Visible = true;
                    lblContainerNo2.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_NO2"].ToString();
                    lblSealNo2.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_SEALNO2"].ToString();
                    lblJainContainerNo2.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_NO2"].ToString();
                    lblJainSealNo2.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_SEALNO2"].ToString();
                    lblContainerNoc1.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_NO2"].ToString();
                    lblJainSealNoc1.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_SEALNO2"].ToString();
                    lblContainerNod1.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_NO2"].ToString();
                    lblJainSealNod1.Text = dsReport.Tables["GRPrint"].Rows[0]["GRCONTANR_SEALNO2"].ToString();
                }
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "CHA")
                {
                    lblCha1.Text = "CHA";
                    lblChaName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor1.Visible = false;
                    lblForName1.Visible = false;
                    lblCha1.Visible = true;
                    lblChaName1.Visible = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "Forwarder")
                {
                    lblFor1.Text = "Forwarder";
                    lblForName1.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor1.Visible = true;
                    lblForName1.Visible = true;
                    lblCha1.Visible = false;
                    lblChaName1.Visible = false;
                }
                else
                {

                    lblFor1.Visible = false;
                    lblForName1.Visible = false;
                    lblCha1.Visible = false;
                    lblChaName1.Visible = false;
                }
                lblGrtype1.Visible = true;
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "2")
                    lblGrtype1.Text = "TO BE BILLED";
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "3")
                    lblGrtype1.Text = "TO PAY";
                else
                    lblGrtype1.Visible = false;

                    /// FOR  

                    lblGRno1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate1j.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lbldeliveryatj.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblJainViaj.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblConsigeeName1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsignorName1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblLorryNo1j.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                lblJainContainerNoj.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString();
                lblJainSealNoj.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString();
                lbltinno2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TinNo"]);
                lblRefNo2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString());
                lblInvNoValue2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"]);
                lblShipmentNo2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"]);
                lblDetails2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Detail"]);
                lblMainConsigGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);
                lblConsignerConsigGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);
                lblConsigneeConsigGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);
                lblDriverConsigGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ReceiverGSTIN"]);

                lblMainConsigneeGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);
                lblConsignerConsigneeGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);
                lblConsigneeConsigneeGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);
                lblDriverConsigneeGSTINNo.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["SenderGSTIN"]);

                if (lblMainConsigGSTINNo.Text == "") {lblMainConsigGSTINNo.Visible = false; MainConsigGSTINNo.Visible = false;}
                if (lblConsignerConsigGSTINNo.Text == "") {lblConsignerConsigGSTINNo.Visible = false; ConsignerConsigGSTINNo.Visible = false;}
                if (lblConsigneeConsigGSTINNo.Text == "") {lblConsigneeConsigGSTINNo.Visible = false; ConsigneeConsigGSTINNo.Visible = false;}
                if (lblDriverConsigGSTINNo.Text == "") {lblDriverConsigGSTINNo.Visible = false; DriverConsigGSTINNo.Visible = false;}
                if (lblMainConsigneeGSTINNo.Text == "") {lblMainConsigneeGSTINNo.Visible = false; MainConsigneeGSTINNo.Visible = false;}
                if (lblConsignerConsigneeGSTINNo.Text == "") {lblConsignerConsigneeGSTINNo.Visible = false; ConsignerConsigneeGSTINNo.Visible = false;}
                if (lblConsigneeConsigneeGSTINNo.Text == "") {lblConsigneeConsigneeGSTINNo.Visible = false; ConsigneeConsigneeGSTINNo.Visible = false;}
                if (lblDriverConsigneeGSTINNo.Text == "") { lblDriverConsigneeGSTINNo.Visible = false; DriverConsigneeGSTINNo.Visible = false; }

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "CHA")
                {
                    lblCha2.Text = "CHA";
                    lblChaName2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor2.Visible = false;
                    lblForName2.Visible = false;
                    lblCha2.Visible = true;
                    lblChaName2.Visible = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "Forwarder")
                {
                    lblFor2.Text = "Forwarder";
                    lblForName2.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor2.Visible = true;
                    lblForName2.Visible = true;
                    lblCha2.Visible = false;
                    lblChaName2.Visible = false;
                }
                else
                {

                    lblFor2.Visible = false;
                    lblForName2.Visible = false;
                    lblCha2.Visible = false;
                    lblChaName2.Visible = false;
                }
                lblGrtype2.Visible = true;
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "2")
                    lblGrtype2.Text = "TO BE BILLED";
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "3")
                    lblGrtype2.Text = "TO PAY";
                else
                    lblGrtype2.Visible = false;

                
                ////C
                lblGRno1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate1c.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lbldeliveryatj.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblJainViac.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblConsigeeName1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsignorName1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblLorryNo1c.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                lblJainContainerNoc.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString();
                lblJainSealNoc.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString();
                lbltinno3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TinNo"]);
                lblRefNo3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString());
                lblInvNoValue3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"]);
                lblShipmentNo3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"]);
                lblDetails3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Detail"]);

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "CHA")
                {
                    lblCha3.Text = "CHA";
                    lblChaName3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor3.Visible = false;
                    lblForName3.Visible = false;
                    lblCha3.Visible = true;
                    lblChaName3.Visible = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "Forwarder")
                {
                    lblFor3.Text = "Forwarder";
                    lblForName3.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor3.Visible = true;
                    lblForName3.Visible = true;
                    lblCha3.Visible = false;
                    lblChaName3.Visible = false;
                }
                else
                {

                    lblFor3.Visible = false;
                    lblForName3.Visible = false;
                    lblCha3.Visible = false;
                    lblChaName3.Visible = false;
                }
                lblGrtype3.Visible = true;
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "2")
                    lblGrtype3.Text = "TO BE BILLED";
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "3")
                    lblGrtype3.Text = "TO PAY";
                else
                    lblGrtype3.Visible = false;


               //D
                lblGRno1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_No"]);
                lblGrDate1d.Text = Convert.ToDateTime(dsReport.Tables["GRPrint"].Rows[0]["Gr_Date"]).ToString("dd-MM-yyyy");
                lblFromCity1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["From_City"]);
                lblToCity1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lbldeliveryatd.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["To_City"]);
                lblJainViad.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Via_City"]);
                lblConsigeeName1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender"]);
                lblConsigneeAddress1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Sender Address"]);
                lblConsignorName1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Receiver"]);
                lblConsignorAddress1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Recriver Address"]);
                lblLorryNo1d.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Lorry No"].ToString());
                lblJainContainerNod.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_No"].ToString();
                lblJainSealNod.Text = dsReport.Tables["GRPrint"].Rows[0]["GRContanr_SealNo"].ToString();
                lbltinno4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["TinNo"]);
                lblRefNo4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Ref_No"].ToString());
                lblInvNoValue4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["DI_NO"]);
                lblShipmentNo4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Shipment_No"]);
                lblDetails4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Detail"]);

                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "CHA")
                {
                    lblCha4.Text = "CHA";
                    lblChaName4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor4.Visible = false;
                    lblForName4.Visible = false;
                    lblCha4.Visible = true;
                    lblChaName4.Visible = true;
                }
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ExpImpTyp"]) == "Forwarder")
                {
                    lblFor4.Text = "Forwarder";
                    lblForName4.Text = Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["ChaFrwdr_Name"]);

                    lblFor4.Visible = true;
                    lblForName4.Visible = true;
                    lblCha4.Visible = false;
                    lblChaName4.Visible = false;
                }
                else
                {

                    lblFor4.Visible = false;
                    lblForName4.Visible = false;
                    lblCha4.Visible = false;
                    lblChaName4.Visible = false;
                }
                lblGrtype4.Visible = true;
                if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "2")
                    lblGrtype4.Text = "TO BE BILLED";
                else if (Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["Gr_Typ"]) == "3")
                    lblGrtype4.Text = "TO PAY";
                else
                    lblGrtype4.Visible = false;



               Int32 inttaxpaidby = string.IsNullOrEmpty(Convert.ToString(dsReport.Tables["GRPrint"].Rows[0]["STax_Typ"])) ? 0 : Convert.ToInt32(dsReport.Tables["GRPrint"].Rows[0]["STax_Typ"]);
               if (inttaxpaidby == 1)
               {
                   lbltaxpaidby.Text = "Transporter";
                   lbltaxpaidbyj.Text = "Transporter";
                   lbltaxpaidbyc.Text = "Transporter";
                   lbltaxpaidbyd.Text = "Transporter";
               }
               else if (inttaxpaidby == 2)
               {
                   lbltaxpaidby.Text = "Consigner";
                   lbltaxpaidbyj.Text = "Consigner";
                   lbltaxpaidbyc.Text = "Consigner";
                   lbltaxpaidbyd.Text = "Consigner";
               }
               else
               {
                   lbltaxpaidby.Text = "Consignee";
                   lbltaxpaidbyj.Text = "Consignee";
                   lbltaxpaidbyc.Text = "Consignee";
                   lbltaxpaidbyd.Text = "Consignee";
               }

               ///hjhgjghj
            }
        }
    }
}