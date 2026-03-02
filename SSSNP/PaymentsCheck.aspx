<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/ccmaster.master" AutoEventWireup="true" Inherits="PaymentsCheck" Title=":: SSSNP Online Management System ::" CodeBehind="PaymentsCheck.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function GetRowValue(val) {
            if (val != '&nbsp;') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;

            }
            window.opener.document.forms[0].submit();
            self.close();

        }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Payments Check</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600" Visible="true" Text="Page version 2022/05/31"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="telGridDetails" PageSize="1000" AutoGenerateColumns="false" runat="server" AllowPaging="true"
                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                        MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                        OnNeedDataSource="telGridDetails_NeedDataSource"
                                        OnItemDataBound="telGridDetails_ItemDataBound"
                                        ShowFooter="true">
                                        <HeaderStyle CssClass="MyGridHeader" />
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <ClientSettings Selecting-AllowRowSelect="true">
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                            <Selecting AllowRowSelect="True" />
                                            <ClientEvents OnRowSelected="rowSelected" />
                                        </ClientSettings>
                                        <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader">
                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                            <HeaderStyle CssClass="MyGridHeader" />
                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="BomaID" Visible="true" HeaderText="BomaID"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PaymentsFromAPI" HeaderText="Pyaments from Biometrics"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PaymentsRegistered" HeaderText="Payments in SSSNP"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PaymentsDifference" HeaderText="Payments Difference"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px"></td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px"></td>
                            </tr>

                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
