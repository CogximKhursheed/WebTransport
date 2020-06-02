<%@ Page Title="Advance Booking [GR] List" Language="C#" MasterPageFile="~/Site1.Master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageAdvBookGr.aspx.cs"
    Inherits="WebTransport.ManageAdvBookGr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Advance Booking [GR] List </h5>
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
                        <a href="AdvBookGR.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="">
                            <div class="col-sm-12 no-pad">
                                <!--FIRST-FOLD-->
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date Range</span>
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
                                <asp:TextBox ID="Datefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="6" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="Dateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loc.[From]</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="drpBaseCity" runat="server" CssClass="form-control backlight" ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>To City</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="drpCityTo" runat="server" CssClass="chzn-select" style="width:100%;" ></asp:DropDownList>    
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Deliv. Place</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="drpCityDelivery" runat="server" CssClass="chzn-select" style="width:100%;" ></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Order No</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtOrderNumb" runat="server" CssClass="form-control backlight" MaxLength="10" placeholder="Order Number" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>&nbsp;</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlGRType" runat="server" CssClass="form-control backlight" Enabled="false">
                                   <asp:ListItem Text="ALL" Value="0"   ></asp:ListItem>
                                    <asp:ListItem Text="Paid GR" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="TBB GR" Value="2"  Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="To Pay GR" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Party</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="ddlSender" runat="server" CssClass="form-control backlight" > </asp:DropDownList>
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Order No."  HeaderStyle-Width="100">    
                                            <ItemStyle CssClass="gridHeaderAlignCenter" />                                         
                                                <ItemTemplate>
                                                    <%#Eval("AdvOrder_No")%>
                                                    <asp:Label ID="lblGridNo" runat="server" Text='<%#Eval("AdvOrderGR_Idno") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="GR Type"  HeaderStyle-Width="100">    
                                            <ItemStyle CssClass="gridHeaderAlignCenter" />                                         
                                                <ItemTemplate>
                                                    <%#Eval("GRTYPENAME")%>                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("AdvOrder_Date")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("Sender")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("CityFrom")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="To City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100" />
                                                <ItemTemplate>
                                                    <%#Eval("CityTo")%>
                                                </ItemTemplate>                          
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Del. Place">                                              
                                                <ItemTemplate>
                                                     <%#Eval("CityDely")%>
                                                </ItemTemplate>      
                                                <FooterStyle HorizontalAlign="Left" />
                                                <FooterTemplate>
                                               <b>Grid Total</b>
                                                </FooterTemplate>                                         
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight"  HeaderStyle-Width="100" HeaderText="Amount">                                              
                                               <ItemStyle CssClass="gridHeaderAlignRight" />
                                                <ItemTemplate>
                                                     <%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>
                                                </ItemTemplate>  
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                <b><asp:Label ID="lblNetAmnt" runat="server"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField> 
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Ref.</br>No.">                                              
                                                <ItemTemplate>
                                                     <%#Eval("RefNo")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Stuff</br>Date">                                              
                                                <ItemTemplate>
                                                     <%#string.IsNullOrEmpty(Eval("StuffDate").ToString()) ? "N/A" : Convert.ToDateTime(Eval("StuffDate")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField> 
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Remark">                                              
                                                <ItemTemplate>
                                                     <%#Eval("Remark")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Cont.</br>No.">                                              
                                                <ItemTemplate>
                                                     <%#Eval("ContNo")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Cont.</br>Size.">                                              
                                                <ItemTemplate>
                                                     <%#Eval("ContSiz")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField> 
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Cont.</br>Type.">                                              
                                                <ItemTemplate>
                                                     <%#Eval("ContType")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Bkg.</br>From">                                              
                                                <ItemTemplate>
                                                <%#Eval("BkgDateFrom")==null? "" : Convert.ToDateTime(Eval("BkgDateFrom")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>   
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Bkg.</br>To">                                              
                                                <ItemTemplate>
                                                <%#Eval("BkgDateTo")==null? "" : Convert.ToDateTime(Eval("BkgDateTo")).ToString("dd-MM-yyyy")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField> 
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Port">                                              
                                                <ItemTemplate>
                                                     <%#Eval("Port")%>
                                                </ItemTemplate>      
                                            </asp:TemplateField>  
                                 <asp:TemplateField HeaderStyle-CssClass="gridHeaderAlignRight"  HeaderStyle-Width="100" HeaderText="Action">
                                            <ItemStyle CssClass="gridHeaderAlignCenter" />
                                                <ItemTemplate>                                               
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("AdvOrderGR_Idno") %>' CommandName="cmdedit" ToolTip="Click to edit" TabIndex="10"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("AdvOrderGR_Idno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete" TabIndex="11" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>                                           

                                                </ItemTemplate>
                                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                           </td>
                                       </tr>
                                        <tr>
                                       <td>
                                       <b>
                                        <div class="secondFooterClass"  id="divpaging" runat="server" visible="false">                                                                           
                                        <div class="col-sm-4" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-1" style="text-align:right">TOTAL</div>
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
                    </div>
                </div>
            </div>
        </section>
    </div>
    <%--hidden fields--%>
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



    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <script language="javascript" type="text/javascript">
        SetFocus();
        function SetFocus() {
            $('input[type="text"]').focus(function () {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function () {
                $(this).removeClass("focus");
            });
            $("select").focus(function () {
                $(this).addClass("focus");
            });
            $("select").blur(function () {
                $(this).removeClass("focus");
            });
        }

        function ShowModalPopup() {
            ShowDialog(true);
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });

        function ShowModalPopup() {
            ShowDialog(true);
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FooterScriptPlaceHolder" runat="server">
    <script>
        //$(document).ready(function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '<%=hidmindate.Value%>',
            maxDate: '<%=hidmaxdate.Value%>'
        });
        //});
    </script>
</asp:Content>

