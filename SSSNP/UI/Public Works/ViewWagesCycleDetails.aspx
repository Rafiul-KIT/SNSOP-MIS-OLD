<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="ViewWagesCycleDetails" CodeBehind="ViewWagesCycleDetails.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
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
            printWindow.document.write('<html><head><title>Registration Certificate</title>');
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
                                <h3 class="panel-title">View Wages Payment Cycle wise Details</h3>
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
                                                        Width="365px" Font-Size="18">Work Cycle wise Invoice</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                                    Width="165px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                                    Width="165px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK"
                                                                    Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage 
                                                        Rate(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblwagerate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label437" runat="server" CssClass="LBLBLACK" Width="128px">Payment Date</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblPaymentDate" runat="server" CssClass="LBLBLACK" Width="160px">10-05-2018</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblfromdate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                            </td>
                                                            <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lbltodate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label432" runat="server" CssClass="LBLBLACK" Width="165px">Paying Agent</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblpayingagent" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label433" runat="server" CssClass="LBLBLACK" Width="165px">Co-Ordinator</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblcoordinator" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label434" runat="server" CssClass="LBLBLACK" Width="165px">Specialist</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblspecialist" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="165px">Manager</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblmanager" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>

                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="128px">Exchange Rate</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="txtExchangerate" runat="server" CssClass="LBLBLACK"
                                                                    Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label436" runat="server" CssClass="LBLBLACK" Width="128px">Wage Request No</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="lblWageRequestNo" runat="server" CssClass="LBLBLACK"
                                                                    Font-Bold="False">02-05-2018</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="165px">Co-Ordinator Review on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="LblCoRevon" runat="server" CssClass="LBLBLACK" Width="160px">03-05-2018</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="165px">Specialist Verified on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="LblSpRevon" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="165px">Manager approved on</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="LblMaRevon" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <asp:Label ID="Label44" runat="server" Text="Payment Summary"
                                                        CssClass="LBLBLACK" Width="350px" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="180px">Number of Paid Beneficiaries</asp:Label>
                                                            </td>

                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                                    Width="165px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="180px">Number of Unpaid Beneficiaries</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                                    Width="165px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="137px">Total Beneficiaries as per wage reqest </asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK"
                                                                    Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="180px">Payment Made to Alternates</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="180px">Payment Made to Households</asp:Label>
                                                            </td>
                                                            <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                        <tr id="Tr1" runat="server">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label26" runat="server" CssClass="LBLBLACK" Width="180px">Wage Request Amount(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label27" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr id="Tr2" runat="server">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label28" runat="server" CssClass="LBLBLACK" Width="180px">Actual Amount Paid(USD)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label29" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr id="Tr4" runat="server">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label14" runat="server" CssClass="LBLBLACK" Width="180px">Actual Amount Paid(SSP)</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="lblAmountpaidSSP" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                        <tr id="Tr3" runat="server">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label30" runat="server" CssClass="LBLBLACK" Width="165px">Difference between Request and Actual(USD) </asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label31" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="4" CssClass="GRD" DataKeyNames="intBenid" ForeColor="#333333"
                                                        Height="62px" OnRowDataBound="grdDetails_RowDataBound" PageSize="15"
                                                        ShowFooter="True" Width="100%">
                                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                                        <RowStyle CssClass="GRDITEM" HorizontalAlign="Left"
                                                            VerticalAlign="Middle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="All" Visible="False">
                                                                <HeaderTemplate>
                                                                    <%-- <asp:CheckBox ID="CheckBox2" runat="server" />--%>
                                                                    <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5" Checked="true" Enabled="false"
                                                                        AutoPostBack="True" onclick="CheckAll(this);" />
                                                                    &nbsp;
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" Checked="true" Enabled="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex +1 %>
                                                                    <asp:HiddenField ID="HdfReqID" runat="server" />
                                                                    <asp:HiddenField ID="hdfRegBenid" runat="server" />
                                                                    <asp:HiddenField ID="HdfBenid" runat="server" />
                                                                    <asp:HiddenField ID="HdfBenWagesInvoiceid" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                                            <asp:BoundField DataField="NameofHousehold" HeaderText="Name of Household" />
                                                            <asp:HyperLinkField DataTextField="household_number" HeaderText="Household Code">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:HyperLinkField>
                                                            <asp:BoundField DataField="cnt" HeaderText="No of Days Present" />
                                                            <%--DataField="Amt" --%>
                                                            <asp:BoundField DataField="Amt" HeaderText="Amount Paid in USD" />
                                                            <asp:BoundField DataField="Amt1" HeaderText="Amount Paid in SSP" />
                                                            <asp:BoundField DataField="PaidToType" HeaderText="Paid to Beneficiary Type" />
                                                            <asp:BoundField DataField="PayStatus" HeaderText="Status" />
                                                            <asp:BoundField DataField="PaidToName" HeaderText="Paid to Name" />
                                                            <asp:HyperLinkField DataTextField="Paidto" HeaderText="Paid to Person Household Code">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:HyperLinkField>
                                                        </Columns>
                                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle Font-Bold="True"
                                                            ForeColor="Black" />
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
                                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                    valign="top" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2"
                                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert"
                                                            href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label
                                                                ID="lblmsg" runat="server"></asp:Label>
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
