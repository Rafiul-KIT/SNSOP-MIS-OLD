<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="ViewWagesReqDetailsAutoRec" CodeBehind="ViewWagesReqDetailsAutoRec.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);
            $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript" language="javascript">
        function OpenPopup() {
            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnl1.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open('', '', 'height=600,width=900');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
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
                        <i class="fa fa-fw fa-desktop"></i>Wages Dashboard
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">View Wage Request Details</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">View Wages Payment Request Details</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-primary"
                                    Height="32px" TabIndex="10" Text="Print" ValidationGroup="group"
                                    Width="155px" OnClientClick="return PrintPanel();" OnClick="btnPrint_Click" />
                                <asp:Panel runat="server" ID="pnl1">
                                    <div id="dvContents" style="border: 1px dotted black; padding: 5px;">
                                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                                    <asp:Label ID="lblProject0" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                        Width="165px" Font-Size="18">Invoice</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK">Project Name</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblProject" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK">Work Code</asp:Label>
                                                            </td>
                                                            <%--<tld style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblWorkCode" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trWorkStartDate">
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK">Approved 
                                                        Date</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblWorkStartDate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trwagerate">
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label430" runat="server">Wage Rate(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblwagerate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trFromDate">
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label412" runat="server">From Date</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblfromdate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trToDate">
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label413" runat="server">To Date</asp:Label>
                                                            </td>
                                                            <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lbltodate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label437" runat="server">First Payment Date</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label15" runat="server">Payment Cycle</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 75%;">
                                                                <asp:Label ID="lblPaymentCycle" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label8" runat="server">Total Beneficiaries as per wage reqest </asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lblTotalPaymRequested" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr runat="server" id="trState">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label427" runat="server">State</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblState" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trCounty">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label428" runat="server">County</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblCounty" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trPayam">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label429" runat="server">Payam</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblPayam" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" id="trBoma">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label364" runat="server">Boma 
                                                        Name</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblBoma" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label432" runat="server">Paying Agent</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblpayingagent" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label433" runat="server">Co-Ordinator</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblcoordinator" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label434" runat="server">Specialist</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblspecialist" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label435" runat="server">Manager</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblmanager" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label431" runat="server">Exchange Rate</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="txtExchangerate" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label436" runat="server">Wage Request No</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="lblWageRequestNo" runat="server"
                                                                    Font-Bold="False"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label1" runat="server">Co-Ordinator Review on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="LblCoRevon" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label3" runat="server">Specialist Verified on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="LblSpRevon" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 40%">
                                                                <asp:Label ID="Label5" runat="server">Manager approved on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 60%">
                                                                <asp:Label ID="LblMaRevon" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="center">
                                                    <asp:Label ID="Label44" runat="server" Text="Payments Overall Summary"
                                                        Width="350px" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label2" runat="server">Number of Paid Beneficiaries</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label6" runat="server">Number of Unpaid Beneficiaries</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label10" runat="server">Payment Made to Alternates</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="Label11" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label12" runat="server">Payment Made to Households</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="Label13" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr id="Tr1" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label26" runat="server">Wage Request Amount (USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label27" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr2" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label28" runat="server">Actual Amount Paid(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label29" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr3" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label30" runat="server">Difference between Request and Actual(USD) </asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="Label31" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr5" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label16" runat="server">Actual Amount Paid(USD) - Direct Income Support</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblPaidAmountDIS" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr6" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label17" runat="server">Actual Amount Paid(USD) - Public Works</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblPaidAmountPublicWorks" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr4" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label14" runat="server">Actual Amount Paid(SSP)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblAmountpaidSSP" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr7" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label18" runat="server">Actual Amount Paid(SSP) - Direct Income Support</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblPaidAmountDISSSP" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr8" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label20" runat="server">Actual Amount Paid(SSP) - Public Works</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblPaidAmountPublicWorksSSP" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">&nbsp;</td>
                                            </tr>
                                            <tr runat="server" id="trRefDocsTitle">
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: left;" valign="top">
                                                    <asp:Label ID="Label438" runat="server" CssClass="LBLBLACK"
                                                        Text="Reference Documents" Width="213px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: left;"
                                                    valign="top" align="center">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" Height="62px"
                                                        OnRowDataBound="grdDetails1_RowDataBound" PageSize="15" ShowFooter="false"
                                                        Width="100%">
                                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex +1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server"
                                                                        Text='<%# Eval("PhotoFileName") %>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                                        </Columns>
                                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <EditRowStyle BackColor="#B9D684" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="center">
                                                    <asp:Label ID="Label9" runat="server" Text="First step of Payments"
                                                        Width="350px" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label19" runat="server">Number of Paid Beneficiaries</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lbl1StepPaidBen" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label24" runat="server">Payment Made to Alternates</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lbl1StepPaydToAlt" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label32" runat="server">Payment Made to Households</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lbl1StepPaidToBeneficiaries" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">

                                                        <tr id="Tr10" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label36" runat="server">Actual Amount Paid(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lbl1StepActualPaidUSD" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr id="Tr12" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label40" runat="server">Actual Amount Paid(USD) - DIS</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lbl1StepActualPaidUSDDIS" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr13" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label42" runat="server">Actual Amount Paid(USD) - LIPW</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lbl1StepActualPaidUSDLIPW" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr14" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label45" runat="server">Actual Amount Paid(SSP)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lbl1StepActualPaidSSP" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr15" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label47" runat="server">Actual Amount Paid(SSP) - DIS</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lbl1StepActualPaidSSPDIS" runat="server"></asp:Label>

                                                            </td>

                                                        </tr>
                                                        <tr id="Tr16" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label49" runat="server">Actual Amount Paid(SSP) - LIPW</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">

                                                                <asp:Label ID="lbl1StepActualPaidSSPLIPW" runat="server"></asp:Label></td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"
                                                    style="padding: 5px; margin: 5px;" valign="top">
                                                    <telerik:RadGrid ID="rg1StepPaidBeneficiaries" PageSize="250" AutoGenerateColumns="false" runat="server" AllowPaging="false"
                                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                        MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                                        OnNeedDataSource="rg1StepPaidBeneficiaries_NeedDataSource" OnItemDataBound="rg1StepPaidBeneficiaries_ItemDataBound">
                                                        <HeaderStyle CssClass="MyGridHeader" />
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <ClientSettings Selecting-AllowRowSelect="true">
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                                            <Selecting AllowRowSelect="True" />
                                                            <ClientEvents OnRowSelected="rowSelected" />
                                                        </ClientSettings>
                                                        <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader"
                                                            DataKeyNames="intBenid"
                                                            ClientDataKeyNames="intBenid">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                            <HeaderStyle CssClass="MyGridHeader" />
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="rowNumberLabel" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="intBenid" Visible="false" HeaderText="intBenid"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name of household" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="tcHouseholdCode" HeaderText="Household Code">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hlHhCode" runat="server" Text="View" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="PresentDays" HeaderText="No of Days Present" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="AbsentDays" HeaderText="No of Days Absent" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Amt" HeaderText="Amount paid in USD" HeaderStyle-Width="11%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Amt1" HeaderText="Amount paid in SSP" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PaidToType" HeaderText="Paid to Beneficiary Type" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PayStatus" HeaderText="Status" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="paidToName" HeaderText="Paid to Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Component" HeaderText="Component" HeaderStyle-Width="13%"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="tcPaidToPersonHHCode" HeaderText="Paid to Person Household Code">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hlPersonHHCode" runat="server" Text="View" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr runat="server" id="trMopUpCountersLabel">
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top" align="center">
                                                    <asp:Label ID="Label21" runat="server" Text="Mop-up Payments"
                                                        Width="350px" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trMopUpCounters">
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label22" runat="server">Number of Paid Beneficiaries</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lblMopUpPaidBen" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label23" runat="server">Payment Made to Alternates</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lblMopUpPaydToAlt" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px; width: 50%">
                                                                <asp:Label ID="Label25" runat="server">Payment Made to Households</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 45%">
                                                                <asp:Label ID="lblMopUpPaidToBeneficiaries" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">

                                                        <tr id="Tr9" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label33" runat="server">Actual Amount Paid(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblMopUpActualPaidUSD" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>

                                                        <tr id="Tr11" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label34" runat="server">Actual Amount Paid(USD) - DIS</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblMopUpActualPaidUSDDIS" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr17" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label35" runat="server">Actual Amount Paid(USD) - LIPW</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblMopUpActualPaidUSDLIPW" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr18" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label37" runat="server">Actual Amount Paid(SSP)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblMopUpActualPaidSSP" runat="server"></asp:Label>
                                                            </td>

                                                        </tr>
                                                        <tr id="Tr19" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label38" runat="server">Actual Amount Paid(SSP) - DIS</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">
                                                                <asp:Label ID="lblMopUpActualPaidSSPDIS" runat="server"></asp:Label>

                                                            </td>

                                                        </tr>
                                                        <tr id="Tr20" runat="server">
                                                            <td style="padding: 5px; margin: 5px; width: 75%">
                                                                <asp:Label ID="Label39" runat="server">Actual Amount Paid(SSP) - LIPW</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 20%">

                                                                <asp:Label ID="lblMopUpActualPaidSSPLIPW" runat="server"></asp:Label></td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr runat="server" id="trMopUpPaidBeneficiaries">
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">
                                                    <telerik:RadGrid ID="rg1MopUpBeneficiaries" PageSize="250" AutoGenerateColumns="false" runat="server" AllowPaging="false"
                                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                        MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                                        OnNeedDataSource="rg1MopUpBeneficiaries_NeedDataSource" OnItemDataBound="rg1MopUpBeneficiaries_ItemDataBound">
                                                        <HeaderStyle CssClass="MyGridHeader" />
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <ClientSettings Selecting-AllowRowSelect="true">
                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                                            <Selecting AllowRowSelect="True" />
                                                            <ClientEvents OnRowSelected="rowSelected" />
                                                        </ClientSettings>
                                                        <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader"
                                                            DataKeyNames="intBenid"
                                                            ClientDataKeyNames="intBenid">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                            <HeaderStyle CssClass="MyGridHeader" />
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <Columns>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="rowNumberLabel" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="intBenid" Visible="false" HeaderText="intBenid"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name of household" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="tcHouseholdCode" HeaderText="Household Code">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hlHhCode" runat="server" Text="View" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="PresentDays" HeaderText="No of Days Present" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="AbsentDays" HeaderText="No of Days Present" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Amt" HeaderText="Amount paid in USD" HeaderStyle-Width="11%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Amt1" HeaderText="Amount paid in SSP" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PaidToType" HeaderText="Paid to Beneficiary Type" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="PayStatus" HeaderText="Status" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="paidToName" HeaderText="Paid to Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Component" HeaderText="Component" HeaderStyle-Width="13%"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="tcPaidToPersonHHCode" HeaderText="Paid to Person Household Code">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hlPersonHHCode" runat="server" Text="View" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert"
                                                            href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <asp:HiddenField ID="hdfID" runat="server" />
                                                    <asp:HiddenField ID="hdfFlagID" runat="server" />
                                                    <asp:HiddenField ID="intWagePaymentReqid" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <td style="width: 27px"></td>
                                <td valign="top"></td>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px; text-align: left;"
                                        valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px"></td>
                                </tr>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>
