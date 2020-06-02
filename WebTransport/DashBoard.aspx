<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="DashBoard.aspx.cs" Inherits="WebTransport.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dash-banner">
        <img src="img/dashboard-banner.jpg" alt="dashboard banner">
    </div>
    <div class="row" id="dashboard-progress" style="height: 45px;">
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color1">
                    <a href="ItemMaster.aspx"><i class="fa fa-user"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Item<br>
                        Master</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color2">
                    <a href="LedgerMaster.aspx"><i class="fa fa-tags"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Ledger<br>
                        Master</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color3">
                    <a href="RateMaster.aspx?FTyp=TBB"><i class="fa fa-reorder"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Rate<br>
                        Master[TBB]</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color4">
                    <a href="GRPrep.aspx"><i class="fa fa-wechat"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>GR<br>
                        Preparation</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color5">
                    <a href="ChlnBooking.aspx"><i class="fa fa-eye"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Challan<br>
                        Booking</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color6">
                    <a href="Challanconfirmation.aspx"><i class="fa fa-server"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Challan<br>
                        Confirmation</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color7">
                    <a href="Invoice.aspx"><i class="fa fa-th-large"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Invoice<br>
                        Generation</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color8">
                    <a href="ChallanDelverd.aspx"><i class="fa fa-send"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Delivery<br>
                        Register</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color9">
                    <a href="DeliveryChallanDetails.aspx"><i class="fa fa-book"></i></a>
                    <%--<i class="fa fa-book"></i>--%>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Delivery<br>
                        Challan</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color10">
                    <a href="AccountBookRpt.aspx"><i class="fa fa-table"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Accounts<br>
                        Book</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color11">
                    <a href="ChallanConfirmationRep.aspx"><i class="fa fa-rocket"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Challan<br>
                        Confirmation<br>
                        Report</strong></p>
            </div>
        </div>
        <div class="col-xs-6 col-sm-3 col-md-2 col-lg-1 animated-circle-width">
            <div class="circle-wrap text-center animation-fadeIn">
                <div class="stats-circle circle-bg-color12">
                    <a href="ShortageReport.aspx"><i class="fa fa-road"></i></a>
                </div>
                <p class="text-center animation-pullDown">
                    <strong>Shortage<br>
                        Report</strong></p>
            </div>
        </div>
    </div>
    <!--DIV for Lorry Details-->
    <div id="HideDiv" runat="server" style="height: 400px;">
    </div>
    <div id="Upload_div" runat="server">
        <div class="panel-body" id="divlorry">
            <div class="col-lg-6">
                <section class="panel panel-in-default">  
            <div class="panel-body" style="overflow:auto;height:200px;">
            <strong style="border-bottom:1px solid #ccc;font-size: 18px;">Outstanding Report</strong> 
            
              	<asp:GridView ID="grdOutstandingRepPartyWise" runat="server" AutoGenerateColumns="false" BorderStyle="None"  Width="100%" GridLines="None" PageSize="25"
                            CssClass="display nowrap dataTable"  BorderWidth="0" OnRowCommand="grdMain_RowCommand">
                        <RowStyle CssClass="odd" />
                        <AlternatingRowStyle CssClass="even" />                                         
                        <Columns> 
                            <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="80">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                <ItemTemplate>
                                   <%#Eval("Party Name")%>      
                                </ItemTemplate>
                            </asp:TemplateField> 
                             
                            <asp:TemplateField HeaderText="Inv. Amnt" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Total Amount")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Recvd. Amnt" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Amount Received")%>
                                   <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=D", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("DONE")%>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="80">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Current Balance")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField>  
                            </Columns>
                            <emptydatarowstyle horizontalalign="Center" />
                            <emptydatatemplate>
                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                        </emptydatatemplate>            
                            </asp:GridView> 
            
              
            </div>
            </section>
            </div>
            <div class="col-lg-6">
                <section class="panel panel-in-default">  
            <div class="panel-body" style="overflow:auto;height:200px;">
            <strong style="border-bottom:1px solid #ccc;font-size: 18px;">Pending POD</strong> 
            
              	<asp:GridView ID="grdGrListChlnNotConfirm" runat="server" AutoGenerateColumns="false" BorderStyle="None"  Width="100%" GridLines="None" PageSize="25"
                            CssClass="display nowrap dataTable"  BorderWidth="0" OnRowCommand="grdMain_RowCommand">
                        <RowStyle CssClass="odd" />
                        <AlternatingRowStyle CssClass="even" />                                         
                        <Columns> 
                            <asp:TemplateField HeaderText="Gr. No" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                   <%#Eval("Gr_No")%>      
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Chln No" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Chln_No")%>
                                   <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=D", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("DONE")%>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Chln Date" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Chln_Date")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Gr Date" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="80">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("Gr_Date")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Lorry No" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="80">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80" />
                                <ItemTemplate>
                                <%#Eval("Lorry_No")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="80">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80" />
                                <ItemTemplate>
                                <%#Eval("Acnt_Name")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
                            </Columns>
                            <emptydatarowstyle horizontalalign="Center" />
                            <emptydatatemplate>
                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                        </emptydatatemplate>            
                            </asp:GridView> 
            
              
            </div>
            </section>
            </div>
            <br />
            <div class="col-lg-6">
                <section class="panel panel-in-default">  
            <div class="panel-body" style="overflow:auto;height:200px;">
            <strong style="border-bottom:1px solid #ccc;font-size: 18px;">DUE DATE DETAILS(LORRY)</strong> 
            
              	<asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None"  Width="100%" GridLines="None" PageSize="25"
                            CssClass="display nowrap dataTable"  BorderWidth="0" OnRowCommand="grdMain_RowCommand">
                        <RowStyle CssClass="odd" />
                        <AlternatingRowStyle CssClass="even" />                                         
                        <Columns> 
                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                       
                                   <asp:LinkButton ID="lmkBtnEdit" class="edit" runat="server" CommandArgument='<%#Eval("Date")%>' Text='<%#Eval("Date")%> '
                                                    CommandName="cmdedit" ToolTip="Show Detail"></asp:LinkButton>                      
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fitness" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                   <%#Eval("FIT")%>      
                                </ItemTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField HeaderText="Insurance" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("INS")%>
                                   <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=D", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("DONE")%>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="RC" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("RC")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                              <asp:TemplateField HeaderText="N.Permit" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("NAT")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="A.Permit" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                                <ItemTemplate>
                                <%#Eval("AUTH")%>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?EmpIdno={0}&Status=P", HttpUtility.UrlEncode(Eval("Dev Idno").ToString())) %>'  Text='<%#Eval("PENDING")%>' />  --%>                                
                                </ItemTemplate>
                            </asp:TemplateField> 
                            </Columns>
                            <emptydatarowstyle horizontalalign="Center" />
                            <emptydatatemplate>
                            <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                        </emptydatatemplate>            
                            </asp:GridView> 
            
              
            </div>
            </section>
            </div>
        </div>
    </div>
    <!-- popup form insurance detail -->
    <div id="Welcome_Msg" class="modal fade">
        <div class="modal-dialog" style="padding-left: 100px; padding-top: 80px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">
                        Welcome&nbsp;</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
                                  <div class="panel-body">
                                     <!-- First Row start -->
                                    <div class="clearfix odd_row" style="text-align:center;">                                
                                      <span> <asp:Label ID="lblWelcomeCaption" runat="server" Text=""></asp:Label> &nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="true" Text=""></asp:Label>.
                                      </span>
                                    </div> 
                                    <div class="clearfix odd_row" style="text-align:center;">Support nos. 7414853123, 7414856123, 7414857123, 7414858123</div>     
                                  </div>
                                </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtnInsuClose" runat="server" CssClass="btn btn-dark" OnClick="lnkbtnOk_OnClick"
                            data-dismiss="modal"><i class="fa fa-times">Ok</i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            SetFocus();
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            SetFocus();
            setDatecontrol();
            $("#btnClose").click(function (e) {
                HideDialog();
                e.preventDefault();
            });
        });
        $(document).ready(function () {
            setDatecontrol();
        });


        function openModal() {
            $('#Welcome_Msg').modal('show');
        }

        function closeModal() {
            $('#Welcome_Msg').modal('close');
        }

        function closepopup() {
            $('#Upload_div').modal('hide');
        }
    </script>
    <%--  <div class="row-fluid">
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div id="page" class="dashboard">
            <!-- BEGIN OVERVIEW STATISTIC BLOCKS-->
            <div class="row-fluid circle-state-overview">
            </div>
                <!-- END OVERVIEW STATISTIC BLOCKS-->
                <div class="row-fluid">
                </div>
                <!-- BEGIN SQUARE STATISTIC BLOCKS-->
                <div class="square-state">
                    <div class="row-fluid">
                       <a class="icon-btn span2" href="ItemMaster.aspx"><i class="icon-sitemap""></i>
                                <div>Item Master</div></a>
                       <a class="icon-btn span2" href="LedgerMaster.aspx"><i class="icon-list-alt"></i>
                                <div>Ledger Master</div></a>
                       <a class="icon-btn span2" href="RateMaster.aspx?FTyp=TBB"><i class="icon-bar-chart"></i>
                                <div>Rate Master</div></a>
                       <a class="icon-btn span2" href="GRPrep.aspx"><i class="icon-paste"></i>
                                <div>GR Preparation</div></a>
                       <a class="icon-btn span2" href="ChlnBooking.aspx"><i class="icon-book"></i>
                                <div>Challan Booking</div></a>
                       <a class="icon-btn span2" href="Challanconfirmation.aspx"><i class="icon-thumbs-up"></i>
                                <div>Challan Confirmation</div></a>
                    </div>
                    <div class="row-fluid">
                        <a class="icon-btn span2" href="Invoice.aspx"><i class="icon-tablet"></i>
                                <div>Invoice Generation</div></a>
                        <a class="icon-btn span2" href="ChallanDelverd.aspx"><i class="icon-truck"></i>
                                <div>Delivery Register</div></a>
                        <a class="icon-btn span2" href="#"><i class="icon-file-alt"></i>
                                <div>Delivery Challan</div></a>
                        <a class="icon-btn span2" href="AccountBookRpt.aspx"><i class="icon-money"></i>
                                <div>Accounts Book</div></a>
                        <a class="icon-btn span2" href="ChallanConfirmationRep.aspx"><i class="icon-list"></i>
                                <div>Challan Confirmation Report</div></a>
                        <a class="icon-btn span2" href="ShortageReport.aspx"><i class="icon-envelope"></i>
                                <div>Shortage Report</div></a>
                    </div>
                </div>
            <!-- END SQUARE STATISTIC BLOCKS-->
            <div class="row-fluid">
            </div>
            <div>
                <img src="img/alila/transport truck.jpg" width="700px" height="800px" style="padding-top: 15px" />
                <div class="notific">
                    <!-- BEGIN NOTIFICATIONS PORTLET-->
                    <div class="widget">
                        <div class="widget-title">
                            <h4>
                                <i class="icon-bell"></i>Notifications</h4>
                            <span class="tools"><a href="javascript:;" class="icon-chevron-down"></a><a href="javascript:;"
                                class="icon-remove"></a></span>
                        </div>
                        <div class="widget-body">
                            <ul class="item-list scroller padding" data-height="365" data-always-visible="1">
                                <li><span class="label label-success"><i class="icon-bell"></i></span><span>New user
                                    registered.</span> <span class="small italic">Just now</span> </li>
                                <li><span class="label label-success"><i class="icon-bell"></i></span><span>New order
                                    received.</span> <span class="small italic">15 mins ago</span> </li>
                                <li><span class="label label-warning"><i class="icon-bullhorn"></i></span><span>Alerting
                                    a user account balance.</span> <span class="small italic">2 hrs ago</span> </li>
                                <li><span class="label label-important"><i class="icon-bolt"></i></span><span>Alerting
                                    administrators staff.</span> <span class="small italic">11 hrs ago</span> </li>
                                <li><span class="label label-important"><i class="icon-bolt"></i></span><span>Messages
                                    are not sent to users.</span> <span class="small italic">14 hrs ago</span> </li>
                                <li><span class="label label-warning"><i class="icon-bullhorn"></i></span><span>Web
                                    server #12 failed to relosd.</span> <span class="small italic">2 days ago</span>
                                </li>
                                <li><span class="label label-success"><i class="icon-bell"></i></span><span>New order
                                    received.</span> <span class="small italic">15 mins ago</span> </li>
                                <li><span class="label label-warning"><i class="icon-bullhorn"></i></span><span>Alerting
                                    a user account balance.</span> <span class="small italic">2 hrs ago</span> </li>
                                <li><span class="label label-important"><i class="icon-bolt"></i></span><span>Alerting
                                    administrators staff.</span> <span class="small italic">11 hrs ago</span> </li>
                            </ul>
                            <div class="space5">
                            </div>
                            <a href="#" class="pull-right">View all notifications</a>
                            <div class="clearfix no-top-space no-bottom-space">
                            </div>
                        </div>
                    </div>
                    <!-- END NOTIFICATIONS PORTLET-->
                </div>
            </div>
        </div>
    </div>
    <!-- END PAGE CONTENT-->--%>
    <script>
        $(document).ready(function () {
            $(".autocomplete-search ").focus();
         });
    </script>
</asp:Content>
