<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="SearchMasterDetails" Title=":: SSSNP Online Management System ::" CodeBehind="SearchMasterDetails.aspx.cs" %>

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
                        <h3 class="panel-title">&nbsp;Search Master Details
                        </h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%" __designer:mapid="363">
                                        <tr __designer:mapid="364">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="365">
                                                <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="165px">Select 
                                                        Type</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="367">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="368">
                                                <asp:DropDownList ID="ddltypeip" runat="server" class="form-control txtbox" Height="28px"
                                                    Width="180px">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="S">State</asp:ListItem>
                                                    <asp:ListItem Value="C">Counties</asp:ListItem>
                                                    <asp:ListItem Value="P">Payams</asp:ListItem>
                                                    <asp:ListItem Value="B">Bomas</asp:ListItem>
                                                    <asp:ListItem Value="IP">Implementing partner(IP)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="36b">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>
                                <td valign="top">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Search" Width="90px" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-warning"
                                        Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen"
                                        Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="165px"
                                        Visible="False">States Details</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="30" Width="100%">
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

                                            <asp:BoundField DataField="intStateid" HeaderText="State ID" />
                                            <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                            <%--<asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Website" HeaderText="Website" />--%>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails0_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="30" Width="100%">
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
                                            <asp:BoundField DataField="intCountieid" HeaderText="Counties ID" />
                                            <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />

                                            <%--<asp:BoundField DataField="Website" HeaderText="Website" />--%>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails1_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="30" Width="100%">
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
                                            <asp:BoundField DataField="intPayamid" HeaderText="Payam ID" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <%--<asp:BoundField DataField="Website" HeaderText="Website" />--%>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails3" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails3_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="30" Width="100%">
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

                                            <asp:BoundField DataField="intIP" HeaderText="IP ID" />
                                            <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                            <%--<asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Website" HeaderText="Website" />--%>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails2_PageIndexChanging" OnRowDataBound="grdDetails_RowDataBound"
                                        PageSize="30" Width="100%">
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
                                            <asp:BoundField DataField="intBomasid" HeaderText="Bomas ID" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <%--<asp:BoundField DataField="Website" HeaderText="Website" />--%>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
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
