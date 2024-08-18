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
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="LabelSearch" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnLookup" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClientClick="return OpenLookupPopup();" Text="Look Up" CausesValidation="False"
                                                Font-Size="12px" Style="position: static" ToolTip="Lookup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserId" runat="server" CssClass="LBLBLACK" Width="165px">User ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserName" runat="server" CssClass="LBLBLACK" Width="165px">User Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserType" runat="server" CssClass="LBLBLACK" Width="165px">User Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserType" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelUserCode" runat="server" CssClass="LBLBLACK" Width="165px">User Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelEmail" runat="server" CssClass="LBLBLACK" Width="165px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelStatus" runat="server" CssClass="LBLBLACK" Width="165px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelGBVUser" runat="server" CssClass="LBLBLACK" Width="165px">GBV User</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtGBVUser" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelIPApprover" runat="server" CssClass="LBLBLACK" Width="165px">IP Approver</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtIPApprover" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelEmailReceiver" runat="server" CssClass="LBLBLACK" Width="165px">Email Receiver</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEmailReceiver" runat="server" CssClass="form-control txtbox" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

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

                function ApplyChanges(userid, username, usertype, usercode, status, email, gbvuser, ipapprover, emailreceiver) {
                    document.getElementById('<%= txtUserId.ClientID %>').value = userid;
                    document.getElementById('<%= txtUserName.ClientID %>').value = username;
                    document.getElementById('<%= txtUserType.ClientID %>').value = usertype;
                    document.getElementById('<%= txtUserCode.ClientID %>').value = usercode;
                    document.getElementById('<%= txtStatus.ClientID %>').value = status;
                    document.getElementById('<%= txtEmail.ClientID %>').value = email;
                    document.getElementById('<%= txtGBVUser.ClientID %>').value = gbvuser;
                    document.getElementById('<%= txtIPApprover.ClientID %>').value = ipapprover;
                    document.getElementById('<%= txtEmailReceiver.ClientID %>').value = emailreceiver;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
