<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCapitalLogistic.aspx.cs" Inherits="WebTransport.PrintCapitalLogistic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style>
    *
    {
        font-family:Arial;
    }
    .table
    {
        
    }
    
   
    span
    {
       
    }
    </style>

<%--       <style>
 #lblTotAmnt2{top: 65%; left: 23%; width: 16%; padding: 0 10px; text-align: center;}
    #lblOrderNo{top: 32%; left: 41%; width: 16%; padding: 0 10px; text-align: center;}
    #lblGrDate{top: 32%; left: 60%; width: 16%; padding: 0 10px; text-align: center;}
    #lblToCity{top: 37%; left: 68%; width: 25%; padding: 0 10px; text-align: center;}
    #lblConsigneeName{top: 44%; left: 41%; width: 52%; padding: 0 10px; text-align: center;}
    #lblAdd1{top: 48%; left: 41%; width: 52%; padding: 0 10px; text-align: center;}
    #lblAdd2{top: 52%; left: 41%; width: 52%; padding: 0 10px; text-align: center;}
    #lblAdd3{top: 57%; left: 41%; width: 52%; padding: 0 10px; text-align: center;}
    #lblGSTINNo{top: 61%; left: 50%; width: 42%; padding: 0 10px; text-align: center;}
    #lblInvNo{top: 70%; left: 60%; width: 16%; padding: 0 10px; text-align: center;}
    #lblMTQty{top: 83%; left: 60%; width: 16%; padding: 0 10px; text-align: center;}
    #lblVehNo{top: 72%; left: 78%; width: 16%; padding: 0 10px; text-align: center;}
    #lblBags{top: 83%; left: 78%; width: 16%; padding: 0 10px; text-align: center;}
    *
    {
        font-family:Arial;
    }
    .table
    {
        position: absolute;
        background-image: url('CapitalLogisticPrint.jpg');
        background-size: 100%;
        width: 1000px;
        height: 740px;
        -webkit-transform: rotate(90deg);
        -moz-transform: rotate(90deg);
        -o-transform: rotate(90deg);
        -ms-transform: rotate(90deg);
        transform: rotate(90deg);
    }
    
    #lblTotAmnt2{top:  calc(481px + 50px); left:  calc(224px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblOrderNo{top:  calc(240px + 50px); left:  calc(416px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblToCity{top:  calc(275px + 50px); left:  calc(675px + 150px); width: 255px; padding: 0 10px; text-align: center;}
    #lblGrDate{top:  calc(240px + 50px); left:  calc(603px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblConsigneeName{top:  calc(327px + 50px); left:  calc(416px + 150px); width: 520px; padding: 0 10px; text-align: center;}
    #lblAdd1{top:  calc(354px + 50px); left:  calc(416px + 150px); width: 520px; padding: 0 10px; text-align: center;}
    #lblAdd2{top:  calc(380px + 50px); left:  calc(416px + 150px); width: 520px; padding: 0 10px; text-align: center;}
    #lblAdd3{top:  calc(420px + 50px); left:  calc(416px + 150px); width: 520px; padding: 0 10px; text-align: center;}
    #lblGSTINNo{top:  calc(450px + 50px); left:  calc(496px + 150px); width: 437px; padding: 0 10px; text-align: center;}
    #lblInvNo{top:  calc(516px + 50px); left:  calc(602px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblMTQty{top:  calc(613px + 50px); left:  calc(602px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblVehNo{top:  calc(534px + 50px); left:  calc(780px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    #lblBags{top:  calc(613px + 50px); left:  calc(780px + 150px); width: 160px; padding: 0 10px; text-align: center;}
    span
    {
        font-weight:bold;
        display:block;
        position:absolute;
    }
    </style>--%>
</head>
<body style="width:100%;display: block;margin: auto;">
    <div class="table" style="border: 1px solid;margin: 10px;">
        <div class="table">
        <!--HEADER ROW-->
        <div style="width:100%;display: inline-block;border-bottom:1px solid;height:150px;">
            <span style="width:33.33%;float:left;font-size:13px;">&nbsp;Email address: capitallogistics@gmail.com</span>
            <span style="width:33.33%;float:left;font-size:13px;text-align:center;">All subject to Jaipur Jurisdiction</span>
            <span style="width:33.33%;float:left;font-size:13px;text-align:right;font-weight:bold">GSTIN No: 08AAMFC0779Q1ZN&nbsp;</span>
            <!--LOGO AND HEADER TEXT-->
            <div style="width:100%">
                <div style="width:20%;float:left">
                    <img src="images/UltraTech_Logo-with_tag.jpg" style="height:60px;padding:20px 10px;-webkit-filter: grayscale(100%); /* Safari 6.0 - 9.0 */filter: grayscale(100%);"/>
                    <div>&nbsp;SAP Code: 2111840</div>
                </div>
                <div style="width:60%;float:left;text-align:center">
                    <h1 style="font-size: 40px;letter-spacing: 5px;margin: 0;font-family:initial;text-transform:uppercase;">Capital Logistic</h1>
                    <div style="font-size:14px;"><b><u>Head Office:</u></b>516, Jaipur Center, Tonk Road, Jaipur (Raj.) 302018</div>
                    <div style="font-size:14px;"><b><u>Local Address:</u></b>Gordhanpura Chowki, Main Road, Kotputali Jaipur (Raj.) 302018</div>
                    <div style="font-size:14px;">Whatever tax is payable under Reserve Charges Mechanism Yes (&nbsp;✔&nbsp;) No ( &nbsp;&nbsp;)</div>
                </div>
                <div style="width:20%;float:left">
                    <span style="display:inline-block;width:100%;font-size:13px;text-align:right;">Mob: 9887545400&nbsp;</span>
                    <span style="display:inline-block;width:100%;font-size:13px;text-align:right;">9829778983&nbsp;</span>
                </div>
            </div>
        </div>
        <!--ROW 2-->
        <div style="display:flex;width:100%;">
            <!--DETAIL COLUMN 1-->
            <div style="width:49.9%;float:left;border-right:1px solid;">
                <div style="border-bottom:1px solid;height:80px;">
                    <span style="display:inline-block;width:100%;font-size:15px;text-align:center;"><b>Declaration</b></span>
                    <span style="display:inline-block;width:100%;font-size:13px;padding-left:3px;">We hereby declare that input Tax Credit of Capital Goods and input Services, used for providing transportation servies, has not been taken by us.</span>
                </div>
                <div style="border-bottom:1px solid;">
                    <span style="display:inline-block;width:100%;font-size:15px;padding:5px;">Person Liable to Pay GST: 08AAACL6442L1ZA</span>
                </div>
                <div style="border-bottom:1px solid;padding: 10px 5px;height:165px;">
                    <span style="display:inline-block;width:100%;font-size:18px;"><b>ULTRATECH CEMENT LIMITED</b></span><br />
                    <span style="display:inline-block;width:100%;">(UNIT : KOTPUTALI CEMENT WORKS)</span><br />
                    <span style="display:inline-block;width:100%;">Village- Mohampura, Tehsil- Kotputali</span><br />
                    <span style="display:inline-block;width:100%;">Distt- Jaipur (Raj.) 303108</span><br />
                </div>
                <div style="min-height:60px;">
                    <div style="width:49.6%;float:left;border-bottom:1px solid;border-right:1px solid;"><span style="display:inline-block;width:100%;padding: 0 5px"><b>Freight Details</b></span></div>
                    <div style="width:50%;float:left;border-bottom:1px solid;"><span style="display:inline-block;width:100%;padding: 0 5px;"><b>Invoice value</b></span></div>
                    <div style="width:49.6%;float:left;border-bottom:1px solid;min-height:30px;border-right:1px solid;"><span style="display:inline-block;width:100%;padding: 0 5px;">TO BE BILLED</span></div>
                    <div style="width:50%;float:left;border-bottom:1px solid;min-height:30px;"><span style="display:inline-block;width:100%;padding: 0 5px;">RS. 
                    <!--Bill Amount-->
                    <asp:Label visible="true" ID="lblTotAmnt2" runat="server" ClientIDMode="Static"></asp:Label>
                    </span></div>
                </div>
                <div style="min-height:150px;">
                    <span style="display:inline-block;width:100%;padding: 0 5px;">Acknowledgement Consignee with Signature & Stamp</span>
                </div>
            </div>
            <!--DETAIL COLUMN 2-->
            <div style="width:50%;float:left;">
                <div style="border-bottom:1px solid;display:inline-block;width:100%;">
                    <div style="width:34%;float:left;border-right:1px solid;min-height:80px;">
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center;">Party/STO Order No.</span>
                        <span style="display:inline-block;width:100%;text-align:center;">&nbsp;<asp:Label visible="true" ID="lblOrderNo" runat="server" ClientIDMode="Static"></asp:Label></span>
                    </div>
                    <div style="width:34%;float:left;border-right:1px solid;min-height:80px;">
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center;">Date</span><span style=""></span>
                        <span style="display:inline-block;width:100%;text-align:center;">&nbsp;<asp:Label visible="true" ID="lblGrDate" runat="server" ClientIDMode="Static"></asp:Label></span>
                    </div>
                    <div style="width:12%;float:left;border-right:1px solid;min-height:80px;">
                        <span style="display:inline-block;width:100%;text-align:center;">L.R. No.</span>
                        <span style="display:inline-block;width:100%;text-align:center;">&nbsp;</span>
                    </div>
                    <div style="width:19%;float:left;min-height:80px;">
                        <span style="display:inline-block;width:100%;">&nbsp;<asp:Label visible="true" ID="lblGRNo" runat="server" ClientIDMode="Static"></asp:Label></span>
                        <span style="display:inline-block;width:100%;">&nbsp;</span>
                    </div>
                </div>
                <div style="display:inline-block;width:100%;border-bottom:1px solid;min-height:29px;">
                    <div style="width:50%;float:left;border-right:1px solid;"><span style="display:inline-block;width:100%;padding: 5px;">From: <b> <asp:Label visible="true" ID="lblFromCity" runat="server"></asp:Label></b></span></div>
                    <div style="width:49%;float:left;"><span style="display:inline-block;width:100%;padding: 5px;">To: <b> <asp:Label visible="true" ID="lblToCity" runat="server"></asp:Label></b></span></div>
                </div>
                <div>
                    <div style="display:inline-block;width:100%;min-height: 184px;"><span style="display:inline-block;width:100%;text-align:center"><b>Consignee</b></span>
                        <asp:Label style="display:inline-block;width:100%;font-size:13px;text-align:center; text-align:center;" visible="true" ID="lblConsigneeName" runat="server" ClientIDMode="Static"></asp:Label> 
                        <asp:Label style="display:inline-block;width:100%;font-size:13px;text-align:center;" visible="true" ID="lblAdd1" runat="server" ClientIDMode="Static"></asp:Label>
                        <asp:Label style="display:inline-block;width:100%;font-size:13px;text-align:center;" visible="true" ID="lblAdd2" runat="server" ClientIDMode="Static"></asp:Label>
                        <asp:Label style="display:inline-block;width:100%;font-size:13px;text-align:center;" visible="true" ID="lblAdd3" runat="server" ClientIDMode="Static"></asp:Label>
                        <asp:Label style="display:inline-block;width:100%;font-size:13px;text-align:center;" visible="true" ID="lblGSTINNo" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div style="display:inline-block;width:100%;"></div>
                    <div ></div>
                    <div style="display:inline-block;width:100%;"></div>
                    <div style="border-bottom:1px solid;display:inline-block;width:100%;"></div>
                </div>
                <div style="display:flex;border-bottom:1px solid;">
                    <div style="width:33.33%;float:left;border-right:1px solid;">
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;padding: 0 5px;">SAP Delivery No.</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;padding: 0 5px;">Invoive No: </span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;">&nbsp;</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center">PRODUCT</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center">&nbsp;CEMENT/CLINKER</span>
                        <span style="display:inline-block;width:100%;text-align:center">&nbsp;GRADE : 43/53/PPC</span>
                    </div>
                    <div style="width:33.33%;float:left;border-right:1px solid;">
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;">&nbsp;</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;">&nbsp;<asp:Label visible="true" ID="lblInvNo" runat="server" ClientIDMode="Static"></asp:Label></span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;">&nbsp;</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center">QUANTITY (M.T.)</span>
                        <span style="display:inline-block;width:100%;"><asp:Label style="display:inline-block;width:100%;min-height: 30px;  text-align:center;" visible="true" ID="lblMTQty" runat="server" ClientIDMode="Static"></asp:Label></span>
                    </div>
                    <div style="width:33.33%;float:left;">
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center">VECHLE No.</span>
                        <span style="display:inline-block;width:100%;"><asp:Label style="display:inline-block;width:100%;min-height: 30px;  text-align:center;" visible="true" ID="lblVehNo" runat="server" ClientIDMode="Static"></asp:Label></span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;">&nbsp;</span>
                        <span style="display:inline-block;width:100%;border-bottom:1px solid;text-align:center">BAGS </span>
                        <span style="display:inline-block;width:100%;"> <asp:Label style="display:inline-block;width:100%;min-height: 30px;  text-align:center;" visible="true" ID="lblBags" runat="server" ClientIDMode="Static"></asp:Label></span>
                    </div>
                </div>
                <div style="text-align:right;position: relative;top: 55px;">For: <b>Capital Logistic</b></div>
            </div>
        </div>
   </div>
   </div>
</body></html>
