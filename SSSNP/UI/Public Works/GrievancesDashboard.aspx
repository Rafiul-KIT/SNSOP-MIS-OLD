<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="GrievancesDashboard.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.GrievancesDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript">



    </script>


    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 30px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Grievances Status Report
                        </h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upPanelMain">
                        <ContentTemplate>
                            <div class="panel-body">
                                <table id="tbMain" runat="server" width="100%" cellpadding="4" cellspacing="5">

                                    <tr style="vertical-align: top">
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width: 95%">
                                                <tr>
                                                    <td>Type of Grievance by County
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                         <telerik:RadGrid ID="rgGRByCountyAll" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByCountyAll_NeedDataSource" OnItemDataBound="rgGRByCountyAll_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="County - Type" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Total" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px;">
                                            <table style="width: 95%">
                                                <tr>
                                                    <td>Grievance by Status
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRByStatusAll" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByStatusAll_NeedDataSource" OnItemDataBound="rgGRByStatusAll_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="Status - Type" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Total" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top">
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width: 95%">
                                                  <tr>
                                                    <td>Average days to close by County
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRAvgToCloseByCountyAll" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRAvgToCloseByCountyAll_NeedDataSource" OnItemDataBound="rgGRAvgToCloseByCountyAll_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="County - Type" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Avg Days to Close" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width: 95%">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSystemReport" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="btnSystemReport_Click" TabIndex="10" Text="Download System Report"
                                                ValidationGroup="group" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
