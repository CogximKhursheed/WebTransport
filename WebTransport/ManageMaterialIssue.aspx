<%@ Page Title="Material Issue List" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageMaterialIssue.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.ManageMaterialIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

            $("#<%=Datefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=Dateto.ClientID %>").datepicker({
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

    <div class="center-block" style="width: 70%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <%--<header class="panel-heading font-bold form_heading">
                <span class="pull-left">
                <div class="printing-animation icon-size"></div>Item Group Master List </span>&nbsp;
                <asp:Label ID="lblMasterName" runat="server"></asp:Label> &nbsp;
                <div class="pull-right action-center">
		            <!--DOWNLOAD OPTIONS-->
                    <div class="fa fa-download">
                        <div class="download-options">
                            <ul>
                                <li class="download-excel">
                                     <!--DOWNLOAD LINK HERE-->
                                    <asp:ImageButton ID="sdfsd" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" onclick="imgBtnExcel_Click"  Visible="false"/>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <a href="ItmGrpMaster.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                </div>
            </header>--%>
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Material Issue List </h5>
                <div class="pull-right title-action">
                    <div class="action-center">
		                <!--DOWNLOAD OPTIONS-->
                        <div id="prints" runat="server">
                        <div class="fa fa-download"></div>
                        <div class="download-option-box">
                            <div class="download-option-container">
                                <ul>
                                    <li class="download-excel" data-name="Download excel"><asp:ImageButton ID="imgBtnExcel" runat="server" AlternateText="Excel"  ToolTip="Export to excel" ImageUrl="~/images/Excel_Img.JPG" style="height: 100%" Visible="false"/></li>
                                </ul>
                            </div>
                            <div class="close-download-box" title="Close download window"></div>
                        </div>
                         </div>
                        <a href="MaterialIssue.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                 <asp:DropDownList ID="ddlDateRange" runat="server" AutoPostBack="True" CssClass="form-control backlight" OnSelectedIndexChanged="ddlDateRange_SelectedIndexChanged" >         </asp:DropDownList>     
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlDateRange" ValidationGroup="save" ErrorMessage="Select date range!" SetFocusOnError="true" CssClass="classValidation"></asp:RequiredFieldValidator>       
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="12" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Dateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="12" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Location</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control backlight" > </asp:DropDownList>   
                            </div>
                        </div>
                                <div class="col-sm-1 no-pad">
                            <div class="control-label"> 
                                <span>&nbsp;</span>
                            </div>
                            <div class="control-holder full-width">
                                 &nbsp;&nbsp;<asp:LinkButton ID="lnkBtnLocation" runat="server" ToolTip="Update From Location"   Height="23px" CausesValidation="false"  class="btn-sm btn btn-primary acc_home" onclick="lnkBtnLocation_Click"><i class="fa fa-refresh"></i></asp:LinkButton>                            
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>TruckNo.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlTruckNo" runat="server" CssClass="form-control backlight" ></asp:DropDownList>      
                            </div>
                        </div>
                                <div class="col-sm-1 no-pad">
                            <div class="control-label"> 
                                <span>&nbsp;</span>
                            </div>
                            <div class="control-holder full-width">
                            &nbsp;&nbsp;<asp:LinkButton ID="lnkTruckRefresh" runat="server" ToolTip="Update Truck No"  Height="23px" CausesValidation="false" class="btn-sm btn btn-primary acc_home" onclick="lnkTruckRefresh_Click"><i class="fa fa-refresh"></i></asp:LinkButton>                             
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
                         <table>
                                       <tr>
                                       <td>    
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowCreated="grdMain_RowCreated" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Issue No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("MatIss_No")%>
                                                    <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("MatIss_No") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Issue Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("MatIss_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("MatIss_Typ")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("CityTo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Truck No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Lorry_No")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                <strong>Grid Total</strong>
                                                </FooterTemplate>    
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                                                                       
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label ID="lblGridtotalnet" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemTemplate>
                                                  <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("MatIss_Idno") %>' CommandName="cmdedit" TabIndex="7" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                   <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("MatIss_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="8" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    <asp:ImageButton ID="imgchallan" runat="server" Visible="false" ToolTip="Challan generated"
                                                        autopostback="false" ImageUrl="~/images/Challan.jpg" />
                                                    <asp:ImageButton ID="imgSold" runat="server" Visible="false" ToolTip="Invoice generated" ImageUrl="~/Images/Sold.png" />
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
		                                  <tr><th rowspan="1" colspan="1" style="width:190px;"> <asp:Label ID="lblcontant" runat="server"></asp:Label></th><th rowspan="1" colspan="1" style="width: 100px;"></th><th rowspan="1" colspan="1" style="width: 23px;text-align:right;"></th><th rowspan="1" colspan="1" style="width: 112px;padding-left:0px;">
                                          </th><th rowspan="1" colspan="1" style="width:100px;">TOTAL</th><th rowspan="1" colspan="1" style="width: 63px;"><asp:Label ID="lblNetTotalAmount" runat="server"></asp:Label></th></tr>                                                  
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
    <asp:HiddenField ID="hidrcptheadidno" runat="server" />
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />

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
