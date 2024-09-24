<%@ Page Title=":: :: SNSOP : : View Payment Cycle Documents" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="ViewPaymentCycleDocuments" CodeBehind="ViewPaymentCycleDocuments.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
        });

        function validateSearch() {
            return true;
        }

    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="">
                        <i class="fa fa-fw fa-table"></i> Payment Cycle Docs
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">View Documents</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">View Payment Cycle Documents</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <!-- Geo Location Filters -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelYear" runat="server" CssClass="LBLBLACK" Width="165px">Year</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" class="form-control txtbox" Height="33px" Width="180px">
                                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                            <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                            <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPaymentCycle" runat="server" CssClass="LBLBLACK" Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox" Height="33px" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" CssClass="btn btn-primary"
                                                Height="32px" Width="110px" OnClientClick="return validateSearch();" />
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="rgResults" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" GridLines="Both" PageSize="100"
                                    OnNeedDataSource="rgResults_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound" AllowCustomPaging="True">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <HeaderStyle CssClass="MyGridHeader" />
                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                    <ClientSettings Selecting-AllowRowSelect="true">
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" AllowResizeToFit="true" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader" AllowPaging="True" PagerStyle-AlwaysVisible="True" AllowCustomPaging="True">
                                        <HeaderStyle CssClass="MyGridHeader" />
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="5%" ItemStyle-Width="5%" DataField="Year" HeaderText="Year"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="10%" ItemStyle-Width="10%" DataField="PaymentCycle" HeaderText="Payment Cycle"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="24%" ItemStyle-Width="24%" DataField="Document1" HeaderText="1st Document" UniqueName="Doc1Column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="23%" ItemStyle-Width="23%" DataField="Document2" HeaderText="2nd Document" UniqueName="Doc2Column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="23%" ItemStyle-Width="23%" DataField="Document3" HeaderText="3rd Document" UniqueName="Doc3Column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="15%" ItemStyle-Width="15%" DataField="UploadDate" HeaderText="Uploaded At"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                    <img alt="" src="../../Resource/Images/Processing.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
