<%@ Page Title="Lorry Master List" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageLorryMaster.aspx.cs" Inherits="WebTransport.ManageLorryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Lorry Master List </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
		                <!--DOWNLOAD OPTIONS-->
                        <div class="fa fa-download"></div>
                        <div class="download-option-box">
                            <div class="download-option-container">
                                <ul>
                                    <li class="download-excel" data-name="Download excel"><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" onclick="imgBtnExcel_Click1" Visible="false"/></li>
                                </ul>
                            </div>
                            <div class="close-download-box" title="Close download window"></div>
                        </div>
                        <a href="LorryMaster.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                 <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control backlight"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Owner Name</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtOwnrNme" runat="server" CssClass="form-control backlight" MaxLength="50"></asp:TextBox>                  
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>Lorry No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtlryno" runat="server" CssClass="form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-6 no-pad">
                            <div class="control-label"> 
                                <span>PAN No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-12 no-pad">
                            <div class="control-holder"> <span class="label-holder">&nbsp;</span></div>
                            <div class="control-holder">
                            <asp:LinkButton ID="lnkBtnPreview" CssClass="btn btn-primary pull-right" runat="server" ValidationGroup="Save"  OnClick="lnkBtnPreview_Click">Search</asp:LinkButton>                                     
                            </div>
                    </div>
                            </div>
                        </div>
                <div class="panel-in-default" >
                    <div class="pull-left"><asp:Label ID="lblTotalRecord" runat="server" Text=" Total Record(s):0" Style="font-size: 13px; text-transform: none;"></asp:Label></div>
                    <div class="pull-right"></div>
                    <div class="report" style="overflow-x:Auto; width:100%;">
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemTemplate> 
                                        <asp:LinkButton ID="lmkBtnEdit" class="edit" runat="server" CommandArgument='<%#Eval("LorryIdno") %>'
                                            CommandName="cmdedit" ToolTip="Edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnDelete" class="delete" runat="server" OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("LorryIdno") %>'
                                            CommandName="cmddelete" ToolTip="Delete" ><i class="fa fa-trash-o"></i></asp:LinkButton>                               
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                        <%#Eval("prty_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Owner Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                        <%#Eval("OwnerName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                        <%#Eval("MobileNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lorry Type " HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                        <%#Eval("LorryType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lorry No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180">
                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                    <ItemTemplate>
                                        <%#Eval("LorryNo")%>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Driver Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180">
                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                    <ItemTemplate>
                                        <%#string.IsNullOrEmpty(Eval("DriverName").ToString()) ? "N/A" : Eval("DriverName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lorry Make" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("LorryMake")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pan No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                    <ItemTemplate>
                                        <%#Eval("PanNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LowRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                            <%#(string.IsNullOrEmpty(Convert.ToString(Eval("LowRate"))) ? "" : (Convert.ToString((Eval("LowRate")))))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Engine No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Left" Width="180" />
                                    <ItemTemplate>
                                        <%#Eval("EngineNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chasis No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Left" Width="190" />
                                    <ItemTemplate>
                                        <%#Eval("ChasisNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnStatus" runat="server" Width="15px" Height="15px" CommandArgument='<%#Eval("LorryIdno")+"_"+Eval("Status") %>'
                                            CommandName="cmdstatus" ToolTip="Active/Inactive" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                               <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <%--hidden fields--%>
    <asp:HiddenField ID="hidlocidno" runat="server" />

    <script language="javascript" type="text/javascript">
        function mouseOverImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkBtnPreview.ClientID %>").attr("src", "images/search_btn.jpg");
            }
        }
        function mouseOutImage(ctrlname) {
            if (ctrlname == "search") {
                $("#<%=lnkBtnPreview.ClientID %>").attr("src", "images/search_img.jpg");
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
