<%@ Page Title=":: :: SSSNP : : User Management" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="SSSNP.UI.Public_Works.UserManagement" %>

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
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td>User ID:</td>
                                        <td><asp:TextBox ID="txtUserId" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>User Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control txtbox" />
                                            <asp:Button ID="btnLookup" runat="server" CssClass="btn btn-primary" Text="Look Up" OnClientClick="return OpenLookupPopup();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>User Type:</td>
                                        <td><asp:TextBox ID="txtUserType" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>User Code:</td>
                                        <td><asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>Status:</td>
                                        <td><asp:TextBox ID="txtStatus" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>Email:</td>
                                        <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>GBV User:</td>
                                        <td><asp:TextBox ID="txtGBVUser" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>IP Approver:</td>
                                        <td><asp:TextBox ID="txtIPApprover" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                    <tr>
                                        <td>Email Receiver:</td>
                                        <td><asp:TextBox ID="txtEmailReceiver" runat="server" CssClass="form-control txtbox" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                function OpenLookupPopup() {
                    window.open('LookupPage.aspx', 'Lookup', 'scrollbars=yes,resizable=yes,width=1000,height=650');
                    return false;
                }

                function ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver) {
                    document.getElementById('<%= txtUserId.ClientID %>').value = userId;
                    document.getElementById('<%= txtUserName.ClientID %>').value = userName;
                    document.getElementById('<%= txtUserType.ClientID %>').value = userType;
                    document.getElementById('<%= txtUserCode.ClientID %>').value = userCode;
                    document.getElementById('<%= txtStatus.ClientID %>').value = status;
                    document.getElementById('<%= txtEmail.ClientID %>').value = email;
                    document.getElementById('<%= txtGBVUser.ClientID %>').value = gbvUser;
                    document.getElementById('<%= txtIPApprover.ClientID %>').value = ipApprover;
                    document.getElementById('<%= txtEmailReceiver.ClientID %>').value = emailReceiver;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

