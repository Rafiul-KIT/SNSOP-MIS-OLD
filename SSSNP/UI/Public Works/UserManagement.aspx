
 <%@ Page Title=":: :: SSSNP : : User Management" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.UserManagement" Async ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">System User Management</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">System User Management</h3>
                            </div>
                            <div class="panel-body">
                                <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" DisplayMode="BulletList" HeaderText="Please fix the following errors:" />--%>
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                   <tr>
                                       <td colspan="2" align="center" style="text-align: center; padding: 5px; margin: 5px;">
                                           <asp:Label ID="LabelSearch" runat="server" CssClass="LBLBLACK">Search&emsp;:</asp:Label>
                                           &nbsp;&nbsp;
           
                                           <asp:Button ID="btnLookup" runat="server" CssClass="btn btn-primary" Height="32px"
                                               OnClientClick="return OpenLookupPopup();" Text="Look Up" CausesValidation="False"
                                               Font-Size="12px" Style="position: static" ToolTip="Lookup"></asp:Button>
                                       </td>
                                   </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <!-- User ID Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserId" runat="server" CssClass="LBLBLACK" Width="165px">User ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="50" />

                                                        <!-- Required Field Validator -->
                                                        <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                                                            ErrorMessage="Please Enter User ID" CssClass="text-danger" Display="Dynamic"><span style="color: red;">&emsp;*</span>
                                                        </asp:RequiredFieldValidator>

                                                       <%-- <!-- Regular Expression Validator for No Spaces -->
                                                        <asp:RegularExpressionValidator ID="revUserId" runat="server" ControlToValidate="txtUserId"
                                                            ErrorMessage="Spaces are not allowed in User ID" ValidationExpression="^\S+$"
                                                            CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                    </td>
                                                </tr>
                                                <!-- User Name Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserName" runat="server" CssClass="LBLBLACK" Width="165px">User Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="100" />
                                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                                            ErrorMessage="Please Enter User Name" CssClass="text-danger" Display="Dynamic"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <!-- User Type Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserType" runat="server" CssClass="LBLBLACK" Width="165px">User Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <div style="display: flex; align-items: center;">
                                                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" Width="180px">
                                                            <asp:ListItem Text="--Select--" Value="" />
                                                            <asp:ListItem Text="Cordinator" Value="Cordinator" />
                                                            <asp:ListItem Text="Manager" Value="Manager" />
                                                            <asp:ListItem Text="IP" Value="IP" />
                                                            <asp:ListItem Text="QualEng" Value="QualEng" />
                                                            <asp:ListItem Text="TST" Value="TST" />
                                                            <asp:ListItem Text="ManagerGRM" Value="ManagerGRM" />
                                                            <asp:ListItem Text="Adm" Value="Adm" />
                                                            <asp:ListItem Text="Specialist" Value="Specialist" />
                                                            <asp:ListItem Text="WB" Value="WB" />
                                                            <asp:ListItem Text="EnvSpec" Value="EnvSpec" />
                                                            <asp:ListItem Text="GenderSpec" Value="GenderSpec" />
                                                            <asp:ListItem Text="SocDevSpec" Value="SocDevSpec" />
                                                            <asp:ListItem Text="PcuSpec" Value="PcuSpec" />
                                                            <asp:ListItem Text="FSP" Value="FSP" />
                                                            <asp:ListItem Text="Report" Value="Report" />
                                                            <asp:ListItem Text="MIS" Value="MIS" />
                                                            <asp:ListItem Text="EntryGRM" Value="EntryGRM" />
                                                            <asp:ListItem Text="WBGBVMgr" Value="WBGBVMgr" />
                                                        </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType"
                                                            InitialValue="" ErrorMessage="Please select User Type" CssClass="text-danger" Display="Dynamic">
                                                                <span style="color: red;">&nbsp;&emsp;*</span>
                                                            </asp:RequiredFieldValidator>
                                                       </div>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <!-- User Code Field -->
                                                <tr>
                                                        <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserCode" runat="server" CssClass="LBLBLACK" Width="165px">User Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="10" />

                                                        <!-- Required Field Validator -->
                                                        <asp:RequiredFieldValidator ID="rfvUserCode" runat="server" ControlToValidate="txtUserCode"
                                                            ErrorMessage="Please Enter User Code" CssClass="text-danger" Display="Dynamic"><span style="color: red;">&emsp;*</span>
                                                        </asp:RequiredFieldValidator>

                                                        <!-- Regular Expression Validator for Integers -->
                                                        <asp:RegularExpressionValidator ID="revUserCode" runat="server" ControlToValidate="txtUserCode"
                                                            ErrorMessage="Please Enter a valid User Code" ValidationExpression="^\d+$"
                                                            CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <!-- Email Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelEmail" runat="server" CssClass="LBLBLACK" Width="165px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="150" ToolTip='<%# Eval("Email") %>' />
                                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please Enter Your Email" CssClass="text-danger" Display="Dynamic"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic"
                                                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <!-- Status Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelStatus" runat="server" CssClass="LBLBLACK" Width="165px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <div style="display: flex; align-items: center;">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="180px">
                                                            <asp:ListItem Text="Select" Value="" />
                                                            <asp:ListItem Text="Yes" Value="Y" />
                                                            <asp:ListItem Text="No" Value="N" />
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus"
                                                            InitialValue="" ErrorMessage="Please Select Status" CssClass="text-danger" Display="Dynamic">
                                                            <span style="color: red;">&emsp;*</span>
                                                        </asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <!-- GBV User Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelGBVUser" runat="server" CssClass="LBLBLACK" Width="165px">GBV User</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RadioButtonList ID="rblGBVUser" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <!-- IP Approver Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelIPApprover" runat="server" CssClass="LBLBLACK" Width="165px">IP Approver</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RadioButtonList ID="rblIPApprover" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <!-- Email Receiver Field -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelEmailReceiver" runat="server" CssClass="LBLBLACK" Width="165px">Email Receiver</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RadioButtonList ID="rblEmailReceiver" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <!-- Additional hidden fields -->
                                                <asp:HiddenField ID="hfCreatedBy" runat="server" />
                                                <asp:HiddenField ID="hfCreatedDt" runat="server" />
                                                <asp:HiddenField ID="hfModifiedBy" runat="server" />
                                                <asp:HiddenField ID="hfModifiedDt" runat="server" />
                                                <asp:HiddenField ID="hfOriginalEmail" runat="server" />
                                            </table>
                                        </td>
                                    </tr>
                                    <!-- Save and Clear Buttons -->
                                    <tr>
                                        <td colspan="2" align="center" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="Clear" OnClick="btnClear_Click" CausesValidation="False" OnClientClick="clearValidationMessages();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- JavaScript Functions -->
            <script type="text/javascript">
                var popupWindow;

                function OpenLookupPopup() {
                    popupWindow = window.open('Lookups/UserManagementLookup.aspx', 'Lookup', 'scrollbars=yes,resizable=yes,width=1000,height=650');
                    return false;
                }

                window.onbeforeunload = function () {
                    if (popupWindow && !popupWindow.closed) {
                        popupWindow.close();
                    }
                };

                function ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver, createdBy, createdDt, modifiedBy, modifiedDt) {
                    document.getElementById('<%= txtUserId.ClientID %>').value = userId;
                    document.getElementById('<%= txtUserName.ClientID %>').value = userName;
                    document.getElementById('<%= ddlUserType.ClientID %>').value = userType;
                    document.getElementById('<%= txtUserCode.ClientID %>').value = userCode;
                    document.getElementById('<%= ddlStatus.ClientID %>').value = status;
                    document.getElementById('<%= txtEmail.ClientID %>').value = email;

                    // Set GBV User radio button
                    if (gbvUser === "True") {
                        document.getElementById('<%= rblGBVUser.ClientID %>_0').checked = true;
                        document.getElementById('<%= rblGBVUser.ClientID %>_1').checked = false;
                    } else if (gbvUser === "False") {
                        document.getElementById('<%= rblGBVUser.ClientID %>_1').checked = true;
                        document.getElementById('<%= rblGBVUser.ClientID %>_0').checked = false;
                    } else {
                        document.getElementById('<%= rblGBVUser.ClientID %>_0').checked = false;
                        document.getElementById('<%= rblGBVUser.ClientID %>_1').checked = false;
                    }

                    // Set IP Approver radio button
                    if (ipApprover === "True") {
                        document.getElementById('<%= rblIPApprover.ClientID %>_0').checked = true;
                        document.getElementById('<%= rblIPApprover.ClientID %>_1').checked = false;
                    } else if (ipApprover === "False") {
                        document.getElementById('<%= rblIPApprover.ClientID %>_1').checked = true;
                        document.getElementById('<%= rblIPApprover.ClientID %>_0').checked = false;
                    } else {
                        document.getElementById('<%= rblIPApprover.ClientID %>_0').checked = false;
                        document.getElementById('<%= rblIPApprover.ClientID %>_1').checked = false;
                    }

                    // Set Email Receiver radio button
                    if (emailReceiver === "1") {
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_0').checked = true;
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_1').checked = false;
                    } else if (emailReceiver === "2") {
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_1').checked = true;
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_0').checked = false;
                    } else {
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_0').checked = false;
                        document.getElementById('<%= rblEmailReceiver.ClientID %>_1').checked = false;
                    }

                    // Populate the hidden fields with the created and modified details
                    document.getElementById('<%= hfCreatedBy.ClientID %>').value = createdBy;
                    document.getElementById('<%= hfCreatedDt.ClientID %>').value = createdDt;
                    document.getElementById('<%= hfModifiedBy.ClientID %>').value = modifiedBy;
                    document.getElementById('<%= hfModifiedDt.ClientID %>').value = modifiedDt;

                    // Store the original email
                    document.getElementById('<%= hfOriginalEmail.ClientID %>').value = email;

                    // Clear any validation messages
                    clearValidationMessages();
                }

                /*// Client-side validation
                 * 
                 * OnClientClick="return validateForm();"
                 * 
                function validateForm() {
                    var userId = document.getElementById('*/<%= txtUserId.ClientID %>/*').value;
                    var userName = document.getElementById('*/<%= txtUserName.ClientID %>/*').value;
                    var userType = document.getElementById('*/<%= ddlUserType.ClientID %>/*').value;
                    var userCode = document.getElementById('*/<%= txtUserCode.ClientID %>/*').value;
                    var email = document.getElementById('*/<%= txtEmail.ClientID %>/*').value;

                    if (!userId || !userName || !userType || !userCode || !email) {
                        alert("Please fill in all required fields.");
                        return false;
                    }

                    return true;
                }*/

                function clearValidationMessages() {
                    var validators = document.getElementsByClassName('text-danger');
                    for (var i = 0; i < validators.length; i++) {
                        validators[i].style.display = 'none';
                    }
                }
            </script>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnLookup" />
            <asp:PostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                    <img alt="" src="../../Resource/Images/Processing.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
