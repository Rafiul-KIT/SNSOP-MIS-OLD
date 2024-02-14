<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="DrillDownFundDistributedReports" Title=":: SSSNP Online Management System ::" CodeBehind="DrillDownFundDistributedReports.aspx.cs" %>

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
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
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

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">&nbsp;Drilldown</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px; width: 1058px;">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                                <td align="center" style="padding: 5px; margin: 5px; text-align: right;">
                                    <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-default"
                                        OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" Text="Print" />
                                    &nbsp;
                                    <asp:Button ID="BtbExport" runat="server" CssClass="btn btn-default"
                                        Text="Export" OnClick="BtbExport_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px" colspan="2">
                                    <div runat="server" id="div_print">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <asp:GridView ID="grdDetails" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="GRD"
                                                ForeColor="#333333" Height="62px"
                                                OnPageIndexChanging="grdDetails_PageIndexChanging"
                                                OnRowDataBound="grdDetails_RowDataBound" PageSize="50" Width="99%"
                                                OnSelectedIndexChanged="grdDetails_SelectedIndexChanged" ShowFooter="True">
                                                <HeaderStyle CssClass="GridviewScrollC1HeaderWrap" />
                                                <RowStyle CssClass="GridviewScrollC1Item" />
                                                <PagerStyle CssClass="GridviewScrollC1Pager" />
                                                <FooterStyle Height="30px" CssClass="GridviewScrollC1Footer" />
                                                <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IPName" HeaderText="IP Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AuthorisedPerson" HeaderText="Authorised Person">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="No of Benificiaries" />
                                                    <asp:BoundField HeaderText="No of male Benificiaries" />
                                                    <asp:BoundField HeaderText="No of female Benificiaries" />
                                                    <asp:BoundField HeaderText="No of  Male Working days" />
                                                    <asp:BoundField HeaderText="No of  Female Working days" />
                                                    <asp:BoundField HeaderText="Total Male Wage paid Amount" />
                                                    <asp:BoundField HeaderText="Total Female Wage paid Amount" />
                                                    <asp:BoundField HeaderText="Start date" />
                                                    <asp:BoundField HeaderText="End date" />
                                                    <%-- <asp:BoundField DataField="Created_by" HeaderText="Created_by" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                                                                                          <asp:BoundField DataField="Created_dt" HeaderText="Created_dt" >
                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                            
                                            <asp:BoundField DataField="Modified_by" HeaderText="Modified_by" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Modified_dt" HeaderText="Modified_dt" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                                    --%>
                                                </Columns>
                                                <%--<pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <alternatingrowstyle backcolor="White" />--%>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px" colspan="2">&nbsp;</td>
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
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />                                                                    
                                                                </asp:TemplateField>
                                            <asp:BoundField DataField="PayamName" HeaderText="Name of the Payam" />                   
                                            <asp:BoundField DataField="IPname" HeaderText="IP Name" />
                                            <asp:BoundField DataField="Type1" HeaderText="No.of Type1" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type2" HeaderText="No.of Type2" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type3" HeaderText="No.of Type3" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type4" HeaderText="No.of Type4" >
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
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
