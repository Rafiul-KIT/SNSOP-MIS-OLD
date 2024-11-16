<%@ Page Title=":: :: SNSOP : : Payroll Reconcile" Language="C#" AutoEventWireup="true" CodeBehind="ReconcilePayroll.aspx.cs" MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="SSSNP.UI.Public_Works.Payroll.ReconcilePayroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Payroll Reconciliation</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Payroll Reconciliation</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="vertical-align: top; width: 65%;">
                                            <!-- Left Side: State, County -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">
                                                <tr>
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelState" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelCounty" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCounty" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; width: 35%;">
                                            <!-- Right Side: Payam, Boma -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">                                                
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelBoma" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <!-- Download Button -->
                                            <asp:Button ID="BtnDownload" runat="server" CssClass="btn btn-primary" Text="Download" OnClick="BtnDownload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <!-- Warning Label -->
                                            <asp:Label ID="LblWarning" runat="server" ForeColor="Red" Visible="False" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>   
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnDownload" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
