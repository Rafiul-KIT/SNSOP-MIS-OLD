<%@ Page Title=":: :: SNSOP : : Implentation Partner Details" Language="C#" MasterPageFile="~/UI/Public Works/AlertMaster.master" AutoEventWireup="true" Inherits="AutoAlertMonthlyIPFundsDisbursementReport" Codebehind="AutoAlertMonthlyIPFundsDisbursementReport.aspx.cs" %>

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

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
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
                        <i class="fa fa-edit"></i><a href="#">Monthly IP Funds Disbursement Report</a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Monthly IP Funds Disbursement Report</h3>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
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
                                                                <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="137px">Year</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:DropDownList ID="ddlyear" runat="server" class="form-control txtbox"
                                                                    Height="28px" Width="180px">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="ddlyear" ErrorMessage="Please Select Year"
                                                                    ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                                &nbsp;</td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td style="width: 27px">
                                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="137px">Month</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control txtbox"
                                                                    Height="28px" Width="180px">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="ddlMonth" ErrorMessage="Please Select Month"
                                                                    ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                        Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Show"
                                                        ValidationGroup="group" Width="90px" />
                                                    <asp:Button CssClass="btn btn-primary" ID="btnMailSend" runat ="server" Text="Send Mail"  OnClick="btnMailSend_Clicked" Visible ="false" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">


                                                    <div id="success" runat="server" visible="false" class="alert alert-success">
                                                        <a href="AddQualification.aspx" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                                        <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                    </div>


                                                    <div id="Failure" runat="server" visible="false" class="alert alert-danger">
                                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                                        <strong>Warning!</strong>
                                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
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

