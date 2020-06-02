<%@ Page Title="Company Master" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="CompanyMast.aspx.cs" Inherits="WebTransport.CompanyMast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upMaster" runat="server">
        <ContentTemplate>
            <div class="row ">
                <div class="col-lg-5 center-block">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="animated-icon fas fa-gas-pump"></i>Company Master</h5>
                        </div>
                        <div class="ibox-content main-form scroll-pane">
                            <div class="col-sm-12 no-pad">
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Company Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please Enter Company !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Description </label>
                                        <asp:TextBox ID="txtCompDescription" runat="server" CssClass="form-control" MaxLength="200" TextMode="MultiLine" Style="resize: none;" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Address 1 <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtAdrs1" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAdrs1" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter address !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Address 2 </label>
                                        <asp:TextBox ID="txtAdrs2" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">State </label>
                                        <asp:DropDownList ID="ddlState" CssClass="form-control required" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">City </label>
                                        <asp:DropDownList ID="ddlCity" CssClass="form-control required" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Pin Code <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpincode" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Pincode !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Email ID <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtemailid" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revemailid" runat="server" ControlToValidate="txtemailid" CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Not a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Phone  </label>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Mobile  </label>
                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNumber" CssClass="classValidation" Display="Dynamic" ErrorMessage="Not a valid Mobile No!" SetFocusOnError="true" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Fax No.  </label>
                                        <asp:TextBox ID="txtFaxNo" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Tin No.  </label>
                                        <asp:TextBox ID="txtTinNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">GST No  </label>
                                        <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control validation-input" MaxLength="15" ClientIDMode="Static"></asp:TextBox>
                                        <span class="validation-tip">Please enter value between 15 - 16 characters.</span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGSTIN"
                                                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please enter GST TIN No!"
                                                                CssClass="classValidation"></asp:RequiredFieldValidator>   --%>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Proprietary Status  </label>
                                        <asp:TextBox ID="txtPStatus" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">CST No.  </label>
                                        <asp:TextBox ID="txtCSTNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">PAN No.  </label>
                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">TAN No.  </label>
                                        <asp:TextBox ID="txtTanNo" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Code No.  </label>
                                        <asp:TextBox ID="txtCodeNo" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Serv Tax No. </label>
                                        <asp:TextBox ID="txtServTaxNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServTaxNo"
                                                                Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Please enter Service Tax No!"
                                                                CssClass="classValidation"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Total Location </label>
                                        <asp:TextBox ID="txtTotLoc" runat="server" CssClass="form-control" MaxLength="14" ReadOnly="true" Text="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RfvtxtTotLoc" runat="server" ControlToValidate="txtTotLoc" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Total Location !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">SAP No. </label>
                                        <asp:TextBox ID="txtSapNo" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-6">
                                        <label class="control-label">Reg. No. </label>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">Contact Detail <span class="required-field">*</span></label>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Con. Person Name <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCntPrsnNm1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCntPrsnNm" runat="server" ControlToValidate="txtCntPrsnNm1" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Contact Name!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Mobile No. <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCntPrsnNo1" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCntPrsnNo" runat="server" ControlToValidate="txtCntPrsnNo1" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Mobile No. !" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCntPrsnNo1" runat="server" ControlToValidate="txtCntPrsnNo1" CssClass="classValidation" Display="Dynamic" ErrorMessage="Not a valid Mobile No!" SetFocusOnError="true" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Email Id. <span class="required-field">*</span> </label>
                                        <asp:TextBox ID="txtCntPrsnEmail1" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfCntPrsnEmail1" runat="server" ControlToValidate="txtCntPrsnEmail1" CssClass="classValidation" Display="Dynamic" ErrorMessage="Please enter Email ID.!" SetFocusOnError="true" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtCntPrsnEmail1" CssClass="classValidation" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Not a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-4">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="txtCntPrsnNm2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="txtCntPrsnNo2" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCntPrsnNo2" runat="server" ControlToValidate="txtCntPrsnNo2" CssClass="classValidation" Display="Dynamic" ErrorMessage="Not a valid Mobile No!" SetFocusOnError="true" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label"></label>
                                        <asp:TextBox ID="txtCntPrsnEmail2" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCntPrsnEmail2" runat="server" ControlToValidate="txtCntPrsnEmail2" CssClass="redfont" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="<br/>Not a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-12">
                                        <label class="control-label">SMS Detail <span class="required-field">*</span></label>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">User Name  </label>
                                        <asp:TextBox ID="txtSMSUserName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Password  </label>
                                        <asp:TextBox ID="txtSMSPassword" runat="server" CssClass="form-control" MaxLength="10" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Sender ID  </label>
                                        <asp:TextBox ID="txtSMSSenderID" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-control-row">
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Profile ID  </label>
                                        <asp:TextBox ID="txtSMSProfileID" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Auth. Type  </label>
                                        <asp:TextBox ID="txtSMSAuthType" runat="server" CssClass="form-control" Autocomplete="Off" MaxLength="1"></asp:TextBox>
                                    </div>
                                    <div class="control-container col-sm-4">
                                        <label class="control-label">Auth. Key  </label>
                                        <asp:TextBox ID="txtSMSAuthKey" runat="server" CssClass="form-control" Autocomplete="Off" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-btn">
                            <div class="pull-right">
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" CausesValidation="true" ValidationGroup="Save" OnClick="lnkbtnSave_OnClick" CssClass="btn btn-labeled btn-primary"><span class="btn-label"><i class="fa fa-save"></i></span>Save</asp:LinkButton>
                                </div>
                                <div class="display-inline">
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_OnClick" CssClass="btn btn-labeled btn-danger"><span class="btn-label"><i class="fa fa-times"></i></span>Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--HIDDEN FIELDS--%>
            <asp:HiddenField ID="hidValidGSTIN" runat="server" />
            <asp:HiddenField ID="hidcompidno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        $('#txtGSTIN').keyup(function () {
            CheckGSTLength();
        });
        function CheckGSTLength() {
            var len = $('#txtGSTIN').val().length;
            if (len != 0 && (len < 15 || len > 16)) {
                $('.validation-input').next('.validation-tip').show();
                setTimeout(function () { $('.validation-input').next('.validation-tip').hide(); }, 5000);
                $('#hidValidGSTIN').val('false');
            }
            else {
                $('#txtGSTIN').next('.validation-tip').hide();
                $('#hidValidGSTIN').val('true');
            }
        }
    </script>
</asp:Content>
