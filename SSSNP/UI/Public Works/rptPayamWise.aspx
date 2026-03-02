<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="rptPayamWise"
    Title=":: SSSNP Online Management System ::" Codebehind="rptPayamWise.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function GetRowValue(val, valinvoice) {
            if (val != '&nbsp;') {
                val1 = 0;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfInVoiceID').value = valinvoice;

            }
            window.opener.document.forms[0].submit();
            self.close();

        }
    
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">
                            Payam Wise Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td align="right" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="btn btn-info"
                                        Height="32px" OnClick="btnExport_Click" TabIndex="10" Text="Export" ToolTip="Click to Export"
                                        Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" GridLines="None"
                                        HeaderStyle-Wrap="true" ShowFooter="true" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name"></asp:BoundField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="NoOfBatches" HeaderText="No Of Batches">
                                            </asp:BoundField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalAmount" HeaderText="Total Amount" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalBeneficiries" HeaderText="Total Beneficiries" />
                                            <asp:HyperLinkField HeaderText="View Details" />
                                        </Columns>
                                        <RowStyle CssClass="GridviewScrollC1Item" Wrap="true" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <SelectedRowStyle CssClass="GridviewScrollC1Item2" Wrap="true" />
                                        <HeaderStyle CssClass="GridviewScrollC1Header" Wrap="true" />
                                        <FooterStyle CssClass="GridviewScrollC1Footer" />
                                        <EditRowStyle CssClass="GridviewScrollC1Item2" Wrap="true" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
