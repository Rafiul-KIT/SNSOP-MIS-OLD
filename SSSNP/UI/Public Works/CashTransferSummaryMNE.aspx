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
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <!-- Geo Location Filters -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelState" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelCounty" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlCounty" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelBoma" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <!-- Other Filters -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelAge" runat="server" CssClass="LBLBLACK" Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelStatus" runat="server" CssClass="LBLBLACK" Width="165px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelComponent" runat="server" CssClass="LBLBLACK" Width="165px">Component</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlComponent" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="165px">Payment Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaidStatus" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
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
                                            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" CssClass="btn btn-primary"
                                                Height="32px" Width="110px" OnClientClick="return validateSearch();" />
                                            <asp:Button ID="BtnDownloadPdf" runat="server" Text="Download PDF" OnClick="BtnDownload_Pdf" CssClass="btn btn-primary"
                                                Height="32px" Visible="false"/>
                                            <asp:Button ID="BtnDownloadExl" runat="server" Text="Download Excel" OnClick="BtnDownload_Exl" CssClass="btn btn-primary"
                                                Height="32px" Visible="false" />
                                            <asp:Button ID="BtnClear" runat="server" Text="Clear" OnClick="BtnClear_Click" CssClass="btn btn-primary"
                                                Height="32px" Width="110px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <!-- Panel to contain the total fields, initially hidden  Visible="false"-->
                                            <asp:Panel ID="pnlTotals" runat="server" Visible="false">
                                                <asp:Label ID="lblTotalPaidBeneficiary" runat="server" CssClass="LBLBLACK" Text="Total Paid Beneficiary: 0" />
                                                <br />
                                                <asp:Label ID="lblTotalUnpaidBeneficiary" runat="server" CssClass="LBLBLACK" Text="Total Unpaid Beneficiary: 0" />
                                                <br />
                                                <asp:Label ID="lblTotalPaidAmount" runat="server" CssClass="LBLBLACK" Text="Total Paid Amount: 0.00" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="rgResults" runat="server" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="True" PageSize="10" GridLines="Both"
                                    OnNeedDataSource="rgResults_NeedDataSource" AllowCustomPaging="True">
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
                                            <telerik:GridBoundColumn DataField="ReqNo" HeaderText="Wage Request No">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Beneficiary Name">
                                                <HeaderStyle Width="20%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="household_number" HeaderText="Household Number">
                                                <HeaderStyle Width="25%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentCycle" HeaderText="Payment Cycle">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentStatus" HeaderText="Payment Status">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Component" HeaderText="Component">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentAmount" HeaderText="Payment Amount">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:MM/dd/yyyy}">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
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
            <asp:PostBackTrigger ControlID="BtnDownloadExl" />
            <asp:PostBackTrigger ControlID="BtnDownloadPdf" />
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
