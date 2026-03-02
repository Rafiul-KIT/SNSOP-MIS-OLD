<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="WorkwiseStatusreportNew2" Title=":: SSSNP Online Management System ::" Codebehind="WorkwiseStatusreportNew.aspx.cs" %>

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
                            &nbsp;Work wise type of Status report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 98%">
                            <tr>
                                <td class="col-xs-3" style="padding: 5px; margin: 5px" align="left">
                                <%--    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" PostBackUrl="~/UI/Public Works/ReportsDashboard.aspx"
                                        Text="Back" />--%>
                                </td>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails0_RowDataBound"
                                        PageSize="15" Width="100%" ShowFooter="True">
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
                                            <asp:BoundField DataField="IPname" HeaderText="IP Name" />
                                            <asp:HyperLinkField DataTextField="Type1" HeaderText="Rural Social Infrastructure" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                            <asp:HyperLinkField  DataTextField="Type2" HeaderText="Soil and water conservation and land productivity" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="Type3" HeaderText="Urban Works" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="Type4" HeaderText="Environmental and social safeguards" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="Type5" HeaderText="Type5" Visible="false" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="Type6" HeaderText="Type6" Visible="false" >
                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:HyperLinkField>
                                        </Columns>
                                        
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="text-align: center">
                                <td>
                                    <asp:Label ID="lblmsg1" Text="Implementing Partner wise Report " Font-Bold="True"
                                        runat="server" Font-Size="Medium" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%" ShowFooter="True"
                                        Visible="False">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name of IP" />
                                            <asp:BoundField DataField="NoofCounties" HeaderText="No.of Counties">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoofPayams" HeaderText="No.of Payams">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoofBomas" HeaderText="No.of Bomas">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="Total No. Of Work Proposed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotaltobeApproved" HeaderText="No. of works to be approved">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalInProgress" HeaderText="No. of works in progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalClosed" HeaderText="Total Closed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalFundsDisbursed" HeaderText="Total Funds Disbursed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalBeneficiaries" HeaderText="Total Beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalFemaleBeneficiaries" HeaderText="Total Female Beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalmaleBeneficiaries" HeaderText="Total Male Beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="Total Work Days">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#FFFFFF" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#013161" />
                                        <AlternatingRowStyle BackColor="White" />
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
