<%@ Page Title="Manage Ledger account" Language="C#" MasterPageFile="~/Site1.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="ManageLedgerAccount.aspx.cs" Inherits="WebTransport.ManageLedgerAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Ledger Account List</h5>
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
                        <a href="LedgerMaster.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Party Name</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtAccountPrtyName" Placeholder="Party Name" runat="server" CssClass="form-control backlight"></asp:TextBox>                                
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Account Type</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control backlight"></asp:DropDownList>   
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Balance Type</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlBalanceType" runat="server" CssClass="form-control backlight">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cr" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Dr" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>State</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control backlight"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Mobile</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="TxtMobile" runat="server" PlaceHolder="Mobile Number" CssClass="form-control backlight"></asp:TextBox>                                 
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-6 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkbtnSearch" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkbtnSearch_OnClick">Search</asp:LinkButton>                                     
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
                        <asp:GridView ID="grdUser" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnRowDataBound="grdUser_RowDataBound" OnRowCommand="grdUser_RowCommand" OnPageIndexChanging="grdUser_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="20" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemTemplate>
                                                <%#Eval("Acnt_Name")%>
                                            </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vechl. Detls." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle CssClass="gridHeaderAlignLeft" Width="50" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnClaimDetails" runat="server" CommandArgument='<%#Eval("AcntIdno") %>' CommandName="CmdeditDetails" ToolTip="Click to Enter New tyre details for Claim Process."><i class="fa fa-dot-circle-o"></i></asp:LinkButton>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Type" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                            <ItemTemplate>
                                                <%#Eval("AcntType_Name")%>
                                            </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Op. Balance" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblOpBal" runat="server" Text='<%#Eval("OpBalnce")%>'></asp:Label>
                                                <asp:Label ID="lblOpBalTyp" runat="server" Text='<%#Eval("BalanceType")%>'></asp:Label>
                                            </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="80" />
                                            <ItemTemplate>
                                                <%#Eval("Address")%>
                                            </ItemTemplate>
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <ItemTemplate>
                                                <%#Eval("MobileNo")%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="CityName" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="70" />
                                            <ItemTemplate>
                                                <%#Eval("City_Name")%>
                                            </ItemTemplate>                                        
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="StateName" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="70" />
                                            <ItemTemplate>
                                                <%#Eval("State_Name")%>
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("AcntIdno")+"_"+Eval("Status") %>'
                                                        CommandName="cmdstatus" ToolTip="Active/Inactive" />
                                                </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" >
                                               <HeaderStyle HorizontalAlign="Center" Width="50" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                              <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("AcntIdno")+","+Eval("AcntType") %>' CommandName="cmdEdit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                              <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("AcntIdno")+","+Eval("AcntType") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>  
                                            </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealerid" runat="server" Text='<%#Eval("AcntIdno")+","+Eval("AcntType") %>'> 
                                                </asp:Label>
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
                                        <b>
                                        <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <div class="col-sm-2" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-4" style="text-align:right">TOTAL</div>
                                        <div class="col-sm-2" style="text-align:right"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></asp:Label></div>
                                       </div>
                                       </b>
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

    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnSearch.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkbtnSearch.ClientID %>").attr("src", "images/search_img.jpg");
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
