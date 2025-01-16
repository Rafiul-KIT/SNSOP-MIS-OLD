<%@ Page Title=":: :: SNSOP : : Payroll Download" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="PayrollDownload" CodeBehind="PayrollDownload.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#txtStartDate').datetimepicker();
        });

    </script>

    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
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
                        <i class="fa fa-edit"></i><a href="#">Payroll Download</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Payroll Download</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">

                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
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
                                            </table>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <%--<tr>
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
                                                </tr>--%>
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
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelSupportType" runat="server" CssClass="LBLBLACK" Width="165px">Support Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlSupportType" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSearch_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="110px" />
                                            <asp:Button ID="BtnDownload" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnDownload_Click" TabIndex="10" Text="Download"
                                                ValidationGroup="group" Width="110px" Visible="false" />
                                            <asp:Button ID="BtnDownloadPdf" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnDownloadPdf_Click" TabIndex="10" Text="Download PDF"
                                                ValidationGroup="group" Visible="false" />
                                            <asp:Button ID="BtnUnlock" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnUnlock_Click" TabIndex="10" Text="Unlock"
                                                ValidationGroup="group" Width="110px" Visible="false" />
                                            <asp:HiddenField ID="HiddenLockedStatus" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <!-- New Label for Locked Status -->
                                            <asp:Label ID="LblLockedStatus" runat="server" CssClass="status-label" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <!-- New Label for warning -->
                                            <asp:Label ID="LblWarning" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <!-- New Label for Download warning -->
                                            <asp:Label ID="LblDownloadWarning" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table cellpadding="4" cellspacing="5" style="width: 100%">
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px; height: 117px;" valign="top">
                                    <asp:Panel ID="pnlResults" runat="server">
                                        <table runat="server" id="tblResults" visible="false" style="padding: 5px">
                                            <tr>
                                                <td>
                                                    <telerik:RadGrid ID="rgResults" PageSize="10" AllowPaging="True" AutoGenerateColumns="false" runat="server" AllowSorting="true" GridLines="Both"
                                                        OnNeedDataSource="rgResults_NeedDataSource" OnItemDataBound="rgResults_ItemDataBound" AllowCustomPaging="True">
                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                        <HeaderStyle CssClass="MyGridHeader" />
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <ClientSettings Selecting-AllowRowSelect="true">
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" AllowResizeToFit="true" />
                                                            <Selecting AllowRowSelect="True" />
                                                        </ClientSettings>
                                                        <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader" DataKeyNames="intWagePaymentReqid" AllowPaging="True" PagerStyle-AlwaysVisible="True" AllowCustomPaging="True">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                            <HeaderStyle CssClass="MyGridHeader" />
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="enrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="dateofCreation" HeaderText="Date Of Creation" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="exchangerateattime" HeaderText="Exchange Rate At Time" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="intBenid" HeaderText="Beneficiary ID" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="intWagePaymentReqid" HeaderText="Wage Payment Req ID" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="intWagesReqBenTransid" HeaderText="Wages Req Ben Trans ID" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="intWorkid" HeaderText="Work ID" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="paymentCycle" HeaderText="Payment Cycle" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="reqFromDate" HeaderText="Req From Date" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="reqNo" HeaderText="Req No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="reqToDate" HeaderText="Req To Date" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="status" HeaderText="Status" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="supportType" HeaderText="Support Type" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="totAttendance" HeaderText="Total Attendance" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="wageRateSSPTotal" HeaderText="Wage Rate SSP Total" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="wageRateUSD" HeaderText="Wage Rate USD" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="wageRateUSDTotal" HeaderText="Wage Rate USD Total" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="workCode" HeaderText="Work Code" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>


        <Triggers>
            <asp:PostBackTrigger ControlID="BtnDownload" />
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
