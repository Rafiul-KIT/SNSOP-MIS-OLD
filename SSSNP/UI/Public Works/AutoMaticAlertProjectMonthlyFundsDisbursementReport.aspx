<%@ Page Title=":: :: SNSOP : : Implentation Partner Details" Language="C#" MasterPageFile="~/UI/Public Works/AlertMaster.master" AutoEventWireup="true" Inherits="AutoMaticAlertProjectMonthlyFundsDisbursementReport" Codebehind="AutoMaticAlertProjectMonthlyFundsDisbursementReport.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 112px;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }
    </style>
    <script type="text/javascript" language="javascript">



        function OpenPopup() {

            window.open("Lookups/LookupTSTI.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }
    </script>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
        updateProgress.style.display = "block";
    }
    </script>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-edit"></i>Report
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a id="headerh3" runat="server" href="#"></a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <table>
                                    <tr class="panel-title">
                                        <td>
                                            <h5 id="header" runat="server" class="panel-title"></h5>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12 pull-right">
                                                <asp:Button CssClass="btn btn-primary" ID="btnMailSend" runat="server" Text="Send Mail" OnClick="btnMailSend_Clicked" Visible="false" />
                                            </div>
                                        </div>
                                        <div>
                                            <rsweb:ReportViewer ID="ReportViewer1" AsyncRendering="false"
                                                ShowBackButton="False" ShowFindControls="False" ShowPageNavigationControls="false"
                                                ShowZoomControl="False" SizeToReportContent="true" ShowPrintButton="true" runat="server">
                                            </rsweb:ReportViewer>
                                        </div>
                                        <asp:HiddenField ID="hdfID" runat="server" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                        <asp:HiddenField ID="hdfFlagID" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />

                        </div>

                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>


            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />


        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

