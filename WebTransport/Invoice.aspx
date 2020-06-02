<%@ Page Title="Invoice" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="Invoice.aspx.cs" Inherits="WebTransport.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/HintPopup.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row ">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <section class="panel panel-default full_form_container quotation_master_form">
                <header class="panel-heading font-bold form_heading">INVOICE
                    <%--PRINT BUTTONS--%>
                    <span class="view_print"><asp:LinkButton ID="lnkBtnLast" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtnLast_Click">LAST PRINT</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="ManageInvoice.aspx"><asp:Label ID="lblViewList" runat="server" Text="LIST"></asp:Label></a>&nbsp;
                         <asp:ImageButton ID="imgPrint" runat="server" AlternateText="Print" ImageUrl="~/images/print.jpeg"
                        Visible="false" title="Print" OnClientClick="return CallPrint('print');" Height="16px" />
                        <asp:ImageButton ID="ImgPrint1" runat="server" AlternateText="Print General" ImageUrl="~/images/print.jpeg"
                        Visible="false" title="Print General" OnClientClick="return CallPrint('print1');"
                        Height="16px" />
                        <asp:ImageButton ID="ImgPrint2" runat="server" AlternateText="Print Jain Bulk" ImageUrl="~/images/print.jpeg"
                        Visible="false" title="Print Jain Bulk" OnClientClick="return CallPrint('Jainprint');"
                        Height="16px" />
                         <asp:ImageButton ID="ImgPrint3" runat="server" AlternateText="Print Jain Bulk" ImageUrl="~/images/print.jpeg"
                        Visible="false" title="Print Jain Bulk" OnClientClick="return CallPrint('JainSingle');"
                        Height="16px" />
                          <asp:ImageButton ID="ImgPrintMultipal" runat="server" 
                            AlternateText="Print Format GCA" ImageUrl="~/images/print.jpeg"
                        Visible="false" title="Print Format GCA" 
                        Height="16px" onclick="ImgPrintMultipal_Click" />
           
                <asp:LinkButton ID="lnkbtnPrint" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="Print" AlternateText="Print" title="Print" Height="16px" Onclick="lnkbtnPrint_Click"></asp:LinkButton>
                 <asp:LinkButton ID="lnkprint111" CssClass="fa fa-print icon" Visible="false" runat="server" ToolTip="Print11" AlternateText="Print" title="Print11" Height="16px" Onclick="lnkinvoicePrint_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkbtnpr" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtn1_Click" Visible="false" > Tax invoice</asp:LinkButton>
                 <asp:LinkButton ID="lnkbillinvoice" class="view_print"  runat="server"  AlternateText="Print" title="Print" Height="16px" onclick="lnkBtn1_Click" Visible="false" > Bill invoice</asp:LinkButton>
                 <asp:LinkButton ID="lnkbtn" CssClass="fa fa-print icon"  runat="server" ToolTip="BirlaTaxInvoice" AlternateText="Print" title="PrintInvoice" Height="16px" Onclick="lnkBirlataxinvoicePrint_Click" Visible="false" ></asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" CssClass="fa fa-print icon"  runat="server" ToolTip="BirlaBILL" AlternateText="Print" title="Print11" Height="16px" Onclick="lnkinvoicePrint_Click1" Visible="false" ></asp:LinkButton>
                <asp:LinkButton ID="lnkprintInvoice" CssClass="fa fa-print icon"  runat="server" ToolTip="BirlaTax" AlternateText="Print" title="PrintInvoice" Height="16px" Onclick="lnktaxinvoicePrint_Click" Visible="false" ></asp:LinkButton>
                 <asp:LinkButton ID="lnktaxinvoice" CssClass="fa fa-print icon"  runat="server" ToolTip="PJLtaxinvoice" AlternateText="Print" title="PrinttaxInvoice" Height="16px" Onclick="lnkPrintTax_Click" Visible="false" ></asp:LinkButton>
                 <asp:LinkButton ID="lnkpjlbillinvoice" CssClass="fa fa-print icon"  runat="server" ToolTip="PJLBillinvoice" AlternateText="Print" title="PrintBillInvoice" Height="16px" Onclick="lnkPJLBillInvoice_Click" Visible="false" ></asp:LinkButton>
                 <asp:LinkButton ID="lnkpjlbillSummary" CssClass="fa fa-print icon"  runat="server" ToolTip="PJLBillSummary" AlternateText="Print" title="PrintBillSummary" Height="16px" Onclick="lnkPJLBillSummary_Click" Visible="false" ></asp:LinkButton>
                    </span>
                </header>
                <div class="panel-body">
                <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                    <div class="clearfix first_section">
                        <section class="panel panel-in-default">  
                        <div class="panel-body">
                            <div class="clearfix odd_row">
                            <div class="col-sm-4">
                                <label class="col-sm-4 control-label">Date Range<span class="required-field">*</span></label>
                                <div class="col-sm-8">
                                <asp:DropDownList ID="ddldateRange" runat="server" AutoPostBack="true" CssClass="form-control"
                                    Height="30px" OnSelectedIndexChanged="ddldateRange_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddldateRange"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select Year!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                            </div>
                            <div class="col-sm-5">
                                <label class="col-sm-2 control-label">Inv. No.<span class="required-field">*</span></label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtInvPreIx" runat="server" PlaceHolder="Prf." CssClass="form-control" MaxLength="16"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    Text="" OnTextChanged="txtInvPreIx_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>

                                <div class="col-sm-2">
                                <asp:TextBox ID="txtinvoicNo" runat="server" PlaceHolder="Invoice No." CssClass="form-control" Height="24px" MaxLength="7"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtinvoicNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="&lt;br/&gt;Please enter Invoice no!"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-3">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="input-sm datepicker form-control" Height="24px" MaxLength="50" PlaceHolder="DD-MM-YYYY"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    CssClass="redfont" Display="Dynamic" ErrorMessage="&lt;br/&gt;Please enter Date!" ForeColor="Red"
                                    SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-5 control-label">Loc. [From]<span class="required-field">*</span></label>
                                <div class="col-sm-7">
                                <asp:DropDownList ID="ddlFromCity" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFromCity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFromCity"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                              
                            </div>
                            </div>         
                            <div class="clearfix even_row">
                            <div class="col-sm-4">
                                <label class="col-sm-4 control-label">Billing Party<span class="required-field">*</span></label>
                                <div class="col-sm-7">
                                <asp:DropDownList ID="ddlSenderName" runat="server" AutoPostBack="true" CssClass="form-control" onselectedindexchanged="ddlSenderName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSenderName"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please select From city!"
                                    InitialValue="0" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-1" style="margin-top:4px;">
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Height="22px" class="btn btn-sm btn-primary acc_home"><i class="fa fa-file"></i></asp:LinkButton>
                                </div>
                            </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 control-label">Delivery Address</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtDelvAddress" runat="server" PlaceHolder="Delivery Address" CssClass="form-control" 
                                        onDrop="blur();return false;" 
                                        Text=""></asp:TextBox>                          
                                    </div>
                                </div>
                              <div class="col-sm-3" id="DivPrintFormat" visible="false" runat="server">
                             <label class="col-sm-4 control-label" style="padding-left:5px"><asp:Label ID="Label16" runat="server" Text="Print"></asp:Label></label>
                               <div class="col-sm-8">
                                <asp:DropDownList ID="ddlPrintLoc" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1" id="DivApprov" visible="false" runat="server">
                             <label class="col-sm-9 control-label"><asp:Label ID="lblApproved" runat="server" Text="Appro."></asp:Label></label>
                               <div class="col-sm-1">
                                <asp:CheckBox ID="chkApproved" runat="server"></asp:CheckBox>
                                </div>
                            </div>
                             <div class="col-sm-4">
                             <label class="col-sm-4 control-label"><asp:Label ID="lblPrintType" runat="server" Text="Print Type"></asp:Label></label>
                               <div class="col-sm-8">
                                <asp:DropDownList ID="drpPrintType" runat="server" CssClass="form-control" AutoPostBack="true" 
                                       onselectedindexchanged="drpPrintType_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="With Shortage"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Without Shortage"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
              
                            </div>
                            <div class="clearfix third_right">
                            <section class="panel panel-in-default">
                                <div class="clearfix even_row relative-box">
                                <div class="addmoreinoutdetail"><i class="fa fa-plus h-v-center"></i></div>
                            <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Plant In/Out Date</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPlantInDate" runat="server" 
                                        CssClass="input-sm datepicker form-control"  MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                        onDrop="blur();return false;" onpaste="return false" 
                                        data-date-format="dd-mm-yyyy" Text="" 
                                        onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static" 
                                        AutoPostBack="true" ontextchanged="txtPlantInDate_TextChanged" ></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPlantOutDate" runat="server" CssClass="input-sm datepicker form-control"  MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false" data-date-format="dd-mm-yyyy" Text=""  AutoPostBack="true" ontextchanged="txtPlantOutDate_TextChanged" onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                 <div class="col-sm-1" style="margin-top:4px;">
                                    <a class="btn btn-sm btn-primary acc_home" onclick="ClearAllTextFields($(this));$('#txtPlantDays').val('');$('#txtPlantAmount').val('')"><i class="fa fa-eraser"></i></a>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Port In/Out Date</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPortinDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false" 
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)" AutoPostBack="true" ontextchanged="txtPortinDate_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPortoutDate" runat="server" CssClass="input-sm datepicker form-control" MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false"
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)" AutoPostBack="true" ontextchanged="txtPortoutDate_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                 <div class="col-sm-1" style="margin-top:4px;">
                                    <a class="btn btn-sm btn-primary acc_home" onclick="ClearAllTextFields($(this));$('#txtPortDays').val('');$('#txtPortAmount').val('')"><i class="fa fa-eraser"></i></a>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Plant In/Out Amount</label>
                                <div class="col-sm-3">
                               <asp:TextBox ID="txtPlantDays" runat="server" PlaceHolder="Plant Days" CssClass="form-control pp-days" Height="24px" MaxLength="3"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                    Text="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">  <asp:TextBox ID="txtPlantAmount" runat="server" PlaceHolder="Plant Amount" CssClass="form-control pp-amount" Height="24px" MaxLength="15"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" ClientIDMode="Static"></asp:TextBox></div>
                            </div>
                            <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Port In/Out Amount</label>
                                <div class="col-sm-3">
                                     <asp:TextBox ID="txtPortDays" runat="server" PlaceHolder="Port Days" CssClass="form-control pp-days" Height="24px" MaxLength="3"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                    Text=""  ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3"> <asp:TextBox ID="txtPortAmount" runat="server" PlaceHolder="Port Amount" CssClass="form-control pp-amount" Height="24px" MaxLength="15"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" ClientIDMode="Static"></asp:TextBox></div>
                            </div>
                            </div>
                            </section>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            
                            <div class="fadeout-box">
                            <section id="addmoreinoutdetail" class="panel panel-in-default h-v-center">
                                <div class="collapse-header"><h3>Add In/Out Details <i class="fa fa-close close-popup pull-right"></i></h3></div>
                                <div class="clearfix even_row">
                                    <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Plant In/Out Date</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPlantInDate2" runat="server" 
                                        CssClass="input-sm datepicker form-control"  MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false" 
                                    data-date-format="dd-mm-yyyy" Text=""   AutoPostBack="true" ontextchanged="txtPlantinDate2_TextChanged" 
                                        onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPlantOutDate2" runat="server" CssClass="input-sm datepicker form-control"  MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false"
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)"  AutoPostBack="true" ontextchanged="txtPlantoutDate2_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                 <div class="col-sm-1" style="margin-top:4px;">
                                    <a class="btn btn-sm btn-primary acc_home" onclick="ClearAllTextFields($(this));$('#txtPlantDays2').val('');$('#txtPlantAmount2').val('')"><i class="fa fa-eraser"></i></a>
                                </div>
                            </div>
                                    <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Port In/Out Date</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPortinDate2" runat="server" CssClass="input-sm datepicker form-control"  MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false"
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)"  AutoPostBack="true" ontextchanged="txtPortinDate2_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPortoutDate2" runat="server" CssClass="input-sm datepicker form-control"  AutoPostBack="true" ontextchanged="txtPortOutDate2_TextChanged" MaxLength="10" PlaceHolder="DD-MM-YYYY"
                                    onDrop="blur();return false;" onpaste="return false"
                                    data-date-format="dd-mm-yyyy" Text="" onkeydown = "return DateFormat(this, event.keyCode)" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                 <div class="col-sm-1" style="margin-top:4px;">
                                    <a class="btn btn-sm btn-primary acc_home" onclick="ClearAllTextFields($(this));$('#txtPortDays2').val('');$('#txtPortAmount2').val('')"><i class="fa fa-eraser"></i></a>
                                </div>
                            </div>
                                    <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Plant In/Out Amount</label>
                                <div class="col-sm-3">
                               <asp:TextBox ID="txtPlantDays2" runat="server" PlaceHolder="Plant Days" CssClass="form-control" Height="24px" MaxLength="3"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    Text=""  ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">  <asp:TextBox ID="txtPlantAmount2" runat="server" PlaceHolder="Plant Amount" CssClass="form-control pp-days" Height="24px" MaxLength="15"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                    Text=""  ClientIDMode="Static"></asp:TextBox></div>
                            </div>
                                    <div class="col-sm-6">
                                <label class="col-sm-4 control-label">Port In/Out Amount</label>
                                <div class="col-sm-3">
                                     <asp:TextBox ID="txtPortDays2" runat="server" PlaceHolder="Port Days" CssClass="form-control" Height="24px" MaxLength="3"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                    Text=""  ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-3"> <asp:TextBox ID="txtPortAmount2" runat="server" PlaceHolder="Port Amount" CssClass="form-control pp-amount" Height="24px" MaxLength="15"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                    Text=""  ClientIDMode="Static"></asp:TextBox></div>
                            </div>
                                </div>
                            </section>
                            </div>

                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                        </section>                        
                    </div>
                    
                    <!-- second  section -->
                    <div class="clearfix third_right">
                    <section class="panel panel-in-default">                            
                    <div class="panel-body" style="overflow:auto">     
                            <asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                Width="100%" GridLines="Both" EnableViewState="true" AllowPaging="true" BorderWidth="0"
                                ShowFooter="true" OnDataBound="grdMain_DataBound" OnRowDataBound="grdMain_RowDataBound" OnRowCommand="grdMain_RowCommand" PageSize="500">
                                <RowStyle CssClass="odd" />
                                <AlternatingRowStyle CssClass="even" />    
                                <Columns>
                                  <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                        <ItemTemplate> 
                                        <asp:LinkButton ID="lnkbtnRemove" CssClass="delete" runat="server" CommandArgument='<%#Eval("Gr_Idno") %>' OnClientClick="return confirm('Do you want to remove this record ?');" CommandName="cmdremove" ToolTip="Click to remove"><i class="fa fa-trash-o"></i></asp:LinkButton>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr No." HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNo" runat="server" Text='<%#Convert.ToString(Eval("Gr_No")) %>'></asp:Label>
                                            <asp:HiddenField runat="server" Value='<%#Eval("Gr_Idno")%>' ID="hidGrIdno" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Date" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delv.Place" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Delvry_Place"))%>
                                            <asp:HiddenField runat="server" Value='<%#Eval("Delvry_Place")%>' ID="hidDeliveryPlace" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Via City" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("CityVia_Name"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Item_Name"))%>
                                            <asp:HiddenField runat="server" Value='<%#Eval("Item_Idno")%>' ID="hidItemIdno" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="50" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Unit"))%>
                                            <asp:HiddenField runat="server" Value='<%#Eval("Unit_Idno")%>' ID="hidUnitIdno" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <FooterTemplate>
                                            Total:
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="45" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Qty"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotQty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="80" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="45" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("Weight"))%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotWeight" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemRate" runat="server" Text='<%#Convert.ToString(Eval("Rate"))%>'></asp:Label>
                                            <asp:HiddenField ID="hidGrtype" runat="server" Value='<%#Convert.ToString(Eval("GrTypeId"))%>'></asp:HiddenField>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotRate" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Convert.ToDouble(Eval("Amount")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STax Paid By" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTaxpaidBy" runat="server" Text='<%#Convert.ToString(Eval("STax_Typ"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Wages" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWagesAmnt" runat="server" Width="70px" AutoPostBack="true" Text='<%#Convert.ToDouble(Eval("Wages_Amnt")).ToString("N2")%>'
                                                MaxLength="9" OnTextChanged="txtWagesAmnt_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotWages" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Amount" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="100" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOtherAmnt" runat="server" Width="70px" AutoPostBack="true" Text='<%#Convert.ToDouble(Eval("Other_Amnt")).ToString("N2")%>'
                                                MaxLength="9" OnTextChanged="txtOtherAmnt_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblOtherAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amnt" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("Net_Amnt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotNetAmnt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serv. Tax" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblServTaxAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("ServTax_Amnt")).ToString("N2")%>'></asp:Label>
                                            <asp:HiddenField runat="server" Value='<%#Eval("ServTax_Perc")%>' ID="hidServTaxPerc" />
                                            <asp:HiddenField runat="server" Value='<%#Eval("ServTax_Valid")%>' ID="hidServTaxValid" />
                                            <asp:HiddenField runat="server" Value='<%#Eval("TBB_Rate")%>' ID="hidTbbType" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblServTaxAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SwchBrt Cess" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" Value='<%#Eval("SwchBrtTax_Perc")%>' ID="hidSwchBrtTaxPerc" />
                                            <asp:Label ID="lblSwchBrtTaxAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("SwchBrtTax_Amt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblSwchBrtTaxAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kisan Kalyan Cess" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" Value='<%#Eval("KisanTax_Perc")%>' ID="HiddKisanTax" />
                                            <asp:Label ID="lblKisanTaxAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("KisanKalyan_Amnt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblKisanTaxAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" Value='<%#Eval("SGST_Per")%>' ID="HidSGST" />
                                            <asp:Label ID="lblSGSTAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("SGST_Amt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblSGSTAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" Value='<%#Eval("CGST_Per")%>' ID="HidCGST" />
                                            <asp:Label ID="lblCGSTAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("CGST_Amt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblCGSTAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="50" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" Value='<%#Eval("IGST_Per")%>' ID="HidIGST" />
                                            <asp:Label ID="lblIGSTAmnt" runat="server" Text='<%#Convert.ToDouble(Eval("IGST_Amt")).ToString("N2")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblIGSTAmntNet" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField   HeaderText="Ref No" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="50" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("Ref_No"))) ? "" : (Convert.ToString((Eval("Ref_No")))))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                    <EmptyDataTemplate>
                                    Records(s) not found.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            </div>
                        </section>
                    </div> 
                    <section class="panel panel-in-default">                            
                        <div class="panel-body alternate-rows">     
                            <div class="clearfix">
                                <div class="col-sm-4">
                                    <label class="col-sm-7 control-label">Gross Total</label>
                                    <div class="col-sm-5">
                                    <asp:TextBox ID="txtGrosstotal" runat="server" CssClass="form-control" Enabled="false" Height="24px"
                                            MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;" Style="text-align:right;" 
                                            onpaste="return false" Text="0.00" Width="81px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                <label class="col-sm-7 control-label">Other Amnt.</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtOtherAmount" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" Style="text-align:right;"
                                    Text="0.00" AutoPostBack="true" Width="81px" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                </div>
                                <div class="col-sm-4">
                                  <label class="col-sm-7 control-label">Bility/O.Charges</label>
                                    <div class="col-sm-3">
                                    <asp:TextBox ID="txtBiltyCharges" runat="server" CssClass="form-control" Height="24px" MaxLength="9"
                                            oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                            Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" OnTextChanged="txtBiltyCharges_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:LinkButton ID="lnkCharges" runat="server" OnClick="lnkCharges_OnClick"  class="btn btn-sm btn-primary acc_home"><i class="fa fa-file"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                                <div class="col-sm-12 tax-calculation">
                                <%--U-HINT--%>
                                <div class="col-sm-4 u-show-hint">
                                    <label class="col-sm-7 control-label">Serv.Tax [Consigner]</label>
                                    <div class="col-sm-5">
                                    <asp:TextBox ID="txtCServTax_Total" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <i class="fa fa-info"></i>
                                    <%--<HINT DETAILS--%>
                                    <div class="hint-detail get-total">
                                        <div class="hint-heading">Tax Details [Consigner]</div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Service Tax</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtCSServTax" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Swchbhrt Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtCSSwchBrtTax" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                        Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Kisan Kalyn Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtKisanTax" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <%--U-HINT--%>
                                <div class="col-sm-4 u-show-hint">
                                    <label class="col-sm-7 control-label">Serv.Tax [Transporter]</label>
                                    <div class="col-sm-5">
                                    <asp:TextBox ID="txtTServTax_Total" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <i class="fa fa-info"></i>
                                    <%--<HINT DETAILS--%>
                                     <div class="hint-detail get-total">
                                        <div class="hint-heading">Tax Details [Transporter]</div>
                                          <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Service Tax</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtTrServTax" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Swchbhrt Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtTrSwchBrtTax" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">Kisan Kalyn Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtKisanTaxTrnptr" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>	
                                    </div>
                                </div>
                                </div>
                                <div class="col-sm-12 gst-calculation">
                                    <%--U-HINT--%>
                                    <div class="col-sm-4 u-show-hint">
                                    <label class="col-sm-7 control-label">GST [Consigner]</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtCGST_Total" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                            oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                            Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <i class="fa fa-info"></i>
                                    <%--HINT DETAILS--%>
                                    <div class="hint-detail get-total">
                                        <div class="hint-heading">GST Details [Consigner]</div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">SGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtC_SGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">CGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtC_CGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 sum-value">
                                            <label class="col-sm-8 control-label">IGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtC_IGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-8 control-label">GST Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtC_GST_Cess" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>	
                                    </div>	 
                                </div>
                                <%--U-HINT--%>
                                <div class="col-sm-4 u-show-hint">
                                    <label class="col-sm-7 control-label">GST [Transporter]</label>
                                    <div class="col-sm-5">
                                    <asp:TextBox ID="txtTGST_Total" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                            oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                            Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <i class="fa fa-info"></i>
                                    <%--HINT DETAILS--%>
                                    <div class="hint-detail get-total">
                                    <div class="hint-heading">GST Details [Transporter]</div>
                                        <div class="col-sm-12 sum-value">
                                            <label class="col-sm-8 control-label">SGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtT_SGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 sum-value">
                                            <label class="col-sm-8 control-label">CGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtT_CGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 sum-value">
                                            <label class="col-sm-8 control-label">IGST</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtT_IGST" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 sum-value">
                                            <label class="col-sm-8 control-label">GST Cess</label>
                                            <div class="col-sm-4">
                                            <asp:TextBox ID="txtT_GSTCess" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50"
                                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                    Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div> 
                                    </div>  
                                    </div>    
                                </div>   
                                <div>
                                    <label class="col-sm-7 control-label">GST [Transporter]</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtChallanAmountOnToPay" CssClass="form-control" Height="24px" MaxLength="50"
                                            oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                            Style="width: 81px;text-align:right;" Text="0.00" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                </div>                 
                            </div>
                            <div class="clearfix">
                            <%--U-HINT--%>
                            <div class="col-sm-4 u-show-hint">
                                <label class="col-sm-7 control-label">Shortage Amnt. </label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtShoratageDetail" runat="server" CssClass="form-control" Height="24px" MaxLength="50" oncopy="return false" 
                                            oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                            Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>

                                <i class="fa fa-info"></i>
                                <%--U-HINT--%>
                                <div class="hint-detail get-total">
                                    <div class="hint-heading">Shortage Details</div>
                                    <div class="col-sm-12">
                                     <label class="col-sm-8 control-label">Shortage Amnt.</label>
                                        <div class="col-sm-4">
                                        <asp:TextBox ID="txtShortageAmnt" runat="server" CssClass="form-control sum-value" Height="24px" MaxLength="50" oncopy="return false" 
                                                    oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                                    Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                        <div class="col-sm-12">
                                        <label class="col-sm-8 control-label">Shortage GST</label>
                                        <div class="col-sm-4">
                                        <asp:TextBox ID="txtShortageGSTAmnt" runat="server" CssClass="form-control col-sm-4" Height="24px" MaxLength="50"
                                                oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                                Style="width:width:52px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-7 control-label">RoundedOff</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtRoundOff" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                    oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                    Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                               <label class="col-sm-7 control-label">Inv.Amount</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtNetAmnt" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" 
                                        Style="width: 81px;text-align:right;" Text="0.00" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>			                              
                        </div>
                            <div class="clearfix">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-7 control-label">Prv. Rcpt Amnt</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtPrevRcptt" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        Style="width: 81px;text-align:right;" Text="0.00" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                             <label class="col-sm-7 control-label">Receivable Amnt</label>
                                <div class="col-sm-5">
                                <asp:TextBox ID="txtRecivable" runat="server" CssClass="form-control" Height="24px" MaxLength="50"
                                        oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false"
                                        Style="width: 81px;text-align:right;" Text="0.00" Enabled="false"></asp:TextBox>
                                </div>
                            </div>	
                        </div>
                        </div>
                    </section>
                    <!-- fourth row -->
                    <div class="clearfix odd_row">
                <div class="col-lg-2">
                </div>
                <div style="width:100%; float:left">
                    <div class="col-sm-2">
                        <asp:LinkButton ID="lnkbtnNew" runat="server" CausesValidation="false" CssClass="btn full_width_btn btn-s-md btn-info"
                            OnClick="lnkbtnNew_OnClick"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                    </div>
                    <div class="col-sm-2">
                        <a onclick="$('#divSave').fadeIn(300);$('#divSave').parent('div').fadeIn(100);" id="LinkButton2" runat="server" class="btn full_width_btn btn-s-md btn-success"> <i class="fa fa-save"></i>Save As</a>
                        <asp:LinkButton ID="lnkBtnUpd" runat="server" CssClass="btn full_width_btn btn-s-md btn-success"
                                        OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="Submit" Visible="false"> <i class="fa fa-save"></i>Save</asp:LinkButton>
                            <asp:HiddenField ID="hidPartyStateIdno" runat="server" Value="" />
                            <asp:HiddenField ID="hidGstType" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="hidPartyStateName" runat="server" Value="" />
                        <asp:HiddenField ID="hidid" runat="server" Value="" />
                        <asp:HiddenField ID="Hidrowid" runat="server" Value="" />
                        <asp:HiddenField ID="hideUserPref" runat="server" Value="" />
                        <asp:HiddenField ID="HidInvoiceTyp" runat="server" />
                        <asp:HiddenField ID="hidmindate" runat="server" />
                        <asp:HiddenField ID="hidmaxdate" runat="server" />
                        <asp:HiddenField ID="hidServTaxLimit" runat="server" Value="" />
                        <asp:HiddenField ID="HidUserprefBilty" runat="server" Value="" />
                        <asp:HiddenField ID="HidwagesUserpref" runat="server" Value="" />
                        <asp:HiddenField ID="HidUserprefSurchrge" runat="server" />
                        <asp:HiddenField ID="HidUserprefTollTax" runat="server" />
                        <asp:HiddenField ID="hidServTaxPercnt" runat="server" Value="" />
                        <asp:HiddenField ID="hidSwchBrtTaxPercnt" runat="server" Value="" />
                        <asp:HiddenField ID="HidTbbRate" runat="server" Value="" />
                        <asp:HiddenField ID="hidpostingmsg" runat="server" />
                        <asp:HiddenField ID="hidPrintType" runat="server" />
                        <asp:HiddenField ID="hidrefno" runat="server" />
                        <asp:HiddenField ID="HiddKisanTax" runat="server" />
                        <asp:HiddenField ID="hidjainbulk" runat="server" />
                        <asp:HiddenField ID="hidAdminApp" runat="server" />
                        <asp:HiddenField ID="hidePrintMultipal" runat="server" />
                        <asp:HiddenField ID="HidGrType" runat="server" Value="" />
                        <asp:HiddenField ID="HidGrId" runat="server" Value="" />
                        <asp:HiddenField ID="hidGrIdnoForChallan" runat="server" Value="" />
                        <asp:HiddenField ID="hidLessChallanAmnt" runat="server" Value="" />
                    </div>
                    <div class="col-sm-2">
                        <asp:LinkButton ID="lnkbtnCancel" runat="server" CssClass="btn full_width_btn btn-s-md btn-danger"
                            OnClick="lnkbtnCancel_OnClick"><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                    </div>
                    <div class="col-sm-2" id="divPosting" runat="server">
                        <button type="button" id="btnAccPost" runat="server" class="btn full_width_btn btn-s-md btn-info"
                            style="height: 32px" data-toggle="modal" data-target="#acc_posting">
                            <i class="fa fa-th-list">Acc Posting</i></button>
                    </div>
                  <%--  <div class="col-sm-2" id="check" runat="server">
                    <asp:CheckBox ID="CheckRCM" runat="server" Text="Tax Payable Under RCM"></asp:CheckBox>
                    </div>--%>
                    <div class="col-sm-1"></div>
                    <div class="col-sm-1">
                        <div title="User Preference setting" class="btn-setting"></div>
                    </div>
                </div>
            </div>
                    <!-- popup form GR detail -->
                    <div id="gr_details_form" class="modal fade">
                <div class="modal-dialog" style="width: 60%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="popform_header">
                                GR Detail
                            </h4>
                        </div>
                        <div class="modal-body">
                            <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">
									        <!-- First Row start -->
								        <div class="clearfix odd_row">	                                
	                                        <div class="col-sm-4">
	                                            <label class="col-sm-4 control-label">Date From</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="input-sm datepicker form-control" Width="140"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRcptEntryDtFrm" runat="server" ErrorMessage="Enter From Date!"
                                                        Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateFrom" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                                </div>
	                                        </div>
	                                        <div class="col-sm-4">
	                                                <label class="col-sm-4 control-label">Date To</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="input-sm datepicker form-control" Width="140"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRcptEntryDtTo" runat="server" ErrorMessage="Enter To Date!"
                                                        Display="Dynamic" CssClass="redfont" ControlToValidate="txtDateTo" ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>
                                                </div>
	                                        </div>
                                            <div class="col-sm-4" style="padding: 0;">
	                                            <div class="col-sm-4 prev_fetch">
                                                    <asp:LinkButton ID="lnkbtnPreview"  OnClick="lnkbtnPreview_Click"  ValidationGroup="RcptEntrySrch" runat="server" class="btn full_width_btn btn-sm btn-primary"><i class="fa fa-search"></i>Preview</asp:LinkButton>
	                                            </div>
	                                            <div class="col-sm-8"> 
	                                                <asp:Label ID="lblTotalRecord" runat="server" Text="T. Record(s) : 0" CssClass="control-label" ></asp:Label>                                   
	                                            </div>
	                                        </div>
	                                    </div> 

	                    <div class="clearfix even_row" style="display:none">
	                            <div class="col-sm-4">
	                                <label class="col-sm-4 control-label">Billing Party</label>
		                            <div class="col-sm-8">
		                                <asp:DropDownList ID="ddlSnder" runat="server" CssClass="form-control" AutoPostBack="False">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvRcptEntryPrty" runat="server" ErrorMessage="</Br>Please select Sender name"
                                            Display="Dynamic" InitialValue="0" CssClass="classValidation" ControlToValidate="ddlSnder"
                                            ValidationGroup="RcptEntrySrch"></asp:RequiredFieldValidator>--%>
		                            </div>
	                            </div>
                                <div class="col-sm-4">
                                <label class="col-sm-4 control-label">Gr Type</label>
                                <div class="col-sm-8">
                                <asp:DropDownList ID="ddlGrType" runat="server" CssClass="form-control" AutoPostBack="False">
                                <asp:ListItem Text="Gr Prepation" Value="GR" ></asp:ListItem>
                                <asp:ListItem Text="Gr Retailer" Value="GRR" ></asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                </div>
	                    </div> 
                        <div class="clearfix third_right">
                             <section class="panel panel-in-default">                          
                            <div class="panel-body" style="overflow:auto;height:450px" >     
                                <asp:GridView ID="grdGrdetals" runat="server" AutoGenerateColumns="false" BorderStyle="None" CssClass="display nowrap dataTable"
                                    Width="100%" GridLines="Both" EnableViewState="true" BorderWidth="0"
                                    ShowFooter="true">
                                    <RowStyle CssClass="odd" />
                                    <AlternatingRowStyle CssClass="even" />    
                                    <Columns>
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Select">
                                                <HeaderStyle HorizontalAlign="Center" Width="40" />
                                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                        CssClass="SACatA" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" HeaderText="Gr No.">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemTemplate>
                                                    <%#Convert.ToString(Eval("Gr_No"))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px"
                                                HeaderText="Gr Date">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemTemplate>
                                                    <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MMM-yyyy")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Type">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOdrNo" runat="server" Text=' <%#Eval("GR_Agnst")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Delivery Place">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#Eval("Delvry_Place")%>
                                                    <asp:HiddenField ID="hidGrIdno" runat="server" Value='<%#Eval("Gr_Idno")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" HeaderText="Via City">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#Eval("ViaCity_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100px"
                                                HeaderText="Wages">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemTemplate>
                                                    <%#Eval("Wages_Amnt")%>
                                                    <asp:HiddenField ID="hidTollAmnt" Value='<%#Eval("Toll_Amnt")%>' runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="150px"
                                                HeaderText="Serv.Tax Amount">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                <ItemTemplate>
                                                    <%#Eval("ServTax_Amnt")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="150px"
                                                HeaderText="Amount">
                                                <ItemStyle HorizontalAlign="Right" Width="180px" />
                                                <ItemTemplate>
                                                    <%#Eval("Total_Amnt")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="150px"
                                                HeaderText=" Net Amount">
                                                <ItemStyle HorizontalAlign="Right" Width="180px" />
                                                <ItemTemplate>
                                                    <%#Eval("Net_Amnt")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"
                                                HeaderText=" Remarks">
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                <ItemTemplate>
                                                    <%#Eval("Remark")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="20px" HeaderText="">
                                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                <ItemTemplate>
                                                    &nbsp;
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                </asp:GridView>
                    
                            </div>
                            </section>
                        </div>
	                              
								        </div>
							        </section>
                        </div>
                        <div class="modal-footer">
                            <div class="popup_footer_btn">
                                <asp:LinkButton ID="lnkbtnSubmit" runat="server" OnClick="lnkbtnSubmit_OnClick" CssClass="btn btn-dark"
                                    ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="Submit"><i class="fa fa-check">Submit</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnclear" runat="server" OnClick="lnkbtnclear_OnClick" CssClass="btn btn-dark"
                                    ToolTip="Click to clear" CausesValidation="true"><i class="fa fa-times">Close</i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                    <div id="divCharges" class="modal fade">
                <div class="modal-dialog" style="width: 40%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="popform_header">
                                Other Charges
                            </h4>
                        </div>
                        <div class="modal-body">
                            <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">
									        <!-- First Row start -->
								        <div class="clearfix odd_row">	                                
	                                        <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">Add. Freight</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtAddFreight" runat="server" CssClass="form-control" placeholder="Amount" ClientIDMode="Static"></asp:TextBox>
                                                </div>
	                                        </div>
	                                           <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">H.Q.CHARGES</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtHQCharges" runat="server" CssClass="form-control" placeholder="Amount"></asp:TextBox>
                                                </div>
	                                        </div>
	                                    </div> 
                                      <div class="clearfix even_row">	                                
	                                        <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">Charge Name 1</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherCharge1" placeholder="Charge Name 1" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
	                                        </div>
	                                           <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">Amount</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherChargesAmount1" runat="server" placeholder="Amount" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
	                                        </div>
	                                    </div> 
                                           <div class="clearfix odd_row">	                                
	                                        <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">Charge Name 2</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherCharge2" placeholder="Charge Name 2" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
	                                        </div>
	                                           <div class="col-sm-6">
	                                            <label class="col-sm-6 control-label">Amount</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtOtherChargesAmount2" runat="server" placeholder="Amount" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
	                                        </div>
	                                    </div> 
                                </div>
							        </section>
                        </div>
                        <div class="modal-footer">
                            <div class="popup_footer_btn">
                                <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-dark" ToolTip="Click to Save"
                                    OnClick="lnkSave_OnClick" CausesValidation="true"><i class="fa fa-check">Submit</i></asp:LinkButton>
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-dark" ToolTip="Click to Close"
                                    CausesValidation="true"><i class="fa fa-times">Close</i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                </form>
                </div>
            </section>
        </div>
    </div>
    <div class="pop-up-parent">
        <div id="PopUpSetting" class="pop-up" style="width: 600px; height: auto; display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            <i class="img-setting"></i><span>Invoice Setting</span> <i class="fa fa-times"></i>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel full_form_container">
								<div class="panel-body">
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkRequiredShortageGST" runat="server" Text="Req. Shortage GST"></asp:CheckBox>
                                    </div>
                                     <div class="col-sm-4">
                                        <asp:CheckBox ID="CheckRCM" runat="server" Text="Tax Payable Under RCM"></asp:CheckBox>
                                    </div>
                                </div>
                            </section>
                    </div>
                    <hr />
                    <div class="clearfix fifth_right panel">
                        <section class="panel panel-in-default btns_without_border">
                                <asp:LinkButton ID="lnkBtnSaveUserPref" runat="server" CssClass="pop-up-button btn btn-s-md btn-success center-block" TabIndex="17" OnClick="lnkBtnSaveUserPref_OnClick" CausesValidation="true" ><i class="fa fa-save"></i> Save</asp:LinkButton>
                            </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="pop-up-parent">
        <div id="divSave" class="pop-up" style="width: 600px; height: auto;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="popform_header">
                            <i class="img-setting"></i><span>Save Type</span> <i onclick="$('#divSave').fadeOut(300);$('#divSave').parent('div').fadeOut(100);" class="fa fa-times"></i>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <section class="panel full_form_container">
								<div class="panel-body">
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn full_width_btn btn-s-md btn-success"
                                        OnClick="lnkbtnSave_OnClick" CausesValidation="true" ValidationGroup="Submit"> <i class="fa fa-save"></i>Single Invoice</asp:LinkButton>
                                    </div>
                                     <div class="col-sm-4">
                                        <asp:LinkButton ID="lnkbtnSaveMulti" runat="server" CssClass="btn full_width_btn btn-s-md btn-success"
                                        OnClick="lnkbtnSaveMulti_OnClick" CausesValidation="true" ValidationGroup="Submit"> <i class="fa fa-save"></i>Multiple Invoice</asp:LinkButton>
                                    </div>
                                </div>
                            </section>
                    </div>
                    <hr />
                </div>
            </div>
        </div>
    </div>
    <div id="acc_posting" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="popform_header">Acc Posting</h4>
                </div>
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								    <div class="panel-body">
									    <!-- First Row start -->
								    <div class="clearfix odd_row">	                                
	                        <div class="col-sm-5">
	                            <label class="col-sm-3 control-label">From <span class="required-field">*</span></label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtIdFrom" runat="server" CssClass="form-control" Width="100px" c oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivFrom" runat="server" ControlToValidate="txtIdFrom"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="From Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-4">
	                            <label class="col-sm-2 control-label">To <span class="required-field">*</span></label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtIdTo" runat="server" CssClass="form-control" Width="100px" oncopy="return false"
                                        onpaste="return false" oncut="return false" oncontextmenu="return false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivTo" runat="server" ControlToValidate="txtIdTo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="To Required." 
                                    SetFocusOnError="true" ValidationGroup="Acc"></asp:RequiredFieldValidator> 
                            </div>
	                        </div>
	                        <div class="col-sm-3" style="padding: 0;">
	                            <div class="col-sm-12"> 
	                            <asp:Label ID="lblPostingLeft" runat="server"></asp:Label>
	                            </div>
	                        </div>
	                        </div> 	                              
	                              
							    </div>
						    </section>
                </div>
                <div class="modal-footer">
                    <div class="popup_footer_btn">
                        <asp:LinkButton ID="lnkbtnAccPosting" ValidationGroup="Acc" OnClick="lnkbtnAccPosting_Click"
                            class="btn btn-dark" runat="server">Acc Posting</asp:LinkButton>
                        <button type="submit" class="btn btn-dark" data-dismiss="modal">
                            <i class="fa fa-times"></i>Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="print" style="font-size: 13px; display: none;">
        <table cellpadding="1" cellspacing="0" width="1100px" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <div style="text-align: left; width: 140px; float: left">
                                    <asp:Image ID="imageprint" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
                                </div>
                                <div style="text-align: center; float: right; padding-left: 20px">
                                    <strong>
                                        <asp:Label ID="lblCompanyname" runat="server" Style="font-size: 14px;"></asp:Label><br />
                                    </strong>
                                    <asp:Label ID="lblCompAdd1" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCompAdd2" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblCompCity" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblCompState" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCompCityPin" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblCompPhNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblFaxNo" Text="FAX No.:" runat="server"></asp:Label>
                                    <asp:Label ID="lblCompFaxNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblTxtPanNo" Text="PAN No.:" runat="server"></asp:Label>
                                    <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblTin" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                        ID="lblCompTIN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblcodeno" Text="Code No. :" Visible="false" runat="server" Style="font-size: 14px;"></asp:Label>&nbsp;&nbsp;<asp:Label
                                        ID="lblvaluecodeno" runat="server" Visible="false" Style="font-size: 14px;"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <%--<td class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">--%>
                    <%-- <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">--%>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none; border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label5" runat="server" Text="To  "></asp:Label></strong>
                                <br />
                                <b>
                                    <asp:Label ID="lblSenderName" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsenderaddress" runat="server"></asp:Label></b>
                                <br />
                                <b>
                                    <asp:Label ID="lblsendercity" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblsenderstate" runat="server"></asp:Label>
                                </b>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none; border-right-style: none">
                                <strong style="text-decoration: underline">
                                    <asp:Label ID="Label4" runat="server" Text="Transportation Freight Bill"></asp:Label></strong>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none; border-right-style: none">
                                <strong style="text-decoration: underline;">
                                    <asp:Label ID="lblPrintHeadng" runat="server" Text="Quantity & Fright Rate Verity <br>  Bill Passed For Rs………"></asp:Label></strong>
                            </td>
                            <td align="center" class="white_bg" valign="top" style="font-size: 12px; border-left-style: none; border-right-style: none">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="lblinvoiceno" runat="server" Text="Invoice No"></asp:Label></b>
                                            &nbsp;&nbsp;&nbsp;:
                                            <asp:Label ID="valuelblinvoicveno" runat="server" Text=""></asp:Label>
                                            <br />
                                            <b>
                                                <asp:Label ID="lblinvoicedate" runat="server" Text="Invoice Date"></asp:Label>
                                            </b>:
                                            <asp:Label ID="valuelblinvoicedate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="3%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%" align="center">
                                        <strong>Gr Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>GR No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Lorry No</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>DI No</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="6%">
                                        <strong>EGp No</strong>
                                    </td>
                                    <td style="font-size: 12px" align="left" width="17%">
                                        <strong>Receiver Name</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Delv. Place</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Via City</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Weight</strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" width="6%">
                                        <strong>Unloading</strong>
                                    </td>
                                    <td style="font-size: 12px" width="8%">
                                        <strong>Net Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Shortage</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Serv. Tax</strong>
                                    </td>
                                    <td style="font-size: 12px" width="3%" align="left">
                                        <strong>SB.Cess </strong>
                                    </td>
                                    <td style="font-size: 12px" width="7%">
                                        <strong>K.K.Cess</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="3%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("Gr_No")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("LORRY_NO")%>
                                    </td>
                                    <td class="white_bg" width="8%">
                                        <%#Eval("DI_NO")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("EGP_NO")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <%#Eval("Recivr_Name")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("Delvry_Place")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("CityVia_Name")%>&nbsp;
                                    </td>
                                    <td class="white_bg" width="8%" align="right">
                                        <%#(Eval("Weight"))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="7%" align="left">
                                        <%#Eval("Rate")%>
                                    </td>
                                    <td class="white_bg" width="8%" align="left">
                                        <%#Eval("Amount")%>
                                    </td>
                                    <td class="white_bg" width="6%" align="left">
                                        <%#Eval("Wages_Amnt")%>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Shortage")))%>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("servTax_Amnt")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="white_bg" width="12%" align="center">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("SwchBrtTax_Amt")))%>
                                    </td>
                                    <td class="white_bg" width="15%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("KisanTax_Amnt")))%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="50%" id="Table2">
                        <tr>
                            <td class="white_bg" width="3%" align="left"></td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="lbltotalWeight" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="6%" align="right">
                                <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblUnloading" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblNetTotAmnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblShtg" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblTotServTax" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lblTotSBTax" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="4%" align="right">
                                <asp:Label ID="lblKisanTax" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table3">
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="5%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="4%" align="left"></td>
                            <td class="white_bg" width="10%" align="right">
                                <asp:Label ID="lbltxtctax" Font-Bold="true" runat="server" Text="Consign. Serv. Tax"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                <asp:Label ID="valuelbltxtctax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="8%" align="center">
                                <asp:Label ID="lbltxtsertax" Text="Tr. Serv. Tax" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="valuelblservtax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="5%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="4%" align="left"></td>
                            <td class="white_bg" width="10%" align="right">
                                <asp:Label ID="Label10" Font-Bold="true" runat="server" Text="Con.Swatch B.Cess"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                <asp:Label ID="valueCSBTax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="8%" align="center">
                                <asp:Label ID="Label12" Text="Tr.Swatch B.Cess" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="valueTSBTax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="5%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="4%" align="left"></td>
                            <td class="white_bg" width="10%" align="right">
                                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Con.Krishi Cess"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                <asp:Label ID="ValueCKisanTax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="8%" align="center">
                                <asp:Label ID="Label6" Text="Tr.Krishi Cess" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="ValueTKisanTax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="5%"></td>
                            <td class="white_bg" width="4%" align="left"></td>
                            <td class="white_bg" width="10%" align="right">
                                <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Other Amount"></asp:Label>
                            </td>
                            <td align="right" class="white_bg" width="5%">
                                <asp:Label ID="lblOtherTotAmnt" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="9%" align="center">
                                <asp:Label ID="Label8" Text="Shortage Amount" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="center">
                                <asp:Label ID="lblShotageAmnt" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" align="right">
                        <tr>
                            <td colspan="3" align="left" width="30%">
                                <table>
                                    <tr>
                                        <td width="80%">
                                            <asp:Label ID="lblremark" runat="server" valign="right" Text="we hereby certify that cenvat credit on input, capital goods and input services,used for providing the taxable service of transportration, has not been taken by the service provider under the provision of the cenvat credit rules,2004					"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="80%">
                                            <asp:Label ID="lblenclosure" runat="server" Text="Enclosures" valign="right"></asp:Label>&nbsp;:&nbsp;<asp:Label
                                                ID="lblvalueencosers" runat="server" valign="right"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="16%" align="center" valign="top"></td>
                            <td colspan="2" width="20%" align="right" valign="top">
                                <table width="100%">
                                    <tr style="border-bottom-style: solid; border-top-width: thin; border-bottom-width: thin; border-right-width: thin">
                                        <td align="right">&nbsp;&nbsp;
                                            <asp:Label ID="lblnet" runat="server" Text="Net Amnt" Font-Size="13px" valign="right"></asp:Label>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right" width="8%">
                                            <asp:Label ID="lblNetAmnt" runat="server" Font-Size="13px" valign="lef"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="white_bg" style="font-size: small" valign="top" colspan="3">
                                            <b>
                                                <asp:Label ID="lblcompname" runat="server"></asp:Label><br />
                                                <br />
                                                <br />
                                                Authorised Signatory&nbsp;</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="print1" style="font-size: 13px; display: none">
        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <div style="text-align: left; width: 140px; float: left;">
                                    <asp:Image ID="imgprintGen" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
                                </div>
                                <div style="text-align: center; float: right; padding-left: 100px">
                                    <%-- <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px;
                    border-left-style: none; border-right-style: none">--%>
                                    <strong>
                                        <asp:Label ID="lblCompanyname1" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                    </strong>
                                    <asp:Label ID="lblCompAdd11" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCompAdd22" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblCompCity1" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblCompState1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCompCityPin1" runat="server"></asp:Label><br />
                                    PH:
                                    <asp:Label ID="lblCompPhNo1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblFaxNo1" Text="FAX No.:" runat="server"></asp:Label>
                                    <asp:Label ID="lblCompFaxNo1" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblTin1" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                        ID="lblCompTIN1" runat="server"></asp:Label>
                                    <%-- </td>--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                    <h3>
                        <strong style="text-decoration: underline">
                            <asp:Label ID="Label19" runat="server" Text="Invoice"></asp:Label></strong></h3>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtgrno" Text="Invoice No." runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblGRno" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtgrdate" Text="Invoice Date" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblGrDate" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lbltxtPartyName" Text="Party Name" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="valuelbltxtPartyName" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblNameShipmentno" Text="Shipment No" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblShipmentNo" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblNameContnrNo" Text="Container No" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblContainerNo" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblNameCntnrSize" Text="Size" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblContainerSize" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblNameContnrType" Text="Type" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblCntnrType" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <asp:Label ID="lblNameSealNo" Text="Seal No" runat="server"></asp:Label>
                            </td>
                            <td>:
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">
                                <b>
                                    <asp:Label ID="lblSealNo" runat="server"></asp:Label></b>
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td align="left" class="white_bg" valign="top" style="font-size: 13px; border-right-style: none">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table4">
                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td class="white_bg" style="font-size: 12px" width="5%">
                                        <strong>S.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>GR.No.</strong>
                                    </td>
                                    <td style="font-size: 12px" width="9%">
                                        <strong>GR. Date</strong>
                                    </td>
                                    <td style="font-size: 12px" width="10%">
                                        <strong>Del. Place</strong>
                                    </td>
                                    <td style="font-size: 12px" width="18%">
                                        <strong>Item Name</strong>
                                    </td>
                                    <td style="font-size: 12px" width="5%">
                                        <strong>Qty</strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="9%">
                                        <strong>Weight</strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="9%">
                                        <strong>Rate</strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="12.5%">
                                        <strong>
                                            <asp:Label ID="lblShort" runat="server" Text="Shortage Qty"></asp:Label></strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="12.5%">
                                        <strong>Amount</strong>
                                    </td>
                                    <td style="font-size: 12px" align="right" width="5%">
                                        <strong>
                                            <asp:Label ID="lblrefno" Text="Ref No." runat="server"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="white_bg" width="5%">
                                        <%#Container.ItemIndex+1 %>.
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td class="white_bg" width="9%">
                                        <%#string.IsNullOrEmpty(Convert.ToString(Eval("GR_Date"))) ? "" : Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MM-yyyy")%>
                                    </td>
                                    <td class="white_bg" width="10%">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                    <td class="white_bg" width="18%">
                                        <%#Eval("Item_Name")%>
                                    </td>
                                    <td class="white_bg" width="5%">
                                        <%#Eval("Qty")%>
                                    </td>
                                    <td class="white_bg" width="9%" align="right">
                                        <%#String.Format("{0:0.0}", Convert.ToDouble(Eval("Weight")))%>
                                    </td>
                                    <td class="white_bg" width="10%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>
                                    </td>
                                    <td class="white_bg" width="12.5%%" align="right">
                                        <asp:Label ID="lblValueShort" runat="server" Text='<%#String.Format("{0:0.00}", Convert.ToDouble(Eval("shortage")))%>'></asp:Label>
                                    </td>
                                    <td class="white_bg" width="12.5%%" align="right">
                                        <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>
                                    </td>
                                    <td class="white_bg" width="5%" align="right">
                                        <%#Eval("Ref_no")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table5">
                        <tr>
                            <td class="white_bg" width="5%"></td>
                            <td class="white_bg" width="10%"></td>
                            <td class="white_bg" width="20%" align="right">
                                <asp:Label ID="lblttl" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="5%" align="right">
                                <asp:Label ID="lbltotalqty" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="10%" align="right">
                                <asp:Label ID="lblweight" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="10%">
                                <asp:Label ID="Label7" Font-Bold="true" Text="" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="10%">
                                <asp:Label ID="Label9" Font-Bold="true" Text="" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="lblshortage" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%%" align="center">
                                <asp:Label ID="lblTotalAmnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" colspan="9" width="15%">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="lblCtax" Text="Consigner Tax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="valuelblCtax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="lblStax" Text="Serv.Tax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="valuelblStax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="lblCSwachtax" Text="C.Swchbhrt Cess" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="valuelblCSwachtax" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="lblSwachtax" Text="T.Swchbhrt Cess" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="valuelblSwachtax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="lblCKisantax" Text="C.Kisan Kalyn Cess" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="valuelblCKisantax" Font-Bold="false" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="lblTKisantax" Text="T.Kisan Kalyn Cess" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="valueKisantax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%--GST Values--%>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="Label11" Text="C.SGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="prntCSGST" Font-Bold="false" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="Label14" Text="T.SGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="prntTSGST" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="Label17" Text="C.CGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="prntCCGST" Font-Bold="false" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="Label20" Text="T.CGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="prntTCGST" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="Label23" Text="C.IGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="prntCIGST" Font-Bold="false" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="Label27" Text="T.IGST" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="prntTIGST" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%"></td>
                            <td class="white_bg" width="15%" align="center"></td>
                            <td class="white_bg" width="15%" align="left">
                                <asp:Label ID="lbltxtprevamnt" Font-Bold="true" Text="Prv.Rcpt.Amnt." runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%">
                                <asp:Label ID="valuelbltxtprevamnt" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="15%">
                                <asp:Label ID="lblnetamntAtbttm" Font-Bold="true" Text="Net Amnt." runat="server"></asp:Label>
                            </td>
                            <td class="white_bg" width="12.5%" align="center">
                                <asp:Label ID="valuelblnetamntAtbttm" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="4">
                    <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                        <tr style="line-height: 25px">
                            <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                <table width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                            <br />
                                            <b>
                                                <asp:Label ID="lblCompname1" runat="server"></asp:Label><br />
                                                <br />
                                                <br />
                                                Authorised Signatory&nbsp;</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <%--#JainBulkPrint--%>
    <div id="Jainprint" style="font-size: 13px; display: none;">
        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td class="white_bg" align="center">
                    <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <td>
                                <div style="text-align: left; width: 140px; float: left;">
                                    <asp:Image ID="Imgjain" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
                                </div>
                                <div class="white_bg" valign="top" colspan="4" style="font-size: 14px; text-align: center; border-left-style: none; border-right-style: none;">
                                    <strong>
                                        <asp:Label ID="lblCompName2" runat="server" Style="font-size: 18px;"></asp:Label><br />
                                    </strong>
                                    <asp:Label ID="lblCompAdd3" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCompAdd4" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblCompCity2" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblCompState2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblCompCityPin2" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblCompPhNo2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblFaxNo2" Text="FAX No.:" runat="server"></asp:Label>
                                    <asp:Label ID="lblCompFaxNo2" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblTin2" runat="server" Text="Serv.Tax No. :"></asp:Label>&nbsp;<asp:Label
                                        ID="lblCompTIN2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbltxtPanNo1" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                        ID="lblPanNo1" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="Label25" runat="server" Text="Transportation Bill"></asp:Label>
                                        <br />
                                    </strong>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none"
                                            valign="top">
                                            <asp:Label ID="Label26" runat="server">Invoice No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblIvNo" runat="server"></asp:Label></b>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 14%; font-size: 13px; border-right-style: none"
                                            valign="top">
                                            <asp:Label ID="Label28" Text="Invoice Date" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblIvDt" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 13px; border-right-style: none"
                                            valign="top">
                                            <asp:Label ID="lblPrtyName1" runat="server">Party Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                            <b>
                                                <asp:Label ID="lblPrtyName" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%;" valign="top">
                                            <table border="0" width="100%" class="white_bg" style="border-right: 1px solid #484848;">
                                                <tr>
                                                    <td colspan="2" style="border-bottom: 1px solid #484848;">
                                                        <strong>Consignor's Details:</strong>
                                                    </td>
                                                </tr>
                                                <tr id="trConsigneeName" runat="server">
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="Label41" runat="server">Name</asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsignorName" runat="server"></asp:Label>
                                                            <b /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="Label42" runat="server">Address</asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsignorAddress" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="Label43" runat="server" Text="Tin"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsignorTin" Text="" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%;" valign="top">
                                            <table border="0" width="100%" class="white_bg">
                                                <tr>
                                                    <td colspan="2" style="border-bottom: 1px solid #484848; height: 10px;">
                                                        <strong>Consignee's Details:</strong>
                                                    </td>
                                                </tr>
                                                <tr id="trConsignorName" runat="server">
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="Label44" runat="server">Name</asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsigeeName" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="Label45" runat="server">Address</asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 13px; border-right-style: none; width: 25%;">
                                                        <asp:Label ID="lblPrtyTinTxt" runat="server" Text="Tin"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 13px; border-right-style: none">
                                                        <b>
                                                            <asp:Label ID="lblConsigneeTin" Text="" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table6">
                                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <td class="white_bg" style="font-size: 12px" width="5%">
                                                    <strong>S.No.</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Lorry No</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>GR No</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>GR Date</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>FromCity</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>ToCity</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Item Name</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Disp. Qty</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Delv. Qty</strong>
                                                </td>
                                                <td style="font-size: 12px" align="left" width="10%">
                                                    <strong>Rate</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Amount</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>S.Amnt</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%">
                                                    <strong>Other</strong>
                                                </td>
                                                <td style="font-size: 12px" width="10%" align="right">
                                                    <strong>T.Freight</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="white_bg" width="10%">
                                                    <%#Container.ItemIndex+1 %>.
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("LORRY_NO")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("GR_No")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#string.IsNullOrEmpty(Convert.ToString(Eval("GR_Date"))) ? "" : Convert.ToDateTime(Eval("GR_Date")).ToString("dd-MM-yyyy")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("From_City")%>
                                                </td>
                                                <td class="white_bg" width="10%">
                                                    <%#Eval("To_City")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("Item_Name")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("Qty")%>
                                                </td>
                                                <td class="white_bg" width="15%">
                                                    <%#Eval("DelvQty")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="15%" align="center">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Rate")))%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="left">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Amount")))%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("ShortageAmnt")))%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="left">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("OtherAmnt")))%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="white_bg" width="10%" align="right">
                                                    <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Net_Amnt")))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" style="font-size: 12px" width="100%" id="Table7">
                                    <tr>
                                        <td class="white_bg" width="15%"></td>
                                        <td class="white_bg" width="17%"></td>
                                        <td class="white_bg" width="8%" align="center">
                                            <asp:Label ID="Label46" Text="Total" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="6%" align="left">
                                            <asp:Label ID="lblQty3" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="8%" align="left">
                                            <asp:Label ID="lblDelQty3" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="1%"></td>
                                        <td class="white_bg" width="6%">
                                            <asp:Label ID="lblAmount1" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="5%" align="left">
                                            <asp:Label ID="lblFshor" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="3%" align="center">
                                            <asp:Label ID="lblother" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td class="white_bg" width="4%" align="right">
                                            <asp:Label ID="lblNetTotAmnt1" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" height="20px"></td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" align="left" width="50%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblWords" runat="server" Font-Size="10" Text="Amount : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblwordsVal" runat="server" Font-Size="10" valign="right"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="3" width="34%" align="right">
                                            <table>
                                                <tr>
                                                    <td>&nbsp; <strong>
                                                        <asp:Label ID="lblNetTotal" runat="server" Font-Size="13px" valign="right">Net Amnt : </asp:Label></strong>
                                                    </td>
                                                    <td align="right">
                                                        <strong>
                                                            <asp:Label ID="valuelblnetAmnt" runat="server" Font-Size="13px"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" width="50%">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="50%">
                                                        <br />
                                                        <b>
                                                            <asp:Label ID="lblCompname4" runat="server"></asp:Label><br />
                                                            <br />
                                                            <br />
                                                            Authorised Signatory&nbsp;</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <%--#JainSinglePrint--%>
    <div id="JainSingle" style="font-size: 13px; display: none;">
        <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
            <tr>
                <td class="white_bg" align="center">
                    <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <%--<td align="left" style=" border-left-style: none; border-right-style: none;"> 
                       <div style="text-align:left;width:140px; float:left;  margin-left:7px ;">
                           <asp:Image ID="imgjainsingle" Width="140px" Height="90px" runat="server" Visible="false"></asp:Image>
                           </div>
                        </td>--%>
                            <td align="left" class="white_bg" valign="top" colspan="2" style="font-size: 14px; border-left-style: none; border-right-style: none; width: 60%;">
                                <strong>
                                    <asp:Label ID="lblCompanyname5" runat="server" Style="font-size: 23px;"></asp:Label><br />
                                </strong>
                                <asp:Label ID="lblCompAdd5" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblCompAdd6" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompCity5" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCompState5" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCompCityPin5" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCompPhNo5" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFaxNo5" Text="FAX No.:" runat="server"></asp:Label>
                                <asp:Label ID="lblCompFaxNo5" runat="server"></asp:Label><br />
                            </td>
                            <td align="right" class="white_bg" valign="top" colspan="2" style="font-size: 14px; border-left-style: none; border-right-style: none; width: 40%;">
                                <b>
                                    <asp:Label ID="lbltxtPanNo5" runat="server" Text="PAN NO. :"></asp:Label>&nbsp;&nbsp;<asp:Label
                                        ID="lblPanNo5" runat="server"></asp:Label></b><br />
                                <b>
                                    <asp:Label ID="lblTin5" runat="server" Text="GSTIN No. :"></asp:Label>&nbsp;<asp:Label
                                        ID="lblCompTIN5" runat="server"></asp:Label></b><br />
                                <table border="1" width="250px" height="80px">
                                    <tr>
                                        <td>
                                            <b>M/s</b>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblSender" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCity" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label
                                                ID="lblState" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSenderGSTINNo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>INVOICE NO.&nbsp;&nbsp;</b>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 22%; font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblInvNo5" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 21%; font-size: 13px; border-right-style: none">
                                            <b><u>LOADING DESCRIPTION</u> </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 18%; font-size: 13px; border-right-style: none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" style="width: 14%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>DATE</b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 13px; border-right-style: none">
                                            <asp:Label ID="lblInvdate5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>PLANT INV. NO</b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblRefNo" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>LORRY NO.</b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 18%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblLorryNo" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 15%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>DISPATCH DATE</b> &nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblDispDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>FROM </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 12%; font-size: 11px; border-right-style: none">
                                            <asp:Label ID="lblFromCity5" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>VIA </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblDelPl" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>TO</b> &nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 18%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblToCity5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>LR NO.</b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblGRNo5" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>GR DATE </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 18%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblGRDate5" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 12%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>CONTAINER NO </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblContainerNo5" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="white_bg" style="width: 13%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>CONTAINER SIZE </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lblContSize" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" class="white_bg" style="width: 13%; font-size: 11px; border-right-style: none"
                                            valign="top">
                                            <b>TOTAL WEIGHT </b>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" class="white_bg" valign="top" style="width: 20%; font-size: 12px; border-right-style: none">
                                            <asp:Label ID="lbltotalweightain" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="white_bg" valign="top" colspan="4" style="font-size: 14px; border-left-style: none; border-right-style: none">
                                <h3>
                                    <strong style="text-decoration: underline">
                                        <asp:Label ID="lbltxtBill" runat="server" Text="Billing Particulars" Font-Underline="true"></asp:Label>
                                        <br />
                                    </strong>
                                </h3>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="1" cellspacing="0" width="800" border="1" style="font-family: Arial,Helvetica,sans-serif;">
                        <tr>
                            <td colspan="3" width="700px"></td>
                            <td colspan="1" align="center">
                                <asp:Label ID="Label22" Width="100Px" runat="server" Text="Amount(Rs)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" width="700px">
                                <table width="100%" align="left" cellpadding="1" cellspacing="0" border="0">
                                    <tr style="height: 15px;">
                                        <td align="left" colspan="3">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">
                                            <asp:Label ID="Label37" runat="server" Font-Bold="true" Font-Size="8" Text="FREIGHT AMOUNT"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label40" runat="server" Font-Bold="true" Font-Size="8" Text="     1. Other Charges"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label47" runat="server" Font-Bold="true" Font-Size="8" Text="A) ADDITIONAL FREIGHT"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label48" runat="server" Font-Bold="true" Font-Size="8" Text="B) ADD.H.Q.CHARGES"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="8" Text="C) BILITY"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblOtherAmnt" runat="server" Font-Bold="true" Font-Size="8" Text="D) Other Charges"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCharge1" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCharge2" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label50" runat="server" Font-Bold="true" Font-Size="8" Text="2."></asp:Label>
                                            <asp:Label ID="Label51" runat="server" Font-Bold="true" Font-Size="8" Text="DETENTION CHARGES"></asp:Label>
                                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                            <%--<asp:Label ID="lblFromCity6" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label55" runat="server" Font-Bold="true" Font-Size="8" Text="A)"></asp:Label>
                                            <asp:Label ID="Label56" runat="server" Font-Bold="true" Font-Size="8" Text="PLANT IN DATE"></asp:Label>
                                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPlantInDate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label59" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="Label60" runat="server" Font-Bold="true" Font-Size="8" Text="PLANT OUT DATE"></asp:Label>
                                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPlantOutDate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPlantRate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label63" runat="server" Font-Bold="true" Font-Size="8" Text="B)"></asp:Label>
                                            <asp:Label ID="Label64" runat="server" Font-Bold="true" Font-Size="8" Text="PORT REPORTING DATE"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPortInDate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label67" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>
                                            <asp:Label ID="Label68" runat="server" Font-Bold="true" Font-Size="8" Text="PORT UNLOADING DATE"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPortOutDate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblPrPortRate" runat="server" Font-Bold="true" Font-Size="8" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" style="visibility: hidden;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="Label71" runat="server" Font-Bold="true" Font-Size="8" Text="ADD/LESS : OTHER CHARGES"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblcontainercharges" runat="server" Text="Container Lifting Charge"
                                                Font-Bold="true" Font-Size="8"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="1" align="right">
                                <table width="100%" align="left" cellpadding="1" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAmnt5" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAddCharges" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblHQCharges" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblBilityCharges" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblOtherChargesAmnt" runat="server" Font-Bold="true" Font-Size="10"
                                                Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblCharges1Amnt" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblCharges2Amnt" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td align="right">
                                            <asp:Label ID="lblTotalPlantAmnt" runat="server" Font-Bold="true" Font-Size="10"
                                                Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTotalPortAmnt" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="visibility: hidden;">
                                            <asp:Label ID="lblOtherChrgs" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" width="600px" align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label73" runat="server" Font-Bold="true" Font-Size="10" Text="TOTAL AMOUNT"></asp:Label>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label74" runat="server" Font-Bold="true" Font-Size="10" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td colspan="1" align="right">
                                <asp:Label ID="lblNetAmnt5" Font-Bold="true" Font-Size="10" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <table width="100%" style="font-size: 12px" border="0" cellspacing="0">
                                    <tr style="line-height: 25px">
                                        <td colspan="9" style="font-size: 13px" align="left" class="white_bg">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" class="white_bg" style="font-size: 13px" width="70%">
                                                        <asp:Label ID="Label75" Font-Size="8" Font-Bold="true" runat="server" Text="AMT. IN WORDS(Rs.) : "></asp:Label>
                                                        <asp:Label ID="lblAmntWords" Font-Size="9" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <b>Customer Signature</b>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" class="white_bg" style="font-size: 13px" valign="top" width="30%">
                                                        <b>
                                                            <asp:Label ID="lblcompname5" runat="server"></asp:Label><br />
                                                            <br />
                                                            <br />
                                                            Authorised Signatory&nbsp;</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- popup for OM  -->
    <div id="Amount" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                        <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="3 Pages" Value="3" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="1 Pages" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                       </div>
                                       <div class="col-sm-9">
                                        <asp:LinkButton ID="lnkPrint" Text="Print" 
                                                class="btn btn-sm btn-primary" runat="server" TabIndex="45"  
                                                OnClick="lnkPrint_click" ></asp:LinkButton>
                                        </div>
                                       </div>
                                        </section>
                </div>
            </div>
        </div>
    </div>
    <!-- popup for tax/bill invoice  -->
    <div id="Birlamodal" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                       <div class="col-sm-12">
                                        <asp:LinkButton ID="lnkBill" Text="Bill Invoice" 
                                                class="btn btn-sm btn-primary" runat="server" 
                                                OnClick="lnkBillInvoice_click" ></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnkTax" Text="Tax Invoice" 
                                                class="btn btn-sm btn-primary" runat="server"  
                                               OnClick="lnkTaxInvoice_Click" ></asp:LinkButton>
                                    
                                        </div>
                                        
                                       </div>
                                        </section>
                </div>
            </div>
        </div>
    </div>
    <div id="PJLmodal" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                       <div class="col-sm-12">
                                        <asp:LinkButton ID="lnkbilinc" Text="Bill Invoice" 
                                                class="btn btn-sm btn-primary" runat="server" 
                                                OnClick="lnkPJLBillInvoice_click" ></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnkTaxinc" Text="Tax Invoice" 
                                                class="btn btn-sm btn-primary" runat="server"  
                                               OnClick="lnkPJLTaxInvoice_Click" ></asp:LinkButton>
                                    
                                        </div>
                                        
                                       </div>
                                        </section>
                </div>
            </div>
        </div>
    </div>
    <!--POPUP FOR INVOICE WITH HEADER -->
    <div id="Headerwith" class="modal fade">
        <div class="modal-dialog" style="width: 25%">
            <div class="modal-header">
                <h4 class="popform_header">Print&nbsp;&nbsp;&nbsp;&nbsp;</h4>
            </div>
            <div class="modal-content">
                <div class="modal-body">
                    <section class="panel panel-default full_form_container material_search_pop_form">
								        <div class="panel-body">  
                                        <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlPrintwith" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="With Header" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Without Header" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                       </div>
                                       <div class="col-sm-7">
                                        <asp:LinkButton ID="lnkbtnPrintwith" Text="Print" 
                                                class="btn btn-sm btn-primary" runat="server" 
                                                OnClick="lnkbtnPrintwith_click" ></asp:LinkButton>
                                        </div>
                                       </div>
                                        </section>
                </div>
            </div>
        </div>
    </div>
    <div id="StandrdPrint" style="display: none;">
        <table width="100%" cellpadding="5" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
            <tbody>
                <tr>
                    <td colspan="10" align="center" style="border: 1px solid;">INVOICE FOR TRANSPORTATION OF GOODS</td>
                </tr>
                <tr>
                    <th colspan="10" align="center" style="border: 1px solid; font-size: 30px;">
                        <asp:Label ID="lblc" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <td align="center" colspan="10" style="font-size: 17px; border: 1px solid; border-bottom: none;">Head office :-<asp:Label ID="lblCA1" runat="server"></asp:Label>,
                        <asp:Label ID="lblct" runat="server"></asp:Label><br>
                        STATE:-
                        <asp:Label ID="lblstat" runat="server"></asp:Label><br>
                        MOBILE NO.:-<asp:Label ID="lblmobile" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th align="center" colspan="10" style="font-size: 15px; border: 1px solid; border-top: none;">BRANCH OFFICE:- 
                        <asp:Label ID="lbladd2" runat="server"></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td colspan="6" style="border: 1px solid;">PAN NO.:- 
                        <asp:Label ID="lblpan" runat="server"></asp:Label></td>
                    <td colspan="4" width="40%" style="border: 1px solid; text-align: right;">GSTIN:-
                        <asp:Label ID="lblgstin" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" style="border: 1px solid;">GOODS TRANSPORT BY ROAD</td>
                    <td colspan="4" style="border: 1px solid; text-align: right;">TAX PAYABLE UNDER</td>
                </tr>
                <tr>
                    <td colspan="6" style="border: 1px solid;">SAC CODE:-996511
                        <asp:Label ID="lblhsn" runat="server"></asp:Label></td>
                    <td colspan="4" style="border: 1px solid; text-align: right;">REVERS CHARGE:- NO</td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;"><b>BILL NO:-
                        <asp:Label ID="lblbillno" runat="server"></asp:Label></b></td>
                    <td rowspan="8" style="border: 1px solid saddlebrown;" colspan="3">&nbsp;
                    <b>Delivery Address:-
                        <asp:Label ID="lblShipingAdd" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" width="40%" style="border: 1px solid;"><b>BILL DATE:-
                        <asp:Label ID="lblbilldate" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;"><b>
                        <asp:Label ID="lblcontname" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;">
                        <asp:Label ID="lbladd1" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;">
                        <asp:Label ID="lblcadd2" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;">
                        <asp:Label ID="lblcity1" runat="server"></asp:Label>,
                        <asp:Label ID="lblst" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" style="border: 1px solid;">GSTIN :- 
                        <asp:Label ID="lblgst" runat="server"></asp:Label></td>
                </tr>
            </tbody>
        </table>
        <table width="100%" cellpadding="5" cellspacing="0" align="center" style="font-family: arial; font-size: 12px; border-collapse: collapse;">
            <thead>
                <tr>
                    <td align="center" colspan="10" style="border: 1px solid;">BILL DETAILS</td>
                </tr>
                <tr>
                    <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                        <HeaderTemplate>
                            <th width="5%" align="center" style="border: 1px solid;">SR. NO.</th>
                            <th style="border: 1px solid;">LR NO.</th>
                            <th style="border: 1px solid;">LR DATE</th>
                            <th style="border: 1px solid;">TRUCK NO.</th>
                            <th style="border: 1px solid;">ORDER NO.</th>
                            <th style="border: 1px solid;">INVOICE NO.</th>
                            <th style="border: 1px solid;">DESTINATION </th>
                            <th style="border: 1px solid;">MT</th>
                            <th style="border: 1px solid;">RATE</th>
                            <th style="border: 1px solid;">AMOUNT</th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td style="border: 1px solid;">
                                        <%#Container.ItemIndex+1 %>
                                    </td>
                                    <td style="border: 1px solid;">
                                        <%#Eval("GR_No")%>
                                    </td>
                                    <td style="border: 1px solid;">
                                        <%#Convert.ToDateTime(Eval("Gr_Date")).ToString("dd-MM-yyyy")%> 
                                    </td>
                                    <td style="border: 1px solid;">
                                        <%#Eval("Lorry_No")%>
                                    </td>
                                    <td style="border: 1px solid;">
                                        <%#Eval("Ordr_No")%>
                                    </td>

                                    <td style="border: 1px solid;">
                                        <%#Eval("Tax_InvNo")%>
                                    </td>
                                    <td style="border: 1px solid;">
                                        <%#Eval("Delvry_Place")%>
                                    </td>
                                    <td style="border: 1px solid;" align="right">
                                        <%#Eval("Weight")%>
                                    </td>
                                    <td style="border: 1px solid;" align="right">
                                        <%#Eval("Rate")%>
                                    </td>
                                    <td style="border: 1px solid;" align="right">
                                        <%#Eval("Amount")%>  
                                    </td>
                                </tr>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </tr>
            </thead>
            <tr>
                <th colspan="7" align="right" style="border: 1px solid;">Total:-</th>
                <th style="border: 1px solid;" align="right">
                    <asp:Label ID="lbltotqty" runat="server"></asp:Label></th>
                <th style="border: 1px solid;">&nbsp;</th>
                <th align="right" style="border: 1px solid;">
                    <asp:Label ID="lbltotal" runat="server"></asp:Label></th>
            </tr>
            <tr>
                <td colspan="7" align="left" style="border: 1px solid;">Declaration:-</td>
                <td colspan="2" style="border: 1px solid;">&nbsp;</td>
                <td style="border: 1px solid;">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="7" align="left" rowspan="2" valign="top" style="border: 1px solid;">Certified that the Particulars given above are true &amp; Correct.</td>
                <td colspan="2" style="border: 1px solid;">&nbsp;</td>
                <td style="border: 1px solid;">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="border: 1px solid;"><b>SGST  6%</b></td>
                <td align="right" style="border: 1px solid;">
                    <asp:Label ID="lblsgst" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="7" rowspan="3" style="border: 1px solid;">&nbsp;</td>
                <td colspan="2" style="border: 1px solid;"><b>CGST  6%</b></td>
                <td align="right" style="border: 1px solid;">
                    <asp:Label ID="lblcgst" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="border: 1px solid;"><b>IGST 12%</b></td>
                <td align="right" style="border: 1px solid;">
                    <asp:Label ID="lbligst" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="border: 1px solid;"><b>TOTAL BILL AMOUNT</b></td>
                <td align="right" style="border: 1px solid;">
                    <asp:Label ID="lbltobillamnt" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th align="left" colspan="10" style="border: 1px solid;">Rs. in words : -
                    <asp:Label ID="lblwd" runat="server"></asp:Label></th>
            </tr>
            <tr>
                <th colspan="10" align="right" style="border: 1px solid;">For SHIV SHAKTI ROAD LINES<br>
                    <br>
                    <br>
                    <br>
                    Authorised signatory</th>
            </tr>
            </tbody>
        </table>
    </div>
    <asp:HiddenField ID="hidPlantPort2Visible" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidNetAmntMinusPlantAmnt" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidPlantPortCharge" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidFlagMoreDetail" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidRequiredShortageGST" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideimgvalue" runat="server" />
    <asp:HiddenField ID="printFormat1" runat="server" />
    <asp:HiddenField ID="iMaxInvIdno1" runat="server" />
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            if (strid == 'print') {
                var stringId = document.getElementById('<%=hidid.ClientID%>').value;
                var jainpr = document.getElementById('<%=hidjainbulk.ClientID%>').value;
                var userPref = document.getElementById('<%=hideUserPref.ClientID%>').value;
                if (userPref == 3) {
                    window.open("PrintInvoice.aspx?Id=" + stringId);
                }
                else if ($('#hidGstType').val() != "0") {
                    window.open("PrintInvoiceGeneral.aspx?q=" + stringId + "&R=1");
                    return false;
                }
                else {
                    var prtContent1 = "<table width='100%' border='0'></table>";
                    var prtContent = document.getElementById(strid);
                    var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                    WinPrint.document.write(prtContent1);
                    WinPrint.document.write(prtContent.innerHTML);
                    WinPrint.document.close();
                    WinPrint.focus();
                    WinPrint.print();
                    //WinPrint.close();
                    return false;
                }
            }
            else if (strid == 'print1') {
                var prtContent1 = "<table width='100%' border='0'></table>";
                var prtContent = document.getElementById(strid);
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent1);
                WinPrint.document.write(prtContent.innerHTML);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                //WinPrint.close();
                return false;
            }
            else if (strid == 'Jainprint') {
                var prtContent1 = "<table width='100%' border='0'></table>";
                var prtContent = document.getElementById(strid);
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent1);
                WinPrint.document.write(prtContent.innerHTML);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                //WinPrint.close();
                return false;
            }
            else if (strid == 'JainSingle') {
                var prtContent1 = "<table width='100%' border='0'></table>";
                var prtContent = document.getElementById(strid);
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent1);
                WinPrint.document.write(prtContent.innerHTML);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                //WinPrint.close();
                return false;
            }
            else if (strid == 'StandrdPrint') {
                var prtContent1 = "<table width='100%' border='0'></table>";
                var prtContent = document.getElementById(strid);
                var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=1,scrollbars=1,status=1');
                WinPrint.document.write(prtContent1);
                WinPrint.document.write(prtContent.innerHTML);
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                //WinPrint.close();
                return false;
            }
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
            $("#<%=txtDate.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateFrom.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
            $("#<%=txtDateTo.ClientID %>").datepicker({
                buttonImageOnly: false,
                maxDate: 0,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                minDate: mindate,
                maxDate: maxdate
            });
        }

        function openGridDetail() {
            $('#gr_details_form').modal('show');
        }

        function CloseModal() {
            $('#gr_details_form').Hide();
        }

        function ClearPlantDate() {
            document.getElementById("<%=txtPlantInDate.ClientID%>").value = "";
            document.getElementById("<%=txtPlantOutDate.ClientID%>").value = "";
        }

        function ClearPortDate() {
            document.getElementById("<%=txtPortinDate.ClientID%>").value = "";
            document.getElementById("<%=txtPortoutDate.ClientID%>").value = "";
        }

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                    elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;

                }
        }

        var isShift = false;
        var seperator = "-";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
                keyCode <= 37 || keyCode <= 39 ||
                (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function Divopen() {
            $('#Amount').modal('show');
        }
        function openOtherCharges() {
            $('#divCharges').modal('show');
        }
        function CloseOtherCharges() {
            $('#divCharges').modal('hide');
        }
        function Printwith() {
            $('#Headerwith').modal('show');
        }

    </script>
    <script language="javascript" type="text/javascript">
        function DivBirla() {
            $('#Birlamodal').modal('show');
        }
        function DivPJL() {
            $('#PJLmodal').modal('show');
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

        $(document).ready(function () {
            var gst_Idno = 0;
            var inGst_val = $('#hidGstType').val()
            if (inGst_val != "") {
                var gst_Idno = parseInt(inGst_val);
                if (gst_Idno > 0) {
                    $('.gst-calculation').show();
                    $('.tax-calculation').hide();
                }
                else {
                    $('.gst-calculation').hide();
                    $('.tax-calculation').show();
                }
            }
            calculation();
        });
        function calculation() {
            var ServtaxTotal = parseFloat($('#txtTrServTax').val() == undefined ? "0" : $('#txtTrServTax').val().replace(",", ""));
            var SBtaxTotal = parseFloat($('#txtTrSwchBrtTax').val() == undefined ? "0" : $('#txtTrSwchBrtTax').val().replace(",", ""));
            var KtaxTotal = parseFloat($('#txtKisanTaxTrnptr').val() == undefined ? "0" : $('#txtKisanTaxTrnptr').val().replace(",", ""));
            $('#txtTServTax_Total').val(ServtaxTotal + SBtaxTotal + KtaxTotal);
            var TServTaxTotal = parseFloat($('#txtCSServTax').val() == undefined ? "0" : $('#txtCSServTax').val().replace(",", ""))
            var TSBTaxTotal = parseFloat($('#txtCSSwchBrtTax').val() == undefined ? "0" : $('#txtCSSwchBrtTax').val().replace(",", ""))
            var TKTaxTotal = parseFloat($('#txtKisanTax').val() == undefined ? "0" : $('#txtKisanTax').val().replace(",", ""));
            $('#txtCServTax_Total').val(TServTaxTotal + TSBTaxTotal + TKTaxTotal);

            var CSGST = parseFloat($('#txtC_SGST').val() == undefined ? "0" : $('#txtC_SGST').val().replace(",", ""))
            var CCGST = parseFloat($('#txtC_CGST').val() == undefined ? "0" : $('#txtC_CGST').val().replace(",", ""))
            var CIGST = parseFloat($('#txtC_IGST').val() == undefined ? "0" : $('#txtC_IGST').val().replace(",", ""))
            var CGSTCess = parseFloat($('#txtC_GST_Cess').val() == undefined ? "0" : $('#txtC_GST_Cess').val().replace(",", ""));
            $('#txtCGST_Total').val(CSGST + CCGST + CIGST + CGSTCess);
            var TSGST = parseFloat($('#txtT_SGST').val() == undefined ? "0" : $('#txtT_SGST').val().replace(",", ""));
            var TCGST = parseFloat($('#txtT_CGST').val() == undefined ? "0" : $('#txtT_CGST').val().replace(",", ""));
            var TIGST = parseFloat($('#txtT_IGST').val() == undefined ? "0" : $('#txtT_IGST').val().replace(",", ""));
            var TGSTCess = parseFloat($('#txtT_GST_Cess').val() == undefined ? "0" : $('#txtT_GST_Cess').val().replace(",", ""));
            $('#txtTGST_Total').val(TSGST + TCGST + TIGST + TGSTCess);
            $('#txtShoratageDetail').val($('#txtShortageAmnt').val() == undefined ? "0" : $('#txtShortageAmnt').val());
            GetTotal();
        }
        function commaSeparateNumber(val) {
            while (/(\d+)(\d{3})/.test(val.toString())) {
                val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$3');
            }
            return parseFloat(val).toFixed(2);
        }
        function GetTotal() {
            $('.get-total').each(function () {
                var el = $(this);
                var total = 0;
                var commaSeperated = "";
                $(el).children().children().children('.sum-value').each(function () {
                    total += parseFloat($(this).val().replace(",", ""));
                });
                commaSeperatedValue = commaSeparateNumber(total);
                $(el).append('<div class="col-sm-12" style="border-top:1px solid"><label class="col-sm-8 control-label">Total :</label><div class="col-sm-4" style="text-align:right;font-size:11px;"><b>' + total.toLocaleString() + '</b></div></div>');
            });
        }
    </script>

    <%--ShowHide Content Box--%>
    <script>
        $(document).ready(function () {
            if ($('#hidPlantPort2Visible').val() == "1") {
                $('#addmoreinoutdetail').fadeIn();
                $('#addmoreinoutdetail').parent('div.fadeout-box').fadeIn();
            }
        });
        $('.addmoreinoutdetail').click(function () {
            $('#hidPlantPort2Visible').val('1');
            $('#addmoreinoutdetail').fadeIn();
            $('#addmoreinoutdetail').parent('div.fadeout-box').fadeIn();
        });
        $('.close-popup').click(function () {
            $('#hidPlantPort2Visible').val('0');
            $(this).parent().parent().parent().fadeOut();
            $(this).parent().parent().parent().parent().fadeOut();
        });
        function ClearAllTextFields(thisEl) {
            $(thisEl).parent().parent().children('.col-sm-3').children('input').val('');
        }
    </script>
    <script src="js/HintPopUp.js" type="text/javascript"></script>
</asp:Content>
