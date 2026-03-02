<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="UI_Public_Works_APIDashboardDrillDown2" Title=":: :: SNSOP : ::" CodeBehind="DeleteBenfAfterConfirmation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

    </script>
    <script type="text/javascript">
        function GetRowValue(val) {
            if (val != '&nbsp;') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;

            }
            window.opener.document.forms[0].submit();
            self.close();
        }
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Delete?"))
                return true;
            else
                return false;
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Delete Benficiary Allotment , Attendance and Wages</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%" __designer:mapid="17eb">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Benificiary Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtBenificiaryName" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" Height="33px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" __designer:mapid="17f5" r runat="server" visible="false">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                                Height="33px" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 100%" __designer:mapid="1800">
                                                    <tr __designer:mapid="1801">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                            <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px">Enrollment No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtEnrollmentNo" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                    OnClick="BtnSave_Click" TabIndex="10" Text="Search" Width="90px" />
                                                &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                                    Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen"
                                                    Width="90px" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="LblMessage2" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="50" Width="99%" OnSelectedIndexChanged="grdDetails_SelectedIndexChanged"
                                        ShowFooter="True">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <asp:HiddenField ID="hdfid" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="All">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5"
                                                        AutoPostBack="True" onclick="CheckAll(this);" />
                                                    &nbsp;
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BenfiName" HeaderText="Benificiary Name" />
                                            <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
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
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center"
                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">&nbsp;
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-warning"
                                                Height="32px" OnClick="Button2_Click" TabIndex="10" Text="Delete"
                                                ValidationGroup="group" Width="155px" OnClientClick="return UserDeleteConfirmation();" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert"
                                            href="#">×</a> <strong>Success!</strong><asp:Label
                                                ID="Label2" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <asp:HiddenField ID="hdfID" runat="server" />
                                    <asp:HiddenField ID="hdfFlagID" runat="server" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
