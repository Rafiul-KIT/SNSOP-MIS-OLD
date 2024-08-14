<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" CodeBehind="UserManagementLookup.aspx.cs" Inherits="SSSNP.UI.Public_Works.Lookups.UserManagementLookup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
        <tr>
            <td>User Name:</td>
            <td><asp:TextBox ID="txtFilterUserName" runat="server" CssClass="form-control txtbox" /></td>
        </tr>
        <tr>
            <td>User Type:</td>
            <td><asp:TextBox ID="txtFilterUserType" runat="server" CssClass="form-control txtbox" /></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtFilterEmail" runat="server" CssClass="form-control txtbox" /></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="grdDetails_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# $"{Eval("UserId")}|{Eval("UserName")}|{Eval("UserType")}|{Eval("UserCode")}|{Eval("Status")}|{Eval("Email")}|{Eval("GBVUser")}|{Eval("IPApprover")}|{Eval("EmailReceiver")}" %>'>Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserId" HeaderText="User ID" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <!-- Add other relevant fields -->
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function SelectRow(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver) {
            window.opener.ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver);
            window.close();
        }
    </script>
</asp:Content>
