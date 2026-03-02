<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" EnableEventValidation="true"
    AutoEventWireup="true" Inherits="LIPWExports" CodeBehind="LIPWExports.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>




    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx"></a>Home</li>
                    <li class=""><i class="fa fa-fw fa-desktop"></i>LIPW Exports</li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">Labour Intensive Public Work - Data Exports</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Labour Intensive Public Work - Data Exports</h3>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upPanelMain">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <table align="center" cellpadding="4" cellspacing="5" style="width: 83%">
                                            <tr>
                                                <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                                    <asp:Button ID="btnLIPWWorkProposalData" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="All LIPW work proposal data" ValidationGroup="group" Width="300px" OnClick="btnLIPWWorkProposalData_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                                    <asp:Button ID="btnLIPWBeneficiaries" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="All LIPW Beneficiaries" ValidationGroup="group" Width="300px" OnClick="btnLIPWBeneficiaries_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divFailure" runat="server" class="alert alert-danger" visible="false">
                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnLIPWWorkProposalData" />
            <asp:PostBackTrigger ControlID="btnLIPWBeneficiaries" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
