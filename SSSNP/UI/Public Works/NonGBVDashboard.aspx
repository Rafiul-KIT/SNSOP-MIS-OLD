<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="NonGBVDashboard.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.NonGBVDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    landing<script type="text/javascript">



</script><table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 30px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Non-GBV Dashboard
                        </h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upPanelMain">
                        <ContentTemplate>
                            <div class="panel-body">
                                <table id="tbMain" runat="server" width="100%" cellpadding="4" cellspacing="5">
                                    <tr style="vertical-align:top">
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width:95%">
                                                <tr>
                                                    <td>Grievances by Status 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRByStatus" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByStatus_NeedDataSource" OnItemDataBound="rgGRByStatus_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="Status" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Count" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width:95%">
                                                <tr>
                                                    <td>Grievances by type
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                      <telerik:RadGrid ID="rgGRByType" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByType_NeedDataSource" OnItemDataBound="rgGRByType_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="Type" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Count" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>  
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align:top">
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width:95%">
                                                <tr>
                                                    <td>Grievances by location
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRByLocation" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByLocation_NeedDataSource" OnItemDataBound="rgGRByLocation_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="StateName" HeaderText="State" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CountyName" HeaderText="County" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="GRsCounter" HeaderText="Grievances" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px;" >
                                            <table style="width:95%">
                                                <tr>
                                                    <td>Average resolution time by location
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRByLocationResTimeAVG" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByLocationResTimeAVG_NeedDataSource" OnItemDataBound="rgGRByLocationResTimeAVG_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="StateName" HeaderText="State" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CountyName" HeaderText="County" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="GRsCounter" HeaderText="Average Resolution Time" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align:top">
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width:95%">
                                                <tr>
                                                    <td>Delayed grievances by type (14 days)
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="rgGRByTypeDelayed" AutoGenerateColumns="false" runat="server" Visible="true"
                                                            AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                            MasterTableView-TableLayout="Fixed"
                                                            OnNeedDataSource="rgGRByTypeDelayed_NeedDataSource" OnItemDataBound="rgGRByTypeDelayed_ItemDataBound">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <MasterTableView TableLayout="Fixed"
                                                                DataKeyNames=""
                                                                ClientDataKeyNames="">
                                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="DisplayName" HeaderText="Type" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DisplayValue" HeaderText="Count" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">
                                            <table style="width:95%">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <table id="tbDetailsList" width="98%" cellpadding="4" cellspacing="5">
                                <tr align="center">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="center" style="padding: 5px; margin: 5px">&nbsp;</td>
                                </tr>

                                <tr align="center">
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
