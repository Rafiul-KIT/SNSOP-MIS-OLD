<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="WagePaymentReport" Title=":: SSSNP Online Management System ::" CodeBehind="WagePaymentReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function GetRowValue(val) {
            if (val != '&nbsp;') {
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
                        <h3 class="panel-title">&nbsp;Wage Payment Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td class="col-xs-3" style="padding: 5px; margin: 5px" align="left">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" PostBackUrl="~/UI/Public Works/ReportsDashboard.aspx"
                                        Text="Back" />
                                </td>
                                <td class="col-xs-3" align="center" style="padding: 5px; margin: 5px; text-align: center;">
                                    <asp:Button ID="btnexp" runat="server" CssClass="btn btn-primary" Height="32px" CausesValidation="False"
                                        Font-Size="12px" Style="position: static" Text="Export"
                                        OnClick="btnexp_Click" ValidationGroup="vg" />
                                </td>
                                <td class="col-xs-3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg2" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600">Wage payment report - IP wise</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="15" Width="99%" OnSelectedIndexChanged="grdDetails_SelectedIndexChanged"
                                        ShowFooter="True">
                                        <HeaderStyle CssClass="GridviewScrollC1HeaderWrap" />
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <FooterStyle Height="30px" CssClass="GridviewScrollC1Footer" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name Of IP" />

                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid in SSP">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt1" HeaderText="Wages paid in USD">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" Visible="false"
                                        PageSize="15" Width="99%"
                                        ShowFooter="True" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name Of IP" />
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="TotalWorkProposed" />
                                            <asp:BoundField DataField="TotalInProgress" HeaderText="TotalInProgress" />
                                            <asp:BoundField DataField="TotalBeneficiaries" HeaderText="TotalBeneficiaries" />
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="TotalWorkDays" />
                                            <asp:BoundField DataField="TotalWageAmt" HeaderText="TotalWageAmt in SSP" />
                                            <asp:BoundField DataField="TotalWageAmt1" HeaderText="TotalWageAmt in USD" />
                                            <%--<asp:HyperLinkField  DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">&nbsp;</td>
                            </tr>
                            <%--<tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails0_RowDataBound" PageSize="15" Width="100%" 
                                        ShowFooter="True" horizontalalign="center">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />
                                                                    
                                                                </asp:TemplateField>
                                            <asp:BoundField DataField="PayamName" HeaderText="PayamName" />                   
                                            <asp:BoundField DataField="IPname" HeaderText="IP Name" />
                                            <asp:BoundField DataField="Type1" HeaderText="Rural Social Infrastructure" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type2" HeaderText="Soil and water conservation and land productivity" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type3" HeaderText="Urban Works" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type4" HeaderText="Environmental and social safeguards" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type5" HeaderText="No.of Type5" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type6" HeaderText="No.of Type6" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                           <asp:BoundField DataField="TotalWorkProposed" HeaderText="Total" >
                                                                                           <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                            
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                </td>                          
                            </tr>--%>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnexp0" runat="server" CssClass="btn btn-primary" Height="32px" CausesValidation="False"
                                        Font-Size="12px" Style="position: static" Text="Export"
                                        OnClick="btnexp0_Click" ValidationGroup="vg" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg0" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600">Wage payment report - County wise</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        OnRowDataBound="grdDetails0_RowDataBound" PageSize="15" Width="99%" ShowFooter="True">
                                        <HeaderStyle CssClass="GridviewScrollC1HeaderWrap" />
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <FooterStyle Height="30px" CssClass="GridviewScrollC1Footer" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid in SSP">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt1" HeaderText="Wages paid in USD">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" Visible="false" PageSize="15" Width="99%"
                                        ShowFooter="True" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="TotalWorkProposed" />
                                            <asp:BoundField DataField="TotalInProgress" HeaderText="TotalInProgress" />
                                            <asp:BoundField DataField="TotalBeneficiaries" HeaderText="TotalBeneficiaries" />
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="TotalWorkDays" />
                                            <asp:BoundField DataField="TotalWageAmt" HeaderText="TotalWageAmt in SSP" />
                                            <asp:BoundField DataField="TotalWageAmt1" HeaderText="TotalWageAmt  in USD" />
                                            <%-- <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField  DataTextField="TotalWageAmt" HeaderText="Wages paid">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnexp1" runat="server" CssClass="btn btn-primary" Height="32px" CausesValidation="False"
                                        Font-Size="12px" Style="position: static" Text="Export"
                                        OnClick="btnexp1_Click" ValidationGroup="vg" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600">Wage payment report - Payam wise</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        OnRowDataBound="grdDetails1_RowDataBound" PageSize="15" Width="99%" ShowFooter="True">
                                        <HeaderStyle CssClass="GridviewScrollC1HeaderWrap" />
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <FooterStyle Height="30px" CssClass="GridviewScrollC1Footer" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid in SSP">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt1" HeaderText="Wages paid in USD">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" PageSize="15" Width="99%" Visible="false"
                                        ShowFooter="True" OnRowDataBound="GridView3_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="TotalWorkProposed" />
                                            <asp:BoundField DataField="TotalInProgress" HeaderText="TotalInProgress" />
                                            <asp:BoundField DataField="TotalBeneficiaries" HeaderText="TotalBeneficiaries" />
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="TotalWorkDays" />
                                            <asp:BoundField DataField="TotalWageAmt" HeaderText="TotalWageAmt in SSP" />
                                            <asp:BoundField DataField="TotalWageAmt1" HeaderText="TotalWageAmt in USD" />
                                            <%--  
                                            <asp:HyperLinkField  DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg3" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600">Wage payment report - Boma wise</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        PageSize="15" Width="99%"
                                        ShowFooter="True" OnRowDataBound="grdDetails2_RowDataBound">
                                        <HeaderStyle CssClass="GridviewScrollC1HeaderWrap" />
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <FooterStyle Height="30px" CssClass="GridviewScrollC1Footer" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />

                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total Works In Progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="No. of direct beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWorkDays" HeaderText="No. of wage days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt" HeaderText="Wages paid in SSP">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalWageAmt1" HeaderText="Wages paid in USD">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
