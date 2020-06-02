<%@ Page Title="Lorry Hire List" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="ManageLorryHire.aspx.cs" Inherits="WebTransport.ManageLorryHire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="center-block" style="width: 100%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
             <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Lorry Hire List</h5>
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
                        <a href="LorryHireSlip.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                 <asp:DropDownList ID="ddlDateRange" runat="server"  CssClass="form-control backlight" AutoPostBack="True" onselectedindexchanged="ddlDateRange_SelectedIndexChanged" ></asp:DropDownList>     
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtdatefrom" runat="server" CssClass="input-sm datepicker form-control backlight"   oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="input-sm datepicker form-control backlight"   oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Slip No.</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:TextBox ID="txtslipno" runat="server"  CssClass="form-control backlight" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loction</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlFromCity" TabIndex="5" runat="server" CssClass="form-control backlight"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Lorry No</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddllorryno" runat="server" CssClass="form-control backlight"></asp:DropDownList> 
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
                          <table>
                                       <tr>
                                       <td> 
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" onrowdatabound="grdMain_RowDataBound"  onrowcommand="grdMain_RowCommand"  onpageindexchanging="grdMain_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                            <ItemStyle HorizontalAlign="Left" Width="190" />
                                            <ItemTemplate>
                                             <%#Convert.ToDateTime(Eval("Lry_Date")).ToString("dd-MM-yyyy")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Slip No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="190">
                                            <ItemStyle HorizontalAlign="Left" Width="190" />
                                            <ItemTemplate>
                                                <%#Eval("Lry_SlipNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lorry No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180">
                                            <ItemStyle HorizontalAlign="Left" Width="180" />
                                            <ItemTemplate>
                                                <%#Eval("Lorry_No")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplied To " HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <%#Eval("SupliedTo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                            <ItemStyle HorizontalAlign="Left" Width="150" />
                                            <ItemTemplate>
                                                <%#Eval("City_Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Freight Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("TotalFrghtAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotalfrieght" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Advance Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("AdvanceAmnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotaladvance" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" HeaderText="Net Amount">
                                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100" />
                                                <ItemTemplate>
                                                    <%#String.Format("{0:0,0.00}", Convert.ToDouble(Eval("Net_amnt")))%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNetAmnt" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemTemplate> 
                                                <asp:LinkButton ID="lmkBtnEdit" class="edit" runat="server" CommandArgument='<%#Eval("LryHire_Idno") %>'
                                                    CommandName="cmdedit" ToolTip="Edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                             <asp:LinkButton ID="lnkBtnDelete" class="delete" runat="server" OnClientClick="return confirm('Do you want to delete this record ?');" CommandArgument='<%#Eval("LryHire_Idno") %>'
                                                    CommandName="cmddelete" ToolTip="Delete" ><i class="fa fa-trash-o"></i></asp:LinkButton>                               
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
                                       
                                     <b>
                                        <div class="col-sm-3" style="text-align:left"><asp:Label ID="lblcontant" runat="server"></asp:Label></div>  
                                        <div class="col-sm-4" style="text-align:right">TOTAL</div>
                                        <div class="col-sm-1" style="text-align:right;width:11%"><asp:Label ID="lblFreightAmount" runat="server"></asp:Label></div>
                                        <div class="col-sm-1" style="text-align:right;width:13%"><asp:Label ID="lblAdvanceAmount" runat="server"></asp:Label></div>
                                       <div class="col-sm-1" style="text-align:right;width:11%;"><asp:Label ID="lblNetAmount" runat="server"></asp:Label></div>
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


    <script language="javascript" type="text/javascript">

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

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        function setDatecontrol() {
            var mindate = $('#<%=hidmindate.ClientID%>').val();
              var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
              $('#<%=txtdatefrom.ClientID %>').datepicker({
                  buttonImageOnly: false,
                  maxDate: 0,
                  changeMonth: true,
                  changeYear: true,
                  dateFormat: 'dd-mm-yy',
                  minDate: mindate,
                  maxDate: maxdate
              });
              $('#<%=txtdateto.ClientID %>').datepicker({
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

