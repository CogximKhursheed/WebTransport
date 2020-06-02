<%@ Page Title="Lorry Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="LorryMaster.aspx.cs" Inherits="WebTransport.LorryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js">
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            ShowImagePreview();
        });
        // Configuration of the x and y offsets
        function ShowImagePreview() {
            xOffset = -20;
            yOffset = 40;

            $("a.preview").hover(function (e) {
                this.t = this.title;
                this.title = "";
                var c = (this.t != "") ? "<br/>" + this.t : "";
                $("body").append("<p id='preview'><img src='" + this.href + "' alt='Image preview' />" + c + "</p>");
                $("#preview")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px")
            .fadeIn("slow");
            },

    function () {
        this.title = this.t;
        $("#preview").remove();
    });

            $("a.preview").mousemove(function (e) {
                $("#preview")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px");
            });
        };

    </script>
    <style type="text/css">
        #preview
        {
            position: absolute;
            border: 3px solid #ccc;
            background: #333;
            padding: 5px;
            display: none;
            color: #fff;
            box-shadow: 4px 4px 3px rgba(103, 115, 130, 1);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%--  <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>--%>
    <div class="row ">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-8">
            <section class="panel panel-default full_form_container part_purchase_bill_form">
                <header class="panel-heading font-bold form_heading">LORRY MASTER
                  <span class="view_print"><a href="ManageLorryMaster.aspx">
                <asp:Label ID="lblViewList" runat="server" Text="View List&nbsp;&nbsp;" 
                    TabIndex="35"></asp:Label></a> </span>
                </header>
                <div class="panel-body">
                  <form class="bs-example form-horizontal">
                    <!-- first  section --> 
                 		<div class="clearfix first_section">
                      <section class="panel panel-in-default">  
                        <div class="panel-body">
                        	<div class="clearfix odd_row">
                              <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Lorry Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 69%;">
                             
                                    <asp:DropDownList ID="ddllorrytyp" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="1"  OnSelectedIndexChanged="ddllorrytyp_SelectedIndexChanged">
                                        <asp:ListItem Text="Own" Value="0" Selected="True">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Hire" Value="1">
                                        </asp:ListItem>
                                    </asp:DropDownList>                             
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 29%;">Lorry No.<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                             
                                  <asp:TextBox ID="txtLorryNo" CssClass="form-control" runat="server"  MaxLength="15" TabIndex="2"  placeholder="Enter Lorry No."></asp:TextBox>
                             
                                <asp:RequiredFieldValidator ID="rfvLorryNo" runat="server" ControlToValidate="txtLorryNo"
                                    CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Lorry No!" SetFocusOnError="true"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>                      
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Party Name<span class="required-field">*</span></label>
                              <div class="col-sm-5" style="width: 43%;">
                                  <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control" AutoPostBack="true"
                                    TabIndex="3" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                  </asp:DropDownList> 
                                  <asp:RequiredFieldValidator ID="rfvPartyName" runat="server" ControlToValidate="ddlPartyName"
                                SetFocusOnError="true" ValidationGroup="Save" InitialValue="0" ErrorMessage="Please Select Party!"
                                 Display="Dynamic" CssClass="required-field"></asp:RequiredFieldValidator>               
                              </div>
                              <div class="col-sm-1" style="width:8%;">
                              <asp:LinkButton ID="lnkPartyName" runat="server"  
                                        class="btn-sm btn btn-primary acc_home" onclick="lnkPartyName_Click"><i class="fa fa-refresh"></i></asp:LinkButton> 
                              </div>
                              <div class="col-sm-2" style="width: 18%;">
                              <asp:LinkButton  ID="lnkValidFrom"  runat="server" ToolTip="Change Party" Width="20px" Height="20px"
                                      CssClass="btn-sm btn btn-primary acc_home" 
                                      data-target="#divPartyChange" onclick="lnkValidFrom_Click" TabIndex="4"><i class="fa fa-plus"></i></asp:LinkButton>
                                <asp:LinkButton  ID="lnkPrtyLorry"  runat="server" ToolTip="Lorry Details" Width="25px" Height="20px"
                                      CssClass="btn-sm btn btn-primary acc_home" 
                                      data-target="#party_name_popup" onclick="lnkPrtyLorry_Click" TabIndex="4"><i class="fa fa-truck" ></i></asp:LinkButton>
                              </div>                    
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 29%;">Owner Name<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                           
                                <asp:TextBox ID="txtOwnrNme" runat="server" CssClass="form-control" 
                                MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                onpaste="return false" TabIndex="5" placeholder="Enter Owner Name"
                                Text=""></asp:TextBox>
            
                            <asp:RequiredFieldValidator ID="rfvOwnrNme" runat="server" ControlToValidate="txtOwnrNme"
                                CssClass="required-field" Display="Dynamic" ErrorMessage="Please enter Owner Name!"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>              
                              </div>             
                              </div>
                            </div>
                            <div class="clearfix odd_row">
                            <div class="col-sm-6">
                                <label class="col-sm-3 control-label" style="width: 31%;">Mobile No.</label>
                              <div class="col-sm-9" style="width: 69%;">
                             
                               <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10"
                                    placeholder="Enter Mobile No." oncopy="return false" oncut="return false"
                                    onBlur="CheckPAN();" onpaste="return false" TabIndex="6"></asp:TextBox>                  
                              </div>                
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 29%;">Owner Address<span class="required-field"></span></label>
                              <div class="col-sm-9" style="width: 71%;">
                           
                                <asp:TextBox ID="txtOwneradd" runat="server" CssClass="form-control" 
                                MaxLength="50" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                onpaste="return false" TabIndex="5" placeholder="Enter Owner Name"
                                Text=""></asp:TextBox>
            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOwnrNme"
                                CssClass="required-field" Display="Dynamic" ErrorMessage="Please enter Owner Name!"
                                SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>              
                              </div>             
                              </div>
                            </div>
                          </div>  
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">PAN No.</label>
                              <div class="col-sm-9" style="width: 69%;">
                             
                               <asp:TextBox ID="txtPanNo" style="width:83%" runat="server" CssClass="form-control col-sm-9" MaxLength="10"
                                    placeholder="Enter Pan Card Number" oncopy="return false" oncut="return false"
                                    onBlur="CheckPAN();" onpaste="return false" TabIndex="6"></asp:TextBox>  
                                    <div class="col-sm-2"><asp:LinkButton Visible="false" OnClick="lnkUpdateLorryPAN_Click" ID="lnkUpdateLorryPAN" CssClass="btn-sm btn btn-primary acc_home" runat="server"><i class="fa fa-upload"></i></asp:LinkButton></div>
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 29%;">PAN Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 71%;">
                           
                                 <asp:DropDownList ID="ddlPANType" runat="server" CssClass="form-control" 
                                      TabIndex="7">
                                </asp:DropDownList>
                    
                                <asp:RequiredFieldValidator ID="rfvPANType" runat="server" ControlToValidate="ddlPANType"
                                    CssClass="required-field" Display="Dynamic" InitialValue="0" ErrorMessage="Please enter PAN Type.!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                           
                              </div>
                            </div>
                          </div>  
                          <div class="clearfix even_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Chassis No.</label>
                              <div class="col-sm-9" style="width: 69%;">
                                
                                 <asp:TextBox ID="txtChasisNo" runat="server" CssClass="form-control" 
                                    placeholder="Enter Chasis No." MaxLength="22" oncopy="return false" oncut="return false"
                                    onDrop="blur();return false;" onpaste="return false"  TabIndex="8"
                                    Text=""></asp:TextBox>
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" 
                                    style="width: 29%;">Engine No.</label>
                              <div class="col-sm-9" style="width: 71%;">
                                
                                <asp:TextBox ID="txtEngineNo" runat="server" CssClass="form-control" 
                                placeholder="Enter Engine No." MaxLength="22" oncopy="return false" oncut="return false"
                                onDrop="blur();return false;" onpaste="return false" TabIndex="9" Text=""></asp:TextBox>     
                              </div>
                            </div>                            
                          </div> 
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Lry.Make/Year <span id="spanMakeYear" runat="server" class="required-field">*</span></label>
                              <div class="col-sm-6" style="width: 50%;">
                             
                                   <asp:TextBox ID="txtLorrymke" runat="server" CssClass="form-control"
                                    MaxLength="30" Onchange="return ValidateTANNumber(this.value,2);" oncopy="return false"
                                    oncut="return false" onDrop="blur();return false;" placeholder="Enter Lorry Make"
                                    onpaste="return false" TabIndex="10" ></asp:TextBox>  
                                    
                                <asp:RequiredFieldValidator ID="rfvLorrymake" runat="server" ControlToValidate="txtLorrymke"
                                    CssClass="required-field" Display="Dynamic" ErrorMessage="Please enter Lorry Make.!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                  
                              </div>
                              <div class="col-sm-3" style="width: 19%;">
                             
                                 <asp:TextBox ID="txtLorrymkeYear" runat="server" CssClass="form-control"
                                    MaxLength="4" Onchange="return ValidateTANNumber(this.value,2);" oncopy="return false"
                                    oncut="return false" onDrop="blur();return false;" placeholder="Year" onpaste="return false"
                                    TabIndex="11" ></asp:TextBox>     
                                    
                                <asp:RequiredFieldValidator ID="rfvLorrymakeYear" runat="server" ControlToValidate="txtLorrymkeYear"
                                    CssClass="required-field" Display="Dynamic" ErrorMessage="Please enter Lorry Make Year.!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-6 control-label">Commission Calculate</label>
                              <div class="col-sm-6" style="text-align: left;">
                       <asp:CheckBox ID="ChkcommsnCal" runat="server" TabIndex="12" />
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">No. Of Tyres </label>
                              <div class="col-sm-8" style="width: 59%;">
                           
                                 <asp:TextBox ID="txtNumberOfTyres" runat="server" CssClass="form-control"
                                    placeholder="Enter Number of Tyres" MaxLength="3" oncopy="return false" oncut="return false"
                                    onDrop="blur();return false;" onpaste="return false" TabIndex="13"
                                    Text=""></asp:TextBox>                       
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                          <asp:LinkButton ID="lnkSerialDetl" ToolTip="Tyre Details" runat="server" 
                                      CssClass="btn-sm btn btn-primary acc_home" 
                                      data-target="#no_of_tyres_popup" onclick="lnkSerialDetl_Click1" 
                                      TabIndex="14"><i class="fa fa-dot-circle-o"></i></asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-sm-3"> 
                              <label class="col-sm-6 control-label">Cal On DF</label>
                              <div class="col-sm-3" style="vertical-align:middle;text-align: left;">
                             
                                <asp:CheckBox ID="chlCalOnDF" runat="server" TabIndex="15" />
                              </div>
                            </div>

                            <div class="col-sm-3"> 
                              <label class="col-sm-8 control-label">LowRate Wise</label>
                              <div class="col-sm-3" style="text-align: left;">
                                <asp:CheckBox ID="chkLowRate" runat="server" TabIndex="15" />
                              </div>
                            </div>

                          </div> 
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Type<span class="required-field">*</span></label>
                              <div class="col-sm-9" style="width: 59%;">
                            
                                   <asp:DropDownList ID="ddllorytype" runat="server" CssClass="form-control" 
                                       AutoPostBack="False" TabIndex="16" onchange="javascript:OnChangeType();">  </asp:DropDownList>    
                                    
                                <asp:RequiredFieldValidator ID="rfvlorytype" runat="server" ControlToValidate="ddllorytype"
                                    CssClass="required-field" Display="Dynamic" InitialValue="0" ErrorMessage="Please enter Lorry Type!"
                                    SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>                     
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                               <asp:LinkButton ID="lnkLorryType" runat="server" ToolTip="Lorry Details(Trailer)" 
                                      CssClass="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal" 
                                      data-target="#type_popup" onclick="lnkLorryType_Click" TabIndex="17"><i class="fa fa-truck"></i></asp:LinkButton>
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label"  style="width: 29%;" >Driver</label>
                              <div class="col-sm-6"  style="width: 50%;" >
                              
                                    <asp:DropDownList ID="ddldriverName" runat="server" CssClass="form-control" 
                                        TabIndex="18"> </asp:DropDownList>      
                                                             
                              </div>
                              <div class="col-sm-3" style="width: 20%;">
                                    <asp:LinkButton ID="lnkBtnDriverRef" runat="server"  
                                        class="btn-sm btn btn-primary acc_home" onclick="lnkBtnDriverRef_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnCreateDriver" ToolTip="Create Driver" runat="server" 
                                      CssClass="btn-sm btn btn-primary acc_home" 
                                        onclick="lnkBtnCreateDriver_Click"><i class="fa fa-database"></i></asp:LinkButton>    
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-6" style="padding: 0;">
                              <div class="col-sm-8">
	                              <%--<label class="col-sm-3 control-label" style="width: 47%; top: -1049px; left: -37px;">Fitness Date </label>--%>
                                  <label class="col-sm-3 control-label" style="width: 47%;">Fitness Date </label>
	                              <div class="col-sm-9" style="width: 53%;">
	                                
                                     <asp:TextBox ID="txtFitValidDat" runat="server" size="16" TabIndex="19" 
                                          CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy"
                                        MaxLength="10"></asp:TextBox>
	                              </div>
	                            </div>
	                            <div class="col-sm-4">
	                              <label class="col-sm-9 control-label" style="width: 62%;">Warn Bef.</label>
	                              <div class="col-sm-3" style="width: 38%;">
	                                <%--<input type="text" class="form-control">                            --%>
                                     <asp:TextBox ID="txtwarnFit" runat="server" CssClass="form-control" TabIndex="20" MaxLength="2"
                                       onDrop="blur();return false;" oncopy="return false"></asp:TextBox>
	                              </div>
	                            </div>
                            </div>
                            <div class="col-sm-6" style="padding: 0;">
                              <div class="col-sm-8">
	                              <label class="col-sm-3 control-label" style="width: 43%;">RC Date </label>
	                              <div class="col-sm-9" style="width: 57%;">
	                                <%--<input class="input-sm datepicker form-control" size="16" type="text" value="02-02-2016" data-date-format="dd-mm-yyyy">--%>
                                       <asp:TextBox ID="txtRCValidDat" runat="server" 
                                          CssClass="input-sm datepicker form-control" size="16" TabIndex="21"
                                        MaxLength="10"  data-date-format="dd-mm-yyyy"
                                     ></asp:TextBox>
	                              </div>
	                            </div>
	                            <div class="col-sm-4">
	                              <label class="col-sm-9 control-label" style="width: 62%;">Warn Bef.</label>
	                              <div class="col-sm-3" style="width: 38%;">
	                                <%--<input type="text" class="form-control">                            --%>
                                    <asp:TextBox ID="txtwarnRC" runat="server" CssClass="form-control" TabIndex="22" MaxLength="2"
                                       onDrop="blur();return false;" oncopy="return false"></asp:TextBox>
	                              </div>
	                            </div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-6" style="padding: 0;">
                              <div class="col-sm-8">
	                              <label class="col-sm-3 control-label" style="width: 47%;">National Permit </label>
	                              <div class="col-sm-9" style="width: 53%;">
	                                <%--<input class="input-sm datepicker form-control" size="16" type="text" value="02-02-2016" data-date-format="dd-mm-yyyy">--%>
                                       <asp:TextBox ID="txtNatPermitDate" runat="server" size="16" 
                                          CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" 
                                        MaxLength="10" 
                                        TabIndex="23" ></asp:TextBox>
	                              </div>
	                            </div>
	                            <div class="col-sm-4">
	                              <label class="col-sm-9 control-label" style="width: 62%;">Warn Bef.</label>
	                              <div class="col-sm-3" style="width: 38%;">
	                                <%--<input type="text" class="form-control">--%>
                                     <asp:TextBox ID="txtNatPerWarn" runat="server" CssClass="form-control" TabIndex="24" MaxLength="2"
                                       onDrop="blur();return false;" oncopy="return false"></asp:TextBox>
	                              </div>
	                            </div>
                            </div>
                            <div class="col-sm-6" style="padding: 0;">
                              <div class="col-sm-8">
	                              <label class="col-sm-3 control-label" style="width: 43%;">Auth. Permit </label>
	                              <div class="col-sm-9" style="width: 57%;">
	                                <%--<input class="input-sm datepicker form-control" size="16" type="text" value="02-02-2016" data-date-format="dd-mm-yyyy">--%>
                                       <asp:TextBox ID="txtAuthDate" runat="server" 
                                          CssClass="input-sm datepicker form-control" size="16" 
                                           TabIndex="24"
                                            MaxLength="10"></asp:TextBox>
	                              </div>
	                            </div>
	                            <div class="col-sm-4">
	                              <label class="col-sm-9 control-label" style="width: 62%;">Warn Bef.</label>
	                              <div class="col-sm-3" style="width: 38%;">
	                               <%-- <input type="text" class="form-control">--%>
                                      <asp:TextBox ID="txtAuthWarn" runat="server" CssClass="form-control" TabIndex="25" MaxLength="2"
                                                                                            
                                          onDrop="blur();return false;" oncopy="return false"></asp:TextBox>
	                              </div>
	                            </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-6">
                                <div class="col-sm-9"> 
                                    <label class="control-label">Document Holder</label> 
                               </div>
 
                               <div class="col-sm-3">
                                 
                                </div>
                            </div>
                            <div class="col-sm-6">  

                                                       
                              <div class="col-sm-5">
                              	<%--<button type="button" class="btn full_width_btn btn-sm btn-primary" data-toggle="modal" data-target="#document_holder_popup"><i class="fa fa-upload"></i>Document</button>--%>
                                    <asp:LinkButton ID="lnkDocHolder" runat="server" 
                                      CssClass="btn full_width_btn btn-sm btn-primary" data-toggle="modal" 
                                      data-target="#document_holder_popup" TabIndex="26" ><i class="fa fa-upload"></i>Document</asp:LinkButton>
                              </div>
                              <%--<label class="col-sm-8 control-label"  style="width: 70%;">1000</label>--%>
                              <div class="col-sm-2"> 
                              <asp:Label ID="TotalDocumentAdd" runat="server" CssClass="col-sm-8 control-label"  Text="0"></asp:Label></div>
                            </div>
                          </div>
                          <div class="clearfix odd_row">
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 31%;">Insurance No.</label>
                              <div class="col-sm-9" style="width: 59%;"> 
                                 <asp:TextBox ID="txtInsuranceNo" runat="server" CssClass="form-control" 
                                      MaxLength="15" TabIndex="27"
                                     onDrop="blur();return false;" onpaste="return false" oncut="return false" 
                                      oncopy="return false"></asp:TextBox>                         
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                                <%--<button class="btn-sm btn btn-primary acc_home" type="button" ToolTip="Insurance Details" data-toggle="modal" data-target="#insurence_popup"><i class="fa fa-database"></i></button>  --%>
                                <asp:LinkButton ID="lnkInsurance" ToolTip="Insurance Details" runat="server" 
                                      CssClass="btn-sm btn btn-primary acc_home"   
                                      TabIndex="28" onclick="lnkInsurance_Click"><i class="fa fa-database"></i></asp:LinkButton>                           
                              </div>
                            </div>
                            <div class="col-sm-6">
                              <label class="col-sm-3 control-label" style="width: 30%;">Financer Name</label>
                              <div class="col-sm-9" style="width: 60%;">
                                <%--<input type="text" class="form-control">    --%>
                                   <asp:TextBox ID="txtFinancerName" runat="server" CssClass="form-control" 
                                      MaxLength="30" TabIndex="29"
                                  onDrop="blur();return false;" onpaste="return false" oncut="return false" 
                                      oncopy="return false"></asp:TextBox>                         
                              </div>
                              <div class="col-sm-1" style="width: 10%;">
                                <%--<button class="btn-sm btn btn-primary acc_home" type="button" data-toggle="modal" data-target="#financer_popup"><i class="fa fa-database"></i></button>                            --%>
                                <asp:LinkButton ID="lnkFinanceDetl"  ToolTip="Financer Details" runat="server" 
                                      CssClass="btn-sm btn btn-primary acc_home" 
                                     TabIndex="30" onclick="lnkFinanceDetl_Click"><i class="fa fa-database"></i></asp:LinkButton>
                              </div>
                            </div>
                          </div>
                          <div class="clearfix even_row">
                            <div class="col-sm-6">
                             <label class="col-sm-3 control-label" style="width: 29%;">Active</label>
                              <div class="col-sm-9" style="text-align: left; width: 71%;">
                                  <asp:CheckBox ID="chkStatus" runat="server" TabIndex="31" />
                              </div>
                            </div>
                            <div class="col-sm-6">
                             
                            </div>
                          </div>

                        </div>
                      </section>                        
                    </div>
                    
             

                     <!-- fourth row -->
                    <div class="clearfix fourth_right">
                      <section class="panel panel-in-default btns_without_border">
                        <div class="panel-body">     
                          <div class="clearfix odd_row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                              <div class="col-sm-4">
                                <asp:LinkButton ID="lnkBtnNew" CssClass="btn full_width_btn btn-s-md btn-info"  TabIndex="34"
                                      runat="server" onclick="lnkBtnNew_Click"><i class="fa fa-file-o"></i>New</asp:LinkButton>
                              
                              </div>                                  
                              <div class="col-sm-4">
                              <asp:LinkButton ID="lnkBtnSave" CssClass="btn full_width_btn btn-s-md btn-success" 
                                      runat="server" ValidationGroup="Save" TabIndex="32" 
                                      onclick="lnkBtnSave_Click" ><i class="fa fa-save"></i>Save</asp:LinkButton>
                                
                              </div>
                              <div class="col-sm-4">
                               <asp:LinkButton ID="lnkBtnCancel" 
                                      CssClass="btn full_width_btn btn-s-md btn-danger" TabIndex="33"
                                      runat="server" onclick="lnkBtnCancel_Click"  ><i class="fa fa-close"></i>Cancel</asp:LinkButton>
                               
                              </div>
                            </div>
                            <div class="col-lg-2"></div>
                          </div> 
                        </div>
                      </section>
                    </div>                      
                            
                     <!-- popup form Party Name -->
										<div id="party_name_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Lorry Detail </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">                
                                                  <asp:GridView ID="grdGrdetals" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display nowrap"
                                            BorderWidth="0" RowStyle-CssClass="even" AlternatingRowStyle-CssClass="odd" 
                                            OnRowCommand="grdGrdetals_RowCommand">
                                            <HeaderStyle ForeColor="Black" CssClass="linearBg" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("Lorry_No"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lorry Type" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("LorryType"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner Name" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Convert.ToString(Eval("OwnerName"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Pan No">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("PanNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Chasis No">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("ChasisNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" HeaderText="Engine No">
                                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                    <ItemTemplate>
                                                        <%#Eval("EngineNo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/pan.png" CommandArgument='<%#Eval("OwnerName") + "-" + Eval("PanNo")%>'
                                                            CommandName="cmdPan" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                         </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn">  
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>

                    <!-- popup form Change Party Name-->

                    <div id="divPartyChange" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Change Party</h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">                
                                                  <div class="clearfix odd_row">
                                                  <div class="col-sm-6">
											         <label class="col-sm-4 control-label">Lorry No</label>
					                                <div class="col-sm-8">
					                                   <asp:DropDownList ID="ddlLorryNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLorryNo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLorryNo"
                                                            CssClass="required-field" Display="Dynamic" ErrorMessage="Please Select Lorry No!" InitialValue="0"
                                                            SetFocusOnError="true" ValidationGroup="SubmitNew"></asp:RequiredFieldValidator>  
					                                </div>
						                            </div>
                                                    <div class="col-sm-6">
											         <label class="col-sm-4 control-label">Party Name</label>
					                                <div class="col-sm-8">
					                                   <asp:DropDownList ID="ddlPartyNameChange" CssClass="form-control" runat="server"></asp:DropDownList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPartyNameChange"
                                                            CssClass="required-field" Display="Dynamic" ErrorMessage="Please Select Party Name!" InitialValue="0"
                                                            SetFocusOnError="true" ValidationGroup="SubmitNew"></asp:RequiredFieldValidator> 
					                                </div>
						                            </div>
                                                    </div>
                                                     <div class="clearfix even_row">
                                                     <div class="col-sm-6">
											         <label class="col-sm-4 control-label">Valid Upto</label>
					                                <div class="col-sm-8">
					                                   <asp:TextBox ID="txtValidFromDt1" CssClass="input-sm datepicker form-control" runat="server"></asp:TextBox>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtValidFromDt1"
                                                            CssClass="required-field" Display="Dynamic" ErrorMessage="Please Select Date!"
                                                            SetFocusOnError="true" ValidationGroup="SubmitNew"></asp:RequiredFieldValidator>
					                                </div>
						                            </div>
                                                    <div class="col-sm-6">
                                                    <div class="col-sm-4">
                                                    <asp:LinkButton ID="lnkbtnSubmitChange" runat="server" style="margin-top:0px;" CssClass="btn full_width_btn btn-sm btn-primary subnew" onclick="lnkbtnSubmitChange_Click"  ToolTip="Click to Submit" CausesValidation="true" ValidationGroup="SubmitNew" >Submit</asp:LinkButton>
                                                   
                                                   <asp:HiddenField ID="hdnLorryMaxID" runat="server"></asp:HiddenField> </div></div>
                                                    </div>
                                                    </div></section>
                                          <section class="panel panel-default full_form_container material_search_pop_form">
                                         <div class="panel-body" style="overflow-x:auto">
                                         <table width="100%">
                                       <tr>
                                       <td>  
                                        <asp:GridView ID="grdPrtyAdd" runat="server" GridLines="None" AutoGenerateColumns="false"
                                            Width="100%" BorderStyle="None" CssClass="display nowrap dataTable"
                                            BorderWidth="0" RowStyle-CssClass="even" AlternatingRowStyle-CssClass="odd" OnRowDataBound="grdPartyChange_RowDataBound"
                                            OnRowCommand="grdPartyChange_RowCommand">
                                            <RowStyle CssClass="odd" />
                                            <AlternatingRowStyle CssClass="even" />  
                                            <Columns>
                                                <asp:TemplateField HeaderText="Lorry No." HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLorryNo" runat="server" Text='<%#Eval("LorryNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyChange" runat="server" Text='<%#Eval("AcntName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid From" HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidFromDt" runat="server" Text='<%#(string.IsNullOrEmpty(Convert.ToString(Eval("ValidFrom"))) ? "" : (Convert.ToDateTime((Eval("ValidFrom"))).ToString("dd-MMM-yyyy")))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblLooryidno" Visible="false" runat="server" Text='<%#Eval("LorryIdno")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnLorryTempId" runat="server" Value='<%#Eval("ID")%>'></asp:HiddenField>
                                               <asp:LinkButton ID="lnkbtnEdit" CssClass="edit" runat="server" CommandArgument='<%#Eval("ID") %>' Visible="false" CommandName="cmdedit" ToolTip="Click to edit"><i class="fa fa-edit icon"></i></asp:LinkButton>   </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Records(s) not found.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        </td>
                                        </tr>
                                        </table>
                                         </div>
										        </section>
                                                <tr>
                                                 <td>
                                        <asp:Label ID="lblLorryError" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn">  
                                                    <asp:LinkButton ID="lnkPartyChangeSave" runat="server" CssClass="btn btn-dark" OnClick="lnkPartyChangeSave_OnClick" 
                                                          ><i class="fa fa-save"></i>Save</asp:LinkButton>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>

                    <!-- popup form Number of tyres -->
										<div id="no_of_tyres_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content" style="width: 800px;">
										      <div class="modal-header">
										        <h4 class="popform_header">Tyre Detail </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
								<asp:Repeater ID="rptstck" runat="server" onitemdatabound="rptstck_ItemDataBound" >
                                <HeaderTemplate>
                                  <div class="clearfix odd_row">
								    <div class="col-md-2">Serial No <span class="required-field">*</span></div>
										    <div class="col-md-2" style="width: 21%;">Tyre Position <span class="required-field">*</span></div>
										    <div class="col-md-2" style="width: 22%;">Company <span class="required-field">*</span></div>
										    <div class="col-md-2" style="width: 12%;">Type <span class="required-field">*</span></div>
										    <div class="col-md-2">Purc. From <span class="required-field">*</span></div>
										    <div class="col-md-2" style="width: 11%;">Fitting KMS <span class="required-field">*</span></div>
									    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <div class="clearfix even_row">
                                <div class="col-md-2">
                                 <asp:TextBox ID="txtserialNo" runat="server" Text='<%# Eval("SerialNo") %>' CssClass="form-control"  TabIndex="41" MaxLength="20" placeholder="Enter Serial No"></asp:TextBox>
								</div>
                                <div class="col-md-2" style="width: 21%;">
		                        <asp:DropDownList ID="ddlPostion" TabIndex="42" runat="server" class="form-control" > </asp:DropDownList>
								</div>          
                                <div class="col-md-2" style="width: 22%;">
                                <asp:TextBox ID="txtCompanys" runat="server" TabIndex="43" Text='<%# Eval("Company") %>' CssClass="form-control"  MaxLength="20" placeholder="Company Name"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="width: 12%;">
								  <asp:DropDownList ID="ddlType" runat="server" TabIndex="44"  class="form-control"> 
                                <asp:ListItem Value="1" Text="New"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Old"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Retrited"></asp:ListItem>
                                </asp:DropDownList>			
								</div>             
                                <div class="col-md-2">
								 <asp:TextBox ID="txtPurParty" runat="server" TabIndex="45" Text='<%# Eval("PurFrom") %>' class="form-control"  MaxLength="20" placeholder="Pur. Party"></asp:TextBox>
								</div>              
                                <div class="col-md-2" style="width: 10%;">
								 <asp:TextBox ID="txtKms" runat="server" TabIndex="46" Text='<%# Eval("kms") %>'  CssClass="form-control" MaxLength="20" placeholder="KMs"></asp:TextBox>
								</div>       
                                </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
									</div>
								</section>
						</div>
						<div class="modal-footer">
						<div class="popup_footer_btn"> 
                        <asp:LinkButton ID="lnkTyreSerialNoSave" runat="server" CssClass="btn btn-dark" 
                                onclick="lnkTyreSerialNoSave_Click1"  ><i class="fa fa-save"></i>Save</asp:LinkButton>
										       
							<button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
                            <asp:Label ID="lblmsg" runat="server" CssClass="redfont"></asp:Label>
                            <asp:HiddenField ID="hiddenitemid" runat="server" />
						</div>
						</div>
					</div>
					</div>
				</div>
										
				   <!-- popup form Document holder -->
										<div id="document_holder_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Document Holder </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										            <!-- first main Column-->
											        	<div class="col-lg-8" style="padding: 0;  width: 64.7%;     margin-left: 0.15%; margin-right: 0.15%;">
											            <!-- first left row -->
											            <div class="clearfix first_left">
											              <section class="panel panel-in-default">
											                <div class="panel-body">
											                  <div class="clearfix odd_row">
											                  	<label class="col-sm-3 control-label">Doc. Name</label>
					                                <div class="col-sm-9">
					                                        <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control" Height="24px"
                                            MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                            onpaste="return false" placeholder="Enter Doc Name" TabIndex="27" Text=""></asp:TextBox>
					                                </div>
						                            </div>
											                  <div class="clearfix even_row">
					                              	<label class="col-sm-3 control-label">Remark</label>
					                                <div class="col-sm-9">				                                 
                                                    <asp:TextBox ID="txtDocRemark" runat="server" CssClass="form-control parsley-validated" Style="resize: none"
                                      MaxLength="100" oncopy="return false" oncut="return false" onDrop="blur();return false;"
                                            onpaste="return false" placeholder="Enter Remark" TabIndex="28" Text="" TextMode="MultiLine"></asp:TextBox>
					                                </div>
						                            </div>

											                </div>
											              </section>
											            </div> 
											          </div>
											          <!-- second main Column -->
											          <div class="col-lg-4" style="padding: 0; width: 34.7%; margin-left: 0.15%; margin-right: 0.15%;">
											            <!-- first_one left row -->
											            <div class="clearfix first_one_left">
											              <section class="panel panel-in-default">
											                <div class="panel-body">
											                	<div class="clearfix odd_row">
					                              	<div style="text-align: center;"> 
                                                         <asp:Image ID="imgEmp" runat="server" ImageUrl="img/placeholder.png" width="117px" height="117px" />
                                                          <asp:Label ID="lblimgError" runat="server" ForeColor="Red"></asp:Label>
					                              	</div>	                              	
						                            </div>
					                              <div class="clearfix even_row"> 
                                                      <asp:FileUpload ID="fuPicture" runat="server" onchange="ShowImagePreview(this);"  TabIndex="29" />
						                            </div>
											                </div>
											              </section>
											            </div>                        

											          </div>                          
										          </div>
										        </section>
										      </div>
                                              <div class="modal-body">
                                               <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
                                                  <asp:GridView ID="grdDocHolder" runat="server" AutoGenerateColumns="false" BorderStyle="None"
                                            OnPageIndexChanging="grdDocHolder_PageIndexChanging" Width="100%" GridLines="None"
                                            AllowPaging="true" PageSize="25" CssClass="display nowrap" OnRowCommand="grdDocHolder_RowCommand"
                                            BorderWidth="0" RowStyle-CssClass="even" AlternatingRowStyle-CssClass="odd"
                                            TabIndex="6" OnRowDataBound="grdDocHolder_RowDataBound">
                                            <HeaderStyle ForeColor="Black" CssClass="linearBg" />
                                            <AlternatingRowStyle CssClass="bgcolor2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="150">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("DocName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150" />
                                                    <ItemTemplate>
                                                        <%#Eval("Remark")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Image" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" class="preview" NavigateUrl='<%#Eval("Image")%>' runat="server"
                                                            Target="_blank">
                                                            <asp:Image Width="100" ID="Image1" ImageUrl='<%#Eval("Image")%>' runat="server" />
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="50" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/delete_sm.png"
                                                            CommandArgument='<%# Container.DataItemIndex%>' CommandName="cmddelete" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LblNoRecordFound" Text="Record(s) not found." runat="server" CssClass="white_bg"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="white_bg" ForeColor="#000" HorizontalAlign="Center" />
                                        </asp:GridView>
                                                   </div>
										        </section>
                                              </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
                                                    <asp:LinkButton ID="btnAdd" CssClass="btn btn-dark" runat="server" data-dismiss="modal"><i class="fa fa-check"></i>Ok</asp:LinkButton>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>   

   				  <!-- popup form  type -->
										<div id="type_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Lorry Detail (Trailer) </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Length</label>
                                    <div class="col-sm-8"> 
                                         <asp:TextBox ID="txtTrollyLength" runat="server" CssClass="form-control"
                                            MaxLength="10" placeholder="Enter Trolley Length" oncopy="return false" oncut="return false"
                                            onDrop="blur();return false;" onpaste="return false" TabIndex="16"
                                            Text=""></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Height</label>
                                    <div class="col-sm-8"> 
                                       <asp:TextBox ID="txtTrollyheight" runat="server" CssClass="form-control"
                                            MaxLength="10" placeholder="Enter Trolley Height" oncopy="return false" oncut="return false"
                                            onDrop="blur();return false;" onpaste="return false" TabIndex="17" Text="" ></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>
	                              <div class="clearfix even_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Weight</label>
                                    <div class="col-sm-8"> 
                                       <asp:TextBox ID="txtTrollyWeight" runat="server" CssClass="form-control" 
                                            MaxLength="10" placeholder="Enter Trolley Weight" oncopy="return false" oncut="return false"
                                            onDrop="blur();return false;" onpaste="return false"  TabIndex="18"
                                            Text=""></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">No.Of Tyres</label>
                                    <div class="col-sm-8"> 
                                        <asp:TextBox ID="txtTyresNo" runat="server" CssClass="form-control" 
                                            placeholder="Enter Tyres No." MaxLength="10" oncopy="return false" oncut="return false"
                                            onDrop="blur();return false;" onpaste="return false" TabIndex="19" Text="" ></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>	                             	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-check"></i>Save</button>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>  

				  <!-- popup form  Insurence No. -->
										<div id="insurence_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Insurance Details </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
	                              <div class="clearfix even_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">Ins. Comp. Name</label>
                                    <div class="col-sm-7">
                                               <asp:DropDownList ID="drpInsuCompName" runat="server" CssClass="form-control" Width="155px"
                                            Height="30px">
                                            <asp:ListItem Text="< Choose >" Value="0" Selected="True">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Company Name1" Value="1">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Company Name2" Value="2">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Warn Bef.</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtInsWarn" runat="server"  MaxLength="2"
                                            onDrop="blur();return false;" onpaste="return false" oncut="return false"  oncopy="return false" ></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>
	                              <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">Valid Till</label>
                                    <div class="col-sm-7">
                                      <asp:TextBox ID="txtValidDat" runat="server" Width="154px" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" MaxLength="10"
                                            ></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label"> Ins. Amount</label>
                                    <div class="col-sm-7">
                                         <asp:TextBox ID="txtInsurAmount" style="text-align:right;" runat="server" CssClass="form-control" MaxLength="13" Text="0.00"
                                             onDrop="blur();return false;" onpaste="return false"
                                            oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>

	                             	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-check"></i>Ok</button>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div> 

  <!-- popup form  Driver Creation Own -->
										<div id="DriverCreation" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">New Driver Creation (Own)</h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
	                              <div class="clearfix even_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">Driver Name</label>
                                    <div class="col-sm-7">
                                          <asp:TextBox ID="txtDriverName" runat="server" CssClass="form-control"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDriverName"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter DriverName" SetFocusOnError="true" ValidationGroup="DiverSave"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">Date Range</label>
                                    <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlDateRange" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    </div>
	                                </div>
	                              </div>
	                              <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">State</label>
                                    <div class="col-sm-7">
                                      <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"  AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select state" InitialValue="0" SetFocusOnError="true" ValidationGroup="DiverSave"></asp:RequiredFieldValidator>
                              
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label"> City</label>
                                    <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                            CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Select City"
                                            InitialValue="0" SetFocusOnError="true" ValidationGroup="DiverSave"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                              </div>

	                             	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
                                                <asp:LinkButton ID="lnkBtnSaveDriver" runat="server" class="btn btn-dark" 
                                                        ValidationGroup="DiverSave" onclick="lnkBtnSaveDriver_Click" CausesValidation="true"><i class="fa fa-check"></i>Save</asp:LinkButton>
										          
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div> 

<!-- popup form  Driver Creation Hirw -->
										<div id="DriverCreationHire" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">New Driver Creation (Hire)</h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
	                              <div class="clearfix even_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">Driver Name</label>
                                    <div class="col-sm-7">
                                          <asp:TextBox ID="txtDriverHire" runat="server" CssClass="form-control"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDriverHire"
                                        CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter DriverName" SetFocusOnError="true" ValidationGroup="DiverSaveHire"></asp:RequiredFieldValidator>
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-4 control-label">License</label>
                                    <div class="col-sm-8">
                                      <asp:TextBox ID="txtLicenseNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>
	                              

	                             	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
                                                <asp:LinkButton ID="lnkSaveDriverHire" runat="server" class="btn btn-dark" 
                                                        ValidationGroup="DiverSaveHire" CausesValidation="true" 
                                                        onclick="lnkSaveDriverHire_Click"><i class="fa fa-check"></i>Save</asp:LinkButton>
										          
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div> 
				  <!-- popup form  Financer Name -->
										<div id="financer_popup" class="modal fade">
										  <div class="modal-dialog">
										    <div class="modal-content">
										      <div class="modal-header">
										        <h4 class="popform_header">Financer Details </h4>
										      </div>
										      <div class="modal-body">
										        <section class="panel panel-default full_form_container material_search_pop_form">
										          <div class="panel-body">
										            <div class="clearfix odd_row">	                                
	                                <div class="col-sm-4">
	                                  <label class="control-label">Fin. Amount</label>
                                    <div>
                                        <asp:TextBox ID="txtFinancerAmount" runat="server"  CssClass="form-control"
                                            TabIndex="51" MaxLength="20" onDrop="blur();return false;" onpaste="return false"
                                            oncut="return false" oncopy="return false"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="control-label">EMT Amount</label>
                                    <div>
                                      <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" TabIndex="52"
                                            MaxLength="20" onDrop="blur();return false;" onpaste="return false" oncut="return false"
                                            oncopy="return false"></asp:TextBox>
                                    </div>
	                                </div>
	                                <div class="col-sm-4">
	                                  <label class="control-label">Total Installment</label>
                                    <div>
                                      <asp:TextBox ID="txtTotalInstallment" runat="server" CssClass="form-control" MaxLength="3"
                                            oncopy="return false" oncut="return false" onDrop="blur();return false;" onpaste="return false" TabIndex="53"></asp:TextBox>
                                    </div>
	                                </div>
	                              </div>	                              
	                              <div class="clearfix odd_row">	                                
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label">Fin. Period From</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtFinPeriodFrom" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" MaxLength="10"
                                            TabIndex="54"></asp:TextBox>
                                      
                                    </div>
	                                </div>
	                                <div class="col-sm-6">
	                                  <label class="col-sm-5 control-label"> Fin. Period To</label>
                                    <div class="col-sm-7">
                                     <asp:TextBox ID="txtFinPeriodTo" runat="server" CssClass="input-sm datepicker form-control" data-date-format="dd-mm-yyyy" MaxLength="10" 
                                             TabIndex="55"></asp:TextBox>
                                      
                                    </div>
	                                </div>
	                              </div>	                             	                              
										          </div>
										        </section>
										      </div>
										      <div class="modal-footer">
										        <div class="popup_footer_btn"> 
										          <button type="submit" class="btn btn-dark" data-dismiss="modal" tabindex="56"><i class="fa fa-check"></i>Ok</button>
										          <button type="submit" class="btn btn-dark" data-dismiss="modal"  tabindex="57"><i class="fa fa-times"></i>Close</button>
										        </div>
										      </div>
										    </div>
										  </div>
										</div>              
                  </form>
                </div>
              </section>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <asp:HiddenField ID="hidPartyIdno" runat="server" />
    <asp:HiddenField ID="hidlorryidno" runat="server" />
    <asp:HiddenField ID="dmindate" runat="server" />
    <asp:HiddenField ID="hidmaxdate" runat="server" />
    <%--        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function CheckParty() {
            __doPostBack('lnkParty', '');
        }

        function checkPicture1(img) {
            var myPopup = window.open(img.ImageUrl, img.ImageUrl, "width=200, height=100");
        }

        function checkPicture(img) {
            var image1 = document.getElementById(img);
            //alert(image1.ImageUrl);
            var url = prompt("change image source", image1.ImageUrl);
            image1.src = url;
        }

        function HideStck() {
            $("#dvStck").fadeOut(300);
        }
        function ShowStck() {
            $("#dvStck").fadeIn(300);

        }
        function HideBillAgainst() {
            $("#dvGrdetails").fadeOut(300);
        }

        function ShowClient() {
            $("#dvGrdetails").fadeIn(300);
        }


        function ShowClientLorry() {
            $("#dvTrolleydetails").fadeIn(300);
        }

        function LorryDetailsSave() {
            $("#dvTrolleydetails").fadeOut(300);
        }


        function OnChangeType() {
            if (document.getElementById("<%=ddllorytype.ClientID%>").value == "2") {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = false;
            }
            else {
                document.getElementById("<%=lnkLorryType.ClientID %>").disabled = true;
            }
        }

        function LorryDetailsCancle() {
            document.getElementById("<%=txtTrollyLength.ClientID%>").value = "";
            document.getElementById("<%=txtTrollyheight.ClientID%>").value = "";
            document.getElementById("<%=txtTrollyWeight.ClientID%>").value = "";
            document.getElementById("<%=txtTyresNo.ClientID%>").value = "";
            $("#dvTrolleydetails").fadeOut(300);
        }

        function InsuranceDetailsSave() {
            $("#dvTrolleydetails").fadeOut(300);
        }

        function InsuranceDetailsCancle() {
            document.getElementById("<%=drpInsuCompName.ClientID%>").value = "0";
            document.getElementById("<%=txtValidDat.ClientID%>").value = "";

            $("#dvTrolleydetails").fadeOut(300);
        }

        function FinancerdetailsSave() {
            $("#dvFinancerdetails").fadeOut(300);
        }

        function FinancerdetailsCancle() {
            document.getElementById("<%=txtFinancerAmount.ClientID%>").value = "";
            document.getElementById("<%=txtEMIAmount.ClientID%>").value = "";
            document.getElementById("<%=txtTotalInstallment.ClientID%>").value = "";
            document.getElementById("<%=txtFinPeriodFrom.ClientID%>").value = "";
            document.getElementById("<%=txtFinPeriodTo.ClientID%>").value = "";

            $("#dvFinancerdetails").fadeOut(300);
        }

        function OnChangeParty() {
            if (document.getElementById("<%=ddlPartyName.ClientID%>").value == "0") {
                document.getElementById("<%=lnkValidFrom.ClientID %>").disabled = true;
            }
            else {
                document.getElementById("<%=lnkValidFrom.ClientID %>").disabled = false;
            }
        }
        function HideDocumentHolder() {
            $("#dvDocumentHolder").fadeOut(300);
        }

        function ShowDocumentHolder() {
            $("#dvDocumentHolder").fadeIn(300);
        }

        function HideInsurancedetails() {
            $("#dvInsurancedetails").fadeOut(300);
        }

        function ShowInsurancedetails() {
            $("#dvInsurancedetails").fadeIn(300);
        }

        function HideFinancerdetails() {
            $("#dvFinancerdetails").fadeOut(300);
        }

        function ShowFinancerdetails() {
            $("#dvFinancerdetails").fadeIn(300);
        }



        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgEmp.ClientID%>').prop('src', e.target.result)
                        .width(120)
                        .height(110);
                };

                if (document.getElementById("<%=txtDocName.ClientID%>").value == "") {

                }
                else {


                    CheckParty();
                }
                reader.readAsDataURL(input.files[0]);

            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            setDatecontrol();
        });

        prm.add_endRequest(function () {
            setDatecontrol();
        });

        $(document).ready(function () {
            setDatecontrol();
        });
        function setDatecontrol() {
            var mindate = $('#<%=dmindate.ClientID%>').val();
            var maxdate = $('#<%=hidmaxdate.ClientID%>').val();
            $('#<%=txtFitValidDat.ClientID %>').datepicker({
                buttonImageOnly: false,
                mindate: mindate,
                maxDate: maxdate,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
            $('#<%=txtRCValidDat.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy',
                yearRange: "-14:+0"
            });
            $('#<%=txtNatPermitDate.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
            $('#<%=txtAuthDate.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });

            $('#<%=txtValidDat.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
            $('#<%=txtFinPeriodFrom.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
            $('#<%=txtFinPeriodTo.ClientID %>').datepicker({
                buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd-mm-yy'
            });
        }
        function CheckPAN() {
            var NEWW = $("#ContentPlaceHolder1_txtPanNo").val().toUpperCase();
            var O = NEWW.substring(3, 4)
            if (O == "P") {
                $("#ContentPlaceHolder1_ddlPANType").val("1");
            }
            else if (O == "C") {
                $("#ContentPlaceHolder1_ddlPANType").val("3");
            }
            else if (O == "H") {
                $("#ContentPlaceHolder1_ddlPANType").val("2");
            }
            else if (O == "F") {
                $("#ContentPlaceHolder1_ddlPANType").val("5");
            }
        }
        
        function openPartyModal() {
            $('#party_name_popup').modal('show');
        }
        function openPartyChangeModal() {
            $('#divPartyChange').modal('show');
        }
        function openTyreModal() {
            $('#no_of_tyres_popup').modal('show');
        }

        function ShowInsourance() {
            $('#insurence_popup').modal('show');
        }

        function ShowDriverCreation() {
            $('#DriverCreation').modal('show');
        }


        function ShowHireDriverCreation() {
            $('#DriverCreationHire').modal('show');
        }

        function ShowFinancer() {
            $('#financer_popup').modal('show');
        }


    </script>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
   
    </script>
</asp:Content>
