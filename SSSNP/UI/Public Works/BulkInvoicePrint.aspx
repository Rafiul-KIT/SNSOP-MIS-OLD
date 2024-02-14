<%@ Page Title=":: :: SSSNP : : Wages Payment Apply" Language="C#" AutoEventWireup="true" Inherits="BulkInvoicePrint" CodeBehind="BulkInvoicePrint.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<form runat="server">
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


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Print Wages Payment Request Details</h3>
                    </div>
                    <div class="panel-body" runat="server">
                        <asp:Panel runat="server" ID="pnl1">
                            <div id="dvContents" style="border: 1px dotted black; padding: 5px;">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="lblInvoiceNumber" runat="server" CssClass="LBLBLACK" Font-Bold="True"
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
                                                        <asp:Label ID="Label437" runat="server">Payment Date</asp:Label>
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
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label19" runat="server"
                                                Width="350px" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label44" runat="server" Text="Payment Summary"
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
                                                        <asp:Label ID="Label8" runat="server">Total Beneficiaries as per wage reqest </asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 5%">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 45%">
                                                        <asp:Label ID="Label9" runat="server"></asp:Label>
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
                                    <tr>
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
                                                <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left"
                                                    verticalalign="Middle" />
                                                <columns>
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
                                                        </columns>
                                                <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                                <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True"
                                                    forecolor="White" />
                                                <editrowstyle backcolor="#B9D684" />
                                                <alternatingrowstyle backcolor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"
                                            style="padding: 5px; margin: 5px;" valign="top">
                                            <telerik:RadGrid ID="rgDetails" PageSize="250" AutoGenerateColumns="false" runat="server" AllowPaging="false"
                                                AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                                OnNeedDataSource="rgDetails_NeedDataSource" OnItemDataBound="rgDetails_ItemDataBound">
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
                                                        <telerik:GridBoundColumn DataField="cnt" HeaderText="No of Days Present" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
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
</form>
