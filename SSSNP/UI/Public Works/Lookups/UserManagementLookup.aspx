<%@ Page Title="User Management Lookup" Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" CodeBehind="UserManagementLookup.aspx.cs" Inherits="SSSNP.UI.Public_Works.Lookups.UserManagementLookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script type="text/javascript">
        function SelectRow(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver) {
            window.opener.ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver);
            window.close();
        }
    </script>--%>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family:Poppins;">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;" valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">User Management Lookup</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="LabelUserName" runat="server" CssClass="LBLBLACK" Width="165px">User Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:</td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtFilterUserName" runat="server" CssClass="form-control txtbox" Width="180px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="LabelUserType" runat="server" CssClass="LBLBLACK" Width="165px">User Type</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:</td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtFilterUserType" runat="server" CssClass="form-control txtbox" Width="180px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="LabelEmail" runat="server" CssClass="LBLBLACK" Width="165px">Email</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:</td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtFilterEmail" runat="server" CssClass="form-control txtbox" Width="180px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" Text="Cancel" ToolTip="To Clear the Screen" Width="90px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
