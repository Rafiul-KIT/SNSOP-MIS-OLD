<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_Public_Works_EnvironmentRegistrationReportDetails"
    Title="Environment Registration Report Details" CodeBehind="EnvironmentRegistrationReportDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function GetRowValue(val) {
            if (val != '&nbsp;') {
                val1 = 0;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;
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
                        <h3 class="panel-title">&nbsp;Grievance Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-warning" Height="32px"
                                        OnClick="PrintAllPages" ToolTip="Print" TabIndex="10" Text="Print" ValidationGroup="group"
                                        Width="90px" Visible="false" />
                                    &nbsp;
                                    <asp:Button ID="btnexp" runat="server" CausesValidation="False" CssClass="btn btn-success"
                                        Font-Size="12px" Height="32px" OnClick="btnexp_Click" Style="position: static"
                                        Text="Export7" ValidationGroup="vg" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div runat="server" id="div_print">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" CssClass="GRD table-bordered" ForeColor="#333333" GridLines="None"
                                                Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                                PageSize="15" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <Columns>
                                                    <%-- <asp:BoundField DataField="Grievance_RegistrationNo" HeaderText="Grievance Registration No"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                    <asp:BoundField DataField="EnvironmentSocialImpact" HeaderText="Environment Social Impact" />
                                                    <asp:BoundField DataField="ProposedMitigationMeasure" HeaderText="Proposed Mitigation Measure" />
                                                    <asp:BoundField DataField="MitigationMeasureImplemented" HeaderText="Mitigation Measure Implemented" />
                                                    <asp:BoundField DataField="PerformanceIndicators" HeaderText="Performance Indicators" />
                                                    <%--<asp:BoundField DataField="FileName" HeaderText="FileName" />--%>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' />
                                                            <asp:HyperLink ID="hypScanLetter0" runat="server" CommandName="PathUpdate" CssClass="LBLBLACKFOOTER" Text='<%# Eval("FilePath") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#B9D684" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
