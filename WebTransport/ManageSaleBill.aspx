<%@ Page Title="Manage Item master" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageSaleBill.aspx.cs"
    Inherits="WebTransport.ManageSaleBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 90%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
             <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Sale List </h5>
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
                        <a href="SaleBill.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                 <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control backlight" AutoPostBack="false"  onselectedindexchanged="ddlDateRange_SelectedIndexChanged"></asp:DropDownList>    
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>From Date</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtBillDateFrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="6"  onchange="Focus()" data-date-format="dd-mm-yyyy"></asp:TextBox>
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>To Date</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtBillDateTo" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="6"  onchange="Focus()" data-date-format="dd-mm-yyyy"></asp:TextBox>
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Pref No</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtprefNo" PlaceHolder="Pref No." runat="server" CssClass="form-control backlight"></asp:TextBox>
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Bill No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtBillNo" PlaceHolder="Bill Number" runat="server" CssClass="form-control backlight"></asp:TextBox>
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Loc. [From]</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlFromCity" runat="server"  CssClass="form-control backlight" AutoPostBack="false">      </asp:DropDownList>
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Party Name</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control backlight" AutoPostBack="false">      </asp:DropDownList> 
                            </div>
                        </div>
                                 <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Bill Type</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="form-control backlight" AutoPostBack="false">
                                      <asp:ListItem Text="Choose..." Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="2"></asp:ListItem>
                                 </asp:DropDownList> 
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50"  OnPageIndexChanging="grdMain_PageIndexChanging" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70" />
                                                    <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("SbillHeadIdno") %>' CommandName="cmdEdit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("SbillHeadIdno") %>' OnClientClick="return confirm('Do you want to delete this record ?');" CommandName="cmddelete"  ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill No." HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("SbillNo")%>
                                                        <asp:Label ID="lblSbillNo" runat="server" Text='<%#Eval("SbillHeadIdno") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Against" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("Against")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sale Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120" />
                                                    <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("SBillDate")).ToString("dd-MM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemTemplate>
                                                        <%#Eval("SbillType")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Location [From]" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemTemplate>
                                                        <%#Eval("FromLocation")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120" />
                                                    <ItemTemplate>
                                                        <%#Eval("PartyName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                            </Columns>
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
    <asp:HiddenField ID="hidmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
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


    <script language="javascript" type="text/javascript">
        window.onload = function () {
        };

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
            $("#<%=txtBillDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate

            });
            var mindate = $('#<%=hidmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $("#<%=txtBillDateTo.ClientID %>").datepicker({
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
