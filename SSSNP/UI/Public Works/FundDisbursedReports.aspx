<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="FundDisbursedReports" Title=":: SSSNP Online Management System ::" Codebehind="FundDisbursedReports.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
   function GetRowValue(val)
    {   
    if(val != '&nbsp;')
    {   
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
                        <h3 class="panel-title">
                            &nbsp;Fund Disbursed Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td class="col-xs-3" style="padding: 5px; margin: 5px" align="left">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" PostBackUrl="~/UI/Public Works/ReportsDashboard.aspx"
                                        Text="Back" />
                                </td>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="btnexp" runat="server" CausesValidation="False" 
                                        CssClass="btn btn-primary" Font-Size="12px" Height="32px" 
                                        onclick="btnexp_Click" Style="position: static" Text="Export" 
                                        ValidationGroup="vg" />
                                </td>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="99%" OnSelectedIndexChanged="grdDetails_SelectedIndexChanged"
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
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name Of IP" />
                                            
                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total no. of works proposed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalTobeApproved" HeaderText="Total No. of works to be approved">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total No. of works in progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalRaiseFundsAmt" HeaderText="Total Bill Raised Amount (in SSP)">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalReleaseFundsAmt" HeaderText="Total Bill Released Amount (in SSP)">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView1" runat="server" Visible="false" AutoGenerateColumns="False"
                                        CellPadding="4" 
                                        ShowFooter="True" onrowdatabound="GridView1_RowDataBound">
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name Of IP" />
                                            
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="TotalWorkProposed" />
                                             <asp:BoundField DataField="TotalTobeApproved" HeaderText="TotalWorkProposed" />
                                              <asp:BoundField DataField="TotalInProgress" HeaderText="TotalInProgress" />
                                               <asp:BoundField DataField="TotalRaiseFundsAmt" HeaderText="TotalRaiseFundsAmt" />
                                                <asp:BoundField DataField="TotalReleaseFundsAmt" HeaderText="TotalReleaseFundsAmt" />
                                                
                                            
                                           <%-- 
                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total no. of works proposed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalTobeApproved" HeaderText="Total No. of works to be approved">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="Total No. of works in progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalRaiseFundsAmt" HeaderText="Total Bill Raised Amount (in SSP)">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalReleaseFundsAmt" HeaderText="Total Bill Released Amount (in SSP)">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    &nbsp;
                                </td>
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
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
