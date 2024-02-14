<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="BomaWiseReportNew" Title=":: SSSNP Online Management System ::" Codebehind="BomaWiseReportNew.aspx.cs" %>

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
                            &nbsp;Boma Wise Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td class="col-xs-3" style="padding: 5px; margin: 5px" align="left">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" PostBackUrl="~/UI/Public Works/ReportsDashboard.aspx"
                                        Text="Back" />
                                </td>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        CellPadding="4" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                         Width="100%" OnSelectedIndexChanged="grdDetails_SelectedIndexChanged"
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
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="PayamName" />
                                            <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                            
                                            <asp:HyperLinkField DataTextField="TotalWorkProposed" HeaderText="Total no. of works proposed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotaltobeApproved" HeaderText="No. of works to be approved">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalInProgress" HeaderText="No. of works in progress">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalClosed" HeaderText="No. of works completed">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalFundsDisbursed" HeaderText="No of Works for which Invoice Amount Released ">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalBeneficiaries" HeaderText="Direct Beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField DataTextField="TotalFemaleBeneficiaries" HeaderText="Female Beneficiaries">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField  DataTextField="TotalWorkDays" HeaderText="No. of work days created" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                             <asp:HyperLinkField  DataTextField="TotalRejected" HeaderText="Total Works Rejected">
                                                <ItemStyle HorizontalAlign="Center" />
                                                
                                                </asp:HyperLinkField>
                                                <asp:HyperLinkField  DataTextField="NoofWorkdaysCreated" HeaderText="No of Work days Created">
                                                <ItemStyle HorizontalAlign="Center" />
                                                
                                                </asp:HyperLinkField>
                                                <asp:HyperLinkField  DataTextField="TotalWageAmount" HeaderText="Total Wage Amount">
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
                                <td colspan="2">
                                    <asp:Label ID="lblmsg1" Text="Boma wise type of works report" Font-Bold="true" runat="server"
                                        Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AllowPaging="false"  AutoGenerateColumns="False"
                                        CellPadding="4" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails0_RowDataBound"
                                         Width="100%" ShowFooter="True" HorizontalAlign="center">
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
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                            <asp:BoundField DataField="Type1" HeaderText="Rural Social Infrastructure">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type2" HeaderText="Soil and water conservation and land productivity">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type3" HeaderText="Urban Works">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type4" HeaderText="Environmental and social safeguards">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type5" HeaderText="No.of Type5" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type6" HeaderText="No.of Type6" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalWorkProposed" HeaderText="Total" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
