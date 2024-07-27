<%@ Page Title=":: :: SSSNP : : Cash Transfer Summary" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="CashTransferSummaryMNE" CodeBehind="CashTransferSummaryMNE.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#txtStartDate').datetimepicker();
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
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Cash Transfer Summary</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Cash Transfer Summary</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <!-- Geo Location Filters -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelState" runat="server" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlState" runat="server" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelCounty" runat="server" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlCounty" runat="server" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPayam" runat="server" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelBoma" runat="server" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <!-- Other Filters -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelAge" runat="server" Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" Width="180px">
                                                            <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="1">Payment Cycle 1</asp:ListItem>
                                                            <asp:ListItem Value="2">Payment Cycle 2</asp:ListItem>
                                                            <asp:ListItem Value="3">Payment Cycle 3</asp:ListItem>
                                                            <asp:ListItem Value="4">Payment Cycle 4</asp:ListItem>
                                                            <asp:ListItem Value="5">Payment Cycle 5</asp:ListItem>
                                                            <asp:ListItem Value="6">Payment Cycle 6</asp:ListItem>
                                                            <asp:ListItem Value="7">Payment Cycle 7</asp:ListItem>
                                                            <asp:ListItem Value="8">Payment Cycle 8</asp:ListItem>
                                                            <asp:ListItem Value="9">Payment Cycle 9</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelStatus" runat="server" Width="165px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="180px">
                                                            <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="">IP Applied</asp:ListItem>
                                                            <asp:ListItem Value="1">IP Approver Approved</asp:ListItem>
                                                            <asp:ListItem Value="2">Initial Reviewer Approved</asp:ListItem>
                                                            <asp:ListItem Value="3">Finantial Approver Approved</asp:ListItem>
                                                            <asp:ListItem Value="4">Final Payment Authorizer Approved</asp:ListItem>
                                                            <asp:ListItem Value="5">Reconciled</asp:ListItem>
                                                            <asp:ListItem Value="11">IP Approver Rejected</asp:ListItem>
                                                            <asp:ListItem Value="12">Initial Reviewer Rejected</asp:ListItem>
                                                            <asp:ListItem Value="13">Finantial Approver Rejected</asp:ListItem>
                                                            <asp:ListItem Value="14">Final Payment Authorizer Rejected</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelComponent" runat="server" Width="165px">Component</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlComponent" runat="server" Width="180px">
                                                            <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="LIPW">LIPW</asp:ListItem>
                                                            <asp:ListItem Value="DIS">DIS</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelFromDate" runat="server" Width="165px">From Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="180px" style="color: gray;" placeholder="Select a date" onfocus="this.style.color='black';" onblur="if(this.value=='') this.style.color='gray';"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="txtFromDate"
                                                            TargetControlID="txtFromDate"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelToDate" runat="server" Width="165px">To Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="180px" style="color: gray;" placeholder="Select a date" onfocus="this.style.color='black';" onblur="if(this.value=='') this.style.color='gray';"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="txtToDate"
                                                            TargetControlID="txtToDate"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" Width="110px" OnClientClick="return validateSearch();" />
                                            <asp:Button ID="BtnDownload" runat="server" Text="Download" OnClick="BtnDownload_Click" Width="110px" Visible="false"/>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="rgResults" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                                    OnNeedDataSource="rgResults_NeedDataSource" AllowCustomPaging="True">
                                    <MasterTableView AllowPaging="True" PagerStyle-AlwaysVisible="True" AllowCustomPaging="True">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="intPaymentId" HeaderText="Payment ID"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentCycle" HeaderText="Payment Cycle"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentStatus" HeaderText="Payment Status"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Component" HeaderText="Component"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentAmount" HeaderText="Payment Amount"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="BtnDownload" />
        </Triggers>

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
