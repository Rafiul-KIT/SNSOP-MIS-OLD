<%@ Page Title=":: :: SNSOP : : Results Framework" MasterPageFile="~/UI/Public Works/CCMaster.master" Language="C#" AutoEventWireup="true" CodeBehind="ResultsFrameworkMNE.aspx.cs" Inherits="SSSNP.UI.Public_Works.ResultsFrameworkMNE" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <i class="fa fa-edit"></i><a href="#">Results Framework</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Results Framework</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%; margin-bottom: 1px; height: 13px;">
                                    <tr>
                                        <td style="vertical-align: top; width: 65%;">
                                            <asp:DropDownList ID="ddlIndicatorType" runat="server" class="form-control txtbox" Width="100%" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="ddlIndicatorType_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select Type--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; width: 65%;">
                                            <asp:DropDownList ID="ddlIndicator" runat="server" class="form-control txtbox" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlIndicator_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select Indicator--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel-body">

                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">

                                    <tr>
                                        <td style="vertical-align: top; width: 65%;">
                                            <!-- Left Side: State, County -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">
                                                <tr id="StateTR">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelState" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="CountyTR">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelCounty" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCounty" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="SupportTypeTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="165px">Support Type</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSupportType" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="LegalStatusTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="165px">Legal Status</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLegalStatus" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="PaymentCycleTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; width: 35%;">
                                            <!-- Right Side: Payam, Boma -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">
                                                <tr id="PayamTR">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="BomaTR">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelBoma" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Width="180px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="SexTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="165px">Sex</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSex" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="AgeRangeTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="165px">Age Range</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAge" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="GrievanceCategoryTR" runat="server" visible="false">
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="165px">Grievance Category</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGrievanceCategory" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <!-- Download Button -->
                                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="BtnSearch_Click" />
                                            <asp:Label ID="LblWarning" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-left: 50px; margin-right: 50px">
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
                                            <telerik:GridBoundColumn DataField="IndicatorKey" HeaderText="">
                                                <HeaderStyle Width="50%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IndicatorValue" HeaderText="Value">
                                                <HeaderStyle Width="20%" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
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
