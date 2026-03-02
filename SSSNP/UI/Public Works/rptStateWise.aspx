<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" EnableEventValidation = "false" Inherits="rptStateWise" Title=":: SSSNP Online Management System ::" Codebehind="rptStateWise.aspx.cs" %>

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
                            View IP Wage Details</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%" __designer:mapid="17eb">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" ValidationGroup="group">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                    OnClick="BtnSave_Click" TabIndex="10" Text="Search" Width="90px" />
                                                &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                                    Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen"
                                                    Width="90px" />
                                                &nbsp; &nbsp;<asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="btn btn-info"
                                                    Height="32px" OnClick="btnExport_Click" TabIndex="10" Text="Export" ToolTip="Click to Export"
                                                    Width="90px" />
                                            </td>
                                        </tr>
                                    </table>
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
                                        CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" GridLines="None" ShowFooter="true" 
                                        Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="15" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                    <asp:HiddenField ID="hdfid" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StateName" HeaderText="State Name"></asp:BoundField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalPaidAmount" HeaderText="Total Paid Amount"></asp:BoundField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotaUnpaidlAmount" HeaderText="Total Unpaid Amount" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalAmount" HeaderText="Total Amount" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalPaidBeneficiries" HeaderText="Total Paid Beneficiries" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalUnpaidBeneficiries" HeaderText="Total Unpaid Beneficiries" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalBeneficiries" HeaderText="Total Beneficiries" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalPaidAttendance" HeaderText="Total Paid Attendance" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalUnpaidAttendance" HeaderText="Total Unpaid Attendance" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="TotalAttendance" HeaderText="TotalAttendance" />
                                        </Columns>
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <SelectedRowStyle CssClass="GridviewScrollC1Item2" />
                                        <HeaderStyle CssClass="GridviewScrollC1Header" />
                                        <FooterStyle CssClass="GridviewScrollC1Footer" />
                                        <EditRowStyle CssClass="GridviewScrollC1Item2" />
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
