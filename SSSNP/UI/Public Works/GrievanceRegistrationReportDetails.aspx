<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="UI_Public_Works_GrievanceRegistrationReportDetails" CodeBehind="GrievanceRegistrationReportDetails.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

                                    <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-warning"
                                        Height="32px" OnClick="PrintAllPages" ToolTip="Print" TabIndex="10" Text="Print"
                                        ValidationGroup="group" Width="90px" Visible="false" />
                                    &nbsp;
                                    <asp:Button ID="btnexp" runat="server" CausesValidation="False"
                                        CssClass="btn btn-success" Font-Size="12px" Height="32px"
                                        OnClick="btnexp_Click" Style="position: static" Text="Export"
                                        ValidationGroup="vg" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div runat="server" id="div_print">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <asp:GridView ID="grdDetails" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered" ForeColor="#333333"
                                                GridLines="None" Height="62px"
                                                OnPageIndexChanging="grdDetails_PageIndexChanging"
                                                OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:BoundField DataField="Grievance_RegistrationNo" HeaderText="Grievance Registration No"></asp:BoundField>
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                    <asp:BoundField DataField="Complainant_Name" HeaderText="Complainant Name" />
                                                    <asp:BoundField DataField="Complainant_EnrolmentNo" HeaderText="Complainant Enrolment No" />
                                                    <asp:BoundField DataField="TypeOfComplaints" HeaderText="Type Of Complaints" />
                                                    <asp:BoundField DataField="Accused_Name" HeaderText="Accused Name" />
                                                    <asp:BoundField DataField="Accused_EnrolmentNo" HeaderText="Accused Enrolment No" />
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                    ForeColor="White" />
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
