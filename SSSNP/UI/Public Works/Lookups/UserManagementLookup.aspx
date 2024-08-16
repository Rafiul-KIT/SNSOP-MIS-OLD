<%@ Page Title=":: SSSNP Online Management System ::" Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" CodeBehind="UserManagementLookup.aspx.cs" Inherits="SSSNP.UI.Public_Works.Lookups.UserManagementLookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function SelectRow(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver) {
            window.opener.ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver);
            window.close();
        }
    </script>

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
                                <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <!-- Additional fields if needed -->
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" Text="Cancel" ToolTip="To Clear the Screen" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <telerik:RadGrid ID="RadGridDetails" runat="server" AllowPaging="True" PageSize="10"
                                        OnNeedDataSource="RadGridDetails_NeedDataSource"
                                        OnItemCommand="RadGridDetails_ItemCommand" AutoGenerateColumns="False" AllowSorting="True">
                                        <MasterTableView DataKeyNames="UserId" TableLayout="Fixed">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="SelectColumn" HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" Text="Select" 
                                                            CommandArgument='<%# $"{Eval("UserId")}|{Eval("UserName")}|{Eval("UserType")}|{Eval("UserCode")}|{Eval("Status")}|{Eval("Email")}|{Eval("GBVUser")}|{Eval("IPApprover")}|{Eval("EmailReceiver")}" %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="UserId" HeaderText="User ID" UniqueName="UserId" />
                                                <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" UniqueName="UserName" />
                                                <telerik:GridBoundColumn DataField="UserType" HeaderText="User Type" UniqueName="UserType" />
                                                <telerik:GridBoundColumn DataField="UserCode" HeaderText="User Code" UniqueName="UserCode" />
                                                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status" />
                                                <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" />
                                                <telerik:GridBoundColumn DataField="GBVUser" HeaderText="GBV User" UniqueName="GBVUser" />
                                                <telerik:GridBoundColumn DataField="IPApprover" HeaderText="IP Approver" UniqueName="IPApprover" />
                                                <telerik:GridBoundColumn DataField="EmailReceiver" HeaderText="Email Receiver" UniqueName="EmailReceiver" />
                                            </Columns>
                                        </MasterTableView>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
