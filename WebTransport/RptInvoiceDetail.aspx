<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptInvoiceDetail.aspx.cs" Inherits="WebTransport.RptInvoiceDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Westend Roadlines</title>
    <style type="text/css">
        table tr {
            height: 30px;
            font-size: 12px;
        }

            table tr u {
                font-size: 45px;
            }

        h1 {
            margin: 0;
            padding: 0;
        }
	</style>
</head>
<body>
    <table width="900" cellspacing="0" cellpadding="10" align="center" border="1" style="font-family: Arial">
        <tr>
            <td colspan="5" align="center">
                <h1><u>
                    <asp:Label ID="lblCompName" runat="server"></asp:Label></u></h1>
                <br />
                <strong>
                    <asp:Label ID="lbltr" runat="server"></asp:Label></strong><br>
                <asp:Label ID="lbladdr" runat="server"></asp:Label><asp:Label ID="lblpin" runat="server"></asp:Label><br>
                Email:
                <asp:Label ID="lblemail" runat="server"></asp:Label>
                Mobile:
                <asp:Label ID="lblmob" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td width="100"><strong>To</strong></td>
            <td colspan="3" align="center"><strong>
                <asp:Label ID="lblcomp" runat="server"></asp:Label><br />
                CEMENT DIVISION<br>
                <asp:Label ID="lbladd" runat="server"></asp:Label></strong></td>
            <td width="200"><strong>BILL NO.
                <asp:Label ID="lblbill" runat="server"></asp:Label><br>
                DATE:
                <asp:Label ID="lbldate" runat="server"></asp:Label></strong></td>
        </tr>
        <tr>
            <td colspan="5" align="left">
                <!--LABEL-->
                <label>Transportation charges of Cement dispatched from TATA CHEMICALS LTD. Mithapur destination given parties on your behalf as per unclosed statements.</label><!--/LABEL--></td>
        </tr>
        <tr>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                    <td><strong>SR NO.</strong></td>
                    <td><strong>NOS, GC</strong></td>
                    <td><strong>BILL NO</strong></td>
                    <td><strong>AMOUNT</strong></td>
                    <td rowspan="14"></td>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%#Eval("GRNo")%></td>
                        <td style="text-align: center;"><%#Eval("Inv_No")%></td>
                        <td style="text-align: right;"><%#Eval("Net_Amnt")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </tr>
        <%-----------------GST BOX-----------------%>
        <tr>
            <td colspan="2" style="text-align: left;">
                <asp:Label ID="lblword" runat="server"></asp:Label></td>
            <td><strong>TOTAL:</strong></td>
            <td align="right">
                <asp:Label ID="lbltotal" Font-Bold="true" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <th colspan="2" style="border-top: 1px solid; border-bottom: none; text-align: right;"></th>
            <th style="font-size: 11px; text-align: left;"><strong>BILL AMOUNT IN RS.</strong></th>
            <th align="right">
                <asp:Label ID="lblbillamnt" Font-Bold="true" runat="server"></asp:Label></th>
        </tr>
        <tr id="trsgst" runat="server">
            <th colspan="2" style="border-top: none; border-bottom: none; text-align: right;"></th>
            <th align="left">
                <asp:Label ID="lblsgstper" Font-Bold="true" runat="server"></asp:Label></th>
            <th align="right">
                <asp:Label ID="lblsgstamnt" Font-Bold="true" runat="server"></asp:Label></th>
        </tr>
        <tr id="trcgst" runat="server">
            <th colspan="2" style="border-top: none; border-bottom: none; text-align: right;"></th>
            <th align="left">
                <asp:Label ID="lblcgstper" Font-Bold="true" runat="server"></asp:Label></th>
            <th align="right">
                <asp:Label ID="lblcgstamnt" Font-Bold="true" runat="server"></asp:Label></th>
        </tr>
        <tr id="trigst" runat="server">
            <th colspan="2" style="border-top: none; border-bottom: none; text-align: right;"></th>
            <th align="left">
                <asp:Label ID="lbligstper" Font-Bold="true" runat="server"></asp:Label></th>
            <th align="right">
                <asp:Label ID="lbligstamnt" Font-Bold="true" runat="server"></asp:Label></th>
        </tr>
        <tr>
            <th colspan="2" style="border-top: none; border-bottom: none; text-align: right;"></th>
            <th style="font-size: 11px; text-align: left;"><strong>TOTAL RS.</strong></th>
            <th align="right">
                <asp:Label ID="lblTotalAmount" Font-Bold="true" runat="server"></asp:Label></th>
        </tr>

        <%------------ SERVICES ,  EDUCATION , HIGHER EDUCATION TAX BOX-------------%>
        <%--<tr>
                    <td style="font-size:11px;"><strong>SERVICES TAX</strong></td>
		            <td> <asp:Label ID="lblsertax" runat="server"></asp:Label></td>
		            <td colspan="3" rowspan="3"><!--LABEL-->
                    <label>--Terms and conditions here--</label><!--/LABEL--></td>
	    </tr>--%>
        <%--<tr>
                    <td style="font-size:11px;"><strong>EDUCATION TAX</strong></td>
		            <td> <asp:Label ID="lbledtax" runat="server"></asp:Label></td>
	   </tr>--%>
        <%--	<tr>
                     <td style="font-size:11px;"><strong>HIGHER EDUCATION TAX</strong></td>
		             <td><asp:Label ID="lblhighertax" runat="server"></asp:Label></td>
	  </tr>--%>
        <tr>
            <td colspan="5" style="text-align: right;">
                <br />
                <br />
                <br />
                <br />
                <strong>For: 
                    <asp:Label ID="lblsign" runat="server"></asp:Label></strong>
            </td>
        </tr>
    </table>
</body>
</html>
