<%@ Page Title="Stock Transfer List" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="ManageStockTransfer.aspx.cs" EnableEventValidation="false" Inherits="WebTransport.ManageStockTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 80%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Stock Transfer List </h5>
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
                        <a href="StockTransfer.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date Range <span class="required-field">*</span> </span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control backlight" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>   
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Issue No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtIssNo" runat="server" PlaceHolder="Stock Transfer No"  CssClass="form-control backlight" MaxLength="50"></asp:TextBox>   
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loc. [From]</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="drpCityFrom" runat="server" CssClass="form-control backlight"></asp:DropDownList>            
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>To City</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="drpCityTo" runat="server" CssClass="form-control backlight"></asp:DropDownList>                            
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad" style="visibility:hidden;">
                            <div class="control-label"> 
                                <span></span>
                            </div>
                            <div class="control-holder full-width">
                            <%--<label class="col-sm-4 control-label" style="width: 29%;">Item Type</label>
	                                <div class="col-sm-8" style="width: 71%;">
	                                  <asp:DropDownList ID="drpItemType" runat="server" CssClass="form-control" TabIndex="7" ></asp:DropDownList>
	                                </div>--%>
                            </div>
                        </div>
                                <!--SECOND-FOLD-->
                                <div class="col-sm-12 no-pad">
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
                             <table width="100%">
                                       <tr>
                                       <td>  
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <b><%#Container.DataItemIndex+1 %>.</b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Issue No." HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("StckTrans_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("StckTrans_Date")).ToString("dd-MM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City From" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("CityFrom")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City TO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("CityTo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Item Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("ItemType")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("SerialNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Qty")%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" ForeColor="Black" Font-Bold="true" />
                                            <FooterTemplate>
                                               Grid Total :
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                 <%-- <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Rate")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                            </ItemTemplate>
                                            <FooterStyle CssClass="gridHeaderAlignRight" ForeColor="Black" Font-Bold="true" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblNetAmnt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-CssClass="gridHeaderAlignCenter">
                                            <ItemStyle CssClass="gridHeaderAlignCenter" Width="50" />
                                             <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("StckTrans_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("StckTrans_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                            <div class="col-sm-6" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                            <div class="col-sm-4" style="text-align:right">TOTAL</div>
                                            <div class="col-sm-1" style="text-align:right"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></div>
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

                        <div style="display:none">
                            <asp:GridView ID="grdprint" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="false" PageSize="25">
                                <Columns>
                                     <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <b><%#Container.DataItemIndex+1 %>.</b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Issue No." HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="center" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("StckTrans_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("StckTrans_Date")).ToString("dd-MM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="City From" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("CityFrom")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="City TO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("CityTo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Item Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("ItemType")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="100">
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("SerialNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                            <ItemStyle HorizontalAlign="Left" Width="100" />
                                            <ItemTemplate>
                                                <%#Eval("Qty")%>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" ForeColor="Black" Font-Bold="true" />
                                            <FooterTemplate>
                                               Grid Total :
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="50" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                            </ItemTemplate>
                                            <FooterStyle CssClass="gridHeaderAlignRight" ForeColor="Black" Font-Bold="true" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblNetAmnt" runat="server"></asp:Label>
                                            </FooterTemplate>
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
            </div>
        </section>
    </div>
    <%--hidden fields--%>
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />

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


    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });
        prm.add_endRequest(function () {
            setDatecontrol();
        });
        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();

            $("#<%=txtDatefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateto.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
    </script>
</asp:Content>
