<%@ Page Title="User Management Lookup" Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" CodeBehind="UserManagementLookup.aspx.cs" Inherits="SSSNP.UI.Public_Works.Lookups.UserManagementLookup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function SelectRow(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver, createdBy, createdDt, modifiedBy, modifiedDt) {
            window.opener.ApplyChanges(userId, userName, userType, userCode, status, email, gbvUser, ipApprover, emailReceiver, createdBy, createdDt, modifiedBy, modifiedDt);
            window.close();
        }
    </script>

    <style>
        /* Increase the font size across the entire page */
        body, td, th, input, select, textarea {
            /*font-family: 'Poppins', sans-serif;*/ /* Keeping the same font-family */
            font-size: 16px; /* Increase the font size for better readability */
        }

        /* Apply the increased font size to RadGrid as well */
        .RadGrid {
            /*font-family: 'Poppins', sans-serif;*/
            font-size: 16px;
        }
    </style>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">User Management Lookup</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                            <tr>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 50%">
                                        <!-- User Name Filter -->
                                        <tr>
                                            <td><asp:Label ID="LabelUserName" runat="server" CssClass="LBLBLACK" Width="165px">User Name</asp:Label></td>
                                            <td>:</td>
                                            <td><asp:TextBox ID="txtFilterUserName" runat="server" CssClass="form-control txtbox" Width="180px" /></td>
                                        </tr>
                                        <!-- User Type Filter -->
                                        <tr>
                                            <td><asp:Label ID="LabelUserType" runat="server" CssClass="LBLBLACK" Width="165px">User Type</asp:Label></td>
                                            <td>:</td>
                                            <td><asp:TextBox ID="txtFilterUserType" runat="server" CssClass="form-control txtbox" Width="180px" /></td>
                                        </tr>
                                        <!-- Email Filter -->
                                        <tr>
                                            <td><asp:Label ID="LabelEmail" runat="server" CssClass="LBLBLACK" Width="165px">Email</asp:Label></td>
                                            <td>:</td>
                                            <td><asp:TextBox ID="txtFilterEmail" runat="server" CssClass="form-control txtbox" Width="180px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <!-- Search and Cancel Buttons -->
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" Text="Cancel" ToolTip="To Clear the Screen" Width="90px" />
                                </td>
                            </tr>
                            <!-- Telerik RadGrid for Search Results -->
                            <tr>
                                <td align="center">
                                    <telerik:RadGrid ID="RadGridDetails" runat="server" AllowPaging="True" PageSize="10" AutoGenerateColumns="False" OnNeedDataSource="RadGridDetails_NeedDataSource" OnItemCommand="RadGridDetails_ItemCommand">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select"
                                                             CommandArgument='<%# $"{Eval("User_id")}|{Eval("User_name")}|{Eval("User_type")}|{Eval("User_code")}|{Eval("Status")}|{Eval("Email")}|{Eval("GBVUser")}|{Eval("IP_Approver")}|{Eval("email_receiver")}|{Eval("Created_by")}|{Eval("Created_dt")}|{Eval("Modified_by")}|{Eval("Modified_dt")}" %>'>Select</asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="User_id" HeaderText="User ID" />
                                                <telerik:GridBoundColumn DataField="User_name" HeaderText="User Name" />
                                                <telerik:GridBoundColumn DataField="User_type" HeaderText="User Type" />
                                                <telerik:GridBoundColumn DataField="User_code" HeaderText="User Code" />
                                                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                                                <telerik:GridBoundColumn DataField="Email" HeaderText="Email" />
                                                <telerik:GridBoundColumn DataField="GBVUser" HeaderText="GBV User" />
                                                <telerik:GridBoundColumn DataField="IP_Approver" HeaderText="IP Approver" />
                                                <telerik:GridBoundColumn DataField="email_receiver" HeaderText="Email Receiver" />
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
