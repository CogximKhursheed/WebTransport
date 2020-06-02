﻿<%@ Page Title="Manage Item master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageItemMast.aspx.cs"
    Inherits="WebTransport.ManageItemMast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 50%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Item Master List </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
		                <!--DOWNLOAD OPTIONS-->
                        <div class="fa fa-download"></div>
                        <div class="download-option-box">
                            <div class="download-option-container">
                                <ul>
                                    <li class="download-excel" data-name="Download excel"><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" onclick="imgBtnExcel_Click" Visible="false"/></li>
                                </ul>
                            </div>
                            <div class="close-download-box" title="Close download window"></div>
                        </div>
                        <a href="ItemMast.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Item Group</span>
                            </div>
                            <div class="control-holder full-width">
                                   <asp:DropDownList ID="ddlGroupType" runat="server" class="form-control backlight"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Item Type</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlItemType" runat="server" class="form-control backlight"></asp:DropDownList> 
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Item Name</span>
                            </div>
                            <div class="control-holder full-width">
                            <asp:TextBox ID="txtItemName" placeholder="Enter Item Name" runat="server" class="form-control backlight" MaxLength="15"></asp:TextBox>   
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkbtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkbtnPreview_OnClick">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                     <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                          <table>
                                       <tr>
                                       <td> 
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("ItemName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Item Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Eval("TypeName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Group Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemTemplate>
                                                        <%#Eval("IgrpName")%>
                                                        <asp:HiddenField ID="hidStats" runat="server" Value='<%#Eval("Status")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UOM Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Eval("UOMName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Pur Rate" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="120">
                                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                    <ItemStyle HorizontalAlign="right" Width="120" />
                                                    <ItemTemplate>
                                                                <%# Convert.ToString(Eval("PurRate")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("PurRate")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="SGST" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="110">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="right" Width="110" />
                                                    <ItemTemplate>
                                                            <%# Convert.ToString(Eval("SGST")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("SGST")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CGST" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="110">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="right" Width="110" />
                                                    <ItemTemplate>
                                                            <%# Convert.ToString(Eval("CGST")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("CGST")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="IGST" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="110">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="right" Width="110" />
                                                    <ItemTemplate>
                                                            <%# Convert.ToString(Eval("IGST")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("IGST")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <%-- <asp:TemplateField HeaderText="VAT Tax" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="110">
                                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                    <ItemStyle HorizontalAlign="right" Width="110" />
                                                    <ItemTemplate>
                                                            <%# Convert.ToString(Eval("VATTaxPer")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("VATTaxPer")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CST Tax" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="right" Width="100" />
                                                    <ItemTemplate>
                                                                <%# Convert.ToString(Eval("CSTTaxPer")) == "" ? Convert.ToDouble(0).ToString("N2") : Convert.ToDouble(Eval("CSTTaxPer")).ToString("N2")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("ItemIdno")+"_"+Eval("Status") %>'
                                                            CommandName="cmdstatus" ToolTip="Active/Inactive" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                                    <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("ItemIdno") %>' CommandName="cmdEdit" TabIndex="7" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("ItemIdno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="8" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                            </Columns>
                              <EmptyDataRowStyle HorizontalAlign="Center" />
                                 <EmptyDataTemplate>
                                     <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                 </EmptyDataTemplate>
                        </asp:GridView>
                                           </td>
                                       </tr>
                                        <tr>
                                       <td>
                                       <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <table class="" id="tblFooterscnd" runat="server" >
		                                  <tr><th rowspan="1" colspan="1" style="width:149px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 149px;"></th><th rowspan="1" colspan="1" style="width: 120px;text-align:right;">&nbsp;</th><th rowspan="1" colspan="1" style="width: 110px;padding-left:60px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label>
                                          </th><th rowspan="1" colspan="1" style="width:2px;"></th><th rowspan="1" colspan="1" style="width: 62px;"></th><th rowspan="1" colspan="1" style="width: 63px;"></th></tr>                                  
		                                </tfoot>
                                        </table>

                                       </div>
                                       </td>
                                       </tr>
                                       <tr>
                                       <td>
                                           <br /> &nbsp;
                                       </td>
                                       </tr>
                                       </table>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <%--hidden fields--%>
    <asp:HiddenField ID="hidid" runat="server" />
    <asp:HiddenField ID="hiditemidno" runat="server" />

    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnPreview.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnPreview.ClientID %>").attr("src", "images/search_img.jpg");
            }
        }
    </script>

    <script src="js/U-Custom.js" type="text/javascript"></script>
    <script src="js/U-Custom.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            LoadControls();

            AutoFormHeight();
        });

        function LoadControls() {
            AnimatedIcon();
        }
        function AutoFormHeight() {
            $('.auto-height-form').height(parseInt($('').height()) - parseInt(40))
        }
    </script>
</asp:Content>
