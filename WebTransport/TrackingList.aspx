<%@ Page Title="TRACKING LIST" Language="C#" MasterPageFile="~/Site1.Master"
    AutoEventWireup="true" CodeBehind="TrackingList.aspx.cs" EnableEventValidation="false"
    Inherits="WebTransport.TrackingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/U-Custom.css" rel="stylesheet" type="text/css" />
    <link href="css/tables.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center-block" style="width: 80%; margin-top: 30px; display: block;">
        <section class="panel panel-default full_form_container part_purchase_bill_form auto-height-form" style="box-shadow: 0 0px 2px gray; border: none; margin-top: 30px">
            <!--FORM HEADER-->
            <div class="ibox-title">
                <h5><div class="printing-animation icon-size"></div>Tracking List </h5>
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
                        <a href="Tracking.aspx"><i style="font-size: 13px !important;" class="fa fa-plus action-icon-style1"></i></a>
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
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control backlight" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged"></asp:DropDownList>  
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date From</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:TextBox ID="txtReceiptDatefrom" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50"  data-date-format="dd-mm-yyyy" ></asp:TextBox>                                     
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Date To</span>
                            </div>
                            <div class="control-holder full-width">
                             <asp:TextBox ID="txtReceiptDateto" runat="server" CssClass="input-sm datepicker form-control backlight" MaxLength="50" data-date-format="dd-mm-yyyy" ></asp:TextBox>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Vehicle No.</span>
                            </div>
                            <div class="control-holder full-width">
                                <asp:DropDownList ID="DdlVehicleNo" runat="server" CssClass="chzn-select" style="width:100%;"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>From Loc.</span>
                            </div>
                            <div class="control-holder full-width">
                           <asp:DropDownList ID="DdlFromLoc" runat="server" CssClass="chzn-select" style="width:100%;"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Company Name</span>
                            </div>
                            <div class="control-holder full-width">
                           <asp:DropDownList ID="DdlCompName" runat="server" CssClass="chzn-select" style="width:100%;"></asp:DropDownList>
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>Lane</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="DdlLane" runat="server" CssClass="form-control backlight"></asp:DropDownList>  
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>From City</span>
                            </div>
                            <div class="control-holder full-width">
                                  <asp:DropDownList ID="DdlFromCity" runat="server" CssClass="form-control backlight"></asp:DropDownList>       
                            </div>
                        </div>
                                <div class="col-sm-4 no-pad">
                            <div class="control-label"> 
                                <span>To City</span>
                            </div>
                            <div class="control-holder full-width">
                                 <asp:DropDownList ID="DdlToCity" runat="server" CssClass="form-control backlight"></asp:DropDownList>    
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
                        <asp:GridView ID="grdMain" runat="server" CssClass="table-style-white last-row-select" AutoGenerateColumns="false" BorderStyle="None" Width="100%" GridLines="None" AllowPaging="true" PageSize="50" OnRowCommand="grdMain_RowCommand" OnRowDataBound="grdMain_RowDataBound" >
                            <Columns>
                                 <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Lorry_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToDateTime(Eval("Tracking_Date")).ToString("dd-MM-yyyy")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From Loc" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("LocFrom")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Comp_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Lane" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("Lane_Name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("FromCity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="To City" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100" />
                                                    <ItemTemplate>
                                                        <%#Eval("ToCity")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                      <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("TrackingHead_Idno") %>' CommandName="cmdedit" TabIndex="5" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>

                                                 <asp:LinkButton ID="lnkbtnDelete" CssClass="delete" runat="server" CommandArgument='<%#Eval("TrackingHead_Idno") %>' CommandName="cmddelete" OnClientClick="return confirm('Do you want to delete this record ?');"  TabIndex="6" ToolTip="Click to delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    
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

            $("#<%=txtReceiptDatefrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtReceiptDateto.ClientID %>").datepicker({
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
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
