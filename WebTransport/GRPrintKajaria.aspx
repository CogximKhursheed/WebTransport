<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GRPrintKajaria.aspx.cs"
    Inherits="WebTransport.GRPrintKajaria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    @media print {
        html, body {
            width: 8.85in; /* was 8.5in */
            height: 5.3in; /* was 5.5in */
            display: block;
            font-family: "Calibri";
            /*font-size: auto; NOT A VALID PROPERTY */
        }

        @page {
            size: 8.85in 5.3in /* . Random dot? */;
        }
    }

    body *
        {
            font-size:14px;
            letter-spacing:3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; right: 113px; top: 2.8cm;">
        <div style="position: absolute; top: 0.3cm; right: 10cm; width: 400px; height: 14px;">
            &nbsp;<asp:Label ID="lblConsignorName" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 1.2cm; right: 10cm; width: 400px; height: 14px;">
            &nbsp;<asp:Label ID="lblConsignorGSTIN" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 1.9cm; right: 10cm; width: 400px; height: 14px;">
            &nbsp;<asp:Label ID="lblConsigneeName" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 2.3cm; right: 10cm; width: 400px; height: 14px;">
            &nbsp;<asp:Label ID="lblConsigneeGSTIN" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 0.3cm; right: -1.5cm; width: 160px; height: 14px;">
            &nbsp;<asp:Label ID="lblGRNo" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 1.2cm; right: -1.5cm; width: 160px; height: 14px;">
            &nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 1.96cm; right: -1.5cm; width: 160px; height: 14px;">
            &nbsp;<asp:Label ID="lblFromCity" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 2.3cm; right: -1.5cm; width: 160px; height: 14px;">
            &nbsp;<asp:Label ID="lblToCity" runat="server"></asp:Label>
        </div>
        <div style="position: absolute; top: 4cm; right: -3cm; width: 985px; border: 1px solid transparent;">
            <%--ITEM GRID --%>
            <div style="height: 55px;position: relative;">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div>
                            <span style="border-right: 1px solid transparent; width: 99px; display: block; float: left;position: relative;left: -40px;">
                                <%#Eval("Qty") %></span> <span style="border-right: 1px solid transparent; width: 481px;position: relative;left: -60px;
                                    display: block; float: left;">
                                    <%#Eval("Item_Modl") %></span> <span style="text-align: center; border-right: 1px solid transparent;
                                        width: 99px; display: block; float: left;position: relative;left: -80px;">
                                        <%#Eval("Act_Weight") %></span> <span id="Span8" style="text-align: center; border-right: 1px solid transparent;
                                            width: 100px; display: block; float: left;position: relative;left: -65px;">
                                            <%#Eval("Item_Rate") %></span> <span id="Span9" style="text-align: center; border-right: 1px solid transparent;
                                                width: 100px; display: block; float: left;position: relative;left: -20px;">
                                                <%#Eval("Amount") %></span> <span style="text-align: center; border-right: 1px solid transparent;
                                                    width: 100px; display: block; float: left;">-</span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <%--OTHER AMOUNT--%>
            <div>
                &nbsp;</div>
            <!--SUM-->
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span10" style="border-right: 1px solid transparent; width: 99px; display: block;float: left;">&nbsp;</span> 
                <span id="Span11" style="border-right: 1px solid transparent;width: 481px; display: block; float: left;">&nbsp;</span>
                <span id="Span12" style="text-align: center;border-right: 1px solid transparent; width: 99px; display: block; float: left;">&nbsp;</span> 
                <span id="Span13" style="text-align: center; border-right: 1px solid transparent;width: 100px; display: block; float: left;">&nbsp;</span> 
                <span id="Span14" style="text-align: center;border-right: 1px solid transparent; width: 100px; display: block; float: left;">&nbsp;<asp:Label ID="lblFreight" runat="server"></asp:Label></span>
                <span id="Span15" style="text-align: center; border-right: 1px solid transparent;width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span16" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span17" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span18" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span19" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span20" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblSurcharge" runat="server"></asp:Label></span>
                <span id="Span21" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span35" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span36" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span37" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span38" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span39" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblLabour" runat="server"></asp:Label></span>
                <span id="Span40" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span41" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span42" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span43" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span44" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span45" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblPickUp" runat="server"></asp:Label></span>
                <span id="Span46" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span47" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span48" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;position: relative;left: -60px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblRemark" style="height: 35px;word-wrap: break-word;display: block;line-height: 13px;width: 317px;overflow: hidden;" runat="server"></asp:Label></span> <span id="Span49" style="text-align: center;
                                border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                                &nbsp;</span> <span id="Span50" style="text-align: center; border-right: 1px solid transparent;
                                    width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span51" style="text-align: center;
                                        border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                        &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblLocalFreight" runat="server"></asp:Label></span>
                <span id="Span52" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span53" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span54" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span55" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span56" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span57" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblBilty" runat="server"></asp:Label></span>
                <span id="Span58" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span59" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span60" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span61" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span62" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span63" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblDoorDel" runat="server"></asp:Label></span>
                <span id="Span64" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span65" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span66" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;position: relative;top: 19px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblDelvryPlace" runat="server"></asp:Label>
                        </span> <span id="Span67" style="text-align: center; border-right: 1px solid transparent;
                            width: 99px; display: block; float: left;">&nbsp;</span> <span id="Span68" style="text-align: center;
                                border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                &nbsp;</span> <span id="Span69" style="text-align: center; border-right: 1px solid transparent;
                                    width: 100px; display: block; float: left;">&nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblTotal" runat="server"></asp:Label></span>
                <span id="Span70" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div>
                <span id="Span71" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span72" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span73" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span74" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span75" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblGST" runat="server"></asp:Label></span>
                <span id="Span76" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span77" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span78" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;</span> <span id="Span79" style="text-align: center;
                            border-right: 1px solid transparent; width: 99px; display: block; float: left;">
                            &nbsp;</span> <span id="Span80" style="text-align: center; border-right: 1px solid transparent;
                                width: 100px; display: block; float: left;">&nbsp;</span> <span id="Span81" style="text-align: center;
                                    border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                    &nbsp; </span><span id="Span82" style="text-align: center; border-right: 1px solid transparent;
                                        width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span83" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span84" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblValue" runat="server"></asp:Label></span> 
                        <span id="Span85" style="text-align: center; border-right: 1px solid transparent;
                            width: 99px; display: block; float: left;">&nbsp;</span> <span id="Span86" style="text-align: center;
                                border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                &nbsp;</span> <span id="Span87" style="text-align: center; border-right: 1px solid transparent;
                                    width: 100px; display: block; float: left;">&nbsp;</span>
                <span id="Span88" style="text-align: center; border-right: 1px solid transparent;
                    width: 100px; display: block; float: left;">&nbsp;</span>
            </div>
            <div style="display: inline-block; height: 14px; width: 100%; line-height: 1px;">
                <span id="Span89" style="border-right: 1px solid transparent; width: 99px; display: block;
                    float: left;">&nbsp;</span> <span id="Span90" style="border-right: 1px solid transparent;
                        width: 481px; display: block; float: left;">Inv. No. :&nbsp;<asp:Label
                            ID="lblRefInvNo" runat="server"></asp:Label>
                        </span> <span id="Span91" style="text-align: center; border-right: 1px solid transparent;
                            width: 99px; display: block; float: left;">&nbsp;</span> <span id="Span92" style="text-align: center;
                                border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                &nbsp;</span> <span id="Span93" style="text-align: center; border-right: 1px solid transparent;
                                    width: 100px; display: block; float: left;">&nbsp;<asp:Label Style="position: relative;top: 2px;" ID="lblGrossTotal" runat="server"></asp:Label></span>
                                    <span id="Span94" style="text-align: center;
                                        border-right: 1px solid transparent; width: 100px; display: block; float: left;">
                                        &nbsp;</span>
            </div>
        </div>
       <%-- <div style="position: absolute; top: 0cm; right: -55px; width: 1037px; height: 400px;>
            <span id="Span22" style=""></span>
        </div>--%>
    </div>
    </form>
    <script type="text/javascript">
        window.print();
        window.onfocus = function () { window.close(); }
        
    </script>
</body>
</html>
