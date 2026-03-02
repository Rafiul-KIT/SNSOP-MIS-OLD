<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_APIDashboard" Title=":: :: SNSOP : ::" CodeBehind="APIDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
</script>


    <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>--%>
    <div align="left">
        <ol class="breadcrumb">
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;
            <li><i class="fa fa-dashboard"></i><a href="#">API Dashboard</a> </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">

            <div class="col-lg-12">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Label ID="lblOfficer" runat="server"></asp:Label>&nbsp; API Dashboard
                        </h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                            <tr>
                                <td style="padding: 5px; margin: 5px">
                                    <div class="panel panel-default">
                                        <div class="panel-body" align="center">
                                            <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="width: 395px; font-size: 16px;">
                                                        <b>
                                                            <asp:Label ID="lblOfficer0" runat="server" Text="API"></asp:Label></b>
                                                    </td>
                                                    <td style="width: 30px">&nbsp;
                                                    </td>
                                                    <td style="width: 395px" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 395px" valign="top">
                                                        <div class="list-group">
                                                            <a href="APIDashboardDrillDown.aspx?Status=0" target="_blank" class="list-group-item">
                                                                <span class="badge">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label></span> <i class="fa fa-fw fa-calendar"></i>Total Beneficiaries </a>
                                                            <a href="APIDashboardDrillDown.aspx?Status=1" target="_blank"
                                                                runat="server" id="lnkOther" class="list-group-item"><span class="badge" style="background-color: #d9534f;">
                                                                    <asp:Label ID="Label26" runat="server"></asp:Label></span> <i class="fa fa-fw fa-calendar"></i>
                                                                <asp:Label ID="Label14" runat="server" Text="Pending Beneficiaries"></asp:Label>
                                                            </a>
                                                        </div>
                                                    </td>
                                                    <td style="width: 30px">&nbsp;
                                                    </td>
                                                    <td valign="top" style="width: 395px">
                                                        <div class="list-group">
                                                            <a href="APIDashboardDrillDown.aspx?Status=2" target="_blank" class="list-group-item"
                                                                runat="server" id="lnkPA6"><span class="badge" style="background-color: #87D37C;">
                                                                    <asp:Label ID="Label13" runat="server"></asp:Label></span> <i class="fa fa-fw fa-calendar"></i>
                                                                <asp:Label ID="Label2" runat="server" Text="Confirmed Beneficiaries"></asp:Label>
                                                            </a><a href="APIDashboardDrillDown.aspx?Status=3" target="_blank" class="list-group-item"
                                                                runat="server" id="lnkOtherD"><span class="badge" style="background-color: #87D37C;">
                                                                    <asp:Label ID="Label41" runat="server"></asp:Label></span> <i class="fa fa-fw fa-calendar"></i>
                                                                <asp:Label ID="Label15" runat="server" Text="Dropped Beneficiaries"></asp:Label>
                                                            </a>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 395px; font-size: 16px;">
                                                        <b></b>
                                                    </td>
                                                    <td style="width: 30px">&nbsp;
                                                    </td>
                                                    <td style="width: 395px" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 395px; font-size: 16px;">
                                                        <b></b>
                                                    </td>
                                                    <td style="width: 30px">&nbsp;
                                                    </td>
                                                    <td style="width: 395px" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp;
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdfID" runat="server" />
                        <asp:HiddenField ID="hdfFlagID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
