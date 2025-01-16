<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WagesPaymentDasboardDrildown" Title=":: SSSNP Online Management System ::" CodeBehind="WagesPaymentDasboardDrildown.aspx.cs" %>

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
                        <h3 class="panel-title" id="h3" runat="server">View Wage Requests</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%"
                                                    __designer:mapid="17eb">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px" ValidationGroup="group"
                                                                OnSelectedIndexChanged="ddlIP_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px"
                                                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox"
                                                                Height="33px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;</td>
                                                    </tr>

                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True"
                                                                class="form-control txtbox" Height="33px"
                                                                OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 100%"
                                                    __designer:mapid="1800">
                                                    <tr __designer:mapid="1801">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                            <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                                Height="28px" Width="180px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                                Height="28px" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox"
                                                                Height="28px" MaxLength="40" TabIndex="1"
                                                                ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;</td>
                                                    </tr>

                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px">Wage Request No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtWorkcode0" runat="server" class="form-control txtbox"
                                                                Height="28px" MaxLength="40" TabIndex="1"
                                                                ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                    Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Search"
                                                    Width="90px" />
                                                &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False"
                                                    CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" TabIndex="10"
                                                    Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" />
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>

                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>

                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333"
                                        GridLines="None" Height="62px"
                                        OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                            VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>

                                                    <asp:HiddenField ID="hdfid" runat="server" />
                                                    <asp:HiddenField ID="hdfreqids" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IPName" HeaderText="Name of the IP"></asp:BoundField>
                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code"></asp:BoundField>
                                            <asp:BoundField DataField="ReqNo" HeaderText="Wage Request No" />
                                            <asp:BoundField DataField="RDates" HeaderText="Request Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ReqFrom" HeaderText="Attendance From" />
                                            <asp:BoundField DataField="ReqTo" HeaderText="Attendance To" />
                                            <asp:TemplateField HeaderText="Beneficiaries selected for Payment">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirect" NavigateUrl='<%# String.Format("ViewBenfWages.aspx?WorkReqId={0}",  Eval("intWagePaymentReqid")) %>' Text='View'>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label377" runat="server" CssClass="LBLBLACK" Width="180px">Status</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                        Height="33px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Width="180px">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:Label ID="Label378" runat="server" CssClass="LBLBLACK" Width="180px">Remarks</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPromotor" runat="server" class="form-control txtbox" Height="40px"
                                                        MaxLength="40" TabIndex="1" TextMode="MultiLine" ValidationGroup="group"
                                                        Width="180px"></asp:TextBox>
                                                    <br />
                                                    <asp:Button ID="BtnSave" runat="server" CausesValidation="False" CssClass="btn-success"
                                                        Height="32px" OnClick="BtnSave_Click1" OnClientClick="return confirm('Do you want to update the record ? ');"
                                                        TabIndex="10" Text="Submit" ValidationGroup="group" Width="80px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pay Wages">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirectA" NavigateUrl='<%# String.Format("PaymentSelectionPayingagencyBenfWages.aspx?WorkReqId={0}",  Eval("intWagePaymentReqPAid")) %>' Text='Pay'>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print Wage Invoice">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirectAA" NavigateUrl='<%# String.Format("ViewSelectionPayingagencyBenfWages.aspx?WorkReqId={0}",  Eval("intWagePaymentReqPAid")) %>' Text='Print'>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Uploaded Paid wages">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirectAAA" NavigateUrl='<%# Eval("UploadSignedDoc") %>' Text='View'>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select Rejected Beneficiaries for Invoice">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirectA123" NavigateUrl='<%# String.Format("UpdateSelectionPayingagencyBenfWages.aspx?WorkReqId={0}&WageReqInvoiceid={1}",  Eval("intWagePaymentReqid"),Eval("intWagePaymentReqPAid")) %>' Text='Update'>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
