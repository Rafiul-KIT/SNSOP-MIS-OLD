<%@ Page Title=":: :: SSSNP : : Cash Transfer Summary" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="CashTransferSummaryMNE" CodeBehind="CashTransferSummaryMNE.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#txtStartDate').datetimepicker();
        });
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
                                                <!-- State Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelState" runat="server"  Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- County Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelCounty" runat="server"  Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounty" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Payam Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPayam" runat="server"  Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Boma Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelBoma" runat="server"  Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Currency Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelCurrency" runat="server"  Width="165px">Currency</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlCurrency" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="SSP">SSP</asp:ListItem>
                                                            <asp:ListItem Value="USD">USD</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Payment Cycle Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPaymentCycle" runat="server"  Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                            <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                                            <asp:ListItem Value="Bi-Annually">Bi-Annually</asp:ListItem>
                                                            <asp:ListItem Value="Annually">Annually</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Payment Status Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPaymentStatus" runat="server"  Width="165px">Payment Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentStatus" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                                            <asp:ListItem Value="Not Paid">Not Paid</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Component Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelComponent" runat="server"  Width="165px">Component</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlComponent" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="LIPW">LIPW</asp:ListItem>
                                                            <asp:ListItem Value="DIS">DIS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <!-- Payment Partner Filter -->
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPaymentPartner" runat="server"  Width="165px">Payment Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentPartner" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Partner1">Partner1</asp:ListItem>
                                                            <asp:ListItem Value="Partner2">Partner2</asp:ListItem>
                                                            <asp:ListItem Value="Partner3">Partner3</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSearch" runat="server"
                                                Height="32px" OnClick="BtnSearch_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
